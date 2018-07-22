using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class TraineeService : AuthorizeBaseService<Trainee>
    {
        public TraineeService(DefaultDbContext cxt, ILogger<TraineeService> logger) : base(cxt, logger)
        {
        }
        public override IQueryable<Trainee> GetEntities()
        {
            return base.GetEntities().Include(t => t.TrainingClass);
        }
    }
}
