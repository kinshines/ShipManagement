using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class InterviewService: AuthorizeBaseService<Interview>
    {
        public InterviewService(DefaultDbContext cxt,ILogger<InterviewService> logger) :base(cxt, logger)
        {
        }
    }
}
