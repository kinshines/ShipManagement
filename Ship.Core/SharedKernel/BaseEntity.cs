using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ship.Core.SharedKernel
{
    public abstract class BaseEntity
    {
        [StringLength(50)]
        public string SysUserId { get; set; }
        public int SysCompanyId { get; set; }
    }
}
