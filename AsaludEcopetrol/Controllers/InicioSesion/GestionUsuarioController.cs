using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.Medicamentos;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.InicioSesion
{
    [SessionExpireFilter]

    public class GestionUsuarioController : Controller
    {
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

        private Facede.Facade _BusClass;
        public Facede.Facade BusClass
        {
            get
            {
                if (_BusClass != null)
                {
                    return _BusClass;
                }
                else
                {
                    return _BusClass = new Facede.Facade();
                }

            }
            set { _BusClass = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();


        public ActionResult Buscar(string variable)
        {

            Models.General General = new Models.General();

            if (variable == "1")
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se activo el usuario correctamente");
            }
            else if (variable == "2")
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se inactivo el usuario correctamente");
            }
            else if (variable == "3")
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se actualizo la contraseña del usuario  correctamente la contraseña prefedinida es 'temporal' ");

            }
            else
            {
                ViewData["alerta"] = "";
            }

            List<sis_usuario> list = BusClass.GetUsuarios().ToList();

            ViewBag.usuarios = list;

            return View();
        }


        [HttpGet]
        public PartialViewResult _Documento(String ID)
        {
            var opcion = 0;

            List<sis_usuario> list = BusClass.GetUsuarios().ToList();

            list = list.Where(l => l.numero_documento == ID).ToList();

            if (list.Count != 0)
            {
                opcion = 1;
            }
            else
            {
                opcion = 2;
            }


            ViewBag.lista = list;
            ViewBag.variante = opcion;

            return PartialView();
        }


        [HttpGet]
        public PartialViewResult _Usuario(String ID)
        {
            var opcion = 0;

            List<sis_usuario> list = BusClass.GetUsuarios().ToList();

            list = list.Where(l => l.usuario == ID).ToList();

            if (list.Count != 0)
            {
                opcion = 1;
            }
            else
            {
                opcion = 2;
            }


            ViewBag.lista = list;
            ViewBag.variante = opcion;



            return PartialView();
        }


        [HttpGet]
        public PartialViewResult _Nombres(String ID)
        {
            var opcion = 0;

            List<sis_usuario> list = BusClass.GetUsuarios().ToList();

            list = list.Where(l => l.nombre == ID).ToList();

            if (list.Count != 0)
            {
                opcion = 1;
            }
            else
            {
                opcion = 2;
            }


            ViewBag.lista = list;
            ViewBag.variante = opcion;



            return PartialView();
        }

        public JsonResult ActivarUsu(String opcionrealizar, Int32? idusuario)
        {
            List<sis_usuario> list = BusClass.GetUsuarios().ToList();
            list = list.Where(l => l.id_usuario == idusuario).ToList();

            foreach (var item in list)
            {
                if (item.id_rol == 1)
                {


                    opcionrealizar = "2";
                }
                else
                {
                    opcionrealizar = "1";
                }
            }

            Models.General General = new Models.General();

            if (opcionrealizar == "1")
            {
                sis_usuario usuario = new sis_usuario();

                usuario.id_usuario = Convert.ToInt32(idusuario);
                usuario.id_estado = 1;
                BusClass.ActualizaEstadoUsuario(usuario, ref MsgRes);




                foreach (var item in list)
                {
                    log_gestion_usuarios log = new log_gestion_usuarios();

                    log.tipo_gestion = "ACTIVAR";
                    log.usuario_modificado = item.usuario;
                    log.fecha_gestion = DateTime.Now;
                    log.usuario_gestion = SesionVar.UserName;

                    BusClass.InsertarLogGestionUusario(log, ref MsgRes);
                }
            }


            return Json(new { idusuario, opcionrealizar }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult InactivarUsu(String opcionrealizar, Int32? idusuario)
        {

            Models.General General = new Models.General();

            List<sis_usuario> list = BusClass.GetUsuarios().ToList();
            list = list.Where(l => l.id_usuario == idusuario).ToList();


            foreach (var item in list)
            {
                if (item.id_rol == 1)
                {
                    opcionrealizar = "2";
                }
                else
                {
                    opcionrealizar = "1";
                }
            }


            if (opcionrealizar == "1")
            {
                sis_usuario usuario = new sis_usuario();
                usuario.id_usuario = Convert.ToInt32(idusuario);
                usuario.id_estado = 2;
                BusClass.ActualizaEstadoUsuario(usuario, ref MsgRes);

                foreach (var item in list)
                {
                    log_gestion_usuarios log = new log_gestion_usuarios();

                    log.tipo_gestion = "INACTIVAR";
                    log.usuario_modificado = item.usuario;
                    log.fecha_gestion = DateTime.Now;
                    log.usuario_gestion = SesionVar.UserName;

                    BusClass.InsertarLogGestionUusario(log, ref MsgRes);
                }
            }

            return Json(new { idusuario, opcionrealizar }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult RestabelecerUsu(String opcionrealizar, Int32? idusuario)
        {

            List<sis_usuario> list = BusClass.GetUsuarios().ToList();
            list = list.Where(l => l.id_usuario == idusuario).ToList();

            foreach (var item in list)
            {
                if (item.id_rol == 1)
                {
                    opcionrealizar = "2";
                }
                else
                {
                    opcionrealizar = "1";
                }
            }

            Models.General General = new Models.General();


            if (opcionrealizar == "1")
            {
                sis_usuario usuario = new sis_usuario();
                usuario.id_usuario = Convert.ToInt32(idusuario);
                usuario.contraseña = "temporal";
                BusClass.ActualizaClaveUsuario(usuario, ref MsgRes);

                foreach (var item in list)
                {
                    log_gestion_usuarios log = new log_gestion_usuarios();

                    log.tipo_gestion = "RESTABLECER CONTRASEÑA";
                    log.usuario_modificado = item.usuario;
                    log.fecha_gestion = DateTime.Now;
                    log.usuario_gestion = SesionVar.UserName;

                    BusClass.InsertarLogGestionUusario(log, ref MsgRes);
                }

            }

            return Json(new { idusuario, opcionrealizar }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GestionUsuarioPrestadores()
        {
            return View();
        }


        public string buscarDatosUsuarioPrestador(int decision, string nit, string nombre, string cedula)
        {
            var retorno = "";
            try
            {
                List<management_informacionUsuarios_prestadoresResult> listado = new List<management_informacionUsuarios_prestadoresResult>();
                listado = BusClass.UsuariosPrestadores(nit, nombre, cedula);

                if (decision == 1)
                {
                    if (nit == "" || nit == null)
                    {
                        listado = listado.OrderByDescending(x => x.ultimo_ingreso).Take(20).ToList();
                    }
                }

                if (decision == 3)
                {
                    listado = listado.OrderByDescending(x => x.ultimo_ingreso).Take(1).ToList();
                }

                var conteo = listado.Count();

                int i = 0;

                if (conteo > 0)
                {
                    foreach (var item in listado)
                    {
                        i += 1;

                        retorno += "<tr>";
                        retorno += "<td>" + item.numero_documento + "</td>";
                        retorno += "<td>" + item.nombre + "</td>";

                        if (item.id_estado == 1)
                        {
                            retorno += "<td>" + "ACTIVO" + "</td>";
                        }
                        else
                        {
                            retorno += "<td>" + "INACTIVO" + "</td>";
                        }
                        retorno += "<td>" + item.ultimo_ingreso + "</td>";
                        retorno += "<td>  <button class='button_Asalud_Aceptar' style='text-align:center;' onclick='MirarPrestadores(" + item.id_usuario + "); ' id='btn-confirmar'>Ver</button></td>";
                        retorno += "</tr>";
                    }
                }
                else
                {
                    retorno += "<tr>";
                    retorno += "<td colspan='12' style='text-align:center; font-size: 14px;'>";
                    retorno += "<label>No hay usuario con esta información.</label>";
                    retorno += "</td>";
                    retorno += "</tr>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return retorno;
        }

        public PartialViewResult _ModalPrestadoresUsuario(int idUsuario)
        {
            var nit = "";
            var nombre = "";
            var cedula = "";
            List<management_informacionUsuarios_prestadoresDetalleResult> listado = new List<management_informacionUsuarios_prestadoresDetalleResult>();
            var conteo = 0;

            try
            {
                listado = BusClass.UsuariosPrestadoresDetalle(idUsuario).OrderByDescending(x => x.Nit).ToList();
                conteo = listado.Count();

                ViewBag.conteo = conteo;
                ViewBag.lista = listado;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView();


        }

    }
}