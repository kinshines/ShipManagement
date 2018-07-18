using Ship.Core.Enums;
using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 业务往来单位
    /// </summary>
    public class Company : BaseEntity
    {
        [Key]
        public int CompanyID { get; set; }

        [Display(Name = "单位名称")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "单位类别")]
        public CompanyType? Type { get; set; }

        [Display(Name = "客户编号")]
        [StringLength(20)]
        public string Code { get; set; }

        [Display(Name = "单位性质")]
        public CompanyProperty? Property { get; set; }

        [Display(Name = "法人代表")]
        [StringLength(10)]
        public string Representative { get; set; }

        [Display(Name = "信誉等级")]
        public CompanyHonor? HonorLevel { get; set; }

        [Display(Name = "地址")]
        [StringLength(200)]
        public string Address { get; set; }

        [Display(Name = "邮编")]
        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Display(Name = "电话")]
        [StringLength(20)]
        public string Telephone { get; set; }

        [Display(Name = "传真")]
        [StringLength(20)]
        public string Fax { get; set; }

        [Display(Name = "电子邮箱")]
        [StringLength(20)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "网址")]
        [StringLength(200)]
        public string Website { get; set; }

        [Display(Name = "联系人")]
        [StringLength(20)]
        public string Contacter { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(20)]
        public string ContactTel { get; set; }

        [Display(Name = "税号")]
        [StringLength(50)]
        public string TaxNo { get; set; }

        [Display(Name = "银行账户")]
        [StringLength(100)]
        public string BankAccount { get; set; }

        [Display(Name = "开户银行")]
        [StringLength(100)]
        public string Bank { get; set; }

        [Display(Name = "备注")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        public virtual ICollection<VesselAccount> VesselCosts { get; set; }
    }
}
