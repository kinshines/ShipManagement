﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorDomain.Entities;
using SailorDomain.Services;

namespace SailorWeb.Services
{
    public class ExperienceService : AuthorizeBaseService<Experience>, IExperienceService
    {
    }
}