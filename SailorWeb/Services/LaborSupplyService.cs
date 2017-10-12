using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public class LaborSupplyService : AuthorizeBaseService<LaborSupply>, ILaborSupplyService
    {
        public IQueryable<LaborSupplyTake> GetTakes()
        {
            return context.LaborSupplyTakes.Include(t => t.LaborSupply).Where(t => t.LaborSupply.SysCompanyId == SysCompanyId);
        }

        public LaborSupply SupplyPut(LaborSupplyPut supplyput)
        {
            LaborSupply laborSupply = Find(supplyput.LaborSupplyID);
            laborSupply.Total = laborSupply.Total + supplyput.Amount;
            Update(laborSupply, false);
            context.LaborSupplyPuts.Add(supplyput);
            Save();
            return laborSupply;
        }

        public LaborSupply SupplyTake(LaborSupplyTake supplytake)
        {
            LaborSupply laborSupply = Find(supplytake.LaborSupplyID);
            if (laborSupply.Total < supplytake.Amount)
                return null;
            laborSupply.Total = laborSupply.Total - supplytake.Amount;
            Update(laborSupply, false);
            context.LaborSupplyTakes.Add(supplytake);
            Save();
            return laborSupply;
        }
    }
}