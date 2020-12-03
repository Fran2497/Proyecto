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
    [AuthorizeRole(Role.Administrador)]
    public class RoluserController : Controller
    {

        clsUsuarioRol ObjUsuarioRol = new BLL.clsUsuarioRol();
        clsUsuario ObjUsuario = new clsUsuario();
        clsRol ObjRol = new clsRol();
        public ActionResult Index()
        {
            try
            {
                var datos = ObjUsuarioRol.ConsultarUsuarioRol();
                List<UsuarioRol> ListaUsuarios = new List<UsuarioRol>();

                foreach (var item in datos)
                {
                    UsuarioRol usuarioRol = new UsuarioRol
                    {
                        IdUsuarioRol = item.IdUsuarioRol,
                        IdUsuario = item.IdUsuario,
                        Usuario = item.Usuario,
                        IdRol = item.IdRol,
                        Rol = item.Rol
                    };

                    ListaUsuarios.Add(usuarioRol);
                }
                return View(ListaUsuarios);

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
                var dato = ObjUsuarioRol.ConsultaUsuarioRol(id);
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
                var dato = ObjUsuarioRol.ConsultaUsuarioRol(id);

                UsuarioRol usuario = new UsuarioRol
                {
                    IdUsuarioRol = dato.IdUsuarioRol,
                    IdUsuario = dato.IdUsuario,
                    IdRol = dato.IdRol
                };

                ViewBag.Roles = ListaRoles().Where(x => x.Estado == true);
                ViewBag.Usuarios = ListaUsuarios().Where(x => x.Estado == true);

                return View(usuario);
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }
        [HttpPost]
        public ActionResult Editar(UsuarioRol usuario)
        {
            try
            {
                if (ObjUsuarioRol.ActualizaUsuarioRol(usuario.IdUsuarioRol,usuario.IdUsuario, usuario.IdRol,  Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("IndexUsuario");
                }
                else
                {
                    ViewBag.Roles = ListaRoles().Where(x => x.Estado == true);
                    ViewBag.Usuarios = ListaUsuarios().Where(x => x.Estado == true);
                    return View(usuario);
                }

            }
            catch (Exception)
            {
                ViewBag.Roles = ListaRoles().Where(x => x.Estado == true);
                ViewBag.Usuarios = ListaUsuarios().Where(x => x.Estado == false && x.UsuarioCreacion.Equals("new_user"));
                return View(usuario);
                throw;
            }
        }
        public ActionResult Crear()
        {
            try
            {
                ViewBag.Roles = ListaRoles().Where(x => x.Estado == true);
                ViewBag.Usuarios = ListaUsuarios().Where(x => x.Estado == true);
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Crear(UsuarioRol usuario)
        {
            try
            {
                if (ObjUsuarioRol.AgregaUsuarioRol(usuario.IdUsuario, usuario.IdRol, Session["Identificacion"].ToString()))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Roles = ListaRoles().Where(x => x.Estado == true);
                    ViewBag.Usuarios = ListaUsuarios().Where(x => x.Estado == true);
                    return View(usuario);
                }

            }
            catch (Exception)
            {
                ViewBag.Roles = ListaRoles().Where(x => x.Estado == true);
                ViewBag.Usuarios = ListaUsuarios().Where(x => x.Estado == true);
                return View(usuario);
                throw;
            }
        }
        public ActionResult Eliminar(int id)
        {
            try
            {
                var dato = ObjUsuarioRol.ConsultaUsuarioRol(id);

                UsuarioRol usuario = new UsuarioRol
                {
                    IdUsuarioRol = dato.IdUsuarioRol,
                    IdUsuario = dato.IdUsuario,
                    Usuario = dato.Usuario,
                    IdRol = dato.IdRol,
                    Rol = dato.Rol
                };

                return View(usuario);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Eliminar(UsuarioRol usuario)
        {
            try
            {
                if (ObjUsuarioRol.EliminaUsuario(usuario.IdUsuarioRol))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(usuario);
                }

            }
            catch (Exception)
            {
                return View(usuario);
                throw;
            }
        }


        #region Datos
        private List<ConsultarRolResult> ListaRoles()
        {
            List<ConsultarRolResult> datos = ObjRol.ConsultarRol();
            return datos;
        }
        private List<ConsultarUsuarioResult> ListaUsuarios()
        {
            List<ConsultarUsuarioResult> datos = ObjUsuario.ConsultarUsuario();
            return datos;
        }

        #endregion
    }
}