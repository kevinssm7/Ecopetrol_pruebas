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
using Microsoft.Reporting.WebForms;
using ECOPETROL_COMMON.ENUM;
using AsaludEcopetrol.Models;

using Microsoft.IO;
using Aspose.Cells;
using System.Globalization;

namespace AsaludEcopetrol.Controllers.CuentasMedicas
{
    public class GastosxServicioController : Controller
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

        public ActionResult CargueGastos()
        {
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 19 de julio de 2022
        /// Nuevo metodo para insertar o cargar datos de gastos por servicio mediante ajax
        /// </summary>
        /// <param name="file"></param>
        /// <param name="mes"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public JsonResult SaveDatosGatosXServicio(HttpPostedFileBase file, int mes, int year, string regional)
        {
            string mensaje = "";
            Models.CuentasMedicas.GastosxServicio Model = new Models.CuentasMedicas.GastosxServicio();

            gasto_por_servicio_cargue_base cargue = Model.getcarguebase(mes, year, regional);
            if (cargue == null)
            {
                try
                {
                    if (this.Request.Files["file"].ContentLength > 0)
                    {
                        try
                        {
                            CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                            var asposeOptions = new Aspose.Cells.LoadOptions
                            {
                                MemorySetting = MemorySetting.MemoryPreference
                            };

                            Workbook wb = new Workbook(file.InputStream, asposeOptions);
                            Worksheet worksheet = wb.Worksheets[0];
                            Cells cells = worksheet.Cells;
                            int MaximaFila = cells.MaxDataRow;

                            var ExportTableOptions = new Aspose.Cells.ExportTableOptions
                            {
                                CheckMixedValueType = false,
                                ExportColumnName = true,
                                ExportAsString = true
                            };

                            DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

                            gasto_por_servicio_cargue_base obj = new gasto_por_servicio_cargue_base();
                            obj.mes = mes;
                            obj.año = year;
                            obj.tipoCargue = regional;
                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Model.InsertarDetallesGatosPorServicio(dataTable, obj, ref MsgRes);

                            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                            {
                                mensaje = "Los datos se cargaron correctamente.";
                                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mensaje = MsgRes.DescriptionResponse;

                                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (Exception e)
                        {
                            mensaje = "ERROR AL INGRESAR LOS REGISTROS: " + e.Message;
                            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        mensaje = "*---DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE -- - *";
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
                mensaje = "Ya se ha realizado un cargue de gastos por servicio en el periodo seleccionado.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 19 de julio de 2022
        /// Metodo o html que muestra el tablero de control de gastos por servicio
        /// </summary>
        /// <returns></returns>
        public ActionResult TableroControlGastosPorServicio()
        {
            Models.CuentasMedicas.GastosxServicio Model = new Models.CuentasMedicas.GastosxServicio();
            ViewBag.rol = SesionVar.ROL;

            return View(Model.ObtenerListadoCarguesGastoPorServicio());
        }

        /// <summary>
        /// Autor: Alexis Quiñones
        /// </summary>
        /// <param name="idCargueBase"></param>
        public void ExcelReporteGastosPorServicio(int idCargueBase)
        {
            try
            {
                Models.CuentasMedicas.GastosxServicio Model = new Models.CuentasMedicas.GastosxServicio();
                List<Management_gasto_por_servicio_por_idCargueBaseResult> listareporte = Model.GetListGastoPorServicioDtllPorIdCargueBase(idCargueBase);
                if (listareporte.Count > 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte GastosPorServicio");

                    Sheet.Cells["A1:BC1"].Style.Font.Bold = true;
                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:BC1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:BC1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:BC1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:BC1"].Style.Font.Name = "Century Gothic";
                    Sheet.Cells["A1:BC1"].Style.Font.Family = 12;

                    Sheet.Cells["A1"].Value = "Periodo";
                    Sheet.Cells["B1"].Value = "LocPrestacion";
                    Sheet.Cells["C1"].Value = "Analista";
                    Sheet.Cells["D1"].Value = "FecFac";
                    Sheet.Cells["E1"].Value = "FecRec";
                    Sheet.Cells["F1"].Value = "FecAut";
                    Sheet.Cells["G1"].Value = "FecCon";
                    Sheet.Cells["H1"].Value = "Factura";
                    Sheet.Cells["I1"].Value = "ConsecPers";
                    Sheet.Cells["J1"].Value = "TipoIDPrestador";
                    Sheet.Cells["K1"].Value = "IDPrestador";
                    Sheet.Cells["L1"].Value = "CodHab";
                    Sheet.Cells["M1"].Value = "CodSAP";
                    Sheet.Cells["N1"].Value = "Pedido";
                    Sheet.Cells["O1"].Value = "Prestador";
                    Sheet.Cells["P1"].Value = "ConsecFac";
                    Sheet.Cells["Q1"].Value = "ConsecBen";
                    Sheet.Cells["R1"].Value = "TipoIDUsuario";
                    Sheet.Cells["S1"].Value = "IDUsuario";
                    Sheet.Cells["T1"].Value = "HisUsuario";
                    Sheet.Cells["U1"].Value = "NombreUsuario";
                    Sheet.Cells["V1"].Value = "FecNac";
                    Sheet.Cells["W1"].Value = "Sexo";
                    Sheet.Cells["X1"].Value = "CodLocUsuario";
                    Sheet.Cells["Y1"].Value = "LocUsuario";
                    Sheet.Cells["Z1"].Value = "CECOS";
                    Sheet.Cells["AA1"].Value = "SerSum";
                    Sheet.Cells["AB1"].Value = "CodSerSum";
                    Sheet.Cells["AC1"].Value = "NomSerSum";
                    Sheet.Cells["AD1"].Value = "TIGA";
                    Sheet.Cells["AE1"].Value = "TipoSer";
                    Sheet.Cells["AF1"].Value = "FecPre";
                    Sheet.Cells["AG1"].Value = "CantidadSerSum";
                    Sheet.Cells["AH1"].Value = "Valor";
                    Sheet.Cells["AI1"].Value = "Dx";
                    Sheet.Cells["AJ1"].Value = "DescripDx";
                    Sheet.Cells["AK1"].Value = "GrupoDx";
                    Sheet.Cells["AL1"].Value = "Edad";
                    Sheet.Cells["AM1"].Value = "Quinquenio";
                    Sheet.Cells["AN1"].Value = "TIGADetalle";
                    Sheet.Cells["AO1"].Value = "TIGAGSD";
                    Sheet.Cells["AP1"].Value = "DescripTIGAGSD";
                    Sheet.Cells["AU1"].Value = "RegPrestacion";
                    Sheet.Cells["AR1"].Value = "DescripCECOS";
                    Sheet.Cells["AS1"].Value = "TipoSalud";
                    Sheet.Cells["AT1"].Value = "TipoPoblacion";
                    Sheet.Cells["AU1"].Value = "UnicoID";
                    Sheet.Cells["AV1"].Value = "UnicoFactura";
                    Sheet.Cells["AW1"].Value = "Eventos";
                    Sheet.Cells["AX1"].Value = "RegReporte";
                    Sheet.Cells["AY1"].Value = "IDQuinquenio";
                    Sheet.Cells["AZ1"].Value = "UNIS";
                    Sheet.Cells["BA1"].Value = "CausadaOtraReg";
                    Sheet.Cells["BB1"].Value = "Latitud";
                    Sheet.Cells["BC1"].Value = "Longitud";


                    int row = 2;
                    foreach (Management_gasto_por_servicio_por_idCargueBaseResult item in listareporte)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.Periodo;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.LocPrestacion;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.Analista;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.FecFac;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.FecRec;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.FecAut;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.FecCon;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.Factura;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.ConsecPers;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.TipoIDPrestador;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.IDPrestador;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.CodHab;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.CodSAP;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.Pedido;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.Prestador;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.ConsecFac;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.ConsecBen;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.TipoIDUsuario;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.IDUsuario;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.HisUsuario;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.NombreUsuario;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.FecNac;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.Sexo;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.CodLocUsuario;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.LocUsuario;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.CECOS;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.SerSum;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.CodSerSum;
                        Sheet.Cells[string.Format("AC{0}", row)].Value = item.NomSerSum;
                        Sheet.Cells[string.Format("AD{0}", row)].Value = item.TIGA;
                        Sheet.Cells[string.Format("AE{0}", row)].Value = item.TipoSer;
                        Sheet.Cells[string.Format("AF{0}", row)].Value = item.FecPre;
                        Sheet.Cells[string.Format("AG{0}", row)].Value = item.CantidadSerSum;
                        Sheet.Cells[string.Format("AH{0}", row)].Value = item.Valor;
                        Sheet.Cells[string.Format("AI{0}", row)].Value = item.Dx;
                        Sheet.Cells[string.Format("AJ{0}", row)].Value = item.DescripDx;
                        Sheet.Cells[string.Format("AK{0}", row)].Value = item.GrupoDx;
                        Sheet.Cells[string.Format("AL{0}", row)].Value = item.Edad;
                        Sheet.Cells[string.Format("AM{0}", row)].Value = item.Quinquenio;
                        Sheet.Cells[string.Format("AN{0}", row)].Value = item.TIGADetalle;
                        Sheet.Cells[string.Format("AO{0}", row)].Value = item.TIGAGSD;
                        Sheet.Cells[string.Format("AP{0}", row)].Value = item.DescripTIGAGSD;
                        Sheet.Cells[string.Format("AQ{0}", row)].Value = item.RegPrestacion;
                        Sheet.Cells[string.Format("AR{0}", row)].Value = item.DescripCECOS;
                        Sheet.Cells[string.Format("AS{0}", row)].Value = item.TipoSalud;
                        Sheet.Cells[string.Format("AT{0}", row)].Value = item.TipoPoblacion;
                        Sheet.Cells[string.Format("AU{0}", row)].Value = item.UnicoID;
                        Sheet.Cells[string.Format("AV{0}", row)].Value = item.UnicoFactura;
                        Sheet.Cells[string.Format("AW{0}", row)].Value = item.Eventos;
                        Sheet.Cells[string.Format("AX{0}", row)].Value = item.RegReporte;
                        Sheet.Cells[string.Format("AY{0}", row)].Value = item.IDQuinquenio;
                        Sheet.Cells[string.Format("AZ{0}", row)].Value = item.UNIS;
                        Sheet.Cells[string.Format("BA{0}", row)].Value = item.CausadaOtraReg;
                        Sheet.Cells[string.Format("BB{0}", row)].Value = item.Latitud;
                        Sheet.Cells[string.Format("BC{0}", row)].Value = item.Longitud;
                        row++;
                    }
                    Sheet.Cells["A:BC"].AutoFitColumns();


                    Response.Clear();
                    Response.ContentType = "application/excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteCargueGastoxSer.xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Error: " + ex.Message;
                throw new Exception(mensaje);
            }
        }

        public PartialViewResult ConsolidadoGastoPorServicio(int idCargueBase)
        {
            Models.CuentasMedicas.GastosxServicio Model = new Models.CuentasMedicas.GastosxServicio();
            List<Management_gasto_x_servicio_consolidadoResult> modelo = Model.ObtenerConsolidadoGastoPorServicioPorIdCargueBase(idCargueBase).OrderBy(l => l.id_tipo_servicio).ToList();
            Session["ConsolidadoGastoXServicio"] = modelo;
            return PartialView(modelo);
        }

        public void ExcelConsolidadoGastosPorServicio()
        {
            try
            {
                List<Management_gasto_x_servicio_consolidadoResult> listareporte = new List<Management_gasto_x_servicio_consolidadoResult>();
                listareporte = (List<Management_gasto_x_servicio_consolidadoResult>)Session["ConsolidadoGastoXServicio"];

                if (listareporte.Count > 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte Consolidado GXS");

                    Sheet.Cells["A1:D1"].Style.Font.Bold = true;
                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:D1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:D1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:D1"].Style.Font.Name = "Century Gothic";
                    Sheet.Cells["A1:D1"].Style.Font.Family = 12;

                    Sheet.Cells["A1"].Value = "Item";
                    Sheet.Cells["B1"].Value = "Servicio";
                    Sheet.Cells["C1"].Value = "Eventos";
                    Sheet.Cells["D1"].Value = "Gastos Cop";

                    int row = 2;
                    foreach (Management_gasto_x_servicio_consolidadoResult item in listareporte)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_tipo_servicio;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.nom_servicio;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.eventos;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.gasto_cop;
                        row++;
                    }
                    Sheet.Cells["A:D"].AutoFitColumns();


                    Response.Clear();
                    Response.ContentType = "application/excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteConsolidadoGXS.xlsx");
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
                string mensaje = "Error: " + ex.Message;

                string rta = "<script LANGUAGE='JavaScript'>" +
                               "window.alert('ERROR EN LA DESCARGA');" +
                               "</script> ";
                Response.Write(rta);
                Response.End();
                //throw new Exception(mensaje);
            }
        }

        /// <summary>
        /// Autor Alexis Quiñones Castillo
        /// Fecha: 5 de agosto de 2022
        /// Metodo que se utiliza para eliminar un cargue de gasto por servicio
        /// </summary>
        /// <param name="idCargueBase"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EliminarCargueBaseGastoPorServicio(int idCargueBase)
        {
            try
            {
                var elimina = BusClass.EliminarCargueGastoPorServicio(idCargueBase);
                if (elimina != 0)
                {
                    log_gastoxServicio_eliminarConsolidado eli = new log_gastoxServicio_eliminarConsolidado();
                    eli.id_cargue = idCargueBase;
                    eli.usuario_digita = SesionVar.UserName;
                    eli.fecha_digita = DateTime.Now;

                    var registro = BusClass.InsertarLogEliminarGastoxServicio(eli);

                    if (registro != 0)
                    {
                        return Json(new { success = true, mensaje = "El cargue de GXS seleccionado se ha eliminado correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, mensaje = "Ha ocurrido un error al momento de eliminar el cargue. Comuniquese con el administrador del sistema para solucionar el inconveniente" }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    return Json(new { success = false, mensaje = "Ha ocurrido un error al momento de eliminar el cargue. Comuniquese con el administrador del sistema para solucionar el inconveniente" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { success = false, mensaje = "Ha ocurrido un error al momento de eliminar el cargue. Comuniquese con el administrador del sistema para solucionar el inconveniente" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TableroGastoXServicioConsulta()
        {
            return View();
        }

        //public void ExcelConsultaGXS(string factura, string cedula, string servicio, DateTime? fechaInicio, DateTime? fechaFin, DateTime? fechaInicioPre, DateTime? fechaFinPre, string tigaGSD)
        //{
        //    try
        //    {
        //        List<management_gastoxservicio_consultaResult> lista = new List<management_gastoxservicio_consultaResult>();

        //        string[] filtroFactura = factura.Split(';');
        //        string[] filtroCedula = cedula.Split(';');
        //        string[] filtroServicio = servicio.Split(';');
        //        string[] filtroTiga = tigaGSD.Split(';');

        //        var captura = 0;

        //        if (!string.IsNullOrEmpty(factura))
        //        {
        //            List<management_gastoxservicio_consultaResult> listaFinal2 = new List<management_gastoxservicio_consultaResult>();

        //            for (var i = 0; i < filtroFactura.Count(); i++)
        //            {
        //                List<management_gastoxservicio_consultaResult> lista2 = new List<management_gastoxservicio_consultaResult>();
        //                var variableTomada = filtroFactura[i];
        //                variableTomada = variableTomada.Replace(" ", "");

        //                if (captura == 0)
        //                {
        //                    lista = BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, variableTomada, 0, "", "", fechaInicioPre, fechaFinPre);
        //                }

        //                lista2 = lista.Where(x => x.Factura.Contains(variableTomada)).ToList();
        //                listaFinal2.AddRange(lista2);
        //            }
        //            captura = 1;

        //            lista = listaFinal2;
        //        }

        //        if (!string.IsNullOrEmpty(cedula))
        //        {

        //            List<management_gastoxservicio_consultaResult> listaFinal2 = new List<management_gastoxservicio_consultaResult>();

        //            for (var i = 0; i < filtroCedula.Count(); i++)
        //            {
        //                List<management_gastoxservicio_consultaResult> lista2 = new List<management_gastoxservicio_consultaResult>();

        //                var variableTomada = filtroCedula[i];
        //                variableTomada = variableTomada.Replace(" ", "");

        //                if (captura == 0)
        //                {

        //                    lista = BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, "", Convert.ToInt32(variableTomada), "", "", fechaInicioPre, fechaFinPre);
        //                }

        //                lista2 = lista.Where(x => Convert.ToString(x.IDUsuario).Contains(variableTomada)).ToList();
        //                listaFinal2.AddRange(lista2);
        //            }

        //            captura = 1;
        //            lista = listaFinal2;
        //        }

        //        if (!string.IsNullOrEmpty(servicio))
        //        {
        //            List<management_gastoxservicio_consultaResult> listaFinal2 = new List<management_gastoxservicio_consultaResult>();


        //            for (var i = 0; i < filtroServicio.Count(); i++)
        //            {
        //                List<management_gastoxservicio_consultaResult> lista2 = new List<management_gastoxservicio_consultaResult>();

        //                var variableTomada = filtroServicio[i];
        //                variableTomada = variableTomada.Replace(" ", "");

        //                if (captura == 0)
        //                {
        //                    lista = BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, "", 0, variableTomada, "", fechaInicioPre, fechaFinPre);

        //                }

        //                lista2 = lista.Where(x => Convert.ToString(x.CodSerSum).Contains(variableTomada) || Convert.ToString(x.NomSerSum).Contains(variableTomada)).ToList();
        //                listaFinal2.AddRange(lista2);
        //            }

        //            captura = 1;

        //            lista = listaFinal2;
        //        }


        //        if (!string.IsNullOrEmpty(tigaGSD))
        //        {
        //            List<management_gastoxservicio_consultaResult> listaFinal2 = new List<management_gastoxservicio_consultaResult>();

        //            for (var i = 0; i < filtroTiga.Count(); i++)
        //            {
        //                List<management_gastoxservicio_consultaResult> lista2 = new List<management_gastoxservicio_consultaResult>();

        //                var variableTomada = filtroTiga[i];
        //                variableTomada = variableTomada.Replace(" ", "");

        //                if (captura == 0)
        //                {
        //                    lista = BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, "", 0, "", variableTomada, fechaInicioPre, fechaFinPre);
        //                }

        //                lista2 = lista.Where(x => Convert.ToString(x.TIGAGSD).Contains(variableTomada)).ToList();

        //                listaFinal2.AddRange(lista2);
        //            }

        //            captura = 1;

        //            lista = listaFinal2;
        //        }

        //        if (captura == 0)
        //        {
        //            lista = BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, "", 0, "", "", fechaInicioPre, fechaFinPre);
        //        }


        //        if (lista.Count > 0)
        //        {
        //            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //            ExcelPackage Ep = new ExcelPackage();
        //            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ReporteConsultaGastosPorServicio");

        //            Sheet.Cells["A1:BC1"].Style.Font.Bold = true;
        //            Color colFromHex = Color.FromArgb(22, 54, 92);
        //            Sheet.Cells["A1:BC1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            Sheet.Cells["A1:BC1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
        //            Sheet.Cells["A1:BC1"].Style.Font.Color.SetColor(Color.White);
        //            Sheet.Cells["A1:BC1"].Style.Font.Name = "Century Gothic";
        //            Sheet.Cells["A1:BC1"].Style.Font.Family = 12;

        //            Sheet.Cells["A1"].Value = "Periodo";
        //            Sheet.Cells["B1"].Value = "LocPrestacion";
        //            Sheet.Cells["C1"].Value = "Analista";
        //            Sheet.Cells["D1"].Value = "FecFac";
        //            Sheet.Cells["E1"].Value = "FecRec";
        //            Sheet.Cells["F1"].Value = "FecAut";
        //            Sheet.Cells["G1"].Value = "FecCon";
        //            Sheet.Cells["H1"].Value = "Factura";
        //            Sheet.Cells["I1"].Value = "ConsecPers";
        //            Sheet.Cells["J1"].Value = "TipoIDPrestador";
        //            Sheet.Cells["K1"].Value = "IDPrestador";
        //            Sheet.Cells["L1"].Value = "CodHab";
        //            Sheet.Cells["M1"].Value = "CodSAP";
        //            Sheet.Cells["N1"].Value = "Pedido";
        //            Sheet.Cells["O1"].Value = "Prestador";
        //            Sheet.Cells["P1"].Value = "ConsecFac";
        //            Sheet.Cells["Q1"].Value = "ConsecBen";
        //            Sheet.Cells["R1"].Value = "TipoIDUsuario";
        //            Sheet.Cells["S1"].Value = "IDUsuario";
        //            Sheet.Cells["T1"].Value = "HisUsuario";
        //            Sheet.Cells["U1"].Value = "NombreUsuario";
        //            Sheet.Cells["V1"].Value = "FecNac";
        //            Sheet.Cells["W1"].Value = "Sexo";
        //            Sheet.Cells["X1"].Value = "CodLocUsuario";
        //            Sheet.Cells["Y1"].Value = "LocUsuario";
        //            Sheet.Cells["Z1"].Value = "CECOS";
        //            Sheet.Cells["AA1"].Value = "SerSum";
        //            Sheet.Cells["AB1"].Value = "CodSerSum";
        //            Sheet.Cells["AC1"].Value = "NomSerSum";
        //            Sheet.Cells["AD1"].Value = "TIGA";
        //            Sheet.Cells["AE1"].Value = "TipoSer";
        //            Sheet.Cells["AF1"].Value = "FecPre";
        //            Sheet.Cells["AG1"].Value = "CantidadSerSum";
        //            Sheet.Cells["AH1"].Value = "Valor";
        //            Sheet.Cells["AI1"].Value = "Dx";
        //            Sheet.Cells["AJ1"].Value = "DescripDx";
        //            Sheet.Cells["AK1"].Value = "GrupoDx";
        //            Sheet.Cells["AL1"].Value = "Edad";
        //            Sheet.Cells["AM1"].Value = "Quinquenio";
        //            Sheet.Cells["AN1"].Value = "TIGADetalle";
        //            Sheet.Cells["AO1"].Value = "TIGAGSD";
        //            Sheet.Cells["AP1"].Value = "DescripTIGAGSD";
        //            Sheet.Cells["AQ1"].Value = "RegPrestacion";
        //            Sheet.Cells["AR1"].Value = "DescripCECOS";
        //            Sheet.Cells["AS1"].Value = "TipoSalud";
        //            Sheet.Cells["AT1"].Value = "TipoPoblacion";
        //            Sheet.Cells["AU1"].Value = "UnicoID";
        //            Sheet.Cells["AV1"].Value = "UnicoFactura";
        //            Sheet.Cells["AW1"].Value = "Eventos";
        //            Sheet.Cells["AX1"].Value = "RegReporte";
        //            Sheet.Cells["AY1"].Value = "IDQuinquenio";
        //            Sheet.Cells["AZ1"].Value = "UNIS";
        //            Sheet.Cells["BA1"].Value = "CausadaOtraReg";
        //            Sheet.Cells["BB1"].Value = "Latitud";
        //            Sheet.Cells["BC1"].Value = "Longitud";

        //            int row = 2;
        //            foreach (management_gastoxservicio_consultaResult item in lista)
        //            {
        //                Sheet.Cells[string.Format("A{0}", row)].Value = item.Periodo;
        //                Sheet.Cells[string.Format("B{0}", row)].Value = item.LocPrestacion;
        //                Sheet.Cells[string.Format("C{0}", row)].Value = item.Analista;
        //                Sheet.Cells[string.Format("D{0}", row)].Value = item.FecFac;
        //                Sheet.Cells[string.Format("E{0}", row)].Value = item.FecRec;
        //                Sheet.Cells[string.Format("F{0}", row)].Value = item.FecAut;
        //                Sheet.Cells[string.Format("G{0}", row)].Value = item.FecCon;
        //                Sheet.Cells[string.Format("H{0}", row)].Value = item.Factura;
        //                Sheet.Cells[string.Format("I{0}", row)].Value = item.ConsecPers;
        //                Sheet.Cells[string.Format("J{0}", row)].Value = item.TipoIDPrestador;
        //                Sheet.Cells[string.Format("K{0}", row)].Value = item.IDPrestador;
        //                Sheet.Cells[string.Format("L{0}", row)].Value = item.CodHab;
        //                Sheet.Cells[string.Format("M{0}", row)].Value = item.CodSAP;
        //                Sheet.Cells[string.Format("N{0}", row)].Value = item.Pedido;
        //                Sheet.Cells[string.Format("O{0}", row)].Value = item.Prestador;
        //                Sheet.Cells[string.Format("P{0}", row)].Value = item.ConsecFac;
        //                Sheet.Cells[string.Format("Q{0}", row)].Value = item.ConsecBen;
        //                Sheet.Cells[string.Format("R{0}", row)].Value = item.TipoIDUsuario;
        //                Sheet.Cells[string.Format("S{0}", row)].Value = item.IDUsuario;
        //                Sheet.Cells[string.Format("T{0}", row)].Value = item.HisUsuario;
        //                Sheet.Cells[string.Format("U{0}", row)].Value = item.NombreUsuario;
        //                Sheet.Cells[string.Format("V{0}", row)].Value = item.FecNac;
        //                Sheet.Cells[string.Format("W{0}", row)].Value = item.Sexo;
        //                Sheet.Cells[string.Format("X{0}", row)].Value = item.CodLocUsuario;
        //                Sheet.Cells[string.Format("Y{0}", row)].Value = item.LocUsuario;
        //                Sheet.Cells[string.Format("Z{0}", row)].Value = item.CECOS;
        //                Sheet.Cells[string.Format("AA{0}", row)].Value = item.SerSum;
        //                Sheet.Cells[string.Format("AB{0}", row)].Value = item.CodSerSum;
        //                Sheet.Cells[string.Format("AC{0}", row)].Value = item.NomSerSum;
        //                Sheet.Cells[string.Format("AD{0}", row)].Value = item.TIGA;
        //                Sheet.Cells[string.Format("AE{0}", row)].Value = item.TipoSer;
        //                Sheet.Cells[string.Format("AF{0}", row)].Value = item.FecPre;
        //                Sheet.Cells[string.Format("AG{0}", row)].Value = item.CantidadSerSum;
        //                Sheet.Cells[string.Format("AH{0}", row)].Value = item.Valor;
        //                Sheet.Cells[string.Format("AI{0}", row)].Value = item.Dx;
        //                Sheet.Cells[string.Format("AJ{0}", row)].Value = item.DescripDx;
        //                Sheet.Cells[string.Format("AK{0}", row)].Value = item.GrupoDx;
        //                Sheet.Cells[string.Format("AL{0}", row)].Value = item.Edad;
        //                Sheet.Cells[string.Format("AM{0}", row)].Value = item.Quinquenio;
        //                Sheet.Cells[string.Format("AN{0}", row)].Value = item.TIGADetalle;
        //                Sheet.Cells[string.Format("AO{0}", row)].Value = item.TIGAGSD;
        //                Sheet.Cells[string.Format("AP{0}", row)].Value = item.DescripTIGAGSD;
        //                Sheet.Cells[string.Format("AQ{0}", row)].Value = item.RegPrestacion;
        //                Sheet.Cells[string.Format("AR{0}", row)].Value = item.DescripCECOS;
        //                Sheet.Cells[string.Format("AS{0}", row)].Value = item.TipoSalud;
        //                Sheet.Cells[string.Format("AT{0}", row)].Value = item.TipoPoblacion;
        //                Sheet.Cells[string.Format("AU{0}", row)].Value = item.UnicoID;
        //                Sheet.Cells[string.Format("AV{0}", row)].Value = item.UnicoFactura;
        //                Sheet.Cells[string.Format("AW{0}", row)].Value = item.Eventos;
        //                Sheet.Cells[string.Format("AX{0}", row)].Value = item.RegReporte;
        //                Sheet.Cells[string.Format("AY{0}", row)].Value = item.IDQuinquenio;
        //                Sheet.Cells[string.Format("AZ{0}", row)].Value = item.UNIS;
        //                Sheet.Cells[string.Format("BA{0}", row)].Value = item.CausadaOtraReg;
        //                Sheet.Cells[string.Format("BB{0}", row)].Value = item.Latitud;
        //                Sheet.Cells[string.Format("BC{0}", row)].Value = item.Longitud;

        //                Sheet.Cells[string.Format("A{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        //                Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        //                Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        //                Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        //                Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        //                Sheet.Cells[string.Format("V{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        //                Sheet.Cells[string.Format("AF{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

        //                row++;
        //            }
        //            Sheet.Cells["A:BC"].AutoFitColumns();

        //            Response.Clear();
        //            Response.ContentType = "application/excel";
        //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteConsultaGXS.xlsx");
        //            Response.BinaryWrite(Ep.GetAsByteArray());
        //            Response.End();
        //        }
        //        else
        //        {
        //            string rta = "<script>" +
        //          "window.close();" +
        //          "window.alert('No se han encontrado resultados');" +
        //          "</script> ";
        //            Response.Write(rta);
        //            Response.End();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "Error: " + ex.Message;
        //        throw new Exception(mensaje);
        //    }
        //}

        //public void ExcelConsultaGXS(string factura, string cedula, string servicio, DateTime? fechaInicio, DateTime? fechaFin, 
        //    DateTime? fechaInicioPre, DateTime? fechaFinPre, string tigaGSD)
        //{
        //    try
        //    {
        //        var filtros = new
        //        {
        //            Factura = factura?.Split(';').Select(f => f.Trim()).Where(f => !string.IsNullOrEmpty(f)).ToList(),
        //            Cedula = cedula?.Split(';').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToList(),
        //            Servicio = servicio?.Split(';').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList(),
        //            TigaGSD = tigaGSD?.Split(';').Select(t => t.Trim()).Where(t => !string.IsNullOrEmpty(t)).ToList()
        //        };

        //        List<management_gastoxservicio_consultaResult> lista = new List<management_gastoxservicio_consultaResult>();

        //        if (filtros.Factura.Any())
        //        {
        //            lista = FiltrarLista(filtros.Factura, (f) => BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, f, 0, "", "", fechaInicioPre, fechaFinPre), x => x.Factura);
        //        }

        //        if (filtros.Cedula.Any())
        //        {
        //            lista = FiltrarLista(filtros.Cedula, (c) => BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, "", int.Parse(c), "", "", fechaInicioPre, fechaFinPre), x => x.IDUsuario.ToString());
        //        }

        //        if (filtros.Servicio.Any())
        //        {
        //            lista = FiltrarLista(filtros.Servicio, (s) => BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, "", 0, s, "", fechaInicioPre, fechaFinPre), x => x.CodSerSum + x.NomSerSum);
        //        }

        //        if (filtros.TigaGSD.Any())
        //        {
        //            lista = FiltrarLista(filtros.TigaGSD, (t) => BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, "", 0, "", t, fechaInicioPre, fechaFinPre), x => x.TIGAGSD.ToString());
        //        }

        //        if (!lista.Any())
        //        {
        //            lista = BusClass.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, "", 0, "", "", fechaInicioPre, fechaFinPre);
        //        }

        //        if (lista.Any())
        //        {
        //            GenerarExcel(lista);
        //        }
        //        else
        //        {
        //            MostrarAlertaNoResultados();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error: " + ex.Message);
        //    }
        //}

        //private List<management_gastoxservicio_consultaResult> FiltrarLista(
        //    List<string> filtros,
        //    Func<string, List<management_gastoxservicio_consultaResult>> obtenerDatos,
        //    Func<management_gastoxservicio_consultaResult, string> selector
        //)
        //{
        //    var listaFinal = new List<management_gastoxservicio_consultaResult>();
        //    foreach (var filtro in filtros)
        //    {
        //        var datos = obtenerDatos(filtro);
        //        listaFinal.AddRange(datos.Where(x => selector(x).Contains(filtro)));
        //    }
        //    return listaFinal;
        //}

        public void ExcelConsultaGXS(string factura, string cedula, string servicio, DateTime? fechaInicio, DateTime? fechaFin,
          DateTime? fechaInicioPre, DateTime? fechaFinPre, string tigaGSD)
        {
            try
            {

                List<management_gastoxservicio_consulta2Result> lista = new List<management_gastoxservicio_consulta2Result>();
                lista = BusClass.ObtenerGastoPorsercicioConsulta2(fechaInicio, fechaFin, factura, cedula, servicio, tigaGSD, fechaInicioPre, fechaFinPre);

                if (lista.Any())
                {
                    GenerarExcel(lista);
                }
                else
                {
                    MostrarAlertaNoResultados();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        private void MostrarAlertaNoResultados()
        {
            var script = "<script>window.close(); window.alert('No se han encontrado resultados');</script>";
            Response.Write(script);
            Response.End();
        }

        private void GenerarExcel(List<management_gastoxservicio_consulta2Result> lista)
        {

            if (lista.Count > 0)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ReporteConsultaGastosPorServicio");

                Sheet.Cells["A1:BC1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:BC1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:BC1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:BC1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:BC1"].Style.Font.Name = "Century Gothic";
                Sheet.Cells["A1:BC1"].Style.Font.Family = 12;

                Sheet.Cells["A1"].Value = "Periodo";
                Sheet.Cells["B1"].Value = "LocPrestacion";
                Sheet.Cells["C1"].Value = "Analista";
                Sheet.Cells["D1"].Value = "FecFac";
                Sheet.Cells["E1"].Value = "FecRec";
                Sheet.Cells["F1"].Value = "FecAut";
                Sheet.Cells["G1"].Value = "FecCon";
                Sheet.Cells["H1"].Value = "Factura";
                Sheet.Cells["I1"].Value = "ConsecPers";
                Sheet.Cells["J1"].Value = "TipoIDPrestador";
                Sheet.Cells["K1"].Value = "IDPrestador";
                Sheet.Cells["L1"].Value = "CodHab";
                Sheet.Cells["M1"].Value = "CodSAP";
                Sheet.Cells["N1"].Value = "Pedido";
                Sheet.Cells["O1"].Value = "Prestador";
                Sheet.Cells["P1"].Value = "ConsecFac";
                Sheet.Cells["Q1"].Value = "ConsecBen";
                Sheet.Cells["R1"].Value = "TipoIDUsuario";
                Sheet.Cells["S1"].Value = "IDUsuario";
                Sheet.Cells["T1"].Value = "HisUsuario";
                Sheet.Cells["U1"].Value = "NombreUsuario";
                Sheet.Cells["V1"].Value = "FecNac";
                Sheet.Cells["W1"].Value = "Sexo";
                Sheet.Cells["X1"].Value = "CodLocUsuario";
                Sheet.Cells["Y1"].Value = "LocUsuario";
                Sheet.Cells["Z1"].Value = "CECOS";
                Sheet.Cells["AA1"].Value = "SerSum";
                Sheet.Cells["AB1"].Value = "CodSerSum";
                Sheet.Cells["AC1"].Value = "NomSerSum";
                Sheet.Cells["AD1"].Value = "TIGA";
                Sheet.Cells["AE1"].Value = "TipoSer";
                Sheet.Cells["AF1"].Value = "FecPre";
                Sheet.Cells["AG1"].Value = "CantidadSerSum";
                Sheet.Cells["AH1"].Value = "Valor";
                Sheet.Cells["AI1"].Value = "Dx";
                Sheet.Cells["AJ1"].Value = "DescripDx";
                Sheet.Cells["AK1"].Value = "GrupoDx";
                Sheet.Cells["AL1"].Value = "Edad";
                Sheet.Cells["AM1"].Value = "Quinquenio";
                Sheet.Cells["AN1"].Value = "TIGADetalle";
                Sheet.Cells["AO1"].Value = "TIGAGSD";
                Sheet.Cells["AP1"].Value = "DescripTIGAGSD";
                Sheet.Cells["AQ1"].Value = "RegPrestacion";
                Sheet.Cells["AR1"].Value = "DescripCECOS";
                Sheet.Cells["AS1"].Value = "TipoSalud";
                Sheet.Cells["AT1"].Value = "TipoPoblacion";
                Sheet.Cells["AU1"].Value = "UnicoID";
                Sheet.Cells["AV1"].Value = "UnicoFactura";
                Sheet.Cells["AW1"].Value = "Eventos";
                Sheet.Cells["AX1"].Value = "RegReporte";
                Sheet.Cells["AY1"].Value = "IDQuinquenio";
                Sheet.Cells["AZ1"].Value = "UNIS";
                Sheet.Cells["BA1"].Value = "CausadaOtraReg";
                Sheet.Cells["BB1"].Value = "Latitud";
                Sheet.Cells["BC1"].Value = "Longitud";

                int row = 2;
                foreach (management_gastoxservicio_consulta2Result item in lista)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.Periodo;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.LocPrestacion;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.Analista;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.FecFac;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.FecRec;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.FecAut;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.FecCon;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.Factura;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.ConsecPers;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.TipoIDPrestador;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.IDPrestador;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.CodHab;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.CodSAP;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.Pedido;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.Prestador;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.ConsecFac;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.ConsecBen;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.TipoIDUsuario;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.IDUsuario;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.HisUsuario;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.NombreUsuario;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.FecNac;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.Sexo;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.CodLocUsuario;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.LocUsuario;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.CECOS;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.SerSum;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.CodSerSum;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = item.NomSerSum;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = item.TIGA;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = item.TipoSer;
                    Sheet.Cells[string.Format("AF{0}", row)].Value = item.FecPre;
                    Sheet.Cells[string.Format("AG{0}", row)].Value = item.CantidadSerSum;
                    Sheet.Cells[string.Format("AH{0}", row)].Value = item.Valor;
                    Sheet.Cells[string.Format("AI{0}", row)].Value = item.Dx;
                    Sheet.Cells[string.Format("AJ{0}", row)].Value = item.DescripDx;
                    Sheet.Cells[string.Format("AK{0}", row)].Value = item.GrupoDx;
                    Sheet.Cells[string.Format("AL{0}", row)].Value = item.Edad;
                    Sheet.Cells[string.Format("AM{0}", row)].Value = item.Quinquenio;
                    Sheet.Cells[string.Format("AN{0}", row)].Value = item.TIGADetalle;
                    Sheet.Cells[string.Format("AO{0}", row)].Value = item.TIGAGSD;
                    Sheet.Cells[string.Format("AP{0}", row)].Value = item.DescripTIGAGSD;
                    Sheet.Cells[string.Format("AQ{0}", row)].Value = item.RegPrestacion;
                    Sheet.Cells[string.Format("AR{0}", row)].Value = item.DescripCECOS;
                    Sheet.Cells[string.Format("AS{0}", row)].Value = item.TipoSalud;
                    Sheet.Cells[string.Format("AT{0}", row)].Value = item.TipoPoblacion;
                    Sheet.Cells[string.Format("AU{0}", row)].Value = item.UnicoID;
                    Sheet.Cells[string.Format("AV{0}", row)].Value = item.UnicoFactura;
                    Sheet.Cells[string.Format("AW{0}", row)].Value = item.Eventos;
                    Sheet.Cells[string.Format("AX{0}", row)].Value = item.RegReporte;
                    Sheet.Cells[string.Format("AY{0}", row)].Value = item.IDQuinquenio;
                    Sheet.Cells[string.Format("AZ{0}", row)].Value = item.UNIS;
                    Sheet.Cells[string.Format("BA{0}", row)].Value = item.CausadaOtraReg;
                    Sheet.Cells[string.Format("BB{0}", row)].Value = item.Latitud;
                    Sheet.Cells[string.Format("BC{0}", row)].Value = item.Longitud;

                    Sheet.Cells[string.Format("A{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("V{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("AF{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }
                Sheet.Cells["A:BC"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteConsultaGXS.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                MostrarAlertaNoResultados();
            }
        }

        private void EnviarArchivoExcel(ExcelPackage ep)
        {
            Response.Clear();
            Response.ContentType = "application/excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ReporteConsultaGXS.xlsx");
            Response.BinaryWrite(ep.GetAsByteArray());
            Response.End();
        }

        public JsonResult BuscarNombre_Servicio()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    List<management_gastoServicio_nombreServicioResult> med = BusClass.ConsultarNombreServicioGXS(term);

                    var lista = (from m in med
                                 select new
                                 {
                                     codigo = m.CodSerSum,
                                     label = m.CodSerSum + "-" + m.NomSerSum
                                 }).Distinct().OrderBy(f => f.label).Take(15);
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CargarCierreContable()
        {
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        public JsonResult SaveDatosCierreContable(HttpPostedFileBase file, int mes, int year, int? regional)
        {
            string mensaje = "";
            Models.CuentasMedicas.GastosxServicio Model = new Models.CuentasMedicas.GastosxServicio();

            cierre_contable_cargue_base cargue = BusClass.traerCierreContable(mes, year, regional);
            if (cargue == null)
            {
                try
                {
                    if (this.Request.Files["file"].ContentLength > 0)
                    {
                        try
                        {
                            CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                            var asposeOptions = new Aspose.Cells.LoadOptions
                            {
                                MemorySetting = MemorySetting.MemoryPreference
                            };

                            Workbook wb = new Workbook(file.InputStream, asposeOptions);
                            Worksheet worksheet = wb.Worksheets[0];
                            Cells cells = worksheet.Cells;
                            int MaximaFila = cells.MaxDataRow;

                            var ExportTableOptions = new Aspose.Cells.ExportTableOptions
                            {
                                CheckMixedValueType = false,
                                ExportColumnName = true,
                                ExportAsString = true
                            };

                            DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

                            cierre_contable_cargue_base obj = new cierre_contable_cargue_base();
                            obj.mes = mes;
                            obj.año = year;
                            obj.regional = regional;
                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Model.InsertarDetallesCierreContable(dataTable, obj, ref MsgRes);

                            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                            {
                                mensaje = "Los datos se cargaron correctamente.";
                                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mensaje = MsgRes.DescriptionResponse;
                                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (Exception e)
                        {
                            mensaje = "ERROR AL INGRESAR LOS REGISTROS: " + e.Message;
                            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        mensaje = "*---DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE -- - *";
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
                mensaje = "Ya se ha realizado un cargue de cierre contable en el periodo seleccionado.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TableroControlCierres()
        {
            ViewBag.rol = SesionVar.ROL;
            List<management_cierre_contable_tableroControlResult> listado = new List<management_cierre_contable_tableroControlResult>();
            var conteo = 0;
            try
            {
                listado = BusClass.TraerDatosCierreContable();
                conteo = listado.Count();
            }
            catch (Exception ex)
            {
                var eeror = ex.Message;
            }
            ViewBag.listado = listado;
            ViewBag.conteo = conteo;

            return View();
        }

        public PartialViewResult ConsolidadoCierreContable(int idCargue)
        {
            Models.CuentasMedicas.GastosxServicio Model = new Models.CuentasMedicas.GastosxServicio();
            //List<Management_gasto_x_servicio_consolidadoResult> modelo = Model.ObtenerConsolidadoGastoPorServicioPorIdCargueBase(idCargueBase).OrderBy(l => l.id_tipo_servicio).ToList();
            //Session["ConsolidadoGastoXServicio"] = modelo;
            return PartialView();
        }

        [HttpPost]
        public JsonResult EliminarCargueBaseCierreContable(int idCargue)
        {
            try
            {
                var elimina = BusClass.EliminarCargueCierreContable(idCargue);

                if (elimina != 0)
                {
                    log_cierreContable_eliminarConsolidado eli = new log_cierreContable_eliminarConsolidado();
                    eli.id_cargue = idCargue;
                    eli.usuario_digita = SesionVar.UserName;
                    eli.fecha_digita = DateTime.Now;

                    var registro = BusClass.InsertarLogEliminarCierreContable(eli);

                    if (registro != 0)
                    {
                        return Json(new { success = true, mensaje = "El cargue de Cierre Contable seleccionado se ha eliminado correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, mensaje = "Ha ocurrido un error al momento de eliminar el cargue. Comuniquese con el administrador del sistema para solucionar el inconveniente" }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    return Json(new { success = false, mensaje = "Ha ocurrido un error al momento de eliminar el cargue. Comuniquese con el administrador del sistema para solucionar el inconveniente" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { success = false, mensaje = "Ha ocurrido un error al momento de eliminar el cargue. Comuniquese con el administrador del sistema para solucionar el inconveniente" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}

