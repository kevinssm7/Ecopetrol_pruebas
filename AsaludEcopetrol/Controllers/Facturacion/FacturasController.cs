using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using AsaludEcopetrol.BussinesManager;
using System.IO;

using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using Microsoft.Reporting.WebForms;

namespace AsaludEcopetrol.Controllers.Facturacion
{
    [SessionExpireFilter]
    public class FacturasController : Controller
    {

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


        // GET: Facturas
        //public ActionResult FacturaDev(Int32? ID)
        //{
        //    Models.Facturacion.FacturaDevolucion Model = new Models.Facturacion.FacturaDevolucion();

        //    Model.ModuloPrestadores = "NO";
        //    Model.id_af = 0;

        //        //List<managmentprestadoresFacturasResult> result = Model.GetFacturasByEstadoAceptadas(5, ref MsgRes);
        //    try
        //    {
        //        List<managmentprestadoresFacturasResult> result = Model.GetFacturasByEstadoAceptadas(5, ref MsgRes);
        //        result = result.Where(x => x.id_cargue_dtll == ID).ToList();

        //        foreach (var item in result)
        //        {
        //            Model.id_regional = item.id_ref_regional;
        //            Model.ciudad = item.id_ref_ciudades;
        //            Model.Prestador = item.prestador.Value;
        //            Model.NumeroFactura = item.num_factura;
        //            Model.ValorFactura = Convert.ToString(item.valor_neto);
        //            Model.ModuloPrestadores = "SI";
        //            Model.id_af = item.id_cargue_dtll;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return View(Model);
        //}



