using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using BLL;

namespace Proyecto.Controllers
{
    [Authorize]
    [AuthorizeRole(Role.Administrador)]
    public class RolController : Controller
    {
        // GET: Rol
        clsRol ObjRol = new clsRol();

        public ActionResult Index()
        {
            try
            {
                var datos = ObjRol.ConsultarRol();
                    List<Rol> ListaRol = new List<Rol>();

                    foreach (var item in datos)
                    {
                        Rol rol = new Rol();

                        rol.IdRol = item.IdRol;
                        rol.Descripcion = item.Descripcion;
                        rol.Estado = item.Estado;

                        ListaRol.Add(rol);
                    }
                    return View(ListaRol);
                
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
                var dato = ObjRol.ConsultaRol(id);
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
                var dato = ObjRol.ConsultaRol(id);

                    Rol rol = new Rol();

                    rol.IdRol = dato.IdRol;
                    rol.Descripcion = dato.Descripcion;
                    rol.Estado = dato.Estado;

                    return View(rol);
                
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(Rol rol)
        {
            try
            {
                if (ObjRol.ActualizaRol(rol.IdRol, rol.Descripcion, rol.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(rol);
                }

            }
            catch (Exception)
            {
                return View(rol);
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
        public ActionResult Crear(Rol rol)
        {
            try
            {
                if (ObjRol.AgregaRol(rol.Descripcion, rol.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(rol);
                }

            }
            catch (Exception)
            {
                return View(rol);
                throw;
            }
        }


        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjRol.ConsultaRol(id);

                    Rol rol = new Rol
                    {
                        IdRol = dato.IdRol,
                        Descripcion = dato.Descripcion,
                        Estado = dato.Estado
                    };

                    return View(rol);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Rol rol)
        {
            try
            {
                if (ObjRol.EliminaRol(rol.IdRol))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(rol);
                }

            }
            catch (Exception)
            {
                return View(rol);
                throw;
            }
        }
    }
}