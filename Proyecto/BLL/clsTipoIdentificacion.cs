using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL;

namespace BLL
{
    public class clsTipoIdentificacion
    {
        public List<ConsultarTipoIdentificacionResult> ConsultarTipoIdentificacion()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarTipoIdentificacionResult> datos = db.ConsultarTipoIdentificacion().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaTipoIdentificacionResult ConsultaTipoIdentificacion(int IdTipoIdentificacion)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaTipoIdentificacionResult dato = db.ConsultaTipoIdentificacion(IdTipoIdentificacion).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool AgregaTipoIdentificacion(String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregarTipoIdentificacion(Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool ActualizaTipoIdentificacion(int IdTipoIdentificacion, String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaTipoIdentificacion(IdTipoIdentificacion, Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool EliminaTipoIdentificacion(int IdTipoIdentificacion)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaTipoIdentificacion(IdTipoIdentificacion);
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
