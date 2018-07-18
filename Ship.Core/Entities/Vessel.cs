using Ship.Core.Enums;
using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 船舶
    /// </summary>
    public class Vessel : BaseEntity
    {
        public int VesselID { get; set; }

        [Display(Name = "船舶名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "船舶呼号")]
        [StringLength(20)]
        public string Catchword { get; set; }

        [Display(Name = "船舶类型")]
        public VesselType? Type { get; set; }

        [Display(Name = "船旗")]
        [StringLength(50)]
        public string Flag { get; set; }

        [Display(Name = "航区")]
        [StringLength(50)]
        public string SailZone { get; set; }

        [Display(Name = "总吨")]
        public int? GrossTon { get; set; }

        [Display(Name = "载重吨")]
        public int? DeadWeightTon { get; set; }

        [Display(Name = "净吨")]
        public int? NetTon { get; set; }

        [Display(Name = "马力")]
        [StringLength(10)]
        public string Power { get; set; }

        [Display(Name = "建造日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BuildDate { get; set; }

        [Display(Name = "建造地点")]
        [StringLength(200)]
        public string BuildPlace { get; set; }

        [Display(Name = "主机型号及缸径")]
        [StringLength(200)]
        public string MainEngine { get; set; }

        [Display(Name = "辅机型号")]
        [StringLength(200)]
        public string AuxiliaryEngine { get; set; }

        [Display(Name = "发电机")]
        [StringLength(20)]
        public string ElectricGenerator { get; set; }

        [Display(Name = "最低配员")]
        public int? MinManning { get; set; }

        [Display(Name = "管理人")]
        [StringLength(20)]
        public string Manager { get; set; }

        [Display(Name = "所属船东")]
        public int ShipownerID { get; set; }

        [Display(Name = "所属船东")]
        [StringLength(50)]
        public string ShipownerName { get; set; }

        [Display(Name = "IMO号")]
        [StringLength(20)]
        public string IMO { get; set; }

        [Display(Name = "管理类别")]
        public VesselManageType VesselManageType { get; set; }
        public virtual Shipowner Shipowner { get; set; }
        public virtual ICollection<ServiceRecord> ServiceRecords { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Sailor> Sailors { get; set; }
        public virtual ICollection<VesselAccount> VesselAccounts { get; set; }
        public virtual ICollection<VesselCertificate> VesselCertificates { get; set; }
    }
}
