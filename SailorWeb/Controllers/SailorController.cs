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
using System.IO;
using PagedList;
using NLog;
using SailorWeb.Services;
using SailorDomain.Infrastructure;
using SailorWeb.Infrastructure;

namespace SailorWeb.Controllers
{
    [Authorize]
    public class SailorController : Controller
    {
        private readonly ISailorService _sailorService;
        private readonly IFamilyService _familyService;
        private readonly IUploadFileService _uploadFileService;
        private readonly ICertificateService _certificateService;
        private readonly IExperienceService _experienceService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SailorController(ISailorService sailorService, IUploadFileService uploadFileService, 
            IFamilyService familyService, ICertificateService certificateService, IExperienceService experienceService)
        {
            _sailorService = sailorService;
            _uploadFileService = uploadFileService;
            _familyService = familyService;
            _certificateService = certificateService;
            _experienceService = experienceService;
        }

        // GET: /Sailor/
        public ActionResult Index(string Name, string IdentityNo, string Mobile, string Remark, string Address, string status, int? Post, int? Degree, int? page)
        {
            var query = _sailorService.GetEntities();
            if (!String.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(s => s.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(IdentityNo))
            {
                query = query.Where(s => s.IdentityNo.Contains(IdentityNo));
            }
            if (!String.IsNullOrWhiteSpace(Remark))
            {
                query = query.Where(s => s.Remark.Contains(Remark));
            }
            if (!String.IsNullOrWhiteSpace(Mobile))
            {
                query = query.Where(s => s.Mobile.Contains(Mobile));
            }
            if (!String.IsNullOrWhiteSpace(Address))
            {
                query = query.Where(s => s.Address.Contains(Address));
            }
            if (Post.HasValue)
            {
                query = query.Where(s => s.Post==(Post)Post.Value);
            }
            if (Degree.HasValue)
            {
                query = query.Where(s => s.Degree == (CertificateDegree)Degree.Value);
            }
            switch (status)
            {
                case "aboard":
                    query = query.Where(s => s.Status == SailorStatus.在船);
                    break;
                case "waiting":
                    query = query.Where(s => s.Status == SailorStatus.待派);
                    break;
                case "training":
                    query = query.Where(s => s.Status == SailorStatus.培训);
                    break;
                case "rest":
                    query = query.Where(s => s.Status == SailorStatus.休假);
                    break;
                case "notrace":
                    query = query.Where(s => s.Status == SailorStatus.不跟踪);
                    break;
                default: break;
            }
            ViewBag.Name = Name;
            ViewBag.IdentityNo = IdentityNo;
            ViewBag.Remark = Remark;
            ViewBag.Mobile = Mobile;
            ViewBag.Address = Address;
            ViewBag.status = status;
            var posts = from Post p in Enum.GetValues(typeof(Post))
                             select new { ID = (int)p, Name = p.ToString() };
            ViewBag.Post = new SelectList(posts, "ID", "Name", Post);
            ViewBag.SelectedPost = Post;
            var degrees = from CertificateDegree d in Enum.GetValues(typeof(CertificateDegree))
                          select new { ID = (int)d, Name = d.ToString() };
            ViewBag.Degree = new SelectList(degrees, "ID", "Name", Degree);
            ViewBag.SelectedDegree = Degree;
            query = query.OrderByDescending(s => s.SailorID);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Sailor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sailor sailor = _sailorService.Find(id);
            if (sailor == null)
            {
                return HttpNotFound();
            }
            return View(sailor);
        }

        // GET: /Sailor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Sailor/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sailor sailor)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["uploadFile"];
                sailor.FileID = _uploadFileService.AddFile(file);
                sailor.Status = SailorStatus.待派;
                _sailorService.Add(sailor);
                if (!String.IsNullOrWhiteSpace(sailor.HomeContacter))
                {
                    var family = new Family()
                    {
                        SailorID=sailor.SailorID,
                        SailorName=sailor.Name,
                        Name=sailor.HomeContacter,
                        Address=sailor.Address,
                        Telephone=sailor.HomeTel,
                        Beneficiary=true
                    };
                    _familyService.Add(family);
                }
                return RedirectToAction("Index");
            }

            return View(sailor);
        }

        // GET: /Sailor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sailor sailor = _sailorService.Find(id);
            if (sailor == null)
            {
                return HttpNotFound();
            }

