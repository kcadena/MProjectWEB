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
        public IActionResult Index(string p)
        {
            //MProjectDeskSQLITEContext dbMP = new MProjectDeskSQLITEContext();

            ViewBag.errLogin = false;
            try
            {
                bool st = (bool)TempData["err"];
                ViewBag.errLogin = st;
            }
            catch { }
            MProjectContext dbMP = new MProjectContext();
            
            ViewData["Title"] = "Mproject";
            

            try
            {
                string[] pro = p.Split('-');
                caracteristicas car = (from x in dbMP.caracteristicas
                                       where x.keym==pro[0] &&
                                       x.id_usuario== Convert.ToInt64(pro[2]) &&
                                       x.id_caracteristica == Convert.ToInt64(pro[1])
                                       select x).First();
                ViewBag.key = car.keym;
                ViewBag.idCar = car.id_caracteristica;
                ViewBag.idUsu = car.id_usuario;
                //ViewBag.Pagina = car.id_usuarioNavigation.repositorios_usuarios.ruta_repositorio+car.proyectos.First().nombre.ToLower().Replace(" ","")+".html";//ruta 
                ViewBag.Pagina = "http://172.16.10.248/prueba%20web/principal1.html";
            }
            catch {

            }
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