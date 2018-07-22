using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class UploadFileService : AuthorizeBaseService<UploadFile>
    {
        private readonly IHostingEnvironment env;
        public UploadFileService(DefaultDbContext cxt, ILogger<UploadFileService> logger, IHostingEnvironment env) : base(cxt, logger)
        {
            this.env = env;
        }
        public int? AddFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                string filePath = "/Files/" + DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                string diskPath = env.ContentRootFileProvider.GetFileInfo(filePath).PhysicalPath;
                using (var stream = new FileStream(diskPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var uploadfile = new UploadFile()
                {
                    Name = file.FileName,
                    Path = filePath,
                    CreateTime = DateTime.Now,
                    SysUserId = SysUserId,
                    SysCompanyId = SysCompanyId
                };
                uploadfile = Add(uploadfile);
                return uploadfile.UploadFileID;
            }
            else
            {
                return null;
            }
        }
        public int? AddFile(IFormFile file, int? existId)
        {
            if (file.Length == 0)
                return existId;
            if (existId.HasValue)
            {
                Delete(existId, false);
            }
            return AddFile(file);
        }

        public bool Delete(int? fileId, bool isSave = true)
        {
            var file = Find(fileId);
            if (file == null)
                return true;
            string oldDiskPath = env.ContentRootFileProvider.GetFileInfo(file.Path).PhysicalPath;
            if (File.Exists(oldDiskPath))
            {
                File.Delete(oldDiskPath);
            }
            return Delete(file, isSave);
        }
    }
}
