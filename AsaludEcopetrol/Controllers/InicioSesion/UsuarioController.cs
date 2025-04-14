using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using AsaludEcopetrol.BussinesManager;
using System.Web.Routing;
using Facede;
using ECOPETROL_COMMON.ENUM;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mail;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Configuration;

namespace AsaludEcopetrol.Controllers.InicioSesion
{

    public class UsuarioController : Controller
    {

        #region PROPIEDADES

        private SessionState _SesionVar;
        public SessionState SesionVar
        {
            get
            {
                if (_SesionVar == null)
                {
                    _SesionVar = new SessionState();
                }
                return _SesionVar;
            }
            set { _SesionVar = value; }
        }

        Facade BusClass = new Facade();

        #endregion

        #region EVENTOS PRIVADOS


        #endregion

        #region METODOS


        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
        // GET: Usuario

        public ActionResult Login()
        {
            ViewBag.logo = BusClass.GetParametro("Logo");
            ViewBag.sami = BusClass.GetParametro("SAMIFIS");
            ViewBag.hsflogo = BusClass.GetParametro("HSFLOGO");
            ViewBag.logoAsalud = BusClass.GetParametro("LOGO ASALUD SELLO");
            ViewBag.logoSami = BusClass.GetParametro("LOGOSAMI");

            return View();
        }

        public ActionResult Login2()
        {
            return View();
        }

        public ActionResult LoginCerrado()
        {
            return View();
        }

        public ActionResult Inicio()
        {
            ViewBag.rol = SesionVar.ROL;
            List<Management_sis_actividad_recienteResult> ListActividadReciente = BusClass.GetListActividadReciente();
            ListActividadReciente = ListActividadReciente.Where(l => l.horas <= 12).ToList();
            ViewBag.actividadReciente = ListActividadReciente.OrderBy(l => l.minutos).ToList();
            System.GC.Collect();
            System.GC.WaitForFullGCComplete();

            ViewBag.proyectos = BusClass.ChatBotProyectos();

            return View(ListActividadReciente);
        }


