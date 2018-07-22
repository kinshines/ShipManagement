using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ship.Web.ViewModels
{
    public class AboardContract
    {
        public int ContractID { get; set; }
        [Display(Name = "船员姓名")]
        public string SailorName { get; set; }
        [Display(Name = "船舶名称")]
        public string VesselName { get; set; }

        [Display(Name = "合同期限")]
        [DisplayFormat(DataFormatString = "{0} 个月")]
        public int? Term { get; set; }

        [Display(Name = "上船日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AboardDate { get; set; }

        [Display(Name = "计划下船日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AshoreDate { get; set; }

        [Display(Name = "提醒日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? NoticeDate { get; set; }

        [Display(Name = "上船地点")]
        [StringLength(50)]
        public string AboardPlace { get; set; }

        [Display(Name = "航区")]
        [StringLength(50)]
        public string SailZone { get; set; }

        [Display(Name = "备注")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }
    }
}