using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.GestorDocumental;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.GestorDocumental
{
    [SessionExpireFilter]
    public class GestorDocumentalController : Controller
    {

        #region  PROPIEDADES

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

        private string nombre = string.Empty;
        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion

        #region METODOS PRIVADOS

        private string GuardarArchivoPqrs(int idpqrs, int tipodocumental, string observacion, string numerocaso)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            string strError = string.Empty;

            byte[] PdfData = null;
            MemoryStream target = new MemoryStream();
            Request.Files["FileUpload1"].InputStream.CopyTo(target);
            PdfData = target.ToArray();

            GestionDocumentalPQRS OBJ = new GestionDocumentalPQRS();

            OBJ.id_ecop_pqr = idpqrs;
            OBJ.id_tipo_documental = tipodocumental;
            OBJ.obs = observacion;
            OBJ.ruta = PdfData;
            OBJ.cargue_fecha = DateTime.Now;
            OBJ.cargue_usuario = SesionVar.UserName;
            OBJ.numero_caso = numerocaso;

            Model.InsertarGestionDocPQRS(OBJ, ref MsgRes);


            return strError;
        }
        private string cargarArchivosPqrs(int idpqrs, int tipodocumental, string observacion, string numerocaso)
        {
            string strRetorno = string.Empty;
            GestionDocumentalPQRS PQRS = new GestionDocumentalPQRS();
            try
            {

                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = Request.Files["FileUpload1"].FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    string[] formatos = new string[] { "pdf" };

                    if (Array.IndexOf(formatos, ext) < 0)
                    {
                        strRetorno = "Formato de archivo inválido.";
                    }
                    else
                    {
                        strRetorno = GuardarArchivoPqrs(idpqrs, tipodocumental, observacion, numerocaso);

                    }
                }


            }
            catch (Exception ex)
            {
                strRetorno = ex.Message;
            }
            return strRetorno;
        }

        private string cargarArchivos(Models.GestorDocumental.GestorDocumental Model)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentos"];
            sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
            string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + Request.Files["FileUpload1"].FileName);
            string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);
            try
            {

                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = Request.Files["FileUpload1"].FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    string[] formatos = new string[] { "pdf", "xls", "xlsx", "jpg", "png" };

                    if (Array.IndexOf(formatos, ext) < 0)
                    {
                        strRetorno = "Formato de archivo inválido.";
                    }
                    else
                    {
                        strRetorno = GuardarArchivoMedicamentos(Model);

                        string fileExt1 = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName);
                        string nombreActual1 = Request.Files["FileUpload1"].FileName;
                        string nombreCarpeta = nombreActual1.Remove(nombreActual1.Length - fileExt1.Length);
                        nombre = nombreCarpeta;
                    }
                }


            }
            catch (Exception ex)
            {
                strRetorno = ex.Message;
            }
            return strRetorno;
        }
        private string GuardarArchivoMedicamentos(Models.GestorDocumental.GestorDocumental Model)
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
            using (var binaryReader = new BinaryReader(Request.Files["FileUpload1"].InputStream))
            {
                ExcelData = binaryReader.ReadBytes(Request.Files["FileUpload1"].ContentLength);
            }
            Ref_gestion_tipo_documental obj = new Ref_gestion_tipo_documental();
            List<Ref_gestion_tipo_documental> lst = new List<Ref_gestion_tipo_documental>();
            obj.id_tipo_documental = Model.tipo_documento;

            lst = Model.ConsultaCodigoGD(obj, ref MsgRes);
            foreach (Ref_gestion_tipo_documental item in lst)
            {
                codigoddl = item.codigo;
                id_ref_tipo_documental = item.id_tipo_documental;
            }

            string ruta = "";
            String carpeta = "MEDICAMENTOS";
            String SubCarpeta = "";
            if (Model.tipo == 1)
            {
                SubCarpeta = "FACTURACION";
                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + SubCarpeta + "\\" + Model.numero_factura + "\\" + codigoddl);
            }
            else if (Model.tipo == 2)
            {
                SubCarpeta = "FACTURACION";
                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + SubCarpeta + "\\" + Model.numero_factura + "\\" + Model.numero_formula + "\\" + codigoddl);
            }

            else if (Model.tipo == 3)
            {
                SubCarpeta = "CALIDAD";
                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + SubCarpeta + "\\" + Model.numero_proveedor + "\\" + Model.id_indicadores_medicamentos + "\\" + codigoddl);
            }

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            if (lst.Count != 0)
            {
                if (Model.proceso == 3)
                {

                    GestionDocumentalMedicamentos objGD = new GestionDocumentalMedicamentos();
                    GestionDocumentalMedicamentosCad objGD2 = new GestionDocumentalMedicamentosCad();

                    if (Model.tipo == 1)
                    {
                        archivo = String.Format("{0}\\{1}_{2}_{3:yyyyMMdd_hhmm}{4}", ruta,
                        Model.numero_factura.ToString(), codigoddl, fecha, Path.GetExtension(Request.Files["FileUpload1"].FileName));

                        objGD.numero_factura = Model.numero_factura;
                        objGD.numero_documento = 0;
                        objGD.numero_formula = "0";

                        objGD.id_tipo_documental = id_ref_tipo_documental;
                        objGD.ruta = Convert.ToString(archivo);
                        objGD.obs = Model.observacion;
                        objGD.cargue_usuario = SesionVar.UserName;
                        objGD.cargue_fecha = DateTime.Now;
                        objGD.ruta_byte = ExcelData;

                        Model.InsertarGestionDoc(objGD, ref MsgRes);
                        Request.Files["FileUpload1"].SaveAs(archivo);

                    }
                    else if (Model.tipo == 2)
                    {

                        archivo = String.Format("{0}\\{1}_{2}_{3:yyyyMMdd_hhmm}{4}", ruta,

                        Model.numero_documento.ToString(), codigoddl, fecha, Path.GetExtension(Request.Files["FileUpload1"].FileName));

                        objGD.numero_factura = Model.numero_factura;
                        objGD.numero_documento = 0;
                        objGD.numero_formula = Model.numero_formula;

                        objGD.id_tipo_documental = id_ref_tipo_documental;
                        objGD.ruta = Convert.ToString(archivo);
                        objGD.ruta_byte = ExcelData;
                        objGD.obs = Model.observacion;
                        objGD.cargue_usuario = SesionVar.UserName;
                        objGD.cargue_fecha = DateTime.Now;
                        Model.InsertarGestionDoc(objGD, ref MsgRes);
                        Request.Files["FileUpload1"].SaveAs(archivo);
                    }

                    else if (Model.tipo == 3)
                    {
                        archivo = String.Format("{0}\\{1}_{2}_{3:yyyyMMdd_hhmm}{4}", ruta,

                        Model.numero_documento.ToString(), codigoddl, fecha, Path.GetExtension(Request.Files["FileUpload1"].FileName));

                        objGD2.nombre_auditado = Model.numero_proveedor;
                        objGD2.id_indicadores_medicamentos = Model.id_indicadores_medicamentos;
                        objGD2.id_md_indicador_detalle = 0;
                        objGD2.id_tipo_documental = id_ref_tipo_documental;
                        objGD2.ruta = Convert.ToString(archivo);
                        objGD2.ruta_byte = ExcelData;
                        objGD2.obs = Model.observacion;
                        objGD2.cargue_usuario = SesionVar.UserName;
                        objGD2.cargue_fecha = DateTime.Now;
                        Model.InsertarGestionDocMedCalidad(objGD2, ref MsgRes);

                        Request.Files["FileUpload1"].SaveAs(archivo);
                    }


                }
                else
                {

                }

            }
            else
            {
                strError = "Error debe seleccionar todas las listas";
            }

            return strError;
        }


        #endregion

        public ActionResult AddDocumentos()
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            return View(Model);
        }

        public ActionResult AddDocumentosMedicamentos(String factura)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_factura = factura;


            return View(Model);
        }

        public ActionResult AddDocumentosMedicamentosCalidad(String proveedor)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_proveedor = proveedor;


            return View(Model);
        }

        public ActionResult AddDocumentosPQR(int pqrs, string numerocaso, string tipodocumento, int? rta)
        {
            Models.General gnral = new Models.General();
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            ViewBag.lstdocumental = Model.ConsultaGestionTipoDocumental(1);
            ViewBag.numerocaso = numerocaso;
            ViewBag.pqrs = pqrs;
            ViewData["tipodocumento"] = tipodocumento;
            string alerta = "";
            if (rta != null)
            {
                if (rta == 1)
                {
                    alerta = gnral.MsgRespuesta("success", "CARGUE EXITOSO", "El documento ha sido cargado exitosamente.");
                }
                else
                {
                    alerta = gnral.MsgRespuesta("danger", "CARGUE FALLIDO", "Ha ocurrido un error al momento de cargar el archivo.");
                }
            }

            ViewData["alerta"] = alerta;
            return View();
        }

        public ActionResult AddDocumentoVisitas(int? rta)
        {
            ViewData["Respuesta"] = rta;
            return View();
        }

        public ActionResult Subirdocumento(int documento, int proceso, String formula, String factura, int tipo)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_documento = documento;
            Model.ConsultaGestionTipoDocumental(proceso);
            if (proceso == 3)
            {
                if (tipo == 1)
                {
                    Model.numero_factura = factura;
                    Model.tipo = tipo;
                    Model.proceso = proceso;
                    Model.numero_formula = "NA";
                }
                else
                {
                    Model.numero_documento = documento;
                    Model.numero_factura = factura;
                    Model.numero_formula = formula;
                    Model.tipo = tipo;
                    Model.proceso = proceso;

                }
            }
            else
            {

            }

            return View(Model);
        }

        public ActionResult SubirdocumentoMedCalidad(int id, int proceso, String numero_proveedor, int tipo)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();


            Model.ConsultaGestionTipoDocumental(proceso);

            if (tipo == 3)
            {
                Model.numero_proveedor = numero_proveedor;
                Model.tipo = tipo;
                Model.proceso = proceso;
                Model.id_indicadores_medicamentos = id;
            }



            return View(Model);
        }


        public ActionResult Verdocumento()
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            ViewBag.rol = SesionVar.ROL;
            return View(Model);
        }

        public ActionResult VerdoFacturaMedicamentos(String factura)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_factura = factura;
            Model.ConsultaFactura();

            return View(Model);
        }

        public ActionResult VerdocumentoMedicamentos(String documento)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_documento = Convert.ToDecimal(documento);

            return View(Model);
        }

        public ActionResult VerdoFacturaMedicamentosCad(String proveedor)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_proveedor = proveedor;



            return View(Model);
        }

        public ActionResult VerdoCasoPQRS(string numcaso)
        {
            Models.General General = new Models.General();

            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            List<GestionDocumentalPQRS> LISTA = new List<GestionDocumentalPQRS>();
            ViewBag.docspqrs = Model.LstGestionDocPQRS(numcaso);
            LISTA = ViewBag.docspqrs;
            if (LISTA.Count != 0)
            {

                ViewData["alerta"] = "";
            }
            else
            {
                ViewData["alerta"] = General.MsgRespuesta("warning", "El numero de caso no cuenta con documentos cargados!", "");
            }


            return View();
        }

        public ActionResult VerdoFacturaMedicamentosCadDetalle(Int32 id)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.id_indicadores_medicamentos = id;


            return View(Model);
        }

        public ActionResult EliminarDocumento()
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            return View(Model);
        }

        public ActionResult Comunicaciones(Int32? id)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            ViewBag.idrole = SesionVar.ROL;

            Models.General General = new Models.General();

            if (id == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "comunicado Ingresado Correctamente");
            }
            else
            {
                ViewData["alerta"] = "";
            }

            ViewBag.dirigido = BusClass.GetDirigido();
            ViewBag.tipo = BusClass.GetMdTipo();
            return View(Model);
        }


        [HttpPost]
        public ActionResult Comunicaciones(Models.GestorDocumental.GestorDocumental Model)
        {


            Models.General General = new Models.General();


            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                if (Model.id_com_digirido != 0)
                {
                    if (Model.id_puntos_dispensacion != 0)
                    {
                        if (Model.id_com_tipo != 0)
                        {
                            string ext = Request.Files["FileUpload1"].FileName;
                            ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                            string[] formatos = new string[] { "pdf" };
                            if (Array.IndexOf(formatos, ext) < 0)
                            {
                                ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", "Formato de archivo inválido debe Seleccionar un archivo PDF.");
                            }
                            else
                            {
                                byte[] PDF = null;

                                using (var binaryReader = new BinaryReader(Request.Files["FileUpload1"].InputStream))
                                {
                                    PDF = binaryReader.ReadBytes(Request.Files["FileUpload1"].ContentLength);
                                }
                                Model.ObjComu.archivo_digitalizado = PDF;
                                Model.ObjComu.id_ref_com_dirigido = Model.id_com_digirido;
                                Model.ObjComu.id_ref_puntos_dispersacion = Model.id_puntos_dispensacion;
                                Model.ObjComu.id_ref_com_tipo = Model.id_com_tipo;
                                Model.ObjComu.observaciones = Model.observaciones_comunicado;
                                Model.ObjComu.fecha = Model.fecha_coOK;
                                Model.ObjComu.fecha_digita = DateTime.Now;
                                Model.ObjComu.usuario_digita = SesionVar.UserName;

                                Model.InsertarComunicaciones(ref MsgRes);

                                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                                {
                                    return RedirectToAction("Comunicaciones", "GestorDocumental", new { id = Model.referencia = 1 });
                                }
                                else
                                {
                                    ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", MsgRes.DescriptionResponse);
                                }
                            }


                        }
                        else
                        {
                            ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", "Debe ingresar el campo tipo");
                        }
                    }
                    else
                    {
                        ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", "Debe ingresar el campo puntos dispensacion");
                    }



                }
                else
                {
                    ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", "Debe ingresar el campo dirigido");
                }
            }
            else
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", "Debe Seleccionar un archivo PDF");
            }

            return View(Model);

        }

        [HttpPost]
        public JsonResult SaveComunicaciones(HttpPostedFileBase file, Models.GestorDocumental.GestorDocumental Model)
        {

            String mensaje = "";
            String tipoAlerta = "";
            Models.General General = new Models.General();

            if (this.Request.Files["file"].ContentLength > 0)
            {

                try
                {
                    MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                    byte[] PDF = null;

                    using (var binaryReader = new BinaryReader(Request.Files["file"].InputStream))
                    {
                        PDF = binaryReader.ReadBytes(Request.Files["file"].ContentLength);
                    }
                    Model.ObjComu.archivo_digitalizado = PDF;
                    Model.ObjComu.id_ref_com_dirigido = Model.id_com_digirido;
                    Model.ObjComu.id_ref_puntos_dispersacion = Model.id_puntos_dispensacion;
                    Model.ObjComu.id_ref_com_tipo = Model.id_com_tipo;
                    Model.ObjComu.observaciones = Model.observaciones_comunicado;
                    Model.ObjComu.fecha = Model.fecha_coOK;
                    Model.ObjComu.fecha_digita = DateTime.Now;
                    Model.ObjComu.usuario_digita = SesionVar.UserName;



                    Model.InsertarComunicaciones(ref MsgRes);


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
        public ActionResult AddDocumentos(Models.GestorDocumental.GestorDocumental Model)
        {
            if (Model.proceso == 1)
            {

            }
            if (Model.proceso == 2)
            {

            }
            if (Model.proceso == 3)
            {
                if (Model.tipo_consulta == 1)
                {
                    return RedirectToAction("AddDocumentosMedicamentos", "GestorDocumental", new { factura = Model.numero_factura });
                }
                else if (Model.tipo_consulta == 2)
                {
                    return RedirectToAction("AddDocumentosMedicamentosCalidad", "GestorDocumental", new { proveedor = Model.numero_proveedor });
                }



            }
            if (Model.proceso == 4)
            {
                return RedirectToAction("AddDocumentosMedicamentos", "GestorDocumental", new { factura = Model.numero_factura_cuentas });
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult Subirdocumento(Models.GestorDocumental.GestorDocumental Model)
        {
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {

                string strInfo = string.Empty;
                strInfo = cargarArchivos(Model);
                if (string.IsNullOrEmpty(strInfo))
                {
                    //return RedirectToAction("AddDocumentosMedicamentos", "GestorDocumental");
                    return RedirectToAction("AddDocumentosMedicamentos", "GestorDocumental", new { factura = Model.numero_factura });
                }
                else
                {
                    ModelState.AddModelError("", strInfo);
                }

            }
            else
            {
                ModelState.AddModelError("", "Error.. Debe Seleccionar el archivo...!");

            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult SubirdocumentoMedCalidad(Models.GestorDocumental.GestorDocumental Model)
        {
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                string strInfo = string.Empty;
                strInfo = cargarArchivos(Model);
                if (string.IsNullOrEmpty(strInfo))
                {

                    return RedirectToAction("AddDocumentos", "GestorDocumental");

                }
                else
                {
                    ModelState.AddModelError("", strInfo);
                }

            }
            else
            {
                ModelState.AddModelError("", "Error.. Debe Seleccionar el archivo...!");

            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult Subirdocumentopqrs(HttpPostedFileBase FileUpload, int idpqrs, int tipodocumental, string observacion, string numerocaso)
        {
            int rta = 0;
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            String strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentos"] + "\\PQRS\\" + numerocaso;
            string filename = idpqrs + observacion.Substring(0, 4) + FileUpload.FileName;

            if (!System.IO.Directory.Exists(strRutaDefinitiva))
            {
                Directory.CreateDirectory(strRutaDefinitiva);
            }
            try
            {
                //Guardar documento en carpeta local
                var path = Path.Combine(strRutaDefinitiva, filename);
                FileUpload.SaveAs(path);

                //Guardar su registro en base de datos
                GestionDocumentalPQRS obj = new GestionDocumentalPQRS();
                obj.id_tipo_documental = tipodocumental;
                obj.id_ecop_pqr = idpqrs;
                //obj.ruta = path;
                obj.numero_caso = numerocaso;
                obj.obs = observacion;
                obj.cargue_usuario = SesionVar.UserName;
                obj.cargue_fecha = DateTime.Now;
                Model.InsertarGestionDocPQRS(obj, ref MsgRes);
                rta = 1;

            }
            catch (Exception ex)
            {
                rta = 2;
            }

            return RedirectToAction("AddDocumentosPQR", "GestorDocumental", new { pqrs = idpqrs, numerocaso = numerocaso, rta = rta });
        }


        [HttpPost]
        public ActionResult Cargardocumentopqrs(int idpqrs, int tipodocumental, string observacion, string numerocaso)
        {
            int rta = 0;
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();


            try
            {

                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    string strInfo = string.Empty;

                    strInfo = cargarArchivosPqrs(idpqrs, tipodocumental, observacion, numerocaso);
                    rta = 1;
                    return RedirectToAction("AddDocumentosPQR", "GestorDocumental", new { pqrs = idpqrs, numerocaso = numerocaso, rta = rta });

                }
                else
                {
                    rta = 2;
                    return RedirectToAction("AddDocumentosPQR", "GestorDocumental", new { pqrs = idpqrs, numerocaso = numerocaso, rta = rta });
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                rta = 2;
                return RedirectToAction("AddDocumentosPQR", "GestorDocumental", new { pqrs = idpqrs, numerocaso = numerocaso, rta = rta });
            }

        }

        [HttpPost]
        public ActionResult SubirdocumentoVisitasCalidad(HttpPostedFileBase file, int idprestador, string observaciones)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }

            GestionDocumentalVisitasCalidad obj = new GestionDocumentalVisitasCalidad();
            obj.documento = fileData;
            obj.id_prestador = idprestador;
            obj.ext = Path.GetExtension(file.FileName);
            obj.contentype = file.ContentType;
            obj.usuario_digita = SesionVar.UserName;
            obj.fecha_digita = DateTime.Now.ToString();
            if (!string.IsNullOrEmpty(observaciones))
            {
                obj.observaciones = observaciones;
            }

            Model.InsertarGestionDocVisitasCalidad(obj, ref MsgRes);
            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return RedirectToAction("AddDocumentoVisitas", "GestorDocumental", new { rta = 1 });
            }
            else
            {
                return RedirectToAction("AddDocumentoVisitas", "GestorDocumental", new { rta = 2 });
            }

        }

        [HttpPost]
        public ActionResult Verdocumento(Models.GestorDocumental.GestorDocumental Model)
        {
            if (Model.proceso == 1)
            {
                return RedirectToAction("VerdoCasoPQRS", "GestorDocumental", new { numcaso = Model.numero_caso });
            }
            if (Model.proceso == 2)
            {

            }
            if (Model.proceso == 3)
            {
                if (Model.tipo_consulta == 1)
                {
                    return RedirectToAction("VerdoFacturaMedicamentos", "GestorDocumental", new { factura = Model.numero_factura });
                }
                else if (Model.tipo_consulta == 2)
                {
                    return RedirectToAction("VerdoFacturaMedicamentosCad", "GestorDocumental", new { proveedor = Model.numero_proveedor });
                }


            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult EliminarDocumento(Models.GestorDocumental.GestorDocumental Model)
        {
            if (Model.proceso == 1)
            {
                return RedirectToAction("EliminarDocspqrs", "GestorDocumental", new { numcaso = Model.numero_caso });
            }
            if (Model.proceso == 2)
            {

            }
            if (Model.proceso == 3)
            {
                if (Model.tipo_consulta == 1)
                {
                    return RedirectToAction("EliminarMedicamentos", "GestorDocumental", new { factura = Model.numero_factura });
                }
                else if (Model.tipo_consulta == 2)
                {
                    return RedirectToAction("EliminarFacturaMedicamentosCad", "GestorDocumental", new { proveedor = Model.numero_proveedor });
                }


            }
            return View(Model);
        }

        public ActionResult EliminarMedicamentos(String factura)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_factura = factura;


            return View(Model);
        }

        public ActionResult EliminarFacturaMedicamentosCad(String proveedor)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_proveedor = proveedor;



            return View(Model);
        }

        public ActionResult EliminarFacturaMedicamentosCadDetalle(Int32 id, String proveedor)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.id_indicadores_medicamentos = id;
            Model.numero_proveedor = proveedor;

            return View(Model);
        }

        public ActionResult EliminarDocspqrs(string numcaso)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            List<GestionDocumentalPQRS> LISTA = new List<GestionDocumentalPQRS>();
            ViewBag.docspqrs = Model.LstGestionDocPQRS(numcaso);
            return View();
        }

        public ActionResult GestorDocs(string id)
        {

            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            List<vw_g_documental_medicamentos> ListUrl2 = Model.ConsultaIdGestionDocumental2(Convert.ToInt32(id), ref MsgRes);

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


        public ActionResult GestorpRuebaa()
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            List<GestionDocumentalMedicamentos> list = new List<GestionDocumentalMedicamentos>();
            foreach (var item in Model.LstGestionMedMas)
            {
                try
                {
                    GestionDocumentalMedicamentos obj = new GestionDocumentalMedicamentos();

                    obj.id_gestion_documental__medicamentos = item.id_gestion_documental__medicamentos;
                    string filename = item.ruta;
                    WebClient User = new WebClient();
                    Byte[] FileBuffer = User.DownloadData(filename);

                    obj.ruta_byte = FileBuffer;

                    //Binary binary = System.Text.Encoding.ASCII.GetBytes(item.ruta);
                    //byte[] array = binary.ToArray();
                    //obj.ruta_byte = array;

                    Model.ActualizarRutaByteMed(obj, ref MsgRes);
                }
                catch (WebException dirEx)
                {

                    GestionDocumentalMedicamentos obj = new GestionDocumentalMedicamentos();

                    obj.ruta = dirEx.Message;

                    list.Add(obj);


                }

            }
            ViewBag.lista = list.ToList();
            var fileDownloadName = String.Format("RutaConError.xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            // Pass your ef data to method
            ExcelPackage package = GenerateExcelFile(list.ToList());

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
            //return View(Model);

        }

        public ActionResult GestorpRuebaa2()
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            List<management_masivo_pqrsResult> list = new List<management_masivo_pqrsResult>();
            list = Model.GestionDocumentalmasivo2();

            foreach (var item in list)
            {
                try
                {
                    WebClient User = new WebClient();
                    Binary binary = item.ruta_byte;
                    byte[] array = binary.ToArray();
                    HttpPostedFileBase sigFile = (HttpPostedFileBase)new CustomPostedFile(array, item.id_ecop_pqr + ".pdf");

                    string strRetorno = string.Empty;
                    StringBuilder sbRutaDefinitiva;
                    string strRutaDefinitiva = string.Empty;
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                    sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                    string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + sigFile.FileName);
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                    MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                    string strError = string.Empty;

                    DateTime fecha = DateTime.Now;
                    string archivo = string.Empty;

                    String carpeta = "";

                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                    {
                        carpeta = "PQRSGestion";
                    }
                    else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                    {
                        carpeta = "PQRSGestionPruebas";
                    }

                    ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + item.id_ecop_pqr);
                    var nombre = Path.GetFileNameWithoutExtension(sigFile.FileName);
                    archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                    fecha, nombre, Path.GetExtension(sigFile.FileName));


                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    //sigFile.SaveAs(archivo);
                    string filename2 = archivo;

                    using (FileStream fs = new FileStream(filename2, FileMode.Create))
                    {
                        fs.Write(array, 0, array.Length);
                    }

                    GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                    OBJ.id_gestion_documental_pqrs = item.id_gestion_documental_pqrs;
                    OBJ.ruta = Convert.ToString(archivo);

                    Model.ActualizarRutaBytePQRS(OBJ, ref MsgRes);
                }
                catch (WebException dirEx)
                {
                    ModelState.AddModelError("", "ERROR... ACTUALIZANDO....");
                }

            }
            return RedirectToAction("Verdocumento", "GestorDocumental");
        }

        private static ExcelPackage GenerateExcelFile(List<GestionDocumentalMedicamentos> datasource)
        {

            ExcelPackage pck = new ExcelPackage();

            //Create the worksheet 
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Casos con error");

            // Sets Headers
            ws.Cells[1, 1].Value = "Numero Ruta";


            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).ruta;

            }

            // Format Header of Table
            using (ExcelRange rng = ws.Cells["A1:B1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid; //Set Pattern for the background to Solid 
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray); //Set color to DarkGray 
                rng.Style.Font.Color.SetColor(Color.White);
            }
            return pck;
        }
        public ActionResult GestorDocsCal(string Ruta)
        {

            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();


            string dirpath = Path.Combine(Request.PhysicalApplicationPath, Ruta);
            List<vw_g_documental_medicamentos> ListUrl3 = new List<vw_g_documental_medicamentos>();
            Ruta = dirpath;

            string filename = Ruta;
            WebClient User = new WebClient();
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



            return View(Model);

        }

        public ActionResult GestorElimDocsMed(Int32 id, string Ruta, String factura)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_factura = factura;

            Model.EliminarDocumento_med(id, ref MsgRes);

            String dato = Ruta;
            System.IO.File.Delete(dato);


            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                Log_GestionDocumental log = new Log_GestionDocumental();
                log.id_proceso = 3;
                log.actividad = "Eliminacion documento medicamentos  facturacion";
                log.usuario = SesionVar.UserName;
                log.fecha = DateTime.Now;
                Model.InsertarLogActividad(log);

                return RedirectToAction("EliminarMedicamentos", "GestorDocumental", new { factura = Model.numero_factura });
            }
            else
            {
                ModelState.AddModelError("", "ERROR...!");
            }

            return View(Model);
        }

        public ActionResult GestorElimDocsCal(Int32 id, string Ruta, Int32 idM, String proveedor)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.numero_proveedor = proveedor;
            Model.id_indicadores_medicamentos = idM;

            Model.EliminarDocumento_med_cal(id, ref MsgRes);

            String dato = Ruta;
            System.IO.File.Delete(dato);


            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                Log_GestionDocumental log = new Log_GestionDocumental();
                log.id_proceso = 3;
                log.actividad = "Eliminacion documento medicamentos  calidad";
                log.usuario = SesionVar.UserName;
                log.fecha = DateTime.Now;
                Model.InsertarLogActividad(log);

                return RedirectToAction("EliminarFacturaMedicamentosCadDetalle", "GestorDocumental", new { id = Model.id_indicadores_medicamentos, proveedor = Model.numero_proveedor });
            }
            else
            {
                ModelState.AddModelError("", "ERROR...!");
            }

            return View(Model);
        }

        public ActionResult GestorDocsPQR(Int32? id)
        {

            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            try
            {
                List<GestionDocumentalPQRS> LsitUrl2 = Model.GetUrlDocumentosPqrs(Convert.ToInt32(id));
                foreach (GestionDocumentalPQRS x in LsitUrl2)
                {

                    WebClient User = new WebClient();
                    Binary binary = x.ruta;
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
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return View(Model);
            }

            //string dirpath = Path.Combine(Request.PhysicalApplicationPath, Ruta);
            //Ruta = dirpath;
            //string[] extensiones = new string[6] { "JPG", "JPEG", "PNG", "PDF", "XLS", "XLSX", };
            //string filename = Ruta;
            //WebClient User = new WebClient();
            //Byte[] FileBuffer = User.DownloadData(filename);
            //if (FileBuffer != null)
            //{

            //    string ext = filename.Split('.').LastOrDefault();
            //    Response.ClearContent();
            //    Response.ClearHeaders();
            //    Response.Clear();

            //    if (ext.ToUpper() == extensiones[0] || ext.ToUpper() == extensiones[1])
            //        Response.ContentType = "image/jpeg";

            //    if (ext.ToUpper() == extensiones[2])
            //        Response.ContentType = "image/png";

            //    if (ext.ToUpper() == extensiones[3])
            //        Response.ContentType = "application/pdf";

            //    if (ext.ToUpper() == extensiones[4] || ext.ToUpper() == extensiones[5])
            //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


            //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
            //    Response.BinaryWrite(FileBuffer);
            //    Response.Flush();


            //}
            return View(Model);

        }

        public ActionResult Descargar(string id)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            Model.ConsultaIdGestionDocumental(Convert.ToInt32(id), ref MsgRes);
            string filename = Model.OBJGestionD.ruta;
            System.IO.FileInfo toDownload = new System.IO.FileInfo(filename);

            Response.Clear();

            Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
            Response.AddHeader("Content-Length", toDownload.Length.ToString());


            Response.ContentType = "application/octet-stream";

            Response.WriteFile(filename);

            Response.End();

            return View();
        }

        public ActionResult ActualizarImagenes(Binary codigo)
        {
            Models.Medicamentos.GestionMedicamentos Model = new Models.Medicamentos.GestionMedicamentos();

            Binary binary = codigo;
            byte[] array = binary.ToArray();

            return File(array, "image/jpeg");
        }

        [HttpPost]
        public ActionResult ConsultaFac(String id, Models.GestorDocumental.GestorDocumental Model)
        {
            if (id != null)
            {
                String mensaje = "";
                List<vw_fac_consolidado> lst = new List<vw_fac_consolidado>();

                Model.numero_factura = id;

                lst = Model.LstFacMed2;
                if (lst.Count != 0)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult ConsultaFacCuentasMed(String id)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            if (id != null)
            {
                String mensaje = "";

                List<vw_ffmm_consulta_radicacion_ips> lst = new List<vw_ffmm_consulta_radicacion_ips>();

                Model.BuscarRadicacionIPsId(Convert.ToInt32(id));

                if (Model.LisRadicacionIPSFacturas.Count != 0)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult ConsultaDoc(int id, Models.GestorDocumental.GestorDocumental Model)
        {
            if (id != 0)
            {
                String mensaje = "";
                List<vw_fac_consolidado> lst = new List<vw_fac_consolidado>();

                Model.numero_documento = id;

                lst = Model.LstDocMed2;
                if (lst.Count != 0)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult ConsultaProve(String id, Models.GestorDocumental.GestorDocumental Model)
        {
            if (id != null)
            {
                String mensaje = "";
                List<vw_fac_consolidado> lst = new List<vw_fac_consolidado>();

                Model.numero_factura = id;

                lst = Model.LstFacMed2;
                if (lst.Count != 0)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCascadeProveedores(string nombre_auditado, Models.GestorDocumental.GestorDocumental Model)
        {
            Model.ConsultaLista();
            return Json(Model.Listproveedor.Select(p => new { id_ref_proveedor = p.id_ref_proveedor, nombre = p.nombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Getpqrsbynumcaso(string numcaso)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            ecop_PQRS ecop = Model.ConsultaPQRSbyNumCaso(numcaso);

            string id = "";
            if (ecop != null)
            {
                id = ecop.id_ecop_PQRS.ToString();
            }
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarDocPQRS(Int32 id, string numcaso)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();

            GestionDocumentalPQRS gestor = Model.ConsultaGestorPQRSbyId(id);
            if (gestor != null)
            {
                //System.IO.File.Delete(gestor.ruta);

                bool elimino = (Model.EliminarDocPQRS(id));
                if (elimino)
                {
                    Log_GestionDocumental log = new Log_GestionDocumental();
                    log.id_proceso = 1;
                    log.actividad = "Eliminacion documento PQR";
                    log.usuario = SesionVar.UserName;
                    log.fecha = DateTime.Now;
                    Model.InsertarLogActividad(log);
                }
            }

            return RedirectToAction("EliminarDocspqrs", "GestorDocumental", new { numcaso = numcaso });
        }

        public JsonResult GetCascadeDirigido(Models.GestorDocumental.GestorDocumental Model)
        {
            return Json(Model.LstDirigido.Select(c => new { CategoryId = c.id_ref_com_dirigido, CategoryName = c.nombre_dirigido }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadePuntosDispensacion(string dirigido, Models.GestorDocumental.GestorDocumental Model)
        {

            Model.ConsultaListaPD(1, ref MsgRes);

            return Json(Model.LstPuntosDispersacion.Select(p => new { PuntosID = p.id_md_ref_puntos_dispensacion, PuntosName = p.nombre_esm }), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCascadeMdTipo(Models.GestorDocumental.GestorDocumental Model)
        {
            return Json(Model.LstMdTipo.Select(c => new { TipoId = c.id_ref_com_tipo, TipoName = c.des_tipo }), JsonRequestBehavior.AllowGet);
        }


        public ActionResult TableroControlComunicado()
        {

            ViewBag.listacomunicado = BusClass.GetDatosComunicado().ToList();
            ViewBag.meses = BusClass.meses();
            ViewBag.proveedor = BusClass.GetMD_Ref_proveedor();
            ViewBag.regionales = BusClass.GetRefRegion();
            ViewData["Rta"] = 0;
            ViewData["Mensaje"] = "";
            return View();
        }


        public JsonResult ArtefactoDocumento(Int32? iddocumento)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";


            return Json(new { iddocumento }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GestorUrlArtefactoDocumento(Int32 id_documental, MessageResponseOBJ MsgRes)
        {
            Models.GestorDocumental.GestorDocumental Model = new Models.GestorDocumental.GestorDocumental();
            try
            {
                List<md_comunicaciones> LsitUrl2 = BusClass.TraerdocumentoComunicados(id_documental);
                foreach (md_comunicaciones x in LsitUrl2)
                {

                    WebClient User = new WebClient();
                    Binary binary = x.archivo_digitalizado;
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
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return View(Model);
            }

            return View(Model);
        }


        public class CustomPostedFile : HttpPostedFileBase
        {
            private readonly byte[] fileBytes;
            MemoryStream stream;

            public CustomPostedFile(byte[] fileBytes, string filename = null)
            {
                this.fileBytes = fileBytes;
                this.FileName = filename;
                this.ContentType = "application/pdf";
                this.stream = new MemoryStream(fileBytes);
            }

            public override int ContentLength => fileBytes.Length;
            public override string FileName { get; }
            public override Stream InputStream
            {
                get { return stream; }
            }
            public override string ContentType { get; }


        }
    }
}