using Ship.Core.Enums;
using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    /// <summary>
    /// 消息提醒
    /// </summary>
    public class Notice : BaseEntity
    {
        public int NoticeID { get; set; }

        [Display(Name = "消息来源")]
        public NoticeSource Source { get; set; }

        [Display(Name = "消息来源索引")]
        public int SourceID { get; set; }

        [Display(Name = "提醒时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NoticeTime { get; set; }

        [Display(Name = "截止日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Deadline { get; set; }

        [Display(Name = "消息内容")]
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        [Display(Name = "是否激活")]
        public bool Active { get; set; }
    }

    public class NoticeHandle
    {
        public int NoticeID { get; set; }
        [Display(Name = "处理提醒")]
        public NoticeHandleType HandleType { get; set; }
        [Display(Name = "推迟天数")]
        [Range(0, Int32.MaxValue)]
        public int? DelayDays { get; set; }
    }
}