        // GET: Facturas
        public ActionResult FacturaDev(Int32? ID)
        {
            Models.Facturacion.FacturaDevolucion Model = new Models.Facturacion.FacturaDevolucion();

            Model.ModuloPrestadores = "NO";
            Model.id_af = 0;

            //List<managmentprestadoresFacturasResult> result = Model.GetFacturasByEstadoAceptadas(5, ref MsgRes);
            List<managmentprestadoresFacturas_devueltasResult> result = new List<managmentprestadoresFacturas_devueltasResult>();

            try
            {
                result = Model.GetFacturasByEstadoDevueltas(5, ID, ref MsgRes);

                if (result != null)
                {
                    if (result.Count() > 0)
                    {
                        foreach (var item in result)
                        {
                            Model.id_regional = item.id_ref_regional;
                            Model.ciudad = item.id_ref_ciudades;
                            Model.Prestador = item.prestador.Value;
                            Model.NumeroFactura = item.num_factura;
                            Model.ValorFactura = Convert.ToString(item.valor_neto);
                            Model.ModuloPrestadores = "SI";
                            Model.id_af = item.id_cargue_dtll;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }


        public ActionResult FacturasinCenso()
        {
            Models.Facturacion.FacturasinCenso Model = new Models.Facturacion.FacturasinCenso();
            return View(Model);
        }

        public ActionResult BuscarFacturas()
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();
            return View(Model);
        }

        public ActionResult GestionFacturasinCenso(String ID)
        {
            Models.Facturacion.FacturasinCenso Model = new Models.Facturacion.FacturasinCenso();

            Model.id_factura_sin_censo = Convert.ToInt32(ID);

            Model.ConsultaFactura(Model.id_factura_sin_censo);

            return View(Model);
        }

        public ActionResult HallazgosRips()
        {
            Models.Facturacion.hallazgosRips Model = new Models.Facturacion.hallazgosRips();
            return View(Model);
        }

        public ActionResult Costoevitado(String id)
        {
            Models.Facturacion.FacturasinCenso Model = new Models.Facturacion.FacturasinCenso();
            Model.id_factura_sin_censo = Convert.ToInt32(id);
            ViewBag.motivo = BusClass.GetCuentaGlosa();
            return View(Model);
        }

        public ActionResult diagnosticosFactura(String id)
        {
            Models.Facturacion.FacturasinCenso Model = new Models.Facturacion.FacturasinCenso();

            Model.id_factura_sin_censo = Convert.ToInt32(id);

            return View(Model);
        }

        public ActionResult GenerarReportesFacturas()
        {
            Models.Facturacion.FacturasinCenso Model = new Models.Facturacion.FacturasinCenso();
            return View(Model);
        }

        [HttpPost]
        public ActionResult FacturaDev(Models.Facturacion.FacturaDevolucion Model, string operacion = null)
        {

            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            List<managmentprestadoresNumeroFacturaResult> list = new List<managmentprestadoresNumeroFacturaResult>();

            list = Model.GetConsultaNumeroFactura(Model.NumeroFactura);
            list = list.Where(x => x.prestador == Model.Prestador).ToList();

            if (list.Count != 0)
            {
                variable2 = "Factura ya tiene un proceso en el módulo e prestadores por favor realizar la devolución desde el tablero de recepción digital.";
                Conteo = Conteo + 1;
            }
            else
            {

            }

            if (Model.fecha_devolucionOK != null)
            {

            }
            else
            {
                variable2 = "FECHA DE DEVOLUCION";
                Conteo = Conteo + 1;
            }

            if (Model.Detalle == null || Model.Detalle.Count == 0)
            {
                ModelState.AddModelError("Detalle", "Debe agregar al menos un detalle para la factura");
            }
            else
            {
                if (Model.ciudad == 0)
                {
                    ModelState.AddModelError("", "Error.. debe ingresar una regional y una ciudad");
                }
                else
                {
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

                        Model.OBJFactura.Prestador = Model.Prestador;
                        Model.OBJFactura.fecha_devolucion = Model.fecha_devolucionOK;
                        Model.OBJFactura.id_regional = Model.id_regional;
                        Model.OBJFactura.NumeroFactura = Model.NumeroFactura;
                        Model.OBJFactura.tipo = Model.tipo;
                        Model.OBJFactura.valor_factura = Convert.ToString(Model.ValorFactura);
                        Model.OBJFactura.ciudad = Model.ciudad;
                        Model.OBJFactura.fecha_digita = Convert.ToDateTime(DateTime.Now);
                        Model.OBJFactura.usuario_digita = Convert.ToString(SesionVar.UserName);
                        Model.OBJFactura.ModuloPrestadores = Model.ModuloPrestadores;
                        Model.OBJFactura.id_af = Model.id_af;

                        Model.ConsultaDevolucionesFactura(Model.NumeroFactura);

                        Int32 sumaGlosa = 0;
                        foreach (var item in Model.Detalle)
                        {
                            sumaGlosa = sumaGlosa + Convert.ToInt32(item.ValorGlosa);
                        }
                        String Glosa = (Model.ValorFactura).Replace(".", ""); ;

                        if (sumaGlosa > Convert.ToInt32(Glosa))
                        {
                            ModelState.AddModelError("", "!! ERROR....LA SUMA DE LAS GLOSAS ES SUPERIOR AL VALOR DE LA FACTURA");
                        }
                        else
                        {
                            Int32 condicion = 0;

                            if (Model.ListFacturaId.Count != 0)
                            {
                                foreach (var item in Model.ListFacturaId)
                                {
                                    if (Model.Prestador == item.Prestador)
                                    {
                                        condicion = 1;
                                    }
                                }
                            }

                            if (condicion == 0)
                            {
                                Model.OBJFactura.id_devolucion_factura = Model.InsertarDevolucionFacturas(ref MsgRes);
                                String ID = Convert.ToString(Model.OBJFactura.id_devolucion_factura);
                                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                                {
                                    foreach (var item in Model.Detalle)
                                    {
                                        Model.OBJFacturaDetalle.id_devolucion_factura = Model.OBJFactura.id_devolucion_factura;
                                        Model.OBJFacturaDetalle.ValorGlosa = item.ValorGlosa;
                                        Model.OBJFacturaDetalle.id_motivo = Convert.ToInt32(item.Id_motivo);
                                        Model.OBJFacturaDetalle.descripcion_devolucion = "NA";
                                        Model.OBJFacturaDetalle.Observaciones = item.Observaciones;

                                        Model.InsertarDevolucionFacturasDetalle(ref MsgRes);
                                    }
                                    CrearPdfCartaDevolucionFac("CartaDevolucionFactura" + ID, ID);
                                }
                                else
                                {
                                    ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "!! ERROR...ESTA FACTURA Y ESTE PRESTADOR YA TIENEN UN INGRESO.");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
                    }
                }
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult FacturasinCenso(Models.Facturacion.FacturasinCenso Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_facturaOK != null)
            {

            }
            else
            {
                variable2 = "FECHA DE FACTURA";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_recepcionOK != null)
            {

            }
            else
            {
                variable2 = "FECHA DE RECEPCION";
                Conteo = Conteo + 1;
            }

            if (Model.Prestador != null)
            {

            }
            else
            {
                variable2 = "IPS";
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
                Model.ObjFac.numero_factura = Model.numero_factura;
                Model.ObjFac.fecha_factura = Model.fecha_facturaOK;
                Model.ObjFac.fecha_recepcion = Model.fecha_recepcionOK;
                Model.ObjFac.ips = Model.Prestador;
                Model.ObjFac.ciudad_ips = Model.ciudad;
                Model.ObjFac.valor_factura = Model.valor_factura;
                Model.ObjFac.tipo_factura = Model.tipo_factura;
                Model.ObjFac.tipo = Model.tipo;
                Model.ObjFac.num_documento_no_multiusuario = Model.numdocumento_multiusuario;
                Model.ObjFac.usuario_digita = SesionVar.UserName;
                Model.ObjFac.digita_fecha = Convert.ToDateTime(DateTime.Now);
                Model.ConsultaFacturaNumero(Model.numero_factura);

                Int32 condicion = 0;

                if (Model.LstFacturasCensoId.Count != 0)
                {
                    foreach (var item in Model.LstFacturasCensoId)
                    {
                        if (Model.Prestador == item.ips)
                        {
                            condicion = 1;
                        }
                    }
                }

                if (condicion == 0)
                {
                    Model.InsertarFacturaSinCenso(ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        return RedirectToAction("Inicio", "Usuario");
                    }
                    else
                    {
                        ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
                    }

                }
                else
                {
                    ModelState.AddModelError("", "!! ERROR...ESTA FACTURA Y ESTE PRESTADOR YA TIENEN UN INGRESO.");
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult GestionFacturasinCenso(Models.Facturacion.FacturasinCenso Model)
        {
            factura_sin_censo OBJ = new factura_sin_censo();

            OBJ.id_factura_sin_censo = Model.id_factura_sin_censo;
            if (Model.tipo_factura == "PREFACTURA")
            {
                OBJ.valor_factura_definitiva = Model.valor_factura_definitiva;
            }
            else
            {

            }
            OBJ.auditada = "SI";

            Model.ActualizaFacturaAuditada(OBJ, ref MsgRes);
            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return RedirectToAction("BuscarFacturas", "Facturas");
            }
            else
            {
                ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult HallazgosRips(Models.Facturacion.hallazgosRips Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_facturaOK != null)
            {

            }
            else
            {
                variable2 = "FECHA FACTURA";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_recepcionOK != null)
            {

            }
            else
            {
                variable2 = "FECHA RECEPCION";
                Conteo = Conteo + 1;
            }

            if (Model.Prestador != 0)
            {

            }
            else
            {
                variable2 = "PRESTADOR";
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

                Model.OBJHallazgo.numero_factura = Model.numero_factura;
                Model.OBJHallazgo.fecha_reporte_hallazgo = Model.fecha_facturaOK;
                Model.OBJHallazgo.fecha_recepcion = Model.fecha_recepcionOK;
                Model.OBJHallazgo.proveedor = Model.Prestador;
                Model.OBJHallazgo.fecha_digita = Convert.ToDateTime(DateTime.Now);
                Model.OBJHallazgo.usuario_digita = Convert.ToString(SesionVar.UserName);


                Model.OBJHallazgo.id_hallazgo_RIPS = Model.InsertarHallazgos(ref MsgRes);
                String ID = Convert.ToString(Model.OBJHallazgo.id_hallazgo_RIPS);
                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    foreach (var item in Model.Detalle)
                    {
                        Model.OBJHallazgoDetalle.id_hallazgo_RIPS = Model.OBJHallazgo.id_hallazgo_RIPS;
                        Model.OBJHallazgoDetalle.id_hallazgo = item.Id_hallazgo;
                        Model.OBJHallazgoDetalle.descripcion_hallazgo = item.Descripcion_hallazgo;

                        Model.InsertarHallazgosDetalle(ref MsgRes);

                    }
                    CrearPdfCartaHallazgosRips("CartaHallazgosRIPS" + ID, ID);
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

        [HttpPost]
        public ActionResult Costoevitado(Models.Facturacion.FacturasinCenso Model)
        {
            ViewBag.motivo = BusClass.GetCuentaGlosa();
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.id_cups != null)
            {

            }
            else
            {
                variable2 = "CUPS";
                Conteo = Conteo + 1;
            }

            if (Model.motivo_glosa == 0)
            {
                variable2 = "MOTIVO GLOSA";
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

                Model.Obj_costoEvitado.id_factura_sin_censo = Model.id_factura_sin_censo;
                Model.Obj_costoEvitado.id_cups = Model.id_cups;
                Model.Obj_costoEvitado.cantidad_glosa = Model.cantidad_glosa;
                Model.Obj_costoEvitado.valor_glosa = Model.valor_glosa;
                Model.Obj_costoEvitado.valor_total = Model.valor_total;
                Model.Obj_costoEvitado.observacion = Model.observacion;
                Model.Obj_costoEvitado.responsable_glosa = Model.responsable_glosa;
                Model.Obj_costoEvitado.motivo_glosa = Model.motivo_glosa;
                Model.Obj_costoEvitado.usuario_digita = Convert.ToString(SesionVar.UserName);
                Model.Obj_costoEvitado.digita_fecha = Convert.ToDateTime(DateTime.Now);

                Model.InsertarCostoEvitado(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {

                    return RedirectToAction("Costoevitado", "Facturas", new { id = Model.id_factura_sin_censo });
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

        [HttpPost]
        public ActionResult diagnosticosFactura(Models.Facturacion.FacturasinCenso Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.cie10 != null)
            {

            }
            else
            {
                variable2 = "DIAGNOSTICO";
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

                Model.Obj_diagnosticos.id_factura_sin_censo = Model.id_factura_sin_censo;
                Model.Obj_diagnosticos.cie10 = Model.cie10;
                Model.Obj_diagnosticos.usuario_digita = Convert.ToString(SesionVar.UserName);
                Model.Obj_diagnosticos.fecha_digita = Convert.ToDateTime(DateTime.Now);

                Model.InsertarDiagnosticoCuentas(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {


                    return RedirectToAction("diagnosticosFactura", "Facturas", new { id = Model.id_factura_sin_censo });
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

        [HttpPost]
        public ActionResult GenerarReportesFacturas(Models.Facturacion.FacturasinCenso Model)
        {
            if (Model.id_tipo_busqueda == "1")
            {
                CrearPdfCartaDevolucionFac("CartaDevolucionFactura" + Model.id_devolucion, Convert.ToString(Model.id_devolucion));
            }
            else if (Model.id_tipo_busqueda == "2")
            {
                CrearPdfCartaHallazgosRips("CartaHallazgosRIPS" + Model.id_rips, Convert.ToString(Model.id_rips));
            }
            return View(Model);
        }

        public JsonResult GetCascadeRegional(Models.Facturacion.FacturaDevolucion Model)
        {
            return Json(Model.RefRegional.Select(c => new { id_ref_regional = c.id_ref_regional, nombre_regional = c.nombre_regional }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeCiudades(string regional, Models.Facturacion.FacturaDevolucion Model)
        {
            if (regional != null)
            {
                Model.ConsultaLista(1, regional, ref MsgRes);
            }
            return Json(Model.ListCiudades.Select(p => new { id_ref_ciudades = p.id_ref_ciudades, nombre = p.nombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeIPS(string Ciudad, Models.Facturacion.FacturaDevolucion Model)
        {
            if (Ciudad != null)
            {
                Model.ConsultaListaIps(1, Ciudad, ref MsgRes);
            }
            return Json(Model.ListIPS2.Select(p => new { id_ref_ips_cuentas = p.id_ref_ips_cuentas, Nombre = p.Nombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIps(Models.Facturacion.FacturaDevolucion Model)
        {
            return Json(Model.ListIPS.ToList(), JsonRequestBehavior.AllowGet);
        }

        public void CrearPdfCartaDevolucionFac(string fileName, string id)
        {

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptDevolucionFac.rdlc");
            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<ManagmentReportDevolucionFacResult> lst = new List<ManagmentReportDevolucionFacResult>();
            lst = Model.ConsultaReporteDevolucionFac(Convert.ToInt32(id));

            var usuarioDig = lst.FirstOrDefault().userDigita;

            var idUsuario = BusClass.datosUsuarioUser(usuarioDig).id_usuario;
            
            var BaseImagen = Model.GetFirmas(idUsuario);

            string Imagen = "";

            if (BaseImagen != null)
            {
                Imagen = Convert.ToBase64String(BaseImagen.firma_digital.ToArray());
            }


            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetDevolucionFac", lst);
            Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);


            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);
            viewer.LocalReport.SetParameters(imagen);


            if (lst.Count != 0)
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
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    this.Response.Clear();
                    this.Response.ContentType = "application/pdf";
                    this.Response.AddHeader("Content-disposition", "attachment; filename=" + fileName + ".pdf");
                    this.Response.BinaryWrite(pdfContent);
                    this.Response.End();


                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

        }

        private void CrearPdfCartaHallazgosRips(string fileName, string id)
        {

            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptHallazgosRIPS.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<ManagmentReportHallazgosRipResult> lst = new List<ManagmentReportHallazgosRipResult>();

            lst = Model.ConsultaReporteHallazgosRips(Convert.ToInt32(id));
            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("HallazgosDataSet", lst);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    //string mimeType;
                    //string encoding;
                    //string fileNameExtension;
                    //string[] streams;
                    //Microsoft.Reporting.WebForms.Warning[] warnings;
                    //byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    ////RETORNO PDF

                    //this.Response.Clear();
                    //this.Response.ContentType = "application/pdf";
                    //this.Response.AddHeader("Content-disposition", "attachment; filename=" + fileName + ".pdf");
                    //this.Response.BinaryWrite(pdfContent);
                    //this.Response.End();

                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;


                    byte[] bytes1 = viewer.LocalReport.Render("WORD", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + "singleReport" + "." + extension);
                    Response.BinaryWrite(bytes1);
                    Response.Flush();
                    Response.End();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

        }


    }

}