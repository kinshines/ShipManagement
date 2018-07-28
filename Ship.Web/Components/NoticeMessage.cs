using Microsoft.AspNetCore.Mvc;
using Ship.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ship.Web.Components
{
    public class NoticeMessage:ViewComponent
    {
        readonly NoticeService _noticeService;
        public NoticeMessage(NoticeService noticeService)
        {
            _noticeService = noticeService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_noticeService.GetMessage().ToList());
        }
    }
}
