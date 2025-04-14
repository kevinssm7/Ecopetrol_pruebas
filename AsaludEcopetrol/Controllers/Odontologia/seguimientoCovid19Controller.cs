using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Helpers;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Odontologia
{
    [SessionExpireFilter]

    public class seguimientoCovid19Controller : Controller
    {
        private ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

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

        // GET: seguimientoCovid19
        public ActionResult CargueBase(String variable)
        {

            Models.General General = new Models.General();
            Models.Odontologia.covid19 Model = new Models.Odontologia.covid19();
            if (variable == "1")
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se ingreso el equipo exitosamente ");
            }
            else
            {
                ViewData["alerta"] = "";
            }



            return View(Model);
        }


        public ActionResult TablerosSeguimiento(String variable, String documento)
        {

            Models.General General = new Models.General();
            Models.Odontologia.covid19 Model = new Models.Odontologia.covid19();
            if (variable == "1")
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se realizo el detalle del documento: " + documento + " exitosamente ");
            }
            else
            {
                ViewData["alerta"] = "";
            }

            Model.ConsultaTotales();
            Model.ConsultaTotalesInterdiario();

            List<vw_seguimiento_covid19_diario> list = BusClass.ConsultaListadoseguimientoCovid19().ToList();
            List<vw_seguimiento_covid19_interdiario> list2 = BusClass.ConsultaListadoseguimientoInterdiarioCovid19().ToList();
            List<vw_seguimiento_covid19_casos_cerrados> list3 = BusClass.ConsultaListadoseguimientoCerradosCovid19().ToList();



            if (SesionVar.ROL == "1")
            {
                list = list.Where(l => l.estado != "5").ToList();
                list = list.Where(l => l.seguimiento == "1. DIARIO").ToList();
                ViewBag.listaseguimientoprincipal = list;

            }
            else
            {
                list = list.Where(l => l.estado != "5").ToList();
                list = list.Where(l => l.seguimiento == "1. DIARIO").ToList();
                list = list.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();
                ViewBag.listaseguimientoprincipal = list;

            }

            if (SesionVar.ROL == "1")
            {
                list2 = list2.Where(l => l.estado == "5").ToList();
                list2 = list2.Where(l => l.seguimiento == "2. INTERDIARIO").ToList();
                ViewBag.listaseguimientointerdiario = list2;

            }
            else
            {

                list2 = list2.Where(l => l.estado == "5").ToList();
                list2 = list2.Where(l => l.seguimiento == "2. INTERDIARIO").ToList();
                list2 = list2.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();
                ViewBag.listaseguimientointerdiario = list2;

            }

            if (SesionVar.ROL == "1")
            {

                ViewBag.listaseguimientocerrados = list3;

            }
            else
            {


                list3 = list3.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();
                ViewBag.listaseguimientocerrados = list3;

            }


            return View(Model);
        }


        public ActionResult Consul()
        {


            return View();
        }


        [HttpPost]
        public JsonResult ConsultaGeneral(DateTime? fechainicial, DateTime? fechafinal, String OPC)
        {
            List<vw_seguimiento_covid19_general_detalle> listaseguimiento = BusClass.Consultageneraldetalleseguimientocovid();
            if (fechainicial != null)
            {

                listaseguimiento = listaseguimiento.Where(l => l.fecha_digita_gestion.Value.Date >= fechainicial.Value.Date).ToList();
            }

            if (fechafinal != null)
            {
      
                listaseguimiento = listaseguimiento.Where(l => l.fecha_digita_gestion.Value.Date <= fechafinal.Value.Date).ToList();
            }

            if (listaseguimiento.Count != 0)
            {
                OPC = "1";
            }
            else
            {
                OPC = "2";
            }

            return Json(new { OPC = OPC, fechauno = fechainicial, fechados = fechafinal }, JsonRequestBehavior.AllowGet);
        }

        public void ExportarListadoSeguimiento(DateTime? fechainicial, DateTime? fechafinal)
        {
            string fecha1 = "";
            string fecha2 = "";

            List<vw_seguimiento_covid19_general_detalle> listaseguimiento = BusClass.Consultageneraldetalleseguimientocovid();
            if (fechainicial != null)
            {
                fecha1 = fechainicial.Value.ToString("dd/MM/yyyy");
                listaseguimiento = listaseguimiento.Where(l => l.fecha_digita_gestion.Value.Date >= fechainicial.Value.Date).ToList();
            }

            if (fechafinal != null)
            {
                fecha2 = fechafinal.Value.ToString("dd/MM/yyyy");
                listaseguimiento = listaseguimiento.Where(l => l.fecha_digita_gestion.Value.Date <= fechafinal.Value.Date).ToList();
            }


            //RUTA REPORTE
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptSeguimientoGeneralCovid.rdlc");
            string filename = "";

            List<vw_seguimiento_covid19_general_detalle> lst = listaseguimiento;

            filename = "Seguimiento general covi19";




            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("seguimientocovid19", lst);

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
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                  

                    byte[] bytes = viewer.LocalReport.Render(
                       "Excel", null, out mimeType, out encoding,
                        out extension,
                       out streamids, out warnings);

                    filename = string.Format("{0}.{1}", "ExportToExcel", "xls");
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                    Response.ContentType = mimeType;
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();



                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
            else
            {
                ModelState.AddModelError("", "-- -!!!NO TIENE NINGUN COMPROBANTE CON ESTOS CRITERIOS DE BUSQUEDA!!! -- - *");
            }


        }


        [HttpGet]
        public PartialViewResult _GestionTableros(Int32? ID)
        {
            Models.Odontologia.covid19 Model = new Models.Odontologia.covid19();
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";

            List<vw_seguimiento_covid19_diario> lista = BusClass.ConsultaListadoseguimientoCovid19();
            lista = lista.Where(l => l.id == ID).ToList();

            foreach (var item in lista)
            {
                if (item.AlertaDiaria != "NO")
                {
                    Model.alertaSioNODiario = "SI";
                }
                else
                {
                    Model.alertaSioNODiario = "NO";

                }
            }


            Model.id_cargue = Convert.ToInt32(ID);
            ViewBag.tificacion = BusClass.ConsultaListadoTipicacionCovid19().ToList();
            ViewBag.tificacion7 = BusClass.ConsultaListadoTipicacion7Covid19().ToList();
            List<ref_covid19_estado_asalud> listaestadoasalud = BusClass.Consultaestadoasaludcovid19().ToList();
            listaestadoasalud = listaestadoasalud.Where(l => l.id_ref_estado_asalud != 1).ToList();
            ViewBag.estadoasalud = listaestadoasalud;
            List<vw_seguimiento_covid19_detalle> listacargueseguimientodetalle = BusClass.ConsultaIdSeguimientoCovid19Detalle(Convert.ToInt32(Model.id_cargue), ref MsgRes).ToList();

            ViewBag.detalle = listacargueseguimientodetalle;


            return PartialView(Model);
        }

        [HttpGet]
        public PartialViewResult _GestionTablerosInterdiarios(Int32? ID)
        {
            Models.Odontologia.covid19 Model = new Models.Odontologia.covid19();
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";

     
            List<vw_seguimiento_covid19_interdiario> lista2 = BusClass.ConsultaListadoseguimientoInterdiarioCovid19();
            lista2 = lista2.Where(l => l.id == ID).ToList();

            foreach (var item in lista2)
            {
                if (item.AlertaDiaria != "NO")
                {
                    Model.alertaSioNOInterdiario = "SI";
                }
                else
                {
                    Model.alertaSioNOInterdiario = "NO";

                }

            }

            Model.id_cargue = Convert.ToInt32(ID);
            ViewBag.tificacion = BusClass.ConsultaListadoTipicacionCovid19().ToList();
            ViewBag.tificacion7 = BusClass.ConsultaListadoTipicacion7Covid19().ToList();
            List<ref_covid19_estado_asalud> listaestadoasalud = BusClass.Consultaestadoasaludcovid19().ToList();
            listaestadoasalud = listaestadoasalud.Where(l => l.id_ref_estado_asalud != 1).ToList();
            ViewBag.estadoasalud = listaestadoasalud;
            List<vw_seguimiento_covid19_detalle> listacargueseguimientodetalle = BusClass.ConsultaIdSeguimientoCovid19Detalle(Convert.ToInt32(Model.id_cargue), ref MsgRes).ToList();

            ViewBag.detalle = listacargueseguimientodetalle;


            return PartialView(Model);
        }



        [HttpPost]
        public ActionResult GestionTableros(String contactabilidad, String observacionseguimiento, Int32? estadoasalud, String actualizarestado, Int32? tipificacionseguimietno,
        Int32? tipificaciondetalleseguimietno, Models.Odontologia.covid19 Model)
        {
            List<cargue_seguimiento_covid19> listacargueseguimiento = BusClass.ConsultaIdSeguimientoCovid19(Convert.ToInt32(Model.id_cargue), ref MsgRes).ToList();
            //List<cargue_seguimiento_covid19_detalle> listacargueseguimientodetalle = BusClass.ConsultaIdSeguimientoCovid19Detalle(Convert.ToInt32(Model.id_cargue), ref MsgRes).ToList();
            List<vw_seguimiento_covid19_ultimo_detalle> listacargueseguimientodetalleultimo = BusClass.ConsultaIdSeguimientoCovid19DetalleUltimo(Convert.ToInt32(Model.id_cargue), ref MsgRes).ToList();

            foreach (var item in listacargueseguimiento)
            {
                Model.documento_paciente = item.documento;
            }


            cargue_seguimiento_covid19_detalle OBJ = new cargue_seguimiento_covid19_detalle();
            cargue_seguimiento_covid19 OBJ2 = new cargue_seguimiento_covid19();
            OBJ.id_cargue = Model.id_cargue;
            OBJ.contactabilidad = contactabilidad;
            OBJ.fecha_gestion = DateTime.Now;
            OBJ.observaciones = observacionseguimiento;
            if (contactabilidad != "NO")
            {
                OBJ.actualiza_estado = actualizarestado;
            }
            else
            {
                OBJ.actualiza_estado = "NO";
            }

            if (actualizarestado == "SI")
            {
                OBJ.id_estado_gestion_asalud = estadoasalud;
            }
            else
            {
                foreach (var item in listacargueseguimientodetalleultimo)
                {
                    OBJ.id_estado_gestion_asalud = item.id_estado_gestion_asalud;
                }
            }

            OBJ.fecha_digita = DateTime.Now;
            OBJ.usuario_digita = SesionVar.UserName;

            Model.InsertarSeguimientoCovid19Detalle(OBJ);
            //Model.ActualizarEstadoSeguimientoCovid19(OBJ2);


            return RedirectToAction("TablerosSeguimiento", "seguimientoCovid19", new { variable = "1", documento = Model.documento_paciente });
        }


        [HttpPost]
        public ActionResult GestionTablerosInterdiarios(String contactabilidad, String observacionseguimiento, Int32? estadoasalud, String actualizarestado, Int32? tipificacionseguimietno,
       Int32? tipificaciondetalleseguimietno, Models.Odontologia.covid19 Model)
        {
            List<cargue_seguimiento_covid19> listacargueseguimiento = BusClass.ConsultaIdSeguimientoCovid19(Convert.ToInt32(Model.id_cargue), ref MsgRes).ToList();
            //List<cargue_seguimiento_covid19_detalle> listacargueseguimientodetalle = BusClass.ConsultaIdSeguimientoCovid19Detalle(Convert.ToInt32(Model.id_cargue), ref MsgRes).ToList();
            List<vw_seguimiento_covid19_ultimo_detalle> listacargueseguimientodetalleultimo = BusClass.ConsultaIdSeguimientoCovid19DetalleUltimo(Convert.ToInt32(Model.id_cargue), ref MsgRes).ToList();

            foreach (var item in listacargueseguimiento)
            {
                Model.documento_paciente = item.documento;
            }


            cargue_seguimiento_covid19_detalle OBJ = new cargue_seguimiento_covid19_detalle();
            cargue_seguimiento_covid19 OBJ2 = new cargue_seguimiento_covid19();
            OBJ.id_cargue = Model.id_cargue;
            OBJ.contactabilidad = contactabilidad;
            OBJ.fecha_gestion = DateTime.Now;
            OBJ.observaciones = observacionseguimiento;
            if (contactabilidad != "NO")
            {
                OBJ.actualiza_estado = actualizarestado;
            }
            else
            {
                OBJ.actualiza_estado = "NO";
            }

            if (actualizarestado == "SI")
            {
                OBJ.id_estado_gestion_asalud = estadoasalud;
            }
            else
            {
                foreach (var item in listacargueseguimientodetalleultimo)
                {
                    OBJ.id_estado_gestion_asalud = item.id_estado_gestion_asalud;
                }
            }

            OBJ.fecha_digita = DateTime.Now;
            OBJ.usuario_digita = SesionVar.UserName;

            Model.InsertarSeguimientoCovid19Detalle(OBJ);
            //Model.ActualizarEstadoSeguimientoCovid19(OBJ2);


            return RedirectToAction("TablerosSeguimiento", "seguimientoCovid19", new { variable = "1", documento = Model.documento_paciente });
        }


        [HttpPost]
        public ActionResult CargueBaseSC(Models.Odontologia.covid19 Model, HttpPostedFileBase file)
        {
            Models.General General = new Models.General();


            String mensaje = "";
            String tipoAlerta = "";

            if (this.Request.Files["file"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                //String strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentos"] + "\\EXCEL\\" + Request.Files["FileUpload1"].FileName;
                string path1 = string.Format("{0}/{1}", ArchivosHelper.ObtenerDirectorioTemporal(), Request.Files["file"].FileName);
                if (System.IO.File.Exists(path1))
                    System.IO.File.Delete(path1);

                Request.Files["file"].SaveAs(path1);

                OleDbConnectionStringBuilder cb = new OleDbConnectionStringBuilder(); cb.DataSource = path1;
                if (Path.GetExtension(path1).ToUpper() == ".XLS")
                {
                    cb.Provider = "Microsoft.ACE.OLEDB.12.0";
                    cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");

                }
                else if (Path.GetExtension(path1).ToUpper() == ".XLSX")
                {
                    cb.Provider = "Microsoft.ACE.OLEDB.12.0";
                    cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
                }
                DataTable dt2 = new DataTable("Seguimiento Covid19");

                try
                {
                    MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                    using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
                    {
                        //Abrimos la conexión
                        conn.Open();
                        using (OleDbCommand cmd = conn.CreateCommand())
                        {
                            //cmd.CommandType = Kendo.Mvc.UI.CommandType.Button;
                            cmd.CommandText = "SELECT * FROM [Seguimiento Covid19$]";

                            //Guardamos los datos en el DataTable
                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            da.Fill(dt2);
                            Model.CargarBaseMDT(dt2, ref MsgRes);
                            conn.Close();

                            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                            {

                                if (Model.conteorepetidas != 0)
                                {
                                    mensaje = "Se ingresaron correctamente" + " " + Model.conteoingresadas + " " + "casos y se actualizaron" + " " + Model.conteorepetidas + " " + "casos que se encontraban en la base";
                                    tipoAlerta = "2";

                                    return Json(new { success = true, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    mensaje = "se ingreso correctamente....";
                                    tipoAlerta = "2";

                                    return Json(new { success = true, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);
                                }


                            }
                            else
                            {
                                mensaje = "*---El archivo no cumple con el formato establecido para el cargue-- - *";
                                tipoAlerta = "3";

                                return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                    mensaje = "*---error -- - *" + ex.Message;
                    tipoAlerta = "3";

                    return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);


                }
            }
            else
            {
                mensaje = "*---DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE -- - *";
                tipoAlerta = "3";

                return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);

            }


        }


        public void GestorConsultas(String opc, MessageResponseOBJ MsgRes, Models.Odontologia.covid19 Model)
        {

            try
            {
                if (opc == "1")
                {
                    List<vw_seguimiento_covid19_descarga_diaria> list = BusClass.ConsultaListadoseguimientodescargaCovid19().ToList();
                    if (SesionVar.ROL == "1")
                    {
                        list = list.Where(l => l.estado != "5").ToList();
                        list = list.Where(l => l.seguimiento == "1. DIARIO").ToList();


                    }
                    else
                    {
                        list = list.Where(l => l.estado != "5").ToList();
                        list = list.Where(l => l.seguimiento == "1. DIARIO").ToList();
                        list = list.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();


                    }

                    if (list.Count != 0)
                    {
                        GridView gv = new GridView();
                        gv.DataSource = list.ToList();
                        gv.DataBind();
                        Response.ClearContent();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment; filename=ConsolidadoSeguimientoDiario.xls");
                        Response.ContentType = "application/ms-excel";
                        Response.Charset = "";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        gv.RenderControl(htw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();

                    }

                }
                else if (opc == "2")
                {
                    List<vw_seguimiento_covid19_descarga_interdiaria> list2 = BusClass.ConsultaListadoseguimientointerdiariodescargaCovid19().ToList();
                    if (SesionVar.ROL == "1")
                    {
                        list2 = list2.Where(l => l.estado == "5").ToList();
                        list2 = list2.Where(l => l.seguimiento == "2. INTERDIARIO").ToList();


                    }
                    else
                    {

                        list2 = list2.Where(l => l.estado == "5").ToList();
                        list2 = list2.Where(l => l.seguimiento == "2. INTERDIARIO").ToList();
                        list2 = list2.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();


                    }
                    if (list2.Count != 0)
                    {

                        StringWriter sw = new StringWriter();
                        sw.WriteLine("\"id\";\"No\";\"tipo_documento\";\"documento\";\"nombres\";\"apellidos\";\"genero\";\"viceprecidencia\";\"Ciudad S/Med\";\"localidad\";\"departamento\";\"regional\";\"fecha_nacimiento\";\"edad\";\"tipo_salud\";\"direccion\";\"telefono_1\";\"telefono_2\";\"correo\";\"incluido_en_sivigila\";\"tipificacion\";\"fecha_ingreso_pais\";\"seguimiento\";\"estado_base\";\"causa_finalizacion_del_seguimiento\";\"observacion_de_caracterizacion\";\"fecha_caracterizacion_registro\";\"nombre_quien_reporta\";\"nombre_auditor_asignado\";\"fecha_cargue\";\"usuario_nombre_cargue\";\"id_seguimiento_detalle\";\"contactabilidad\";\"observaciones\";\"actualiza_estado\";\"id_estado_gestion_asalud\";\"descripcion_estado_gestion_asalud\";\"fecha_digita_gestion\";\"usuario_digita\";\"nombre_digita_gestion\"");
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachment;filename=ConsolidadoInterdiarioCovid19" + DateTime.Now + ".csv");
                        Response.ContentType = "text/csv";

                        foreach (var line in list2)
                        {
                            sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\";\"{27}\";\"{28}\";\"{29}\";\"{30}\";\"{31}\";\"{32}\";\"{33}\";\"{34}\";\"{35}\";\"{36}\";\"{37}\";\"{38}\";\"{39}\"",
                                  line.id,
                                               line.No,
                                               line.tipo_documento,
                                               line.documento,
                                               line.nombres,
                                               line.apellidos,
                                               line.genero,
                                               line.viceprecidencia,
                                               line.descripcion_ciudad,
                                               line.localidad,
                                               line.departamento,
                                               line.regional,
                                               line.fecha_nacimiento,
                                               line.edad,
                                               line.tipo_salud,
                                               line.direccion,
                                               line.telefono_1,
                                               line.telefono_2,
                                               line.correo,
                                               line.incluido_en_sivigila,
                                               line.tipificacion,
                                               line.fecha_ingreso_pais,
                                               line.seguimiento,
                                               line.estado_base,
                                               line.causa_finalizacion_del_seguimiento,
                                               line.observacion_de_caracterizacion,
                                               line.fecha_caracterizacion_registro,
                                               line.nombre_quien_reporta,
                                               line.nombre_auditor_asignado,
                                               line.fecha_cargue,
                                               line.usuario_nombre_cargue,
                                               line.id_seguimiento_detalle,
                                               line.contactabilidad,
                                               line.observaciones,
                                               line.actualiza_estado,
                                               line.id_estado_gestion_asalud,
                                               line.descripcion_estado_gestion_asalud,
                                               line.fecha_digita_gestion,
                                               line.usuario_digita,
                                               line.nombre_digita_gestion

                                               ));
                        }

                        Response.Write(sw.ToString());
                        Response.End();

                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
                    }
                }
                else if (opc == "3")
                {
                    List<vw_seguimiento_covid19_descarga_casos_cerrados> list3 = BusClass.ConsultaListadoseguimientoCasosCerradosdescargaCovid19().ToList();
                    if (SesionVar.ROL == "1")
                    {

                    }
                    else
                    {

                        list3 = list3.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();


                    }
                    if (list3.Count != 0)
                    {

                        StringWriter sw = new StringWriter();
                        sw.WriteLine("\"id\";\"No\";\"tipo_documento\";\"documento\";\"nombres\";\"apellidos\";\"genero\";\"viceprecidencia\";\"Ciudad S/Med\";\"localidad\";\"departamento\";\"regional\";\"fecha_nacimiento\";\"edad\";\"tipo_salud\";\"direccion\";\"telefono_1\";\"telefono_2\";\"correo\";\"incluido_en_sivigila\";\"tipificacion\";\"fecha_ingreso_pais\";\"seguimiento\";\"estado_base\";\"causa_finalizacion_del_seguimiento\";\"observacion_de_caracterizacion\";\"fecha_caracterizacion_registro\";\"nombre_quien_reporta\";\"nombre_auditor_asignado\";\"fecha_cargue\";\"usuario_nombre_cargue\";\"id_seguimiento_detalle\";\"contactabilidad\";\"observaciones\";\"actualiza_estado\";\"id_estado_gestion_asalud\";\"descripcion_estado_gestion_asalud\";\"fecha_digita_gestion\";\"usuario_digita\";\"nombre_digita_gestion\"");
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachment;filename=ConsolidadoCasosCerradosCovid19" + DateTime.Now + ".csv");
                        Response.ContentType = "text/csv";

                        foreach (var line in list3)
                        {
                            sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\";\"{27}\";\"{28}\";\"{29}\";\"{30}\";\"{31}\";\"{32}\";\"{33}\";\"{34}\";\"{35}\";\"{36}\";\"{37}\";\"{38}\";\"{39}\"",
                                  line.id,
                                               line.No,
                                               line.tipo_documento,
                                               line.documento,
                                               line.nombres,
                                               line.apellidos,
                                               line.genero,
                                               line.viceprecidencia,
                                               line.descripcion_ciudad,
                                               line.localidad,
                                               line.departamento,
                                               line.regional,
                                               line.fecha_nacimiento,
                                               line.edad,
                                               line.tipo_salud,
                                               line.direccion,
                                               line.telefono_1,
                                               line.telefono_2,
                                               line.correo,
                                               line.incluido_en_sivigila,
                                               line.tipificacion,
                                               line.fecha_ingreso_pais,
                                               line.seguimiento,
                                               line.estado_base,
                                               line.causa_finalizacion_del_seguimiento,
                                               line.observacion_de_caracterizacion,
                                               line.fecha_caracterizacion_registro,
                                               line.nombre_quien_reporta,
                                               line.nombre_auditor_asignado,
                                               line.fecha_cargue,
                                               line.usuario_nombre_cargue,
                                               line.id_seguimiento_detalle,
                                               line.contactabilidad,
                                               line.observaciones,
                                               line.actualiza_estado,
                                               line.id_estado_gestion_asalud,
                                               line.descripcion_estado_gestion_asalud,
                                               line.fecha_digita_gestion,
                                               line.usuario_digita,
                                               line.nombre_digita_gestion

                                               ));
                        }

                        Response.Write(sw.ToString());
                        Response.End();

                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }


        }

        public JsonResult Consultas(String opc, String opcionrealizar)
        {
            Models.Odontologia.covid19 Model = new Models.Odontologia.covid19();

            if (opc == "1")
            {

                List<vw_seguimiento_covid19_descarga_diaria> list = BusClass.ConsultaListadoseguimientodescargaCovid19().ToList();
                if (SesionVar.ROL == "1")
                {
                    list = list.Where(l => l.estado != "5").ToList();
                    list = list.Where(l => l.seguimiento == "1. DIARIO").ToList();

                }
                else
                {
                    list = list.Where(l => l.estado != "5").ToList();
                    list = list.Where(l => l.seguimiento == "1. DIARIO").ToList();
                    list = list.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();

                }

                if (list.Count != 0)
                {
                    opcionrealizar = "1";
                }
                else
                {
                    opcionrealizar = "2";
                }

            }
            else if (opc == "2")
            {
                List<vw_seguimiento_covid19_descarga_interdiaria> list2 = BusClass.ConsultaListadoseguimientointerdiariodescargaCovid19().ToList();
                if (SesionVar.ROL == "1")
                {
                    list2 = list2.Where(l => l.estado == "5").ToList();
                    list2 = list2.Where(l => l.seguimiento == "2. INTERDIARIO").ToList();


                }
                else
                {

                    list2 = list2.Where(l => l.estado == "5").ToList();
                    list2 = list2.Where(l => l.seguimiento == "2. INTERDIARIO").ToList();
                    list2 = list2.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();


                }
                if (list2.Count != 0)
                {
                    opcionrealizar = "1";
                }
                else
                {
                    opcionrealizar = "2";
                }

            }
            else if (opc == "3")
            {
                List<vw_seguimiento_covid19_descarga_casos_cerrados> list3 = BusClass.ConsultaListadoseguimientoCasosCerradosdescargaCovid19().ToList();
                if (SesionVar.ROL == "1")
                {

                }
                else
                {
                    list3 = list3.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();


                }
                if (list3.Count != 0)
                {
                    opcionrealizar = "1";
                }
                else
                {
                    opcionrealizar = "2";
                }
            }

            return Json(new { opc, opcionrealizar }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Get2()
        {

            Models.Odontologia.covid19 Model = new Models.Odontologia.covid19();

            List<vw_seguimiento_covid19_diario> Lista = new List<vw_seguimiento_covid19_diario>();

            Lista = BusClass.ConsultaListadoseguimientoCovid19().ToList();
            if (SesionVar.IDUser == 1)
            {
                Lista = Lista.Where(l => l.estado != "6").ToList();
                Lista = Lista.Where(l => l.estado != "10").ToList();

            }
            else
            {
                Lista = Lista.Where(l => l.estado != "6").ToList();
                Lista = Lista.Where(l => l.estado != "10").ToList();
                Lista = Lista.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();

            }

            var query = Lista;

            return this.Json(new { query }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult VerificarFecha(String opcionrealizar, DateTime? fecha , Int32? idcargue)
        //{

        //    Models.General General = new Models.General();
        //    Models.Odontologia.covid19 Model = new Models.Odontologia.covid19();

        //    List<vw_seguimiento_covid19_diario> lista = BusClass.ConsultaListadoseguimientoCovid19();
        //    lista = lista.Where(l => l.id == idcargue).ToList();

        //    if (fecha != null)
        //    {
        //        foreach (var item in lista)
        //        {

        //            var fechacv = fecha.Value.ToString("yyyy-MM-dd");

        //            if (item.fecha_ingresar == fechacv)
        //            {
        //                opcionrealizar = "1";
        //            }
        //            else
        //            {
        //                opcionrealizar = "2";
        //            }

        //        }
        //    }
        //    else
        //    {
        //        opcionrealizar = "3";
        //    }




        //    return Json(new { fecha, opcionrealizar }, JsonRequestBehavior.AllowGet);

        //}


    }
}