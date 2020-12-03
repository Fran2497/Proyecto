using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Proyecto.Models;
using System.Web.Security;
using System.Configuration;
using Proyecto.Tools;

namespace Proyecto.Controllers
{
    public class LoginController : Controller
    {
        clsUsuario ObjUsuario = new clsUsuario();
        clsSector ObjSector = new clsSector();
        clsBitacora ObjBitacora = new clsBitacora();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login,string ReturnUrl)
        {
            var SecretKey = ConfigurationManager.AppSettings["SecretKey"];
            if (ModelState.IsValid)
            {
                var clavesegura = Seguridad.EncryptString(SecretKey, login.Clave);
                //var claveorigen = Seguridad.DecryptString(SecretKey, clavesegura);
                var datos = ObjUsuario.ValidaUsuario(login.Correo, clavesegura);
                if (datos==null)
                {
                    ViewBag.Error = "¡Correo o contraseña inválidos!";
                    ObjBitacora.RegistraBitacora(login.Correo, "Login", "Ingreso", "Credenciales de ingreso no válidas", 0);
                    return View(login);
                }
                else
                {
                    var ListaRoles = ObjUsuario.UsuarioRoles(datos.IdUsuario);

                    var roles = String.Join(",", ListaRoles.Select(x => x.Rol));

                    Session["Identificacion"] = datos.Identificacion.ToString();
                    Session["Telefono"] = datos.Telefono.ToString();
                    Session["Empresa"] = datos.IdEmpresa;
                    Session["Nombre"] = datos.Nombre;
                    Session["Roles"] = roles;
                    //FormsAuthentication.SetAuthCookie(datos.Nombre + " " + datos.Apellido1 + " " +  datos.Apellido2, login.Recordarme);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, login.Correo, DateTime.Now, DateTime.Now.AddMinutes(30), login.Recordarme, roles, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);

                    Session["Identificacion"] = datos.Identificacion;
                    if (Request.Browser.IsMobileDevice)
                    {
                        ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Login", "Ingreso", "Ingreso en dispositivo móvil", 1);
                        return RedirectToAction("Index", "Carnet");
                    }
                    else
                    {
                        if (roles.Equals("Usuario"))
                        {
                            ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Login", "Ingreso", "Ingreso de usuario", 1);
                            return RedirectToAction("IndexProductos", "Home", new { idEmp = datos.IdEmpresa });
                        }
                        else if(roles.Equals("Administrador") || roles.Equals("Supervisor"))
                        {
                            ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Login", "Ingreso", "Ingreso de administrador o supervisor", 1);
                            return RedirectToAction("Index","Home");
                        }
                        else
                        {
                            ViewBag.Error = "¡Usuario sin Rol asignado!";
                            ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Login", "Ingreso", "Usuario sin rol asignado", 0);
                            return View(login);
                        }
                        
                    }

                }
            }
            else
            {
                ViewBag.Error = "¡Correo o contraseña inválidos!";
                ObjBitacora.RegistraBitacora(Session["Identificacion"].ToString(), "Login", "Ingreso", "Credenciales de ingreso no válidas", 0);
                return View(login);
            }
           
        }
        public ActionResult Salir()
        {
            Session.Remove("Identificacion");
            Session.RemoveAll();
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cache.SetNoServerCaching();
            Request.Cookies.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}