using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class SailorService : AuthorizeBaseService<Sailor>
    {
        public SailorService(DefaultDbContext cxt, ILogger<SailorService> logger) : base(cxt, logger)
        {
        }
    }
}
