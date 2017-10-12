using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SailorDomain.Entities
{
    /// <summary>
    /// 培训人员
    /// </summary>
    public class Trainee:IEntity
    {
        public int TraineeID { get; set; }

        [Display(Name="费用")]
        [DataType(DataType.Currency)]
        public double? Expense { get; set; }

        [Display(Name = "报销费用")]
        [DataType(DataType.Currency)]
        public double? ExpenseClaim { get; set; }

        [Display(Name = "证书编号")]
        [StringLength(50)]
        public string CertificateNo { get; set; }

        [Display(Name="合格")]
        public bool Qualified { get; set; }

        [Display(Name = "备注")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Display(Name="培训班")]
        public int TrainingClassID { get; set; }
        public virtual TrainingClass TrainingClass { get; set; }

        [Display(Name = "培训班")]
        [StringLength(50)]
        public string TrainingClassName { get; set; }

        [Display(Name="船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }
        public virtual Sailor Sailor { get; set; }

        [StringLength(50)]
        public string SysUserId { get; set; }
        public int SysCompanyId { get; set; }
    }
}