using Proyecto.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class clsBitacora
    {
        public bool RegistraBitacora(string Usuario, string Controlador, string Accion, string Detalle, byte Tipo)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.RegistraBitacora(Usuario, Controlador, Accion, Detalle, Tipo);
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
