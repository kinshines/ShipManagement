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
using PagedList;
using SailorWeb.Services;
using SailorDomain.Services;

namespace SailorWeb.Controllers
{
    [Authorize]
    public class ServiceRecordController : Controller
    {
        readonly IServiceRecordService _recordService;
        readonly ISailorService _sailorService;
        readonly IVesselService _vesselService;
        readonly IShipownerService _shipownerService;
        readonly IContractService _contractService;
        readonly IExperienceService _experienceService;
        readonly ISysCompanyService _companyService;
        public ServiceRecordController(IServiceRecordService recordService, ISailorService sailorService,
            IVesselService vesselService, IShipownerService shipownerService, IContractService contractService,
            IExperienceService experienceService, ISysCompanyService companyService)
        {
            _recordService = recordService;
            _sailorService = sailorService;
            _vesselService = vesselService;
            _shipownerService = shipownerService;
            _contractService = contractService;
            _experienceService = experienceService;
            _companyService = companyService;
        }

        // GET: /ServiceRecord/
        public ActionResult Index(string SailorName, string VesselName, string SailZone,string status,
            DateTime? AboardBeginDate, DateTime? AboardEndDate, DateTime? AshoreBeginDate, 
            DateTime? AshoreEndDate, int? Post, int? page)
        {
            var query = _recordService.GetEntities();
            if (!String.IsNullOrWhiteSpace(SailorName))
            {
                query = query.Where(x => x.SailorName.Contains(SailorName));
            }
            if (!String.IsNullOrWhiteSpace(VesselName))
            {
                query = query.Where(x => x.VesselName.Contains(VesselName));
            }
            if (!String.IsNullOrWhiteSpace(SailZone))
            {
                query = query.Where(x => x.SailZone.Contains(SailZone));
            }
            if ("aboard".Equals(status))
            {
                query = query.Where(x => !x.Complete);
            }
            if (AboardBeginDate.HasValue)
            {
                query = query.Where(x => x.AboardDate >= AboardBeginDate);
                ViewBag.AboardBeginDate = AboardBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (AboardEndDate.HasValue)
            {
                query = query.Where(x => x.AboardDate <= AboardEndDate);
                ViewBag.AboardEndDate = AboardEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (AshoreBeginDate.HasValue)
            {
                query = query.Where(x => x.AshoreDate >= AshoreBeginDate);
                ViewBag.AshoreBeginDate = AshoreBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (AshoreEndDate.HasValue)
            {
                query = query.Where(x => x.AshoreDate <= AshoreEndDate);
                ViewBag.AshoreEndDate = AshoreEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (Post.HasValue)
            {
                query = query.Where(x => x.Post == (Post)Post);
            }
            query = query.OrderByDescending(x => x.ServiceRecordID);

            ViewBag.SailorName = SailorName;
            ViewBag.VesselName = VesselName;
            ViewBag.SailZone = SailZone;
            ViewBag.status = status;

            var posts = from Post p in Enum.GetValues(typeof(Post))
                        select new { ID = (int)p, Name = p.ToString() };
            ViewBag.Post = new SelectList(posts, "ID", "Name", Post);
            ViewBag.SelectedPost = Post;
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: /ServiceRecord/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRecord servicerecord = _recordService.Find(id);
            if (servicerecord == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Details", servicerecord);
            }
            return View(servicerecord);
        }

        // GET: /ServiceRecord/Create
        public ActionResult Create()
        {
            var SailorID = Request.QueryString["SailorID"];
            var VesselID = Request.QueryString["VesselID"];
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

        // POST: /ServiceRecord/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ServiceRecordID,Post,SailZone,AboardDate,AboardPlace,AshoreDate,AshorePlace,AshoreReason,Remark,Comment,SailorID,ShipownerID,VesselID,SailorName,ShipownerName,VesselName")] ServiceRecord servicerecord)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(servicerecord.SailorID);
                var vessel = _vesselService.Find(servicerecord.VesselID);
                var shipowner = _shipownerService.Find(vessel.ShipownerID);

                servicerecord.SailorName = sailor.Name;
                servicerecord.VesselName = vessel.Name;
                servicerecord.ShipownerName = shipowner.Name;

                _recordService.Add(servicerecord);

                if ("Sailor".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Sailor", new { id = servicerecord.SailorID, tab = "tab_servicerecord" });
                }
                return RedirectToAction("Index");
            }

            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", servicerecord.SailorID);
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", servicerecord.ShipownerID);
            ViewBag.VesselID = new SelectList(_vesselService.GetEntities(), "VesselID", "Name", servicerecord.VesselID);
            return View(servicerecord);
        }

        // GET: /ServiceRecord/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = "编辑服务记录";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRecord servicerecord = _recordService.Find(id);
            if (servicerecord == null)
            {
                return HttpNotFound();
            }
            if ("Ashore".Equals(Request.QueryString["medium"]))
            {
                ViewBag.Title = "船员下船";
            }

            ViewBag.medium = Request.QueryString["medium"];
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", servicerecord.SailorID);
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", servicerecord.ShipownerID);
            ViewBag.VesselID = new SelectList(_vesselService.GetEntities(), "VesselID", "Name", servicerecord.VesselID);
            
            return View(servicerecord);
        }

        // POST: /ServiceRecord/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceRecord servicerecord)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(servicerecord.SailorID);
                var vessel = _vesselService.Find(servicerecord.VesselID);
                var shipowner = _shipownerService.Find(vessel.ShipownerID);

                servicerecord.SailorName = sailor.Name;
                servicerecord.VesselName = vessel.Name;
                servicerecord.ShipownerName = shipowner.Name;

                _recordService.Update(servicerecord);

                if ("Ashore".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Index", "Sailor");
                }

                return RedirectToAction("Index");
            }
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", servicerecord.SailorID);
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", servicerecord.ShipownerID);
            ViewBag.VesselID = new SelectList(_vesselService.GetEntities(), "VesselID", "Name", servicerecord.VesselID);
            return View(servicerecord);
        }

