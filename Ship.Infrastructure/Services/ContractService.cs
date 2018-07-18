using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class ContractService:AuthorizeBaseService<Contract>
    {
        public ContractService(DefaultDbContext cxt, ILogger<ContractService> logger) : base(cxt, logger)
        {
        }
    }
}
