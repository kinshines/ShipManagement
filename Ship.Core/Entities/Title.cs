using Ship.Core.Enums;
using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 职称
    /// </summary>
    public class Title : BaseEntity
    {
        public int TitleID { get; set; }

        [Display(Name = "职称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "获得资格途径")]
        [StringLength(50)]
        public string Approach { get; set; }

        [Display(Name = "开始日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "结束日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "主管从事工作")]
        [StringLength(50)]
        public string Work { get; set; }

        [Display(Name = "专业")]
        [StringLength(50)]
        public string Major { get; set; }

        [Display(Name = "职业类别")]
        [StringLength(50)]
        public string Category { get; set; }

        [Display(Name = "聘任职务")]
        public Post? Post { get; set; }

        [Display(Name = "聘任单位")]
        [StringLength(50)]
        public string Company { get; set; }

        [Display(Name = "聘任日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? EngageDate { get; set; }

        [Display(Name = "备注")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Display(Name = "船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }
        public virtual Sailor Sailor { get; set; }
    }
}
