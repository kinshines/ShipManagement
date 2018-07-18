using Ship.Core.SharedKernel;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class AuthorizeBaseService<T> : BaseService<T> where T : BaseEntity
    {
        public AuthorizeBaseService(DefaultDbContext cxt):base(cxt)
        {
        }

    }
}
