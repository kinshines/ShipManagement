using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SailorDomain.Entities
{
    public class CertificateType:IEntity
    {
        public int CertificateTypeID { get; set; }

        [Display(Name = "证书名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "证书描述")]
        [StringLength(100)]
        public string Description { get; set; }

        [Display(Name = "证书类别")]
        public CertificateCategory CertificateCategory { get; set; }

        [Display(Name = "是否公共")]
        public bool IsPublic { get; set; }

        [StringLength(50)]
        public string SysUserId { get; set; }
        public int SysCompanyId { get; set; }
    }
}