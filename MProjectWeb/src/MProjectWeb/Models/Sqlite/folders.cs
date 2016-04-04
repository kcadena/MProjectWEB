using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.Sqlite
{
    public partial class folders
    {
        public folders()
        {
            actividades = new HashSet<actividades>();
            archivos = new HashSet<archivos>();
        }

        public long id_folder { get; set; }
        public long id_proyecto { get; set; }
        public string nombre { get; set; }
        public long Parent_id_folder { get; set; }

        public virtual ICollection<actividades> actividades { get; set; }
        public virtual ICollection<archivos> archivos { get; set; }
        public virtual folders Parent_id_folderNavigation { get; set; }
        public virtual ICollection<folders> InverseParent_id_folderNavigation { get; set; }
        public virtual proyectos id_proyectoNavigation { get; set; }
    }
}
