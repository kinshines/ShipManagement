using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 培训班
    /// </summary>
    public class TrainingClass : BaseEntity
    {
        public int TrainingClassID { get; set; }

        [Display(Name = "培训班名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "培训项目")]
        [StringLength(50)]
        public string Subject { get; set; }

        [Display(Name = "开始日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "结束日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "期数")]
        public int? Period { get; set; }

        [Display(Name = "课时")]
        public int? ClassHour { get; set; }

        [Display(Name = "办班形式")]
        [StringLength(20)]
        public string Form { get; set; }

        [Display(Name = "培训对象")]
        [StringLength(20)]
        public string Target { get; set; }

        [Display(Name = "办班性质")]
        [StringLength(20)]
        public string Property { get; set; }

        [Display(Name = "参加人数")]
        public int? ParticipantNumber { get; set; }

        [Display(Name = "结业人数")]
        public int? GraduateNumber { get; set; }

        [Display(Name = "学制")]
        [StringLength(20)]
        public string SchoolingLength { get; set; }

        [Display(Name = "学历")]
        [StringLength(20)]
        public string EducationDegree { get; set; }

        [Display(Name = "教师")]
        [StringLength(20)]
        public string Teacher { get; set; }

        [Display(Name = "培训单位")]
        [StringLength(50)]
        public string Company { get; set; }

        [Display(Name = "培训费用")]
        [DataType(DataType.Currency)]
        public double? Fees { get; set; }

        [Display(Name = "备注")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        public virtual ICollection<Trainee> Trainees { get; set; }
    }
}
