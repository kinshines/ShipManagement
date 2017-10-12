using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public interface ILaborSupplyService : IBaseService<LaborSupply>
    {
        IQueryable<LaborSupplyTake> GetTakes();
        LaborSupply SupplyPut(LaborSupplyPut supplyput);
        LaborSupply SupplyTake(LaborSupplyTake supplytake);
    }
}
