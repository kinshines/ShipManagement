using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SailorWeb.Models;
using SailorWeb.ViewModels;
using SailorDomain.Entities;
using SailorWeb.Services;
using Microsoft.AspNet.Identity;

namespace SailorWeb.Controllers
{
    public class HomeController : Controller
    {
        readonly INoticeService _noticeService;
        readonly ISailorService _sailorService;
        readonly ICertificateService _certificateService;
        readonly IContractService _contractService;
        readonly IShipownerService _shipownerService;
        readonly IVesselService _vesselService;
        public HomeController(INoticeService noticeService,ISailorService sailorService,ICertificateService certificateService,
            IContractService contractService, IShipownerService shipownerService, IVesselService vesselService)
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
            var userId = User.Identity.GetUserId();
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