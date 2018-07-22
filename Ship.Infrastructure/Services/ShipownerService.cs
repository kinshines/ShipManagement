using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class ShipownerService : AuthorizeBaseService<Shipowner>
    {
        public ShipownerService(DefaultDbContext cxt, ILogger<ShipownerService> logger) : base(cxt, logger)
        {
        }
    }
}
