using Ship.Core.Enums;
using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 证书
    /// </summary>
    public class Certificate: BaseEntity
    {
        public int CertificateID { get; set; }

        [Display(Name = "证书名称")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "证书编号")]
        [StringLength(50)]
        public string Code { get; set; }

        [Display(Name = "签发地点")]
        [StringLength(50)]
        public string IssuePlace { get; set; }

        [Display(Name = "签发日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "失效日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "提醒日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NoticeDate { get; set; }

        [Display(Name = "海事局")]
        [StringLength(20)]
        public string MaritimeBureau { get; set; }

        [Display(Name = "工作部门")]
        public VesselDepartment? Department { get; set; }

        [Display(Name = "证书等级")]
        public CertificateDegree? Degree { get; set; }

        [Display(Name = "船舶等级")]
        public VesselDegree? VesselDegree { get; set; }

        [Display(Name = "国家标志")]
        [StringLength(20)]
        public string Nationality { get; set; }

        [Display(Name = "船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }

        public int? FileID { get; set; }

        public virtual Sailor Sailor { get; set; }
        [Display(Name = "证书名称")]
        public int CertificateTypeID { get; set; }
    }
}
