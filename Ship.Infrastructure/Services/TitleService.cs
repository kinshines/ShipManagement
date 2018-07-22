using Microsoft.Extensions.Logging;
using Ship.Core.Entities;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class TitleService : AuthorizeBaseService<Title>
    {
        public TitleService(DefaultDbContext cxt, ILogger<TitleService> logger) : base(cxt, logger)
        {
        }
    }
}
