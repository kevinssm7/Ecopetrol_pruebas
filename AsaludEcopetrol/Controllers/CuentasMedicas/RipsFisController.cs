using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using ANALITICA_COMMON.ENTIDADES;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using System.Globalization;
using OfficeOpenXml;
using System.Data.Linq;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using System.Configuration;
using System.Text.RegularExpressions;
using AsaludEcopetrol.Models.CuentasMedicas;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using OfficeOpenXml.Style;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Runtime.Caching;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Text;
using Aspose.Cells;
using AsaludEcopetrol.Models;
using System.Drawing;

namespace AsaludEcopetrol.Controllers.CuentasMedicas
{
    public class RipsFisController : Controller
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

        #region Metodos


        public ActionResult ConsultarRipsFis()
        {

            ViewBag.listaRegionales = BusClass.TraerregionalesFis();
            ViewBag.meses = BusClass.meses();


            return View();
        }




        public void ExportaraexcelReporteEvaluacion(int regional, int mes, int año)
        {
            Models.CuentasMedicas.RipsFis Model = new Models.CuentasMedicas.RipsFis();
            List<reporterips> reporte = new List<reporterips>();
            Ref_regional regionales = BusClass.GetRefRegion().Where(l => l.id_ref_regional == regional).FirstOrDefault();

            try
            {

                reporte = Model.ConsultaRipsFisEvaluacion(regional, mes, año, ref MsgRes).ToList();


                if (reporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                                 "window.alert('No se han encontrado resultados');" +
                                 "</script> ";
                    Response.Write(rta);
                    Response.End();
                }


                ExcelPackage Ep = new ExcelPackage();
                //Nombre de la hoja de calculo
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Evaluación");

                // Encabezados
                Sheet.Cells["A1:L1"].Style.Font.Bold = true;
                System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:L1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:L1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                Sheet.Cells["A1:L1"].Style.Font.Name = "Century Gothic";
                Sheet.Cells["A1:L1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                Sheet.Cells["A1:L1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                // Alinear columnas según tipo de dato
                Sheet.Cells["A:C"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;   // Texto
                Sheet.Cells["K:L"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;   // Regional y Fecha
                Sheet.Cells["D:J"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Números y %

                Sheet.Cells["A:L"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;     // Todo vertical centrado
                Sheet.Row(1).Height = 25;  // Aumenta alto de la fila



                Sheet.Cells["A1"].Value = "Código del prestador";
                Sheet.Cells["B1"].Value = "Razón social";
                Sheet.Cells["C1"].Value = "Ciudad prestación";
                Sheet.Cells["D1"].Value = "Cantidad";
                Sheet.Cells["E1"].Value = "Registros Facturados Oportunamente";
                Sheet.Cells["F1"].Value = "% Operación Facturación";
                //Sheet.Cells["G1"].Value = "Errores D.X";
                //Sheet.Cells["H1"].Value = "Errores P.X";
                //Sheet.Cells["I1"].Value = "Errores R.C";
                Sheet.Cells["G1"].Value = "Total Errores";
                Sheet.Cells["H1"].Value = "Registros Unicos con Error";
                Sheet.Cells["I1"].Value = "Registros sin error";
                Sheet.Cells["J1"].Value = "% Calidad RIPS";
                Sheet.Cells["K1"].Value = "Regional de cargue";
                Sheet.Cells["L1"].Value = "Fecha reporte";
                int row = 2;

                foreach (reporterips item in reporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.codhabilitacion;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.razon_social;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.muni_nombre;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.cantidad;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.registros_facturados_oportunamente;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.porcentaje_oportunidad + "%";
                    //Sheet.Cells[string.Format("G{0}", row)].Value = item.Errores_dx;
                    //Sheet.Cells[string.Format("H{0}", row)].Value = item.Errores_pc;
                    //Sheet.Cells[string.Format("I{0}", row)].Value = item.Errores_rc;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.Total_Errores;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.Registros_unicos_con_error;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.Registros_sin_error;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.porcentaje_calidad_rips + "%";
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.nombreRegional;
                    Sheet.Cells[string.Format("L{0}", row)].Value = mes + "/" + año;


                    row++;
                }

                string namefile = "Evaluacion_RIPS_SAMI_FIS_" + regionales.indice + "_" + mes + "_" + año + "_" + DateTime.Now;
                Sheet.Cells["A:AZ"].AutoFitColumns();
                Sheet.Cells["A1:L1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                Sheet.Cells["A1:L1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                Sheet.Cells["A2:L" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();


            }

            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('Problemas al generar el reporte');" +
                "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }



        public void ExportaraexcelLogCorrectos(int regional, int mes, int año)
        {
            Models.CuentasMedicas.RipsFis Model = new Models.CuentasMedicas.RipsFis();
            Ref_regional regionales = BusClass.GetRefRegion().Where(l => l.id_ref_regional == regional).FirstOrDefault();

            try
            {
                var ripsAC = Model.FisRipsCorrectos_AC(regional, mes, año, ref MsgRes);
                var ripsAF = Model.FisRipsCorrectos_AF(regional, mes, año, ref MsgRes);
                var ripsAH = Model.FisRipsCorrectos_AH(regional, mes, año, ref MsgRes);
                var ripsAM = Model.FisRipsCorrectos_AM(regional, mes, año, ref MsgRes);
                var ripsAN = Model.FisRipsCorrectos_AN(regional, mes, año, ref MsgRes);
                var ripsAP = Model.FisRipsCorrectos_AP(regional, mes, año, ref MsgRes);
                var ripsAT = Model.FisRipsCorrectos_AT(regional, mes, año, ref MsgRes);
                var ripsAU = Model.FisRipsCorrectos_AU(regional, mes, año, ref MsgRes);
                var ripsUS = Model.FisRipsCorrectos_US(regional, mes, año, ref MsgRes);


                if (!ripsAC.Any() && !ripsAF.Any() && !ripsAH.Any() && !ripsAM.Any()
                    && !ripsAN.Any() && !ripsAP.Any() && !ripsAT.Any() && !ripsAU.Any() && !ripsUS.Any())
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                                 "window.alert('No se han encontrado resultados');" +
                                 "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                string[] array = new string[9] { "AC", "AF", "AH", "AM", "AN", "AP", "AT", "AU", "US" };

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet SheetAC = Ep.Workbook.Worksheets.Add("AC");
                ExcelWorksheet SheetAF = Ep.Workbook.Worksheets.Add("AF");
                ExcelWorksheet SheetAH = Ep.Workbook.Worksheets.Add("AH");
                ExcelWorksheet SheetAM = Ep.Workbook.Worksheets.Add("AM");
                ExcelWorksheet SheetAN = Ep.Workbook.Worksheets.Add("AN");
                ExcelWorksheet SheetAP = Ep.Workbook.Worksheets.Add("AP");
                ExcelWorksheet SheetAT = Ep.Workbook.Worksheets.Add("AT");
                ExcelWorksheet SheetAU = Ep.Workbook.Worksheets.Add("AU");
                ExcelWorksheet SheetUS = Ep.Workbook.Worksheets.Add("US");
                System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(22, 54, 92);

                foreach (string archivo in array)
                {
                    switch (archivo)
                    {
                        case "AC":

                            SheetAC.Cells["A1:R1"].Style.Font.Bold = true;
                            SheetAC.Cells["A1:R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAC.Cells["A1:R1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAC.Cells["A1:R1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAC.Cells["A1"].Value = "num_factura";
                            SheetAC.Cells["B1"].Value = "codigo_prestador";
                            SheetAC.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAC.Cells["D1"].Value = "num_id_usuario";
                            SheetAC.Cells["E1"].Value = "fecha_consulta";
                            SheetAC.Cells["F1"].Value = "num_autorizacion";
                            SheetAC.Cells["G1"].Value = "cod_consulta";
                            SheetAC.Cells["H1"].Value = "finalidad_consulta";
                            SheetAC.Cells["I1"].Value = "causa_externa  ";
                            SheetAC.Cells["J1"].Value = "cod_dx_ppal";
                            SheetAC.Cells["K1"].Value = "cod_dx_rel_1";
                            SheetAC.Cells["L1"].Value = "cod_dx_rel_2";
                            SheetAC.Cells["M1"].Value = "cod_dx_rel_3";
                            SheetAC.Cells["N1"].Value = "tipo_dx_ppal";
                            SheetAC.Cells["O1"].Value = "valor_consulta";
                            SheetAC.Cells["P1"].Value = "valor_neto_a_pagar";
                            SheetAC.Cells["Q1"].Value = "Regional";
                            SheetAC.Cells["R1"].Value = "Mes y año";

                            break;

                        case "AF":

                            SheetAF.Cells["A1:J1"].Style.Font.Bold = true;
                            SheetAF.Cells["A1:J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAF.Cells["A1:J1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAF.Cells["A1:J1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAF.Cells["A1"].Value = "num_factura";
                            SheetAF.Cells["B1"].Value = "codigo_prestador";
                            SheetAF.Cells["C1"].Value = "nombre prestador";
                            SheetAF.Cells["D1"].Value = "num id_prestador";
                            SheetAF.Cells["E1"].Value = "fecha exp_factura";
                            SheetAF.Cells["F1"].Value = "valor_neto";
                            SheetAF.Cells["G1"].Value = "tipo_nota";
                            SheetAF.Cells["H1"].Value = "num_nota";
                            SheetAF.Cells["I1"].Value = "Regional";
                            SheetAF.Cells["J1"].Value = "Mes y año";

                            break;




                        case "AH":

                            SheetAH.Cells["A1:S1"].Style.Font.Bold = true;
                            SheetAH.Cells["A1:S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAH.Cells["A1:S1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAH.Cells["A1:S1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAH.Cells["A1"].Value = "num_factura";
                            SheetAH.Cells["B1"].Value = "codigo_prestador";
                            SheetAH.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAH.Cells["D1"].Value = "num_id_usuario";
                            SheetAH.Cells["E1"].Value = "via_ingreso";
                            SheetAH.Cells["F1"].Value = "fecha_ingreso";
                            SheetAH.Cells["G1"].Value = "num_autorizacion";
                            SheetAH.Cells["H1"].Value = "causa_externa";
                            SheetAH.Cells["I1"].Value = "dx_ppal_ingreso";
                            SheetAH.Cells["J1"].Value = "dx_ppal_egreso";
                            SheetAH.Cells["K1"].Value = "dx_rel_1_egreso";
                            SheetAH.Cells["L1"].Value = "dx_rel_2_egreso";
                            SheetAH.Cells["M1"].Value = "dx_rel_3_egreso";
                            SheetAH.Cells["N1"].Value = "dx_complicacion";
                            SheetAH.Cells["O1"].Value = "estado_salida";
                            SheetAH.Cells["P1"].Value = "dx_causa_basica_muerte";
                            SheetAH.Cells["Q1"].Value = "fecha_egreso";
                            SheetAH.Cells["R1"].Value = "Regional";
                            SheetAH.Cells["S1"].Value = "Mes y año";
                            break;


                        case "AM":
                            SheetAM.Cells["A1:R1"].Style.Font.Bold = true;
                            SheetAM.Cells["A1:R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAM.Cells["A1:R1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAM.Cells["A1:R1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAM.Cells["A1"].Value = "num_factura";
                            SheetAM.Cells["B1"].Value = "codigo_prestador";
                            SheetAM.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAM.Cells["D1"].Value = "num_id_usuario";
                            SheetAM.Cells["E1"].Value = "codDiagnosticoPrincipal";
                            SheetAM.Cells["F1"].Value = "codDiagnosticoRelacionado";
                            SheetAM.Cells["G1"].Value = "fechaDispensAdmon";
                            SheetAM.Cells["H1"].Value = "tipoMedicamento";
                            SheetAM.Cells["I1"].Value = "codTecnologiaSalud";
                            SheetAM.Cells["J1"].Value = "nomTecnologiaSalud";
                            SheetAM.Cells["K1"].Value = "concentracionMedicamento";
                            SheetAM.Cells["L1"].Value = "unidadMedida";
                            SheetAM.Cells["M1"].Value = "cantidadMedicamento";
                            SheetAM.Cells["N1"].Value = "diasTratamiento";
                            SheetAM.Cells["O1"].Value = "vrUnitMedicamento";
                            SheetAM.Cells["P1"].Value = "vr_total_Servicio";
                            SheetAM.Cells["Q1"].Value = "Regional";
                            SheetAM.Cells["R1"].Value = "Mes y año";
                            break;


                        case "AN":
                            SheetAN.Cells["A1:O1"].Style.Font.Bold = true;
                            SheetAN.Cells["A1:O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAN.Cells["A1:O1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAN.Cells["A1:O1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAN.Cells["A1"].Value = "num_factura";
                            SheetAN.Cells["B1"].Value = "codigo_prestador";
                            SheetAN.Cells["C1"].Value = "tipo_id_madre";
                            SheetAN.Cells["D1"].Value = "num_id_madre";
                            SheetAN.Cells["E1"].Value = "fecha_nacimiento_rn";
                            SheetAN.Cells["F1"].Value = "edad_gestacional";
                            SheetAN.Cells["G1"].Value = "control_prenatal";
                            SheetAN.Cells["H1"].Value = "sexo";
                            SheetAN.Cells["I1"].Value = "peso";
                            SheetAN.Cells["J1"].Value = "dx_recien_nacido";
                            SheetAN.Cells["K1"].Value = "causa_muerte";
                            SheetAN.Cells["L1"].Value = "fecha_egreso";
                            SheetAN.Cells["M1"].Value = "condicion_egreso";
                            SheetAN.Cells["N1"].Value = "Regional";
                            SheetAN.Cells["O1"].Value = "Mes y año";
                            break;

                        case "AP":
                            SheetAP.Cells["A1:P1"].Style.Font.Bold = true;
                            SheetAP.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAP.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAP.Cells["A1:P1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAP.Cells["A1"].Value = "num_factura";
                            SheetAP.Cells["B1"].Value = "codigo_prestador";
                            SheetAP.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAP.Cells["D1"].Value = "num_id_usuario";
                            SheetAP.Cells["E1"].Value = "fecha_procedimiento";
                            SheetAP.Cells["F1"].Value = "num_autorizacion";
                            SheetAP.Cells["G1"].Value = "cod_procedimiento";
                            SheetAP.Cells["H1"].Value = "ambito_procedimiento";
                            SheetAP.Cells["I1"].Value = "finalidad_procedimiento";
                            SheetAP.Cells["J1"].Value = "dx_ppal";
                            SheetAP.Cells["K1"].Value = "dx_rel";
                            SheetAP.Cells["L1"].Value = "complicacion";
                            SheetAP.Cells["M1"].Value = "valor_procedimiento";
                            SheetAP.Cells["N1"].Value = "vr_total_Servicio";
                            SheetAP.Cells["O1"].Value = "Regional";
                            SheetAP.Cells["P1"].Value = "Mes y año";
                            break;

                        case "AT":
                            SheetAT.Cells["A1:N1"].Style.Font.Bold = true;
                            SheetAT.Cells["A1:N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAT.Cells["A1:N1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAT.Cells["A1:N1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAT.Cells["A1"].Value = "num_factura";
                            SheetAT.Cells["B1"].Value = "codigo_prestador";
                            SheetAT.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAT.Cells["D1"].Value = "num_id_usuario";
                            SheetAT.Cells["E1"].Value = "numAutorizacion";
                            SheetAT.Cells["F1"].Value = "fechaSuministroTecnologia";
                            SheetAT.Cells["G1"].Value = "tipoOS";
                            SheetAT.Cells["H1"].Value = "codTecnologiaSalud";
                            SheetAT.Cells["I1"].Value = "nomTecnologiaSalud";
                            SheetAT.Cells["J1"].Value = "cantidadOS";
                            SheetAT.Cells["K1"].Value = "vrUnitOS";
                            SheetAT.Cells["L1"].Value = "vrServicio";
                            SheetAT.Cells["M1"].Value = "Regional";
                            SheetAT.Cells["N1"].Value = "Mes y año";

                            break;



                        case "AU":
                            SheetAU.Cells["A1:P1"].Style.Font.Bold = true;
                            SheetAU.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAU.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAU.Cells["A1:P1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAU.Cells["A1"].Value = "num_factura";
                            SheetAU.Cells["B1"].Value = "codigo_prestador";
                            SheetAU.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAU.Cells["D1"].Value = "num_id_usuario";
                            SheetAU.Cells["E1"].Value = "fechaInicioAtencion";
                            SheetAU.Cells["F1"].Value = "MotivoAtencion";
                            SheetAU.Cells["G1"].Value = "dx_ppal";
                            SheetAU.Cells["H1"].Value = "dx_ppal_Egreso";
                            SheetAU.Cells["I1"].Value = "dx_rel_Egreso1";
                            SheetAU.Cells["J1"].Value = "dx_rel_Egreso2";
                            SheetAU.Cells["K1"].Value = "dx_rel_Egreso3";
                            SheetAU.Cells["L1"].Value = "CondicionEgreso";
                            SheetAU.Cells["M1"].Value = "dx_causa_muerte";
                            SheetAU.Cells["N1"].Value = "Fecha_Egreso";
                            SheetAU.Cells["O1"].Value = "Regional";
                            SheetAU.Cells["P1"].Value = "Mes y año";

                            break;



                        case "US":
                            SheetUS.Cells["A1:L1"].Style.Font.Bold = true;
                            SheetUS.Cells["A1:L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetUS.Cells["A1:L1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetUS.Cells["A1:L1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetUS.Cells["A1"].Value = "tipo_id_usuario";
                            SheetUS.Cells["B1"].Value = "num_id_usuario";
                            SheetUS.Cells["C1"].Value = "cod_entidad_adm";
                            SheetUS.Cells["D1"].Value = "tipo_usuario";
                            SheetUS.Cells["E1"].Value = "apellidos";
                            SheetUS.Cells["F1"].Value = "nombres";
                            SheetUS.Cells["G1"].Value = "edad";
                            SheetUS.Cells["H1"].Value = "sexo";
                            SheetUS.Cells["I1"].Value = "cod_municipio_residencia";
                            SheetUS.Cells["J1"].Value = "zona_residencia";
                            SheetUS.Cells["K1"].Value = "Regional";
                            SheetUS.Cells["L1"].Value = "Mes y año";


                            break;
                        default:
                            break;
                    }
                }

                int rowac = 2, rowaf = 2, rowah = 2, rowam = 2, rowan = 2, rowap = 2, rowat = 2, rowau = 2, rowus = 2;

                for (int i = 0; i < array.Length; i++)
                {
                    switch (array[i])
                    {
                        case "AC":

                            foreach (var obj in ripsAC)
                            {
                                SheetAC.Cells["A" + rowac].Value = obj.numFactura;
                                SheetAC.Cells["B" + rowac].Value = obj.codigo_habilitacion_homologado;
                                SheetAC.Cells["C" + rowac].Value = obj.tipoDocumentoIdentificacion;
                                SheetAC.Cells["D" + rowac].Value = obj.numDocumentoIdentificacion;
                                SheetAC.Cells["E" + rowac].Value = obj.fecha_prestacion?.ToString("dd/MM/yyyy");
                                SheetAC.Cells["F" + rowac].Value = obj.numAutorizacion;
                                SheetAC.Cells["G" + rowac].Value = obj.codConsulta;
                                SheetAC.Cells["H" + rowac].Value = obj.finalidadTecnologiaSalud;
                                SheetAC.Cells["I" + rowac].Value = obj.causaMotivoAtencion;
                                SheetAC.Cells["J" + rowac].Value = obj.codDiagnosticoPrincipal;
                                SheetAC.Cells["K" + rowac].Value = obj.codDiagnosticoRelacionado1;
                                SheetAC.Cells["L" + rowac].Value = obj.codDiagnosticoRelacionado2;
                                SheetAC.Cells["M" + rowac].Value = obj.codDiagnosticoRelacionado3;
                                SheetAC.Cells["N" + rowac].Value = obj.tipoDiagnosticoPrincipal;
                                SheetAC.Cells["O" + rowac].Value = obj.vrServicio;
                                SheetAC.Cells["P" + rowac].Value = obj.vrServicio + (obj.valorPagoModerador ?? 0);
                                SheetAC.Cells["Q" + rowac].Value = obj.indice;
                                SheetAC.Cells["R" + rowac].Value = obj.mes + "/" + obj.año;
                                rowac++;
                            }
                            break;

                        case "AF":

                            foreach (var obj in ripsAF)
                            {
                                SheetAF.Cells["A" + rowaf].Value = obj.numFactura;
                                SheetAF.Cells["B" + rowaf].Value = obj.codigo_habilitacion_homologado;
                                SheetAF.Cells["C" + rowaf].Value = obj.nomPrestador;
                                SheetAF.Cells["D" + rowaf].Value = obj.numDocumentoIdObligado;
                                SheetAF.Cells["E" + rowaf].Value = obj.fecha_exp_factura?.ToString("dd/MM/yyyy");
                                SheetAF.Cells["F" + rowaf].Value = obj.valor_neto;
                                SheetAF.Cells["G" + rowaf].Value = obj.tipoNota;
                                SheetAF.Cells["H" + rowaf].Value = obj.numNota;
                                SheetAF.Cells["I" + rowaf].Value = obj.indice;
                                SheetAF.Cells["J" + rowaf].Value = obj.Mes + "/" + obj.año;
                                rowaf++;
                            }


                            break;

                        case "AH":

                            foreach (var obj in ripsAH)
                            {
                                SheetAH.Cells["A" + rowah].Value = obj.numFactura;
                                SheetAH.Cells["B" + rowah].Value = obj.codigo_habilitacion_homologado;
                                SheetAH.Cells["C" + rowah].Value = obj.tipoDocumentoIdentificacion;
                                SheetAH.Cells["D" + rowah].Value = obj.numDocumentoIdentificacion;
                                SheetAH.Cells["E" + rowah].Value = obj.viaIngresoServicioSalud;
                                SheetAH.Cells["F" + rowah].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy");
                                SheetAH.Cells["G" + rowah].Value = obj.numAutorizacion;
                                SheetAH.Cells["H" + rowah].Value = obj.causaMotivoAtencion;
                                SheetAH.Cells["I" + rowah].Value = obj.codDiagnosticoPrincipal;
                                SheetAH.Cells["J" + rowah].Value = obj.codDiagnosticoPrincipalE;
                                SheetAH.Cells["K" + rowah].Value = obj.codDiagnosticoRelacionadoE1;
                                SheetAH.Cells["L" + rowah].Value = obj.codDiagnosticoRelacionadoE2;
                                SheetAH.Cells["M" + rowah].Value = obj.codDiagnosticoRelacionadoE3;
                                SheetAH.Cells["N" + rowah].Value = obj.codComplicacion;
                                SheetAH.Cells["O" + rowah].Value = obj.condicionDestinoUsuarioEgreso;
                                SheetAH.Cells["P" + rowah].Value = obj.codDiagnosticoCausaMuerte;
                                SheetAH.Cells["Q" + rowah].Value = obj.fechaEgreso?.ToString("dd/MM/yyyy");
                                SheetAH.Cells["R" + rowah].Value = obj.indice;
                                SheetAH.Cells["S" + rowah].Value = obj.mes + "/" + obj.año;
                                rowah++;
                            }

                            break;


                        case "AM":
                            foreach (var obj in ripsAM)
                            {
                                SheetAM.Cells["A" + rowam].Value = obj.numFactura;
                                SheetAM.Cells["B" + rowam].Value = obj.codigo_habilitacion_homologado;
                                SheetAM.Cells["C" + rowam].Value = obj.tipoDocumentoIdentificacion;
                                SheetAM.Cells["D" + rowam].Value = obj.numDocumentoIdentificacion;
                                SheetAM.Cells["E" + rowam].Value = obj.codDiagnosticoPrincipal;
                                SheetAM.Cells["F" + rowam].Value = obj.codDiagnosticoRelacionado;
                                SheetAM.Cells["G" + rowam].Value = obj.fechaDispensAdmon?.ToString("dd/MM/yyyy");
                                SheetAM.Cells["H" + rowam].Value = obj.tipoMedicamento;
                                SheetAM.Cells["I" + rowam].Value = obj.codTecnologiaSalud;
                                SheetAM.Cells["J" + rowam].Value = obj.nomTecnologiaSalud;
                                SheetAM.Cells["K" + rowam].Value = obj.concentracionMedicamento;
                                SheetAM.Cells["L" + rowam].Value = obj.unidadMedida;
                                SheetAM.Cells["M" + rowam].Value = obj.cantidadMedicamento;
                                SheetAM.Cells["N" + rowam].Value = obj.diasTratamiento;
                                SheetAM.Cells["O" + rowam].Value = obj.vrUnitMedicamento;
                                SheetAM.Cells["P" + rowam].Value = obj.vrServicio + (obj.valorPagoModerador ?? 0);
                                SheetAM.Cells["Q" + rowam].Value = obj.indice;
                                SheetAM.Cells["R" + rowam].Value = obj.mes + "/" + obj.año;

                                rowam++;
                            }
                            break;

                        case "AN":

                            foreach (var obj in ripsAN)
                            {
                                SheetAN.Cells["A" + rowan].Value = obj.numFactura;
                                SheetAN.Cells["B" + rowan].Value = obj.codigo_habilitacion_homologado;
                                SheetAN.Cells["C" + rowan].Value = obj.tipoDocumentoIdentificacionmadre;
                                SheetAN.Cells["D" + rowan].Value = obj.numDocumentoIdentificacionmadre;
                                SheetAN.Cells["E" + rowan].Value = obj.fechaNacimiento?.ToString("dd/MM/yyyy");
                                SheetAN.Cells["F" + rowan].Value = obj.edadGestacional;
                                SheetAN.Cells["G" + rowan].Value = obj.numConsultasCPrenatal;
                                SheetAN.Cells["H" + rowan].Value = obj.codSexoBiologico;
                                SheetAN.Cells["I" + rowan].Value = obj.peso;
                                SheetAN.Cells["J" + rowan].Value = obj.codDiagnosticoPrincipal;
                                SheetAN.Cells["K" + rowan].Value = obj.codDiagnosticoCausaMuerte;
                                SheetAN.Cells["L" + rowan].Value = obj.fechaEgreso?.ToString("dd/MM/yyyy");
                                SheetAN.Cells["M" + rowan].Value = obj.condicionDestinoUsuarioEgreso;
                                SheetAN.Cells["N" + rowan].Value = obj.indice;
                                SheetAN.Cells["O" + rowan].Value = obj.mes + "/" + obj.año;
                                rowan++;
                            }
                            break;

                        case "AP":

                            foreach (var obj in ripsAP)
                            {
                                SheetAP.Cells["A" + rowap].Value = obj.numFactura;
                                SheetAP.Cells["B" + rowap].Value = obj.codigo_habilitacion_homologado;
                                SheetAP.Cells["C" + rowap].Value = obj.tipoDocumentoIdentificacion;
                                SheetAP.Cells["D" + rowap].Value = obj.numDocumentoIdentificacion;
                                SheetAP.Cells["E" + rowap].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy");
                                SheetAP.Cells["F" + rowap].Value = obj.numAutorizacion;
                                SheetAP.Cells["G" + rowap].Value = obj.codProcedimiento;
                                SheetAP.Cells["H" + rowap].Value = obj.viaIngresoServicioSalud;
                                SheetAP.Cells["I" + rowap].Value = obj.finalidadTecnologiaSalud;
                                SheetAP.Cells["J" + rowap].Value = obj.codDiagnosticoPrincipal;
                                SheetAP.Cells["K" + rowap].Value = obj.codDiagnosticoRelacionado;
                                SheetAP.Cells["L" + rowap].Value = obj.codComplicacion;
                                SheetAP.Cells["M" + rowap].Value = obj.vrServicio;
                                SheetAP.Cells["N" + rowap].Value = obj.vrServicio + (obj.valorPagoModerador ?? 0);
                                SheetAP.Cells["O" + rowap].Value = obj.indice;
                                SheetAP.Cells["P" + rowap].Value = obj.mes + "/" + obj.año;
                                rowap++;
                            }
                            break;

                        case "AT":

                            foreach (var obj in ripsAT)
                            {
                                SheetAT.Cells["A" + rowat].Value = obj.numFactura;
                                SheetAT.Cells["B" + rowat].Value = obj.codigo_habilitacion_homologado;
                                SheetAT.Cells["C" + rowat].Value = obj.tipoDocumentoIdentificacion;
                                SheetAT.Cells["D" + rowat].Value = obj.numDocumentoIdentificacion;
                                SheetAT.Cells["E" + rowat].Value = obj.numAutorizacion;
                                SheetAT.Cells["F" + rowat].Value = obj.fechaSuministroTecnologia?.ToString("dd/MM/yyyy");
                                SheetAT.Cells["G" + rowat].Value = obj.tipoOS;
                                SheetAT.Cells["H" + rowat].Value = obj.codTecnologiaSalud;
                                SheetAT.Cells["I" + rowat].Value = obj.nomTecnologiaSalud;
                                SheetAT.Cells["J" + rowat].Value = obj.cantidadOS;
                                SheetAT.Cells["K" + rowat].Value = obj.vrUnitOS;
                                SheetAT.Cells["L" + rowat].Value = obj.vrServicio + (obj.valorPagoModerador ?? 0);
                                SheetAT.Cells["M" + rowat].Value = obj.indice;
                                SheetAT.Cells["N" + rowat].Value = obj.mes + "/" + obj.año;

                                rowat++;
                            }
                            break;


                        case "AU":

                            foreach (var obj in ripsAU)
                            {
                                SheetAU.Cells["A" + rowau].Value = obj.numFactura;
                                SheetAU.Cells["B" + rowau].Value = obj.codigo_habilitacion_homologado;
                                SheetAU.Cells["C" + rowau].Value = obj.tipoDocumentoIdentificacion;
                                SheetAU.Cells["D" + rowau].Value = obj.numDocumentoIdentificacion;
                                SheetAU.Cells["E" + rowau].Value = obj.causaMotivoAtencion;
                                SheetAU.Cells["F" + rowau].Value = obj.codDiagnosticoPrincipal;
                                SheetAU.Cells["G" + rowau].Value = obj.codDiagnosticoPrincipalE;
                                SheetAU.Cells["H" + rowau].Value = obj.codDiagnosticoRelacionadoE1;
                                SheetAU.Cells["I" + rowau].Value = obj.codDiagnosticoRelacionadoE2;
                                SheetAU.Cells["J" + rowau].Value = obj.codDiagnosticoRelacionadoE3;
                                SheetAU.Cells["K" + rowau].Value = obj.condicionDestinoUsuarioEgreso;
                                SheetAU.Cells["L" + rowau].Value = obj.codDiagnosticoCausaMuerte;
                                SheetAU.Cells["M" + rowau].Value = obj.fechaEgreso?.ToString("dd/MM/yyyy");
                                SheetAU.Cells["N" + rowau].Value = obj.indice;
                                SheetAU.Cells["P" + rowau].Value = obj.mes + "/" + obj.año;

                                rowat++;
                            }
                            break;

                        case "US":

                            foreach (var obj in ripsUS)
                            {
                                SheetUS.Cells["A" + rowus].Value = obj.tipoDocumentoIdentificacion;
                                SheetUS.Cells["B" + rowus].Value = obj.numDocumentoIdentificacion;
                                SheetUS.Cells["C" + rowus].Value = "RES002";
                                SheetUS.Cells["D" + rowus].Value = obj.tipoUsuario;
                                SheetUS.Cells["E" + rowus].Value = obj.Apellidos;
                                SheetUS.Cells["F" + rowus].Value = obj.Nombre;
                                SheetUS.Cells["G" + rowus].Value = obj.edad;
                                SheetUS.Cells["H" + rowus].Value = obj.codSexo;
                                SheetUS.Cells["J" + rowus].Value = obj.codMunicipioResidencia;
                                SheetUS.Cells["K" + rowus].Value = obj.codZonaTerritorialResidencia;
                                SheetUS.Cells["L" + rowus].Value = obj.indice;
                                SheetUS.Cells["M" + rowus].Value = obj.mes + "/" + obj.año;


                                rowus++;
                            }
                            break;

                            #endregion
                    }
                }

                //}

                string namefile = "Reporte_Correctos_SAMI_FIS_" + regionales.indice + "_" + mes + "_" + año;
                SheetAC.Cells["A:R"].AutoFitColumns();
                SheetAF.Cells["A:Q"].AutoFitColumns();
                SheetAH.Cells["A:P"].AutoFitColumns();
                SheetAM.Cells["A:S"].AutoFitColumns();
                SheetAN.Cells["A:U"].AutoFitColumns();
                SheetAP.Cells["A:S"].AutoFitColumns();
                SheetAT.Cells["A:P"].AutoFitColumns();
                SheetUS.Cells["A:P"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('Problemas al generar el reporte');" +
                "</script> ";
                Response.Write(rta);
                Response.End();
            }

        }



        public void ExportaraexcelLogErrores(int regional, int mes, int año)
        {
            Models.CuentasMedicas.RipsFis Model = new Models.CuentasMedicas.RipsFis();
            Ref_regional regionales = BusClass.GetRefRegion().Where(l => l.id_ref_regional == regional).FirstOrDefault();

            try
            {
                var ripsAC = Model.FisRipsErrores_AC(regional, mes, año, ref MsgRes);
                var ripsAH = Model.FisRipsErrores_AH(regional, mes, año, ref MsgRes);
                var ripsAM = Model.FisRipsErrores_AM(regional, mes, año, ref MsgRes);
                var ripsAN = Model.FisRipsErrores_AN(regional, mes, año, ref MsgRes);
                var ripsAP = Model.FisRipsErrores_AP(regional, mes, año, ref MsgRes);
                var ripsAT = Model.FisRipsErrores_AT(regional, mes, año, ref MsgRes);
                var ripsAU = Model.FisRipsErrores_AU(regional, mes, año, ref MsgRes);
                var ripsUS = Model.FisRipsErrores_US(regional, mes, año, ref MsgRes);


                if (!ripsAC.Any() && !ripsAH.Any() && !ripsAM.Any()
                    && !ripsAN.Any() && !ripsAP.Any() && !ripsAT.Any() && !ripsAU.Any() && !ripsUS.Any())
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                                 "window.alert('No se han encontrado resultados');" +
                                 "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                //string[] array = new string[5] { "AC", "AP", "AN", "AU", "AH" };
                string[] array = new string[8] { "AC", "AH", "AM", "AN", "AP", "AT", "AU", "US" };

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet SheetAC = Ep.Workbook.Worksheets.Add("AC");
                //ExcelWorksheet SheetAF = Ep.Workbook.Worksheets.Add("AF");
                ExcelWorksheet SheetAH = Ep.Workbook.Worksheets.Add("AH");
                ExcelWorksheet SheetAM = Ep.Workbook.Worksheets.Add("AM");
                ExcelWorksheet SheetAN = Ep.Workbook.Worksheets.Add("AN");
                ExcelWorksheet SheetAP = Ep.Workbook.Worksheets.Add("AP");
                ExcelWorksheet SheetAT = Ep.Workbook.Worksheets.Add("AT");
                ExcelWorksheet SheetAU = Ep.Workbook.Worksheets.Add("AU");
                ExcelWorksheet SheetUS = Ep.Workbook.Worksheets.Add("US");
                System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(22, 54, 92);

                foreach (string archivo in array)
                {
                    switch (archivo)
                    {
                        case "AC":

                            SheetAC.Cells["A1:AA1"].Style.Font.Bold = true;
                            SheetAC.Cells["A1:AA1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAC.Cells["A1:AA1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAC.Cells["A1:AA1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAC.Cells["A1"].Value = "CodPrestador";
                            SheetAC.Cells["B1"].Value = "num_factura";
                            SheetAC.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAC.Cells["D1"].Value = "num_id_usuario";
                            SheetAC.Cells["E1"].Value = "fechalnicioAtencion";
                            SheetAC.Cells["F1"].Value = "numAutorizacion";
                            SheetAC.Cells["G1"].Value = "codConsulta";
                            SheetAC.Cells["H1"].Value = "modalidadGrupoServicioTecSal";
                            SheetAC.Cells["I1"].Value = "grupoServicios";
                            SheetAC.Cells["J1"].Value = "codServicio";
                            SheetAC.Cells["K1"].Value = "finalidadTecnologiaSalud";
                            SheetAC.Cells["L1"].Value = "causaMotivoAtencion";
                            SheetAC.Cells["M1"].Value = "codDiagnosticoPrincipal";
                            SheetAC.Cells["N1"].Value = "codDiagnosticoRelacionado1";
                            SheetAC.Cells["O1"].Value = "codDiagnosticoRelacionado2";
                            SheetAC.Cells["P1"].Value = "codDiagnosticoRelacionado3";
                            SheetAC.Cells["Q1"].Value = "tipoDiagnosticoPrincipal";
                            SheetAC.Cells["R1"].Value = "tipoDocumentoIdentificacion";
                            SheetAC.Cells["S1"].Value = "numDocumentoIdentificacion";
                            SheetAC.Cells["T1"].Value = "vrServicio";
                            SheetAC.Cells["U1"].Value = "conceptoRecaudo";
                            SheetAC.Cells["V1"].Value = "valorPagoModerador";
                            SheetAC.Cells["W1"].Value = "numFEVPagoModerador";
                            SheetAC.Cells["X1"].Value = "consecutivo";
                            SheetAC.Cells["Y1"].Value = "Detalle Log";
                            SheetAC.Cells["Z1"].Value = "Regional";
                            SheetAC.Cells["AA1"].Value = "Mes y año reporte";

                            break;


                        case "AH":

                            SheetAH.Cells["A1:U1"].Style.Font.Bold = true;
                            SheetAH.Cells["A1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAH.Cells["A1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAH.Cells["A1:U1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAH.Cells["A1"].Value = "codPrestador";
                            SheetAH.Cells["B1"].Value = "num_factura";
                            SheetAH.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAH.Cells["D1"].Value = "num_id_usuario";
                            SheetAH.Cells["E1"].Value = "viaIngresoServicioSalud";
                            SheetAH.Cells["F1"].Value = "fechaInicioAtencion";
                            SheetAH.Cells["G1"].Value = "numAutorizacion";
                            SheetAH.Cells["H1"].Value = "causaMotivoAtencion";
                            SheetAH.Cells["I1"].Value = "codDiagnosticoPrincipal";
                            SheetAH.Cells["J1"].Value = "codDiagnosticoPrincipalE";
                            SheetAH.Cells["K1"].Value = "codDiagnosticoRelacionadoE1";
                            SheetAH.Cells["L1"].Value = "codDiagnosticoRelacionadoE2";
                            SheetAH.Cells["M1"].Value = "codDiagnosticoRelacionadoE3";
                            SheetAH.Cells["N1"].Value = "codComplicacion";
                            SheetAH.Cells["O1"].Value = "condicionDestinoUsuarioEgreso";
                            SheetAH.Cells["P1"].Value = "codDiagnosticoCausaMuerte";
                            SheetAH.Cells["Q1"].Value = "fechaEgreso";
                            SheetAH.Cells["R1"].Value = "consecutivo";
                            SheetAH.Cells["S1"].Value = "Detalle Log";
                            SheetAH.Cells["T1"].Value = "Regional";
                            SheetAH.Cells["U1"].Value = "Mes y año reporte";

                            break;



                        case "AM":

                            SheetAM.Cells["A1:AE1"].Style.Font.Bold = true;
                            SheetAM.Cells["A1:AE1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAM.Cells["A1:AE1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAM.Cells["A1:AE1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAM.Cells["A1"].Value = "codPrestador";
                            SheetAM.Cells["B1"].Value = "num_factura";
                            SheetAM.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAM.Cells["D1"].Value = "num_id_usuario";
                            SheetAM.Cells["E1"].Value = "numAutorizacion";
                            SheetAM.Cells["F1"].Value = "idMIPRES";
                            SheetAM.Cells["G1"].Value = "fechaDispensAdmon";
                            SheetAM.Cells["H1"].Value = "codDiagnosticoPrincipal";
                            SheetAM.Cells["I1"].Value = "codDiagnosticoRelacionado";
                            SheetAM.Cells["J1"].Value = "tipoMedicamento";
                            SheetAM.Cells["K1"].Value = "codTecnologiaSalud";
                            SheetAM.Cells["L1"].Value = "nomTecnologiaSalud";
                            SheetAM.Cells["M1"].Value = "concentracionMedicamento";
                            SheetAM.Cells["N1"].Value = "unidadMedida";
                            SheetAM.Cells["O1"].Value = "formaFarmaceutica";
                            SheetAM.Cells["P1"].Value = "unidadMinDispensa";
                            SheetAM.Cells["Q1"].Value = "cantidadMedicamento";
                            SheetAM.Cells["R1"].Value = "diasTratamiento";
                            SheetAM.Cells["S1"].Value = "tipoDocumentoIdentificacion";
                            SheetAM.Cells["T1"].Value = "numDocumentoIdentificacion";
                            SheetAM.Cells["U1"].Value = "vrUnitMedicamento";
                            SheetAM.Cells["V1"].Value = "vrServicio";
                            SheetAM.Cells["W1"].Value = "conceptoRecaudo";
                            SheetAM.Cells["X1"].Value = "valorPagoModerador";
                            SheetAM.Cells["Y1"].Value = "numFEVPagoModerador";
                            SheetAM.Cells["Z1"].Value = "consecutivo";
                            SheetAM.Cells["AA1"].Value = "Detalle Log";
                            SheetAM.Cells["AB1"].Value = "Regional";
                            SheetAM.Cells["AC1"].Value = "Mes y año reporte";

                            break;



                        case "AN":

                            SheetAN.Cells["A1:P1"].Style.Font.Bold = true;
                            SheetAN.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAN.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAN.Cells["A1:P1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAN.Cells["A1"].Value = "codPrestador";
                            SheetAN.Cells["B1"].Value = "num_factura";
                            SheetAN.Cells["C1"].Value = "tipoDocumentoIdentificacion";
                            SheetAN.Cells["D1"].Value = "numDocumentoIdentificacion";
                            SheetAN.Cells["E1"].Value = "fechaNacimiento";
                            SheetAN.Cells["F1"].Value = "edadGestacional";
                            SheetAN.Cells["G1"].Value = "numConsultasCPrenatal";
                            SheetAN.Cells["H1"].Value = "codSexoBiologico";
                            SheetAN.Cells["I1"].Value = "peso";
                            SheetAN.Cells["J1"].Value = "codDiagnosticoPrincipal";
                            SheetAN.Cells["K1"].Value = "condicionDestinoUsuarioEgreso";
                            SheetAN.Cells["L1"].Value = "codDiagnosticoCausaMuerte";
                            SheetAN.Cells["M1"].Value = "fechaEgreso";
                            SheetAN.Cells["N1"].Value = "consecutivo";
                            SheetAN.Cells["O1"].Value = "Detalle Log";
                            SheetAN.Cells["P1"].Value = "Regional";
                            SheetAN.Cells["Q1"].Value = "Mes y año reporte";

                            break;

                        case "AP":
                            SheetAP.Cells["A1:Z1"].Style.Font.Bold = true;
                            SheetAP.Cells["A1:Z1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAP.Cells["A1:Z1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAP.Cells["A1:Z1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAP.Cells["A1"].Value = "codPrestador";
                            SheetAP.Cells["B1"].Value = "num_factura";
                            SheetAP.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAP.Cells["D1"].Value = "num_id_usuario";
                            SheetAP.Cells["E1"].Value = "fechaInicioAtencion";
                            SheetAP.Cells["F1"].Value = "idMIPRES";
                            SheetAP.Cells["G1"].Value = "numAutorizacion";
                            SheetAP.Cells["H1"].Value = "codProcedimiento";
                            SheetAP.Cells["I1"].Value = "viaIngresoServicioSalud";
                            SheetAP.Cells["J1"].Value = "modalidadGrupoServicioTecSal";
                            SheetAP.Cells["K1"].Value = "grupoServicios";
                            SheetAP.Cells["L1"].Value = "codServicio";
                            SheetAP.Cells["M1"].Value = "finalidadTecnologiaSalud";
                            SheetAP.Cells["N1"].Value = "tipoDocumentoIdentificacion";
                            SheetAP.Cells["O1"].Value = "numDocumentoIdentificacion";
                            SheetAP.Cells["P1"].Value = "codDiagnosticoPrincipal";
                            SheetAP.Cells["Q1"].Value = "codDiagnosticoRelacionado";
                            SheetAP.Cells["R1"].Value = "codComplicacion";
                            SheetAP.Cells["S1"].Value = "vrServicio";
                            SheetAP.Cells["T1"].Value = "conceptoRecaudo";
                            SheetAP.Cells["U1"].Value = "valorPagoModerador";
                            SheetAP.Cells["V1"].Value = "numFEVPagoModerador";
                            SheetAP.Cells["W1"].Value = "consecutivo";
                            SheetAP.Cells["X1"].Value = "Detalle Log";
                            SheetAP.Cells["Y1"].Value = "Regional";
                            SheetAP.Cells["Z1"].Value = "Mes y año reporte";

                            break;



                        case "AT":
                            SheetAT.Cells["A1:V1"].Style.Font.Bold = true;
                            SheetAT.Cells["A1:V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAT.Cells["A1:V1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAT.Cells["A1:V1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAT.Cells["A1"].Value = "codPrestador";
                            SheetAT.Cells["B1"].Value = "num_factura";
                            SheetAT.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAT.Cells["D1"].Value = "num_id_usuario";
                            SheetAT.Cells["E1"].Value = "numAutorizacion";
                            SheetAT.Cells["F1"].Value = "idMIPRES";
                            SheetAT.Cells["G1"].Value = "fechaSuministroTecnologia";
                            SheetAT.Cells["H1"].Value = "tipoOS";
                            SheetAT.Cells["I1"].Value = "codTecnologiaSalud";
                            SheetAT.Cells["J1"].Value = "nomTecnologiaSalud";
                            SheetAT.Cells["K1"].Value = "cantidadOS";
                            SheetAT.Cells["L1"].Value = "tipoDocumentoIdentificacion";
                            SheetAT.Cells["M1"].Value = "numDocumentoIdentificacion";
                            SheetAT.Cells["N1"].Value = "vrUnitOS";
                            SheetAT.Cells["O1"].Value = "vrServicio";
                            SheetAT.Cells["P1"].Value = "conceptoRecaudo";
                            SheetAT.Cells["Q1"].Value = "valorPagoModerador";
                            SheetAT.Cells["R1"].Value = "numFEVPagoModerador";
                            SheetAT.Cells["S1"].Value = "consecutivo";
                            SheetAT.Cells["T1"].Value = "Detalle Log";
                            SheetAT.Cells["U1"].Value = "Regional";
                            SheetAT.Cells["V1"].Value = "Mes y año reporte";

                            break;

                        case "AU":
                            SheetAU.Cells["A1:R1"].Style.Font.Bold = true;
                            SheetAU.Cells["A1:R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAU.Cells["A1:R1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAU.Cells["A1:R1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAU.Cells["A1"].Value = "codPrestador";
                            SheetAU.Cells["B1"].Value = "num_factura";
                            SheetAU.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAU.Cells["D1"].Value = "num_id_usuario";
                            SheetAU.Cells["E1"].Value = "fechaInicioAtencion";
                            SheetAU.Cells["F1"].Value = "causaMotivoAtencion";
                            SheetAU.Cells["G1"].Value = "codDiagnosticoPrincipal";
                            SheetAU.Cells["H1"].Value = "codDiagnosticoPrincipalE";
                            SheetAU.Cells["I1"].Value = "codDiagnosticoRelacionadoE1";
                            SheetAU.Cells["J1"].Value = "codDiagnosticoRelacionadoE2";
                            SheetAU.Cells["K1"].Value = "codDiagnosticoRelacionadoE3";
                            SheetAU.Cells["L1"].Value = "condicionDestinoUsuarioEgreso";
                            SheetAU.Cells["M1"].Value = "codDiagnosticoCausaMuerte";
                            SheetAU.Cells["N1"].Value = "fechaEgreso";
                            SheetAU.Cells["O1"].Value = "consecutivo";
                            SheetAU.Cells["P1"].Value = "Detalle Log";
                            SheetAU.Cells["Q1"].Value = "Regional";
                            SheetAU.Cells["R1"].Value = "Mes y año reporte";

                            break;



                        case "US":
                            SheetUS.Cells["A1:N1"].Style.Font.Bold = true;
                            SheetUS.Cells["A1:N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetUS.Cells["A1:N1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetUS.Cells["A1:N1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetUS.Cells["A1"].Value = "TipoDocumentoIdentificacion";
                            SheetUS.Cells["B1"].Value = "NumDocumentoIdentificacion";
                            SheetUS.Cells["C1"].Value = "tipoUsuario";
                            SheetUS.Cells["D1"].Value = "fechaNacimiento";
                            SheetUS.Cells["E1"].Value = "codSexo";
                            SheetUS.Cells["F1"].Value = "codPaisResidencia";
                            SheetUS.Cells["G1"].Value = "codMunicipioResidencia";
                            SheetUS.Cells["H1"].Value = "codZonaTerritorialResidencia";
                            SheetUS.Cells["I1"].Value = "incapacidad";
                            SheetUS.Cells["J1"].Value = "consecutivo";
                            SheetUS.Cells["K1"].Value = "codPaisOrigen";
                            SheetUS.Cells["L1"].Value = "Detalle Log";
                            SheetUS.Cells["M1"].Value = "Regional";
                            SheetUS.Cells["N1"].Value = "Mes y año reporte";

                            break;



                        default:
                            break;
                    }
                }

                int rowac = 2, rowaf = 2, rowah = 2, rowam = 2, rowan = 2, rowap = 2, rowat = 2, rowau = 2, rowus = 2;

                for (int i = 0; i < array.Length; i++)
                {
                    switch (array[i])
                    {
                        case "AC":
                            foreach (var obj in ripsAC)
                            {
                                SheetAC.Cells["A" + rowac].Value = obj.codigo_habilitacion_homologado; // codPrestador
                                SheetAC.Cells["B" + rowac].Value = obj.numFactura;
                                SheetAC.Cells["C" + rowac].Value = obj.tipoDocumentoIdentificacion;
                                SheetAC.Cells["D" + rowac].Value = obj.numDocumentoIdentificacion;
                                SheetAC.Cells["E" + rowac].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy");
                                SheetAC.Cells["F" + rowac].Value = obj.numAutorizacion;
                                SheetAC.Cells["G" + rowac].Value = obj.codConsulta;
                                SheetAC.Cells["H" + rowac].Value = obj.modalidadGrupoServicioTecSal;
                                SheetAC.Cells["I" + rowac].Value = obj.grupoServicios;
                                SheetAC.Cells["J" + rowac].Value = obj.codServicio;
                                SheetAC.Cells["K" + rowac].Value = obj.finalidadTecnologiaSalud;
                                SheetAC.Cells["L" + rowac].Value = obj.causaMotivoAtencion;
                                SheetAC.Cells["M" + rowac].Value = obj.codDiagnosticoPrincipal;
                                SheetAC.Cells["N" + rowac].Value = obj.codDiagnosticoRelacionado1;
                                SheetAC.Cells["O" + rowac].Value = obj.codDiagnosticoRelacionado2;
                                SheetAC.Cells["P" + rowac].Value = obj.codDiagnosticoRelacionado3;
                                SheetAC.Cells["Q" + rowac].Value = obj.tipoDiagnosticoPrincipal;
                                SheetAC.Cells["R" + rowac].Value = obj.tipoDocumentoIdentificacion;
                                SheetAC.Cells["S" + rowac].Value = obj.numDocumentoIdentificacion;
                                SheetAC.Cells["T" + rowac].Value = obj.vrServicio;
                                SheetAC.Cells["U" + rowac].Value = obj.conceptoRecaudo;
                                SheetAC.Cells["V" + rowac].Value = obj.valorPagoModerador;
                                SheetAC.Cells["W" + rowac].Value = obj.numFEVPagoModerador;
                                SheetAC.Cells["X" + rowac].Value = obj.consecutivo;
                                SheetAC.Cells["Y" + rowac].Value = obj.mensaje; // Detalle Log
                                SheetAC.Cells["Z" + rowac].Value = obj.indice;
                                SheetAC.Cells["AA" + rowac].Value = obj.mes + "/" + obj.año; // Mes y año reporte
                                rowac++;
                            }
                            break;



                        case "AH":

                            foreach (var obj in ripsAH)
                            {
                                SheetAH.Cells["A" + rowah].Value = obj.codigo_habilitacion_homologado;   // codPrestador
                                SheetAH.Cells["B" + rowah].Value = obj.numFactura;                        // num_factura
                                SheetAH.Cells["C" + rowah].Value = obj.tipoDocumentoIdentificacion;       // tipo_id_usuario
                                SheetAH.Cells["D" + rowah].Value = obj.numDocumentoIdentificacion;        // num_id_usuario
                                SheetAH.Cells["E" + rowah].Value = obj.viaIngresoServicioSalud;
                                SheetAH.Cells["F" + rowah].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy");
                                SheetAH.Cells["G" + rowah].Value = obj.numAutorizacion;
                                SheetAH.Cells["H" + rowah].Value = obj.causaMotivoAtencion;
                                SheetAH.Cells["I" + rowah].Value = obj.codDiagnosticoPrincipal;
                                SheetAH.Cells["J" + rowah].Value = obj.codDiagnosticoPrincipalE;
                                SheetAH.Cells["K" + rowah].Value = obj.codDiagnosticoRelacionadoE1;
                                SheetAH.Cells["L" + rowah].Value = obj.codDiagnosticoRelacionadoE2;
                                SheetAH.Cells["M" + rowah].Value = obj.codDiagnosticoRelacionadoE3;
                                SheetAH.Cells["N" + rowah].Value = obj.codComplicacion;
                                SheetAH.Cells["O" + rowah].Value = obj.condicionDestinoUsuarioEgreso;
                                SheetAH.Cells["P" + rowah].Value = obj.codDiagnosticoCausaMuerte;
                                SheetAH.Cells["Q" + rowah].Value = obj.fechaEgreso?.ToString("dd/MM/yyyy");
                                SheetAH.Cells["R" + rowah].Value = obj.consecutivo;                            // consecutivo
                                SheetAH.Cells["S" + rowah].Value = obj.mensaje;                           // Detalle Log
                                SheetAH.Cells["T" + rowah].Value = obj.indice;                          // Regional
                                SheetAH.Cells["U" + rowah].Value = obj.mes + "/" + obj.año;              // Mes y año reporte
                                rowah++;
                            }

                            break;

                        // ==================== AM ====================
                        case "AM":
                            foreach (var obj in ripsAM)
                            {
                                SheetAM.Cells["A" + rowam].Value = obj.codigo_habilitacion_homologado;   // codPrestador
                                SheetAM.Cells["B" + rowam].Value = obj.numFactura;                       // num_factura
                                SheetAM.Cells["C" + rowam].Value = obj.tipoDocumentoIdentificacion;      // tipo_id_usuario
                                SheetAM.Cells["D" + rowam].Value = obj.numDocumentoIdentificacion;       // num_id_usuario
                                SheetAM.Cells["E" + rowam].Value = obj.numAutorizacion;
                                SheetAM.Cells["F" + rowam].Value = obj.idMIPRES;
                                SheetAM.Cells["G" + rowam].Value = obj.fechaDispensAdmon?.ToString("dd/MM/yyyy");
                                SheetAM.Cells["H" + rowam].Value = obj.codDiagnosticoPrincipal;
                                SheetAM.Cells["I" + rowam].Value = obj.codDiagnosticoRelacionado;
                                SheetAM.Cells["J" + rowam].Value = obj.tipoMedicamento;
                                SheetAM.Cells["K" + rowam].Value = obj.codTecnologiaSalud;
                                SheetAM.Cells["L" + rowam].Value = obj.nomTecnologiaSalud;
                                SheetAM.Cells["M" + rowam].Value = obj.concentracionMedicamento;
                                SheetAM.Cells["N" + rowam].Value = obj.unidadMedida;
                                SheetAM.Cells["O" + rowam].Value = obj.formaFarmaceutica;
                                SheetAM.Cells["P" + rowam].Value = obj.unidadMinDispensa;
                                SheetAM.Cells["Q" + rowam].Value = obj.cantidadMedicamento;
                                SheetAM.Cells["R" + rowam].Value = obj.diasTratamiento;
                                SheetAM.Cells["S" + rowam].Value = obj.vrUnitMedicamento;
                                SheetAM.Cells["T" + rowam].Value = obj.vrServicio;
                                SheetAM.Cells["U" + rowam].Value = obj.conceptoRecaudo;
                                SheetAM.Cells["V" + rowam].Value = obj.valorPagoModerador;
                                SheetAM.Cells["W" + rowam].Value = obj.numFEVPagoModerador;
                                SheetAM.Cells["X" + rowam].Value = obj.consecutivo;                           // consecutivo
                                SheetAM.Cells["Y" + rowam].Value = obj.mensaje;                          // Detalle Log
                                SheetAM.Cells["Z" + rowam].Value = obj.indice;
                                SheetAM.Cells["AA" + rowam].Value = obj.mes + "/" + obj.año;             // Mes y año reporte
                                rowam++;
                            }
                            break;


                        // ==================== AN ====================
                        case "AN":
                            foreach (var obj in ripsAN)
                            {
                                SheetAN.Cells["A" + rowan].Value = obj.codigo_habilitacion_homologado;   // codPrestador
                                SheetAN.Cells["B" + rowan].Value = obj.numFactura;
                                SheetAN.Cells["C" + rowan].Value = obj.tipoDocumentoIdentificacion;
                                SheetAN.Cells["D" + rowan].Value = obj.numDocumentoIdentificacion;
                                SheetAN.Cells["E" + rowan].Value = obj.fechaNacimiento;
                                SheetAN.Cells["F" + rowan].Value = obj.edadGestacional;
                                SheetAN.Cells["G" + rowan].Value = obj.numConsultasCPrenatal;
                                SheetAN.Cells["H" + rowan].Value = obj.codSexoBiologico;
                                SheetAN.Cells["I" + rowan].Value = obj.peso;
                                SheetAN.Cells["J" + rowan].Value = obj.codDiagnosticoPrincipal;
                                SheetAN.Cells["K" + rowan].Value = obj.condicionDestinoUsuarioEgreso;
                                SheetAN.Cells["L" + rowan].Value = obj.codDiagnosticoCausaMuerte;
                                SheetAN.Cells["M" + rowan].Value = obj.fechaEgreso;
                                SheetAN.Cells["N" + rowan].Value = obj.consecutivo;
                                SheetAN.Cells["O" + rowan].Value = obj.mensaje;
                                SheetAN.Cells["P" + rowan].Value = obj.indice;                           // consecutivo
                                SheetAN.Cells["Q" + rowan].Value = obj.mes + "/" + obj.año;                           // Detalle Log

                                rowan++;
                            }
                            break;


                        // ==================== AP ====================
                        case "AP":
                            foreach (var obj in ripsAP)
                            {
                                SheetAP.Cells["A" + rowap].Value = obj.codigo_habilitacion_homologado;
                                SheetAP.Cells["B" + rowap].Value = obj.numFactura;
                                SheetAP.Cells["C" + rowap].Value = obj.tipoDocumentoIdentificacion;
                                SheetAP.Cells["D" + rowap].Value = obj.numDocumentoIdentificacion;
                                SheetAP.Cells["E" + rowap].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy"); ;
                                SheetAP.Cells["F" + rowap].Value = obj.idMIPRES;
                                SheetAP.Cells["G" + rowap].Value = obj.numAutorizacion;
                                SheetAP.Cells["H" + rowap].Value = obj.codProcedimiento;
                                SheetAP.Cells["I" + rowap].Value = obj.viaIngresoServicioSalud;
                                SheetAP.Cells["J" + rowap].Value = obj.modalidadGrupoServicioTecSal;
                                SheetAP.Cells["K" + rowap].Value = obj.grupoServicios;
                                SheetAP.Cells["L" + rowap].Value = obj.codServicio;
                                SheetAP.Cells["M" + rowap].Value = obj.finalidadTecnologiaSalud;
                                SheetAP.Cells["N" + rowap].Value = obj.tipoDocumentoIdentificacion;
                                SheetAP.Cells["O" + rowap].Value = obj.numDocumentoIdentificacion;
                                SheetAP.Cells["P" + rowap].Value = obj.codDiagnosticoPrincipal;
                                SheetAP.Cells["Q" + rowap].Value = obj.codDiagnosticoRelacionado;
                                SheetAP.Cells["R" + rowap].Value = obj.codComplicacion;
                                SheetAP.Cells["S" + rowap].Value = obj.vrServicio;                           // consecutivo
                                SheetAP.Cells["T" + rowap].Value = obj.conceptoRecaudo;                          // Detalle Log
                                SheetAP.Cells["U" + rowap].Value = obj.valorPagoModerador;
                                SheetAP.Cells["V" + rowap].Value = obj.numFEVPagoModerador;
                                SheetAP.Cells["W" + rowap].Value = obj.consecutivo;
                                SheetAP.Cells["X" + rowap].Value = obj.mensaje;
                                SheetAP.Cells["Y" + rowap].Value = obj.indice;
                                SheetAP.Cells["Z" + rowap].Value = obj.mes + "/" + obj.año;              // Mes y año reporte
                                rowap++;
                            }
                            break;


                        // ==================== AT ====================
                        case "AT":
                            foreach (var obj in ripsAT)
                            {
                                SheetAT.Cells["A" + rowat].Value = obj.codigo_habilitacion_homologado;
                                SheetAT.Cells["B" + rowat].Value = obj.numFactura;
                                SheetAT.Cells["C" + rowat].Value = obj.tipoDocumentoIdentificacion;
                                SheetAT.Cells["D" + rowat].Value = obj.numDocumentoIdentificacion;
                                SheetAT.Cells["E" + rowat].Value = obj.numAutorizacion;
                                SheetAT.Cells["F" + rowat].Value = obj.idMIPRES;
                                SheetAT.Cells["G" + rowat].Value = obj.fechaSuministroTecnologia?.ToString("dd/MM/yyyy");
                                SheetAT.Cells["H" + rowat].Value = obj.tipoOS;
                                SheetAT.Cells["I" + rowat].Value = obj.codTecnologiaSalud;
                                SheetAT.Cells["J" + rowat].Value = obj.nomTecnologiaSalud;
                                SheetAT.Cells["K" + rowat].Value = obj.cantidadOS;
                                SheetAT.Cells["L" + rowat].Value = obj.tipoDocumentoIdentificacion;
                                SheetAT.Cells["M" + rowat].Value = obj.numDocumentoIdentificacion;
                                SheetAT.Cells["N" + rowat].Value = obj.vrUnitOS;
                                SheetAT.Cells["O" + rowat].Value = obj.vrServicio;
                                SheetAT.Cells["P" + rowat].Value = obj.conceptoRecaudo;                           // consecutivo
                                SheetAT.Cells["Q" + rowat].Value = obj.valorPagoModerador;                          // Detalle Log
                                SheetAT.Cells["R" + rowat].Value = obj.numFEVPagoModerador;
                                SheetAT.Cells["S" + rowat].Value = obj.consecutivo;
                                SheetAT.Cells["T" + rowat].Value = obj.mensaje;
                                SheetAT.Cells["U" + rowat].Value = obj.indice;
                                SheetAT.Cells["V" + rowat].Value = obj.mes + "/" + obj.año;              // Mes y año reporte
                                rowat++;
                            }
                            break;


                        // ==================== AU ====================
                        case "AU":
                            foreach (var obj in ripsAU)
                            {
                                SheetAU.Cells["A" + rowau].Value = obj.codigo_habilitacion_homologado;
                                SheetAU.Cells["B" + rowau].Value = obj.numFactura;
                                SheetAU.Cells["C" + rowau].Value = obj.tipoDocumentoIdentificacion;
                                SheetAU.Cells["D" + rowau].Value = obj.numDocumentoIdentificacion;
                                SheetAU.Cells["E" + rowau].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy");
                                SheetAU.Cells["F" + rowau].Value = obj.causaMotivoAtencion;
                                SheetAU.Cells["G" + rowau].Value = obj.codDiagnosticoPrincipal;
                                SheetAU.Cells["H" + rowau].Value = obj.codDiagnosticoPrincipalE;
                                SheetAU.Cells["I" + rowau].Value = obj.codDiagnosticoRelacionadoE1;
                                SheetAU.Cells["J" + rowau].Value = obj.codDiagnosticoRelacionadoE2;
                                SheetAU.Cells["K" + rowau].Value = obj.codDiagnosticoRelacionadoE3;
                                SheetAU.Cells["L" + rowau].Value = obj.condicionDestinoUsuarioEgreso;
                                SheetAU.Cells["M" + rowau].Value = obj.codDiagnosticoCausaMuerte;
                                SheetAU.Cells["N" + rowau].Value = obj.fechaEgreso?.ToString("dd/MM/yyyy");
                                SheetAU.Cells["O" + rowau].Value = obj.consecutivo;
                                SheetAU.Cells["P" + rowau].Value = obj.mensaje;
                                SheetAU.Cells["Q" + rowau].Value = obj.indice;
                                SheetAU.Cells["R" + rowau].Value = obj.mes + "/" + obj.año;

                                rowau++;
                            }
                            break;


                        // ==================== US ====================
                        case "US":
                            foreach (var obj in ripsUS)
                            {
                                SheetUS.Cells["A" + rowus].Value = obj.tipoDocumentoIdentificacion;
                                SheetUS.Cells["B" + rowus].Value = obj.numDocumentoIdentificacion;
                                SheetUS.Cells["C" + rowus].Value = obj.tipoUsuario;
                                SheetUS.Cells["D" + rowus].Value = obj.fechaNacimiento?.ToString("dd/MM/yyyy");
                                SheetUS.Cells["E" + rowus].Value = obj.codSexo;
                                SheetUS.Cells["F" + rowus].Value = obj.codPaisResidencia;
                                SheetUS.Cells["G" + rowus].Value = obj.codMunicipioResidencia;
                                SheetUS.Cells["H" + rowus].Value = obj.codZonaTerritorialResidencia;
                                SheetUS.Cells["I" + rowus].Value = obj.incapacidad;
                                SheetUS.Cells["J" + rowus].Value = obj.consecutivo;
                                SheetUS.Cells["K" + rowus].Value = obj.codPaisOrigen;
                                SheetUS.Cells["L" + rowus].Value = obj.mensaje;
                                SheetUS.Cells["M" + rowus].Value = obj.indice;
                                SheetUS.Cells["N" + rowus].Value = obj.mes + "/" + obj.año;
                                rowus++;
                            }
                            break;

                    }
                }


                string namefile = "Reporte_Log_Errores_SAMI_FIS_" + regionales.indice + "_" + mes + "_" + año;
                SheetAC.Cells["A:AA"].AutoFitColumns();
                SheetAH.Cells["A:U"].AutoFitColumns();
                SheetAM.Cells["A:AA"].AutoFitColumns();
                SheetAN.Cells["A:Q"].AutoFitColumns();
                SheetAP.Cells["A:Z"].AutoFitColumns();
                SheetAT.Cells["A:V"].AutoFitColumns();
                SheetAU.Cells["A:R"].AutoFitColumns();
                SheetUS.Cells["A:N"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('Problemas al generar el reporte');" +
                "</script> ";
                Response.Write(rta);
                Response.End();
            }

        }



        public void ExportarLogRipsInoportunos(int regional, int mes, int año)
        {

            Models.CuentasMedicas.RipsFis Model = new Models.CuentasMedicas.RipsFis();
            Ref_regional regionales = BusClass.GetRefRegion().Where(l => l.id_ref_regional == regional).FirstOrDefault();

            try
            {
                var ripsAC = Model.FisRipsInoportuno_AC(regional, mes, año, ref MsgRes);
                var ripsAP = Model.FisRipsInoportuno_AP(regional, mes, año, ref MsgRes);
                var ripsAN = Model.FisRipsInoportuno_AN(regional, mes, año, ref MsgRes);
                var ripsAU = Model.FisRipsInoportuno_AU(regional, mes, año, ref MsgRes);
                var ripsAH = Model.FisRipsInoportuno_AH(regional, mes, año, ref MsgRes);
                var ripsAM = Model.FisRipsInoportuno_AM(regional, mes, año, ref MsgRes);



                if (!ripsAC.Any()
                     && !ripsAP.Any()
                     && !ripsAN.Any()
                     && !ripsAU.Any()
                     && !ripsAH.Any()
                     && !ripsAM.Any())
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                                 "window.alert('No se han encontrado resultados');" +
                                 "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                else
                {
                    string[] array = new string[6] { "AC", "AP", "AN", "AU", "AH", "AM" };

                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet SheetAC = Ep.Workbook.Worksheets.Add("AC");
                    ExcelWorksheet SheetAP = Ep.Workbook.Worksheets.Add("AP");
                    ExcelWorksheet SheetAN = Ep.Workbook.Worksheets.Add("AN");
                    ExcelWorksheet SheetAU = Ep.Workbook.Worksheets.Add("AU");
                    ExcelWorksheet SheetAH = Ep.Workbook.Worksheets.Add("AH");
                    ExcelWorksheet SheetAM = Ep.Workbook.Worksheets.Add("AM");

                    System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(22, 54, 92);

                    foreach (string archivo in array)
                    {
                        switch (archivo)
                        {
                            case "AC":

                                SheetAC.Cells["A1:Z1"].Style.Font.Bold = true;
                                SheetAC.Cells["A1:Z1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                SheetAC.Cells["A1:Z1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                SheetAC.Cells["A1:Z1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                                SheetAC.Cells["A1"].Value = "CodPrestador";
                                SheetAC.Cells["B1"].Value = "num_factura";
                                SheetAC.Cells["C1"].Value = "tipo_id_usuario";
                                SheetAC.Cells["D1"].Value = "num_id_usuario";
                                SheetAC.Cells["E1"].Value = "fechalnicioAtencion";
                                SheetAC.Cells["F1"].Value = "numAutorizacion";
                                SheetAC.Cells["G1"].Value = "codConsulta";
                                SheetAC.Cells["H1"].Value = "modalidadGrupoServicioTecSal";
                                SheetAC.Cells["I1"].Value = "grupoServicios";
                                SheetAC.Cells["J1"].Value = "codServicio";
                                SheetAC.Cells["K1"].Value = "finalidadTecnologiaSalud";
                                SheetAC.Cells["L1"].Value = "causaMotivoAtencion";
                                SheetAC.Cells["M1"].Value = "codDiagnosticoPrincipal";
                                SheetAC.Cells["N1"].Value = "codDiagnosticoRelacionado1";
                                SheetAC.Cells["O1"].Value = "codDiagnosticoRelacionado2";
                                SheetAC.Cells["P1"].Value = "codDiagnosticoRelacionado3";
                                SheetAC.Cells["Q1"].Value = "tipoDiagnosticoPrincipal";
                                SheetAC.Cells["R1"].Value = "tipoDocumentoIdentificacion";
                                SheetAC.Cells["S1"].Value = "numDocumentoIdentificacion";
                                SheetAC.Cells["T1"].Value = "vrServicio";
                                SheetAC.Cells["U1"].Value = "conceptoRecaudo";
                                SheetAC.Cells["V1"].Value = "valorPagoModerador";
                                SheetAC.Cells["W1"].Value = "numFEVPagoModerador";
                                SheetAC.Cells["X1"].Value = "consecutivo";
                                //SheetAC.Cells["Y1"].Value = "Detalle Log";
                                SheetAC.Cells["Y1"].Value = "Regional";
                                SheetAC.Cells["Z1"].Value = "Mes y año reporte";

                                break;


                            case "AP":
                                SheetAP.Cells["A1:Y1"].Style.Font.Bold = true;
                                SheetAP.Cells["A1:Y1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                SheetAP.Cells["A1:Y1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                SheetAP.Cells["A1:Y1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                                SheetAP.Cells["A1"].Value = "codPrestador";
                                SheetAP.Cells["B1"].Value = "num_factura";
                                SheetAP.Cells["C1"].Value = "tipo_id_usuario";
                                SheetAP.Cells["D1"].Value = "num_id_usuario";
                                SheetAP.Cells["E1"].Value = "fechaInicioAtencion";
                                SheetAP.Cells["F1"].Value = "idMIPRES";
                                SheetAP.Cells["G1"].Value = "numAutorizacion";
                                SheetAP.Cells["H1"].Value = "codProcedimiento";
                                SheetAP.Cells["I1"].Value = "viaIngresoServicioSalud";
                                SheetAP.Cells["J1"].Value = "modalidadGrupoServicioTecSal";
                                SheetAP.Cells["K1"].Value = "grupoServicios";
                                SheetAP.Cells["L1"].Value = "codServicio";
                                SheetAP.Cells["M1"].Value = "finalidadTecnologiaSalud";
                                SheetAP.Cells["N1"].Value = "tipoDocumentoIdentificacion";
                                SheetAP.Cells["O1"].Value = "numDocumentoIdentificacion";
                                SheetAP.Cells["P1"].Value = "codDiagnosticoPrincipal";
                                SheetAP.Cells["Q1"].Value = "codDiagnosticoRelacionado";
                                SheetAP.Cells["R1"].Value = "codComplicacion";
                                SheetAP.Cells["S1"].Value = "vrServicio";
                                SheetAP.Cells["T1"].Value = "conceptoRecaudo";
                                SheetAP.Cells["U1"].Value = "valorPagoModerador";
                                SheetAP.Cells["V1"].Value = "numFEVPagoModerador";
                                SheetAP.Cells["W1"].Value = "consecutivo";
                                //SheetAP.Cells["X1"].Value = "Detalle Log";
                                SheetAP.Cells["X1"].Value = "Regional";
                                SheetAP.Cells["Y1"].Value = "Mes y año reporte";

                                break;


                            case "AN":

                                SheetAN.Cells["A1:P1"].Style.Font.Bold = true;
                                SheetAN.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                SheetAN.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                SheetAN.Cells["A1:P1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                                SheetAN.Cells["A1"].Value = "codPrestador";
                                SheetAN.Cells["B1"].Value = "num_factura";
                                SheetAN.Cells["C1"].Value = "tipoDocumentoIdentificacion";
                                SheetAN.Cells["D1"].Value = "numDocumentoIdentificacion";
                                SheetAN.Cells["E1"].Value = "fechaNacimiento";
                                SheetAN.Cells["F1"].Value = "edadGestacional";
                                SheetAN.Cells["G1"].Value = "numConsultasCPrenatal";
                                SheetAN.Cells["H1"].Value = "codSexoBiologico";
                                SheetAN.Cells["I1"].Value = "peso";
                                SheetAN.Cells["J1"].Value = "codDiagnosticoPrincipal";
                                SheetAN.Cells["K1"].Value = "condicionDestinoUsuarioEgreso";
                                SheetAN.Cells["L1"].Value = "codDiagnosticoCausaMuerte";
                                SheetAN.Cells["M1"].Value = "fechaEgreso";
                                SheetAN.Cells["N1"].Value = "consecutivo";
                                //SheetAN.Cells["O1"].Value = "Detalle Log";
                                SheetAN.Cells["O1"].Value = "Regional";
                                SheetAN.Cells["P1"].Value = "Mes y año reporte";

                                break;


                            case "AU":
                                SheetAU.Cells["A1:Q1"].Style.Font.Bold = true;
                                SheetAU.Cells["A1:Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                SheetAU.Cells["A1:Q1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                SheetAU.Cells["A1:Q1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                                SheetAU.Cells["A1"].Value = "codPrestador";
                                SheetAU.Cells["B1"].Value = "num_factura";
                                SheetAU.Cells["C1"].Value = "tipo_id_usuario";
                                SheetAU.Cells["D1"].Value = "num_id_usuario";
                                SheetAU.Cells["E1"].Value = "fechaInicioAtencion";
                                SheetAU.Cells["F1"].Value = "causaMotivoAtencion";
                                SheetAU.Cells["G1"].Value = "codDiagnosticoPrincipal";
                                SheetAU.Cells["H1"].Value = "codDiagnosticoPrincipalE";
                                SheetAU.Cells["I1"].Value = "codDiagnosticoRelacionadoE1";
                                SheetAU.Cells["J1"].Value = "codDiagnosticoRelacionadoE2";
                                SheetAU.Cells["K1"].Value = "codDiagnosticoRelacionadoE3";
                                SheetAU.Cells["L1"].Value = "condicionDestinoUsuarioEgreso";
                                SheetAU.Cells["M1"].Value = "codDiagnosticoCausaMuerte";
                                SheetAU.Cells["N1"].Value = "fechaEgreso";
                                SheetAU.Cells["O1"].Value = "consecutivo";
                                //SheetAU.Cells["P1"].Value = "Detalle Log";
                                SheetAU.Cells["P1"].Value = "Regional";
                                SheetAU.Cells["Q1"].Value = "Mes y año reporte";

                                break;


                            case "AH":

                                SheetAH.Cells["A1:T1"].Style.Font.Bold = true;
                                SheetAH.Cells["A1:T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                SheetAH.Cells["A1:T1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                SheetAH.Cells["A1:T1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                                SheetAH.Cells["A1"].Value = "codPrestador";
                                SheetAH.Cells["B1"].Value = "num_factura";
                                SheetAH.Cells["C1"].Value = "tipo_id_usuario";
                                SheetAH.Cells["D1"].Value = "num_id_usuario";
                                SheetAH.Cells["E1"].Value = "viaIngresoServicioSalud";
                                SheetAH.Cells["F1"].Value = "fechaInicioAtencion";
                                SheetAH.Cells["G1"].Value = "numAutorizacion";
                                SheetAH.Cells["H1"].Value = "causaMotivoAtencion";
                                SheetAH.Cells["I1"].Value = "codDiagnosticoPrincipal";
                                SheetAH.Cells["J1"].Value = "codDiagnosticoPrincipalE";
                                SheetAH.Cells["K1"].Value = "codDiagnosticoRelacionadoE1";
                                SheetAH.Cells["L1"].Value = "codDiagnosticoRelacionadoE2";
                                SheetAH.Cells["M1"].Value = "codDiagnosticoRelacionadoE3";
                                SheetAH.Cells["N1"].Value = "codComplicacion";
                                SheetAH.Cells["O1"].Value = "condicionDestinoUsuarioEgreso";
                                SheetAH.Cells["P1"].Value = "codDiagnosticoCausaMuerte";
                                SheetAH.Cells["Q1"].Value = "fechaEgreso";
                                SheetAH.Cells["R1"].Value = "consecutivo";
                                //SheetAH.Cells["S1"].Value = "Detalle Log";
                                SheetAH.Cells["S1"].Value = "Regional";
                                SheetAH.Cells["T1"].Value = "Mes y año reporte";

                                break;



                            case "AM":

                                SheetAM.Cells["A1:Z1"].Style.Font.Bold = true;
                                SheetAM.Cells["A1:Z1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                SheetAM.Cells["A1:Z1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                SheetAM.Cells["A1:Z1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                                SheetAM.Cells["A1"].Value = "codPrestador";
                                SheetAM.Cells["B1"].Value = "num_factura";
                                SheetAM.Cells["C1"].Value = "tipo_id_usuario";
                                SheetAM.Cells["D1"].Value = "num_id_usuario";
                                SheetAM.Cells["E1"].Value = "numAutorizacion";
                                SheetAM.Cells["F1"].Value = "idMIPRES";
                                SheetAM.Cells["G1"].Value = "fechaDispensAdmon";
                                SheetAM.Cells["H1"].Value = "codDiagnosticoPrincipal";
                                SheetAM.Cells["I1"].Value = "codDiagnosticoRelacionado";
                                SheetAM.Cells["J1"].Value = "tipoMedicamento";
                                SheetAM.Cells["K1"].Value = "codTecnologiaSalud";
                                SheetAM.Cells["L1"].Value = "nomTecnologiaSalud";
                                SheetAM.Cells["M1"].Value = "concentracionMedicamento";
                                SheetAM.Cells["N1"].Value = "unidadMedida";
                                SheetAM.Cells["O1"].Value = "formaFarmaceutica";
                                SheetAM.Cells["P1"].Value = "unidadMinDispensa";
                                SheetAM.Cells["Q1"].Value = "cantidadMedicamento";
                                SheetAM.Cells["R1"].Value = "diasTratamiento";
                                SheetAM.Cells["S1"].Value = "vrUnitMedicamento";
                                SheetAM.Cells["T1"].Value = "vrServicio";
                                SheetAM.Cells["U1"].Value = "conceptoRecaudo";
                                SheetAM.Cells["V1"].Value = "valorPagoModerador";
                                SheetAM.Cells["W1"].Value = "numFEVPagoModerador";
                                SheetAM.Cells["X1"].Value = "consecutivo";
                                SheetAM.Cells["Y1"].Value = "Regional";
                                SheetAM.Cells["Z1"].Value = "Mes y año reporte";
                               

                                break;


                            default:
                                break;
                        }
                    }


                    int rowac = 2, rowap = 2, rowan = 2, rowau = 2, rowah = 2, rowam = 2;

                    for (int i = 0; i < array.Length; i++)
                    {
                        switch (array[i])
                        {
                            case "AC":
                                foreach (var obj in ripsAC)
                                {
                                    SheetAC.Cells["A" + rowac].Value = obj.codigo_habilitacion_homologado; // codPrestador
                                    SheetAC.Cells["B" + rowac].Value = obj.numFactura;
                                    SheetAC.Cells["C" + rowac].Value = obj.tipoDocumentoIdentificacion;
                                    SheetAC.Cells["D" + rowac].Value = obj.numDocumentoIdentificacion;
                                    SheetAC.Cells["E" + rowac].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy");
                                    SheetAC.Cells["F" + rowac].Value = obj.numAutorizacion;
                                    SheetAC.Cells["G" + rowac].Value = obj.codConsulta;
                                    SheetAC.Cells["H" + rowac].Value = obj.modalidadGrupoServicioTecSal;
                                    SheetAC.Cells["I" + rowac].Value = obj.grupoServicios;
                                    SheetAC.Cells["J" + rowac].Value = obj.codServicio;
                                    SheetAC.Cells["K" + rowac].Value = obj.finalidadTecnologiaSalud;
                                    SheetAC.Cells["L" + rowac].Value = obj.causaMotivoAtencion;
                                    SheetAC.Cells["M" + rowac].Value = obj.codDiagnosticoPrincipal;
                                    SheetAC.Cells["N" + rowac].Value = obj.codDiagnosticoRelacionado1;
                                    SheetAC.Cells["O" + rowac].Value = obj.codDiagnosticoRelacionado2;
                                    SheetAC.Cells["P" + rowac].Value = obj.codDiagnosticoRelacionado3;
                                    SheetAC.Cells["Q" + rowac].Value = obj.tipoDiagnosticoPrincipal;
                                    SheetAC.Cells["R" + rowac].Value = obj.tipoDocumentoIdentificacion;
                                    SheetAC.Cells["S" + rowac].Value = obj.numDocumentoIdentificacion;
                                    SheetAC.Cells["T" + rowac].Value = obj.vrServicio;
                                    SheetAC.Cells["U" + rowac].Value = obj.conceptoRecaudo;
                                    SheetAC.Cells["V" + rowac].Value = obj.valorPagoModerador;
                                    SheetAC.Cells["W" + rowac].Value = obj.numFEVPagoModerador;
                                    SheetAC.Cells["X" + rowac].Value = obj.consecutivo;
                                    //SheetAC.Cells["Y" + rowac].Value = obj.mensaje; // Detalle Log
                                    SheetAC.Cells["Y" + rowac].Value = obj.indice;
                                    SheetAC.Cells["Z" + rowac].Value = obj.mes + "/" + obj.año; // Mes y año reporte
                                    rowac++;
                                }
                                break;


                            case "AP":
                                foreach (var obj in ripsAP)
                                {
                                    SheetAP.Cells["A" + rowap].Value = obj.codigo_habilitacion_homologado;
                                    SheetAP.Cells["B" + rowap].Value = obj.numFactura;
                                    SheetAP.Cells["C" + rowap].Value = obj.tipoDocumentoIdentificacion;
                                    SheetAP.Cells["D" + rowap].Value = obj.numDocumentoIdentificacion;
                                    SheetAP.Cells["E" + rowap].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy"); ;
                                    SheetAP.Cells["F" + rowap].Value = obj.idMIPRES;
                                    SheetAP.Cells["G" + rowap].Value = obj.numAutorizacion;
                                    SheetAP.Cells["H" + rowap].Value = obj.codProcedimiento;
                                    SheetAP.Cells["I" + rowap].Value = obj.viaIngresoServicioSalud;
                                    SheetAP.Cells["J" + rowap].Value = obj.modalidadGrupoServicioTecSal;
                                    SheetAP.Cells["K" + rowap].Value = obj.grupoServicios;
                                    SheetAP.Cells["L" + rowap].Value = obj.codServicio;
                                    SheetAP.Cells["M" + rowap].Value = obj.finalidadTecnologiaSalud;
                                    SheetAP.Cells["N" + rowap].Value = obj.tipoDocumentoIdentificacion;
                                    SheetAP.Cells["O" + rowap].Value = obj.numDocumentoIdentificacion;
                                    SheetAP.Cells["P" + rowap].Value = obj.codDiagnosticoPrincipal;
                                    SheetAP.Cells["Q" + rowap].Value = obj.codDiagnosticoRelacionado;
                                    SheetAP.Cells["R" + rowap].Value = obj.codComplicacion;
                                    SheetAP.Cells["S" + rowap].Value = obj.vrServicio;                           // consecutivo
                                    SheetAP.Cells["T" + rowap].Value = obj.conceptoRecaudo;                          // Detalle Log
                                    SheetAP.Cells["U" + rowap].Value = obj.valorPagoModerador;
                                    SheetAP.Cells["V" + rowap].Value = obj.numFEVPagoModerador;
                                    SheetAP.Cells["W" + rowap].Value = obj.consecutivo;
                                    //SheetAP.Cells["X" + rowap].Value = obj.mensaje;
                                    SheetAP.Cells["X" + rowap].Value = obj.indice;
                                    SheetAP.Cells["Y" + rowap].Value = obj.mes + "/" + obj.año;              // Mes y año reporte
                                    rowap++;
                                }
                                break;


                            case "AN":
                                foreach (var obj in ripsAN)
                                {
                                    SheetAN.Cells["A" + rowan].Value = obj.codigo_habilitacion_homologado;   // codPrestador
                                    SheetAN.Cells["B" + rowan].Value = obj.numFactura;
                                    SheetAN.Cells["C" + rowan].Value = obj.tipoDocumentoIdentificacion;
                                    SheetAN.Cells["D" + rowan].Value = obj.numDocumentoIdentificacion;
                                    SheetAN.Cells["E" + rowan].Value = obj.fechaNacimiento;
                                    SheetAN.Cells["F" + rowan].Value = obj.edadGestacional;
                                    SheetAN.Cells["G" + rowan].Value = obj.numConsultasCPrenatal;
                                    SheetAN.Cells["H" + rowan].Value = obj.codSexoBiologico;
                                    SheetAN.Cells["I" + rowan].Value = obj.peso;
                                    SheetAN.Cells["J" + rowan].Value = obj.codDiagnosticoPrincipal;
                                    SheetAN.Cells["K" + rowan].Value = obj.condicionDestinoUsuarioEgreso;
                                    SheetAN.Cells["L" + rowan].Value = obj.codDiagnosticoCausaMuerte;
                                    SheetAN.Cells["M" + rowan].Value = obj.fechaEgreso;
                                    SheetAN.Cells["N" + rowan].Value = obj.consecutivo;
                                    //SheetAN.Cells["O" + rowan].Value = obj.mensaje;
                                    SheetAN.Cells["O" + rowan].Value = obj.indice;                           // consecutivo
                                    SheetAN.Cells["P" + rowan].Value = obj.mes + "/" + obj.año;                           // Detalle Log

                                    rowan++;
                                }
                                break;




                            case "AU":
                                foreach (var obj in ripsAU)
                                {
                                    SheetAU.Cells["A" + rowau].Value = obj.codigo_habilitacion_homologado;
                                    SheetAU.Cells["B" + rowau].Value = obj.numFactura;
                                    SheetAU.Cells["C" + rowau].Value = obj.tipoDocumentoIdentificacion;
                                    SheetAU.Cells["D" + rowau].Value = obj.numDocumentoIdentificacion;
                                    SheetAU.Cells["E" + rowau].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy");
                                    SheetAU.Cells["F" + rowau].Value = obj.causaMotivoAtencion;
                                    SheetAU.Cells["G" + rowau].Value = obj.codDiagnosticoPrincipal;
                                    SheetAU.Cells["H" + rowau].Value = obj.codDiagnosticoPrincipalE;
                                    SheetAU.Cells["I" + rowau].Value = obj.codDiagnosticoRelacionadoE1;
                                    SheetAU.Cells["J" + rowau].Value = obj.codDiagnosticoRelacionadoE2;
                                    SheetAU.Cells["K" + rowau].Value = obj.codDiagnosticoRelacionadoE3;
                                    SheetAU.Cells["L" + rowau].Value = obj.condicionDestinoUsuarioEgreso;
                                    SheetAU.Cells["M" + rowau].Value = obj.codDiagnosticoCausaMuerte;
                                    SheetAU.Cells["N" + rowau].Value = obj.fechaEgreso?.ToString("dd/MM/yyyy");
                                    SheetAU.Cells["O" + rowau].Value = obj.consecutivo;
                                    //SheetAU.Cells["P" + rowau].Value = obj.mensaje;
                                    SheetAU.Cells["P" + rowau].Value = obj.indice;
                                    SheetAU.Cells["Q" + rowau].Value = obj.mes + "/" + obj.año;

                                    rowau++;
                                }
                                break;



                            case "AH":

                                foreach (var obj in ripsAH)
                                {
                                    SheetAH.Cells["A" + rowah].Value = obj.codigo_habilitacion_homologado;   // codPrestador
                                    SheetAH.Cells["B" + rowah].Value = obj.numFactura;                        // num_factura
                                    SheetAH.Cells["C" + rowah].Value = obj.tipoDocumentoIdentificacion;       // tipo_id_usuario
                                    SheetAH.Cells["D" + rowah].Value = obj.numDocumentoIdentificacion;        // num_id_usuario
                                    SheetAH.Cells["E" + rowah].Value = obj.viaIngresoServicioSalud;
                                    SheetAH.Cells["F" + rowah].Value = obj.fechaInicioAtencion?.ToString("dd/MM/yyyy");
                                    SheetAH.Cells["G" + rowah].Value = obj.numAutorizacion;
                                    SheetAH.Cells["H" + rowah].Value = obj.causaMotivoAtencion;
                                    SheetAH.Cells["I" + rowah].Value = obj.codDiagnosticoPrincipal;
                                    SheetAH.Cells["J" + rowah].Value = obj.codDiagnosticoPrincipalE;
                                    SheetAH.Cells["K" + rowah].Value = obj.codDiagnosticoRelacionadoE1;
                                    SheetAH.Cells["L" + rowah].Value = obj.codDiagnosticoRelacionadoE2;
                                    SheetAH.Cells["M" + rowah].Value = obj.codDiagnosticoRelacionadoE3;
                                    SheetAH.Cells["N" + rowah].Value = obj.codComplicacion;
                                    SheetAH.Cells["O" + rowah].Value = obj.condicionDestinoUsuarioEgreso;
                                    SheetAH.Cells["P" + rowah].Value = obj.codDiagnosticoCausaMuerte;
                                    SheetAH.Cells["Q" + rowah].Value = obj.fechaEgreso?.ToString("dd/MM/yyyy");
                                    SheetAH.Cells["R" + rowah].Value = obj.consecutivo;                            // consecutivo
                                    //SheetAH.Cells["S" + rowah].Value = obj.mensaje;                           // Detalle Log
                                    SheetAH.Cells["S" + rowah].Value = obj.indice;                          // Regional
                                    SheetAH.Cells["T" + rowah].Value = obj.mes + "/" + obj.año;              // Mes y año reporte
                                    rowah++;
                                }

                                break;

                            case "AM":
                                foreach (var obj in ripsAM)
                                {
                                    SheetAM.Cells["A" + rowam].Value = obj.codigo_habilitacion_homologado;   // codPrestador
                                    SheetAM.Cells["B" + rowam].Value = obj.numFactura;                       // num_factura
                                    SheetAM.Cells["C" + rowam].Value = obj.tipoDocumentoIdentificacion;      // tipo_id_usuario
                                    SheetAM.Cells["D" + rowam].Value = obj.numDocumentoIdentificacion;       // num_id_usuario
                                    SheetAM.Cells["E" + rowam].Value = obj.numAutorizacion;
                                    SheetAM.Cells["F" + rowam].Value = obj.idMIPRES;
                                    SheetAM.Cells["G" + rowam].Value = obj.fechaDispensAdmon?.ToString("dd/MM/yyyy");
                                    SheetAM.Cells["H" + rowam].Value = obj.codDiagnosticoPrincipal;
                                    SheetAM.Cells["I" + rowam].Value = obj.codDiagnosticoRelacionado;
                                    SheetAM.Cells["J" + rowam].Value = obj.tipoMedicamento;
                                    SheetAM.Cells["K" + rowam].Value = obj.codTecnologiaSalud;
                                    SheetAM.Cells["L" + rowam].Value = obj.nomTecnologiaSalud;
                                    SheetAM.Cells["M" + rowam].Value = obj.concentracionMedicamento;
                                    SheetAM.Cells["N" + rowam].Value = obj.unidadMedida;
                                    SheetAM.Cells["O" + rowam].Value = obj.formaFarmaceutica;
                                    SheetAM.Cells["P" + rowam].Value = obj.unidadMinDispensa;
                                    SheetAM.Cells["Q" + rowam].Value = obj.cantidadMedicamento;
                                    SheetAM.Cells["R" + rowam].Value = obj.diasTratamiento;
                                    SheetAM.Cells["S" + rowam].Value = obj.vrUnitMedicamento;
                                    SheetAM.Cells["T" + rowam].Value = obj.vrServicio;
                                    SheetAM.Cells["U" + rowam].Value = obj.conceptoRecaudo;
                                    SheetAM.Cells["V" + rowam].Value = obj.valorPagoModerador;
                                    SheetAM.Cells["W" + rowam].Value = obj.numFEVPagoModerador;
                                    SheetAM.Cells["X" + rowam].Value = obj.consecutivo;                           // consecutivo
                                    //SheetAM.Cells["Y" + rowam].Value = obj.mensaje;                          // Detalle Log
                                    SheetAM.Cells["Y" + rowam].Value = obj.indice;
                                    SheetAM.Cells["Z" + rowam].Value = obj.mes + "/" + obj.año;             // Mes y año reporte
                                    rowam++;
                                }
                                break;

                        }
                    }


                    string namefile = "Reporte_Log_RIPS_Inoportunos_SAMI_FIS_" + regionales.indice + "_" + mes + "_" + año;
                    SheetAC.Cells["A:AZ"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }



        }


        public ActionResult TableroControlValidacionesRips()
        {
            List<ref_validaciones_Ripsfis> listado = new List<ref_validaciones_Ripsfis>();

            try
            {
                listado = BusClass.ListarValidacionesFis();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.conteo = listado.Count();

            return View();
        }


        public JsonResult ActualizarValidacionRips(int idValidacion, int estado)
        {
            int rta = 0;
            string data = "";

            try
            {

                rta = BusClass.ActualizarEstadoValidacion(idValidacion, estado);

                if(rta == 1)
                {
                    data = "Se modifico el estado de la Validacion con ID: " + idValidacion;
                }
               

            }
            catch(Exception ex)
            {
                rta = 0;
                data = "Error al actualizar el estado de la validación: " + ex.Message;
            }



            return Json(new { idrta = rta, mensaje = data }, JsonRequestBehavior.AllowGet);
        }

    }
}