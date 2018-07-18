using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class CompanyService : AuthorizeBaseService<Company>
    {
        public CompanyService(DefaultDbContext cxt, ILogger<CompanyService> logger) : base(cxt, logger)
        {
        }
    }
}
