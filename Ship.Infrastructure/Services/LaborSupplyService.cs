using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class LaborSupplyService: AuthorizeBaseService<LaborSupply>
    {
        public LaborSupplyService(DefaultDbContext cxt, ILogger<LaborSupplyService> logger) : base(cxt, logger)
        {
        }
    }
}
