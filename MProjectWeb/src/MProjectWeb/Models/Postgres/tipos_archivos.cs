using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class tipos_archivos
    {
        public tipos_archivos()
        {
            archivos = new HashSet<archivos>();
        }

        public int id_tipo_archivo { get; set; }
        public string nombre { get; set; }

        public virtual ICollection<archivos> archivos { get; set; }
    }
}
