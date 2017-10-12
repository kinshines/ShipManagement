using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SailorDomain.Entities
{
    /// <summary>
    /// 船员
    /// </summary>
    public class Sailor:IEntity
    {
        public int SailorID { get; set; }

        [Display(Name="姓名")]
        [StringLength(10)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "英文姓名")]
        [StringLength(50)]
        public string EnglishName { get; set; }

        [Display(Name = "职务")]
        public Post Post { get; set; }

        [Display(Name = "身份证号")]
        [Required]
        [StringLength(50)]
        public string IdentityNo { get; set; }

        [Display(Name="出生日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime Birthday { get; set; }

        [Display(Name="出生地")]
        [Required]
        [StringLength(50)]
        public string Birthplace { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        [Display(Name = "民族")]
        public Ethnic? Ethnic { get; set; }

        [Display(Name = "英文水平")]
        public EnglishLevel? EnglishLevel { get; set; }

        [Display(Name = "婚姻状况")]
        public Marital Marital { get; set; }

        [Display(Name = "船员来源")]
        public SailorSource? Source { get; set; }

        [Display(Name = "证书等级")]
        public CertificateDegree? Degree { get; set; }

        [Display(Name = "参加工作时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? WorkInDate { get; set; }

        [Display(Name = "进入公司时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ComeInDate { get; set; }

        [Display(Name = "学历")]
        public EducationDegree? EducationDegree { get; set; }

        [Display(Name = "专业")]
        [StringLength(50)]
        public string Major { get; set; }

        [Display(Name = "毕业院校")]
        [StringLength(50)]
        public string GraduateCollege { get; set; }

        [Display(Name = "入校时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EnrollmentDate { get; set; }

        [Display(Name = "毕业时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? GraduateDate { get; set; }

        [Display(Name = "手机")]
        [StringLength(20)]
        public string Mobile { get; set; }

        [Display(Name = "家庭联系人")]
        [StringLength(20)]
        public string HomeContacter { get; set; }

        [Display(Name = "家庭电话")]
        [StringLength(20)]
        public string HomeTel { get; set; }

        [Display(Name = "家庭住址")]
        [StringLength(200)]
        public string Address { get; set; }

        [Display(Name = "身高")]
        [DisplayFormat(DataFormatString = "{0} CM")]
        public int? Height { get; set; }

        [Display(Name = "体重")]
        [DisplayFormat(DataFormatString = "{0} KG")]
        public int? Weight { get; set; }

        [Display(Name = "血型")]
        public Blood Blood { get; set; }

        [Display(Name = "袖长")]
        [StringLength(10)]
        public string Sleeve { get; set; }

        [Display(Name = "领口")]
        [StringLength(10)]
        public string Collar { get; set; }

        [Display(Name = "腰围")]
        [StringLength(10)]
        public string Waist { get; set; }

        [Display(Name = "胸围")]
        [StringLength(10)]
        public string Chest { get; set; }

        [Display(Name = "衣长")]
        [StringLength(10)]
        public string CoatLength { get; set; }

        [Display(Name = "裤长")]
        [StringLength(10)]
        public string TrouserLength { get; set; }

        [Display(Name = "鞋号")]
        [StringLength(10)]
        public string ShoeSize { get; set; }

        [Display(Name = "帽号")]
        [StringLength(10)]
        public string HatSize { get; set; }

        [Display(Name = "工资卡号")]
        [StringLength(50)]
        public string WageCardNo { get; set; }

        [Display(Name = "账户名")]
        [StringLength(10)]
        public string AccountName { get; set; }

        [Display(Name = "开户银行")]
        [StringLength(50)]
        public string Bank { get; set; }

        [Display(Name = "公积金账号")]
        [StringLength(50)]
        public string ProvidentFundNo { get; set; }

        [Display(Name = "养老金账号")]
        [StringLength(50)]
        public string PensionNo { get; set; }

        [Display(Name = "失业救济金账号")]
        [StringLength(50)]
        public string UnemployBenefitNo { get; set; }

        [Display(Name = "医疗保险金账号")]
        [StringLength(50)]
        public string MedicalInsuranceNo { get; set; }

        [Display(Name = "社会保险卡号")]
        [StringLength(50)]
        public string SocialInsuranceNo { get; set; }

        [Display(Name = "电子邮箱")]
        [DataType(DataType.EmailAddress)]
        [StringLength(20)]
        public string Email { get; set; }

        [Display(Name = "备注")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Display(Name="状态")]
        public SailorStatus Status { get; set; }

        [Display(Name = "所在船舶")]
        public int? VesselID { get; set; }

        [Display(Name = "所在船舶")]
        public string VesselName { get; set; }

        public int? FileID { get; set; }

        [Display(Name = "服务记录")]
        public int? ServiceRecordID { get; set; }

        [Display(Name = "培训记录")]
        public int? TraineeID { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Family> Families { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual ICollection<ServiceRecord> ServiceRecords { get; set; }
        public virtual ICollection<Title> Titles { get; set; }
        public virtual ICollection<Trainee> Trainees { get; set; }
        public virtual Vessel Vessel { get; set; }

        [StringLength(50)]
        public string SysUserId { get; set; }
        public int SysCompanyId { get; set; }
    }
}