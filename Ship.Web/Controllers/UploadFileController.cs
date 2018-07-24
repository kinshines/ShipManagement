using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Ship.Core.Entities;
using Ship.Infrastructure.Services;

namespace Ship.Web.Controllers
{
    public class UploadFileController : Controller
    {
        readonly UploadFileService _uploadFileService;
        readonly IHostingEnvironment _env;
        public UploadFileController(UploadFileService uploadFileService, IHostingEnvironment env)
        {
            _uploadFileService = uploadFileService;
            _env = env;
        }

        public ActionResult GetFile(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            UploadFile uploadfile = _uploadFileService.Find(id);
            if (uploadfile == null)
            {
                return NotFound();
            }
            string diskPath = _env.ContentRootFileProvider.GetFileInfo(uploadfile.Path).PhysicalPath;
            var provider = new FileExtensionContentTypeProvider();
            var ext = System.IO.Path.GetExtension(diskPath);
            string contentType = provider.Mappings[ext];
            return File(diskPath, contentType);
        }
        public FileResult DownloadFile(int? id)
        {
            UploadFile uploadfile = _uploadFileService.Find(id);
            string diskPath = _env.ContentRootFileProvider.GetFileInfo(uploadfile.Path).PhysicalPath;
            var provider = new FileExtensionContentTypeProvider();
            var ext = System.IO.Path.GetExtension(diskPath);
            string contentType = provider.Mappings[ext];
            return File(diskPath, contentType, WebUtility.UrlEncode(uploadfile.Name));
        }

        public string GetFileType(int? id)
        {
            if (id == null)
            {
                return "";
            }
            UploadFile uploadfile = _uploadFileService.Find(id);
            var ext = System.IO.Path.GetExtension(uploadfile.Name);
            var provider = new FileExtensionContentTypeProvider();
            return provider.Mappings[ext];
        }
    }
}