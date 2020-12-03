using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.DAL;
using BLL;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    [Authorize]
    [AuthorizeRole(Role.Administrador, Role.Supervisor)]
    public class TipoIdentificacionController : Controller
    {
        // GET: TipoIdentificacion
        clsTipoIdentificacion ObjTipoIdentificacion = new clsTipoIdentificacion();
        public ActionResult Index()
        {
            try
            {
                var datos = ObjTipoIdentificacion.ConsultarTipoIdentificacion();
                    List<TipoIdentificacion> ListaTipoIdentificacion = new List<TipoIdentificacion>();

                    foreach (var item in datos)
                    {
                        TipoIdentificacion tipoIdentificacion = new TipoIdentificacion();

                        tipoIdentificacion.IdTipoIdentificacion = item.IdTipoIdentificacion;
                        tipoIdentificacion.Descripcion = item.Descripcion;
                        tipoIdentificacion.Estado = item.Estado;

                        ListaTipoIdentificacion.Add(tipoIdentificacion);
                    }
                    return View(ListaTipoIdentificacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Consulta(int id)
        {
            try
            {
                var dato = ObjTipoIdentificacion.ConsultaTipoIdentificacion(id);
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
                var dato = ObjTipoIdentificacion.ConsultaTipoIdentificacion(id);

                    TipoIdentificacion tipoIdentificacion = new TipoIdentificacion();

                    tipoIdentificacion.IdTipoIdentificacion = dato.IdTipoIdentificacion;
                    tipoIdentificacion.Descripcion = dato.Descripcion;
                    tipoIdentificacion.Estado = dato.Estado;

                    return View(tipoIdentificacion);
                
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(TipoIdentificacion tipoIdentificacion)
        {
            try
            {
                if (ObjTipoIdentificacion.ActualizaTipoIdentificacion(tipoIdentificacion.IdTipoIdentificacion, tipoIdentificacion.Descripcion, tipoIdentificacion.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(tipoIdentificacion);
                }

            }
            catch (Exception)
            {
                return View(tipoIdentificacion);
                throw;
            }
        }


        public ActionResult Crear()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Crear(TipoIdentificacion tipoIdentificacion)
        {
            try
            {
                if (ObjTipoIdentificacion.AgregaTipoIdentificacion(tipoIdentificacion.Descripcion, tipoIdentificacion.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(tipoIdentificacion);
                }

            }
            catch (Exception)
            {
                return View(tipoIdentificacion);
                throw;
            }
        }


        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjTipoIdentificacion.ConsultaTipoIdentificacion(id);

                    TipoIdentificacion tipoIdentificacion = new TipoIdentificacion
                    {
                        IdTipoIdentificacion = dato.IdTipoIdentificacion,
                        Estado = dato.Estado
                    };

                    return View(tipoIdentificacion);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Eliminar(TipoIdentificacion tipoIdentificacion)
        {
            try
            {
                if (ObjTipoIdentificacion.EliminaTipoIdentificacion(tipoIdentificacion.IdTipoIdentificacion))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(tipoIdentificacion);
                }

            }
            catch (Exception)
            {
                return View(tipoIdentificacion);
                throw;
            }
        }
    }
}