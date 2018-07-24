using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ship.Core.Entities;
using Ship.Core.Enums;
using Ship.Infrastructure.Services;
using X.PagedList;

namespace Ship.Web.Controllers
{
    public class VesselController : Controller
    {
        readonly VesselService _vesselService;
        readonly ShipownerService _shipownerService;
        public VesselController(VesselService vesselService, ShipownerService shipownerService)
        {
            _vesselService = vesselService;
            _shipownerService = shipownerService;
        }

        // GET: /Vessel/
        public ActionResult Index(string Name, string ShipownerName, string IMO, string ManageType, int? page)
        {
            var query = _vesselService.GetEntities();
            if (!String.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(x => x.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(ShipownerName))
            {
                query = query.Where(x => x.ShipownerName.Contains(ShipownerName));
            }
            if (!String.IsNullOrWhiteSpace(IMO))
            {
                query = query.Where(x => x.IMO.Contains(IMO));
            }
            if ("manage".Equals(ManageType))
            {
                query = query.Where(x => x.VesselManageType == VesselManageType.管理船舶);
            }
            query = query.OrderByDescending(x => x.VesselID);

            ViewBag.Name = Name;
            ViewBag.ShipownerName = ShipownerName;
            ViewBag.IMO = IMO;
            ViewBag.ManageType = ManageType;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ManagedList(string Name, string ShipownerName, string IMO, int? page)
        {
            var query = _vesselService.GetEntities();
            query = query.Where(x => x.VesselManageType == VesselManageType.管理船舶);
            if (!String.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(x => x.Name.Contains(Name));
            }
            if (!String.IsNullOrWhiteSpace(ShipownerName))
            {
                query = query.Where(x => x.ShipownerName.Contains(ShipownerName));
            }
            if (!String.IsNullOrWhiteSpace(IMO))
            {
                query = query.Where(x => x.IMO.Contains(IMO));
            }
            query = query.OrderByDescending(x => x.VesselID);

            ViewBag.Name = Name;
            ViewBag.ShipownerName = ShipownerName;
            ViewBag.IMO = IMO;

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Vessel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Vessel vessel = _vesselService.Find(id);
            if (vessel == null)
            {
                return NotFound();
            }
            return View(vessel);
        }

        // GET: /Vessel/Create
        public ActionResult Create()
        {
            var ShipownerID = Request.Query["ShipownerID"];
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", ShipownerID);
            ViewBag.medium = Request.Query["medium"];
            if (String.IsNullOrWhiteSpace(Request.Query["VesselManageType"]))
            {
                ViewBag.VesselManageType = VesselManageType.派员船舶;
            }
            else
            {
                ViewBag.VesselManageType = Request.Query["VesselManageType"];
            }

            return View();
        }

        // POST: /Vessel/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vessel vessel)
        {
            if (ModelState.IsValid)
            {
                var shipowner = _shipownerService.Find(vessel.ShipownerID);
                vessel.ShipownerName = shipowner.Name;
                _vesselService.Add(vessel);
                if ("shipowner".Equals(Request.Form["medium"]))
                {
                    return RedirectToAction("Details", "Shipowner", new { id = vessel.ShipownerID, tab = "tab_vessel" });
                }
                else if (vessel.VesselManageType == VesselManageType.管理船舶)
                {
                    return RedirectToAction("ManagedList");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", vessel.ShipownerID);
            return View(vessel);
        }

        // GET: /Vessel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Vessel vessel = _vesselService.Find(id);
            if (vessel == null)
            {
                return NotFound();
            }
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", vessel.ShipownerID);
            return View(vessel);
        }

        // POST: /Vessel/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vessel vessel)
        {
            if (ModelState.IsValid)
            {
                var shipowner = _shipownerService.Find(vessel.ShipownerID);
                vessel.ShipownerName = shipowner.Name;
                _vesselService.Update(vessel);
                if (vessel.VesselManageType == VesselManageType.管理船舶)
                {
                    return RedirectToAction("ManagedList");
                }
                return RedirectToAction("Index");
            }
            ViewBag.ShipownerID = new SelectList(_shipownerService.GetEntities(), "ShipownerID", "Name", vessel.ShipownerID);
            return View(vessel);
        }

        // GET: /Vessel/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Vessel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _vesselService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult GetVessels(int? ShipownerID)
        {
            var vesselList = _vesselService.GetVesselsByShipownerID(ShipownerID);
            var formattedData = vesselList.Select(v => new
            {
                VesselID = v.VesselID,
                Name = v.Name
            });
            return Json(formattedData);
        }

        public ActionResult GetVessel(int VesselID)
        {
            var vessel = _vesselService.Find(VesselID);
            var formattedData = new
            {
                VesselID = vessel.VesselID,
                Name = vessel.Name,
                ShipownerID = vessel.ShipownerID
            };
            return Json(formattedData);
        }
    }
}