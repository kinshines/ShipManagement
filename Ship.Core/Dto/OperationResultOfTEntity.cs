using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Core.Dto
{
    public class OperationResult<TEntity> : OperationResult
    {
        public OperationResult(bool isSuccess)
            : base(isSuccess) { }

        public TEntity Entity { get; set; }
    }
}
