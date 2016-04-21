using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;


using MProjectWeb.Models.Sqlite;
using MProjectWeb.Models.DBControllers;

using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MProjectWeb.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            HttpContext.Session.SetString("op", webOptions().ToString());
            return View();
        }
        public IActionResult Projects()
        {
            return View();
        }
        private JObject webOptions()
        {
            /*
            rol
            1-  default
            2-  Admin
            */

            WebData wd = new WebData();
            return wd.defaultUser();
        }
    }
}
