using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorDomain.Entities
{
    public interface IEntity
    {
        string SysUserId { get; set; }
        int SysCompanyId { get; set; }
    }
}
