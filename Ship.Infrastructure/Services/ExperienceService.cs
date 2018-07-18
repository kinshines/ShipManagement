using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class ExperienceService : AuthorizeBaseService<Experience>
    {
        public ExperienceService(DefaultDbContext cxt, ILogger<ExperienceService> logger) : base(cxt, logger)
        {
        }
    }
}
