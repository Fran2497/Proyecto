using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL;

namespace BLL
{
    public class clsCategoria
    {
        public List<ConsultarCategoriaResult> ConsultarCategoria()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarCategoriaResult> datos = db.ConsultarCategoria().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaCategoriaResult ConsultaCategoria(int IdCategoria)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaCategoriaResult dato = db.ConsultaCategoria(IdCategoria).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool AgregaCategoria(int IdSector, String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregarCategoria(IdSector, Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool ActualizaCategoria(int IdCategoria, int IdSector, String Descripcion, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaCategoria(IdCategoria, IdSector, Descripcion, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool EliminaCategoria(int IdCategoria)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaCategoria(IdCategoria);
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
