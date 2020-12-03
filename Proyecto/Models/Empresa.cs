using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdSector { get; set; }
        public string Sector { get; set; }
        public char Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public bool Estado { get; set; }
        public string ProvinciaDesc { get; set; }
        public string CantonDesc { get; set; }
        public string DistritoDesc { get; set; }
    }
}