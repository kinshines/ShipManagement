using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SailorDomain.Entities
{
    /// <summary>
    /// 劳保用品库存记录
    /// </summary>
    public class LaborSupplyPut
    {
        public int LaborSupplyPutID { get; set; }

        [Display(Name = "存入类别")]
        public LaborSupplyPutType PutType { get; set; }

        [Display(Name="日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PutDate { get; set; }

        [Display(Name="数量")]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }

        [Display(Name = "备注")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Display(Name="用品名称")]
        public int LaborSupplyID { get; set; }
        public virtual LaborSupply LaborSupply { get; set; }
    }
}