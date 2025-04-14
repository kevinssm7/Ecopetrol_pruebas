using LinqToExcel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.Models.Urgencias;
using ECOPETROL_COMMON.ENTIDADES;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.CUrgencias
{
    [SessionExpireFilter]
    public class UrgenciasController : Controller
    {
        Urgencias Model = new Urgencias();

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


        // GET: Urgencias
        public ActionResult CargueUrgencias()
        {
            // ToEntidadHojaExcelList("C:/Users/User/Desktop/requerimiento.xlsx");
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";
            return View();
        }

        public ActionResult TableroUrgenciasFull(DateTime? FECHA1 , DateTime? FECHA2, String Regional)
        {
            Models.Urgencias.Urgencias Model = new Models.Urgencias.Urgencias();

            ViewBag.idrole = SesionVar.ROL;
            ViewBag.usuario = SesionVar.UserName;
            ViewData["alerta"] = "";

            Model.fecha_desde = FECHA1;
            Model.fecha_hasta = FECHA2;
            Model.regional = Regional;
 

            return View(Model);

        }

        [HttpPost]
        public ActionResult CargueUrgencias(HttpPostedFileBase file)
        {
            Models.General General = new Models.General();

            /* Agrega el archivo y lo guarda */
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Files"), fileName);
            file.SaveAs(path);

            try
            {
                /*Luego que guarda el archivo, setea los valores y agrega a base de datos */
                List<urg_cargue_base_ok> ListaUrgencias = Excelaentidad(path);

                Model.InsertarUrgencias(ListaUrgencias);
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Urgencias Cargadas Exitosamente");
            }
            catch(Exception ex)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", ex.Message);
            }

            ViewBag.idrole = SesionVar.ROL;
            return View();

        }

        public List<urg_cargue_base_ok> Excelaentidad(string pathDelFicheroExcel)
        {
            var book = new ExcelQueryFactory(pathDelFicheroExcel);
            var resultado = (from row in book.Worksheet("ANEXO 1 ESTRUCTURA URG")
                             let item = new ECOPETROL_COMMON.ENTIDADES.urg_cargue_base_ok
                             {
                                 ciudad = row["Ciudad"],
                                 ips = row["Ips"],
                                 tipo_documento = row["Tipo documento"],
                                 numero_documento = row["Numero documento"],
                                 fecha_ingreso = Convert.ToDateTime(row["Fecha ingreso"]),
                                 hora_ingreso = row["Hora ingreso (hh:mm:ss PM/AM)"],
                                 CODIGO_DX_INGRESO = row["Codigo de ingreso"],
                                 DX_DE_INGRESO = row["Dx de ingreso"],
                                 FECHA_EGRESO = Convert.ToDateTime(row["Fecha egreso"]),
                                 Hora_Egreso = row["Hora Egreso (hh:mm:ss PM/AM)"],
                                 CODIGO_DE_EGRESO = row["Codigo de egreso"],
                                 DX_DE_EGRESO = row["Dx de egreso"],
                                 TRIAGE = row["Triage"],
                                 COORDINACION = row["Coordinacion"],
                                 DESTINO = row["Destino"],
                                 usuario_digita = SesionVar.UserName,
                                 fecha_digita = DateTime.Now
                             }
                             select item).ToList();
            book.Dispose();
            return resultado;
        }

        public ActionResult TableroUrgencias()
        {
            Models.Urgencias.Urgencias Model = new Models.Urgencias.Urgencias();

            ViewBag.idrole = SesionVar.ROL;
            ViewBag.usuario = SesionVar.UserName;
            ViewData["alerta"] = "";
            List<Ref_regional> listaregional = Model.RefRegional;
            ViewBag.listaRegionales = listaregional;


            return View(Model);

            //List<urg_cargue_base_ok> lista = new List<urg_cargue_base_ok>();
            
            //List<Ref_regional> listaregional = facmodel.RefRegional;
            //ViewBag.listaRegionales = listaregional;
            //ViewBag.idrole = SesionVar.ROL;

            //ViewBag.fechadesde = (string)Session["fecha1"];
            //ViewBag.fechahasta = (string)Session["fecha2"];
            //ViewBag.regional = (string)Session["regional"];

            //DateTime? fecha1 = null;
            //DateTime? fecha2 = null;
            //int? regional = null;

            //if (!string.IsNullOrEmpty((string)Session["fecha1"]))
            //    fecha1 = Convert.ToDateTime((string)Session["fecha1"]);

            //if (!string.IsNullOrEmpty((string)Session["fecha2"]))
            //    fecha2 = Convert.ToDateTime((string)Session["fecha2"]);

            //if (!string.IsNullOrEmpty((string)Session["regional"]))
            //    regional = Convert.ToInt32((string)Session["regional"]);

            //List<urg_cargue_base_ok> listacarguebase = new List<urg_cargue_base_ok>();

            //if(SesionVar.ROL == "1")
            //{
            //    listacarguebase = Model.ConsultarUrgencias(null, fecha1, fecha2, regional, null);
            //}
            //else
            //{
            //    listacarguebase = Model.ConsultarUrgencias(null, fecha1, fecha2, regional, SesionVar.IDUser);
            //}

            //return View(listacarguebase);

        }

        [HttpPost]
        public ActionResult TableroUrgencias(Models.Urgencias.Urgencias Model)
        {
            Models.General General = new Models.General();
            ViewBag.idrole = SesionVar.ROL;
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_desdeOK != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "DEBE INGRESAR FECHA DESDE";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_hastaOK != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "DEBE INGRESAR FECHA HASTA";
                Conteo = Conteo + 1;
            }

            if (Model.regional != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "REGIONAL";
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
                return RedirectToAction("TableroUrgenciasFull", "Urgencias", new { FECHA1 = Model.fecha_desdeOK, FECHA2 = Model.fecha_hastaOK , Regional = Model.regional });
            }
            else
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", variable2);
            }


         

            return View(Model);

            //if (SesionVar.ROL == "1")
            //{
            //    listacarguebase = Model.ConsultarUrgencias(null, fechadesde, fechahasta, regional, null);
            //}
            //else
            //{
            //    listacarguebase = Model.ConsultarUrgencias(null, fechadesde, fechahasta, regional, SesionVar.IDUser);
            //}

            //Models.Facturacion.FacturaDevolucion facmodel = new Models.Facturacion.FacturaDevolucion();
            //List<Ref_regional> listaregional = facmodel.RefRegional;
            //ViewBag.listaRegionales = listaregional;

            //ViewBag.fechadesde = fechadesde.Value.ToString("MM/dd/yyyy");
            //ViewBag.fechahasta = fechahasta.Value.ToString("MM/dd/yyyy");
            //ViewBag.regional = regional;

            //Session["fecha1"] = fechadesde.Value.ToString("MM/dd/yyyy");
            //Session["fecha2"] = fechahasta.Value.ToString("MM/dd/yyyy");
            //Session["regional"] = regional.ToString();

            //return View(listacarguebase);
        }

        public ActionResult AuditoriaUrgencia(int? idurgencia, string[] Filtros)
        {
            
            ViewBag.listatipoegreso = Model.ConsultaTipoEgreso();
            ViewBag.listadestinopaciente = Model.ConsultaDestinoPaciente();
            ViewBag.idcargueurg = idurgencia;
            return View();
        }

        [HttpPost]
        public ActionResult AuditoriaUrgencia(int idurgencia, DateTime fechaingreso, string horas_horaingreso, string min_horaingreso, string triage, string dg1ingreso, string dg2ingreso, string dg3ingreso, 
            DateTime fechaegreso, string horas_horaegreso, string min_horaegreso, int estadosalida, string dg1egreso, string dg2egreso, string dg3egreso, int destinopaciente, string otrodestinopaciente)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            ViewBag.listatipoegreso = db.Ref_tipo_egreso.ToList();

            urg_auditoria_urgencias ObjUrgencia = new urg_auditoria_urgencias();
            ObjUrgencia.fecha_ingreso = fechaingreso;
            ObjUrgencia.hora_ingreso = horas_horaingreso + ":" + min_horaingreso;
            ObjUrgencia.Triage = triage;
            ObjUrgencia.cie10_1_egreso = dg1ingreso;
            ObjUrgencia.cie10_2_ingreso = dg2ingreso;
            ObjUrgencia.cie10_3_ingreso = dg3ingreso;
            ObjUrgencia.Fecha_salida = fechaegreso;
            ObjUrgencia.hora_salida = horas_horaegreso + ":" + min_horaegreso;
            ObjUrgencia.cie10_1_egreso = dg1egreso;
            ObjUrgencia.cie10_2_egreso = dg2egreso;
            ObjUrgencia.cie10_3_egreso = dg3egreso;
            ObjUrgencia.id_destino_paciente = destinopaciente;
            ObjUrgencia.otro_destino_paciente = otrodestinopaciente;
            ObjUrgencia.usuario_digita = SesionVar.UserName;
            ObjUrgencia.id_urg_base = idurgencia;
            ObjUrgencia.fecha_digita = DateTime.Now;
            Model.InsertarAuditoriaUrgencias(ObjUrgencia);

            ViewData["fecha1"] = Session["fecha1"].ToString();
            ViewData["fecha2"] = Session["fecha2"].ToString();
            ViewData["regional"] = Session["fecha1"].ToString();

            return RedirectToAction("TableroUrgencias", "Urgencias", ViewData);
        }
    }
}