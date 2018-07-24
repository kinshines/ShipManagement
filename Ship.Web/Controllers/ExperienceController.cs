using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ship.Infrastructure.Services;
using Ship.Core.Enums;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ship.Core.Entities;

namespace Ship.Web.Controllers
{
    public class ExperienceController : Controller
    {
        readonly ExperienceService _experienceService;
        readonly SailorService _sailorService;
        public ExperienceController(ExperienceService experienceService, SailorService sailorService)
        {
            _experienceService = experienceService;
            _sailorService = sailorService;
        }

        // GET: /Experience/
        public ActionResult Index(string CompanyName, string SailorName, int? Post, int? page)
        {
            var experiences = _experienceService.GetEntities();
            if (!String.IsNullOrWhiteSpace(CompanyName))
            {
                experiences = experiences.Where(e => e.CompanyName.Contains(CompanyName));
            }
            if (!String.IsNullOrWhiteSpace(SailorName))
            {
                experiences = experiences.Where(e => e.SailorName.Contains(SailorName));
            }
            if (Post.HasValue)
            {
                experiences = experiences.Where(e => e.Post == (Post)Post);
            }
            experiences = experiences.OrderByDescending(e => e.ExperienceID);

            ViewBag.CompanyName = CompanyName;
            ViewBag.SailorName = SailorName;
            var posts = from Post p in Enum.GetValues(typeof(Post))
                        select new { ID = (int)p, Name = p.ToString() };
            ViewBag.Post = new SelectList(posts, "ID", "Name", Post);
            ViewBag.SelectedPost = Post;

            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(experiences.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Experience/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Experience experience = _experienceService.Find(id);
            if (experience == null)
            {
                return NotFound();
            }
            return View(experience);
        }

        // GET: /Experience/Create
        public ActionResult Create()
        {
            var SailorID = Request.Query["SailorID"];
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", SailorID);
            ViewBag.medium = Request.Query["medium"];
            return View();
        }

        // POST: /Experience/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Experience experience)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(experience.SailorID);
                experience.SailorName = sailor.Name;
                _experienceService.Add(experience);
                if ("Sailor".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Sailor", new { id = experience.SailorID, tab = "tab_experience" });
                }
                return RedirectToAction("Index");
            }

            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", experience.SailorID);
            return View(experience);
        }

        // GET: /Experience/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Experience experience = _experienceService.Find(id);
            if (experience == null)
            {
                return NotFound();
            }
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", experience.SailorID);
            return View(experience);
        }

        // POST: /Experience/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Experience experience)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(experience.SailorID);
                experience.SailorName = sailor.Name;
                _experienceService.Update(experience);
                return RedirectToAction("Index");
            }
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", experience.SailorID);
            return View(experience);
        }

        // GET: /Experience/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Experience/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _experienceService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}