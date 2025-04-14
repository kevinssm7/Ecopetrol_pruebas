using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using Aspose.Cells;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using Facede;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using Microsoft.IO;
using System.Data.Linq;
using System.IO;
using Ionic.Zip;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Configuration;
using System.Runtime.Caching;


using System.IO;
using System.IO.Compression;


using System.Text;
using System.Net.Mail;

namespace AsaludEcopetrol.Controllers.CuentasMedicas
{
    [SessionExpireFilter]
    public class InventarioContabilizadasController : Controller
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
        Models.CuentasMedicas.InventarioContabilizadas Model = new Models.CuentasMedicas.InventarioContabilizadas();
        #endregion

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Vista o pantalla principal donde se realiza el cargue masivo de las facturas contabilizadas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CargueInventarioFacturasContabilizadas()
        {
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Metodo post donde se lee el excel y se insertan los registros en base de datos.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="regional"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CargueInventarioFacturasContabilizadas(HttpPostedFileBase file, int mes, int año, int regional)
        {
            try
            {
                inventario_facturas_contabilizadas_carguebase obj = Model.ConsultarExistenciaPeriodoCargado(mes, año, regional);
                if (obj == null)
                {
                    CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                    var asposeOptions = new Aspose.Cells.LoadOptions { MemorySetting = MemorySetting.MemoryPreference };
                    Workbook wb = new Workbook(file.InputStream, asposeOptions);
                    Worksheet worksheet = wb.Worksheets[0];
                    Cells cells = worksheet.Cells;
                    var ExportTableOptions = new Aspose.Cells.ExportTableOptions { CheckMixedValueType = false, ExportColumnName = true, ExportAsString = true };
                    DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

                    obj = new inventario_facturas_contabilizadas_carguebase();
                    obj.mes = mes;
                    obj.año = año;
                    obj.regional = regional;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    Model.GuardarInventarioFacturasContabilizadas(obj, dataTable, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["rta"] = 1;
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                    }

                    ViewData["Msg"] = MsgRes.DescriptionResponse;
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = "¡Lo sentimos! Ya existe un inventario de facturas contabilizadas cargado en este mismo periodo para esta misma regional.";
                }
            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = "Ha ocurrido un error al momento de procesar la solicitud. Error:" + ex.Message;
            }

            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            return View();
        }

        /// <summary>
        /// Autor: Alexis Quilñones Castillo
        /// Fecha: 28 de noviembre de 2022
        /// Vista html que muestra el tablero de control de inventario facturacion
        /// </summary>
        /// <returns></returns>
        public ActionResult TableroControlInventarioFacturacion()
        {
            List<Ref_regional> region = new List<Ref_regional>();
            region = BusClass.GetRefRegion();
            ViewBag.regional = region;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <param name="regional"></param>
        /// <returns></returns>
        public PartialViewResult _verTableroControlInventarioFacturacion(DateTime? fechaIni, DateTime? fechaFin, int? regional)
        {
            List<Management_inventario_facturas_contabilizadasResult> result = new List<Management_inventario_facturas_contabilizadasResult>();
            List<Management_inventario_facturas_contabilizadasResult> listaFinal = new List<Management_inventario_facturas_contabilizadasResult>();

            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;

            try
            {
                List<Management_inventario_facturas_contabilizadasResult> ListadoFacturasAuditor = cache["facturasArchivo" + nomUsuario] as List<Management_inventario_facturas_contabilizadasResult>;

                DateTime? cacheFechaInicio = (DateTime?)cache["fechaInicio" + nomUsuario];
                DateTime? cacheFechaFinal = (DateTime?)cache["fechaFinal" + nomUsuario];
                int? cacheRegional = (int?)cache["regional" + nomUsuario];

                List<Management_inventario_facturas_contabilizadasResult> listInventarioFacturas = new List<Management_inventario_facturas_contabilizadasResult>();

                if (ListadoFacturasAuditor == null || ListadoFacturasAuditor.Count() == 0)
                {
                    /*Consulta en base de datos*/
                    if (fechaIni != null && fechaFin != null && regional != null)
                    {
                        cache["fechaInicio" + nomUsuario] = fechaIni;
                        cache["fechaFinal" + nomUsuario] = fechaFin;
                        cache["regional" + nomUsuario] = regional;

                        listInventarioFacturas = Model.ConsultarInventarioFacturasPorEstado(1, fechaIni, fechaFin, regional, ref MsgRes);
                    }

                    /*Se definen las politicas y se insertan en cache los resultados de la consulta hecha a base de datos*/
                    DateTime expirationDate = DateTime.Now.AddHours(10);
                    CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expirationDate };
                    cache.Set("facturasArchivo" + nomUsuario, listInventarioFacturas, policy);
                    cache.Set("tiempoExpiracionMemoria" + nomUsuario, expirationDate, policy);
                }
                else
                {

                    if ((fechaIni != cacheFechaInicio) || (fechaFin != cacheFechaFinal) || (regional != cacheRegional))
                    {
                        /*Consulta en base de datos*/
                        if (fechaIni != null && fechaFin != null && regional != null)
                        {
                            cache["fechaInicio" + nomUsuario] = fechaIni;
                            cache["fechaFinal" + nomUsuario] = fechaFin;
                            cache["regional" + nomUsuario] = regional;
                            listInventarioFacturas = Model.ConsultarInventarioFacturasPorEstado(1, fechaIni, fechaFin, regional, ref MsgRes);
                        }
                        else
                        {
                            listInventarioFacturas = new List<Management_inventario_facturas_contabilizadasResult>();
                        }
                    }
                    else
                    {
                        /*Se setea en el listado de resultados lo que aun hay en cache*/
                        listInventarioFacturas = ListadoFacturasAuditor;
                    }
                }

                if (SesionVar.ROL != "1")
                {
                    List<sis_auditor_regional> list = BusClass.GetRegionalAuditor().Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                    foreach (var item in list)
                    {
                        var listFacturas = listInventarioFacturas.Where(x => x.regional == item.id_regional).ToList();
                        if (listFacturas.Count > 0)
                        {
                            result.AddRange(listFacturas);
                        }
                    }

                    listaFinal = result;
                    Session["inventarioArchivo"] = listaFinal;
                    return PartialView(listaFinal);
                }
                else
                {
                    listaFinal = listInventarioFacturas;
                    Session["inventarioArchivo"] = listaFinal;
                    return PartialView(listaFinal);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return PartialView(listaFinal);
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 28 de noviembre de 2022
        /// Metodo para descargar en un archivo zip el conjuto de facturas y soportes.
        /// </summary>
        /// <param name="idDetalle"></param>
        public ActionResult DescargarSoportesFactura(Int32 idDetalle)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managment_prestadores_documentosResult> docs = Model.GetSoportesList(idDetalle);
            try
            {
                if (docs.Count == 0)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "La factura no contiene documentos." });
                }
                else
                {
                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        int count = 0;
                        foreach (var item in docs)
                        {
                            string dirpath = Path.Combine(Request.PhysicalApplicationPath, item.ruta);
                            byte[] bytes = System.IO.File.ReadAllBytes(dirpath);

                            if (item.tipo.Contains("ZIP"))
                            {
                                zip.AddEntry(item.nombre + ".zip", bytes);
                            }
                            else
                            {
                                zip.AddEntry(item.nombre + ".pdf", bytes);
                            }
                            count++;
                        }

                        using (MemoryStream salida = new MemoryStream())
                        {
                            zip.Save(salida);
                            return File(salida.ToArray(), "application/zip", "DocumentosFactura_" + idDetalle.ToString() + ".zip");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 28 de noviembre de 2022
        /// Vista parcial para ingresar la gestion de la factura del inventario
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _FormularioGestionFacturaInventarioContabilizadas(Int32 id, DateTime? fechaIni, DateTime? fechaFin, int? regional)
        {
            ViewBag.idInventarioCargueDtll = id;
            ViewBag.fechaIncial = fechaIni.Value.ToString("MM/dd/yyyy");
            ViewBag.fechaFin = fechaFin.Value.ToString("MM/dd/yyyy"); ;
            ViewBag.regional = regional;
            return PartialView();
        }

        public PartialViewResult _FormularioGestionFacturaInventarioContabilizadasSegunda(Int32 id)
        {
            ViewBag.idInventarioCargueDtll = id;
            return PartialView();
        }


        public string verGestionInventarioAuxiliar(int? id)
        {
            var respuesta = "";

            try
            {

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha 29 de noviembre de 2022
        /// Metodo utilizado para guardar la gestion de las facturas por parte del area de archivo
        /// </summary>
        /// <param name="idInventarioCargueDtll"></param>
        /// <param name="descargueImagenes"></param>
        /// <param name="imagenesSinNovedades"></param>
        /// <param name="observaciones"></param>
        /// <returns></returns>
        public JsonResult GuardarGestionInvetarioFacturaContabilizada(int idInventarioCargueDtll, String descargueImagenes, String imagenesSinNovedades, String observaciones)
        {
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;

            try
            {
                inventario_facturas_contabilizadas_gestion gestion = new inventario_facturas_contabilizadas_gestion();
                gestion.inventario_cargue_dtll_id = idInventarioCargueDtll;
                gestion.descarga_imagen = descargueImagenes;
                gestion.imagenes_sin_novedades = imagenesSinNovedades;
                gestion.observaciones = observaciones;
                gestion.fecha_digita = DateTime.Now;
                gestion.usuario_digita = SesionVar.UserName;
                Model.GuardarGestionInvetarioFacturaContabilizada(gestion, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    /*En este apartado se limpiara la factura que se gestionó de la memoria cache*/
                    List<Management_inventario_facturas_contabilizadasResult> ListadoFacturasArchivoEnCache = cache["facturasArchivo" + nomUsuario] as List<Management_inventario_facturas_contabilizadasResult>;

                    if (ListadoFacturasArchivoEnCache != null && ListadoFacturasArchivoEnCache.Count() > 0)
                    {
                        var itemCache = ListadoFacturasArchivoEnCache.Where(l => l.id_inventario_cargue_dtll == idInventarioCargueDtll).FirstOrDefault();
                        if (itemCache != null)
                        {
                            String key = "tiempoExpiracionMemoria" + nomUsuario;
                            DateTime expiracionCache = CheckCachedExpiry(key);
                            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expiracionCache };

                            ListadoFacturasArchivoEnCache.Remove(itemCache);
                            cache.Set("facturasArchivo" + nomUsuario, ListadoFacturasArchivoEnCache, policy);
                            cache.Set("tiempoExpiracionMemoria" + nomUsuario, expiracionCache, policy);
                        }
                    }

                    return Json(new { success = true, message = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ha ocurrido un error al momento de procesar la solicitud. " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// </summary>
        /// <param name="idDetalle"></param>
        /// <returns></returns>
        [HttpGet]
        public string verGestionInventarioFacturaContabilizada(int idDetalle)
        {
            inventario_facturas_contabilizadas_gestion gestion = Model.consultarGestionFacturaContabilizadaporIdDetalle(idDetalle);
            string result = "";
            result += "<table class='table table-bordered'>";
            result += "<tr>";
            result += "<td>Descargue imagenes:</td>";
            result += "<td>" + gestion.descarga_imagen + "</td>";
            result += "</tr>";
            result += "<tr>";
            result += "<td>Imagenes sin novedades:</td>";
            result += "<td>" + gestion.imagenes_sin_novedades + "</td>";
            result += "</tr>";
            result += "<tr>";
            result += "<td>Observaciones:</td>";
            result += "<td>" + gestion.observaciones + "</td>";
            result += "</tr>";
            result += "</table>";

            return result;
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Vista maestro para los resultados de la busqueda del tablero de inventario facturas contabilizadas
        /// </summary>
        /// <returns></returns>
        public ActionResult TableroControlInventarioFacturacionCordinacion()
        {
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            Session["resultadosInventarioFacCordinacion"] = new List<Management_inventario_facturas_contabilizadas_cordinacionResult>();
            return View();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para buscar y cargar los resultados de la busqueda hecha en el tablero
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="regional"></param>
        /// <returns></returns>
        public PartialViewResult _resultadosTableroControlInventarioFacturacionCordinacion(int mes, int año, int regional)
        {
            List<Management_inventario_facturas_contabilizadas_cordinacionResult> model = new List<Management_inventario_facturas_contabilizadas_cordinacionResult>();
            try
            {
                model = Model.ConsultarInventarioFacturasCordinacion(mes, año, regional, ref MsgRes);
                Session["resultadosInventarioFacCordinacion"] = model;
            }
            catch (Exception ex)
            {
                model = new List<Management_inventario_facturas_contabilizadas_cordinacionResult>();
            }
            return PartialView(model);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para exportar a excel los resultados del tablero de inventario facturas contabilizadas.
        /// </summary>
        public void ReporteExcelTableroInventarioFacturasContabilizadas()
        {
            List<Management_inventario_facturas_contabilizadas_cordinacionResult> result = (List<Management_inventario_facturas_contabilizadas_cordinacionResult>)Session["resultadosInventarioFacCordinacion"];

            if (result.Count > 0)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Inventario facturas contabilizadas");

                Sheet.Cells["A1:R1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(99, 99, 99);
                Sheet.Cells["A1:R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:R1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:R1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A:R"].Style.Font.Name = "century gothic";

                Sheet.Cells["A1"].Value = "Fecha de contabilización";
                Sheet.Cells["B1"].Value = "Documento contable";
                Sheet.Cells["C1"].Value = "Número de factura SAP";
                Sheet.Cells["D1"].Value = "Código SAP";
                Sheet.Cells["E1"].Value = "Nit";
                Sheet.Cells["F1"].Value = "Razón social";
                Sheet.Cells["G1"].Value = "Ciudad";
                Sheet.Cells["H1"].Value = "Valor total factura";
                Sheet.Cells["I1"].Value = "Regional";
                Sheet.Cells["J1"].Value = "Fecha de recepción";
                Sheet.Cells["K1"].Value = "Unis";
                Sheet.Cells["L1"].Value = "Analista cuentas";
                Sheet.Cells["M1"].Value = "Estado";
                Sheet.Cells["N1"].Value = "Gestor archivo nombre";
                Sheet.Cells["O1"].Value = "Descargue imagenes";
                Sheet.Cells["P1"].Value = "Imagenes sin novedades";
                Sheet.Cells["Q1"].Value = "Observaciones";
                Sheet.Cells["R1"].Value = "Fecha gestión";


                int row = 2;
                foreach (Management_inventario_facturas_contabilizadas_cordinacionResult item in result)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.fecha_contabilizacion;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.documento_contable;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.numero_factura_sap;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.cod_sap;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.nit;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.razon_social;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.ciudad;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.valor_total_factura;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.nombre_regional;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_recepcion;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.analista_cuentas;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.nom_estado;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.gestor_archivo_nombre;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.descarga_imagen;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.imagenes_sin_novedades;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.observaciones;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.fecha_gestion;
                    row++;
                }
                Sheet.Cells["A:R"].AutoFitColumns();


                Response.Clear();
                Response.ContentType = "application/excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteInventarioFacturasContabilizadas.xls");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.BufferOutput = false;
                Response.End();
            }
            else
            {
                string rta = "<script LANGUAGE='JavaScript'>window.alert('Lo sentimos, no se han encontrado resultados.');</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// </summary>
        /// <returns></returns>
        public ActionResult TableroControlInventarioFacturacionConsolidado()
        {
            List<Management_inventario_facturas_contabilizadas_consolidadoResult> model = new List<Management_inventario_facturas_contabilizadas_consolidadoResult>();
            try
            {
                model = Model.ConsultarInventarioFacturasConsolidado();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(model);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de noviembre de 2022
        /// Metodo utilizadao para guardar el archivo zip o rar cargado en el tablero de inventario facturas contabilizadas consolidado.
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveArchivoCargadoInventarioFacContabilizadasConsolidado()
        {
            inventario_facturas_contabilizadas_gestor_documental doc = new inventario_facturas_contabilizadas_gestor_documental();
            try
            {

                doc.mes = Convert.ToInt32(Request.Form["mes"]);
                doc.año = Convert.ToInt32(Request.Form["año"]);
                doc.id_regional = Convert.ToInt32(Request.Form["regional"]);

                string strRutaDefinitiva = ConfigurationManager.AppSettings["rutaArchivosInventarioFacturasContabilizadas"], carpeta = "";
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "ArchivosCargados";
                }
                else
                {
                    carpeta = "ArchivosCargadosPruebas";
                }

                HttpPostedFileBase file = Request.Files["file"];
                string rutaArchivosCargados = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + doc.mes + doc.año + doc.id_regional);

                if (!System.IO.Directory.Exists(rutaArchivosCargados))
                {
                    System.IO.Directory.CreateDirectory(rutaArchivosCargados);
                }

                var path = Path.Combine(rutaArchivosCargados, file.FileName);
                file.SaveAs(path);


                doc.nom_documento = file.FileName;
                doc.ruta_documento = path;
                doc.ext = Path.GetExtension(file.FileName);
                doc.fecha_digita = DateTime.Now;
                doc.usuario_digita = SesionVar.UserName;

                Model.insertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado(doc, ref MsgRes);

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
                return Json(new { success = false, message = "Ha ocurrido un error al momento de procesar la solicitud: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de noviembre de 2022
        /// Metodo utilizado para descargar el archivo zip o rar cargado en el tablero de control
        /// </summary>
        /// <param name="idArchivo"></param>
        public ActionResult VerArchivoCargadoZip(int idArchivo)
        {
            try
            {
                inventario_facturas_contabilizadas_gestor_documental obj = Model.ConsultarRegistroArchivoCargadoPorId(idArchivo, ref MsgRes);

                string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta_documento);
                obj.ruta_documento = dirpath;

                using (FileStream fs = new FileStream(dirpath, FileMode.Open))
                {
                    //response is HttpListenerContext.Response... 
                    Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                    Response.AppendHeader("content-disposition", "attachment; filename=ArchivoEntrega.Zip");
                    byte[] buffer = new byte[64 * 1024];
                    int read;
                    while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        Response.OutputStream.Write(buffer, 0, read);
                        Response.OutputStream.Flush();
                    }
                    Response.OutputStream.Close();
                }
                Response.BufferOutput = false;
                Response.End();

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message });
            }
        }

        public ActionResult VerArchivoCargadoZipLista(int? mes, int? año, int? regional)
        {
            List<inventario_facturas_contabilizadas_gestor_documental> obj = new List<inventario_facturas_contabilizadas_gestor_documental>();
            try
            {
                obj = Model.ConsultarRegistroArchivoCargadoPorIdLista(mes, año, regional, ref MsgRes).ToList();

                if (obj.Count == 0)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "La factura no contiene documentos." });
                }

                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    int count = 0;
                    foreach (var item in obj)
                    {
                        count++;

                        string dirpath = Path.Combine(Request.PhysicalApplicationPath, item.ruta_documento);
                        byte[] bytes = System.IO.File.ReadAllBytes(dirpath);

                        if (item.nom_documento.ToUpper().Contains("ZIP"))
                        {
                            zip.AddEntry("Entrega_" + count + ".zip", bytes);
                        }
                        else if (item.nom_documento.ToUpper().Contains("RAR"))
                        {
                            zip.AddEntry("Entrega_" + count + ".rar", bytes);
                        }
                        else
                        {
                            zip.AddEntry("Entrega_" + count + ".pdf", bytes);
                        }
                    }

                    using (MemoryStream salida = new MemoryStream())
                    {
                        zip.Save(salida);
                        return File(salida.ToArray(), "application/zip", "ArchivosDigital_" + mes.ToString() + año.ToString() + regional.ToString() + ".zip");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new
                {
                    Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message
                });
            }
        }

        //Tablero
        public ActionResult TableroControlInventarioFacturacionGestionadas(DateTime? fechaIni, DateTime? fechaFin, int? regional)
        {

            List<Management_inventario_facturas_contabilizadasResult> result = new List<Management_inventario_facturas_contabilizadasResult>();
            List<Management_inventario_facturas_contabilizadasResult> listaFinal = new List<Management_inventario_facturas_contabilizadasResult>();

            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            List<Ref_regional> region = new List<Ref_regional>();

            region = BusClass.GetRefRegion();
            ViewBag.regional = region;

            try
            {
                List<Management_inventario_facturas_contabilizadasResult> ListadoFacturasAuditor = cache["facturasArchivoGestionada" + nomUsuario] as List<Management_inventario_facturas_contabilizadasResult>;

                DateTime? cacheFechaInicio = (DateTime?)cache["fechaInicioGestionada" + nomUsuario];
                DateTime? cacheFechaFinal = (DateTime?)cache["fechaFinalGestionada" + nomUsuario];
                int? cacheRegional = (int?)cache["regionalGestionada" + nomUsuario];

                List<Management_inventario_facturas_contabilizadasResult> listInventarioFacturas = new List<Management_inventario_facturas_contabilizadasResult>();

                if (ListadoFacturasAuditor == null || ListadoFacturasAuditor.Count() == 0)
                {
                    /*Consulta en base de datos*/
                    if (fechaIni != null && fechaFin != null && regional != null)
                    {
                        cache["fechaInicioGestionada" + nomUsuario] = fechaIni;
                        cache["fechaFinalGestionada" + nomUsuario] = fechaFin;
                        cache["regionalGestionada" + nomUsuario] = regional;

                        listInventarioFacturas = Model.ConsultarInventarioFacturasPorEstado(2, fechaIni, fechaFin, regional, ref MsgRes);
                    }

                    /*Se definen las politicas y se insertan en cache los resultados de la consulta hecha a base de datos*/
                    DateTime expirationDate = DateTime.Now.AddHours(10);
                    CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expirationDate };
                    cache.Set("facturasArchivoGestionada" + nomUsuario, listInventarioFacturas, policy);
                    cache.Set("tiempoExpiracionMemoriaGestionada" + nomUsuario, expirationDate, policy);
                }
                else
                {

                    if ((fechaIni != cacheFechaInicio) || (fechaFin != cacheFechaFinal) || (regional != cacheRegional))
                    {
                        /*Consulta en base de datos*/
                        if (fechaIni != null && fechaFin != null && regional != null)
                        {
                            cache["fechaInicioGestionada" + nomUsuario] = fechaIni;
                            cache["fechaFinalGestionada" + nomUsuario] = fechaFin;
                            cache["regionalGestionada" + nomUsuario] = regional;
                            listInventarioFacturas = Model.ConsultarInventarioFacturasPorEstado(2, fechaIni, fechaFin, regional, ref MsgRes);
                        }
                        else
                        {
                            listInventarioFacturas = new List<Management_inventario_facturas_contabilizadasResult>();
                        }
                    }
                    else
                    {
                        /*Se setea en el listado de resultados lo que aun hay en cache*/
                        listInventarioFacturas = ListadoFacturasAuditor;
                    }
                }

                if (SesionVar.ROL != "1")
                {
                    listInventarioFacturas = listInventarioFacturas.Where(x => x.usuario_digita_1 == nomUsuario || x.usuario_digita_2 == nomUsuario).ToList();

                    listaFinal = result;
                    Session["inventarioArchivoGestionada"] = listaFinal;
                    return PartialView(listaFinal);
                }
                else
                {
                    listaFinal = listInventarioFacturas;
                    Session["inventarioArchivoGestionada"] = listaFinal;
                    return PartialView(listaFinal);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return PartialView(listaFinal);
            }

            //List<Management_inventario_facturas_contabilizadasResult> listInventarioFacturas = Model.ConsultarInventarioFacturasPorEstado(2, fechaIni, fechaFin, regional, ref MsgRes);
            //List<Management_inventario_facturas_contabilizadasResult> result = new List<Management_inventario_facturas_contabilizadasResult>();
            //List<Management_inventario_facturas_contabilizadasResult> listaFinal = new List<Management_inventario_facturas_contabilizadasResult>();
            //List<Ref_regional> region = new List<Ref_regional>();
            //region = BusClass.GetRefRegion();
            //ViewBag.regional = region;

            //var usuario = SesionVar.UserName;

            //try
            //{
            //    if (SesionVar.ROL != "1")
            //    {
            //        listInventarioFacturas = listInventarioFacturas.Where(x => x.usuario_digita_1 == usuario || x.usuario_digita_2 == usuario).ToList();

            //        listaFinal = listInventarioFacturas;
            //        Session["inventarioArchivoGestionadas"] = listaFinal;
            //        return View(listaFinal);
            //    }
            //    else
            //    {
            //        listaFinal = listInventarioFacturas;
            //        Session["inventarioArchivoGestionadas"] = listaFinal;
            //        return View(listaFinal);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var error = ex.Message;
            //    return View(listInventarioFacturas);
            //}
        }


        //Descargar reporte inventario contabilizados
        public void ExportarReporteInventarioContabilizadas(int? tipo)
        {
            List<Management_inventario_facturas_contabilizadasResult> Lista = new List<Management_inventario_facturas_contabilizadasResult>();
            string proye;
            string namefile;
            string limiteFila = "";
            try
            {
                ExcelPackage Ep = new ExcelPackage();
                try
                {
                    if (tipo == 1)
                    {
                        Lista = (List<Management_inventario_facturas_contabilizadasResult>)Session["inventarioArchivo"];
                        proye = "InventarioContabilizadas";
                        namefile = "InventarioContabilizadas_" + DateTime.Now;
                        limiteFila = "E";
                    }
                    else
                    {
                        Lista = (List<Management_inventario_facturas_contabilizadasResult>)Session["inventarioArchivoGestionadas"];
                        namefile = "InventarioContabilizadasGestionadas_" + DateTime.Now;
                        proye = "InventarioContabilizadasGestionadas";
                        limiteFila = "O";
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    throw new Exception(error);
                }

                if (Lista == null || Lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {
                    try
                    {
                        ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(proye);

                        Color colFromHex = Color.FromArgb(99, 99, 99);
                        Sheet.Cells["A1:" + limiteFila + "1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        Sheet.Cells["A1:" + limiteFila + "1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        Sheet.Cells["A1:" + limiteFila + "1"].Style.Font.Color.SetColor(Color.White);
                        Sheet.Cells["A1:" + limiteFila + "1"].Style.Font.Size = 10;
                        //Sheet.Cells["A1:" + limiteFila + "1"].Style.WrapText = true;
                        Sheet.Cells["A1:" + limiteFila + "1"].Style.Font.Bold = true;
                        //Sheet.Cells["A1:G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //Sheet.Cells["A1:G1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        Sheet.Cells["A1"].Value = "Factura";
                        Sheet.Cells["B1"].Value = "Fecha contabilización";
                        Sheet.Cells["C1"].Value = "Documento SAP";
                        Sheet.Cells["D1"].Value = "Prestador";
                        Sheet.Cells["E1"].Value = "Regional";

                        if (tipo != 1)
                        {
                            Sheet.Cells["F1"].Value = "Analista gestiona 1";
                            Sheet.Cells["G1"].Value = "Analista gestiona 2";
                            Sheet.Cells["H1"].Value = "Fecha gestión 1";
                            Sheet.Cells["I1"].Value = "Fecha gestión 2";
                            Sheet.Cells["J1"].Value = "Descarga imagen 1";
                            Sheet.Cells["K1"].Value = "Descarga imagen 2";
                            Sheet.Cells["L1"].Value = "Imagenes sin novedad 1";
                            Sheet.Cells["M1"].Value = "Imagenes sin novedad 2";
                            Sheet.Cells["N1"].Value = "Observaciones 1";
                            Sheet.Cells["O1"].Value = "Observaciones 2";
                        }

                        int row = 2;

                        foreach (Management_inventario_facturas_contabilizadasResult item in Lista)
                        {
                            Sheet.Cells["A" + row + ":" + limiteFila + row].Style.Font.Size = 10;
                            Sheet.Cells[string.Format("A{0}", row)].Value = item.numero_factura_sap;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fecha_contabilizacion;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.documento_contable;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.razon_social;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.nombreRegional;
                            if (tipo != 1)
                            {
                                Sheet.Cells[string.Format("F{0}", row)].Value = item.nombreGestiona_1;
                                Sheet.Cells[string.Format("H{0}", row)].Value = item.fechaGestion_1;
                                Sheet.Cells[string.Format("I{0}", row)].Value = item.fechaGestion_2;
                                Sheet.Cells[string.Format("G{0}", row)].Value = item.nombreGestiona_2;
                                Sheet.Cells[string.Format("J{0}", row)].Value = item.descarga_imagen_1;
                                Sheet.Cells[string.Format("K{0}", row)].Value = item.descarga_imagen_2;
                                Sheet.Cells[string.Format("L{0}", row)].Value = item.imagenes_sin_novedades_1;
                                Sheet.Cells[string.Format("M{0}", row)].Value = item.imagenes_sin_novedades_2;
                                Sheet.Cells[string.Format("N{0}", row)].Value = item.observaciones_1;
                                Sheet.Cells[string.Format("O{0}", row)].Value = item.observaciones_2;

                                Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                                Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                            }

                            //else
                            //{
                            //    Sheet.Cells[string.Format("F{0}", row)].Value = item.analista;
                            //    Sheet.Cells[string.Format("G{0}", row)].Value = item.analista;
                            //}

                            Sheet.Cells[string.Format("B{0}", row)].Style.Numberformat.Format = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                            row++;
                        }

                        Sheet.Cells["A:" + limiteFila].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                        throw new Exception(error);
                    }
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

        public void reporteInventario(int estado)
        {
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            //RUTA REPORTE

            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "ReporteInventarioContabilizadas.rdlc");


                //CARGAR LISTA
                //List<management_inventario_facturas_contabilizadas_reporteResult> lista = new List<management_inventario_facturas_contabilizadas_reporteResult>();
                List<Management_inventario_facturas_contabilizadasResult> lista = new List<Management_inventario_facturas_contabilizadasResult>();

                if (estado == 1)
                {
                    lista = (List<Management_inventario_facturas_contabilizadasResult>)Session["inventarioArchivo"];
                }
                else
                {
                    lista = (List<Management_inventario_facturas_contabilizadasResult>)Session["inventarioArchivoGestionadas"];
                }

                //lista = BusClass.ReporteInventarioContabilizadas(estado);

                if (lista.Count() > 0)
                {
                    //ASIGNACION  DATASET A REPORT
                    Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetInvenConta", lista);
                    Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();

                    viewer.LocalReport.EnableExternalImages = true;
                    viewer.LocalReport.ReportPath = rPath;
                    viewer.LocalReport.DataSources.Clear();
                    viewer.LocalReport.DataSources.Add(rds);

                    viewer.LocalReport.EnableHyperlinks = true;
                    viewer.LocalReport.Refresh();

                    try
                    {
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
                        var error = ex.Message;
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = error;

                        string rta = "<script LANGUAGE='JavaScript'>" +
                        "window.alert('ERROR EN LA DESCARGA');" +
                        "</script> ";
                        Response.Write(rta);
                        Response.End();
                    }
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('NO HAY DATOS PARA VISUALIZAR');" +
                    "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = error;

                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('ERROR EN LA DESCARGA');" +
                "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public static DateTime CheckCachedExpiry(string key)
        {
            DateTime MemCacheExpiryDate = default(DateTime);
            MemCacheExpiryDate = Convert.ToDateTime(MemoryCache.Default.Get(key));
            return MemCacheExpiryDate;
        }

    }
}