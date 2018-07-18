using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class ExamService : AuthorizeBaseService<Exam>
    {
        public ExamService(DefaultDbContext cxt, ILogger<ExamService> logger) : base(cxt, logger)
        {
        }

        public void AddItem(ExamItem item)
        {
            context.ExamItems.Add(item);
            context.SaveChanges();
        }
    }
}
