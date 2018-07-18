using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Core.Enums;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class CertificateService: AuthorizeBaseService<Certificate>
    {
        public CertificateService(DefaultDbContext cxt, ILogger<CertificateService> logger):base(cxt,logger)
        {
        }
        public Dictionary<String, String> AddCertificateDic(string prefix, Dictionary<String, String> dic, Certificate certificate)
        {
            if (certificate != null)
            {
                dic.Add(prefix + "No", certificate.Code);
                dic.Add(prefix + "IssueDate", EnglishIssueDate(certificate));
                dic.Add(prefix + "IssuePlace", certificate.IssuePlace);
                dic.Add(prefix + "ExpiryDate", EnglishExpiryDate(certificate));
            }
            return dic;
        }
        private string EnglishIssueDate(Certificate certificate)
        {
            return EnglishConvert.EnglishDate(certificate.IssueDate);
        }
        private string EnglishExpiryDate(Certificate certificate)
        {
            return EnglishConvert.EnglishDate(certificate.ExpiryDate);
        }
    }
}
