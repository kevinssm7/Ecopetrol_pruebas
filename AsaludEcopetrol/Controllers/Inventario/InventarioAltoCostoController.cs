using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using Aspose.Cells;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using static AsaludEcopetrol.Controllers.Medicamentos.MedicamentosCalidadController;

namespace AsaludEcopetrol.Controllers.Inventario
{
    [SessionExpireFilter]
    public class InventarioAltoCostoController : Controller
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

        public ActionResult CargueMasivoAltoCosto()
        {
            Models.InventarioAltoCosto.inventarioAltoCosto Model = new Models.InventarioAltoCosto.inventarioAltoCosto();
            return View();
        }

        [HttpPost]
        public ActionResult CargueMasivoAltoCosto(List<HttpPostedFileBase> archivos)
        {
            var mensaje = "";
            int lote = 0;
            Models.InventarioAltoCosto.inventarioAltoCosto Model = new Models.InventarioAltoCosto.inventarioAltoCosto();
            var path = "";
            try
            {
                if (archivos != null)
                {
                    if (archivos.Count() > 0)
                    {
                        HttpPostedFileBase archivo = archivos[0];

                        string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos"];
                        var fileName = Path.GetFileName(archivo.FileName);
                        path = Path.Combine(ruta, fileName);

                        if (!Directory.Exists(ruta))
                        {
                            Directory.CreateDirectory(ruta);
                        }

                        archivo.SaveAs(path);

                        inventario_altoCosto_carguebase obj = new inventario_altoCosto_carguebase
                        {
                            fecha_digita = DateTime.Now,
                            usuario_digita = SesionVar.UserName
                        };

                        lote = Model.estructuraExcelCargueInventarioAltoCosto(path, obj, ref MsgRes);

                        var resultado = MsgRes.ResponseType;
                        var mensajeSalida = MsgRes.DescriptionResponse;
                        var idUsuario = SesionVar.IDUser;

                        if (resultado == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            ViewData["MensajeRta"] = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE CARGUE MASIVO ALTO COSTO #" + lote + " </div>";
                        }
                        else
                        {
                            mensaje = "ERROR AL INGRESAR CARGUE PQRS: " + MsgRes.DescriptionResponse;
                            ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                        }
                    }
                    else
                    {
                        mensaje = "SELECCIONE UN ARCHIVO VALIDO.";
                        ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                    }
                }
                else
                {
                    mensaje = "SELECCIONE UN ARCHIVO VALIDO.";
                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                }
            }
            catch (Exception ex)
            {
                var error = ex;
                mensaje = "ERROR EN EL CARGUE MASIVO DE ALTO COSTO: " + error;
                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
            }

            FileInfo fileDelete = new FileInfo(path);
            if (fileDelete != null)
            {
                fileDelete.Delete();
            }

            return View();
        }

