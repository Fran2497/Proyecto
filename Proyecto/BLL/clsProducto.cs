using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL;

namespace BLL
{
    public class clsProducto
    {
        public List<ConsultarProductoResult> ConsultarProducto()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ConsultarProductoResult> datos = db.ConsultarProducto().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ConsultaProductoResult ConsultaProducto(int IdProducto)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ConsultaProductoResult dato = db.ConsultaProducto(IdProducto).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool AgregaProducto(int IdEmpresa, int IdCategoria, String Descripcion, String Codigo, Decimal Precio, int Cantidad,
            bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.AgregarProducto(IdEmpresa, IdCategoria, Descripcion, Codigo, Precio, Cantidad, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool ActualizaProducto(int IdProducto, int IdEmpresa, int IdCategoria, String Descripcion, String Codigo, Decimal Precio, int Cantidad,
            bool Estado, String Usuario)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.ActualizaProducto(IdProducto, IdEmpresa, IdCategoria, Descripcion, Codigo, Precio, Cantidad, Estado, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool EliminaProducto(int IdProducto)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                db.EliminaProducto(IdProducto);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public List<ProductosEmpresaResult> ProductosEmpresa(int idEmp)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ProductosEmpresaResult> dato = db.ProductosEmpresa(idEmp).ToList();
                return dato;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
