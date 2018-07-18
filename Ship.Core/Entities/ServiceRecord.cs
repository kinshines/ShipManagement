using Ship.Core.Enums;
using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 服务记录
    /// </summary>
    public class ServiceRecord : BaseEntity
    {
        public int ServiceRecordID { get; set; }

        [Display(Name = "职务")]
        public Post Post { get; set; }

        [Display(Name = "航区")]
        [StringLength(50)]
        public string SailZone { get; set; }

        [Display(Name = "上船日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AboardDate { get; set; }

        [Display(Name = "上船地点")]
        [StringLength(50)]
        public string AboardPlace { get; set; }

        [Display(Name = "下船日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? AshoreDate { get; set; }

        [Display(Name = "下船地点")]
        [StringLength(50)]
        public string AshorePlace { get; set; }

        [Display(Name = "离船原因")]
        [StringLength(50)]
        public string AshoreReason { get; set; }

        [Display(Name = "备注")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Display(Name = "评价")]
        [StringLength(200)]
        public string Comment { get; set; }

        [Display(Name = "船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船东")]
        public int ShipownerID { get; set; }

        [Display(Name = "船舶名称")]
        public int VesselID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }

        [Display(Name = "船东")]
        [StringLength(50)]
        public string ShipownerName { get; set; }

        [Display(Name = "船舶名称")]
        [StringLength(50)]
        public string VesselName { get; set; }

        public int? ContractID { get; set; }

        public virtual Shipowner Shipowner { get; set; }
        public virtual Vessel Vessel { get; set; }
        public virtual Sailor Sailor { get; set; }
        public virtual Contract Contract { get; set; }

        public bool Complete { get; set; }

        [NotMapped]
        public int PostInt
        {
            get { return (int)Post; }
        }
    }
}
