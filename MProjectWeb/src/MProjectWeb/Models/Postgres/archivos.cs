using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class archivos
    {
        public string keym { get; set; }
        public int id_archivo { get; set; }
        public int id_usuario { get; set; }
        public string contenido { get; set; }
        public DateTime fecha_carga { get; set; }
        public DateTime fecha_ultima_modificacion { get; set; }
        public int id_caracteristica { get; set; }
        public int id_tipo_archivo { get; set; }
        public string nombre { get; set; }
        public int? publicacion { get; set; }

        public virtual tipos_archivos id_tipo_archivoNavigation { get; set; }
        public virtual caracteristicas caracteristicas { get; set; }
    }
}
