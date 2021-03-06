using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class actividades
    {
        public long keym { get; set; }
        public long id_actividad { get; set; }
        public long id_usuario { get; set; }
        public string descripcion { get; set; }
        public string fecha_ultima_modificacion { get; set; }
        public int folder { get; set; }
        public long id_caracteristica { get; set; }
        public long id_usuario_car { get; set; }
        public long keym_car { get; set; }
        public string nombre { get; set; }
        public int pos { get; set; }

        public virtual usuarios id_usuarioNavigation { get; set; }
        public virtual caracteristicas caracteristicas { get; set; }
    }
}
