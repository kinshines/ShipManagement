using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SailorDomain.Entities;

namespace SailorWeb.Services
{
    public class TraineeService:AuthorizeBaseService<Trainee>,ITraineeService
    {
        public override IQueryable<Trainee> GetEntities()
        {
            return base.GetEntities().Include(t => t.TrainingClass);
        }
    }
}