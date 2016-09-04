using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using MProjectWeb.Models.ModelController;
using MProjectWeb.ViewModels;
using Newtonsoft.Json;


using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

using MProjectWeb.LuceneIR;


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
                long user = Convert.ToInt64(HttpContext.Session.GetString("idUsu"));
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
            long user = Convert.ToInt64(HttpContext.Session.GetString("idUsu"));
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
            string x = dat["id"];
            
            ViewBag.id_prj = x;

            HttpContext.Session.SetString("id_prj", x);
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

            try
            {
                string ax = HttpContext.Session.GetString("id_prj");
                string[] prj = ax.Split('-'); //[0]=>keym   [1]=>idCarProject
                long idUsu = Convert.ToInt64(HttpContext.Session.GetString("idUsu"));
                string keym = prj[0];
                ViewBag.id_prj = ax;
                try
                {

                    dynamic dat = new JsonArrayAttribute();
                    dat = Request.Form;
                    long idCar = Convert.ToInt64(dat["id_car"]);
                    HttpContext.Session.SetString("par_car", idCar.ToString());
                    int op = Convert.ToInt32(dat["opt"]);


                    DBCActivities act = new DBCActivities();
                    List<ActivityList> act_lst = act.getActivityList(idCar, idUsu, keym, op);
                    ViewBag.act_lst = act_lst;
                    try
                    {
                        ViewBag.par = act_lst.First().parCar;
                    }
                    catch
                    {
                        return Content("0");
                    }
                    ViewBag.prj = Convert.ToInt64(prj[1]);
                }
                catch
                {
                    long idCar = Convert.ToInt64(prj[1]);// prj   -->   [0]=>keym   [1]=>idCarProject
                    DBCActivities act = new DBCActivities();
                    List<ActivityList> act_lst = act.getActivityList(idCar, idUsu, keym, 1);
                    ViewBag.act_lst = act_lst;
                }
            }
            catch
            {
                ViewBag.id_prj = null;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Files()
        {
            try
            {
                dynamic dat = Request.Form;

                ViewBag.idCar = dat["idCar"];
                ViewBag.idUsu = dat["idUsu"];
                ViewBag.keym = dat["keym"];
                ViewBag.type = dat["type"];

                LuceneAct lc = new LuceneAct();
                List<Lucene.Net.Search.ScoreDoc> lstDoc = lc.search(dat["text"], dat["type"], "");
                Lucene.Net.Documents.Document doc = lc.searcher.Doc(lstDoc.ElementAt(0).Doc);
                ViewBag.lstDoc = lstDoc;
                ViewBag.searcher = lc.searcher;
                if(lc.totSear>0)
                    ViewBag.st = true;
                else
                    ViewBag.st = false;
            }
            catch
            {
                ViewBag.st = false;
            }
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
