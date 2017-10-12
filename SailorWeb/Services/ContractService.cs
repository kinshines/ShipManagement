using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorWeb.Models;
using SailorDomain.Entities;
using SailorDomain.Services;
using SailorDomain.Infrastructure;

namespace SailorWeb.Services
{
    public class ContractService:AuthorizeBaseService<Contract>,IContractService
    {
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