using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MProjectWeb.Models.postgres;

namespace MProjectWeb.Models.ModelController
{
    public class DBCUsuarios
    {
        MProjectContext db;
        public DBCUsuarios()
        {
            db = new MProjectContext();
        }

        //Inicio de cesion del usuario => disponible: identifica si esta activo o no
        public usuarios loginUsuario(Dictionary<string, string> dic)
        {
            try
            {
                usuarios dat = (from x in db.usuarios
                                where x.e_mail == dic["email"] && x.pass == dic["pass"] && x.disponible==true
                                select x).First();
                return dat;
            }
            catch
            {
                return null;
            }
        }



        //Otiene el id del usuario libre para luego ser asignado al siguiente
        public long getFreeIdUser()
        {
            try
            {
                long id = db.usuarios.OrderByDescending(x => x.id_usuario).First().id_usuario;
                return id + 1;
            }
            catch
            {
                return -1;
            }
        }
        //registrar usuario
        public long regUser(usuarios usu)
        {
            try
            {
                long id = getFreeIdUser();
                if (id != -1)
                {
                    usu.id_usuario = id;
                    db.usuarios.Add(usu);
                    db.SaveChanges();
                    return id;
                }
                else
                    return -1;
                
            }
            catch
            {
                return -1;
            }
        }

        //Activa el registro hecho por el usuario => necesario para poder ingresar a la plataforma
        public usuarios userActivate(long id)
        {
            try
            {
                var usr = db.usuarios.Where(x => x.id_usuario == id).First();
                usr.disponible = true;
                db.SaveChanges();
                return usr;
            }
            catch { return null; }
        }

        //Genera y asigna nueva clave al usuario 
        public string forgetPassword(string email)
        {
            try
            {
                string pass = newRandomPassword();
                var usr = db.usuarios.Where(x => x.e_mail == email && x.disponible == true).First();
                usr.pass = pass;
                db.SaveChanges();
                return pass;
            }
            catch
            {
                return "err";
            }
        }

        //crea clave aleatoria
        private string newRandomPassword()
        {
            Random obj = new Random();
            string opc = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int length = opc.Length;
            char letra;
            int cadLength = 30;
            string newCad = "";
            for (int i = 0; i < cadLength; i++)
            {
                letra = opc[obj.Next(length)];
                newCad += letra.ToString();
            }
            return newCad;
        }
    }
}
