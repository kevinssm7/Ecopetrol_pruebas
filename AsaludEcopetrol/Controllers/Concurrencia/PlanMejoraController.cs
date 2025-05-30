using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Controllers.InicioSesion;
using AsaludEcopetrol.Models.Concurrencia;
using Aspose.Email.Amp;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;

using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Concurrencia
{
    [SessionExpireFilter]
    public class PlanMejoraController : Controller
    {
        #region  PROPIEDADES
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

        #endregion
        public ActionResult IngresoCaracterización(int? idCronograma)
        {
            Models.Concurrencia.PlandeMejora Model = new Models.Concurrencia.PlandeMejora();
            List<management_planmejora_focoResult> lista = new List<management_planmejora_focoResult>();
            List<ref_medicion_riesgo> riesgo = new List<ref_medicion_riesgo>();
            List<ref_costos_noCalidad> noCalidad = new List<ref_costos_noCalidad>();
            List<ref_cobertura> cobertura = new List<ref_cobertura>();

            try
            {
                riesgo = BusClass.PlanMejoraMedicionRiesgo();
                noCalidad = BusClass.PlanMejoraCostosNoCalidad();
                cobertura = BusClass.PlanMejoraCobertura();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.proceso = BusClass.GetRef_proceso_auditado();
            ViewBag.categoria = BusClass.GetRefplan_mejora_categoria();
            ViewBag.foco = BusClass.GetRef_plan_mejora_foco();
            ViewBag.prioridad = BusClass.listaPrioridadPM();
            ViewData["cant_otroproducto"] = 0;
            ViewData["cant_otroproducto"] = lista.Count();

            ViewBag.Lista = lista;
            ViewBag.idCronograma = idCronograma;

            ViewBag.añoActual = DateTime.Now.Year;
            ViewBag.mesActual = DateTime.Now.Month;

            ViewBag.medicionRiesgo = riesgo;
            ViewBag.costosNoCalidad = noCalidad;
            ViewBag.mejoraCobertura = cobertura;

            ViewBag.sumaPuntajes = Convert.ToInt32(riesgo.OrderByDescending(x => x.puntaje).Select(x => x.puntaje).FirstOrDefault()) + Convert.ToInt32(noCalidad.OrderByDescending(x => x.puntaje).Select(x => x.puntaje).FirstOrDefault()) + Convert.ToInt32(cobertura.OrderByDescending(x => x.puntaje).Select(x => x.puntaje).FirstOrDefault());


            return View(Model);
        }

        public JsonResult GetCascadetrimestre(Int32 mes, Models.Concurrencia.PlandeMejora Model)
        {
            List<ref_trimeste> list = new List<ref_trimeste>();

            list = Model.GetRefTrimestre();
            list = list.Where(x => x.id_mes == mes).ToList();

            return Json(list.Select(p => new { id_ref_unis = p.id_ref_trimeste, nombre = p.nombre }), JsonRequestBehavior.AllowGet);
        }

        public string ObtenerUnis(int idregional)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_odont_unis> Unis = BusClass.Odont_unisIdRegional(idregional);
            foreach (var item in Unis)
            {
                result += "<option value='" + item.id_ref_unis + "'>" + item.descripcion + "</option>";
            }

            return result;
        }

        public string ObtenerCiudades(int idunis)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_ciudades> Ciudades = BusClass.GetCiudadesXUnis(idunis);
            foreach (var item in Ciudades)
            {
                result += "<option value='" + item.id_ref_ciudades + "'>" + item.nombre + "</option>";
            }

            return result;
        }

        public JsonResult SaveplanMejora(Models.Concurrencia.PlandeMejora Model)
        {
            String mensaje = "";

            try
            {
                ecop_plan_de_mejora obj = new ecop_plan_de_mejora();

                if (Model.id_visitas == 0)
                {
                    Model.id_visitas = null;
                }

                obj.id_visitas = Model.id_visitas;
                obj.id_regional = Model.id_regional;
                //obj.id_prioridad = Model.id_prioridad;

                obj.medicion_riesgo = Model.id_medicion;
                obj.costos_noCalidad = Model.id_noCalidad;
                obj.cobertura = Model.id_cobertura;
                obj.PuntajeFinal = Model.puntajeFinal;

                obj.id_unis = Model.id_unis;
                obj.id_localidad = Model.id_localidad;
                obj.id_prestador = Model.id_prestador;
                obj.fecha_creacion = Model.fecha_creacion;

                obj.id_proceso = Model.id_proceso;
                obj.estado_plan = 0;
                obj.usuario_ingreso = SesionVar.UserName;
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);

                obj.tipo_prioridad = new int?(Model.tipo_prioridad);
                if (Model.tipo_prioridad == 1)
                    obj.fecha_estimada_cierre = new DateTime?(Convert.ToDateTime((object)obj.fecha_creacion).AddMonths(1));
                else if (Model.tipo_prioridad == 2)
                    obj.fecha_estimada_cierre = new DateTime?(Convert.ToDateTime((object)obj.fecha_creacion).AddMonths(3));
                else if (Model.tipo_prioridad == 3)
                    obj.fecha_estimada_cierre = new DateTime?(Convert.ToDateTime((object)obj.fecha_creacion).AddMonths(6));

                Model.id_plan_de_mejora = Model.InsertarPlanMejora(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE.";
                    return Json(new { success = true, message = mensaje, id = Model.id_plan_de_mejora }, JsonRequestBehavior.AllowGet);
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

        public JsonResult SaveplanMejorafoco(Models.Concurrencia.PlandeMejora Model)
        {
            String mensaje = "";
            try
            {
                ecop_plan_mejora_foco_intervencion obj = new ecop_plan_mejora_foco_intervencion();

                obj.porque_1 = Model.porque1;
                obj.porque_2 = Model.porque2;
                obj.porque_3 = Model.porque3;
                obj.porque_4 = Model.porque4;
                obj.porque_5 = Model.porque5;

                obj.descripcion_problema = Model.descripcion_problema;

                obj.id_plan_de_mejora = Model.id_plan_de_mejora;
                obj.id_categoria = Model.id_categoria;
                obj.id_foco_gestion = Model.id_foco_gestion;
                obj.hallazgo = Model.porque5;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                if (Model.idFoco != 0 && Model.idFoco != null)
                {
                    obj.id_plan_mejora_foco_intervencion = (int)Model.idFoco;
                    BusClass.ActualizarFocoPlanMejora(obj, ref MsgRes);
                }
                else
                {
                    Model.InsertarPlanMejorafoco(obj, ref MsgRes);
                }

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    string result = "";
                    List<management_planmejora_focoResult> lista = new List<management_planmejora_focoResult>();

                    lista = Model.Cuentadetallefoco((int)Model.id_plan_de_mejora, ref MsgRes);
                    int i = 0;
                    foreach (var item in lista)
                    {
                        i += 1;
                        result += "<tr>";
                        result += "<td>";
                        result += $"<input type='hidden' id='porque1_{item.id_plan_mejora_foco_intervencion}' name='porque1_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_1}'/>";
                        result += $"<input type='hidden' id='porque2_{item.id_plan_mejora_foco_intervencion}' name='porque2_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_2}'/>";
                        result += $"<input type='hidden' id='porque3_{item.id_plan_mejora_foco_intervencion}' name='porque3_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_3}'/>";
                        result += $"<input type='hidden' id='porque4_{item.id_plan_mejora_foco_intervencion}' name='porque4_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_4}'/>";
                        result += $"<input type='hidden' id='porque5_{item.id_plan_mejora_foco_intervencion}' name='porque5_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_5}'/>";
                        result += $"<input type='hidden' id='desProblema_{item.id_plan_mejora_foco_intervencion}' name='desProblema_{item.id_plan_mejora_foco_intervencion}' value='{item.descripcion_problema}'/>";

                        result += $"<input type='hidden' id='idFoco_{item.id_plan_mejora_foco_intervencion}' name='idFoco_{item.id_plan_mejora_foco_intervencion}' value='{item.id_plan_mejora_foco_intervencion}'/>";
                        result += $"<input type='hidden' id='idcategoria_{item.id_plan_mejora_foco_intervencion}' name='idcategoria_{item.id_plan_mejora_foco_intervencion}' value='{item.id_categoria}'/>";
                        result += $"<input type='hidden' id='desCategoria_{item.id_plan_mejora_foco_intervencion}' name='desCategoria_{item.id_plan_mejora_foco_intervencion}' value='{item.categoria}'/>";
                        result += $"<input type='hidden' id='desFoco_{item.id_plan_mejora_foco_intervencion}' name='desFoco_{item.id_plan_mejora_foco_intervencion}' value='{item.foco}'/>";

                        result += i + "</td>";
                        result += "<td>" + item.id_plan_de_mejora + "</td>";
                        result += "<td>" + item.categoria + "</td>";
                        result += "<td>" + item.foco + "</td>";
                        result += "<td>" + item.hallazgo + "</td>";
                        result += "<td><a href='javascript:EditarPorques(" + item.id_plan_de_mejora + "," + item.id_plan_mejora_foco_intervencion + ")'>Editar porques</a></td>";
                        result += "<td><a title='Remover' href='javascript:removerotroProducto(" + item.id_plan_mejora_foco_intervencion + "," + item.id_plan_de_mejora + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                        result += "<td><a href='javascript:CargarTarea(" + item.id_plan_de_mejora + "," + item.id_plan_mejora_foco_intervencion + "," + Model.tipoPrioridad + ")'>Acciones de mejora</a></td>";
                        result += "</tr>";
                    }

                    var data = new
                    {
                        tabla = result,

                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                    // return Json(new { success = true, message = mensaje, id = Model.id_plan_de_mejora }, JsonRequestBehavior.AllowGet);
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

        public JsonResult RemoverOtroProducto(Models.Concurrencia.PlandeMejora Model)
        {
            String mensaje = "";
            string result = "";

            try
            {
                Model.EliminarDetallePlan(Model.id_plan_mejora_foco_intervencion, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    List<management_planmejora_focoResult> lista = Model.Cuentadetallefoco((int)Model.id_plan_de_mejora, ref this.MsgRes);

                    int i = 0;
                    foreach (var item in lista)
                    {
                        i += 1;
                        result += "<tr>";
                        result += "<td>";
                        result += $"<input type='hidden' id='porque1_{item.id_plan_mejora_foco_intervencion}' name='porque1_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_1}'/>";
                        result += $"<input type='hidden' id='porque2_{item.id_plan_mejora_foco_intervencion}' name='porque2_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_2}'/>";
                        result += $"<input type='hidden' id='porque3_{item.id_plan_mejora_foco_intervencion}' name='porque3_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_3}'/>";
                        result += $"<input type='hidden' id='porque4_{item.id_plan_mejora_foco_intervencion}' name='porque4_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_4}'/>";
                        result += $"<input type='hidden' id='porque5_{item.id_plan_mejora_foco_intervencion}' name='porque5_{item.id_plan_mejora_foco_intervencion}' value='{item.porque_5}'/>";
                        result += $"<input type='hidden' id='desProblema_{item.id_plan_mejora_foco_intervencion}' name='desProblema_{item.id_plan_mejora_foco_intervencion}' value='{item.descripcion_problema}'/>";

                        result += $"<input type='hidden' id='idFoco_{item.id_plan_mejora_foco_intervencion}' name='idFoco_{item.id_plan_mejora_foco_intervencion}' value='{item.id_plan_mejora_foco_intervencion}'/>";
                        result += $"<input type='hidden' id='idcategoria_{item.id_plan_mejora_foco_intervencion}' name='idcategoria_{item.id_plan_mejora_foco_intervencion}' value='{item.id_categoria}'/>";
                        result += $"<input type='hidden' id='desCategoria_{item.id_plan_mejora_foco_intervencion}' name='desCategoria_{item.id_plan_mejora_foco_intervencion}' value='{item.categoria}'/>";
                        result += $"<input type='hidden' id='desFoco_{item.id_plan_mejora_foco_intervencion}' name='desFoco_{item.id_plan_mejora_foco_intervencion}' value='{item.foco}'/>";

                        result += i + "</td>";
                        result += "<td>" + item.id_plan_de_mejora + "</td>";
                        result += "<td>" + item.categoria + "</td>";
                        result += "<td>" + item.foco + "</td>";
                        result += "<td>" + item.hallazgo + "</td>";
                        result += "<td><a href='javascript:EditarPorques(" + item.id_plan_de_mejora + "," + item.id_plan_mejora_foco_intervencion + ")'>Editar porques</a></td>";
                        result += "<td><a title='Remover' href='javascript:removerotroProducto(" + item.id_plan_mejora_foco_intervencion + "," + item.id_plan_de_mejora + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                        result += "<td><a href='javascript:CargarTarea(" + item.id_plan_de_mejora + "," + item.id_plan_mejora_foco_intervencion + "," + Model.tipoPrioridad + ")'>Acciones de mejora</a></td>";
                        result += "</tr>";
                    }


                    var data = new
                    {
                        tabla = result,
                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EN LA ELIMINACIÓN DEL DETALLE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "ERROR EN LA ELIMINACIÓN DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public PartialViewResult GestionarTareas(Int32? ID, Int32? ID2, int? tipoPrioridad)
        {
            Models.Concurrencia.PlandeMejora Model = new Models.Concurrencia.PlandeMejora();

            List<management_planmejora_tareaResult> list = new List<management_planmejora_tareaResult>();
            ecop_plan_de_mejora plan = new ecop_plan_de_mejora();

            management_planmejora_datosIntervencionResult intervencionResult = BusClass.datosIntervencionPM(ID2);

            var fechaCreacion = new DateTime();
            var fechaLimite = new DateTime();

            try
            {
                ecop_plan_de_mejora ecopPlanDeMejora2 = BusClass.PlanMejoraId(ID);
                if (ecopPlanDeMejora2 != null)
                {
                    var prioridad = ecopPlanDeMejora2.tipo_prioridad;

                    fechaCreacion = (DateTime)ecopPlanDeMejora2.fecha_creacion;
                    if (fechaCreacion != null)
                    {
                        if (prioridad == 1)
                        {
                            fechaLimite = fechaCreacion.AddMonths(1);
                        }
                        else
                        {
                            if (prioridad == 2)
                            {
                                fechaLimite = fechaCreacion.AddMonths(3);
                            }
                            else
                            {
                                fechaLimite = fechaCreacion.AddMonths(6);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.fechaCreacion = fechaCreacion;
            ViewBag.fechaLimite = fechaLimite;

            ViewBag.Lista = list;
            ViewBag.id_plan_mejora = ID;
            ViewBag.id_plan_mejora_foco_intervencion = ID2;

            ViewBag.categoria = intervencionResult.desCategoria;
            ViewBag.foco = intervencionResult.desFoco;
            ViewBag.desProblema = intervencionResult.descripcion_problema;
            ViewBag.hallazgo = intervencionResult.hallazgo;

            return PartialView(Model);
        }

        public JsonResult GestionarTareasBuscar(Models.Concurrencia.PlandeMejora Model)
        {
            string result = "";


            List<management_planmejora_tareaResult> lista = new List<management_planmejora_tareaResult>();

            lista = Model.CuentadetalleTarea(Model.id_plan_mejora_foco_intervencion, ref MsgRes);
            int i = 0;
            foreach (var item in lista)
            {
                i += 1;
                result += "<tr>";
                result += "<td>" + i + "</td>";
                result += "<td>" + item.id_plan_mejora_foco_intervencion + "</td>";
                result += "<td>" + item.tarea + "</td>";
                result += "<td>" + item.fecha_plazo + "</td>";
                result += "<td>" + item.responsable + "</td>";
                result += "<td>" + item.descripcionEstadoTarea + "</td>";
                result += "<td>" + item.fecha_seguimiento + "</td>";
                result += "<td><a title='Remover' href='javascript:removerotroProductoTarea(" + item.id_plan_mejora_foco_intervencion + "," + item.id_plan_mejora_tareas + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                result += "</tr>";
            }

            var data = new
            {
                tabla = result,

            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveplanMejoraTarea(Models.Concurrencia.PlandeMejora Model)
        {
            String mensaje = "";

            try
            {
                ecop_plan_mejora_tareas obj = new ecop_plan_mejora_tareas();

                obj.id_plan_mejora_foco_intervencion = Model.id_plan_mejora_foco_intervencion;
                obj.tarea = Model.tarea;
                obj.fecha_plazo = Model.fecha_plazo;
                obj.responsable = Model.responsable;
                obj.fecha_seguimiento = Model.fecha_seguimiento;
                obj.id_estado_tarea = 1;
                obj.usuario_gestiona = SesionVar.UserName;
                obj.fecha_gestion_tarea = Convert.ToDateTime(DateTime.Now);

                Model.id_plan_de_mejora = Model.InsertarPlanMejoratarea(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    string result = "";
                    List<management_planmejora_tareaResult> lista = new List<management_planmejora_tareaResult>();

                    lista = Model.CuentadetalleTarea(Model.id_plan_mejora_foco_intervencion, ref MsgRes);
                    int i = 0;
                    foreach (var item in lista)
                    {
                        i += 1;
                        result += "<tr>";
                        result += "<td>" + i + "</td>";
                        result += "<td>" + item.id_plan_mejora_foco_intervencion + "</td>";
                        result += "<td>" + item.tarea + "</td>";
                        result += "<td>" + item.fecha_plazo.Value.ToString("dd/MM/yyyy") + "</td>";
                        result += "<td>" + item.responsable + "</td>";
                        result += "<td>" + item.descripcionEstadoTarea + "</td>";
                        result += "<td>" + item.fecha_seguimiento + "</td>";
                        result += "<td><a title='Remover' href='javascript:removerotroProductoTarea(" + item.id_plan_mejora_foco_intervencion + "," + item.id_plan_mejora_tareas + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                        result += "</tr>";
                    }

                    var data = new
                    {
                        tabla = result,

                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
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

        public JsonResult RemoverOtroProductoTarea(Int32 ID, Int32 ID2)
        {
            String mensaje = "";
            Models.Concurrencia.PlandeMejora Model = new Models.Concurrencia.PlandeMejora();
            string result = "";

            Model.EliminarDetallePlanTarea(ID2, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {

                List<management_planmejora_tareaResult> lista = new List<management_planmejora_tareaResult>();

                lista = Model.CuentadetalleTarea(ID, ref MsgRes);
                int i = 0;
                foreach (var item in lista)
                {
                    i += 1;
                    result += "<tr>";
                    result += "<td>" + i + "</td>";
                    result += "<td>" + item.id_plan_mejora_foco_intervencion + "</td>";
                    result += "<td>" + item.fecha_plazo.Value.ToString("dd/MM/yyyy") + "</td>";
                    result += "<td>" + item.responsable + "</td>";
                    result += "<td>" + item.tarea + "</td>";
                    result += "<td>" + item.id_estado_tarea + "</td>";
                    result += "<td>" + item.fecha_seguimiento + "</td>";
                    result += "<td><a title='Remover' href='javascript:removerotroProductoTarea(" + item.id_plan_mejora_foco_intervencion + "," + item.id_plan_mejora_tareas + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                    result += "</tr>";
                }

                var data = new
                {
                    tabla = result,

                };

                return Json(data, JsonRequestBehavior.AllowGet);
                // return Json(new { success = true, message = mensaje, id = Model.id_plan_de_mejora }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult CerrarplanMejora(Models.Concurrencia.PlandeMejora Model)
        {
            String mensaje = "";
            List<management_planmejora_tarea_controlResult> list = new List<management_planmejora_tarea_controlResult>();
            var pasa = false;

            try
            {
                list = Model.CuentadetalleTareacontrol((int)Model.id_plan_de_mejora, ref MsgRes);

                if (list.Count != 0)
                {
                    var ALERTA = "NA";
                    foreach (var item in list)
                    {
                        if (item.tarea == "NO")
                        {
                            ALERTA = "SI";
                        }
                    }
                    if (ALERTA != "SI")
                    {
                        try
                        {
                            Model.ActualizarPlanEgreso((int)Model.id_plan_de_mejora, 1, ref MsgRes);

                            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                            {
                                mensaje = "SE INGRESÓ CORRECTAMENTE.";
                                return Json(new { success = true, message = mensaje, id = Model.id_plan_de_mejora }, JsonRequestBehavior.AllowGet);
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
                    else
                    {
                        mensaje = "ERROR... HAY HALLAZGOS SIN TAREAS ASIGNADAS.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "ERROR... DEBE INGRESAR ALMENOS UN HALLAZGO PARA ESTE PLAN.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR... DEBE INGRESAR ALMENOS UN HALLAZGO PARA ESTE PLAN: " + error;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CerrarplanMejoraCompleto(int? idPlan, int? cerradaForzada)
        {
            String mensaje = "";
            Models.Concurrencia.PlandeMejora Model = new PlandeMejora();
            List<management_planmejora_tarea_control_estadoResult> list = new List<management_planmejora_tarea_control_estadoResult>();
            try
            {
                if (cerradaForzada != 1)
                {
                    list = Model.CuentadetalleTareacontrolEstado((int)idPlan, ref MsgRes);

                    //Si es un cierre normal

                    if (list.Count != 0)
                    {
                        var ALERTA = "NA";
                        foreach (var item in list)
                        {
                            if (item.estadoTarea == "NO")
                            {
                                ALERTA = "SI";
                                break;
                            }
                        }
                        if (ALERTA != "SI")
                        {
                            try
                            {
                                Model.ActualizarPlanEgreso((int)idPlan, 2, ref MsgRes);
                                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                                {
                                    mensaje = "SE CERRÓ CORRECTAMENTE.";
                                    return Json(new { success = true, message = mensaje, id = idPlan }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    mensaje = "ERROR EN EL CIERRE DEL PLAN DE MEJORA.";
                                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            catch (Exception ex)
                            {
                                mensaje = "*---ERROR -- - *" + ex.Message;
                                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            mensaje = "ERROR... HAY ACCIONES SIN GESTIONAR.";
                            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                        }

                        //Si ya se envia mensaje de que no hay 
                    }
                    else
                    {
                        mensaje = "NO HAY ACCIONES PARA ESTE PLAN DE MEJORA";
                        return Json(new { success = false, message = mensaje, forzado = 1 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Model.ActualizarPlanEgreso((int)idPlan, 2, ref MsgRes);
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE CERRÓ CORRECTAMENTE.";
                        return Json(new { success = true, message = mensaje, id = idPlan }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR EN EL CIERRE DEL PLAN DE MEJORA.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TableroControlPlanMejora()
        {
            Models.Concurrencia.PlandeMejora Model = new Models.Concurrencia.PlandeMejora();
            List<management_plan_mejora_tableroResult> Lista = new List<management_plan_mejora_tableroResult>();
            List<management_plan_mejora_tableroResult> listAbiertos = new List<management_plan_mejora_tableroResult>();
            List<management_plan_mejora_tableroResult> listCerrados = new List<management_plan_mejora_tableroResult>();
            List<management_plan_mejora_tableroResult> listPorNotificar = new List<management_plan_mejora_tableroResult>();
            List<management_plan_mejora_tableroResult> listNotificados = new List<management_plan_mejora_tableroResult>();

            var conteo = 0;
            var rol = SesionVar.ROL;
            try
            {
                Lista = Model.PlanTableroGeneral();

                if (rol != "1" && SesionVar.IDUser != 154)
                {
                    Lista = Lista.Where(x => x.id_usuario == SesionVar.IDUser).ToList();
                }

                listAbiertos = Lista.Where(x => x.estadoplan == "Abierto").ToList();
                listCerrados = Lista.Where(x => x.estadoplan == "Cerrado").ToList();
                listPorNotificar = Lista.Where(x => x.estadoplan == "notificar al administrador del contrato").ToList();
                listNotificados = Lista.Where(x => x.estadoplan == "Notificado al administrador del contrato").ToList();

                Lista = Lista.OrderBy(x => x.fecha_respuesta).ToList();
                conteo = Lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = Lista;
            ViewBag.conteo = conteo;

            ViewBag.listAbiertos = listAbiertos;
            ViewBag.listCerrados = listCerrados;
            ViewBag.listPorNotificar = listPorNotificar;
            ViewBag.listNotificados = listNotificados;

            ViewBag.conteoAbiertas = listAbiertos.Count();
            ViewBag.conteoCerrados = listCerrados.Count();
            ViewBag.conteoPorNotificar = listPorNotificar.Count();
            ViewBag.conteoNotificados = listNotificados.Count();

            ViewBag.estado = Model.estadoTarea();

            Session["ListadoTableroPM"] = Lista;

            return View(Model);
        }

        public PartialViewResult MostrarTareasPlanMejora(int? idPlan)
        {
            Models.Concurrencia.PlandeMejora Model = new PlandeMejora();

            List<management_plan_mejora_tablero_dtllResult> Lista = new List<management_plan_mejora_tablero_dtllResult>();
            var conteo = 0;

            try
            {
                Lista = Model.PlanTableroGeneralDtll((int)idPlan, ref MsgRes);
                conteo = Lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idPlan = idPlan;
            ViewBag.listadoTareas = Lista;
            ViewBag.conteoTareas = conteo;
            return PartialView(Model);
        }

        public PartialViewResult MostrarAmpliacionPM(int? idPlan, DateTime? fechaMaxima, int? maximoAmpliaciones, DateTime? fechaInicial)
        {
            Models.Concurrencia.PlandeMejora Model = new PlandeMejora();
            management_plan_mejora_tableroResult dato = new management_plan_mejora_tableroResult();
            List<management_planMejora_ampliacionesResult> Lista = new List<management_planMejora_ampliacionesResult>();
            var conteo = 0;
            DateTime? fechaRespuesta = new DateTime();
            DateTime? fechaInicialAumentada = new DateTime();
            DateTime? fechaCreacion = new DateTime();
            DateTime? fechaAmpliacion = new DateTime();
            DateTime? fechaEstimada = new DateTime();

            try
            {
                Lista = BusClass.PlanMejoraAmpliaciones(idPlan);
                conteo = Lista.Count();

                dato = Model.PlanTableroGeneral().Where(x => x.id_plan_de_mejora == idPlan).FirstOrDefault();

                if (dato != null)
                {
                    fechaRespuesta = dato.fecha_respuesta;
                    fechaCreacion = dato.fecha_creacion;
                    fechaAmpliacion = dato.fecha_ampliacion;
                    fechaEstimada = dato.fecha_estimada_cierre;
                }

                if (fechaInicial.HasValue)
                {
                    fechaInicialAumentada = fechaInicial.Value.AddYears(1);
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idPlan = idPlan;
            ViewBag.fechaRespuesta = fechaRespuesta.Value.ToString("MM/dd/yyyy");
            ViewBag.listadoAmpliaciones = Lista;
            ViewBag.conteoAmpliaciones = conteo;
            ViewBag.fechaMaxima = fechaMaxima;
            ViewBag.maximoAmpliaciones = maximoAmpliaciones;

            ViewBag.fechaInicialAumentada = fechaInicialAumentada;

            ViewBag.fechaEstimada = fechaEstimada;
            ViewBag.fechaAmpliacion = fechaAmpliacion;

            ViewBag.fechaCreacion = fechaCreacion;

            return PartialView(Model);
        }

        public JsonResult GuardarAmpliacion(int? idPlan, DateTime? fechaAmpliacion, string obsAmpliacion)
        {
            log_PM_ampliaciones amp = new log_PM_ampliaciones();
            var mensaje = "";

            try
            {
                amp.id_pm = idPlan;
                amp.fecha_ampliacion = fechaAmpliacion;
                amp.observacion_ampliacion = obsAmpliacion;
                amp.usuario_digita = SesionVar.UserName;
                amp.fecha_digita = DateTime.Now;
                var insertaLog = BusClass.InsertarAmpliacionPlanMejora(amp);

                if (insertaLog != 0)
                {
                    var actualiza = BusClass.ActualizarPlanEgresoAmpliacion(fechaAmpliacion, obsAmpliacion, idPlan);
                    if (actualiza != 0)
                    {
                        mensaje = "AMPLIACIÓN INGRESADA CORRECTAMENTE";
                    }
                    else
                    {
                        mensaje = "ERROR EN EL INGRESO DE LA AMPLIACIÓN";
                    }
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE LA AMPLIACIÓN";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO AMPLIACIÓN: " + error;
            }

            return Json(new { mensaje = mensaje, id = idPlan });
        }

        public JsonResult GetCascadePrestadores(int? regional, int? ciudad, Models.Concurrencia.PlandeMejora Model)
        {
            List<management_ref_regiona_ciudadResult> List1 = new List<management_ref_regiona_ciudadResult>();
            List1 = BusClass.ListadoPrestadoresRegionalCiudad(regional, ciudad);
            return Json(List1.Select(p => new { id_ref_ips_cuentas = p.id_ref_ips_cuentas, Razón_Social = p.Nombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get2(int? page, int? limit)
        {

            Models.Concurrencia.PlandeMejora Model = new PlandeMejora();

            List<management_plan_mejora_tableroResult> Lista = new List<management_plan_mejora_tableroResult>();

            Lista = Model.PlanTableroGeneral();
            Lista = Lista.Where(x => x.id_usuario == SesionVar.IDUser).ToList();
            Lista = Lista.OrderBy(x => x.fecha_respuesta).ToList();

            var query = Lista;
            List<management_plan_mejora_tableroResult> records = new List<management_plan_mejora_tableroResult>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeams(int id_plan_de_mejora, int? page, int? limit)
        {
            Models.Concurrencia.PlandeMejora Model = new PlandeMejora();

            List<management_plan_mejora_tablero_dtllResult> Lista = new List<management_plan_mejora_tablero_dtllResult>();

            Lista = Model.PlanTableroGeneralDtll(id_plan_de_mejora, ref MsgRes);

            var query = Lista;
            List<management_plan_mejora_tablero_dtllResult> records = new List<management_plan_mejora_tablero_dtllResult>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GestionEstado(Int32? idTarea, Int32? estadoAct, Int32? idPlan)
        {
            Models.Concurrencia.PlandeMejora Model = new PlandeMejora();
            List<management_planmejora_tarea_obsResult> list = new List<management_planmejora_tarea_obsResult>();
            ecop_plan_de_mejora ecopPlanDeMejora = BusClass.PlanMejoraId(idPlan);

            var conteoGestiones = 0;

            try
            {
                list = Model.GetobsTareas(idTarea.Value, ref MsgRes);
                conteoGestiones = list.Count();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.Listestado = Model.estadoTarea().Where(x => x.id_ref_plan_mejora_estado_tarea >= 2).ToList();
            ViewBag.id_tarea = idTarea;
            ViewBag.id_plan_de_mejora = idPlan;
            ViewBag.estado_act = estadoAct;
            ViewBag.listaGestiones = list;
            ViewBag.conteoGestiones = conteoGestiones;
            ViewBag.fechaCreacion = ecopPlanDeMejora.fecha_creacion.Value.ToString("MM/dd/yyyy");

            return PartialView(Model);
        }

        public JsonResult SavegestionObsTareas(Models.Concurrencia.PlandeMejora Model)
        {
            String mensaje = "";

            try
            {
                if (Model.id_estado_tarea != 0)
                {
                    Model.Actualizarplan_estado_tarea((int)Model.id_plan_mejora_tareas, Model.id_estado_tarea, ref this.MsgRes);

                    if (this.MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                        return this.Json((object)new
                        {
                            success = false,
                            message = "ERROR AL ACTUALIZAR ESTADO."
                        },
                        JsonRequestBehavior.AllowGet
                        );
                }

                ecop_plan_mejora_obs_tareas obj = new ecop_plan_mejora_obs_tareas();

                obj.id_plan_de_mejora = Model.id_plan_de_mejora;
                obj.id_plan_mejora_tareas = Model.id_plan_mejora_tareas;
                obj.id_estado = Model.id_estado_tarea;
                obj.observacion = Model.observacion;
                obj.usuario_ingreso = SesionVar.UserName;
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                obj.fecha_seguimiento = Model.fecha_seguimiento_accion;

                if (Model.idAccion != 0 && Model.idAccion != null)
                {
                    obj.id_plan_mejora_obs_tareas = (int)Model.idAccion;
                    Model.id_plan_de_mejora = BusClass.ActualizarDatosPlanMejoraAccion(obj, ref this.MsgRes);
                }
                else
                {
                    Model.id_plan_de_mejora = Model.InsertarPlanMejora_obs(obj, ref MsgRes);
                }

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE.";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DE LA OBSERVACION.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TableroGestionDocumental(int? regional, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<management_planMejora_tableroDocumentalResult> listado = new List<management_planMejora_tableroDocumentalResult>();
            List<management_planesMejora_reporteSeguimientoResult> reporte = new List<management_planesMejora_reporteSeguimientoResult>();

            var conteo = 0;
            try
            {
                listado = BusClass.DatosPMgestionDocumental(regional, fechaInicio, fechaFin);
                reporte = BusClass.DatosPMgestionDocumentalReporte(regional, fechaInicio, fechaFin);
                conteo = listado.Count();
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.listado = listado;
            ViewBag.conteo = conteo;
            ViewBag.regionales = BusClass.GetRefRegion();

            Session["ListadoPlanMejoraGD"] = reporte;

            return View();
        }

        public PartialViewResult MostrarArchivosDocumental(int? idPlan, int? tipo)
        {
            List<management_planMejora_DocumentosPlanResult> listaArchivos = new List<management_planMejora_DocumentosPlanResult>();
            var conteo = 0;

            try
            {
                listaArchivos = BusClass.PlanMejoraArchivoporTipo(idPlan, tipo);
                conteo = listaArchivos.Count();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaArchivos = listaArchivos;
            ViewBag.conteoArchivos = conteo;
            ViewBag.rol = SesionVar.ROL;
            ViewBag.idPlan = idPlan;
            ViewBag.tipo = tipo;

            return PartialView();

        }

        public ActionResult VerArchivoPM(int? idArchivo)
        {
            try
            {
                ecop_plan_de_mejora_documental dato = new ecop_plan_de_mejora_documental();
                dato = BusClass.PlanMejoraGestionDocumentalIdArchivo(idArchivo);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extensionTipo = obj.extension;
                    var nombre = dato.nombre_archivo;

                    return File(dirpath, extensionTipo, nombre);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public JsonResult EliminarArchivoPM(int? idArchivo)
        {
            var mensaje = "";

            try
            {
                BusClass.EliminarArchivoPM(idArchivo, ref MsgRes);
                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "ARCHIVO ELIMINADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR ARCHIVO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR ARCHIVO: " + error;
            }

            return Json(new { mensaje = mensaje });
        }

        [HttpPost]
        public ActionResult GuardarArchivoPlanMejora(List<HttpPostedFileBase> files, int? idPlan, int? tipo)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            var respuesta = 0;

            var mensaje = "";
            var aprobada = false;

            try
            {
                foreach (var item in files)
                {
                    HttpPostedFileBase file = item;

                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentalPlanesMejora"];

                    sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                    string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                    MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                    DateTime fecha = DateTime.Now;
                    string archivo = string.Empty;

                    String carpeta = "";

                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                    {
                        carpeta = "ArchivosPlanMejora";
                    }
                    else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                    {
                        carpeta = "ArchivosPlanMejoraPruebas";
                    }

                    ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idPlan + "_" + tipo);
                    var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                    archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                    fecha, nombre, Path.GetExtension(file.FileName));

                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    file.SaveAs(archivo);

                    ecop_plan_de_mejora_documental OBJ = new ecop_plan_de_mejora_documental();
                    OBJ.id_planmejora = idPlan;
                    OBJ.id_tipo = tipo;
                    OBJ.ruta = Convert.ToString(archivo);
                    OBJ.extension = file.ContentType;
                    OBJ.nombre_archivo = file.FileName;
                    OBJ.fecha_digita = DateTime.Now;
                    OBJ.usuario_digita = SesionVar.UserName;

                    respuesta = BusClass.InsertarPlanMejora_documentos(OBJ);

                    if (respuesta != 0)
                    {
                        mensaje = "ARCHIVO GUARDADO CORRECTAMENTE";
                        aprobada = true;
                    }
                    else
                    {
                        mensaje = "ERROR EN EL INGRESO DEL ARCHIVO";
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DEL ARCHIVO: " + error;
            }

            return Json(new { mensaje = mensaje, rta = aprobada });
        }

        public ActionResult TableroVisitasInferiores()
        {
            List<management_planMejora_tableroVisitasResult> listado = new List<management_planMejora_tableroVisitasResult>();
            var conteo = 0;
            var auditor = SesionVar.UserName;

            try
            {
                listado = BusClass.DatosPMVisitas(auditor);
                conteo = listado.Count();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.conteo = conteo;
            ViewBag.auditor = auditor;

            Session["ListadoVisitasPlanMejora"] = listado;

            return View();
        }

        public void DescargarReportePM()

        {
            List<management_plan_mejora_tableroResult> lista = new List<management_plan_mejora_tableroResult>();

            try
            {
                lista = (List<management_plan_mejora_tableroResult>)Session["ListadoTableroPM"];
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosPlanMejora");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:J1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:J1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:J1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:J1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:J1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id plan mejora";
                    Sheet.Cells["B1"].Value = "Id visitas";
                    Sheet.Cells["C1"].Value = "Regional";
                    Sheet.Cells["D1"].Value = "Unis";
                    Sheet.Cells["E1"].Value = "Localidad";
                    Sheet.Cells["F1"].Value = "Prestador";
                    Sheet.Cells["G1"].Value = "Fecha creación";
                    Sheet.Cells["H1"].Value = "Proceso";
                    Sheet.Cells["I1"].Value = "Auditor";
                    Sheet.Cells["J1"].Value = "Fecha respuesta";

                    int row = 2;

                    foreach (management_plan_mejora_tableroResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":J" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_plan_de_mejora;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.id_visitas;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.nombre_regional;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.nom_unis;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.nombrelocalidad;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.nom_prestador;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_creacion;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nom_proceso;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.nombre;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_respuesta.Value.ToString("dd/MM/yyyy");

                        Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReportePlanMejora";
                    Sheet.Cells["A:J"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('ERROR EN LA DESCARGA');" +
                       "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public void DescargarReportePMGD()

        {
            List<management_planesMejora_reporteSeguimientoResult> lista = new List<management_planesMejora_reporteSeguimientoResult>();

            try
            {
                lista = (List<management_planesMejora_reporteSeguimientoResult>)Session["ListadoPlanMejoraGD"];
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosPlanMejora");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:W1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:W1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:W1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:W1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:W1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:W1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id plan mejora";
                    Sheet.Cells["B1"].Value = "Regional";
                    Sheet.Cells["C1"].Value = "Unis";
                    Sheet.Cells["D1"].Value = "Localidad";
                    Sheet.Cells["E1"].Value = "Trimestre/Año";
                    Sheet.Cells["F1"].Value = "Prestador";
                    Sheet.Cells["G1"].Value = "Proceso auditado";
                    Sheet.Cells["H1"].Value = "Foco de gestión";
                    Sheet.Cells["I1"].Value = "Hallazgo";
                    Sheet.Cells["J1"].Value = "Id plan mejora tareas";
                    Sheet.Cells["K1"].Value = "Tarea";
                    Sheet.Cells["L1"].Value = "Responsable";
                    Sheet.Cells["M1"].Value = "Fecha definición";
                    Sheet.Cells["N1"].Value = "Fecha seguimiento";
                    Sheet.Cells["O1"].Value = "Fecha real seguimiento";
                    Sheet.Cells["P1"].Value = "Observación seguimiento";
                    Sheet.Cells["Q1"].Value = "Estado acciones plan mejora";
                    Sheet.Cells["R1"].Value = "Descripción estado PM";
                    Sheet.Cells["S1"].Value = "Última fecha plazo";
                    Sheet.Cells["T1"].Value = "Fecha cierre";
                    Sheet.Cells["U1"].Value = "Fecha ampliación";
                    Sheet.Cells["V1"].Value = "Estado plan";
                    Sheet.Cells["W1"].Value = "Nombre auditor";

                    int row = 2;

                    foreach (management_planesMejora_reporteSeguimientoResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":V" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_plan_de_mejora;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.nombre_regional;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.descripcionUnis;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.Localidad;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.trimestreyaño;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.nombrePrestador;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.descripcionProcesoAuditado;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.descripcionFocoGestion;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.hallazgo;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.id_plan_mejora_tareas;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.tarea;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.responsable;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.fechadefinicion;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.fechaSeguimiento;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.fechaRealSeguimiento;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.observacionSeguimiento;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.estadoAccionesPlanMejora;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.descripcionEstadoPM;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.ultimaFechaPlazo;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.fecha_cierre;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.fechaAmpliacion;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.estadoplan;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.nombreAuditor;


                        Sheet.Cells[string.Format("M{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("N{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("O{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("T{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("U{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReportePlanMejoraDocumental";
                    Sheet.Cells["A:W"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('ERROR EN LA DESCARGA');" +
                       "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public void DescargarReportePMVisitas()

        {
            List<management_planMejora_tableroVisitasResult> lista = new List<management_planMejora_tableroVisitasResult>();

            try
            {
                lista = (List<management_planMejora_tableroVisitasResult>)Session["ListadoVisitasPlanMejora"];
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosVisitasAPlanMejora");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:K1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:K1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:K1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:K1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:K1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id visita";
                    Sheet.Cells["B1"].Value = "Regional";
                    Sheet.Cells["C1"].Value = "NIT";
                    Sheet.Cells["D1"].Value = "Razón social";
                    Sheet.Cells["E1"].Value = "Especialidad";
                    Sheet.Cells["F1"].Value = "Fecha programada visita";
                    Sheet.Cells["G1"].Value = "Fecha visita final";
                    Sheet.Cells["H1"].Value = "Tipo";
                    Sheet.Cells["I1"].Value = "Nro contrato";
                    Sheet.Cells["J1"].Value = "Auditor responsable";
                    Sheet.Cells["K1"].Value = "Calificación";

                    int row = 2;

                    foreach (management_planMejora_tableroVisitasResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":K" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_cronograma_visitas;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.nombre_regional;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.no_id_prestador;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.razon_social;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.descripcion;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_visita.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_final_visita.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nom_tipo_prestador;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.num_contrato;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.nombre;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.calificacion_final_visita;

                        Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReporteVisitasAPlanMejora";
                    Sheet.Cells["A:I"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('ERROR EN LA DESCARGA');" +
                       "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public void CrearReporteCreacionPM(int? idPlan)
        {
            //RUTA REPORTE

            List<management_planMejora_reporteResult> lst = new List<management_planMejora_reporteResult>();
            List<management_planMejora_reporte_detalleCeacionResult> lst2 = new List<management_planMejora_reporte_detalleCeacionResult>();

            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptCreacionPM.rdlc");

                lst = BusClass.DatosPMReporte(idPlan);
                lst2 = BusClass.DatosPMReporteDetalleCreacion(idPlan);

                string filename = "PlanMejora" + idPlan;

                //ASIGNAICON  DATASET A REPORT
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPMCreacion", lst);
                Microsoft.Reporting.WebForms.ReportDataSource rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPMCreacionDetalle", lst2);

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.DataSources.Add(rds2);

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
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void CrearReporteCierrePM(int? idPlan)
        {
            //RUTA REPORTE

            List<management_planMejora_reporteResult> lst = new List<management_planMejora_reporteResult>();
            List<management_planMejora_reporte_detalleCierreResult> lst2 = new List<management_planMejora_reporte_detalleCierreResult>();

            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptCierrePM.rdlc");

                lst = BusClass.DatosPMReporte(idPlan);
                lst2 = BusClass.DatosPMReporteDetalleCierre(idPlan);

                string filename = "PlanMejora" + idPlan;

                //ASIGNAICON  DATASET A REPORT
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPMCreacion", lst);
                Microsoft.Reporting.WebForms.ReportDataSource rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPMCierreDetalle", lst2);

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.DataSources.Add(rds2);

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
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public ActionResult TableroReactivacionPlan(int? idPlan)
        {

            List<management_plan_mejora_tablero_reactivacionResult> planes = new List<management_plan_mejora_tablero_reactivacionResult>();

            try
            {
                if (idPlan != null)
                {
                    planes = BusClass.TraerListadoPlanesMejora(idPlan);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.planes = planes;
            ViewBag.conteo = planes.Count();

            return View();
        }

        public JsonResult ReabrirPlan(int? idPlan)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var actualiza = BusClass.ActualizarEstadoPlanesDeMejora(idPlan);
                if (actualiza != 0)
                {
                    log_plan_mejora_reactivar reac = new log_plan_mejora_reactivar();
                    reac.id_plan_mejora = idPlan;
                    reac.fecha_digita = DateTime.Now;
                    reac.usuario_digita = SesionVar.UserName;

                    var logueado = BusClass.InsertarLogReactivacionPlanMejora(reac);

                    mensaje = "EL PLAN DE MEJORA SE REABRIO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL REABRIR EL PLAN DE MEJORA";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL REABRIR EL PLAN DE MEJORA: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult EliminarPlan(int? idPlan)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var elimina = BusClass.EliminarPlanDeMejora_id(idPlan);
                if (elimina != 0)
                {
                    log_plan_mejora_eliminar elim = new log_plan_mejora_eliminar();
                    elim.id_plan_mejora = idPlan;
                    elim.fecha_digita = DateTime.Now;
                    elim.usuario_digita = SesionVar.UserName;

                    var logueado = BusClass.InsertarLogEliminarPlanMejora(elim);

                    mensaje = "EL PLAN DE MEJORA SE ELIMINÓ CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR EL PLAN DE MEJORA";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR EL PLAN DE MEJORA: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });

        }

        public void ValidarCerradaPlanesMejora()
        {
            List<management_planesMejora_alertaVencimientoResult> source = this.BusClass.ListadoAlertasVencimiento();
            List<log_planes_mejora_notificaciones> mejoraNotificacionesList = new List<log_planes_mejora_notificaciones>();
            if (source.Count<management_planesMejora_alertaVencimientoResult>() <= 0)
                return;
            foreach (management_planesMejora_alertaVencimientoResult vencimientoResult in source)
            {
                int? idPlanDeMejora = vencimientoResult.id_plan_de_mejora;
                int? idUsuario = vencimientoResult.id_usuario;
                int? nullable1 = vencimientoResult.diferenciaDias;
                int num1 = 0;
                int num2;
                if (!(nullable1.GetValueOrDefault() >= num1 & nullable1.HasValue))
                {
                    nullable1 = vencimientoResult.diferenciaDias;
                    int num3 = 1;
                    if (!(nullable1.GetValueOrDefault() < num3 & nullable1.HasValue))
                    {
                        nullable1 = vencimientoResult.diferenciaDias;
                        int num4 = 7;
                        if (!(nullable1.GetValueOrDefault() >= num4 & nullable1.HasValue))
                        {
                            nullable1 = vencimientoResult.diferenciaDias;
                            int num5 = 8;
                            if (!(nullable1.GetValueOrDefault() < num5 & nullable1.HasValue))
                            {
                                num2 = 0;
                                goto label_10;
                            }
                        }
                        num2 = 2;
                        goto label_10;
                    }
                }
                num2 = 1;
                label_10:
                int num6 = num2;
                if (num6 != 0)
                {
                    int? nullable2 = new int?(0);
                    log_planes_mejora_notificaciones mejoraNotificaciones1 = this.BusClass.TraerUltimoLogNotificacionPM(idPlanDeMejora);
                    if (mejoraNotificaciones1 != null)
                    {
                        if ((DateTime.Now - mejoraNotificaciones1.fecha_digita.Value).TotalHours > 24.0)
                            nullable2 = new int?(1);
                    }
                    else
                        nullable2 = new int?(1);
                    nullable1 = nullable2;
                    int num7 = 1;
                    if (nullable1.GetValueOrDefault() == num7 & nullable1.HasValue && this.EnviarCorreoVencimiento(vencimientoResult, new int?(num6)) == "")
                    {
                        log_planes_mejora_notificaciones mejoraNotificaciones2 = new log_planes_mejora_notificaciones();
                        mejoraNotificaciones2.id_plan = idPlanDeMejora;
                        mejoraNotificaciones2.usuario_digita = this.SesionVar.UserName;
                        mejoraNotificaciones2.fecha_digita = new DateTime?(DateTime.Now);
                        nullable1 = vencimientoResult.conteoNotificaciones;
                        int num8 = 0;
                        mejoraNotificaciones2.alerta_nro = new int?(nullable1.GetValueOrDefault() == num8 & nullable1.HasValue ? 1 : 2);
                        log_planes_mejora_notificaciones mejoraNotificaciones3 = mejoraNotificaciones2;
                        mejoraNotificacionesList.Add(mejoraNotificaciones3);
                    }
                }
            }
            if (mejoraNotificacionesList.Count<log_planes_mejora_notificaciones>() > 0)
                this.BusClass.InsertarLogAlertasPlanes(mejoraNotificacionesList);
        }

        public string EnviarCorreoVencimiento(management_planesMejora_alertaVencimientoResult item, int? tipoAlerta)
        {
            string str1 = "";
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string str2 = "desarrollo.soporte@asalud.co";
            StringBuilder sb = new StringBuilder();

            string textBody = sb.ToString();

            string filename = "";

            textBody += " Estimado/a ";
            textBody += item.nombreDigita + ",";
            textBody += "<br />";
            textBody += "<br />";
            textBody += $"Le recordamos que la siguiente acción de mejora asignada en el plan de mejora con ID {item.id_plan_de_mejora} y prioridad {item.tipoPrioridadDescripcion} está próxima a vencer. ";


            textBody += "<br />";
            textBody += "<br />";
            textBody += "A continuación, encontrará los detalles de la acción:";
            textBody += "<br />";
            textBody += "<br />";
            textBody += "Nombre del Prestador: " + item.nombrePrestador;
            textBody += "<br />";
            textBody += "Nombre de la Acción de Mejora: ";
            textBody += item.tarea;
            textBody += "<br />";
            textBody += $"Fecha de vencimiento: {item.fecha_plazo}";
            textBody += "<br />";
            textBody += "<br />";
            textBody += "Agradecemos que revise el avance de esta acción y, de ser necesario, realice las gestiones correspondientes y actualizaciones en SAMI para su cumplimiento en el plazo establecido.";

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

                mailMessage.From = new MailAddress("admin@asaludltda.com");

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    string str6 = item.correo_ins != "" ? item.correo_ins : item.correo;
                    mailMessage.To.Add(str6.Replace(" ", ""));
                    mailMessage.To.Add("coordinacionambulatoria@asalud.co");
                }
                else
                {
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                    mailMessage.To.Add(str2.Replace(" ", ""));
                }
                mailMessage.Subject = "[Mensaje Automático] Alerta de Vencimiento - Acción de Mejora";

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

            }
            catch (Exception ex)
            {
                str1 = "ERROR: " + ex.Message;
            }

            return str1;
        }

        public PartialViewResult ModalCargueSoportesAdmin(int? idPlan)
        {
            ViewBag.idPlan = idPlan;
            return this.PartialView();
        }

        [HttpPost]
        public ActionResult GuardarNotificacionAdmin(int? idPlan, string correos, string observacion)
        {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            bool flag = false;
            List<ecop_plan_de_mejora_notificarAdmin_archivos> notificarAdminArchivosList = new List<ecop_plan_de_mejora_notificarAdmin_archivos>();
            string str;
            try
            {

                ecop_plan_de_mejora_notificarAdmin obj = new ecop_plan_de_mejora_notificarAdmin()
                {
                    id_plan = idPlan,
                    correos = correos,
                    observacion = observacion,
                    fecha_digita = new DateTime?(DateTime.Now),
                    usuario_digita = this.SesionVar.UserName,
                };

                var inserta = BusClass.IngresarPlanMejora_notificacionAdmin(obj);

                if (inserta != 0)
                {
                    string message = this.EnviarCorreoNotificacion(correos, idPlan, observacion);
                    if (message.Contains("ERROR"))
                    {
                        throw new Exception(message);
                    }

                    BusClass.ActualizarEstadoPlanMejora(idPlan, new int?(3));
                    str = "NOTIFICACIÓN ENVIADA CORRECTAMENTE";
                }
                else
                {
                    throw new Exception("ERROR AL INGRESAR LA NOTIFICACIÓN");
                }
            }
            catch (Exception ex)
            {
                str = "ERROR EN EL INGRESO DEL ARCHIVO: " + ex.Message;
            }
            return (ActionResult)this.Json((object)new
            {
                mensaje = str,
                rta = flag
            });
        }

        public string EnviarCorreoNotificacion(string correos, int? idPlan, string observacion)
        {
            SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string str1 = "";
            management_planMejora_correosNotificacionIdPlanResult notificacionIdPlanResult = this.BusClass.DatosNotificacionCorreos(idPlan);
            MemoryStream contentStream = new MemoryStream(this.CrearReporteCreacionPMDevolverPdf(idPlan));
            string str2 = "Estimado/a Administrador/a de Contrato," + "<br />" + "<br />" + string.Format("Por medio de la presente, notificamos que el plan de mejora identificado con el número {0}, de prioridad {1}, no cumplió con las condiciones ni los plazos establecidos y conciliados con el prestador, debido a los siguientes motivos:", (object)idPlan, (object)notificacionIdPlanResult.descripcionPrioridad) + "<br />" + "<br />" + observacion + "<br />" + "<br />" + "A continuación, compartimos los detalles del plan:" + "<br />" + "<br />" + "- Nombre del Prestador: " + notificacionIdPlanResult.nombrePrestador + "<br />" + "- Localidad: " + notificacionIdPlanResult.nombreCiudad + "<br />" + "- UNIS: " + notificacionIdPlanResult.nombreUnis + "<br />" + string.Format("- Fecha de inicio del plan de mejora: {0}", (object)notificacionIdPlanResult.fecha_creacion.Value.ToString("dd/MM/yyyy")) + "<br />" + "Solicitamos amablemente revisar el plan de mejora y tomar las acciones pertinentes." + "<br />" + "<br />" + "Saludos cordiales," ?? "";
            string[] strArray = correos.Split(';');
            try
            {
                string str3 = "<style>" + "body { margin: 0; padding: 0; }" + ".PageContainer{ background-repeat: no-repeat; width: 600px; height: 900px; }" + "#ContentContainer { clear: both; width: 600px; height: 187px; }" + "#LeftPane { width: 260px; padding-top: 150px; float: left; padding-left: 40px; color='#F1C40F';}" + "#RightPane { width: 230px; float: right; padding-top: 150px; text-align: center; padding-right: 30px; }" + "#RightPane2 {width: 260px; float: right; padding-top: 150px; text-align: center; }" + ".tableClass { border: dashed 1px #000000; padding: 5px 5px 5px 5px; }" + "#FooterContainer { clear: both; width: 590px; padding-top: 515px; height: 80px; font-size: 0.6em; padding-left: 10px; font-weight: bold; }" + ".a { color: #063971; }" + "</style>";
                string str4 = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">" + "<div class='PageContainer' style=' font-family: Century Gothic, Century Gothic, sans-serif; '>" + "<div id='ContentContainer' style=' color: #063971;'>" + "<div id='LeftPane' style='font-size: 14.5px;'>" + "<br />" + str2 + "<br />" + "<br />" + "</div>" + "<div id='RightPane' align='center'  style='font-size: 13px;'>" + "<br />" + "<img src='cid:dealer_logo' />" + "<br />" + "<STRONG>ASALUD</STRONG>" + "<br />" + "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>" + "<br />" + "Bogotá" + "</div>" + "<br />" + "<div id='RightPane2' align='center'  style='font-size: 10.5px;'>" + "“El contenido de este mensaje electrónico, su texto o elementos adjuntos, son de uso confidencial y privado entre el remitente y su (s) destinatario (s). Cualquier uso o distribución indebida o sin autorización escrita por parte del remitente, se encuentran estrictamente prohibidas y acarrean sanciones penales. Por tanto, en caso de recibir este mensaje por error, por favor notificarlo y devolverlo de inmediato al remitente del mismo, sin guardar copia alguna..." + "<br />" + "<br />" + "Por un medio ambiente sano, imprima solo lo necesario." + "</div>" + "</div>" + "</div>";
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = section.Network.Host;
                smtpClient.Port = section.Network.Port;
                smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(section.Network.UserName, section.Network.Password);
                ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)((s, certificate, chain, sslPolicyErrors) => true);
                smtpClient.EnableSsl = true;
                MailMessage message = new MailMessage();
                AlternateView alternateViewFromString = AlternateView.CreateAlternateViewFromString(str4.ToString(), new ContentType("text/html"));
                message.Attachments.Add(new Attachment((Stream)contentStream, "CartaCreacion_" + idPlan.ToString() + ".pdf", "application/pdf"));
                LinkedResource linkedResource = new LinkedResource(AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                linkedResource.ContentId = "dealer_logo";
                alternateViewFromString.LinkedResources.Add(linkedResource);
                message.AlternateViews.Add(alternateViewFromString);
                message.From = new MailAddress("admin@asaludltda.com");
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    foreach (string str5 in strArray)
                        message.To.Add(str5.Replace(" ", ""));
                }
                else
                {
                    message.To.Add("desarrollo.soporte@asalud.co");
                    message.To.Add("ivanduran@asalud.co");
                    foreach (string str6 in strArray)
                        message.To.Add(str6.Replace(" ", ""));
                }
                message.Subject = "[Mensaje Automático] " + string.Format("Notificación de Incumplimiento en Plan de Mejora - {0}", (object)idPlan);
                message.IsBodyHtml = true;
                message.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + str3 + "</head><body> " + str2 + "<br>" + str4 + "</body></HTML>";
                message.IsBodyHtml = true;
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                str1 = "ERROR AL ENVIAR CORREOS DE NOTIFICACIÓN";
            }
            return str1;
        }

        public byte[] CrearReporteCreacionPMDevolverPdf(int? idPlan)
        {
            List<management_planMejora_reporteResult> mejoraReporteResultList = new List<management_planMejora_reporteResult>();
            List<management_planMejora_reporte_detalleCeacionResult> detalleCeacionResultList = new List<management_planMejora_reporte_detalleCeacionResult>();
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/ReportViewer"), "RptCreacionPM.rdlc");
                List<management_planMejora_reporteResult> dataSourceValue1 = this.BusClass.DatosPMReporte(idPlan);
                List<management_planMejora_reporte_detalleCeacionResult> dataSourceValue2 = this.BusClass.DatosPMReporteDetalleCreacion(idPlan);
                string str2 = "PlanMejora" + idPlan.ToString();
                ReportDataSource reportDataSource1 = new ReportDataSource("DataSetPMCreacion", (IEnumerable)dataSourceValue1);
                ReportDataSource reportDataSource2 = new ReportDataSource("DataSetPMCreacionDetalle", (IEnumerable)dataSourceValue2);
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportPath = str1;
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                reportViewer.LocalReport.DataSources.Add(reportDataSource2);
                if (dataSourceValue1.Count == 0)
                    return (byte[])null;
                try
                {
                    reportViewer.LocalReport.Refresh();
                    return reportViewer.LocalReport.Render("PDF", (string)null, out string _, out string _, out string _, out string[] _, out Warning[] _);
                }
                catch (Exception ex)
                {
                    this.MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    this.MsgRes.DescriptionResponse = ex.Message;
                    return (byte[])null;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return (byte[])null;
            }
        }

        public JsonResult EnviarNotificacionLiderazgo(int? idPlan, string observacion)
        {
            string str1;
            try
            {
                sis_usuario sisUsuario = this.BusClass.datosUsuarioUser(this.BusClass.PlanMejoraId(idPlan).usuario_ingreso);
                string correos = sisUsuario.correo_ins != null ? sisUsuario.correo_ins : sisUsuario.correo;
                string str2 = this.EnviarCorreoNotificacion(correos, idPlan, observacion);
                if (str2.Contains("ERROR"))
                    throw new Exception("ERROR AL ENVIAR CORREO: " + str2);
                if (this.BusClass.ActualizarEstadoPlanMejora(idPlan, new int?(3)) != 0)
                {
                    if (this.BusClass.IngresarPlanMejora_notificacionAdmin(new ecop_plan_de_mejora_notificarAdmin()
                    {
                        id_plan = idPlan,
                        correos = correos,
                        fecha_digita = new DateTime?(DateTime.Now),
                        usuario_digita = this.SesionVar.UserName
                    }) == 0)
                        throw new Exception("ERROR AL INGRESAR LA NOTIFICACIÓN");
                    str1 = "NOTIFICACIÓN ENVIADA CORRECTAMENTE";
                }
                else
                    str1 = "ERROR AL ENVIAR LA NOTIFICACIÓN";
            }
            catch (Exception ex)
            {
                str1 = "ERROR: " + ex.Message;
            }
            return this.Json((object)new { mensaje = str1 });
        }

        [HttpPost]
        public ActionResult CrearReporteCreacionPMFirma(int? idPlan, HttpPostedFileBase firma)
        {
            List<management_planMejora_reporteResult> mejoraReporteResultList = new List<management_planMejora_reporteResult>();
            List<management_planMejora_reporte_detalleCeacionResult> detalleCeacionResultList = new List<management_planMejora_reporte_detalleCeacionResult>();
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/ReportViewer"), "RptCreacionPM.rdlc");
                string str2 = string.Empty;
                if (firma != null && firma.ContentLength > 0)
                {
                    using (MemoryStream destination = new MemoryStream())
                    {
                        firma.InputStream.CopyTo((Stream)destination);
                        str2 = Convert.ToBase64String(destination.ToArray());
                    }
                }

                List<management_planMejora_reporteResult> dataSourceValue1 = this.BusClass.DatosPMReporte(idPlan);
                List<management_planMejora_reporte_detalleCeacionResult> dataSourceValue2 = this.BusClass.DatosPMReporteDetalleCreacion(idPlan);
                string fileDownloadName = "PlanMejora" + idPlan.ToString() + ".pdf";
                ReportDataSource reportDataSource1 = new ReportDataSource("DataSetPMCreacion", (IEnumerable)dataSourceValue1);
                ReportDataSource reportDataSource2 = new ReportDataSource("DataSetPMCreacionDetalle", (IEnumerable)dataSourceValue2);
                ReportParameter parameter = new ReportParameter("Imagen", str2);
                ReportViewer reportViewer = new ReportViewer()
                {
                    ProcessingMode = ProcessingMode.Local,
                    LocalReport = {
            EnableExternalImages = true,
            ReportPath = str1
          }
                };
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                reportViewer.LocalReport.DataSources.Add(reportDataSource2);
                reportViewer.LocalReport.SetParameters(parameter);
                reportViewer.LocalReport.Refresh();
                string mimeType;
                return (ActionResult)this.File(reportViewer.LocalReport.Render("PDF", (string)null, out mimeType, out string _, out string _, out string[] _, out Warning[] _), mimeType, fileDownloadName);
            }
            catch (Exception ex)
            {
                return (ActionResult)new HttpStatusCodeResult(500, "Error al generar el reporte: " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CrearReporteCierrePMFirma(int? idPlan, HttpPostedFileBase firma)
        {
            List<management_planMejora_reporteResult> mejoraReporteResultList = new List<management_planMejora_reporteResult>();
            List<management_planMejora_reporte_detalleCierreResult> detalleCierreResultList = new List<management_planMejora_reporte_detalleCierreResult>();
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/ReportViewer"), "RptCierrePM.rdlc");
                string str2 = string.Empty;
                if (firma != null && firma.ContentLength > 0)
                {
                    using (MemoryStream destination = new MemoryStream())
                    {
                        firma.InputStream.CopyTo((Stream)destination);
                        str2 = Convert.ToBase64String(destination.ToArray());
                    }
                }

                List<management_planMejora_reporteResult> dataSourceValue1 = this.BusClass.DatosPMReporte(idPlan);
                List<management_planMejora_reporte_detalleCierreResult> dataSourceValue2 = this.BusClass.DatosPMReporteDetalleCierre(idPlan);
                string fileDownloadName = "PlanMejora" + idPlan.ToString();
                ReportDataSource reportDataSource1 = new ReportDataSource("DataSetPMCreacion", (IEnumerable)dataSourceValue1);
                ReportDataSource reportDataSource2 = new ReportDataSource("DataSetPMCierreDetalle", (IEnumerable)dataSourceValue2);
                ReportParameter parameter = new ReportParameter("ImagenCierre", str2);
                ReportViewer reportViewer = new ReportViewer()
                {
                    ProcessingMode = ProcessingMode.Local,
                    LocalReport = {
            EnableExternalImages = true,
            ReportPath = str1
          }
                };
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                reportViewer.LocalReport.DataSources.Add(reportDataSource2);
                reportViewer.LocalReport.SetParameters(parameter);
                reportViewer.LocalReport.Refresh();
                string mimeType;
                return (ActionResult)this.File(reportViewer.LocalReport.Render("PDF", (string)null, out mimeType, out string _, out string _, out string[] _, out Warning[] _), mimeType, fileDownloadName);
            }
            catch (Exception ex)
            {
                return (ActionResult)new HttpStatusCodeResult(500, "Error al generar el reporte: " + ex.Message);
            }
        }


    }
}