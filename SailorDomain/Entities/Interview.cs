using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SailorDomain.Entities
{
    /// <summary>
    /// 面试
    /// </summary>
    public class Interview:IEntity
    {
        public int InterviewID { get; set; }

        [Display(Name = "面试职务")]
        public Post Post { get; set; }

        [Display(Name = "英语水平")]
        public EnglishLevel? EnglishLevel { get; set; }

        [Display(Name = "听")]
        public InterviewScore? Listening { get; set; }

        [Display(Name = "说")]
        public InterviewScore? Speaking { get; set; }

        [Display(Name = "读")]
        public InterviewScore? Reading { get; set; }

        [Display(Name = "写")]
        public InterviewScore? Writing { get; set; }

        [Display(Name = "专业技能")]
        public InterviewScore? Expertise { get; set; }

        [Display(Name = "航海资历")]
        public InterviewScore? Qualification { get; set; }

        [Display(Name = "应变能力")]
        public InterviewScore? EmergencyHandle { get; set; }

        [Display(Name = "服务意识")]
        public InterviewScore? ServiceAwareness { get; set; }

        [Display(Name = "健康状况")]
        public InterviewScore? Health { get; set; }

        [Display(Name = "管理技能")]
        public InterviewScore? Management { get; set; }

        [Display(Name = "SMS运行能力")]
        public InterviewScore? SmsOperation { get; set; }

        [Display(Name = "其他内容")]
        [StringLength(100)]
        public string Other { get; set; }

        [Display(Name = "面试成绩")]
        [Required]
        [StringLength(10)]
        public string InterviewScore { get; set; }

        [Display(Name = "面试结论")]
        public InterviewConclusion Conclusion { get; set; }

        [Display(Name = "面试评语")]
        [Required]
        [StringLength(100)]
        public string Comment { get; set; }

        [Display(Name = "面试人")]
        [Required]
        [StringLength(10)]
        public string Interviewer { get; set; }

        [Display(Name = "面试日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? InterviewDate { get; set; }

        [Display(Name = "面试地点")]
        [StringLength(10)]
        public string InterviewPlace { get; set; }

        [Display(Name = "专业考试成绩")]
        [StringLength(10)]
        public string ProfessionalScore { get; set; }

        [Display(Name = "综合评定成绩")]
        [StringLength(10)]
        public string ComprehensiveScore { get; set; }

        [Display(Name = "综合评定")]
        [StringLength(10)]
        public string ComprehensiveAssessment { get; set; }

        [Display(Name = "船员要求")]
        [StringLength(100)]
        public string SailorRequirement { get; set; }

        [Display(Name="船员姓名")]
        public int SailorID { get; set; }

        [Display(Name = "船员姓名")]
        [StringLength(10)]
        public string SailorName { get; set; }
        public Sailor Sailor { get; set; }

        [StringLength(50)]
        public string SysUserId { get; set; }
        public int SysCompanyId { get; set; }
    }
}