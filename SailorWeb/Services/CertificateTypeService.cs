using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public class CertificateTypeService : AuthorizeBaseService<CertificateType>, ICertificateTypeService
    {
        private IQueryable<CertificateType> GetAllCertificates()
        {
            var query = context.Set<CertificateType>().Where(t=>t.IsPublic);
            return query.Concat(GetEntities());
        }
        public IQueryable<CertificateType> GetVesselCertificates()
        {
            return GetAllCertificates().Where(t => t.CertificateCategory == CertificateCategory.船舶证书);
        }
        public IQueryable<CertificateType> GetSailorCertificates()
        {
            return GetAllCertificates().Where(t => t.CertificateCategory == CertificateCategory.船员证书);
        }
    }
}