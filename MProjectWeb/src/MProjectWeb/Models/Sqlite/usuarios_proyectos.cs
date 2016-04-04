using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.Sqlite
{
    public partial class usuarios_proyectos
    {
        public long id_usuario { get; set; }
        public long id_proyecto { get; set; }

        public virtual proyectos id_proyectoNavigation { get; set; }
        public virtual usuarios id_usuarioNavigation { get; set; }
    }
}
