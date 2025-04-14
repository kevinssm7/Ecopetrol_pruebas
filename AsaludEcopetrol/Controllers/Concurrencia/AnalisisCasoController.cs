using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using AsaludEcopetrol.Models;
using System.IO;
using ECOPETROL_COMMON.ENUM;
using Microsoft.Reporting.WebForms;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Concurrencia
{
    [SessionExpireFilter]
    public class AnalisisCasoController : Controller
    {

        #region EVENTOS PRIVADOS

        #endregion

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

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
        General Cgeneral = new General();
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TableroControl()
        {
            string idrolesesion = SesionVar.ROL;

            Models.analisis_de_caso.tablerocontrol Model = new Models.analisis_de_caso.tablerocontrol();

            List<vw_tablero_analisis_casos> listacasos = Model.ConsultaTableroAnalisisCasos();

            if (idrolesesion == "1")
            {
                ViewBag.listaCasos = listacasos.Where(l => l.estado_caso != 2).ToList();
            }
            else
            {
                ViewBag.listaCasos = listacasos.Where(l => l.usuario_digita == SesionVar.UserName && (l.estado_caso != 2)).ToList();
            }

            ViewBag.rolusuario = idrolesesion;
            return View(Model);
        }

        [HttpGet]
        public ActionResult CreateAnalisisAlertas(int? id_concurrencia, int? opc, int? idanalisis, string numdocumento)
        {
            Models.analisis_de_caso.AnalisisOriginal Model = new Models.analisis_de_caso.AnalisisOriginal();

            if (idanalisis != null)
            {
                analisis_caso_alertas obj = Model.ConsultaAnalisisCasoAlertas(id_concurrencia, idanalisis).FirstOrDefault();
                if (obj != null)
                {
                    Model = Model.SetearvaloresAnalisisAlertas(obj);
                }
            }
            else
            {
                Model.ConsultaIdConcurrenciaAlertas(Convert.ToInt32(id_concurrencia));

            }

            if (opc == null)
                opc = 1;

            Model.opc = opc.Value;
            ViewData["Alerta"] = "";
            ViewBag.usuario = SesionVar.ROL;

            return View(Model);
        }

        public ActionResult CreateAnalisisMuestreo(int? id_concurrencia, int? opc, int? idanalisis)
        {
            Models.analisis_de_caso.AnalisisMuestreo Model = new Models.analisis_de_caso.AnalisisMuestreo();

            if (idanalisis != null)
            {
                analisis_caso_muestreo obj = Model.ConsultaAnalisisCasoMuestreo(id_concurrencia, idanalisis).FirstOrDefault();
                if (obj != null)
                {
                    Model = Model.SetearvaloresAnalisisMuestreo(obj);
                }
            }
            else
            {
                Model.ConsultaIdConcurrenciaMuestreo(Convert.ToInt32(id_concurrencia));

            }

            if (opc == null)
                opc = 1;

            Model.opc = opc.Value;
            ViewData["Alerta"] = "";
            ViewBag.usuario = SesionVar.ROL;

            return View(Model);
        }

        /// <summary>
        /// Pantalla inicial, crear analisis de caso
        /// </summary>
        /// <param name="id_concurrencia"></param>
        /// <param name="opc"></param>
        /// <returns></returns>
        public ActionResult CreateAnalisisCaso(int id_concurrencia, int? opc, int? idanalisis)
        {
            Models.analisis_de_caso.AnalisisOriginal Model = new Models.analisis_de_caso.AnalisisOriginal();

            if (idanalisis != null)
            {
                analisis_caso_original obj = Model.ConsultaAnalisisCasoOriginal(id_concurrencia, idanalisis).FirstOrDefault();
                if (obj != null)
                {
                    Model = Model.SetearvaloresAnalisis(obj);
                }
            }

            if (opc == null)
                opc = 1;
            Model.opc = opc.Value;
            ViewData["Alerta"] = "";
            ViewBag.usuario = SesionVar.ROL;

            return View(Model);
        }

        /// <summary>
        /// Pantalla inicial, crear analisis de casos de salud publica
        /// </summary>
        /// <param name="id_concurrencia"></param>
        /// <param name="opc"></param>
        /// <returns></returns>
        public ActionResult CreateAnalisisCasoSaludP(int id_concurrencia, int? opc, int? idanalisis)
        {
            Models.analisis_de_caso.AnalisisSaludPublica Model = new Models.analisis_de_caso.AnalisisSaludPublica();

            if (idanalisis != null)
            {
                analisis_caso_salud_publica obj = Model.ConsultaEvolucionAnalisisSaludP(id_concurrencia, idanalisis).FirstOrDefault();
                if (obj != null)
                {
                    Model = Model.SetearvaloresAnalisis(obj);
                }
            }

            if (opc == null)
                opc = 1;

            Model.opc = opc.Value;
            ViewData["Alerta"] = "";
            ViewBag.usuario = SesionVar.ROL;

            return View(Model);
        }

        /// <summary>
        /// Pantalla inicial, crear analisis de casos de eventos en salud
        /// </summary>
        /// <param name="id_concurrencia"></param>
        /// <param name="opc"></param>
        /// <returns></returns>
        public ActionResult CreateAnalisisEventosenSalud(int id_concurrencia, int? opc, int? idanalisis)
        {
            Models.Evolucion.Eventos_en_salud Model = new Models.Evolucion.Eventos_en_salud();
            Session["OtroSiList"] = new List<ecop_concurrencia_eventos_en_salud_detalle>();

            List<ecop_concurrencia_eventos_en_salud_detalle> lista = new List<ecop_concurrencia_eventos_en_salud_detalle>();
            ViewBag.valuepro = "";

            ViewData["cant_otrossi"] = 0;

            if (opc == null)
            {
                opc = 1;
            }
            Model.opc = opc.Value;
            ViewData["Alerta"] = "";
            ViewBag.usuario = SesionVar.ROL;

            if (idanalisis != null)
            {
                ecop_concurrencia_eventos_en_asalud obj = Model.ConsultaAnalisisEventosenSalud(id_concurrencia, idanalisis).FirstOrDefault();
                if (obj != null)
                {
                    Model = Model.SetearvaloresAnalisis(obj);
                    lista = Model.GetAnalisisEventosenSaludDetalle(Convert.ToInt32(idanalisis));
                    ViewData["cant_otrossi"] = lista.Count();
                    ViewBag.listadoOtroSi = lista;
                    Session["OtroSiList"] = lista;

                    if (opc == null)
                    {
                        opc = 1;
                    }
                    Model.opc = opc.Value;
                    ViewData["Alerta"] = "";
                    ViewBag.usuario = SesionVar.ROL;

                    return View(Model);

                }
            }
            else
            {
                ecop_concurrencia_eventos_en_asalud cont = new ecop_concurrencia_eventos_en_asalud();
                ViewData["cant_otrossi"] = lista.Count();
                ViewBag.listadoOtroSi = lista;
            }

      



            return View(Model);
        }

        [HttpPost]
        public ActionResult CreateAnalisisAlertas(Models.analisis_de_caso.AnalisisOriginal Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_revisionOK == null)
            {
                if (Model.fecha_revision == null)
                {
                    variable = "ERROR";
                    variable2 = "INGRESAR FECHA REVISION";
                    Conteo = Conteo + 1;
                }

            }

            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {
                analisis_caso_alertas AnalisisA = new analisis_caso_alertas();
                AnalisisA.id_concurrencia = Model.id_concurrencia;
                AnalisisA.id_analisis_caso_alertas = Model.id_analisis_caso_alertas;
                AnalisisA.id_regional = Model.id_regional;
                AnalisisA.id_ciudad = Model.ciudad;
                AnalisisA.id_ips = Model.id_ips;
                AnalisisA.tipo_documento = Model.tipo_documento;
                AnalisisA.numero_identificacion = Model.numero_identificacion;
                AnalisisA.nombres_completos = Model.nombres_apellidos_pacientes;
                AnalisisA.edad = Model.edad;
                AnalisisA.sexo = Model.sexo;
                AnalisisA.nombre_mega = Model.nombre_del_mega;
                if (Model.fecha_revisionOK == null)
                {
                    AnalisisA.fecha_revision = Model.fecha_revision;
                }
                else
                {
                    AnalisisA.fecha_revision = Model.fecha_revisionOK;
                }
                AnalisisA.tipo_evento = Model.tipo_evento_alerta;
                AnalisisA.nombre_alerta = Model.nombre_alerta;
                AnalisisA.descripcion_alerta = Model.descripcion_alerta;
                AnalisisA.fecha_registro = Model.fecha_registro;
                AnalisisA.diagnostico_ingreso = Model.cie10_ingreso;
                AnalisisA.diagnostico_egreso = Model.cie10_egreso;
                AnalisisA.resumen_caso = Model.resumen_caso;
                AnalisisA.analisis_caso = Model.analisis_caso;
                AnalisisA.fuente_informacion = Model.fuente_informacion;
                AnalisisA.eventual_causa_evento = Model.eventuales_causas_evento;
                AnalisisA.fallos_calidad = Model.fallos_calidad;
                AnalisisA.alerta_evitable = Model.alerta_evitable;
                AnalisisA.conclusiones = Model.conclusiones;
                AnalisisA.recomendaciones = Model.recomendaciones;
                AnalisisA.plan_mejora = Model.plan_mejora;


                if (Model.id_analisis_caso_alertas == 0)
                {
                    AnalisisA.digita_usuario = SesionVar.UserName;
                    AnalisisA.fecha_digita = DateTime.Now;
                    Int32 IdAnalisis = Model.InsertarAnalisisCasoAlerta(AnalisisA);
                    ViewData["Alerta"] = Cgeneral.MsgRespuesta("success", "Transaccion Exitosa", "Analisis Guardado exitosamente.");

                }
                else
                {
                    Model.ActualizarAnalisisAlertas(AnalisisA);
                    ViewData["Alerta"] = Cgeneral.MsgRespuesta("success", "Transaccion Exitosa", "Analisis Actualizado exitosamente.");
                }
            }
            else
            {
                ViewData["Alerta"] = Cgeneral.MsgRespuesta("danger", "Error!", "DEBE INGRESAR TODOS LOS CAMPOS");
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            if (Model.opc == 1)
            {
                return View(Model);
            }
            else
            {
                return RedirectToAction("TableroControl", "AnalisisCaso");
            }

        }

        [HttpPost]
        public ActionResult CreateAnalisisMuestreo(Models.analisis_de_caso.AnalisisMuestreo Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_revisionOK == null)
            {
                if (Model.fecha_revision == null)
                {
                    variable = "ERROR";
                    variable2 = "INGRESAR FECHA REVISION";
                    Conteo = Conteo + 1;
                }

            }

            if (Model.fecha_ultimo_controlOK == null)
            {
                if (Model.fecha_ultimo_control == null)
                {
                    variable = "ERROR";
                    variable2 = "INGRESAR FECHA ULTIMO CONTROL";
                    Conteo = Conteo + 1;
                }

            }

            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }


            if (variable != "ERROR")
            {
                analisis_caso_muestreo AnalisisM = new analisis_caso_muestreo();
                AnalisisM.id_concurrencia = Model.id_concurrencia;
                AnalisisM.id_analisis_caso_muestreo = Model.id_analisis_caso_muestreo;
                AnalisisM.numero_casos = Model.numero_casos;
                AnalisisM.regional_origen = Model.id_regional;
                AnalisisM.cohorte_pertenece = Model.cohorte_paciente;
                AnalisisM.antecedentes_personales = Model.antecedentes_personales;
                AnalisisM.descripcion_atenciones_ambulatorias = Model.descripciones_atenciones_ambulatorias;
                AnalisisM.numero_controles = Model.numero_controles;
                AnalisisM.plan_manejo_definido_ultimo_control = Model.plan_manejo_ultimo_control;
                AnalisisM.concepto_auditoria_ambulatoria = Model.concepto_auditoria_ambulatoria;
                AnalisisM.conclusion_caso_ambulatorio = Model.conclusion_caso_ambulatorio;
                AnalisisM.adherencia_del_paciente = Model.adherencia_paciente;
                AnalisisM.plan_mejora_ppe = Model.plan_de_mejora_ppe;
                AnalisisM.recomendaciones_ambulatorio = Model.recomendaciones_ambulatoria;
                AnalisisM.id_ips = Convert.ToInt32(Model.id_ips);
                AnalisisM.tipo_documento = Model.tipo_documento;
                AnalisisM.numero_documento = Model.numero_identificacion;
                AnalisisM.nombres_paciente = Model.nombres_apellidos_pacientes;
                AnalisisM.edad = Model.edad;
                AnalisisM.sexo = Model.sexo;
                AnalisisM.nombre_mega = Model.nombre_del_mega;
                if (Model.fecha_revisionOK == null)
                {
                    AnalisisM.fecha_revision = Model.fecha_revision;
                }
                else
                {
                    AnalisisM.fecha_revision = Model.fecha_revisionOK;
                }
                if (Model.fecha_ultimo_controlOK == null)
                {
                    AnalisisM.fecha_ultimo_control = Model.fecha_ultimo_control;
                }
                else
                {
                    AnalisisM.fecha_ultimo_control = Model.fecha_ultimo_controlOK;
                }
                if (Model.fecha_ingresoOK == null)
                {
                    AnalisisM.fecha_ingreso = Model.fecha_ingreso;
                }
                else
                {
                    AnalisisM.fecha_ingreso = Model.fecha_ingresoOK;
                }

                if (Model.fecha_egresoOK == null)
                {
                    AnalisisM.fecha_egreso = Model.fecha_egreso;
                }
                else
                {
                    AnalisisM.fecha_egreso = Model.fecha_egresoOK;
                }
                AnalisisM.diagnostico_ingreso = Model.cie10_ingreso;
                AnalisisM.diagnostico_egreso = Model.cie10_egreso;
                AnalisisM.auditor_asalud = Model.auditor_asalud;
                AnalisisM.diagnostico_ingreso = Model.cie10_ingreso;
                AnalisisM.diagnostico_egreso = Model.cie10_egreso;
                AnalisisM.descripcion_hospitalizacion = Model.descripcion_hospitalizacion;
                AnalisisM.concepto_auditoria = Model.concepto_auditoria;
                AnalisisM.conclusion_caso_hospitalario = Model.conclusion_caso_hospitalario;
                AnalisisM.hospitalizacion_evitable = Model.hospitalizacion_evitable;
                AnalisisM.recomendaciones_hospitalario = Model.recomendaciones_hospitalaraio;
                AnalisisM.definicion_integral_caso = Model.definicion_integral_caso;


                if (Model.id_analisis_caso_muestreo == 0)
                {
                    AnalisisM.usuario_digita = SesionVar.UserName;
                    AnalisisM.fecha_digita = DateTime.Now;
                    int id = Model.InsertarAnalisisMuestreo(AnalisisM);
                    ViewData["Alerta"] = Cgeneral.MsgRespuesta("success", "Transaccion Exitosa", "Analisis Guardado exitosamente.");

                }
                else
                {
                    Model.ActualizarAnalisisMuestreo(AnalisisM);
                    ViewData["Alerta"] = Cgeneral.MsgRespuesta("success", "Transaccion Exitosa", "Analisis Actualizado exitosamente.");
                }
            }
            else
            {
                ViewData["Alerta"] = Cgeneral.MsgRespuesta("danger", "Error!", "DEBE INGRESAR TODOS LOS CAMPOS");
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            if (Model.opc == 1)
            {
                return View(Model);
            }
            else
            {
                return RedirectToAction("TableroControl", "AnalisisCaso");
            }

        }

        [HttpPost]
        public ActionResult TableroControl(Models.analisis_de_caso.tablerocontrol Model)
        {
            return View(Model);
        }

        /// <summary>
        /// Metodo post crear analisis de caso original
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAnalisisCaso(Models.analisis_de_caso.AnalisisOriginal Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_analisisOK == null)
            {
                variable = "ERROR";
                variable2 = "INGRESAR FECHA ANALISIS";
                Conteo = Conteo + 1;
            }
            if (Model.fecha_inicio_eventoOK == null)
            {
                variable = "ERROR";
                variable2 = "INGRESAR FECHA INICIO EVENTO";
                Conteo = Conteo + 1;
            }
            if (Model.fecha_fin_eventoOK == null)
            {
                variable = "ERROR";
                variable2 = "INGRESAR FECHA FIN EVENTO";
                Conteo = Conteo + 1;
            }
            if (Model.id_ips == 0)
            {
                variable = "ERROR";
                variable2 = "INGRESAR IPS";
                Conteo = Conteo + 1;
            }
            if (Model.id_regional == 0)
            {
                variable = "ERROR";
                variable2 = "INGRESAR  REGIONAL";
                Conteo = Conteo + 1;
            }

            if (Model.cie101 == null)
            {
                variable = "ERROR";
                variable2 = "INGRESAR  cie10";
                Conteo = Conteo + 1;
            }



            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {
                analisis_caso_original Analisis = new analisis_caso_original();
                Analisis.id_concurrencia = Model.id_concurrencia;
                Analisis.id_analisis_caso_original = Model.id_analisis_caso_original;
                Analisis.fecha_analisis = Model.fecha_analisisOK;
                Analisis.solicitud = Model.solicitud;
                Analisis.id_regional = Model.id_regional;
                Analisis.tipo_evento = Model.tipo_evento;
                Analisis.fecha_inicio_evento = Model.fecha_fin_eventoOK;
                Analisis.fecha_fin_evento = Model.fecha_fin_eventoOK;
                Analisis.id_ips = Model.id_ips;
                Analisis.prestadores_intervinientes = Model.prestadores_intervinientes;
                Analisis.objeto_alcance_actividad = Model.objeto_alcance_actividad;
                Analisis.marco_legal = Model.marco_legal;
                Analisis.cie101 = Model.cie101;
                Analisis.cie102 = Model.cie102;
                Analisis.cie103 = Model.cie103;
                Analisis.resumen_secuencial_caso = Model.resumen_secuencial_caso;
                Analisis.analisis_clinico_caso = Model.analisis_clinico_caso;
                Analisis.aplicacion_metodologia = Model.aplicacion_metodologia;
                Analisis.eventuales_causas = Model.eventuales_causas;
                Analisis.eventuales_fallas_control = Model.eventuales_fallas_control;
                Analisis.eventuales_fallas_calidad = Model.eventuales_fallas_calidad;
                Analisis.fuentes_info = Model.fuentes_info;
                Analisis.conclucion_analisis = Model.conclucion_analisis;
                Analisis.prevenible_atribuible = Model.prevenible_atribuible;
                Analisis.costo_no_calidad = Model.costo_no_calidad;
                Analisis.hallazgos_legal = Model.hallazgos_legal;
                Analisis.cumplimientos_indicadores = Model.cumplimientos_indicadores;
                Analisis.patologias_eventos_centinelas = Model.patologias_eventos_centinelas;
                Analisis.pertinencia_acciones = Model.pertinencia_acciones;
                Analisis.eficacia_acciones = Model.eficacia_acciones;
                Analisis.recomendaciones_mejora = Model.recomendaciones_mejora;
                Analisis.compromiso_mejora = Model.compromiso_mejora;
                Analisis.evaluacion_impacto = Model.evaluacion_impacto;

                if (Model.id_analisis_caso_original == 0)
                {
                    Analisis.usuario_digita = SesionVar.UserName;
                    Analisis.fecha_digita = DateTime.Now;
                    int id = Model.InsertarAnalisisCasoOriginal(Analisis);
                    ViewData["Alerta"] = Cgeneral.MsgRespuesta("success", "Transaccion Exitosa", "Analisis Guardado exitosamente.");

                }
                else
                {
                    Model.ActualizarAnalisisCasoOriginal(Analisis);
                    ViewData["Alerta"] = Cgeneral.MsgRespuesta("success", "Transaccion Exitosa", "Analisis Actualizado exitosamente.");
                }
            }
            else
            {
                ViewData["Alerta"] = Cgeneral.MsgRespuesta("danger", "Error!", "DEBE INGRESAR TODOS LOS CAMPOS");
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            if (Model.opc == 1)
            {
                return View(Model);

            }
            else
            {
                return RedirectToAction("TableroControl", "AnalisisCaso");
            }

        }

        /// <summary>
        /// Metodo post crear analisis de casos en salud publica
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAnalisisCasoSaludP(Models.analisis_de_caso.AnalisisSaludPublica Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";


            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {
                analisis_caso_salud_publica analisis = new analisis_caso_salud_publica();
                analisis.id_concurrencia = Model.id_concurrencia;
                analisis.id_analisis_caso_salud_publica = Model.id_analisis_caso_salud_publica;
                analisis.fecha_del_analisis = Model.fecha_del_analisisok;
                analisis.edad_momento_analisis = Model.edad_momento_analisis;
                analisis.tipo_evento = Model.tipo_evento;
                analisis.fecha_ocurrencia_evento = Model.fecha_ocurrencia_eventoOK;
                analisis.fuente_del_reporte = Model.fuente_del_reporte;
                analisis.nombre_reportante = Model.nombre_reportante;
                analisis.id_ips = Model.id_ips;
                analisis.entidades_responsables = Model.entidades_responsables;
                analisis.objetivo_auditoria = Model.objetivo_auditoria;
                analisis.descripcion_evento = Model.descripcion_evento;
                analisis.id_ref_ambito_evento = Model.id_ref_ambito_evento;
                analisis.Resumen_clinico_evento = Model.Resumen_clinico_evento;
                analisis.concepto_primer_nivel = Model.concepto_primer_nivel;
                analisis.guias_terapeuticas = Model.guias_terapeuticas;
                analisis.periocidad_controles = Model.periocidad_controles;
                analisis.pruebas_diagnosticas = Model.pruebas_diagnosticas;
                analisis.plan_terapeutico = Model.plan_terapeutico;
                analisis.eventuales_causas_muerte = Model.eventuales_causas_muerte;
                analisis.concepto_auditoria = Model.concepto_auditoria;
                analisis.propuesta_acciones = Model.propuesta_acciones;
                analisis.relacion_anexos = Model.relacion_anexos;
                analisis.firmas = Model.firmas;
                analisis.usuario_digita = SesionVar.UserName;
                analisis.fecha_digita = DateTime.Now;

                /*si en el modelo, este id es igual a 0, es porque se creara un nuevo registro*/
                if (Model.id_analisis_caso_salud_publica == 0)
                {
                    Int32 IdAnalisis = Convert.ToInt32(Model.InsertarAnalisisCasoSaludPublica(analisis));
                    ViewData["Alerta"] = Cgeneral.MsgRespuesta("success", "Transaccion Exitosa", "Analisis Guardado exitosamente.");
                }
                else
                {
                    Model.ActualizarAnalisisCasoSaludPublica(analisis);
                    ViewData["Alerta"] = Cgeneral.MsgRespuesta("success", "Transaccion Exitosa", "Analisis Actualizado exitosamente.");
                }

                //Model = new Models.analisis_de_caso.AnalisisSaludPublica();

            }
            else
            {
                ViewData["Alerta"] = Cgeneral.MsgRespuesta("danger", "Error!", "DEBE INGRESAR TODOS LOS CAMPOS");
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            if (Model.opc == 1)
            {
                return View(Model);
            }
            else
            {
                return RedirectToAction("TableroControl", "AnalisisCaso");
            }

        }

        /// <summary>
        /// Metodo post crear analisis en eventos en salud
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAnalisisEventosenSalud(Models.Evolucion.Eventos_en_salud Model)
        {
            List<ecop_concurrencia_eventos_en_salud_detalle> ListaOtroSi = (List<ecop_concurrencia_eventos_en_salud_detalle>)Session["OtroSiList"];



            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            ViewData["cant_otrossi"] = 0;

            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";
                variable2 = "DEBE INGRESAR ALGUN DETALLE DE PLAN DE ACCION";

            }



            if (variable != "ERROR")
            {
                ecop_concurrencia_eventos_en_asalud analisis = new ecop_concurrencia_eventos_en_asalud();

                analisis.id_ecop_concurrencia_eventos_en_asalud = Model.id_ecop_concurrencia_eventos_en_asalud;
                analisis.id_ref_evento_adv = Model.id_ref_evento_adv;
                analisis.id_concurrencia = Model.id_concurrencia;
                analisis.fecha_analisis = Model.fecha_analisisok;
                analisis.examenes_hallazgos = Model.examenes_hallazgos;
                analisis.cie10_antes_del_evento = Model.cie10_antes_del_evento;
                analisis.cie10_resultado_evento = Model.cie10_resultado_evento;
                analisis.conducta_inmediata = Model.conducta_inmediata;
                analisis.id_ref_evento_adv = Model.id_ref_evento_adv;
                analisis.acciones_inseguras = Model.acciones_inseguras;
                analisis.id_decisiones = Model.id_decisiones;
                analisis.id_habilidad = Model.id_habilidad;
                analisis.id_percepcion = Model.id_percepcion;
                analisis.id_rutinaria = Model.id_rutinaria;
                analisis.id_excepcionales = Model.id_excepcionales;
                analisis.otros_general_acciones_inseguras = Model.otro_general_accion_inseguras;
                analisis.factores_contributivos = Model.factores_contributivos;
                analisis.id_paciente = Model.id_paciente;
                analisis.id_tarea_tecnologia = Model.id_tarea_tecnologia;
                analisis.id_equipo_trabajo = Model.id_equipo_trabajo;
                analisis.id_individuo = Model.id_individuo;
                analisis.id_ambiente_trabajo = Model.id_ambiente_trabajo;
                analisis.id_organizacion_gerencia = Model.id_organizacion_gerencia;
                analisis.id_contexto = Model.id_contexto;
                analisis.otros_general_factores_contibutivos = Model.otro_general_factores_contributivos;
                analisis.falla_activa_final = Model.falla_activa_final;
                analisis.probabilidad_repeticion = Model.probabilidad_repeticion;
                analisis.id_tipo_evento = Model.id_tipo_evento;
                analisis.prevenible = Model.prevenible;
                analisis.concluciones = Model.concluciones;
                analisis.seguimiento_auditoria = Model.seguimiento_auditoria;
                analisis.equipo_analisis = Model.equipo_analisis;
                analisis.id_severidad = Model.id_severidad;

                if (Model.opc == 1)
                {
                    analisis.usuario_digita = SesionVar.UserName;
                    analisis.fecha_digita = DateTime.Now;
                    Model.InsertarAnalisisEventosSalud(analisis , ListaOtroSi);

  
                    ViewData["Alerta"] = Cgeneral.MsgRespuesta("success", "Transaccion Exitosa", "Analisis Guardado exitosamente");
                }
                else
                {
                    Model.ActualizarAnalisisEventosSalud(analisis);
                    foreach (var item in ListaOtroSi)
                    {
                        if (item.id_ecop_concurrencia_eventos_en_salud_detalle == 0)
                        {   
                            Model.InsertarAnalisisCasosEventosDet(analisis, ListaOtroSi);
                        }

                    }


                }

            }
            else
            {
                ViewData["Alerta"] = Cgeneral.MsgRespuesta("danger", "Error!", "DEBE INGRESAR TODOS LOS CAMPOS");
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            if (Model.opc == 1)
            {
                return RedirectToAction("TableroControl", "AnalisisCaso");
            }
            else
            {
                return RedirectToAction("TableroControl", "AnalisisCaso");
            }


        }

        /// <summary>
        /// Obtiene las concurrrencias a partir de un numero de documento y crea la tabla tipos de analisis
        /// </summary>
        /// <param name="numdocumento"></param>
        /// <returns></returns>
        public string ObtenerConcurrencias(String numdocumento)
        {
            string Result = "<tr class='common' align='center' style='height:50px;'><td colspan='5'>No se han encontrado registros.</td></tr>";

            Models.Concurrencia.Concurrencia Model = new Models.Concurrencia.Concurrencia();
            var ListaConcurrencias = Model.GetConcurenciasByDocumento(numdocumento);
            if (ListaConcurrencias.Count() > 0)
            {
                Result = "";
                foreach (var item in ListaConcurrencias)
                {
                    Result += "<tr><td>" + item.id_concurrencia + "</td>";
                    Result += "<td>" + item.afi_nom + "</td>";
                    Result += "<td>" + item.fecha_ingreso.Value.ToString("dd/MM/yyyy") + "</td>";
                    Result += "<td><div class='dropdown'>"
                           
                           + "<a class='dropdown-toggle rowlink btn button_Asalud_Aceptar' role='button' data-toggle='dropdown' href=''>Crear Analisis de casos</a>"
                           + "<ul class='dropdown-menu' role='menu' aria-labelledby='dLabel'>"
                           //+ "<li><a href='" + Url.Action("CreateAnalisisAlertas", "AnalisisCaso", new { id_concurrencia = item.id_concurrencia, opc = 1 }) + "' title='Crear Analisis Alertas' class='ion-trash-a'>Alertas</a></li>"
                           + "<li><a href='" + Url.Action("CreateAnalisisCasoSaludP", "AnalisisCaso", new { id_concurrencia = item.id_concurrencia, opc = 1 }) + "' title='Crear Analisis Salud Publica' class='ion-trash-a'>Salud Publica</a></li>"
                           + "<li><a href='" + Url.Action("CreateAnalisisEventosenSalud", "AnalisisCaso", new { id_concurrencia = item.id_concurrencia, opc = 1 }) + "' title='Crear Analisis Eventos en Salud' class='ion-trash-a'>Eventos en salud</a></li>"
                           //+ "<li><a href='" + Url.Action("CreateAnalisisMuestreo", "AnalisisCaso", new { id_concurrencia = item.id_concurrencia, opc = 1 }) + "' title='Crear Analisis Muestreo' class='ion-trash-a'>Muestreo</a></li>"

                           + "</ul></div>"
                        + "</td></tr>";
                }
            }
            else
            {

                Result += "<td><div class='dropdown'>"
                       + "<a class='dropdown-toggle rowlink btn button_Asalud_Aceptar' role='button' data-toggle='dropdown' href=''>Crear Analisis de caso sin concurrencia</a>"
                       + "<ul class='dropdown-menu' role='menu' aria-labelledby='dLabel'>"
                       //+ "<li><a href='" + Url.Action("CreateAnalisisAlertas", "AnalisisCaso", new { id_concurrencia = 0, numdocumento = numdocumento, opc = 1 }) + "' title='Crear Analisis Alertas' class='ion-trash-a'>Alertas</a></li>"
                       + "<li><a href='" + Url.Action("CreateAnalisisCasoSaludP", "AnalisisCaso", new { id_concurrencia = 0, numdocumento = numdocumento, opc = 1 }) + "' title='Crear Analisis Salud Publica' class='ion-trash-a'>Salud Publica</a></li>"
                       + "<li><a href='" + Url.Action("CreateAnalisisEventosenSalud", "AnalisisCaso", new { id_concurrencia = 0, numdocumento = numdocumento, opc = 1 }) + "' title='Crear Analisis Eventos en Salud' class='ion-trash-a'>Eventos en salud</a></li>"
                       //+ "<li><a href='" + Url.Action("CreateAnalisisMuestreo", "AnalisisCaso", new { id_concurrencia = 0, numdocumento = numdocumento, opc = 1 }) + "' title='Crear Analisis Muestreo' class='ion-trash-a'>Muestreo</a></li>"

                       + "</ul></div>"
                    + "</td></tr>";
            }

            return Result;
        }

        /// <summary>
        /// Crear reporte en pdf. analisis de caso original
        /// </summary>
        /// <param name="idConcurrencia"></param>
        public void CrearPdfCartaAnalisisCasoSaludPublica(Int32 idConcurrencia, Int32 idanalisis)
        {

            //RUTA REPORTE
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptAnalisisCasoSaludP.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.analisis_de_caso.AnalisisSaludPublica Model = new Models.analisis_de_caso.AnalisisSaludPublica();

            List<ManagmentReporteAnalisisCasoSPResult> Result = new List<ManagmentReporteAnalisisCasoSPResult>();

            Result = Model.ReporteAnalisisCasoSP(idConcurrencia, idanalisis).ToList();

            string filename = "AnalisisCaso_SP_" + Result.FirstOrDefault().id_analisis_caso_salud_publica;
            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("AnalisisCasoSPDataSet", Result);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (Result != null)
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
                    byte[] pdfContent = viewer.LocalReport.Render("PDF");

                    //RETORNO PDF

                    this.Response.Clear();
                    this.Response.ContentType = "application/pdf";
                    this.Response.AddHeader("Content-disposition", "attachment; filename=" + filename + ".pdf");
                    this.Response.BinaryWrite(pdfContent);
                    this.Response.End();


                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

        }

        public void CrearPdfCartaAnalisisCasoOriginal(Int32 idConcurrencia, Int32 idanalisis)
        {

            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptAnalisisCasoOriginal.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.analisis_de_caso.AnalisisOriginal Model = new Models.analisis_de_caso.AnalisisOriginal();

            List<ManagmentReporteAnalisisCasoOrgResult> Result = new List<ManagmentReporteAnalisisCasoOrgResult>();

            Result = Model.ReporteAnalisisCasoOriginal(idConcurrencia, idanalisis).ToList();

            string filename = "AnalisisCaso_Org_" + Result.FirstOrDefault().id_analisis_caso_original;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("AnalisisCasoOrgDataSet", Result);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (Result != null)
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
                    byte[] pdfContent = viewer.LocalReport.Render("PDF");

                    //RETORNO PDF

                    this.Response.Clear();
                    this.Response.ContentType = "application/pdf";
                    this.Response.AddHeader("Content-disposition", "attachment; filename=" + filename + ".pdf");
                    this.Response.BinaryWrite(pdfContent);
                    this.Response.End();


                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

        }

        public void CrearPdfCartaAnalisisCasoEventoSalud(Int32 idConcurrencia, Int32 idanalisis)
        {

            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptAnalisisCasoES.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.Evolucion.Eventos_en_salud Model = new Models.Evolucion.Eventos_en_salud();

            List<ManagmentReporteAnalisisESResult> Result = new List<ManagmentReporteAnalisisESResult>();

            Result = Model.ReporteEventosSalud(idConcurrencia, idanalisis).ToList();

            string filename = "AnalisisCaso_EvntS_" + Result.FirstOrDefault().id_ecop_concurrencia_eventos_en_asalud;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("AnalisisCasosESDataSet", Result);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (Result != null)
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
                    byte[] pdfContent = viewer.LocalReport.Render("PDF");

                    //RETORNO PDF

                    this.Response.Clear();
                    this.Response.ContentType = "application/pdf";
                    this.Response.AddHeader("Content-disposition", "attachment; filename=" + filename + ".pdf");
                    this.Response.BinaryWrite(pdfContent);
                    this.Response.End();


                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

        }

        public JsonResult CerrarAnalisis(int idanalisis, int idtipoanalisis, string observacion)
        {
            Models.analisis_de_caso.tablerocontrol Model = new Models.analisis_de_caso.tablerocontrol();

            GestionAnalisisDeCasos obj = new GestionAnalisisDeCasos();

            var objeto = Model.GetAnalisisGestion(idtipoanalisis, idanalisis);
            if (objeto != null)
            {
                obj = objeto;

            }
            obj.id_tipo_analisis_caso = idtipoanalisis;
            if (idtipoanalisis == 1)
                obj.nom_tipo_analisiscaso = "Analisis Caso Original";
            if (idtipoanalisis == 2)
                obj.nom_tipo_analisiscaso = "Analisis Caso Salud Publica";
            if (idtipoanalisis == 3)
                obj.nom_tipo_analisiscaso = "Analisis Caso Eventos en Salud";
            if (idtipoanalisis == 4)
                obj.nom_tipo_analisiscaso = "Analisis Caso Alertas";
            if (idtipoanalisis == 5)
                obj.nom_tipo_analisiscaso = "Analisis Caso Muestreo";

            obj.id_analisis_caso = idanalisis;
            obj.observacion = observacion;
            obj.usuario_digita = SesionVar.UserName;
            obj.fecha_digita = DateTime.Now;
            obj.estado_caso = 1;

            if (objeto != null)
            {
                Model.Actualizargestionanalisisdecaso(obj);
            }
            else
            {
                Model.Insertargestionanalisisdecaso(obj);
            }


            return Json(0);
        }

        public JsonResult GestionarEstadoCaso(int opcionrealizar, Int32 idconcurrencia, Int32 idtipoanalisis, Int32 idanalisiscaso, string justificacion)
        {
            Models.analisis_de_caso.tablerocontrol Model = new Models.analisis_de_caso.tablerocontrol();
            GestionAnalisisDeCasos caso = Model.GetAnalisisGestion(idtipoanalisis, idanalisiscaso);
            if (opcionrealizar == 1)
            {
                // estado dos significa aprobado
                caso.estado_caso = 2;
            }
            else
            {
                // estado tres significa rechazado
                caso.estado_caso = 3;
                caso.justificacion_rechazo = justificacion;
            }

            Model.Actualizargestionanalisisdecaso(caso);
            var url = "";
            if (idtipoanalisis == 1)
                url = "/AnalisisCaso/CrearPdfCartaAnalisisCasoOriginal?idConcurrencia=" + idconcurrencia + "&idanalisis=" + caso.id_analisis_caso;
            if (idtipoanalisis == 2)
                url = "/AnalisisCaso/CrearPdfCartaAnalisisCasoSaludPublica?idConcurrencia=" + idconcurrencia + "&idanalisis=" + caso.id_analisis_caso;
            if (idtipoanalisis == 3)
                url = "/AnalisisCaso/CrearPdfCartaAnalisisCasoEventoSalud?idConcurrencia=" + idconcurrencia + "&idanalisis=" + caso.id_analisis_caso;
            if (idtipoanalisis == 4)
            {
                // url = "/AnalisisCaso/CrearPdfCartaAnalisisCasoEventoSalud?idConcurrencia=" + idconcurrencia + "&idanalisis=" + caso.id_analisis_caso;
            }
            if (idtipoanalisis == 5)
            {
                // url = "/AnalisisCaso/CrearPdfCartaAnalisisCasoEventoSalud?idConcurrencia=" + idconcurrencia + "&idanalisis=" + caso.id_analisis_caso;
            }

            return Json(new { url, opcionrealizar }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCascadeIps(Models.analisis_de_caso.AnalisisMuestreo Model)
        {
            return Json(Model.ListIPS.Select(c => new { id_ips = c.id_ref_ips, nombre_ips = c.Nombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeCie10(Models.analisis_de_caso.AnalisisMuestreo Model)
        {
            return Json(Model.ListCie10.Select(c => new { id_cie10 = c.id_cie10, nombre_cie10 = c.des }), JsonRequestBehavior.AllowGet);
        }

        public string Agregarotrosi(string seguimiento, string fechacumplimiento, string accionmejora, string responsable)
        {
            string result = "";
            List<ecop_concurrencia_eventos_en_salud_detalle> Agregadas = (List<ecop_concurrencia_eventos_en_salud_detalle>)Session["OtroSiList"];

            ecop_concurrencia_eventos_en_salud_detalle NewOtroSi = new ecop_concurrencia_eventos_en_salud_detalle();
            NewOtroSi.fecha_cumplimieto = fechacumplimiento;
            NewOtroSi.seguimiento = seguimiento;
            NewOtroSi.responsable = responsable;
            NewOtroSi.accion_mejora = accionmejora;
            Agregadas.Add(NewOtroSi);

            //result += "<tbody>";
            int i = 0;
            foreach (ecop_concurrencia_eventos_en_salud_detalle a in Agregadas)
            {
                i += 1;

                result += "<tr>"
                    + "<td>" + i + "</td>"
                    + "<td>" + a.accion_mejora + "</td>";

                result += "<td>" + a.responsable + "</td>";

                result += "<td>" + a.fecha_cumplimieto + "</td>"
                    + "<td>" + a.seguimiento + "</td>"
                    + "<td class='text-center'><a title='Remover Otro Si' href='javascript:removerotrosi(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                result += "</tr>";

            }
            //result += "</tbody>";

            return result;
        }

        public string RemoverOtroSi(int? posicion,int? iddetalle)
        {
            if (iddetalle != 0 && iddetalle != null)
            {
                Models.Evolucion.Eventos_en_salud Model = new Models.Evolucion.Eventos_en_salud();
                Model.EliminarPlanAccion(iddetalle);
            }
     
            List<ecop_concurrencia_eventos_en_salud_detalle> otrosilist = (List<ecop_concurrencia_eventos_en_salud_detalle>)Session["OtroSiList"];  
            
            otrosilist.Remove(otrosilist[Convert.ToInt32(posicion)- 1]);
            Session["OtroSiList"] = otrosilist;

            string result = "";

            int i = 0;
            foreach (ecop_concurrencia_eventos_en_salud_detalle a in otrosilist)
            {
                i += 1;

                result += "<tr>"
                    + "<td>" + i + "</td>"
                    + "<td>" + a.accion_mejora + "</td>";

                result += "<td>" + a.responsable + "</td>";

                result += "<td>" + a.fecha_cumplimieto + "</td>"
                    + "<td>" + a.seguimiento + "</td>"
                    + "<td class='text-center'><a title='Remover Otro Si' href='javascript:removerotrosi(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                result += "</tr>";
            }

            return result;
        }

        public string ActualizarOtroSi(int? posicion, int? iddetalle)
        {
            if (iddetalle != 0 && iddetalle != null)
            {
                Models.Evolucion.Eventos_en_salud Model = new Models.Evolucion.Eventos_en_salud();
                Model.EliminarPlanAccion(iddetalle);
            }

            List<ecop_concurrencia_eventos_en_salud_detalle> otrosilist = (List<ecop_concurrencia_eventos_en_salud_detalle>)Session["OtroSiList"];

            otrosilist.Remove(otrosilist[Convert.ToInt32(posicion) - 1]);
            Session["OtroSiList"] = otrosilist;

            string result = "";

            int i = 0;
            foreach (ecop_concurrencia_eventos_en_salud_detalle a in otrosilist)
            {
                i += 1;

                result += "<tr>"
                    + "<td>" + i + "</td>"
                    + "<td>" + a.accion_mejora + "</td>";

                result += "<td>" + a.responsable + "</td>";

                result += "<td>" + a.fecha_cumplimieto + "</td>"
                    + "<td>" + a.seguimiento + "</td>"
                    + "<td class='text-center'><a title='Remover Otro Si' href='javascript:removerotrosi(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                result += "</tr>";
            }

            return result;
        }



    }

}