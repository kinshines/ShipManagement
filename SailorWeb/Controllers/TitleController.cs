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
    public class TitleController : Controller
    {
        readonly ITitleService _titleService;
        readonly ISailorService _sailorService;
        public TitleController(ITitleService titleService, ISailorService sailorService)
        {
            _titleService = titleService;
            _sailorService = sailorService;
        }

        // GET: /Title/
        public ActionResult Index(string Name,string SailorName, int? Post, int? page)
        {
            var titles = _titleService.GetEntities();
            if (!String.IsNullOrWhiteSpace(Name))
            {
                titles = titles.Where(t => t.Name.Contains(Name));
            }
            if(!String.IsNullOrWhiteSpace(SailorName))
            {
                titles = titles.Where(t => t.SailorName.Contains(SailorName));
            }
            if (Post.HasValue)
            {
                titles = titles.Where(t => t.Post == (Post)Post);
            }
            titles = titles.OrderByDescending(t => t.TitleID);

            ViewBag.Name = Name;
            ViewBag.SailorName = SailorName;
            var posts = from Post p in Enum.GetValues(typeof(Post))
                        select new { ID = (int)p, Name = p.ToString() };
            ViewBag.Post = new SelectList(posts, "ID", "Name", Post);
            ViewBag.SelectedPost = Post;

            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(titles.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Title/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = _titleService.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // GET: /Title/Create
        public ActionResult Create()
        {
            var SailorID = Request.QueryString["SailorID"];
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", SailorID);
            ViewBag.medium = Request.QueryString["medium"];
            return View();
        }

        // POST: /Title/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TitleID,Name,Approach,BeginDate,EndDate,Work,Major,Category,Post,Company,EngageDate,Remark,SailorID")] Title title)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(title.SailorID);
                title.SailorName = sailor.Name;
                _titleService.Add(title);
                if ("Sailor".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Sailor", new { id = title.SailorID, tab = "tab_title" });
                }
                return RedirectToAction("Index");
            }

            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", title.SailorID);
            return View(title);
        }

        // GET: /Title/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = _titleService.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", title.SailorID);
            return View(title);
        }

        // POST: /Title/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TitleID,Name,Approach,BeginDate,EndDate,Work,Major,Category,Post,Company,EngageDate,Remark,SailorID")] Title title)
        {
            if (ModelState.IsValid)
            {
                var sailor = _sailorService.Find(title.SailorID);
                title.SailorName = sailor.Name;
                _titleService.Update(title);
                return RedirectToAction("Index");
            }
            ViewBag.SailorID = new SelectList(_sailorService.GetEntities(), "SailorID", "Name", title.SailorID);
            return View(title);
        }

        // GET: /Title/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Title/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _titleService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
