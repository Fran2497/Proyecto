using Proyecto.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class clsRol
    {
        public List<ConsultarRolResult> ConsultarRol()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarRolResult> datos = db.ConsultarRol().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaRolResult ConsultaRol(int IdRol)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaRolResult dato = db.ConsultaRol(IdRol).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool AgregaRol(String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregarRol(Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool ActualizaRol(int IdRol, String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaRol(IdRol, Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool EliminaRol(int IdRol)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaRol(IdRol);
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
