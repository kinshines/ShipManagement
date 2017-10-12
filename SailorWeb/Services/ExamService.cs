using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public class ExamService : AuthorizeBaseService<Exam>, IExamService
    {
        public void AddItem(ExamItem item)
        {
            context.ExamItems.Add(item);
            context.SaveChanges();
        }
    }
}