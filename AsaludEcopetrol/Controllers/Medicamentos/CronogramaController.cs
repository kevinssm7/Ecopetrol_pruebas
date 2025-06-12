using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
//using Microsoft.Reporting.WebForms;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace AsaludEcopetrol.Controllers.Medicamentos
{
    [SessionExpireFilter]
    public class CronogramaController : Controller
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

        #endregion

        // GET: Cronograma
        public ActionResult CronogramaVisitas(Int32? ID)
        {
            Models.Medicamentos.Cronograma Model = new Models.Medicamentos.Cronograma();
            Models.General General = new Models.General();
            Model.auditor_grupo = "";
            ViewBag.idrole = SesionVar.ROL;
            if (ID == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Ingreso Correctamente");
            }
            else
            {
                ViewData["alerta"] = "";

            }




            return View(Model);
        }

        public ActionResult TableroCronogramaVisitas()
        {
            Models.Medicamentos.Cronograma Model = new Models.Medicamentos.Cronograma();

            ViewBag.idrole = SesionVar.ROL;
            ViewBag.usuario = SesionVar.UserName;
            ViewData["alerta"] = "";
            Model.ConsultaCronoUsuario(ViewBag.usuario);


            return View(Model);
        }


        [HttpPost]
        public ActionResult CronogramaVisitas(Models.Medicamentos.Cronograma Model)
        {
            Models.General General = new Models.General();

            try
            {
                ViewBag.idrole = SesionVar.ROL;
                ViewData["alerta"] = "";
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";

                if (Model.fecha_visitaOK != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA VISITA";
                    Conteo = Conteo + 1;
                }

                if (Model.tipo_visita != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "TIPO VISITA";
                    Conteo = Conteo + 1;
                }

                if (Model.id_puntos_dispensacion != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "PUNTOS DISPENSACION";
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

                    Model.OBJCrono.id_punto_dispensacion = Convert.ToString(Model.id_puntos_dispensacion);
                    Model.OBJCrono.tipo_visita = Model.tipo_visita;
                    Model.OBJCrono.auditor_asignado = Convert.ToString(Model.auditor_grupo);
                    Model.OBJCrono.fecha_visita = Model.fecha_visitaOK;
                    Model.OBJCrono.fecha_digita = DateTime.Now;
                    Model.OBJCrono.usuario_digita = SesionVar.UserName;

                    Model.InsertarCronoVisitas(ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {

                        return RedirectToAction("CronogramaVisitas", "Cronograma", new { ID = 1 });
                    }
                    else
                    {
                        ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", "ERROR AL INGRESO!");
                    }
                }
                else
                {
                    ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", variable2);
                }



            }
            catch
            {

            }
            return View(Model);

        }

        public JsonResult GetPuntosDispensacion(Models.Medicamentos.Cronograma Model)
        {
            return Json(Model.lisPuntosDispersacion.Select(p => new { PuntosId = p.id_md_ref_puntos_dispensacion, PuntosName = p.nombre_esm }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCronoAuditores(Int32? idpuntosdispensacion)
        {
            Models.Medicamentos.Cronograma Model = new Models.Medicamentos.Cronograma();
            List<Managment_md_Ref_crono_auditoresResult> result = Model.ConsultaListaCronoAuditoresGrupo(1, idpuntosdispensacion, ref MsgRes);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cronograma_Read([DataSourceRequest] DataSourceRequest request)
        {
            Models.Medicamentos.Cronograma  Model = new Models.Medicamentos.Cronograma();

            return Json(GetCronograma().ToDataSourceResult(request));
        }

        private static IEnumerable<vw_md_crono> GetCronograma()
        {
            Models.Medicamentos.Cronograma Model = new Models.Medicamentos.Cronograma();

            List<vw_md_crono> Lista = new List<vw_md_crono>();

            Lista =  Model.listaCronograma();

            return Lista;

        }

        public ActionResult CargarReporte(Int32 id)
        {
            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptNotificacionAuditoria.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<ManagmentReportNotifiAuditoriaResult> lst = new List<ManagmentReportNotifiAuditoriaResult>();

            lst = Model.ReportNotificacionAudi(Convert.ToInt32(id));
            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetNotificacionAudi", lst);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                try
                {
                    viewer.LocalReport.Refresh();
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    
                    byte[] bytes1 = viewer.LocalReport.Render("WORD", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                 
                    byte[] array = bytes1.ToArray();

                    if (array != null)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();

                        Response.ContentType = mimeType;
                        Response.AddHeader("content-length", array.Length.ToString());
                        Response.BinaryWrite(array);
                        Response.Flush();
                       
                    }
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
            return View();
        }
    }
}