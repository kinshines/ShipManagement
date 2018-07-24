using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ship.Core.Entities;
using Ship.Core.Enums;
using Ship.Infrastructure.Services;
using Ship.Web.ViewModels;
using X.PagedList;

namespace Ship.Web.Controllers
{
    public class VesselAccountController : Controller
    {
        readonly VesselAccountService _accountService;
        readonly VesselService _vesselService;
        readonly CompanyService _companyService;
        readonly ShipownerService _shipownerService;
        readonly UploadFileService _uploadService;
        public VesselAccountController(VesselAccountService accountService, VesselService vesselService,
            CompanyService companyService, ShipownerService shipownerService, UploadFileService uploadService)
        {
            _accountService = accountService;
            _vesselService = vesselService;
            _companyService = companyService;
            _shipownerService = shipownerService;
            _uploadService = uploadService;
        }

        // GET: /VesselCost/
        public ActionResult CostList(string status, string InvoiceNo, int? VesselID, int? CompanyID,
            int? FeeItem, DateTime? InvoiceBeginDate, DateTime? InvoiceEndDate, int? page)
        {
            var query = _accountService.GetCosts();
            if (!String.IsNullOrWhiteSpace(InvoiceNo))
            {
                query = query.Where(c => c.InvoiceNo.Contains(InvoiceNo));
            }
            if (FeeItem.HasValue)
            {
                query = query.Where(c => c.FeeItem == (VesselFeeItem)FeeItem);
            }
            if (InvoiceBeginDate.HasValue)
            {
                query = query.Where(c => c.InvoiceDate >= InvoiceBeginDate.Value);
                ViewBag.InvoiceBeginDate = InvoiceBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (InvoiceEndDate.HasValue)
            {
                query = query.Where(c => c.InvoiceDate <= InvoiceEndDate.Value);
                ViewBag.InvoiceEndDate = InvoiceEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (VesselID.HasValue)
            {
                query = query.Where(c => c.VesselID == VesselID.Value);
            }
            if (CompanyID.HasValue)
            {
                query = query.Where(c => c.CompanyID == CompanyID.Value);
            }
            if (!String.IsNullOrWhiteSpace(status) && status == "debt")
            {
                query = query.Where(c => c.Payoff == false);
            }

            query = query.OrderByDescending(c => c.InvoiceDate);
            ViewBag.CompanyID = new SelectList(_companyService.GetEntities(), "CompanyID", "Name", CompanyID);
            ViewBag.status = status;
            ViewBag.InvoiceNo = InvoiceNo;

            var feeItems = from VesselFeeItem f in Enum.GetValues(typeof(VesselFeeItem))
                           select new { ID = (int)f, Name = f.ToString() };
            ViewBag.FeeItem = new SelectList(feeItems, "ID", "Name", FeeItem);
            ViewBag.SelectedFeeItem = FeeItem;
            ViewBag.SelectedVesselID = VesselID;
            ViewBag.SelectedCompanyID = CompanyID;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: /VesselCost/Create
        public ActionResult Cost()
        {
            ViewBag.medium = Request.Query["medium"];
            return View();
        }

        // POST: /VesselCost/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cost(VesselAccount vesselcost,IFormFile invoiceFile, IFormFile receiptFile)
        {
            if (ModelState.IsValid)
            {
                vesselcost.InvoiceFileID = _uploadService.AddFile(invoiceFile);

                var vessel = _vesselService.Find(vesselcost.VesselID);
                var shipowner = _shipownerService.Find(vessel.ShipownerID);
                if (!String.IsNullOrWhiteSpace(vesselcost.CompanyName))
                {
                    vesselcost.CompanyName = vesselcost.CompanyName.Trim();
                    var company = _companyService.Find(c => c.Name == vesselcost.CompanyName);
                    if (company == null)
                    {
                        company = new Company()
                        {
                            Name = vesselcost.CompanyName
                        };
                        company = _companyService.Add(company);
                    }
                    vesselcost.CompanyID = company.CompanyID;
                    vesselcost.CompanyName = company.Name;
                }

                vesselcost.VesselName = vessel.Name;
                vesselcost.ShipownerID = vessel.ShipownerID;
                vesselcost.ShipownerName = shipowner.Name;

                var payment = new VesselCostPayment()
                {
                    Payment = vesselcost.Payment,
                    Debt = vesselcost.Debt,
                    USPayment = vesselcost.USPayment,
                    USDebt = vesselcost.USDebt,
                    PaymentDate = vesselcost.PaymentDate ?? DateTime.Now.Date,
                    VesselAccountID = vesselcost.VesselAccountID,
                };

                vesselcost = _accountService.AddCost(vesselcost);

                if (payment.Payment > 0 || payment.USPayment > 0)
                {
                    payment.ReceiptFileID = _uploadService.AddFile(receiptFile);
                    payment.VesselAccountID = vesselcost.VesselAccountID;
                    _accountService.AddPayment(payment, vesselcost.Payoff);
                }

                if ("Vessel".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Vessel", new { id = vesselcost.VesselID, tab = "tab_vesselcost" });
                }
                return RedirectToAction("CostList", new { VesselID = vesselcost.VesselID, VesselName = vesselcost.VesselName });
            }

            ViewBag.VesselID = new SelectList(_vesselService.GetManagedVessels(), "VesselID", "Name", vesselcost.VesselID);
            ViewBag.CompanyID = new SelectList(_companyService.GetEntities(), "CompanyID", "Name", vesselcost.CompanyID);
            return View(vesselcost);
        }

        public ActionResult Settle(int? VesselCostID)
        {
            if (VesselCostID == null)
            {
                return BadRequest();
            }
            VesselAccount cost = _accountService.Find(VesselCostID);
            if (cost == null)
            {
                return NotFound();
            }
            var payment = new SettlePayment()
            {
                Cost = cost.Cost,
                USCost = cost.USCost,
                PaidCost = cost.Payment,
                USPaidCost = cost.USPayment,
                Debt = cost.Debt,
                USDebt = cost.USDebt,
                VesselCostID = cost.VesselAccountID,
                Payment = cost.Debt,
                USPayment = cost.USDebt,
                PaymentDate = DateTime.Now
            };
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settle(SettlePayment settle,IFormFile receiptFile)
        {
            if (ModelState.IsValid)
            {
                VesselCostPayment payment = new VesselCostPayment()
                {
                    VesselAccountID = settle.VesselCostID,
                    Payment = settle.Payment,
                    USPayment = settle.USPayment,
                    Debt = settle.Debt,
                    USDebt = settle.USDebt,
                    PaymentDate = settle.PaymentDate,
                    Remark = settle.Remark
                };
                payment.ReceiptFileID = _uploadService.AddFile(receiptFile);

                _accountService.AddPayment(payment, settle.Payoff);
                var cost = _accountService.Find(payment.VesselAccountID);
                return RedirectToAction("CostList", new { VesselID = cost.VesselID, VesselName = cost.VesselName });
            }
            return View(settle);
        }

        public ActionResult BalanceList(int? VesselID, int? ShipownerID, int? page)
        {
            var vesselbalance = _accountService.BalanceList();
            vesselbalance = vesselbalance.Where(b => !VesselID.HasValue || b.VesselID == VesselID.Value);
            if (!VesselID.HasValue)
            {
                vesselbalance = vesselbalance.Where(b => !ShipownerID.HasValue || b.ShipownerID == ShipownerID.Value);
            }
            vesselbalance = vesselbalance.OrderByDescending(b => b.VesselID);
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", ShipownerID);
            ViewBag.VesselID = new SelectList(_vesselService.GetManagedVessels(), "VesselID", "Name", VesselID);
            if (VesselID.HasValue)
            {
                ViewBag.Vessel = VesselID.Value;
            }
            if (ShipownerID.HasValue)
            {
                ViewBag.Shipowner = ShipownerID.Value;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(vesselbalance.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CostBill(int? VesselID, int? ShipownerID, DateTime? BeginDate, DateTime? EndDate)
        {
            DateTime end = DateTime.Now;
            DateTime begin = new DateTime(end.Year, end.Month, 1);
            if (BeginDate.HasValue)
            {
                begin = BeginDate.Value;
            }
            if (EndDate.HasValue)
            {
                end = EndDate.Value;
            }

            var query = _accountService.GetCosts().Where(c => c.InvoiceDate >= begin && c.InvoiceDate <= end)
                .Where(c => !VesselID.HasValue || c.VesselID == VesselID.Value);

            if (!VesselID.HasValue)
            {
                query = query.Where(c => !ShipownerID.HasValue || c.ShipownerID == ShipownerID.Value);
            }

            var list = query.ToList();
            var groups = list.GroupBy(c => c.VesselID);
            var result =
                from g in groups
                select new VesselCostBill()
                {
                    VesselID = g.Key,
                    VesselName = g.OrderByDescending(c => c.VesselAccountID).First().VesselName,
                    ShipownerID = g.OrderByDescending(c => c.VesselAccountID).First().ShipownerID,
                    ShipownerName = g.OrderByDescending(c => c.VesselAccountID).First().ShipownerName,
                    BeginDate = begin,
                    EndDate = end,
                    Sailor = g.Where(c => c.FeeItem == VesselFeeItem.船员花销).Sum(c => c.Cost),
                    Material = g.Where(c => c.FeeItem == VesselFeeItem.物料).Sum(c => c.Cost),
                    Spareparts = g.Where(c => c.FeeItem == VesselFeeItem.备件).Sum(c => c.Cost),
                    Maintenance = g.Where(c => c.FeeItem == VesselFeeItem.维修保养).Sum(c => c.Cost),
                    LubricatingOil = g.Where(c => c.FeeItem == VesselFeeItem.滑油).Sum(c => c.Cost),
                    DailyExpenses = g.Where(c => c.FeeItem == VesselFeeItem.日常花销).Sum(c => c.Cost),
                    Communication = g.Where(c => c.FeeItem == VesselFeeItem.通讯费).Sum(c => c.Cost),
                    Insurance = g.Where(c => c.FeeItem == VesselFeeItem.保险费).Sum(c => c.Cost),
                    Others = g.Where(c => c.FeeItem == VesselFeeItem.其他费用).Sum(c => c.Cost),
                    Certificate = g.Where(c => c.FeeItem == VesselFeeItem.检验发证).Sum(c => c.Cost),
                    USSailor = g.Where(c => c.FeeItem == VesselFeeItem.船员花销).Sum(c => c.USCost),
                    USMaterial = g.Where(c => c.FeeItem == VesselFeeItem.物料).Sum(c => c.USCost),
                    USSpareparts = g.Where(c => c.FeeItem == VesselFeeItem.备件).Sum(c => c.USCost),
                    USMaintenance = g.Where(c => c.FeeItem == VesselFeeItem.维修保养).Sum(c => c.USCost),
                    USLubricatingOil = g.Where(c => c.FeeItem == VesselFeeItem.滑油).Sum(c => c.USCost),
                    USDailyExpenses = g.Where(c => c.FeeItem == VesselFeeItem.日常花销).Sum(c => c.USCost),
                    USCommunication = g.Where(c => c.FeeItem == VesselFeeItem.通讯费).Sum(c => c.USCost),
                    USInsurance = g.Where(c => c.FeeItem == VesselFeeItem.保险费).Sum(c => c.USCost),
                    USOthers = g.Where(c => c.FeeItem == VesselFeeItem.其他费用).Sum(c => c.USCost),
                    USCertificate = g.Where(c => c.FeeItem == VesselFeeItem.检验发证).Sum(c => c.USCost)
                };

            ViewBag.BeginDate = begin;
            ViewBag.EndDate = end;
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", ShipownerID);
            ViewBag.VesselID = new SelectList(_vesselService.GetManagedVessels(), "VesselID", "Name", VesselID);
            return View(result);
        }

        // GET: /VesselDeposit/
        public ActionResult DepositList(int? VesselID, int? ShipownerID, DateTime? BeginDate, DateTime? EndDate, int? page)
        {
            var vesseldeposits = _accountService.GetDeposits();
            if (BeginDate.HasValue)
            {
                vesseldeposits = vesseldeposits.Where(c => c.PaymentDate >= BeginDate.Value);
            }
            if (EndDate.HasValue)
            {
                vesseldeposits = vesseldeposits.Where(c => c.PaymentDate <= EndDate.Value);
            }
            if (VesselID.HasValue)
            {
                vesseldeposits = vesseldeposits.Where(c => c.VesselID == VesselID.Value);
            }
            else if (ShipownerID.HasValue)
            {
                vesseldeposits = vesseldeposits.Where(c => c.ShipownerID == ShipownerID.Value);
            }

            vesseldeposits = vesseldeposits.OrderByDescending(d => d.PaymentDate);
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", ShipownerID);
            ViewBag.VesselID = new SelectList(_vesselService.GetEntities(), "VesselID", "Name", VesselID);
            if (BeginDate.HasValue)
            {
                ViewBag.BeginDate = BeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (EndDate.HasValue)
            {
                ViewBag.EndDate = EndDate.Value.ToString("yyyy-MM-dd");
            }
            if (VesselID.HasValue)
            {
                ViewBag.Vessel = VesselID.Value;
            }
            if (ShipownerID.HasValue)
            {
                ViewBag.Shipowner = ShipownerID.Value;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(vesseldeposits.ToPagedList(pageNumber, pageSize));
        }

        // GET: /VesselDeposit/Deposit
        public ActionResult Deposit()
        {
            ViewBag.medium = Request.Query["medium"];
            return View();
        }

        // POST: /VesselDeposit/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit([Bind("VesselDepositID,Deposit,USDeposit,Remark,PaymentDate,VesselID,ShipownerID")] VesselAccount vesseldeposit)
        {
            if (ModelState.IsValid)
            {
                var vessel = _vesselService.Find(vesseldeposit.VesselID);
                var shipowner = _shipownerService.Find(vessel.ShipownerID);
                vesseldeposit.VesselName = vessel.Name;
                vesseldeposit.ShipownerName = shipowner.Name;
                _accountService.AddDeposit(vesseldeposit);

                if ("Vessel".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Vessel", new { id = vesseldeposit.VesselID, tab = "tab_vesseldeposit" });
                }
                return RedirectToAction("DepositList", new { VesselID = vesseldeposit.VesselID, VesselName = vesseldeposit.VesselName });
            }

            return View(vesseldeposit);
        }

        public ActionResult Sign(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            VesselAccount cost = _accountService.Find(id);
            if (cost == null)
            {
                return NotFound();
            }
            return View(cost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignFile(int? VesselAccountID,IFormFile signFile)
        {
            var cost = _accountService.Find(VesselAccountID);
            cost.SignFileID = _uploadService.AddFile(signFile);
            _accountService.Update(cost);
            return RedirectToAction("CostList", new { VesselID = cost.VesselID, VesselName = cost.VesselName });
        }

        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /VesselCost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cost = _accountService.Find(id);
            _accountService.DeleteAccount(id);
            return RedirectToAction("CostList", new { VesselID = cost.VesselID, VesselName = cost.VesselName });
        }

        public ActionResult DeletePayment(int? id)
        {
            return PartialView();
        }

        // POST: /Notice/Delete/5
        [HttpPost, ActionName("DeletePayment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePaymentConfirmed(int id)
        {
            _accountService.DeletePayment(id);
            return RedirectToAction("CostList");
        }

        public ActionResult PaymentList(int id)
        {
            var cost = _accountService.Find(id);
            return PartialView(cost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyRemark(int VesselAccountID, string Remark)
        {
            var cost = _accountService.Find(VesselAccountID);
            if (cost == null)
                return Json("error");
            cost.Remark = Remark;
            if (_accountService.Update(cost))
            {
                var formattedData = new
                {
                    Id = cost.VesselAccountID,
                    Remark = cost.Remark
                };
                return Json(formattedData);
            }
            return Json("error");
        }
    }
}