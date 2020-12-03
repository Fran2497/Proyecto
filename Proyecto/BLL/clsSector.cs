using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL;

namespace BLL
{
    public class clsSector
    {
        public List<ConsultarSectorResult> ConsultarSector()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarSectorResult> datos = db.ConsultarSector().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaSectorResult ConsultaSector(int IdSector)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaSectorResult dato = db.ConsultaSector(IdSector).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool AgregaSector(String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregarSector(Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool ActualizaSector(int IdSector, String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaSector(IdSector, Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool EliminaSector(int IdSector)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaSector(IdSector);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public ConsultaSectorEmpResult ConsultaSectorEmp(int IdEmp)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaSectorEmpResult dato = db.ConsultaSectorEmp(IdEmp).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
