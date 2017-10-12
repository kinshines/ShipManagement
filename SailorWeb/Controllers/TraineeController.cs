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

namespace SailorWeb.Controllers
{
    [Authorize]
    public class TraineeController : Controller
    {
        readonly ITrainingClassService _trainingService;
        readonly ITraineeService _traineeService;
        readonly ISailorService _sailorService;
        public TraineeController(ITrainingClassService trainingService, ITraineeService traineeService, ISailorService sailorService)
        {
            _trainingService = trainingService;
            _traineeService = traineeService;
            _sailorService = sailorService;
        }

        // GET: /Trainee/
        public ActionResult Index(string SailorName, string TrainingClassName, string Subject, string status,
            DateTime? BeginBeginDate, DateTime? EndBeginDate,
            DateTime? BeginEndDate, DateTime? EndEndDate, int? page)
        {
            var query = _traineeService.GetEntities();
            if (!String.IsNullOrWhiteSpace(SailorName))
            {
                query = query.Where(x => x.SailorName.Contains(SailorName));
            }
            if (!String.IsNullOrWhiteSpace(TrainingClassName))
            {
                query = query.Where(x => x.TrainingClassName.Contains(TrainingClassName));
            }
            if (!String.IsNullOrWhiteSpace(Subject))
            {
                query = query.Where(x => x.TrainingClass.Subject.Contains(Subject));
            }
            if (BeginBeginDate.HasValue)
            {
                query = query.Where(x => x.TrainingClass.BeginDate >= BeginBeginDate.Value);
                ViewBag.BeginBeginDate = BeginBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (EndBeginDate.HasValue)
            {
                query = query.Where(x => x.TrainingClass.BeginDate <= EndBeginDate.Value);
                ViewBag.EndBeginDate = EndBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (BeginEndDate.HasValue)
            {
                query = query.Where(x => x.TrainingClass.EndDate >= BeginEndDate.Value);
                ViewBag.BeginEndDate = BeginEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (EndEndDate.HasValue)
            {
                query = query.Where(x => x.TrainingClass.EndDate <= EndEndDate.Value);
                ViewBag.EndEndDate = EndEndDate.Value.ToString("yyyy-MM-dd");
            }
            query = query.OrderByDescending(x => x.TraineeID);

            ViewBag.SailorName = SailorName;
            ViewBag.TrainingClassName = TrainingClassName;
            ViewBag.Subject = Subject;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Trainee/Create
        public ActionResult Create()
        {
            var SailorID = Request.QueryString["SailorID"];
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", SailorID);
            ViewBag.medium = Request.QueryString["medium"];
            ViewBag.TrainingClassID = new SelectList(_trainingService.GetEntities(), "TrainingClassID", "Name");
            return View();
        }

        // POST: /Trainee/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TraineeID,Expense,ExpenseClaim,CertificateNo,Qualified,Remark,TrainingClassID,SailorID")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(trainee.SailorID);
                var trainingclass = _trainingService.Find(trainee.TrainingClassID);
                trainee.SailorName = sailor.Name;
                trainee.TrainingClassName = trainingclass.Name;
                trainee = _traineeService.Add(trainee);

                if ("Sailor".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Sailor", new { id = trainee.SailorID, tab = "tab_trainee" });
                }
                else if ("BeginTraining".Equals(Request.Form["medium"]))
                {
                    sailor.Status = SailorStatus.培训;
                    sailor.TraineeID = trainee.TraineeID;
                    _sailorService.Update(sailor);
                    return RedirectToAction("Index", "Sailor");
                }

                return RedirectToAction("Index");
            }

            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", trainee.SailorID);
            ViewBag.TrainingClassID = new SelectList(_trainingService.GetEntities(), "TrainingClassID", "Name", trainee.TrainingClassID);
            return View(trainee);
        }

        // GET: /Trainee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = _traineeService.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            ViewBag.medium = Request.QueryString["medium"];
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", trainee.SailorID);
            ViewBag.TrainingClassID = new SelectList(_trainingService.GetEntities(), "TrainingClassID", "Name", trainee.TrainingClassID);
            return View(trainee);
        }

        // POST: /Trainee/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TraineeID,Expense,ExpenseClaim,CertificateNo,Qualified,Remark,TrainingClassID,SailorID")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(trainee.SailorID);
                var trainingclass = _trainingService.Find(trainee.TrainingClassID);
                trainee.SailorName = sailor.Name;
                trainee.TrainingClassName = trainingclass.Name;

                _traineeService.Update(trainee);
                if ("EndTraining".Equals(Request.Form["medium"]))
                {
                    sailor.Status = SailorStatus.待派;
                    sailor.TraineeID = null;
                    _sailorService.Update(sailor);
                    return RedirectToAction("Index", "Sailor");
                }
                return RedirectToAction("Index");
            }
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", trainee.SailorID);
            ViewBag.TrainingClassID = new SelectList(_trainingService.GetEntities(), "TrainingClassID", "Name", trainee.TrainingClassID);
            return View(trainee);
        }

        // GET: /Trainee/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Trainee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var trainees = _sailorService.GetEntities().Where(s => s.TraineeID == id);
            foreach (var sailor in trainees)
            {
                sailor.Status = SailorStatus.待派;
                sailor.TraineeID = null;
                _sailorService.Update(sailor);
            }
            _traineeService.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: /Trainee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = _traineeService.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Details", trainee);
            }
            return View(trainee);
        }
    }
}
