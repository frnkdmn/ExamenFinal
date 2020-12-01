using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Core.Entities
{
    public class Cliente
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }
        [DisplayName("NOMBRE")]
        [Required(ErrorMessage = "INGRESE NOMBRE")]
        public string nombre { get; set; }
        [DisplayName("DNI")]
        [Required(ErrorMessage = "INGRESE DNI")]
        public string dni { get; set; }
        [DisplayName("TELEFONO")]
        [Required(ErrorMessage = "INGRESE TELEFONO")]
        public string telefono { get; set; }
        [DisplayName("DISTRITO")]
        [Required(ErrorMessage ="INGRESE DISTRITO")]
        public string distrito { get; set; }
    }
}
