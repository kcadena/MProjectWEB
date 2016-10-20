using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MProjectWeb.ViewModels
{
    public partial class usuarios
    {



        [Display(Name = "ID")]
        public long id_usuario { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string e_mail { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string pass { get; set; }
        [Required]
        [Display(Name = "Cargo")]
        public string cargo { get; set; }
        [Required]
        [Display(Name = "Entidad")]
        public string entidad { get; set; }
        [Required]
        [Display(Name = "Genero")]
        public string genero { get; set; }
        [Display(Name = "Imagen")]
        public string imagen { get; set; }
        [Required(ErrorMessage ="Es requerido")]

        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string telefono { get; set; }


        public bool administrador { get; set; }
        public bool aux { get; set; }

    }
}
