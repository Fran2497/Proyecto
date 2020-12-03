using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL;

namespace BLL
{
    public class clsUsuario
    {
        public List<ConsultarUsuarioResult> ConsultarUsuario()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarUsuarioResult> datos = db.ConsultarUsuario().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaUsuarioResult ConsultaUsuario(int IdUsuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaUsuarioResult dato = db.ConsultaUsuario(IdUsuario).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool AgregaUsuarior(int IdEmpresa, int IdTipoIdentificacion, String Identificacion, String Nombre, String Apellido1, String Apellido2,
            String Telefono, String Correo, String Clave, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregarUsuario(IdEmpresa, IdTipoIdentificacion, Identificacion, Nombre, Apellido1, Apellido2, Telefono, Correo, Clave, Estado,
                    Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool ActualizaUsuario(int IdUsuario, int IdEmpresa, int IdTipoIdentificacion, String Identificacion, String Nombre, String Apellido1, String Apellido2,
            String Telefono, String Correo, String Clave, bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaUsuario(IdUsuario, IdEmpresa, IdTipoIdentificacion, Identificacion, Nombre, Apellido1, Apellido2, Telefono, Correo,
                    Clave, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool EliminaUsuario(int IdUsuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaUsuario(IdUsuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public ValidaUsuarioResult ValidaUsuario(string correo, string clave)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ValidaUsuarioResult dato = db.ValidaUsuario(correo, clave).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<UsuarioRolesResult> UsuarioRoles(int IdUsuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<UsuarioRolesResult> lista = db.UsuarioRoles(IdUsuario).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}