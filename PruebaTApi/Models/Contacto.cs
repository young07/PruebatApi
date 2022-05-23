using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTApi.Models
{
    public class Contacto
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Por favor especificar un contacto")]
        public string Numero { get; set; }
        public virtual Cliente cliente { get; set; }
    }
}
