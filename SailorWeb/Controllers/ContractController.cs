using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SailorWeb.Models;
using SailorDomain.Entities;
using SailorDomain.Services;
using PagedList;
using SailorWeb.ViewModels;
using SailorWeb.Services;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using SailorWeb.Infrastructure;

namespace SailorWeb.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        readonly IContractService _contractService;
        readonly ISailorService _sailorService;
        readonly IVesselService _vesselService;
        readonly IShipownerService _shipownerService;
        readonly INoticeService _noticeService;
        readonly IServiceRecordService _serviceRecordService;
        OperationResult operationResult;
        public ContractController(IContractService contractService, ISailorService sailorService, IVesselService vesselService, 
            IShipownerService shipownerService, IServiceRecordService serviceRecordService,INoticeService noticeService)
        {
            _contractService = contractService;
            _sailorService = sailorService;
            _vesselService = vesselService;
            _shipownerService = shipownerService;
            _serviceRecordService = serviceRecordService;
            _noticeService = noticeService;
        }

        // GET: /Contract/
        public ActionResult Index(string SailorName, string VesselName, string ShipownerName, string status,
            DateTime? AboardBeginDate, DateTime? AboardEndDate, DateTime? AshoreBeginDate, DateTime? AshoreEndDate, int? page)
        {
            var contracts = _contractService.GetEntities();
            if (AboardBeginDate.HasValue)
            {
                contracts = contracts.Where(c => c.AboardDate >= AboardBeginDate.Value);
                ViewBag.AboardBeginDate = AboardBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (AboardEndDate.HasValue)
            {
                contracts = contracts.Where(c => c.AboardDate <= AboardEndDate.Value);
                ViewBag.AboardEndDate = AboardEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (AshoreBeginDate.HasValue)
            {
                contracts = contracts.Where(c => c.AshoreDate >= AshoreBeginDate.Value);
                ViewBag.AshoreBeginDate = AshoreBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (AshoreEndDate.HasValue)
            {
                contracts = contracts.Where(c => c.AshoreDate <= AshoreEndDate.Value);
                ViewBag.AshoreEndDate = AshoreEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (!String.IsNullOrWhiteSpace(SailorName))
            {
                contracts = contracts.Where(c => c.SailorName.Contains(SailorName));
            }
            if (!String.IsNullOrWhiteSpace(ShipownerName))
            {
                contracts = contracts.Where(c => c.ShipownerName.Contains(ShipownerName));
            }
            if (!String.IsNullOrWhiteSpace(VesselName))
            {
                contracts = contracts.Where(c => c.VesselName.Contains(VesselName));
            }
            var now = DateTime.Now;
            switch (status)
            {
                case "todo":
                    contracts = contracts.Where(c => !c.AboardDate.HasValue);
                    break;
                case "normal":
                    contracts = contracts.Where(c => !c.Complete && c.AboardDate.HasValue);
                    break;
                case "overdue":
                    contracts = contracts.Where(c => c.Complete);
                    break;
                case "notice":
                    contracts = contracts.Where(c => c.NoticeDate < now && !c.Complete);
                    break;
                default: break;
            }

            contracts = contracts.OrderByDescending(i => i.ContractID);

            ViewBag.SailorName = SailorName;
            ViewBag.ShipownerName = ShipownerName;
            ViewBag.VesselName = VesselName;
            ViewBag.status = status;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(contracts.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = _contractService.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: /Contract/Create
        public ActionResult Create()
        {
            string SailorID = Request.QueryString["SailorID"];
            string VesselID = Request.QueryString["VesselID"];
            var ShipownerID = Request.QueryString["ShipownerID"];
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", SailorID);
            ViewBag.VesselID = new SelectList(_vesselService.GetEntities(), "VesselID", "Name", VesselID);
            ViewBag.medium = Request.QueryString["medium"];

            int vesselKey;
            if (Int32.TryParse(VesselID, out vesselKey))
            {
                ShipownerID = _vesselService.Find(vesselKey).ShipownerID.ToString();
            }
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", ShipownerID);
            return View();
        }

        // POST: /Contract/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(contract.SailorID);
                var vessel = _vesselService.Find(contract.VesselID);
                var shipowner = _shipownerService.Find(vessel.ShipownerID);
                contract.SailorName = sailor.Name;
                contract.VesselName = vessel.Name;
                contract.ShipownerName = shipowner.Name;

                contract = _contractService.Add(contract);

                if ("Sailor".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Sailor", new { id = contract.SailorID, tab = "tab_contract" });
                }
                else if ("Vessel".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Vessel", new { id = contract.VesselID, tab = "tab_contract" });
                }

                return RedirectToAction("Index", new { status = "todo" });
            }

            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", contract.SailorID);
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", contract.ShipownerID);
            ViewBag.VesselID = new SelectList(_vesselService.GetEntities(), "VesselID", "Name", contract.VesselID);
            return View(contract);
        }

        // GET: /Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var record = _serviceRecordService.Find(r => r.ContractID == id);
            if (record != null)
            {
                var sailor = _sailorService.Find(s => s.Status == SailorStatus.在船 && s.ServiceRecordID == record.ServiceRecordID);
                if (sailor != null)
                {
                    sailor.Status = SailorStatus.待派;
                    sailor.ServiceRecordID = null;
                    sailor.VesselID = null;
                    sailor.VesselName = "";
                    _sailorService.Update(sailor);
                }
                _serviceRecordService.Delete(record.ServiceRecordID);
            }
            _contractService.Delete(id);
            _noticeService.DeleteRange(n => n.Source == NoticeSource.Contract && n.SourceID == id);
            return RedirectToAction("Index");
        }

        public ActionResult Aboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = _contractService.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            var adjust = new AboardContract()
            {
                ContractID = contract.ContractID,
                SailorName = contract.SailorName,
                VesselName = contract.VesselName,
                Term = contract.Term,
                AboardDate = contract.AboardDate,
                AshoreDate = contract.AshoreDate,
                NoticeDate = contract.NoticeDate
            };
            return PartialView(adjust);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Aboard(AboardContract aboard)
        {
            if (ModelState.IsValid)
            {
                var contract = _contractService.Find(aboard.ContractID);
                contract.AboardDate = aboard.AboardDate;
                contract.AshoreDate = aboard.AshoreDate;
                contract.NoticeDate = aboard.NoticeDate;
                var signedContract = _contractService.SignedContract(contract.SailorID, contract);
                if (signedContract != null)
                {
                    string error="船员" + signedContract.SailorName + "已与船舶" + signedContract.VesselName + "签订了日期为" + signedContract.AboardDate.Value.ToLongDateString() + "至" + signedContract.AshoreDate.Value.ToLongDateString() + "的合同";
                    operationResult = new OperationResult(false);
                    operationResult.Message = error;
                    return Json(operationResult);
                }
                //保存服务记录
                ServiceRecord record = new ServiceRecord()
                {
                    SailorID = contract.SailorID,
                    Post = contract.Post,
                    SailZone = aboard.SailZone,
                    AboardDate = contract.AboardDate,
                    AboardPlace = aboard.AboardPlace,
                    AshoreDate=contract.AshoreDate,
                    Remark = aboard.Remark,
                    ShipownerID = contract.ShipownerID,
                    VesselID = contract.VesselID,
                    SailorName = contract.SailorName,
                    ShipownerName = contract.ShipownerName,
                    VesselName = contract.VesselName,
                    ContractID = contract.ContractID,
                };

                record = _serviceRecordService.Add(record,false);
                _contractService.Update(contract);

                var sailor = _sailorService.Find(contract.SailorID);
                sailor.Status = SailorStatus.在船;
                sailor.VesselID = contract.VesselID;
                sailor.VesselName = contract.VesselName;
                sailor.ServiceRecordID = record.ServiceRecordID;
                _sailorService.Update(sailor);
                
                _noticeService.AddSailorContractNotice(contract);
                var contractResult = new OperationResult<object>(true);
                var data = new
                {
                    ContractID = contract.ContractID,
                    AboardDate = contract.AboardDate.Value.ToString("yyyy-MM-dd"),
                    AshoreDate = contract.AshoreDate.Value.ToString("yyyy-MM-dd")
                };
                contractResult.Entity = data;
                return Json(contractResult);
            }
            operationResult = new OperationResult(false);
            operationResult.Message = "输入数据无效";
            return Json(operationResult);
        }

        public ActionResult Export(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = _contractService.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            var sailor = _sailorService.Find(contract.SailorID);
            string companyId = contract.SysCompanyId.ToString();
            Dictionary<String, String> dic = new Dictionary<string, string>();
            dic.Add("ContractNo", contract.ContractNo);
            dic.Add("SailorName", contract.SailorName);
            dic.Add("ShipownerName", contract.ShipownerName);
            dic.Add("VesselName", contract.VesselName);
            dic.Add("UpperSigningDate", UpperDateConvert.dateToUpper(contract.SigningDate.Value));
            dic.Add("SailorName2", contract.SailorName);
            dic.Add("IdentityNo", sailor.IdentityNo);
            dic.Add("Mobile", sailor.Mobile);
            dic.Add("HomeContacter", sailor.HomeContacter);
            dic.Add("Address", sailor.Address);
            dic.Add("HomeTel", sailor.HomeTel);
            dic.Add("Post", contract.Post.ToString());
            if (contract.Term.HasValue)
            {
                dic.Add("Term", contract.Term.ToString());
            }
            if (contract.Wage.HasValue)
            {
                dic.Add("Wage", contract.Wage.ToString());
            }
            if (contract.HomeWage.HasValue)
            {
                dic.Add("HomeWage", contract.HomeWage.ToString());
            }
            if (contract.ShipWage.HasValue)
            {
                dic.Add("ShipWage", contract.ShipWage.ToString());
            }
            if (contract.VacationWage.HasValue)
            {
                dic.Add("VacationWage", contract.VacationWage.ToString());
            }
            string wageInterval="按月";
            switch (contract.WageInterval)
            {
                case 1:
                    wageInterval = "按月";
                    break;
                case 2:
                    wageInterval = "按每两个月";
                    break;
                case 3:
                    wageInterval = "按每三个月";
                    break;
                case 4:
                    wageInterval = "按每四个月";
                    break;
                default: break;
            }
            dic.Add("WageInterval", wageInterval);
            dic.Add("AccountName", sailor.AccountName);
            dic.Add("Bank", sailor.Bank);
            dic.Add("WageCardNo", sailor.WageCardNo);
            return ExportContract(dic, contract.SysCompanyId.ToString(), contract.SailorID.ToString(), contract.SailorName);
        }

        private FileResult ExportContract(Dictionary<String, String> contractDic,string companyId,string sailorId,string sailorName)
        {
            string sourcepath = HttpContext.Server.MapPath("~/Files/Contract/Template/" + companyId + ".docx");
            if (!System.IO.File.Exists(sourcepath))
                sourcepath = HttpContext.Server.MapPath("~/Files/Contract/Template/0.docx");
            string targetPath = HttpContext.Server.MapPath("~/Files/Contract/Export/" + sailorId + "_外派合同.docx");
            System.IO.File.Copy(sourcepath, targetPath, true);
            var contentControlManager = new ContentControlManager();
            contentControlManager.OpenDocuemnt(targetPath);
            contentControlManager.UpdateText(contractDic);
            contentControlManager.CloseDocument();
            var name = Path.GetFileName(targetPath);
            string contentType = MimeMapping.GetMimeMapping(name);
            return File(targetPath, contentType, Url.Encode(sailorName + "_外派合同.docx"));
        }
    }
}
