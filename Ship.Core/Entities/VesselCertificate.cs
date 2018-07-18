using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 船舶证书
    /// </summary>
    public class VesselCertificate : BaseEntity
    {
        public int VesselCertificateID { get; set; }

        [Display(Name = "证书名称")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "证书编号")]
        [StringLength(50)]
        public string Code { get; set; }

        [Display(Name = "签发日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "失效日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "检验日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CheckBeginDate { get; set; }

        [Display(Name = "检验日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CheckEndDate { get; set; }

        [Display(Name = "检验提醒日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CheckNoticeDate { get; set; }

        [Display(Name = "失效提醒日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryNoticeDate { get; set; }


        [Display(Name = "船舶名称")]
        public int VesselID { get; set; }

        [Display(Name = "船舶名称")]
        [StringLength(50)]
        public string VesselName { get; set; }

        public int? FileID { get; set; }

        public virtual Vessel Vessel { get; set; }
        [Display(Name = "证书名称")]
        public int CertificateTypeID { get; set; }
    }
}
