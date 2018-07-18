using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Core.Enums;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class CertificateTypeService : AuthorizeBaseService<CertificateType>
    {
        public CertificateTypeService(DefaultDbContext cxt, ILogger<CertificateTypeService> logger) : base(cxt, logger)
        {
        }
        private IQueryable<CertificateType> GetAllCertificates()
        {
            var query = context.Set<CertificateType>().Where(t => t.IsPublic);
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
