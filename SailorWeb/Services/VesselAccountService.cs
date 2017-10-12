using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorWeb.Models;
using System.Data.Entity;
using SailorDomain.Entities;

namespace SailorWeb.Services
{
    public class VesselAccountService : AuthorizeBaseService<VesselAccount>, IVesselAccountService
    {
        public IQueryable<VesselAccount> GetCosts()
        {
            return GetEntities().Where(a => a.Side == VesselAccountSide.Cost);
        }

        public IQueryable<VesselAccount> GetDeposits()
        {
            return GetEntities().Where(a => a.Side == VesselAccountSide.Deposit);
        }

        private VesselBalance GetBalance(int vesselID)
        {
            return context.VesselBalances.Find(vesselID);
        }
        public IQueryable<VesselBalance> BalanceList()
        {
            return context.VesselBalances.Include(b => b.Shipowner).Include(b => b.Vessel).Where(b => b.Shipowner.SysCompanyId == SysCompanyId);
        }

        public VesselAccount AddCost(VesselAccount vesselcost, bool isSave = true)
        {
            vesselcost.Debt = vesselcost.Cost;
            vesselcost.USDebt = vesselcost.USCost;
            if (vesselcost.Cost == 0)
                vesselcost.Cost = null;
            if (vesselcost.USCost == 0)
                vesselcost.USCost = null;
            vesselcost.Payment = null;
            vesselcost.USPayment = null;

            var balance = GetBalance(vesselcost.VesselID);
            if (balance == null)
            {
                balance = new VesselBalance()
                {
                    VesselID = vesselcost.VesselID,
                    ShipownerID = vesselcost.ShipownerID,
                    Balance = -(vesselcost.Cost.HasValue?vesselcost.Cost.Value:0),
                    USBalance = -(vesselcost.USCost.HasValue?vesselcost.USCost.Value:0)
                };
                context.VesselBalances.Add(balance);
            }
            else
            {
                balance.Balance -= vesselcost.Cost.HasValue ? vesselcost.Cost.Value : 0;
                balance.USBalance -= vesselcost.USCost.HasValue ? vesselcost.USCost.Value : 0;
                context.Entry(balance).State = EntityState.Modified;
            }
            vesselcost.Balance = balance.Balance;
            vesselcost.USBalance = balance.USBalance;
            if ((!vesselcost.Payment.HasValue && !vesselcost.Cost.HasValue || vesselcost.Payment >= vesselcost.Cost) && (!vesselcost.USPayment.HasValue && !vesselcost.USCost.HasValue || vesselcost.USPayment >= vesselcost.USCost))
            {
                vesselcost.Payoff = true;
            }

            vesselcost.Side = VesselAccountSide.Cost;
            return Add(vesselcost, isSave);
        }

        public VesselCostPayment AddPayment(VesselCostPayment payment,bool payoff)
        {
            if (payment.Payment == 0)
                payment.Payment = null;
            if (payment.USPayment == 0)
                payment.USPayment = null;
            var cost = Find(payment.VesselAccountID);
            cost.Payoff = payoff;
            if ((!payment.Payment.HasValue && !payment.Debt.HasValue || payment.Payment >= cost.Debt) && (!payment.USPayment.HasValue && !payment.USDebt.HasValue || payment.USPayment >= cost.USDebt))
            {
                cost.Payoff = true;
            }
            if(cost.Payment.HasValue)
            {
                cost.Payment += payment.Payment ?? 0;
            }
            else
            {
                cost.Payment = payment.Payment;
            }
            if (cost.USPayment.HasValue)
            {
                cost.USPayment += payment.USPayment ?? 0;
            }
            else
            {
                cost.USPayment = payment.USPayment;
            }
            
            cost.Debt = payment.Debt;
            cost.USDebt = payment.USDebt;
            cost.PaymentDate = payment.PaymentDate;
            context.VesselCostPayments.Add(payment);
            Update(cost);
            return payment;
        }

        public VesselAccount AddDeposit(VesselAccount vesseldeposit, bool isSave = true)
        {
            var balance = GetBalance(vesseldeposit.VesselID);
            if (balance == null)
            {
                balance = new VesselBalance()
                {
                    VesselID = vesseldeposit.VesselID,
                    ShipownerID = vesseldeposit.ShipownerID,
                    Balance = vesseldeposit.Deposit.HasValue?vesseldeposit.Deposit.Value:0,
                    USBalance = vesseldeposit.USDeposit.HasValue?vesseldeposit.USDeposit.Value:0
                };
                context.VesselBalances.Add(balance);
            }
            else
            {
                balance.Balance += vesseldeposit.Deposit.HasValue ? vesseldeposit.Deposit.Value : 0;
                balance.USBalance += vesseldeposit.USDeposit.HasValue ? vesseldeposit.USDeposit.Value : 0;
                context.Entry(balance).State = EntityState.Modified;
            }
            vesseldeposit.Balance = balance.Balance;
            vesseldeposit.USBalance = balance.USBalance;
            vesseldeposit.Side = VesselAccountSide.Deposit;
            if (!vesseldeposit.PaymentDate.HasValue)
            {
                vesseldeposit.PaymentDate = DateTime.Now.Date;
            }
            vesseldeposit.InvoiceDate = vesseldeposit.PaymentDate.Value;
            vesseldeposit.CompanyID = null;
            return Add(vesseldeposit, isSave);
        }
        public bool DeleteAccount(int? accountID)
        {
            var account = Find(accountID);
            if (account == null)
                return true;
            DeleteFile(account.InvoiceFileID);
            DeleteFile(account.SignFileID);
            var balance = GetBalance(account.VesselID);
            if (account.Side == VesselAccountSide.Deposit)
            {
                balance.Balance -= account.Deposit.HasValue ? account.Deposit.Value : 0;
                balance.USBalance -= account.USDeposit.HasValue ? account.USDeposit.Value : 0;
            }
            else
            {
                var payments = context.VesselCostPayments.Where(v => v.VesselAccountID == accountID.Value).ToList();
                foreach (var payment in payments)
                {
                    DeleteFile(payment.ReceiptFileID);
                }
                context.VesselCostPayments.RemoveRange(payments);
                balance.Balance += account.Cost.HasValue ? account.Cost.Value : 0;
                balance.USBalance += account.USCost.HasValue ? account.USCost.Value : 0;
            }
            context.Entry(balance).State = EntityState.Modified;
            return Delete(account);
        }
        public bool DeletePayment(int? paymentID)
        {
            var payment = context.VesselCostPayments.Find(paymentID);
            if (payment == null)
                return true;
            var cost = Find(payment.VesselAccountID);
            if (cost == null)
                return true;
            cost.Payoff = false;
            if (cost.Payment.HasValue)
            {
                cost.Payment -= payment.Payment ?? 0;
            }
            if (cost.USPayment.HasValue)
            {
                cost.USPayment -= payment.USPayment ?? 0;
            }
            if (cost.Debt.HasValue)
            {
                cost.Debt += payment.Payment ?? 0;
            }
            if (cost.USDebt.HasValue)
            {
                cost.USDebt += payment.USPayment ?? 0;
            }
            context.VesselCostPayments.Remove(payment);
            return Update(cost);
        }

        public void DeleteFile(int? fileId)
        {
            var file = context.UploadFiles.Find(fileId);
            if (file == null)
                return ;
            string oldDiskPath = HttpContext.Current.Server.MapPath(file.Path);
            if (System.IO.File.Exists(oldDiskPath))
            {
                System.IO.File.Delete(oldDiskPath);
            }
            context.UploadFiles.Remove(file);
        }
    }
}