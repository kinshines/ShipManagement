using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using SailorDomain.Entities;

namespace SailorWeb.Services
{
    public class UploadFileService : AuthorizeBaseService<UploadFile>, IUploadFileService
    {
        public int? AddFile(HttpPostedFileBase file)
        {
            if (file.ContentLength != 0)
            {
                string filePath = "~/Files/" + DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                string diskPath = HttpContext.Current.Server.MapPath(filePath);
                file.SaveAs(diskPath);
                var uploadfile = new UploadFile()
                {
                    Name = file.FileName,
                    Path = filePath,
                    CreateTime=DateTime.Now,
                    SysUserId=SysUserId,
                    SysCompanyId=SysCompanyId
                };
                uploadfile = Add(uploadfile);
                return uploadfile.UploadFileID;
            }
            else
            {
                return null;
            }
        }
        public int? AddFile(HttpPostedFileBase file, int? existId)
        {
            if (file.ContentLength == 0)
                return existId;
            if (existId.HasValue)
            {
                Delete(existId, false);
            }
            return AddFile(file);
        }

        public bool Delete(int? fileId,bool isSave=true)
        {
            var file = Find(fileId);
            if (file == null)
                return true;
            string oldDiskPath = HttpContext.Current.Server.MapPath(file.Path);
            if (System.IO.File.Exists(oldDiskPath))
            {
                System.IO.File.Delete(oldDiskPath);
            }
            return Delete(file, isSave);
        }
    }
}