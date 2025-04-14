using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Adherencia
{

    [SessionExpireFilter]
    public class AdherenciaController : Controller
    {
        readonly Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

        #region PROPIEDADES

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

        /// <summary>
        /// Vista configuracion de cohortes y criterios
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public ActionResult ConfiguracionAdherencia(int? tab)
        {
            ViewBag.reftipocriterio = Model.get_ref_TipoCriterio();
            ViewBag.reftipocohorte = BusClass.Get_refCohortes();
            ViewBag.tab = tab;
            return View();
        }

        /// <summary>
        /// Vista para admistrar los criterios de un tipo de adherencia
        /// </summary>
        /// <param name="idtipoadherencia"></param>
        /// <returns></returns>
        public ActionResult AdminCriterios(int idtipoadherencia)
        {
            ViewBag.reftipocriterio = Model.get_ref_TipoCriterio();
            ViewBag.tipoadherencia = idtipoadherencia;
            ViewBag.adh_tipoadherencia = Model.get_adh_tipocriterio(idtipoadherencia);
            ViewBag.refadhGrupoTipoAdherencia = Model.get_ref_adh_grupotipocriterio();
            return View();
        }

        /// <summary>
        /// Metodo post para guardar administracion de criterios
        /// </summary>
        /// <param name="tipoadherencia"></param>
        /// <param name="seleccionados"></param>
        /// <returns></returns>
        public JsonResult GuardarAdmincriterios(int tipoadherencia, List<int> seleccionados, List<int> seleccionados2)
        {
            Model.InsertarAdminCriterios(tipoadherencia, seleccionados, seleccionados2, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { rta = 1, msj = "Guardado Exitosamente" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { rta = 2, msj = "Error: " + MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Vista general tipos de criterio
        /// </summary>
        /// <param name="idtipocohorte"></param>
        /// <returns></returns>
        public ActionResult AdherenciatipoCriterio(int idtipocohorte)
        {
            List<adh_tipocriterio> lista = Model.get_adh_tipocriterio(idtipocohorte);
            ViewBag.adh_tipoadherencia = lista;
            ViewBag.pesogeneral = lista.Select(l => l.puntaje).Sum();
            ViewBag.idtipocohorte = idtipocohorte;
            ViewBag.nomcohorte = BusClass.getTipoCohorteById(idtipocohorte).descripcion;
            ViewBag.criterios = Model.getcriteriosbytipocohorte(idtipocohorte);
            return View();
        }

        /// <summary>
        /// Metodo post para guardar la configuracion de un criterio
        /// </summary>
        /// <param name="idtipocohorte"></param>
        /// <param name="idtipocriterio"></param>
        /// <param name="peso"></param>
        /// <returns></returns>
        public JsonResult GuardarCofiguracioncriterio(int idtipocohorte, int idtipocriterio, string peso)
        {
            List<adh_tipocriterio> lista = Model.get_adh_tipocriterio(idtipocohorte);
            //double? pesogeneral = lista.Where(l => l.id_tipo_criterio != idtipocriterio).Select(l => l.puntaje).Sum() + Convert.ToDouble(peso);


            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                adh_tipocriterio obj = db.adh_tipocriterio.Where(l => l.id_tipo_cohorte == idtipocohorte && l.id_tipo_criterio == idtipocriterio).FirstOrDefault();
                obj.puntaje = Convert.ToDouble(peso);
                db.SubmitChanges();
            }
            lista = Model.get_adh_tipocriterio(idtipocohorte);
            double? puntajetotal = lista.Select(l => l.puntaje).Sum();
            return Json(new { rta = 1, puntajetotal = puntajetotal }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Vista para configurar los criterios de una cohorte
        /// </summary>
        /// <param name="idtipocohorte"></param>
        /// <returns></returns>
        public ActionResult CriteriosCohorte(int idtipocohorte, int tipocriterio)
        {

            var obj = BusClass.get_adh_tipocriterio(idtipocohorte).Where(l => l.id_tipo_criterio == tipocriterio).FirstOrDefault();
            ViewBag.nomcohorte = obj.ref_cohortes.descripcion;
            ViewBag.tipocriterio = obj.ref_adh_tipo_criterio.nom_tipo_criterion;
            ViewBag.pesotipocriterio = obj.puntaje;
            ViewBag.idtipocriterio = tipocriterio;
            ViewBag.tipocohorte = idtipocohorte;
            ViewBag.respuesta = 0;
            ViewData["mensajeRespuesta"] = "";

            var modelo = Model.getcriteriosbytipocohorte(idtipocohorte).Where(l => l.id_tipo_criterio == tipocriterio).ToList();
            ViewBag.totalpeso = modelo.Select(l => l.puntaje).Sum();
            return View(modelo);
        }

        /// <summary>
        /// Metodo post para guardar y editar los criterios de una cohorte
        /// </summary>
        /// <param name="idcriterio"></param>
        /// <param name="idtipocohorte"></param>
        /// <param name="tipocriterio"></param>
        /// <param name="nomcriterio"></param>
        /// <param name="puntaje"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CriteriosCohorte(int idcriterio, int idtipocohorte, int tipocriterio, string nomcriterio, string atributo)
        {
            adh_criterio criterio = null;
            if (idcriterio == 0)
            {
                criterio = new adh_criterio();
            }
            else
            {
                criterio = Model.ConsultarCriterioById(idcriterio);
            }

            criterio.id_tipo_cohorte = idtipocohorte;
            criterio.id_tipo_criterio = tipocriterio;
            criterio.descripcion = nomcriterio;
            criterio.puntaje = 0;
            criterio.atributo = atributo;

            if (idcriterio == 0)
            {
                Model.InsertarCriterio(criterio, ref MsgRes);
            }
            else
            {
                Model.ActualizarCriterio(criterio, ref MsgRes);
            }


            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                ViewBag.respuesta = 1;
                ViewData["mensajeRespuesta"] = "Guardado Exitosamente";
            }
            else
            {
                ViewBag.respuesta = 2;
                ViewData["mensajeRespuesta"] = "Ha ocurrido un error al momento de guardar la información: " + MsgRes.DescriptionResponse;
            }

            var obj = BusClass.get_adh_tipocriterio(idtipocohorte).Where(l => l.id_tipo_criterio == tipocriterio).FirstOrDefault();
            ViewBag.nomcohorte = obj.ref_cohortes.descripcion;
            ViewBag.tipocriterio = obj.ref_adh_tipo_criterio.nom_tipo_criterion;
            ViewBag.pesotipocriterio = obj.puntaje;
            ViewBag.idtipocriterio = tipocriterio;
            ViewBag.tipocohorte = idtipocohorte;

            var modelo = Model.getcriteriosbytipocohorte(idtipocohorte).Where(l => l.id_tipo_criterio == tipocriterio).ToList();
            ViewBag.totalpeso = modelo.Select(l => l.puntaje).Sum();
            return View(modelo);
        }

        /// <summary>
        /// Obtener los datos de un criterio
        /// </summary>
        /// <param name="idcriterio"></param>
        /// <returns></returns>
        public JsonResult GetInfoCriterio(int idcriterio)
        {
            adh_criterio criterio = Model.ConsultarCriterioById(idcriterio);
            var data = new
            {
                atributo = criterio.atributo,
                idcriterio = criterio.id_adh_criterio,
                descripcion = criterio.descripcion,
                puntaje = criterio.puntaje,
                idtipocriterio = criterio.id_tipo_criterio
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Metodo para validar el puntaje total del criterio de una cohorte
        /// </summary>
        /// <param name="idcohorte"></param>
        /// <param name="idtipocriterio"></param>
        /// <param name="idcriterio"></param>
        /// <param name="nuevopuntaje"></param>
        /// <returns></returns>
        public JsonResult ValidarPuntajeCriterio(int idcohorte, int idtipocriterio, int idcriterio, double nuevopuntaje)
        {
            double? puntaje = 0;
            var obj = BusClass.get_adh_tipocriterio(idcohorte).Where(l => l.id_tipo_criterio == idtipocriterio).FirstOrDefault();
            var criterios = Model.getcriteriosbytipocohorte(idcohorte).Where(l => l.id_tipo_criterio == idtipocriterio).ToList();

            double? pesogeneral = obj.puntaje;

            if (criterios.Count > 0)
            {
                if (idcriterio == 0)
                {
                    puntaje = criterios.Select(l => l.puntaje).Sum();
                    puntaje += nuevopuntaje;
                }
                else
                {
                    var criterio = criterios.Where(l => l.id_adh_criterio == idcriterio).First();
                    criterios.Remove(criterio);
                    puntaje = criterios.Select(l => l.puntaje).Sum();
                    puntaje += nuevopuntaje;
                }
            }


            if (puntaje <= pesogeneral)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Metodo para eliminar el criterio de una cohorte
        /// </summary>
        /// <param name="idtipocohorte"></param>
        /// <param name="idcriterio"></param>
        /// <returns></returns>
        public ActionResult EliminarCriterioCohorte(int idtipocohorte, int idcriterio)
        {
            Model.EliminarCriterioCohorte(idcriterio, ref MsgRes);

            return RedirectToAction("CriteriosCohorte", "Adherencia", new { idtipocohorte = idtipocohorte });
        }

        /// <summary>
        /// Vista principal, tablero de resultados
        /// </summary>
        /// <returns></returns>
        public ActionResult TableroResultados(int? regional, int? unis, int? ciudad, int? prestador, int? profesional, int? mes, int? numhistorias)
        {
            List<vw_rptResultadosAdherencia> listado = new List<vw_rptResultadosAdherencia>();
            try
            {
                listado = Model.GetResultadosPrestador(null);

                if (regional != null)
                {
                    listado = listado.Where(l => l.id_ref_regional == regional).ToList();
                }

                if (unis != null)
                {
                    listado = listado.Where(l => l.id_unis == unis).ToList();
                }

                if (ciudad != null)
                {
                    listado = listado.Where(l => l.id_ciudad == ciudad).ToList();
                }

                if (prestador != null)
                {
                    listado = listado.Where(l => l.id_prestador == prestador).ToList();
                }

                if (profesional != null)
                {
                    listado = listado.Where(l => l.id_profesional == profesional).ToList();
                }

                if (mes != null)
                {
                    listado = listado.Where(l => l.mes == mes.Value).ToList();
                }

                //if (numhistorias != null)
                //{
                //    List<vw_rptResultadosAdherencia> nuevolistado = new List<vw_rptResultadosAdherencia>();
                //    if (numhistorias.Value == 2)
                //    {
                //        var objlist = listado.Where(l => l.num_hc_evaluada == 2).Select(l => l.nit_prestador).ToList();
                //        foreach (var item in objlist)
                //        {
                //            List<vw_rptResultadosAdherencia> obj = listado.Where(l => l.nit_prestador == item).ToList();
                //            nuevolistado.AddRange(obj);
                //        }
                //    }
                //    else
                //    {
                //        var objlist = listado.Select(l => l.nit_prestador).ToList();
                //        foreach (var item in objlist)
                //        {
                //            List<vw_rptResultadosAdherencia> obj = listado.Where(l => l.nit_prestador == item).ToList();
                //            if (obj.Count <= 1)
                //            {
                //                nuevolistado.AddRange(obj);
                //            }
                //        }
                //    }

                //    listado = nuevolistado;
                //}

                ViewBag.resultados = listado.OrderByDescending(l => l.fecha_creacion).ToList();
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.meses = BusClass.meses();
                ViewBag.filtroreg = regional;
                ViewBag.filtrounis = unis;
                ViewBag.filtrociudad = ciudad;
                ViewBag.filtropresta = prestador;
                ViewBag.filtroprofes = profesional;
                ViewBag.filtromes = mes;
                ViewBag.filtronum = numhistorias;
                Session["ResultadosAdherencia"] = listado;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        /// <summary>
        /// Vista para elegir de que cohorte va a ingresar el resultado
        /// </summary>
        /// <returns></returns>
        public ActionResult Resultados()
        {
            ViewBag.reftipocohorte = BusClass.Get_refCohortes();
            return View();
        }

        /// <summary>
        /// Vita para ingresar el resultado
        /// </summary>
        /// <param name="idtipocohorte"></param>
        /// <returns></returns>
        public ActionResult IngresarResultados(int idtipocohorte)
        {
            Session["resultados"] = new adh_resultados();
            var tipocohorte = BusClass.Get_refCohortes().Where(l => l.id_ref_cohortes == idtipocohorte).FirstOrDefault();
            ViewBag.nomtipocohorte = tipocohorte.descripcion.ToUpper();
            ViewBag.idtipocohorte = idtipocohorte;
            ViewBag.reftipocriterio = Model.get_ref_TipoCriterio();
            ViewBag.refgrupotipocriterio = Model.get_ref_grupoTipoCriterio();
            ViewBag.reftipocohorte = BusClass.Get_refCohortes();
            ViewBag.criterios = Model.getcriteriosbytipocohorte(idtipocohorte);
            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.modalidadprestacion = BusClass.Get_adherencia_modalidad_prestacion();

            return View();
        }

        /// <summary>
        /// Metodo para llenar el formulario y guardar los resultados
        /// </summary>
        /// <param name="tipocohorte"></param>
        /// <param name="idprestador"></param>
        /// <param name="nit"></param>
        /// <param name="nombre"></param>
        /// <param name="contrato"></param>
        /// <param name="idregional"></param>
        /// <param name="historiaclinica1"></param>
        /// <param name="historiaclinica2"></param>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="resultadoshc1"></param>
        /// <param name="resultadoshc2"></param>
        /// <returns></returns>
        public JsonResult GuardarResultados(int tipocohorte, int regional, int unis, int ciudad, int prestador, int profesional, int contrato,
            int mes, int año, DateTime fecha_evaluacion, List<String> resultadoshc, string observaciones, string numdocumento
            , DateTime fechahistoriaclinica, int modalidadprestacion)
        {
            int contador = 0;
            List<adh_resultados> resul = Model.GetResultadosPrestador(prestador, profesional, mes, año);
            if (resul.Count == 0)
            {
                contador += 1;
            }
            else
            {
                contador = resul.Count + 1;
            }

            try
            {
                adh_resultados resultado = new adh_resultados();
                resultado.num_contrato = contrato;
                resultado.id_tipo_cohorte = tipocohorte;
                resultado.id_regional = regional;
                resultado.id_prestador = prestador;
                resultado.id_ciudad = ciudad;
                resultado.id_unis = unis;
                resultado.id_profesional = profesional;
                resultado.num_historia_clinica = "";
                resultado.mes_periodo_evaluacion = mes;
                resultado.año_periodo_evaluacion = año;
                resultado.fecha_evaluacion = fecha_evaluacion;
                resultado.fecha_creacion = DateTime.Now;
                resultado.usuario_digita = SesionVar.UserName;
                resultado.consecutivo_historia_clinica = contador;
                resultado.Observaciones = observaciones;
                resultado.num_historia_clinica = numdocumento;
                resultado.fecha_historia_clinica = fechahistoriaclinica;
                resultado.id_modalidad_prestacion = modalidadprestacion;
                int idreporte = Model.InsertarResultados(resultado, resultadoshc, ref MsgRes);
                return Json(new { id = idreporte, rta = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { id = 0, rta = 2 }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Validacion de una historia clinica
        /// </summary>
        /// <param name="idprestador"></param>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="historiaclinica"></param>
        /// <returns></returns>
        public JsonResult ValidacionHistoriasClinicas(int prestador, int profesional, int mes, int año, string historiaclinica)
        {
            int contador = 0;
            List<adh_resultados> resul = Model.GetResultadosPrestador(prestador, profesional, mes, año);

            if (resul.Count == 0)
            {
                contador += 1;
            }
            else
            {
                contador = resul.Count + 1;
            }

            if (contador <= 2)
            {
                var resul2 = resul.Where(l => l.num_historia_clinica == historiaclinica).FirstOrDefault();
                if (resul2 == null)
                {
                    return Json(0);
                }
                else
                {
                    return Json(2);
                }
            }
            else
            {
                return Json(1);
            }
        }

        /// <summary>
        /// Metodo para descargar el reporte de los resultados 
        /// </summary>
        /// <param name="idreportresultados"></param>
        public void ExportarInformeResultados(Int32? idreportresultados)
        {
            var reporte = Model.GetResultadosPrestador(idreportresultados.Value);
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptResultadosAdherencia.rdlc");
            Microsoft.Reporting.WebForms.ReportDataSource lista1 = new Microsoft.Reporting.WebForms.ReportDataSource("ResultadoAdherenciaPrestadorDataSet", reporte);
            Microsoft.Reporting.WebForms.ReportDataSource lista2 = new Microsoft.Reporting.WebForms.ReportDataSource("ResultadosAdherenciaReporteDataSet", Model.GetResultadosAdherencia(idreportresultados.Value));

            string nom = BusClass.Get_refCohortes().Where(l => l.id_ref_cohortes == reporte[0].id_tipo_cohorte).FirstOrDefault().descripcion;

            ReportParameter nomcohorte = new ReportParameter("nomtipocohorte", nom);
            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            //viewer.LocalReport.EnableExternalImages = true;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(lista1);
            viewer.LocalReport.DataSources.Add(lista2);
            viewer.LocalReport.SetParameters(nomcohorte);


            if (reporte.Count != 0)
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

        public void ExportarInformeResultados2(Int32? idreportresultados)
        {
            var reporte = Model.GetResultadosPrestador(idreportresultados.Value);
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            Models.Adherencia.AdherenciaConteo obj = Model.GetDatosConteoAdh(idreportresultados.Value);

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptResultadosAdherencia2.rdlc");
            Microsoft.Reporting.WebForms.ReportDataSource lista1 = new Microsoft.Reporting.WebForms.ReportDataSource("ResultadosAdherenciaDataSet", reporte);
            Microsoft.Reporting.WebForms.ReportDataSource lista2 = new Microsoft.Reporting.WebForms.ReportDataSource("ResultadosAdherenciaReporte2DataSet", Model.GetResultadosAdherencia2(idreportresultados.Value));
            Microsoft.Reporting.WebForms.ReportDataSource lista3 = new Microsoft.Reporting.WebForms.ReportDataSource("ResultadosAdherenciaResultConteoDataSet", Model.GetResultadosGrupoAdherencia(idreportresultados.Value));

            string nom = BusClass.Get_refCohortes().Where(l => l.id_ref_cohortes == reporte[0].id_tipo_cohorte).FirstOrDefault().descripcion;

            ReportParameter calidadcumple = new ReportParameter("count_calidad_cumple", obj.calidad_cumple.ToString());
            ReportParameter calidadnocumple = new ReportParameter("count_calidad_nocumple", obj.calidad_nocumple.ToString());
            ReportParameter calidadnoaplica = new ReportParameter("count_calidad_noaplica", obj.calidad_noaplica.ToString());
            ReportParameter calidadparcial = new ReportParameter("count_calidad_parcial", obj.calidad_parcial.ToString());
            ReportParameter cumplimientocumple = new ReportParameter("count_cumplimiento_cumple", obj.cumplimiento_cumple.ToString());
            ReportParameter cumplimientonocumple = new ReportParameter("count_cumplimiento_nocumple", obj.cumplimiento_nocumple.ToString());
            ReportParameter cumplimientonoaplica = new ReportParameter("count_cumplimiento_noaplica", obj.cumplimiento_noaplica.ToString());
            ReportParameter cumplimientoparcial = new ReportParameter("count_cumplimiento_parcial", obj.cumplimiento_parcial.ToString());

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            //viewer.LocalReport.EnableExternalImages = true;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(lista1);
            viewer.LocalReport.DataSources.Add(lista2);
            viewer.LocalReport.DataSources.Add(lista3);
            viewer.LocalReport.SetParameters(calidadcumple);
            viewer.LocalReport.SetParameters(calidadnocumple);
            viewer.LocalReport.SetParameters(calidadnoaplica);
            viewer.LocalReport.SetParameters(calidadparcial);
            viewer.LocalReport.SetParameters(cumplimientocumple);
            viewer.LocalReport.SetParameters(cumplimientonocumple);
            viewer.LocalReport.SetParameters(cumplimientonoaplica);
            viewer.LocalReport.SetParameters(cumplimientoparcial);


            if (reporte.Count != 0)
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

        /// <summary>
        /// Metodo para buscar un proveedor.... autocomplete
        /// </summary>
        /// <returns></returns>
        public JsonResult Buscar_Proveedor()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                List<Ref_ips_cuentas> proveedores = Model.getprestadores(term);


                var lista = (from reg in proveedores
                             select new
                             {
                                 id = reg.id_ref_ips_cuentas,
                                 nit = reg.Nit,
                                 nombre = reg.Nombre,
                                 label = reg.Nit + ", " + reg.Nombre,
                             }).Distinct().OrderBy(f => f.label).Take(15);
                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Metodo json post para guardar los tipos de cohorte
        /// </summary>
        /// <param name="idtipocohorte"></param>
        /// <param name="NomTipoCohorte"></param>
        /// <returns></returns>
        public JsonResult GuardarTipoCohorte(int idtipocohorte, string NomTipoCohorte)
        {
            ref_cohortes nuevo = new ref_cohortes();
            nuevo.id_ref_cohortes = idtipocohorte;
            nuevo.descripcion = NomTipoCohorte.ToUpper();
            nuevo.aplica_adh = true;
            try
            {
                if (idtipocohorte == 0)
                {
                    Model.InsertarTipoCohorte(nuevo);
                }
                else
                {
                    Model.ActualizarTipoCohorte(nuevo);
                }

                return Json(new { rta = 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { rta = 1, msj = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Metodo json post para obtener los datos de un tipo de cohorte
        /// </summary>
        /// <param name="idtipocohorte"></param>
        /// <returns></returns>
        public JsonResult GetdatosTipoCohorte(int idtipocohorte)
        {
            ref_cohortes cohorte = Model.getTipoCohorteById(idtipocohorte);

            if (cohorte != null)
            {
                var data = new
                {
                    idcohorte = cohorte.id_ref_cohortes,
                    nomcohorte = cohorte.descripcion
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }
        }

        /// <summary>
        /// Metodo json post para guardar un tipo de criterio
        /// </summary>
        /// <param name="idtipocriterio"></param>
        /// <param name="indice"></param>
        /// <param name="NomTipoCriterio"></param>
        /// <returns></returns>
        public JsonResult GuardarTipoCriterio(int idtipocriterio, string indice, string NomTipoCriterio)
        {
            ref_adh_tipo_criterio obj = new ref_adh_tipo_criterio();
            obj.id_tipo_criterio = idtipocriterio;
            obj.indice = indice.ToUpper();
            obj.nom_tipo_criterion = NomTipoCriterio.ToUpper();
            if (idtipocriterio == 0)
            {
                Model.InsertarTipoCriterio(obj, ref MsgRes);
            }
            else
            {
                Model.ActualizarTipoCriterio(obj, ref MsgRes);
            }

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { rta = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { rta = 1, msj = "Error: " + MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Metodo json get para obtener los datos de un tipo de critero
        /// </summary>
        /// <param name="idtipocriterio"></param>
        /// <returns></returns>
        public JsonResult GetdatosTipoCriterio(int idtipocriterio)
        {
            ref_adh_tipo_criterio tipocriterio = Model.get_ref_TipoCriterio().Where(l => l.id_tipo_criterio == idtipocriterio).FirstOrDefault();

            if (tipocriterio != null)
            {
                var data = new
                {
                    idtipocriterio = tipocriterio.id_tipo_criterio,
                    idc = tipocriterio.indice,
                    nomtipocriterio = tipocriterio.nom_tipo_criterion
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }
        }

        /// <summary>
        /// Eliminar Tipo criterio
        /// </summary>
        /// <param name="idtipocriterio"></param>
        /// <returns></returns>
        public ActionResult EliminarTipoCriterio(int idtipocriterio, int tipoahd)
        {
            Model.EliminarTipoCriterio(idtipocriterio, ref MsgRes);
            return RedirectToAction("AdminCriterios", "Adherencia", new { idtipoadherencia = tipoahd });
        }

        /// <summary>
        /// Buscar beneficiarios autocomplete
        /// </summary>
        /// <returns></returns>
        public JsonResult Buscar_Beneficiario()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 2)
                {
                    List<VW_base_beneficiarios> beneficiarios = BusClass.GetBeneficiarios(term, ref MsgRes);
                    var lista = (from reg in beneficiarios
                                 select new
                                 {
                                     id = reg.id_base_beneficiarios,
                                     nit = reg.Numero_identificacion,
                                     nombre = reg.Nombre,
                                     edad = reg.edad,
                                     label = reg.Numero_identificacion + "-" + reg.Nombre + " " + reg.Apellidos,
                                 }).Distinct().OrderBy(f => f.id).Take(25);
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

        /// <summary>
        /// Eliminar un tipo de cohorte
        /// </summary>
        /// <param name="idtipocohorte"></param>
        /// <returns></returns>
        public ActionResult EliminarTipoCohorte(int idtipocohorte)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    var criterios = db.adh_criterio.Where(l => l.id_tipo_cohorte == idtipocohorte).ToList();
                    if (criterios.Count > 0)
                    {
                        db.adh_criterio.DeleteAllOnSubmit(criterios);
                        db.SubmitChanges();
                    }

                    var tipocriterios = db.adh_tipocriterio.Where(l => l.id_tipo_cohorte == idtipocohorte).ToList();
                    if (tipocriterios.Count > 0)
                    {
                        db.adh_tipocriterio.DeleteAllOnSubmit(tipocriterios);
                        db.SubmitChanges();
                    }

                    var resultados = db.adh_resultados.Where(l => l.id_tipo_cohorte == idtipocohorte).ToList();
                    if (resultados.Count > 0)
                    {
                        foreach (var r in resultados)
                        {
                            var resuldetalle = db.adh_resultados_detalle.Where(l => l.id_adh_resultados == r.id_adh_resultados).ToList();
                            if (resuldetalle.Count > 0)
                            {
                                db.adh_resultados_detalle.DeleteAllOnSubmit(resuldetalle);
                                db.SubmitChanges();
                            }

                            db.adh_resultados.DeleteOnSubmit(r);
                            db.SubmitChanges();
                        }
                    }

                    var cohorte = db.ref_cohortes.Where(l => l.id_ref_cohortes == idtipocohorte).FirstOrDefault();
                    db.ref_cohortes.DeleteOnSubmit(cohorte);
                    db.SubmitChanges();

                    return RedirectToAction("ConfiguracionAdherencia", "Adherencia");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


        public string ObtenerUnis(int idregional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            List<ref_adherencia_unis> Unis = Model.GetUnisByRegional(idregional);
            foreach (var item in Unis)
            {
                result += "<option value='" + item.id_ref_adherencia_unis + "'>" + item.nom_adherencia_unis + "</option>";
            }

            return result;
        }

        public string ObtenerCiudades(int idunis)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            List<ref_adherencia_ciudad> Ciudades = Model.GetciudadByunis(idunis);
            foreach (var item in Ciudades)
            {
                result += "<option value='" + item.id_ref_adherencia_ciudad + "'>" + item.nom_ciudad + "</option>";
            }

            return result;
        }

        public string ObtenerPrestadores(int Ciudad)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            List<ref_adherencia_prestador_ciudad> Prestadores = Model.GetPrestadoresByciudad(Ciudad);
            foreach (var item in Prestadores)
            {
                result += "<option value='" + item.id_ref_adherencia_prestador + "'>" + item.ref_adherencia_prestador.Nit_prestador + " - " + item.ref_adherencia_prestador.Nom_prestador + "</option>";
            }

            return result;
        }

        public string ObtenerProfesionales(int idprestador)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            ref_adherencia_prestador presta = Model.GetprestadorByid(idprestador);
            List<ref_adherencia_profesional_prestador> Profesionales = Model.GetProfesionalesByprestador(idprestador);
            foreach (var item in Profesionales)
            {
                if (presta.Nit_prestador == item.ref_adherencia_profesional.Num_documento)
                {
                    result += "<option selected='selected' value='" + item.profesional_id + "'>" + item.ref_adherencia_profesional.Num_documento + " - " + item.ref_adherencia_profesional.Nom_profesional + "</option>";
                }
                else
                {
                    result += "<option value='" + item.profesional_id + "'>" + item.ref_adherencia_profesional.Num_documento + " - " + item.ref_adherencia_profesional.Nom_profesional + "</option>";
                }

            }

            return result;
        }

        public void ExportarResultadosAdherencia()
        {
            List<vw_rptResultadosAdherencia> Listado = (List<vw_rptResultadosAdherencia>)Session["ResultadosAdherencia"];

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");

            Sheet.Cells["A1:N1"].Style.Font.Bold = true;

            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A1:Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:Q1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:Q1"].Style.Font.Color.SetColor(Color.White);

            Sheet.Cells["A1"].Value = "Nit prestador";
            Sheet.Cells["B1"].Value = "Nombre prestador";
            Sheet.Cells["C1"].Value = "Documento profesional";
            Sheet.Cells["D1"].Value = "Nombre profesional";
            Sheet.Cells["E1"].Value = "Regional";
            Sheet.Cells["F1"].Value = "Unis";
            Sheet.Cells["G1"].Value = "Ciudad";
            Sheet.Cells["H1"].Value = "Número contrato";
            Sheet.Cells["I1"].Value = "Documento paciente";
            Sheet.Cells["J1"].Value = "Paciente";
            Sheet.Cells["K1"].Value = "Tipo adherencia";
            Sheet.Cells["L1"].Value = "Fecha evaluación";
            Sheet.Cells["M1"].Value = "Periodo evaluación";
            Sheet.Cells["N1"].Value = "Auditor";
            Sheet.Cells["O1"].Value = "Resultado";
            Sheet.Cells["P1"].Value = "Resultado cumplimiento programa";
            Sheet.Cells["Q1"].Value = "Resultado cumplimiento historia clinica";

            int row = 2;

            foreach (vw_rptResultadosAdherencia item in Listado)
            {

                Sheet.Cells["A" + row].Value = item.nit_prestador;
                Sheet.Cells["B" + row].Value = item.nom_prestador;
                Sheet.Cells["C" + row].Value = item.documento_profesional;
                Sheet.Cells["D" + row].Value = item.nom_profesional;
                Sheet.Cells["E" + row].Value = item.nombre_regional;
                Sheet.Cells["F" + row].Value = item.nom_adherencia_unis;
                Sheet.Cells["G" + row].Value = item.nom_ciudad;
                Sheet.Cells["H" + row].Value = item.num_contrato;
                Sheet.Cells["I" + row].Value = item.num_historia_clinica;
                Sheet.Cells["J" + row].Value = item.nom_evaluado;
                Sheet.Cells["K" + row].Value = item.nom_tipo_cohorte;
                Sheet.Cells["L" + row].Value = item.fecha_evaluacion;
                Sheet.Cells["M" + row].Value = item.periodo_evaluacion;
                Sheet.Cells["N" + row].Value = item.nom_auditor;
                if (!String.IsNullOrEmpty(item.nom_adh_modalidad_prestacion))
                {
                    Sheet.Cells["O" + row].Value = "N/A";
                    Sheet.Cells["P" + row].Value = item.resultado_cumplimineto_programa;
                    Sheet.Cells["Q" + row].Value = item.resultado_cumplimineto_hc;
                }
                else
                {
                    Sheet.Cells["O" + row].Value = item.resultado_evaluacion;
                    Sheet.Cells["P" + row].Value = "N/A";
                    Sheet.Cells["Q" + row].Value = "N/A";
                }

                row++;
            }

            string namefile = "Resultados_Adherencia_" + DateTime.Now.ToString("dd-mm-yyyy");
            Sheet.Cells["A:R"].AutoFitColumns();
            Sheet.Cells["A1:R" + row].Style.Font.Name = "Century Gothic";

            if (Listado.Count > 0)
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "</script> ";
                Response.Write(rta);
            }
        }

        public ActionResult ConsultaResultAdherencia()
        {
            ViewBag.reftipocohorte = BusClass.Get_refCohortes();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            return View();
        }

        public void DescargarResultadosAdherencia(int idtipodh, int? Regional, DateTime? fechainicio, DateTime? fechafinal, int? mesini, int? mesfin,
            int? añoini, int? añofin, DateTime? fechahcinicio, DateTime? fechahcfinal, int? unis, int? ciudad, int? prestador, int? profesional)
        {

            try
            {

                int countresult = 0;
                int z = 0;
                Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();
                var list = Model.GetListResultadosComponente(idtipodh);
                var listcriterios = Model.get_ref_TipoCriterio_cohorte(idtipodh);

                string[] columnas_1 = new string[100] {"P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "AA", "AB", "AC", "AD","AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT",
                "AU", "AV", "AW", "AX", "AY", "AZ","BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL","BM","BN","BO","BP","BQ","BR","BS","BT","BU","BV","BW",
                "BX","BY","BZ","CA","CB","CC","CD","CE","CF","CG","CH","CI","CJ","CK","CL","CM","CN","CO","CP","CQ","CR","CS","CT","CU","CV","CW","CX","CY","CZ","DA","DB","DC","DD","DE","DF","DG","DH","DI","DJ","DK"};

                string[] columnas_2 = new string[100] {"P{0}", "Q{0}", "R{0}", "S{0}", "T{0}", "U{0}", "V{0}", "W{0}", "X{0}", "Y{0}", "Z{0}", "AA{0}"
                ,"AB{0}", "AC{0}", "AD{0}", "AE{0}", "AF{0}", "AG{0}", "AH{0}", "AI{0}", "AJ{0}", "AK{0}", "AL{0}", "AM{0}", "AN{0}", "AO{0}", "AP{0}", "AQ{0}", "AR{0}", "AS{0}", "AT{0}", "AU{0}", "AV{0}", "AW{0}"
                ,"AX{0}", "AY{0}", "AZ{0}", "BA{0}", "BB{0}", "BC{0}", "BD{0}", "BE{0}", "BF{0}", "BG{0}", "BH{0}", "BI{0}", "BJ{0}", "BK{0}", "BL{0}","BM{0}","BN{0}","BO{0}","BP{0}","BQ{0}","BR{0}","BS{0}","BT{0}"
                ,"BU{0}","BV{0}","BW{0}","BX{0}","BY{0}","BZ{0}","CA{0}","CB{0}","CC{0}","CD{0}","CE{0}","CF{0}","CG{0}","CH{0}","CI{0}","CJ{0}","CK{0}","CL{0}","CM{0}","CN{0}","CO{0}","CP{0}","CQ{0}","CR{0}","CS{0}"
                ,"CT{0}","CU{0}","CV{0}","CW{0}","CX{0}","CY{0}","CZ{0}","DA{0}","DB{0}","DC{0}","DD{0}","DE{0}","DF{0}","DG{0}","DH{0}","DI{0}","DJ{0}","DK{0}"};

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");

                Sheet.Cells["A1:AZ1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:AZ1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AZ1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:BZ1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:BZ1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Nit prestador";
                Sheet.Cells["B1"].Value = "Prestador";
                Sheet.Cells["C1"].Value = "Documento profesional";
                Sheet.Cells["D1"].Value = "Profesional evaluado";
                Sheet.Cells["E1"].Value = "Regional";
                Sheet.Cells["F1"].Value = "Unis";
                Sheet.Cells["G1"].Value = "Ciudad";
                Sheet.Cells["H1"].Value = "Documento paciente";
                Sheet.Cells["I1"].Value = "Nombre paciente";
                Sheet.Cells["J1"].Value = "Periodo";
                Sheet.Cells["K1"].Value = "Fecha evaluación";
                Sheet.Cells["L1"].Value = "Fecha historia clinica";
                Sheet.Cells["M1"].Value = "Resultado";
                Sheet.Cells["N1"].Value = "Resultado cumplimiento programa";
                Sheet.Cells["O1"].Value = "Resultado cumplimiento historia clinica";

                int row = 2;

                List<Ref_regional> regionales = BusClass.GetRefRegion();
                if (Regional != null)
                {
                    regionales = regionales.Where(l => l.id_ref_regional == Regional).ToList();
                }

                foreach (Ref_regional reg in regionales)
                {
                    List<ManagmentReporteResultadosAdherenciaGnrResult> resultgnr = Model.ObtenerResultadosGeneralesAdherencia(idtipodh, reg.id_ref_regional, fechainicio, fechafinal, mesini, mesfin, añoini, añofin, fechahcinicio, fechahcfinal, unis, ciudad, prestador, profesional);
                    countresult += resultgnr.Count;
                    foreach (var item in resultgnr)
                    {
                        List<managmentReporteResultadosAdherencia2Result> result = Model.GetResultadosAdherencia2(item.id_adh_resultados);

                        Sheet.Cells["A" + row].Value = item.nit_prestador;
                        Sheet.Cells["B" + row].Value = item.nom_prestador;
                        Sheet.Cells["C" + row].Value = item.documento_profesional;
                        Sheet.Cells["D" + row].Value = item.nom_profesional;
                        Sheet.Cells["E" + row].Value = item.indice;
                        Sheet.Cells["F" + row].Value = item.nom_adherencia_unis;
                        Sheet.Cells["G" + row].Value = item.nom_ciudad;
                        Sheet.Cells["H" + row].Value = item.num_historia_clinica;
                        Sheet.Cells["I" + row].Value = item.nom_evaluado;
                        Sheet.Cells["J" + row].Value = item.periodo_evaluacion;
                        Sheet.Cells["K" + row].Value = item.fecha_evaluacion;
                        Sheet.Cells["L" + row].Value = item.fecha_historia_clinica;
                        if (idtipodh > 9)
                        {
                            Sheet.Cells["M" + row].Value = "N/A";
                            Sheet.Cells["N" + row].Value = item.resultado_cumplimineto_programa;
                            Sheet.Cells["O" + row].Value = item.resultado_cumplimineto_hc;
                        }
                        else
                        {
                            Sheet.Cells["M" + row].Value = item.resultado_evaluacion;
                            Sheet.Cells["N" + row].Value = "N/A";
                            Sheet.Cells["O" + row].Value = "N/A";
                        }

                        int i = 0;
                        int? idtipocriterio = 0;
                        foreach (var item2 in result)
                        {
                            idtipocriterio = item2.id_tipo_criterio;
                            Sheet.Cells[columnas_1[i] + "1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            Sheet.Cells[columnas_1[i] + "1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                            Sheet.Cells[columnas_1[i] + "1"].Style.Font.Color.SetColor(Color.White);
                            Sheet.Cells[columnas_1[i] + "1"].Value = item2.descripcion;
                            if (!string.IsNullOrEmpty(item2.nom_adh_grupo_tipocriterio))
                            {
                                switch (item2.seleccion_historia_clinica)
                                {
                                    case "SI":
                                        Sheet.Cells[string.Format(columnas_2[i], row)].Value = "CUMPLE";
                                        break;
                                    case "NO":
                                        Sheet.Cells[string.Format(columnas_2[i], row)].Value = "NO CUMPLE";
                                        break;
                                    case "NA":
                                        Sheet.Cells[string.Format(columnas_2[i], row)].Value = "NO APLICA";
                                        break;
                                    case "PA":
                                        Sheet.Cells[string.Format(columnas_2[i], row)].Value = "PARCIAL";
                                        break;
                                }
                            }
                            else
                            {
                                Sheet.Cells[string.Format(columnas_2[i], row)].Value = item2.resultado_historia_clinica;
                            }

                            i = i + 1;
                            z = i;
                        }

                        if (idtipodh > 9)
                        {
                            foreach (var item3 in listcriterios)
                            {
                                List<vw_seleccion_adh_por_componente> obj = list.Where(l => l.id_adh_resultados == item.id_adh_resultados && l.id_adh_tipocriterio == item3.id_tipo_criterio).ToList();
                                double resultcriterio = Resultado(obj);
                                Sheet.Cells[columnas_1[z] + "1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                Sheet.Cells[columnas_1[z] + "1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                Sheet.Cells[columnas_1[z] + "1"].Style.Font.Color.SetColor(Color.White);
                                Sheet.Cells[columnas_1[z] + "1"].Value = item3.nom_tipo_criterion;
                                Sheet.Cells[string.Format(columnas_2[z], row)].Value = resultcriterio;
                                z++;
                            }
                        }


                        row++;
                    }
                }

                string namefile = "ResultadosAdherencia";

                Sheet.Cells["A:K"].AutoFitColumns();

                if (countresult > 0)
                {
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('No se han encontrado resultados');" +
                    "</script> ";
                    Response.Write(rta);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

        }

        public double Resultado(List<vw_seleccion_adh_por_componente> obj)
        {
            double countno = 0, countSi = 0;
            double resultado = 0;

            if (obj.Where(l => l.seleccion_historia_clinica == "NO").Count() > 0)
            {
                countno = obj.Where(l => l.seleccion_historia_clinica == "NO").FirstOrDefault().count.Value;
            }

            if (obj.Where(l => l.seleccion_historia_clinica == "SI").Count() > 0)
            {
                countSi = obj.Where(l => l.seleccion_historia_clinica == "SI").FirstOrDefault().count.Value;
            }

            if (countSi > 0)
            {
                double co = (countSi + countno);
                resultado = ((countSi / co) * 100);
            }

            return Math.Round(resultado, 2);
        }

        public ActionResult AgregarPrestador(int? alerta)
        {
            Models.General General = new Models.General();

            if (alerta == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transacción exitosa!", "Ingresó el prestador exitosamente!");
            }

            else if (alerta == 2)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Ingreso fallido!", MsgRes.DescriptionResponse);
            }
            else
            {
                ViewData["alerta"] = "";
            }

            List<ref_adherencia_profesional> listaPrestadores = new List<ref_adherencia_profesional>();

            ViewBag.listaPrestadores = listaPrestadores;
            ViewData["CantidadProfesionales"] = 0;
            ViewBag.CantidadProfesionales = 0;
            Session["OtroProfesionalList"] = new List<ref_adherencia_profesional>();

            List<calidad_ref_especialidad> lista = new List<calidad_ref_especialidad>();
            lista = BusClass.GetEspecialidades();
            ViewBag.especialidad = lista;

            List<ref_adherencia_ciudad> ciudad = BusClass.GetCiudad();
            ViewBag.ciudades = ciudad;
            ViewBag.regional = BusClass.GetRefRegion();


            Session["creado"] = 0;

            return View();
        }

        public string ObtenerUnisPrestador(int idregional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            List<ref_adherencia_unis> Unis = Model.GetUnisByRegional(idregional);

            foreach (var item in Unis)
            {
                result += "<option value='" + item.id_ref_adherencia_unis + "'>" + item.nom_adherencia_unis + "</option>";
            }

            return result;
        }


        public JsonResult ObtenerUnisPrestador2(Int32 regional)
        {
            List<ref_adherencia_unis> Unis = Model.GetUnisByRegional(regional);

            return Json(Unis.Select(p => new { idUnis = p.id_ref_adherencia_unis, nombre = p.nom_adherencia_unis }), JsonRequestBehavior.AllowGet);
        }

        public string ObtenerCiudadesPrestador(int idunis)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            List<ref_adherencia_ciudad> Ciudades = Model.GetciudadByunis(idunis);
            foreach (var item in Ciudades)
            {
                result += "<option value='" + item.id_ref_adherencia_ciudad + "'>" + item.nom_ciudad + "</option>";
            }

            return result;
        }


        public JsonResult ObtenerCiudadesPrestador2(Int32 idUnis)
        {
            List<ref_adherencia_ciudad> Ciudades = Model.getCiudadesUnis(idUnis);

            return Json(Ciudades.Select(p => new { idCiudad = p.id_ref_adherencia_ciudad, nombre = p.nom_ciudad }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarPrestador(string value)
        {
            try
            {
                string term = Convert.ToString(value);

                if (term.Length >= 7)
                {
                    List<management_traerPrestadoresResult> prestadores = new List<management_traerPrestadoresResult>();
                    prestadores = BusClass.traerPrestadoresId(value).ToList();

                    var lista = (from reg in prestadores
                                 select new
                                 {
                                     nit = reg.Nit_prestador,
                                     nombre = reg.Nom_prestador,
                                     regional = reg.regional_id,
                                     unis = reg.id_ref_adherencia_unis,
                                     ciudad = reg.id_ref_adherencia_ciudad,
                                     var = 0,
                                 }).Distinct().OrderBy(f => f.nombre).Take(15);

                    if (prestadores.Count() > 0)
                    {
                        Session["creado"] = 1;
                    }
                    else
                    {
                        Session["creado"] = 0;
                    }

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

        public JsonResult AgregarOtroProfesional(string documento, String nombre, int especialidad)
        {

            var result = "";

            List<calidad_ref_especialidad> lista = new List<calidad_ref_especialidad>();
            lista = BusClass.GetEspecialidades();
            lista = lista.Where(x => x.id == especialidad).ToList();

            calidad_ref_especialidad nuevo = lista.FirstOrDefault();

            List<ref_adherencia_profesional> Agregadas = (List<ref_adherencia_profesional>)Session["OtroProfesionalList"];

            ref_adherencia_profesional NewProfesional = new ref_adherencia_profesional
            {
                Num_documento = documento,
                Nom_profesional = nombre,
                Especialidad = nuevo.descripcion,
            };
            try
            {
                Agregadas.Add(NewProfesional);
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            Session["OtroProfesionalList"] = Agregadas;
            ViewBag.CantidadAsistentes = Agregadas.Count();

            int i = 0;

            foreach (ref_adherencia_profesional a in Agregadas)
            {
                i += 1;

                result += "<tr>"
                    + "<td>" + i + "</td>";

                result += "<td>" + a.Num_documento + "</td>";
                result += "<td>" + a.Nom_profesional + "</td>";
                result += "<td>" + a.Especialidad + "</td>";
                result += "<td class='text-center'><a title='Remover Otro Si' href='javascript:removerProfesional(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                result += "</tr>";
            }

            var data = new
            {
                tabla = result,
                cuentaProfesional = Agregadas.Count(),

            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoverProfesional(Int32? idProfesional)
        {
            string result = "";

            List<ref_adherencia_profesional> otroProfesionalList = (List<ref_adherencia_profesional>)Session["OtroProfesionalList"];
            otroProfesionalList.Remove(otroProfesionalList[Convert.ToInt32(idProfesional) - 1]);

            Session["OtroProfesionalList"] = otroProfesionalList;

            int i = 0;

            if (otroProfesionalList.Count != 0)
            {

                foreach (ref_adherencia_profesional a in otroProfesionalList)
                {
                    i += 1;

                    result += "<tr>"
                        + "<td>" + i + "</td>";

                    result += "<td>" + a.Num_documento + "</td>";
                    result += "<td>" + a.Nom_profesional + "</td>"
                        + "<td>" + a.Especialidad + "</td>"
                        + "<td class='text-center'><a title='Remover Otro Si' href='javascript:removerProfesional(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                    result += "</tr>";
                }
            }
            else
            {
                result += "<tr> <td colspan='10' style='text-align:center'> " + "No se han agregado Profesionales. " + "</td> </tr>";
            }

            var data = new
            {
                tabla = result,
                cuentaProfesional = otroProfesionalList.Count(),
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistroPrestador(string docPre, string nomPre, int ciuPre)
        {

            List<ref_adherencia_profesional> otroProfesionalList = (List<ref_adherencia_profesional>)Session["OtroProfesionalList"];

            try
            {
                ref_adherencia_prestador obj = new ref_adherencia_prestador();
                obj.Nit_prestador = docPre;
                obj.Nom_prestador = nomPre;
                var creado = Convert.ToInt32(Session["creado"]);

                var idPrestador = BusClass.insertarPrestador(obj, otroProfesionalList, creado);

                if (idPrestador != 0)
                {
                    if (creado == 0)
                    {
                        ref_adherencia_prestador_ciudad obj2 = new ref_adherencia_prestador_ciudad();
                        obj2.id_ref_adherencia_prestador = idPrestador;
                        obj2.id_ref_adherencia_ciudad = ciuPre;
                        var idPrestadorCiudad = BusClass.insertarPrestadorCiudad(obj2);
                    }
                }
                return Json(new { success = true, idActa = 0, variable = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return Json(new { success = false, idActa = 0, variable = 2 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
