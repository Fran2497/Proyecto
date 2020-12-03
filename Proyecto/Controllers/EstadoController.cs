using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    [Authorize]
    public class EstadoController : Controller
    {
        // GET: Estado
        clsEstado ObjEstado = new clsEstado();
        public ActionResult Index()
        {
            try
            {
                var datos = ObjEstado.ConsultarEstado();
                    List<Estado> ListaEstado = new List<Estado>();

                    foreach (var item in datos)
                    {
                        Estado estado = new Estado();

                        estado.IdEstado = item.IdEstado;
                        estado.Descripcion = item.Descripcion;
                        estado.Estado_1 = item.Estado;

                        ListaEstado.Add(estado);
                    }
                    return View(ListaEstado);
                
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
                var dato = ObjEstado.ConsultaEstado(id);
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
                var dato = ObjEstado.ConsultaEstado(id);

                    Estado estado = new Estado();

                    estado.IdEstado = dato.IdEstado;
                    estado.Descripcion = dato.Descripcion;
                    estado.Estado_1 = dato.Estado;

                    return View(estado);
                
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(Estado estado)
        {
            try
            {
                if (ObjEstado.ActualizaEstado(estado.IdEstado, estado.Descripcion, estado.Estado_1, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(estado);
                }

            }
            catch (Exception)
            {
                return View(estado);
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
        public ActionResult Crear(Estado estado)
        {
            try
            {
                if (ObjEstado.AgregaEstado(estado.Descripcion, estado.Estado_1, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(estado);
                }

            }
            catch (Exception)
            {
                return View(estado);
                throw;
            }
        }


        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjEstado.ConsultaEstado(id);

                    Estado estado = new Estado
                    {
                        IdEstado = dato.IdEstado,
                        Descripcion = dato.Descripcion,
                        Estado_1 = dato.Estado
                    };

                    return View(estado);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Estado estado)
        {
            try
            {
                if (ObjEstado.EliminaEstado(estado.IdEstado))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(estado);
                }

            }
            catch (Exception)
            {
                return View(estado);
                throw;
            }
        }
    }
}