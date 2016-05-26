﻿using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using MProjectWeb.Models.DBControllers;
using MProjectWeb.ViewModels;
using Newtonsoft.Json;

using MProjectWeb.Models.Sqlite;

using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace MProjectWeb.Controllers
{
    public class MultiMediaController : Controller
    {
        public IActionResult Pictures()
        {
            return View();
        }
        public IActionResult Videos()
        {
            return View();
        }
    }
}