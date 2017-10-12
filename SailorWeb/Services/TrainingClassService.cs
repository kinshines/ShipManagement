using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SailorDomain.Entities;

namespace SailorWeb.Services
{
    public class TrainingClassService : AuthorizeBaseService<TrainingClass>, ITrainingClassService
    {
    }
}