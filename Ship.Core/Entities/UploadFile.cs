using Ship.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.Entities
{
    public class UploadFile : BaseEntity
    {
        public int UploadFileID { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Path { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
