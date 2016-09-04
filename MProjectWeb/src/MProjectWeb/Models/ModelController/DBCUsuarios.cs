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
        public usuarios loginUsuario(Dictionary<string, string> dic)
        {
            try
            {
                usuarios dat = (from x in db.usuarios
                                where x.e_mail == dic["email"] && x.pass == dic["pass"]
                                select x).First();
                return dat;
            }
            catch
            {
                return null;
            }
        }
    }
}
