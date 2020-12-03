using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Proyecto.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required]
        public int IdEmpresa { get; set; }

        public string Empresa { get; set; }

        [Required]
        public int IdTipoIdentificacion { get; set; }

        public string TipoIdentificacion { get; set; }

        [Required]
        [StringLength(10)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string Apellido1 { get; set; }

        [Required]
        [StringLength(20)]
        public string Apellido2 { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [StringLength(50)]
        public string Clave { get; set; }

        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}