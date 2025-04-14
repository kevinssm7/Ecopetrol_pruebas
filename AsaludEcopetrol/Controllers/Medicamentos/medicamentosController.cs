using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Helpers;
using AsaludEcopetrol.Models;
using AsaludEcopetrol.Models.Medicamentos;
using Aspose.Cells;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using iTextSharp.text;
using LinqToExcel;
using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime.Caching;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Medicamentos
{
    [SessionExpireFilter]
    public class medicamentosController : Controller
    {
        private Cuentas db = new Cuentas();

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

        #region EVENTOS PRIVADOS

        private void ExportaConsulta1(Models.Medicamentos.ConsultaMed Model)
        {
            if (Model.ListaConsulta.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"descripcion\";\"valor\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=ConsultaMedicamentos" + DateTime.Now + ".csv");
                Response.ContentType = "text/csv";

                foreach (var line in Model.ListaConsulta)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\"",
                                               line.descripcion,
                                               line.valor));



                }
                Response.Write(sw.ToString());

                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }

        }

        private string cargarArchivos(HttpPostedFileBase file, Models.Medicamentos.GestionIndicadores Model)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            sis_configuracion conf = BusClass.GetParametro("rutaDocumentos");
            if (conf != null)
                strRutaDefinitiva = conf.valor_parametro;
            sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
            string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
            string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

            try
            {

                if (file.ContentLength > 0)
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = file.FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    string[] formatos = new string[] { "pdf", "xls", "xlsx", "jpg", "png" };
                    if (Array.IndexOf(formatos, ext) < 0)
                    {
                        strRetorno = "Formato de archivo inválido.";
                    }
                    else
                    {
                        strRetorno = GuardarArchivoMedicamentos(file, Model);

                        string fileExt1 = System.IO.Path.GetExtension(file.FileName);
                        string nombreActual1 = file.FileName;
                        string nombreCarpeta = nombreActual1.Remove(nombreActual1.Length - fileExt1.Length);

                    }
                }


            }
            catch (Exception ex)
            {
                strRetorno = ex.Message;
            }
            return strRetorno;
        }

        private string GuardarArchivoMedicamentos(HttpPostedFileBase file, Models.Medicamentos.GestionIndicadores Model)
        {
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            string strError = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentos"];
            sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
            //Model.CargarInformacion();

            DateTime fecha = DateTime.Now;
            string archivo = string.Empty;
            string codigoddl = string.Empty;
            Int32 id_ref_tipo_documental = 0;

            byte[] ExcelData = null;
            MemoryStream target = new MemoryStream();
            file.InputStream.CopyTo(target);
            ExcelData = target.ToArray();

            Ref_gestion_tipo_documental obj = new Ref_gestion_tipo_documental();
            List<Ref_gestion_tipo_documental> lst = new List<Ref_gestion_tipo_documental>();
            obj.id_tipo_documental = 7;

            lst = Model.ConsultaCodigoGD(obj, ref MsgRes);
            foreach (Ref_gestion_tipo_documental item in lst)
            {
                codigoddl = item.codigo;
                id_ref_tipo_documental = item.id_tipo_documental;
            }

            string ruta = "";
            String carpeta = "MEDICAMENTOS";
            String SubCarpeta = "";

            SubCarpeta = "CALIDAD";
            ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + SubCarpeta + "\\" + Model.nombre_auditado + "\\" + "IdM" + Model.id_indicadores_medicamentos + "\\" + "IdDt" + Model.id_md_ref_indicador + "\\" + codigoddl);

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            if (lst.Count != 0)
            {
                GestionDocumentalMedicamentos objGD = new GestionDocumentalMedicamentos();
                GestionDocumentalMedicamentosCad objGD2 = new GestionDocumentalMedicamentosCad();

                archivo = String.Format("{0}\\{1}_{2}_{3:yyyyMMdd_hhmm}{4}", ruta,
                Model.nombre_auditado.ToString(), codigoddl, fecha, file.FileName);

                objGD2.nombre_auditado = Model.nombre_auditado;
                objGD2.id_indicadores_medicamentos = Model.id_indicadores_medicamentos;
                objGD2.id_md_indicador_detalle = Model.id_md_ref_indicador;
                objGD2.id_tipo_documental = id_ref_tipo_documental;
                objGD2.ruta = Convert.ToString(archivo);
                objGD2.obs = Model.observaciones;
                objGD2.cargue_usuario = SesionVar.UserName;
                objGD2.cargue_fecha = DateTime.Now;
                objGD2.ruta_byte = ExcelData;
                Model.InsertarGestionDocMedCalidad(objGD2, ref MsgRes);

                file.SaveAs(archivo);
            }
            else
            {
                strError = "Error debe seleccionar todas las listas";
            }

            return strError;
        }

        #endregion

        public ActionResult BuscarFactura()
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            return View(Model);
        }

        public ActionResult ControlMedicamentosFac(String variable, int tipo)
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            Model.numero_factura = variable;
            Model.tipo_consulta = tipo;

            return View(Model);
        }

        public ActionResult TableroControlMedicamentos(String variable)
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            Model.numero_formula = variable;

            Model.Cargardatos();

            return View(Model);
        }

        public ActionResult TableroControlMedGeneral(String ciudad)
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();
            List<Managment_md_tablerocontrolResult> list = new List<Managment_md_tablerocontrolResult>();

            list = Model.CuentaFacTableroControl(Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), ref MsgRes);

            if (SesionVar.ROL == "1")
            {
                list = list.OrderBy(x => x.dias).ToList();
            }
            else
            {
                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();
                list = list.OrderBy(x => x.dias).ToList();
            }

            ViewBag.lSTA = list;

            var vlr1 = list.Where(x => x.dias >= 1 && x.dias <= 3).ToList();
            var vlr2 = list.Where(x => x.dias > 3 && x.dias <= 5).ToList();
            var vlr3 = list.Where(x => x.dias > 5).ToList();

            ViewBag.casos1 = vlr1.Count;
            ViewBag.casos2 = vlr2.Count;
            ViewBag.casos3 = vlr3.Count;

            ViewBag.casostotal = vlr3;

            return View();
        }

        public ActionResult DescargarReporteCuentas(DateTime? fechainicial, DateTime? fechafinal, String ciudad, String detalle, String anexo)
        {

            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();
            List<Managment_md_tablerocontrolResult> list = new List<Managment_md_tablerocontrolResult>();

            list = Model.CuentaFacTableroControl(Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), ref MsgRes);

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
            Response.AddHeader("content-disposition", "attachment; filename=ConsolidadoConciliacion.xlsx");
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

        public ActionResult Buscar()
        {
            Models.Medicamentos.GestionIndicadores Model = new Models.Medicamentos.GestionIndicadores();

            Model.ConsultaLista();

            return View(Model);
        }

        public ActionResult BuscarAL()
        {
            Models.Medicamentos.GestionObligaciones Model = new Models.Medicamentos.GestionObligaciones();

            Model.ConsultaLista();

            return View(Model);
        }

        public ActionResult Indicadores()
        {
            Models.Medicamentos.GestionIndicadores Model = new Models.Medicamentos.GestionIndicadores();

            Model.fecha_auditoria = Convert.ToDateTime(DateTime.Now);
            Model.fecha_auditoriaOK = Convert.ToDateTime(DateTime.Now);
            Model.nombre_auditor = Convert.ToString(SesionVar.NombreUsuario);

            return View(Model);
        }

        public ActionResult IndicadoresDetalle(Int32 id, String proveedor)
        {
            Models.Medicamentos.GestionIndicadores Model = new Models.Medicamentos.GestionIndicadores();

            Model.id_indicadores_medicamentos = id;
            Model.nombre_auditado = proveedor;

            return View(Model);

            //Models.Medicamentos.GestionIndicadores Model = new GestionIndicadores();
            //Model.id_indicadores_medicamentos = id;

            //var datos = new GestionIndicadores().GetVwIndicadoresDetalle(Model.id_indicadores_medicamentos);

            //return View(datos);

        }

        public ActionResult IndicadoresDetalle2(Int32 id, String proveedor, String Hallazgo)
        {
            Models.Medicamentos.GestionIndicadores Model = new Models.Medicamentos.GestionIndicadores();

            Model.id_indicadores_medicamentos = id;
            Model.nombre_auditado = proveedor;
            Model.hallazgos = Hallazgo;

            return View(Model);
        }

        public ActionResult Obligaciones()
        {
            Models.Medicamentos.GestionObligaciones Model = new Models.Medicamentos.GestionObligaciones();

            Model.fecha_auditoria = Convert.ToDateTime(DateTime.Now);
            Model.fecha_auditoriaOK = Convert.ToDateTime(DateTime.Now);
            Model.nombre_auditor = Convert.ToString(SesionVar.NombreUsuario);

            return View(Model);
        }

        public ActionResult ObligacionesDetalle(Int32 id, String proveedor, String Hallazgo)
        {
            Models.Medicamentos.GestionObligaciones Model = new Models.Medicamentos.GestionObligaciones();

            Model.id_obligaciones_contractuales = id;
            Model.nombre_auditado = proveedor;
            Model.hallazgos = Hallazgo;


            return View(Model);
        }

        public ActionResult TableroGestion()
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            return View(Model);

        }

        public PartialViewResult Gestion(String ID)
        {
            Models.Medicamentos.GestionTablero Model = new Models.Medicamentos.GestionTablero();

            Model.id_indicadores_medicamentos = Convert.ToInt32(ID);

            return PartialView(Model);

        }

        public PartialViewResult GestionHT(String ID, Int32 tabla)
        {
            Models.Medicamentos.GestionTablero Model = new Models.Medicamentos.GestionTablero();

            Model.id_herramienta_tec_med = Convert.ToInt32(ID);
            Model.tabla = tabla;

            return PartialView(Model);

        }

        public ActionResult ConsultasMedicamentos()
        {
            Models.Medicamentos.ConsultaMed Model = new Models.Medicamentos.ConsultaMed();
            return View(Model);
        }

        [HttpGet]
        public ActionResult ConsultaFiltradaMD(Int32 opc, DateTime DAT1, DateTime DAT2)
        {
            Models.Medicamentos.ConsultaMed Model = new ConsultaMed();

            Model.opc = opc;
            Model.fecha_inicial = DAT1;
            Model.fecha_final = DAT2;

            Model.ListaC(opc);

            Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);

            string tempDesc = string.Empty;
            string tempValor = string.Empty;
            Model.ProductWiseSales(out tempDesc, out tempValor);

            ViewBag.descripcion_List = tempDesc.Trim();
            ViewBag.valor_List = tempValor.Trim();
            ViewBag.consulta = Model.consulta;



            return View(Model);
        }

        [HttpGet]
        public ActionResult ConsultaFiltradaMDOK(Int32 opc, DateTime DAT1, DateTime DAT2)
        {
            Models.Medicamentos.ConsultaMed Model = new ConsultaMed();

            Model.opc = opc;
            Model.fecha_inicial = DAT1;
            Model.fecha_final = DAT2;

            Model.ListaC(opc);

            Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);

            string tempDesc = string.Empty;
            string tempValor = string.Empty;
            Model.ProductWiseSales(out tempDesc, out tempValor);

            ViewBag.descripcion_List = tempDesc.Trim();
            ViewBag.valor_List = tempValor.Trim();
            ViewBag.consulta = Model.consulta;



            return View(Model);
        }

        [HttpGet]
        public ActionResult BarChart()
        {
            try
            {
                string tempMobile = string.Empty;
                string tempProduct = string.Empty;

                ViewBag.MobileCount_List = tempMobile.Trim();
                ViewBag.Productname_List = tempProduct.Trim();

                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PartialViewResult GlosaGeneral(String ID, Decimal valor)
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();


            ViewBag.numero_formula = ID;
            ViewBag.VlrPendiente = valor;
            ViewBag.LIst = Model.GetMedglosa;

            return PartialView(Model);
        }

        [HttpGet]
        public PartialViewResult GlosaDetalle(String ID)
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            Model.numero_formula = ID;

            return PartialView(Model);
        }

        [HttpGet]
        public PartialViewResult GlosaGeneralDetalle(String ID, Decimal valor)
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            ViewBag.numero_formula = ID;
            ViewBag.VlrPendientedtll = valor;
            ViewBag.LIst = Model.GetMedglosa;

            return PartialView(Model);
        }

        [HttpGet]
        public ActionResult CargueBaseMD()
        {
            Models.Medicamentos.Cuentas Model = new Models.Medicamentos.Cuentas();
            ViewData["alerta"] = "";
            ViewData["tipoalerta"] = 1;
            string RolUsuario = SesionVar.ROL;
            ViewBag.idadmin = SesionVar.UserName;
            List<sis_usuario> usuarios = Model.Usuarios().ToList();
            ViewBag.solicitantes = usuarios;

            return View(Model);

        }

        [HttpGet]
        public ActionResult ReportesMD()
        {
            Models.Medicamentos.Cuentas Model = new Models.Medicamentos.Cuentas();

            ViewData["alerta"] = "";
            ViewData["tipoalerta"] = 1;
            string RolUsuario = SesionVar.ROL;
            ViewBag.idadmin = SesionVar.UserName;
            ViewBag.historico = Model.ListReporte;
            ViewBag.consutas2 = Model.ListMDconsulta;

            return View(Model);

        }

        [HttpPost]
        public ActionResult CargueBaseMD(Models.Medicamentos.Cuentas Model, HttpPostedFileBase file)
        {
            Models.General General = new Models.General();

            if (file.ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(file.FileName);
                //String strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentos"] + "\\EXCEL\\" + Request.Files["FileUpload1"].FileName;
                string path1 = string.Format("{0}/{1}", ArchivosHelper.ObtenerDirectorioTemporal(), file.FileName);
                if (System.IO.File.Exists(path1))
                    System.IO.File.Delete(path1);

                //file.SaveAs(path1);

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
                DataTable dt2 = new DataTable("Consolidado Facturación");

                try
                {
                    MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                    using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
                    {
                        //Abrimos la conexión

                        using (OleDbCommand cmd = conn.CreateCommand())
                        {
                            //cmd.CommandType = Kendo.Mvc.UI.CommandType.Button;
                            cmd.CommandText = "SELECT * FROM [Consolidado Facturación$]";

                            //Guardamos los datos en el DataTable
                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            da.Fill(dt2);
                            Model.CargarBaseMDT(dt2, ref MsgRes);

                            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                            {
                                ViewData["alerta"] = "-- -!!!CARGUE EXITOSO!!! -- - *";
                                ViewData["tipoalerta"] = 2;
                            }
                            else
                            {

                                ViewData["alerta"] = "*---EL ARCHIVO NO CUMPLE CON EL FORMATO ESTABLECIDO PARA EL CARGUE -- - *" + MsgRes.DescriptionResponse;
                                ViewData["tipoalerta"] = 3;
                            }
                        }
                        conn.Open();
                        conn.Close();

                    }

                }
                catch (Exception ex)
                {
                    ViewData["alerta"] = "ERROR.." + ex.Message;
                    ViewData["tipoalerta"] = 3;

                }
            }
            else
            {
                ViewData["alerta"] = " *---DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE -- - *";
                ViewData["tipoalerta"] = 3;
            }
            return View(Model);


        }

        [HttpPost]
        public JsonResult SaveBaseMD(HttpPostedFileBase file)
        {

            String mensaje = "";
            String tipoAlerta = "";
            Models.Medicamentos.Cuentas Model = new Cuentas();

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
                DataTable dt2 = new DataTable("Consolidado Facturación");

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
                            cmd.CommandText = "SELECT * FROM [Consolidado Facturación$]";

                            //Guardamos los datos en el DataTable
                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            da.Fill(dt2);
                            Model.CargarBaseMDT(dt2, ref MsgRes);
                            conn.Close();

                            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                            {

                                mensaje = "SE INGRESO CORRECTAMENTE....";
                                tipoAlerta = "2";

                                return Json(new { success = true, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);

                            }
                            else
                            {
                                mensaje = "*---EL ARCHIVO NO CUMPLE CON EL FORMATO ESTABLECIDO PARA EL CARGUE -- - *";
                                tipoAlerta = "3";

                                return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                    mensaje = "*---ERROR -- - *" + ex.Message;
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

        [HttpPost]
        public JsonResult BuscarMasivo(Models.Medicamentos.Cuentas Model)
        {
            String mensaje = "";
            String tipoAlerta = "";

            List<ManagmentocargueconsolidadoResult> list = new List<ManagmentocargueconsolidadoResult>();

            list = Model.CuentaConsolidadoMD(Model.Numero_factura, Model.Numero_formula, Model.fecha_inicial, Model.fecha_final, ref MsgRes);

            //foreach (var item in list)
            //{
            //    result += "<tr>";
            //    result += "<td>" + item.id_consolidado_facturacion + "</td>";
            //    result += "<td>" + item.numero_factura + "</td>";
            //    result += "<td>" + item.numero_formula + "</td>";
            //    result += "<td>" + item.doc_beneficiario + "</td>";
            //    result += "<td>" + item.nombre_beneficiario + "</td>";

            //    result += "</tr>";
            //}


            //mensaje = "SE DESCARGO CORRECTAMENTE....";
            //tipoAlerta = "2";
            //return Json(result);

            if (list.Count != 0)
            {

                GridView gv = new GridView();
                gv.DataSource = list.ToList();
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Marklist.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();



                mensaje = "SE DESCARGO CORRECTAMENTE....";
                tipoAlerta = "2";
                return Json(new { success = true, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                mensaje = "*---SIN REGISTROS ---*";
                tipoAlerta = "3";

                return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);

            }

        }

        public void CrearConsolidadottosExcel(Models.Medicamentos.Cuentas Model)
        {


            List<ManagmentocargueconsolidadoResult> list = new List<ManagmentocargueconsolidadoResult>();

            Model.fecha_inicial = Convert.ToDateTime("01/01/1900");
            Model.fecha_final = Convert.ToDateTime("01/01/2020");

            list = Model.CuentaConsolidadoMD("", "", Model.fecha_inicial, Model.fecha_final, ref MsgRes);



            //GridView gv = new GridView();
            //gv.DataSource = list.ToList();
            //gv.DataBind();
            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment; filename=ConsolidadoFacturas.xls");
            //Response.ContentType = "application/ms-excel";
            //Response.Charset = "";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //gv.RenderControl(htw);
            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();


        }

        public FileContentResult Download(Models.Medicamentos.Cuentas Model)
        {
            List<ManagmentocargueconsolidadoResult> list = new List<ManagmentocargueconsolidadoResult>();

            Model.fecha_inicial = Convert.ToDateTime("01/01/1900");
            Model.fecha_final = Convert.ToDateTime("01/01/2020");

            list = Model.CuentaConsolidadoMD("", "", Model.fecha_inicial, Model.fecha_final, ref MsgRes);


            var fileDownloadName = String.Format("Consolidado.xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


            // Pass your ef data to method
            ExcelPackage package = GenerateExcelFile(list.ToList());

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        public FileContentResult Downloadtxt(Models.Medicamentos.Cuentas Model)
        {
            List<ManagmentocargueconsolidadoResult> list = new List<ManagmentocargueconsolidadoResult>();

            Model.fecha_inicial = Convert.ToDateTime("01/01/1900");
            Model.fecha_final = Convert.ToDateTime("01/01/2020");

            list = Model.CuentaConsolidadoMD("", "", Model.fecha_inicial, Model.fecha_final, ref MsgRes);

            Int32 numeroItems = list.Count();
            StringWriter sw = new StringWriter();
            using (sw)
            {
                for (Int32 i = 0; i < numeroItems; i++)
                {
                    sw.WriteLine(list[i].numero_formula + "," + list[i].numero_factura + "," + list[i].regional + "," + list[i].ciudad + "," + list[i].tipo_documento + "," + list[i].doc_beneficiario + "," + list[i].nombre_beneficiario + "," + list[i].presentacion + "," + list[i].cat_entregada + "," + list[i].cod_comercial + "," + list[i].nombre_comercial);
                }

            }

            String contenido = sw.ToString();
            String NombreArchivo = "ListadoMasivo" + DateTime.Now.Date;
            String ExtensionArchivo = "txt";
            return File(new System.Text.UTF8Encoding().GetBytes(contenido), "text/" + ExtensionArchivo, NombreArchivo + "." + ExtensionArchivo);
        }

        private static ExcelPackage GenerateExcelFile(List<ManagmentocargueconsolidadoResult> datasource)
        {

            ExcelPackage pck = new ExcelPackage();

            //Create the worksheet 
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Cargue Masivo");

            // Sets Headers
            ws.Cells[1, 1].Value = "Numero Formula";
            ws.Cells[1, 2].Value = "Numero Factura";
            ws.Cells[1, 3].Value = "Regional";
            ws.Cells[1, 4].Value = "Ciudad";
            ws.Cells[1, 5].Value = "Tipo Documento";
            ws.Cells[1, 6].Value = "Documento";
            ws.Cells[1, 7].Value = "Nombre";
            ws.Cells[1, 8].Value = "Presentación";
            ws.Cells[1, 9].Value = "Cat entregada";
            ws.Cells[1, 10].Value = "Cod comercial";
            ws.Cells[1, 11].Value = "Nombre comercial";
            ws.Cells[1, 12].Value = "Laboratorio";

            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).numero_formula;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).numero_factura;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).regional;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).ciudad;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).tipo_documento;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).doc_beneficiario;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).nombre_beneficiario;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).presentacion;
                ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).cat_entregada;
                ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).cod_comercial;
                ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).nombre_comercial;
                ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).nombre_laboratorio;

            }

            // Format Header of Table
            using (ExcelRange rng = ws.Cells["A1:L1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid; //Set Pattern for the background to Solid 
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray); //Set color to DarkGray 
                rng.Style.Font.Color.SetColor(Color.White);
            }
            return pck;
        }

        public void CrearConsolidadottosPDF(Models.Medicamentos.Cuentas Model)
        {

            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoMD.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            List<ManagmentocargueconsolidadoResult> lst = new List<ManagmentocargueconsolidadoResult>();
            lst = Model.CuentaConsolidadoMD("", "", Model.fecha_inicial, Model.fecha_final, ref MsgRes);
            string fileName = "pruebas";
            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetConsolidadoCM", lst);

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

                //RUTA REPORTE
            }

        }

        public void CrearConsolidadottosTXT(Models.Medicamentos.Cuentas Model)
        {



        }

        [HttpPost]
        public ActionResult Buscar(Models.Medicamentos.GestionIndicadores Model)
        {
            String proveedor = Model.nombre_auditor;

            Model.IndicadoresProvvedor(proveedor);
            Model.ConsultaLista();

            return View(Model);
        }

        [HttpPost]
        public ActionResult BuscarAL(Models.Medicamentos.GestionObligaciones Model)
        {
            String proveedor = Model.nombre_auditor;

            Model.ObligacionesProveedor(proveedor);
            Model.ConsultaLista();

            return View(Model);
        }

        [HttpPost]
        public ActionResult BuscarFactura(Models.Medicamentos.GestionMedicamentos Model)
        {
            return RedirectToAction("TableroControlMedicamentos", "medicamentos", new { variable = Model.numero_formula });

        }

        public JsonResult BuscarProducto(string nombre)
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();
            return Json(Model.Productos2.ToList());
        }

        [HttpPost]
        public ActionResult GlosaGeneral(Models.Medicamentos.GestionMedicamentos Model)
        {

            try
            {
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";

                if (Model.motivo_glosa != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "MOTIVO GLOSA";
                    Conteo = Conteo + 1;
                }

                if (Model.valor_glosa != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "VALOR GLOSA";
                    Conteo = Conteo + 1;
                }

                if (Model.observaciones != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "OBSERVACIONES";
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
                    Model.OBJGlosaDetalle.numero_formula = Model.numero_formula;
                    Model.OBJGlosaDetalle.motivo_glosa = Model.motivo_glosa;
                    Model.OBJGlosaDetalle.valor_glosa = Model.valor_glosa;
                    Model.OBJGlosaDetalle.observaciones = Model.observaciones;
                    Model.OBJGlosaDetalle.fecha_digita = Convert.ToDateTime(DateTime.Now);
                    Model.OBJGlosaDetalle.usuario_digita = Convert.ToString(SesionVar.UserName);
                    Model.OBJGlosaDetalle.tipo_glosa = Convert.ToString("G");

                    Model.InsertarGlosaDetalleMD(ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        return RedirectToAction("TableroControlMedicamentos", "medicamentos", new { variable = Model.numero_formula });
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR AL INGRESO!");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
                }



            }
            catch
            {
                ModelState.AddModelError("", "ERROR...");
            }
            return PartialView(Model);
        }

        [HttpPost]
        public JsonResult SaveGlosaGeneral(Models.Medicamentos.GestionMedicamentos Model)
        {

            String mensaje = "";


            Model.OBJGlosaDetalle.numero_formula = Model.numero_formula;
            Model.OBJGlosaDetalle.motivo_glosa = Model.motivo_glosa;
            Model.OBJGlosaDetalle.valor_glosa = Model.valor_glosa;
            Model.OBJGlosaDetalle.observaciones = Model.observaciones;
            Model.OBJGlosaDetalle.fecha_digita = Convert.ToDateTime(DateTime.Now);
            Model.OBJGlosaDetalle.usuario_digita = Convert.ToString(SesionVar.UserName);
            Model.OBJGlosaDetalle.tipo_glosa = Convert.ToString("G");

            Model.InsertarGlosaDetalleMD(ref MsgRes);

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

        [HttpPost]
        public ActionResult GlosaGeneralDetalle(Models.Medicamentos.GestionMedicamentos Model)
        {

            try
            {
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";

                if (Model.motivo_glosa != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "MOTIVO GLOSA";
                    Conteo = Conteo + 1;
                }

                if (Model.valor_glosa != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "VALOR GLOSA";
                    Conteo = Conteo + 1;
                }

                if (Model.observaciones != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "OBSERVACIONES";
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
                    Model.OBJGlosaDetalle.numero_formula = Model.numero_formula;
                    Model.OBJGlosaDetalle.motivo_glosa = Model.motivo_glosa;
                    Model.OBJGlosaDetalle.valor_glosa = Model.valor_glosa;
                    Model.OBJGlosaDetalle.observaciones = Model.observaciones;
                    Model.OBJGlosaDetalle.fecha_digita = Convert.ToDateTime(DateTime.Now);
                    Model.OBJGlosaDetalle.usuario_digita = Convert.ToString(SesionVar.UserName);
                    Model.OBJGlosaDetalle.tipo_glosa = Convert.ToString("D");

                    Model.InsertarGlosaDetalleMD(ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        return RedirectToAction("TableroControlMedicamentos", "medicamentos", new { variable = Model.numero_formula });
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR AL INGRESO!");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
                }



            }
            catch
            {
                ModelState.AddModelError("", "ERROR...");
            }
            return PartialView(Model);
        }

        [HttpPost]
        public JsonResult SaveGlosaGeneralDetalle(Models.Medicamentos.GestionMedicamentos Model)
        {

            String mensaje = "";


            Model.OBJGlosaDetalle.numero_formula = Model.numero_formula;
            Model.OBJGlosaDetalle.motivo_glosa = Model.motivo_glosa;
            Model.OBJGlosaDetalle.valor_glosa = Model.valor_glosa;
            Model.OBJGlosaDetalle.observaciones = Model.observaciones;
            Model.OBJGlosaDetalle.fecha_digita = Convert.ToDateTime(DateTime.Now);
            Model.OBJGlosaDetalle.usuario_digita = Convert.ToString(SesionVar.UserName);
            Model.OBJGlosaDetalle.tipo_glosa = Convert.ToString("D");

            Model.InsertarGlosaDetalleMD(ref MsgRes);

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

        public ActionResult EliminarGlosa(String id, String formula)
        {

            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            Model.numero_formula = formula;

            if (id != null)
            {
                Model.EliminarGlosa(Convert.ToInt32(id), ref MsgRes);

                return RedirectToAction("TableroControlMedicamentos", "medicamentos", new { variable = Model.numero_formula });
            }
            else
            {
                ModelState.AddModelError("", "ERROR...");
            }
            return View(Model);

        }

        [HttpPost]
        public ActionResult TableroControlMedicamentos(Models.Medicamentos.GestionMedicamentos Model)
        {

            Model.OBJGlosa.id_ref_md_resultado_auditoria = Model.resultado_auditoria;
            Model.OBJGlosa.numero_formula = Model.numero_formula;
            Model.OBJGlosa.valor_total_glosa = Model.VlrGlosa;
            Model.OBJGlosa.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
            Model.OBJGlosa.usuario_ingreso = Convert.ToString(SesionVar.UserName);



            Model.InsertarGlosaMD(ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                if (Model.resultado_auditoria != 1)
                {
                    CrearCartaAuditoriaMed(Model.numero_formula);
                    //return RedirectToAction("CrearCartaAuditoriaMed", "medicamentos", new { id = Model.numero_formula });
                    return RedirectToAction("BuscarFactura", "medicamentos");
                }
                else
                {
                    return RedirectToAction("BuscarFactura", "medicamentos");
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR AL INGRESO!");
            }


            return View(Model);
        }

        [HttpPost]
        public ActionResult Indicadores(Models.Medicamentos.GestionIndicadores Model)
        {
            try
            {
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";

                if (Model.fecha_auditoriaOK != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA AUDITORIA";
                    Conteo = Conteo + 1;
                }

                if (Model.ciudad != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "CIUDAD";
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
                    Model.OBJIndicadores.fecha_auditoria = Model.fecha_auditoriaOK;
                    Model.OBJIndicadores.nombre_auditor = Convert.ToString(SesionVar.NombreUsuario);
                    Model.OBJIndicadores.ciudad = Convert.ToString(Model.ciudad);
                    Model.OBJIndicadores.nombre_auditado = Model.nombre_auditado;
                    Model.OBJIndicadores.nombre_farmacia = Model.nombre_farmacia;
                    Model.OBJIndicadores.direccion_farmacia = Model.direccion_farmacia;
                    Model.OBJIndicadores.telefono_farmacia = Model.telefono_farmacia;
                    Model.OBJIndicadores.matricula_mercantil = Model.matricula_mercantil;
                    Model.OBJIndicadores.unis = Model.unis;
                    Model.OBJIndicadores.coordinacion = Model.fuerza;
                    Model.OBJIndicadores.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    Model.OBJIndicadores.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                    Int32 id = Model.InsertarIndicador(ref MsgRes);
                    Model.id_indicadores_medicamentos = id;
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        List<Managment_md_Ref_indicadorResult> list = new List<Managment_md_Ref_indicadorResult>();
                        list = Model.ListaIndicadoresActivos(Model.id_indicadores_medicamentos);

                        foreach (var item in list)
                        {
                            Model.OBJDetallle.id_indicadores_medicamentos = Model.id_indicadores_medicamentos;
                            Model.OBJDetallle.id_md_ref_indicador = item.id_md_ref_indicador;
                            Model.OBJDetallle.peso = item.peso;
                            Model.OBJDetallle.valor = 1;
                            Model.OBJDetallle.resultado = 1 * item.peso;
                            Model.OBJDetallle.observaciones = "";

                            Model.InsertarIndicadorDetalle(ref MsgRes);
                        }

                        return RedirectToAction("IndicadoresDetalle2", "medicamentos", new { id = Model.id_indicadores_medicamentos, proveedor = Model.nombre_auditado });
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR AL INGRESO!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
                }



            }
            catch
            {

            }
            return View(Model);

        }

        [HttpPost]
        public ActionResult Obligaciones(Models.Medicamentos.GestionObligaciones Model)
        {
            try
            {
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";

                if (Model.fecha_auditoriaOK != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA AUDITORIA";
                    Conteo = Conteo + 1;
                }

                if (Model.ciudad != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "CIUDAD";
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
                    Model.OBJObligacionesContractuales.fecha_auditoria = Model.fecha_auditoriaOK;
                    Model.OBJObligacionesContractuales.nombre_auditor = Model.nombre_auditor;
                    Model.OBJObligacionesContractuales.ciudad = Convert.ToString(Model.ciudad);
                    Model.OBJObligacionesContractuales.nombre_auditado = Model.nombre_auditado;
                    Model.OBJObligacionesContractuales.nombre_punto_dispensacion = Model.nombre_farmacia;
                    Model.OBJObligacionesContractuales.direccion_farmacia = Model.direccion_farmacia;
                    Model.OBJObligacionesContractuales.telefono_farmacia = Model.telefono_farmacia;
                    Model.OBJObligacionesContractuales.matricula_mercantil = Model.matricula_mercantil;
                    Model.OBJObligacionesContractuales.unis = Model.unis;
                    Model.OBJObligacionesContractuales.coordinacion = Model.fuerza;
                    Model.OBJObligacionesContractuales.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    Model.OBJObligacionesContractuales.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                    Int32 id = Model.InsertarObligaciones(ref MsgRes);
                    Model.id_obligaciones_contractuales = id;
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        List<Managment_md_Ref_obligacionesResult> list = new List<Managment_md_Ref_obligacionesResult>();
                        list = Model.ListaObligacionesActivos(Model.id_obligaciones_contractuales);

                        foreach (var item in list)
                        {
                            Model.OBJDetallle.id_obligaciones_contractuales = Model.id_obligaciones_contractuales;
                            Model.OBJDetallle.id_md_ref_obligaciones = item.id_md_ref_obligaciones;
                            Model.OBJDetallle.peso = item.peso;
                            Model.OBJDetallle.valor = 1;
                            Model.OBJDetallle.resultado = 1 * item.peso;
                            Model.OBJDetallle.observaciones = "";

                            Model.InsertarObligacionesDetalle(ref MsgRes);
                        }

                        return RedirectToAction("ObligacionesDetalle", "medicamentos", new { id = Model.id_obligaciones_contractuales, proveedor = Model.nombre_auditado });
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR AL INGRESO!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
                }



            }
            catch
            {

            }
            return View(Model);

        }

        [HttpPost]
        public ActionResult IndicadoresDetalle(Int32? ID, String hallazgo)
        {

            Models.Medicamentos.GestionIndicadores Model = new GestionIndicadores();

            vw_total_md_detalle OBJ = new vw_total_md_detalle();

            OBJ = Model.Total_DetalleIndMD(ID.Value);


            Decimal VALOR = OBJ.sum_resultado.Value / 74 * 100;

            Model.resultado = VALOR;

            Model.OBJIndicadores.id_indicadores_medicamentos = ID.Value;
            Model.OBJIndicadores.resultado = Model.resultado;
            Model.OBJIndicadores.hallazgos = Model.hallazgos;
            Model.OBJIndicadores.estado = 1;

            Model.ActualizarIndicadoresMD(ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                CrearPdfCartaIndicadoresMD("CartaIndicaMD   " + ID, ID.Value);
            }
            else
            {
                ModelState.AddModelError("", "ERROR AL INGRESO!");
            }


            return View(Model);
        }

        [HttpPost]
        public ActionResult IndicadoresDetalle2(Int32? ID, String hallazgo)
        {

            Models.Medicamentos.GestionIndicadores Model = new GestionIndicadores();

            vw_total_md_detalle OBJ = new vw_total_md_detalle();

            OBJ = Model.Total_DetalleIndMD(ID.Value);


            Decimal VALOR = OBJ.sum_resultado.Value / 74 * 100;

            Model.resultado = VALOR;

            Model.OBJIndicadores.id_indicadores_medicamentos = ID.Value;
            Model.OBJIndicadores.resultado = Model.resultado;
            Model.OBJIndicadores.hallazgos = Model.hallazgos;
            Model.OBJIndicadores.estado = 1;

            Model.ActualizarIndicadoresMD(ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return RedirectToAction("CrearPdfCartaIndicadoresMD2", "medicamentos", new { fileName = "CartaIndicaMD", id = Model.id_indicadores_medicamentos });
            }
            else
            {
                ModelState.AddModelError("", "ERROR AL INGRESO!");
            }


            return View(Model);
        }

        [HttpPost]
        public ActionResult Gestion(Models.Medicamentos.GestionTablero Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_limite_respuesta != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "FECHAS";
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

                Model.OBJGestion.id_indicadores_medicamentos = Model.id_indicadores_medicamentos;
                Model.OBJGestion.tipo_hallazgo = Model.tipo_hallazgo;
                Model.OBJGestion.soporte = Model.soporte;
                Model.OBJGestion.plan_mejora = Model.plan_mejora;
                Model.OBJGestion.fecha_limite_respuesta = Model.fecha_limite_respuesta;
                Model.OBJGestion.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                Model.OBJGestion.usuario_ingreso = Convert.ToString(SesionVar.UserName);


                Model.InsertarIndicadorGestion(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    md_indicadores obj = new md_indicadores();
                    if (Model.fecha_limite_respuesta < Convert.ToDateTime(DateTime.Now))
                    {
                        obj.estado = 3;
                    }
                    else
                    {
                        obj.estado = 4;
                    }

                    obj.id_indicadores_medicamentos = Model.id_indicadores_medicamentos;

                    Model.ActualizarIndicadoresMDEstado(obj, ref MsgRes);

                    return RedirectToAction("TableroGestion", "medicamentos");
                }
                else
                {
                    ModelState.AddModelError("", "ERROR AL INGRESO!");
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            return View(Model);
        }


        [HttpPost]
        public ActionResult GestionHT(Models.Medicamentos.GestionTablero Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_limite_respuesta != null)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "FECHAS";
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
                md_herramienta_tec_gestion OBJGestion = new md_herramienta_tec_gestion();

                OBJGestion.id_herramienta_tec_med = Model.id_herramienta_tec_med;
                OBJGestion.tabla = Model.tabla;
                OBJGestion.tipo_hallazgo = Model.tipo_hallazgo;
                OBJGestion.soporte = Model.soporte;
                OBJGestion.plan_mejora = Model.plan_mejora;
                OBJGestion.fecha_limite_respuesta = Model.fecha_limite_respuesta;
                OBJGestion.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                OBJGestion.usuario_ingreso = Convert.ToString(SesionVar.UserName);


                Model.InsertarHerramientaGestion(OBJGestion, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("TableroGestion", "medicamentos");
                }
                else
                {
                    ModelState.AddModelError("", "ERROR AL INGRESO!");
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsultasMedicamentos(Models.Medicamentos.ConsultaMed Model)
        {

            Model.opc = Model.opc;
            Model.fecha_inicial = Model.fecha_inicialok;
            Model.fecha_final = Model.fecha_finalok;

            if (Model.fecha_final != null && Model.fecha_inicial != null)
            {
                Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);
                return RedirectToAction("ConsultaFiltradaMD", "medicamentos", new { opc = Model.opc, DAT1 = Model.fecha_inicial, DAT2 = Model.fecha_final });
            }
            else
            {

                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!");
                return View(Model);
            }
        }

        [HttpGet]
        public ActionResult ConsultaBaseMD()
        {
            Models.Medicamentos.Cuentas Model = new Models.Medicamentos.Cuentas();
            ViewData["alerta"] = "";
            ViewData["tipoalerta"] = 1;
            string RolUsuario = SesionVar.ROL;
            ViewBag.idadmin = SesionVar.UserName;
            ViewBag.historico = Model.ListReporte;

            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsolidadosCargueMasivo(Models.Medicamentos.ConsultaMed Model)
        {

            List<ManagmentocargueconsolidadoResult> list = new List<ManagmentocargueconsolidadoResult>();
            //list = Model.CuentaConsolidadoMD("", "", Model.fecha_inicial.Value, Model.fecha_final.Value, ref MsgRes);

            //ViewBag.Lis = list;
            //ViewBag.total = list.Count();

            Model.fecha_inicial = Convert.ToDateTime(DateTime.Now);
            Model.fecha_final = Convert.ToDateTime(DateTime.Now);

            ViewBag.fecha_inicial = Model.fecha_inicialM;
            ViewBag.fecha_final = Model.fecha_finalM;
            ViewBag.historico = list;
            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsolidadosA(Models.Medicamentos.ConsultaMed Model)
        {

            Model.opc = Model.opc;
            Model.fecha_inicial = Model.fecha_inicialA;
            Model.fecha_final = Model.fecha_finalA;

            //Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);

            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsolidadosB(Models.Medicamentos.ConsultaMed Model)
        {

            Model.opc = Model.opc;
            Model.fecha_inicial = Model.fecha_inicialB;
            Model.fecha_final = Model.fecha_finalB;

            //Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);


            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsolidadosC(Models.Medicamentos.ConsultaMed Model)
        {

            Model.opc = Model.opc;
            Model.fecha_inicial = Model.fecha_inicialC;
            Model.fecha_final = Model.fecha_finalC;

            //Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);

            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsolidadosD(Models.Medicamentos.ConsultaMed Model)
        {

            Model.opc = Model.opc;
            Model.fecha_inicial = Model.fecha_inicialD;
            Model.fecha_final = Model.fecha_finalD;

            //Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);

            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsolidadosE(Models.Medicamentos.ConsultaMed Model)
        {

            Model.opc = Model.opc;
            Model.fecha_inicial = Model.fecha_inicialE;
            Model.fecha_final = Model.fecha_finalE;

            //Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);

            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsolidadosF(Models.Medicamentos.ConsultaMed Model)
        {

            Model.opc = Model.opc;
            Model.fecha_inicial = Model.fecha_inicialF;
            Model.fecha_final = Model.fecha_finalF;

            //Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);

            return View(Model);

        }

        [HttpPost]
        public ActionResult ConsolidadosG(Models.Medicamentos.ConsultaMed Model)
        {
            Model.GetpuntoControl();

            return View(Model);

        }

        [HttpPost]
        public ActionResult ConsultaFiltradaMD(Models.Medicamentos.ConsultaMed Model)
        {

            Model.opc = Model.opc;
            Model.fecha_inicial = Model.fecha_inicial;
            Model.fecha_final = Model.fecha_final;

            Model.CuentaFechaCargue(Model.opc, Model.fecha_inicial.Value, Model.fecha_final.Value);


            return View(Model);
        }

        public JsonResult Get(Int32? id)
        {

            Models.Medicamentos.GestionIndicadores Model = new GestionIndicadores();
            Model.id_indicadores_medicamentos = id.Value;

            List<Managment_md_Ref_indicadorResult> Lista = new List<Managment_md_Ref_indicadorResult>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefIndicadores(Model.id_indicadores_medicamentos);

            return Json(Lista.ToList(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Save(Models.Medicamentos.GestionIndicadores Model)
        {

            List<md_indicadores_detalle> List = new List<md_indicadores_detalle>();
            List = Model.GetIndicadoresDetalleID(Model.id_md_ref_indicador, Model.id_indicadores_medicamentos);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {
                    Model.OBJDetallle.id_md_indicador_detalle = item.id_md_indicador_detalle;
                    Model.OBJDetallle.peso = Model.peso;
                    Model.OBJDetallle.valor = Model.valor;
                    Model.OBJDetallle.resultado = Model.valor * Model.peso;
                    Model.OBJDetallle.observaciones = Model.observaciones;


                    Model.ActualizarIndicadoresMedicamentos(ref MsgRes);
                }

            }

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }


        }

        public JsonResult Save2(HttpPostedFileBase file, String nombre_auditado, String id_md_ref_indicador, String id_indicadores_medicamentos, String observaciones)
        {
            Models.Medicamentos.GestionIndicadores Model = new GestionIndicadores();

            if (file.ContentLength > 0)
            {
                Model.id_indicadores_medicamentos = Convert.ToInt32(id_indicadores_medicamentos);
                Model.id_md_ref_indicador = Convert.ToInt32(id_md_ref_indicador);
                Model.observaciones = observaciones;
                Model.nombre_auditado = nombre_auditado;

                string strInfo = string.Empty;

                strInfo = cargarArchivos(file, Model);

                if (string.IsNullOrEmpty(strInfo))
                {
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }

            }
            else
            {
                ModelState.AddModelError("", "Error.. Debe Seleccionar el archivo...!");
                return Json(new { error = true });
            }



        }

        public JsonResult GetCascadeCiudades(string regional, Models.Medicamentos.GestionIndicadores Model)
        {
            Model.ConsultaLista(1, "", ref MsgRes);

            return Json(Model.ListCiudades.Select(p => new { id_ref_ciudades = p.id_ref_ciudades, nombre = p.nombre }), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult CrearPdfCartaIndicadoresMD2(Int32 id_indicadores_medicamentos)
        //{
        //    //RUTA REPORTE

        //    string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptIndicadoresMd.rdlc");

        //    //CONEXION BD  PARA CARGAR DATASET

        //    Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

        //    List<ManagmentReporIndicadorMDResult> lst = new List<ManagmentReporIndicadorMDResult>();

        //    lst = Model.ReporteIndicadores(Convert.ToInt32(id_indicadores_medicamentos));
        //    //ASIGNAICON  DATASET A REPORT
        //    Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetReportMD", lst);

        //    // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
        //    Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
        //    viewer.ProcessingMode = ProcessingMode.Local;
        //    viewer.LocalReport.ReportPath = rPath;
        //    viewer.LocalReport.DataSources.Clear();
        //    viewer.LocalReport.DataSources.Add(rds);

        //    if (lst.Count != 0)
        //    {
        //        //CONTROL EXCEPCION
        //        try
        //        {
        //            viewer.LocalReport.Refresh();

        //            string mimeType;
        //            string encoding;
        //            string fileNameExtension;
        //            string[] streams;
        //            Microsoft.Reporting.WebForms.Warning[] warnings;
        //            byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        //            //RETORNO PDF

        //            this.Response.Clear();
        //            this.Response.ContentType = "application/pdf";
        //            this.Response.AddHeader("Content-disposition", "attachment; filename=" + "CartaIndicaMD" + ".pdf");
        //            this.Response.BinaryWrite(pdfContent);
        //            this.Response.End();

        //        }
        //        catch (Exception ex)
        //        {
        //            MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
        //            MsgRes.DescriptionResponse = ex.Message;
        //        }
        //    }

        //    return View(Model);
        //}

        public void CrearPdfCartaIndicadoresMD2(Int32 id_indicadores_medicamentos)
        {
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptIndicadoresMd.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<ManagmentReporIndicadorMDResult> lst = new List<ManagmentReporIndicadorMDResult>();

            lst = Model.ReporteIndicadores(Convert.ToInt32(id_indicadores_medicamentos));
            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetReportMD", lst);

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

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    this.Response.Clear();
                    this.Response.ContentType = "application/pdf";
                    this.Response.AddHeader("Content-disposition", "attachment; filename=" + "CartaIndicaMD" + ".pdf");
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

        public void CrearCartaAuditoriaMed(String id)
        {
            //RUTA REPORTE
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "ReporteAuditoriaMedicamentos.rdlc");

            //CONEXION BD  PARA CARGAR DATASET
            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<ManagmentFacMedicamentosResult> lst = new List<ManagmentFacMedicamentosResult>();
            lst = Model.CuentaFacMedicamentos("", id, 1, ref MsgRes);

            List<vw_glosa_medicamentos> lstg = new List<vw_glosa_medicamentos>();
            lstg = Model.ConsultaGlosa(id);

            string filename = "CartaAuditoriaM" + lst.FirstOrDefault().numero_formula;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetDatosFormula", lst);
            Microsoft.Reporting.WebForms.ReportDataSource rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetGlosasMD", lstg);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);
            viewer.LocalReport.DataSources.Add(rds2);

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

        public JsonResult Get2(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.GestionIndicadores Model = new GestionIndicadores();
            Model.id_indicadores_medicamentos = id.Value;

            List<Managment_md_Ref_indicadorResult> Lista = new List<Managment_md_Ref_indicadorResult>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefIndicadores(Model.id_indicadores_medicamentos);

            var query = Lista;
            List<Managment_md_Ref_indicadorResult> records = new List<Managment_md_Ref_indicadorResult>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get3(Int32 id, int id2, int? page, int? limit)
        {

            Models.Medicamentos.GestionIndicadores Model = new GestionIndicadores();
            Model.id_indicadores_medicamentos = id;

            List<vw_gestionDocumentalCad> Lista = new List<vw_gestionDocumentalCad>();

            //Model.LlenaLista();
            Lista = Model.GestionDocumentalMedCalidad(id, id2);

            var query = Lista;
            List<vw_gestionDocumentalCad> records = new List<vw_gestionDocumentalCad>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConsolidado(String id, String id2, int? page, int? limit)
        {

            Models.Medicamentos.ConsultaMed Model = new ConsultaMed();

            Model.fecha_inicial = Convert.ToDateTime("01/01/1900");
            Model.fecha_final = Convert.ToDateTime("01/01/2020");

            List<ManagmentocargueconsolidadoResult> list = new List<ManagmentocargueconsolidadoResult>();

            list = Model.CuentaConsolidadoMD("", "", Model.fecha_inicial.Value, Model.fecha_final.Value);


            var query = list;
            List<ManagmentocargueconsolidadoResult> records = new List<ManagmentocargueconsolidadoResult>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);

        }

        //public JsonResult GetConsolidado(String id, String id2, int? page, int? limit)
        //{
        //    string result = "";
        //    Models.Medicamentos.ConsultaMed Model = new ConsultaMed();

        //    List<ManagmentocargueconsolidadoResult> list = new List<ManagmentocargueconsolidadoResult>();
        //    list = Model.CuentaConsolidadoMD("", "", Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(DateTime.Now));

        //    //foreach (var item in list)
        //    //{
        //    //    result += "<tr>";
        //    //    result += "<td>" + item.id_consolidado_facturacion + "</td>";
        //    //    result += "<td>" + item.numero_factura + "</td>";
        //    //    result += "<td>" + item.numero_formula + "</td>";
        //    //    result += "<td>" + item.doc_beneficiario + "</td>";
        //    //    result += "<td>" + item.nombre_beneficiario + "</td>";

        //    //    result += "</tr>";
        //    //}



        //    //return Json(result);

        //   return Json(list, JsonRequestBehavior.AllowGet);

        //}

        [HttpPost]
        public JsonResult Save3(Models.Medicamentos.GestionIndicadores record)
        {

            List<md_indicadores_detalle> List = new List<md_indicadores_detalle>();
            List = record.GetIndicadoresDetalleID(record.id_md_ref_indicador, record.id_indicadores_medicamentos);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {

                    record.OBJDetallle.id_indicadores_medicamentos = item.id_indicadores_medicamentos;
                    record.OBJDetallle.id_md_ref_indicador = item.id_md_ref_indicador;
                    record.OBJDetallle.peso = record.peso;
                    record.OBJDetallle.valor = record.valor;
                    record.OBJDetallle.resultado = record.valor * record.peso;
                    record.OBJDetallle.observaciones = record.observaciones;


                    record.ActualizarIndicadoresMedicamentos(ref MsgRes);
                }

            }

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }


        }

        [HttpPost]
        public JsonResult Save4(Int32 id_indicadores_medicamentos, String hallazgos)
        {

            String mensaje = "";

            Models.Medicamentos.GestionIndicadores Model = new GestionIndicadores();
            Model.id_indicadores_medicamentos = Convert.ToInt32(id_indicadores_medicamentos);
            vw_total_md_detalle OBJ = new vw_total_md_detalle();

            OBJ = Model.Total_DetalleIndMD(id_indicadores_medicamentos);

            Decimal VALOR = OBJ.sum_resultado.Value / OBJ.sum_peso * 100;

            Model.resultado = VALOR;

            Model.OBJIndicadores.id_indicadores_medicamentos = id_indicadores_medicamentos;
            Model.OBJIndicadores.resultado = Model.resultado;
            Model.OBJIndicadores.hallazgos = hallazgos;
            Model.OBJIndicadores.estado = 1;

            Model.ActualizarIndicadoresMD(ref MsgRes);

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

        private void CrearPdfCartaIndicadoresMD(string fileName, Int32 id)
        {
            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptIndicadoresMd.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<ManagmentReporIndicadorMDResult> lst = new List<ManagmentReporIndicadorMDResult>();

            lst = Model.ReporteIndicadores(Convert.ToInt32(id));
            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetReportMD", lst);

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

        //public ActionResult ActualizarImagenes(String codigo)
        //{
        //    Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

        //    Model.CargarImg(codigo);

        //    var imagenMunicipio = Model.ListFacturasImg.FirstOrDefault();

        //    //Binary binary = imagenMunicipio.ruta_byte;

        //    //byte[] array = binary.ToArray();


        //    // return resulted pdf document 
        //    FileResult fileResult = new FileContentResult(array, "application/pdf");
        //    fileResult.FileDownloadName = "Document.pdf";
        //    return fileResult;




        //}

        public ActionResult GestorDocs(string formula)
        {

            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            List<vw_g_documental_medicamentos> ListUrl2 = Model.ConsultaIdGestionDocumentalFormula(formula, ref MsgRes);
            foreach (vw_g_documental_medicamentos x in ListUrl2)
            {
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, x.ruta);
                List<vw_g_documental_medicamentos> ListUrl3 = new List<vw_g_documental_medicamentos>();

                if (x.ruta_byte != null)
                {


                    Binary binary = x.ruta_byte;
                    byte[] array = binary.ToArray();
                    if (array != null)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", array.Length.ToString());
                        Response.BinaryWrite(array);
                        Response.Flush();


                    }
                }
                else if (x.ruta != null)
                {
                    WebClient User = new WebClient();
                    x.ruta = dirpath;
                    ListUrl3.Add(x);
                    Model.ListUrl = ListUrl3;
                    string filename = x.ruta;
                    Byte[] FileBuffer = User.DownloadData(filename);

                    if (FileBuffer != null)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", FileBuffer.Length.ToString());
                        Response.BinaryWrite(FileBuffer);
                        Response.Flush();


                    }
                }
                else
                {


                }
            }
            return View(Model);
        }

        public JsonResult Getobligaciones2(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.GestionObligaciones Model = new GestionObligaciones();
            Model.id_obligaciones_contractuales = id.Value;

            List<Managment_md_Ref_obligacionesResult> Lista = new List<Managment_md_Ref_obligacionesResult>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefObligaciones(Model.id_obligaciones_contractuales);

            var query = Lista;
            List<Managment_md_Ref_obligacionesResult> records = new List<Managment_md_Ref_obligacionesResult>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveObligaciones4(Int32 id_obligaciones_contractuales, String hallazgos)
        {
            Models.Medicamentos.GestionObligaciones Model = new GestionObligaciones();
            Model.id_obligaciones_contractuales = Convert.ToInt32(id_obligaciones_contractuales);
            vw_total_md_obligaciones_detalle OBJ = new vw_total_md_obligaciones_detalle();

            OBJ = Model.Total_DetalleObligacionesMD(id_obligaciones_contractuales);


            Decimal? VALOR = OBJ.sum_resultado.Value / OBJ.sum_peso * 100;

            Model.resultado = VALOR;

            Model.OBJObligacionesContractuales.id_obligaciones_contractuales = id_obligaciones_contractuales;
            Model.OBJObligacionesContractuales.resultado = Model.resultado;
            Model.OBJObligacionesContractuales.hallazgos = hallazgos;
            Model.OBJObligacionesContractuales.estado = 1;

            Model.ActualizarObligacionesMD(ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                //CrearPdfCartaIndicadoresMD("CartaIndicaMD   " + id_obligaciones_contractuales, id_obligaciones_contractuales);
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }


        }

        public JsonResult Saveobligaciones3(Models.Medicamentos.GestionObligaciones record)
        {

            List<md_obligaciones_contractuales_detalle> List = new List<md_obligaciones_contractuales_detalle>();
            List = record.GetObligacionesDetalleID(record.id_md_ref_obligaciones, record.id_obligaciones_contractuales);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {
                    record.OBJDetallle.id_md_obligaciones_contractuales_detalle = item.id_md_obligaciones_contractuales_detalle;
                    record.OBJDetallle.id_obligaciones_contractuales = item.id_obligaciones_contractuales;
                    record.OBJDetallle.peso = record.peso;
                    record.OBJDetallle.valor = record.valor;
                    record.OBJDetallle.resultado = record.valor * record.peso;
                    record.OBJDetallle.observaciones = record.observaciones;

                    record.ActualizarObligacionesDetalleMD(ref MsgRes);
                }

            }

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }

        }

        public List<md_consolidado_facturacion> Excelaentidad(string pathDelFicheroExcel)
        {
            var book = new ExcelQueryFactory(pathDelFicheroExcel);
            var resultado = (from row in book.Worksheet("Consolidado Facturación")
                             let item = new ECOPETROL_COMMON.ENTIDADES.md_consolidado_facturacion
                             {
                                 regional = row["REGIONAL"],
                                 proveedor = row["PROVEEDOR"],
                                 ciudad = row["CIUDAD"],
                                 numero_radicado = row["N° RAD"],
                                 fecha_real = Convert.ToDateTime(row["FECHA REAL"]),
                                 numero_factura = row["NUMERO DE FACT"],
                                 fecha_factura = Convert.ToDateTime(row["FECHA DE FACTURA"]),
                                 nit = row["Nit"],
                                 tipo_documento = row["TIPO DE DOC"],
                                 historia_clinica = row["HISTORIA CLINICA_OK"],
                                 doc_beneficiario = row["DOC BENEFICIARIO"],
                                 nombre_beneficiario = row["Nombre beneficiario"],
                                 edad = Convert.ToInt32(row["EDAD"]),
                                 nom_medico = row["NOMBRE MEDICO"],
                                 doc_medico = row["DOC MEDICO"],
                                 nombre_especialidad = row["NOMBRE ESPECIALIDAD"],
                                 cod_comercial = row["CÓDIGO ECOPETROL (Padre)"],
                                 cod_equivalente = row["CÓDIGO HIJO * UNIDAD MINIMA FARMACEUTICA"],
                                 producto_en_DCI = row["DESCRIPCION CLIENTE"],
                                 nombre_comercial = row["NOMBRE COMERCIAL"],
                                 nombre_laboratorio = row["NOMBRE LABORATORIO"],
                                 fecha_entrega = Convert.ToDateTime(row["FECHA DE ENTREGA"]),
                                 presentacion = row["Presentación"],
                                 cat_entregada = row["CANT ENTREGADA"],
                                 valor_unitario = Convert.ToDecimal(row["VALOR UNITARIO"]),
                                 valor_total = Convert.ToDecimal(row["Valor total"]),
                                 numero_formula = row["FÓRMULA"],
                                 med_pendiente = row["MED PENDIENTE"],
                                 nombre_nivel_autorizacion = row["NOMBRE NIVEL AUTORIZACION"],
                                 diagnostico_cod_base = row["DIAGNOSTICO COD CLASE"],
                                 cod_atc = row["COD ATC"],
                                 cobro_iva = row["COBRO DE IVA"],
                                 subcuenta = row["SUBCUENTA"],
                                 dosis = row["DOSIS"],
                                 expediente = row["EXPEDIENTE"],
                                 consecutivo = row["CONSECUTIVO"],
                                 categoria_invima = row["CATEGORIA INVIMA"],
                                 registro_sanitario = row["REGISTRO SANITARIO"],
                                 tipo_documento2 = row["TIPO DOCUMENTO 2"],
                                 letra_ilegible = row["LETRA ILEGIBLE"],
                                 enmendadu_tachaduras = row["ENMENDADURA"],
                                 unidades_diferentes = row["DOSIS EN UNIDADES DIFERENTES"],
                                 medicamentos_control_especial = row["PRESCRIPCIÓN DE MEDICAMENTOS"],
                                 sin_datos_paciente = row["SIN DATOS DEL PACIENTE: NOMBRE Y NÚMERO DE IDENTIFICACIÓN"],
                                 nombre_medicamento_en_comercial = row["NOMBRE DEL MEDICAMENTO EN COMERCIAL"],
                                 sin_concentracion = row["SIN CONCENTRACION"],
                                 sin_forma_farmaceutica = row["SIN FORMA FARMACEUTICA"],
                                 sin_via_de_administracion_o_via_incorrecta = row["SIN VIA DE ADMINISTRACIÓN O VIA INCORRECTA"],
                                 sin_dosis = row["SIN DOSIS"],
                                 sin_frecuencia_de_adm = row["SIN FRECUENCIA DE ADMINISTRACION"],
                                 sin_cantidad_total_tratamiento = row["SIN CANTIDAD TOTAL DE TRATAMIENTO"],
                                 sin_datos_prescriptor = row["SIN DATOS DEL PRESCRIPTOR O DATOS INCOMPLETOS"],
                                 total_errore_mes = row["TOTAL ERRORES MES"],
                                 en_auditor_prescriptor_no_formulas_hechas = row["FÓRMULAS HECHAS EN AUDITOR PRESCRIPTOR"],
                                 otros_convenios = row["FÓRMULAS HECHAS OTROS CONVENIOS"],
                                 en_sistemas_esalud = row["FÓRMULAS HECHAS EN SISTEMA E-SALUD"],
                                 formulas_hechas_manual = row["FÓRMULAS HECHAS MANUAL"],
                                 total_formulas_evaluadas_mes_medico = row["TOTAL FORMULAS EVALUADAS MES POR MÉDICO"],
                                 tipo_formula = row["TIPO DE FÓRMULA"],
                                 accion = row["ACCION"],
                                 medicamentos_de_aplicacion_supervisada = row["MEDICAMENTOS DE APLICACIÓN SUPERVISADA"],
                                 formula_de_trascripcion = row["FÓRMULA DE TRASNCRIPCIÓN"],
                                 mes_de_despacho = row["MES DE DESPACHO"],
                                 especialidad = row["Especialidad"],
                                 mega_para_adherencia = row["MEGA PARA ADHERENCIA?"],
                                 lupe = row["LUPE"],
                                 dci_lupe = row["DCI LUPE"],
                                 dci_sin_espacio = row["espacios"],
                                 fecha_cargue = Convert.ToDateTime(DateTime.Now),

                             }
                             select item).ToList();
            book.Dispose();

            return resultado;

        }

        [HttpPost]
        public ActionResult ListaDispersacion(Int32 id, Models.Medicamentos.GestionIndicadores Model)
        {
            var List = Model.ListaPuntosDispersacion(id);
            if (List.Count() > 0)
            {
                md_ref_puntos_dispensacion OBJ = new md_ref_puntos_dispensacion();
                foreach (var item in List)
                {
                    OBJ.direccion = item.direccion;
                    OBJ.telefono = item.telefono;
                    OBJ.fuerza = item.fuerza;
                }


                return Json(new { success = true, OBJ }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult GetIndicadorDtll(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.GestionTablero Model = new GestionTablero();
            Model.id_indicadores_medicamentos = id.Value;

            List<vw_indicador_detalle_sin_cumplir> Lista = new List<vw_indicador_detalle_sin_cumplir>();

            //Model.LlenaLista();
            Lista = Model.GetIndicadoresSinCumplir(Model.id_indicadores_medicamentos);

            var query = Lista;
            List<vw_indicador_detalle_sin_cumplir> records = new List<vw_indicador_detalle_sin_cumplir>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DescargarImg(Int32 id)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();


            GestionDocumentalMedicamentosCad ListUrl2 = BusClass.Traerimagenindicacioes(Convert.ToInt32(id));
            //var ListUrl2 = Model.ConsultaIdGestionDocumentalMDCalId(Convert.ToInt32(id), ref MsgRes);

            Binary binary = ListUrl2.ruta_byte;
            byte[] array = binary.ToArray();

            return File(array, "image/jpeg");

        }

        public ActionResult BuscarDispensacionAmbulatoria()
        {
            Models.Medicamentos.dispensaciones Model = new Models.Medicamentos.dispensaciones();

            Model.ConsultaLista();

            return View(Model);

        }

        [HttpPost]
        public ActionResult BuscarDispensacionAmbulatoria(Models.Medicamentos.dispensaciones Model)
        {
            String proveedor = Model.nombre_auditor;

            Model.AmbulatorioProvvedor(proveedor);
            Model.ConsultaLista();

            return View(Model);
        }

        public ActionResult IngresoDispensacionAmbulatoria()
        {
            Models.Medicamentos.dispensaciones Model = new Models.Medicamentos.dispensaciones();

            Model.fecha_auditoria = Convert.ToDateTime(DateTime.Now);
            Model.fecha_auditoriaOK = Convert.ToDateTime(DateTime.Now);
            Model.nombre_auditor = Convert.ToString(SesionVar.NombreUsuario);

            return View(Model);

        }

        [HttpPost]
        public ActionResult IngresoDispensacionAmbulatoria(Models.Medicamentos.dispensaciones Model)
        {
            try
            {
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";

                if (Model.fecha_auditoriaOK != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA AUDITORIA";
                    Conteo = Conteo + 1;
                }

                if (Model.ciudad != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "CIUDAD";
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
                    md_dispensacion_ambulatoria obj = new md_dispensacion_ambulatoria();

                    obj.fecha_auditoria = Model.fecha_auditoriaOK;
                    obj.nombre_auditor = Convert.ToString(SesionVar.NombreUsuario);
                    obj.ciudad = Convert.ToString(Model.ciudad);
                    obj.nombre_auditado = Convert.ToInt32(Model.nombre_auditado);
                    obj.id_punto_dispensacion = Convert.ToInt32(Model.nombre_farmacia);
                    obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                    Int32 id = Model.Insertardispensacion_ambulatoria(obj, ref MsgRes);
                    Model.id_md_dispensacion_ambulatoria = id;
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        md_dispensacion_ambulatoria_detalle objDetalle = new md_dispensacion_ambulatoria_detalle();

                        List<md_ref_dispens_ambulatoria> list = new List<md_ref_dispens_ambulatoria>();
                        list = Model.RefDispersacionAmbulatoria();

                        foreach (var item in list)
                        {
                            objDetalle.id_md_dispensacion_ambulatoria = Model.id_md_dispensacion_ambulatoria;
                            objDetalle.id_md_ref_dispens_ambulatoria = item.id_md_ref_dispens_ambulatoria;
                            objDetalle.peso = item.peso;
                            objDetalle.valor = 1;
                            objDetalle.resultado = 1 * item.peso;
                            objDetalle.observaciones = "";

                            Model.Insertardispensacion_ambulatoriaDetalle(objDetalle, ref MsgRes);
                        }

                        return RedirectToAction("DispensacionAmbulatoriaDetalle", "medicamentos", new { id = Model.id_md_dispensacion_ambulatoria });
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR AL INGRESO!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
                }



            }
            catch
            {

            }
            return View(Model);


        }

        public ActionResult ReasignacionCasos()
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();



            return View(Model);

        }

        public ActionResult DispensacionAmbulatoriaDetalle(int id)
        {
            Models.Medicamentos.dispensaciones Model = new dispensaciones();

            Model.id_md_dispensacion_ambulatoria = id;

            return View(Model);


        }

        public JsonResult GetAmbulatorio(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.dispensaciones Model = new dispensaciones();
            Model.id_md_dispensacion_ambulatoria = id.Value;

            List<Managment_md_Ref_ambulatorioResult> Lista = new List<Managment_md_Ref_ambulatorioResult>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefAmbulatorio(Model.id_md_dispensacion_ambulatoria);

            var query = Lista;
            List<Managment_md_Ref_ambulatorioResult> records = new List<Managment_md_Ref_ambulatorioResult>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAmbulatorio(Models.Medicamentos.dispensaciones record)
        {

            List<md_dispensacion_ambulatoria_detalle> List = new List<md_dispensacion_ambulatoria_detalle>();
            List = record.GetAmbulatorioDetalleID(record.id_md_ref_dispens_ambulatoria, record.id_md_dispensacion_ambulatoria);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {
                    md_dispensacion_ambulatoria_detalle obj = new md_dispensacion_ambulatoria_detalle();

                    obj.id_md_dispensacion_ambulatoria = item.id_md_dispensacion_ambulatoria;
                    obj.id_md_ref_dispens_ambulatoria = item.id_md_ref_dispens_ambulatoria;
                    obj.peso = record.peso;
                    obj.valor = record.valor;
                    obj.resultado = record.valor * record.peso;
                    obj.observaciones = record.observaciones;

                    record.ActualizarDispersacionAmbulatorio(obj, ref MsgRes);
                }

            }

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }


        }

        [HttpPost]
        public JsonResult SaveAmbulatorioGeneral(Int32 id_md_dispensacion_ambulatoria, String hallazgos)
        {
            Models.Medicamentos.dispensaciones Model = new dispensaciones();

            Model.id_md_dispensacion_ambulatoria = Convert.ToInt32(id_md_dispensacion_ambulatoria);

            vw_md_total_ambulatoria OBJ = new vw_md_total_ambulatoria();

            OBJ = Model.Total_DetalleAmbulatoria(id_md_dispensacion_ambulatoria);

            Decimal VALOR = OBJ.sum_resultado.Value / Convert.ToDecimal(OBJ.suma_peso) * 100;

            Model.resultado = VALOR;

            md_dispensacion_ambulatoria OBJAmb = new md_dispensacion_ambulatoria();

            OBJAmb.id_md_dispensacion_ambulatoria = id_md_dispensacion_ambulatoria;
            OBJAmb.resultado = Model.resultado;
            OBJAmb.hallazgos = hallazgos;
            OBJAmb.estado = 1;

            Model.ActualizarAmbulatoriaMD(OBJAmb, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }


        }

        public ActionResult BuscarDispensacionHospitalaria()
        {
            Models.Medicamentos.dispensaciones Model = new Models.Medicamentos.dispensaciones();

            Model.ConsultaLista();

            return View(Model);

        }

        [HttpPost]
        public ActionResult BuscarDispensacionHospitalaria(Models.Medicamentos.dispensaciones Model)
        {
            String proveedor = Model.nombre_auditor;

            Model.hospitalarioProvvedor(proveedor);
            Model.ConsultaLista();

            return View(Model);
        }

        public ActionResult IngresoDispensacionHospitalaria()
        {
            Models.Medicamentos.dispensaciones Model = new Models.Medicamentos.dispensaciones();

            Model.fecha_auditoria = Convert.ToDateTime(DateTime.Now);
            Model.fecha_auditoriaOK = Convert.ToDateTime(DateTime.Now);
            Model.nombre_auditor = Convert.ToString(SesionVar.NombreUsuario);

            return View(Model);

        }

        [HttpPost]
        public ActionResult IngresoDispensacionHospitalaria(Models.Medicamentos.dispensaciones Model)
        {
            try
            {
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";

                if (Model.fecha_auditoriaOK != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA AUDITORIA";
                    Conteo = Conteo + 1;
                }

                if (Model.ciudad != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "CIUDAD";
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
                    md_dispensacion_hospitalaria obj = new md_dispensacion_hospitalaria();

                    obj.fecha_auditoria = Model.fecha_auditoriaOK;
                    obj.nombre_auditor = Convert.ToString(SesionVar.NombreUsuario);
                    obj.ciudad = Convert.ToString(Model.ciudad);
                    obj.nombre_auditado = Convert.ToInt32(Model.nombre_auditado);
                    obj.id_punto_dispensacion = Convert.ToInt32(Model.nombre_farmacia);
                    obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                    Int32 id = Model.Insertardispensacion_Hospitalaria(obj, ref MsgRes);
                    Model.id_md_dispensacion_hospitalaria = id;
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        md_dispensacion_hospitalaria_detalle objDetalle = new md_dispensacion_hospitalaria_detalle();

                        List<md_ref_dispens_hospitalaria> list = new List<md_ref_dispens_hospitalaria>();
                        list = Model.RefDispersacionHospitalaria();

                        foreach (var item in list)
                        {
                            objDetalle.id_md_dispensacion_hospitalaria = Model.id_md_dispensacion_hospitalaria;
                            objDetalle.id_md_ref_dispens_hospitalaria = item.id_md_ref_dispens_hospitalaria;
                            objDetalle.peso = item.peso;
                            objDetalle.valor = 1;
                            objDetalle.resultado = 1 * item.peso;
                            objDetalle.observaciones = "";

                            Model.Insertardispensacion_HospitalariaDetalle(objDetalle, ref MsgRes);
                        }

                        return RedirectToAction("DispensacionHospitalariaDetalle", "medicamentos", new { id = Model.id_md_dispensacion_hospitalaria });
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR AL INGRESO!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
                }



            }
            catch
            {

            }
            return View(Model);


        }

        public ActionResult DispensacionHospitalariaDetalle(int id)
        {
            Models.Medicamentos.dispensaciones Model = new dispensaciones();

            Model.id_md_dispensacion_hospitalaria = id;

            return View(Model);


        }

        public JsonResult GetHospitalario(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.dispensaciones Model = new dispensaciones();
            Model.id_md_dispensacion_hospitalaria = id.Value;

            List<Managment_md_Ref_hospitalarioResult> Lista = new List<Managment_md_Ref_hospitalarioResult>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefHospitalario(Model.id_md_dispensacion_hospitalaria);

            var query = Lista;
            List<Managment_md_Ref_hospitalarioResult> records = new List<Managment_md_Ref_hospitalarioResult>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveHospitalario(Models.Medicamentos.dispensaciones record)
        {

            List<md_dispensacion_hospitalaria_detalle> List = new List<md_dispensacion_hospitalaria_detalle>();
            List = record.GetHospitalarioDetalleID(record.id_md_ref_dispens_hospitalaria, record.id_md_dispensacion_hospitalaria);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {
                    md_dispensacion_hospitalaria_detalle obj = new md_dispensacion_hospitalaria_detalle();

                    obj.id_md_dispensacion_hospitalaria = item.id_md_dispensacion_hospitalaria;
                    obj.id_md_ref_dispens_hospitalaria = item.id_md_ref_dispens_hospitalaria;
                    obj.peso = record.peso;
                    obj.valor = record.valor;
                    obj.resultado = record.valor * record.peso;
                    obj.observaciones = record.observaciones;

                    record.ActualizarDispersacionHospitalizacion(obj, ref MsgRes);
                }

            }

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }


        }

        [HttpPost]
        public JsonResult SaveHospitalarioGeneral(Int32 id_md_dispensacion_hospitalaria, String hallazgos)
        {
            Models.Medicamentos.dispensaciones Model = new dispensaciones();

            Model.id_md_dispensacion_hospitalaria = Convert.ToInt32(id_md_dispensacion_hospitalaria);

            vw_md_total_hospitalaria OBJ = new vw_md_total_hospitalaria();

            OBJ = Model.Total_DetalleHospitalaria(id_md_dispensacion_hospitalaria);

            Decimal VALOR = OBJ.sum_resultado.Value / Convert.ToDecimal(OBJ.suma_peso) * 100;

            Model.resultado = VALOR;

            md_dispensacion_hospitalaria OBJHos = new md_dispensacion_hospitalaria();

            OBJHos.id_md_dispensacion_hospitalaria = id_md_dispensacion_hospitalaria;
            OBJHos.resultado = Model.resultado;
            OBJHos.hallazgos = hallazgos;
            OBJHos.estado = 1;

            Model.ActualizarHospitalariaMD(OBJHos, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }
        }

        /*Kevin  Suarez 22-05-22*/

        public ActionResult Prefacturas()
        {
            try
            {
                ViewData["MensajeRta"] = "";
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Prefacturas(int regional, int prestador)
        {
            var mensaje = "";
            var path = "";
            var idUsuario = SesionVar.IDUser;
            try
            {
                string numremision = "";
                GestionMedicamentos Model = new GestionMedicamentos();
                ViewData["MensajeRta"] = "";
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();

                IList<HttpPostedFileBase> files = Request.Files.GetMultiple("file");
                HttpPostedFileBase file = null;

                if (files.Count > 0)
                {
                    file = files[0];
                }

                numremision = regional + "-" + prestador + "-" + DateTime.Now.Year + "-" + DateTime.Now.Month;

                if (file.ContentLength > 0)
                {
                    try
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

                        md_prefacturas_cargue_base obj = new md_prefacturas_cargue_base();

                        obj.id_regional = regional;
                        obj.num_prefactura = numremision;
                        obj.id_prestador = prestador;
                        obj.abierta = true;
                        obj.fecha_digita = Convert.ToDateTime(DateTime.Now);
                        obj.usuario_digita = SesionVar.UserName;

                        Ref_regional datoReg = BusClass.GetRefRegionId(regional);

                        List<md_prefacturas_detalle> listaDetalle = new List<md_prefacturas_detalle>();
                        try
                        {
                            listaDetalle = Model.ExcelAPrefacturas(dataTable, ref MsgRes);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            throw new Exception(error);
                        }

                        var resultado = MsgRes.ResponseType;

                        if (listaDetalle.Count() != 0 && resultado == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            if (listaDetalle.Count() != 0)
                            {
                                var lote = BusClass.CarguePrefacturaBase(obj, listaDetalle);

                                var mensajeSalida = MsgRes.DescriptionResponse;
                                var conteo = 0;

                                if (lote != 0)
                                {
                                    ViewData["MensajeRta"] = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE - CARGUE #" + lote + " </div>";
                                    var envioCorreo = EnviarCasoCargueCompleto(lote, SesionVar.IDUser, listaDetalle.Count(), 1);
                                }
                                else
                                {
                                    BusClass.EliminarCarguePrefacturasCompleto((int)lote);
                                    var errorMensaje = MsgRes.DescriptionResponse;

                                    mensaje = "ERROR AL INGRESAR LAS PREFACTURAS: " + errorMensaje;
                                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                                }
                            }
                            else
                            {
                                mensaje = "ERROR AL INGRESAR LAS PREFACTURAS: " + MsgRes.DescriptionResponse;
                                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                            }
                        }
                        else
                        {
                            mensaje = "ERROR AL INGRESAR LAS PREFACTURAS: " + MsgRes.DescriptionResponse;
                            ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                        }
                    }
                    catch (Exception e)
                    {
                        mensaje = "ERROR AL INGRESAR LAS PREFACTURAS: " + e.Message;
                        ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                    }
                }
                else
                {
                    mensaje = "FALTA UN ARCHIVO EN FORMATO EXCEL.";
                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                }
            }
            catch (Exception e)
            {
                mensaje = "ERROR AL INGRESAR LAS PREFACTURAS: " + e.Message;
                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error! </strong>" + mensaje + "</div>";
            }

            return View();
        }

        public ActionResult TableroValidarCargues()
        {
            GestionMedicamentos Model = new GestionMedicamentos();
            List<management_validadorCarguePrefacturasResult> listaInicial = new List<management_validadorCarguePrefacturasResult>();
            List<management_validadorCarguePrefacturasResult> listaFase1 = new List<management_validadorCarguePrefacturasResult>();
            List<management_validadorCarguePrefacturasResult> listaFase2 = new List<management_validadorCarguePrefacturasResult>();
            List<management_validadorCarguePrefacturasResult> listaFase3 = new List<management_validadorCarguePrefacturasResult>();
            var rol = Convert.ToInt32(SesionVar.ROL);
            var usuario = SesionVar.UserName;

            var conteo1 = 0;
            var conteo2 = 0;
            var conteo3 = 0;

            try
            {
                listaInicial = Model.GetPrefacturasListadoCargue(rol, usuario);

                listaFase1 = listaInicial.Where(x => x.fase_validacion == null || x.fase_validacion == 0 || x.fase_validacion == 1).ToList();
                conteo1 = listaFase1.Count();

                listaFase2 = listaInicial.Where(x => x.fase_validacion == 2).ToList();
                conteo2 = listaFase2.Count();

                listaFase3 = listaInicial.Where(x => x.fase_validacion == 3).ToList();
                conteo3 = listaFase3.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = listaInicial;
            ViewBag.lista1 = listaFase1;
            ViewBag.lista2 = listaFase2;
            ViewBag.lista3 = listaFase3;
            ViewBag.conteo = listaInicial.Count;
            ViewBag.conteo1 = conteo1;
            ViewBag.conteo2 = conteo2;
            ViewBag.conteo3 = conteo3;

            ViewBag.rol = rol;

            return View();
        }

        public JsonResult ValidarCarguesId(int? idCargue, int? idUsuario, int? conteoDatos, int? fase)
        {
            var mensajeCorrecto = "";
            var rta = 0;
            var conteo = 0;
            HttpClient httpClient = new HttpClient();
            sis_usuario usuario = new sis_usuario();
            md_prefacturas_cargue_base baseDatos = new md_prefacturas_cargue_base();
            var tipoRespuesta = 0;
            var nombreUsuario = "";
            log_control_validaciones_prefacturas logueado = new log_control_validaciones_prefacturas();
            var actu = 0;
            try
            {
                baseDatos = BusClass.GetPrefacturas((int)idCargue);
                logueado = BusClass.GetLogPrefacturas(idCargue);
                usuario = BusClass.datosUsuarioId((int)idUsuario);

                if (usuario != null)
                {
                    nombreUsuario = usuario.usuario;
                }

                log_control_validaciones_prefacturas log = new log_control_validaciones_prefacturas();

                log.id_cargue_base = idCargue;
                log.fase = fase;
                log.usuario_digita = SesionVar.UserName;
                log.fecha_digita = DateTime.Now;

                var insertaLog = BusClass.GuardarLogControl_validacionesPrefacturas(log);

                // Obtener el valor actual del tiempo de sesión

                if (fase == 1)
                {
                    mensajeCorrecto = "DATOS FASE 1 VALIDADOS CORRECTAMENTE";
                    conteo = BusClass.ActualizarConteo_Facturas_fase1((int)idCargue, usuario.usuario, ref MsgRes);
                }

                else if (fase == 2)
                {
                    mensajeCorrecto = "DATOS FASE 2 VALIDADOS CORRECTAMENTE";
                    conteo = BusClass.ActualizarConteo_Facturas_fase2_2((int)idCargue, usuario.usuario, ref MsgRes);
                }

                else if (fase == 3)
                {
                    mensajeCorrecto = "DATOS FASE 3  VALIDADOS CORRECTAMENTE";
                    conteo = BusClass.ActualizarConteo_Facturas_fase3((int)idCargue, SesionVar.UserName, ref MsgRes);
                }

                if (conteo != 0)
                {
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        httpClient.Timeout = TimeSpan.FromHours(1);
                        var seEnviaCorreo = EnviarCasoCargueCompleto(idCargue, idUsuario, conteoDatos, 2);
                        rta = 1;
                    }
                    else
                    {
                        throw new Exception(MsgRes.DescriptionResponse);
                    }
                }
                else
                {
                    throw new Exception(MsgRes.DescriptionResponse);
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;

                log_prefacturas_errorFases fs = new log_prefacturas_errorFases();
                fs.id_cargue = idCargue;
                fs.fase = fase;
                fs.error = error;
                fs.usuario_digita = SesionVar.UserName;
                fs.fecha_digita = DateTime.Now;

                var insertaErrror = BusClass.LogErroresFasesPrefacturas(fs);

                mensajeCorrecto = error;
                if (mensajeCorrecto.Contains("YA SE HA ENVIADO ESTE CARGUE PARA VALIDACIÓN EN FASE"))
                {
                    var seEnviaCorreo = EnviarCasoCargueCompleto(idCargue, idUsuario, conteoDatos, 4);
                    tipoRespuesta = 1;
                }
                else
                {
                    var seEnviaCorreo = EnviarCasoCargueCompleto(idCargue, idUsuario, conteoDatos, 3);
                }

                BusClass.ActualizarEnValidacionPrefacturas(idCargue, 0);
            }
            return Json(new { mensaje = mensajeCorrecto, rta = rta, tipoRespuesta = tipoRespuesta, fase = fase });
        }

        public void UpdateSessionTimeout(int minutes)
        {
            // Cargar el archivo de configuración
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");

            // Obtener la sección de sessionState
            SessionStateSection sessionStateSection = (SessionStateSection)config.GetSection("system.web/sessionState");

            // Actualizar el tiempo de sesión
            if (sessionStateSection != null)
            {
                sessionStateSection.Timeout = TimeSpan.FromMinutes(minutes);

                // Guardar los cambios en el archivo de configuración
                config.Save();
            }
        }

        public PartialViewResult LogValidaciones(int? idCargue, int? fase)
        {
            List<log_prefacturas_estadoValidacion> log = new List<log_prefacturas_estadoValidacion>();
            var conteo = 0;

            try
            {
                log = BusClass.GetLogEstadoValidacionPrefacturasFases(idCargue, fase);
                conteo = log.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoFases = log;
            ViewBag.conteoFases = conteo;
            ViewBag.fase = fase;
            ViewBag.idCargue = idCargue;

            return PartialView();
        }

        public JsonResult CargueValidadoCorrectamente(int? idCargue)
        {
            var mensaje = "";
            try
            {
                var actu = BusClass.ActualizarEnValidacionPrefacturas(idCargue, 0);
                if (actu != 0)
                {
                    mensaje = "CARGUE VALIDADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR EN LA VALIDACIÓN DEL CARGUE";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA VALIDACIÓN DEL CARGUE: " + error;
            }

            return Json(new { mensaje = mensaje });
        }

        public JsonResult DevolverEstadoAnterior(int? idCargue)
        {
            var mensaje = "";
            log_control_validaciones_prefacturas logueado = new log_control_validaciones_prefacturas();

            try
            {
                logueado = BusClass.GetLogPrefacturas(idCargue);
                DateTime fechadigita = (DateTime)logueado.fecha_digita;
                TimeSpan diferencia = DateTime.Now - fechadigita;
                double horasDiferencia = diferencia.TotalHours;
                double minutos = diferencia.TotalMinutes;

                double minutosfaltantes = 120 - minutos;

                if (horasDiferencia >= 2)
                {
                    mensaje = "EL CARGUE FUE ENVIADO HACE MENOS DE DOS HORAS, POR FAVOR ESPERE " + minutosfaltantes + " MINUTOS";
                    throw new Exception(mensaje);
                }

                var actualiza = BusClass.ActualizarPrefacturaCargueFaseDevolver(idCargue);

                if (actualiza != 0)
                {
                    mensaje = "CARGUE ACTUALIZADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR EN LA ACTUALIZACIÓN DE CARGUE";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ACTUALIZACIÓN DE CARGUE: " + error;
            }

            return Json(new { mensaje = mensaje });
        }

        public ActionResult CargueLupe()
        {
            GestionMedicamentos Model = new GestionMedicamentos();

            try
            {
                Session["Intermediaciones"] = new List<med_intermediacion>();
                ViewData["MensajeRta"] = "";

                ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.referenciaPa = Model.GetReferenciaPagoList();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }

        [HttpPost]
        public ActionResult CargueLupe(int referencia, DateTime vigencia, int prestador, HttpPostedFileBase file)
        {
            var path = "";

            try
            {
                List<med_intermediacion> listado = (List<med_intermediacion>)Session["Intermediaciones"];
                List<md_lupe_intermediacion_dtll> Intermediaciones = new List<md_lupe_intermediacion_dtll>();
                foreach (var item in listado)
                {
                    md_lupe_intermediacion_dtll md = new md_lupe_intermediacion_dtll();
                    md.regional_id = item.id_regional;
                    md.porcentaje_intermediacion_ABE = item.porcentaje_ABE;
                    md.porcentaje_intermediacion_FC = item.porcentaje_FC;
                    Intermediaciones.Add(md);
                }

                GestionMedicamentos Model = new GestionMedicamentos();
                ViewData["MensajeRta"] = "";

                ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
                ViewBag.regionales = BusClass.GetRefRegion();
                Session["Intermediaciones"] = new List<med_intermediacion>();

                if (Intermediaciones.Count() != 0)
                {

                    if (this.Request.Files["file"].ContentLength > 0)
                    {
                        try
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

                            md_Lupe_cargue_base consulta = new md_Lupe_cargue_base();
                            md_Lupe_cargue_base obj = new md_Lupe_cargue_base();

                            obj.id_prestador = prestador;

                            obj.vigencia_desde = vigencia;
                            obj.vigencia_hasta = null;
                            obj.referente_pago = referencia;
                            obj.fecha_digita = Convert.ToDateTime(DateTime.Now);
                            obj.usuario_digita = SesionVar.UserName;

                            consulta = BusClass.UltimoCargueLupe(prestador);
                            if (consulta != null)
                            {
                                consulta.vigencia_hasta = vigencia.AddDays(-1);
                                try
                                {
                                    var áctualiza = BusClass.ActualizarVigenciaHastaLupe(consulta);
                                }
                                catch (Exception ex)
                                {
                                    var erro = ex.Message;
                                }
                            }

                            Int32 lote = Model.ExcelALUPE(dataTable, obj, Intermediaciones, ref MsgRes);

                            var resultado = MsgRes.ResponseType;
                            var mensajeSalida = MsgRes.DescriptionResponse;

                            ViewBag.referenciaPa = Model.GetReferenciaPagoList();

                            if (resultado == BussinesEnums.EnumTipoRespuesta.Correcto)
                            {
                                ViewData["MensajeRta"] = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESO CORRECTAMENTE. </div>";
                            }
                            else
                            {
                                var mensaje = "ERROR AL INGRESAR CARGUE LTE: " + MsgRes.DescriptionResponse;
                                Model.EliminarCargueLUPE(lote, ref MsgRes);
                                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong>" + mensaje + "</div>";
                            }
                        }
                        catch (Exception e)
                        {
                            ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> NO SE HAN PODIDO CARGAR LOS REGISTROS: " + e.Message + ".</div>";
                        }
                    }
                    else
                    {
                        ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> FALTA UN ARCHIVO EN FORMATO EXCEL. </div>";
                    }
                }
                else
                {
                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> DEBE AÑADIR AL MENOS UNA INTERMEDIACIÓN. </div>";
                }

                ViewBag.referenciaPa = Model.GetReferenciaPagoList();
            }
            catch (Exception e)
            {
                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> NO SE HAN PODIDO CARGAR LOS REGISTROS: " + e.Message + ".</div>";
            }

            return View();
        }

        public ActionResult TableroValidacion()
        {
            GestionMedicamentos Model = new GestionMedicamentos();
            List<management_validadorPrefacturasResult> listaInicial = new List<management_validadorPrefacturasResult>();
            var rol = Convert.ToInt32(SesionVar.ROL);
            var usuario = SesionVar.UserName;

            try
            {
                listaInicial = Model.GetPrefacturasListado(rol, usuario);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = listaInicial;
            ViewBag.conteo = listaInicial.Count;
            ViewBag.rol = rol;

            return View();
        }

        public ActionResult ControlValidacionPrefacturas(int? idPrefacturaBase, int? prestador)
        {
            GestionMedicamentos Model = new GestionMedicamentos();
            List<management_listadoPrefacturasNoCruzanResult> listadoNoCruzan = new List<management_listadoPrefacturasNoCruzanResult>();
            List<management_listadoPrefacturasCruzanResult> listadoCruzan = new List<management_listadoPrefacturasCruzanResult>();

            management_prefacturasDatosCargueResult datosCargue = new management_prefacturasDatosCargueResult();

            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromHours(2);

            var conteoCruzan = 0;
            var conteoNoCruzan = 0;

            try
            {
                datosCargue = BusClass.DatosPrefacturaIdCargue((int)idPrefacturaBase);

                ViewBag.numremision = datosCargue.num_prefactura;
                ViewBag.prestador = datosCargue.Nombre;

                ViewBag.abierta = datosCargue.abierta;
                ViewBag.idFactura = idPrefacturaBase;

                Session["idPrefacturaBase"] = idPrefacturaBase;

                try
                {
                    listadoCruzan = BusClass.listadoSiCruzanPrefacturasLupe((int)idPrefacturaBase);
                    listadoNoCruzan = BusClass.listadoNoCruzanPrefacturasLupe((int)idPrefacturaBase);

                    conteoCruzan = listadoCruzan.Count();
                    conteoNoCruzan = listadoNoCruzan.Count();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    throw new Exception(error);
                }

                Session["aprobadosIni"] = listadoCruzan;
                Session["no_aprobadosIni"] = listadoNoCruzan;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaCruzan = listadoCruzan;
            ViewBag.conteoCruzan = conteoCruzan;

            ViewBag.listaNoCruzan = listadoNoCruzan;
            ViewBag.conteoNoCruzan = conteoNoCruzan;

            httpClient.Timeout = TimeSpan.FromHours(2);

            return View();
        }

        public ActionResult TableroValidacionFacturas()
        {
            GestionMedicamentos Model = new GestionMedicamentos();
            return View();
        }

        public JsonResult EliminarCarguePrefacturas(int? idCargue)
        {
            var mensaje = "";
            try
            {
                var resultado = BusClass.EliminarCarguePrefacturasCompleto((int)idCargue);
                if (resultado != 0)
                {
                    log_prefacturas_eliminarCargues log = new log_prefacturas_eliminarCargues();
                    log.id_cargue = idCargue;
                    log.fecha_eliminacion = DateTime.Now;
                    log.usuario_elimina = SesionVar.UserName;

                    var elimina = BusClass.GuardarLogEliminacionPrefacturas(log);

                    mensaje = "CARGUE PREFACTURAS ELIMINADO CORRECTAMENTE.";
                    return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EN LA ELIMINACIÓN DE CARGUE PREFACTURAS.";
                    return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ELIMINACIÓN DE CARGUE PREFACTURAS: " + error;
                return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ExportarExcelNoAprobados()
        {
            List<management_listadoPrefacturasNoCruzanResult> Listado = new List<management_listadoPrefacturasNoCruzanResult>();
            try
            {
                Listado = (List<management_listadoPrefacturasNoCruzanResult>)Session["no_aprobadosIni"];
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Cargue");
                GestionMedicamentos Model = new GestionMedicamentos();

                Sheet.Cells["A1:AT1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:AT1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AT1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AT1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AT1"].Style.Font.Name = "Century Gothic";

                //Sheet.Protection.SetPassword("sami2023*");
                //Sheet.Protection.IsProtected = true;
                //Sheet.Cells["AF2:AF999999"].Style.Locked = false;
                //Sheet.Cells["AG2:AG999999"].Style.Locked = false;

                Sheet.Cells["A1"].Value = "Nro cargue";
                Sheet.Cells["B1"].Value = "Nro registro";
                Sheet.Cells["C1"].Value = "Remision pre factura fact";
                Sheet.Cells["D1"].Value = "Num Tirilla o consecutivo";
                Sheet.Cells["E1"].Value = "Fecha radicacion";
                Sheet.Cells["F1"].Value = "Nit";
                Sheet.Cells["G1"].Value = "Tipo doc beneficiario";
                Sheet.Cells["H1"].Value = "Num documento beneficiario";
                Sheet.Cells["I1"].Value = "Nombre beneficiario";
                Sheet.Cells["J1"].Value = "Nombre beneficiario especial";
                Sheet.Cells["K1"].Value = "Ciudad despacho";
                Sheet.Cells["L1"].Value = "Id prescriptor";
                Sheet.Cells["M1"].Value = "Prescriptor";
                Sheet.Cells["N1"].Value = "Especialidad";
                Sheet.Cells["O1"].Value = "Cum";
                Sheet.Cells["P1"].Value = "Cod Ecopetrol hijo comercial";
                Sheet.Cells["Q1"].Value = "Cod interno medicamento";
                Sheet.Cells["R1"].Value = "Cod generico o interno medicamento";
                Sheet.Cells["S1"].Value = "Presentacion";
                Sheet.Cells["T1"].Value = "Descripcion producto";
                Sheet.Cells["U1"].Value = "Nombre comercial medicamento";
                Sheet.Cells["V1"].Value = "Laboratorio fabricante";
                Sheet.Cells["W1"].Value = "Fecha despacho formula";
                Sheet.Cells["X1"].Value = "Num unidades prescritas";
                Sheet.Cells["Y1"].Value = "Num formula";
                Sheet.Cells["Z1"].Value = "Fecha formula";
                Sheet.Cells["AA1"].Value = "Num unidades entregadas";
                Sheet.Cells["AB1"].Value = "Vlr unitario und entregada";
                Sheet.Cells["AC1"].Value = "Valor Iva";
                Sheet.Cells["AD1"].Value = "Valor total";
                Sheet.Cells["AE1"].Value = "Codigo ATC";
                Sheet.Cells["AF1"].Value = "Grupo farmacologico";

                //Hasta aqui va la estructura como tal, los demas son campos agregados
                Sheet.Cells["AG1"].Value = "Diferencia";
                Sheet.Cells["AH1"].Value = "Observaciones";
                Sheet.Cells["AI1"].Value = "Nuevo valor unitario";
                Sheet.Cells["AJ1"].Value = "Nuevo valor IVA";
                Sheet.Cells["AK1"].Value = "Cruza valor con LTE";
                Sheet.Cells["AL1"].Value = "Existe beneficiario";
                Sheet.Cells["AM1"].Value = "Cruce medicamento regulado";
                Sheet.Cells["AN1"].Value = "Factura duplicada histórico";
                Sheet.Cells["AO1"].Value = "Factura duplicada cargue";
                Sheet.Cells["AP1"].Value = "Observación desaprobación";
                Sheet.Cells["AQ1"].Value = "Cargue prefactura historica";
                Sheet.Cells["AR1"].Value = "Valor total acorde";
                Sheet.Cells["AS1"].Value = "Unis";
                Sheet.Cells["AT1"].Value = "Localidad";

                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                int row = 2;

                foreach (var detalleprefactura in Listado)
                {
                    double ValorUno = Convert.ToDouble(detalleprefactura.vlr_unitario_und_entregada);
                    double ValorDos = Convert.ToDouble(detalleprefactura.nuevo_valor);

                    double resultado = ValorUno - ValorDos;

                    Sheet.Cells[string.Format("A{0}", row)].Value = detalleprefactura.Id_prefactura_cargue_base;
                    Sheet.Cells[string.Format("B{0}", row)].Value = detalleprefactura.id_detalle_prefactura;
                    Sheet.Cells[string.Format("C{0}", row)].Value = detalleprefactura.remision_prefactura_fact;
                    Sheet.Cells[string.Format("D{0}", row)].Value = detalleprefactura.num_tirilla;
                    Sheet.Cells[string.Format("E{0}", row)].Value = detalleprefactura.fecha_radicacion;
                    Sheet.Cells[string.Format("F{0}", row)].Value = detalleprefactura.nit;
                    Sheet.Cells[string.Format("G{0}", row)].Value = detalleprefactura.tipo_id_beneficiario;
                    Sheet.Cells[string.Format("H{0}", row)].Value = detalleprefactura.num_documento_beneficiario;
                    Sheet.Cells[string.Format("I{0}", row)].Value = detalleprefactura.nombre_beneficiario;
                    Sheet.Cells[string.Format("J{0}", row)].Value = detalleprefactura.nombre_beneficiario_especial;
                    Sheet.Cells[string.Format("K{0}", row)].Value = detalleprefactura.ciudad_despacho;
                    Sheet.Cells[string.Format("L{0}", row)].Value = detalleprefactura.id_prescriptor;
                    Sheet.Cells[string.Format("M{0}", row)].Value = detalleprefactura.prescriptor;
                    Sheet.Cells[string.Format("N{0}", row)].Value = detalleprefactura.especialidad;
                    Sheet.Cells[string.Format("O{0}", row)].Value = detalleprefactura.cum;
                    Sheet.Cells[string.Format("P{0}", row)].Value = detalleprefactura.cod_ecopetrol_hijo_comercial;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = detalleprefactura.cod_interno_medicamento;
                    Sheet.Cells[string.Format("R{0}", row)].Value = detalleprefactura.cod_generico_o_interno_medicamento;
                    Sheet.Cells[string.Format("S{0}", row)].Value = detalleprefactura.Presentacion;
                    Sheet.Cells[string.Format("T{0}", row)].Value = detalleprefactura.descripcion_producto;
                    Sheet.Cells[string.Format("U{0}", row)].Value = detalleprefactura.nombre_comercial_medicacmento;
                    Sheet.Cells[string.Format("V{0}", row)].Value = detalleprefactura.laboratorio_fabricante;
                    Sheet.Cells[string.Format("W{0}", row)].Value = detalleprefactura.fecha_despacho_formula;
                    Sheet.Cells[string.Format("X{0}", row)].Value = detalleprefactura.num_unidades_prescritas;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = detalleprefactura.num_formula;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = detalleprefactura.fecha_formula;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = detalleprefactura.num_unidades_entregadas;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = detalleprefactura.vlr_unitario_und_entregada;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = detalleprefactura.IVA;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = detalleprefactura.valor_total;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = detalleprefactura.CODIGO_ATC;
                    Sheet.Cells[string.Format("AF{0}", row)].Value = detalleprefactura.grupo_farmacologico;
                    Sheet.Cells[string.Format("AG{0}", row)].Value = resultado;

                    if (String.IsNullOrEmpty(detalleprefactura.observaciones))
                    {
                        Sheet.Cells[string.Format("AH{0}", row)].Value = "";
                    }
                    else
                    {
                        Sheet.Cells[string.Format("AH{0}", row)].Value = detalleprefactura.observaciones.ToUpper();
                    }

                    Sheet.Cells[string.Format("AI{0}", row)].Value = 0;
                    Sheet.Cells[string.Format("AJ{0}", row)].Value = 0;
                    Sheet.Cells[string.Format("AK{0}", row)].Value = detalleprefactura.mensajeValor;
                    Sheet.Cells[string.Format("AL{0}", row)].Value = detalleprefactura.existeBeneficiarioMensaje;
                    Sheet.Cells[string.Format("AM{0}", row)].Value = detalleprefactura.MensajeMedicamentoRegulado;
                    Sheet.Cells[string.Format("AN{0}", row)].Value = detalleprefactura.mensajeHistoricoFactura;
                    Sheet.Cells[string.Format("AO{0}", row)].Value = detalleprefactura.mensajeDuplicidadCargue;
                    Sheet.Cells[string.Format("AP{0}", row)].Value = detalleprefactura.observacion_desaprobacion;
                    Sheet.Cells[string.Format("AQ{0}", row)].Value = detalleprefactura.duplicado_cargues_anteriores;
                    Sheet.Cells[string.Format("AR{0}", row)].Value = detalleprefactura.mensajeValorTotalNumeroUnidades;
                    Sheet.Cells[string.Format("AS{0}", row)].Value = detalleprefactura.unis;
                    Sheet.Cells[string.Format("AT{0}", row)].Value = detalleprefactura.localidad;

                    Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("W{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("Z{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    row++;
                }

                var prefactura = Session["idPrefacturaBase"];

                string namefile = "ReportePreFacturas_NoAprobadas_" + prefactura + "_" + DateTime.Now;
                Sheet.Cells["A:AT"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/xls";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('ERROR EN LA DESCARGA DE NO APROBADOS.');" +
                   "</script> ";

                Response.Write(rta);
            }
        }

        public void ExportarExcelAprobados()
        {
            List<management_listadoPrefacturasCruzanResult> Listado = new List<management_listadoPrefacturasCruzanResult>();
            try
            {
                Listado = (List<management_listadoPrefacturasCruzanResult>)Session["aprobadosIni"];
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Cargue");
                GestionMedicamentos Model = new GestionMedicamentos();

                Sheet.Cells["A1:AU1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:AU1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AU1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AU1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AU1"].Style.Font.Name = "Century Gothic";
                //Sheet.Protection.SetPassword("sami2023*");
                //Sheet.Protection.IsProtected = true;
                //Sheet.Cells["AO2:AO999999"].Style.Locked = false;
                Sheet.Cells["A1"].Value = "Nro cargue";
                Sheet.Cells["B1"].Value = "Nro registro";
                Sheet.Cells["C1"].Value = "Remision pre factura fact";
                Sheet.Cells["D1"].Value = "Num Tirilla o consecutivo";
                Sheet.Cells["E1"].Value = "Fecha radicacion";
                Sheet.Cells["F1"].Value = "Nit";
                Sheet.Cells["G1"].Value = "Tipo doc beneficiario";
                Sheet.Cells["H1"].Value = "Num documento beneficiario";
                Sheet.Cells["I1"].Value = "Nombre beneficiario";
                Sheet.Cells["J1"].Value = "Nombre beneficiario especial";
                Sheet.Cells["K1"].Value = "Ciudad despacho";
                Sheet.Cells["L1"].Value = "Id prescriptor";
                Sheet.Cells["M1"].Value = "Prescriptor";
                Sheet.Cells["N1"].Value = "Especialidad";
                Sheet.Cells["O1"].Value = "Cum";
                Sheet.Cells["P1"].Value = "Cod Ecopetrol hijo comercial";
                Sheet.Cells["Q1"].Value = "Cod interno medicamento";
                Sheet.Cells["R1"].Value = "Cod generico o interno medicamento";
                Sheet.Cells["S1"].Value = "Presentacion";
                Sheet.Cells["T1"].Value = "Descripcion producto";
                Sheet.Cells["U1"].Value = "Nombre comercial medicamento";
                Sheet.Cells["V1"].Value = "Laboratorio fabricante";
                Sheet.Cells["W1"].Value = "Fecha despacho formula";
                Sheet.Cells["X1"].Value = "Num unidades prescritas";
                Sheet.Cells["Y1"].Value = "Num formula";
                Sheet.Cells["Z1"].Value = "Fecha formula";
                Sheet.Cells["AA1"].Value = "Num unidades entregadas";
                Sheet.Cells["AB1"].Value = "Vlr unitario und entregada";
                Sheet.Cells["AC1"].Value = "Valor Iva";
                Sheet.Cells["AD1"].Value = "Valor total";
                Sheet.Cells["AE1"].Value = "Codigo ATC";
                Sheet.Cells["AF1"].Value = "Grupo farmacologico";

                //Hasta aqui va la estructura como tal, los demas son campos agregados
                Sheet.Cells["AG1"].Value = "Diferencia";
                Sheet.Cells["AH1"].Value = "Observaciones";
                Sheet.Cells["AI1"].Value = "Nuevo valor unitario";
                Sheet.Cells["AJ1"].Value = "Nuevo valor IVA";
                Sheet.Cells["AK1"].Value = "Nuevo valor total";
                Sheet.Cells["AL1"].Value = "Cruza valor con LTE";
                Sheet.Cells["AM1"].Value = "Existe beneficiario";
                Sheet.Cells["AN1"].Value = "Cruce medicamento regulado";
                Sheet.Cells["AO1"].Value = "Factura duplicada histórico";
                Sheet.Cells["AP1"].Value = "Factura duplicada cargue";
                Sheet.Cells["AQ1"].Value = "Valor total acorde";
                Sheet.Cells["AR1"].Value = "Cargue prefactura historica";
                Sheet.Cells["AS1"].Value = "Observacion desaprobación";
                Sheet.Cells["AT1"].Value = "Unis";
                Sheet.Cells["AU1"].Value = "Localidad";


                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                int row = 2;
                foreach (var detalleprefactura in Listado)
                {
                    double ValorUno = Convert.ToDouble(detalleprefactura.vlr_unitario_und_entregada);
                    double ValorDos = Convert.ToDouble(detalleprefactura.nuevo_valor);
                    double resultado = ValorUno - ValorDos;

                    Sheet.Cells[string.Format("A{0}", row)].Value = detalleprefactura.Id_prefactura_cargue_base;
                    Sheet.Cells[string.Format("B{0}", row)].Value = detalleprefactura.id_detalle_prefactura;
                    Sheet.Cells[string.Format("C{0}", row)].Value = detalleprefactura.remision_prefactura_fact;
                    Sheet.Cells[string.Format("D{0}", row)].Value = detalleprefactura.num_tirilla;
                    Sheet.Cells[string.Format("E{0}", row)].Value = detalleprefactura.fecha_radicacion;
                    Sheet.Cells[string.Format("F{0}", row)].Value = detalleprefactura.nit;
                    Sheet.Cells[string.Format("G{0}", row)].Value = detalleprefactura.tipo_id_beneficiario;
                    Sheet.Cells[string.Format("H{0}", row)].Value = detalleprefactura.num_documento_beneficiario;
                    Sheet.Cells[string.Format("I{0}", row)].Value = detalleprefactura.nombre_beneficiario;
                    Sheet.Cells[string.Format("J{0}", row)].Value = detalleprefactura.nombre_beneficiario_especial;
                    Sheet.Cells[string.Format("K{0}", row)].Value = detalleprefactura.ciudad_despacho;
                    Sheet.Cells[string.Format("L{0}", row)].Value = detalleprefactura.id_prescriptor;
                    Sheet.Cells[string.Format("M{0}", row)].Value = detalleprefactura.prescriptor;
                    Sheet.Cells[string.Format("N{0}", row)].Value = detalleprefactura.especialidad;
                    Sheet.Cells[string.Format("O{0}", row)].Value = detalleprefactura.cum;
                    Sheet.Cells[string.Format("P{0}", row)].Value = detalleprefactura.cod_ecopetrol_hijo_comercial;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = detalleprefactura.cod_interno_medicamento;
                    Sheet.Cells[string.Format("R{0}", row)].Value = detalleprefactura.cod_generico_o_interno_medicamento;
                    Sheet.Cells[string.Format("S{0}", row)].Value = detalleprefactura.Presentacion;
                    Sheet.Cells[string.Format("T{0}", row)].Value = detalleprefactura.descripcion_producto;
                    Sheet.Cells[string.Format("U{0}", row)].Value = detalleprefactura.nombre_comercial_medicacmento;
                    Sheet.Cells[string.Format("V{0}", row)].Value = detalleprefactura.laboratorio_fabricante;
                    Sheet.Cells[string.Format("W{0}", row)].Value = detalleprefactura.fecha_despacho_formula;
                    Sheet.Cells[string.Format("X{0}", row)].Value = detalleprefactura.num_unidades_prescritas;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = detalleprefactura.num_formula;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = detalleprefactura.fecha_formula;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = detalleprefactura.num_unidades_entregadas;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = detalleprefactura.vlr_unitario_und_entregada;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = detalleprefactura.IVA;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = detalleprefactura.valor_total;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = detalleprefactura.CODIGO_ATC;
                    Sheet.Cells[string.Format("AF{0}", row)].Value = detalleprefactura.grupo_farmacologico;
                    Sheet.Cells[string.Format("AG{0}", row)].Value = resultado;

                    if (String.IsNullOrEmpty(detalleprefactura.observaciones))
                    {
                        Sheet.Cells[string.Format("AH{0}", row)].Value = "";
                    }
                    else
                    {
                        Sheet.Cells[string.Format("AH{0}", row)].Value = detalleprefactura.observaciones.ToUpper();
                    }

                    Sheet.Cells[string.Format("AI{0}", row)].Value = detalleprefactura.nuevo_valor;
                    Sheet.Cells[string.Format("AJ{0}", row)].Value = detalleprefactura.nuevo_iva;
                    Sheet.Cells[string.Format("AK{0}", row)].Value = detalleprefactura.totalConIva;
                    Sheet.Cells[string.Format("AL{0}", row)].Value = detalleprefactura.mensajeValor;
                    Sheet.Cells[string.Format("AM{0}", row)].Value = detalleprefactura.existeBeneficiarioMensaje;
                    Sheet.Cells[string.Format("AN{0}", row)].Value = detalleprefactura.MensajeMedicamentoRegulado;
                    Sheet.Cells[string.Format("AO{0}", row)].Value = detalleprefactura.mensajeHistoricoFactura;
                    Sheet.Cells[string.Format("AP{0}", row)].Value = detalleprefactura.mensajeDuplicidadCargue;
                    Sheet.Cells[string.Format("AQ{0}", row)].Value = detalleprefactura.mensajeValorTotalNumeroUnidades;
                    Sheet.Cells[string.Format("AR{0}", row)].Value = detalleprefactura.duplicado_cargues_anteriores;
                    Sheet.Cells[string.Format("AS{0}", row)].Value = "";
                    Sheet.Cells[string.Format("AT{0}", row)].Value = detalleprefactura.unis;
                    Sheet.Cells[string.Format("AU{0}", row)].Value = detalleprefactura.localidad;

                    Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("W{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("Z{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }
                var prefactura = Session["idPrefacturaBase"];

                string namefile = "ReportePreFacturasAprobadas_" + prefactura + "_" + DateTime.Now;
                Sheet.Cells["A:AU"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('ERROR EN LA DESCARGA DE APROBADOS.');" +
                  "</script> ";
                Response.Write(rta);
            }
        }

        public JsonResult UpdatedetPrefactura(int id_detprefactura, string observaciones, double nuevo_valor)
        {

            GestionMedicamentos Model = new GestionMedicamentos();
            try
            {
                var prefactura = Model.GetPrefacturasID(id_detprefactura);
                prefactura.observaciones = observaciones;
                prefactura.nuevo_valor = Convert.ToDecimal(nuevo_valor);
                prefactura.aprobado = true;
                prefactura.desaprobada = 0;
                Model.ActualizarPrefactura(prefactura);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(0);
        }

        public JsonResult UpdatedetPrefacturaList(List<int> ListaIds, int? idCargue, string observaciones, double nuevo_valor)
        {
            GestionMedicamentos Model = new GestionMedicamentos();
            var mensaje = "";
            List<log_prefacturas_aprobacion> listaLog = new List<log_prefacturas_aprobacion>();

            try
            {
                if (ListaIds != null)
                {
                    var resultado = Model.ActualizarPrefacturaLista(ListaIds, observaciones, nuevo_valor);

                    if (resultado != 0)
                    {
                        var conteoLog = 0;
                        log_prefacturas_aprobacion obj = new log_prefacturas_aprobacion();
                        obj.id_cargue = idCargue;
                        obj.observacion = observaciones;
                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = SesionVar.UserName;

                        foreach (var item in ListaIds)
                        {
                            obj.id_detalle = item;
                            listaLog.Add(obj);
                            conteoLog++;
                        }

                        if (conteoLog > 0 && listaLog.Count() > 0)
                        {
                            var respuestaLog = BusClass.guardarLogAprobacionPrefacturas(listaLog);
                        }
                        mensaje = "PREFACTURAS APROBADAS CORRECTAMENTE";
                    }
                    else
                    {
                        mensaje = "ERROR EN LA APROBACIÓN DE PREFACTURAS";
                    }
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                mensaje = "ERROR EN LA APROBACIÓN DE PREFACTURAS: " + erro;

            }
            return Json(new { mensaje = mensaje });
        }

        public JsonResult UpdatedetPrefacturaListTotal(int idCargue, string observaciones, double nuevo_valor)
        {
            GestionMedicamentos Model = new GestionMedicamentos();
            if (idCargue != 0)
            {
                Model.ActualizarPrefacturaListaTotal(idCargue, observaciones, nuevo_valor);
            }
            return Json(0);
        }

        //public JsonResult AprobacionMasivaExcel(int? idPrefactura, string observaciones, HttpPostedFileBase file)
        //{
        //    var mensaje = "";
        //    var pasa = false;

        //    GestionMedicamentos Model = new GestionMedicamentos();
        //    var fileName = "";
        //    var path = "";
        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            try
        //            {
        //                string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivosPrefacturas"];
        //                //string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivosServer"];

        //                fileName = Path.GetFileName(file.FileName);
        //                path = Path.Combine(ruta, fileName);

        //                if (!Directory.Exists(ruta))
        //                {
        //                    Directory.CreateDirectory(ruta);
        //                }

        //                file.SaveAs(path);

        //                log_prefacturas_aprobacionMasiva mas = new log_prefacturas_aprobacionMasiva();
        //                mas.fecha_digita = DateTime.Now;

        //                var ultimoLogAnterior = BusClass.TraerUltimoCargueLogAprobacion();
        //                ultimoLogAnterior = ultimoLogAnterior + 1;

        //                HttpClient httpClient = new HttpClient();
        //                httpClient.Timeout = TimeSpan.FromHours(2);

        //                var finaliza = Model.ExcelAPrefacturasReaprobacion(path, ultimoLogAnterior, idPrefactura, observaciones, ref MsgRes);

        //                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto && finaliza != 0)
        //                {
        //                    mas.id_cargue = idPrefactura;
        //                    mas.usuario_digita = SesionVar.UserName;

        //                    httpClient.Timeout = TimeSpan.FromHours(2);

        //                    var idLog = BusClass.GuardarLogAprobacionMasiva(mas);

        //                    httpClient.Timeout = TimeSpan.FromHours(2);

        //                    var masivo = BusClass.GuardarLogDatosAprobacionMasiva(idPrefactura, idLog, SesionVar.UserName);

        //                    httpClient.Timeout = TimeSpan.FromHours(2);

        //                    mensaje = "REGISTROS ACTUALIZADOS CORRECTAMENTE";
        //                    pasa = true;
        //                }
        //                else
        //                {
        //                    mensaje = MsgRes.DescriptionResponse;
        //                }

        //                FileInfo fileDelete = new FileInfo(path);
        //                if (fileDelete != null)
        //                {
        //                    fileDelete.Delete();
        //                }

        //                return Json(new { success = pasa, message = mensaje }, JsonRequestBehavior.AllowGet);
        //            }
        //            catch (Exception e)
        //            {
        //                FileInfo fileDelete = new FileInfo(path);
        //                if (fileDelete != null)
        //                {
        //                    fileDelete.Delete();
        //                }

        //                var msg = MsgRes.DescriptionResponse;
        //                if (msg == null || msg == "")
        //                {
        //                    mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + e.Message;
        //                }
        //                else
        //                {
        //                    mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + MsgRes.DescriptionResponse;
        //                }
        //                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            FileInfo fileDelete = new FileInfo(path);
        //            if (fileDelete != null)
        //            {
        //                fileDelete.Delete();
        //            }

        //            mensaje = "FALTA UN ARCHIVO EN FORMATO EXCEL.";
        //            return Json(new
        //            {
        //                success = false,
        //                message = mensaje
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var msg = MsgRes.DescriptionResponse;
        //        if (msg == null || msg == "")
        //        {
        //            mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + e.Message;
        //        }
        //        else
        //        {
        //            mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + MsgRes.DescriptionResponse;
        //        }

        //        FileInfo fileDelete = new FileInfo(path);
        //        if (fileDelete != null)
        //        {
        //            fileDelete.Delete();
        //        }

        //        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult AprobacionMasivaExcel(int? idPrefactura, string observaciones, HttpPostedFileBase file)
        {
            var mensaje = "";
            var pasa = false;

            GestionMedicamentos Model = new GestionMedicamentos();
            var fileName = "";
            try
            {
                if (file.ContentLength > 0)
                {
                    try
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


                        log_prefacturas_aprobacionMasiva mas = new log_prefacturas_aprobacionMasiva();
                        mas.fecha_digita = DateTime.Now;

                        var ultimoLogAnterior = BusClass.TraerUltimoCargueLogAprobacion();
                        ultimoLogAnterior = ultimoLogAnterior + 1;

                        HttpClient httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromHours(2);

                        var finaliza = Model.ExcelAPrefacturasReaprobacion(dataTable, ultimoLogAnterior, idPrefactura, observaciones, ref MsgRes);

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto && finaliza != 0)
                        {
                            mas.id_cargue = idPrefactura;
                            mas.usuario_digita = SesionVar.UserName;

                            httpClient.Timeout = TimeSpan.FromHours(2);

                            var idLog = BusClass.GuardarLogAprobacionMasiva(mas);

                            httpClient.Timeout = TimeSpan.FromHours(2);

                            var masivo = BusClass.GuardarLogDatosAprobacionMasiva(idPrefactura, idLog, SesionVar.UserName);

                            httpClient.Timeout = TimeSpan.FromHours(2);

                            mensaje = "REGISTROS ACTUALIZADOS CORRECTAMENTE";
                            pasa = true;
                        }
                        else
                        {
                            mensaje = MsgRes.DescriptionResponse;
                        }

                        return Json(new { success = pasa, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        var msg = MsgRes.DescriptionResponse;
                        if (msg == null || msg == "")
                        {
                            mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + e.Message;
                        }
                        else
                        {
                            mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + MsgRes.DescriptionResponse;
                        }
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "FALTA UN ARCHIVO EN FORMATO EXCEL.";
                    return Json(new
                    {
                        success = false,
                        message = mensaje
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                var msg = MsgRes.DescriptionResponse;
                if (msg == null || msg == "")
                {
                    mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + e.Message;
                }
                else
                {
                    mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + MsgRes.DescriptionResponse;
                }

                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DesaprobacionMasivaExcel(int? idPrefactura, string observaciones, HttpPostedFileBase file)
        {
            var mensaje = "";

            GestionMedicamentos Model = new GestionMedicamentos();
            var fileName = "";
            var pasa = false;
            var path = "";
            try
            {
                if (file.ContentLength > 0)
                {
                    try
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

                        log_prefacturas_desaprobacionMasiva mas = new log_prefacturas_desaprobacionMasiva();
                        mas.fecha_digita = DateTime.Now;

                        var ultimoLogAnterior = BusClass.TraerUltimoCargueLogDesaprobacion();
                        ultimoLogAnterior = ultimoLogAnterior + 1;

                        HttpClient httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromHours(2);

                        var finaliza = Model.ExcelAPrefacturasDesaprobacion(dataTable, ultimoLogAnterior, idPrefactura, observaciones, ref MsgRes);

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto && finaliza != 0)
                        {
                            mas.id_cargue = idPrefactura;
                            mas.usuario_digita = SesionVar.UserName;

                            httpClient.Timeout = TimeSpan.FromHours(2);

                            var idLog = BusClass.GuardarLogDesaprobacionMasiva(mas);

                            var masivo = BusClass.GuardarLogDatosDesaprobacionMasiva(idPrefactura, idLog, SesionVar.UserName);

                            mensaje = "REGISTROS ACTUALIZADOS CORRECTAMENTE";
                            pasa = true;
                        }
                        else
                        {
                            mensaje = MsgRes.DescriptionResponse;
                        }

                        return Json(new { success = pasa, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        var msg = MsgRes.DescriptionResponse;
                        if (msg == null || msg == "")
                        {
                            mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + e.Message;
                        }
                        else
                        {
                            mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + MsgRes.DescriptionResponse;
                        }
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "FALTA UN ARCHIVO EN FORMATO EXCEL.";
                    return Json(new
                    {
                        success = false,
                        message = mensaje
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                var msg = MsgRes.DescriptionResponse;
                if (msg == null || msg == "")
                {
                    mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + e.Message;
                }
                else
                {
                    mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + MsgRes.DescriptionResponse;
                }

                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        /*Requerimiento de dispensacion*/

        public ActionResult CargueDispensacion()
        {
            try
            {

                ViewBag.meses = BusClass.meses();
                ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewData["Rta"] = 0;
                ViewData["Mensaje"] = "";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult CargueDispensacion(int prestador, int mes, int año, int regional, HttpPostedFileBase file)
        {
            dispensaciones modelo = new dispensaciones();

            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Resources"), fileName);
                file.SaveAs(path);
                dispensacion_cargue_base cargue = new dispensacion_cargue_base();
                cargue.prestador_id = prestador;
                cargue.id_regional = regional;
                cargue.mes = mes;
                cargue.año = año;
                cargue.fecha_digita = DateTime.Now;
                cargue.usuario_digita = SesionVar.UserName;
                List<dispensacion_cargue_base_dtll> Listado = ExceltoLista(path);
                modelo.InsertarCargueMasivoDispensaciones(cargue, Listado, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    ViewData["Rta"] = 1;
                    ViewData["Mensaje"] = "Los datos han sido guardados exitosamente!";

                }
                else
                {
                    ViewData["Rta"] = 2;
                    ViewData["Mensaje"] = MsgRes.DescriptionResponse;

                }


            }
            catch (Exception ex)
            {

                ViewData["Rta"] = 2;
                if (ex.Message.Contains("is not a valid worksheet name in file"))
                {
                    ViewData["Mensaje"] = "El nombre de la hoja de calculo es invalido. El nombre correcto debe ser (Base de dispensación)";
                }
                else
                {
                    ViewData["Mensaje"] = ex.Message;
                }

            }

            ViewBag.meses = BusClass.meses();
            ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
            ViewBag.regionales = BusClass.GetRefRegion();

            return View();
        }

        public List<dispensacion_cargue_base_dtll> ExceltoLista(string path)
        {
            var resultado = new List<dispensacion_cargue_base_dtll>();
            var book = new ExcelQueryFactory(path);

            try
            {
                resultado = (from row in book.Worksheet("Base de dispensación")
                             let item = new dispensacion_cargue_base_dtll
                             {
                                 tipo_identificacion_beneficiario = row["TIPO_DE_IDENTIFICACIÓN_DEL_BENEFICIARIO"],
                                 num_identificacion_beneficiario = row["NÚMERO_DE_DOCUMENTO_DE_IDENTIFICACIÓN_DEL_BENEFICIARIO"],
                                 nombre_beneficiario = row["NOMBRE_DEL_BENEFICIARIO"],
                                 regional_de_salud = row["REGIONAL_DE_SALUD"],
                                 fecha_factura = Convert.ToDateTime(row["FECHA_DE_LA_FACTURA"]),
                                 num_factura = row["NÚMERO_DE_FACTURA"],
                                 id_prescriptor = row["ID_PRESCRIPTOR"],
                                 nombre_prescriptor = row["NOMBRE_DE_PRESCRIPTOR"],
                                 especialidad = row["Especialidad"],
                                 CUM = Convert.ToInt32(row["Cum"]),
                                 consecutivo_CUM = row["CONSECUTIVO_DEL_CUM"],
                                 forma_farmaceutica = row["FORMA_FARMACÉUTICA"],
                                 concentracion = row["CONCENTRACIÓN"],
                                 presentacion = row["PRESENTACIÓN"],
                                 descripcion_producto = row["DESCRIPCIÓN_PRODUCTO"],
                                 nom_comercial_medicamento = row["NOMBRE_COMERCIAL_DEL_MEDICAMENTO"],
                                 laboratorio_fabricante = row["LABORATORIO_FABRICANTE"],
                                 registro_sanitario = row["REGISTRO_SANITARIO"],
                                 laboratorio_titular_registro_sanitario = row["LABORATORIO_TITULAR_DEL_REGISTRO_SANITARIO"],
                                 fecha_despacho_formula = Convert.ToDateTime(row["FECHA_DE_DESPACHO_DE_LA_FÓRMULA"]),
                                 unidad_de_entrega = row["UNIDAD_DE_ENTREGA"],
                                 numero_unidades_entrega = Convert.ToInt32(row["NÚMERO_DE_UNIDADES_ENTREGADAS"]),
                                 valor_unitario_entrega = Convert.ToDecimal(row["VALOR_UNITARIO_DE_LA_UNIDAD_ENTREGADA"]),
                                 valor_total = Convert.ToDecimal(row["VALOR_TOTAL"]),
                                 consecutivo_tiquete = row["CONSECUTIVO_TIQUETE"],
                                 codigo_ATC = row["CÓDIGO_ATC"],
                                 centro_dispensacion_farmacia_o_drogueria = row["CENTRO_DE_DISPENSACIÓN_FARMACIA_O_DROGUERÍA"],
                                 lupe_LTE = row["LUPE_(LTE)"],
                                 ABE = row["TCP"],
                                 clasificacion_acuerdo_base_economica = row["CLASIFICACION_ACUERDO_BASE_ECONOMICA"],
                                 concatena_descripcion = row["CONCATENA_DESCRIPCION"],
                                 mes_reporte_fact = row["MES_REPORTE_FACT"],
                                 ciudad_beneficiario = row["CIUDAD_DE_BENEFICIARIO"],
                                 ciudad_despacho = row["CIUDAD_DE_DESPACHO"],
                                 tipo_de_cobro = Convert.ToInt32(row["TIPO_DE_COBRO"]),
                                 comercial_o_generico = row["¿COMERCIAL_O_GENÉRICO?"],
                                 codigo_interno_medicamento = row["CÓDIGO_INTERNO_DE_MEDICAMENTO_(EL_QUE_UTILICE_EL_OPERADOR)"],
                                 IVA = Convert.ToDecimal(row["IVA"]),
                                 grupo_farmacologico_ATC = row["GRUPO_FARMACOLOGICO_ATC"],
                                 expediente = row["EXPEDIENTE"],
                                 clasificacion_producto = row["CLASIFICACIÓN_PRODUCTO"],
                                 mes_de_despacho = row["MES_DE_DESPACHO"],
                                 año_despacho = Convert.ToInt32(row["AÑO"]),
                                 ambito = row["AMBITO"],
                                 codigo_ecopetrol_padre = row["CÓDIGO_ECOPETROL_(PADRE)"],
                                 codigo_ecopetrol_hijo = row["CÓDIGO_HIJO_ECOPETROL"],
                                 codigo_und_minima = row["CÓDIGO_UNIDAD_MINIMA"],
                                 prestador = row["PRESTADOR"],
                                 mes = row["MES"],
                                 regional = row["REGIONAL"],
                                 año_bd = Convert.ToInt32(row["AÑO BD"]),
                             }
                             select item).ToList();
                book.Dispose();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return resultado;
        }

        public JsonResult AgregarIntermediacion(int id_regional, decimal inter_abe, decimal inter_fc)
        {
            int rta = 0;
            List<med_intermediacion> listado = new List<med_intermediacion>();
            try
            {

                listado = (List<med_intermediacion>)Session["Intermediaciones"];

                med_intermediacion registro = new med_intermediacion();

                if (listado != null)
                {
                    registro = listado.Where(l => l.id_regional == id_regional).FirstOrDefault();
                }

                if (registro == null)
                {
                    var regional = BusClass.GetRefRegion().Where(l => l.id_ref_regional == id_regional).FirstOrDefault();
                    med_intermediacion obj = new med_intermediacion();
                    obj.id_regional = id_regional;
                    obj.Indice_regional = regional.indice;
                    obj.porcentaje_ABE = inter_abe;
                    obj.porcentaje_FC = inter_fc;
                    listado.Add(obj);
                    rta = 0;
                }
                else
                {
                    rta = 1;
                }

                Session["Intermediaciones"] = listado;


            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var data = new
            {
                intermediaciones = listado.Select(p => new { idreg = p.id_regional, coor = p.Indice_regional, abe = p.porcentaje_ABE, fc = p.porcentaje_FC }),
                rta = rta,
                contador = listado.Count
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QuitarIntermediacion(int id_regional)
        {
            List<med_intermediacion> listado = new List<med_intermediacion>();
            try
            {
                listado = (List<med_intermediacion>)Session["Intermediaciones"];
                var registro = listado.Where(l => l.id_regional == id_regional).FirstOrDefault();
                listado.Remove(registro);
                Session["Intermediaciones"] = listado;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var data = new
            {
                intermediaciones = listado.Select(p => new { idreg = p.id_regional, coor = p.Indice_regional, abe = p.porcentaje_ABE, fc = p.porcentaje_FC }),
                contador = listado.Count
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // tableros alejandro

        public ActionResult TableroControlInterventoria()
        {
            try
            {
                ViewBag.listageneral = BusClass.Getinterventoriagneral().ToList();
                ViewBag.meses = BusClass.meses();
                ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewData["Rta"] = 0;
                ViewData["Mensaje"] = "";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult _DetalleInterventoria(Int32? ID)
        {
            Models.Medicamentos.Cuentas Model = new Cuentas();
            try
            {

                List<vw_md_tablero_intenventoria_general_detalle1> lista1 = BusClass.Detalleinterventoria1(Convert.ToInt32(ID)).ToList();
                List<vw_md_tablero_interventoria_general_detalle2> lista2 = BusClass.Detalleinterventoria2(Convert.ToInt32(ID)).ToList();
                List<vw_md_tablero_interventoria_general_detalle3> lista3 = BusClass.Detalleinterventoria3(Convert.ToInt32(ID)).ToList();
                List<vw_md_tablero_interventoria_general_detalle4> lista4 = BusClass.Detalleinterventoria4(Convert.ToInt32(ID)).ToList();

                ViewBag.lista1 = lista1;
                ViewBag.lista2 = lista2;
                ViewBag.lista3 = lista3;
                ViewBag.lista4 = lista4;

                ViewBag.idrole = SesionVar.ROL;
                ViewData["alerta"] = "";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView(Model);
        }

        public ActionResult AsignacionCuentas()
        {

            return View();
        }

        [HttpPost]
        public JsonResult SaveAsignacionCuentas()
        {
            String mensaje = "";
            String tipoAlerta = "";
            Models.Medicamentos.Cuentas Model = new Cuentas();
            try
            {
                List<vw_md_consolidado_sin_auditor> list = BusClass.Getfactursinauditor().ToList();

                foreach (var item in list)
                {
                    String factura = "";
                    factura = item.numero_factura;

                    BusClass.GetAsignarAuditorConsolidado(factura, ref MsgRes);
                }
                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {

                    mensaje = "SE ACTUALIZO CORRECTAMENTE....";
                    tipoAlerta = "2";

                    return Json(new { success = true, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    mensaje = "*---EL ARCHIVO NO CUMPLE CON EL FORMATO ESTABLECIDO PARA EL CARGUE -- - *";
                    tipoAlerta = "3";

                    return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                mensaje = "*---ERROR -- - *" + ex.Message;
                tipoAlerta = "3";

                return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CargueMedicamentosFacturacion()
        {
            try
            {
                ViewBag.meses = BusClass.meses();
                ViewBag.regional = BusClass.GetRefRegion();
                ViewData["rta"] = 0;
                ViewData["Msg"] = "";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }


        [HttpPost]
        public ActionResult CargueMedicamentosFacturacion(HttpPostedFileBase file, int regional, int mes, int año)
        {

            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();
            var path = "";
            var mensajeLog = "";
            log_cargues_masivos logMas = new log_cargues_masivos();

            try
            {
                List<managemente_medicamentos_facturacionResult> med = Model.Getmedicamentos_facturacion(mes, año, regional);
                if (med.Count == 0)
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

                    medicamentos_facturacion obj = new medicamentos_facturacion();
                    obj.mes = mes;
                    obj.año = año;
                    obj.regional = regional;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    List<medicamentos_facturacion_dtll> result = Model.EntidadMedicamentosFcturacionDtll(dataTable, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        var id = Model.GuardarMedicamentosFacturacion(obj, result, ref MsgRes);

                        logMas.id_cargue = id;

                        logMas.fecha_Cargue = DateTime.Now;
                        logMas.periodo_cargue = new DateTime((int)obj.año, (int)obj.mes, 1);
                        logMas.nombre_digita = SesionVar.NombreUsuario;
                        logMas.usuario_digita = SesionVar.UserName;
                        logMas.tipo_registro = "Facturación de medicamentos";

                        logMas.registros_Cargados = result.Count();

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            ViewData["rta"] = 1;
                            ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                            mensajeLog = "Cargue completado";
                        }
                        else
                        {
                            ViewData["rta"] = 2;
                            ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                            mensajeLog = "Error en el cargue";
                        }

                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                        mensajeLog = "Error en el cargue";
                    }
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = "Ya se encuentra cargada una base en el mismo periodo y para la misma regional";
                    mensajeLog = "Error en el cargue";
                }
            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
                mensajeLog = "Error en el cargue";
            }

            logMas.estado_cargue = mensajeLog;
            var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            return View();
        }

        private List<medicamentos_facturacion_dtll> EntidadMedicamentosFcturacionDtll(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            List<medicamentos_facturacion_dtll> result = new List<medicamentos_facturacion_dtll>();
            string columna = "";
            int fila = 1;
            try
            {
                var book = new ExcelQueryFactory(rutafichero);
                var EData1 = (from c in book.WorksheetRange("A1", "Q999999", "Medicamentos Facturacion") select c).ToList();

                for (var i = 0; i < EData1.Count; i++)
                {
                    medicamentos_facturacion_dtll obj = new medicamentos_facturacion_dtll();
                    var item = EData1[i];
                    fila++;
                    if (item[0] != "")
                    {
                        columna = "REGIONAL";
                        obj.regional = Convert.ToString(item[0]);

                        columna = "UNIS";
                        obj.unis = Convert.ToString(item[1]);

                        columna = "LOCALDAD";
                        obj.localidad = Convert.ToString(item[2]);

                        columna = "TIPO DE IDENTIFICACIÓN";
                        obj.tipo_identificacion = Convert.ToString(item[3]);

                        columna = "IDENTIFICACIÓN DEL PACIENTE";
                        obj.identificacion_paciente = Convert.ToString(item[4]);

                        columna = "Cum";
                        obj.cum = Convert.ToString(item[5]);

                        columna = "CONCEPTO";
                        obj.concepto = Convert.ToString(item[6]);

                        columna = "CANTIDAD";
                        obj.cantidad = Convert.ToInt32(item[7]);

                        columna = "FECHA DISPENSACIÓN";
                        obj.fecha_dispensacion = Convert.ToDateTime(item[8]);

                        columna = "FECHA FACTURA";
                        obj.fecha_factura = Convert.ToDateTime(item[9]);

                        columna = "NUMERO DE FACTURA";
                        obj.numero_factura = Convert.ToString(item[10]);

                        columna = "VALOR";
                        obj.valor = Convert.ToDecimal(item[11]);

                        columna = "NIT DEL PRESTADOR";
                        obj.nit_prestador = Convert.ToString(item[12]);

                        columna = "PRESTADOR";
                        obj.prestador = Convert.ToString(item[13]);

                        columna = "FACTURA SIN LETRAS";
                        obj.factura_sin_letras = Convert.ToString(item[14]);

                        columna = "FORMULA";
                        obj.formula = Convert.ToString(item[15]);

                        columna = "DOCUMENTO CONTABLE";
                        obj.documento_contable = Convert.ToString(item[16]);
                    }

                    result.Add(obj);
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
                book.Dispose();
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                MsgRes.CodeError = ex.Message;
            }

            return result;
        }

        public ActionResult TableroControlMdFacturacion()
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();
            List<ManagementMedicamentosFacturacionResult> List = new List<ManagementMedicamentosFacturacionResult>();

            try
            {
                Session["ListMedFacturacion"] = List;
                ViewBag.meses = BusClass.meses();
                ViewBag.regional = BusClass.GetRefRegion();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View(List);
        }

        public JsonResult GetTableromedfacturacion(int? mesinicio, int? añoinicio, int? mesfinal, int? añofin, string Prestador, string regional, int? page, int? limit)
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            ObjectCache cache = MemoryCache.Default;
            List<ManagementMedicamentosFacturacionResult> fileContents = cache["filecontents"] as List<ManagementMedicamentosFacturacionResult>;

            List<ManagementMedicamentosFacturacionResult> result = new List<ManagementMedicamentosFacturacionResult>();
            List<ManagementMedicamentosFacturacionResult> lista = new List<ManagementMedicamentosFacturacionResult>();
            List<ManagementMedicamentosFacturacionResult> lista3 = new List<ManagementMedicamentosFacturacionResult>();
            List<ManagementMedicamentosFacturacionResult> listaok = new List<ManagementMedicamentosFacturacionResult>();

            List<ManagementMedicamentosFacturacionResult> ListaTotal = (List<ManagementMedicamentosFacturacionResult>)Session["ListMedFacturacionTotal"];

            if (mesinicio == null && añoinicio == null && mesfinal == null && añofin == null && string.IsNullOrEmpty(Prestador) && string.IsNullOrEmpty(regional))
            {
                listaok = new List<ManagementMedicamentosFacturacionResult>();
                Session["ListMedFacturacion"] = listaok.ToList();
                Session["ListMedFacturacionTotal"] = listaok.ToList();
            }
            else
            {
                listaok = Model.GetMedicamentosFacturacionList(mesinicio, añoinicio, mesfinal, añofin, Prestador, regional);
            }
            lista = listaok;
            var query = lista.Select(item => new ManagementMedicamentosFacturacionResult
            {
                id_medicamentos_facturacion = item.id_medicamentos_facturacion,
                regional = item.regional,
                unis = item.unis,
                localidad = item.localidad,
                tipo_identificacion = item.tipo_identificacion,
                identificacion_paciente = item.identificacion_paciente,
                cum = item.cum,
                concepto = item.concepto,
                cantidad = item.cantidad,
                fecha_dispensacion = item.fecha_dispensacion,
                fecha_factura = item.fecha_factura,
                numero_factura = item.numero_factura,
                valor = item.valor,
                nit_prestador = item.nit_prestador,
                prestador = item.prestador,
                factura_sin_letras = item.factura_sin_letras,
                nombre_regional = item.nombre_regional,
                mes = item.mes,
                año = item.año,
                fecha_digita = item.fecha_digita,
                usuario_digita = item.usuario_digita
            });


            if (mesinicio != null && mesinicio != null)
            {
                query = query.Where(x => x.mes >= mesinicio).ToList();
                query = query.Where(x => x.año >= añoinicio).ToList();
            }

            if (mesfinal != null && añofin != null)
            {
                query = query.Where(x => x.mes <= mesfinal).ToList();
                query = query.Where(x => x.año <= añofin).ToList();
            }

            if (!string.IsNullOrEmpty(Prestador))
            {
                query = query.Where(q => q.prestador.ToUpper().Contains(Prestador.ToUpper()));
            }
            if (!string.IsNullOrEmpty(regional))
            {
                query = query.Where(q => q.regional.Contains(regional));
            }

            List<ManagementMedicamentosFacturacionResult> records = new List<ManagementMedicamentosFacturacionResult>();

            Session["ListMedFacturacion"] = query.ToList();

            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);

        }

        public void DescargarResultadosMdFacturacion()
        {
            try
            {

                List<ManagementMedicamentosFacturacionResult> List = (List<ManagementMedicamentosFacturacionResult>)Session["ListMedFacturacion"];

                if (List.Count != 0)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Medicamentos Facturacion");

                    Sheet.Cells["A1"].Value = "Regional";
                    Sheet.Cells["B1"].Value = "Unis";
                    Sheet.Cells["C1"].Value = "Localidad";
                    Sheet.Cells["D1"].Value = "Tipo identificacion";
                    Sheet.Cells["E1"].Value = "Identificacion Paciente";
                    Sheet.Cells["F1"].Value = "Cum";
                    Sheet.Cells["G1"].Value = "Concepto";
                    Sheet.Cells["H1"].Value = "Cantidad";
                    Sheet.Cells["I1"].Value = "Fecha Dispensacion";
                    Sheet.Cells["J1"].Value = "Fecha factura";
                    Sheet.Cells["K1"].Value = "Numero de factura";
                    Sheet.Cells["L1"].Value = "Valor";
                    Sheet.Cells["M1"].Value = "Nit del prestador";
                    Sheet.Cells["N1"].Value = "Prestador";
                    Sheet.Cells["O1"].Value = "Factura sin letras";
                    int row = 2;

                    foreach (var line in List)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = line.regional;
                        Sheet.Cells[string.Format("B{0}", row)].Value = line.unis;
                        Sheet.Cells[string.Format("C{0}", row)].Value = line.localidad;
                        Sheet.Cells[string.Format("D{0}", row)].Value = line.tipo_identificacion;
                        Sheet.Cells[string.Format("E{0}", row)].Value = line.identificacion_paciente;
                        Sheet.Cells[string.Format("F{0}", row)].Value = line.cum;
                        Sheet.Cells[string.Format("G{0}", row)].Value = line.concepto;
                        Sheet.Cells[string.Format("H{0}", row)].Value = line.cantidad;
                        Sheet.Cells[string.Format("I{0}", row)].Value = line.fecha_dispensacion;
                        Sheet.Cells[string.Format("J{0}", row)].Value = line.fecha_factura;
                        Sheet.Cells[string.Format("K{0}", row)].Value = line.numero_factura;
                        Sheet.Cells[string.Format("L{0}", row)].Value = line.valor;
                        Sheet.Cells[string.Format("M{0}", row)].Value = line.nit_prestador;
                        Sheet.Cells[string.Format("N{0}", row)].Value = line.prestador;
                        Sheet.Cells[string.Format("O{0}", row)].Value = line.factura_sin_letras;
                        row++;
                    }

                    Sheet.Cells["A:O"].AutoFitColumns();
                    Sheet.Cells["A:O"].AutoFitColumns();
                    Sheet.Cells["A1:O1"].Style.Font.Bold = true;
                    Color colFromHex = Color.FromArgb(54, 96, 146);
                    Sheet.Cells["A1:O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:O1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:O1"].Style.Font.Color.SetColor(Color.White);

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoMdFacturacion" + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                 "window.alert('ERROR EN GENERAR REPORTE.');" +
                   "</script> ";
                Response.Write(rta);
                Response.End();
            }

        }

        public JsonResult GetCascadeMes(Models.Odontologia.ortodoncia Model)
        {
            List<ref_meses_del_año> Lista = new List<ref_meses_del_año>();
            Lista = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            return Json(Lista.Select(p => new { id_mes = p.id_mes, nombre = p.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeRegional(Models.Odontologia.ortodoncia Model)
        {
            List<Ref_regional> Lista = new List<Ref_regional>();
            Lista = BusClass.GetRefRegion();
            return Json(Lista.Select(p => new { id_ref_regional = p.id_ref_regional, nombre = p.nombre_regional }), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _FNOAPROBADAS(string numFacturaM, DateTime? fechaFacturaM, int? valorM)
        {
            GestionMedicamentos Model = new GestionMedicamentos();

            List<Managment_ReportePrefacturas_totalResult> prefacturas = new List<Managment_ReportePrefacturas_totalResult>();

            prefacturas = Model.GetPrefacturasTotal();

            //var No_Cruzan = prefacturas.Where(l => l.coincide_prefactura_lupe == 0).ToList();
            //No_Cruzan = No_Cruzan.Where(l => l.aprobado == false).OrderByDescending(l => l.num_prefactura).ToList();

            //Session["no_aprobados"] = No_Cruzan;
            //ViewBag.noaprobados = No_Cruzan;

            //ViewBag.totalN = No_Cruzan.Count();

            return PartialView();
        }

        public void ExportarExcelTotal(int opcion)
        {
            GestionMedicamentos Model = new GestionMedicamentos();

            try
            {
                ExcelPackage Ep = new ExcelPackage();

                var eleccion = "";

                List<Managment_ReportePrefacturas_totalResult> Listado = new List<Managment_ReportePrefacturas_totalResult>();

                if (opcion == 1)
                {
                    Listado = (List<Managment_ReportePrefacturas_totalResult>)Session["aprobados"];
                    eleccion = "Aprobadas";
                }

                else if (opcion == 2)
                {
                    Listado = (List<Managment_ReportePrefacturas_totalResult>)Session["no_aprobados"];
                    eleccion = "NoAprobadas";
                }

                //Tablero cierre prefacturas abiertas
                else if (opcion == 3)
                {
                    Listado = BusClass.GetPrefacturasTotal().Where(x => x.abiertaPrefactura == 1 &&
                    (x.aprobado == true || (x.pasa == 1 && x.existeBeneficiario == 1)) && x.medicamentoRegulado != 2 && x.medicamentoRegulado != 3 && x.existeCerrada == 0).ToList();
                    eleccion = "Abiertas";
                }

                //Tablero cierre prefacturas cerradas
                else
                {
                    Listado = BusClass.GetPrefacturasTotal().Where(x => x.abiertaPrefactura == 2 && x.usuario_digita == SesionVar.UserName && x.existeCerrada == 1).ToList();
                    eleccion = "Cerradas";
                }

                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(eleccion);

                Sheet.Cells["A1:AO1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:AO1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AO1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AO1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AO1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Nro cargue";
                Sheet.Cells["B1"].Value = "Prestador";
                Sheet.Cells["C1"].Value = "Regional";
                Sheet.Cells["D1"].Value = "Remision pre factura fact";
                Sheet.Cells["E1"].Value = "Fecha radicación";
                Sheet.Cells["F1"].Value = "Nit";
                Sheet.Cells["G1"].Value = "Tipo doc beneficiario";
                Sheet.Cells["H1"].Value = "Num documento beneficiario";
                Sheet.Cells["I1"].Value = "Nombre beneficiario";
                Sheet.Cells["J1"].Value = "Ciudad despacho";
                Sheet.Cells["K1"].Value = "Id prescriptor";
                Sheet.Cells["L1"].Value = "Prescriptor";
                Sheet.Cells["M1"].Value = "Especialidad";
                Sheet.Cells["N1"].Value = "Cum";
                Sheet.Cells["O1"].Value = "Cod Ecopetrol hijo comercial";
                Sheet.Cells["P1"].Value = "Cod interno medicamento";
                Sheet.Cells["Q1"].Value = "Cod generico o interno medicamento";
                Sheet.Cells["R1"].Value = "Presentacion";
                Sheet.Cells["S1"].Value = "Descripcion producto";
                Sheet.Cells["T1"].Value = "Nombre comercial medicamento";
                Sheet.Cells["U1"].Value = "Laboratorio fabricante";
                Sheet.Cells["V1"].Value = "Fecha despacho formula";
                Sheet.Cells["W1"].Value = "Num unidades prescritas";
                Sheet.Cells["X1"].Value = "Num formula";
                Sheet.Cells["Y1"].Value = "Fecha formula";
                Sheet.Cells["Z1"].Value = "Num unidades entregadas";
                Sheet.Cells["AA1"].Value = "Vlr unitario und entregada";
                Sheet.Cells["AB1"].Value = "Valor Iva";
                Sheet.Cells["AC1"].Value = "Valor total";
                Sheet.Cells["AD1"].Value = "Código ATC";
                Sheet.Cells["AE1"].Value = "Grupo farmacologico";

                //Hasta aqui va la estructura como tal, los demas son campos agregados
                Sheet.Cells["AF1"].Value = "Diferencia";
                Sheet.Cells["AG1"].Value = "Observaciones";
                Sheet.Cells["AH1"].Value = "Nuevo valor unitario";
                Sheet.Cells["AI1"].Value = "Cruza valor con LTE";
                Sheet.Cells["AJ1"].Value = "Existe beneficiario";
                Sheet.Cells["AK1"].Value = "Cruce medicamento regulado";
                Sheet.Cells["AL1"].Value = "Factura duplicada historico";
                Sheet.Cells["AM1"].Value = "Factura duplicada cargue";
                Sheet.Cells["AN1"].Value = "Numero factura final";
                Sheet.Cells["AO1"].Value = "Valor final";

                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                int row = 2;

                foreach (var detalleprefactura in Listado)
                {
                    double ValorUno = Convert.ToDouble(detalleprefactura.vlr_unitario_und_entregada);
                    double ValorDos = Convert.ToDouble(detalleprefactura.nuevo_valor);
                    double resultado = ValorUno - ValorDos;

                    Sheet.Cells[string.Format("A{0}", row)].Value = detalleprefactura.id_detalle_prefactura;
                    Sheet.Cells[string.Format("B{0}", row)].Value = detalleprefactura.nombre_prestador;
                    Sheet.Cells[string.Format("C{0}", row)].Value = detalleprefactura.nombre_regional;
                    Sheet.Cells[string.Format("D{0}", row)].Value = detalleprefactura.remision_prefactura_fact;
                    Sheet.Cells[string.Format("E{0}", row)].Value = detalleprefactura.fecha_radicacion;
                    Sheet.Cells[string.Format("F{0}", row)].Value = detalleprefactura.nit;
                    Sheet.Cells[string.Format("G{0}", row)].Value = detalleprefactura.tipo_id_beneficiario;
                    Sheet.Cells[string.Format("H{0}", row)].Value = detalleprefactura.num_documento_beneficiario;
                    Sheet.Cells[string.Format("I{0}", row)].Value = detalleprefactura.nombre_beneficiario;
                    Sheet.Cells[string.Format("J{0}", row)].Value = detalleprefactura.ciudad_despacho;
                    Sheet.Cells[string.Format("K{0}", row)].Value = detalleprefactura.id_prescriptor;
                    Sheet.Cells[string.Format("L{0}", row)].Value = detalleprefactura.prescriptor;
                    Sheet.Cells[string.Format("M{0}", row)].Value = detalleprefactura.especialidad;
                    Sheet.Cells[string.Format("N{0}", row)].Value = detalleprefactura.cum;
                    Sheet.Cells[string.Format("O{0}", row)].Value = detalleprefactura.cod_ecopetrol_hijo_comercial;
                    Sheet.Cells[string.Format("P{0}", row)].Value = detalleprefactura.cod_interno_medicamento;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = detalleprefactura.cod_generico_o_interno_medicamento;
                    Sheet.Cells[string.Format("R{0}", row)].Value = detalleprefactura.Presentacion;
                    Sheet.Cells[string.Format("S{0}", row)].Value = detalleprefactura.descripcion_producto;
                    Sheet.Cells[string.Format("T{0}", row)].Value = detalleprefactura.nombre_comercial_medicacmento;
                    Sheet.Cells[string.Format("U{0}", row)].Value = detalleprefactura.laboratorio_fabricante;
                    Sheet.Cells[string.Format("V{0}", row)].Value = detalleprefactura.fecha_despacho_formula;
                    Sheet.Cells[string.Format("W{0}", row)].Value = detalleprefactura.num_unidades_prescritas;
                    Sheet.Cells[string.Format("X{0}", row)].Value = detalleprefactura.num_formula;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = detalleprefactura.fecha_formula;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = detalleprefactura.num_unidades_entregadas;

                    Sheet.Cells[string.Format("AA{0}", row)].Value = detalleprefactura.vlr_unitario_und_entregada;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = detalleprefactura.IVA;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = detalleprefactura.valor_total;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = detalleprefactura.CODIGO_ATC;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = detalleprefactura.grupo_farmacologico;

                    Sheet.Cells[string.Format("AF{0}", row)].Value = resultado;

                    if (String.IsNullOrEmpty(detalleprefactura.observaciones))
                    {
                        Sheet.Cells[string.Format("AG{0}", row)].Value = "TCP";
                    }
                    else
                    {
                        Sheet.Cells[string.Format("AG{0}", row)].Value = detalleprefactura.observaciones.ToUpper();
                    }


                    Sheet.Cells[string.Format("AH{0}", row)].Value = detalleprefactura.nuevo_valor;
                    Sheet.Cells[string.Format("AI{0}", row)].Value = detalleprefactura.mensajeValor;
                    Sheet.Cells[string.Format("AJ{0}", row)].Value = detalleprefactura.existeBeneficiarioMensaje;
                    Sheet.Cells[string.Format("AK{0}", row)].Value = detalleprefactura.MensajeMedicamentoRegulado;
                    Sheet.Cells[string.Format("AL{0}", row)].Value = detalleprefactura.mensajeHistoricoFactura;
                    Sheet.Cells[string.Format("AM{0}", row)].Value = detalleprefactura.mensajeDuplicidadCargue;
                    Sheet.Cells[string.Format("AN{0}", row)].Value = detalleprefactura.valor_final;
                    Sheet.Cells[string.Format("AO{0}", row)].Value = detalleprefactura.num_factura_final;

                    Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("V{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("Y{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }

                string namefile = "ReportePreFacturas_" + eleccion + "_" + DateTime.Now;

                Sheet.Cells["A" + row + ":AO1" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:AO"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('ERROR AL DESCARGAR REPORTE');" +
                    "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public ActionResult tableroTotalPrefacturas(string numFactura, int? prestador, int? regional)
        {
            GestionMedicamentos Model = new GestionMedicamentos();

            List<Managment_ReportePrefacturas_cerrar_abiertasResult> prefacturas = new List<Managment_ReportePrefacturas_cerrar_abiertasResult>();

            try
            {
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
                ViewBag.busca = 0;
                ViewBag.numFactura = "";

                prefacturas = Model.GetPrefacturasCerrarAbiertas().ToList();

                if (!string.IsNullOrEmpty(numFactura))
                {
                    prefacturas = prefacturas.Where(x => x.num_prefactura.Contains(numFactura)).ToList();
                }

                if (prestador != null)
                {
                    prefacturas = prefacturas.Where(x => x.id_prestador == prestador).ToList();
                }

                if (regional != null)
                {
                    prefacturas = prefacturas.Where(x => x.id_regional == regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = prefacturas;
            ViewBag.total = prefacturas.Count();

            return View();
        }

        //Nuevo tablero de cierre prefacturas

        public ActionResult TableroConsolidadoCierrePrefacturas()
        {
            List<management_consolidadoInicialPrefacturasResult> listado = new List<management_consolidadoInicialPrefacturasResult>();
            var conteo = 0;
            var idUsuario = SesionVar.IDUser;

            try
            {
                listado = BusClass.GetPrefacturasListadoConsolidadoInicial(idUsuario);
                conteo = listado.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.conteo = conteo;

            return View();
        }

        public ActionResult tableroCierrePrefacturas(string numFactura, int? prestador, int? regional, string numFactura2, int? prestador2, int? regional2, int? cargueBase)
        {
            GestionMedicamentos Model = new GestionMedicamentos();
            List<management_prefacturas_consolidado_abiertasResult> prefacturasAbiertas = new List<management_prefacturas_consolidado_abiertasResult>();
            List<management_prefacturas_consolidado_cerradasResult> prefacturasCerradas = new List<management_prefacturas_consolidado_cerradasResult>();
            var conteoAbiertas = 0;
            var conteoCerradas = 0;
            var idUsuario = SesionVar.IDUser;
            var idRol = SesionVar.ROL;

            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
            ViewBag.busca = 0;
            ViewBag.numFactura = "";

            try
            {
                //Abiertas
                prefacturasAbiertas = BusClass.GetPrefacturasAbiertas(cargueBase);

                if (idRol != "1" && idRol != "39")
                {
                    prefacturasAbiertas = prefacturasAbiertas.Where(x => x.id_usuario == idUsuario).ToList();
                }

                if (prestador != null && prestador != 1 && prestador != 0)
                {
                    prefacturasAbiertas = prefacturasAbiertas.Where(x => x.id_prestador == prestador).ToList();
                }

                if (regional != null && regional != 0)
                {
                    prefacturasAbiertas = prefacturasAbiertas.Where(x => x.id_regional == regional).ToList();
                }

                if (numFactura != null && numFactura != "")
                {
                    prefacturasAbiertas = prefacturasAbiertas.Where(x => x.remision_prefactura_fact.Contains(numFactura)).ToList();
                }

                conteoAbiertas = prefacturasAbiertas.Count();
                Session["listadoAbiertas"] = prefacturasAbiertas;


                //Cerradas
                prefacturasCerradas = BusClass.GetPrefacturasCerradas(cargueBase);

                if (idRol != "1" && idRol != "39")
                {
                    prefacturasCerradas = prefacturasCerradas.Where(x => x.id_usuario == idUsuario).ToList();
                }

                if (prestador2 != null && prestador2 != 1 && prestador2 != 0)
                {
                    prefacturasCerradas = prefacturasCerradas.Where(x => x.id_prestador == prestador2).ToList();
                }

                if (regional2 != null && regional2 != 0)
                {
                    prefacturasCerradas = prefacturasCerradas.Where(x => x.id_regional == regional2).ToList();
                }

                if (numFactura2 != null && numFactura2 != "")
                {
                    prefacturasCerradas = prefacturasCerradas.Where(x => x.facturaCierre.Contains(numFactura2)).ToList();
                }

                conteoCerradas = prefacturasCerradas.Count();
                Session["listadoCerradas"] = prefacturasCerradas;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaAbiertas = prefacturasAbiertas;
            ViewBag.conteoAbiertas = conteoAbiertas;

            ViewBag.listaCerradas = prefacturasCerradas;
            ViewBag.conteoCerradas = conteoCerradas;
            ViewBag.cargueBase = cargueBase;

            return View();
        }

        public JsonResult UpdatePrefactura(string numeroPrefactura, string numFactura, DateTime fechaFactura, decimal? valor, decimal? iva, int? idCargueBase)
        {
            GestionMedicamentos Model = new GestionMedicamentos();

            var mensaje = "";
            md_prefacturas_detalle obj = new md_prefacturas_detalle();
            List<md_prefacturas_detalle> validarDatos = new List<md_prefacturas_detalle>();

            obj.Id_prefactura_cargue_base = idCargueBase;
            obj.remision_prefactura_fact = numeroPrefactura;
            obj.abierta = 2;

            try
            {
                var idCargueDetalle = Model.ActualizarPrefacturaCerrar(obj);

                if (idCargueDetalle != 0)
                {
                    md_prefacturas_cargue_cerradas obj2 = new md_prefacturas_cargue_cerradas();

                    obj2.num_prefacturaCerrada = numeroPrefactura;
                    obj2.id_cargue_base = idCargueBase;
                    obj2.num_factura = numFactura;
                    obj2.fecha_factura = fechaFactura;
                    obj2.valor = valor;
                    obj2.iva = iva;
                    obj2.fecha_digita = DateTime.Now;
                    obj2.usuario_digita = SesionVar.UserName;

                    var idCerrada = Model.GuardarPrefacturaCerrada(obj2);
                    if (idCerrada != 0)
                    {
                        mensaje = "PRE-FACTURA CERRADA CORRECTAMENTE.";
                        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "PRE-FACTURA CERRADA CON ERRORES.";
                        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "ERROR AL CERRAR LA PRE-FACTURA";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                mensaje = "ERROR AL CERRAR LA PRE-FACTURA-" + e.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CierrePrefacturasMasivoExcel(int? cargueBase)
        {
            var mensaje = "";

            GestionMedicamentos Model = new GestionMedicamentos();
            var fileName = "";
            var pasa = false;
            var path = "";
            try
            {
                if (this.Request.Files["file"].ContentLength > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    try
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

                        List<management_prefacturas_consolidado_abiertasResult> prefacturasAbiertas = new List<management_prefacturas_consolidado_abiertasResult>();
                        prefacturasAbiertas = (List<management_prefacturas_consolidado_abiertasResult>)Session["listadoAbiertas"];

                        var finaliza = Model.ExcelCierreMasivoPrefacturas(dataTable, cargueBase, ref MsgRes, prefacturasAbiertas);

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto && finaliza != 0)
                        {
                            mensaje = "PREFACTURAS DILIGENCIADAS POR COMPLETO HAN SIDO CERRADAS CORRECTAMENTE";
                            pasa = true;
                        }
                        else
                        {
                            mensaje = MsgRes.DescriptionResponse;
                        }

                        if (!string.IsNullOrEmpty(path))
                        {
                            FileInfo fileDelete = new FileInfo(path);
                            if (fileDelete != null)
                            {
                                fileDelete.Delete();
                            }
                        }

                        return Json(new { success = pasa, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        if (!string.IsNullOrEmpty(path))
                        {
                            FileInfo fileDelete = new FileInfo(path);
                            if (fileDelete != null)
                            {
                                fileDelete.Delete();
                            }
                        }

                        var msg = MsgRes.DescriptionResponse;
                        if (msg == null || msg == "")
                        {
                            mensaje = "NO SE HAN PODIDO CERRAR LAS PREFACTURAS:" + e.Message;
                        }
                        else
                        {
                            mensaje = "NO SE HAN PODIDO CERRAR LAS PREFACTURAS:" + MsgRes.DescriptionResponse;
                        }
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        FileInfo fileDelete = new FileInfo(path);
                        if (fileDelete != null)
                        {
                            fileDelete.Delete();
                        }
                    }

                    mensaje = "FALTA UN ARCHIVO EN FORMATO EXCEL.";
                    return Json(new
                    {
                        success = false,
                        message = mensaje
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                var msg = MsgRes.DescriptionResponse;
                if (msg == null || msg == "")
                {
                    mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + e.Message;
                }
                else
                {
                    mensaje = "NO SE HAN PODIDO CARGAR LOS REGISTROS:" + MsgRes.DescriptionResponse;
                }

                FileInfo fileDelete = new FileInfo(path);
                if (fileDelete != null)
                {
                    fileDelete.Delete();
                }

                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ExportarExcelConsolidadoAbiertas()
        {
            ExcelPackage Ep = new ExcelPackage();
            List<management_prefacturas_consolidado_abiertasResult> Listado = new List<management_prefacturas_consolidado_abiertasResult>();

            try
            {
                Listado = (List<management_prefacturas_consolidado_abiertasResult>)Session["listadoAbiertas"];

                if (Listado.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                                 "window.alert('SIN DATOS POR MOSTRAR');" +
                                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Abiertas");

                Sheet.Cells["A1:K1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:K1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:K1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:K1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id cargue";
                Sheet.Cells["B1"].Value = "Remisión prefactura";
                Sheet.Cells["C1"].Value = "Conteo prefacturas";
                Sheet.Cells["D1"].Value = "Regional";
                Sheet.Cells["E1"].Value = "Prestador";
                Sheet.Cells["F1"].Value = "Total valor unitario";
                Sheet.Cells["G1"].Value = "Valor total prefacturas";
                Sheet.Cells["H1"].Value = "Nuevo valor total";

                Sheet.Cells["I1"].Value = "Número factura final";
                Sheet.Cells["J1"].Value = "Fecha factura";
                Sheet.Cells["K1"].Value = "Valor cierre";
                //Sheet.Cells["L1"].Value = "IVA cierre";

                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                int row = 2;

                foreach (var detalleprefactura in Listado)
                {

                    Sheet.Cells[string.Format("A{0}", row)].Value = detalleprefactura.id_cargue_base;
                    Sheet.Cells[string.Format("B{0}", row)].Value = detalleprefactura.remision_prefactura_fact;
                    Sheet.Cells[string.Format("C{0}", row)].Value = detalleprefactura.conteoPrefacturas;
                    Sheet.Cells[string.Format("D{0}", row)].Value = detalleprefactura.nombre_regional;
                    Sheet.Cells[string.Format("E{0}", row)].Value = detalleprefactura.nombrePrestador;
                    Sheet.Cells[string.Format("F{0}", row)].Value = detalleprefactura.vlrUnitario_suma;
                    Sheet.Cells[string.Format("G{0}", row)].Value = detalleprefactura.valorTotalPrefacturas;
                    Sheet.Cells[string.Format("H{0}", row)].Value = detalleprefactura.nuevoValor_suma;
                    Sheet.Cells[string.Format("I{0}", row)].Value = "";
                    Sheet.Cells[string.Format("J{0}", row)].Value = "";
                    Sheet.Cells[string.Format("K{0}", row)].Value = "";
                    //Sheet.Cells[string.Format("L{0}", row)].Value = "";

                    Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = "MM/dd/yyyy";

                    row++;
                }

                string namefile = "ReportePreFacturasAbiertas_" + DateTime.Now;

                Sheet.Cells["A" + row + ":K1" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:K"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('ERROR AL DESCARGAR REPORTE');" +
                    "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public void ExportarExcelConsolidadoCerradas()
        {
            ExcelPackage Ep = new ExcelPackage();
            List<management_prefacturas_consolidado_cerradasResult> Listado = new List<management_prefacturas_consolidado_cerradasResult>();

            try
            {
                Listado = (List<management_prefacturas_consolidado_cerradasResult>)Session["listadoCerradas"];

                if (Listado.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                                 "window.alert('SIN DATOS POR MOSTRAR');" +
                                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Cerradas");

                Sheet.Cells["A1:L1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:L1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:L1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:L1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id cargue";
                Sheet.Cells["B1"].Value = "Remisión prefactura";
                Sheet.Cells["C1"].Value = "Número factura cierre";
                Sheet.Cells["D1"].Value = "Conteo prefacturas";
                Sheet.Cells["E1"].Value = "Regional";
                Sheet.Cells["F1"].Value = "Prestador";
                Sheet.Cells["G1"].Value = "Total valor unitario";
                Sheet.Cells["H1"].Value = "Valor total prefacturas";
                Sheet.Cells["I1"].Value = "Nuevo valor total";
                //Sheet.Cells["J1"].Value = "Valor IVA";
                Sheet.Cells["J1"].Value = "Valor cierre";
                Sheet.Cells["K1"].Value = "Usuario cierra";
                Sheet.Cells["L1"].Value = "Fecha cierra";

                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                int row = 2;

                foreach (var detalleprefactura in Listado)
                {

                    Sheet.Cells[string.Format("A{0}", row)].Value = detalleprefactura.id_cargue_base;
                    Sheet.Cells[string.Format("B{0}", row)].Value = detalleprefactura.remision_prefactura_fact;
                    Sheet.Cells[string.Format("C{0}", row)].Value = detalleprefactura.facturaCierre;
                    Sheet.Cells[string.Format("D{0}", row)].Value = detalleprefactura.conteoPrefacturas;
                    Sheet.Cells[string.Format("E{0}", row)].Value = detalleprefactura.nombre_regional;
                    Sheet.Cells[string.Format("F{0}", row)].Value = detalleprefactura.nombre;
                    Sheet.Cells[string.Format("G{0}", row)].Value = detalleprefactura.vlrUnitario_suma;
                    Sheet.Cells[string.Format("H{0}", row)].Value = detalleprefactura.valorTotalPrefacturas;
                    Sheet.Cells[string.Format("I{0}", row)].Value = detalleprefactura.nuevoValor_suma;
                    //Sheet.Cells[string.Format("J{0}", row)].Value = detalleprefactura.ivaCierre;
                    Sheet.Cells[string.Format("J{0}", row)].Value = detalleprefactura.valorCierre;
                    Sheet.Cells[string.Format("K{0}", row)].Value = detalleprefactura.nombreCierre;
                    Sheet.Cells[string.Format("L{0}", row)].Value = detalleprefactura.fechaCierre;

                    Sheet.Cells[string.Format("L{0}", row)].Style.Numberformat.Format = "MM/dd/yyyy";

                    row++;
                }

                string namefile = "ReportePreFacturasCerradas_" + DateTime.Now;

                Sheet.Cells["A" + row + ":L1" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:L"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('ERROR AL DESCARGAR REPORTE');" +
                    "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        //public JsonResult Selection_ReadCruzan(int? carguefacturas)
        //{
        //    GestionMedicamentos Model = new GestionMedicamentos();
        //    List<management_listadoPrefacturasCruzanResult> listado = new List<management_listadoPrefacturasCruzanResult>();
        //    var lista = new object();

        //    try
        //    {
        //        listado = BusClass.listadoSiCruzanPrefacturasLupe((int)carguefacturas);
        //        //listado = listado.Where(x => x.desaprobada != 1 && (x.aprobado == true || (x.pasa == 1 && x.existeBeneficiario == 1 && x.valores_iguales == 1))).ToList();

        //        Session["aprobadosIni"] = listado;

        //        lista = (from item in listado
        //                 select new
        //                 {
        //                     id_cargue_base = item.id_cargue_base,
        //                     num_tirilla = item.num_tirilla,
        //                     num_prefactura = item.remision_prefactura_fact,
        //                     cum = item.cum,
        //                     cod = item.cod_interno_medicamento,
        //                     nit = item.nit,
        //                     id_detalle_prefactura = item.id_detalle_prefactura,
        //                     valor_total = item.valor_total,
        //                     aprobado = item.aprobado,
        //                     nuevo_valor = item.nuevo_valor,
        //                     fecha_despacho_formula = item.fecha_despacho_formula.Value.ToString("dd/MM/yyyy"),
        //                     pasa = item.pasa,
        //                     abiertaPrefactura = item.abierta,

        //                 }).Distinct().OrderByDescending(f => f.id_detalle_prefactura);
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult Selection_ReadNoCruzan(int? carguefacturas)
        //{
        //    GestionMedicamentos Model = new GestionMedicamentos();
        //    List<management_listadoPrefacturasNoCruzanResult> listado = new List<management_listadoPrefacturasNoCruzanResult>();
        //    var lista2 = new object();

        //    try
        //    {
        //        //listado = BusClass.listadoNoCruzanPrefacturasLupe((int)carguefacturas).Where(x => x.desaprobada == 1 || (x.aprobado == false && (x.pasa == 2 || x.existeBeneficiario == 2 || x.valores_iguales != 1))).ToList();

        //        listado = BusClass.listadoNoCruzanPrefacturasLupe((int)carguefacturas);
        //        //listado = listado.Where(x => x.desaprobada == 1 || (x.aprobado == false && (x.pasa == 2 || x.existeBeneficiario == 2 || x.valores_iguales != 1))).ToList();

        //        Session["no_aprobadosIni"] = listado;

        //        lista2 = (from item in listado
        //                  select new
        //                  {
        //                      //id_cargue_base = item.id_cargue_base,
        //                      //num_tirilla = item.num_tirilla,
        //                      //num_prefactura = item.remision_prefactura_fact,
        //                      //cum = item.cum,
        //                      //cod = item.cod_interno_medicamento,
        //                      //nit = item.nit,
        //                      //id_detalle_prefactura = item.id_detalle_prefactura,
        //                      //valor_total = item.valor_total,
        //                      //aprobado = item.aprobado,
        //                      //nuevo_valor = item.nuevo_valor,
        //                      //fecha_despacho_formula = item.fecha_despacho_formula,
        //                      //pasa = item.pasa,
        //                      //abiertaPrefactura = item.abierta,

        //                      id_cargue_base = item.id_cargue_base,
        //                      num_tirilla = item.num_tirilla,
        //                      num_prefactura = item.remision_prefactura_fact,
        //                      cum = item.cum,
        //                      cod = item.cod_interno_medicamento,
        //                      nit = item.nit,
        //                      id_detalle_prefactura = item.id_detalle_prefactura,
        //                      valor_total = item.valor_total,
        //                      aprobado = item.aprobado,
        //                      nuevo_valor = item.nuevo_valor,
        //                      fecha_despacho_formula = item.fecha_despacho_formula.Value.ToString("dd/MM/yyyy"),
        //                      pasa = item.pasa,
        //                      abiertaPrefactura = item.abierta,

        //                  }).Distinct().OrderByDescending(f => f.id_detalle_prefactura);

        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return Json(lista2, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult TableroConsolidadoLupe(int? prestador, int? referenciaPago, int? regional, int? rta)
        {
            var respuesta = 0;
            var mensaje = "";

            try
            {

                ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.referenciaPa = BusClass.GetReferenciaPagoList();

                if (rta != null)
                {
                    if (rta == 1)
                    {
                        mensaje = "Cargue lupe eliminado correctamente.";
                    }
                    else if (rta == 2)
                    {
                        mensaje = "Problemas al eliminar cargue Lupe.";
                    }

                    respuesta = (int)rta;
                }

                ViewBag.rta = respuesta;
                ViewBag.msg = mensaje;

                List<management_lupe_carguesResult> listado = new List<management_lupe_carguesResult>();
                listado = BusClass.listadoCargueLupe().OrderByDescending(x => x.id_lupe_cargue_base).ToList();

                if (prestador != null)
                {
                    listado = listado.Where(x => x.id_prestador == prestador).ToList();
                }
                if (referenciaPago != null)
                {
                    listado = listado.Where(x => x.referente_pago == referenciaPago).ToList();
                }
                if (referenciaPago != null)
                {
                    listado = listado.Where(x => x.referente_pago == referenciaPago).ToList();
                }

                var rol = SesionVar.ROL;
                ViewBag.rol = rol;

                var conteo = listado.Count();
                ViewBag.conteo = conteo;
                ViewBag.lista = listado;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        public PartialViewResult _IntermediacionesLupe(int? idLupe)
        {

            List<management_lupe_cargues_intermediacionesResult> list = new List<management_lupe_cargues_intermediacionesResult>();
            try
            {
                list = BusClass.listadoCargueLupeIntermediaciones((int)idLupe);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var conteoI = list.Count();
            ViewBag.total = conteoI;
            ViewBag.list = list;

            ViewBag.id = idLupe;

            return PartialView();
        }

        public JsonResult EliminarLupe(int idLupe)
        {

            var mensaje = "";
            var resultado = 0;
            var usuarioElimina = SesionVar.UserName;

            try
            {
                resultado = BusClass.EliminarLupe(idLupe, usuarioElimina);

                if (resultado != 0)
                {
                    mensaje = "CARGUE LUPE ELIMINADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR CARGUE LUPE";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR CARGUE LUPE: " + error;
            }

            return Json(new { success = true, msg = mensaje, rta = resultado }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TableroControlMedicamentosFacturas()
        {
            return View();
        }

        public void ReporteMedicamentosFacturacion(string documento, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                List<management_facturacion_tableroControlResult> listado = new List<management_facturacion_tableroControlResult>();
                string[] documentoFiltro = documento.Split(';');

                if (!string.IsNullOrEmpty(documento))
                {
                    List<management_facturacion_tableroControlResult> listado2 = new List<management_facturacion_tableroControlResult>();
                    for (var i = 0; i < documentoFiltro.Count(); i++)
                    {
                        var variableTomada = documentoFiltro[i];
                        variableTomada = variableTomada.Replace(" ", "");
                        variableTomada = variableTomada.Replace(",", "");

                        List<management_facturacion_tableroControlResult> listado3 = new List<management_facturacion_tableroControlResult>();
                        listado3 = BusClass.ListadoMedicamentosFacturas((DateTime)fechaInicio, (DateTime)fechaFin, variableTomada);
                        listado2.AddRange(listado3);
                    }
                    listado = listado2;
                }

                if (listado.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('NO SE HAN ENCONTRADO RESULTADOS.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                else
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Medicamentos Facturacion");

                    Sheet.Cells["A1:Q1"].Style.Font.Bold = true;
                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:Q1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:Q1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:Q1"].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A1"].Value = "REGIONAL";
                    Sheet.Cells["B1"].Value = "UNIS";
                    Sheet.Cells["C1"].Value = "LOCALIDAD";
                    Sheet.Cells["D1"].Value = "TIPO DE IDENTIFICACIÓN";
                    Sheet.Cells["E1"].Value = "IDENTIFICACIÓN DEL PACIENTE";
                    Sheet.Cells["F1"].Value = "Cum";
                    Sheet.Cells["G1"].Value = "CONCEPTO";
                    Sheet.Cells["H1"].Value = "CANTIDAD";
                    Sheet.Cells["I1"].Value = "FECHA DISPENSACIÓN";
                    Sheet.Cells["J1"].Value = "FECHA FACTURA";
                    Sheet.Cells["K1"].Value = "NUMERO DE FACTURA";
                    Sheet.Cells["L1"].Value = "VALOR";
                    Sheet.Cells["M1"].Value = "NIT DEL PRESTADOR";
                    Sheet.Cells["N1"].Value = "PRESTADOR";
                    Sheet.Cells["O1"].Value = "FACTURA SIN LETRAS";
                    Sheet.Cells["P1"].Value = "FORMULA";
                    Sheet.Cells["Q1"].Value = "DOCUMENTO CONTABLE";

                    int row = 2;
                    foreach (management_facturacion_tableroControlResult item in listado)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.regional_dtll;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.unis;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.localidad;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.tipo_identificacion;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.identificacion_paciente;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.cum;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.concepto;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.cantidad;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_dispensacion;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_factura;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.numero_factura;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.valor;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.nit_prestador;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.prestador;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.factura_sin_letras;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.formula;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.documento_contable;

                        Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    Sheet.Cells["A" + row + ":Q1" + row].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A:Q"].AutoFitColumns();

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=RptMedicamentosFacturacion.xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('PROBLEMAS AL GENERAR EL REPORTE - CONTACTE CON EL ÁREA SISTEMAS.');" +
                  "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public ActionResult TableroConsolidadoMedicamentosFacturas(int? mesIni, int? añoIni, int? mesFin, int? añoFin)
        {
            List<management_facturacion_consolidado_listaResult> lista = new List<management_facturacion_consolidado_listaResult>();
            try
            {

                if (mesIni != null && añoIni != null && mesFin != null && añoFin != null)
                {
                    var fechaIni = new DateTime((int)añoIni, (int)mesIni, 01);
                    var fechaFin = new DateTime((int)añoFin, (int)mesFin, 01);

                    fechaIni = Convert.ToDateTime(fechaIni);
                    fechaFin = Convert.ToDateTime(fechaFin);

                    lista = BusClass.ListadoMedicamentosFacturasConsolidadoLista(fechaIni, fechaFin);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var conteo = lista.Count();
            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.mes = BusClass.meses();

            ViewBag.mesInicial = mesIni;
            ViewBag.añoInicial = añoIni;
            ViewBag.mesFinal = mesFin;
            ViewBag.añoFinal = añoFin;

            return View();
        }

        public JsonResult buscarPorNit()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 0)
                {
                    List<md_Ref_proveedor> prestadores = BusClass.GetMD_Ref_proveedor();
                    prestadores = prestadores.Where(x => x.nombre.ToUpper().Contains(term.ToUpper()) || Convert.ToString(x.nit).ToUpper().Contains(term.ToUpper())).ToList();

                    var lista = (from reg in prestadores
                                 select new
                                 {
                                     nit = reg.nit,
                                     label = reg.nit + "-" + reg.nombre,
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
                var error = e.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public void DescargarConsolidadoFacturas(int? mesIni, int? añoIni, int? mesFin, int? añoFin)
        {
            List<management_facturacion_consolidado_listaResult> lista = new List<management_facturacion_consolidado_listaResult>();
            try
            {
                try
                {
                    var fechaIni = new DateTime((int)añoIni, (int)mesIni, 01);
                    var fechaFin = new DateTime((int)añoFin, (int)mesFin, 01);

                    fechaIni = Convert.ToDateTime(fechaIni);
                    fechaFin = Convert.ToDateTime(fechaFin);

                    lista = BusClass.ListadoMedicamentosFacturasConsolidadoLista(fechaIni, fechaFin);
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                if (lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('NO SE HAN ENCONTRADO RESULTADOS.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                else
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ConsolidadoMedicamentosFacturación");

                    Sheet.Cells["A1:E1"].Style.Font.Bold = true;
                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:E1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:E1"].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A1"].Value = "AÑO";
                    Sheet.Cells["B1"].Value = "MES";
                    Sheet.Cells["C1"].Value = "NUMERO FACTURA";
                    Sheet.Cells["D1"].Value = "FECHA FACTURA";
                    Sheet.Cells["E1"].Value = "VALOR FACTURA";

                    int row = 2;
                    foreach (management_facturacion_consolidado_listaResult item in lista)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.año;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.descripcion;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.numero_factura;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_factura;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.valorFactura;

                        Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    Sheet.Cells["A" + row + ":E1" + row].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A:E"].AutoFitColumns();

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=RptConsolidadoMedicamentosFacturacion.xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('PROBLEMAS AL GENERAR EL REPORTE - CONTACTE CON EL ÁREA SISTEMAS.');" +
                  "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public JsonResult EnviarCaso(int? idCargue, int? tipo)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            byte[] array = new byte[0];
            byte[] arrayDatos = new byte[0];
            StringBuilder sb = new StringBuilder();
            string filename = "";
            string filenameExcel = "";
            List<management_Validador_datosCorreosResult> correosFinal = new List<management_Validador_datosCorreosResult>();
            List<sis_auditor_regional> listaRegionales = new List<sis_auditor_regional>();
            var idUsuario = 0;

            try
            {
                idUsuario = SesionVar.IDUser;
                listaRegionales = BusClass.listadoRegionalesUsuario(idUsuario);

                if (listaRegionales == null)
                {
                    mensaje = "EL USUARIO NO TIENE REGIONALES ASIGNADAS.";
                    return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }

                if (listaRegionales.Count() < 1)
                {
                    mensaje = "EL USUARIO NO TIENE REGIONALES ASIGNADAS.";
                    return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }

                if (tipo == 1)
                {
                    array = DevolverNotificacionOPLNoAprobadas((int)idCargue);
                    arrayDatos = traerExcelACorreoOPLNoPasan();
                }
                else
                {
                    array = DevolverNotificacionOPLAprobadas((int)idCargue);
                    arrayDatos = traerExcelACorreoOPLPasan();
                }

                foreach (var item in listaRegionales)
                {
                    List<management_Validador_datosCorreosResult> correos = new List<management_Validador_datosCorreosResult>();
                    correos = BusClass.ListadoCorreosValidadorOPL(item.id_regional);
                    correosFinal.AddRange(correos);
                }

                if (correosFinal.Count() < 1)
                {
                    mensaje = "NO HAY CORREOS OPL REGISTRADOS.";
                    return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }

                if (tipo == 1)
                {
                    sb.Append("Se envian las prefacturas no aprobadas correspondientes al cargue #" + idCargue);
                    filename = "FacturasNoAprobadas_" + idCargue + ".pdf";
                    filenameExcel = "FacturasNoAprobadas_" + idCargue + ".xlsx";
                }
                else
                {
                    sb.Append("Se envian las prefacturas aprobadas correspondientes al cargue #" + idCargue);
                    filename = "FacturasAprobadas_" + idCargue + ".pdf";
                    filenameExcel = "FacturasAprobadas_" + idCargue + ".xlsx";
                }

                sb.Append("<br/>");

                string textBody = sb.ToString();

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
                mailBody += "<STRONG>Asalud SAS </STRONG>";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
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

                mailMessage.From = new MailAddress(smtpSection.From);
                mailMessage.To.Clear();

                foreach (var item2 in correosFinal)
                {
                    var correo = "";
                    if (item2.correo_OPL != "")
                    {
                        correo = item2.correo_OPL;

                        if (correo != null && correo != "")
                        {
                            //mailMessage.To.Add("desarrollo.soporte@asaludltda.com");
                            mailMessage.To.Add(correo);
                        }
                    }
                }

                if (tipo == 1)
                {
                    mailMessage.Subject = "[Mensaje Automático]" + "Notificación prefacturas no aprobadas.";
                }
                else
                {
                    mailMessage.Subject = "[Mensaje Automático]" + "Notificación prefacturas aprobadas.";
                }

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                MemoryStream memoryStream = new MemoryStream(array);
                MemoryStream memoryStream2 = new MemoryStream(arrayDatos);
                mailMessage.Attachments.Add(new Attachment(memoryStream, filename));
                mailMessage.Attachments.Add(new Attachment(memoryStream2, filenameExcel));

                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                mensaje = "INFORME ENVIADO CORRECTAMENTE A LOS OPL.";

                return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                mensaje = "Lo sentimos, estamos enfrentando problemas aquí: " + ex.Message;
                return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public string EnviarCasoCargueCompleto(int? idCargue, int? idUsuario, int? totalLineas, int? estado)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            StringBuilder sb = new StringBuilder();
            sis_usuario usuario = new sis_usuario();
            sis_usuario usuario2 = new sis_usuario();
            md_prefacturas_cargue_base DatosCargue = new md_prefacturas_cargue_base();
            log_prefacturas_estadoValidacion log = new log_prefacturas_estadoValidacion();
            int fase = 0;
            var mensajeLog = "";
            try
            {

                log = BusClass.GetLogEstadoValidacionPrefacturas(idCargue);
                if (log != null)
                {
                    fase = (int)log.fase;
                    mensajeLog = log.mensajeEstado;
                }

                usuario = BusClass.datosUsuarioId((int)idUsuario);
                usuario2 = BusClass.datosUsuarioId(SesionVar.IDUser);
                DatosCargue = BusClass.GetPrefacturas((int)idCargue);

                if (estado == 1)
                {
                    sb.Append("El cargue #" + idCargue + " Se ha cargado correctamente con " + totalLineas + " registros y se encuentra listo para validar.");
                    sb.Append("<br/>");
                    sb.Append("Cargue hecho por parte de: " + usuario.nombre);
                    sb.Append("<br/>");
                }
                else if (estado == 2 || estado == 3)
                {
                    if (estado == 2)
                    {
                        sb.Append("El cargue #" + idCargue + " ha sido validado en fase: " + fase);
                        sb.Append("<br/>");
                    }

                    else if (estado == 3)
                    {
                        sb.Append("El cargue #" + idCargue + " no ha podido ser validado en fase: " + fase + ". Intentelo nuevamente.");
                        sb.Append("<br/>");
                    }

                    sb.Append("Por parte de: " + usuario2.nombre);
                    sb.Append("<br/>");

                    if (fase == 1)
                    {
                        sb.Append("Fecha validación inicio fase 1: " + DatosCargue.fecha_inicio_validacion1);
                        sb.Append("<br/>");
                        sb.Append("Fecha validación fin fase 1: " + DatosCargue.fecha_fin_validacion1);
                    }
                    else if (fase == 2)
                    {
                        sb.Append("Fecha validación inicio fase 2: " + DatosCargue.fecha_inicio_validacion2);
                        sb.Append("<br/>");
                        sb.Append("Fecha validación fin fase 2: " + DatosCargue.fecha_fin_validacion2);
                    }
                    else if (fase == 3)
                    {
                        sb.Append("Fecha validación inicio fase 3: " + DatosCargue.fecha_inicio_validacion3);
                        sb.Append("<br/>");
                        sb.Append("Fecha validación fin fase 3: " + DatosCargue.fecha_fin_validacion3);
                    }

                    sb.Append("<br/>");
                }
                else if (estado == 4)
                {
                    sb.Append("El cargue #" + idCargue + " ha sido devuelto para volver a validar la fase: " + fase);
                    sb.Append("<br/>");
                }

                string textBody = sb.ToString();

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
                mailBody += "<STRONG>Asalud SAS </STRONG>";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
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

                mailMessage.From = new MailAddress(smtpSection.From);
                mailMessage.To.Clear();

                var correo = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    if (usuario != null)
                    {
                        correo = usuario.correo_ins;
                        if (string.IsNullOrEmpty(correo))
                        {
                            correo = usuario.correo;
                        }

                        if (!string.IsNullOrEmpty(correo))
                        {
                            mailMessage.To.Add(correo);
                        }
                    }

                    mailMessage.To.Add("reporteinformes2@asalud.co");
                }
                else
                {
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                }

                if (estado == 1)
                {
                    mailMessage.Subject = "[Mensaje Automático]" + "Cargue cargado correctamente.";
                }
                else if (estado == 2)
                {
                    mailMessage.Subject = "[Mensaje Automático]" + "Cargue validado correctamente.";
                }
                else
                {
                    mailMessage.Subject = "[Mensaje Automático]" + "Error en la validación.";

                }
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                mensaje = "ENVIADO CORRECTAMENTE.";
            }

            catch (Exception ex)
            {
                mensaje = "LO SENTIMOS, ESTAMOS ENFRENTANDO PROBLEMAS AQUÍ: " + ex.Message;
            }

            return mensaje;
        }

        public JsonResult solicitudEliminacionDatosPrefactura(int? idCargue)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            StringBuilder sb = new StringBuilder();
            var idUsuario = 0;

            sis_usuario datosUsuario = new sis_usuario();
            var nombre = "";
            var usuario = "";

            try
            {
                idUsuario = SesionVar.IDUser;
                datosUsuario = BusClass.datosUsuarioId(idUsuario);

                if (datosUsuario != null)
                {
                    nombre = datosUsuario.nombre;
                    usuario = datosUsuario.usuario;
                }

                if (datosUsuario == null)
                {
                    mensaje = "NO TIENE DATOS EN EL SISTEMA.";
                    return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }

                sb.Append("Se solicita la eliminación de registros de prefacturas para el cargue #" + idCargue);
                sb.Append("<br/>");

                string textBody = sb.ToString();

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
                mailBody += "Cordialmente:";
                mailBody += nombre;
                mailBody += "<br />";
                mailBody += "Usuario:";
                mailBody += usuario;
                mailBody += "<br />";
                mailBody += "</div>";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<STRONG>Asalud SAS </STRONG>";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
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

                mailMessage.From = new MailAddress(smtpSection.From);
                mailMessage.To.Clear();


                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    mailMessage.To.Add("sami.soporte@asalud.co");
                }
                else
                {
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                }

                mailMessage.Subject = "[Mensaje Automático]" + "Pedido eliminación de registros - Cargue#" + idCargue;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                mensaje = "SOLICITUD ENVIADA CORRECTAMENTE A SAMI.";

                return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                mensaje = "Lo siento, estamos enfrentando problemas aquí: " + ex.Message;
                return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public byte[] DevolverNotificacionOPLNoAprobadas(int idCargue)
        {
            byte[] documento = null;

            List<management_prefacturas_notificacionOPLNoPasanResult> listaDatos = new List<management_prefacturas_notificacionOPLNoPasanResult>();

            try
            {
                listaDatos = BusClass.ListaDatoaReportePrefacturasaOPLNoPasan(idCargue);
                ecop_firma_digital_sami firma = new ecop_firma_digital_sami();

                var Imagen = "";
                var idUsuarioCalidad = SesionVar.IDUser;

                firma = BusClass.GetFirmas(idUsuarioCalidad);

                try
                {
                    if (firma != null)
                    {
                        if (firma.firma_digital != null)
                        {
                            Imagen = Convert.ToBase64String(firma.firma_digital.ToArray());
                        }
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptNotificacionPrefacturasNAOPL.rdlc");

                Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
                Microsoft.Reporting.WebForms.ReportDataSource Lista = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPrefacturasNoAprobadas", listaDatos);

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.EnableHyperlinks = true;

                viewer.ProcessingMode = ProcessingMode.Local;

                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(Lista);
                try
                {
                    viewer.LocalReport.SetParameters(imagen);
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
                if (listaDatos.Count() != 0)
                {
                    try
                    {
                        viewer.LocalReport.EnableHyperlinks = true;

                        viewer.LocalReport.Refresh();

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        return documento = pdfContent;

                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                        return documento;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return documento;
        }

        public void MostrarNotificacionOPLNoAprobadas(int idCargue)
        {
            List<management_prefacturas_notificacionOPLNoPasanResult> listaDatos = new List<management_prefacturas_notificacionOPLNoPasanResult>();

            try
            {
                listaDatos = BusClass.ListaDatoaReportePrefacturasaOPLNoPasan(idCargue);

                if (listaDatos.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                     "window.alert('NO SE PUDO GENERAR EL PDF.');" +
                     "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ecop_firma_digital_sami firma = new ecop_firma_digital_sami();

                var Imagen = "";
                var idUsuarioCalidad = SesionVar.IDUser;

                firma = BusClass.GetFirmas(idUsuarioCalidad);

                try
                {
                    if (firma != null)
                    {
                        if (firma.firma_digital != null)
                        {
                            Imagen = Convert.ToBase64String(firma.firma_digital.ToArray());
                        }
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptNotificacionPrefacturasNAOPL.rdlc");

                Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
                Microsoft.Reporting.WebForms.ReportDataSource Lista = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPrefacturasNoAprobadas", listaDatos);

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.EnableHyperlinks = true;

                viewer.ProcessingMode = ProcessingMode.Local;

                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(Lista);
                try
                {
                    viewer.LocalReport.SetParameters(imagen);
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
                if (listaDatos.Count() != 0)
                {
                    try
                    {
                        viewer.LocalReport.EnableHyperlinks = true;

                        viewer.LocalReport.Refresh();

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
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

                        string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO SE PUDO GENERAR EL PDF.');" +
                       "</script> ";

                        Response.Write(rta);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO SE PUDO GENERAR EL PDF.');" +
                       "</script> ";

                Response.Write(rta);
                Response.End();
            }
        }

        public byte[] DevolverNotificacionOPLAprobadas(int idCargue)
        {
            byte[] documento = null;

            List<management_prefacturas_notificacionOPLPasanResult> listaDatos = new List<management_prefacturas_notificacionOPLPasanResult>();

            try
            {
                listaDatos = BusClass.ListaDatoaReportePrefacturasaOPLPasan(idCargue);
                ecop_firma_digital_sami firma = new ecop_firma_digital_sami();

                var Imagen = "";
                var idUsuarioCalidad = SesionVar.IDUser;

                firma = BusClass.GetFirmas(idUsuarioCalidad);

                try
                {
                    if (firma != null)
                    {
                        if (firma.firma_digital != null)
                        {
                            Imagen = Convert.ToBase64String(firma.firma_digital.ToArray());
                        }
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptNotificacionPrefacturasAOPL.rdlc");

                Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
                Microsoft.Reporting.WebForms.ReportDataSource Lista = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPrefacturasAprobadas", listaDatos);

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.EnableHyperlinks = true;

                viewer.ProcessingMode = ProcessingMode.Local;

                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(Lista);
                try
                {
                    viewer.LocalReport.SetParameters(imagen);
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
                if (listaDatos.Count() != 0)
                {
                    try
                    {
                        viewer.LocalReport.EnableHyperlinks = true;

                        viewer.LocalReport.Refresh();

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        return documento = pdfContent;

                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                        return documento;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return documento;
        }

        public void MostrarNotificacionOPLAprobadas(int idCargue)
        {
            List<management_prefacturas_notificacionOPLPasanResult> listaDatos = new List<management_prefacturas_notificacionOPLPasanResult>();

            try
            {
                listaDatos = BusClass.ListaDatoaReportePrefacturasaOPLPasan(idCargue);

                if (listaDatos.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                     "window.alert('NO SE PUDO GENERAR EL PDF.');" +
                     "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ecop_firma_digital_sami firma = new ecop_firma_digital_sami();

                var Imagen = "";
                var idUsuarioCalidad = SesionVar.IDUser;

                firma = BusClass.GetFirmas(idUsuarioCalidad);

                try
                {
                    if (firma != null)
                    {
                        if (firma.firma_digital != null)
                        {

                            Imagen = Convert.ToBase64String(firma.firma_digital.ToArray());
                        }
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptNotificacionPrefacturasAOPL.rdlc");

                Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
                Microsoft.Reporting.WebForms.ReportDataSource Lista = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPrefacturasAprobadas", listaDatos);

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.EnableHyperlinks = true;

                viewer.ProcessingMode = ProcessingMode.Local;

                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(Lista);
                try
                {
                    viewer.LocalReport.SetParameters(imagen);
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
                if (listaDatos.Count() != 0)
                {

                    try
                    {
                        viewer.LocalReport.EnableHyperlinks = true;

                        viewer.LocalReport.Refresh();

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
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

                        string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO SE PUDO GENERAR EL PDF.');" +
                       "</script> ";

                        Response.Write(rta);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO SE PUDO GENERAR EL PDF.');" +
                       "</script> ";

                Response.Write(rta);
                Response.End();
            }
        }

        public byte[] traerExcelACorreoOPLPasan()
        {
            byte[] documento = null;

            GestionMedicamentos Model = new GestionMedicamentos();
            List<management_listadoPrefacturasCruzanResult> Listado = new List<management_listadoPrefacturasCruzanResult>();
            try
            {
                Listado = (List<management_listadoPrefacturasCruzanResult>)Session["aprobadosIni"];
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Cargue");

                Sheet.Cells["A1:AH1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:AH1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AH1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AH1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AH1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Nro cargue";
                Sheet.Cells["B1"].Value = "Remision pre factura fact";
                Sheet.Cells["C1"].Value = "Num Tirilla o consecutivo";
                Sheet.Cells["D1"].Value = "Fecha radicacion";
                Sheet.Cells["E1"].Value = "Nit";
                Sheet.Cells["F1"].Value = "Tipo doc beneficiario";
                Sheet.Cells["G1"].Value = "Num documento beneficiario";
                Sheet.Cells["H1"].Value = "Nombre beneficiario";
                Sheet.Cells["I1"].Value = "Ciudad despacho";
                Sheet.Cells["J1"].Value = "Id prescriptor";
                Sheet.Cells["K1"].Value = "Prescriptor";
                Sheet.Cells["L1"].Value = "Especialidad";
                Sheet.Cells["M1"].Value = "Cum";
                Sheet.Cells["N1"].Value = "Cod Ecopetrol hijo comercial";
                Sheet.Cells["O1"].Value = "Cod interno medicamento";
                Sheet.Cells["P1"].Value = "Cod generico o interno medicamento";
                Sheet.Cells["Q1"].Value = "Presentacion";
                Sheet.Cells["R1"].Value = "Descripcion producto";
                Sheet.Cells["S1"].Value = "Nombre comercial medicamento";
                Sheet.Cells["T1"].Value = "Laboratorio fabricante";
                Sheet.Cells["U1"].Value = "Fecha despacho formula";
                Sheet.Cells["V1"].Value = "Num unidades prescritas";
                Sheet.Cells["W1"].Value = "Num formula";
                Sheet.Cells["X1"].Value = "Fecha formula";
                Sheet.Cells["Y1"].Value = "Num unidades entregadas";
                Sheet.Cells["Z1"].Value = "Vlr unitario und entregada";
                Sheet.Cells["AA1"].Value = "Valor Iva";
                Sheet.Cells["AB1"].Value = "Valor total";
                Sheet.Cells["AC1"].Value = "Codigo ATC";
                Sheet.Cells["AD1"].Value = "Grupo farmacologico";

                //Hasta aqui va la estructura como tal, los demas son campos agregados
                Sheet.Cells["AE1"].Value = "Diferencia";
                Sheet.Cells["AF1"].Value = "Observaciones";
                Sheet.Cells["AG1"].Value = "Nuevo valor unitario";
                Sheet.Cells["AH1"].Value = "Nuevo valor total";

                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                int row = 2;
                foreach (var detalleprefactura in Listado)
                {
                    double ValorUno = Convert.ToDouble(detalleprefactura.vlr_unitario_und_entregada);
                    double ValorDos = Convert.ToDouble(detalleprefactura.nuevo_valor);
                    double resultado = ValorUno - ValorDos;

                    Sheet.Cells[string.Format("A{0}", row)].Value = detalleprefactura.Id_prefactura_cargue_base;
                    Sheet.Cells[string.Format("B{0}", row)].Value = detalleprefactura.remision_prefactura_fact;
                    Sheet.Cells[string.Format("C{0}", row)].Value = detalleprefactura.num_tirilla;
                    Sheet.Cells[string.Format("D{0}", row)].Value = detalleprefactura.fecha_radicacion;
                    Sheet.Cells[string.Format("E{0}", row)].Value = detalleprefactura.nit;
                    Sheet.Cells[string.Format("F{0}", row)].Value = detalleprefactura.tipo_id_beneficiario;
                    Sheet.Cells[string.Format("G{0}", row)].Value = detalleprefactura.num_documento_beneficiario;
                    Sheet.Cells[string.Format("H{0}", row)].Value = detalleprefactura.nombre_beneficiario;
                    Sheet.Cells[string.Format("I{0}", row)].Value = detalleprefactura.ciudad_despacho;
                    Sheet.Cells[string.Format("J{0}", row)].Value = detalleprefactura.id_prescriptor;
                    Sheet.Cells[string.Format("K{0}", row)].Value = detalleprefactura.prescriptor;
                    Sheet.Cells[string.Format("L{0}", row)].Value = detalleprefactura.especialidad;
                    Sheet.Cells[string.Format("M{0}", row)].Value = detalleprefactura.cum;
                    Sheet.Cells[string.Format("N{0}", row)].Value = detalleprefactura.cod_ecopetrol_hijo_comercial;
                    Sheet.Cells[string.Format("O{0}", row)].Value = detalleprefactura.cod_interno_medicamento;
                    Sheet.Cells[string.Format("P{0}", row)].Value = detalleprefactura.cod_generico_o_interno_medicamento;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = detalleprefactura.Presentacion;
                    Sheet.Cells[string.Format("R{0}", row)].Value = detalleprefactura.descripcion_producto;
                    Sheet.Cells[string.Format("S{0}", row)].Value = detalleprefactura.nombre_comercial_medicacmento;
                    Sheet.Cells[string.Format("T{0}", row)].Value = detalleprefactura.laboratorio_fabricante;
                    Sheet.Cells[string.Format("U{0}", row)].Value = detalleprefactura.fecha_despacho_formula;
                    Sheet.Cells[string.Format("V{0}", row)].Value = detalleprefactura.num_unidades_prescritas;
                    Sheet.Cells[string.Format("W{0}", row)].Value = detalleprefactura.num_formula;
                    Sheet.Cells[string.Format("X{0}", row)].Value = detalleprefactura.fecha_formula;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = detalleprefactura.num_unidades_entregadas;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = detalleprefactura.vlr_unitario_und_entregada;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = detalleprefactura.IVA;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = detalleprefactura.valor_total;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = detalleprefactura.CODIGO_ATC;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = detalleprefactura.grupo_farmacologico;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = resultado;

                    if (String.IsNullOrEmpty(detalleprefactura.observaciones))
                    {
                        Sheet.Cells[string.Format("AF{0}", row)].Value = "";
                    }
                    else
                    {
                        Sheet.Cells[string.Format("AF{0}", row)].Value = detalleprefactura.observaciones.ToUpper();
                    }

                    Sheet.Cells[string.Format("AG{0}", row)].Value = detalleprefactura.nuevo_valor;
                    Sheet.Cells[string.Format("AH{0}", row)].Value = detalleprefactura.totalConIva;

                    Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("U{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("X{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }
                var prefactura = Session["numPre"];

                string namefile = "ReportePreFacturasAprobadas_" + prefactura + "_" + DateTime.Now;
                Sheet.Cells["A:AH"].AutoFitColumns();

                documento = Ep.GetAsByteArray();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('ERROR EN LA DESCARGA DE APROBADOS.');" +
                  "</script> ";
                Response.Write(rta);
            }

            return documento;
        }

        public byte[] traerExcelACorreoOPLNoPasan()
        {
            byte[] documento = null;

            List<management_listadoPrefacturasNoCruzanResult> Listado = new List<management_listadoPrefacturasNoCruzanResult>();
            try
            {
                Listado = (List<management_listadoPrefacturasNoCruzanResult>)Session["no_aprobadosIni"];
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Cargue");
                GestionMedicamentos Model = new GestionMedicamentos();

                Sheet.Cells["A1:AG1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:AG1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AG1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AG1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AG1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Nro cargue";
                Sheet.Cells["B1"].Value = "Remision pre factura fact";
                Sheet.Cells["C1"].Value = "Num Tirilla o consecutivo";
                Sheet.Cells["D1"].Value = "Fecha radicacion";
                Sheet.Cells["E1"].Value = "Nit";
                Sheet.Cells["F1"].Value = "Tipo doc beneficiario";
                Sheet.Cells["G1"].Value = "Num documento beneficiario";
                Sheet.Cells["H1"].Value = "Nombre beneficiario";
                Sheet.Cells["I1"].Value = "Ciudad despacho";
                Sheet.Cells["J1"].Value = "Id prescriptor";
                Sheet.Cells["K1"].Value = "Prescriptor";
                Sheet.Cells["L1"].Value = "Especialidad";
                Sheet.Cells["M1"].Value = "Cum";
                Sheet.Cells["N1"].Value = "Cod Ecopetrol hijo comercial";
                Sheet.Cells["O1"].Value = "Cod interno medicamento";
                Sheet.Cells["P1"].Value = "Cod generico o interno medicamento";
                Sheet.Cells["Q1"].Value = "Presentacion";
                Sheet.Cells["R1"].Value = "Descripcion producto";
                Sheet.Cells["S1"].Value = "Nombre comercial medicamento";
                Sheet.Cells["T1"].Value = "Laboratorio fabricante";
                Sheet.Cells["U1"].Value = "Fecha despacho formula";
                Sheet.Cells["V1"].Value = "Num unidades prescritas";
                Sheet.Cells["W1"].Value = "Num formula";
                Sheet.Cells["X1"].Value = "Fecha formula";
                Sheet.Cells["Y1"].Value = "Num unidades entregadas";
                Sheet.Cells["Z1"].Value = "Vlr unitario und entregada";
                Sheet.Cells["AA1"].Value = "Valor Iva";
                Sheet.Cells["AB1"].Value = "Valor total";
                Sheet.Cells["AC1"].Value = "Codigo ATC";
                Sheet.Cells["AD1"].Value = "Grupo farmacologico";

                //Hasta aqui va la estructura como tal, los demas son campos agregados
                Sheet.Cells["AE1"].Value = "Diferencia";
                Sheet.Cells["AF1"].Value = "Observaciones";
                Sheet.Cells["AG1"].Value = "Nuevo valor unitario";

                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                int row = 2;

                foreach (var detalleprefactura in Listado)
                {
                    double ValorUno = Convert.ToDouble(detalleprefactura.vlr_unitario_und_entregada);
                    double ValorDos = Convert.ToDouble(detalleprefactura.nuevo_valor);

                    double resultado = ValorUno - ValorDos;

                    Sheet.Cells[string.Format("A{0}", row)].Value = detalleprefactura.Id_prefactura_cargue_base;
                    Sheet.Cells[string.Format("B{0}", row)].Value = detalleprefactura.remision_prefactura_fact;
                    Sheet.Cells[string.Format("C{0}", row)].Value = detalleprefactura.num_tirilla;
                    Sheet.Cells[string.Format("D{0}", row)].Value = detalleprefactura.fecha_radicacion;
                    Sheet.Cells[string.Format("E{0}", row)].Value = detalleprefactura.nit;
                    Sheet.Cells[string.Format("F{0}", row)].Value = detalleprefactura.tipo_id_beneficiario;
                    Sheet.Cells[string.Format("G{0}", row)].Value = detalleprefactura.num_documento_beneficiario;
                    Sheet.Cells[string.Format("H{0}", row)].Value = detalleprefactura.nombre_beneficiario;
                    Sheet.Cells[string.Format("I{0}", row)].Value = detalleprefactura.ciudad_despacho;
                    Sheet.Cells[string.Format("J{0}", row)].Value = detalleprefactura.id_prescriptor;
                    Sheet.Cells[string.Format("K{0}", row)].Value = detalleprefactura.prescriptor;
                    Sheet.Cells[string.Format("L{0}", row)].Value = detalleprefactura.especialidad;
                    Sheet.Cells[string.Format("M{0}", row)].Value = detalleprefactura.cum;
                    Sheet.Cells[string.Format("N{0}", row)].Value = detalleprefactura.cod_ecopetrol_hijo_comercial;
                    Sheet.Cells[string.Format("O{0}", row)].Value = detalleprefactura.cod_interno_medicamento;
                    Sheet.Cells[string.Format("P{0}", row)].Value = detalleprefactura.cod_generico_o_interno_medicamento;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = detalleprefactura.Presentacion;
                    Sheet.Cells[string.Format("R{0}", row)].Value = detalleprefactura.descripcion_producto;
                    Sheet.Cells[string.Format("S{0}", row)].Value = detalleprefactura.nombre_comercial_medicacmento;
                    Sheet.Cells[string.Format("T{0}", row)].Value = detalleprefactura.laboratorio_fabricante;
                    Sheet.Cells[string.Format("U{0}", row)].Value = detalleprefactura.fecha_despacho_formula;
                    Sheet.Cells[string.Format("V{0}", row)].Value = detalleprefactura.num_unidades_prescritas;
                    Sheet.Cells[string.Format("W{0}", row)].Value = detalleprefactura.num_formula;
                    Sheet.Cells[string.Format("X{0}", row)].Value = detalleprefactura.fecha_formula;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = detalleprefactura.num_unidades_entregadas;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = detalleprefactura.vlr_unitario_und_entregada;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = detalleprefactura.IVA;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = detalleprefactura.valor_total;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = detalleprefactura.CODIGO_ATC;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = detalleprefactura.grupo_farmacologico;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = resultado;

                    if (String.IsNullOrEmpty(detalleprefactura.observaciones))
                    {
                        Sheet.Cells[string.Format("AF{0}", row)].Value = "";
                    }
                    else
                    {
                        Sheet.Cells[string.Format("AF{0}", row)].Value = detalleprefactura.observaciones.ToUpper();
                    }

                    Sheet.Cells[string.Format("AG{0}", row)].Value = 0;

                    Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("U{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("X{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    row++;
                }

                var prefactura = Session["numPre"];

                string namefile = "ReportePreFacturas_NoAprobadas_" + prefactura + "_" + DateTime.Now;
                Sheet.Cells["A:AG"].AutoFitColumns();

                documento = Ep.GetAsByteArray();
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('ERROR EN LA DESCARGA DE NO APROBADOS.');" +
                   "</script> ";

                Response.Write(rta);
            }
            return documento;
        }

        public ActionResult CargueMedicamentosRegulados()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CargueMedicamentosRegulados(string vigenciadesde, string vigenciahasta, List<HttpPostedFileBase> files)
        {
            var path = "";
            GestionMedicamentos Model = new GestionMedicamentos();
            try
            {
                if (files.Count() > 0)
                {
                    HttpPostedFileBase file = files[0];

                    try
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

                        md_medicamentos_regulados obj = new md_medicamentos_regulados();
                        obj.vigencia_desde = Convert.ToDateTime(vigenciadesde);
                        obj.vigencia_hasta = Convert.ToDateTime(vigenciahasta);
                        obj.usuario_digita = SesionVar.UserName;
                        obj.fecha_digita = DateTime.Now;

                        Int32 lote = Model.ExcelMedicamentosRegulados(dataTable, obj, ref MsgRes);

                        var resultado = MsgRes.ResponseType;
                        var mensajeSalida = MsgRes.DescriptionResponse;

                        if (resultado == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            ViewData["MensajeRta"] = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE. </div>";
                        }
                        else
                        {
                            var mensaje = "ERROR AL INGRESAR CARGUE MEDICAMENTOS REGULADOS: " + MsgRes.DescriptionResponse;
                            BusClass.EliminarMedicamentosRegulados(lote, ref MsgRes);
                            ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                        }
                    }
                    catch (Exception e)
                    {
                        ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> NO SE HAN PODIDO CARGAR LOS REGISTROS: " + e.Message + ".</div>";
                    }
                }
                else
                {
                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> FALTA UN ARCHIVO EN FORMATO EXCEL. </div>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> NO SE HAN PODIDO CARGAR LOS REGISTROS: " + ex.Message + ".</div>";
            }

            return View();
        }

        public ActionResult TableroMedicamentosRegulados(int? rta)
        {
            List<management_prefacturas_listaMedicamentosReguladosResult> lista = new List<management_prefacturas_listaMedicamentosReguladosResult>();
            var conteo = 0;
            var mensaje = "";
            var respuesta = 0;

            try
            {
                lista = BusClass.ListaMedicamentosRegulados();
                conteo = lista.Count();
                if (rta != null)
                {
                    if (rta == 1)
                    {
                        mensaje = "CARGUE MEDICAMENTOS REGULADOS ELIMINADO CORRECTAMENTE.";

                    }
                    else
                    {
                        mensaje = "NO SE PUDO ELIMINAR EL CARGUE DE MEDICAMENTOS REGULADOS.";
                    }

                    respuesta = (int)rta;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.msg = mensaje;
            ViewBag.rta = respuesta;
            ViewBag.rol = SesionVar.ROL;

            return View();
        }

        public JsonResult EliminarCargueMedicamentosRegulados(int idMed)
        {

            var mensaje = "";
            var resultado = 0;
            var usuarioElimina = SesionVar.UserName;

            try
            {
                resultado = BusClass.EliminarMedicamentosRegulados(idMed, usuarioElimina);

                if (resultado != 0)
                {
                    mensaje = "CARGUE MEDICAMENTOS REGULADOS ELIMINADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR CARGUE MEDICAMENTOS REGULADOS";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR CARGUE MEDICAMENTOS REGULADOS";
            }

            return Json(new { success = true, msg = mensaje, rta = resultado }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargueMasivoOPL()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CargueMasivoOPL(List<HttpPostedFileBase> files)
        {
            var path = "";
            try
            {
                if (files.Count() > 0)
                {
                    HttpPostedFileBase file = files[0];

                    try
                    {
                        GestionMedicamentos Model = new GestionMedicamentos();

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

                        md_medicamentos_OPLS obj = new md_medicamentos_OPLS();
                        obj.usuario_digita = SesionVar.UserName;
                        obj.fecha_digita = DateTime.Now;

                        Int32 lote = Model.ExcelMedicamentosOPLS(dataTable, obj, ref MsgRes);

                        var resultado = MsgRes.ResponseType;
                        var mensajeSalida = MsgRes.DescriptionResponse;

                        if (resultado == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            ViewData["MensajeRta"] = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESARON CORRECTAMENTE LOS DATOS OPL. </div>";
                        }
                        else
                        {
                            var mensaje = "ERROR AL INGRESAR CARGUE DATOS OPL: " + MsgRes.DescriptionResponse;
                            BusClass.EliminarMedicamentosRegulados(lote, ref MsgRes);
                            ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                        }
                    }
                    catch (Exception e)
                    {
                        ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> NO SE HAN PODIDO CARGAR LOS REGISTROS: " + e.Message + ".</div>";
                    }
                }
                else
                {
                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> FALTA UN ARCHIVO EN FORMATO EXCEL. </div>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'><strong>Error!</strong> NO SE HAN PODIDO CARGAR LOS REGISTROS: " + ex.Message + ".</div>";
            }

            FileInfo fileDelete = new FileInfo(path);
            if (fileDelete != null)
            {
                fileDelete.Delete();
            }


            return View();
        }

        public JsonResult DesaprobarPrefacturas(List<int> ListaIds, int? idCargue, string observacion)
        {
            var mensaje = "";
            var rta = 0;
            List<log_prefacturas_desaprobacion> listaLog = new List<log_prefacturas_desaprobacion>();
            try
            {
                if (ListaIds.Count() > 0)
                {
                    var respuesta = BusClass.DesaprobarPrefacturas(ListaIds, observacion);

                    if (respuesta != 0)
                    {
                        var conteoLog = 0;
                        log_prefacturas_desaprobacion obj = new log_prefacturas_desaprobacion();
                        obj.id_cargue = idCargue;
                        obj.observacion = observacion;
                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = SesionVar.UserName;

                        foreach (var item in ListaIds)
                        {
                            obj.id_detalle = item;
                            listaLog.Add(obj);
                            conteoLog++;
                        }

                        if (conteoLog > 0 && listaLog.Count() > 0)
                        {
                            var respuestaLog = BusClass.guardarLogDesaprobacionPrefacturas(listaLog);
                        }

                        mensaje = "PREFACTURAS DESAPROBADAS CORRECTAMENTE";
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "ERROR AL DESAPROBAR LAS PREFACTURAS";
                    }
                }
                else
                {
                    mensaje = "Debe seleccionar almenos uno de los ítems";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL DESAPROBAR LAS PREFACTURAS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public void ExportarExcelCerradas(int? idCargue)
        {
            List<management_prefacturas_reporteCierreResult> Listado = new List<management_prefacturas_reporteCierreResult>();
            try
            {
                Listado = BusClass.ReportePrefacturasCerradas(idCargue);

                if (Listado.Count == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('SIN DATOS.');" +
                       "</script> ";

                    Response.Write(rta);
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Datos cerrados");
                GestionMedicamentos Model = new GestionMedicamentos();

                Sheet.Cells["A1:P1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:P1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:P1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "UNIS";
                Sheet.Cells["B1"].Value = "LOCALDAD";
                Sheet.Cells["C1"].Value = "TIPO DE IDENTIFICACIÓN";
                Sheet.Cells["D1"].Value = "IDENTIFICACIÓN DL PACIENTE";
                Sheet.Cells["E1"].Value = "CUM";
                Sheet.Cells["F1"].Value = "CONCEPTO";
                Sheet.Cells["G1"].Value = "CANTIDAD";
                Sheet.Cells["H1"].Value = "FECHA DISPENSACIÓN";
                Sheet.Cells["I1"].Value = "FECHA FACTURA";
                Sheet.Cells["J1"].Value = "NUMERO DE FACTURA";
                Sheet.Cells["K1"].Value = "VALOR";
                Sheet.Cells["L1"].Value = "NIT DEL PRESTADOR";
                Sheet.Cells["M1"].Value = "PRESTADOR";
                Sheet.Cells["N1"].Value = "FACTURA SIN LETRAS";
                Sheet.Cells["O1"].Value = "FORMULA";
                Sheet.Cells["P1"].Value = "NÚMERO PREFACTURA";

                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                int row = 2;

                foreach (var item in Listado)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.localidad;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.tipo_id_beneficiario;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.num_documento_beneficiario;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.cum;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.concepto;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.num_unidades_entregadas;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_dispensacion;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_factura;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.num_factura;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.valor;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.nit;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.nombrePrestador;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.numFactura_sinletras;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.num_formula;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.remision_prefactura_fact;

                    Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    row++;
                }

                var prefactura = Session["numPre"];

                string namefile = "ReportePreFacturasCerradas_" + prefactura + "_" + DateTime.Now;
                Sheet.Cells["A:P"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/xls";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('ERROR EN LA DESCARGA DE NO APROBADOS.');" +
                   "</script> ";

                Response.Write(rta);
            }
        }

        //Nuevo
        public ActionResult TableroInformacionCierres(string[] regional, string[] usuario, DateTime? fechaInicio, DateTime? fechaFin, string estado, int? opl)
        {

            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
            List<management_prefacturas_tableroCierreResult> lista = new List<management_prefacturas_tableroCierreResult>();
            List<management_prefacturas_tableroCierreResult> listaRegional = new List<management_prefacturas_tableroCierreResult>();
            List<management_prefacturas_tableroCierreResult> listaUsuarios = new List<management_prefacturas_tableroCierreResult>();
            List<management_prefacturas_tableroCierreResult> listaFinalizada = new List<management_prefacturas_tableroCierreResult>();
            var conteo = 0;
            var conteoAbiertos = 0;
            var conteoCerrados = 0;
            var conteoPromedioDiasCierre = 0;
            var usarFiltro = 0;

            try
            {
                if (fechaInicio != null && fechaFin != null)
                {
                    lista = BusClass.TableroInformacionCierrePrefacturas(fechaInicio, fechaFin);
                }

                if (lista.Count() > 0)
                {
                    if (regional != null)
                    {
                        if (regional.Count() > 0)
                        {
                            if (usarFiltro == 0)
                            {
                                usarFiltro = 1;
                            }

                            for (var i = 0; i < regional.Count(); i++)
                            {
                                List<management_prefacturas_tableroCierreResult> listaTemporal = new List<management_prefacturas_tableroCierreResult>();

                                if (listaFinalizada.Count() > 0)
                                {
                                    listaTemporal = listaFinalizada.Where(x => x.id_regional == Convert.ToInt32(regional[i])).ToList();
                                }
                                else
                                {
                                    listaTemporal = lista.Where(x => x.id_regional == Convert.ToInt32(regional[i])).ToList();
                                }
                                listaRegional.AddRange(listaTemporal);
                            }

                            listaFinalizada = listaRegional;
                        }
                    }

                    if (usuario != null)
                    {
                        if (usuario.Count() > 0)
                        {
                            if (usarFiltro == 0)
                            {
                                usarFiltro = 1;
                            }

                            for (var i = 0; i < usuario.Count(); i++)
                            {
                                List<management_prefacturas_tableroCierreResult> listaTemporal = new List<management_prefacturas_tableroCierreResult>();
                                if (listaFinalizada.Count() > 0)
                                {
                                    listaTemporal = listaFinalizada.Where(x => x.id_usuario == Convert.ToInt32(usuario[i])).ToList();
                                }
                                else
                                {
                                    listaTemporal = lista.Where(x => x.id_usuario == Convert.ToInt32(usuario[i])).ToList();
                                }

                                listaUsuarios.AddRange(listaTemporal);
                            }

                            listaFinalizada = listaUsuarios;
                        }
                    }

                    if (listaFinalizada.Count() == 0 && usarFiltro == 0)
                    {
                        listaFinalizada = lista;
                    }

                    //if (fechaInicio != null)
                    //{
                    //    listaFinalizada = listaFinalizada.Where(x => x.fechaCreacion > fechaInicio).ToList();
                    //}

                    //if (fechaFin != null)
                    //{
                    //    listaFinalizada = listaFinalizada.Where(x => x.fechaCreacion < fechaFin).ToList();
                    //}

                    if (!string.IsNullOrEmpty(estado))
                    {
                        listaFinalizada = listaFinalizada.Where(x => x.estado == estado).ToList();
                    }

                    if (opl != null && opl != 1)
                    {
                        listaFinalizada = listaFinalizada.Where(x => x.id_prestador == opl).ToList();
                    }

                    conteo = listaFinalizada.Count();

                    if (conteo > 0)
                    {
                        conteoAbiertos = listaFinalizada.Where(x => x.estado == "abierta").Count();
                        conteoCerrados = listaFinalizada.Where(x => x.estado == "cerrada").Count();
                        conteoPromedioDiasCierre = (int)(listaFinalizada.Where(x => x.estado == "cerrada").Select(x => x.diasDiferencia).Sum() / listaFinalizada.Count());
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["listaTablerCierre"] = listaFinalizada;

            ViewBag.lista = listaFinalizada;
            ViewBag.conteo = conteo;
            ViewBag.conteoAbiertos = conteoAbiertos;
            ViewBag.conteoCerrados = conteoCerrados;
            ViewBag.conteoPromedioDiasCierre = conteoPromedioDiasCierre;
            return View();
        }

        public void ExportarDatosTableroCierres()
        {
            List<management_prefacturas_tableroCierreResult> lista = new List<management_prefacturas_tableroCierreResult>();
            try
            {
                lista = (List<management_prefacturas_tableroCierreResult>)Session["listaTablerCierre"];

                if (lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('NO SE HAN ENCONTRADO RESULTADOS.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                else
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosDeCierre");

                    Sheet.Cells["A1:H1"].Style.Font.Bold = true;
                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:H1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:H1"].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A1"].Value = "ID CARGUE";
                    Sheet.Cells["B1"].Value = "FECHA CREACIÓN";
                    Sheet.Cells["C1"].Value = "ESTADO";
                    Sheet.Cells["D1"].Value = "DÍAS SIN CIERRE";
                    Sheet.Cells["E1"].Value = "FECHA CIERRE";
                    Sheet.Cells["F1"].Value = "DÍAS CREACIÓN-CIERRE";
                    Sheet.Cells["G1"].Value = "QUIEN CARGA";
                    Sheet.Cells["H1"].Value = "REGIONAL";

                    int row = 2;
                    foreach (management_prefacturas_tableroCierreResult item in lista)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_cargue_base;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.fechaCreacion;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.estado;
                        if (item.estado == "cerrada")
                        {
                            Sheet.Cells[string.Format("D{0}", row)].Value = 0;
                        }
                        else
                        {
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.diasSincierre;
                        }

                        Sheet.Cells[string.Format("E{0}", row)].Value = item.fechaCierre;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.diasDiferencia;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.nombre;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nombre_regional;

                        Sheet.Cells[string.Format("B{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    Sheet.Cells["A" + row + ":H1" + row].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A:H"].AutoFitColumns();

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment;filename = RptPrefacturasDatosCierre_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('PROBLEMAS AL GENERAR EL REPORTE - CONTACTE CON EL ÁREA SISTEMAS.');" +
                  "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public ActionResult TableroInformacionAhorro(string[] regional, string[] usuario, DateTime? fechaInicio, DateTime? fechaFin, int? opl)
        {
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
            List<management_prefacturas_tableroAhorroResult> lista = new List<management_prefacturas_tableroAhorroResult>();
            List<management_prefacturas_tableroAhorroResult> listaRegional = new List<management_prefacturas_tableroAhorroResult>();
            List<management_prefacturas_tableroAhorroResult> listaUsuarios = new List<management_prefacturas_tableroAhorroResult>();
            List<management_prefacturas_tableroAhorroResult> listaFinalizada = new List<management_prefacturas_tableroAhorroResult>();
            var conteo = 0;
            var conteoAbiertos = 0;
            var conteoCerrados = 0;
            var conteoPromedioDiasCierre = 0;
            var usarFiltro = 0;

            try
            {
                if (fechaInicio != null && fechaFin != null)
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.Timeout = TimeSpan.FromHours(1);

                    lista = BusClass.TableroInformacionAhorroPrefacturas(fechaInicio, fechaFin);
                }

                if (lista.Count() > 0)
                {
                    if (regional != null)
                    {
                        if (regional.Count() > 0)
                        {
                            if (usarFiltro == 0)
                            {
                                usarFiltro = 1;
                            }

                            for (var i = 0; i < regional.Count(); i++)
                            {
                                List<management_prefacturas_tableroAhorroResult> listaTemporal = new List<management_prefacturas_tableroAhorroResult>();

                                if (listaFinalizada.Count() > 0)
                                {
                                    listaTemporal = listaFinalizada.Where(x => x.id_regional == Convert.ToInt32(regional[i])).ToList();
                                }
                                else
                                {
                                    listaTemporal = lista.Where(x => x.id_regional == Convert.ToInt32(regional[i])).ToList();
                                }
                                listaRegional.AddRange(listaTemporal);
                            }

                            listaFinalizada = listaRegional;
                        }
                    }

                    if (usuario != null)
                    {
                        if (usuario.Count() > 0)
                        {
                            if (usarFiltro == 0)
                            {
                                usarFiltro = 1;
                            }

                            for (var i = 0; i < usuario.Count(); i++)
                            {
                                List<management_prefacturas_tableroAhorroResult> listaTemporal = new List<management_prefacturas_tableroAhorroResult>();
                                if (listaFinalizada.Count() > 0)
                                {
                                    listaTemporal = listaFinalizada.Where(x => x.id_usuario == Convert.ToInt32(usuario[i])).ToList();
                                }
                                else
                                {
                                    listaTemporal = lista.Where(x => x.id_usuario == Convert.ToInt32(usuario[i])).ToList();
                                }

                                listaUsuarios.AddRange(listaTemporal);
                            }

                            listaFinalizada = listaUsuarios;
                        }
                    }

                    listaFinalizada = listaFinalizada.Count() == 0 && usarFiltro == 0 ? lista : listaFinalizada;
                    listaFinalizada = opl != null && opl != 1 ? listaFinalizada.Where(x => x.id_prestador == opl).ToList() : listaFinalizada;

                    conteo = listaFinalizada.Count();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            Session["listaTablerAhorro"] = listaFinalizada;

            ViewBag.lista = listaFinalizada;
            ViewBag.conteo = conteo;
            return View();
        }

        public void ExportarDatosTablerAhorro()
        {
            List<management_prefacturas_tableroAhorroResult> lista = new List<management_prefacturas_tableroAhorroResult>();
            try
            {

                lista = (List<management_prefacturas_tableroAhorroResult>)Session["listaTablerAhorro"];

                if (lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('NO SE HAN ENCONTRADO RESULTADOS.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                else
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosDeAhorro");

                    Sheet.Cells["A1:H1"].Style.Font.Bold = true;
                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:H1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:H1"].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A1"].Value = "ID CARGUE";
                    Sheet.Cells["B1"].Value = "VALOR TOTAL INICIAL CARGUES CERRADOS";
                    Sheet.Cells["C1"].Value = "VALOR TOTAL FINAL CARGUES CERRADOS";
                    Sheet.Cells["D1"].Value = "VALOR NO APROBADOS";
                    Sheet.Cells["E1"].Value = "CAMBIO DE TARIFAS";
                    Sheet.Cells["F1"].Value = "AHORRO";
                    Sheet.Cells["G1"].Value = "QUIEN CARGA";
                    Sheet.Cells["H1"].Value = "REGIONAL";

                    int row = 2;
                    foreach (management_prefacturas_tableroAhorroResult item in lista)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_cargue_base;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.valorTotalInicial;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.valorTotalFinalCerrados;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.valorNoAprobados;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.cambioTarifas;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.ahorro;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.nombreCarga;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nombre_regional;

                        row++;
                    }

                    Sheet.Cells["A" + row + ":H1" + row].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A:H"].AutoFitColumns();

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=RptDatosPrefacturasAhorro_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('PROBLEMAS AL GENERAR EL REPORTE - CONTACTE CON EL ÁREA SISTEMAS.');" +
                  "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public string BuscarUsuarios(string[] regionales)
        {
            string retorno = "";

            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();
            List<management_usuarios_regionalResult> lista = new List<management_usuarios_regionalResult>();
            List<management_usuarios_regionalResult> listaFinal = new List<management_usuarios_regionalResult>();

            try
            {
                lista = BusClass.ListadoRegionalUsuario();

                for (var i = 0; i < regionales.Length; i++)
                {
                    List<management_usuarios_regionalResult> listado = new List<management_usuarios_regionalResult>();
                    listado = lista.Where(x => x.id_regional == Convert.ToInt32(regionales[i])).ToList();
                    listaFinal.AddRange(listado);
                }

                if (listaFinal.Count() > 0)
                {
                    foreach (var item in listaFinal)
                    {
                        retorno += "<option value='" + item.id_auditor + "'>" + item.nombre + "</option>";
                    }

                    listaFinal = listaFinal.OrderByDescending(x => x.nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return retorno;
        }

        //Fin nuevo
    }
}