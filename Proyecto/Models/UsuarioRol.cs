using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class UsuarioRol
    {

        public int IdUsuarioRol { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        [Required]
        public int IdRol { get; set; }
        public string Rol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}