


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
    [SessionExpireFilter]
    public class RipsController : Controller
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

        /*Alexis quiñones 26/03/2019*/
        public ActionResult CargueRIPS()
        {
            Models.Facturacion.FacturaDevolucion Model = new Models.Facturacion.FacturaDevolucion();
            ViewBag.listaRegionales = Model.RefRegional;
            ViewData["exitoso"] = 0;
            ViewData["archivosnoc"] = new List<string>();
            return View();
        }

        [HttpPost]
        public ActionResult CargueRIPS(int chkant, HttpPostedFileBase[] file, int regional, string mes, string año)
        {
            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();
            Models.Facturacion.FacturaDevolucion Model2 = new Models.Facturacion.FacturaDevolucion();
            int exito = 1;

            //Me dice cuantos archivos se van a cargar.
            int numarchivos = file.Length;
            int archivosnocargados = 0;
            Int32 IdRips = 0;
            Int32 rta = 0;
            List<string> listaerrores = new List<string>();
            var rip = Model.ValidacionCargueRips(regional, int.Parse(mes), int.Parse(año));

            try
            {
                int line = 0;
                if (chkant == 0 && rip == null)
                {
                    RIPS ObjRips = new RIPS();
                    ObjRips.cantidad = numarchivos.ToString();
                    ObjRips.usuario = SesionVar.UserName;
                    ObjRips.fecha_cargue = DateTime.Now;
                    ObjRips.id_regional = regional;
                    ObjRips.mes = mes;
                    ObjRips.año = año;
                    ObjRips.activo = true;
                    IdRips = Model.InsertRips(ObjRips);
                }
                else
                {
                    IdRips = rip.id_rips;
                }

                if (IdRips != 0)
                {
                    foreach (HttpPostedFileBase item in file)
                    {
                        rta = 0;
                        string namefile = item.FileName;
                        string tiposrips = item.FileName.Substring(0, 2);

                        RIPS Obj = Model.ConsultaRips(IdRips).FirstOrDefault();
                        Obj.rips_nom += tiposrips + "; ";
                        Obj.descripcion += namefile + "; ";

                        using (StreamReader lector = new StreamReader(item.InputStream))
                        {
                            try
                            {
                                string tipoRip = item.FileName.Substring(0, 2);
                                if (tipoRip.Contains("AC"))
                                {
                                    List<RIPS_AC> listaac = new List<RIPS_AC>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaac.Add(CargarObjRipsAC(linea, IdRips));

                                        }
                                    }
                                    rta += Model.InsertRipsAC(listaac, ref MsgRes);

                                }
                                else if (tipoRip.Contains("AD"))
                                {
                                    List<RIPS_AD> listaad = new List<RIPS_AD>();

                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaad.Add(CargarObjRipsAD(linea, IdRips));
                                        }
                                    }
                                    rta = Model.InsertRipsAD(listaad, ref MsgRes);

                                }
                                else if (tipoRip.Contains("AF"))
                                {
                                    List<RIPS_AF> listaaf = new List<RIPS_AF>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaaf.Add(CargarObjRipsAF(linea, IdRips));
                                        }
                                    }
                                    rta = Model.InsertRipsAF(listaaf, ref MsgRes);

                                }
                                else if (tipoRip.Contains("AH"))
                                {
                                    List<RIPS_AH> listaah = new List<RIPS_AH>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaah.Add(CargarObjRipsAH(linea, IdRips));
                                        }
                                    }
                                    rta = Model.InsertRipsAH(listaah, ref MsgRes);

                                }
                                else if (tipoRip.Contains("AM"))
                                {
                                    List<RIPS_AM> listaam = new List<RIPS_AM>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaam.Add(CargarObjRipsAM(linea, IdRips));
                                        }
                                    }
                                    rta = Model.InsertRipsAM(listaam, ref MsgRes);

                                }
                                else if (tipoRip.Contains("AN"))
                                {
                                    List<RIPS_AN> listaan = new List<RIPS_AN>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaan.Add(CargarObjRipsAN(linea, IdRips));
                                        }
                                    }
                                    rta = Model.InsertRipsAN(listaan, ref MsgRes);

                                }
                                else if (tipoRip.Contains("AP"))
                                {
                                    List<RIPS_AP> listaap = new List<RIPS_AP>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaap.Add(CargarObjRipsAP(linea, IdRips));
                                        }
                                    }
                                    rta = Model.InsertRipsAP(listaap, ref MsgRes);

                                }
                                else if (tipoRip.Contains("AT"))
                                {
                                    List<RIPS_AT> listaat = new List<RIPS_AT>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaat.Add(CargarObjRipsAT(linea, IdRips));
                                        }
                                    }
                                    rta = Model.InsertRipsAT(listaat, ref MsgRes);

                                }
                                else if (tipoRip.Contains("AU"))
                                {
                                    List<RIPS_AU> listaau = new List<RIPS_AU>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaau.Add(CargarObjRipsAU(linea, IdRips));
                                        }

                                    }
                                    rta = Model.InsertRipsAU(listaau, ref MsgRes);

                                }
                                else if (tipoRip.Contains("CT"))
                                {
                                    List<RIPS_CT> listact = new List<RIPS_CT>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listact.Add(CargarObjRipsCT(linea, IdRips));
                                        }

                                    }
                                    rta = Model.InsertRipsCT(listact, ref MsgRes);

                                }
                                else if (tipoRip.Contains("US"))
                                {
                                    List<RIPS_US> listaus = new List<RIPS_US>();
                                    while (lector.Peek() > -1)
                                    {
                                        string linea = lector.ReadLine();
                                        if (!String.IsNullOrEmpty(linea))
                                        {
                                            line++;
                                            listaus.Add(CargarObjRipsUS(linea, IdRips));
                                        }

                                    }
                                    rta = Model.InsertRipsUS(listaus, ref MsgRes);
                                }

                                if (rta == 0)
                                {
                                    Model.ActualizarRips(Obj);
                                }
                                else
                                {
                                    listaerrores.Add("File: " + namefile + " Causa:" + MsgRes.DescriptionResponse);
                                    archivosnocargados += 1;
                                    numarchivos -= 1;
                                }

                                line = 0;
                            }
                            catch (Exception ex)
                            {
                                listaerrores.Add("File: " + namefile + "   Causa:" + ex.Message + "  Linea:" + line);
                                archivosnocargados += 1;
                                numarchivos -= 1;
                            }

                        }
                    }

                    //se consulta el registro rips insertado, para actualizar su estado segun las respuestas del proceso
                    RIPS ObjRips2 = Model.ConsultaRips(IdRips).FirstOrDefault();
                    if (ObjRips2.id_rips != 0)
                    {
                        if (archivosnocargados > 0)
                        {
                            ObjRips2.estado = "FA";
                        }
                        else
                        {
                            ObjRips2.estado = "CO";
                        }
                        Model.ActualizarRips(ObjRips2);

                    }
                }

            }
            catch (Exception ex)
            {
                RIPS ObjRips = Model.ConsultaRips(IdRips).FirstOrDefault();
                ObjRips.estado = "FA";
                Model.ActualizarRips(ObjRips);
                exito = 2;
            }


            ViewBag.numarchivos = numarchivos;
            ViewData["archivosnoc"] = listaerrores;
            ViewBag.listaRegionales = Model2.RefRegional;
            ViewData["exitoso"] = exito;
            return View();
        }

        public ActionResult Rips()
        {
            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();
            ViewBag.ListRips = Model.ConsultaRips(0).OrderByDescending(l => l.mes).Where(l => !String.IsNullOrEmpty(l.estado) && l.estado.ToUpper() != "FA" && l.activo == true);
            return View();
        }
        
        public ActionResult ReporteEvaluacion(Int32 IdRips)
        {
            ViewData["Error"] = "";
            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();

            var con = Model.getprestadoresnoexistentes(IdRips, ref MsgRes).ToList();

            List<managmentReportePrestadoresNoExistentesResult> listaprestadoresnoexistentes = new List<managmentReportePrestadoresNoExistentesResult>();
            List<reporterips> reporte = new List<reporterips>();

            try
            {
                if (con.Count() == 0)
                {
                    reporte = Model.GetEvaluacionRips(IdRips).ToList();
                }
                else
                {
                    listaprestadoresnoexistentes = con.Distinct().ToList();
                    ViewData["Error"] = "Algunos prestadores no aparecen en base de datos y se pueden generar errores de calculo en el reporte";
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }

            Session["ListadoEvaluacion"] = reporte;
            Session["ListadoErrores"] = new List<ManagmentErroresRipsEvaluacionResult>();
            ViewBag.Reporte = reporte.ToList();

            ViewBag.TCantidad = reporte.Select(l => l.cantidad).Sum();
            ViewBag.TRfactudaros = reporte.Select(l => l.registros_facturados_oportunamente).Sum();
            ViewBag.TErroresdx = reporte.Select(l => l.Errores_dx).Sum();
            ViewBag.TErrorespx = reporte.Select(l => l.Errores_pc).Sum();
            ViewBag.TErroresrc = reporte.Select(l => l.Errores_rc).Sum();
            ViewBag.TtlErrores = reporte.Select(l => l.Total_Errores).Sum();
            ViewBag.TtlSinError = reporte.Select(l => l.Registros_sin_error).Sum();
            ViewBag.TtlConError = reporte.Select(l => l.Registros_unicos_con_error).Sum();

            ViewData["Prestadoresnoexistentes"] = listaprestadoresnoexistentes;
            var rips = Model.ConsultaRips(IdRips).FirstOrDefault();
            ViewData["idrips"] = IdRips;
            ViewData["NombreRips"] = rips.id_regional + "_" + rips.mes + "" + rips.año;
            ViewBag.tabla = "";

            ViewBag.rol = SesionVar.ROL;

            return View();
        }

        public ActionResult TableroControlRips()
        {
            List<management_rips_tableroControlResult> listado = new List<management_rips_tableroControlResult>();
            listado = BusClass.TraerListadoRips();
            ViewBag.rol = SesionVar.ROL;
            return View(listado);
        }

        public PartialViewResult ConsolidadoRipsId(int? idRips)
        {
            List<management_rips_tableoControl_detalladoResult> Listado = BusClass.TraerListadoRipsConsolidadoId(idRips).OrderByDescending(x => x.conteo).ToList();
            ViewBag.rol = SesionVar.ROL;
            Session["ConsolidadoRipsDetallado"] = Listado;

            return PartialView(Listado);
        }

        public JsonResult EliminarRipsId(int? idRips)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var elimina = BusClass.EliminarCargueRipsId(idRips);

                if (elimina != 0)
                {
                    log_rips_eliminarCargue log = new log_rips_eliminarCargue();
                    log.id_rips = idRips;
                    log.usuario_digita = SesionVar.UserName;
                    log.fecha_digita = DateTime.Now;

                    var insertaLog = BusClass.InsertarLogEliminacionRips(log);
                    mensaje = "RIPS ELIMINADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN LA ELIMINACIÓN DE RIPS";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ELIMINACIÓN DE RIPS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public void ExcelConsolidadoRipsId()
        {
            try
            {
                List<management_rips_tableoControl_detalladoResult> listareporte = new List<management_rips_tableoControl_detalladoResult>();
                listareporte = (List<management_rips_tableoControl_detalladoResult>)Session["ConsolidadoRipsDetallado"];

                if (listareporte.Count > 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte Consolidado RIPS");

                    Sheet.Cells["A1:D1"].Style.Font.Bold = true;
                    System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:D1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:D1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    Sheet.Cells["A1:D1"].Style.Font.Name = "Century Gothic";
                    Sheet.Cells["A1:D1"].Style.Font.Family = 12;

                    Sheet.Cells["A1"].Value = "Id RIPS";
                    Sheet.Cells["B1"].Value = "Conteo";
                    Sheet.Cells["C1"].Value = "Código prestador";
                    Sheet.Cells["D1"].Value = "Tipo cargue";

                    int row = 2;
                    foreach (management_rips_tableoControl_detalladoResult item in listareporte)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_rips;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.conteo;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.codigo_prestador;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.TipoCargue;
                        row++;
                    }
                    Sheet.Cells["A:D"].AutoFitColumns();


                    Response.Clear();
                    Response.ContentType = "application/excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteConsolidadoRIPS_" + DateTime.Now + ".xlsx");
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

        public string ArmartablaLogEvaluacion(Int32 IdRips)
        {

            string result = "";
            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();
            List<Logerroresevaluacionrips> listado = Model.GetLogEvaluacionRips(IdRips);
            List<LogerroresevaluacionripsDtll> consulta2 = BusClass.ConsultaLogRipsEvaluacionDtll(IdRips, ref MsgRes);
            Session["ListaErroresDetalle"] = consulta2;
            Session["ListadoErrores"] = listado;

            if (listado.Count > 0)
            {
                result += "<table class='table table-striped table-condensed table-bordered table-hover'>";
                result += "<thead>";
                result += "<tr>";
                result += "<th>Codigo Prestador</th>";
                result += "<th>Prestador</th>";
                result += "<th>Destalle Log</th>";
                result += "<th>Total Errorres</th>";
                result += "<th></th>";
                result += "</tr>";
                result += "</thead>";
                result += "<tbody>";
                foreach (Logerroresevaluacionrips item in listado)
                {
                    if (item.Total_Errores > 0)
                    {
                        result += "<tr>";
                        result += "<td>" + item.codigo_prestador + "</td>";
                        result += "<td>" + item.prestador + "</td>";
                        result += "<td>";
                        result += "<ul>";
                        if (item.AC_Error_en_DX_Genero > 0)
                            result += "<li> AC: Error en DX Genero: " + item.AC_Error_en_DX_Genero + "</li>";
                        if (item.AP_Error_en_DX_Genero > 0)
                            result += "<li> AP: Error en DX Genero: " + item.AP_Error_en_DX_Genero + "</li>";
                        if (item.AH_Error_en_DX_Genero > 0)
                            result += "<li> AH: Error en DX Genero: " + item.AH_Error_en_DX_Genero + "</li>";
                        if (item.AC_Num_factura_no_existe_en_AF > 0)
                            result += "<li> AC: Numero de factura no existe en  archivoAF: " + item.AC_Num_factura_no_existe_en_AF + "</li>";
                        if (item.AP_Num_factura_no_existe_en_AF > 0)
                            result += "<li> AP: Numero de factura no existe en  archivoAF: " + item.AP_Num_factura_no_existe_en_AF + "</li>";
                        if (item.AH_Num_factura_no_existe_en_AF > 0)
                            result += "<li> AU: Numero de factura no existe en  archivoAF: " + item.AH_Num_factura_no_existe_en_AF + "</li>";
                        if (item.AU_Num_factura_no_existe_en_AF > 0)
                            result += "<li> AU: Numero de factura no existe en  archivoAF: " + item.AU_Num_factura_no_existe_en_AF + "</li>";
                        if (item.AC_Dx_no_corresponde_con_finalidad > 0)
                            result += "<li> AC: DX no corrresponde con finalidad: " + item.AC_Dx_no_corresponde_con_finalidad + "</li>";
                        if (item.AC_Usuario_debe_estar_en_US > 0)
                            result += "<li> AC: Identificación no existe en archivo US: " + item.AC_Usuario_debe_estar_en_US + "</li>";
                        if (item.AP_Usuario_debe_estar_en_US > 0)
                            result += "<li> AP: Identificación no existe en archivo US: " + item.AP_Usuario_debe_estar_en_US + "</li>";
                        if (item.AH_Usuario_debe_estar_en_US > 0)
                            result += "<li> AH: Identificación no existe en archivo US: " + item.AH_Usuario_debe_estar_en_US + "</li>";
                        if (item.AU_Usuario_debe_estar_en_US > 0)
                            result += "<li> AU: Identificación no existe en archivo US: " + item.AU_Usuario_debe_estar_en_US + "</li>";
                        if (item.AC_sin_DX > 0)
                            result += "<li> AC: Sin DX: " + item.AC_sin_DX + "</li>";
                        if (item.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado > 0)
                            result += "<li> AP: Procedimiento Quirúrgico sin DX o con DX errado: " + item.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado + "</li>";
                        if (item.AP_Sin_ambito_en_el_CUPS > 0)
                            result += "<li> AP: Sin ambito en el Cups: " + item.AP_Sin_ambito_en_el_CUPS + "</li>";
                        if (item.AP_Sin_CUPS > 0)
                            result += "<li> AP: Sin Cups: " + item.AP_Sin_CUPS + "</li>";
                        if (item.AU_Sin_causa_basica_de_muerte > 0)
                            result += "<li> AU: Sin Causa Basica de Muerte: " + item.AU_Sin_causa_basica_de_muerte + "</li>";
                        if (item.AU_Error_en_fecha_de_egreso > 0)
                            result += "<li> AU_Error en fecha de egreso: " + item.AU_Error_en_fecha_de_egreso + "</li>";
                        if (item.AU_Error_en_causa_externa > 0)
                            result += "<li> AU: Error en causa externa: " + item.AU_Error_en_causa_externa + "</li>";
                        if (item.AH_Error_en_causa_externa > 0)
                            result += "<li> AH: Error en causa externa: " + item.AH_Error_en_causa_externa + "</li>";
                        if (item.AH_Error_de_DX_no_aplica_R_Z > 0)
                            result += "<li> AH: Error de diagnostico, No aplica R-Z: " + item.AH_Error_de_DX_no_aplica_R_Z + "</li>";
                        if (item.AU_Error_de_DX_no_aplica_R_Z > 0)
                            result += "<li> AU: Error de diagnostico, No aplica R-Z: " + item.AU_Error_de_DX_no_aplica_R_Z + "</li>";
                        if (item.AN_Sin_fecha_de_muerte > 0)
                            result += "<li> AN: Sin fecha de Muerte: " + item.AN_Sin_fecha_de_muerte + "</li>";
                        if (item.AN_Sin_hora_de_muerte > 0)
                            result += "<li> AN: Sin hora de Muerte: " + item.AN_Sin_hora_de_muerte + "</li>";
                        result += "</ul>";
                        result += "</td>";
                        result += "<td>" + item.Total_Errores + "</td>";
                        result += "<td><a href='" + Url.Action("ExportaraexcelLog", "Rips", new { codigohabilitacion = item.codigo_prestador }) + "'>Informe Detallado</a></td>";
                        result += "</tr>";
                    }
                }
                result += "</tbody>";
                result += "</table>";
            }
            return result;
        }

        public void ConsultaRIPSLogtabulado(int mes, int año)
        {
            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();
            List<RIPS> listarips = Model.GetListaRipsPorMesYAño(mes, año, null);

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte log errores tabulados");

            Sheet.Cells["A1"].Value = "Codigo Prestador";
            Sheet.Cells["B1"].Value = "Razón Social";
            Sheet.Cells["C1"].Value = "AC: Error en DX Genero";
            Sheet.Cells["D1"].Value = "AP: Error en DX Genero";
            Sheet.Cells["E1"].Value = "AH: Error en DX Genero";
            Sheet.Cells["F1"].Value = "AC: Numero de factura no existe en  archivo AF";
            Sheet.Cells["G1"].Value = "AP: Numero de factura no existe en  archivo AF";
            Sheet.Cells["H1"].Value = "AU: Numero de factura no existe en  archivoAF";
            Sheet.Cells["I1"].Value = "AH: Numero de factura no existe en  archivoAF";
            Sheet.Cells["J1"].Value = "AC: Finalidad 5 fuera del rango de edad";
            Sheet.Cells["K1"].Value = "AC: Identificación no existe en archivo US";
            Sheet.Cells["L1"].Value = "AP: Identificación no existe en archivo US";
            Sheet.Cells["M1"].Value = "AU: Identificación no existe en archivo US";
            Sheet.Cells["N1"].Value = "AH: Identificación no existe en archivo US";
            Sheet.Cells["O1"].Value = "AC: Sin DX";
            Sheet.Cells["P1"].Value = "AP: Procedimiento Quirúrgico sin DX o con DX errado";
            Sheet.Cells["Q1"].Value = "AP: Sin ambito en el Cups";
            Sheet.Cells["R1"].Value = "AP: Sin Cups";
            Sheet.Cells["S1"].Value = "AU: Sin Causa Basica de Muerte";
            Sheet.Cells["T1"].Value = "AU: Error en fecha de egreso";
            Sheet.Cells["U1"].Value = "AU: Error en causa externa RC";
            Sheet.Cells["V1"].Value = "AH: Error en causa externa";
            Sheet.Cells["W1"].Value = "AU: Error en causa externa DX";
            Sheet.Cells["X1"].Value = "AH: Error de diagnostico, No aplica R-Z";
            Sheet.Cells["Y1"].Value = "AN: Sin fecha de Muerte";
            Sheet.Cells["Z1"].Value = "AN: Sin hora de Muerte";
            Sheet.Cells["AA1"].Value = "Total Errores";
            Sheet.Cells["AB1"].Value = "Regional";
            Sheet.Cells["AC1"].Value = "Mes y Año";
            int row = 2;

            foreach (RIPS item in listarips)
            {
                Ref_regional reg = BusClass.GetRefRegion().Where(l => l.id_ref_regional == item.id_regional).FirstOrDefault();
                List<Logerroresevaluacionrips> listadohistorico = Model.GetLogEvaluacionRipsHistorico(item.id_rips, reg.indice, item.mes, item.año);

                foreach (Logerroresevaluacionrips item2 in listadohistorico)
                {
                    Sheet.Cells["A" + row].Value = item2.codigo_prestador;
                    Sheet.Cells["B" + row].Value = item2.prestador;
                    Sheet.Cells["C" + row].Value = item2.AC_Error_en_DX_Genero;
                    Sheet.Cells["D" + row].Value = item2.AP_Error_en_DX_Genero;
                    Sheet.Cells["E" + row].Value = item2.AH_Error_en_DX_Genero;
                    Sheet.Cells["F" + row].Value = item2.AC_Num_factura_no_existe_en_AF;
                    Sheet.Cells["G" + row].Value = item2.AP_Num_factura_no_existe_en_AF;
                    Sheet.Cells["H" + row].Value = item2.AU_Num_factura_no_existe_en_AF;
                    Sheet.Cells["I" + row].Value = item2.AH_Num_factura_no_existe_en_AF;
                    Sheet.Cells["J" + row].Value = item2.AC_Dx_no_corresponde_con_finalidad;
                    Sheet.Cells["K" + row].Value = item2.AC_Usuario_debe_estar_en_US;
                    Sheet.Cells["L" + row].Value = item2.AP_Usuario_debe_estar_en_US;
                    Sheet.Cells["M" + row].Value = item2.AU_Usuario_debe_estar_en_US;
                    Sheet.Cells["N" + row].Value = item2.AH_Usuario_debe_estar_en_US;
                    Sheet.Cells["O" + row].Value = item2.AC_sin_DX;
                    Sheet.Cells["P" + row].Value = item2.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado;
                    Sheet.Cells["Q" + row].Value = item2.AP_Sin_ambito_en_el_CUPS;
                    Sheet.Cells["R" + row].Value = item2.AP_Sin_CUPS;
                    Sheet.Cells["S" + row].Value = item2.AU_Sin_causa_basica_de_muerte;
                    Sheet.Cells["T" + row].Value = item2.AU_Error_en_fecha_de_egreso;
                    Sheet.Cells["U" + row].Value = item2.AU_Error_en_causa_externa;
                    Sheet.Cells["V" + row].Value = item2.AH_Error_en_causa_externa;
                    Sheet.Cells["W" + row].Value = item2.AU_Error_de_DX_no_aplica_R_Z;
                    Sheet.Cells["X" + row].Value = item2.AH_Error_de_DX_no_aplica_R_Z;
                    Sheet.Cells["Y" + row].Value = item2.AN_Sin_fecha_de_muerte;
                    Sheet.Cells["Z" + row].Value = item2.AN_Sin_hora_de_muerte;
                    Sheet.Cells["AA" + row].Value = item2.Total_Errores;
                    Sheet.Cells["AB" + row].Value = reg.nombre_regional;
                    Sheet.Cells["AC" + row].Value = mes.ToString() + "/" + año.ToString();

                    row++;
                }
            }

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ReportesLogRips_" + mes.ToString() + "_" + año.ToString() + ".xls");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        #region Cargar o setear valores de los registros por tipo de RIPS

        public RIPS_AC CargarObjRipsAC(string Linea, Int32 IdRips)
        {
            string[] array = Linea.Split(';');
            if (array.Length < 2)
            {
                array = Linea.Split(',');
            }
            RIPS_AC ObjRip = new RIPS_AC();
            int longitud = array.Length;
            try
            {
                if (longitud > 0)
                    ObjRip.num_factura = array[0];
                if (longitud > 1)
                    ObjRip.codigo_prestador = array[1];
                if (longitud > 2)
                    ObjRip.tipo_id_usuario = array[2];
                if (longitud > 3)
                    ObjRip.num_id_usuario = array[3];
                if (longitud > 4)
                {
                    if (!String.IsNullOrEmpty(array[4]))
                    {
                        string strDate = array[4];
                        string[] dateString = strDate.Split('/');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_consulta = enter_date;
                    }
                }
                if (longitud > 5)
                    ObjRip.num_autorizacion = array[5];

                if (longitud > 6)
                    ObjRip.cod_consulta = array[6];

                if (longitud > 7)
                    ObjRip.finalidad_consulta = array[7];

                if (longitud > 8)
                    ObjRip.causa_externa = array[8];

                if (longitud > 9)
                    ObjRip.cod_dx_ppal = array[9];

                if (longitud > 10)
                    ObjRip.cod_dx_rel_1 = array[10];

                if (longitud > 11)
                    ObjRip.cod_dx_rel_2 = array[11];

                if (longitud > 12)
                    ObjRip.cod_dx_rel_3 = array[12];

                if (longitud > 13)
                    ObjRip.tipo_dx_ppal = array[13];

                if (longitud > 14)
                {
                    if (!String.IsNullOrEmpty(array[14]))
                        ObjRip.valor_consulta = Convert.ToDecimal(array[14]);
                }
                if (longitud > 15)
                {
                    if (!String.IsNullOrEmpty(array[15]))
                        ObjRip.valor_cuota_moderadora = Convert.ToDecimal(array[15]);
                }
                if (longitud > 16)
                {
                    if (!String.IsNullOrEmpty(array[16]))
                        ObjRip.valor_neto_a_pagar = Convert.ToDecimal(array[16]);
                }
                ObjRip.id_rips = IdRips;

                return ObjRip;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public RIPS_AD CargarObjRipsAD(string Linea, Int32 IdRips)
        {
            RIPS_AD ObjRip = new RIPS_AD();
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }

                int longitud = array.Length;

                if (longitud > 0)
                    ObjRip.num_factura = array[0];
                if (longitud > 1)
                    ObjRip.codigo_prestador = array[1];
                if (longitud > 2)
                    ObjRip.codigo_concepto = array[2];
                if (longitud > 3)
                {
                    if (!String.IsNullOrEmpty(array[3]))
                        ObjRip.cantidad = Convert.ToDouble(array[3]);
                }
                if (longitud > 4)
                {
                    if (!String.IsNullOrEmpty(array[4]))
                        ObjRip.valor_unitario = Convert.ToDecimal(array[4]);
                }
                if (longitud > 5)
                {
                    if (!String.IsNullOrEmpty(array[5]))
                        ObjRip.valor_total_concepto = Convert.ToDecimal(array[5]);
                }
                ObjRip.id_rips = IdRips;
                return ObjRip;
            }
            catch
            {
                throw;
            }

        }

        public RIPS_AF CargarObjRipsAF(string Linea, Int32 IdRips)
        {
            RIPS_AF ObjRip = new RIPS_AF();
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }

                int longitud = array.Length;

                if (longitud > 0)
                    ObjRip.codigo_prestador = array[0];

                if (longitud > 2)
                    ObjRip.nombre_prestador = array[1];

                if (longitud > 3)
                    ObjRip.tipo_id_prestador = array[2];

                if (longitud > 4)
                    ObjRip.num_id_prestador = array[3];

                if (longitud > 5)
                    ObjRip.num_factura = array[4];
                if (longitud > 6)
                {
                    if (!String.IsNullOrEmpty(array[5]))
                    {

                        string strDate = array[5];
                        string[] dateString = strDate.Split('/');
                        if (dateString.Count() <= 1)
                            dateString = strDate.Split('-');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_exp_factura = enter_date;
                    }
                }

                if (longitud > 7)
                {
                    if (!String.IsNullOrEmpty(array[6]))
                    {

                        string strDate = array[6];
                        string[] dateString = strDate.Split('/');
                        if (dateString.Count() <= 1)
                            dateString = strDate.Split('-');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_inicio = enter_date;

                    }
                }

                if (longitud > 8)
                {
                    if (!String.IsNullOrEmpty(array[7]))
                    {
                        string strDate = array[7];
                        string[] dateString = strDate.Split('/');
                        if (dateString.Count() <= 1)
                            dateString = strDate.Split('-');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_final = enter_date;

                    }
                }
                if (longitud > 9)
                    ObjRip.cod_entidad_adm = array[8];
                if (longitud > 9)
                    ObjRip.nom_entidad_adm = array[9];
                if (longitud > 10)
                    ObjRip.num_contrato = array[10];
                if (longitud > 11)
                    ObjRip.plan_beneficios = array[11];
                if (longitud > 12)
                    ObjRip.num_poliza = array[12];
                if (longitud > 13)
                {
                    if (!String.IsNullOrEmpty(array[13]))
                        ObjRip.copago = Convert.ToDecimal(RemoveSpecialCharacters(array[13]));
                }
                if (longitud > 14)
                {
                    if (!String.IsNullOrEmpty(array[14]))
                        ObjRip.valor_comision = Convert.ToDecimal(RemoveSpecialCharacters(array[14]));
                }
                if (longitud > 15)
                {
                    if (!String.IsNullOrEmpty(array[15]))
                        ObjRip.valor_descuentos = Convert.ToDecimal(RemoveSpecialCharacters(array[15]));
                }
                if (longitud > 16)
                {
                    if (!String.IsNullOrEmpty(array[16]))
                        ObjRip.valor_neto = Convert.ToDecimal(RemoveSpecialCharacters(array[16]));
                }
                ObjRip.id_rips = IdRips;
                return ObjRip;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public RIPS_AH CargarObjRipsAH(string Linea, Int32 IdRips)
        {
            RIPS_AH ObjRip = new RIPS_AH();
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }

                int longitud = array.Length;
                if (longitud > 0)
                    ObjRip.num_factura = array[0];
                if (longitud > 1)
                    ObjRip.cod_prestador = array[1];
                if (longitud > 2)
                    ObjRip.tipo_id_usuario = array[2];
                if (longitud > 3)
                    ObjRip.num_id_usuario = array[3];
                if (longitud > 4)
                    ObjRip.via_ingreso = array[4];
                if (longitud > 5)
                {
                    if (!String.IsNullOrEmpty(array[5]))
                    {
                        string strDate = array[5];
                        string[] dateString = strDate.Split('/');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_ingreso = enter_date;
                    }
                }
                if (longitud > 6)
                    ObjRip.hora_ingreso = array[6];
                if (longitud > 7)
                    ObjRip.num_autorizacion = array[7];
                if (longitud > 8)
                    ObjRip.causa_externa = array[8];
                if (longitud > 9)
                    ObjRip.dx_ppal_ingreso = array[9];
                if (longitud > 10)
                    ObjRip.dx_ppal_egreso = array[10];
                if (longitud > 11)
                    ObjRip.dx_rel_1_egreso = array[11];
                if (longitud > 12)
                    ObjRip.dx_rel_2_egreso = array[12];
                if (longitud > 13)
                    ObjRip.dx_rel_3_egreso = array[13];
                if (longitud > 14)
                    ObjRip.dx_complicacion = array[14];
                if (longitud > 15)
                    ObjRip.estado_salida = array[15];
                if (longitud > 16)
                    ObjRip.dx_causa_basica_muerte = array[16];
                if (longitud > 17)
                {
                    if (!String.IsNullOrEmpty(array[17]))
                    {

                        string strDate = array[17];
                        string[] dateString = strDate.Split('/');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_egreso = enter_date;
                    }
                }
                if (longitud > 18)
                    ObjRip.hora_egreso = array[18];
                ObjRip.id_rips = IdRips;
                return ObjRip;

            }
            catch (Exception EX)
            {
                throw;
            }
        }

        public RIPS_AM CargarObjRipsAM(string Linea, Int32 IdRips)
        {
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }
                RIPS_AM ObjRip = new RIPS_AM();

                int longitud = array.Length;
                if (longitud > 0)
                    ObjRip.num_factura = array[0];
                if (longitud > 1)
                    ObjRip.cod_prestador = array[1];
                if (longitud > 2)
                    ObjRip.tipo_id_usuario = array[2];
                if (longitud > 3)
                    ObjRip.num_id_usuario = array[3];
                if (longitud > 4)
                    ObjRip.num_autorizacion = array[4];
                if (longitud > 5)
                    ObjRip.cod_medicamento = array[5];
                if (longitud > 6)
                    ObjRip.tipo_medicamento = array[6];
                if (longitud > 7)
                    ObjRip.nom_generico_medicamento = array[7];
                if (longitud > 8)
                    ObjRip.forma_farmaceutica = array[8];
                if (longitud > 9)
                    ObjRip.concentracion_medicamento = array[9];
                if (longitud > 10)
                    ObjRip.unidad_medida_medicamento = array[10];
                if (longitud > 11)
                    ObjRip.num_unidades = array[11];
                if (longitud > 12)
                {
                    if (!String.IsNullOrEmpty(array[12]))
                        ObjRip.valor_unitario_medicamento = Convert.ToDecimal(array[12]);
                }
                if (longitud > 13)
                {
                    if (!String.IsNullOrEmpty(array[13]))
                        ObjRip.valor_total_medicamento = Convert.ToDecimal(RemoveSpecialCharacters(array[13]));
                }
                ObjRip.id_rips = IdRips;
                return ObjRip;
            }
            catch (Exception msg)
            {

                throw;
            }
        }

        public RIPS_AN CargarObjRipsAN(string Linea, Int32 IdRips)
        {
            RIPS_AN ObjRip = new RIPS_AN();
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }

                int longitud = array.Length;


                if (longitud > 0)
                    ObjRip.num_factura = array[0];

                if (longitud > 1)
                    ObjRip.cod_prestador = array[1];
                if (longitud > 2)
                    ObjRip.tipo_id_madre = array[2];
                if (longitud > 3)
                    ObjRip.num_id_madre = array[3];
                if (longitud > 4)
                {
                    if (!String.IsNullOrEmpty(array[4]))
                    {
                        string strDate = array[4];
                        string[] dateString = strDate.Split('/');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_nacimiento_rn = enter_date;

                    }
                }
                if (longitud > 5)
                    ObjRip.hora_nacimiento = array[5];
                if (longitud > 6)
                    ObjRip.edad_gestacional = array[6];
                if (longitud > 7)
                    ObjRip.control_prenatal = array[7];
                if (longitud > 8)
                    ObjRip.sexo = array[8];
                if (longitud > 9)
                    ObjRip.peso = array[9];
                if (longitud > 10)
                    ObjRip.dx_recien_nacido = array[10];
                if (longitud > 11)
                    ObjRip.causa_muerte = array[11];

                if (longitud > 12)
                {
                    if (!String.IsNullOrEmpty(array[12]))
                        ObjRip.fecha_muerte = array[12];
                }

                if (longitud > 13)
                {
                    if (!String.IsNullOrEmpty(array[13]) && array.Length >= 13)
                        ObjRip.hora_muerte = array[13];
                }

                ObjRip.id_rips = IdRips;
                return ObjRip;
            }
            catch
            {
                throw;
            }
        }

        public RIPS_AP CargarObjRipsAP(string Linea, Int32 IdRips)
        {
            RIPS_AP ObjRip = new RIPS_AP();
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }

                int longitud = array.Length;

                if (longitud > 0)
                    ObjRip.num_factura = array[0];
                if (longitud > 1)
                    ObjRip.cod_prestador = array[1];
                if (longitud > 2)
                    ObjRip.tipo_id_usuario = array[2];
                if (longitud > 3)
                    ObjRip.num_id_usuario = array[3];
                if (longitud > 4)
                {
                    if (!String.IsNullOrEmpty(array[4]))
                    {
                        string strDate = array[4];
                        string[] dateString = strDate.Split('/');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_procedimiento = enter_date;
                    }
                }
                if (longitud > 5)
                    ObjRip.num_autorizacion = array[5];
                if (longitud > 6)
                    ObjRip.cod_procedimiento = array[6];
                if (longitud > 7)
                    ObjRip.ambito_procedimiento = array[7];
                if (longitud > 8)
                    ObjRip.finalidad_procedimiento = array[8];
                if (longitud > 9)
                    ObjRip.personal_atiende = array[9];
                if (longitud > 10)
                    ObjRip.dx_ppal = array[10];
                if (longitud > 11)
                    ObjRip.dx_rel = array[11];
                if (longitud > 12)
                    ObjRip.complicacion = array[12];
                if (longitud > 13)
                    ObjRip.forma_acto_quirurgico = array[13];
                if (longitud > 14)
                {
                    if (!String.IsNullOrEmpty(array[14]))
                        ObjRip.valor_procedimiento = Convert.ToDecimal(array[14]);
                }
                ObjRip.id_rips = IdRips;
                return ObjRip;
            }
            catch
            {
                throw;
            }
        }

        public RIPS_AT CargarObjRipsAT(string Linea, Int32 IdRips)
        {
            RIPS_AT ObjRip = new RIPS_AT();
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }


                int longitud = array.Length;
                if (longitud > 0)
                    ObjRip.num_factura = array[0];
                if (longitud > 1)
                    ObjRip.cod_prestador = array[1];
                if (longitud > 2)
                    ObjRip.tipo_id_usuario = array[2];
                if (longitud > 3)
                    ObjRip.num_id_usuario = array[3];
                if (longitud > 4)
                    ObjRip.num_autorizacion = array[4];
                if (longitud > 5)
                    ObjRip.tipo_servicio = array[5];
                if (longitud > 6)
                    ObjRip.cod_servicio = array[6];
                if (longitud > 7)
                    ObjRip.nombre_servicio = array[7];
                if (longitud > 8)
                    ObjRip.cantidad = array[8];
                if (longitud > 9)
                {
                    if (!String.IsNullOrEmpty(array[9]))
                        ObjRip.valor_unitario = Convert.ToDecimal(RemoveSpecialCharacters(array[9]));
                }
                if (longitud > 10)
                {
                    if (!String.IsNullOrEmpty(array[10]))
                        ObjRip.valor_total = Convert.ToDecimal(RemoveSpecialCharacters(array[10]));
                }
                ObjRip.id_rips = IdRips;
                return ObjRip;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public RIPS_AU CargarObjRipsAU(string Linea, Int32 IdRips)
        {
            RIPS_AU ObjRip = new RIPS_AU();
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }

                int longitud = array.Length;

                if (longitud > 0)
                    ObjRip.num_factura = array[0];
                if (longitud > 1)
                    ObjRip.cod_prestador = array[1];
                if (longitud > 2)
                    ObjRip.tipo_id_usuario = array[2];
                if (longitud > 3)
                    ObjRip.num_id_usuario = array[3];
                if (longitud > 4)
                {
                    if (!String.IsNullOrEmpty(array[4]))
                    {
                        string strDate = array[4];
                        string[] dateString = strDate.Split('/');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_ingreso_observacion = enter_date;
                    }
                }
                if (longitud > 5)
                    ObjRip.hora_ingreso_observacion = array[5];
                if (longitud > 6)
                    ObjRip.num_autorizacion = array[6];
                if (longitud > 7)
                    ObjRip.causa_externa = array[7];
                if (longitud > 8)
                    ObjRip.dx_salida = array[8];
                if (longitud > 9)
                    ObjRip.dx_rel_salida_1 = array[9];
                if (longitud > 10)
                    ObjRip.dx_rel_salida_2 = array[10];
                if (longitud > 11)
                    ObjRip.dx_rel_salida_3 = array[11];
                if (longitud > 12)
                    ObjRip.destino_usuario_salida = array[12];
                if (longitud > 13)
                    ObjRip.estado_usuario_salida = array[13];
                if (longitud > 14)
                    ObjRip.causa_basica_muerte = array[14];
                if (longitud > 15)
                {
                    if (!String.IsNullOrEmpty(array[15]))
                    {
                        string strDate = array[15];
                        string[] dateString = strDate.Split('/');
                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                        ObjRip.fecha_salida = enter_date;
                    }
                }
                if (longitud > 16)
                    ObjRip.hora_salida = array[16];
                ObjRip.id_rips = IdRips;
                return ObjRip;
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public RIPS_CT CargarObjRipsCT(string Linea, Int32 IdRips)
        {
            RIPS_CT ObjRip = new RIPS_CT();
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }

                int longitud = array.Length;

                if (longitud > 0)
                    ObjRip.codigo_prestador = array[0];
                if (longitud > 1)
                {
                    if (!String.IsNullOrEmpty(array[1]))
                        ObjRip.fecha_remision = array[1];
                }
                if (longitud > 2)
                    ObjRip.codigo_archivo = array[2];
                if (longitud > 3)
                    ObjRip.total_registros = array[3];
                ObjRip.id_rips = IdRips;
                return ObjRip;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public RIPS_US CargarObjRipsUS(string Linea, Int32 IdRips)
        {
            RIPS_US ObjRip = new RIPS_US();
            try
            {
                string[] array = Linea.Split(';');
                if (array.Length < 2)
                {
                    array = Linea.Split(',');
                }

                int longitud = array.Length;

                if (longitud > 0)
                    ObjRip.tipo_id_usuario = array[0];
                if (longitud > 1)
                    ObjRip.num_id_usuario = array[1];
                if (longitud > 2)
                    ObjRip.cod_entidad_adm = array[2];
                if (longitud > 3)
                    ObjRip.tipo_usuario = array[3];
                if (longitud > 4)
                    ObjRip.primer_apellido = RemoveSpecialCharacters(array[4]);
                if (longitud > 5)
                    ObjRip.segundo_apellido = RemoveSpecialCharacters(array[5]);
                if (longitud > 6)
                    ObjRip.primer_nombre = RemoveSpecialCharacters(array[6]);
                if (longitud > 7)
                    ObjRip.segundo_nombre = RemoveSpecialCharacters(array[7]);
                if (longitud > 8)
                {
                    if (!string.IsNullOrEmpty(array[8]))
                    {
                        int edad = Convert.ToInt32(RemoveSpecialCharacters(array[8]));
                        ObjRip.edad = RemoveSpecialCharacters(array[8]);
                    }
                    else
                    {
                        ObjRip.edad = "";
                    }
                }

                if (longitud > 9)
                    ObjRip.unidad_medida_edad = RemoveSpecialCharacters(array[9]);
                if (longitud > 10)
                    ObjRip.sexo = array[10];
                if (longitud > 11)
                    ObjRip.cod_departamento_residencia = array[11];
                if (longitud > 12)
                    ObjRip.cod_municipio_residencia = array[12];
                if (longitud > 13)
                    ObjRip.zona_residencia = array[13];
                ObjRip.id_rips = IdRips;
                return ObjRip;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        /// <summary>
        /// Metodo que valida que la fecha de ingreso sea menor a la fecha de egreso
        /// </summary>
        /// <param name="fechaingreso"></param>
        /// <param name="fechaegreso"></param>
        /// <returns></returns>
        public bool ValidarFechas(DateTime? fechaingreso, DateTime? fechaegreso)
        {
            bool result = false;

            if (fechaingreso != null && fechaegreso != null)
            {
                if (DateTime.Compare(fechaingreso.Value, fechaegreso.Value) < 0)
                {
                    // la fecha de egreso es mayor a la fecha de ingreso
                    result = false;
                }
                else
                {
                    result = true;
                }

            }

            return result;
        }

        public bool ValidarRangoFechas(DateTime fechafactura, DateTime fechaconsulta)
        {
            decimal NumeroMeses = Math.Abs((fechafactura.Month - fechaconsulta.Month) + 12 * (fechafactura.Year - fechaconsulta.Year));

            if (NumeroMeses <= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Evaluacion(Int32 IdRips, string nombre)
        {

            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();
            List<reporterips> RipsList = (List<reporterips>)Session["ListadoEvaluacion"];

            ExcelPackage Ep = new ExcelPackage();
            //Nombre de la hoja de calculo
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Evaluación");

            Sheet.Cells["A1:O1"].Style.Font.Bold = true;
            System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(12, 64, 102);
            Sheet.Cells["A1:O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:O1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:O1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
            Sheet.Cells["A1:O1"].Style.Font.Name = "Century Gothic";

            Sheet.Cells["A1"].Value = "Código del prestador";
            Sheet.Cells["B1"].Value = "Razón social";
            Sheet.Cells["C1"].Value = "Ciudad prestación";
            Sheet.Cells["D1"].Value = "Cantidad";
            Sheet.Cells["E1"].Value = "Registros Facturados Oportunamente";
            Sheet.Cells["F1"].Value = "% Operación Facturación";
            Sheet.Cells["G1"].Value = "Errores D.X";
            Sheet.Cells["H1"].Value = "Errores P.X";
            Sheet.Cells["I1"].Value = "Errores R.C";
            Sheet.Cells["J1"].Value = "Total Errores";
            Sheet.Cells["K1"].Value = "Registros Unicos con Error";
            Sheet.Cells["L1"].Value = "Registros sin error";
            Sheet.Cells["M1"].Value = "% Calidad RIPS";
            Sheet.Cells["N1"].Value = "Regional de cargue";
            Sheet.Cells["O1"].Value = "Fecha reporte";
            int row = 2;

            foreach (reporterips item in RipsList)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.codhabilitacion;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.razon_social;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.muni_nombre;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.cantidad;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.registros_facturados_oportunamente;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.porcentaje_oportunidad + "%";
                Sheet.Cells[string.Format("G{0}", row)].Value = item.Errores_dx;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.Errores_pc;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.Errores_rc;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.Total_Errores;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.Registros_unicos_con_error;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.Registros_sin_error;
                Sheet.Cells[string.Format("M{0}", row)].Value = item.porcentaje_calidad_rips + "%";
                Sheet.Cells[string.Format("N{0}", row)].Value = item.nombreRegional;
                Sheet.Cells[string.Format("O{0}", row)].Value = item.fecha_cargue;

                Sheet.Cells[string.Format("O{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                row++;
            }

            string namefile = "EvaluacionRips_" + nombre + "_" + DateTime.Now;
            Sheet.Cells["A:AZ"].AutoFitColumns();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        /// <summary>
        /// f
        /// </summary>
        public void ExportaraexcelLog(string codigohabilitacion)
        {

            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();

            List<Logerroresevaluacionrips> reportelog = (List<Logerroresevaluacionrips>)Session["ListadoErrores"];
            List<LogerroresevaluacionripsDtll> reportelog2 = (List<LogerroresevaluacionripsDtll>)Session["ListaErroresDetalle"];
            string[] array = new string[5] { "AC", "AP", "AN", "AU", "AH" };

            if (!String.IsNullOrEmpty(codigohabilitacion))
            {
                reportelog = reportelog.Where(l => l.codigo_prestador == codigohabilitacion).ToList();
            }

            ExcelPackage Ep = new ExcelPackage();
            //Nombre de la hoja de calculo
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Evaluación");

            #region parte1
            Sheet.Cells["A1:M1"].Style.Font.Bold = true;
            Sheet.Cells["A1:M1"].Style.Font.Size = 14;

            Sheet.Cells["A1"].Value = "Codigo Prestador";
            Sheet.Cells["B1"].Value = "Razón Social";
            Sheet.Cells["C1"].Value = "Detalle Log";
            Sheet.Cells["D1"].Value = "Total Errores";

            int row = 2;
            int row2 = 2;



            foreach (Logerroresevaluacionrips item in reportelog)
            {
                var listaerrores = reportelog2.Where(l => l.codigo_prestador == item.codigo_prestador).ToList();

                Sheet.Cells["A" + row].Value = item.codigo_prestador;
                Sheet.Cells["B" + row].Value = item.prestador;
                Sheet.Cells["D" + row].Value = item.Total_Errores;

                var correspondientes = reportelog.Where(l => l.codigo_prestador == item.codigo_prestador).ToList();
                foreach (Logerroresevaluacionrips item2 in correspondientes)
                {
                    #region foreach

                    row2 = row;

                    if (item.AC_Error_en_DX_Genero > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AC: Error en DX Genero: " + item.AC_Error_en_DX_Genero;
                        row2++;
                    }

                    if (item.AP_Error_en_DX_Genero > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AP: Error en DX Genero: " + item.AP_Error_en_DX_Genero;
                        row2++;
                    }

                    if (item.AH_Error_en_DX_Genero > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AH: Error en DX Genero: " + item.AH_Error_en_DX_Genero;
                        row2++;
                    }

                    if (item.AC_Num_factura_no_existe_en_AF > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AC: Numero de factura no existe en  archivoAF: " + item.AC_Num_factura_no_existe_en_AF;
                        row2++;
                    }

                    if (item.AP_Num_factura_no_existe_en_AF > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AP: Numero de factura no existe en  archivoAF: " + item.AP_Num_factura_no_existe_en_AF;
                        row2++;
                    }
                    if (item.AU_Num_factura_no_existe_en_AF > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AU: Numero de factura no existe en  archivoAF: " + item.AU_Num_factura_no_existe_en_AF;
                        row2++;
                    }
                    if (item.AH_Num_factura_no_existe_en_AF > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AH: Numero de factura no existe en  archivoAF: " + item.AH_Num_factura_no_existe_en_AF;
                        row2++;
                    }

                    if (item.AC_Dx_no_corresponde_con_finalidad > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AC: Finalidad 5 fuera del rango de edad: " + item.AC_Dx_no_corresponde_con_finalidad;
                        row2++;
                    }

                    if (item.AC_Usuario_debe_estar_en_US > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AC: Identificación no existe en archivo US: " + item.AC_Usuario_debe_estar_en_US;
                        row2++;
                    }
                    if (item.AP_Usuario_debe_estar_en_US > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AP: Identificación no existe en archivo US: " + item.AP_Usuario_debe_estar_en_US;
                        row2++;
                    }
                    if (item.AU_Usuario_debe_estar_en_US > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AU: Identificación no existe en archivo US: " + item.AU_Usuario_debe_estar_en_US;
                        row2++;
                    }
                    if (item.AH_Usuario_debe_estar_en_US > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AH: Identificación no existe en archivo US: " + item.AH_Usuario_debe_estar_en_US;
                        row2++;
                    }

                    if (item.AC_sin_DX > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AC: Sin DX: " + item.AC_sin_DX;
                        row2++;
                    }

                    if (item.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AP: Procedimiento Quirúrgico sin DX o con DX errado: " + item.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado;
                        row2++;
                    }

                    if (item.AP_Sin_ambito_en_el_CUPS > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AP: Sin ambito en el Cups: " + item.AP_Sin_ambito_en_el_CUPS;
                        row2++;
                    }

                    if (item.AP_Sin_CUPS > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AP: Sin Cups: " + item.AP_Sin_CUPS;
                        row2++;
                    }

                    if (item.AU_Sin_causa_basica_de_muerte > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AU: Sin Causa Basica de Muerte: " + item.AU_Sin_causa_basica_de_muerte;
                        row2++;
                    }

                    if (item.AU_Error_en_fecha_de_egreso > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AU: Error en fecha de egreso: " + item.AU_Error_en_fecha_de_egreso;
                        row2++;
                    }


                    if (item.AU_Error_en_causa_externa > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AU: Error en causa externa RC: " + item.AU_Error_en_causa_externa;
                        row2++;
                    }
                    if (item.AH_Error_en_causa_externa > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AH: Error en causa externa: " + item.AH_Error_en_causa_externa;
                        row2++;
                    }

                    if (item.AU_Error_de_DX_no_aplica_R_Z > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AU: Error en causa externa DX: " + item.AU_Error_de_DX_no_aplica_R_Z;
                        row2++;
                    }

                    if (item.AH_Error_de_DX_no_aplica_R_Z > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AH: Error de diagnostico, No aplica R-Z: " + item.AH_Error_de_DX_no_aplica_R_Z;
                        row2++;
                    }

                    if (item.AN_Sin_fecha_de_muerte > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AN: Sin fecha de Muerte: " + item.AN_Sin_fecha_de_muerte;
                        row2++;
                    }

                    if (item.AN_Sin_hora_de_muerte > 0)
                    {
                        Sheet.Cells[string.Format("C{0}", row2)].Value = "AN: Sin hora de Muerte: " + item.AN_Sin_hora_de_muerte;
                        row2++;
                    }
                    row = row2;
                    #endregion

                }


                for (int i = 0; i < array.Length; i++)
                {
                    System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(255, 255, 0);
                    var lista = listaerrores.Where(l => l.tipoarchivo == array[i]).ToList();
                    if (lista.Count > 0)
                    {
                        switch (array[i])
                        {
                            #region case
                            case "AC":
                                row++;
                                Sheet.Cells["A" + row + ":p" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":p" + row].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                Sheet.Cells["A" + row + ":p" + row].Style.Font.Bold = true;
                                Sheet.Cells["A" + row].Value = "Errores en el archivo " + array[i];
                                row++;

                                Sheet.Cells["A" + row + ":p" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":p" + row].Style.Fill.BackgroundColor.SetColor(colFromHex); ;
                                Sheet.Cells["A" + row + ":p" + row].Style.Font.Bold = true;
                                Sheet.Cells["A" + row].Value = "num_factura";
                                Sheet.Cells["B" + row].Value = "codigo_prestador";
                                Sheet.Cells["C" + row].Value = "tipo_id_usuario";
                                Sheet.Cells["D" + row].Value = "num_id_usuario";
                                Sheet.Cells["E" + row].Value = "fecha_consulta";
                                Sheet.Cells["F" + row].Value = "num_autorizacion";
                                Sheet.Cells["G" + row].Value = "cod_consulta";
                                Sheet.Cells["H" + row].Value = "finalidad_consulta";
                                Sheet.Cells["I" + row].Value = "causa_externa  ";
                                Sheet.Cells["J" + row].Value = "cod_dx_ppal";
                                Sheet.Cells["K" + row].Value = "cod_dx_rel_1";
                                Sheet.Cells["L" + row].Value = "cod_dx_rel_2";
                                Sheet.Cells["M" + row].Value = "cod_dx_rel_3";
                                Sheet.Cells["N" + row].Value = "tipo_dx_ppal";
                                Sheet.Cells["O" + row].Value = "valor_consulta";
                                Sheet.Cells["P" + row].Value = "valor_neto_a_pagar";
                                foreach (var ac in lista)
                                {
                                    row++;
                                    RIPS_AC obj = Model.GetRipsAcById(ac.id_registro);
                                    Sheet.Cells["A" + row].Value = obj.num_factura;
                                    Sheet.Cells["B" + row].Value = obj.codigo_prestador;
                                    Sheet.Cells["C" + row].Value = obj.tipo_id_usuario;
                                    Sheet.Cells["D" + row].Value = obj.num_id_usuario;
                                    Sheet.Cells["E" + row].Value = obj.fecha_consulta.Value.ToString("MM/dd/yyyy"); ;
                                    Sheet.Cells["F" + row].Value = obj.num_autorizacion;
                                    Sheet.Cells["G" + row].Value = obj.cod_consulta;
                                    Sheet.Cells["H" + row].Value = obj.finalidad_consulta;
                                    Sheet.Cells["I" + row].Value = obj.causa_externa;
                                    Sheet.Cells["J" + row].Value = obj.cod_dx_ppal;
                                    Sheet.Cells["K" + row].Value = obj.cod_dx_rel_1;
                                    Sheet.Cells["L" + row].Value = obj.cod_dx_rel_2;
                                    Sheet.Cells["M" + row].Value = obj.cod_dx_rel_3;
                                    Sheet.Cells["N" + row].Value = obj.tipo_dx_ppal;
                                    Sheet.Cells["O" + row].Value = obj.valor_consulta;
                                    Sheet.Cells["P" + row].Value = obj.valor_neto_a_pagar;
                                }
                                break;
                            case "AP":
                                row++;
                                Sheet.Cells["A" + row + ":O" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":O" + row].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                Sheet.Cells["A" + row].Value = "Errores en el archivo " + array[i];
                                row++;
                                Sheet.Cells["A" + row + ":O" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":O" + row].Style.Fill.BackgroundColor.SetColor(colFromHex); ;
                                Sheet.Cells["A" + row + ":O" + row].Style.Font.Bold = true;
                                Sheet.Cells["A" + row].Value = "num_factura";
                                Sheet.Cells["B" + row].Value = "codigo_prestador";
                                Sheet.Cells["C" + row].Value = "tipo_id_usuario";
                                Sheet.Cells["D" + row].Value = "num_id_usuario";
                                Sheet.Cells["E" + row].Value = "fecha_procedimiento";
                                Sheet.Cells["F" + row].Value = "num_autorizacion";
                                Sheet.Cells["G" + row].Value = "cod_procedimiento";
                                Sheet.Cells["H" + row].Value = "ambito_procedimiento";
                                Sheet.Cells["I" + row].Value = "finalidad_procedimiento";
                                Sheet.Cells["J" + row].Value = "personal_atiende";
                                Sheet.Cells["K" + row].Value = "dx_ppal";
                                Sheet.Cells["L" + row].Value = "dx_rel";
                                Sheet.Cells["M" + row].Value = "complicacion";
                                Sheet.Cells["N" + row].Value = "forma_acto_quirurgico";
                                Sheet.Cells["O" + row].Value = "valor_procedimiento";

                                foreach (var ap in lista)
                                {
                                    row++;
                                    RIPS_AP obj = Model.GetRipsApById(ap.id_registro);
                                    Sheet.Cells["A" + row].Value = obj.num_factura;
                                    Sheet.Cells["B" + row].Value = obj.cod_prestador;
                                    Sheet.Cells["C" + row].Value = obj.tipo_id_usuario;
                                    Sheet.Cells["D" + row].Value = obj.num_id_usuario;
                                    Sheet.Cells["E" + row].Value = obj.fecha_procedimiento.Value.ToString("MM/dd/yyyy");
                                    Sheet.Cells["F" + row].Value = obj.num_autorizacion;
                                    Sheet.Cells["G" + row].Value = obj.cod_procedimiento;
                                    Sheet.Cells["H" + row].Value = obj.ambito_procedimiento;
                                    Sheet.Cells["I" + row].Value = obj.finalidad_procedimiento;
                                    Sheet.Cells["J" + row].Value = obj.personal_atiende;
                                    Sheet.Cells["K" + row].Value = obj.dx_ppal;
                                    Sheet.Cells["L" + row].Value = obj.dx_rel;
                                    Sheet.Cells["M" + row].Value = obj.complicacion;
                                    Sheet.Cells["N" + row].Value = obj.forma_acto_quirurgico;
                                    Sheet.Cells["O" + row].Value = obj.valor_procedimiento;
                                }
                                break;
                            case "AU":
                                row++;
                                Sheet.Cells["A" + row + ":P" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":P" + row].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                Sheet.Cells["A" + row].Value = "Errores en el archivo " + array[i];
                                row++;
                                Sheet.Cells["A" + row + ":P" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":P" + row].Style.Fill.BackgroundColor.SetColor(colFromHex); ;
                                Sheet.Cells["A" + row + ":P" + row].Style.Font.Bold = true;
                                Sheet.Cells["A" + row].Value = "num_factura";
                                Sheet.Cells["B" + row].Value = "codigo_prestador";
                                Sheet.Cells["C" + row].Value = "tipo_id_usuario";
                                Sheet.Cells["D" + row].Value = "num_id_usuario";
                                Sheet.Cells["E" + row].Value = "fecha_ingreso_observacion";
                                Sheet.Cells["F" + row].Value = "hora_ingreso_observacion";
                                Sheet.Cells["G" + row].Value = "num_autorizacion";
                                Sheet.Cells["H" + row].Value = "causa_externa";
                                Sheet.Cells["I" + row].Value = "dx_salida  ";
                                Sheet.Cells["J" + row].Value = "dx_rel_salida_1";
                                Sheet.Cells["K" + row].Value = "dx_rel_salida_2";
                                Sheet.Cells["L" + row].Value = "dx_rel_salida_3";
                                Sheet.Cells["M" + row].Value = "destino_usuario_salida";
                                Sheet.Cells["N" + row].Value = "estado_usuario_salida";
                                Sheet.Cells["O" + row].Value = "causa_basica_muerte";
                                Sheet.Cells["P" + row].Value = "fecha_salida";
                                Sheet.Cells["P" + row].Value = "hora_salida";
                                foreach (var au in lista)
                                {
                                    row++;
                                    RIPS_AU obj = Model.GetRipsAuById(au.id_registro);
                                    Sheet.Cells["A" + row].Value = obj.num_factura;
                                    Sheet.Cells["B" + row].Value = obj.cod_prestador;
                                    Sheet.Cells["C" + row].Value = obj.tipo_id_usuario;
                                    Sheet.Cells["D" + row].Value = obj.num_id_usuario;
                                    Sheet.Cells["E" + row].Value = obj.fecha_ingreso_observacion;
                                    Sheet.Cells["F" + row].Value = obj.hora_ingreso_observacion;
                                    Sheet.Cells["G" + row].Value = obj.num_autorizacion;
                                    Sheet.Cells["H" + row].Value = obj.causa_externa;
                                    Sheet.Cells["I" + row].Value = obj.dx_salida;
                                    Sheet.Cells["J" + row].Value = obj.dx_rel_salida_1;
                                    Sheet.Cells["K" + row].Value = obj.dx_rel_salida_2;
                                    Sheet.Cells["L" + row].Value = obj.dx_rel_salida_3;
                                    Sheet.Cells["M" + row].Value = obj.destino_usuario_salida;
                                    Sheet.Cells["N" + row].Value = obj.estado_usuario_salida;
                                    Sheet.Cells["O" + row].Value = obj.causa_basica_muerte;
                                    Sheet.Cells["P" + row].Value = obj.fecha_salida;
                                    Sheet.Cells["P" + row].Value = obj.hora_salida;
                                }

                                break;
                            case "AN":
                                row++;
                                Sheet.Cells["A" + row + ":N" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":N" + row].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                Sheet.Cells["A" + row + ":N" + row].Style.Font.Bold = true;
                                Sheet.Cells["A" + row].Value = "Errores en el archivo " + array[i];
                                row++;

                                Sheet.Cells["A" + row + ":N" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":N" + row].Style.Fill.BackgroundColor.SetColor(colFromHex); ;
                                Sheet.Cells["A" + row + ":N" + row].Style.Font.Bold = true;
                                Sheet.Cells["A" + row].Value = "num_factura";
                                Sheet.Cells["B" + row].Value = "codigo_prestador";
                                Sheet.Cells["C" + row].Value = "tipo_id_madre";
                                Sheet.Cells["D" + row].Value = "num_id_madre";
                                Sheet.Cells["E" + row].Value = "fecha_nacimiento_rn";
                                Sheet.Cells["F" + row].Value = "hora_nacimiento";
                                Sheet.Cells["G" + row].Value = "edad_gestacional";
                                Sheet.Cells["H" + row].Value = "control_prenatal";
                                Sheet.Cells["I" + row].Value = "sexo  ";
                                Sheet.Cells["J" + row].Value = "peso";
                                Sheet.Cells["K" + row].Value = "dx_recien_nacido";
                                Sheet.Cells["L" + row].Value = "causa_muerte";
                                Sheet.Cells["M" + row].Value = "fecha_muerte";
                                Sheet.Cells["N" + row].Value = "hora_muerte";
                                foreach (var an in lista)
                                {
                                    row++;
                                    RIPS_AN obj = Model.GetRipsAnById(an.id_registro);
                                    Sheet.Cells["A" + row].Value = obj.num_factura;
                                    Sheet.Cells["B" + row].Value = obj.cod_prestador;
                                    Sheet.Cells["C" + row].Value = obj.tipo_id_madre;
                                    Sheet.Cells["D" + row].Value = obj.num_id_madre;
                                    Sheet.Cells["E" + row].Value = obj.fecha_nacimiento_rn;
                                    Sheet.Cells["F" + row].Value = obj.hora_nacimiento;
                                    Sheet.Cells["G" + row].Value = obj.edad_gestacional;
                                    Sheet.Cells["H" + row].Value = obj.control_prenatal;
                                    Sheet.Cells["I" + row].Value = obj.sexo;
                                    Sheet.Cells["J" + row].Value = obj.peso;
                                    Sheet.Cells["K" + row].Value = obj.dx_recien_nacido;
                                    Sheet.Cells["L" + row].Value = obj.causa_muerte;
                                    Sheet.Cells["M" + row].Value = obj.fecha_muerte;
                                    Sheet.Cells["N" + row].Value = obj.hora_muerte;
                                }
                                break;
                            case "AH":
                                row++;
                                Sheet.Cells["A" + row + ":S" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":S" + row].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                Sheet.Cells["A" + row].Value = "Errores en el archivo " + array[i];
                                row++;
                                Sheet.Cells["A" + row + ":P" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells["A" + row + ":P" + row].Style.Fill.BackgroundColor.SetColor(colFromHex); ;
                                Sheet.Cells["A" + row + ":P" + row].Style.Font.Bold = true;
                                Sheet.Cells["A" + row].Value = "num_factura";
                                Sheet.Cells["B" + row].Value = "codigo_prestador";
                                Sheet.Cells["C" + row].Value = "tipo_id_usuario";
                                Sheet.Cells["D" + row].Value = "num_id_usuario";
                                Sheet.Cells["E" + row].Value = "via_ingreso";
                                Sheet.Cells["F" + row].Value = "fecha_ingreso";
                                Sheet.Cells["G" + row].Value = "hora_ingreso";
                                Sheet.Cells["H" + row].Value = "num_autorizacion";
                                Sheet.Cells["I" + row].Value = "causa_externa";
                                Sheet.Cells["J" + row].Value = "dx_ppal_ingreso";
                                Sheet.Cells["K" + row].Value = "dx_ppal_egreso";
                                Sheet.Cells["L" + row].Value = "dx_rel_1_egreso";
                                Sheet.Cells["M" + row].Value = "dx_rel_2_egreso";
                                Sheet.Cells["N" + row].Value = "dx_rel_3_egreso";
                                Sheet.Cells["O" + row].Value = "dx_complicacion";
                                Sheet.Cells["P" + row].Value = "estado_salida";
                                Sheet.Cells["Q" + row].Value = "dx_causa_basica_muerte";
                                Sheet.Cells["R" + row].Value = "fecha_egreso";
                                Sheet.Cells["S" + row].Value = "hora_egreso";

                                foreach (var ah in lista)
                                {
                                    row++;
                                    RIPS_AH obj = Model.GetRipsAhById(ah.id_registro);
                                    Sheet.Cells["A" + row].Value = obj.num_factura;
                                    Sheet.Cells["B" + row].Value = obj.cod_prestador;
                                    Sheet.Cells["C" + row].Value = obj.tipo_id_usuario;
                                    Sheet.Cells["D" + row].Value = obj.num_id_usuario;
                                    Sheet.Cells["E" + row].Value = obj.via_ingreso;
                                    Sheet.Cells["F" + row].Value = obj.fecha_ingreso;
                                    Sheet.Cells["G" + row].Value = obj.hora_ingreso;
                                    Sheet.Cells["H" + row].Value = obj.num_autorizacion;
                                    Sheet.Cells["I" + row].Value = obj.causa_externa;
                                    Sheet.Cells["J" + row].Value = obj.dx_ppal_ingreso;
                                    Sheet.Cells["K" + row].Value = obj.dx_ppal_egreso;
                                    Sheet.Cells["L" + row].Value = obj.dx_rel_1_egreso;
                                    Sheet.Cells["M" + row].Value = obj.dx_rel_2_egreso;
                                    Sheet.Cells["N" + row].Value = obj.dx_rel_3_egreso;
                                    Sheet.Cells["O" + row].Value = obj.dx_complicacion;
                                    Sheet.Cells["P" + row].Value = obj.estado_salida;
                                    Sheet.Cells["Q" + row].Value = obj.dx_causa_basica_muerte;
                                    Sheet.Cells["R" + row].Value = obj.fecha_egreso;
                                    Sheet.Cells["S" + row].Value = obj.hora_egreso;
                                }
                                break;
                                #endregion
                        }
                        row += 2;
                    }
                }

                row += 2;
            }


            string namefile = "ReporteLogprueba";
            if (!string.IsNullOrEmpty(codigohabilitacion))
            {
                namefile = "LogErrores_" + codigohabilitacion;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            #endregion

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xls");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public void ExportaraexcelLogV2(int regional, int mes, int año)
        {
            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();
            List<RIPS> rips = Model.GetListaRipsPorMesYAño(mes, año, regional);
            string[] array = new string[5] { "AC", "AP", "AN", "AU", "AH" };

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet SheetAC = Ep.Workbook.Worksheets.Add("ErroresAc");
            ExcelWorksheet SheetAP = Ep.Workbook.Worksheets.Add("ErroresAp");
            ExcelWorksheet SheetAN = Ep.Workbook.Worksheets.Add("ErroresAn");
            ExcelWorksheet SheetAU = Ep.Workbook.Worksheets.Add("ErroresAu");
            ExcelWorksheet SheetAH = Ep.Workbook.Worksheets.Add("ErroresAh");

            System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(12, 64, 102);

            foreach (string archivo in array)
            {
                switch (archivo)
                {
                    case "AC":
                        #region Encabezados Ac

                        SheetAC.Cells["A1:V1"].Style.Font.Bold = true;
                        SheetAC.Cells["A1:V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        SheetAC.Cells["A1:V1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        SheetAC.Cells["A1:V1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        SheetAC.Cells["A1:V1"].Style.Font.Size = 12;

                        SheetAC.Cells["A1"].Value = "Codigo Prestador";
                        SheetAC.Cells["B1"].Value = "Razón Social";
                        SheetAC.Cells["C1"].Value = "Detalle Log";
                        SheetAC.Cells["D1"].Value = "num_factura";
                        SheetAC.Cells["E1"].Value = "codigo_prestador";
                        SheetAC.Cells["F1"].Value = "tipo_id_usuario";
                        SheetAC.Cells["G1"].Value = "num_id_usuario";
                        SheetAC.Cells["H1"].Value = "fecha_consulta";
                        SheetAC.Cells["I1"].Value = "num_autorizacion";
                        SheetAC.Cells["J1"].Value = "cod_consulta";
                        SheetAC.Cells["K1"].Value = "finalidad_consulta";
                        SheetAC.Cells["L1"].Value = "causa_externa  ";
                        SheetAC.Cells["M1"].Value = "cod_dx_ppal";
                        SheetAC.Cells["N1"].Value = "cod_dx_rel_1";
                        SheetAC.Cells["O1"].Value = "cod_dx_rel_2";
                        SheetAC.Cells["P1"].Value = "cod_dx_rel_3";
                        SheetAC.Cells["Q1"].Value = "tipo_dx_ppal";
                        SheetAC.Cells["R1"].Value = "valor_consulta";
                        SheetAC.Cells["S1"].Value = "valor_neto_a_pagar";
                        SheetAC.Cells["T1"].Value = "Total Errores";
                        SheetAC.Cells["U1"].Value = "Regional";
                        SheetAC.Cells["V1"].Value = "Mes y año";
                        #endregion
                        break;
                    case "AP":
                        #region Encabezados AP

                        SheetAP.Cells["A1:U1"].Style.Font.Bold = true;
                        SheetAP.Cells["A1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        SheetAP.Cells["A1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        SheetAP.Cells["A1:U1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        SheetAP.Cells["A1:U1"].Style.Font.Size = 12;

                        SheetAP.Cells["A1"].Value = "Codigo Prestador";
                        SheetAP.Cells["B1"].Value = "Razón Social";
                        SheetAP.Cells["C1"].Value = "Detalle Log";
                        SheetAP.Cells["D1"].Value = "num_factura";
                        SheetAP.Cells["E1"].Value = "codigo_prestador";
                        SheetAP.Cells["F1"].Value = "tipo_id_usuario";
                        SheetAP.Cells["G1"].Value = "num_id_usuario";
                        SheetAP.Cells["H1"].Value = "fecha_procedimiento";
                        SheetAP.Cells["I1"].Value = "num_autorizacion";
                        SheetAP.Cells["J1"].Value = "cod_procedimiento";
                        SheetAP.Cells["K1"].Value = "ambito_procedimiento";
                        SheetAP.Cells["L1"].Value = "finalidad_procedimiento  ";
                        SheetAP.Cells["M1"].Value = "personal_atiende";
                        SheetAP.Cells["N1"].Value = "dx_ppal";
                        SheetAP.Cells["O1"].Value = "dx_rel";
                        SheetAP.Cells["P1"].Value = "complicacion";
                        SheetAP.Cells["Q1"].Value = "forma_acto_quirurgico";
                        SheetAP.Cells["R1"].Value = "valor_procedimiento";
                        SheetAP.Cells["S1"].Value = "Total Errores";
                        SheetAP.Cells["T1"].Value = "Regional";
                        SheetAP.Cells["U1"].Value = "Mes y año";
                        #endregion
                        break;
                    case "AN":

                        SheetAN.Cells["A1:T1"].Style.Font.Bold = true;
                        SheetAN.Cells["A1:T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        SheetAN.Cells["A1:T1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        SheetAN.Cells["A1:T1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        SheetAN.Cells["A1:T1"].Style.Font.Size = 12;

                        SheetAN.Cells["A1"].Value = "Codigo Prestador";
                        SheetAN.Cells["B1"].Value = "Razón Social";
                        SheetAN.Cells["C1"].Value = "Detalle Log";
                        SheetAN.Cells["D1"].Value = "num_factura";
                        SheetAN.Cells["E1"].Value = "codigo_prestador";
                        SheetAN.Cells["F1"].Value = "tipo_id_madre";
                        SheetAN.Cells["G1"].Value = "num_id_madre";
                        SheetAN.Cells["H1"].Value = "fecha_nacimiento_rn";
                        SheetAN.Cells["I1"].Value = "hora_nacimiento";
                        SheetAN.Cells["J1"].Value = "edad_gestacional";
                        SheetAN.Cells["K1"].Value = "control_prenatal";
                        SheetAN.Cells["L1"].Value = "sexo  ";
                        SheetAN.Cells["M1"].Value = "peso";
                        SheetAN.Cells["N1"].Value = "dx_recien_nacido";
                        SheetAN.Cells["O1"].Value = "causa_muerte";
                        SheetAN.Cells["P1"].Value = "fecha_muerte";
                        SheetAN.Cells["Q1"].Value = "hora_muerte";
                        SheetAN.Cells["R1"].Value = "Total Errores";
                        SheetAN.Cells["S1"].Value = "Regional";
                        SheetAN.Cells["T1"].Value = "Mes y año";
                        break;
                    case "AU":
                        SheetAU.Cells["A1:W1"].Style.Font.Bold = true;
                        SheetAU.Cells["A1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        SheetAU.Cells["A1:W1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        SheetAU.Cells["A1:W1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        SheetAU.Cells["A1:W1"].Style.Font.Size = 12;

                        SheetAU.Cells["A1"].Value = "Codigo Prestador";
                        SheetAU.Cells["B1"].Value = "Razón Social";
                        SheetAU.Cells["C1"].Value = "Detalle Log";
                        SheetAU.Cells["D1"].Value = "num_factura";
                        SheetAU.Cells["E1"].Value = "codigo_prestador";
                        SheetAU.Cells["F1"].Value = "tipo_id_usuario";
                        SheetAU.Cells["G1"].Value = "num_id_usuario";
                        SheetAU.Cells["H1"].Value = "fecha_ingreso_observacion";
                        SheetAU.Cells["I1"].Value = "hora_ingreso_observacion";
                        SheetAU.Cells["J1"].Value = "num_autorizacion";
                        SheetAU.Cells["K1"].Value = "causa_externa";
                        SheetAU.Cells["L1"].Value = "dx_salida  ";
                        SheetAU.Cells["M1"].Value = "dx_rel_salida_1";
                        SheetAU.Cells["N1"].Value = "dx_rel_salida_2";
                        SheetAU.Cells["O1"].Value = "dx_rel_salida_3";
                        SheetAU.Cells["P1"].Value = "destino_usuario_salida";
                        SheetAU.Cells["Q1"].Value = "estado_usuario_salida";
                        SheetAU.Cells["R1"].Value = "causa_basica_muerte";
                        SheetAU.Cells["S1"].Value = "fecha_salida";
                        SheetAU.Cells["T1"].Value = "hora_salida";
                        SheetAU.Cells["U1"].Value = "Total Errores";
                        SheetAU.Cells["V1"].Value = "Regional";
                        SheetAU.Cells["W1"].Value = "Mes y año";

                        break;
                    case "AH":

                        SheetAH.Cells["A1:Y1"].Style.Font.Bold = true;
                        SheetAH.Cells["A1:Y1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        SheetAH.Cells["A1:Y1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        SheetAH.Cells["A1:Y1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        SheetAH.Cells["A1:Y1"].Style.Font.Size = 12;

                        SheetAH.Cells["A1"].Value = "Codigo Prestador";
                        SheetAH.Cells["B1"].Value = "Razón Social";
                        SheetAH.Cells["C1"].Value = "Detalle Log";
                        SheetAH.Cells["D1"].Value = "num_factura";
                        SheetAH.Cells["E1"].Value = "codigo_prestador";
                        SheetAH.Cells["F1"].Value = "tipo_id_usuario";
                        SheetAH.Cells["G1"].Value = "num_id_usuario";
                        SheetAH.Cells["H1"].Value = "via_ingreso";
                        SheetAH.Cells["I1"].Value = "fecha_ingreso";
                        SheetAH.Cells["J1"].Value = "hora_ingreso";
                        SheetAH.Cells["K1"].Value = "num_autorizacion";
                        SheetAH.Cells["L1"].Value = "causa_externa";
                        SheetAH.Cells["M1"].Value = "dx_ppal_ingreso";
                        SheetAH.Cells["N1"].Value = "dx_ppal_egreso";
                        SheetAH.Cells["O1"].Value = "dx_rel_1_egreso";
                        SheetAH.Cells["P1"].Value = "dx_rel_2_egreso";
                        SheetAH.Cells["Q1"].Value = "dx_rel_3_egreso";
                        SheetAH.Cells["R1"].Value = "dx_complicacion";
                        SheetAH.Cells["S1"].Value = "estado_salida";
                        SheetAH.Cells["T1"].Value = "dx_causa_basica_muerte";
                        SheetAH.Cells["U1"].Value = "fecha_egreso";
                        SheetAH.Cells["V1"].Value = "hora_egreso";
                        SheetAH.Cells["W1"].Value = "Total Errores";
                        SheetAH.Cells["X1"].Value = "Regional";
                        SheetAH.Cells["Y1"].Value = "Mes y año";
                        break;
                    default:
                        break;
                }
            }

            foreach (RIPS item in rips)
            {

                List<RIPS_AC_HISTORICO> listac = BusClass.GetRipsAcHistoricoById(item.id_rips, 2);
                List<RIPS_AP_HISTORICO> listap = BusClass.GetRipsApHistoricoById(item.id_rips, 2);
                List<RIPS_AN_HISTORICO> listan = BusClass.GetRipsAnHistoricoById(item.id_rips, 2);
                List<RIPS_AH_HISTORICO> listah = BusClass.GetRipsAhHistoricoById(item.id_rips, 2);
                List<RIPS_AU_HISTORICO> listau = BusClass.GetRipsAuHistoricoById(item.id_rips, 2);
                List<Logerroresevaluacionrips> reportelog = Model.GetLogEvaluacionRips(item.id_rips);

                Ref_regional regionals = BusClass.GetRefRegion().Where(l => l.id_ref_regional == item.id_regional).FirstOrDefault();

                int rowac = 2, rowap = 2, rowan = 2, rowau = 2, rowah = 2;

                foreach (Logerroresevaluacionrips item2 in reportelog)
                {
                    List<Logerroresevaluacionrips> correspondientes = reportelog.Where(l => l.codigo_prestador == item2.codigo_prestador).ToList();

                    for (int i = 0; i < array.Length; i++)
                    {
                        switch (array[i])
                        {
                            #region case
                            case "AC":

                                List<RIPS_AC_HISTORICO> list_ac = listac.Where(l => l.codigo_prestador == item2.codigo_prestador).ToList();
                                if (list_ac.Count > 0)
                                {
                                    LogErroresevaluacionRipsAC logac = Model.ConstruirDetalleLogAC(correspondientes);

                                    foreach (var obj in list_ac)
                                    {
                                        SheetAC.Cells["A" + rowac].Value = item2.codigo_prestador;
                                        SheetAC.Cells["B" + rowac].Value = item2.prestador;
                                        SheetAC.Cells["C" + rowac].Value = logac.detallelog;
                                        SheetAC.Cells["D" + rowac].Value = obj.num_factura;
                                        SheetAC.Cells["E" + rowac].Value = obj.codigo_prestador;
                                        SheetAC.Cells["F" + rowac].Value = obj.tipo_id_usuario;
                                        SheetAC.Cells["G" + rowac].Value = obj.num_id_usuario;
                                        if (obj.fecha_consulta != null)
                                            SheetAC.Cells["H" + rowac].Value = obj.fecha_consulta.Value.ToString("MM/dd/yyyy"); ;
                                        SheetAC.Cells["I" + rowac].Value = obj.num_autorizacion;
                                        SheetAC.Cells["J" + rowac].Value = obj.cod_consulta;
                                        SheetAC.Cells["K" + rowac].Value = obj.finalidad_consulta;
                                        SheetAC.Cells["L" + rowac].Value = obj.causa_externa;
                                        SheetAC.Cells["M" + rowac].Value = obj.cod_dx_ppal;
                                        SheetAC.Cells["N" + rowac].Value = obj.cod_dx_rel_1;
                                        SheetAC.Cells["O" + rowac].Value = obj.cod_dx_rel_2;
                                        SheetAC.Cells["P" + rowac].Value = obj.cod_dx_rel_3;
                                        SheetAC.Cells["Q" + rowac].Value = obj.tipo_dx_ppal;
                                        SheetAC.Cells["R" + rowac].Value = obj.valor_consulta;
                                        SheetAC.Cells["S" + rowac].Value = obj.valor_neto_a_pagar;
                                        SheetAC.Cells["T" + rowac].Value = logac.numerrores;
                                        SheetAC.Cells["U" + rowac].Value = regionals.indice;
                                        SheetAC.Cells["V" + rowac].Value = item.mes + "/" + item.año;
                                        rowac++;
                                    }
                                }

                                break;
                            case "AP":
                                List<RIPS_AP_HISTORICO> list_ap = listap.Where(l => l.cod_prestador == item2.codigo_prestador).ToList();
                                if (list_ap.Count > 0)
                                {
                                    LogErroresevaluacionRipsAP logap = Model.ConstruirDetalleLogAP(correspondientes);
                                    foreach (var obj in list_ap)
                                    {
                                        SheetAP.Cells["A" + rowap].Value = item2.codigo_prestador;
                                        SheetAP.Cells["B" + rowap].Value = item2.prestador;
                                        SheetAP.Cells["C" + rowap].Value = logap.detallelog;
                                        SheetAP.Cells["D" + rowap].Value = obj.num_factura;
                                        SheetAP.Cells["E" + rowap].Value = obj.cod_prestador;
                                        SheetAP.Cells["F" + rowap].Value = obj.tipo_id_usuario;
                                        SheetAP.Cells["G" + rowap].Value = obj.num_id_usuario;
                                        SheetAP.Cells["H" + rowap].Value = obj.fecha_procedimiento.Value.ToString("MM/dd/yyyy");
                                        SheetAP.Cells["I" + rowap].Value = obj.num_autorizacion;
                                        SheetAP.Cells["J" + rowap].Value = obj.cod_procedimiento;
                                        SheetAP.Cells["K" + rowap].Value = obj.ambito_procedimiento;
                                        SheetAP.Cells["L" + rowap].Value = obj.finalidad_procedimiento;
                                        SheetAP.Cells["M" + rowap].Value = obj.personal_atiende;
                                        SheetAP.Cells["N" + rowap].Value = obj.dx_ppal;
                                        SheetAP.Cells["O" + rowap].Value = obj.dx_rel;
                                        SheetAP.Cells["P" + rowap].Value = obj.complicacion;
                                        SheetAP.Cells["Q" + rowap].Value = obj.forma_acto_quirurgico;
                                        SheetAP.Cells["R" + rowap].Value = obj.valor_procedimiento;
                                        SheetAP.Cells["S" + rowap].Value = logap.numerrores;
                                        SheetAP.Cells["T" + rowap].Value = regionals.indice;
                                        SheetAP.Cells["U" + rowap].Value = item.mes + "/" + item.año;
                                        rowap++;
                                    }
                                }
                                break;
                            case "AU":

                                List<RIPS_AU_HISTORICO> list_au = listau.Where(l => l.cod_prestador == item2.codigo_prestador).ToList();
                                if (list_au.Count > 0)
                                {
                                    LogErroresevaluacionRipsAU logau = Model.ConstruirDetalleLogAU(correspondientes);
                                    foreach (var obj in list_au)
                                    {
                                        SheetAU.Cells["A" + rowau].Value = item2.codigo_prestador;
                                        SheetAU.Cells["B" + rowau].Value = item2.prestador;
                                        SheetAU.Cells["C" + rowau].Value = logau.detallelog;
                                        SheetAU.Cells["D" + rowau].Value = obj.num_factura;
                                        SheetAU.Cells["E" + rowau].Value = obj.cod_prestador;
                                        SheetAU.Cells["F" + rowau].Value = obj.tipo_id_usuario;
                                        SheetAU.Cells["G" + rowau].Value = obj.num_id_usuario;
                                        SheetAU.Cells["H" + rowau].Value = obj.fecha_ingreso_observacion;
                                        SheetAU.Cells["I" + rowau].Value = obj.hora_ingreso_observacion;
                                        SheetAU.Cells["J" + rowau].Value = obj.num_autorizacion;
                                        SheetAU.Cells["K" + rowau].Value = obj.causa_externa;
                                        SheetAU.Cells["L" + rowau].Value = obj.dx_salida;
                                        SheetAU.Cells["M" + rowau].Value = obj.dx_rel_salida_1;
                                        SheetAU.Cells["N" + rowau].Value = obj.dx_rel_salida_2;
                                        SheetAU.Cells["O" + rowau].Value = obj.dx_rel_salida_3;
                                        SheetAU.Cells["P" + rowau].Value = obj.destino_usuario_salida;
                                        SheetAU.Cells["Q" + rowau].Value = obj.estado_usuario_salida;
                                        SheetAU.Cells["R" + rowau].Value = obj.causa_basica_muerte;
                                        SheetAU.Cells["S" + rowau].Value = obj.fecha_salida;
                                        SheetAU.Cells["T" + rowau].Value = obj.hora_salida;
                                        SheetAU.Cells["U" + rowau].Value = logau.numerrores;
                                        SheetAU.Cells["V" + rowau].Value = regionals.indice;
                                        SheetAU.Cells["W" + rowau].Value = item.mes + "/" + item.año;
                                        rowau++;
                                    }
                                }


                                break;
                            case "AN":
                                List<RIPS_AN_HISTORICO> list_an = listan.Where(l => l.cod_prestador == item2.codigo_prestador).ToList();
                                if (list_an.Count > 0)
                                {
                                    LogErroresevaluacionRipsAN logan = Model.ConstruirDetalleLogAN(correspondientes);
                                    foreach (var obj in list_an)
                                    {
                                        SheetAN.Cells["A" + rowan].Value = item2.codigo_prestador;
                                        SheetAN.Cells["B" + rowan].Value = item2.prestador;
                                        SheetAN.Cells["C" + rowan].Value = logan.detallelog;
                                        SheetAN.Cells["D" + rowan].Value = obj.num_factura;
                                        SheetAN.Cells["E" + rowan].Value = obj.cod_prestador;
                                        SheetAN.Cells["F" + rowan].Value = obj.tipo_id_madre;
                                        SheetAN.Cells["G" + rowan].Value = obj.num_id_madre;
                                        SheetAN.Cells["H" + rowan].Value = obj.fecha_nacimiento_rn;
                                        SheetAN.Cells["I" + rowan].Value = obj.hora_nacimiento;
                                        SheetAN.Cells["J" + rowan].Value = obj.edad_gestacional;
                                        SheetAN.Cells["K" + rowan].Value = obj.control_prenatal;
                                        SheetAN.Cells["L" + rowan].Value = obj.sexo;
                                        SheetAN.Cells["M" + rowan].Value = obj.peso;
                                        SheetAN.Cells["N" + rowan].Value = obj.dx_recien_nacido;
                                        SheetAN.Cells["O" + rowan].Value = obj.causa_muerte;
                                        SheetAN.Cells["P" + rowan].Value = obj.fecha_muerte;
                                        SheetAN.Cells["Q" + rowan].Value = obj.hora_muerte;
                                        SheetAN.Cells["R" + rowan].Value = logan.numerrores;
                                        SheetAN.Cells["S" + rowan].Value = regionals.indice;
                                        SheetAN.Cells["T" + rowan].Value = item.mes + "/" + item.año;
                                        rowan++;
                                    }
                                }

                                break;
                            case "AH":
                                List<RIPS_AH_HISTORICO> list_ah = listah.Where(l => l.cod_prestador == item2.codigo_prestador).ToList();
                                if (list_ah.Count > 0)
                                {
                                    LogErroresevaluacionRipsAH logah = Model.ConstruirDetalleLogAH(correspondientes);
                                    foreach (var obj in list_ah)
                                    {
                                        SheetAH.Cells["A" + rowah].Value = item2.codigo_prestador;
                                        SheetAH.Cells["B" + rowah].Value = item2.prestador;
                                        SheetAH.Cells["C" + rowah].Value = logah.detallelog;
                                        SheetAH.Cells["D" + rowah].Value = obj.num_factura;
                                        SheetAH.Cells["E" + rowah].Value = obj.cod_prestador;
                                        SheetAH.Cells["F" + rowah].Value = obj.tipo_id_usuario;
                                        SheetAH.Cells["G" + rowah].Value = obj.num_id_usuario;
                                        SheetAH.Cells["H" + rowah].Value = obj.via_ingreso;
                                        SheetAH.Cells["I" + rowah].Value = obj.fecha_ingreso;
                                        SheetAH.Cells["J" + rowah].Value = obj.hora_ingreso;
                                        SheetAH.Cells["K" + rowah].Value = obj.num_autorizacion;
                                        SheetAH.Cells["L" + rowah].Value = obj.causa_externa;
                                        SheetAH.Cells["M" + rowah].Value = obj.dx_ppal_ingreso;
                                        SheetAH.Cells["N" + rowah].Value = obj.dx_ppal_egreso;
                                        SheetAH.Cells["O" + rowah].Value = obj.dx_rel_1_egreso;
                                        SheetAH.Cells["P" + rowah].Value = obj.dx_rel_2_egreso;
                                        SheetAH.Cells["Q" + rowah].Value = obj.dx_rel_3_egreso;
                                        SheetAH.Cells["R" + rowah].Value = obj.dx_complicacion;
                                        SheetAH.Cells["S" + rowah].Value = obj.estado_salida;
                                        SheetAH.Cells["T" + rowah].Value = obj.dx_causa_basica_muerte;
                                        SheetAH.Cells["U" + rowah].Value = obj.fecha_egreso;
                                        SheetAH.Cells["V" + rowah].Value = obj.hora_egreso;
                                        SheetAH.Cells["W" + rowah].Value = logah.numerrores;
                                        SheetAH.Cells["X" + rowah].Value = regionals.indice;
                                        SheetAH.Cells["Y" + rowah].Value = item.mes + "/" + item.año;
                                    }
                                }

                                break;
                                #endregion
                        }
                    }
                }
            }

            string namefile = "ReporteLogErrores" + mes + "_" + año;
            SheetAC.Cells["A:AZ"].AutoFitColumns();
            SheetAP.Cells["A:U"].AutoFitColumns();
            SheetAN.Cells["A:T"].AutoFitColumns();
            SheetAU.Cells["A:W"].AutoFitColumns();
            SheetAH.Cells["A:Y"].AutoFitColumns();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public ActionResult ConsultarRipsCorrectos()
        {
            ViewBag.listaRegionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            return View();
        }

        public void ExportaraexcelLogCorrectos(int? regional, int mes, int año)
        {
            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();
            try
            {

                List<RIPS> rips = Model.GetListaRipsPorMesYAño(mes, año, regional);
                Ref_regional regionals = BusClass.GetRefRegion().Where(l => l.id_ref_regional == regional.Value).FirstOrDefault();
                if (rips.Count == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('No se han encontrado resultados');" +
                    "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                string[] array = new string[7] { "AC", "AP", "AN", "AU", "AH", "AF", "US" };

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet SheetAC = Ep.Workbook.Worksheets.Add("AC");
                ExcelWorksheet SheetAP = Ep.Workbook.Worksheets.Add("AP");
                ExcelWorksheet SheetAN = Ep.Workbook.Worksheets.Add("AN");
                ExcelWorksheet SheetAU = Ep.Workbook.Worksheets.Add("AU");
                ExcelWorksheet SheetAH = Ep.Workbook.Worksheets.Add("AH");
                ExcelWorksheet SheetAF = Ep.Workbook.Worksheets.Add("AF");
                ExcelWorksheet SheetUS = Ep.Workbook.Worksheets.Add("US");
                System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(22, 54, 92);

                foreach (string archivo in array)
                {
                    switch (archivo)
                    {
                        case "AC":
                            #region Encabezados Ac

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
                            #endregion
                            break;
                        case "AP":
                            #region Encabezados AP
                            SheetAP.Cells["A1:Q1"].Style.Font.Bold = true;
                            SheetAP.Cells["A1:Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAP.Cells["A1:Q1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAP.Cells["A1:Q1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            SheetAP.Cells["A1"].Value = "num_factura";
                            SheetAP.Cells["B1"].Value = "codigo_prestador";
                            SheetAP.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAP.Cells["D1"].Value = "num_id_usuario";
                            SheetAP.Cells["E1"].Value = "fecha_procedimiento";
                            SheetAP.Cells["F1"].Value = "num_autorizacion";
                            SheetAP.Cells["G1"].Value = "cod_procedimiento";
                            SheetAP.Cells["H1"].Value = "ambito_procedimiento";
                            SheetAP.Cells["I1"].Value = "finalidad_procedimiento  ";
                            SheetAP.Cells["J1"].Value = "personal_atiende";
                            SheetAP.Cells["K1"].Value = "dx_ppal";
                            SheetAP.Cells["L1"].Value = "dx_rel";
                            SheetAP.Cells["M1"].Value = "complicacion";
                            SheetAP.Cells["N1"].Value = "forma_acto_quirurgico";
                            SheetAP.Cells["O1"].Value = "valor_procedimiento";
                            SheetAP.Cells["P1"].Value = "Regional";
                            SheetAP.Cells["Q1"].Value = "Mes y año";
                            #endregion
                            break;
                        case "AN":
                            SheetAN.Cells["A1:P1"].Style.Font.Bold = true;
                            SheetAN.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAN.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAN.Cells["A1:P1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAN.Cells["A1"].Value = "num_factura";
                            SheetAN.Cells["B1"].Value = "codigo_prestador";
                            SheetAN.Cells["C1"].Value = "tipo_id_madre";
                            SheetAN.Cells["D1"].Value = "num_id_madre";
                            SheetAN.Cells["E1"].Value = "fecha_nacimiento_rn";
                            SheetAN.Cells["F1"].Value = "hora_nacimiento";
                            SheetAN.Cells["G1"].Value = "edad_gestacional";
                            SheetAN.Cells["H1"].Value = "control_prenatal";
                            SheetAN.Cells["I1"].Value = "sexo  ";
                            SheetAN.Cells["J1"].Value = "peso";
                            SheetAN.Cells["K1"].Value = "dx_recien_nacido";
                            SheetAN.Cells["L1"].Value = "causa_muerte";
                            SheetAN.Cells["M1"].Value = "fecha_muerte";
                            SheetAN.Cells["N1"].Value = "hora_muerte";
                            SheetAN.Cells["O1"].Value = "Regional";
                            SheetAN.Cells["P1"].Value = "Mes y año";
                            break;
                        case "AU":
                            SheetAU.Cells["A1:S1"].Style.Font.Bold = true;
                            SheetAU.Cells["A1:S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAU.Cells["A1:S1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAU.Cells["A1:S1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAU.Cells["A1"].Value = "num_factura";
                            SheetAU.Cells["B1"].Value = "codigo_prestador";
                            SheetAU.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAU.Cells["D1"].Value = "num_id_usuario";
                            SheetAU.Cells["E1"].Value = "fecha_ingreso_observacion";
                            SheetAU.Cells["F1"].Value = "hora_ingreso_observacion";
                            SheetAU.Cells["G1"].Value = "num_autorizacion";
                            SheetAU.Cells["H1"].Value = "causa_externa";
                            SheetAU.Cells["I1"].Value = "dx_salida  ";
                            SheetAU.Cells["J1"].Value = "dx_rel_salida_1";
                            SheetAU.Cells["K1"].Value = "dx_rel_salida_2";
                            SheetAU.Cells["L1"].Value = "dx_rel_salida_3";
                            SheetAU.Cells["M1"].Value = "destino_usuario_salida";
                            SheetAU.Cells["N1"].Value = "estado_usuario_salida";
                            SheetAU.Cells["O1"].Value = "causa_basica_muerte";
                            SheetAU.Cells["P1"].Value = "fecha_salida";
                            SheetAU.Cells["Q1"].Value = "hora_salida";
                            SheetAU.Cells["R1"].Value = "Regional";
                            SheetAU.Cells["S1"].Value = "Mes y año";

                            break;
                        case "AH":
                            SheetAH.Cells["A1:U1"].Style.Font.Bold = true;
                            SheetAH.Cells["A1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAH.Cells["A1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAH.Cells["A1:U1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAH.Cells["A1"].Value = "num_factura";
                            SheetAH.Cells["B1"].Value = "codigo_prestador";
                            SheetAH.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAH.Cells["D1"].Value = "num_id_usuario";
                            SheetAH.Cells["E1"].Value = "via_ingreso";
                            SheetAH.Cells["F1"].Value = "fecha_ingreso";
                            SheetAH.Cells["G1"].Value = "hora_ingreso";
                            SheetAH.Cells["H1"].Value = "num_autorizacion";
                            SheetAH.Cells["I1"].Value = "causa_externa";
                            SheetAH.Cells["J1"].Value = "dx_ppal_ingreso";
                            SheetAH.Cells["K1"].Value = "dx_ppal_egreso";
                            SheetAH.Cells["L1"].Value = "dx_rel_1_egreso";
                            SheetAH.Cells["M1"].Value = "dx_rel_2_egreso";
                            SheetAH.Cells["N1"].Value = "dx_rel_3_egreso";
                            SheetAH.Cells["O1"].Value = "dx_complicacion";
                            SheetAH.Cells["P1"].Value = "estado_salida";
                            SheetAH.Cells["Q1"].Value = "dx_causa_basica_muerte";
                            SheetAH.Cells["R1"].Value = "fecha_egreso";
                            SheetAH.Cells["S1"].Value = "hora_egreso";
                            SheetAH.Cells["T1"].Value = "Regional";
                            SheetAH.Cells["U1"].Value = "Mes y año";
                            break;
                        case "AF":
                            SheetAF.Cells["A1:S1"].Style.Font.Bold = true;
                            SheetAF.Cells["A1:S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAF.Cells["A1:S1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAF.Cells["A1:S1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAF.Cells["A1"].Value = "codigo prestador";
                            SheetAF.Cells["B1"].Value = "nombre prestador";
                            SheetAF.Cells["C1"].Value = "tipo id_prestador";
                            SheetAF.Cells["D1"].Value = "num id_prestador";
                            SheetAF.Cells["E1"].Value = "num factura";
                            SheetAF.Cells["F1"].Value = "fecha exp_factura";
                            SheetAF.Cells["G1"].Value = "fecha inicio";
                            SheetAF.Cells["H1"].Value = "fecha final";
                            SheetAF.Cells["I1"].Value = "cod entidad_adm";
                            SheetAF.Cells["J1"].Value = "nom entidad_adm";
                            SheetAF.Cells["K1"].Value = "num contrato";
                            SheetAF.Cells["L1"].Value = "plan beneficios";
                            SheetAF.Cells["M1"].Value = "num poliza";
                            SheetAF.Cells["N1"].Value = "copago";
                            SheetAF.Cells["O1"].Value = "valor_comision";
                            SheetAF.Cells["P1"].Value = "valor_descuentos";
                            SheetAF.Cells["Q1"].Value = "valor_neto";
                            SheetAF.Cells["R1"].Value = "Regional";
                            SheetAF.Cells["S1"].Value = "Mes y año";
                            break;
                        case "US":
                            SheetUS.Cells["A1:P1"].Style.Font.Bold = true;
                            SheetUS.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetUS.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetUS.Cells["A1:P1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetUS.Cells["A1"].Value = "tipo_id_usuario";
                            SheetUS.Cells["B1"].Value = "num_id_usuario";
                            SheetUS.Cells["C1"].Value = "cod_entidad_adm";
                            SheetUS.Cells["D1"].Value = "tipo_usuario";
                            SheetUS.Cells["E1"].Value = "primer_apellido";
                            SheetUS.Cells["F1"].Value = "segundo_apellido";
                            SheetUS.Cells["G1"].Value = "primer_nombre";
                            SheetUS.Cells["H1"].Value = "segundo_nombre";
                            SheetUS.Cells["I1"].Value = "edad";
                            SheetUS.Cells["J1"].Value = "unidad_medida_edad";
                            SheetUS.Cells["K1"].Value = "sexo";
                            SheetUS.Cells["L1"].Value = "cod_departamento_residencia";
                            SheetUS.Cells["M1"].Value = "cod_municipio_residencia";
                            SheetUS.Cells["N1"].Value = "zona_residencia";
                            SheetUS.Cells["O1"].Value = "Regional";
                            SheetUS.Cells["P1"].Value = "Mes y año";
                            break;
                        default:
                            break;
                    }
                }

                int rowac = 2, rowap = 2, rowan = 2, rowau = 2, rowah = 2, rowaf = 2, rowus = 2;
                foreach (RIPS item in rips)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        switch (array[i])
                        {
                            #region case
                            case "AC":

                                List<RIPS_AC_HISTORICO> list = BusClass.GetRipsAcHistoricoById(item.id_rips, 1);
                                foreach (var obj in list)
                                {
                                    SheetAC.Cells["A" + rowac].Value = obj.num_factura;
                                    SheetAC.Cells["B" + rowac].Value = obj.codigo_prestador;
                                    SheetAC.Cells["C" + rowac].Value = obj.tipo_id_usuario;
                                    SheetAC.Cells["D" + rowac].Value = obj.num_id_usuario;
                                    SheetAC.Cells["E" + rowac].Value = obj.fecha_consulta.Value.ToString("MM/dd/yyyy");
                                    SheetAC.Cells["F" + rowac].Value = obj.num_autorizacion;
                                    SheetAC.Cells["G" + rowac].Value = obj.cod_consulta;
                                    SheetAC.Cells["H" + rowac].Value = obj.finalidad_consulta;
                                    SheetAC.Cells["I" + rowac].Value = obj.causa_externa;
                                    SheetAC.Cells["J" + rowac].Value = obj.cod_dx_ppal;
                                    SheetAC.Cells["K" + rowac].Value = obj.cod_dx_rel_1;
                                    SheetAC.Cells["L" + rowac].Value = obj.cod_dx_rel_2;
                                    SheetAC.Cells["M" + rowac].Value = obj.cod_dx_rel_3;
                                    SheetAC.Cells["N" + rowac].Value = obj.tipo_dx_ppal;
                                    SheetAC.Cells["O" + rowac].Value = obj.valor_consulta;
                                    SheetAC.Cells["P" + rowac].Value = obj.valor_neto_a_pagar;
                                    SheetAC.Cells["Q" + rowac].Value = regionals.indice;
                                    SheetAC.Cells["R" + rowac].Value = item.mes + "/" + item.año;
                                    rowac++;
                                }
                                break;
                            case "AP":
                                List<RIPS_AP_HISTORICO> list2 = BusClass.GetRipsApHistoricoById(item.id_rips, 1);
                                foreach (var obj in list2)
                                {
                                    SheetAP.Cells["A" + rowap].Value = obj.num_factura;
                                    SheetAP.Cells["B" + rowap].Value = obj.cod_prestador;
                                    SheetAP.Cells["C" + rowap].Value = obj.tipo_id_usuario;
                                    SheetAP.Cells["D" + rowap].Value = obj.num_id_usuario;
                                    SheetAP.Cells["E" + rowap].Value = obj.fecha_procedimiento.Value.ToString("MM/dd/yyyy");
                                    SheetAP.Cells["F" + rowap].Value = obj.num_autorizacion;
                                    SheetAP.Cells["G" + rowap].Value = obj.cod_procedimiento;
                                    SheetAP.Cells["H" + rowap].Value = obj.ambito_procedimiento;
                                    SheetAP.Cells["I" + rowap].Value = obj.finalidad_procedimiento;
                                    SheetAP.Cells["J" + rowap].Value = obj.personal_atiende;
                                    SheetAP.Cells["K" + rowap].Value = obj.dx_ppal;
                                    SheetAP.Cells["L" + rowap].Value = obj.dx_rel;
                                    SheetAP.Cells["M" + rowap].Value = obj.complicacion;
                                    SheetAP.Cells["N" + rowap].Value = obj.forma_acto_quirurgico;
                                    SheetAP.Cells["O" + rowap].Value = obj.valor_procedimiento;
                                    SheetAP.Cells["P" + rowap].Value = regionals.indice;
                                    SheetAP.Cells["Q" + rowap].Value = item.mes + "/" + item.año;
                                    rowap++;
                                }


                                break;
                            case "AU":
                                List<RIPS_AU_HISTORICO> list3 = BusClass.GetRipsAuHistoricoById(item.id_rips, 1);
                                foreach (var obj in list3)
                                {
                                    SheetAU.Cells["A" + rowau].Value = obj.num_factura;
                                    SheetAU.Cells["B" + rowau].Value = obj.cod_prestador;
                                    SheetAU.Cells["C" + rowau].Value = obj.tipo_id_usuario;
                                    SheetAU.Cells["D" + rowau].Value = obj.num_id_usuario;
                                    SheetAU.Cells["E" + rowau].Value = obj.fecha_ingreso_observacion;
                                    SheetAU.Cells["F" + rowau].Value = obj.hora_ingreso_observacion;
                                    SheetAU.Cells["G" + rowau].Value = obj.num_autorizacion;
                                    SheetAU.Cells["H" + rowau].Value = obj.causa_externa;
                                    SheetAU.Cells["I" + rowau].Value = obj.dx_salida;
                                    SheetAU.Cells["J" + rowau].Value = obj.dx_rel_salida_1;
                                    SheetAU.Cells["K" + rowau].Value = obj.dx_rel_salida_2;
                                    SheetAU.Cells["L" + rowau].Value = obj.dx_rel_salida_3;
                                    SheetAU.Cells["M" + rowau].Value = obj.destino_usuario_salida;
                                    SheetAU.Cells["N" + rowau].Value = obj.estado_usuario_salida;
                                    SheetAU.Cells["O" + rowau].Value = obj.causa_basica_muerte;
                                    SheetAU.Cells["P" + rowau].Value = obj.fecha_salida;
                                    SheetAU.Cells["Q" + rowau].Value = obj.hora_salida;
                                    SheetAU.Cells["R" + rowau].Value = regionals.indice;
                                    SheetAU.Cells["S" + rowau].Value = item.mes + "/" + item.año;
                                    rowau++;
                                }

                                break;
                            case "AN":
                                List<RIPS_AN_HISTORICO> list4 = BusClass.GetRipsAnHistoricoById(item.id_rips, 1);
                                foreach (var obj in list4)
                                {
                                    SheetAN.Cells["A" + rowan].Value = obj.num_factura;
                                    SheetAN.Cells["B" + rowan].Value = obj.cod_prestador;
                                    SheetAN.Cells["C" + rowan].Value = obj.tipo_id_madre;
                                    SheetAN.Cells["D" + rowan].Value = obj.num_id_madre;
                                    SheetAN.Cells["E" + rowan].Value = obj.fecha_nacimiento_rn;
                                    SheetAN.Cells["F" + rowan].Value = obj.hora_nacimiento;
                                    SheetAN.Cells["G" + rowan].Value = obj.edad_gestacional;
                                    SheetAN.Cells["H" + rowan].Value = obj.control_prenatal;
                                    SheetAN.Cells["I" + rowan].Value = obj.sexo;
                                    SheetAN.Cells["J" + rowan].Value = obj.peso;
                                    SheetAN.Cells["K" + rowan].Value = obj.dx_recien_nacido;
                                    SheetAN.Cells["L" + rowan].Value = obj.causa_muerte;
                                    SheetAN.Cells["M" + rowan].Value = obj.fecha_muerte;
                                    SheetAN.Cells["N" + rowan].Value = obj.hora_muerte;
                                    SheetAN.Cells["O" + rowan].Value = regionals.indice;
                                    SheetAN.Cells["P" + rowan].Value = item.mes + "/" + item.año;

                                    rowan++;
                                }
                                break;
                            case "AH":
                                List<RIPS_AH_HISTORICO> list5 = BusClass.GetRipsAhHistoricoById(item.id_rips, 1);
                                foreach (var obj in list5)
                                {
                                    SheetAH.Cells["A" + rowah].Value = obj.num_factura;
                                    SheetAH.Cells["B" + rowah].Value = obj.cod_prestador;
                                    SheetAH.Cells["C" + rowah].Value = obj.tipo_id_usuario;
                                    SheetAH.Cells["D" + rowah].Value = obj.num_id_usuario;
                                    SheetAH.Cells["E" + rowah].Value = obj.via_ingreso;
                                    SheetAH.Cells["F" + rowah].Value = obj.fecha_ingreso;
                                    SheetAH.Cells["G" + rowah].Value = obj.hora_ingreso;
                                    SheetAH.Cells["H" + rowah].Value = obj.num_autorizacion;
                                    SheetAH.Cells["I" + rowah].Value = obj.causa_externa;
                                    SheetAH.Cells["J" + rowah].Value = obj.dx_ppal_ingreso;
                                    SheetAH.Cells["K" + rowah].Value = obj.dx_ppal_egreso;
                                    SheetAH.Cells["L" + rowah].Value = obj.dx_rel_1_egreso;
                                    SheetAH.Cells["M" + rowah].Value = obj.dx_rel_2_egreso;
                                    SheetAH.Cells["N" + rowah].Value = obj.dx_rel_3_egreso;
                                    SheetAH.Cells["O" + rowah].Value = obj.dx_complicacion;
                                    SheetAH.Cells["P" + rowah].Value = obj.estado_salida;
                                    SheetAH.Cells["Q" + rowah].Value = obj.dx_causa_basica_muerte;
                                    SheetAH.Cells["R" + rowah].Value = obj.fecha_egreso;
                                    SheetAH.Cells["S" + rowah].Value = obj.hora_egreso;
                                    SheetAH.Cells["T" + rowah].Value = regionals.indice;
                                    SheetAH.Cells["U" + rowah].Value = item.mes + "/" + item.año;
                                    rowah++;
                                }
                                break;
                            case "AF":
                                List<RIPS_AF_HISTORICO> list6 = BusClass.GetRipsAfHistoricoById(item.id_rips);
                                foreach (var obj in list6)
                                {
                                    SheetAF.Cells["A" + rowaf].Value = obj.codigo_prestador;
                                    SheetAF.Cells["B" + rowaf].Value = obj.nombre_prestador;
                                    SheetAF.Cells["C" + rowaf].Value = obj.tipo_id_prestador;
                                    SheetAF.Cells["D" + rowaf].Value = obj.num_id_prestador;
                                    SheetAF.Cells["E" + rowaf].Value = obj.num_factura;
                                    SheetAF.Cells["F" + rowaf].Value = obj.fecha_exp_factura;
                                    SheetAF.Cells["G" + rowaf].Value = obj.fecha_inicio;
                                    SheetAF.Cells["H" + rowaf].Value = obj.fecha_final;
                                    SheetAF.Cells["I" + rowaf].Value = obj.cod_entidad_adm;
                                    SheetAF.Cells["J" + rowaf].Value = obj.nom_entidad_adm;
                                    SheetAF.Cells["K" + rowaf].Value = obj.num_contrato;
                                    SheetAF.Cells["L" + rowaf].Value = obj.plan_beneficios;
                                    SheetAF.Cells["M" + rowaf].Value = obj.num_poliza;
                                    SheetAF.Cells["N" + rowaf].Value = obj.copago;
                                    SheetAF.Cells["O" + rowaf].Value = obj.valor_comision;
                                    SheetAF.Cells["P" + rowaf].Value = obj.valor_descuentos;
                                    SheetAF.Cells["Q" + rowaf].Value = obj.valor_neto;
                                    SheetAF.Cells["R" + rowaf].Value = regionals.indice;
                                    SheetAF.Cells["S" + rowaf].Value = item.mes + "/" + item.año;
                                    rowaf++;
                                }
                                break;
                            case "US":
                                List<RIPS_US_HISTORICO> list7 = BusClass.GetRipsUsHistoricoById(item.id_rips);
                                foreach (var obj in list7)
                                {
                                    SheetUS.Cells["A" + rowus].Value = obj.tipo_id_usuario;
                                    SheetUS.Cells["B" + rowus].Value = obj.num_id_usuario;
                                    SheetUS.Cells["C" + rowus].Value = obj.cod_entidad_adm;
                                    SheetUS.Cells["D" + rowus].Value = obj.tipo_id_usuario;
                                    SheetUS.Cells["E" + rowus].Value = obj.primer_apellido;
                                    SheetUS.Cells["F" + rowus].Value = obj.segundo_apellido;
                                    SheetUS.Cells["G" + rowus].Value = obj.primer_nombre;
                                    SheetUS.Cells["H" + rowus].Value = obj.segundo_nombre;
                                    SheetUS.Cells["I" + rowus].Value = obj.edad;
                                    SheetUS.Cells["J" + rowus].Value = obj.unidad_medida_edad;
                                    SheetUS.Cells["K" + rowus].Value = obj.sexo;
                                    SheetUS.Cells["L" + rowus].Value = obj.cod_departamento_residencia;
                                    SheetUS.Cells["M" + rowus].Value = obj.cod_municipio_residencia;
                                    SheetUS.Cells["N" + rowus].Value = obj.zona_residencia;
                                    SheetUS.Cells["O" + rowus].Value = regionals.indice;
                                    SheetUS.Cells["P" + rowus].Value = item.mes + "/" + item.año;
                                    rowus++;
                                }
                                break;
                                #endregion
                        }
                    }

                    //}

                    string namefile = "ReporteLogRipsCorrectos_" + regionals.indice + "_" + mes + "_" + año;
                    SheetAC.Cells["A:R"].AutoFitColumns();
                    SheetAP.Cells["A:Q"].AutoFitColumns();
                    SheetAN.Cells["A:P"].AutoFitColumns();
                    SheetAU.Cells["A:S"].AutoFitColumns();
                    SheetAH.Cells["A:U"].AutoFitColumns();
                    SheetAF.Cells["A:S"].AutoFitColumns();
                    SheetUS.Cells["A:P"].AutoFitColumns();

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
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('Problemas al generar el reporte');" +
                "</script> ";
                Response.Write(rta);
                Response.End();
            }

        }

        /*NUEVO LOG RIPS INOPORTUNO*/
        public void ExportarLogRipsInoportunos(int? regional, int mes, int año)
        {
            Models.CuentasMedicas.Rips Model = new Models.CuentasMedicas.Rips();
            List<RIPS> rips = Model.GetListaRipsPorMesYAño(mes, año, regional);
            Ref_regional regionals = BusClass.GetRefRegion().Where(l => l.id_ref_regional == regional.Value).FirstOrDefault();
            if (rips.Count == 0)
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "</script> ";
                Response.Write(rta);
                Response.End();
            }
            else
            {
                string[] array = new string[5] { "AC", "AP", "AN", "AU", "AH" };

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet SheetAC = Ep.Workbook.Worksheets.Add("AC");
                ExcelWorksheet SheetAP = Ep.Workbook.Worksheets.Add("AP");
                ExcelWorksheet SheetAN = Ep.Workbook.Worksheets.Add("AN");
                ExcelWorksheet SheetAU = Ep.Workbook.Worksheets.Add("AU");
                ExcelWorksheet SheetAH = Ep.Workbook.Worksheets.Add("AH");
                System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(22, 54, 92);

                foreach (string archivo in array)
                {
                    switch (archivo)
                    {
                        case "AC":
                            #region Encabezados Ac

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
                            #endregion
                            break;
                        case "AP":
                            #region Encabezados AP
                            SheetAP.Cells["A1:Q1"].Style.Font.Bold = true;
                            SheetAP.Cells["A1:Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAP.Cells["A1:Q1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAP.Cells["A1:Q1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAP.Cells["A1"].Value = "num_factura";
                            SheetAP.Cells["B1"].Value = "codigo_prestador";
                            SheetAP.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAP.Cells["D1"].Value = "num_id_usuario";
                            SheetAP.Cells["E1"].Value = "fecha_procedimiento";
                            SheetAP.Cells["F1"].Value = "num_autorizacion";
                            SheetAP.Cells["G1"].Value = "cod_procedimiento";
                            SheetAP.Cells["H1"].Value = "ambito_procedimiento";
                            SheetAP.Cells["I1"].Value = "finalidad_procedimiento  ";
                            SheetAP.Cells["J1"].Value = "personal_atiende";
                            SheetAP.Cells["K1"].Value = "dx_ppal";
                            SheetAP.Cells["L1"].Value = "dx_rel";
                            SheetAP.Cells["M1"].Value = "complicacion";
                            SheetAP.Cells["N1"].Value = "forma_acto_quirurgico";
                            SheetAP.Cells["O1"].Value = "valor_procedimiento";
                            SheetAP.Cells["P1"].Value = "Regional";
                            SheetAP.Cells["Q1"].Value = "Mes y año";
                            #endregion
                            break;
                        case "AN":
                            SheetAN.Cells["A1:P1"].Style.Font.Bold = true;
                            SheetAN.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAN.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAN.Cells["A1:P1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAN.Cells["A1"].Value = "num_factura";
                            SheetAN.Cells["B1"].Value = "codigo_prestador";
                            SheetAN.Cells["C1"].Value = "tipo_id_madre";
                            SheetAN.Cells["D1"].Value = "num_id_madre";
                            SheetAN.Cells["E1"].Value = "fecha_nacimiento_rn";
                            SheetAN.Cells["F1"].Value = "hora_nacimiento";
                            SheetAN.Cells["G1"].Value = "edad_gestacional";
                            SheetAN.Cells["H1"].Value = "control_prenatal";
                            SheetAN.Cells["I1"].Value = "sexo  ";
                            SheetAN.Cells["J1"].Value = "peso";
                            SheetAN.Cells["K1"].Value = "dx_recien_nacido";
                            SheetAN.Cells["L1"].Value = "causa_muerte";
                            SheetAN.Cells["M1"].Value = "fecha_muerte";
                            SheetAN.Cells["N1"].Value = "hora_muerte";
                            SheetAN.Cells["O1"].Value = "Regional";
                            SheetAN.Cells["P1"].Value = "Mes y año";
                            break;
                        case "AU":
                            SheetAU.Cells["A1:S1"].Style.Font.Bold = true;
                            SheetAU.Cells["A1:S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAU.Cells["A1:S1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAU.Cells["A1:S1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAU.Cells["A1"].Value = "num_factura";
                            SheetAU.Cells["B1"].Value = "codigo_prestador";
                            SheetAU.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAU.Cells["D1"].Value = "num_id_usuario";
                            SheetAU.Cells["E1"].Value = "fecha_ingreso_observacion";
                            SheetAU.Cells["F1"].Value = "hora_ingreso_observacion";
                            SheetAU.Cells["G1"].Value = "num_autorizacion";
                            SheetAU.Cells["H1"].Value = "causa_externa";
                            SheetAU.Cells["I1"].Value = "dx_salida  ";
                            SheetAU.Cells["J1"].Value = "dx_rel_salida_1";
                            SheetAU.Cells["K1"].Value = "dx_rel_salida_2";
                            SheetAU.Cells["L1"].Value = "dx_rel_salida_3";
                            SheetAU.Cells["M1"].Value = "destino_usuario_salida";
                            SheetAU.Cells["N1"].Value = "estado_usuario_salida";
                            SheetAU.Cells["O1"].Value = "causa_basica_muerte";
                            SheetAU.Cells["P1"].Value = "fecha_salida";
                            SheetAU.Cells["Q1"].Value = "hora_salida";
                            SheetAU.Cells["R1"].Value = "Regional";
                            SheetAU.Cells["S1"].Value = "Mes y año";

                            break;
                        case "AH":
                            SheetAH.Cells["A1:U1"].Style.Font.Bold = true;
                            SheetAH.Cells["A1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            SheetAH.Cells["A1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            SheetAH.Cells["A1:U1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            SheetAH.Cells["A1"].Value = "num_factura";
                            SheetAH.Cells["B1"].Value = "codigo_prestador";
                            SheetAH.Cells["C1"].Value = "tipo_id_usuario";
                            SheetAH.Cells["D1"].Value = "num_id_usuario";
                            SheetAH.Cells["E1"].Value = "via_ingreso";
                            SheetAH.Cells["F1"].Value = "fecha_ingreso";
                            SheetAH.Cells["G1"].Value = "hora_ingreso";
                            SheetAH.Cells["H1"].Value = "num_autorizacion";
                            SheetAH.Cells["I1"].Value = "causa_externa";
                            SheetAH.Cells["J1"].Value = "dx_ppal_ingreso";
                            SheetAH.Cells["K1"].Value = "dx_ppal_egreso";
                            SheetAH.Cells["L1"].Value = "dx_rel_1_egreso";
                            SheetAH.Cells["M1"].Value = "dx_rel_2_egreso";
                            SheetAH.Cells["N1"].Value = "dx_rel_3_egreso";
                            SheetAH.Cells["O1"].Value = "dx_complicacion";
                            SheetAH.Cells["P1"].Value = "estado_salida";
                            SheetAH.Cells["Q1"].Value = "dx_causa_basica_muerte";
                            SheetAH.Cells["R1"].Value = "fecha_egreso";
                            SheetAH.Cells["S1"].Value = "hora_egreso";
                            SheetAH.Cells["T1"].Value = "Regional";
                            SheetAH.Cells["U1"].Value = "Mes y año";
                            break;

                        default:
                            break;
                    }
                }


                int rowac = 2, rowap = 2, rowan = 2, rowau = 2, rowah = 2, rowaf = 2, rowus = 2;
                foreach (RIPS item in rips)
                {
                    DateTime fechaNueva = new DateTime(Convert.ToInt32(item.año), Convert.ToInt32(item.mes), 1);

                    for (int i = 0; i < array.Length; i++)
                    {
                        switch (array[i])
                        {
                            #region case
                            case "AC":

                                List<RIPS_AC_HISTORICO> list = BusClass.GetRipsAcOportunidadById(item.id_rips, 2);
                                foreach (var obj in list)
                                {
                                    SheetAC.Cells["A" + rowac].Value = obj.num_factura;
                                    SheetAC.Cells["B" + rowac].Value = obj.codigo_prestador;
                                    SheetAC.Cells["C" + rowac].Value = obj.tipo_id_usuario;
                                    SheetAC.Cells["D" + rowac].Value = obj.num_id_usuario;
                                    SheetAC.Cells["E" + rowac].Value = obj.fecha_consulta.Value.ToString("MM/dd/yyyy");
                                    SheetAC.Cells["F" + rowac].Value = obj.num_autorizacion;
                                    SheetAC.Cells["G" + rowac].Value = obj.cod_consulta;
                                    SheetAC.Cells["H" + rowac].Value = obj.finalidad_consulta;
                                    SheetAC.Cells["I" + rowac].Value = obj.causa_externa;
                                    SheetAC.Cells["J" + rowac].Value = obj.cod_dx_ppal;
                                    SheetAC.Cells["K" + rowac].Value = obj.cod_dx_rel_1;
                                    SheetAC.Cells["L" + rowac].Value = obj.cod_dx_rel_2;
                                    SheetAC.Cells["M" + rowac].Value = obj.cod_dx_rel_3;
                                    SheetAC.Cells["N" + rowac].Value = obj.tipo_dx_ppal;
                                    SheetAC.Cells["O" + rowac].Value = obj.valor_consulta;
                                    SheetAC.Cells["P" + rowac].Value = obj.valor_neto_a_pagar;
                                    SheetAC.Cells["Q" + rowac].Value = regionals.indice;
                                    SheetAC.Cells["R" + rowac].Value = fechaNueva;
                                    SheetAC.Cells[string.Format("R{0}", rowac)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                                    rowac++;
                                }
                                SheetAC.Cells["A:AZ"].AutoFitColumns();

                                break;
                            case "AP":
                                List<RIPS_AP_HISTORICO> list2 = BusClass.GetRipsApOportunidadById(item.id_rips, 2);
                                foreach (var obj in list2)
                                {
                                    SheetAP.Cells["A" + rowap].Value = obj.num_factura;
                                    SheetAP.Cells["B" + rowap].Value = obj.cod_prestador;
                                    SheetAP.Cells["C" + rowap].Value = obj.tipo_id_usuario;
                                    SheetAP.Cells["D" + rowap].Value = obj.num_id_usuario;
                                    SheetAP.Cells["E" + rowap].Value = obj.fecha_procedimiento.Value.ToString("MM/dd/yyyy");
                                    SheetAP.Cells["F" + rowap].Value = obj.num_autorizacion;
                                    SheetAP.Cells["G" + rowap].Value = obj.cod_procedimiento;
                                    SheetAP.Cells["H" + rowap].Value = obj.ambito_procedimiento;
                                    SheetAP.Cells["I" + rowap].Value = obj.finalidad_procedimiento;
                                    SheetAP.Cells["J" + rowap].Value = obj.personal_atiende;
                                    SheetAP.Cells["K" + rowap].Value = obj.dx_ppal;
                                    SheetAP.Cells["L" + rowap].Value = obj.dx_rel;
                                    SheetAP.Cells["M" + rowap].Value = obj.complicacion;
                                    SheetAP.Cells["N" + rowap].Value = obj.forma_acto_quirurgico;
                                    SheetAP.Cells["O" + rowap].Value = obj.valor_procedimiento;
                                    SheetAP.Cells["P" + rowap].Value = regionals.indice;
                                    SheetAP.Cells["Q" + rowap].Value = fechaNueva;
                                    SheetAP.Cells[string.Format("Q{0}", rowap)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                                    rowap++;
                                }
                                SheetAP.Cells["A:AZ"].AutoFitColumns();

                                break;
                            case "AU":
                                List<RIPS_AU_HISTORICO> list3 = BusClass.GetRipsAuoportunidadById(item.id_rips, 2);
                                foreach (var obj in list3)
                                {
                                    SheetAU.Cells["A" + rowau].Value = obj.num_factura;
                                    SheetAU.Cells["B" + rowau].Value = obj.cod_prestador;
                                    SheetAU.Cells["C" + rowau].Value = obj.tipo_id_usuario;
                                    SheetAU.Cells["D" + rowau].Value = obj.num_id_usuario;
                                    SheetAU.Cells["E" + rowau].Value = obj.fecha_ingreso_observacion;
                                    SheetAU.Cells["F" + rowau].Value = obj.hora_ingreso_observacion;
                                    SheetAU.Cells["G" + rowau].Value = obj.num_autorizacion;
                                    SheetAU.Cells["H" + rowau].Value = obj.causa_externa;
                                    SheetAU.Cells["I" + rowau].Value = obj.dx_salida;
                                    SheetAU.Cells["J" + rowau].Value = obj.dx_rel_salida_1;
                                    SheetAU.Cells["K" + rowau].Value = obj.dx_rel_salida_2;
                                    SheetAU.Cells["L" + rowau].Value = obj.dx_rel_salida_3;
                                    SheetAU.Cells["M" + rowau].Value = obj.destino_usuario_salida;
                                    SheetAU.Cells["N" + rowau].Value = obj.estado_usuario_salida;
                                    SheetAU.Cells["O" + rowau].Value = obj.causa_basica_muerte;
                                    SheetAU.Cells["P" + rowau].Value = obj.fecha_salida;
                                    SheetAU.Cells["Q" + rowau].Value = obj.hora_salida;
                                    SheetAU.Cells["R" + rowau].Value = regionals.indice;
                                    SheetAU.Cells["S" + rowau].Value = fechaNueva;
                                    SheetAU.Cells[string.Format("S{0}", rowau)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                                    rowau++;
                                }
                                SheetAU.Cells["A:AZ"].AutoFitColumns();

                                break;
                            case "AN":
                                List<RIPS_AN_HISTORICO> list4 = BusClass.GetRipsAnOportunidadById(item.id_rips, 2);
                                foreach (var obj in list4)
                                {
                                    SheetAN.Cells["A" + rowan].Value = obj.num_factura;
                                    SheetAN.Cells["B" + rowan].Value = obj.cod_prestador;
                                    SheetAN.Cells["C" + rowan].Value = obj.tipo_id_madre;
                                    SheetAN.Cells["D" + rowan].Value = obj.num_id_madre;
                                    SheetAN.Cells["E" + rowan].Value = obj.fecha_nacimiento_rn;
                                    SheetAN.Cells["F" + rowan].Value = obj.hora_nacimiento;
                                    SheetAN.Cells["G" + rowan].Value = obj.edad_gestacional;
                                    SheetAN.Cells["H" + rowan].Value = obj.control_prenatal;
                                    SheetAN.Cells["I" + rowan].Value = obj.sexo;
                                    SheetAN.Cells["J" + rowan].Value = obj.peso;
                                    SheetAN.Cells["K" + rowan].Value = obj.dx_recien_nacido;
                                    SheetAN.Cells["L" + rowan].Value = obj.causa_muerte;
                                    SheetAN.Cells["M" + rowan].Value = obj.fecha_muerte;
                                    SheetAN.Cells["N" + rowan].Value = obj.hora_muerte;
                                    SheetAN.Cells["O" + rowan].Value = regionals.indice;
                                    SheetAN.Cells["P" + rowan].Value = fechaNueva;
                                    SheetAN.Cells[string.Format("P{0}", rowan)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                                    rowan++;
                                }
                                SheetAN.Cells["A:AZ"].AutoFitColumns();

                                break;
                            case "AH":
                                List<RIPS_AH_HISTORICO> list5 = BusClass.GetRipsAhOportunidadById(item.id_rips, 2);
                                foreach (var obj in list5)
                                {
                                    SheetAH.Cells["A" + rowah].Value = obj.num_factura;
                                    SheetAH.Cells["B" + rowah].Value = obj.cod_prestador;
                                    SheetAH.Cells["C" + rowah].Value = obj.tipo_id_usuario;
                                    SheetAH.Cells["D" + rowah].Value = obj.num_id_usuario;
                                    SheetAH.Cells["E" + rowah].Value = obj.via_ingreso;
                                    SheetAH.Cells["F" + rowah].Value = obj.fecha_ingreso;
                                    SheetAH.Cells["G" + rowah].Value = obj.hora_ingreso;
                                    SheetAH.Cells["H" + rowah].Value = obj.num_autorizacion;
                                    SheetAH.Cells["I" + rowah].Value = obj.causa_externa;
                                    SheetAH.Cells["J" + rowah].Value = obj.dx_ppal_ingreso;
                                    SheetAH.Cells["K" + rowah].Value = obj.dx_ppal_egreso;
                                    SheetAH.Cells["L" + rowah].Value = obj.dx_rel_1_egreso;
                                    SheetAH.Cells["M" + rowah].Value = obj.dx_rel_2_egreso;
                                    SheetAH.Cells["N" + rowah].Value = obj.dx_rel_3_egreso;
                                    SheetAH.Cells["O" + rowah].Value = obj.dx_complicacion;
                                    SheetAH.Cells["P" + rowah].Value = obj.estado_salida;
                                    SheetAH.Cells["Q" + rowah].Value = obj.dx_causa_basica_muerte;
                                    SheetAH.Cells["R" + rowah].Value = obj.fecha_egreso;
                                    SheetAH.Cells["S" + rowah].Value = obj.hora_egreso;
                                    SheetAH.Cells["T" + rowah].Value = regionals.indice;
                                    SheetAH.Cells["U" + rowah].Value = fechaNueva;
                                    SheetAH.Cells[string.Format("U{0}", rowah)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                                    rowah++;
                                }
                                SheetAH.Cells["A:AZ"].AutoFitColumns();

                                break;
                                #endregion
                        }
                    }
                }

                string namefile = "ReporteLogRipsInoportunos_" + regionals.indice + "_" + mes + "_" + año;
                SheetAC.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
        }

        /*FIN NUEVO RIPS INOPORTUNO*/

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^0-9A-Za-z]", "", RegexOptions.None);
        }

        public ActionResult HomologacionRips()
        {
            Models.CuentasMedicas.homologacion Model = new Models.CuentasMedicas.homologacion();
            ViewBag.TipoIdentificacion = Model.GetTipoIdentificacion(ref MsgRes).OrderBy(p => p.descripcion);
            return View(Model);
        }

        public JsonResult Selection_RipsFac(Models.CuentasMedicas.homologacion Model)
        {
            String mensaje = "";
            string result = "";
            List<ManagmentRipsHomologacionFacResult> listaok = new List<ManagmentRipsHomologacionFacResult>();
            try
            {

                var TipoDoc = "";
                if (Model.tipo_id_prestador == "NIT")
                {
                    TipoDoc = "NI";
                }
                else
                {
                    TipoDoc = Model.tipo_id_prestador;
                }

                listaok = Model.ConsultaHomologacionFac(Model.num_factura, TipoDoc, Model.num_id_prestador);

                int i = 0;
                foreach (var item in listaok)
                {
                    i += 1;
                    result += "<tr>";
                    result += "<td>" + item.id_rips + "</td>";
                    result += "<td>" + item.fecha_cargue + "</td>";
                    result += "<td>" + item.nombre_regional + "</td>";
                    result += "<td>" + item.tipo_id_prestador + "</td>";
                    result += "<td>" + item.num_id_prestador + "</td>";
                    result += "<td>" + item.nombre_prestador + "</td>";
                    result += "<td>" + item.num_factura + "</td>";
                    result += "<td><a href='" + Url.Action("HomologacionRipsDtll", "Rips", new { id_rips = item.id_rips, tipo_doc = item.tipo_id_prestador, documento = item.num_id_prestador, num_factura = item.num_factura }) + "'>Gestionar</a></td>";
                    result += "</tr>";

                }

                var data = new
                {
                    tabla = result,
                };

                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                mensaje = "ERROR EL INGRESO." + ex.Message;
                return Json(new
                {
                    success = false,
                    message = mensaje
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult HomologacionRipsDtll(Int32 id_rips, String tipo_doc, String documento, String num_factura)
        {

            var id_rips_homologacion = 0;
            Models.CuentasMedicas.homologacion Model = new Models.CuentasMedicas.homologacion();
            List<rips_homologacion> ListaHomologacio = new List<rips_homologacion>();
            List<ManagmentRipsHomologacionFacDTLLResult> lista = new List<ManagmentRipsHomologacionFacDTLLResult>();
            lista = Model.ConsultaHomologacionFacdtll(num_factura, tipo_doc, Convert.ToString(documento), id_rips);

            List<rips_homologacion_dtll> listaH = new List<rips_homologacion_dtll>();
            rips_homologacion obj = new rips_homologacion();

            obj.id_rips = id_rips;
            obj.num_facturas = num_factura;

            ViewBag.id_rips = id_rips;
            ViewBag.num_fac = num_factura;
            ViewBag.tipo = tipo_doc;
            ViewBag.documento = documento;
            ViewBag.ListaGlosa = Model.GetMedglosa();
            ListaHomologacio = Model.Traerhomologacion_dtll(obj);

            if (ListaHomologacio.Count == 0)
            {
                if (lista.Count != 0)
                {
                    rips_homologacion obj2 = new rips_homologacion();
                    rips_homologacion_dtll obj3 = new rips_homologacion_dtll();


                    obj2.id_rips = id_rips;
                    obj2.num_facturas = num_factura;
                    obj2.estado = 1;
                    obj2.fecha_ingresa = Convert.ToDateTime(DateTime.Now);
                    obj2.usuario_ingresa = SesionVar.UserName;

                    id_rips_homologacion = Model.Insertar_rips_homologacion(obj2, ref MsgRes);

                    foreach (var item in lista)
                    {
                        obj3.id_rips_homologacion = id_rips_homologacion;
                        obj3.tipo_rips = item.tipo;
                        obj3.id_tipo = item.id;
                        obj3.cod_rips = item.codigo;
                        obj3.cod_homologacion = item.codigoH;
                        obj3.valorT = item.valor;
                        obj3.valorH = item.valorH;
                        obj3.descripcion_rips = item.descripcion_rips;
                        obj3.descripcion_homologacion = item.descripcionH;
                        obj3.obs_homologacion = item.obs_tarifario;
                        //Leo

                        listaH.Add(obj3);
                        obj3 = new rips_homologacion_dtll();
                    }
                    Model.Insertar_rips_homologacion_dtll(listaH, ref MsgRes);
                }

            }

            return View(Model);
        }

        public JsonResult Selection_AnalistaTotal(Models.CuentasMedicas.homologacion Model)
        {

            List<ManagmentRipsHomologacionFacDTLLFinalResult> result = new List<ManagmentRipsHomologacionFacDTLLFinalResult>();
            List<ManagmentRipsHomologacionFacDTLLFinalResult> lista = new List<ManagmentRipsHomologacionFacDTLLFinalResult>();

            lista = Model.ConsultaHomologacionFacdtllFinal(Model.num_factura, Model.id_rips);

            var query = lista.Select(item => new ManagmentRipsHomologacionFacDTLLFinalResult
            {
                id_rips_homologacion_dtll = item.id_rips_homologacion_dtll,
                tipo_rips = item.tipo_rips,
                descripcion_rips = item.descripcion_rips,
                valorT = item.valorT,
                cod_rips = item.cod_rips,
                descripcion_homologacion = item.descripcion_homologacion,
                valorH = item.valorH,
                cod_homologacion = item.cod_homologacion + '-' + item.obs_homologacion,
                tiene_glosa = item.tiene_glosa,
                descripcionGlosa = item.descripcionGlosa,

            }).OrderBy(x => x.cod_rips);

            return Json(query, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Selection_Agrupado(Models.CuentasMedicas.homologacion Model)
        {

            List<ManagmentRipsHomologacionFacDTLLFinalResult> result = new List<ManagmentRipsHomologacionFacDTLLFinalResult>();
            List<ManagmentRipsHomologacionFacDTLLFinalResult> lista = new List<ManagmentRipsHomologacionFacDTLLFinalResult>();

            lista = Model.ConsultaHomologacionFacdtllFinal(Model.num_factura, Model.id_rips);



            var resultado = (from a in lista
                             group a by new { a.cod_rips, a.descripcion_rips, a.valorT } into g
                             select new
                             {
                                 descripcion_rips = g.Key.descripcion_rips,
                                 cod_rips = g.Key.cod_rips,
                                 TotalValor = g.Sum(y => y.valorT),
                                 Count = g.Count(a => a.id_rips_homologacion_dtll != null)
                             }
                     ).OrderBy(x => x.cod_rips);

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetEval1(string ID, Int32? ID2, int? page, int? limit)
        {
            Models.CuentasMedicas.homologacion Model = new homologacion();
            List<ManagmentRipsHomologacionFacDTLLFinalResult> result = new List<ManagmentRipsHomologacionFacDTLLFinalResult>();
            List<ManagmentRipsHomologacionFacDTLLFinalResult> lista = new List<ManagmentRipsHomologacionFacDTLLFinalResult>();

            lista = Model.ConsultaHomologacionFacdtllFinal("LD3", 54);
            lista = lista.Where(x => x.id_rips_homologacion_dtll == 1).ToList();
            var query = lista.Select(item => new ManagmentRipsHomologacionFacDTLLFinalResult
            {
                id_rips = item.id_rips,
                id_rips_homologacion_dtll = item.id_rips_homologacion_dtll,
                tipo_rips = item.tipo_rips,
                cod_rips = item.cod_rips,
                valorT = item.valorT,
                valorH = item.valorH,
                cod_homologacion = item.cod_homologacion,
                tiene_glosa = item.tiene_glosa,
                descripcionGlosa = item.descripcionGlosa,
                descripcion_rips = item.descripcion_rips,

            });
            List<ManagmentRipsHomologacionFacDTLLFinalResult> records = new List<ManagmentRipsHomologacionFacDTLLFinalResult>();
            int total;
            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Selection_homologacionAF(Models.CuentasMedicas.homologacion Model)
        {
            List<ManagmentRipsHomologacionFacFinalResult> listaok = new List<ManagmentRipsHomologacionFacFinalResult>();

            listaok = Model.ConsultaHomologacionFacFinal(Model.num_factura, Model.tipo_id_prestador, Model.num_id_prestador, Model.id_rips);

            return Json(listaok, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SavegestionGlosa(Models.CuentasMedicas.homologacion Model)
        {
            String mensaje = "";
            String Alerta = "";

            try
            {
                rips_homologacion_dtll obj = new rips_homologacion_dtll();

                obj.id_rips_homologacion_dtll = Model.id_rips_homologacion_dtll2;
                if (Model.tiene_glosa == 1)
                {
                    obj.tiene_glosa = Convert.ToBoolean(1);
                    obj.descripcionGlosa = Convert.ToString(Model.obsGlosa);
                    obj.id_tipo_glosa = Model.TipoGlosa;
                }
                else
                {
                    obj.tiene_glosa = Convert.ToBoolean(0);
                    obj.descripcionGlosa = "";
                    obj.id_tipo_glosa = 0;

                }


                Model.Actualizar_md_Lupe_cargue_base_G(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult SavegestionGlosaGeneral(Models.CuentasMedicas.homologacion Model)
        {
            String mensaje = "";
            String Alerta = "";

            try
            {
                rips_homologacion obj = new rips_homologacion();

                obj.id_rips_homologacion = Model.id_rips_homologacion_dtllG;
                if (Model.tiene_glosaG == 1)
                {
                    obj.tiene_glosa = Convert.ToBoolean(1);
                    obj.descripcion_glosa = Convert.ToString(Model.obsGlosaG);
                    obj.id_motivo_glosa = Model.TipoGlosaG;
                }
                else
                {
                    obj.tiene_glosa = Convert.ToBoolean(0);
                    obj.descripcion_glosa = "";
                    obj.id_motivo_glosa = 0;

                }

                Model.Actualizar_rips_homologacion(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult SavegestionHomologacion(Models.CuentasMedicas.homologacion Model)
        {
            String mensaje = "";
            String Alerta = "";
            Decimal valor = 0;
            String descricionH = "";
            String observacion = "";
            String codigoHomologacion = "";
            List<vw_rips_homologacion_tarifario> Lst = new List<vw_rips_homologacion_tarifario>();

            try
            {
                Lst = BusClass.TarifarioRips();
                Lst = Lst.Where(x => x.id_rips_homologacion_tarifario == Model.id_rips_homologacion_tarifario).ToList();
                foreach (var item in Lst)
                {
                    valor = item.Valor.Value;
                    descricionH = item.procedimiento;
                    observacion = item.observacion;
                    codigoHomologacion = item.codigoH;

                }

                rips_homologacion_dtll obj = new rips_homologacion_dtll();

                obj.id_rips_homologacion_dtll = Model.id_rips_homologacion_dtll;
                obj.cod_homologacion = codigoHomologacion;
                obj.descripcion_homologacion = descricionH;
                obj.valorH = valor;
                obj.obs_homologacion = observacion;

                Model.Actualizar_md_Lupe_cargue_base_H(obj, ref MsgRes);


                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult GetHologacionTarifario(Models.CuentasMedicas.homologacion Model)
        {
            ObjectCache cache = MemoryCache.Default;
            List<vw_rips_homologacion_tarifario> filecontentsT = cache["filecontentsT"] as List<vw_rips_homologacion_tarifario>;
            List<vw_rips_homologacion_tarifario> Lst = new List<vw_rips_homologacion_tarifario>();

            if (filecontentsT != null)
            {
                Lst = filecontentsT;
            }
            else
            {
                Lst = BusClass.TarifarioRips();
                if (filecontentsT == null || filecontentsT.Count == 0)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();
                    CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(300.0);
                    filecontentsT = Lst.ToList();

                    cache.Add("filecontents", filecontentsT, cacheItemPolicy);

                }
            }
            // return Json(new { success = true, message = mensaje, id = Model.id_plan_de_mejora }, JsonRequestBehavior.AllowGet);
            //return Json(Lst.ToList(), "application/json", Encoding.UTF8,JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Lst.ToList(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo del 2023
        /// Metodo get que carga el formulario para realizar el cargue masivo de cargue de rips
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CargueRipsDepurados()
        {
            ViewBag.tipoRips = BusClass.ConsultaTipoRips(ref MsgRes);
            ViewBag.meses = BusClass.meses();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo del 2023
        /// Metodo post que carga el formulario para realizar el cargue masivo de cargue de rips
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CargueRipsDepurados(string tipoRips, HttpPostedFileBase file)
        {
            RipsDepurados Model = new RipsDepurados();

            try
            {
                //var cargueAnterior = BusClass.ConsultarCargueBaseRipsDepurados(tipoRips, mes, año);
                //if(cargueAnterior == null)
                //{

                CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                var asposeOptions = new Aspose.Cells.LoadOptions { MemorySetting = MemorySetting.MemoryPreference };
                Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook(file.InputStream, asposeOptions);
                Aspose.Cells.Worksheet worksheet = wb.Worksheets[0];
                Cells cells = worksheet.Cells;
                var ExportTableOptions = new Aspose.Cells.ExportTableOptions { CheckMixedValueType = false, ExportColumnName = true, ExportAsString = true };
                DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

                rips_depurados_carguebase CargueBase = new rips_depurados_carguebase();
                CargueBase.tipo_archivo_rips = tipoRips;
                //CargueBase.id_mes = mes;
                //CargueBase.id_año = año;
                CargueBase.fecha_digita = DateTime.Now;
                CargueBase.usuario_digita = SesionVar.UserName;

                switch (tipoRips)
                {
                    case "AC":
                        Model.CargarRipsDepuradosAC(dataTable, CargueBase, ref MsgRes);
                        break;
                    case "AP":
                        Model.CargarRipsDepuradosAP(dataTable, CargueBase, ref MsgRes);
                        break;
                    case "AU":
                        Model.CargarRipsDepuradosAU(dataTable, CargueBase, ref MsgRes);
                        break;
                    case "AH":

                        Model.CargarRipsDepuradosAH(dataTable, CargueBase, ref MsgRes);
                        break;
                    case "AM":
                        Model.CargarRipsDepuradosAM(dataTable, CargueBase, ref MsgRes);
                        break;
                    case "AN":
                        Model.CargarRipsDepuradosAN(dataTable, CargueBase, ref MsgRes);
                        break;
                    default:
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Lo sentimos, pero no esta habilitado el cargue masivo para el archivo " + tipoRips;
                        break;
                }

                ViewData["rta"] = MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto ? 1 : 2;

                string txterror = MsgRes.DescriptionResponse;

                txterror = txterror.Replace("Input string was not in a correct format", "Cadena de entrada no tiene el formato correcto");
                txterror = txterror.Replace("The string was not recognized as a valid DateTime. There is an unknown word starting at index 0", "La cadena no se reconoció como una fecha y hora válida.");
                txterror = txterror.Replace("Row number or column number cannot be zero", "El número de fila o el número de columna no puede ser cero");
                txterror = txterror.Replace("Corrupted file", "Archivo dañado");
                ViewData["Msg"] = txterror;

                //}
                //else
                //{
                //    ViewData["rta"] = 2;
                //    ViewData["Msg"] = "Ya se encuentra registrado un cargue anterior de RIPS depurados en el mismo rango de fecha seleccionado.";
                //}
            }
            catch (Exception ex)
            {
                string txterror = ex.Message;
                txterror = txterror.Replace("Input string was not in a correct format", "Cadena de entrada no tiene el formato correcto");
                txterror = txterror.Replace("The string was not recognized as a valid DateTime. There is an unknown word starting at index 0", "La cadena no se reconoció como una fecha y hora válida.");
                txterror = txterror.Replace("Row number or column number cannot be zero", "El número de fila o el número de columna no puede ser cero");
                txterror = txterror.Replace("Corrupted file", "Archivo dañado");

                ViewData["rta"] = 2;
                ViewData["Msg"] = "Ha ocurrido un error al momento de procesar la solicitud: " + txterror;
            }

            ViewBag.tipoRips = BusClass.ConsultaTipoRips(ref MsgRes);
            ViewBag.meses = BusClass.meses();
            return View();
        }

        public ActionResult TableroConsultaACAP()
        {
            return View();
        }

        [ValidateInput(false)]
        public FileContentResult DescargarRipsAC(string codCups, string cedula, string diagnostico, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<management_rips_busqueda_acResult> lista = new List<management_rips_busqueda_acResult>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            var nomArchivo = "";

            try
            {
                string[] filtroCodCups = codCups.Split(';');
                string[] filtroCedula = cedula.Split(';');
                string[] filtrodiagnostico = diagnostico.Split(';');

                var captura = 0;

                if (!string.IsNullOrEmpty(codCups))
                {
                    List<management_rips_busqueda_acResult> listaFinal2 = new List<management_rips_busqueda_acResult>();

                    for (var i = 0; i < filtroCodCups.Count(); i++)
                    {
                        List<management_rips_busqueda_acResult> lista2 = new List<management_rips_busqueda_acResult>();
                        var variableTomada = filtroCodCups[i];
                        variableTomada = variableTomada.Replace(" ", "");

                        if (captura == 0)
                        {
                            lista = BusClass.TraerConsultaRIPSAC((DateTime)fechaInicio, (DateTime)fechaFin, variableTomada, "", "");
                        }

                        lista2 = lista.Where(x => x.cod_consulta.Contains(variableTomada)).ToList();
                        listaFinal2.AddRange(lista2);
                    }
                    captura = 1;

                    lista = listaFinal2;
                }


                if (!string.IsNullOrEmpty(diagnostico))
                {
                    List<management_rips_busqueda_acResult> listaFinal2 = new List<management_rips_busqueda_acResult>();


                    for (var i = 0; i < filtrodiagnostico.Count(); i++)
                    {
                        List<management_rips_busqueda_acResult> lista2 = new List<management_rips_busqueda_acResult>();

                        var variableTomada = filtrodiagnostico[i];
                        variableTomada = variableTomada.Replace(" ", "");

                        if (captura == 0)
                        {
                            lista = BusClass.TraerConsultaRIPSAC((DateTime)fechaInicio, (DateTime)fechaFin, "", variableTomada, "");
                        }

                        lista2 = lista.Where(x => Convert.ToString(x.cod_dx_ppal).Contains(variableTomada)).ToList();
                        listaFinal2.AddRange(lista2);
                    }

                    captura = 1;

                    lista = listaFinal2;
                }


                if (!string.IsNullOrEmpty(cedula))
                {

                    List<management_rips_busqueda_acResult> listaFinal2 = new List<management_rips_busqueda_acResult>();

                    for (var i = 0; i < filtroCedula.Count(); i++)
                    {
                        List<management_rips_busqueda_acResult> lista2 = new List<management_rips_busqueda_acResult>();

                        var variableTomada = filtroCedula[i];
                        variableTomada = variableTomada.Replace(" ", "");

                        if (captura == 0)
                        {
                            lista = BusClass.TraerConsultaRIPSAC((DateTime)fechaInicio, (DateTime)fechaFin, "", "", variableTomada);
                        }

                        lista2 = lista.Where(x => Convert.ToString(x.num_id_usuario).Contains(variableTomada)).ToList();
                        listaFinal2.AddRange(lista2);
                    }

                    captura = 1;
                    lista = listaFinal2;
                }

                if (captura == 0)
                {
                    lista = BusClass.TraerConsultaRIPSAC((DateTime)fechaInicio, (DateTime)fechaFin, "", "", "");
                }


                if (lista.Count > 0)
                {

                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ReporteAC");

                    System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:Y1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:Y1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:Y1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:Y1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    Sheet.Cells["A1:Y1"].Style.Font.Name = "Century Gothic";
                    Sheet.Cells["A1:Y1"].Style.Font.Family = 12;

                    Sheet.Cells["A1"].Value = "Id RIPS";
                    Sheet.Cells["B1"].Value = "Número factura";
                    Sheet.Cells["C1"].Value = "Código prestador";
                    Sheet.Cells["D1"].Value = "Razón social prestador";
                    Sheet.Cells["E1"].Value = "Nit prestador";
                    Sheet.Cells["F1"].Value = "Tipo documento";
                    Sheet.Cells["G1"].Value = "Número documento";
                    Sheet.Cells["H1"].Value = "Fecha consulta";
                    Sheet.Cells["I1"].Value = "Número autorización";
                    Sheet.Cells["J1"].Value = "Código consulta";
                    Sheet.Cells["K1"].Value = "Finalidad consulta";
                    Sheet.Cells["L1"].Value = "Causa externa";
                    Sheet.Cells["M1"].Value = "Diagnóstico principal";
                    Sheet.Cells["N1"].Value = "Descripción diagnóstico principal";
                    Sheet.Cells["O1"].Value = "Diagnóstico 1";
                    Sheet.Cells["P1"].Value = "Descripción diagnóstico 1";
                    Sheet.Cells["Q1"].Value = "Diagnóstico 2";
                    Sheet.Cells["R1"].Value = "Descripción diagnóstico 2";
                    Sheet.Cells["S1"].Value = "Diagnóstico 3";
                    Sheet.Cells["T1"].Value = "Descripción diagnóstico 3";
                    Sheet.Cells["U1"].Value = "Tipo diagnóstico";
                    Sheet.Cells["V1"].Value = "Valor consulta";
                    Sheet.Cells["W1"].Value = "Valor cuota moderadora";
                    Sheet.Cells["X1"].Value = "Valor neto a pagar";
                    Sheet.Cells["Y1"].Value = "Regional";

                    int row = 2;
                    foreach (management_rips_busqueda_acResult item in lista)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_rips;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.num_factura;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.codigo_prestador;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.nits_nit;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.razon_social;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.tipo_id_usuario;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.num_id_usuario;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_consulta;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.num_autorizacion;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.cod_consulta;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.descripcionFinalidad;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.descripcionCausaExterna;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.cod_dx_ppal;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.descripcioncieppal;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.cod_dx_rel_1;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.descripcioncie1;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.descripcioncie2;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.cod_dx_rel_2;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.descripcioncie3;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.cod_dx_rel_3;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.descripcionDiagnosticoPrincipal;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.valor_consulta;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.valor_cuota_moderadora;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.valor_neto_a_pagar;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.nombre_regional;

                        Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }
                    Sheet.Cells["A:Y"].AutoFitColumns();

                    nomArchivo = "ReporteConsultaRipsAC_" + DateTime.Now;
                    return File(Ep.GetAsByteArray(), "application/excel", nomArchivo + ".xlsx");
                }
                else
                {
                    return File(Ep.GetAsByteArray(), "application/excel", nomArchivo + ".xlsx");
                }
            }
            catch (Exception ex)
            {
                return File(Ep.GetAsByteArray(), "application/excel", nomArchivo + ".xlsx");
            }
        }

        [ValidateInput(false)]
        public FileContentResult DescargarRipsAP(string codCups, string cedula, string diagnostico, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<management_rips_busqueda_apResult> lista = new List<management_rips_busqueda_apResult>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            var nomArchivo = "";

            try
            {
                string[] filtroCodCups = Convert.ToString(codCups).Split(';');
                string[] filtroCedula = cedula.Split(';');
                string[] filtrodiagnostico = diagnostico.Split(';');

                var captura = 0;

                if (!string.IsNullOrEmpty(codCups))
                {
                    List<management_rips_busqueda_apResult> listaFinal2 = new List<management_rips_busqueda_apResult>();

                    for (var i = 0; i < filtroCodCups.Count(); i++)
                    {
                        List<management_rips_busqueda_apResult> lista2 = new List<management_rips_busqueda_apResult>();
                        var variableTomada = filtroCodCups[i];
                        variableTomada = variableTomada.Replace(" ", "");

                        if (captura == 0)
                        {
                            lista = BusClass.TraerConsultaRIPSAP((DateTime)fechaInicio, (DateTime)fechaFin, variableTomada, "", "");
                        }

                        lista2 = lista.Where(x => x.cod_procedimiento.Contains(variableTomada)).ToList();
                        listaFinal2.AddRange(lista2);
                    }
                    captura = 1;

                    lista = listaFinal2;
                }


                if (!string.IsNullOrEmpty(diagnostico))
                {
                    List<management_rips_busqueda_apResult> listaFinal2 = new List<management_rips_busqueda_apResult>();


                    for (var i = 0; i < filtrodiagnostico.Count(); i++)
                    {
                        List<management_rips_busqueda_apResult> lista2 = new List<management_rips_busqueda_apResult>();

                        var variableTomada = filtrodiagnostico[i];
                        variableTomada = variableTomada.Replace(" ", "");

                        if (captura == 0)
                        {
                            lista = BusClass.TraerConsultaRIPSAP((DateTime)fechaInicio, (DateTime)fechaFin, "", variableTomada, "");

                        }

                        lista2 = lista.Where(x => Convert.ToString(x.dx_ppal).Contains(variableTomada)).ToList();
                        listaFinal2.AddRange(lista2);
                    }

                    captura = 1;

                    lista = listaFinal2;
                }

                if (!string.IsNullOrEmpty(cedula))
                {

                    List<management_rips_busqueda_apResult> listaFinal2 = new List<management_rips_busqueda_apResult>();

                    for (var i = 0; i < filtroCedula.Count(); i++)
                    {
                        List<management_rips_busqueda_apResult> lista2 = new List<management_rips_busqueda_apResult>();

                        var variableTomada = filtroCedula[i];
                        variableTomada = variableTomada.Replace(" ", "");

                        if (captura == 0)
                        {

                            lista = BusClass.TraerConsultaRIPSAP((DateTime)fechaInicio, (DateTime)fechaFin, "", "", variableTomada);
                        }


                        lista2 = lista.Where(x => Convert.ToString(x.num_id_usuario).Contains(variableTomada)).ToList();
                        listaFinal2.AddRange(lista2);
                    }

                    captura = 1;
                    lista = listaFinal2;
                }


                if (captura == 0)
                {
                    lista = BusClass.TraerConsultaRIPSAP((DateTime)fechaInicio, (DateTime)fechaFin, "", "", "");
                }


                if (lista.Count > 0)
                {

                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ReporteAP");

                    System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:T1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:T1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:T1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    Sheet.Cells["A1:T1"].Style.Font.Name = "Century Gothic";
                    Sheet.Cells["A1:T1"].Style.Font.Family = 12;

                    Sheet.Cells["A1"].Value = "Id RIPS";
                    Sheet.Cells["B1"].Value = "Número factura";
                    Sheet.Cells["C1"].Value = "Código prestador";
                    Sheet.Cells["D1"].Value = "Nit prestador";
                    Sheet.Cells["E1"].Value = "Razón social prestador";
                    Sheet.Cells["F1"].Value = "Tipo documento";
                    Sheet.Cells["G1"].Value = "Número documento";
                    Sheet.Cells["H1"].Value = "Fecha procedimiento";
                    Sheet.Cells["I1"].Value = "Número autorización";
                    Sheet.Cells["J1"].Value = "Código procedimiento";
                    Sheet.Cells["K1"].Value = "Finalidad procedimiento";
                    Sheet.Cells["L1"].Value = "Personal atiende";
                    Sheet.Cells["M1"].Value = "Diagnóstico principal";
                    Sheet.Cells["N1"].Value = "Descripción diagnóstico principal";
                    Sheet.Cells["O1"].Value = "Diagnóstico 1";
                    Sheet.Cells["P1"].Value = "Descripción diagnóstico 1";
                    Sheet.Cells["Q1"].Value = "Complicación";
                    Sheet.Cells["R1"].Value = "Forma acto quirurgico";
                    Sheet.Cells["S1"].Value = "Valor procedimiento";
                    Sheet.Cells["T1"].Value = "Regional";

                    int row = 2;
                    foreach (management_rips_busqueda_apResult item in lista)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_rips;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.num_factura;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.cod_prestador;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.nits_nit;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.razon_social;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.tipo_id_usuario;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.num_id_usuario;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_procedimiento;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.num_autorizacion;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.cod_procedimiento;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.descripcionFinalidad;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.descripcionPersonalAtiende;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.dx_ppal;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.descripcionDiagnosticoPrincipal;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.dx_rel;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.descripcionDiagnosticoRel;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.complicacion;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.descripcionActoQuirurgico;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.valor_procedimiento;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.nombre_regional;

                        Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }
                    Sheet.Cells["A:T"].AutoFitColumns();

                    nomArchivo = "ReporteConsultaRipsAP_" + DateTime.Now;
                    return File(Ep.GetAsByteArray(), "application/excel", nomArchivo + ".xlsx");
                }
                else
                {
                    return File(Ep.GetAsByteArray(), "application/excel", nomArchivo + ".xlsx");
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Error: " + ex.Message;
                return File(Ep.GetAsByteArray(), "application/excel", nomArchivo + ".xlsx");
            }
        }

        public ActionResult CreacionPrestadorRips()
        {
            ViewBag.regional = BusClass.GetRefRegion();
            return View();
        }

        [HttpPost]
        public ActionResult CreacionPrestadorRips(RipsDepurados Model)
        {
            ViewBag.regional = BusClass.GetRefRegion();
            Ref_RIPS_Prestadores presta = new Ref_RIPS_Prestadores();
            var mensaje = "";
            var rta = 2;

            try
            {
                presta.depa_nombre = Model.departamento;
                presta.muni_nombre = Model.ciudad.ToUpper();
                presta.codigo_habilitacion = Model.IDPrestador;
                presta.nits_nit = Model.nit;
                presta.razon_social = Model.nombre_prestador;
                presta.muni_nombre1 = Model.ciudad.ToUpper();
                presta.tipo_id = Model.TipoID;

                var inserta = BusClass.InsertarPrestadorRIPS(presta);
                if (inserta != 0)
                {
                    mensaje = "PRESTADOR INSERTADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DEL PRESTADOR";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DEL PRESTADOR: " + error;
            }

            ViewBag.rta = rta;
            ViewBag.msg = mensaje;

            return View();
        }

        public string ObtenerDepartamentos(int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<management_total_departamentosResult> departamentos = new List<management_total_departamentosResult>();

            try
            {
                departamentos = BusClass.TraerDepartamentosRegional(regional);
                foreach (var item in departamentos)
                {
                    result += "<option value='" + item.departamento + "'>" + item.departamento + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public string ObtenerCiudades(string departamento, int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            List<Ref_ciudades> Ciudades = new List<Ref_ciudades>();

            try
            {
                Ciudades = BusClass.GetCiudades();

                if (!string.IsNullOrEmpty(departamento))
                {
                    Ciudades = Ciudades.Where(x => x.departamento == departamento && x.id_ref_regional == regional).ToList();
                }

                foreach (var item in Ciudades)
                {
                    result += "<option value='" + item.nombre + "'>" + item.nombre + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public string ValidarExistenciaPrestadorNit(double nit)
        {
            var resultado = "";
            List<Ref_RIPS_Prestadores> lista = new List<Ref_RIPS_Prestadores>();
            try
            {
                lista = BusClass.ConsultaPrestadoresRipsNit(nit, ref MsgRes);

                if (lista.Count() > 0)
                {
                    resultado = "ESTE ID PRESTADOR YA EXISTE";
                }
                else
                {
                    resultado = "";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                resultado = "ERROR EN LA CONSULTA";
            }
            return resultado;
        }

        public string ValidarExistenciaPrestadorId(string IDPrestador)
        {
            var resultado = "";
            List<Ref_RIPS_Prestadores> lista = new List<Ref_RIPS_Prestadores>();
            try
            {
                lista = BusClass.ConsultaPrestadoresRipsIdPrestador(IDPrestador, ref MsgRes);

                if (lista.Count() > 0)
                {
                    resultado = "ESTE CÓDIGO PRESTADOR YA EXISTE";
                }
                else
                {
                    resultado = "";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                resultado = "ERROR EN LA CONSULTA";
            }
            return resultado;
        }
    }
}
