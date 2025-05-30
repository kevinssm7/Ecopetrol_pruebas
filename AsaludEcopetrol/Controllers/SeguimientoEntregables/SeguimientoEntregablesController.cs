using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using System.IO;
using System.Data.Linq;
using System.Configuration;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Net.Configuration;

namespace AsaludEcopetrol.Controllers.SeguimientoEntregables
{
    public class SeguimientoEntregablesController : Controller
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

        public ActionResult TableroControlEntregables()
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<seguimiento_entregables> List = Model.GetListSeguimientoEntregable();
            ViewBag.personaresponsable = Model.GetResponsablesList();
            return View(List);
        }

        /// <summary>
        /// Formulario para agregar un entregable
        /// </summary>
        /// <returns></returns>
        public ActionResult AgregarEntregable(int? idSeguimientoEntregable)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();

            ViewBag.periodicidad = Model.GetListPeriodicidadEntregables();
            ViewBag.procesos = Model.Getprocesoentregable();
            ViewBag.tiposegentregable = Model.GetListTipoEntregable();
            ViewBag.personaresponsable = Model.GetResponsablesList();

            //ViewBag.regionales = BusClass.GetRefRegion();

            ViewData["rta"] = 0;
            ViewData["Msg"] = "";

            if (idSeguimientoEntregable == null)
            {
                return View(new seguimiento_entregables());
            }
            else
            {
                return View(Model.GetSeguimientoEntregable(idSeguimientoEntregable.Value));
            }
        }

        /// <summary>
        /// /Metodo post para agregar un nuevo entregable
        /// </summary>
        /// <param name="periodicidad_entrega"></param>
        /// <param name="nomentregable"></param>
        /// <param name="proceso"></param>
        /// <param name="tipo_entregable"></param>
        /// <param name="regional"></param>
        /// <param name="persona_responsable"></param>
        /// <param name="Fechainicio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AgregarEntregable(int idSegEntregable, int periodicidad_entrega, string nomentregable, int proceso, int tipo_entregable, string persona_responsable, DateTime Fechainicio)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables(); ViewBag.periodicidad = Model.GetListPeriodicidadEntregables();

            ViewBag.periodicidad = Model.GetListPeriodicidadEntregables();
            ViewBag.procesos = Model.Getprocesoentregable();
            ViewBag.tiposegentregable = Model.GetListTipoEntregable();
            ViewBag.personaresponsable = Model.GetResponsablesList();

            seguimiento_entregables obj = new seguimiento_entregables();
            obj.id_seg_entregables = idSegEntregable;
            obj.cobertura_seg_entregable = 1;
            obj.id_ref_regional = null;
            obj.Nom_entregable = nomentregable;
            obj.Proceso = proceso;
            obj.periodicidad_entrega = periodicidad_entrega;
            obj.persona_responsable = persona_responsable;
            obj.fecha_control = Fechainicio;
            obj.id_tipo_entregable = tipo_entregable;

            Model.InsertarOActualizarSeguimientoEntregable(obj, ref MsgRes);

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                ViewData["rta"] = 1;
            }
            else
            {
                ViewData["rta"] = 2;
            }
            ViewData["Msg"] = MsgRes.DescriptionResponse;

            if (idSegEntregable == 0)
            {
                return View(new seguimiento_entregables());
            }
            else
            {
                return View(Model.GetSeguimientoEntregable(idSegEntregable));
            }
        }

        /// <summary>
        /// Tablero seguimiento entregables
        /// </summary>
        /// <param name="rta"></param>
        /// <returns></returns>
        public ActionResult TableroSeguimientoEntregables(int? rta)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            ViewData["rta"] = rta;
            ViewData["msg"] = "";

            try
            {
                string mensaje = (string)TempData["mensaje"];
                if (!string.IsNullOrEmpty(mensaje))
                {
                    ViewData["msg"] = mensaje;
                }

                ViewBag.tipoEntregable = Model.GetListTipoEntregable();
                ViewBag.estadoEntregable = Model.GetListEstadoEntregable().Where(l => l.estado == 'A').ToList();
                ViewBag.PeriodicidadEntregable = Model.GetListPeriodicidadEntregables();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        public PartialViewResult _PintarfiltosColoresContadorEntregables(int? periodicidad, int? estado, int? tipoEntregable, string periodoFechaLimite, string periodoFechaEntrega)
        {
            if (periodicidad == 0)
            {
                periodicidad = null;
            }

            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<vw_seguimiento_entregables> model = Model.GetListEntregables(periodicidad).ToList();

            /*Se filtran los entregables dependiendo del rol*/
            if (SesionVar.ROL != "1" && SesionVar.ROL != "35")
            {
                model = model.Where(l => l.persona_responsable == SesionVar.UserName).ToList();
            }

            int enTiempos = 0;
            int proxAVencer = 0;
            int vencidos = 0;
            int aceptados = 0;
            int fechaLimiteAmpliada = 0;

            if (model.Count > 0)
            {
                /*entregables que contengan estado (1,2,3) , hayan sido presentados en tiempos y no tengan ampliacion*/
                enTiempos += model.Where(l => (l.id_estado_entregable != 3 && l.id_estado_entregable != 7) && l.diferencia_dias > 2 && (l.tiene_ampliacion == false || l.tiene_ampliacion == null)).Count();

                /*Entregables los cuales el estado sea direfente a ACEPTADO, los dias a vencer esten entre 1 y 2 y que no tengan ampliacion*/
                proxAVencer += model.Where(l => (l.id_estado_entregable != 3 && l.id_estado_entregable != 7) && (l.diferencia_dias <= 2 && l.diferencia_dias >= 0) && (l.tiene_ampliacion == false || l.tiene_ampliacion == null)).Count();

                /*Entregables los cuales esl estadse sea diferente a aceptado y ampliado. ademas que esten vencidos y que no tengan ampliacion*/
                vencidos += model.Where(l => (l.id_estado_entregable != 3 && l.id_estado_entregable != 7) && l.diferencia_dias < 0 && (l.tiene_ampliacion == false || l.tiene_ampliacion == null)).Count();

                /*Entregables con la fecha limite ampliada*/
                fechaLimiteAmpliada += model.Where(l => l.id_estado_entregable == 7 || (l.id_estado_entregable != 3 && l.tiene_ampliacion == true)).Count();

                /*Entregables aceptados*/
                aceptados += model.Where(l => l.id_estado_entregable == 3).Count();
            }

            ViewBag.oportunos = enTiempos;
            ViewBag.proximosavencer = proxAVencer;
            ViewBag.vencidos = vencidos;
            ViewBag.aceptados = aceptados;
            ViewBag.fechaLimiteAmpliada = fechaLimiteAmpliada;

            return PartialView();
        }

        /// <summary>
        /// Obtener tablero de seguimiento entregables filtrado por periodicidad y estado
        /// </summary>
        /// <param name="periodicidad"></param>
        /// <param name="estado"></param>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public PartialViewResult GetTableroSeguimientoEntregables(int? periodicidad, int? estado, int? tipoEntregable, string periodoFechaLimite, string periodoFechaEntrega, int? colorFiltro)
        {
            if (periodicidad == 0)
            {
                periodicidad = null;
            }

            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<vw_seguimiento_entregables> model = new List<vw_seguimiento_entregables>();
            try
            {
                model = Model.GetListEntregables(periodicidad).ToList();

                /*Se filtran los entregables dependiendo del rol*/
                if (SesionVar.ROL != "1" && SesionVar.ROL != "35")
                {
                    model = model.Where(l => l.persona_responsable == SesionVar.UserName).ToList();
                }

                List<ref_seguimiento_entregable_usuario_gestion> ListUserGestion = Model.GetUsuariosSegGestion();
                ViewBag.permitidogestionar = false;
                var obj = ListUserGestion.Where(l => l.usuario == SesionVar.UserName).FirstOrDefault();
                if (obj != null)
                    ViewBag.permitidogestionar = true;


                /*Se filtran los entregables por estado*/
                if (estado != null)
                {
                    if (estado != 0)
                    {
                        model = model.Where(l => l.id_estado_entregable == estado).OrderBy(l => l.fecha_limite).ToList();
                    }
                }

                /*Se filtran los entregables por tipo de entregable*/
                if (tipoEntregable != null)
                {
                    model = model.Where(l => l.id_tipo_entregable == tipoEntregable.Value).ToList();
                }

                /*Se filtran los entregables por fecha limirte*/
                if (!string.IsNullOrEmpty(periodoFechaLimite))
                {
                    var periodo = periodoFechaLimite.Split('/');
                    int mes = Convert.ToInt32(periodo[0]);
                    int año = Convert.ToInt32(periodo[1]);
                    model = model.Where(l => l.fecha_entrega_validada.Value.Month == Convert.ToInt32(periodo[0]) && l.fecha_entrega_validada.Value.Year == Convert.ToInt32(periodo[1])).ToList();
                }

                if (!string.IsNullOrEmpty(periodoFechaEntrega))
                {
                    var periodo = periodoFechaEntrega.Split('/');
                    int mes = Convert.ToInt32(periodo[0]);
                    int año = Convert.ToInt32(periodo[1]);
                    model = model.Where(l => l.fecha_entrega.Value.Month == Convert.ToInt32(periodo[0]) && l.fecha_entrega.Value.Year == Convert.ToInt32(periodo[1])).ToList();
                }

                if (colorFiltro != null)
                {
                    switch (colorFiltro)
                    {
                        case 1:
                            model = model.Where(l => (l.id_estado_entregable != 3 && l.id_estado_entregable != 7) && l.diferencia_dias > 2 && (l.tiene_ampliacion == false || l.tiene_ampliacion == null)).ToList();
                            break;
                        case 2:
                            model = model.Where(l => (l.id_estado_entregable != 3 && l.id_estado_entregable != 7) && (l.diferencia_dias <= 2 && l.diferencia_dias >= 0) && (l.tiene_ampliacion == false || l.tiene_ampliacion == null)).ToList();
                            break;
                        case 3:
                            model = model.Where(l => (l.id_estado_entregable != 3 && l.id_estado_entregable != 7) && l.diferencia_dias < 0 && (l.tiene_ampliacion == false || l.tiene_ampliacion == null)).ToList();
                            break;
                        case 4:
                            model = model.Where(l => l.id_estado_entregable == 7 || (l.id_estado_entregable != 3 && l.tiene_ampliacion == true)).ToList();
                            break;
                        case 5:
                            model = model.Where(l => l.id_estado_entregable == 3).ToList();
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView(model);
        }


        public ActionResult PresentarEntregable2(int txtidentregable, int? txtsegperiodo, List<HttpPostedFileBase> documento, HttpPostedFileBase soporte)
        {
            Int32 id_seg_entregable_periodo = 0;
            Int32 id_seg_dtll_entrega = 0;
            string archivo = "";
            string archivo2 = "";

            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();

            if (txtsegperiodo == null)
            {
                /*Obtenemos el periodo ya que no esta creado*/
                id_seg_entregable_periodo = Model.InsertarEntregableInicial(txtidentregable, ref MsgRes);
            }
            else
            {
                Model.InsertarProximoEntregable(txtsegperiodo.Value, ref MsgRes);

                /*Como ya se sabe el id del periodo, lo seteamos en la variable id_seg_entregable_periodo*/
                id_seg_entregable_periodo = txtsegperiodo.Value;
            }

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                try
                {

                    seguimiento_dtll_entrega obj = new seguimiento_dtll_entrega();
                    obj.id_seg_entregable = txtidentregable;
                    obj.id_seg_entregable_periodo = txtsegperiodo;
                    obj.fecha_entrega = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;
                    obj.id_estado_entregable = 2;

                    id_seg_dtll_entrega = Model.InsertarSeguimientoEntregableDTLL1(txtidentregable, obj, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        DateTime fecha = DateTime.Now;
                        String carpeta = "";
                        seguimiento_entregables_documentos obj2 = new seguimiento_entregables_documentos();
                        List<seguimiento_entregables_documentos> Lista = new List<seguimiento_entregables_documentos>();

                        string strRetorno = string.Empty;
                        StringBuilder sbRutaDefinitiva;
                        string strRutaDefinitiva = string.Empty;
                        strRutaDefinitiva = ConfigurationManager.AppSettings["RutadocumentoEntregables"];
                        sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);

                        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                        {
                            carpeta = "Entregables";
                        }
                        else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                        {
                            carpeta = "EntregablesPruebas";
                        }

                        if (soporte != null)
                        {
                            string nombreSintilde = Regex.Replace(soporte.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                            string ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + txtidentregable + "\\" + id_seg_dtll_entrega);
                            var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                            archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                            fecha, nombre, Path.GetExtension(soporte.FileName));

                            if (!Directory.Exists(ruta))
                                Directory.CreateDirectory(ruta);

                            soporte.SaveAs(archivo);

                            string dirpath2 = Path.Combine(Request.PhysicalApplicationPath, archivo);
                            WebClient User = new WebClient();
                            archivo = dirpath2;
                            string filename = archivo;
                            Byte[] FileBuffer = User.DownloadData(filename);

                            if (FileBuffer != null)
                            {
                                obj2.id_seg_entregable = txtidentregable;
                                obj2.id_seg_dtll_entrega = id_seg_dtll_entrega;
                                obj2.tipo_anexo_entregable = 2;
                                obj2.ruta_documento = Convert.ToString(archivo);
                                obj2.ext = Path.GetExtension(soporte.FileName);
                                obj2.contentType = soporte.ContentType;

                                Lista.Add(obj2);
                                obj2 = new seguimiento_entregables_documentos();

                            }
                        }

                        foreach (var item in documento)
                        {
                            string nombreSintilde = Regex.Replace(item.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                            string ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + txtidentregable + "\\" + id_seg_dtll_entrega);
                            var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                            archivo2 = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                            fecha, nombre, Path.GetExtension(item.FileName));

                            if (!Directory.Exists(ruta))
                                Directory.CreateDirectory(ruta);

                            item.SaveAs(archivo2);

                            string dirpath2 = Path.Combine(Request.PhysicalApplicationPath, archivo2);
                            WebClient User = new WebClient();
                            archivo2 = dirpath2;
                            string filename = archivo2;
                            Byte[] FileBuffer = User.DownloadData(filename);

                            if (FileBuffer != null)
                            {
                                obj2.id_seg_entregable = txtidentregable;
                                obj2.id_seg_dtll_entrega = id_seg_dtll_entrega;
                                obj2.tipo_anexo_entregable = 1;
                                obj2.ruta_documento = archivo2;
                                obj2.ext = Path.GetExtension(item.FileName);
                                obj2.contentType = item.ContentType;

                                Lista.Add(obj2);
                                obj2 = new seguimiento_entregables_documentos();


                            }
                        }

                        Model.InsertarSeguimientoEntregableDTLL2(Lista, ref MsgRes);

                    }

                }
                catch (Exception ex)
                {
                    TempData["mensaje"] = ex.Message;
                }

                /*Envio de correo de la presentación del entregable*/
                EnvioCorreoPresentacionEntregable(id_seg_entregable_periodo);

            }
            else
            {
                TempData["mensaje"] = "No inserta los registros iniciales de seguimiento periodo";
            }

            return RedirectToAction("TableroSeguimientoEntregables", "SeguimientoEntregables", new { rta = 1 });
        }

        /// <summary>
        /// Metodo post para agregar las observaciones de un entregable
        /// </summary>
        /// <param name="id_entregable_periodo"></param>
        /// <param name="id_ultimo_entregable"></param>
        /// <param name="observaciones"></param>
        /// <returns></returns>
        public ActionResult ObservacionesEntregable(int id_entregable_periodo, int id_ultimo_entregable, string observaciones, int? tipo)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_dtll_entrega entrega = new seguimiento_dtll_entrega();
            seguimiento_dtll_entrega obj = new seguimiento_dtll_entrega();

            try
            {
                if (tipo == 1)
                {
                    entrega = Model.GetseguimientoDtllEntrega(id_ultimo_entregable);
                    obj.id_estado_entregable = 4;
                }
                else
                {
                    //entrega = Model.GetseguimientoDtllEntrega(id_ultimo_entregable);
                    entrega = Model.GetseguimientoDtllEntregaPresentado((int)id_ultimo_entregable);
                    obj.id_estado_entregable = 8;
                    var elimina = BusClass.eliminarEvaluacioEntregablesID(id_entregable_periodo);
                    var eliminaFelicitaciones = BusClass.eliminarFelicitacionesEntregablesID(id_entregable_periodo);
                }

                obj.id_seg_entregable = entrega.id_seg_entregable;
                obj.id_seg_entregable_periodo = id_entregable_periodo;
                obj.id_dtll_ant = id_ultimo_entregable;
                obj.fecha_entrega = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.observaciones = observaciones;
                Model.InsertarGestionEntregable(id_entregable_periodo, obj, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    /*Se envía el correo de rechazo u observacion del entregable al lider encargado. accion 2 significa observacion */
                    if (tipo == 1)
                    {
                        EnvioCorreoAceptacionRechazoEntregable(id_entregable_periodo, 2);
                    }
                    else
                    {
                        EnvioCorreoAceptacionRechazoEntregable(id_entregable_periodo, 3);
                    }

                    return RedirectToAction("TableroGestionSeguimientoEntregables", "SeguimientoEntregables", new { idSeguimientoEntregable = entrega.id_seg_entregable });
                }
                else
                {
                    return RedirectToAction("TableroGestionSeguimientoEntregables", "SeguimientoEntregables", new { idSeguimientoEntregable = entrega.id_seg_entregable });
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return RedirectToAction("TableroGestionSeguimientoEntregables", "SeguimientoEntregables", new { idSeguimientoEntregable = entrega.id_seg_entregable });
            }
        }

        /// <summary>
        /// Metodo post para aceptar entregables
        /// </summary>
        /// <param name="id_entregable_periodo"></param>
        /// <param name="id_ult_dtll"></param>
        /// <returns></returns>
        public ActionResult AceptarEntregable(int id_entregable_periodo, int id_ult_dtll)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_dtll_entrega entrega = Model.GetseguimientoDtllEntrega(id_ult_dtll);
            seguimiento_dtll_entrega obj = new seguimiento_dtll_entrega();
            obj.id_seg_entregable = entrega.id_seg_entregable;
            obj.id_seg_entregable_periodo = id_entregable_periodo;
            obj.fecha_entrega = DateTime.Now;
            obj.usuario_digita = SesionVar.UserName;
            obj.id_dtll_ant = id_ult_dtll;
            obj.id_estado_entregable = 3;
            Model.InsertarGestionEntregable(id_entregable_periodo, obj, ref MsgRes);

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                /*Se envía el correo de aceptacion del entregable al lider encargado. accion 1 significa aceptacion */
                EnvioCorreoAceptacionRechazoEntregable(id_entregable_periodo, 1);

                return RedirectToAction("TableroGestionSeguimientoEntregables", "SeguimientoEntregables", new { idSeguimientoEntregable = entrega.id_seg_entregable });
            }
            else
            {
                return RedirectToAction("TableroGestionSeguimientoEntregables", "SeguimientoEntregables", new { idSeguimientoEntregable = entrega.id_seg_entregable });
            }
        }

        /// <summary>
        /// Metodo post para guardar las respuestas a la observacion de un entregable
        /// </summary>
        /// <param name="txtiddtllentrega"></param>
        /// <param name="observaciones2"></param>
        /// <param name="evidencia"></param>
        /// <returns></returns>
        public ActionResult RtaObservacionEntregable(int txtiddtllentrega, string observaciones2, HttpPostedFileBase evidencia)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            try
            {
                string carpeta = "";
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "Entregables";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "EntregablesPruebas";
                }
                string Rutadocumentos = ConfigurationManager.AppSettings["RutadocumentoEntregables"] + "//" + carpeta + "//";
                string filenamedoc = Model.nomdocumento(txtiddtllentrega, 3, Path.GetExtension(evidencia.FileName));
                string path = Path.Combine(Rutadocumentos, filenamedoc);
                evidencia.SaveAs(path);

                seguimiento_dtll_entrega obj = Model.GetseguimientoDtllEntrega(txtiddtllentrega);
                obj.fecha_obs_respuesta = DateTime.Now;
                obj.observaciones_respuesta = observaciones2;
                obj.ruta_evidencia_rta = path;
                Model.GuardarRespuestaObservaciones(obj, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("TableroSeguimientoEntregables", "SeguimientoEntregables", new { rta = 7 });
                }
                else
                {
                    return RedirectToAction("TableroSeguimientoEntregables", "SeguimientoEntregables", new { rta = 8 });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("TableroSeguimientoEntregables", "SeguimientoEntregables", new { rta = 8 });
            }

        }

        /// <summary>
        /// Metodo void para ver los documentos y soportes filtrados por el tipo de documento
        /// </summary>
        /// <param name="id_seg_dtll"></param>
        /// <param name="id_tipo_doc"></param>
        public void Verdocumentocargado(int id_seg_dtll, int id_tipo_doc)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();

            List<seguimiento_entregables_documentos> documentos_entregable = new List<seguimiento_entregables_documentos>();
            seguimiento_dtll_entrega dtll = Model.GetseguimientoDtllEntrega(id_seg_dtll);
            if (dtll.id_dtll_ant == null)
            {
                documentos_entregable = Model.GetSeguimientoEntregableDocs(id_seg_dtll);
            }
            else
            {
                documentos_entregable = Model.GetSeguimientoEntregableDocs(dtll.id_dtll_ant.Value);
                if (documentos_entregable.Count == 0)
                {
                    var dtll2 = Model.GetseguimientoDtllEntrega(dtll.id_dtll_ant.Value);
                    if (dtll2.id_dtll_ant != null)
                    {
                        documentos_entregable = Model.GetSeguimientoEntregableDocs(dtll2.id_dtll_ant.Value);
                    }
                }
            }

            var documento = documentos_entregable.Where(l => l.tipo_anexo_entregable == id_tipo_doc).FirstOrDefault();

            if (documento != null)
            {

                byte[] bytes;
                using (FileStream file = new FileStream(documento.ruta_documento, FileMode.Open, FileAccess.Read))
                {
                    bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                }
                byte[] array = bytes.ToArray();

                if (array != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";// documento.contentType;
                    Response.AddHeader("content-length", array.Length.ToString());
                    Response.BinaryWrite(array);
                    Response.Flush();
                }
            }
            else
            {
                Response.Write("<script language=javascript>alert('No contiene evidencias');</script>");
            }

        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Metodo para ver la evidencia cargada por parte del usuario (documento)
        /// </summary>
        /// <param name="id_seg_dtll"></param>
        /// <returns></returns>
        public string Verdocumentocargado2(int id_seg_dtll)
        {
            string result = "";

            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();

            List<managmentSeguimiento_entregables_documentosResult> documentos_entregable = new List<managmentSeguimiento_entregables_documentosResult>();
            try
            {
                seguimiento_dtll_entrega dtll = Model.GetseguimientoDtllEntrega(id_seg_dtll);
                if (dtll.id_dtll_ant == null)
                {
                    documentos_entregable = Model.GetSeguimientoEntregableDocs2(ref MsgRes);
                    documentos_entregable = documentos_entregable.Where(X => X.id_seg_dtll_entrega == id_seg_dtll).ToList();
                }
                else
                {
                    documentos_entregable = Model.GetSeguimientoEntregableDocs2(ref MsgRes);
                    documentos_entregable = documentos_entregable.Where(X => X.id_seg_dtll_entrega == dtll.id_dtll_ant.Value).ToList();
                    if (documentos_entregable.Count == 0)
                    {
                        var dtll2 = Model.GetseguimientoDtllEntrega(dtll.id_dtll_ant.Value);
                        if (dtll2.id_dtll_ant != null)
                        {
                            documentos_entregable = Model.GetSeguimientoEntregableDocs2(ref MsgRes);
                            documentos_entregable = documentos_entregable.Where(X => X.id_seg_dtll_entrega == dtll.id_dtll_ant.Value).ToList();
                        }
                    }
                }

                //documentos_entregable = Model.GetSeguimientoEntregableDocs2(id_seg_dtll, ref MsgRes);
                //documentos_entregable = documentos_entregable.Where(X => X.id_seg_dtll_entrega == id_seg_dtll).ToList();

                if (documentos_entregable.Count == 0)
                {
                    var dtll2 = Model.GetseguimientoDtllEntrega(dtll.id_seg_dtll_entrega);
                    if (dtll2.id_dtll_ant != null)
                    {
                        documentos_entregable = Model.GetSeguimientoEntregableDocs2(ref MsgRes);
                        documentos_entregable = documentos_entregable.Where(X => X.id_seg_dtll_entrega == dtll.id_dtll_ant.Value).ToList();
                    }
                }

                int i = 0;

                if (documentos_entregable.Count() > 0)
                {
                    foreach (var item in documentos_entregable)
                    {
                        i += 1;
                        result += "<tr>";
                        result += "<td>" + i + "</td>";
                        result += "<td>" + item.tipo_anexo_entregable + "</td>";
                        result += "<td>" + item.nom_anexo + "</td>";
                        result += "<td><a href='javascript:AbrirSoporte(" + item.id_documento_seg_entregable + ")'>Ver documento</a></td>";
                        result += "</tr>";
                    }
                }
                else
                {
                    result += "<tr>";
                    result += "<td colspan='14' style='text-font:14px; text-align: center;'> SIN ARCHIVOS";
                    result += "</td>";
                    result += "</tr>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public ActionResult versoporteclinico2(Int32 id_documento_seg_entregable)
        {
            seguimiento_entregables_documentos dato = new seguimiento_entregables_documentos();
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            try
            {
                string tipoArchivo = "";

                dato = Model.traerDocumentoEntregableId(id_documento_seg_entregable);
                if (dato != null)
                {
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, dato.ruta_documento);
                    string contentype = "";
                    tipoArchivo = dato.contentType;

                    if (dato.ext == ".xlsx" || dato.ext == ".xls")
                    {
                        contentype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else if (dato.ext == ".png" || dato.ext == ".jpg" || dato.ext == ".jpeg")
                    {
                        contentype = "image/png";
                    }
                    else
                    {
                        contentype = "application/pdf";
                    }

                    if (tipoArchivo == "")
                    {
                        return File(dirpath, contentype);
                    }
                    else
                    {
                        return File(dirpath, tipoArchivo);
                    }

                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Error al vusualizar el archvo" });
                }
            }
            catch
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
            }
        }

        /// <summary>
        /// Metodo string que arma una tabla que lista el historico de observaciones
        /// </summary>
        /// <param name="id_seg_periodo"></param>
        /// <returns></returns>
        public string ObtenerHistoricoObservaciones(int id_seg_periodo, int? tipo)
        {
            string result = "";
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<seguimiento_dtll_entrega> entregas = new List<seguimiento_dtll_entrega>();

            if (tipo == 1)
            {
                entregas = Model.GetListseguimientoDtllEntrega(id_seg_periodo).Where(l => l.id_estado_entregable == 4).ToList();
            }
            else
            {
                entregas = Model.GetListseguimientoDtllEntrega(id_seg_periodo).Where(l => l.id_estado_entregable == 8).ToList();
            }

            if (entregas.Count == 0)
            {
                result += "<tr>";
                result += "<td colspan='3' class='text-center'>No se ha ingresado ningúna  observación</td>";
                result += "</tr>";
            }
            else
            {
                int i = 0;
                foreach (var item in entregas)
                {
                    i += 1;
                    result += "<tr>";
                    result += "<td>" + i + "</td>";
                    result += "<td>" + item.fecha_entrega.ToString("dd/MM/yyyy") + "</td>";
                    result += "<td>" + item.observaciones + "</td>";
                    //if (item.fecha_obs_respuesta != null)
                    //{
                    //    result += "<td class='text-center'><a role='button' class='btn button_Asalud_Aceptar btn-xs' href='javascript:Verrespuesta(" + item.id_seg_dtll_entrega + ")'>Ver respuesta&nbsp</a></td>";
                    //}
                    //else
                    //{
                    //    result += "<td class='text-center'><a role='button' class='btn button_Asalud_Aceptar btn-xs' href='javascript:responderobservacion(" + item.id_seg_dtll_entrega + ")'>Responder&nbsp;<i class='glyphicon glyphicon-share-alt'></i></a></td>";
                    //}

                    result += "</tr>";
                }
            }



            return result;

        }

        /// <summary>
        /// Metodo json para obtener los datos de un detalle del entregable
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public JsonResult ObtenerDatosEntregaDetalle(int id, int tipo)
        {
            string fechaobservacion = "";
            string fechalimiteampliacion = "";
            string fechaContractualmentePactada = "";
            string solicitadoPor = "";

            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_dtll_entrega obj = Model.GetseguimientoDtllEntrega(id);

            switch (tipo)
            {
                case 1:
                    fechaobservacion = obj.fecha_obs_respuesta.Value.ToString("dd/MM/yyyy");
                    break;
                case 2:
                    var seguimiento_periodo = Model.GetEntregablePeriodoById(obj.id_seg_entregable_periodo.Value);
                    fechalimiteampliacion = seguimiento_periodo.fecha_limite_ampliacion.Value.ToString("dd/MM/yyyy");
                    fechaContractualmentePactada = seguimiento_periodo.fecha_limite.ToString("dd/MM/yyyy");
                    solicitadoPor = seguimiento_periodo.solicitud_ampliacion_realizada_por;
                    break;
            }

            var data = new
            {
                id = obj.id_seg_dtll_entrega,
                fecha_observacion = fechaobservacion,
                observaciones_rta = obj.observaciones_respuesta,
                fecha_entrega = obj.fecha_entrega.ToString("dd/MM/yyyy"),
                observaciones = obj.observaciones,
                fecha_limite_ampliacion = fechalimiteampliacion,
                fecha_contractualmente_pactada = fechaContractualmentePactada,
                solicitadoPor = solicitadoPor
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo json para retornar los datos de un periodo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Obtenerdatosperiodo(int id)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_entregables_periodo obj = Model.GetEntregablePeriodoById(id);

            var data = new
            {
                id = obj.id_seg_entregable_periodo,
                fecha_limite = obj.fecha_limite.ToString("dd/MM/yyyy")
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public void abrirEvidenciaRta(int id_seg_dtll)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_dtll_entrega dtll = Model.GetseguimientoDtllEntrega(id_seg_dtll);

            List<seguimiento_entregables_documentos> documentos_entregable = new List<seguimiento_entregables_documentos>();
            documentos_entregable = Model.GetSeguimientoEntregableDocs(id_seg_dtll);

            byte[] bytes;
            using (FileStream file = new FileStream(dtll.ruta_evidencia_rta, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
            }
            byte[] array = bytes.ToArray();

            if (array != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Clear();
                Response.ContentType = "image/png";
                Response.AddHeader("content-length", array.Length.ToString());
                Response.BinaryWrite(array);
                Response.Flush();
            }

        }

        public void AbrirEvidenciaSolicitud(int id_seg_dtll)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_dtll_entrega dtll = Model.GetseguimientoDtllEntrega(id_seg_dtll);

            List<seguimiento_entregables_documentos> documentos_entregable = new List<seguimiento_entregables_documentos>();
            documentos_entregable = Model.GetSeguimientoEntregableDocs(id_seg_dtll);

            var documento = documentos_entregable.Where(l => l.tipo_anexo_entregable == 4).FirstOrDefault();
            byte[] bytes;
            using (FileStream file = new FileStream(documento.ruta_documento, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
            }
            byte[] array = bytes.ToArray();

            if (array != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Clear();
                Response.ContentType = documento.contentType;
                Response.AddHeader("content-length", array.Length.ToString());
                Response.BinaryWrite(array);
                Response.Flush();
            }

        }

        /// <summary>
        /// Autor: Alexis quiñones castillo
        /// Metodo action donde se guarda la informacion al momento de realizar la solicitud de ampliacion para la presentacion de un entregable
        /// 
        /// </summary>
        /// <param name="txtidsegentregables"></param>
        /// <param name="txtidperiodoentregable"></param>
        /// <param name="nuevafechalimite"></param>
        /// <param name="Justificacion"></param>
        /// <param name="fileevidencia"></param>
        /// <returns></returns>
        public ActionResult SolicitarAmpliacionPlazo2(int txtidsegentregables, int? txtidperiodoentregable, string txtSolicitudRealizadaPor, DateTime nuevafechalimite, string Justificacion, HttpPostedFileBase fileevidencia)
        {
            Int32 id_seg_dtll_entrega = 0;
            string archivo = "";
            int idperiodoentregable = 0;

            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            try
            {
                DateTime fecha = DateTime.Now;
                String carpeta = "";
                seguimiento_entregables_documentos obj2 = new seguimiento_entregables_documentos();
                List<seguimiento_entregables_documentos> Lista = new List<seguimiento_entregables_documentos>();

                idperiodoentregable = Model.InsertarSolicitudAmpliacion(txtidperiodoentregable, txtidsegentregables, txtSolicitudRealizadaPor, nuevafechalimite);

                if (idperiodoentregable > 0)
                {
                    seguimiento_dtll_entrega obj = new seguimiento_dtll_entrega();

                    obj.id_seg_entregable = txtidsegentregables;
                    obj.id_seg_entregable_periodo = idperiodoentregable;
                    obj.fecha_entrega = DateTime.Now;
                    obj.observaciones = Justificacion;
                    obj.usuario_digita = SesionVar.UserName;
                    obj.id_estado_entregable = 6;

                    id_seg_dtll_entrega = Model.InsertarSeguimientoEntregableDTLL1(txtidsegentregables, obj, ref MsgRes);

                    string strRetorno = string.Empty;
                    StringBuilder sbRutaDefinitiva;
                    string strRutaDefinitiva = string.Empty;
                    strRutaDefinitiva = ConfigurationManager.AppSettings["RutadocumentoEntregables"];
                    sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);

                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                    {
                        carpeta = "Facturacion";
                    }
                    else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                    {
                        carpeta = "FacturacionPruebas";
                    }

                    string nombreSintilde = Regex.Replace(fileevidencia.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                    string ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + txtidsegentregables + "\\" + id_seg_dtll_entrega);
                    var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                    archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                    fecha, nombre, Path.GetExtension(fileevidencia.FileName));

                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    fileevidencia.SaveAs(archivo);

                    string dirpath2 = Path.Combine(Request.PhysicalApplicationPath, archivo);
                    WebClient User = new WebClient();
                    archivo = dirpath2;
                    string filename = archivo;
                    Byte[] FileBuffer = User.DownloadData(filename);

                    if (FileBuffer != null)
                    {
                        List<seguimiento_entregables_documentos> Objdocumentos = new List<seguimiento_entregables_documentos>();

                        obj2.id_seg_entregable = txtidsegentregables;
                        obj2.id_seg_dtll_entrega = id_seg_dtll_entrega;
                        obj2.tipo_anexo_entregable = 4;
                        obj2.ruta_documento = archivo;
                        obj2.ext = Path.GetExtension(fileevidencia.FileName);
                        obj2.contentType = fileevidencia.ContentType;

                        Objdocumentos.Add(obj2);
                        obj2 = new seguimiento_entregables_documentos();

                        Model.InsertarSeguimientoEntregableDTLL2(Objdocumentos, ref MsgRes);

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            EnvioCorreoAmpliazionPlazo(idperiodoentregable);
                            return RedirectToAction("TableroSeguimientoEntregables", "SeguimientoEntregables", new { rta = 11 });
                        }
                        else
                        {
                            return RedirectToAction("TableroSeguimientoEntregables", "SeguimientoEntregables", new { rta = 10 });
                        }
                    }
                    else
                    {
                        return RedirectToAction("TableroSeguimientoEntregables", "SeguimientoEntregables", new { rta = 10 });
                    }
                }
                else
                {
                    return RedirectToAction("TableroSeguimientoEntregables", "SeguimientoEntregables", new { rta = 10 });
                }
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = ex.Message;
            }

            return View(Model);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 12 de enero de 2023
        /// Metodo para aeptar solicitud
        /// </summary>
        /// <param name="id_seg_dtll"></param>
        /// <returns></returns>
        public ActionResult AceptarSolicitudAmpliacion(int id_seg_dtll, string observaciones)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();

            seguimiento_dtll_entrega dtll = Model.GetseguimientoDtllEntrega(id_seg_dtll);
            int id_seg_periodo = dtll.id_seg_entregable_periodo.Value;

            /*Se actualiza el entregable del periodo */
            seguimiento_entregables_periodo periodo = Model.GetEntregablePeriodoById(id_seg_periodo);
            periodo.id_estado_entregable = 7;
            periodo.tiene_ampliacion = true;
            periodo.observaciones_ampliacion = observaciones;
            Model.ActualizarEntregablePeriodo(periodo, ref MsgRes);

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                /*Se envia correo de aceptacion de la solicitud de ampliacion de plazo*/
                EnvioCorreoAceptacionRechazoSolicitudAmpliacionPlazo(id_seg_periodo, 1);

                /*Se crea el nuevo entregable */
                Model.InsertarProximoEntregable(id_seg_periodo, ref MsgRes);

                return RedirectToAction("TableroGestionSeguimientoEntregables", "SeguimientoEntregables", new { idSeguimientoEntregable = periodo.id_seg_entregable });
            }
            else
            {
                return RedirectToAction("TableroGestionSeguimientoEntregables", "SeguimientoEntregables", new { idSeguimientoEntregable = periodo.id_seg_entregable });
            }

        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 12 de enero de 2023
        /// Metodo para rechazar una solicitud de ampliacion
        /// </summary>
        /// <param name="id_seg_dtll"></param>
        /// <returns></returns>
        public ActionResult RechazarSolicitudAmpliacion(int id_seg_dtll, string observaciones)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();

            seguimiento_dtll_entrega dtll = Model.GetseguimientoDtllEntrega(id_seg_dtll);
            int id_seg_periodo = dtll.id_seg_entregable_periodo.Value;

            seguimiento_entregables_periodo periodo = Model.GetEntregablePeriodoById(id_seg_periodo);
            periodo.id_estado_entregable = 1;
            periodo.tiene_ampliacion = false;
            periodo.observaciones_ampliacion = observaciones;
            periodo.fecha_limite_ampliacion = null;
            periodo.fecha_entrega = null;
            Model.ActualizarEntregablePeriodo(periodo, ref MsgRes);

            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                EnvioCorreoAceptacionRechazoSolicitudAmpliacionPlazo(id_seg_periodo, 2);
                return RedirectToAction("TableroGestionSeguimientoEntregables", "SeguimientoEntregables", new { idSeguimientoEntregable = periodo.id_seg_entregable });
            }
            else
            {
                return RedirectToAction("TableroGestionSeguimientoEntregables", "SeguimientoEntregables", new { idSeguimientoEntregable = periodo.id_seg_entregable });
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 4 de enero de 2023
        /// Tablero de control donde se puede ver el listado de entregables y asi mismo se puede realizar la respectiva gestion de los entregables.
        /// </summary>
        /// <returns></returns>
        public ActionResult TableroGestionSeguimientoEntregables()
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            //List<Management_seguimiento_entregables_gestionResult> List = Model.GetListSeguimientoEntregableGestion(periodicidad, tipoEntregable);

            ViewBag.tipoEntregable = Model.GetListTipoEntregable();
            ViewBag.estadoEntregable = Model.GetListEstadoEntregable().Where(l => l.estado == 'A').ToList();
            ViewBag.PeriodicidadEntregable = Model.GetListPeriodicidadEntregables();

            return View();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 04 de enero de 2023
        /// Vista o html donde se listaran las entregas realizadas a un entregable y a su vez se podra evaluar la calidad del mimo por parte de la analista de proyectos
        /// </summary>
        /// <param name="idSeguimientoEntregable"></param>
        /// <returns></returns>
        public PartialViewResult VerEntregasGestionSeguimientoEntregables(int? periodicidad, int? estado, int? tipoEntregable, string periodoFechaLimite, string periodoFechaEntrega, int? colorFiltro)
        {
            if (periodicidad == 0)
            {
                periodicidad = null;
            }

            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<vw_seguimiento_entregables> model = new List<vw_seguimiento_entregables>();
            var conteo = 0;
            try
            {
                model = Model.GetListEntregables(periodicidad).ToList();

                /*Se filtran los entregables dependiendo del rol*/
                if (SesionVar.ROL != "1" && SesionVar.ROL != "35")
                {
                    model = model.Where(l => l.persona_responsable == SesionVar.UserName).ToList();
                }

                List<ref_seguimiento_entregable_usuario_gestion> ListUserGestion = Model.GetUsuariosSegGestion();
                ViewBag.permitidogestionar = false;
                var obj = ListUserGestion.Where(l => l.usuario == SesionVar.UserName).FirstOrDefault();
                if (obj != null)
                    ViewBag.permitidogestionar = true;

                /*Se filtran los entregables por estado*/
                if (estado != null)
                {
                    if (estado != 0)
                    {
                        model = model.Where(l => l.id_estado_entregable == estado).OrderBy(l => l.fecha_limite).ToList();
                    }
                }

                /*Se filtran los entregables por tipo de entregable*/
                if (tipoEntregable != null)
                {
                    model = model.Where(l => l.id_tipo_entregable == tipoEntregable.Value).ToList();
                }

                /*Se filtran los entregables por fecha limirte*/
                if (!string.IsNullOrEmpty(periodoFechaLimite))
                {
                    var periodo = periodoFechaLimite.Split('/');
                    int mes = Convert.ToInt32(periodo[0]);
                    int año = Convert.ToInt32(periodo[1]);
                    model = model.Where(l => l.fecha_entrega_validada.Value.Month == Convert.ToInt32(periodo[0]) && l.fecha_entrega_validada.Value.Year == Convert.ToInt32(periodo[1])).ToList();
                }

                if (!string.IsNullOrEmpty(periodoFechaEntrega))
                {
                    var periodo = periodoFechaEntrega.Split('/');
                    int mes = Convert.ToInt32(periodo[0]);
                    int año = Convert.ToInt32(periodo[1]);
                    model = model.Where(l => l.fecha_entrega.Value.Month == Convert.ToInt32(periodo[0]) && l.fecha_entrega.Value.Year == Convert.ToInt32(periodo[1])).ToList();
                }

                if (colorFiltro != null)
                {
                    switch (colorFiltro)
                    {
                        case 1:
                            model = model.Where(l => (l.id_estado_entregable == null || l.id_estado_entregable == 1 || l.id_estado_entregable == 4 || l.id_estado_entregable == 7)).ToList();
                            break;
                        case 2:
                            model = model.Where(l => (l.id_estado_entregable == 2 || l.id_estado_entregable == 6)).ToList();
                            break;
                        case 3:
                            model = model.Where(l => l.id_estado_entregable == 3 && (l.tiene_ampliacion == null || l.tiene_ampliacion == false) && l.diferencia_dias >= 0).ToList();
                            break;
                        case 4:
                            model = model.Where(l => l.id_estado_entregable == 3 && (l.tiene_ampliacion == null || l.tiene_ampliacion == false) && l.diferencia_dias < 0).ToList();
                            break;
                        case 5:
                            model = model.Where(l => l.id_estado_entregable == 3 && (l.tiene_ampliacion != null && l.tiene_ampliacion == true)).ToList();
                            break;
                    }
                }
                conteo = model.Count();
                ViewBag.conteo = conteo;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.bdactiva = ConfigurationManager.AppSettings["BDActiva"];
            return PartialView(model);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// </summary>
        /// <param name="periodicidad"></param>
        /// <param name="estado"></param>
        /// <param name="tipoEntregable"></param>
        /// <param name="periodoFechaLimite"></param>
        /// <param name="periodoFechaEntrega"></param>
        /// <returns></returns>
        public PartialViewResult _PintarFiltrosColoresGestionEntregables(int? periodicidad, int? estado, int? tipoEntregable, string periodoFechaLimite, string periodoFechaEntrega)
        {

            if (periodicidad == 0)
            {
                periodicidad = null;
            }

            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<vw_seguimiento_entregables> model = Model.GetListEntregables(periodicidad).ToList();

            /*Se filtran los entregables dependiendo del rol*/
            if (SesionVar.ROL != "1" && SesionVar.ROL != "35")
            {
                model = model.Where(l => l.persona_responsable == SesionVar.UserName).ToList();
            }

            List<ref_seguimiento_entregable_usuario_gestion> ListUserGestion = Model.GetUsuariosSegGestion();
            ViewBag.permitidogestionar = false;
            var obj = ListUserGestion.Where(l => l.usuario == SesionVar.UserName).FirstOrDefault();
            if (obj != null)
                ViewBag.permitidogestionar = true;


            /*Se filtran los entregables por estado*/
            if (estado != null)
            {
                if (estado != 0)
                {
                    model = model.Where(l => l.id_estado_entregable == estado).OrderBy(l => l.fecha_limite).ToList();
                }
            }

            /*Se filtran los entregables por tipo de entregable*/
            if (tipoEntregable != null)
            {
                model = model.Where(l => l.id_tipo_entregable == tipoEntregable.Value).ToList();
            }

            /*Se filtran los entregables por fecha limirte*/
            if (!string.IsNullOrEmpty(periodoFechaLimite))
            {
                var periodo = periodoFechaLimite.Split('/');
                int mes = Convert.ToInt32(periodo[0]);
                int año = Convert.ToInt32(periodo[1]);
                model = model.Where(l => l.fecha_entrega_validada.Value.Month == Convert.ToInt32(periodo[0]) && l.fecha_entrega_validada.Value.Year == Convert.ToInt32(periodo[1])).ToList();
            }

            if (!string.IsNullOrEmpty(periodoFechaEntrega))
            {
                var periodo = periodoFechaEntrega.Split('/');
                int mes = Convert.ToInt32(periodo[0]);
                int año = Convert.ToInt32(periodo[1]);
                model = model.Where(l => l.fecha_entrega.Value.Month == Convert.ToInt32(periodo[0]) && l.fecha_entrega.Value.Year == Convert.ToInt32(periodo[1])).ToList();
            }


            int pendientePresentacion = 0;
            int pendienteGestion = 0;
            int Oportunos = 0;
            int Inoportunos = 0;
            int fechaLimiteAmpliada = 0;

            if (model.Count > 0)
            {
                pendientePresentacion = model.Where(l => (l.id_estado_entregable == null || l.id_estado_entregable == 1 || l.id_estado_entregable == 4 || l.id_estado_entregable == 7)).Count();
                pendienteGestion = model.Where(l => (l.id_estado_entregable == 2 || l.id_estado_entregable == 6)).Count();
                Oportunos = model.Where(l => l.id_estado_entregable == 3 && (l.tiene_ampliacion == null || l.tiene_ampliacion == false) && l.diferencia_dias >= 0).Count();
                Inoportunos = model.Where(l => l.id_estado_entregable == 3 && (l.tiene_ampliacion == null || l.tiene_ampliacion == false) && l.diferencia_dias < 0).Count();
                fechaLimiteAmpliada = model.Where(l => l.id_estado_entregable == 3 && (l.tiene_ampliacion != null && l.tiene_ampliacion == true)).Count();
            }

            ViewBag.pendientePresentacion = pendientePresentacion;
            ViewBag.pendienteGestion = pendienteGestion;
            ViewBag.Oportunos = Oportunos;
            ViewBag.Inoportunos = Inoportunos;
            ViewBag.fechaLimiteAmpliada = fechaLimiteAmpliada;

            return PartialView();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 11 de enero de 2023
        /// Metodo utilizado para guardar los datos de la pequeña evaluacion de calidad realizada a un entregable.
        /// </summary>
        /// <param name="idSegEntregablePeriodo"></param>
        /// <param name="cumple"></param>
        /// <param name="observaciones"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GuardarDatosEvalCalidadSegEntregable(int idSegEntregablePeriodo, string cumple, string observaciones)
        {
            try
            {
                Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();

                seguimiento_entregables_periodo_eval_calidad obj = new seguimiento_entregables_periodo_eval_calidad();
                obj.seguimiento_entregables_periodo_id = idSegEntregablePeriodo;
                obj.cumple = cumple;
                obj.oservaciones = observaciones;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                Model.GuardarDatosEvalCalidadSegEntregable(obj, ref MsgRes);
                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
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
                return Json(new { success = false, message = "Ah ocurrido un error al momento de procesar la solicitud." }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 12 de enero de 2023
        /// Metodo json para poder obtener el detalle de la evaluacion de calidad realizada a un entregable.
        /// </summary>
        /// <param name="idPeriodoSegEntregable"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult verDetallesEvaluacionCalidadSegEntregables(int idPeriodoSegEntregable)
        {
            try
            {
                Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
                seguimiento_entregables_periodo_eval_calidad evaluacion = Model.ConsultarEvaluacionPorIdPeriodoSegEntregable(idPeriodoSegEntregable);
                string result = "";

                result += "<table>";
                result += "<tbody>";
                result += "<tr>";
                result += "<td>¿Cumple con los estándares de calidad?</td>";
                result += "<td>" + evaluacion.cumple + "</td>";
                result += "</tr>";

                result += "<tr>";
                result += "<td>Observaciones:</td>";
                result += "<td>" + evaluacion.oservaciones + "</td>";
                result += "</tr>";

                result += "</tbody>";
                result += "</table>";

                return Json(new { success = true, datos = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ah ocurrido un error al momento de procesar la solicitud: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 16 de enero de 2023
        /// Vista html que donde se evidenciara el consolidado (oportunidad) de los entregables
        /// </summary>
        /// <returns></returns>
        public ActionResult TableroOportunidadEntregables()
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();


            return View();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 16 de enero de 2023
        /// </summary>
        /// <param name="personaResponsable"></param>
        /// <param name="tipoEntregable"></param>
        /// <param name="periodicidad"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public PartialViewResult GetResultTableroOportunidadEntregables(string personaResponsable, int? tipoEntregable, int? periodicidad, int? año)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<Management_seguimiento_entregables_indicadoresResult> List = Model.GetListadoOportunidadSeguimientoEntregables(personaResponsable, tipoEntregable, periodicidad, año);
            ViewBag.usuarios = Model.GetResponsablesList();
            ViewBag.periodicidad = Model.GetListPeriodicidadEntregables();
            ViewBag.tipoEntregable = Model.GetListTipoEntregable();
            Session["listadoOportunidadSeguimientoEntregables"] = List;
            return PartialView(List);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// </summary>
        /// <returns></returns>
        public PartialViewResult GetResultTableroIndicadoresGestionEntregables()
        {
            return PartialView();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public PartialViewResult _IndicadoresGestionEntregablesPorPesona(int? mesInicial, int? mesFinal, int? año, string responsable)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            if (mesInicial == null) { mesInicial = 1; }
            if (mesFinal == null) { mesFinal = 12; }
            if (año == null) { año = DateTime.Now.Year; }
            if (string.IsNullOrEmpty(responsable))
            {
                responsable = "";
            }

            var mesesDelAño = BusClass.meses();
            List<ref_meses_del_año> listadoMeses = new List<ref_meses_del_año>();
            for (int i = mesInicial.Value; i <= mesFinal.Value; i++)
            {
                ref_meses_del_año mes = mesesDelAño.Where(l => l.id_mes == i).FirstOrDefault();
                listadoMeses.Add(mes);
            }


            List<Management_SeguimientoEntregables_IndicadorXPersonaResult> result = Model.GetListadoIndicadoresXPersonaSeguimientoEntregables(mesInicial.Value, mesFinal.Value, año.Value, responsable);
            ViewBag.meses = listadoMeses.OrderBy(l => l.id_mes).ToList();
            ViewBag.responsables = new List<string>();
            ViewBag.componentes = new List<string>();
            if (result.Count > 0)
            {
                ViewBag.responsables = result.Select(l => l.nom_persona_responsable).Distinct().ToList();
            }

            ViewBag.ref_meses = BusClass.meses();
            ViewBag.personaresponsable = Model.GetResponsablesList();


            ViewBag.mesInicial = mesInicial;
            ViewBag.mesFinal = mesFinal;
            ViewBag.año = año;
            ViewBag.perResponsable = responsable;

            ViewBag.result = result;
            return PartialView();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// </summary>
        /// <param name="responsable"></param>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public string GetDetalleDatosTableroIndicadoresXPersona(string responsable, int mes, int año)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<Management_SeguimientoEntregables_IndicadorXPersonaResult> result = Model.GetListadoIndicadoresXPersonaSeguimientoEntregables(mes, mes, año, responsable);
            string html = "";
            if (result.Count > 0)
            {
                var obj = result.FirstOrDefault();
                html += "<span class='head'>Num entregables : </span><span>" + obj.num_entregables_programados + "</span><br/>";
                html += "<span class='head'>Oportunos : </span><span>" + obj.num_entregables_oportunos + "</span><br/>";
                html += "<span class='head'>Inoportunos : </span>" + obj.num_entregables_inoportunos + "<span></span><br/>";
            }
            else
            {
                html += "<span class='head'>Sin registros. </span>";
            }

            return html;
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public PartialViewResult _IndicadoresGestionEntregablesPorComponente(int? mesInicial, int? mesFinal, int? año, int? idComponente)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            if (mesInicial == null) { mesInicial = 1; }
            if (mesFinal == null) { mesFinal = 12; }
            if (año == null) { año = DateTime.Now.Year; }

            var mesesDelAño = BusClass.meses();
            List<ref_meses_del_año> listadoMeses = new List<ref_meses_del_año>();
            for (int i = mesInicial.Value; i <= mesFinal.Value; i++)
            {
                ref_meses_del_año mes = mesesDelAño.Where(l => l.id_mes == i).FirstOrDefault();
                listadoMeses.Add(mes);
            }


            List<Management_SeguimientoEntregables_IndicadorXComponenteResult> result = Model.GetListadoIndicadoresXComponenteSeguimientoEntregables(mesInicial.Value, mesFinal.Value, año.Value, idComponente);
            ViewBag.meses = listadoMeses.OrderBy(l => l.id_mes).ToList();
            ViewBag.componentes = new List<string>();
            if (result.Count > 0)
            {
                ViewBag.componentes = result.Select(l => l.componente).Distinct().ToList();
            }

            ViewBag.ref_meses = BusClass.meses();
            ViewBag.procesos = Model.Getprocesoentregable();

            ViewBag.mesInicial = mesInicial;
            ViewBag.mesFinal = mesFinal;
            ViewBag.año = año;
            ViewBag.componenteSeleccionado = idComponente;

            ViewBag.result = result;
            return PartialView();
        }

        public string GetDetalleDatosTableroIndicadoresXComponente(int idComponente, int mes, int año)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<Management_SeguimientoEntregables_IndicadorXComponenteResult> result = Model.GetListadoIndicadoresXComponenteSeguimientoEntregables(mes, mes, año, idComponente);
            string html = "";
            if (result.Count > 0)
            {
                var obj = result.FirstOrDefault();
                html += "<span class='head'>Num entregables : </span><span>" + obj.num_entregables_programados + "</span><br/>";
                html += "<span class='head'>Oportunos : </span><span>" + obj.num_entregables_oportunos + "</span><br/>";
                html += "<span class='head'>Inoportunos : </span>" + obj.num_entregables_inoportunos + "<span></span><br/>";
            }
            else
            {
                html += "<span class='head'>Sin registros. </span>";
            }

            return html;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <param name="idComponente"></param>
        /// <param name="periodicidad"></param>
        /// <returns></returns>
        public PartialViewResult _IndicadoresGestionEntregablesPorCompyPeriodicidad(int? mesInicial, int? mesFinal, int? año, int? idComponente, int? periodicidad)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            if (mesInicial == null) { mesInicial = 1; }
            if (mesFinal == null) { mesFinal = 12; }
            if (año == null) { año = DateTime.Now.Year; }

            var mesesDelAño = BusClass.meses();
            List<ref_meses_del_año> listadoMeses = new List<ref_meses_del_año>();
            for (int i = mesInicial.Value; i <= mesFinal.Value; i++)
            {
                ref_meses_del_año mes = mesesDelAño.Where(l => l.id_mes == i).FirstOrDefault();
                listadoMeses.Add(mes);
            }

            List<Management_SeguimientoEntregables_IndicadorXCompyPeridicidadResult> result = Model.GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables(mesInicial.Value, mesFinal.Value, año.Value, idComponente, periodicidad);

            ViewBag.meses = listadoMeses.OrderBy(l => l.id_mes).ToList();
            ViewBag.componentes = new List<string>();
            ViewBag.periodicidades = new List<string>();

            if (result.Count > 0)
            {
                ViewBag.componentes = result.Select(l => l.componente).Distinct().ToList();
                ViewBag.periodicidades = result.Select(l => l.periodicidad).Distinct().ToList();
            }

            /*ref necesarios */
            ViewBag.ref_meses = BusClass.meses();
            ViewBag.procesos = Model.Getprocesoentregable();
            ViewBag.periodicidadesList = Model.GetListPeriodicidadEntregables();

            ViewBag.mesInicial = mesInicial;
            ViewBag.mesFinal = mesFinal;
            ViewBag.año = año;
            ViewBag.componenteSeleccionado = idComponente;
            ViewBag.periodicidadSeleccionada = periodicidad;

            ViewBag.result = result;
            return PartialView();
        }

        public string GetDetalleDatosTableroIndicadoresXComponenteyPeriodicidad(int idComponente, int periodicidad, int mes, int año)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<Management_SeguimientoEntregables_IndicadorXCompyPeridicidadResult> result = Model.GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables(mes, mes, año, idComponente, periodicidad);
            string html = "";
            if (result.Count > 0)
            {
                var obj = result.FirstOrDefault();
                html += "<span class='head'>Num entregables : </span><span>" + obj.num_entregables_programados + "</span><br/>";
                html += "<span class='head'>Oportunos : </span><span>" + obj.num_entregables_oportunos + "</span><br/>";
                html += "<span class='head'>Inoportunos : </span>" + obj.num_entregables_inoportunos + "<span></span><br/>";
            }
            else
            {
                html += "<span class='head'>Sin registros. </span>";
            }

            return html;
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 3 de febrero de 2023
        /// </summary>
        /// <param name="responsable"></param>
        /// <param name="idProceso"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public PartialViewResult _IndicadoresTiemposEntregaSegEntregables(string responsable, int? idProceso, int? año)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<Management_SeguimientoEntregables_IndicadorVencimientoResult> result = Model.GetIndicadorDiasVencimientSegEntregables(responsable, idProceso, año);
            ViewBag.usuarios = Model.GetResponsablesList();
            ViewBag.procesos = Model.Getprocesoentregable();

            ViewBag.responsable = responsable;
            ViewBag.idProceso = idProceso;
            ViewBag.año = año;

            return PartialView(result);
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 17 de enero de 2022
        /// Metodo para exportar resultados del tablero de oportunidad de entregables
        /// </summary>
        public void exportarResultadosTableroOportunidad()
        {
            try
            {
                List<Management_seguimiento_entregables_indicadoresResult> listareporte = (List<Management_seguimiento_entregables_indicadoresResult>)Session["listadoOportunidadSeguimientoEntregables"];
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Opotunidad");

                Sheet.Cells["A1:J1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(99, 99, 99);
                Sheet.Cells["A1:J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:J1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:J1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A:J"].Style.Font.Name = "century gothic";

                Sheet.Cells["A1"].Value = "Responsable";
                Sheet.Cells["B1"].Value = "Tipo entregable";
                Sheet.Cells["C1"].Value = "Periodicidad de entrega";
                Sheet.Cells["D1"].Value = "Año";
                Sheet.Cells["E1"].Value = "Nro de entregables";
                Sheet.Cells["F1"].Value = "Nro de entregables oportunos";
                Sheet.Cells["G1"].Value = "Nro de entregables inoportunos";
                Sheet.Cells["H1"].Value = "Nro de entregables con calidad";
                Sheet.Cells["I1"].Value = "Nro de entregables ampliados por el cliente";
                Sheet.Cells["J1"].Value = "Nro de entregables ampliados por ASALUD";


                int row = 2;
                foreach (Management_seguimiento_entregables_indicadoresResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.nom_persona_responsable;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.nom_tipo_entregable;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.nom_periodicidad_entrega;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.año;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.Num_entregables;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.Num_entregables_oportunos;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.Num_entregables_inoportunos;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.Num_entregables_con_calidad;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.Num_entregables_ampliados_Cliente;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.Num_entregables_ampliados_Asalud;
                    row++;
                }
                Sheet.Cells["A:J"].AutoFitColumns();


                Response.Clear();
                Response.ContentType = "application/excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteListadoOportunidadEntregables.xls");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Metodo que valida que la fecha propuesta de ampliacion no sea menor a la fecha limite del entregable y a su vez que sea una fecha que aun no haya pasado.
        /// </summary>
        /// <param name="fechaLimite"></param>
        /// <param name="idsegEntregablesPeriodo"></param>
        /// <param name="idsegEntregables"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ValidarFechaLimiteAmpliacion(DateTime fechaLimite, int? idsegEntregablesPeriodo, int idsegEntregables)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            DateTime fecha = new DateTime();

            if (idsegEntregablesPeriodo != null)
            {
                var segEntregablePeriodo = Model.GetEntregablePeriodoById(idsegEntregablesPeriodo.Value);
                fecha = segEntregablePeriodo.fecha_limite;
            }
            else
            {
                var segEntregable = Model.GetSeguimientoEntregable(idsegEntregables);
                fecha = segEntregable.fecha_control.Value;
            }

            if (fechaLimite.Date > fecha.Date && fechaLimite.Date <= DateTime.Now.Date)
            {
                return Json(new { success = false, message = "La fecha limite de ampliación no puede ser una fecha que ya pasó o el día actual." }, JsonRequestBehavior.AllowGet);
            }
            else if (fechaLimite.Date <= fecha.Date)
            {
                return Json(new { success = false, message = "La fecha limite de ampliación no puede ser menor o igual a la fecha limite actual." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, message = "Fecha valida" }, JsonRequestBehavior.AllowGet);
            }
        }

        /*Metodos en espera de confirmacion*/

        /// <summary>
        /// Tablero de evaluacion de competencias
        /// 
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        public ActionResult TableroEvaluacionCompetencias(int? mes)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<vw_seguimiento_entregables_competencias> list = Model.GetSeguimientoEntregablesCompetencias();

            if (mes != null)
            {
                list = list.Where(l => l.id_mes == mes).ToList();
            }

            ViewBag.mes = BusClass.meses();
            return View(list);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// </summary>
        /// <param name="idSegEntregablePeriodo"></param>
        /// <param name="felicitaciones"></param>
        /// <returns></returns>
        public JsonResult GuardarDatosFelicitaciones(int idSegEntregablePeriodo, string felicitaciones)
        {
            try
            {
                Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
                seguimiento_entregables_periodo_felicitaciones obj = new seguimiento_entregables_periodo_felicitaciones();
                obj.id_seguimeinto_entregable_periodo = idSegEntregablePeriodo;
                obj.observaciones = felicitaciones;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                Model.GuardarDatosFelicitaciones(obj, ref MsgRes);
                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
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
                return Json(new { success = false, message = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
            }
        }

        public string verDetallesFelicitaciones(int idPeriodoSegEntregable)
        {
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_entregables_periodo_felicitaciones felicitaciones = Model.GetFelicitacionesByIdPeriodo(idPeriodoSegEntregable);
            return "<p>" + felicitaciones.observaciones + "</p>";
        }

        #region envio de correos

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 5 de enero de 2023
        /// Metodo para enviar correo a la analista de proyectos al momento de que un usuario presente un entregable
        /// </summary>
        /// <param name="idSegEntregablePeriodo"></param>
        private void EnvioCorreoPresentacionEntregable(int idSegEntregablePeriodo)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_entregables_periodo segEntregablePeriodo = Model.GetEntregablePeriodoById(idSegEntregablePeriodo);

            try
            {
                string bdactiva = ConfigurationManager.AppSettings["BDActiva"];
                string mailBody = "";
                string mailCSS = "";
                string textBody = "";

                textBody += "<p>El sistema de gestión SAMI informa que el usuario <strong>" + SesionVar.NombreUsuario + "</strong> acaba de presentar el entregable llamado <strong>" + segEntregablePeriodo.seguimiento_entregables.Nom_entregable + "</strong> con fecha contractualmente pactada para el día <strong>" + segEntregablePeriodo.fecha_limite.ToString("dd/MM/yyyy") + "</strong></p>";
                textBody += "<p>Por favor ingrese a SAMI en el apartado de gestión y seguimiento de entregables para realizar la administración correspondiente.</p>";

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
                mailBody += "Buen día, cordial saludo.";
                mailBody += "<br />";
                mailBody += textBody;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "Cordialmente,";
                mailBody += "</div>";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<STRONG>Asalud SAS </STRONG>";
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
                mailMessage.From = new MailAddress(smtpSection.From);
                if (bdactiva == "1")
                {
                    mailMessage.To.Add("calidad@asalud.co");
                }
                else
                {
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                    mailMessage.CC.Add("sami.soporte@asalud.co");
                }

                mailMessage.Subject = "[Entregable presentado]";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();
            }

        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 5 de enero de 2023
        /// Metodo para el envio de correo cuando un entregable se acepta o se rechaza
        /// </summary>
        /// <param name="idSegEntregablePeriodo"></param>
        /// <param name="tipoAccion"></param>
        private void EnvioCorreoAceptacionRechazoEntregable(int idSegEntregablePeriodo, int tipoAccion)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_entregables_periodo segEntregablePeriodo = Model.GetEntregablePeriodoById(idSegEntregablePeriodo);
            try
            {
                string bdactiva = ConfigurationManager.AppSettings["BDActiva"];
                string mailBody = "";
                string mailCSS = "";
                string textBody = "";
                string Asunto = "";
                string correo = "";

                var personaResponsable = BusClass.GetUsuarios().Where(l => l.usuario == segEntregablePeriodo.seguimiento_entregables.persona_responsable).FirstOrDefault();
                if (personaResponsable != null)
                {
                    if (!string.IsNullOrEmpty(personaResponsable.correo_ins))
                    {
                        correo = personaResponsable.correo_ins;
                    }
                    else
                    {
                        correo = personaResponsable.correo;
                    }
                }

                if (tipoAccion == 1)
                {
                    textBody += "<p>El sistema de gestión SAMI informa que el entregable llamado <strong>" + segEntregablePeriodo.seguimiento_entregables.Nom_entregable + "</strong> con fecha contractualmente pactada para el día <strong>" + segEntregablePeriodo.fecha_limite.ToString("dd/MM/yyyy") + "</strong> ha  sido aceptado por el área de análisis de proyectos.</p>";
                    textBody += "<p>Por favor estar atento al tablero de control de entregables para que pueda hacer la entrega de estos en fechas oportunas.</p>";

                    Asunto = "[Entregable aceptado]";
                }
                else if (tipoAccion == 2)
                {
                    textBody += "<p>El sistema de gestión SAMI informa que el entregable llamado <strong>" + segEntregablePeriodo.seguimiento_entregables.Nom_entregable + "</strong> con fecha contractualmente pactada para el día <strong>" + segEntregablePeriodo.fecha_limite.ToString("dd/MM/yyyy") + "</strong> ha sido rechazado.</p>";
                    textBody += "<p>Por favor, ingresar al tablero de control de entregables para conocer las observaciones realizadas a esta entrega, y así mismo gestionar nuevamente el entregable.</p>";

                    Asunto = "[Entregable rechazado]";
                }
                else
                {
                    textBody += "<p>El sistema de gestión SAMI informa que el entregable llamado <strong>" + segEntregablePeriodo.seguimiento_entregables.Nom_entregable + "</strong> con fecha contractualmente pactada para el día <strong>" + segEntregablePeriodo.fecha_limite.ToString("dd/MM/yyyy") + "</strong> ha sido rechazado.</p>";
                    textBody += "<p>Por favor, ingresar al tablero de control de entregables para conocer las observaciones realizadas a esta entrega, y así mismo gestionar nuevamente el entregable.</p>";

                    Asunto = "[Entregable reabierto]";
                }



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
                mailBody += "Buen día, cordial saludo.";
                mailBody += "<br />";
                mailBody += textBody;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "Cordialmente,";
                mailBody += "</div>";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<STRONG>Asalud SAS </STRONG>";
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
                mailMessage.From = new MailAddress(smtpSection.From);
                if (bdactiva == "1")
                {
                    mailMessage.To.Add(correo);
                }
                else
                {
                    mailMessage.To.Add("desarrollo.soporte2@asalud.co");
                    mailMessage.CC.Add("sami.soporte@asalud.co");
                }

                mailMessage.Subject = Asunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();
            }

        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 12 de enero de 2023
        /// Metodo utilizado para enviar el correo de ampliacion de plazo para la presentacion del entregable
        /// </summary>
        /// <param name="idperiodoentregable"></param>
        private void EnvioCorreoAmpliazionPlazo(int idperiodoentregable)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            seguimiento_entregables_periodo obj = BusClass.GetEntregablePeriodoById(idperiodoentregable);

            try
            {
                string bdactiva = ConfigurationManager.AppSettings["BDActiva"];
                string mailBody = "";
                string mailCSS = "";
                string textBody = "<p>" + string.Format("SAMI Informa: {1}{0}  {2}{0}  {3}{0}  {4}{0}Link Ingreso:  {5}", Environment.NewLine, string.Empty, string.Empty, "Se ha solicitado una ampliación de plazo para el documento entregable llamado <strong>" + obj.seguimiento_entregables.Nom_entregable + "</strong> de periodicidad  <strong>" + obj.seguimiento_entregables.ref_periodicidad_entregables.nom_periodicidad_entregable + "</strong> y  fecha contractualmente pactada para el día <strong>" + obj.fecha_limite.ToString("dd/MM/yyyy") + "</strong>. La persona responsable del entregable ha propuesto presentar el documento hasta el día <strong>" + obj.fecha_limite_ampliacion.Value.ToString("dd/MM/yyyy") + "</strong>. por favor ingrese al aplicativo en el módulo de gestión y seguimiento entregables para realizar la administración correspondiente.", string.Empty, "http://ecopetrol.aplicativoasalud.co/") + "</p>";

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
                mailBody += "Buen día, cordial saludo.";
                mailBody += "<br />";
                mailBody += textBody;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "Cordialmente,";
                mailBody += "</div>";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<STRONG>Asalud SAS </STRONG>";
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
                mailMessage.From = new MailAddress(smtpSection.From);
                if (bdactiva == "1")
                {
                    mailMessage.To.Add("calidad@asalud.co");
                }
                else
                {
                    mailMessage.To.Add("desarrollo.soporte2@asalud.co");
                    mailMessage.To.Add("sami.soporte@asalud.co");
                }

                mailMessage.Subject = "[Solicitud de ampliación de plazo documento entregable]";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 5 de enero de 2023
        /// Metodo para el envío de correo cuando la solicitud de ampliacion de plazo de un entrgable se acepta o se rechaza
        /// </summary>
        /// <param name="idSegEntregablePeriodo"></param>
        /// <param name="tipoAccion"></param>
        private void EnvioCorreoAceptacionRechazoSolicitudAmpliacionPlazo(int idSegEntregablePeriodo, int tipoAccion)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            seguimiento_entregables_periodo segEntregablePeriodo = Model.GetEntregablePeriodoById(idSegEntregablePeriodo);
            try
            {
                string bdactiva = ConfigurationManager.AppSettings["BDActiva"];
                string mailBody = "";
                string mailCSS = "";
                string textBody = "";
                string Asunto = "";
                string correo = "";

                var personaResponsable = BusClass.GetUsuarios().Where(l => l.usuario == segEntregablePeriodo.seguimiento_entregables.persona_responsable).FirstOrDefault();
                if (personaResponsable != null)
                {
                    if (!string.IsNullOrEmpty(personaResponsable.correo_ins))
                    {
                        correo = personaResponsable.correo_ins;
                    }
                    else
                    {
                        correo = personaResponsable.correo;
                    }
                }

                if (tipoAccion == 1)
                {
                    textBody += "<p>El sistema de gestión SAMI informa que la solicitud de ampliación de plazo para el documento entregable llamado <strong>" + segEntregablePeriodo.seguimiento_entregables.Nom_entregable + "</strong> ha  sido aprobada por el área de análisis de proyectos.</p>";
                    textBody += "<p>Por favor ingresar al sistema en el tablero de control de entregables para realizar la gestión correspondiente </p>";

                    Asunto = "[Solicitud de ampliación de plazo documento entregable aprobada]";
                }
                else
                {
                    textBody += "<p>El sistema de gestión SAMI informa que la solicitud de ampliación de plazo realizada para el documento entregable llamado <strong>" + segEntregablePeriodo.seguimiento_entregables.Nom_entregable + "</strong> con fecha contractualmente pactada para el día <strong>" + segEntregablePeriodo.fecha_limite.ToString("dd/MM/yyyy") + "</strong> ha sido rechazada por los siguientes motivos:</p>";
                    textBody += "<p>" + segEntregablePeriodo.observaciones_ampliacion + "</p>";
                    textBody += "<p>Por favor, ingresar al tablero de control de entregables para conocer las observaciones realizadas a esta entrega, y así mismo gestionar nuevamente el entregable.</p>";

                    Asunto = "[Solicitud de ampliación de plazo documento entregable rechazada]";
                }


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
                mailBody += "Buen día, cordial saludo.";
                mailBody += "<br />";
                mailBody += textBody;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "Cordialmente,";
                mailBody += "</div>";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<STRONG>Asalud SAS </STRONG>";
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
                mailMessage.From = new MailAddress(smtpSection.From);
                if (bdactiva == "1")
                {
                    mailMessage.To.Add(correo);
                }
                else
                {
                    mailMessage.To.Add("desarrollo.soporte2@asalud.co");
                    mailMessage.CC.Add("sami.soporte@asalud.co");
                }

                mailMessage.Subject = Asunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de diciembre de 2022
        /// Metodo utilizado para el envio de notificaciones diarias de los entregables que estan proximos a vencer
        /// </summary>
        [HttpPost]
        public void EnvioCorreoEntrablesProximosaVencer()
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string bdactiva = ConfigurationManager.AppSettings["BDActiva"];
            string mailBody = "";
            string mailCSS = "";

            Models.SeguimientoEntregables.SeguimientoEntregables Model = new Models.SeguimientoEntregables.SeguimientoEntregables();
            List<seguimiento_entregables_alerta_diaria> alertaEnviada = Model.GetListNotificacionesEnviadasSeguimientoEntregables(DateTime.Now);
            if (alertaEnviada.Count == 0)
            {
                List<vw_seguimiento_entregables> segEntregables = Model.GetListEntregables(null).Where(l => l.diferencia_dias <= 2 && l.diferencia_dias >= 1).ToList();
                if (segEntregables.Count > 0)
                {
                    List<string> usuarios = segEntregables.Select(l => l.persona_responsable).Distinct().ToList();
                    foreach (string user in usuarios)
                    {
                        var listEntregables = segEntregables.Where(l => l.persona_responsable == user).ToList();

                        string correo = listEntregables.FirstOrDefault().correo_ins;

                        if (string.IsNullOrEmpty(correo))
                        {
                            correo = listEntregables.FirstOrDefault().correo;
                        }

                        try
                        {
                            string textBody = "<p>SAMI informa que usted tiene a cargo los siguientes entregables que estan próximos a vencer:</p>";
                            textBody += "<ul>";
                            foreach (var item in listEntregables)
                            {
                                textBody += "<li>" + item.Nom_entregable + "</li>";
                            }
                            textBody += "</ul>";
                            textBody += "<p>Por favor ingrese a SAMI en el tablero de control de entregables para realizar la gestión correspondiente.</p>";

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
                            mailBody += "Cordialmente,";
                            mailBody += "</div>";
                            mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                            mailBody += "<br />";
                            mailBody += "<img src='cid:dealer_logo' />";
                            mailBody += "<br />";
                            mailBody += "<STRONG>Asalud SAS </STRONG>";
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
                            mailMessage.From = new MailAddress(smtpSection.From);
                            if (bdactiva == "1")
                            {
                                mailMessage.To.Add(correo);
                                mailMessage.CC.Add("analistadeproyectos@asalud.co");
                            }
                            else
                            {
                                mailMessage.To.Add("desarrollo.soporte2@asalud.co");
                                mailMessage.CC.Add("sami.soporte@asalud.co");
                            }

                            mailMessage.Subject = "[Documentos entregables próximos a vencer]";
                            mailMessage.IsBodyHtml = true;
                            mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                            mailMessage.IsBodyHtml = true;
                            objMail.Send(mailMessage);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                            ModelState.Clear();
                        }
                    }

                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        seguimiento_entregables_alerta_diaria obj = new seguimiento_entregables_alerta_diaria();
                        obj.fecha_envio_notificacion = DateTime.Now;
                        db.seguimiento_entregables_alerta_diaria.InsertOnSubmit(obj);
                        db.SubmitChanges();
                    }
                }
            }
        }


        #endregion
    }
}