using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;


namespace AsaludEcopetrol.Controllers.Medicamentos
{
    [SessionExpireFilter]
    public class MDConciliacionController : Controller
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


        public ActionResult BuscarFactura()
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            return View(Model);
        }

        [HttpPost]
        public ActionResult BuscarFactura(Models.Medicamentos.GestionMedicamentos Model)
        {
            return RedirectToAction("ConciliacionesMedicamentos", "MDConciliacion", new { variable = Model.numero_formula });

        }
        #endregion
        public ActionResult TableroConciliacionMedicamentos(DateTime? fechainicial, DateTime? fechafinal, String ciudad, String detalle, String anexo)
        {

            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();
            List<Managment_md_tablero_ConciliacionesResult> list = new List<Managment_md_tablero_ConciliacionesResult>();

            Model.CuentaFacTableroControlConciliaciones(ref MsgRes);

            if (fechainicial != null)
            {
                list = Model.CuentaFacTableroControlConciliaciones(ref MsgRes);

                list = list.Where(l => l.cargue_fecha >= fechainicial).ToList();
                ViewBag.fechainicial = fechainicial.Value.ToString("MM/dd/yyyy");
            }

            if (fechafinal != null)
            {
                list = Model.CuentaFacTableroControlConciliaciones(ref MsgRes);

                list = list.Where(l => l.cargue_fecha <= fechafinal).ToList();
                ViewBag.fechafinal = fechafinal.Value.ToString("MM/dd/yyyy");
            }

            ViewBag.List = list;

            return View();
        }

        public ActionResult DescargarReporteConciliaciones(DateTime? fechainicial, DateTime? fechafinal, String ciudad, String detalle, String anexo)
        {

            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();
            List<Managment_md_tablero_Conciliaciones_detalleResult> list = new List<Managment_md_tablero_Conciliaciones_detalleResult>();

            list = Model.CuentaFacTableroControlConciliacionesdtll(ref MsgRes);

            if (fechainicial != null)
            {
                list = list.Where(l => l.cargue_fecha >= fechainicial).ToList();
                ViewBag.fechainicial = fechainicial.Value.ToString("MM/dd/yyyy");
            }

            if (fechafinal != null)
            {
                list = list.Where(l => l.cargue_fecha <= fechafinal).ToList();
                ViewBag.fechafinal = fechafinal.Value.ToString("MM/dd/yyyy");
            }


            if (SesionVar.ROL == "1")
            {
                list = list.OrderBy(x => x.cargue_fecha).ToList();
            }
            else
            {
                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();
                list = list.OrderBy(x => x.cargue_fecha).ToList();
            }

            ViewBag.List = list;

            GridView gv = new GridView();
            gv.DataSource = list.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ConsolidadoConciliacion.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return View();
        }

        public ActionResult ConciliacionesMedicamentos(String variable)
        {

            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();
            List<Managment_md_tablero_Conciliaciones_detalleResult> list = new List<Managment_md_tablero_Conciliaciones_detalleResult>();

            list = Model.CuentaFacTableroControlConciliacionesdtll(ref MsgRes);
            list = list.Where(x => x.numero_formula == variable).ToList();

            ViewBag.List = list;

            return View(Model);
        }

        public PartialViewResult GestionarConciliacion(Int32? ID)
        {
            Models.General General = new Models.General();
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();
            ViewBag.idrole = SesionVar.ROL;

            vw_md_glosa_conciliacion Obj = new vw_md_glosa_conciliacion();

            Obj = Model.ConsultaGlosaDtllId(ID.Value);

            ViewBag.id_md_glosa_detalle = Obj.Id;
            ViewBag.vlrformula = Obj.vlr_total;
            ViewBag.vlrglosa = Obj.valor_glosado;
            ViewBag.Observaciones = Obj.observaciones;
            ViewBag.motglosa = Obj.motivo_glosa;
            ViewBag.numero_formula = Obj.numero_formula;
            ViewBag.obsGlosa = Obj.observaciones;




            return PartialView(Model);
        }


        public JsonResult SaveConciliacion(Models.Medicamentos.GestionMedicamentos Model)
        {

            String mensaje = "";

            try
            {
                md_glosa_conciliacion obj = new md_glosa_conciliacion();

                obj.id_md_glosa_detalle = Model.id_md_glosa_detalle;
                obj.resultado_conciliacion = Convert.ToString(Model.resultado_conciliacion);
                obj.valor_sustentado = Model.valor_sustentado;
                obj.observaciones = Model.observaciones;
                obj.estado = Model.estado;
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                Model.InsertarFFMMGlosaConciliacion(obj, ref MsgRes);

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
    }
}