using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        [Required]
        public int IdTipoIdentificacion { get; set; }

        public string TipoIdentificacion { get; set; }

        [Required]
        [MinLength(9)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Correo { get; set; }

        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}