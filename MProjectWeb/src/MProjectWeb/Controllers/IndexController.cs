using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MProjectWeb.Models.postgres;
using Microsoft.AspNet.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MProjectWeb.Controllers
{
    public class IndexController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //MProjectDeskSQLITEContext dbMP = new MProjectDeskSQLITEContext();

            ViewBag.errLogin = false;
            try
            {
                bool st = (bool)TempData["err"];
                ViewBag.errLogin = st;
            }
            catch { }
            
            
            ViewData["Title"] = "Mproject";

           
            //if (project==1)
            //    ViewBag.Pagina = "http://localhost:60395/prueba%20web/principal1.html";
            //else if (project == 2)
            //    ViewBag.Pagina = "http://localhost:60395/prueba%20web/principal2.html";
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Index");
        }
        
    }
}