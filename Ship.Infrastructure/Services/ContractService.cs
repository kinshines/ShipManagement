using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class ContractService:AuthorizeBaseService<Contract>
    {
        public ContractService(DefaultDbContext cxt, ILogger<ContractService> logger) : base(cxt, logger)
        {
        }

        public Contract SignedContract(int sailorID, Contract contract)
        {
            var query = GetEntities().Where(c => c.SailorID == sailorID);
            query = query.Where(c => (c.AboardDate <= contract.AboardDate && contract.AboardDate <= c.AshoreDate) ||
                (c.AboardDate <= contract.AshoreDate && contract.AshoreDate <= c.AshoreDate) ||
                (contract.AboardDate <= c.AboardDate && c.AshoreDate <= contract.AshoreDate));
            return query.FirstOrDefault();
        }
    }
}
