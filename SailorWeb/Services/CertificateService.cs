using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorDomain.Entities;
using SailorDomain.Services;
using SailorDomain.Infrastructure;

namespace SailorWeb.Services
{
    public class CertificateService : AuthorizeBaseService<Certificate>, ICertificateService
    {
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