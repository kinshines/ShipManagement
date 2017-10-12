using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public interface ICertificateTypeService : IBaseService<CertificateType>
    {
        IQueryable<CertificateType> GetVesselCertificates();
        IQueryable<CertificateType> GetSailorCertificates();
    }
}
