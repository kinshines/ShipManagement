using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SailorDomain.Entities
{
    public class UploadFile:IEntity
    {
        public int UploadFileID { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Path { get; set; }
        public DateTime CreateTime { get; set; }

        [StringLength(50)]
        public string SysUserId { get; set; }
        public int SysCompanyId { get; set; }
    }
}