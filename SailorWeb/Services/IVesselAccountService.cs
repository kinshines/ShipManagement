using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorWeb.Models;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public interface IVesselAccountService : IBaseService<VesselAccount>
    {
        IQueryable<VesselAccount> GetCosts();
        IQueryable<VesselAccount> GetDeposits();
        IQueryable<VesselBalance> BalanceList();
        VesselAccount AddCost(VesselAccount vesselcost, bool isSave = true);
        VesselCostPayment AddPayment(VesselCostPayment payment, bool payoff);
        VesselAccount AddDeposit(VesselAccount vesseldeposit, bool isSave = true);
        bool DeleteAccount(int? accountID);
        bool DeletePayment(int? paymentID);
    }
}
