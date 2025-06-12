using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using AsaludEcopetrol.Models;

using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.html;
using SelectPdf;
using iTextSharp.text.pdf.parser;
using System.Diagnostics;



using Google.Apis.Pagespeedonline.v5;
using Google.Apis.Pagespeedonline.v5.Data;
using Google.Apis.Services;
using ECOPETROL_COMMON.ENTIDADES;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace AsaludEcopetrol.Controllers.LecturaPDFPrueba
{

    [SessionExpireFilter]

    [Route("[controller]")]

    public class LecturaPdfController : Controller
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

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
        Facade BusClass = new Facade();

        #endregion

        // GET: LecturaPdf
        public ActionResult CargueDocumento()
        {
            //hunspell = new Hunspell("~/App_Data/es_ES.aff", "~/App_Data/es_ES.dic");
            ViewBag.rta = 0;
            ViewBag.msg = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueDocumento(HttpPostedFileBase file, string numero)
        {
            var rta = 0;
            var numeroPagina = "";
            var mensaje = "";

            try
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        // Ruta temporal para guardar el archivo PDF cargado
                        var filePath = System.IO.Path.GetTempFileName();

                        // Guardar el archivo PDF en la ruta temporal
                        file.SaveAs(filePath);

                        // Abrir el archivo PDF
                        using (PdfReader reader = new PdfReader(filePath))
                        {
                            // Recorrer las páginas del PDF
                            for (int pageNum = 1; pageNum <= reader.NumberOfPages; pageNum++)
                            {
                                // Extraer el texto de la página actual
                                string text = PdfTextExtractor.GetTextFromPage(reader, pageNum);

                                text = text.Replace(" ", "");
                                text = text.Replace("\n", "");

                                // Verificar si el número de factura está presente en el texto de la página
                                if (text.Contains(numero))
                                {
                                    if (numeroPagina == "")
                                    {
                                        numeroPagina += Convert.ToString(pageNum);
                                    }
                                    else
                                    {
                                        numeroPagina += " - ";
                                        numeroPagina += Convert.ToString(pageNum);
                                    }

                                    rta = 1;
                                }
                            }

                            if (numeroPagina == "")
                            {
                                mensaje = "No se encuentra el número de factura en el documento";
                                rta = 2;
                            }
                            else
                            {
                                mensaje = "Número de factura encontrado en página(s): " + numeroPagina;
                            }
                        }

                        // Eliminar el archivo PDF temporal
                        System.IO.File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.msg = mensaje;
            ViewBag.rta = rta;

            return View();
        }

        public ActionResult VistaPrueba()
        {
            try
            {
                var service = new PagespeedonlineService(new BaseClientService.Initializer
                {
                    ApiKey = "AIzaSyDFgoHA5uK0JRwPmhCYbjIdMXevty_cq0Q"
                });

                var urlToAnalyze = "https://ecopetrol.aplicativoasalud.co";
                var request = service.Pagespeedapi.Runpagespeed(urlToAnalyze);
                var request2 = service.Features;
                request.Strategy = PagespeedapiResource.RunpagespeedRequest.StrategyEnum.Desktop; // O "mobile" según tu preferencia
                request.Category = PagespeedapiResource.RunpagespeedRequest.CategoryEnum.Performance;

                // Ejecutar la solicitud
                //var result = request.Execute();

                //var AnalysisUTCTimestamp = result.AnalysisUTCTimestamp;
                //var CaptchaResult = result.CaptchaResult;
                //var ETag = result.ETag;
                //var Kind = result.Kind;
                //var OriginLoadingExperience = result.OriginLoadingExperience;

                //var id = result.Id;
                //var version = result.Version;
                //var loadingExperience = result.LoadingExperience;

                //// Obtener los datos de rendimiento, accesibilidad, buenas prácticas y SEO
                //var performanceScore = result.LighthouseResult.Categories.Performance?.Score;
                //var accessibilityScore = result.LighthouseResult.Categories.Accessibility?.Score;
                //var bestPracticesScore = result.LighthouseResult.Categories.BestPractices?.Score;
                //var seoScore = result.LighthouseResult.Categories.Seo?.Score;

                //// Asignar los datos a la ViewBag para mostrar en la vista
                //ViewBag.Id = id;
                //ViewBag.AnalysisUTCTimestamp = AnalysisUTCTimestamp;
                //ViewBag.CaptchaResult = CaptchaResult;
                //ViewBag.ETag = ETag;
                //ViewBag.Kind = Kind;
                //ViewBag.OriginLoadingExperience = OriginLoadingExperience;
                //ViewBag.Version = version;
                //ViewBag.LoadingExperience = loadingExperience;
                //ViewBag.PerformanceScore = performanceScore;
                //ViewBag.AccessibilityScore = accessibilityScore;
                //ViewBag.BestPracticesScore = bestPracticesScore;
                //ViewBag.SEOScore = seoScore;

                //return View(result);









            }
            catch (Exception ex)
            {
                var error = ex.Message;
                // Manejar cualquier error que ocurra durante la ejecución
                ViewBag.ErrorMessage = error;
            }

            return View();
        }

        public ActionResult GenerarPerfil()
        {
            try
            {

                int PID = 0;
                string nombreProcesoServidorWeb = "devenv";

                // Buscar el proceso del servidor web por nombre
                Process[] procesosServidorWeb = Process.GetProcessesByName(nombreProcesoServidorWeb);

                if (procesosServidorWeb.Length > 0)
                {
                    // Obtener el proceso de la aplicación web actualmente en ejecución
                    Process procesoAppWeb = procesosServidorWeb[0];
                    // Obtener el PID del proceso de la aplicación web
                    PID = procesoAppWeb.Id;
                }

                if (PID != 0)
                {
                    // Ruta al ejecutable de DotTrace CommandLine Tools
                    string dotTraceExecutable = @"D:\proyectos\asalud ecopetrol\packages\JetBrains.dotTrace.CommandLineTools.windows-x64.2024.1.0\tools\dotTrace.exe";

                    // Comando para generar el perfil de rendimiento
                    string comando = "collect --type=Sampling --output=procesoPerformanceAsalud.dtp --attach-to-process=36148";

                    // Ejecutar el comando de DotTrace
                    EjecutarComando(dotTraceExecutable, comando);

                    // Redirigir a la acción para mostrar los resultados del perfil
                    return RedirectToAction("MostrarResultados");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error al generar el perfil de rendimiento: No se encuentra codigo PID";
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la ejecución del comando
                ViewBag.ErrorMessage = "Error al generar el perfil de rendimiento: " + ex.Message;
            }

            return View();
        }

        public ActionResult MostrarResultados()
        {
            // Ruta al archivo de perfil de rendimiento generado por DotTrace
            string rutaArchivoPerfil = Server.MapPath("~/procesoPerformanceAsalud.dtp");

            // Verificar si el archivo existe
            try
            {
                if (!System.IO.File.Exists(rutaArchivoPerfil))
                {
                    ViewBag.ErrorMessage = "El archivo de perfil de rendimiento no se encontró.";
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la lectura de los datos del perfil
                ViewBag.ErrorMessage = "Error al cargar los datos del perfil de rendimiento: " + ex.Message;
            }

            return View();
        }

        private void EjecutarComando(string rutaEjecutable, string comando)
        {

            try
            {
                // Configurar el proceso de inicio para ejecutar el comando
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = rutaEjecutable,
                    Arguments = comando,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                // Iniciar el proceso
                Process proceso = new Process
                {
                    StartInfo = startInfo
                };
                proceso.Start();
                proceso.WaitForExit();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public ActionResult ChatBot()
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

                    result = "<label class='text-secondary_asalud'>Selecciona el proyecto</label>";
                    result += "<input type='hidden' id='tipoPreguntaAnterior' name='tipoPreguntaAnterior' value='" + 0 + "'>";

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

                    result = "<label class='text-secondary_asalud'>Selecciona el proceso</label>";
                    result += "<input type='hidden' id='tipoPreguntaAnterior' name='tipoPreguntaAnterior' value='" + (tipo - 1) + "'>";

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

                    result = "<label class='text-secondary_asalud'>Selecciona el sub proceso</label>";

                    result += " <div class='col-md-12 text-center'>";
                    result += "<a class='btn botonAnterior' onclick='LlenarOpcionesMensaje(1, " + idProceso + ")'> Volver al menú anterior </a>";
                    result += " </div>";
                    result += " <br />";
                    result += " <br />";

                    result += "<input type='hidden' id='tipoPreguntaAnterior' name='tipoPreguntaAnterior' value='" + (tipo - 1) + "'>";

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

                    result += "<input type='hidden' id='tipoPreguntaAnterior' name='tipoPreguntaAnterior' value='" + (tipo - 1) + "'>";

                    foreach (var item in respuestas)
                    {
                        result += " <div class='col-md-12'></div>";
                        result += " <br />";
                        result += " <div class='col-md-12 text-left respuestasChat'>";
                        result += " <span>" + item.descripcion + "</span>";
                        //result += " <span class='text-secondary_asalud'>" + item.descripcion + "</span>";
                        result += " </div>";
                        result += " <br />";

                        //Abierta
                        if (item.tipoRespuesta == 1)
                        {
                            List<Management_chatbot_ref_respuestas_archivosResult> respuestasArchivos = new List<Management_chatbot_ref_respuestas_archivosResult>();

                            respuestasArchivos = BusClass.ChatBotRespuestasArchivos(item.id_chatbot_ref_respuesta);

                            foreach (var archivo in respuestasArchivos)
                            {
                                //Imagen
                                if (item.tipoArchivo == 1)
                                {
                                    var rutaArchivoCompleta = archivo.ruta;
                                    var rutaArchivo = rutaArchivoCompleta.Split('\\');

                                    var rutaFinal = "";

                                    foreach (var rutParcial in rutaArchivo)
                                    {
                                        if (rutParcial == "Resources")
                                        {
                                            if (rutaFinal == "")
                                            {
                                                rutaFinal = rutParcial;
                                            }
                                            else
                                            {
                                                rutaFinal += "\\" + rutParcial;
                                            }
                                        }

                                        if (rutParcial == "imagenesChatBot")
                                        {
                                            rutaFinal += "\\" + rutParcial;
                                        }
                                    }

                                    rutaFinal += "\\" + rutaArchivo[7];

                                    rutaFinal = rutaFinal.Replace("\\", "/");

                                    result += " <div class='col-md-12 text-center contenidoImagen' id='DivImagen_" + archivo.id_archivo + "'>";
                                    result += " <input type='hidden' id='imgAbierta_" + archivo.id_archivo + "' name='" + archivo.id_archivo + "'/> ";
                                    result += " <img src='../" + rutaFinal + "' alt='Imagen' id='imagenMostrada_" + archivo.id_archivo + "' onclick='AgrandarImagen(" + archivo.id_archivo + ")'>";
                                    result += " </div>";
                                    result += " <br />";
                                    result += " <br />";
                                    result += " <br />";
                                }
                                //PDF
                                else
                                {
                                    result += " <div class='col-md-12 text-center'>";
                                    result += " <button role='button' onclick='DescargarArchivo(" + archivo.id_archivo + ")'class='btn-sm button_Asalud_descargas'> <i class='glyphicon glyphicon-download'></i>&nbsp; Descargar</button>";
                                    result += " </div>";
                                    result += " <br />";
                                    result += " <br />";
                                    result += " <br />";
                                }
                            }
                        }
                        //Cerrada
                        else
                        {
                            result += " <div class='col-md-12 text-center'>";
                            result += " <textarea id='RespuestaCerradaCorreo_" + item.id_chatbot_ref_respuesta + "' class='form-control' name='RespuestaCerradaCorreo_" + item.id_chatbot_ref_respuesta + "' maxlength='500' required> </textarea>";
                            result += " </div>";
                            result += " <br />";

                            result += " <div class='col-md-12 text-center'>";
                            result += " <a class='btn btn-sm button_Asalud_Aceptar botonEnviar' onclick='EnviarCorreoaSAMI(" + item.id_chatbot_ref_respuesta + ")'><span style='color: #fff;'>ENVIAR</span></a>";
                            result += " </div>";
                            result += " <br />";
                            result += " <br />";
                            result += " <br />";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public void DescargarArchivoChatBot(int? idArchivo)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

            chatbot_ref_respuestas_archivos obj = new chatbot_ref_respuestas_archivos();
            try
            {
                obj = BusClass.TraerArchivoChatBot(idArchivo);
                if (obj == null)
                {
                    Response.Write("<script language=javascript>alert('El archivo ya no se encuentra en ruta');</script>");
                    return;
                }

                if (!string.IsNullOrEmpty(obj.ruta))
                {
                    var fileName = System.IO.Path.GetFileName(obj.ruta);
                    var extension = System.IO.Path.GetExtension(obj.ruta);

                    Response.Clear();
                    Response.ContentType = extension;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.TransmitFile(obj.ruta);
                    Response.Flush();
                }
                else
                {
                    Response.Write("<script language=javascript>alert('No existe el archivo');</script>");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Response.Write("<script language=javascript>alert('Error al mostrar el archivo');</script>");
            }
        }

        public ActionResult AdministrarChatBot()
        {
            ViewBag.procesos = BusClass.ChatBotProcesos(1);
            return View();
        }

        public string ObtenerProcesos(int? proyecto)
        {
            string resultado = "<option value=''>- Seleccionar -</option>";

            List<Management_chatbot_ref_procesosResult> procesos = new List<Management_chatbot_ref_procesosResult>();
            try
            {
                procesos = BusClass.ChatBotProcesos(proyecto);


                foreach (var item in procesos)
                {
                    resultado += "<option value='" + item.id_chatbot_ref_proceso + "'>" + item.descripcion + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return resultado;
        }

        public string ObtenerSubProcesos(int? proceso)
        {
            string resultado = "<option value=''>- Seleccionar -</option>";

            List<Management_chatbot_ref_subprocesosResult> subprocesos = new List<Management_chatbot_ref_subprocesosResult>();
            try
            {
                subprocesos = BusClass.ChatBotSubProcesos(proceso);

                foreach (var item in subprocesos)
                {
                    resultado += "<option value='" + item.id_chatbot_ref_subproceso + "'>" + item.descripcion + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return resultado;
        }

        public string ObtenerPreguntas(int? subProceso)
        {
            string resultado = "<option value=''>- Seleccionar -</option>";

            List<Management_chatbot_ref_preguntasResult> preguntas = new List<Management_chatbot_ref_preguntasResult>();
            try
            {
                preguntas = BusClass.ChatBotPreguntas(subProceso);

                foreach (var item in preguntas)
                {
                    resultado += "<option value='" + item.id_chatbot_ref_pregunta + "'>" + item.descripcion + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return resultado;
        }

        public JsonResult AgregarProcesoNuevo(string procesoNuevo, int? proyecto)
        {
            var mensaje = "";
            var rta = 0;
            var idProceso = 0;
            try
            {
                chatbot_ref_procesos dato = new chatbot_ref_procesos();
                dato.descripcion = procesoNuevo;
                dato.id_chatbot_ref_proyecto = (int)proyecto;
                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;

                idProceso = BusClass.ChatBotInsertarProceso(dato);
                if (idProceso != 0)
                {
                    mensaje = "PROCESO INGRESADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DEL PROCESO";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DEL PROCESO: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta, idProceso = idProceso });
        }

        public JsonResult AgregarSubProcesoNuevo(string subprocesoNuevo, int? proceso)
        {
            var mensaje = "";
            var rta = 0;
            var idSubProceso = 0;

            try
            {
                chatbot_ref_subprocesos dato = new chatbot_ref_subprocesos();
                dato.descripcion = subprocesoNuevo;
                dato.id_chatbot_ref_proceso = (int)proceso;
                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;

                idSubProceso = BusClass.ChatBotInsertarSubProceso(dato);
                if (idSubProceso != 0)
                {
                    mensaje = "SUB PROCESO INGRESADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DEL SUB PROCESO";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DEL SUB PROCESO: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta, idSubProceso = idSubProceso });
        }

        public JsonResult AgregarpreguntaNueva(string preguntaNueva, int? subProceso)
        {
            var mensaje = "";
            var rta = 0;
            var idPregunta = 0;

            try
            {
                chatbot_ref_preguntas dato = new chatbot_ref_preguntas();
                dato.descripcion = preguntaNueva;
                dato.id_chatbot_ref_subproceso = (int)subProceso;
                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;

                idPregunta = BusClass.ChatBotInsertarPreguntas(dato);
                if (idPregunta != 0)
                {
                    mensaje = "PREGUNTA INGRESADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE LA PREGUNTA";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE LA PREGUNTA: " + error;
                ;
            }

            return Json(new { mensaje = mensaje, rta = rta, idPregunta = idPregunta });
        }

        public JsonResult GuardarCreacionRespuestas(int? idPregunta, int? tipoRespuesta, int? tipoArchivo, string respuesta, List<HttpPostedFileBase> archivos)
        {
            var mensaje = "";
            var rta = 0;
            var idRespuesta = 0;

            try
            {
                chatbot_ref_respuestas dato = new chatbot_ref_respuestas();
                dato.id_chatbot_ref_pregunta = (int)idPregunta;
                dato.descripcion = respuesta;
                dato.tipoArchivo = tipoArchivo;
                dato.tipoRespuesta = tipoRespuesta;
                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;

                idRespuesta = BusClass.ChatBotInsertarRespuestas(dato);
                if (idRespuesta != 0)
                {
                    //Abierta
                    if (tipoRespuesta == 1)
                    {
                        if (archivos.Count() > 0)
                        {
                            for (var i = 0; i < archivos.Count(); i++)
                            {
                                HttpPostedFileBase archivo = archivos[i];
                                var guardadoArchivo = GuardarArchivoRespuesta(idPregunta, idRespuesta, tipoArchivo, archivo);

                                if (guardadoArchivo != "")
                                {
                                    throw new Exception("ERROR AL INGRESAR EL ARCHIVO: " + archivo.FileName);
                                }
                            }

                            mensaje = "ARCHIVOS GUARDADOS CORRECTAMENTE";
                        }
                    }

                    mensaje = "RESPUESTA INGRESADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE LA RESPUESTA";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE LA RESPUESTA: " + error;
                ;
            }

            return Json(new { mensaje = mensaje, rta = rta, idPregunta = idPregunta });
        }

        private string GuardarArchivoRespuesta(int? idPregunta, int? idRespuesta, int? tipoArchivo, HttpPostedFileBase file)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosChatBot"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosChatBot"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = System.IO.Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = System.IO.Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "ArchivosChatBot";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "ArchivosChatBot_pruebas";
                }

                ruta = System.IO.Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idRespuesta);
                var nombre = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, System.IO.Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                file.SaveAs(archivo);
                var rutaNueva = "";

                if (tipoArchivo == 1)
                {
                    //var nombreArchivo = "../Resources/imagenesChatBot/" + file.FileName;
                    string directorio = Server.MapPath("~/Resources/imagenesChatBot"); // Obtener la ruta física del directorio

                    rutaNueva = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", directorio,
                    fecha, nombre, System.IO.Path.GetExtension(file.FileName));

                    if (!Directory.Exists(directorio))
                        Directory.CreateDirectory(directorio);
                    //string rutaNueva = System.IO.Path.Combine(directorio, file.FileName); // Combinar directorio y nombre del archivo
                    file.SaveAs(rutaNueva); // Guardar el archivo en la ruta especificada
                }
                else
                {
                    rutaNueva = archivo;
                }

                chatbot_ref_respuestas_archivos OBJ = new chatbot_ref_respuestas_archivos();

                //OBJ.id_pregunta = idPregunta;
                OBJ.id_respuesta = idRespuesta;
                OBJ.tipo_archivo = tipoArchivo;
                OBJ.nombre_archivo = nombre;
                OBJ.ruta = Convert.ToString(rutaNueva);
                OBJ.extension = file.ContentType;
                OBJ.fecha_digita = DateTime.Now;
                OBJ.usuario_digita = SesionVar.UserName;

                var respuesta = BusClass.ChatBotInsertarRespuestasArchivos(OBJ);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                strError = error;
            }

            return strError;
        }

        [ValidateInput(false)]
        public JsonResult EnviarCorreoSoporte(string correo, int? idRespuesta)
        {
            var mensaje = "";
            var rta = 0;
            chatbot_ref_respuestas respuesta = new chatbot_ref_respuestas();
            chatbot_ref_preguntas pregunta = new chatbot_ref_preguntas();
            chatbot_ref_subprocesos subProceso = new chatbot_ref_subprocesos();
            chatbot_ref_procesos proceso = new chatbot_ref_procesos();
            chatbot_ref_proyectos proyecto = new chatbot_ref_proyectos();

            try
            {
                respuesta = BusClass.TraerRespuestaId(idRespuesta);
                if (respuesta != null)
                {
                    pregunta = BusClass.TraerPreguntaId(respuesta.id_chatbot_ref_pregunta);
                    if (pregunta != null)
                    {
                        subProceso = BusClass.TraerSubProcesoId(pregunta.id_chatbot_ref_subproceso);
                        if (subProceso != null)
                        {
                            proceso = BusClass.TraerProcesoId(subProceso.id_chatbot_ref_proceso);
                            if (proceso != null)
                            {
                                proyecto = BusClass.TraerProyectoId(proceso.id_chatbot_ref_proyecto);
                            }
                        }
                    }
                }


                var usuario = SesionVar.NombreUsuario;

                var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                string filename = "";

                StringBuilder sb = new StringBuilder();

                string encabezado = "<strong> El usuario: </strong>" + usuario + " ha enviado un chat.";
                string ruta = "<br/>";
                ruta += "<strong> Proyecto: </strong>" + proyecto.descripcion;
                ruta += "<br/>";
                ruta += "<strong> Proceso: </strong> " + proceso.descripcion;
                ruta += "<br/>";
                ruta += "<strong> Sub proceso: </strong>" + subProceso.descripcion;
                ruta += "<br/>";
                ruta += "<strong> Pregunta: </strong>" + pregunta.descripcion;
                ruta += "<br/>";
                ruta += "<strong> Respuesta: </strong>" + respuesta.descripcion;

                string textBody = correo;

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
                mailBody += encabezado;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += ruta;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += textBody;
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "<br />";
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
                    mailMessage.To.Add("sami.soporte@asalud.co");
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    mailMessage.From = new MailAddress("admin@asaludltda.com");
                    mailMessage.To.Add("sami.soporte@asalud.co");
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                }

                mailMessage.Subject = "[Mensaje Automático]" + " " + "CHAT BOT - " + usuario;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                mensaje = "CORREO ENVIADO CORRECTAMENTE";
                rta = 1;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "Lo siento, estamos enfrentando problemas aquí: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }
    }
}