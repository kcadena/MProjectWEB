using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class actividades
    {
        public string keym { get; set; }
        public int id_actividad { get; set; }
        public int id_usuario { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_ultima_modificacion { get; set; }
        public int folder { get; set; }
        public int id_caracteristica { get; set; }
        public string nombre { get; set; }
        public int pos { get; set; }

        public virtual usuarios id_usuarioNavigation { get; set; }
        public virtual caracteristicas caracteristicas { get; set; }
    }
}
