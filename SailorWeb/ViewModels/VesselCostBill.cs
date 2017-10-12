using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SailorWeb.ViewModels
{
    public class VesselCostBill
    {
        public int VesselID { get; set; }

        [Display(Name = "船舶名称")]
        public string VesselName { get; set; }
        public int ShipownerID { get; set; }

        [Display(Name = "船东")]
        public string ShipownerName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "起始日期")]
        public DateTime BeginDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "截止日期")]
        public DateTime EndDate { get; set; }

        [Display(Name="船员花销")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Sailor { get; set; }

        [Display(Name = "船员花销")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USSailor { get; set; }

        [Display(Name = "物料")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Material { get; set; }

        [Display(Name = "物料")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USMaterial { get; set; }

        [Display(Name = "备件")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Spareparts { get; set; }

        [Display(Name = "备件")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USSpareparts { get; set; }

        [Display(Name = "维修保养")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Maintenance { get; set; }

        [Display(Name = "维修保养")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USMaintenance { get; set; }

        [Display(Name = "滑油")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? LubricatingOil { get; set; }

        [Display(Name = "滑油")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USLubricatingOil { get; set; }

        [Display(Name = "日常花销")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? DailyExpenses { get; set; }

        [Display(Name = "日常花销")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USDailyExpenses { get; set; }

        [Display(Name = "通讯费")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Communication { get; set; }

        [Display(Name = "通讯费")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USCommunication { get; set; }

        [Display(Name = "保险费")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Insurance { get; set; }

        [Display(Name = "保险费")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USInsurance { get; set; }

        [Display(Name = "其他费用")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Others { get; set; }

        [Display(Name = "其他费用")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USOthers { get; set; }

        [Display(Name = "检验发证")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Certificate { get; set; }

        [Display(Name = "检验发证")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USCertificate { get; set; }

        [Display(Name="合计费用")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double? Total 
        {
            get
            {
                return Sailor + Material + Spareparts + Maintenance + LubricatingOil + DailyExpenses + Communication + Insurance + Others + Certificate;
            }
        }

        [Display(Name = "合计费用")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double? USTotal
        {
            get
            {
                return USSailor + USMaterial + USSpareparts + USMaintenance + USLubricatingOil + USDailyExpenses + USCommunication + USInsurance + USOthers + USCertificate;
            }
        }
    }
}