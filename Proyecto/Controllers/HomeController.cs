using BLL;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        clsProducto ObjProducto = new clsProducto();
        public ActionResult Index()
        {
            ViewBag.Empresa = Session["Empresa"];
            ViewBag.Usuario = Session["Nombre"];
            ViewBag.Rol = Session["Roles"];
            return View();
            
        }

        public ActionResult IndexProductos(int idEmp)
        {
            try
            {
                var dato = ObjProducto.ProductosEmpresa(idEmp);
                List<Producto> ListaProductos = new List<Producto>();

                foreach (var item in dato)
                {
                    Producto producto = new Producto
                    {
                        IdProducto = item.IdProducto,
                        IdEmpresa = (int)item.IdEmpresa,
                        IdCategoria = item.IdCategoria,
                        Descripcion = item.Descripcion,
                        Codigo = item.Codigo,
                        Precio = item.Precio,
                        Cantidad = item.Cantidad,
                        Estado = item.Estado
                    };

                    ListaProductos.Add(producto);
                }
                return View(ListaProductos);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}