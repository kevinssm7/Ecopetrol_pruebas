using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using LinqToExcel;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Facturacion
{

    public class CierreContableController : Controller
    {
        #region Propiedades
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

        #region CierreContable  

        public ActionResult CargueFacturas()
        {
            Models.Facturacion.FacturaDevolucion Model = new Models.Facturacion.FacturaDevolucion();
            List<Ref_regional> listaregional = Model.RefRegional;
            ViewBag.listaRegionales = listaregional;
            ViewData["alerta"] = "";
            ViewData["ListaErrores"] = new List<string>();
            return View();
        }

        public ActionResult TableroCierre()
        {
            Models.CierreContable.CierreContable Model = new Models.CierreContable.CierreContable();

            List<cierre_contable> lista = Model.GetListCierreContable(ref MsgRes);
            return View(lista);
        }

        public ActionResult ReporteCierreContable(int idcierre)
        {
            Models.CierreContable.CierreContable Model = new Models.CierreContable.CierreContable();
            List<vw_totales_cierre_contable> ListaTotales = Model.GetListTotalesCierreContable(idcierre, ref MsgRes);

            /*Reporte*/

            ViewBag.listatotales = ListaTotales;

            ViewBag.totalfacturas1 = ListaTotales.Where(l => l.Tipo_factura.Contains("MES_ANTERIOR") || l.Tipo_factura.Contains("RADICADAS")).Select(l => l.valor_total).Sum().Value.ToString("C");
            ViewBag.cantidad1 = ListaTotales.Where(l => l.Tipo_factura.Contains("MES_ANTERIOR") || l.Tipo_factura.Contains("RADICADAS")).Select(l => l.cantidad).Sum();

            ViewBag.totalfacturas2 = ListaTotales.Where(l => !l.Tipo_factura.Contains("MES_ANTERIOR") && !l.Tipo_factura.Contains("RADICADAS")).Select(l => l.valor_total).Sum();
            ViewBag.cantidad2 = ListaTotales.Where(l => !l.Tipo_factura.Contains("MES_ANTERIOR") && !l.Tipo_factura.Contains("RADICADAS")).Select(l => l.cantidad).Sum();

            /* Causas*/
            /*Rechazadas*/
            List<vw_causas_facturas> ListaCausas = Model.GetListCausasCierreContable(idcierre, ref MsgRes);
            ViewBag.listaCausasRechazadas = ListaCausas.Where(l => l.tipo_factura.Contains("RECHAZADAS")).OrderByDescending(l => l.cantidad).ToList();
            ViewBag.cantidadrechazadas = ListaCausas.Where(l => l.tipo_factura.Contains("RECHAZADAS")).Select(l => l.cantidad).Sum();
            ViewBag.totallistarechazadas = ListaCausas.Where(l => l.tipo_factura.Contains("RECHAZADAS")).Select(l => l.valor).Sum().Value.ToString("C");

            /*Pendientes*/
            ViewBag.listaCausasPendientes = ListaCausas.Where(l => l.tipo_factura.Contains("PENDIENTES")).OrderByDescending(l => l.cantidad).ToList();
            ViewBag.cantidadpendientes = ListaCausas.Where(l => l.tipo_factura.Contains("PENDIENTES")).Select(l => l.cantidad).Sum();
            ViewBag.totallistapendientes = ListaCausas.Where(l => l.tipo_factura.Contains("PENDIENTES")).Select(l => l.valor).Sum().Value.ToString("C");

            Session["TotatalesCierreContable"] = ListaTotales;
            Session["CausasFactura"] = ListaCausas;
            ViewBag.idcierre = idcierre;

            return View();
        }

        [HttpPost]
        public ActionResult CargueFacturas(HttpPostedFileBase[] file, int regional, int mes, int year)
        {

            Models.Facturacion.FacturaDevolucion Model2 = new Models.Facturacion.FacturaDevolucion();
            List<Ref_regional> listaregional = Model2.RefRegional;
            ViewBag.listaRegionales = listaregional;
            string nomfile = "";
            List<string> Archivos_error = new List<string>();
            int correctos = 0, incorrectos = 0;
            ViewData["ListaErrores"] = new List<string>();
            var ruta = "";

            try
            {
                Models.CierreContable.CierreContable Model = new Models.CierreContable.CierreContable();
                cierre_contable cierre = new cierre_contable();
                cierre.id_regional = regional;
                cierre.mes = mes;
                cierre.año = year;
                cierre.fecha_cargue = DateTime.Now;
                cierre.usuario = SesionVar.UserName;
                int id = Model.InsertarCierreContable(cierre, ref MsgRes);

                string indreg = listaregional.Where(l => l.id_ref_regional == regional).FirstOrDefault().indice;
                List<string> filenames = new List<string>();


                String path = ConfigurationManager.AppSettings["rutaDocumentos"] + "\\CierreContable";
                if (!System.IO.Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (id != 0)
                {
                    path = Path.Combine(path, indreg + mes.ToString() + year.ToString());
                    if (!System.IO.Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    foreach (HttpPostedFileBase item in file)
                    {
                        try
                        {
                            ruta = Path.Combine(path, item.FileName);
                            item.SaveAs(ruta);

                            var excel = new ExcelQueryFactory(ruta);
                            var NameHojaCalculo = excel.GetWorksheetNames().FirstOrDefault();
                            nomfile = NameHojaCalculo;

                            if (NameHojaCalculo.Contains("MES_ANTERIOR"))
                            {
                                List<cierre_cont_mes_anterior> listamesanterior = CargarListMesAnterior(ruta, id);
                                if (Model.InsertarFacturasMesInterior(listamesanterior, ref MsgRes))
                                {
                                    correctos += 1;
                                }
                                else
                                {
                                    Archivos_error.Add(nomfile + ": " + MsgRes.DescriptionResponse);
                                    incorrectos += 1;
                                }
                            }

                            if (NameHojaCalculo.Contains("RECHAZOS"))
                            {
                                List<cierre_cont_rechazos> listarechazos = CargarListRechazos(ruta, id);
                                if (Model.InsertarFacturasRechazos(listarechazos, ref MsgRes))
                                {
                                    correctos += 1;
                                }
                                else
                                {
                                    Archivos_error.Add(nomfile + ": " + MsgRes.DescriptionResponse);
                                    incorrectos += 1;
                                }
                            }

                            if (NameHojaCalculo.Contains("PENDIENTE_PROCESAR"))
                            {
                                List<cierre_cont_pendiente_procesar> listarpendienteprocesar = CargarListPendienteProcesar(ruta, id);
                                if (Model.InsertarFacturasPendientesProcs(listarpendienteprocesar, ref MsgRes))
                                {
                                    correctos += 1;
                                }
                                else
                                {
                                    Archivos_error.Add(nomfile + ": " + MsgRes.DescriptionResponse);
                                    incorrectos += 1;
                                }
                            }

                            if (NameHojaCalculo.Contains("DEVOLUCIONES"))
                            {
                                List<cierre_cont_devoluciones> listadevoluciones = CargarListdevoluciones(ruta, id);
                                if (Model.InsertarFacturasdevoluciones(listadevoluciones, ref MsgRes))
                                {
                                    correctos += 1;
                                }
                                else
                                {
                                    Archivos_error.Add(nomfile + ": " + MsgRes.DescriptionResponse);
                                    incorrectos += 1;
                                }
                            }

                            if (NameHojaCalculo.Contains("CAUSADAS"))
                            {
                                List<cierre_cont_causadas> listacausadas = CargarListCausadas(ruta, id);
                                if (Model.InsertarFacturascausadas(listacausadas, ref MsgRes))
                                {
                                    correctos += 1;
                                }
                                else
                                {
                                    Archivos_error.Add(nomfile + ": " + MsgRes.DescriptionResponse);
                                    incorrectos += 1;
                                }
                            }

                            if (NameHojaCalculo.Contains("RADICADAS"))
                            {
                                List<cierre_cont_radicadas> listaradicadas = CargarListRadicadas(ruta, id);
                                if (Model.InsertarFacturasradicadas(listaradicadas, ref MsgRes))
                                {
                                    correctos += 1;
                                }
                                else
                                {
                                    Archivos_error.Add(nomfile + ": " + MsgRes.DescriptionResponse);
                                    incorrectos += 1;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            incorrectos += 1;
                            Archivos_error.Add(nomfile + ": " + ex.Message);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Archivos_error.Add(nomfile + ": " + ex.Message);
                incorrectos += 1;
            }

            if (incorrectos > 0)
            {
                ViewData["alerta"] = "Hubo conflictos cargando las facturas";
                ViewData["ListaErrores"] = Archivos_error;
            }
            else
            {
                ViewData["alerta"] = "Las facturas han sido cargadas exitosamente";
            }


            FileInfo fileDelete = new FileInfo(ruta);
            if (fileDelete != null)
            {
                fileDelete.Delete();
            }

            return View();
        }

        public List<cierre_cont_mes_anterior> CargarListMesAnterior(string pathDelFicheroExcel, int idcierre)
        {
            var book = new ExcelQueryFactory(pathDelFicheroExcel);

            List<cierre_cont_mes_anterior> result = new List<cierre_cont_mes_anterior>();
            var resultado = (from row in book.Worksheet("MES_ANTERIOR")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cierre_cont_mes_anterior
                             {
                                 id_cierre = idcierre,
                                 fecha_contabilizacion = row["Fecha de Contabilización"],
                                 documento_contable = row["Documento contable"],
                                 numero_factura = row["Número de Factura"],
                                 codigo_sap = row["Código SAP"],
                                 nit = Convert.ToInt64(row["NIT"]),
                                 razon_social = row["Razón Social"],
                                 valor_total_factura = Convert.ToDecimal(row["Valor Total Factura"]),
                                 ordenador = row["Ordenador"],
                                 pedido = row["Pedido"],
                                 fecha_recepcion = row["Fecha de Recepción"],
                             }
                             select item).ToList();
            book.Dispose();
            return resultado;
        }

        public List<cierre_cont_rechazos> CargarListRechazos(string pathDelFicheroExcel, int idcierre)
        {

            var book = new ExcelQueryFactory(pathDelFicheroExcel);
            var resultado = (from row in book.Worksheet("RECHAZOS")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cierre_cont_rechazos
                             {
                                 id_cierre = idcierre,
                                 fecha_contable = row["Fecha Contable"],
                                 fecha_recepcion = row["Fecha de Recepción"],
                                 valor_total_factura = Convert.ToDecimal(row["Valor Total Factura"]),
                                 numero_factura = row["Número de Factura"],
                                 codigo_sap = row["Código SAP"],
                                 nit = Convert.ToInt64(row["NIT"]),
                                 razon_social = row["Razón Social"],
                                 ciudad = row["Ciudad"],
                                 regional = row["Regional"],
                                 motivo_rechazo = row["Motivo de rechazo"],
                             }
                             select item).ToList();
            book.Dispose();
            return resultado;
        }

        public List<cierre_cont_pendiente_procesar> CargarListPendienteProcesar(string pathDelFicheroExcel, int idcierre)
        {

            var book = new ExcelQueryFactory(pathDelFicheroExcel);
            var resultado = (from row in book.Worksheet("PENDIENTE_PROCESAR")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cierre_cont_pendiente_procesar
                             {
                                 id_cierre = idcierre,
                                 fecha_recepcion =row["Fecha de Recepción"],
                                 fecha_factura = row["Fecha de Factura"],
                                 numero_factura = row["Número de Factura"],
                                 valor_total_factura = Convert.ToDecimal(row["Valor total factura"]),
                                 nit = Convert.ToInt64(row["NIT"]),
                                 razon_social = row["Razón Social"],
                                 ciudad = row["Ciudad"],
                                 regional = row["Regional"],
                                 codigo_sap = row["Código SAP"],
                                 observaciones = row["Observaciones"]
                             }
                             select item).ToList();
            book.Dispose();
            return resultado;
        }

        public List<cierre_cont_devoluciones> CargarListdevoluciones(string pathDelFicheroExcel, int idcierre)
        {
            var book = new ExcelQueryFactory(pathDelFicheroExcel);
            var resultado = (from row in book.Worksheet("DEVOLUCIONES")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cierre_cont_devoluciones
                             {
                                 id_cierre = idcierre,
                                 fecha_factura = row["Fecha de Factura"],
                                 numero_factura = row["Número de Factura"],
                                 codigo_sap = row["Código SAP"],
                                 nit = Convert.ToInt64(row["NIT"]),
                                 razon_social = row["Razón Social"],
                                 ciudad = row["Ciudad"],
                                 regional = row["Regional"],
                                 fecha_devolucion = row["Fecha de Devolución"],
                                 fecha_recepcion = row["Fecha de Recepción"],
                                 valor_total_factura = Convert.ToDecimal(row["Valor Total Factura"]),
                             }
                             select item).ToList();
            book.Dispose();
            return resultado;
        }

        public List<cierre_cont_causadas> CargarListCausadas(string pathDelFicheroExcel, int idcierre)
        {
            var book = new ExcelQueryFactory(pathDelFicheroExcel);
            var resultado = (from row in book.Worksheet("CAUSADAS")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cierre_cont_causadas
                             {
                                 id_cierre = idcierre,
                                 fecha_de_contabilización = row["Fecha de contabilización"],
                                 documento_contable = row["Documento contable"],
                                 numero_factura = row["Número de Factura"],
                                 codigo_sap = row["Código SAP"],
                                 nit = Convert.ToInt64(row["NIT"]),
                                 razon_social = row["Razón Social"],
                                 ciudad = row["Ciudad"],
                                 valor_total_factura = Convert.ToDecimal(row["Valor Total Factura"]),
                                 regional = row["Regional"],
                             }
                             select item).ToList();
            book.Dispose();
            return resultado;
        }

        public List<cierre_cont_radicadas> CargarListRadicadas(string pathDelFicheroExcel, int idcierre)
        {
            var book = new ExcelQueryFactory(pathDelFicheroExcel);
            var resultado = (from row in book.Worksheet("RADICADAS")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cierre_cont_radicadas
                             {
                                 id_cierre = idcierre,
                                 fecha_recepcion = row["Fecha de Recepción"],
                                 fecha_factura = row["Fecha de Factura"],
                                 numero_factura = row["Número de Factura"],
                                 codigo_sap = row["Código SAP"],
                                 nit = Convert.ToInt64(row["NIT"]),
                                 razon_social = row["Razón Social"],
                                 ciudad = row["Ciudad"],
                                 regional = row["Regional"],
                                 valor_total_factura = Convert.ToDecimal(row["Valor Total Factura"]),
                                 pedido = row["Pedido"]

                             }
                             select item).ToList();
            book.Dispose();
            return resultado;
        }

        public ActionResult PDFCierreContable(int idcierre)
        {
            Models.CierreContable.CierreContable Model = new Models.CierreContable.CierreContable();
            cierre_contable cierre = Model.GetCierreContable(idcierre, ref MsgRes);

            List<vw_totales_cierre_contable> ListaTotales = Model.GetListTotalesCierreContable(idcierre, ref MsgRes);
            List<vw_causas_facturas> ListaCausas = Model.GetListCausasCierreContable(idcierre, ref MsgRes);

            ViewBag.listatotales = ListaTotales;

            ViewBag.totalfacturas1 = ListaTotales.Where(l => l.Tipo_factura.Contains("MES_ANTERIOR") || l.Tipo_factura.Contains("RADICADAS")).Select(l => l.valor_total).Sum().Value.ToString("C");
            ViewBag.cantidad1 = ListaTotales.Where(l => l.Tipo_factura.Contains("MES_ANTERIOR") || l.Tipo_factura.Contains("RADICADAS")).Select(l => l.cantidad).Sum();
            ViewBag.totalfacturas2 = ListaTotales.Where(l => !l.Tipo_factura.Contains("MES_ANTERIOR") && !l.Tipo_factura.Contains("RADICADAS")).Select(l => l.valor_total).Sum();
            ViewBag.cantidad2 = ListaTotales.Where(l => !l.Tipo_factura.Contains("MES_ANTERIOR") && !l.Tipo_factura.Contains("RADICADAS")).Select(l => l.cantidad).Sum();

            ViewBag.listaCausasRechazadas = ListaCausas.Where(l => l.tipo_factura.Contains("RECHAZADAS")).OrderByDescending(l => l.cantidad).ToList();
            ViewBag.cantidadrechazadas = ListaCausas.Where(l => l.tipo_factura.Contains("RECHAZADAS")).Select(l => l.cantidad).Sum();
            ViewBag.totallistarechazadas = ListaCausas.Where(l => l.tipo_factura.Contains("RECHAZADAS")).Select(l => l.valor).Sum().Value.ToString("C");

            ViewBag.listaCausasPendientes = ListaCausas.Where(l => l.tipo_factura.Contains("PENDIENTES")).OrderByDescending(l => l.cantidad).ToList();
            ViewBag.cantidadpendientes = ListaCausas.Where(l => l.tipo_factura.Contains("PENDIENTES")).Select(l => l.cantidad).Sum();
            ViewBag.totallistapendientes = ListaCausas.Where(l => l.tipo_factura.Contains("PENDIENTES")).Select(l => l.valor).Sum().Value.ToString("C");

            ViewBag.regional = cierre.Ref_regional.indice.ToUpper() + " - " + cierre.Ref_regional.nombre_regional.ToUpper();
            ViewBag.periododesde = "01/" + cierre.mes.ToString() + "/" + cierre.año.ToString();
            ViewBag.periodoHasta = "30/" + cierre.mes.ToString() + "/" + cierre.año.ToString();
            return View();
        }

        public void Pdf(int idcierre)
        {
            var file = new ActionAsPdf("PDFCierreContable", new { idcierre = idcierre })
            {
                FileName = "CierreContable.pdf"
            };
            var byteArray = file.BuildPdf(ControllerContext);
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=CierreContable.pdf");
            Response.BinaryWrite(byteArray);
            Response.End();
        }


        #endregion
    }
}