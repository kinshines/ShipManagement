using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class WageService : AuthorizeBaseService<Wage>
    {
        public WageService(DefaultDbContext cxt, ILogger<WageService> logger) : base(cxt, logger)
        {
        }
    }
}
