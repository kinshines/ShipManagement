using Ship.Core.Enums;
using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 合同
    /// </summary>
    public class Contract : BaseEntity
    {
        public int ContractID { get; set; }

        [Display(Name = "合同号")]
        [StringLength(50)]
        public string ContractNo { get; set; }

        [Display(Name = "签订日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? SigningDate { get; set; }

        [Display(Name = "职务")]
        public Post Post { get; set; }

        [Display(Name = "合同期限")]
        [DisplayFormat(DataFormatString = "{0} 个月")]
        public int? Term { get; set; }

        [Display(Name = "上船日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AboardDate { get; set; }

        [Display(Name = "计划下船日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AshoreDate { get; set; }

        [Display(Name = "提醒日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? NoticeDate { get; set; }

        [Display(Name = "合同工资")]
        public double? Wage { get; set; }

        [Display(Name = "船付工资")]
        public double? ShipWage { get; set; }

        [Display(Name = "家汇工资")]
        public double? HomeWage { get; set; }

        [Display(Name = "休假工资")]
        public double? VacationWage { get; set; }

        [Display(Name = "管理费")]
        public double? ManagementFee { get; set; }

        [Display(Name = "中介费")]
        public double? AgencyFee { get; set; }

        [Display(Name = "付第三方费")]
        public double? ThirdPartyFee { get; set; }

        [Display(Name = "合同备注")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Display(Name = "船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }
        public virtual Sailor Sailor { get; set; }

        [Display(Name = "船东")]
        public int ShipownerID { get; set; }

        [Display(Name = "船东")]
        [StringLength(50)]
        public string ShipownerName { get; set; }
        public virtual Shipowner Shipowner { get; set; }

        [Display(Name = "船舶名称")]
        public int VesselID { get; set; }

        [Display(Name = "船舶名称")]
        [StringLength(50)]
        public string VesselName { get; set; }

        public virtual Vessel Vessel { get; set; }

        [Display(Name = "工资发放周期")]
        [DisplayFormat(DataFormatString = "{0} 个月")]
        public int WageInterval { get; set; }
        public bool Complete { get; set; }
    }
}