            return View(sailor);
        }

        // POST: /Sailor/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sailor sailor)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["uploadFile"];
                sailor.FileID = _uploadFileService.AddFile(file, sailor.FileID);
                _sailorService.Update(sailor);
                return RedirectToAction("Index");
            }

            return View(sailor);
        }

        // GET: /Sailor/Delete/5
        public PartialViewResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Sailor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sailor = _sailorService.Find(id);
            _uploadFileService.Delete(sailor.FileID);
            _sailorService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int SailorID, byte SailorStatus)
        {
            try
            {
                var sailor = _sailorService.Find(SailorID);
                sailor.Status = (SailorStatus)SailorStatus;
                _sailorService.Update(sailor);
                var data = new
                {
                    Id = sailor.SailorID,
                    Status = sailor.Status.ToString()
                };
                return Json(data);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return Json("error");
            }
        }

        public ActionResult GetSailor(int id)
        {
            var sailor = _sailorService.Find(id);
            var formattedData = new
            {
                SailorID = sailor.SailorID,
                IdentityNo = sailor.IdentityNo,
                Name = sailor.Name,
                Post = sailor.Post
            };
            return Json(formattedData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Export(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sailor sailor = _sailorService.Find(id);
            if (sailor == null)
            {
                return HttpNotFound();
            }
            string companyId = sailor.SysCompanyId.ToString();
            Dictionary<String, String> dic = new Dictionary<string, string>();
            dic.Add("EnglishName", sailor.EnglishName);
            dic.Add("Name", sailor.Name);
            dic.Add("Post", EnglishConvert.EnglishPost(sailor.Post));
            dic.Add("Post2", EnglishConvert.EnglishPost(sailor.Post));
            dic.Add("Birthday", sailor.Birthday.ToString("dd-MM-yyyy"));
            dic.Add("Birthplace", sailor.Birthplace);
            dic.Add("Marital", EnglishConvert.EnglishMarital(sailor.Marital));
            if(sailor.Height.HasValue){
                dic.Add("Height", sailor.Height + "cm");
            }
            if (sailor.Weight.HasValue)
            {
                dic.Add("Weight", sailor.Weight + "kg");
            }
            dic.Add("ShoeSize", sailor.ShoeSize);
            dic.Add("HomeContacter", sailor.HomeContacter);
            dic.Add("GraduateCollege", sailor.GraduateCollege);
            if (sailor.EnrollmentDate.HasValue)
            {
                dic.Add("EnrollmentYear", sailor.EnrollmentDate.Value.Year.ToString());
            }
            if (sailor.GraduateDate.HasValue)
            {
                dic.Add("GraduateYear", sailor.GraduateDate.Value.Year.ToString());
            }
            dic.Add("HomeTel", sailor.HomeTel);
            dic.Add("Address", sailor.Address);
            dic.Add("Mobile", sailor.Mobile);

            //护照
            var Passport = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 2);
            dic = _certificateService.AddCertificateDic("Passport", dic, Passport);
            //适任证书
            var RankLicense = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID >= 4 && c.CertificateTypeID <= 23);
            dic = _certificateService.AddCertificateDic("RankLicense", dic, RankLicense);
            //服务薄
            var RecordBook = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 3);
            dic = _certificateService.AddCertificateDic("RecordBook", dic, RecordBook);
            //海员证
            var SeafarerPassport = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 1);
            dic = _certificateService.AddCertificateDic("SeafarerPassport", dic, SeafarerPassport);
            //精通急救培训
            var MedicalFirstAid = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 29);
            dic = _certificateService.AddCertificateDic("MedicalFirstAid", dic, MedicalFirstAid);
            //船上医护培训
            var MedicalCareOnBoard = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 30);
            dic = _certificateService.AddCertificateDic("MedicalCareOnBoard", dic, MedicalCareOnBoard);
            //基本安全培训
            var BasicSafetyTraining = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 25);
            dic = _certificateService.AddCertificateDic("BasicSafetyTraining", dic, BasicSafetyTraining);
            //高级消防培训
            var AdvanceFireFighting = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 28);
            dic = _certificateService.AddCertificateDic("AdvanceFireFighting", dic, AdvanceFireFighting);
            //精通救生艇筏和救助艇培训
            var ProficiencySurvivalCraft = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 27);
            dic = _certificateService.AddCertificateDic("ProficiencySurvivalCraft", dic, ProficiencySurvivalCraft);
            //GMDSS证书（G证）
            var GMDSS = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 24);
            dic = _certificateService.AddCertificateDic("GMDSS", dic, GMDSS);
            //船舶保安员专业证书
            var ShipSecurityOfficer = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 33);
            dic = _certificateService.AddCertificateDic("ShipSecurityOfficer", dic, ShipSecurityOfficer);
            //保安意识培训
            var SecurityAwarenessTraining = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 33);
            dic = _certificateService.AddCertificateDic("SecurityAwarenessTraining", dic, SecurityAwarenessTraining);
            //负有指定保安职责船员培训
            var SeafarerSecurityDuty = _certificateService.Find(c => c.SailorID == sailor.SailorID && c.CertificateTypeID == 32);
            dic = _certificateService.AddCertificateDic("SeafarerSecurityDuty", dic, SeafarerSecurityDuty);

            var experiences = _experienceService.GetEntities().Where(e => e.SailorID == sailor.SailorID).OrderByDescending(e => e.EndTime).ToList();
            for (int i = 0; i < Math.Min(experiences.Count(), 5); i++)
            {
                if (experiences[i].Post.HasValue)
                {
                    dic.Add("ExperiencePost" + i , EnglishConvert.EnglishPost(experiences[i].Post.Value));
                }                    
                dic.Add("ExperienceVesselName" + i , experiences[i].VesselName);
                dic.Add("ExperienceIMO" + i , experiences[i].IMO);
                dic.Add("ExperienceCompany" + i , experiences[i].CompanyName);
                dic.Add("ExperienceBeginTime" + i , EnglishConvert.EnglishDate(experiences[i].BeginTime));
                dic.Add("ExperienceEndTime" + i, EnglishConvert.EnglishDate(experiences[i].EndTime));
                if (experiences[i].DurationMonth.HasValue)
                {
                    dic.Add("ExperienceDurationMonth" + i, experiences[i].DurationMonth + "M");
                }
                if (experiences[i].VesselType.HasValue)
                {
                    dic.Add("ExperienceVesselType" + i,
                        EnglishConvert.EnglishVesselType(experiences[i].VesselType.Value));
                }
                dic.Add("ExperienceFlag" + i , experiences[i].Flag);
                if (experiences[i].DeadWeightTon.HasValue)
                {
                    dic.Add("ExperienceDeadWeightTon" + i, experiences[i].DeadWeightTon.ToString());
                }
                dic.Add("ExperienceMainEngine" + i, experiences[i].MainEngine);
                dic.Add("ExperiencePower" + i, experiences[i].Power);
            }

            return ExportContract(dic, sailor.SysCompanyId.ToString(), sailor.SailorID, sailor.Name);
        }

        private FileResult ExportContract(Dictionary<String, String> textDict, string companyId, int sailorId, string sailorName)
        {
            string sourcepath = HttpContext.Server.MapPath("~/Files/Resume/Template/" + companyId + ".docx");
            if (!System.IO.File.Exists(sourcepath))
                sourcepath = HttpContext.Server.MapPath("~/Files/Resume/Template/0.docx");
            string targetPath = HttpContext.Server.MapPath("~/Files/Resume/Export/" + sailorId + "_简历.docx");
            System.IO.File.Copy(sourcepath, targetPath, true);
            var contentControlManager = new ContentControlManager();
            contentControlManager.OpenDocuemnt(targetPath);
            contentControlManager.UpdateText(textDict);
            var stream = GetPhoto(sailorId);
            if (stream != null)
            {
                var imageDict = new Dictionary<string, MemoryStream>
                                {
                                    {"Photo", stream},
                                };
                contentControlManager.UpdateImage(imageDict);
            }
            contentControlManager.CloseDocument();
            var name = Path.GetFileName(targetPath);
            string contentType = MimeMapping.GetMimeMapping(name);
            return File(targetPath, contentType, Url.Encode(sailorName + "_简历.docx"));
        }

        private MemoryStream GetPhoto(int sailorId)
        {
            var sailor = _sailorService.Find(sailorId);
            if (sailor == null)
                return null;
            var photoFile = _uploadFileService.Find(sailor.FileID);
            if (photoFile == null)
                return null;
            string diskPath = HttpContext.Server.MapPath(photoFile.Path);
            if (!System.IO.File.Exists(diskPath))
            {
                return null;
            }

            byte[] original = System.IO.File.ReadAllBytes(diskPath);
            var stream = new MemoryStream(original);
            return stream;
        }
    }
}
