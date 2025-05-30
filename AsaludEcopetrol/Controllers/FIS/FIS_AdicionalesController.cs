using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Linq;
using System.Threading.Tasks;

using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using Aspose.Cells;
using ClosedXML.Excel;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace AsaludEcopetrol.Controllers.FIS
{
    [SessionExpireFilter]

    public class FIS_AdicionalesController : Controller
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
        
        public ActionResult TraerDocumentosGOLD()
        {
            return View();
        }

        public JsonResult ListadoDocumentosGols(string nit, string numFactura)
        {
            var respuesta = "";
            List<string> listadoRutas = new List<string>();
            var tablaContenido = "";

            try
            {
                //Local
                //string apiUrl = $"https://localhost:44386/api/Blop/ConsultaArchivosBlop?nit=" + nit + "&numFactura=" + numFactura;

                //Pruebas
                string apiUrl = $"https://wssapvimpruebas.aplicativoasalud.co/api/Blop/ConsultaArchivosBlop?nit=" + nit + "&numFactura=" + numFactura;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "GET";
                request.ContentType = "application/json";

                try
                {

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string jsonResponse = reader.ReadToEnd();
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            var apiResponse = serializer.Deserialize<Dictionary<string, object>>(jsonResponse);

                            if (apiResponse.ContainsKey("rutas"))
                            {
                                listadoRutas = serializer.ConvertToType<List<string>>(apiResponse["rutas"]);
                                respuesta = $"Se encontraron {listadoRutas.Count} archivos.";
                            }
                            else
                            {
                                respuesta = "No se encontraron archivos.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    tablaContenido += "<tr>";
                    tablaContenido += "<td colspan='1' style='width: 100%; text-align:center'>NO SE ENCONTRARON DOCUMENTOS</td>";
                    tablaContenido += "<td> ";
                    tablaContenido += "</td> ";
                    tablaContenido += "</tr>";

                    throw new Exception(error);
                }

                if (listadoRutas.Count() > 0)
                {
                    foreach (var item in listadoRutas)
                    {
                        string[] descomposicionArchivo = item.Split('\\');
                        int longitud = descomposicionArchivo.Length;
                        string nombreArchivo = descomposicionArchivo[longitud - 1];

                        var ruta = item.Replace("\\", "/");

                        tablaContenido += "<tr>";
                        tablaContenido += "<td> ";

                        tablaContenido += $"<a onclick='previsualizarArchivo(\"{ruta}\")' class='archivo-link'>{nombreArchivo}</a>";

                        tablaContenido += "</td> ";
                        tablaContenido += "</tr>";
                    }
                }
                else
                {
                    tablaContenido += "<tr>";
                    tablaContenido += "<td colspan='1' style='width: 100%; text-align:center'>NO SE ENCONTRARON DOCUMENTOS</td>";
                    tablaContenido += "<td> ";
                    tablaContenido += "</td> ";

                    tablaContenido += "</tr>";
                }
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("Error en el servidor remoto: (404) No se encontró."))
                {
                    return Json(new { rta = 0, mensaje = "Sin archivos para este nit y número de factura", tablaContenido = tablaContenido }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { rta = 0, mensaje = ex.Message, tablaContenido = tablaContenido }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { rta = 1, rutas = listadoRutas, tablaContenido = tablaContenido }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult VerArchivoBlop(string ruta)
        {
            try
            {
                var rutaDirecta = ruta.Replace("", "/").Replace("\\", "/").Replace("", "");
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, rutaDirecta);

                if (System.IO.File.Exists(dirpath))
                {
                    string extension = Path.GetExtension(dirpath).ToLower(); // Obtener extensión del archivo
                    string contentType;
                    bool isInline = false; // Para previsualizar en el navegador

                    switch (extension)
                    {
                        case ".pdf":
                            contentType = "application/pdf";
                            isInline = true;
                            break;

                        case ".json":
                        case ".xml":
                            contentType = "application/json";
                            isInline = true;
                            break;

                        //contentType = "application/xml";
                        //isInline = true;
                        //break;
                        case ".zip":
                            contentType = "application/zip";
                            break;
                        default:
                            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Formato de archivo no soportado." });
                    }

                    byte[] bytes = System.IO.File.ReadAllBytes(dirpath);

                    Response.Clear();
                    Response.ContentType = contentType;
                    Response.AddHeader("Content-Length", bytes.Length.ToString());

                    if (isInline)
                    {
                        // Previsualización en el navegador
                        Response.AddHeader("Content-Disposition", "inline; filename=" + Path.GetFileName(dirpath));
                    }
                    else
                    {
                        // Descarga del ZIP u otros formatos no compatibles con inline
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(dirpath));
                    }

                    Response.BinaryWrite(bytes);
                    Response.End();
                    return new EmptyResult();
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al mostrar el archivo: " + ex.Message });
            }
        }
        
        public PartialViewResult MostrarDocumentoRuta(string ruta)
        {
            ViewBag.ruta = ruta;
            return PartialView();
        }
    }
}



