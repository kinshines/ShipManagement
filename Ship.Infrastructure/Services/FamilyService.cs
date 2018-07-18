using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class FamilyService:AuthorizeBaseService<Family>
    {
        public FamilyService(DefaultDbContext cxt, ILogger<FamilyService> logger) : base(cxt, logger)
        {
        }
    }
}
