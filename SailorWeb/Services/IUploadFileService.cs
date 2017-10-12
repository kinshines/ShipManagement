using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public interface IUploadFileService:IBaseService<UploadFile>
    {
        int? AddFile(HttpPostedFileBase file);
        int? AddFile(HttpPostedFileBase file,int? existId);
        bool Delete(int? fileId, bool isSave = true);
    }
}
