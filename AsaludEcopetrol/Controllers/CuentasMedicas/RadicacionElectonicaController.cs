using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using AsaludEcopetrol.BussinesManager;
using System.Net;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Configuration;
using QRCoder;
using Ionic.Zip;
using AsaludEcopetrol.Models.CuentasMedicas;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mail;
using System.Text;
using AsaludEcopetrol.Helpers;
using System.Data.OleDb;
using System.Data.Entity.SqlServer;
using Kendo.Mvc.UI;
using QRCode = QRCoder.QRCode;
using System.Globalization;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Data.Linq;
using AsaludEcopetrol.Models;
using Microsoft.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO.MemoryMappedFiles;
using System.Net.Configuration;
using Aspose.Cells;

namespace AsaludEcopetrol.Controllers.CuentasMedicas
{

    [SessionExpireFilter]
    public class RadicacionElectonicaController : Controller
    {
        #region Propiedades

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

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PrestadoresConnectionString"].ConnectionString;

        ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

        #endregion

        public ActionResult RefrescarTableroRecepcionFacturas()
        {
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            List<vw_prestadores_lotes> ListadoLotesFacturasCache = cache["listadoLoteFacturas" + nomUsuario] as List<vw_prestadores_lotes>;
            if (ListadoLotesFacturasCache.Count > 0)
            {
                cache.Remove("listadoLoteFacturas" + nomUsuario);
            }
            return RedirectToAction("TableroRecepcionFacturas", "RadicacionElectonica");
        }

        public ActionResult TableroRecepcionFacturas(int? rta)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<vw_prestadores_lotes> Lista = new List<vw_prestadores_lotes>();

            ViewBag.idusuario = SesionVar.IDUser;

            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            List<vw_prestadores_lotes> ListadoLotesFacturasCache = cache["listadoLoteFacturas" + nomUsuario] as List<vw_prestadores_lotes>;
            if (ListadoLotesFacturasCache == null || ListadoLotesFacturasCache.Count() == 0)
            {
                Lista = Model.GetRecepcionFacturas(ref MsgRes).Where(l => l.lote_asignado_a_analista == "NO").ToList();

                /*Se definen las politicas y se insertan en cache los resultados de la consulta hecha a base de datos*/
                DateTime expirationDate = DateTime.Now.AddHours(1);
                CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expirationDate };

                cache.Set("listadoLoteFacturas" + nomUsuario, Lista, policy);
                cache.Set("tiempoExpiracionLotes" + nomUsuario, expirationDate, policy);
            }
            else
            {
                Lista = ListadoLotesFacturasCache;
            }

            List<vw_prestadores_lotes> Lista2 = new List<vw_prestadores_lotes>();
            List<vw_prestadores_lotes> Lista3 = new List<vw_prestadores_lotes>();
            List<vw_analistas_recepcion> listadoanalistas = new List<vw_analistas_recepcion>();

            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            if (rta != null)
            {
                ViewData["rta"] = rta;
                ViewData["Msg"] = TempData["mensaje"].ToString();
            }

            if (SesionVar.ROL != "1")
            {
                List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                regional obj = new regional();
                list = BusClass.GetRegionalAuditor();
                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                foreach (var item in list)
                {
                    Lista2 = Lista.Where(x => x.id_ref_regional == item.id_regional).ToList();

                    Lista3 = Lista3.Concat(Lista2).ToList();
                }

                Lista3 = Lista3.OrderBy(x => x.fecha_digita).ToList();
                ViewBag.lista = Lista3;
                ViewBag.count = Lista.Count();

                return View(Lista3);
            }
            else
            {
                Lista = Lista.OrderBy(x => x.fecha_digita).ToList();
                ViewBag.lista = Lista;
                ViewBag.count = Lista.Count();

                ViewBag.analistas = BusClass.GetListAnalistas();
                return View(Lista);
            }
        }

        public string ObtenerAnalistasRegional(int idregional)
        {
            string result = "<option value=''>-Seleccionar-<option>";

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            var listanalistas = BusClass.GetListAnalistas().Where(l => l.id_regional == idregional);
            foreach (var item in listanalistas)
            {
                result += "<option value=" + item.id_usuario + ">" + item.nom_analista + "<option>";
            }

            return result;
        }

        public PartialViewResult ObtenerAnalistasRegional2(List<int> capitulos)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<vw_analistas_recepcion> listanalistas = new List<vw_analistas_recepcion>();
            List<vw_analistas_recepcion> Lista3 = new List<vw_analistas_recepcion>();
            List<sis_auditor_regional> list = new List<sis_auditor_regional>();

            listanalistas = BusClass.GetListAnalistas();

            regional obj = new regional();

            list = BusClass.GetRegionalAuditor();
            list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

            foreach (var item in list)
            {
                listanalistas = listanalistas.Where(l => l.id_regional == item.id_regional).ToList();
                Lista3 = Lista3.Concat(listanalistas).ToList();
            }

            ViewBag.lista = Lista3;
            ViewBag.cargue_base = capitulos;

            return PartialView(Model);
        }

        public ActionResult AsignarLoteAnalista(int id_lote, int id_analista)
        {
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            try
            {
                ref_analista_lote obj = new ref_analista_lote();
                obj.id_lote_facturas = id_lote;
                obj.id_analista = id_analista;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                List<ref_analista_lote> listaExistenciaLote = new List<ref_analista_lote>();
                listaExistenciaLote = BusClass.TraerAnalistaLoteExistente(id_lote);

                if (listaExistenciaLote.Count() > 0)
                {
                    Model.ActualizarLoteAnalista(obj, ref MsgRes);
                }
                else
                {
                    Model.Insertaranalistalote(obj, ref MsgRes);
                }

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    BusClass.ActualizarFacturas_automaticas(id_lote);

                    /*Validacion empleada para eliminar el lote del listado que aun esta en cache*/

                    List<vw_prestadores_lotes> ListadoLotesFacturasCache = cache["listadoLoteFacturas" + nomUsuario] as List<vw_prestadores_lotes>;
                    if (ListadoLotesFacturasCache != null)
                    {
                        var itemCache = ListadoLotesFacturasCache.Where(l => l.id_cargue_base == id_lote).FirstOrDefault();
                        if (itemCache != null)
                        {
                            String key = "tiempoExpiracionLotes" + nomUsuario;
                            DateTime expiracionCache = CheckCachedExpiry(key);
                            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expiracionCache };

                            ListadoLotesFacturasCache.Remove(itemCache);
                            cache.Set("listadoLoteFacturas" + nomUsuario, ListadoLotesFacturasCache, policy);
                            cache.Set("tiempoExpiracionLotes" + nomUsuario, expiracionCache, policy);
                        }
                    }

                    TempData["mensaje"] = "Lote asignado al analista correctamente";
                    return RedirectToAction("TableroRecepcionFacturas", "RadicacionElectonica", new { rta = 1 });
                }
                else
                {
                    TempData["mensaje"] = "Hubo un error asignando el lote de facturas al analista:  " + MsgRes.DescriptionResponse;
                    return RedirectToAction("TableroRecepcionFacturas", "RadicacionElectonica", new { rta = 2 });
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                TempData["mensaje"] = "Hubo un error asignando el lote de facturas al analista:  " + error;
                return RedirectToAction("TableroRecepcionFacturas", "RadicacionElectonica", new { rta = 2 });
            }
        }


        #region tablero de facturas para coordinadores
        public ActionResult TableroFacturas(int idcargue)
        {

            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;

            ViewData["rta"] = 0;
            ViewData["msj"] = "";

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managment_prestadores_facturasResult> result = new List<managment_prestadores_facturasResult>();
            List<ref_rechazos_Fac> list = new List<ref_rechazos_Fac>();
            try
            {
                List<vw_prestadores_lotes> ListadoLotesFacturasCache = cache["listadoLoteFacturas" + nomUsuario] as List<vw_prestadores_lotes>;
                result = Model.GetFacturasByIdRecepcion(idcargue, ref MsgRes);
                list = Model.Getref_rechazos_Fac(ref MsgRes);

                if (result.Count == 0)
                {
                    if (ListadoLotesFacturasCache != null)
                    {
                        var itemCache = ListadoLotesFacturasCache.Where(l => l.id_cargue_base == idcargue).FirstOrDefault();
                        if (itemCache != null)
                        {
                            String key = "tiempoExpiracionLotes" + nomUsuario;
                            DateTime expiracionCache = CheckCachedExpiry(key);
                            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expiracionCache };

                            ListadoLotesFacturasCache.Remove(itemCache);
                            cache.Set("listadoLoteFacturas" + nomUsuario, ListadoLotesFacturasCache, policy);
                            cache.Set("tiempoExpiracionLotes" + nomUsuario, expiracionCache, policy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.List = list;
            ViewBag.idcargue = idcargue;

            return View(result);
        }

        [HttpPost]
        public ActionResult TableroFacturas(int idcargue, int detalle, int accion, string observaciones, Int32? id_motivo_rechazo)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            ecop_firma_digital_sami firma = new ecop_firma_digital_sami();

            try
            {
                firma = BusClass.traerDatosFirma(SesionVar.IDUser);

                if (firma == null)
                {
                    throw new Exception("El usuario no se encuentra con firma digital en SAMI. Por favor solicitarla");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    if (accion == 1)
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 2,id_analista_gestiona = @id_analista_gestiona Where id_af = @id";
                        command.Parameters.AddWithValue("@id_analista_gestiona", SesionVar.IDUser);
                    }
                    else
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 3, observaciones = @obs, id_motivo_rechazo = @id_motivo_rechazo, id_analista_gestiona = @id_analista_gestiona  Where id_af = @id";
                        command.Parameters.AddWithValue("@obs", observaciones);
                        command.Parameters.AddWithValue("@id_motivo_rechazo", id_motivo_rechazo);
                        command.Parameters.AddWithValue("@id_analista_gestiona", SesionVar.IDUser);
                    }
                    command.Parameters.AddWithValue("@id", detalle);
                    connection.Open();
                    command.CommandTimeout = 120;
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                ViewData["rta"] = 1;
                if (accion == 1)
                {
                    ViewData["msj"] = "Factura aprobada exitosamente";
                }
                else
                {
                    ViewData["msj"] = "Factura no aprobada exitosamente";
                }

            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["msj"] = ex.Message;
            }


            List<ref_rechazos_Fac> list = new List<ref_rechazos_Fac>();

            list = Model.Getref_rechazos_Fac(ref MsgRes);

            ViewBag.List = list;

            List<managment_prestadores_facturasResult> result = Model.GetFacturasByIdRecepcion(idcargue, ref MsgRes);
            return View(result);
        }
        #endregion

        #region tablero de facturas para analistas

        public ActionResult RefrescarTableroRecepcionFacturasAnalista()
        {
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            List<managementFacturasanalistas_lotes_okResult> ListadoLotesFacturasAnalistaCache = cache["ListadoLotesFacturasAnalistaCache" + nomUsuario] as List<managementFacturasanalistas_lotes_okResult>;
            if (ListadoLotesFacturasAnalistaCache.Count > 0)
            {
                cache.Remove("ListadoLotesFacturasAnalistaCache" + nomUsuario);
            }
            return RedirectToAction("TableroRecepcionFacturasAnalistas", "RadicacionElectonica");
        }

        public ActionResult TableroRecepcionFacturasAnalistas()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            List<managementFacturasanalistas_lotes_okResult> ListadoLotesFacturasAnalistaCache = cache["ListadoLotesFacturasAnalistaCache" + nomUsuario] as List<managementFacturasanalistas_lotes_okResult>;
            List<managementFacturasanalistas_lotes_okResult> Lista = new List<managementFacturasanalistas_lotes_okResult>();
            if (ListadoLotesFacturasAnalistaCache == null || ListadoLotesFacturasAnalistaCache.Count() == 0)
            {
                if (SesionVar.ROL != "1")
                {
                    Lista = Model.GetFacturaAnalistasok(Convert.ToInt32(SesionVar.ROL), SesionVar.UserName, ref MsgRes);
                }
                else
                {
                    Lista = Model.GetFacturaAnalistasok(Convert.ToInt32(SesionVar.ROL), "", ref MsgRes);
                }

                /*Se definen las politicas y se insertan en cache los resultados de la consulta hecha a base de datos*/
                DateTime expirationDate = DateTime.Now.AddHours(1);
                CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expirationDate };

                cache.Set("ListadoLotesFacturasAnalistaCache" + nomUsuario, Lista, policy);
                cache.Set("tiempoExpiracionAnalistaLotes" + nomUsuario, expirationDate, policy);
            }
            else
            {
                Lista = ListadoLotesFacturasAnalistaCache;
            }

            ViewBag.idUsuario = SesionVar.IDUser;
            ViewBag.Lista = Lista.ToList();
            return View(Model);
        }

        public ActionResult TableroFacturasAnalista(int idcargue)
        {
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;

            ViewData["rta"] = 0;
            ViewData["msj"] = "";

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managment_prestadores_facturas2Result> result = new List<managment_prestadores_facturas2Result>();
            List<ref_rechazos_Fac> list = new List<ref_rechazos_Fac>();
            try
            {
                List<managementFacturasanalistas_lotes_okResult> ListadoLotesFacturasAnalistaCache = cache["ListadoLotesFacturasAnalistaCache" + nomUsuario] as List<managementFacturasanalistas_lotes_okResult>;
                result = Model.GetFacturasByIdRecepcion2(idcargue, ref MsgRes);
                list = Model.Getref_rechazos_Fac(ref MsgRes);

                if (result.Count == 0)
                {
                    if (ListadoLotesFacturasAnalistaCache != null)
                    {
                        var itemCache = ListadoLotesFacturasAnalistaCache.Where(l => l.id_cargue_base == idcargue).FirstOrDefault();
                        if (itemCache != null)
                        {
                            String key = "tiempoExpiracionAnalistaLotes" + nomUsuario;
                            DateTime expiracionCache = CheckCachedExpiry(key);
                            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expiracionCache };

                            ListadoLotesFacturasAnalistaCache.Remove(itemCache);
                            cache.Set("ListadoLotesFacturasAnalistaCache" + nomUsuario, ListadoLotesFacturasAnalistaCache, policy);
                            cache.Set("tiempoExpiracionAnalistaLotes" + nomUsuario, expiracionCache, policy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.List = list;
            ViewBag.idcargue = idcargue;
            ViewBag.conteo = result.Count();

            return View(result);
        }

        [HttpPost]
        public ActionResult TableroFacturasAnalista(int idcargue, int detalle, int accion, string observaciones, Int32? id_motivo_rechazo)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {

                    if (accion == 1)
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 2,id_analista_gestiona = @id_analista_gestiona Where id_af = @id";
                        command.Parameters.AddWithValue("@id_analista_gestiona", SesionVar.IDUser);
                    }
                    else
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 3, observaciones = @obs, id_motivo_rechazo = @id_motivo_rechazo, id_analista_gestiona = @id_analista_gestiona  Where id_af = @id";
                        command.Parameters.AddWithValue("@obs", observaciones);
                        command.Parameters.AddWithValue("@id_motivo_rechazo", id_motivo_rechazo);
                        command.Parameters.AddWithValue("@id_analista_gestiona", SesionVar.IDUser);
                    }
                    command.Parameters.AddWithValue("@id", detalle);
                    connection.Open();
                    command.CommandTimeout = 120;
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                ViewData["rta"] = 1;
                if (accion == 1)
                {
                    ViewData["msj"] = "Factura aprobada exitosamente";
                }
                else
                {
                    ViewData["msj"] = "Factura no aprobada exitosamente";
                }

            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["msj"] = ex.Message;
            }


            List<ref_rechazos_Fac> list = new List<ref_rechazos_Fac>();

            list = Model.Getref_rechazos_Fac(ref MsgRes);

            ViewBag.List = list;

            List<managment_prestadores_facturasResult> result = Model.GetFacturasByIdRecepcion(idcargue, ref MsgRes);
            return View(result);
        }
        #endregion

        public PartialViewResult ModalRipsMasivo(int? tipo, int? idDetalle, int? idCargue, string listadoFacturas,
       string codPrestador,
       string listadoCodPrestador)
        {
            //tipo 2: facturas individual; tipo 3: varios id masivo
            var cuv = "";
            string listadoCuv = "";
            var existeCuv = 0;

            try
            {
                if (tipo == 2)
                {
                    var consulta = BusClass.TraerCuv(idDetalle);
                    cuv = consulta != null ? consulta.cuv : "";

                    existeCuv = string.IsNullOrEmpty(cuv) ? existeCuv = 2 : existeCuv = 1;
                }

                else if (tipo == 3)
                {
                    string[] listadoId = listadoFacturas.Split(',');

                    foreach (var item in listadoId)
                    {
                        int id = Convert.ToInt32(item);
                        var codcuv = BusClass.TraerCuv(id).cuv;

                        if (codcuv != "")
                        {
                            existeCuv = string.IsNullOrEmpty(codcuv) ? 2 : 1;
                            listadoCuv = listadoCuv == "" ? listadoCuv += codcuv : listadoCuv += "," + codcuv;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.cuv = cuv;
            ViewBag.existeCuv = existeCuv;
            ViewBag.idDetalle = idDetalle;
            ViewBag.idCargue = idCargue;
            ViewBag.listadoId = listadoFacturas;
            ViewBag.listadoCuv = listadoCuv;
            ViewBag.tipo = tipo;
            ViewBag.codPrestador = codPrestador;
            ViewBag.listadoCodPrestador = listadoCodPrestador;

            return PartialView();
        }

        public JsonResult SaveTableroFacturas(int detalle)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            String mensaje = "";

            var auditor = SesionVar.IDUser;

            var BaseImagen = Model.GetFirmas(auditor);
            if (BaseImagen != null)
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

                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);


                }
                catch (Exception ex)
                {

                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                mensaje = "ERROR... Usuario no puede aprobar porque no tiene firma digital en SAMI.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult TableroFacturasAprobadas()
        {
            ViewData["rta"] = 0;
            ViewData["msj"] = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managmentprestadoresfacturasaceptadasResult> result = Model.GetFacturasAceptadas(2, SesionVar.IDUser, ref MsgRes);
            result = result.OrderByDescending(x => x.dias_trascurridos).ToList();

            List<vw_auditores_totales> list = new List<vw_auditores_totales>();

            list = Model.GetAuditorTotales(ref MsgRes);
            ViewBag.Auditores = list;
            ViewBag.lista = result;
            ViewBag.count = result.Count();

            return View(result);
        }

        public ActionResult TableroFacturasAprobadas2()
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            return View(Model);
        }

        public ActionResult Selection_Read([DataSourceRequest] DataSourceRequest request, int? opc)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managmentprestadoresfacturasaceptadasResult> ListaTotal = (List<managmentprestadoresfacturasaceptadasResult>)Session["ListaFacturasAsigAuditor"];
            List<managmentprestadoresfacturasaceptadasResult> listaok = new List<managmentprestadoresfacturasaceptadasResult>();
            opc = 1;

            listaok = Model.GetFacturasAceptadas(2, SesionVar.IDUser, ref MsgRes);
            Session["ListaFacturasAsigAuditor"] = listaok.ToList();
            ViewBag.fecha = Convert.ToDateTime(DateTime.Now);
            var lista = new object();

            lista = (from item in listaok
                     select new
                     {
                         id_cargue_dtll = item.id_cargue_dtll,
                         id_cargue_base = item.id_cargue_base,
                         num_factura = item.num_factura,
                         valor_neto = item.valor_neto,
                         nombre_prestador = item.nombre_prestador,
                         dias_trascurridos = item.dias_trascurridos,
                         fecha_ultima_gestion = item.fecha_ultima_gestion.Value.ToString("dd/MM/yyyy"),
                         id_regional = item.id_ref_regional,
                         version = item.version

                     }).Distinct().OrderByDescending(f => f.dias_trascurridos);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult TableroGestionFacturas(DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac)
        //{

        //    if (prestador == "")
        //    {
        //        prestador = null;
        //    }
        //    if (nit == "")
        //    {
        //        nit = null;
        //    }
        //    Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

        //    try
        //    {

        //        List<managmentprestadoresfacturasgestionadas3Result> result = new List<managmentprestadoresfacturasgestionadas3Result>();
        //        List<managmentprestadoresfacturasgestionadas3Result> lista = new List<managmentprestadoresfacturasgestionadas3Result>();
        //        List<managmentprestadoresfacturasgestionadas3Result> lista3 = new List<managmentprestadoresfacturasgestionadas3Result>();
        //        List<managmentprestadoresfacturasgestionadas3Result> Lista2 = (List<managmentprestadoresfacturasgestionadas3Result>)Session["ListaFacturas2"];

        //        List<managmentprestadoresfacturasgestionadasFechasResult> listaFechas = new List<managmentprestadoresfacturasgestionadasFechasResult>();

        //        List<managmentprestadoresfacturasgestionadas3Result> listaok = new List<managmentprestadoresfacturasgestionadas3Result>();

        //        DateTime fechaIni = Convert.ToDateTime(fechainicial);
        //        DateTime fechaFin = Convert.ToDateTime(fechafinal);

        //        if (fechainicial != null)
        //        {
        //            List<managmentprestadoresfacturasgestionadas3Result> client = new List<managmentprestadoresfacturasgestionadas3Result>();

        //            client = Model.GetGestionFacturas();
        //            client = client.Where(x => x.fecha_recepcion_fac >= fechaIni && x.fecha_recepcion_fac <= fechafinal).ToList();

        //            listaok = client;

        //            if (estado != "")
        //            {
        //                Int32 estado_ok = Convert.ToInt32(estado);
        //                listaok = listaok.Where(x => x.estado_factura.Value == estado_ok).ToList();
        //            }
        //            if (regional != null)
        //            {
        //                listaok = listaok.Where(x => x.id_ref_regional.Value == regional).ToList();
        //            }
        //            if (prestador != null)
        //            {
        //                Int32 prestador_ok = Convert.ToInt32(prestador);
        //                listaok = listaok.Where(x => x.prestador == prestador_ok).ToList();
        //            }
        //            if (nit != null)
        //            {
        //                listaok = listaok.Where(x => x.num_id_prestador == nit).ToList();
        //            }

        //            Session["ListaFacturas2"] = listaok;

        //            if (SesionVar.ROL != "1")
        //            {
        //                if (SesionVar.ROL == "21")
        //                {
        //                    ViewBag.opcion = 3;
        //                    lista = listaok;
        //                }
        //                else
        //                {
        //                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
        //                    regional obj = new regional();
        //                    list = BusClass.GetRegionalAuditor();
        //                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

        //                    foreach (var item in list)
        //                    {
        //                        result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
        //                        lista3 = lista3.Concat(result).ToList();
        //                    }

        //                    lista = lista3;
        //                    ViewBag.opcion = 2;
        //                }

        //            }
        //            else
        //            {
        //                lista = listaok;
        //                ViewBag.opcion = 1;
        //            }
        //            Session["ListaFacturas"] = lista;
        //            ViewBag.Lista = lista;


        //        }
        //        else
        //        {
        //            Lista2 = Model.GetGestionFacturasv2(null, fechainicial, fechafinal, estado, regional, prestador, nit, numFac);

        //            List<managmentprestadoresfacturasgestionadas3Result> client = (from item in Lista2
        //                                                                          select new managmentprestadoresfacturasgestionadas3Result()
        //                                                                          {
        //                                                                              id_cargue_base = item.id_cargue_base,
        //                                                                              id_cargue_dtll = item.id_cargue_dtll,
        //                                                                              prestador = item.prestador,
        //                                                                              nombre_prestador = item.nombre_prestador,
        //                                                                              num_id_prestador = item.num_id_prestador,
        //                                                                              num_factura = item.num_factura,
        //                                                                              valor_neto = item.valor_neto,
        //                                                                              estado_factura = item.estado_factura,
        //                                                                              id_ref_ciudades = item.id_ref_ciudades,
        //                                                                              ciudad = item.ciudad,
        //                                                                              id_ref_regional = item.id_ref_regional,
        //                                                                              nombre_regional = item.nombre_regional,
        //                                                                              id_auditor_asignado = item.id_auditor_asignado,
        //                                                                              nom_auditor = item.nom_auditor,
        //                                                                              id_analista_gestiona = item.id_analista_gestiona,
        //                                                                              nom_analitica = item.nom_analitica,
        //                                                                              multiusuario = item.multiusuario,
        //                                                                              id_diagnostico = item.id_diagnostico,
        //                                                                              diagnostico = item.diagnostico,
        //                                                                              fecha_recepcion_fac = item.fecha_recepcion_fac,
        //                                                                              fecha_aprobacion = item.fecha_aprobacion,
        //                                                                              estado_des = item.estado_des,
        //                                                                              count_soportes = item.count_soportes,
        //                                                                              count_soportes_zip = item.count_soportes_zip,
        //                                                                              fecha_exp_factura = item.fecha_exp_factura,
        //                                                                              tipoGastos = item.tipoGastos,
        //                                                                              ruta = item.ruta,
        //                                                                              valorGlosa = item.valorGlosa,
        //                                                                              MotivosGlosa = item.MotivosGlosa,
        //                                                                              Observaciones = item.Observaciones


        //                                                                          }).ToList();

        //            listaok = client;

        //            Session["ListaFacturas2"] = listaok;

        //            if (SesionVar.ROL != "1")
        //            {
        //                if (SesionVar.ROL == "21")
        //                {
        //                    ViewBag.opcion = 3;
        //                    lista = listaok;
        //                }
        //                else
        //                {
        //                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
        //                    regional obj = new regional();
        //                    list = BusClass.GetRegionalAuditor();
        //                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

        //                    foreach (var item in list)
        //                    {
        //                        result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
        //                        lista3 = lista3.Concat(result).ToList();
        //                    }

        //                    lista = lista3;
        //                    ViewBag.opcion = 2;
        //                }

        //            }

        //            else
        //            {
        //                lista = listaok;
        //                ViewBag.opcion = 1;
        //            }
        //            Session["ListaFacturas"] = lista;
        //            ViewBag.Lista = lista;

        //        }

        //        ViewBag.ROL = SesionVar.ROL;
        //        ViewBag.regionales = BusClass.GetRefRegion();
        //        ViewBag.ref_estado = Model.GetEstadoFacturas();
        //    }

        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return View(Model);
        //}

        public JsonResult BuscarPrestador(string term, int tipofiltro)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<Ref_ips_cuentas> prestadores = Model.GetPrstadorCuentas(term, tipofiltro);

            var lista = new object();

            switch (tipofiltro)
            {
                case 2:

                    lista = (from item in prestadores
                             select new
                             {
                                 id = item.id_ref_ips_cuentas,
                                 nit = item.Nit,
                                 nombre = item.Nombre.ToUpper(),
                                 label = item.Nit,
                             }).Distinct().OrderBy(f => f.label).Take(15);
                    break;
                case 1:
                    lista = (from item in prestadores
                             select new
                             {
                                 id = item.id_ref_ips_cuentas,
                                 nit = item.Nit,
                                 nombre = item.Nombre.ToUpper(),
                                 label = item.Nombre,
                             }).Distinct().OrderBy(f => f.label).Take(15);

                    break;

                default:
                    break;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TableroGestionFacturas2()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            try
            {

                if (SesionVar.ROL != "1")
                {
                    if (SesionVar.ROL == "21")
                    {
                        ViewBag.opcion = 3;
                    }
                    else
                    {
                        ViewBag.opcion = 2;
                    }
                }
                else
                {
                    ViewBag.opcion = 1;
                }
                ViewBag.ROL = SesionVar.ROL;
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.ref_estado = Model.GetEstadoFacturas();
                //ViewBag.fecha = Convert.ToDateTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        //Get antiguo
        //public JsonResult Get(int? idDetalle, DateTime? fechainicial, DateTime? fechafin, String nombre_prestador, Int32? estado, int? regional, String nit, String numFac, int? page, int? limit, string buscar)
        //{
        //    string filtrosUsados = "";
        //    ObjectCache cache = MemoryCache.Default;
        //    List<managmentprestadoresfacturasgestionadas3Result> fileContents = cache["filecontents"] as List<managmentprestadoresfacturasgestionadas3Result>;

        //    if (nombre_prestador == "")
        //    {
        //        nombre_prestador = null;
        //    }
        //    if (nit == "")
        //    {
        //        nit = null;
        //    }
        //    if (numFac == "")
        //    {
        //        numFac = null;
        //    }

        //    Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

        //    List<managmentprestadoresfacturasgestionadas3Result> result = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    List<managmentprestadoresfacturasgestionadas3Result> lista = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    List<managmentprestadoresfacturasgestionadas3Result> lista3 = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    List<managmentprestadoresfacturasgestionadas3Result> listaok = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    List<managmentprestadoresfacturasgestionadas3Result> records = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    int total = 0;

        //    try
        //    {
        //        #region aquiñonesc 17-nov-2022 se usa para saber que filtros usan y que usuario lo hace

        //        if (!string.IsNullOrEmpty(buscar))
        //        {
        //            log_busquedas_tableros_facturas obj = new log_busquedas_tableros_facturas();
        //            obj.tablero_A_consultar = "Tablero gestión facturas";
        //            obj.usuario_digita = SesionVar.UserName;
        //            obj.fecha_digita = DateTime.Now;

        //            if (fechainicial != null)
        //                filtrosUsados += "//Fecha recep inicial: " + fechainicial.Value.ToString("dd/MM/yyyy") + " ";
        //            if (fechafin != null)
        //                filtrosUsados += "//Fecha recep final: " + fechafin.Value.ToString("dd/MM/yyyy") + " ";
        //            if (estado != null)
        //                filtrosUsados += "//Estado del caso: " + estado.ToString() + " ";
        //            if (regional != null)
        //                filtrosUsados += "//Regional: " + regional.ToString() + " ";
        //            if (!string.IsNullOrEmpty(nombre_prestador))
        //                filtrosUsados += "//prestador: " + nombre_prestador.ToString() + " ";
        //            if (!string.IsNullOrEmpty(nit))
        //                filtrosUsados += "//Nit prestador: " + nit.ToString() + " ";
        //            if (!string.IsNullOrEmpty(numFac))
        //                filtrosUsados += "//Número factura: " + numFac.ToString() + " ";

        //            obj.parametros_buscados = filtrosUsados;

        //            Model.InsertarLogBusquedaTableros(obj, ref MsgRes);
        //        }
        //        #endregion

        //        List<managmentprestadoresfacturasgestionadas3Result> ListaTotal = (List<managmentprestadoresfacturasgestionadas3Result>)Session["ListaFacturasTotal"];

        //        if (fechainicial == null && nombre_prestador == null && estado == null && regional == null && nit == null && numFac == null && idDetalle == null)
        //        {
        //            listaok = new List<managmentprestadoresfacturasgestionadas3Result>();
        //            Session["ListaFacturas3"] = listaok.ToList();
        //            Session["ListaFacturasTotal"] = listaok.ToList();
        //        }
        //        else
        //        {
        //            if (fileContents != null)
        //            {
        //                listaok = fileContents;
        //            }
        //            else
        //            {
        //                if (numFac != null || !string.IsNullOrEmpty(Convert.ToString(estado)) || !string.IsNullOrEmpty(Convert.ToString(regional)) || !string.IsNullOrEmpty(nombre_prestador)
        //                    || !string.IsNullOrEmpty(nit))
        //                {
        //                    listaok = Model.GetGestionFacturasv2(idDetalle, fechainicial, fechafin, Convert.ToString(estado), regional, nombre_prestador, nit, numFac);
        //                }
        //                else
        //                {
        //                    listaok = Model.GetGestionFacturas();
        //                    //Session["ListaFacturasTotal"] = listaok.ToList();
        //                    ViewBag.fecha = Convert.ToDateTime(DateTime.Now);
        //                    if (fileContents == null || fileContents.Count == 0)
        //                    {
        //                        CacheItemPolicy policy = new CacheItemPolicy();
        //                        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
        //                        cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(180.0);
        //                        fileContents = listaok.ToList();

        //                        cache.Add("filecontents", fileContents, cacheItemPolicy);

        //                    }
        //                }
        //            }
        //        }

        //        if (SesionVar.ROL != "1")
        //        {
        //            if (SesionVar.ROL == "21" || SesionVar.ROL == "20")
        //            {
        //                ViewBag.opcion = 3;
        //                lista = listaok;
        //            }
        //            else
        //            {
        //                List<sis_auditor_regional> list = new List<sis_auditor_regional>();
        //                regional obj = new regional();
        //                list = BusClass.GetRegionalAuditor();
        //                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

        //                foreach (var item in list)
        //                {
        //                    result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
        //                    lista3 = lista3.Concat(result).ToList();
        //                }

        //                lista = lista3;
        //                ViewBag.opcion = 2;
        //            }

        //        }
        //        else
        //        {
        //            lista = listaok;
        //            ViewBag.opcion = 1;
        //        }

        //        var query = lista.Select(item => new managmentprestadoresfacturasgestionadas3Result
        //        {
        //            id_cargue_base = item.id_cargue_base,
        //            id_cargue_dtll = item.id_cargue_dtll,
        //            prestador = item.prestador,
        //            nombre_prestador = item.nombre_prestador,
        //            homologacion_razonSocial = item.homologacion_razonSocial,
        //            homologacion_nit = item.homologacion_nit,
        //            homologacion_sap = item.homologacion_sap,
        //            num_id_prestador = item.num_id_prestador,
        //            num_factura = item.num_factura,
        //            valor_neto = item.valor_neto,
        //            estado_factura = item.estado_factura,
        //            id_ref_ciudades = item.id_ref_ciudades,
        //            ciudad = item.ciudad,
        //            id_ref_regional = item.id_ref_regional,
        //            nombre_regional = item.nombre_regional,
        //            id_auditor_asignado = item.id_auditor_asignado,
        //            nom_auditor = item.nom_auditor,
        //            id_analista_gestiona = item.id_analista_gestiona,
        //            nom_analitica = item.nom_analitica,
        //            multiusuario = item.multiusuario,
        //            id_diagnostico = item.id_diagnostico,
        //            diagnostico = item.diagnostico,
        //            fecha_recepcion_fac = item.fecha_recepcion_fac,
        //            fecha_aprobacion = item.fecha_aprobacion,
        //            estado_des = item.estado_des,
        //            count_soportes = item.count_soportes,
        //            count_soportes_zip = item.count_soportes_zip,
        //            fecha_exp_factura = item.fecha_exp_factura,
        //            tipoGastos = item.tipoGastos,
        //            ruta = item.ruta,
        //            valorGlosa = item.valorGlosa,
        //            MotivosGlosa = item.MotivosGlosa,
        //            Observaciones = item.Observaciones,
        //            estado_aprobada = item.estado_aprobada,
        //            motivos_rechazo = item.motivos_rechazo,
        //            id_factura_nueva = item.id_factura_nueva,
        //            Fecha_nueva_recepción_nueva = item.Fecha_nueva_recepción_nueva,
        //            Numero_factura_nueva = item.Numero_factura_nueva,
        //            valor_factura_nueva = item.valor_factura_nueva,
        //            estado_fac_nueva = item.estado_fac_nueva
        //        });

        //        DateTime fechaIni = Convert.ToDateTime(fechainicial);
        //        DateTime fechaFin = Convert.ToDateTime(fechafin);


        //        if (fechainicial != null)
        //        {
        //            query = query.Where(x => x.fecha_recepcion_fac >= fechaIni).ToList();
        //            query = query.Where(x => x.fecha_recepcion_fac <= fechaFin).ToList();
        //        }

        //        if (idDetalle != null)
        //        {
        //            query = query.Where(q => q.id_cargue_dtll == idDetalle);
        //        }

        //        if (!string.IsNullOrWhiteSpace(nombre_prestador))
        //        {
        //            //query = query.Where(q => q.nombre_prestador != null && q.homologacion_razonSocial != null);
        //            query = query.Where(q => q.nombre_prestador.Contains(nombre_prestador) || q.homologacion_razonSocial.Contains(nombre_prestador));
        //        }

        //        if (estado != null)
        //        {
        //            query = query.Where(q => q.estado_factura == estado);
        //        }

        //        if (regional != null)
        //        {
        //            query = query.Where(q => q.id_ref_regional == regional);
        //        }
        //        if (!string.IsNullOrWhiteSpace(nit))
        //        {
        //            query = query.Where(q => q.num_id_prestador == nit || q.homologacion_nit == nit);
        //        }
        //        if (!string.IsNullOrWhiteSpace(numFac))
        //        {
        //            query = query.Where(q => q.num_factura.Contains(numFac));
        //        }

        //        Session["ListaFacturas3"] = query.ToList();

        //        total = query.Count();
        //        if (page.HasValue && limit.HasValue)
        //        {
        //            int start = (page.Value - 1) * limit.Value;
        //            records = query.Skip(start).Take(limit.Value).ToList();
        //        }
        //        else
        //        {
        //            records = query.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        //}

        //Nuevo get, solo 2023 en adelante

        //public JsonResult Get(int? idDetalle, DateTime? fechainicial, DateTime? fechafin, String nombre_prestador, Int32? estado, int? regional, String nit, String numFac, int? page, int? limit, string buscar)
        //{
        //    string filtrosUsados = "";
        //    ObjectCache cache = MemoryCache.Default;
        //    List<managmentprestadoresfacturasgestionadas3Result> fileContents = cache["filecontents"] as List<managmentprestadoresfacturasgestionadas3Result>;

        //    if (nombre_prestador == "")
        //    {
        //        nombre_prestador = null;
        //    }
        //    if (nit == "")
        //    {
        //        nit = null;
        //    }
        //    if (numFac == "")
        //    {
        //        numFac = null;
        //    }

        //    Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

        //    List<managmentprestadoresfacturasgestionadas3Result> result = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    List<managmentprestadoresfacturasgestionadas3Result> lista = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    List<managmentprestadoresfacturasgestionadas3Result> lista3 = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    List<managmentprestadoresfacturasgestionadas3Result> listaok = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    List<managmentprestadoresfacturasgestionadas3Result> records = new List<managmentprestadoresfacturasgestionadas3Result>();
        //    int total = 0;

        //    try
        //    {
        //        #region aquiñonesc 17-nov-2022 se usa para saber que filtros usan y que usuario lo hace

        //        if (!string.IsNullOrEmpty(buscar))
        //        {
        //            log_busquedas_tableros_facturas obj = new log_busquedas_tableros_facturas();
        //            obj.tablero_A_consultar = "Tablero gestión facturas";
        //            obj.usuario_digita = SesionVar.UserName;
        //            obj.fecha_digita = DateTime.Now;

        //            if (fechainicial != null)
        //                filtrosUsados += "//Fecha recep inicial: " + fechainicial.Value.ToString("dd/MM/yyyy") + " ";
        //            if (fechafin != null)
        //                filtrosUsados += "//Fecha recep final: " + fechafin.Value.ToString("dd/MM/yyyy") + " ";
        //            if (estado != null)
        //                filtrosUsados += "//Estado del caso: " + estado.ToString() + " ";
        //            if (regional != null)
        //                filtrosUsados += "//Regional: " + regional.ToString() + " ";
        //            if (!string.IsNullOrEmpty(nombre_prestador))
        //                filtrosUsados += "//prestador: " + nombre_prestador.ToString() + " ";
        //            if (!string.IsNullOrEmpty(nit))
        //                filtrosUsados += "//Nit prestador: " + nit.ToString() + " ";
        //            if (!string.IsNullOrEmpty(numFac))
        //                filtrosUsados += "//Número factura: " + numFac.ToString() + " ";

        //            obj.parametros_buscados = filtrosUsados;

        //            Model.InsertarLogBusquedaTableros(obj, ref MsgRes);
        //        }
        //        #endregion

        //        List<managmentprestadoresfacturasgestionadas3Result> ListaTotal = (List<managmentprestadoresfacturasgestionadas3Result>)Session["ListaFacturasTotal"];

        //        if (fechainicial == null && nombre_prestador == null && estado == null && regional == null && nit == null && numFac == null && idDetalle == null)
        //        {
        //            listaok = new List<managmentprestadoresfacturasgestionadas3Result>();
        //            Session["ListaFacturas3"] = listaok.ToList();
        //            Session["ListaFacturasTotal"] = listaok.ToList();
        //        }
        //        else
        //        {
        //            if (fileContents != null)
        //            {
        //                listaok = fileContents;
        //            }
        //            else
        //            {
        //                if (numFac != null || !string.IsNullOrEmpty(Convert.ToString(estado)) || !string.IsNullOrEmpty(Convert.ToString(regional)) || !string.IsNullOrEmpty(nombre_prestador)
        //                    || !string.IsNullOrEmpty(nit))
        //                {
        //                    listaok = Model.GetGestionFacturasv2(idDetalle, fechainicial, fechafin, Convert.ToString(estado), regional, nombre_prestador, nit, numFac);
        //                }
        //                else
        //                {
        //                    listaok = Model.GetGestionFacturas();
        //                    //Session["ListaFacturasTotal"] = listaok.ToList();
        //                    ViewBag.fecha = Convert.ToDateTime(DateTime.Now);
        //                    if (fileContents == null || fileContents.Count == 0)
        //                    {
        //                        CacheItemPolicy policy = new CacheItemPolicy();
        //                        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
        //                        cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(180.0);
        //                        fileContents = listaok.ToList();

        //                        cache.Add("filecontents", fileContents, cacheItemPolicy);

        //                    }
        //                }
        //            }
        //        }

        //        if (SesionVar.ROL != "1")
        //        {
        //            if (SesionVar.ROL == "21" || SesionVar.ROL == "20")
        //            {
        //                ViewBag.opcion = 3;
        //                lista = listaok;
        //            }
        //            else
        //            {
        //                List<sis_auditor_regional> list = new List<sis_auditor_regional>();
        //                regional obj = new regional();
        //                list = BusClass.GetRegionalAuditor();
        //                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

        //                foreach (var item in list)
        //                {
        //                    result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
        //                    lista3 = lista3.Concat(result).ToList();
        //                }

        //                lista = lista3;
        //                ViewBag.opcion = 2;
        //            }
        //        }
        //        else
        //        {
        //            lista = listaok;
        //            ViewBag.opcion = 1;
        //        }

        //        var query = lista.Select(item => new managmentprestadoresfacturasgestionadas3Result
        //        {
        //            id_cargue_base = item.id_cargue_base,
        //            id_cargue_dtll = item.id_cargue_dtll,
        //            prestador = item.prestador,
        //            nombre_prestador = item.nombre_prestador,
        //            homologacion_razonSocial = item.homologacion_razonSocial,
        //            homologacion_nit = item.homologacion_nit,
        //            homologacion_sap = item.homologacion_sap,
        //            num_id_prestador = item.num_id_prestador,
        //            num_factura = item.num_factura,
        //            valor_neto = item.valor_neto,
        //            estado_factura = item.estado_factura,
        //            id_ref_ciudades = item.id_ref_ciudades,
        //            ciudad = item.ciudad,
        //            id_ref_regional = item.id_ref_regional,
        //            nombre_regional = item.nombre_regional,
        //            id_auditor_asignado = item.id_auditor_asignado,
        //            nom_auditor = item.nom_auditor,
        //            id_analista_gestiona = item.id_analista_gestiona,
        //            nom_analitica = item.nom_analitica,
        //            multiusuario = item.multiusuario,
        //            id_diagnostico = item.id_diagnostico,
        //            diagnostico = item.diagnostico,
        //            fecha_recepcion_fac = item.fecha_recepcion_fac,
        //            fecha_aprobacion = item.fecha_aprobacion,
        //            estado_des = item.estado_des,
        //            count_soportes = item.count_soportes,
        //            count_soportes_zip = item.count_soportes_zip,
        //            fecha_exp_factura = item.fecha_exp_factura,
        //            tipoGastos = item.tipoGastos,
        //            ruta = item.ruta,
        //            valorGlosa = item.valorGlosa,
        //            MotivosGlosa = item.MotivosGlosa,
        //            Observaciones = item.Observaciones,
        //            estado_aprobada = item.estado_aprobada,
        //            motivos_rechazo = item.motivos_rechazo,
        //            id_factura_nueva = item.id_factura_nueva,
        //            Fecha_nueva_recepción_nueva = item.Fecha_nueva_recepción_nueva,
        //            Numero_factura_nueva = item.Numero_factura_nueva,
        //            valor_factura_nueva = item.valor_factura_nueva,
        //            estado_fac_nueva = item.estado_fac_nueva
        //        });

        //        DateTime fechaIni = Convert.ToDateTime(fechainicial);
        //        DateTime fechaFin = Convert.ToDateTime(fechafin);


        //        if (fechainicial != null)
        //        {
        //            query = query.Where(x => x.fecha_recepcion_fac >= fechaIni).ToList();
        //            query = query.Where(x => x.fecha_recepcion_fac <= fechaFin).ToList();
        //        }

        //        if (idDetalle != null)
        //        {
        //            query = query.Where(q => q.id_cargue_dtll == idDetalle);
        //        }

        //        if (!string.IsNullOrWhiteSpace(nombre_prestador))
        //        {
        //            //query = query.Where(q => q.nombre_prestador != null && q.homologacion_razonSocial != null);
        //            query = query.Where(q => q.nombre_prestador.Contains(nombre_prestador) || q.homologacion_razonSocial.Contains(nombre_prestador));
        //        }

        //        if (estado != null)
        //        {
        //            query = query.Where(q => q.estado_factura == estado);
        //        }

        //        if (regional != null)
        //        {
        //            query = query.Where(q => q.id_ref_regional == regional);
        //        }
        //        if (!string.IsNullOrWhiteSpace(nit))
        //        {
        //            query = query.Where(q => q.num_id_prestador == nit || q.homologacion_nit == nit);
        //        }
        //        if (!string.IsNullOrWhiteSpace(numFac))
        //        {
        //            query = query.Where(q => q.num_factura.Contains(numFac));
        //        }

        //        Session["ListaFacturas3"] = query.ToList();

        //        total = query.Count();
        //        if (page.HasValue && limit.HasValue)
        //        {
        //            int start = (page.Value - 1) * limit.Value;
        //            records = query.Skip(start).Take(limit.Value).ToList();
        //        }
        //        else
        //        {
        //            records = query.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult Get(int? idDetalle, DateTime? fechainicial, DateTime? fechafin, string nombre_prestador, int? estado, int? regional, string nit, string numFac, int? page, int? limit, string buscar)
        {
            string filtrosUsados = string.Empty;
            var cache = MemoryCache.Default;
            var fileContents = cache["filecontents"] as List<managmentprestadoresfacturasgestionadas3Result>;
            var model = new Models.CuentasMedicas.RadicacionElectronica();
            var listaok = new List<managmentprestadoresfacturasgestionadas3Result>();
            var records = new List<managmentprestadoresfacturasgestionadas3Result>();
            int total = 0;

            try
            {
                nombre_prestador = string.IsNullOrWhiteSpace(nombre_prestador) ? null : nombre_prestador;
                nit = string.IsNullOrWhiteSpace(nit) ? null : nit;
                numFac = string.IsNullOrWhiteSpace(numFac) ? null : numFac;

                if (!string.IsNullOrEmpty(buscar))
                {
                    var logBusqueda = new log_busquedas_tableros_facturas
                    {
                        tablero_A_consultar = "Tablero gestión facturas",
                        usuario_digita = SesionVar.UserName,
                        fecha_digita = DateTime.Now,
                        parametros_buscados = GenerarFiltrosUsados(fechainicial, fechafin, estado, regional, nombre_prestador, nit, numFac)
                    };
                    model.InsertarLogBusquedaTableros(logBusqueda, ref MsgRes);
                }

                if (fileContents != null)
                {
                    listaok = fileContents;
                }
                else
                {
                    if (numFac != null || estado.HasValue || regional.HasValue || !string.IsNullOrEmpty(nombre_prestador) || !string.IsNullOrEmpty(nit))
                    {
                        listaok = model.GetGestionFacturasv2(idDetalle, fechainicial, fechafin, estado?.ToString(), regional, nombre_prestador, nit, numFac);
                    }
                    else
                    {
                        listaok = model.GetGestionFacturas();

                        if (cache["filecontents"] == null)
                        {
                            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) };
                            cache.Add("filecontents", listaok, policy);
                        }
                    }
                }

                var listaFiltrada = FiltrarListaPorRol(listaok);
                var query = AplicarFiltros(listaFiltrada, fechainicial, fechafin, idDetalle, nombre_prestador, estado, regional, nit, numFac);

                Session["ListaFacturas3"] = query.ToList();
                total = query.Count();
                records = (page.HasValue && limit.HasValue) ? query.Skip((page.Value - 1) * limit.Value).Take(limit.Value).ToList() : query.ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        private string GenerarFiltrosUsados(DateTime? fechainicial, DateTime? fechafin, int? estado, int? regional, string nombre_prestador, string nit, string numFac)
        {
            var filtros = new List<string>();
            if (fechainicial.HasValue) filtros.Add($"Fecha recep inicial: {fechainicial:dd/MM/yyyy}");
            if (fechafin.HasValue) filtros.Add($"Fecha recep final: {fechafin:dd/MM/yyyy}");
            if (estado.HasValue) filtros.Add($"Estado del caso: {estado}");
            if (regional.HasValue) filtros.Add($"Regional: {regional}");
            if (!string.IsNullOrEmpty(nombre_prestador)) filtros.Add($"Prestador: {nombre_prestador}");
            if (!string.IsNullOrEmpty(nit)) filtros.Add($"Nit prestador: {nit}");
            if (!string.IsNullOrEmpty(numFac)) filtros.Add($"Número factura: {numFac}");
            return string.Join(" // ", filtros);
        }

        private List<managmentprestadoresfacturasgestionadas3Result> FiltrarListaPorRol(List<managmentprestadoresfacturasgestionadas3Result> listaok)
        {
            if (SesionVar.ROL == "1") return listaok;
            if (SesionVar.ROL == "21" || SesionVar.ROL == "20") return listaok;

            var regionalesUsuario = BusClass.GetRegionalAuditor().Where(x => x.id_auditor == SesionVar.IDUser).Select(x => x.id_regional).ToList();
            return listaok.Where(x => regionalesUsuario.Contains(x.id_ref_regional)).ToList();
        }

        private IEnumerable<managmentprestadoresfacturasgestionadas3Result> AplicarFiltros(IEnumerable<managmentprestadoresfacturasgestionadas3Result> query, DateTime? fechainicial, DateTime? fechafin, int? idDetalle, string nombre_prestador, int? estado, int? regional, string nit, string numFac)
        {
            if (fechainicial.HasValue) query = query.Where(x => x.fecha_recepcion_fac >= fechainicial.Value);
            if (fechafin.HasValue) query = query.Where(x => x.fecha_recepcion_fac <= fechafin.Value);
            if (idDetalle.HasValue) query = query.Where(q => q.id_cargue_dtll == idDetalle);
            if (!string.IsNullOrWhiteSpace(nombre_prestador)) query = query.Where(q => q.nombre_prestador.Contains(nombre_prestador) || q.homologacion_razonSocial.Contains(nombre_prestador));
            if (estado.HasValue) query = query.Where(q => q.estado_factura == estado);
            if (regional.HasValue) query = query.Where(q => q.id_ref_regional == regional);
            if (!string.IsNullOrWhiteSpace(nit)) query = query.Where(q => q.num_id_prestador == nit || q.homologacion_nit == nit);
            if (!string.IsNullOrWhiteSpace(numFac)) query = query.Where(q => q.num_factura.Contains(numFac));
            return query;
        }


        public ActionResult TableroGestionFacturasNumFactura()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            try
            {

                if (SesionVar.ROL != "1")
                {
                    if (SesionVar.ROL == "21")
                    {
                        ViewBag.opcion = 3;
                    }
                    else
                    {
                        ViewBag.opcion = 2;
                    }
                }
                else
                {
                    ViewBag.opcion = 1;
                }
                ViewBag.ROL = SesionVar.ROL;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public JsonResult GetNumFactura(String numFac, int? page, int? limit, string buscar)
        {

            if (numFac == "")
            {
                numFac = null;
            }

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managmentprestadoresfacturasgestionadas3_numFacturaResult> lista = new List<managmentprestadoresfacturasgestionadas3_numFacturaResult>();
            List<managmentprestadoresfacturasgestionadas3_numFacturaResult> records = new List<managmentprestadoresfacturasgestionadas3_numFacturaResult>();
            int total = 0;

            try
            {
                lista = BusClass.BuscarFactura_numFactura(numFac);
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var query = lista.Select(item => new managmentprestadoresfacturasgestionadas3_numFacturaResult
            {
                id_cargue_base = item.id_cargue_base,
                id_cargue_dtll = item.id_cargue_dtll,
                prestador = item.prestador,
                nombre_prestador = item.nombre_prestador,
                homologacion_razonSocial = item.homologacion_razonSocial,
                homologacion_nit = item.homologacion_nit,
                homologacion_sap = item.homologacion_sap,
                num_id_prestador = item.num_id_prestador,
                num_factura = item.num_factura,
                valor_neto = item.valor_neto,
                estado_factura = item.estado_factura,
                id_ref_ciudades = item.id_ref_ciudades,
                ciudad = item.ciudad,
                id_ref_regional = item.id_ref_regional,
                nombre_regional = item.nombre_regional,
                id_auditor_asignado = item.id_auditor_asignado,
                nom_auditor = item.nom_auditor,
                id_analista_gestiona = item.id_analista_gestiona,
                nom_analitica = item.nom_analitica,
                multiusuario = item.multiusuario,
                id_diagnostico = item.id_diagnostico,
                diagnostico = item.diagnostico,
                fecha_recepcion_fac = item.fecha_recepcion_fac,
                fecha_aprobacion = item.fecha_aprobacion,
                estado_des = item.estado_des,
                count_soportes = item.count_soportes,
                count_soportes_zip = item.count_soportes_zip,
                fecha_exp_factura = item.fecha_exp_factura,
                tipoGastos = item.tipoGastos,
                ruta = item.ruta,
                valorGlosa = item.valorGlosa,
                MotivosGlosa = item.MotivosGlosa,
                Observaciones = item.Observaciones,
                estado_aprobada = item.estado_aprobada,
                motivos_rechazo = item.motivos_rechazo,
                id_factura_nueva = item.id_factura_nueva,
                Fecha_nueva_recepción_nueva = item.Fecha_nueva_recepción_nueva,
                Numero_factura_nueva = item.Numero_factura_nueva,
                valor_factura_nueva = item.valor_factura_nueva,
                estado_fac_nueva = item.estado_fac_nueva,
                nroPedido = item.nroPedido,
                numero_contrato = item.numero_contrato
            });


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

            return this.Json(new
            {
                records,
                total
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportarCarta(int? idcargue)
        {
            List<vw_recep_facturas_cargue_base> lst = BusClass.GetHistoricoRadicacionById((int)idcargue);
            List<ManagmentFacturasV2Result> facturas = BusClass.GetFacturasByRecepcionV2((int)idcargue);

            try
            {
                //RUTA REPORTE
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptCartaFacturas.rdlc");

                decimal? valortotal = facturas.Select(l => l.valor_neto).Sum();

                ReportParameter p1 = new ReportParameter("Contador", facturas.Count().ToString());
                ReportParameter p2 = new ReportParameter("Valortotal", valortotal.ToString());

                //ASIGNAICON  DATASET A REPORT
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("FacturaCargueBaseDataSet", lst);
                Microsoft.Reporting.WebForms.ReportDataSource rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("FacturasCargueDetalleDataSet", facturas);


                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.DataSources.Add(rds2);
                viewer.LocalReport.SetParameters(p1);
                viewer.LocalReport.SetParameters(p2);


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


                        //return File(pdfContent, "application/pdf", idcargue + "_" + DateTime.Now + ".pdf");
                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                    }
                }
                else
                {
                    Response.Write("<script language=javascript>alert('No se han encontrado resultados');</script>");
                }
            }
            catch (Exception ex)
            {
                var errorMensaje = ex.Message;

                Response.Write("<script language=javascript>alert('No se han encontrado resultados');</script>");
            }

            return File(new byte[0], "application/pdf");
        }

        public FileContentResult ExportarLstFacturasGestionadas2(DateTime? fechainicial, DateTime? fechafin, String nombre_prestador, Int32? estado, int? regional, String nit, String numFac, string descarga)
        {
            ExcelPackage package = new ExcelPackage();
            List<managmentprestadoresfacturasgestionadas3Result> ListaSession = (List<managmentprestadoresfacturasgestionadas3Result>)Session["ListaFacturas3"];
            List<managmentprestadoresfacturasgestionadas3Result> result2 = new List<managmentprestadoresfacturasgestionadas3Result>();

            if (ListaSession.Count != 0)
            {
                result2 = ListaSession.ToList();
            }

            var fileDownloadName = String.Format("Consolidado" + Convert.ToDateTime(DateTime.Now) + ".xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            try
            {
                // Pass your ef data to method
                package = GenerateExcelFileFacturasGestionadas(result2.ToList());
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        public void ExportarFacturasMasivamente2(DateTime? fechainicial, DateTime? fechafin, String nombre_prestador, Int32? estado, int? regional, String nit, String numFac, string descarga)
        {
            try
            {
                List<managmentprestadoresfacturasgestionadas3Result> ListaSession = (List<managmentprestadoresfacturasgestionadas3Result>)Session["ListaFacturas3"];
                List<managmentprestadoresfacturasgestionadas3Result> result2 = new List<managmentprestadoresfacturasgestionadas3Result>();


                if (ListaSession.Count != 0)
                {
                    result2 = ListaSession.ToList();
                }

                result2 = result2.Where(x => x.estado_factura != 0).ToList();
                if (result2.Count == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('No se han encontrado resultados');" +
                    "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {

                    using (ZipFile zip = new ZipFile())
                    {
                        int count = 0;
                        foreach (var item in result2)
                        {
                            try
                            {
                                WebClient User = new WebClient();
                                string filename = item.ruta;
                                Byte[] FileBuffer = User.DownloadData(filename);
                                //Binary binary2 = item.ruta;
                                byte[] array = FileBuffer.ToArray();
                                zip.AddEntry(item.num_factura + ".pdf", array);
                            }
                            catch (Exception ex)
                            {

                            }

                            count++;
                        }

                        using (MemoryTributary salida = new MemoryTributary())
                        {
                            zip.Save(salida);

                            Response.Clear();
                            Response.BufferOutput = false;
                            Response.ContentType = "application/zip";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=Facturas" + ".zip");
                            Response.BinaryWrite(salida.ToArray());
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        [HttpPost]
        public ActionResult TableroFacturasAprobadas(int idcargue, int idfactura, int accion)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {

                    if (accion == 1)
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 4 Where id_af = @idfact";
                    }
                    else
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 3, observaciones = 'Factura no autorizada' Where id_af = @idfact";
                    }
                    command.Parameters.AddWithValue("@idfact", idfactura);
                    connection.Open();
                    command.CommandTimeout = 120;
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                if (accion == 1)
                {
                    ViewData["rta"] = 1;
                    ViewData["msj"] = "Factura autorizada exitosamente";
                }
            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["msj"] = ex.Message;
            }

            List<vw_auditores_totales> list = new List<vw_auditores_totales>();

            list = Model.GetAuditorTotales(ref MsgRes);
            ViewBag.Auditores = list;

            List<managmentprestadoresfacturasestadoResult> result = Model.GetFacturasByEstado(2, ref MsgRes);
            return View(result);
        }

        public JsonResult SaveFacturasAprobadas(int idcargue, int idfactura, int accion, Int32? idAuditor)
        {

            String mensaje = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {

                    DateTime fecha = Convert.ToDateTime(DateTime.Now);
                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 4,id_auditor_asignado =@idAuditor, fecha_asignacion_auditor =@fecha Where id_af = @idfact";

                    command.Parameters.AddWithValue("@idfact", idfactura);
                    command.Parameters.AddWithValue("@idAuditor", idAuditor);
                    command.Parameters.AddWithValue("@fecha", fecha);
                    connection.Open();
                    command.CommandTimeout = 120;
                    command.ExecuteNonQuery();
                    connection.Close();
                }


                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {

                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }
        }


        public JsonResult AsignarAuditorFactura(int idcargue, int idfactura, int accion, Int32? idAuditor)
        {

            String mensaje = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {

                    DateTime fecha = Convert.ToDateTime(DateTime.Now);
                    command.CommandText = "UPDATE rips_af_cargue_dtll SET id_auditor_asignado =@idAuditor, fecha_asignacion_auditor =@fecha Where id_af = @idfact";

                    command.Parameters.AddWithValue("@idfact", idfactura);
                    command.Parameters.AddWithValue("@idAuditor", idAuditor);
                    command.Parameters.AddWithValue("@fecha", fecha);
                    connection.Open();
                    command.CommandTimeout = 120;
                    command.ExecuteNonQuery();
                    connection.Close();
                }


                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {

                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult SaveFacturasAprobadasNA(int idcargue, int idfactura, int accion, Int32? idAuditor)
        {

            String mensaje = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {

                    DateTime fecha = Convert.ToDateTime(DateTime.Now);
                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 6,id_auditor_asignado =@idAuditor, fecha_asignacion_auditor =@fecha,fecha_aprobacion = @fecha_aprobacion Where id_af = @idfact";

                    command.Parameters.AddWithValue("@idfact", idfactura);
                    command.Parameters.AddWithValue("@idAuditor", idAuditor);
                    command.Parameters.AddWithValue("@fecha", fecha);
                    command.Parameters.AddWithValue("@fecha_aprobacion", fecha);
                    connection.Open();
                    command.CommandTimeout = 120;
                    command.ExecuteNonQuery();
                    connection.Close();
                }


                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {

                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }
        }

        public PartialViewResult GestionarAuditor(Int32? ID, Int32? ID2, Int32? regional, int? tipo)
        {

            //tipo 2; actualziar auditor desde tablero facturas aprobadas 2

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<vw_auditores_totales> list = new List<vw_auditores_totales>();

            list = Model.GetAuditorTotales(ref MsgRes);
            list = list.Where(x => x.id_regional == regional).ToList();

            ViewBag.Auditores = list;
            ViewBag.idcargue = ID;
            ViewBag.idfactura = ID2;
            ViewBag.regional = regional;
            ViewBag.tipo = tipo;

            return PartialView(Model);
        }

        public JsonResult ValidarFirma(string usuario, String regional, Models.CuentasMedicas.RadicacionElectronica Model)
        {
            var mensaje = 0;
            var auditor = Convert.ToInt32(usuario);

            var BaseImagen = Model.GetFirmas(auditor);
            if (BaseImagen != null)
            {
                mensaje = 1;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<vw_auditores_totales> list = new List<vw_auditores_totales>();

                list = Model.GetAuditorTotales(ref MsgRes);
                list = list.Where(x => x.id_regional == Convert.ToInt32(regional)).ToList();
                return Json(list.Select(p => new { id_usuario = p.id_usuario, nombre = p.nombre }), JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult VerArchivoDocumentoDigital(int tipo, int idCargueBase, int idCargueDtll)
        {
            managment_prestadores_facturas_GDResult obj = new managment_prestadores_facturas_GDResult();

            try
            {
                Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                obj = Model.GetFacturaGD(idCargueDtll, ref MsgRes);

                if (obj == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }

                else
                {

                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "application/pdf";
                    string nombre = Path.GetFileName(obj.ruta);

                    if (System.IO.File.Exists(dirpath))
                    {
                        return File(dirpath, extension, nombre);
                    }
                    else
                    {
                        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }


        public PartialViewResult DocumentoDigital(int tipo, int idcarguebase, int idcarguedtll)
        {
            ViewBag.tipo = tipo;
            ViewBag.idcarguebase = idcarguebase;
            ViewBag.idcarguedtll = idcarguedtll;

            return PartialView();
        }

        public ActionResult Verdocumentodigital(int tipo, int idcarguebase, int idcarguedtll)
        {
            managment_prestadores_facturas_GDResult obj = new managment_prestadores_facturas_GDResult();
            try
            {
                Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                obj = Model.GetFacturaGD(idcarguedtll, ref MsgRes);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                if (System.IO.File.Exists(dirpath))
                {
                    return File(dirpath, "application/pdf");
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message });
            }
        }

        //public void Verdocumentodigital_zip(int idcarguedtll)
        //{
        //    try
        //    {
        //        Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
        //        managment_prestadores_facturas_GD_zipResult obj = Model.GetFacturaGD2(idcarguedtll, ref MsgRes);

        //        string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);

        //        using (FileStream fs = new FileStream(obj.ruta, FileMode.Open))
        //        {
        //            //response is HttpListenerContext.Response... 
        //            Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
        //            Response.AppendHeader("content-disposition", "attachment; filename=" + "Factura:" + obj.num_factura + ".Zip");
        //            byte[] buffer = new byte[64 * 1024];
        //            int read;
        //            while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
        //            {
        //                Response.OutputStream.Write(buffer, 0, read);
        //                Response.OutputStream.Flush(); //seems to have no effect
        //            }
        //            Response.OutputStream.Close();
        //        }
        //        Response.BufferOutput = false;
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        string rta = "<script LANGUAGE='JavaScript'>window.alert('Ha ocurrido un error al momento de visualizar el archivo. Por favor comuniquese con el administrador);</script>";
        //        Response.Write(rta);
        //        Response.End();
        //    }
        //}

        public void Verdocumentodigital_zip2(int? idcarguedtll)
        {
            var ruta = "";
            var extension = "";

            try
            {
                Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                managment_prestadores_facturas_GD_zipResult obj = Model.GetFacturaGD2((int)idcarguedtll, ref MsgRes);
                ruta = obj.ruta;

                if (!string.IsNullOrEmpty(ruta))
                {
                    byte[] bytes;
                    using (FileStream file = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);
                    }
                    byte[] array = bytes.ToArray();

                    if (array != null)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();

                        if (ruta.Contains(".rar"))
                        {
                            extension = "application/x-rar-compressed";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=facturacion_" + idcarguedtll + ".rar");
                        }
                        else if (ruta.Contains(".zip"))
                        {
                            extension = "application/zip";
                            Response.AppendHeader("Content-Disposition", "attachment;  filename=facturacion_" + idcarguedtll + ".zip");
                        }

                        Response.ContentType = extension;
                        Response.BinaryWrite(array);
                        Response.Flush();
                    }
                }
                else

                {
                    Response.Write("<script language=javascript>alert('No contiene documento');</script>");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Response.Write("<script language=javascript>alert('Error" + error + "');</script>");
            }
        }

        public void Verdocumentodigital_zip(int? idcarguedtll)
        {
            try
            {
                var ruta = "";
                Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                managment_prestadores_facturas_GD_zipResult obj = Model.GetFacturaGD2((int)idcarguedtll, ref MsgRes);
                ruta = obj.ruta;

                if (!string.IsNullOrEmpty(ruta))
                {
                    if (System.IO.File.Exists(ruta))
                    {
                        var extension = Path.GetExtension(ruta);
                        var contentType = "";

                        switch (extension.ToLower())
                        {
                            case ".rar":
                                contentType = "application/x-rar-compressed";
                                break;
                            case ".zip":
                                contentType = "application/zip";
                                break;

                            case ".7z":
                                contentType = "application/zip";
                                break;
                        }

                        if (!string.IsNullOrEmpty(contentType))
                        {
                            Response.Clear();
                            Response.ContentType = contentType;
                            Response.AppendHeader("Content-Disposition", $"attachment; filename=facturacion_{idcarguedtll}{extension}");
                            Response.TransmitFile(ruta);
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('El archivo no existe');</script>");
                    }
                }
                else
                {
                    Response.Write("<script language=javascript>alert('No contiene documento');</script>");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Response.Write($"<script language=javascript>alert('Error {error}');</script>");
            }
        }

        public PartialViewResult SoportesClinicos(int idsoporteclinico)
        {
            ViewBag.idsoporteclinico = idsoporteclinico;
            return PartialView();
        }

        public ActionResult versoporteclinico(int idsoporteclinico)
        {
            try
            {
                Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                management_prestadores_get_soporteResult obj = Model.Getsoporteclinico(idsoporteclinico);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);

                //if (System.IO.File.Exists(dirpath))
                //{
                //    byte[] bytes = System.IO.File.ReadAllBytes(dirpath);
                //    return File(bytes, "application/pdf");
                //}
                //else
                //{
                //    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
                //}


                if (System.IO.File.Exists(dirpath))
                {
                    string extension = Path.GetExtension(dirpath).ToLower(); // Obtener extensión del archivo
                    string contentType;
                    bool isInline = false; // Para previsualizar en el navegador

                    switch (extension)
                    {
                        case ".pdf":
                            contentType = "application/pdf";
                            isInline = true; // Se previsualiza
                            break;
                        case ".zip":
                            contentType = "application/zip";
                            break;
                        default:
                            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Formato de archivo no soportado." });
                    }

                    byte[] bytes = System.IO.File.ReadAllBytes(dirpath);

                    Response.Clear();
                    Response.ContentType = contentType;
                    Response.AddHeader("Content-Length", bytes.Length.ToString());

                    if (isInline)
                    {
                        // Previsualización del PDF
                        Response.AddHeader("Content-Disposition", "inline; filename=" + Path.GetFileName(dirpath));
                    }
                    else
                    {
                        // Descarga del ZIP
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(dirpath));
                    }

                    Response.BinaryWrite(bytes);
                    Response.End();
                    return new EmptyResult();
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
                }



            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message });
            }
        }

        public string tablasoportesclinicos(int idcargue, int iddetalle)
        {
            string result = "";

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managment_prestadores_soportes_clinicosResult> lst = Model.GetSoportesClinicosList(idcargue, iddetalle);

            int i = 0;
            foreach (var item in lst)
            {
                i += 1;
                result += "<tr>";
                result += "<td>" + i + "</td>";
                result += "<td>" + item.nom_documento + "</td>";
                result += "<td><a href='javascript:AbrirSoporteClinico(" + item.id_recep_soportes_clinicos + ")'>Ver documento</a></td>";
                result += "</tr>";
            }

            return result;
        }

        public string tablasoportesTotales(int iddetalle)
        {
            string result = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            try
            {
                List<managment_prestadores_documentosResult> lst = Model.GetSoportesList(iddetalle);
                lst = lst.OrderBy(x => x.num_tipo).ToList();

                if (lst.Count > 0)
                {
                    int i = 0;
                    foreach (var item in lst)
                    {
                        i += 1;
                        result += "<tr>";
                        result += "<td>" + i + "</td>";
                        result += "<td>" + item.tipo + "</td>";
                        result += "<td>" + item.nombre + "</td>";
                        if (item.tipo != "ZIP" && (item.tipo != "SUBSANACIÓN" && item.tipo != "7z"))
                        {
                            result += "<td><a href='javascript:AbrirSoporteClinico2(" + item.Id_gestion_documental + "," + item.id_cargue_dtll + ")'>Ver documento</a></td>";
                        }
                        else
                        {
                            result += "<td><a href='javascript:AbrirSoporteClinicoZip(" + item.id_cargue_dtll + ")'>Ver documento Zip</a></td>";
                        }

                        result += "</tr>";
                    }
                }
                else
                {
                    result += "<tr>";
                    result += "<td class='text-center' colspan='4'>No se han encontrado documentos para esta factura.</td>";
                    result += "</tr>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public ActionResult TableroFacturasAuditor()
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturasAuditorResult> result = Model.GetFacturasByAuditor(4, SesionVar.IDUser, ref MsgRes);

            //if (SesionVar.ROL != "1")
            //{
            //    result = result.Where(x => x.id_auditor_asignado == SesionVar.IDUser).ToList();
            //}
            ViewBag.count = result.Count();
            return View(result);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 3 de noviembre de 2022
        /// Metodo utilizado para limpiar lo que hay en chache del resultado de facturas auditor y refrescar el tablero
        /// </summary>
        /// <returns></returns>
        public ActionResult RefrescarTableroFacturasAuditor2()
        {
            try
            {
                string nomUsuario = SesionVar.UserName;
                ObjectCache cache = MemoryCache.Default;
                List<managmentprestadoresFacturasAuditorOKCompletaResult> ListadoFacturasAuditor = cache["facturasAuditor" + nomUsuario] as List<managmentprestadoresFacturasAuditorOKCompletaResult>;

                if (ListadoFacturasAuditor != null)
                {
                    if (ListadoFacturasAuditor.Count > 0)
                    {
                        cache.Remove("facturasAuditor" + nomUsuario);
                        cache.Remove("tiempoExpiracionMemoria" + nomUsuario);
                    }
                }
                return RedirectToAction("TableroFacturasAuditor2", "RadicacionElectonica");
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + error });
            }
        }

        public ActionResult TableroFacturasAuditor2(int? desdeAuditor)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            Session["facturasSeleccionadas"] = new List<managmentprestadoresFacturasAuditorOKResult>();

            ViewBag.desdeAuditor = desdeAuditor;

            return View(Model);
        }

        //public JsonResult GetAuditores([DataSourceRequest] DataSourceRequest request, int? opc)
        //{

        //    Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();


        //    List<managmentprestadoresFacturasAuditorOKResult> ListaTotal = (List<managmentprestadoresFacturasAuditorOKResult>)Session["ListaFacturas2"];
        //    List<managmentprestadoresFacturasAuditorOKResult> listaok = new List<managmentprestadoresFacturasAuditorOKResult>();

        //    listaok = Model.GetFacturasByAuditor2(ref MsgRes);
        //    listaok = listaok.Where(x => x.id_auditor_asignado == SesionVar.IDUser).ToList();

        //    Session["ListaFacturas2"] = listaok.ToList();

        //    ViewBag.fecha = Convert.ToDateTime(DateTime.Now);

        //    var lista = new object();

        //    lista = (from item in listaok
        //             select new
        //             {
        //                 id_cargue_dtll = item.id_cargue_dtll,
        //                 id_cargue_base = item.id_cargue_base,
        //                 num_factura = item.num_factura,
        //                 valor_neto = item.valor_neto,
        //                 nombre_prestador = item.nombre_prestador,
        //                 dias_trascurridos = item.dias_trascurridos,
        //                 fecha_ultima_gestion = item.fecha_ultima_gestion.Value.ToString("dd/MM/yyyy"),
        //                 id_regional = item.id_ref_regional,
        //                 analista = item.nom_analitica,
        //                 auditor = item.nom_auditor,


        //             }).Distinct().OrderByDescending(f => f.dias_trascurridos);


        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetAuditores([DataSourceRequest] DataSourceRequest request, int? opc)
        {
            string nomUsuario = SesionVar.UserName;

            ObjectCache cache = MemoryCache.Default;

            List<managmentprestadoresFacturasAuditorOKCompletaResult> ListadoFacturasAuditor = cache["facturasAuditor" + nomUsuario] as List<managmentprestadoresFacturasAuditorOKCompletaResult>;

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managmentprestadoresFacturasAuditorOKCompletaResult> listaok = new List<managmentprestadoresFacturasAuditorOKCompletaResult>();

            var idAuditor = SesionVar.IDUser;
            try
            {
                if (ListadoFacturasAuditor == null || ListadoFacturasAuditor.Count() == 0)
                {
                    /*Consulta en base de datos*/
                    listaok = Model.GetFacturasByAuditor3(idAuditor);

                    /*Se definen las politicas y se insertan en cache los resultados de la consulta hecha a base de datos*/
                    DateTime expirationDate = DateTime.Now.AddHours(10);
                    CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expirationDate };
                    cache.Set("facturasAuditor" + nomUsuario, listaok, policy);
                    cache.Set("tiempoExpiracionMemoria" + nomUsuario, expirationDate, policy);
                }
                else
                {
                    /*Se setea en el listado de resultados lo que aun hay en cache*/
                    listaok = ListadoFacturasAuditor;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["ListaFacturas2"] = listaok.ToList();

            ViewBag.fecha = Convert.ToDateTime(DateTime.Now);

            var lista = new object();

            lista = (from item in listaok
                     select new
                     {
                         id_cargue_dtll = item.id_cargue_dtll,
                         id_cargue_base = item.id_cargue_base,
                         num_factura = item.num_factura,
                         valor_neto = item.valor_neto,
                         nombre_prestador = item.nombre_prestador,
                         dias_trascurridos = item.dias_trascurridos,
                         fecha_ultima_gestion = item.fecha_ultima_gestion.Value.ToString("dd/MM/yyyy"),
                         id_regional = item.id_ref_regional,
                         analista = item.nom_analitica,
                         auditor = item.nom_auditor,
                         version = item.version

                     }).Distinct().OrderByDescending(f => f.dias_trascurridos);


            ViewBag.ConteoListadoFac = listaok.Count();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargarFirmasami()
        {
            List<sis_usuario> usuarios = new List<sis_usuario>();
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            try
            {
                usuarios = BusClass.GetUsuarios();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.usuario = usuarios;

            return View(Model);
        }

        //public PartialViewResult GestionarFactura(Int32? ID)
        //{

        //    Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
        //    //List<managmentprestadoresFacturasResult> result = Model.GetFacturasByEstadoAceptadas(4, ref MsgRes);
        //    List<managmentprestadoresFacturasAuditorOKResult> result = (List<managmentprestadoresFacturasAuditorOKResult>)Session["ListaFacturas2"];

        //    result = result.Where(x => x.id_cargue_dtll == ID).ToList();

        //    ViewBag.listgastos = BusClass.Getref_tipo_gasto_facturas(ref MsgRes).OrderBy(x => Convert.ToInt32(x.descripcion.Substring(0, 2))).ToList();
        //    ViewBag.listCie10 = BusClass.GetCie10Unido();

        //    foreach (var item in result)
        //    {
        //        ViewBag.numFactura = item.num_factura;
        //        ViewBag.prestador = item.nombre_prestador;
        //        ViewBag.Vlrfactura = item.valor_neto;
        //        ViewBag.id_cargue_dtll = item.id_cargue_dtll;
        //    }

        //    return PartialView(Model);
        //}

        public PartialViewResult GestionarFactura(Int32? ID)
        {
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturasAuditorOKCompletaResult> result = cache["facturasAuditor" + nomUsuario] as List<managmentprestadoresFacturasAuditorOKCompletaResult>;

            result = result.Where(x => x.id_cargue_dtll == ID).ToList();

            ViewBag.listgastos = BusClass.Getref_tipo_gasto_facturas(ref MsgRes).OrderBy(x => Convert.ToInt32(x.descripcion.Substring(0, 2))).ToList();
            ViewBag.listCie10 = BusClass.GetCie10Unido();

            foreach (var item in result)
            {
                ViewBag.numFactura = item.num_factura;
                ViewBag.prestador = item.nombre_prestador;
                ViewBag.Vlrfactura = item.valor_neto;
                ViewBag.id_cargue_dtll = item.id_cargue_dtll;
            }

            return PartialView(Model);
        }

        public JsonResult ValidarExistenciaFirmaDigitalSami()
        {
            bool firmaDigital = (bool)Session["firmaDigital"];
            if (firmaDigital)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, message = "ERROR... Usuario no puede gestionar porque no tiene firma digital en SAMI." }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SavegestionFacturas(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            var detalle = Model.DetalleGasto;
            String mensaje = "", Alerta = "";

            var idDetalle = Model.id_cargue_dtll;

            if (detalle == null)
            {
                if (Model.factura_autorizada == "NO")
                {
                    Alerta = "NO";
                }
                else
                {
                    Alerta = "SI";
                }
            }
            else
            {
                Alerta = "NA";
            }

            if (Alerta != "SI")
            {
                try
                {
                    ecop_gestion_factura_digital obj = new ecop_gestion_factura_digital();
                    obj.id_cargue_dtll = Model.id_cargue_dtll;
                    obj.multiusuario = Convert.ToString(Model.multiusuario);
                    obj.id_dx1 = Model.id_dx1;
                    obj.gasto = Model.gasto;
                    obj.factura_autorizada = Model.factura_autorizada;
                    obj.fecha_digita = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_digita = Convert.ToString(SesionVar.UserName);
                    obj.aplica_auditoria = true;
                    Model.id_gestion_factura_digital = Model.InsertarGestionFacturadigital(obj, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        if (Alerta == "NA")
                        {
                            List<ecop_gestion_factura_digital_gasto> listadoGasto = new List<ecop_gestion_factura_digital_gasto>();

                            foreach (var item in detalle)
                            {
                                ecop_gestion_factura_digital_gasto objg = new ecop_gestion_factura_digital_gasto();
                                objg.id_ref_tipo_gasto_facturas = item.id_gasto;
                                objg.id_gestion_factura_digital = Model.id_gestion_factura_digital;
                                objg.observacion_gasto = item.obs_gasto;
                                listadoGasto.Add(objg);
                            }

                            if (listadoGasto.Count > 0)
                            {
                                Model.insertarListadoGestionFacturadigitalGasto(listadoGasto, ref MsgRes);
                            }
                        }

                        mensaje = "SE INGRESO CORRECTAMENTE....";

                        if (Model.factura_autorizada == "SI")
                        {

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            using (SqlCommand command = connection.CreateCommand())
                            {
                                command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 6, fecha_aprobacion =@fecha_aprobacion Where id_af = @idfact";
                                command.Parameters.AddWithValue("@idfact", Model.id_cargue_dtll);
                                command.Parameters.AddWithValue("@fecha_aprobacion", Convert.ToDateTime(DateTime.Now));
                                connection.Open();
                                command.CommandTimeout = 120;
                                command.ExecuteNonQuery();
                                connection.Close();
                            }


                            /*En este apartado se limpiara la factura que se gestionó de la memoria cache*/
                            List<managmentprestadoresFacturasAuditorOKCompletaResult> ListadoFacturasAuditorEnCache = cache["facturasAuditor" + nomUsuario] as List<managmentprestadoresFacturasAuditorOKCompletaResult>;
                            if (ListadoFacturasAuditorEnCache != null)
                            {
                                var itemCache = ListadoFacturasAuditorEnCache.Where(l => l.id_cargue_dtll == Model.id_cargue_dtll).FirstOrDefault();
                                if (itemCache != null)
                                {
                                    String key = "tiempoExpiracionMemoria" + nomUsuario;
                                    DateTime expiracionCache = CheckCachedExpiry(key);
                                    CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expiracionCache };

                                    ListadoFacturasAuditorEnCache.Remove(itemCache);
                                    cache.Set("facturasAuditor" + nomUsuario, ListadoFacturasAuditorEnCache, policy);
                                    cache.Set("tiempoExpiracionMemoria" + nomUsuario, expiracionCache, policy);
                                }
                            }
                            idDetalle = Model.id_cargue_dtll;

                            return Json(new { success = true, message = mensaje, id = idDetalle, opc = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, message = mensaje, id = idDetalle, opc = 2 }, JsonRequestBehavior.AllowGet);
                        }
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
            else
            {
                mensaje = "*ERROR.... INGRESE MINIMO UN GASTO.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }


        }

        /// <summary>
        /// Metodo para la creacion de cartas de aprobacion
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>

        public ActionResult CrearPdfFacturasDigreal(Int32? ID)
        {
            try
            {
                Models.Reportes.Reportes Model = new Models.Reportes.Reportes();
                Models.InicioSesion.Usuarios Model2 = new Models.InicioSesion.Usuarios();

                string filename2 = "";
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital2.rdlc");
                string strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];


                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromHours(1);

                List<managmentprestadoresFacturasReporteResult> lst = Model.GetFacturasByEstadoReporte(ID.Value, ref MsgRes);
                String id_base = Convert.ToString(lst.FirstOrDefault().id_cargue_base);
                String id_analista = Convert.ToString(lst.FirstOrDefault().id_analista_gestiona);
                String id_auditor = Convert.ToString(lst.FirstOrDefault().id_auditor_asignado);
                String id_factura = Convert.ToString(lst.FirstOrDefault().id_gestion_factura_digital);
                String id_detalle = Convert.ToString(lst.FirstOrDefault().id_cargue_dtll);

                id_base = Model.AgregarValor(id_base) + id_base;
                id_analista = Model.AgregarValor2(id_analista) + id_analista;
                id_auditor = Model.AgregarValor2(id_auditor) + id_auditor;
                id_factura = Model.AgregarValor(id_factura) + id_factura;

                var msg = id_detalle;

                ecop_firma_digital_cod_barras ObjFirma = new ecop_firma_digital_cod_barras();
                using (MemoryStream file = new MemoryStream())
                {
                    file.Write(RsaFileDemo.Encriptar(msg, id_detalle), 0, RsaFileDemo.Encriptar(msg, id_detalle).Length);
                    ObjFirma.llave_firma = file.ToArray();
                    ObjFirma.fecha_digita = Convert.ToDateTime(DateTime.Now);
                    ObjFirma.usuario_digita = Convert.ToString(SesionVar.UserName);
                }

                httpClient.Timeout = TimeSpan.FromHours(1);

                var id_firma_digital = Model2.InsertarFirmadigital(ObjFirma, ref MsgRes);
                id_base = Model.AgregarValor(id_base) + id_base;
                string msg2 = id_base + "," + Convert.ToString(id_firma_digital);


                String valorEncriptado = null;
                using (MemoryStream file = new MemoryStream())
                {
                    file.Write(RsaFileDemo.LaunchDemo(msg, id_detalle), 0, RsaFileDemo.LaunchDemo(msg, id_detalle).Length);
                    valorEncriptado = BitConverter.ToString(file.ToArray()).Replace("-", "");
                }

                string cadena = (lst.FirstOrDefault().num_factura + ","
               + lst.FirstOrDefault().valor_neto + ","
               + lst.FirstOrDefault().num_id_prestador + ","
               + valorEncriptado + ","
               + id_detalle);

                int? analista = lst.FirstOrDefault().id_analista_gestiona;
                ecop_firma_digital_sami BaseImagen = Model.GetFirmas(analista);

                string Imagen = "";

                if (BaseImagen != null)
                {
                    if (BaseImagen.firma_digital != null)
                    {
                        Imagen = Convert.ToBase64String(BaseImagen.firma_digital.ToArray());
                        BaseImagen = null;
                    }
                    else
                    {
                        BaseImagen = null;
                    }
                }

                httpClient.Timeout = TimeSpan.FromHours(1);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                byte[] ImgByte = null;
                MemoryStream fileUniversal = new MemoryStream();

                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(fileUniversal, System.Drawing.Imaging.ImageFormat.Png);
                    ImgByte = fileUniversal.ToArray();
                }

                string Imagen2 = "";
                if (ImgByte != null)
                {
                    Imagen2 = Convert.ToBase64String(ImgByte.ToArray());
                    ImgByte = null;
                }
                fileUniversal.Dispose();

                int? auditor = lst.FirstOrDefault().id_auditor_asignado;

                ecop_firma_digital_sami BaseImagen2 = Model.GetFirmas(auditor);

                string Imagen3 = "";

                if (BaseImagen2.firma_digital != null)
                {
                    Imagen3 = Convert.ToBase64String(BaseImagen2.firma_digital.ToArray());
                    BaseImagen2 = null;
                }

                string filename = "CartaUsuarios" + lst.FirstOrDefault().num_factura;

                httpClient.Timeout = TimeSpan.FromHours(1);

                Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
                Microsoft.Reporting.WebForms.ReportParameter imagen2 = new Microsoft.Reporting.WebForms.ReportParameter("Imagen2", Imagen2);
                Microsoft.Reporting.WebForms.ReportParameter imagen3 = new Microsoft.Reporting.WebForms.ReportParameter("Imagen3", Imagen3);
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("FirmasDatSet", lst);

                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.SetParameters(imagen);
                viewer.LocalReport.SetParameters(imagen2);
                viewer.LocalReport.SetParameters(imagen3);
                viewer.LocalReport.Refresh();

                string mimeType;
                string encoding;
                string fileNameExtension;
                string[] streams;
                Microsoft.Reporting.WebForms.Warning[] warnings;
                byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                String carpeta = "FacturasAprobadas", SubCarpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    SubCarpeta = "Facturacion";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    SubCarpeta = "FacturacionPruebas";
                }
                DateTime fecha = DateTime.Now;

                string ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + SubCarpeta + "\\" + ID.Value);
                string nombre = Path.GetFileNameWithoutExtension("Aprobada" + ID.Value);
                filename2 = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta, fecha, nombre, Path.GetExtension(".pdf"));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                System.IO.File.WriteAllBytes(filename2, pdfContent);

                ecop_gestion_facturas_aprobadas obj = new ecop_gestion_facturas_aprobadas();
                obj.id_cargue_dtll = ID.Value;
                obj.ruta = filename2;
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);

                httpClient.Timeout = TimeSpan.FromHours(1);

                int resultadoInsercionCarta = Model.InsertarFacturaAprobadas(obj, ref MsgRes);
                if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                {

                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de insertar la carta:" + MsgRes.DescriptionResponse });
                }

                int bytesRead;
                byte[] buffer = new byte[4096];
                using (Stream stream1 = new MemoryStream(pdfContent))
                {
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=" + nombre + ".pdf");
                    Response.ContentType = "application/pdf";
                    while ((bytesRead = stream1.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        Response.OutputStream.Write(buffer, 0, bytesRead);
                        Response.Flush();
                    }
                }

                Response.End();

                pdfContent = null;
                streams = null;
                imagen = null;
                imagen2 = null;
                imagen3 = null;
                lst = null;

                httpClient.Timeout = TimeSpan.FromHours(1);

                return File(filename2, "application/pdf");
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                //CrearPdfFacturasDig(ID);
                return RedirectToAction("ControlErrores", "Usuario", new
                {
                    Mensaje = "Ha ocurrido un error al momento de generar la carta de aprobación." + ex.Message
                }); ;
            }
        }

        //public ActionResult CrearPdfFacturasDig(Int32? ID)
        //{
        //    try
        //    {
        //        List<management_ecop_firma_digital_sami_todoResult> listado = new List<management_ecop_firma_digital_sami_todoResult>();
        //        listado = BusClass.ListadoFirmasSinRuta();

        //        if (listado.Count() > 0)
        //        {
        //            for (var i = false; i == false;)
        //            {
        //                var retorno = ActualizarRutaFirmasDigitales();
        //                if (retorno != "")
        //                {
        //                    i = true;
        //                }
        //            }
        //        }

        //        Models.Reportes.Reportes Model = new Models.Reportes.Reportes();
        //        Models.InicioSesion.Usuarios Model2 = new Models.InicioSesion.Usuarios();

        //        string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital2.rdlc");
        //        string strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];

        //        List<managmentprestadoresFacturasReporteResult> lst = new List<managmentprestadoresFacturasReporteResult>();
        //        lst = Model.GetFacturasByEstadoReporte(ID.Value, ref MsgRes);

        //        var id_base = "";
        //        var id_detalle = "";

        //        if (lst.Count() > 0)
        //        {
        //            id_base = Model.AgregarValor(lst.FirstOrDefault().id_cargue_base.ToString()) + lst.FirstOrDefault().id_cargue_base;
        //            id_detalle = lst.FirstOrDefault().id_cargue_dtll.ToString();
        //        }

        //        //var id_analista = Model.AgregarValor2(lst.FirstOrDefault().id_analista_gestiona.ToString()) + lst.FirstOrDefault().id_analista_gestiona;
        //        //var id_auditor = Model.AgregarValor2(lst.FirstOrDefault().id_auditor_asignado.ToString()) + lst.FirstOrDefault().id_auditor_asignado;
        //        //var id_factura = Model.AgregarValor(lst.FirstOrDefault().id_gestion_factura_digital.ToString()) + lst.FirstOrDefault().id_gestion_factura_digital;

        //        var msg = id_detalle;
        //        var encryptedMsg = RsaFileDemo.Encriptar(msg, id_detalle);
        //        ecop_firma_digital_cod_barras ObjFirma = new ecop_firma_digital_cod_barras
        //        {
        //            llave_firma = encryptedMsg,
        //            fecha_digita = DateTime.Now,
        //            usuario_digita = SesionVar.UserName.ToString()
        //        };

        //        var id_firma_digital = Model2.InsertarFirmadigital(ObjFirma, ref MsgRes);

        //        var msg2 = id_base + "," + Convert.ToString(id_firma_digital);
        //        var encryptedMsg2 = RsaFileDemo.LaunchDemo(msg, id_detalle);
        //        var valorEncriptado = BitConverter.ToString(encryptedMsg2).Replace("-", "");

        //        var cadena = lst.FirstOrDefault().num_factura + ","
        //                   + lst.FirstOrDefault().valor_neto + ","
        //                   + lst.FirstOrDefault().num_id_prestador + ","
        //                   + valorEncriptado + ","
        //                   + id_detalle;

        //        var analista = lst.FirstOrDefault().id_analista_gestiona;
        //        var BaseImagen = Model.GetFirmasId(analista);

        //        //var Imagen = BaseImagen?.firma_digital != null ? Convert.ToBase64String(BaseImagen.firma_digital.ToArray()) : "";

        //        if (BaseImagen == null)
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de insertar la carta: El analista no tiene firma." });
        //        }

        //        var Imagen = BaseImagen.ruta;
        //        var ImagenAuditor = BaseImagen.ruta;

        //        //if (string.IsNullOrEmpty(Imagen))
        //        //{
        //        //    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "El auditor asignado no posee firma en SAMI, por favor solicitarla." }); ;
        //        //}

        //        //var qrGenerator = new QRCodeGenerator();
        //        //var qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
        //        //var qrCode = new QRCode(qrCodeData);

        //        //byte[] ImgByte;
        //        //using (var fileUniversal = new MemoryStream())
        //        //{
        //        //    using (var bitMap = qrCode.GetGraphic(20))
        //        //    {
        //        //        bitMap.Save(fileUniversal, System.Drawing.Imaging.ImageFormat.Png);
        //        //        ImgByte = fileUniversal.ToArray();
        //        //    }
        //        //}

        //        //var Imagen2 = ImgByte != null ? Convert.ToBase64String(ImgByte.ToArray()) : "";


        //        string Imagen2 = null;

        //        var qrGenerator = new QRCodeGenerator();
        //        var qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
        //        var qrCode = new QRCode(qrCodeData);

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            qrCode.GetGraphic(20).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
        //            Imagen2 = Convert.ToBase64String(memoryStream.ToArray());
        //        }

        //        var auditor = lst.FirstOrDefault().id_auditor_asignado;
        //        var BaseImagen2 = Model.GetFirmasId(auditor);

        //        if (BaseImagen2 == null)
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de insertar la carta: El auditor no tiene firma." });
        //        }

        //        //var Imagen3 = BaseImagen2?.firma_digital != null ? Convert.ToBase64String(BaseImagen2.firma_digital.ToArray()) : "";
        //        var Imagen3 = BaseImagen2.ruta;

        //        //if (string.IsNullOrEmpty(Imagen3))
        //        //{
        //        //    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "El analista asignado no posee firma en SAMI, por favor solicitarla." }); ;
        //        //}

        //        var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
        //        viewer.ProcessingMode = ProcessingMode.Local;

        //        viewer.LocalReport.ReportPath = rPath;
        //        viewer.LocalReport.DataSources.Clear();
        //        viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FirmasDatSet", lst));

        //        viewer.LocalReport.EnableExternalImages = true;

        //        viewer.LocalReport.SetParameters(new[]
        //        {
        //            new Microsoft.Reporting.WebForms.ReportParameter("Imagen", @"file:///" + Imagen),
        //            new Microsoft.Reporting.WebForms.ReportParameter("Imagen2", Imagen2),
        //            new Microsoft.Reporting.WebForms.ReportParameter("Imagen3", @"file:///" + Imagen3),
        //            //new Microsoft.Reporting.WebForms.ReportParameter("Imagen4", @"file:///" + ImagenAuditor)
        //        });

        //        viewer.LocalReport.Refresh();

        //        string mimeType;
        //        string encoding;
        //        string fileNameExtension;
        //        string[] streams;
        //        Microsoft.Reporting.WebForms.Warning[] warnings;
        //        var pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        //        var carpeta = "FacturasAprobadas";
        //        var SubCarpeta = ConfigurationManager.AppSettings["BDActiva"].ToString() == "1" ? "Facturacion" : "FacturacionPruebas";
        //        var fecha = DateTime.Now;
        //        var ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva, carpeta, SubCarpeta, ID.Value.ToString());
        //        var nombre = "Aprobada" + ID.Value;
        //        var filename2 = $"{ruta}\\{fecha:yyyyMMdd_hhmm}_{nombre}.pdf";

        //        Directory.CreateDirectory(ruta);
        //        System.IO.File.WriteAllBytes(filename2, pdfContent);

        //        var obj = new ecop_gestion_facturas_aprobadas
        //        {
        //            id_cargue_dtll = ID.Value,
        //            ruta = filename2,
        //            fecha_ingreso = DateTime.Now
        //        };

        //        var resultadoInsercionCarta = Model.InsertarFacturaAprobadas(obj, ref MsgRes);
        //        if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de insertar la carta:" + MsgRes.DescriptionResponse });
        //        }

        //        //Response.ClearContent();
        //        //Response.AddHeader("content-disposition", "attachment;filename=" + nombre + ".pdf");
        //        //Response.ContentType = "application/pdf";
        //        //Response.BinaryWrite(pdfContent);
        //        //Response.End();

        //        return File(pdfContent, "application/pdf");
        //        //return File(filename2, "application/pdf");
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        //CrearPdfFacturasDig(ID);
        //        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar la carta de aprobación." + ex.Message }); ;
        //    }
        //}


        //public ActionResult CrearPdfFacturasDig(int? ID)
        //{
        //    if (!ID.HasValue)
        //    {
        //        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "ID no proporcionado." });
        //    }

        //    try
        //    {
        //        var firmasSinRuta = BusClass.ListadoFirmasSinRuta();

        //        if (firmasSinRuta.Any())
        //        {
        //            while (true)
        //            {
        //                var retorno = ActualizarRutaFirmasDigitales();
        //                if (!string.IsNullOrEmpty(retorno))
        //                {
        //                    break;
        //                }
        //            }
        //        }

        //        var model = new Models.Reportes.Reportes();
        //        var usuariosModel = new Models.InicioSesion.Usuarios();

        //        string reportPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital2.rdlc");
        //        string rutaDocumentosAprobados = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];

        //        var facturas = model.GetFacturasByEstadoReporte(ID.Value, ref MsgRes);

        //        if (!facturas.Any())
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se encontraron facturas." });
        //        }

        //        var factura = facturas.First();
        //        var idBase = model.AgregarValor(factura.id_cargue_base.ToString()) + factura.id_cargue_base;
        //        var idDetalle = factura.id_cargue_dtll.ToString();
        //        var valorEncriptado = BitConverter.ToString(RsaFileDemo.LaunchDemo(idDetalle, idDetalle)).Replace("-", "");

        //        var cadena = string.Join(",", factura.num_factura, factura.valor_neto, factura.num_id_prestador, valorEncriptado, idDetalle);
        //        var analista = factura.id_analista_gestiona;

        //        var firmaAnalista = model.GetFirmasId(analista);
        //        if (firmaAnalista == null)
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "El analista no tiene firma." });
        //        }

        //        var qrGenerator = new QRCodeGenerator();
        //        var qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
        //        var qrCode = new QRCode(qrCodeData);

        //        string imagenQr;
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            qrCode.GetGraphic(20).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
        //            imagenQr = Convert.ToBase64String(memoryStream.ToArray());
        //        }

        //        var auditor = factura.id_auditor_asignado;
        //        var firmaAuditor = model.GetFirmasId(auditor);
        //        if (firmaAuditor == null)
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "El auditor no tiene firma." });
        //        }

        //        var viewer = new ReportViewer
        //        {
        //            ProcessingMode = ProcessingMode.Local,
        //            LocalReport =
        //                {
        //                    ReportPath = reportPath
        //                }
        //        };

        //        viewer.LocalReport.DataSources.Clear();
        //        viewer.LocalReport.DataSources.Add(new ReportDataSource("FirmasDatSet", facturas));
        //        viewer.LocalReport.EnableExternalImages = true;
        //        viewer.LocalReport.SetParameters(new[]
        //        {
        //            new ReportParameter("Imagen", @"file:///" + firmaAnalista.ruta),
        //            new ReportParameter("Imagen2", imagenQr),
        //            new ReportParameter("Imagen3", @"file:///" + firmaAuditor.ruta),
        //        });

        //        viewer.LocalReport.Refresh();

        //        var pdfContent = viewer.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streams, out Microsoft.Reporting.WebForms.Warning[] warnings);

        //        var subCarpeta = ConfigurationManager.AppSettings["BDActiva"] == "1" ? "Facturacion" : "FacturacionPruebas";
        //        var ruta = Path.Combine(Request.PhysicalApplicationPath, rutaDocumentosAprobados, "FacturasAprobadas", subCarpeta, ID.Value.ToString());
        //        var filename = $"{ruta}\\{DateTime.Now:yyyyMMdd_hhmm}_Aprobada{ID.Value}.pdf";

        //        Directory.CreateDirectory(ruta);
        //        System.IO.File.WriteAllBytes(filename, pdfContent);

        //        var facturaAprobada = new ecop_gestion_facturas_aprobadas
        //        {
        //            id_cargue_dtll = ID.Value,
        //            ruta = filename,
        //            fecha_ingreso = DateTime.Now
        //        };

        //        var resultadoInsercion = model.InsertarFacturaAprobadas(facturaAprobada, ref MsgRes);
        //        if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Error al insertar la carta: " + MsgRes.DescriptionResponse });
        //        }

        //        return File(pdfContent, "application/pdf");
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Error al generar la carta de aprobación: " + ex.Message });
        //    }
        //}

        public ActionResult CrearPdfFacturasDig(int? ID)
        {
            if (!ID.HasValue)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "ID no proporcionado." });
            }

            try
            {
                var firmasSinRuta = BusClass.ListadoFirmasSinRuta();

                if (firmasSinRuta.Any())
                {
                    while (true)
                    {
                        var retorno = ActualizarRutaFirmasDigitales();
                        if (!string.IsNullOrEmpty(retorno))
                        {
                            break;
                        }
                    }
                }

                var model = new Models.Reportes.Reportes();
                var usuariosModel = new Models.InicioSesion.Usuarios();

                string reportPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital_fis.rdlc");
                string rutaDocumentosAprobados = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];

                var facturas = model.GetFacturasByEstadoReporte_fis(ID.Value, ref MsgRes);

                if (!facturas.Any())
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se encontraron facturas." });
                }

                var factura = facturas.First();
                var idBase = model.AgregarValor(factura.id_cargue_base.ToString()) + factura.id_cargue_base;
                var idDetalle = factura.id_cargue_dtll.ToString();
                var valorEncriptado = BitConverter.ToString(RsaFileDemo.LaunchDemo(idDetalle, idDetalle)).Replace("-", "");

                var cadena = string.Join(",", factura.num_factura, factura.valor_neto, factura.num_id_prestador, idDetalle, factura.valorNotaCredito, factura.valor_total_factura_conGlosa, valorEncriptado);
                var analista = factura.id_analista_gestiona;

                var firmaAnalista = model.GetFirmasId(analista);
                if (firmaAnalista == null)
                {
                    //return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "El analista no tiene firma." });
                }

                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                string imagenQr;
                using (var memoryStream = new MemoryStream())
                {
                    qrCode.GetGraphic(20).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    imagenQr = Convert.ToBase64String(memoryStream.ToArray());
                }

                var auditor = factura.id_auditor_asignado;
                var firmaAuditor = model.GetFirmasId(auditor);
                if (firmaAuditor == null)
                {
                    //return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "El auditor no tiene firma." });
                }

                var viewer = new ReportViewer
                {
                    ProcessingMode = ProcessingMode.Local,
                    LocalReport =
                        {
                            ReportPath = reportPath
                        }
                };

                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(new ReportDataSource("FirmasFisDatSet", facturas));
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.SetParameters(new[]
                {
                    new ReportParameter("Imagen", @"file:///" + firmaAnalista.ruta),
                    new ReportParameter("Imagen2", imagenQr),
                    new ReportParameter("Imagen3", @"file:///" + firmaAuditor.ruta),
                });

                viewer.LocalReport.Refresh();

                var pdfContent = viewer.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streams, out Microsoft.Reporting.WebForms.Warning[] warnings);

                var subCarpeta = ConfigurationManager.AppSettings["BDActiva"] == "1" ? "Facturacion" : "FacturacionPruebas";
                var ruta = Path.Combine(Request.PhysicalApplicationPath, rutaDocumentosAprobados, "FacturasAprobadas", subCarpeta, ID.Value.ToString());
                var filename = $"{ruta}\\{DateTime.Now:yyyyMMdd_hhmm}_Aprobada{ID.Value}.pdf";

                Directory.CreateDirectory(ruta);
                System.IO.File.WriteAllBytes(filename, pdfContent);

                var facturaAprobada = new ecop_gestion_facturas_aprobadas
                {
                    id_cargue_dtll = ID.Value,
                    ruta = filename,
                    fecha_ingreso = DateTime.Now
                };

                var resultadoInsercion = model.InsertarFacturaAprobadas(facturaAprobada, ref MsgRes);
                if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Error al insertar la carta: " + MsgRes.DescriptionResponse });
                }

                return File(pdfContent, "application/pdf");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Error al generar la carta de aprobación: " + ex.Message });
            }
        }


        public string ActualizarRutaFirmasDigitales()
        {
            var retorno = "";
            List<ecop_firma_digital_sami> listado = new List<ecop_firma_digital_sami>();

            try
            {
                listado = BusClass.listaFirmasUsuarios();
                if (listado.Count() > 0)
                {
                    foreach (var item in listado)
                    {
                        WebClient User = new WebClient();
                        Binary binary = item.firma_digital;
                        byte[] array = binary.ToArray();
                        HttpPostedFileBase sigFile = (HttpPostedFileBase)new CustomPostedFile(array, item.id_usuario + ".jpg");

                        string strRetorno = string.Empty;
                        StringBuilder sbRutaDefinitiva;
                        string strRutaDefinitiva = string.Empty;
                        strRutaDefinitiva = ConfigurationManager.AppSettings["rutaFirmasDigitales"];
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
                            carpeta = "FirmasDigitales";
                        }
                        else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                        {
                            carpeta = "FirmasDigitalesPruebas";
                        }

                        ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "idUsuario_" + item.id_firmas_sami);
                        var nombre = Path.GetFileNameWithoutExtension(sigFile.FileName);
                        archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                        fecha, nombre, Path.GetExtension(sigFile.FileName));

                        if (!Directory.Exists(ruta))
                            Directory.CreateDirectory(ruta);

                        string filename2 = archivo;

                        using (FileStream fs = new FileStream(filename2, FileMode.Create))
                        {
                            fs.Write(array, 0, array.Length);
                        }

                        if (Directory.Exists(ruta))
                        {
                            BusClass.ActualizarRutaFirmasDigital(archivo, item.id_firmas_sami);
                        }
                    }
                    retorno = "Aprobado";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return retorno;
        }

        public string DevolverRutaFirmasDigitalesIndividual(int? idUsuario, Binary firmaDigital)
        {
            var retorno = "";

            try
            {

                WebClient User = new WebClient();
                Binary binary = firmaDigital;
                byte[] array = binary.ToArray();
                HttpPostedFileBase sigFile = (HttpPostedFileBase)new CustomPostedFile(array, idUsuario + ".jpg");

                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;
                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaFirmasDigitales"];
                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + sigFile.FileName);
                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "idUsuario_" + idUsuario);

                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                string strError = string.Empty;

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "FirmasDigitales";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "FirmasDigitalesPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "idUsuario_" + idUsuario);
                var nombre = Path.GetFileNameWithoutExtension(sigFile.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(sigFile.FileName));


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                string filename2 = archivo;

                using (FileStream fs = new FileStream(filename2, FileMode.Create))
                {
                    fs.Write(array, 0, array.Length);
                }
                retorno = archivo;
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return retorno;
        }

        public class CustomPostedFile : HttpPostedFileBase
        {
            private readonly byte[] fileBytes;
            MemoryStream stream;

            public CustomPostedFile(byte[] fileBytes, string filename = null)
            {
                this.fileBytes = fileBytes;
                this.FileName = filename;
                this.ContentType = "image/jpeg";
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

        /// <summary>
        /// Segundo metodo para la creacion de cartas de aprobacion
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>

        public ActionResult CrearPdfFacturasDigAprobadas(Int32? ID)
        {
            try
            {
                MemoryStream fileUniversal = new MemoryStream();

                Models.Reportes.Reportes Model = new Models.Reportes.Reportes();
                Models.InicioSesion.Usuarios Model2 = new Models.InicioSesion.Usuarios();

                string filename2 = "";
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital2.rdlc");
                string strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];


                List<managmentprestadoresFacturasReporteResult> lst = Model.GetFacturasByEstadoReporte(ID.Value, ref MsgRes);
                String id_base = Convert.ToString(lst.FirstOrDefault().id_cargue_base);
                String id_analista = Convert.ToString(lst.FirstOrDefault().id_analista_gestiona);
                String id_auditor = Convert.ToString(lst.FirstOrDefault().id_auditor_asignado);
                String id_factura = Convert.ToString(lst.FirstOrDefault().id_gestion_factura_digital);
                String id_detalle = Convert.ToString(lst.FirstOrDefault().id_cargue_dtll);

                id_base = Model.AgregarValor(id_base) + id_base;
                id_analista = Model.AgregarValor2(id_analista) + id_analista;
                id_auditor = Model.AgregarValor2(id_auditor) + id_auditor;
                id_factura = Model.AgregarValor(id_factura) + id_factura;

                var msg = id_detalle;

                fileUniversal = new MemoryStream();
                fileUniversal.Write(RsaFileDemo.Encriptar(msg, id_detalle), 0, RsaFileDemo.Encriptar(msg, id_detalle).Length);

                ecop_firma_digital_cod_barras ObjFirma = new ecop_firma_digital_cod_barras();
                ObjFirma.llave_firma = fileUniversal.ToArray();
                ObjFirma.fecha_digita = Convert.ToDateTime(DateTime.Now);
                ObjFirma.usuario_digita = Convert.ToString(SesionVar.UserName);
                var id_firma_digital = Model2.InsertarFirmadigital(ObjFirma, ref MsgRes);
                id_base = Model.AgregarValor(id_base) + id_base;
                string msg2 = id_base + "," + Convert.ToString(id_firma_digital);
                fileUniversal.Dispose();


                fileUniversal = new MemoryStream();
                fileUniversal.Write(RsaFileDemo.LaunchDemo(msg, id_detalle), 0, RsaFileDemo.LaunchDemo(msg, id_detalle).Length);
                String valorEncriptado = BitConverter.ToString(fileUniversal.ToArray()).Replace("-", "");
                fileUniversal.Dispose();

                string cadena = (lst.FirstOrDefault().num_factura + ","
               + lst.FirstOrDefault().valor_neto + ","
               + lst.FirstOrDefault().num_id_prestador + ","
               + valorEncriptado + ","
               + id_detalle);

                int? analista = lst.FirstOrDefault().id_analista_gestiona;
                ecop_firma_digital_sami BaseImagen = Model.GetFirmas(analista);

                string Imagen = "";

                if (BaseImagen != null)
                {
                    if (BaseImagen.firma_digital != null)
                    {
                        Imagen = Convert.ToBase64String(BaseImagen.firma_digital.ToArray());
                        BaseImagen = null;
                    }
                    else
                    {
                        BaseImagen = null;
                    }
                }

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                byte[] ImgByte = null;
                fileUniversal = new MemoryStream();

                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(fileUniversal, System.Drawing.Imaging.ImageFormat.Png);
                    ImgByte = fileUniversal.ToArray();
                }

                string Imagen2 = "";
                if (ImgByte != null)
                {
                    Imagen2 = Convert.ToBase64String(ImgByte.ToArray());
                    ImgByte = null;
                }
                fileUniversal.Dispose();

                int? auditor = lst.FirstOrDefault().id_auditor_asignado;

                ecop_firma_digital_sami BaseImagen2 = Model.GetFirmas(auditor);

                string Imagen3 = "";

                if (BaseImagen2.firma_digital != null)
                {
                    Imagen3 = Convert.ToBase64String(BaseImagen2.firma_digital.ToArray());
                    BaseImagen2 = null;
                }

                string filename = "CartaUsuarios" + lst.FirstOrDefault().num_factura;

                Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
                Microsoft.Reporting.WebForms.ReportParameter imagen2 = new Microsoft.Reporting.WebForms.ReportParameter("Imagen2", Imagen2);
                Microsoft.Reporting.WebForms.ReportParameter imagen3 = new Microsoft.Reporting.WebForms.ReportParameter("Imagen3", Imagen3);
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("FirmasDatSet", lst);

                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.SetParameters(imagen);
                viewer.LocalReport.SetParameters(imagen2);
                viewer.LocalReport.SetParameters(imagen3);
                viewer.LocalReport.Refresh();

                string mimeType;
                string encoding;
                string fileNameExtension;
                string[] streams;
                Microsoft.Reporting.WebForms.Warning[] warnings;
                byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                String carpeta = "FacturasAprobadas", SubCarpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    SubCarpeta = "Facturacion";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    SubCarpeta = "FacturacionPruebas";
                }
                DateTime fecha = DateTime.Now;

                string ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + SubCarpeta + "\\" + ID.Value);
                string nombre = Path.GetFileNameWithoutExtension("Aprobada" + ID.Value);
                filename2 = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta, fecha, nombre, Path.GetExtension(".pdf"));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                System.IO.File.WriteAllBytes(filename2, pdfContent);

                ecop_gestion_facturas_aprobadas obj = new ecop_gestion_facturas_aprobadas();
                obj.id_cargue_dtll = ID.Value;
                obj.ruta = filename2;
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);

                int resultadoInsercionCarta = Model.InsertarFacturaAprobadas(obj, ref MsgRes);
                if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                {

                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de insertar la carta:" + MsgRes.DescriptionResponse });
                }

                int bytesRead;
                byte[] buffer = new byte[4096];
                using (Stream stream1 = new MemoryStream(pdfContent))
                {
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=ConsultaCenso" + DateTime.Now + ".csv");
                    Response.ContentType = "application/pdf";
                    while ((bytesRead = stream1.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        Response.OutputStream.Write(buffer, 0, bytesRead);
                        Response.Flush();
                        Response.End();
                    }
                }

                pdfContent = null;
                streams = null;
                imagen = null;
                imagen2 = null;
                imagen3 = null;
                lst = null;

                return File(filename2, "application/pdf");
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return RedirectToAction("ControlErrores", "Usuario", new
                {
                    Mensaje = "Ha ocurrido un error al momento de generar la carta de aprobación." + ex.Message
                });
            }

        }


        public JsonResult SaveFirmaSami(HttpPostedFileBase file, int? idUsuario)
        {

            String mensaje = "";
            String tipoAlerta = "";
            Models.InicioSesion.Usuarios Model = new Models.InicioSesion.Usuarios();

            try
            {
                byte[] Img = null;
                MemoryStream target = new MemoryStream();
                Request.Files["file"].InputStream.CopyTo(target);
                Img = target.ToArray();

                ecop_firma_digital_sami obj = new ecop_firma_digital_sami();

                //obj.id_usuario = Convert.ToInt32(SesionVar.IDUser);
                obj.id_usuario = idUsuario;

                obj.firma_digital = Img;
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                obj.usuario_ingreso = SesionVar.UserName;
                obj.ruta = DevolverRutaFirmasDigitalesIndividual(idUsuario, Img);

                Model.InsertarFirmadigitalsami(obj, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {

                    ActualizarRutaFirmasDigitales();

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
                var error = ex.Message;
                mensaje = "*--ERRRO EN EL CARGUE DE LA FIRMA--*";
                tipoAlerta = "3";
                return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult GestionarFacRechazadas(Int32? ID, Int32? ID2)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            //List<managmentprestadoresFacturasResult> result = Model.GetFacturasByEstadoAceptadas(4, ref MsgRes);

            //List<ref_rechazos_Fac> list = new List<ref_rechazos_Fac>();

            //Model.AllGroups = Model.Getref_rechazos_Fac(ref MsgRes);

            //Model.UserGroups = Model.Getref_rechazos_Fac(ref MsgRes);

            //Model.SelectedGroups = Model.UserGroups.Select(x => x.id_ref_rechazos_Fac).ToArray();


            //list = Model.Getref_rechazos_Fac(ref MsgRes);

            //ViewBag.List = list;

            ViewBag.id_cargue = ID;
            ViewBag.id_dtll = ID2;

            return PartialView(Model);
        }

        public JsonResult SavegestionFacturasRechazadas(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            ManagementPrestadoresFacturasByIdDtllResult factura = Model.GetInfoFacturaById(Model.id_cargue_dtll);
            ecop_firma_digital_sami firma = new ecop_firma_digital_sami();

            try
            {
                firma = BusClass.traerDatosFirma(SesionVar.IDUser);

                if (firma == null)
                {
                    throw new Exception("El usuario no se encuentra con firma digital en SAMI. Por favor solicitarla");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {

                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 3, observaciones = @obs, tiene_rechazo = @tiene_rechazo, id_analista_gestiona = @id_analista_gestiona  Where id_af = @id";
                    command.Parameters.AddWithValue("@obs", Model.obs);
                    command.Parameters.AddWithValue("@tiene_rechazo", "SI");
                    command.Parameters.AddWithValue("@id_analista_gestiona", SesionVar.IDUser);
                    command.Parameters.AddWithValue("@id", Model.id_cargue_dtll);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    var Cadena = Model.SelectedList;

                    foreach (var item in Cadena.ToList())
                    {
                        FacturasRechazadasDetalle(Model.id_cargue_dtll, Convert.ToInt32(item));
                    }

                    if (factura != null)
                    {
                        EnviarCorreoFacturaRechazada(Model.id_cargue_dtll, factura.correo, ref MsgRes);
                    }

                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll }, JsonRequestBehavior.AllowGet);

                }


            }
            catch (Exception ex)
            {

                mensaje = "*---ERROR -- - *" + ex.Message;


                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }
        }

        public void EnviarCorreoFacturaRechazada(int idcarguedtll, string correo, ref MessageResponseOBJ MsgRes)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            RadicacionElectronica Model = new RadicacionElectronica();
            ManagementPrestadoresFacturasByIdDtllResult factura = Model.GetInfoFacturaById(idcarguedtll);

            try
            {
                string bdactiva = ConfigurationManager.AppSettings["BDActiva"];
                string mailBody = "";
                string mailCSS = "";
                string textBody = string.Format("SAMI Informa: {1}{0}  {2}{0}  {3}{0}  {4}{0}Link Ingreso:  {5}", Environment.NewLine, string.Empty, string.Empty, "Cordial saludo. Se ha rechazado la factura No  " + factura.num_factura + " del prestador " + factura.nombre_prestador + ". Debe ingresar al tablero de facturas rechazadas para poder ver los motivos del rechazo, y a su vez subsanar la factura. Puede ingresar al aplicativo, y realizar la gestión correspondiente por la siguiente url: ", string.Empty, "https://prestadores.aplicativoasalud.co"); ;

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
                mailBody += "Cordialmente,";
                mailBody += "</div>";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<STRONG>Asalud SAS </STRONG>";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
                mailBody += "<br />";
                mailBody += "Dir. Calle 96 # 13a-03 Piso 4";
                mailBody += "<br />";
                mailBody += "Bogotá";
                mailBody += "</div>";

                mailBody += "<br />";
                mailBody += "<div id='RightPane2' align='center'  style='font-size: 10.5px;'>";
                mailBody += "El contenido de este mensaje electrónico, su texto o elementos adjuntos, son de uso confidencial y privado entre el remitente y su (s) destinatario (s). Cualquier uso o distribución indebida o sin autorización escrita por parte del remitente, se encuentran estrictamente prohibidas y acarrean sanciones penales. Por tanto, en caso de recibir este mensaje por error, por favor notificarlo y devolverlo de inmediato al remitente del mismo, sin guardar copia alguna...";
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
                mailMessage.To.Clear();

                if (bdactiva == "1")
                {
                    mailMessage.To.Add("notificaciones@asaludltda.com");

                    if (!string.IsNullOrEmpty(correo))
                    {
                        mailMessage.To.Add(correo);
                    }
                }
                else
                {
                    mailMessage.To.Add("sami.soporte@asalud.co");
                    //mailMessage.To.Add("notificaciones@asalud.co");
                }

                mailMessage.Subject = "[Rechazo de la factura No " + factura.num_factura + "]";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
            }
        }

        public JsonResult SavegestionLoteFacturasRechazadas(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            int idcargue = Convert.ToInt32(Request.Form["id_cargue"]);
            ecop_firma_digital_sami firma = new ecop_firma_digital_sami();

            try
            {
                firma = BusClass.traerDatosFirma(SesionVar.IDUser);

                if (firma == null)
                {
                    throw new Exception("El usuario no se encuentra con firma digital en SAMI. Por favor solicitarla");
                }

                List<managment_prestadores_facturasResult> result = Model.GetFacturasByIdRecepcion(idcargue, ref MsgRes);

                int count = 0;
                if (result.Count > 0)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        var Cadena = Model.SelectedList;
                        foreach (var factura in result)
                        {
                            count += 1;
                            command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 3, observaciones = @obss_" + count + ", tiene_rechazo = @tiene_rechazo_" + count + ", id_analista_gestiona = @id_analista_gestiona_" + count + "  Where id_af = @id_" + count + "";
                            command.Parameters.AddWithValue("@obss_" + count, Model.obs);
                            command.Parameters.AddWithValue("@tiene_rechazo_" + count, "SI");

                            command.Parameters.AddWithValue("@id_analista_gestiona_" + count, SesionVar.IDUser);
                            command.Parameters.AddWithValue("@id_" + count, factura.id_cargue_dtll);

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();

                            /*Inserta el detalle de la factura que se esta rechazaondo en la tabla rips_af_cargue_base_dtll_rechazos */
                            LoteFacturasRechazadasDetallev2(idcargue, factura.id_cargue_dtll);

                            foreach (var item in Cadena.ToList())
                            {
                                FacturasRechazadasDetalle(factura.id_cargue_dtll, Convert.ToInt32(item));
                            }
                        }
                        foreach (var item in Cadena.ToList())
                        {
                            LoteFacturasRechazadasDetalle(idcargue, Convert.ToInt32(item));
                        }
                        //CrearPdfFacturasDigRechazadas(Model.id_cargue_dtll);
                        mensaje = "SE INGRESO CORRECTAMENTE....";
                        return Json(new { success = true, message = mensaje, id = idcargue }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    mensaje = "NO HAY FACTURAS DISPONIBLES PARA RECHAZAR ....";
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

        public void FacturasRechazadasDetalle(Int32? ID, Int32? ID2)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                var fecha = Convert.ToDateTime(DateTime.Now);
                command.CommandText = "INSERT INTO rips_af_cargue_dtll_rechazos(id_af,id_ref_rechazos_Fac,fecha_ingreso)values(@id_af,@id_ref_rechazos_Fac,@fecha_ingreso)";
                command.Parameters.AddWithValue("@id_af", ID);
                command.Parameters.AddWithValue("@fecha_ingreso", fecha);
                command.Parameters.AddWithValue("@id_ref_rechazos_Fac", ID2);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }

        }

        public void LoteFacturasRechazadasDetalle(Int32? idcargue, Int32? iditem)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                var fecha = Convert.ToDateTime(DateTime.Now);

                command.CommandText = "INSERT INTO rips_af_cargue_base_rechazos(id_cargue_base,id_ref_rechazo_fac,fecha_ingreso,usuario_digita)values(@id_cargue,@id_dtll,@fecha, @usuario)";
                command.Parameters.AddWithValue("@id_cargue", idcargue);
                command.Parameters.AddWithValue("@id_dtll", iditem);
                command.Parameters.AddWithValue("@fecha", DateTime.Now);
                command.Parameters.AddWithValue("@usuario", SesionVar.UserName);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void LoteFacturasRechazadasDetallev2(Int32? idcargue, Int32? idfactura)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                var fecha = Convert.ToDateTime(DateTime.Now);

                command.CommandText = "INSERT INTO rips_af_cargue_base_dtll_rechazos(id_cargue_base,id_af,fecha_ingreso,usuario_digita)values(@id_cargue,@id_dtll,@fecha, @usuario)";
                command.Parameters.AddWithValue("@id_cargue", idcargue);
                command.Parameters.AddWithValue("@id_dtll", idfactura);
                command.Parameters.AddWithValue("@fecha", DateTime.Now);
                command.Parameters.AddWithValue("@usuario", SesionVar.UserName);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public ActionResult CrearPdfFacturasDigRechazadas(int? ID)
        {
            try
            {
                //RUTA REPORTE
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptfacDigRechazo2.rdlc");

                Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

                List<managmentRechazoFacturasReporteResult> lst = new List<managmentRechazoFacturasReporteResult>();
                lst = Model.GetFacturasByRechazoReporte((int)ID, ref MsgRes);

                if (lst.Count() == 0)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido generar la carta de aprobación" });
                }

                var analista = lst.FirstOrDefault().id_analista_gestiona;

                var BaseImagen = Model.GetFirmas(analista);

                string Imagen = "";

                if (BaseImagen != null)
                {
                    Imagen = Convert.ToBase64String(BaseImagen.firma_digital.ToArray());
                }

                Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetRechazo", lst);

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

                        //Response.ClearContent();
                        //Response.ClearHeaders();
                        //Response.Clear();

                        //Response.BufferOutput = false;
                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("content-length", pdfContent.Length.ToString());
                        //Response.BinaryWrite(pdfContent);
                        //Response.Flush();

                        return File(pdfContent, "application/pdf", "FacturasDigRechaza" + DateTime.Now + ".pdf");

                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;

                        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido generar la carta de aprobación: " + MsgRes.DescriptionResponse });
                    }
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido generar la carta de aprobación" });
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido generar la carta de aprobación: " + error });
            }

        }

        public void CrearPdfLoteFacturasDigRechazadas(Int32? ID)
        {
            try
            {
                //RUTA REPORTE
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptfacDigRechazo3.rdlc");

                Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

                List<managmentRechazoLoteFacturasReporteResult> lst = new List<managmentRechazoLoteFacturasReporteResult>();
                List<managmentRechazoLoteDtllFacturasReporteResult> lst2 = new List<managmentRechazoLoteDtllFacturasReporteResult>();
                lst = Model.GetLoteFacturasByRechazoReporte(ID.Value, ref MsgRes);
                lst2 = Model.GetLoteFacturasdtllByRechazoReporte(ID.Value, ref MsgRes);

                var analista = lst.FirstOrDefault().id_analista;

                var BaseImagen = Model.GetFirmas(analista);

                string Imagen = "";

                if (BaseImagen != null)
                {
                    Imagen = Convert.ToBase64String(BaseImagen.firma_digital.ToArray());
                }

                ReportParameter p1 = new ReportParameter("idcarguebase", ID.Value.ToString());

                Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetRechazo2", lst);
                Microsoft.Reporting.WebForms.ReportDataSource rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetRechazo2Dtll", lst2);

                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.DataSources.Add(rds2);
                viewer.LocalReport.SetParameters(imagen);
                viewer.LocalReport.SetParameters(p1);

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

                        Response.BufferOutput = false;
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

        public PartialViewResult GestionarLoteFacRechazadas(Int32? ID)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managmentprestadoresFacturasResult> result = Model.GetFacturasByEstadoAceptadas(4, ref MsgRes);
            List<ref_rechazos_Fac> list = new List<ref_rechazos_Fac>();

            Model.AllGroups = Model.Getref_rechazos_Fac(ref MsgRes);

            Model.UserGroups = Model.Getref_rechazos_Fac(ref MsgRes);

            Model.SelectedGroups = Model.UserGroups.Select(x => x.id_ref_rechazos_Fac).ToArray();

            list = Model.Getref_rechazos_Fac(ref MsgRes);

            ViewBag.List = list;

            ViewBag.id_cargue = ID;

            return PartialView(Model);
        }

        public PartialViewResult VerRechazo(string ID, String IDBASE)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            //managment_prestadores_facturasResult factura = Model.GetFactura(Convert.ToInt32(IDBASE), Convert.ToInt32(ID), ref MsgRes);
            List<managment_prestadores_facturasResult> factura = Model.GetFactura(Convert.ToInt32(IDBASE), Convert.ToInt32(ID), ref MsgRes);
            Model.id_cargue_dtll = Convert.ToInt32(ID);
            factura = factura.Where(x => x.id_cargue_dtll == Int32.Parse(ID)).ToList();

            ViewBag.List = Model.GetFacturasByRechazoList(Model.id_cargue_dtll, ref MsgRes);

            foreach (var item in factura)
            {
                ViewBag.observaciones = item.observaciones;
            }

            return PartialView(Model);
        }

        public PartialViewResult VerDevolucion(string ID)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            Model.id_cargue_dtll = Convert.ToInt32(ID);

            ViewBag.List = Model.GetDetalleFacturadevuletas_FIS(Model.id_cargue_dtll);

            return PartialView(Model);
        }

        public ActionResult TableroFacturasAuditorAprobadas()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturasAprobadasResult> result = Model.GetFacturasAprobadas(0, ref MsgRes);

            if (SesionVar.ROL != "1")
            {
                result = result.Where(x => x.id_auditor_asignado == SesionVar.IDUser || x.id_analista_gestiona == SesionVar.IDUser).ToList();
            }

            return View(result);
        }

        public FileContentResult Download(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            List<managmentprestadoresFacturasResult> list = Model.GetFacturasByEstadoAceptadas(6, ref MsgRes);

            var fileDownloadName = String.Format("Consolidado.xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


            // Pass your ef data to method
            ExcelPackage package = GenerateExcelFile(list.ToList());

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        private static ExcelPackage GenerateExcelFile(List<managmentprestadoresFacturasResult> datasource)
        {

            ExcelPackage pck = new ExcelPackage();

            //Create the worksheet 
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

            // Sets Headers
            ws.Cells[1, 1].Value = "Regional";
            ws.Cells[1, 2].Value = "Ciudad";
            ws.Cells[1, 3].Value = "Prestador";
            ws.Cells[1, 4].Value = "Numero Factura";
            ws.Cells[1, 5].Value = "Valor Factura";
            ws.Cells[1, 6].Value = "Fecha Expedicion Factura";
            ws.Cells[1, 7].Value = "Fecha Recepcion Factura";
            ws.Cells[1, 8].Value = "Fecha Aprobacion Factura";

            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).nombre_regional;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).nom_ciudad;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).nombre_prestador;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).num_factura;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).valor_neto;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).fecha_exp_factura;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).fecha_recepcion_fac;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).fecha_aprobacion;

            }
            using (ExcelRange rng = ws.Cells["A1:H1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                rng.Style.Font.Color.SetColor(Color.White);
            }
            return pck;
        }

        public PartialViewResult FacturasAprobadasNA(Int32? ID, Int32? regional)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            //List<managmentprestadoresFacturasResult> result = Model.GetFacturasByEstadoAceptadas(2, ref MsgRes);
            List<managmentprestadoresfacturasaceptadasResult> result = (List<managmentprestadoresfacturasaceptadasResult>)Session["ListaFacturasAsigAuditor"];

            List<vw_auditores_totales> list = new List<vw_auditores_totales>();
            list = Model.GetAuditorTotales(ref MsgRes);
            list = list.Where(x => x.id_regional == regional).ToList();

            ViewBag.Auditores = list;

            result = result.Where(x => x.id_cargue_dtll == ID).ToList();
            ViewBag.listgastos = BusClass.Getref_tipo_gasto_facturas(ref MsgRes).OrderBy(x => Convert.ToInt32(x.descripcion.Substring(0, 2))).ToList();
            ViewBag.listCie10 = BusClass.GetCie10Unido();

            foreach (var item in result)
            {
                ViewBag.numFactura = item.num_factura;
                ViewBag.prestador = item.nombre_prestador;
                ViewBag.Vlrfactura = item.valor_neto;
                ViewBag.id_cargue_dtll = item.id_cargue_dtll;
            }

            ViewBag.regional = regional;

            return PartialView(Model);
        }

        public string LlenadoAuditores(int regional)
        {
            var resultado = "";
            try
            {
                Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                List<vw_auditores_totales> list = new List<vw_auditores_totales>();
                list = Model.GetAuditorTotales(ref MsgRes);
                list = list.Where(x => x.id_regional == regional).ToList();

                resultado = "<option value=''>- Seleccionar -</option>";

                foreach (var item in list)
                {
                    resultado += "<option value='" + item.id_usuario + "'>" + item.nombre + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return resultado;
        }

        public JsonResult SavegestionFacturasNA(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            String Alerta = "";

            var detalle = Model.DetalleGasto;

            var auditor = SesionVar.IDUser;

            var BaseImagen = Model.GetFirmas(auditor);
            if (BaseImagen != null)
            {
                if (detalle == null)
                {
                    if (Model.factura_autorizada == "NO")
                    {
                        Alerta = "NO";
                    }
                    else
                    {
                        Alerta = "SI";
                    }
                }
                else
                {
                    Alerta = "NA";
                }

                if (Alerta != "SI")
                {
                    try
                    {
                        ecop_gestion_factura_digital obj = new ecop_gestion_factura_digital();

                        obj.id_cargue_dtll = Model.id_cargue_dtll;
                        obj.multiusuario = Convert.ToString(Model.multiusuario);
                        obj.id_dx1 = Model.id_dx1;
                        obj.gasto = Model.gasto;
                        obj.factura_autorizada = Model.factura_autorizada;
                        obj.fecha_digita = Convert.ToDateTime(DateTime.Now);
                        obj.usuario_digita = Convert.ToString(SesionVar.UserName);
                        if (!string.IsNullOrEmpty(Model.requiere_auditoria))
                        {
                            if (Model.requiere_auditoria == "SI")
                            {
                                obj.aplica_auditoria = true;
                            }
                            else
                            {
                                obj.aplica_auditoria = false;
                            }
                        }
                        else
                        {
                            obj.aplica_auditoria = false;
                        }


                        Model.id_gestion_factura_digital = Model.InsertarGestionFacturadigital(obj, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            if (Alerta == "NA")
                            {
                                foreach (var item in detalle)
                                {
                                    ecop_gestion_factura_digital_gasto objg = new ecop_gestion_factura_digital_gasto();

                                    objg.id_ref_tipo_gasto_facturas = item.id_gasto;
                                    objg.id_gestion_factura_digital = Model.id_gestion_factura_digital;
                                    objg.observacion_gasto = item.obs_gasto;

                                    Model.InsertarGestionFacturadigitalGasto(objg, ref MsgRes);
                                }
                            }

                            mensaje = "SE INGRESO CORRECTAMENTE....";
                            if (Model.factura_autorizada == "SI")
                            {
                                using (SqlConnection connection = new SqlConnection(connectionString))
                                using (SqlCommand command = connection.CreateCommand())
                                {
                                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 11, fecha_aprobacion =@fecha_aprobacion, id_auditor_asignado =@idAuditor, fecha_asignacion_auditor =@fecha Where id_af = @idfact";

                                    command.Parameters.AddWithValue("@idfact", Model.id_cargue_dtll);
                                    command.Parameters.AddWithValue("@idAuditor", Model.id_auditor);
                                    command.Parameters.AddWithValue("@fecha_aprobacion", Convert.ToDateTime(DateTime.Now));
                                    command.Parameters.AddWithValue("@fecha", Convert.ToDateTime(DateTime.Now));

                                    connection.Open();
                                    command.ExecuteNonQuery();
                                    connection.Close();
                                }
                                return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 1 }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 2 }, JsonRequestBehavior.AllowGet);
                            }

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
                else
                {
                    mensaje = "*ERROR.... INGRESE MINIMO UN GASTO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                mensaje = "ERROR... Usuario no puede gestionar porque no tiene firma digital en SAMI.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Creacion de cartas de aprobacion que no necesitan auditoria
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 

        //public ActionResult CrearPdfFacturasDigNA(Int32? ID)
        //{
        //    try
        //    {
        //        MemoryStream memoriaUniversal = new MemoryStream();
        //        string filename2 = "";
        //        string strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];
        //        string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital2NA.rdlc");
        //        Models.Reportes.Reportes Model = new Models.Reportes.Reportes();
        //        Models.InicioSesion.Usuarios Model2 = new Models.InicioSesion.Usuarios();


        //        List<managmentprestadoresFacturasReporteResult> lst = Model.GetFacturasByEstadoReporte(ID.Value, ref MsgRes);
        //        String id_base = Convert.ToString(lst.FirstOrDefault().id_cargue_base);
        //        String id_analista = Convert.ToString(lst.FirstOrDefault().id_analista_gestiona);
        //        String id_auditor = Convert.ToString(lst.FirstOrDefault().id_auditor_asignado);
        //        String id_factura = Convert.ToString(lst.FirstOrDefault().id_gestion_factura_digital);
        //        String id_detalle = Convert.ToString(lst.FirstOrDefault().id_cargue_dtll);

        //        id_base = Model.AgregarValor(id_base) + id_base;
        //        id_analista = Model.AgregarValor2(id_analista) + id_analista;
        //        id_auditor = Model.AgregarValor2(id_auditor) + id_auditor;
        //        id_factura = Model.AgregarValor(id_factura) + id_factura;

        //        var msg = id_detalle;
        //        int bytesRead;
        //        memoriaUniversal.Write(RsaFileDemo.Encriptar(msg, id_detalle), 0, RsaFileDemo.Encriptar(msg, id_detalle).Length);

        //        ecop_firma_digital_cod_barras ObjFirma = new ecop_firma_digital_cod_barras();
        //        ObjFirma.llave_firma = memoriaUniversal.ToArray(); ;
        //        ObjFirma.fecha_digita = Convert.ToDateTime(DateTime.Now);
        //        ObjFirma.usuario_digita = Convert.ToString(SesionVar.UserName);
        //        var id_firma_digital = Model2.InsertarFirmadigital(ObjFirma, ref MsgRes);

        //        memoriaUniversal.Dispose();


        //        id_base = Model.AgregarValor(id_base) + id_base;
        //        string msg2 = id_base + "," + Convert.ToString(id_firma_digital);

        //        memoriaUniversal = new MemoryStream();
        //        memoriaUniversal.Write(RsaFileDemo.LaunchDemo(msg2, id_detalle), 0, RsaFileDemo.LaunchDemo(msg2, id_detalle).Length);
        //        String valorEncriptado = BitConverter.ToString(memoriaUniversal.ToArray()).Replace("-", "");
        //        memoriaUniversal.Dispose();

        //        string cadena = (lst.FirstOrDefault().num_factura + ","
        //           + lst.FirstOrDefault().valor_neto + ","
        //           + lst.FirstOrDefault().num_id_prestador + ","
        //           + valorEncriptado + ","
        //           + id_detalle);

        //        int? analista = lst.FirstOrDefault().id_analista_gestiona;
        //        ecop_firma_digital_sami BaseImagen = Model.GetFirmas(analista);

        //        string Imagen = "";

        //        if (BaseImagen.firma_digital != null)
        //        {
        //            Imagen = Convert.ToBase64String(BaseImagen.firma_digital.ToArray());
        //            BaseImagen = null;
        //        }

        //        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //        QRCodeData qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
        //        QRCode qrCode = new QRCode(qrCodeData);

        //        memoriaUniversal = new MemoryStream();
        //        byte[] ImgByte = null;

        //        using (Bitmap bitMap = qrCode.GetGraphic(20))
        //        {
        //            bitMap.Save(memoriaUniversal, System.Drawing.Imaging.ImageFormat.Png);
        //            ImgByte = memoriaUniversal.ToArray();
        //        }

        //        string Imagen2 = "";
        //        if (ImgByte != null)
        //        {
        //            Imagen2 = Convert.ToBase64String(ImgByte.ToArray());
        //            ImgByte = null;
        //        }
        //        memoriaUniversal.Dispose();

        //        string filename = "CartaUsuarios" + lst.FirstOrDefault().num_factura;

        //        Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
        //        Microsoft.Reporting.WebForms.ReportParameter imagen2 = new Microsoft.Reporting.WebForms.ReportParameter("Imagen2", Imagen2);
        //        Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("FirmasDatSet", lst);
        //        Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();

        //        viewer.ProcessingMode = ProcessingMode.Local;
        //        viewer.LocalReport.ReportPath = rPath;
        //        viewer.LocalReport.DataSources.Clear();
        //        viewer.LocalReport.DataSources.Add(rds);
        //        viewer.LocalReport.SetParameters(imagen);
        //        viewer.LocalReport.SetParameters(imagen2);
        //        viewer.LocalReport.Refresh();

        //        string mimeType;
        //        string encoding;
        //        string fileNameExtension;
        //        string[] streams;
        //        Microsoft.Reporting.WebForms.Warning[] warnings;
        //        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);


        //        String carpeta = "FacturasAprobadas", SubCarpeta = "";
        //        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
        //        {
        //            SubCarpeta = "Facturacion";
        //        }
        //        else
        //        {
        //            SubCarpeta = "FacturacionPruebas";
        //        }

        //        DateTime fecha = DateTime.Now;
        //        string nombre = Path.GetFileNameWithoutExtension("Aprobada" + ID.Value);
        //        string ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + SubCarpeta + "\\" + ID.Value);
        //        filename2 = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta, fecha, nombre, Path.GetExtension(".pdf"));

        //        if (!Directory.Exists(ruta))
        //            Directory.CreateDirectory(ruta);

        //        System.IO.File.WriteAllBytes(filename2, pdfContent);

        //        ecop_gestion_facturas_aprobadas obj = new ecop_gestion_facturas_aprobadas();
        //        obj.id_cargue_dtll = ID.Value;
        //        obj.ruta = filename2;
        //        obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
        //        Model.InsertarFacturaAprobadas(obj, ref MsgRes);

        //        pdfContent = null;
        //        streams = null;
        //        imagen = null;
        //        imagen2 = null;
        //        lst = null;

        //        if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
        //        {

        //            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error en la generación del reporte: " + MsgRes.DescriptionResponse });
        //        }


        //        return File(filename2, "application/pdf");
        //    }
        //    catch (Exception ex)
        //    {

        //        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error en la generación del reporte: " + ex.Message });
        //    }
        //}


        public ActionResult CrearPdfFacturasDigNA(int? ID)
        {
            if (!ID.HasValue)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "ID no proporcionado." });
            }

            try
            {
                var memoriaUniversal = new MemoryStream();
                var filename2 = "";
                var strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];
                var rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital2NA.rdlc");
                var model = new Models.Reportes.Reportes();
                var usuariosModel = new Models.InicioSesion.Usuarios();

                var facturas = model.GetFacturasByEstadoReporte(ID.Value, ref MsgRes);

                if (!facturas.Any())
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se encontraron facturas." });
                }

                var factura = facturas.First();
                var idBase = model.AgregarValor(factura.id_cargue_base.ToString()) + factura.id_cargue_base;
                var idAnalista = model.AgregarValor2(factura.id_analista_gestiona.ToString()) + factura.id_analista_gestiona;
                var idAuditor = model.AgregarValor2(factura.id_auditor_asignado.ToString()) + factura.id_auditor_asignado;
                var idFactura = model.AgregarValor(factura.id_gestion_factura_digital.ToString()) + factura.id_gestion_factura_digital;
                var idDetalle = factura.id_cargue_dtll.ToString();

                var encryptedMsg2 = RsaFileDemo.LaunchDemo(idDetalle, idDetalle);
                memoriaUniversal.Write(encryptedMsg2, 0, encryptedMsg2.Length);

                var objFirma = new ecop_firma_digital_cod_barras
                {
                    llave_firma = memoriaUniversal.ToArray(),
                    fecha_digita = DateTime.Now,
                    usuario_digita = SesionVar.UserName
                };

                var idFirmaDigital = usuariosModel.InsertarFirmadigital(objFirma, ref MsgRes);
                memoriaUniversal.Dispose();

                idBase = model.AgregarValor(idBase) + idBase;
                var msg2 = $"{idBase},{idFirmaDigital}";
                var encryptedMsg3 = RsaFileDemo.LaunchDemo(msg2, idDetalle);
                var valorEncriptado = BitConverter.ToString(encryptedMsg3).Replace("-", "");

                var cadena = $"{factura.num_factura},{factura.valor_neto},{factura.num_id_prestador},{valorEncriptado},{idDetalle}";
                var analista = factura.id_analista_gestiona;
                var baseImagen = model.GetFirmas(analista);

                if (baseImagen == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "El analista no tiene firma." });
                }

                var imagenAnalista = baseImagen.ruta;

                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                using (var memoryStream = new MemoryStream())
                {
                    using (var bitMap = qrCode.GetGraphic(20))
                    {
                        bitMap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        var imgByte = memoryStream.ToArray();
                        var imagenQr = Convert.ToBase64String(imgByte);
                        var reportParameters = new[]
                        {
                            new ReportParameter("Imagen", @"file:///" + imagenAnalista),
                            new ReportParameter("Imagen2", imagenQr)
                         };

                        var viewer = new ReportViewer
                        {
                            ProcessingMode = ProcessingMode.Local,
                            LocalReport =
                            {
                                ReportPath = rPath,
                                DataSources = { new ReportDataSource("FirmasDatSet", facturas) }
                            }
                        };

                        viewer.LocalReport.EnableExternalImages = true;
                        viewer.LocalReport.SetParameters(reportParameters);
                        viewer.LocalReport.Refresh();

                        var pdfContent = viewer.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streams, out Microsoft.Reporting.WebForms.Warning[] warnings);

                        var carpeta = "FacturasAprobadas";
                        var subCarpeta = ConfigurationManager.AppSettings["BDActiva"] == "1" ? "Facturacion" : "FacturacionPruebas";
                        var fecha = DateTime.Now;
                        var ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva, carpeta, subCarpeta, ID.Value.ToString());
                        var nombre = $"Aprobada{ID.Value}";
                        filename2 = $"{ruta}\\{fecha:yyyyMMdd_hhmm}_{nombre}.pdf";

                        if (!Directory.Exists(ruta))
                        {
                            Directory.CreateDirectory(ruta);
                        }

                        System.IO.File.WriteAllBytes(filename2, pdfContent);

                        var obj = new ecop_gestion_facturas_aprobadas
                        {
                            id_cargue_dtll = ID.Value,
                            ruta = filename2,
                            fecha_ingreso = DateTime.Now
                        };

                        var resultadoInsercion = model.InsertarFacturaAprobadas(obj, ref MsgRes);
                        if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Error al insertar la carta: " + MsgRes.DescriptionResponse });
                        }

                        return File(filename2, "application/pdf");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Error en la generación del reporte: " + ex.Message });
            }
        }

        public FileContentResult ExportarLstFacturasGestionadas()
        {
            List<managmentprestadoresfacturasgestionadas3Result> Lista = (List<managmentprestadoresfacturasgestionadas3Result>)Session["ListaFacturas"];

            var fileDownloadName = String.Format("Consolidado.xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            // Pass your ef data to method
            ExcelPackage package = GenerateExcelFileFacturasGestionadas(Lista.ToList());

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        private static ExcelPackage GenerateExcelFileFacturasGestionadas(List<managmentprestadoresfacturasgestionadas3Result> datasource)
        {
            ExcelPackage pck = new ExcelPackage();

            try
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

                // Sets Headers
                ws.Cells[1, 1].Value = "Id cargue";
                ws.Cells[1, 2].Value = "Id detalle";
                ws.Cells[1, 3].Value = "Numero Factura";
                ws.Cells[1, 4].Value = "Valor Factura";
                ws.Cells[1, 5].Value = "NIT";
                ws.Cells[1, 6].Value = "Codigo SAP";
                ws.Cells[1, 7].Value = "Prestador";
                ws.Cells[1, 8].Value = "Regional";
                ws.Cells[1, 9].Value = "Ciudad";
                ws.Cells[1, 10].Value = "codigo CIE 10";
                ws.Cells[1, 11].Value = "Diagnostico";
                ws.Cells[1, 12].Value = "Fecha de factura";
                ws.Cells[1, 13].Value = "Fecha Recepción";
                ws.Cells[1, 14].Value = "Fecha Aprobación";
                ws.Cells[1, 15].Value = "Tipos gasto aplicado";
                ws.Cells[1, 16].Value = "Estado Actual";
                ws.Cells[1, 17].Value = "Analista";
                ws.Cells[1, 18].Value = "Auditor";
                ws.Cells[1, 19].Value = "Valor Glosa";
                ws.Cells[1, 20].Value = "Motivo Glosa";
                ws.Cells[1, 21].Value = "Observación";
                ws.Cells[1, 22].Value = "Motivos Rechazo";
                ws.Cells[1, 23].Value = "Número nueva factura";
                ws.Cells[1, 24].Value = "Valor nueva factura";
                ws.Cells[1, 25].Value = "Número contrato";

                // Inserts Data
                for (int i = 0; i < datasource.Count(); i++)
                {
                    ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id_cargue_base;
                    ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).id_cargue_dtll;
                    ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).num_factura;
                    ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).valor_neto;

                    if (string.IsNullOrEmpty(datasource.ElementAt(i).nitAdicional))
                    {
                        ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).num_id_prestador;
                    }
                    else
                    {
                        ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).nitAdicional;
                    }

                    if (string.IsNullOrEmpty(datasource.ElementAt(i).homologacion_sap))
                    {
                        ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).cod_sap;
                    }
                    else
                    {
                        ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).homologacion_sap;
                    }

                    if (string.IsNullOrEmpty(datasource.ElementAt(i).nombreAdicional))
                    {
                        ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).nombre_prestador;
                    }
                    else
                    {
                        ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).nombreAdicional;
                    }

                    ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).nombre_regional;
                    ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).ciudad;
                    ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).id_diagnostico;
                    ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).diagnostico;
                    ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).fecha_exp_factura;
                    ws.Cells[i + 2, 13].Value = datasource.ElementAt(i).fecha_recepcion_fac;
                    ws.Cells[i + 2, 14].Value = datasource.ElementAt(i).fecha_aprobacion;
                    ws.Cells[i + 2, 15].Value = datasource.ElementAt(i).tipoGastos;
                    ws.Cells[i + 2, 16].Value = datasource.ElementAt(i).estado_des;
                    ws.Cells[i + 2, 17].Value = datasource.ElementAt(i).nom_analitica;
                    ws.Cells[i + 2, 18].Value = datasource.ElementAt(i).nom_auditor;
                    ws.Cells[i + 2, 19].Value = datasource.ElementAt(i).valorGlosa;
                    ws.Cells[i + 2, 20].Value = datasource.ElementAt(i).MotivosGlosa;
                    ws.Cells[i + 2, 21].Value = datasource.ElementAt(i).Observaciones;
                    ws.Cells[i + 2, 22].Value = datasource.ElementAt(i).motivos_rechazo;
                    ws.Cells[i + 2, 23].Value = datasource.ElementAt(i).Numero_factura_nueva;
                    ws.Cells[i + 2, 24].Value = datasource.ElementAt(i).valor_factura_nueva;
                    ws.Cells[i + 2, 25].Value = datasource.ElementAt(i).numero_contrato;

                    ws.Cells[string.Format("L{0}", i + 2)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    ws.Cells[string.Format("M{0}", i + 2)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    ws.Cells[string.Format("N{0}", i + 2)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                }
                ws.Cells["A:AC"].AutoFitColumns();


                using (ExcelRange rng = ws.Cells["A1:AC1"])
                {

                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    rng.Style.Font.Color.SetColor(Color.White);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return pck;
        }

        public void ExportarFacturasMasivamente()
        {
            List<managmentprestadoresfacturasgestionadas3Result> result = (List<managmentprestadoresfacturasgestionadas3Result>)Session["ListaFacturas"];

            result = result.Where(x => x.estado_factura != 0).ToList();
            if (result.Count == 0)
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "</script> ";
                Response.Write(rta);
                Response.End();
            }
            else
            {

                using (ZipFile zip = new ZipFile())
                {
                    int count = 0;
                    foreach (var item in result)
                    {
                        try
                        {
                            WebClient User = new WebClient();
                            string filename = item.ruta;
                            Byte[] FileBuffer = User.DownloadData(filename);
                            //Binary binary2 = item.ruta;
                            byte[] array = FileBuffer.ToArray();
                            zip.AddEntry(item.num_factura + ".pdf", array);
                        }
                        catch (Exception ex)
                        {

                        }

                        count++;
                    }

                    using (MemoryTributary salida = new MemoryTributary())
                    {
                        zip.Save(salida);

                        Response.Clear();
                        Response.BufferOutput = false;
                        Response.ContentType = "application/zip";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=Facturas" + ".zip");
                        Response.BinaryWrite(salida.ToArray());
                        Response.End();
                    }
                }
            }
        }

        public JsonResult GetCascadePrestador(Int32? regional, Models.CuentasMedicas.RadicacionElectronica Model)
        {
            List<management_prestadores_regionalResult> result = Model.GetPrestadoresRegional(regional.Value);

            result = result.Where(x => x.id_ref_regional == regional).ToList();

            return Json(result.Select(p => new { id_ref_unis = p.id_ref_ips_cuentas, nombre = p.Nombre }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RevisarQr()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            return View(Model);
        }

        //public ActionResult PdfFacturasAprobadas(int? ID, Int32? ID2)
        //{
        //    Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

        //    ecop_gestion_facturas_aprobadas obj = new ecop_gestion_facturas_aprobadas();
        //    string dirpath = "";
        //    string extension = "";
        //    var nombreFinal = "";

        //    try
        //    {
        //        obj = Model.GetFacturasAprobadas((int)ID);

        //        if (obj == null)
        //        {
        //            if (ID2 == 6)
        //            {
        //                return CrearPdfFacturasDig(ID);
        //            }
        //            else if (ID2 == 11)
        //            {
        //                ecop_gestion_factura_digital obj2 = Model.GetConsultaGestionFactura(ID).Where(l => l.factura_autorizada == "SI").FirstOrDefault();
        //                if (obj2.aplica_auditoria.Value)
        //                {
        //                    return CrearPdfFacturasDig(ID);
        //                }
        //                else
        //                {
        //                    return CrearPdfFacturasDigNA(ID);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                if (obj.ruta == null || obj.ruta == "")
        //                {
        //                    if (ID2 == 6)
        //                    {
        //                        return CrearPdfFacturasDig(ID);
        //                    }
        //                    else if (ID2 == 11)
        //                    {
        //                        ecop_gestion_factura_digital obj2 = Model.GetConsultaGestionFactura(ID).Where(l => l.factura_autorizada == "SI").FirstOrDefault();
        //                        if (obj2.aplica_auditoria.Value)
        //                        {
        //                            return CrearPdfFacturasDig(ID);
        //                        }
        //                        else
        //                        {
        //                            return CrearPdfFacturasDigNA(ID);
        //                        }
        //                    }
        //                }

        //                dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
        //                string filename = obj.ruta;

        //                string[] nombrePartido = new string[0];
        //                nombrePartido = obj.ruta.Split('\\');
        //                nombreFinal = nombrePartido[5];

        //                var ruta = nombrePartido[0] + "\\" + nombrePartido[1] + "\\" + nombrePartido[2] + "\\" + nombrePartido[3] + "\\" + nombrePartido[4];

        //                try
        //                {
        //                    DirectoryInfo directory = new DirectoryInfo(ruta);

        //                    bool existeRuta = Directory.Exists(ruta);

        //                    if (existeRuta == false)
        //                    {
        //                        if (ID2 == 6)
        //                        {
        //                            return CrearPdfFacturasDig(ID);
        //                        }
        //                        else if (ID2 == 11)
        //                        {
        //                            ecop_gestion_factura_digital obj2 = Model.GetConsultaGestionFactura(ID).Where(l => l.factura_autorizada == "SI").FirstOrDefault();
        //                            if (obj2.aplica_auditoria.Value)
        //                            {
        //                                return CrearPdfFacturasDig(ID);
        //                            }
        //                            else
        //                            {
        //                                return CrearPdfFacturasDigNA(ID);
        //                            }
        //                        }
        //                    }


        //                    FileInfo[] files = directory.GetFiles();

        //                    bool fileFound = false;
        //                    foreach (FileInfo file in files)
        //                    {
        //                        if (String.Compare(file.Name, nombreFinal) == 0)
        //                        {
        //                            fileFound = true;
        //                        }
        //                    }

        //                    bool dirExists = Directory.Exists(dirpath);

        //                    if (!fileFound)
        //                    {
        //                        if (ID2 == 6)
        //                        {
        //                            return CrearPdfFacturasDig(ID);
        //                        }
        //                        else if (ID2 == 11)
        //                        {
        //                            ecop_gestion_factura_digital obj2 = Model.GetConsultaGestionFactura(ID).Where(l => l.factura_autorizada == "SI").FirstOrDefault();
        //                            if (obj2.aplica_auditoria.Value)
        //                            {
        //                                return CrearPdfFacturasDig(ID);
        //                            }
        //                            else
        //                            {
        //                                return CrearPdfFacturasDigNA(ID);
        //                            }
        //                        }
        //                    }

        //                    else
        //                    {
        //                        if (filename.Contains(".pdf"))
        //                        {
        //                            extension = "application/pdf";
        //                        }

        //                        else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
        //                        {
        //                            extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                        }

        //                        else
        //                        {
        //                            extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //                        }

        //                        return File(dirpath, extension, nombreFinal);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    var error = ex.Message;
        //                    throw new Exception(error);
        //                }
        //            }

        //            catch (Exception ex)
        //            {
        //                var error = ex.Message;
        //                throw new Exception(error);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        var error2 = MsgRes.DescriptionResponse;

        //        string rta = "<script LANGUAGE='JavaScript'>" +
        //            "window.alert('ERROR AL GENERAR REPORTE);" +
        //            "</script> ";
        //        Response.Write(rta);
        //        Response.End();
        //    }
        //    return View();
        //}

        private string ObtenerExtensionArchivo(string filename)
        {
            var extension = Path.GetExtension(filename)?.ToLower();
            switch (extension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".xls":
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".doc":
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                default:
                    return "application/octet-stream";
            }
        }

        public ActionResult PdfFacturasAprobadas(int? ID, Int32? ID2)
        {
            var model = new Models.CuentasMedicas.RadicacionElectronica();
            var nombreFinal = string.Empty;

            try
            {
                var facturaAprobada = model.GetFacturasAprobadas((int)ID);

                if (facturaAprobada == null || string.IsNullOrEmpty(facturaAprobada.ruta))
                {
                    return ProcesarFactura(ID, ID2, model);
                }

                var dirPath = Path.Combine(Request.PhysicalApplicationPath, facturaAprobada.ruta);
                var nombrePartido = facturaAprobada.ruta.Split('\\');
                nombreFinal = nombrePartido.Last();
                var ruta = Path.Combine(nombrePartido.Take(5).ToArray());

                if (!Directory.Exists(ruta))
                {
                    return ProcesarFactura(ID, ID2, model);
                }

                var extension = ObtenerExtensionArchivo(facturaAprobada.ruta);
                return File(dirPath, extension, nombreFinal);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                var error2 = MsgRes?.DescriptionResponse ?? "No additional error information.";
                var mensajeError = $"<script LANGUAGE='JavaScript'>window.alert('ERROR AL GENERAR REPORTE: {error}, {error2}');</script>";
                Response.Write(mensajeError);
                Response.End();
            }

            return View();
        }

        private ActionResult ProcesarFactura(int? ID, int? ID2, Models.CuentasMedicas.RadicacionElectronica model)
        {
            if (ID2 == 6 || ID2 == 18)
            {
                return CrearPdfFacturasDig(ID);
            }
            if (ID2 == 11)
            {
                var factura = model.GetConsultaGestionFactura(ID).FirstOrDefault(l => l.factura_autorizada == "SI");
                if (factura?.aplica_auditoria == true)
                {
                    return CrearPdfFacturasDig(ID);
                }
                else
                {
                    return CrearPdfFacturasDigNA(ID);
                }
            }

            return View();
        }

        public ActionResult TableroControlAdmiContrato()
        {
            string rolcordinador = "19";
            if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
            {
                rolcordinador = "20";
            }

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresfacturasDEV_RECHResult> List = Model.GetConsultaRechDevFactura();
            List<managmentprestadoresfacturasDEV_RECHResult> NewList = new List<managmentprestadoresfacturasDEV_RECHResult>();

            List<sis_auditor_regional> regionalesauditor = BusClass.GetRegionalAuditor().Where(l => l.id_auditor == SesionVar.IDUser).ToList();

            if (SesionVar.ROL != "1")
            {
                foreach (var item in List)
                {
                    if (SesionVar.ROL == rolcordinador)
                    {
                        foreach (var regional in regionalesauditor)
                        {
                            var list2 = List.Where(x => x.id_ref_regional == regional.id_regional).ToList();
                            if (list2.Count > 0)
                            {
                                NewList.AddRange(list2);
                            }
                        }
                    }
                    else
                    {
                        if (item.estado_factura == 5)
                        {
                            NewList = List.Where(x => x.id_auditor_asignado == SesionVar.IDUser).ToList();
                        }
                        else
                        {
                            if (item.id_auditor_asignado != null)
                            {
                                NewList = List.Where(x => x.id_auditor_asignado == SesionVar.IDUser).ToList();
                            }
                            else
                            {
                                NewList = List.Where(x => x.id_analista_gestiona == SesionVar.IDUser).ToList();
                            }
                        }
                    }
                }

                ViewBag.Lista = NewList;
                ViewBag.ROL = SesionVar.ROL;
            }
            else
            {
                ViewBag.Lista = List;
                ViewBag.ROL = SesionVar.ROL;
            }

            return View(Model);
        }

        public PartialViewResult GestionarObservacion(int ID)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managmentprestadoresFacturasOBSResult> list = new List<managmentprestadoresFacturasOBSResult>();

            list = Model.GetConsultaObsFactura(ID);

            ViewBag.listaObs = list;
            ViewBag.id_af = ID;

            return PartialView();
        }

        public JsonResult SaveObservaciones(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            var Usuario = Convert.ToString(SesionVar.UserName);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {

                    command.CommandText = "INSERT INTO recep_facturas_obs(id_af,observacion,usuario_ingreso,fecha_ingreso)values(@id_af,@observacion,@usuario_ingreso,@fecha_ingreso)";
                    command.Parameters.AddWithValue("@id_af", Model.id_cargue_dtll);
                    command.Parameters.AddWithValue("@observacion", Model.observaciones);
                    command.Parameters.AddWithValue("@usuario_ingreso", Usuario);
                    command.Parameters.AddWithValue("@fecha_ingreso", Convert.ToDateTime(DateTime.Now));

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveObservacionesDtll(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_observacion = 1 Where id_af = @idfact";

                    command.Parameters.AddWithValue("@idfact", Model.id_cargue_dtll);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                //EnviarCaso(Model);

                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 2 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EnviarCaso(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            String correo = "desarrollo.soporte@asalud.co";
            String asunto = "Ingreso Observaciones Rechazos y Devoluciones";
            String factura = "";
            byte[] array = new byte[0];

            List<managmentprestadoresfacturasDEV_RECHResult> List = new List<managmentprestadoresfacturasDEV_RECHResult>();
            List = Model.GetConsultaRechDevFactura();
            List = List.Where(x => x.id_cargue_dtll == Model.id_cargue_dtll).ToList();

            foreach (var item in List)
            {
                factura = item.num_factura;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("Se ha generado una observación para la factura numero: ");
            sb.Append(factura);
            sb.Append("<br/>");
            sb.Append("Por favor revisar el tablero de Observaciones.");

            string textBody = sb.ToString();

            string filename = "";

            try
            {

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

                mailMessage.From = new MailAddress("sistemas@asalud.co");
                mailMessage.To.Add(correo);

                mailMessage.Subject = "[Mensaje Automático]" + " " + asunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                //MemoryStream memoryStream = new MemoryStream(array);
                //mailMessage.Attachments.Add(new Attachment(memoryStream, filename));
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.AddModelError("", "ERROR!" + ViewBag.Message);
            }

            return View(Model);
        }

        public PartialViewResult GestionarObservacionAnalista(int ID, Int32? ID2)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managmentprestadoresFacturasOBSResult> list = new List<managmentprestadoresFacturasOBSResult>();

            list = Model.GetConsultaObsFactura(ID);

            ViewBag.listaObs = list;
            ViewBag.id_af = ID;
            ViewBag.estado = ID2;

            return PartialView();
        }

        public JsonResult SaveLevantar(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            Int32 estado = 0;

            List<managmentprestadoresfacturasDEV_RECHResult> List = new List<managmentprestadoresfacturasDEV_RECHResult>();

            List = Model.GetConsultaRechDevFactura();
            List = List.Where(x => x.id_cargue_dtll == Model.id_cargue_dtll).ToList();

            foreach (var item in List)
            {
                if (item.estado_factura == 5)
                {
                    estado = 4;
                }
                else
                {
                    if (item.id_auditor_asignado != null)
                    {
                        estado = 4;
                    }
                    else
                    {
                        estado = 1;
                    }
                }
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_observacion = 3,estado_factura = @estado_factura  Where id_af = @idfact";

                    command.Parameters.AddWithValue("@idfact", Model.id_cargue_dtll);
                    command.Parameters.AddWithValue("@estado_factura", estado);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 2 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveObservacionesAnalista(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            var Usuario = Convert.ToString(SesionVar.UserName);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {

                    command.CommandText = "INSERT INTO recep_facturas_obs(id_af,observacion,usuario_ingreso,fecha_ingreso)values(@id_af,@observacion,@usuario_ingreso,@fecha_ingreso)";
                    command.Parameters.AddWithValue("@id_af", Model.id_cargue_dtll);
                    command.Parameters.AddWithValue("@observacion", Model.observaciones);
                    command.Parameters.AddWithValue("@usuario_ingreso", Usuario);
                    command.Parameters.AddWithValue("@fecha_ingreso", Convert.ToDateTime(DateTime.Now));

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveObservacionesDtllAnalista(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_observacion = 2 Where id_af = @idfact";

                    command.Parameters.AddWithValue("@idfact", Model.id_cargue_dtll);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 2 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveAsignacionMasivo(Models.CuentasMedicas.RadicacionElectronica Model)
        {

            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            String mensaje = "";

            var detalle = Model.SelectedList;
            try
            {
                foreach (var item in detalle.ToList())
                {
                    ref_analista_lote obj = new ref_analista_lote();
                    obj.id_lote_facturas = Convert.ToInt32(item);
                    obj.id_analista = Model.id_auditor;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;
                    Model.Insertaranalistalote(obj, ref MsgRes);

                    /*Validacion empleada para eliminar el lote del listado que aun esta en cache*/

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        List<vw_prestadores_lotes> ListadoLotesFacturasCache = cache["listadoLoteFacturas" + nomUsuario] as List<vw_prestadores_lotes>;
                        if (ListadoLotesFacturasCache != null)
                        {
                            var itemCache = ListadoLotesFacturasCache.Where(l => l.id_cargue_base == Convert.ToInt32(item)).FirstOrDefault();
                            if (itemCache != null)
                            {
                                String key = "tiempoExpiracionLotes" + nomUsuario;
                                DateTime expiracionCache = CheckCachedExpiry(key);
                                CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expiracionCache };

                                ListadoLotesFacturasCache.Remove(itemCache);
                                cache.Set("listadoLoteFacturas" + nomUsuario, ListadoLotesFacturasCache, policy);
                                cache.Set("tiempoExpiracionLotes" + nomUsuario, expiracionCache, policy);
                            }
                        }
                    }
                }
                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 1 }, JsonRequestBehavior.AllowGet);
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

        public JsonResult FacturasValicacionCreacion(Int32? id_dtll)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new RadicacionElectronica();
            List<ecop_gestion_factura_digital> lista = new List<ecop_gestion_factura_digital>();

            lista = Model.GetConsultaGestionFactura(id_dtll);

            if (lista.Count == 0)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, id = Model.id_cargue_dtll }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult FacturaDevPrestadores(int ID)
        {
            Models.Facturacion.FacturaDevolucion Model2 = new Models.Facturacion.FacturaDevolucion();

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            ViewBag.id_af = ID;

            List<managmentprestadoresfacturasACEP_ASIGResult> result = new List<managmentprestadoresfacturasACEP_ASIGResult>();
            result = Model.GetConsultaAcep_AsigFactura();
            result = result.Where(x => x.id_cargue_dtll == ID).ToList();

            foreach (var item in result)
            {
                ViewBag.id_regional = item.id_ref_regional;
                ViewBag.ciudad = item.id_ref_ciudades;
                ViewBag.Prestador = item.prestador;
                ViewBag.NumeroFactura = item.num_factura;
                ViewBag.ValorFactura = Convert.ToString(item.valor_neto);
                ViewBag.ModuloPrestadores = "SI";
            }

            ViewBag.productos = BusClass.GetMedglosa();
            ViewBag.listaRegional = BusClass.GetRefRegion();
            ViewBag.listaCiudad = BusClass.GetCiudades();
            ViewBag.ips = BusClass.GetPrstadorCuentas();

            return PartialView(Model2);
        }

        public JsonResult SaveFacturaDevolucionPrestadores(Models.Facturacion.FacturaDevolucion Model)
        {
            var mensaje = "";
            var rta = 0;
            var aprueba = false;

            fis_rips_facturas fac = new fis_rips_facturas();
            factura_devolucion obj = new factura_devolucion();
            fis_rips_prestadores pre = new fis_rips_prestadores();
            ecop_gestion_factura_digital obj2 = new ecop_gestion_factura_digital();


            try
            {
                fac = BusClass.TraerFacturaIdAf(Model.id_af);
                if (fac != null)
                {

                    obj2.id_cargue_dtll = Model.id_af;
                    obj2.gasto = null;
                    obj2.factura_autorizada = "NO";
                    obj2.fecha_digita = Convert.ToDateTime(DateTime.Now);
                    obj2.usuario_digita = Convert.ToString(SesionVar.UserName);
                    obj2.aplica_auditoria = true;
                    BusClass.InsertarGestionFacturadigital(obj2, ref MsgRes);

                    pre = BusClass.PrestadorxNit(fac.nit_prestador);
                    obj.Nit = fac.nit_prestador;

                    if (pre != null)
                    {
                        obj.Prestador = pre.id_prestador;
                    }

                    obj.fecha_devolucion = DateTime.Now;
                    obj.NumeroFactura = fac.numero_factura;
                    obj.valor_factura = Convert.ToString(fac.valor_total_factura);
                    obj.id_regional = Model.id_regional;
                    obj.ciudad = Model.ciudad;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;
                    obj.ModuloPrestadores = "NO";
                    obj.id_af = Model.id_af;

                    //tipo devolución: 1 - analista, 2 - auditor

                    obj.tipoDevolucion = Model.tipoDevolucion;

                    Model.id_devolucion_factura = BusClass.InsertarDevolucionFacturas(obj, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        fis_rips_facturas fac2 = BusClass.TraerFacturaIdAfAuditor(Model.id_af);

                        Model.OBJFacturaDetalle.id_devolucion_factura = Model.id_devolucion_factura;
                        Model.OBJFacturaDetalle.ValorGlosa = Convert.ToString(fac.valor_total_factura + (fac.valor_total_factura - fac.valor_total_factura_conGlosa));
                        Model.OBJFacturaDetalle.id_motivo = fac2.glosa_integral == 1 ? 2222 : 1111;
                        Model.OBJFacturaDetalle.descripcion_devolucion = "NA";
                        Model.OBJFacturaDetalle.Observaciones = "Se devuelve desde auditor por tener glosas activas";

                        Model.InsertarDevolucionFacturasDetalle(ref MsgRes);

                        EnviarCorreoFacturaDevuelta(Model.id_af, Model.id_devolucion_factura, null, ref MsgRes);

                        mensaje = "FACTURA DEVUELTA AL PRESTADOR CORRECTAMENTE";
                        aprueba = true;
                    }
                    else
                    {
                        mensaje = "ERROR AL DEVOLVER LA FACTURA";
                    }
                }
                else
                {
                    mensaje = "ERROR AL TRAER DATOS DE FACTURA";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(new { success = aprueba, id = Model.id_devolucion_factura, mensaje = mensaje });
        }

        public JsonResult SavegestionFacturasFis(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            string nomUsuario = SesionVar.UserName;
            String mensaje = "";
            var idDetalle = Model.id_cargue_dtll;
            management_fisFactura_ultimoDiagnosticoResult cie10 = new management_fisFactura_ultimoDiagnosticoResult();
            ecop_gestion_factura_digital obj = new ecop_gestion_factura_digital();

            try
            {

                fis_rips_cargue_registrosCompletos reg = new fis_rips_cargue_registrosCompletos();
                reg = BusClass.TraerRegistroRipsIdFactura(Model.id_cargue_dtll);

                if (reg != null)
                {
                    cie10 = BusClass.TraerUltimoDiagnosticoCie100FacturaFis(reg.cod_cuv);
                    if (cie10 != null)
                    {
                        obj.id_dx1 = cie10.codDiagnosticoPrincipal;
                    }
                }

                obj.id_cargue_dtll = Model.id_cargue_dtll;
                obj.multiusuario = Convert.ToString(Model.multiusuario);

                obj.gasto = Model.gasto;
                obj.factura_autorizada = Model.factura_autorizada;
                obj.fecha_digita = Convert.ToDateTime(DateTime.Now);
                obj.usuario_digita = Convert.ToString(SesionVar.UserName);
                obj.aplica_auditoria = true;
                Model.id_gestion_factura_digital = Model.InsertarGestionFacturadigital(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";

                    if (Model.factura_autorizada == "SI")
                    {
                        idDetalle = Model.id_cargue_dtll;

                        return Json(new { success = true, message = mensaje, id = idDetalle, opc = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, message = mensaje, id = idDetalle, opc = 2 }, JsonRequestBehavior.AllowGet);
                    }
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

        public JsonResult SaveFacturaDevPrestadores(Models.Facturacion.FacturaDevolucion Model)
        {
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            String mensaje = "";
            var detalle = Model.Detalle;
            try
            {
                if (Model.Detalle == null || Model.Detalle.Count == 0)
                {
                    mensaje = "Debe agregar al menos un detalle para la factura.";

                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Int32 sumaGlosa = 0;
                    if (Model.Detalle.Count > 0)
                    {
                        sumaGlosa = Model.Detalle.Select(l => Convert.ToInt32(l.ValorGlosa)).Sum();
                    }

                    String Glosa = (Model.ValorFactura).Replace(".", "");

                    if (sumaGlosa > Convert.ToInt32(Glosa))
                    {
                        mensaje = "!! ERROR....LA SUMA DE LAS GLOSAS ES SUPERIOR AL VALOR DE LA FACTURA.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {


                        Int32 condicion = 0;

                        if (condicion == 0)
                        {

                            Model.OBJFactura.Prestador = Model.Prestador;
                            Model.OBJFactura.fecha_devolucion = Convert.ToDateTime(DateTime.Now);
                            Model.OBJFactura.id_regional = Model.id_regional;
                            Model.OBJFactura.NumeroFactura = Model.NumeroFactura;
                            Model.OBJFactura.valor_factura = Convert.ToString(Model.ValorFactura);
                            Model.OBJFactura.ciudad = Model.ciudad;
                            Model.OBJFactura.fecha_digita = Convert.ToDateTime(DateTime.Now);
                            Model.OBJFactura.usuario_digita = Convert.ToString(SesionVar.UserName);
                            Model.OBJFactura.ModuloPrestadores = "SI";
                            Model.OBJFactura.id_af = Model.id_af;

                            Model.id_devolucion_factura = Model.InsertarDevolucionFacturas(ref MsgRes);

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

                                EnviarCorreoFacturaDevuelta(Model.id_af, Model.id_devolucion_factura, null, ref MsgRes);

                                /*En este apartado se limpiara la factura que se gestionó de la memoria cache*/
                                List<managmentprestadoresFacturasAuditorOKCompletaResult> ListadoFacturasAuditorEnCache = cache["facturasAuditor" + nomUsuario] as List<managmentprestadoresFacturasAuditorOKCompletaResult>;
                                if (ListadoFacturasAuditorEnCache != null)
                                {
                                    var itemCache = ListadoFacturasAuditorEnCache.Where(l => l.id_cargue_dtll == Model.id_af).FirstOrDefault();
                                    if (itemCache != null)
                                    {
                                        String key = "tiempoExpiracionMemoria" + nomUsuario;
                                        DateTime expiracionCache = CheckCachedExpiry(key);
                                        CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expiracionCache };

                                        ListadoFacturasAuditorEnCache.Remove(itemCache);
                                        cache.Set("facturasAuditor" + nomUsuario, ListadoFacturasAuditorEnCache, policy);
                                        cache.Set("tiempoExpiracionMemoria" + nomUsuario, expiracionCache, policy);
                                    }
                                }

                                mensaje = "SE INGRESO CORRECTAMENTE....";
                                return Json(new { success = true, message = mensaje, id = Model.id_devolucion_factura }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            mensaje = "!! ERROR...ESTA FACTURA Y ESTE PRESTADOR YA TIENEN UN INGRESO.";
                            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public void EnviarCorreoFacturaDevuelta(int idcarguedtll, int idDevolucion, string correo, ref MessageResponseOBJ MsgRes)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            RadicacionElectronica Model = new RadicacionElectronica();
            ManagementPrestadoresFacturasByIdDtllResult factura = Model.GetInfoFacturaById(idcarguedtll);
            if (factura != null)
            {
                if (string.IsNullOrEmpty(correo))
                {
                    correo = factura.correo;
                }
            }

            byte[] data = CrearPdfCartaDevolucionFacCorreo(idDevolucion, idcarguedtll);
            MemoryStream ms = new MemoryStream(data);

            try
            {
                string bdactiva = ConfigurationManager.AppSettings["BDActiva"];
                string mailBody = "";
                string mailCSS = "";
                string textBody = string.Format("SAMI Informa: {1}{0}  {2}{0}  {3}{0}  {4}{0}Link Ingreso:  {5}", Environment.NewLine, string.Empty, string.Empty, "Cordial saludo. Se ha devuelto la factura No  " + factura.num_factura + " del prestador " + factura.nombre_prestador + ". Debe ingresar al tablero de facturas devueltas para poder ver los motivos de la devolución, y a su vez subsanar la factura. Puede ingresar al aplicativo, y realizar la gestión correspondiente por la siguiente url: ", string.Empty, "https://prestadores.aplicativoasalud.co"); ;

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
                mailBody += "Cordialmente,";
                mailBody += "</div>";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                mailBody += "<img src='cid:dealer_logo' />";
                mailBody += "<br />";
                mailBody += "<STRONG>Asalud SAS </STRONG>";
                mailBody += "<br />";
                mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
                mailBody += "<br />";
                mailBody += "Dir. Calle 96 # 13a-03 Piso 4";
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

                mailMessage.Attachments.Add(new Attachment(ms, "CartaDevolucion_" + idcarguedtll + ".pdf", "application/pdf"));

                LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                resource.ContentId = "dealer_logo";
                htmlView.LinkedResources.Add(resource);

                mailMessage.AlternateViews.Add(htmlView);
                mailMessage.From = new MailAddress("admin@asaludltda.com");
                mailMessage.To.Clear();

                if (bdactiva == "1")
                {
                    mailMessage.To.Add(correo);
                    mailMessage.To.Add("notificaciones@asaludltda.com");
                }
                else
                {
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                }

                mailMessage.Subject = "[Devolución de la factura No " + factura.num_factura + "]";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
            }
        }

        public JsonResult ObtenerIdDevolucion(int id)
        {
            Models.Facturacion.FacturaDevolucion Model = new Models.Facturacion.FacturaDevolucion();

            List<factura_devolucion> result = Model.GetfacturadevolucionByIdFactura(id);
            factura_devolucion devolucion = result.LastOrDefault();

            var data = new
            {

                idDev = devolucion.id_devolucion_factura
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrearPdfCartaDevolucionFac(Int32? ID)
        {
            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptDevolucionFac.rdlc");
                Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

                string filename = "CartaDevolucionFactura" + ID;
                List<ManagmentReportDevolucionFacResult> lst = new List<ManagmentReportDevolucionFacResult>();
                lst = Model.ConsultaReporteDevolucionFac(Convert.ToInt32(ID));

                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetDevolucionFac", lst);

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

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);


                        //Response.ClearContent();
                        //Response.ClearHeaders();
                        //Response.Clear();

                        //Response.BufferOutput = false;
                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("content-length", pdfContent.Length.ToString());
                        //Response.BinaryWrite(pdfContent);
                        //Response.Flush();

                        return File(pdfContent, "application/pdf", "CartaDevolucionFac" + DateTime.Now + ".pdf");

                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;

                        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message });
                    }
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message });
            }

        }


        public ActionResult CrearPdfCartaDevolucionFac2(Int32? ID, int? idAf)
        {
            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "ReporteDevolucionFacturas.rdlc");
                Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

                string filename = "CartaDevolucionFactura" + ID;
                List<ManagmentReportDevolucionFacResult> lst = new List<ManagmentReportDevolucionFacResult>();
                lst = Model.ConsultaReporteDevolucionFac(Convert.ToInt32(ID));

                List<ManagmentReportDevolucionFac_glosasResult> glos = new List<ManagmentReportDevolucionFac_glosasResult>();
                glos = BusClass.ConsultaReporteDevolucionFac_glosa(idAf);

                var auditor = lst.Select(x => x.id_auditor_asignado).FirstOrDefault();

                var firmaAuditor = Model.GetFirmasId(auditor);
                if (firmaAuditor == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "El auditor no tiene firma." });
                }

                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetDevolucionFac", lst);
                Microsoft.Reporting.WebForms.ReportDataSource rdsGlosas = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetDevolucionFac_glosas", glos);
                Microsoft.Reporting.WebForms.ReportParameter Imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", @"file:///" + firmaAuditor.ruta);

                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.EnableExternalImages = true;

                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.DataSources.Add(rdsGlosas);

                if (firmaAuditor != null)
                {
                    viewer.LocalReport.SetParameters(Imagen);
                }

                if (lst.Count != 0)
                {
                    try
                    {
                        viewer.LocalReport.Refresh();

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                        return File(pdfContent, "application/pdf", "CartaDevolucionFac" + DateTime.Now + ".pdf");

                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;

                        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message });
                    }
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message });
            }

        }

        public byte[] CrearPdfCartaDevolucionFacCorreo(Int32? ID, int? idAf)
        {
            byte[] pdfDevuelve;
            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "ReporteDevolucionFacturas.rdlc");
                Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

                string filename = "CartaDevolucionFactura" + ID;
                List<ManagmentReportDevolucionFacResult> lst = new List<ManagmentReportDevolucionFacResult>();
                lst = Model.ConsultaReporteDevolucionFac(Convert.ToInt32(ID));

                List<ManagmentReportDevolucionFac_glosasResult> glos = new List<ManagmentReportDevolucionFac_glosasResult>();
                glos = BusClass.ConsultaReporteDevolucionFac_glosa(idAf);

                var auditor = lst.Select(x => x.id_auditor_asignado).FirstOrDefault();
                var firmaAuditor = Model.GetFirmasId(auditor);

                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetDevolucionFac", lst);
                Microsoft.Reporting.WebForms.ReportDataSource rdsGlosas = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetDevolucionFac_glosas", glos);
                Microsoft.Reporting.WebForms.ReportParameter Imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", @"file:///" + (firmaAuditor != null ? firmaAuditor.ruta : ""));

                ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.LocalReport.EnableExternalImages = true;

                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.DataSources.Add(rdsGlosas);


                if (firmaAuditor != null)
                {
                    viewer.LocalReport.SetParameters(Imagen);
                }

                if (lst.Count != 0)
                {
                    try
                    {
                        viewer.LocalReport.Refresh();

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        pdfDevuelve = pdfContent;
                        return pdfDevuelve;

                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                        return null;

                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public JsonResult ActualizaDevolucion(Int32? id_af)
        //{
        //    String mensaje = "";

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        using (SqlCommand command = connection.CreateCommand())
        //        {
        //            command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 5 Where id_af = @idfact AND estado_factura <> 5";
        //            command.Parameters.AddWithValue("@idfact", id_af);
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //            connection.Close();
        //        }

        //        mensaje = "SE INGRESO CORRECTAMENTE....";
        //        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = "*---ERROR -- - *" + ex.Message;
        //        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        //    }

        //}

        public ActionResult TableroGestionTotalesFacturas(Int32? regional, Int32? rango)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managmentprestadores_estados_factura_totalResult> result = new List<managmentprestadores_estados_factura_totalResult>();
            List<managmentprestadores_estados_factura_totalResult> lista = new List<managmentprestadores_estados_factura_totalResult>();
            List<managmentprestadores_estados_factura_totalResult> lista3 = new List<managmentprestadores_estados_factura_totalResult>();
            List<managmentprestadores_estados_factura_totalResult> Lista2 = new List<managmentprestadores_estados_factura_totalResult>();

            List<vw_ref_estado_factura_total_rango> result2 = new List<vw_ref_estado_factura_total_rango>();
            List<vw_ref_estado_factura_total_rango> Lista22 = new List<vw_ref_estado_factura_total_rango>();
            List<vw_ref_estado_factura_total_rango> Lista33 = new List<vw_ref_estado_factura_total_rango>();
            List<vw_ref_estado_factura_total_rango> Lista11 = new List<vw_ref_estado_factura_total_rango>();
            List<sis_auditor_regional> list = new List<sis_auditor_regional>();

            if (rango != null)
            {
                ViewBag.opc = 2;
                Lista22 = Model.GetRecepcionFacturasRango(rango.Value);

                if (SesionVar.ROL != "1")
                {
                    ViewBag.opcion = 2;
                    list = BusClass.listadoRegionalesUsuario(SesionVar.IDUser);

                    foreach (var item in list)
                    {
                        result2 = Lista22.Where(x => x.id_ref_regional == item.id_regional).ToList();
                        Lista33 = Lista33.Concat(result2).ToList();
                    }

                    Lista11 = Lista33;
                }
                else
                {
                    ViewBag.opcion = 1;
                    Lista11 = Lista22;
                }

                if (regional != null)
                {
                    Lista11 = Lista11.Where(x => x.id_ref_regional == regional).ToList();
                }
                ViewBag.Lista = Lista11;

            }
            else
            {
                ViewBag.opc = 1;
                Lista2 = Model.GetTotalFacturas();

                if (SesionVar.ROL != "1")
                {
                    ViewBag.opcion = 2;
                    list = BusClass.listadoRegionalesUsuario(SesionVar.IDUser);

                    foreach (var item in list)
                    {
                        result = Lista2.Where(x => x.id_ref_regional == item.id_regional).ToList();
                        lista3 = lista3.Concat(result).ToList();
                    }

                    lista = lista3;
                }
                else
                {
                    ViewBag.opcion = 1;
                    lista = Lista2;
                }

                if (regional != null)
                {
                    lista = lista.Where(x => x.id_ref_regional == regional).ToList();
                }

                ViewBag.Lista = lista;
            }

            ViewBag.ROL = SesionVar.ROL;
            ViewBag.regionales = BusClass.GetRefRegion();
            return View(Model);
        }

        public FileContentResult ExportarFacturasTotales(Int32 estado, Int32 id_regional)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturasResult> Lista = new List<managmentprestadoresFacturasResult>();

            var fileDownloadName = String.Format("Consolidado.xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            Lista = Model.GetFacturasByEstadoAceptadas(estado, ref MsgRes);
            Lista = Lista.Where(x => x.id_ref_regional == id_regional).ToList();

            // Pass your ef data to method
            ExcelPackage package = GenerateExcelFileFacturasTotales(Lista.ToList());

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        private static ExcelPackage GenerateExcelFileFacturasTotales(List<managmentprestadoresFacturasResult> datasource)
        {
            ExcelPackage pck = new ExcelPackage();

            //Create the worksheet 
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

            // Sets Headers
            ws.Cells[1, 1].Value = "Id Lote";
            ws.Cells[1, 2].Value = "Id Factura";
            ws.Cells[1, 3].Value = "Numero Factura";
            ws.Cells[1, 4].Value = "Nit Prestador";
            ws.Cells[1, 5].Value = "Nombre Prestador";
            ws.Cells[1, 6].Value = "Regional";
            ws.Cells[1, 7].Value = "Fecha Recepción Factura";
            ws.Cells[1, 8].Value = "Fecha Gestion Analista";
            ws.Cells[1, 9].Value = "Fecha Aprobación";
            ws.Cells[1, 10].Value = "Valor Factura";
            ws.Cells[1, 11].Value = "Analista asignado";
            ws.Cells[1, 12].Value = "Fecha asignación analista ";
            ws.Cells[1, 13].Value = "Auditor asignado";
            ws.Cells[1, 14].Value = "Fecha Asignación Auditor";
            ws.Cells[1, 15].Value = "Estado Factura";
            ws.Cells[1, 16].Value = "Motivo rechazo";
            ws.Cells[1, 17].Value = "Observaciones rechazo";
            ws.Cells[1, 18].Value = "Causal devolución";
            ws.Cells[1, 19].Value = "Observaciones devolución";

            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id_cargue_base;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).id_cargue_dtll;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).num_factura;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).num_id_prestador;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).nombre_prestador;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).nombre_regional;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).fecha_ultima_gestion;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).fecha_recepcion_fac;
                ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).fecha_aprobacion;
                ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).valor_neto;
                ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).nom_analitica;
                ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).fecha_asignacion_analista;
                ws.Cells[i + 2, 13].Value = datasource.ElementAt(i).nom_auditor;
                ws.Cells[i + 2, 14].Value = datasource.ElementAt(i).fecha_asignacion_auditor;
                ws.Cells[i + 2, 15].Value = datasource.ElementAt(i).estado_des;
                ws.Cells[i + 2, 16].Value = datasource.ElementAt(i).motivos_rechazo;
                ws.Cells[i + 2, 17].Value = datasource.ElementAt(i).observaciones;
                ws.Cells[i + 2, 18].Value = datasource.ElementAt(i).MotivosGlosa;
                ws.Cells[i + 2, 19].Value = datasource.ElementAt(i).Observaciones_devolucion;
            }

            using (ExcelRange rng = ws.Cells["A1:T1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                rng.Style.Font.Color.SetColor(Color.White);
            }
            ws.Cells["A:T"].AutoFitColumns();
            return pck;
        }

        public FileContentResult ExportarFacturasTotalesRango(Int32 rango, Int32 id_regional)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturas_rangoResult> Lista = new List<managmentprestadoresFacturas_rangoResult>();

            var fileDownloadName = String.Format("Consolidado.xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            Lista = Model.GetFacturasByEstadoAceptadasRango(rango, id_regional);

            // Pass your ef data to method
            ExcelPackage package = GenerateExcelFileFacturasTotalesRango(Lista.ToList());

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        private static ExcelPackage GenerateExcelFileFacturasTotalesRango(List<managmentprestadoresFacturas_rangoResult> datasource)
        {
            ExcelPackage pck = new ExcelPackage();

            //Create the worksheet 
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

            // Sets Headers
            ws.Cells[1, 1].Value = "Id Lote";
            ws.Cells[1, 1].Value = "Id Factura";
            ws.Cells[1, 1].Value = "Numero Factura";
            ws.Cells[1, 2].Value = "Nombre Prestador";
            ws.Cells[1, 3].Value = "Regional";
            ws.Cells[1, 4].Value = "Fecha Recepción Factura";
            ws.Cells[1, 5].Value = "Fecha Gestion Analista";
            ws.Cells[1, 6].Value = "Fecha Aprobación";
            ws.Cells[1, 7].Value = "Valor Factura";
            ws.Cells[1, 8].Value = "Analista asignado";
            ws.Cells[1, 9].Value = "Auditor asignado";
            ws.Cells[1, 10].Value = "Fecha Asignación Auditor";
            ws.Cells[1, 11].Value = "Estado Factura";
            ws.Cells[1, 12].Value = "Motivo rechazo";
            ws.Cells[1, 13].Value = "Observaciones rechazo";
            ws.Cells[1, 14].Value = "Causal devolución";
            ws.Cells[1, 15].Value = "Observaciones devolución";

            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id_cargue_base;
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id_cargue_dtll;
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).num_factura;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).nombre_prestador;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).nombre_regional;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).fecha_ultima_gestion;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).fecha_recepcion_fac;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).fecha_aprobacion;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).valor_neto;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).nom_analitica;
                ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).nom_auditor;
                ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).fecha_asignacion_auditor;
                ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).estado_des;
                ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).motivos_rechazo;
                ws.Cells[i + 2, 13].Value = datasource.ElementAt(i).observaciones;
                ws.Cells[i + 2, 14].Value = datasource.ElementAt(i).MotivosGlosa;
                ws.Cells[i + 2, 15].Value = datasource.ElementAt(i).Observaciones_devolucion;

            }
            using (ExcelRange rng = ws.Cells["A1:Q1"])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                rng.Style.Font.Color.SetColor(Color.White);
            }

            ws.Cells["A:Q"].AutoFitColumns();
            return pck;
        }

        public PartialViewResult ObtenerAuditorRegional(List<int> capitulos)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<vw_auditores_totales> listaAuditores = new List<vw_auditores_totales>();
            List<vw_auditores_totales> Lista3 = new List<vw_auditores_totales>();
            List<sis_auditor_regional> list = new List<sis_auditor_regional>();

            listaAuditores = Model.GetAuditorTotales(ref MsgRes);

            regional obj = new regional();

            list = BusClass.GetRegionalAuditor();
            list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

            foreach (var item in list)
            {
                List<vw_auditores_totales> listaAuditores2 = new List<vw_auditores_totales>();

                listaAuditores2 = listaAuditores.Where(l => l.id_regional == item.id_regional).ToList();
                Lista3 = Lista3.Concat(listaAuditores2).ToList();
            }

            ViewBag.lista = Lista3;
            ViewBag.cargue_base = capitulos;

            return PartialView(Model);
        }

        public JsonResult SaveAsignacionMasivoAuditor(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            var detalle = Model.SelectedList;
            try
            {
                foreach (var item in detalle.ToList())
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {

                        DateTime fecha = Convert.ToDateTime(DateTime.Now);
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 4,id_auditor_asignado =@idAuditor, fecha_asignacion_auditor =@fecha Where id_af = @idfact";

                        command.Parameters.AddWithValue("@idfact", item);
                        command.Parameters.AddWithValue("@idAuditor", Model.id_auditor);
                        command.Parameters.AddWithValue("@fecha", fecha);
                        connection.Open();
                        command.CommandTimeout = 120;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }

                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CargueBaseContabilizado()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            return View(Model);

        }

        public JsonResult SaveBaseContabilizada(HttpPostedFileBase file)
        {

            String mensaje = "";
            String tipoAlerta = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new RadicacionElectronica();

            try
            {

                if (this.Request.Files["file"].ContentLength > 0)
                {
                    using (var excel = new ExcelPackage(file.InputStream))
                    {
                        var tbl = new DataTable();
                        var ws = excel.Workbook.Worksheets.First();
                        var hasHeader = true;
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text
                                : String.Format("Column {0}", firstRowCell.Start.Column));


                        int startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                            DataRow row = tbl.NewRow();
                            foreach (var cell in wsRow)
                                row[cell.Start.Column - 1] = cell.Text;
                            tbl.Rows.Add(row);
                        }

                        Int32 lote = Model.CargarBaseContabilizado(tbl, ref MsgRes);

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensaje = "SE INGRESO CORRECTAMENTE....";
                            return Json(new { success = true, message = mensaje, lote = lote }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            mensaje = MsgRes.DescriptionResponse;

                            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    mensaje = "*---DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE -- - *";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public void CrearPdfCartaContabilizada(Int32? ID)
        {
            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptLoteContabilizado.rdlc");
                Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

                string filename = "CartaLoteContabilizada_" + ID;
                List<management_reportelotecontabilizadoResult> lst = new List<management_reportelotecontabilizadoResult>();
                lst = Model.ConsultaReporteLote(Convert.ToInt32(ID));

                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("ContabilizadosDataSet", lst);

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

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();

                        Response.BufferOutput = false;
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

        public ActionResult TableroControlAsignaciones(String numFac, String nit, String prestador, String sap, int? rta, int? estado, int? idDetalle)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<view_ref_estado_facturas> estados = new List<view_ref_estado_facturas>();
            List<managmentprestadoresfacturasgestionadasCompletaResult> result = new List<managmentprestadoresfacturasgestionadasCompletaResult>();
            List<managmentprestadoresfacturasgestionadasCompletaResult> lista = new List<managmentprestadoresfacturasgestionadasCompletaResult>();
            List<managmentprestadoresfacturasgestionadasCompletaResult> lista3 = new List<managmentprestadoresfacturasgestionadasCompletaResult>();
            List<managmentprestadoresfacturasgestionadasCompletaResult> Lista2 = new List<managmentprestadoresfacturasgestionadasCompletaResult>();

            try
            {
                //Lista2 = Model.GetGestionFacturasv2(fechainicial, fechafinal, estado, regional, prestador, nit, numFac);
                if (!string.IsNullOrEmpty(numFac) || !string.IsNullOrEmpty(nit) || !string.IsNullOrEmpty(prestador) || !string.IsNullOrEmpty(sap)
                    || estado != null || idDetalle != null)
                {
                    Lista2 = Model.GetGestionFacturasv3(numFac, nit, prestador, sap, estado, idDetalle);
                }

                if (!string.IsNullOrEmpty(sap))
                {
                    Lista2 = Lista2.Where(x => x.cod_sap_prestador == sap).ToList();
                }

                if (Lista2 != null)
                {
                    if (Lista2.Count() > 0)
                    {
                        if (SesionVar.ROL != "1")
                        {
                            List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                            regional obj = new regional();
                            list = BusClass.GetRegionalAuditor();
                            list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                            foreach (var item in list)
                            {
                                result = Lista2.Where(x => x.id_ref_regional == item.id_regional).ToList();
                                lista3 = lista3.Concat(result).ToList();
                            }

                            lista = lista3;
                        }
                        else
                        {
                            lista = Lista2;
                        }
                    }
                    else
                    {
                        lista = Lista2;
                    }
                }

                ViewBag.ROL = SesionVar.ROL;
                Session["ListaFacturas"] = lista;

                var conteo = lista.Count();

                ViewBag.Lista = lista;
                ViewBag.conteo = conteo;

                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.ref_estado = Model.GetEstadoFacturas();

                //ViewBag.fechainicio = fechainicial;
                //ViewBag.fechafin = fechafinal;
                ViewBag.rta = rta;

                var estadoUsado = 0;
                if (estado != null)
                {
                    estadoUsado = (int)estado;
                }
                ViewBag.estadoUsado = estadoUsado;

                var rol = SesionVar.ROL;
                ViewBag.rol = rol;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            estados = BusClass.GetEstadoFacturas();

            ViewBag.estados = estados;

            return View(Model);
        }

        public JsonResult Buscar_Prestador_Nit()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    List<management_prestadoresHomologadosResult> prestadores = new List<management_prestadoresHomologadosResult>();
                    prestadores = BusClass.getprestadoresHomologados();
                    prestadores = prestadores.Where(x => x.homologado_nit != null && x.homologado_nit != "" && x.homologado_razonSocial != "" && x.homologado_razonSocial != null).ToList();
                    prestadores = prestadores.Where(x => x.homologado_nit.Contains(term) || x.homologado_razonSocial.Contains(term)).ToList();

                    var lista = (from reg in prestadores
                                 select new
                                 {
                                     nit = reg.homologado_nit,
                                     label = reg.homologado_nit + "-" + reg.homologado_razonSocial,
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
        public JsonResult Buscar_Prestador_Nombre()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {

                    List<management_prestadoresHomologadosResult> prestadores = new List<management_prestadoresHomologadosResult>();
                    prestadores = BusClass.getprestadoresHomologados();
                    prestadores = prestadores.Where(x => x.homologado_nit != null && x.homologado_nit != "" && x.homologado_razonSocial != "" && x.homologado_razonSocial != null).ToList();
                    prestadores = prestadores.Where(x => x.homologado_nit.ToUpper().Contains(term.ToUpper()) || x.homologado_razonSocial.ToUpper().Contains(term.ToUpper())).ToList();

                    var lista = (from reg in prestadores
                                 select new
                                 {
                                     nombre = reg.homologado_razonSocial,
                                     label = reg.homologado_nit + "-" + reg.homologado_razonSocial,
                                 }).Distinct().OrderBy(f => f.nombre).Take(15);
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

        public JsonResult Buscar_Prestador_Sap()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {

                    List<management_prestadoresHomologadosResult> prestadores = new List<management_prestadoresHomologadosResult>();
                    prestadores = BusClass.getprestadoresHomologados();
                    prestadores = prestadores.Where(x => x.homologado_sap != null).ToList();
                    prestadores = prestadores.Where(x => x.homologado_sap.Contains(term)).ToList();

                    var lista = (from reg in prestadores
                                 select new
                                 {
                                     sap = reg.homologado_sap,
                                     label = reg.homologado_sap + "-" + reg.homologado_razonSocial,
                                 }).Distinct().OrderBy(f => f.sap).Take(15);
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

        public PartialViewResult GestionarAnalista(int ID, Int32 ID2, Int32 ID3)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<vw_analistas_recepcion> listanalistas = new List<vw_analistas_recepcion>();
            List<vw_analistas_recepcion> Lista3 = new List<vw_analistas_recepcion>();
            List<sis_auditor_regional> list = new List<sis_auditor_regional>();

            listanalistas = BusClass.GetListAnalistas();

            regional obj = new regional();

            list = BusClass.GetRegionalAuditor();
            list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

            foreach (var item in list)
            {
                listanalistas = listanalistas.Where(l => l.id_regional == item.id_regional).ToList();
                Lista3 = Lista3.Concat(listanalistas).ToList();
            }

            ViewBag.lista = Lista3;
            ViewBag.id_af = ID;
            ViewBag.id_analista = ID2;
            ViewBag.id_cargue = ID3;

            return PartialView(Model);
        }

        public JsonResult SaveGestionarAnalista(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            var Usuario = Convert.ToString(SesionVar.UserName);
            DateTime fecha = Convert.ToDateTime(DateTime.Now);

            ecop_gestion_factura_digital_control_cambios obj = new ecop_gestion_factura_digital_control_cambios();

            obj.observacion = Model.observaciones;
            obj.id_analista1 = Model.id_analista;
            obj.id_analista2 = Model.id_analista2;
            obj.fecha_ingeso = fecha;
            obj.usuario_ingreso = Usuario;
            Model.InsertarControlCambios(obj, ref MsgRes);

            if (Model.idcargue != null)
            {
                Model.ActualizarAnalistalote(Model.idcargue, Model.id_analista2);
            }

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET id_analista_gestiona =@id_analista_gestiona Where id_recep_facturas_cargue_base = @idcargue";
                        command.Parameters.AddWithValue("@id_analista_gestiona", Model.id_analista2);
                        command.Parameters.AddWithValue("@idcargue", Model.idcargue);

                        connection.Open();
                        command.CommandTimeout = 120;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "*---ERROR -- - *" + ex.Message;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

                mensaje = "SE INGRESO CORRECTAMENTE.";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult GestionarAuditorReasig(int ID, Int32 ID2)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<vw_auditores_totales> listaAuditores = new List<vw_auditores_totales>();
            List<vw_auditores_totales> Lista3 = new List<vw_auditores_totales>();
            List<sis_auditor_regional> list = new List<sis_auditor_regional>();

            listaAuditores = Model.GetAuditorTotales(ref MsgRes);

            regional obj = new regional();

            list = BusClass.GetRegionalAuditor();
            list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

            foreach (var item in list)
            {
                listaAuditores = listaAuditores.Where(l => l.id_regional == item.id_regional).ToList();
                Lista3 = Lista3.Concat(listaAuditores).ToList();
            }

            ViewBag.lista = Lista3;

            ViewBag.id_af = ID;
            ViewBag.id_auditor = ID2;

            return PartialView(Model);
        }

        [ValidateInput(false)]
        public PartialViewResult GestionarAuditorReasigGlobal(string items)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<vw_auditores_totales> listaAuditores = new List<vw_auditores_totales>();
            List<vw_auditores_totales> Lista3 = new List<vw_auditores_totales>();
            List<sis_auditor_regional> list = new List<sis_auditor_regional>();
            try
            {
                listaAuditores = Model.GetAuditorTotales(ref MsgRes);
                regional obj = new regional();

                list = BusClass.GetRegionalAuditor();
                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                foreach (var item in list)
                {
                    listaAuditores = listaAuditores.Where(l => l.id_regional == item.id_regional).ToList();
                    Lista3 = Lista3.Concat(listaAuditores).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.lista = Lista3;
            ViewBag.items = items;

            return PartialView(Model);
        }
        public JsonResult SaveGestionarAuditorReasig(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            var Usuario = Convert.ToString(SesionVar.UserName);
            DateTime fecha = Convert.ToDateTime(DateTime.Now);

            ecop_gestion_factura_digital_control_cambios obj = new ecop_gestion_factura_digital_control_cambios();

            obj.observacion = Model.observaciones;
            obj.id_auditor1 = Model.id_auditor;
            obj.id_auditor2 = Model.id_auditor2;
            obj.id_auditor2 = Model.id_auditor2;
            obj.fecha_ingeso = fecha;
            obj.usuario_ingreso = Usuario;

            Model.InsertarControlCambios(obj, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET id_auditor_asignado =@id_auditor_asignado Where id_af = @id_dtll";
                        command.Parameters.AddWithValue("@id_auditor_asignado", Model.id_auditor2);
                        command.Parameters.AddWithValue("@id_dtll", Model.id_cargue_dtll);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "*---ERROR -- - *" + ex.Message;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveGestionarAuditorReasigGlobal(Models.CuentasMedicas.RadicacionElectronica Model, string items)
        {
            String mensaje = "";
            var Usuario = Convert.ToString(SesionVar.UserName);
            DateTime fecha = Convert.ToDateTime(DateTime.Now);

            try
            {
                if (!string.IsNullOrEmpty(items))
                {

                    String[] itemsId = new string[0];
                    if (items != null)
                    {
                        itemsId = items.Split(',');
                    }

                    if (itemsId.Count() > 0)
                    {

                        foreach (var dato in itemsId)
                        {
                            managemenet_prestadores_traerDatosFacturasResult dato_af = new managemenet_prestadores_traerDatosFacturasResult();

                            var id = dato;

                            id = id.Replace(" ", "");
                            id = id.Replace(",", "");

                            dato_af = BusClass.ListadoFacturasIdAf(Convert.ToInt32(id));

                            ecop_gestion_factura_digital_control_cambios obj = new ecop_gestion_factura_digital_control_cambios();

                            obj.observacion = Model.observaciones;
                            obj.id_auditor1 = dato_af.id_auditor_asignado;
                            obj.id_auditor2 = Model.id_auditor2;
                            obj.id_gestion_factura_control = Convert.ToInt32(id);
                            obj.fecha_ingeso = fecha;
                            obj.usuario_ingreso = Usuario;

                            Model.InsertarControlCambios(obj, ref MsgRes);

                            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                            {
                                try
                                {
                                    using (SqlConnection connection = new SqlConnection(connectionString))
                                    using (SqlCommand command = connection.CreateCommand())
                                    {
                                        command.CommandText = "UPDATE rips_af_cargue_dtll SET id_auditor_asignado =@id_auditor_asignado Where id_af = @id_dtll";
                                        command.Parameters.AddWithValue("@id_auditor_asignado", Model.id_auditor2);
                                        command.Parameters.AddWithValue("@id_dtll", Convert.ToInt32(id));

                                        connection.Open();
                                        command.ExecuteNonQuery();
                                        connection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    mensaje = "*---ERROR -- - *" + ex.Message;
                                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        mensaje = "SE ACTUALIZÓ EL AUDITOR CORRECTAMENTE.";
                        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO DEL DETALLE.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "*---ERROR -- - *" + error;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult GestionarEstadoReasig(int ID, Int32 ID2)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<view_ref_estado_facturas> estado = new List<view_ref_estado_facturas>();

            estado = Model.GetEstadoFacturas();

            switch (ID2)
            {
                case 0:
                    estado = estado.Where(x => x.id_estado_factura == 0).ToList();
                    break;
                case 1:
                    estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    break;
                case 2:
                    estado = estado.Where(x => x.id_estado_factura == 2).ToList();
                    break;
                case 3:
                    estado = estado.Where(x => x.id_estado_factura == 3 || x.id_estado_factura == 2).ToList();
                    break;
                case 4:
                    estado = estado.Where(x => x.id_estado_factura == 4).ToList();
                    break;
                case 5:
                    estado = estado.Where(x => x.id_estado_factura == 5 || x.id_estado_factura == 4).ToList();
                    break;
                case 6:
                    estado = estado.Where(x => x.id_estado_factura == 6).ToList();
                    break;
                case 7:
                    estado = estado.Where(x => x.id_estado_factura == 7).ToList();
                    break;
                case 8:
                    estado = estado.Where(x => x.id_estado_factura == 8).ToList();
                    break;
                case 9:
                    estado = estado.Where(x => x.id_estado_factura == 9).ToList();
                    break;
                case 10:
                    estado = estado.Where(x => x.id_estado_factura == 10).ToList();
                    break;
                case 11:
                    estado = estado.Where(x => x.id_estado_factura == 11).ToList();
                    break;
                case 12:
                    estado = estado.Where(x => x.id_estado_factura == 12).ToList();
                    break;
                default:

                    break;
            }


            ViewBag.ref_estado = estado;
            ViewBag.id_af = ID;
            ViewBag.estado = ID2;

            return PartialView();
        }

        public PartialViewResult GestionarEstadoReasigGlobal(string item, Int32 estadoUsado)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<view_ref_estado_facturas> estado = new List<view_ref_estado_facturas>();

            estado = Model.GetEstadoFacturas();

            switch (estadoUsado)
            {
                case 0:
                    estado = estado.Where(x => x.id_estado_factura == 0).ToList();
                    break;
                case 1:
                    estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    break;
                case 2:
                    estado = estado.Where(x => x.id_estado_factura == 2).ToList();
                    break;
                case 3:
                    estado = estado.Where(x => x.id_estado_factura == 3 || x.id_estado_factura == 2).ToList();
                    break;
                case 4:
                    estado = estado.Where(x => x.id_estado_factura == 4).ToList();
                    break;
                case 5:
                    estado = estado.Where(x => x.id_estado_factura == 5 || x.id_estado_factura == 4).ToList();
                    break;
                case 6:
                    estado = estado.Where(x => x.id_estado_factura == 6).ToList();
                    break;
                case 7:
                    estado = estado.Where(x => x.id_estado_factura == 7).ToList();
                    break;
                case 8:
                    estado = estado.Where(x => x.id_estado_factura == 8).ToList();
                    break;
                case 9:
                    estado = estado.Where(x => x.id_estado_factura == 9).ToList();
                    break;
                case 10:
                    estado = estado.Where(x => x.id_estado_factura == 10).ToList();
                    break;
                case 11:
                    estado = estado.Where(x => x.id_estado_factura == 11).ToList();
                    break;
                case 12:
                    estado = estado.Where(x => x.id_estado_factura == 12).ToList();
                    break;
                default:

                    break;
            }


            ViewBag.ref_estado = estado;
            ViewBag.items = item;
            ViewBag.estado = estadoUsado;

            return PartialView();
        }

        public PartialViewResult GestionarEstadoReasigEspe(int ID, Int32 ID2)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<view_ref_estado_facturas> estado = new List<view_ref_estado_facturas>();

            List<management_existeLoteAsignado_FacturaResult> listadoAsignado = new List<management_existeLoteAsignado_FacturaResult>();
            listadoAsignado = BusClass.ExisteLoteAsignado(ID);

            var existe = 0;
            if (listadoAsignado.Count > 0)
            {
                existe = 1;
            }

            estado = Model.GetEstadoFacturas().OrderByDescending(x => x.nom_estado).ToList();

            switch (ID2)
            {
                case 0:
                    estado = estado.Where(x => x.id_estado_factura == 0).ToList();
                    break;
                case 1:
                    estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    break;
                case 2:

                    if (existe == 1)
                    {
                        estado = estado.Where(x => x.id_estado_factura == 15).ToList();
                    }
                    else
                    {
                        estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    }

                    break;
                case 3:
                    estado = estado.Where(x => x.id_estado_factura == 2).ToList();
                    break;
                case 4:

                    if (existe == 1)
                    {
                        estado = estado.Where(x => x.id_estado_factura == 15).ToList();
                    }
                    else
                    {
                        estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    }

                    break;
                case 5:
                    estado = estado.Where(x => x.id_estado_factura == 4).ToList();
                    break;
                case 6:
                    if (existe == 1)
                    {
                        estado = estado.Where(x => x.id_estado_factura == 15 || x.id_estado_factura == 4).ToList();
                    }
                    else
                    {
                        estado = estado.Where(x => x.id_estado_factura == 1 || x.id_estado_factura == 4).ToList();
                    }
                    break;
                case 7:
                    estado = estado.Where(x => x.id_estado_factura == 7).ToList();
                    break;
                case 8:
                    estado = estado.Where(x => x.id_estado_factura == 8).ToList();
                    break;
                case 9:
                    estado = estado.Where(x => x.id_estado_factura == 9).ToList();
                    break;
                case 10:
                    estado = estado.Where(x => x.id_estado_factura == 10).ToList();
                    break;
                case 11:

                    if (existe == 1)
                    {
                        estado = estado.Where(x => x.id_estado_factura == 15).ToList();
                    }
                    else
                    {
                        estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    }

                    break;
                case 12:
                    estado = estado.Where(x => x.id_estado_factura == 12).ToList();
                    break;
                default:

                    break;
            }

            ViewBag.ref_estado = estado;
            ViewBag.id_af = ID;
            ViewBag.estado = ID2;

            return PartialView();
        }

        public PartialViewResult GestionarEstadoReasigEspeGlobal(string item, Int32 estadoUsado)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            var existe = 0;
            List<view_ref_estado_facturas> estado = new List<view_ref_estado_facturas>();

            estado = Model.GetEstadoFacturas().OrderByDescending(x => x.nom_estado).ToList();

            switch (estadoUsado)
            {
                case 0:
                    estado = estado.Where(x => x.id_estado_factura == 0).ToList();
                    break;
                case 1:
                    estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    break;
                case 2:

                    if (existe == 1)
                    {
                        estado = estado.Where(x => x.id_estado_factura == 15).ToList();
                    }
                    else
                    {
                        estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    }

                    break;
                case 3:
                    estado = estado.Where(x => x.id_estado_factura == 2).ToList();
                    break;
                case 4:

                    if (existe == 1)
                    {
                        estado = estado.Where(x => x.id_estado_factura == 15).ToList();
                    }
                    else
                    {
                        estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    }

                    break;
                case 5:
                    estado = estado.Where(x => x.id_estado_factura == 4).ToList();
                    break;
                case 6:
                    if (existe == 1)
                    {
                        estado = estado.Where(x => x.id_estado_factura == 15 || x.id_estado_factura == 4).ToList();
                    }
                    else
                    {
                        estado = estado.Where(x => x.id_estado_factura == 1 || x.id_estado_factura == 4).ToList();
                    }
                    break;
                case 7:
                    estado = estado.Where(x => x.id_estado_factura == 7).ToList();
                    break;
                case 8:
                    estado = estado.Where(x => x.id_estado_factura == 8).ToList();
                    break;
                case 9:
                    estado = estado.Where(x => x.id_estado_factura == 9).ToList();
                    break;
                case 10:
                    estado = estado.Where(x => x.id_estado_factura == 10).ToList();
                    break;
                case 11:

                    if (existe == 1)
                    {
                        estado = estado.Where(x => x.id_estado_factura == 15).ToList();
                    }
                    else
                    {
                        estado = estado.Where(x => x.id_estado_factura == 1).ToList();
                    }

                    break;
                case 12:
                    estado = estado.Where(x => x.id_estado_factura == 12).ToList();
                    break;
                default:

                    break;
            }

            ViewBag.ref_estado = estado;
            ViewBag.items = item;
            ViewBag.estado = estadoUsado;

            return PartialView();
        }


        public JsonResult SaveGestionarEstadoReasig(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            var Usuario = Convert.ToString(SesionVar.UserName);
            DateTime fecha = Convert.ToDateTime(DateTime.Now);

            ecop_gestion_factura_digital_control_cambios obj = new ecop_gestion_factura_digital_control_cambios();

            obj.observacion = Model.observaciones;
            obj.estado1 = Model.estado;
            obj.estado2 = Model.estado2;
            obj.fecha_ingeso = fecha;
            obj.id_dtll_factura = Model.id_cargue_dtll;
            obj.fecha_ingeso = fecha;
            obj.usuario_ingreso = Usuario;

            Model.InsertarControlCambios(obj, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura =@estado_factura Where id_af = @id_dtll";
                        command.Parameters.AddWithValue("@estado_factura", Model.estado2);
                        command.Parameters.AddWithValue("@id_dtll", Model.id_cargue_dtll);

                        connection.Open();
                        command.CommandTimeout = 120;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "*---ERROR -- - *" + ex.Message;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

                try
                {
                    var resultadoActualizar = Model.ActualizarEstado_Facturas(Model.id_cargue_dtll, Model.estado, Model.estado2);
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveGestionarEstadoReasigGlobal(ecop_gestion_factura_digital_control_cambios Model, string items)
        {
            String mensaje = "";
            var idUsado = 0;
            try
            {
                var Usuario = Convert.ToString(SesionVar.UserName);
                DateTime fecha = Convert.ToDateTime(DateTime.Now);

                String[] itemsId = new string[0];
                if (items != null)
                {
                    itemsId = items.Split(',');
                }

                if (itemsId.Count() > 0)
                {
                    ecop_gestion_factura_digital_control_cambios obj = new ecop_gestion_factura_digital_control_cambios();

                    foreach (var valor in itemsId)
                    {
                        idUsado = Convert.ToInt32(valor);
                        obj.observacion = Model.observacion;
                        obj.estado1 = Model.estado1;
                        obj.estado2 = Model.estado2;
                        obj.fecha_ingeso = fecha;
                        obj.id_dtll_factura = Convert.ToInt32(valor);
                        obj.fecha_ingeso = fecha;
                        obj.usuario_ingreso = Usuario;

                        var resultado = BusClass.InsertarControlCambios(obj, ref MsgRes);
                    }

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                                foreach (var valor2 in itemsId)
                                {
                                    using (SqlCommand command = connection.CreateCommand())
                                    {
                                        command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura =@estado_factura Where id_af = @id_dtll";
                                        command.Parameters.AddWithValue("@estado_factura", Model.estado2);
                                        command.Parameters.AddWithValue("@id_dtll", valor2);

                                        connection.Open();
                                        command.CommandTimeout = 120;
                                        command.ExecuteNonQuery();
                                        connection.Close();
                                    }

                                    try
                                    {
                                        var resultadoActualizar = BusClass.ActualizarEstado_Facturas(Convert.ToInt32(valor2), (int)Model.estado1, (int)Model.estado2);
                                    }
                                    catch (Exception ex)
                                    {
                                        var error = ex.Message;
                                    }
                                }
                        }
                        catch (Exception ex)
                        {
                            mensaje = "*---ERROR -- - *" + ex.Message + "-" + idUsado;
                            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                        }

                        mensaje = "SE ACTUALIZÓ EL ESTADO CORRECTAMENTE.";
                        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "*---ERROR -- - *" + idUsado;
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "DEBE SELECCIONAR FACTURAS.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message + "-" + idUsado;
                //mensaje = "ERROR AL REASIGNAR ESTADO: " + error;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public PartialViewResult GestionarRegional(int ID)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            ViewBag.id_lote = ID;
            ViewBag.regionales = BusClass.GetRefRegion();

            return PartialView();
        }

        public PartialViewResult GestionarRegionalGlobal(string items)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            ViewBag.items = items;
            ViewBag.regionalesG = BusClass.GetRefRegion();

            return PartialView();
        }

        public JsonResult SaveGestionarRegional(Int32 id_lote, Int32 regional)
        {
            String mensaje = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE recep_facturas_cargue_base SET id_regional =@id_regional Where id_cargue_base = @id_cargue_base";
                    command.Parameters.AddWithValue("@id_regional", regional);
                    command.Parameters.AddWithValue("@id_cargue_base", id_lote);

                    connection.Open();
                    command.CommandTimeout = 120;
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                try
                {
                    var eliminadoLote = BusClass.EliminarLoteFacturas(id_lote);
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }


                mensaje = "SE INGRESO CORRECTAMENTE....";

                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveGestionarRegionalGlobal(string items, Int32 regional)
        {
            String mensaje = "";

            String[] ids = new string[0];
            ids = items.Split(',');

            try
            {
                foreach (var campo in ids)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        var count = 0;
                        command.CommandText = "UPDATE recep_facturas_cargue_base SET id_regional = @id_regional Where id_cargue_base = @cargueItems";
                        command.Parameters.AddWithValue("@id_regional", regional);
                        command.Parameters.AddWithValue("@cargueItems", Convert.ToInt32(campo));

                        connection.Open();
                        command.CommandTimeout = 120;
                        command.ExecuteNonQuery();
                        connection.Close();
                        count++;
                    }
                }
                try
                {
                    var count = 0;
                    foreach (var campo in ids)
                    {
                        var eliminadoLote = BusClass.EliminarLoteFacturas(Convert.ToInt32(campo));
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                mensaje = "SE ACTUALIZÓ REGIONAL CORRECTAMENTE.";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult versoporteclinico2(Int32 idsoporteclinico, Int32 idDtll)
        //{
        //    try
        //    {
        //        Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
        //        List<managment_prestadores_documentosResult> List = Model.GetSoportesList(idDtll);
        //        List = List.Where(x => x.Id_gestion_documental == idsoporteclinico).ToList();
        //        var obj = List.FirstOrDefault();
        //        string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
        //        if (System.IO.File.Exists(dirpath))
        //        {
        //            string extension = Path.GetExtension(dirpath).ToLower(); // Obtener extensión del archivo
        //            string contentType;

        //            switch (extension)
        //            {
        //                case ".pdf":
        //                    contentType = "application/pdf";
        //                    break;
        //                case ".zip":
        //                    contentType = "application/zip";
        //                    break;
        //                default:
        //                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Formato de archivo no soportado." });
        //            }

        //            byte[] bytes = System.IO.File.ReadAllBytes(dirpath);
        //            //return File(bytes, contentType, Path.GetFileName(dirpath)); // Se devuelve el archivo con su nombre original
        //            return File(bytes, contentType);
        //        }
        //        else
        //        {
        //            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de generar mostrar el archivo: " + ex.Message });
        //    }
        //}

        public ActionResult versoporteclinico2(Int32 idsoporteclinico, Int32 idDtll)
        {
            try
            {
                Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                List<managment_prestadores_documentosResult> List = Model.GetSoportesList(idDtll);
                List = List.Where(x => x.Id_gestion_documental == idsoporteclinico).ToList();
                var obj = List.FirstOrDefault();

                if (obj == null || string.IsNullOrEmpty(obj.ruta))
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se encontró la ruta del archivo." });
                }

                string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                if (System.IO.File.Exists(dirpath))
                {
                    string extension = Path.GetExtension(dirpath).ToLower(); // Obtener extensión del archivo
                    string contentType;
                    bool isInline = false; // Para previsualizar en el navegador

                    switch (extension)
                    {
                        case ".pdf":
                            contentType = "application/pdf";
                            isInline = true; // Se previsualiza
                            break;
                        case ".zip":
                            contentType = "application/zip";
                            break;
                        default:
                            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Formato de archivo no soportado." });
                    }

                    byte[] bytes = System.IO.File.ReadAllBytes(dirpath);


                    Response.Clear();
                    Response.ContentType = contentType;
                    Response.AddHeader("Content-Length", bytes.Length.ToString());

                    if (isInline)
                    {
                        // Previsualización del PDF
                        Response.AddHeader("Content-Disposition", "inline; filename=" + Path.GetFileName(dirpath));
                    }
                    else
                    {
                        // Descarga del ZIP
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(dirpath));
                    }

                    Response.BinaryWrite(bytes);
                    Response.End();
                    return new EmptyResult();
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al mostrar el archivo: " + ex.Message });
            }
        }


        public PartialViewResult AprobarAuditorMasivo(List<int> capitulos)
        {

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<vw_auditores_totales> list = new List<vw_auditores_totales>();
            List<vw_auditores_totales> list3 = new List<vw_auditores_totales>();
            list = Model.GetAuditorTotales(ref MsgRes);
            //list = list.Where(x => x.id_regional == regional).ToList();

            List<sis_auditor_regional> list2 = new List<sis_auditor_regional>();
            regional obj = new regional();
            list2 = BusClass.GetRegionalAuditor();
            list2 = list2.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

            foreach (var item in list2)
            {
                List<vw_auditores_totales> list4 = new List<vw_auditores_totales>();
                list4 = list.Where(x => x.id_regional == item.id_regional).ToList();
                list3 = list3.Concat(list4).ToList();
            }

            ViewBag.Auditores = list3;

            ViewBag.cargue_base = capitulos;
            ViewBag.listgastos = BusClass.Getref_tipo_gasto_facturas(ref MsgRes).OrderBy(x => Convert.ToInt32(x.descripcion.Substring(0, 2))).ToList();
            ViewBag.listCie10 = BusClass.GetCie10Unido();

            return PartialView(Model);
        }

        public JsonResult SavegestionFacturasMasivo(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            var detalle = Model.DetalleGasto;
            var detalle2 = Model.SelectedList;

            String mensaje = "";
            String Alerta = "";

            if (detalle != null)
            {
                try
                {
                    if (detalle2 != null)
                    {
                        foreach (var itemDtll in detalle2.ToList())
                        {
                            ecop_gestion_factura_digital obj = new ecop_gestion_factura_digital();

                            obj.id_cargue_dtll = Convert.ToInt32(itemDtll);
                            obj.multiusuario = Convert.ToString("NO");
                            obj.id_dx1 = Model.id_dx1;
                            obj.gasto = Model.gasto;
                            obj.factura_autorizada = Model.factura_autorizada;
                            obj.fecha_digita = Convert.ToDateTime(DateTime.Now);
                            obj.usuario_digita = Convert.ToString(SesionVar.UserName);
                            if (!string.IsNullOrEmpty(Model.requiere_auditoria))
                            {
                                if (Model.requiere_auditoria == "SI")
                                {
                                    obj.aplica_auditoria = true;
                                }
                                else
                                {
                                    obj.aplica_auditoria = false;
                                }
                            }
                            else
                            {
                                obj.aplica_auditoria = false;
                            }

                            Model.id_gestion_factura_digital = Model.InsertarGestionFacturadigital(obj, ref MsgRes);


                            foreach (var item in detalle)
                            {
                                ecop_gestion_factura_digital_gasto objg = new ecop_gestion_factura_digital_gasto();

                                objg.id_ref_tipo_gasto_facturas = item.id_gasto;
                                objg.id_gestion_factura_digital = Model.id_gestion_factura_digital;
                                objg.observacion_gasto = item.obs_gasto;

                                Model.InsertarGestionFacturadigitalGasto(objg, ref MsgRes);
                            }

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            using (SqlCommand command = connection.CreateCommand())
                            {
                                command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 11, fecha_aprobacion =@fecha_aprobacion, id_auditor_asignado =@idAuditor, fecha_asignacion_auditor =@fecha Where id_af = @idfact";

                                command.Parameters.AddWithValue("@idfact", Convert.ToInt32(itemDtll));
                                command.Parameters.AddWithValue("@idAuditor", Model.id_auditor);
                                command.Parameters.AddWithValue("@fecha_aprobacion", Convert.ToDateTime(DateTime.Now));
                                command.Parameters.AddWithValue("@fecha", Convert.ToDateTime(DateTime.Now));

                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }

                        mensaje = "SE INGRESO CORRECTAMENTE....";
                        return Json(new { success = true, message = mensaje, id = Model.id_cargue_dtll, opc = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "*ERROR.... AL INGRESAR.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "*---ERROR -- - *" + ex.Message;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                mensaje = "*ERROR.... INGRESE MINIMO UN GASTO.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public PartialViewResult GestionarTIGA(int ID)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<vw_factura_digital_gasto_total> gastos = Model.GetGatosFactura(ID);
            ViewBag.listTipoGasto = Model.Getref_tipo_gasto_facturas(ref MsgRes).OrderBy(x => Convert.ToInt32(x.descripcion.Substring(0, 2))).ToList();
            ViewBag.idfactura = ID;
            Session["tiga"] = gastos;
            return PartialView(gastos);
        }


        public PartialViewResult GestionarTIGA2(int ID)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<vw_factura_digital_gasto_total> gastos = Model.GetGatosFactura(ID);
            ViewBag.listTipoGasto = Model.Getref_tipo_gasto_facturas(ref MsgRes).OrderBy(x => Convert.ToInt32(x.descripcion.Substring(0, 2))).ToList();
            ViewBag.idfactura = ID;
            Session["tiga2"] = gastos;
            return PartialView(gastos);
        }

        public JsonResult EditarTiga(int idfacturagasto)
        {
            List<vw_factura_digital_gasto_total> gastosactuales = (List<vw_factura_digital_gasto_total>)Session["tiga"];
            vw_factura_digital_gasto_total obj = gastosactuales.Where(l => l.id_factura_digital_gasto == idfacturagasto).FirstOrDefault();

            var data = new
            {
                id = idfacturagasto,
                idtipogasto = obj.id_ref_tipo_gasto_facturas,
                observaciones = obj.observacion_gasto
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditarTiga2(int idfacturagasto)
        {
            List<vw_factura_digital_gasto_total> gastosactuales = (List<vw_factura_digital_gasto_total>)Session["tiga2"];
            vw_factura_digital_gasto_total obj = gastosactuales.Where(l => l.id_factura_digital_gasto == idfacturagasto).FirstOrDefault();

            var data = new
            {
                id = idfacturagasto,
                idtipogasto = obj.id_ref_tipo_gasto_facturas,
                observaciones = obj.observacion_gasto
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string ActualizarDatosTiga(int idfacgasto, int tipogasto, string observaciones)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<vw_factura_digital_gasto_total> gastosactuales = (List<vw_factura_digital_gasto_total>)Session["tiga"];
            vw_factura_digital_gasto_total obj = gastosactuales.Where(l => l.id_factura_digital_gasto == idfacgasto).FirstOrDefault();
            gastosactuales.Remove(obj);
            obj.id_ref_tipo_gasto_facturas = tipogasto;
            obj.observacion_gasto = observaciones;
            obj.tipo_gasto = Model.Getref_tipo_gasto_facturas(ref MsgRes).Where(l => l.id_ref_tipo_gasto_facturas == tipogasto).FirstOrDefault().descripcion;
            gastosactuales.Add(obj);

            Session["tiga"] = gastosactuales;
            string result = "";
            foreach (var item in gastosactuales)
            {
                result += "<tr>";
                result += "<td>" + item.tipo_gasto + "</td>";
                result += "<td>" + item.observacion_gasto + "</td>";
                result += "<td><button class='btn button_Asalud_Aceptar btn-sm' type='button' onclick='EditarTiga(" + item.id_factura_digital_gasto + ")'>Editar</button></td>";
                result += "</tr>";
            }

            return result;
        }
        public string ActualizarDatosTiga2(int idfacgasto, int tipogasto, string observaciones)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<vw_factura_digital_gasto_total> gastosactuales = (List<vw_factura_digital_gasto_total>)Session["tiga2"];
            vw_factura_digital_gasto_total obj = gastosactuales.Where(l => l.id_factura_digital_gasto == idfacgasto).FirstOrDefault();
            gastosactuales.Remove(obj);
            obj.id_ref_tipo_gasto_facturas = tipogasto;
            obj.observacion_gasto = observaciones;
            obj.tipo_gasto = Model.Getref_tipo_gasto_facturas(ref MsgRes).Where(l => l.id_ref_tipo_gasto_facturas == tipogasto).FirstOrDefault().descripcion;
            gastosactuales.Add(obj);

            Session["tiga2"] = gastosactuales;
            string result = "";
            foreach (var item in gastosactuales)
            {
                result += "<tr>";
                result += "<td>" + item.tipo_gasto + "</td>";
                result += "<td>" + item.observacion_gasto + "</td>";
                result += "<td><button class='btn button_Asalud_Aceptar btn-sm' type='button' onclick='EditarTiga(" + item.id_factura_digital_gasto + ")'>Editar</button></td>";
                result += "</tr>";
            }

            return result;
        }

        public ActionResult GuardarCambiosTiga()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<vw_factura_digital_gasto_total> gastosactuales = (List<vw_factura_digital_gasto_total>)Session["tiga"];
            foreach (var item in gastosactuales)
            {
                Model.ActualizarGestionFacturadigitalGasto(item, ref MsgRes);
            }

            return RedirectToAction("TableroControlAsignaciones", "RadicacionElectonica", new { rta = 1 });
        }

        public ActionResult GuardarCambiosTiga2()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<vw_factura_digital_gasto_total> gastosactuales = (List<vw_factura_digital_gasto_total>)Session["tiga2"];
            foreach (var item in gastosactuales)
            {
                Model.ActualizarGestionFacturadigitalGasto(item, ref MsgRes);

            }

            return RedirectToAction("TableroControlAsignacionesTiga", "RadicacionElectonica", new { rta = 1 });
        }

        public ActionResult TableroControlAlertas()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            ViewBag.meses = BusClass.meses();
            ViewBag.regionales = BusClass.GetRefRegion();

            return View(Model);
        }

        public JsonResult SaveGestionAlertas(Int32 regional, DateTime fechaFin, String año, Int32 mes, DateTime fechaIni2)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            String mensaje = "";

            String fecha = fechaFin.Date.ToString("MM/dd/yyyy");
            String fechaFinok = fecha + " " + "00:00:00.000";
            var alerta = "";

            List<managementprestadores_alertas_activasResult> result = new List<managementprestadores_alertas_activasResult>();

            result = Model.GetConsultaAlertasactivas();
            //result = result.Where(x => x.ALERTA == "SI").ToList();
            result = result.Where(x => x.año == año && x.mes == mes).ToList();
            foreach (var item in result)
            {
                if (item.id_regional == regional)
                {
                    if (item.año == año)
                    {
                        if (item.mes == mes)
                        {
                            alerta = "SI";
                        }
                    }

                }

            }
            if (alerta == "SI")
            {
                mensaje = "*---ERROR -- - *" + " Regional y periodo ya tiene una validación activa...";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText = "INSERT INTO ref_validacion_fechas(fecha_inicial,fecha_final,estado,id_regional,año,mes)values(@fecha_inicial,@fecha_final,@estado,@id_regional,@año,@mes)";
                        command.Parameters.AddWithValue("@fecha_inicial", fechaIni2);
                        command.Parameters.AddWithValue("@fecha_final", fechaFinok);
                        command.Parameters.AddWithValue("@estado", Convert.ToBoolean(1));
                        command.Parameters.AddWithValue("@id_regional", regional);
                        command.Parameters.AddWithValue("@año", año);
                        command.Parameters.AddWithValue("@mes", mes);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                    }
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    mensaje = "*---ERROR -- - *" + ex.Message;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult GetAlertas(int? page, int? limit)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managementprestadores_alertas_activasResult> result = new List<managementprestadores_alertas_activasResult>();

            result = Model.GetConsultaAlertasactivas();
            //result = result.Where(x => x.ALERTA == "SI").ToList();


            var query = result.Select(item => new managementprestadores_alertas_activasResult
            {
                id_ref_validacion_fechas = item.id_ref_validacion_fechas,
                fecha_inicial = item.fecha_inicial,
                fecha_final = item.fecha_final,
                id_regional = item.id_regional,
                nombre_regional = item.nombre_regional,

            });

            List<managementprestadores_alertas_activasResult> records = new List<managementprestadores_alertas_activasResult>();

            Session["ListaFacturas3"] = query.ToList();

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

        public JsonResult InactivarAlerta(Int32 ID)
        {
            String mensaje = "";
            Models.Concurrencia.PlandeMejora Model = new Models.Concurrencia.PlandeMejora();
            string result = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE ref_validacion_fechas SET estado =@estado Where id_ref_validacion_fechas = @id_ref_validacion_fechas";
                    command.Parameters.AddWithValue("@estado", Convert.ToBoolean(0));
                    command.Parameters.AddWithValue("@id_ref_validacion_fechas", ID);

                    connection.Open();
                    command.CommandTimeout = 120;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }


        }

        //public ActionResult ModificacionesFactura(String txtnumfactura)
        //{
        //    Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
        //    List<managmentprestadoresfacturasgestionadasdtllResult> result = new List<managmentprestadoresfacturasgestionadasdtllResult>();
        //    if (!string.IsNullOrEmpty(txtnumfactura))
        //    {
        //        result = Model.GetListFacturasByNumFactura(txtnumfactura);
        //    }
        //    ViewBag.numfactura = txtnumfactura;
        //    return View(result);
        //}

        public ActionResult ModificacionesFactura(int? idFactura, String numFac, String nit, String prestador, String sap)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresfacturasgestionadasdtllCompletaResult> result = new List<managmentprestadoresfacturasgestionadasdtllCompletaResult>();

            try
            {
                result = Model.GetListFacturasByNumFacturaCompleta(numFac, nit, prestador, sap, idFactura);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.numfactura = numFac;
            ViewBag.idFactura = idFactura;

            return View(result);
        }

        public JsonResult IngresoSoportes(int id_cargue_dtll, string num_factura, int id_cargue, List<HttpPostedFileBase> file)
        {

            String mensaje = "";
            string archivo = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<Models.CuentasMedicas.SoportesClinicos> listasoportes = new List<Models.CuentasMedicas.SoportesClinicos>();

            ViewBag.af = false;
            try
            {
                file.RemoveAt(0);

                foreach (var item in file)
                {
                    string strRetorno = string.Empty;
                    StringBuilder sbRutaDefinitiva;
                    string strRutaDefinitiva = string.Empty;
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPrestadores"];
                    sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                    string nombreSintilde = Regex.Replace(item.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");

                    string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + nombreSintilde);
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                    MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                    string strError = string.Empty;

                    DateTime fecha = DateTime.Now;
                    archivo = string.Empty;
                    String carpeta = "";

                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                    {
                        carpeta = "Facturacion";
                    }
                    else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                    {
                        carpeta = "FacturacionPruebas";
                    }
                    ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + id_cargue + "\\" + num_factura);

                    Models.CuentasMedicas.SoportesClinicos objGD = new Models.CuentasMedicas.SoportesClinicos();

                    var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                    archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                    fecha, nombre, Path.GetExtension(item.FileName));

                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);


                    item.SaveAs(archivo);

                    byte[] ExcelData = null;
                    using (var binaryReader = new BinaryReader(item.InputStream))
                    {
                        ExcelData = binaryReader.ReadBytes(item.ContentLength);
                    }
                    //objGD.documento = ExcelData;

                    objGD.id_cargue_base = id_cargue;
                    objGD.id_cargue_dtll = id_cargue_dtll;
                    objGD.num_factura = num_factura;
                    objGD.nom_documento = item.FileName;
                    objGD.ruta = Convert.ToString(archivo);
                    objGD.fecha_ingreso = DateTime.Now;
                    objGD.usuario = SesionVar.UserName;

                    listasoportes.Add(objGD);

                }

                Model.IngresoDocsSoportes(listasoportes, connectionString, ref MsgRes);
                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje, id = id_cargue_dtll, id2 = num_factura, id3 = id_cargue }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                mensaje = "ERROR EL INGRESO." + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult ModificarFactura(int idlote, int idfactura, string numanteriorfactura, string numnuevofactura, decimal valoranteriorfactura, decimal valornuevofactura)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE rips_af_cargue_dtll SET num_factura = @num_factura, valor_neto = @valor_neto Where id_af = @idfact";
                    command.Parameters.AddWithValue("@idfact", idfactura);
                    command.Parameters.AddWithValue("@num_factura", numnuevofactura);
                    command.Parameters.AddWithValue("@valor_neto", valornuevofactura);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                InsertarLogModificacionFactura(idlote, idfactura, numanteriorfactura, numnuevofactura, valoranteriorfactura, valornuevofactura);

                return Json(new { success = true, message = "Ingreso exitoso!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }



            return Json(0);
        }

        public JsonResult ValidarEvistenciaFactura(int idfactura, string numnuevofactura, string numidprestador)
        {
            List<getfacturabynumfactura_idprestadorResult> List = BusClass.ValidarEvistenciaFactura(idfactura, numnuevofactura, numidprestador);
            if (List.Count > 0)
            {
                return Json(new { success = false, message = "Lo sentimos, ya se encuentra registrada una factura con el mismo número para este prestador." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public void InsertarLogModificacionFactura(int idlote, int idfactura, string numanteriorfactura, string numnuevofactura, decimal valoranteriorfactura, decimal valornuevofactura)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO log_modificacion_factura(id_af,id_cargue_base,num_factura_anterior,valor_neto_anterior,num_factura_nuevo,valor_neto_nuevo,fecha_digita,usuario_digita)values(@id_af,@id_cargue_base,@num_factura_anterior,@valor_neto_anterior,@num_factura_nuevo,@valor_neto_nuevo,@fecha_digita,@usuario_digita)";
                command.Parameters.AddWithValue("@id_af", idfactura);
                command.Parameters.AddWithValue("@id_cargue_base", idlote);
                command.Parameters.AddWithValue("@num_factura_anterior", numanteriorfactura);
                command.Parameters.AddWithValue("@valor_neto_anterior", valoranteriorfactura);
                command.Parameters.AddWithValue("@num_factura_nuevo", numnuevofactura);
                command.Parameters.AddWithValue("@valor_neto_nuevo", valornuevofactura);
                command.Parameters.AddWithValue("@fecha_digita", Convert.ToDateTime(DateTime.Now));
                command.Parameters.AddWithValue("@usuario_digita", SesionVar.UserName);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public ActionResult TableroControlFacrurasDEV_RECH()
        {
            List<managmentprestadoresfacturasDEV_RECHV2Result> List = new List<managmentprestadoresfacturasDEV_RECHV2Result>();
            List<managmentprestadoresfacturasDEV_RECHV2Result> List2 = new List<managmentprestadoresfacturasDEV_RECHV2Result>();

            List = BusClass.GetConsultaRechDevFacturaV2(null);

            if (SesionVar.ROL != "1")
            {
                List<sis_auditor_regional> auditorRegional = BusClass.GetRegionalAuditor().Where(l => l.id_auditor == SesionVar.IDUser).ToList();
                foreach (var item in auditorRegional)
                {
                    var facturas = List.Where(l => l.id_ref_regional == item.id_regional).ToList();
                    List2.AddRange(facturas);
                }
                List = List2;
            }

            ViewData["rta"] = 0;
            ViewData["msg"] = "";
            Session["ListFacturasDevRech"] = List;

            return View(List);
        }

        [HttpPost]
        public ActionResult TableroControlFacrurasDEV_RECH(int txtidfactura, string numfactura, int txtestadofactura, decimal valorneto)
        {
            List<managmentprestadoresfacturasDEV_RECHV2Result> List = new List<managmentprestadoresfacturasDEV_RECHV2Result>();
            List<managmentprestadoresfacturasDEV_RECHV2Result> List2 = new List<managmentprestadoresfacturasDEV_RECHV2Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = @estado_factura Where id_af = @idfact";
                    command.Parameters.AddWithValue("@idfact", txtidfactura);
                    command.Parameters.AddWithValue("@estado_factura", txtestadofactura);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                InsertarValoresCerrarRechazoDevolucion(txtidfactura, numfactura, valorneto);

                ViewData["rta"] = 1;
                if (txtestadofactura == 13)
                {
                    ViewData["msg"] = "Rechazo cerrado correctamente!";
                }
                else
                {
                    ViewData["msg"] = "Devolución cerrada correctamente! ";
                }
            }

            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["msg"] = "Ah ocurrido un error: " + ex.Message;
            }

            List = BusClass.GetConsultaRechDevFacturaV2(null);

            if (SesionVar.ROL != "1")
            {
                List<sis_auditor_regional> auditorRegional = BusClass.GetRegionalAuditor().Where(l => l.id_auditor == SesionVar.IDUser).ToList();
                foreach (var item in auditorRegional)
                {
                    var facturas = List.Where(l => l.id_ref_regional == item.id_regional).ToList();
                    List2.AddRange(facturas);
                }
                List = List2;
            }

            Session["ListFacturasDevRech"] = List;

            return View(List);
        }

        public void InsertarValoresCerrarRechazoDevolucion(int txtidfactura, string numfactura, decimal valorneto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO gestion_rechazos_devoluciones(id_af,num_nueva_factura,valor_nueva_factura,fecha_digita,usuario_digita)values(@id_af,@num_nueva_factura,@valor_nueva_factura,@fecha_digita,@usuario_digita)";
                command.Parameters.AddWithValue("@id_af", txtidfactura);
                command.Parameters.AddWithValue("@num_nueva_factura", numfactura);
                command.Parameters.AddWithValue("@valor_nueva_factura", valorneto);
                command.Parameters.AddWithValue("@fecha_digita", Convert.ToDateTime(DateTime.Now));
                command.Parameters.AddWithValue("@usuario_digita", SesionVar.UserName);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void ExcelReporteFacturasDev_Rech()
        {
            List<managmentprestadoresfacturasDEV_RECHV2Result> listareporte = (List<managmentprestadoresfacturasDEV_RECHV2Result>)Session["ListFacturasDevRech"];
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte");

            Sheet.Cells["A1:M1"].Style.Font.Bold = true;
            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A1:M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:M1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:M1"].Style.Font.Color.SetColor(Color.White);

            Sheet.Cells["A1"].Value = "Nómero Factura";
            Sheet.Cells["B1"].Value = "Id Lote";
            Sheet.Cells["C1"].Value = "Id Factura";
            Sheet.Cells["D1"].Value = "Valor";
            Sheet.Cells["E1"].Value = "Nit prestador";
            Sheet.Cells["F1"].Value = "Prestador";
            Sheet.Cells["G1"].Value = "Regional";
            Sheet.Cells["H1"].Value = "Estado";
            Sheet.Cells["I1"].Value = "Fecha recepción";
            Sheet.Cells["J1"].Value = "Fecha Rechazo";
            Sheet.Cells["K1"].Value = "Analista";
            Sheet.Cells["L1"].Value = "Fecha devolución";
            Sheet.Cells["M1"].Value = "Auditor";


            int row = 2;
            foreach (managmentprestadoresfacturasDEV_RECHV2Result item in listareporte)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.num_factura;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.id_cargue_base;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.id_cargue_dtll;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.valor_neto;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.num_id_prestador;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.nombre_prestador;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.nombre_regional;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.estado_des;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_recepcion_fac;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_rechazo;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.nom_analitica;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.fecha_devolucion;
                Sheet.Cells[string.Format("M{0}", row)].Value = item.nom_auditor;
                row++;
            }
            Sheet.Cells["A:M"].AutoFitColumns();


            Response.Clear();
            Response.BufferOutput = false;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteFacturasDev_Rech.xlsX");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public ActionResult TableroTrazabilidadFacturas()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            if (SesionVar.ROL != "1")
            {
                if (SesionVar.ROL == "21")
                {
                    ViewBag.opcion = 3;
                }
                else
                {
                    ViewBag.opcion = 2;
                }
            }
            else
            {
                ViewBag.opcion = 1;
            }
            ViewBag.ROL = SesionVar.ROL;
            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.ref_estado = Model.GetEstadoFacturas();
            //ViewBag.fecha = Convert.ToDateTime(DateTime.Now);

            return View(Model);
        }

        public JsonResult GetTableroTrazabilidad(DateTime? fechainicial, DateTime? fechafin, String nombre_prestador, String nit, String numFac, int? page, int? limit, string sortBy, string direction, string buscar)
        {
            string filtrosUsados = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresfacturasgestionadasTrazabilidadResult> records = new List<managmentprestadoresfacturasgestionadasTrazabilidadResult>();
            int total = 0;

            try
            {
                #region aquiñonesc 17-nov-2022 se usa para saber que filtros usan y que usuario lo hace

                if (!string.IsNullOrEmpty(buscar))
                {
                    log_busquedas_tableros_facturas obj = new log_busquedas_tableros_facturas();
                    obj.tablero_A_consultar = "Tablero trazabilidad facturas";
                    obj.usuario_digita = SesionVar.UserName;
                    obj.fecha_digita = DateTime.Now;

                    if (fechainicial != null)
                        filtrosUsados += "//Fecha recep inicial: " + fechainicial.Value.ToString("dd/MM/yyyy") + " ";
                    if (fechafin != null)
                        filtrosUsados += "//Fecha recep final: " + fechafin.Value.ToString("dd/MM/yyyy") + " ";
                    if (!string.IsNullOrEmpty(nombre_prestador))
                        filtrosUsados += "//prestador: " + nombre_prestador.ToString() + " ";
                    if (!string.IsNullOrEmpty(nit))
                        filtrosUsados += "//Nit prestador: " + nit.ToString() + " ";
                    if (!string.IsNullOrEmpty(numFac))
                        filtrosUsados += "//Número factura: " + numFac.ToString() + " ";

                    obj.parametros_buscados = filtrosUsados;
                    Model.InsertarLogBusquedaTableros(obj, ref MsgRes);
                }
                #endregion


                ObjectCache cache = MemoryCache.Default;
                List<managmentprestadoresfacturasgestionadasTrazabilidadResult> fileContents = cache["filecontents"] as List<managmentprestadoresfacturasgestionadasTrazabilidadResult>;

                if (nombre_prestador == "")
                {
                    nombre_prestador = null;
                }
                if (nit == "")
                {
                    nit = null;
                }
                if (numFac == "")
                {
                    numFac = null;
                }



                List<managmentprestadoresfacturasgestionadasTrazabilidadResult> result = new List<managmentprestadoresfacturasgestionadasTrazabilidadResult>();
                List<managmentprestadoresfacturasgestionadasTrazabilidadResult> lista = new List<managmentprestadoresfacturasgestionadasTrazabilidadResult>();
                List<managmentprestadoresfacturasgestionadasTrazabilidadResult> lista3 = new List<managmentprestadoresfacturasgestionadasTrazabilidadResult>();
                List<managmentprestadoresfacturasgestionadasTrazabilidadResult> listaok = new List<managmentprestadoresfacturasgestionadasTrazabilidadResult>();

                List<managmentprestadoresfacturasgestionadasTrazabilidadResult> ListaTotal = (List<managmentprestadoresfacturasgestionadasTrazabilidadResult>)Session["ListaFacturastrazabilidadTotal"];

                if (fechainicial == null && nombre_prestador == null && nit == null && numFac == null)
                {
                    listaok = new List<managmentprestadoresfacturasgestionadasTrazabilidadResult>();
                    Session["ListaFacturast3"] = listaok.ToList();
                    Session["ListaFacturastrazabilidadTotal"] = listaok.ToList();
                }
                else
                {
                    if (fileContents != null)
                    {
                        listaok = fileContents;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(nit) || !string.IsNullOrEmpty(numFac))
                        {
                            listaok = Model.GetGestionFacturasTrazabilidadV2(fechainicial, fechafin, null, null, nombre_prestador, nit, numFac);
                        }
                        else
                        {
                            listaok = Model.GetGestionFacturasTrazabilidad();
                            //Session["ListaFacturasTotal"] = listaok.ToList();
                            ViewBag.fecha = Convert.ToDateTime(DateTime.Now);
                            if (fileContents == null || fileContents.Count == 0)
                            {
                                CacheItemPolicy policy = new CacheItemPolicy();
                                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                                cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(180.0);
                                fileContents = listaok.ToList();

                                cache.Add("filecontents", fileContents, cacheItemPolicy);

                            }
                        }
                    }
                }

                if (SesionVar.ROL != "1")
                {
                    if (SesionVar.ROL == "21" || SesionVar.ROL == "20")
                    {
                        ViewBag.opcion = 3;
                        lista = listaok;
                    }
                    else
                    {
                        List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                        regional obj = new regional();
                        list = BusClass.GetRegionalAuditor();
                        list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                        foreach (var item in list)
                        {
                            result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
                            lista3 = lista3.Concat(result).ToList();
                        }

                        lista = lista3;
                        ViewBag.opcion = 2;
                    }
                }
                else
                {
                    lista = listaok;
                    ViewBag.opcion = 1;
                }

                var query = lista.Select(item => new managmentprestadoresfacturasgestionadasTrazabilidadResult
                {
                    id_cargue_base = item.id_cargue_base,
                    id_cargue_dtll = item.id_cargue_dtll,
                    prestador = item.prestador,
                    nombre_prestador = item.nombre_prestador,
                    num_id_prestador = item.num_id_prestador,
                    homologacion_nit = item.homologacion_nit,
                    homologacion_sap = item.homologacion_sap,
                    homologacion_razonSocial = item.homologacion_razonSocial,
                    num_factura = item.num_factura,
                    valor_neto = item.valor_neto,
                    estado_factura = item.estado_factura,
                    id_ref_ciudades = item.id_ref_ciudades,
                    ciudad = item.ciudad,
                    id_ref_regional = item.id_ref_regional,
                    nombre_regional = item.nombre_regional,
                    id_auditor_asignado = item.id_auditor_asignado,
                    nom_auditor = item.nom_auditor,
                    id_analista_gestiona = item.id_analista_gestiona,
                    nom_analitica = item.nom_analitica,
                    multiusuario = item.multiusuario,
                    id_diagnostico = item.id_diagnostico,
                    diagnostico = item.diagnostico,
                    fecha_recepcion_fac = item.fecha_recepcion_fac,
                    fecha_aprobacion = item.fecha_aprobacion,
                    estado_des = item.estado_des,
                    count_soportes = item.count_soportes,
                    count_soportes_zip = item.count_soportes_zip,
                    fecha_exp_factura = item.fecha_exp_factura,
                    tipoGastos = item.tipoGastos,
                    ruta = item.ruta,
                    valorGlosa = item.valorGlosa,
                    MotivosGlosa = item.MotivosGlosa,
                    Observaciones = item.Observaciones,
                    estado_aprobada = item.estado_aprobada,
                    motivos_rechazo = item.motivos_rechazo,
                    fecha_asignacion_auditor = item.fecha_asignacion_auditor,
                    dias_procesamiento = item.dias_procesamiento,
                    contabilizado_factura = item.contabilizado_factura,
                    contabilizado_documento = item.contabilizado_documento,
                    contabilizado_fecha = item.contabilizado_fecha,
                    observacion_rechazo = item.observacion_rechazo,
                    cod_sap = item.cod_sap,
                    nombre_prestador_suis = item.nombre_prestador_suis,
                    nit_prestador_suis = item.nit_prestador_suis,
                });

                DateTime fechaIni = Convert.ToDateTime(fechainicial);
                DateTime fechaFin = Convert.ToDateTime(fechafin);

                if (fechainicial != null)
                {
                    query = query.Where(x => x.fecha_recepcion_fac >= fechaIni).ToList();
                    query = query.Where(x => x.fecha_recepcion_fac <= fechaFin).ToList();

                }

                if (!string.IsNullOrWhiteSpace(nombre_prestador))
                {
                    query = query.Where(q => q.nombre_prestador != null);
                    query = query.Where(q => q.nombre_prestador.Contains(nombre_prestador) || (q.homologacion_razonSocial != null && (q.homologacion_razonSocial.Contains(nombre_prestador))));
                }

                if (!string.IsNullOrWhiteSpace(nit))
                {
                    query = query.Where(q => q.num_id_prestador == nit || q.homologacion_nit == nit);
                }
                if (!string.IsNullOrWhiteSpace(numFac))
                {
                    query = query.Where(q => q.num_factura.Contains(numFac));
                }


                if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
                {
                    if (direction.Trim().ToLower() == "asc")
                    {
                        switch (sortBy.Trim().ToLower())
                        {
                            case "id_cargue_base":
                                query = query.OrderBy(q => q.id_cargue_base);
                                break;
                            case "num_factura":
                                query = query.OrderBy(q => q.num_factura);
                                break;
                            case "valor_neto":
                                query = query.OrderBy(q => q.valor_neto);
                                break;
                            case "fecha_recepcion_fac":
                                query = query.OrderBy(q => q.fecha_recepcion_fac);
                                break;
                        }
                    }
                    else
                    {
                        switch (sortBy.Trim().ToLower())
                        {
                            case "id_cargue_base":
                                query = query.OrderByDescending(q => q.id_cargue_base);
                                break;
                            case "num_factura":
                                query = query.OrderByDescending(q => q.num_factura);
                                break;
                            case "valor_neto":
                                query = query.OrderByDescending(q => q.valor_neto);
                                break;
                            case "fecha_recepcion_fac":
                                query = query.OrderByDescending(q => q.fecha_recepcion_fac);
                                break;
                        }
                    }
                }
                else
                {
                    query = query.OrderBy(q => q.fecha_recepcion_fac);
                }


                Session["ListaFacturast3"] = query.ToList();

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
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult ExportarLstTableroTrazabilidad(DateTime? fechainicial, DateTime? fechafin, String nombre_prestador, String nit, String numFac, string descarga)
        {
            ExcelPackage package = new ExcelPackage();

            try
            {
                List<managmentprestadoresfacturasgestionadasTrazabilidadResult> ListaSession = (List<managmentprestadoresfacturasgestionadasTrazabilidadResult>)Session["ListaFacturast3"];
                List<managmentprestadoresfacturasgestionadasTrazabilidadResult> result2 = new List<managmentprestadoresfacturasgestionadasTrazabilidadResult>();

                if (ListaSession.Count != 0)
                {
                    result2 = ListaSession.ToList();
                }

                // Pass your ef data to method
                package = GenerateExcelFileFacturasGestionadas2(result2.ToList());
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var fileDownloadName = String.Format("Consolidado" + Convert.ToDateTime(DateTime.Now) + ".xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        private static ExcelPackage GenerateExcelFileFacturasGestionadas2(List<managmentprestadoresfacturasgestionadasTrazabilidadResult> datasource)
        {

            ExcelPackage pck = new ExcelPackage();

            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

            // Sets Headers
            ws.Cells[1, 1].Value = "Id cargue base";
            ws.Cells[1, 2].Value = "NIT";
            ws.Cells[1, 3].Value = "Codigo SAP";
            ws.Cells[1, 4].Value = "Nombre Prestador";
            ws.Cells[1, 5].Value = "Razon social ECP";
            ws.Cells[1, 6].Value = "Num factura";
            ws.Cells[1, 7].Value = "Valor neto";
            ws.Cells[1, 8].Value = "Ciudad";
            ws.Cells[1, 9].Value = "Regional";
            ws.Cells[1, 10].Value = "Fecha Recepción";
            ws.Cells[1, 11].Value = "Estado Actual";
            ws.Cells[1, 12].Value = "Fecha Aprobación";
            ws.Cells[1, 13].Value = "Analista";
            ws.Cells[1, 14].Value = "Auditor";
            ws.Cells[1, 15].Value = "Tipos gasto aplicado";
            ws.Cells[1, 16].Value = "Motivo Glosa";
            ws.Cells[1, 17].Value = "Observación";
            ws.Cells[1, 18].Value = "Valor Glosa";
            ws.Cells[1, 19].Value = "Motivos Rechazo";
            ws.Cells[1, 20].Value = "Fecha Rechazo";
            ws.Cells[1, 21].Value = "Fecha asignación auditor";
            ws.Cells[1, 22].Value = "Dias procesamiento factura en proceso";
            ws.Cells[1, 23].Value = "Contabilizado factura";
            ws.Cells[1, 24].Value = "Contabilizado documento";
            ws.Cells[1, 25].Value = "Contabilizado fecha";
            ws.Cells[1, 26].Value = "Dias cumplidos de procesamiento";

            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id_cargue_base;

                if (datasource.ElementAt(i).homologacion_nit == null)
                {
                    ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).num_id_prestador;
                }
                else
                {
                    ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).homologacion_nit;
                }
                if (datasource.ElementAt(i).homologacion_sap == null)
                {
                    ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).cod_sap;
                }
                else
                {
                    ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).homologacion_sap;
                }

                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).nombre_prestador;

                if (datasource.ElementAt(i).homologacion_razonSocial == null)
                {
                    ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).nombre_prestador_suis;
                }
                else
                {
                    ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).homologacion_razonSocial;
                }
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).num_factura;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).valor_neto;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).ciudad;
                ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).nombre_regional;
                ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).fecha_recepcion_fac;
                ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).estado_des;
                ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).fecha_aprobacion;
                ws.Cells[i + 2, 13].Value = datasource.ElementAt(i).nom_analitica;
                ws.Cells[i + 2, 14].Value = datasource.ElementAt(i).nom_auditor;
                ws.Cells[i + 2, 15].Value = datasource.ElementAt(i).tipoGastos;
                ws.Cells[i + 2, 16].Value = datasource.ElementAt(i).MotivosGlosa;
                ws.Cells[i + 2, 17].Value = datasource.ElementAt(i).Observaciones;
                ws.Cells[i + 2, 18].Value = datasource.ElementAt(i).valorGlosa;
                ws.Cells[i + 2, 19].Value = datasource.ElementAt(i).motivos_rechazo;

                ws.Cells[i + 2, 20].Value = "";
                ws.Cells[i + 2, 21].Value = datasource.ElementAt(i).fecha_asignacion_auditor;
                if (datasource.ElementAt(i).estado_factura != 6 && datasource.ElementAt(i).estado_factura != 11 && datasource.ElementAt(i).estado_factura != 12)
                {
                    ws.Cells[i + 2, 22].Value = datasource.ElementAt(i).dias_procesamiento;
                }
                else
                {
                    ws.Cells[i + 2, 22].Value = "";
                }

                ws.Cells[i + 2, 23].Value = datasource.ElementAt(i).contabilizado_factura;
                ws.Cells[i + 2, 24].Value = datasource.ElementAt(i).contabilizado_documento;
                ws.Cells[i + 2, 25].Value = datasource.ElementAt(i).contabilizado_fecha;
                if (datasource.ElementAt(i).estado_factura == 6 || datasource.ElementAt(i).estado_factura == 11 || datasource.ElementAt(i).estado_factura == 12)
                {
                    ws.Cells[i + 2, 26].Value = datasource.ElementAt(i).dias_procesamiento;
                }
                else
                {
                    ws.Cells[i + 2, 26].Value = "";
                }

                ws.Cells[i + 2, 10].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                ws.Cells[i + 2, 12].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                ws.Cells[i + 2, 20].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                ws.Cells[i + 2, 21].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                ws.Cells[i + 2, 25].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            }

            ws.Cells["A:Z"].AutoFitColumns();
            using (ExcelRange rng = ws.Cells["A1:Z1"])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                rng.Style.Font.Color.SetColor(Color.White);
            }
            return pck;
        }

        public ActionResult CartaRechazos()
        {
            return View();
        }

        public JsonResult LlenadoCartaRechazos(string factura)
        {
            String mensaje = "";
            string datos = "";

            try
            {
                Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
                List<managmentprestadoresFacturasRechazadasResult> result = Model.GetFacturasRechazadas(factura, ref MsgRes);

                int i = 0;

                if (result.Count != 0)
                {
                    foreach (var fac in result)
                    {
                        if (fac.estado_factura == 3 || fac.estado_factura == 13)
                        {
                            i += 1;
                            datos += "<tr>";
                            //datos += "<td>" + i + "</td>";
                            datos += "<td>" + fac.id_cargue_dtll + "</td>";
                            datos += "<td>" + fac.num_factura + "</td>";
                            datos += "<td>" + fac.num_id_prestador + "</td>";
                            datos += "<td>" + fac.nombre_prestador + "</td>";
                            datos += "<td>" + fac.valor_neto + "</td>";
                            datos += "<td class='text-center''><a class='button_Asalud_Reporte' href='javascript:ReporteFacturaRechazada(" + fac.id_cargue_dtll + ")'> Reporte </a></td>";
                            datos += "</tr>";
                        }
                        else
                        {
                            mensaje = "ESTA FACTURA NO ESTA RECHAZADA";
                            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    var data = new
                    {
                        tabla = datos,

                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "NO EXISTE FACTURA RECHAZADA";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GestionarAnalistas()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            return View(Model);
        }

        public ActionResult Selection_Analista_sin([DataSourceRequest] DataSourceRequest request)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturas_analistasResult> listaok = new List<managmentprestadoresFacturas_analistasResult>();
            List<managmentprestadoresFacturas_analistasResult> result = new List<managmentprestadoresFacturas_analistasResult>();
            List<managmentprestadoresFacturas_analistasResult> result2 = new List<managmentprestadoresFacturas_analistasResult>();
            List<managmentprestadoresFacturas_analistasResult> listaFinal = new List<managmentprestadoresFacturas_analistasResult>();

            listaok = Model.prestadoresFacturas_analistas();
            listaok = listaok.Where(x => x.Id_analista == null).ToList();
            if (SesionVar.ROL != "1")
            {
                List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                regional obj = new regional();
                list = BusClass.GetRegionalAuditor();
                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                foreach (var item in list)
                {
                    result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
                    result2 = result2.Concat(result).ToList();
                }

                listaFinal = result2;
                ViewBag.opcion = 2;
            }
            else
            {
                listaFinal = listaok.ToList();
            }


            var lista = new object();

            lista = (from item in listaFinal
                     select new
                     {
                         prestador = item.prestador,
                         nombre_prestador = item.nombre_prestador,
                         id_regional = item.id_ref_regional,
                         nombre_regional = item.nombre_regional,
                         Id_analista = item.Id_analista,
                         NombreAnalista = item.NombreAnalista,


                     }).Distinct().OrderByDescending(f => f.prestador);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Selection_Analista_con([DataSourceRequest] DataSourceRequest request)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturas_analistasResult> listaok = new List<managmentprestadoresFacturas_analistasResult>();
            List<managmentprestadoresFacturas_analistasResult> result = new List<managmentprestadoresFacturas_analistasResult>();
            List<managmentprestadoresFacturas_analistasResult> result2 = new List<managmentprestadoresFacturas_analistasResult>();
            List<managmentprestadoresFacturas_analistasResult> listaFinal = new List<managmentprestadoresFacturas_analistasResult>();

            listaok = Model.prestadoresFacturas_analistas();
            listaok = listaok.Where(x => x.activo == 1 && x.Id_analista != null).ToList();

            if (SesionVar.ROL != "1")
            {
                List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                regional obj = new regional();
                list = BusClass.GetRegionalAuditor();
                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                foreach (var item in list)
                {
                    result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
                    result2 = result2.Concat(result).ToList();
                }

                listaFinal = result2;
                ViewBag.opcion = 2;
            }
            else
            {
                listaFinal = listaok.ToList();
            }


            var lista = new object();

            lista = (from item in listaFinal
                     select new
                     {
                         prestador = item.prestador,
                         nombre_prestador = item.nombre_prestador,
                         id_regional = item.id_ref_regional,
                         nombre_regional = item.nombre_regional,
                         Id_analista = item.Id_analista,
                         NombreAnalista = item.NombreAnalista,
                         id_ref_cuentas_medicas_analista = item.id_ref_cuentas_medicas_analista,

                     }).Distinct().OrderByDescending(f => f.prestador);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Selection_Auditor_sin([DataSourceRequest] DataSourceRequest request)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturas_auditoresResult> listaok = new List<managmentprestadoresFacturas_auditoresResult>();
            List<managmentprestadoresFacturas_auditoresResult> result = new List<managmentprestadoresFacturas_auditoresResult>();
            List<managmentprestadoresFacturas_auditoresResult> result2 = new List<managmentprestadoresFacturas_auditoresResult>();
            List<managmentprestadoresFacturas_auditoresResult> listaFinal = new List<managmentprestadoresFacturas_auditoresResult>();
            var lista = new object();

            try
            {

                listaok = Model.prestadoresFacturas_auditores();
                listaok = listaok.Where(x => x.Id_auditor == null).ToList();
                if (SesionVar.ROL != "1")
                {
                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                    regional obj = new regional();
                    list = BusClass.GetRegionalAuditor();
                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                    foreach (var item in list)
                    {
                        result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
                        result2 = result2.Concat(result).ToList();
                    }

                    listaFinal = result2;
                    ViewBag.opcion = 2;
                }
                else
                {
                    listaFinal = listaok.ToList();
                }



                lista = (from item in listaFinal
                         select new
                         {
                             prestador = item.prestador,
                             nombre_prestador = item.nombre_prestador,
                             id_regional = item.id_ref_regional,
                             nombre_regional = item.nombre_regional,
                             Id_auditor = item.Id_auditor,
                             NombreAuditor = item.NombreAuditor,


                         }).Distinct().OrderByDescending(f => f.prestador);

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Selection_Auditor_con([DataSourceRequest] DataSourceRequest request)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturas_auditoresResult> listaok = new List<managmentprestadoresFacturas_auditoresResult>();
            List<managmentprestadoresFacturas_auditoresResult> result = new List<managmentprestadoresFacturas_auditoresResult>();
            List<managmentprestadoresFacturas_auditoresResult> result2 = new List<managmentprestadoresFacturas_auditoresResult>();
            List<managmentprestadoresFacturas_auditoresResult> listaFinal = new List<managmentprestadoresFacturas_auditoresResult>();
            var lista = new object();

            try
            {

                listaok = Model.prestadoresFacturas_auditores();
                listaok = listaok.Where(x => x.Id_auditor != null).ToList();
                if (SesionVar.ROL != "1")
                {
                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                    regional obj = new regional();
                    list = BusClass.GetRegionalAuditor();
                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                    foreach (var item in list)
                    {
                        result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
                        result2 = result2.Concat(result).ToList();
                    }

                    listaFinal = result2;
                    ViewBag.opcion = 2;
                }
                else
                {
                    listaFinal = listaok.ToList();
                }

                lista = (from item in listaFinal
                         select new
                         {
                             prestador = item.prestador,
                             nombre_prestador = item.nombre_prestador,
                             id_regional = item.id_ref_regional,
                             nombre_regional = item.nombre_regional,
                             Id_auditor = item.Id_auditor,
                             NombreAuditor = item.NombreAuditor,
                             id_ref_cuentas_medicas_auditores = item.id_ref_cuentas_medicas_auditores,

                         }).Distinct().OrderByDescending(f => f.prestador);
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Selection_AnalistaTotal(Models.CuentasMedicas.RadicacionElectronica Mode)
        {

            DateTime fecha_ini = Mode.fecha_inicial;
            DateTime fecha_fin = Mode.fecha_final;

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<Management_Lotes_totales_con_analistaResult> listaok = new List<Management_Lotes_totales_con_analistaResult>();
            List<Management_Lotes_totales_con_analistaResult> result = new List<Management_Lotes_totales_con_analistaResult>();
            List<Management_Lotes_totales_con_analistaResult> result2 = new List<Management_Lotes_totales_con_analistaResult>();
            List<Management_Lotes_totales_con_analistaResult> listaFinal = new List<Management_Lotes_totales_con_analistaResult>();
            var lista = new object();

            try
            {

                listaok = Model.GetLotesAnalistaTotal(fecha_ini, fecha_fin, ref MsgRes);

                if (SesionVar.ROL != "1")
                {
                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                    regional obj = new regional();
                    list = BusClass.GetRegionalAuditor();
                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                    foreach (var item in list)
                    {
                        result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
                        result2 = result2.Concat(result).ToList();
                    }

                    listaFinal = result2;
                    ViewBag.opcion = 2;
                }
                else
                {
                    listaFinal = listaok.ToList();
                }

                var resultado = from a in listaFinal
                                group a by new { a.prestador, a.Nombre, a.nom_analista, a.id_usuario } into g
                                select new
                                {
                                    Nombre = g.Key.Nombre,
                                    prestador = g.Key.prestador,
                                    nom_analista = g.Key.nom_analista,
                                    id_usuario = g.Key.id_usuario,
                                    Total = g.Sum(y => y.count_facturas_totales)
                                };

                lista = resultado;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Selection_AnalistaTotalDtll(Models.CuentasMedicas.RadicacionElectronica Mode)
        {
            Int32 ID = Mode.id_prestador;
            DateTime fecha_ini = Mode.fecha_inicial;
            DateTime fecha_fin = Mode.fecha_final;
            Int32 ID2 = Mode.id_analista;

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<Management_Lotes_totales_con_analistaResult> listaok = new List<Management_Lotes_totales_con_analistaResult>();
            List<Management_Lotes_totales_con_analistaResult> result = new List<Management_Lotes_totales_con_analistaResult>();
            List<Management_Lotes_totales_con_analistaResult> result2 = new List<Management_Lotes_totales_con_analistaResult>();
            List<Management_Lotes_totales_con_analistaResult> listaFinal = new List<Management_Lotes_totales_con_analistaResult>();
            var lista = new object();

            try
            {

                listaok = Model.GetLotesAnalistaTotal(fecha_ini, fecha_fin, ref MsgRes);
                listaok = listaok.Where(x => x.prestador == ID).ToList();
                listaok = listaok.Where(x => x.id_usuario == ID2).ToList();

                if (SesionVar.ROL != "1")
                {
                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                    regional obj = new regional();
                    list = BusClass.GetRegionalAuditor();
                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                    foreach (var item in list)
                    {
                        result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
                        result2 = result2.Concat(result).ToList();
                    }

                    listaFinal = result2;
                    ViewBag.opcion = 2;
                }
                else
                {
                    listaFinal = listaok.ToList();

                }

                lista = (from item in listaFinal
                         select new
                         {
                             id_cargue_base = item.id_cargue_base,
                             prestador = item.Nombre,
                             Fecha = item.fecha.ToString("dd/MM/yyyy"),
                             nom_analista = item.nom_analista,
                             facturas_totales = item.count_facturas_totales,
                             facturas_pendientes = item.count_facturas_pendientes

                         }).Distinct().OrderByDescending(f => f.prestador);

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAnalistaTotal(DateTime? fechainicial, DateTime? fechafin, int? page, int? limit)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<Management_Lotes_totales_con_analistaResult> listaok = new List<Management_Lotes_totales_con_analistaResult>();
            List<Management_Lotes_totales_con_analistaResult> result = new List<Management_Lotes_totales_con_analistaResult>();
            List<Management_Lotes_totales_con_analistaResult> result2 = new List<Management_Lotes_totales_con_analistaResult>();
            List<Management_Lotes_totales_con_analistaResult> listaFinal = new List<Management_Lotes_totales_con_analistaResult>();

            if (fechainicial != null && fechafin != null)
            {
                DateTime fecha_ini = fechainicial.Value;
                DateTime fecha_fin = fechafin.Value;
                listaok = Model.GetLotesAnalistaTotal(fecha_ini, fecha_fin, ref MsgRes);

                if (SesionVar.ROL != "1")
                {
                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                    regional obj = new regional();
                    list = BusClass.GetRegionalAuditor();
                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                    foreach (var item in list)
                    {
                        result = listaok.Where(x => x.id_ref_regional == item.id_regional).ToList();
                        result2 = result2.Concat(result).ToList();
                    }

                    listaFinal = result2;
                    ViewBag.opcion = 2;
                }
                else
                {
                    listaFinal = listaok.ToList();

                }
            }

            var resultado = from a in listaFinal
                            group a by new { a.prestador, a.Nombre, a.nom_analista, a.id_usuario } into g
                            select new
                            {
                                Nombre = g.Key.Nombre,
                                prestador = g.Key.prestador,
                                nom_analista = g.Key.nom_analista,
                                id_usuario = g.Key.id_usuario,
                                Total = g.Sum(y => y.count_facturas_totales)

                            };

            var lista = resultado;

            var query = lista.Select(item => new Management_Lotes_totales_con_analistaResult
            {
                Nombre = item.Nombre,
                prestador = item.prestador,
                nom_analista = item.nom_analista,
                id_usuario = item.id_usuario,


            });

            List<Management_Lotes_totales_con_analistaResult> records = new List<Management_Lotes_totales_con_analistaResult>();

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

        public PartialViewResult GestionCasoAnalistaIntermedio(Int32 ID, Int32 ID2)
        {
            ViewBag.regional = ID;
            ViewBag.prestador = ID2;

            return PartialView();
        }

        public PartialViewResult GestionCasoAnalistaSin(Int32 ID, Int32 ID2)
        {
            ViewBag.regional = ID;
            ViewBag.prestador = ID2;
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            ViewBag.listanalistas = BusClass.GetListAnalistas().Where(l => l.id_regional == ID);

            return PartialView();
        }

        public PartialViewResult GestionCasoAnalistaMulti(Int32 ID, Int32 ID2)
        {
            ViewBag.regional = ID;
            ViewBag.prestador = ID2;
            List<vw_analistas_recepcion> lista = new List<vw_analistas_recepcion>();
            lista = BusClass.GetListAnalistas().Where(l => l.id_regional == ID).ToList();
            ViewBag.listanalistas = lista;

            return PartialView();
        }

        public ActionResult GetAnalistaMulti([DataSourceRequest] DataSourceRequest request, Int32? ID, Int32? ID2)
        {
            List<vw_analistas_recepcion> lista = new List<vw_analistas_recepcion>();
            lista = BusClass.GetListAnalistas().Where(l => l.id_regional == ID).ToList();
            ViewBag.listanalistas = lista;

            var listaObj = new object();

            listaObj = (from item in lista
                        select new
                        {
                            id_usuario = item.id_usuario,
                            nom_analista = item.nom_analista,
                            id_regional = item.id_regional,
                            id_estado = item.id_estado,

                        }).Distinct().OrderByDescending(f => f.id_usuario);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IngresarAnalistas(List<int> ListaIds, int prestador, int regional)
        {
            var mensaje = "";
            var conteoError = 0;
            var nombreAnalista = "";
            List<ref_cuentas_medicas_analista> objeto = new List<ref_cuentas_medicas_analista>();

            try
            {
                if (ListaIds != null)
                {
                    foreach (var item in ListaIds)
                    {
                        ref_cuentas_medicas_analista obj = new ref_cuentas_medicas_analista();

                        obj.id_usuario = item;
                        obj.id_prestador = prestador;
                        obj.id_regional = regional;

                        ref_cuentas_medicas_analista resultado = new ref_cuentas_medicas_analista();
                        resultado = BusClass.TraerAnalistasIngresados(obj);

                        if (resultado != null)
                        {
                            vw_analistas_recepcion datos = new vw_analistas_recepcion();
                            datos = BusClass.TraerAnalista((int)resultado.id_usuario);
                            nombreAnalista = datos.nom_analista;
                            mensaje += "El analista: " + nombreAnalista + " ya está con este prestador y regional";
                            mensaje += "<br/>";
                            conteoError++;
                        }
                        else
                        {
                            obj.conteo = 0;
                            obj.id_estado = 1;
                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            objeto.Add(obj);
                        }
                    }

                    if (conteoError != 0)
                    {
                        return Json(new { success = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        try
                        {
                            var resultadoInsercion = BusClass.InsertarAnalistas(objeto);

                            if (resultadoInsercion != 0)
                            {
                                mensaje = "ANALISTAS ASIGNADOS CORRECTAMENTE";
                                return Json(new { success = true, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mensaje = "ERROR AL AÑADIR ANALISTAS";
                                return Json(new { success = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            mensaje = "ERROR AL AÑADIR ANALISTAS " + ex.Message;
                            return Json(new { success = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                        }

                    }

                }
                else
                {
                    mensaje = "DEBE AÑADIR ANALISTAS";
                    return Json(new { success = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL AÑADIR ANALISTAS " + ex.Message;
                return Json(new { success = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveAnalistaSin(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            try
            {
                ref_cuentas_medicas_analista obj = new ref_cuentas_medicas_analista();

                obj.id_usuario = Model.id_analista;
                obj.id_prestador = Model.id_prestador;
                obj.id_regional = Model.id_regional;
                obj.conteo = 0;
                obj.id_estado = 16;
                obj.activo = 1;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                Model.InsertarGestionAnalista(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE." + MsgRes.DescriptionResponse;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult GestionCasoAnalistaCon(Int32 ID, Int32 ID2)
        {
            ViewBag.id_ref_cuentas_medicas_analista = ID;

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            ViewBag.listanalistas = BusClass.GetListAnalistas().Where(l => l.id_regional == ID2);

            return PartialView();
        }
        public JsonResult SaveAnalistaCon(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            try
            {
                ref_cuentas_medicas_analista obj = new ref_cuentas_medicas_analista();

                obj.id_usuario = Model.id_analista;
                obj.id_ref_cuentas_medicas_analista = Model.id_ref_cuentas_medicas_analista;

                Model.ActualizaAnalistaAsignado(obj, ref MsgRes);


                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE." + MsgRes.DescriptionResponse;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public PartialViewResult GestionCasoAuditorSin(Int32 ID, Int32 ID2)
        {
            ViewBag.regional = ID;
            ViewBag.prestador = ID2;
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<vw_auditores_totales> list = new List<vw_auditores_totales>();

            list = Model.GetAuditorTotales(ref MsgRes);
            list = list.Where(x => x.id_regional == ID).ToList();

            ViewBag.Auditores = list;

            return PartialView();
        }
        public JsonResult SaveAuditorSin(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            try
            {
                ref_cuentas_medicas_auditores obj = new ref_cuentas_medicas_auditores();

                obj.id_usuario = Model.id_analista;
                obj.id_prestador = Model.id_prestador;
                obj.id_regional = Model.id_regional;


                Model.InsertarGestionAuditor(obj, ref MsgRes);


                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE." + MsgRes.DescriptionResponse;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }
        public PartialViewResult GestionCasoAuditorCon(Int32 ID, Int32 ID2)
        {
            ViewBag.id_ref_cuentas_medicas_auditores = ID;

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<vw_auditores_totales> list = new List<vw_auditores_totales>();

            list = Model.GetAuditorTotales(ref MsgRes);
            list = list.Where(x => x.id_regional == ID2).ToList();

            ViewBag.Auditores = list;
            return PartialView();
        }
        public JsonResult SaveAuditorCon(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            try
            {
                ref_cuentas_medicas_auditores obj = new ref_cuentas_medicas_auditores();

                obj.id_usuario = Model.id_analista;
                obj.id_ref_cuentas_medicas_auditores = Model.id_ref_cuentas_medicas_auditores;

                Model.ActualizaAuditorAsignado(obj, ref MsgRes);


                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE." + MsgRes.DescriptionResponse;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public PartialViewResult GestionarAnalistaAutom(int ID)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            var regional = 0;

            management_traerRegionalFacturaResult datosRegional = new management_traerRegionalFacturaResult();
            datosRegional = BusClass.TraerRegionalFacturas(ID);

            regional = (int)datosRegional.id_ref_regional;
            List<vw_analistas_recepcion> listanalistas = new List<vw_analistas_recepcion>();
            listanalistas = BusClass.GetListAnalistas().Where(x => x.id_regional == regional).ToList();

            regional obj = new regional();

            ViewBag.lista = listanalistas;
            ViewBag.id_cargue = ID;

            return PartialView(Model);
        }

        public JsonResult SaveGestionarAnalistaAutom(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            String mensaje = "";
            var Usuario = Convert.ToString(SesionVar.UserName);
            DateTime fecha = Convert.ToDateTime(DateTime.Now);

            ecop_gestion_factura_digital_control_cambios obj = new ecop_gestion_factura_digital_control_cambios();

            obj.observacion = Model.observaciones;
            obj.id_analista1 = 0;
            obj.id_analista2 = Model.id_analista2;
            obj.fecha_ingeso = fecha;
            obj.usuario_ingreso = Usuario;
            Model.InsertarControlCambios(obj, ref MsgRes);

            if (Model.idcargue != null)
            {
                Model.ActualizarAnalistalote(Model.idcargue, Model.id_analista2);
            }

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE rips_af_cargue_dtll SET id_analista_gestiona =@id_analista_gestiona Where id_recep_facturas_cargue_base = @idcargue";
                        command.Parameters.AddWithValue("@id_analista_gestiona", Model.id_analista2);
                        command.Parameters.AddWithValue("@idcargue", Model.idcargue);

                        connection.Open();
                        command.CommandTimeout = 120;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "*---ERROR -- - *" + ex.Message;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 18 de julio de 2022
        /// Vista parcial para gestionar las facturas de forma masiva
        /// </summary>
        /// <param name="facturas"></param>
        /// <returns></returns>
        public PartialViewResult GestionarFacturaMavisamente(List<int> facturas)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturasAuditorOKResult> seleccionadas = new List<managmentprestadoresFacturasAuditorOKResult>();
            List<managmentprestadoresFacturasAuditorOKResult> result = (List<managmentprestadoresFacturasAuditorOKResult>)Session["ListaFacturas2"];
            string tabla = "";

            tabla += "<table class='table table-bordered'>";
            tabla += "<thead><tr>";
            tabla += "<th>Numero factura</th>";
            tabla += "<th>Valor</th>";
            tabla += "<th>Prestador</th></tr></thead>";
            tabla += "<tbody>";
            foreach (int item in facturas)
            {
                var obj = result.Where(l => l.id_cargue_dtll == item).FirstOrDefault();
                if (obj != null)
                {
                    seleccionadas.Add(obj);
                    tabla += "<tr>";
                    tabla += "<td>" + obj.num_factura + "</td>";
                    tabla += "<td>" + obj.valor_neto.Value.ToString("C0", System.Globalization.CultureInfo.GetCultureInfo("es-CO")) + "</td>";
                    tabla += "<td>" + obj.nombre_prestador + "</td>";
                    tabla += "</tr>";
                }
            }
            tabla += "</tbody></table>";
            Session["facturasSeleccionadas"] = seleccionadas;
            ViewBag.infoFacturas = tabla;
            ViewBag.listgastos = BusClass.Getref_tipo_gasto_facturas(ref MsgRes).OrderBy(x => Convert.ToInt32(x.descripcion.Substring(0, 2))).ToList();
            ViewBag.listCie10 = BusClass.GetCie10Unido();

            return PartialView(Model);
        }

        /// <summary>
        /// Autor: Alexis quiñones castillo
        /// Fecha: 18 de julio de 2022
        /// Metodo para guardar la gestion de las facturas gestionadas de forma masiva
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public JsonResult SavegestionFacturasMasivas(Models.CuentasMedicas.RadicacionElectronica Model)
        {
            List<managmentprestadoresFacturasAuditorOKResult> result = (List<managmentprestadoresFacturasAuditorOKResult>)Session["facturasSeleccionadas"];
            List<ecop_gestion_factura_digital> facturas = new List<ecop_gestion_factura_digital>();
            var detalle = Model.DetalleGasto;
            String mensaje = "";
            String Alerta = "";
            if (detalle == null)
            {
                if (Model.factura_autorizada == "NO")
                {
                    Alerta = "NO";
                }
                else
                {
                    Alerta = "SI";
                }

            }
            else
            {
                Alerta = "NA";
            }

            if (Alerta != "SI")
            {
                try
                {
                    foreach (var factura in result)
                    {
                        ecop_gestion_factura_digital obj = new ecop_gestion_factura_digital();
                        obj.id_cargue_dtll = factura.id_cargue_dtll;
                        obj.multiusuario = Convert.ToString(Model.multiusuario);
                        obj.id_dx1 = Model.id_dx1;
                        obj.gasto = Model.gasto;
                        obj.factura_autorizada = Model.factura_autorizada;
                        obj.fecha_digita = Convert.ToDateTime(DateTime.Now);
                        obj.usuario_digita = Convert.ToString(SesionVar.UserName);
                        obj.aplica_auditoria = true;
                        Model.id_gestion_factura_digital = Model.InsertarGestionFacturadigital(obj, ref MsgRes);


                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            if (Alerta == "NA")
                            {
                                foreach (var item2 in detalle)
                                {
                                    ecop_gestion_factura_digital_gasto objg = new ecop_gestion_factura_digital_gasto();

                                    objg.id_ref_tipo_gasto_facturas = item2.id_gasto;
                                    objg.id_gestion_factura_digital = Model.id_gestion_factura_digital;
                                    objg.observacion_gasto = item2.obs_gasto;

                                    Model.InsertarGestionFacturadigitalGasto(objg, ref MsgRes);
                                }
                            }


                            mensaje = "SE INGRESO CORRECTAMENTE....";
                            if (Model.factura_autorizada == "SI")
                            {
                                using (SqlConnection connection = new SqlConnection(connectionString))
                                using (SqlCommand command = connection.CreateCommand())
                                {

                                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 6, fecha_aprobacion =@fecha_aprobacion Where id_af = @idfact";

                                    command.Parameters.AddWithValue("@idfact", factura.id_cargue_dtll);
                                    command.Parameters.AddWithValue("@fecha_aprobacion", Convert.ToDateTime(DateTime.Now));

                                    connection.Open();
                                    command.CommandTimeout = 120;
                                    command.ExecuteNonQuery();
                                    connection.Close();
                                }

                            }
                        }
                        else
                        {
                            mensaje = "ERROR EL INGRESO DEL DETALLE.";
                        }
                    }

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        if (Model.factura_autorizada == "SI")
                        {
                            return Json(new { success = true, message = mensaje, opc = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, message = mensaje, opc = 2 }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = mensaje, opc = 2 }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {

                    mensaje = "*---ERROR -- - *" + ex.Message;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                mensaje = "*ERROR.... INGRESE MINIMO UN GASTO.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha:18 de julio de 2022
        /// Metodo que se utiliza para exportar en zip las cartas de aprobacion de las facturas gestioanas (aprobadas)
        /// </summary>
        public void CrearPdfMasivoAprobacionFacturasDig()
        {
            try
            {
                List<string> listadoRutas = new List<string>();
                List<managmentprestadoresFacturasAuditorOKResult> result = (List<managmentprestadoresFacturasAuditorOKResult>)Session["facturasSeleccionadas"];
                foreach (managmentprestadoresFacturasAuditorOKResult objFactura in result)
                {

                    //RUTA REPORTE
                    string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital2.rdlc");

                    Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

                    List<managmentprestadoresFacturasReporteResult> lst = new List<managmentprestadoresFacturasReporteResult>();
                    lst = Model.GetFacturasByEstadoReporte(objFactura.id_cargue_dtll, ref MsgRes);

                    String id_base = Convert.ToString(lst.FirstOrDefault().id_cargue_base);
                    String id_analista = Convert.ToString(lst.FirstOrDefault().id_analista_gestiona);
                    String id_auditor = Convert.ToString(lst.FirstOrDefault().id_auditor_asignado);
                    String id_factura = Convert.ToString(lst.FirstOrDefault().id_gestion_factura_digital);
                    String id_detalle = Convert.ToString(lst.FirstOrDefault().id_cargue_dtll);

                    id_base = Model.AgregarValor(id_base) + id_base;
                    id_analista = Model.AgregarValor2(id_analista) + id_analista;
                    id_auditor = Model.AgregarValor2(id_auditor) + id_auditor;
                    id_factura = Model.AgregarValor(id_factura) + id_factura;

                    var msg = id_detalle;

                    byte[] file = RsaFileDemo.Encriptar(msg, id_detalle);

                    Models.InicioSesion.Usuarios Model2 = new Models.InicioSesion.Usuarios();
                    ecop_firma_digital_cod_barras ObjFirma = new ecop_firma_digital_cod_barras();

                    ObjFirma.llave_firma = file;
                    ObjFirma.fecha_digita = Convert.ToDateTime(DateTime.Now);
                    ObjFirma.usuario_digita = Convert.ToString(SesionVar.UserName);

                    var id_firma_digital = Model2.InsertarFirmadigital(ObjFirma, ref MsgRes);


                    id_base = Model.AgregarValor(id_base) + id_base;

                    string msg2 = id_base + "," + Convert.ToString(id_firma_digital);

                    byte[] file2 = RsaFileDemo.LaunchDemo(msg2, id_detalle);

                    String valorEncriptado = BitConverter.ToString(file2).Replace("-", "");

                    string cadena = (lst.FirstOrDefault().num_factura + ","
                                   + lst.FirstOrDefault().valor_neto + ","
                                   + lst.FirstOrDefault().num_id_prestador + ","
                                   + valorEncriptado + ","
                                   + id_detalle);

                    var analista = lst.FirstOrDefault().id_analista_gestiona;
                    var BaseImagen = Model.GetFirmas(analista);

                    string Imagen = "";

                    if (BaseImagen.firma_digital != null)
                    {
                        Imagen = Convert.ToBase64String(BaseImagen.firma_digital.ToArray());
                    }

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                    {

                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            ViewBag.imageBytes = ms.ToArray();

                        }
                    }
                    byte[] ImgByte = ViewBag.imageBytes;


                    string Imagen2 = "";
                    if (ImgByte != null)
                    {
                        Imagen2 = Convert.ToBase64String(ImgByte.ToArray());
                    }
                    var auditor = lst.FirstOrDefault().id_auditor_asignado;

                    var BaseImagen2 = Model.GetFirmas(auditor);

                    string Imagen3 = "";
                    if (BaseImagen2.firma_digital != null)
                    {
                        Imagen3 = Convert.ToBase64String(BaseImagen2.firma_digital.ToArray());
                    }

                    string filename = "CartaUsuarios" + lst.FirstOrDefault().num_factura;

                    Microsoft.Reporting.WebForms.ReportParameter imagen = new Microsoft.Reporting.WebForms.ReportParameter("Imagen", Imagen);
                    Microsoft.Reporting.WebForms.ReportParameter imagen2 = new Microsoft.Reporting.WebForms.ReportParameter("Imagen2", Imagen2);
                    Microsoft.Reporting.WebForms.ReportParameter imagen3 = new Microsoft.Reporting.WebForms.ReportParameter("Imagen3", Imagen3);
                    Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("FirmasDatSet", lst);

                    Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                    viewer.ProcessingMode = ProcessingMode.Local;
                    viewer.LocalReport.ReportPath = rPath;
                    viewer.LocalReport.DataSources.Clear();
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.SetParameters(imagen);
                    viewer.LocalReport.SetParameters(imagen2);
                    viewer.LocalReport.SetParameters(imagen3);

                    if (lst.Count != 0)
                    {

                        try
                        {
                            viewer.LocalReport.Refresh();

                            string mimeType;
                            string encoding;
                            string fileNameExtension;
                            string[] streams;
                            Microsoft.Reporting.WebForms.Warning[] warnings;
                            byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                            string strRutaDefinitiva = string.Empty;
                            string ruta = "";
                            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];
                            String carpeta = "FacturasAprobadas";
                            String SubCarpeta = "";
                            if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                            {
                                SubCarpeta = "Facturacion";
                            }
                            else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                            {
                                SubCarpeta = "FacturacionPruebas";
                            }
                            DateTime fecha = DateTime.Now;
                            string archivo = string.Empty;
                            ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + SubCarpeta + "\\" + objFactura.id_cargue_dtll);

                            var nombre = Path.GetFileNameWithoutExtension("Aprobada" + objFactura.id_cargue_dtll);
                            archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                            fecha, nombre, Path.GetExtension(".pdf"));

                            if (!Directory.Exists(ruta))
                                Directory.CreateDirectory(ruta);

                            string filename2 = archivo;

                            using (FileStream fs = new FileStream(filename2, FileMode.Create))
                            {
                                fs.Write(pdfContent, 0, pdfContent.Length);
                            }

                            listadoRutas.Add(archivo);

                            ecop_gestion_facturas_aprobadas obj = new ecop_gestion_facturas_aprobadas();
                            obj.id_cargue_dtll = objFactura.id_cargue_dtll;
                            obj.ruta = archivo;
                            obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                            Model.InsertarFacturaAprobadas(obj, ref MsgRes);

                        }
                        catch (Exception ex)
                        {
                            MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                            MsgRes.DescriptionResponse = ex.Message;
                        }
                    }

                }

                using (ZipFile zip = new ZipFile())
                {
                    int i = 0;
                    foreach (var item in result)
                    {
                        try
                        {
                            string ruta = listadoRutas[i];
                            WebClient User = new WebClient();
                            string filename = ruta;
                            Byte[] FileBuffer = User.DownloadData(filename);
                            byte[] array = FileBuffer.ToArray();
                            zip.AddEntry("CartaAprobacion_" + item.num_factura + ".pdf", array);
                            i++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    using (MemoryStream salida = new MemoryStream())
                    {
                        zip.Save(salida);

                        Response.Clear();
                        Response.BufferOutput = false;
                        Response.ContentType = "application/zip";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=CartasDeAprobacionFacturas.zip");
                        Response.BinaryWrite(salida.ToArray());
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 18 de julio de 2022
        /// Vista parcial para realizar la devolucion de las facturas de forma masiva
        /// </summary>
        /// <returns></returns>
        public PartialViewResult FacturaDevPrestadoresMasivo()
        {
            Models.Facturacion.FacturaDevolucion Model2 = new Models.Facturacion.FacturaDevolucion();
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<managmentprestadoresFacturasAuditorOKResult> result = (List<managmentprestadoresFacturasAuditorOKResult>)Session["facturasSeleccionadas"];
            string tabla = "";

            tabla += "<table class='table table-bordered'>";
            tabla += "<thead><tr>";
            tabla += "<th>Numero factura</th>";
            tabla += "<th>Valor</th>";
            tabla += "<th>Prestador</th></tr></thead>";
            tabla += "<tbody>";
            foreach (managmentprestadoresFacturasAuditorOKResult item in result)
            {
                tabla += "<tr>";
                tabla += "<td>" + item.num_factura + "</td>";
                tabla += "<td>" + item.valor_neto.Value.ToString("C0", System.Globalization.CultureInfo.GetCultureInfo("es-CO")) + "</td>";
                tabla += "<td>" + item.nombre_prestador + "</td>";
                tabla += "</tr>";

            }
            tabla += "</tbody></table>";

            ViewBag.Html = tabla;
            ViewBag.productos = BusClass.GetMedglosa();
            ViewBag.listaRegional = BusClass.GetRefRegion();
            ViewBag.listaCiudad = BusClass.GetCiudades();
            ViewBag.ips = BusClass.GetPrstadorCuentas();

            return PartialView(Model2);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 18 de julio de 2022
        /// </summary>
        /// <param name="listadoFacturas"></param>
        /// <returns></returns>
        public JsonResult AceptacionFacturasMasivamente(List<int> listadoFacturas)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            ecop_firma_digital_sami firma = new ecop_firma_digital_sami();

            try
            {
                firma = BusClass.traerDatosFirma(SesionVar.IDUser);

                if (firma == null)
                {
                    throw new Exception("El usuario no se encuentra con firma digital en SAMI. Por favor solicitarla");
                }

                Model.ActualizarFacturaAceptadaAnalista(listadoFacturas, SesionVar.IDUser, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return Json(new { success = true, mensaje = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, mensaje = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                if (error.Contains("El usuario no se encuentra con firma digital en SAMI"))
                {
                    return Json(new { success = false, mensaje = error }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, mensaje = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);

                }

            }
        }

        public ActionResult TableroRecepcionFacturasIncompletas()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<management_facturas_sinDocumentacionResult> listaFac = new List<management_facturas_sinDocumentacionResult>();
            listaFac = Model.ListaFacturasIncompletas();

            if (SesionVar.ROL != "1")
            {
                listaFac = listaFac.Where(x => x.usuario_digita == SesionVar.UserName).ToList();
            }

            ViewBag.Lista = listaFac.ToList();
            return View(Model);
        }

        public void ExcelReporteFacturasIncompletas()
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<management_facturas_sinDocumentacionResult> listaFac = new List<management_facturas_sinDocumentacionResult>();
            listaFac = Model.ListaFacturasIncompletas();

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte");

            Sheet.Cells["A1:W1"].Style.Font.Bold = true;
            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:W1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:W1"].Style.Font.Color.SetColor(Color.White);

            Sheet.Cells["A1"].Value = "Id cargue";
            Sheet.Cells["B1"].Value = "Prestador";
            Sheet.Cells["C1"].Value = "Nombre prestador";
            Sheet.Cells["D1"].Value = "Fecha inserción";
            Sheet.Cells["E1"].Value = "Usuario inserción";

            Sheet.Cells["F1"].Value = "Id af";
            Sheet.Cells["G1"].Value = "Codigo prestador";
            Sheet.Cells["H1"].Value = "Nombre prestador";
            Sheet.Cells["I1"].Value = "Tipo prestador";
            Sheet.Cells["J1"].Value = "Id prestador";
            Sheet.Cells["K1"].Value = "Numero factura";
            Sheet.Cells["L1"].Value = "Fecha Expiración";
            Sheet.Cells["M1"].Value = "Fecha inicio";
            Sheet.Cells["N1"].Value = "Fecha fin";
            Sheet.Cells["O1"].Value = "Codigo adm";
            Sheet.Cells["P1"].Value = "Nombre adm";
            Sheet.Cells["Q1"].Value = "Numero contrato";
            Sheet.Cells["R1"].Value = "Plan beneficios";
            Sheet.Cells["S1"].Value = "Numero poliza";
            Sheet.Cells["T1"].Value = "Copago";
            Sheet.Cells["U1"].Value = "Valor comisión";
            Sheet.Cells["V1"].Value = "Valor descuentos";
            Sheet.Cells["W1"].Value = "Valor neto";


            int row = 2;
            foreach (management_facturas_sinDocumentacionResult item in listaFac)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.id_cargue_base;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.id_prestador;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.nom_prestador;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_digita;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.usuario_digita;

                Sheet.Cells[string.Format("F{0}", row)].Value = item.id_af;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.codigo_prestador;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.nombre_prestador;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.tipo_id_prestador;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.num_id_prestador;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.num_factura;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.fecha_exp_factura;
                Sheet.Cells[string.Format("M{0}", row)].Value = item.fecha_inicio;
                Sheet.Cells[string.Format("N{0}", row)].Value = item.fecha_final;
                Sheet.Cells[string.Format("O{0}", row)].Value = item.cod_entidad_adm;
                Sheet.Cells[string.Format("P{0}", row)].Value = item.nom_entidad_adm;
                Sheet.Cells[string.Format("Q{0}", row)].Value = item.num_contrato;
                Sheet.Cells[string.Format("R{0}", row)].Value = item.plan_beneficios;
                Sheet.Cells[string.Format("S{0}", row)].Value = item.num_poliza;
                Sheet.Cells[string.Format("T{0}", row)].Value = item.copago;
                Sheet.Cells[string.Format("U{0}", row)].Value = item.valor_comision;
                Sheet.Cells[string.Format("V{0}", row)].Value = item.valor_descuentos;
                Sheet.Cells[string.Format("W{0}", row)].Value = item.valor_neto;
                row++;
            }
            Sheet.Cells["A:W"].AutoFitColumns();

            Response.Clear();
            Response.BufferOutput = false;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteFacturasIncompletas.xlsX");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public ActionResult ControlAsignacionAutomaticas(string nit, string prestador, int? rta)
        {
            Models.General General = new Models.General();
            if (rta == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se ingreso el equipo exitosamente ");
            }
            else if (rta == 2)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se ingreso el monitor exitosamente ");
            }
            else
            {
                ViewData["alerta"] = "";
            }

            List<Ref_ips_cuentas> listado = new List<Ref_ips_cuentas>();
            try
            {
                if (!string.IsNullOrEmpty(nit) || !string.IsNullOrEmpty(prestador))
                {
                    listado = BusClass.getprestadoresEspecial(nit, prestador);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.lista = listado;

            return View();
        }

        public PartialViewResult _GestionarAnalistas(int idPrestador)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            management_prestadoresRegionalIdPrResult regional = new management_prestadoresRegionalIdPrResult();
            List<ref_cuentas_medicas_analista> prestadorAnalista = new List<ref_cuentas_medicas_analista>();
            List<vw_analistas_recepcion> usuarios = new List<vw_analistas_recepcion>();
            var IdRegional = 0;
            try
            {
                regional = BusClass.PrestadorRegional(idPrestador);
                if (regional != null)
                {
                    usuarios = BusClass.GetListAnalistas().Where(l => l.id_regional == (int)regional.id_ref_regional).ToList();
                    IdRegional = (int)regional.id_ref_regional;
                }

                prestadorAnalista = BusClass.getAnalistasAsignadosprestador(idPrestador);

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.usuarios = usuarios;
            ViewBag.asignacion = prestadorAnalista;
            ViewBag.prestador = idPrestador;
            ViewBag.regional = IdRegional;

            return PartialView();
        }

        public JsonResult GuardarAnalistasSeleccion(List<int> analistas, int? idRegional, int? prestador)
        {

            List<ref_cuentas_medicas_analista> listadoNuevo = new List<ref_cuentas_medicas_analista>();
            List<ref_cuentas_medicas_analista> listadoActualizar = new List<ref_cuentas_medicas_analista>();
            var mensaje = "";

            try
            {
                if (analistas.Count() > 0)
                {
                    var actualizacion = BusClass.ActualizarAsignacion_automatica((int)prestador);

                    for (var i = 0; i < analistas.Count(); i++)
                    {
                        ref_cuentas_medicas_analista cuentas = new ref_cuentas_medicas_analista();

                        ref_cuentas_medicas_analista existencia = new ref_cuentas_medicas_analista();

                        var analistaId = analistas[i];
                        existencia = BusClass.ListadoActualizarAnalistas((int)prestador, analistaId);

                        if (existencia != null)
                        {
                            cuentas.id_ref_cuentas_medicas_analista = existencia.id_ref_cuentas_medicas_analista;
                            cuentas.activo = 1;
                            var actualizado = BusClass.ActualizarAsignacionAutomatica(cuentas);
                        }
                        else
                        {
                            cuentas.conteo = 0;
                            cuentas.id_usuario = analistaId;
                            cuentas.id_prestador = prestador;
                            cuentas.id_regional = idRegional;
                            cuentas.id_estado = 16;
                            cuentas.usuario_digita = SesionVar.UserName;
                            cuentas.fecha_digita = DateTime.Now;
                            cuentas.activo = 1;
                            listadoNuevo.Add(cuentas);
                        }
                    }
                    var insertar = BusClass.InsertarNuevosAnalistas_asignacionAutomatica(listadoNuevo);
                }
                mensaje = "ANALISTAS ASIGNADOS CORRECTAMENTE";
                return Json(new { msg = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "PROBLEMAS AL ASIGNAR ANALISTAS: " + error;
                return Json(new { msg = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public static DateTime CheckCachedExpiry(string key)
        {
            DateTime MemCacheExpiryDate = default(DateTime);
            MemCacheExpiryDate = Convert.ToDateTime(MemoryCache.Default.Get(key));
            return MemCacheExpiryDate;
        }

        public ActionResult CreacionRembolsos(int? idReembolso)
        {
            cuentas_reembolsos reembolso = new cuentas_reembolsos();

            try
            {
                reembolso = BusClass.TraerDatosReembolso(idReembolso);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.tipoMoneda = BusClass.TipoMoneda();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.ciudades = BusClass.GetCiudades();
            ViewBag.tipoReembolso = BusClass.TipoReembolso();

            if (reembolso == null)
            {
                reembolso = new cuentas_reembolsos();
            }

            ViewBag.reembolso = reembolso;
            ViewBag.idReembolso = idReembolso;
            ViewBag.rol = SesionVar.ROL;

            return View(reembolso);
        }

        [HttpPost]
        public ActionResult CreacionRembolsos(cuentas_reembolsos obj)
        {
            cuentas_reembolsos obj2 = new cuentas_reembolsos();
            var mensaje = "";
            var rta = 0;
            cuentas_reembolsos reembolso = new cuentas_reembolsos();

            try
            {

                if (reembolso == null)
                {
                    reembolso = new cuentas_reembolsos();
                }

                obj2.id_reembolso = obj.id_reembolso;
                obj2.id_regional = obj.id_regional;
                obj2.id_ciudad = obj.id_ciudad;
                obj2.fecha_recepcion = obj.fecha_recepcion;
                obj2.sad_titular = obj.sad_titular;
                obj2.titular = obj.titular;
                obj2.unis = obj.unis;
                obj2.tipo_reembolso = obj.tipo_reembolso;
                obj2.identificacion_beneficiario = obj.identificacion_beneficiario;
                obj2.beneficiario = obj.beneficiario;
                obj2.num_factura = obj.num_factura;
                obj2.sap_entidad = obj.sap_entidad;
                obj2.prestador = obj.prestador;
                obj2.nit = obj.nit;
                obj2.valor = obj.valor;
                obj2.id_tipo_moneda = obj.id_tipo_moneda;
                obj2.fecha_digita = DateTime.Now;
                obj2.usuario_digita = SesionVar.UserName;
                //obj2.id_estado = 0;

                IList<HttpPostedFileBase> fileReem = Request.Files.GetMultiple("fileReem");

                var resultado = 0;

                if (obj.id_reembolso != 0 && obj.id_reembolso != null)
                {
                    resultado = BusClass.ActualizarDatosReembolso(obj);
                    reembolso = BusClass.TraerDatosReembolso(obj.id_reembolso);
                }
                else
                {
                    resultado = BusClass.InsertarRembolso(obj2);
                }


                if (resultado != 0)
                {
                    var ingresoArchivo = 0;

                    if (fileReem != null)
                    {
                        if (fileReem.Count > 0)
                        {
                            for (var i = 0; i < fileReem.Count(); i++)
                            {
                                HttpPostedFileBase archivo = fileReem[i];

                                if (archivo.ContentLength > 0)
                                {
                                    ingresoArchivo = GuardarArchivoReembolso(archivo, resultado);
                                }
                            }
                        }
                    }

                    if (obj.id_reembolso > 0)
                    {
                        mensaje = "SE ACTUALIZÓ CORRECTAMENTE.";
                    }
                    else
                    {
                        mensaje = "SE INGRESÓ CORRECTAMENTE.";
                    }
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN LA INSERCIÓN";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA INSERCIÓN - " + error;
            }

            ViewBag.rta = rta;
            ViewBag.msg = mensaje;

            ViewBag.reembolso = reembolso;
            ViewBag.idReembolso = obj.id_reembolso;
            ViewBag.rol = SesionVar.ROL;

            ViewBag.tipoMoneda = BusClass.TipoMoneda();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.ciudades = BusClass.GetCiudades();
            ViewBag.tipoReembolso = BusClass.TipoReembolso();

            return View(reembolso);
        }

        private int GuardarArchivoReembolso(HttpPostedFileBase file, int? idReembolso)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            var respuesta = 0;

            try
            {
                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosReembolso"];

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "ArchivosIngresoReembolso";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "ArchivosIngresoReembolsoPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + "Reembolso_" + idReembolso);

                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                cuentas_reembolsos_archivos OBJ = new cuentas_reembolsos_archivos();
                OBJ.id_reembolso = idReembolso;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.extension = file.ContentType;
                OBJ.nombre_archivo = file.FileName;
                OBJ.fecha_digita = DateTime.Now;
                OBJ.usuario_digita = SesionVar.UserName;

                respuesta = BusClass.InsertarRembolsoArchivos(OBJ);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }
        public ActionResult TableroReembolsos(int? idRegional)
        {
            List<management_reembolsos_tableroResult> lista = new List<management_reembolsos_tableroResult>();
            List<Ref_regional> regionales = new List<Ref_regional>();
            var conteo = 0;
            try
            {
                lista = BusClass.listadoReembolsosIngresados(idRegional);
                conteo = lista.Count();
                regionales = BusClass.GetRefRegion();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["listadoReembolsos"] = lista;

            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.regionales = regionales;
            ViewBag.rol = SesionVar.ROL;

            return View();
        }

        public ActionResult TableroReembolsosGestionados(int? idRegional)
        {
            List<management_reembolsos_tablero_gestionadosResult> lista = new List<management_reembolsos_tablero_gestionadosResult>();
            List<Ref_regional> regionales = new List<Ref_regional>();
            var conteo = 0;
            try
            {
                lista = BusClass.listadoReembolsosGestionados(idRegional);
                conteo = lista.Count();
                regionales = BusClass.GetRefRegion();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["listadoReembolsosGestionados"] = lista;

            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.regionales = regionales;
            ViewBag.rol = SesionVar.ROL;

            return View();
        }

        public ActionResult TableroReembolsosEdicion(int? idCargue)
        {
            List<management_reembolsos_tablero_gestionadosResult> lista = new List<management_reembolsos_tablero_gestionadosResult>();
            List<Ref_regional> regionales = new List<Ref_regional>();
            var conteo = 0;
            try
            {
                lista = BusClass.listadoReembolsosGestionados(null);
                if (lista.Count() > 0)
                {
                    lista = lista.Where(x => x.id_reembolso == idCargue).ToList();
                }

                conteo = lista.Count();
                regionales = BusClass.GetRefRegion();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["listadoReembolsosGestionadosEdicion"] = lista;

            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.regionales = regionales;
            ViewBag.rol = SesionVar.ROL;

            return View();
        }

        public PartialViewResult ModalArchivosReembolso(int? idReembolso, int? tipo)
        {
            List<management_cuentas_reembolso_ArchivosResult> listado = new List<management_cuentas_reembolso_ArchivosResult>();
            var conteo = 0;

            try
            {
                listado = BusClass.listadoReembolsosArchivos(idReembolso);
                conteo = listado.Count();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoArchivos = listado;
            ViewBag.conteoArchivos = conteo;
            ViewBag.idReembolso = idReembolso;
            ViewBag.rol = SesionVar.ROL;

            //tipo 1: tablero reembolsos, tipo 2 tablero reembolsos gestionados, tipo 3 tablero editar reembolsos
            ViewBag.tipo = tipo;

            return PartialView();
        }

        public ActionResult VerArchivoReembolso(Int32 idArchivo)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                cuentas_reembolsos_archivos dato = new cuentas_reembolsos_archivos();
                dato = BusClass.TrarArchivoReembolso(idArchivo);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extensionTipo = obj.extension;
                    var nombre = dato.nombre_archivo;

                    return File(dirpath, extensionTipo, nombre);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public JsonResult IngresarArchivosNuevosReembolso(int? idReembolso, List<HttpPostedFileBase> fileReemAd)
        {
            var mensaje = "";
            try
            {
                var ingresoArchivo = 0;
                var seInserta = 0;

                if (fileReemAd != null)
                {
                    if (fileReemAd.Count > 0)
                    {
                        for (var i = 0; i < fileReemAd.Count(); i++)
                        {
                            HttpPostedFileBase archivo = fileReemAd[i];

                            if (archivo.ContentLength > 0)
                            {
                                ingresoArchivo = GuardarArchivoReembolso(archivo, idReembolso);
                                seInserta = 1;
                                if (ingresoArchivo != 0)
                                {

                                }
                            }
                        }
                    }
                }

                if (seInserta != 0)
                {
                    if (ingresoArchivo != 0)
                    {
                        mensaje = "ARCHIVO INGRESADO CORRECTAMENTE";
                    }
                    else
                    {
                        mensaje = "ERROR AL INSERTAR ARCHIVO";
                    }
                }
                else
                {
                    mensaje = "NO SE CARGARON ARCHIVOS";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL INSERTAR ARCHIVO: " + error;
            }

            return Json(new { mensaje = mensaje });
        }

        public JsonResult EliminarArchivoReembolso(int? idArchivo)
        {
            var mensaje = "";

            try
            {
                var elimina = BusClass.EliminarArchivoReembolsos(idArchivo);
                if (elimina != 0)
                {
                    mensaje = "ARCHIVO ELIMINADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR EN LA ELIMINACIÓN DE ARCHIVO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ELIMINACIÓN DE ARCHIVO: " + error;
            }

            return Json(new { mensaje = mensaje });
        }

        public PartialViewResult _GestionRrembolso(int? idReembolso)
        {
            ViewBag.idReembolso = idReembolso;
            ViewBag.estado = BusClass.EstadoReembolso();

            List<management_reembolsos_gestionResult> lista = new List<management_reembolsos_gestionResult>();
            var conteoGestion = 0;
            try
            {
                lista = BusClass.listadoReembolsosIngresadosGestiones(idReembolso);
                conteoGestion = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaGestion = lista;
            ViewBag.conteoGestion = conteoGestion;
            ViewBag.idReembolso = idReembolso;

            return PartialView();
        }

        public JsonResult GuardarGestionReembolso(cuentas_reembolso_detalle obj)
        {
            var mensaje = "";
            var rta = 0;
            var estado = 0;
            try
            {
                cuentas_reembolso_detalle obj2 = new cuentas_reembolso_detalle();

                obj2.id_reembolso = obj.id_reembolso;
                obj2.estado = obj.estado;
                obj2.observacion = obj.observacion;
                obj2.clase_documento = obj.clase_documento;
                obj2.documento_contable = obj.documento_contable;
                obj2.fecha_contabilizacion = obj.fecha_contabilizacion;

                obj2.valor_final = obj.valor_final;
                obj2.usuario_digita = SesionVar.UserName;
                obj2.fecha_digita = DateTime.Now;

                estado = (int)obj.estado;

                var resultado = BusClass.InsertarRembolsoDetalle(obj2);

                if (resultado != 0)
                {
                    //if (estado == 1 || estado == 5)
                    //{
                    cuentas_reembolsos reem = new cuentas_reembolsos();
                    reem.id_estado = estado;
                    reem.id_reembolso = (int)obj.id_reembolso;
                    var actualizaEstado = BusClass.ActualizarEstadoReembolso(reem);
                    //}
                    mensaje = "GESTIÓN INGRESADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE GESTIÓN";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE GESTIÓN - " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta, estado = estado, idReembolso = obj.id_reembolso });
        }

        public void DescargarReporteReembolsos()
        {
            List<management_reembolsos_tableroResult> lista = (List<management_reembolsos_tableroResult>)Session["listadoReembolsos"];

            try
            {
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosReembolso");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:O1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:O1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:O1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:O1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:O1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:O1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id registro";
                    Sheet.Cells["B1"].Value = "Regional";
                    Sheet.Cells["C1"].Value = "Ciudad";
                    Sheet.Cells["D1"].Value = "Fecha recepción";
                    Sheet.Cells["E1"].Value = "SAD titular";
                    Sheet.Cells["F1"].Value = "Titular";
                    Sheet.Cells["G1"].Value = "Identificación beneficiario";
                    Sheet.Cells["H1"].Value = "Beneficiario";
                    Sheet.Cells["I1"].Value = "Número factura";
                    Sheet.Cells["J1"].Value = "SAP entidad";
                    Sheet.Cells["K1"].Value = "Prestador";
                    Sheet.Cells["L1"].Value = "Nit";
                    Sheet.Cells["M1"].Value = "Valor";
                    Sheet.Cells["N1"].Value = "Tipo moneda";
                    Sheet.Cells["O1"].Value = "Estado";

                    int row = 2;

                    foreach (management_reembolsos_tableroResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":N" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_reembolso;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.indice;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.nombreCiudad;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_recepcion;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.sad_titular;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.titular;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.identificacion_beneficiario;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.beneficiario;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.num_factura;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.sap_entidad;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.prestador;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.nit;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.valor;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.descripcionMoneda;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.ultimoEstado;

                        Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReporteDatosReembolso";
                    Sheet.Cells["A:O"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

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

        public void DescargarReporteReembolsosGestionados(int? tipo)
        {

            List<management_reembolsos_tablero_gestionadosResult> lista = new List<management_reembolsos_tablero_gestionadosResult>();

            if (tipo == 1)
            {
                lista = (List<management_reembolsos_tablero_gestionadosResult>)Session["listadoReembolsosGestionados"];
            }
            else if (tipo == 2)
            {
                lista = (List<management_reembolsos_tablero_gestionadosResult>)Session["listadoReembolsosGestionadosEdicion"];

            }

            try
            {
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosReembolsogestionados");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:O1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:O1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:O1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:O1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:O1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:O1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id registro";
                    Sheet.Cells["B1"].Value = "Regional";
                    Sheet.Cells["C1"].Value = "Ciudad";
                    Sheet.Cells["D1"].Value = "Fecha recepción";
                    Sheet.Cells["E1"].Value = "SAD titular";
                    Sheet.Cells["F1"].Value = "Titular";
                    Sheet.Cells["G1"].Value = "Identificación beneficiario";
                    Sheet.Cells["H1"].Value = "Beneficiario";
                    Sheet.Cells["I1"].Value = "Número factura";
                    Sheet.Cells["J1"].Value = "SAP entidad";
                    Sheet.Cells["K1"].Value = "Prestador";
                    Sheet.Cells["L1"].Value = "Nit";
                    Sheet.Cells["M1"].Value = "Valor";
                    Sheet.Cells["N1"].Value = "Tipo moneda";
                    Sheet.Cells["O1"].Value = "Estado";

                    int row = 2;

                    foreach (management_reembolsos_tablero_gestionadosResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":N" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_reembolso;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.indice;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.nombreCiudad;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_recepcion;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.sad_titular;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.titular;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.identificacion_beneficiario;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.beneficiario;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.num_factura;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.sap_entidad;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.prestador;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.nit;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.valor;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.descripcionMoneda;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.ultimoEstado;

                        Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReporteDatosReembolsoGestionados";
                    Sheet.Cells["A:O"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

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

        public ActionResult TableroControlAsignacionesTiga(String numFac, String nit, String prestador, String sap, int? rta, int? estado)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<view_ref_estado_facturas> estados = new List<view_ref_estado_facturas>();
            List<managmentprestadoresfacturasgestionadasCompletaResult> result = new List<managmentprestadoresfacturasgestionadasCompletaResult>();
            List<managmentprestadoresfacturasgestionadasCompletaResult> lista = new List<managmentprestadoresfacturasgestionadasCompletaResult>();
            List<managmentprestadoresfacturasgestionadasCompletaResult> lista3 = new List<managmentprestadoresfacturasgestionadasCompletaResult>();
            List<managmentprestadoresfacturasgestionadasCompletaResult> Lista2 = new List<managmentprestadoresfacturasgestionadasCompletaResult>();

            try
            {
                //Lista2 = Model.GetGestionFacturasv2(fechainicial, fechafinal, estado, regional, prestador, nit, numFac);
                if (!string.IsNullOrEmpty(numFac) || !string.IsNullOrEmpty(nit) || !string.IsNullOrEmpty(prestador) || !string.IsNullOrEmpty(sap)
                    || estado != null)
                {
                    Lista2 = Model.GetGestionFacturasv3(numFac, nit, prestador, sap, estado, null);
                }

                if (!string.IsNullOrEmpty(sap))
                {
                    Lista2 = Lista2.Where(x => x.cod_sap_prestador == sap).ToList();
                }

                if (Lista2 != null)
                {
                    if (Lista2.Count() > 0)
                    {
                        if (SesionVar.ROL != "1")
                        {
                            List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                            regional obj = new regional();
                            list = BusClass.GetRegionalAuditor();
                            list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                            foreach (var item in list)
                            {
                                result = Lista2.Where(x => x.id_ref_regional == item.id_regional).ToList();
                                lista3 = lista3.Concat(result).ToList();
                            }

                            lista = lista3;
                        }
                        else
                        {
                            lista = Lista2;
                        }
                    }
                    else
                    {
                        lista = Lista2;
                    }
                }

                ViewBag.ROL = SesionVar.ROL;
                Session["ListaFacturas"] = lista;

                var conteo = lista.Count();

                ViewBag.Lista = lista;
                ViewBag.conteo = conteo;

                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.ref_estado = Model.GetEstadoFacturas();

                //ViewBag.fechainicio = fechainicial;
                //ViewBag.fechafin = fechafinal;
                ViewBag.rta = rta;

                var estadoUsado = 0;
                if (estado != null)
                {
                    estadoUsado = (int)estado;
                }
                ViewBag.estadoUsado = estadoUsado;

                var rol = SesionVar.ROL;
                ViewBag.rol = rol;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            estados = BusClass.GetEstadoFacturas();

            ViewBag.estados = estados;

            return View(Model);
        }

        public ActionResult IngresoNoEsalud()
        {
            ViewBag.mensaje = "";
            ViewBag.rta = 0;
            ViewBag.analistas = BusClass.ListadoAnalistas();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.motivos = BusClass.ListaMotivosNoRips();
            ViewBag.ciudades = BusClass.GetCiudades();

            return View();
        }

        [HttpPost]
        public ActionResult IngresoNoEsalud(cuentas_medicas_norips obj)
        {
            cuentas_medicas_norips obj2 = new cuentas_medicas_norips();
            var rta = 0;
            var mensaje = "";
            var dañado = 0;
            var insercionArchivo = 0;
            var mensajeArchivos = "";

            ViewBag.analistas = BusClass.ListadoAnalistas();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.motivos = BusClass.ListaMotivosNoRips();
            ViewBag.ciudades = BusClass.GetCiudades();

            try
            {
                obj2.id_analista = obj.id_analista;
                obj2.nit = obj.nit;
                obj2.nombre_prestador = obj.nombre_prestador;
                obj2.tiene_contrato = obj.tiene_contrato;
                obj2.regional = obj.regional;
                obj2.unis = obj.unis;
                obj2.ciudad = obj.ciudad;
                obj2.motivo_nocargue = obj.motivo_nocargue;
                obj2.motivo_otro = obj.motivo_otro;
                obj2.fecha_digita = DateTime.Now;
                obj2.usuario_digita = SesionVar.UserName;

                var resultado = BusClass.InsertarNoRips(obj2, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    IList<HttpPostedFileBase> files = Request.Files.GetMultiple("files");
                    if (files.Count() > 0)
                    {
                        for (var i = 0; i < files.Count(); i++)
                        {
                            insercionArchivo = GuardarArchivosNoRips(files[i], resultado);
                            if (resultado == 0)
                            {
                                mensajeArchivos += "/n";
                                mensajeArchivos += "No se cargó el archivo: " + files[i].FileName;
                                rta = 2;
                                dañado = 1;
                            }
                        }
                    }

                    if (dañado == 0)
                    {
                        mensaje = "GESTIÓN INGRESADA CORRECTAMENTE";
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "ERROR EN LA INSERCIÓN DE ARCHIVOS: ";
                        mensaje += "/n";
                        mensaje += mensajeArchivos;
                        rta = 2;
                    }
                }
                else
                {
                    mensaje = MsgRes.DescriptionResponse;
                    rta = 2;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = ex.Message;
                rta = 2;
            }

            ViewBag.mensaje = mensaje;
            ViewBag.rta = rta;

            return View();
        }

        private int GuardarArchivosNoRips(HttpPostedFileBase file, int idNoRips)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            var respuesta = 0;

            try
            {
                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosRipsNoesalud"];

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "ArchivosNoRips";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "ArchivosNoRipsPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idNoRips);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                cuentas_medicas_norips_Archivos OBJ = new cuentas_medicas_norips_Archivos();
                OBJ.id_med = idNoRips;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.extension = file.ContentType;
                OBJ.nombre_archivo = file.FileName;
                OBJ.fecha_digita = DateTime.Now;
                OBJ.usuario_digita = SesionVar.UserName;

                respuesta = BusClass.IngresoArchivosRipsNoEsalud(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }

        public string ObtenerDepartamentos(int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            List<management_total_departamentosResult> departamentos = new List<management_total_departamentosResult>();

            try
            {
                departamentos = BusClass.TraerDepartamentosRegional(regional);
                foreach (var item in departamentos)
                {
                    result += "<option value='" + item.departamento + "'>" + item.departamento + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public string ObtenerCiudades(string departamento, int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();
            List<Ref_ciudades> Ciudades = new List<Ref_ciudades>();
            try
            {
                Ciudades = BusClass.GetCiudades();

                if (!string.IsNullOrEmpty(departamento))
                {
                    Ciudades = Ciudades.Where(x => x.departamento.ToUpper() == departamento.ToUpper() && x.id_ref_regional == regional).ToList();
                }

                foreach (var item in Ciudades)
                {
                    result += "<option value='" + item.id_ref_ciudades + "'>" + item.nombre + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public string ObtenerUnis(int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();
            List<ref_adherencia_unis> unis = new List<ref_adherencia_unis>();
            try
            {
                unis = BusClass.GetUnisByRegional((int)regional);

                foreach (var item in unis)
                {
                    result += "<option value='" + item.id_ref_adherencia_unis + "'>" + item.nom_adherencia_unis + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public JsonResult BuscarBeneficiario()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {

                    List<management_baseBeneficiarios_xDocumentoResult> beneficiarios = BusClass.BusquedaBeneficiarioCedula(term);
                    var lista = (from bb in beneficiarios
                                 select new
                                 {
                                     cedula = bb.documentoBeneficiario,
                                     nombre = bb.nombreBenefiairio,
                                     label = bb.documentoBeneficiario + "-" + bb.nombreBenefiairio,
                                 }).Distinct().OrderBy(f => f.cedula).Take(15);
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

        public string ObtenerMunicipiosFis(int? departamento)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();
            List<ref_fis_municipios> Ciudades = new List<ref_fis_municipios>();
            try
            {
                Ciudades = BusClass.TraerMunicipiosFis(departamento);

                foreach (var item in Ciudades)
                {
                    result += "<option value='" + item.id_municipio + "'>" + item.nombre_municipio + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public ActionResult TableroEsalud(DateTime? fechaInicio, DateTime? fechaFin, int? idRegional)
        {
            List<management_cuentasMedicas_ripsNoEsaludResult> lista = new List<management_cuentasMedicas_ripsNoEsaludResult>();
            var conteo = 0;

            try
            {
                lista = BusClass.TableroRipsNoEsalud(fechaInicio, fechaFin, idRegional);
                conteo = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["fechaInicioESalud"] = fechaInicio;
            Session["fechaFinESalud"] = fechaFin;
            Session["regionalESalud"] = idRegional;

            Session["listadoDatosESsalud"] = lista;

            ViewBag.lista = lista;
            ViewBag.conteo = conteo;

            ViewBag.regional = BusClass.GetRefRegion();

            return View();
        }

        public JsonResult EliminarCasoNoRips(int? idMed)
        {
            var mensaje = "";
            try
            {
                var elimina = BusClass.EliminarCasoNoRips(idMed);
                if (elimina != 0)
                {
                    mensaje = "CASO ELIMINADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR EL CASO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR EL CASO: " + error;
            }
            return Json(new { mensaje = mensaje });
        }

        public void DescargarReporteNoEsalud()
        {
            List<management_cuentasMedicas_ripsNoEsaludResult> lista = (List<management_cuentasMedicas_ripsNoEsaludResult>)Session["listadoDatosESsalud"];

            try
            {
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosENoSalud");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:K1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:K1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:K1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:K1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:K1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id registro";
                    Sheet.Cells["B1"].Value = "Nombre del analista que realiza el cargue";
                    Sheet.Cells["C1"].Value = "NIT/Sin digito de Verificación";
                    Sheet.Cells["D1"].Value = "Nombre del prestador";
                    Sheet.Cells["E1"].Value = "Tiene contrato";
                    Sheet.Cells["F1"].Value = "Regional";
                    Sheet.Cells["G1"].Value = "Ciudad";
                    Sheet.Cells["H1"].Value = "UNIS";
                    Sheet.Cells["I1"].Value = "Motivo de no cargue E-salud";
                    Sheet.Cells["J1"].Value = "Cual otro";
                    Sheet.Cells["K1"].Value = "Fecha del reporte";

                    int row = 2;

                    foreach (management_cuentasMedicas_ripsNoEsaludResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":K" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_med;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.nombreAnalista;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.nit;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.nombre_prestador;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.tiene_contrato;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.indiceRegional;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.nombreCiudad;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nombreUnis;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.motivo;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.motivo_otro;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.fecha_digita;

                        Sheet.Cells[string.Format("K{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReporteDatosENoSalud";
                    Sheet.Cells["A:K"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

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

        public PartialViewResult MostrarRepositorioNoEsalud(int? idMed)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            List<management_cuentasMedicas_ripsNoEsalud_archivosResult> listaArchivos = new List<management_cuentasMedicas_ripsNoEsalud_archivosResult>();
            var conteoArchivos = 0;

            try
            {
                listaArchivos = BusClass.TableroRepositorioRipsNoEsalud(idMed);
                conteoArchivos = listaArchivos.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idMed = idMed;
            ViewBag.listaArchivos = listaArchivos;
            ViewBag.conteoArchivos = conteoArchivos;
            return PartialView(Model);
        }

        public ActionResult VerArchivoIngreso(Int32 idArchivo)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                cuentas_medicas_norips_Archivos dato = new cuentas_medicas_norips_Archivos();
                dato = BusClass.traerArchivoNoRips(idArchivo);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extensionTipo = obj.extension;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];

                    return File(dirpath, extensionTipo, nombreFinal);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public ActionResult DescargarTodosLosArchivos()
        {
            List<management_cuentasMedicas_ripsNoEsalud_TodosArchivosResult> lista = new List<management_cuentasMedicas_ripsNoEsalud_TodosArchivosResult>();
            try
            {
                var fechaInicio = Session["fechaInicioESalud"];
                var fechaFin = Session["fechaFinESalud"];
                var regional = Session["regionalESalud"];

                DateTime fechaInicioFinal = new DateTime();
                DateTime fechaFinFinal = new DateTime();

                if ((fechaInicio != null && fechaFin != null) || regional != null)
                {
                    fechaInicioFinal = Convert.ToDateTime(fechaInicio);
                    fechaFinFinal = Convert.ToDateTime(fechaFin);
                    lista = BusClass.TraerTodosLosArchivosRipsNoEsalud(fechaInicioFinal, fechaFinFinal, (int?)regional);
                }
                else
                {
                    lista = BusClass.TraerTodosLosArchivosRipsNoEsalud(null, null, null);
                }

                if (lista.Count == 0)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "No se encuentran documentos." });
                }

                //using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                //{
                //    int count = 0;
                //    foreach (var item in lista)
                //    {
                //        var ruta = item.id_med + "_" + item.nombre_prestador;
                //        count++;
                //        string dirpath = item.ruta;
                //        byte[] bytes = System.IO.File.ReadAllBytes(dirpath);

                //        zip.AddEntry(ruta + "\\ " + item.id_archivo + "_" + item.nombre_prestador + "_" + item.nombre_archivo + ".zip", bytes);
                //    }

                //    using (MemoryStream salida = new MemoryStream())
                //    {
                //        zip.Save(salida);
                //        return File(salida.ToArray(), "application/zip", "ConsolidadoArchivos_" + DateTime.Now + ".zip");
                //    }
                //}

                var zipFileName = "ConsolidadoArchivos_" + DateTime.Now + ".zip";
                Response.Clear();
                Response.BufferOutput = false; // Evitar la acumulación en la memoria
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipFileName);

                using (ZipFile zip = new ZipFile())
                {
                    foreach (var item in lista)
                    {
                        var ruta = item.id_med + "_" + item.nombre_prestador;
                        string dirpath = item.ruta;
                        zip.AddFile(dirpath, ruta);
                    }
                    zip.Save(Response.OutputStream); // Enviar directamente al flujo de salida
                }
                Response.End();
                return null;
            }

            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new
                {
                    Mensaje = "Ha ocurrido un error al momento de generar el archivo: " + ex.Message
                });
            }
        }

        public ActionResult DescargarFacturasEnComprimido()
        {
            List<management_facturas_creacionreporteAprobacionResult> listadoFacturas = BusClass.listadoFacturasReporteAprobacion();

            if (listadoFacturas.Count() > 0)
            {
                using (var zip = new Ionic.Zip.ZipFile())
                {
                    foreach (var item in listadoFacturas)
                    {
                        ecop_gestion_facturas_aprobadas factura = BusClass.GetFacturasAprobadas((int)item.id_factura);

                        if (factura != null)
                        {
                            if (!string.IsNullOrEmpty(factura.ruta))
                            {
                                // Verificar si el archivo existe en la ruta
                                if (System.IO.File.Exists(factura.ruta))
                                {
                                    // Crear un nombre único para cada archivo en el ZIP
                                    string nombreArchivo = $"FacturaAprobada_{item.id_factura}_{item.num_factura}.pdf";
                                    // Agregar el archivo desde la ruta al ZIP
                                    zip.AddFile(factura.ruta).FileName = nombreArchivo;
                                }
                            }
                        }
                        else
                        {
                            byte[] archivoPdf = DevolverFacturaArray(item.id_factura);
                            if (archivoPdf != null)
                            {
                                // Crear un nombre único para cada archivo en el ZIP
                                string nombreArchivo = $"FacturaAprobada_{item.id_factura}_{item.num_factura}.pdf";
                                // Agregar el archivo PDF al ZIP como byte array
                                zip.AddEntry(nombreArchivo, archivoPdf);
                            }
                        }

                        // Configurar la respuesta HTTP para descarga del ZIP
                        Response.Clear();
                        Response.BufferOutput = false;
                        Response.ContentType = "application/zip";
                        string nombreZip = "FacturasAprobadas_" + DateTime.Now;
                        Response.AddHeader("content-disposition", "attachment; filename=" + nombreZip + ".zip");

                        // Guardar el ZIP directamente en la respuesta HTTP
                        zip.Save(Response.OutputStream);
                        Response.End();
                        return new EmptyResult();
                    }
                }

                string mensaje = "<script LANGUAGE='JavaScript'>" +
                                 "window.alert('NO HAY DATOS POR MOSTRAR');" +
                                 "</script>";
                Response.Write(mensaje);
                Response.End();
                return new EmptyResult();
            }
            else
            {
                string mensaje = "<script LANGUAGE='JavaScript'>" +
                                 "window.alert('NO HAY DATOS POR MOSTRAR');" +
                                 "</script>";
                Response.Write(mensaje);
                Response.End();
                return new EmptyResult();
            }
        }

        public byte[] DevolverFacturaArray(int? ID)
        {
            try
            {
                var model = new Models.Reportes.Reportes();
                var usuariosModel = new Models.InicioSesion.Usuarios();

                string reportPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital2.rdlc");
                string rutaDocumentosAprobados = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];

                var facturas = model.GetFacturasByEstadoReporte(ID.Value, ref MsgRes);

                var factura = facturas.First();
                var idBase = model.AgregarValor(factura.id_cargue_base.ToString()) + factura.id_cargue_base;
                var idDetalle = factura.id_cargue_dtll.ToString();
                var valorEncriptado = BitConverter.ToString(RsaFileDemo.LaunchDemo(idDetalle, idDetalle)).Replace("-", "");

                var cadena = string.Join(",", factura.num_factura, factura.valor_neto, factura.num_id_prestador, valorEncriptado, idDetalle);
                var analista = factura.id_analista_gestiona;

                var firmaAnalista = model.GetFirmasId(analista);
                if (firmaAnalista == null)
                {
                    return null;
                }

                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                string imagenQr;
                using (var memoryStream = new MemoryStream())
                {
                    qrCode.GetGraphic(20).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    imagenQr = Convert.ToBase64String(memoryStream.ToArray());
                }

                var auditor = factura.id_auditor_asignado;
                var firmaAuditor = model.GetFirmasId(auditor);
                if (firmaAuditor == null)
                {
                    return null;
                }

                var viewer = new ReportViewer
                {
                    ProcessingMode = ProcessingMode.Local,
                    LocalReport =
                        {
                            ReportPath = reportPath
                        }
                };

                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(new ReportDataSource("FirmasDatSet", facturas));
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.SetParameters(new[]
                {
                    new ReportParameter("Imagen", @"file:///" + firmaAnalista.ruta),
                    new ReportParameter("Imagen2", imagenQr),
                    new ReportParameter("Imagen3", @"file:///" + firmaAuditor.ruta),
                });

                viewer.LocalReport.Refresh();

                var pdfContent = viewer.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streams, out Microsoft.Reporting.WebForms.Warning[] warnings);
                return pdfContent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult VerDetalleFactura(int? a, int? b, int? c)
        {
            //a = id dtll, b = id cargue, c = regional 

            managemenet_prestadores_traerDatosFacturas_idDetalleResult datos = new managemenet_prestadores_traerDatosFacturas_idDetalleResult();
            List<management_fis_cargueRips_usuariosResult> usuarios = new List<management_fis_cargueRips_usuariosResult>();

            management_fis_traerDatosContrato_nitResult contrato = new management_fis_traerDatosContrato_nitResult();
            base_beneficiarios beneficiario = new base_beneficiarios();
            management_fis_traerFechas_cuvResult fechas = new management_fis_traerFechas_cuvResult();
            management_fisRips_prestadorTieneContratoResult tieneContrato = new management_fisRips_prestadorTieneContratoResult();
            List<management_fis_traerDatosContrato_nitResult> listaContratos = new List<management_fis_traerDatosContrato_nitResult>();

            var cuv = "";
            var cedulaUsuario = "";
            var nombreUsuario = "";
            var tipoDocumentoUsuario = "";
            var filtradoCodPrestador = "";
            var conContrato = 0;
            var valorFactura = new decimal?();
            decimal? valorFacturaMenosDetalles = new decimal();
            decimal? valorTotalCups = new decimal();

            var listadoUsuarios = "";

            try
            {
                var listadoCups = BusClass.TraerListadoCupsFacturas(a);
                valorTotalCups = listadoCups.Sum(x => x.valor_cups ?? 0);

                Session["ListadoCupsCompleto"] = listadoCups;

                var listadoGlosas = BusClass.ListaGlosas(a);
                Session["ListadoGlosasCompleto"] = listadoGlosas;

                datos = BusClass.ListadoFacturasIdDetalle((int)a);
                if (datos != null)
                {
                    valorFactura = datos.valor_neto;

                    valorFacturaMenosDetalles = valorFactura - valorTotalCups;

                    tieneContrato = BusClass.UnPrestadorTeieneNegociacion(datos.nit);

                    conContrato = tieneContrato != null ? 1 : 0;

                    cuv = datos.cuv;

                    if (!string.IsNullOrEmpty(datos.codigo_prestador))
                    {
                        filtradoCodPrestador = datos.codigo_prestador;
                    }
                    else
                    {
                        filtradoCodPrestador = datos.nit;
                    }

                    if (!string.IsNullOrEmpty(filtradoCodPrestador))
                    {
                        contrato = BusClass.TraerDatosContratoNit(filtradoCodPrestador);
                    }

                    listaContratos = BusClass.TraerListaDatosContratoNit(filtradoCodPrestador);

                    fechas = BusClass.TraerLimiteFechasFactura(a);

                    usuarios = BusClass.ListadoRipsUsuarios(a);

                    if (usuarios != null)
                    {
                        foreach (var use in usuarios)
                        {
                            listadoUsuarios += string.IsNullOrEmpty(listadoUsuarios) ? Convert.ToString(use.id_usuarios) : "," + Convert.ToString(use.id_usuarios);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.idDetalle = a;
            ViewBag.idCargue = b;
            ViewBag.regional = c;
            ViewBag.ripsUsuarios = usuarios;
            ViewBag.listadoUsuarios = listadoUsuarios;

            ViewBag.nombreUsuario = nombreUsuario;
            ViewBag.contratos = contrato;
            ViewBag.ListaContratos = listaContratos;
            ViewBag.fechas = fechas;
            ViewBag.cuv = cuv;
            ViewBag.idUsuario = SesionVar.IDUser;
            ViewBag.conContrato = conContrato;
            ViewBag.valorFactura = valorFactura;

            ViewBag.departamentos = BusClass.TraerDepartamentosFisTodos();

            ViewBag.valorTotalCups = valorTotalCups;
            ViewBag.valorFacturaMenosDetalles = valorFacturaMenosDetalles;
            ViewBag.valorFacturaMenosDetallesResultado = valorFacturaMenosDetalles == 0 ? 1 : 0;

            return View(datos);
        }

        public ActionResult VerDetalleFactura2(int? a, int? b, int? c)
        {
            //a = id dtll, b = id cargue, c = regional 

            managemenet_prestadores_traerDatosFacturas_idDetalleResult datos = new managemenet_prestadores_traerDatosFacturas_idDetalleResult();
            List<management_fis_traerDatosContrato_nitResult> listaContratos = new List<management_fis_traerDatosContrato_nitResult>();
            List<management_fis_cargueRips_usuariosResult> usuarios = new List<management_fis_cargueRips_usuariosResult>();

            var cuv = "";
            var cedulaUsuario = "";
            var nombreUsuario = "";
            var tipoDocumentoUsuario = "";
            var filtradoCodPrestador = "";
            var conContrato = 0;
            var valorFactura = new decimal?();

            List<management_fis_facturasCuv_conBeneficiariosResult> listadoCups = new List<management_fis_facturasCuv_conBeneficiariosResult>();
            management_fis_traerFechas_cuvResult fechas = new management_fis_traerFechas_cuvResult();
            List<management_fisFacturas_glosaResult> listadoGlosas = new List<management_fisFacturas_glosaResult>();
            var listadoUsuarios = "";

            try
            {
                listadoCups = BusClass.TraerListadoCupsFacturasConBenficiarios(a).OrderBy(x => x.tipoDocumentoIdentificacion).ToList();
                listadoGlosas = BusClass.ListaGlosas(a);

                Session["ListadoCupsCompleto"] = listadoCups;
                Session["ListadoGlosasCompleto"] = listadoGlosas;

                datos = BusClass.ListadoFacturasIdDetalle((int)a);
                if (datos != null)
                {
                    valorFactura = datos.valor_neto;
                    conContrato = listadoCups.Select(x => x.presConCOntrato).FirstOrDefault() != 0 ? 1 : 0;

                    cuv = datos.cuv;

                    if (!string.IsNullOrEmpty(datos.codigo_prestador))
                    {
                        filtradoCodPrestador = datos.codigo_prestador;
                    }
                    else
                    {
                        filtradoCodPrestador = datos.nit;
                    }

                    listaContratos = BusClass.TraerListaDatosContratoNit(filtradoCodPrestador);
                    fechas = BusClass.TraerLimiteFechasFactura(a);

                    usuarios = BusClass.ListadoRipsUsuarios(a);

                    if (usuarios != null)
                    {
                        foreach (var use in usuarios)
                        {
                            listadoUsuarios += string.IsNullOrEmpty(listadoUsuarios) ? Convert.ToString(use.id_usuarios) : "," + Convert.ToString(use.id_usuarios);
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.idDetalle = a;
            ViewBag.idCargue = b;
            ViewBag.regional = c;
            ViewBag.ListaContratos = listaContratos;

            ViewBag.cuv = cuv;
            ViewBag.fechas = fechas;

            ViewBag.idUsuario = SesionVar.IDUser;
            ViewBag.conContrato = listaContratos.Count() > 0 ? 1 : 0;
            ViewBag.valorFactura = valorFactura;

            ViewBag.departamentos = BusClass.TraerDepartamentosFisTodos();

            ViewBag.listadoUsuarios = listadoUsuarios;

            ViewBag.listadoCups = listadoCups;
            ViewBag.listadoGlosas = listadoGlosas;

            return View(datos);
        }

        public StringBuilder TraerDetalleIndividualCups(int? idRegistro, int? idFactura, int? tipoPintada)
        {
            //tipoPintada 1 : viene de analista, tipoíntada 2: viene de auditor
            var resultado = new StringBuilder();
            const string estiloSombreado = "height: 100%; background-color: #b1b1b1 !important; border: 1px solid #dcdcdc; padding: 10px; border-radius: 5px; font-weight: bold; color: #000000; text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);";
            var monedaCol = new CultureInfo("es-CO");

            try
            {
                management_fis_facturasCuv_conBeneficiarios_idRegistroResult registro = BusClass.TraerCupsRegistro(idFactura, idRegistro);

                if (registro != null)
                {
                    var codGlosa = registro.codConGlosa != null && registro.cod_cups == registro.codConGlosa ? $"class='Sombreado'" : "";

                    // Agregar fila
                    resultado.AppendLine($"<tr id ='tr_{registro.id_registro}'>");
                    resultado.AppendLine($"<td {codGlosa}>{registro.id_registro}</td>");

                    resultado.AppendLine($"<td {codGlosa}><input type=\"checkBox\" id=\"chekregistro_{registro.id_usuario}-{registro.id_registro}-{registro.id_factura}-{registro.id_recep_facturas_cargue_base}-{registro.tipo}\" name=\"chekregistro_{registro.cod_cups}-{registro.id_usuario}-{registro.id_registro}-{registro.id_factura}-{registro.id_recep_facturas_cargue_base}-{registro.tipo}\"/>");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"codCups_{registro.id_registro}-{registro.id_usuario}\" value=\"{registro.cod_cups}\" />");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"codigo_cuv_{registro.id_registro}-{registro.id_usuario}\" value=\"{registro.cod_cuv}\" />");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"tipo_{registro.id_registro}-{registro.id_usuario}\" value=\"{registro.tipo}\" />");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"valorCups_{registro.id_registro}-{registro.id_usuario}\" value=\"{registro.valor_cups}\" />");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"valorindividualCups_{registro.id_registro}-{registro.id_usuario}\" value=\"{registro.valor_individual}\" />");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"codPrestador_{registro.id_registro}\" value=\"{registro.codigo_prestador}-{registro.id_usuario}\" />");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"idRips_{registro.id_registro}-{registro.id_usuario}\" value=\"{registro.id_registro}\" />");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"cantidad_{registro.id_registro}-{registro.id_usuario}\" value=\"{registro.conteo_cups}\" />");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"fechaPrestacion_{registro.id_registro}-{registro.id_usuario}\" value=\"{registro.fecha_prestacion}\" /> </td>");

                    resultado.AppendLine($"<input type=\"hidden\" id=\"cie10_{registro.id_registro}\" value=\"{registro.cie10}\" /> </td>");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"descripcioncie10_{registro.id_registro}\" value=\"{registro.descripcion_cie10}\" /> </td>");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"cie10relacionado_{registro.id_registro}\" value=\"{registro.cie10_relacionado}\" /> </td>");
                    resultado.AppendLine($"<input type=\"hidden\" id=\"descripcioncie10relacionado_{registro.id_registro}\" value=\"{registro.descripcion_cie10_relacionado}\" /> </td>");

                    resultado.AppendLine($" <td {codGlosa}>{registro.documentoUsuario}</td>");
                    resultado.AppendLine($" <td {codGlosa}>{registro.nombreUsuario}</td>");
                    resultado.AppendLine($" <td {codGlosa}>{registro.cod_cups}</td>");
                    resultado.AppendLine($" <td {codGlosa}>{registro.descripcion_cuvs}</td>");
                    resultado.AppendLine($" <td {codGlosa}>{registro.fecha_prestacion}</td>");

                    resultado.AppendLine($"<td {codGlosa}>");
                    resultado.AppendLine($"<label onclick='EditarCIE10({registro.id_registro} , {registro.id_factura})'> {registro.cie10} - {registro.descripcion_cie10}");
                    resultado.AppendLine("</td>");

                    resultado.AppendLine($"<td {codGlosa}>");
                    resultado.AppendLine($"<label onclick='EditarCIE10({registro.id_registro} , {registro.id_factura})'> {registro.cie10_relacionado} - {registro.descripcion_cie10_relacionado}");
                    resultado.AppendLine("</td>");

                    resultado.AppendLine($" <td {codGlosa}>{registro.conteo_cups}</td>");
                    resultado.AppendLine($" <td {codGlosa}>{registro.valor_individual?.ToString("C", monedaCol)}</td>");
                    resultado.AppendLine($" <td {codGlosa}>{registro.valor_cups?.ToString("C", monedaCol)}</td>");
                    resultado.AppendLine($" <td {codGlosa}>{registro.tipo_iva}</td>");

                    if (tipoPintada == 1)
                    {
                        resultado.AppendLine($"<td {codGlosa}>");
                        resultado.AppendLine($"<select id='ivaCalculado_{registro.id_registro}' onchange='RecalcularIva({registro.id_registro}, this.value, {registro.id_usuario} ,{registro.id_factura})'>");
                        foreach (var opcion in new[] { "26", "LA", "LB" })
                        {
                            var selected = opcion == registro.tipo_iva ? "selected" : "";
                            resultado.AppendLine($"<option value='{opcion}' {selected}>{opcion}</option>");
                        }
                        resultado.AppendLine("</select>");
                        resultado.AppendLine("</td>");
                    }

                    resultado.AppendLine($" <td {codGlosa}>{registro.ValorNetoConIVA?.ToString("C", monedaCol)}</td>");
                    resultado.AppendLine($" <td {codGlosa}>{registro.codigo_tiga}</td>");
                    resultado.AppendLine($" <td {codGlosa}>{registro.descripcion_tiga}</td>");
                    resultado.AppendLine($" <td style='background-color:{(registro.existeGlosaLevantada == 1 ? "#DBC5C5" : "transparent")}'>{registro.glosa_automatica?.ToString("C", monedaCol)}</td>");

                    resultado.AppendLine($"<td {codGlosa}>");
                    resultado.AppendLine($"<a class=\"btn btn-xs button_Asalud_Aceptar\" onclick=\"AgregarGlosa('{registro.id_registro}','{registro.id_usuario}')\">Agregar</a>");
                    resultado.AppendLine("</td>");
                    resultado.AppendLine($"<td {codGlosa}>");
                    resultado.AppendLine($"<a class=\"btn btn-xs button_Asalud_Guardar\" onclick=\"EditarRips('{registro.id_registro}','{registro.id_usuario}', '{registro.id_transaccion}', {tipoPintada})\">Editar</a>");

                    if (tipoPintada == 1)
                    {
                        resultado.AppendLine($"<a class=\"btn btn-xs button_Asalud_Rechazar\" onclick=\"EliminarRips('{registro.id_registro}', '{registro.id_usuario}', '{registro.id_factura}')\">Eliminar</a>");
                    }

                    resultado.AppendLine("</td>");
                    resultado.AppendLine("</tr>");

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return resultado;
        }

        public StringBuilder TraerGlosaIndividual(int? idGlosa, int? idFactura, int? tipoPintada)
        {
            //tipoPintada 1 : viene de analista, tipoíntada 2: viene de auditor

            var monedaCol = new CultureInfo("es-CO");
            var resultado = new StringBuilder();

            try
            {
                management_fisFacturas_glosa_idGlopsaResult registro = BusClass.traerGlosaId(idFactura, idGlosa);
                if (registro != null)
                {
                    resultado.AppendLine($"<tr id=tr_{registro.id_glosa}>");
                    resultado.AppendLine($"    <td>");
                    resultado.AppendLine($"        <input type=\"checkbox\" id=\"ch_{registro.id_usuarios}-{registro.id_glosa}-{registro.id_factura}-{registro.id_cargue}\"");
                    resultado.AppendLine($"               name=\"ch_{registro.id_usuarios}-{registro.id_glosa}-{registro.id_factura}-{registro.id_cargue}\" />");
                    resultado.AppendLine($"    </td>");
                    resultado.AppendLine($"    <td>");
                    resultado.AppendLine($"        {registro.id_glosa}");
                    resultado.AppendLine($"        <input type=\"hidden\" id=\"cantidadGlosa_{registro.id_glosa}\" value=\"{registro.cantidad}\" />");
                    resultado.AppendLine($"        <input type=\"hidden\" id=\"valorGlosa_{registro.id_glosa}\" value=\"{registro.valor_glosado}\" />");
                    resultado.AppendLine($"    </td>");
                    resultado.AppendLine($"    <td>{registro.documentoUsuario}</td>");
                    resultado.AppendLine($"    <td>{registro.nombreUsuario}</td>");
                    resultado.AppendLine($"    <td>{registro.codCups}</td>");
                    resultado.AppendLine($"    <td>{registro.tipo}</td>");
                    resultado.AppendLine($"    <td>{registro.descripcionGeneral}</td>");
                    resultado.AppendLine($"    <td>{registro.descripcionEspecifico}</td>");
                    resultado.AppendLine($"    <td>{registro.descripcionAplicacion}</td>");
                    resultado.AppendLine($"    <td>{registro.valor_glosado?.ToString("C", monedaCol)}</td>");
                    resultado.AppendLine($"    <td>{registro.cantidad}</td>");
                    resultado.AppendLine($"    <td>{registro.valorTotal?.ToString("C", monedaCol)}</td>");
                    resultado.AppendLine($"    <td>");
                    resultado.AppendLine($"        <span class=\"observaciones_{registro.id_glosa}\">{(registro.observacion.Length > 100 ? registro.observacion.Substring(0, 100) : registro.observacion)}</span>");
                    resultado.AppendLine($"        <span class=\"observaciones_completas_{registro.id_glosa}\" style=\"display: none;\">{registro.observacion}</span>");
                    if (registro.observacion.Length > 100)
                    {
                        resultado.AppendLine($"        <label class=\"text-secondary_asalud botonMostrar_{registro.id_glosa}\" onclick=\"MostrarTextoCompleto({registro.id_glosa})\">Mostrar</label>");
                        resultado.AppendLine($"        <label class=\"text-secondary_asalud botonOcultar_{registro.id_glosa}\" style=\"display: none;\" onclick=\"OcultarTextoCompleto({registro.id_glosa})\">Ocultar</label>");
                    }
                    resultado.AppendLine($"    </td>");
                    resultado.AppendLine($"    <td>");
                    resultado.AppendLine($"        <a class=\"btn btn-xs button_Asalud_Aceptar\" onclick=\"LevantarGlosa({registro.id_glosa}, {registro.id_usuarios})\">");
                    resultado.AppendLine($"            <i class=\"glyphicon glyphicon-edit\"></i>&nbsp; Levantar");
                    resultado.AppendLine($"        </a>");
                    resultado.AppendLine($"    </td>");
                    if (tipoPintada == 1)
                    {

                        resultado.AppendLine($"    <td>");
                        resultado.AppendLine($"        <a class=\"btn btn-xs button_Asalud_Rechazar\" onclick=\"DeleteGlosa({registro.id_glosa}, {registro.id_usuarios})\">");
                        resultado.AppendLine($"            <i class=\"glyphicon glyphicon-trash\"></i>&nbsp; Eliminar");
                        resultado.AppendLine($"        </a>");
                        resultado.AppendLine($"        &nbsp;");
                        resultado.AppendLine($"        <a class=\"btn btn-xs button_Asalud_Guardar\" onclick=\"EditarGlosa({registro.id_glosa}, {registro.id_usuarios})\" data-toggle=\"modal\" data-target=\"#modalGlosa\">");
                        resultado.AppendLine($"            <i class=\"glyphicon glyphicon-edit\"></i>&nbsp; Editar");
                        resultado.AppendLine($"        </a>");
                        resultado.AppendLine($"    </td>");

                    }
                    resultado.AppendLine($"</tr>");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return resultado;
        }

        public string DevolverValoresFactura(int? idFactura)
        {
            var resultado = "";
            try
            {
                management_fis_TraerValoresFacturaResult valores = BusClass.TraerValoreFactura(idFactura);
                resultado += valores.valor_neto + "_" + valores.valorMenosDetalles + "_" + valores.valorGlosas + "_" + valores.valorCups + "_" + valores.ValorNetoConIVA;
            }
            catch (Exception ex)
            {
                var eror = ex.Message;
            }

            return resultado;


        }

        public JsonResult BuscarCupsIdFactura(int? idFactura)
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    term = term.ToUpper();

                    List<management_fisBuscarCupsResult> cup = BusClass.TraerListaCupsIdFactura(term, idFactura);

                    var lista = (from cu in cup
                                 select new
                                 {
                                     id = cu.cod_cups,
                                     label = cu.cod_cups + "-" + cu.descripcion_cuvs,
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

        public PartialViewResult ModalCrearGlosa(int? idGlosa, int? idRips, string codCups, string codigo_cuv, string tipo,
            int? idCargue, int? idDetalle, decimal? valorGlosa, decimal? valorGlosaTotal, int? tipoIngreso, string codPrestador,
            int? cantidadMaxima, int? idUsuario)
        {
            fis_rips_facturas_glosa glosa = new fis_rips_facturas_glosa();

            ViewBag.codCups = codCups;
            ViewBag.codigo_cuv = codigo_cuv;
            ViewBag.tipo = tipo;
            ViewBag.idCargue = idCargue;
            ViewBag.idDetalle = idDetalle;
            ViewBag.valorGlosa = valorGlosa;
            ViewBag.valorGlosaTotal = valorGlosaTotal;
            ViewBag.tipoIngreso = tipoIngreso;
            ViewBag.codPrestador = codPrestador;
            ViewBag.cantidadMaxima = cantidadMaxima;
            ViewBag.idUsuario = idUsuario;

            ViewBag.conceptoGeneral = BusClass.ListadoConceptosGeneral();

            glosa = idGlosa != null ? BusClass.TraerGlosaRips(idGlosa) : new fis_rips_facturas_glosa();

            ViewBag.idGlosa = idGlosa;

            ViewBag.glosa = glosa;
            ViewBag.idRips = idRips;

            return PartialView(glosa);
        }

        public string ObtenerEspecificos(int? general)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<ref_fisFacturas_conceptoEspecifico> especificos = new List<ref_fisFacturas_conceptoEspecifico>();

            try
            {
                especificos = BusClass.ListadoConceptosEspecifico(general);
                foreach (var item in especificos)
                {
                    result += "<option value='" + item.id_tipo + "'>" + item.codigo + "-" + item.descripcion + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public string ObtenerAplicacion(int? especifico)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<ref_fisFacturas_conceptoAplicacion> aplicacion = new List<ref_fisFacturas_conceptoAplicacion>();

            try
            {
                aplicacion = BusClass.ListadoConceptosAplicacion(especifico);
                foreach (var item in aplicacion)
                {
                    result += "<option value='" + item.id_tipo + "'>" + item.codigo + "-" + item.descripcion + "</option>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public JsonResult GuardarGlosa(Models.FIS.Facturacion_FIS modelo)
        {
            var mensaje = "";
            var rta = 0;

            fis_rips_facturas_glosa glosa = new fis_rips_facturas_glosa();

            try
            {
                string restriccion = ValidarSumatoriaGlosas(modelo.idRips, modelo.id_factura, modelo.idUsuario, modelo.codCups, modelo.valor_glosado);
                if (restriccion != "")
                {
                    throw new Exception(restriccion);
                }

                if (modelo.valor_glosado == 0)
                {
                    throw new Exception("Valor de la glosa debe ser mayor a 0");
                }

                glosa.id_registroRips = modelo.idRips;
                glosa.id_cargue = modelo.id_cargue;
                glosa.id_factura = modelo.id_factura;
                glosa.id_usuario = modelo.idUsuario;
                glosa.codigo_prestador = modelo.codPrestador;
                glosa.codCups = modelo.codCups;
                glosa.codigo_cuv = modelo.codigo_cuv;
                glosa.tipo = modelo.tipo;
                glosa.concepto_general = modelo.concepto_general;
                glosa.concepto_especifico = modelo.concepto_especifico;
                glosa.concepto_aplicacion = modelo.concepto_aplicacion;
                glosa.valor_glosado = modelo.valor_glosado;
                glosa.valor_factura = modelo.valor_factura;
                glosa.cantidad = modelo.cantidad;
                glosa.cantidadMaxima = modelo.cantidadMaxima;
                glosa.observacion = modelo.observacion;
                glosa.fecha_digita = DateTime.Now;
                glosa.usuario_digita = SesionVar.UserName;
                glosa.tipo_ingreso = modelo.tipoIngreso;
                glosa.en_prestadores = 1;
                glosa.estado = 1;
                var inserta = 0;
                if (modelo.id_glosa != null && modelo.id_glosa != 0)
                {
                    glosa.id_glosa = modelo.id_glosa;
                    inserta = BusClass.ActualizarGlosaRipsFactura(glosa);
                }
                else
                {
                    inserta = BusClass.InsertarGlosaFacturaFis(glosa);
                }

                if (inserta != 0)
                {
                    var listadoCups = BusClass.TraerListadoCupsFacturas(modelo.id_factura);
                    Session["ListadoCupsCompleto"] = listadoCups;

                    var listadoGlosas = BusClass.ListaGlosas(modelo.id_factura);
                    Session["ListadoGlosasCompleto"] = listadoGlosas;

                    mensaje = "GLOSA INGRESADA CORRECTAMENTE";
                    rta = inserta;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE LA GLOSA";
                }
            }
            catch (Exception ex)
            {
                mensaje = "ERROR EN EL INGRESO DE LA GLOSA: " + ex.Message;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        //public string TraerDetalleCups(string cuv, int? idDetalle, int? idUsuario, int? tipo)
        //{
        //    //tipo 1 = analista, tipo 2 = auditor
        //    var resultado = new StringBuilder();
        //    int i = 0;
        //    var totalCups = new decimal?(0);
        //    CultureInfo monedaCol = new CultureInfo("es-CO");

        //    var cie10 = "";
        //    var descie10 = "";

        //    var cie10_relacionado = "";
        //    var descie10_relacionado = "";
        //    var usuarioTieneGlosa = 0;
        //    string codGlosa = "";
        //    string estiloSombreado = "height: 100%;" +
        //                    "background-color: #b1b1b1 !important; " +
        //                    "border: 1px solid #dcdcdc; " +
        //                    "padding: 10px; " +
        //                    "border-radius: 5px; " +
        //                    "font-weight: bold; " +
        //                    "color: #000000; " +
        //                    "text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);";

        //    try
        //    {
        //        List<management_fis_facturasCuvResult> listadoCups = BusClass.TraerListadoCupsFacturas(cuv, idDetalle, idUsuario);
        //        management_fis_facturasCuvResult listadoCupscie = listadoCups.Where(x => x.cie10 != "" && x.cie10 != null).FirstOrDefault() != null ? listadoCups.Where(x => x.cie10 != "" && x.cie10 != null).FirstOrDefault() : new management_fis_facturasCuvResult();

        //        if (listadoCups.Count > 0)
        //        {
        //            foreach (var item in listadoCups)
        //            {
        //                if (listadoCupscie != null)
        //                {
        //                    cie10 = listadoCupscie.cie10 != "" ? listadoCupscie.cie10 : cie10;
        //                    descie10 = listadoCupscie.descripcion_cie10 != "" ? listadoCupscie.descripcion_cie10 : descie10;
        //                    cie10_relacionado = listadoCupscie.cie10_relacionado != "" ? listadoCupscie.cie10_relacionado : cie10_relacionado;
        //                    descie10_relacionado = listadoCupscie.descripcion_cie10_relacionado != "" ? listadoCupscie.descripcion_cie10_relacionado : descie10_relacionado;
        //                }

        //                if (usuarioTieneGlosa == 0)
        //                {
        //                    if (item.usuarioConGlosa != null && item.id_usuario != 0)
        //                    {
        //                        usuarioTieneGlosa = 1;
        //                    }
        //                }

        //                i++;
        //                var existeBen = item.existeBeneficiario == 0 ? " style='background-color:#DBC5C5;'" : "";

        //                codGlosa = item.codConGlosa != null ? " style='" + estiloSombreado + "'' " : "";

        //                resultado.AppendLine($"<tr>");
        //                resultado.AppendLine($"<td {codGlosa}>{i}</td>");
        //                resultado.AppendLine($"<td {codGlosa} ><input type=\"checkBox\" id=\"chekregistro_{item.id_usuario}-{item.id_registro}-{item.id_factura}-{item.id_recep_facturas_cargue_base}-{item.tipo}\" name=\"chekregistro_{item.cod_cups}-{item.id_usuario}-{item.id_registro}-{item.id_factura}-{item.id_recep_facturas_cargue_base}-{item.tipo}\"/></td>");
        //                resultado.AppendLine($"<input type=\"hidden\" id=\"codCups_{i}-{item.id_usuario}\" name=\"codCups_{i}-{item.id_usuario}\" value=\"{item.cod_cups}\" />");
        //                resultado.AppendLine($"<input type=\"hidden\" id=\"codigo_cuv_{i}-{item.id_usuario}\" name=\"codigo_cuv_{i}-{item.id_usuario}\" value=\"{item.cod_cuv}\" />");
        //                resultado.AppendLine($"<input type=\"hidden\" id=\"tipo_{i}-{item.id_usuario}\" name=\"tipo_{i}-{item.id_usuario}\" value=\"{item.tipo}\" />");
        //                resultado.AppendLine($"<input type=\"hidden\" id=\"valorCups_{i}-{item.id_usuario}\" name=\"valorCups_{i}-{item.id_usuario}\" value=\"{item.valor_cups}\" />");
        //                resultado.AppendLine($"<input type=\"hidden\" id=\"valorindividualCups_{i}-{item.id_usuario}\" name=\"valorindividualCups_{i}-{item.id_usuario}\" value=\"{item.valor_individual}\" />");
        //                resultado.AppendLine($"<input type=\"hidden\" id=\"codPrestador_{i}\" name=\"codPrestador_{i}-{item.id_usuario}\" value=\"{item.codigo_prestador}-{item.id_usuario}\" />");
        //                resultado.AppendLine($"<input type=\"hidden\" id=\"idRips_{i}-{item.id_usuario}\" name=\"idRips_{i}-{item.id_usuario}\" value=\"{item.id_registro}\" />");
        //                resultado.AppendLine($"<input type=\"hidden\" id=\"cantidad_{i}-{item.id_usuario}\" name=\"cantidad_{i}-{item.id_usuario}\" value=\"{item.conteo_cups}\" />");
        //                resultado.AppendLine($"<input type=\"hidden\" id=\"fechaPrestacion_{i}-{item.id_usuario}\" name=\"fechaPrestacion_{i}-{item.id_usuario}\" value=\"{item.fecha_prestacion}\" />");

        //                resultado.AppendLine($"<td {codGlosa}>{item.documentoUsuario}</td>");
        //                resultado.AppendLine($"<td {codGlosa}>{item.nombreUsuario}</td>");

        //                resultado.AppendLine($"<td {codGlosa}>{item.cod_cups}</td>");
        //                resultado.AppendLine($"<td {codGlosa}>{item.descripcion_cuvs}</td>");
        //                resultado.AppendLine($"<td {codGlosa}>{item.fecha_prestacion}</td>");
        //                resultado.AppendLine($"<td {codGlosa}>{item.conteo_cups}</td>");
        //                resultado.AppendLine($"<td {codGlosa}>{item.valor_individual.Value.ToString("C", monedaCol)}</td>");
        //                resultado.AppendLine($"<td {codGlosa}>{item.valor_cups.Value.ToString("C", monedaCol)}</td>");

        //                resultado.AppendLine($"<td {codGlosa}>{item.tipo_iva}</td>");
        //                resultado.AppendLine($"<td {codGlosa}>{item.ValorNetoConIVA.Value.ToString("C", monedaCol)}</td>");

        //                resultado.AppendLine($"<td {codGlosa}>{item.codigo_tiga}</td>");
        //                resultado.AppendLine($"<td {codGlosa}>{item.descripcion_tiga}</td>");

        //                var glosaCellStyle = item.existeGlosaLevantada == 1 ? " style='background-color:#DBC5C5;'" : "";
        //                resultado.AppendLine($"<td{glosaCellStyle}>{item.glosa_automatica.Value.ToString("C", monedaCol)}</td>");

        //                resultado.AppendLine($"<td {codGlosa}> ");
        //                resultado.AppendLine($"<a class=\"btn btn-xs button_Asalud_Aceptar\" onclick=\"AgregarGlosa('{i}','{item.id_usuario}')\" data-toggle=\"modal\" data-target=\"#modalGlosa\">Agregar &nbsp<i class=\"glyphicon glyphicon-open\" style=\"align-content:center;\"></i></a>");
        //                resultado.AppendLine("</td>");

        //                resultado.AppendLine($"<td {codGlosa}>");
        //                resultado.AppendLine($"<a class=\"btn btn-xs button_Asalud_Guardar\" onclick=\"EditarRips('{item.id_registro}','{item.id_usuario}', '{item.id_transaccion}', {tipo})\" data-toggle=\"modal\" data-target=\"#modalRegistroRips\">Editar &nbsp<i class=\"glyphicon glyphicon-edit\" style=\"align-content:center;\"></i></a>");

        //                if (tipo == 1)
        //                {
        //                    resultado.AppendLine($"<a class=\"btn btn-xs button_Asalud_Rechazar\" onclick=\"EliminarRips('{item.id_registro}', '{item.id_usuario}')\">Eliminar &nbsp<i class=\"glyphicon glyphicon-trash\" style=\"align-content:center;\"></i></a>");
        //                }
        //                resultado.AppendLine("</td>");


        //                if (tipo == 1)
        //                {
        //                    resultado.AppendLine($"<td {codGlosa}>");
        //                    resultado.AppendLine($"<select id='ivaCalculado_{item.id_registro}' name='ivaCalculado_{item.id_registro}' class='form-control' onchange='RecalcularIva({item.id_registro}, this.value, {item.id_usuario})'>");
        //                    var opcionesIva = new List<string> { "26", "LA", "LB" };
        //                    foreach (var opcion in opcionesIva)
        //                    {
        //                        if (opcion == item.tipo_iva)
        //                        {
        //                            resultado.AppendLine($"<option value='{opcion}' selected='{opcion}'>{opcion}</option>");
        //                        }
        //                        else
        //                        {
        //                            resultado.AppendLine($"<option value='{opcion}'>{opcion}</option>");
        //                        }
        //                    }
        //                    resultado.AppendLine("</select>");
        //                    resultado.AppendLine("</td>");
        //                }

        //                resultado.AppendLine("</tr>");
        //                totalCups += +item.ValorNetoConIVA;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return $"{resultado}|{i}|{totalCups}|{cie10}-{descie10}|{cie10_relacionado}-{descie10_relacionado}|{idUsuario}-{usuarioTieneGlosa}";
        //}



        public string TraerDetalleCups(string cuv, int? idDetalle, int? idUsuario, int? tipo, int? nuevo)
        {
            // Tipo 1 = Analista, Tipo 2 = Auditor
            var resultado = new StringBuilder();
            int i = 0;
            var totalCups = new decimal?(0);
            var monedaCol = new CultureInfo("es-CO");

            string cie10 = "", descie10 = "", cie10Relacionado = "", descie10Relacionado = "";
            int usuarioTieneGlosa = 0;

            const string estiloSombreado = "height: 100%; background-color: #b1b1b1 !important; border: 1px solid #dcdcdc; padding: 10px; border-radius: 5px; font-weight: bold; color: #000000; text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);";

            try
            {
                var listadoCups = (List<management_fis_facturasCuvResult>)Session["ListadoCupsCompleto"];
                listadoCups = listadoCups.Where(x => x.id_usuario == idUsuario).ToList();

                if (nuevo == 1)
                {
                    listadoCups = BusClass.TraerListadoCupsFacturas(idDetalle);
                    Session["ListadoCupsCompleto"] = listadoCups;

                    listadoCups = listadoCups.Where(x => x.id_usuario == idUsuario).ToList();

                }

                var listadoCupscie = listadoCups.FirstOrDefault(x => !string.IsNullOrEmpty(x.cie10)) ?? new management_fis_facturasCuvResult();

                // Asignar valores CIE10 si están disponibles
                cie10 = listadoCupscie.cie10 ?? cie10;
                descie10 = listadoCupscie.descripcion_cie10 ?? descie10;
                cie10Relacionado = listadoCupscie.cie10_relacionado ?? cie10Relacionado;
                descie10Relacionado = listadoCupscie.descripcion_cie10_relacionado ?? descie10Relacionado;

                if (listadoCups.Any())
                {
                    foreach (var item in listadoCups)
                    {
                        usuarioTieneGlosa |= item.usuarioConGlosa != null && item.id_usuario != 0 ? 1 : 0;

                        i++;
                        var existeBen = item.existeBeneficiario == 0 ? " style='background-color:#DBC5C5;'" : "";
                        var codGlosa = item.codConGlosa != null && item.cod_cups == item.codConGlosa ? $" style='{estiloSombreado}'" : "";

                        // Agregar fila
                        resultado.AppendLine("<tr>");
                        resultado.AppendLine($"<td {codGlosa}>{i}</td>");
                        resultado.AppendLine(GenerarCheckbox(item, i, codGlosa));
                        resultado.AppendLine(GenerarInputsOcultos(item, i));
                        resultado.AppendLine(GenerarCeldas(item, codGlosa, monedaCol));
                        resultado.AppendLine(GenerarAcciones(item, i, tipo, codGlosa));
                        resultado.AppendLine("</tr>");

                        totalCups += item.ValorNetoConIVA ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores (puede mejorarse según la lógica requerida)
                var error = ex.Message;
            }

            return $"{resultado}|{i}|{totalCups}|{cie10}-{descie10}|{cie10Relacionado}-{descie10Relacionado}|{idUsuario}-{usuarioTieneGlosa}";
        }

        private string GenerarCheckbox(management_fis_facturasCuvResult item, int i, string codGlosa)
        {
            return $"<td {codGlosa}><input type=\"checkBox\" id=\"chekregistro_{item.id_usuario}-{item.id_registro}-{item.id_factura}-{item.id_recep_facturas_cargue_base}-{item.tipo}\" name=\"chekregistro_{item.cod_cups}-{item.id_usuario}-{item.id_registro}-{item.id_factura}-{item.id_recep_facturas_cargue_base}-{item.tipo}\"/></td>";
        }

        private string GenerarInputsOcultos(management_fis_facturasCuvResult item, int i)
        {
            var inputs = new StringBuilder();
            inputs.AppendLine($"<input type=\"hidden\" id=\"codCups_{i}-{item.id_usuario}\" value=\"{item.cod_cups}\" />");
            inputs.AppendLine($"<input type=\"hidden\" id=\"codigo_cuv_{i}-{item.id_usuario}\" value=\"{item.cod_cuv}\" />");
            inputs.AppendLine($"<input type=\"hidden\" id=\"tipo_{i}-{item.id_usuario}\" value=\"{item.tipo}\" />");
            inputs.AppendLine($"<input type=\"hidden\" id=\"valorCups_{i}-{item.id_usuario}\" value=\"{item.valor_cups}\" />");
            inputs.AppendLine($"<input type=\"hidden\" id=\"valorindividualCups_{i}-{item.id_usuario}\" value=\"{item.valor_individual}\" />");
            inputs.AppendLine($"<input type=\"hidden\" id=\"codPrestador_{i}\" value=\"{item.codigo_prestador}-{item.id_usuario}\" />");
            inputs.AppendLine($"<input type=\"hidden\" id=\"idRips_{i}-{item.id_usuario}\" value=\"{item.id_registro}\" />");
            inputs.AppendLine($"<input type=\"hidden\" id=\"cantidad_{i}-{item.id_usuario}\" value=\"{item.conteo_cups}\" />");
            inputs.AppendLine($"<input type=\"hidden\" id=\"fechaPrestacion_{i}-{item.id_usuario}\" value=\"{item.fecha_prestacion}\" />");
            return inputs.ToString();
        }

        private string GenerarCeldas(management_fis_facturasCuvResult item, string codGlosa, CultureInfo monedaCol)
        {
            return $@"
            <td {codGlosa}>{item.documentoUsuario}</td>
            <td {codGlosa}>{item.nombreUsuario}</td>
            <td {codGlosa}>{item.cod_cups}</td>
            <td {codGlosa}>{item.descripcion_cuvs}</td>
            <td {codGlosa}>{item.fecha_prestacion}</td>
            <td {codGlosa}>{item.conteo_cups}</td>
            <td {codGlosa}>{item.valor_individual?.ToString("C", monedaCol)}</td>
            <td {codGlosa}>{item.valor_cups?.ToString("C", monedaCol)}</td>
            <td {codGlosa}>{item.tipo_iva}</td>
            <td {codGlosa}>{item.ValorNetoConIVA?.ToString("C", monedaCol)}</td>
            <td {codGlosa}>{item.codigo_tiga}</td>
            <td {codGlosa}>{item.descripcion_tiga}</td>
            <td style='background-color:{(item.existeGlosaLevantada == 1 ? "#DBC5C5" : "transparent")}'>{item.glosa_automatica?.ToString("C", monedaCol)}</td>
            ";
        }

        private string GenerarAcciones(management_fis_facturasCuvResult item, int i, int? tipo, string codGlosa)
        {
            var acciones = new StringBuilder();
            acciones.AppendLine($"<td {codGlosa}>");
            acciones.AppendLine($"<a class=\"btn btn-xs button_Asalud_Aceptar\" onclick=\"AgregarGlosa('{i}','{item.id_usuario}')\">Agregar</a>");
            acciones.AppendLine("</td>");
            acciones.AppendLine($"<td {codGlosa}>");
            acciones.AppendLine($"<a class=\"btn btn-xs button_Asalud_Guardar\" onclick=\"EditarRips('{item.id_registro}','{item.id_usuario}', '{item.id_transaccion}', {tipo})\">Editar</a>");

            if (tipo == 1)
            {
                acciones.AppendLine($"<a class=\"btn btn-xs button_Asalud_Rechazar\" onclick=\"EliminarRips('{item.id_registro}', '{item.id_usuario}', '{item.id_factura}')\">Eliminar</a>");
                acciones.AppendLine("</td>");

                acciones.AppendLine($"<td {codGlosa}>");

                acciones.AppendLine($"<select id='ivaCalculado_{item.id_registro}' onchange='RecalcularIva({item.id_registro}, this.value, {item.id_usuario})'>");
                foreach (var opcion in new[] { "26", "LA", "LB" })
                {
                    var selected = opcion == item.tipo_iva ? "selected" : "";
                    acciones.AppendLine($"<option value='{opcion}' {selected}>{opcion}</option>");
                }
                acciones.AppendLine("</select>");
            }

            acciones.AppendLine("</td>");

            acciones.AppendLine("</tr>");
            return acciones.ToString();
        }

        public string TraerGlosas(int? idDetalle, int? tipo, int? idUsuario, int? nuevo)
        {
            // tipo 1 = analistas, tipo 2 = auditores

            StringBuilder resultado = new StringBuilder();
            int i = 0;
            var totalGlosas = new decimal?(0);
            CultureInfo monedaCol = new CultureInfo("es-CO");

            try
            {
                List<management_fisFacturas_glosaResult> listado = (List<management_fisFacturas_glosaResult>)Session["ListadoGlosasCompleto"];
                listado = listado.Where(x => x.id_usuarios == idUsuario).ToList();

                if (nuevo == 1)
                {
                    listado = BusClass.ListaGlosas(idDetalle);
                    Session["ListadoGlosasCompleto"] = listado;
                }

                if (listado.Count > 0)
                {
                    foreach (var item in listado)
                    {
                        i++;

                        resultado.Append("<tr>");
                        resultado.AppendLine($"<td><input type=\"checkBox\" id=\"ch_{item.id_usuarios}-{item.id_glosa}-{item.id_factura}-{item.id_cargue}\" name=\"ch_{item.id_usuarios}-{item.id_glosa}-{item.id_factura}-{item.id_cargue}\"/></td>");
                        //resultado.AppendFormat("<td class='text-center'><input id='ch_{0}' name='ch_{0}' type='checkbox' value={1} /></td>", i, item.id_glosa);
                        resultado.AppendFormat("<td>{0}<input type='hidden' id='cantidadGlosa_{1}' value='{2}'/><input type='hidden' id='valorGlosa_{1}' value='{3}'/></td>",
                                               item.id_glosa, i, item.cantidad, item.valor_glosado);

                        resultado.AppendFormat("<td>{0}</td>", item.documentoUsuario);
                        resultado.AppendFormat("<td>{0}</td>", item.nombreUsuario);

                        if (tipo == 2 && item.tipo_ingreso == 1)
                        {
                            resultado.AppendFormat("<td style='background-color: #FFFAE0;'>{0}</td>", item.codCups);
                        }
                        else
                        {
                            resultado.AppendFormat("<td>{0}</td>", item.codCups);
                        }

                        resultado.AppendFormat("<td>{0}</td>", item.tipo);
                        resultado.AppendFormat("<td>{0}</td>", item.descripcionGeneral);
                        resultado.AppendFormat("<td>{0}</td>", item.descripcionEspecifico);
                        resultado.AppendFormat("<td>{0}</td>", item.descripcionAplicacion);
                        resultado.AppendLine($"<td>{item.valor_glosado.Value.ToString("C", monedaCol)}</td>");
                        resultado.AppendFormat("<td>{0}</td>", item.cantidad);
                        resultado.AppendLine($"<td>{item.valorTotal.Value.ToString("C", monedaCol)}</td>");

                        string observacion = item.observacion.Length > 100 ? item.observacion.Substring(0, 100) : item.observacion;
                        resultado.AppendFormat("<td><span class='observaciones_{0}'>{1}</span><span class='observaciones_completas_{0}' style='display: none;'>{2}</span>", i, observacion, item.observacion);

                        if (item.observacion.Length > 100)
                        {
                            resultado.AppendFormat("<label class='text-secondary_asalud botonMostrar_{0}' onclick='MostrarTextoCompleto({0})'>Mostrar</label>", i);
                            resultado.AppendFormat("<label class='text-secondary_asalud botonOcultar_{0}' style='display: none;' onclick='OcultarTextoCompleto({0})'>Ocultar</label>", i);
                        }

                        resultado.Append("</td>");
                        resultado.AppendFormat("<td>");
                        resultado.AppendFormat($"<a class='btn btn-xs button_Asalud_Aceptar' onclick='LevantarGlosa({item.id_glosa},{item.id_usuarios})'><i class='glyphicon glyphicon-edit'></i>&nbsp; Levantar</a>", item.id_glosa);
                        resultado.AppendFormat("</td>");
                        resultado.AppendFormat("<td>");
                        resultado.AppendFormat("&nbsp; <a class='btn btn-xs button_Asalud_Rechazar' onclick='DeleteGlosa({0}, {1})'><i class='glyphicon glyphicon-trash'></i>&nbsp; Eliminar</a>", item.id_glosa, item.id_usuarios);
                        resultado.AppendFormat("&nbsp; <a class='btn btn-xs button_Asalud_Guardar' onclick='EditarGlosa({0}, {1}, {2})' data-toggle='modal' data-target='#modalGlosa'><i class='glyphicon glyphicon-edit'></i>&nbsp; Editar</a>", i, item.id_glosa, idUsuario);
                        resultado.AppendFormat("</td>");
                        resultado.Append("</tr>");

                        totalGlosas += item.valorTotal;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return $"{resultado}|{i}|{totalGlosas}";
        }

        public void DescargarReporteCupsGlosas(int? idFactura)
        {
            List<management_fis_facturasCuvGlosas_reporteResult> lista = BusClass.ListaReporteCupsGlosas(idFactura);

            try
            {
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosRips_" + idFactura);

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:N1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:N1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:N1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:N1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:N1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:N1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "item";
                    Sheet.Cells["B1"].Value = "Documento usuario";
                    Sheet.Cells["C1"].Value = "Usuario";
                    Sheet.Cells["D1"].Value = "Código CUPS-CUMS";
                    Sheet.Cells["E1"].Value = "Descripción código CUPS-CUMS";
                    Sheet.Cells["F1"].Value = "Cantidad";
                    Sheet.Cells["G1"].Value = "Valor unitario";
                    Sheet.Cells["H1"].Value = "Valor total";
                    Sheet.Cells["I1"].Value = "Tipo IVA";
                    Sheet.Cells["J1"].Value = "Tiga";
                    Sheet.Cells["K1"].Value = "Descripción Tiga";
                    Sheet.Cells["L1"].Value = "Glosa automática";
                    Sheet.Cells["M1"].Value = "Valor glosa";
                    Sheet.Cells["N1"].Value = "Fecha prestación";

                    int row = 2;

                    foreach (management_fis_facturasCuvGlosas_reporteResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":N" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_registro;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.documentoUsuario;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.nombreUsuario;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.cod_cups;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.descripcion_cuvs;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.conteo_cups;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.valor_individual;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.valor_cups;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.tipo_iva;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.codigo_tiga;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.descripcion_tiga;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.glosa_automatica;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.valor_glosado;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.fecha_prestacion;

                        Sheet.Cells[string.Format("N{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReporteRipsFactura_" + idFactura;
                    Sheet.Cells["A:N"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

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

        public JsonResult BuscarCie10Fis()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    term = term.ToUpper();

                    List<ref_cie10_fis> cie = new List<ref_cie10_fis>();
                    cie = BusClass.listadoCie10FISCodigo(term);

                    var lista = (from ci in cie
                                 select new
                                 {
                                     id = ci.codigo,
                                     label = ci.codigo + "-" + ci.descripcion,
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

        public JsonResult ActualizarDiagnosticos(int? idUsuario, int? idFactura, int? idLote, string dx, string dxDes, string relDx, string relDxDes, string dxA,
            string dxDesA, string relDxA, string relDxDesA)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var actualiza = BusClass.ActualizarDiagnosticosCie10Fis(idUsuario, idFactura, dx, dxDes, relDx, relDxDes, dxA, dxDesA, relDxA, relDxDesA);
                if (actualiza != 0)
                {
                    mensaje = "DIAGNÓSTICOS ACTUALIZADOS CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    throw new Exception("NO SE ACTUALZIARON LOS DIAGNÓSTICOS");
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public PartialViewResult ModalEditarCups(int? idRegistro, int? idFactura, int? idUsuario, int? idTransaccion, int? tipoIngreso)
        {
            fis_rips_cargue_registrosCompletos registro = new fis_rips_cargue_registrosCompletos();
            try
            {
                registro = BusClass.TraerRegistroRipsId(idRegistro);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idRegistro = idRegistro;
            ViewBag.registro = registro;
            ViewBag.idFactura = idFactura;
            ViewBag.idUsuario = idUsuario;
            ViewBag.idTransaccion = idTransaccion;
            ViewBag.tipoIngreso = tipoIngreso;

            return PartialView(registro);
        }

        public PartialViewResult ModalAgregarCups(int? idFactura)
        {

            fis_rips_cargue_registrosCompletos registro = BusClass.TraerRegistroRipsIdFactura(idFactura);
            ViewBag.idFactura = idFactura;
            ViewBag.listaUsuario = BusClass.listadoUsuariosFisIdFactura(idFactura);
            return PartialView(registro);
        }

        public JsonResult GuardarEdicionRips(int? id_registro, string cod_cups, string descripcion_cuvs, int? conteo_cups, Decimal? valor_individual,
            string tiga, string descripcion_tiga, int? idFactura
             , int? idUsuario, int? idTransaccion, DateTime? fecha_prestacion)
        {
            var mensaje = "";
            var rta = 0;

            var gestion = 0;
            try
            {
                fis_rips_cargue_registrosCompletos reg = new fis_rips_cargue_registrosCompletos
                {
                    id_registro = (int)id_registro,
                    cod_cups = cod_cups,
                    descripcion_cuvs = descripcion_cuvs,
                    conteo_cups = conteo_cups,
                    valor_individual = valor_individual,
                    valor_cups = (valor_individual * conteo_cups),
                    codigo_tiga = tiga,
                    descripcion_tiga = descripcion_tiga,
                    fecha_prestacion = fecha_prestacion
                };

                gestion = BusClass.ActualizarRipsFactura(reg);

                if (gestion != 0)
                {
                    log_fis_rips_cargue_registrosCompletos log = new log_fis_rips_cargue_registrosCompletos();
                    log.id_registro = reg.id_registro;
                    log.cod_cups = reg.cod_cups;
                    log.descripcion_cuvs = reg.descripcion_cuvs;
                    log.conteo_cups = reg.conteo_cups;
                    log.valor_individual = reg.valor_individual;
                    log.valor_cups = reg.valor_cups;
                    log.codigo_tiga = reg.codigo_tiga;
                    log.descripcion_tiga = reg.descripcion_tiga;
                    log.usuario_digita = SesionVar.UserName;
                    log.fecha_digita = DateTime.Now;

                    var insertaLog = BusClass.IngresarLogRipsFis(log);

                    var conteo = BusClass.ActualizarCantidadCupsEnGlosa(conteo_cups, id_registro);

                    var listadoCups = BusClass.TraerListadoCupsFacturas(idFactura);
                    Session["ListadoCupsCompleto"] = listadoCups;

                    var listadoGlosas = BusClass.ListaGlosas(idFactura);
                    Session["ListadoGlosasCompleto"] = listadoGlosas;

                    mensaje = "REGISTRO ACTUALIZADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ACTUALIZAR REGISTRO";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL EDITAR EL REGISTRO: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult GuardarNuevoRip(int? id_registro, string cod_cups, string descripcion_cuvs, int? conteo_cups,
            Decimal? valor_individual, string tiga, string descripcion_tiga, string tipo, int? idFactura, int? idBase, string cuv
            , int? idUsuario, int? idTransaccion, string codPrestador, DateTime? fechaPrestacion)
        {
            var mensaje = "";
            var rta = 0;
            var gestion = 0;
            try
            {
                fis_rips_cargue_registrosCompletos reg = new fis_rips_cargue_registrosCompletos
                {
                    id_factura = idFactura,
                    id_usuario = idUsuario,
                    id_transaccion = idTransaccion,
                    id_af = idFactura,
                    id_recep_facturas_cargue_base = idBase,
                    cod_cuv = cuv,
                    cod_cups = cod_cups,
                    descripcion_cuvs = descripcion_cuvs,
                    conteo_cups = conteo_cups,
                    valor_individual = valor_individual,
                    valor_cups = (valor_individual * conteo_cups),
                    codigo_tiga = tiga,
                    descripcion_tiga = descripcion_tiga,
                    tipo = tipo,
                    glosa_automatica = 0,
                    existeGlosaLevantada = 0,
                    codigo_prestador = codPrestador,
                    tipo_iva = "26",
                    iva_recalculado = 0,
                    usuario_digita = SesionVar.UserName,
                    fecha_digita = DateTime.Now,
                    activo = 1,
                    fecha_prestacion = fechaPrestacion
                };

                gestion = BusClass.IngresarNuevoRipsFis(reg);

                if (gestion != 0)
                {
                    var conteo = BusClass.ActualizarCantidadCupsEnGlosa(conteo_cups, id_registro);

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

            return Json(new { mensaje = mensaje, rta = rta, idRegistro = gestion });
        }

        public PartialViewResult MostrarNegociacionesprestador(int? idPrestador)
        {
            List<management_fisPrestadores_contratos_negociaciones_idPrestadorResult> listaContratos = new List<management_fisPrestadores_contratos_negociaciones_idPrestadorResult>();
            ViewBag.idPrestador = idPrestador;

            listaContratos = BusClass.TraerNegociacionesPrestador(idPrestador);

            ViewBag.listadoNegociaciones = listaContratos;
            ViewBag.conteoNegociaciones = listaContratos.Count();

            return PartialView();
        }

        public JsonResult GuardarFacturaFis(Models.FIS.Facturacion_FIS modelo, List<string> listaValores, string listausuarios)
        {
            //tipo ingreso 1 = ingreso analista; tipo ingreso 2 = ingreso auditor

            var mensaje = "";
            var rta = 0;
            fis_rips_facturas factura = new fis_rips_facturas();
            var estadoNuevo = 0;

            try
            {
                factura.id_cargue = modelo.idCargue;
                factura.id_factura = modelo.idDetalle;
                factura.cuv = modelo.cuv;

                factura.codigo_prestador = modelo.codigoPrestador;
                factura.nit_prestador = modelo.nit;
                factura.prestador = modelo.prestador;
                factura.numero_factura = modelo.factura;

                if (modelo.fechafactura != null)
                {
                    factura.fecha_factura = modelo.fechafactura;
                }

                if (modelo.fechainicio.Year > 2000)
                {
                    factura.fecha_inicio_atencion = modelo.fechainicio;
                }

                if (modelo.fechafin.Year > 2000)
                {
                    factura.fecha_fin_atencion = modelo.fechafin;
                }

                factura.localidad_prestacion = modelo.localidad != "undefined" ? modelo.localidad : "";
                factura.FI = (modelo.FI != null && modelo.FI != "" && modelo.FI != "undefined") ? modelo.FI : null;

                factura.valor_total_factura = modelo.valor_factura;
                factura.valor_total_factura_conGlosa = modelo.totalfactura;

                if (modelo.tipoIngreso == 1)
                {
                    factura.numero_contrato = modelo.numcontrato != "undefined" ? modelo.numcontrato : "";
                    factura.estado_contrato = modelo.estado;

                    factura.idContrato = modelo.idContrato;
                    factura.centro_logistico = modelo.centroLogistico;
                    factura.grupo_compras = modelo.grupoCompras;
                    factura.posicion_contrato = modelo.posicionContrato;
                    factura.numero_contrato_operativo = modelo.contratoOperativo;

                    factura.anticipo = modelo.anticipo;
                    factura.iva = modelo.iva;
                    factura.valor_anticipo = modelo.valoranticipo;
                    factura.base_iva_valor = modelo.baseiva;
                    factura.hallazgo_rips = modelo.hallazgo;
                    factura.gasto_integral = null;
                    factura.tiga_integral = null;
                    factura.descripcion_tiga_integral = null;

                    factura.id_departamento = modelo.id_departamento;
                    factura.id_municipio = modelo.id_municipio;

                    estadoNuevo = 4;

                    var mensajeErrado = "";
                    var contenidoMensaje = "";

                    List<management_fis_rips_usuariosSinFechaPrestacionResult> sinFechaPrestacion = BusClass.TraerUsuariosSinFechaPrestacion(modelo.idDetalle);

                    if (sinFechaPrestacion.Count() > 0)
                    {
                        mensajeErrado = "\n No tienen fecha prestación ingresadas los usuarios con identificaciones: ";
                        mensajeErrado += "\n";

                        foreach (var item in sinFechaPrestacion)
                        {
                            contenidoMensaje += contenidoMensaje != "" ? "," + item.numDocumentoIdentificacion : item.numDocumentoIdentificacion;
                        }

                        mensajeErrado += "\n";
                        mensajeErrado += contenidoMensaje;
                        throw new Exception(mensajeErrado);
                    }

                    BusClass.ActualizarVersionFactura(modelo.idDetalle);
                }
                else
                {
                    factura.numero_contrato = modelo.numcontrato != "undefined" ? modelo.numcontrato : "";
                    factura.estado_contrato = modelo.estado != "undefined" ? modelo.estado : "";
                    factura.anticipo = null;
                    factura.iva = null;
                    factura.valor_anticipo = null;
                    factura.base_iva_valor = null;
                    factura.hallazgo_rips = null;
                    factura.gasto_integral = modelo.gasto_integral;
                    factura.tiga_integral = modelo.tiga_integral;
                    factura.pertinencia = modelo.pertinencia;
                    factura.descripcion_tiga_integral = modelo.descripcion_tiga_integral;

                    var mensajeErrado = "";
                    var contenidoMensaje = "";

                    var mensajeErradoTigas = "";
                    var contenidoMensajeTigas = "";

                    if (factura.gasto_integral == 2)
                    {
                        List<management_fis_rips_usuariosSinTigaResult> sinTigas = BusClass.TraerUsuariosSinTiga(modelo.idDetalle);

                        if (sinTigas.Count() > 0)
                        {
                            mensajeErrado = "\n No tienen tigas ingresados los usuarios con identificaciones: ";
                            mensajeErrado += "\n";

                            foreach (var item in sinTigas)
                            {
                                contenidoMensaje += contenidoMensaje != "" ? "," + item.numDocumentoIdentificacion : item.numDocumentoIdentificacion;
                            }

                            mensajeErrado += "\n";
                            mensajeErrado += contenidoMensaje;
                            throw new Exception(mensajeErrado);
                        }
                        //else
                        //{
                        //    List<management_fis_validacionExistentesTigasdetalles_contratoResult> listaTigas = BusClass.ListaTigasSinContrato(modelo.idDetalle);
                        //    if (listaTigas.Count() > 0)
                        //    {
                        //        mensajeErradoTigas = "\n En los siguientes registros no existe el tiga en contrato del prestador: ";
                        //        mensajeErradoTigas += "\n";

                        //        foreach (var item in listaTigas)
                        //        {
                        //            contenidoMensajeTigas += contenidoMensajeTigas != "" ? "," + $"Registro {item.id_registro} - TIGA: {item.codigo_tiga} - {item.descripcion_tiga}" : $"Registro {item.id_registro} - TIGA: {item.codigo_tiga} - {item.descripcion_tiga}";
                        //            contenidoMensajeTigas += "\n";
                        //        }

                        //        mensajeErradoTigas += "\n";
                        //        mensajeErradoTigas += contenidoMensajeTigas;
                        //        throw new Exception(mensajeErradoTigas);
                        //    }
                        //}
                    }
                    //else
                    //{
                    //    var contrato = BusClass.TraerFacturaIdAfAnalista(modelo.idDetalle);
                    //    int? idContrato = contrato.idContrato;
                    //    var tiga = modelo.tiga_integral;

                    //    if (idContrato != null)
                    //    {
                    //        int? existeTigaContrato = BusClass.ExisteTigaContrato(Convert.ToInt32(tiga), idContrato);
                    //        if (existeTigaContrato != 1)
                    //        {
                    //            throw new Exception($"NO EXISTE ESTE TIGA INTEGRAL EN LOS TIGAS DEL CONTRATO NRO {contrato.numero_contrato}");
                    //        }
                    //    }
                    //}

                    if (modelo.glosaCompleta == 1)
                    {
                        factura.glosa_integral = 1;
                        var insertaGlosaMasivo = BusClass.InsertarDatosGlosaCompleta(modelo.idDetalle, modelo.concepto_general, modelo.concepto_especifico, modelo.concepto_aplicacion, modelo.observacionGlosaCompleta, SesionVar.UserName);
                    }

                    var glosa = BusClass.ListadoGlosaSinLevantar(modelo.idDetalle);
                    estadoNuevo = glosa == null ? 6 : 5;
                }

                factura.tipo_ingreso = modelo.tipoIngreso;
                factura.fecha_digita = DateTime.Now;
                factura.usuario_digita = SesionVar.UserName;

                var inserta = BusClass.InsertarFacturaFis(factura);
                if (inserta != 0)
                {
                    log_fis_rips_facturas log = new log_fis_rips_facturas();

                    log.id_fis_factura = inserta;
                    log.id_factura = factura.id_factura;
                    log.id_cargue = factura.id_cargue;
                    log.cuv = factura.cuv;
                    log.codigo_prestador = factura.codigo_prestador;
                    log.nit_prestador = factura.nit_prestador;
                    log.prestador = factura.prestador;
                    log.numero_factura = factura.numero_factura;
                    log.tipo_documento = factura.tipo_documento;
                    log.numero_documento = factura.numero_documento;
                    log.nombre_beneficiario = factura.nombre_beneficiario;
                    log.fecha_factura = factura.fecha_factura;
                    log.fecha_inicio_atencion = factura.fecha_inicio_atencion;
                    log.fecha_fin_atencion = factura.fecha_fin_atencion;
                    log.localidad_prestacion = factura.localidad_prestacion;
                    log.numero_contrato = factura.numero_contrato;
                    log.estado_contrato = factura.estado_contrato;
                    log.anticipo = factura.anticipo;
                    log.iva = factura.iva;
                    log.valor_anticipo = factura.valor_anticipo;
                    log.base_iva_valor = factura.base_iva_valor;
                    log.hallazgo_rips = factura.hallazgo_rips;
                    log.valor_total_factura = factura.valor_total_factura;
                    log.valor_total_factura_conGlosa = factura.valor_total_factura_conGlosa;
                    log.tipo_ingreso = factura.tipo_ingreso;
                    log.cie10 = factura.cie10;
                    log.descripcion_cie10 = factura.descripcion_cie10;
                    log.cie10_relacionado = factura.cie10_relacionado;
                    log.descripcion_cie10_relacionado = factura.descripcion_cie10_relacionado;
                    log.cod_tiga = factura.cod_tiga;
                    log.descripcion_tiga = factura.descripcion_tiga;
                    log.gasto_integral = factura.gasto_integral;
                    log.tiga_integral = factura.tiga_integral;
                    log.descripcion_tiga_integral = factura.descripcion_tiga_integral;
                    log.id_departamento = factura.id_departamento;
                    log.id_municipio = factura.id_municipio;
                    log.FI = factura.FI;
                    log.glosa_integral = factura.glosa_integral;
                    log.fecha_digita = factura.fecha_digita;
                    log.usuario_digita = factura.usuario_digita;

                    var insertarLog = BusClass.InsertarLogFacturaFis(log);

                    string[] listadoUsuarios = listausuarios.Split(',');

                    var actualiza = BusClass.ActualizarEstadoFacturaFis(modelo.idDetalle, estadoNuevo);

                    if (actualiza != 0)
                    {
                        mensaje = "DATOS INGRESADOS CORRECTAMENTE";
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "DATOS INGRESADOS CORRECTAMENTE - SIN ACTUALIZAR ESTADO FACTURA";
                    }
                }
                else
                {
                    mensaje = "ERROR INGRESANDO LOS DATOS";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR INGRESANDO LOS DATOS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta, estado = estadoNuevo });
        }

        public string MostrarAuditor(int? auditor)
        {
            var resultado = "";
            vw_auditores_totales dato = new vw_auditores_totales();
            try
            {
                dato = BusClass.GetAuditorId(auditor);
                if (dato != null)
                {
                    resultado = dato.nombre;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return "Auditor: " + resultado;
        }

        public ActionResult VerDetalleFacturaAuditor(int? a, int? b, int? c)
        {
            //a = id dtll, b = id cargue, c = regional

            managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult datos = new managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult();
            List<management_fis_cargueRips_usuariosResult> usuarios = new List<management_fis_cargueRips_usuariosResult>();

            var cuv = "";
            var valorFactura = new decimal?();
            var valorFacturaConGlosa = new decimal?();
            var listadoUsuarios = "";

            ViewBag.conceptoGeneral = BusClass.ListadoConceptosGeneral();

            try
            {
                var listadoCups = BusClass.TraerListadoCupsFacturas(a);
                Session["ListadoCupsCompleto"] = listadoCups;

                var listadoGlosas = BusClass.ListaGlosas(a);
                Session["ListadoGlosasCompleto"] = listadoGlosas;

                datos = BusClass.ListadoFacturasIdDetalleAuditor((int)a);
                if (datos != null)
                {
                    valorFactura = datos.valor_total_factura;
                    valorFacturaConGlosa = datos.valor_total_factura_conGlosa;
                    cuv = datos.cuv;

                    usuarios = BusClass.ListadoRipsUsuarios(a);
                    if (usuarios != null)
                    {
                        foreach (var use in usuarios)
                        {
                            listadoUsuarios += string.IsNullOrEmpty(listadoUsuarios) ? Convert.ToString(use.id_usuarios) : "," + Convert.ToString(use.id_usuarios);
                        }
                    }

                }
                else
                {
                    datos = new managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.idDetalle = a;
            ViewBag.idCargue = b;
            ViewBag.regional = c;
            ViewBag.valorFactura = valorFactura;
            ViewBag.valorFacturaConGlosa = valorFacturaConGlosa;

            ViewBag.listadoUsuarios = listadoUsuarios;
            ViewBag.ripsUsuarios = usuarios;

            return View(datos);
        }

        public ActionResult VerDetalleFacturaAuditor2(int? a, int? b, int? c)
        {
            //a = id dtll, b = id cargue, c = regional

            managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult datos = new managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult();
            List<management_fis_cargueRips_usuariosResult> usuarios = new List<management_fis_cargueRips_usuariosResult>();

            List<management_fis_facturasCuv_conBeneficiariosResult> listadoCups = new List<management_fis_facturasCuv_conBeneficiariosResult>();
            List<management_fisFacturas_glosaResult> listadoGlosas = new List<management_fisFacturas_glosaResult>();

            var cuv = "";
            var valorFactura = new decimal?();
            var valorFacturaConGlosa = new decimal?();
            var listadoUsuarios = "";

            ViewBag.conceptoGeneral = BusClass.ListadoConceptosGeneral();

            try
            {
                listadoCups = BusClass.TraerListadoCupsFacturasConBenficiarios(a).OrderBy(x => x.tipoDocumentoIdentificacion).ToList();
                listadoGlosas = BusClass.ListaGlosas(a);

                Session["ListadoCupsCompleto"] = listadoCups;
                Session["ListadoGlosasCompleto"] = listadoGlosas;

                datos = BusClass.ListadoFacturasIdDetalleAuditor((int)a);
                if (datos != null)
                {
                    valorFactura = datos.valor_total_factura;
                    valorFacturaConGlosa = datos.valor_total_factura_conGlosa;
                    cuv = datos.cuv;

                    usuarios = BusClass.ListadoRipsUsuarios(a);
                    if (usuarios != null)
                    {
                        foreach (var use in usuarios)
                        {
                            listadoUsuarios += string.IsNullOrEmpty(listadoUsuarios) ? Convert.ToString(use.id_usuarios) : "," + Convert.ToString(use.id_usuarios);
                        }
                    }

                }
                else
                {
                    datos = new managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.idDetalle = a;
            ViewBag.idCargue = b;
            ViewBag.regional = c;
            ViewBag.valorFactura = valorFactura;
            ViewBag.valorFacturaConGlosa = valorFacturaConGlosa;

            ViewBag.listadoUsuarios = listadoUsuarios;
            ViewBag.ripsUsuarios = usuarios;

            ViewBag.listadoCups = listadoCups;
            ViewBag.listadoGlosas = listadoGlosas;

            return View(datos);
        }

        public JsonResult BuscarCIE10()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    term = term.ToUpper();

                    List<Ref_cie10> CIELOS = new List<Ref_cie10>();
                    CIELOS = BusClass.GetCie10Bycodigo(term);

                    var lista = (from reg in CIELOS
                                 select new
                                 {
                                     id = reg.id_cie10,
                                     label = reg.id_cie10 + "-" + reg.des,
                                     des = reg.des,
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

        public JsonResult BuscarTigas()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length > 0)
                {
                    term = term.ToUpper();

                    List<ref_tigas_detallados> tiga = new List<ref_tigas_detallados>();
                    tiga = BusClass.TraerTigasDetalladosCod(term);

                    var lista = (from ti in tiga
                                 select new
                                 {
                                     id = ti.tiga_detallado,
                                     label = ti.tiga_detallado + "-" + ti.descripcion_tiga_detallado
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

        public JsonResult BuscarCUPS()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    term = term.ToUpper();

                    List<fis_rips_cups> cups = new List<fis_rips_cups>();
                    cups = BusClass.TraerListadoCupsCodigo(term);

                    var lista = (from reg in cups
                                 select new
                                 {
                                     id = reg.codigo_cups,
                                     label = reg.codigo_cups + "-" + reg.descripcion,
                                     des = reg.descripcion,
                                 }).Distinct().OrderBy(f => f.label).Take(50);
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

        public ActionResult TableroFacturasEnDevolucion(int? desdeAuditor)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            Session["facturasSeleccionadasEnSubsanacion"] = new List<managmentprestadoresFacturasAuditorEnSubsanacionResult>();

            ViewBag.desdeAuditor = desdeAuditor;

            return View(Model);
        }

        public JsonResult GetAuditoresEnSubsanacion([DataSourceRequest] DataSourceRequest request, int? opc)
        {
            string nomUsuario = SesionVar.UserName;

            ObjectCache cache = MemoryCache.Default;

            List<managmentprestadoresFacturasAuditorEnSubsanacionResult> ListadoFacturasAuditor = cache["facturasAuditorSubsanacion" + nomUsuario] as List<managmentprestadoresFacturasAuditorEnSubsanacionResult>;

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            List<managmentprestadoresFacturasAuditorEnSubsanacionResult> listaok = new List<managmentprestadoresFacturasAuditorEnSubsanacionResult>();

            var idAuditor = SesionVar.IDUser;
            try
            {
                if (ListadoFacturasAuditor == null || ListadoFacturasAuditor.Count() == 0)
                {
                    /*Consulta en base de datos*/
                    listaok = Model.GetFacturasByAuditorEnSubsanacion(idAuditor);

                    /*Se definen las politicas y se insertan en cache los resultados de la consulta hecha a base de datos*/
                    DateTime expirationDate = DateTime.Now.AddHours(10);
                    CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expirationDate };
                    cache.Set("facturasAuditorSubsanacion" + nomUsuario, listaok, policy);
                    cache.Set("tiempoExpiracionMemoriaSubsanacion" + nomUsuario, expirationDate, policy);
                }
                else
                {
                    /*Se setea en el listado de resultados lo que aun hay en cache*/
                    listaok = ListadoFacturasAuditor;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["ListaFacturasSubsanacion"] = listaok.ToList();

            ViewBag.fecha = Convert.ToDateTime(DateTime.Now);

            var lista = new object();

            lista = (from item in listaok
                     select new
                     {
                         id_cargue_dtll = item.id_cargue_dtll,
                         id_cargue_base = item.id_cargue_base,
                         num_factura = item.num_factura,
                         valor_neto = item.valor_neto,
                         nombre_prestador = item.nombre_prestador,
                         dias_trascurridos = item.dias_trascurridos,
                         fecha_ultima_gestion = item.fecha_ultima_gestion.Value.ToString("dd/MM/yyyy"),
                         id_regional = item.id_ref_regional,
                         analista = item.nom_analitica,
                         auditor = item.nom_auditor,

                     }).Distinct().OrderByDescending(f => f.dias_trascurridos);


            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RefrescarTableroFacturasAuditorSubsanacion()
        {
            try
            {
                string nomUsuario = SesionVar.UserName;
                ObjectCache cache = MemoryCache.Default;
                List<managmentprestadoresFacturasAuditorEnSubsanacionResult> ListadoFacturasAuditor = cache["facturasAuditorSubsanacion" + nomUsuario] as List<managmentprestadoresFacturasAuditorEnSubsanacionResult>;

                if (ListadoFacturasAuditor != null)
                {
                    if (ListadoFacturasAuditor.Count > 0)
                    {
                        cache.Remove("facturasAuditorSubsanacion" + nomUsuario);
                        cache.Remove("tiempoExpiracionMemoriaSubsanacion" + nomUsuario);
                    }
                }
                return RedirectToAction("TableroFacturasEnDevolucion", "RadicacionElectonica");
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + error });
            }
        }

        public PartialViewResult GestionesPrestador(int? idDetalle)
        {
            ViewBag.idDetalle = idDetalle;
            return PartialView();
        }

        public ActionResult VerDetalleFacturaAuditorSubsanacion(int? a, int? b, int? c)
        {
            //a = id dtll, b = id cargue, c = regional

            managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult datos = new managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult();
            List<management_fis_facturasCuv_conBeneficiariosResult> listadoCups = new List<management_fis_facturasCuv_conBeneficiariosResult>();

            var cuv = "";
            var valorFactura = new decimal?();
            var valorFacturaConGlosa = new decimal?();
            var listadoUsuarios = "";

            try
            {
                listadoCups = BusClass.TraerListadoCupsFacturasConBenficiarios(a).OrderBy(x => x.tipoDocumentoIdentificacion).ToList();

                datos = BusClass.ListadoFacturasIdDetalleAuditor((int)a);
                if (datos != null)
                {
                    valorFactura = datos.valor_total_factura;
                    valorFacturaConGlosa = datos.valor_total_factura_conGlosa;
                    cuv = datos.cuv;
                }
                else
                {
                    datos = new managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.idDetalle = a;
            ViewBag.idCargue = b;

            ViewBag.regional = c;
            ViewBag.valorFactura = valorFactura;
            ViewBag.valorFacturaConGlosa = valorFacturaConGlosa;
            ViewBag.listadoUsuarios = listadoUsuarios;

            ViewBag.listadoCups = listadoCups;

            return View(datos);
        }

        //public string TraerGlosasaSubsanar(int? idDetalle, int? tipo)
        //{
        //    // tipo 1 = analistas, tipo 2 = auditores

        //    StringBuilder resultado = new StringBuilder();
        //    int i = 0;
        //    int? conteoActivas = 0;

        //    try
        //    {
        //        List<management_fisFacturas_glosaResult> listado = BusClass.ListaGlosas(idDetalle);

        //        if (listado.Count > 0)
        //        {
        //            foreach (var item in listado)
        //            {
        //                i++;

        //                resultado.Append("<tr>");

        //                if (item.estado == 3)
        //                {
        //                    resultado.AppendFormat("<td class='text-center'></td>");
        //                }
        //                else
        //                {
        //                    conteoActivas++;
        //                    resultado.AppendFormat("<td class='text-center'><input id='ch_{0}' name='ch_{0}' type='checkbox' value={1} /></td>", i, item.id_glosa);
        //                }



        //                resultado.AppendFormat("<td>{0}<input type='hidden' id='cantidadGlosa_{1}' value='{2}'/><input type='hidden' id='valorGlosa_{1}' value='{3}'/></td>",
        //                                       item.id_glosa, i, item.cantidad, item.valor_glosado);

        //                if (tipo == 2 && item.tipo_ingreso == 1)
        //                {
        //                    resultado.AppendFormat("<td style='background-color: #FFFAE0;'>{0}</td>", item.codCups);
        //                }
        //                else
        //                {
        //                    resultado.AppendFormat("<td>{0}</td>", item.codCups);
        //                }

        //                resultado.AppendFormat("<td>{0}</td>", item.tipo);

        //                string observacion = item.observacion.Length > 100 ? item.observacion.Substring(0, 100) : item.observacion;
        //                resultado.AppendFormat("<td><span class='observaciones_{0}'>{1}</span><span class='observaciones_completas_{0}' style='display: none;'>{2}</span>", i, observacion, item.observacion);

        //                if (item.observacion.Length > 100)
        //                {
        //                    resultado.AppendFormat("<label class='text-secondary_asalud botonMostrar_{0}' onclick='MostrarTextoCompleto({0})'>Mostrar</label>", i);
        //                    resultado.AppendFormat("<label class='text-secondary_asalud botonOcultar_{0}' style='display: none;' onclick='OcultarTextoCompleto({0})'>Ocultar</label>", i);
        //                }

        //                resultado.Append("</td>");
        //                resultado.AppendFormat("<td>{0}</td>", item.valor_glosado);

        //                if (item.estado != 3)
        //                {
        //                    resultado.AppendFormat("<td><a class='btn btn-xs button_Asalud_Aceptar' onclick='LevantarGlosa({0})'><i class='glyphicon glyphicon-edit'></i>&nbsp; Levantar</a></td>", item.id_glosa);
        //                    resultado.AppendFormat("<td><a class='btn btn-xs button_Asalud_Aceptar' onclick='MantenerGlosa({0})'><i class='glyphicon glyphicon-edit'></i>&nbsp; Mantener</a></td>", item.id_glosa);
        //                }
        //                else
        //                {
        //                    resultado.AppendFormat("<td></td>");
        //                    resultado.AppendFormat("<td></td>");
        //                }
        //                resultado.Append("</tr>");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return $"{resultado}|{i}|{conteoActivas}";
        //}

        public string TraerGlosaSubsanadaPrestador(int? idDetalle, int? tipo)
        {
            // tipo 1 = analistas, tipo 2 = auditores

            StringBuilder resultado = new StringBuilder();
            int i = 0;

            var totalGlosas = new decimal?(0);
            CultureInfo monedaCol = new CultureInfo("es-CO");

            int? conteoActivas = 0;

            try
            {
                List<management_fisGlosasSubsanadas_prestadorResult> listado = BusClass.ListaGlosasSubsanadasPrestador(idDetalle);

                if (listado.Count > 0)
                {
                    foreach (var item in listado)
                    {
                        i++;

                        resultado.Append("<tr>");
                        if (item.estado == 3)
                        {
                            resultado.AppendFormat("<td class='text-center'></td>");
                        }
                        else
                        {
                            resultado.AppendFormat("<td class='text-center'><input id='chS_{0}' name='chS_{0}' type='checkbox' value={1} /><input id='idRegistro_{0}' name='idRegistro_{0}' type='hidden' value={2} /></td>", i, item.id_glosa, item.id_registro);
                        }

                        resultado.AppendFormat("<td>{0}<input type='hidden' id='cantidadGlosaSubsanada_{1}' value='{2}'/><input type='hidden' id='valorGlosaSubsanada_{1}' value='{3}'/></td>",
                                               item.id_glosa, i, item.cantidad, item.valor_subsanado);

                        resultado.AppendFormat("<td>{0}</td>", item.nombreUsuario);
                        resultado.AppendFormat("<td>{0}</td>", item.documentoUsuario);

                        if (tipo == 2 && item.tipo_ingreso == 1)
                        {
                            resultado.AppendFormat("<td style='background-color: #FFFAE0;'>{0}</td>", item.codCups);
                        }
                        else
                        {
                            resultado.AppendFormat("<td>{0}</td>", item.codCups);
                        }

                        string observacion = item.observacion.Length > 100 ? item.observacion.Substring(0, 100) : item.observacion;
                        resultado.AppendFormat("<td><span class='observaciones_{0}'>{1}</span><span class='observaciones_completas_{0}' style='display: none;'>{2}</span>", i, observacion, item.observacion);

                        if (item.observacion.Length > 100)
                        {
                            resultado.AppendFormat("<label class='text-secondary_asalud botonMostrar_{0}' onclick='MostrarTextoCompleto({0})'>Mostrar</label>", i);
                            resultado.AppendFormat("<label class='text-secondary_asalud botonOcultar_{0}' style='display: none;' onclick='OcultarTextoCompleto({0})'>Ocultar</label>", i);
                        }
                        resultado.Append("</td>");

                        string observacionSubsanacion = item.observacionSubsanacion.Length > 100 ? item.observacionSubsanacion.Substring(0, 100) : item.observacionSubsanacion;
                        resultado.AppendFormat("<td><span class='observacionesSubsanacion_{0}'>{1}</span><span class='observaciones_completasSubsanacion_{0}' style='display: none;'>{2}</span>", i, observacionSubsanacion, item.observacionSubsanacion);

                        if (item.observacionSubsanacion.Length > 100)
                        {
                            resultado.AppendFormat("<label class='text-secondary_asalud botonMostrarSubsanacion_{0}' onclick='MostrarTextoCompletoSubsanacion({0})'>Mostrar</label>", i);
                            resultado.AppendFormat("<label class='text-secondary_asalud botonOcultarSubsanacion_{0}' style='display: none;' onclick='OcultarTextoCompletoSubsanacion({0})'>Ocultar</label>", i);
                        }

                        resultado.Append("</td>");
                        resultado.AppendFormat("<td>{0}</td>", item.subsanadaONo);
                        resultado.AppendFormat("<td>{0}</td>", item.nota_credito);
                        resultado.AppendFormat("<td>{0}</td>", item.valor_nota_credito);

                        resultado.AppendFormat("<td>{0}</td>", item.cantidad);
                        resultado.AppendLine($"<td>{item.valor_glosado.ToString("C", monedaCol)}</td>");

                        resultado.AppendFormat("<td>{0}</td>", item.cantidadSubsanada);
                        //resultado.AppendLine($"<td>{item.valor_subsanado.Value.ToString("C", monedaCol)}</td>");

                        if (item.valor_subsanado != null)
                        {
                            resultado.AppendLine($"<td>{item.valor_subsanado.Value.ToString("C", monedaCol)}</td>");
                        }
                        else
                        {
                            resultado.AppendLine($"<td>{item.valor_subsanado}</td>");
                        }

                        if (item.estado == 4)
                        {
                            resultado.AppendFormat("<td><a class='btn btn-xs button_Asalud_Aceptar' onclick='MantenerGlosa({0}, {1})'><i class='glyphicon glyphicon-edit'></i>&nbsp; Mantener</a></td>", item.id_glosa, item.id_registro);
                            resultado.AppendFormat("<td><a class='btn btn-xs button_Asalud_Aceptar' onclick='FinalizarGlosa({0}, {1})'><i class='glyphicon glyphicon-ok'></i>&nbsp; Finalizar</a></td>", item.id_glosa, item.id_registro);
                            conteoActivas++;
                        }
                        else
                        {
                            resultado.AppendFormat("<td></td>");
                            resultado.AppendFormat("<td></td>");
                        }

                        resultado.Append("</tr>");

                        totalGlosas += item.valor_subsanado;

                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return $"{resultado}|{i}|{totalGlosas}|{conteoActivas}";
        }

        public JsonResult MantenerGlosaMasivo(string glosas, string registros, string observacionMantenida)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                string[] idGlosas = glosas.Split(',');
                string[] idRegistros = registros.Split(',');

                int conteo = 0;

                foreach (var item in idGlosas)
                {
                    var idGlosa = Convert.ToInt32(item);
                    int? idRegistro = Convert.ToInt32(idRegistros[conteo]);

                    var actualiza = BusClass.MantenerGlosa(idGlosa, observacionMantenida);
                    if (actualiza != 0)
                    {
                        var actualizaGlosa = BusClass.ActualizarEstadoGlosa_prestadores(idRegistro, 1);
                        log_fisFactura_mantenerGlosa glosa = new log_fisFactura_mantenerGlosa();
                        glosa.id_glosa = idGlosa;
                        glosa.fecha_digita = DateTime.Now;
                        glosa.usuario_digita = SesionVar.UserName;

                        var insertarLog = BusClass.InsertarLogGlosaFacturasFisMantener(glosa);
                        mensaje = "GLOSA MANTENIDA CORRECTAMENTE";
                    }
                    else
                    {
                        throw new Exception($"ERROR AL MANTENER LA GLOSA {idGlosa}");
                    }

                    conteo++;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult MantenerGlosa(int? idGlosa, int? idRegistro, string observacionMantenida)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var actualiza = BusClass.MantenerGlosa(idGlosa, observacionMantenida);
                if (actualiza != 0)
                {
                    var actualizaGlosa = BusClass.ActualizarEstadoGlosa_prestadores(idRegistro, 1);
                    log_fisFactura_mantenerGlosa glosa = new log_fisFactura_mantenerGlosa();
                    glosa.id_glosa = idGlosa;
                    glosa.fecha_digita = DateTime.Now;
                    glosa.usuario_digita = SesionVar.UserName;

                    var insertarLog = BusClass.InsertarLogGlosaFacturasFisMantener(glosa);

                    mensaje = "GLOSA MANTENIDA CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR AL MANTENER LA GLOSA";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL MANTENER LA GLOSA: " + error;

            }
            return Json(new { mensaje = mensaje, rta = rta });
        }


        public JsonResult LevantarGlosa(int? idGlosa, int? idFactura)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var actualiza = BusClass.Levantarglosa(idGlosa);
                if (actualiza != 0)
                {
                    log_fisFactura_levantarGlosa glosa = new log_fisFactura_levantarGlosa();
                    glosa.id_glosa = idGlosa;
                    glosa.fecha_digita = DateTime.Now;
                    glosa.usuario_digita = SesionVar.UserName;

                    var insertarLog = BusClass.InsertarLogGlosaFacturasFis(glosa);

                    mensaje = "GLOSA LEVANTADA CORRECTAMENTE";

                    var listadoCups = BusClass.TraerListadoCupsFacturas(idFactura);
                    Session["ListadoCupsCompleto"] = listadoCups;

                    var listadoGlosas = BusClass.ListaGlosas(idFactura);
                    Session["ListadoGlosasCompleto"] = listadoGlosas;
                }
                else
                {
                    mensaje = "ERROR AL LEVANTAR LA GLOSA";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL LEVANTAR LA GLOSA: " + error;

            }
            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult LevantarGlosaMasivos(string seleccionados, int? idFac)
        {
            string mensaje = "";
            int rta = 0;
            try
            {
                if (!string.IsNullOrEmpty(seleccionados))
                {
                    string[] completos = seleccionados.Split('|');
                    foreach (var item in completos)
                    {
                        string[] partidos = item.Split('-');

                        int idUsuario = Convert.ToInt32(partidos[0]);
                        int idGlosa = Convert.ToInt32(partidos[1]);
                        int idFactura = Convert.ToInt32(partidos[2]);

                        var actualiza = BusClass.Levantarglosa(idGlosa);
                        if (actualiza != 0)
                        {
                            rta = 1;
                            mensaje = "GLOSA(S) LEVANTADA(S) CORRECTAMENTE";

                            log_fisFactura_levantarGlosa glosa = new log_fisFactura_levantarGlosa();
                            glosa.id_glosa = idGlosa;
                            glosa.fecha_digita = DateTime.Now;
                            glosa.usuario_digita = SesionVar.UserName;
                            var insertarLog = BusClass.InsertarLogGlosaFacturasFis(glosa);
                        }
                        else
                        {
                            mensaje = "ERROR AL LEVANTAR LA GLOSA NRO: " + idGlosa;
                            rta = 0;
                            throw new Exception(mensaje);
                        }
                    }


                }
                else
                {
                    mensaje = "NO HAY GLOSAS SELECCIONADAS";
                    throw new Exception(mensaje);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL LEVANTAR LA GLOSA: " + error;
            }


            var listadoCups = BusClass.TraerListadoCupsFacturas(idFac);
            Session["ListadoCupsCompleto"] = listadoCups;

            var listadoGlosas = BusClass.ListaGlosas(idFac);
            Session["ListadoGlosasCompleto"] = listadoGlosas;

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult FinalizarGlosaMasivos(List<int> totalGlosas, List<int> totalRegistros)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                if (totalGlosas != null)
                {
                    var conteo = 0;

                    foreach (var item in totalGlosas)
                    {
                        var idGlosa = Convert.ToInt32(item);

                        var actualiza = BusClass.FinalizarGlosa(totalGlosas[conteo]);
                        var actualizaGlosa = BusClass.ActualizarEstadoGlosa_prestadores(totalRegistros[conteo], 5);

                        //var actualiza = BusClass.FinalizarGlosa(idGlosa);
                        if (actualizaGlosa != 0)
                        {
                            rta = 1;
                            mensaje = "GLOSA(S) FINALIZADA(S) CORRECTAMENTE";
                        }
                        else
                        {
                            mensaje = "ERROR AL FINALIZADAR LA GLOSA NRO: " + idGlosa;
                            rta = 0;
                            throw new Exception(mensaje);
                        }
                        conteo++;
                    }
                }
                else
                {
                    mensaje = "NO HAY GLOSAS SELECCIONADAS";
                    throw new Exception(mensaje);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL LEVANTAR LA GLOSA: " + error;

            }
            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult FinalizarGlosa(int? idGlosa, int? idRegistro)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var actualiza = BusClass.FinalizarGlosa(idGlosa);

                if (actualiza != 0)
                {
                    var actualizaGlosa = BusClass.ActualizarEstadoGlosa_prestadores(idRegistro, 5);

                    mensaje = "GLOSA FINALIZADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL FINALIZAR LA GLOSA";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL FINALIZAR LA GLOSA: " + error;

            }
            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult DevolverFacturaAPrestador(int? idDetalle)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var actualiza = BusClass.ActualizarEstadoFacturaFis(idDetalle, 5);
                if (actualiza != 0)
                {
                    mensaje = "FACTURA DEVUELTA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN LA DEVOLUCIÓN DE LA FACTURA";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA DEVOLUCIÓN DE LA FACTURA: " + error;

            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult AprobarFacturaSubsanada(int? idDetalle)
        {
            var mensaje = "";
            var rta = 0;
            List<fis_rips_facturas_glosa> listaMantenidas = new List<fis_rips_facturas_glosa>();
            List<management_fis_glosaTieneNotaCreditoResult> listaConNC = new List<management_fis_glosaTieneNotaCreditoResult>();
            int? actualiza = 0;
            try
            {
                listaMantenidas = BusClass.TraerGlosasMantenidas(idDetalle);
                listaConNC = BusClass.TraerGlosasConNoptaCredito(idDetalle);

                if (listaMantenidas.Count() > 0)
                {
                    actualiza = BusClass.ActualizarEstadoFacturaFis(idDetalle, 5);
                    mensaje = actualiza != 0
                      ? "LA FACTURA SE DEVOLVIÓ AL PRESTADOR CORRECTAMENTE"
                      : "ERROR EN LA GESTIÓN DE LA FACTURA";
                }
                else if (listaConNC.Count() > 0)
                {
                    actualiza = BusClass.ActualizarEstadoFacturaFis(idDetalle, 18);
                    mensaje = actualiza != 0
                      ? "LA FACTURA SE APROBÓ CON NOTA CREDITO CORRECTAMENTE"
                      : "ERROR EN LA GESTIÓN DE LA FACTURA";
                }
                else
                {
                    actualiza = BusClass.ActualizarEstadoFacturaFis(idDetalle, 6);
                    mensaje = actualiza != 0
                      ? "LA FACTURA SE APROBÓ CORRECTAMENTE"
                      : "ERROR EN LA GESTIÓN DE LA FACTURA";
                }

                if (actualiza != 0)
                {
                    rta = 1;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA GESTIÓN DE LA FACTURA: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult EliminarGlosaFactura(int? idGlo, int? idFactura)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var elimina = BusClass.EliminarGlosaFactura(idGlo);
                if (elimina != 0)
                {
                    mensaje = "GLOSA ELIMINADA CORRECTAMENTE";
                    rta = 1;

                    var listadoCups = BusClass.TraerListadoCupsFacturas(idFactura);
                    Session["ListadoCupsCompleto"] = listadoCups;

                    var listadoGlosas = BusClass.ListaGlosas(idFactura);
                    Session["ListadoGlosasCompleto"] = listadoGlosas;

                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR LA GLOSA";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR LA GLOSA: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult EliminarRegistroRips(int? idRegistro, int? idFactura)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var elimina = BusClass.EliminarRegistroRipsFis(idRegistro);
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

        public ActionResult TableroFacturasTraza(int? idDetalle, DateTime? fechainicial, DateTime? fechaFinal, int? estado, int? regional, string prestador, string nit, string numFac)
        {
            List<management_prestadoresFacturasGestionadas_fisResult> listado = new List<management_prestadoresFacturasGestionadas_fisResult>();

            try
            {
                if (idDetalle.HasValue || fechainicial.HasValue || fechaFinal.HasValue || estado.HasValue || regional.HasValue || !string.IsNullOrEmpty(prestador) || !string.IsNullOrEmpty(nit) ||
                !string.IsNullOrEmpty(numFac))
                {
                    listado = BusClass.TraerListadoFacturas();

                    if (idDetalle != 0 && idDetalle != null)
                    {
                        listado = listado.Where(x => x.id_af == idDetalle).ToList();
                    }

                    if (fechainicial != null)
                    {
                        listado = listado.Where(x => x.fecha_recepcion_fac >= fechainicial).ToList();
                    }

                    if (fechaFinal != null)
                    {
                        listado = listado.Where(x => x.fecha_recepcion_fac <= fechaFinal).ToList();
                    }

                    if (estado != null)
                    {
                        listado = listado.Where(x => x.estado_factura == estado).ToList();
                    }

                    if (regional != null)
                    {
                        listado = listado.Where(x => x.regional == regional).ToList();
                    }

                    if (regional != null)
                    {
                        listado = listado.Where(x => x.regional == regional).ToList();
                    }

                    if (!string.IsNullOrEmpty(prestador))
                    {
                        listado = listado.Where(x => x.razon_social == prestador).ToList();
                    }

                    if (!string.IsNullOrEmpty(nit))
                    {
                        listado = listado.Where(x => x.nit == nit).ToList();
                    }

                    if (!string.IsNullOrEmpty(nit))
                    {
                        listado = listado.Where(x => x.num_factura == numFac).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.ROL = SesionVar.ROL;
            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.ref_estado = BusClass.GetEstadoFacturas();

            ViewBag.listado = listado;
            ViewBag.conteo = listado.Count();

            Session["ListadoFacturas"] = listado;

            return View();
        }

        public void ExportarFacturasMasivamenteFis()
        {
            try
            {
                List<management_prestadoresFacturasGestionadas_fisResult> ListaSession = (List<management_prestadoresFacturasGestionadas_fisResult>)Session["ListadoFacturas"];
                List<management_prestadoresFacturasGestionadas_fisResult> result2 = new List<management_prestadoresFacturasGestionadas_fisResult>();


                if (ListaSession.Count != 0)
                {
                    result2 = ListaSession.ToList();
                }

                result2 = result2.Where(x => x.estado_factura != 0).ToList();
                if (result2.Count == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('No se han encontrado resultados');" +
                    "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {

                    using (ZipFile zip = new ZipFile())
                    {
                        int count = 0;
                        foreach (var item in result2)
                        {
                            try
                            {
                                WebClient User = new WebClient();
                                string filename = item.ruta;
                                Byte[] FileBuffer = User.DownloadData(filename);
                                //Binary binary2 = item.ruta;
                                byte[] array = FileBuffer.ToArray();
                                zip.AddEntry(item.num_factura + ".pdf", array);
                            }
                            catch (Exception ex)
                            {

                            }

                            count++;
                        }

                        using (MemoryTributary salida = new MemoryTributary())
                        {
                            zip.Save(salida);

                            Response.Clear();
                            Response.BufferOutput = false;
                            Response.ContentType = "application/zip";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=Facturas" + ".zip");
                            Response.BinaryWrite(salida.ToArray());
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void ExportarDatosFacturas(int? tipo)
        {
            List<management_prestadoresFacturasGestionadas_fisResult> Lista = (List<management_prestadoresFacturasGestionadas_fisResult>)Session["ListadoFacturas"];
            try
            {
                ExcelPackage Ep = new ExcelPackage();

                if (Lista == null || Lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('ERROR EN LA DESCARGA');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ListadoFacturas");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:X1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:X1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:X1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:X1"].Style.Font.Size = 10;
                    //Sheet.Cells["A1:T1"].Style.WrapText = true;
                    Sheet.Cells["A1:X1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:X1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:X1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id factura";
                    Sheet.Cells["B1"].Value = "Fecha factura";
                    Sheet.Cells["C1"].Value = "Regional";
                    Sheet.Cells["D1"].Value = "UNIS";
                    Sheet.Cells["E1"].Value = "Ciudad";
                    Sheet.Cells["F1"].Value = "Numero de factura";
                    Sheet.Cells["G1"].Value = "Valor factura";
                    Sheet.Cells["H1"].Value = "Contrato";
                    Sheet.Cells["I1"].Value = "Nit prestador";
                    Sheet.Cells["J1"].Value = "SAP prestador";
                    Sheet.Cells["K1"].Value = "Nombre prestador";
                    Sheet.Cells["L1"].Value = "Fecha de recepción";
                    Sheet.Cells["M1"].Value = "Fecha de asignación analista";
                    Sheet.Cells["N1"].Value = "Fecha de auditoria analista";
                    Sheet.Cells["O1"].Value = "Nombre analista";
                    Sheet.Cells["P1"].Value = "Fecha de asignación auditor";
                    Sheet.Cells["Q1"].Value = "Fecha de auditoria auditor";
                    Sheet.Cells["R1"].Value = "Fecha de aprobación auditor";
                    Sheet.Cells["S1"].Value = "Nombre auditor";
                    Sheet.Cells["T1"].Value = "Tigas factura";
                    Sheet.Cells["U1"].Value = "Valor glosa total";
                    Sheet.Cells["V1"].Value = "Valor aprobado";
                    Sheet.Cells["W1"].Value = "Documento contable";
                    Sheet.Cells["X1"].Value = "Fecha de contabilización";


                    int row = 2;

                    foreach (management_prestadoresFacturasGestionadas_fisResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":W" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_af;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.fecha_factura;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.nombre_regional;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.nom_ciudad;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.nom_departamento;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.num_factura;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.valor_neto;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.num_contrato;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.nit;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.codigo_SAP;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.razon_social;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.fecha_recepcion_fac;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.fecha_asignacion_analista;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.fecha_auditoria_analista;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.nombreAnalistaAsignado;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.fecha_asignacion_auditor;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.fecha_auditoria_auditor;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.fecha_aprobacion_auditor;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.nombreAuditorAsignado;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.tigas_factura;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.valor_total_glosas;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.valor_aprobado;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.documento_contable;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.fecha_contabilizacion;

                        Sheet.Cells[string.Format("B{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("L{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("M{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("N{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("O{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("P{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("X{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    Sheet.Cells["A:w"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ListadoFacturas_" + DateTime.Now + ".xlsx");

                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
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

        public ActionResult TableroEdicionTigas()
        {
            ViewBag.regional = BusClass.GetRefRegion();
            return View();
        }

        public void DescargarReporteTigas(int? idAf, DateTime? fechaIni, DateTime? fechaFin, string codPrestador, string numFactura, string regional)
        {
            List<management_fis_rips_ParaActualizar_tigasResult> listado = new List<management_fis_rips_ParaActualizar_tigasResult>();

            try
            {
                ExcelPackage Ep = new ExcelPackage();

                listado = BusClass.TraerListadoCUPSActualziarTigas(idAf, fechaIni, fechaFin, codPrestador, numFactura, regional);

                if (listado == null || listado.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('SIN DATOS PARA MOSTRAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ListadoTigasActualizar");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:AG1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:AG1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:AG1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:AG1"].Style.Font.Size = 10;
                    //Sheet.Cells["A1:T1"].Style.WrapText = true;   
                    Sheet.Cells["A1:AG1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:AG1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:AG1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id Registro";
                    Sheet.Cells["B1"].Value = "Id Factura FIS";
                    Sheet.Cells["C1"].Value = "Id Factura";
                    Sheet.Cells["D1"].Value = "REGIONAL";
                    Sheet.Cells["E1"].Value = "NIT";
                    Sheet.Cells["F1"].Value = "COD SAP";
                    Sheet.Cells["G1"].Value = "Nombre prestador";
                    Sheet.Cells["H1"].Value = "Contrato";

                    Sheet.Cells["I1"].Value = "Numero de Factura";
                    Sheet.Cells["J1"].Value = "Fecha de Recepción";
                    Sheet.Cells["K1"].Value = "Código Prestador";
                    Sheet.Cells["L1"].Value = "Id Transacción";
                    Sheet.Cells["M1"].Value = "Id Usuario";
                    Sheet.Cells["N1"].Value = "Num Documento Identificación";
                    Sheet.Cells["O1"].Value = "Nombre Usuario";

                    Sheet.Cells["P1"].Value = "Cuenta mayor";
                    Sheet.Cells["Q1"].Value = "Ceco";
                    Sheet.Cells["R1"].Value = "Código CUPS";
                    Sheet.Cells["S1"].Value = "Descripción CUPS";
                    Sheet.Cells["T1"].Value = "Conteo CUPS";
                    Sheet.Cells["U1"].Value = "Valor CUPS";
                    Sheet.Cells["V1"].Value = "Valor Individual";

                    Sheet.Cells["W1"].Value = "Código CUV";
                    Sheet.Cells["X1"].Value = "TIGA detalles";
                    Sheet.Cells["Y1"].Value = "Descripción TIGA detalles";
                    Sheet.Cells["Z1"].Value = "TIGA integral";
                    Sheet.Cells["AA1"].Value = "Descripcion TIGA integral";

                    Sheet.Cells["AB1"].Value = "TIGA SUGERIDO";
                    Sheet.Cells["AC1"].Value = "DESCRIPCION TIGA SUGERIDO";

                    Sheet.Cells["AD1"].Value = "TIGA A CORREGIR";
                    Sheet.Cells["AE1"].Value = "ANALISTA NOMBRE";
                    Sheet.Cells["AF1"].Value = "AUDITOR NOMBRE";
                    Sheet.Cells["AG1"].Value = "TIGA INTEGRAL";

                    int row = 2;

                    foreach (management_fis_rips_ParaActualizar_tigasResult item in listado)
                    {
                        Sheet.Cells["A" + row + ":AD" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_registro;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.idFacturaFis;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.id_factura;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.nom_regional;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.nit;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.codigo_SAP;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.nombre_prestador;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.num_contrato;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.num_factura;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_recepcion_fac;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.codigo_prestador;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.id_transaccion;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.id_usuario;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.numDocumentoIdentificacion;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.nombreUsuario;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.cuenta_mayor;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.ceco;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.cod_cups;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.descripcion_cuvs;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.conteo_cups;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.valor_cups;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.valor_individual;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.cod_cuv;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.tigaDetalle;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.descripcionTigaDetalle;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.tigaIntegral;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.descripcionTigaIntegral;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.tiga_sugerido;
                        Sheet.Cells[string.Format("AC{0}", row)].Value = item.descripciontiga_sugerido;
                        Sheet.Cells[string.Format("AD{0}", row)].Value = "";
                        Sheet.Cells[string.Format("AE{0}", row)].Value = item.nombreAnalista;
                        Sheet.Cells[string.Format("AF{0}", row)].Value = item.nombreAuditor;
                        Sheet.Cells[string.Format("AG{0}", row)].Value = "NO";

                        // Formatear fechas si es necesario

                        Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = "DD/MM/YYYY";


                    Sheet.Cells["A:AG"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ListadoRipsCUPSTigas_" + DateTime.Now + ".xlsx");

                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
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

        public JsonResult ActualizarTigas(HttpPostedFileBase archivo)
        {
            var rta = 0;
            var mensaje = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

            try
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

                List<management_fis_rips_ParaActualizar_tigasResult> listado = new List<management_fis_rips_ParaActualizar_tigasResult>();
                listado = Model.ValidacionDatosCUPSTigas(dataTable, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                {
                    throw new Exception(MsgRes.DescriptionResponse);
                }

                else
                {
                    foreach (var item in listado)
                    {
                        var gestion = 0;

                        fis_rips_cargue_registrosCompletos reg = new fis_rips_cargue_registrosCompletos
                        {
                            id_registro = item.id_registro,
                            cod_cups = item.cod_cups,
                            descripcion_cuvs = item.descripcion_cuvs,
                            conteo_cups = item.conteo_cups,
                            valor_individual = item.valor_individual,
                            valor_cups = item.valor_cups,
                            codigo_tiga = item.codigo_tiga,
                            descripcion_tiga = item.descripcion_tiga
                        };

                        if (item.actualizaTigaintegral.ToUpper() == "SI")
                        {
                            gestion = BusClass.ActualizarTigaInteralFisFactura(Convert.ToInt32(item.codigo_tiga), item.id_factura);
                        }
                        else
                        {
                            gestion = BusClass.ActualizarRipsFacturaTiga(reg);
                        }

                        if (gestion != 0)
                        {
                            log_fis_rips_cargue_registrosCompletos log = new log_fis_rips_cargue_registrosCompletos();
                            log.id_registro = reg.id_registro;
                            log.cod_cups = reg.cod_cups;
                            log.descripcion_cuvs = reg.descripcion_cuvs;
                            log.conteo_cups = reg.conteo_cups;
                            log.valor_individual = reg.valor_individual;
                            log.valor_cups = reg.valor_cups;
                            log.codigo_tiga = reg.codigo_tiga;
                            log.integral = item.actualizaTigaintegral;
                            log.descripcion_tiga = reg.descripcion_tiga;

                            var insertaLog = BusClass.IngresarLogRipsFis(log);
                        }
                        else
                        {
                            throw new Exception("No se puede actualizar el registro nro: " + item.id_registro);
                        }
                    }

                    mensaje = "DATOS ACTUALIZADOS CORRECTAMENTE";
                    rta = 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ACTUALIZAR DATOS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult CargarMasivoDetalles()
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                List<management_fis_cargarMasivoDetalles_idsFacturasResult> listado = BusClass.listadoFacturasCargueMasivo();
                if (listado.Count() > 0)
                {
                    int conteo = 0;
                    foreach (var item in listado)
                    {
                        conteo++;
                        var existenDatos = ExistenMasivosCargadosIdDetalle(Convert.ToInt32(item.id_factura));
                        if (existenDatos == 0)
                        {
                            throw new Exception("NO EXISTEN DATOS PARA LA FACTURA CON ID DETALLE: " + item);
                        }
                        else
                        {
                            var cargarInformacion = SubirDatosSegunIdMasivo(Convert.ToInt32(item.id_factura), item.usuario);

                            if (!cargarInformacion.Contains("ERROR"))
                            {
                                mensaje = "INFORMACIÓN VALIDADA CORRECTAMENTE";
                                rta = 1;
                            }
                            else
                            {
                                throw new Exception(cargarInformacion);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = error;
            }
            return Json(new { mensaje = mensaje, rta = rta });
        }

        public string SubirDatosSegunIdMasivo(int? idDetalle, string usuarioAnalista)
        {
            var resultado = "";
            var conteoServicios = 0;

            try
            {
                var buscarCuv = BusClass.FisBuscarCuv(idDetalle);
                var existenDetalles = BusClass.HayDetallesIdFactura(idDetalle);

                if (existenDetalles != 1)
                {
                    throw new Exception("NO EXISTEN DATOS DE FACTURA CON ESTE ID");
                }
                else
                {
                    var EliminaDetalles = BusClass.EliminarDetallesIdFactura(idDetalle);
                }

                var listadoFacturas = BusClass.FisBuscarFacturas(idDetalle);
                var listadoUsuariosInicial = BusClass.FisBuscarFacturasUsuarios(idDetalle, "");
                var serviciosInicial = BusClass.FisBuscarFacturasServicios(idDetalle, "");

                // Inserción del lote
                var idLote = InsertarLote(buscarCuv, idDetalle);

                if (idLote != 0 && listadoFacturas.Count() > 0)
                {
                    foreach (var factura in listadoFacturas)
                    {
                        // Inserción de transacción
                        var idTransaccion = InsertarTransaccion(factura, idLote, idDetalle);

                        if (idTransaccion != 0)
                        {
                            // Filtrar los usuarios correspondientes a la factura actual
                            var listadoUsuarios = listadoUsuariosInicial.Where(x => x.numero_factura == factura.numero_factura
                            && x.id_detalle == idDetalle && x.codigo_prestador == factura.codigo_prestador).ToList();

                            if (listadoUsuarios.Count() > 0)
                            {
                                foreach (var usuario in listadoUsuarios)
                                {
                                    // Inserción de usuario
                                    var idUsuario = InsertarUsuario(usuario, idLote, idTransaccion, idDetalle);
                                    if (!idUsuario.Contains("ERROR"))
                                    {
                                        var idUsua = Convert.ToInt32(idUsuario);

                                        // Filtrar los servicios correspondientes al usuario actual
                                        var servicios = serviciosInicial.Where(x => x.id_detalle == idDetalle
                                        && x.numero_identificacion_usuario == usuario.numero_identificacion_usuario
                                        && x.numero_factura == factura.numero_factura).ToList();

                                        if (servicios.Count() > 0)
                                        {
                                            List<fis_rips_cargue_consulta> listausuariosinsercion = new List<fis_rips_cargue_consulta>();
                                            List<fis_rips_cups> listaCups = BusClass.TraerListadoCups();

                                            foreach (var servicio in servicios)
                                            {
                                                conteoServicios++;
                                                // Inserción de consulta
                                                fis_rips_cargue_consulta consu = EstructurarCampoConsulta(servicio, factura, usuario, idLote, idTransaccion, idUsua, idDetalle, listaCups);
                                                listausuariosinsercion.Add(consu);
                                            }

                                            if (listausuariosinsercion.Count() > 0)
                                            {
                                                var insertaMasivoConsulta = BusClass.InsertarFisRipsConsultaMasivo(listausuariosinsercion, ref MsgRes);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception(idUsuario);
                                    }
                                }
                            }
                        }
                    }

                    // Guardar registros finales
                    var insertaRegistros = BusClass.GuardarRegistroTarifasRipsFacturas(buscarCuv.cuv, idDetalle, SesionVar.UserName);
                    if (insertaRegistros == 0)
                    {
                        throw new Exception("ERROR AL INGRESAR LOS DATOS");
                    }
                    else
                    {
                        resultado = GuardarAceptadaFacturaMasivo(idDetalle, usuarioAnalista);
                        if (resultado.Contains("ERROR EN EL INGRESO"))
                        {
                            throw new Exception(resultado);
                        }
                    }
                }
                else
                {
                    throw new Exception("ERROR AL INSERTAR DATOS INICIALES");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                BusClass.EliminarTodoElCargueFisIdFactura(idDetalle);

                resultado = "ERROR AL CARGAR INFORMACIÓN DE ID " + idDetalle + " : " + error;
            }

            return resultado;
        }

        public string GuardarAceptadaFacturaMasivo(int? detalle, string usuario)
        {
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            String mensaje = "";

            sis_usuario us = BusClass.datosUsuarioUser(usuario);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PrestadoresConnectionString"].ConnectionString;

            var auditor = SesionVar.IDUser;

            var BaseImagen = Model.GetFirmas(auditor);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE rips_af_cargue_dtll SET estado_factura = 2,fecha_recepcion_fac = @fecha_recepcion_fac, id_analista_gestiona = @id_analista_gestiona Where id_af = @id";

                    if (us != null)
                    {
                        command.Parameters.AddWithValue("@id_analista_gestiona", us.id_usuario);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@id_analista_gestiona", 0);
                    }

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

        public JsonResult ValidarConMasivo(int? idCargue, int? idDetalle, string codPrestador, string seleccionadas, string codSeleecionados, int? tipoCargue)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                string[] listadoFacturas = new string[0];

                if (tipoCargue == 1)
                {
                    var existenDatos = ExistenMasivosCargadosIdDetalle(idDetalle);
                    if (existenDatos == 0)
                    {
                        throw new Exception("NO EXISTEN DATOS PARA LA FACTURA CON ID DETALLE: " + idDetalle);
                    }
                    else
                    {
                        var cargarInformacion = SubirDatosSegunId(idDetalle, idCargue);

                        if (!cargarInformacion.Contains("ERROR"))
                        {
                            mensaje = "INFORMACIÓN VALIDADA CORRECTAMENTE";
                            rta = 1;
                        }
                        else
                        {
                            throw new Exception(cargarInformacion);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(seleccionadas))
                    {
                        throw new Exception("SIN FACTURAS SELECCIONADAS");
                    }

                    listadoFacturas = seleccionadas.Split(',');

                    foreach (var item in listadoFacturas)
                    {
                        var existenDatos = ExistenMasivosCargadosIdDetalle(Convert.ToInt32(item));
                        if (existenDatos == 0)
                        {
                            throw new Exception("NO EXISTEN DATOS PARA LA FACTURA CON ID DETALLE: " + item);
                        }
                        else
                        {
                            var cargarInformacion = SubirDatosSegunId(Convert.ToInt32(item), idCargue);

                            if (!cargarInformacion.Contains("ERROR"))
                            {
                                mensaje = "INFORMACIÓN VALIDADA CORRECTAMENTE";
                                rta = 1;
                            }
                            else
                            {
                                throw new Exception(cargarInformacion);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL VALIDAR FACTURAS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public int ExistenMasivosCargadosIdDetalle(int? idDetalle)
        {
            var respuesta = 0;
            try
            {
                List<fis_rips_sinJson_detalle> listado = BusClass.TraerListadoCargueMasivo(idDetalle);
                respuesta = listado.Count() > 0 ? 1 : 0;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }

        //public string SubirDatosSegunId(int? idDetalle)
        //{
        //    var resultado = "";
        //    try
        //    {
        //        management_fisBuscarCuv_idFacturaResult buscarCuv = new management_fisBuscarCuv_idFacturaResult();
        //        buscarCuv = BusClass.FisBuscarCuv(idDetalle);

        //        List<management_fisMasivo_buscarFacturasResult> listadoFacturas = BusClass.FisBuscarFacturas(idDetalle);

        //        List<management_fisMasivo_buscarUsuariosResult> listadoUsuariosInicial = BusClass.FisBuscarFacturasUsuarios(idDetalle, "");

        //        List<management_fisMasivo_buscarServiciosResult> serviciosInicial = BusClass.FisBuscarFacturasServicios(idDetalle, "");

        //        if (buscarCuv != null)
        //        {
        //            fis_rips_cargue_LoteConsultas lote = new fis_rips_cargue_LoteConsultas();
        //            lote.id_factura = idDetalle;
        //            lote.codigo_cuv = buscarCuv.cuv;
        //            lote.estado = 1;
        //            lote.tipo = 3;
        //            lote.tipo_ingreso = null;
        //            lote.codPrestador_factura = buscarCuv.codigo_prestador;
        //            lote.fecha_digita = DateTime.Now;
        //            lote.usuario_digita = SesionVar.UserName;
        //            var idLote = BusClass.FisRipsInsercionLote(lote);

        //            if (idLote != 0)
        //            {

        //                if (listadoFacturas.Count() > 0)
        //                {
        //                    var conteoFacturas = 0;
        //                    foreach (var facturas in listadoFacturas)
        //                    {
        //                        conteoFacturas++;

        //                        fis_rips_cargue_transaccion transa = new fis_rips_cargue_transaccion();
        //                        transa.id_lote = idLote;
        //                        transa.id_factura = idDetalle;
        //                        transa.numDocumentoIdObligado = facturas.codigo_prestador;
        //                        transa.numFactura = facturas.numero_factura;
        //                        transa.tipoNota = null;
        //                        transa.numNota = null;
        //                        transa.fecha_digita = DateTime.Now;
        //                        transa.usuario_digita = SesionVar.UserName;

        //                        var idTransaccion = BusClass.FisRipsInsercionTransaccion(transa);
        //                        if (idTransaccion != 0)
        //                        {
        //                            List<management_fisMasivo_buscarUsuariosResult> listadoUsuarios = listadoUsuariosInicial.Where(x => x.numero_factura == facturas.numero_factura).ToList();
        //                            //List<management_fisMasivo_buscarUsuariosResult> listadoUsuarios = BusClass.FisBuscarFacturasUsuarios(idDetalle, facturas.numero_factura);

        //                            if (listadoUsuarios.Count() > 0)
        //                            {
        //                                var conteoUsuarios = 0;
        //                                foreach (var usuarios in listadoUsuarios)
        //                                {
        //                                    conteoUsuarios++;

        //                                    fis_rips_cargue_usuarios us = new fis_rips_cargue_usuarios();
        //                                    us.id_lote = idLote;
        //                                    us.id_transaccion = idTransaccion;
        //                                    us.id_factura = idDetalle;
        //                                    us.tipoDocumentoIdentificacion = usuarios.tipo_identificacion_usuario;
        //                                    us.numDocumentoIdentificacion = usuarios.numero_identificacion_usuario;
        //                                    us.codMunicipioResidencia = null;
        //                                    us.tipoUsuario = null;
        //                                    us.fechaNacimiento = null;
        //                                    us.codSexo = null;
        //                                    us.codPaisResidencia = null;
        //                                    us.codZonaTerritorialResidencia = null;
        //                                    us.incapacidad = null;
        //                                    us.consecutivo = null;
        //                                    us.codPaisOrigen = null;
        //                                    us.fecha_digita = DateTime.Now;
        //                                    us.usuario_digita = SesionVar.UserName;

        //                                    var idUsuario = BusClass.FisRipsInsercionUsuario(us);
        //                                    if (idUsuario != null)
        //                                    {
        //                                        //List<management_fisMasivo_buscarServiciosResult> servicios = BusClass.FisBuscarFacturasServicios(idDetalle, us.numDocumentoIdentificacion);
        //                                        List<management_fisMasivo_buscarServiciosResult> servicios = serviciosInicial.Where(x => x.id_detalle == idDetalle && x.numero_identificacion_usuario == us.numDocumentoIdentificacion).ToList();

        //                                        if (servicios.Count() > 0)
        //                                        {
        //                                            var conteoServicios = 0;

        //                                            foreach (var servicio in servicios)
        //                                            {
        //                                                conteoServicios++;

        //                                                fis_rips_cargue_consulta con = new fis_rips_cargue_consulta();
        //                                                con.id_lote = idLote;
        //                                                con.id_factura = idDetalle;
        //                                                con.id_transaccion = idTransaccion;
        //                                                con.id_usuarios = idUsuario;
        //                                                con.codPrestador = Convert.ToDecimal(facturas.codigo_prestador);
        //                                                con.fechaInicioAtencion = servicio.fecha_atencion;
        //                                                con.numAutorizacion = servicio.numero_autorizacion;
        //                                                con.codConsulta = null;
        //                                                con.modalidadGrupoServicioTecSal = null;
        //                                                con.grupoServicios = null;
        //                                                con.codServicio = null;
        //                                                con.finalidadTecnologiaSalud = null;
        //                                                con.causaMotivoAtencion = null;
        //                                                con.codDiagnosticoPrincipal = servicio.codigo_diagnostico_principal;
        //                                                con.codDiagnosticoRelacionado1 = servicio.codigo_diagnostico_relacionado;
        //                                                con.codDiagnosticoRelacionado2 = null;
        //                                                con.codDiagnosticoRelacionado3 = null;
        //                                                con.tipoDiagnosticoPrincipal = null;
        //                                                con.tipoDocumentoIdentificacion = us.tipoDocumentoIdentificacion;
        //                                                con.numDocumentoIdentificacion = us.numDocumentoIdentificacion;
        //                                                con.vrServicio = servicio.valor_neto_pagar;
        //                                                con.conceptoRecaudo = null;
        //                                                con.valorPagoModerador = null;
        //                                                con.numFEVPagoModerador = null;
        //                                                con.consecutivo = null;
        //                                                con.fecha_digita = DateTime.Now;
        //                                                con.usuario_digita = SesionVar.UserName;

        //                                                var insertaConsulta = BusClass.FisRipsInsercionConsulta(con);
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }

        //                        }
        //                    }

        //                    var insertaRegistros = BusClass.GuardarRegistroTarifasRipsFacturas(buscarCuv.cuv, idDetalle, SesionVar.UserName);
        //                    if (insertaRegistros == 0)
        //                    {
        //                        throw new Exception("ERROR AL INGRESAR LOS DATOS");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("ERROR AL INSERTAR DATOS INICIALES");
        //            }


        //        }
        //        else
        //        {
        //            throw new Exception("NO EXISTE FACTURA CON ESTE ID");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        resultado = "ERROR AL CARGAR INFORMACIÓN DE ID " + idDetalle + " : " + error;
        //    }

        //    return resultado;
        //}

        public string SubirDatosSegunId(int? idDetalle, int? idCargue)
        {
            var resultado = "";
            var conteoServicios = 0;

            try
            {
                var buscarCuv = BusClass.FisBuscarCuv(idDetalle);
                var existenDetalles = BusClass.HayDetallesIdFactura(idDetalle);

                if (existenDetalles != 1)
                {
                    throw new Exception("NO EXISTEN DATOS DE FACTURA CON ESTE ID");
                }
                else
                {
                    var EliminaDetalles = BusClass.EliminarDetallesIdFactura(idDetalle);
                }

                var listadoFacturas = BusClass.FisBuscarFacturas(idDetalle);
                var listadoUsuariosInicial = BusClass.FisBuscarFacturasUsuarios(idDetalle, "");
                var serviciosInicial = BusClass.FisBuscarFacturasServicios(idDetalle, "");

                // Inserción del lote
                var idLote = InsertarLote(buscarCuv, idDetalle);

                if (idLote != 0 && listadoFacturas.Count() > 0)
                {
                    foreach (var factura in listadoFacturas)
                    {
                        // Inserción de transacción
                        var idTransaccion = InsertarTransaccion(factura, idLote, idDetalle);

                        if (idTransaccion != 0)
                        {
                            // Filtrar los usuarios correspondientes a la factura actual
                            var listadoUsuarios = listadoUsuariosInicial.Where(x => x.numero_factura == factura.numero_factura
                            && x.id_detalle == idDetalle && x.codigo_prestador == factura.codigo_prestador).ToList();

                            if (listadoUsuarios.Count() > 0)
                            {
                                foreach (var usuario in listadoUsuarios)
                                {
                                    // Inserción de usuario
                                    var idUsuario = InsertarUsuario(usuario, idLote, idTransaccion, idDetalle);
                                    if (!idUsuario.Contains("ERROR"))
                                    {
                                        var idUsua = Convert.ToInt32(idUsuario);

                                        // Filtrar los servicios correspondientes al usuario actual
                                        var servicios = serviciosInicial.Where(x => x.id_detalle == idDetalle
                                        && x.numero_identificacion_usuario == usuario.numero_identificacion_usuario
                                        && x.numero_factura == factura.numero_factura).ToList();

                                        if (servicios.Count() > 0)
                                        {
                                            List<fis_rips_cargue_consulta> listausuariosinsercion = new List<fis_rips_cargue_consulta>();
                                            List<fis_rips_cups> listaCups = BusClass.TraerListadoCups();

                                            foreach (var servicio in servicios)
                                            {
                                                conteoServicios++;
                                                // Inserción de consulta
                                                fis_rips_cargue_consulta consu = EstructurarCampoConsulta(servicio, factura, usuario, idLote, idTransaccion, idUsua, idDetalle, listaCups);
                                                listausuariosinsercion.Add(consu);
                                            }

                                            if (listausuariosinsercion.Count() > 0)
                                            {
                                                var insertaMasivoConsulta = BusClass.InsertarFisRipsConsultaMasivo(listausuariosinsercion, ref MsgRes);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception(idUsuario);
                                    }
                                }
                            }
                        }
                    }

                    // Guardar registros finales
                    var insertaRegistros = BusClass.GuardarRegistroTarifasRipsFacturas(buscarCuv.cuv, idDetalle, SesionVar.UserName);
                    if (insertaRegistros == 0)
                    {
                        throw new Exception("ERROR AL INGRESAR LOS DATOS");
                    }
                    else
                    {
                        resultado = GuardarAceptadaFactura(idDetalle, idCargue);
                        if (resultado.Contains("ERROR EN EL INGRESO"))
                        {
                            throw new Exception(resultado);
                        }
                    }
                }
                else
                {
                    throw new Exception("ERROR AL INSERTAR DATOS INICIALES");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                BusClass.EliminarTodoElCargueFisIdFactura(idDetalle);

                resultado = "ERROR AL CARGAR INFORMACIÓN DE ID " + idDetalle + " : " + error;
            }

            return resultado;
        }

        // Función para insertar el lote
        private int InsertarLote(management_fisBuscarCuv_idFacturaResult buscarCuv, int? idDetalle)
        {
            var lote = new fis_rips_cargue_LoteConsultas
            {
                id_factura = idDetalle,
                codigo_cuv = buscarCuv.cuv,
                estado = 1,
                tipo = 3,
                tipo_ingreso = null,
                codPrestador_factura = buscarCuv.codigo_prestador,
                fecha_digita = DateTime.Now,
                usuario_digita = SesionVar.UserName
            };
            return BusClass.FisRipsInsercionLote(lote);
        }

        // Función para insertar la transacción
        private int InsertarTransaccion(management_fisMasivo_buscarFacturasResult factura, int idLote, int? idDetalle)
        {
            var transa = new fis_rips_cargue_transaccion
            {
                id_lote = idLote,
                id_factura = idDetalle,
                numDocumentoIdObligado = factura.codigo_prestador,
                numFactura = factura.numero_factura,
                tipoNota = null,
                numNota = null,
                fecha_digita = DateTime.Now,
                usuario_digita = SesionVar.UserName
            };
            return BusClass.FisRipsInsercionTransaccion(transa);
        }

        // Función para insertar usuario
        private string InsertarUsuario(management_fisMasivo_buscarUsuariosResult usuario, int idLote, int idTransaccion, int? idDetalle)
        {
            var mensaje = "";

            try
            {
                //List<base_beneficiarios_analitica> bas = BusClass.TraerBeneficiarioDocumentoActivo(usuario.numero_identificacion_usuario);
                //if(bas.Count() == 0)
                //{
                //    throw new Exception("El usuario con documento " + usuario.numero_identificacion_usuario + " no se encuentra activo");
                //}

                var us = new fis_rips_cargue_usuarios
                {
                    id_lote = idLote,
                    id_transaccion = idTransaccion,
                    id_factura = idDetalle,
                    tipoDocumentoIdentificacion = usuario.tipo_identificacion_usuario,
                    numDocumentoIdentificacion = usuario.numero_identificacion_usuario,
                    codMunicipioResidencia = null,
                    tipoUsuario = null,
                    fechaNacimiento = null,
                    codSexo = null,
                    codPaisResidencia = null,
                    codZonaTerritorialResidencia = null,
                    incapacidad = null,
                    consecutivo = null,
                    codPaisOrigen = null,
                    fecha_digita = DateTime.Now,
                    usuario_digita = SesionVar.UserName
                };

                var inserta = BusClass.FisRipsInsercionUsuario(us);

                if (inserta != 0)
                {
                    mensaje = Convert.ToString(inserta);
                }
                else
                {
                    mensaje = "NO SE PUDDO INGRESAR EL USUARIO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return mensaje;
        }

        // Función para insertar consulta
        private fis_rips_cargue_consulta EstructurarCampoConsulta(management_fisMasivo_buscarServiciosResult servicio,
            management_fisMasivo_buscarFacturasResult factura, management_fisMasivo_buscarUsuariosResult usuario,
            int idLote, int idTransaccion, int? idUsuario, int? idDetalle, List<fis_rips_cups> listadoCups)
        {
            fis_rips_cargue_consulta con = new fis_rips_cargue_consulta();

            try
            {
                con.id_lote = idLote;
                con.id_factura = idDetalle;
                con.id_transaccion = idTransaccion;
                con.id_usuarios = idUsuario;
                //con.codPrestador = Convert.ToDecimal(factura.codigo_prestador);

                if (decimal.TryParse(factura.codigo_prestador, out decimal codPrestador))
                {
                    con.codPrestador = codPrestador; // Asignar si es numérico
                }
                else
                {
                    con.codPrestador = 0; // O un valor predeterminado si no es numérico
                }

                if (servicio.fecha_atencion == null)
                {
                    throw new Exception("Fecha atención no puede venir vacía");
                }

                con.fechaInicioAtencion = servicio.fecha_atencion;
                con.numAutorizacion = servicio.numero_autorizacion;
                con.fecha_prestacion = servicio.fecha_atencion;

                con.codConsulta = servicio.cups_homologado;

                string codigoLimpio = con.codConsulta.Replace("-0", "-");
                if (codigoLimpio.StartsWith("0"))
                {
                    codigoLimpio = codigoLimpio.Substring(1);
                }

                fis_rips_cups cups = listadoCups.FirstOrDefault(x => x.codigo_cups == codigoLimpio);

                con.modalidadGrupoServicioTecSal = servicio.descripcion_cups;
                con.grupoServicios = null;
                con.codServicio = null;
                con.finalidadTecnologiaSalud = null;
                con.causaMotivoAtencion = null;
                con.codDiagnosticoPrincipal = servicio.codigo_diagnostico_principal;
                con.codDiagnosticoRelacionado1 = servicio.codigo_diagnostico_relacionado;
                con.codDiagnosticoRelacionado2 = null;
                con.codDiagnosticoRelacionado3 = null;
                con.tipoDiagnosticoPrincipal = null;
                con.tipoDocumentoIdentificacion = usuario.tipo_identificacion_usuario;
                con.numDocumentoIdentificacion = usuario.numero_identificacion_usuario;
                con.vrServicio = servicio.valor_neto_pagar;
                con.conceptoRecaudo = null;
                con.valorPagoModerador = null;
                con.numFEVPagoModerador = null;
                con.consecutivo = null;
                con.fecha_digita = DateTime.Now;
                con.usuario_digita = SesionVar.UserName;
                con.cantidad = servicio.numero_unidades > 0 ? servicio.numero_unidades : 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception(error);
            }

            return con;

            //return BusClass.FisRipsInsercionConsulta(con);
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

        public JsonResult ActualizarIvaFis(int? idRegistro, string tipo, int? idFactura)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var iva = (tipo == "LA" || tipo == "LB") ? 19 : 0;

                var actualiza = BusClass.ActualizarTipoIva(idRegistro, tipo, iva);
                mensaje = actualiza != 0 ? "" : "ERROR AL ACTUALIZAR IVA";

                //var listadoCups = BusClass.TraerListadoCupsFacturas(idFactura);
                //Session["ListadoCupsCompleto"] = listadoCups;

                //var listadoGlosas = BusClass.ListaGlosas(idFactura);
                //Session["ListadoGlosasCompleto"] = listadoGlosas;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ACTUALIZAR IVA: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public int DevolverTotalIva(int? idFactura)
        {
            var resultado = 0;
            try
            {
                management_fis_devolverIVAResult total = BusClass.DevolverIvaTotalIdFactura(idFactura);
                resultado = Convert.ToInt32(total.ValorNetoConIVA);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return resultado;
        }

        public ActionResult TableroCreditosSAP()
        {
            List<management_fis_tableroCargueContableSapResult> listado = new List<management_fis_tableroCargueContableSapResult>();
            try
            {
                listado = BusClass.TraerGlosasTerminadasDocumentoSAP();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.listado = listado;
            ViewBag.conteo = listado.Count();

            return View();
        }

        public PartialViewResult ModalDocumentosContables(int? idGlosa, int? idRegistro, int? idAf)
        {
            ViewBag.idGlosa = idGlosa;
            ViewBag.idRegistro = idRegistro;
            ViewBag.idAf = idAf;

            return PartialView();
        }

        public JsonResult GuardarDocumentoContable(int? idGlosa, int? idRegistro, int? idAf, string aplicacionSap,
            string documentoContable, DateTime? fechaContable, HttpPostedFileBase archivo)
        {
            var mensaje = "";
            var rta = 0;
            try
            {

                fis_rips_cargue_registrosCompletos reg = BusClass.TraerRegistroRipsId(idRegistro);

                fis_rips_facturas_glosa_archivosContables con = new fis_rips_facturas_glosa_archivosContables
                {
                    id_factura = idAf,
                    id_usuario = reg.id_usuario,
                    id_glosa = idGlosa,
                    aplicacion_sap = aplicacionSap,
                    documento_contable_sap = documentoContable,
                    fecha_contable = fechaContable,
                };


                fis_rips_facturas_glosa glo = new fis_rips_facturas_glosa
                {
                    id_glosa = (int)idGlosa,
                    aplicacion_sap = aplicacionSap,
                    documento_contable_sap = documentoContable,
                    fecha_contable = fechaContable
                };

                var actualiza = BusClass.ActulizaGlosaDocContable(glo);
                if (actualiza != 0)
                {
                    mensaje = "REGISTRO INGRESADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL GUARDAR LA INFORMACIÓN";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL GUARDAR LA INFORMACIÓN: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult TableroDescargueFacturas()
        {
            ViewBag.regional = BusClass.TraerregionalesFis();

            return View();
        }

        public void DescargarReporteHES(DateTime? fechaIni, DateTime? fechaFin, string regional)
        {
            List<management_fis_reporteHesResult> listado = new List<management_fis_reporteHesResult>();

            try
            {
                ExcelPackage Ep = new ExcelPackage();

                listado = BusClass.TraerDatosReporteHES(fechaIni, fechaFin, regional);
                if (listado == null || listado.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('SIN DATOS PARA MOSTRAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ListadoHES");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:EZ1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:EZ1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:EZ1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:EZ1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:EZ1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:EZ1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    Sheet.Cells["A1:EZ1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    // Encabezados

                    Sheet.Cells["A1"].Value = "Cabecera";
                    //Sheet.Cells["B1"].Value = "Id listado";
                    Sheet.Cells["B1"].Value = "Clase de documento";
                    Sheet.Cells["C1"].Value = "Proveedor";
                    Sheet.Cells["D1"].Value = "Numero del pedido";
                    Sheet.Cells["E1"].Value = "Fecha del documento";
                    Sheet.Cells["F1"].Value = "Condición de pago";
                    Sheet.Cells["G1"].Value = "Incoterms";
                    Sheet.Cells["H1"].Value = "Texto del incoterm";
                    Sheet.Cells["I1"].Value = "Moneda";
                    Sheet.Cells["J1"].Value = "Tipo de cambio";
                    Sheet.Cells["K1"].Value = "Clase de condición";
                    Sheet.Cells["L1"].Value = "Importe";
                    Sheet.Cells["M1"].Value = "Por";
                    Sheet.Cells["N1"].Value = "Unidad de medida";
                    Sheet.Cells["O1"].Value = "PO - Objeto del contrato";
                    Sheet.Cells["P1"].Value = "Texto de cabecera";
                    Sheet.Cells["Q1"].Value = "Otros textos de cabecera";
                    Sheet.Cells["R1"].Value = "Clausulas adicionales";
                    Sheet.Cells["S1"].Value = "Garantía";
                    Sheet.Cells["T1"].Value = "POC - Objeto del contrato";
                    Sheet.Cells["U1"].Value = "Plan amortización anticipos";
                    Sheet.Cells["V1"].Value = "Observaciones a las garantias";
                    Sheet.Cells["W1"].Value = "Condiciones comerciales";
                    Sheet.Cells["X1"].Value = "Condiciones de entrega";
                    Sheet.Cells["Y1"].Value = "Forma de pago";
                    Sheet.Cells["Z1"].Value = "Textos para bodega";
                    Sheet.Cells["AA1"].Value = "Concepto tributario";
                    Sheet.Cells["AB1"].Value = "Liquidación TRM";
                    Sheet.Cells["AC1"].Value = "B";
                    Sheet.Cells["AD1"].Value = "Vendedor";
                    Sheet.Cells["AE1"].Value = "Telefono";
                    Sheet.Cells["AF1"].Value = "Nuestra referencia";
                    Sheet.Cells["AG1"].Value = "Numero de licitación";
                    Sheet.Cells["AH1"].Value = "Función";
                    Sheet.Cells["AI1"].Value = "Número";
                    Sheet.Cells["AJ1"].Value = "Organización de compras";
                    Sheet.Cells["AK1"].Value = "Grupo de compras";
                    Sheet.Cells["AL1"].Value = "Sociedad";
                    Sheet.Cells["AM1"].Value = "TRM pactada";
                    Sheet.Cells["AN1"].Value = "Arrendamiento implícito";
                    Sheet.Cells["AO1"].Value = "Contrato con cesión de derechos económicos y/o créditos ?";
                    Sheet.Cells["AP1"].Value = "Cedido desde";
                    Sheet.Cells["AQ1"].Value = "Cedido hasta";
                    Sheet.Cells["AR1"].Value = "Tipo de cuantía";
                    Sheet.Cells["AS1"].Value = "Régimen aplicable";
                    Sheet.Cells["AT1"].Value = "Fecha doc";
                    Sheet.Cells["AU1"].Value = "Tipo Doc";
                    Sheet.Cells["AV1"].Value = "Valor base";
                    Sheet.Cells["AW1"].Value = "Valor pagado";
                    Sheet.Cells["AX1"].Value = "No. Consignación";
                    Sheet.Cells["AY1"].Value = "Clave de Banco";
                    Sheet.Cells["AZ1"].Value = "Documento contable /CRP";
                    Sheet.Cells["BA1"].Value = "Año";
                    Sheet.Cells["BB1"].Value = "Flag de Impresión";
                    Sheet.Cells["BC1"].Value = "% Retención en renta";
                    Sheet.Cells["BD1"].Value = "Posición";
                    Sheet.Cells["BE1"].Value = "Tipo de Imputación";
                    Sheet.Cells["BF1"].Value = "Tipo de Posición";
                    Sheet.Cells["BG1"].Value = "Material";
                    Sheet.Cells["BH1"].Value = "Texto breve";
                    Sheet.Cells["BI1"].Value = "Cantidad pedido";
                    Sheet.Cells["BJ1"].Value = "UMP";
                    Sheet.Cells["BK1"].Value = "T";
                    Sheet.Cells["BL1"].Value = "Fecha de entrega";
                    Sheet.Cells["BM1"].Value = "Precio neto";
                    Sheet.Cells["BN1"].Value = "Por 2";
                    Sheet.Cells["BO1"].Value = "CPP";
                    Sheet.Cells["BP1"].Value = "Grupo artículos";
                    Sheet.Cells["BQ1"].Value = "Centro";
                    Sheet.Cells["BR1"].Value = "Almacén";
                    Sheet.Cells["BS1"].Value = "N° nec";
                    Sheet.Cells["BT1"].Value = "Contrato marco";
                    Sheet.Cells["BU1"].Value = "Poscontrato(OJOCAMBIAR)BV";
                    Sheet.Cells["BV1"].Value = "Status";
                    Sheet.Cells["BW1"].Value = "Dirección";
                    Sheet.Cells["BX1"].Value = "Entrega final";
                    Sheet.Cells["BY1"].Value = "Ind. Impuestos";
                    Sheet.Cells["BZ1"].Value = "Recepción Factura";
                    Sheet.Cells["CA1"].Value = "Factura final";
                    Sheet.Cells["CB1"].Value = "Verificación factura basa en EM";
                    Sheet.Cells["CC1"].Value = "Verificación factura FS";
                    Sheet.Cells["CD1"].Value = "Imputacion_amb";
                    Sheet.Cells["CE1"].Value = "Cta.mayor";
                    Sheet.Cells["CF1"].Value = "Sociedad CO";
                    Sheet.Cells["CG1"].Value = "Centro de coste";
                    Sheet.Cells["CH1"].Value = "Orden";
                    Sheet.Cells["CI1"].Value = "CeBe";
                    Sheet.Cells["CJ1"].Value = "Grafo";
                    Sheet.Cells["CK1"].Value = "Operación";
                    Sheet.Cells["CL1"].Value = "Elemento PEP";
                    Sheet.Cells["CM1"].Value = "Número principal de activo fijo";
                    Sheet.Cells["CN1"].Value = "Texto pedido de material";
                    Sheet.Cells["CO1"].Value = "Justificación del requerimiento";
                    Sheet.Cells["CP1"].Value = "PO - Desc. Extendida del item";
                    Sheet.Cells["CQ1"].Value = "PO - Texto de reparación";
                    Sheet.Cells["CR1"].Value = "PO - Texto de posición (MPPT)";
                    Sheet.Cells["CS1"].Value = "PO - Lugar de entrega";
                    Sheet.Cells["CT1"].Value = "PO - Solicitante";
                    Sheet.Cells["CU1"].Value = "PO - Solicitud aprobada por";
                    Sheet.Cells["CV1"].Value = "Ind. Borrado";
                    Sheet.Cells["CW1"].Value = "No. servicio";
                    Sheet.Cells["CX1"].Value = "Txt.brv.";
                    Sheet.Cells["CY1"].Value = "Cantidad";
                    Sheet.Cells["CZ1"].Value = "UM";
                    Sheet.Cells["DA1"].Value = "Precio bruto";
                    Sheet.Cells["DB1"].Value = "Mon";
                    Sheet.Cells["DC1"].Value = "Valor neto";
                    Sheet.Cells["DD1"].Value = "Cantidad real";
                    Sheet.Cells["DE1"].Value = "Limite Global";
                    Sheet.Cells["DF1"].Value = "Moneda 2";
                    Sheet.Cells["DG1"].Value = "Ilimitado";
                    Sheet.Cells["DH1"].Value = "Valor previsto";
                    Sheet.Cells["DI1"].Value = "Valor real";
                    Sheet.Cells["DJ1"].Value = "Ped. Ab";
                    Sheet.Cells["DK1"].Value = "Posicion";
                    Sheet.Cells["DL1"].Value = "Ilimitado 2";
                    Sheet.Cells["DM1"].Value = "Limite";
                    Sheet.Cells["DN1"].Value = "Valo real";
                    Sheet.Cells["DO1"].Value = "Texto breve 2";
                    Sheet.Cells["DP1"].Value = "Límite";
                    Sheet.Cells["DQ1"].Value = "Ilimitado 3";
                    Sheet.Cells["DR1"].Value = "Valor real 2";
                    Sheet.Cells["DS1"].Value = "Cedula";
                    Sheet.Cells["DT1"].Value = "Placa";
                    Sheet.Cells["DU1"].Value = "Placa del remolque";
                    Sheet.Cells["DV1"].Value = "Ciudad de destino";
                    Sheet.Cells["DW1"].Value = "Inicio periodo validez";
                    Sheet.Cells["DX1"].Value = "Fin periodo validez";
                    Sheet.Cells["DY1"].Value = "Modo Transporte frontera";
                    Sheet.Cells["DZ1"].Value = "Modo Transporte Nacional";
                    Sheet.Cells["EA1"].Value = "Clase documento preliminar";
                    Sheet.Cells["EB1"].Value = "Solicitud de Pedido";
                    Sheet.Cells["EC1"].Value = "Posicion Solicitud";
                    Sheet.Cells["ED1"].Value = "Doc.modelo";
                    Sheet.Cells["EE1"].Value = "Pos.modelo";
                    Sheet.Cells["EF1"].Value = "Cantidad o porcentaje fijo de compra";
                    Sheet.Cells["EG1"].Value = "Porcentaje";
                    Sheet.Cells["EH1"].Value = "No. Mercancia/No. Cod. Imp";
                    Sheet.Cells["EI1"].Value = "No. Codigo importacion";
                    Sheet.Cells["EJ1"].Value = "Impresión de Precio";
                    Sheet.Cells["EK1"].Value = "Precio estimado";
                    Sheet.Cells["EL1"].Value = "Perfil Pieza Fabricante";
                    Sheet.Cells["EM1"].Value = "Total Exceso Suministro";
                    Sheet.Cells["EN1"].Value = "Ilimitado 4";
                    Sheet.Cells["EO1"].Value = "Entrada de Mercancia";
                    Sheet.Cells["EP1"].Value = "Ultima Fecha EM";
                    Sheet.Cells["EQ1"].Value = "Neto";
                    Sheet.Cells["ER1"].Value = "Solicitante";
                    Sheet.Cells["ES1"].Value = "NUEVO CAMPO";
                    Sheet.Cells["ET1"].Value = "munipio de prestaccion";
                    Sheet.Cells["EU1"].Value = "ID FACTURA";
                    Sheet.Cells["EV1"].Value = "NOMBRE PRESTADOR";
                    Sheet.Cells["EW1"].Value = "NUMERO DE PEDIDO";
                    Sheet.Cells["EX1"].Value = "Regional factura";
                    Sheet.Cells["EY1"].Value = "Valor facturado";
                    Sheet.Cells["EZ1"].Value = "Sumatoria IVA";

                    int row = 2;

                    foreach (management_fis_reporteHesResult item in listado)
                    {
                        Sheet.Cells["A" + row + ":EZ" + row].Style.Font.Size = 10;

                        if (item.ID_FACTURA == 44616)
                        {
                            var rpeba = "";
                        }

                        //Sheet.Cells[string.Format("A{0}", row)].Value = item.Id_listado == 1 ? Convert.ToString("X") : string.Empty;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.Id_listado == 1 ? "X" : null;

                        //Sheet.Cells[string.Format("B{0}", row)].Value = item.Id_listado;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.Clase_de_documento;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.Proveedor;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.Numero_del_pedido;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.Fecha_del_documento.HasValue ? item.Fecha_del_documento.Value.ToString("dd.MM.yyyy") : string.Empty;

                        //Sheet.Cells[string.Format("E{0}", row)].Value = item.Fecha_del_documento != null ? item.Fecha_del_documento.Value.ToString("dd.MM.yyyy") : item.Fecha_del_documento;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.Condición_de_pago;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.Incoterms;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.Texto_del_incoterm;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.Moneda;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.Tipo_de_cambio;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.Clase_de_condición;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.Importe;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.Por;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.Unidad_de_medida;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.PO___Objeto_del_contrato;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.Texto_de_cabecera;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.Otros_textos_de_cabecera;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.Clausulas_adicionales;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.Garantía;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.POC___Objeto_del_contrato;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.Plan_amortización_anticipos;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.Observaciones_a_las_garantias;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.Condiciones_comerciales;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.Condiciones_de_entrega;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.Forma_de_pago;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.Textos_para_bodega;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.Concepto_tributario;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.Liquidación_TRM;
                        Sheet.Cells[string.Format("AC{0}", row)].Value = item.B;
                        Sheet.Cells[string.Format("AD{0}", row)].Value = item.Vendedor;
                        Sheet.Cells[string.Format("AE{0}", row)].Value = item.Telefono;
                        Sheet.Cells[string.Format("AF{0}", row)].Value = item.Nuestra_referencia;
                        Sheet.Cells[string.Format("AG{0}", row)].Value = item.Numero_de_licitación;
                        Sheet.Cells[string.Format("AH{0}", row)].Value = item.Función;
                        Sheet.Cells[string.Format("AI{0}", row)].Value = item.Número;
                        Sheet.Cells[string.Format("AJ{0}", row)].Value = item.Organización_de_compras;
                        Sheet.Cells[string.Format("AK{0}", row)].Value = item.Grupo_de_compras;
                        Sheet.Cells[string.Format("AL{0}", row)].Value = item.Sociedad;
                        Sheet.Cells[string.Format("AM{0}", row)].Value = item.TRM_pactada;
                        Sheet.Cells[string.Format("AN{0}", row)].Value = item.Arrendamiento_implícito;
                        Sheet.Cells[string.Format("AO{0}", row)].Value = item.Contrato_con_cesión_de_derechos_económicos_y_o_créditos__;
                        Sheet.Cells[string.Format("AP{0}", row)].Value = item.Cedido_desde;
                        Sheet.Cells[string.Format("AQ{0}", row)].Value = item.Cedido_hasta;
                        Sheet.Cells[string.Format("AR{0}", row)].Value = item.Tipo_de_cuantía;
                        Sheet.Cells[string.Format("AS{0}", row)].Value = item.Régimen_aplicable;
                        Sheet.Cells[string.Format("AT{0}", row)].Value = item.Fecha_doc;
                        Sheet.Cells[string.Format("AU{0}", row)].Value = item.Tipo_Doc;
                        Sheet.Cells[string.Format("AV{0}", row)].Value = item.Valor_base;
                        Sheet.Cells[string.Format("AW{0}", row)].Value = item.Valor_pagado;
                        Sheet.Cells[string.Format("AX{0}", row)].Value = item.No__Consignación;
                        Sheet.Cells[string.Format("AY{0}", row)].Value = item.Clave_de_Banco;
                        Sheet.Cells[string.Format("AZ{0}", row)].Value = item.Documento_contable__CRP;
                        Sheet.Cells[string.Format("BA{0}", row)].Value = item.Año;
                        Sheet.Cells[string.Format("BB{0}", row)].Value = item.Flag_de_Impresión;
                        Sheet.Cells[string.Format("BC{0}", row)].Value = item.Por;
                        Sheet.Cells[string.Format("BD{0}", row)].Value = item.Posición;
                        Sheet.Cells[string.Format("BE{0}", row)].Value = item.Tipo_de_Imputación;
                        Sheet.Cells[string.Format("BF{0}", row)].Value = item.Tipo_de_Posicón;
                        Sheet.Cells[string.Format("BG{0}", row)].Value = item.Material;
                        Sheet.Cells[string.Format("BH{0}", row)].Value = item.Texto_breve;
                        Sheet.Cells[string.Format("BI{0}", row)].Value = item.Cantidad_pedido;
                        Sheet.Cells[string.Format("BJ{0}", row)].Value = item.UMP;
                        Sheet.Cells[string.Format("BK{0}", row)].Value = item.T;
                        Sheet.Cells[string.Format("BL{0}", row)].Value = item.Fecha_de_entrega.ToString("dd.MM.yyyy");
                        Sheet.Cells[string.Format("BM{0}", row)].Value = item.Precio_neto;
                        Sheet.Cells[string.Format("BN{0}", row)].Value = item.Por_2;
                        Sheet.Cells[string.Format("BO{0}", row)].Value = item.CPP;
                        Sheet.Cells[string.Format("BP{0}", row)].Value = item.Grupo_artículos;
                        Sheet.Cells[string.Format("BQ{0}", row)].Value = item.Centro;
                        Sheet.Cells[string.Format("BR{0}", row)].Value = item.Almacén;
                        Sheet.Cells[string.Format("BS{0}", row)].Value = item.N_nec;
                        Sheet.Cells[string.Format("BT{0}", row)].Value = item.Contrato_marco;
                        Sheet.Cells[string.Format("BU{0}", row)].Value = item.Poscontrato_OJOCAMBIAR_BV;
                        Sheet.Cells[string.Format("BV{0}", row)].Value = item.Status;
                        Sheet.Cells[string.Format("BW{0}", row)].Value = item.Dirección;
                        Sheet.Cells[string.Format("BX{0}", row)].Value = item.Entrega_final;
                        Sheet.Cells[string.Format("BY{0}", row)].Value = item.Ind_impuestos;
                        Sheet.Cells[string.Format("BZ{0}", row)].Value = item.Recepción_Factura;
                        Sheet.Cells[string.Format("CA{0}", row)].Value = item.Factura_final;
                        Sheet.Cells[string.Format("CB{0}", row)].Value = item.Verificación_factura_basa_en_EM;
                        Sheet.Cells[string.Format("CC{0}", row)].Value = item.Verificación_factura_FS;
                        Sheet.Cells[string.Format("CD{0}", row)].Value = item.Imputacion_amb;
                        Sheet.Cells[string.Format("CE{0}", row)].Value = item.Cta_mayor;
                        Sheet.Cells[string.Format("CF{0}", row)].Value = item.Sociedad_CO;
                        Sheet.Cells[string.Format("CG{0}", row)].Value = item.Centro_de_coste;
                        Sheet.Cells[string.Format("CH{0}", row)].Value = item.Orden;
                        Sheet.Cells[string.Format("CI{0}", row)].Value = item.CeBe;
                        Sheet.Cells[string.Format("CJ{0}", row)].Value = item.Grafo;
                        Sheet.Cells[string.Format("CK{0}", row)].Value = item.Operación;
                        Sheet.Cells[string.Format("CL{0}", row)].Value = item.Elemento_PEP;
                        Sheet.Cells[string.Format("CM{0}", row)].Value = item.Número_principal_de_activo_fijo;
                        Sheet.Cells[string.Format("CN{0}", row)].Value = item.Texto_pedido_de_material;
                        Sheet.Cells[string.Format("CO{0}", row)].Value = item.Justificación_del_requerimiento;
                        Sheet.Cells[string.Format("CP{0}", row)].Value = item.PO___Desc_Extendida_del_item;
                        Sheet.Cells[string.Format("CQ{0}", row)].Value = item.PO___Texto_de_reparación;
                        Sheet.Cells[string.Format("CR{0}", row)].Value = item.PO___Texto_de_posición__MPPT_;
                        Sheet.Cells[string.Format("CS{0}", row)].Value = item.PO___Lugar_de_entrega;
                        Sheet.Cells[string.Format("CT{0}", row)].Value = item.PO___Solicitante;
                        Sheet.Cells[string.Format("CU{0}", row)].Value = item.PO___Solicitud_aprobada_por;
                        Sheet.Cells[string.Format("CV{0}", row)].Value = item.Ind_Borrado;
                        Sheet.Cells[string.Format("CW{0}", row)].Value = item.Nro_Servicio;
                        Sheet.Cells[string.Format("CX{0}", row)].Value = item.Txt_brv;
                        Sheet.Cells[string.Format("CY{0}", row)].Value = item.Cantidad;
                        Sheet.Cells[string.Format("CZ{0}", row)].Value = item.UM;
                        Sheet.Cells[string.Format("DA{0}", row)].Value = item.Precio_bruto;
                        Sheet.Cells[string.Format("DB{0}", row)].Value = item.Mon;
                        Sheet.Cells[string.Format("DC{0}", row)].Value = item.Valor_neto;
                        Sheet.Cells[string.Format("DD{0}", row)].Value = item.Cantidad_real;
                        Sheet.Cells[string.Format("DE{0}", row)].Value = item.Limite_Global;
                        Sheet.Cells[string.Format("DF{0}", row)].Value = item.Moneda_2;
                        Sheet.Cells[string.Format("DG{0}", row)].Value = item.Ilimitado;
                        Sheet.Cells[string.Format("DH{0}", row)].Value = item.Valor_previsto;
                        Sheet.Cells[string.Format("DI{0}", row)].Value = item.Valor_real;
                        Sheet.Cells[string.Format("DJ{0}", row)].Value = item.Ped_Ab;
                        Sheet.Cells[string.Format("DK{0}", row)].Value = item.Posicion;
                        Sheet.Cells[string.Format("DL{0}", row)].Value = item.Ilimitado_2;
                        Sheet.Cells[string.Format("DM{0}", row)].Value = item.Limite;
                        Sheet.Cells[string.Format("DN{0}", row)].Value = item.Valor_real;
                        Sheet.Cells[string.Format("DO{0}", row)].Value = item.Texto_breve_2;
                        Sheet.Cells[string.Format("DP{0}", row)].Value = item.Limite;
                        Sheet.Cells[string.Format("DQ{0}", row)].Value = item.Ilimitado_3;
                        Sheet.Cells[string.Format("DR{0}", row)].Value = item.Valor_real_2;
                        Sheet.Cells[string.Format("DS{0}", row)].Value = item.Cedula;
                        Sheet.Cells[string.Format("DT{0}", row)].Value = item.Placa;
                        Sheet.Cells[string.Format("DU{0}", row)].Value = item.Placa_del_remolque;
                        Sheet.Cells[string.Format("DV{0}", row)].Value = item.Ciudad_de_destino;
                        Sheet.Cells[string.Format("DW{0}", row)].Value = item.Inicio_periodo_validez;
                        Sheet.Cells[string.Format("DX{0}", row)].Value = item.Fin_periodo_validez;
                        Sheet.Cells[string.Format("DY{0}", row)].Value = item.Modo_Transporte_frontera;
                        Sheet.Cells[string.Format("DZ{0}", row)].Value = item.Modo_Transporte_Nacional;
                        Sheet.Cells[string.Format("EA{0}", row)].Value = item.Clase_documento_preliminar;
                        Sheet.Cells[string.Format("EB{0}", row)].Value = item.Solicitud_de_Pedido;
                        Sheet.Cells[string.Format("EC{0}", row)].Value = item.Posicion_Solicitud;
                        Sheet.Cells[string.Format("ED{0}", row)].Value = item.Doc_modelo;
                        Sheet.Cells[string.Format("EE{0}", row)].Value = item.Pos_modelo;
                        Sheet.Cells[string.Format("EF{0}", row)].Value = item.Cantidad_o_porcentaje_fijo_de_compra;
                        Sheet.Cells[string.Format("EG{0}", row)].Value = item.Porcentaje;
                        Sheet.Cells[string.Format("EH{0}", row)].Value = item.No_Mercancia_No_Cod_Imp;
                        Sheet.Cells[string.Format("EI{0}", row)].Value = item.No_Codigo_importacion;
                        Sheet.Cells[string.Format("EJ{0}", row)].Value = item.Impresión_de_Precio;
                        Sheet.Cells[string.Format("EK{0}", row)].Value = item.Precio_estimado;
                        Sheet.Cells[string.Format("EL{0}", row)].Value = item.Perfil_Pieza_Fabricante;
                        Sheet.Cells[string.Format("EM{0}", row)].Value = item.Total_Exceso_Suministro;
                        Sheet.Cells[string.Format("EN{0}", row)].Value = item.Ilimitado_4;
                        Sheet.Cells[string.Format("EO{0}", row)].Value = item.Entrada_de_Mercancia;
                        Sheet.Cells[string.Format("EP{0}", row)].Value = item.Ultima_Fecha_EM;
                        Sheet.Cells[string.Format("EQ{0}", row)].Value = item.Neto;
                        Sheet.Cells[string.Format("ER{0}", row)].Value = item.Solicitante;
                        Sheet.Cells[string.Format("ES{0}", row)].Value = item.NUEVO_CAMPO;
                        Sheet.Cells[string.Format("ET{0}", row)].Value = item.munipio_de_prestaccion;
                        Sheet.Cells[string.Format("EU{0}", row)].Value = item.ID_FACTURA;
                        Sheet.Cells[string.Format("EV{0}", row)].Value = item.NOMBRE_PRESTADOR;
                        Sheet.Cells[string.Format("EW{0}", row)].Value = item.NUMERO_DE_PEDIDO;
                        Sheet.Cells[string.Format("EX{0}", row)].Value = item.nomRegionalFactura;
                        Sheet.Cells[string.Format("EY{0}", row)].Value = item.valorFacturado;
                        Sheet.Cells[string.Format("EZ{0}", row)].Value = item.sumatoria_iva;

                        row++;
                    }

                    Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = "DD.MM.YYYY";
                    Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = "DD.MM.YYYY";
                    Sheet.Cells[string.Format("AV{0}", row)].Style.Numberformat.Format = "DD.MM.YYYY";
                    Sheet.Cells[string.Format("BN{0}", row)].Style.Numberformat.Format = "DD.MM.YYYY";
                    Sheet.Cells[string.Format("DY{0}", row)].Style.Numberformat.Format = "DD.MM.YYYY";
                    Sheet.Cells[string.Format("DZ{0}", row)].Style.Numberformat.Format = "DD.MM.YYYY";
                    Sheet.Cells[string.Format("ER{0}", row)].Style.Numberformat.Format = "DD.MM.YYYY";

                    Sheet.Cells["A:EZ"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ListadoHES_" + DateTime.Now + ".xlsx");

                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
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


        public ActionResult VerDetalleFacturasAuditorDetallado(int? a, int? b)
        {
            //a = id dtll, b = id cargue

            managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult datos = new managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult();
            List<management_fis_cargueRips_usuariosResult> usuarios = new List<management_fis_cargueRips_usuariosResult>();

            var cuv = "";
            var valorFactura = new decimal?();
            var valorFacturaConGlosa = new decimal?();
            var listadoUsuarios = "";

            try
            {
                usuarios = BusClass.ListadoRipsUsuarios(a);
                if (usuarios != null)
                {
                    foreach (var use in usuarios)
                    {
                        listadoUsuarios += string.IsNullOrEmpty(listadoUsuarios) ? Convert.ToString(use.id_usuarios) : "," + Convert.ToString(use.id_usuarios);
                    }
                }

                datos = BusClass.ListadoFacturasIdDetalleAuditor((int)a);
                if (datos != null)
                {
                    valorFactura = datos.valor_total_factura;
                    valorFacturaConGlosa = datos.valor_total_factura_conGlosa;
                    cuv = datos.cuv;
                }
                else
                {
                    datos = new managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.idDetalle = a;
            ViewBag.idCargue = b;
            ViewBag.valorFactura = valorFactura;
            ViewBag.valorFacturaConGlosa = valorFacturaConGlosa;

            ViewBag.listadoUsuarios = listadoUsuarios;
            ViewBag.ripsUsuarios = usuarios;

            return View(datos);
        }

        public string TraerDetalleCupsDetallado(string cuv, int? idDetalle, int? idUsuario, int? tipo)
        {
            var resultado = new StringBuilder();
            int i = 0;
            var totalCups = new decimal?(0);
            var monedaCol = new CultureInfo("es-CO");

            string cie10 = "", descie10 = "", cie10Relacionado = "", descie10Relacionado = "";
            int usuarioTieneGlosa = 0;

            const string estiloSombreado = "height: 100%; background-color: #b1b1b1 !important; border: 1px solid #dcdcdc; padding: 10px; border-radius: 5px; font-weight: bold; color: #000000; text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);";

            try
            {
                var listadoCups = BusClass.TraerListadoCupsFacturas(idDetalle);
                listadoCups = listadoCups.Where(x => x.id_usuario == idUsuario).ToList();
                var listadoCupscie = listadoCups.FirstOrDefault(x => !string.IsNullOrEmpty(x.cie10)) ?? new management_fis_facturasCuvResult();

                // Asignar valores CIE10 si están disponibles
                cie10 = listadoCupscie.cie10 ?? cie10;
                descie10 = listadoCupscie.descripcion_cie10 ?? descie10;
                cie10Relacionado = listadoCupscie.cie10_relacionado ?? cie10Relacionado;
                descie10Relacionado = listadoCupscie.descripcion_cie10_relacionado ?? descie10Relacionado;

                if (listadoCups.Any())
                {
                    foreach (var item in listadoCups)
                    {
                        usuarioTieneGlosa |= item.usuarioConGlosa != null && item.id_usuario != 0 ? 1 : 0;

                        i++;
                        var existeBen = item.existeBeneficiario == 0 ? " style='background-color:#DBC5C5;'" : "";
                        var codGlosa = item.codConGlosa != null && item.cod_cups == item.codConGlosa ? $" style='{estiloSombreado}'" : "";

                        // Agregar fila
                        resultado.AppendLine("<tr>");
                        resultado.AppendLine($"<td {codGlosa}>{i}</td>");
                        resultado.AppendLine(GenerarInputsOcultos(item, i));
                        resultado.AppendLine(GenerarCeldas(item, codGlosa, monedaCol));
                        resultado.AppendLine("</tr>");

                        totalCups += item.ValorNetoConIVA ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores (puede mejorarse según la lógica requerida)
                var error = ex.Message;
            }

            return $"{resultado}|{i}|{totalCups}|{cie10}-{descie10}|{cie10Relacionado}-{descie10Relacionado}|{idUsuario}-{usuarioTieneGlosa}";
        }

        public string TraerGlosasDetallado(int? idDetalle, int? tipo, int? idUsuario)
        {
            // tipo 1 = analistas, tipo 2 = auditores

            StringBuilder resultado = new StringBuilder();
            int i = 0;
            var totalGlosas = new decimal?(0);
            CultureInfo monedaCol = new CultureInfo("es-CO");

            try
            {
                List<management_fisFacturas_glosaResult> listado = BusClass.ListaGlosas(idDetalle);

                if (listado.Count > 0)
                {
                    foreach (var item in listado)
                    {
                        i++;

                        resultado.Append("<tr>");
                        resultado.AppendFormat("<td>{0}<input type='hidden' id='cantidadGlosa_{1}' value='{2}'/><input type='hidden' id='valorGlosa_{1}' value='{3}'/></td>",
                                               item.id_glosa, i, item.cantidad, item.valor_glosado);

                        resultado.AppendFormat("<td>{0}</td>", item.documentoUsuario);
                        resultado.AppendFormat("<td>{0}</td>", item.nombreUsuario);

                        if (tipo == 2 && item.tipo_ingreso == 1)
                        {
                            resultado.AppendFormat("<td style='background-color: #FFFAE0;'>{0}</td>", item.codCups);
                        }
                        else
                        {
                            resultado.AppendFormat("<td>{0}</td>", item.codCups);
                        }

                        resultado.AppendFormat("<td>{0}</td>", item.tipo);
                        resultado.AppendFormat("<td>{0}</td>", item.descripcionGeneral);
                        resultado.AppendFormat("<td>{0}</td>", item.descripcionEspecifico);
                        resultado.AppendFormat("<td>{0}</td>", item.descripcionAplicacion);
                        resultado.AppendLine($"<td>{item.valor_glosado.Value.ToString("C", monedaCol)}</td>");
                        resultado.AppendFormat("<td>{0}</td>", item.cantidad);
                        resultado.AppendLine($"<td>{item.valorTotal.Value.ToString("C", monedaCol)}</td>");

                        string observacion = item.observacion.Length > 100 ? item.observacion.Substring(0, 100) : item.observacion;
                        resultado.AppendFormat("<td><span class='observaciones_{0}'>{1}</span><span class='observaciones_completas_{0}' style='display: none;'>{2}</span>", i, observacion, item.observacion);

                        if (item.observacion.Length > 100)
                        {
                            resultado.AppendFormat("<label class='text-secondary_asalud botonMostrar_{0}' onclick='MostrarTextoCompleto({0})'>Mostrar</label>", i);
                            resultado.AppendFormat("<label class='text-secondary_asalud botonOcultar_{0}' style='display: none;' onclick='OcultarTextoCompleto({0})'>Ocultar</label>", i);
                        }

                        resultado.Append("</td>");

                        resultado.Append("</tr>");

                        totalGlosas += item.valorTotal;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return $"{resultado}|{i}|{totalGlosas}";
        }

        public ActionResult TableroEliminarDetallesFactura(int? idDetalle, string numFactura)
        {
            List<management_fis_facturasCuv_tableroEliminarResult> listado = new List<management_fis_facturasCuv_tableroEliminarResult>();
            try
            {
                if (idDetalle != null && idDetalle != 0 || !string.IsNullOrEmpty(numFactura))
                {
                    listado = BusClass.TraerListadoDetallesId(idDetalle, numFactura);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.conteo = listado.Count();

            ViewBag.idDetalle = idDetalle;
            ViewBag.numFactura = numFactura;

            return View();
        }

        public JsonResult EliminarMasivo(string datos, int? idFac, string numFactura)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var datoslist = datos.Split('|');

                management_fis_facturasCuv_tableroEliminar_conteoResult datoConteo = BusClass.DevolverConteoFacturas(idFac, numFactura);

                foreach (var item in datoslist)
                {
                    var registroIndi = item.Split('-');
                    var idRegistro = Convert.ToInt32(registroIndi[0]);
                    var cups = registroIndi[1];
                    var idFactura = Convert.ToInt32(registroIndi[2]);

                    log_fis_rips_cargue_registrosCompletos_eliminar log = new log_fis_rips_cargue_registrosCompletos_eliminar();
                    log.id_factura = idFactura;
                    log.id_registro = idRegistro;
                    log.cod_cups = cups;
                    log.fecha_digita = DateTime.Now;
                    log.usuario_digita = SesionVar.UserName;

                    var insertarLog = BusClass.InsertarLogEliminaRegistroFis(log);

                    var elimina = BusClass.EliminarRegistroFis(idRegistro);
                    if (elimina == 0)
                    {
                        throw new Exception("El registro con id " + idRegistro + " no se pudo eliminar");
                    }
                }

                management_fis_facturasCuv_tableroEliminar_conteoResult datoConteoFinal = BusClass.DevolverConteoFacturas(idFac, numFactura);

                if (datoConteo != null && datoConteoFinal == null)
                {
                    BusClass.ActualizarEstadoFacturaFis(datoConteo.id_factura, 1);
                }

                mensaje = "REGISTROS ELIMINADOS CORRECTAMENTE";
                rta = 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }
            return Json(new { rta = rta, mensaje = mensaje });

        }

        public JsonResult EliminarRegistroUnico(int? idRegistro, int? idFactura, string cups)
        {
            var mensaje = "";
            var rta = 0;

            log_fis_rips_cargue_registrosCompletos_eliminar log = new log_fis_rips_cargue_registrosCompletos_eliminar();

            try
            {
                log.id_factura = idFactura;
                log.id_registro = idRegistro;
                log.cod_cups = cups;
                log.fecha_digita = DateTime.Now;
                log.usuario_digita = SesionVar.UserName;
                var insertarLog = BusClass.InsertarLogEliminaRegistroFis(log);

                var elimina = BusClass.EliminarRegistroFis(idRegistro);
                if (elimina == 0)
                {
                    throw new Exception("EL REGISTRO CON ID " + idRegistro + " NO SE PUDO ELIMINAR");
                }
                else
                {
                    mensaje = "REGISTRO ELIMINADO CORRECTAMENTE";
                    rta = 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return Json(new { rta = rta, mensaje = mensaje });

        }

        public PartialViewResult ModalGlosaMasivo(string seleccionados, int? idFactura)
        {
            ViewBag.seleccionados = seleccionados;
            ViewBag.idFactura = idFactura;
            ViewBag.conceptoGeneral = BusClass.ListadoConceptosGeneral();

            return PartialView();
        }

        [ValidateInput(false)]
        public JsonResult GuardarGlosaMasivo(string seleccionados, int? concepto_general, int? cantidad, int? concepto_especifico, int? concepto_aplicacion, decimal? valor_glosado, string observacion, int? idFactura)
        {
            var mensaje = "";
            var rta = 0;
            var seleccionadosDevuelva = "";
            try
            {
                var items = seleccionados.Split('|');

                //foreach (var item in items)
                foreach (var item in items)
                {
                    seleccionadosDevuelva += seleccionadosDevuelva != "" ? "|" : seleccionadosDevuelva;

                    var particion = item.Split('-');

                    seleccionadosDevuelva += particion[2];
                    seleccionadosDevuelva += "-" + particion[1];
                    var idRegistro = Convert.ToInt32(particion[1]);
                    fis_rips_cargue_registrosCompletos rips = BusClass.TraerRegistroRipsId(idRegistro);
                    fis_rips_facturas_glosa glosa = new fis_rips_facturas_glosa();

                    string restriccion = ValidarSumatoriaGlosas(idRegistro, Convert.ToInt32(particion[2]), Convert.ToInt32(particion[0]), rips.cod_cups, valor_glosado);
                    if (restriccion != "")
                    {
                        throw new Exception(restriccion);
                    }

                    glosa.id_usuario = Convert.ToInt32(particion[0]);
                    glosa.id_registroRips = Convert.ToInt32(particion[1]);
                    glosa.id_factura = Convert.ToInt32(particion[2]);
                    var idCarga = particion[3];

                    if (idCarga != "")
                    {
                        glosa.id_cargue = Convert.ToInt32(idCarga);

                    }
                    else
                    {
                        glosa.id_cargue = null;
                    }

                    glosa.tipo = Convert.ToString(particion[4]);
                    glosa.codigo_prestador = rips.codigo_prestador;
                    glosa.codCups = rips.cod_cups;
                    glosa.codigo_cuv = rips.cod_cuv;
                    glosa.concepto_general = concepto_general;
                    glosa.concepto_especifico = concepto_especifico;
                    glosa.concepto_aplicacion = concepto_aplicacion;
                    glosa.valor_glosado = valor_glosado;
                    glosa.cantidad = cantidad;
                    glosa.cantidadMaxima = cantidad;
                    glosa.observacion = observacion;
                    glosa.fecha_digita = DateTime.Now;
                    glosa.usuario_digita = SesionVar.UserName;
                    glosa.tipo_ingreso = 3; // Masivo
                    glosa.en_prestadores = 1;
                    glosa.estado = 1;

                    var inserta = 0;
                    inserta = BusClass.InsertarGlosaFacturaFis(glosa);

                    if (inserta != 0)
                    {
                        seleccionadosDevuelva += "-" + inserta;

                        mensaje = "GLOSA INGRESADA CORRECTAMENTE";
                        rta = 1;
                    }
                    else
                    {
                        throw new Exception("ERROR EN EL INGRESO DE LA GLOSA CON CÓDIGO CUPS: " + rips.cod_cups);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
                rta = 0;
            }

            return Json(new { mensaje = mensaje, rta = rta, seleccionadosDevuelva = seleccionadosDevuelva });
        }

        public PartialViewResult ModalEditarIva(string seleccionados, int? idFactura)
        {
            ViewBag.seleccionados = seleccionados;
            ViewBag.idFactura = idFactura;
            return PartialView();
        }

        public JsonResult ActualizarIvasMasivo(string tipoIva, string seleccionados, int? idFactura)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var items = seleccionados.Split('|');

                foreach (var item in items)
                {
                    var particion = item.Split('-');
                    var idRegistro = Convert.ToInt32(particion[1]);

                    var iva = (tipoIva == "LA" || tipoIva == "LB") ? 19 : 0;

                    var actualiza = BusClass.ActualizarTipoIva(idRegistro, tipoIva, iva);

                    if (actualiza == 0)
                    {
                        throw new Exception("ERROR AL ACTUALIZAR IVA DEL DETALLE: " + idRegistro);
                    }
                    else
                    {
                        mensaje = "IVA ACTUALIZADO CORRECTAMENTE";
                        rta = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
                rta = 0;
            }

            var listadoCups = BusClass.TraerListadoCupsFacturas(idFactura);
            Session["ListadoCupsCompleto"] = listadoCups;

            var listadoGlosas = BusClass.ListaGlosas(idFactura);
            Session["ListadoGlosasCompleto"] = listadoGlosas;

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public PartialViewResult ModalEditarTigas(string seleccionados, int? idFactura)
        {
            ViewBag.seleccionados = seleccionados;
            ViewBag.idFactura = idFactura;

            return PartialView();
        }

        public JsonResult ActualizarTigaMasivo(string seleccionados, string tiga, string descripcion_tiga, int? idFactura)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var items = seleccionados.Split('|');

                foreach (var item in items)
                {
                    var particion = item.Split('-');
                    var idRegistro = Convert.ToInt32(particion[1]);

                    fis_rips_cargue_registrosCompletos obj = new fis_rips_cargue_registrosCompletos()
                    {
                        codigo_tiga = tiga,
                        descripcion_tiga = descripcion_tiga,
                        id_registro = idRegistro,
                    };

                    var actualiza = BusClass.ActualizarTigaRipsCompletos(obj);

                    if (actualiza == 0)
                    {
                        throw new Exception("ERROR AL ACTUALIZAR TIGA DEL DETALLE: " + idRegistro);
                    }
                    else
                    {
                        mensaje = "TIGA ACTUALIZADO CORRECTAMENTE";
                        rta = 1;
                    }
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            var listadoCups = BusClass.TraerListadoCupsFacturas(idFactura);
            Session["ListadoCupsCompleto"] = listadoCups;

            var listadoGlosas = BusClass.ListaGlosas(idFactura);
            Session["ListadoGlosasCompleto"] = listadoGlosas;

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public string ValidarSumatoriaGlosas(int? idRegistro, int? idFactura, int? idUsuario, string cups, decimal? valorNuevo)
        {
            string mensaje = "";

            try
            {
                management_fisFacturas_sumaGlosasValidacionResult valor = BusClass.BuscarSumatoriaGlosas(idRegistro, idFactura, idUsuario, cups, valorNuevo);
                if (valor != null)
                {
                    if (valor.ValorNetoConIVA < valor.vlrGlosadoConNvaGlosa)
                    {
                        var monedaCol = new CultureInfo("es-CO");
                        mensaje = $"La sumatoría de las glosas para el CUPS {valor.codCups} es: {valor.valor_glosado}, con la nueva glosa es: {valor.vlrGlosadoConNvaGlosa}, lo cual no puede superar el valor inicial: {valor.ValorNetoConIVA}";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return mensaje;
        }

        public PartialViewResult EditarCIE10(int? idRegistro, int? idFactura, string cie10, string descripcioncie10, string cie10relacionado,
            string descripcioncie10relacionado)
        {
            ViewBag.idRegistro = idRegistro;
            ViewBag.idFactura = idFactura;
            ViewBag.cie10 = cie10;
            ViewBag.descripcioncie10 = descripcioncie10;
            ViewBag.cie10relacionado = cie10relacionado;
            ViewBag.descripcioncie10relacionado = descripcioncie10relacionado;

            return PartialView();
        }

        public JsonResult GuardarEdicionCIE10(int? idRegistro, int? idFactura, string cie10, string descripcioncie10, string cie10relacionado,
            string descripcioncie10relacionado)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                fis_rips_cargue_registrosCompletos com = new fis_rips_cargue_registrosCompletos()
                {
                    id_factura = idFactura,
                    id_registro = (int)idRegistro,
                    cie10 = cie10,
                    descripcion_cie10 = descripcioncie10,
                    cie10_relacionado = cie10relacionado,
                    descripcion_cie10_relacionado = descripcioncie10relacionado
                };

                var actualiza = BusClass.ActualizarCIE10Registro(com);

                if (actualiza != 0)
                {
                    mensaje = "CIE10 ACTUALIZADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    throw new Exception("ERROR AL EDITAR EL CIE10");
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult TableroEliminarActaPortafolio()
        {
            return View();
        }

        public JsonResult EliminarActaInforme(int? idVisita, int? acta, int? informe)
        {
            var rta = 0;
            var mensaje = "";

            management_cronograma_visita_documento_idResult dato = new management_cronograma_visita_documento_idResult();

            var nombreArchivo = "";
            var idArchivo = 0;

            try
            {
                if (acta == 1)
                {
                    dato = BusClass.Getactavisita2((int)idVisita);

                    if (dato != null)
                    {
                        nombreArchivo = dato.nom_documento;
                        idArchivo = dato.id_documento_visita;
                    }

                    var elimina = BusClass.EliminarActaVisitasCronogramaId(idVisita, ref MsgRes);
                    if (elimina != 0)
                    {
                        var log = BusClass.GuardarLogEliminarActaVisitas(idVisita, idArchivo, nombreArchivo, SesionVar.UserName);

                        mensaje = "ACTA ELIMINADA CORRECTAMENTE";
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "ERROR AL ELIMINAR EL ACTA: " + MsgRes.DescriptionResponse;
                    }

                }
                if (informe == 1)
                {
                    var elimina = BusClass.EliminarInformeOperativo(idVisita, ref MsgRes);

                    if (elimina != 0)
                    {
                        mensaje = "INFORME OPERATIVO ELIMINADO CORRECTAMENTE";
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "ERROR AL ELIMINAR EL INFORME OPERATIVO: " + MsgRes.DescriptionResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });

        }

        public JsonResult MasivoReaporbacioncartasAprobacion()
        {
            var rta = 0;
            var mensaje = "";
            List<management_fis_facturasActuaRutaAprobadoResult> listado = new List<management_fis_facturasActuaRutaAprobadoResult>();

            try
            {
                listado = BusClass.listadoFacturasActualizarAprobadas();
                if (listado.Count() > 0)
                {
                    var conteo = 0;
                    foreach (var item in listado)
                    {
                        conteo++;

                        int? idFactura = item.id_factura;

                        var insertaRegistro = InsertarRegistroAprobadas(idFactura);

                        if (!insertaRegistro.Contains("ERROR"))
                        {
                            string insertaRuta = IngresarRutaAprobadas(idFactura);
                            if (!insertaRuta.Contains("ERROR"))
                            {
                                mensaje = "Rutas cargadas correctamente";
                            }
                            else
                            {
                                throw new Exception(insertaRuta);
                            }
                        }
                        else
                        {
                            throw new Exception(insertaRegistro);
                        }
                    }
                }
                else
                {
                    mensaje = "Sin datos";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return Json(new { rta = rta, mensaje = mensaje });

        }

        public string InsertarRegistroAprobadas(int? idFactura)
        {
            string mensaje = "";

            try
            {
                ecop_gestion_factura_digital obj2 = new ecop_gestion_factura_digital();

                obj2.id_cargue_dtll = idFactura;
                obj2.gasto = null;
                obj2.factura_autorizada = "NO";
                obj2.fecha_digita = Convert.ToDateTime(DateTime.Now);
                obj2.usuario_digita = Convert.ToString(SesionVar.UserName);
                obj2.aplica_auditoria = true;

                var inserta = BusClass.InsertarGestionFacturadigital(obj2, ref MsgRes);
                if (inserta != 0)
                {
                    mensaje = "CORRECTO";
                }
                else
                {
                    throw new Exception("INCORRECTO");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return mensaje;
        }

        public string IngresarRutaAprobadas(int? ID)
        {
            string mensaje = "";

            try
            {
                var model = new Models.Reportes.Reportes();
                var usuariosModel = new Models.InicioSesion.Usuarios();

                string reportPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptFacturaDigital_fis.rdlc");
                string rutaDocumentosAprobados = ConfigurationManager.AppSettings["rutaDocumentosAprobados"];

                var facturas = model.GetFacturasByEstadoReporte_fis(ID.Value, ref MsgRes);

                var factura = facturas.First();
                var idBase = model.AgregarValor(factura.id_cargue_base.ToString()) + factura.id_cargue_base;
                var idDetalle = factura.id_cargue_dtll.ToString();
                var valorEncriptado = BitConverter.ToString(RsaFileDemo.LaunchDemo(idDetalle, idDetalle)).Replace("-", "");

                var cadena = string.Join(",", factura.num_factura, factura.valor_neto, factura.num_id_prestador, idDetalle, factura.valorNotaCredito, factura.valor_total_factura_conGlosa, valorEncriptado);
                var analista = factura.id_analista_gestiona;

                var firmaAnalista = model.GetFirmasId(analista);

                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                string imagenQr;
                using (var memoryStream = new MemoryStream())
                {
                    qrCode.GetGraphic(20).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    imagenQr = Convert.ToBase64String(memoryStream.ToArray());
                }

                var auditor = factura.id_auditor_asignado;
                var firmaAuditor = model.GetFirmasId(auditor);

                var viewer = new ReportViewer
                {
                    ProcessingMode = ProcessingMode.Local,
                    LocalReport =
                        {
                            ReportPath = reportPath
                        }
                };

                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(new ReportDataSource("FirmasFisDatSet", facturas));
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.SetParameters(new[]
                {
                    new ReportParameter("Imagen", @"file:///" + firmaAnalista.ruta),
                    new ReportParameter("Imagen2", imagenQr),
                    new ReportParameter("Imagen3", @"file:///" + firmaAuditor.ruta),
                });

                viewer.LocalReport.Refresh();

                var pdfContent = viewer.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streams, out Microsoft.Reporting.WebForms.Warning[] warnings);

                var subCarpeta = ConfigurationManager.AppSettings["BDActiva"] == "1" ? "Facturacion" : "FacturacionPruebas";
                var ruta = Path.Combine(Request.PhysicalApplicationPath, rutaDocumentosAprobados, "FacturasAprobadas", subCarpeta, ID.Value.ToString());
                var filename = $"{ruta}\\{DateTime.Now:yyyyMMdd_hhmm}_Aprobada{ID.Value}.pdf";

                Directory.CreateDirectory(ruta);
                System.IO.File.WriteAllBytes(filename, pdfContent);

                var facturaAprobada = new ecop_gestion_facturas_aprobadas
                {
                    id_cargue_dtll = ID.Value,
                    ruta = filename,
                    fecha_ingreso = DateTime.Now
                };

                var resultadoInsercion = model.InsertarFacturaAprobadas(facturaAprobada, ref MsgRes);
                if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    throw new Exception(MsgRes.DescriptionResponse);
                }
                else
                {
                    mensaje = "CORRECTO";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return mensaje;
        }

        public ActionResult CreacionNovedadesFactura(int? idNovedad)
        {
            factura_novedades obj = new factura_novedades();

            if (idNovedad != null && idNovedad != 0)
            {
                obj = BusClass.BuscarNovedadId(idNovedad);
            }

            ViewBag.tipoNovedad = BusClass.TiposNovedadesFacturas();
            return View(obj);
        }

        public JsonResult GuardarNovedadFactura(int? idNovedad, int? idFactura, int? novedad, string observacion)
        {
            var rta = 0;
            var mensaje = "";
            var idGestion = 0;
            var actualiza = 0;

            try
            {

                ManagementPrestadoresFacturasByIdDtllResult existeFac = BusClass.GetInfoFacturaById((int)idFactura);

                if (existeFac == null)
                {
                    throw new Exception("No existe factura con este id");
                }

                factura_novedades obj = new factura_novedades()
                {
                    id_factura = idFactura,
                    novedad = novedad,
                    observacion = observacion,
                    estado = 1,
                    fecha_digita = DateTime.Now,
                    usuario_digita = SesionVar.UserName
                };

                if (idNovedad != 0 && idNovedad != null)
                {
                    obj.id_novedad = (int)idNovedad;
                    idGestion = BusClass.ActualizarNovedadFactura(obj);
                    actualiza = 1;
                }
                else
                {
                    idGestion = BusClass.InsertarNovedadFactura(obj);
                }

                if (idGestion != 0)
                {
                    rta = 1;
                    if (actualiza == 1)
                    {

                        mensaje = "NOVEDAD ACTUALIZADA CORRECTAMENTE";
                    }
                    else
                    {
                        mensaje = "NOVEDAD INGRESADA CORRECTAMENTE";
                    }
                }
                else
                {
                    mensaje = "ERROR AL GESTIONAR LA NOVEDAD DE FACTURA";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = $"ERROR AL GESTIONAR LA NOVEDAD DE FACTURA: {error}";
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult EliminarNovedadFaC(int? id)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var elimina = BusClass.EliminarNovedadFactura(id);

                if (elimina != 0)
                {
                    mensaje = "NOVEDAD ELIMINADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR LA NOVEDAD";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = $"ERROR AL ELIMINAR LA NOVEDAD: {error}";
            }
            return Json(new { mensaje = mensaje, rta = rta });

        }

        public ActionResult TableroNovedadesFacturs(int? idFactura, int? novedad)
        {
            List<management_facturasNovedades_buscarResult> listado = BusClass.TraerDatosNovedadesFacturas(idFactura, novedad);
            ViewBag.tipoNovedad = BusClass.TiposNovedadesFacturas();

            ViewBag.listado = listado;
            ViewBag.conteoList = listado.Count();

            Session["ListadoNovedades"] = listado;
            return View();
        }

        public void ExportarDatosNovedadesFacturas()
        {
            List<management_facturasNovedades_buscarResult> Lista = (List<management_facturasNovedades_buscarResult>)Session["ListadoNovedades"];
            try
            {
                ExcelPackage Ep = new ExcelPackage();

                if (Lista == null || Lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('SIN DATOS');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ListadoNovedades");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:F1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:F1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:F1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:F1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id novedad";
                    Sheet.Cells["B1"].Value = "Id factura";
                    Sheet.Cells["C1"].Value = "Tipo novedad";
                    Sheet.Cells["D1"].Value = "Observación";
                    Sheet.Cells["E1"].Value = "Fecha creación";
                    Sheet.Cells["F1"].Value = "Usuario creación";

                    int row = 2;

                    foreach (management_facturasNovedades_buscarResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":W" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_novedad;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.id_factura;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.descripcionNovedad;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.observacion;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_digita;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.nombreDigita;

                        Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    Sheet.Cells["A:F"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ListadoNovedadFacturas_" + DateTime.Now + ".xlsx");

                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
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

        public ActionResult ActualizarContable()
        {
            return View();
        }

        public JsonResult EditarDocPedidoFactura(int? idFactura, string documentoConta, DateTime? fechaConta, string numPedido, DateTime? fechaPedido)
        {
            var rta = 0;
            var mensaje = "";
            try
            {
                ManagementPrestadoresFacturasByIdDtllResult existeFac = BusClass.GetInfoFacturaById((int)idFactura);

                if (existeFac == null)
                {
                    throw new Exception("No existe factura con este id");
                }
                else
                {
                    var actualiza = BusClass.ActualizarContabilizadoPedidoFactura(idFactura, documentoConta, fechaConta, numPedido, fechaPedido);
                    if (actualiza != 0)
                    {
                        mensaje = "DATOS ACTUALIZADOS CORRECTAMENTE";
                        rta = 1;
                    }
                    else
                    {
                        throw new Exception("ERROR AL ACTUALIZAR DATOS");
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

    }
}




