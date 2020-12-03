using BLL;
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
    public class SectorController : Controller
    {
        // GET: Sector
        clsSector ObjSector = new clsSector();

        public ActionResult Index()
        {
            try
            {
                var datos = ObjSector.ConsultarSector();
                    List<Sector> ListaSector = new List<Sector>();

                    foreach (var item in datos)
                    {
                        Sector sector = new Sector();

                        sector.IdSector = item.IdSector;
                        sector.Descripcion = item.Descripcion;
                        sector.Estado = item.Estado;

                        ListaSector.Add(sector);
                    }
                    return View(ListaSector);
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
                var dato = ObjSector.ConsultaSector(id);
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
                var dato = ObjSector.ConsultaSector(id);

                    Sector sector = new Sector();

                    sector.IdSector = dato.IdSector;
                    sector.Descripcion = dato.Descripcion;
                    sector.Estado = dato.Estado;

                    return View(sector);
                
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(Sector sector)
        {
            try
            {
                if (ObjSector.ActualizaSector(sector.IdSector, sector.Descripcion, sector.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(sector);
                }

            }
            catch (Exception)
            {
                return View(sector);
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
        public ActionResult Crear(Sector sector)
        {
            try
            {
                if (ObjSector.AgregaSector(sector.Descripcion, sector.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(sector);
                }

            }
            catch (Exception)
            {
                return View(sector);
                throw;
            }
        }


        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjSector.ConsultaSector(id);

                    Sector sector = new Sector
                    {
                        IdSector = dato.IdSector,
                        Descripcion = dato.Descripcion,
                        Estado = dato.Estado
                    };

                    return View(sector);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Sector sector)
        {
            try
            {
                if (ObjSector.EliminaSector(sector.IdSector))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(sector);
                }

            }
            catch (Exception)
            {
                return View(sector);
                throw;
            }
        }
    }
}