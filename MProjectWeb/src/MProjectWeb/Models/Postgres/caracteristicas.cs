using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class caracteristicas
    {
        public caracteristicas()
        {
            actividades = new HashSet<actividades>();
            archivos = new HashSet<archivos>();
            costosNavigation = new HashSet<costos>();
            presupuestoNavigation = new HashSet<presupuesto>();
            proyectos = new HashSet<proyectos>();
            recursosNavigation = new HashSet<recursos>();
        }

        public string keym { get; set; }
        public int id_caracteristica { get; set; }
        public int id_usuario { get; set; }
        public string costos { get; set; }
        public string estado { get; set; }
        public DateTime? fecha_fin { get; set; }
        public DateTime? fecha_inicio { get; set; }
        public DateTime fecha_ultima_modificacion { get; set; }
        public int? id_caracteristica_padre { get; set; }
        public int porcentaje_asignado { get; set; }
        public int porcentaje_cumplido { get; set; }
        public string presupuesto { get; set; }
        public string recursos { get; set; }
        public string tipo_caracteristica { get; set; }
        public int? usuario_asignado { get; set; }
        public bool visualizar_superior { get; set; }

        public virtual ICollection<actividades> actividades { get; set; }
        public virtual ICollection<archivos> archivos { get; set; }
        public virtual ICollection<costos> costosNavigation { get; set; }
        public virtual ICollection<presupuesto> presupuestoNavigation { get; set; }
        public virtual ICollection<proyectos> proyectos { get; set; }
        public virtual ICollection<recursos> recursosNavigation { get; set; }
        public virtual usuarios id_usuarioNavigation { get; set; }
        public virtual usuarios usuario_asignadoNavigation { get; set; }
        public virtual caracteristicas caracteristicasNavigation { get; set; }
        public virtual ICollection<caracteristicas> InversecaracteristicasNavigation { get; set; }
    }
}
