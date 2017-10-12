using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SailorDomain.Entities;
using SailorWeb.Services;

namespace SailorWeb.Controllers
{
    public class WageController : Controller
    {
        private readonly IWageService _wageService;
        private readonly IContractService _contractService;

        public WageController(IWageService wageService,IContractService contractService)
        {
            _wageService = wageService;
            _contractService = contractService;
        }

        //
        // GET: /Wage/
        public ActionResult Index(int? page)
        {
            var lastDate = DateTime.Now.AddMonths(-1);
            var lastMonth = new DateTime(lastDate.Year, lastDate.Month, 1);
            var startDate = new DateTime(2014, 1, 1);
            var dateList=new List<DateTime>();
            while (startDate<=lastMonth)
            {
                dateList.Add(lastMonth);
                lastMonth = lastMonth.AddMonths(-1);
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(dateList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MonthlyList(int year, int month,string sailorName,int? page)
        {
            var query = _wageService.GetEntities().Where(x => x.Year == year && x.Month == month);
            if (!String.IsNullOrWhiteSpace(sailorName))
            {
                query = query.Where(x => x.SailorName.Contains(sailorName));
            }
            query = query.OrderBy(x => x.SailorID);
            ViewBag.sailorName = sailorName;
            ViewBag.year = year;
            ViewBag.month = month;
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Calculate(int year, int month)
        {
            _wageService.DeleteRange(x => x.Year == year && x.Month == month);
            DateTime beginTime = new DateTime(year, month, 1);
            DateTime endTime = beginTime.AddMonths(1).AddDays(-1);
            int monthlyDays = DateTime.DaysInMonth(year, month);
            var contracts = _contractService.GetEntities()
                .Where(x => (beginTime <= x.AboardDate && x.AboardDate <= endTime) ||
                            (beginTime <= x.AshoreDate && x.AshoreDate <= endTime) ||
                            (x.AboardDate <= beginTime && endTime <= x.AshoreDate)).ToList();
            foreach (var contract in contracts)
            {
                var wage = new Wage()
                {
                    Year = year,
                    Month = month,
                    MonthlyDays = monthlyDays,
                    StandardWage = contract.HomeWage,
                    ContractID = contract.ContractID,
                    SailorName = contract.SailorName,
                    SailorID = contract.SailorID,
                    BeginDate = contract.AboardDate.Value>beginTime?contract.AboardDate.Value:beginTime,
                    EndDate = contract.AshoreDate.Value>endTime?endTime:contract.AshoreDate.Value
                };
                _wageService.Add(wage);
            }
            return RedirectToAction("MonthlyList", new {year = year, month = month});
        }
	}
}