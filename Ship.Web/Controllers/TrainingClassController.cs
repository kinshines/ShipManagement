using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ship.Core.Entities;
using Ship.Infrastructure.Services;
using X.PagedList;

namespace Ship.Web.Controllers
{
    public class TrainingClassController : Controller
    {
        readonly TrainingClassService _trainingService;
        public TrainingClassController(TrainingClassService trainingService)
        {
            _trainingService = trainingService;
        }

        // GET: /TrainingClass/
        public ActionResult Index(string Name, string Subject, DateTime? BeginBeginDate, DateTime? EndBeginDate,
            DateTime? BeginEndDate, DateTime? EndEndDate, int? page)
        {
            var query = _trainingService.GetEntities();
            if (!String.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(x => x.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(Subject))
            {
                query = query.Where(x => x.Subject.Contains(Subject));
            }
            if (BeginBeginDate.HasValue)
            {
                query = query.Where(x => x.BeginDate >= BeginBeginDate.Value);
                ViewBag.BeginBeginDate = BeginBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (EndBeginDate.HasValue)
            {
                query = query.Where(x => x.BeginDate <= EndBeginDate.Value);
                ViewBag.EndBeginDate = EndBeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (BeginEndDate.HasValue)
            {
                query = query.Where(x => x.EndDate >= BeginEndDate.Value);
                ViewBag.BeginEndDate = BeginEndDate.Value.ToString("yyyy-MM-dd");
            }
            if (EndEndDate.HasValue)
            {
                query = query.Where(x => x.EndDate <= EndEndDate.Value);
                ViewBag.EndEndDate = EndEndDate.Value.ToString("yyyy-MM-dd");
            }
            query = query.OrderByDescending(x => x.TrainingClassID);

            ViewBag.Name = Name;
            ViewBag.Subject = Subject;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: /TrainingClass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TrainingClass trainingclass = _trainingService.Find(id);
            if (trainingclass == null)
            {
                return NotFound();
            }
            return View(trainingclass);
        }

        // GET: /TrainingClass/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TrainingClass/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("TrainingClassID,Name,Subject,BeginDate,EndDate,Period,ClassHour,Form,Target,Property,ParticipantNumber,GraduateNumber,SchoolingLength,EducationDegree,Teacher,Company,Fees,Remark")] TrainingClass trainingclass)
        {
            if (ModelState.IsValid)
            {
                _trainingService.Add(trainingclass);
                return RedirectToAction("Index");
            }

            return View(trainingclass);
        }

        // GET: /TrainingClass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TrainingClass trainingclass = _trainingService.Find(id);
            if (trainingclass == null)
            {
                return NotFound();
            }
            return View(trainingclass);
        }

        // POST: /TrainingClass/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("TrainingClassID,Name,Subject,BeginDate,EndDate,Period,ClassHour,Form,Target,Property,ParticipantNumber,GraduateNumber,SchoolingLength,EducationDegree,Teacher,Company,Fees,Remark")] TrainingClass trainingclass)
        {
            if (ModelState.IsValid)
            {
                _trainingService.Update(trainingclass);
                return RedirectToAction("Index");
            }
            return View(trainingclass);
        }

        // GET: /TrainingClass/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /TrainingClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _trainingService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}