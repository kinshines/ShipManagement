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
    public class LaborSupplyController : Controller
    {
        readonly ILaborSupplyService _laborSupplyService;
        public LaborSupplyController(ILaborSupplyService laborSupplyService)
        {
            _laborSupplyService = laborSupplyService;
        }

        // GET: /LaborSupply/
        public ActionResult Index(string Name, string Specification, int? page)
        {
            var query = _laborSupplyService.GetEntities();
            if (!String.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(x => x.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(Specification))
            {
                query = query.Where(x => x.Specification.Contains(Specification));
            }
            query = query.OrderByDescending(x => x.LaborSupplyID);

            ViewBag.Name = Name;
            ViewBag.Contacter = Specification;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult TakeList(string Name, DateTime? BeginDate, DateTime? EndDate, int? page)
        {
            var takes = _laborSupplyService.GetTakes();
            if (BeginDate.HasValue)
            {
                takes = takes.Where(t => t.TakeDate >= BeginDate.Value);
            }
            if (EndDate.HasValue)
            {
                takes = takes.Where(t => t.TakeDate <= EndDate.Value);
            }
            if (!String.IsNullOrWhiteSpace(Name))
            {
                takes = takes.Where(i => i.LaborSupply.Name.Contains(Name));
            }
            takes = takes.OrderByDescending(i => i.LaborSupplyTakeID);

            if (BeginDate.HasValue)
            {
                ViewBag.BeginDate = BeginDate.Value.ToString("yyyy-MM-dd");
            }
            if (EndDate.HasValue)
            {
                ViewBag.EndDate = EndDate.Value.ToString("yyyy-MM-dd");
            }
            ViewBag.Name = Name;
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(takes.ToPagedList(pageNumber, pageSize));
        }

        // GET: /LaborSupply/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaborSupply laborsupply = _laborSupplyService.Find(id);
            if (laborsupply == null)
            {
                return HttpNotFound();
            }
            return View(laborsupply);
        }

        // GET: /LaborSupply/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /LaborSupply/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="LaborSupplyID,Name,Specification,Total,Baseline,Remark")] LaborSupply laborsupply)
        {
            if (ModelState.IsValid)
            {
                _laborSupplyService.Add(laborsupply);
                return RedirectToAction("Index");
            }

            return View(laborsupply);
        }

        // GET: /LaborSupply/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaborSupply laborsupply = _laborSupplyService.Find(id);
            if (laborsupply == null)
            {
                return HttpNotFound();
            }
            return View(laborsupply);
        }

        // POST: /LaborSupply/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="LaborSupplyID,Name,Specification,Total,Baseline,Remark")] LaborSupply laborsupply)
        {
            if (ModelState.IsValid)
            {
                _laborSupplyService.Update(laborsupply);
                return RedirectToAction("Index");
            }
            return View(laborsupply);
        }

        // GET: /LaborSupply/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /LaborSupply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _laborSupplyService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupplyPut(LaborSupplyPut supplyput)
        {
            if (ModelState.IsValid)
            {
                LaborSupply laborSupply = _laborSupplyService.SupplyPut(supplyput);
                var formattedData = new
                {
                    Id=laborSupply.LaborSupplyID,
                    Name = laborSupply.Name,
                    Specification = laborSupply.Specification,
                    Total = laborSupply.Total,
                    Baseline = laborSupply.Baseline
                };
                return Json(formattedData);
            }
            return Json("error", JsonRequestBehavior.AllowGet);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupplyTake(LaborSupplyTake supplytake)
        {
            if (ModelState.IsValid)
            {
                LaborSupply laborSupply = _laborSupplyService.SupplyTake(supplytake);
                if (laborSupply == null)
                    return Json("error", JsonRequestBehavior.AllowGet);
                var formattedData = new
                {
                    Id = laborSupply.LaborSupplyID,
                    Name = laborSupply.Name,
                    Specification = laborSupply.Specification,
                    Total = laborSupply.Total,
                    Baseline = laborSupply.Baseline
                };
                return Json(formattedData);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
    }
}
