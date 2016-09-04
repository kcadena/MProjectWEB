using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class costos
    {
        public string keym { get; set; }
        public int id_costo { get; set; }
        public int id_usuario { get; set; }
        public int cantidad { get; set; }
        public int id_caracteristica { get; set; }
        public string nombre { get; set; }
        public int valor { get; set; }

        public virtual usuarios id_usuarioNavigation { get; set; }
        public virtual caracteristicas caracteristicas { get; set; }
    }
}