        // GET: /ServiceRecord/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /ServiceRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sailor = _sailorService.Find(s => s.ServiceRecordID == id);
            if(sailor!=null)
            {
                sailor.Status = SailorStatus.待派;
                sailor.ServiceRecordID = null;
                _sailorService.Update(sailor);
            }
            _recordService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Ashore(int? id, int? ContractID)
        {
            if (id == null&&ContractID==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRecord record;
            if (id != null)
            {
                record = _recordService.Find(id);
            }
            else
            {
                record = _recordService.Find(r => r.ContractID == ContractID);
            }
            
            if (record == null||record.Complete)
            {
                return HttpNotFound();
            }
            return PartialView(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ashore(ServiceRecord viewRecord)
        {
            var record = _recordService.Find(viewRecord.ServiceRecordID);
            record.AshoreDate = viewRecord.AshoreDate;
            record.AshorePlace = viewRecord.AshorePlace;
            record.AshoreReason = viewRecord.AshoreReason;
            record.Comment = viewRecord.Comment;
            record.Remark = viewRecord.Remark;
            record.Complete = true;

            var sailor = _sailorService.Find(record.SailorID);
            var contract = _contractService.Find(record.ContractID);
            var vessel = _vesselService.Find(record.VesselID);

            sailor.Status = SailorStatus.休假;
            sailor.VesselID = null;
            sailor.VesselName = null;
            sailor.ServiceRecordID = null;
            _sailorService.Update(sailor, false);

            contract.AshoreDate = record.AshoreDate;
            contract.Complete = true;
            _contractService.Update(contract, false);
            _recordService.Update(record);

            int months = 0;
            var company = _companyService.Find(sailor.SysCompanyId);
            if (record.AshoreDate.HasValue && record.AboardDate.HasValue)
            {
                months = GetMonthSpan(record.AboardDate.Value, record.AshoreDate.Value);
            }
            var expericence = new Experience()
            {
                BeginTime = record.AboardDate,
                EndTime = record.AshoreDate,
                DurationMonth = months,
                Post = record.Post,
                CompanyName = company.Name,
                SailorID = sailor.SailorID,
                SailorName = sailor.Name,
                VesselName = vessel.Name,
                IMO = vessel.IMO,
                VesselType = vessel.Type,
                Flag = vessel.Flag,
                DeadWeightTon = vessel.DeadWeightTon,
                MainEngine = vessel.MainEngine,
                Power = vessel.Power
            };
            _experienceService.Add(expericence);

            var contractResult = new OperationResult<object>(true);
            var data = new
            {
                ContractID = contract.ContractID,
                ServiceRecordID=record.ServiceRecordID,
                SailorID=record.SailorID,
                AshoreDate = record.AshoreDate.Value.ToString("yyyy-MM-dd")
            };
            contractResult.Entity = data;
            return Json(contractResult);
        }

        /// <summary>
        /// 获取两个时间的月份差。非整月的情况下最后一个月按 “天数/当月总天数” 算。
        /// </summary>
        /// <param name="fBeginDateTime">开始时间</param>
        /// <param name="fEndDateTime">结束时间</param>
        /// <returns>开始与结束时间的月份差</returns>
        private static int GetMonthSpan(DateTime fBeginDateTime, DateTime fEndDateTime)
        {
            if (fBeginDateTime > fEndDateTime)
            {
                throw new Exception("开始时间应小于或等结束时间");
            }

            // 计算整年的情况
            int prefullYear = fEndDateTime.Year - fBeginDateTime.Year;
            int fullYear = (fBeginDateTime.AddYears(prefullYear) > fEndDateTime)
                ? prefullYear - 1
                : prefullYear;

            int fullMonth = fullYear * 12;
            DateTime curBeginDate = fBeginDateTime.AddMonths(fullMonth);

            while (curBeginDate < fEndDateTime)
            {
                DateTime curEndDate = curBeginDate.AddMonths(1);

                if (curEndDate > fEndDateTime)
                {
                    double days = (fEndDateTime - curBeginDate).TotalDays;  // 最后一个月的天数差
                    double fullDaysOfMonth = (curBeginDate.AddMonths(1) - curBeginDate).TotalDays; // 最一后一个月的总天数
                    double p = days / fullDaysOfMonth;
                    return fullMonth + Convert.ToInt32(Math.Round(p)); // 整月 + (最后一个月的天数差 / 最后一个月的总天数)
                }
                else
                {
                    curBeginDate = curEndDate;
                    fullMonth++;
                }
            }

            return fullMonth;
        }
    }
}
