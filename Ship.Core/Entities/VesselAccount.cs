using Ship.Core.Enums;
using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    public class VesselAccount : BaseEntity
    {
        public int VesselAccountID { get; set; }

        [Display(Name = "费用项")]
        public VesselFeeItem FeeItem { get; set; }

        [Display(Name = "借贷方向")]
        public VesselAccountSide Side { get; set; }

        [Display(Name = "费用额(人民币)")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Cost { get; set; }

        [Display(Name = "费用额(美元)")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USCost { get; set; }

        [Display(Name = "汇款额(人民币)")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Deposit { get; set; }

        [Display(Name = "汇款额(美元)")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USDeposit { get; set; }

        [Display(Name = "是否付清")]
        public bool Payoff { get; set; }

        [Display(Name = "已付款额(人民币)")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Payment { get; set; }

        [Display(Name = "已付款额(美元)")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USPayment { get; set; }

        [Display(Name = "余额(人民币)")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Debt { get; set; }

        [Display(Name = "余额(美元)")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USDebt { get; set; }

        [Display(Name = "船舶余额(人民币)")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double Balance { get; set; }

        [Display(Name = "船舶余额(美元)")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double USBalance { get; set; }

        [Display(Name = "账单日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "付款日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "备注")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Display(Name = "船舶名称")]
        public int VesselID { get; set; }

        [Display(Name = "船舶名称")]
        [StringLength(50)]
        public string VesselName { get; set; }
        public virtual Vessel Vessel { get; set; }

        [Display(Name = "船东")]
        public int ShipownerID { get; set; }

        [Display(Name = "船东")]
        [StringLength(50)]
        public string ShipownerName { get; set; }

        [Display(Name = "收款单位")]
        public int? CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [Display(Name = "收款单位")]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [Display(Name = "发票号")]
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        public int? InvoiceFileID { get; set; }
        public int? SignFileID { get; set; }
        public virtual ICollection<VesselCostPayment> VesselCostPayments { get; set; }
    }
}
