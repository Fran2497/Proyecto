using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public int IdEmpresa { get; set; }

        public string Empresa { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        public string Categoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, 99999)]
        [StringLength(5)]
        public string Codigo { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [Required]
        [Range(1,200, ErrorMessage ="Cantidad máxima de articulos es 200")]
        public int Cantidad { get; set; }

        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}