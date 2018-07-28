using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class LaborSupplyService: AuthorizeBaseService<LaborSupply>
    {
        public LaborSupplyService(DefaultDbContext cxt, ILogger<LaborSupplyService> logger) : base(cxt, logger)
        {
        }
        public IQueryable<LaborSupplyTake> GetTakes()
        {
            return context.LaborSupplyTake.Include(t => t.LaborSupply).Where(t => t.LaborSupply.SysCompanyId == SysCompanyId);
        }

        public LaborSupply SupplyPut(LaborSupplyPut supplyput)
        {
            LaborSupply laborSupply = Find(supplyput.LaborSupplyID);
            laborSupply.Total = laborSupply.Total + supplyput.Amount;
            Update(laborSupply, false);
            context.LaborSupplyPut.Add(supplyput);
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
            context.LaborSupplyTake.Add(supplytake);
            Save();
            return laborSupply;
        }
    }
}
