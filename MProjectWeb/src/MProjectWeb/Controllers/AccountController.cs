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
using System.Net.Mail;
using System.Net;



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
                DBCUsuarios dbUsu = new DBCUsuarios();
                usuarios usu = new usuarios();

                usu.nombre = q.nombre;
                usu.apellido = q.apellido;
                usu.e_mail = q.e_mail;
                usu.pass = q.pass;
                usu.genero = q.genero;
                usu.entidad = q.entidad;
                usu.cargo = q.cargo;
                usu.telefono = q.telefono;
                usu.administrador = false;
                int i = 0;
                bool st = false;
                try
                {
                   for( i = 0; i < 5; i++)
                    {
                        usu.id_usuario = dbUsu.regUser(usu);
                        if (usu.id_usuario != -1)
                        {
                            i = 5;
                            st = true;
                        }
                    }
                }
                catch
                {
                    q.aux = true;
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction("Index", "Index", q);
                }
                
                if(!st)
                {
                    q.aux = true;
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    TempData["errReg"] = true;
                    return RedirectToAction("Index", "Index");
                }
                else
                {
                    try
                    {
                        string cont = "Bienvenido: para confirmar su registro a MProject por favor ingrese a: <br>" +
                            "<a href='http://localhost:5000/account/userActivate?id=" + usu.id_usuario + "'>Confirmar</a>";
                        sendEmail(usu.e_mail,cont,"Confirmacion MProject");

                        HttpContext.Session.SetString("estReg", "true");
                    }
                    catch {
                        HttpContext.Session.SetString("estReg", "false");
                    }
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                q.aux = true;
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //TempData["errReg"] = true;
                //return RedirectToAction("Index", "Index");
                //q.aux = true;
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //return RedirectToAction("Index", "Index", q);
                return View(q);
            }
        }

        public IActionResult userActivate(long id)
        {
            DBCUsuarios us = new DBCUsuarios();
            try
            {
                usuarios q = us.userActivate(id);
                HttpContext.Session.SetString("UsuNam", q.nombre + " " + q.apellido);
                HttpContext.Session.SetString("idUsu", q.id_usuario.ToString());
                return RedirectToAction("Index","Projects");
            }
            catch
            {
                //HttpContext.Session.SetString("estReg", "false");
                return RedirectToAction("Index", "Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> fogetPassword(string email)
        {
            DBCUsuarios usr = new DBCUsuarios();
            string pass = usr.forgetPassword(email.ToString());
            string cont="Apreciado/a Su nueva clave para MProject es:   "+pass;
            sendEmail(email.ToString(),cont,"Recuperacion clave MProject");
            HttpContext.Session.SetString("estPass", "true");
            return Redirect("/Index/Index");
        }


        //Metodos auxiliares

        //envio de correo electronico
        private async void  sendEmail(string email,string content,string subject)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("aslan310593@hotmail.com");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = content;
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("aslan310593@hotmail.com", "310593LIVE");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
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
