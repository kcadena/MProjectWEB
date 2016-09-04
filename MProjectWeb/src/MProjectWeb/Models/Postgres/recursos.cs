using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class recursos
    {
        public string keym { get; set; }
        public int id_recurso { get; set; }
        public int id_usuario { get; set; }
        public int cantidad { get; set; }
        public int id_caracteristica { get; set; }
        public string nombre_recurso { get; set; }

        public virtual usuarios id_usuarioNavigation { get; set; }
        public virtual caracteristicas caracteristicas { get; set; }
    }
}
