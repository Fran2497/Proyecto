using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL; 
namespace BLL
{
    public class clsEstado
    {
        public List<ConsultarEstadoResult> ConsultarEstado()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarEstadoResult> datos = db.ConsultarEstado().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaEstadoResult ConsultaEstado(int IdEstado)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaEstadoResult dato = db.ConsultaEstado(IdEstado).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool AgregaEstado(String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregarEstado(Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool ActualizaEstado(int IdEstado, String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaEstado(IdEstado, Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool EliminaEstado(int IdEstado)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaEstado(IdEstado);
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
