using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Text;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Web.UI;

using System.Security.Cryptography;
using iTextSharp.text.pdf;
using System.Web;
using Kendo.Mvc.UI;
using System.Web.Script.Serialization;
using System.Net.Configuration;

namespace AsaludEcopetrol.Controllers.Concurrencia
{
    [SessionExpireFilter]
    public class ConcurrenciaController : Controller
    {
        #region EVENTOS PRIVADOS

        private void EnvioCorreoDireccionProyectos(String tipodoc, String documento, String ips, String alto_costo)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                MailMessage msz = new MailMessage();
                msz.From = new MailAddress("direccionproyectoscm@asalud.co");//Email which you are getting
                msz.To.Add("direccionproyectoscm@asalud.co");//Where mail will be sent 
                msz.To.Add("coordinacionproyectos@asalud.co");//Where mail will be sent 
                msz.To.Add("richard.hernandez@ecopetrol.com.co");//Where mail will be sent 
                msz.To.Add("ibeth.karima@ecopetrol.com.co");//Where mail will be sent 
                msz.To.Add("maria.betancurt@ecopetrol.com.co");//Where mail will be sent 
                msz.To.Add("wilmer.andrade@Ecopetrol.com.co");//Where mail will be sent 


                msz.Subject = "[Alerta Alto Costo] PACIENTE HOSPITALIZADO DE SEGUIMIENTO PRIORITARIO";
                //msz.Body = vm.Name+"/n"+ vm.Message+ "/n"+ "Telefono De Contacto:"+ vm.Subject +"/n"+vm.Email;
                msz.Body = string.Format("SAMI Informa: {1}{0}  {2}{0} {3}{0} {4}{0}Tipo Documento:{5} {6}{0} Documento:{7} {8}{0} IPS:{9}  {10}{0} Descripción:{11}  {12}{0} {13}{0} {14}{0}", Environment.NewLine, string.Empty, string.Empty, "Respetado Equipo Ecopetrol, se genera alerta para seguimiento prioritario.", string.Empty, tipodoc, string.Empty, documento, string.Empty, ips, string.Empty, alto_costo, string.Empty, string.Empty, "Saludos Cordiales.");
                SmtpClient smtp = new SmtpClient();

