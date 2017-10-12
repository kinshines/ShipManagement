using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorWeb.Models;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public interface IVesselService : IBaseService<Vessel>
    {
        IQueryable<Vessel> GetVesselsByShipownerID(int? ShipownerID);
        IQueryable<Vessel> GetManagedVessels();
    }
}
