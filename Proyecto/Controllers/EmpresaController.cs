using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Proyecto.Models;
using QRCoder;
using Proyecto.DAL;

namespace Proyecto.Controllers
{
    [Authorize]
    [AuthorizeRole(Role.Administrador, Role.Supervisor)]
    public class EmpresaController : Controller
    {
        // GET: Empresa
        clsEmpresa ObjEmpresa = new clsEmpresa();
        clsSector ObjSector = new clsSector();
        clsConsulta ObjConsulta = new clsConsulta();
        clsBitacora ObjBitacora = new clsBitacora();

        public ActionResult Index()
        {
            try
            {
                var datos = ObjEmpresa.ConsultarEmpresa();
                List<Empresa> ListaEmpresas = new List<Empresa>();

                foreach (var item in datos)
                {
                    Empresa empresa = new Empresa
                    {
                        IdEmpresa = item.IdEmpresa,
                        Descripcion = item.Descripcion,
                        IdTipoIdentificacion = item.IdTipoIdentificacion,
                        TipoIdentificacion = item.TipoIdentificacion,
                        Identificacion = item.Identificacion,
                        Direccion = item.Direccion,
                        Telefono = item.Telefono,
                        IdSector = item.IdSector,
                        Sector = item.Sector,
                        Provincia = (char)item.Provincia,
                        Canton = item.Canton,
                        Distrito = item.Distrito,
                        Estado = item.Estado,
                        ProvinciaDesc = item.N_Provincia,
                        CantonDesc = item.N_Canton,
                        DistritoDesc = item.N_Distrito
                    };

                    ListaEmpresas.Add(empresa);
                }

                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Ver listado de Empresas", "Se muestra listado de empresas.", 1);
                return View(ListaEmpresas);
            }
            catch (Exception)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Ver listado de Empresas", "Fallo al mostrar listado de empresas.", 0);
                throw;
            }
        }
        public ActionResult Consulta(int id)
        {
            try
            {
                var dato = ObjEmpresa.ConsultaEmpresa(id);
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Consulta de una Empresa", "Se muestra detalles de empresa.", 1);
                return View(dato);
            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Consulta de una Empresa", "Error al mostrar detalles de empresa.", 1);
                ViewBag.Error = "Error: " + e.Message;
                throw;
            }
        }
        public ActionResult Editar(int id)
        {
            try
            {
                var dato = ObjEmpresa.ConsultaEmpresa(id);

                Empresa empresa = new Empresa();

                empresa.IdEmpresa = dato.IdEmpresa;
                empresa.Descripcion = dato.Descripcion;
                empresa.IdTipoIdentificacion = dato.IdTipoIdentificacion;
                empresa.Identificacion = dato.Identificacion;
                empresa.Direccion = dato.Direccion;
                empresa.Telefono = dato.Telefono;
                empresa.IdSector = dato.IdSector;
                empresa.Provincia = (char)dato.Provincia;
                empresa.Canton = dato.Canton;
                empresa.Distrito = dato.Distrito;
                empresa.Estado = dato.Estado;

                ViewBag.Provincias = ListaProvincias();
                ViewBag.Cantones = ListaCantones((char)dato.Provincia);
                ViewBag.Distritos = ListaDistritos((char)dato.Provincia, dato.Canton);
                ViewBag.Sectores = ObjSector.ConsultarSector().Where(x => x.Estado == true).ToList();
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Editar Empresa", "Se muestra vista de empresa a editar.", 1);
                return View(empresa);
            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Editar Empresa", "Error al mostrar vista de empresa a editar.", 0);
                ViewBag.Error = "Error: " + e.Message;
                throw;
            }
        }
        [HttpPost]
        public ActionResult Editar(Empresa empresa)
        {
            try
            {
                if (ObjEmpresa.ActualizaEmpresa(empresa.IdEmpresa, empresa.Descripcion, 2, empresa.Identificacion, empresa.Direccion, empresa.Telefono, empresa.IdSector, empresa.Provincia, empresa.Canton, empresa.Distrito, empresa.Estado, Session["Identificacion"].ToString()))
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Editar Empresa", "Se edita empresa.", 1);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Provincias = ListaProvincias();
                    ViewBag.Cantones = ListaCantones(empresa.Provincia);
                    ViewBag.Distritos = ListaDistritos(empresa.Provincia, empresa.Canton);
                    ViewBag.Sectores = ObjSector.ConsultarSector().Where(x => x.Estado == true).ToList();
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Editar Empresa", "Error al editar empresa.", 0);
                    return View(empresa);
                }               
            }
            catch (Exception e)
            {
                ViewBag.Provincias = ListaProvincias();
                ViewBag.Cantones = ListaCantones(empresa.Provincia);
                ViewBag.Distritos = ListaDistritos(empresa.Provincia, empresa.Canton);
                ViewBag.Sectores = ObjSector.ConsultarSector().Where(x => x.Estado == true).ToList();
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Editar Empresa", "Error al editar empresa.", 0);
                ViewBag.Error = "Error: " + e.Message;
                return View(empresa);
                throw;
            }
            
        }

        public ActionResult Crear()
        {
            try
            {
                ViewBag.Provincias = ListaProvincias();
                ViewBag.Sectores = ObjSector.ConsultarSector().Where(x => x.Estado == true).ToList();
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), this.ControllerContext.Controller.ToString(), "Crear Empresa", "Se muestra vista de empresa a crear.", 1);
                return View();
            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Creat Empresa", "Error al mostrar vista de empresa a crear.", 0);
                ViewBag.Error = "Error: " + e.Message;
                throw;
            }
        }
        [HttpPost]
        public ActionResult Crear(Empresa empresa)
        {
            try
            {
                if (ObjEmpresa.CreaEmpresa(empresa.Descripcion, 2, empresa.Identificacion, empresa.Direccion, empresa.Telefono, empresa.IdSector, empresa.Provincia, empresa.Canton, empresa.Distrito, empresa.Estado, Session["Identificacion"].ToString()))
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Crear Empresa", "Se crea empresa correctamente.", 1);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Provincias = ListaProvincias();
                    ViewBag.Cantones = ListaCantones(empresa.Provincia);
                    ViewBag.Distritos = ListaDistritos(empresa.Provincia, empresa.Canton);
                    ViewBag.Sectores = ObjSector.ConsultarSector().Where(x => x.Estado == true).ToList();
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Crear Empresa", "Error al crear empresa.", 0);
                    return View(empresa);
                }
            }
            catch (Exception e)
            {
                ViewBag.Provincias = ListaProvincias();
                ViewBag.Cantones = ListaCantones(empresa.Provincia);
                ViewBag.Distritos = ListaDistritos(empresa.Provincia, empresa.Canton);
                ViewBag.Sectores = ObjSector.ConsultarSector().Where(x => x.Estado == true).ToList();
                ViewBag.Error = "Error: " + e.Message; 
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Crear Empresa", "Error al crear empresa.", 0);
                return View(empresa);
                throw;
            }

        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjEmpresa.ConsultaEmpresa(id);

                Empresa empresa = new Empresa
                {
                    IdEmpresa = dato.IdEmpresa,
                    Descripcion = dato.Descripcion,
                    IdTipoIdentificacion = dato.IdTipoIdentificacion,
                    TipoIdentificacion = dato.TipoIdentificacion,
                    Identificacion = dato.Identificacion,
                    Direccion = dato.Direccion,
                    Telefono = dato.Telefono,
                    IdSector = dato.IdSector,
                    Sector = dato.Sector,
                    Provincia = (char)dato.Provincia,
                    ProvinciaDesc = dato.N_Provincia,
                    Canton = dato.Canton,
                    CantonDesc = dato.N_Canton,
                    Distrito = dato.Distrito,
                    DistritoDesc = dato.N_Distrito,
                    Estado = dato.Estado
                };
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Eliminar Empresa", "Se muestra vista de empresa a eliminar.", 1);
                return View(empresa);
            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Eliminar Empresa", "Error al mostrar vista de empresa a eliminar.", 0);
                ViewBag.Error = "Error: " + e.Message;
                throw;
            }
        }
        [HttpPost]
        public ActionResult Eliminar(Empresa empresa)
        {
            try
            {
                if (ObjEmpresa.EliminaEmpresa(empresa.IdEmpresa))
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Eliminar Empresa", "Se elimina empresa correctamente.", 1);
                    return RedirectToAction("Index");
                }
                else
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Eliminar Empresa", "Error al eliminar empresa.", 0);
                    return View(empresa);
                }
            }
            catch (Exception e)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Empresa", "Eliminar Empresa", "Error al eliminar empresa.", 0);
                ViewBag.Error = "Error: " + e.Message;
                return View(empresa);
                throw;
            }

        }

        #region Datos
        private List<ProvinciasResult> ListaProvincias()
        {
            List<ProvinciasResult> datos = ObjConsulta.ConsultarProvincia();
            return datos;
        }
        private List<CantonesResult> ListaCantones(char Provincia)
        {
            List<CantonesResult> datos = ObjConsulta.ConsultarCanton(Provincia);
            return datos;
        }
        private List<DistritosResult> ListaDistritos(char Provincia, string Canton)
        {
            List<DistritosResult> datos = ObjConsulta.ConsultarDistrito(Provincia, Canton);
            return datos;
        }
        [HttpPost]
        public JsonResult CargaCantones(char provincia)
        {
            List<CantonesResult> cantones = ObjConsulta.ConsultarCanton(provincia);
            return Json(cantones, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargaDistritos(char provincia, string canton)
        {
            List<DistritosResult> distritos = ObjConsulta.ConsultarDistrito(provincia, canton);
            return Json(distritos, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}