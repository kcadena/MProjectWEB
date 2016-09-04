using System;
using System.Collections.Generic;

namespace MProjectWeb.Models.postgres
{
    public partial class repositorios_usuarios
    {
        public int id_usuario { get; set; }
        public string ruta_repositorio { get; set; }

        public virtual usuarios id_usuarioNavigation { get; set; }
    }
}
