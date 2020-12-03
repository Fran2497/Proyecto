using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL;

namespace BLL
{
    public class clsUsuarioRol
    {
        public List<ConsultarUsuarioRolResult> ConsultarUsuarioRol()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarUsuarioRolResult> datos = db.ConsultarUsuarioRol().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaUsuarioRolResult ConsultaUsuarioRol(int IdUsuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaUsuarioRolResult dato = db.ConsultaUsuarioRol(IdUsuario).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool AgregaUsuarioRol(int IdUsuario, int IdRol, String usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregaUsuarioRol(IdUsuario, IdRol,usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool ActualizaUsuarioRol(int IdUsuarioRol, int IdUsuario, int IdRol, String usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaUsuarioRol(IdUsuarioRol, IdUsuario, IdRol, usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool EliminaUsuario(int IdUsuarioRol)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaUsuarioRol(IdUsuarioRol);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool TRAgregaUsuarioRol(int IdRol, String usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.TR_AgregaUsuarioRol(IdRol, usuario);
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
