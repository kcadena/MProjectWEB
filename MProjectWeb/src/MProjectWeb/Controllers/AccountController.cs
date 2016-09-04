using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;

using MProjectWeb.Models.postgres;
using MProjectWeb.Models.ModelController;

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

    public class AccountController : Controller
    {
        protected DBCUsuarios dbu;
        private MProjectContext db = new MProjectContext();
        public AccountController()
        {
            dbu = new DBCUsuarios();
        }
        [HttpPost]
        public IActionResult Login(usuarios q)
        {
            Dictionary<string, string> dat = new Dictionary<string, string>();
            dat["email"] = q.e_mail;
            dat["pass"] = q.pass;
            q = dbu.loginUsuario(dat);
            if (q != null)
            {
                HttpContext.Session.SetString("UsuNam", q.nombre + " " + q.apellido);
                HttpContext.Session.SetString("idUsu", q.id_usuario.ToString());
                return RedirectToAction("Index", "Projects");
            }
            TempData["err"] = true;
            return RedirectToAction("Index", "Index");
        }

        [HttpPost]
        public IActionResult Register(ViewModels.usuarios q)
        {
            if (ModelState.IsValid)
            {
                usuarios usu = new usuarios();

                usu.apellido = q.apellido;
                usu.nombre = q.nombre;
                usu.e_mail = q.e_mail;
                usu.pass = q.pass;
                usu.id_usuario =(int) q.id_usuario;

                try
                {
                    db.usuarios.Add(usu);
                    db.SaveChanges();
                }
                catch
                {
                    q.aux = true;
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction("Index", "Index", q);
                }

                HttpContext.Session.SetString("UsuNam", q.nombre + " " + q.apellido);
                HttpContext.Session.SetString("idUsu", q.id_usuario.ToString());
                return RedirectToAction("Index", "Projects");
            }
            else
            {
                q.aux = true;
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return RedirectToAction("Index", "Index", q);
            }
        }

       
    }
}






/*

            //MProjectDeskSQLITEContext db = new MProjectDeskSQLITEContext();
            //ViewData["ReturnUrl"] = "Projects/Projects";

            //var a= db.actividades.First();
            //ViewBag.nom = a.nombre.ToString();

            //HttpContext.Session.SetString("user","kelvin");
            // GET: /<controller>/

    ======================================================================================
            //var result = new ViewResult()
            //{
            //    ViewName = "~/Views/Account/Register"
            //};
            //return result;

    

    =======================================================================================
 //[HttpGet]
        //[ValidateAntiForgeryToken]
        //public IActionResult Register(usuarios q)
        //{
        //    if (q.id_usuario == 1)
        //    {
        //        return View();
        //    }
        //    else {
        //        var s = new ViewResult()
        //        {
        //            ViewName = "~/Views/Account/Login"
        //        };
        //        return s;
        //    }
        //}

    */
