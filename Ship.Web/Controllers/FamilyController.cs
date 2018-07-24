using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ship.Core.Entities;
using Ship.Infrastructure.Services;
using X.PagedList;

namespace Ship.Web.Controllers
{
    public class FamilyController : Controller
    {
        readonly FamilyService _familyService;
        readonly SailorService _sailorService;

        public FamilyController(FamilyService familyService, SailorService sailorService)
        {
            _familyService = familyService;
            _sailorService = sailorService;
        }

        // GET: /Family/
        public ActionResult Index(string Name, string SailorName, int? page)
        {
            var families = _familyService.GetEntities();
            if (!String.IsNullOrWhiteSpace(Name))
            {
                families = families.Where(f => f.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(SailorName))
            {
                families = families.Where(f => f.SailorName.Contains(SailorName));
            }
            families = families.OrderByDescending(f => f.FamilyID);

            ViewBag.Name = Name;
            ViewBag.SailorName = SailorName;

            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(families.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Family/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Family family = _familyService.Find(id);
            if (family == null)
            {
                return NotFound();
            }
            return View(family);
        }

        // GET: /Family/Create
        public ActionResult Create()
        {
            var SailorID = Request.Query["SailorID"];
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", SailorID);
            ViewBag.medium = Request.Query["medium"];
            return View();
        }

        // POST: /Family/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FamilyID,Name,Relationship,Beneficiary,Telephone,Address,Remark,SailorID")] Family family)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(family.SailorID);
                family.SailorName = sailor.Name;
                _familyService.Add(family);
                if ("Sailor".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Sailor", new { id = family.SailorID, tab = "tab_family" });
                }

                return RedirectToAction("Index");
            }

            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", family.SailorID);
            return View(family);
        }

        // GET: /Family/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Family family = _familyService.Find(id);
            if (family == null)
            {
                return NotFound();
            }
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", family.SailorID);
            return View(family);
        }

        // POST: /Family/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("FamilyID,Name,Relationship,Beneficiary,Telephone,Address,Remark,SailorID")] Family family)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(family.SailorID);
                family.SailorName = sailor.Name;
                _familyService.Update(family);
                return RedirectToAction("Index");
            }
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", family.SailorID);
            return View(family);
        }

        // GET: /Family/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Family/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _familyService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}