        public ActionResult TableroControlAltoCosto()
        {

            List<management_inventario_altoCosto_tableroResult> lista = new List<management_inventario_altoCosto_tableroResult>();
            List<management_inventario_altoCosto_tableroResult> listaGestioAbierta = new List<management_inventario_altoCosto_tableroResult>();
            List<management_inventario_altoCosto_tableroResult> listaGestioCerrada = new List<management_inventario_altoCosto_tableroResult>();
            List<management_inventario_altoCosto_tableroResult> listaGestionPendiente = new List<management_inventario_altoCosto_tableroResult>();
            var conteo = 0;
            var conteoAbiertas = 0;
            var conteoCerradas = 0;
            var conteoPendientes = 0;
            try
            {
                lista = BusClass.ListadoInventarioAltoCosto();
                listaGestionPendiente = lista.Where(x => x.tipoRegistro == 3).ToList();
                listaGestioAbierta = lista.Where(x => x.tipoRegistro == 1).ToList();
                listaGestioCerrada = lista.Where(x => x.tipoRegistro == 2).ToList();

                conteo = lista.Count();
                conteoPendientes = listaGestionPendiente.Count();
                conteoAbiertas = listaGestioAbierta.Count();
                conteoCerradas = listaGestioCerrada.Count();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = lista;

            Session["ListaTableroAltoCosto"] = lista;

            ViewBag.listaGestionPendiente = listaGestionPendiente;
            ViewBag.listaGestioAbierta = listaGestioAbierta;
            ViewBag.listaGestioCerrada = listaGestioCerrada;
            ViewBag.conteo = conteo;
            ViewBag.conteoAbiertas = conteoAbiertas;
            ViewBag.conteoCerradas = conteoCerradas;
            ViewBag.conteoPendientes = conteoPendientes;

            return View();
        }

        public void ExportarDatosTableroControlAC()
        {
            List<management_inventario_altoCosto_tableroResult> Lista = new List<management_inventario_altoCosto_tableroResult>();
            string proye;

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                Lista = (List<management_inventario_altoCosto_tableroResult>)Session["ListaTableroAltoCosto"];
                proye = "InventarioAltoCostoCancer";

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
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(proye);

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:AB1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:AB1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:AB1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:AB1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:AB1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:AB1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:AB1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Nro inventario";
                    Sheet.Cells["B1"].Value = "Nro detalle";
                    Sheet.Cells["C1"].Value = "Ccagrupador";
                    Sheet.Cells["D1"].Value = "Requerido - obligatorio";
                    Sheet.Cells["E1"].Value = "Tipo de caso";
                    Sheet.Cells["F1"].Value = "Coordinación";
                    Sheet.Cells["G1"].Value = "Activo población";
                    Sheet.Cells["H1"].Value = "Fecha fallecimiento";
                    Sheet.Cells["I1"].Value = "Causa fallecimiento";
                    Sheet.Cells["J1"].Value = "Fecha desafiliación";
                    Sheet.Cells["K1"].Value = "Primer nombre del usuario";
                    Sheet.Cells["L1"].Value = "Segundo nombre del usuario";
                    Sheet.Cells["M1"].Value = "Primer apellido del usuario";
                    Sheet.Cells["N1"].Value = "Segundo apellido del usuario";
                    Sheet.Cells["O1"].Value = "Tipo de Identificación del usuario";
                    Sheet.Cells["P1"].Value = "Número de identificación del usuario";
                    Sheet.Cells["Q1"].Value = "Fecha de nacimiento";
                    Sheet.Cells["R1"].Value = "Sexo";
                    Sheet.Cells["S1"].Value = "Ocupación";
                    Sheet.Cells["T1"].Value = "Régimen de afiliación AL SGSSS";
                    Sheet.Cells["U1"].Value = "Código de la EAPB o de la entidad territorial";
                    Sheet.Cells["V1"].Value = "Código pertenencia étnica";
                    Sheet.Cells["W1"].Value = "Grupo poblacional";
                    Sheet.Cells["X1"].Value = "Municipio de residencia";
                    Sheet.Cells["Y1"].Value = "Número telefónico del paciente";
                    Sheet.Cells["Z1"].Value = "Fecha de afiliación a la EAPB que reporta";
                    Sheet.Cells["AA1"].Value = "Código CIE - 10 de la neoplasia";
                    Sheet.Cells["AB1"].Value = "Estado";

                    int row = 2;

                    foreach (management_inventario_altoCosto_tableroResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":N" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_inventario;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.id_detalle;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.ccagrupador;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.requerido_obligatorio;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.tipo_caso;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.coordinacion;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.activo_poblacion;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_fallecimiento;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.causa_fallecimiento;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_desafiliacion;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.primer_nombre_usuario;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.segundo_nombre_usuario;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.primer_apellido_usuario;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.segundo_apellido_usuario;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.tipo_identificacion_usuario;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.numero_identificacion_usuario;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.fecha_nacimiento;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.sexo;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.ocupacion;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.regimen_afiliacion;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.codigo_EAPB;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.codigo_pertenencia_etnica;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.grupo_poblacional;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.municipio_residencia;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.numero_telefonico_paciente;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.fecha_afiliacion_EAPB;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.cod_cie10_neoplasia;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.tipoRegistroDetalle;

                        //Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        //Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        //Sheet.Cells[string.Format("Q{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        //Sheet.Cells[string.Format("Z{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile;
                    namefile = "InventarioAltoCostoCancer_" + DateTime.Now;

                    Sheet.Cells["A:AB"].AutoFitColumns();
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
                   "window.alert('ERROR EN LA DESCARGA');" +
                   "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public ActionResult GestionInventario(int? idDetalle, int? idInventario, string msg, int? rta, int? idGestion)
        {
            Models.InventarioAltoCosto.inventarioAltoCosto Model = new Models.InventarioAltoCosto.inventarioAltoCosto();
            List<ref_inventario_altoCostoCancer_atc> listaATC = new List<ref_inventario_altoCostoCancer_atc>();
            List<inventario_altoCosto_gestiones> listaGestionadas = new List<inventario_altoCosto_gestiones>();

            var cerrado = 0;

            inventario_altoCosto_gestiones datoGestion = new inventario_altoCosto_gestiones();
            inventario_altoCosto_gestiones datoCerrado = new inventario_altoCosto_gestiones();

            var conteo = 0;
            try
            {
                if (rta == 1 || rta == 2)
                {
                    ViewBag.msg = msg;
                    ViewBag.rta = rta;
                }
                else
                {
                    ViewBag.rta = 0;
                }

                listaGestionadas = BusClass.listaInvAltoCostoGestionadas(idDetalle);
                conteo = listaGestionadas.Count();

                listaATC = BusClass.listadoInventarioATC();

                if (idGestion != null)
                {
                    datoGestion = BusClass.DatoInvAltoCostoGestionID(idGestion);
                }
                else
                {
                    datoGestion = BusClass.DatoUltimoInvAltoCostoGestionID(idDetalle);
                }

                datoCerrado = BusClass.DatoUltimoInvAltoCostoGestionID(idDetalle);

                if (datoCerrado != null)
                {
                    cerrado = (int)datoCerrado.cerrado;
                }

                if (datoGestion == null)
                {
                    datoGestion = new inventario_altoCosto_gestiones();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idDetalle = idDetalle;
            ViewBag.idInventario = idInventario;
            ViewBag.idGestion = idGestion;
            ViewBag.atc = listaATC;
            ViewBag.conteoGestionadas = conteo;
            ViewBag.listaGestionadas = listaGestionadas;
            ViewBag.cerrado = cerrado;

            return View(datoGestion);
        }

        [HttpPost]
        public ActionResult GestionInventario(Models.InventarioAltoCosto.inventarioAltoCosto model)
        {
            inventario_altoCosto_gestiones obj = new inventario_altoCosto_gestiones();

            var mensaje = "";
            var rta = 0;

            ViewBag.msg = "";
            ViewBag.rta = 0;

            try
            {
                obj.id_inventario = model.id_inventario;
                obj.id_detalle = model.id_detalle;
                obj.fecha_diagnostico = model.fecha_diagnostico;
                obj.fecha_remision = model.fecha_remision;
                obj.fecha_ingreso = model.fecha_ingreso;
                obj.tipo_estudio = model.tipo_estudio;
                obj.motivo_no_diagnostico_usuario = model.motivo_no_diagnostico_usuario;
                obj.fecha_recoleccion_muestra = model.fecha_recoleccion_muestra;
                obj.fecha_primer_informeHispatologico = model.fecha_primer_informeHispatologico;
                obj.codigo_habillitacion_ips = model.codigo_habillitacion_ips;
                obj.fecha_consulta_medicoTratante = model.fecha_consulta_medicoTratante;
                obj.histologia_tumor = model.histologia_tumor;
                obj.grado_diferenciacion_tumor = model.grado_diferenciacion_tumor;
                obj.estadificacion_tnm_figo = model.estadificacion_tnm_figo;
                obj.fecha_estadificacion = model.fecha_estadificacion;
                obj.se_realiza_her1 = model.se_realiza_her1;
                obj.fecha_realizacion_her1 = model.fecha_realizacion_her1;
                obj.resultado_prueba_her1 = model.resultado_prueba_her1;
                obj.estadificacion_dukes = model.estadificacion_dukes;
                obj.fecha_estadificacion_dukes = model.fecha_estadificacion_dukes;
                obj.estadificacion_hodkin = model.estadificacion_hodkin;
                obj.clasificacion_escala_gleason = model.clasificacion_escala_gleason;
                obj.clasificacion_riesgo_leucemialinfoma = model.clasificacion_riesgo_leucemialinfoma;
                obj.fecha_clasificacion_riesgo = model.fecha_clasificacion_riesgo;
                obj.objetivo_tratamiento_inicial = model.objetivo_tratamiento_inicial;
                obj.objetivo_intervencion_periodo_prueba = model.objetivo_intervencion_periodo_prueba;
                obj.antecedentes_otro_cancer_primario = model.antecedentes_otro_cancer_primario;
                obj.fecha_otro_cancer_primario = model.fecha_otro_cancer_primario;
                obj.tipo_cie10_otro_cancer = model.tipo_cie10_otro_cancer;
                obj.usuario_recibio_quimioterapia = model.usuario_recibio_quimioterapia;
                obj.cuantas_quimioterapias_recibioUsuario = model.cuantas_quimioterapias_recibioUsuario;
                obj.usuario_recibio_citorreduccion = model.usuario_recibio_citorreduccion;
                obj.usuario_recibio_induccion = model.usuario_recibio_induccion;
                obj.usuario_recibio_intensificacion = model.usuario_recibio_intensificacion;
                obj.usuario_recibio_consolidacion = model.usuario_recibio_consolidacion;
                obj.usuario_recibio_reinduccion = model.usuario_recibio_reinduccion;
                obj.usuario_recibio_mantenimiento = model.usuario_recibio_mantenimiento;
                obj.usuario_recibio_mantenimiento_largo = model.usuario_recibio_mantenimiento_largo;
                obj.usuario_recibio_otra_fase = model.usuario_recibio_otra_fase;

                obj.numero_ciclos_iniciados = model.numero_ciclos_iniciados;
                obj.ubicacion_temporal_esquema_quimioterapia = model.ubicacion_temporal_esquema_quimioterapia;
                obj.fechaInicio_temporal_esquema_quimioterapia1 = model.fechaInicio_temporal_esquema_quimioterapia1;
                obj.numero_ips = model.numero_ips;
                obj.codigo_ips1 = model.codigo_ips1;
                obj.codigo_ips2 = model.codigo_ips2;
                obj.cuantos_medicamentos_neoplasicos = model.cuantos_medicamentos_neoplasicos;
                obj.medicamento_antineoplasico_suministrado_1 = model.medicamento_antineoplasico_suministrado_1;
                obj.medicamento_antineoplasico_suministrado_2 = model.medicamento_antineoplasico_suministrado_2;
                obj.medicamento_antineoplasico_suministrado_3 = model.medicamento_antineoplasico_suministrado_3;
                obj.medicamento_antineoplasico_suministrado_4 = model.medicamento_antineoplasico_suministrado_4;
                obj.medicamento_antineoplasico_suministrado_5 = model.medicamento_antineoplasico_suministrado_5;
                obj.recibio_ciclosporina = model.recibio_ciclosporina;
                obj.medicamento_antineoplasico_suministrado_7 = model.medicamento_antineoplasico_suministrado_7;
                obj.medicamento_antineoplasico_suministrado_8 = model.medicamento_antineoplasico_suministrado_8;
                obj.medicamento_antineoplasico_suministrado_9 = model.medicamento_antineoplasico_suministrado_9;
                obj.medicamento_antineoplasico_suministrado_adicional_1 = model.medicamento_antineoplasico_suministrado_adicional_1;
                obj.medicamento_antineoplasico_suministrado_adicional_2 = model.medicamento_antineoplasico_suministrado_adicional_2;
                obj.medicamento_antineoplasico_suministrado_adicional_3 = model.medicamento_antineoplasico_suministrado_adicional_3;
                obj.recibio_quimioterapia_intratecal = model.recibio_quimioterapia_intratecal;
                obj.fecha_finalizacion_periodo_reporte = model.fecha_finalizacion_periodo_reporte;
                obj.caracteristicas_actuales_esquema_periodo = model.caracteristicas_actuales_esquema_periodo;
                obj.motivo_finalizacion_prematura = model.motivo_finalizacion_prematura;
                obj.ubicacion_temporal_ultimoesquema = model.ubicacion_temporal_ultimoesquema;
                obj.fecha_inicio_ultimo_esquema = model.fecha_inicio_ultimo_esquema;
                obj.numero_ips_suministra_ultimoesquema = model.numero_ips_suministra_ultimoesquema;
                obj.codigo_ips_suministra_ultimoesquema_1 = model.codigo_ips_suministra_ultimoesquema_1;
                obj.codigo_ips_suministra_ultimoesquema_2 = model.codigo_ips_suministra_ultimoesquema_2;
                obj.cuantos_tratamientos_antineoplasicos_propusieron = model.cuantos_tratamientos_antineoplasicos_propusieron;
                obj.antineoplasico_administradoUsuario_ultimoesquema_1 = model.antineoplasico_administradoUsuario_ultimoesquema_1;
                obj.antineoplasico_administradoUsuario_ultimoesquema_2 = model.antineoplasico_administradoUsuario_ultimoesquema_2;
                obj.antineoplasico_administradoUsuario_ultimoesquema_3 = model.antineoplasico_administradoUsuario_ultimoesquema_3;
                obj.antineoplasico_administradoUsuario_ultimoesquema_4 = model.antineoplasico_administradoUsuario_ultimoesquema_4;
                obj.antineoplasico_administradoUsuario_ultimoesquema_5 = model.antineoplasico_administradoUsuario_ultimoesquema_5;
                obj.antineoplasico_administradoUsuario_ultimoesquema_6 = model.antineoplasico_administradoUsuario_ultimoesquema_6;
                obj.antineoplasico_administradoUsuario_ultimoesquema_7 = model.antineoplasico_administradoUsuario_ultimoesquema_7;
                obj.antineoplasico_administradoUsuario_ultimoesquema_8 = model.antineoplasico_administradoUsuario_ultimoesquema_8;
                obj.antineoplasico_administradoUsuario_ultimoesquema_9 = model.antineoplasico_administradoUsuario_ultimoesquema_9;
                obj.antineoplasico_administradoUsuario_ultimoesquema_adicional_1 = model.antineoplasico_administradoUsuario_ultimoesquema_adicional_1;
                obj.antineoplasico_administradoUsuario_ultimoesquema_adicional_2 = model.antineoplasico_administradoUsuario_ultimoesquema_adicional_2;
                obj.antineoplasico_administradoUsuario_ultimoesquema_adicional_3 = model.antineoplasico_administradoUsuario_ultimoesquema_adicional_3;
                obj.recibio_terapiaIntratecal_ultimoperiodo = model.recibio_terapiaIntratecal_ultimoperiodo;
                obj.fecha_finalizacionUltimoEsquema_quimioterapia_terapiasistemica = model.fecha_finalizacionUltimoEsquema_quimioterapia_terapiasistemica;
                obj.caracteristicas_actuales_ultimoPeriodo = model.caracteristicas_actuales_ultimoPeriodo;
                obj.motivo_fializacionPrematura_ultimoEsquema = model.motivo_fializacionPrematura_ultimoEsquema;
                obj.usuario_sometidoa_cirugias_paliativasCurativas = model.usuario_sometidoa_cirugias_paliativasCurativas;
                obj.numeroCirugias_sometidoUsuario_ultimo = model.numeroCirugias_sometidoUsuario_ultimo;
                obj.fecha_realizacionprimeracirugia_periodoreporte = model.fecha_realizacionprimeracirugia_periodoreporte;
                obj.codigo_ips_realizo_primercirugia = model.codigo_ips_realizo_primercirugia;
                obj.codigo_primeracirugia_periodoactual = model.codigo_primeracirugia_periodoactual;
                obj.ubicaciontemporal_manejo_oncologico_primeraCirugia = model.ubicaciontemporal_manejo_oncologico_primeraCirugia;
                obj.fecharealizacion_ultimacirugia_reintervencion = model.fecharealizacion_ultimacirugia_reintervencion;
                obj.motivo_ultimacirugia = model.motivo_ultimacirugia;
                obj.codigo_ips_realizo_ultimacirugia = model.codigo_ips_realizo_ultimacirugia;
                obj.codigo_ultimacirugia_periodoactual = model.codigo_ultimacirugia_periodoactual;
                obj.ubicaciontemporal_manejooncologico_ultimacirugia = model.ubicaciontemporal_manejooncologico_ultimacirugia;
                obj.estadovital_finalizarultimacirugia = model.estadovital_finalizarultimacirugia;
                obj.usuariorecibio_radioterapia_periodoreporte_actual = model.usuariorecibio_radioterapia_periodoreporte_actual;
                obj.numero_sesionesradioterapia_recibidas_periodo = model.numero_sesionesradioterapia_recibidas_periodo;
                obj.fechaInicio_primerounicoesquema_tiporadioterapia = model.fechaInicio_primerounicoesquema_tiporadioterapia;
                obj.ubicaciontemporal_primerunico_esquemaradioterapia_tratamientoOncologico = model.ubicaciontemporal_primerunico_esquemaradioterapia_tratamientoOncologico;
                obj.tipo_radioterapia_aplicada_primeresquema = model.tipo_radioterapia_aplicada_primeresquema;
                obj.numeroips_suministranprimeresquema_radioterapia = model.numeroips_suministranprimeresquema_radioterapia;
                obj.codigo_ips1_suministraprimeresquema_radioterapia_1 = model.codigo_ips1_suministraprimeresquema_radioterapia_1;
                obj.codigo_ips1_suministraprimeresquema_radioterapia_2 = model.codigo_ips1_suministraprimeresquema_radioterapia_2;
                obj.fecha_finalizacion_primeresquema_radioterapia = model.fecha_finalizacion_primeresquema_radioterapia;
                obj.caracteristicas_actuales_primeresquema_radioterapia = model.caracteristicas_actuales_primeresquema_radioterapia;
                obj.motivo_fianlizacionprimeresquema_radioterapia = model.motivo_fianlizacionprimeresquema_radioterapia;
                obj.fechaInicio_ultimounicoesquema_tiporadioterapia = model.fechaInicio_ultimounicoesquema_tiporadioterapia;
                obj.ubicaciontemporal_ultimounico_esquemaradioterapia_tratamientoOncologico = model.ubicaciontemporal_ultimounico_esquemaradioterapia_tratamientoOncologico;
                obj.tipo_radioterapia_aplicada_ultimoesquema = model.tipo_radioterapia_aplicada_ultimoesquema;
                obj.numeroips_suministranultimoesquema_radioterapia = model.numeroips_suministranultimoesquema_radioterapia;
                obj.codigo_ips1_suministraultimoesquema_radioterapia_1 = model.codigo_ips1_suministraultimoesquema_radioterapia_1;
                obj.codigo_ips1_suministraultimoesquema_radioterapia_2 = model.codigo_ips1_suministraultimoesquema_radioterapia_2;
                obj.fecha_finalizacion_ultimoesquema_radioterapia = model.fecha_finalizacion_ultimoesquema_radioterapia;
                obj.caracteristicasactuales_ultimoesquema_radioterapia = model.caracteristicasactuales_ultimoesquema_radioterapia;
                obj.motivo_fianlizacionultimoesquema_radioterapia = model.motivo_fianlizacionultimoesquema_radioterapia;
                obj.recibiousuario_transplantecelulas_progenitorashematopoyetica_periodoactual = model.recibiousuario_transplantecelulas_progenitorashematopoyetica_periodoactual;
                obj.tipo_transplante_recibido = model.tipo_transplante_recibido;
                obj.ubicacion_temporal_transplante_relaciononcologico = model.ubicacion_temporal_transplante_relaciononcologico;
                obj.fecha_transplante = model.fecha_transplante;
                obj.codigo_ips_realizo_transplante = model.codigo_ips_realizo_transplante;
                obj.usuario_recibiocirugia_reconstructiva_periodoactual = model.usuario_recibiocirugia_reconstructiva_periodoactual;
                obj.fecha_cirugia_reconstructiva = model.fecha_cirugia_reconstructiva;
                obj.codigo_ips_realizocirugia_reconstructiva = model.codigo_ips_realizocirugia_reconstructiva;
                obj.usuario_valoradoconsulta_procedimientocuidado_paliativo = model.usuario_valoradoconsulta_procedimientocuidado_paliativo;
                obj.usuario_valoradoconsulta_procedimientocuidado_paliativo_medicoespecialista = model.usuario_valoradoconsulta_procedimientocuidado_paliativo_medicoespecialista;
                obj.usuario_valoradoconsulta_procedimientocuidado_paliativo_nomedico = model.usuario_valoradoconsulta_procedimientocuidado_paliativo_nomedico;
                obj.usuario_valoradoconsulta_procedimientocuidado_paliativo_otraespecialidad = model.usuario_valoradoconsulta_procedimientocuidado_paliativo_otraespecialidad;
                obj.usuario_recibioconsulta_procedimiento_cuidadopaliativo_medicogeneral = model.usuario_recibioconsulta_procedimiento_cuidadopaliativo_medicogeneral;
                obj.usuario_recibioconsulta_procedimiento_cuidadopaliativo_trabajosocial = model.usuario_recibioconsulta_procedimiento_cuidadopaliativo_trabajosocial;
                obj.usuario_recibioconsulta_procedimiento_cuidadopaliativo_otroprofesional = model.usuario_recibioconsulta_procedimiento_cuidadopaliativo_otroprofesional;
                obj.fecha_primeraconsulta_procedimientocuidado_paliativo = model.fecha_primeraconsulta_procedimientocuidado_paliativo;
                obj.codigo_ips__recibeatencion_cuidadopaliativo = model.codigo_ips__recibeatencion_cuidadopaliativo;
                obj.usuario_valoradoservicio_psiquiatria = model.usuario_valoradoservicio_psiquiatria;
                obj.fecha_primeraconsulta_serviciopsiquiatria = model.fecha_primeraconsulta_serviciopsiquiatria;
                obj.codigo_ipsdonde_recibeprimeravaloracion_psiquiatria = model.codigo_ipsdonde_recibeprimeravaloracion_psiquiatria;
                obj.usuario_valoradoporprofesional_120_nutricion = model.usuario_valoradoporprofesional_120_nutricion;
                obj.fecha_consultaInicial_nutricion = model.fecha_consultaInicial_nutricion;
                obj.codigo_ips_recibevaloracion_nutricion = model.codigo_ips_recibevaloracion_nutricion;
                obj.usuario_recibiosoporte_nutricional = model.usuario_recibiosoporte_nutricional;
                obj.usuario_recibeterapias_complementarias_rehabilitacion = model.usuario_recibeterapias_complementarias_rehabilitacion;
                obj.tipo_tratamiento_estarecibiendo_usuario = model.tipo_tratamiento_estarecibiendo_usuario;
                obj.resultadofinal_resultadooncologico_usuarioesta = model.resultadofinal_resultadooncologico_usuarioesta;
                obj.estadovital_periodoreporte = model.estadovital_periodoreporte;
                obj.novedadadministrativa_usuariorespectoreporteanterior = model.novedadadministrativa_usuariorespectoreporteanterior;
                obj.novedadclinica_usuario_afechacorte = model.novedadclinica_usuario_afechacorte;
                obj.fecha_desafiliacion_EAPB = model.fecha_desafiliacion_EAPB;
                obj.fecha_muerte = model.fecha_muerte;
                obj.causa_muerte = model.causa_muerte;
                obj.codigo_unico_identificacion_BDUA_BDEX_PVS = model.codigo_unico_identificacion_BDUA_BDEX_PVS;
                obj.fecha_corte = model.fecha_corte;
                obj.auditor_asignado = model.auditor_asignado;
                obj.observaciones = model.observaciones;
                obj.regional = model.regional;
                obj.cerrado = model.cerrado;
                obj.fecha_gestion = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                var resultado = BusClass.insercionGestionInventario(obj, ref MsgRes);
                var descripcion = MsgRes.DescriptionResponse;

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    List<inventario_altoCosto_archivos> ListaArchivos = new List<inventario_altoCosto_archivos>();

                    IList<HttpPostedFileBase> files = Request.Files.GetMultiple("file");
                    if (files != null)
                    {

                        if (files.Count() > 0)
                        {
                            try
                            {
                                for (var i = 0; i < files.Count(); i++)
                                {
                                    HttpPostedFileBase file = files[i];
                                    inventario_altoCosto_archivos Archivo = new inventario_altoCosto_archivos();

                                    if (file.ContentLength > 0)
                                    {
                                        Archivo = DevolverGestionArchivoAltoCosto(model.id_detalle, resultado, file);
                                        if (Archivo != null)
                                        {
                                            ListaArchivos.Add(Archivo);
                                        }
                                    }
                                }

                                if (ListaArchivos.Count() > 0)
                                {
                                    var insercionArchivos = BusClass.InsertarArchivoisAltoCostoCancer(ListaArchivos, ref MsgRes);
                                }

                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }
                        }
                    }

                    mensaje = "GESTIÓN INGRESADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN LA INSERCIÓN DE LA GESTIÓN: " + descripcion;
                    rta = 2;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                rta = 2;
                mensaje = "ERROR EN LA INSERCIÓN DE LA GESTIÓN: " + error;
            }

            ViewBag.msg = mensaje;
            ViewBag.rta = rta;

            return RedirectToAction("GestionInventario", "InventarioAltoCosto", new { idInventario = model.id_inventario, idDetalle = model.id_detalle, msg = ViewBag.msg, rta = ViewBag.rta });
        }

        private inventario_altoCosto_archivos DevolverGestionArchivoAltoCosto(int? idDetalle, int? idGestion, HttpPostedFileBase file)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;
            string dirpath = "";
            inventario_altoCosto_archivos OBJ = new inventario_altoCosto_archivos();

            try
            {
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaArchivosInventarioAltoCosto"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaArchivosInventarioAltoCostoPruebas"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "AltoCostoCancer";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "AltoCostoCancerPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idDetalle + "\\" + idGestion);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                OBJ.id_detalle = idDetalle;
                OBJ.id_gestion = idGestion;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.nombre = file.FileName;
                OBJ.tipo = 1;
                OBJ.extension = file.ContentType;
                OBJ.fecha_digita = DateTime.Now;
                OBJ.usuario_digita = SesionVar.UserName;
            }

            catch (Exception ex)
            {
                var error = ex.Message;

                FileInfo fileDelete = new FileInfo(dirpath);
                if (fileDelete != null)
                {
                    fileDelete.Delete();
                }
            }

            return OBJ;
        }

        public PartialViewResult MostrarRepositorioArchivosGestion(int? idGestion, int? idDetalle)
        {
            List<management_inventario_altoCosto_verArchivosResult> lista = new List<management_inventario_altoCosto_verArchivosResult>();
            var conteo = 0;
            try
            {
                lista = BusClass.ListadoArchivosGestionados(idGestion);
                conteo = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.idGestion = idGestion;
            ViewBag.idDetalle = idDetalle;
            ViewBag.rol = SesionVar.ROL;

            return PartialView();
        }

        public ActionResult VerArchivoGestionACidArchivo(Int32 idArchivo)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                inventario_altoCosto_archivos dato = new inventario_altoCosto_archivos();
                dato = BusClass.traerArchivoAltoCostoIdArchivo(idArchivo);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extensionTipo = obj.extension;
                    var nombreFinal = obj.nombre;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    nombreFinal = nombrePartido[5];

                    var ruta = nombrePartido[0] + "\\" + nombrePartido[1] + "\\" + nombrePartido[2] + "\\" + nombrePartido[3] + "\\" + nombrePartido[4];

                    try
                    {
                        DirectoryInfo directory = new DirectoryInfo(ruta);
                        FileInfo[] files = directory.GetFiles();

                        bool fileFound = false;
                        foreach (FileInfo file in files)
                        {
                            if (String.Compare(file.Name, nombreFinal) == 0)
                            {
                                fileFound = true;
                            }
                        }

                        if (fileFound)
                        {
                            return File(dirpath, extensionTipo, nombreFinal);
                        }
                        else
                        {
                            return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "El archivo se ha movido de su posición original. No se encuentra." });
                        }

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
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public JsonResult GuardarNuevosArchivosALGestion(List<HttpPostedFileBase> archivos, int? gestionId, int? idDetalle)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                if (archivos != null)
                {
                    if (archivos.Count() > 0)
                    {
                        List<inventario_altoCosto_archivos> ListaArchivos = new List<inventario_altoCosto_archivos>();

                        try
                        {
                            for (var i = 0; i < archivos.Count(); i++)
                            {
                                HttpPostedFileBase file = archivos[i];
                                inventario_altoCosto_archivos Archivo = new inventario_altoCosto_archivos();

                                if (file.ContentLength > 0)
                                {
                                    Archivo = DevolverGestionArchivoAltoCosto(idDetalle, gestionId, file);
                                    if (Archivo != null)
                                    {
                                        ListaArchivos.Add(Archivo);
                                    }
                                }
                            }

                            if (ListaArchivos.Count() > 0)
                            {
                                var insercionArchivos = BusClass.InsertarArchivoisAltoCostoCancer(ListaArchivos, ref MsgRes);

                                if (insercionArchivos != 0 && MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                                {
                                    mensaje = "SE INGRESARON LOS ARCHIVOS CORRECTAMENTE";
                                    rta = 1;
                                }
                                else
                                {
                                    mensaje = "ERROR EN EL INGRESO DE ARCHIVOS: " + MsgRes.DescriptionResponse;
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            throw new Exception(error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE ARCHIVOS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult EliminarArchivoAC(int? idArchivo, int? idGestion)
        {
            String mensaje = "";
            try
            {
                var resultado = BusClass.eliminarArchivoAltoCostoCanceridArchivo((int)idArchivo);

                if (resultado != 0)
                {
                    log_inventario_altoCosto_eliminacionArchivos obj = new log_inventario_altoCosto_eliminacionArchivos();
                    obj.id_archivo = idArchivo;
                    obj.id_gestion = idGestion;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    var eliminacion = BusClass.InsertarLogEliminacionArchivoAltoCosto(obj);

                    mensaje = "SE ELIMINÓ CORRECTAMENTE EL ARCHIVO.";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR ARCHIVO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "ERROR AL ELIMINAR :" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ExportarDatosTableroControlACGestiones(int? idDetalle)
        {
            List<management_inventario_altoCosto_tableroGestionesResult> Lista = new List<management_inventario_altoCosto_tableroGestionesResult>();
            string proye;

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                Lista = BusClass.ListaAltoCostoGestiones(idDetalle);

                proye = "InventarioCancerGestionadas";

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
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(proye);

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:EX1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:EX1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:EX1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:EX1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:EX1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:EX1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:EX1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Nro gestion";
                    Sheet.Cells["B1"].Value = "Nro inventario";
                    Sheet.Cells["C1"].Value = "Nro detalle";
                    Sheet.Cells["D1"].Value = "Fecha de diagnóstico del cáncer reportado";
                    Sheet.Cells["E1"].Value = "Fecha de la nota de remisión o interconsulta del médico o institución general hacia la institución o médico que hizo el diagnóstico";
                    Sheet.Cells["F1"].Value = "Fecha de ingreso a la institución que realizó el diagnóstico luego de la remisión o interconsulta";
                    Sheet.Cells["G1"].Value = "Tipo de estudio con el que se realizó el diagnóstico de cáncer";
                    Sheet.Cells["H1"].Value = "Motivo por el cual el usuario no tuvo diagnóstico por histopatología  (aplica para registros con respuesta igual a 7 en la variable anterior)";
                    Sheet.Cells["I1"].Value = "Fecha de recolección de muestra para estudio histopatológico";
                    Sheet.Cells["J1"].Value = "Fecha de primero o único  informe histopatológico válido de diagnostico";
                    Sheet.Cells["K1"].Value = "Código válido de habilitación de la IPS donde se realiza la confirmación diagnóstica";
                    Sheet.Cells["L1"].Value = "Fecha de primera consulta con médico tratante de la enfermedad maligna";
                    Sheet.Cells["M1"].Value = "Histología del tumor en muestra de biopsia o quirúrgica";
                    Sheet.Cells["N1"].Value = "Grado de diferenciación del tumor sólido maligno según la biopsia o informe de primera cirugía";
                    Sheet.Cells["O1"].Value = "Si es tumor sólido, cuál fue la primera estadificación basada en TNM, FIGO, u otras compatibles con esta numeración según tumor.";
                    Sheet.Cells["P1"].Value = "Fecha en que se realizó esta estadificación";
                    Sheet.Cells["Q1"].Value = "Para cáncer de mama, ¿se le realizó a este usuario la prueba HER1 (llamado también receptor 1 del factor de crecimiento epidérmico humano, también llamado erb-B1)";
                    Sheet.Cells["R1"].Value = "Para cáncer de mama, fecha de realización de la última prueba HER1";
                    Sheet.Cells["S1"].Value = "Para cáncer de mama, resultado de la única o ultima prueba HER1";
                    Sheet.Cells["T1"].Value = "Para cáncer colorrectal, estadificación de Dukes";
                    Sheet.Cells["U1"].Value = "Fecha en que se realizó la estadificación de Dukes";
                    Sheet.Cells["V1"].Value = "Estadificación clínica  en linfoma no Hodgkin y linfoma Hodgkin adulto y pediatrico (Ann Arbor-Lugano)";
                    Sheet.Cells["W1"].Value = "Para cáncer de próstata, valor de clasificación de la escala Gleason en el momento del diagnóstico";
                    Sheet.Cells["X1"].Value = "Clasificación de riesgo leucemias o linfomas (para toda la población), y sólidos pediátricos";
                    Sheet.Cells["Y1"].Value = "Fecha de clasificación de riesgo ";
                    Sheet.Cells["Z1"].Value = "Objetivo (o intención) del tratamiento médico inicial (al diagnóstico)";
                    Sheet.Cells["AA1"].Value = "Objetivo de la intervención médica durante el periodo de reporte.";
                    Sheet.Cells["AB1"].Value = "Tiene antecedente de otro cáncer primario (es decir, tiene o tuvo otro tumor maligno diferente al que está notificando)";
                    Sheet.Cells["AC1"].Value = "Fecha de diagnóstico del otro cáncer primario";
                    Sheet.Cells["AD1"].Value = "Tipo (CIE-10)  de ese cáncer antecedente o concurrente ";
                    Sheet.Cells["AE1"].Value = "¿Recibió el usuario quimioterapia u otra terapia sistémica (incluye quimioterapia, hormonoterapia, inmunoterapia y terapia dirigida) dentro del periodo de reporte? ";
                    Sheet.Cells["AF1"].Value = "¿Cuántas fases de quimioterapia recibió el usuario en este periodo de reporte? (aplica para hematolinfáticos con los siguientes códigos de clasificación diagnóstica CIE-10; C835 Linfoma no Hodgkin linfoblástico (difuso), C910 Leucemia linfoblástica aguda, C910 Leucemia mieloide aguda, C914 Leucemia promielocitica aguda y C915 Leucemia mielomonocítica aguda)";
                    Sheet.Cells["AG1"].Value = "El usuario recibió en este periodo la fase de quimioterapia denominada Prefase o Citorreducción inicial (aplica solo para leucemia linfoide aguda y linfoma linfoblástico, puede haber recibido más de una fase)";
                    Sheet.Cells["AH1"].Value = "El usuario recibió en este periodo la fase de quimioterapia denominada Inducción (aplica solo para leucemia linfoide o mieloide aguda y linfoma linfoblástico, puede haber recibido más de una fase)";
                    Sheet.Cells["AI1"].Value = "El usuario recibió en este periodo la fase de quimioterapia denominada Intensificación (aplica solo para leucemia linfoide o mieloide aguda y linfoma linfoblástico, puede haber recibido más de una fase)";
                    Sheet.Cells["AJ1"].Value = "El usuario recibió en este periodo la fase de quimioterapia denominada Consolidación (aplica solo para leucemia linfoide o mieloide aguda y linfoma linfoblástico, puede haber recibido más de una fase)";
                    Sheet.Cells["AK1"].Value = "El usuario recibió en este periodo la fase de quimioterapia denominada Reinducción (aplica solo para leucemia linfoide aguda y linfoma linfoblástico, puede haber recibido más de una fase)";
                    Sheet.Cells["AL1"].Value = "El usuario recibió en este periodo la fase de quimioterapia denominada Mantenimiento (aplica solo para leucemia linfoide o mieloide aguda y linfoma linfoblástico, puede haber recibido más de una fase)";
                    Sheet.Cells["AM1"].Value = "El usuario recibió en este periodo la fase de quimioterapia denominada Mantenimiento largo o final (aplica solo para leucemia linfoide o mieloide aguda y linfoma linfoblástico, puede haber recibido más de una fase)";
                    Sheet.Cells["AN1"].Value = " El usuario recibió en este periodo Otra fase de quimioterapia denominada diferente a las anteriores(aplica solo para leucemia linfoide o mieloide aguda y linfoma linfoblástico, puede haber recibido más de una fase)";
                    Sheet.Cells["AO1"].Value = "Número de ciclos iniciados y administrados en el periodo de reporte, incluyendo  el que aún recibe en la fecha de finalización del periodo (aplica para todos los canceres)";
                    Sheet.Cells["AP1"].Value = "Ubicación temporal del primer o único esquema de quimioterapia o terapia sistémica en el periodo en relación al manejo oncológico";
                    Sheet.Cells["AQ1"].Value = "Fecha de inicio del primer o único esquema de quimioterapia o terapia sistémica que recibió en este periodo. Este esquema pudo haber sido iniciado antes de periodo de reporte";
                    Sheet.Cells["AR1"].Value = "Número de IPS que suministran el primer o único esquema de quimioterapia o terapia sistémica de este periodo de reporte";
                    Sheet.Cells["AS1"].Value = "Código de la IPS1 que suministra el primer o único esquema de quimioterapia o terapia sistémica de este periodo de reporte";
                    Sheet.Cells["AT1"].Value = "Código de la IPS2 que suministra el primer o único esquema de quimioterapia o terapia sistémica de este periodo de reporte";
                    Sheet.Cells["AU1"].Value = "Cuantos medicamentos antineoplásicos o terapia hormonal, el (los) especialista(s) tratante(s) del cáncer propusieron como manejo en el  primer o único esquema de quimioterapia o terapia sitémica de este periodo de reporte";
                    Sheet.Cells["AV1"].Value = "Medicamento antineoplásico administrado al usuario- PRIMER o único esquema del periodo de reporte";
                    Sheet.Cells["AW1"].Value = "Medicamento antineoplásico administrado al usuario- PRIMER o único esquema del periodo de reporte";
                    Sheet.Cells["AX1"].Value = "Medicamento antineoplásico administrado al usuario- PRIMER o único esquema del periodo de reporte";
                    Sheet.Cells["AY1"].Value = "Medicamento antineoplásico administrado al usuario- PRIMER o único esquema del periodo de reporte";
                    Sheet.Cells["AZ1"].Value = "Medicamento antineoplásico administrado al usuario- PRIMER o único esquema del periodo de reporte";
                    Sheet.Cells["BA1"].Value = "En este primer ciclo el usuario recibió Ciclosporina (puede haber recibido más de un medicamento)";
                    Sheet.Cells["BB1"].Value = "Medicamento antineoplásico administrado al usuario- PRIMER o único esquema del periodo de reporte";
                    Sheet.Cells["BC1"].Value = "Medicamento antineoplásico administrado al usuario- PRIMER o único esquema del periodo de reporte";
                    Sheet.Cells["BD1"].Value = "Medicamento antineoplásico administrado al usuario- PRIMER o único esquema del periodo de reporte";
                    Sheet.Cells["BE1"].Value = "Medicamento Antineoplásico o terapia hormonal para el cáncer, adicional a los reportados en las variables 53.1 a 53.9 - 1 administrado al usuario- primer o único esquema del periodo de reporte";
                    Sheet.Cells["BF1"].Value = "Medicamento Antineoplásico o terapia hormonal para el cáncer, adicional a los reportados en las variables 53.1 a 53.9 - 1 administrado al usuario- primer o único esquema del periodo de reporte";
                    Sheet.Cells["BG1"].Value = "Medicamento Antineoplásico o terapia hormonal para el cáncer, adicional a los reportados en las variables 53.1 a 53.9 - 3 administrado al usuario- primer o único esquema del periodo de reporte";
                    Sheet.Cells["BH1"].Value = "¿Recibió quimioterapia intratecal en el primer o único esquema de este periodo de reporte?";
                    Sheet.Cells["BI1"].Value = "Fecha de finalización del primer o único esquema de este periodo de reporte. Si es hormonoterapia terminada o esquema terminado en este periodo reporte la fecha de finalización del tratamiento actual";
                    Sheet.Cells["BJ1"].Value = "Características actuales del primer o único esquema  de este periodo de reporte";
                    Sheet.Cells["BK1"].Value = "Motivo de la finalización (prematura) de este primer o único esquema (Aplica si registró la opción 1 de la variable anterior). Seleccione un sólo número (lo que primero ocurrió)";
                    Sheet.Cells["BL1"].Value = "Ubicación temporal del ÚLTIMO esquema de quimioterapia o terapia sistémica de este periodo de reporte en relación al manejo oncológico";
                    Sheet.Cells["BM1"].Value = "Fecha de inicio del último esquema de quimioterapia o terapia sistémica de este periodo de reporte. Si es hormonoterapia reporte la fecha de inicio del tratamiento actual, así haya sido iniciada previo al reporte actual";
                    Sheet.Cells["BN1"].Value = "Número de IPS que suministran el último esquema de este periodo de reporte";
                    Sheet.Cells["BO1"].Value = "Codigo de IPS1 que suministra el último esquema en este  periodo de reporte ";
                    Sheet.Cells["BP1"].Value = "Codigo de IPS1 que suministran el último esquema en este  periodo de reporte ";
                    Sheet.Cells["BQ1"].Value = "Cuantos medicamentos antineoplásicos o terapia hormonal, el (los) especialista(s) tratante(s) del cáncer propusieron como manejo en  último esquema de quimioterapia o terapia sistémica de este periodo de reporte ";
                    Sheet.Cells["BR1"].Value = "Medicamento antineoplásico administrado al usuario - ULTIMO esquema del periodo de reporte";
                    Sheet.Cells["BS1"].Value = "Medicamento antineoplásico administrado al usuario - ULTIMO esquema del periodo de reporte";
                    Sheet.Cells["BT1"].Value = "Medicamento antineoplásico administrado al usuario - ULTIMO esquema del periodo de reporte";
                    Sheet.Cells["BU1"].Value = "Medicamento antineoplásico administrado al usuario - ULTIMO esquema del periodo de reporte";
                    Sheet.Cells["BV1"].Value = "Medicamento antineoplásico administrado al usuario - ULTIMO esquema del periodo de reporte";
                    Sheet.Cells["BW1"].Value = "Medicamento antineoplásico administrado al usuario - ULTIMO esquema del periodo de reporte";
                    Sheet.Cells["BX1"].Value = "Medicamento antineoplásico administrado al usuario - ULTIMO esquema del periodo de reporte";
                    Sheet.Cells["BY1"].Value = "Medicamento antineoplásico administrado al usuario - ULTIMO esquema del periodo de reporte";
                    Sheet.Cells["BZ1"].Value = "Medicamento antineoplásico administrado al usuario - ULTIMO esquema del periodo de reporte";
                    Sheet.Cells["CA1"].Value = "Medicamento Antineoplásico o terapia hormonal para cáncer, adicional a los reportados en variables 66.1 a 66.9 -1 administrado al usuario- último esquema";
                    Sheet.Cells["CB1"].Value = "Medicamento Antineoplásico o terapia hormonal para cáncer, adicional a los reportados en variables 66.1 a 66.9 -1 administrado al usuario- último esquema";
                    Sheet.Cells["CC1"].Value = "Medicamento Antineoplásico o terapia hormonal para cáncer, adicional a los reportados en variables 66.3 a 66.9 -1 administrado al usuario- último esquema";
                    Sheet.Cells["CD1"].Value = "¿Recibió quimioterapia intratecal en el último esquema de este periodo de reporte?";
                    Sheet.Cells["CE1"].Value = "Fecha de finalización del último esquema de quimioterapia o terapia sistemica  este periodo de reporte. Si es hormonoterapia terminada o esquema terminado en este periodo,  reporte la fecha de finalización del tratamiento actual";
                    Sheet.Cells["CF1"].Value = "Características actuales del último  esquema  de este periodo ";
                    Sheet.Cells["CG1"].Value = "Motivo de la finalización (prematura) de este último esquema  (Aplica si registró la opción 1 de la pregunta anterior) Selecciona un sólo número (lo que primero ocurrió)";
                    Sheet.Cells["CH1"].Value = "¿Fue sometido el usuario a una o más cirugías curativas o paliativas como parte del manejo del cáncer durante este periodo de reporte?";
                    Sheet.Cells["CI1"].Value = "Número de cirugías a las que fue sometido el usuario durante el periodo de reporte actual";
                    Sheet.Cells["CJ1"].Value = "Fecha de realización de la primera cirugía en este periodo de  reporte";
                    Sheet.Cells["CK1"].Value = "Código de la IPS  que realizó la primera cirugía de este periodo de reporte ";
                    Sheet.Cells["CL1"].Value = "Código de primera cirugía en este periodo de reporte ";
                    Sheet.Cells["CM1"].Value = "Ubicación temporal de esta primera cirugía en relación al manejo oncológico";
                    Sheet.Cells["CN1"].Value = "Fecha de realización de la última cirugía o última cirugía de reintervención de este periodo de reporte";
                    Sheet.Cells["CO1"].Value = "Motivo de haber realizado la última cirugía de este periodo de reporte ";
                    Sheet.Cells["CP1"].Value = "Código de la IPS  que realiza la última cirugía del periodo de reporte ";
                    Sheet.Cells["CQ1"].Value = "Código de última cirugía de este periodo de reporte ";
                    Sheet.Cells["CR1"].Value = "Ubicación temporal de esta última cirugía en relación al manejo oncológico, en este periodo de reporte";
                    Sheet.Cells["CS1"].Value = "Estado vital al finalizar la única o última cirugía de este periodo de reporte";
                    Sheet.Cells["CT1"].Value = "¿Recibió el usuario algún tipo de radioterapia en el periodo de reporte actual?";
                    Sheet.Cells["CU1"].Value = "Número de sesiones de radioterapia recibidas  en el periodo";
                    Sheet.Cells["CV1"].Value = "Fecha de inicio de primer o único esquema de cualquier tipo de radioterapia suministrado en el periodo de reporte actual. Reporte la fecha de inicio del tratamiento actual, así haya sido iniciada previo al reporte actual";
                    Sheet.Cells["CW1"].Value = "Ubicación temporal del primer o único esquema de cualquier tipo de radioterapia en el periodo de reporte en relación al tratamiento oncologico";
                    Sheet.Cells["CX1"].Value = "Tipo de radioterapia aplicada en este primer o único esquema";
                    Sheet.Cells["CY1"].Value = "Número de IPS que suministran este primer o único esquema de radioterapia";
                    Sheet.Cells["CZ1"].Value = "Código de la IPS1 que suministra la radioterapia de este primer o único esquema";
                    Sheet.Cells["DA1"].Value = "Código de la IPS1 que suministra la radioterapia de este primer o único esquema";
                    Sheet.Cells["DB1"].Value = "Fecha de finalización de primer o único esquema de radioterapia";
                    Sheet.Cells["DC1"].Value = "Características actuales de este primer o único esquema de radioterapia";
                    Sheet.Cells["DD1"].Value = "Motivo de la finalización de este primer o único esquema de radioterapia (Aplica si registró la opción 1 de la pregunta anterior) Selecciona un sólo número (lo que primero ocurrió).";
                    Sheet.Cells["DE1"].Value = "Fecha de inicio del último esquema de cualquier tipo de radioterapia suministrado en el periodo de reporte actual";
                    Sheet.Cells["DF1"].Value = "Ubicación temporal/intención del ÚLTIMO esquema de cualquier tipo de radioterapia suministrado en el periodo actual en relación al tratamiento oncológico";
                    Sheet.Cells["DG1"].Value = "Tipo de radioterapia aplicada en el ÚLTIMO esquema de cualquier tipo de radioterapia suministrado en el periodo de reporte actual";
                    Sheet.Cells["DH1"].Value = "Número de IPS que suministran este último esquema de cualquier tipo de radioterapia en el periodo de reporte actua";
                    Sheet.Cells["DI1"].Value = "Código de la IPS1 que suministra último esquema de cualquier tipo de radioterapia en el periodo de reporte actual";
                    Sheet.Cells["DJ1"].Value = "Código de la IPS1 que suministra último esquema de cualquier tipo de radioterapia en el periodo de reporte actual";
                    Sheet.Cells["DK1"].Value = "Fecha de finalización del último esquema de cualquier tipo de radioterapia suministrado en el periodo de reporte actual";
                    Sheet.Cells["DL1"].Value = "Características actuales de este último esquema de cualquier tipo de radioterapia suministrado en el periodo de reporte actual";
                    Sheet.Cells["DM1"].Value = "Motivo de la finalización de este último esquema de cualquier tipo de radioterapia suministrado en el periodo de reporte actual  (Aplica si registró la opción 1 de la pregunta anterior) Selecciona un sólo número (lo que primero ocurrió)";
                    Sheet.Cells["DN1"].Value = "¿Recibió el usuario trasplante de células progenitoras hematopoyética dentro del periodo de reporte actual?";
                    Sheet.Cells["DO1"].Value = "Tipo de trasplante recibido";
                    Sheet.Cells["DP1"].Value = "Ubicación temporal de este trasplante en relación al manejo oncológico";
                    Sheet.Cells["DQ1"].Value = "Fecha de trasplante";
                    Sheet.Cells["DR1"].Value = "Código de la IPS que realizó este trasplante";
                    Sheet.Cells["DS1"].Value = "El usuario, ¿recibió cirugía reconstructiva en el periodo de reporte actual?";
                    Sheet.Cells["DT1"].Value = "Fecha de la cirugía reconstructiva";
                    Sheet.Cells["DU1"].Value = "Código de la IPS que realizó cirugía reconstructiva";
                    Sheet.Cells["DV1"].Value = "¿El usuario fue valorado en consulta o procedimiento de cuidado paliativo en el periodo de reporte actual? (pueden haber sido múltiples)";
                    Sheet.Cells["DW1"].Value = "El usuario recibió consulta o procedimiento de cuidado paliativo en el periodo de reporte actual , por médico especialista en cuidado paliativo";
                    Sheet.Cells["DX1"].Value = "El usuario recibió consulta o procedimiento de cuidado paliativo en el periodo de reporte actual , por profesional de la salud (no médico, incluye psicólogo) especialista en cuidado paliativo";
                    Sheet.Cells["DY1"].Value = "El usuario recibió consulta o procedimiento de cuidado paliativo en el periodo de reporte actual , por médico especialista, otra especialidad";
                    Sheet.Cells["DZ1"].Value = "El usuario recibió consulta o procedimiento de cuidado paliativo en el periodo de reporte actual , por médico general";
                    Sheet.Cells["EA1"].Value = "El usuario recibió consulta o procedimiento de cuidado paliativo en el periodo de reporte actual, por trabajo social";
                    Sheet.Cells["EB1"].Value = "El usuario recibió consulta o procedimiento de cuidado paliativo en el periodo de reporte actual, por otro profesional de salud (no médico, incluye psicólogo) no especializado";
                    Sheet.Cells["EC1"].Value = "Fecha de primera consulta o procedimiento de cuidado paliativo en el periodo de reporte actual ";
                    Sheet.Cells["ED1"].Value = "Código de la IPS donde recibe la atención  de cuidado paliativo en el periodo de reporte actual ";
                    Sheet.Cells["EE1"].Value = "¿Ha sido valorado el usuario por el servicio de psiquiatría en el periodo de reporte actual?";
                    Sheet.Cells["EF1"].Value = "Fecha de primera consulta con el servicio de psiquiatría (para todos los usuarios) en este periodo de reporte actual ";
                    Sheet.Cells["EG1"].Value = "Código de la IPS  donde recibió la primera valoración de psiquiatría en el periodo de reporte actual ";
                    Sheet.Cells["EH1"].Value = "¿Fue valorado el usuario por profesional en 120, nutrición en el periodo de reporte actual?";
                    Sheet.Cells["EI1"].Value = "Fecha de consulta inicial con nutrición en el periodo de reporte actual ";
                    Sheet.Cells["EJ1"].Value = "Código de la IPS donde recibió la valoración por nutrición, en el periodo de reporte actual";
                    Sheet.Cells["EK1"].Value = "¿El usuario recibió soporte nutricional?";
                    Sheet.Cells["EL1"].Value = "¿El usuario ha recibido terapias complementarias para su rehabilitación?";
                    Sheet.Cells["EM1"].Value = "Tipo de tratamiento que está recibiendo el usuario a la fecha de corte (el día 01/01/1011)";
                    Sheet.Cells["EN1"].Value = "Resultado final del manejo oncológico en este periodo de reporte, luego de ser tratado en este periodo el usuario esta:";
                    Sheet.Cells["EO1"].Value = "Estado vital al finalizar este periodo de reporte";
                    Sheet.Cells["EP1"].Value = "Novedad ADMINISTRATIVA del usuario respecto al reporte anterior";
                    Sheet.Cells["EQ1"].Value = "Novedad clínica  del usuario a la fecha de corte";
                    Sheet.Cells["ER1"].Value = "Fecha de desafiliación de la EAPB";
                    Sheet.Cells["ES1"].Value = "Fecha de muerte";
                    Sheet.Cells["ET1"].Value = "Causa de muerte";
                    Sheet.Cells["EU1"].Value = "Código único de identificación (BDUA-BDEX-PVS)";
                    Sheet.Cells["EV1"].Value = "Fecha de Corte";
                    Sheet.Cells["EW1"].Value = "Observaciones";
                    Sheet.Cells["EX1"].Value = "Cerrado";


                    int row = 2;

                    foreach (management_inventario_altoCosto_tableroGestionesResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":EX" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_gestion;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.id_inventario;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.id_detalle;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_diagnostico;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_remision;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_ingreso;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.tipo_estudio;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.motivo_no_diagnostico_usuario;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_recoleccion_muestra;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_primer_informeHispatologico;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.codigo_habillitacion_ips;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.fecha_consulta_medicoTratante;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.histologia_tumor;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.grado_diferenciacion_tumor;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.estadificacion_tnm_figo;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.fecha_estadificacion;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.se_realiza_her1;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.fecha_realizacion_her1;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.resultado_prueba_her1;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.estadificacion_dukes;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.fecha_estadificacion_dukes;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.estadificacion_hodkin;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.clasificacion_escala_gleason;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.clasificacion_riesgo_leucemialinfoma;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.fecha_clasificacion_riesgo;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.objetivo_tratamiento_inicial;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.objetivo_intervencion_periodo_prueba;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.antecedentes_otro_cancer_primario;
                        Sheet.Cells[string.Format("AC{0}", row)].Value = item.fecha_otro_cancer_primario;
                        Sheet.Cells[string.Format("AD{0}", row)].Value = item.tipo_cie10_otro_cancer;
                        Sheet.Cells[string.Format("AE{0}", row)].Value = item.usuario_recibio_quimioterapia;
                        Sheet.Cells[string.Format("AF{0}", row)].Value = item.cuantas_quimioterapias_recibioUsuario;
                        Sheet.Cells[string.Format("AG{0}", row)].Value = item.usuario_recibio_citorreduccion;
                        Sheet.Cells[string.Format("AH{0}", row)].Value = item.usuario_recibio_induccion;
                        Sheet.Cells[string.Format("AI{0}", row)].Value = item.usuario_recibio_intensificacion;
                        Sheet.Cells[string.Format("AJ{0}", row)].Value = item.usuario_recibio_consolidacion;
                        Sheet.Cells[string.Format("AK{0}", row)].Value = item.usuario_recibio_reinduccion;
                        Sheet.Cells[string.Format("AL{0}", row)].Value = item.usuario_recibio_mantenimiento;
                        Sheet.Cells[string.Format("AM{0}", row)].Value = item.usuario_recibio_mantenimiento_largo;
                        Sheet.Cells[string.Format("AN{0}", row)].Value = item.usuario_recibio_otra_fase;
                        Sheet.Cells[string.Format("AO{0}", row)].Value = item.numero_ciclos_iniciados;
                        Sheet.Cells[string.Format("AP{0}", row)].Value = item.ubicacion_temporal_esquema_quimioterapia;
                        Sheet.Cells[string.Format("AQ{0}", row)].Value = item.fechaInicio_temporal_esquema_quimioterapia1;
                        Sheet.Cells[string.Format("AR{0}", row)].Value = item.numero_ips;
                        Sheet.Cells[string.Format("AS{0}", row)].Value = item.codigo_ips1;
                        Sheet.Cells[string.Format("AT{0}", row)].Value = item.codigo_ips2;
                        Sheet.Cells[string.Format("AU{0}", row)].Value = item.cuantos_medicamentos_neoplasicos;
                        Sheet.Cells[string.Format("AV{0}", row)].Value = item.medicamento_antineoplasico_suministrado_1;
                        Sheet.Cells[string.Format("AW{0}", row)].Value = item.medicamento_antineoplasico_suministrado_2;
                        Sheet.Cells[string.Format("AX{0}", row)].Value = item.medicamento_antineoplasico_suministrado_3;
                        Sheet.Cells[string.Format("AY{0}", row)].Value = item.medicamento_antineoplasico_suministrado_4;
                        Sheet.Cells[string.Format("AZ{0}", row)].Value = item.medicamento_antineoplasico_suministrado_5;
                        Sheet.Cells[string.Format("BA{0}", row)].Value = item.recibio_ciclosporina;
                        Sheet.Cells[string.Format("BB{0}", row)].Value = item.medicamento_antineoplasico_suministrado_7;
                        Sheet.Cells[string.Format("BC{0}", row)].Value = item.medicamento_antineoplasico_suministrado_8;
                        Sheet.Cells[string.Format("BD{0}", row)].Value = item.medicamento_antineoplasico_suministrado_9;
                        Sheet.Cells[string.Format("BE{0}", row)].Value = item.medicamento_antineoplasico_suministrado_adicional_1;
                        Sheet.Cells[string.Format("BF{0}", row)].Value = item.medicamento_antineoplasico_suministrado_adicional_2;
                        Sheet.Cells[string.Format("BG{0}", row)].Value = item.medicamento_antineoplasico_suministrado_adicional_3;
                        Sheet.Cells[string.Format("BH{0}", row)].Value = item.recibio_quimioterapia_intratecal;
                        Sheet.Cells[string.Format("BI{0}", row)].Value = item.fecha_finalizacion_periodo_reporte;
                        Sheet.Cells[string.Format("BJ{0}", row)].Value = item.caracteristicas_actuales_esquema_periodo;
                        Sheet.Cells[string.Format("BK{0}", row)].Value = item.motivo_finalizacion_prematura;
                        Sheet.Cells[string.Format("BL{0}", row)].Value = item.ubicacion_temporal_ultimoesquema;
                        Sheet.Cells[string.Format("BM{0}", row)].Value = item.fecha_inicio_ultimo_esquema;
                        Sheet.Cells[string.Format("BN{0}", row)].Value = item.numero_ips_suministra_ultimoesquema;
                        Sheet.Cells[string.Format("BO{0}", row)].Value = item.codigo_ips_suministra_ultimoesquema_1;
                        Sheet.Cells[string.Format("BP{0}", row)].Value = item.codigo_ips_suministra_ultimoesquema_2;
                        Sheet.Cells[string.Format("BQ{0}", row)].Value = item.cuantos_tratamientos_antineoplasicos_propusieron;
                        Sheet.Cells[string.Format("BR{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_1;
                        Sheet.Cells[string.Format("BS{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_2;
                        Sheet.Cells[string.Format("BT{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_3;
                        Sheet.Cells[string.Format("BU{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_4;
                        Sheet.Cells[string.Format("BV{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_5;
                        Sheet.Cells[string.Format("BW{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_6;
                        Sheet.Cells[string.Format("BX{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_7;
                        Sheet.Cells[string.Format("BY{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_8;
                        Sheet.Cells[string.Format("BZ{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_9;
                        Sheet.Cells[string.Format("CA{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_adicional_1;
                        Sheet.Cells[string.Format("CB{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_adicional_2;
                        Sheet.Cells[string.Format("CC{0}", row)].Value = item.antineoplasico_administradoUsuario_ultimoesquema_adicional_3;
                        Sheet.Cells[string.Format("CD{0}", row)].Value = item.recibio_terapiaIntratecal_ultimoperiodo;
                        Sheet.Cells[string.Format("CE{0}", row)].Value = item.fecha_finalizacionUltimoEsquema_quimioterapia_terapiasistemica;
                        Sheet.Cells[string.Format("CF{0}", row)].Value = item.caracteristicas_actuales_ultimoPeriodo;
                        Sheet.Cells[string.Format("CG{0}", row)].Value = item.motivo_fializacionPrematura_ultimoEsquema;
                        Sheet.Cells[string.Format("CH{0}", row)].Value = item.usuario_sometidoa_cirugias_paliativasCurativas;
                        Sheet.Cells[string.Format("CI{0}", row)].Value = item.numeroCirugias_sometidoUsuario_ultimo;
                        Sheet.Cells[string.Format("CJ{0}", row)].Value = item.fecha_realizacionprimeracirugia_periodoreporte;
                        Sheet.Cells[string.Format("CK{0}", row)].Value = item.codigo_ips_realizo_primercirugia;
                        Sheet.Cells[string.Format("CL{0}", row)].Value = item.codigo_primeracirugia_periodoactual;
                        Sheet.Cells[string.Format("CM{0}", row)].Value = item.ubicaciontemporal_manejo_oncologico_primeraCirugia;
                        Sheet.Cells[string.Format("CN{0}", row)].Value = item.fecharealizacion_ultimacirugia_reintervencion;
                        Sheet.Cells[string.Format("CO{0}", row)].Value = item.motivo_ultimacirugia;
                        Sheet.Cells[string.Format("CP{0}", row)].Value = item.codigo_ips_realizo_ultimacirugia;
                        Sheet.Cells[string.Format("CQ{0}", row)].Value = item.codigo_ultimacirugia_periodoactual;
                        Sheet.Cells[string.Format("CR{0}", row)].Value = item.ubicaciontemporal_manejooncologico_ultimacirugia;
                        Sheet.Cells[string.Format("CS{0}", row)].Value = item.estadovital_finalizarultimacirugia;
                        Sheet.Cells[string.Format("CT{0}", row)].Value = item.usuariorecibio_radioterapia_periodoreporte_actual;
                        Sheet.Cells[string.Format("CU{0}", row)].Value = item.numero_sesionesradioterapia_recibidas_periodo;
                        Sheet.Cells[string.Format("CV{0}", row)].Value = item.fechaInicio_primerounicoesquema_tiporadioterapia;
                        Sheet.Cells[string.Format("CW{0}", row)].Value = item.ubicaciontemporal_primerunico_esquemaradioterapia_tratamientoOncologico;
                        Sheet.Cells[string.Format("CX{0}", row)].Value = item.tipo_radioterapia_aplicada_primeresquema;
                        Sheet.Cells[string.Format("CY{0}", row)].Value = item.numeroips_suministranprimeresquema_radioterapia;
                        Sheet.Cells[string.Format("CZ{0}", row)].Value = item.codigo_ips1_suministraprimeresquema_radioterapia_1;
                        Sheet.Cells[string.Format("DA{0}", row)].Value = item.codigo_ips1_suministraprimeresquema_radioterapia_2;
                        Sheet.Cells[string.Format("DB{0}", row)].Value = item.fecha_finalizacion_primeresquema_radioterapia;
                        Sheet.Cells[string.Format("DC{0}", row)].Value = item.caracteristicas_actuales_primeresquema_radioterapia;
                        Sheet.Cells[string.Format("DD{0}", row)].Value = item.motivo_fianlizacionprimeresquema_radioterapia;
                        Sheet.Cells[string.Format("DE{0}", row)].Value = item.fechaInicio_ultimounicoesquema_tiporadioterapia;
                        Sheet.Cells[string.Format("DF{0}", row)].Value = item.ubicaciontemporal_ultimounico_esquemaradioterapia_tratamientoOncologico;
                        Sheet.Cells[string.Format("DG{0}", row)].Value = item.tipo_radioterapia_aplicada_ultimoesquema;
                        Sheet.Cells[string.Format("DH{0}", row)].Value = item.numeroips_suministranultimoesquema_radioterapia;
                        Sheet.Cells[string.Format("DI{0}", row)].Value = item.codigo_ips1_suministraultimoesquema_radioterapia_1;
                        Sheet.Cells[string.Format("DJ{0}", row)].Value = item.codigo_ips1_suministraultimoesquema_radioterapia_2;
                        Sheet.Cells[string.Format("DK{0}", row)].Value = item.fecha_finalizacion_ultimoesquema_radioterapia;
                        Sheet.Cells[string.Format("DL{0}", row)].Value = item.caracteristicasactuales_ultimoesquema_radioterapia;
                        Sheet.Cells[string.Format("DM{0}", row)].Value = item.motivo_fianlizacionultimoesquema_radioterapia;
                        Sheet.Cells[string.Format("DN{0}", row)].Value = item.recibiousuario_transplantecelulas_progenitorashematopoyetica_periodoactual;
                        Sheet.Cells[string.Format("DO{0}", row)].Value = item.tipo_transplante_recibido;
                        Sheet.Cells[string.Format("DP{0}", row)].Value = item.ubicacion_temporal_transplante_relaciononcologico;
                        Sheet.Cells[string.Format("DQ{0}", row)].Value = item.fecha_transplante;
                        Sheet.Cells[string.Format("DR{0}", row)].Value = item.codigo_ips_realizo_transplante;
                        Sheet.Cells[string.Format("DS{0}", row)].Value = item.usuario_recibiocirugia_reconstructiva_periodoactual;
                        Sheet.Cells[string.Format("DT{0}", row)].Value = item.fecha_cirugia_reconstructiva;
                        Sheet.Cells[string.Format("DU{0}", row)].Value = item.codigo_ips_realizocirugia_reconstructiva;
                        Sheet.Cells[string.Format("DV{0}", row)].Value = item.usuario_valoradoconsulta_procedimientocuidado_paliativo;
                        Sheet.Cells[string.Format("DW{0}", row)].Value = item.usuario_valoradoconsulta_procedimientocuidado_paliativo_medicoespecialista;
                        Sheet.Cells[string.Format("DX{0}", row)].Value = item.usuario_valoradoconsulta_procedimientocuidado_paliativo_nomedico;
                        Sheet.Cells[string.Format("DY{0}", row)].Value = item.usuario_valoradoconsulta_procedimientocuidado_paliativo_otraespecialidad;
                        Sheet.Cells[string.Format("DZ{0}", row)].Value = item.usuario_recibioconsulta_procedimiento_cuidadopaliativo_medicogeneral;
                        Sheet.Cells[string.Format("EA{0}", row)].Value = item.usuario_recibioconsulta_procedimiento_cuidadopaliativo_trabajosocial;
                        Sheet.Cells[string.Format("EB{0}", row)].Value = item.usuario_recibioconsulta_procedimiento_cuidadopaliativo_otroprofesional;
                        Sheet.Cells[string.Format("EC{0}", row)].Value = item.fecha_primeraconsulta_procedimientocuidado_paliativo;
                        Sheet.Cells[string.Format("ED{0}", row)].Value = item.codigo_ips__recibeatencion_cuidadopaliativo;
                        Sheet.Cells[string.Format("EE{0}", row)].Value = item.usuario_valoradoservicio_psiquiatria;
                        Sheet.Cells[string.Format("EF{0}", row)].Value = item.fecha_primeraconsulta_serviciopsiquiatria;
                        Sheet.Cells[string.Format("EG{0}", row)].Value = item.codigo_ipsdonde_recibeprimeravaloracion_psiquiatria;
                        Sheet.Cells[string.Format("EH{0}", row)].Value = item.usuario_valoradoporprofesional_120_nutricion;
                        Sheet.Cells[string.Format("EI{0}", row)].Value = item.fecha_consultaInicial_nutricion;
                        Sheet.Cells[string.Format("EJ{0}", row)].Value = item.codigo_ips_recibevaloracion_nutricion;
                        Sheet.Cells[string.Format("EK{0}", row)].Value = item.usuario_recibiosoporte_nutricional;
                        Sheet.Cells[string.Format("EL{0}", row)].Value = item.usuario_recibeterapias_complementarias_rehabilitacion;
                        Sheet.Cells[string.Format("EM{0}", row)].Value = item.tipo_tratamiento_estarecibiendo_usuario;
                        Sheet.Cells[string.Format("EN{0}", row)].Value = item.resultadofinal_resultadooncologico_usuarioesta;
                        Sheet.Cells[string.Format("EO{0}", row)].Value = item.estadovital_periodoreporte;
                        Sheet.Cells[string.Format("EP{0}", row)].Value = item.novedadadministrativa_usuariorespectoreporteanterior;
                        Sheet.Cells[string.Format("EQ{0}", row)].Value = item.novedadclinica_usuario_afechacorte;
                        Sheet.Cells[string.Format("ER{0}", row)].Value = item.fecha_desafiliacion_EAPB;
                        Sheet.Cells[string.Format("ES{0}", row)].Value = item.fecha_muerte;
                        Sheet.Cells[string.Format("ET{0}", row)].Value = item.causa_muerte;
                        Sheet.Cells[string.Format("EU{0}", row)].Value = item.codigo_unico_identificacion_BDUA_BDEX_PVS;
                        Sheet.Cells[string.Format("EV{0}", row)].Value = item.fecha_corte;
                        Sheet.Cells[string.Format("EW{0}", row)].Value = item.observaciones;
                        Sheet.Cells[string.Format("EX{0}", row)].Value = item.cerrado;

                        Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("L{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("P{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("U{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("Y{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("AC{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("AQ{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("BI{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("BM{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("CH{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("CJ{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("CN{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("DB{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("DE{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("DK{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("DQ{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("DT{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("EC{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("EF{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("EI{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("ER{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("ES{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("ET{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("EV{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile;
                    namefile = "DetalleGestiones_" + idDetalle + "_" + DateTime.Now;

                    Sheet.Cells["A:EX"].AutoFitColumns();
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
                   "window.alert('ERROR EN LA DESCARGA');" +
                   "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public ActionResult CargueCuentasPoblacionConfirmada()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CargueCuentasPoblacionConfirmada(HttpPostedFileBase file)
        {
            var mensaje = "";
            Models.InventarioAltoCosto.inventarioAltoCosto Model = new Models.InventarioAltoCosto.inventarioAltoCosto();
            var nombreArchivo = "";
            var idCargue = 0;
            var ruta = "";
            try
            {
                if (file != null)
                {
                    nombreArchivo = file.FileName;

                    if (!nombreArchivo.Contains("confirmados"))
                    {
                        throw new Exception("Asegurese de que el archivo es el correcto. Este archivo no tiene en su nombre 'confirmados'");
                    }

                    ruta = DevolverRutaArchivo(file);
                    //return Json(new { success = true, message = ruta, idMed = 1 }, JsonRequestBehavior.AllowGet);

                    string dirpath = Path.Combine(ruta);
                    WebClient User = new WebClient();
                    ruta = dirpath;
                    string filename = ruta;

                    var tipoArchivo = Path.GetExtension(file.FileName);

                    Byte[] FileBuffer = User.DownloadData(filename);

                    byte[] array = new byte[0];
                    array = FileBuffer;
                    array = array.ToArray();

                    HttpPostedFileBase sigFile = (HttpPostedFileBase)new HttpPostedFileBaseCustom(array, file.ContentType, file.FileName + "." + tipoArchivo);

                    CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                    var asposeOptions = new Aspose.Cells.LoadOptions
                    {
                        MemorySetting = MemorySetting.MemoryPreference
                    };

                    Workbook wb = new Workbook(sigFile.InputStream, asposeOptions);
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

                    idCargue = Model.CargueMasivoPoblacionComprobada(5, dataTable, file.FileName, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["MensajeRta"] = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE - CARGUE #" + idCargue + " </div>";
                    }
                    else
                    {
                        mensaje = MsgRes.DescriptionResponse;
                        ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                    }
                }
                else
                {
                    mensaje = "DEBE SELECCIONAR UN ARCHIVO PARA SUBIR DATOS";
                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                }
            }
            catch (Exception ex)
            {
                var error = "";
                if (ex.Message.Contains("Row number or column number cannot be zero"))
                {
                    error = "El archivo no puede cargarse vacío";
                }
                else
                {
                    error = ex.Message;
                }

                mensaje = "ERROR EN EL CARGUE MASIVO DE ALTO COSTO: " + error;
                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
            }

            FileInfo fileDelete = new FileInfo(ruta);
            if (fileDelete != null)
            {
                fileDelete.Delete();
            }

            return View();
        }


        public ActionResult CargueMasivoGSDRastreos()
        {
            ViewBag.tipo = BusClass.listadoCargueGsdRastreo();
            return View();
        }

        [HttpPost]
        public ActionResult CargueMasivoGSDRastreos(HttpPostedFileBase file, int? tipo)
        {
            var mensaje = "";
            Models.InventarioAltoCosto.inventarioAltoCosto Model = new Models.InventarioAltoCosto.inventarioAltoCosto();
            var nombreArchivo = "";
            var idCargue = 0;
            ViewBag.tipo = BusClass.listadoCargueGsdRastreo();

            var tipoCargado = "";
            var ruta = "";

            try
            {
                if (file != null)
                {
                    tipoCargado = BusClass.listadoCargueGsdRastreo().Where(x => x.id_tipo == tipo).Select(x => x.descripcion).FirstOrDefault();

                    nombreArchivo = file.FileName.ToUpper();

                    if (tipo == 1)
                    {
                        if (!nombreArchivo.Contains("CANCER"))
                        {
                            throw new Exception("Asegurese de que el archivo es el correcto. Este archivo no tiene en su nombre 'Cancer'");
                        }
                    }
                    else if (tipo == 2)
                    {
                        if (!nombreArchivo.Contains("HEMOFILIA"))
                        {
                            throw new Exception("Asegurese de que el archivo es el correcto. Este archivo no tiene en su nombre 'Hemofilia'");
                        }
                    }
                    else if (tipo == 3)
                    {
                        if (!nombreArchivo.Contains("ARTRITIS"))
                        {
                            throw new Exception("Asegurese de que el archivo es el correcto. Este archivo no tiene en su nombre 'Artritis'");
                        }
                    }
                    else if (tipo == 4)
                    {
                        if (!nombreArchivo.Contains("VIH"))
                        {
                            throw new Exception("Asegurese de que el archivo es el correcto. Este archivo no tiene en su nombre 'Vih'");
                        }
                    }

                    ruta = DevolverRutaArchivo(file);
                    //return Json(new { success = true, message = ruta, idMed = 1 }, JsonRequestBehavior.AllowGet);

                    string dirpath = Path.Combine(ruta);
                    WebClient User = new WebClient();
                    ruta = dirpath;
                    string filename = ruta;

                    var tipoArchivo = Path.GetExtension(file.FileName);

                    Byte[] FileBuffer = User.DownloadData(filename);

                    byte[] array = new byte[0];
                    array = FileBuffer;
                    array = array.ToArray();

                    HttpPostedFileBase sigFile = (HttpPostedFileBase)new HttpPostedFileBaseCustom(array, file.ContentType, file.FileName + "." + tipoArchivo);

                    CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                    var asposeOptions = new Aspose.Cells.LoadOptions
                    {
                        MemorySetting = MemorySetting.MemoryPreference
                    };

                    Workbook wb = new Workbook(sigFile.InputStream, asposeOptions);
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

                    if (tipo == 1)
                    {
                        idCargue = Model.CargueMasivoCancer(tipo, dataTable, file.FileName, ref MsgRes);
                    }
                    else if (tipo == 2)
                    {
                        idCargue = Model.CargueMasivoHemofilia(tipo, dataTable, file.FileName, ref MsgRes);
                    }
                    else if (tipo == 3)
                    {
                        idCargue = Model.CargueMasivoArtritis(tipo, dataTable, file.FileName, ref MsgRes);
                    }
                    else if (tipo == 4)
                    {
                        idCargue = Model.CargueMasivoVih(tipo, dataTable, file.FileName, ref MsgRes);
                    }

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["MensajeRta"] = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE - CARGUE #" + idCargue + " - Tipo: " + tipoCargado + " </div>";
                    }
                    else
                    {
                        mensaje = MsgRes.DescriptionResponse;
                        ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                    }
                }
                else
                {

                    mensaje = "DEBE SELECCIONAR UN ARCHIVO PARA SUBIR DATOS";
                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                }
            }
            catch (Exception ex)
            {
                var error = "";
                if (ex.Message.Contains("Row number or column number cannot be zero"))
                {
                    error = "El archivo no puede cargarse vacío";
                }
                else
                {
                    error = ex.Message;
                }

                mensaje = "ERROR EN EL CARGUE MASIVO DE ALTO COSTO: " + error;
                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
            }

            FileInfo fileDelete = new FileInfo(ruta);
            if (fileDelete != null)
            {
                fileDelete.Delete();
            }

            return View();
        }

        public string DevolverRutaArchivo(HttpPostedFileBase file)
        {

            string archivo = "";
            string rutaFinal = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<Models.CuentasMedicas.SoportesClinicos> listasoportes = new List<Models.CuentasMedicas.SoportesClinicos>();

            ViewBag.af = false;
            try
            {
                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;
                strRutaDefinitiva = ConfigurationManager.AppSettings["RutaTemporalArchivos"];
                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                //string nombreSintilde = Regex.Replace(file.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                var extension = Path.GetExtension(file.FileName);
                string nombreSintilde = file.FileName.Replace(extension, "");

                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + nombreSintilde + extension);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                string strError = string.Empty;

                DateTime fecha = DateTime.Now;
                archivo = string.Empty;
                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "CuentasAltoCosto";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "CuentasAltoCostoPruebas";
                }
                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta);

                Models.CuentasMedicas.SoportesClinicos objGD = new Models.CuentasMedicas.SoportesClinicos();

                var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                byte[] ExcelData = null;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    ExcelData = binaryReader.ReadBytes(file.ContentLength);
                }

                rutaFinal = archivo;
                return rutaFinal;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return rutaFinal;
            }
        }

        [ValidateInput(false)]
        public ActionResult TableroListadoRastreo(int? tipo, int? rta)
        {
            List<management_cuentasAltoCosto_rastreosResult> listado = new List<management_cuentasAltoCosto_rastreosResult>();
            List<management_cuentasAltoCosto_rastreosResult> listadoSin = new List<management_cuentasAltoCosto_rastreosResult>();
            List<management_cuentasAltoCosto_rastreosResult> listadoCon = new List<management_cuentasAltoCosto_rastreosResult>();

            var conteo = 0;
            ViewBag.tipo = BusClass.listadoCargueGsdRastreo();
            ViewBag.estado = BusClass.listadoEstadosCuentaAltoCosto();

            var mensaje = "";
            if (rta != null)
            {
                if (rta == 1)
                {
                    mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE. </div>";
                }
                else
                {
                    mensaje = "<div class='alert alert-danger' role='alert'>ERROR EN LA GESTIÓN</div>";
                }
            }

            try
            {
                listado = BusClass.ListadoDatosRastreoTotal(tipo);

                listadoSin = listado.Where(x => x.estado == null).ToList();
                listadoCon = listado.Where(x => x.estado != null).ToList();

                conteo = listado.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["TableroRastreo"] = listado;

            ViewBag.lista = listado;
            ViewBag.listadoSin = listadoSin;
            ViewBag.listadoCon = listadoCon;

            ViewBag.conteo = listado.Count();
            ViewBag.conteoSin = listadoSin.Count();
            ViewBag.conteoCon = listadoCon.Count();

            ViewBag.msg = mensaje;
            ViewBag.tipoFiltrado = tipo;

            return View();
        }

        public PartialViewResult _GestionRastreos(int? idRegistro, int? tipo)
        {
            ViewBag.id = idRegistro;
            ViewBag.tipo = tipo;
            ViewBag.estado = BusClass.listadoEstadosCuentaAltoCosto();
            List<management_cuentasAltoCosto_gestionesResult> listaGestiones = new List<management_cuentasAltoCosto_gestionesResult>();
            var conteo = 0;
            try
            {
                listaGestiones = BusClass.listadoGestionesAltoCosto(idRegistro, tipo);
                conteo = listaGestiones.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.gestiones = listaGestiones;
            ViewBag.conteoGestiones = conteo;

            return PartialView();
        }

        public ActionResult GuardarGestionRastreo(int? id_registro, int? confirmacionDiagnostico, int? tipo, int? estado_caso, string observaciones, string estadio, DateTime? fecha_diagnostico)
        {
            var mensaje = "";
            var rta = 0;
            cargue_cuentas_altoCosto_gestiones obj = new cargue_cuentas_altoCosto_gestiones();
            try
            {
                obj.id_registro = id_registro;
                if (tipo == 1)
                {
                    obj.estado_caso = confirmacionDiagnostico;
                }
                else
                {
                    obj.estado_caso = estado_caso;
                }

                obj.observaciones = observaciones;
                obj.tipo = tipo;
                obj.estadio = estadio;
                obj.fecha_diagnostico = fecha_diagnostico;
                if (fecha_diagnostico != null)
                {
                    obj.año_diagnostico = fecha_diagnostico.Value.Year;
                }
                obj.fecha_gestion = DateTime.Now;
                obj.usuario_gestion = SesionVar.UserName;

                var insercion = BusClass.GuardarGestionCuentasAltoCosto(obj, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    rta = 1;
                    mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE. </div>";
                }
                else
                {
                    mensaje = "<div class='alert alert-danger' role='alert'>" + MsgRes.DescriptionResponse + "</div>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "<div class='alert alert-danger' role='alert'>" + error + "</div>";
            }

            return RedirectToAction("TableroListadoRastreo", "InventarioAltoCosto", new { rta = rta });
        }

        public void DescargarReporteRastreo()
        {
            List<management_cuentasAltoCosto_rastreosResult> lista = (List<management_cuentasAltoCosto_rastreosResult>)Session["TableroRastreo"];

            try
            {
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosRastreo");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:Y1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:Y1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:Y1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:Y1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:Y1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:Y1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:Y1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id cargue";
                    Sheet.Cells["B1"].Value = "Rastreo mes";
                    Sheet.Cells["C1"].Value = "Rastreo año";
                    Sheet.Cells["D1"].Value = "Regional";
                    Sheet.Cells["E1"].Value = "Unis";
                    Sheet.Cells["F1"].Value = "Tipo documento";
                    Sheet.Cells["G1"].Value = "Documento paciente";
                    Sheet.Cells["H1"].Value = "Nombre paciente";
                    Sheet.Cells["I1"].Value = "Fecha nacimiento";
                    Sheet.Cells["J1"].Value = "Edad";
                    Sheet.Cells["K1"].Value = "Sexo";
                    Sheet.Cells["L1"].Value = "Diagnóstico CIE10";
                    Sheet.Cells["M1"].Value = "Reportado CAC";
                    Sheet.Cells["N1"].Value = "En Medicarte";
                    Sheet.Cells["O1"].Value = "Estado gestión";
                    Sheet.Cells["P1"].Value = "Observación";
                    Sheet.Cells["Q1"].Value = "Estadio";
                    Sheet.Cells["R1"].Value = "Fecha diagnóstico";
                    Sheet.Cells["S1"].Value = "Fecha gestion";
                    Sheet.Cells["T1"].Value = "Agrupador";
                    Sheet.Cells["U1"].Value = "Auditor";
                    Sheet.Cells["V1"].Value = "Mes auditoría";
                    Sheet.Cells["W1"].Value = "Año auditoría";
                    Sheet.Cells["X1"].Value = "Mes corte";
                    Sheet.Cells["Y1"].Value = "Año corte";

                    int row = 2;

                    foreach (management_cuentasAltoCosto_rastreosResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":Y" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_cargue;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.rastreo_mes;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.año_rastreo;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.regional;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.unis;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.tipo_documento;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.documento;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nombreCompleto;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_nacimiento;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.edad;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.sexo;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.CIE10detallado;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.reportado_cac;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.en_medicarte;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.descripcionEstadoGestion;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.observacionesGestion;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.estadioGestion;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.fecha_diagnosticoGestion;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.fecha_gestion;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.agrupador;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.auditor;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.mes_auditoria;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.año_auditoria;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.mes_corte;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.año_corte;


                        Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("S{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "TableroRastreo";
                    Sheet.Cells["A:Y"].AutoFitColumns();
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

        public ActionResult TableroDatosConfirmados(int? tipo)
        {
            List<management_cuentasAltoCosto_rastreosConfirmadosResult> lista = new List<management_cuentasAltoCosto_rastreosConfirmadosResult>();
            //List<management_cuentasAltoCosto_rastreosConfirmadosResult> listaPendiente = new List<management_cuentasAltoCosto_rastreosConfirmadosResult>();
            List<management_cuentasAltoCosto_rastreosConfirmadosResult> listaConfirmado = new List<management_cuentasAltoCosto_rastreosConfirmadosResult>();
            List<management_cuentasAltoCosto_rastreosConfirmadosResult> listaDescartado = new List<management_cuentasAltoCosto_rastreosConfirmadosResult>();
            List<management_cuentasAltoCosto_rastreosConfirmadosResult> listaTramite = new List<management_cuentasAltoCosto_rastreosConfirmadosResult>();

            var conteo = 0;
            ViewBag.tipo = BusClass.listadoCargueGsdRastreo();

            try
            {
                if (tipo != null)
                {
                    lista = BusClass.ListadoDatosCuentasAltoCostoConfirmados(tipo);
                }

                //listaPendiente = lista.Where(x => x.estado == null).ToList();
                listaConfirmado = lista.Where(x => x.estado == 1).ToList();
                listaDescartado = lista.Where(x => x.estado == 2).ToList();
                listaTramite = lista.Where(x => x.estado == 3).ToList();


                conteo = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["ListadoDatosGestionados"] = lista;

            //ViewBag.conteoPendientes = listaPendiente.Count();
            ViewBag.conteoConfirmado = listaConfirmado.Count();
            ViewBag.conteoDescartado = listaDescartado.Count();
            ViewBag.conteoTramite = listaTramite.Count();
            ViewBag.conteo = lista.Count();

            //ViewBag.listaPendiente = listaPendiente;
            ViewBag.listaConfirmado = listaConfirmado;
            ViewBag.listaDescartado = listaDescartado;
            ViewBag.listaTramite = listaTramite;
            ViewBag.tipoFiltrado = tipo;
            return View();
        }

        public void DescargarReporteConfirmados()
        {
            List<management_cuentasAltoCosto_rastreosConfirmadosResult> Lista = new List<management_cuentasAltoCosto_rastreosConfirmadosResult>();

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                Lista = (List<management_cuentasAltoCosto_rastreosConfirmadosResult>)Session["ListadoDatosGestionados"];

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
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosGestionados");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:AF1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:AF1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:AF1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:AF1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:AF1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:AF1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:AF1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id población";
                    Sheet.Cells["B1"].Value = "Tipo";
                    Sheet.Cells["C1"].Value = "Tipo dato";
                    Sheet.Cells["D1"].Value = "Tipo de documento usuario";
                    Sheet.Cells["E1"].Value = "Identidad usuario";
                    Sheet.Cells["F1"].Value = "Primer apellido";
                    Sheet.Cells["G1"].Value = "Segundo apellido";
                    Sheet.Cells["H1"].Value = "Primer nombre";
                    Sheet.Cells["I1"].Value = "Segundo nombre";
                    Sheet.Cells["J1"].Value = "Fecha nacimiento CAC";
                    Sheet.Cells["K1"].Value = "Género CAC";
                    Sheet.Cells["L1"].Value = "Estado población";
                    Sheet.Cells["M1"].Value = "Regional";
                    Sheet.Cells["N1"].Value = "Unis";
                    Sheet.Cells["O1"].Value = "Tipo paciente homologado";
                    Sheet.Cells["P1"].Value = "Grupo de edad";
                    Sheet.Cells["Q1"].Value = "Id grupo edad";
                    Sheet.Cells["R1"].Value = "Población CAC";
                    Sheet.Cells["S1"].Value = "Diagnóstico CIE10";
                    Sheet.Cells["T1"].Value = "Descripción Dx";
                    Sheet.Cells["U1"].Value = "Estado";

                    Sheet.Cells["V1"].Value = "Mes rastreo";
                    Sheet.Cells["W1"].Value = "Año rastreo";
                    Sheet.Cells["X1"].Value = "Auditor";
                    Sheet.Cells["Y1"].Value = "Mes auditoría";
                    Sheet.Cells["Z1"].Value = "Año auditoría";
                    Sheet.Cells["AA1"].Value = "Mes de corte";
                    Sheet.Cells["AB1"].Value = "Año de corte";
                    Sheet.Cells["AC1"].Value = "fecha diagnóstico";
                    Sheet.Cells["AD1"].Value = "Estadio";
                    Sheet.Cells["AE1"].Value = "Agrupador";
                    Sheet.Cells["AF1"].Value = "Observaciones auditor";

                    int row = 2;

                    foreach (management_cuentasAltoCosto_rastreosConfirmadosResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":AF" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_poblacion;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.descripcionTipo;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.tipoDato;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.tipo_documento_usuario;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.documento_usuario;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.primer_apellido;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.segundo_apellido;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.primer_nombre;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.segundo_nombre;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_nacimiento;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.genero_poblacion;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.estado_poblacion;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.regional_poblacion;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.unis_poblacion;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.tipo_paciente_homologado_poblacion;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.grupo_edad_poblacion;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.idGrupoEdadPoblacionCAC;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.poblacion_cac;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.codigo_cie10;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.descripcion_codigo_cie10;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.descripcionEstado;

                        Sheet.Cells[string.Format("V{0}", row)].Value = item.mes_rastreo;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.año_rastreo;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.auditor;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.mes_auditoria;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.año_auditoria;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.mes_corte;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.año_corte;
                        Sheet.Cells[string.Format("AC{0}", row)].Value = item.fecha_diagnostico;
                        Sheet.Cells[string.Format("AD{0}", row)].Value = item.estadio;
                        Sheet.Cells[string.Format("AE{0}", row)].Value = item.agrupador;
                        Sheet.Cells[string.Format("AF{0}", row)].Value = item.observaciones;

                        Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("AC{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile;
                    namefile = "DatosGestionados_" + DateTime.Now;

                    Sheet.Cells["A:AF"].AutoFitColumns();
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
                   "window.alert('ERROR EN LA DESCARGA');" +
                   "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public PartialViewResult RepositorioInicialDatosConfirmados(int? idRegistro, int? tipoCargue, string fechaCargue, int? id)
        {
            List<management_cuentasAltoCosto_repositorioResult> listaArchivos = new List<management_cuentasAltoCosto_repositorioResult>();
            var conteoArchivos = 0;

            DateTime fecha_cargue = (!string.IsNullOrEmpty(fechaCargue) && fechaCargue != "undefined") ? Convert.ToDateTime(fechaCargue) : DateTime.Now;

            try
            {
                listaArchivos = BusClass.CuentasAltoCostoRepositorio(idRegistro, tipoCargue);
                conteoArchivos = listaArchivos.Count();
            }

            catch (Exception ex)
            {
                var erorr = ex.Message;
            }
            ViewBag.tipoSoporte = BusClass.tipoSoporteCAC();

            ViewBag.listaArchivos = listaArchivos;
            ViewBag.conteoArchivos = conteoArchivos;
            ViewBag.rol = SesionVar.ROL;
            ViewBag.idRegistro = idRegistro;
            ViewBag.tipoCargue = tipoCargue;
            ViewBag.fechaCargue = fecha_cargue.ToString("MM/dd/yyyy");
            ViewBag.id = id;

            return PartialView();

        }

        public PartialViewResult RepositorioInicialDatosConfirmadosGestionados(int? idRegistro, int? tipo)
        {
            List<management_cuentasAltoCosto_repositorioResult> listaArchivos = new List<management_cuentasAltoCosto_repositorioResult>();
            var conteoArchivos = 0;
            try
            {
                listaArchivos = BusClass.CuentasAltoCostoRepositorio(idRegistro, tipo);
                conteoArchivos = listaArchivos.Count();

            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }

            ViewBag.listaArchivos = listaArchivos;
            ViewBag.conteoArchivos = conteoArchivos;
            ViewBag.rol = SesionVar.ROL;
            ViewBag.idRegistroG = idRegistro;
            ViewBag.tipoG = tipo;

            return PartialView();

        }

        public ActionResult VerArchivoRepositorio(Int32 idArchivo)
        {
            try
            {
                cargue_cuentas_altoCosto_archivos dato = new cargue_cuentas_altoCosto_archivos();
                dato = BusClass.TraerArchivoRepositorio(idArchivo);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";
                    string extensionTipo = obj.extension;
                    var nombreFinal = obj.nombre_archivo;

                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    if (!string.IsNullOrEmpty(extensionTipo))
                    {
                        return File(dirpath, extensionTipo, nombreFinal);
                    }
                    else
                    {
                        return File(dirpath, extension, nombreFinal);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public JsonResult InsertarArchivosRepositorio(int? idRegistro, List<HttpPostedFileBase> archivos, int? tipoCargue, int? tipoSoporte, DateTime? fecha_prestacion_soporte)
        {
            var mensaje = "";
            var rta = 0;
            var resultado = 0;
            var lineaDañada = 0;
            var dañado = 0;
            var nombreArchivo = "";

            try
            {
                if (archivos != null)
                {
                    if (archivos.Count() > 0)
                    {
                        for (var i = 0; i < archivos.Count(); i++)
                        {
                            nombreArchivo = archivos[i].FileName;
                            resultado = guardarArchivosRepositorio(archivos[i], (int)idRegistro, (int)tipoCargue, (int)tipoSoporte, fecha_prestacion_soporte);
                            lineaDañada = i + 1;
                            if (resultado == 0)
                            {
                                i = archivos.Count();
                                dañado = 1;
                            }
                        }

                        if (dañado == 0)
                        {
                            mensaje = "ARCHIVO(S) INSERTADO(S) CORRECTAMENTE.";
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ERROR EN LA INSERCIÓN DEL ARCHIVOS #" + lineaDañada + " - " + nombreArchivo;
                            rta = 0;
                        }
                    }
                    else
                    {
                        mensaje = "SELECCIONE ARCHIVO A INSERTAR.";
                        rta = 0;
                    }
                }
                else
                {
                    mensaje = "SELECCIONE ARCHIVO A INSERTAR.";
                    rta = 0;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA INSERCIÓN DE ARCHIVOS :" + error;
                rta = 0;
            }

            return Json(new { rta = rta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertarArchivosRepositorioGestionado(int? idRegistroG, List<HttpPostedFileBase> files, int? tipoG)
        {
            var mensaje = "";
            var rta = 0;
            var resultado = 0;
            var lineaDañada = 0;
            var dañado = 0;
            var nombreArchivo = "";

            try
            {
                if (files != null)
                {
                    if (files.Count() > 0)
                    {
                        for (var i = 0; i < files.Count(); i++)
                        {
                            nombreArchivo = files[i].FileName;
                            resultado = guardarArchivosRepositorio(files[i], (int)idRegistroG, (int)tipoG, 0, DateTime.Now);
                            lineaDañada = i + 1;
                            if (resultado == 0)
                            {
                                i = files.Count();
                                dañado = 1;
                            }
                        }

                        if (dañado == 0)
                        {
                            mensaje = "ARCHIVO(S) INSERTADO(S) CORRECTAMENTE.";
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ERROR EN LA INSERCIÓN DEL ARCHIVOS #" + lineaDañada + " - " + nombreArchivo;
                            rta = 0;
                        }
                    }
                    else
                    {
                        mensaje = "SELECCIONE ARCHIVO A INSERTAR.";
                        rta = 0;
                    }
                }
                else
                {
                    mensaje = "SELECCIONE ARCHIVO A INSERTAR.";
                    rta = 0;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA INSERCIÓN DE ARCHIVOS :" + error;
                rta = 0;
            }

            return Json(new { rta = rta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        private int guardarArchivosRepositorio(HttpPostedFileBase file, int idRegistro, int tipoCargue, int tipoSoporte, DateTime? fecha_prestacion_soporte)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            var respuesta = 0;

            try
            {
                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaRepositorioCuentasAltoCosto"];
                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;
                String carpeta = "";

                carpeta = "Repositorio_" + idRegistro + "_" + tipoCargue;

                ruta = Path.Combine(Request.PhysicalApplicationPath,
                                    strRutaDefinitiva + "\\" + carpeta);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                cargue_cuentas_altoCosto_archivos OBJ = new cargue_cuentas_altoCosto_archivos();
                OBJ.id_registro = idRegistro;
                OBJ.id_tipo = tipoCargue;
                OBJ.tipo_soporte = tipoSoporte;
                OBJ.nombre_archivo = file.FileName;
                OBJ.extension = file.ContentType;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.fecha_prestacion_soporte = fecha_prestacion_soporte;

                OBJ.fecha_digita = DateTime.Now;
                OBJ.usuario_digita = SesionVar.UserName;

                respuesta = BusClass.InsertarArchivoReposAltoCosto(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }

        public JsonResult EliminarArchivoRepositorio(int? idArchivo, int? tipo, int? idRegistro)
        {
            String mensaje = "";
            try
            {
                var resultado = BusClass.eliminarArchivoRepositorioAltoCosto((int)idArchivo);

                if (resultado != 0)
                {
                    log_cargue_cuentas_altoCosto_archivos obj = new log_cargue_cuentas_altoCosto_archivos();
                    obj.id_archivo = idArchivo;
                    obj.id_registro = idRegistro;
                    obj.id_tipo = tipo;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    var eliminacion = BusClass.LogArchivoReposAltoCosto(obj, ref MsgRes);

                    mensaje = "SE ELIMINÓ CORRECTAMENTE.";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "ERROR AL ELIMINAR :" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EliminarArchivoRepositorioGestionada(int? idArchivo, int? tipo, int? idRegistro)
        {
            String mensaje = "";
            try
            {
                var resultado = BusClass.eliminarArchivoRepositorioAltoCosto((int)idArchivo);

                if (resultado != 0)
                {
                    log_cargue_cuentas_altoCosto_archivos obj = new log_cargue_cuentas_altoCosto_archivos();
                    obj.id_archivo = idArchivo;
                    obj.id_registro = idRegistro;
                    obj.id_tipo = tipo;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    var eliminacion = BusClass.LogArchivoReposAltoCosto(obj, ref MsgRes);

                    mensaje = "SE ELIMINÓ CORRECTAMENTE.";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "ERROR AL ELIMINAR :" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TableroDatosConfirmadosGestionadas(int? tipo)
        {
            List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoResult> lista = new List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoResult>();
            var conteo = 0;
            ViewBag.tipo = BusClass.listadoCargueGsdRastreo();

            try
            {
                lista = BusClass.ListadoDatosCuentasAltoCostoConfirmadosConArchivos();

                if (tipo != null)
                {
                    lista = lista.Where(x => x.tipo == tipo).ToList();
                }

                //listaTramite = lista.Where(x=>x.tipoDato)

                conteo = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var tipoFinal = 0;

            if (tipo != null && tipo != 0)
            {
                tipoFinal = (int)tipo;
            }

            Session["ListadoDatosConfirmadosGestionados"] = tipoFinal;
            Session["TipoDatoConfirmadoGestionado"] = tipo;

            ViewBag.lista = lista;
            ViewBag.conteo = conteo;

            return View();
        }

        public void DescargarReporteConfirmadosGestionados()
        {
            int tipo = (int)Session["ListadoDatosConfirmadosGestionados"];

            List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoCompletaResult> Lista = new List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoCompletaResult>();

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                Lista = BusClass.ListadoDatosCuentasAltoCostoConfirmadosConArchivosDetallada();

                if (tipo != 0)
                {
                    Lista = Lista.Where(x => x.tipo == tipo).ToList();
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
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosConfirmados");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:AG1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:AG1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:AG1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:AG1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:AG1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:AG1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:AG1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id población";
                    Sheet.Cells["B1"].Value = "Tipo";
                    Sheet.Cells["C1"].Value = "Tipo de documento usuario";
                    Sheet.Cells["D1"].Value = "Identidad usuario";
                    Sheet.Cells["E1"].Value = "Primer apellido";
                    Sheet.Cells["F1"].Value = "Segundo apellido";
                    Sheet.Cells["G1"].Value = "Primer nombre";
                    Sheet.Cells["H1"].Value = "Segundo nombre";
                    Sheet.Cells["I1"].Value = "Fecha nacimiento CAC";
                    Sheet.Cells["J1"].Value = "Género CAC";
                    Sheet.Cells["K1"].Value = "Estado";
                    Sheet.Cells["L1"].Value = "Regional";
                    Sheet.Cells["M1"].Value = "Unis";
                    Sheet.Cells["N1"].Value = "Tipo paciente homologado";
                    Sheet.Cells["O1"].Value = "Grupo de edad";
                    Sheet.Cells["P1"].Value = "Id grupo edad";
                    Sheet.Cells["Q1"].Value = "Población CAC";
                    Sheet.Cells["R1"].Value = "Diagnóstico CIE10";
                    Sheet.Cells["S1"].Value = "Descripción Dx";
                    Sheet.Cells["T1"].Value = "Observación coordinador";
                    Sheet.Cells["U1"].Value = "Fecha digita";
                    Sheet.Cells["V1"].Value = "Usuario digita";

                    Sheet.Cells["W1"].Value = "Mes rastreo";
                    Sheet.Cells["X1"].Value = "Año rastreo";
                    Sheet.Cells["Y1"].Value = "Auditor";
                    Sheet.Cells["Z1"].Value = "Mes auditoría";
                    Sheet.Cells["AA1"].Value = "Año auditoría";
                    Sheet.Cells["AB1"].Value = "Mes de corte";
                    Sheet.Cells["AC1"].Value = "Año de corte";
                    Sheet.Cells["AD1"].Value = "Fecha diagnóstico";
                    Sheet.Cells["AE1"].Value = "Estadio";
                    Sheet.Cells["AF1"].Value = "Agrupador";
                    Sheet.Cells["AG1"].Value = "Observaciones auditor";
                    int row = 2;

                    foreach (management_cuentasAltoCosto_rastreosConfirmados_conArchivoCompletaResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":AG" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_poblacion;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.descripcionTipo;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.tipo_documento_usuario;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.documento_usuario;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.primer_apellido;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.segundo_apellido;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.primer_nombre;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.segundo_nombre;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_nacimiento;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.genero_poblacion;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.estado_poblacion;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.regional_poblacion;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.unis_poblacion;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.tipo_paciente_homologado_poblacion;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.grupo_edad_poblacion;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.idGrupoEdadPoblacionCAC;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.poblacion_cac;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.codigo_cie10;

                        Sheet.Cells[string.Format("S{0}", row)].Value = item.descripcion_codigo_cie10;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.observacion;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.fecha_digita;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.nombreGestiona;

                        Sheet.Cells[string.Format("W{0}", row)].Value = item.mes_rastreo;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.año_rastreo;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.auditor;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.mes_auditoria;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.año_auditoria;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.mes_corte;
                        Sheet.Cells[string.Format("AC{0}", row)].Value = item.año_corte;
                        Sheet.Cells[string.Format("AD{0}", row)].Value = item.fecha_diagnostico;
                        Sheet.Cells[string.Format("AE{0}", row)].Value = item.estadio;
                        Sheet.Cells[string.Format("AF{0}", row)].Value = item.agrupador;
                        Sheet.Cells[string.Format("AG{0}", row)].Value = item.observaciones;

                        Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("U{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("AD{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile;
                    namefile = "DatosConfirmados_" + DateTime.Now;

                    Sheet.Cells["A:AG"].AutoFitColumns();
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
                   "window.alert('ERROR EN LA DESCARGA');" +
                   "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public PartialViewResult GestionObservacionConfirmadas(int? idRegistro, int? tipo)
        {
            List<management_cuentasAltoCosto_rastreosConfirmados_observacionesResult> listado = new List<management_cuentasAltoCosto_rastreosConfirmados_observacionesResult>();
            var conteoObservaciones = 0;
            try
            {
                listado = BusClass.ListadoObservacionesCuentasAltoCostoGestionadas(idRegistro, tipo);
                conteoObservaciones = listado.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.id = idRegistro;
            ViewBag.tipoDato = tipo;

            ViewBag.listaObservaciones = listado;
            ViewBag.conteoObservaciones = conteoObservaciones;

            ViewBag.rol = SesionVar.ROL;
            return PartialView();
        }

        public JsonResult GuardarObservacionesGestionadas(int? idRegistro, int? tipoObs, string observaciones)
        {
            var mensaje = "";
            try
            {
                if (idRegistro != null && tipoObs != null)
                {
                    cargue_cuentas_altoCosto_observaciones obj = new cargue_cuentas_altoCosto_observaciones();
                    obj.id_registro = idRegistro;
                    obj.id_tipo = tipoObs;
                    obj.observacion = observaciones;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    var ingreso = BusClass.GuardarObservacionesCuentasAltoCosto(obj, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "OBSERVACIÓN INGRESADA CORRECTAMENTE";
                    }
                    else
                    {
                        mensaje = "ERROR EN EL INGRESO: " + MsgRes.DescriptionResponse;
                    }
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

            return Json(new { mensaje = mensaje });

        }

        public JsonResult EliminarObservacionGestionada(int? idObservacion, int? tipoRef, int? idRegistro)
        {
            String mensaje = "";
            try
            {
                var resultado = BusClass.eliminarObservacionAltoCosto((int)idObservacion);

                if (resultado != 0)
                {
                    //log_cargue_cuentas_altoCosto_archivos obj = new log_cargue_cuentas_altoCosto_archivos();
                    //obj.id_archivo = idArchivo;
                    //obj.id_registro = idRegistro;
                    //obj.id_tipo = tipo;
                    //obj.fecha_digita = DateTime.Now;
                    //obj.usuario_digita = SesionVar.UserName;

                    //var eliminacion = BusClass.LogArchivoReposAltoCosto(obj, ref MsgRes);

                    mensaje = "SE ELIMINÓ CORRECTAMENTE.";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "ERROR AL ELIMINAR :" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TableroDescargueArchivos()
        {

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 4);
            años.Add(DateTime.Now.Year - 3);
            años.Add(DateTime.Now.Year - 2);
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);
            ViewBag.años = años;
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.tipo = BusClass.listadoCargueGsdRastreo();

            return View();
        }

        //public ActionResult DescargarArchivosConsolidado(int? tipo)
        //{
        //    List<management_cuentasAltoCosto_documentosArchivosResult> lista = new List<management_cuentasAltoCosto_documentosArchivosResult>();
        //    List<ref_cargue_cuentas_altoCosto> listadoTipos = new List<ref_cargue_cuentas_altoCosto>();
        //    listadoTipos = BusClass.listadoCargueGsdRastreo();
        //    var tipoFinal = listadoTipos.Where(x => x.id_tipo == tipo).Select(x => x.descripcion).FirstOrDefault();

        //    try
        //    {
        //        lista = BusClass.DocumentosConArchivos(tipo);
        //        lista = lista.Where(x => x.tipo == tipo).ToList();

        //        if (lista.Count == 0)
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "El tipo no contiene documentos." });
        //        }

        //        using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
        //        {
        //            int count = 0;
        //            foreach (var item in lista)
        //            {
        //                List<management_cuentasAltoCosto_consolidadoArchivosResult> obj = new List<management_cuentasAltoCosto_consolidadoArchivosResult>();
        //                obj = BusClass.ListaArchivosPorDocumentoCAC(item.documento, tipo);
        //                if (obj.Count() > 0)
        //                {
        //                    var ruta = item.tipo_documento + "_" + item.documento;
        //                    foreach (var item2 in obj)
        //                    {
        //                        count++;
        //                        string dirpath = item2.ruta;
        //                        var existencia = Directory.Exists(dirpath);

        //                        try
        //                        {
        //                            byte[] bytes = System.IO.File.ReadAllBytes(dirpath);
        //                            var direccionCompleta = ruta + "\\ " + item2.tipo_documento_usuario + item2.documento_usuario + "_" + item2.descripcionTipoSoporte + "_" + item2.fecha_prestacion_soporte.Value.ToString("yyyyMMdd") + "_" + item2.id_archivo + ".pdf";
        //                            zip.AddEntry(direccionCompleta, bytes);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            var error = ex.Message;
        //                        }
        //                    }
        //                }
        //            }

        //            using (MemoryStream salida = new MemoryStream())
        //            {
        //                zip.Save(salida);
        //                return File(salida.ToArray(), "application/zip", "ConsolidadoArchivos_tipo_" + tipoFinal + ".zip");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("ControlErrores", "Usuario", new
        //        {
        //            Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message
        //        });
        //    }
        //}

        //public ActionResult DescargarArchivosConsolidado(int? tipo)
        //{
        //    try
        //    {
        //        // Obtener el listado de tipos
        //        var listadoTipos = BusClass.listadoCargueGsdRastreo();
        //        var tipoFinal = listadoTipos.FirstOrDefault(x => x.id_tipo == tipo)?.descripcion;

        //        if (tipoFinal == null)
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "El tipo no existe." });
        //        }

        //        // Obtener la lista de documentos del tipo solicitado
        //        var lista = BusClass.DocumentosConArchivos(tipo).Where(x => x.tipo == tipo).ToList();

        //        if (lista.Count == 0)
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "El tipo no contiene documentos." });
        //        }

        //        // Crear el archivo ZIP
        //        using (var zip = new Ionic.Zip.ZipFile())
        //        {
        //            foreach (var item in lista)
        //            {
        //                // Obtener archivos asociados al documento
        //                var archivos = BusClass.ListaArchivosPorDocumentoCAC(item.documento, tipo);

        //                if (archivos.Count > 0)
        //                {
        //                    var rutaCarpeta = item.tipo_documento + "_" + item.documento;

        //                    foreach (var archivo in archivos)
        //                    {
        //                        string dirPath = archivo.ruta;

        //                        // Verificar si el archivo existe antes de agregarlo
        //                        if (System.IO.File.Exists(dirPath))
        //                        {
        //                            try
        //                            {
        //                                // Crear un stream para agregar al ZIP
        //                                var direccionCompleta = $"{rutaCarpeta}/{archivo.tipo_documento_usuario}{archivo.documento_usuario}_{archivo.descripcionTipoSoporte}_{archivo.fecha_prestacion_soporte:yyyyMMdd}_{archivo.id_archivo}.pdf";
        //                                zip.AddFile(dirPath).FileName = direccionCompleta;
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                // Registrar errores específicos de un archivo
        //                                // Podrías loguear el error aquí si es necesario
        //                                Console.WriteLine($"Error al agregar archivo: {ex.Message}");
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            // Retornar el archivo ZIP al cliente
        //            using (var salida = new MemoryStream())
        //            {
        //                zip.Save(salida);
        //                return File(salida.ToArray(), "application/zip", $"ConsolidadoArchivos_tipo_{tipoFinal}.zip");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("ControlErrores", "Usuario", new
        //        {
        //            mensaje = "Ha ocurrido un error al generar el archivo: " + ex.Message
        //        });
        //    }
        //}

        public ActionResult DescargarArchivosConsolidado(int? año, string mes, string regional, int? tipo, string documento)
        {
            try
            {
                // Obtener el listado de tipos
                var listadoTipos = BusClass.listadoCargueGsdRastreo();
                var tipoFinal = listadoTipos.FirstOrDefault(x => x.id_tipo == tipo)?.descripcion;

                if (tipoFinal == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "El tipo no existe." });
                }

                // Obtener la lista de documentos del tipo solicitado
                var lista = BusClass.DocumentosConArchivos(año, mes, regional, tipo, documento);
                if (lista.Count == 0)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "No se encontraron documentos." });
                }

                // Generar el archivo ZIP en streaming
                Response.Clear();
                Response.BufferOutput = false; // Desactiva el buffer para transmitir en tiempo real
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", $"attachment; filename=ConsolidadoArchivos_tipo_{tipoFinal}.zip");

                using (var zip = new Ionic.Zip.ZipFile())
                {
                    foreach (var item in lista)
                    {
                        // Obtener archivos asociados al documento
                        var archivos = BusClass.ListaArchivosPorDocumentoCAC(item.documento, tipo);

                        if (archivos.Count > 0)
                        {
                            var rutaCarpeta = item.tipo_documento + " " + item.documento;

                            foreach (var archivo in archivos)
                            {
                                string dirPath = archivo.ruta;

                                // Verificar si el archivo existe antes de agregarlo
                                if (System.IO.File.Exists(dirPath))
                                {
                                    try
                                    {
                                        var direccionCompleta = $"{rutaCarpeta}/{archivo.tipo_documento_usuario}{archivo.documento_usuario}_{archivo.descripcionTipoSoporte}_{archivo.fecha_prestacion_soporte:yyyyMMdd}_{archivo.id_archivo}.pdf";
                                        zip.AddFile(dirPath).FileName = direccionCompleta;
                                    }
                                    catch (Exception ex)
                                    {
                                        // Loguear el error del archivo
                                        Console.WriteLine($"Error al agregar archivo: {ex.Message}");
                                    }
                                }
                            }
                        }
                    }

                    // Guardar el ZIP directamente en la respuesta HTTP
                    zip.Save(Response.OutputStream);
                }

                Response.End();
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new
                {
                    mensaje = "Ha ocurrido un error al generar el archivo: " + ex.Message
                });
            }
        }


    }
}


