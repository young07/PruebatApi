using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTApi.Models
{
    public class Cliente
    {
        public Cliente()
        {
            this.Contactos = new HashSet<Contacto>();
        }
        public int id { get; set; }
        [Required(ErrorMessage = "Por favor especificar un Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Por favor especificar un Apellido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Por favor especificar una Fecha Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string Fecha_Nacimiento { get; set; }
        [Required(ErrorMessage = "Por favor especificar un genero")]
        public string genero { get; set; }
        [Required(ErrorMessage = "Por favor especificar un correo electronico")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }
        public virtual ICollection<Contacto> Contactos { get; set; }
        
    }
}
