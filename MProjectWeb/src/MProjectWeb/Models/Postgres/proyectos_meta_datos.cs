using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class proyectos_meta_datos
    {
        public string keym { get; set; }
        public int id_proyecto_meta_dato { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_ultima_modificacion { get; set; }
        public int id_proyecto { get; set; }
        public bool is_title { get; set; }
        public string tipo { get; set; }
        public string valor { get; set; }

        public virtual usuarios id_usuarioNavigation { get; set; }
        public virtual proyectos proyectos { get; set; }
    }
}
