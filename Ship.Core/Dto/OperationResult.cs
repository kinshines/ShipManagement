using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Core.Dto
{
    public class OperationResult
    {
        public OperationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public string Message { get; set; }

        public bool IsSuccess { get; private set; }
    }
}
