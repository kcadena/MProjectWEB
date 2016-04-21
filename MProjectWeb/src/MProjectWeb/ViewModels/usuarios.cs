using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MProjectWeb.ViewModels
{
    public partial class usuarios
    {
        [Required]
        [Display(Name = "ID")]
        public long id_usuario { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string e_mail { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string pass { get; set; }

       
     
        public bool aux { get; set; }

    }
}
