using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ship.Core.Entities;
using Ship.Infrastructure.Services;

namespace Ship.Web.Controllers
{
    public class ClientCompanyController : Controller
    {
        readonly SysCompanyService _companyService;
        public ClientCompanyController(SysCompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: /SysCompany/
        public ActionResult Index()
        {
            return View(_companyService.GetEntities().ToList());
        }

        // GET: /SysCompany/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            SysCompany syscompany = _companyService.Find(id);
            if (syscompany == null)
            {
                return NotFound();
            }
            return View(syscompany);
        }

        // GET: /SysCompany/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SysCompany/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("SysCompanyId,Name,Telephone,Contacter,OpenTime,ExpireTime")] SysCompany syscompany)
        {
            if (ModelState.IsValid)
            {
                _companyService.Add(syscompany);
                return RedirectToAction("Index");
            }

            return View(syscompany);
        }

        // GET: /SysCompany/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            SysCompany syscompany = _companyService.Find(id);
            if (syscompany == null)
            {
                return NotFound();
            }
            return View(syscompany);
        }

        // POST: /SysCompany/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("SysCompanyId,Name,Telephone,Contacter,OpenTime,ExpireTime")] SysCompany syscompany)
        {
            if (ModelState.IsValid)
            {
                _companyService.Update(syscompany);
                return RedirectToAction("Index");
            }
            return View(syscompany);
        }

        // GET: /SysCompany/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            SysCompany syscompany = _companyService.Find(id);
            if (syscompany == null)
            {
                return NotFound();
            }
            return View(syscompany);
        }

        // POST: /SysCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _companyService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}