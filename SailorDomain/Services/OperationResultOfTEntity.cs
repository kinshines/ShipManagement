using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorDomain.Services
{
    public class OperationResult<TEntity>:OperationResult
    {
        public OperationResult(bool isSuccess)
            : base(isSuccess) { }

        public TEntity Entity { get; set; }
    }
}
