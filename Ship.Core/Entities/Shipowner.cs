using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 船东
    /// </summary>
    public class Shipowner : BaseEntity
    {
        public int ShipownerID { get; set; }

        [Display(Name = "船东名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "联系人")]
        [StringLength(50)]
        public string Contacter { get; set; }

        [Display(Name = "地址")]
        [StringLength(50)]
        public string Address { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(20)]
        public string Telephone { get; set; }

        [Display(Name = "传真")]
        [StringLength(20)]
        public string Fax { get; set; }

        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        [StringLength(20)]
        public string Email { get; set; }

        [Display(Name = "网址")]
        [StringLength(50)]
        public string Website { get; set; }

        [Display(Name = "法人")]
        [StringLength(10)]
        public string Representative { get; set; }

        [Display(Name = "邮编")]
        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public virtual ICollection<Vessel> Vessels { get; set; }
        public virtual ICollection<ServiceRecord> ServiceRecords { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
