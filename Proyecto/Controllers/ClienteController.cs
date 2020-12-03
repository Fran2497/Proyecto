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
    public class ClienteController : Controller
    {
        // GET: Cliente
        clsTipoIdentificacion ObjTipoIdentificacion = new clsTipoIdentificacion();
        clsCliente ObjCliente = new clsCliente();

        public ActionResult Index()
        {
            try
            {
                var datos = ObjCliente.ConsultarCliente();
                    List<Cliente> ListaClientes = new List<Cliente>();

                    foreach (var item in datos)
                    {
                    Cliente cliente = new Cliente
                    {
                        IdCliente = item.IdCliente,
                        IdTipoIdentificacion = (int)item.IdTipoIdentificacion,
                        TipoIdentificacion = item.TipoIdentificacion,
                        Identificacion = item.Identificacion,
                        Correo = item.Correo,
                        Estado = item.Estado
                    };

                    ListaClientes.Add(cliente);
                    }
                    return View(ListaClientes);
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
                var dato = ObjCliente.ConsultaCliente(id);
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
                var dato = ObjCliente.ConsultaCliente(id);

                    Cliente cliente = new Cliente();

                    cliente.IdCliente = dato.IdCliente;
                    cliente.IdTipoIdentificacion = (int)dato.IdTipoIdentificacion;
                    cliente.Identificacion = dato.Identificacion;
                    cliente.Correo = dato.Correo;
                    cliente.Estado = dato.Estado;

                    ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);

                    return View(cliente);
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(Cliente cliente)
        {
            try
            {
                if (ObjCliente.ActualizaCliente(cliente.IdCliente, cliente.IdTipoIdentificacion, cliente.Identificacion, cliente.Correo, 
                    cliente.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                    return View(cliente);
                }

            }
            catch (Exception)
            {
                ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                return View(cliente);
                throw;
            }
        }


        public ActionResult Crear()
        {
            try
            {
                ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Crear(Cliente cliente)
        {
            try
            {
                if (ObjCliente.AgregaCliente(cliente.IdTipoIdentificacion, cliente.Identificacion, cliente.Correo, cliente.Estado, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                    return View(cliente);
                }

            }
            catch (Exception)
            {
                ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                return View(cliente);
                throw;
            }
        }


        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjCliente.ConsultaCliente(id);

                    Cliente cliente = new Cliente
                    {
                        IdCliente = dato.IdCliente,
                        IdTipoIdentificacion = (int)dato.IdTipoIdentificacion,
                        TipoIdentificacion = dato.TipoIdentificacion,
                        Identificacion = dato.Identificacion,
                        Correo = dato.Correo,
                        Estado = dato.Estado
                    };

                    return View(cliente);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Cliente cliente)
        {
            try
            {
                if (ObjCliente.EliminaCliente(cliente.IdCliente))
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(cliente);
                }

            }
            catch (Exception)
            {
                return View(cliente);
                throw;
            }
        }

        #region Datos
        private List<ConsultarTipoIdentificacionResult> ListaTipoIdentificacion()
        {
            List<ConsultarTipoIdentificacionResult> datos = ObjTipoIdentificacion.ConsultarTipoIdentificacion();
            return datos;
        }
        #endregion
    }
}