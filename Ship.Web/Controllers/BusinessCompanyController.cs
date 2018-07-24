using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ship.Core.Entities;
using Ship.Infrastructure.Services;
using X.PagedList;

namespace Ship.Web.Controllers
{
    public class BusinessCompanyController : Controller
    {
        readonly CompanyService _companyService;
        public BusinessCompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: /Company/
        public ActionResult Index(string Name, string Contacter, string ContactTel, int? page)
        {
            var query = _companyService.GetEntities();
            if (!String.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(x => x.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(Contacter))
            {
                query = query.Where(x => x.Contacter.Contains(Contacter));
            }
            if (!String.IsNullOrWhiteSpace(ContactTel))
            {
                query = query.Where(x => x.ContactTel.Contains(ContactTel));
            }
            query = query.OrderByDescending(x => x.CompanyID);

            ViewBag.Name = Name;
            ViewBag.Contacter = Contacter;
            ViewBag.ContactTel = ContactTel;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Company company = _companyService.Find(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // GET: /Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Company/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyService.Add(company);
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: /Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Company company = _companyService.Find(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: /Company/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyService.Update(company);
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: /Company/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _companyService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult GetCompanyName(string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                query = query.Trim();
                var list = _companyService.GetEntities().Where(c => c.Name.Contains(query));
                var formattedData = list.Select(c => new
                {
                    CompanyID = c.CompanyID,
                    Name = c.Name
                });
                return Json(formattedData);
            }
            return NotFound();
        }
    }
}