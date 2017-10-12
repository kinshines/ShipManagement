using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorDomain.Entities;

namespace SailorWeb.Services
{
    public class CompanyService : AuthorizeBaseService<Company>, ICompanyService
    {
    }
}