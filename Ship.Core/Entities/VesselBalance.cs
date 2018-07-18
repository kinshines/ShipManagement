using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ship.Core.Entities
{
    public class VesselBalance
    {
        [Key]
        [ForeignKey("Vessel")]
        public int VesselID { get; set; }

        [Display(Name = "人民币账户余额")]
        [DisplayFormat(DataFormatString = "¥{0}")]
        public double Balance { get; set; }

        [Display(Name = "美元账户余额")]
        [DisplayFormat(DataFormatString = "${0}")]
        public double USBalance { get; set; }
        public virtual Vessel Vessel { get; set; }
        public int ShipownerID { get; set; }
        public virtual Shipowner Shipowner { get; set; }
    }
}
