using BLL;
using Proyecto.DAL;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        // GET: Producto
        clsProducto ObjProducto = new clsProducto();
        clsEmpresa ObjEmpresa = new clsEmpresa();
        clsCategoria ObjCategoria = new clsCategoria();
        public ActionResult Index()
        {
            try
            {
                var datos = ObjProducto.ConsultarProducto();
                List<Producto> ListaProductos = new List<Producto>();

                foreach (var item in datos)
                {
                    Producto producto = new Producto
                    {
                        IdProducto = item.IdProducto,
                        IdEmpresa = (int)item.IdEmpresa,
                        Empresa = item.Empresa,
                        IdCategoria = item.IdCategoria,
                        Categoria = item.Categoria,
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

        public ActionResult IndexMovil(int id)
        {
            ViewBag.Identificacion = id;
            return View();
        }

        public ActionResult Consulta(int id)
        {
            try
            {
                var dato = ObjProducto.ConsultaProducto(id);
                return View(dato);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                var dato = ObjProducto.ConsultaProducto(id);

                Producto producto = new Producto
                {
                    IdProducto = dato.IdProducto,
                    IdEmpresa = (int)dato.IdEmpresa,
                    IdCategoria = dato.IdCategoria,
                    Descripcion = dato.Descripcion,
                    Codigo = dato.Codigo,
                    Precio = dato.Precio,
                    Cantidad = dato.Cantidad,
                    Estado = dato.Estado
                };

                ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                ViewBag.Categorias = ListaCategorias().Where(x => x.Estado == true);

                return View(producto);

            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(Producto producto)
        {
            try
            {
                if (ObjProducto.ActualizaProducto(producto.IdProducto, producto.IdEmpresa, producto.IdCategoria, producto.Descripcion,
                    producto.Codigo, producto.Precio, producto.Cantidad, producto.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("IndexProductos", "Home", new { idEmp = Session["Empresa"] });
                }
                else
                {
                    ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                    ViewBag.Categorias = ListaCategorias().Where(x => x.Estado == true);
                    return View(producto);
                }

            }
            catch (Exception)
            {
                ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                ViewBag.Categorias = ListaCategorias().Where(x => x.Estado == true);
                return View(producto);
                throw;
            }
        }


        public ActionResult Crear()
        {
            try
            {
                ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                ViewBag.Categorias = ListaCategorias().Where(x => x.Estado == true);
                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Crear(Producto producto)
        {
            try
            {
                var empresa = 0;
                if (User.IsInRole("Usuario"))
                {
                    empresa = Convert.ToInt32(Session["Empresa"].ToString());
                }
                else
                {
                    empresa = producto.IdEmpresa;
                }

                if (ObjProducto.AgregaProducto(empresa, producto.IdCategoria, producto.Descripcion,
                    producto.Codigo, producto.Precio, producto.Cantidad, producto.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("IndexProductos", "Home", new { idEmp = Session["Empresa"] });
                }
                else
                {
                    ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                    ViewBag.Categorias = ListaCategorias().Where(x => x.Estado == true);
                    return View(producto);
                }

            }
            catch (Exception)
            {
                ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                ViewBag.Categorias = ListaCategorias().Where(x => x.Estado == true);
                return View(producto);
                throw;
            }
        }


        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjProducto.ConsultaProducto(id);

                Producto producto = new Producto
                {
                    IdProducto = dato.IdProducto,
                    IdEmpresa = (int)dato.IdEmpresa,
                    Empresa = dato.Empresa,
                    IdCategoria = dato.IdCategoria,
                    Categoria = dato.Categoria,
                    Descripcion = dato.Descripcion,
                    Codigo = dato.Codigo,
                    Precio = dato.Precio,
                    Cantidad = dato.Cantidad,
                    Estado = dato.Estado
                };

                return View(producto);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Producto producto)
        {
            try
            {
                if (ObjProducto.EliminaProducto(producto.IdProducto))
                {
                    return RedirectToAction("IndexProductos", "Home", new { idEmp = Session["Empresa"] });
                }
                else
                {
                    return View(producto);
                }

            }
            catch (Exception)
            {
                return View(producto);
                throw;
            }
        }



        #region Datos
        private List<ConsultarEmpresaResult> ListaEmpresas()
        {
            List<ConsultarEmpresaResult> datos = ObjEmpresa.ConsultarEmpresa();
            return datos;
        }
        private List<ConsultarCategoriaResult> ListaCategorias()
        {
            List<ConsultarCategoriaResult> datos = ObjCategoria.ConsultarCategoria();
            return datos;
        }
        #endregion

    }
}