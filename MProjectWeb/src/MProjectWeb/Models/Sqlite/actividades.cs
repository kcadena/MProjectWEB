using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.Sqlite
{
    public partial class actividades
    {
        public actividades()
        {
            caracteristicas = new HashSet<caracteristicas>();
        }

        public long id_actividad { get; set; }
        public string descripcion { get; set; }
        public long? id_folder { get; set; }
        public string nombre { get; set; }
        public long? pos { get; set; }

        public virtual ICollection<caracteristicas> caracteristicas { get; set; }
        public virtual folders id_folderNavigation { get; set; }
    }
}
