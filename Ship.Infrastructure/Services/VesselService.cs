using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Core.Enums;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class VesselService : AuthorizeBaseService<Vessel>
    {
        public VesselService(DefaultDbContext cxt, ILogger<VesselService> logger) : base(cxt, logger)
        {
        }
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
