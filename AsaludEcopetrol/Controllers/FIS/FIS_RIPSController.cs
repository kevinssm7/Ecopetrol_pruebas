using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using Aspose.Cells;
using ClosedXML.Excel;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace AsaludEcopetrol.Controllers.FIS
{
    public class FIS_RIPSController : Controller
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

        public ActionResult CrearPrestador(int? idPrestador)
        {
            ViewBag.regionales = BusClass.GetRefRegion();
            management_fisPrestadoresResult prestador = new management_fisPrestadoresResult();
            List<management_fisPrestadores_sedesResult> listaSedes = new List<management_fisPrestadores_sedesResult>();

            try
            {
                if (idPrestador != null)
                {
                    prestador = BusClass.TraerPrestadorId(idPrestador);
                    listaSedes = BusClass.TraerPrestadorSedesId(idPrestador).Where(x => x.estado_sede == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.id_prestador = idPrestador;

            Session["ListadoSedes"] = listaSedes;

            ViewBag.regionales = BusClass.TraerregionalesFis();

            return View(prestador);
        }

        public string ObtenerDepartamentos(int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<management_fis_departamento_regionalResult> departamentos = new List<management_fis_departamento_regionalResult>();

            try
            {
                departamentos = BusClass.TraerDepartamentosFis(regional);
                foreach (var item in departamentos)
                {
                    result += "<option value='" + item.id_departamento + "'>" + item.nom_departamento + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public string ObtenerCiudades(int? departamento)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<fis_rips_ciudad> departamentos = new List<fis_rips_ciudad>();

            try
            {
                departamentos = BusClass.TraerCiudadesFis(departamento);
                foreach (var item in departamentos)
                {
                    result += "<option value='" + item.id_ciudad + "'>" + item.nom_ciudad + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public string ValidarExistenciaPrestadorNit(string IDPrestador)
        {
            var resultado = "";
            fis_rips_prestadores lista = new fis_rips_prestadores();
            try
            {
                lista = BusClass.PrestadorxNit(Convert.ToString(IDPrestador));
                resultado = lista != null ? "ESTE NIT PRESTADOR YA EXISTE" : "";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                resultado = "ERROR EN LA CONSULTA";
            }
            return resultado;
        }

        public string ValidarExistenciaPrestadorCodHab(string codHab)
        {
            var resultado = "";
            List<fis_rips_prestadores> lista = new List<fis_rips_prestadores>();
            try
            {
                lista = BusClass.ConsultaPrestadoresFisCodHab(codHab, ref MsgRes);
                resultado = lista.Count() > 0 ? "ESTE CODIGO HABILITACIÓN YA EXISTE" : "";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                resultado = "ERROR EN LA CONSULTA";
            }
            return resultado;
        }

        public JsonResult CrearSedesPrestadorGuardar(Models.FIS.FIS_RIPS Model)
        {
            String mensaje = "";
            fis_rips_prestadores_sedes prestadorSedes = new fis_rips_prestadores_sedes();
            List<management_fisPrestadores_sedesResult> TotalSedes = new List<management_fisPrestadores_sedesResult>();
            fis_rips_prestadores prestador = new fis_rips_prestadores();

            int i = 0;
            int ultestado = 0;
            string result = "";

            try
            {
                prestador.nit = Model.nit;
                prestador.codigo_verfificacion = Model.codigo_verfificacion;
                prestador.codigo_SAP = Model.codigo_SAP;
                prestador.codigo_habilitacion = Model.codigo_habilitacion;
                prestador.razon_social = Model.razon_social;
                prestador.ciudad_proveedor = Model.ciudad_proveedor;
                prestador.departamento_proveedor = Model.departamento_proveedor;
                prestador.regional = Model.regional;
                prestador.direccion = Model.direccion;
                prestador.contacto_telefonico = Model.contacto_telefonico;
                prestador.correo_electronico = Model.correo_electronico;
                prestador.estado = Model.estado;
                prestador.tiene_mas_sedes = Model.tiene_mas_sedes;
                prestador.fecha_digita = DateTime.Now;
                prestador.usuario_digita = SesionVar.UserName;

                if (Model.id_prestador == 0 || Model.id_prestador == null)
                {
                    Model.id_prestador = BusClass.InsertarPrestadorFis(prestador);
                }
                else
                {
                    prestador.id_prestador = (int)Model.id_prestador;
                    BusClass.ActualizarDatosPrestador(prestador);
                }

                var codigo_habilitacion = Model.codigo_habilitacion_sede;
                var OtraSede = Model.codigo_otra_sede;

                var existe = BusClass.TraerprestadorSedeCodHabilitacion(OtraSede, codigo_habilitacion, Model.id_prestador);

                if (existe != 0)
                {
                    return Json(new { success = false, message = "ESTE CÓDIGO HABILITACIÓN Y CÓDIGO SEDE YA ESTÁN REGISTRADOS" }, JsonRequestBehavior.AllowGet);
                }

                prestadorSedes.id_prestador_sede = Model.id_prestador;
                prestadorSedes.codigo_habilitacion_sede = Model.codigo_habilitacion_sede;
                prestadorSedes.codigo_otra_sede = Model.codigo_otra_sede;
                prestadorSedes.ciudad_sede = Model.ciudad_sede;
                prestadorSedes.departamento_sede = Model.departamento_sede;
                prestadorSedes.regional_sede = Model.regional_sede;
                prestadorSedes.direccion_sede = Model.direccion_sede;
                prestadorSedes.contacto_telefonico_sede = Model.contacto_telefonico_sede;
                prestadorSedes.correo_electronico_sede = Model.correo_electronico_sede;
                prestadorSedes.usuario_digita = SesionVar.UserName;
                prestadorSedes.estado_sede = 0;
                prestadorSedes.fecha_digita = DateTime.Now;

                var idSede = BusClass.InsertarSedePrestadorFis(prestadorSedes);

                if (idSede != 0)
                {
                    TotalSedes = BusClass.TraerPrestadorSedesId(Model.id_prestador);
                }

                if (TotalSedes.Count() > 0)
                {
                    var j = 0;
                    foreach (var item in TotalSedes)
                    {
                        j++;
                        result += "<tr>";
                        result += "<td>" + item.id_sede + "</td>";
                        result += "<td>" + item.codigo_habilitacion_sede + "</td>";
                        result += "<td>" + item.codigo_otra_sede + "</td>";
                        result += "<td>" + item.regional_sede + "</td>";
                        result += "<td>" + item.departamento_sede + "</td>";
                        result += "<td>" + item.ciudad_sede + "</td>";
                        result += "<td>" + item.direccion_sede + "</td>";
                        result += "<td>" + item.contacto_telefonico_sede + "</td>";
                        result += "<td>" + item.correo_electronico_sede + "</td>";
                        result += "<td>" + item.fecha_digita + "</td>";
                        result += "<td>" + item.nombreDiigtaSedes + "</td>";
                        result += "<td class='text-center'>" +
                            "<a class='btn btn-xs button_Asalud_Rechazar' onclick='EliminarSede(" + item.id_sede + ")'><i class='glyphicon glyphicon-trash'></i>&nbsp; Eliminar</a>" +
                            "</td>";
                        result += "</tr>";
                    }
                }

                Session["ListadoSedes"] = TotalSedes;

                if (idSede != 0)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE LA SEDE.";
                    return Json(new { success = true, message = mensaje, id = Model.id_prestador, tabla = result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE LA SEDE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CrearPrestadorGuardar(Models.FIS.FIS_RIPS Model, List<HttpPostedFileBase> archivos)
        {
            String mensaje = "";
            fis_rips_prestadores prestador = new fis_rips_prestadores();
            List<management_fisPrestadores_sedesResult> TotalSedes = new List<management_fisPrestadores_sedesResult>();
            var rta = 0;

            try
            {
                if (Model.id_prestador == 0 || Model.id_prestador == null)
                {
                    var existeNit = BusClass.getprestadoresNit(Convert.ToString(Model.nit)) != null ? throw new Exception("ESTE PRESTADOR CON ESTE NIT YA EXISTE") : 0;
                }

                TotalSedes = (List<management_fisPrestadores_sedesResult>)Session["ListadoSedes"];

                var tieneSedes = Model.tiene_mas_sedes;
                if (tieneSedes == 1)
                {
                    if (TotalSedes.Count() == 0)
                    {
                        mensaje = "DEBE INGRESAR AL MENOS UNA SEDE";
                        return Json(new { success = true, message = mensaje, id = Model.id_prestador }, JsonRequestBehavior.AllowGet);
                    }
                }

                prestador.nit = Model.nit;
                prestador.codigo_verfificacion = Model.codigo_verfificacion;
                prestador.codigo_SAP = Model.codigo_SAP;
                prestador.codigo_habilitacion = Model.codigo_habilitacion;
                prestador.codigo_sede = Model.codigo_sede;
                prestador.razon_social = Model.razon_social;
                prestador.tipoPrestador = Model.tipoPrestador;
                prestador.ciudad_proveedor = Model.ciudad_proveedor;
                prestador.departamento_proveedor = Model.departamento_proveedor;
                prestador.regional = Model.regional;
                prestador.direccion = Model.direccion;
                prestador.contacto_telefonico = Model.contacto_telefonico;
                prestador.correo_electronico = Model.correo_electronico;
                prestador.estado = 1;
                prestador.tiene_mas_sedes = Model.tiene_mas_sedes;
                prestador.fecha_digita = DateTime.Now;
                prestador.usuario_digita = SesionVar.UserName;

                var gestion = 0;
                var seActualiza = 0;

                if (Model.id_prestador == 0 || Model.id_prestador == null)
                {
                    Model.id_prestador = BusClass.InsertarPrestadorFis(prestador);
                    gestion = (int)Model.id_prestador;
                }
                else
                {
                    seActualiza = 1;
                    prestador.id_prestador = (int)Model.id_prestador;
                    gestion = BusClass.ActualizarDatosPrestador(prestador);
                }

                if (gestion != 0)
                {
                    if (tieneSedes == 1)
                    {
                        var actualizaSedes = BusClass.ActualizarEstadoPrestadoresFIS(Model.id_prestador);
                    }

                    if (archivos != null)
                    {
                        if (archivos.Count() > 0)
                        {
                            for (var a = 0; a < archivos.Count(); a++)
                            {
                                HttpPostedFileBase file = archivos[a];
                                if (file != null)
                                {
                                    if (file.ContentLength > 0)
                                    {
                                        var guardadoArchivo = GuardarArchivo(Model.id_prestador, file);
                                    }
                                }
                            }
                        }
                    }


                    Ref_ips_cuentas presta = new Ref_ips_cuentas
                    {
                        Nombre = Model.razon_social,
                        Nit = Convert.ToString(Model.nit),
                        id_ref_ciudades = Model.ciudad_proveedor,
                        cod_sap_prestador = Convert.ToString(Model.codigo_SAP)
                    };

                    var insertaPrestador = BusClass.InsertarPrestadorIpsCuentas(presta);

                    if (seActualiza != 0)
                    {
                        rta = 2;
                    }
                    else
                    {
                        rta = 1;
                    }

                    mensaje = "SE INGRESÓ CORRECTAMENTE EL PRESTADOR";

                    return Json(new { success = true, mensaje = mensaje, rta = rta, id = Model.id_prestador }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DEL PRESTADOR";
                    return Json(new { success = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DEL PRESTADOR: " + error;
                return Json(new { success = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        private string GuardarArchivo(int? id_prestador, HttpPostedFileBase file)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {
                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPrestadoresFis"];
                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "Fis_prestadores";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "Fis_prestadores_pruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + id_prestador);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                fis_rips_prestadores_archivos OBJ = new fis_rips_prestadores_archivos();

                OBJ.id_prestador = id_prestador;
                OBJ.nombre_archivo = nombre;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.extension = file.ContentType;
                OBJ.fecha_digita = DateTime.Now;
                OBJ.usuario_digita = SesionVar.UserName;

                var respuesta = BusClass.GuardarArchivosPrestador(OBJ);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                strError = error;
            }

            return strError;
        }

        public JsonResult EliminarSede(int? idSede, int? id_prestador)
        {
            var mensaje = "";
            List<management_fisPrestadores_sedesResult> TotalSedes = new List<management_fisPrestadores_sedesResult>();
            string result = "";
            var rta = 0;

            try
            {
                var eliminar = BusClass.EliminarSedePrestadores(idSede);
                if (eliminar != 0)
                {
                    mensaje = "LA SEDE SE ELIMINÓ CORRECTAMENTE";
                    rta = 1;

                    TotalSedes = BusClass.TraerPrestadorSedesId(id_prestador);

                    if (TotalSedes.Count() > 0)
                    {
                        var j = 0;
                        foreach (var item in TotalSedes)
                        {
                            j++;
                            result += "<tr>";
                            result += "<td>" + item.id_sede + "</td>";
                            result += "<td>" + item.codigo_habilitacion_sede + "</td>";
                            result += "<td>" + item.regional_sede + "</td>";
                            result += "<td>" + item.departamento_sede + "</td>";
                            result += "<td>" + item.ciudad_sede + "</td>";
                            result += "<td>" + item.direccion_sede + "</td>";
                            result += "<td>" + item.contacto_telefonico_sede + "</td>";
                            result += "<td>" + item.correo_electronico_sede + "</td>";
                            result += "<td>" + item.fecha_digita + "</td>";
                            result += "<td>" + item.nombreDiigtaSedes + "</td>";
                            result += "<td class='text-center'>" +
                                "<a class='btn btn-xs button_Asalud_Rechazar' onclick='EliminarSede(" + item.id_sede + ")'><i class='glyphicon glyphicon-trash'></i>&nbsp; Eliminar</a>" +
                                "</td>";
                            result += "</tr>";
                        }
                    }

                    Session["ListadoSedes"] = TotalSedes;

                }
                else
                {
                    mensaje = "ERROR EN LA ELIMINACIÓN DE LA SEDE";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ELIMINACIÓN DE LA SEDE: " + error;
            }

            return Json(new { mensaje = mensaje, tabla = result, rta = rta });
        }

        public ActionResult TableroPrestadores(string nitBusqueda, string sapBusqueda, int? rta)
        {
            List<management_fisPrestadores_tableroControlResult> listado = new List<management_fisPrestadores_tableroControlResult>();
            List<management_fisPrestadores_tableroControl_detalladoResult> listadoDetallado = new List<management_fisPrestadores_tableroControl_detalladoResult>();
            var conteo = 0;
            var mensaje = "";

            try
            {

                if (rta != null)
                {
                    if (rta == 1)
                    {
                        mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE EL PRESTADOR. </div>";
                    }
                    else if (rta == 2)
                    {
                        mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE ACTUALIZÓ CORRECTAMENTE EL PRESTADOR. </div>";
                    }
                    else if (rta == 3)
                    {
                        mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE EL PRESTADOR. </div>";
                    }
                    else
                    {
                        mensaje = "<div class='alert alert-danger' role='alert'>ERROR EN LA GESTIÓN</div>";
                    }
                }

                listado = BusClass.TableroControlPrestadores(nitBusqueda, sapBusqueda);
                listadoDetallado = BusClass.TableroControlPrestadoresDetallado(nitBusqueda, sapBusqueda);

                conteo = listado.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.conteo = conteo;
            ViewBag.msg = mensaje;

            Session["listadoPrestadoresDetallado"] = listadoDetallado;

            return View();
        }

        public JsonResult BuscarNitPrestador()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    term = term.ToUpper();

                    List<fis_rips_prestadores> pres = new List<fis_rips_prestadores>();
                    pres = BusClass.ConsultaPrestadoresFis(term, ref MsgRes);

                    var lista = (from reg in pres
                                 select new
                                 {
                                     id = reg.nit,
                                     label = reg.nit,
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

        public JsonResult BuscarSAPPrestador()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    term = term.ToUpper();

                    List<fis_rips_prestadores> pres = new List<fis_rips_prestadores>();
                    pres = BusClass.ConsultaPrestadoresFisSAP(Convert.ToDecimal(term), ref MsgRes);

                    var lista = (from reg in pres
                                 select new
                                 {
                                     id = reg.codigo_SAP,
                                     label = reg.codigo_SAP,
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

        public PartialViewResult MostrarRepositorioArchivos(int? idPrestador, int? tipoEntrada)
        {
            List<management_fisPrestadores_tableroControl_archivosResult> listadoArchivos = new List<management_fisPrestadores_tableroControl_archivosResult>();
            var conteoArchivos = 0;

            try
            {
                listadoArchivos = BusClass.TraerArchivosPrestadorId(idPrestador);
                conteoArchivos = listadoArchivos.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoArchivos = listadoArchivos;
            ViewBag.conteoArchivos = conteoArchivos;
            ViewBag.tipoEntrada = tipoEntrada;
            ViewBag.id_prestador = idPrestador;
            ViewBag.rol = SesionVar.ROL;

            return PartialView();
        }

        public void VerArchivoPrestador(int? idArchivo)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

            fis_rips_prestadores_archivos obj = new fis_rips_prestadores_archivos();
            try
            {
                obj = BusClass.ArchivoPrestadorId(idArchivo);
                if (obj == null)
                {
                    Response.Write("<script language=javascript>alert('El archivo ya no se encuentra en ruta');</script>");
                    return;
                }

                if (!string.IsNullOrEmpty(obj.ruta))
                {
                    var fileName = Path.GetFileName(obj.ruta);
                    var extension = Path.GetExtension(obj.ruta);

                    Response.Clear();
                    Response.ContentType = extension;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.TransmitFile(obj.ruta);
                    Response.Flush();
                }
                else
                {
                    Response.Write("<script language=javascript>alert('No existe el archivo');</script>");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Response.Write("<script language=javascript>alert('Error al mostrar el archivo');</script>");
            }
        }

        public JsonResult GuardarArchivosNuevos(int? id_prestador, List<HttpPostedFileBase> archivosNuevos)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                if (archivosNuevos != null)
                {
                    if (archivosNuevos.Count() > 0)
                    {
                        for (var i = 0; i < archivosNuevos.Count(); i++)
                        {
                            HttpPostedFileBase archivo = archivosNuevos[i];
                            var guardadoArchivo = GuardarArchivo(id_prestador, archivo);

                            if (guardadoArchivo != "")
                            {
                                throw new Exception("ERROR AL INGRESAR EL ARCHIVO: " + archivo.FileName);
                            }
                        }

                        mensaje = "ARCHIVOS GUARDADOS CORRECTAMENTE";
                    }
                    else
                    {
                        mensaje = "ADJUNTE ARCHIVOS PARA GUARDAR";
                    }
                }
                else
                {
                    mensaje = "ADJUNTE ARCHIVOS PARA GUARDAR";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE ARCHIVOS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult EliminarArchivo(int? idArchivo)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var eliminar = BusClass.EliminarArchivoPrestador(idArchivo);
                if (eliminar != 0)
                {
                    mensaje = "ARCHIVO ELIMINADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR EL ARCHIVO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR EL ARCHIVO: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });

        }

        public ActionResult CrearNegociacion(int? idPrestador, int? idContrato)
        {
            ViewBag.idPrestador = idPrestador;
            ViewBag.idContrato = idContrato;

            ViewBag.tigas = BusClass.TraerTigasDetallados();
            management_fisPrestadores_contratosResult contrato = new management_fisPrestadores_contratosResult();
            List<management_fisPrestadores_contratos_tigasResult> tigas = new List<management_fisPrestadores_contratos_tigasResult>();
            fis_rips_prestadores_contratos existeContrato = new fis_rips_prestadores_contratos();

            try
            {
                existeContrato = BusClass.TraerDatosContratoPrestadorIdPrestador(idPrestador);

                ViewBag.existeNegociacion = existeContrato != null ? 1 : 0;

                if (idContrato != 0 && idContrato != null)
                {
                    contrato = BusClass.TraerDatosContrato(idContrato);
                    tigas = BusClass.TraerDatosContratoTigas(idContrato);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaTigas = tigas;
            ViewBag.conteoTigas = tigas.Count();

            Session["ListadoTigas"] = tigas;

            return View(contrato);
        }

        public string ValidarExistenciaNumContrato(string numContrato)
        {
            var resultado = "";
            List<fis_rips_prestadores_contratos> lista = new List<fis_rips_prestadores_contratos>();
            try
            {
                lista = BusClass.ConsultaContratoPrestadoresFis(numContrato, ref MsgRes);

                if (lista.Count() > 0)
                {
                    resultado = "ESTE NÚMERO DE CONTRATO YA EXISTE";
                }
                else
                {
                    resultado = "";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                resultado = "ERROR EN LA CONSULTA";
            }
            return resultado;
        }

        public JsonResult GuardarNegociacion(Models.FIS.Contratos Model, List<HttpPostedFileBase> archivos)
        {
            var mensaje = "";
            var rta = 0;
            fis_rips_prestadores_contratos contrato = new fis_rips_prestadores_contratos();
            List<management_fisPrestadores_contratos_tigasResult> TotalTigas = new List<management_fisPrestadores_contratos_tigasResult>();

            try
            {
                TotalTigas = BusClass.TraerDatosContratoTigas(Model.id_contrato);

                if (TotalTigas.Count() == 0)
                {
                    mensaje = "DEBE INGRESAR AL MENOS UN TIGA";
                    return Json(new { mensaje = mensaje, rta = rta });
                }

                contrato.id_prestador = Model.id_prestador;
                contrato.num_contrato = Model.num_contrato;
                contrato.fecha_suscripcion = Model.fecha_suscripcion;
                contrato.fecha_inicial = Model.fecha_inicial;
                contrato.fecha_final = Model.fecha_final;
                contrato.objeto_contrato = Model.objeto_contrato;
                contrato.id_adm_contrato = Model.id_adm_contrato;
                contrato.nom_adm_contrato = Model.nom_adm_contrato;
                contrato.id_apoyo_transaccional = Model.id_apoyo_transaccional;
                contrato.nom_apoyo_transaccional = Model.nom_apoyo_transaccional;
                contrato.id_interventor = Model.id_interventor;
                contrato.nom_interventor = Model.nom_interventor;
                contrato.valor_contrato = Model.valor_contrato;
                contrato.manual_tarifario = Model.manual_tarifario;
                contrato.neogociacion = Model.neogociacion;

                contrato.grupo_compras = Model.grupo_compras;
                contrato.centro_logistico = Model.centro_logistico;
                contrato.posicion_contrato = Model.posicion_contrato;
                contrato.contrato_operativo = Model.contrato_operativo;

                contrato.estado = 1;
                contrato.fecha_digita = DateTime.Now;
                contrato.usuario_digita = SesionVar.UserName;

                if (Model.id_contrato == 0 || Model.id_contrato == null)
                {
                    Model.id_contrato = BusClass.GuardarContratoPrestador(contrato);
                }
                else
                {
                    contrato.id_contrato = (int)Model.id_contrato;
                    BusClass.ActualizarDatosContratoPrestador(contrato);
                }

                var actualizarTigas = 0;

                if (Model.id_contrato != 0)
                {
                    actualizarTigas = BusClass.ActualizarEstadoTigasContrato(Model.id_contrato);

                    if (archivos != null)
                    {
                        if (archivos.Count() > 0)
                        {
                            var ActualizarTarifas = BusClass.ActualizarEstadoTarifas(Model.id_contrato);

                            fis_rips_prestadores_contratos_tarifas tarifa = new fis_rips_prestadores_contratos_tarifas();
                            tarifa.id_contrato = Model.id_contrato;
                            tarifa.estado = 1;
                            tarifa.fecha_digita = DateTime.Now;
                            tarifa.usuario_digita = SesionVar.UserName;

                            for (var a = 0; a < archivos.Count(); a++)
                            {
                                HttpPostedFileBase file = archivos[a];
                                if (file != null)
                                {
                                    if (file.ContentLength > 0)
                                    {
                                        var guardadoArchivo = Model.CargueTarifas(file, tarifa, contrato);

                                        if (Model.rtaIngresoTarifas == 2)
                                        {
                                            throw new Exception(guardadoArchivo);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (actualizarTigas != 0)
                    {
                        mensaje = "CONTRATO INGRESADO CORRECTAMENTE";
                        rta = 3;
                    }
                    else
                    {
                        mensaje = "ERROR AL INGRESAR EL CONTRATO";
                    }
                }
                else
                {
                    mensaje = "ERROR AL INGRESAR EL CONTRATO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL INGRESAR EL CONTRATO: " + error;
            }
            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult GuardarTigasContratos(Models.FIS.Contratos Model)
        {
            String mensaje = "";
            fis_rips_prestadores_contrato_tigas tigas = new fis_rips_prestadores_contrato_tigas();
            List<management_fisPrestadores_contratos_tigasResult> TotalTigas = new List<management_fisPrestadores_contratos_tigasResult>();
            fis_rips_prestadores_contratos contrato = new fis_rips_prestadores_contratos();

            int i = 0;
            int ultestado = 0;
            string result = "";

            try
            {
                contrato.id_prestador = Model.id_prestador;
                contrato.num_contrato = Model.num_contrato;
                contrato.objeto_contrato = Model.objeto_contrato;
                contrato.id_adm_contrato = Model.id_adm_contrato;
                contrato.nom_adm_contrato = Model.nom_adm_contrato;
                contrato.nom_apoyo_transaccional = Model.nom_apoyo_transaccional;
                contrato.id_interventor = Model.id_interventor;
                contrato.nom_interventor = Model.nom_interventor;
                contrato.valor_contrato = Model.valor_contrato;
                contrato.manual_tarifario = Model.manual_tarifario;
                contrato.neogociacion = Model.neogociacion;
                contrato.estado = 0;
                contrato.fecha_digita = DateTime.Now;
                contrato.usuario_digita = SesionVar.UserName;

                if (Model.id_contrato == 0 || Model.id_contrato == null)
                {
                    Model.id_contrato = BusClass.GuardarContratoPrestador(contrato);
                }
                else
                {
                    contrato.fecha_suscripcion = Model.fecha_suscripcion;
                    contrato.fecha_inicial = Model.fecha_inicial;
                    contrato.fecha_final = Model.fecha_final;
                    contrato.id_contrato = (int)Model.id_contrato;
                    BusClass.ActualizarDatosContratoPrestador(contrato);
                }

                tigas.id_contrato = Model.id_contrato;
                tigas.id_tiga = Model.id_tiga;
                tigas.fecha_digita = DateTime.Now;
                tigas.usuario_digita = SesionVar.UserName;
                tigas.estado = 0;

                var existeTiga = BusClass.ExisteTigaContrato(Model.id_tiga, Model.id_contrato);

                var idTiga = 0;

                if (existeTiga == 0)
                {
                    idTiga = BusClass.GuardarTigaContratoPrestador(tigas);
                }

                TotalTigas = BusClass.TraerDatosContratoTigas(Model.id_contrato);
                if (TotalTigas.Count() > 0)
                {
                    var j = 0;
                    foreach (var item in TotalTigas)
                    {
                        j++;

                        result += "<tr>";
                        result += "<td>" + item.id_registro + "</td>";
                        result += "<td>" + item.codigoTiga + "</td>";
                        result += "<td>" + item.descripcionTiga + "</td>";
                        result += "<td>" + item.nombreDigita + "</td>";
                        result += "<td>" + item.fecha_digita + "</td>";
                        result += "<td class='text-center'>" +
                            "<a class='btn btn-xs button_Asalud_Rechazar' onclick='EliminarTiga(" + item.id_registro + ")'><i class='glyphicon glyphicon-trash'></i>&nbsp; Eliminar</a>" +
                            "</td>";
                        result += "</tr>";
                    }

                }

                Session["ListadoTigas"] = TotalTigas;


                if (idTiga != 0)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE EL TIGA.";
                    return Json(new { success = true, message = mensaje, id = Model.id_contrato, tabla = result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (existeTiga != 0)
                    {
                        mensaje = "ESTE TIGA YA EXISTE EN ESTA NEGOCIACIÓN";
                    }
                    else
                    {
                        mensaje = "ERROR EN EL INGRESO DE EL TIGA.";
                    }
                    return Json(new { success = false, message = mensaje, id = Model.id_contrato, tabla = result }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE EL TIGA: " + error;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EliminarTiga(int? idTiga, int? id_contrato)
        {
            var mensaje = "";
            List<management_fisPrestadores_contratos_tigasResult> TotalTigas = new List<management_fisPrestadores_contratos_tigasResult>();
            string result = "";
            var rta = 0;

            try
            {
                var eliminar = BusClass.EliminarTigaContratosPrestadores(idTiga);
                if (eliminar != 0)
                {
                    mensaje = "EL TIGA SE ELIMINÓ CORRECTAMENTE";
                    rta = 1;

                    TotalTigas = BusClass.TraerDatosContratoTigas(id_contrato);

                    if (TotalTigas.Count() > 0)
                    {
                        var j = 0;
                        foreach (var item in TotalTigas)
                        {
                            j++;
                            result += "<tr>";
                            result += "<td>" + item.id_registro + "</td>";
                            result += "<td>" + item.codigoTiga + "</td>";
                            result += "<td>" + item.descripcionTiga + "</td>";
                            result += "<td>" + item.nombreDigita + "</td>";
                            result += "<td>" + item.fecha_digita + "</td>";
                            result += "<td class='text-center'>" +
                                "<a class='btn btn-xs button_Asalud_Rechazar' onclick='EliminarTiga(" + item.id_registro + ")'><i class='glyphicon glyphicon-trash'></i>&nbsp; Eliminar</a>" +
                                "</td>";
                            result += "</tr>";
                        }

                    }

                    Session["ListadoTigas"] = TotalTigas;

                }
                else
                {
                    mensaje = "ERROR EN LA ELIMINACIÓN DE LA SEDE";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ELIMINACIÓN DE LA SEDE: " + error;
            }

            return Json(new { mensaje = mensaje, tabla = result, rta = rta });
        }

        public ActionResult TableroContratos(int? rta)
        {
            List<management_fisPrestadores_contratos_tableroControlResult> listado = new List<management_fisPrestadores_contratos_tableroControlResult>();
            List<management_fisPrestadores_contratos_tableroControlResult> listadoActivos = new List<management_fisPrestadores_contratos_tableroControlResult>();
            List<management_fisPrestadores_contratos_tableroControlResult> listadoInactivos = new List<management_fisPrestadores_contratos_tableroControlResult>();
            var conteo = 0;
            var mensaje = "";

            try
            {
                if (rta != null)
                {
                    if (rta == 1)
                    {
                        mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE EL PRESTADOR. </div>";
                    }
                    else if (rta == 2)
                    {
                        mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE ACTUALIZÓ CORRECTAMENTE EL PRESTADOR. </div>";
                    }
                    else if (rta == 3)
                    {
                        mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE EL PRESTADOR. </div>";
                    }
                    else
                    {
                        mensaje = "<div class='alert alert-danger' role='alert'>ERROR EN LA GESTIÓN</div>";
                    }
                }


                listado = BusClass.DatosTableroControlContratos();

                listadoActivos = listado.Where(x => x.estado == 1).ToList();
                listadoInactivos = listado.Where(x => x.estado == 0).ToList();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.conteoActivos = listadoActivos.Count();
            ViewBag.conteoInactivos = listadoInactivos.Count();

            ViewBag.listadoActivos = listadoActivos;
            ViewBag.listadoInactivos = listadoInactivos;

            ViewBag.rol = SesionVar.ROL;

            Session["ListadoContratos"] = listado;
            ViewBag.msg = mensaje;

            return View();
        }

        public ActionResult TableroContratosPorVencer(int? rta)
        {
            List<management_fisPrestadores_contratos_tableroControl_porVencerResult> listado = new List<management_fisPrestadores_contratos_tableroControl_porVencerResult>();
            var conteo = 0;
            var mensaje = "";

            try
            {
                if (rta != null)
                {
                    if (rta == 1)
                    {
                        mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE EL PRESTADOR. </div>";
                    }
                    else if (rta == 2)
                    {
                        mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE ACTUALIZÓ CORRECTAMENTE EL PRESTADOR. </div>";
                    }
                    else if (rta == 3)
                    {
                        mensaje = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE EL PRESTADOR. </div>";
                    }
                    else
                    {
                        mensaje = "<div class='alert alert-danger' role='alert'>ERROR EN LA GESTIÓN</div>";
                    }
                }


                listado = BusClass.DatosTableroControlContratosxVencer();
                conteo = listado.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.conteo = conteo;
            ViewBag.listado = listado;
            ViewBag.rol = SesionVar.ROL;

            Session["ListadoContratosXVencer"] = listado;
            ViewBag.msg = mensaje;

            return View();
        }

        public PartialViewResult MostrarNegociacionesContrato(int? idPrestador, int? idContrato)
        {
            List<management_fisPrestadores_contratos_negociacionesResult> listaContratos = new List<management_fisPrestadores_contratos_negociacionesResult>();
            ViewBag.idPrestador = idPrestador;
            ViewBag.idContrato = idContrato;

            listaContratos = BusClass.TraerNegociacionesContrato(idContrato);

            ViewBag.listadoNegociaciones = listaContratos;
            ViewBag.conteoNegociaciones = listaContratos.Count();

            return PartialView();
        }

        public PartialViewResult MostrarNegociacionesContratoDetale(int? idMasivo, int? idContrato)
        {
            List<management_fisPrestadores_contratos_negociaciones_detalleResult> listado = new List<management_fisPrestadores_contratos_negociaciones_detalleResult>();
            ViewBag.idMasivo = idMasivo;
            ViewBag.idContrato = idContrato;

            listado = BusClass.TraerNegociacionesContratoDetalle(idMasivo);

            ViewBag.listado = listado;
            ViewBag.conteoDetalle = listado.Count();

            Session["ListadoNegociacionesDetalle"] = listado;

            return PartialView();
        }

        public JsonResult EliminarNegociacion(int? idMasivo, int? idContrato)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var eliminar = BusClass.EliminarNegociacionContrato(idMasivo);
                if (eliminar != 0)
                {
                    log_fisPrestador_contrato_eliminar log = new log_fisPrestador_contrato_eliminar();
                    log.id_contrato = idContrato;
                    log.id_negociacion = idMasivo;
                    log.usuario_digita = SesionVar.UserName;
                    log.fecha_digita = DateTime.Now;

                    var insertaLog = BusClass.InsertarLogEliminarNegociacion(log);

                    mensaje = "NEGOCIACIÓN ELIMINADA CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR LA NEGOCIACIÓN";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR LA NEGOCIACIÓN: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });

        }

        public JsonResult GuardarTarifasNuevas(int? idContrato, List<HttpPostedFileBase> archivos)
        {
            Models.FIS.Contratos Modelo = new Models.FIS.Contratos();
            fis_rips_prestadores_contratos contrato = new fis_rips_prestadores_contratos();

            var mensaje = "";
            var rta = 0;

            try
            {
                contrato = BusClass.TraerDatosContratoPrestadorId(idContrato);

                if (archivos != null)
                {
                    if (archivos.Count() > 0)
                    {
                        fis_rips_prestadores_contratos_tarifas tarifa = new fis_rips_prestadores_contratos_tarifas();
                        tarifa.id_contrato = idContrato;
                        tarifa.estado = 1;
                        tarifa.fecha_digita = DateTime.Now;
                        tarifa.usuario_digita = SesionVar.UserName;

                        for (var a = 0; a < archivos.Count(); a++)
                        {
                            HttpPostedFileBase file = archivos[a];
                            if (file != null)
                            {
                                if (file.ContentLength > 0)
                                {
                                    var guardadoArchivo = Modelo.CargueTarifas(file, tarifa, contrato);

                                    var ActualizarTarifas = BusClass.ActualizarEstadoTarifasDiferenteId(idContrato, Modelo.id_registro);

                                    if (Modelo.rtaIngresoTarifas == 2)
                                    {
                                        throw new Exception(guardadoArchivo);
                                    }
                                }
                            }
                        }

                        mensaje = "CONTRATO INGRESADO CORRECTAMENTE";
                        rta = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE NUEVAS TARIFAS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult CreacionCUPS(int? idCups)
        {
            fis_rips_cups cups = new fis_rips_cups();
            try
            {
                if (idCups != null && idCups != 0)
                {
                    cups = BusClass.TraerCupsId(idCups);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.id_cups = idCups;

            return View(cups);
        }

        public JsonResult GuardarCups(int? id_cups, string codigo_cups, string descripcion, string manual, int? estado)
        {
            var mensaje = "";
            var rta = 0;
            fis_rips_cups cups = new fis_rips_cups();
            try
            {
                cups.codigo_cups = codigo_cups;
                cups.descripcion = descripcion;
                cups.manual = manual;
                cups.estado = estado;
                cups.fecha_digita = DateTime.Now;
                cups.usuario_digita = SesionVar.UserName;

                var gestion = 0;
                var actualiza = 0;

                if (id_cups == null || id_cups == 0)
                {
                    var existeCups = BusClass.TraerCupsCodigo(codigo_cups);
                    if (existeCups != null)
                    {
                        throw new Exception("CUPS YA EXISTE");
                    }

                    gestion = BusClass.CrearCups(cups);
                }
                else
                {
                    cups.id_cups = (int)id_cups;
                    gestion = BusClass.ActualizarCupsFis(cups);
                    actualiza = 1;
                }

                if (gestion != 0)
                {
                    if (actualiza == 0)
                    {
                        mensaje = "CUPS INGRESADA CORRECTAMENTE";
                    }
                    else
                    {
                        mensaje = "CUPS ACTUALIZADA CORRECTAMENTE";
                    }

                    rta = 1;
                }
                else
                {
                    if (actualiza == 0)
                    {
                        mensaje = "ERROR AL INGRESAR LA CUPS";
                    }
                    else
                    {
                        mensaje = "ERROR AL ACTUALIZAR LA CUPS";
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL GESTIONAR LA CUPS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });

        }

        public ActionResult TableroCups()
        {

            List<management_fis_cupsResult> Listado = new List<management_fis_cupsResult>();
            var conteo = 0;

            try
            {
                Listado = BusClass.TraerCUpsTablero();
                conteo = Listado.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = Listado;
            ViewBag.conteo = conteo;

            Session["ListadoCUPSFIS"] = Listado;

            return View();
        }

        public JsonResult EliminarCups(int? idCups)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var elimina = BusClass.EliminarCupsFis(idCups);
                if (elimina != 0)
                {
                    mensaje = "CUPS ELIMINADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR EL CUPS";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR EL CUPS: " + error;

            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult CargarMasivos()
        {
            ViewBag.tipoConsulta = BusClass.ListadoTipoConsultaJson();
            return View();
        }

        public string ValidarExistenciaCUV(string codCuv)
        {
            var resultado = "";
            List<fis_rips_cargue_LoteConsultas> lista = new List<fis_rips_cargue_LoteConsultas>();
            try
            {
                lista = BusClass.ConsultaCUVFIS(codCuv, ref MsgRes);

                resultado = lista.Count() > 0 ? resultado = "ESTE CUV YA EXISTE" : resultado = "";

                //if (lista.Count() > 0)
                //{
                //    resultado = "ESTE CUV YA EXISTE";
                //}
                //else
                //{
                //    resultado = "";
                //}

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                resultado = "ERROR EN LA CONSULTA";
            }
            return resultado;
        }

        public class ByteArrayHttpPostedFile : HttpPostedFileBase
        {
            private readonly byte[] bytes;

            public ByteArrayHttpPostedFile(byte[] bytes, string fileName)
            {
                this.bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
                this.FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            }

            public override int ContentLength => bytes.Length;

            public override string ContentType => "application/json";

            public override string FileName { get; }

            public override Stream InputStream => new MemoryStream(bytes);
        }

        //public JsonResult GuardarConsultas(string codigo_cuv, HttpPostedFileBase archivo, int? tipo, int? idDetalle, int? idCargue, string listadoId, string listadoCuv, int? existeCuv)
        //{
        //    //1 = masivo, 2 = facturación individual, 3 = facturacion masivo

        //    var mensaje = "";
        //    var rta = 0;

        //    Models.FIS.CUPS Model = new Models.FIS.CUPS();

        //    try
        //    {
        //        List<fis_rips_cargue_LoteConsultas> listaCargue = new List<fis_rips_cargue_LoteConsultas>();
        //        fis_rips_cargue_LoteConsultas cargue = new fis_rips_cargue_LoteConsultas();

        //        List<int> idList = new List<int>();
        //        string[] listado = !string.IsNullOrEmpty(listadoId) ? listadoId.Split(',') : new string[0];

        //        List<string> cuvList = new List<string>();
        //        string[] listCuv = !string.IsNullOrEmpty(listadoCuv) ? listadoCuv.Split(',') : new string[0];

        //        //1 = masivo, 2 = facturación individual, 3 = facturacion masivo
        //        if (tipo == 3)
        //        {
        //            foreach (var item in listado)
        //            {
        //                int id = Convert.ToInt32(item);
        //                idList.Add(id);
        //            }

        //            foreach (var item in listCuv)
        //            {
        //                string cuvIndividual = Convert.ToString(item);
        //                cuvList.Add(cuvIndividual);
        //            }

        //            if (idList.Count() > 0)
        //            {
        //                var j = 0;
        //                foreach (var item in idList)
        //                {
        //                    fis_rips_cargue_LoteConsultas cargueIndividual = new fis_rips_cargue_LoteConsultas();
        //                    cargueIndividual.estado = 1;
        //                    cargueIndividual.fecha_digita = DateTime.Now;
        //                    cargueIndividual.usuario_digita = SesionVar.UserName;
        //                    cargueIndividual.codigo_cuv = cuvList[j];
        //                    //1 = masivo, 2 = facturación individual, 3 = facturacion masivo

        //                    cargueIndividual.tipo = tipo;
        //                    cargueIndividual.id_factura = item;

        //                    if (existeCuv != 1)
        //                    {
        //                        List<management_fisFacturas_buscarExistenciaCuvResult> listaCuv = new List<management_fisFacturas_buscarExistenciaCuvResult>();
        //                        listaCuv = BusClass.ListadoCuvExistentesCodigo(cuvList[j]);
        //                        if (listaCuv.Count() > 0)
        //                        {
        //                            mensaje = "EL CUV " + cuvList[j] + " YA EXISTE.";
        //                            throw new Exception(mensaje);
        //                        }
        //                    }

        //                    listaCargue.Add(cargueIndividual);

        //                    j++;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            cargue.estado = 1;
        //            cargue.fecha_digita = DateTime.Now;
        //            cargue.usuario_digita = SesionVar.UserName;
        //            cargue.codigo_cuv = codigo_cuv;

        //            if (existeCuv != 1)
        //            {
        //                List<management_fisFacturas_buscarExistenciaCuvResult> listaCuv = new List<management_fisFacturas_buscarExistenciaCuvResult>();
        //                listaCuv = BusClass.ListadoCuvExistentesCodigo(codigo_cuv);
        //                if (listaCuv.Count() > 0)
        //                {
        //                    mensaje = "EL CUV " + codigo_cuv + " YA EXISTE.";
        //                    throw new Exception(mensaje);
        //                }
        //            }

        //            cargue.tipo = tipo;
        //            cargue.id_factura = idDetalle;

        //        }
        //        using (ZipArchive zip = new ZipArchive(archivo.InputStream, ZipArchiveMode.Read))
        //        {
        //            foreach (ZipArchiveEntry entrada in zip.Entries)
        //            {
        //                if (Path.GetExtension(entrada.FullName).Equals(".json", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    // Es un archivo JSON
        //                    using (MemoryStream ms = new MemoryStream())
        //                    {
        //                        entrada.Open().CopyTo(ms);
        //                        ms.Seek(0, SeekOrigin.Begin);

        //                        byte[] contenidoArchivo = ms.ToArray();

        //                        // Crear un objeto HttpPostedFileBase utilizando la clase personalizada
        //                        HttpPostedFileBase fileDatos = new ByteArrayHttpPostedFile(contenidoArchivo, entrada.FullName);

        //                        if (fileDatos != null)
        //                        {
        //                            if (fileDatos.ContentLength > 0)
        //                            {
        //                                var nombreArchivoMayusculas = fileDatos.FileName.ToUpper().Trim();
        //                                var id_tipo = DevolverTipo(nombreArchivoMayusculas);

        //                                if (id_tipo != 0)
        //                                {
        //                                    switch (id_tipo)
        //                                    {
        //                                        case 1:
        //                                            //Tipo consulta

        //                                            if (tipo == 3)
        //                                            {
        //                                                foreach (var cargues in listaCargue)
        //                                                {
        //                                                    mensaje = Model.InsertarTipoConsulta(fileDatos, cargues);
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mensaje = Model.InsertarTipoConsulta(fileDatos, cargue);
        //                                            }
        //                                            break;

        //                                        case 2:
        //                                            //Tipo hospitalización


        //                                            if (tipo == 3)
        //                                            {
        //                                                foreach (var cargues in listaCargue)
        //                                                {
        //                                                    mensaje = Model.InsertarTipo_hospitalizacion(fileDatos, cargues);
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mensaje = Model.InsertarTipo_hospitalizacion(fileDatos, cargue);
        //                                            }

        //                                            break;
        //                                        case 3:
        //                                            //Tipo medicamentos
        //                                            if (tipo == 3)
        //                                            {
        //                                                foreach (var cargues in listaCargue)
        //                                                {
        //                                                    mensaje = Model.InsertarTipo_medicamentos(fileDatos, cargues);
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mensaje = Model.InsertarTipo_medicamentos(fileDatos, cargue);
        //                                            }

        //                                            break;
        //                                        case 4:
        //                                            //Tipo otrosServicios
        //                                            if (tipo == 3)
        //                                            {
        //                                                foreach (var cargues in listaCargue)
        //                                                {
        //                                                    mensaje = Model.InsertarTipo_otrosServicios(fileDatos, cargues);
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mensaje = Model.InsertarTipo_otrosServicios(fileDatos, cargue);
        //                                            }
        //                                            break;

        //                                        case 5:
        //                                            //Tipo procedimientos
        //                                            if (tipo == 3)
        //                                            {
        //                                                foreach (var cargues in listaCargue)
        //                                                {
        //                                                    mensaje = Model.InsertarTipo_procedimientos(fileDatos, cargues);
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mensaje = Model.InsertarTipo_procedimientos(fileDatos, cargue);
        //                                            }

        //                                            break;
        //                                        case 6:
        //                                            //Tipo recienNacidos
        //                                            if (tipo == 3)
        //                                            {
        //                                                foreach (var cargues in listaCargue)
        //                                                {
        //                                                    mensaje = Model.InsertarTipo_recienNacidos(fileDatos, cargues);

        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mensaje = Model.InsertarTipo_recienNacidos(fileDatos, cargue);
        //                                            }

        //                                            break;
        //                                        case 7:
        //                                            //Tipo transaccion
        //                                            if (tipo == 3)
        //                                            {
        //                                                foreach (var cargues in listaCargue)
        //                                                {
        //                                                    mensaje = Model.InsertarTipo_transaccion(fileDatos, cargues);

        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mensaje = Model.InsertarTipo_transaccion(fileDatos, cargue);
        //                                            }

        //                                            break;
        //                                        case 8:
        //                                            //Tipo urgencias
        //                                            if (tipo == 3)
        //                                            {
        //                                                foreach (var cargues in listaCargue)
        //                                                {
        //                                                    mensaje = Model.InsertarTipo_urgencias(fileDatos, cargues);

        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mensaje = Model.InsertarTipo_urgencias(fileDatos, cargue);
        //                                            }

        //                                            break;
        //                                        default:
        //                                            //Tipo usuarios
        //                                            if (tipo == 3)
        //                                            {
        //                                                foreach (var cargues in listaCargue)
        //                                                {
        //                                                    mensaje = Model.InsertarTipo_usuarios(fileDatos, cargues);
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mensaje = Model.InsertarTipo_usuarios(fileDatos, cargue);
        //                                            }

        //                                            break;
        //                                    }
        //                                }

        //                                else
        //                                {
        //                                    mensaje = "EL ARCHIVO: " + fileDatos.FileName + " NO CONTIENE UN NOMBRE PARA NINGÚN TIPO DE CONSULTA";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                mensaje = "EL ARCHIVO: " + entrada.FullName + " ESTÁ VACIO O CORRUPTO.";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            mensaje = "EL ARCHIVO: " + entrada.FullName + " ESTÁ VACIO O CORRUPTO.";
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    // No es un archivo JSON
        //                    mensaje = "EL ARCHIVO: " + entrada.FullName + " NO ES DE TIPO JSON";
        //                    throw new Exception(mensaje);
        //                }
        //            }

        //            if (mensaje.Contains("Datos del archivo procesados correctamente"))
        //            {
        //                if (tipo == 2 || tipo == 4)
        //                {
        //                    mensaje = GuardarAceptadaFactura(idDetalle, idCargue);
        //                    if (mensaje.Contains("ERROR EN EL INGRESO"))
        //                    {
        //                        throw new Exception(mensaje);
        //                    }

        //                    if (existeCuv == 2)
        //                    {
        //                        var actualizaCuv = BusClass.ActualizarCuvFacturaId(idDetalle, codigo_cuv);
        //                    }
        //                }

        //                else if (tipo == 3)
        //                {
        //                    if (existeCuv == 2)
        //                    {
        //                        foreach (var item in listaCargue)
        //                        {
        //                            var actualizaCuv = BusClass.ActualizarCuvFacturaId(item.id_factura, item.codigo_cuv);
        //                        }
        //                    }

        //                    ecop_firma_digital_sami firma = new ecop_firma_digital_sami();
        //                    firma = BusClass.traerDatosFirma(SesionVar.IDUser);

        //                    if (firma == null)
        //                    {
        //                        throw new Exception("El usuario no se encuentra con firma digital en SAMI. Por favor solicitarla");
        //                    }
        //                    else
        //                    {
        //                        mensaje = AceptacionFacturasMasivamente(idList);

        //                        if (mensaje.Contains("ocurrido un error"))
        //                        {
        //                            throw new Exception(mensaje);
        //                        }
        //                    }
        //                }

        //                rta = 1;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        mensaje = error;
        //        if (idDetalle != null && idDetalle != 0)
        //        {
        //            var elimina = BusClass.EliminarDatosRipsFisFacturasIdFactura(idDetalle);
        //        }
        //    }

        //    return Json(new { mensaje = mensaje, rta = rta });

        //}

        public JsonResult GuardarConsultas(string codigo_cuv, HttpPostedFileBase archivo, int? tipo, int? idDetalle, int? idCargue, string listadoId, string listadoCuv, int? existeCuv,
            string codPrestador, string listadoCodPrestador)
        {
            //1 = masivo, 2 = facturación individual, 3 = facturacion masivo

            var mensaje = "";
            var rta = 0;

            Models.FIS.CUPS Model = new Models.FIS.CUPS();

            try
            {
                List<fis_rips_cargue_LoteConsultas> listaCargue = new List<fis_rips_cargue_LoteConsultas>();
                fis_rips_cargue_LoteConsultas cargue = new fis_rips_cargue_LoteConsultas();

                List<int> idList = new List<int>();
                string[] listado = !string.IsNullOrEmpty(listadoId) ? listadoId.Split(',') : new string[0];

                List<string> cuvList = new List<string>();
                string[] listCuv = !string.IsNullOrEmpty(listadoCuv) ? listadoCuv.Split(',') : new string[0];

                List<string> codList = new List<string>();
                string[] listCodPrestador = !string.IsNullOrEmpty(listadoCodPrestador) ? listadoCodPrestador.Split(',') : new string[0];

                //1 = masivo, 2 = facturación individual, 3 = facturacion masivo
                if (tipo == 3)
                {
                    foreach (var item in listado)
                    {
                        int id = Convert.ToInt32(item);
                        idList.Add(id);
                    }

                    foreach (var item in listCuv)
                    {
                        string cuvIndividual = Convert.ToString(item);
                        cuvList.Add(cuvIndividual);
                    }

                    foreach (var item in listCodPrestador)
                    {
                        string codIndividual = Convert.ToString(item);
                        codList.Add(codIndividual);
                    }

                    if (idList.Count() > 0)
                    {
                        var j = 0;
                        foreach (var item in idList)
                        {
                            fis_rips_cargue_LoteConsultas cargueIndividual = new fis_rips_cargue_LoteConsultas();
                            cargueIndividual.estado = 1;
                            cargueIndividual.fecha_digita = DateTime.Now;
                            cargueIndividual.usuario_digita = SesionVar.UserName;
                            cargueIndividual.codigo_cuv = !string.IsNullOrEmpty(listadoCuv) ? cuvList[j] : codigo_cuv;
                            cargueIndividual.codPrestador_factura = codPrestador != null ? codPrestador : "";

                            //1 = masivo, 2 = facturación individual, 3 = facturacion masivo

                            cargueIndividual.tipo = tipo;
                            cargueIndividual.id_factura = item;

                            if (existeCuv != 1)
                            {
                                List<management_fisFacturas_buscarExistenciaCuvResult> listaCuv = new List<management_fisFacturas_buscarExistenciaCuvResult>();
                                listaCuv = BusClass.ListadoCuvExistentesCodigo(!string.IsNullOrEmpty(listadoCuv) ? cuvList[j] : codigo_cuv);
                                if (listaCuv.Count() > 0)
                                {
                                    mensaje = "EL CUV " + (!string.IsNullOrEmpty(listadoCuv) ? cuvList[j] : codigo_cuv) + " YA EXISTE.";
                                    throw new Exception(mensaje);
                                }
                            }

                            listaCargue.Add(cargueIndividual);

                            j++;
                        }
                    }
                }
                else
                {
                    cargue.estado = 1;
                    cargue.fecha_digita = DateTime.Now;
                    cargue.usuario_digita = SesionVar.UserName;
                    cargue.codigo_cuv = codigo_cuv;
                    cargue.codPrestador_factura = codPrestador != null ? codPrestador : "";

                    if (existeCuv != 1)
                    {
                        List<management_fisFacturas_buscarExistenciaCuvResult> listaCuv = new List<management_fisFacturas_buscarExistenciaCuvResult>();
                        listaCuv = BusClass.ListadoCuvExistentesCodigo(codigo_cuv);
                        if (listaCuv.Count() > 0)
                        {
                            mensaje = "EL CUV " + codigo_cuv + " YA EXISTE.";
                            throw new Exception(mensaje);
                        }
                    }

                    cargue.tipo = tipo;
                    cargue.id_factura = idDetalle;

                }
                using (ZipArchive zip = new ZipArchive(archivo.InputStream, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entrada in zip.Entries)
                    {
                        if (Path.GetExtension(entrada.FullName).Equals(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            // Es un archivo JSON
                            using (MemoryStream ms = new MemoryStream())
                            {
                                entrada.Open().CopyTo(ms);
                                ms.Seek(0, SeekOrigin.Begin);

                                byte[] contenidoArchivo = ms.ToArray();

                                // Crear un objeto HttpPostedFileBase utilizando la clase personalizada
                                HttpPostedFileBase fileDatos = new ByteArrayHttpPostedFile(contenidoArchivo, entrada.FullName);

                                if (fileDatos != null)
                                {
                                    if (fileDatos.ContentLength > 0)
                                    {
                                        var nombreArchivoMayusculas = fileDatos.FileName.ToUpper().Trim();
                                        var id_tipo = DevolverTipo(nombreArchivoMayusculas);

                                        if (id_tipo != 0)
                                        {
                                            switch (id_tipo)
                                            {
                                                case 1:
                                                    //Tipo consulta

                                                    if (tipo == 3)
                                                    {
                                                        var i = 0;
                                                        foreach (var cargues in listaCargue)
                                                        {
                                                            mensaje = Model.InsertarTipoConsulta(fileDatos, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensaje = Model.InsertarTipoConsulta(fileDatos, cargue, codPrestador);
                                                    }
                                                    break;

                                                case 2:
                                                    //Tipo hospitalización


                                                    if (tipo == 3)
                                                    {
                                                        var i = 0;

                                                        foreach (var cargues in listaCargue)
                                                        {
                                                            mensaje = Model.InsertarTipo_hospitalizacion(fileDatos, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensaje = Model.InsertarTipo_hospitalizacion(fileDatos, cargue, codPrestador);
                                                    }

                                                    break;
                                                case 3:
                                                    //Tipo medicamentos
                                                    if (tipo == 3)
                                                    {
                                                        var i = 0;

                                                        foreach (var cargues in listaCargue)
                                                        {
                                                            mensaje = Model.InsertarTipo_medicamentos(fileDatos, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensaje = Model.InsertarTipo_medicamentos(fileDatos, cargue, codPrestador);
                                                    }

                                                    break;
                                                case 4:
                                                    //Tipo otrosServicios
                                                    if (tipo == 3)
                                                    {
                                                        var i = 0;

                                                        foreach (var cargues in listaCargue)
                                                        {
                                                            mensaje = Model.InsertarTipo_otrosServicios(fileDatos, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensaje = Model.InsertarTipo_otrosServicios(fileDatos, cargue, codPrestador);
                                                    }
                                                    break;

                                                case 5:
                                                    //Tipo procedimientos
                                                    if (tipo == 3)
                                                    {
                                                        var i = 0;

                                                        foreach (var cargues in listaCargue)
                                                        {
                                                            mensaje = Model.InsertarTipo_procedimientos(fileDatos, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensaje = Model.InsertarTipo_procedimientos(fileDatos, cargue, codPrestador);
                                                    }

                                                    break;
                                                case 6:
                                                    //Tipo recienNacidos
                                                    if (tipo == 3)
                                                    {
                                                        var i = 0;

                                                        foreach (var cargues in listaCargue)
                                                        {
                                                            mensaje = Model.InsertarTipo_recienNacidos(fileDatos, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensaje = Model.InsertarTipo_recienNacidos(fileDatos, cargue, codPrestador);
                                                    }

                                                    break;
                                                case 7:
                                                    //Tipo transaccion
                                                    if (tipo == 3)
                                                    {
                                                        var i = 0;

                                                        foreach (var cargues in listaCargue)
                                                        {
                                                            mensaje = Model.InsertarTipo_transaccion(fileDatos, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensaje = Model.InsertarTipo_transaccion(fileDatos, cargue, codPrestador);
                                                    }

                                                    break;
                                                case 8:
                                                    //Tipo urgencias
                                                    if (tipo == 3)
                                                    {
                                                        var i = 0;

                                                        foreach (var cargues in listaCargue)
                                                        {
                                                            mensaje = Model.InsertarTipo_urgencias(fileDatos, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensaje = Model.InsertarTipo_urgencias(fileDatos, cargue, codPrestador);
                                                    }

                                                    break;
                                                default:
                                                    //Tipo usuarios
                                                    if (tipo == 3)
                                                    {
                                                        var i = 0;

                                                        foreach (var cargues in listaCargue)
                                                        {
                                                            mensaje = Model.InsertarTipo_usuarios(fileDatos, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensaje = Model.InsertarTipo_usuarios(fileDatos, cargue, codPrestador);
                                                    }

                                                    break;
                                            }

                                            if (mensaje.Contains("Error"))
                                            {
                                                throw new Exception(mensaje);
                                            }
                                            else
                                            {
                                                mensaje = "DATOS INGRESADOS CORRECTAMENTE";
                                            }

                                        }

                                        else
                                        {
                                            mensaje = "EL ARCHIVO: " + fileDatos.FileName + " NO CONTIENE UN NOMBRE PARA NINGÚN TIPO DE CONSULTA";
                                        }
                                    }
                                    else
                                    {
                                        mensaje = "EL ARCHIVO: " + entrada.FullName + " ESTÁ VACIO O CORRUPTO.";
                                    }
                                }
                                else
                                {
                                    mensaje = "EL ARCHIVO: " + entrada.FullName + " ESTÁ VACIO O CORRUPTO.";
                                }
                            }
                        }
                        else
                        {
                            // No es un archivo JSON
                            mensaje = "EL ARCHIVO: " + entrada.FullName + " NO ES DE TIPO JSON";
                            throw new Exception(mensaje);
                        }
                    }

                    if (mensaje.Contains("Datos del archivo procesados correctamente") || mensaje.Contains("DATOS INGRESADOS CORRECTAMENTE"))
                    {
                        if (tipo == 2 || tipo == 4)
                        {
                            mensaje = GuardarAceptadaFactura(idDetalle, idCargue);
                            if (mensaje.Contains("ERROR EN EL INGRESO"))
                            {
                                throw new Exception(mensaje);
                            }

                            //if (existeCuv == 2)
                            //{
                            //    var actualizaCuv = BusClass.ActualizarCuvFacturaId(idDetalle, codigo_cuv);
                            //}
                        }

                        else if (tipo == 3)
                        {
                            if (existeCuv == 2)
                            {
                                //foreach (var item in listaCargue)
                                //{
                                //    var actualizaCuv = BusClass.ActualizarCuvFacturaId(item.id_factura, item.codigo_cuv);
                                //}
                            }

                            ecop_firma_digital_sami firma = new ecop_firma_digital_sami();
                            firma = BusClass.traerDatosFirma(SesionVar.IDUser);

                            if (firma == null)
                            {
                                throw new Exception("El usuario no se encuentra con firma digital en SAMI. Por favor solicitarla");
                            }
                            else
                            {
                                mensaje = AceptacionFacturasMasivamente(idList);

                                if (mensaje.Contains("ocurrido un error"))
                                {
                                    throw new Exception(mensaje);
                                }
                            }
                        }

                        if (tipo == 3)
                        {
                            var k = 0;
                            foreach (var item in idList)
                            {
                                var cuv = !string.IsNullOrEmpty(listadoCuv) ? cuvList[k] : codigo_cuv;
                                var idFactura = Convert.ToInt32(item);
                                var insertaRegistros = BusClass.GuardarRegistroTarifasRipsFacturas(cuv, idFactura, SesionVar.UserName);

                                if (insertaRegistros == 0)
                                {
                                    throw new Exception("NO SE INGRESARON LOS REGISTROS DE RIPS");
                                }
                                k++;
                                rta = 1;
                            }
                        }
                        else
                        {
                            var insertaRegistros = BusClass.GuardarRegistroTarifasRipsFacturas(codigo_cuv, idDetalle, SesionVar.UserName);
                            if (insertaRegistros == 0)
                            {
                                throw new Exception("NO SE INGRESARON LOS REGISTROS DE RIPS");
                            }
                            rta = 1;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = error;
                if (idDetalle != null && idDetalle != 0)
                {
                    var elimina = BusClass.EliminarDatosRipsFisFacturasIdFactura(idDetalle);
                }
            }

            return Json(new { mensaje = mensaje, rta = rta });

        }

        public JsonResult GuardarConsultasFis(string codigo_cuv, HttpPostedFileBase archivo, int? tipo, int? idDetalle, int? idCargue, string listadoId, string listadoCuv, int? existeCuv,
            string codPrestador, string listadoCodPrestador)
        {

            Models.FIS.CUPSUNICOS Model = new Models.FIS.CUPSUNICOS();

            var mensaje = "";
            var rta = 0;
            List<fis_rips_cargue_LoteConsultas> listaCargue = new List<fis_rips_cargue_LoteConsultas>();
            fis_rips_cargue_LoteConsultas cargue = new fis_rips_cargue_LoteConsultas();

            if (archivo != null && archivo.ContentLength > 0)
            {
                try
                {
                    List<int> idList = new List<int>();
                    string[] listado = !string.IsNullOrEmpty(listadoId) ? listadoId.Split(',') : new string[0];

                    List<string> cuvList = new List<string>();
                    string[] listCuv = !string.IsNullOrEmpty(listadoCuv) ? listadoCuv.Split(',') : new string[0];

                    List<string> codList = new List<string>();
                    string[] listCodPrestador = !string.IsNullOrEmpty(listadoCodPrestador) ? listadoCodPrestador.Split(',') : new string[0];

                    //1 = masivo, 2 = facturación individual, 3 = facturacion masivo
                    if (tipo == 3)
                    {
                        foreach (var item in listado)
                        {
                            int id = Convert.ToInt32(item);
                            idList.Add(id);
                        }

                        foreach (var item in listCuv)
                        {
                            string cuvIndividual = Convert.ToString(item);
                            cuvList.Add(cuvIndividual);
                        }

                        foreach (var item in listCodPrestador)
                        {
                            string codIndividual = Convert.ToString(item);
                            codList.Add(codIndividual);
                        }

                        if (idList.Count() > 0)
                        {
                            var j = 0;
                            foreach (var item in idList)
                            {
                                fis_rips_cargue_LoteConsultas cargueIndividual = new fis_rips_cargue_LoteConsultas();
                                cargueIndividual.estado = 1;
                                cargueIndividual.fecha_digita = DateTime.Now;
                                cargueIndividual.usuario_digita = SesionVar.UserName;
                                cargueIndividual.codigo_cuv = !string.IsNullOrEmpty(listadoCuv) ? cuvList[j] : codigo_cuv;
                                cargueIndividual.codPrestador_factura = codPrestador != null ? codPrestador : "";

                                //1 = masivo, 2 = facturación individual, 3 = facturacion masivo

                                cargueIndividual.tipo = tipo;
                                cargueIndividual.id_factura = item;

                                //if (existeCuv != 1)
                                //{
                                //    List<management_fisFacturas_buscarExistenciaCuvResult> listaCuv = new List<management_fisFacturas_buscarExistenciaCuvResult>();
                                //    listaCuv = BusClass.ListadoCuvExistentesCodigo(!string.IsNullOrEmpty(listadoCuv) ? cuvList[j] : codigo_cuv);
                                //    if (listaCuv.Count() > 0)
                                //    {
                                //        mensaje = "EL CUV " + (!string.IsNullOrEmpty(listadoCuv) ? cuvList[j] : codigo_cuv) + " YA EXISTE.";
                                //        throw new Exception(mensaje);
                                //    }
                                //}

                                listaCargue.Add(cargueIndividual);

                                var existenDetalles = BusClass.HayDetallesIdFactura(item);

                                if (existenDetalles == 1)
                                {
                                    var EliminaDetalles = BusClass.EliminarDetallesIdFactura(item);
                                }


                                j++;
                            }
                        }
                    }
                    else
                    {
                        cargue.estado = 1;
                        cargue.fecha_digita = DateTime.Now;
                        cargue.usuario_digita = SesionVar.UserName;
                        cargue.codigo_cuv = codigo_cuv;
                        cargue.codPrestador_factura = codPrestador != null ? codPrestador : "";

                        //if (existeCuv != 1)
                        //{
                        //    List<management_fisFacturas_buscarExistenciaCuvResult> listaCuv = new List<management_fisFacturas_buscarExistenciaCuvResult>();
                        //    listaCuv = BusClass.ListadoCuvExistentesCodigo(codigo_cuv);
                        //    if (listaCuv.Count() > 0)
                        //    {
                        //        mensaje = "EL CUV " + codigo_cuv + " YA EXISTE.";
                        //        throw new Exception(mensaje);
                        //    }
                        //}

                        cargue.tipo = tipo;
                        cargue.id_factura = idDetalle;

                        var existenDetalles = BusClass.HayDetallesIdFactura(idDetalle);

                        if (existenDetalles == 1)
                        {
                            var EliminaDetalles = BusClass.EliminarDetallesIdFactura(idDetalle);
                        }
                    }

                    // Leer el archivo en un string
                    if (Path.GetExtension(archivo.FileName).Equals(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (var reader = new StreamReader(archivo.InputStream))
                        {
                            string jsonContent = reader.ReadToEnd();

                            // Parsear el string JSON a un JObject
                            JObject jsonObject = JObject.Parse(jsonContent);

                            if (jsonObject == null)
                            {
                                throw new Exception("JSON viene vacio");
                            }

                            var mensajeRips = "";
                            BusClass.EliminarTodoCargueFIS(idDetalle);

                            if (tipo == 3)
                            {
                                var i = 0;
                                foreach (var cargues in listaCargue)
                                {

                                    var idLote = BusClass.FisRipsInsercionLote(cargues);

                                    if (idLote == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        mensajeRips = InsercionDatosJson(jsonObject, cargues, (!string.IsNullOrEmpty(listadoCodPrestador) ? codList[i] : codPrestador));
                                    }

                                    i++;
                                }
                            }
                            else
                            {
                                var idLote = BusClass.FisRipsInsercionLote(cargue);
                                if (idLote != 0)
                                {
                                    mensajeRips = InsercionDatosJson(jsonObject, cargue, codPrestador);
                                }
                            }

                            if (mensajeRips == "")
                            {
                                if (tipo == 2 || tipo == 4)
                                {
                                    mensaje = GuardarAceptadaFactura(idDetalle, idCargue);
                                    if (mensaje.Contains("ERROR"))
                                    {
                                        throw new Exception(mensaje);
                                    }

                                    //if (existeCuv == 2)
                                    //{
                                    //    var actualizaCuv = BusClass.ActualizarCuvFacturaId(idDetalle, codigo_cuv);
                                    //}
                                }

                                else if (tipo == 3)
                                {
                                    if (existeCuv == 2)
                                    {
                                        //foreach (var item in listaCargue)
                                        //{
                                        //    var actualizaCuv = BusClass.ActualizarCuvFacturaId(item.id_factura, item.codigo_cuv);
                                        //}
                                    }

                                    ecop_firma_digital_sami firma = new ecop_firma_digital_sami();
                                    firma = BusClass.traerDatosFirma(SesionVar.IDUser);

                                    if (firma == null)
                                    {
                                        throw new Exception("El usuario no se encuentra con firma digital en SAMI. Por favor solicitarla");
                                    }
                                    else
                                    {
                                        mensaje = AceptacionFacturasMasivamente(idList);

                                        if (mensaje.Contains("ocurrido un error"))
                                        {
                                            throw new Exception(mensaje);
                                        }
                                    }
                                }

                                if (tipo == 3)
                                {
                                    var k = 0;
                                    foreach (var item in idList)
                                    {
                                        var cuv = !string.IsNullOrEmpty(listadoCuv) ? cuvList[k] : codigo_cuv;
                                        var idFactura = Convert.ToInt32(item);
                                        var insertaRegistros = BusClass.GuardarRegistroTarifasRipsFacturas(cuv, idFactura, SesionVar.UserName);

                                        if (insertaRegistros == 0)
                                        {
                                            throw new Exception("NO SE INGRESARON LOS REGISTROS DE RIPS");
                                        }
                                        k++;
                                        rta = 1;
                                    }
                                }
                                else
                                {
                                    var insertaRegistros = BusClass.GuardarRegistroTarifasRipsFacturas(codigo_cuv, idDetalle, SesionVar.UserName);
                                    if (insertaRegistros == 0)
                                    {
                                        throw new Exception("NO SE INGRESARON LOS REGISTROS DE RIPS");
                                    }
                                    rta = 1;
                                }
                            }
                            else
                            {
                                throw new Exception(mensajeRips);
                            }
                        }
                    }

                    else
                    {
                        // No es un archivo JSON
                        mensaje = "EL ARCHIVO: " + archivo.FileName + " NO ES DE TIPO JSON";
                        throw new Exception(mensaje);
                    }
                    // Puedes realizar operaciones adicionales con los datos extraídos

                    return Json(new { rta = 1, mensaje = "Archivo procesado correctamente." });
                }
                catch (Exception ex)
                {
                    // Manejo de errores

                    if (tipo == 3)
                    {
                        foreach (var cargues in listaCargue)
                        {
                            BusClass.EliminarTodoElCargueFisIdFactura(cargues.id_factura);
                        }
                    }
                    else
                    {
                        BusClass.EliminarTodoElCargueFisIdFactura(cargue.id_factura);
                    }

                    return Json(new { rta = 2, mensaje = ex.Message });
                }
            }
            else
            {
                return Json(new { rta = 2, mensaje = "No se ha seleccionado un archivo o el archivo está vacío." });
            }
        }

        public string InsercionDatosJson(JObject DatosTransaccion, fis_rips_cargue_LoteConsultas lote, string codPrestador)
        {
            var retorno = "";
            Models.FIS.CUPSUNICOS Model = new Models.FIS.CUPSUNICOS();
            var mensaje = "";
            fis_rips_cargue_transaccion transa = new fis_rips_cargue_transaccion();
            transa.id_lote = lote.id_cargue;
            transa.id_factura = lote.id_factura;
            management_fis_traerNumfactura_idResult numFactura = BusClass.TraerNumFactura_idAf(lote.id_factura);
            var filaServicios = 0;

            try
            {
                var idTrasanccion = 0;

                if (!DatosTransaccion.ContainsKey("numDocumentoIdObligado"))
                {
                    // Si tiene la estructura "rips": { "numDocumentoIdObligado": ... }, lanzar excepción
                    if (DatosTransaccion.ContainsKey("rips") && DatosTransaccion["rips"] is JObject ripsObject &&
                        ripsObject.ContainsKey("numDocumentoIdObligado"))
                    {
                        throw new Exception("Estructura JSON incorrecta: 'numDocumentoIdObligado' está anidado dentro de 'rips'.");
                    }
                    else
                    {
                        throw new Exception("Estructura JSON incorrecta: falta el campo 'numDocumentoIdObligado'.");
                    }
                }

                transa = Model.ValidarEstructuraTransaccion(DatosTransaccion, numFactura.num_factura, codPrestador);
                mensaje = Model.mensajeResultado;
                if (string.IsNullOrEmpty(mensaje))
                {
                    transa.id_lote = lote.id_cargue;
                    transa.id_factura = lote.id_factura;

                    idTrasanccion = BusClass.FisRipsInsercionTransaccion(transa);
                    if (idTrasanccion != 0)
                    {
                        // Acceder al array de "usuarios"
                        JArray usuarios = (JArray)DatosTransaccion["usuarios"];

                        if (usuarios != null)
                        {
                            var i = 0;
                            foreach (var usuario in usuarios)
                            {
                                fis_rips_cargue_usuarios usu = new fis_rips_cargue_usuarios();
                                usu = Model.ValidarEstructuraUsuario((JObject)usuario, codPrestador);

                                mensaje = Model.mensajeResultado;
                                i++;

                                if (string.IsNullOrEmpty(mensaje))
                                {
                                    usu.id_transaccion = idTrasanccion;
                                    usu.id_factura = lote.id_factura;
                                    usu.id_lote = lote.id_cargue;

                                    var insercionUsuario = BusClass.FisRipsInsercionUsuario(usu);

                                    if (insercionUsuario != 0)
                                    {
                                        JObject servicios = (JObject)usuario["servicios"];

                                        if (servicios != null)
                                        {
                                            List<fis_rips_cups> listadoCups = BusClass.TraerListadoCups();

                                            foreach (var tipoServicio in servicios.Properties())
                                            {
                                                var tipoDatoArray = tipoServicio.Name;
                                                JArray registrosServicioArray = (JArray)tipoServicio.Value;

                                                if (registrosServicioArray != null)
                                                {
                                                    foreach (JObject registrosServicio in registrosServicioArray)
                                                    {
                                                        filaServicios++;

                                                        if (tipoDatoArray == "consultas")
                                                        {
                                                            fis_rips_cargue_consulta cons = Model.ValidarEstructuraConsulta((JObject)registrosServicio, codPrestador, listadoCups, filaServicios);
                                                            if (string.IsNullOrEmpty(mensaje))
                                                            {
                                                                if (cons != null)
                                                                {
                                                                    cons.id_lote = lote.id_cargue;
                                                                    cons.id_factura = lote.id_factura;
                                                                    cons.id_transaccion = idTrasanccion;
                                                                    cons.id_usuarios = insercionUsuario;

                                                                    var insertaConsulta = BusClass.FisRipsInsercionConsulta(cons);

                                                                    if (insertaConsulta == 0)
                                                                    {
                                                                        throw new Exception("Error al insertar datos consulta");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception(Model.mensajeResultado);
                                                            }
                                                        }

                                                        else if (tipoDatoArray == "hospitalizacion")
                                                        {
                                                            fis_rips_cargue_hospitalizacion hospi = Model.ValidarEstructuraHospitalizacion((JObject)registrosServicio, codPrestador, filaServicios);
                                                            if (string.IsNullOrEmpty(mensaje))
                                                            {
                                                                if (hospi != null)
                                                                {
                                                                    hospi.id_lote = lote.id_cargue;
                                                                    hospi.id_factura = lote.id_factura;
                                                                    hospi.id_usuarios = insercionUsuario;
                                                                    hospi.id_transaccion = idTrasanccion;

                                                                    var insertaHospitalizacion = BusClass.FisRipsInsercionHospitalizacion(hospi);

                                                                    if (insertaHospitalizacion == 0)
                                                                    {
                                                                        throw new Exception("Error al insertar datos hospitalización");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception(Model.mensajeResultado);
                                                            }
                                                        }
                                                        else if (tipoDatoArray == "procedimientos")
                                                        {
                                                            fis_rips_cargue_procedimientos proce = Model.ValidarEstructuraProcedimiento((JObject)registrosServicio, codPrestador, listadoCups, filaServicios);
                                                            if (string.IsNullOrEmpty(mensaje))
                                                            {
                                                                if (proce != null)
                                                                {
                                                                    proce.id_lote = lote.id_cargue;
                                                                    proce.id_factura = lote.id_factura;
                                                                    proce.id_usuarios = insercionUsuario;
                                                                    proce.id_transaccion = idTrasanccion;

                                                                    var insertaProcedimiento = BusClass.FisRipsInsercionProcedimientos(proce);

                                                                    if (insertaProcedimiento == 0)
                                                                    {
                                                                        throw new Exception("Error al insertar datos procedimiento");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception(Model.mensajeResultado);
                                                            }
                                                        }
                                                        else if (tipoDatoArray == "urgencias")
                                                        {
                                                            fis_rips_cargue_urgencias urge = Model.ValidarEstructuraUrgencias((JObject)registrosServicio, codPrestador, filaServicios);
                                                            if (string.IsNullOrEmpty(mensaje))
                                                            {
                                                                if (urge != null)
                                                                {
                                                                    urge.id_lote = lote.id_cargue;
                                                                    urge.id_factura = lote.id_factura;
                                                                    urge.id_usuarios = insercionUsuario;
                                                                    urge.id_transaccion = idTrasanccion;

                                                                    var insertaUrgencias = BusClass.FisRipsInsercionUrgencias(urge);

                                                                    if (insertaUrgencias == 0)
                                                                    {
                                                                        throw new Exception("Error al insertar datos urgencias");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception(Model.mensajeResultado);
                                                            }
                                                        }
                                                        else if (tipoDatoArray == "recienNacidos")
                                                        {
                                                            fis_rips_cargue_reciennacido recien = Model.ValidarEstructuraRecienNacido((JObject)registrosServicio, codPrestador, filaServicios);
                                                            if (string.IsNullOrEmpty(mensaje))
                                                            {
                                                                if (recien != null)
                                                                {
                                                                    recien.id_lote = lote.id_cargue;
                                                                    recien.id_factura = lote.id_factura;
                                                                    recien.id_usuarios = insercionUsuario;
                                                                    recien.id_transaccion = idTrasanccion;

                                                                    var insertaRecienNacido = BusClass.FisRipsInsercionRecienNacido(recien);

                                                                    if (insertaRecienNacido == 0)
                                                                    {
                                                                        throw new Exception("Error al insertar datos recien nacido");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception(Model.mensajeResultado);

                                                            }
                                                        }
                                                        else if (tipoDatoArray == "medicamentos")
                                                        {
                                                            fis_rips_cargue_medicamentos medi = Model.ValidarEstructuraMedicamento((JObject)registrosServicio, codPrestador, listadoCups, filaServicios);
                                                            if (string.IsNullOrEmpty(mensaje))
                                                            {
                                                                if (medi != null)
                                                                {
                                                                    medi.id_lote = lote.id_cargue;
                                                                    medi.id_factura = lote.id_factura;
                                                                    medi.id_usuarios = insercionUsuario;
                                                                    medi.id_transaccion = idTrasanccion;

                                                                    var insertaMedicamento = BusClass.FisRipsInsercionMedicamento(medi);

                                                                    if (insertaMedicamento == 0)
                                                                    {
                                                                        throw new Exception("Error al insertar datos medicamento");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception(Model.mensajeResultado);
                                                            }
                                                        }
                                                        else if (tipoDatoArray == "otrosServicios")
                                                        {
                                                            fis_rips_cargue_otros_servicios otro = Model.ValidarEstructuraOtroServicio((JObject)registrosServicio, codPrestador, filaServicios);
                                                            if (string.IsNullOrEmpty(mensaje))
                                                            {
                                                                if (otro != null)
                                                                {
                                                                    otro.id_lote = lote.id_cargue;
                                                                    otro.id_factura = lote.id_factura;
                                                                    otro.id_usuarios = insercionUsuario;
                                                                    otro.id_transaccion = idTrasanccion;

                                                                    var insertaOtroServicio = BusClass.FisRipsInsercionOtroServicio(otro);

                                                                    if (insertaOtroServicio == 0)
                                                                    {
                                                                        throw new Exception("Error al insertar datos otro servicio");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception(Model.mensajeResultado);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        management_fis_buscarFechaAtencion_idUsuarioResult BusFechaAten = BusClass.BuscarFechaAtencionUsuario(insercionUsuario, idTrasanccion);

                                        var fechaInicioAtencion = BusFechaAten != null ? BusFechaAten.fechaInicioAtencion : null;

                                        List<management_fis_buscarExietenciaBeneficiarioResult> existeBeneficiario = BusClass.BuscarBeneficiarioEnFis(fechaInicioAtencion, usu.tipoDocumentoIdentificacion, usu.numDocumentoIdentificacion);
                                        if (existeBeneficiario.Count() == 0)
                                        {
                                            var mensajeError = "El documento: " + usu.numDocumentoIdentificacion + " no existe en beneficiarios";
                                            throw new Exception(mensajeError);
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception(mensaje);
                                }

                            }
                        }
                    }
                }
                else
                {
                    throw new Exception(mensaje);
                }

            }

            catch (Exception ex)
            {
                var error = ex.Message;
                retorno = "ERROR EN EL CARGUE: " + error;
            }

            return retorno;
        }

        //public int DevolverTipo(string nombre)
        //{
        //    var retorno = 0;
        //    if (nombre != "")
        //    {
        //        nombre = nombre.ToUpper(); // Convertir el nombre del archivo a mayúsculas
        //        if (nombre.Contains("CONSULTA"))
        //        {
        //            retorno = 1;
        //        }
        //        else if (nombre.Contains("HOSPITALIZACION") || nombre.Contains("HOSPITALIZACIÓN"))
        //        {
        //            retorno = 2;
        //        }
        //        else if (nombre.Contains("MEDICAMENTOS"))
        //        {
        //            retorno = 3;
        //        }
        //        else if (nombre.Contains("OTROS SERVICIOS") || nombre.Contains("OTROSSERVICIOS"))
        //        {
        //            retorno = 4;
        //        }
        //        else if (nombre.Contains("PROCEDIMIENTOS"))
        //        {
        //            retorno = 5;
        //        }
        //        else if (nombre.Contains("RECIEN NACIDO") || nombre.Contains("RECIENNACIDO"))
        //        {
        //            retorno = 6;
        //        }
        //        else if (nombre.Contains("TRANSACCIÓN") || nombre.Contains("TRANSACCION"))
        //        {
        //            retorno = 7;
        //        }
        //        else if (nombre.Contains("URGENCIAS"))
        //        {
        //            retorno = 8;
        //        }
        //        else if (nombre.Contains("USUARIOS"))
        //        {
        //            retorno = 9;
        //        }
        //    }
        //    else
        //    {
        //        retorno = 0;
        //    }

        //    return retorno;
        //}

        public int DevolverTipo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return 0;

            nombre = nombre.ToUpper();

            switch (nombre)
            {
                case var n when n.Contains("CONSULTA"):
                    return 1;
                case var n when n.Contains("HOSPITALIZACION") || n.Contains("HOSPITALIZACIÓN"):
                    return 2;
                case var n when n.Contains("MEDICAMENTOS"):
                    return 3;
                case var n when n.Contains("OTROS SERVICIOS") || n.Contains("OTROSSERVICIOS"):
                    return 4;
                case var n when n.Contains("PROCEDIMIENTOS"):
                    return 5;
                case var n when n.Contains("RECIEN NACIDO") || n.Contains("RECIENNACIDO"):
                    return 6;
                case var n when n.Contains("TRANSACCIÓN") || n.Contains("TRANSACCION"):
                    return 7;
                case var n when n.Contains("URGENCIAS"):
                    return 8;
                case var n when n.Contains("USUARIOS"):
                    return 9;
                default:
                    return 0;
            }
        }

        public string GuardarAceptadaFactura(int? detalle, int? idCargue)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            String mensaje = "";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PrestadoresConnectionString"].ConnectionString;

            var auditor = SesionVar.IDUser;

            var BaseImagen = Model.GetFirmas(auditor);
            if (BaseImagen != null || auditor == 396)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 2,fecha_recepcion_fac = @fecha_recepcion_fac, id_analista_gestiona = @id_analista_gestiona Where id_af = @id";
                        command.Parameters.AddWithValue("@id_analista_gestiona", SesionVar.IDUser);

                        command.Parameters.AddWithValue("@id", detalle);
                        command.Parameters.AddWithValue("@fecha_recepcion_fac", Convert.ToDateTime(DateTime.Now));

                        connection.Open();
                        command.CommandTimeout = 120;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    List<ref_rechazos_Fac> list = new List<ref_rechazos_Fac>();
                    list = Model.Getref_rechazos_Fac(ref MsgRes);

                    ViewBag.List = list;

                    mensaje = "SE INGRESÓ CORRECTAMENTE";
                    return mensaje;

                }
                catch (Exception ex)
                {
                    mensaje = "ERROR EN EL INGRESO.";
                    return mensaje;
                }
            }
            else
            {
                mensaje = "ERROR... Usuario no puede aprobar porque no tiene firma digital en SAMI.";
                return mensaje;
            }
        }

        public string AceptacionFacturasMasivamente(List<int> listadoFacturas)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            var mensaje = "";
            var rta = 0;

            try
            {
                Model.ActualizarFacturaAceptadaAnalista(listadoFacturas, SesionVar.IDUser, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = MsgRes.DescriptionResponse;
                }
                else
                {
                    mensaje = MsgRes.DescriptionResponse;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                if (error.Contains("El usuario no se encuentra con firma digital en SAMI"))
                {
                    mensaje = MsgRes.DescriptionResponse;
                }
                else
                {
                    mensaje = MsgRes.DescriptionResponse;
                }
            }

            return mensaje;
        }

        //public ActionResult TableroControlMasivos(string codigoCuv)
        //{
        //    List<management_fis_cargueRips_consultaResult> listaConsulta = new List<management_fis_cargueRips_consultaResult>();
        //    List<management_fis_cargueRips_hospitalizacionResult> listaHospitalizacion = new List<management_fis_cargueRips_hospitalizacionResult>();
        //    List<management_fis_cargueRips_medicamentosResult> listaMedicamento = new List<management_fis_cargueRips_medicamentosResult>();
        //    List<management_fis_cargueRips_otrosServiciosResult> listaOtroServicio = new List<management_fis_cargueRips_otrosServiciosResult>();
        //    List<management_fis_cargueRips_procedimientosResult> listaProcedimiento = new List<management_fis_cargueRips_procedimientosResult>();
        //    List<management_fis_cargueRips_recienNacidoResult> listaRecienNacido = new List<management_fis_cargueRips_recienNacidoResult>();
        //    List<management_fis_cargueRips_transaccionResult> listaTransaccion = new List<management_fis_cargueRips_transaccionResult>();
        //    List<management_fis_cargueRips_urgenciasResult> listaurgencia = new List<management_fis_cargueRips_urgenciasResult>();
        //    List<management_fis_cargueRips_usuariosResult> listaUsuario = new List<management_fis_cargueRips_usuariosResult>();

        //    try
        //    {
        //        listaConsulta = BusClass.ListadoRipsConsulta(codigoCuv);
        //        listaHospitalizacion = BusClass.ListadoRipsHospitalizacion(codigoCuv);
        //        listaMedicamento = BusClass.ListadoRipsMedicamentos(codigoCuv);
        //        listaOtroServicio = BusClass.ListadoRipsOtrosServicios(codigoCuv);
        //        listaProcedimiento = BusClass.ListadoRipsProcedimientos(codigoCuv);
        //        listaRecienNacido = BusClass.ListadoRipsRecienNacido(codigoCuv);
        //        listaTransaccion = BusClass.ListadoRipsTransaccion(codigoCuv);
        //        listaurgencia = BusClass.ListadoRipsUrgencias(codigoCuv);
        //        listaUsuario = BusClass.ListadoRipsUsuarios(codigoCuv);
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    ViewBag.listaConsulta = listaConsulta;
        //    ViewBag.listaHospitalizacion = listaHospitalizacion;
        //    ViewBag.listaMedicamento = listaMedicamento;
        //    ViewBag.listaOtroServicio = listaOtroServicio;
        //    ViewBag.listaProcedimiento = listaProcedimiento;
        //    ViewBag.listaRecienNacido = listaRecienNacido;
        //    ViewBag.listaTransaccion = listaTransaccion;
        //    ViewBag.listaurgencia = listaurgencia;
        //    ViewBag.listaUsuario = listaUsuario;

        //    Session["listaConsulta"] = listaConsulta;
        //    Session["listaHospitalizacion"] = listaHospitalizacion;
        //    Session["listaMedicamento"] = listaMedicamento;
        //    Session["listaOtroServicio"] = listaOtroServicio;
        //    Session["listaProcedimiento"] = listaProcedimiento;
        //    Session["listaRecienNacido"] = listaRecienNacido;
        //    Session["listaTransaccion"] = listaTransaccion;
        //    Session["listaTransaccion"] = listaTransaccion;
        //    Session["listaurgencia"] = listaurgencia;
        //    Session["listaUsuario"] = listaUsuario;

        //    return View();
        //}

        public void ExcelTableroControlPrestadores()
        {
            List<management_fisPrestadores_tableroControl_detalladoResult> listareporte = new List<management_fisPrestadores_tableroControl_detalladoResult>();
            try
            {
                listareporte = (List<management_fisPrestadores_tableroControl_detalladoResult>)Session["listadoPrestadoresDetallado"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadosPrestadores");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:Y1"].Style.Font.Bold = true;
                Sheet.Cells["A1:Y1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:Y1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:Y1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:Y1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Nit";
                Sheet.Cells["B1"].Value = "Código de verificación";
                Sheet.Cells["C1"].Value = "Código SAP";
                Sheet.Cells["D1"].Value = "Código de habilitación";
                Sheet.Cells["E1"].Value = "Razón social";
                Sheet.Cells["F1"].Value = "Ciudad proveedor";
                Sheet.Cells["G1"].Value = "Departamento proveedor";
                Sheet.Cells["H1"].Value = "Regional";
                Sheet.Cells["I1"].Value = "Dirección";
                Sheet.Cells["J1"].Value = "Contacto telefónico";
                Sheet.Cells["K1"].Value = "Correo electrónico";
                Sheet.Cells["L1"].Value = "Estado";
                Sheet.Cells["M1"].Value = "Tiene más sedes";
                Sheet.Cells["N1"].Value = "Fecha de digitación";
                Sheet.Cells["O1"].Value = "Usuario de digitación";
                Sheet.Cells["P1"].Value = "Código de habilitación de la sede";
                Sheet.Cells["Q1"].Value = "Ciudad de la sede";
                Sheet.Cells["R1"].Value = "Departamento de la sede";
                Sheet.Cells["S1"].Value = "Regional de la sede";
                Sheet.Cells["T1"].Value = "Dirección de la sede";
                Sheet.Cells["U1"].Value = "Contacto telefónico de la sede";
                Sheet.Cells["V1"].Value = "Correo electrónico de la sede";
                Sheet.Cells["W1"].Value = "Estado de la sede";
                Sheet.Cells["X1"].Value = "Fecha de digitación de la sede";
                Sheet.Cells["Y1"].Value = "Nombre de digitación de la sede";

                int row = 2;
                foreach (management_fisPrestadores_tableroControl_detalladoResult item in listareporte)
                {

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.nit;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.codigo_verfificacion;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.codigo_SAP;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.codigo_habilitacion;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.razon_social;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.nombreCiudad;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.nombreDepartamento;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.nombreRegional;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.direccion;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.contacto_telefonico;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.correo_electronico;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.estado;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.tiene_mas_sedes;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.fecha_digita;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.usuario_digita;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.codigo_habilitacion_sede;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.nombreCiudadSede;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.nombreDepartamentoSede;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.nombreRegionalSede;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.direccion_sede;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.contacto_telefonico_sede;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.correo_electronico_sede;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.estado_sede;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.fechaDigita_sede;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.nombreDigitaSede;

                    Sheet.Cells[string.Format("N{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("X{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells["A" + row + ":Y1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:Y"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptTableroControl_prestadores.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public void ExcelTableroControlContratos()
        {
            List<management_fisPrestadores_contratos_tableroControlResult> listareporte = new List<management_fisPrestadores_contratos_tableroControlResult>();
            try
            {
                listareporte = (List<management_fisPrestadores_contratos_tableroControlResult>)Session["ListadoContratos"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadoContratos");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:Q1"].Style.Font.Bold = true;
                Sheet.Cells["A1:Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:Q1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:Q1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:Q1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id contrato";
                Sheet.Cells["B1"].Value = "Nit prestador";
                Sheet.Cells["C1"].Value = "Razón social";
                Sheet.Cells["D1"].Value = "Número contrato";
                Sheet.Cells["E1"].Value = "Fecha suscripción";
                Sheet.Cells["F1"].Value = "Fecha inicial";
                Sheet.Cells["G1"].Value = "Fecha final";
                Sheet.Cells["H1"].Value = "Objeto contrato";
                Sheet.Cells["I1"].Value = "Id administrador contrato";
                Sheet.Cells["J1"].Value = "Nombre administrador contrato";
                Sheet.Cells["K1"].Value = "Nombre apoyo transaccional";
                Sheet.Cells["L1"].Value = "Id interventor";
                Sheet.Cells["M1"].Value = "Nombre interventor";
                Sheet.Cells["N1"].Value = "Valor contrato";
                Sheet.Cells["O1"].Value = "Manual tarifario";
                Sheet.Cells["P1"].Value = "Fecha creación";
                Sheet.Cells["Q1"].Value = "Usuario creación";

                int row = 2;
                foreach (management_fisPrestadores_contratos_tableroControlResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_contrato;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.nitPrestador;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.razonSocial_prestador;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.num_contrato;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_suscripcion;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_inicial;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_final;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.objeto_contrato;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.id_adm_contrato;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.nom_adm_contrato;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.nom_apoyo_transaccional;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.id_interventor;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.nom_interventor;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.valor_contrato;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.manual_tarifario;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.fecha_digita;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.nombreDigita;

                    Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("P{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells["A" + row + ":Q1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:Q"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptTableroControl_Contratos.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public void ExcelTableroControlContratosxVencer()
        {
            List<management_fisPrestadores_contratos_tableroControl_porVencerResult> listareporte = new List<management_fisPrestadores_contratos_tableroControl_porVencerResult>();
            try
            {
                listareporte = (List<management_fisPrestadores_contratos_tableroControl_porVencerResult>)Session["ListadoContratosXVencer"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadoContratosxVencer");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:Q1"].Style.Font.Bold = true;
                Sheet.Cells["A1:Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:Q1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:Q1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:Q1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id contrato";
                Sheet.Cells["B1"].Value = "Nit prestador";
                Sheet.Cells["C1"].Value = "Razón social";
                Sheet.Cells["D1"].Value = "Número contrato";
                Sheet.Cells["E1"].Value = "Fecha suscripción";
                Sheet.Cells["F1"].Value = "Fecha inicial";
                Sheet.Cells["G1"].Value = "Fecha final";
                Sheet.Cells["H1"].Value = "Objeto contrato";
                Sheet.Cells["I1"].Value = "Id administrador contrato";
                Sheet.Cells["J1"].Value = "Nombre administrador contrato";
                Sheet.Cells["K1"].Value = "Nombre apoyo transaccional";
                Sheet.Cells["L1"].Value = "Id interventor";
                Sheet.Cells["M1"].Value = "Nombre interventor";
                Sheet.Cells["N1"].Value = "Valor contrato";
                Sheet.Cells["O1"].Value = "Manual tarifario";
                Sheet.Cells["P1"].Value = "Fecha creación";
                Sheet.Cells["Q1"].Value = "Usuario creación";

                int row = 2;
                foreach (management_fisPrestadores_contratos_tableroControl_porVencerResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_contrato;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.nitPrestador;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.razonSocial_prestador;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.num_contrato;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_suscripcion;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_inicial;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_final;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.objeto_contrato;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.id_adm_contrato;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.nom_adm_contrato;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.nom_apoyo_transaccional;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.id_interventor;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.nom_interventor;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.valor_contrato;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.manual_tarifario;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.fecha_digita;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.nombreDigita;

                    Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("P{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells["A" + row + ":Q1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:Q"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptTableroControl_Contratos_XVencer.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public void ExcelTableroControlCUPS()
        {
            List<management_fis_cupsResult> listareporte = new List<management_fis_cupsResult>();
            try
            {
                listareporte = (List<management_fis_cupsResult>)Session["ListadoCUPSFIS"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadoCUPS");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:F1"].Style.Font.Bold = true;
                Sheet.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:F1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:F1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id CUPS";
                Sheet.Cells["B1"].Value = "Código CUPS";
                Sheet.Cells["C1"].Value = "Descripción CUPS";
                Sheet.Cells["D1"].Value = "Manual";
                Sheet.Cells["E1"].Value = "Estado";
                //Sheet.Cells["F1"].Value = "Fecha creación";
                Sheet.Cells["F1"].Value = "Usuario creación";

                int row = 2;
                foreach (management_fis_cupsResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_cups;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.codigo_cups;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.descripcion;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.manual;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.estado;
                    //Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_digita;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.nombreDigita;

                    row++;
                }

                //Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                Sheet.Cells["A" + row + ":F" + row].Style.Font.Name = "Century Gothic";
                Sheet.Cells["A:F"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptTableroControl_CUPS.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public void ExcelTableroControlCarguesRips()
        {
            List<management_fis_cargueRips_consultaResult> listaConsulta = new List<management_fis_cargueRips_consultaResult>();
            List<management_fis_cargueRips_hospitalizacionResult> listaHospitalizacion = new List<management_fis_cargueRips_hospitalizacionResult>();
            List<management_fis_cargueRips_medicamentosResult> listaMedicamento = new List<management_fis_cargueRips_medicamentosResult>();
            List<management_fis_cargueRips_otrosServiciosResult> listaOtroServicio = new List<management_fis_cargueRips_otrosServiciosResult>();
            List<management_fis_cargueRips_procedimientosResult> listaProcedimiento = new List<management_fis_cargueRips_procedimientosResult>();
            List<management_fis_cargueRips_recienNacidoResult> listaRecienNacido = new List<management_fis_cargueRips_recienNacidoResult>();
            List<management_fis_cargueRips_transaccionResult> listaTransaccion = new List<management_fis_cargueRips_transaccionResult>();
            List<management_fis_cargueRips_urgenciasResult> listaurgencia = new List<management_fis_cargueRips_urgenciasResult>();
            List<management_fis_cargueRips_usuariosResult> listaUsuario = new List<management_fis_cargueRips_usuariosResult>();
            int row = 0;

            try
            {
                ExcelPackage Ep = new ExcelPackage();

                listaConsulta = (List<management_fis_cargueRips_consultaResult>)Session["listaConsulta"];
                listaHospitalizacion = (List<management_fis_cargueRips_hospitalizacionResult>)Session["listaHospitalizacion"];
                listaMedicamento = (List<management_fis_cargueRips_medicamentosResult>)Session["listaMedicamento"];
                listaOtroServicio = (List<management_fis_cargueRips_otrosServiciosResult>)Session["listaOtroServicio"];
                listaProcedimiento = (List<management_fis_cargueRips_procedimientosResult>)Session["listaProcedimiento"];
                listaRecienNacido = (List<management_fis_cargueRips_recienNacidoResult>)Session["listaRecienNacido"];
                listaTransaccion = (List<management_fis_cargueRips_transaccionResult>)Session["listaTransaccion"];
                listaurgencia = (List<management_fis_cargueRips_urgenciasResult>)Session["listaurgencia"];
                listaUsuario = (List<management_fis_cargueRips_usuariosResult>)Session["listaUsuario"];

                //Tabla de consulta
                ExcelWorksheet Sheet1 = Ep.Workbook.Worksheets.Add("Consulta");

                Color colFromHex = Color.FromArgb(22, 54, 92);

                Sheet1.Cells["A1:W1"].Style.Font.Bold = true;
                Sheet1.Cells["A1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet1.Cells["A1:W1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet1.Cells["A1:W1"].Style.Font.Color.SetColor(Color.White);
                Sheet1.Cells["A1:W1"].Style.Font.Name = "Century Gothic";

                Sheet1.Cells["A1"].Value = "Id lote";
                Sheet1.Cells["B1"].Value = "Id consulta";
                Sheet1.Cells["C1"].Value = "Cod prestador";
                Sheet1.Cells["D1"].Value = "Fecha inicio atención";
                Sheet1.Cells["E1"].Value = "Número autorización";
                Sheet1.Cells["F1"].Value = "Código consulta";
                Sheet1.Cells["G1"].Value = "Modalidad grupo servicio tec sal";
                Sheet1.Cells["H1"].Value = "Grupo servicios";
                Sheet1.Cells["I1"].Value = "Código servicio";
                Sheet1.Cells["J1"].Value = "Finalidad tecnologia salud";
                Sheet1.Cells["K1"].Value = "Causa motivo atención";
                Sheet1.Cells["L1"].Value = "Código diagnóstico principal";
                Sheet1.Cells["M1"].Value = "Código diagnóstico relacionado1";
                Sheet1.Cells["N1"].Value = "Código diagnóstico relacionado2";
                Sheet1.Cells["O1"].Value = "Código diagnóstico relacionado3";
                Sheet1.Cells["P1"].Value = "Tipo diagnóstico principal";
                Sheet1.Cells["Q1"].Value = "Tipo documento identificación";
                Sheet1.Cells["R1"].Value = "Número documento identificación";
                Sheet1.Cells["S1"].Value = "Vr servicio";
                Sheet1.Cells["T1"].Value = "Concepto recaudo";
                Sheet1.Cells["U1"].Value = "Valor pago moderador";
                Sheet1.Cells["V1"].Value = "Número FEV pago moderador";
                Sheet1.Cells["W1"].Value = "Consecutivo";

                row = 2;
                foreach (management_fis_cargueRips_consultaResult item in listaConsulta)
                {

                    Sheet1.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet1.Cells[string.Format("B{0}", row)].Value = item.id_consulta;
                    Sheet1.Cells[string.Format("C{0}", row)].Value = item.codPrestador;
                    Sheet1.Cells[string.Format("D{0}", row)].Value = item.fechaInicioAtencion;
                    Sheet1.Cells[string.Format("E{0}", row)].Value = item.numAutorizacion;
                    Sheet1.Cells[string.Format("F{0}", row)].Value = item.codConsulta;
                    Sheet1.Cells[string.Format("G{0}", row)].Value = item.modalidadGrupoServicioTecSal;
                    Sheet1.Cells[string.Format("H{0}", row)].Value = item.grupoServicios;
                    Sheet1.Cells[string.Format("I{0}", row)].Value = item.codServicio;
                    Sheet1.Cells[string.Format("J{0}", row)].Value = item.finalidadTecnologiaSalud;
                    Sheet1.Cells[string.Format("K{0}", row)].Value = item.causaMotivoAtencion;
                    Sheet1.Cells[string.Format("L{0}", row)].Value = item.codDiagnosticoPrincipal;
                    Sheet1.Cells[string.Format("M{0}", row)].Value = item.codDiagnosticoRelacionado1;
                    Sheet1.Cells[string.Format("N{0}", row)].Value = item.codDiagnosticoRelacionado2;
                    Sheet1.Cells[string.Format("O{0}", row)].Value = item.codDiagnosticoRelacionado3;
                    Sheet1.Cells[string.Format("P{0}", row)].Value = item.tipoDiagnosticoPrincipal;
                    Sheet1.Cells[string.Format("Q{0}", row)].Value = item.tipoDocumentoIdentificacion;
                    Sheet1.Cells[string.Format("R{0}", row)].Value = item.numDocumentoIdentificacion;
                    Sheet1.Cells[string.Format("S{0}", row)].Value = item.vrServicio;
                    Sheet1.Cells[string.Format("T{0}", row)].Value = item.conceptoRecaudo;
                    Sheet1.Cells[string.Format("U{0}", row)].Value = item.valorPagoModerador;
                    Sheet1.Cells[string.Format("V{0}", row)].Value = item.numFEVPagoModerador;
                    Sheet1.Cells[string.Format("W{0}", row)].Value = item.consecutivo;

                    Sheet1.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet1.Cells["A" + row + ":W1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet1.Cells["A:W"].AutoFitColumns();

                //Tabla de hospitalización
                ExcelWorksheet Sheet2 = Ep.Workbook.Worksheets.Add("Hospitalizacion");

                Sheet2.Cells["A1:Q1"].Style.Font.Bold = true;
                Sheet2.Cells["A1:Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet2.Cells["A1:Q1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet2.Cells["A1:Q1"].Style.Font.Color.SetColor(Color.White);
                Sheet2.Cells["A1:Q1"].Style.Font.Name = "Century Gothic";

                Sheet2.Cells["A1"].Value = "Id lote";
                Sheet2.Cells["B1"].Value = "Id hospitalización";
                Sheet2.Cells["C1"].Value = "Cod prestador";
                Sheet2.Cells["D1"].Value = "Via ingreso servicio salud";
                Sheet2.Cells["E1"].Value = "Fecha inicio atención";
                Sheet2.Cells["F1"].Value = "Número autorización";
                Sheet2.Cells["G1"].Value = "Causa motivo atención";
                Sheet2.Cells["H1"].Value = "Código diagnóstico principal";
                Sheet2.Cells["I1"].Value = "Código diagnóstico principal E";
                Sheet2.Cells["J1"].Value = "Código diagnóstico relacionado E1";
                Sheet2.Cells["K1"].Value = "Código diagnóstico relacionado E2";
                Sheet2.Cells["L1"].Value = "Código diagnóstico relacionado E3";
                Sheet2.Cells["M1"].Value = "Código complicación";
                Sheet2.Cells["N1"].Value = "Condición destino usuario egreso";
                Sheet2.Cells["O1"].Value = "Código diagnóstico causa muerte";
                Sheet2.Cells["P1"].Value = "Fecha egreso";
                Sheet2.Cells["Q1"].Value = "Consecutivo";

                row = 2;
                foreach (management_fis_cargueRips_hospitalizacionResult item in listaHospitalizacion)
                {
                    Sheet2.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet2.Cells[string.Format("B{0}", row)].Value = item.id_hospitalizacion;
                    Sheet2.Cells[string.Format("C{0}", row)].Value = item.codPrestador;
                    Sheet2.Cells[string.Format("D{0}", row)].Value = item.viaIngresoServicioSalud;
                    Sheet2.Cells[string.Format("E{0}", row)].Value = item.fechaInicioAtencion;
                    Sheet2.Cells[string.Format("F{0}", row)].Value = item.numAutorizacion;
                    Sheet2.Cells[string.Format("G{0}", row)].Value = item.causaMotivoAtencion;
                    Sheet2.Cells[string.Format("H{0}", row)].Value = item.codDiagnosticoPrincipal;
                    Sheet2.Cells[string.Format("I{0}", row)].Value = item.codDiagnosticoPrincipalE;
                    Sheet2.Cells[string.Format("J{0}", row)].Value = item.codDiagnosticoRelacionadoE1;
                    Sheet2.Cells[string.Format("K{0}", row)].Value = item.codDiagnosticoRelacionadoE2;
                    Sheet2.Cells[string.Format("L{0}", row)].Value = item.codDiagnosticoRelacionadoE3;
                    Sheet2.Cells[string.Format("M{0}", row)].Value = item.codComplicacion;
                    Sheet2.Cells[string.Format("N{0}", row)].Value = item.condicionDestinoUsuarioEgreso;
                    Sheet2.Cells[string.Format("O{0}", row)].Value = item.codDiagnosticoCausaMuerte;
                    Sheet2.Cells[string.Format("P{0}", row)].Value = item.fechaEgreso;
                    Sheet2.Cells[string.Format("Q{0}", row)].Value = item.consecutivo;

                    Sheet2.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet2.Cells[string.Format("P{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet2.Cells["A" + row + ":Q1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet2.Cells["A:Q"].AutoFitColumns();

                //Tabla de medicamentos
                ExcelWorksheet Sheet3 = Ep.Workbook.Worksheets.Add("Medicamentos");

                Sheet3.Cells["A1:Y1"].Style.Font.Bold = true;
                Sheet3.Cells["A1:Y1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet3.Cells["A1:Y1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet3.Cells["A1:Y1"].Style.Font.Color.SetColor(Color.White);
                Sheet3.Cells["A1:Y1"].Style.Font.Name = "Century Gothic";

                Sheet3.Cells["A1"].Value = "Id lote";
                Sheet3.Cells["B1"].Value = "Id medicamento";
                Sheet3.Cells["C1"].Value = "Cod prestador";
                Sheet3.Cells["D1"].Value = "Número autorización";
                Sheet3.Cells["E1"].Value = "Id MIPRES";
                Sheet3.Cells["F1"].Value = "Fecha dispens admón";
                Sheet3.Cells["G1"].Value = "Código diagnóstico principal";
                Sheet3.Cells["H1"].Value = "Código diagnóstico relacionado";
                Sheet3.Cells["I1"].Value = "Tipo medicamento";
                Sheet3.Cells["J1"].Value = "Código tecnología salud";
                Sheet3.Cells["K1"].Value = "Nombre tecnología salud";
                Sheet3.Cells["L1"].Value = "Concentración medicamento";
                Sheet3.Cells["M1"].Value = "Unidad medida";
                Sheet3.Cells["N1"].Value = "Forma farmacéutica";
                Sheet3.Cells["O1"].Value = "Unidad mín dispensa";
                Sheet3.Cells["P1"].Value = "Cantidad medicamento";
                Sheet3.Cells["Q1"].Value = "Días tratamiento";
                Sheet3.Cells["R1"].Value = "Tipo documento identificación";
                Sheet3.Cells["S1"].Value = "Número documento identificación";
                Sheet3.Cells["T1"].Value = "Vr unit medicamento";
                Sheet3.Cells["U1"].Value = "Vr servicio";
                Sheet3.Cells["V1"].Value = "Concepto recaudo";
                Sheet3.Cells["W1"].Value = "Valor pago moderador";
                Sheet3.Cells["X1"].Value = "Número FEV pago moderador";
                Sheet3.Cells["Y1"].Value = "Consecutivo";


                row = 2;
                foreach (management_fis_cargueRips_medicamentosResult item in listaMedicamento)
                {
                    Sheet3.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet3.Cells[string.Format("B{0}", row)].Value = item.id_medicamentos;
                    Sheet3.Cells[string.Format("C{0}", row)].Value = item.codPrestador;
                    Sheet3.Cells[string.Format("D{0}", row)].Value = item.numAutorizacion;
                    Sheet3.Cells[string.Format("E{0}", row)].Value = item.idMIPRES;
                    Sheet3.Cells[string.Format("F{0}", row)].Value = item.fechaDispensAdmon;
                    Sheet3.Cells[string.Format("G{0}", row)].Value = item.codDiagnosticoPrincipal;
                    Sheet3.Cells[string.Format("H{0}", row)].Value = item.codDiagnosticoRelacionado;
                    Sheet3.Cells[string.Format("I{0}", row)].Value = item.tipoMedicamento;
                    Sheet3.Cells[string.Format("J{0}", row)].Value = item.codTecnologiaSalud;
                    Sheet3.Cells[string.Format("K{0}", row)].Value = item.nomTecnologiaSalud;
                    Sheet3.Cells[string.Format("L{0}", row)].Value = item.concentracionMedicamento;
                    Sheet3.Cells[string.Format("M{0}", row)].Value = item.unidadMedida;
                    Sheet3.Cells[string.Format("N{0}", row)].Value = item.formaFarmaceutica;
                    Sheet3.Cells[string.Format("O{0}", row)].Value = item.unidadMinDispensa;
                    Sheet3.Cells[string.Format("P{0}", row)].Value = item.cantidadMedicamento;
                    Sheet3.Cells[string.Format("Q{0}", row)].Value = item.diasTratamiento;
                    Sheet3.Cells[string.Format("R{0}", row)].Value = item.tipoDocumentoIdentificacion;
                    Sheet3.Cells[string.Format("S{0}", row)].Value = item.numDocumentoIdentificacion;
                    Sheet3.Cells[string.Format("T{0}", row)].Value = item.vrUnitMedicamento;
                    Sheet3.Cells[string.Format("U{0}", row)].Value = item.vrServicio;
                    Sheet3.Cells[string.Format("V{0}", row)].Value = item.conceptoRecaudo;
                    Sheet3.Cells[string.Format("W{0}", row)].Value = item.valorPagoModerador;
                    Sheet3.Cells[string.Format("X{0}", row)].Value = item.numFEVPagoModerador;
                    Sheet3.Cells[string.Format("Y{0}", row)].Value = item.consecutivo;

                    Sheet3.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet3.Cells["A" + row + ":Y1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet3.Cells["A:Y"].AutoFitColumns();



                //Tabla de otros servicios
                ExcelWorksheet Sheet4 = Ep.Workbook.Worksheets.Add("Otros servicios");

                Sheet4.Cells["A1:R1"].Style.Font.Bold = true;
                Sheet4.Cells["A1:R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet4.Cells["A1:R1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet4.Cells["A1:R1"].Style.Font.Color.SetColor(Color.White);
                Sheet4.Cells["A1:R1"].Style.Font.Name = "Century Gothic";

                Sheet4.Cells["A1"].Value = "Id lote";
                Sheet4.Cells["B1"].Value = "Id otros servicios";
                Sheet4.Cells["C1"].Value = "Cod prestador";
                Sheet4.Cells["D1"].Value = "Número autorización";
                Sheet4.Cells["E1"].Value = "Id MIPRES";
                Sheet4.Cells["F1"].Value = "Fecha suministro tecnología";
                Sheet4.Cells["G1"].Value = "Tipo OS";
                Sheet4.Cells["H1"].Value = "Código tecnología salud";
                Sheet4.Cells["I1"].Value = "Nombre tecnología salud";
                Sheet4.Cells["J1"].Value = "Cantidad OS";
                Sheet4.Cells["K1"].Value = "Tipo documento identificación";
                Sheet4.Cells["L1"].Value = "Número documento identificación";
                Sheet4.Cells["M1"].Value = "Vr unit OS";
                Sheet4.Cells["N1"].Value = "Vr servicio";
                Sheet4.Cells["O1"].Value = "Concepto recaudo";
                Sheet4.Cells["P1"].Value = "Valor pago moderador";
                Sheet4.Cells["Q1"].Value = "Número FEV pago moderador";
                Sheet4.Cells["R1"].Value = "Consecutivo";

                row = 2;
                foreach (management_fis_cargueRips_otrosServiciosResult item in listaOtroServicio)
                {
                    Sheet4.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet4.Cells[string.Format("B{0}", row)].Value = item.id_otros_servicios;
                    Sheet4.Cells[string.Format("C{0}", row)].Value = item.codPrestador;
                    Sheet4.Cells[string.Format("D{0}", row)].Value = item.numAutorizacion;
                    Sheet4.Cells[string.Format("E{0}", row)].Value = item.idMIPRES;
                    Sheet4.Cells[string.Format("F{0}", row)].Value = item.fechaSuministroTecnologia;
                    Sheet4.Cells[string.Format("G{0}", row)].Value = item.tipoOS;
                    Sheet4.Cells[string.Format("H{0}", row)].Value = item.codTecnologiaSalud;
                    Sheet4.Cells[string.Format("I{0}", row)].Value = item.nomTecnologiaSalud;
                    Sheet4.Cells[string.Format("J{0}", row)].Value = item.cantidadOS;
                    Sheet4.Cells[string.Format("K{0}", row)].Value = item.tipoDocumentoIdentificacion;
                    Sheet4.Cells[string.Format("L{0}", row)].Value = item.numDocumentoIdentificacion;
                    Sheet4.Cells[string.Format("M{0}", row)].Value = item.vrUnitOS;
                    Sheet4.Cells[string.Format("N{0}", row)].Value = item.vrServicio;
                    Sheet4.Cells[string.Format("O{0}", row)].Value = item.conceptoRecaudo;
                    Sheet4.Cells[string.Format("P{0}", row)].Value = item.valorPagoModerador;
                    Sheet4.Cells[string.Format("Q{0}", row)].Value = item.numFEVPagoModerador;
                    Sheet4.Cells[string.Format("R{0}", row)].Value = item.consecutivo;

                    Sheet4.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet4.Cells["A" + row + ":R1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet4.Cells["A:R"].AutoFitColumns();


                //Tabla de procedimientos
                ExcelWorksheet Sheet5 = Ep.Workbook.Worksheets.Add("Procedimientos");

                Sheet5.Cells["A1:V1"].Style.Font.Bold = true;
                Sheet5.Cells["A1:V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet5.Cells["A1:V1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet5.Cells["A1:V1"].Style.Font.Color.SetColor(Color.White);
                Sheet5.Cells["A1:V1"].Style.Font.Name = "Century Gothic";

                Sheet5.Cells["A1"].Value = "Id lote";
                Sheet5.Cells["B1"].Value = "Id procedimientos";
                Sheet5.Cells["C1"].Value = "Cod prestador";
                Sheet5.Cells["D1"].Value = "Fecha inicio atención";
                Sheet5.Cells["E1"].Value = "Id MIPRES";
                Sheet5.Cells["F1"].Value = "Número autorización";
                Sheet5.Cells["G1"].Value = "Cod procedimiento";
                Sheet5.Cells["H1"].Value = "Via ingreso servicio salud";
                Sheet5.Cells["I1"].Value = "Modalidad grupo servicio tec sal";
                Sheet5.Cells["J1"].Value = "Grupo servicios";
                Sheet5.Cells["K1"].Value = "Código servicio";
                Sheet5.Cells["L1"].Value = "Finalidad tecnologia salud";
                Sheet5.Cells["M1"].Value = "Tipo documento identificación";
                Sheet5.Cells["N1"].Value = "Número documento identificación";
                Sheet5.Cells["O1"].Value = "Código diagnóstico principal";
                Sheet5.Cells["P1"].Value = "Código diagnóstico relacionado";
                Sheet5.Cells["Q1"].Value = "Código complicación";
                Sheet5.Cells["R1"].Value = "Vr servicio";
                Sheet5.Cells["S1"].Value = "Concepto recaudo";
                Sheet5.Cells["T1"].Value = "Valor pago moderador";
                Sheet5.Cells["U1"].Value = "Número FEV pago moderador";
                Sheet5.Cells["V1"].Value = "Consecutivo";


                row = 2;
                foreach (management_fis_cargueRips_procedimientosResult item in listaProcedimiento)
                {
                    Sheet5.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet5.Cells[string.Format("B{0}", row)].Value = item.id_procedimiento;
                    Sheet5.Cells[string.Format("C{0}", row)].Value = item.codPrestador;
                    Sheet5.Cells[string.Format("D{0}", row)].Value = item.fechaInicioAtencion;
                    Sheet5.Cells[string.Format("E{0}", row)].Value = item.idMIPRES;
                    Sheet5.Cells[string.Format("F{0}", row)].Value = item.numAutorizacion;
                    Sheet5.Cells[string.Format("G{0}", row)].Value = item.codProcedimiento;
                    Sheet5.Cells[string.Format("H{0}", row)].Value = item.viaIngresoServicioSalud;
                    Sheet5.Cells[string.Format("I{0}", row)].Value = item.modalidadGrupoServicioTecSal;
                    Sheet5.Cells[string.Format("J{0}", row)].Value = item.grupoServicios;
                    Sheet5.Cells[string.Format("K{0}", row)].Value = item.codServicio;
                    Sheet5.Cells[string.Format("L{0}", row)].Value = item.finalidadTecnologiaSalud;
                    Sheet5.Cells[string.Format("M{0}", row)].Value = item.tipoDocumentoIdentificacion;
                    Sheet5.Cells[string.Format("N{0}", row)].Value = item.numDocumentoIdentificacion;
                    Sheet5.Cells[string.Format("O{0}", row)].Value = item.codDiagnosticoPrincipal;
                    Sheet5.Cells[string.Format("P{0}", row)].Value = item.codDiagnosticoRelacionado;
                    Sheet5.Cells[string.Format("Q{0}", row)].Value = item.codComplicacion;
                    Sheet5.Cells[string.Format("R{0}", row)].Value = item.vrServicio;
                    Sheet5.Cells[string.Format("S{0}", row)].Value = item.conceptoRecaudo;
                    Sheet5.Cells[string.Format("T{0}", row)].Value = item.valorPagoModerador;
                    Sheet5.Cells[string.Format("U{0}", row)].Value = item.numFEVPagoModerador;
                    Sheet5.Cells[string.Format("V{0}", row)].Value = item.consecutivo;

                    Sheet5.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet5.Cells["A" + row + ":V1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet5.Cells["A:V"].AutoFitColumns();


                //Tabla de recien nacidos
                ExcelWorksheet Sheet6 = Ep.Workbook.Worksheets.Add("Recien nacido");

                Sheet6.Cells["A1:O1"].Style.Font.Bold = true;
                Sheet6.Cells["A1:O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet6.Cells["A1:O1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet6.Cells["A1:O1"].Style.Font.Color.SetColor(Color.White);
                Sheet6.Cells["A1:O1"].Style.Font.Name = "Century Gothic";

                Sheet6.Cells["A1"].Value = "Id lote";
                Sheet6.Cells["B1"].Value = "Id recien nacido";
                Sheet6.Cells["C1"].Value = "Cod prestador";
                Sheet6.Cells["D1"].Value = "Tipo documento identificación";
                Sheet6.Cells["E1"].Value = "Número documento identificación";
                Sheet6.Cells["F1"].Value = "Fecha nacimiento";
                Sheet6.Cells["G1"].Value = "Edad gestacional";
                Sheet6.Cells["H1"].Value = "Número consultas C prenatal";
                Sheet6.Cells["I1"].Value = "Cod sexo biológico";
                Sheet6.Cells["J1"].Value = "Peso";
                Sheet6.Cells["K1"].Value = "Código diagnóstico principal";
                Sheet6.Cells["L1"].Value = "Condición destino usuario egreso";
                Sheet6.Cells["M1"].Value = "Código diagnóstico causa muerte";
                Sheet6.Cells["N1"].Value = "Fecha egreso";
                Sheet6.Cells["O1"].Value = "Consecutivo";

                row = 2;
                foreach (management_fis_cargueRips_recienNacidoResult item in listaRecienNacido)
                {
                    Sheet6.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet6.Cells[string.Format("B{0}", row)].Value = item.id_nacido;
                    Sheet6.Cells[string.Format("C{0}", row)].Value = item.codPrestador;
                    Sheet6.Cells[string.Format("D{0}", row)].Value = item.tipoDocumentoIdentificacion;
                    Sheet6.Cells[string.Format("E{0}", row)].Value = item.numDocumentoIdentificacion;
                    Sheet6.Cells[string.Format("F{0}", row)].Value = item.fechaNacimiento;
                    Sheet6.Cells[string.Format("G{0}", row)].Value = item.edadGestacional;
                    Sheet6.Cells[string.Format("H{0}", row)].Value = item.numConsultasCPrenatal;
                    Sheet6.Cells[string.Format("I{0}", row)].Value = item.codSexoBiologico;
                    Sheet6.Cells[string.Format("J{0}", row)].Value = item.peso;
                    Sheet6.Cells[string.Format("K{0}", row)].Value = item.codDiagnosticoPrincipal;
                    Sheet6.Cells[string.Format("L{0}", row)].Value = item.condicionDestinoUsuarioEgreso;
                    Sheet6.Cells[string.Format("M{0}", row)].Value = item.codDiagnosticoCausaMuerte;
                    Sheet6.Cells[string.Format("N{0}", row)].Value = item.fechaEgreso;
                    Sheet6.Cells[string.Format("O{0}", row)].Value = item.consecutivo;

                    Sheet6.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet6.Cells[string.Format("N{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet6.Cells["A" + row + ":O1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet6.Cells["A:O"].AutoFitColumns();


                //Tabla de transacción
                ExcelWorksheet Sheet7 = Ep.Workbook.Worksheets.Add("Transaccion");

                Sheet7.Cells["A1:F1"].Style.Font.Bold = true;
                Sheet7.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet7.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet7.Cells["A1:F1"].Style.Font.Color.SetColor(Color.White);
                Sheet7.Cells["A1:F1"].Style.Font.Name = "Century Gothic";

                Sheet7.Cells["A1"].Value = "Id lote";
                Sheet7.Cells["B1"].Value = "Id transacción";
                Sheet7.Cells["C1"].Value = "Num documento ID obligado";
                Sheet7.Cells["D1"].Value = "Num factura";
                Sheet7.Cells["E1"].Value = "Tipo nota";
                Sheet7.Cells["F1"].Value = "Num nota";


                row = 2;
                foreach (management_fis_cargueRips_transaccionResult item in listaTransaccion)
                {
                    Sheet7.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet7.Cells[string.Format("B{0}", row)].Value = item.id_transaccion;
                    Sheet7.Cells[string.Format("C{0}", row)].Value = item.numDocumentoIdObligado;
                    Sheet7.Cells[string.Format("D{0}", row)].Value = item.numFactura;
                    Sheet7.Cells[string.Format("E{0}", row)].Value = item.tipoNota;

                    Sheet7.Cells[string.Format("F{0}", row)].Value = item.numNota;

                    Sheet7.Cells["A" + row + ":F1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet7.Cells["A:F"].AutoFitColumns();

                //Tabla de urgencias
                ExcelWorksheet Sheet8 = Ep.Workbook.Worksheets.Add("Urgencias");

                Sheet8.Cells["A1:N1"].Style.Font.Bold = true;
                Sheet8.Cells["A1:N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet8.Cells["A1:N1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet8.Cells["A1:N1"].Style.Font.Color.SetColor(Color.White);
                Sheet8.Cells["A1:N1"].Style.Font.Name = "Century Gothic";

                Sheet8.Cells["A1"].Value = "Id lote";
                Sheet8.Cells["B1"].Value = "Id urgencias";
                Sheet8.Cells["C1"].Value = "Cod prestador";
                Sheet8.Cells["D1"].Value = "Fecha inicio atención";
                Sheet8.Cells["E1"].Value = "Causa motivo atención";
                Sheet8.Cells["F1"].Value = "Cod diagnóstico principal";
                Sheet8.Cells["G1"].Value = "Cod diagnóstico principal E";
                Sheet8.Cells["H1"].Value = "Cod diagnóstico relacionado E1";
                Sheet8.Cells["I1"].Value = "Cod diagnóstico relacionado E2";
                Sheet8.Cells["J1"].Value = "Cod diagnóstico relacionado E3";
                Sheet8.Cells["K1"].Value = "Condición destino usuario egreso";
                Sheet8.Cells["L1"].Value = "Cod diagnóstico causa muerte";
                Sheet8.Cells["M1"].Value = "Fecha egreso";
                Sheet8.Cells["N1"].Value = "Consecutivo";


                row = 2;
                foreach (management_fis_cargueRips_urgenciasResult item in listaurgencia)
                {
                    Sheet8.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet8.Cells[string.Format("B{0}", row)].Value = item.id_urgencias;
                    Sheet8.Cells[string.Format("C{0}", row)].Value = item.codPrestador;
                    Sheet8.Cells[string.Format("D{0}", row)].Value = item.fechaInicioAtencion;
                    Sheet8.Cells[string.Format("E{0}", row)].Value = item.causaMotivoAtencion;
                    Sheet8.Cells[string.Format("F{0}", row)].Value = item.codDiagnosticoPrincipal;
                    Sheet8.Cells[string.Format("G{0}", row)].Value = item.codDiagnosticoPrincipalE;
                    Sheet8.Cells[string.Format("H{0}", row)].Value = item.codDiagnosticoRelacionadoE1;
                    Sheet8.Cells[string.Format("I{0}", row)].Value = item.codDiagnosticoRelacionadoE2;
                    Sheet8.Cells[string.Format("J{0}", row)].Value = item.codDiagnosticoRelacionadoE3;
                    Sheet8.Cells[string.Format("K{0}", row)].Value = item.condicionDestinoUsuarioEgreso;
                    Sheet8.Cells[string.Format("L{0}", row)].Value = item.codDiagnosticoCausaMuerte;
                    Sheet8.Cells[string.Format("M{0}", row)].Value = item.fechaEgreso;
                    Sheet8.Cells[string.Format("N{0}", row)].Value = item.consecutivo;

                    Sheet8.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet8.Cells[string.Format("M{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet8.Cells["A" + row + ":N1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet8.Cells["A:N"].AutoFitColumns();


                //Tabla de usuarios
                ExcelWorksheet Sheet9 = Ep.Workbook.Worksheets.Add("Usuarios");

                Sheet9.Cells["A1:M1"].Style.Font.Bold = true;
                Sheet9.Cells["A1:M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet9.Cells["A1:M1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet9.Cells["A1:M1"].Style.Font.Color.SetColor(Color.White);
                Sheet9.Cells["A1:M1"].Style.Font.Name = "Century Gothic";

                Sheet9.Cells["A1"].Value = "Id lote";
                Sheet9.Cells["B1"].Value = "Id usuario";
                Sheet9.Cells["C1"].Value = "Tipo documento identificación";
                Sheet9.Cells["D1"].Value = "Num documento identificación";
                Sheet9.Cells["E1"].Value = "Tipo usuario";
                Sheet9.Cells["F1"].Value = "Fecha nacimiento";
                Sheet9.Cells["G1"].Value = "Cod sexo";
                Sheet9.Cells["H1"].Value = "Cod pais residencia";
                Sheet9.Cells["I1"].Value = "Cod municipio residencia";
                Sheet9.Cells["J1"].Value = "Cod zona territorial residencia";
                Sheet9.Cells["K1"].Value = "Incapacidad";
                Sheet9.Cells["L1"].Value = "Consecutivo";
                Sheet9.Cells["M1"].Value = "Cod pais origen";

                row = 2;
                foreach (management_fis_cargueRips_usuariosResult item in listaUsuario)
                {
                    Sheet9.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet9.Cells[string.Format("B{0}", row)].Value = item.id_usuarios;
                    Sheet9.Cells[string.Format("C{0}", row)].Value = item.tipoDocumentoIdentificacion;
                    Sheet9.Cells[string.Format("D{0}", row)].Value = item.numDocumentoIdentificacion;
                    Sheet9.Cells[string.Format("E{0}", row)].Value = item.tipoUsuario;
                    Sheet9.Cells[string.Format("F{0}", row)].Value = item.fechaNacimiento;
                    Sheet9.Cells[string.Format("G{0}", row)].Value = item.codSexo;
                    Sheet9.Cells[string.Format("H{0}", row)].Value = item.codPaisResidencia;
                    Sheet9.Cells[string.Format("I{0}", row)].Value = item.codMunicipioResidencia;
                    Sheet9.Cells[string.Format("J{0}", row)].Value = item.codZonaTerritorialResidencia;
                    Sheet9.Cells[string.Format("K{0}", row)].Value = item.incapacidad;
                    Sheet9.Cells[string.Format("L{0}", row)].Value = item.consecutivo;
                    Sheet9.Cells[string.Format("M{0}", row)].Value = item.codPaisOrigen;

                    Sheet9.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet9.Cells["A" + row + ":M1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet9.Cells["A:M"].AutoFitColumns();

                var nombreArchivo = "RptRegistrosCargueRIPS_" + DateTime.Now;

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public void ExcelTableroNegociaciones(int? idMasivo, int? idContrato)
        {
            List<management_fisPrestadores_contratos_negociaciones_detalleResult> listareporte = new List<management_fisPrestadores_contratos_negociaciones_detalleResult>();
            try
            {
                listareporte = (List<management_fisPrestadores_contratos_negociaciones_detalleResult>)Session["ListadoNegociacionesDetalle"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Cargue");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:I1"].Style.Font.Bold = true;
                Sheet.Cells["A1:I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:I1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:I1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:I1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "CUPS NEGOCIADO";
                Sheet.Cells["B1"].Value = "DESCRIPCIÓN CUPS";
                Sheet.Cells["C1"].Value = "MANUAL";
                Sheet.Cells["D1"].Value = "INTERMEDIACIÓN";
                Sheet.Cells["E1"].Value = "TARIFA INICIAL";
                Sheet.Cells["F1"].Value = "TAFIFA CON INTERMEDIACIÓN";
                Sheet.Cells["G1"].Value = "FECHA DE INICIO DE VIGENCIA";
                Sheet.Cells["H1"].Value = "FECHA FIN VIGENCIA";
                Sheet.Cells["I1"].Value = "AMBITO";

                int row = 2;
                foreach (management_fisPrestadores_contratos_negociaciones_detalleResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.cups_negociado;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.descripcion_cups;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.manual;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.intermediacion;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.tarifa_inicial;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.tarifa_intermediacion;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_inicio_vigencia;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_fin_vigencia;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.ambito;

                    Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells["A" + row + ":I1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:I"].AutoFitColumns();

                var nombreArchivo = "RptNegociacion_" + idMasivo + "_contrato_" + idContrato + "_" + DateTime.Now;

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public ActionResult CargueMasivoDetalles()
        {
            ViewBag.regional = BusClass.GetRefRegion();

            return View();
        }

        //public JsonResult GuardarMasivoDetalles(HttpPostedFileBase archivo, int? regional)
        //{
        //    var mensaje = "";
        //    var respuesta = "";
        //    var rta = 0;
        //    Models.FIS.Facturacion_FIS Modelo = new Models.FIS.Facturacion_FIS();

        //    try
        //    {
        //        if (archivo.ContentLength > 0)
        //        {
        //            CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
        //            var asposeOptions = new Aspose.Cells.LoadOptions
        //            {
        //                MemorySetting = MemorySetting.MemoryPreference
        //            };

        //            Workbook wb = new Workbook(archivo.InputStream, asposeOptions);
        //            Worksheet worksheet = wb.Worksheets[0];
        //            Cells cells = worksheet.Cells;
        //            int MaximaFila = cells.MaxDataRow;

        //            var ExportTableOptions = new Aspose.Cells.ExportTableOptions
        //            {
        //                CheckMixedValueType = false,
        //                ExportColumnName = true,
        //                ExportAsString = true
        //            };

        //            DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

        //            fis_rips_sinJson_lote lote = new fis_rips_sinJson_lote();
        //            lote.fecha_digita = DateTime.Now;
        //            lote.usuario_digita = SesionVar.UserName;
        //            lote.regional = regional;
        //            respuesta = Modelo.ExcelMasivoDetalles(dataTable, lote, ref MsgRes);

        //            if (Modelo.rtaIngresoFacturasDetalle == 1)
        //            {
        //                mensaje = Modelo.mensajeIngresoFacturasDetalle;
        //                rta = 1;
        //            }
        //            else
        //            {
        //                mensaje = "ERROR AL INGRESAR CARGUE TARIFAS EN EL ARCHIVO: " + archivo.FileName + " - " + Modelo.mensajeIngresoFacturasDetalle;
        //            }
        //        }
        //        else
        //        {
        //            mensaje = "ARCHIVO SIN DATOS";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        mensaje = "ERROR AL CARGAR MASIVO: " + error;
        //    }

        //    return Json(new { mensaje = mensaje, rta = rta });
        //}

        public JsonResult GuardarMasivoDetalles(HttpPostedFileBase archivo, int? regional)
        {
            var mensaje = "";
            var respuesta = "";
            var rta = 0;
            Models.FIS.Facturacion_FIS Modelo = new Models.FIS.Facturacion_FIS();

            try
            {
                if (archivo.ContentLength > 0)
                {
                    using (var workbook = new XLWorkbook(archivo.InputStream))
                    {
                        var worksheet = workbook.Worksheet(1); // Asume que el archivo tiene al menos una hoja
                        var range = worksheet.RangeUsed(); // Obtiene el rango de celdas con datos

                        // Convertir el rango a DataTable
                        DataTable dataTable = new DataTable();
                        bool firstRow = true;

                        foreach (var row in range.Rows())
                        {
                            if (firstRow)
                            {
                                // Crear columnas a partir de la primera fila
                                foreach (var cell in row.Cells())
                                {
                                    dataTable.Columns.Add(cell.GetValue<string>());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                // Agregar filas al DataTable
                                var newRow = dataTable.NewRow();
                                for (int i = 0; i < row.Cells().Count(); i++)
                                {
                                    newRow[i] = row.Cell(i + 1).GetValue<string>();
                                }
                                dataTable.Rows.Add(newRow);
                            }
                        }


                        fis_rips_sinJson_lote lote = new fis_rips_sinJson_lote();
                        lote.fecha_digita = DateTime.Now;
                        lote.usuario_digita = SesionVar.UserName;
                        lote.regional = regional;
                        respuesta = Modelo.ExcelMasivoDetalles(dataTable, lote, ref MsgRes);

                        if (Modelo.rtaIngresoFacturasDetalle == 1)
                        {
                            mensaje = Modelo.mensajeIngresoFacturasDetalle;
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ERROR AL INGRESAR CARGUE TARIFAS EN EL ARCHIVO: " + archivo.FileName + " - " + Modelo.mensajeIngresoFacturasDetalle;
                        }
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

        public ActionResult TableroCargueMasivoDetalles(DateTime? fechaIni, DateTime? fechaFin, int? regional)
        {
            List<management_fisRips_TableroDetallesCargueResult> listado = new List<management_fisRips_TableroDetallesCargueResult>();
            List<management_fisRips_TableroDetallesCargueResult> listadoFinal = new List<management_fisRips_TableroDetallesCargueResult>();

            var idRol = Convert.ToInt32(SesionVar.ROL);

            try
            {

                listado = BusClass.ListadoDetallesMasivo(fechaIni, fechaFin, regional);
                if (idRol != 1)
                {
                    List<sis_auditor_regional> regionales = BusClass.listadoRegionalesUsuario(SesionVar.IDUser);

                    foreach (var item in regionales)
                    {
                        List<management_fisRips_TableroDetallesCargueResult> listadoFiltro = listado.Where(x => x.id_ref_regional == item.id_regional).ToList();
                        if (listadoFiltro.Count() > 0)
                        {
                            listadoFinal.AddRange(listadoFiltro);
                        }
                    }
                }
                else
                {
                    listadoFinal = listado;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.regional = BusClass.GetRefRegion();

            ViewBag.rol = idRol;
            ViewBag.listado = listadoFinal;
            ViewBag.conteo = listadoFinal.Count();
            ViewBag.idUsuario = SesionVar.IDUser;

            Session["ListadoDetalles"] = listadoFinal;

            return View();
        }

        public JsonResult EliminarCargueMasivo(int? id_lote, int? conteo, decimal? valor_total, string observacion)
        {
            try
            {
                var elimina = BusClass.EliminarCargueMasivoDetalles(id_lote);
                if (elimina != 0)
                {
                    log_eliminar_cargueMasivo_detalles eli = new log_eliminar_cargueMasivo_detalles();
                    eli.id_lote = id_lote;
                    eli.conteo_registros = conteo;
                    eli.valor_total = valor_total;
                    eli.observacion = observacion;
                    eli.usuario_digita = SesionVar.UserName;
                    eli.fecha_digita = DateTime.Now;

                    var registro = BusClass.InsertarLogEliminarMasivoDetalles(eli);

                    if (registro != 0)
                    {
                        return Json(new { success = true, mensaje = "El cargue de Detalles seleccionado se ha eliminado correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, mensaje = "Ha ocurrido un error al momento de eliminar el cargue. Comuniquese con el administrador del sistema para solucionar el inconveniente" }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    return Json(new { success = false, mensaje = "Ha ocurrido un error al momento de eliminar el cargue. Comuniquese con el administrador del sistema para solucionar el inconveniente" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { success = false, mensaje = "Ha ocurrido un error al momento de eliminar el cargue. Comuniquese con el administrador del sistema para solucionar el inconveniente" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CargueMasivoIVM()
        {
            return View();
        }

        public JsonResult GuardarMasivoIVM(HttpPostedFileBase archivo)
        {
            var mensaje = "";
            var respuesta = "";
            var rta = 0;
            Models.FIS.Facturacion_FIS Modelo = new Models.FIS.Facturacion_FIS();

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

                    fis_rips_cargueMasivo_ivm_lote lote = new fis_rips_cargueMasivo_ivm_lote();
                    lote.fecha_digita = DateTime.Now;
                    lote.usuario_digita = SesionVar.UserName;
                    respuesta = Modelo.ExcelMasivoIVM(dataTable, lote, ref MsgRes);

                    if (Modelo.rtaIngresoFacturasDetalle == 1)
                    {
                        mensaje = Modelo.mensajeIngresoFacturasDetalle;
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "ERROR AL INGRESAR CARGUE VIM EN EL ARCHIVO: " + archivo.FileName + " - " + Modelo.mensajeIngresoFacturasDetalle;
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

        public ActionResult CarguemasivoPedidos()
        {
            return View();
        }

        public JsonResult GuardarMasivoPedidos(HttpPostedFileBase archivo)
        {
            var mensaje = "";
            var respuesta = "";
            var rta = 0;
            Models.FIS.Facturacion_FIS Modelo = new Models.FIS.Facturacion_FIS();

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

                    fis_rips_cargueMasivo_pedidos_lote lote = new fis_rips_cargueMasivo_pedidos_lote();
                    lote.fecha_digita = DateTime.Now;
                    lote.usuario_digita = SesionVar.UserName;
                    respuesta = Modelo.ExcelMasivoPedidos(dataTable, lote, ref MsgRes);

                    if (Modelo.rtaIngresoFacturasDetalle == 1)
                    {
                        mensaje = Modelo.mensajeIngresoFacturasDetalle;
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "ERROR AL INGRESAR CARGUE PEDIDO EN EL ARCHIVO: " + archivo.FileName + " - " + Modelo.mensajeIngresoFacturasDetalle;
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

        public void ExcelDescargarDetalles(int? idLote)
        {
            List<management_fisSinJson_DescargarDatosResult> listareporte = new List<management_fisSinJson_DescargarDatosResult>();
            try
            {
                listareporte = BusClass.TraerListadoDetallesSinJson(idLote);

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadoDetalles_" + idLote);

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:P1"].Style.Font.Bold = true;
                Sheet.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:P1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:P1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "ID_detalle";
                Sheet.Cells["B1"].Value = "Número de la factura";
                Sheet.Cells["C1"].Value = "Código de habilitación";
                Sheet.Cells["D1"].Value = "Tipo Doc";
                Sheet.Cells["E1"].Value = "Numero Doc";
                Sheet.Cells["F1"].Value = "Fecha atención";
                Sheet.Cells["G1"].Value = "Número de autorización";
                Sheet.Cells["H1"].Value = "Código servicio";
                Sheet.Cells["I1"].Value = "Codigo Homologado FIS";
                Sheet.Cells["J1"].Value = "Descripción servicio";
                Sheet.Cells["K1"].Value = "Código del diagnóstico principal";
                Sheet.Cells["L1"].Value = "Diagnóstico relacionado";
                Sheet.Cells["M1"].Value = "Cantidad";
                Sheet.Cells["N1"].Value = "Valor Unitario";
                Sheet.Cells["O1"].Value = "Valor total";
                Sheet.Cells["P1"].Value = "Fuente";

                int row = 2;
                foreach (management_fisSinJson_DescargarDatosResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_detalle;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.numero_factura;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.codigo_habilitacion;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.tipo_identificacion_usuario;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.numero_identificacion_usuario;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_atencion;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.numero_autorizacion;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.codigo_cups;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.cups_homologado;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.descripcion_cups;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.codigo_diagnostico_principal;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.codigo_diagnostico_relacionado;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.numero_unidades;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.valor_unitario_medicamento;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.valor_neto_pagar;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.fuente;

                    Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells["A" + row + ":P1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:P"].AutoFitColumns();

                var nombreArchivo = "ReporteCargueDetalles_" + idLote + "_" + DateTime.Now.ToString("dd_MM_yyyy");

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public ActionResult VerPlantillaCargueDetalles()
        {
            try
            {
                //var ruta = "../Resources/CargueMasivoDetalles.xlsx";
                //string dirpath = Path.Combine(Request.PhysicalApplicationPath, ruta);

                var rutaRelativa = "Resources/CargueMasivoDetalles.xlsx";
                string dirpath = Path.Combine(Server.MapPath("~"), rutaRelativa);
                var ruta = Path.GetFullPath(dirpath);

                string filename = ruta;
                string extension = "";

                string[] nombrePartido = new string[0];
                nombrePartido = ruta.Split('\\');
                var nombreFinal = "CargueMasivoDetalles.xlsx";

                extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(dirpath, extension, nombreFinal);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public ActionResult CrearCie10FIS(string codigo)
        {
            ref_cie10_fis fis = new ref_cie10_fis();

            try
            {
                fis = BusClass.TraerCie10Codigo(codigo);

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.codigo = codigo;
            ViewBag.descripcion = fis != null ? fis.descripcion : "";

            return View();
        }

        public String validarCie10Codigo(string codigo)
        {
            var mensaje = "";

            try
            {
                ref_cie10_fis cie = BusClass.TraerCie10Codigo(codigo);
                if (cie.codigo != null)
                {
                    mensaje = "ESTE CODIGO CIE10 YA EXISTE";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return mensaje;
        }

        public String validarCie10Descripcion(string descripcion)
        {
            var mensaje = "";

            try
            {
                ref_cie10_fis cie = BusClass.TraerCie10Descripcion(descripcion);
                if (cie != null)
                {
                    mensaje = "ESTA DESCRIPCIÓN DE CIE10 YA EXISTE";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return mensaje;
        }

        public JsonResult GuardarCIE10FIS(string codigo, string descripcion)
        {
            var mensaje = "";
            var rta = 0;
            var gestion = 0;

            try
            {

                ref_cie10_fis_lote lote = new ref_cie10_fis_lote();
                lote.fecha_digita = DateTime.Now;
                lote.usuario_digita = SesionVar.UserName;
                var idLote = BusClass.InsertarLoteCIE10(lote);

                ref_cie10_fis fis = new ref_cie10_fis()
                {
                    id_lote = idLote,
                    codigo = codigo,
                    descripcion = descripcion,
                    estado = 1,
                    usuario_digita = SesionVar.UserName,
                    fecha_digita = DateTime.Now
                };

                var YaExiste = validarCie10Codigo(codigo);

                if (YaExiste == "")
                {
                    gestion = BusClass.InsertarCIE1OFIS(fis);
                }
                else
                {
                    gestion = BusClass.ActualizarCie10FIS(fis);
                }

                if (gestion != 0)
                {
                    mensaje = "CIE10 INGRESADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL INGRESAR EL CIE10";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = $"ERROR {error}";
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult CargueMasivoCIE10()
        {
            return View();
        }

        public JsonResult GuardarMasivoCIE10(HttpPostedFileBase archivo)
        {
            var mensaje = "";
            var respuesta = "";
            var rta = 0;
            Models.FIS.Facturacion_FIS Modelo = new Models.FIS.Facturacion_FIS();

            try
            {
                if (archivo.ContentLength > 0)
                {
                    using (var workbook = new XLWorkbook(archivo.InputStream))
                    {
                        var worksheet = workbook.Worksheet(1); // Asume que el archivo tiene al menos una hoja
                        var range = worksheet.RangeUsed(); // Obtiene el rango de celdas con datos

                        // Convertir el rango a DataTable
                        DataTable dataTable = new DataTable();
                        bool firstRow = true;

                        foreach (var row in range.Rows())
                        {
                            if (firstRow)
                            {
                                // Crear columnas a partir de la primera fila
                                foreach (var cell in row.Cells())
                                {
                                    dataTable.Columns.Add(cell.GetValue<string>());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                // Agregar filas al DataTable
                                var newRow = dataTable.NewRow();
                                for (int i = 0; i < row.Cells().Count(); i++)
                                {
                                    newRow[i] = row.Cell(i + 1).GetValue<string>();
                                }
                                dataTable.Rows.Add(newRow);
                            }
                        }


                        ref_cie10_fis_lote lote = new ref_cie10_fis_lote();
                        lote.fecha_digita = DateTime.Now;
                        lote.usuario_digita = SesionVar.UserName;

                        respuesta = Modelo.ExcelMasivoCIE10(dataTable, lote, ref MsgRes);

                        if (Modelo.rtaIngresoFacturasDetalle == 1)
                        {
                            mensaje = Modelo.mensajeIngresoFacturasDetalle;
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ERROR AL INGRESAR CARGUE CIE10 EN EL ARCHIVO: " + archivo.FileName + " - " + Modelo.mensajeIngresoFacturasDetalle;
                        }
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

        public ActionResult TableroCIE10FIS()
        {
            List<ref_cie10_fis> listado = new List<ref_cie10_fis>();

            try
            {
                listado = BusClass.listadoCie10FIS();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.conteo = listado.Count();

            Session["ListadoCIE10"] = listado;

            return View();
        }

        public void ExcelTableroControlCie10()
        {
            List<ref_cie10_fis> listareporte = new List<ref_cie10_fis>();
            try
            {
                listareporte = (List<ref_cie10_fis>)Session["ListadoCIE10"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadoCUPS");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:C1"].Style.Font.Bold = true;
                Sheet.Cells["A1:C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:C1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:C1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:C1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Código CIE10";
                Sheet.Cells["B1"].Value = "Descripción CIE10";
                Sheet.Cells["C1"].Value = "Fecha creación";

                int row = 2;
                foreach (ref_cie10_fis item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.codigo;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.descripcion;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.fecha_digita;

                    Sheet.Cells[string.Format("C{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells["A" + row + ":C1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:C"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", $"attachment; filename=RptCie10FIS_{DateTime.Now}.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public JsonResult EliminarCIE10(string cod)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var elimina = BusClass.ElininarCIE10FIS(cod);
                if (elimina != 0)
                {
                    mensaje = "CIE10 ELIMINADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR EL CIE10";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR EL CIE10: " + error;

            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult CargarContratosMasivo()
        {
            Session["ListadoPrestadores"] = BusClass.ListadoprestadoresFISeXISTENTES();
            return View();
        }

        public JsonResult GuardarContratosMasivo(HttpPostedFileBase archivo)
        {
            var mensaje = "";
            var respuesta = "";
            var rta = 0;
            Models.FIS.Facturacion_FIS Modelo = new Models.FIS.Facturacion_FIS();

            try
            {
                if (archivo.ContentLength > 0)
                {
                    using (var workbook = new XLWorkbook(archivo.InputStream))
                    {
                        var worksheet = workbook.Worksheet(1); // Asume que el archivo tiene al menos una hoja
                        var range = worksheet.RangeUsed(); // Obtiene el rango de celdas con datos

                        // Convertir el rango a DataTable
                        DataTable dataTable = new DataTable();
                        bool firstRow = true;

                        foreach (var row in range.Rows())
                        {
                            if (firstRow)
                            {
                                // Crear columnas a partir de la primera fila
                                foreach (var cell in row.Cells())
                                {
                                    dataTable.Columns.Add(cell.GetValue<string>());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                // Agregar filas al DataTable
                                var newRow = dataTable.NewRow();
                                for (int i = 0; i < row.Cells().Count(); i++)
                                {
                                    newRow[i] = row.Cell(i + 1).GetValue<string>();
                                }
                                dataTable.Rows.Add(newRow);
                            }
                        }


                        fis_rips_prestadores_contratos_lote lote = new fis_rips_prestadores_contratos_lote();
                        lote.fecha_digita = DateTime.Now;
                        lote.usuario_digita = SesionVar.UserName;

                        respuesta = Modelo.ExcelMasivoContratos(dataTable, lote, ref MsgRes);

                        if (Modelo.rtaIngresoFacturasDetalle == 1)
                        {
                            mensaje = Modelo.mensajeIngresoFacturasDetalle;
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ERROR AL INGRESAR CARGUE CONTRATOS EN EL ARCHIVO: " + archivo.FileName + " - " + Modelo.mensajeIngresoFacturasDetalle;
                        }
                    }
                }
                else
                {
                    mensaje = "ARCHIVO SIN DATOS";
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public void ExcelDescargarPrestadoresFISExistentes()
        {
            List<management_fis_listadoPrestadoresExistentesResult> listareporte = new List<management_fis_listadoPrestadoresExistentesResult>();
            try
            {
                listareporte = (List<management_fis_listadoPrestadoresExistentesResult>)Session["ListadoPrestadores"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ListadoPrestadores");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:G1"].Style.Font.Bold = true;
                Sheet.Cells["A1:G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:G1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:G1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id prestador";
                Sheet.Cells["B1"].Value = "Nit";
                Sheet.Cells["C1"].Value = "Codigo verificacion";
                Sheet.Cells["D1"].Value = "Codigo sede";
                Sheet.Cells["E1"].Value = "Codigo SAP";
                Sheet.Cells["F1"].Value = "Codigo habilitacion";
                Sheet.Cells["G1"].Value = "Razon social";

                int row = 2;
                foreach (management_fis_listadoPrestadoresExistentesResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_prestador;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.nit;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.codigo_verfificacion;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.codigo_sede;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.codigo_sede;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.codigo_SAP;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.razon_social;

                    row++;
                }

                Sheet.Cells["A" + row + ":G1" + row].Style.Font.Name = "Century Gothic";
                Sheet.Cells["A:G"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", $"attachment; filename=RptListaPrestadores_{DateTime.Now}.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public ActionResult VerPlantillaCargueContratos()
        {
            try
            {
                //var ruta = "../Resources/CargueMasivoDetalles.xlsx";
                //string dirpath = Path.Combine(Request.PhysicalApplicationPath, ruta);

                var rutaRelativa = "Resources/CargueMasivoContratos.xlsx";
                string dirpath = Path.Combine(Server.MapPath("~"), rutaRelativa);
                var ruta = Path.GetFullPath(dirpath);

                string filename = ruta;
                string extension = "";

                string[] nombrePartido = new string[0];
                nombrePartido = ruta.Split('\\');
                var nombreFinal = "CargueMasivoContratos.xlsx";

                extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(dirpath, extension, nombreFinal);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public ActionResult CargarTigasContratosMasivo()
        {
            Session["ListadoContratos"] = BusClass.ListadoContratosFISExistntes();
            Session["ListadoTigasDetallados"] = BusClass.TraerTigasDetallados();

            return View();
        }

        public JsonResult GuardarTigasContratoMasivo(HttpPostedFileBase archivo)
        {
            var mensaje = "";
            var respuesta = "";
            var rta = 0;
            Models.FIS.Facturacion_FIS Modelo = new Models.FIS.Facturacion_FIS();

            try
            {
                if (archivo.ContentLength > 0)
                {
                    using (var workbook = new XLWorkbook(archivo.InputStream))
                    {
                        var worksheet = workbook.Worksheet(1); // Asume que el archivo tiene al menos una hoja
                        var range = worksheet.RangeUsed(); // Obtiene el rango de celdas con datos

                        // Convertir el rango a DataTable
                        DataTable dataTable = new DataTable();
                        bool firstRow = true;

                        foreach (var row in range.Rows())
                        {
                            if (firstRow)
                            {
                                // Crear columnas a partir de la primera fila
                                foreach (var cell in row.Cells())
                                {
                                    dataTable.Columns.Add(cell.GetValue<string>());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                // Agregar filas al DataTable
                                var newRow = dataTable.NewRow();
                                for (int i = 0; i < row.Cells().Count(); i++)
                                {
                                    newRow[i] = row.Cell(i + 1).GetValue<string>();
                                }
                                dataTable.Rows.Add(newRow);
                            }
                        }

                        fis_rips_prestadores_contrato_tigas_lote lote = new fis_rips_prestadores_contrato_tigas_lote();
                        lote.fecha_digita = DateTime.Now;
                        lote.usuario_digita = SesionVar.UserName;

                        respuesta = Modelo.ExcelMasivoTigasContrato(dataTable, lote, ref MsgRes);

                        if (Modelo.rtaIngresoFacturasDetalle == 1)
                        {
                            mensaje = Modelo.mensajeIngresoFacturasDetalle;
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ERROR AL INGRESAR CARGUE TIGAS CONTRATOS EN EL ARCHIVO: " + archivo.FileName + " - " + Modelo.mensajeIngresoFacturasDetalle;
                        }
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
                mensaje = $"ERROR: {error}";
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult VerPlantillaCargueTIGASContrato()
        {
            try
            {
                //var ruta = "../Resources/CargueMasivoDetalles.xlsx";
                //string dirpath = Path.Combine(Request.PhysicalApplicationPath, ruta);

                var rutaRelativa = "Resources/CargueMasivoTigaContratos.xlsx";
                string dirpath = Path.Combine(Server.MapPath("~"), rutaRelativa);
                var ruta = Path.GetFullPath(dirpath);

                string filename = ruta;
                string extension = "";

                string[] nombrePartido = new string[0];
                nombrePartido = ruta.Split('\\');
                var nombreFinal = "CargueMasivoTigaContratos.xlsx";

                extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(dirpath, extension, nombreFinal);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public void ExcelDescargarContratosExistentes()
        {
            List<management_fis_contratosExistentesResult> listareporte = new List<management_fis_contratosExistentesResult>();
            try
            {
                listareporte = (List<management_fis_contratosExistentesResult>)Session["ListadoContratos"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ListadoContratos");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:H1"].Style.Font.Bold = true;
                Sheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:H1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:H1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id contrato";
                Sheet.Cells["B1"].Value = "Num contrato";
                Sheet.Cells["C1"].Value = "Contrato operativo";
                Sheet.Cells["D1"].Value = "Grupo compras";
                Sheet.Cells["E1"].Value = "Razón social";
                Sheet.Cells["F1"].Value = "Codigo SAP";
                Sheet.Cells["G1"].Value = "Codigo habilitacion";
                Sheet.Cells["H1"].Value = "Codigo sede";

                int row = 2;
                foreach (management_fis_contratosExistentesResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_contrato;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.num_contrato;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.contrato_operativo;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.grupo_compras;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.razon_social;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.codigo_SAP;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.codigo_habilitacion;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.codigo_sede;

                    row++;
                }

                Sheet.Cells["A" + row + ":H1" + row].Style.Font.Name = "Century Gothic";
                Sheet.Cells["A:H"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", $"attachment; filename=RptListaContratos_{DateTime.Now}.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public void ExcelDescargarTigasDetallados()
        {
            List<ref_tigas_detallados> listareporte = new List<ref_tigas_detallados>();
            try
            {
                listareporte = (List<ref_tigas_detallados>)Session["ListadoTigasDetallados"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ListadoTigaDetallado");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:D1"].Style.Font.Bold = true;
                Sheet.Cells["A1:D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:D1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:D1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:D1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id ref";
                Sheet.Cells["B1"].Value = "Id tiga";
                Sheet.Cells["C1"].Value = "Tiga detallado";
                Sheet.Cells["D1"].Value = "Descripción tiga detallado";

                int row = 2;
                foreach (ref_tigas_detallados item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_ref;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.id_tiga;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.tiga_detallado;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.descripcion_tiga_detallado;

                    row++;
                }

                Sheet.Cells["A" + row + ":D1" + row].Style.Font.Name = "Century Gothic";
                Sheet.Cells["A:D"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", $"attachment; filename=RptListaTigaDetallado_{DateTime.Now}.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public ActionResult TableroEliminarNroPedido()
        {
            return View();
        }

        public JsonResult EliminarNroPedidoIdAf(int? idAf)
        {
            var rta = 0;
            var mensaje = "";
            try
            {
                managemenet_prestadores_traerDatosFacturasResult datos = BusClass.ListadoFacturasIdAf((int)idAf);

                if (datos != null)
                {
                    if (datos.estado_factura == 21 || datos.estado_factura == 22)
                    {
                        var gestiona = BusClass.DevolverNroPedidoFacturaId(idAf, SesionVar.UserName);
                        if (gestiona != 0)
                        {
                            rta = 0;
                            mensaje = "NRO PEDIDO DEVUELTO CORRECTAMENTE";
                        }
                        else
                        {
                            rta = 0;
                            mensaje = "ERROR AL DEVOLVER EL NRO DE PEDIDO";
                        }
                    }
                    else
                    {
                        rta = 0;
                        mensaje = "LA FACTURA NO ESTÁ EN ESTADOS CON PEDIDO";
                    }
                }
                else
                {
                    rta = 0;
                    mensaje = "NO HAY FACTURAS CON ESTE ID";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = $"ERROR AL DEVOLVER EL NRO DE PEDIDO : {error}";
            }
            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult TableroEdicionContratosFacturas()
        {

            List<management_fis_tableroActualizarContratos_gestionAnalistaResult> listado = new List<management_fis_tableroActualizarContratos_gestionAnalistaResult>();

            try
            {
                listado = BusClass.ListadoGestionContratos();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.conteo = listado.Count();

            Session["ListadoContratosFacturas"] = listado;

            return View();
        }

        public PartialViewResult EditarContratoFactura(int? idGestion, int? idFac, int? idContra, string codPresta)
        {
            ViewBag.idGestion = idGestion;
            ViewBag.idFac = idFac;
            ViewBag.idContra = idContra;
            ViewBag.codPresta = codPresta;

            List<management_fis_traerDatosContrato_nitResult> listaContratos = new List<management_fis_traerDatosContrato_nitResult>();
            fis_rips_facturas factura = new fis_rips_facturas();
            try
            {
                listaContratos = BusClass.TraerListaDatosContratoNit(codPresta);
                factura = BusClass.TraerFacturaIdAfAnalista(idFac);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaContratos = listaContratos;

            return PartialView(factura);
        }

        public JsonResult ActualizarContratoFactura(int? idGestion, int? idFac, int? idContrato, string numContrato, string centro, string grupoCompras, string posContrato, string contratoOperativo, int? idContratoNuevo)
        {
            int rta = 0; string mensaje = "";
            try
            {

                fis_rips_prestadores_contratos contrato = BusClass.TraerDatosContratoPrestadorId(idContratoNuevo);
                if (contrato != null)
                {
                    var actualizaNumContrato = BusClass.ActualizarContratoGestionFisFactura(idFac, contrato);

                    if (actualizaNumContrato != 0)
                    {
                        log_fis_rips_prestadores_contratos_actualizaciones nuevo = new log_fis_rips_prestadores_contratos_actualizaciones();
                        nuevo.id_factura = idFac;
                        nuevo.id_gestion = idGestion;
                        nuevo.idContrato_anterior = idContrato;
                        nuevo.idContrato_nuevo = idContratoNuevo;
                        nuevo.numero_contrato_anterior = numContrato;
                        nuevo.numero_contrato_nuevo = contrato.num_contrato;
                        nuevo.numero_contrato_nuevo = contrato.num_contrato;
                        nuevo.centro_logistico_anterior = centro;
                        nuevo.centro_logistico_nuevo = contrato.centro_logistico;
                        nuevo.grupo_compras_anterior = grupoCompras;
                        nuevo.grupo_compras_nuevo = contrato.grupo_compras;
                        nuevo.posicion_contrato_anterior = posContrato;
                        nuevo.posicion_contrato_nuevo = contrato.posicion_contrato;
                        nuevo.numero_contrato_operativo_anterior = contratoOperativo;
                        nuevo.numero_contrato_operativo_nuevo = contrato.contrato_operativo;
                        nuevo.fecha_digita = DateTime.Now;
                        nuevo.usuario_digita = SesionVar.UserName;

                        var insertaLogNuevo = BusClass.InsertarLogCambioContratoFactura(nuevo);

                        mensaje = "CONTRATO DE GESTIÓN ACTUALIZADO CORRECTAMEHTE";
                        rta = 1;
                    }
                    else
                    {
                        throw new Exception("NO SE PUDO ACTUALIZAR LOS DATOS DEL CONTRATO");
                    }
                }
                else
                {
                    throw new Exception("NO EXISTE EL CONTRATO SELECCIONADO");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });

        }

        public void ExcelContratosFacturas()
        {
            List<management_fis_tableroActualizarContratos_gestionAnalistaResult> listareporte = new List<management_fis_tableroActualizarContratos_gestionAnalistaResult>();
            try
            {
                listareporte = (List<management_fis_tableroActualizarContratos_gestionAnalistaResult>)Session["ListadoContratosFacturas"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ListadoContratosFacturas");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:L1"].Style.Font.Bold = true;
                Sheet.Cells["A1:L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:L1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:L1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:L1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id gestión";
                Sheet.Cells["B1"].Value = "Id factura";
                Sheet.Cells["C1"].Value = "Numero factura";
                Sheet.Cells["D1"].Value = "Id contrato";
                Sheet.Cells["E1"].Value = "Numero contrato";
                Sheet.Cells["F1"].Value = "Centro logistico";
                Sheet.Cells["G1"].Value = "Grupo compras";
                Sheet.Cells["H1"].Value = "posición contrato";
                Sheet.Cells["I1"].Value = "Numero contrato operativo";
                Sheet.Cells["J1"].Value = "Valor factura";
                Sheet.Cells["K1"].Value = "Codigo prestador";
                Sheet.Cells["L1"].Value = "Razon social";

                int row = 2;
                foreach (management_fis_tableroActualizarContratos_gestionAnalistaResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_fis_factura;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.id_factura;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.numero_factura;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.idContrato;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.numero_contrato;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.centro_logistico;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.grupo_compras;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.posicion_contrato;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.numero_contrato_operativo;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.valor_total_factura;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.codigo_prestador;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.prestador;

                    Sheet.Cells["A" + row + ":L1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:L"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", $"attachment; filename=RptContratosFacturas_{DateTime.Now}.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public JsonResult DesactivarContrato(int? id)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var actualiza = BusClass.ActualizarEstadoContrato(id, 0);
                if(actualiza != 0)
                {
                    mensaje = "CONTRATO DESACTIVADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL DESACTIVAR CONTRATO";
                }
            }
            catch(Exception ex)
            {
                var error = ex.Message;
                mensaje = $"ERROR AL DESACTIVAR CONTRATO {error} ";

            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult ActivarContrato(int? id)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var actualiza = BusClass.ActualizarEstadoContrato(id, 0);
                if (actualiza != 0)
                {
                    mensaje = "CONTRATO ACTIVADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ACTIVAR CONTRATO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = $"ERROR AL ACTIVAR CONTRATO {error} ";

            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

    }
}