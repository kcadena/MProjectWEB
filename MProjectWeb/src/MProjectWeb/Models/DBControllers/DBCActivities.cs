using MProjectWeb.Models.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MProjectWeb.ViewModels;
using System.Data.Common;

namespace MProjectWeb.Models.DBControllers
{
    public class DBCActivities
    {

        MProjectDeskSQLITEContext db;
        public DBCActivities()
        {
            db = new MProjectDeskSQLITEContext();
        }

        public List<ActivityList> activityList(long id)
        {
            /* select 	
	actividades.id_actividad,
	actividades.nombre,
	actividades.descripcion,
	folders.id_folder,
	folders.nombre,
	caracteristicas.estado,
	caracteristicas.id_caracteristica
from 	
	actividades 	join caracteristicas on caracteristicas.id_actividad=actividades.id_actividad 
	left join folders on actividades.id_folder = folders.id_folder
	where caracteristicas.proyecto_padre = 1
	;
            */
            try
            {
                var dat = from act in db.actividades
                          join car in db.caracteristicas on act.id_actividad equals car.id_actividad
                          join fol in db.folders on act.id_folder equals fol.id_folder into pp

                          from fol in pp.DefaultIfEmpty()
                           
                          select 
                          new ActivityList
                          {
                              id_act = (long)act.id_actividad,
                              //id_fol =  (act.id_folder),
                              id_characteristic = (long)car.id_caracteristica,
                              description = act.descripcion,
                              name = act.nombre,
                              folder = fol == null ? "NULL": "OK",
                              state = car.estado
                          };
               

                return dat.ToList<ActivityList>();
                         
            }
            catch { return null; }
            
        }

    }

    

}
