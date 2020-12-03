using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Login
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [StringLength(50)]
        public string Clave { get; set; }

        public bool Recordarme { get; set; }

        //[DisplayName("Price")]
        //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
    }
}