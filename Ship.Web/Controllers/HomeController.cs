using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ship.Core.Enums;
using Ship.Infrastructure.Services;
using Ship.Web.Models;
using Ship.Web.ViewModels;

namespace Ship.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly NoticeService _noticeService;
        readonly SailorService _sailorService;
        readonly CertificateService _certificateService;
        readonly ContractService _contractService;
        readonly ShipownerService _shipownerService;
        readonly VesselService _vesselService;
        public HomeController(NoticeService noticeService, SailorService sailorService, CertificateService certificateService,
            ContractService contractService, ShipownerService shipownerService, VesselService vesselService)
        {
            _noticeService = noticeService;
            _sailorService = sailorService;
            _certificateService = certificateService;
            _contractService = contractService;
            _shipownerService = shipownerService;
            _vesselService = vesselService;
        }
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            var now = DateTime.Now;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var indexdata = new IndexData()
            {
                Notice = _noticeService.GetEntities().Count(n => n.SysUserId == userId && n.NoticeTime <= now && n.Active == true),
                SailorTotal = _sailorService.GetEntities().Count(),
                SailorAboard = _sailorService.GetEntities().Count(s => s.Status == SailorStatus.在船),
                SailorRest = _sailorService.GetEntities().Count(s => s.Status == SailorStatus.休假),
                SailorWaiting = _sailorService.GetEntities().Count(s => s.Status == SailorStatus.待派),
                CertificateTotal = _certificateService.GetEntities().Count(),
                CertificateNotice = _certificateService.GetEntities().Count(c => c.NoticeDate < now && now < c.ExpiryDate),
                CertificateOverdue = _certificateService.GetEntities().Count(c => c.ExpiryDate < now),
                ContractPerform = _contractService.GetEntities().Count(c => !c.Complete && c.AboardDate.HasValue),
                ContractNotice = _contractService.GetEntities().Count(c => c.NoticeDate < now && !c.Complete),
                Shipowner = _shipownerService.GetEntities().Count(),
                Vessel = _vesselService.GetEntities().Count()
            };
            return View(indexdata);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
