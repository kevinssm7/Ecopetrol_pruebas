using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ECOPETROL_COMMON.ENTIDADES;
using LinqToExcel;
using System.Data.OleDb;
using System.Data;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Globalization;
using ClosedXML.Excel;

namespace AsaludEcopetrol.Controllers.Facturacion
{
    public class FacturacionSapController : Controller
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
        public ActionResult CargueFacturacionSap(int? rta, string msg)
        {
            Models.Facturacion.FacturacionSap Model = new Models.Facturacion.FacturacionSap();

            Models.General General = new Models.General();

            if (rta == 1)
            {
                ViewData["msg"] = msg;
            }
            else if (rta == 2)
            {
                ViewData["msg"] = msg;
            }
            else
            {
                ViewData["msg"] = "";
            }

            ViewData["rta"] = 0;

            if (rta != 0 && rta != null)
            {
                ViewData["rta"] = rta;
            }

            return View(Model);
        }

        public JsonResult GuardarFacturacionSap(HttpPostedFileBase file)
        {
            Models.Facturacion.FacturacionSap Model = new Models.Facturacion.FacturacionSap();

            var rta = 0;
            var resultado = false;
            var mensaje = "";

            try
            {

                using (var workbook = new XLWorkbook(file.InputStream))
                {
                    var worksheet = workbook.Worksheet(1); // Asume que el archivo tiene al menos una hoja
                    var range = worksheet.RangeUsed(); // Obtiene el rango de celdas con datos

                    // Convertir el rango a DataTable
                    DataTable dataTable = new DataTable();
                    bool firstRow = true;

                    foreach (var row in range.Rows())
                    {
                        if (firstRow)
                        {
                            // Crear columnas a partir de la primera fila
                            foreach (var cell in row.Cells())
                            {
                                dataTable.Columns.Add(cell.GetValue<string>());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            // Agregar filas al DataTable
                            var newRow = dataTable.NewRow();
                            for (int i = 0; i < row.Cells().Count(); i++)
                            {
                                newRow[i] = row.Cell(i + 1).GetValue<string>();
                            }
                            dataTable.Rows.Add(newRow);
                        }
                    }

                    facturacion_sap_cargue obj = new facturacion_sap_cargue();
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    List<facturacion_sap_dtll> list = Model.ValidacionArchivo(dataTable, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        Model.InsertarFacturacionSAP(list, obj, ref MsgRes);

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            rta = 1;
                            mensaje = "INGRESO DE FACTURACIÓN SAP CARGADO CORRECTAMENTE";
                            resultado = true;
                        }
                        else
                        {
                            rta = 2;
                            mensaje = MsgRes.DescriptionResponse;
                        }
                    }
                    else
                    {
                        rta = 2;
                        mensaje = MsgRes.DescriptionResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                rta = 2;
                mensaje = MsgRes.DescriptionResponse + ex.Message;
            }

            return Json(new { success = resultado, rta = rta, msg = mensaje }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TableroFacturacionSAP(DateTime? fechaIni, DateTime? fechaFin)
        {
            List<management_facturacionSAP_listaResult> list = new List<management_facturacionSAP_listaResult>();
            management_facturacionSAP_listaResult consolidado = new management_facturacionSAP_listaResult();

            var opera = 0;

            if (fechaIni != null || fechaFin != null)
            {
                list = BusClass.ListarFacturacionSAP();
                opera = 1;
            }

            ViewBag.conteo = 0;
            ViewBag.resultado = 0;

            if (fechaIni != null)
            {
                list = list.Where(x => x.fecha_digita >= fechaIni).ToList();
            }

            if (fechaFin != null)
            {
                list = list.Where(x => x.fecha_digita <= fechaFin).ToList();
            }

            if (list.Count() > 0)
            {
                consolidado = list.FirstOrDefault();
                ViewBag.conteo = list.Sum(x => x.conteo);
                //ViewBag.conteo = consolidado.conteo;

                if (opera == 1)
                {
                    ViewBag.resultado = 1;
                }
            }
            else
            {
                if (opera == 1)
                {
                    ViewBag.resultado = 2;
                }
            }

            ViewBag.list = list;
            Session["Ini"] = fechaIni;
            Session["Fin"] = fechaFin;

            return View();

        }

        public string DevolverMes(int mes)
        {
            var Devueltames = "";

            if (mes == 1)
            {
                Devueltames = "Enero";
            }
            else if (mes == 2)
            {
                Devueltames = "Febrero";
            }
            else if (mes == 3)
            {
                Devueltames = "Marzo";
            }
            else if (mes == 4)
            {
                Devueltames = "Abril";
            }
            else if (mes == 5)
            {
                Devueltames = "Mayo";
            }
            else if (mes == 6)
            {
                Devueltames = "Junio";
            }
            else if (mes == 7)
            {
                Devueltames = "Julio";
            }
            else if (mes == 8)
            {
                Devueltames = "Agosto";
            }
            else if (mes == 9)
            {
                Devueltames = "Septiembre";
            }
            else if (mes == 10)
            {
                Devueltames = "Octubre";
            }
            else if (mes == 11)
            {
                Devueltames = "Noviembre";
            }
            else if (mes == 12)
            {
                Devueltames = "Diciembre";
            }
            return Devueltames;
        }

        public void DescargarDatosFacturacion()
        {
            DateTime fechaIni = (DateTime)Session["Ini"];
            DateTime fechaFin = (DateTime)Session["Fin"];

            List<management_facturacionSAP_listaREPORTEResult> list = new List<management_facturacionSAP_listaREPORTEResult>();
            list = BusClass.ListarFacturacionSAPReporte(fechaIni, fechaFin).ToList();

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Descargue");

            Sheet.Cells["A1:W1"].Style.Font.Bold = true;
            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:W1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:W1"].Style.Font.Color.SetColor(Color.White);
            Sheet.Cells["A1:W1"].Style.Font.Name = "Century Gothic";

            Sheet.Cells["A1"].Value = "Sociedad";
            Sheet.Cells["B1"].Value = "Cuenta";
            Sheet.Cells["C1"].Value = "NIT";
            Sheet.Cells["D1"].Value = "Cuenta de mayor";
            Sheet.Cells["E1"].Value = "Nº documento";
            Sheet.Cells["F1"].Value = "Clase de documento";
            Sheet.Cells["G1"].Value = "Referencia";
            Sheet.Cells["H1"].Value = "Referencia sin letras";
            Sheet.Cells["I1"].Value = "Fe.contabilización";
            Sheet.Cells["J1"].Value = "Fecha compensación";
            Sheet.Cells["K1"].Value = "Fecha de pago";

            Sheet.Cells["L1"].Value = "Moneda del documento";
            Sheet.Cells["M1"].Value = "Importe en moneda doc.";
            Sheet.Cells["N1"].Value = "Nombre del usuario";
            Sheet.Cells["O1"].Value = "Texto";
            Sheet.Cells["P1"].Value = "Importe en moneda local";
            Sheet.Cells["Q1"].Value = "Base p. plazo pago";
            Sheet.Cells["R1"].Value = "Clave referencia 3";
            Sheet.Cells["S1"].Value = "Año";
            Sheet.Cells["T1"].Value = "Mes";
            Sheet.Cells["U1"].Value = "Regional";
            Sheet.Cells["V1"].Value = "Unis";
            Sheet.Cells["W1"].Value = "Localidad";

            int row = 2;
            foreach (management_facturacionSAP_listaREPORTEResult item in list)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.Sociedad;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.cuenta;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.nit;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.cuenta_de_mayor;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.nro_documento;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.clase_documento;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.referencia;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.referencia_sin_letras;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.fe_contabilizacion;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_compensacion;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.fecha_pago;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.moneda_documento;
                Sheet.Cells[string.Format("M{0}", row)].Value = item.importe_moneda_doc;
                Sheet.Cells[string.Format("N{0}", row)].Value = item.nombre_usuario;
                Sheet.Cells[string.Format("O{0}", row)].Value = item.texto;
                Sheet.Cells[string.Format("P{0}", row)].Value = item.importe_moneda_local;
                Sheet.Cells[string.Format("Q{0}", row)].Value = item.base_p_plazo_pago;
                Sheet.Cells[string.Format("R{0}", row)].Value = item.clave_referencia_3;
                Sheet.Cells[string.Format("S{0}", row)].Value = item.año;
                Sheet.Cells[string.Format("T{0}", row)].Value = item.mes;
                Sheet.Cells[string.Format("U{0}", row)].Value = item.regional;
                Sheet.Cells[string.Format("V{0}", row)].Value = item.unis;
                Sheet.Cells[string.Format("W{0}", row)].Value = item.localidad;

                Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                Sheet.Cells[string.Format("K{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                row++;
            }

            Sheet.Cells["A" + row + ":W1" + row].Style.Font.Name = "Century Gothic";

            Sheet.Cells["A:W"].AutoFitColumns();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=RptFacturacionSAP.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

    }
}