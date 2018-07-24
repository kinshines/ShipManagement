using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Core.Enums;
using Ship.Infrastructure.Services;
using X.PagedList;

namespace Ship.Web.Controllers
{
    public class NoticeController : Controller
    {
        readonly NoticeService _noticeService;
        private ILogger<NoticeController> logger;

        public NoticeController(NoticeService noticeService,ILogger<NoticeController> logger)
        {
            _noticeService = noticeService;
            this.logger = logger;
        }

        // GET: /Notice/
        public ActionResult Index(string Content, int? Source, int? page)
        {
            var query = _noticeService.GetEntities().Where(n => n.NoticeTime <= DateTime.Now && n.Active);
            if (!String.IsNullOrWhiteSpace(Content))
            {
                query = query.Where(n => n.Content.Contains(Content));
            }
            if (Source.HasValue)
            {
                query = query.Where(q => q.Source == (NoticeSource)Source);
            }
            query = query.OrderBy(n => n.NoticeTime);

            ViewBag.Content = Content;
            var sources = from NoticeSourceChinese s in Enum.GetValues(typeof(NoticeSourceChinese))
                          select new { ID = (int)s, Name = s.ToString() };
            ViewBag.Source = new SelectList(sources, "ID", "Name", Source);
            ViewBag.SelectedSource = Source;
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Notice/Delete/5
        public ActionResult Delete(int? id)
        {
            return PartialView();
        }

        // POST: /Notice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _noticeService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NoticeHandle(NoticeHandle handle)
        {
            if (ModelState.IsValid)
            {
                _noticeService.NoticeHandle(handle);
                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        [ResponseCache(Duration = 3600)]
        public PartialViewResult Message()
        {
            return PartialView(_noticeService.GetMessage().ToList());
        }

    }
}