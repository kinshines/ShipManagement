using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class TrainingClassService : AuthorizeBaseService<TrainingClass>
    {
        public TrainingClassService(DefaultDbContext cxt, ILogger<TrainingClassService> logger) : base(cxt, logger)
        {
        }
    }
}
