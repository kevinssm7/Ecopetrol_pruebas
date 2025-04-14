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


namespace AsaludEcopetrol.Controllers.Soporte
{
    [SessionExpireFilter]

    public class CambiosController : Controller
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



        // GET: Cambios
        public ActionResult GestionAuditor(Int32? variable)
        {

            Models.General General = new Models.General();

            if (variable == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "¡Transaccion exitosa!!", "Se cambió el auditor del paciente correctamente.");
            }
            else if (variable == 2)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "¡Transaccion Fallida!!", "Error en el procedimiento .");
            }
            else if (variable == 3)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "¡Transaccion exitosa!!", "Se eliminó la fecha de egreso censo  correctamente.");
            }
            else if (variable == 4)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "¡Transaccion exitosa!!", "Se modificó los datos del paciente correctamente.");

            }
            else
            {
                ViewData["alerta"] = "";
            }

            List<sis_usuario> list = new List<sis_usuario>();
            sis_usuario propio = new sis_usuario();
            var idUsuario = SesionVar.IDUser;
            var rol = 0;

            try
            {
                propio = BusClass.datosUsuarioId(idUsuario);

                if (propio != null)
                {
                    rol = (int)propio.id_rol;
                }

                list = BusClass.GetUsuarios().ToList();
                list = list.Where(l => l.id_rol == 7).ToList();
                list = list.Where(l => l.id_estado == 1).ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = rol;
            ViewBag.auditores = list;



            return View();
        }

        [HttpPost]
        public ActionResult GestionAuditor(String numerodocumento, String auditor)
        {
            Models.General General = new Models.General();

            if (numerodocumento != null)
            {
                return RedirectToAction("_Documento", "Cambios", new { ID = numerodocumento }); ;
            }
            else
            {
                return RedirectToAction("_Auditor", "Cambios", new { ID = auditor }); ;
            }
        }

        [HttpPost]
        public ActionResult GestionLevanteEgreso(String numerodocumentoegreso)
        {
            Models.General General = new Models.General();

            return RedirectToAction("_DocumentoEgreso", "Cambios", new { ID = numerodocumentoegreso });


        }

        [HttpPost]
        public ActionResult GestionCambioDatos(String numerodocumentopaciente)
        {
            Models.General General = new Models.General();

            return RedirectToAction("_CambiosPaciente", "Cambios", new { ID = numerodocumentopaciente });


        }


        [HttpGet]
        public ActionResult _Documento(String ID)
        {
            var opcion = 0;

            List<vw_tablero_concurrencia> list = BusClass.GetTableroConcurrencia().ToList();

            list = list.Where(l => l.id_afi == ID).ToList();

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



            return View();
        }

        [HttpGet]
        public ActionResult _Auditor(String ID)
        {
            var opcion = 0;

            List<vw_tablero_concurrencia> list = BusClass.GetTableroConcurrencia().ToList();

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



            return View();
        }

        [HttpGet]
        public ActionResult _DocumentoEgreso(String ID)
        {
            var opcion = 0;

            List<vw_tablero_levante_egreso> list = BusClass.GetlevanteEgreso(ID, ref MsgRes).ToList();

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



            return View();
        }

        [HttpGet]
        public ActionResult _CambiosPaciente(String ID)
        {
            var opcion = 0;

            List<vw_tablero_levante_egreso> list = BusClass.GetlevanteEgreso(ID, ref MsgRes).ToList();

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



            return View();
        }



        public PartialViewResult _CambioAuditor(String ID, String IDConcu)
        {

            Models.General General = new Models.General();
            Models.Cambio.cambio Model = new Models.Cambio.cambio();

            List<sis_usuario> list = BusClass.GetUsuarios().ToList();
            list = list.Where(l => l.usuario == ID).ToList();
            foreach (var item in list)
            {
                Model.id_usuario = item.id_usuario;
                Model.usuario = item.usuario;
            }

            List<sis_usuario> list2 = BusClass.GetUsuarios().ToList();
            list2 = list2.Where(l => l.id_rol == 7).ToList();
            list2 = list2.Where(l => l.id_estado == 1).ToList();

            Model.id_concurrencia = Convert.ToInt32(IDConcu);
            ViewBag.auditores2 = list2;
            ViewBag.idadmin = SesionVar.UserName;
            string RolUsuario = SesionVar.ROL;

            return PartialView(Model);
        }

        public PartialViewResult _CambiosDatosPaciente(String ID, String IDConcu)
        {

            Models.General General = new Models.General();
            Models.Cambio.cambio Model = new Models.Cambio.cambio();


            Model.id_censo = Convert.ToInt32(ID);
            Model.id_concurrencia = Convert.ToInt32(IDConcu);

            ViewBag.idadmin = SesionVar.UserName;
            string RolUsuario = SesionVar.ROL;

            return PartialView(Model);
        }


        [HttpPost]
        public JsonResult ModificarAuditor(Int32? auditor, Int32? id_concurrencia, Int32? id_usuario, String usuario, Models.Cambio.cambio Model)
        {
            String mensaje = "";

            List<ecop_concurrencia> list = BusClass.ConsultaConcurrenciaIdLista(Convert.ToInt32(Model.id_concurrencia), ref MsgRes);

            foreach (var item in list)
            {
                ecop_concurrencia OBJ = new ecop_concurrencia();
                OBJ.id_concurrencia = item.id_concurrencia;
                OBJ.id_auditor = Convert.ToInt32(auditor);
                OBJ.id_editor = Convert.ToInt32(auditor);

                BusClass.ActualizarAuditorConcurrencia(OBJ, ref MsgRes);


                ecop_censo OBJ2 = new ecop_censo();

                OBJ2.id_censo = Convert.ToInt32(item.id_censo);
                OBJ2.id_medico_auditor = Convert.ToInt32(auditor);

                BusClass.ActualizarAuditorCenso(OBJ2, ref MsgRes);


                log_cambios_gestion_hospitalaria OBJ3 = new log_cambios_gestion_hospitalaria();

                OBJ3.id_censo = item.id_censo;
                OBJ3.tipo_modificacion = "CAMBIO AUDITOR";
                OBJ3.fecha_modificacion = DateTime.Now;
                OBJ3.usuario_modificacion = SesionVar.UserName;

                List<sis_usuario> list2 = BusClass.GetUsuarios().ToList();
                list2 = list2.Where(l => l.id_usuario == Convert.ToInt32(auditor)).ToList();

                foreach (var order in list2)
                {
                    OBJ3.auditor_nuevo = order.usuario;

                }

                List<sis_usuario> list3 = BusClass.GetUsuarios().ToList();
                list3 = list3.Where(l => l.id_usuario == item.id_auditor).ToList();

                foreach (var order2 in list3)
                {

                    OBJ3.auditor_anterior = order2.usuario;
                }

                BusClass.InsertarLogCambiosGetionHospitalaria(OBJ3, ref MsgRes);


            }

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {


                mensaje = "SE MODIFICO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mensaje = "ERROR AL ACTUALIZAR EL AUDITOR.";
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpPost]
        public JsonResult ModificarDatosPaciente(Int32? id_censo, Int32? id_concurrencia, String tipocambio, String nombreP, String nombreS, String ApellidoP, String ApellidoS,
           String tipodocumento, String numerodocumento, DateTime? fechaingreso, Models.Cambio.cambio Model)
        {
            String mensaje = "";
            String descripcion_anterior = "";
            String descripcion_nueva = "";

            List<ecop_censo> list = BusClass.ConsultaCensoIdLista(Convert.ToInt32(id_censo), ref MsgRes);

            foreach (var item in list)
            {

                descripcion_anterior = item.primer_apellido + " " + item.segundo_apellido + " " + item.primer_nombre + " " + item.segundo_nombre + "/" + item.tipo_identifi_afiliado + item.num_identifi_afil + "/" + item.fecha_ingreso;


                if (tipocambio == "1") //Nombres
                {
                    ecop_censo OBJ = new ecop_censo();

                    OBJ.id_censo = Convert.ToInt32(id_censo);
                    OBJ.primer_apellido = ApellidoP;
                    OBJ.segundo_apellido = ApellidoS;
                    OBJ.primer_nombre = nombreP;
                    OBJ.segundo_nombre = nombreS;

                    BusClass.ActualizarCambiosPacienteCenso(OBJ, tipocambio, ref MsgRes);

                    ecop_concurrencia OBJ2 = new ecop_concurrencia();

                    OBJ2.id_concurrencia = Convert.ToInt32(id_concurrencia);
                    OBJ2.afi_nom = ApellidoP + ' ' + ApellidoS + ' ' + nombreP + ' ' + nombreS;

                    BusClass.ActualizarCambiosPacienteConcu(OBJ2, tipocambio, ref MsgRes);


                    descripcion_nueva = ApellidoP + " " + ApellidoS + " " + nombreP + " " + nombreS + "/" + item.tipo_identifi_afiliado + item.num_identifi_afil + "/" + item.fecha_ingreso;
                }
                else if (tipocambio == "2") //Documento
                {

                    ecop_censo OBJ3 = new ecop_censo();
                    OBJ3.id_censo = Convert.ToInt32(id_censo);
                    OBJ3.tipo_identifi_afiliado = tipodocumento;
                    OBJ3.num_identifi_afil = numerodocumento;

                    BusClass.ActualizarCambiosPacienteCenso(OBJ3, tipocambio, ref MsgRes);

                    ecop_concurrencia OBJ4 = new ecop_concurrencia();

                    OBJ4.id_concurrencia = Convert.ToInt32(id_concurrencia);
                    OBJ4.afi_tipo_doc = tipodocumento;
                    OBJ4.id_afi = numerodocumento;

                    BusClass.ActualizarCambiosPacienteConcu(OBJ4, tipocambio, ref MsgRes);

                    descripcion_nueva = item.primer_apellido + " " + item.segundo_apellido + " " + item.primer_nombre + " " + item.segundo_nombre + "/" + tipodocumento + numerodocumento + "/" + item.fecha_ingreso;
                }
                else if (tipocambio == "3") //FechaIngreso
                {

                    ecop_censo OBJ5 = new ecop_censo();
                    OBJ5.id_censo = Convert.ToInt32(id_censo);
                    OBJ5.fecha_ingreso = fechaingreso;

                    BusClass.ActualizarCambiosPacienteCenso(OBJ5, tipocambio, ref MsgRes);

                    ecop_concurrencia OBJ6 = new ecop_concurrencia();

                    OBJ6.id_concurrencia = Convert.ToInt32(id_concurrencia);
                    OBJ6.fecha_ingreso = fechaingreso;


                    BusClass.ActualizarCambiosPacienteConcu(OBJ6, tipocambio, ref MsgRes);

                    descripcion_nueva = item.primer_apellido + " " + item.segundo_apellido + " " + item.primer_nombre + " " + item.segundo_nombre + "/" + tipodocumento + numerodocumento + "/" + fechaingreso;
                }


                log_cambios_gestion_hospitalaria OBJ7 = new log_cambios_gestion_hospitalaria();

                OBJ7.id_censo = item.id_censo;
                OBJ7.tipo_modificacion = "MODIFICACION DATOS PACIENTE";
                OBJ7.fecha_modificacion = DateTime.Now;
                OBJ7.usuario_modificacion = SesionVar.UserName;
                OBJ7.descripcion_cambios_anterior = descripcion_anterior;
                OBJ7.descripcion_cambios_nuevo = descripcion_nueva;

                BusClass.InsertarLogCambiosGetionHospitalaria(OBJ7, ref MsgRes);

            }


            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {

                mensaje = "SE MODIFICO LOS DATOS CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mensaje = "ERROR AL ACTUALIZAR EL AUDITOR.";
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }

        }


        public JsonResult EliminarEgresoCenso(String opcionrealizar, Int32? idconcurrencia)
        {

            List<ecop_concurrencia> list = BusClass.ConsultaConcurrenciaIdLista(Convert.ToInt32(idconcurrencia), ref MsgRes);

            foreach (var item in list)
            {
                List<ecop_censo> list2 = BusClass.ConsultaCensoIdLista(Convert.ToInt32(item.id_censo), ref MsgRes);

                foreach (var order in list2)
                {
                    log_cambios_gestion_hospitalaria log = new log_cambios_gestion_hospitalaria();

                    log.id_censo = item.id_censo;
                    log.tipo_modificacion = "ELIMINAR FECHA EGRESO CENSO";
                    log.fecha_modificacion = DateTime.Now;
                    log.usuario_modificacion = SesionVar.UserName;
                    log.fecha_egreso = order.fecha_egreso_censo;

                    BusClass.InsertarLogCambiosGetionHospitalaria(log, ref MsgRes);

                    ecop_censo OBJ = new ecop_censo();

                    OBJ.id_censo = Convert.ToInt32(item.id_censo);
                    OBJ.fecha_egreso_censo = null;
                    OBJ.condicion_de_alta = null;
                    OBJ.admitido_auditor = null;

                    BusClass.ActualizarFechaEgresoCenso(OBJ, ref MsgRes);
                }
            }

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                opcionrealizar = "1";

            }
            else
            {
                opcionrealizar = "2";
            }
            return Json(new { idconcurrencia, opcionrealizar }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult EliminarEgreso(String opcionrealizar, Int32? idconcurrencia)
        {
            List<ecop_concurrencia> list = BusClass.ConsultaConcurrenciaIdLista(Convert.ToInt32(idconcurrencia), ref MsgRes);

            foreach (var item in list)
            {
                List<ecop_censo> list2 = BusClass.ConsultaCensoIdLista(Convert.ToInt32(item.id_censo), ref MsgRes);

                foreach (var order in list2)
                {
                    log_cambios_gestion_hospitalaria log = new log_cambios_gestion_hospitalaria();

                    log.id_censo = item.id_censo;
                    log.tipo_modificacion = "ELIMINAR EGRESO";
                    log.fecha_modificacion = DateTime.Now;
                    log.usuario_modificacion = SesionVar.UserName;
                    log.fecha_egreso = order.fecha_egreso;

                    BusClass.InsertarLogCambiosGetionHospitalaria(log, ref MsgRes);

                    BusClass.EliminarEgreso(Convert.ToInt32(idconcurrencia), ref MsgRes);

                    ecop_censo OBJ = new ecop_censo();

                    OBJ.id_censo = Convert.ToInt32(item.id_censo);
                    OBJ.fecha_egreso = null;
                    OBJ.admitido_auditor = null;

                    BusClass.ActualizarLeEgresoCenso(OBJ, ref MsgRes);

                    ecop_concurrencia OBJ2 = new ecop_concurrencia();

                    OBJ2.id_concurrencia = item.id_concurrencia;
                    OBJ2.fecha_egreso = null;
                    BusClass.ActualizarEgresoConcu(OBJ2, ref MsgRes);
                }
            }

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                opcionrealizar = "1";

            }
            else
            {
                opcionrealizar = "2";
            }


            return Json(new { idconcurrencia, opcionrealizar }, JsonRequestBehavior.AllowGet);

        }


        /*Alexis 16-10-20  eliminacion de visitas */

        public ActionResult GestionVisitas()
        {
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
            ViewBag.CalidadPrestadores = Model.getprestadoresList(null, null);
            return View();
        }

        public ActionResult GestionarVisita(Int32? id_visita, Int32? id_prestador, int? rta)
        {
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
            List<vw_visitas> visitas = new List<vw_visitas>();

            if (id_visita != null)
            {
                visitas = Model.GetListVisitas(id_visita, null, null).Where(l => l.Realizo_evaluacion == false).ToList();
            }

            if (id_prestador != null)
            {
                visitas = Model.GetListVisitas(null, id_prestador, null).Where(l => l.Realizo_evaluacion == false).ToList();
            }

            ViewData["rta"] = rta;
            ViewBag.prestador = id_prestador;
            return View(visitas);
        }

        public ActionResult EliminarVisita(Int32? idvisita, Int32? id_prestador)
        {
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();

            log_eliminacion_visitas log = new log_eliminacion_visitas();
            log.id_cronograma_visita = idvisita.Value;
            log.observaciones_log = "Eliminación de visita de calidad";
            log.fecha_digita = DateTime.Now;
            log.usuario_digita = SesionVar.UserName;
            Model.EliminarVisita(idvisita.Value, log, ref MsgRes);

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                if (id_prestador != null)
                {
                    return RedirectToAction("GestionarVisita", "Cambios", new { id_prestador = id_prestador, rta = 1 });
                }
                else
                {
                    return RedirectToAction("GestionarVisita", "Cambios", new { id_visita = idvisita, rta = 1 });
                }

            }
            else
            {
                if (id_prestador != null)
                {
                    return RedirectToAction("GestionarVisita", "Cambios", new { id_prestador = id_prestador, rta = 2 });
                }
                else
                {
                    return RedirectToAction("GestionarVisita", "Cambios", new { id_visita = idvisita, rta = 2 });
                }
            }
        }

        public ActionResult GestionarVisitasEvaluadas(Int32? id_visita2, Int32? id_prestador2, int? rta)
        {
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
            List<vw_visitas> visitas = new List<vw_visitas>();

            if (id_visita2 != null)
            {
                visitas = Model.GetListVisitas(id_visita2, null, null).Where(l => l.Realizo_evaluacion == true).ToList();
            }

            if (id_prestador2 != null)
            {
                visitas = Model.GetListVisitas(null, id_prestador2, null).Where(l => l.Realizo_evaluacion == true).ToList();
            }

            ViewData["rta"] = rta;
            ViewBag.prestador = id_prestador2;
            return View(visitas);
        }

        public ActionResult EliminarEvaluacionVisita(Int32? idvisita, Int32? id_prestador, string num_contrato)
        {
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();

            log_eliminacion_visitas log = new log_eliminacion_visitas();
            log.id_cronograma_visita = idvisita.Value;
            log.observaciones_log = "Eliminación de evaluación de visita de calidad";
            log.fecha_digita = DateTime.Now;
            log.usuario_digita = SesionVar.UserName;
            Model.EliminarEvaluacionVisita(idvisita.Value, log, ref MsgRes);

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                if (id_prestador != null)
                {
                    return RedirectToAction("GestionarVisitasEvaluadas", "Cambios", new { id_prestador2 = id_prestador, rta = 1 });
                }
                else
                {
                    return RedirectToAction("GestionarVisitasEvaluadas", "Cambios", new { id_visita2 = idvisita, rta = 1 });
                }
            }
            else
            {
                if (id_prestador != null)
                {
                    return RedirectToAction("GestionarVisitasEvaluadas", "Cambios", new { id_prestador2 = id_prestador, rta = 2 });
                }
                else
                {
                    return RedirectToAction("GestionarVisitasEvaluadas", "Cambios", new { id_visita2 = idvisita, rta = 2 });
                }
            }
        }


        public ActionResult GestionHcOdontologia()
        {
            ViewBag.regionales = BusClass.GetRefRegion();
            return View();
        }

        public ActionResult _idhistoriaclinica(int id_historia)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();
            List<vw_odont_historia_clinica> listhistoria = Model.GetListHistoriaClinica(id_historia);
            return View(listhistoria);
        }

        public ActionResult _OdontologoPrestador(string id_prestador)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();
            List<vw_odont_historia_clinica> listhistoria = Model.GetListHistoriaClinicaXOdontologo(id_prestador);
            return View(listhistoria);
        }

        public ActionResult _PacientesHistoriasclinicas(int id_historia, int modo, int? rta)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();
            List<odont_historia_clinica_paciente> Listhcpacientes = Model.GetHistoriaClinicaPaciente(id_historia, ref MsgRes);
            odont_historia_clinica hc = Model.GetHistoriaClinica().Where(l => l.id_odont_historia_clinica == id_historia).FirstOrDefault();
            ViewData["rta"] = rta;
            ViewBag.id_historia = id_historia;
            ViewBag.modo = modo;
            ViewBag.nomprestador = hc.nom_odontologo_auditado;
            return View(Listhcpacientes);
        }

        public ActionResult Eliminarhcpaciente(int id_historia, int modo, int id_hcpaciente)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();
            log_eliminacion_historias_clinicas_odontologia obj = new log_eliminacion_historias_clinicas_odontologia();
            obj.fecha_digita = DateTime.Now;
            obj.usuario_digita = SesionVar.UserName;
            Model.EliminarHistoriaclinica(id_hcpaciente, obj, ref MsgRes);
            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return RedirectToAction("_PacientesHistoriasclinicas", "Cambios", new { id_historia = id_historia, modo = modo, rta = 1 });
            }
            else
            {
                return RedirectToAction("_PacientesHistoriasclinicas", "Cambios", new { id_historia = id_historia, modo = modo, rta = 2 });
            }
        }


        public ActionResult ActualizarDatosEgreso(String numerodocumentoegreso2, int? rta)
        {
            var opcion = 0;

            List<vw_tablero_levante_egreso> list = BusClass.GetlevanteEgreso(numerodocumentoegreso2, ref MsgRes).ToList();

            if (list.Count != 0)
            {
                opcion = 1;
            }
            else
            {
                opcion = 2;
            }

            ViewBag.variante = opcion;
            ViewBag.numdocumento = numerodocumentoegreso2;

            ViewData["rta"] = rta;
            string msg = (string)TempData["mensaje"];
            if (string.IsNullOrEmpty(msg))
            {
                msg = "Datos actualizados exitosamente";
            }
            ViewData["msg"] = msg;
            return View(list);
        }

        public JsonResult ObtenerFechasEgreso(int idcenso, int idconcurrencia)
        {
            ecop_censo censo = BusClass.GetCensoId(idcenso, ref MsgRes).FirstOrDefault();
            ecop_concurrencia concu = BusClass.ConsultaConcurrenciaId(idconcurrencia);

            string fecha_egreso = "";
            string fecha_egreso_censo = "";

            if (censo.fecha_egreso_censo != null)
            {
                fecha_egreso_censo = censo.fecha_egreso_censo.Value.ToString("MM/dd/yyyy");
            }

            if (concu.fecha_egreso != null)
            {
                fecha_egreso = concu.fecha_egreso.Value.ToString("MM/dd/yyyy");
            }

            var data = new
            {
                idcenso = idcenso,
                idconcurrencia = idconcurrencia,
                fecha_egreso = fecha_egreso,
                fecha_egreso_censo = fecha_egreso_censo
            };

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ActualizarFechaEgresoCensov(int txtidcenso, DateTime fechaegresocensonew, string numdocumento)
        {
            ecop_censo censo = BusClass.GetCensoId(txtidcenso, ref MsgRes).FirstOrDefault();
            DateTime? fechaant = censo.fecha_egreso_censo;
            censo.fecha_egreso_censo = fechaegresocensonew;
            BusClass.ActualizarFechaEgresoCenso(censo, ref MsgRes);

            TempData["mensaje"] = MsgRes.DescriptionResponse;
            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                log_update_fecha_egreso log = new log_update_fecha_egreso();
                log.descripcion = "Actualizar fecha egreso censo";
                log.fecha_anterior = fechaant.Value;
                log.fecha_nueva = fechaegresocensonew;
                log.id_censo = txtidcenso;
                log.id_concurrencia = null;
                log.fecha_digita = DateTime.Now;
                log.usuario_digita = SesionVar.UserName;
                BusClass.InsertarLogActualizacionFechaEgreso(log, ref MsgRes);

                return RedirectToAction("ActualizarDatosEgreso", "Cambios", new { numerodocumentoegreso2 = numdocumento, rta = 1 });
            }
            else
            {
                return RedirectToAction("ActualizarDatosEgreso", "Cambios", new { numerodocumentoegreso2 = numdocumento, rta = 2 });
            }

        }

        public ActionResult ActualizarFechaEgresov(int txtidcensov2, int txtidconcurrenciav2, DateTime fechaegresonew, string numdocumento2)
        {
            ecop_censo censo = BusClass.GetCensoId(txtidcensov2, ref MsgRes).FirstOrDefault();
            DateTime? fechaant = censo.fecha_egreso;
            censo.fecha_egreso = fechaegresonew;
            BusClass.ActualizarFechaEgresoConcucenso(censo, txtidconcurrenciav2, ref MsgRes);

            TempData["mensaje"] = MsgRes.DescriptionResponse;
            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                log_update_fecha_egreso log = new log_update_fecha_egreso();
                log.descripcion = "Actualizar fecha egreso";
                log.fecha_anterior = fechaant.Value;
                log.fecha_nueva = fechaegresonew;
                log.id_censo = txtidcensov2;
                log.id_concurrencia = txtidconcurrenciav2;
                log.fecha_digita = DateTime.Now;
                log.usuario_digita = SesionVar.UserName;
                BusClass.InsertarLogActualizacionFechaEgreso(log, ref MsgRes);


                return RedirectToAction("ActualizarDatosEgreso", "Cambios", new { numerodocumentoegreso2 = numdocumento2, rta = 1 });
            }
            else
            {
                return RedirectToAction("ActualizarDatosEgreso", "Cambios", new { numerodocumentoegreso2 = numdocumento2, rta = 2 });
            }

        }

        public PartialViewResult ActualizarEstanciaEvolucion()
        {
            ViewBag.tipoEstancia = BusClass.GetTipoHabitacion();

            return PartialView();
        }

        public string BuscarEvoluciones(int? idConcurrencia)
        {
            string tabla = "";
            List<management_evoluciones_listadoIdConcurrenciaResult> listado = new List<management_evoluciones_listadoIdConcurrenciaResult>();
            try
            {
                listado = BusClass.TraerEvolucionesIdConcurrencia(idConcurrencia);
                if (listado.Count() > 0)
                {
                    foreach (var item in listado)
                    {
                        tabla += "<tr>";
                        tabla += $"<td> {item.id_evolucion} </td>";
                        tabla += $"<td> {item.descripcion} </td>";
                        tabla += $"<td> {item.descripcionTipoHabitacion} </td>";
                        tabla += $"<td> <a class='btn button_Asalud_Aceptar' onclick='MostrarTipoEstancia({item.id_evolucion} , {item.id_concurrencia})'> Editar </a> </td>";
                        tabla += "</tr>";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return tabla;
        }

        public JsonResult ActualizarTipoHabitacionEvolucion(int? idEvolucion, int? idConcurrencia, int? estancia)
        {
            var rta = 0;
            var mensaje = "";
            try
            {
                ecop_concurrencia_evolucion obj = new ecop_concurrencia_evolucion()
                {
                    id_evolucion = (int)idEvolucion,
                    id_tipo_habitacion = estancia
                };

                var idEstanciaAntigua = BusClass.ActualizarEstanciaEvolucion(obj);

                if(idEstanciaAntigua != 0)
                {
                    mensaje = "ESTANCIA ACTUALIZADA CORRECTAMENTE";
                    rta = 1;

                    log_ecop_concurrencia_evolucion_habitacion log = new log_ecop_concurrencia_evolucion_habitacion()
                    {
                        id_concurrencia = idConcurrencia,
                        id_evolucion = idEvolucion,
                        tipo_habitacion_anterior = idEstanciaAntigua,
                        tipo_habitacion_nuevo = estancia,
                        fecha_digita = DateTime.Now,
                        usuario_digita = SesionVar.UserName
                    };

                    var insertaLog = BusClass.InsertarLogCambioEstanciaEvolucion(log);
                }
                else
                {
                    mensaje = "ERROR AL ACTUALIZAR ESTANCIA";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ACTUALIZAR ESTANCIA: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }
    }
}

