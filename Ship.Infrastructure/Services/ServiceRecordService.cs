using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class ServiceRecordService : AuthorizeBaseService<ServiceRecord>
    {
        public ServiceRecordService(DefaultDbContext cxt, ILogger<ServiceRecordService> logger) : base(cxt, logger)
        {
        }
    }
}
