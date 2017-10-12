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
    public interface INoticeService : IBaseService<Notice>
    {
        IQueryable<Notice> GetMessage();
        void AddSailorCertificateNotice(Certificate certificate);
        void AddSailorContractNotice(Contract contract);
        void AddVesselCertificateNotice(VesselCertificate vesselcertificate);
        void NoticeHandle(NoticeHandle handle);
        new bool Delete(int ID, bool isSave = true);
    }
}
