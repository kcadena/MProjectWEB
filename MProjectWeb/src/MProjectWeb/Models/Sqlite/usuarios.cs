using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.Sqlite
{
    public partial class usuarios
    {
        public usuarios()
        {
            usuarios_proyectos = new HashSet<usuarios_proyectos>();
        }

        public long id_usuario { get; set; }
        public string apellido { get; set; }
        public string e_mail { get; set; }
        public string nombre { get; set; }
        public string pass { get; set; }

        public virtual ICollection<usuarios_proyectos> usuarios_proyectos { get; set; }
    }
}
