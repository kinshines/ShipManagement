using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    public class Wage : BaseEntity
    {
        [Key]
        public int WageID { get; set; }

        [Display(Name = "年")]
        public int Year { get; set; }

        [Display(Name = "月")]
        public int Month { get; set; }
        [Display(Name = "当月天数")]
        public int MonthlyDays { get; set; }
        [Display(Name = "开始日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "结束日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "出勤天数")]
        public int WorkDays
        {
            get { return (EndDate - BeginDate).Days + 1; }
            private set { value = (EndDate - BeginDate).Days + 1; }
        }

        [Display(Name = "标准工资")]
        public double? StandardWage { get; set; }
        [Display(Name = "应发工资")]
        public double? ShouldWage
        {
            get { return Math.Round((StandardWage * WorkDays / MonthlyDays).Value, 2); }
            private set { value = Math.Round((StandardWage * WorkDays / MonthlyDays).Value, 2); }
        }
        public int ContractID { get; set; }
        public virtual Contract Contract { get; set; }
        [Display(Name = "船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }
        public virtual Sailor Sailor { get; set; }
    }
}
