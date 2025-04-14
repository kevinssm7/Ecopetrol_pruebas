using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using Aspose.Cells;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.EventosEnSalud
{
    [SessionExpireFilter]

    public class EventosSaludController : Controller
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

        // GET: EventosSalud
        public ActionResult CargueMasivoEventos()
        {
            ViewBag.rta = 0;
            ViewBag.msg = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueMasivoEventos(HttpPostedFileBase file)
        {
            var mensaje = "";
            var rta = 2;
            Models.EventosSalud.EventosSalud Model = new Models.EventosSalud.EventosSalud();

            try
            {
                if (file != null)
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

                    evento_salud_cargue obj = new evento_salud_cargue();
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    Int32 lote = Model.ExcelMasivoAlertasDispe(dataTable, obj, ref MsgRes);

                    var resultado = MsgRes.ResponseType;
                    var mensajeSalida = MsgRes.DescriptionResponse;
                    var idUsuario = SesionVar.IDUser;

                    if (resultado == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESÓ CORRECTAMENTE CARGUE EVENTOS EN SALUD #" + lote;
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "ERROR AL INGRESAR CARGUE EVENTOS EN SALUD: " + MsgRes.DescriptionResponse;
                        rta = 2;
                    }
                }
                else
                {
                    mensaje = "SIN ARCHIVO PARA CARGAR";
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

        public ActionResult CargueEventos(int? idEvento, int? tipoActualizacion)
        {
            ECOPETROL_COMMON.ENTIDADES.eventos_salud_registros Model = new eventos_salud_registros();

            ViewBag.rta = 0;
            ViewBag.msg = "";

            ViewBag.meses = BusClass.meses();
            ViewBag.tipoDocumento = BusClass.GetTipoIdentificacion(ref MsgRes);

            ViewBag.fuente = BusClass.ListaEventosSaludFuenteReporte();
            ViewBag.ambito = BusClass.ListaEventosSaludAmbitoOcurrencia();
            ViewBag.severidad = BusClass.ListaEventosSaludSeveridadDesenlace();
            ViewBag.probabilidad = BusClass.ListaEventosSaludProbabilidadRepeticion();
            ViewBag.concepto = BusClass.ListaEventosSaludConceptoAuditoria();
            ViewBag.seguimiento = BusClass.ListaEventosSaludSeguimiento();

            ViewBag.categoria = BusClass.ListaEventosSaludCategoria();
            ViewBag.subcategoria = BusClass.ListaEventosSaludSubCategoria();
            ViewBag.negativo = BusClass.ListaEventosSaludResNegativo();
            ViewBag.clasificacion = BusClass.ListaEventosSaludClasificacion();

            var regionalPropia = BusClass.listadoRegionalesUsuario(SesionVar.IDUser).Take(1).Select(x => x.id_regional).FirstOrDefault();
            Ref_regional reg = new Ref_regional();
            reg = BusClass.GetRefRegionId((int)regionalPropia);
            var regionalDes = reg.indice;
            ViewBag.regionalPropia = regionalDes;

            ViewBag.ciudad = BusClass.GetCiudadesXRegional(regionalPropia);

            ViewBag.prestador = BusClass.getprestadores();

            if (idEvento != null && idEvento != 0)
            {
                Model = BusClass.TraerDatosEventosSaludId(idEvento);
            }
            else
            {
                Model.anio = DateTime.Now.Year;
                Model.dependencia_de_salud = regionalDes;
            }

            ViewBag.eventos = Model;
            ViewBag.idEvento = idEvento;
            ViewBag.tipoActualizacion = tipoActualizacion;
            ViewBag.unis = BusClass.unisRegional(reg.id_ref_regional);

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);

            ViewBag.años = años;

            return View();
        }

        public string ObtenerSubCategoria(int? categoria)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<ref_eventosSalud_subCategoriaEvento> sub = BusClass.EventosSaludTraerSubCategoriaId(categoria);

            foreach (var item in sub)
            {
                result += "<option value='" + item.id_tipo + "'>" + item.descripcion + "</option>";
            }
            return result;
        }

        public string ObtenerNegativos(int? categoria, int? clasificacion)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<ref_eventosSalud_resultadoNegativo> sub = BusClass.ListaEventosSaludResNegativoIdCategoriaClasificacion(categoria, clasificacion);

            foreach (var item in sub)
            {
                result += "<option value='" + item.id_tipo + "'>" + item.descripcion + "</option>";
            }
            return result;
        }

        public string TraerCiudadUnis(int? idUnis)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            List<Ref_ciudades> ciudad = new List<Ref_ciudades>();
            ciudad = BusClass.GetCiudadesXUnis(idUnis);

            foreach (var item in ciudad)
            {
                result += "<option value='" + item.id_ref_ciudades + "'>" + item.nombre + "</option>";
            }

            return result;
        }

        [HttpPost]
        public ActionResult CargueEventos(Models.EventosSalud.EventosSalud Model)
        {
            var mensaje = "";
            var rta = 2;

            ECOPETROL_COMMON.ENTIDADES.eventos_salud_registros evento = new eventos_salud_registros();
            ViewBag.eventos = evento;
            var gestion = 0;

            try
            {
                evento.dependencia_de_salud = Model.dependencia_de_salud;
                evento.anio = Model.anio;
                evento.id_mes = Model.id_mes;
                if (Model.id_mes != null)
                {
                    evento.mes = BusClass.meses().Where(x => x.id_mes == Model.id_mes).Select(x => x.descripcion).FirstOrDefault();
                }

                evento.consecutivo = Model.consecutivo;
                evento.fecha_de_reporte = Model.fecha_de_reporte;
                evento.fecha_de_ocurrencia_del_evento = Model.fecha_de_ocurrencia_del_evento;
                evento.localidad_de_servicios_de_salud = Model.localidad_de_servicios_de_salud;
                evento.fuente_del_reporte = Model.fuente_del_reporte;
                evento.reportante_nombre_de_quien_realiza_el_reporte = Model.reportante_nombre_de_quien_realiza_el_reporte;
                evento.nombre_de_municipio_donde_ocurrio_el_evento = Model.nombre_de_municipio_donde_ocurrio_el_evento;

                if (!string.IsNullOrEmpty(Model.nombre_de_municipio_donde_ocurrio_el_evento))
                {
                    evento.codigo_municipal_donde_ocurrio_el_evento = BusClass.GetCiudades().Where(x => x.id_ref_ciudades == Convert.ToInt32(Model.nombre_de_municipio_donde_ocurrio_el_evento)).Select(x => x.Codigo_DANE).FirstOrDefault();
                }

                evento.reportante_identificacion_de_quien_realiza_el_reporte = Model.reportante_identificacion_de_quien_realiza_el_reporte;
                evento.ambito_de_ocurrencia_del_evento = Model.ambito_de_ocurrencia_del_evento;
                evento.nit_del_prestador_en_donde_ocurrio_el_evento_adverso = Model.nit_del_prestador_en_donde_ocurrio_el_evento_adverso;

                if (!string.IsNullOrEmpty(Model.nit_del_prestador_en_donde_ocurrio_el_evento_adverso))
                {
                    Ref_ips_cuentas pres = new Ref_ips_cuentas();
                    pres = BusClass.getprestadoresNit(Model.nit_del_prestador_en_donde_ocurrio_el_evento_adverso);
                    if (pres != null)
                    {
                        if (!string.IsNullOrEmpty(pres.Nombre))
                        {
                            evento.nombre_del_prestador_en_donde_ocurrio_el_evento_adverso = pres.Nombre;
                        }
                        if (!string.IsNullOrEmpty(pres.cod_sap_prestador))
                        {
                            evento.numero_de_identificacion_del_prestador_codigo_sap = Convert.ToDecimal(pres.cod_sap_prestador);
                        }
                    }

                }

                evento.tipo_de_identificacion_del_beneficiario_en_el_cual_ocurrio_el_evento = Model.tipo_de_identificacion_del_beneficiario_en_el_cual_ocurrio_el_evento;
                evento.numero_de_identificacion_del_beneficiario = Model.numero_de_identificacion_del_beneficiario;
                evento.nombre_del_beneficiario = Model.nombre_del_beneficiario;
                evento.edad_del_beneficiario = Model.edad_del_beneficiario;
                evento.descripcion_del_evento = Model.descripcion_del_evento;
                evento.clasificacion_del_evento_de_la_atencion_en_salud = Model.clasificacion_del_evento_de_la_atencion_en_salud;
                evento.categoria_del_evento = Model.categoria_del_evento;
                evento.subcategoria_del_evento = Model.subcategoria_del_evento;
                evento.resultado_negativo_de_la_medicacion = Model.resultado_negativo_de_la_medicacion;
                evento.confirmacion_evento_prevenible_no_prevenible = Model.confirmacion_evento_prevenible_no_prevenible;
                evento.severidad_del_desenlace = Model.severidad_del_desenlace;
                evento.probabilidad_de_repeticion = Model.probabilidad_de_repeticion;
                evento.concepto_auditoria = Model.concepto_auditoria;
                evento.gestion_de_la_gestoria_integral_de_la_calidad = Model.gestion_de_la_gestoria_integral_de_la_calidad;
                evento.plan_de_mejora_al_prestador_si_o_no = Model.plan_de_mejora_al_prestador_si_o_no;
                evento.fecha_radicacion_del_plan_de_mejora = Model.fecha_radicacion_del_plan_de_mejora;
                evento.fecha_programada_para_revision_de_plan_de_mejora = Model.fecha_programada_para_revision_de_plan_de_mejora;
                evento.costo_de_no_calidad = Model.costo_de_no_calidad;
                evento.descripcion_de_costo_de_no_calidad = Model.descripcion_de_costo_de_no_calidad;
                evento.seguimiento = Model.seguimiento;
                evento.novedades = Model.novedades;
                evento.edicion_regional = Model.edicion_regional;
                evento.edicion_nacional = Model.edicion_nacional;
                evento.fecha_digita = DateTime.Now;
                evento.usuario_digita = SesionVar.UserName;

                if (Model.id_evento != 0 && Model.id_evento != null)
                {
                    evento.id_evento = Model.id_evento;

                    if (Model.tipoActualizacion == 1)
                    {
                        evento.edicion_regional = 1;
                    }
                    if (Model.tipoActualizacion == 2)
                    {
                        evento.edicion_nacional = 1;
                    }

                    gestion = BusClass.ActualizarRegistroEventosSalud(evento);
                }
                else
                {
                    evento.id_cargue = 0;
                    gestion = BusClass.InsertarEventoSalud(evento);
                }

                if (gestion != 0)
                {
                    mensaje = "EVENTO EN SALUD INGRESADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO: " + error;
            }

            ViewBag.msg = mensaje;
            ViewBag.rta = rta;

            ViewBag.meses = BusClass.meses();
            ViewBag.tipoDocumento = BusClass.GetTipoIdentificacion(ref MsgRes);

            ViewBag.fuente = BusClass.ListaEventosSaludFuenteReporte();
            ViewBag.ambito = BusClass.ListaEventosSaludAmbitoOcurrencia();
            ViewBag.severidad = BusClass.ListaEventosSaludSeveridadDesenlace();
            ViewBag.probabilidad = BusClass.ListaEventosSaludProbabilidadRepeticion();
            ViewBag.concepto = BusClass.ListaEventosSaludConceptoAuditoria();
            ViewBag.seguimiento = BusClass.ListaEventosSaludSeguimiento();

            ViewBag.categoria = BusClass.ListaEventosSaludCategoria();
            ViewBag.subcategoria = BusClass.ListaEventosSaludSubCategoria();
            ViewBag.negativo = BusClass.ListaEventosSaludResNegativo();
            ViewBag.clasificacion = BusClass.ListaEventosSaludClasificacion();

            var regionalPropia = BusClass.listadoRegionalesUsuario(SesionVar.IDUser).Take(1).Select(x => x.id_regional).FirstOrDefault();
            Ref_regional reg = new Ref_regional();
            reg = BusClass.GetRefRegionId((int)regionalPropia);
            var regionalDes = reg.indice;
            ViewBag.regionalPropia = regionalDes;

            ViewBag.ciudad = BusClass.GetCiudadesXRegional(regionalPropia);

            ViewBag.prestador = BusClass.getprestadores();

            ViewBag.unis = BusClass.unisRegional(reg.id_ref_regional);

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);

            ViewBag.años = años;
            ViewBag.idEvento = 0;

            if (Model.id_evento == 0 || Model.id_evento == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("TableroEventos", "EventosSalud", new { rta = rta, msg = mensaje });
            }
        }

        public ActionResult TableroEventos(int? mes, int? año, int? rta, string msg)
        {
            List<management_eventosSalud_tableroResult> lista = new List<management_eventosSalud_tableroResult>();
            var conteo = 0;
            try
            {
                lista = BusClass.ListadoEventosEnSaludTablero(mes, año);
                conteo = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoEventos = lista;
            ViewBag.conteo = conteo;

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);

            ViewBag.años = años;
            ViewBag.meses = BusClass.meses();

            ViewBag.rol = SesionVar.ROL;

            ViewBag.msg = msg;
            ViewBag.rta = rta;

            Session["ListadoEventos"] = lista;
            return View();
        }


        public void ExportarDatosEventos()
        {
            List<management_eventosSalud_tableroResult> lista = new List<management_eventosSalud_tableroResult>();

            try
            {
                lista = (List<management_eventosSalud_tableroResult>)Session["ListadoEventos"];
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosEventosSalud");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:AL1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:AL1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:AL1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:AL1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:AL1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:AL1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:AL1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id evento";
                    Sheet.Cells["B1"].Value = "Dependencia de Salud";
                    Sheet.Cells["C1"].Value = "Año";
                    Sheet.Cells["D1"].Value = "Id Mes";
                    Sheet.Cells["E1"].Value = "Mes";
                    Sheet.Cells["F1"].Value = "Fecha de Reporte";
                    Sheet.Cells["G1"].Value = "Fecha de ocurrencia del evento";
                    Sheet.Cells["H1"].Value = "Localidad de Servicios de Salud";
                    Sheet.Cells["I1"].Value = "Fuente del Reporte";
                    Sheet.Cells["J1"].Value = "Reportante (nombre de quien realiza el reporte)";
                    Sheet.Cells["K1"].Value = "Nombre de municipio donde ocurrio el evento";
                    Sheet.Cells["L1"].Value = "Código municipal donde ocurrio el evento";
                    Sheet.Cells["M1"].Value = "Reportante (identificación de quien realiza el reporte)";
                    Sheet.Cells["N1"].Value = "Ámbito de ocurrencia del evento";
                    Sheet.Cells["O1"].Value = "Nombre del Prestador en donde ocurrió el evento adverso";
                    Sheet.Cells["P1"].Value = "Nit del Prestador en donde ocurrió el evento adverso";
                    Sheet.Cells["Q1"].Value = "Número de identificación del Prestador (código SAP)";
                    Sheet.Cells["R1"].Value = "Tipo de identificación del beneficiario en el cual ocurrió el evento";
                    Sheet.Cells["S1"].Value = "Número de identificación del beneficiario";
                    Sheet.Cells["T1"].Value = "Nombre del beneficiario";
                    Sheet.Cells["U1"].Value = "Edad del Beneficiario";
                    Sheet.Cells["V1"].Value = "Descripción del evento";
                    Sheet.Cells["W1"].Value = "Clasificación del Evento de la Atención en Salud";
                    Sheet.Cells["X1"].Value = "Categoría del evento";
                    Sheet.Cells["Y1"].Value = "Subcategoría del evento";
                    Sheet.Cells["Z1"].Value = "Resultado negativo de la medicación";
                    Sheet.Cells["AA1"].Value = "Confirmación evento (prevenible /no prevenible)";
                    Sheet.Cells["AB1"].Value = "Severidad del desenlace";
                    Sheet.Cells["AC1"].Value = "Probabilidad de repetición";
                    Sheet.Cells["AD1"].Value = "Concepto Auditoria";
                    Sheet.Cells["AE1"].Value = "Gestión de la Gestoría Integral de la Calidad";
                    Sheet.Cells["AF1"].Value = "Plan de Mejora al Prestador (si o no)";
                    Sheet.Cells["AG1"].Value = "Fecha radicación del plan de mejora.";
                    Sheet.Cells["AH1"].Value = "fecha programada para revisión de plan de mejora.";
                    Sheet.Cells["AI1"].Value = "Costo de No Calidad";
                    Sheet.Cells["AJ1"].Value = "Descripción de costo de No Calidad";
                    Sheet.Cells["AK1"].Value = "Seguimiento";
                    Sheet.Cells["AL1"].Value = "Novedades";

                    int row = 2;

                    foreach (management_eventosSalud_tableroResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":AM" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_evento;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.dependencia_de_salud;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.anio;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.id_mes;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.mes;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_de_reporte;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_de_ocurrencia_del_evento;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.localidad_de_servicios_de_salud;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fuente_del_reporte;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.reportante_nombre_de_quien_realiza_el_reporte;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.nombre_de_municipio_donde_ocurrio_el_evento;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.codigo_municipal_donde_ocurrio_el_evento;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.reportante_identificacion_de_quien_realiza_el_reporte;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.ambito_de_ocurrencia_del_evento;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.nombre_del_prestador_en_donde_ocurrio_el_evento_adverso;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.nit_del_prestador_en_donde_ocurrio_el_evento_adverso;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.numero_de_identificacion_del_prestador_codigo_sap;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.tipo_de_identificacion_del_beneficiario_en_el_cual_ocurrio_el_evento;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.numero_de_identificacion_del_beneficiario;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.nombre_del_beneficiario;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.edad_del_beneficiario;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.descripcion_del_evento;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.clasificacion_del_evento_de_la_atencion_en_salud;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.categoria_del_evento;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.subcategoria_del_evento;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.resultado_negativo_de_la_medicacion;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.confirmacion_evento_prevenible_no_prevenible;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.severidad_del_desenlace;
                        Sheet.Cells[string.Format("AC{0}", row)].Value = item.probabilidad_de_repeticion;
                        Sheet.Cells[string.Format("AD{0}", row)].Value = item.concepto_auditoria;
                        Sheet.Cells[string.Format("AE{0}", row)].Value = item.gestion_de_la_gestoria_integral_de_la_calidad;
                        Sheet.Cells[string.Format("AF{0}", row)].Value = item.plan_de_mejora_al_prestador_si_o_no;
                        Sheet.Cells[string.Format("AG{0}", row)].Value = item.fecha_radicacion_del_plan_de_mejora;
                        Sheet.Cells[string.Format("AH{0}", row)].Value = item.fecha_programada_para_revision_de_plan_de_mejora;
                        Sheet.Cells[string.Format("AI{0}", row)].Value = item.costo_de_no_calidad;
                        Sheet.Cells[string.Format("AJ{0}", row)].Value = item.descripcion_de_costo_de_no_calidad;
                        Sheet.Cells[string.Format("AK{0}", row)].Value = item.seguimiento;
                        Sheet.Cells[string.Format("AL{0}", row)].Value = item.novedades;

                        Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("AI{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("AJ{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReporteEventosSalud";
                    Sheet.Cells["A:AL"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
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
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('ERROR EN LA DESCARGA');" +
                       "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }
    }
}
