using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class plantillas_meta_datos
    {
        public string keym { get; set; }
        public int id_plantilla_meta_dato { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_ultima_modificacion { get; set; }
        public int id_meta_dato { get; set; }
        public int id_plantilla { get; set; }
        public bool requerido { get; set; }

        public virtual usuarios id_usuarioNavigation { get; set; }
        public virtual meta_datos meta_datos { get; set; }
        public virtual plantillas plantillas { get; set; }
    }
}
