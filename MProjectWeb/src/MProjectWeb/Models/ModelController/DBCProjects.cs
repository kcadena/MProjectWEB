using MProjectWeb.Models.postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MProjectWeb.Models.ModelController
{
    public class DBCProjects
    {
        MProjectContext db;
        public DBCProjects()
        {
            db = new MProjectContext();
        }
        public List<ProjectsUsers> listProjectsUsers(long id_usu)
        {
            try
            {
                var dat = from pr in db.proyectos
                          join car in db.caracteristicas on pr.id_caracteristica equals car.id_caracteristica
                          
                          where pr.id_usuario == id_usu

                          select new ProjectsUsers()
                          {
                              id_pro_car=pr.id_caracteristica,
                              id_usu = pr.id_usuario,
                              id_pro = pr.id_proyecto,
                              keym=car.keym,
                              desc =pr.descripcion
                          };

                if (dat.Count() > 0)
                    return dat.ToList<ProjectsUsers>();
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
    public class ProjectsUsers
    {
        public long? id_pro { get; set; }
        public long? id_pro_car { get; set; }
        public long? id_usu { get; set; }
        public string keym { get; set; }
        public string desc { get; set; }
        public string valor { get; set; }

    }
}

