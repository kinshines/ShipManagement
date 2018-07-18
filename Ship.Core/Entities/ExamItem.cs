using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 考试科目
    /// </summary>
    public class ExamItem
    {
        public int ExamItemID { get; set; }

        [Display(Name = "考试科目")]
        [StringLength(50)]
        public string ItemName { get; set; }

        [Display(Name = "考试日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ExamDate { get; set; }

        [Display(Name = "考试成绩")]
        public int Score { get; set; }

        [Display(Name = "是否通过")]
        public bool Qualified { get; set; }
        public int ExamID { get; set; }
        public virtual Exam Exam { get; set; }
    }
}
