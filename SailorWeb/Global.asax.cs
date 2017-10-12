using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading.Tasks;
using System.Data.Entity;
using SailorDomain.Entities;
using SailorWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using NLog;

namespace SailorWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacMvc.Initialize();

            Task.Factory.StartNew(() =>
            {
                Database.SetInitializer(new CreateDatabaseIfNotExists<DefaultDbContext>());
            });
        }
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom.Equals("NoticeMessage"))
            {
                var userId = context.User.Identity.GetUserId();
                var val = context.Cache[custom + userId];
                if (val == null)
                    return string.Empty;
                else
                    return (string)val;
            }
            return base.GetVaryByCustomString(context, custom);
        }
    }
}
