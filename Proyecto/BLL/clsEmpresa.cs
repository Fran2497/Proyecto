using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL;

namespace BLL
{
    public class clsEmpresa
    {
        public List<ConsultarEmpresaResult> ConsultarEmpresa() 
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarEmpresaResult> datos = db.ConsultarEmpresa().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaEmpresaResult ConsultaEmpresa(int IdEmpresa)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaEmpresaResult dato = db.ConsultaEmpresa(IdEmpresa).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool ActualizaEmpresa(int IdEmpresa, string Descripcion,int IdTipoIdentificacion, string Identificacion, string Direccion, string Telefono,int IdSector,char Provincia, string Canton, string Distrito,bool Estado, string Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaEmpresa(IdEmpresa,Descripcion,IdTipoIdentificacion,Identificacion,Direccion,Telefono,IdSector,Provincia,Canton,Distrito,Estado,Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool CreaEmpresa(string Descripcion, int IdTipoIdentificacion, string Identificacion, string Direccion, string Telefono, int IdSector, char Provincia, string Canton, string Distrito, bool Estado, string Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregarEmpresa(Descripcion, IdTipoIdentificacion, Identificacion, Direccion, Telefono, IdSector, Provincia, Canton, Distrito, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool EliminaEmpresa(int IdEmpresa)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaEmpresa(IdEmpresa);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

    }
}
