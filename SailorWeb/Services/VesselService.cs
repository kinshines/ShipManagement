using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorWeb.Models;
using SailorDomain.Entities;

namespace SailorWeb.Services
{
    public class VesselService : AuthorizeBaseService<Vessel>, IVesselService
    {
        public IQueryable<Vessel> GetVesselsByShipownerID(int? ShipownerID)
        {
            int shipownerID = ShipownerID.GetValueOrDefault();
            return GetEntities().Where(v => !ShipownerID.HasValue || v.ShipownerID == shipownerID);
        }
        public IQueryable<Vessel> GetManagedVessels()
        {
            return GetEntities().Where(v => v.VesselManageType == VesselManageType.管理船舶);
        }
    }
}