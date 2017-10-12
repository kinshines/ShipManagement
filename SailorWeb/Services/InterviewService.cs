using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorWeb.Models;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public class InterviewService : AuthorizeBaseService<Interview>, IInterviewService
    {
    }
}