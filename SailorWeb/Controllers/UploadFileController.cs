using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SailorWeb.Models;
using SailorDomain.Entities;
using SailorWeb.Services;

namespace SailorWeb.Controllers
{
    public class UploadFileController : Controller
    {
        readonly IUploadFileService _uploadFileService;
        public UploadFileController(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }

        public ActionResult GetFile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFile uploadfile = _uploadFileService.Find(id);
            if (uploadfile == null)
            {
                return HttpNotFound();
            }
            string diskPath = HttpContext.Server.MapPath(uploadfile.Path);
            string contentType = MimeMapping.GetMimeMapping(uploadfile.Name);
            return File(diskPath, contentType);
        }
        public FileResult DownloadFile(int? id)
        {
            UploadFile uploadfile = _uploadFileService.Find(id);
            string diskPath = HttpContext.Server.MapPath(uploadfile.Path);
            string contentType = MimeMapping.GetMimeMapping(uploadfile.Name);
            return File(diskPath, contentType, Url.Encode(uploadfile.Name));
        }

        public string GetFileType(int? id)
        {
            if (id == null)
            {
                return "";
            }
            UploadFile uploadfile = _uploadFileService.Find(id);
            return MimeMapping.GetMimeMapping(uploadfile.Name);
        }
    }
}
