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
using System.IO;
using SailorWeb.Services;

namespace SailorWeb.Controllers
{
    public class VesselCertificateController : Controller
    {
        readonly IVesselCertificateService _certificateService;
        readonly IVesselService _vesselService;
        readonly ICertificateTypeService _typeService;
        readonly IUploadFileService _uploadService;
        readonly INoticeService _noticeService;

        public VesselCertificateController(IVesselCertificateService certificateService, IVesselService vesselService,
            ICertificateTypeService typeService, IUploadFileService uploadService, INoticeService noticeService)
        {
            _certificateService = certificateService;
            _vesselService = vesselService;
            _typeService = typeService;
            _uploadService = uploadService;
            _noticeService = noticeService;
        }

        // GET: /VesselCertificate/
        public ActionResult Index(string VesselName, string Name, string Code, string status,
            DateTime? ExpiryBeginDate, DateTime? ExpiryEndDate, DateTime? IssueBeginDate,
            DateTime? IssueEndDate, DateTime? CheckBeginDate, DateTime? CheckEndDate, int? page)
        {
            var certificates = _certificateService.GetEntities();
            if (ExpiryBeginDate.HasValue)
            {
                certificates = certificates.Where(c => c.ExpiryDate >= ExpiryBeginDate.Value);
                ViewBag.ExpiryBeginDate = ExpiryBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (ExpiryEndDate.HasValue)
            {
                certificates = certificates.Where(c => c.ExpiryDate <= ExpiryEndDate.Value);
                ViewBag.ExpiryEndDate = ExpiryEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (IssueBeginDate.HasValue)
            {
                certificates = certificates.Where(c => c.IssueDate >= IssueBeginDate.Value);
                ViewBag.IssueBeginDate = IssueBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (IssueEndDate.HasValue)
            {
                certificates = certificates.Where(c => c.IssueDate <= IssueEndDate.Value);
                ViewBag.IssueEndDate = IssueEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (CheckBeginDate.HasValue)
            {
                certificates = certificates.Where(c => c.CheckBeginDate >= IssueBeginDate.Value);
                ViewBag.CheckBeginDate = CheckBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (CheckEndDate.HasValue)
            {
                certificates = certificates.Where(c => c.CheckEndDate <= CheckEndDate.Value);
                ViewBag.CheckEndDate = CheckEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (!String.IsNullOrWhiteSpace(VesselName))
            {
                certificates = certificates.Where(c => c.VesselName.Contains(VesselName));
            }
            if (!String.IsNullOrWhiteSpace(Name))
            {
                certificates = certificates.Where(c => c.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(Code))
            {
                certificates = certificates.Where(c => c.Code.Contains(Code));
            }
            switch (status)
            {
                case "normal":
                    certificates = certificates.Where(c => DateTime.Now < c.ExpiryDate);
                    break;
                case "check":
                    certificates = certificates.Where(c => c.CheckNoticeDate < DateTime.Now && DateTime.Now < c.CheckEndDate);
                    break;
                case "notice":
                    certificates = certificates.Where(c => c.ExpiryNoticeDate < DateTime.Now && DateTime.Now < c.ExpiryDate);
                    break;
                case "overdue":
                    certificates = certificates.Where(c => c.ExpiryDate < DateTime.Now);
                    break;
                default: break;
            }

            certificates = certificates.OrderByDescending(i => i.VesselCertificateID);

            ViewBag.VesselName = VesselName;
            ViewBag.Name = Name;
            ViewBag.Code = Code;
            ViewBag.status = status;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(certificates.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CertificateList(string Name, string Code,int VesselID, 
            DateTime? ExpiryBeginDate, DateTime? ExpiryEndDate, DateTime? IssueBeginDate,
            DateTime? IssueEndDate, DateTime? CheckBeginDate, DateTime? CheckEndDate, int? page)
        {
            var certificates = _certificateService.GetEntities().Where(c => c.VesselID == VesselID);
            if (ExpiryBeginDate.HasValue)
            {
                certificates = certificates.Where(c => c.ExpiryDate >= ExpiryBeginDate.Value);
                ViewBag.ExpiryBeginDate = ExpiryBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (ExpiryEndDate.HasValue)
            {
                certificates = certificates.Where(c => c.ExpiryDate <= ExpiryEndDate.Value);
                ViewBag.ExpiryEndDate = ExpiryEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (IssueBeginDate.HasValue)
            {
                certificates = certificates.Where(c => c.IssueDate >= IssueBeginDate.Value);
                ViewBag.IssueBeginDate = IssueBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (IssueEndDate.HasValue)
            {
                certificates = certificates.Where(c => c.IssueDate <= IssueEndDate.Value);
                ViewBag.IssueEndDate = IssueEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (CheckBeginDate.HasValue)
            {
                certificates = certificates.Where(c => c.CheckBeginDate >= IssueBeginDate.Value);
                ViewBag.CheckBeginDate = CheckBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (CheckEndDate.HasValue)
            {
                certificates = certificates.Where(c => c.CheckEndDate <= CheckEndDate.Value);
                ViewBag.CheckEndDate = CheckEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (!String.IsNullOrWhiteSpace(Name))
            {
                certificates = certificates.Where(c => c.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(Code))
            {
                certificates = certificates.Where(c => c.Code.Contains(Code));
            }

            certificates = certificates.OrderBy(i => i.Code);

            ViewBag.Name = Name;
            ViewBag.Code = Code;

            return View(certificates.ToList());
        }

        // GET: /VesselCertificate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VesselCertificate vesselcertificate = _certificateService.Find(id);
            if (vesselcertificate == null)
            {
                return HttpNotFound();
            }
            UploadFile uploadfile = _uploadService.Find(vesselcertificate.FileID);
            string contentType = "";
            if (uploadfile != null)
            {
                contentType = MimeMapping.GetMimeMapping(uploadfile.Name);
            }
            ViewBag.FileType = contentType;
            
            return View(vesselcertificate);
        }

        // GET: /VesselCertificate/Create
        public ActionResult Create()
        {
            ViewBag.medium = Request.QueryString["medium"];
            return View();
        }

        // POST: /VesselCertificate/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VesselCertificate vesselcertificate)
        {
            if (ModelState.IsValid)
            {
                var vessel = _vesselService.Find(vesselcertificate.VesselID);
                vesselcertificate.VesselName = vessel.Name;
                var certificateType = _typeService.GetVesselCertificates().FirstOrDefault(c => c.Name == vesselcertificate.Name);
                if (certificateType == null)
                {
                    certificateType = new CertificateType()
                    {
                        Name = vesselcertificate.Name,
                        CertificateCategory = CertificateCategory.船舶证书
                    };
                    certificateType = _typeService.Add(certificateType);
                }
                vesselcertificate.Name = certificateType.Name;
                vesselcertificate.CertificateTypeID = certificateType.CertificateTypeID;
                HttpPostedFileBase file = Request.Files["certificateFile"];
                vesselcertificate.FileID = _uploadService.AddFile(file);
                vesselcertificate = _certificateService.Add(vesselcertificate);

                _noticeService.AddVesselCertificateNotice(vesselcertificate);

                if ("Vessel".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Vessel", new { id = vesselcertificate.VesselID, tab = "tab_vesselcertificate" });
                }

                return RedirectToAction("CertificateList", new { VesselID = vesselcertificate.VesselID, VesselName = vesselcertificate.VesselName });
            }

            return View(vesselcertificate);
        }

        // GET: /VesselCertificate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VesselCertificate vesselcertificate = _certificateService.Find(id);
            if (vesselcertificate == null)
            {
                return HttpNotFound();
            }
            UploadFile uploadfile = _uploadService.Find(vesselcertificate.FileID);
            string contentType = "";
            if (uploadfile != null)
            {
                contentType = MimeMapping.GetMimeMapping(uploadfile.Name);
            }
            ViewBag.FileType = contentType;
            return View(vesselcertificate);
        }

        // POST: /VesselCertificate/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VesselCertificate vesselcertificate)
        {
            if (ModelState.IsValid)
            {
                var vessel = _vesselService.Find(vesselcertificate.VesselID);
                vesselcertificate.VesselName = vessel.Name;
                var certificateType = _typeService.GetVesselCertificates().FirstOrDefault(c => c.Name == vesselcertificate.Name);
                if (certificateType == null)
                {
                    certificateType = new CertificateType()
                    {
                        Name = vesselcertificate.Name,
                        CertificateCategory = CertificateCategory.船舶证书
                    };
                    certificateType = _typeService.Add(certificateType);
                }
                vesselcertificate.Name = certificateType.Name;
                vesselcertificate.CertificateTypeID = certificateType.CertificateTypeID;
                HttpPostedFileBase file = Request.Files["certificateFile"];
                vesselcertificate.FileID = _uploadService.AddFile(file, vesselcertificate.FileID);

                _certificateService.Update(vesselcertificate, false);
                _noticeService.DeleteRange(n => n.Source == NoticeSource.VesselCertificate && n.SourceID == vesselcertificate.VesselCertificateID);
                _noticeService.AddVesselCertificateNotice(vesselcertificate);
                return RedirectToAction("CertificateList", new { VesselID = vesselcertificate.VesselID, VesselName = vesselcertificate.VesselName });
            }
            return View(vesselcertificate);
        }

        // GET: /VesselCertificate/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /VesselCertificate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var certificate = _certificateService.Find(id);
            _uploadService.Delete(certificate.FileID, false);
            _certificateService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
