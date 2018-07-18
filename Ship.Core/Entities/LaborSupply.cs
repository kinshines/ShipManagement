using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 劳保用品
    /// </summary>
    public class LaborSupply : BaseEntity
    {
        public int LaborSupplyID { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "规格")]
        [StringLength(20)]
        public string Specification { get; set; }

        [Display(Name = "库存数量")]
        [Range(0, int.MaxValue)]
        public int Total { get; set; }

        [Display(Name = "底线")]
        [Range(0, int.MaxValue)]
        public int Baseline { get; set; }

        [Display(Name = "备注")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        public virtual ICollection<LaborSupplyTake> LaborSupplyTakes { get; set; }
        public virtual ICollection<LaborSupplyPut> LaborSupplyPuts { get; set; }
    }
}
