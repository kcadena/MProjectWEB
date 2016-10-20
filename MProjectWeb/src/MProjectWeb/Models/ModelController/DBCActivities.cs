using MProjectWeb.Models.postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MProjectWeb.ViewModels;
using System.Data.Common;

namespace MProjectWeb.Models.ModelController
{
    public class DBCActivities
    {

        MProjectContext db;
        public List<string> llink = null;
        public DBCActivities()
        {
            db = new MProjectContext();
        }
        //Obtiene el usuario asignado a una actividad (caracteristica) espesifica
        public long getIdOwnActivity(long keym, long usu, long car)
        {
            caracteristicas ax = db.caracteristicas.Where(x =>
                x.keym == keym &&
                x.id_caracteristica == car &&
                x.id_usuario == usu
            ).First();
            long idUsu = ax.id_usuario;

            try
            {
                while (ax.usuario_asignado == null)
                {
                    ax = db.caracteristicas.Where(x => x.id_caracteristica == ax.id_caracteristica_padre).First();
                }
                return (int)ax.usuario_asignado;
            }
            catch { return idUsu; }
        }
        //Obtiene los links de las actividades inclido el boton regresar
        public List<ActivityList> getActivityList(long idCar, long idUsu, long keym, int op)
        {
            //op opcion  1=>actividades  2=>back
            try
            {
                if (op == 1)
                {
                    List<ActivityList> dat = search(idCar, idUsu, keym);
                    return dat;
                }
                else if (op == 2)
                {
                    long idPar = (long)db.caracteristicas.Where(x =>
                        x.id_caracteristica == idCar &&
                        x.keym == keym &&
                        x.id_usuario == idUsu
                    ).Select(x => x.id_caracteristica_padre).First();
                    List<ActivityList> dat = search(idPar, idUsu, keym);
                    return dat;
                }
            }
            catch (Exception err)
            {
                return null;
            }
            return null;
        }
        private List<ActivityList> search(long idCar, long idUsu, long keym)
        {
            try
            {
                long ownAct = getIdOwnActivity(keym, idUsu, idCar);

                List<ActivityList> dat = db.actividades.Where(y =>
                    y.caracteristicas.id_caracteristica_padre == idCar &&
                    y.caracteristicas.keym_padre == keym &&
                    y.caracteristicas.id_usuario_padre == idUsu
                //&& y.caracteristicas.visualizar_superior == false
                ).OrderBy(x => x.pos).Select(x => new ActivityList()
                {
                    desc = x.descripcion,
                    folder = (x.folder == 0 ? 1 : 0),
                    idAct = x.id_actividad,
                    idCar = x.id_caracteristica,
                    usuCar = x.id_usuario,
                    keyM = x.keym.ToString(),
                    nom = x.nombre,
                    pos = x.pos,
                    parCar = idCar,
                    parUsu = idUsu,
                    sta = x.caracteristicas.estado
                }).ToList();
                return dat;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        //genera los links por publicacion web => 1 nivel
        public List<string> getLinks(long key, long idcar, long usu)
        {
            llink = new List<string>();
            try
            {
                caracteristicas car = (from x in db.caracteristicas
                                       where x.keym == key && x.id_caracteristica == idcar && x.id_usuario == usu
                                       select x).First();
                caracteristicas cx = car;
                //Crea opcion para retroceder en la navegacion de las caracteristicas que poseen paginaWeb
                try
                {
                    
                    bool st = true;
                    //segun actividades
                    while (st)
                    {
                        var carPar = (from x in db.caracteristicas
                                      join y in db.actividades on new { A = x.keym, B = x.id_caracteristica, C = x.id_usuario } equals new { A = y.keym, B = y.id_caracteristica, C = y.id_usuario }

                                      where x.keym == car.keym_padre &&
                                              x.id_caracteristica == car.id_caracteristica_padre &&
                                              x.id_usuario == car.id_usuario_padre
                                      select new
                                      {
                                          x,
                                          x.keym,
                                          x.id_caracteristica,
                                          x.id_usuario,
                                          y.nombre,
                                          y.id_usuarioNavigation.repositorios_usuarios.ruta_repositorio
                                      }).First();

                        st = (bool)carPar.x.publicacion_web;
                        if (st)
                        {
                            string idPadre = carPar.keym + "," + carPar.id_caracteristica + "," + carPar.id_usuario;
                            string rutaPadre = carPar.ruta_repositorio + carPar.nombre + ".html";

                            llink.Add(idPadre + "-" + "Atras" + "-" + rutaPadre);
                            st = false;
                            
                        }
                        else
                        {
                            car = carPar.x;
                            st = true;
                        }
                    }
                }
                catch
                {
                    try
                    {
                        car = cx;
                        //segun proyectos
                        var carPar = (from x in db.caracteristicas
                                      join pro in db.proyectos on new { A = x.keym, B = x.id_caracteristica, C = x.id_usuario } equals new { A = pro.keym, B = pro.id_caracteristica, C = pro.id_usuario }

                                      where x.keym == car.keym_padre &&
                                              x.id_caracteristica == car.id_caracteristica_padre &&
                                              x.id_usuario == car.id_usuario_padre
                                      select new
                                      {
                                          x.keym,
                                          x.id_caracteristica,
                                          x.id_usuario,
                                          pro.nombre,
                                          pro.id_usuarioNavigation.repositorios_usuarios.ruta_repositorio
                                      }).First();




                        string idPadre = carPar.keym + "," + carPar.id_caracteristica + "," + carPar.id_usuario;
                        string rutaPadre = carPar.ruta_repositorio + carPar.nombre + ".html";

                        llink.Add(idPadre + "-" + "Atras" + "-" + rutaPadre);
                    }
                    catch
                    {

                    }
                }

                getLinksRecursive(cx);


            }
            catch (Exception err) { llink.Add(err.Message); }

            return llink;
        }
        private void getLinksRecursive(caracteristicas car)
        {
            try
            {
                //db.Dispose();
                db = new MProjectContext();
                //lista de hijos de la caracteristica car
                var lstCar = (
                    from x in db.caracteristicas
                    join y in db.actividades
                    on new { A = x.keym, B = x.id_caracteristica, C = x.id_usuario } equals new { A = y.keym_car, B = y.id_caracteristica, C = y.id_usuario_car }

                    where (
                        x.keym_padre == car.keym &&
                        x.id_caracteristica_padre == car.id_caracteristica &&
                        x.id_usuario_padre == car.id_usuario)

                    orderby y.pos

                    select new
                    {
                        x,
                        x.keym,
                        y.id_actividad,
                        x.id_caracteristica,
                        x.id_usuario,
                        y.nombre,
                        y.id_usuarioNavigation.repositorios_usuarios,
                        x.publicacion_web
                    }
                    );
                try
                {
                    foreach (var x in lstCar)
                    {
                        if ((bool)x.publicacion_web)
                        {
                            try
                            {
                                string id = x.keym + "," + x.id_caracteristica + "," + x.id_usuario;
                                string nombre = x.nombre;
                                string ruta = x.repositorios_usuarios.ruta_repositorio + x.nombre + ".html";
                                llink.Add(id + "-" + nombre + "-" + ruta);
                            }
                            catch
                            {
                                llink.Add("ERROR");
                            }
                        }
                        else
                        {
                            getLinksRecursive(x.x);
                        }
                    }
                }
                catch (Exception err)
                {
                    string s = err.ToString();
                }
            }
            catch { }
        }

        //GEnera los links de todas las caracteristicas por publicacion web => todos los niveles
        private List<string> allLink;
        private int pos=0;
        public List<string> getAllLinks()
        {
            allLink = new List<string>();
            try
            {
                var carPro = from x in db.caracteristicas
                             join pro in db.proyectos on new { A = x.keym, B = x.id_caracteristica, C = x.id_usuario } equals new { A = pro.keym, B = pro.id_caracteristica, C = pro.id_usuario }

                             select new datLinks()
                             {
                                 car = x,
                                 idKey = x.keym,
                                 idCar = x.id_caracteristica,
                                 idUsu = x.id_usuario,
                                 nom = pro.nombre,
                                 rutaRep = pro.id_usuarioNavigation.repositorios_usuarios.ruta_repositorio,
                                 pubWeb = x.publicacion_web
                             };
                foreach (var x in carPro)
                {
                    string pubWeb = "";
                    if ((bool)x.pubWeb)
                        pubWeb = "Y";
                    else
                        pubWeb = "N";
                    string id = x.idKey + "," + x.idCar + "," + x.idUsu;
                    string idPar = x.car.keym_padre + "," + x.car.id_caracteristica_padre + "," + x.car.id_usuario_padre;
                    string nombre = x.nom;
                    string rutaPadre = x.rutaRep + x.nom + ".html";
                    allLink.Add(pubWeb + "-" + id + "-" + idPar + "-" + nombre + "-" + rutaPadre+"-"+pos);
                    getAllLinksRecursive(x.car);
                }


                return allLink;
            }
            catch { }
            return null;
        }
        private void getAllLinksRecursive(caracteristicas car)
        {
            pos++;
            try
            {
                //db.Dispose();
                db = new MProjectContext();
                //lista de hijos de la caracteristica car
                var lstCar = (
                    from x in db.caracteristicas
                    join y in db.actividades
                    on new { A = x.keym, B = x.id_caracteristica, C = x.id_usuario } equals new { A = y.keym_car, B = y.id_caracteristica, C = y.id_usuario_car }

                    where (
                        x.keym_padre == car.keym &&
                        x.id_caracteristica_padre == car.id_caracteristica &&
                        x.id_usuario_padre == car.id_usuario)

                    orderby y.pos ascending
                    
                    select new datLinks()
                    {
                        car = x,
                        idKey = x.keym,
                        idAct = y.id_actividad,
                        idCar = x.id_caracteristica,
                        idUsu = x.id_usuario,
                        nom = y.nombre,
                        repoUsu = y.id_usuarioNavigation.repositorios_usuarios,
                        pubWeb = x.publicacion_web
                    }
                    );
                try
                {
                   
                    foreach (datLinks x in lstCar)
                    {
                        
                        string pubWeb = "";
                        if ((bool)x.pubWeb)
                            pubWeb = "Y";
                        else
                            pubWeb = "N";
                        try
                        {
                            string id = x.idKey + "," + x.idCar + "," + x.idUsu;
                            string idPar = x.car.keym_padre + "," + x.car.id_caracteristica_padre + "," + x.car.id_usuario_padre;
                            string nombre = x.nom;
                            string ruta = x.repoUsu.ruta_repositorio + x.nom + ".html";
                            allLink.Add(pubWeb + "-" + id + "-" + idPar + "-" + nombre + "-" + ruta+"-"+pos);
                            getAllLinksRecursive(x.car);
                        }
                        catch
                        {
                            allLink.Add("ERROR");
                        }


                    }


                }
                catch (Exception err)
                {
                    string s = err.ToString();
                    return;
                }
                try
                {
                    foreach (datLinks x in lstCar)
                    {
                        caracteristicas cr = x.car;
                        //getAllLinksRecursive(cr);
                    }
                }
                catch { }
            }
            catch { }
            pos--;
            return;
        }
        private class datLinks
        {
            public caracteristicas car { get; set; }
            public long? idCar { get; set; }
            public long? idAct { get; set; }
            public long? idKey { get; set; }
            public long? idUsu { get; set; }
            public repositorios_usuarios repoUsu { get; set; }
            public bool? pubWeb { get; set; }
            public string nom { get; set; }
            public string rutaRep { get; set; }
        }
    }
}











/*
try
{
    if (op == 1)
    {
        var dat = from act in db.actividades
                  join car in db.caracteristicas on act.id_actividad equals car.id_actividad
                  where car.padre_caracteristica == id
                  orderby act.pos, act.folder
                  select new ActivityList
                  {
                      id_act = act.id_actividad,
                      name = act.nombre,
                      description = act.descripcion,
                      state = car.estado,
                      id_characteristic = car.id_caracteristica,
                      type = 1,
                      folder = (long)act.folder,
                      par_characteristic = (long)car.padre_caracteristica
                  };


        if (dat.Count() == 0)
        {
            caracteristicas cr = (from car in db.caracteristicas
                                  where car.padre_caracteristica == id
                                  select car).First();
var res = from act in db.actividades
          join car in db.caracteristicas on act.id_actividad equals car.id_actividad
          where car.padre_caracteristica == cr.id_caracteristica && act.compartido == 1
          orderby act.pos, act.folder
          select new ActivityList
          {
              id_act = act.id_actividad,
              name = act.nombre,
              description = act.descripcion,
              state = car.estado,
              id_characteristic = car.id_caracteristica,
              type = 2,
              folder = (long)act.folder,
              par_characteristic = (long)car.padre_caracteristica
          };
List<ActivityList> ds = res.ToList<ActivityList>();
            return ds;
        }
        else {
            List<ActivityList> d = dat.ToList<ActivityList>();
            return d;
        }
    }
    else if (op == 2)
    {
        caracteristicas cr = (from car in db.caracteristicas
                              where car.id_caracteristica == id
                              select car).First();

var dat = from act in db.actividades
          join car in db.caracteristicas on act.id_actividad equals car.id_actividad
          where car.padre_caracteristica == cr.padre_caracteristica
          orderby act.pos, act.folder
          select new ActivityList
          {
              id_act = act.id_actividad,
              name = act.nombre,
              description = act.descripcion,
              state = car.estado,
              id_characteristic = car.id_caracteristica,
              type = 1,
              folder = (long)act.folder,
              par_characteristic = (long)car.padre_caracteristica
          };

        if (dat.Count() == 0)
        {
            cr = (from car in db.caracteristicas
                  where car.id_caracteristica == cr.padre_caracteristica
                  select car).First();
dat = from act in db.actividades
          join car in db.caracteristicas on act.id_actividad equals car.id_actividad
          where car.padre_caracteristica == cr.padre_caracteristica
          orderby act.pos, act.folder
          select new ActivityList
                      {
                          id_act = act.id_actividad,
                          name = act.nombre,
                          description = act.descripcion,
                          state = car.estado,
                          id_characteristic = car.id_caracteristica,
                          type = 1,
                          folder = (long)act.folder,
                          par_characteristic = (long)car.padre_caracteristica
                      };

        }

        List<ActivityList> d = dat.ToList<ActivityList>();
        return d;
    }

}
catch { }
    */











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


  */
