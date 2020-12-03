using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL;
using Proyecto.DAL;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        // GET: Categoria
        clsCategoria ObjCategoria = new clsCategoria();
        clsSector ObjSector = new clsSector();
        clsBitacora ObjBitacora = new clsBitacora();
        public ActionResult Index()
        {
            try
            {
                 var datos = ObjCategoria.ConsultarCategoria();
                    List<Categoria> ListaCategorias = new List<Categoria>();

                    foreach (var item in datos)
                    {
                    Categoria categoria = new Categoria
                    {
                        IdCategoria = item.IdCategoria,
                        IdSector = (int)item.IdSector,
                        Sector = item.Sector,
                        Descripcion = item.Descripcion,
                        Estado = item.Estado
                    };

                    ListaCategorias.Add(categoria);
                    }
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Ver listado de categorías.", "Se muestra listado de categorías.", 1);
                return View(ListaCategorias);
            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Ver listado de categorías.", "Erro al mostrar listado de categorías.", 0);
                
                throw;
            }
        }

        public ActionResult Consulta(int id)
        {
            try
            {
                var dato = ObjCategoria.ConsultaCategoria(id);
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Ver detalle de una categoría.", "Se muestra detalle de categoría.", 1);
                return View(dato);
            }
            catch (Exception)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Ver detalle de una categoría.", "Error al mostrar detalle de categoría.", 0);
                throw;
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                var dato = ObjCategoria.ConsultaCategoria(id);

                Categoria categoria = new Categoria
                {
                    IdCategoria = dato.IdCategoria,
                    IdSector = (int)dato.IdSector,
                    Sector = dato.Sector,
                    Descripcion = dato.Descripcion,
                    Estado = dato.Estado
                };

                ViewBag.Sectores = ListaSector().Where(x => x.Estado == true);
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Editar categoría.", "Se muestra vista de edición de categoría.", 1);
                return View(categoria);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error: " + e.Message;
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Editar categoría.", "Error al mostrar vista de edición de categoría.", 0);
                return View();
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(Categoria categoria)
        {
            try
            {
                if (ObjCategoria.ActualizaCategoria(categoria.IdCategoria, categoria.IdSector, categoria.Descripcion, categoria.Estado, Session["Identificacion"].ToString()))
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Editar categoría.", "Se edita correctamente la categoría.", 1);
                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.Error = "Error en los campos";
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Editar categoría.", "Error en la edición de categoría.", 0);
                    ViewBag.Sectores = ListaSector().Where(x => x.Estado == true);
                    return View(categoria);
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = "Error: " + e.Message;
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Editar categoría.", "Error en la edición de categoría.", 0);
                ViewBag.Sectores = ListaSector().Where(x => x.Estado == true);
                return View(categoria);
                throw;
            }
        }


        public ActionResult Crear()
        {
            try
            {
                ViewBag.Sectores = ListaSector().Where(x => x.Estado == true);
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Crear categoría.", "Se muestra vista de creacíon de categoría.", 1);
                return View();
            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Crear categoría.", "Error al mostrar vista de creacíon de categoría.", 0);
                ViewBag.Error = "Error: " + e.Message;
                throw;
            }
        }

        [HttpPost]
        public ActionResult Crear(Categoria categoria)
        {
            try
            {
                var sector_id = 0;

                if (User.IsInRole("Usuario"))
                {
                    sector_id = ObjSector.ConsultaSectorEmp(Convert.ToInt32(Session["Empresa"])).IdSector;
                }
                else
                {
                    sector_id = categoria.IdSector;
                }

                if (ObjCategoria.AgregaCategoria(sector_id, categoria.Descripcion, categoria.Estado, Session["Identificacion"].ToString()))
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Crear categoría.", "Se crea correctamente la categoría.", 1);
                    return RedirectToAction("index");
                }
                else
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Crear categoría.", "Error al crear la categoría.", 0);
                    ViewBag.Sectores = ListaSector().Where(x => x.Estado == true);
                    return View(categoria);
                }

            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Crear categoría.", "Error al crear la categoría.", 0);
                ViewBag.Sectores = ListaSector().Where(x => x.Estado == true);
                ViewBag.Error = "Error: " + e.Message;
                return View(categoria);
                throw;
            }
        }


        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjCategoria.ConsultaCategoria(id);

                    Categoria categoria = new Categoria
                    {
                        IdCategoria = dato.IdCategoria,
                        IdSector = (int)dato.IdSector,
                        Sector = dato.Sector,
                        Descripcion = dato.Descripcion,
                        Estado = dato.Estado
                    };
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Eliminar categoría.", "Se muestra vista de eliminación de categoría.", 1);
                return View(categoria);
            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Eliminar categoría.", "Error al mostrar vista de eliminación de categoría.", 0);
                ViewBag.Error = "Error: " + e.Message; 
                throw;
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Categoria categoria)
        {
            try
            {
                if (ObjCategoria.EliminaCategoria(categoria.IdCategoria))
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Crear categoría.", "Se elimina correctamente la categoría.", 1);
                    return RedirectToAction("index");
                }
                else
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Crear categoría.", "Error al eliminar la categoría.", 0);
                    return View(categoria);
                }

            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Categoría", "Crear categoría.", "Error al eliminar la categoría.", 0);
                ViewBag.Error = "Error: " + e.Message;
                return View(categoria);
                throw;
            }
        }


        #region Datos
        private List<ConsultarSectorResult> ListaSector()
        {
            List<ConsultarSectorResult> datos = ObjSector.ConsultarSector();
            return datos;
        }
        #endregion
    }
}