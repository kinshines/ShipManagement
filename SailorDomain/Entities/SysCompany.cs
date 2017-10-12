using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SailorDomain.Entities
{
    public class SysCompany
    {
        public int SysCompanyId { get; set; }

        [StringLength(50)]
        [Display(Name="单位名称")]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name="联系电话")]
        public string Telephone { get; set; }

        [StringLength(10)]
        [Display(Name="联系人")]
        public string Contacter { get; set; }

        [Display(Name="开通时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OpenTime { get; set; }

        [Display(Name="到期时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpireTime { get; set; }
    }
}