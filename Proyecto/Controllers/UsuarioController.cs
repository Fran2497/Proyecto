using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.DAL;
using BLL;
using Proyecto.Models;
using Proyecto.Tools;

namespace Proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        clsUsuario ObjUsuario = new clsUsuario();
        clsUsuarioRol objRoluser = new clsUsuarioRol();
        clsEmpresa ObjEmpresa = new clsEmpresa();
        clsBitacora ObjBitacora = new clsBitacora();
        clsTipoIdentificacion ObjTipoIdentificacion = new clsTipoIdentificacion();

        public ActionResult IndexUsuario()
        {
            try
            {
                var datos = ObjUsuario.ConsultarUsuario();
                List<Usuario> ListaUsuarios = new List<Usuario>();

                    foreach (var item in datos)
                    {
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = item.IdUsuario,
                        IdEmpresa = (int)item.IdEmpresa,
                        Empresa = item.Empresa,
                        IdTipoIdentificacion = (int)item.IdTipoIdentificacion,
                        TipoIdentificacion = item.TipoIdentificacion,
                        Identificacion = item.Identificacion,
                        Nombre = item.Nombre,
                        Apellido1 = item.Apellido1,
                        Apellido2 = item.Apellido2,
                        Telefono = item.Telefono,
                        Correo = item.Correo,
                        Clave = item.Clave,
                        Estado = item.Estado,
                        UsuarioCreacion = item.UsuarioCreacion,
                        UsuarioModificacion = item.UsuarioModificacion
                    };

                    ListaUsuarios.Add(usuario);
                    }
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Ver listado de Usuarios", "Se muestra listado de usuarios.", 1);
                return View(ListaUsuarios);
                
            }
            catch (Exception)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Ver listado de Usuarios", "Fallo al mostrar listado de usuarios.", 0);
                throw;
            }
        }

        public ActionResult Consulta(int id)
        {
            try
            {
                var dato = ObjUsuario.ConsultaUsuario(id);
                ViewBag.Clave = Seguridad.DecryptString(Seguridad.SecureKey, dato.Clave);
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Consulta de un Usuario", "Se muestra detalles de usuario.", 1);
                return View(dato);
            }
            catch (Exception)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Consulta de un Usuario", "Error al mostrar detalles de usuario.", 1);
                throw;
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                var dato = ObjUsuario.ConsultaUsuario(id);

                    Usuario usuario = new Usuario();

                    usuario.IdUsuario = dato.IdUsuario;
                    usuario.IdEmpresa = (int)dato.IdEmpresa;
                    usuario.IdTipoIdentificacion = (int)dato.IdTipoIdentificacion;
                    usuario.Identificacion = dato.Identificacion;
                    usuario.Nombre = dato.Nombre;
                    usuario.Apellido1 = dato.Apellido1;
                    usuario.Apellido2 = dato.Apellido2;
                    usuario.Telefono = dato.Telefono;
                    usuario.Correo = dato.Correo;
                    //usuario.Clave = dato.Clave;
                    usuario.Clave = Seguridad.DecryptString(Seguridad.SecureKey, dato.Clave);
                    usuario.Estado = dato.Estado;

                    ViewBag.Empresas = ListaEmpresas();
                    ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Editar Usuario", "Se muestra vista de usuario a editar.", 1);
                return View(usuario);
            }
            catch (Exception)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Editar Usuario", "Error al mostrar vista de usuario a editar.", 0);
                return View();
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(Usuario usuario)
        {
            try
            {
                var claveSegura = Seguridad.EncryptString(Seguridad.SecureKey, usuario.Clave);
                if (ObjUsuario.ActualizaUsuario(usuario.IdUsuario, usuario.IdEmpresa, usuario.IdTipoIdentificacion, usuario.Identificacion, 
                    usuario.Nombre, usuario.Apellido1, usuario.Apellido2, usuario.Telefono, usuario.Correo, claveSegura, usuario.Estado, Session["Identificacion"].ToString()))
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Editar Usuario", "Se edita correctamente un usuario.", 1);
                    return RedirectToAction("IndexUsuario");
                }
                else
                {
                    ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                    ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Editar Usuario", "Error al editar usuario.", 0);
                    return View(usuario);
                }

            }
            catch (Exception)
            {
                ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Editar Usuario", "Error al editar usuario.", 0);
                return View(usuario);
                throw;
            }
        }

        
        public ActionResult Crear()
        {
            try
            {
                ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);

                if (Request.IsAuthenticated)
                {
                    ObjBitacora.RegistraBitacora("New User", "Usuario", "Crear Usuario", "Se muestra vista para crear usuario.", 1);
                }
                else
                {
                    ObjBitacora.RegistraBitacora("new user", "Usuario", "Crear Usuario", "Se muestra vista para crear usuario.", 1);
                }
                
                return View();
            }
            catch (Exception)
            {
                if (Request.IsAuthenticated)
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Crear Usuario", "Se muestra vista para crear usuario.", 1);
                }
                else
                {
                    ObjBitacora.RegistraBitacora("new user", "Usuario", "Crear Usuario", "Se muestra vista para crear usuario.", 1);
                }
                throw;
            }
        }

        [HttpPost]
        public ActionResult Crear(Usuario usuario)
        {
            try
            {
                var user = "";
                var estado = true;
                if (!Request.IsAuthenticated)
                {
                    user = "new_user";
                }
                else
                {
                    user = Session["Identificacion"].ToString();
                    estado = usuario.Estado;
                }
                var claveSegura = Seguridad.EncryptString(Seguridad.SecureKey, usuario.Clave);
                if (ObjUsuario.AgregaUsuarior(usuario.IdEmpresa, usuario.IdTipoIdentificacion, usuario.Identificacion,
                    usuario.Nombre, usuario.Apellido1, usuario.Apellido2, usuario.Telefono, usuario.Correo, claveSegura, 
                    estado, user))
                {
                    if (Request.IsAuthenticated)
                    {
                        ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Crear Usuario", "Se registra un usuario desde administrador.", 1);
                        if (objRoluser.TRAgregaUsuarioRol(1,user)) 
                        {
                            ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Asignar Rol automático", "Se asigna rol de usuario al nuevo usuario.", 1);
                        }
                        return RedirectToAction("IndexUsuario");
                    }
                    else
                    {
                        ObjBitacora.RegistraBitacora("new user", "Usuario", "Crear Usuario", "Se realiza un autoregistro de un nuevo usuario.", 1);
                        if (objRoluser.TRAgregaUsuarioRol(1, user))
                        {
                            ObjBitacora.RegistraBitacora("new user", "Usuario", "Asignar Rol automático", "Se asigna rol de usuario al nuevo usuario.", 1);
                        }
                        return RedirectToAction("Login", "Login");
                    }
                }
                else
                {
                    if (Request.IsAuthenticated)
                    {
                        ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Crear Usuario", "Error al crear nuevo usuario.", 0);
                    }
                    else
                    {
                        ObjBitacora.RegistraBitacora("new user", "Usuario", "Crear Usuario", "Error al crear nuevo usuario.", 0);
                    }
                    ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                    ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                    return View(usuario);
                }

            }
            catch (Exception)
            {
                if (Request.IsAuthenticated)
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Crear Usuario", "Error al crear nuevo usuario.", 1);
                }
                else
                {
                    ObjBitacora.RegistraBitacora("new user", "Usuario", "Crear Usuario", "Error al crear nuevo usuario.", 1);
                }
                ViewBag.TipoIdentificaciones = ListaTipoIdentificacion().Where(x => x.Estado == true);
                ViewBag.Empresas = ListaEmpresas().Where(x => x.Estado == true);
                return View(usuario);
                throw;
            }
        }

        [AuthorizeRole(Role.Administrador, Role.Supervisor)]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjUsuario.ConsultaUsuario(id);

                    Usuario usuario = new Usuario
                    {
                        IdUsuario = dato.IdUsuario,
                        IdEmpresa = (int)dato.IdEmpresa,
                        Empresa = dato.Empresa,
                        IdTipoIdentificacion = (int)dato.IdTipoIdentificacion,
                        TipoIdentificacion = dato.TipoIdentificacion,
                        Identificacion = dato.Identificacion,
                        Nombre = dato.Nombre,
                        Apellido1 = dato.Apellido1,
                        Apellido2 = dato.Apellido2,
                        Telefono = dato.Telefono,
                        Correo = dato.Correo,
                        Clave = Seguridad.DecryptString(Seguridad.SecureKey, dato.Clave),
                        Estado = dato.Estado
                    };
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Eliminar Usuario", "Se muestra vista de usuario a eliminar.", 1);
                return View(usuario);
                
            }
            catch (Exception)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Eliminar Usuario", "Error al mostrar vista de usuario a eliminar.", 0);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Usuario usuario)
        {
            try
            {
                if (ObjUsuario.EliminaUsuario(usuario.IdUsuario))
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Eliminar Usuario", "Se eliminó un usuario.", 1);
                    if (objRoluser.EliminaUsuario(usuario.IdUsuario))
                    {
                        ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Eliminar Rol de Usuario", "Se elimina rol automaticamente al eliminar el usuario.", 1);
                    }
                    return RedirectToAction("IndexUsuario");
                }
                else
                {
                    ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Eliminar Usuario", "Error al eliminar usuario.", 0);
                    return View(usuario);
                }

            }
            catch (Exception)
            {
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Usuario", "Eliminar Usuario", "Error al eliminar usuario.", 0);
                return View(usuario);
                throw;
            }
        }


        #region Datos
        private List<ConsultarTipoIdentificacionResult> ListaTipoIdentificacion()
        {
            List<ConsultarTipoIdentificacionResult> datos = ObjTipoIdentificacion.ConsultarTipoIdentificacion();
            return datos;
        }
        private List<ConsultarEmpresaResult> ListaEmpresas()
        {
            List<ConsultarEmpresaResult> datos = ObjEmpresa.ConsultarEmpresa();
            return datos;
        }

        #endregion
    }
}