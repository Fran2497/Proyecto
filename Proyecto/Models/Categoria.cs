using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }

        [Required]
        public int IdSector { get; set; }

        public string Sector { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}