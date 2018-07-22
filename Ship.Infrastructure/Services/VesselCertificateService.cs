﻿using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class VesselCertificateService : AuthorizeBaseService<VesselCertificate>
    {
        public VesselCertificateService(DefaultDbContext cxt, ILogger<VesselCertificateService> logger) : base(cxt, logger)
        {
        }
    }
}