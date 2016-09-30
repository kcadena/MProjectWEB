using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class presupuesto
    {
        public long keym { get; set; }
        public long id_presupuesto { get; set; }
        public long id_usuario { get; set; }
        public int cantidad { get; set; }
        public DateTime fecha_ultima_modificacion { get; set; }
        public long id_caracteristica { get; set; }
        public long id_usuario_car { get; set; }
        public long keym_car { get; set; }
        public string nombre { get; set; }
        public int valor { get; set; }

        public virtual usuarios id_usuarioNavigation { get; set; }
        public virtual caracteristicas caracteristicas { get; set; }
    }
}
