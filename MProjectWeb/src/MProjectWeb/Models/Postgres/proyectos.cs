using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class proyectos
    {
        public proyectos()
        {
            proyectos_meta_datos = new HashSet<proyectos_meta_datos>();
        }

        public string keym { get; set; }
        public int id_proyecto { get; set; }
        public int id_usuario { get; set; }
        public int? contador { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_ultima_modificacion { get; set; }
        public string icon { get; set; }
        public int id_caracteristica { get; set; }
        public bool ir_proyecto { get; set; }
        public string nombre { get; set; }
        public string plantilla { get; set; }

        public virtual ICollection<proyectos_meta_datos> proyectos_meta_datos { get; set; }
        public virtual usuarios id_usuarioNavigation { get; set; }
        public virtual caracteristicas caracteristicas { get; set; }
    }
}
