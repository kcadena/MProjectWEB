﻿using MProjectWeb.Models.postgres;
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
                //var dat = from pr in db.proyectos
                //          join car in db.caracteristicas on pr.id_caracteristica equals car.id_caracteristica

                //          where pr.id_usuario == id_usu

                //          select new ProjectsUsers()
                //          {
                //              id_pro_car=pr.id_caracteristica,
                //              id_usu = pr.id_usuario,
                //              id_pro = pr.id_proyecto,
                //              keym=car.keym,
                //              desc =pr.descripcion
                //          };
                var dat = db.proyectos.Where(x => x.id_usuario == id_usu).Select(x =>
                         new ProjectsUsers()
                         {
                             id_car = x.id_caracteristica,
                             id_usu = x.id_usuario_car,
                             id_pro = x.id_proyecto,
                             keym = x.keym,
                             desc = x.descripcion
                         }
                    );

                if (dat.Count() > 0)
                    return dat.ToList<ProjectsUsers>();
            }
            catch
            {
                return null;
            }
            return null;
        }
        public List<ProjectsUsers> listPublicProjectsUsers()
        {
            try
            {
                var dat = db.proyectos.Where(x => x.caracteristicas.publicacion_web == true).Select(x =>
                    new ProjectsUsers()
                    {
                        id_car = x.id_caracteristica,
                        id_usu = x.id_usuario_car,
                        keym = x.keym,
                        desc = x.descripcion,
                        ruta = x.id_usuarioNavigation.repositorios_usuarios.ruta_repositorio +
                        x.nombre
                        + ".html"
                    }
                    );
                //var dat = db.proyectos.Where(x =>x.caracteristicas.publicacion_web==true).Select(x =>
                //         new ProjectsUsers()
                //         {
                //             id_car = x.id_caracteristica,
                //             id_usu = x.id_usuario_car,
                //             id_pro = x.id_proyecto,
                //             keym = x.keym,
                //             desc = x.descripcion
                //         }
                //    );

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
        public long? id_car { get; set; }
        public long? id_usu { get; set; }
        public long keym { get; set; }
        public string desc { get; set; }
        public string valor { get; set; }
        public string ruta{ get; set; }

    }
}

