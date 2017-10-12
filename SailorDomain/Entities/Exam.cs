using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SailorDomain.Entities
{
    /// <summary>
    /// 考证情况
    /// </summary>
    public class Exam:IEntity
    {
        public int ExamID { get; set; }

        [Display(Name = "申请职务")]
        public Post? ApplyPost { get; set; }

        [Display(Name = "准考证号")]
        [StringLength(50)]
        public string ExamNo { get; set; }

        [Display(Name = "考试日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ExamDate { get; set; }

        [Display(Name = "费用")]
        [DataType(DataType.Currency)]
        public double? Expense { get; set; }

        [Display(Name = "报销费用")]
        [DataType(DataType.Currency)]
        public double? ExpenseClaim { get; set; }

        [Display(Name = "证书号码")]
        [StringLength(50)]
        public string CertificateNo { get; set; }

        [Display(Name = "签发日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "合格")]
        public bool Qualified { get; set; }

        [Display(Name = "备注")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Display(Name="船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }
        public virtual Sailor Sailor { get; set; }

        public virtual ICollection<ExamItem> ExamItems { get; set; }

        [StringLength(50)]
        public string SysUserId { get; set; }
        public int SysCompanyId { get; set; }
    }
}