        public string ObtenerOpciones(int? tipo, int? idProyecto, int? idProceso, int? idsubProceso, int? idPregunta)
        {
            string result = "";
            List<Management_chatbot_ref_proyectosResult> proyecto = new List<Management_chatbot_ref_proyectosResult>();
            List<Management_chatbot_ref_procesosResult> procesos = new List<Management_chatbot_ref_procesosResult>();
            List<Management_chatbot_ref_subprocesosResult> subprocesos = new List<Management_chatbot_ref_subprocesosResult>();
            List<Management_chatbot_ref_preguntasResult> preguntas = new List<Management_chatbot_ref_preguntasResult>();
            List<Management_chatbot_ref_respuestasResult> respuestas = new List<Management_chatbot_ref_respuestasResult>();

            try
            {
                if (tipo == 0)
                {
                    proyecto = BusClass.ChatBotProyectos();

                    result = "<label class='text-secondary_asalud'>Seleccione el proyecto</label>";
                    result += "<input type='hidden' id='tipoPreguntaAnterior' name='tipoPreguntaAnterior' value='" + 0 + "'>";
                    //result += "<input type='hidden' id='respuestaAnterior' name='respuestaAnterior' value='" + 0 + "'>";

                    foreach (var item in proyecto)
                    {
                        result += " <div class='col-md-12 text-center'>";
                        result += " <a class='btn botonChat' onclick='LlenarOpcionesMensaje(1, " + item.id_chatbot_ref_proyecto + ", 1)'>" + item.descripcion + "</a>";
                        result += " </div>";
                        result += " <br />";
                        result += " <br />";
                    }
                }

                else if (tipo == 1)
                {
                    procesos = BusClass.ChatBotProcesos(idProyecto);

                    result = "<label class='text-secondary_asalud'>Seleccione el proceso</label>";

                    //result += " <div class='col-md-12 text-center'>";
                    //result += "<a class='btn botonAnterior' onclick='LlenarOpcionesMensaje(" + 0 + ", " + 0 + ")'> Volver al menú anterior </a>";
                    //result += " </div>";
                    //result += " <br />";
                    //result += " <br />";

                    result += "<input type='hidden' id='tipoPreguntaAnterior' name='tipoPreguntaAnterior' value='" + (tipo - 1) + "'>";
                    //result += "<input type='hidden' id='respuestaAnterior' name='respuestaAnterior' value='" + respuesta + "'>";

                    foreach (var item in procesos)
                    {
                        result += " <div class='col-md-12 text-center'>";
                        result += " <a class='btn botonChat' onclick='LlenarOpcionesMensaje(2, " + item.id_chatbot_ref_proceso + ", 1)'>" + item.descripcion + "</a>";
                        result += " </div>";
                        result += " <br />";
                        result += " <br />";

                    }
                }

                else if (tipo == 2)
                {
                    subprocesos = BusClass.ChatBotSubProcesos(idProceso);

                    result = "<label class='text-secondary_asalud'>Seleccione el sub proceso</label>";

                    result += " <div class='col-md-12 text-center'>";
                    result += "<a class='btn botonAnterior' onclick='LlenarOpcionesMensaje(1, " + idProceso + ")'> Volver al menú anterior </a>";
                    result += " </div>";
                    result += " <br />";
                    result += " <br />";

                    result += "<input type='hidden' id='tipoPreguntaAnterior' name='tipoPreguntaAnterior' value='" + (tipo - 1) + "'>";
                    //result += "<input type='hidden' id='respuestaAnterior' name='respuestaAnterior' value='" + respuesta + "'>";


                    foreach (var item in subprocesos)
                    {
                        result += " <div class='col-md-12 text-center'>";
                        result += " <a class='btn botonChat' onclick='LlenarOpcionesMensaje(3, " + item.id_chatbot_ref_subproceso + ", 1)'>" + item.descripcion + "</a>";
                        result += " </div>";
                        result += " <br />";
                        result += " <br />";
                    }
                }

                else if (tipo == 3)
                {
                    preguntas = BusClass.ChatBotPreguntas(idsubProceso);

                    result = "<label class='text-secondary_asalud'>Preguntas: </label>";

                    result += " <div class='col-md-12 text-center'>";
                    result += "<a class='btn botonAnterior' onclick='LlenarOpcionesMensaje(2, " + idsubProceso + ")'> Volver al menú anterior </a>";
                    result += " </div>";
                    result += " <br />";
                    result += " <br />";

                    result += "<input type='hidden' id='tipoPreguntaAnterior' name='tipoPreguntaAnterior' value='" + (tipo - 1) + "'>";
                    //result += "<input type='hidden' id='respuestaAnterior' name='respuestaAnterior' value='" + respuesta + "'>";
                    //result += "<input type='hidden' id='subProceso' name='subProceso' value='" + respuesta + "'>";

                    foreach (var item in preguntas)
                    {
                        result += " <div class='col-md-12 text-center'>";

                        result += " <a class='btn botonChat' onclick='LlenarOpcionesMensaje(4, " + item.id_chatbot_ref_pregunta + ", 1)'>" + item.descripcion + "</a>";

                        result += " </div>";
                        result += " <br />";
                        result += " <br />";
                    }
                }

                else if (tipo == 4)
                {
                    respuestas = BusClass.ChatBotRespuestas(idPregunta);

                    result = "<label class='text-secondary_asalud'>Respuestas: </label>";

                    result += " <div class='col-md-12 text-center'>";
                    result += "<a class='btn botonAnterior' onclick='LlenarOpcionesMensaje(3, " + idPregunta + ")'> Volver al menú anterior </a>";
                    result += " </div>";
                    result += " <br />";
                    result += " <br />";

                    result += "<input type='hidden' id='tipoPreguntaAnterior' name='tipoPreguntaAnterior' value='" + (tipo - 1) + "'>";
                    //result += "<input type='hidden' id='respuestaAnterior' name='respuestaAnterior' value='" + respuesta + "'>";

                    foreach (var item in respuestas)
                    {
                        result += " <div class='col-md-12 text-center'>";

                        result += " <a class='btn botonChat'>" + item.descripcion + "</a>";
                        result += " </div>";
                        result += " <br />";
                        result += " <br />";
                    }
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }



        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult CambiarClave2()
        {
            return View();
        }

        /// <summary>
        /// Autor: Alexis Quñones castillo
        /// Fecha: 11 de abirl de 2023
        /// </summary>
        /// <returns></returns>
        public ActionResult CambiarClave3()
        {
            ViewBag.logo = BusClass.GetParametro("Logo");
            ViewBag.message = "Señor(a) usuario(a). La contraseña ha expirado. Por políticas de seguridad de la información es necesario que sea actualizada para poder continuar usando el aplicativo.";
            return View();
        }

        /// <summary>
        /// Autor: Alexis Quñones castillo
        /// Fecha: 11 de abirl de 2023
        /// Metodo post para actualizar la contraseña cuando ya ha expirado.
        /// </summary>
        /// <param name="nuevaClave"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CambiarClave3(string claveAnterior, string nuevaClave)
        {
            try
            {
                Models.InicioSesion.CambioClave Model = new Models.InicioSesion.CambioClave();

                string usuario = this.Session["usuario"].ToString();

                string claveActualSesion = this.Session["claveActual"].ToString();
                string claveActualCifrada = "temporal";
                if (claveActualSesion != "temporal")
                {
                    claveActualCifrada = Models.InicioSesion.CambioClave.GetSHA1(claveAnterior);
                }

                if (claveActualCifrada == claveActualSesion)
                {
                    Model.ActualizaContraseña(usuario, nuevaClave, ref MsgRes);
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        sis_usuario auditorUsuario = BusClass.BuscaAuditorUsu(usuario, ref MsgRes).FirstOrDefault();
                        if (auditorUsuario != null)
                        {
                            this.Session["idUsuario"] = auditorUsuario.id_usuario;
                            this.Session["rol"] = auditorUsuario.id_rol;
                            this.Session["nombre"] = auditorUsuario.nombre;

                            FormsAuthentication.SetAuthCookie(usuario, true);
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usuario, DateTime.Now, DateTime.Now.AddMinutes(10), true, usuario);
                            String Encrypt = FormsAuthentication.Encrypt(ticket);
                            HttpCookie cookie = new HttpCookie("Usuario", Encrypt);

                            Response.Cookies.Add(cookie);

                            FormsAuthentication.RedirectFromLoginPage(auditorUsuario.nombre, false);
                            SesionVar.IDUser = auditorUsuario.id_usuario;
                            SesionVar.UserName = auditorUsuario.usuario;
                            SesionVar.NombreUsuario = auditorUsuario.nombre;
                            SesionVar.ROL = auditorUsuario.id_rol.ToString();
                            SesionVar.Estado = auditorUsuario.id_estado.ToString();
                            SesionVar.ROL_CARGO = auditorUsuario.id_rol_cargo.ToString();

                            sis_usuario Obj = new sis_usuario();
                            Obj.usuario = auditorUsuario.usuario;
                            Obj.estado_usuario = "A";

                            // user.ActualizaEstadoUsuario(Obj, ref MsgRes);
                            Models.CuentasMedicas.RadicacionElectronica ModelII = new Models.CuentasMedicas.RadicacionElectronica();
                            this.Session["firmaDigital"] = false;
                            var BaseImagen = ModelII.GetFirmas(auditorUsuario.id_usuario);
                            if (BaseImagen != null)
                            {
                                this.Session["firmaDigital"] = true;
                            }

                            /*Insertar log inicio de sesion */

                            Log_InicioSession logInicioSesion = new Log_InicioSession();
                            logInicioSesion.usuario = auditorUsuario.usuario;
                            logInicioSesion.fecha_ingreso = DateTime.Now;
                            logInicioSesion.host = GetComputerName(Request.UserHostAddress);//User.Identity.Name;
                            logInicioSesion.direccion_ip = Request.UserHostAddress; //GetIPAddress();
                            BusClass.InsertarLogInicioSesion(logInicioSesion, ref MsgRes);

                            return RedirectToAction("Inicio", "Usuario");
                        }
                        else
                        {
                            ViewBag.message = "No ha sido encontrado el usuario en base de datos. Por favor intente nuevamente";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.message = "Ha ocurrido un error al momento de actualizar la contraseña";
                        return View();
                    }
                }
                else
                {
                    ViewBag.message = "Lo sentimos. La clave actual ingresada no coincide con la clave registrada en nuetra base de datos.";
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.message = "Ha ocurrido un error al momento procesar la solicitud: " + ex.Message;
                return View();
            }
        }

        public ActionResult ControlErrores(string Mensaje)
        {
            ViewBag.mensaje = Mensaje;
            return View();
        }

        public PartialViewResult GestionCambiodeclave(int ID, String codigo)
        {
            Models.InicioSesion.Login Model = new Models.InicioSesion.Login();

            ViewBag.codigo = codigo;
            ViewBag.id_usuario = ID;

            return PartialView(Model);
        }

        public JsonResult SaveValidarUsuario(Models.InicioSesion.Login Model)
        {
            String mensaje = "";
            String Alerta = "";
            try
            {
                sis_usuario ListAuditor = new sis_usuario();

                ListAuditor = Model.ValidaIngresoClave(Model.usuarioClave, Model.numero_documento);

                if (ListAuditor != null)
                {

                    if (ListAuditor.correo_ins != null)
                    {
                        EnviarCaso(ListAuditor.correo_ins, ListAuditor.codigo);
                        return Json(new { success = true, message = mensaje, id = ListAuditor.id_usuario, codigo = ListAuditor.codigo, opc = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "*---ERROR -- - *" + " " + "Sin correo institucional, por favor solicitar la creación de correo en SAMI…";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    mensaje = "*---ERROR -- - *" + " " + "Sin registros para estos datos…";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {

                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult Logout()
        {

            HttpContext.User = new GenericPrincipal(new GenericIdentity(""), null);
            FormsAuthentication.SignOut();

            //HttpCookie cookie = Request.Cookies["Usuario"];
            //String data = cookie.Value;
            //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(data);

            //String username = ticket.Name;

            //Models.InicioSesion.Login user = new Models.InicioSesion.Login();

            //sis_usuario Obj = new sis_usuario();

            //Obj.usuario = username;
            //Obj.estado_usuario = "I";

            //user.ActualizaEstadoUsuario(Obj, ref MsgRes);

            //cookie.Expires = DateTime.Now.AddYears(-1);
            //Response.Cookies.Add(cookie);

            Session.Abandon();
            return RedirectToAction("Login", "Usuario");


        }

        public ActionResult LogoutSession()
        {
            HttpContext.User = new GenericPrincipal(new GenericIdentity(""), null);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Write("<script language=javascript>alert('la sesión ha expirado....');</script>");

            return View();
        }

        public ActionResult CreacionUsuarioSami()
        {
            Models.InicioSesion.Usuarios Model = new Models.InicioSesion.Usuarios();

            ViewBag.rol = BusClass.Ref_rol_Usuario();
            ViewBag.cargo = BusClass.Ref_cargo_Usuario();

            return View();
        }

        //[HttpPost]
        //public ActionResult Login(Models.InicioSesion.Login user)
        //{
        //    ViewBag.logo = BusClass.GetParametro("Logo");
        //    if (ModelState.IsValid)
        //    {
        //        List<sis_usuario> ListAuditor = new List<sis_usuario>();
        //        ListAuditor = user.ValidaIngreso(user.usuario, user.contraseña);

        //        if (ListAuditor.Count != 0)
        //        {
        //            this.Session["usuario"] = user.usuario;
        //            return RedirectToAction("ValidacionCodigo", "Usuario");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "!El usuario o contraseña son invalidos!");
        //        }
        //    }
        //    return View(user);

        //}


        [HttpPost]
        public ActionResult Login(Models.InicioSesion.Login user)
        {
            ViewBag.logo = BusClass.GetParametro("Logo");
            ViewBag.sami = BusClass.GetParametro("SAMIFIS");
            ViewBag.hsflogo = BusClass.GetParametro("HSFLOGO");
            ViewBag.logoAsalud = BusClass.GetParametro("LOGO ASALUD SELLO");
            ViewBag.logoSami = BusClass.GetParametro("LOGOSAMI");

            if (ModelState.IsValid)
            {
                List<sis_usuario> ListAuditor = new List<sis_usuario>();
                ListAuditor = user.ValidaIngreso(user.usuario, user.contraseña);

                if (ListAuditor.Count != 0)
                {
                    if (ConfigurationManager.AppSettings["DobleAutentificacion"].ToString() == "1")
                    {
                        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                        {
                            this.Session["usuario"] = user.usuario;
                            return RedirectToAction("ValidacionCodigo", "Usuario");
                        }

                        else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                        {
                            this.Session["usuario"] = user.usuario;
                            return RedirectToAction("ValidacionCodigo", "Usuario");
                        }
                    }
                    else
                    {
                        sis_usuario usuarioLogin = ListAuditor.FirstOrDefault();

                        this.Session["idUsuario"] = usuarioLogin.id_usuario;
                        this.Session["usuario"] = usuarioLogin.usuario;
                        this.Session["rol"] = usuarioLogin.id_rol;
                        this.Session["nombre"] = usuarioLogin.nombre;
                        this.Session["claveActual"] = usuarioLogin.contraseña;

                        /*Si la contraseña es temporal, sera redirigido a actualizar la contraseña*/

                        if (user.contraseña == "temporal" || usuarioLogin.actualiza_contraseña == null)
                        {
                            return RedirectToAction("CambiarClave3", "Usuario");
                        }

                        int totalDias = 0;
                        if (usuarioLogin.actualiza_contraseña != null)
                        {
                            TimeSpan difFechas = DateTime.Now - usuarioLogin.actualiza_contraseña.Value;
                            totalDias = difFechas.Days;
                        }

                        if (totalDias <= 30)
                        {
                            List<sis_usuario> ListAuditor2 = new List<sis_usuario>();
                            ListAuditor2 = user.BuscaAuditorUsu(user.usuario, ref MsgRes);
                            FormsAuthentication.SetAuthCookie(user.usuario, user.RememberMe);

                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.usuario, DateTime.Now, DateTime.Now.AddMinutes(10), true, user.usuario);
                            String Encrypt = FormsAuthentication.Encrypt(ticket);
                            HttpCookie cookie = new HttpCookie("Usuario", Encrypt);

                            Response.Cookies.Add(cookie);

                            foreach (sis_usuario item in ListAuditor)
                            {
                                FormsAuthentication.RedirectFromLoginPage(item.nombre, false);
                                SesionVar.IDUser = item.id_usuario;
                                SesionVar.UserName = item.usuario;
                                SesionVar.NombreUsuario = item.nombre;
                                SesionVar.ROL = item.id_rol.ToString();
                                SesionVar.Estado = item.id_estado.ToString();
                                SesionVar.ROL_CARGO = item.id_rol_cargo.ToString();

                                sis_usuario Obj = new sis_usuario();
                                Obj.usuario = item.usuario;
                                Obj.estado_usuario = "A";

                                // user.ActualizaEstadoUsuario(Obj, ref MsgRes);
                                Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                                this.Session["firmaDigital"] = false;
                                var BaseImagen = Model.GetFirmas(item.id_usuario);
                                if (BaseImagen != null)
                                {
                                    this.Session["firmaDigital"] = true;
                                }

                                /*Insertar log inicio de sesion */

                                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                                Log_InicioSession logInicioSesion = new Log_InicioSession();
                                logInicioSesion.usuario = item.usuario;
                                logInicioSesion.fecha_ingreso = DateTime.Now;
                                logInicioSesion.host = GetComputerName(Request.UserHostAddress);//User.Identity.Name;
                                logInicioSesion.direccion_ip = Request.UserHostAddress; //GetIPAddress();
                                BusClass.InsertarLogInicioSesion(logInicioSesion, ref MsgRes);

                                break;
                            }

                            return RedirectToAction("Inicio", "Usuario");
                        }
                        else
                        {
                            return RedirectToAction("CambiarClave3", "Usuario");
                        }
                    }
                }

                else
                {
                    ModelState.AddModelError("", "!El usuario o contraseña son invalidos!");
                }
            }
            return View(user);

        }

        public ActionResult ValidacionCodigo(Models.InicioSesion.Login user)
        {
            sis_usuario usuario = new sis_usuario();
            ViewBag.logo = BusClass.GetParametro("Logo");
            ViewBag.sami = BusClass.GetParametro("SAMIFIS");
            ViewBag.hsflogo = BusClass.GetParametro("HSFLOGO");
            ViewBag.logoAsalud = BusClass.GetParametro("LOGO ASALUD SELLO");
            ViewBag.logoSami = BusClass.GetParametro("LOGOSAMI");
            
            var usu = Convert.ToString(Session["usuario"]);
            ViewBag.usuario = usu;
            var codigoAlterno = "";

            try
            {
                if (usu != null)
                {
                    if (!string.IsNullOrEmpty(usu))
                    {
                        usuario = BusClass.datosUsuarioUser(usu);

                        Random generator = new Random();
                        int r = generator.Next(1, 1000000);
                        codigoAlterno = r.ToString().PadLeft(6, '0');

                        BusClass.ActualizaCodigoIngreso(usuario.usuario, codigoAlterno, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            EnviarCodigoCorreo(usuario.correo_ins, codigoAlterno);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                EnviarCodigoCorreo2(usuario.correo_ins, codigoAlterno);
            }

            ViewBag.mensaje = "";

            return View(user);
        }

        [HttpPost]
        public ActionResult ValidacionCodigo(Models.InicioSesion.Login user, string codigo)
        {
            var codUusario = "";
            ViewBag.logo = BusClass.GetParametro("Logo");
            ViewBag.sami = BusClass.GetParametro("SAMIFIS");
            ViewBag.hsflogo = BusClass.GetParametro("HSFLOGO");
            ViewBag.logoAsalud = BusClass.GetParametro("LOGO ASALUD SELLO");
            ViewBag.logoSami = BusClass.GetParametro("LOGOSAMI");
            
            ViewBag.usuario = user.usuario;
            var mensaje = "";

            try
            {
                var validacionCodigo = BusClass.datosUsuarioUser(user.usuario);

                if (validacionCodigo != null)
                {
                    codUusario = validacionCodigo.codigo;

                    if (codUusario == codigo)
                    {
                        sis_usuario usuarioLogin = validacionCodigo;

                        this.Session["idUsuario"] = usuarioLogin.id_usuario;
                        this.Session["usuario"] = usuarioLogin.usuario;
                        this.Session["rol"] = usuarioLogin.id_rol;
                        this.Session["nombre"] = usuarioLogin.nombre;
                        this.Session["claveActual"] = usuarioLogin.contraseña;
                        //this.Session["correoCorp"] = usuarioLogin.correo_ins;

                        /*Si la contraseña es temporal, sera redirigido a actualizar la contraseña*/

                        if (user.contraseña == "temporal" || usuarioLogin.actualiza_contraseña == null)
                        {
                            return RedirectToAction("CambiarClave3", "Usuario");
                        }

                        int totalDias = 0;
                        if (usuarioLogin.actualiza_contraseña != null)
                        {
                            TimeSpan difFechas = DateTime.Now - usuarioLogin.actualiza_contraseña.Value;
                            totalDias = difFechas.Days;
                        }

                        if (totalDias <= 30)
                        {
                            List<sis_usuario> ListAuditor2 = new List<sis_usuario>();
                            ListAuditor2 = user.BuscaAuditorUsu(user.usuario, ref MsgRes);
                            FormsAuthentication.SetAuthCookie(user.usuario, user.RememberMe);

                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.usuario, DateTime.Now, DateTime.Now.AddMinutes(120), true, user.usuario);
                            String Encrypt = FormsAuthentication.Encrypt(ticket);
                            HttpCookie cookie = new HttpCookie("Usuario", Encrypt);

                            Response.Cookies.Add(cookie);

                            FormsAuthentication.RedirectFromLoginPage(validacionCodigo.nombre, false);
                            SesionVar.IDUser = validacionCodigo.id_usuario;
                            SesionVar.UserName = validacionCodigo.usuario;
                            SesionVar.NombreUsuario = validacionCodigo.nombre;
                            SesionVar.ROL = validacionCodigo.id_rol.ToString();
                            SesionVar.Estado = validacionCodigo.id_estado.ToString();
                            SesionVar.ROL_CARGO = validacionCodigo.id_rol_cargo.ToString();

                            sis_usuario Obj = new sis_usuario();
                            Obj.usuario = validacionCodigo.usuario;
                            Obj.estado_usuario = "A";

                            // user.ActualizaEstadoUsuario(Obj, ref MsgRes);
                            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                            this.Session["firmaDigital"] = false;
                            var BaseImagen = Model.GetFirmas(validacionCodigo.id_usuario);
                            if (BaseImagen != null)
                            {
                                this.Session["firmaDigital"] = true;
                            }

                            /*Insertar log inicio de sesion */

                            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                            Log_InicioSession logInicioSesion = new Log_InicioSession();
                            logInicioSesion.usuario = validacionCodigo.usuario;
                            logInicioSesion.fecha_ingreso = DateTime.Now;
                            logInicioSesion.host = GetComputerName(Request.UserHostAddress);//User.Identity.Name;
                            logInicioSesion.direccion_ip = Request.UserHostAddress; //GetIPAddress();
                            BusClass.InsertarLogInicioSesion(logInicioSesion, ref MsgRes);

                            return RedirectToAction("Inicio", "Usuario");
                        }
                        else
                        {
                            return RedirectToAction("CambiarClave3", "Usuario");
                        }
                    }

                    else
                    {
                        ViewBag.mensaje = "El digito de verificación ingresado no es correcto. Por favor vuelva a intentarlo";
                    }
                }
                else
                {
                    ViewBag.mensaje = "El digito de verificación ingresado no es correcto. Por favor vuelva a intentarlo";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                ViewBag.mensaje = "El digito de verificación ingresado no es correcto. Por favor vuelva a intentarlo";
            }

            return View();
        }

        [HttpPost]
        public ActionResult CambiarClave(Models.InicioSesion.CambioClave user)
        {
            if (ModelState.IsValid)
            {
                if (user.contraseñaNueva != user.contraseñaActual)
                {
                    if (user.contraseñaNueva == user.ConfirmaContraseña)
                    {
                        Models.InicioSesion.Login ObjLogin = new Models.InicioSesion.Login();
                        List<sis_usuario> ListAuditor = new List<sis_usuario>();
                        ListAuditor = ObjLogin.ValidaIngreso(this.Session["usuario"].ToString(), user.contraseñaActual);
                        if (ListAuditor.Count != 0)
                        {
                            user.ActualizaContraseña(this.Session["usuario"].ToString(), user.contraseñaNueva, ref MsgRes);
                            ModelState.AddModelError("", "Cambio De Contraseña Exitoso");
                            return RedirectToAction("Inicio", "Usuario");
                        }
                        else
                        {
                            ModelState.AddModelError("", "ERROR: *--- Contraseña Actual No Coincide ---*");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR: *---La nueva contraseña No Coincide-- - *");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ERROR: *---La nueva contraseña no debe coincidir con la actual-- - *");
                }
            }
            return View(user);
        }

        public JsonResult SaveCambiarClave2(Models.InicioSesion.CambioClave Model)
        {
            String mensaje = "";
            String Alerta = "";
            try
            {
                Model.ActualizaContraseñaOlvido(Model.id_usuario, Model.contraseñaNueva, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }

        }

        public class SessionExpireFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                SessionState ctx = new SessionState();


                if (ctx.UserName == "")
                {

                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                    { "Controller", "Usuario" },
                    { "Action", "LogoutSession" }
                    });
                }

                base.OnActionExecuting(filterContext);
            }
        }


        [HttpPost]
        public ActionResult ListaUsuarios(String id, Models.InicioSesion.Usuarios Model)
        {
            String mensaje = "";
            List<sis_usuario> List = new List<sis_usuario>();
            List = Model.BuscaAuditorDoc(id, ref MsgRes);

            if (List.Count() > 0)
            {
                var usuarioSami = List.FirstOrDefault().usuario;
                mensaje = "OK";
                return Json(new { success = true, message = mensaje, usuario = usuarioSami }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveUsuarios(Models.InicioSesion.Usuarios Model)
        {
            String mensaje = "";
            try
            {

                sis_usuario obj = new sis_usuario();

                List<sis_usuario> list = new List<sis_usuario>();

                list = Model.BuscaAuditorUsuSami(Model.usuario, ref MsgRes);

                if (list.Count == 0)
                {
                    obj.usuario = Model.usuario;
                }
                else
                {
                    obj.usuario = Model.usuario + "s";
                }

                obj.contraseña = "temporal";
                obj.nombre = Model.nombre.ToUpper();
                obj.numero_documento = Model.numero_documento;
                obj.profesion = Model.profesion;
                obj.tel = Model.tel;
                obj.correo = Model.correo;
                obj.correo_ins = Model.correo_ins;
                obj.id_rol = Model.id_rol;
                obj.id_estado = 2;
                obj.id_rol_cargo = Model.id_rol_cargo;

                obj.id_usuario = Model.CrearUsuairo(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE EL USUARIO";
                    return Json(new { success = true, message = mensaje, ID = obj.numero_documento }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE USUARIO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(false);
            }

        }

        public JsonResult SaveFactura(Models.InicioSesion.Usuarios Model)
        {
            String mensaje = "";
            try
            {
                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false);
            }

        }

        [SessionExpireFilter]
        public void CrearPdfUsuarios(String id)
        {

            //RUTA REPORTE
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "usuariosSami.rdlc");

            //CONEXION BD  PARA CARGAR DATASET
            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<sis_usuario> lst = new List<sis_usuario>();
            lst = Model.ConsultaDocUusuario(id, ref MsgRes);

            string filename = "CartaUsuarios" + lst.FirstOrDefault().numero_documento;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetUsuario", lst);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }

        public void CrearPdfUsuarios2(String id)
        {
            //RUTA REPORTE
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "usuariosSami2.rdlc");

            //CONEXION BD  PARA CARGAR DATASET
            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<vw_pruebas_img_usuarios> lst = new List<vw_pruebas_img_usuarios>();
            lst = Model.BuscaAuditorImg("1049602350", ref MsgRes);


            //string msg = "0000000001000010000000001";

            //byte[] file = RsaFileDemo.Encriptar(msg);

            //Image myimg = Code128Rendering.MakeBarcodeImage(msg, int.Parse("1"), true);
            //ImageConverter _imageConverter = new ImageConverter();
            //byte[] ImgByte = (byte[])_imageConverter.ConvertTo(myimg, typeof(byte[]));

            //Models.InicioSesion.Usuarios Model2 = new Models.InicioSesion.Usuarios();

            //ecop_firma_digital ObjFirma = new ecop_firma_digital();

            //ObjFirma.llave_firma = file;
            //ObjFirma.cod_barras_firma = ImgByte;
            //ObjFirma.fecha_digita = Convert.ToDateTime(DateTime.Now);
            //ObjFirma.usuario_digita = Convert.ToString(SesionVar.UserName);
            //ObjFirma.imagen = ImgByte;




            //var idfirma = Model2.InsertarFirmadigital(ObjFirma,ref MsgRes);

            string filename = "CartaUsuarios" + lst.FirstOrDefault().numero_documento;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetUsuario", lst);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }

        public void alertas()
        {
            Models.InicioSesion.Login user = new Models.InicioSesion.Login();
            Int32 alerta = 0;
            List<ManagementPrestadoresAlertasFechaResult> listAlertas = new List<ManagementPrestadoresAlertasFechaResult>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            listAlertas = user.ManagmentAlertas(ref MsgRes);
            string mensaje = "";
            string alertaNom = "";
            foreach (var item in listAlertas)
            {
                if (item.ALERTA == "SI")
                {
                    alerta = alerta + 1;
                    mensaje = item.descripcion;
                    alertaNom = item.ALERTA;
                }
            }

            if (alerta > 0)
            {
                this.Session["alerta"] = mensaje;
                this.Session["alertaNom"] = alertaNom;
            }
            else
            {
                this.Session["alerta"] = "";
                this.Session["alertaNom"] = alertaNom;
            }
        }

        private void EnviarCaso(string correo, string codigo)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string filename = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("Usted ha solicitado  el cambio de contraseña por olvido");
            sb.Append("<br/>");
            sb.Append("Su código es: " + codigo);
            sb.Append("<br/>");
            sb.Append("Por favor Ingrese el código y cambie la contraseña.");

            string textBody = sb.ToString();
            try
            {

                string mailBody = "";
                string mailCSS = "";
                mailCSS = "<style>";
                mailCSS += @"body { margin: 0; padding: 0; }";
                mailCSS += @".PageContainer{ background-repeat: no-repeat; width: 600px; height: 900px; }";
                mailCSS += @"#ContentContainer { clear: both; width: 600px; height: 187px; }";
                mailCSS += @"#LeftPane { width: 260px; padding-top: 150px; float: left; padding-left: 40px; color='#F1C40F';}";
                mailCSS += @"#RightPane { width: 230px; float: right; padding-top: 150px; text-align: center; padding-right: 30px; }";
                mailCSS += @"#RightPane2 {width: 260px; float: right; padding-top: 150px; text-align: center; }";
                mailCSS += @".tableClass { border: dashed 1px #000000; padding: 5px 5px 5px 5px; }";
                mailCSS += @"#FooterContainer { clear: both; width: 590px; padding-top: 515px; height: 80px; font-size: 0.6em; padding-left: 10px; font-weight: bold; }";
                mailCSS += @".a { color: #063971; }";

                mailCSS += "</style>";
                mailBody = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                mailBody += "<div class='PageContainer' style=' font-family: Century Gothic, Century Gothic, sans-serif; '>";
                mailBody += "<div id='ContentContainer' style=' color: #063971;'>";

                mailBody += "<div id='LeftPane' style='font-size: 14.5px;'>";
                mailBody += "<br />";
                mailBody += textBody;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
                mailBody += "<br />";
                mailBody += "Dir. Calle 96 # 13a-03 Piso 4";
                mailBody += "<br />";
                //mailBody += "Tel. 3203764283";
                //mailBody += "<br />";
                mailBody += "Bogotá";
                mailBody += "</div>";

                mailBody += "<br />";
                mailBody += "<div id='RightPane2' align='center'  style='font-size: 10.5px;'>";
                mailBody += "“El contenido de este mensaje electrónico, su texto o elementos adjuntos, son de uso confidencial y privado entre el remitente y su (s) destinatario (s). Cualquier uso o distribución indebida o sin autorización escrita por parte del remitente, se encuentran estrictamente prohibidas y acarrean sanciones penales. Por tanto, en caso de recibir este mensaje por error, por favor notificarlo y devolverlo de inmediato al remitente del mismo, sin guardar copia alguna...";
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "Por un medio ambiente sano, imprima solo lo necesario.";
                mailBody += "</div>";

                mailBody += "</div>";
                mailBody += "</div>";

                System.Net.Mail.SmtpClient objMail = new System.Net.Mail.SmtpClient();
                objMail.Host = smtpSection.Network.Host;
                objMail.Port = smtpSection.Network.Port;
                objMail.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                objMail.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody.ToString(), new System.Net.Mime.ContentType("text/html"));
                LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                resource.ContentId = "dealer_logo";

                htmlView.LinkedResources.Add(resource);

                mailMessage.AlternateViews.Add(htmlView);

                mailMessage.To.Clear();

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    mailMessage.From = new MailAddress("admin@asaludltda.com");
                    mailMessage.To.Add(correo);
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    mailMessage.From = new MailAddress("admin@asaludltda.com");
                    mailMessage.To.Add(correo);
                }

                mailMessage.Subject = "[Mensaje Automático]" + " " + "Cambio de contraseña";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.AddModelError("", "ERROR!" + ViewBag.Message);
            }
        }

        public void EnviarCodigoCorreo(string correo, string codigo)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            string filename = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("Buen día, cordial saludo.");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("Su código de validación para el inicio de sesión es: " + codigo);
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("Este código es personal e intransferible. No lo comparta con nadie para mantener segura su información.");

            string textBody = sb.ToString();
            try
            {

                string mailBody = "";
                string mailCSS = "";
                mailCSS = "<style>";
                mailCSS += @"body { margin: 0; padding: 0; }";
                mailCSS += @".PageContainer{ background-repeat: no-repeat; width: 600px; height: 900px; }";
                mailCSS += @"#ContentContainer { clear: both; width: 600px; height: 187px; }";
                mailCSS += @"#LeftPane { width: 260px; padding-top: 150px; float: left; padding-left: 40px; color='#F1C40F';}";
                mailCSS += @"#RightPane { width: 230px; float: right; padding-top: 150px; text-align: center; padding-right: 30px; }";
                mailCSS += @"#RightPane2 {width: 260px; float: right; padding-top: 150px; text-align: center; }";
                mailCSS += @".tableClass { border: dashed 1px #000000; padding: 5px 5px 5px 5px; }";
                mailCSS += @"#FooterContainer { clear: both; width: 590px; padding-top: 515px; height: 80px; font-size: 0.6em; padding-left: 10px; font-weight: bold; }";
                mailCSS += @".a { color: #063971; }";

                mailCSS += "</style>";
                mailBody = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                mailBody += "<div class='PageContainer' style=' font-family: Century Gothic, Century Gothic, sans-serif; '>";
                mailBody += "<div id='ContentContainer' style=' color: #063971;'>";

                mailBody += "<div id='LeftPane' style='font-size: 14.5px;'>";
                mailBody += "<br />";
                mailBody += textBody;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
                mailBody += "<br />";
                mailBody += "Dir. Calle 96 # 13a-03 Piso 4";
                mailBody += "<br />";
                //mailBody += "Tel. 3203764283";
                //mailBody += "<br />";
                mailBody += "Bogotá";
                mailBody += "</div>";

                mailBody += "<br />";
                mailBody += "<div id='RightPane2' align='center'  style='font-size: 10.5px;'>";
                mailBody += "“El contenido de este mensaje electrónico, su texto o elementos adjuntos, son de uso confidencial y privado entre el remitente y su (s) destinatario (s). Cualquier uso o distribución indebida o sin autorización escrita por parte del remitente, se encuentran estrictamente prohibidas y acarrean sanciones penales. Por tanto, en caso de recibir este mensaje por error, por favor notificarlo y devolverlo de inmediato al remitente del mismo, sin guardar copia alguna...";
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "Por un medio ambiente sano, imprima solo lo necesario.";
                mailBody += "</div>";

                mailBody += "</div>";
                mailBody += "</div>";

                System.Net.Mail.SmtpClient objMail = new System.Net.Mail.SmtpClient();
                objMail.Host = smtpSection.Network.Host;
                objMail.Port = smtpSection.Network.Port;
                objMail.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                objMail.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody.ToString(), new System.Net.Mime.ContentType("text/html"));
                LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                resource.ContentId = "dealer_logo";
                htmlView.LinkedResources.Add(resource);
                mailMessage.AlternateViews.Add(htmlView);
                mailMessage.To.Clear();

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    mailMessage.From = new MailAddress("admin@asaludltda.com");
                    mailMessage.To.Add(correo);
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    mailMessage.From = new MailAddress("admin@asaludltda.com");
                    mailMessage.To.Add(correo);
                }

                mailMessage.Subject = "[Mensaje Automático]" + " " + "Código de verificación - Inicio de sesión";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);
            }

            catch (Exception ex)
            {
                var mensaje = "";
                var mensajeFinal = "";

                if (ex.Message.Contains("addresses"))
                {
                    mensaje = "El correo corporativo es erróneo. Por favor informar al correo sami.soporte@asalud.co";
                }
                else if (ex.Message.Contains("La cadena especificada no tiene la forma obligatoria para una dirección"))
                {
                    mensaje = "El correo corporativo es erróneo. Por favor informar al correo sami.soporte@asalud.co";
                }
                else
                {
                    mensaje = ex.Message;
                }

                EnviarCodigoCorreo2(correo, codigo);
                mensajeFinal = "Estamos enfrentando problemas: " + mensaje;
                ModelState.AddModelError("", "" + mensajeFinal);
            }
        }


        public void EnviarCodigoCorreo2(string correo, string codigo)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string filename = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("Buen día, cordial saludo.");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("Su código de validación para el inicio de sesión es: " + codigo);
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("Este código es personal e intransferible. No lo comparta con nadie para mantener segura su información.");

            string textBody = sb.ToString();
            try
            {
                string mailBody = "";
                string mailCSS = "";
                mailCSS = "<style>";
                mailCSS += @"body { margin: 0; padding: 0; }";
                mailCSS += @".PageContainer{ background-repeat: no-repeat; width: 600px; height: 900px; }";
                mailCSS += @"#ContentContainer { clear: both; width: 600px; height: 187px; }";
                mailCSS += @"#LeftPane { width: 260px; padding-top: 150px; float: left; padding-left: 40px; color='#F1C40F';}";
                mailCSS += @"#RightPane { width: 230px; float: right; padding-top: 150px; text-align: center; padding-right: 30px; }";
                mailCSS += @"#RightPane2 {width: 260px; float: right; padding-top: 150px; text-align: center; }";
                mailCSS += @".tableClass { border: dashed 1px #000000; padding: 5px 5px 5px 5px; }";
                mailCSS += @"#FooterContainer { clear: both; width: 590px; padding-top: 515px; height: 80px; font-size: 0.6em; padding-left: 10px; font-weight: bold; }";
                mailCSS += @".a { color: #063971; }";

                mailCSS += "</style>";
                mailBody = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                mailBody += "<div class='PageContainer' style=' font-family: Century Gothic, Century Gothic, sans-serif; '>";
                mailBody += "<div id='ContentContainer' style=' color: #063971;'>";

                mailBody += "<div id='LeftPane' style='font-size: 14.5px;'>";
                mailBody += "<br />";
                mailBody += textBody;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
                mailBody += "<br />";
                mailBody += "Dir. Calle 96 # 13a-03 Piso 4";
                mailBody += "<br />";
                //mailBody += "Tel. 3203764283";
                //mailBody += "<br />";
                mailBody += "Bogotá";
                mailBody += "</div>";

                mailBody += "<br />";
                mailBody += "<div id='RightPane2' align='center'  style='font-size: 10.5px;'>";
                mailBody += "“El contenido de este mensaje electrónico, su texto o elementos adjuntos, son de uso confidencial y privado entre el remitente y su (s) destinatario (s). Cualquier uso o distribución indebida o sin autorización escrita por parte del remitente, se encuentran estrictamente prohibidas y acarrean sanciones penales. Por tanto, en caso de recibir este mensaje por error, por favor notificarlo y devolverlo de inmediato al remitente del mismo, sin guardar copia alguna...";
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "Por un medio ambiente sano, imprima solo lo necesario.";
                mailBody += "</div>";

                mailBody += "</div>";
                mailBody += "</div>";

                System.Net.Mail.SmtpClient objMail = new System.Net.Mail.SmtpClient();
                objMail.Host = smtpSection.Network.Host;
                objMail.Port = smtpSection.Network.Port;
                objMail.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                objMail.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody.ToString(), new System.Net.Mime.ContentType("text/html"));
                LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                resource.ContentId = "dealer_logo";
                htmlView.LinkedResources.Add(resource);
                mailMessage.AlternateViews.Add(htmlView);
                mailMessage.To.Clear();

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    mailMessage.From = new MailAddress("notificaciones@asaludltda.com");
                    mailMessage.To.Add(correo);
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    mailMessage.From = new MailAddress("notificaciones@asaludltda.com");
                    mailMessage.To.Add(correo);
                }

                mailMessage.Subject = "[Mensaje Automático]" + " " + "Código de verificación - Inicio de sesión";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);
            }

            catch (Exception ex)
            {
                var mensaje = "";
                var mensajeFinal = "";

                if (ex.Message.Contains("addresses"))
                {
                    mensaje = "El correo corporativo es erróneo. Por favor informar al correo sami.soporte@asalud.co";
                }
                else if (ex.Message.Contains("La cadena especificada no tiene la forma obligatoria para una dirección"))
                {
                    mensaje = "El correo corporativo es erróneo. Por favor informar al correo sami.soporte@asalud.co";
                }
                else
                {
                    mensaje = ex.Message;
                }

                mensajeFinal = "Estamos enfrentando problemas: " + mensaje;
                ModelState.AddModelError("", "" + mensajeFinal);
            }
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 13 de enero de 2023
        /// Metodo para obtener el listado de entregables pendientes
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerEntregablesPendientes()
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            DateTime fechahoy = DateTime.Now;
            var events = Model.SetearEntregablesEnEvent(SesionVar.UserName, SesionVar.ROL).ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 24 de enero de 2022
        /// Metodo utilizado para poder insertar los daros en actividad reciente
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GuardarActividadReciente(string titulo, string descripcion)
        {
            try
            {
                sis_actividad_reciente obj = new sis_actividad_reciente();
                obj.titulo = titulo;
                obj.descripcion = descripcion;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                BusClass.InsertarActividadReciente(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return Json(new { success = true, message = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ha ocurrido un error al momento procesar la solicitud: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 11 de abril de 2023
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetPublicIPAddress()
        {
            try
            {
                String address = "";
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    address = stream.ReadToEnd();
                }

                int first = address.IndexOf("Address: ") + 9;
                int last = address.LastIndexOf("</body>");
                address = address.Substring(first, last - first);

                return address;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetComputerName(string clientIP)
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(clientIP);
                return hostEntry.HostName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        #endregion
    }
}