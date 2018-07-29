using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ship.Web.Components
{
    public class Menu: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
