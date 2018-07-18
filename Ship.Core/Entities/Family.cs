using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 家庭
    /// </summary>
    public class Family : BaseEntity
    {
        public int FamilyID { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "关系")]
        [StringLength(10)]
        public string Relationship { get; set; }

        [Display(Name = "是否受益人")]
        public bool Beneficiary { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(20)]
        public string Telephone { get; set; }

        [Display(Name = "家庭住址")]
        [StringLength(50)]
        public string Address { get; set; }

        [Display(Name = "备注")]
        [StringLength(50)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Display(Name = "船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }
        public Sailor Sailor { get; set; }
    }
}
