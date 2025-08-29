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

                    Int32 lote = 0;

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

        public ActionResult CargueEventos(int? idEvento, int? idConcurrencia, int? idEvolucion, int? idPlan)
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

            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.prestador = BusClass.getprestadores();

            if (idEvento != null && idEvento != 0)
            {
                Model = BusClass.TraerDatosEventosSaludId(idEvento);
            }

            ViewBag.eventos = Model;
            ViewBag.idEvento = idEvento;
            ViewBag.idConcurrencia = idConcurrencia;
            ViewBag.idEvolucion = idEvolucion;
            ViewBag.idPlan = idPlan;
            
            ViewBag.unis = BusClass.unisRegional(reg.id_ref_regional);

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);

            ViewBag.años = años;

            return View(Model);
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
            int? idEvento = 0;

            ECOPETROL_COMMON.ENTIDADES.eventos_salud_registros Modelo = new eventos_salud_registros();

            try
            {
                evento = new ECOPETROL_COMMON.ENTIDADES.eventos_salud_registros
                {
                    id_cargue = Model.id_cargue,
                    id_concurrencia = Model.id_concurrencia,
                    id_evolucion_concurrencia = Model.id_evolucion_concurrencia,
                    id_planMejora = Model.id_planMejora,
                    Año = Model.Año,
                    IdMes = Model.IdMes,
                    Mes = Model.Mes,
                    FechaReporte = Model.FechaReporte,
                    FechaOcurrenciaEvento = Model.FechaOcurrenciaEvento,
                    RegionalReporta = Model.RegionalReporta,
                    LocalidadServiciosSalud = Model.LocalidadServiciosSalud,
                    NombreReportante = Model.NombreReportante,
                    IdentificacionReportante = Model.IdentificacionReportante,
                    NombrePrestadorEvento = Model.NombrePrestadorEvento,
                    CodigoSAPPrestador = Model.CodigoSAPPrestador,
                    NombreMunicipio = Model.NombreMunicipio,
                    CodigoMunicipal = Model.CodigoMunicipal,
                    RegionalBeneficiario = Model.RegionalBeneficiario,
                    TipoIdentificacion = Model.TipoIdentificacion,
                    NumeroIdentificacion = Model.NumeroIdentificacion,
                    NombreCompleto = Model.NombreCompleto,
                    Edad = Model.Edad,
                    FuenteReporte = Model.FuenteReporte,
                    AmbitoOcurrenciaEvento = Model.AmbitoOcurrenciaEvento,
                    DescripcionEvento = Model.DescripcionEvento,
                    ClasificacionEvento = Model.ClasificacionEvento,
                    CategoriaEvento = Model.CategoriaEvento,
                    SubcategoriaEvento = Model.SubcategoriaEvento,
                    ResultadoNegativoMedicacion = Model.ResultadoNegativoMedicacion,
                    ConfirmacionEventoAdverso = Model.ConfirmacionEventoAdverso,
                    SeveridadDesenlace = Model.SeveridadDesenlace,
                    ProbabilidadRepeticion = Model.ProbabilidadRepeticion,
                    ConceptoAuditoria = Model.ConceptoAuditoria,
                    GestionRegional = Model.GestionRegional,
                    PlanMejoraGenerado = Model.PlanMejoraGenerado,
                    CostoNoCalidad = Model.CostoNoCalidad,
                    DescripcionCostoNoCalidad = Model.DescripcionCostoNoCalidad,
                    estado_evento = 2,
                    fecha_digita = DateTime.Now,
                    usuario_digita = SesionVar.UserName
                };

                if (Model.id_evento != 0 && Model.id_evento != null)
                {
                    evento.id_evento = (int)Model.id_evento;
                    gestion = BusClass.ActualizarRegistroEventosSalud(evento);
                }
                else
                {
                    evento.id_cargue = 0;
                    gestion = BusClass.InsertarEventoSalud(evento);
                }

                if (gestion != 0)
                {
                    idEvento = gestion;

                    if (Model.PlanMejoraGenerado == 1)
                    {
                        ecop_plan_de_mejora plan = new ecop_plan_de_mejora()
                        {
                            id_eventos_salud = gestion,
                            estado_plan = 0,
                            fecha_ingreso = DateTime.Now,
                            usuario_ingreso = SesionVar.UserName
                        };

                        var ingresoPlan = BusClass.InsertarPlanMejora(plan, ref MsgRes);
                        if (ingresoPlan != 0)
                        {
                            var actualizaPMevento = BusClass.ActualizarRegistroEventosSaludPM(evento.id_cargue, ingresoPlan);
                        }
                    }

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

            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.prestador = BusClass.getprestadores();

            //if (idEvento != null && idEvento != 0)
            //{
            //    Modelo = BusClass.TraerDatosEventosSaludId(idEvento);
            //}

            ViewBag.eventos = Model;
            ViewBag.idEvento = idEvento;
            ViewBag.idConcurrencia = Model.id_concurrencia;
            ViewBag.idEvolucion = Model.id_evolucion_concurrencia;
            ViewBag.idPlan = Model.id_planMejora;

            ViewBag.unis = BusClass.unisRegional(reg.id_ref_regional);

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);

            ViewBag.años = años;

            if (Model.id_evento == 0 || Model.id_evento == null)
            {
                return View(Modelo);
            }
            else
            {
                return RedirectToAction("TableroEventos", "EventosSalud", new { rta = rta, msg = mensaje });
            }
        }

        public JsonResult CargueEventosParcial(Models.EventosSalud.EventosSalud Model)
        {
            var mensaje = "";
            var rta = 2;

            ECOPETROL_COMMON.ENTIDADES.eventos_salud_registros evento = new eventos_salud_registros();
            ViewBag.eventos = evento;
            var gestion = 0;
            int? idEvento = 0;
            int? idPlan = 0;

            ECOPETROL_COMMON.ENTIDADES.eventos_salud_registros Modelo = new eventos_salud_registros();

            try
            {
                evento = new ECOPETROL_COMMON.ENTIDADES.eventos_salud_registros
                {
                    id_cargue = Model.id_cargue,
                    id_concurrencia = Model.id_concurrencia,
                    id_evolucion_concurrencia = Model.id_evolucion_concurrencia,
                    id_planMejora = Model.id_planMejora,
                    Año = Model.Año,
                    IdMes = Model.IdMes,
                    Mes = Model.Mes,
                    FechaReporte = Model.FechaReporte,
                    FechaOcurrenciaEvento = Model.FechaOcurrenciaEvento,
                    RegionalReporta = Model.RegionalReporta,
                    LocalidadServiciosSalud = Model.LocalidadServiciosSalud,
                    NombreReportante = Model.NombreReportante,
                    IdentificacionReportante = Model.IdentificacionReportante,
                    NombrePrestadorEvento = Model.NombrePrestadorEvento,
                    CodigoSAPPrestador = Model.CodigoSAPPrestador,
                    NombreMunicipio = Model.NombreMunicipio,
                    CodigoMunicipal = Model.CodigoMunicipal,
                    RegionalBeneficiario = Model.RegionalBeneficiario,
                    TipoIdentificacion = Model.TipoIdentificacion,
                    NumeroIdentificacion = Model.NumeroIdentificacion,
                    NombreCompleto = Model.NombreCompleto,
                    Edad = Model.Edad,
                    FuenteReporte = Model.FuenteReporte,
                    AmbitoOcurrenciaEvento = Model.AmbitoOcurrenciaEvento,
                    DescripcionEvento = Model.DescripcionEvento,
                    ClasificacionEvento = Model.ClasificacionEvento,
                    CategoriaEvento = Model.CategoriaEvento,
                    SubcategoriaEvento = Model.SubcategoriaEvento,
                    ResultadoNegativoMedicacion = Model.ResultadoNegativoMedicacion,
                    ConfirmacionEventoAdverso = Model.ConfirmacionEventoAdverso,
                    SeveridadDesenlace = Model.SeveridadDesenlace,
                    ProbabilidadRepeticion = Model.ProbabilidadRepeticion,
                    ConceptoAuditoria = Model.ConceptoAuditoria,
                    GestionRegional = Model.GestionRegional,
                    PlanMejoraGenerado = Model.PlanMejoraGenerado,
                    CostoNoCalidad = Model.CostoNoCalidad,
                    DescripcionCostoNoCalidad = Model.DescripcionCostoNoCalidad,
                    estado_evento = 1,
                    fecha_digita = DateTime.Now,
                    usuario_digita = SesionVar.UserName
                };

                if (Model.id_evento != 0 && Model.id_evento != null)
                {
                    evento.id_evento = (int)Model.id_evento;
                    gestion = BusClass.ActualizarRegistroEventosSalud(evento);
                }
                else
                {
                    evento.id_cargue = 0;
                    gestion = BusClass.InsertarEventoSalud(evento);
                }

                if (gestion != 0)
                {
                    idEvento = gestion;

                    if (Model.PlanMejoraGenerado == 1)
                    {
                        ecop_plan_de_mejora plan = new ecop_plan_de_mejora()
                        {
                            id_eventos_salud = gestion,
                            estado_plan = 0,
                            fecha_ingreso = DateTime.Now,
                            usuario_ingreso = SesionVar.UserName
                        };

                        if (Model.id_planMejora == 0 || Model.id_planMejora == null)
                        {
                            var ingresoPlan = BusClass.InsertarPlanMejora(plan, ref MsgRes);
                            if (ingresoPlan != 0)
                            {
                                idPlan = ingresoPlan;
                                var actualizaPMevento = BusClass.ActualizarRegistroEventosSaludPM(evento.id_evento, ingresoPlan);
                            }
                        }
                    }

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

            return Json(new { mensaje = mensaje, rta = rta, idEvento = idEvento, idPlan = idPlan });
        }

        public ActionResult TableroEventos(int? mes, int? año, int? rta, string msg)
        {
            List<management_eventosSalud_tableroResult> lista = new List<management_eventosSalud_tableroResult>();

            List<management_eventosSalud_tableroResult> listaConstruccion = new List<management_eventosSalud_tableroResult>();
            List<management_eventosSalud_tableroResult> listaConstruccionPM = new List<management_eventosSalud_tableroResult>();
            List<management_eventosSalud_tableroResult> listaConcurrenciaConstruccion = new List<management_eventosSalud_tableroResult>();

            List<management_eventosSalud_tableroResult> listaCompletados = new List<management_eventosSalud_tableroResult>();
            List<management_eventosSalud_tableroResult> listaPM = new List<management_eventosSalud_tableroResult>();
            List<management_eventosSalud_tableroResult> listaConcurrencia = new List<management_eventosSalud_tableroResult>();

            try
            {
                lista = BusClass.ListadoEventosEnSaludTablero();

                listaConstruccion = lista.Where(x => x.estado_evento == 1 && x.id_planMejora == 0 && x.id_concurrencia == 0).ToList();
                listaConcurrenciaConstruccion = lista.Where(x => x.estado_evento == 1 && (x.id_concurrencia != null && x.id_concurrencia != 0)).ToList();
                listaConstruccionPM = lista.Where(x => x.estado_evento == 1 && (x.id_planMejora != 0 && x.id_planMejora != 0) ).ToList();

                listaCompletados = lista.Where(x => x.estado_evento == 2 && x.id_planMejora == 0 && (x.id_concurrencia == null || x.id_concurrencia == 0)).ToList();
                listaConcurrencia = lista.Where(x => x.estado_evento == 2 && (x.id_concurrencia != null && x.id_concurrencia != 0)).ToList();
                listaPM = lista.Where(x => x.estado_evento == 2 && (x.id_planMejora != 0 && x.id_planMejora != 0)).ToList();
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaConstruccion = listaConstruccion;
            ViewBag.listaConcurrenciaConstruccion = listaConcurrenciaConstruccion;
            ViewBag.listaConstruccionPM = listaConstruccionPM;
            ViewBag.listaConcurrencia = listaConcurrencia;
            ViewBag.listaCompletados = listaCompletados;
            ViewBag.listaPM = listaPM;

            ViewBag.conteoListaConstruccion = listaConstruccion.Count();
            ViewBag.conteoListaConcurrenciaConstruccion = listaConcurrenciaConstruccion.Count();
            ViewBag.conteoListaConstruccionPM = listaConstruccionPM.Count();

            ViewBag.conteoListaCompletados = listaCompletados.Count();
            ViewBag.conteoListaConcurrencia = listaConcurrencia.Count();
            ViewBag.conteoListaPM = listaPM.Count();

            ViewBag.conteo = lista.Count();


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

                    Sheet.Cells["A1:AG1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:AG1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:AG1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:AG1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:AG1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:AG1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:AG1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Año";
                    Sheet.Cells["B1"].Value = "Mes";
                    Sheet.Cells["C1"].Value = "Fecha de reporte";
                    Sheet.Cells["D1"].Value = "Fecha de ocurrencia del evento";
                    Sheet.Cells["E1"].Value = "Regional que reporte";
                    Sheet.Cells["F1"].Value = "Localidad de servicios de salud";
                    Sheet.Cells["G1"].Value = "Nombre del reportante";
                    Sheet.Cells["H1"].Value = "Identificación del reportante";
                    Sheet.Cells["I1"].Value = "Nombre del prestador donde ocurrió el evento";
                    Sheet.Cells["J1"].Value = "Código SAP del prestador (si aplica)";
                    Sheet.Cells["K1"].Value = "Nombre del municipio";
                    Sheet.Cells["L1"].Value = "Código municipal";
                    Sheet.Cells["M1"].Value = "Regional del beneficiario";
                    Sheet.Cells["N1"].Value = "Tipo identificación";
                    Sheet.Cells["O1"].Value = "Número de identificación";
                    Sheet.Cells["P1"].Value = "Nombre completo";
                    Sheet.Cells["Q1"].Value = "Edad";
                    Sheet.Cells["R1"].Value = "Fuente del reporte";
                    Sheet.Cells["S1"].Value = "Ámbito de ocurrencia del evento";
                    Sheet.Cells["T1"].Value = "Clasificación del evento";
                    Sheet.Cells["U1"].Value = "Categoría del evento";
                    Sheet.Cells["V1"].Value = "Subcategoría del evento";
                    Sheet.Cells["W1"].Value = "Resultado negativo de la medicación";
                    Sheet.Cells["X1"].Value = "Confirmación del evento adverso";
                    Sheet.Cells["Y1"].Value = "Severidad del desenlace";
                    Sheet.Cells["Z1"].Value = "Probabilidad de repetición";
                    Sheet.Cells["AA1"].Value = "Descripción del evento";
                    Sheet.Cells["AB1"].Value = "Concepto de auditoría";
                    Sheet.Cells["AC1"].Value = "¿Se generó Plan de Mejora al Prestador? (Sí / No)";
                    Sheet.Cells["AD1"].Value = "Costo de no calidad";
                    Sheet.Cells["AE1"].Value = "Descripción del costo de no calidad";
                    Sheet.Cells["AF1"].Value = "Gestión realizada por la regional";
                    Sheet.Cells["AG1"].Value = "Fecha gestión";

                    int row = 2;

                    foreach (management_eventosSalud_tableroResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":AG" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.Año;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.IdMes;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.FechaReporte;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.FechaOcurrenciaEvento;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.RegionalReporta;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.LocalidadServiciosSalud;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.NombreReportante;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.IdentificacionReportante;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.NombrePrestadorEvento;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.CodigoSAPPrestador;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.NombreMunicipio;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.CodigoMunicipal;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.RegionalBeneficiario;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.TipoIdentificacion;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.NumeroIdentificacion;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.NombreCompleto;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.Edad;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.FuenteReporte;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.AmbitoOcurrenciaEvento;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.ClasificacionEvento;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.CategoriaEvento;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.SubcategoriaEvento;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.ResultadoNegativoMedicacion;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.ConfirmacionEventoAdverso;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.SeveridadDesenlace;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.ProbabilidadRepeticion;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.DescripcionEvento;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.ConceptoAuditoria;
                        Sheet.Cells[string.Format("AC{0}", row)].Value = item.PlanMejoraGenerado;
                        Sheet.Cells[string.Format("AD{0}", row)].Value = item.CostoNoCalidad;
                        Sheet.Cells[string.Format("AE{0}", row)].Value = item.DescripcionCostoNoCalidad;
                        Sheet.Cells[string.Format("AF{0}", row)].Value = item.GestionRegional;
                        Sheet.Cells[string.Format("AG{0}", row)].Value = item.fecha_digita;

                        Sheet.Cells[string.Format("C{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("AG{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReporteEventosSalud";
                    Sheet.Cells["A:AG"].AutoFitColumns();
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