                smtp.Host = smtpSection.Network.Host;
                smtp.Port = smtpSection.Network.Port;
                smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;
                smtp.Send(msz);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();
            }
        }

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

        public ActionResult CriterioIngreso(String idConcu)
        {
            Models.Concurrencia.Concurrencia Model = new Models.Concurrencia.Concurrencia();

            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.ConsultaIdConcurrenia(Convert.ToInt32(idConcu));
                Model.id_concurrencia = (Convert.ToInt32(idConcu));

                ViewBag.usuario = SesionVar.ROL;

                return View(Model);
            }

            else
            {
                return View();
            }
        }

        public ActionResult EgresoConcurrencia(String idConcu, String id_censo)
        {
            Models.Concurrencia.ConcurrenciaEgreso Model = new Models.Concurrencia.ConcurrenciaEgreso();
            List<Management_evolucion_procedimientosResult> listaPro = new List<Management_evolucion_procedimientosResult>();
            ecop_censo censo = new ecop_censo();
            var edad = 0;

            try
            {
                listaPro = Model.ConsultaProcedimientosConcurrencia(ref MsgRes);
                listaPro = listaPro.Where(x => x.id_concurrencia == Convert.ToInt32(idConcu)).ToList();

                ViewBag.Procedimietos = listaPro.ToList();

                Model.id_concurrencia = (Convert.ToInt32(idConcu));
                Model.id_censo = (Convert.ToInt32(id_censo));

                if (!string.IsNullOrEmpty(id_censo))
                {
                    censo = BusClass.ConsultaCensoIdCenso(Convert.ToInt32(id_censo));

                    if (censo != null)
                    {
                        DateTime thisDay = DateTime.Today;
                        TimeSpan age = (TimeSpan)(thisDay - censo.fecha_nacimiento);
                        var diasPasados = age.Days;
                        var añosPasados = (diasPasados / 365);
                        var diasReales = (diasPasados - (añosPasados / 4));
                        var edadFinal = (diasReales / 365);
                        edad = (int)edadFinal;
                    }
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.usuario = SesionVar.ROL;
            ViewBag.tipopatologiacatastrofica = Model.GetRefTipoPatologiaCatastrofica();
            ViewBag.tipohospitalizacion = Model.GetRefTipoHospitalizacion();
            ViewBag.estanciaprolongada = Model.GetRefCondicionProlongada();
            ViewBag.pertinenciaprolongada = Model.GetRefPertinenciaProlongada();
            Session["Categorizacion"] = new categorizacion_egreso_hospitalario();

            ViewBag.concu = idConcu;
            ViewBag.edad = edad;

            return View(Model);
        }

        public ActionResult EncuestaSatisfacionConcurrencia(String idConcu)
        {
            Models.Concurrencia.ConcurrenciaEncuesta Model = new Models.Concurrencia.ConcurrenciaEncuesta();

            Model.id_concurrencia = (Convert.ToInt32(idConcu));

            ViewBag.usuario = SesionVar.ROL;

            return View(Model);

        }

        [HttpGet]
        public ActionResult BuscarControlConcurrencia()
        {

            Models.Concurrencia.Concurrencia Model = new Models.Concurrencia.Concurrencia();
            try
            {
                var pdf = 0;

                if (Session["pdfCrear"] != null)
                {
                    pdf = (int)Session["pdfCrear"];
                }

                if (pdf != null && pdf != 0)
                {
                    ViewBag.js = pdf;
                    Session["pdfCrear"] = null;
                }
                else
                {
                    ViewBag.js = 0;
                }
                var lista = BusClass.GetTableroConcurrencia();
                ViewBag.lista = lista;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public ActionResult ActualizarTSH(String idConcu)
        {
            Models.Evolucion.Evolucion Model = new Models.Evolucion.Evolucion();


            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.id_concurrencia = Convert.ToInt32(idConcu);

                return View(Model);
            }

            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CriterioIngreso(Models.Concurrencia.Concurrencia Model)
        {
            Boolean bolValida = true;

            if (Model.lesiones_severas == false)
            {
                ModelState.Remove("id_lesiones_severas");
            }

            if (Model.lesiones_severas == true && Model.id_lesiones_severas == 0)
            {
                bolValida = false;
            }
            if (bolValida == true)
            {

                ecop_concurrencia objConcu = new ecop_concurrencia();
                objConcu.id_concurrencia = Model.id_concurrencia;
                objConcu.afi_tipo_doc = Model.afi_tipoDoc;
                objConcu.id_afi = Model.afi_NumDoc;
                objConcu.afi_nom = Model.afi_Nom;
                objConcu.afi_edad = Model.afi_Edad;
                objConcu.Genero = Model.genero;
                objConcu.afi_dir = Model.afi_Dir;
                objConcu.afi_tel = Model.afi_tel1;
                objConcu.afi_cel = Model.afi_cel;
                objConcu.afi_contacto_nom = Model.contacto_paciente;
                objConcu.afi_contacto_cel = Model.contacto_celular;
                objConcu.id_ips = Model.id_ips;
                objConcu.id_reingreso = Model.id_reingreso;
                objConcu.id_origen_hospitalizacion = Model.id_origen_hospitalizacion;
                if (Model.id_reingreso == 9)
                {
                    objConcu.otro_reingreso = Model.otro_reingreso;
                }
                else
                {

                }

                objConcu.servicio = Model.id_servicio_tratante;
                objConcu.med_ser_trata = Model.nombre_medico_tratante;

                objConcu.hospitalizacion_prevenible = Model.hospitalizacion_prevenible;

                if (Model.hospitalizacion_prevenible == "SI")
                {
                    objConcu.descripcion_prevenible = Model.descripcion_prevenible;
                }
                else
                {
                    objConcu.descripcion_prevenible = "NA";
                }

                if (Model.lesiones_severas == Convert.ToBoolean(true))
                {
                    objConcu.lesion_severa = "SI";
                    objConcu.id_lesion_severa = Model.id_lesiones_severas;
                }
                else
                {
                    objConcu.lesion_severa = "NO";
                    objConcu.id_lesion_severa = 0;
                }

                objConcu.reingreso = Model.reingreso;
                objConcu.dx1 = Model.id_cie10_1;
                objConcu.id_editor = SesionVar.IDUser;

                objConcu.Gestantes = Model.Gestantes;
                objConcu.Trabajador = Model.Trabajador;
                if (Model.Trabajador == "SI")
                {
                    objConcu.ciudad_trabajador = Model.ciudad_trabajador;
                }
                else
                {
                    objConcu.ciudad_trabajador = "0";
                }
                objConcu.triage = Model.triage;
                objConcu.auditoria_telefonica = Model.auditoria_telefonica;

                if (Model.salud_publica == Convert.ToBoolean(true))
                {
                    objConcu.salud_publica = "SI";
                    objConcu.id_salud_publica = Model.id_salud_publica;
                }
                else
                {
                    objConcu.salud_publica = "NO";
                    objConcu.id_salud_publica = 0;
                }

                if (Model.id_salud_publica == 10)
                {
                    objConcu.otro_salud_publica = Model.otroSalud;
                }
                else
                {
                    objConcu.otro_salud_publica = "";
                }

                Model.ActualizaConcurrencia(objConcu, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    if (Model.lesiones_severas == true)
                    {
                        if (Model.id_ciudadIPS == 1)
                        {
                            //  EnvioCorreoDireccionProyectos(Model.afi_tipoDoc, Model.afi_NumDoc, Model.Desips, Model.DesAlto);
                        }
                        else
                        {

                        }

                    }
                    return RedirectToAction("Inicio", "Usuario");
                }
                else
                {
                    ModelState.AddModelError("", "Error... Actualizado....");
                }

            }
            else
            {
                ModelState.AddModelError("", "Error.. Debe Ingresar la descripción de alto costo!");
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult EgresoConcurrencia(Models.Concurrencia.ConcurrenciaEgreso Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";
            List<Management_evolucion_procedimientosResult> listaPro = new List<Management_evolucion_procedimientosResult>();
            ViewBag.Procedimietos = listaPro.ToList();

            if (Model.CondicionAlta == "MUERTO")
            {
                if (Model.fecha_de_muerteok != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE LA MUERTE";
                    Conteo = Conteo + 1;
                }

                if (Model.diag_causa_directa_muerte != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "DIAGNOSTICO CAUSA DIRECTA DE LA MUERTE";
                    Conteo = Conteo + 1;
                }

                if (Model.diag_causa_basica_muerte != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "DIAGNOSTICO CAUSA BASICA DE LA MUERTE";
                    Conteo = Conteo + 1;
                }

                if (Model.diag_causa_antecedente_muerte != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "DIAGNOSTICO CAUSA ANTECEDENTE DE LA MUERTE";
                    Conteo = Conteo + 1;
                }
            }
            if (Model.procedimientoqx == "SI")
            {
                if (Model.id_cups_qx != null || Model.id_cups_qx != "")
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "INGRESAR CUPS";
                    Conteo = Conteo + 1;
                }
            }

            if (Model.diagnostico_calificado == null)
            {
                variable = "ERROR";
                variable2 = "INGRESAR DIAGNOSTICO PRINCIPAL";
                Conteo = Conteo + 1;
            }

            if (Model.calidadDiagnostico_egreso == "NO")
            {
                if (Model.diagnostico_calificado == null)
                {
                    variable = "ERROR";
                    variable2 = "INGRESAR DIAGNOSTICO EGRESO";
                    Conteo = Conteo + 1;
                }
                if (Model.justificacion == null || Model.justificacion == "")
                {
                    variable = "ERROR";
                    variable2 = "INGRESAR DIAGNOSTICO EGRESO";
                    Conteo = Conteo + 1;
                }
            }
            else
            {
                if (Model.diagnostico_calificado == null)
                {
                    variable = "ERROR";
                    variable2 = "INGRESAR DIAGNOSTICO EGRESO";
                    Conteo = Conteo + 1;
                }
            }
            if (Model.incapacidades == "SI")
            {
                DateTime dia1 = Convert.ToDateTime(Model.fecha_inicial.Value.Date);
                DateTime dia2 = Convert.ToDateTime(Model.fecha_final.Value.Date);

                TimeSpan ts = dia2.Date - dia1.Date;

                int differenceInDays = ts.Days + 1;
                if (differenceInDays < 0)
                {
                    variable = "ERROR";
                    variable2 = "FECHA INICIO INCAPACIDAD NO PUEDE SER MAYOR A FECHA FIN INCAPACIDAD";
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
                if (Model.fecha_egresook != null)
                {
                    List<ecop_concurrencia_evolucion> lst = new List<ecop_concurrencia_evolucion>();
                    ecop_concurrencia_evolucion objConEv = new ecop_concurrencia_evolucion();
                    objConEv.id_concurrencia = Model.id_concurrencia;

                    lst = Model.ConsultaEvoluciones(objConEv, ref MsgRes);

                    if (lst.Count != 0)
                    {
                        Nullable<DateTime> DtUltimaEvolucion = DateTime.Now;
                        foreach (ecop_concurrencia_evolucion item in lst)
                        {
                            DtUltimaEvolucion = Convert.ToDateTime(item.fecha);
                        }
                        if (DtUltimaEvolucion.Value.ToString("MM/dd/yyyy") == Model.fecha_egresook.Value.ToString("MM/dd/yyyy"))
                        {

                            List<ecop_concurrencia> LstCr = new List<ecop_concurrencia>();
                            LstCr = Model.ConsultaCriterioIngresoActualizado(Model.id_concurrencia, ref MsgRes);

                            egreso_auditoria_Hospitalaria objConcu = new egreso_auditoria_Hospitalaria();


                            objConcu.id_concurrencia = Model.id_concurrencia;

                            objConcu.fecha_egreso = Model.fecha_egresook;
                            objConcu.DxprincipalEgreso = Model.diagnostico_calificado;
                            objConcu.calidad_diagnostico_egreso = Model.calidadDiagnostico_egreso;
                            objConcu.diagnostico_egreso_historia_clinica = Model.diagnosticoEgreso_historiaClnica;
                            objConcu.justificacion = Model.justificacion;

                            objConcu.CondicionAlta = Convert.ToString(Model.CondicionAlta);
                            if (Model.CondicionAlta == "MUERTO")
                            {
                                objConcu.NumeroDefuncion = Model.NumeroDefuncion;
                                objConcu.HoraDefuncion = Model.HoraDefuncion;
                                objConcu.fecha_fallecimiento = Model.fecha_de_muerteok;
                                objConcu.observacion_fallecimiento = Model.Observacion;
                                objConcu.diag_causa_directa_muerte = Model.diag_causa_directa_muerte;
                                objConcu.diag_causa_basica_muerte = Model.diag_causa_basica_muerte;
                                objConcu.diag_causa_antecedente_muerte = Model.diag_causa_antecedente_muerte;
                                objConcu.fecha_exp_cedula_fallecido = Model.fecha_exp_cedula_fallecidook;
                            }
                            else
                            {
                                objConcu.NumeroDefuncion = "NA";
                                objConcu.HoraDefuncion = "NA";
                            }
                            objConcu.especialidad = Model.especialidad;
                            objConcu.eventos_en_salud = Model.eventos_en_salud;
                            objConcu.procedimientoqx = Model.procedimientoqx;
                            objConcu.id_cups_qx = Model.id_cups_qx;
                            objConcu.id_cups_qx2 = Model.id_cups_qx2;
                            objConcu.id_cups_qx3 = Model.id_cups_qx3;
                            if (Model.beneficiario == "SI")
                            {
                                objConcu.incapacidades = "SI";
                            }
                            else
                            {
                                objConcu.incapacidades = Model.incapacidades;
                            }

                            if (objConcu.incapacidades == "SI")
                            {
                                objConcu.fecha_inicial = Model.fecha_inicial;
                                objConcu.fecha_final = Model.fecha_final;

                                DateTime dias1 = Convert.ToDateTime(Model.fecha_inicial.Value.Date);
                                DateTime dias2 = Convert.ToDateTime(Model.fecha_final.Value.Date);

                                TimeSpan tss = dias2.Date - dias1.Date;

                                int differenceInDayss = tss.Days + 1;

                                objConcu.cantidad_dias = differenceInDayss;

                            }
                            else
                            {
                                objConcu.fecha_inicial = null;
                                objConcu.fecha_final = null;
                                objConcu.cantidad_dias = 0;
                            }
                            objConcu.origen_incap = Model.origen_incap;
                            objConcu.usuario_digita = Convert.ToString(SesionVar.UserName);
                            objConcu.fecha_digita = Convert.ToDateTime(DateTime.Now);
                            objConcu.causal_egreso = Convert.ToString(Model.causal_egreso);
                            objConcu.gestantes = Model.gestante;
                            objConcu.DxprincipalEgreso2 = Model.diagnostico_calificado2;
                            objConcu.DxprincipalEgreso3 = Model.diagnostico_calificado3;

                            if (Model.tiene_enfermedad_huer == "SI")
                            {
                                objConcu.id_enfermedades_huerfanas = Convert.ToInt32(Model.id_enfermedades_huerfanas);
                            }
                            else
                            {
                                objConcu.id_enfermedades_huerfanas = Convert.ToInt32(0);
                            }

                            objConcu.infeccion = Convert.ToBoolean(Model.infeccion);
                            objConcu.microorganismo = Model.microorganismo;

                            var idEgreso = Model.InsertaEgreso(objConcu, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);

                            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                            {
                                //insertar datos en hospitalizacion prevenible

                                var resultado = InsercionHospitalizacionPrevenible(idEgreso);

                                //fin datos hospitalización prevenible

                                List<vw_tablero_concurrencia> list = new List<vw_tablero_concurrencia>();
                                list = Model.GetLista;
                                foreach (var item in list)
                                {
                                    DateTime? dtFechaIngresook = Convert.ToDateTime(item.fecha_egreso_censo);

                                    Model.OBJCenso.id_censo = Model.id_censo;
                                    Model.OBJCenso.fecha_egreso_censo = Model.fecha_egresook;

                                    Model.ActualizarEgresoCenso(ref MsgRes);
                                }

                                ecop_concurrencia objpre = new ecop_concurrencia();

                                objpre.id_concurrencia = Model.id_concurrencia;
                                objpre.hospitalizacion_prevenible = Model.hospitalizacion_prevenible;
                                objpre.descripcion_prevenible = Model.descripcion_prevenible;

                                if (Model.lesiones_severas == Convert.ToBoolean(true))
                                {
                                    objpre.lesion_severa = "SI";
                                    objpre.id_lesion_severa = Model.id_lesiones_severas;
                                }
                                else
                                {
                                    objpre.lesion_severa = "NO";
                                    objpre.id_lesion_severa = 0;
                                }

                                Model.Actualizarprevenible(objpre, ref MsgRes);

                                ecop_censo obj1 = new ecop_censo();

                                obj1.id_censo = Model.id_censo;
                                obj1.fecha_egreso = Model.fecha_egresook;
                                Model.ActualizarCensoEgresoOK(obj1, ref MsgRes);

                                ecop_concurrencia obj2 = new ecop_concurrencia();

                                obj2.id_concurrencia = Model.id_concurrencia;
                                obj2.fecha_egreso = Model.fecha_egresook;

                                Session["pdfCrear"] = obj2.id_concurrencia;

                                List<vw_evo_ecop_concurrencia> listaC = new List<vw_evo_ecop_concurrencia>();
                                vw_evo_ecop_concurrencia ObjAfiliado = new vw_evo_ecop_concurrencia();
                                ObjAfiliado.id_concurrencia = Model.id_concurrencia;


                                listaC = BusClass.ConsultaIdConcurreniaEvo(ObjAfiliado, ref MsgRes);
                                foreach (var item2 in listaC.ToList())
                                {
                                    if (item2.Regional_Beneficiario_indice != "NA")
                                    {
                                        if (item2.Regional_Beneficiario_indice != item2.regional_asalud_indice)
                                        {

                                            CorreoDatosConcuRegionalDif(Model.id_concurrencia, item2.Regional_Beneficiario_indice);
                                        }

                                    }
                                    if (item2.Glosa == "SI")
                                    {
                                        CorreoDatosConcuRegionalGlosa(Model.id_concurrencia, item2.regional_asalud_indice);
                                    }
                                }

                                //CorreoDatosConcu(Convert.ToInt32(Model.id_concurrencia));

                                return RedirectToAction("BuscarControlConcurrencia", "Concurrencia");
                            }
                            else
                            {
                                ModelState.AddModelError("", "ERROR... ACTUALIZANDO....");
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("", "LA FECHA DE LA ULTIMA EVOLUCION:" + DtUltimaEvolucion.Value.ToString("dd/MM/yyy") + "DEBE SER IGUAL A LA DEL EGRESO");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error..DEBE TENER UNA EVOLUCION INGRESADA, PARA PODER REALIZAR EL EGRESO!");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR FECHA DEL  EGRESO!");
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            ViewBag.usuario = SesionVar.ROL;
            ViewBag.tipopatologiacatastrofica = Model.GetRefTipoPatologiaCatastrofica();
            ViewBag.tipohospitalizacion = Model.GetRefTipoHospitalizacion();
            ViewBag.estanciaprolongada = Model.GetRefCondicionProlongada();
            ViewBag.pertinenciaprolongada = Model.GetRefPertinenciaProlongada();

            return View(Model);
        }

        public ActionResult Tablero_hospitalizacionPrevenible()
        {
            return View();
        }


        [HttpPost]
        public ActionResult EncuestaSatisfacionConcurrencia(Models.Concurrencia.ConcurrenciaEncuesta Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.justificacion == null)
            {
                if (Model.pregunta1 == "Regular" || Model.pregunta1 == "Mala")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }
                if (Model.pregunta2 == "Regular" || Model.pregunta2 == "Mala")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }
                if (Model.pregunta3 == "Regular" || Model.pregunta3 == "Mala")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }
                if (Model.pregunta4 == "Medianamente Satisfecho" || Model.pregunta4 == "Nada Satisfecho")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }
                if (Model.pregunta5 == "Regular" || Model.pregunta5 == "Mala")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }
                if (Model.pregunta6 == "Regular" || Model.pregunta6 == "Mala")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }
                if (Model.pregunta7 == "Regular" || Model.pregunta7 == "Mala")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }
                if (Model.pregunta8 == "Casi Nunca" || Model.pregunta8 == "Nunca")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }

                if (Model.pregunta9 == "Regular" || Model.pregunta9 == "Mala")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }

                if (Model.pregunta10 == "Regular" || Model.pregunta10 == "Mala")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }

                if (Model.pregunta11 == "Regular" || Model.pregunta11 == "Mala")
                {
                    variable = "ERROR";
                    Conteo = Conteo + 1;
                }

            }

            if (variable != "ERROR")
            {
                Model.OBJEncuesta.id_concurrencia = Model.id_concurrencia;
                Model.OBJEncuesta.nombre_responde_encuesta = Model.nombre_responde_encuesta;
                Model.OBJEncuesta.canal = Model.canal;
                Model.OBJEncuesta.numero_encuesta = NumeroAuditoria(Model.id_concurrencia);
                Model.OBJEncuesta.pregunta1 = Model.pregunta1;
                Model.OBJEncuesta.pregunta2 = Model.pregunta2;
                Model.OBJEncuesta.pregunta3 = Model.pregunta3;
                Model.OBJEncuesta.pregunta4 = Model.pregunta4;
                Model.OBJEncuesta.pregunta5 = Model.pregunta5;
                Model.OBJEncuesta.pregunta6 = Model.pregunta6;
                Model.OBJEncuesta.pregunta7 = Model.pregunta7;
                Model.OBJEncuesta.pregunta8 = Model.pregunta8;
                Model.OBJEncuesta.pregunta9 = Model.pregunta9;
                Model.OBJEncuesta.pregunta10 = Model.pregunta10;
                Model.OBJEncuesta.pregunta11 = Model.pregunta11;
                Model.OBJEncuesta.observaciones = Model.observaciones;
                Model.OBJEncuesta.justificacion = Model.justificacion;
                Model.OBJEncuesta.fecha_digita = Convert.ToDateTime(DateTime.Now);
                Model.OBJEncuesta.usuario_digita = Convert.ToString(SesionVar.UserName);

                Model.InsertarEncuestaConcurrencia(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("BuscarControlConcurrencia", "Concurrencia");
                }
                else
                {
                    ModelState.AddModelError("", "ERROR - Actualizando");
                }
            }
            else
            {
                //ModelState.AddModelError("", "ERROR... DEBE INGRESAR LA JUSTIFICACION DE CASOS CON CALIFICACION REGULAR,MALA,NUNCA O CASI NUNCA" + ' ');
                ModelState.AddModelError("", "ERROR - Debe ingresar la justificación de casos con calificación Regular, Mala, Nunca o Casi nunca" + ' ');
            }

            return View(Model);
        }

        public JsonResult GetCie10(Models.Concurrencia.ConcurrenciaEgreso Model)
        {
            return Json(Model.LstCie10.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCie10Egreso(Models.Concurrencia.ConcurrenciaEgreso Model)
        {
            return Json(Model.LstCie102.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCie10EgresoPrincipal(Models.Concurrencia.ConcurrenciaEgreso Model)
        {
            return Json(Model.LstCie102Principal.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCie10EgresoPrincipal2(Models.Concurrencia.ConcurrenciaEgreso Model)
        {
            return Json(Model.LstCie102Principal.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCups(Models.Concurrencia.ConcurrenciaEgreso Model)
        {
            return Json(Model.LstCups.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEnfermedadesHuerfanas(Models.Concurrencia.ConcurrenciaEgreso Model)
        {
            return Json(Model.LstEnfermedadesHuerfanas.ToList(), JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public ActionResult UpdateOrder(String id, Int32? idconcu, Models.Evolucion.Evolucion Model)
        //{
        //    if (id != null)
        //    {
        //        Model.ConsultaListaAlertasNuevo(id, ref MsgRes);
        //        var AlertList = Model.ConsultaAlertasConcurrencia(Convert.ToInt32(idconcu), id).ToList();
        //        if (AlertList.Count() <= 0)
        //        {
        //            if (Model.ListaAlertasCie10.Count != 0)
        //            {
        //                String mensaje = "";
        //                foreach (var item in Model.ListaAlertasCie10)
        //                {
        //                    mensaje = item.alerta;
        //                }
        //                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    else
        //    {
        //        return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //    // return Json(Model.ListIPS2.Select(p => new { id_ref_ips_cuentas = p.id_ref_ips_cuentas, Nombre = p.Nombre }), JsonRequestBehavior.AllowGet);


        //}


        [HttpPost]
        public ActionResult UpdateOrder(String id, Int32? idconcu, Models.Evolucion.Evolucion Model)
        {
            bool rta = false;
            if (id != null)
            {
                Model.ConsultaListaAlertasNuevo(id, ref MsgRes);

                ecop_concurrencia concurrencia = BusClass.ConsultaConcurrenciaId(idconcu.Value);
                var paciente = concurrencia.id_afi;

                management_datosPaciente_alertasResult datos = new management_datosPaciente_alertasResult();
                datos = BusClass.DatosPaciente((int)idconcu);
                DateTime fechaNacimiento = new DateTime();

                if (datos.fecha_nacimiento != null)
                {
                    fechaNacimiento = (DateTime)datos.fecha_nacimiento;
                }

                var diff = DateTime.Now - fechaNacimiento;
                var dias = diff.TotalDays;
                var edadP = (int)(dias / 365.25);

                var AlertList = Model.ConsultaAlertasConcurrencia(Convert.ToInt32(idconcu), id).ToList();

                if (AlertList.Count() <= 0)
                {
                    if (Model.ListaAlertasCie10.Count != 0)
                    {
                        String mensaje = "";
                        var pasa = 0;

                        foreach (var item in Model.ListaAlertasCie10)
                        {
                            if (!item.tipo_rango.Equals("NULL"))
                            {
                                var tipoRango = item.tipo_rango;

                                //int edad = (int)concurrencia.afi_edad;
                                int edad = edadP;

                                String[] tipoRange = new string[0];

                                if (tipoRango.Contains("/"))
                                {
                                    tipoRange = tipoRango.Split('/');

                                    if (tipoRange.Length > 1)
                                    {
                                        for (int i = 0; i < tipoRange.Length; i++)
                                        {
                                            var tipoRangoDef = tipoRange[i];
                                            var rango = tipoRangoDef.Split('-');

                                            if (rango.Length > 1)
                                            {
                                                if (edad >= Convert.ToInt32(rango[0]) && edad <= Convert.ToInt32(rango[1]))
                                                {
                                                    pasa = pasa + 2;
                                                }
                                                else
                                                {
                                                    pasa--;
                                                }
                                            }
                                            else
                                            {
                                                if (edad >= Convert.ToInt32(rango[0]))
                                                {
                                                    pasa = pasa + 2;
                                                }
                                                else
                                                {
                                                    pasa--;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var rango = tipoRango.Split('-');
                                        var tipoRangoDef = rango[0];

                                        if (edad >= Convert.ToInt32(tipoRangoDef[0]) && edad <= Convert.ToInt32(tipoRangoDef[1]))
                                        {
                                            pasa = 1;
                                        }
                                        else
                                        {
                                            pasa = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    var rango = tipoRango.Split('-');

                                    if (edad >= Convert.ToInt32(rango[0]) && edad <= Convert.ToInt32(rango[1]))
                                    {
                                        pasa = 1;
                                    }
                                    else
                                    {
                                        pasa = 0;
                                    }
                                }

                                if (pasa > 0)
                                {
                                    mensaje = item.alerta;
                                    return Json(new { success = true, message = mensaje, opc = 1 }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    mensaje = $"Dx no cumple con el rango de edad : {tipoRango}";
                                    return Json(new { success = false, message = mensaje, opc = 2 }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                mensaje = item.alerta;
                                return Json(new { success = true, message = mensaje, opc = 1 }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (rta)
                        {
                            return Json(new { success = true, message = mensaje, opc = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, message = "Dx no cumple con el rango de edad", opc = 2 }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = true, message = "", opc = 1 }, JsonRequestBehavior.AllowGet);
            }
            // return Json(Model.ListIPS2.Select(p => new { id_ref_ips_cuentas = p.id_ref_ips_cuentas, Nombre = p.Nombre }), JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult UpdateOrder2(String id, String concurrencia, Models.Evolucion.Evolucion Model)
        {
            int tipoevento = 0;
            string msgRta = "";

            var cie10_alerta = Model.ConsultaAlertaCie10(id);
            if (cie10_alerta.alerta.ToUpper().StartsWith("SALUD PUBLICA"))
            {
                Models.analisis_de_caso.AnalisisSaludPublica saludp = new Models.analisis_de_caso.AnalisisSaludPublica();
                var listadx = saludp.ConsultaEvolucionAnalisisSaludP(Convert.ToInt32(concurrencia), null).ToList();

                tipoevento = 1;
                msgRta = "Este diagnostico genera un analisis de caso en salud publica.";
            }
            else
            {
                Models.analisis_de_caso.AnalisisOriginal saludp = new Models.analisis_de_caso.AnalisisOriginal();
                var listadx = saludp.ConsultaAnalisisCasoOriginal(Convert.ToInt32(concurrencia), null).ToList();

                tipoevento = 2;
                msgRta = "Este diagnostico genera un analisis de caso en centinelas, catastroficas o trazadoras.";

            }

            if (id != null)
            {
                Model.ObjAlertas.id_concurrencia = Convert.ToInt32(concurrencia);
                Model.ObjAlertas.cie10 = Convert.ToString(id);
                Model.ObjAlertas.usuario_ingreso = SesionVar.UserName;
                Model.ObjAlertas.fecha_ingreso = Convert.ToDateTime(DateTime.Now);

                Model.InsertarAlertasConcurrencia(ref MsgRes);
            }

            var data = new
            {
                success = true,
                message = "Ingreso Exitoso....",
                msgEvento = msgRta,
                evento = tipoevento
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ActualizarTSH(Models.Evolucion.Evolucion Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";
            if (Model.fechaTCHok == null)
            {
                variable2 = "INGRESAR FECHA";
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
                Model.Objegreso.id_concurrencia = Model.id_concurrencia;
                Model.Objegreso.fechaTCH = Model.fechaTCHok;
                Model.Objegreso.resultadoTCH = Model.resultadoTCH;

                Model.ActualizarEgreso(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("Evolucion", "Evolucion", new { idConcu = Model.id_concurrencia });
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            return View(Model);

        }

        private void Random()
        {
            string s_rand = Convert.ToString(Session["Random"]);
            Random r = new Random();
            int e;
            if (s_rand == "")
            {
                e = r.Next(1, 24);
            }
            else
            {
                e = r.Next(1, 24);
                e = e + 1;
            }

            switch (e)
            {
                case 1:
                    Session["Random"] = "A";
                    break;
                case 2:
                    Session["Random"] = "B";
                    break;
                case 3:
                    Session["Random"] = "C";
                    break;
                case 4:
                    Session["Random"] = "D";
                    break;
                case 5:
                    Session["Random"] = "E";
                    break;
                case 6:
                    Session["Random"] = "F";
                    break;
                case 7:
                    Session["Random"] = "G";
                    break;
                case 8:
                    Session["Random"] = "H";
                    break;
                case 9:
                    Session["Random"] = "I";
                    break;
                case 10:
                    Session["Random"] = "J";
                    break;
                case 11:
                    Session["Random"] = "K";
                    break;
                case 12:
                    Session["Random"] = "M";
                    break;
                case 13:
                    Session["Random"] = "N";
                    break;
                case 14:
                    Session["Random"] = "L";
                    break;
                case 15:
                    Session["Random"] = "O";
                    break;
                case 16:
                    Session["Random"] = "P";
                    break;
                case 17:
                    Session["Random"] = "Q";
                    break;
                case 18:
                    Session["Random"] = "R";
                    break;
                case 19:
                    Session["Random"] = "S";
                    break;
                case 20:
                    Session["Random"] = "T";
                    break;
                case 21:
                    Session["Random"] = "U";
                    break;
                case 22:
                    Session["Random"] = "V";
                    break;
                case 23:
                    Session["Random"] = "W";
                    break;
                case 24:
                    Session["Random"] = "X";
                    break;
                case 25:
                    Session["Random"] = "Y";
                    break;
                case 26:
                    Session["Random"] = "Z";
                    break;
                default:
                    break;
            }
        }

        private string NumeroAuditoria(Int32 id_concurrencia)
        {

            int anio;
            int Aumento = id_concurrencia;
            string RLetras;
            string AuditoriaNumero;
            string RLetrasDos;
            string Letras;

            anio = Convert.ToInt32(DateTime.Now.Year);


            Random();
            RLetras = Convert.ToString(Session["Random"]);
            Random();
            RLetrasDos = Convert.ToString(Session["Random"]);
            Letras = RLetras + RLetrasDos;

            AuditoriaNumero = anio + "" + Aumento + "" + Letras;



            return AuditoriaNumero;
        }

        [HttpGet]
        public ActionResult Gestantes(String ID)
        {
            Models.Concurrencia.ConcurrenciaEgreso Model = new Models.Concurrencia.ConcurrenciaEgreso();
            Model.id_concurrencia = Convert.ToInt32(ID);

            return View(Model);
        }

        [HttpPost]
        public ActionResult Gestantes(Models.Concurrencia.ConcurrenciaEgreso Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_nacimientook != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "FECHA DE NACIMIENTO";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_BCGok != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "FECHA DE BCG";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_vitaminaKok != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "FECHA DE VITAMINA K";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_consenjeria_lactanciaok != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "FECHA DE CONSEJERIA DE LACTANCIA";
                Conteo = Conteo + 1;
            }
            if (Model.fechaTCHok != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "FECHA DE TCH";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_consulta_pediatriaok != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "FECHA DE CONSULTA PEDIATRIA";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_hepatitisBok != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "FECHA DE VACUNA HEPATITIS B";
                Conteo = Conteo + 1;
            }
            if (Model.tamizaje == "SI")
            {
                if (Model.fecha_tamizajeOk == null)
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE TAMIZAJE";
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

                Model.OBJEgresoGestantes.id_concurrencia = Model.id_concurrencia;
                Model.OBJEgresoGestantes.edad_gestacional = Model.edad_gestacional;
                Model.OBJEgresoGestantes.fecha_nacimiento = Model.fecha_nacimientook;
                Model.OBJEgresoGestantes.peso = Model.peso;
                Model.OBJEgresoGestantes.via_parto = Model.id_via_parto;
                Model.OBJEgresoGestantes.talla = Model.talla;
                Model.OBJEgresoGestantes.sexo = Model.sexo;
                Model.OBJEgresoGestantes.apgar = Model.apgar;
                Model.OBJEgresoGestantes.control_prenatal = Model.control_prenatal;
                Model.OBJEgresoGestantes.fecha_BCG = Model.fecha_BCGok;
                Model.OBJEgresoGestantes.fecha_vitaminaK = Model.fecha_vitaminaKok;
                Model.OBJEgresoGestantes.fecha_consenjeria_lactancia = Model.fecha_consenjeria_lactanciaok;
                Model.OBJEgresoGestantes.resultadoTCH = Model.resultadoTCH;
                Model.OBJEgresoGestantes.fechaTCH = Model.fechaTCHok;
                Model.OBJEgresoGestantes.fecha_consulta_pediatria = Model.fecha_consulta_pediatriaok;
                Model.OBJEgresoGestantes.fecha_hepatitisB = Model.fecha_hepatitisBok;
                Model.OBJEgresoGestantes.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                Model.OBJEgresoGestantes.tamizaje = Model.tamizaje;
                Model.OBJEgresoGestantes.acompañamiento_parto = Model.compañiaduranteparto;

                Model.InsertarEgresoGestantes(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("Gestantes", "Concurrencia", new { ID = Model.id_concurrencia });
                }
                else
                {
                    ModelState.AddModelError("", "ERROR AL INGRESO......");
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            return View(Model);
        }

        public ActionResult tableroegresos(int? rta, DateTime? fechainicial, DateTime? fechafinal)
        {
            Models.Concurrencia.ConcurrenciaEgreso Model = new Models.Concurrencia.ConcurrenciaEgreso();

            List<management_ecop_egresos_hospitalariosResult> egresos = new List<management_ecop_egresos_hospitalariosResult>();
            try
            {
                if (fechainicial != null || fechafinal != null)
                {
                    egresos = Model.listadoEgresosHospitalarios(fechainicial, fechafinal);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.fechainicial = fechainicial;
            ViewBag.fechafinal = fechafinal;

            ViewBag.egresos = egresos;
            ViewBag.rta = rta;
            return View();
        }

        public ActionResult categorizacionegresohosp(int id_concurrencia, int? rta)
        {
            categorizacion_egreso_hospitalario modelo = new categorizacion_egreso_hospitalario();
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<management_egresos_categorizacionResult> listaConsolidado = new List<management_egresos_categorizacionResult>();
            var conteo = 0;
            Models.General General = new Models.General();

            try
            {
                if (rta == 1)
                {
                    ViewData["alerta"] = General.MsgRespuesta("success", "Transacción exitosa!", "Ingresó la evaluación pertinencia, correctamente!");
                }

                else if (rta == 2)
                {
                    ViewData["alerta"] = General.MsgRespuesta("danger", "Ingreso fallido!", MsgRes.DescriptionResponse);
                }
                else
                {
                    ViewData["alerta"] = "";
                }

                listaConsolidado = BusClass.listadoEgresosCategorizacion(id_concurrencia);
                conteo = listaConsolidado.Count();

                ViewBag.conteo = conteo;
                ViewBag.lista = listaConsolidado;
                ViewBag.id_concurrencia = id_concurrencia;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            if (modelo != null)
            {
                return View(modelo);
            }
            else
            {
                return View(new categorizacion_egreso_hospitalario());
            }
        }

        public PartialViewResult EvaluarPertinencia(int idEgreso, int? idConcurrencia, int dias, string documento)
        {
            Models.Concurrencia.ConcurrenciaEgreso Model = new Models.Concurrencia.ConcurrenciaEgreso();
            List<management_egresos_categorizacionResult> listaConsolidado = new List<management_egresos_categorizacionResult>();
            management_egresos_categorizacionResult datosEgreso = new management_egresos_categorizacionResult();
            //List<vw_evo_ecop_concurrencia_evoluciones> evoluciones = new List<vw_evo_ecop_concurrencia_evoluciones>();
            try
            {
                listaConsolidado = BusClass.listadoEgresosCategorizacion((int)idConcurrencia);
                datosEgreso = listaConsolidado.Where(x => x.idEgreso == idEgreso).FirstOrDefault();

                ViewBag.datosEgreso = datosEgreso;
                //ViewBag.estancia = estancia;
                //ViewBag.tipoHabitacion = validacionHospitalizacion(documento, id_evolucion);
                ViewBag.tipopatologiacatastrofica = BusClass.GetAltoCosto();
                ViewBag.tipohospitalizacion = Model.GetRefTipoHospitalizacion();
                ViewBag.estanciaprolongada = Model.GetRefCondicionProlongada();
                ViewBag.pertinenciaprolongada = Model.GetRefPertinenciaProlongada();
                ViewBag.idEgreso = idEgreso;
                ViewBag.causal = BusClass.GetCausalGlosa();
                ViewBag.tipohabitacion = datosEgreso.tipoHabitacion;
                ViewBag.pertinenciaestancia = datosEgreso.estancia_pertinente;
                ViewBag.valorGlosas = datosEgreso.valorGlosasTotal;
                ViewBag.num_dias_no_pertinentes = datosEgreso.diasEstancia;

                ViewBag.patologia = datosEgreso.id_lesion_severa;

                ViewBag.idConcurrencia = idConcurrencia;

                ViewBag.diasestancia = dias;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView(new categorizacion_egreso_hospitalario());
        }

        [HttpPost]
        public ActionResult categorizacionegresohosp(int? idegreso, int? idcategorizacion, int? tipopatologia, int? tipohospitalizacion,
            int? condicioneprolongada, string tipomedicoadmin, string responsable_pertinencia, int? dias,
            string valor, string causal, string observaciones, int? id_concurrencia, int? estanciaPertinente)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            double total_glosa = 0;
            Models.Evolucion.Evolucion Model2 = new Models.Evolucion.Evolucion();
            Models.Concurrencia.ConcurrenciaEgreso Model = new Models.Concurrencia.ConcurrenciaEgreso();
            categorizacion_egreso_hospitalario obj = new categorizacion_egreso_hospitalario();

            try
            {
                valor = RemoveSpecialCharacters(valor);

                obj.id_egreso_hospitalario = (int)idegreso;
                obj.id_concurrencia = id_concurrencia;
                obj.id_categorizacion_egreso_hospitalario = (int)idcategorizacion;
                obj.id_tipo_patologia_catastrofica = (int)tipopatologia;
                obj.id_tipo_hospitalizacion = (int)tipohospitalizacion;
                obj.id_condicion_estancia_prolongada = (int)condicioneprolongada;
                obj.id_pertinencia_estancia_prolongada = "SI";
                obj.tipo_medico_admin = tipomedicoadmin;
                obj.responsable_no_pertinencia = responsable_pertinencia;
                obj.num_dias_no_pertinentes = dias;
                if (!string.IsNullOrEmpty(valor))
                {
                    obj.valor_aprox_pertinencia = Convert.ToDecimal(valor);
                }

                obj.causal_no_pertinencia = causal;
                obj.observaciones = observaciones;
                obj.tiene_pertinencia = estanciaPertinente;

                if (idcategorizacion == 0)
                {
                    obj.usuario_digita = SesionVar.UserName;
                    obj.fecha_digita = DateTime.Now;
                    Model.insertarcategorizacion(obj, ref MsgRes);
                }
                else
                {
                    Model.actualizarcategorizacion(obj, ref MsgRes);
                }

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("categorizacionegresohosp", "Concurrencia", new { id_concurrencia = id_concurrencia, rta = 1 });
                }
                else
                {
                    return RedirectToAction("categorizacionegresohosp", "Concurrencia", new { id_concurrencia = id_concurrencia, rta = 2 });
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return RedirectToAction("categorizacionegresohosp", "Concurrencia", new { id_concurrencia = id_concurrencia, rta = 2 });
            }
        }

        public PartialViewResult VerEvaluacionPertinencia(int idEgreso)
        {
            Models.Concurrencia.ConcurrenciaEgreso Model = new Models.Concurrencia.ConcurrenciaEgreso();
            List<management_egresos_verCategorizacionResult> dato = new List<management_egresos_verCategorizacionResult>();

            try
            {
                dato = BusClass.traerDatosCategorizacionEgreso(idEgreso);
                var conteo = dato.Count();
                ViewBag.dato = dato;
                ViewBag.conteo = conteo;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            //vw_ecop_evo_evaluacion_pertinencia modelo = Model.GetEvaluacionPertinenciaById(idevolucion);

            return PartialView();
        }

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^0-9A-Za-z]", "", RegexOptions.None);
        }

        public PartialViewResult ActualizarIPS()
        {
            Models.Concurrencia.Concurrencia Model = new Models.Concurrencia.Concurrencia();
            ViewBag.ips = Model.GetRefIps();
            return PartialView();
        }

        public JsonResult BuscarAfiliados(string term)
        {
            Models.Concurrencia.Concurrencia Model = new Models.Concurrencia.Concurrencia();
            List<ecop_concurrencia> afiliados = Model.GetconcurrenciaAfiliados(term);

            var lista = (from item in afiliados
                         select new
                         {
                             idconcurrencia = item.id_concurrencia,
                             nombre = item.afi_nom.ToUpper(),
                             fecha_ingreso = item.fecha_ingreso.Value.ToString("dd/MM/yyyy"),
                             ips = item.id_ips,
                             label = item.id_afi
                         }).Distinct().OrderBy(f => f.label).Take(15);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarModificacionIPS(int idconcurrencia, int idips)
        {
            Models.Concurrencia.Concurrencia Model = new Models.Concurrencia.Concurrencia();
            Model.ActualizarIps(idconcurrencia, idips, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { rta = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { rta = 1, msj = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ExcelConcurrenciaSinEgreso()
        {
            try
            {
                List<vw_tablero_concurrencia> listareporte = BusClass.GetTableroConcurrencia();

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");

                Sheet.Cells["A1:T1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:T1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:T1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:T1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "ID_CONCURRENCIA";
                Sheet.Cells["B1"].Value = "ID_CENSO";
                Sheet.Cells["C1"].Value = "AFI_TIPO_DOC";
                Sheet.Cells["D1"].Value = "ID_AFI";
                Sheet.Cells["E1"].Value = "AFI_NOM";
                Sheet.Cells["F1"].Value = "FECHA_INGRESO";
                Sheet.Cells["G1"].Value = "NOMBRE_IPS";
                Sheet.Cells["H1"].Value = "CIUDAD_IPS";
                Sheet.Cells["I1"].Value = "REGIONAL";
                Sheet.Cells["J1"].Value = "FECHA_MOD";
                Sheet.Cells["K1"].Value = "USUARIO";
                Sheet.Cells["L1"].Value = "NOMBRE_USUARIO";
                Sheet.Cells["M1"].Value = "FECHA_EGRESO_CENSO";
                Sheet.Cells["N1"].Value = "TIPO_HABITACION";
                Sheet.Cells["O1"].Value = "FECHA_INGRESAR";
                Sheet.Cells["P1"].Value = "TIENE_ENCUESTA";
                Sheet.Cells["Q1"].Value = "ID_BASE_BENEFICIARIOS";
                Sheet.Cells["R1"].Value = "GENERO";
                Sheet.Cells["S1"].Value = "HOSPITALIZACIÓN";
                Sheet.Cells["T1"].Value = "CAUSA HOSPITALIZACIÓN";

                int row = 2;
                foreach (vw_tablero_concurrencia item in listareporte)
                {

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_concurrencia;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.id_censo;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.afi_tipo_doc;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.id_afi;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.afi_nom;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_ingreso;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.nombre_ips;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.nombre_ips;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.nombre_regional;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_mod;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.usuario;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.Nombre_usuario;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.fecha_egreso_censo;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.tipo_habitacion;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.fecha_ingresar;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.tiene_encuesta;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.id_base_beneficiarios;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.genero;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.hospitalizacion_prevenible;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.descripcion_prevenible;

                    Sheet.Cells["A" + row + ":T1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:T"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptTableroConcurrencia.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public void CorreoDatosConcu(int idConcurrencia)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                byte[] array = new byte[0];
                array = PdfEvoluciones(idConcurrencia);
                array = array.ToArray();
                string filename = "";

                List<management_evolucionEgresosListaResult> lista = new List<management_evolucionEgresosListaResult>();
                lista = BusClass.GetEvolucionesConcurrencia(idConcurrencia);

                management_evolucionEgresosListaResult concu = lista.FirstOrDefault();

                var documento = concu.id_afi;
                var tipoDoc = concu.tipo_documento;
                var nombre = concu.afi_nom;
                var ips = concu.nombre_ips;
                var diagnostico = concu.des_diagnostico_egreso_historia_clinica;
                var nombreMega = "";
                var documentoMega = "";

                filename = tipoDoc + "_" + documento + "_" + idConcurrencia + ".pdf";

                List<management_egresBuscar_megaResult> bb = new List<management_egresBuscar_megaResult>();
                management_egresBuscar_megaResult baseB = new management_egresBuscar_megaResult();
                List<ecop_directorioPPE_correos> correos = new List<ecop_directorioPPE_correos>();

                bb = BusClass.BuscarMegaEgreso(documento).OrderByDescending(x => x.id_base_beneficiarios).ToList();

                if (bb.Count() != 0)
                {
                    baseB = bb.FirstOrDefault();

                    if (baseB != null)
                    {
                        nombreMega = baseB.mega_nombre;
                        documentoMega = baseB.mega_documento;

                        correos = BusClass.GetEcop_DirectorioPPE_CorreosDocumento(documentoMega);
                    }
                }

                List<management_cohortesBeneficiarioResult> list = new List<management_cohortesBeneficiarioResult>();
                list = BusClass.GetCohortesBeneficiario(documento);
                list = list.OrderByDescending(x => x.id_cohorte).ToList();

                management_cohortesBeneficiarioResult Cohorte = list.FirstOrDefault();
                var regional = "";
                if (Cohorte != null)
                {
                    regional = Cohorte.regional;
                }

                var tipoSalud = "";
                if (baseB != null)
                {
                    tipoSalud = baseB.tipo_salud;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("Estimado Profesional puerta de entrada PPE - Ecopetrol.");
                sb.Append("<br/>");
                sb.Append("<br/>");

                string textBody = sb.ToString();

                var textMensaje = "Gusto en saludarles, nos permitimos informarles que el paciente " + nombre + " con " + tipoDoc + " " + documento + " ha egresado de hospitalización de la IPS " + ips + " con el siguiente diagnóstico " + diagnostico + " para su importante seguimiento y gestión que aplique.";

                var textMensaje2 = "";
                if (tipoSalud != "" && tipoSalud != null)
                {
                    textMensaje2 = "Este beneficiario es: " + tipoSalud;
                }
                else
                {
                    textMensaje2 = "Este beneficiario es: " + "Sin tipo de salud";
                }
                var textMensaje3 = "Este beneficiario (a) es: pertenece a la cohorte: ";

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
                mailBody += textMensaje;
                mailBody += "<br />";
                mailBody += "<br />";

                mailBody += textMensaje2;

                mailBody += "<br />";
                if (list.Count != 0)
                {
                    mailBody += textMensaje3;
                    mailBody += Cohorte.descripcion;
                }
                else
                {
                    mailBody += textMensaje3;
                    mailBody += "Sin Cohortes.";
                }

                mailBody += "<br />";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
                mailBody += "<br />";
                mailBody += "Dir. Calle 96 # 13a-03 Piso 4";
                mailBody += "<br />";
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

                if (baseB != null)
                {
                    foreach (var item in correos)
                    {
                        String[] correoVario = new string[0];
                        String[] correosCortados = new string[0];
                        String[] correosCortadosAuditor = new string[0];
                        String[] TotalidadCorreos = new string[0];

                        var correosCompletos = "";
                        var correosCompletosAuditor = "";

                        if (item.correo != "" && item.correo != null && item.mega_correo != "")
                        {
                            correosCompletosAuditor = item.mega_correo;
                            correosCompletos = item.correo;
                        }

                        correosCompletos = correosCompletos.Replace("/", "|");
                        correosCompletos = correosCompletos.Replace(",", "|");
                        correosCompletos = correosCompletos.Replace("-", "|");
                        correosCompletos = correosCompletos.Replace(";", "|");
                        correosCortados = correosCompletos.Split('|');

                        correosCompletosAuditor = correosCompletosAuditor.Replace("/", "|");
                        correosCompletosAuditor = correosCompletosAuditor.Replace(",", "|");
                        correosCompletosAuditor = correosCompletosAuditor.Replace("-", "|");
                        correosCompletosAuditor = correosCompletosAuditor.Replace(";", "|");
                        correosCortadosAuditor = correosCompletosAuditor.Split('|');

                        var conteoMega = correosCortados.Count();

                        var conteo = 0;
                        var conteoAuditor = 0;

                        foreach (var cor in correosCortados)
                        {
                            try
                            {
                                var correo = "";

                                if (correosCortados.Count() > conteo)
                                {
                                    correo = correosCortados[conteo];
                                    correo = correo.Replace(" ", "");

                                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                                    {
                                        mailMessage.From = new MailAddress(smtpSection.From);
                                        mailMessage.To.Clear();
                                        mailMessage.To.Add(correo);
                                        mailMessage.Bcc.Clear();
                                        mailMessage.Bcc.Add("desarrollo.soporte@asalud.co");
                                    }
                                    else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                                    {
                                        mailMessage.From = new MailAddress(smtpSection.From);
                                        mailMessage.To.Clear();
                                        mailMessage.To.Add(correo);

                                    }

                                    mailMessage.Subject = "[Mensaje Automático]" + " " + "Egreso usuario";
                                    mailMessage.IsBodyHtml = true;
                                    mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                                    mailMessage.IsBodyHtml = true;

                                    MemoryStream memoryStream = new MemoryStream(array);
                                    mailMessage.Attachments.Clear();
                                    mailMessage.Attachments.Add(new Attachment(memoryStream, filename));
                                    mailMessage.IsBodyHtml = true;
                                    objMail.Send(mailMessage);
                                }
                                conteo++;
                            }
                            catch (Exception ex)
                            {
                                var errorMega = ex.Message;
                                CorreoDatosConcuExcepcion(idConcurrencia, nombreMega, 1);
                                Session["pdfCrear"] = null;
                            }
                            Session["pdfCrear"] = null;
                        }

                        foreach (var cor in correosCortadosAuditor)
                        {
                            try
                            {
                                var correo = "";

                                if (correosCortadosAuditor.Count() > conteoAuditor)
                                {
                                    correo = correosCortadosAuditor[conteoAuditor];

                                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                                    {
                                        mailMessage.From = new MailAddress(smtpSection.From);
                                        mailMessage.To.Clear();
                                        mailMessage.To.Add(correo);
                                        //mailMessage.Bcc.Clear();
                                        //mailMessage.Bcc.Add("desarrollo@asaludltda.com");
                                    }
                                    else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                                    {
                                        mailMessage.From = new MailAddress(smtpSection.From);
                                        mailMessage.To.Clear();
                                        mailMessage.To.Add(correo);
                                    }

                                    mailMessage.Subject = "[Mensaje Automático]" + " " + "Egreso usuario";
                                    mailMessage.IsBodyHtml = true;
                                    mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                                    mailMessage.IsBodyHtml = true;

                                    MemoryStream memoryStream = new MemoryStream(array);

                                    mailMessage.Attachments.Clear();
                                    mailMessage.Attachments.Add(new Attachment(memoryStream, filename));
                                    mailMessage.IsBodyHtml = true;
                                    objMail.Send(mailMessage);
                                }
                                conteoAuditor++;
                            }
                            catch (Exception ex)
                            {
                                var errorMega = ex.Message;
                                CorreoDatosConcuExcepcion(idConcurrencia, nombreMega, 2);
                                Session["pdfCrear"] = null;
                            }
                            Session["pdfCrear"] = null;
                        }
                    }
                }
                Session["pdfCrear"] = idConcurrencia;
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.AddModelError("", "ERROR!" + ViewBag.Message);
                Session["pdfCrear"] = null;
            }
        }

        public byte[] PdfEvoluciones(int idConcurrencia)
        {
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "ReporteConcurrencia.rdlc");

            List<management_evolucionEgresosListaResult> lst = new List<management_evolucionEgresosListaResult>();
            management_evolucionEgresosListaResult listaUnica = new management_evolucionEgresosListaResult();
            List<management_evolucionEgresosListaResult> lst2 = new List<management_evolucionEgresosListaResult>();
            List<management_evolucionEgresosListaResult> lst3 = new List<management_evolucionEgresosListaResult>();
            lst = BusClass.GetEvolucionesConcurrencia(idConcurrencia);

            listaUnica = lst.FirstOrDefault();
            lst2.Add(listaUnica);
            lst3.Add(listaUnica);

            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetConcurrencia", lst);
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();

            viewer.LocalReport.EnableExternalImages = true;
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            viewer.LocalReport.EnableHyperlinks = true;

            byte[] documento = null;

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

                    //using (MemoryStream inputData = new MemoryStream(pdfContent))
                    //{
                    //    using (MemoryStream outputData = new MemoryStream())
                    //    {
                    //        string PDFFilepassword = "123";
                    //        PdfReader reader = new PdfReader(inputData);
                    //        PdfReader.unethicalreading = true;
                    //        PdfEncryptor.Encrypt(reader, outputData, true, PDFFilepassword, PDFFilepassword, PdfWriter.ALLOW_SCREENREADERS);
                    //        documento = outputData.ToArray();
                    //    }
                    //}

                    //return documento;
                    return documento = pdfContent;

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return documento;
                }
            }
            return documento;
        }

        public void PdfDescargaConcu(int idConcurrencia)
        {
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            //RUTA REPORTE
            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "ReporteConcurrencia.rdlc");

                List<management_evolucionEgresosListaResult> lst = new List<management_evolucionEgresosListaResult>();
                management_evolucionEgresosListaResult listaUnica = new management_evolucionEgresosListaResult();
                List<management_evolucionEgresosListaResult> lst2 = new List<management_evolucionEgresosListaResult>();
                List<management_evolucionEgresosListaResult> lst3 = new List<management_evolucionEgresosListaResult>();
                lst = BusClass.GetEvolucionesConcurrencia(idConcurrencia);


                listaUnica = lst.FirstOrDefault();
                lst2.Add(listaUnica);
                lst3.Add(listaUnica);

                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetConcurrencia", lst);
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();

                viewer.LocalReport.EnableExternalImages = true;
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);

                viewer.LocalReport.EnableHyperlinks = true;

                byte[] documento = null;

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
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void CorreoDatosConcuExcepcion(int idConcurrencia, string nombreMega, int opcion)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            byte[] array = new byte[0];
            array = PdfEvoluciones(idConcurrencia);
            array = array.ToArray();
            string filename = "";

            List<management_evolucionEgresosListaResult> lista = new List<management_evolucionEgresosListaResult>();
            lista = BusClass.GetEvolucionesConcurrencia(idConcurrencia);

            management_evolucionEgresosListaResult concu = lista.FirstOrDefault();

            var documento = concu.id_afi;
            var tipoDoc = concu.tipo_documento;
            var nombre = concu.afi_nom;
            var ips = concu.nombre_ips;
            var diagnostico = concu.des_diagnostico_egreso_historia_clinica;

            filename = tipoDoc + "_" + documento + "_" + idConcurrencia + ".pdf";

            //var correo = "desarrollo@asaludltda.com";
            var correo = "asistenteecopetrol@asalud.co";


            StringBuilder sb = new StringBuilder();
            sb.Append("Estimado Profesional puerta de entrada PPE - Ecopetrol.");
            sb.Append("<br/>");
            sb.Append("<br/>");

            string textBody = sb.ToString();


            var textMensaje = "No se le ha podido enviar la notificación del paciente con " + tipoDoc + ": " + documento;

            if (opcion == 1)
            {
                textMensaje += " al mega: " + nombreMega + " para el egreso de la ips: " + ips + " con diagnostico: " + diagnostico;
            }
            else
            {
                textMensaje += " al auditor del mega: " + nombreMega + " para el egreso de la ips: " + ips + " con diagnostico: " + diagnostico;
            }

            var textMensaje2 = "Por favor verificar los datos del mega";

            var textMensaje3 = "Error: Correo mal gestionado";

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
                mailBody += textMensaje;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += textMensaje2;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += textMensaje3;
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

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    mailMessage.From = new MailAddress(smtpSection.From);

                    mailMessage.To.Clear();
                    mailMessage.To.Add(correo);

                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    mailMessage.From = new MailAddress(smtpSection.From);
                    mailMessage.To.Clear();
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                }

                mailMessage.Subject = "[Mensaje Automático]" + " " + "Egreso usuario";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";


                MemoryStream memoryStream = new MemoryStream(array);
                mailMessage.Attachments.Clear();
                mailMessage.Attachments.Add(new Attachment(memoryStream, filename));
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);
                Session["pdfCrear"] = null;

            }

            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.AddModelError("", "ERROR!" + ViewBag.Message);
                Session["pdfCrear"] = null;
            }
        }


        public void CorreoDatosConcuRegionalDif(int idConcurrencia, String Regional)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                byte[] array = new byte[0];
                array = PdfEvoluciones(idConcurrencia);
                array = array.ToArray();
                string filename = "";

                List<management_evolucionEgresosListaResult> lista = new List<management_evolucionEgresosListaResult>();
                lista = BusClass.GetEvolucionesConcurrencia(idConcurrencia);

                management_evolucionEgresosListaResult concu = lista.FirstOrDefault();

                var documento = concu.id_afi;
                var tipoDoc = concu.tipo_documento;
                var nombre = concu.afi_nom;
                var ips = concu.nombre_ips;
                var diagnostico = concu.des_diagnostico_egreso_historia_clinica;
                var nombreMega = "";
                var documentoMega = "";
                var correo_envio = "";

                filename = tipoDoc + "_" + documento + "_" + idConcurrencia + ".pdf";

                List<management_egresBuscar_megaResult> bb = new List<management_egresBuscar_megaResult>();
                management_egresBuscar_megaResult baseB = new management_egresBuscar_megaResult();
                List<ecop_directorioPPE_correos> correos = new List<ecop_directorioPPE_correos>();

                List<Ref_regional> Lista_correo = new List<Ref_regional>();

                Lista_correo = BusClass.GetRefRegion();
                Lista_correo = Lista_correo.Where(x => x.indice == Regional).ToList();
                foreach (var item in Lista_correo)
                {
                    correo_envio = item.correo;
                }

                bb = BusClass.BuscarMegaEgreso(documento).OrderByDescending(x => x.id_base_beneficiarios).ToList();

                if (Lista_correo.Count() != 0)
                {
                    baseB = bb.FirstOrDefault();

                    if (baseB != null)
                    {
                        nombreMega = baseB.mega_nombre;
                        documentoMega = baseB.mega_documento;

                        correos = BusClass.GetEcop_DirectorioPPE_CorreosDocumento(documentoMega);
                    }
                }

                List<management_cohortesBeneficiarioResult> list = new List<management_cohortesBeneficiarioResult>();
                list = BusClass.GetCohortesBeneficiario(documento);
                list = list.OrderByDescending(x => x.id_cohorte).ToList();

                management_cohortesBeneficiarioResult Cohorte = list.FirstOrDefault();
                var regional = "";
                if (Cohorte != null)
                {
                    regional = Cohorte.regional;
                }

                var tipoSalud = "";
                if (baseB != null)
                {
                    tipoSalud = baseB.tipo_salud;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("Estimado Coordinador Regional,");
                sb.Append("<br/>");
                sb.Append("<br/>");

                string textBody = sb.ToString();

                var textMensaje = "Gusto en saludarles, nos permitimos informarles que el paciente " + nombre + " con " + tipoDoc + " " + documento + " ha egresado de hospitalización de la IPS " + ips + " con el siguiente diagnóstico " + diagnostico + " Este beneficiario estuvo hospitalizado en otra regional, pero pertenece a la suya para su importante seguimiento y gestión que aplique.";

                var textMensaje2 = "";
                if (tipoSalud != "" && tipoSalud != null)
                {
                    textMensaje2 = "Este beneficiario es: " + tipoSalud;
                }
                else
                {
                    textMensaje2 = "Este beneficiario es: " + "Sin tipo de salud";
                }
                var textMensaje3 = "Este beneficiario (a) es: pertenece a la cohorte: ";
                var textMensaje4 = "Adjuntamos Historia Clínico/administrativa del seguimiento de auditoría concurrente.";


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
                mailBody += textMensaje;
                mailBody += "<br />";
                mailBody += "<br />";

                mailBody += textMensaje2;

                mailBody += "<br />";
                if (list.Count != 0)
                {
                    mailBody += textMensaje3;
                    mailBody += Cohorte.descripcion;
                }
                else
                {
                    mailBody += textMensaje3;
                    mailBody += "Sin Cohortes.";
                }

                mailBody += "<br />";

                mailBody += textMensaje4;

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

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    mailMessage.From = new MailAddress(smtpSection.From);
                    mailMessage.To.Clear();
                    mailMessage.To.Add(correo_envio);
                    mailMessage.To.Add("asistenteecopetrol@asalud.co");
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    mailMessage.From = new MailAddress(smtpSection.From);
                    mailMessage.To.Clear();
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                }

                mailMessage.Subject = "[Mensaje Automático]" + " " + "Egreso usuario  hospitalizado en otra regional";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                mailMessage.IsBodyHtml = true;

                MemoryStream memoryStream = new MemoryStream(array);
                mailMessage.Attachments.Clear();
                mailMessage.Attachments.Add(new Attachment(memoryStream, filename));
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                Session["pdfCrear"] = 0;
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.AddModelError("", "ERROR!" + ViewBag.Message);
                Session["pdfCrear"] = null;
            }
        }

        public void CorreoDatosConcuRegionalGlosa(int idConcurrencia, String Regional)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                byte[] array = new byte[0];
                array = PdfEvoluciones(idConcurrencia);
                array = array.ToArray();
                string filename = "";

                List<management_evolucionEgresosListaResult> lista = new List<management_evolucionEgresosListaResult>();
                lista = BusClass.GetEvolucionesConcurrencia(idConcurrencia);

                management_evolucionEgresosListaResult concu = lista.FirstOrDefault();

                var documento = concu.id_afi;
                var tipoDoc = concu.tipo_documento;
                var nombre = concu.afi_nom;
                var ips = concu.nombre_ips;
                var diagnostico = concu.des_diagnostico_egreso_historia_clinica;
                var nombreMega = "";
                var documentoMega = "";
                var correo_envio = "";

                filename = tipoDoc + "_" + documento + "_" + idConcurrencia + ".pdf";

                List<management_egresBuscar_megaResult> bb = new List<management_egresBuscar_megaResult>();
                management_egresBuscar_megaResult baseB = new management_egresBuscar_megaResult();
                List<ecop_directorioPPE_correos> correos = new List<ecop_directorioPPE_correos>();

                List<Ref_regional> Lista_correo = new List<Ref_regional>();

                Lista_correo = BusClass.GetRefRegion();
                Lista_correo = Lista_correo.Where(x => x.indice == Regional).ToList();
                foreach (var item in Lista_correo)
                {
                    correo_envio = item.correo;
                }
                List<ecop_concurrencia_glosa> lstGlosa = new List<ecop_concurrencia_glosa>();
                ecop_concurrencia_glosa objGlosa = new ecop_concurrencia_glosa();
                objGlosa.id_concurrencia = idConcurrencia;
                lstGlosa = BusClass.ConsultaGlosa(objGlosa, ref MsgRes);

                bb = BusClass.BuscarMegaEgreso(documento).OrderByDescending(x => x.id_base_beneficiarios).ToList();

                if (Lista_correo.Count() != 0)
                {
                    baseB = bb.FirstOrDefault();

                    if (baseB != null)
                    {
                        nombreMega = baseB.mega_nombre;
                        documentoMega = baseB.mega_documento;

                        correos = BusClass.GetEcop_DirectorioPPE_CorreosDocumento(documentoMega);
                    }
                }

                List<management_cohortesBeneficiarioResult> list = new List<management_cohortesBeneficiarioResult>();
                list = BusClass.GetCohortesBeneficiario(documento);
                list = list.OrderByDescending(x => x.id_cohorte).ToList();

                management_cohortesBeneficiarioResult Cohorte = list.FirstOrDefault();
                var regional = "";
                if (Cohorte != null)
                {
                    regional = Cohorte.regional;
                }

                var tipoSalud = "";
                if (baseB != null)
                {
                    tipoSalud = baseB.tipo_salud;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("Estimado Coordinador Regional,");
                sb.Append("<br/>");
                sb.Append("<br/>");

                string textBody = sb.ToString();

                var textMensaje = "Gusto en saludarles, nos permitimos informale que se ha creado la siguiente glosa en SAMI concurrencia para que sea efectiva en el momento de la radicación de la factura. se trata del paciente " + nombre + " con " + tipoDoc + " " + documento + " ha egresado de hospitalización de la IPS " + ips + " con el siguiente diagnóstico " + diagnostico + ".";

                var textMensaje2 = "";
                if (tipoSalud != "" && tipoSalud != null)
                {
                    textMensaje2 = "Este beneficiario es: " + tipoSalud;
                }
                else
                {
                    textMensaje2 = "Este beneficiario es: " + "Sin tipo de salud";
                }
                var textMensaje3 = "Este beneficiario (a) es: pertenece a la cohorte: ";


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
                mailBody += textMensaje;
                mailBody += "<br />";
                mailBody += "<br />";

                mailBody += textMensaje2;

                mailBody += "<br />";
                if (list.Count != 0)
                {
                    mailBody += textMensaje3;
                    mailBody += Cohorte.descripcion;
                }
                else
                {
                    mailBody += textMensaje3;
                    mailBody += "Sin Cohortes.";
                }

                mailBody += "<br />";


                mailBody += "<br/>";
                mailBody += "<label class='text-secondary_asalud'> Glosas: </label>";
                mailBody += "<br/>";

                mailBody += "<table border='1'";
                mailBody += "style='border: solid 1px #fff; font-family: 'Century Gothic', 'Century Gothic', Sans-Serif; font-size: 9px;'>";
                mailBody += "<tr>";
                mailBody += "<td class='text-center'style='background:#636363; color:white;'><strong>Id glosa</strong></td>";
                mailBody += "<td class='text-center'style='background:#636363; color:white;'><strong>Cups</strong></td>";
                mailBody += "<td class='text-center'style='background:#636363; color:white;'><strong>Cantidad glosa</strong></td>";
                mailBody += "<td class='text-center'style='background:#636363; color:white;'><strong>Valor grolsa</strong></td>";
                mailBody += "<td class='text-center'style='background:#636363; color:white;'><strong>Observaciones</strong></td>";
                mailBody += "<td class='text-center'style='background:#636363; color:white;'><strong>Usuario</strong></td>";
                mailBody += "<td class='text-center'style='background:#636363; color:white;'><strong>Fecha digita</ strong></td>";
                mailBody += "</tr>";
                mailBody += "</thead>";
                mailBody += "<tbody>";

                foreach (var item in lstGlosa)
                {
                    mailBody += "<tr>";
                    mailBody += "<td class='text-center'style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + item.id_concurrencia_glosa + "</td>";
                    mailBody += "<td class='text-center'style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + item.id_cups + "</td>";
                    mailBody += "<td class='text-center'style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + item.cantidad_glosa + "</td>";
                    mailBody += "<td class='text-center'style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + item.Valor_Glosa + "</td>";
                    mailBody += "<td class='text-center'style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + item.observaciones_auditoria + "</td>";
                    mailBody += "<td class='text-center'style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + item.usuario_digita + "</td>";
                    mailBody += "<td class='text-center'style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + item.digita_fecha.Value.ToString("dd/MM/yyyy") + "</td>";
                    mailBody += "</tbody>";
                }
                mailBody += "</tr>";
                mailBody += "</tbody>";
                mailBody += "</table>";

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

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    mailMessage.From = new MailAddress(smtpSection.From);
                    mailMessage.To.Clear();
                    mailMessage.To.Add(correo_envio);
                    mailMessage.To.Add("asistenteecopetrol@asalud.co");
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    mailMessage.From = new MailAddress(smtpSection.From);
                    mailMessage.To.Clear();
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                }

                mailMessage.Subject = "[Mensaje Automático]" + " " + "Egreso usuario con glosa";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                mailMessage.IsBodyHtml = true;

                MemoryStream memoryStream = new MemoryStream(array);
                mailMessage.Attachments.Clear();
                mailMessage.Attachments.Add(new Attachment(memoryStream, filename));
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                Session["pdfCrear"] = 0;
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.AddModelError("", "ERROR!" + ViewBag.Message);
                Session["pdfCrear"] = null;
            }
        }

        public ActionResult TableroConcurrenciaAlertas()
        {
            try
            {
                List<management_concurrencia_alertasResult> list = new List<management_concurrencia_alertasResult>();
                list = BusClass.ConsultaConcurrenciaAlertasEvolucion();

                //if (!string.IsNullOrEmpty(alerta))
                //{
                //    Session["filtro"] = alerta;
                //    list = list.Where(X => X.Diagnostico_Egreso.Contains(alerta)).ToList();
                //}

                var conteo = list.Count();
                ViewBag.conteo = conteo;
                ViewBag.list = list;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }


        //public ActionResult TableroConcurrenciaAlertas()
        //{
        //    return View();
        //}

        //public string TableroDatos()
        //{
        //    string result = "";

        //    List<management_concurrencia_alertasResult> list = new List<management_concurrencia_alertasResult>();

        //    try
        //    {
        //        list = BusClass.ConsultaConcurrenciaAlertasEvolucion();

        //        var conteo = list.Count();
        //        ViewBag.conteo = conteo;

        //        int i = 0;

        //        if (list.Count() > 0)
        //        {
        //            foreach (var item in list)
        //            {
        //                i += 1;
        //                result += "<tr>";
        //                result += "<td>" + i + "</td>";
        //                result += "<td>" + item.id_concurrencia + "</td>";
        //                result += "<td>" + item.id_censo + "</td>";
        //                result += "<td>" + item.id_evolucion + "</td>";
        //                result += "<td>" + item.Alerta_Confirmada + "</td>";
        //                result += "<td>" + item.documento_afiliado + "</td>";
        //                result += "<td>" + item.nombrePaciente + "</td>";
        //                result += "</tr>";
        //            }
        //        }
        //        else
        //        {
        //            result += "<tr>";
        //            result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
        //            result += "<label>No hay alertas.</label>";
        //            result += "</td>";
        //            result += "</tr>";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }
        //    return result;
        //}


        public ActionResult LlenadoDatos()
        {

            List<management_concurrencia_alertasResult> list = new List<management_concurrencia_alertasResult>();
            list = BusClass.ConsultaConcurrenciaAlertasEvolucion();

            var conteo = list.Count();
            ViewBag.conteo = conteo;

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            var lista = new object();

            lista = (from item in list
                     select new
                     {
                         id_concurrencia = item.id_concurrencia,
                         id_censo = item.id_censo,
                         id_evolucion = item.id_evolucion,
                         Alerta_Confirmada = item.Alerta_Confirmada,
                         documento_afiliado = item.documento_afiliado,
                         nombrePaciente = item.nombrePaciente,

                     }).Distinct().OrderByDescending(f => f.id_concurrencia);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public void DescargarAlertas()
        {
            List<management_concurrencia_alerta_ReporteResult> List = new List<management_concurrencia_alerta_ReporteResult>();
            List = BusClass.ConsultaConcurrenciaAlertasEvolucionReporte();

            try
            {

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Alertas");

                Sheet.Cells["A1"].Value = "id_concurrencia";
                Sheet.Cells["B1"].Value = "id_censo";
                Sheet.Cells["C1"].Value = "id_evoluciónion";
                Sheet.Cells["D1"].Value = "afi_tipo_doc";
                Sheet.Cells["E1"].Value = "Documento_Afiliado";
                Sheet.Cells["F1"].Value = "afi_nom";
                Sheet.Cells["G1"].Value = "edad";
                Sheet.Cells["H1"].Value = "Diagnostico_Censo";
                Sheet.Cells["I1"].Value = "Nombre_Diagnostico_Censo";
                Sheet.Cells["J1"].Value = "Nombre_auditor";
                Sheet.Cells["K1"].Value = "CiudadIPs";
                Sheet.Cells["L1"].Value = "Nit_Ips";
                Sheet.Cells["M1"].Value = "Nombre_Ips";
                Sheet.Cells["N1"].Value = "Diagnostico_1_Evolu";
                Sheet.Cells["O1"].Value = "Nombre_Diagnostico_1_Evolu";
                Sheet.Cells["P1"].Value = "Diagnostico_2_Evolu";
                Sheet.Cells["Q1"].Value = "Nombre_Diagnostico_2_Evolu";
                Sheet.Cells["R1"].Value = "Diagnostico_3_Evolu";
                Sheet.Cells["S1"].Value = "Nombre_Diagnostico_3_Evolu";
                Sheet.Cells["T1"].Value = "Diagnostico_4_Evolu";
                Sheet.Cells["U1"].Value = "Nombre_Diagnostico_4_Evolu";
                Sheet.Cells["V1"].Value = "fecha_digita";
                Sheet.Cells["W1"].Value = "fecha_ingreso";
                Sheet.Cells["X1"].Value = "fecha_egreso";
                Sheet.Cells["Y1"].Value = "Diagnostico_Egreso";
                Sheet.Cells["Z1"].Value = "Nombre_Diagnostico_Egreso";
                Sheet.Cells["AA1"].Value = "Incapacidad";
                Sheet.Cells["AB1"].Value = "Fecha_Inicial_Incapacidad";
                Sheet.Cells["AC1"].Value = "Fecha_final_Incapacidad";
                Sheet.Cells["AD1"].Value = "Alerta_Confirmada";
                Sheet.Cells["AE1"].Value = "Tipo_Evento";
                Sheet.Cells["AF1"].Value = "Nombre_De_Alerta";
                Sheet.Cells["AG1"].Value = "Descripcion_Alerta";
                Sheet.Cells["AH1"].Value = "Diagnostico_que_genero_la_alerta";
                Sheet.Cells["AI1"].Value = "Nombre_Diagnostico_que_genero_la_alerta";
                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.id_concurrencia;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.id_censo;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.id_evolucion;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.id_afi_tipo_doc;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.documento_afiliado;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.nombrePaciente;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.afi_edad;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.Diagnostico_Censo;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.Nombre_Diagnostico_Censo;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.nombreAuditor;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.nombreCiudad;
                    Sheet.Cells[string.Format("L{0}", row)].Value = line.Nit_Ips;
                    Sheet.Cells[string.Format("M{0}", row)].Value = line.Nombre_Ips;
                    Sheet.Cells[string.Format("N{0}", row)].Value = line.Diagnostico_1_Evolu;
                    Sheet.Cells[string.Format("O{0}", row)].Value = line.Nombre_Diagnostico_1_Evolu;
                    Sheet.Cells[string.Format("P{0}", row)].Value = line.Diagnostico_2_Evolu;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = line.Nombre_Diagnostico_2_Evolu;
                    Sheet.Cells[string.Format("R{0}", row)].Value = line.Diagnostico_3_Evolu;
                    Sheet.Cells[string.Format("S{0}", row)].Value = line.Nombre_Diagnostico_3_Evolu;
                    Sheet.Cells[string.Format("T{0}", row)].Value = line.Diagnostico_4_Evolu;
                    Sheet.Cells[string.Format("U{0}", row)].Value = line.Nombre_Diagnostico_4_Evolu;
                    Sheet.Cells[string.Format("V{0}", row)].Value = line.fecha_digita;
                    Sheet.Cells[string.Format("W{0}", row)].Value = line.fecha_ingreso;
                    Sheet.Cells[string.Format("X{0}", row)].Value = line.fecha_egreso;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = line.Diagnostico_Egreso;

                    Sheet.Cells[string.Format("Z{0}", row)].Value = line.Nombre_Diagnostico_Egreso;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = line.incapacidades;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = line.fecha_inicial;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = line.fecha_final;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = line.Alerta_Confirmada;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = line.Tipo_Evento;
                    Sheet.Cells[string.Format("AF{0}", row)].Value = line.Nombre_De_Alerta;
                    Sheet.Cells[string.Format("AG{0}", row)].Value = line.Descripcion_Alerta;
                    Sheet.Cells[string.Format("AH{0}", row)].Value = line.Diagnostico_que_genero_la_alerta;
                    Sheet.Cells[string.Format("AI{0}", row)].Value = line.Nombre_genero_alerta;

                    Sheet.Cells[string.Format("V{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy";
                    Sheet.Cells[string.Format("W{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy";
                    Sheet.Cells[string.Format("X{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy";
                    Sheet.Cells[string.Format("AB{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy";
                    Sheet.Cells[string.Format("AC{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy";

                    row++;
                }

                Sheet.Cells["A:AI"].AutoFitColumns();
                Sheet.Cells["A:AI"].AutoFitColumns();
                Sheet.Cells["A1:AI1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:AI1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AI1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AI1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoAlertas" + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        //public string tableroDatos(string alerta)
        //{
        //    string result = "";


        //    List<vw_concurrencia_alertas_evolucion> list = new List<vw_concurrencia_alertas_evolucion>();
        //    list = BusClass.ConsultaConcurrenciaAlertasEvolucion();

        //    if (!string.IsNullOrEmpty(alerta))
        //    {
        //        list = list.Where(X => X.diagnostico.Contains(alerta)).ToList();
        //    }

        //    var conteo = list.Count();

        //    int i = 0;

        //    if (list.Count() > 0)
        //    {
        //        foreach (var item in list)
        //        {
        //            var valorCielo = "";

        //            i += 1;
        //            result += "<tr>";
        //            result += "<td>" + i + "</td>";
        //            result += "<td>" + item.id_concurrencia + "</td>";
        //            result += "<td>" + item.id_concurrencia + "</td>";
        //            result += "<td>" + item.id_censo + "</td>";
        //            result += "<td>" + item.diagnostico + "</td>";

        //            if (item.existencia == "0")
        //            {
        //                valorCielo = "NO";
        //            }
        //            else
        //            {
        //                valorCielo = "SI";

        //            }

        //            result += "<td>" + valorCielo + "</td>";
        //            result += "</tr>";
        //        }
        //    }
        //    else
        //    {
        //        result += "<tr>";
        //        result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
        //        result += "<label>No hay evoluciones con alerta.</label>";
        //        result += "</td>";
        //        result += "</tr>";

        //    }
        //    return result;
        //}

        //public JsonResult buscarCie10()
        //{
        //    if (string.IsNullOrEmpty(Request.Params["term"]))
        //        return null;
        //    try
        //    {
        //        string term = Request.Params["term"];
        //        if (term.Length >= 2)
        //        {
        //            List<Ref_cie10> Prestadores = BusClass.GetCie10Bycodigo(term).ToList();
        //            var lista = (from reg in Prestadores
        //                         select new
        //                         {
        //                             id = reg.id_cie10,
        //                             des = reg.des,
        //                             label = reg.des,
        //                         }).Distinct().OrderBy(f => f.label).Take(15);
        //            return Json(lista, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(null, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(null, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public int InsercionHospitalizacionPrevenible(int idEgreso)
        {
            var resultado = 0;
            List<management_egresosEvolucionesResult> listaEgreso = new List<management_egresosEvolucionesResult>();
            management_egresosEvolucionesResult egreso = new management_egresosEvolucionesResult();
            ecop_hospitalizacion_prevenible prevenible = new ecop_hospitalizacion_prevenible();
            ecop_hospitalizacion_prevenible_dtll dtll = new ecop_hospitalizacion_prevenible_dtll();
            List<management_HospitalizacionEvitable_cohortesResult> cohorte = new List<management_HospitalizacionEvitable_cohortesResult>();
            management_HospitalizacionEvitable_cohortesResult cohorteInd = new management_HospitalizacionEvitable_cohortesResult();

            var docIdentidad = "";

            try
            {
                listaEgreso = BusClass.ConsultaEgresoId(idEgreso);
                egreso = listaEgreso.OrderByDescending(x => x.id_egreso_auditoria_Hospitalaria).Take(1).FirstOrDefault();
                docIdentidad = egreso.documento;

                cohorte = BusClass.HospitalizacionPrevenible_cohortes(docIdentidad);
                cohorteInd = cohorte.OrderByDescending(x => x.id_cohorte).Take(1).FirstOrDefault();

                if (cohorteInd != null)
                {
                    prevenible.id_concurrencia = egreso.id_concurrencia;
                    prevenible.id_egreso = egreso.id_egreso_auditoria_Hospitalaria;
                    prevenible.doc_paciente = egreso.documento;
                    prevenible.diagnostico_cie10 = egreso.DxprincipalEgreso;
                    prevenible.fecha_digita = DateTime.Now;
                    prevenible.usuario_digita = SesionVar.UserName;
                    prevenible.cohorte = cohorteInd.id_cohorte;

                    resultado = BusClass.InsertarHospitalizacionPrevenible(prevenible);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return resultado;
        }

        public ActionResult TableroHospitalizacionPrevenible(string CIE10, string regional)
        {
            List<management_hospitalizacionPrevenible_TableroResult> List = new List<management_hospitalizacionPrevenible_TableroResult>();
            ViewBag.conteo = 0;
            List<ref_cohortes> cohortes = new List<ref_cohortes>();
            cohortes = BusClass.Get_refCohortes();

            ViewBag.cohorte = cohortes;

            List<Ref_regional> regio = new List<Ref_regional>();
            regio = BusClass.GetRefRegion();

            ViewBag.regional = regio;

            try
            {
                List = BusClass.GetHospitalizacionPrevenible();

                if (!string.IsNullOrEmpty(CIE10))
                {
                    List = List.Where(x => x.dx1.Equals(CIE10)).ToList();
                }

                if (!string.IsNullOrEmpty(regional))
                {
                    List = List.Where(x => x.regional == regional).ToList();
                }

                var conteo = List.Count;
                ViewBag.list = List;
                ViewBag.conteo = conteo;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }

        public ActionResult GestionHospitalizacionPrevenible(int idHE, string msg, int? rta)
        {
            try
            {
                var conteo = 0;
                ViewBag.rta = 0;
                ViewBag.msg = "";

                if (rta == 1 || rta == 2)
                {
                    ViewBag.msg = msg;
                    ViewBag.rta = rta;
                }

                management_hospitalizacionPrevenible_detalleResult datos = new management_hospitalizacionPrevenible_detalleResult();
                datos = BusClass.GetHospitalizacionPrevenibleDetalle(idHE);

                List<management_hospitalizacionPrevenible_detalle_gestionResult> lista = new List<management_hospitalizacionPrevenible_detalle_gestionResult>();
                lista = BusClass.GetHospitalizacionPrevenibleDetalle_gestion(idHE);
                conteo = lista.Count();

                ViewBag.lista = lista;
                ViewBag.conteo = conteo;
                ViewBag.idHE = idHE;

                DateTime fechaNacimiento = new DateTime();
                if (datos.fecha_nacimiento == null)
                {
                    fechaNacimiento = DateTime.Now;
                }
                else
                {
                    fechaNacimiento = (DateTime)datos.fecha_nacimiento;
                }


                var diff = DateTime.Now - fechaNacimiento;
                var dias = diff.TotalDays;
                var edadP = (int)(dias / 365.25);

                ViewBag.idConcu = datos.id_concurrencia;
                ViewBag.identificacion = datos.id_afi;
                ViewBag.nombre = datos.afi_nom;
                ViewBag.edad = edadP;
                ViewBag.direccion = datos.afi_dir;
                ViewBag.telefono = datos.afi_tel;
                ViewBag.ips = datos.nombre_ips;
                ViewBag.mega = datos.mega_documento + "-" + datos.mega_nombre;
                ViewBag.fechaIngreso = datos.fecha_ingreso;
                ViewBag.fechaEgreso = datos.fecha_egreso;
                ViewBag.dx1 = datos.diagnostico_cie10 + " - " + datos.descripcion_cie10;

                TimeSpan ts = new TimeSpan();

                if (datos.fecha_ingreso != null)
                {
                    DateTime dias1 = Convert.ToDateTime(datos.fecha_ingreso.Value.Date);
                    DateTime dias2 = Convert.ToDateTime(datos.fecha_egreso.Value.Date);
                    ts = dias2.Date - dias1.Date;
                }
                else
                {
                    DateTime dias1 = Convert.ToDateTime(DateTime.Now);
                    DateTime dias2 = Convert.ToDateTime(DateTime.Now);
                    ts = dias2.Date - dias1.Date;

                }


                int differenceInDayss = ts.Days + 1;

                ViewBag.dias_hospitalizacion = differenceInDayss;
                ViewBag.trabajador = datos.Trabajador;
                ViewBag.auditor = datos.nombre_auditor;
                ViewBag.regional = datos.regional;
                ViewBag.ciudad = datos.ciudad_b;

                List<management_cohortesBeneficiarioResult> listCohortes = new List<management_cohortesBeneficiarioResult>();
                listCohortes = BusClass.GetCohortesBeneficiario(datos.id_afi);

                var conteoCohortes = listCohortes.Count();
                ViewBag.listaCohortes = listCohortes;
                ViewBag.conteoCohorte = conteoCohortes;

                List<ref_he_analisisCaso> analisis = new List<ref_he_analisisCaso>();
                analisis = BusClass.ListAnalisisCasoHE();

                List<ref_he_analisisCaso_si> analisisSi = new List<ref_he_analisisCaso_si>();
                analisisSi = BusClass.ListAnalisisCasoHESi();

                List<ref_he_analisisCaso_no> analisisNo = new List<ref_he_analisisCaso_no>();
                analisisNo = BusClass.ListAnalisisCasoHENo();

                ViewBag.analisis = analisis;
                ViewBag.analisisSi = analisisSi;
                ViewBag.analisisNo = analisisNo;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }

        [HttpPost]
        public ActionResult GestionHospitalizacionPrevenible(int? idHE, int? idConcu, string identificacion, string nombre, int? edad, string direccion, string telefono, string ips, string mega, string dx1,
            DateTime? fechaIngreso, DateTime? fechaEgreso, int? dias_hospitalizacion, string trabajador, string auditor, string ambulatorio, int? analisis, int? analisisSi, int? analisisNo,
            HttpPostedFileBase file, string descripcionArchivo, string regional, string ciudad)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                ecop_hospitalizacion_prevenible_dtll obj = new ecop_hospitalizacion_prevenible_dtll();

                obj.id_HE = idHE;
                obj.id_concurrencia = idConcu;
                obj.num_identificacion = identificacion;
                obj.nombre_paciente = nombre;
                obj.edad = edad;
                obj.direccion = direccion;
                obj.telefono = telefono;
                obj.ips = ips;
                obj.mega = mega;
                obj.dx1 = dx1;
                obj.fecha_ingreso = fechaIngreso;
                obj.fecha_egreso = fechaEgreso;
                obj.dias_hospitalizacion = dias_hospitalizacion;
                obj.trabajador = trabajador;
                obj.auditor = auditor;
                obj.concepto_ambulatorio = ambulatorio;
                obj.analisis = analisis;
                obj.analisis_si = analisisSi;
                obj.analisis_no = analisisNo;
                obj.regional = regional;
                obj.ciudad = ciudad;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;


                var respuesta = BusClass.InsertarHospitalizacionPrevenibleDtll(obj);
                if (respuesta != 0)
                {
                    HttpPostedFileBase Files;
                    Files = Request.Files[0];

                    if (Files.ContentLength > 0)
                    {
                        string strInfo = string.Empty;
                        strInfo = cargarArchivosHE((int)idHE, respuesta, descripcionArchivo, file);
                        if (strInfo == string.Empty)
                        {
                            mensaje = "SE INGRESO CORRECTAMENTE.";
                            rta = 1;
                            return RedirectToAction("GestionHospitalizacionPrevenible", "Concurrencia", new { idHE = idHE, rta = rta });

                        }
                        else
                        {
                            mensaje = "SE INGRESO CORRECTAMENTE. PROBLEMAS CON EL ARCHIVO";
                            rta = 1;
                            return RedirectToAction("GestionHospitalizacionPrevenible", "Concurrencia", new { idHE = idHE, rta = rta });

                        }
                    }
                    else
                    {
                        mensaje = "SE INGRESO CORRECTAMENTE.";
                        rta = 1;
                        return RedirectToAction("GestionHospitalizacionPrevenible", "Concurrencia", new { idHE = idHE, rta = rta });

                    }
                }
                else
                {
                    mensaje = "ERROR AL INSERTAR EL DETALLE DE HOSPITALIZACIÓN EVITABLE.";
                    rta = 2;
                    return RedirectToAction("GestionHospitalizacionPrevenible", "Concurrencia", new { idHE = idHE, rta = rta, msg = mensaje });

                }


            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR - " + ex.Message;
                rta = 2;
                return RedirectToAction("GestionHospitalizacionPrevenible", "Concurrencia", new { idHE = idHE, rta = rta, msg = mensaje });

            }

        }
        private string cargarArchivosHE(int idHE, int HE_dtll, string descripcionArchivo, HttpPostedFileBase file)
        {
            string strRetorno = string.Empty;

            try
            {

                if (Request.Files["file"].ContentLength > 0)
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = Request.Files["file"].FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    string[] formatos = new string[] { "png", "pdf", "jpg", "xlsx", "xls", "jpeg", "rar", "zip", "docx", "pptx", "Zip", "Rar", "rar" };
                    if (Array.IndexOf(formatos, ext) < 0)
                    {
                        strRetorno = "Formato de archivo inválido.";
                    }
                    else
                    {
                        strRetorno = GuardarArchivoHE(idHE, HE_dtll, descripcionArchivo, file);
                    }
                }
            }
            catch (Exception ex)
            {
                strRetorno = ex.Message;
            }
            return strRetorno;
        }

        private string GuardarArchivoHE(int HE, int HE_dtll, string descripcionArchivo, HttpPostedFileBase file)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaArchivosHE"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaArchivosHE"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "Registro " + HE;
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "Registro_pruebas " + HE;
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1}_{2}{3}", ruta,
                HE_dtll, nombre, Path.GetExtension(Request.Files["file"].FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                ecop_HE_gestion_documental OBJ = new ecop_HE_gestion_documental();

                OBJ.id_he = HE;
                OBJ.id_he_dtll = HE_dtll;
                OBJ.nombre = nombre;
                OBJ.extension = file.ContentType;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.descripcion = descripcionArchivo;
                OBJ.fecha_digita = DateTime.Now;
                OBJ.usuario_digita = SesionVar.UserName;

                var respuesta = BusClass.InsertarArchivoHospitalziacionEvitable(OBJ);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                strError = error;
            }

            return strError;
        }

        public void ReporteHETableroControl()
        {
            List<management_hospitalizacionPrevenible_reporteResult> List = new List<management_hospitalizacionPrevenible_reporteResult>();
            List = BusClass.GetHospitalizacionPrevenible_Reporte();

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte HE");

                Sheet.Cells["A1"].Value = "Id Hospitalización prevenible";
                Sheet.Cells["B1"].Value = "Id concurrencia";
                Sheet.Cells["C1"].Value = "Id egreso";
                Sheet.Cells["D1"].Value = "Documento paciente";
                Sheet.Cells["E1"].Value = "Nombre paciente";
                Sheet.Cells["F1"].Value = "Cohorte";

                Sheet.Cells["G1"].Value = "Id detalle";
                Sheet.Cells["H1"].Value = "Diagnostico CIE10";
                Sheet.Cells["I1"].Value = "Edad";
                Sheet.Cells["J1"].Value = "Dirección";
                Sheet.Cells["K1"].Value = "Telefono";
                Sheet.Cells["L1"].Value = "Nombre IPS";
                Sheet.Cells["M1"].Value = "MEGA";
                Sheet.Cells["N1"].Value = "Fecha ingreso";
                Sheet.Cells["O1"].Value = "Fecha egreso";
                Sheet.Cells["P1"].Value = "Dias hospitalización";
                Sheet.Cells["Q1"].Value = "Trabajador";
                Sheet.Cells["R1"].Value = "Nombre auditor";
                Sheet.Cells["S1"].Value = "Concepto de auditor ambulatorio";
                Sheet.Cells["T1"].Value = "Analisis de caso";
                Sheet.Cells["U1"].Value = "Analisis de caso SI";
                Sheet.Cells["V1"].Value = "Analisis de caso NO";

                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.id_HE;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.id_concurrencia;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.id_egreso;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.doc_paciente;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.nombre_paciente;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.cohorte;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.int_HE_Dtll;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.diagnostico_cie10;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.edad;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.direccion;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.telefono;
                    Sheet.Cells[string.Format("L{0}", row)].Value = line.ips;
                    Sheet.Cells[string.Format("M{0}", row)].Value = line.mega;
                    Sheet.Cells[string.Format("N{0}", row)].Value = line.fecha_ingreso;
                    Sheet.Cells[string.Format("O{0}", row)].Value = line.fecha_egreso;
                    Sheet.Cells[string.Format("P{0}", row)].Value = line.dias_hospitalizacion;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = line.trabajador;
                    Sheet.Cells[string.Format("R{0}", row)].Value = line.auditor;
                    Sheet.Cells[string.Format("S{0}", row)].Value = line.concepto_ambulatorio;
                    Sheet.Cells[string.Format("T{0}", row)].Value = line.descripcion_analisis;
                    Sheet.Cells[string.Format("U{0}", row)].Value = line.descripcion_analisis_si;
                    Sheet.Cells[string.Format("V{0}", row)].Value = line.descripcion_analisis_no;

                    Sheet.Cells[string.Format("N{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy";
                    Sheet.Cells[string.Format("O{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy";

                    row++;
                }

                Sheet.Cells["A:V"].AutoFitColumns();
                Sheet.Cells["A:V"].AutoFitColumns();
                Sheet.Cells["A1:V1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:V1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:V1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoHE - " + DateTime.Now + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void ReporteHETableroControlIndividual(int id_HE)
        {
            List<management_hospitalizacionPrevenible_detalle_gestionResult> List = new List<management_hospitalizacionPrevenible_detalle_gestionResult>();
            List = BusClass.GetHospitalizacionPrevenibleDetalle_gestion(id_HE);

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte HE individual");

                Sheet.Cells["A1"].Value = "Id Hospitalización prevenible";
                Sheet.Cells["B1"].Value = "Id concurrencia";
                Sheet.Cells["C1"].Value = "Id egreso";
                Sheet.Cells["D1"].Value = "Documento paciente";
                Sheet.Cells["E1"].Value = "Nombre paciente";
                Sheet.Cells["F1"].Value = "Cohorte";

                Sheet.Cells["G1"].Value = "Id detalle";
                Sheet.Cells["H1"].Value = "Diagnostico CIE10";
                Sheet.Cells["I1"].Value = "Edad";
                Sheet.Cells["J1"].Value = "Dirección";
                Sheet.Cells["K1"].Value = "Telefono";
                Sheet.Cells["L1"].Value = "Nombre IPS";
                Sheet.Cells["M1"].Value = "MEGA";
                Sheet.Cells["N1"].Value = "Fecha ingreso";
                Sheet.Cells["O1"].Value = "Fecha egreso";
                Sheet.Cells["P1"].Value = "Dias hospitalización";
                Sheet.Cells["Q1"].Value = "Trabajador";
                Sheet.Cells["R1"].Value = "Nombre auditor";
                Sheet.Cells["S1"].Value = "Concepto de auditor ambulatorio";
                Sheet.Cells["T1"].Value = "Analisis de caso";
                Sheet.Cells["U1"].Value = "Analisis de caso SI";
                Sheet.Cells["V1"].Value = "Analisis de caso NO";

                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.id_HE;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.id_concurrencia;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.id_egreso;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.doc_paciente;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.nombre_paciente;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.cohorte;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.int_HE_Dtll;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.diagnostico_cie10;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.edad;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.direccion;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.telefono;
                    Sheet.Cells[string.Format("L{0}", row)].Value = line.ips;
                    Sheet.Cells[string.Format("M{0}", row)].Value = line.mega;
                    Sheet.Cells[string.Format("N{0}", row)].Value = line.fecha_ingreso;
                    Sheet.Cells[string.Format("O{0}", row)].Value = line.fecha_egreso;
                    Sheet.Cells[string.Format("P{0}", row)].Value = line.dias_hospitalizacion;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = line.trabajador;
                    Sheet.Cells[string.Format("R{0}", row)].Value = line.auditor;
                    Sheet.Cells[string.Format("S{0}", row)].Value = line.concepto_ambulatorio;
                    Sheet.Cells[string.Format("T{0}", row)].Value = line.descripcion_analisis;
                    Sheet.Cells[string.Format("U{0}", row)].Value = line.descripcion_analisis_si;
                    Sheet.Cells[string.Format("V{0}", row)].Value = line.descripcion_analisis_no;

                    Sheet.Cells[string.Format("N{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy";
                    Sheet.Cells[string.Format("O{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy";

                    row++;
                }

                Sheet.Cells["A:V"].AutoFitColumns();
                Sheet.Cells["A:V"].AutoFitColumns();
                Sheet.Cells["A1:V1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:V1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:V1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoHE" + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }
        public ActionResult verArchivoHE(int idDtll)
        {
            ecop_HE_gestion_documental obj = new ecop_HE_gestion_documental();

            try
            {
                obj = BusClass.buscarArchivoHEDtll(idDtll);

                string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                WebClient User = new WebClient();
                obj.ruta = dirpath;
                string filename = obj.ruta;

                var tipo = obj.extension;
                var nombre = obj.nombre;

                Byte[] FileBuffer = User.DownloadData(filename);

                if (FileBuffer != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = tipo;
                    //Response.ContentType = "application/pdf";

                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    //Response.AddHeader("content-length", String.Format("inline; filename={0}.pdf", Path.GetFileName(nombre)));
                    Response.BinaryWrite(FileBuffer);
                    Response.Flush();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }

        public ActionResult VigilanciaEpidemiologica(int? rta, string msg)
        {
            try
            {


                ViewBag.rta = 0;
                ViewBag.msg = "";

                if (rta == 1 || rta == 2)
                {
                    ViewBag.msg = msg;
                    ViewBag.rta = rta;
                }

                List<Ref_regional> regional = new List<Ref_regional>();
                regional = BusClass.GetRefRegion();

                List<Ref_ips> ips = new List<Ref_ips>();
                ips = BusClass.GetRefIps();

                List<Ref_cie10> cie10 = new List<Ref_cie10>();
                cie10 = BusClass.GetCie10();

                List<sis_usuario> auditores = BusClass.GetUsuarios().ToList();
                auditores = auditores.Where(l => l.id_rol == 7).ToList();
                auditores = auditores.Where(l => l.id_estado == 1).ToList();


                List<ref_cohortes> cohortes = new List<ref_cohortes>();
                cohortes = BusClass.Get_refCohortes();

                ViewBag.ips = ips;
                ViewBag.regional = regional;
                ViewBag.CIE10 = cie10;
                ViewBag.auditores = auditores;
                ViewBag.cohortes = cohortes;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }


        [HttpPost]
        public ActionResult VigilanciaGuardar(string documento, string nombre, int? ips, string mega, int? regional, int? unis, int? ciudad, string cie10, string trabajador,
            int? auditor, int? cohorte, HttpPostedFileBase file, HttpPostedFileBase fileSoporte, string descripcionArchivo)
        {

            var mensaje = "";
            var rta = 0;
            try
            {
                ecop_vigilancia_epidemiologica obj = new ecop_vigilancia_epidemiologica();
                obj.documento_paciente = documento;
                obj.nombre_paciente = nombre;
                obj.id_ips = ips;
                obj.mega = mega;
                obj.regional = regional;
                obj.unis = unis;
                obj.ciudad = ciudad;
                obj.id_cie10 = cie10;
                obj.trabajador_ecopetrol = trabajador;
                obj.auditor_asignado = auditor;
                obj.cohorte = cohorte;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                var resultado = BusClass.InsertarVigilanciaEpidemiologica(obj);
                if (resultado != 0)
                {
                    if (file != null || fileSoporte != null)
                    {
                        if (file != null)
                        {
                            string strInfo = string.Empty;
                            strInfo = cargarArchivosVE(resultado, descripcionArchivo, 1, file);
                            if (strInfo == string.Empty)
                            {
                                mensaje = "SE INGRESO CORRECTAMENTE.";
                                rta = 1;
                            }
                            else
                            {
                                mensaje = "SE INGRESO CORRECTAMENTE. PROBLEMAS CON EL ARCHIVO";
                                rta = 1;
                            }
                        }
                        if (fileSoporte != null)
                        {
                            string strInfo = string.Empty;
                            strInfo = cargarArchivosVE(resultado, descripcionArchivo, 2, fileSoporte);
                            if (strInfo == string.Empty)
                            {
                                mensaje = "SE INGRESO CORRECTAMENTE.";
                                rta = 1;
                            }
                            else
                            {
                                mensaje = "SE INGRESO CORRECTAMENTE. PROBLEMAS CON EL ARCHIVO SOPORTE";
                                rta = 1;
                            }
                        }
                        return RedirectToAction("VigilanciaEpidemiologica", "Concurrencia", new { rta = rta, msg = mensaje });
                    }
                    else
                    {
                        mensaje = "SE INGRESO CORRECTAMENTE.";
                        rta = 1;
                        return RedirectToAction("VigilanciaEpidemiologica", "Concurrencia", new { rta = rta });
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                rta = 2;
                mensaje = "ERROR EN EL INGRESO";
                return RedirectToAction("VigilanciaEpidemiologica", "Concurrencia", new { rta = rta, msg = mensaje });
            }

            return View();
        }

        private string cargarArchivosVE(int idVE, string descripcionArchivo, int tipo, HttpPostedFileBase file)
        {
            string strRetorno = string.Empty;

            try
            {
                if (file.ContentLength > 0)
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = file.FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    string[] formatos = new string[] { "png", "pdf", "jpg", "xlsx", "xls", "jpeg", "rar", "zip", "docx", "pptx", "Zip", "Rar", "rar", "doc" };
                    if (Array.IndexOf(formatos, ext) < 0)
                    {
                        strRetorno = "Formato de archivo inválido.";
                    }
                    else
                    {
                        strRetorno = GuardarArchivoVE(idVE, descripcionArchivo, tipo, file);
                    }
                }
            }
            catch (Exception ex)
            {
                strRetorno = ex.Message;
            }
            return strRetorno;
        }

        private string GuardarArchivoVE(int idVE, string descripcionArchivo, int tipo, HttpPostedFileBase file)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaArchivosVE"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaArchivosVE"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "Registro " + idVE;
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "Registro_pruebas " + idVE;
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1}_{2}{3}", ruta,
                idVE, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                ecop_VE_gestion_documental OBJ = new ecop_VE_gestion_documental();

                OBJ.id_VE = idVE;
                OBJ.nombre = nombre;
                OBJ.extension = file.ContentType;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.descripcion = descripcionArchivo;
                OBJ.tipo = tipo;
                OBJ.fecha_digita = DateTime.Now;
                OBJ.usuario_digita = SesionVar.UserName;

                var respuesta = BusClass.InsertarVigilanciaEpidemiologica_archivos(OBJ);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                strError = error;
            }

            return strError;
        }

        public string ObtenerUnis(int idregional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            try
            {
                var regional = BusClass.GetRefRegion().Where(l => l.id_ref_regional == idregional).FirstOrDefault();
                List<Ref_odont_unis> Unis = BusClass.Odont_unis().Where(l => l.id_regional == regional.id_ref_regional).ToList();
                foreach (var item in Unis)
                {
                    result += "<option value='" + item.id_ref_unis + "'>" + item.descripcion + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public string ObtenerCiudades(int idunis)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            try
            {
                Ref_odont_unis unis = BusClass.Odont_unis().Where(l => l.id_ref_unis == idunis).FirstOrDefault();

                List<Ref_ciudades> Ciudades = BusClass.GetCiudades().Where(l => l.id_ref_odont_unis == unis.id_ref_unis).ToList();
                foreach (var item in Ciudades)
                {
                    result += "<option value='" + item.id_ref_ciudades + "'>" + item.nombre + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public ActionResult TableroVigilanciaEpidemiologica(int? regional, int? cohorte, int? unis, int? ciudad, string cie10)
        {

            try
            {

                List<management_vigilancia_epidemiologica_tableroResult> list = new List<management_vigilancia_epidemiologica_tableroResult>();
                list = BusClass.GetVigilanciaEpidemiologica();

                if (regional != null)
                {
                    list = list.Where(x => x.regional == regional).ToList();
                }
                if (cohorte != null)
                {
                    list = list.Where(x => x.cohorte == cohorte).ToList();
                }

                if (regional != null)
                {
                    list = list.Where(x => x.regional == regional).ToList();
                }

                if (unis != null)
                {
                    list = list.Where(x => x.unis == unis).ToList();
                }

                if (ciudad != null)
                {
                    list = list.Where(x => x.ciudad == ciudad).ToList();
                }

                if (!string.IsNullOrEmpty(cie10))
                {
                    list = list.Where(x => x.id_cie10 == cie10).ToList();
                }

                var conteo = list.Count();

                ViewBag.list = list;
                ViewBag.conteo = conteo;

                List<Ref_regional> regionales = new List<Ref_regional>();
                regionales = BusClass.GetRefRegion();
                List<ref_cohortes> cohortes = new List<ref_cohortes>();
                cohortes = BusClass.Get_refCohortes();

                ViewBag.regional = regionales;
                ViewBag.cohortes = cohortes;

                Session["tableroVEcontrol"] = list;
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }
        public ActionResult verArchivoVE(int idVE, int tipo)
        {
            ecop_VE_gestion_documental obj = new ecop_VE_gestion_documental();

            try
            {
                obj = BusClass.buscarArchivoVE(idVE, tipo);

                string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                WebClient User = new WebClient();
                obj.ruta = dirpath;
                string filename = obj.ruta;

                var extension = obj.extension;
                var nombre = obj.nombre;

                Byte[] FileBuffer = User.DownloadData(filename);

                if (FileBuffer != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = extension;
                    //Response.ContentType = "application/pdf";

                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    //Response.AddHeader("content-length", String.Format("inline; filename={0}.pdf", Path.GetFileName(nombre)));
                    Response.BinaryWrite(FileBuffer);
                    Response.Flush();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }
        public void ReporteVETableroControl()
        {
            List<management_vigilancia_epidemiologica_tableroResult> List = (List<management_vigilancia_epidemiologica_tableroResult>)Session["tableroVEcontrol"];

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte VE");

                Sheet.Cells["A1"].Value = "Id Vigilancia Epidemiologia";
                Sheet.Cells["B1"].Value = "Documento Paciente";
                Sheet.Cells["C1"].Value = "Nombre Paciente";
                Sheet.Cells["D1"].Value = "IPS";
                Sheet.Cells["E1"].Value = "Nombre IPS";
                Sheet.Cells["F1"].Value = "Mega";
                Sheet.Cells["G1"].Value = "Regional";
                Sheet.Cells["H1"].Value = "Unis";
                Sheet.Cells["I1"].Value = "Ciudad";
                Sheet.Cells["J1"].Value = "CIE10";
                Sheet.Cells["K1"].Value = "Trabajador Ecopetrol";
                Sheet.Cells["L1"].Value = "Auditor Asignado";
                Sheet.Cells["M1"].Value = "Cohorte";
                Sheet.Cells["N1"].Value = "Fecha Creación";
                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.id_vigilancia;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.documento_paciente;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.nombre_paciente;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.id_ips;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.Nombre_ips;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.mega;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.nombre_regional;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.nombre_unis;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.nombre_ciudad;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.des;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.trabajador_ecopetrol;
                    Sheet.Cells[string.Format("L{0}", row)].Value = line.nombre_auditor;
                    Sheet.Cells[string.Format("M{0}", row)].Value = line.nombre_cohorte;
                    Sheet.Cells[string.Format("N{0}", row)].Value = line.fecha_digita;

                    Sheet.Cells[string.Format("N{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy hh:mm:ss";

                    row++;
                }

                Sheet.Cells["A:N"].AutoFitColumns();
                Sheet.Cells["A:N"].AutoFitColumns();
                Sheet.Cells["A1:N1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:N1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:N1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoVE - " + DateTime.Now + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public JsonResult BuscarCIE10(string value)
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];

                if (term.Length >= 1)
                {
                    List<Ref_cie10> cie10 = new List<Ref_cie10>();
                    cie10 = BusClass.GetCie10Bycodigo(term).ToList();

                    var lista = (from a in cie10
                                 select new
                                 {
                                     cie10 = a.id_cie10,
                                     descripcion = a.des,
                                     label = a.id_cie10 + "-" + a.des,

                                 }).Distinct().OrderBy(f => f.label).Take(15);
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult _CriterioIngreso(String idConcu)
        {
            Models.Concurrencia.Concurrencia Model = new Models.Concurrencia.Concurrencia();

            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.ConsultaIdConcurrenia(Convert.ToInt32(idConcu));
                Model.id_concurrencia = (Convert.ToInt32(idConcu));

                ViewBag.usuario = SesionVar.ROL;

                return PartialView(Model);
            }

            else
            {
                return PartialView();
            }
        }

        public JsonResult _GuardarCriterioIngreso(Models.Concurrencia.Concurrencia Model)
        {
            Boolean bolValida = true;

            if (Model.lesiones_severas == false)
            {
                ModelState.Remove("id_lesiones_severas");
            }

            //if (Model.lesiones_severas == true && Model.id_lesiones_severas == 0)
            //{
            //    bolValida = false;
            //}
            if (bolValida == true)
            {
                ecop_concurrencia objConcu = new ecop_concurrencia();
                objConcu.id_concurrencia = Model.id_concurrencia;
                objConcu.afi_tipo_doc = Model.afi_tipoDoc;
                objConcu.id_afi = Model.afi_NumDoc;
                objConcu.afi_nom = Model.afi_Nom;
                objConcu.afi_edad = Model.afi_Edad;
                objConcu.Genero = Model.genero;
                objConcu.afi_dir = Model.afi_Dir;
                objConcu.afi_tel = Model.afi_tel1;
                objConcu.afi_cel = Model.afi_cel;
                objConcu.afi_contacto_nom = Model.contacto_paciente;
                objConcu.afi_contacto_cel = Model.contacto_celular;
                objConcu.id_ips = Model.id_ips;
                objConcu.id_reingreso = Model.id_reingreso;
                objConcu.id_origen_hospitalizacion = Model.id_origen_hospitalizacion;
                if (Model.id_reingreso == 9)
                {
                    objConcu.otro_reingreso = Model.otro_reingreso;
                }
                else
                {
                }

                objConcu.servicio = Model.id_servicio_tratante;
                objConcu.med_ser_trata = Model.nombre_medico_tratante;


                if (objConcu.hospitalizacion_prevenible == null)
                {
                    objConcu.hospitalizacion_prevenible = "";
                }
                else
                {
                    objConcu.hospitalizacion_prevenible = Model.hospitalizacion_prevenible;

                }

                if (Model.hospitalizacion_prevenible == "SI")
                {
                    objConcu.descripcion_prevenible = Model.descripcion_prevenible;
                }
                else
                {
                    objConcu.descripcion_prevenible = "NA";
                }

                if (Model.lesiones_severas == Convert.ToBoolean(true))
                {
                    objConcu.lesion_severa = "SI";
                    objConcu.id_lesion_severa = Model.id_lesiones_severas;
                }
                else
                {
                    objConcu.lesion_severa = "NO";
                    objConcu.id_lesion_severa = 0;
                }

                objConcu.reingreso = Model.reingreso;
                objConcu.dx1 = Model.id_cie10_1;
                objConcu.id_editor = SesionVar.IDUser;

                if (Model.Gestantes == "undefined")
                {
                    objConcu.Gestantes = "";
                }
                else
                {
                    objConcu.Gestantes = Model.Gestantes;

                }

                objConcu.Trabajador = Model.Trabajador;
                if (Model.Trabajador == "SI")
                {
                    objConcu.ciudad_trabajador = Model.ciudad_trabajador;
                }
                else
                {
                    objConcu.ciudad_trabajador = "0";
                }
                objConcu.triage = Model.triage;
                objConcu.auditoria_telefonica = Model.auditoria_telefonica;

                if (Model.salud_publica == Convert.ToBoolean(true))
                {
                    objConcu.salud_publica = "SI";
                    objConcu.id_salud_publica = Model.id_salud_publica;
                }
                else
                {
                    objConcu.salud_publica = "NO";
                    objConcu.id_salud_publica = 0;
                }

                if (Model.id_salud_publica == 10)
                {
                    objConcu.otro_salud_publica = Model.otroSalud;
                }
                else
                {
                    objConcu.otro_salud_publica = "";
                }

                Model.ActualizaConcurrencia(objConcu, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    //if (Model.lesiones_severas == true)
                    //{
                    //    if (Model.id_ciudadIPS == 1)
                    //    {
                    //        //  EnvioCorreoDireccionProyectos(Model.afi_tipoDoc, Model.afi_NumDoc, Model.Desips, Model.DesAlto);
                    //    }
                    //    else
                    //    {
                    //    }
                    //}
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult MandarDatosAContactCenter(List<int> datos)
        {
            var mensaje = "";

            try
            {
                BusClass.MandarConcurrenciaContactCenter(datos, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    log_concurrenciaEnviada_contactCenter log = new log_concurrenciaEnviada_contactCenter();
                    List<log_concurrenciaEnviada_contactCenter> logList = new List<log_concurrenciaEnviada_contactCenter>();
                    log.usuario_digita = SesionVar.UserName;
                    log.fecha_digita = DateTime.Now;

                    foreach (var item in datos)
                    {
                        log.id_concurrencia = item;
                        logList.Add(log);
                    }

                    BusClass.InsertarLogConcurrenciaEnviadaCallCenter(logList, ref MsgRes);

                    mensaje = "CONCURRENCIAS MANDADAS A CONTACT CENTER CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR EN LA GESTIÓN: " + MsgRes.DescriptionResponse;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA GESTIÓN: " + MsgRes.DescriptionResponse;
            }

            return Json(new { mensaje = mensaje });
        }


        public JsonResult MandarCasoIndividualContact(int? idConcu, string observaciones)
        {
            var mensaje = "";

            try
            {
                BusClass.MandarindividualConcurrenciaContactCenter(idConcu, observaciones, SesionVar.IDUser, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    log_concurrenciaEnviada_contactCenter log = new log_concurrenciaEnviada_contactCenter();
                    log.usuario_digita = SesionVar.UserName;
                    log.fecha_digita = DateTime.Now;
                    log.id_concurrencia = idConcu;
                    log.observacion_contact = observaciones;

                    mensaje = "CONCURRENCIA MANDADA A CONTACT CENTER CORRECTAMENTE";

                    BusClass.InsertarLogindividualConcurrenciaEnviadaCallCenter(log, ref MsgRes);
                }
                else
                {
                    mensaje = "ERROR EN LA GESTIÓN: " + MsgRes.DescriptionResponse;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA GESTIÓN: " + MsgRes.DescriptionResponse;
            }

            return Json(new { mensaje = mensaje });
        }

    }
}
