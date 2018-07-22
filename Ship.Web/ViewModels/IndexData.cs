using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ship.Web.ViewModels
{
    public class IndexData
    {
        [Display(Name = "消息提醒")]
        public int Notice { get; set; }

        [Display(Name = "所有船员")]
        public int SailorTotal { get; set; }

        [Display(Name = "休假船员")]
        public int SailorRest { get; set; }

        [Display(Name = "在船船员")]
        public int SailorAboard { get; set; }

        [Display(Name = "待派船员")]
        public int SailorWaiting { get; set; }

        [Display(Name = "所有证书")]
        public int CertificateTotal { get; set; }

        [Display(Name = "即将到期证书")]
        public int CertificateNotice { get; set; }

        [Display(Name = "逾期证书")]
        public int CertificateOverdue { get; set; }

        [Display(Name = "合同履行中")]
        public int ContractPerform { get; set; }

        [Display(Name = "即将到期合同")]
        public int ContractNotice { get; set; }

        [Display(Name = "所有船东")]
        public int Shipowner { get; set; }

        [Display(Name = "所有船舶")]
        public int Vessel { get; set; }
    }
}