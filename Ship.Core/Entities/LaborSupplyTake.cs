using Ship.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    public class LaborSupplyTake
    {
        public int LaborSupplyTakeID { get; set; }

        [Display(Name = "领取类别")]
        public LaborSupplyTakeType TakeType { get; set; }

        [Display(Name = "日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TakeDate { get; set; }

        [Display(Name = "部门")]
        [StringLength(50)]
        public string Department { get; set; }

        [Display(Name = "领用人")]
        [StringLength(10)]
        public string TakePerson { get; set; }

        [Display(Name = "数量")]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }

        [Display(Name = "备注")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }
        public int LaborSupplyID { get; set; }
        public virtual LaborSupply LaborSupply { get; set; }
    }
}
