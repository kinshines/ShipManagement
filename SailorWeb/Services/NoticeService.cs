using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using SailorDomain.Entities;
using Microsoft.AspNet.Identity;
using System.Linq.Expressions;

namespace SailorWeb.Services
{
    public class NoticeService : AuthorizeBaseService<Notice>, INoticeService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public IQueryable<Notice> GetMessage()
        {
            logger.Info("getNoticeMessage" + SysUserId);
            return GetEntities().Where(n => n.NoticeTime <= DateTime.Now && n.Active);
        }
        public void AddSailorCertificateNotice(Certificate certificate)
        {
            if (certificate.NoticeDate != null && certificate.ExpiryDate != null)
            {
                Notice notice = new Notice()
                {
                    Source = NoticeSource.Certificate,
                    SourceID = certificate.CertificateID,
                    NoticeTime = certificate.NoticeDate.Value,
                    Deadline = certificate.ExpiryDate.Value,
                    Content = "船员 " + certificate.SailorName + " 的 " + certificate.Name + " 证书将于 " + certificate.ExpiryDate.Value.ToLongDateString() + " 到期",
                    Active = true,
                    SysUserId=SysUserId,
                    SysCompanyId=SysCompanyId
                };
                Add(notice);
                HttpContext.Current.Cache["NoticeMessage" + SysUserId] = DateTime.Now.Ticks.ToString();
            }
        }

        public void AddVesselCertificateNotice(VesselCertificate vesselcertificate)
        {
            if (vesselcertificate.ExpiryNoticeDate != null && vesselcertificate.ExpiryDate != null)
            {
                Notice notice = new Notice()
                {
                    Source = NoticeSource.VesselCertificate,
                    SourceID = vesselcertificate.VesselCertificateID,
                    NoticeTime = vesselcertificate.ExpiryNoticeDate.Value,
                    Deadline = vesselcertificate.ExpiryDate.Value,
                    Content = "船舶 " + vesselcertificate.VesselName + " 的 " + vesselcertificate.Name + " 证书将于 " + vesselcertificate.ExpiryDate.Value.ToLongDateString() + " 到期",
                    Active = true,
                    SysUserId = SysUserId,
                    SysCompanyId = SysCompanyId
                };
                Add(notice);
                HttpContext.Current.Cache["NoticeMessage" + SysUserId] = DateTime.Now.Ticks.ToString();
            }
            if (vesselcertificate.CheckNoticeDate != null && vesselcertificate.CheckBeginDate != null)
            {
                Notice notice = new Notice()
                {
                    Source = NoticeSource.VesselCertificate,
                    SourceID = vesselcertificate.VesselCertificateID,
                    NoticeTime = vesselcertificate.CheckNoticeDate.Value,
                    Deadline = vesselcertificate.CheckBeginDate.Value,
                    Content = "船舶 " + vesselcertificate.VesselName + " 的 " + vesselcertificate.Name + " 证书将于 " + vesselcertificate.CheckBeginDate.Value.ToLongDateString() + " 到期检验",
                    Active = true,
                    SysUserId = SysUserId,
                    SysCompanyId = SysCompanyId
                };
                Add(notice);
                HttpContext.Current.Cache["NoticeMessage" + SysUserId] = DateTime.Now.Ticks.ToString();
            }
        }

        public void AddSailorContractNotice(Contract contract)
        {
            if (contract.NoticeDate != null && contract.AshoreDate != null)
            {
                Notice notice = new Notice()
                {
                    Source = NoticeSource.Contract,
                    SourceID = contract.ContractID,
                    NoticeTime = contract.NoticeDate.Value,
                    Deadline = contract.AshoreDate.Value,
                    Content = "船员 " + contract.SailorName + " 与船舶 " + contract.VesselName + " 签订的合同将于 " + contract.AshoreDate.Value.ToLongDateString() + " 到期",
                    Active = true,
                    SysUserId = SysUserId,
                    SysCompanyId = SysCompanyId
                };
                Add(notice);
                HttpContext.Current.Cache["NoticeMessage" + SysUserId] = DateTime.Now.Ticks.ToString();
            }
        }

        public void NoticeHandle(NoticeHandle handle)
        {
            Notice notice = Find(handle.NoticeID);

            if (handle.HandleType==NoticeHandleType.推迟提醒 && handle.DelayDays.HasValue)
            {
                Notice delayNotice = new Notice()
                {
                    Source = notice.Source,
                    SourceID = notice.SourceID,
                    NoticeTime = notice.NoticeTime.AddDays(handle.DelayDays.Value),
                    Deadline = notice.Deadline,
                    Content = notice.Content,
                    Active = true,
                    SysUserId = SysUserId,
                    SysCompanyId = SysCompanyId
                };
                Add(delayNotice, false);
            }
            notice.Active = false;
            Update(notice);

            HttpContext.Current.Cache["NoticeMessage" + SysUserId] = DateTime.Now.Ticks.ToString();
        }

        public new bool Delete(int ID, bool isSave = true)
        {
            HttpContext.Current.Cache["NoticeMessage" + SysUserId] = DateTime.Now.Ticks.ToString();
            return base.Delete(ID, isSave);
        }

        public new bool DeleteRange(Expression<Func<Notice, bool>> whereLambda, bool isSave = true)
        {
            HttpContext.Current.Cache["NoticeMessage" + SysUserId] = DateTime.Now.Ticks.ToString();
            return base.DeleteRange(whereLambda, isSave);
        }
    }
}