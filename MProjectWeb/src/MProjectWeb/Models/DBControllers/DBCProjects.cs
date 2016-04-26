using MProjectWeb.Models.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MProjectWeb.Models.DBControllers
{
    public class DBCProjects
    {
        MProjectDeskSQLITEContext db;
        public DBCProjects()
        {
            db = new MProjectDeskSQLITEContext();
        }
        public List<ProjectsUsers> listProjectsUsers(long id_usu)
        {
            try
            {
                var dat = from u in db.usuarios_proyectos
                          join up in db.proyectos on u.id_proyecto equals up.id_proyecto
                          join pm in db.proyectos_meta_datos on up.id_proyecto equals pm.id_proyecto
                          join plm in db.plantillas_meta_datos on pm.id_plantilla_meta_dato equals plm.id_plantilla_meta_dato
                          join md in db.meta_datos on plm.id_meta_datos equals md.id_meta_datos
                          where u.id_usuario == id_usu

                          select new ProjectsUsers()
                          {
                              id_usu = u.id_usuario,
                              id_pro = up.id_proyecto,
                              desc = md.descripcion,
                              valor = pm.valor
                          };


                return dat.ToList<ProjectsUsers>();
            }
            catch
            {
                return null;
            }
        }
    }
    public class ProjectsUsers
    {
        public long? id_pro { get; set; }
        public long? id_usu { get; set; }
        public string desc { get; set; }
        public string valor { get; set; }

    }
}

