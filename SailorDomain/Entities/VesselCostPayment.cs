using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SailorDomain.Entities
{
    public class VesselCostPayment
    {
        public int VesselCostPaymentID { get; set; }

        [Display(Name = "本次出账(人民币)")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Payment { get; set; }

        [Display(Name = "本次出账(美元)")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USPayment { get; set; }

        [Display(Name = "余额(人民币)")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Debt { get; set; }

        [Display(Name = "余额(美元)")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USDebt { get; set; }

        [Display(Name = "付款日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }
        public int VesselAccountID { get; set; }
        public int? ReceiptFileID { get; set; }

        [Display(Name = "备注")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }
        public virtual VesselAccount VesselAccount { get; set; }
    }
}