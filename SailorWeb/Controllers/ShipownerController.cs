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
    public class ShipownerController : Controller
    {
        readonly IShipownerService _shipownerService;
        public ShipownerController(IShipownerService shipownerService)
        {
            _shipownerService = shipownerService;
        }

        // GET: /Shipowner/
        public ActionResult Index(string Name, string Contacter, string Telephone, int? page)
        {
            var query = _shipownerService.GetEntities();
            if (!String.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(x => x.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(Contacter))
            {
                query = query.Where(x => x.Contacter.Contains(Contacter));
            }
            if (!String.IsNullOrWhiteSpace(Telephone))
            {
                query = query.Where(x => x.Telephone.Contains(Telephone));
            }
            query = query.OrderByDescending(x => x.ShipownerID);

            ViewBag.Name = Name;
            ViewBag.Contacter = Contacter;
            ViewBag.Telephone = Telephone;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Shipowner/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipowner shipowner = _shipownerService.Find(id);
            if (shipowner == null)
            {
                return HttpNotFound();
            }
            return View(shipowner);
        }

        // GET: /Shipowner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Shipowner/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ShipownerID,Name,Contacter,Address,Telephone,Fax,Email,Website,Representative,PostalCode")] Shipowner shipowner)
        {
            if (ModelState.IsValid)
            {
                _shipownerService.Add(shipowner);
                return RedirectToAction("Index");
            }

            return View(shipowner);
        }

        // GET: /Shipowner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipowner shipowner = _shipownerService.Find(id);
            if (shipowner == null)
            {
                return HttpNotFound();
            }
            return View(shipowner);
        }

        // POST: /Shipowner/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ShipownerID,Name,Contacter,Address,Telephone,Fax,Email,Website,Representative,PostalCode")] Shipowner shipowner)
        {
            if (ModelState.IsValid)
            {
                _shipownerService.Update(shipowner);
                return RedirectToAction("Index");
            }
            return View(shipowner);
        }

        // GET: /Shipowner/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Shipowner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            _shipownerService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult GetShipowners()
        {
            var shipownerList = _shipownerService.GetEntities();
            var formattedData = shipownerList.Select(s => new
            {
                ShipownerID = s.ShipownerID,
                Name = s.Name
            });
            return Json(formattedData, JsonRequestBehavior.AllowGet);
        }
    }
}
