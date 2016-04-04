using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.Sqlite
{
    public partial class proyectos
    {
        public proyectos()
        {
            caracteristicas = new HashSet<caracteristicas>();
            caracteristicasNavigation = new HashSet<caracteristicas>();
            folders = new HashSet<folders>();
            proyectos_meta_datos = new HashSet<proyectos_meta_datos>();
            usuarios_proyectos = new HashSet<usuarios_proyectos>();
        }

        public long id_proyecto { get; set; }
        public long id_plantilla { get; set; }
        public long id_repositorio { get; set; }
        public string IR_proyecto { get; set; }

        public virtual ICollection<caracteristicas> caracteristicas { get; set; }
        public virtual ICollection<caracteristicas> caracteristicasNavigation { get; set; }
        public virtual ICollection<folders> folders { get; set; }
        public virtual ICollection<proyectos_meta_datos> proyectos_meta_datos { get; set; }
        public virtual ICollection<usuarios_proyectos> usuarios_proyectos { get; set; }
        public virtual plantillas id_plantillaNavigation { get; set; }
        public virtual repositorio id_repositorioNavigation { get; set; }
    }
}
