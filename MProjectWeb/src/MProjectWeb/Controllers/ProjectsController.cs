using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MProjectWeb.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            
            DBCProjects h = new DBCProjects();
            try
            {
                long user = Convert.ToInt64(HttpContext.Session.GetString("UsuID"));
                var x = h.listProjectsUsers(user);
                //  TempData["prj"] = x;
            }
            catch { }
            HttpContext.Session.SetString("op", webOptions().ToString());
            return View();
        }
        public IActionResult Projects()
        {
            HttpContext.Session.Remove("id_prj");
            DBCProjects h = new DBCProjects();
            long user = Convert.ToInt64(HttpContext.Session.GetString("UsuID"));
            ViewBag.projects = h.listProjectsUsers(user);
            return View();
        }

        //==========================================       VIEWS HELP       ===============================================//
        [HttpPost]
        public IActionResult PanelProject()
        {
            //var s = JObject.Parse(this.Request.Form.ElementAt(0).Key);
            dynamic dat = Request.Form;
            //var s = json.GetValue("id");
            //var ds = json.GetValue("id");
            long id = Convert.ToInt64(dat["id"]);
            ViewBag.id_prj = id.ToString();
            HttpContext.Session.SetString("id_prj",id.ToString());
            return View();
        }

        [HttpPost]
        public IActionResult ActMoreInfo()
        {
            dynamic dat = Request.Form;
            long id = Convert.ToInt64(dat["id_car"]);
            ViewBag.id_car = id;
            return View();
        }

        //==========================================   VISTAS SUBOPCIONES   ===============================================//
        public IActionResult Activity()
        {
            ViewBag.id_prj = HttpContext.Session.GetString("id_prj");
            if (HttpContext.Session.GetString("id_prj")!=null)
            {
                long id = Convert.ToInt64(HttpContext.Session.GetString("id_prj"));
                DBCActivities act = new DBCActivities();
                List<ActivityList> act_lst = act.activityList(id);
                ViewBag.act_lst = act_lst;
            }
            return View();
        }

        public IActionResult Files()
        {
            return View();
        }
        public IActionResult Georeference()
        {
            return View();
        }
        public IActionResult Charts()
        {
            return View();
        }
        public IActionResult Olders()
        {
            return View();
        }
        public IActionResult Publishes()
        {
            return View();
        }
        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }


        //=====================================   METODOS/FUNCIONES AUXILIARES   ==========================================//
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
