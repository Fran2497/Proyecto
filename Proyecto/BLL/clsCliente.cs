using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL;

namespace BLL
{
    public class clsCliente
    {
        public List<ConsultarClienteResult> ConsultarCliente()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarClienteResult> datos = db.ConsultarCliente().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaClienteResult ConsultaCliente(int IdCliente)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaClienteResult dato = db.ConsultaCliente(IdCliente).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool AgregaCliente(int IdTipoIdentificacion, String Identificacion, String Correo, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregarCliente(IdTipoIdentificacion, Identificacion, Correo, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool ActualizaCliente(int IdCliente, int IdTipoIdentificacion, String Identificacion, String Correo, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaCliente(IdCliente, IdTipoIdentificacion, Identificacion, Correo, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool EliminaCliente(int IdCliente)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaCliente(IdCliente);
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
