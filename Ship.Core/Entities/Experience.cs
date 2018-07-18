using Ship.Core.Enums;
using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    public class Experience : BaseEntity
    {
        public int ExperienceID { get; set; }

        [Display(Name = "开始工作时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BeginTime { get; set; }

        [Display(Name = "结束工作时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? EndTime { get; set; }

        [Display(Name = "职务")]
        public Post? Post { get; set; }

        [Display(Name = "单位名称")]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Display(Name = "船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }

        [Display(Name = "船舶名称")]
        [StringLength(50)]
        public string VesselName { get; set; }

        [Display(Name = "IMO号")]
        [StringLength(20)]
        public string IMO { get; set; }

        [Display(Name = "工作时间")]
        [DisplayFormat(DataFormatString = "{0} M")]
        public int? DurationMonth { get; set; }

        [Display(Name = "船舶类型")]
        public VesselType? VesselType { get; set; }

        [Display(Name = "船旗")]
        [StringLength(50)]
        public string Flag { get; set; }

        [Display(Name = "载重吨")]
        public int? DeadWeightTon { get; set; }

        [Display(Name = "主机型号")]
        [StringLength(200)]
        public string MainEngine { get; set; }

        [Display(Name = "马力")]
        [StringLength(10)]
        public string Power { get; set; }
        public Sailor Sailor { get; set; }
    }
}
