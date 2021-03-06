using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class plantillas
    {
        public plantillas()
        {
            plantillas_meta_datos = new HashSet<plantillas_meta_datos>();
        }

        public long keym { get; set; }
        public long id_plantilla { get; set; }
        public long id_usuario { get; set; }
        public string descripcion { get; set; }
        public string fecha_ultima_modificacion { get; set; }
        public string nombre { get; set; }

        public virtual ICollection<plantillas_meta_datos> plantillas_meta_datos { get; set; }
        public virtual usuarios id_usuarioNavigation { get; set; }
    }
}
