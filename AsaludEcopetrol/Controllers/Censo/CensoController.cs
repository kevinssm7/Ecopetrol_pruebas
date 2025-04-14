using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using Aspose.Cells;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.Reporting.WebForms;

using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Censo
{
    [SessionExpireFilter]
    public class CensoController : Controller
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

        public ActionResult BuscarCenso()
        {
            Models.Censo.censo Model = new Models.Censo.censo();
            return View(Model);
        }

        [HttpGet]
        public ActionResult BuscarCensoD(Int64? ID, String ID2, int pagina = 1)
        {
            //ID : numero identificación
            //ID2: tipo identificacion

            Models.Censo.censo Model = new Models.Censo.censo();
            List<base_beneficiarios> bb = new List<base_beneficiarios>();
            var periodoBB = new DateTime();
            var periodoBBFinal = "";
            var existeBB = 0;

            try
            {
                Model.tipo_identifi_afiliado = Convert.ToString(ID2);
                Model.num_identifi_afil = Convert.ToString(ID);

                var cantidadRegistrosPorPagina = 5;
                using (var db = new ECOPETROL_DataContexDataContext())
                {
                    //Func<vw_datos_censo, bool> predicado = x => !ID.HasValue || ID.Value == Convert.ToInt64(x.num_identifi_afil);

                    var personas = db.vw_datos_censo.Where(x => x.num_identifi_afil == Convert.ToString(ID)).OrderBy(x => x.num_identifi_afil).ToList();

                    //.Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    //.Take(cantidadRegistrosPorPagina).ToList();

                    var totalDeRegistros = db.vw_datos_censo.Where(x => x.num_identifi_afil == Convert.ToString(ID)).Count();

                    Model.std = personas;

                    Model.PaginaActual = pagina;
                    Model.TotalDeRegistros = totalDeRegistros;
                    Model.RegistrosPorPagina = cantidadRegistrosPorPagina;
                    Model.ValoresQueryString = new RouteValueDictionary();
                    Model.ValoresQueryString["ID"] = ID;

                    bb = BusClass.GetUltimoBeneficiarios(Convert.ToString(ID), ID2, ref MsgRes);

                    if (bb.Count() > 0)
                    {
                        periodoBB = (DateTime)bb.OrderByDescending(x => x.id_base_beneficiarios).Select(x => x.periodo).FirstOrDefault();

                        if (periodoBB != null)
                        {
                            periodoBBFinal = Convert.ToString(periodoBB.ToString("dd/MM/yyyy"));
                        }

                        existeBB = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }

            ViewBag.existeBB = existeBB;
            ViewBag.periodoBB = periodoBBFinal;

            return View(Model);
        }


        [HttpGet]
        public PartialViewResult BuscarCensoN(String ID)
        {
            Models.Censo.censo Model = new Models.Censo.censo();

            Model.id_censo = Convert.ToInt32(ID);

            return PartialView(Model);

        }

        [HttpGet]
        public ActionResult IngresoCenso(String ID, String ID2, int? BB, string TI)
        {
            Models.Censo.censo Model = new Models.Censo.censo();

            Model.tipo_identifi_afiliado = ID;
            Model.num_identifi_afil = ID2;

            ViewBag.usuario = SesionVar.ROL;

            //existe beneficiario
            //ViewBag.BB = BB;

            //tipo ingreso cuando noe xiste beneficiario

            if (string.IsNullOrEmpty(TI) || TI == "undefined")
            {
                TI = "";
            }

            //ViewBag.TI = TI;

            Model.BB = BB;
            Model.TI = TI;

            return View(Model);
        }

        [HttpGet]
        public ActionResult IngresoCenso2()
        {
            return View();
        }

        public ActionResult IngresoCenso3()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult CensoEgreso(String ID, String ID2)
        {
            Models.Censo.censo Model = new Models.Censo.censo();

            Model.id_censo = Convert.ToInt32(ID);
            Model.ips_primaria2 = Convert.ToString(ID2);

            ViewBag.usuario = SesionVar.ROL;

            return PartialView(Model);
        }

        [HttpGet]
        public PartialViewResult CensoFacturaEgreso(String ID)
        {
            Models.Censo.censo Model = new Models.Censo.censo();

            Model.id_censo = Convert.ToInt32(ID);

            return PartialView(Model);

        }

        [HttpGet]
        public PartialViewResult VerCenso(String ID, String ID2)
        {
            Models.Censo.censo Model = new Models.Censo.censo();

            Model.id_censo = Convert.ToInt32(ID);
            Model.ips_primaria2 = Convert.ToString(ID2);

            Model.ConsultaCenso(Convert.ToInt32(ID));
            ViewBag.usuario = SesionVar.ROL;

            return PartialView(Model);
        }

        //[HttpGet]
        //public ActionResult BuscarControlCenso(String ID, String ID2)
        //{
        //    Models.Censo.censo Model = new Models.Censo.censo();

        //    Model.ips_primaria2 = ID2;

        //    Model.Lista = Model.GetLista();
        //    Model.Lista = Model.Lista.Where(x => x.ips_primaria == Model.ips_primaria2).ToList();
        //    return View(Model);
        //}



        //[HttpPost]
        //public ActionResult BuscarControlCenso(Models.Censo.censo Model)
        //{
        //    if (Model.Items == "2")
        //    {
        //        Model.Lista = Model.GetLista();
        //        return View(Model);
        //    }
        //    else
        //    {
        //        Model.Lista = Model.GetLista();
        //        Model.Lista = Model.Lista.Where(x => x.ips_primaria == Model.ips_primaria).ToList();

        //        return View(Model);
        //    }

        //}

        public ActionResult BuscarControlCenso(String ID, String ID2, Models.Censo.censo Model)
        {
            List<vw_ips_ciudad> listado = new List<vw_ips_ciudad>();

            try
            {
                //datosEgreso = BusClass.datosEgreso(ID);

                listado = BusClass.GetIPS();
                listado = listado.Where(x => x.usuario == Convert.ToString(SesionVar.UserName)).ToList();

                Model.ips_primaria2 = ID2;

                if (string.IsNullOrEmpty(Model.ips_primaria) && string.IsNullOrEmpty(Model.ips_primaria2))
                {
                    Model.Lista = Model.GetLista();
                }
                else
                {
                    Model.Lista = Model.GetLista();
                    Model.Lista = Model.Lista.Where(x => x.ips_primaria == Model.ips_primaria || x.ips_primaria == Model.ips_primaria2).ToList();
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoIps = listado;
            ViewBag.conteoL = Model.Lista.Count();

            return View(Model);
        }

        public JsonResult SacarCensoTablero(int? idCenso)
        {
            var mensaje = "";

            try
            {
                ecop_censo censo = new ecop_censo();
                censo.id_censo = (int)idCenso;
                censo.admitido_auditor = 1;

                var actualiza = BusClass.ActualizarCensoSacarDelTablero(censo);

                if (actualiza != 0)
                {
                    mensaje = "GESTIÓN HECHA CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR EN LA GESTIÓN DE CENSO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA GESTIÓN DE CENSO: " + error;
            }

            return Json(new { mensaje = mensaje });

        }

        public ActionResult BuscarControlCensoFacturacion(string documento, string nombre, DateTime? fechaEgresoConcu, DateTime? fechaEgresoCenso)
        {
            List<management_datos_censoResult> listaCenso = new List<management_datos_censoResult>();
            var conteo = 0;

            try
            {

                if (!string.IsNullOrEmpty(documento) || !string.IsNullOrEmpty(nombre) || fechaEgresoCenso != null || fechaEgresoCenso != null)
                {
                    listaCenso = BusClass.CensoFacturasDetallado(documento, nombre, fechaEgresoConcu, fechaEgresoCenso, ref MsgRes);
                }

                conteo = listaCenso.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoCenso = listaCenso;
            ViewBag.conteoCenso = conteo;

            return View();
        }

        [HttpPost]
        public ActionResult BuscarCenso(Models.Censo.censo Model)
        {
            if (Model.Items == "2")
            {
                ModelState.Remove("numeroIdentificacion");
                ModelState.Remove("IdSeleccionado");
                return View(Model);
            }
            else
            {
                if (Model.id_censo == 0)
                {

                }
                else
                {

                }
                return RedirectToAction("BuscarCensoD", "Censo", new { id = Model.num_identifi_afil, id2 = Model.tipo_identifi_afiliado });
            }




        }

        [HttpPost]
        public ActionResult IngresoCenso(Models.Censo.censo Model)
        {

            if (Model.dx_actual != null)
            {
                if (Model.ips_primaria != null)
                {
                    if (Model.fecha_nacimientook != null)
                    {
                        if (Model.fecha_ingresook != null)
                        {
                            Model.fecha_recepcion_censoOK = Model.fecha_ingresook;

                            if (Model.fecha_recepcion_censoOK != null)
                            {
                                if (Model.fecha_ingresook < DateTime.Now)
                                {
                                    if (Model.fecha_recepcion_censoOK >= Model.fecha_ingresook)
                                    {
                                        Model.OBJCenso.fecha_recepcion_censo = Convert.ToDateTime(Model.fecha_recepcion_censoOK);
                                        Model.OBJCenso.ips_primaria = Model.ips_primaria;
                                        Model.OBJCenso.sucursal = Model.sucursal;
                                        Model.OBJCenso.tipo_identifi_afiliado = Model.tipo_identifi_afiliado;
                                        Model.OBJCenso.num_identifi_afil = Model.num_identifi_afil;

                                        Model.OBJCenso.primer_apellido = Model.primer_apellido;
                                        Model.OBJCenso.tipo_ingreso_noBeneficiario = Model.TI;
                                        Model.OBJCenso.existe_bb = Model.BB;

                                        Model.OBJCenso.linea_pago = Model.linea_pago;
                                        Model.OBJCenso.caso_Especial = Model.caso_Especial;

                                        if (Model.caso_Especial != null && Model.caso_Especial != 0)
                                        {
                                            Model.OBJCenso.caso_Especial_detalle = Model.caso_Especial_detalle;
                                        }

                                        if (Model.segundo_apellido == null)
                                        {
                                            Model.OBJCenso.segundo_apellido = "";
                                        }
                                        else
                                        {
                                            Model.OBJCenso.segundo_apellido = Model.segundo_apellido;
                                        }

                                        Model.OBJCenso.primer_nombre = Model.primer_nombre;

                                        if (Model.segundo_nombre == null)
                                        {
                                            Model.OBJCenso.segundo_nombre = "";
                                        }
                                        else
                                        {
                                            Model.OBJCenso.segundo_nombre = Model.segundo_nombre;
                                        }

                                        Model.OBJCenso.fecha_nacimiento = Convert.ToDateTime(Model.fecha_nacimientook);
                                        Model.OBJCenso.genero = Model.genero;
                                        Model.OBJCenso.edad = Model.edad;
                                        Model.OBJCenso.fecha_ingreso = Model.fecha_ingresook;
                                        Model.OBJCenso.tipo_habitacion = Model.tipo_habitacion;
                                        Model.OBJCenso.dias_estancia = Model.dias_estancia;
                                        Model.OBJCenso.cie10dx_actual = Model.cie10dx_actual;
                                        Model.OBJCenso.tipo_ingreso = Model.tipo_ingreso;
                                        Model.OBJCenso.dx_actual = Model.dx_actual;
                                        Model.OBJCenso.origen_evento = Model.origen_evento;
                                        Model.OBJCenso.id_medico_auditor = Model.id_medico_auditor;
                                        Model.OBJCenso.estado_contact = 1;

                                        Model.OBJCenso.digita_usuario = Convert.ToString(SesionVar.UserName);
                                        Model.OBJCenso.digita_fecha = Convert.ToDateTime(DateTime.Now);
                                        Model.OBJCenso.reingresado = 0;

                                        Model.OBJCenso.id_censo = Model.InsertarCenso(ref MsgRes);


                                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                                        {
                                            return RedirectToAction("BuscarCenso", "Censo", new { id = Model.id_censo });
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "!!...Error...la fecha de recepción del censo no debe ser menor a la fecha de ingreso !!!");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "!!...Error...la fecha de ingreso no debe ser mayor al dia de hoy !!!");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "!!...Error...Debe ingresar fecha recepcion.!!!");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "!!...Error...Debe ingresar fecha Ingreso.!!!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "!!...Error...Debe ingresar fecha nacimiento.!!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error...Debe ingresar la ips primaria.!!!");
                }
            }
            else
            {
                ModelState.AddModelError("", "!!...Error...Debe ingresar el Diagnostico.!!!");
            }
            return View(Model);
        }


        [HttpPost]
        public ActionResult VerCenso(Models.Censo.censo Model)
        {

            if (Model.ips_primaria != null)
            {

                if (Model.fecha_recepcion_censoOK != null)
                {
                    Model.OBJCenso.id_censo = Model.id_censo;
                    Model.OBJCenso.tipo_identifi_afiliado = Model.tipo_identifi_afiliado;
                    Model.OBJCenso.fecha_recepcion_censo = Convert.ToDateTime(Model.fecha_recepcion_censoOK);
                    Model.OBJCenso.tipo_ingreso = Model.tipo_ingreso;
                    Model.OBJCenso.origen_evento = Model.origen_evento;
                    Model.OBJCenso.tipo_habitacion = Model.tipo_habitacion;
                    Model.OBJCenso.dias_estancia = Model.dias_estancia;
                    Model.OBJCenso.id_medico_auditor = Model.id_medico_auditor;
                    Model.OBJCenso.dx_actual = Model.dx_actualok;
                    Model.OBJCenso.ips_primaria = Model.ips_primaria;
                    Model.OBJCenso.caso_Especial = Model.caso_Especial;
                    Model.OBJCenso.caso_Especial_detalle = Model.caso_Especial_detalle;
                    Model.OBJCenso.linea_pago = Model.linea_pago;

                    if (Model.egreso == "SI")
                    {
                        Model.OBJCenso.fecha_egreso = Convert.ToDateTime(Model.fecha_egreso);
                        Model.OBJCenso.condicion_alta = Model.condicion_alta;
                        Model.OBJCenso.dx_egreso = Model.dx_egreso;
                        Model.OBJCenso.incapacidad = Model.incapacidad;
                        Model.OBJCenso.Numero_factura = Model.Numero_factura;
                    }
                    else
                    {
                        Model.OBJCenso.fecha_egreso = null;
                        Model.OBJCenso.condicion_alta = null;
                        Model.OBJCenso.dx_egreso = null;
                        Model.OBJCenso.incapacidad = null;
                        Model.OBJCenso.Numero_factura = null;
                    }

                    Model.ActualizarCenso(ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        log_censo_reingresos reg = new log_censo_reingresos();
                        reg.id_censo = Model.OBJCenso.id_censo;
                        reg.documento = RetornoCedula(Model.id_censo);
                        reg.fecha_reingreso = DateTime.Now;
                        reg.usuario_reingreso = SesionVar.UserName;

                        var insertaLog = BusClass.InsertarLogCensoReingreso(reg, ref MsgRes);

                        if (Model.ips_primaria2 != null)
                        {
                            return RedirectToAction("BuscarControlCenso", "Censo", new { ID = Model.id_censo, ID2 = Model.ips_primaria2 });
                        }
                        else
                        {
                            return RedirectToAction("BuscarCenso", "Censo", new { id = Model.id_censo });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error...Debe ingresar fecha recepcion.!!!");
                }
            }
            else
            {
                ModelState.AddModelError("", "!!...Error...Debe ingresar IPS.!!!");
            }

            return View(Model);
        }


        public string RetornoCedula(int? idCenso)
        {
            var cedula = "";
            ecop_censo cen = new ecop_censo();
            try
            {
                cen = BusClass.ConsultaCensoIdCenso(idCenso);

                if (cen != null)
                {
                    if (cen.num_identifi_afil != null)
                    {
                        cedula = cen.num_identifi_afil;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return cedula;
        }

        [HttpPost]
        public ActionResult CensoEgreso(Models.Censo.censo Model)
        {
            if (Model.fecha_egreso_censook != null)
            {
                Model.OBJCenso.id_censo = Model.id_censo;
                Model.OBJCenso.fecha_egreso_censo = Model.fecha_egreso_censook;
                Model.OBJCenso.condicion_de_alta = Model.condicion_alta;

                Model.CensoEgreso(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("BuscarControlCenso", "Censo", new { id = Model.id_censo, ID2 = Model.ips_primaria2 });
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error...al ingreso" + MsgRes.DescriptionResponse);
                }



            }
            else
            {
                ModelState.AddModelError("", "!!...Error...Debe ingresar fecha del egreso.!!!");
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult CensoFacturaEgreso(Models.Censo.censo Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";
            if (Model.fecha_factura != null)
            {

            }
            else
            {

                variable2 = "FECHA DE FACTURA";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_recepcion_factura != null)
            {

            }
            else
            {

                variable2 = "FECHA DE RECEPCION FACTURA";
                Conteo = Conteo + 1;
            }


            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {
                Model.OBJCenso.id_censo = Model.id_censo;
                Model.OBJCenso.Numero_factura = Model.Numero_factura;
                Model.OBJCenso.valor_egreso = Model.valor_egreso;
                Model.OBJCenso.fecha_factura = Model.fecha_factura;
                Model.OBJCenso.fecha_recepcion_factura = Model.fecha_recepcion_censo;
                Model.OBJCenso.digita_usuario = SesionVar.UserName;
                Model.OBJCenso.digita_fecha = Convert.ToDateTime(DateTime.Now);


                Model.ActualizarCensoEgreso(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("BuscarControlCensoFacturacion", "Censo", new { id = Model.id_censo });
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }
            return View(Model);
        }

        public ActionResult TableroCensoDetallado(DateTime? fechaInicio, DateTime? fechaFin, string documento)
        {
            List<management_censo_tableroDetalladoResult> listado = new List<management_censo_tableroDetalladoResult>();
            var conteo = 0;
            try
            {
                listado = BusClass.GetCensoDetallado(fechaInicio, fechaFin, documento);
                conteo = listado.Count();
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }

            ViewBag.listadoD = listado;
            ViewBag.conteoD = conteo;

            Session["listadoCensoDetallado"] = listado;

            return View();
        }

        public void ReporteCensoDetallado()
        {
            List<management_censo_tableroDetalladoResult> listado = new List<management_censo_tableroDetalladoResult>();

            try
            {
                listado = (List<management_censo_tableroDetalladoResult>)Session["listadoCensoDetallado"];

                if (listado.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ReporteCensoDetallado");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:X1"].Style.Font.Bold = true;
                Sheet.Cells["A1:X1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:X1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:X1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:X1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id historico";
                Sheet.Cells["B1"].Value = "Id censo";
                Sheet.Cells["C1"].Value = "Fecha recepción censo";
                Sheet.Cells["D1"].Value = "Tipo identificación paciente";
                Sheet.Cells["E1"].Value = "Identificación paciente";
                Sheet.Cells["F1"].Value = "Primer nombre paciente";
                Sheet.Cells["G1"].Value = "Segundo nombre paciente";
                Sheet.Cells["H1"].Value = "Primer apellido paciente";
                Sheet.Cells["I1"].Value = "Segundo apellido paciente";
                Sheet.Cells["J1"].Value = "Edad";
                Sheet.Cells["K1"].Value = "Fecha nacimiento";
                Sheet.Cells["L1"].Value = "Fecha ingreso";
                Sheet.Cells["M1"].Value = "Fecha egreso";
                Sheet.Cells["N1"].Value = "Tipo habitación";
                Sheet.Cells["O1"].Value = "Días estancia";
                Sheet.Cells["P1"].Value = "Diagnóstico actual";
                Sheet.Cells["Q1"].Value = "Número factura";
                Sheet.Cells["R1"].Value = "Fecha factura";
                Sheet.Cells["S1"].Value = "Fecha egreso censo";
                Sheet.Cells["T1"].Value = "Existe beneficiario";
                Sheet.Cells["U1"].Value = "Tipo ingreso no beneficiario";
                Sheet.Cells["V1"].Value = "Tiene caso especial";
                Sheet.Cells["W1"].Value = "Caso especial";
                Sheet.Cells["X1"].Value = "Linea pago";

                int row = 2;
                foreach (management_censo_tableroDetalladoResult item in listado)
                {

                    Sheet.Cells["A" + row + ":X1" + row].Style.Font.Name = "Century Gothic";

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_censo_hist;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.id_censo;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.fecha_recepcion_censo;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.tipo_identifi_afiliado;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.num_identifi_afil;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.primer_nombre;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.segundo_nombre;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.primer_apellido;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.segundo_apellido;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.edad;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.fecha_nacimiento;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.fecha_ingreso;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.fecha_egreso;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.descripcionHabitacion;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.dias_estancia;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.dx_actual;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.Numero_factura;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.fecha_factura;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.fecha_egreso_censo;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.existe_bb;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.tipo_ingreso_noBeneficiario;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.tiene_caso_Especial;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.descripcionCasoEspecial;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.linea_pago;

                    Sheet.Cells[string.Format("C{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("K{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("L{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("M{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("S{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }
                Sheet.Cells["A:X"].AutoFitColumns();

                var nombre = "ReporteCenso_detallado_" + DateTime.Now + ".xlsx";
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename= " + nombre + "");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public ActionResult TableroAccionespersonal()
        {
            ViewBag.regional = BusClass.GetRefRegion();
            return View();
        }

        //public PartialViewResult _GeneracionCreacionReporte(string nombre, string documento, int? regional)
        //{
        //    List<vw_censo_control_concurrencia> concu = new List<vw_censo_control_concurrencia>();
        //    List<vw_censo_control_cuentasMedicas> cuentas = new List<vw_censo_control_cuentasMedicas>();
        //    List<vw_censo_control_visitas> visitas = new List<vw_censo_control_visitas>();
        //    List<management_sis_usuarios_controlActividadesCensoResult> usuarios = new List<management_sis_usuarios_controlActividadesCensoResult>();

        //    try
        //    {
        //        concu = BusClass.CensoConcurrenciasTotales();
        //        cuentas = BusClass.CensoCuentasMedicasTotales();
        //        visitas = BusClass.CensoVisitasTotales();
        //        usuarios = BusClass.GetUsuariosCenso();

        //        if (!string.IsNullOrEmpty(nombre))
        //        {
        //            usuarios = usuarios.Where(x => x.nombre.Contains(nombre.ToUpper())).ToList();
        //        }

        //        if (!string.IsNullOrEmpty(documento))
        //        {
        //            usuarios = usuarios.Where(x => x.numero_documento == documento).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    ViewBag.usuarios = usuarios;
        //    ViewBag.tipoCOnsulta = BusClass.CensoConsultaReportesActividades();

        //    ViewBag.concurrencias = concu;
        //    ViewBag.cuentasMedicas = cuentas;
        //    ViewBag.visitas = visitas;

        //    ViewBag.ConteoUsuarios = usuarios.Count();

        //    Session["filtroNombreUsuario"] = nombre;
        //    Session["filtroDocumentoUsuario"] = documento;

        //    Session["listaCensoConcu"] = concu;
        //    Session["listaCensoCuentas"] = cuentas;
        //    Session["listaCensoVisitas"] = visitas;
        //    Session["listaCensoUsuarios"] = usuarios;

        //    return PartialView();
        //}

        public PartialViewResult _GeneracionCreacionReporte(string nombre, string documento, int? regional)
        {
            List<management_censo_control_concurrencia_optimizadaResult> concu = new List<management_censo_control_concurrencia_optimizadaResult>();
            List<management_censo_control_cuentasMedicas_optimizadaResult> cuentas = new List<management_censo_control_cuentasMedicas_optimizadaResult>();
            List<management_censo_control_visitas_optimizadaResult> visitas = new List<management_censo_control_visitas_optimizadaResult>();
            List<management_sis_usuarios_controlActividadesCenso_optimizadaResult> usuarios = new List<management_sis_usuarios_controlActividadesCenso_optimizadaResult>();

            try
            {
                usuarios = BusClass.GetUsuariosCensoOptimizada(regional, documento, nombre);
                concu = BusClass.CensoConcurrenciasTotalesOptimizada(regional, documento, nombre);
                visitas = BusClass.CensoVisitasTotalesOptimizada(regional, documento, nombre);
                cuentas = BusClass.CensoCuentasMedicasTotalesOptimizada(regional, documento, nombre);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.usuarios = usuarios;
            ViewBag.tipoCOnsulta = BusClass.CensoConsultaReportesActividades();

            ViewBag.concurrencias = concu;
            ViewBag.cuentasMedicas = cuentas;
            ViewBag.visitas = visitas;

            ViewBag.ConteoUsuarios = usuarios.Count();

            Session["filtroNombreUsuario"] = nombre;
            Session["filtroDocumentoUsuario"] = documento;

            Session["listaCensoConcu"] = concu;
            Session["listaCensoCuentas"] = cuentas;
            Session["listaCensoVisitas"] = visitas;
            Session["listaCensoUsuarios"] = usuarios;

            ViewBag.regional = BusClass.GetRefRegion();

            return PartialView();
        }

        public void ReporteControlActividades()
        {
            List<management_censo_control_concurrencia_optimizadaResult> concu = new List<management_censo_control_concurrencia_optimizadaResult>();
            List<management_censo_control_cuentasMedicas_optimizadaResult> cuentas = new List<management_censo_control_cuentasMedicas_optimizadaResult>();
            List<management_censo_control_visitas_optimizadaResult> visitas = new List<management_censo_control_visitas_optimizadaResult>();
            List<management_sis_usuarios_controlActividadesCenso_optimizadaResult> usuarios = new List<management_sis_usuarios_controlActividadesCenso_optimizadaResult>();

            ExcelPackage Ep = new ExcelPackage();

            try
            {
                concu = (List<management_censo_control_concurrencia_optimizadaResult>)Session["listaCensoConcu"];

                cuentas = (List<management_censo_control_cuentasMedicas_optimizadaResult>)Session["listaCensoCuentas"];
                visitas = (List<management_censo_control_visitas_optimizadaResult>)Session["listaCensoVisitas"];
                usuarios = (List<management_sis_usuarios_controlActividadesCenso_optimizadaResult>)Session["listaCensoUsuarios"];

                if (usuarios == null)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }


                if (usuarios.Count() > 0)
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("HojaUsuarios");

                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:G1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:G1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:G1"].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A1"].Value = "Documento";
                    Sheet.Cells["B1"].Value = "Nombre";
                    Sheet.Cells["C1"].Value = "Estado";
                    Sheet.Cells["D1"].Value = "Cantidad concurrencias";
                    Sheet.Cells["E1"].Value = "Cantidad cuentas médicas ";
                    Sheet.Cells["F1"].Value = "Cantidad visitas";
                    Sheet.Cells["G1"].Value = "Conteo registros";

                    int row = 2;
                    foreach (management_sis_usuarios_controlActividadesCenso_optimizadaResult item in usuarios)
                    {
                        var conteoConcu = 0;
                        var conteoCuentas = 0;
                        var conteoVisitas = 0;
                        var conteoRegistros = 0;

                        conteoConcu = concu.Where(x => x.id_auditor == item.id_usuario).Count();
                        conteoCuentas = cuentas.Where(x => x.id_auditor_asignado == item.id_usuario).Count();
                        conteoVisitas = visitas.Where(x => x.Auditor_Asignado == item.usuario).Count();

                        conteoRegistros = (conteoConcu + conteoCuentas + conteoVisitas);

                        Sheet.Cells["A" + row + ":H1" + row].Style.Font.Name = "Century Gothic";

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.numero_documento;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.nombre;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.estado;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.conteoConcurrencia;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.conteoCensos;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.conteoVisitas;
                        Sheet.Cells[string.Format("G{0}", row)].Value = conteoRegistros;

                        row++;
                    }
                    Sheet.Cells["A:G"].AutoFitColumns();
                }


                if (concu.Count() > 0)
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("HojaConcurrencia");

                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:K1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:K1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:K1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:K1"].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A1"].Value = "Fecha ingreso";
                    Sheet.Cells["B1"].Value = "Id censo";
                    Sheet.Cells["C1"].Value = "Id concurrencia";
                    Sheet.Cells["D1"].Value = "Ciudad";
                    Sheet.Cells["E1"].Value = "IPS";
                    Sheet.Cells["F1"].Value = "Dx de ingreso";
                    Sheet.Cells["G1"].Value = "Dx de egreso";
                    Sheet.Cells["H1"].Value = "Fecha de egreso";
                    Sheet.Cells["I1"].Value = "Usuario auditor";
                    Sheet.Cells["J1"].Value = "Nombre auditor";
                    Sheet.Cells["K1"].Value = "Última estancia";

                    int row = 2;
                    foreach (management_censo_control_concurrencia_optimizadaResult item in concu)
                    {

                        Sheet.Cells["A" + row + ":K1" + row].Style.Font.Name = "Century Gothic";

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.fecha_ingreso;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.id_censo;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.id_concurrencia;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.nombreCiudad;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.nombreIps;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.diagnosticoPrincipalIngreso;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.diagnosticoPrincipalEgreso;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_egreso;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.usuarioAuditor;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.nombreAuditor;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.ultimaestancia;

                        Sheet.Cells[string.Format("A{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("K{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }
                    Sheet.Cells["A:K"].AutoFitColumns();
                }


                if (cuentas.Count() > 0)
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("HojaCuentasMedicas");

                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:L1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:L1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:L1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:L1"].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A1"].Value = "Fecha de recepción facturas";
                    Sheet.Cells["B1"].Value = "Fecha de asignación facturas";
                    Sheet.Cells["C1"].Value = "Usuario auditor";
                    Sheet.Cells["D1"].Value = "Nombre auditor";
                    Sheet.Cells["E1"].Value = "Ciudad";
                    Sheet.Cells["F1"].Value = "NIT";
                    Sheet.Cells["G1"].Value = "SAP";
                    Sheet.Cells["H1"].Value = "Prestador";
                    Sheet.Cells["I1"].Value = "Núumero de factura";
                    Sheet.Cells["J1"].Value = "Id detalle factura";
                    Sheet.Cells["K1"].Value = "Valor factura";
                    Sheet.Cells["L1"].Value = "Último estado de la factura";

                    int row = 2;
                    foreach (management_censo_control_cuentasMedicas_optimizadaResult item in cuentas)
                    {

                        Sheet.Cells["A" + row + ":L1" + row].Style.Font.Name = "Century Gothic";

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.fecha_recepcion_fac;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.fecha_asignacion_auditor;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.usuarioAuditorAsignado;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.nombreAuditorAsignado;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.nombreCiudad;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.Nit;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.homologado_sap;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.prestador;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.num_factura;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.id_af;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.valor_neto;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.descripcionEstado;

                        Sheet.Cells[string.Format("A{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("B{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }
                    Sheet.Cells["A:L"].AutoFitColumns();
                }


                if (visitas.Count() > 0)
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("HojaVisitas");

                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:J1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:J1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:J1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:J1"].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A1"].Value = "Fecha de visita";
                    Sheet.Cells["B1"].Value = "Usuario auditor";
                    Sheet.Cells["C1"].Value = "Nombre auditor";
                    Sheet.Cells["D1"].Value = "NIT";
                    Sheet.Cells["E1"].Value = "SAP";
                    Sheet.Cells["F1"].Value = "Prestador";
                    Sheet.Cells["G1"].Value = "Fecha final visita";
                    Sheet.Cells["H1"].Value = "Estado de la visita";
                    Sheet.Cells["I1"].Value = "Novedad visita";
                    Sheet.Cells["J1"].Value = "Id visita";

                    int row = 2;
                    foreach (management_censo_control_visitas_optimizadaResult item in visitas)
                    {

                        Sheet.Cells["A" + row + ":J1" + row].Style.Font.Name = "Century Gothic";

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.fecha_visita;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.Auditor_Asignado;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.nombreAuditor;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.no_id_prestador;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.cod_sap;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.razon_social;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_final_visita;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.estadoVisita;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.otra_novedad;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.id_cronograma_visitas;

                        Sheet.Cells[string.Format("A{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }
                    Sheet.Cells["A:J"].AutoFitColumns();
                }

                var nombreArcivo = "ReporteControlActividades_" + DateTime.Now + ".xlsx";
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename= " + nombreArcivo + "");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public ActionResult CarguemasivoDatosCenso()
        {
            return View();
        }

        public JsonResult GuardarCargueMasivo(HttpPostedFileBase archivo)
        {
            var mensaje = "";
            var respuesta = "";
            var rta = 0;
            Models.Censo.censo Modelo = new Models.Censo.censo();

            try
            {
                if (archivo.ContentLength > 0)
                {
                    CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                    var asposeOptions = new Aspose.Cells.LoadOptions
                    {
                        MemorySetting = MemorySetting.MemoryPreference
                    };

                    Workbook wb = new Workbook(archivo.InputStream, asposeOptions);
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

                    cargue_censo_ah_lote lote = new cargue_censo_ah_lote();
                    lote.fecha_digita = DateTime.Now;
                    lote.usuario_digita = SesionVar.UserName;
                    respuesta = Modelo.ExcelMasivoAHCenso(dataTable, lote, ref MsgRes);

                    if (Modelo.rtaIngresoFacturasDetalle == 1)
                    {
                        mensaje = Modelo.mensajeIngresoFacturasDetalle;
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "ERROR AL INGRESAR CARGUE AH EN EL ARCHIVO: " + archivo.FileName + " - " + Modelo.mensajeIngresoFacturasDetalle;
                    }
                }
                else
                {
                    mensaje = "ARCHIVO SIN DATOS";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL CARGAR MASIVO: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult TableroAhAseguramiento(int? regional, int? unis, int? municipio)
        {
            List<management_censo_aseguramientoTableroControlResult> listado = new List<management_censo_aseguramientoTableroControlResult>();

            try
            {
                listado = BusClass.DatosCensoAseguramiento();

                if (regional != null && regional != 0)
                {
                    Ref_regional reg = BusClass.GetRefRegionId((int)regional);
                    listado = listado.Where(x => x.regional == reg.indice).ToList();
                }

                if (unis != null && unis != 0)
                {
                    Ref_odont_unis un = BusClass.traerUnisId(unis);
                    listado = listado.Where(x => x.unis == un.descripcion).ToList();
                }

                if (municipio != null && municipio != 0)
                {
                    Ref_ciudades ci = BusClass.CiudadesId(municipio);
                    listado = listado.Where(x => x.municipio == ci.nombre).ToList();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.conteo = listado.Count();
            ViewBag.regionales = BusClass.GetRefRegion();

            return View();

        }

        public string ObtenerUnis(int idregional)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_odont_unis> Unis = BusClass.Odont_unisIdRegional(idregional);
            foreach (var item in Unis)
            {
                result += "<option value='" + item.id_ref_unis + "'>" + item.descripcion + "</option>";
            }

            return result;
        }

        public string ObtenerCiudades(int idunis)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_ciudades> Ciudades = BusClass.GetCiudadesXUnis(idunis);
            foreach (var item in Ciudades)
            {
                result += "<option value='" + item.id_ref_ciudades + "'>" + item.nombre + "</option>";
            }

            return result;
        }

        public PartialViewResult ModalDetallesAseguramiento(int? Lot, int? det, string nombre)
        {
            ViewBag.idLote = Lot;
            ViewBag.idDetalle = det;
            ViewBag.nombre = nombre;

            ViewBag.lineasPago = BusClass.listaCensoLineasPago();
            ViewBag.tipoHabitacion = BusClass.ListadoTipoGabitacion();
            //List<management_censo_aseguramientoTableroControl_detallesResult> listadoDatos = BusClass.DatosCensoAseguramiento_detalleId(det);

            management_censo_aseguramientoTableroControl_idResult dato = BusClass.TraerRegistroAH(det);
            List<management_censo_aseguramientoTableroControl_idResult> listado = new List<management_censo_aseguramientoTableroControl_idResult>();
            listado.Add(dato);

            ViewBag.listado = listado;

            return PartialView();
        }

        public JsonResult BuscarCie10()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length > 0)
                {
                    term = term.ToUpper();

                    List<Ref_cie10> tiga = new List<Ref_cie10>();
                    tiga = BusClass.GetCie10Bycodigo(term);

                    var lista = (from ti in tiga
                                 select new
                                 {
                                     id = ti.id_cie10,
                                     label = ti.id_cie10 + "-" + ti.des,
                                     des = ti.des
                                 }).Distinct().OrderBy(f => f.label).Take(15);
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GuardarDetallesAseguramiento(Models.Censo.censo Modelo)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                cargue_censo_ah_registros_detalle det = new cargue_censo_ah_registros_detalle
                {
                    id_registro = (int)Modelo.idDetalle,
                    tipo_habitacion = Modelo.tipoHabitacion,
                    medico_auditor = SesionVar.UserName,
                    linea_pago = Modelo.lineaPago,
                    cie10 = Modelo.cie10,
                    descripcion_cie10 = Modelo.cie10Des,
                    caso_inferior_72horas = Modelo.caso_inferior_72horas,
                    nota_auditoria = Modelo.notas,
                    fecha_digita = DateTime.Now,
                    usuario_digita = SesionVar.UserName
                };

                var inserta = BusClass.InsertarDetalleRegistroAH(det, ref MsgRes);
                if (inserta != 0)
                {
                    mensaje = "REGISTRO INGRESADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL INGRESAR EL REGISTRO";
                }


            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL INGRESAR EL REGISTRO: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult EliminarGestionDetalle(int? idDetalle)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var elimina = BusClass.EliminarDetalleAHCenso(idDetalle);
                if (elimina != 0)
                {
                    mensaje = "REGISTRO ELIMINADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR EL REGISTRO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR EL REGISTRO: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }


        public ActionResult TableroAlertasEpidemiologicas()
        {
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listado = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();

            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoAbiertas = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoInformativa = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoInmediata = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoAGestion = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            
            
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoCerradas = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoEnGestion = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoGestionadas = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();

            try
            {
                listado = BusClass.ListadoAlertasEpidemiologicasGestionadas();

                listadoAbiertas = listado.Where(x => x.id_registro == null).ToList();

                listadoInformativa = listadoAbiertas.Where(x => x.tipo_alerta == "Alerta informativa").ToList();
                listadoInmediata = listadoAbiertas.Where(x => x.tipo_alerta == "Alerta para gestión inmediata").ToList();
                listadoAGestion = listadoAbiertas.Where(x => x.tipo_alerta == "Alerta que requiere alguna gestión").ToList();
                
                listadoCerradas = listado.Where(x => x.id_registro != null).ToList();

                listadoGestionadas = listadoCerradas.Where(x => x.cerrada == 1).ToList();
                listadoEnGestion = listadoCerradas.Where(x => x.cerrada != 1).ToList();
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoInformativa = listadoInformativa;
            ViewBag.listadoInmediata = listadoInmediata;
            ViewBag.listadoAGestion = listadoAGestion;
            ViewBag.listadoEnGestion = listadoEnGestion;
            ViewBag.listadoGestionadas = listadoGestionadas;

            ViewBag.conteoInformativa = listadoInformativa.Count();
            ViewBag.conteoInmediata = listadoInmediata.Count();
            ViewBag.conteoGestion = listadoAGestion.Count();
            ViewBag.conteoEnGestion = listadoEnGestion.Count();
            ViewBag.conteoGestionadas = listadoGestionadas.Count();

            return View();
        }

        public void ReporteAlertasEpidemiologicas()
        {
            List<vw_reporte_alertas_epidemiologia> listado = new List<vw_reporte_alertas_epidemiologia>();

            try
            {
                listado = BusClass.ListadoAlertasEpidemiologicasReporte();

                if (listado.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ReporteCensoDetallado");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:AB1"].Style.Font.Bold = true;
                Sheet.Cells["A1:AB1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AB1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AB1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AB1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id censo";
                Sheet.Cells["B1"].Value = "Id concurrencia";
                Sheet.Cells["C1"].Value = "Id alertas generadas concurrencia";
                Sheet.Cells["D1"].Value = "Afi tipo doc";
                Sheet.Cells["E1"].Value = "Documento afiliado";
                Sheet.Cells["F1"].Value = "Afi nom";
                Sheet.Cells["G1"].Value = "Edad";
                Sheet.Cells["H1"].Value = "Ciudad IPs";
                Sheet.Cells["I1"].Value = "Nit Ips";
                Sheet.Cells["J1"].Value = "Nombre Ips";
                Sheet.Cells["K1"].Value = "Fecha ingreso";
                Sheet.Cells["L1"].Value = "Diagnostico censo";
                Sheet.Cells["M1"].Value = "Nombre diagnostico censo";
                Sheet.Cells["N1"].Value = "Fecha egreso";
                Sheet.Cells["O1"].Value = "Diagnostico egreso";
                Sheet.Cells["P1"].Value = "Nombre diagnostico egreso";
                Sheet.Cells["Q1"].Value = "Incapacidad";
                Sheet.Cells["R1"].Value = "Fecha inicial incapacidad";
                Sheet.Cells["S1"].Value = "Fecha final incapacidad";
                Sheet.Cells["T1"].Value = "Nombre auditor";
                Sheet.Cells["U1"].Value = "Diagnostico genero alerta";
                Sheet.Cells["V1"].Value = "Nombre diagnostico que genero alerta";
                Sheet.Cells["W1"].Value = "Grupo diagnostico";
                Sheet.Cells["X1"].Value = "Tipo evento";
                Sheet.Cells["Y1"].Value = "Nombre de alerta";
                Sheet.Cells["Z1"].Value = "Descripcion alerta";
                Sheet.Cells["AA1"].Value = "Tipo alerta";
                Sheet.Cells["AB1"].Value = "Alerta confirmada";
                //Sheet.Cells["AC1"].Value = "Id registro";
                //Sheet.Cells["AD1"].Value = "Id concurrencia gestion";
                //Sheet.Cells["AE1"].Value = "Id censo gestion";
                //Sheet.Cells["AF1"].Value = "Id tipo CIE10";
                //Sheet.Cells["AG1"].Value = "Alerta";
                //Sheet.Cells["AH1"].Value = "Confirmacion alerta";
                //Sheet.Cells["AI1"].Value = "Estado final paciente";
                //Sheet.Cells["AJ1"].Value = "Requiere analisis";
                //Sheet.Cells["AK1"].Value = "Requiere cargue soportes";
                //Sheet.Cells["AL1"].Value = "Requiere verificacion Sivigilia";
                //Sheet.Cells["AM1"].Value = "Observaciones";
                //Sheet.Cells["AN1"].Value = "Motivo descarte";
                //Sheet.Cells["AO1"].Value = "Fecha digita gestion";
                //Sheet.Cells["AP1"].Value = "Id gestion analisis";
                //Sheet.Cells["AQ1"].Value = "Id gestion";
                //Sheet.Cells["AR1"].Value = "Fecha analisis";
                //Sheet.Cells["AS1"].Value = "Nombres beneficiario";
                //Sheet.Cells["AT1"].Value = "Tipo beneficiario";
                //Sheet.Cells["AU1"].Value = "Identificacion beneficiario";
                //Sheet.Cells["AV1"].Value = "Fecha inicio servicio";
                //Sheet.Cells["AW1"].Value = "Edad momento evento";
                //Sheet.Cells["AX1"].Value = "Tipo evento gestion analisis";
                //Sheet.Cells["AY1"].Value = "Fecha ocurrencia evento";
                //Sheet.Cells["AZ1"].Value = "Fecha fin evento";
                //Sheet.Cells["BA1"].Value = "Fuente reporte";
                //Sheet.Cells["BB1"].Value = "Nombre reportante";
                //Sheet.Cells["BC1"].Value = "Nombre prestador ocurre evento";
                //Sheet.Cells["BD1"].Value = "Nit prestador";
                //Sheet.Cells["BE1"].Value = "Profesionales entidades responsables nivel1";
                //Sheet.Cells["BF1"].Value = "Diagnosticos";
                //Sheet.Cells["BG1"].Value = "Objetivo auditoria";
                //Sheet.Cells["BH1"].Value = "Descripcion evento";
                //Sheet.Cells["BI1"].Value = "Ambito sucedio evento";
                //Sheet.Cells["BJ1"].Value = "Analisis critico caso";
                //Sheet.Cells["BK1"].Value = "Concepto resolutividad primer nivel";
                //Sheet.Cells["BL1"].Value = "Aplicacion guias terapeuticas";
                //Sheet.Cells["BM1"].Value = "Periodicidad controles medicos";
                //Sheet.Cells["BN1"].Value = "Aplicacion pruebas diagnosticos monitoreo";
                //Sheet.Cells["BO1"].Value = "Informacion paciente cuidadores plan terapeutico";
                //Sheet.Cells["BP1"].Value = "Descripcion eventuales causas muerte relacionadas";
                //Sheet.Cells["BQ1"].Value = "Conclusiones analisis caso";
                //Sheet.Cells["BR1"].Value = "Concepto auditoria evento prevenible";
                //Sheet.Cells["BS1"].Value = "Propuesta acciones mejora";
                //Sheet.Cells["BT1"].Value = "Fecha digita gestion analisis";
                //Sheet.Cells["BU1"].Value = "Id demora";
                //Sheet.Cells["BV1"].Value = "Id gestion analisis demoras";
                //Sheet.Cells["BW1"].Value = "Id ref demoras";
                //Sheet.Cells["BX1"].Value = "Descripcion ref demoras";
                //Sheet.Cells["BY1"].Value = "Id ref detallado";
                //Sheet.Cells["BZ1"].Value = "Descripcion demorada detallado";
                //Sheet.Cells["CA1"].Value = "Respuesta demora";
                //Sheet.Cells["CB1"].Value = "Observaciones demoras";
                //Sheet.Cells["CC1"].Value = "Fecha digita demoras";




                int row = 2;
                foreach (vw_reporte_alertas_epidemiologia item in listado)
                {

                    Sheet.Cells["A" + row + ":AB1" + row].Style.Font.Name = "Century Gothic";

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_censo;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.id_concurrencia;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.id_alertas_generadas_concurrencia;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.afi_tipo_doc;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.Documento_Afiliado;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.afi_nom;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.edad;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.CiudadIPs;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.Nit_Ips;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.Nombre_Ips;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.fecha_ingreso;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.Diagnostico_Censo;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.Nombre_Diagnostico_Censo;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.fecha_egreso;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.Diagnostico_Egreso;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.Nombre_Diagnostico_Egreso;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.Incapacidad;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.Fecha_Inicial_Incapacidad;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.Fecha_final_Incapacidad;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.Nombre_auditor;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.Diagnostico_genero_alerta;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.Nombre_Diagnostico_que_genero_alerta;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.grupo_diagnostico;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.Tipo_Evento;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.Nombre_De_Alerta;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.Descripcion_Alerta;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.tipo_alerta;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.Alerta_Confirmada;

                    //Sheet.Cells[string.Format("AC{0}", row)].Value = item.id_registro;
                    //Sheet.Cells[string.Format("AD{0}", row)].Value = item.id_concurrencia_gestion;
                    //Sheet.Cells[string.Format("AE{0}", row)].Value = item.id_censo_gestion;
                    //Sheet.Cells[string.Format("AF{0}", row)].Value = item.idTipoCie10;
                    //Sheet.Cells[string.Format("AG{0}", row)].Value = item.alerta;
                    //Sheet.Cells[string.Format("AH{0}", row)].Value = item.confirmacion_alerta;
                    //Sheet.Cells[string.Format("AI{0}", row)].Value = item.estado_final_paciente;
                    //Sheet.Cells[string.Format("AJ{0}", row)].Value = item.requiere_analisis;
                    //Sheet.Cells[string.Format("AK{0}", row)].Value = item.requiere_cargue_soportes;
                    //Sheet.Cells[string.Format("AL{0}", row)].Value = item.requiere_verificacion_sivigilia;
                    //Sheet.Cells[string.Format("AM{0}", row)].Value = item.observaciones;
                    //Sheet.Cells[string.Format("AN{0}", row)].Value = item.motivo_descarte;
                    //Sheet.Cells[string.Format("AO{0}", row)].Value = item.fecha_digita_gestion;
                    //Sheet.Cells[string.Format("AP{0}", row)].Value = item.id_gestionAnalisis;
                    //Sheet.Cells[string.Format("AQ{0}", row)].Value = item.id_gestion;
                    //Sheet.Cells[string.Format("AR{0}", row)].Value = item.fecha_analisis;
                    //Sheet.Cells[string.Format("AS{0}", row)].Value = item.nombres_beneficiario;
                    //Sheet.Cells[string.Format("AT{0}", row)].Value = item.tipo_beneficiario;
                    //Sheet.Cells[string.Format("AU{0}", row)].Value = item.identificacion_beneficiario;
                    //Sheet.Cells[string.Format("AV{0}", row)].Value = item.fecha_inicio_servicio;
                    //Sheet.Cells[string.Format("AW{0}", row)].Value = item.edad_momento_evento;
                    //Sheet.Cells[string.Format("AX{0}", row)].Value = item.tipo_evento_gestionAnalisis;
                    //Sheet.Cells[string.Format("AY{0}", row)].Value = item.fecha_ocurrencia_evento;
                    //Sheet.Cells[string.Format("AZ{0}", row)].Value = item.fecha_fin_evento;
                    //Sheet.Cells[string.Format("BA{0}", row)].Value = item.fuente_reporte;
                    //Sheet.Cells[string.Format("BB{0}", row)].Value = item.nombre_reportante;
                    //Sheet.Cells[string.Format("BC{0}", row)].Value = item.nombre_prestador_ocurre_evento;
                    //Sheet.Cells[string.Format("BD{0}", row)].Value = item.nit_prestador;
                    //Sheet.Cells[string.Format("BE{0}", row)].Value = item.profesionales_entidadesresponsables_nivel1;
                    //Sheet.Cells[string.Format("BF{0}", row)].Value = item.diagnosticos;
                    //Sheet.Cells[string.Format("BG{0}", row)].Value = item.objetivo_auditoria;
                    //Sheet.Cells[string.Format("BH{0}", row)].Value = item.descripcion_evento;
                    //Sheet.Cells[string.Format("BI{0}", row)].Value = item.ambito_sucedio_evento;
                    //Sheet.Cells[string.Format("BJ{0}", row)].Value = item.analisis_critico_caso;
                    //Sheet.Cells[string.Format("BK{0}", row)].Value = item.concepto_resolutividad_primer_nivel;
                    //Sheet.Cells[string.Format("BL{0}", row)].Value = item.aplicacion_guias_terapeuticas;
                    //Sheet.Cells[string.Format("BM{0}", row)].Value = item.periodicidad_controles_medicos;
                    //Sheet.Cells[string.Format("BN{0}", row)].Value = item.aplicacion_pruebas_diagnosticos_monitoreo;
                    //Sheet.Cells[string.Format("BO{0}", row)].Value = item.informacion_paciente_cuidadores_plan_terapeutico;
                    //Sheet.Cells[string.Format("BP{0}", row)].Value = item.descripcion_eventuales_Causasmuerte_relacionadas;
                    //Sheet.Cells[string.Format("BQ{0}", row)].Value = item.conclusiones_analisis_caso;
                    //Sheet.Cells[string.Format("BR{0}", row)].Value = item.concepto_auditoria_evento_prevenible;
                    //Sheet.Cells[string.Format("BS{0}", row)].Value = item.propuesta_acciones_mejora;
                    //Sheet.Cells[string.Format("BT{0}", row)].Value = item.fecha_digita_gestionAnalisis;
                    //Sheet.Cells[string.Format("BU{0}", row)].Value = item.id_demora;
                    //Sheet.Cells[string.Format("BV{0}", row)].Value = item.id_gestionAnalisis_demoras;
                    //Sheet.Cells[string.Format("BW{0}", row)].Value = item.id_ref_Demoras;
                    //Sheet.Cells[string.Format("BX{0}", row)].Value = item.descripcion_refDemoras;
                    //Sheet.Cells[string.Format("BY{0}", row)].Value = item.id_refDetallado;
                    //Sheet.Cells[string.Format("BZ{0}", row)].Value = item.descripcion_demoradaDetallado;
                    //Sheet.Cells[string.Format("CA{0}", row)].Value = item.respuesta_demora;
                    //Sheet.Cells[string.Format("CB{0}", row)].Value = item.observaciones_demoras;
                    //Sheet.Cells[string.Format("CC{0}", row)].Value = item.fecha_digita_demoras;

                    Sheet.Cells[string.Format("K{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("S{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }

                Sheet.Cells["A:AB"].AutoFitColumns();

                var nombre = "ReporteAlertasEpidemiologicas_" + DateTime.Now + ".xlsx";
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename= " + nombre + "");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public void ReporteAlertasEpidemiologicasGestionadas()
        {
            List<vw_reporte_alertas_epidemiologia_gestiones> listado = new List<vw_reporte_alertas_epidemiologia_gestiones>();

            try
            {
                listado = BusClass.ListadoAlertasEpidemiologicasReporteGestiones();

                if (listado.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ReporteCensoDetallado");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:AB1"].Style.Font.Bold = true;
                Sheet.Cells["A1:AB1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AB1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AB1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AB1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id censo";
                Sheet.Cells["B1"].Value = "Id concurrencia";
                Sheet.Cells["C1"].Value = "Id alertas generadas concurrencia";
                Sheet.Cells["D1"].Value = "Afi tipo doc";
                Sheet.Cells["E1"].Value = "Documento afiliado";
                Sheet.Cells["F1"].Value = "Afi nom";
                Sheet.Cells["G1"].Value = "Edad";
                Sheet.Cells["H1"].Value = "Ciudad IPs";
                Sheet.Cells["I1"].Value = "Nit Ips";
                Sheet.Cells["J1"].Value = "Nombre Ips";
                Sheet.Cells["K1"].Value = "Fecha ingreso";
                Sheet.Cells["L1"].Value = "Diagnostico censo";
                Sheet.Cells["M1"].Value = "Nombre diagnostico censo";
                Sheet.Cells["N1"].Value = "Fecha egreso";
                Sheet.Cells["O1"].Value = "Diagnostico egreso";
                Sheet.Cells["P1"].Value = "Nombre diagnostico egreso";
                Sheet.Cells["Q1"].Value = "Incapacidad";
                Sheet.Cells["R1"].Value = "Fecha inicial incapacidad";
                Sheet.Cells["S1"].Value = "Fecha final incapacidad";
                Sheet.Cells["T1"].Value = "Nombre auditor";
                Sheet.Cells["U1"].Value = "Diagnostico genero alerta";
                Sheet.Cells["V1"].Value = "Nombre diagnostico que genero alerta";
                Sheet.Cells["W1"].Value = "Grupo diagnostico";
                Sheet.Cells["X1"].Value = "Tipo evento";
                Sheet.Cells["Y1"].Value = "Nombre de alerta";
                Sheet.Cells["Z1"].Value = "Descripcion alerta";
                Sheet.Cells["AA1"].Value = "Tipo alerta";
                Sheet.Cells["AB1"].Value = "Alerta confirmada";

                int row = 2;
                foreach (vw_reporte_alertas_epidemiologia_gestiones item in listado)
                {

                    Sheet.Cells["A" + row + ":AB1" + row].Style.Font.Name = "Century Gothic";

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_censo;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.id_concurrencia;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.id_alertas_generadas_concurrencia;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.afi_tipo_doc;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.Documento_Afiliado;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.afi_nom;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.edad;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.CiudadIPs;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.Nit_Ips;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.Nombre_Ips;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.fecha_ingreso;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.Diagnostico_Censo;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.Nombre_Diagnostico_Censo;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.fecha_egreso;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.Diagnostico_Egreso;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.Nombre_Diagnostico_Egreso;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.Incapacidad;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.Fecha_Inicial_Incapacidad;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.Fecha_final_Incapacidad;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.Nombre_auditor;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.Diagnostico_genero_alerta;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.Nombre_Diagnostico_que_genero_alerta;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.grupo_diagnostico;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.Tipo_Evento;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.Nombre_De_Alerta;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.Descripcion_Alerta;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.tipo_alerta;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.Alerta_Confirmada;
                    Sheet.Cells[string.Format("K{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("S{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }

                Sheet.Cells["A:AB"].AutoFitColumns();

                var nombre = "ReporteAlertasEpidemiologicasGestiones_" + DateTime.Now + ".xlsx";
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename= " + nombre + "");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                      "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public PartialViewResult ModalGestionEpidemiologica(int? idConcu, int? idCenso, int? idRef)
        {
            ViewBag.idConcu = idConcu;
            ViewBag.idCenso = idCenso;
            ViewBag.idRef = idRef;

            return PartialView();
        }


        public JsonResult GuardarGestionEpidemiologica(int? idConcu, int? idCenso, int? idRef, string confirmacion, string estado_paciente, string requiere_analisis, string requiere_soportes
            , string requiere_sivigila, string observaciones, string motivo_descarte)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                alerta_epidemiologica_gestion obj = new alerta_epidemiologica_gestion
                {
                    id_concurrencia = idConcu,
                    id_censo = idCenso,
                    id_Ref = idRef,
                    confirmacion_alerta = confirmacion,
                    estado_final_paciente = estado_paciente,
                    requiere_analisis = requiere_analisis,
                    requiere_cargue_soportes = requiere_soportes,
                    requiere_verificacion_sivigilia = requiere_sivigila,
                    observaciones = observaciones,
                    motivo_descarte = motivo_descarte,
                    fecha_digita = DateTime.Now,
                    usuario_digita = SesionVar.UserName
                };

                var inserta = BusClass.InsertarGestionAlertaEpidemio(obj);
                if (inserta != 0)
                {
                    if (confirmacion == "DESCARTADA")
                    {
                        EnviarCorreoAuditorGestionEpidemiologica(inserta);
                    }

                    mensaje = "GESTIÓN REALIZADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE LA GESTIÓN";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE LA GESTIÓN: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public void EnviarCorreoAuditorGestionEpidemiologica(int? idRegistro)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            sis_usuario usuario = new sis_usuario();
            management_alertas_epidemiologicas_tableroControl_idGestionResult datos = new management_alertas_epidemiologicas_tableroControl_idGestionResult();

            try
            {
                datos = BusClass.TraerDatosGestionId(idRegistro);

                if (datos != null)
                {
                    usuario = BusClass.datosUsuarioNombre(datos.Nombre_auditor);
                    if (usuario == null)
                    {
                        throw new Exception("No hay datos para este auditor");
                    }
                }
                else
                {
                    throw new Exception("No hay datos para esta gestión");
                }

                string textBody = "Estimado/a " + usuario.nombre;

                textBody += "<br/>";
                textBody += "<br/>";
                textBody += $"Se ha identificado una marcación incorrecta en la alerta de salud para el paciente {datos.Documento_Afiliado} - {datos.afi_nom} de la hospitalización identificada con id concurrencia {datos.id_concurrencia} en la IPS {datos.Nombre_Ips}";
                textBody += "<br/>";
                textBody += "<br/>";
                textBody += "Las características de la alerta marcada fueron: ";
                textBody += "<br/>";
                textBody += "<br/>";
                textBody += $"{datos.Tipo_Evento} - {datos.Nombre_De_Alerta} - {datos.Descripcion_Alerta} - {datos.codigo} - {datos.descripcion}.";
                textBody += "<br/>";
                textBody += "<br/>";
                textBody += $"Finalmente, el motivo del descarte por el areá de Epidemiología fue: {datos.motivo_descarte}.";
                textBody += "<br/>";
                textBody += "<br/>";
                textBody += "Por favor, revise los criterios de marcación y garantice que no vuelva a suceder.";
                textBody += "<br/>";
                textBody += "<br/>";
                textBody += "Gracias por su colaboración y compromiso para el mejoramiento continuo de nuestros procesos.";
                textBody += "<br/>";
                textBody += "<br/>";

                textBody += "Atentamente,";
                textBody += "<br/>";
                textBody += "Asalud";
                textBody += "<br/>";

                string mailBody = "";
                string mailCSS = "";
                mailCSS = "<style>";
                mailCSS += @"body { margin: 0; padding: 0; }";
                mailCSS += @".PageContainer{ background-repeat: no-repeat; width: 600px; height: 900px; }";
                mailCSS += @"#ContentContainer { clear: both; width: 600px; height: 187px; }";
                mailCSS += @"#LeftPane { width: 260px; padding-top: 150px; float: left; padding-left: 40px; color='#F1C40F';}";
                mailCSS += @"#RightPane { width: 230px; float: right; padding-top: 150px; text-align: center; padding-right: 30px; }";
                mailCSS += @"#RightPane2 {width: 260px; float: right; padding-top: 150px; text-align: center; }";
                mailCSS += @".tableClass { border: dashed 1px #000000; padding: 5px 5px 5px 5px; }";
                mailCSS += @"#FooterContainer { clear: both; width: 590px; padding-top: 515px; height: 80px; font-size: 0.6em; padding-left: 10px; font-weight: bold; }";
                mailCSS += @".a { color: #063971; }";

                mailCSS += "</style>";
                mailBody = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                mailBody += "<div class='PageContainer' style=' font-family: Century Gothic, Century Gothic, sans-serif; '>";
                mailBody += "<div id='ContentContainer' style=' color: #063971;'>";

                mailBody += "<div id='LeftPane' style='font-size: 14.5px;'>";
                mailBody += "<br />";

                mailBody += textBody;

                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "</div>";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<STRONG>ASALUD</STRONG>";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asaludltda.com</a>";
                mailBody += "<br />";
                mailBody += "Bogotá";
                mailBody += "</div>";

                mailBody += "<br />";
                mailBody += "<div id='RightPane2' align='center'  style='font-size: 10.5px;'>";
                mailBody += "“El contenido de este mensaje electrónico, su texto o elementos adjuntos, son de uso confidencial y privado entre el remitente y su (s) destinatario (s). Cualquier uso o distribución indebida o sin autorización escrita por parte del remitente, se encuentran estrictamente prohibidas y acarrean sanciones penales. Por tanto, en caso de recibir este mensaje por error, por favor notificarlo y devolverlo de inmediato al remitente del mismo, sin guardar copia alguna...";
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "Por un medio ambiente sano, imprima solo lo necesario.";
                mailBody += "</div>";

                mailBody += "</div>";
                mailBody += "</div>";

                System.Net.Mail.SmtpClient objMail = new System.Net.Mail.SmtpClient();
                objMail.Host = smtpSection.Network.Host;
                objMail.Port = smtpSection.Network.Port;
                objMail.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                objMail.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody.ToString(), new System.Net.Mime.ContentType("text/html"));
                LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                resource.ContentId = "dealer_logo";
                htmlView.LinkedResources.Add(resource);

                mailMessage.AlternateViews.Add(htmlView);

                mailMessage.From = new MailAddress("admin@asaludltda.com");

                //mailMessage.To.Add("notificacionespqrs@asaludltda.com");
                mailMessage.To.Clear();


                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    if (!string.IsNullOrEmpty(usuario.correo_ins))
                    {
                        mailMessage.To.Add(usuario.correo_ins);
                    }
                }
                else
                {
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                }

                mailMessage.Subject = "[Mensaje Automático]" + "Notificación de marcación errada en alerta de salud.";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                mensaje = "NOTIFICACIÓN ENVIADA CORRECTAMENTE.";

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ENVIAR NOTIFICACIÓN SOLUCIONADOR.";
            }
        }

        public ActionResult TableroAlertasGestionadas()
        {
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listado = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();

            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoInformativa = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoInmediata = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoAGestion = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> listadoGestionadas = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();

            try
            {
                listado = BusClass.ListadoAlertasEpidemiologicasGestionadasTotal();

                listadoInformativa = listado.Where(x => x.tipo_alerta == "Alerta informativa" && x.cerrada == null).ToList();
                listadoInmediata = listado.Where(x => x.tipo_alerta == "Alerta para gestión inmediata" && x.cerrada == null).ToList();
                listadoAGestion = listado.Where(x => x.tipo_alerta == "Alerta que requiere alguna gestión" && x.cerrada == null).ToList();
                listadoGestionadas = listado.Where(x => x.cerrada == 1).ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoInmediata = listadoInmediata;
            ViewBag.listadoAGestion = listadoAGestion;
            ViewBag.listadoInformativa = listadoInformativa;
            ViewBag.listadoGestionadas = listadoGestionadas;

            ViewBag.conteoInmediata = listadoInmediata.Count();
            ViewBag.conteoGestion = listadoAGestion.Count();
            ViewBag.conteoInformativa = listadoInformativa.Count();
            ViewBag.conteoGestionadas = listadoGestionadas.Count();

            return View();
        }

        public PartialViewResult ModalGestionesRealizadas(int? idRegistro, int? cerrada, int? tipoIngreso)
        {
            // tipo ingreso - 1: TABLERO ALERTAS EN SALUD
            // tipo ingreso - 2: Tablero Gestión de alertas: Auditoría

            management_alertas_epidemiologicas_gestionesResult ges = BusClass.TraerGestionAlertasEpidemiologicas(idRegistro);
            ViewBag.idGestion = ges != null ? ges.id_registro : 0;

            List<management_alertas_epidemiologicas_gestionesResult> lista = new List<management_alertas_epidemiologicas_gestionesResult>();
            lista.Add(ges);

            ViewBag.dato = lista;
            ViewBag.idRegistro = idRegistro;
            ViewBag.cerrada = cerrada;
            ViewBag.tipoIngreso = tipoIngreso;

            return PartialView(ges);
        }

        public PartialViewResult ModalCargarSoportes(int? idRegistro, int? tipo, int? cerrada, int? tipoIngreso)
        {
            // tipo ingreso - 1: TABLERO ALERTAS EN SALUD
            // tipo ingreso - 2: Tablero Gestión de alertas: Auditoría

            ViewBag.idRegistro = idRegistro;
            ViewBag.tipo = tipo;

            ViewBag.listadoArchivos = BusClass.ListadoArchivosEpidemiologicaId(idRegistro, tipo);
            ViewBag.cerrada = cerrada;
            ViewBag.tipoIngreso = tipoIngreso;

            return PartialView();
        }

        public JsonResult GuardarSoportesGE(int? idRegistro, int? tipo, List<HttpPostedFileBase> archivos)
        {
            var mensaje = "";
            var mensajeFinal = "";
            var rta = 0;
            try
            {
                mensaje = GuardarSoporteTipo(idRegistro, tipo, archivos);
                if (mensaje == "")
                {
                    mensajeFinal = "SOPORTES INGRESADOS CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensajeFinal = mensaje;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return Json(new { mensaje = mensajeFinal, rta = rta });
        }

        public string GuardarSoporteTipo(int? idRegistro, int? tipo, List<HttpPostedFileBase> archivos)
        {
            var respuesta = "";

            List<alerta_epidemiologica_gestion_archivos> listado = new List<alerta_epidemiologica_gestion_archivos>();

            try
            {
                if (archivos.Count() > 0)
                {
                    foreach (var item in archivos)
                    {
                        HttpPostedFileBase archivo = item;
                        if (archivo.ContentLength > 0)
                        {
                            alerta_epidemiologica_gestion_archivos validarArchivo = GuardarArchivoPlanMejora(archivo, idRegistro, tipo, 0);
                            if (validarArchivo != null)
                            {
                                if (validarArchivo.id_registro != null)
                                {
                                    listado.Add(validarArchivo);
                                }
                                else
                                {
                                    throw new Exception("El archivo " + archivo.FileName + " no se pudo validar");
                                }
                            }
                            else
                            {
                                throw new Exception("El archivo " + archivo.FileName + " no se pudo validar");
                            }
                        }
                        else
                        {
                            throw new Exception("El archivo " + archivo.FileName + " está corrupto.");
                        }
                    }

                    if (listado.Count() > 0)
                    {
                        var insertarMasivo = BusClass.InsertarMasivoArchivosAlertasEpidemiologicas(listado, ref MsgRes);
                        if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            throw new Exception(MsgRes.DescriptionResponse);
                        }
                    }
                }
                else
                {

                    throw new Exception("NO EXISTEN ARCHIVOS CARGADOS");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                respuesta = "ERROR: " + error;
            }

            return respuesta;
        }

        public alerta_epidemiologica_gestion_archivos GuardarArchivoPlanMejora(HttpPostedFileBase file, int? idRegistro, int? tipo, int? id_requiere_analisis)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            var respuesta = 0;

            var mensaje = "";

            alerta_epidemiologica_gestion_archivos dato = new alerta_epidemiologica_gestion_archivos();

            try
            {
                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaAlertasEpidemiologica"];

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "ArchivosAlertas";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "ArchivosAlertasPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idRegistro + "_" + tipo);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                dato.id_gestion = idRegistro;
                dato.tipo_Archivo = tipo;
                dato.id_requiere_analisis = id_requiere_analisis;
                dato.ruta = Convert.ToString(archivo);
                dato.extension = file.ContentType;
                dato.nombre_archivo = file.FileName;
                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DEL ARCHIVO: " + error;
            }

            return dato;
        }

        public JsonResult EliminarArchivo(int? id)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var elimina = BusClass.EliminarArchivoEpidemiologico(id);
                if (elimina != 0)
                {
                    mensaje = "ARCHIVO ELIMINADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR ARCHIVO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR ARCHIVO: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult VerArchivoEpidemiologico(int? id)
        {
            try
            {
                alerta_epidemiologica_gestion_archivos dato = new alerta_epidemiologica_gestion_archivos();
                dato = BusClass.TraerArchivoEpidemiologicoId(id);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string extension = obj.extension;
                    string nombre = obj.nombre_archivo;

                    return File(dirpath, extension, nombre);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public PartialViewResult ModuloRequiereAnalisis(int? idGestion, int? id_gestionAnalisis)
        {

            management_alertas_epidemiologicas_tableroControl_idGestionResult datos = new management_alertas_epidemiologicas_tableroControl_idGestionResult();
            alerta_epidemiologica_gestion_gestionAnalisis gestion = new alerta_epidemiologica_gestion_gestionAnalisis();
            List<management_alertas_epidemiologicas_alerta_epidemiologica_gestion_gestionAnalisisDetalladoResult> listaDemoras = new List<management_alertas_epidemiologicas_alerta_epidemiologica_gestion_gestionAnalisisDetalladoResult>();

            try
            {
                datos = BusClass.TraerDatosGestionId(idGestion);

                if (id_gestionAnalisis != null)
                {
                    gestion = BusClass.TraerGestionAEId(id_gestionAnalisis);
                    listaDemoras = BusClass.TraerGestionAEIdDemorasDetallado(id_gestionAnalisis);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idGestion = idGestion;
            ViewBag.listadoAE = BusClass.ListadoAE();
            ViewBag.listadoAEDetallado = BusClass.ListadoAEDetallado();
            ViewBag.tipoBen = BusClass.ListadoTipoBeneficiarios();

            ViewBag.gestionCompleta = datos;
            ViewBag.id_gestionAnalisis = id_gestionAnalisis;

            ViewBag.gestion = gestion;
            ViewBag.listaDemoras = listaDemoras;

            return PartialView();
        }


        [ValidateInput(false)]
        public JsonResult GuardarRequiereAnalisis(Models.Censo.AlertaEpidemiologica Modelo, string gestionDemoras, List<HttpPostedFileBase> archivos, HttpPostedFileBase firma_evaluador, HttpPostedFileBase firma_evaluado)
        {
            var mensaje = "";
            var rta = 0;
            var insertaId = 0;

            try
            {
                alerta_epidemiologica_gestion_gestionAnalisis obj = new alerta_epidemiologica_gestion_gestionAnalisis();
                obj.id_gestion = Modelo.id_gestion;
                obj.fecha_analisis = Modelo.fecha_analisis;
                obj.nombres_beneficiario = Modelo.nombres_beneficiario;
                obj.tipo_beneficiario = Modelo.tipo_beneficiario;
                obj.identificacion_beneficiario = Modelo.identificacion_beneficiario;
                obj.fecha_inicio_servicio = Modelo.fecha_inicio_servicio;
                obj.edad_momento_evento = Modelo.edad_momento_evento;
                obj.tipo_evento = Modelo.tipo_evento;
                obj.fecha_ocurrencia_evento = Modelo.fecha_ocurrencia_evento;
                obj.fecha_fin_evento = Modelo.fecha_fin_evento;
                obj.fuente_reporte = Modelo.fuente_reporte;
                obj.nombre_reportante = Modelo.nombre_reportante;
                obj.nombre_prestador_ocurre_evento = Modelo.nombre_prestador_ocurre_evento;
                obj.nit_prestador = Modelo.nit_prestador;
                obj.profesionales_entidadesresponsables_nivel1 = Modelo.profesionales_entidadesresponsables_nivel1;
                obj.diagnosticos = Modelo.diagnosticos;
                obj.objetivo_auditoria = Modelo.objetivo_auditoria;
                obj.descripcion_evento = Modelo.descripcion_evento;
                obj.ambito_sucedio_evento = Modelo.ambito_sucedio_evento;
                obj.analisis_critico_caso = Modelo.analisis_critico_caso;
                obj.concepto_resolutividad_primer_nivel = Modelo.concepto_resolutividad_primer_nivel;
                obj.aplicacion_guias_terapeuticas = Modelo.aplicacion_guias_terapeuticas;
                obj.periodicidad_controles_medicos = Modelo.periodicidad_controles_medicos;
                obj.aplicacion_pruebas_diagnosticos_monitoreo = Modelo.aplicacion_pruebas_diagnosticos_monitoreo;
                obj.informacion_paciente_cuidadores_plan_terapeutico = Modelo.informacion_paciente_cuidadores_plan_terapeutico;
                obj.descripcion_eventuales_Causasmuerte_relacionadas = Modelo.descripcion_eventuales_Causasmuerte_relacionadas;
                obj.conclusiones_analisis_caso = Modelo.conclusiones_analisis_caso;
                obj.concepto_auditoria_evento_prevenible = Modelo.concepto_auditoria_evento_prevenible;
                obj.propuesta_acciones_mejora = Modelo.propuesta_acciones_mejora;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                //estado 0: guardado parcial
                //estado 1: guardado completo

                obj.estado = 1;


                if (Modelo.id_gestionAnalisis != 0)
                {
                    obj.id_gestionAnalisis = Modelo.id_gestionAnalisis;
                    insertaId = BusClass.ActualizarGestionAnalisisAE(obj);
                }
                else
                {
                    insertaId = BusClass.InsertarGestionRequiereAnlisis(obj);
                }

                if (insertaId != 0)
                {
                    if (Modelo.id_gestionAnalisis != 0)
                    {
                        var eliminaDemoras = BusClass.EliminarDemorasEpidemiologicas(Modelo.id_gestionAnalisis);
                    }

                    List<alerta_epidemiologica_gestion_gestionAnalisis_demoras> demorasDetalle = Modelo.ArreglarListadoDemoras(insertaId, gestionDemoras, SesionVar.UserName, 1);
                    if (demorasDetalle.Count() > 0)
                    {
                        var insertarMasivo = BusClass.InsertrMasivogestionesDemorasAE(demorasDetalle, ref MsgRes);
                        if (insertarMasivo != 0)
                        {
                            mensaje = "DATOS INGRESADOS CORRECTAMENTE";
                            rta = 1;

                            List<alerta_epidemiologica_gestion_archivos> listadoGeneral = new List<alerta_epidemiologica_gestion_archivos>();

                            if (archivos.Count() > 0)
                            {
                                foreach (var item in archivos)
                                {
                                    HttpPostedFileBase archivo = item;
                                    if (archivo.ContentLength > 0)
                                    {
                                        alerta_epidemiologica_gestion_archivos validarArchivo = GuardarArchivoPlanMejora(archivo, Modelo.id_gestion, 3, insertaId);
                                        if (validarArchivo != null)
                                        {
                                            listadoGeneral.Add(validarArchivo);
                                        }
                                        else
                                        {
                                            throw new Exception("El archivo " + archivo.FileName + " no se pudo validar");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("El archivo " + archivo.FileName + " está corrupto.");
                                    }
                                }

                            }

                            if (firma_evaluador != null)
                            {
                                HttpPostedFileBase archivo = firma_evaluador;
                                if (archivo.ContentLength > 0)
                                {
                                    alerta_epidemiologica_gestion_archivos validarArchivo = GuardarArchivoPlanMejora(archivo, Modelo.id_gestion, 4, insertaId);
                                    if (validarArchivo != null)
                                    {
                                        listadoGeneral.Add(validarArchivo);
                                    }
                                    else
                                    {
                                        throw new Exception("El archivo " + archivo.FileName + " no se pudo validar");
                                    }
                                }
                                else
                                {
                                    throw new Exception("El archivo " + archivo.FileName + " está corrupto.");
                                }
                            }

                            if (firma_evaluado != null)
                            {
                                HttpPostedFileBase archivo = firma_evaluador;
                                if (archivo.ContentLength > 0)
                                {
                                    alerta_epidemiologica_gestion_archivos validarArchivo = GuardarArchivoPlanMejora(archivo, Modelo.id_gestion, 5, insertaId);
                                    if (validarArchivo != null)
                                    {
                                        listadoGeneral.Add(validarArchivo);
                                    }
                                    else
                                    {
                                        throw new Exception("El archivo " + archivo.FileName + " no se pudo validar");
                                    }
                                }
                                else
                                {
                                    throw new Exception("El archivo " + archivo.FileName + " está corrupto.");
                                }
                            }


                            if (listadoGeneral.Count() > 0)
                            {
                                var insertarArchivoMasivo = BusClass.InsertarMasivoArchivosAlertasEpidemiologicas(listadoGeneral, ref MsgRes);
                            }
                        }
                        else
                        {
                            mensaje = "ERROR AL INGRESAR LAS DEMORAS";
                        }
                    }
                    else
                    {
                        throw new Exception("ERROR: " + Modelo.problemasGestionDemoras);
                    }
                }
                else
                {
                    throw new Exception("ERROR AL INSERTAR LA GESTIÓN");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = error;
            }

            return Json(new { mensaje = mensaje, rta = rta, insertaId = insertaId });
        }


        [ValidateInput(false)]
        public JsonResult GuardarRequiereAnalisisAutomatico(Models.Censo.AlertaEpidemiologica Modelo, string gestionDemoras, List<HttpPostedFileBase> archivos, HttpPostedFileBase firma_evaluador, HttpPostedFileBase firma_evaluado)
        {
            // tipo_ingreso 1: Guardado completo
            // tipo_ingreso 2: guardado parcial

            var mensaje = "";
            var rta = 0;
            var insertaId = 0;
            DateTime fechaCondicion = new DateTime(2000, 01, 01);
            try
            {
                alerta_epidemiologica_gestion_gestionAnalisis obj = new alerta_epidemiologica_gestion_gestionAnalisis();
                obj.id_gestion = Modelo.id_gestion;
                obj.fecha_analisis = Modelo.fecha_analisis;
                obj.nombres_beneficiario = Modelo.nombres_beneficiario;
                obj.tipo_beneficiario = Modelo.tipo_beneficiario;
                obj.identificacion_beneficiario = Modelo.identificacion_beneficiario;
                obj.edad_momento_evento = Modelo.edad_momento_evento;
                obj.tipo_evento = Modelo.tipo_evento;

                if (Modelo.fecha_inicio_servicio > fechaCondicion)
                {
                    obj.fecha_inicio_servicio = Modelo.fecha_inicio_servicio;
                }

                if (Modelo.fecha_fin_evento > fechaCondicion)
                {
                    obj.fecha_fin_evento = Modelo.fecha_fin_evento;
                }

                if (Modelo.fecha_ocurrencia_evento > fechaCondicion)
                {
                    obj.fecha_ocurrencia_evento = Modelo.fecha_ocurrencia_evento;
                }

                obj.fuente_reporte = Modelo.fuente_reporte;
                obj.nombre_reportante = Modelo.nombre_reportante;
                obj.nombre_prestador_ocurre_evento = Modelo.nombre_prestador_ocurre_evento;
                obj.nit_prestador = Modelo.nit_prestador;
                obj.profesionales_entidadesresponsables_nivel1 = Modelo.profesionales_entidadesresponsables_nivel1;
                obj.diagnosticos = Modelo.diagnosticos;
                obj.objetivo_auditoria = Modelo.objetivo_auditoria;
                obj.descripcion_evento = Modelo.descripcion_evento;
                obj.ambito_sucedio_evento = Modelo.ambito_sucedio_evento;
                obj.analisis_critico_caso = Modelo.analisis_critico_caso;
                obj.concepto_resolutividad_primer_nivel = Modelo.concepto_resolutividad_primer_nivel;
                obj.aplicacion_guias_terapeuticas = Modelo.aplicacion_guias_terapeuticas;
                obj.periodicidad_controles_medicos = Modelo.periodicidad_controles_medicos;
                obj.aplicacion_pruebas_diagnosticos_monitoreo = Modelo.aplicacion_pruebas_diagnosticos_monitoreo;
                obj.informacion_paciente_cuidadores_plan_terapeutico = Modelo.informacion_paciente_cuidadores_plan_terapeutico;
                obj.descripcion_eventuales_Causasmuerte_relacionadas = Modelo.descripcion_eventuales_Causasmuerte_relacionadas;
                obj.conclusiones_analisis_caso = Modelo.conclusiones_analisis_caso;
                obj.concepto_auditoria_evento_prevenible = Modelo.concepto_auditoria_evento_prevenible;
                obj.propuesta_acciones_mejora = Modelo.propuesta_acciones_mejora;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                //estado 0: guardado parcial
                //estado 1: guardado completo

                obj.estado = 0;

                if (Modelo.id_gestionAnalisis != 0)
                {
                    obj.id_gestionAnalisis = Modelo.id_gestionAnalisis;
                    insertaId = BusClass.ActualizarGestionAnalisisAE(obj);
                }
                else
                {
                    insertaId = BusClass.InsertarGestionRequiereAnlisis(obj);
                }

                if (insertaId != 0)
                {
                    if (Modelo.id_gestionAnalisis != 0)
                    {
                        var eliminaDemoras = BusClass.EliminarDemorasEpidemiologicas(Modelo.id_gestionAnalisis);
                    }

                    List<alerta_epidemiologica_gestion_gestionAnalisis_demoras> demorasDetalle = Modelo.ArreglarListadoDemoras(insertaId, gestionDemoras, SesionVar.UserName, 2);
                    if (demorasDetalle.Count() > 0)
                    {
                        var insertarMasivo = BusClass.InsertrMasivogestionesDemorasAE(demorasDetalle, ref MsgRes);
                        if (insertarMasivo != 0)
                        {
                            mensaje = "DATOS INGRESADOS CORRECTAMENTE";
                            rta = 1;

                            List<alerta_epidemiologica_gestion_archivos> listadoGeneral = new List<alerta_epidemiologica_gestion_archivos>();

                            if (archivos.Count() > 0)
                            {

                                foreach (var item in archivos)
                                {
                                    HttpPostedFileBase archivo = item;
                                    if (archivo.ContentLength > 0)
                                    {
                                        alerta_epidemiologica_gestion_archivos validarArchivo = GuardarArchivoPlanMejora(archivo, Modelo.id_gestion, 3, insertaId);
                                        if (validarArchivo != null)
                                        {
                                            listadoGeneral.Add(validarArchivo);
                                        }
                                        else
                                        {
                                            throw new Exception("El archivo " + archivo.FileName + " no se pudo validar");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("El archivo " + archivo.FileName + " está corrupto.");
                                    }
                                }

                            }

                            if (firma_evaluador != null)
                            {
                                HttpPostedFileBase archivo = firma_evaluador;
                                if (archivo.ContentLength > 0)
                                {
                                    alerta_epidemiologica_gestion_archivos validarArchivo = GuardarArchivoPlanMejora(archivo, Modelo.id_gestion, 4, insertaId);
                                    if (validarArchivo != null)
                                    {
                                        listadoGeneral.Add(validarArchivo);
                                    }
                                    else
                                    {
                                        throw new Exception("El archivo " + archivo.FileName + " no se pudo validar");
                                    }
                                }
                                else
                                {
                                    throw new Exception("El archivo " + archivo.FileName + " está corrupto.");
                                }
                            }

                            if (firma_evaluado != null)
                            {
                                HttpPostedFileBase archivo = firma_evaluador;
                                if (archivo.ContentLength > 0)
                                {
                                    alerta_epidemiologica_gestion_archivos validarArchivo = GuardarArchivoPlanMejora(archivo, Modelo.id_gestion, 5, insertaId);
                                    if (validarArchivo != null)
                                    {
                                        listadoGeneral.Add(validarArchivo);
                                    }
                                    else
                                    {
                                        throw new Exception("El archivo " + archivo.FileName + " no se pudo validar");
                                    }
                                }
                                else
                                {
                                    throw new Exception("El archivo " + archivo.FileName + " está corrupto.");
                                }
                            }


                            if (listadoGeneral.Count() > 0)
                            {
                                var insertarArchivoMasivo = BusClass.InsertarMasivoArchivosAlertasEpidemiologicas(listadoGeneral, ref MsgRes);
                            }
                        }
                        else
                        {
                            mensaje = "ERROR AL INGRESAR LAS DEMORAS";
                        }
                    }
                    else
                    {
                        throw new Exception("ERROR: " + Modelo.problemasGestionDemoras);
                    }
                }
                else
                {
                    throw new Exception("ERROR AL INSERTAR LA GESTIÓN");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = error;
            }

            return Json(new { mensaje = mensaje, rta = rta, insertaId = insertaId });
        }

        public void PdfReporteAnalisis(int? idGestion)
        {
            //recibe el id del requiere analisis

            try
            {
                List<management_alertaEpidemiologica_reporteIdResult> datos = new List<management_alertaEpidemiologica_reporteIdResult>();
                List<management_alertaEpidemiologica_reporteId_demorasResult> datos_Demoras = new List<management_alertaEpidemiologica_reporteId_demorasResult>();

                datos = BusClass.TraerGestionAnalisis(idGestion);
                datos_Demoras = BusClass.TraerGestionAnalisis_demoras(idGestion);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptCensoRequiereAnalisis.rdlc");
                Microsoft.Reporting.WebForms.ReportDataSource RtaDatos = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetCensoRequiereAnalisis", datos);
                Microsoft.Reporting.WebForms.ReportDataSource RtaDatos_demoras = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetCensoRequiereAnalisis_demoras", datos_Demoras);

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                //viewer.LocalReport.EnableExternalImages = true;

                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(RtaDatos);
                viewer.LocalReport.DataSources.Add(RtaDatos_demoras);

                if (datos != null)
                {
                    //CONTROL EXCEPCION
                    try
                    {
                        viewer.LocalReport.Refresh();
                        //EXPORTAR PDF

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

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
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public JsonResult CerrarAlertaEpi(int? idRegistro)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                management_alertas_epidemiologicas_gestionesResult ges = BusClass.TraerGestionAlertasEpidemiologicas(idRegistro);
                if (ges.requiere_analisis == "SI")
                {
                    if (ges.id_gestion == null)
                    {
                        throw new Exception("No se han gestionado los datos de requiere analisis");
                    }
                }

                if (ges.requiere_cargue_soportes == "SI")
                {
                    if (ges.existeSoporte == "NO")
                    {
                        throw new Exception("No se han cargado soportes");
                    }
                }

                if (ges.requiere_verificacion_sivigilia == "SI")
                {
                    if (ges.existeVerificacionSIVIGILIA == "NO")
                    {
                        throw new Exception("No se han cargado soportes SIVIGILIA");
                    }
                }

                var actualiza = BusClass.CerrarAlertaEpidemiologica(idRegistro, SesionVar.UserName);
                if (actualiza != 0)
                {
                    mensaje = "ALERTA CERRADA CORRECTAMEMTE";
                    rta = 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });


        }

        public ActionResult TableroDescargueArchivos()
        {
            ViewBag.regional = BusClass.GetRefRegion();
            return View();
        }

        public ActionResult DescargarArchivosConsolidadoAlertas(int? regional, int? localidad, int? unis, DateTime? fechaAlertaIni, DateTime? fechaAlertaFin,
            string documentoPaciente, int? idConcurrencia, DateTime? fechaIngresoIni, DateTime? fechaIngresoFin, int? conEgreso)
        {
            try
            {
                // Obtener el listado de tipos
                var lista = BusClass.TraerDocumentosArchivosGestionAE(regional, localidad, unis, fechaAlertaIni, fechaAlertaFin, documentoPaciente, idConcurrencia, fechaIngresoIni, fechaIngresoFin, conEgreso);
                if (lista == null || lista.Count() == 0)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "No se encontraron documentos.." });
                }

                // Generar el archivo ZIP en streaming
                Response.Clear();
                Response.BufferOutput = false; // Desactiva el buffer para transmitir en tiempo real
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", $"attachment; filename=ConsolidadoArchivosAE_{DateTime.Now}.zip");

                using (var zip = new Ionic.Zip.ZipFile())
                {
                    foreach (var item in lista)
                    {
                        // Obtener archivos asociados al documento
                        var archivos = BusClass.TraerArchivosGestionAE(item.identificacion);

                        if (archivos.Count > 0)
                        {
                            var rutaCarpeta = item.tipo_identificacion + "_" + item.identificacion + "_" + item.id_concurrencia;

                            foreach (var archivo in archivos)
                            {
                                string dirPath = archivo.ruta1;

                                // Verificar si el archivo existe antes de agregarlo
                                if (System.IO.File.Exists(dirPath))
                                {
                                    try
                                    {
                                        var direccionCompleta = "";

                                        if (archivo.tipo_Archivo == 1)
                                        {
                                            direccionCompleta = $"{rutaCarpeta}/ 02. SOPORTES DE GESTION AUDITORIA_{archivo.id_registro}.pdf";
                                        }
                                        else if (archivo.tipo_Archivo == 2)
                                        {
                                            direccionCompleta = $"{rutaCarpeta}/ 03. SOPORTES SIVIGILA_{archivo.id_registro}.pdf";
                                        }
                                        else if (archivo.tipo_Archivo == 3)
                                        {
                                            direccionCompleta = $"{rutaCarpeta}/ 01. ANALISIS DE CASO_{archivo.id_registro}.pdf";
                                        }
                                        else if (archivo.tipo_Archivo == 4)
                                        {
                                            direccionCompleta = $"{rutaCarpeta}/ 04. ANALISIS DE CASO FIRMA EVALUADOR_{archivo.id_registro}.pdf";
                                        }
                                        else
                                        {
                                            direccionCompleta = $"{rutaCarpeta}/ 05. ANALISIS DE CASO FIRMA EVALUADO_{archivo.id_registro}.pdf";
                                        }

                                        zip.AddFile(dirPath).FileName = direccionCompleta;
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception($"Error al agregar archivo: {ex.Message}");
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