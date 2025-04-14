using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Configuration;
using System.Data.Linq;
using Facede;
using System.Net.Mime;
using System.Text;
using System.Web;
using Lucene.Net.Sandbox.Queries;
using LinqToExcel;
using System.Text.RegularExpressions;
using System.Globalization;
using Aspose.Cells;
using AsaludEcopetrol.Models;
using AsaludEcopetrol.Models.PQRS;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Xml.Serialization;
using RestSharp.Extensions;
using QiHe.CodeLib;
using System.Reflection;
using OpenMcdf;
using System.Net.Configuration;
using System.Dynamic;
using System.Runtime.Caching;

namespace AsaludEcopetrol.Controllers.PQRS
{
    [SessionExpireFilter]

    public class PqrsController : Controller
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

        public class CustomPostedFile : HttpPostedFileBase
        {
            private readonly byte[] fileBytes;
            MemoryStream stream;
            public CustomPostedFile(byte[] fileBytes, string filename = null)
            {
                this.fileBytes = fileBytes;
                this.FileName = filename;
                this.ContentType = "application/octet-stream";
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

        // GET: Pqrs/Create
        public ActionResult Create()
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            List<Ref_regional> regional = new List<Ref_regional>();
            try
            {
                regional = BusClass.GetRefRegion();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.regional = regional;

            return View(Model);
        }

        public string ObtenerCiudades(int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();
            try
            {
                List<Ref_ciudades> Ciudades = BusClass.GetCiudades();
                Ciudades = Ciudades.Where(x => x.id_ref_regional == regional).ToList();

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

        public string ObtenerSolucionador(int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            List<management_ref_solucionadorResult> list = new List<management_ref_solucionadorResult>();
            List<management_ref_solucionadorResult> listFiltrada = new List<management_ref_solucionadorResult>();
            var nombreUsuario = SesionVar.NombreUsuario;
            var tipo = 0;
            var id = 0;
            var nombreSolucionador = "";

            try
            {
                list = BusClass.getSolucionadorRegActivos((int)regional);
                listFiltrada = list.Where(X => X.nombre_solucionador == nombreUsuario).Take(1).ToList();

                if (listFiltrada.Count() == 0)
                {
                    foreach (var item in list)
                    {
                        result += "<option value='" + item.nombre_solucionador + "'>" + item.nombre_solucionador + "</option>";
                        tipo = 1;
                    }
                }
                else
                {
                    foreach (var item in listFiltrada)
                    {
                        nombreSolucionador = item.nombre_solucionador;
                        result += "<option selected='" + item.id_solucionador + "' value='" + item.nombre_solucionador + "'>" + item.nombre_solucionador + "</option>";
                        tipo = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result + "|" + tipo + "|" + nombreSolucionador;
        }

        public string ObtenerAuxSolucionador(string solucionador)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            List<ref_solucionador> list = new List<ref_solucionador>();
            List<ref_solucionador> listFiltrada = new List<ref_solucionador>();

            try
            {
                if (solucionador != null && solucionador != "")
                {
                    list = BusClass.getSolucionadorTotal();
                    list = list.Where(X => (X.auxiliar_solucionador != "" && X.auxiliar_solucionador != null && X.nombre_solucionador == solucionador) || X.nombre_solucionador == "OTRO").ToList();

                    foreach (var item in list)
                    {
                        result += "<option value='" + item.auxiliar_solucionador + "'>" + item.auxiliar_solucionador + "</option>";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public ActionResult RefrescarTableroControlPqrs()
        {
            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            List<management_pqrs_tablero_controlResult> ListadoDePqrs = cache["ListadoDeCachePqrs_" + nomUsuario] as List<management_pqrs_tablero_controlResult>;
            if (ListadoDePqrs.Count > 0)
            {
                cache.Remove("ListadoDeCachePqrs_" + nomUsuario);
            }

            return RedirectToAction("TableroControl", "Pqrs");
        }

        public ActionResult TableroControl(int? idCategoria, int? estado, int? estado2, int? tipoBusqueda, string numero_caso)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            List<Ref_PQRS_categorizacion> categorias = new List<Ref_PQRS_categorizacion>();

            string nomUsuario = SesionVar.UserName;
            ObjectCache cache = MemoryCache.Default;
            List<management_pqrs_tablero_controlResult> ListadoDePqrsCache = cache["ListadoDeCachePqrs_" + nomUsuario] as List<management_pqrs_tablero_controlResult>;
            List<management_pqrs_tablero_controlResult> listaGeneralDatos = new List<management_pqrs_tablero_controlResult>();

            var categoria = 0;
            var estadoFin = 0;
            var estadoFin2 = 0;
            try
            {
                if (idCategoria != null) 
                {
                    categoria = (int)idCategoria;
                }
                Session["idCategoria"] = categoria;

                if (estado != null)
                {
                    estadoFin = (int)estado;
                }

                Session["estado"] = estadoFin;

                if (estado2 != null)
                {
                    estadoFin2 = (int)estado2;
                }

                Session["estado2"] = estadoFin2;
                Session["numeroCasoFiltro"] = numero_caso;
                categorias = BusClass.ConsultaPQRSCategorizacion();

                var estadosPermitidos = new int?[] { 1, 2, 4, 5, 6 };

                if (ListadoDePqrsCache == null || ListadoDePqrsCache.Count() == 0)
                {
                    listaGeneralDatos = BusClass.GestiontableroPQRS().Where(x => estadosPermitidos.Contains(x.estado_gestion)).ToList();

                    /*Se definen las politicas y se insertan en cache los resultados de la consulta hecha a base de datos*/
                    DateTime expirationDate = DateTime.Now.AddHours(1);
                    CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = expirationDate };

                    cache.Set("ListadoDeCachePqrs_" + nomUsuario, listaGeneralDatos, policy);
                    cache.Set("tiempoExpiracionAnalistaPqrs_" + nomUsuario, expirationDate, policy);
                }
                else
                {
                    listaGeneralDatos = ListadoDePqrsCache;
                }

                Session["listaGeneralDatos"] = listaGeneralDatos;
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.categorias = categorias;
            ViewBag.tipo = tipoBusqueda;

            return View(Model);
        }

        public PartialViewResult _TableroControl()
        {
            try
            {
                var rolActual = SesionVar.ROL;
                var idUsuarioActual = SesionVar.IDUser;

                Models.PQRS.GestionPqrs model = new Models.PQRS.GestionPqrs
                {
                    Habilitar = new[] { "1", "10", "13", "32" }.Contains(rolActual) ? "SI" : "NO"
                };

                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;

                //var estadosPermitidos = new int?[] { 1, 2, 4, 5, 6 };
                List<management_pqrs_tablero_controlResult> listaGeneralDatos = (List<management_pqrs_tablero_controlResult>)Session["listaGeneralDatos"];

                var rolesPermitidos = new[] { "1", "2", "10", "29", "32" };
                List<management_pqrs_tablero_controlResult> listaFinal = rolesPermitidos.Contains(SesionVar.ROL)
                    ? listaGeneralDatos
                    : listaGeneralDatos.Where(x => x.usuario == SesionVar.UserName).ToList();

                if (SesionVar.ROL == "2")
                {
                    List<management_pqrs_tablero_controlResult> listTemporal = new List<management_pqrs_tablero_controlResult>();

                    List<sis_auditor_regional_pqrs> listReg = new List<sis_auditor_regional_pqrs>();
                    listReg = BusClass.listadoRegionalesUsuarioPqrs(SesionVar.IDUser);

                    if (listReg.Count() > 0)
                    {
                        foreach (var item in listReg)
                        {
                            List<management_pqrs_tablero_controlResult> listTemporal2 = new List<management_pqrs_tablero_controlResult>();
                            listTemporal2 = listaFinal.Where(x => x.regional == Convert.ToInt32(item.id_regional)).ToList();

                            if (listTemporal2.Count > 0)
                            {
                                listTemporal.AddRange(listTemporal2);
                            }
                        }
                    }

                    if (listTemporal.Count() > 0)
                    {
                        listaFinal = listTemporal;
                    }
                }

                var categoria = (int)Session["idCategoria"];
                if (categoria != 0)
                {
                    listaFinal = categoria == 20
                        ? listaFinal.Where(x => x.supersalud == 1).ToList()
                        : listaFinal.Where(x => x.id_ref_categorizacon == categoria).ToList();
                }

                var filtros = new Dictionary<string, Func<management_pqrs_tablero_controlResult, bool>>
                {
                    { "Vencidas", x => ((x.dias_ok_salfe > 10 || x.dias_ok_salfe_egreso > 1 ) || x.dias_ok_salfe_egreso > 1) && x.estado_gestion != 4 },
                    { "Alerta", x => x.estado_gestion != 4 && (x.dias_ok_salfe == 9 || x.dias_ok_salfe == 10) && x.dias_ok_salfe_egreso <= 1},
                    { "Priorizar", x => x.dias_ok_salfe >= 3 && x.dias_ok_salfe <= 8 && x.estado_gestion != 4 && x.dias_ok_salfe_egreso <= 1},
                    { "Tiempos", x => x.dias_ok_salfe < 3 && x.estado_gestion != 4 && x.dias_ok_salfe_egreso <= 1 },

                    { "AmpliacionVen", x => x.semaforo_ok_salfe == "4" },
                    { "Ampliacion", x => x.semaforo_ok_salfe == "5" },
                    { "SuperSalud", x => x.supersalud == 1 },
                    { "Reabiertos", x => x.reabierto == 1 },
                    { "Devueltocierre", x => x.devuelto_en_cierre == 1 }
                };

                var conteoFiltradas = filtros.ToDictionary(
                    filtro => filtro.Key,
                    filtro => listaFinal.Where(filtro.Value).Count()
                );

                var listasFiltradas = filtros.ToDictionary(
                    filtro => filtro.Key,
                    filtro => listaFinal.Where(filtro.Value).ToList()
                );

                ViewBag.filtros = filtros;
                ViewBag.conteoFiltradas = conteoFiltradas;

                ViewBag.lista = listaFinal;
                ViewBag.Total = listaFinal.Count();

                var estado = (int)Session["estado"];
                if (estado != 0)
                {
                    var estados = new[]
                    {
                        new { Key = 1, Lista = listaFinal },
                        new { Key = 2, Lista = listasFiltradas["SuperSalud"] },
                        new { Key = 3, Lista = listasFiltradas["Vencidas"] },
                        new { Key = 4, Lista = listasFiltradas["Alerta"] },
                        new { Key = 5, Lista = listasFiltradas["Priorizar"] },
                        new { Key = 6, Lista = listasFiltradas["Tiempos"] },
                        new { Key = 7, Lista = listasFiltradas["Ampliacion"] },
                        new { Key = 8, Lista = listasFiltradas["AmpliacionVen"] },
                        new { Key = 9, Lista = listasFiltradas["Reabiertos"] },
                        new { Key = 10, Lista = listasFiltradas["Devueltocierre"] }
                    };

                    listaFinal = estados.FirstOrDefault(e => e.Key == estado)?.Lista ?? listaFinal;
                }

                var numeroCaso = Convert.ToString(Session["numeroCasoFiltro"]);
                if (!string.IsNullOrEmpty(numeroCaso))
                {
                    listaFinal = listaFinal.Where(x => x.numero_caso.ToUpper().Contains(numeroCaso.ToUpper())).ToList();
                }

                Session["tableroControlPqrs"] = listaFinal;

                var estadosGestion = new Dictionary<string, Func<management_pqrs_tablero_controlResult, bool>>
                {
                    { "listaRevision", x => x.estado_gestion == 5 && (x.vobo_auditor == "SI" || x.analisis_caso == "SI") },
                    { "listaSinrevision", x => x.estado_gestion != 5},
                    { "listaRespuestSi", x => x.respuestaAuditor == "SI" },
                    { "listaRespuestNo", x => x.respuestaAuditor == "NO" && x.ingreso_inicial == null}
                };

                var filtrado = new Dictionary<string, int?>();

                foreach (var estadoGestion in estadosGestion)
                {
                    var listaEstado = listaFinal.Where(estadoGestion.Value).ToList();
                    Session[estadoGestion.Key] = listaEstado;
                    filtrado.Add(estadoGestion.Key, listaEstado.Count());
                }

                ViewBag.filtrado = filtrado;

                ViewBag.categorias = BusClass.ConsultaPQRSCategorizacion();
                Session["ListaPQRS"] = new List<management_pqrs_tablero_controlResult>(); ;
                Session["ListaPQRS"] = listaFinal;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView();
        }

        public PartialViewResult _conRevision()
        {
            try
            {
                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;
                ViewBag.listaRevision = Session["listaRevision"];
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return PartialView();
        }

        public PartialViewResult _sinRevision()
        {
            try
            {
                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;
                ViewBag.listaSinRevision = Session["listaSinrevision"];
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return PartialView();
        }

        public PartialViewResult _voBoAuditor()
        {
            try
            {
                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;
                ViewBag.respuestSi = Session["listaRespuestSi"];
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return PartialView();
        }

        public PartialViewResult _voBoAuditorNo()
        {
            try
            {
                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;
                ViewBag.respuestNo = Session["listaRespuestNo"];
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return PartialView();
        }

        public PartialViewResult _TableroControlSemaforizacion()
        {
            try
            {
                var idUsuarioActual = SesionVar.IDUser;
                var rolActual = SesionVar.ROL;

                Models.PQRS.GestionPqrs model = new Models.PQRS.GestionPqrs
                {
                    Habilitar = new[] { "1", "10", "13", "32" }.Contains(rolActual) ? "SI" : "NO"
                };

                ViewBag.rol2 = SesionVar.ROL;
                ViewBag.idusuario2 = SesionVar.IDUser;

                //var estadosPermitidos = new int?[] { 1, 2, 4, 5, 6 };

                List<management_pqrs_tablero_controlResult> lista = (List<management_pqrs_tablero_controlResult>)Session["listaGeneralDatos"];

                var rolesPermitidos = new[] { "1", "2", "10", "29", "32" };
                var listaFinal = rolesPermitidos.Contains(SesionVar.ROL)
                     ? lista
                     : lista.Where(x => x.usuario == SesionVar.UserName).ToList();

                if (SesionVar.ROL == "2")
                {
                    List<management_pqrs_tablero_controlResult> listTemporal = new List<management_pqrs_tablero_controlResult>();

                    List<sis_auditor_regional> listReg = new List<sis_auditor_regional>();
                    listReg = BusClass.listadoRegionalesUsuario(SesionVar.IDUser);

                    if (listReg.Count() > 0)
                    {
                        foreach (var item in listReg)
                        {
                            List<management_pqrs_tablero_controlResult> listTemporal2 = new List<management_pqrs_tablero_controlResult>();
                            listTemporal2 = listaFinal.Where(x => x.regional == Convert.ToInt32(item.id_regional)).ToList();

                            if (listTemporal2.Count > 0)
                            {
                                listTemporal.AddRange(listTemporal2);
                            }
                        }
                    }

                    if (listTemporal.Count() > 0)
                    {
                        listaFinal = listTemporal;
                    }
                }

                var categoria = (int)Session["idCategoria"];
                if (categoria != 0)
                {
                    listaFinal = categoria == 20
                        ? listaFinal.Where(x => x.supersalud == 1).ToList()
                        : listaFinal.Where(x => x.id_ref_categorizacon == categoria).ToList();
                }

                var filtros = new Dictionary<string, Func<management_pqrs_tablero_controlResult, bool>>
                {
                    { "Vencidas2",x => x.dias_ok_salfe_egreso > 1 && x.estado_gestion != 4 },
                    { "Priorizar2", x => x.dias_ok_salfe_egreso <= 1 && x.estado_gestion != 4 },
                    { "AmpliacionVen2",x => x.semaforo_ok_salfe == "4" },
                    { "Ampliacion2", x => x.semaforo_ok_salfe == "5" },
                    { "SuperSalud2", x => x.supersalud == 1 },
                    { "Reabiertos2", x => x.reabierto == 1 },
                    { "Devueltocierre2", x => x.devuelto_en_cierre == 1 }
                };

                var conteoFiltradas = filtros.ToDictionary(
                    filtro => filtro.Key,
                    filtro => listaFinal.Where(filtro.Value).Count()
                );

                var listasFiltradas = filtros.ToDictionary(
                    filtro => filtro.Key,
                    filtro => listaFinal.Where(filtro.Value).ToList()
                );

                ViewBag.filtros2 = filtros;
                ViewBag.conteoFiltradas2 = conteoFiltradas;

                ViewBag.lista2 = listaFinal.ToList();
                ViewBag.Total2 = listaFinal.Count();

                var estado = (int)Session["estado2"];
                if (estado != 0)
                {
                    var estados = new[]
                    {
                        new { Key = 1, Lista = listaFinal },
                        new { Key = 2, Lista = listasFiltradas["SuperSalud2"] },
                        new { Key = 3, Lista = listasFiltradas["Vencidas2"] },
                        new { Key = 5, Lista = listasFiltradas["Priorizar2"] },
                        new { Key = 7, Lista = listasFiltradas["Ampliacion2"] },
                        new { Key = 8, Lista = listasFiltradas["AmpliacionVen2"] },
                        new { Key = 9, Lista = listasFiltradas["Reabiertos2"] },
                        new { Key = 10, Lista = listasFiltradas["Devueltocierre2"] }
                    };

                    listaFinal = estados.FirstOrDefault(e => e.Key == estado)?.Lista ?? listaFinal;
                }

                var numeroCaso = Convert.ToString(Session["numeroCasoFiltro"]);
                if (!string.IsNullOrEmpty(numeroCaso))
                {
                    listaFinal = listaFinal.Where(x => x.numero_caso.ToUpper().Contains(numeroCaso.ToUpper())).ToList();
                }

                Session["tableroControlPqrsSemaforo"] = listaFinal.ToList();
                var estadosGestion = new Dictionary<string, Func<management_pqrs_tablero_controlResult, bool>>
                {
                    { "listaRevision2", x => x.estado_gestion == 5 && (x.vobo_auditor == "SI" || x.analisis_caso == "SI") },
                    { "listaSinrevision2", x => x.estado_gestion != 5 },
                    { "listaRespuestSi2", x => x.respuestaAuditor == "SI" },
                    { "listaRespuestNo2", x => x.respuestaAuditor == "NO" }
                };

                var filtrado = new Dictionary<string, int?>();

                foreach (var estadoGestion in estadosGestion)
                {
                    var listaEstado = listaFinal.Where(estadoGestion.Value).ToList();
                    Session[estadoGestion.Key] = listaEstado;
                    filtrado.Add(estadoGestion.Key, listaEstado.Count());
                }

                ViewBag.filtrado2 = filtrado;
                Session["ListaPQRS2"] = new List<management_pqrs_tablero_controlResult>();
                Session["ListaPQRS2"] = listaFinal.ToList();

                ViewBag.categorias2 = BusClass.ConsultaPQRSCategorizacion();
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView();
        }

        public PartialViewResult _conRevision2()
        {
            try
            {
                ViewBag.rol2 = SesionVar.ROL;
                ViewBag.idusuario2 = SesionVar.IDUser;
                ViewBag.listaRevision2 = Session["listaRevision2"];

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return PartialView();
        }

        public PartialViewResult _sinRevision2()
        {
            try
            {
                ViewBag.rol2 = SesionVar.ROL;
                ViewBag.idusuario2 = SesionVar.IDUser;
                ViewBag.listaSinRevision2 = Session["listaSinRevision2"];
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return PartialView();
        }

        public PartialViewResult _voBoAuditor2()
        {
            try
            {
                ViewBag.rol2 = SesionVar.ROL;
                ViewBag.idusuario2 = SesionVar.IDUser;
                ViewBag.listaRespuestSi2 = Session["listaRespuestSi2"];
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return PartialView();
        }

        public PartialViewResult _voBoAuditorNo2()
        {
            try
            {
                ViewBag.rol2 = SesionVar.ROL;
                ViewBag.idusuario2 = SesionVar.IDUser;
                ViewBag.listarespuestNo2 = Session["listarespuestNo2"];
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return PartialView();
        }

        //Nuevo modulo para auditores
        //Se ven todos los pqrs de su regional

        public ActionResult TableroAuditoresControl(int? idCategoria)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            List<Ref_PQRS_categorizacion> categorias = new List<Ref_PQRS_categorizacion>();
            try
            {
                if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "15" || SesionVar.ROL == "32")
                {
                    Model.Habilitar = "SI";
                }
                else
                {
                    Model.Habilitar = "NO";
                }

                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;
                //Model.ConsultaTotales();

                List<management_pqrs_tablero_controlResult> lista = new List<management_pqrs_tablero_controlResult>();
                List<management_pqrs_tablero_controlResult> listaFinal = new List<management_pqrs_tablero_controlResult>();

                lista = Model.GestiontableroPQRS();
                lista = lista.Where(x => x.estado_gestion == 1 || x.estado_gestion == 2 || x.estado_gestion == 4 || x.estado_gestion == 5 || x.estado_gestion == 6).ToList();

                if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "29" || SesionVar.ROL == "32")
                {
                    listaFinal = lista.ToList();
                }
                else
                {
                    listaFinal = lista.Where(x => x.usuarioAuditorAsignado.Contains(SesionVar.UserName)).ToList();
                }

                //var categoria = (int)Session["idCategoria"];

                var categoria = idCategoria;

                if (categoria != null)
                {
                    if (categoria != 0)
                    {
                        if (categoria == 20)
                        {
                            listaFinal = listaFinal.Where(x => x.supersalud == 1).ToList();
                        }
                        else
                        {
                            listaFinal = listaFinal.Where(x => x.id_ref_categorizacon == (int)categoria).ToList();
                        }
                    }
                }

                ViewBag.lista = listaFinal.ToList();
                ViewBag.Total = listaFinal.Count();

                ViewBag.vencidas = listaFinal.Where(x => x.dias_ok_salfe > 10 && x.estado_gestion != 4).ToList().Count();
                ViewBag.alerta = listaFinal.Where(x => x.dias_ok_salfe == 9 || x.dias_ok_salfe == 10 && x.estado_gestion != 4).ToList().Count();
                ViewBag.priorizar = listaFinal.Where(x => x.dias_ok_salfe >= 3 && x.dias_ok_salfe <= 8 && x.estado_gestion != 4).ToList().Count();
                ViewBag.tiempos = listaFinal.Where(x => x.dias_ok_salfe < 3 && x.estado_gestion != 4).ToList().Count();

                ViewBag.ampliacionVen = listaFinal.Where(x => x.semaforo_ok_salfe == "4").ToList().Count();
                ViewBag.ampliacion = listaFinal.Where(x => x.semaforo_ok_salfe == "5").ToList().Count();

                ViewBag.revision = listaFinal.Where(x => x.estado_gestion == 5 && (x.vobo_auditor == "SI" || x.analisis_caso == "SI")).ToList().Count();
                ViewBag.Sinrevision = listaFinal.Where(x => x.estado_gestion != 5 || (x.vobo_auditor != "SI" && x.analisis_caso != "SI")).ToList().Count();

                ViewBag.respuestSi = listaFinal.Where(x => x.respuestaAuditor == "SI").ToList().Count();
                ViewBag.respuestNo = listaFinal.Where(x => x.respuestaAuditor == "NO").ToList().Count();

                ViewBag.superSalud = listaFinal.Where(x => x.supersalud == 1).ToList().Count();

                Session["ListaPQRSAuditores"] = listaFinal.ToList();

                categorias = BusClass.ConsultaPQRSCategorizacion();

                ViewBag.categorias = categorias;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public ActionResult TableroPQRSEnrevision(Int32? ID)
        {
            Models.General General = new Models.General();
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                if (ID == 1)
                {
                    ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se ingreso el equipo exitosamente ");
                }
                else if (ID == 2)
                {
                    ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se ingreso el monitor exitosamente ");
                }
                else
                {
                    ViewData["alerta"] = "";
                }

                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;

                List<vw_ecop_PQRS_enrevision> lista = BusClass.ConsultaPQRSEnRevision().ToList();
                if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "13" || SesionVar.ROL == "32")
                {
                    lista = lista.Where(x => x.estado_revision == 1).ToList();
                }
                else
                {
                    lista = lista.Where(x => x.auditor_asignado == SesionVar.IDUser).ToList();
                    lista = lista.Where(x => x.estado_revision == 1).ToList();
                }

                ViewBag.listaenrevision = lista;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public ActionResult TableroPQRSEnrevisionAnalista(Int32? ID)
        {
            Models.General General = new Models.General();
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                if (ID == 1)
                {
                    ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se ingreso el equipo exitosamente ");
                }
                else if (ID == 2)
                {
                    ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Se ingreso el monitor exitosamente ");
                }
                else
                {
                    ViewData["alerta"] = "";
                }

                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;

                List<vw_ecop_PQRS_enrevision> lista = BusClass.ConsultaPQRSEnRevision().ToList();
                if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "13" || SesionVar.ROL == "32")
                {
                    lista = lista.Where(x => x.estado_revision == 2 || x.estado_revision == 3).ToList();
                }
                else
                {
                    List<Ref_PQRS_usuarios> usuarioanalista = BusClass.GetusuariosPQRS().ToList();
                    usuarioanalista = usuarioanalista.Where(l => l.usuario == SesionVar.UserName).ToList();

                    foreach (var item in usuarioanalista)
                    {
                        Int32? analista = item.id_ref_PQRS_usuarios;
                        lista = lista.Where(x => x.usuario_asignado == analista).ToList();
                        lista = lista.Where(x => x.estado_revision == 1).ToList();
                    }
                }

                ViewBag.listaenrevision = lista;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public ActionResult TableroControlAuditor(int? estado)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            List<management_pqrs_tableroAuditorResult> lista = new List<management_pqrs_tableroAuditorResult>();
            try
            {
                // Asignar habilitación y rol
                Model.Habilitar = (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "13" || SesionVar.ROL == "32") ? "SI" : "NO";
                ViewBag.rol = SesionVar.ROL;

                // Obtener la lista base
                lista = Model.consultaTableroAuditor(estado);
                ViewBag.Total = lista.Count();

                // Filtrar lista por estado
                lista = estado != null ? Model.FiltrarPorEstado(lista, (int)estado) : lista;

                // Calcular las estadísticas
                ViewBag.vencidas = lista.Count(x => x.dias_ok_salfe_egreso > 1 && x.estado_gestion != 4);
                ViewBag.priorizar = lista.Count(x => x.dias_ok_salfe_egreso <= 1 && x.estado_gestion != 4);
                ViewBag.ampliacionVen = lista.Count(x => x.semaforo_ok_salfe == "4");
                ViewBag.ampliacion = lista.Count(x => x.semaforo_ok_salfe == "5");
                ViewBag.superSalud = lista.Count(x => x.supersalud == 1);
                ViewBag.reabiertos = lista.Count(x => x.reabierto == 1);
                ViewBag.totalVobo = lista.Count(x => x.vobo_auditor == "SI");
                ViewBag.totalAsignacion = lista.Count(x => x.analisis_caso == "SI");
                ViewBag.totalPasan = lista.Count(x => x.pasa_auditor == "SI");

                // Asignar las listas para ViewBag
                ViewBag.listaVencidas = lista.Where(x => x.dias_ok_salfe_egreso > 1 && x.estado_gestion != 4).ToList();
                ViewBag.listaPorPriorizar = lista.Where(x => x.dias_ok_salfe_egreso <= 1 && x.estado_gestion != 4).ToList();
                ViewBag.lista = lista;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View(Model);
        }

        public ActionResult TableroControlCoordinador()
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {
                if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "13" || SesionVar.ROL == "32")
                {
                    Model.Habilitar = "SI";
                }
                else
                {
                    Model.Habilitar = "NO";
                }
                ViewBag.rol = SesionVar.ROL;
                Model.ConsultaTotalesCordinador();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View(Model);
        }

        public ActionResult TableroControlProyectadas(string numcaso, string numopc, string numdocumento, DateTime? fechainicial, DateTime? fechafinal, Int32? id, int? idPqr, int? rta, string msj)
        {
            Models.General General = new Models.General();
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            List<management_pqrs_tablero_control_proyectadasResult> lista = new List<management_pqrs_tablero_control_proyectadasResult>();

            try
            {
                if (id != null)
                {
                    ViewData["alerta"] = General.MsgRespuesta("succes", "Se abrio correctamente el caso!", "por favor revisar el tablero PQRS");
                }
                else
                {
                    ViewData["alerta"] = "";
                }

                if (!String.IsNullOrEmpty(numcaso) || !String.IsNullOrEmpty(numopc) || !String.IsNullOrEmpty(numdocumento) || fechainicial != null || fechafinal != null || idPqr != null)
                {
                    lista = Model.GestiontableroPQRSProyectadas(numcaso, numopc, numdocumento, fechainicial, fechafinal, idPqr).ToList();
                }

                //if (!String.IsNullOrEmpty(numcaso))
                //{
                //    lista = lista.Where(l => l.numero_caso.Contains(numcaso)).ToList();
                //}

                //if (!String.IsNullOrEmpty(numopc))
                //{
                //    lista = lista.Where(l => l.consecutivo_caso.Contains(numopc)).ToList();
                //}

                //if (!String.IsNullOrEmpty(numdocumento))
                //{
                //    lista = lista.Where(l => l.solicitante_numero_identi.Contains(numdocumento)).ToList();
                //}

                //if (fechainicial != null)
                //{
                //    lista = lista.Where(l => Convert.ToDateTime(l.fecha_gestion).Date != null && Convert.ToDateTime(l.fecha_gestion).Date >= Convert.ToDateTime(fechainicial).Date).ToList();
                //}

                //if (fechafinal != null)
                //{
                //    lista = lista.Where(l => Convert.ToDateTime(l.fecha_gestion).Date != null && Convert.ToDateTime(l.fecha_gestion).Date <= Convert.ToDateTime(fechafinal).Date).ToList();
                //}

                //if (idPqr != null)
                //{
                //    lista = lista.Where(x => x.id_ecop_PQRS == idPqr).ToList();
                //}

                Session["ListaPQRSP"] = lista;

                ViewBag.listaEstado = lista;
                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;
                ViewBag.numcaso = numcaso;
                ViewBag.numopc = numopc;
                ViewBag.numdocumento = numdocumento;
                ViewBag.fechainicial = fechainicial;
                ViewBag.fechafinal = fechafinal;

                ViewBag.rta = 0;
                ViewBag.msj = "";
                ViewBag.acto = lista.Count();

                if (rta != null && rta != 0)
                {
                    ViewBag.rta = rta;
                    ViewBag.msj = msj;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View(Model);
        }

        public PartialViewResult MostrarGestionesPqr(int? idPqr)
        {

            List<management_vw_ecop_PQRS_DetalleGResult> listaDetalle = new List<management_vw_ecop_PQRS_DetalleGResult>();
            List<management_pqrs_auditorListaResult> listaAuditor = new List<management_pqrs_auditorListaResult>();
            List<management_pqrs_observacionesAuditorResult> observaciones = new List<management_pqrs_observacionesAuditorResult>();
            var conteoDetalle = 0;
            var conteoAuditor = 0;
            var conteoObservaciones = 0;

            try
            {
                listaDetalle = BusClass.ConsultaPQRSDetalle2((int)idPqr);
                listaAuditor = BusClass.ListaPqrsAuditor((int)idPqr);
                observaciones = BusClass.PqrsListaObservacionesAuditor(Convert.ToInt32(idPqr));

                conteoDetalle = listaDetalle.Count();
                conteoAuditor = listaAuditor.Count();
                conteoObservaciones = observaciones.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaDetalle = listaDetalle;
            ViewBag.listaAuditor = listaAuditor;
            ViewBag.observaciones = observaciones;

            ViewBag.conteoDetalle = conteoDetalle;
            ViewBag.conteoAuditor = conteoAuditor;
            ViewBag.conteoObservaciones = conteoObservaciones;

            return PartialView();
        }

        public ActionResult GestionPQRS(String idPqrs, string msg, int? rta, int? tipo)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            ViewBag.rta = 0;
            ViewBag.msg = "";
            List<management_pqrs_observacionesAuditorResult> listaObservaciones = new List<management_pqrs_observacionesAuditorResult>();
            var conteoObservaciones = 0;
            var pasaAuditor = "";

            try
            {
                if (rta == 1 || rta == 2)
                {
                    ViewBag.msg = msg;
                    ViewBag.rta = rta;
                }

                Model.requirio_llamada = null;
                List<management_vw_ecop_PQRS_DetalleGResult> listaDtll = new List<management_vw_ecop_PQRS_DetalleGResult>();

                Model.id_ecop_PQRS = Convert.ToInt32(idPqrs);

                Model.ConsultaPQRSId(Model.id_ecop_PQRS);

                var lista = BusClass.RolCargo();

                lista = lista.Where(x => x.id_rol_cargo == 20 || x.id_rol_cargo == 10 || x.id_rol_cargo == 13 || x.id_rol_cargo == 14 || x.id_rol_cargo == 15 || x.id_rol_cargo == 17).ToList();

                listaDtll = BusClass.ConsultaPQRSDetalle2(Model.id_ecop_PQRS);
                listaDtll = listaDtll.OrderBy(x => x.fecha_gestion).ToList();

                Model.CamposGestionPqr(listaDtll, Convert.ToInt32(idPqrs));
                //CamposGestionPqr(listaDtll, Convert.ToInt32(idPqrs));

                ViewBag.cargo = lista.ToList();
                ViewBag.lista = listaDtll.ToList();
                ViewBag.count = listaDtll.Count();

                List<ecop_pqrs_prestadores> listaPrestadores = BusClass.ListadoPrestadoresIdPqrs(Model.id_ecop_PQRS);
                var listPrestadores = "";
                if (listaPrestadores.Count() > 0)
                {
                    foreach (var pres in listaPrestadores)
                    {
                        if (listPrestadores == "")
                        {
                            listPrestadores += pres.id_prestador;
                        }
                        else
                        {
                            listPrestadores += "," + pres.id_prestador;
                        }
                    }
                }

                List<ecop_pqrs_a_quien_llamo> listadoQuienLlamo = BusClass.aQuienLlamoId(Model.id_ecop_PQRS);
                var listQuienLlamo = "";
                if (listadoQuienLlamo.Count() > 0)
                {
                    foreach (var llam in listadoQuienLlamo)
                    {
                        if (listQuienLlamo == "")
                        {
                            listQuienLlamo += llam.id_tipo_llamado + "_" + llam.a_quien_llamo + "_" + llam.telefono;
                        }
                        else
                        {
                            listQuienLlamo += "," + llam.id_tipo_llamado + "_" + llam.a_quien_llamo + "_" + llam.telefono;
                        }
                    }
                }

                ViewBag.listPrestadores = listPrestadores;
                ViewBag.listQuienLlamo = listQuienLlamo;

                List<Ref_PQRS_estado_Gestion> PqrsEstadoGest = new List<Ref_PQRS_estado_Gestion>();

                PqrsEstadoGest = BusClass.GetRefPqrsGestion();
                ViewBag.listEstados = PqrsEstadoGest;

                ecop_PQRS pq = new ecop_PQRS();

                pq = BusClass.LlamarPqrsById(Convert.ToInt32(idPqrs));
                if (pq != null)
                {
                    pasaAuditor = pq.pasa_auditor;
                }

                var pasa_Auditor = 0;

                if (pasaAuditor == "SI")
                {
                    pasa_Auditor = 1;
                }
                else if (pasaAuditor == "NO")
                {
                    pasa_Auditor = 2;
                }
                else
                {
                    pasa_Auditor = 2;
                }
                ViewBag.pasaAuditor = pasa_Auditor;

                ecop_PQRS_Auditor audi = new ecop_PQRS_Auditor();
                audi = BusClass.ConsultaPQRSAuditor(Convert.ToInt32(idPqrs)).OrderByDescending(x => x.id_ecop_PQRS_auditor).FirstOrDefault();
                var decisionAudi = "";
                var decisionFinalAudi = 0;

                if (audi != null)
                {
                    decisionAudi = audi.vobo;

                    if (decisionAudi == "SI" || decisionAudi == null)
                    {
                        decisionFinalAudi = 1;
                    }
                    else
                    {
                        decisionFinalAudi = 2;
                    }
                }

                ViewBag.final = decisionFinalAudi;
                ViewBag.componentes = BusClass.GetComponentesHospitalarios();
                ViewBag.idPqrs = idPqrs;

                List<GestionDocumentalPQRS2> LsitUrl2 = new List<GestionDocumentalPQRS2>();
                LsitUrl2 = Model.GetUrlProyeccion(Model.id_ecop_PQRS).Where(x => x.id_tipo_documental == 8).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).ToList();

                if (LsitUrl2 == null || LsitUrl2.Count() == 0)
                {
                    ViewBag.existeArchivo = "NO";
                }
                else
                {
                    ViewBag.existeArchivo = "SI";
                }

                listaObservaciones = BusClass.PqrsListaObservacionesAuditor(Convert.ToInt32(idPqrs));
                conteoObservaciones = listaObservaciones.Count;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.tipo = tipo;
            ViewBag.observaciones = listaObservaciones;
            ViewBag.conteoObs = conteoObservaciones;
            ViewBag.idRol = SesionVar.ROL;

            return View(Model);
        }

        public ActionResult GestionPQRSAuditor(String idPqrs, string msg)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            ViewBag.rol = SesionVar.ROL;
            List<management_vw_ecop_PQRS_DetalleGResult> listaDtll = new List<management_vw_ecop_PQRS_DetalleGResult>();

            try
            {

                listaDtll = BusClass.ConsultaPQRSDetalle2(Convert.ToInt32(idPqrs)).OrderBy(x => x.fecha_gestion).ToList();

                ViewBag.lista = listaDtll.ToList();
                ViewBag.count = listaDtll.Count();

                ViewBag.rta = 0;
                ViewBag.msg = "";

                if (!string.IsNullOrEmpty(msg))
                {
                    ViewBag.rta = 2;
                    ViewBag.msg = msg;
                }

                Model.id_ecop_PQRS = Convert.ToInt32(idPqrs);
                Model.ConsultaPQRSId(Model.id_ecop_PQRS);

                ViewBag.pasa_auditor = Model.pasa_auditor;

                List<Ref_PQRS_Atributo> listAtributos = new List<Ref_PQRS_Atributo>();
                listAtributos = Model.listaAtributosPqrs();

                ViewBag.listAtributos = listAtributos;
                ViewBag.idPqrs = Convert.ToInt32(idPqrs);

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public ActionResult GestionPQRSCoordinador(String idPqrs)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {
                Model.id_ecop_PQRS = Convert.ToInt32(idPqrs);
                Model.ConsultaPQRSId(Model.id_ecop_PQRS);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public ActionResult GestorDocumentalPQRS()
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            return View(Model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Models.PQRS.GestionPqrs Model)
        {
            try
            {
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";
                var analista = 0;
                var idUsuario = SesionVar.IDUser;
                analista = IdAanalistaAsignado(Model.ciudad_del_caso, Model.regional);

                if (Model.horabuzon == null || Model.fecha_ingreso_buzon_asalud == null)
                {
                    variable = "ERROR";
                    variable2 = "Hora y fecha de Ingreso Buzon";
                    Conteo = Conteo + 1;
                }

                if (Model.horasalesforce == null || Model.fecha_ingreso_salesforce == null)
                {
                    variable = "ERROR";
                    variable2 = "Hora y fecha Ingreso Sales Force";
                    Conteo = Conteo + 1;
                }


                if (Model.fecha_egreso_salesforce == null)
                {
                    variable = "ERROR";
                    variable2 = "Fecha respuesta programada";
                    Conteo = Conteo + 1;
                }

                if (Model.regional == 0)
                {
                    variable = "ERROR";
                    variable2 = "REGIONAL";
                    Conteo = Conteo + 1;
                }

                if (Model.ciudad_del_caso == 0)
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

                String fechaforce = Model.fecha_ingreso_salesforce.Value.Date.ToString("MM/dd/yyyy");
                String fechaforceok = fechaforce + " " + Model.horasalesforce;

                DateTime fechaForce = Convert.ToDateTime(fechaforceok);

                String fechaBuz = Model.fecha_ingreso_buzon_asalud.Value.Date.ToString("MM/dd/yyyy");
                String fechaBuzok = fechaBuz + " " + Model.horabuzon;

                DateTime fechaBuzon = Convert.ToDateTime(fechaBuzok);

                if (fechaForce > Model.fecha_egreso_salesforce)
                {
                    variable = "ERROR";
                    variable2 = "Fecha egreso salesforce no puede ser menor a fecha ingreso salesforce";
                    Conteo = Conteo + 1;
                }

                if (variable != "ERROR")
                {
                    Model.ObjPqrs.numero_caso = Model.numero_caso;
                    Model.ObjPqrs.consecutivo_caso = Model.consecutivo_caso;
                    Model.ObjPqrs.fecha_ingreso_salesforce = fechaForce;
                    Model.ObjPqrs.fecha_egreso_salesforce = Model.fecha_egreso_salesforce;
                    Model.ObjPqrs.fecha_ingreso_buzon_asalud = fechaBuzon;
                    Model.ObjPqrs.fecha_asignacion = Convert.ToDateTime(DateTime.Now);
                    Model.ObjPqrs.solicitante_numero_identi = Model.solicitante_numero_identi;
                    Model.ObjPqrs.solicitante_nombre = Model.solicitante_nombre;
                    Model.ObjPqrs.regional = Model.regional;
                    Model.ObjPqrs.ciudad_del_caso = Model.ciudad_del_caso;
                    Model.ObjPqrs.tipo_solicitud = Model.tipo_solicitud;
                    Model.ObjPqrs.especificacion_solicitud = Model.especificacion_solicitud;
                    Model.ObjPqrs.usuario_asignado = analista;
                    Model.ObjPqrs.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    Model.ObjPqrs.usuario_ingreso = Convert.ToString(SesionVar.UserName);
                    Model.ObjPqrs.estado_gestion = 1;
                    Model.ObjPqrs.solucionador = Model.solucionador;
                    //Model.ObjPqrs.auditor_asignado = Model.auditor;
                    Model.ObjPqrs.id_ref_categorizacon = Model.tipo_categorizacion;
                    Model.ObjPqrs.fecha_envio_correo = Convert.ToDateTime(DateTime.Now);
                    Model.ObjPqrs.fecha_envio_correo_direc = Convert.ToDateTime(DateTime.Now);
                    Model.ObjPqrs.otro_solucionador = Model.otro_solucionador;
                    Model.ObjPqrs.id_solicionador = Model.id_solicionador;
                    Model.ObjPqrs.auxiliar_solucionador = Model.auxiliar_solucionador;
                    Model.ObjPqrs.otro_auxiliar_solucionador = Model.otro_auxiliar_solucionador;
                    Model.ObjPqrs.pasa_auditor = null;
                    Model.ObjPqrs.supersalud = Model.superSalud;
                    Model.ObjPqrs.tipo_ingreso = 1;
                    Model.ObjPqrs.conArchivo = 1;
                    Model.InsertarPQRS(ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        var resultado = agregarConteoAnalista(analista, idUsuario);

                        return RedirectToAction("TableroControl", "Pqrs");
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
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            List<Ref_regional> regional = new List<Ref_regional>();
            regional = BusClass.GetRefRegion();
            ViewBag.regional = regional;

            return View(Model);
        }

        public int IdAanalistaAsignado(int ciudad, int regional)
        {
            var idAnalista = 0;
            var analista = "";
            var mesActual = DateTime.Now.Month;
            var añoActual = DateTime.Now.Year;
            try
            {
                log_pqrs_reinicioConteo_asignacionAnalistas logBus = new log_pqrs_reinicioConteo_asignacionAnalistas();
                logBus = BusClass.BuscarReinicioConteoPqrs(mesActual, añoActual);

                if (logBus == null)
                {
                    log_pqrs_reinicioConteo_asignacionAnalistas log = new log_pqrs_reinicioConteo_asignacionAnalistas();

                    log.mes = mesActual;
                    log.año = añoActual;
                    log.fecha_digita = DateTime.Now;
                    log.usuario_digita = SesionVar.UserName;

                    var insertaLog = BusClass.InsertarLogReinicioConteoPqrs(log);

                    if (insertaLog != 0)
                    {
                        var actualiza = BusClass.ActualizaConteoPqrsAnalistas();
                    }
                }

                List<management_buscarAnalistaPqrsResult> pqr = new List<management_buscarAnalistaPqrsResult>();
                management_buscarAnalistaPqrsResult dato = new management_buscarAnalistaPqrsResult();

                pqr = BusClass.AnalistaPqr(ciudad, regional);
                if (pqr.Count() > 0)
                {
                    dato = pqr.FirstOrDefault();
                    if (dato != null)
                    {
                        analista = dato.usuario;

                        Ref_PQRS_usuarios usuario = new Ref_PQRS_usuarios();
                        usuario = BusClass.GetUsuarioPqrs(analista);

                        if (usuario != null)
                        {
                            idAnalista = usuario.id_ref_PQRS_usuarios;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return idAnalista;
        }

        public int agregarConteoAnalista(int idAnalista, int idUsuario)
        {
            var resultado = 0;
            try
            {
                resultado = BusClass.insertarConteoAnalistaPQR(idAnalista, idUsuario);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GestionPQRS(Models.PQRS.GestionPqrs Model)
        {
            var idPqrs = Model.id_ecop_PQRS;

            ViewBag.idRol = SesionVar.ROL;

            List<Ref_PQRS_estado_Gestion> PqrsEstadoGest = new List<Ref_PQRS_estado_Gestion>();
            List<management_pqrs_observacionesAuditorResult> listaObservaciones = new List<management_pqrs_observacionesAuditorResult>();
            ecop_PQRS_Auditor respuestaAuditor = new ecop_PQRS_Auditor();
            respuestaAuditor = BusClass.TraerRespuestaAuditor(Model.id_ecop_PQRS);

            var conteoObservaciones = 0;

            PqrsEstadoGest = BusClass.GetRefPqrsGestion();
            ViewBag.listEstados = PqrsEstadoGest;

            var listaRang = BusClass.RolCargo();

            var pasaAuditor = "";

            try
            {

                listaObservaciones = BusClass.PqrsListaObservacionesAuditor(Convert.ToInt32(idPqrs));
                conteoObservaciones = listaObservaciones.Count;

                ViewBag.conteoObs = conteoObservaciones;

                ecop_PQRS pq = new ecop_PQRS();

                pq = BusClass.LlamarPqrsById(Convert.ToInt32(idPqrs));
                if (pq != null)
                {
                    pasaAuditor = pq.pasa_auditor;
                }

                var pasa_Auditor = 0;

                if (pasaAuditor == "SI")
                {
                    pasa_Auditor = 1;
                }
                else if (pasaAuditor == "NO")
                {
                    pasa_Auditor = 2;
                }
                else
                {
                    pasa_Auditor = 2;
                }
                ViewBag.pasaAuditor = pasa_Auditor;

                ecop_PQRS_Auditor audi = new ecop_PQRS_Auditor();
                audi = BusClass.ConsultaPQRSAuditor(Convert.ToInt32(idPqrs)).OrderByDescending(x => x.id_ecop_PQRS_auditor).FirstOrDefault();
                var decisionAudi = "";
                var decisionFinalAudi = 0;

                if (audi != null)
                {
                    decisionAudi = audi.vobo;

                    if (decisionAudi == "SI" || decisionAudi == null)
                    {
                        decisionFinalAudi = 1;
                    }
                    else
                    {
                        decisionFinalAudi = 2;
                    }
                }

                ViewBag.final = decisionFinalAudi;

                List<GestionDocumentalPQRS2> LsitUrl2 = new List<GestionDocumentalPQRS2>();
                LsitUrl2 = Model.GetUrlProyeccion(Model.id_ecop_PQRS).Where(x => x.id_tipo_documental == 8).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).ToList();

                if (LsitUrl2 == null || LsitUrl2.Count() == 0)
                {
                    ViewBag.existeArchivo = "NO";
                }
                else
                {
                    ViewBag.existeArchivo = "SI";
                }


                listaRang = listaRang.Where(x => x.id_rol_cargo == 20 || x.id_rol_cargo == 10 || x.id_rol_cargo == 13 || x.id_rol_cargo == 14 || x.id_rol_cargo == 15 || x.id_rol_cargo == 17).ToList();
                ViewBag.cargo = listaRang.ToList();

                List<ecop_pqrs_a_quien_llamo> ListaaQuienLlama = new List<ecop_pqrs_a_quien_llamo>();
                List<ecop_pqrs_auditores> listAudi = new List<ecop_pqrs_auditores>();
                List<ecop_pqrs_prestadores> listPres = new List<ecop_pqrs_prestadores>();

                List<vw_ecop_PQRS_DetalleG> listaDtll = new List<vw_ecop_PQRS_DetalleG>();
                listaDtll = BusClass.ConsultaPQRSDetalle(Model.id_ecop_PQRS);
                listaDtll = listaDtll.OrderBy(x => x.fecha_gestion).ToList();

                var lista = listaDtll.Count();

                //HttpPostedFileBase Files3;
                //Files3 = Request.Files[1];

                //HttpPostedFileBase Files5;
                //Files5 = Request.Files[0];

                ViewBag.msg = "";
                ViewBag.rta = 0;

                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";
                var estadoGestion = Model.estado_gestion;

                ViewBag.componentes = BusClass.GetComponentesHospitalarios();

                if (Model.fecha_gestionOK != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE GESTION";
                    Conteo = Conteo + 1;
                }


                //if (Model.estado_gestion == 3)
                //{
                //    if (Files3.FileName != null && Files3.FileName != "")
                //    {

                //    }
                //    else
                //    {
                //        variable = "ERROR";
                //        variable2 = "INGRESAR LA PROYECCION EN PDF O WORD";
                //        Conteo = Conteo + 1;

                //    }
                //}

                if (Model.estado_gestion == 2)
                {
                    if (Model.FechaPrestador == "SI")
                    {
                        if (lista == 0)
                        {
                            if (Model.fechaenvioprestador == null)
                            {
                                variable = "ERROR";
                                variable2 = "DEBE INGRESAR FECHA ENVIO AL PRESTADOR";
                                Conteo = Conteo + 1;
                            }
                        }
                    }
                }
                //if (Model.estado_gestion == 5)
                //{
                //    if (Files5.FileName != null && Files5.FileName != "")
                //    {

                //    }
                //    else
                //    {
                //        variable = "ERROR";
                //        variable2 = "INGRESAR LA REVISIÓN EN PDF O WORD";
                //        Conteo = Conteo + 1;

                //    }
                //}

                if (Model.estado_gestion != 4)
                {

                    if (Model.id_pqrs_subtematica != 0)
                    {

                    }
                    else
                    {
                        variable = "ERROR";
                        variable2 = "INGRESAR SUBTEMATICA";
                        Conteo = Conteo + 1;
                    }

                    if (Model.listaPrestador != "")
                    {

                    }
                    else
                    {
                        variable = "ERROR";
                        variable2 = "INGRESAR PRESTADOR";
                        Conteo = Conteo + 1;
                    }
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
                    Model.ObjPqrs.id_ecop_PQRS = Model.id_ecop_PQRS;

                    if (Model.Voboauditor != null || Model.requiereAnalisis_caso != null)
                    {
                        if ((Model.Voboauditor.Equals("SI") || Model.requiereAnalisis_caso.Equals("SI")) && Model.estado_gestion == 5)
                        {
                            Model.ObjPqrs.pasa_auditor = "SI";

                            if (respuestaAuditor != null)
                            {
                                if (respuestaAuditor.vobo == "NO")
                                {
                                    Model.ObjPqrs.ingreso_inicial = 1;
                                }
                                else
                                {
                                    Model.ObjPqrs.ingreso_inicial = null;
                                }
                            }
                        }
                        else
                        {
                            Model.ObjPqrs.pasa_auditor = "NO";
                        }
                    }

                    Model.ObjPqrs.id_pqrs_subtematica = Model.id_pqrs_subtematica;
                    Model.ObjPqrs.prestador = Model.listaPrestador;
                    Model.ObjPqrs.fecha_gestion = Model.fecha_gestionOK;
                    Model.ObjPqrs.evento_adverso = Model.evento_adverso;

                    if (Model.seHaceLlamada == 1)
                    {
                        Model.ObjPqrs.requirio_llamada = "SI";
                    }
                    else
                    {
                        Model.ObjPqrs.requirio_llamada = "NO";
                    }

                    Model.ObjPqrs.ciudad_del_caso = Model.ciudad_del_caso;
                    Model.ObjPqrs.regional = Model.regional;
                    Model.ObjPqrs.componente = Model.componente;
                    Model.ObjPqrs.otro_prestador = Model.otro_prestador;
                    Model.ObjPqrs.plan_de_mejora = Model.plan_mejora;

                    if (Model.plan_mejora == 1)
                    {
                        Model.ObjPqrs.observacion_plan_mejora = Model.observacion_plan_mejora;
                    }

                    Model.ObjPqrs.regional = Model.regional;

                    Model.ObjPqrs.solucionador = Model.solucionador;

                    //Model.usuario_ingreso
                    var decision = Model.FechaPrestador;

                    if (decision == "SI")
                    {
                        Model.ObjPqrs.fecha_envio_prestador_opcion = true;
                    }
                    else
                    {
                        Model.ObjPqrs.fecha_envio_prestador_opcion = false;
                    }

                    //nuevo codigo para llenar quien llamó

                    //Model.ObjPqrs.a_quie_llamo = 0;
                    //Model.ObjPqrs.nombre_quien_llamo = "NA";

                    var listaLlama = Model.listaQuien;

                    if (listaLlama != "" && listaLlama != null)
                    {
                        String[] listaQuienesLlamo = new string[0];
                        if (listaLlama != null)
                        {
                            listaQuienesLlamo = listaLlama.Split(',');
                        }

                        foreach (var item in listaQuienesLlamo)
                        {
                            ecop_pqrs_a_quien_llamo obj = new ecop_pqrs_a_quien_llamo();

                            obj.id_tipo_llamado = Convert.ToInt32(item);
                            obj.id_ecop_pqr = Model.id_ecop_PQRS;
                            obj.usuario_digita = SesionVar.UserName;
                            obj.fecha_digita = DateTime.Now;

                            if (Convert.ToInt32(item) == 1)
                            {
                                obj.a_quien_llamo = Model.nombrellamo_prestador;
                                obj.telefono = Convert.ToString(Model.telefono_llamo_prestador);
                            }
                            else if (Convert.ToInt32(item) == 2)
                            {
                                obj.a_quien_llamo = Model.nombrellamo_auditor;
                                obj.telefono = Convert.ToString(Model.telefono_llamo_auditor);
                            }
                            else if (Convert.ToInt32(item) == 3)
                            {
                                obj.a_quien_llamo = Model.nombrellamo_ecopetrol;
                                obj.telefono = Convert.ToString(Model.telefono_llamo_ecopetrol);
                            }
                            else if (Convert.ToInt32(item) == 4)
                            {
                                obj.a_quien_llamo = Model.nombrellamo_paciente;
                                obj.telefono = Convert.ToString(Model.telefono_llamo_paciente);
                            }

                            ListaaQuienLlama.Add(obj);
                        }
                    }

                    var listaAuditores = Model.listaAuditor;
                    if (!String.IsNullOrEmpty(listaAuditores))
                    {
                        string[] listadoAuditores = listaAuditores.Split(',');
                        foreach (var item in listadoAuditores)
                        {
                            ecop_pqrs_auditores individualAudi = new ecop_pqrs_auditores();
                            individualAudi.id_pqrs = Model.id_ecop_PQRS;
                            individualAudi.id_auditor = Convert.ToInt32(item);
                            individualAudi.fecha_digita = DateTime.Now;
                            individualAudi.usuario_digita = SesionVar.UserName;
                            listAudi.Add(individualAudi);
                        }
                    }

                    var listaPrestador = Model.listaPrestador;
                    if (!String.IsNullOrEmpty(listaPrestador))
                    {
                        string[] listadoPrestadores = listaPrestador.Split(',');
                        foreach (var item in listadoPrestadores)
                        {
                            ecop_pqrs_prestadores individualPre = new ecop_pqrs_prestadores();
                            individualPre.id_pqrs = Model.id_ecop_PQRS;
                            individualPre.id_prestador = Convert.ToInt32(item);
                            individualPre.fecha_digita = DateTime.Now;
                            individualPre.usuario_digita = SesionVar.UserName;
                            listPres.Add(individualPre);
                        }
                    }

                    Model.ObjPqrs.observacion_gestion = Model.observacion_gestion;

                    if (Model.estado_gestion == 2)
                    {
                        Model.ObjPqrs.validez = Model.validez;
                        Model.ObjPqrs.atributo = Model.atributo;
                        Model.ObjPqrs.fecha_envio_prestador = Model.fechaenvioprestador;
                        Model.ObjPqrs.estado_gestion = Model.estado_gestion;

                        BusClass.ActualizarPQRSAuditorId(Model.id_ecop_PQRS);

                        string strInfo = string.Empty;
                        strInfo = cargarArchivos(Model);
                    }
                    //15178
                    if (Model.estado_gestion == 3 || Model.estado_gestion == 7)
                    {
                        Model.ObjPqrs.validez = Model.validez;
                        Model.ObjPqrs.atributo = Model.atributo;
                        Model.ObjPqrs.estado_gestion = Model.estado_gestion;
                        Model.ObjPqrs.fecha_recepcion_prestador = Model.fecharecepcionprestadorselect;

                        string strInfo = string.Empty;
                        strInfo = cargarArchivos(Model);
                    }
                    if (Model.estado_gestion == 5)
                    {
                        Model.ObjPqrs.validez = Model.validez;
                        Model.ObjPqrs.atributo = Model.atributo;
                        //string strInfo = string.Empty;
                        //strInfo = cargarArchivosEnrevision(Model);
                        Model.ObjPqrs.estado_gestion = Model.estado_gestion;

                        Model.ObjPqrs.fecha_envio_revision = DateTime.Now;
                        Model.ObjPqrs.usuario_envio_revision = SesionVar.UserName;
                    }

                    else if (Model.estado_gestion == 4)
                    {
                        Model.ObjPqrs.observacion_ampliacion = Model.observacion_ampliacion;
                        Model.ObjPqrs.fecha_ampliacion = Model.fecha_ampliacionok;
                    }
                    else
                    {
                        Model.ObjPqrs.validez = "NA";
                        Model.ObjPqrs.atributo = 0;
                    }

                    if (Model.Voboauditor == "SI")
                    {
                        Model.ObjPqrs.vobo_auditor = Model.Voboauditor;
                        Model.ObjPqrs.auditor_asignado = Model.listaAuditor;
                        Model.ObjPqrs.id_vobo_auditor = Model.auditor;
                    }
                    else
                    {
                        Model.ObjPqrs.vobo_auditor = Model.Voboauditor;
                    }

                    if (Model.estado_gestion == 3)
                    {
                        Model.estado_gestion = 7;
                    }

                    Model.ObjPqrs.estado_gestion = Model.estado_gestion;
                    Model.ObjPqrs.doc_beneficiario = Model.documento_beneficiario;
                    Model.ObjPqrs.edad_beneficiario = Model.edad_beneficiario;
                    Model.ObjPqrs.analisis_caso = Model.requiereAnalisis_caso;
                    Model.ObjPqrs.usuario_ingreso = SesionVar.UserName;

                    Model.ActualizarPQRS(ref MsgRes);
                    ViewBag.tipo = 1;

                    idPqrs = Model.id_ecop_PQRS;

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            if (listPres.Count() > 0)
                            {
                                var respuestaPresta = BusClass.CargueMasivoPrestadores(listPres, ref MsgRes);
                            }

                            if (ListaaQuienLlama.Count() > 0)
                            {
                                var eliminaLlama = BusClass.EiminarQuienesLlamoGestion(Model.id_ecop_PQRS);
                                var respuestaLlama = Model.CargueMasivoQuienLlamoPqrs(ListaaQuienLlama, ref MsgRes);
                            }

                            if (listAudi.Count() > 0)
                            {
                                var respuestaAuditores = Model.CargueMasivoAuditores(listAudi, ref MsgRes);

                                if (respuestaAuditores != 0)
                                {
                                    EnviarCasoAnalistaAuditor(idPqrs, 1);
                                }
                            }

                            ViewBag.msg = "INGRESADO CORRECTAMENTE";
                            ViewBag.rta = 1;
                            return RedirectToAction("GestionPQRS", "Pqrs", new { idPqrs = Model.id_ecop_PQRS, msg = ViewBag.msg, rta = ViewBag.rta, tipo = 1 });
                        }
                        else
                        {
                            ViewBag.msg = "ERROR AL ACTUALIZAR!";
                            ViewBag.rta = 2;
                            return RedirectToAction("GestionPQRS", "Pqrs", new { idPqrs = Model.id_ecop_PQRS, msg = ViewBag.msg, rta = ViewBag.rta, tipo = 1 });
                        }
                    }
                    else
                    {
                        ViewBag.msg = "ERROR AL ACTUALIZAR!";
                        ViewBag.rta = 2;
                        return RedirectToAction("GestionPQRS", "Pqrs", new { idPqrs = Model.id_ecop_PQRS, msg = ViewBag.msg, rta = ViewBag.rta, tipo = 1 });
                    }
                }
                else
                {
                    ViewBag.msg = "ERROR...DEBE INGRESAR TODOS LOS CAMPOS! " + variable2;
                    ViewBag.rta = 2;
                    return RedirectToAction("GestionPQRS", "Pqrs", new { idPqrs = Model.id_ecop_PQRS, msg = ViewBag.msg, rta = ViewBag.rta, tipo = 1 });
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }

            return View(Model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GestionPQRSAuditor(Models.PQRS.GestionPqrs Model)
        {
            var mensaje = "";
            var rta = 0;
            ViewBag.rol = SesionVar.ROL;

            try
            {
                if (Model.atributo_auditor == null || Model.atributo_auditor == 0)
                {
                    rta = 2;
                    mensaje = "Seleccione atributo.";
                    return RedirectToAction("GestionPQRSAuditor", "Pqrs", new { idPqrs = Model.id_ecop_PQRS, msg = mensaje });
                }

                ecop_PQRS_Auditor OBJ = new ecop_PQRS_Auditor();

                OBJ.id_ecop_PQRS = Model.id_ecop_PQRS;
                OBJ.observacion_gestion = Model.observacion_gestion;
                OBJ.validez = Model.validez_Auditor;

                OBJ.vobo = Model.vobo_Auditor;

                OBJ.atributo = Model.atributo_auditor;
                OBJ.respuesta_prestador = Model.respuesta_prestador;
                OBJ.contacto_con_beneficiario = Model.contacto_con_beneficiario;
                OBJ.concepto_tecnico_auditor = Model.concepto_tecnico_auditor;
                OBJ.soluciona_el_problema = Model.solucion_problema;
                OBJ.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                OBJ.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                Model.InsertarPQRSAuditor(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    ecop_PQRS obj2 = new ecop_PQRS();
                    obj2.pasa_auditor = "NO";
                    obj2.id_ecop_PQRS = Model.id_ecop_PQRS;

                    if (OBJ.vobo == "NO")
                    {
                        obj2.estado_gestion = 1;
                    }

                    var resultadoAc = Model.ActualizarPqrsEstado(obj2, ref MsgRes);

                    ecop_PQRS pqr = BusClass.LlamarPqrsById(Model.id_ecop_PQRS);

                    GestionDocumentalPQRS2 existeArchivoConcepto = BusClass.ExisteArchivoConceptoAuditor(Model.id_ecop_PQRS);

                    if (existeArchivoConcepto != null)
                    {
                        BusClass.eliminarArchivoPqrsidArchivo(existeArchivoConcepto.id_gestion_documental_pqrs);
                    }

                    pdfConceptoAuditor(pqr);

                    return RedirectToAction("TableroControlAuditor", "Pqrs");
                }
                else
                {
                    mensaje = "ERROR AL INGRESO " + MsgRes.DescriptionResponse;
                    return RedirectToAction("GestionPQRSAuditor", "Pqrs", new { idPqrs = Model.id_ecop_PQRS, msg = mensaje });
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }

            return View(Model);
        }

        public JsonResult GetCascadeSubtematica(Models.PQRS.GestionPqrs Model)
        {
            return Json(Model.PqrSubtematica.Select(c => new { id_pqrs_subtematica = c.id_pqrs_subtematica, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeIPS(string Ciudad, Models.Facturacion.FacturaDevolucion Model)
        {
            return Json(Model.ListIPS.Select(p => new { id_ref_ips_cuentas = p.id_ref_ips_cuentas, Nombre = p.Nombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadaComponente(Models.Facturacion.FacturaDevolucion Model)
        {
            return Json(Model.ListComponente.Select(p => new { id_componente = p.id_componente, descripcion = p.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeAuditores(string regional, string ciudad_del_caso, Models.PQRS.GestionPqrs Model)
        {
            return Json(Model.ListAudPQRS.Select(p => new { id_usuario = p.id_usuario, Nombre = p.nombre }), JsonRequestBehavior.AllowGet);
        }


        public JsonResult Get2()
        {

            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            List<vw_ecop_PQRS> Lista = new List<vw_ecop_PQRS>();

            //Model.LlenaLista();
            Lista = Model.BuscarTablero();

            var query = Lista;
            List<Managment_md_Ref_indicadorResult> records = new List<Managment_md_Ref_indicadorResult>();

            return this.Json(new { query }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetCascadeCargos(Models.PQRS.GestionPqrs Model)
        //{
        //    return Json(Model.RolCargos.Select(c => new { id_rol_cargo = c.id_rol_cargo, Rol = c.Rol }), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetCascadeCargos()
        {
            var miproyecto = "";
            var listatotal = new object();
            List<Ref_rol_cargo> listarol = new List<Ref_rol_cargo>();

            try
            {
                listarol = BusClass.RolCargo().Where(x => x.id_rol_cargo == 20 || x.id_rol_cargo == 10 || x.id_rol_cargo == 13 || x.id_rol_cargo == 14 || x.id_rol_cargo == 15 || x.id_rol_cargo == 17).ToList();

                ViewBag.Lista = listarol;
                listatotal = (from item in listarol
                              select new
                              {
                                  value = item.id_rol_cargo,
                                  text = item.Rol,
                              });
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(listatotal, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCascadeAuditor(string cargo, String regional, Models.PQRS.GestionPqrs Model)
        {
            return Json(Model.ListAudPQRS.Select(c => new { id_usuario = c.id_usuario, nombre = c.nombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarPQRS(Models.PQRS.GestionPqrs Model)
        {
            String mensaje = "";
            try
            {
                Model.id_ecop_PQRS = Model.id_ecop_PQRS;

                var idPqrs = Model.id_ecop_PQRS;

                ecop_PQRS datos = new ecop_PQRS();
                datos = BusClass.LlamarPqrsById(idPqrs);

                var numeroCaso = "";

                if (datos != null)
                {
                    numeroCaso = datos.numero_caso;
                }

                Model.EliminarPQRS(Model.id_ecop_PQRS, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Log_eliminacion_pqrs obj = new Log_eliminacion_pqrs();

                    obj.id_ecop_PQRS = Model.id_ecop_PQRS;
                    obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);
                    obj.numero_caso = numeroCaso;

                    Model.InsertarPQRSEliminar(obj, ref MsgRes);

                    mensaje = "SE ELIMINÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR...";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(false);
            }

        }

        //[HttpPost]
        //public ActionResult GenerarCorreo(String id, String numerocaso, String correo)
        //{
        //    Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

        //    sis_configuracion conf = BusClass.GetParametro("EnvioCorreo");
        //    if (conf != null)
        //    {
        //        GenerarCorreoDirec();
        //    }
        //    var data = new
        //    {
        //        success = true,
        //        message = "Ingreso Exitoso....",
        //    };

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult DescargarPqrs()
        //{
        //    Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

        //    try
        //    {
        //        ExportarInformePQRS();
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return View(Model);
        //}

        //public ActionResult DescargarPqrs2()
        //{
        //    Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

        //    try
        //    {
        //        ExportarInformePQRS();
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }


        //    return View(Model);
        //}

        public ActionResult DescargarPqrsAuditor()
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {
                //ExportarInformePQRS();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public ActionResult Reabrircaso(Int32? pqrs)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            ecop_PQRS OBJ = new ecop_PQRS();

            try
            {

                OBJ.id_ecop_PQRS = Convert.ToInt32(pqrs);
                OBJ.estado_gestion = 2;
                OBJ.fecha_gestion = null;

                Model.ActualizaReabrirPQRS(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("TableroControlProyectadas", "Pqrs", new { id = 1 });
                }
                else
                {
                    ModelState.AddModelError("", "ERROR AL ACTUALIZAR!");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public void ExportarInformePQRS(int? tipo)
        {
            string proye;
            List<management_pqrs_tablero_controlResult> Lista = new List<management_pqrs_tablero_controlResult>();
            try
            {
                ExcelPackage Ep = new ExcelPackage();
                if (tipo == 1)
                {
                    Lista = (List<management_pqrs_tablero_controlResult>)Session["ListaPQRS"];
                }
                else
                {
                    Lista = (List<management_pqrs_tablero_controlResult>)Session["ListaPQRSAuditores"];
                }

                proye = "ConsultaPqrs";

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
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(proye);

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:U1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:U1"].Style.Font.Size = 10;
                    //Sheet.Cells["A1:T1"].Style.WrapText = true;
                    Sheet.Cells["A1:U1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:U1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:U1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id";
                    Sheet.Cells["B1"].Value = "Número caso";
                    Sheet.Cells["C1"].Value = "Ciudad del caso";
                    Sheet.Cells["D1"].Value = "Fecha asignación";
                    Sheet.Cells["E1"].Value = "Ingreso SalesForce";
                    Sheet.Cells["F1"].Value = "Respuesta programada Salesforce";
                    Sheet.Cells["G1"].Value = "Vencimiento 2 días hábiles SalesForce";
                    Sheet.Cells["H1"].Value = "Fecha ampliación";
                    Sheet.Cells["I1"].Value = "Ingreso buzón Asalud";
                    Sheet.Cells["J1"].Value = "Vencimiento 5 días hábiles buzón Asalud";
                    Sheet.Cells["K1"].Value = "Última observación a buzón";
                    Sheet.Cells["L1"].Value = "Analista asignado";
                    Sheet.Cells["M1"].Value = "Prestador";
                    Sheet.Cells["N1"].Value = "Auditor asignado";
                    Sheet.Cells["O1"].Value = "Días a vencer";
                    Sheet.Cells["P1"].Value = "Estado semáforo";
                    Sheet.Cells["Q1"].Value = "Estado semáforo salesforce";
                    Sheet.Cells["R1"].Value = "Evento adverso";
                    Sheet.Cells["S1"].Value = "Fecha de envío al prestador";
                    Sheet.Cells["T1"].Value = "Fecha de recepción respuesta del prestador";
                    Sheet.Cells["U1"].Value = "Usuario ingresó queja";

                    int row = 2;

                    foreach (management_pqrs_tablero_controlResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":T" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_ecop_PQRS;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.numero_caso;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.ciudad_del_caso_descripcion;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_asignacion;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_ingreso_salesforce;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_egreso_salesforce;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_respuesta_programada2sales;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_ampliacion;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_ingreso_buzon_asalud;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_respuesta_programada5Buzon;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.ultima_obs;
                        Sheet.Cells[string.Format("K{0}", row)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        Sheet.Cells[string.Format("K{0}", row)].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.usuario_asignado_descrip;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.prestador_nom;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.nombre_auditor_des;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.dias_restantes10sales;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.Estado_SemaforoSalesforce == "Vencido" ? "Vencido" : item.Estado_Semaforo;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.Estado_SemaforoSalesforce;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.evento_adverso;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.fecha_envio_prestador;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.fecha_recepcion_respuesta_prestador;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.usuarioQueja;


                        Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("S{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("T{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    Sheet.Cells["A:U"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ConsultaPQRS" + DateTime.Now + ".xlsx");

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

        public void ExportarInformePQRSSalesForce(int? tipo)
        {
            string proye;
            List<management_pqrs_tablero_controlResult> Lista = new List<management_pqrs_tablero_controlResult>();
            try
            {
                ExcelPackage Ep = new ExcelPackage();
                if (tipo == 1)
                {
                    Lista = (List<management_pqrs_tablero_controlResult>)Session["ListaPQRS2"];
                }
                else
                {
                    Lista = (List<management_pqrs_tablero_controlResult>)Session["ListaPQRSAuditores"];
                }

                proye = "ConsultaPqrs";

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
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(proye);

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:T1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:T1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:T1"].Style.Font.Size = 10;
                    //Sheet.Cells["A1:T1"].Style.WrapText = true;
                    Sheet.Cells["A1:T1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:T1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:T1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id";
                    Sheet.Cells["B1"].Value = "Número caso";
                    Sheet.Cells["C1"].Value = "Ciudad del caso";
                    Sheet.Cells["D1"].Value = "Fecha asignación";
                    Sheet.Cells["E1"].Value = "Ingreso SalesForce";
                    Sheet.Cells["F1"].Value = "Respuesta programada Salesforce";
                    Sheet.Cells["G1"].Value = "Vencimiento 2 días hábiles SalesForce";
                    Sheet.Cells["H1"].Value = "Fecha ampliación";
                    Sheet.Cells["I1"].Value = "Ingreso buzón Asalud";
                    Sheet.Cells["J1"].Value = "Vencimiento 5 días hábiles buzón Asalud";
                    Sheet.Cells["K1"].Value = "Última observación a buzón";
                    Sheet.Cells["L1"].Value = "Analista asignado";
                    Sheet.Cells["M1"].Value = "Prestador";
                    Sheet.Cells["N1"].Value = "Auditor asignado";
                    Sheet.Cells["O1"].Value = "Días a vencer";
                    Sheet.Cells["P1"].Value = "Estado semáforo";

                    Sheet.Cells["Q1"].Value = "Evento adverso";
                    Sheet.Cells["R1"].Value = "Fecha de envío al prestador";
                    Sheet.Cells["S1"].Value = "Fecha de recepción respuesta del prestador";
                    Sheet.Cells["T1"].Value = "Usuario ingresó queja";

                    int row = 2;

                    foreach (management_pqrs_tablero_controlResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":T" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_ecop_PQRS;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.numero_caso;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.ciudad_del_caso_descripcion;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_asignacion;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_ingreso_salesforce;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_egreso_salesforce;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_respuesta_programada2sales;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_ampliacion;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_ingreso_buzon_asalud;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.fecha_respuesta_programada5Buzon;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.ultima_obs;
                        Sheet.Cells[string.Format("K{0}", row)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        Sheet.Cells[string.Format("K{0}", row)].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.usuario_asignado_descrip;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.prestador_nom;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.nombre_auditor_des;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.dias_restantes10sales;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.Estado_SemaforoSalesforce;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.evento_adverso;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.fecha_envio_prestador;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.fecha_recepcion_respuesta_prestador;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.usuarioQueja;

                        Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("S{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    Sheet.Cells["A:T"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ConsultaPQRS" + DateTime.Now + ".xlsx");

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

        public void ExportarInformePQRSAuditor()
        {
            try
            {
                List<management_pqrs_tablero_controlResult> Lista = (List<management_pqrs_tablero_controlResult>)Session["ListaPQRSAuditor"];

                if (Lista.Count != 0)
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("\"Id\";\"Numero Caso\";\"Ciudad del caso\";\"Fecha asignacion\";\"Ingreso SalesForce\";\"Vencimiento 10 Dias Habiles SalesForce\";\"fecha ampliacion\";\"Ingreso Buzon Asalud\";\"Vencimiento 5 Dias Habiles Buzon Asalud\";\"Ultima Observacion\";\"Analista Asignado\";\"Prestador\";\"Auditor Asignado\";\"Dias a vencer\";\"Estado Semaforo\";\"Evento Adverso\";\"Fecha de envio al prestador \";\"Fecha de recepcion respuesta del prestador\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=ConsultaPQRS" + DateTime.Now + ".csv");
                    Response.ContentType = "text/csv";

                    foreach (var line in Lista)
                    {
                        sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\"",
                                                   line.id_ecop_PQRS,
                                                   line.numero_caso,
                                                   line.ciudad_del_caso_descripcion,
                                                   line.fecha_asignacion,
                                                   line.fecha_ingreso_salesforce,
                                                   line.fecha_respuesta_programada10sales,
                                                   line.fecha_ampliacion,
                                                   line.fecha_ingreso_buzon_asalud,
                                                   line.fecha_respuesta_programada5Buzon,
                                                   line.ultima_obs,
                                                   line.usuario_asignado_descrip,
                                                   line.prestador_nom,
                                                   line.nombre_auditor_des,
                                                   line.dias_restantes10sales,
                                                   line.Estado_Semaforo,
                                                   line.evento_adverso,
                                                   line.fecha_envio_prestador,
                                                   line.fecha_recepcion_respuesta_prestador
                                                   ));
                    }

                    Response.Write(sw.ToString());

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

        public ActionResult GenerarCorreoDirec()
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {

                List<vw_ecop_PQRS_correo_direc> list = new List<vw_ecop_PQRS_correo_direc>();

                list = Model.ConsultaPQRSCorreo();
                list = list.Where(x => x.AlertaCorreo == "SI").ToList();

                if (list.Count != 0)
                {
                    string textBody = " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 700 + "><tr bgcolor='#9FABC1'><td><b>No Caso</b></td> <td> <b>Regional</b> </td> <td> <b>Estado semáforo</b> </td><td> <b>Días a vencer</b> </td> <td> <b>Analista asignado</b> </td></tr>";
                    foreach (var item in list)
                    {
                        textBody += "<tr><td>" + item.numero_caso + "</td><td> " + item.regional_descripcion + "</td>  <td> " + item.semaforo + "</td> <td> " + item.dias_restantes10sales + "</td> <td> " + item.usuario_asignado_descrip + "</td> </tr>";
                    }
                    textBody += "</table>";


                    try
                    {
                        MailMessage msz = new MailMessage();
                        msz.From = new MailAddress("notificacionespqrs@asaludltda.com");

                        msz.To.Add("adrianamurcia@asaludltda.com");
                        msz.To.Add("direccionproyectos@asaludltda.com");
                        msz.To.Add("ingenieroprocesos@asaludltda.com");
                        msz.To.Add("analistadeproyectos@asaludltda.com");

                        msz.Subject = "[Mensaje Automático] Notificación diaria PQRS.";


                        msz.Body = textBody;
                        msz.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = smtpSection.Network.Host;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.EnableSsl = true;
                        smtp.Send(msz);

                        var data = new
                        {
                            success = true,
                            message = "Ingreso Exitoso....",
                        };


                        List<vw_ecop_PQRS_correo_direc> list2 = new List<vw_ecop_PQRS_correo_direc>();

                        list2 = list;
                        list2 = list2.Where(x => x.regional_descripcion == "Coordinacion Salud Sur (CUR)").ToList();
                        if (list2.Count != 0)
                        {
                            GenerarCorreoSur(list2);
                        }

                        List<vw_ecop_PQRS_correo_direc> list3 = new List<vw_ecop_PQRS_correo_direc>();

                        list3 = list;
                        list3 = list3.Where(x => x.regional_descripcion == "Coordinacion Salud Caribe (CUI)").ToList();
                        if (list3.Count != 0)
                        {
                            GenerarCorreoCaribe(list3);
                        }

                        List<vw_ecop_PQRS_correo_direc> list4 = new List<vw_ecop_PQRS_correo_direc>();

                        list4 = list;
                        list4 = list4.Where(x => x.regional_descripcion == "Coordinacion Salud Santanderes (CUS)").ToList();
                        if (list4.Count != 0)
                        {
                            GenerarCorreoSantander(list4);
                        }

                        List<vw_ecop_PQRS_correo_direc> list5 = new List<vw_ecop_PQRS_correo_direc>();

                        list5 = list;
                        list5 = list5.Where(x => x.regional_descripcion == "Coordinacion Salud Orinoquia (CUQ)").ToList();
                        if (list5.Count != 0)
                        {
                            GenerarCorreoOrinoquia(list5);
                        }

                        List<vw_ecop_PQRS_correo_direc> list6 = new List<vw_ecop_PQRS_correo_direc>();

                        list6 = list;
                        list6 = list6.Where(x => x.regional_descripcion == "Coordinacion Salud Bogota (CUB)").ToList();
                        if (list6.Count != 0)
                        {
                            GenerarCorreoBogota(list6);
                        }

                        List<vw_ecop_PQRS_correo_direc> list7 = new List<vw_ecop_PQRS_correo_direc>();

                        list7 = list;
                        list7 = list7.Where(x => x.regional_descripcion == "Departamento de Salud MM (PSM)").ToList();
                        if (list7.Count != 0)
                        {
                            GenerarCorreoMM(list7);
                        }


                        foreach (var item in list)
                        {
                            Model.ActualizarFechaPQRSDirec(Convert.ToInt32(item.id_ecop_PQRS), ref MsgRes);
                        }

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                        ModelState.Clear();

                        var data = new
                        {
                            success = true,
                            message = "Ingreso errado....",

                        };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(Model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();

                var data = new
                {
                    success = true,
                    message = "Ingreso errado....",

                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GenerarCorreoSur(List<vw_ecop_PQRS_correo_direc> lista)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {

                if (lista.Count != 0)
                {
                    string textBody = " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 700 + "><tr bgcolor='#9FABC1'><td><b>No Caso</b></td> <td> <b>Regional</b> </td> <td> <b>Estado semáforo</b> </td><td> <b>Días a vencer</b> </td> <td> <b>Analista asignado</b> </td></tr>";
                    foreach (var item in lista)
                    {
                        textBody += "<tr><td>" + item.numero_caso + "</td><td> " + item.regional_descripcion + "</td>  <td> " + item.semaforo + "</td> <td> " + item.dias_restantes10sales + "</td> <td> " + item.usuario_asignado_descrip + "</td> </tr>";
                    }
                    textBody += "</table>";

                    try
                    {
                        MailMessage msz = new MailMessage();
                        msz.From = new MailAddress("notificacionespqrs@asaludltda.com");
                        msz.To.Add("coordinacionsur@asaludltda.com");

                        msz.Subject = "[Mensaje Automático] Notificación diaria PQRS por regional.";


                        msz.Body = textBody;
                        msz.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = smtpSection.Network.Host;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.EnableSsl = true;
                        smtp.Send(msz);

                        var data = new
                        {
                            success = true,
                            message = "Ingreso Exitoso....",
                        };
                        foreach (var item in lista)
                        {
                            Model.ActualizarFechaPQRS(Convert.ToInt32(item.id_ecop_PQRS), ref MsgRes);
                        }

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                        ModelState.Clear();

                        var data = new
                        {
                            success = true,
                            message = "Ingreso errado....",

                        };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(Model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();

                var data = new
                {
                    success = true,
                    message = "Ingreso errado....",

                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GenerarCorreoCaribe(List<vw_ecop_PQRS_correo_direc> lista)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {


                if (lista.Count != 0)
                {
                    string textBody = " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 700 + "><tr bgcolor='#9FABC1'><td><b>No Caso</b></td> <td> <b>Regional</b> </td> <td> <b>Estado semáforo</b> </td><td> <b>Días a vencer</b> </td> <td> <b>Analista asignado</b> </td></tr>";
                    foreach (var item in lista)
                    {
                        textBody += "<tr><td>" + item.numero_caso + "</td><td> " + item.regional_descripcion + "</td>  <td> " + item.semaforo + "</td> <td> " + item.dias_restantes10sales + "</td> <td> " + item.usuario_asignado_descrip + "</td> </tr>";
                    }
                    textBody += "</table>";

                    try
                    {
                        MailMessage msz = new MailMessage();
                        msz.From = new MailAddress("notificacionespqrs@asaludltda.com");
                        msz.To.Add("coordinacioncaribe@asaludltda.com");


                        msz.Subject = "[Mensaje Automático] Notificación diaria PQRS por regional.";


                        msz.Body = textBody;
                        msz.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = smtpSection.Network.Host;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.EnableSsl = true;
                        smtp.Send(msz);

                        var data = new
                        {
                            success = true,
                            message = "Ingreso Exitoso....",
                        };
                        foreach (var item in lista)
                        {
                            Model.ActualizarFechaPQRS(Convert.ToInt32(item.id_ecop_PQRS), ref MsgRes);
                        }

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                        ModelState.Clear();

                        var data = new
                        {
                            success = true,
                            message = "Ingreso errado....",
                        };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(Model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();

                var data = new
                {
                    success = true,
                    message = "Ingreso errado....",

                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GenerarCorreoSantander(List<vw_ecop_PQRS_correo_direc> lista)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {

                if (lista.Count != 0)
                {
                    string textBody = " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 700 + "><tr bgcolor='#9FABC1'><td><b>No Caso</b></td> <td> <b>Regional</b> </td> <td> <b>Estado semáforo</b> </td><td> <b>Días a vencer</b> </td> <td> <b>Analista asignado</b> </td></tr>";
                    foreach (var item in lista)
                    {
                        textBody += "<tr><td>" + item.numero_caso + "</td><td> " + item.regional_descripcion + "</td>  <td> " + item.semaforo + "</td> <td> " + item.dias_restantes10sales + "</td> <td> " + item.usuario_asignado_descrip + "</td> </tr>";
                    }
                    textBody += "</table>";

                    try
                    {
                        MailMessage msz = new MailMessage();
                        msz.From = new MailAddress("notificacionespqrs@asaludltda.com");
                        msz.To.Add("coordinacionsantander@asaludltda.com");

                        msz.Subject = "[Mensaje Automático] Notificación diaria PQRS por regional.";

                        msz.Body = textBody;
                        msz.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = smtpSection.Network.Host;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.EnableSsl = true;
                        smtp.Send(msz);

                        var data = new
                        {
                            success = true,
                            message = "Ingreso Exitoso....",
                        };
                        foreach (var item in lista)
                        {
                            Model.ActualizarFechaPQRS(Convert.ToInt32(item.id_ecop_PQRS), ref MsgRes);
                        }

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                        ModelState.Clear();

                        var data = new
                        {
                            success = true,
                            message = "Ingreso errado....",

                        };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(Model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();

                var data = new
                {
                    success = true,
                    message = "Ingreso errado....",

                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GenerarCorreoOrinoquia(List<vw_ecop_PQRS_correo_direc> lista)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {

                if (lista.Count != 0)
                {
                    string textBody = " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 700 + "><tr bgcolor='#9FABC1'><td><b>No Caso</b></td> <td> <b>Regional</b> </td> <td> <b>Estado semáforo</b> </td><td> <b>Días a vencer</b> </td> <td> <b>Analista asignado</b> </td></tr>";
                    foreach (var item in lista)
                    {
                        textBody += "<tr><td>" + item.numero_caso + "</td><td> " + item.regional_descripcion + "</td>  <td> " + item.semaforo + "</td> <td> " + item.dias_restantes10sales + "</td> <td> " + item.usuario_asignado_descrip + "</td> </tr>";
                    }
                    textBody += "</table>";


                    try
                    {
                        MailMessage msz = new MailMessage();
                        msz.From = new MailAddress("notificacionespqrs@asaludltda.com");
                        msz.To.Add("leidyriveros@asaludltda.com");


                        msz.Subject = "[Mensaje Automático] Notificación diaria PQRS por regional.";


                        msz.Body = textBody;
                        msz.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = smtpSection.Network.Host;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.EnableSsl = true;
                        smtp.Send(msz);

                        var data = new
                        {
                            success = true,
                            message = "Ingreso Exitoso....",
                        };
                        foreach (var item in lista)
                        {
                            Model.ActualizarFechaPQRS(Convert.ToInt32(item.id_ecop_PQRS), ref MsgRes);
                        }

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                        ModelState.Clear();

                        var data = new
                        {
                            success = true,
                            message = "Ingreso errado....",

                        };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(Model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();

                var data = new
                {
                    success = true,
                    message = "Ingreso errado....",

                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GenerarCorreoBogota(List<vw_ecop_PQRS_correo_direc> lista)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {

                if (lista.Count != 0)
                {
                    string textBody = " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 700 + "><tr bgcolor='#9FABC1'><td><b>No Caso</b></td> <td> <b>Regional</b> </td> <td> <b>Estado semáforo</b> </td><td> <b>Días a vencer</b> </td> <td> <b>Analista asignado</b> </td></tr>";
                    foreach (var item in lista)
                    {
                        textBody += "<tr><td>" + item.numero_caso + "</td><td> " + item.regional_descripcion + "</td>  <td> " + item.semaforo + "</td> <td> " + item.dias_restantes10sales + "</td> <td> " + item.usuario_asignado_descrip + "</td> </tr>";
                    }
                    textBody += "</table>";


                    try
                    {
                        MailMessage msz = new MailMessage();
                        msz.From = new MailAddress("notificacionespqrs@asaludltda.com");
                        msz.To.Add("vivianamoreno@asaludltda.com");


                        msz.Subject = "[Mensaje Automático] Notificación diaria PQRS por regional.";


                        msz.Body = textBody;
                        msz.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = smtpSection.Network.Host;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.EnableSsl = true;
                        smtp.Send(msz);

                        var data = new
                        {
                            success = true,
                            message = "Ingreso Exitoso....",
                        };
                        foreach (var item in lista)
                        {
                            Model.ActualizarFechaPQRS(Convert.ToInt32(item.id_ecop_PQRS), ref MsgRes);
                        }

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                        ModelState.Clear();

                        var data = new
                        {
                            success = true,
                            message = "Ingreso errado....",

                        };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(Model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();

                var data = new
                {
                    success = true,
                    message = "Ingreso errado....",

                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GenerarCorreoMM(List<vw_ecop_PQRS_correo_direc> lista)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {

                if (lista.Count != 0)
                {
                    string textBody = " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 700 + "><tr bgcolor='#9FABC1'><td><b>No Caso</b></td> <td> <b>Regional</b> </td> <td> <b>Estado semáforo</b> </td><td> <b>Días a vencer</b> </td> <td> <b>Analista asignado</b> </td></tr>";
                    foreach (var item in lista)
                    {
                        textBody += "<tr><td>" + item.numero_caso + "</td><td> " + item.regional_descripcion + "</td>  <td> " + item.semaforo + "</td> <td> " + item.dias_restantes10sales + "</td> <td> " + item.usuario_asignado_descrip + "</td> </tr>";
                    }
                    textBody += "</table>";


                    try
                    {
                        MailMessage msz = new MailMessage();
                        msz.From = new MailAddress("notificacionespqrs@asaludltda.com");
                        msz.To.Add("coordinacionmagdalenamedio@asaludltda.com");

                        msz.Subject = "[Mensaje Automático] Notificación diaria PQRS por regional.";


                        msz.Body = textBody;
                        msz.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = smtpSection.Network.Host;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.EnableSsl = true;
                        smtp.Send(msz);

                        var data = new
                        {
                            success = true,
                            message = "Ingreso Exitoso....",
                        };
                        foreach (var item in lista)
                        {
                            Model.ActualizarFechaPQRS(Convert.ToInt32(item.id_ecop_PQRS), ref MsgRes);
                        }

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                        ModelState.Clear();

                        var data = new
                        {
                            success = true,
                            message = "Ingreso errado....",

                        };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(Model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $" lo siento, estamos enfrentando problemas aquí { ex.Message}";
                ModelState.Clear();

                var data = new
                {
                    success = true,
                    message = "Ingreso errado....",

                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public void ExportarLstpqrsProyectadas()
        {
            List<management_pqrs_tablero_control_proyectadasResult> Lista = new List<management_pqrs_tablero_control_proyectadasResult>();
            string proye;

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                Lista = (List<management_pqrs_tablero_control_proyectadasResult>)Session["ListaPQRSP"];
                proye = "Proyectadas";

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
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(proye);

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:V1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:V1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:V1"].Style.Font.Size = 10;
                    //Sheet.Cells["A1:V1"].Style.WrapText = true;
                    Sheet.Cells["A1:V1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:V1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:V1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                    Sheet.Cells["A1"].Value = "NÚMERO CASO";
                    Sheet.Cells["B1"].Value = "NÚMERO OPERACIÓN";
                    Sheet.Cells["C1"].Value = "CIUDAD";
                    Sheet.Cells["D1"].Value = "NUM IDENTIFICACIÓN SOLICITANTE";
                    Sheet.Cells["E1"].Value = "ASIGNACIÓN";
                    Sheet.Cells["F1"].Value = "INGRESO SALESFORCE";
                    Sheet.Cells["G1"].Value = "VENCIMIENTO 10 DÍAS HÁBILES SALESFORCE";
                    Sheet.Cells["H1"].Value = "INGRESO BUZÓN ASALUD";
                    Sheet.Cells["I1"].Value = "VENCIMIENTO 5  DÍAS HÁBILES BUZÓN ASALUD";
                    Sheet.Cells["J1"].Value = "DIFERENCIA HÁBILES INOPORTUNIDAD INGRESO A BUZÓN";
                    Sheet.Cells["K1"].Value = "ANALISTA ASIGNADO";
                    Sheet.Cells["L1"].Value = "NOMBRE AUDITOR";
                    Sheet.Cells["M1"].Value = "FECHA RESPUESTA PROGRAMADA SALESFORCE";
                    Sheet.Cells["N1"].Value = "DÍAS A VENCER";
                    Sheet.Cells["O1"].Value = "ESTADO SEMÁFORO";
                    Sheet.Cells["P1"].Value = "FECHA APROBACIÓN";
                    Sheet.Cells["Q1"].Value = "OBSERVACIÓN APROBACIÓN";
                    Sheet.Cells["R1"].Value = "FECHA CIERRE";
                    Sheet.Cells["S1"].Value = "OBSERVACIÓN CIERRE";
                    Sheet.Cells["T1"].Value = "USUARIO REAPERTURA CASO";
                    Sheet.Cells["U1"].Value = "OBSERVACIÓN REAPERTURA CASO";
                    Sheet.Cells["V1"].Value = "FECHA REAPERTURA CASO";

                    int row = 2;

                    foreach (management_pqrs_tablero_control_proyectadasResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":V" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.numero_caso;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.consecutivo_caso;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.ciudad_del_caso_descripcion;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.solicitante_numero_identi;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_asignacion.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_ingreso_salesforce.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_respuesta_programada10sales.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_ingreso_buzon_asalud.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_respuesta_programada5Buzon.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.dias_restantes10sales;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.usuario_asignado_descrip;
                        if (item.fecha_gestion != null)
                        {
                            Sheet.Cells[string.Format("L{0}", row)].Value = item.fecha_gestion.Value.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            Sheet.Cells[string.Format("L{0}", row)].Value = "";
                        }

                        Sheet.Cells[string.Format("M{0}", row)].Value = item.dias_restantes10sales;
                        Sheet.Cells[string.Format("M{0}", row)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.dias_restantes10sales;

                        if (item.dias_restantes10sales >= 3 && item.dias_restantes10sales <= 8)
                        {
                            Sheet.Cells[string.Format("O{0}", row)].Value = "Por Vencer.";
                        }
                        else if (item.dias_restantes10sales > 0 && item.dias_restantes10sales <= 2)
                        {
                            Sheet.Cells[string.Format("O{0}", row)].Value = "Casos en alerta.";
                        }
                        else if (item.dias_restantes10sales == 0)
                        {
                            Sheet.Cells[string.Format("O{0}", row)].Value = "Vencidos";
                        }
                        else if (item.dias_restantes10sales <= 2)
                        {
                            Sheet.Cells[string.Format("O{0}", row)].Value = "Ampliación vencidos";
                        }

                        Sheet.Cells[string.Format("P{0}", row)].Value = item.fecha_envio_proyectada;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.observacion_aprobacion;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.fecha_envio_proyectadaCierre;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.observacion_aprobacionCierre;

                        Sheet.Cells[string.Format("T{0}", row)].Value = item.usuarioReapertura;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.observacionReapertura;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.fechaReapertura;


                        Sheet.Cells[string.Format("P{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("V{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile;
                    namefile = "Proyectadas";

                    Sheet.Cells["A:V"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
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

        public void ExportarLstpqrsProyectadasFinales()
        {
            List<management_pqrs_tablero_control_proyectadasFinalesResult> Lista = new List<management_pqrs_tablero_control_proyectadasFinalesResult>();
            string proye;

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                Lista = (List<management_pqrs_tablero_control_proyectadasFinalesResult>)Session["ListaPQRSPF"];
                proye = "ProyectadasFinales";

                if (Lista == null || Lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('No se han encontrado resultados');" +
                    "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(proye);

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:L1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:L1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:L1"].Style.Font.Size = 10;
                    //Sheet.Cells["A1:O1"].Style.WrapText = true;
                    Sheet.Cells["A1:L1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:L1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:L1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "NUMERO CASO";
                    Sheet.Cells["B1"].Value = "NUMERO OPERACION";
                    Sheet.Cells["C1"].Value = "CIUDAD";
                    Sheet.Cells["D1"].Value = "NUM IDENTIFICACION SOLICITANTE";
                    Sheet.Cells["E1"].Value = "ASIGNACION";
                    Sheet.Cells["F1"].Value = "INGRESO SALESFORCE";
                    Sheet.Cells["G1"].Value = "VENCIMIENTO 10 DIAS HABILES SALESFORCE";
                    Sheet.Cells["H1"].Value = "INGRESO BUZON ASALUD";
                    Sheet.Cells["I1"].Value = "VENCIMIENTO 5 DIAS HABILES BUZON ASALUD";
                    Sheet.Cells["J1"].Value = "DIFERENCIA HABILES INOPORTUNIDAD INGRESO A BUZON";
                    Sheet.Cells["K1"].Value = "ANALISTA ASIGNADO";
                    Sheet.Cells["L1"].Value = "NOMBRE AUDITOR";

                    int row = 2;

                    foreach (management_pqrs_tablero_control_proyectadasFinalesResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":L" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.numero_caso;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.consecutivo_caso;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.ciudad_del_caso_descripcion;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.solicitante_numero_identi;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_asignacion.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_ingreso_salesforce.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_respuesta_programada10sales.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_ingreso_buzon_asalud.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fecha_respuesta_programada5Buzon.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.usuario_asignado_descrip;
                        if (item.fecha_gestion != null)
                        {
                            Sheet.Cells[string.Format("L{0}", row)].Value = item.fecha_gestion.Value.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            Sheet.Cells[string.Format("L{0}", row)].Value = "";
                        }

                        row++;
                    }

                    string namefile;

                    namefile = "Proyectadas finales";

                    Sheet.Cells["A:L"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        private string cargarArchivos(Models.PQRS.GestionPqrs Model)
        {
            string strRetorno = string.Empty;

            try
            {
                IList<HttpPostedFileBase> files = Request.Files.GetMultiple("FileUpload1");

                if (files != null)
                {
                    if (files.Count() > 0)
                    {
                        for (var i = 0; i < files.Count(); i++)
                        {
                            try
                            {
                                HttpPostedFileBase file = files[i];
                                if (file.ContentLength > 0)
                                {

                                    strRetorno = GuardarArchivo(Model, file);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strRetorno = ex.Message;
            }
            return strRetorno;
        }

        private string cargarArchivosEnrevision(Models.PQRS.GestionPqrs Model)
        {
            string strRetorno = string.Empty;

            try
            {

                if (Request.Files["FileUpload2"].FileName != null && Request.Files["FileUpload2"].FileName != "")
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = Request.Files["FileUpload2"].FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    string[] formatos = new string[] { "pdf", "docx", "doc", "PDF", "DOCX", "DOC" };
                    if (Array.IndexOf(formatos, ext) < 0)
                    {
                        strRetorno = "Formato de archivo inválido.";
                    }
                    else
                    {
                        strRetorno = GuardarArchivoEnrevision(Model);

                    }
                }
            }
            catch (Exception ex)
            {
                strRetorno = ex.Message;
            }
            return strRetorno;
        }

        private string cargarArchivosAuditor(Models.PQRS.GestionPqrs Model)
        {
            string strRetorno = string.Empty;

            try
            {

                if (Request.Files["FileUpload3"].ContentLength > 0)
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = Request.Files["FileUpload3"].FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    string[] formatos = new string[] { "pdf", "docx", "doc", "PDF", "DOCX", "DOC" };
                    if (Array.IndexOf(formatos, ext) < 0)
                    {
                        strRetorno = "Formato de archivo inválido.";
                    }
                    else
                    {
                        strRetorno = GuardarArchivoGestionAuditor(Model);

                    }
                }
            }
            catch (Exception ex)
            {
                strRetorno = ex.Message;
            }
            return strRetorno;
        }

        private string GuardarArchivo(Models.PQRS.GestionPqrs Model, HttpPostedFileBase file)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

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

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + Model.id_ecop_PQRS);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                OBJ.id_ecop_pqr = Model.id_ecop_PQRS;
                OBJ.id_tipo_documental = 2;
                OBJ.numero_caso = Model.numero_caso;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.cargue_fecha = DateTime.Now;
                OBJ.cargue_usuario = SesionVar.UserName;
                OBJ.tipo = file.ContentType;

                var respuesta = Model.InsertarPQRSProyeccion(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                strError = error;
            }

            return strError;
        }

        private string GuardarArchivoEnrevision(Models.PQRS.GestionPqrs Model)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + Request.Files["FileUpload2"].FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "PQRSGestionRevision";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "PQRSGestionRevisionPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + Model.id_ecop_PQRS);
                var nombre = Path.GetFileNameWithoutExtension(Request.Files["FileUpload2"].FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(Request.Files["FileUpload2"].FileName));


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                Request.Files["FileUpload2"].SaveAs(archivo);

                GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                OBJ.id_ecop_pqr = Model.id_ecop_PQRS;
                OBJ.id_tipo_documental = 5;
                OBJ.numero_caso = Model.numero_caso;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.cargue_fecha = DateTime.Now;
                OBJ.cargue_usuario = SesionVar.UserName;
                OBJ.tipo = Request.Files["FileUpload2"].ContentType;

                Model.InsertarPQRSProyeccion(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return strError;
        }

        private string GuardarArchivoGestionAuditor(Models.PQRS.GestionPqrs Model)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + Request.Files["FileUpload3"].FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "PQRSGestionAuditor";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "PQRSGestionAuditorPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + Model.id_ecop_PQRS);
                var nombre = Path.GetFileNameWithoutExtension(Request.Files["FileUpload3"].FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(Request.Files["FileUpload3"].FileName));


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                Request.Files["FileUpload3"].SaveAs(archivo);

                GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                OBJ.id_ecop_pqr = Model.id_ecop_PQRS;
                OBJ.id_tipo_documental = 6;
                OBJ.numero_caso = Model.numero_caso;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.cargue_fecha = DateTime.Now;
                OBJ.cargue_usuario = SesionVar.UserName;
                OBJ.tipo = Request.Files["FileUpload3"].ContentType;

                Model.InsertarPQRSProyeccion(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return strError;
        }

        public JsonResult ArtefactoProyeccion(Int32? idecopPqrs)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            ViewBag.idrole = SesionVar.ROL;

            Model.id_ecop_PQRS = Convert.ToInt32(idecopPqrs);

            return Json(new { idecopPqrs }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GestorUrlProyeccion(Int32 idecopPqrs)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
                dato = Model.GetUrlProyeccion(idecopPqrs).Where(x => x.id_tipo_documental == 2).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).FirstOrDefault();

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";
                    string extensionTipo = obj.tipo;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];


                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    if (!string.IsNullOrEmpty(extensionTipo))
                    {
                        return File(dirpath, extensionTipo, nombreFinal);
                    }
                    else
                    {
                        return File(dirpath, extension, nombreFinal);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }

            //try
            //{
            //    List<GestionDocumentalPQRS2> LsitUrl2 = new List<GestionDocumentalPQRS2>();
            //    LsitUrl2 = Model.GetUrlProyeccion(idecopPqrs).Where(x => x.id_tipo_documental == 2).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).ToList();

            //    if (LsitUrl2.Count() > 0)
            //    {
            //        foreach (GestionDocumentalPQRS2 x in LsitUrl2)
            //        {
            //            WebClient User = new WebClient();
            //            string filename = x.ruta;
            //            Byte[] FileBuffer = User.DownloadData(filename);

            //            var extension = "";

            //            if (filename.Contains(".pdf"))
            //            {
            //                extension = "application/pdf";
            //            }
            //            else
            //            {
            //                extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //            }

            //            if (FileBuffer != null)
            //            {
            //                Response.ClearContent();
            //                Response.ClearHeaders();
            //                Response.Clear();

            //                Response.ContentType = extension;
            //                Response.AddHeader("content-length", FileBuffer.Length.ToString());
            //                Response.BinaryWrite(FileBuffer);
            //                Response.Flush();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var error = ex.Message;
            //    string rta = "<script LANGUAGE='JavaScript'>" +
            //                         "window.alert('ERROR EN LA DESCARGA');" +
            //                         "</script> ";
            //    Response.Write(rta);
            //    Response.End();
            //}
        }

        public ActionResult GestorUrlArchivoAuditor(Int32 idecopPqrs, MessageResponseOBJ MsgRes)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {
                GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
                dato = Model.GetUrlProyeccion(idecopPqrs).Where(x => x.id_tipo_documental == 6).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).FirstOrDefault();

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";
                    string extensionTipo = obj.tipo;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];


                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    if (!string.IsNullOrEmpty(extensionTipo))
                    {
                        return File(dirpath, extensionTipo, nombreFinal);
                    }
                    else
                    {
                        return File(dirpath, extension, nombreFinal);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }

            return View(Model);
        }

        public ActionResult GestorUrlArchivoAnalista(Int32 idecopPqrs, MessageResponseOBJ MsgRes)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
                dato = Model.GetUrlProyeccion(idecopPqrs).Where(x => x.id_tipo_documental == 5).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).FirstOrDefault();

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";
                    string extensionTipo = obj.tipo;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];


                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    if (!string.IsNullOrEmpty(extensionTipo))
                    {
                        return File(dirpath, extensionTipo, nombreFinal);
                    }
                    else
                    {
                        return File(dirpath, extension, nombreFinal);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }


            //try
            //{
            //    List<GestionDocumentalPQRS2> LsitUrl2 = new List<GestionDocumentalPQRS2>();
            //    LsitUrl2 = Model.GetUrlProyeccion(idecopPqrs).Where(x => x.id_tipo_documental == 5).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).ToList();

            //    var obj = LsitUrl2.FirstOrDefault();
            //    if (obj != null)
            //    {
            //        string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
            //        string filename = obj.ruta;
            //        string extension = "";
            //        string extensionTipo = obj.tipo;

            //        if (filename.Contains(".pdf"))
            //        {
            //            extension = "application/pdf";
            //        }
            //        else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
            //        {
            //            extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        }
            //        else
            //        {
            //            extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //        }

            //        if (!string.IsNullOrEmpty(extensionTipo))
            //        {
            //            return File(dirpath, extensionTipo);
            //        }
            //        else
            //        {
            //            return File(dirpath, extension);
            //        }
            //    }
            //    else
            //    {
            //        return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            //}

        }

        public ActionResult GestorUrlArchivoRespuestaProyectada(Int32 idPqrs)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
                dato = Model.GetUrlProyeccion(idPqrs).Where(x => x.id_tipo_documental == 7).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).FirstOrDefault();

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";
                    string extensionTipo = obj.tipo;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];


                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    if (!string.IsNullOrEmpty(extensionTipo))
                    {
                        return File(dirpath, extensionTipo, nombreFinal);
                    }
                    else
                    {
                        return File(dirpath, extension, nombreFinal);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }

            //try
            //{
            //    List<GestionDocumentalPQRS2> LsitUrl2 = new List<GestionDocumentalPQRS2>();
            //    LsitUrl2 = Model.GetUrlProyeccion(idPqrs).Where(x => x.id_tipo_documental == 7).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).ToList();

            //    foreach (GestionDocumentalPQRS2 x in LsitUrl2)
            //    {
            //        WebClient User = new WebClient();
            //        string filename = x.ruta;
            //        Byte[] FileBuffer = User.DownloadData(filename);

            //        var extension = "";

            //        if (filename.Contains(".pdf"))
            //        {
            //            extension = "application/pdf";
            //        }
            //        else
            //        {
            //            extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //        }

            //        if (FileBuffer != null)
            //        {
            //            Response.ClearContent();
            //            Response.ClearHeaders();
            //            Response.Clear();

            //            Response.ContentType = extension;
            //            Response.AddHeader("content-length", FileBuffer.Length.ToString());
            //            Response.BinaryWrite(FileBuffer);
            //            Response.Flush();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            //    MsgRes.DescriptionResponse = ex.Message;
            //    return View(Model);
            //}

            return View(Model);
        }

        public PartialViewResult _DetallePqrs(Int32? ID)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {
                ViewBag.idrole = SesionVar.ROL;
                ViewData["alerta"] = "";
                Model.id_ecop_PQRS = Convert.ToInt32(ID);
                Model.ConsultaPQRSId(Model.id_ecop_PQRS);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView(Model);
        }

        public ActionResult GestionEnrevision(Int32? ID_PQRS, Models.PQRS.GestionPqrs Model)
        {
            ecop_PQRS_enrevision OBJ = new ecop_PQRS_enrevision();
            ecop_PQRS OBJ2 = new ecop_PQRS();
            try
            {
                Model.ActualizarGestionPQRSEnrevision(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return RedirectToAction("TableroPQRSEnrevision", "PQRS", new { variable = "3" });
        }

        public PartialViewResult _AprobarEnrevision(Int32? ID)
        {

            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";
            Model.id_ecop_PQRA_enrevision = Convert.ToInt32(ID);

            return PartialView(Model);
        }

        [HttpPost]
        public ActionResult _AprobarEnrevision(String observacionaprobo, Models.PQRS.GestionPqrs Model)
        {
            ecop_PQRS_enrevision OBJ = new ecop_PQRS_enrevision();

            try
            {
                OBJ.id_ecop_PQRA_enrevision = Convert.ToInt32(Model.id_ecop_PQRA_enrevision);
                OBJ.observacion_auditor = observacionaprobo;
                OBJ.estado_revision = 2;
                Model.ActualizarGestionPQRSEnrevision(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return RedirectToAction("TableroPQRSEnrevision", "PQRS", new { variable = "2" });
        }

        public PartialViewResult _RechazarEnrevision(Int32? ID)
        {

            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";
            Model.id_ecop_PQRA_enrevision = Convert.ToInt32(ID);

            return PartialView(Model);
        }

        [HttpPost]
        public ActionResult _RechazarEnrevision(String observaciorechazo, Models.PQRS.GestionPqrs Model)
        {
            ecop_PQRS_enrevision OBJ = new ecop_PQRS_enrevision();

            try
            {
                OBJ.id_ecop_PQRA_enrevision = Convert.ToInt32(Model.id_ecop_PQRA_enrevision);
                OBJ.observacion_auditor = observaciorechazo;
                OBJ.estado_revision = 3;
                Model.ActualizarGestionPQRSEnrevision(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return RedirectToAction("TableroPQRSEnrevision", "PQRS", new { variable = "3" });
        }

        public JsonResult ArtefactoProyeccionEnreviasion(Int32? id)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";


            return Json(new { id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GestorUrlArtefactoProyeccion(Int32 id, MessageResponseOBJ MsgRes)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {
                List<ecop_PQRS_enrevision> LsitUrl2 = BusClass.GetPQRSIdEnrevision(id);
                foreach (ecop_PQRS_enrevision x in LsitUrl2)
                {

                    WebClient User = new WebClient();
                    Binary binary = x.pdf_enrevision;
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

                Response.Clear();
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return View(Model);
            }

            return View(Model);
        }

        public ActionResult _AprobadaPQRSEngestion(Int32? idpqrs, Int32? estadogestion, Int32? idpqrsenrevision)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";
            try
            {
                Model.estado_gestion = Convert.ToInt32(estadogestion);
                Model.id_ecop_PQRS = Convert.ToInt32(idpqrs);
                Model.id_ecop_PQRA_enrevision = Convert.ToInt32(idpqrsenrevision);
                Model.ConsultaPQRSId(Model.id_ecop_PQRS);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }
        [HttpPost]
        public ActionResult _AprobadaPQRSEngestion(Models.PQRS.GestionPqrs Model)
        {
            try
            {
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";

                if (Model.fecha_gestionOK != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE GESTION";
                    Conteo = Conteo + 1;
                }

                if (Model.estado_gestion == 3)
                {
                    if (Request.Files["FileUpload1"].ContentLength > 0)
                    {

                    }
                    else
                    {
                        variable = "ERROR";
                        variable2 = "INGRESAR LA PROYECCION EN PDF";
                        Conteo = Conteo + 1;
                    }
                }

                if (Model.estado_gestion == 5)
                {
                    if (Request.Files["FileUpload2"].ContentLength > 0)
                    {

                    }
                    else
                    {
                        variable = "ERROR";
                        variable2 = "INGRESAR EL PDF O WORD A REVISION";
                        Conteo = Conteo + 1;
                    }
                }

                if (Model.id_pqrs_subtematica != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "INGRESAR SUBTEMATICA";
                    Conteo = Conteo + 1;
                }
                if (Model.prestador != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "INGRESAR PRESTADOR";
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
                    Model.ObjPqrs.id_ecop_PQRS = Model.id_ecop_PQRS;
                    Model.ObjPqrs.estado_gestion = Model.estado_gestion;
                    Model.ObjPqrs.id_pqrs_subtematica = Model.id_pqrs_subtematica;
                    //Model.ObjPqrs.prestador = Model.prestador;
                    Model.ObjPqrs.fecha_gestion = Model.fecha_gestionOK;
                    Model.ObjPqrs.evento_adverso = Model.evento_adverso;
                    Model.ObjPqrs.requirio_llamada = Model.requirio_llamada;

                    if (Model.requirio_llamada == "SI")
                    {
                        Model.ObjPqrs.a_quie_llamo = Model.a_quie_llamo;
                        Model.ObjPqrs.nombre_quien_llamo = Model.nombre_quien_llamo;
                    }
                    else
                    {
                        Model.ObjPqrs.a_quie_llamo = 0;
                        Model.ObjPqrs.nombre_quien_llamo = "NA";
                    }

                    Model.ObjPqrs.observacion_gestion = Model.observacion_gestion;

                    //Se selecciona proyectada
                    if (Model.estado_gestion == 3)
                    {
                        Model.ObjPqrs.validez = Model.validez;
                        Model.ObjPqrs.atributo = Model.atributo;
                        Model.ObjPqrs.fecha_envio_prestador = Model.fechaenvioprestador;
                        Model.ObjPqrs.fecha_recepcion_prestador = Model.fecharecepcionprestador;

                        string strInfo = string.Empty;
                        strInfo = cargarArchivos(Model);
                    }

                    //Se selecciona revision
                    if (Model.estado_gestion == 5)
                    {
                        Model.ObjPqrs.validez = Model.validez;
                        Model.ObjPqrs.atributo = Model.atributo;
                        string strInfo = string.Empty;
                        strInfo = cargarArchivosEnrevision(Model);
                    }

                    //Se selecciona en ampliacion
                    else if (Model.estado_gestion == 4)
                    {
                        Model.ObjPqrs.observacion_ampliacion = Model.observacion_ampliacion;
                        Model.ObjPqrs.fecha_ampliacion = Model.fecha_ampliacionok;
                    }

                    else
                    {
                        Model.ObjPqrs.validez = "NA";
                        Model.ObjPqrs.atributo = 0;
                    }

                    Model.ActualizarPQRS(ref MsgRes);
                    ecop_PQRS_enrevision OBJ2 = new ecop_PQRS_enrevision();

                    OBJ2.id_ecop_PQRA_enrevision = Model.id_ecop_PQRA_enrevision;
                    OBJ2.estado_revision = 4;
                    BusClass.ActualizarEstadoEnrevisionpryectada(OBJ2, ref MsgRes);


                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            return RedirectToAction("TableroControl", "Pqrs");
                        }
                        else
                        {
                            ModelState.AddModelError("", "ERROR AL ACTUALIZAR2!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR AL ACTUALIZAR!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View(Model);
        }

        public JsonResult Aprobo(Int32? idpqrs, Int32? estadogestion, String opcionrealizar, Int32? idpqrsenrevision)
        {
            Models.Odontologia.covid19 Model = new Models.Odontologia.covid19();


            return Json(new { idpqrs, estadogestion, opcionrealizar, idpqrsenrevision }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Rechazar(Int32? idpqrs, String opcionrealizar, Int32? idpqrsenrevision)
        {
            Models.Odontologia.covid19 Model = new Models.Odontologia.covid19();

            ecop_PQRS_enrevision OBJ = new ecop_PQRS_enrevision();
            try
            {

                OBJ.id_ecop_PQRS = idpqrs;
                OBJ.id_ecop_PQRA_enrevision = Convert.ToInt32(idpqrsenrevision);
                OBJ.estado_revision = 0;
                BusClass.ActualizarEstadoEnrevisionpryectada(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(new { idpqrs, opcionrealizar }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult EnviarCaso(Int32? ID)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            List<Ref_PQRS_correo_envio> list = new List<Ref_PQRS_correo_envio>();

            try
            {
                list = Model.ConsultaPQRSref_correo();
                ViewBag.Correo = list;
                ViewBag.id_pqrs = ID;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView(Model);
        }

        [HttpPost]
        public ActionResult EnviarCaso(Models.PQRS.GestionPqrs Model)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            try
            {

                byte[] array = new byte[0];
                string textBody = Model.observacion_gestion;
                string filename = "";
                var resultado = true;

                List<GestionDocumentalPQRS2> LsitUrl2 = Model.GetUrlProyeccion(Model.id_ecop_PQRS);

                if (Model.Detalle != null && Model.Detalle.Count() != 0)
                {

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
                        mailBody += "Cordialmente,";
                        mailBody += "</div>";
                        mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                        mailBody += "<br />";
                        mailBody += "<img src='cid:dealer_logo' />";
                        mailBody += "<br />";
                        mailBody += "<STRONG>Marcela Clavijo Gutierrez </STRONG>";
                        mailBody += "<br />";
                        mailBody += "<STRONG>Lider PQRS </STRONG>";
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
                        LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                        resource.ContentId = "dealer_logo";
                        htmlView.LinkedResources.Add(resource);

                        mailMessage.AlternateViews.Add(htmlView);

                        //mailMessage.From = new MailAddress("pqrs.ecopetrol@asalud.co");
                        mailMessage.From = new MailAddress("admin@asaludltda.com");

                        var otroCorreoSolucionador = Model.otro_solucionador_correo;

                        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                        {
                            mailMessage.To.Add("notificacionespqrs@asaludltda.com");

                            //mailMessage.To.Add(Model.correo);

                            if (Model.Detalle != null)
                            {
                                foreach (var item in Model.Detalle)
                                {
                                    mailMessage.To.Add(item.Correo);
                                }
                            }
                        }
                        else
                        {

                            //mailMessage.To.Add("notificacionespqrs@asaludltda.com");

                            //mailMessage.To.Add(Model.correo);
                            if (Model.Detalle != null)
                            {
                                foreach (var item in Model.Detalle)
                                {
                                    mailMessage.To.Add(item.Correo);
                                }
                            }
                        }
                        if (otroCorreoSolucionador != null && otroCorreoSolucionador != "")
                        {
                            mailMessage.To.Add(otroCorreoSolucionador);
                        }

                        foreach (GestionDocumentalPQRS2 x in LsitUrl2)
                        {
                            String adjunto = x.ruta;
                            Attachment oAttch = new Attachment(adjunto);
                            mailMessage.Attachments.Add(oAttch);
                        }

                        //mailMessage.To.Add("pqrs.ecopetrol@asalud.co");

                        mailMessage.Subject = "[Mensaje Automático]" + " " + Model.asunto;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";
                        //MemoryStream memoryStream = new MemoryStream(array);
                        //mailMessage.Attachments.Add(new Attachment(memoryStream, filename));

                        mailMessage.IsBodyHtml = true;
                        objMail.Send(mailMessage);

                        Model.ActualizarEnvioPQRS(Model.id_ecop_PQRS, Convert.ToString(SesionVar.UserName), ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensaje = "PROYECTADA ENVIADA CORRECTAMENTE.";
                            return RedirectToAction("TableroControlProyectadas", "Pqrs", new { rta = 1, msj = mensaje });
                        }
                        else
                        {
                            mensaje = "ERROR AL ENVIAR PROYECTADA.";
                            return RedirectToAction("TableroControlProyectadas", "Pqrs", new { rta = 2, msj = mensaje });

                        }
                        List<Ref_PQRS_correo_envio> list = new List<Ref_PQRS_correo_envio>();

                        list = Model.ConsultaPQRSref_correo();

                        ViewBag.Correo = list;
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                        mensaje = "ERROR AL ENVIAR PROYECTADA.";
                        return RedirectToAction("TableroControlProyectadas", "Pqrs", new { rta = 2, msj = mensaje });
                    }
                }
                else
                {
                    mensaje = "INGRESE CORREOS DE DESTINO...";
                    return RedirectToAction("TableroControlProyectadas", "Pqrs", new { rta = 2, msj = mensaje });

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ENVIAR CORREOS";
                return RedirectToAction("TableroControlProyectadas", "Pqrs", new { rta = 2, msj = mensaje });
            }
        }

        public JsonResult GetCascadeSolucionador(Models.PQRS.GestionPqrs Model)
        {
            List<ref_solucionador> list = new List<ref_solucionador>();
            try
            {
                list = BusClass.getSolucionador(Model.ciudad_del_caso);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(list.Select(p => new { id_solucionador = p.id_ciudad, nombre = p.nombre_solucionador }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeSolucionadorReg(Models.PQRS.GestionPqrs Model)
        {
            List<ref_solucionador> list = new List<ref_solucionador>();
            List<ref_solucionador> listFiltrada = new List<ref_solucionador>();
            var nombreUsuario = SesionVar.NombreUsuario;

            try
            {
                list = BusClass.getSolucionadorReg(Model.regional);
                listFiltrada = list.Where(X => X.nombre_solucionador == nombreUsuario).ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            if (listFiltrada.Count() > 0)
            {
                return Json(listFiltrada.Select(p => new { id_solucionador = p.id_solucionador, nombre = p.nombre_solucionador }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(list.Select(p => new { id_solucionador = p.id_solucionador, nombre = p.nombre_solucionador }), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCascadeTipoIdentificacion(Models.PQRS.GestionPqrs Model)
        {
            List<Ref_tipo_documental> list = new List<Ref_tipo_documental>();
            try
            {
                list = BusClass.GetTipoIdentificacion(ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(list.Select(p => new { id_ref_tipo_documental = p.id_ref_tipo_documental, descripcion = p.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeAuxSolucionador(Models.PQRS.GestionPqrs Model)
        {
            List<Management_PQRS_solucionadoresResult> lista = new List<Management_PQRS_solucionadoresResult>();
            try
            {
                lista = Model.getSolucionadorAux();
                lista = lista.Where(x => x.auxiliar_solucionador != "" || x.nombre_solucionador == "OTRO").ToList();
                lista = lista.Where(x => x.nombre_solucionador == Model.solucionador || x.nombre_solucionador == "OTRO").ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(lista.Select(p => new { id_solucionador = p.id_ciudad, nombre = p.auxiliar_solucionador }), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _ModalGestion(int idanalista)
        {
            List<vw_PQRS_usuarios> list = new List<vw_PQRS_usuarios>();
            try
            {
                list = BusClass.GetRefPqrsUsuarios();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.list = list;
            ViewBag.idanalista = idanalista;

            return PartialView();
        }

        public PartialViewResult _ModalCategorizacion(int idPqr)
        {
            List<Ref_PQRS_categorizacion> list = new List<Ref_PQRS_categorizacion>();
            try
            {
                list = BusClass.ConsultaPQRSCategorizacion();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.list = list;
            ViewBag.pqr = idPqr;

            return PartialView();
        }

        public JsonResult ActualizarAnalista(int id, int analista)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                ecop_PQRS obj = new ecop_PQRS();
                obj.id_ecop_PQRS = id;
                obj.usuario_asignado = analista;

                var pasa = Model.ActualizarUsuarioAsignado(obj, ref MsgRes);

                if (pasa != 0)
                {
                    return Json(new { message = "ANALISTA ACTUALIZADO CORRECTAMENTE", success = true });
                }
                else
                {
                    return Json(new { message = "ERRRO AL ACTUALIZAR EL ANALISTA", success = false });
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return Json(new { message = "ERRRO AL ACTUALIZAR EL ANALISTA", success = false });
            }
        }

        public JsonResult ActualizarCategorizacion(int id, string categoria)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                ecop_PQRS obj = new ecop_PQRS();
                obj.id_ecop_PQRS = id;
                var supersalud = 0;

                if (categoria == "SI")
                {
                    supersalud = 1;
                }
                else
                {
                    supersalud = 0;
                }

                obj.supersalud = supersalud;

                var pasa = Model.ActualizarCategorizacionPQR(obj, ref MsgRes);

                if (pasa != 0)
                {
                    return Json(new { message = "SUPERSALUD ACTUALIZADA CORRECTAMENTE", success = true });
                }
                else
                {
                    return Json(new { message = "ERROR AL ACTUALIZAR LA SUPERSALUD", success = false });
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return Json(new { message = "ERROR AL ACTUALIZAR LA SUPERSALUD", success = false });
            }
        }

        public ActionResult TableroCasosProyeccionesFinales(string numcaso, string numopc, string numdocumento, DateTime? fechainicial, DateTime? fechafinal, Int32? id)
        {
            Models.General General = new Models.General();
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                if (id != null)
                {
                    ViewData["alerta"] = General.MsgRespuesta("succes", "Se abrio correctamente el caso!", "por favor revisar el tablero PQRS");
                }
                else
                {
                    ViewData["alerta"] = "";
                }

                //List<management_pqrs_tablero_control_proyectadasResult> lista = new List<management_pqrs_tablero_control_proyectadasResult>();
                //lista = Model.GestiontableroPQRSProyectadas().Where(x => x.estado_gestion == 7).ToList();

                List<management_pqrs_tablero_control_proyectadasFinalesResult> lista = new List<management_pqrs_tablero_control_proyectadasFinalesResult>();
                List<management_pqrs_tablero_control_proyectadasFinalesResult> listaFinal = new List<management_pqrs_tablero_control_proyectadasFinalesResult>();

                lista = Model.DatosTableroPqrsProyectadasFinales();

                if (!String.IsNullOrEmpty(numcaso))
                {
                    lista = lista.Where(l => l.numero_caso.ToUpper().Contains(numcaso.ToUpper())).ToList();
                }

                if (!String.IsNullOrEmpty(numopc))
                {
                    lista = lista.Where(l => l.consecutivo_caso.ToUpper().Contains(numopc.ToUpper())).ToList();
                }

                if (!String.IsNullOrEmpty(numdocumento))
                {
                    lista = lista.Where(l => l.solicitante_numero_identi.ToUpper().Contains(numdocumento.ToUpper())).ToList();
                }

                if (fechainicial != null)
                {
                    lista = lista.Where(l => l.fecha_gestion != null && l.fecha_gestion.Value.Date >= fechainicial.Value.Date).ToList();
                }

                if (fechafinal != null)
                {
                    lista = lista.Where(l => l.fecha_gestion != null && l.fecha_gestion.Value.Date <= fechafinal.Value.Date).ToList();
                }

                Session["ListaPQRSPF"] = lista;

                ViewBag.listaEstado = lista;
                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;
                ViewBag.numcaso = numcaso;
                ViewBag.numopc = numopc;
                ViewBag.numdocumento = numdocumento;
                ViewBag.fechainicial = fechainicial;
                ViewBag.fechafinal = fechafinal;

                ViewBag.acto = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public PartialViewResult _OpcionProyectada(int ID, string numero_caso)
        {
            ViewBag.id = ID;
            ViewBag.numero_caso = numero_caso;
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            List<vw_ecop_PQRS_DetalleG> listaDetalle = new List<vw_ecop_PQRS_DetalleG>();
            try
            {
                listaDetalle = BusClass.ConsultaPQRSDetalle(ID);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaDetalle = listaDetalle;
            var conteo = listaDetalle.Count();
            ViewBag.conteoDetalle = conteo;

            return PartialView();
        }

        public JsonResult ActualizarProyectada(int id, string observacion, string numero_caso, string radio)
        //public JsonResult ActualizarProyectada(int id, string observacion, string numero_caso, string radio, List<HttpPostedFileBase> file)
        {
            ecop_PQRS obj = new ecop_PQRS();
            var respuesta = 0;
            try
            {
                obj.id_ecop_PQRS = id;
                obj.observacion_aprobacion = observacion;
                obj.usuario_ingreso = SesionVar.UserName;

                if (radio == "si")
                {
                    obj.estado_gestion = 8;
                    obj.fecha_envio_proyectada = DateTime.Now;
                }
                else
                {
                    obj.estado_gestion = 1;
                    obj.fecha_envio_proyectada = null;
                    obj.pasa_auditor = "NO";
                }

                var retorno = BusClass.ActualizarAvanzarProyectada(obj, ref MsgRes);

                if (retorno != 0)
                {
                    //if (file != null)
                    //{
                    //    if (file.Count() > 0)
                    //    {
                    //        for (var i = 0; i < file.Count(); i++)
                    //        {
                    //            respuesta = guardarArchivoRespuestaProyectada(file[i], id, numero_caso);
                    //        }
                    //    }
                    //}
                    return Json(new { message = "REGISTRO GESTIONADO CORRECTAMENTE", success = true });
                }
                else
                {
                    return Json(new { message = "ERRRO AL GESTIONAR PQR", success = true });
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return Json(new { message = "ERRRO AL GESTIONAR PQR", success = false });

            }
        }

        private int guardarArchivoRespuestaProyectada(HttpPostedFileBase file, int idPQR, string numero_caso)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            var respuesta = 0;

            try
            {
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "PQRSRespuestaProyectada";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "PQRSRespuestaProyectadaPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idPQR);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                OBJ.id_ecop_pqr = idPQR;
                OBJ.id_tipo_documental = 7;
                OBJ.numero_caso = numero_caso;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.cargue_fecha = DateTime.Now;
                OBJ.cargue_usuario = SesionVar.UserName;
                OBJ.tipo = file.ContentType;

                respuesta = BusClass.InsertarArchivoPQRRespuestaProyectada(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }

        public void pdfPqr(int idPqr)
        {
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            //RUTA REPORTE

            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptPqrsConceptoAuditor.rdlc");

                //CARGAR LISTA
                List<management_pqrs_datosReporte_conceptoResult> lista = new List<management_pqrs_datosReporte_conceptoResult>();
                lista = BusClass.ReporteConceptoAuditor(idPqr);

                //ASIGNACION  DATASET A REPORT
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPqrsConceptoAuditor", lista);

                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();

                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);

                viewer.LocalReport.EnableHyperlinks = true;

                viewer.LocalReport.Refresh();

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
                var erro = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public ActionResult CreateRadicacion(string msg, int? rta)
        {

            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            var nombreUsuario = "";
            var idUsuario = SesionVar.IDUser;
            var usuario = SesionVar.NombreUsuario;

            List<Ref_tipo_documental> tipoIdentificacion = new List<Ref_tipo_documental>();
            List<Ref_PQRS_tipo_solicitud> ListTipoSolicitud = new List<Ref_PQRS_tipo_solicitud>();
            List<Ref_PQRS_categorizacion> ListTipocatego = new List<Ref_PQRS_categorizacion>();
            List<Ref_regional> RefRegional = new List<Ref_regional>();
            List<Ref_tipo_documental> tipoDocumental = new List<Ref_tipo_documental>();
            ref_solucionador regPropio = new ref_solucionador();
            List<management_pqrs_sinArchivoInicialResult> consolidado = new List<management_pqrs_sinArchivoInicialResult>();

            var conteoLista = 0;
            try
            {
                if (rta == 1 || rta == 2)
                {
                    ViewBag.msg = msg;
                    ViewBag.rta = rta;
                }
                else
                {
                    ViewBag.rta = 0;
                }

                tipoIdentificacion = BusClass.GetTipoIdentificacion(ref MsgRes);
                nombreUsuario = SesionVar.NombreUsuario;
                ListTipoSolicitud = BusClass.GetRefPqrsSolicitud();
                ListTipocatego = BusClass.ConsultaPQRSCategorizacion();
                RefRegional = BusClass.GetRefRegion();
                tipoDocumental = BusClass.GetTipoIdentificacion(ref MsgRes);
                regPropio = BusClass.UltimaRegionalUsuario(usuario);

                consolidado = BusClass.listadoPqrsInicialSinArchivo(idUsuario);
                conteoLista = consolidado.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.fechaActual = DateTime.Now.ToString("MM/dd/yyyy");
            ViewBag.tipoIdentificacion = tipoIdentificacion;
            ViewBag.nombreUsuario = nombreUsuario;
            ViewBag.tipoSolicitud = ListTipoSolicitud;
            ViewBag.tipoCategorizacion = ListTipocatego;
            ViewBag.regional = RefRegional;
            ViewBag.tipoDocumental = tipoDocumental;

            if (regPropio != null)
            {
                ViewBag.regionalPropia = regPropio.id_regional;
            }
            else
            {
                ViewBag.regionalPropia = 0;
            }

            ViewBag.conteoLista = conteoLista;
            ViewBag.consolidado = consolidado;

            return View(Model);
        }

        public PartialViewResult MostrarRepositorioArchivosIngresoInicial(int? idPqr, string numero_caso)
        {
            List<management_pqrs_mirarArchivosResult> lista = new List<management_pqrs_mirarArchivosResult>();
            management_pqrs_mirarArchivosResult dato = new management_pqrs_mirarArchivosResult();
            var conteo = 0;
            var usuarioCreador = "";
            ecop_PQRS datoPqr = new ecop_PQRS();
            try
            {

                datoPqr = BusClass.LlamarPqrsById((int)idPqr);
                lista = BusClass.ArchivosPqrs((int)idPqr).Where(x => x.id_tipo_documental == 8 || x.id_tipo_documental == 14).ToList();

                if (lista.Count() > 0)
                {
                    dato = lista.OrderByDescending(x => x.id_ecop_pqr).FirstOrDefault();
                    if (dato != null)
                    {
                        if (dato.cargue_usuario != null && dato.cargue_usuario != "")
                        {
                            usuarioCreador = dato.cargue_usuario;
                        }
                    }
                }
                conteo = lista.Count();
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idPqr = idPqr;
            ViewBag.lista = lista;
            ViewBag.conteoArchivos = conteo;
            ViewBag.creador = usuarioCreador;
            ViewBag.usuarioActual = SesionVar.UserName;
            ViewBag.rol = SesionVar.ROL;
            ViewBag.numCaso = datoPqr.numero_caso;

            return PartialView();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateRadicacion(Models.PQRS.GestionPqrs Model)
        {
            //List<HttpPostedFileBase> Files;
            var respuestaArchivo = "";
            Int32 Conteo = 0;
            String variable = "";

            String variable2 = "";
            var analista = 0;

            var mensaje = "";
            var rta = 0;

            ViewBag.msg = "";
            ViewBag.rta = 0;

            List<Ref_tipo_documental> tipoIdentificacion = new List<Ref_tipo_documental>();
            List<Ref_PQRS_tipo_solicitud> ListTipoSolicitud = new List<Ref_PQRS_tipo_solicitud>();
            List<Ref_PQRS_categorizacion> ListTipocatego = new List<Ref_PQRS_categorizacion>();
            List<Ref_regional> RefRegional = new List<Ref_regional>();
            List<Ref_tipo_documental> tipoDocumental = new List<Ref_tipo_documental>();
            List<management_pqrs_sinArchivoInicialResult> consolidado = new List<management_pqrs_sinArchivoInicialResult>();
            ref_solucionador regPropio = new ref_solucionador();

            var conteoLista = 0;
            var idUsuario = SesionVar.IDUser;
            var usuario = SesionVar.NombreUsuario;

            try
            {
                tipoIdentificacion = BusClass.GetTipoIdentificacion(ref MsgRes);
                ListTipoSolicitud = BusClass.GetRefPqrsSolicitud();
                //ListTipocatego = BusClass.ConsultaPQRSCategorizacion();
                RefRegional = BusClass.GetRefRegion();
                tipoDocumental = BusClass.GetTipoIdentificacion(ref MsgRes);

                consolidado = BusClass.listadoPqrsInicialSinArchivo(idUsuario);
                conteoLista = consolidado.Count();
                regPropio = BusClass.UltimaRegionalUsuario(usuario);

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.fechaActual = DateTime.Now.ToString("MM/dd/yyyy");
            ViewBag.tipoIdentificacion = tipoIdentificacion;
            ViewBag.tipoSolicitud = ListTipoSolicitud;
            ViewBag.tipoCategorizacion = ListTipocatego;
            ViewBag.regional = RefRegional;
            ViewBag.tipoDocumental = tipoDocumental;

            ViewBag.conteoLista = conteoLista;
            ViewBag.consolidado = consolidado;

            if (regPropio != null)
            {
                ViewBag.regionalPropia = regPropio.id_regional;
            }
            else
            {
                ViewBag.regionalPropia = 0;
            }

            try
            {
                //IList<HttpPostedFileBase> files = Request.Files.GetMultiple("file");

                if (Model.regional != 0)
                {
                    analista = IdAanalistaAsignado(Model.ciudad_del_caso, Model.regional);
                }
                else
                {
                    analista = IdAanalistaAsignado(Model.ciudad_del_caso, Model.regionalOpcion);
                }

                if (Model.fecha_ingreso_buzon_asaludOK == null)
                {
                    variable = "ERROR";
                    variable2 = "Fecha de Ingreso Buzon";
                    Conteo = Conteo + 1;
                }

                if (Model.fecha_ingreso_salesforceOK == null)
                {
                    variable = "ERROR";
                    variable2 = "Fecha Ingreso Sales Force";
                    Conteo = Conteo + 1;
                }

                if (Model.fecha_egreso_salesforceOK == null)
                {
                    variable = "ERROR";
                    variable2 = "Fecha egreso Sales Force";
                    Conteo = Conteo + 1;
                }

                if (Model.regionalOpcion == 0)
                {
                    variable = "ERROR";
                    variable2 = "REGIONAL";
                    Conteo = Conteo + 1;
                }

                if (Model.ciudad_del_caso == 0)
                {
                    variable = "ERROR";
                    variable2 = "CIUDAD";
                    Conteo = Conteo + 1;
                }

                if (Model.solicitante_numero_identi == null || Model.solicitante_numero_identi == "")
                {
                    variable = "ERROR";
                    variable2 = "Número identificación solicitante";
                    Conteo = Conteo + 1;
                }

                if (Model.mismoBeneficiario == 1)
                {
                    if (Model.identificacion_paciente == null || Model.identificacion_paciente == "")
                    {
                        variable = "ERROR";
                        variable2 = "Número identificación paciente:";
                        Conteo = Conteo + 1;
                    }
                }

                if (Conteo == 0)
                {
                    variable = "OK";
                }
                else
                {
                    variable = "ERROR";
                }

                //variable = "ERROR";
                //variable2 = "Los campos de fecha no pueden quedar vacios";

                String fechaforce = Model.fecha_ingreso_salesforceOK.Value.Date.ToString("MM/dd/yyyy");
                String fechaforceok = fechaforce;

                DateTime fechaForce = Convert.ToDateTime(fechaforceok);

                String fechaBuz = Model.fecha_ingreso_buzon_asaludOK.Value.Date.ToString("MM/dd/yyyy");
                String fechaBuzok = fechaBuz + " " + DateTime.Now.ToString("h:mm:ss tt");

                DateTime fechaBuzon = Convert.ToDateTime(fechaBuzok);

                if (fechaForce > Model.fecha_egreso_salesforceOK)
                {
                    variable = "ERROR";
                    variable2 = "Fecha respuesta programada no puede ser menor a fecha ingreso salesforce";
                    Conteo = Conteo + 1;
                }

                if (variable != "ERROR")
                {
                    Model.ObjPqrs.numero_caso = Model.numero_caso;
                    Model.ObjPqrs.consecutivo_caso = Model.consecutivo_caso;
                    Model.ObjPqrs.fecha_ingreso_salesforce = fechaForce;
                    Model.ObjPqrs.fecha_ingreso_buzon_asalud = fechaBuzon;
                    Model.ObjPqrs.tipo_solicitud = Model.tipo_solicitud;
                    Model.ObjPqrs.id_ref_categorizacon = Model.tipo_categorizacion;
                    Model.ObjPqrs.supersalud = Model.superSalud;
                    Model.ObjPqrs.fecha_egreso_salesforce = Model.fecha_egreso_salesforceOK;

                    Model.ObjPqrs.solicitante_nombre = Model.solicitante_nombre;
                    Model.ObjPqrs.tipo_identi_solicitante = Model.tipo_identificacion;
                    Model.ObjPqrs.solicitante_numero_identi = Model.solicitante_numero_identi;

                    if (Model.mismoBeneficiario == 1)
                    {
                        Model.ObjPqrs.tipo_identificacion = Model.tipo_identificacion;
                        Model.ObjPqrs.nombre_paciente = Model.solicitante_nombre;
                        Model.ObjPqrs.numero_identificacion = Model.solicitante_numero_identi;
                    }
                    else
                    {
                        Model.ObjPqrs.tipo_identificacion = Model.tipo_identificacion_paciente;
                        Model.ObjPqrs.nombre_paciente = Model.nombre_paciente;
                        Model.ObjPqrs.numero_identificacion = Model.identificacion_paciente;
                    }

                    Model.ObjPqrs.fecha_asignacion = Convert.ToDateTime(DateTime.Now);
                    //Model.ObjPqrs.regional = Model.regional;
                    Model.ObjPqrs.regional = Model.regionalOpcion;

                    if (Model.ciudad_del_caso != 0 && Model.ciudad_del_caso != null)
                    {
                        Model.ObjPqrs.ciudad_del_caso = Model.ciudad_del_caso;

                        Ref_ciudades ciudad = new Ref_ciudades();
                        ciudad = BusClass.CiudadesId(Model.ciudad_del_caso);

                        if (ciudad != null)
                        {
                            Model.ObjPqrs.ciudad_caso_DANE = Convert.ToInt32(ciudad.Codigo_DANE);
                        }
                    }

                    Model.ObjPqrs.especificacion_solicitud = Model.especificacion_solicitud;
                    Model.ObjPqrs.usuario_asignado = analista;
                    Model.ObjPqrs.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    Model.ObjPqrs.usuario_ingreso = Convert.ToString(SesionVar.UserName);
                    Model.ObjPqrs.estado_gestion = 1;

                    if (Model.solucionador != null && Model.solucionador != "")
                    {
                        Model.ObjPqrs.solucionador = Model.solucionador;
                    }
                    else
                    {
                        Model.ObjPqrs.solucionador = Model.solucionadorLleno;
                    }
                    //Model.ObjPqrs.auditor_asignado = Model.auditor;
                    Model.ObjPqrs.fecha_envio_correo = Convert.ToDateTime(DateTime.Now);
                    Model.ObjPqrs.fecha_envio_correo_direc = Convert.ToDateTime(DateTime.Now);
                    Model.ObjPqrs.otro_solucionador = Model.otro_solucionador;
                    Model.ObjPqrs.id_solicionador = Model.id_solicionador;
                    Model.ObjPqrs.auxiliar_solucionador = Model.auxiliar_solucionador;
                    Model.ObjPqrs.otro_auxiliar_solucionador = Model.otro_auxiliar_solucionador;
                    Model.ObjPqrs.pasa_auditor = null;
                    Model.ObjPqrs.conArchivo = 0;

                    Model.ObjPqrs.tipo_ingreso = 2;

                    Model.id_ecop_PQRS = Model.InsertarPQRS(ref MsgRes);
                    var descripcion = MsgRes.DescriptionResponse;

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {


                        var resultado = agregarConteoAnalista(analista, idUsuario);

                        if (resultado != 0)
                        {
                            if (Model.notificacionesExternas == 1)
                            {
                                ecop_pqrs_envioCorreos correo = new ecop_pqrs_envioCorreos();
                                correo.id_pqrs = Model.id_ecop_PQRS;
                                correo.asunto = Model.asuntoCorreo;
                                correo.cuerpo = Model.cuerpoCorreo;
                                correo.correos_notificar = Model.correosNotificar;
                                correo.copia_notificar = Model.correosCopiar;
                                correo.usuario_digita = SesionVar.UserName;
                                correo.fecha_digita = DateTime.Now;

                                var insertarCorreos = BusClass.insertarDatosCorreos(correo);
                            }

                            //EnviarNotificacionSolucionador(Model, files);
                        }
                        mensaje = "GESTIÓN INGRESADA CORRECTAMENTE";
                        rta = 1;
                    }
                    else
                    {
                        mensaje = "ERROR EN LA INSERCIÓN DE LA GESTIÓN: " + descripcion;
                        rta = 2;
                    }
                }
                else
                {
                    mensaje = "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2;
                    rta = 2;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2;
                rta = 2;
            }

            return RedirectToAction("CreateRadicacion", "Pqrs", new { msg = mensaje, rta = rta });
        }

        public DateTime DevolverFechaHabil(DateTime? fechaIngreso, int? tipoSolicitud)
        {
            DateTime fechaSalesforce = DateTime.MinValue;
            int dias = 0;

            try
            {
                if (tipoSolicitud.HasValue && fechaIngreso.HasValue)
                {
                    switch (tipoSolicitud)
                    {
                        case 4:
                        case 5:
                            dias = 15;
                            break;
                        case 6:
                            dias = 10;
                            break;
                        case 8:
                            dias = 3;
                            break;
                        case 9:
                            dias = 2;
                            break;
                        case 10:
                            dias = 1;
                            break;
                        default:
                            break;
                    }

                    if (dias > 0)
                    {
                        var dato = BusClass.DevolverDiasHabiles(fechaIngreso, dias);
                        if (dato != null)
                        {
                            if (dato.Column1 is DateTime fecha)
                            {
                                fechaSalesforce = fecha;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                // Considera registrar el error en algún log o realizar alguna acción adicional
            }

            return fechaSalesforce;
        }

        public int buscarNumeroCaso(string numeroCaso)
        {
            ecop_PQRS pqr = new ecop_PQRS();
            var respuesta = 0;
            try
            {
                pqr = BusClass.buscarNumeroCasoPqrs(numeroCaso);

                if (pqr != null)
                {
                    respuesta = 1;
                }

            }
            catch (Exception e)
            {
                var error = e.Message;
            }

            return respuesta;

        }

        private string GuardarArchivoIngresoRadicacion(Models.PQRS.GestionPqrs Model, HttpPostedFileBase file)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "PQRSIngresoRadicacion";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "PQRSIngresoRadicacionPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + Model.id_ecop_PQRS);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                OBJ.id_ecop_pqr = Model.id_ecop_PQRS;
                OBJ.id_tipo_documental = 8;
                OBJ.numero_caso = Model.numero_caso;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.cargue_fecha = DateTime.Now;
                OBJ.cargue_usuario = SesionVar.UserName;
                OBJ.tipo = file.ContentType;

                Model.InsertarPQRSProyeccion(OBJ, ref MsgRes);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return strError;
        }

        public PartialViewResult _reaperturaPQR(int? idPqrs, string numeroCaso, int? tipo)
        {
            ViewBag.idPqr = idPqrs;
            ViewBag.numeroCaso = numeroCaso;
            ViewBag.tipo = tipo;
            return PartialView();
        }

        public JsonResult ReabrirCasoPQR(string observacion, int? idPqrs, List<HttpPostedFileBase> file, string numeroCaso, int? tipo)
        {
            var mensaje = "";
            try
            {
                ecop_PQRS pqrs = new ecop_PQRS();
                pqrs.id_ecop_PQRS = (int)idPqrs;
                pqrs.numero_caso = numeroCaso;

                if (tipo == 2)
                {
                    pqrs.observacionReaperturaseguimiento = observacion;
                    pqrs.reabiertoSeguimiento = 1;
                    pqrs.estado_gestion = 8;
                }
                else
                {
                    pqrs.observacionReapertura = observacion;
                    pqrs.reabierto = 1;
                    pqrs.estado_gestion = 1;
                }

                log_pqrs_reaperturas obj = new log_pqrs_reaperturas();
                obj.id_ecop_pqrs = idPqrs;
                obj.observacion = observacion;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.tipo = tipo;

                var resultado = BusClass.ActualizarDatosReaperturaPQR(pqrs);
                if (resultado != 0)
                {
                    var log = BusClass.InsertarLogReaperturaPqrs(obj);

                    if (file != null)
                    {
                        if (file.Count() > 0)
                        {
                            GuardarArchivoReAbrirPqr(idPqrs, file[0], numeroCaso);
                        }
                    }
                    mensaje = "PQR REABIERTO CORRECTAMENTE.";
                    return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    mensaje = "NO SE PUDO REABRIR EL PQR.";
                    return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "NO SE PUDO REABRIR EL CASO: " + error;
                return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        private string GuardarArchivoReAbrirPqr(int? idPqrs, HttpPostedFileBase file, string numeroCaso)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "PQRSReaperturaCaso";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "PQRSReaperturaCasoPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idPqrs);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                OBJ.id_ecop_pqr = (int)idPqrs;
                OBJ.id_tipo_documental = 9;
                OBJ.numero_caso = numeroCaso;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.cargue_fecha = DateTime.Now;
                OBJ.cargue_usuario = SesionVar.UserName;
                OBJ.tipo = file.ContentType;

                var respuesta = BusClass.InsertarArchivoReaperturaPQR(OBJ);
                if (respuesta == 0)
                {
                    strError = "NO SE PUDO GUARDAR ARCHIVO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return strError;
        }

        public void GestorUrlArchivoReabierto(Int32 idPqrs)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {
                List<GestionDocumentalPQRS2> lista = new List<GestionDocumentalPQRS2>();
                lista = Model.GetUrlProyeccion(idPqrs).Where(x => x.id_tipo_documental == 9).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).ToList();

                if (lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY ARCHIVO PARA VISUALIZAR');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                foreach (GestionDocumentalPQRS2 x in lista)
                {
                    WebClient User = new WebClient();
                    string filename = x.ruta;
                    Byte[] FileBuffer = User.DownloadData(filename);

                    var extension = "";
                    extension = x.tipo;

                    if (FileBuffer != null)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();

                        Response.ContentType = extension;
                        Response.AddHeader("content-length", FileBuffer.Length.ToString());
                        Response.BinaryWrite(FileBuffer);
                        Response.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                     "window.alert('ERROR EN LA DESCARGA DE ARCHIVO');" +
                     "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public JsonResult cerrarCasoPqr(int? idPqr)
        {
            string mensaje = "";
            var rta = 1;
            try
            {

                log_pqrs_cerradasSolucionador obj = new log_pqrs_cerradasSolucionador();
                obj.id_ecop_pqrs = idPqr;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                var respuesta = BusClass.InsertarLogCierrePqrsSolucionador(obj);
                if (respuesta != 0)
                {
                    ecop_PQRS obj2 = new ecop_PQRS();
                    obj2.id_ecop_PQRS = (int)idPqr;
                    obj2.cerradoSolucionador = 1;

                    var actualiza = BusClass.CerrarCasoPqrSolucionador(obj2);

                    mensaje = "CASO CERRADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR AL CERRAR EL CASO";
                    rta = 0;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL CERRAR EL CASO: " + error;
                rta = 0;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        /// <summary>
        /// Modificado por: Alexis Quiñones Castillo
        /// Fecha Modificacion: 27 de enero de 2023
        /// </summary>
        /// <param name="idPqrs"></param>
        /// <returns></returns>
        public ActionResult GestorUrlArchivoCierreProyectada(Int32 idPqrs)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {
                GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
                dato = Model.GetUrlProyeccion(idPqrs).Where(x => x.id_tipo_documental == 7).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).FirstOrDefault();

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";
                    string extensionTipo = obj.tipo;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];


                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    if (!string.IsNullOrEmpty(extensionTipo))
                    {
                        return File(dirpath, extensionTipo, nombreFinal);
                    }
                    else
                    {
                        return File(dirpath, extension, nombreFinal);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }

            //try
            //{
            //    List<GestionDocumentalPQRS2> LsitUrl2 = new List<GestionDocumentalPQRS2>();
            //    LsitUrl2 = Model.GetUrlProyeccion(idPqrs).Where(x => x.id_tipo_documental == 7).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).ToList();
            //    GestionDocumentalPQRS2 obj = LsitUrl2.FirstOrDefault();
            //    if (obj != null)
            //    {
            //        string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
            //        string filename = obj.ruta;
            //        string extension = "";

            //        if (filename.Contains(".pdf"))
            //        {
            //            extension = "application/pdf";
            //        }
            //        else if (extension.Contains(".xls") || extension.Contains(".xlsx"))
            //        {
            //            extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        }
            //        else
            //        {
            //            extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //        }
            //        return File(dirpath, extension);
            //    }
            //    else
            //    {
            //        return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            //}
        }

        public ActionResult TableroSeguimientoPqrs()
        {
            List<management_pqrs_TableroseguimientoResult> lista = new List<management_pqrs_TableroseguimientoResult>();
            var rol = SesionVar.ROL;
            var tipoBusqueda = 1;

            try
            {
                tipoBusqueda = (rol == "1" || rol == "15" || rol == "10") ? 1
               : (rol == "2") ? 2
               : 3;

                lista = BusClass.GestiontableeroSeguimientoPqrs(SesionVar.UserName, SesionVar.NombreUsuario, tipoBusqueda, SesionVar.IDUser);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.lista = lista;
            ViewBag.conteo = lista.Count();

            Session["listadoPqrSeguimiento"] = lista;

            return View();
        }

        public ActionResult CargueMasivoPqrs()
        {
            ViewData["MensajeRta"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueMasivoPqrs(List<HttpPostedFileBase> files)
        {
            var mensaje = "";

            GestionPqrs model = new GestionPqrs();

            try
            {
                if (files != null)
                {
                    HttpPostedFileBase file = files[0];

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

                    ecop_pqrs_registroMasivo obj = new ecop_pqrs_registroMasivo();
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    Int32 lote = model.ExcelMasivoPqrs(dataTable, obj, ref MsgRes);

                    var resultado = MsgRes.ResponseType;
                    var mensajeSalida = MsgRes.DescriptionResponse;
                    var idUsuario = SesionVar.IDUser;


                    if (resultado == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        List<ecop_PQRS> Listado = new List<ecop_PQRS>();
                        Listado = BusClass.ListadoPqrsMasivo(lote);

                        if (Listado != null)
                        {
                            if (Listado.Count() > 0)
                            {
                                foreach (var item in Listado)
                                {
                                    try
                                    {
                                        Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
                                        ecop_PQRS obj2 = new ecop_PQRS();

                                        Model.id_ecop_PQRS = item.id_ecop_PQRS;
                                        Model.solucionador = item.solucionador;
                                        Model.numero_caso = item.numero_caso;

                                        var idAnalista = 0;
                                        idAnalista = IdAanalistaAsignado((int)item.ciudad_del_caso, (int)item.regional);

                                        obj2.id_ecop_PQRS = item.id_ecop_PQRS;
                                        obj2.usuario_asignado = idAnalista;

                                        try
                                        {
                                            var actualizado = BusClass.ActualizarAnalistaAsignadoPqr(obj2);
                                            if (actualizado != 0)
                                            {
                                                agregarConteoAnalista(idAnalista, idUsuario);
                                            }
                                        }

                                        catch (Exception ex)
                                        {
                                            var error = ex.Message;
                                        }

                                        try
                                        {
                                            EnviarNotificacionSolucionadorMasivo(Model);
                                        }
                                        catch (Exception ex)
                                        {
                                            var error = ex.Message;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        var error = ex.Message;
                                    }
                                }
                            }
                        }

                        ViewData["MensajeRta"] = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE INGRESÓ CORRECTAMENTE CARGUE MASIVO PQR #" + lote + " </div>";
                    }
                    else
                    {
                        mensaje = "ERROR AL INGRESAR CARGUE PQRS: " + MsgRes.DescriptionResponse;
                        ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                    }
                }
                else
                {
                    mensaje = "SELECCIONE UN ARCHIVO VALIDO.";
                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "NO SE CARGARON LOS REGISTROS:" + error;
            }

            return View();
        }

        public void EnviarNotificacionSolucionadorMasivo(Models.PQRS.GestionPqrs Model)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            ref_solucionador sol = new ref_solucionador();

            try
            {
                var solucionador = Model.solucionador;
                var auxSolucionador = Model.auxiliar_solucionador;

                if (!string.IsNullOrEmpty(solucionador))
                {
                    sol = BusClass.getSolucionadorNombre(solucionador, auxSolucionador);
                }

                if (sol != null)
                {
                    string textBody = "Cordial saludo,";
                    textBody += "<br/>";
                    textBody += "<br/>";

                    textBody += "Apreciado Solucionador, informamos que el caso OPC " + Model.numero_caso + " se ha ingresado a nuestro sistema de quejas y reclamos para realizar la debida gestión, una vez se tenga la respuesta se emitirá una proyección formal por parte de Asalud.";
                    textBody += "<br/>";
                    textBody += "<br/>";
                    textBody += "En el equipo de PQRS nos encontramos a su entera disposición, no solo para la atención de este requerimiento, también para atender los casos que usted considere necesario reportarnos.";
                    textBody += "<br/>";
                    textBody += "<br/>";

                    textBody += "Atentamente,";
                    textBody += "<br/>";
                    textBody += "Asalud";
                    textBody += "<br/>";
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

                        mailBody += "<br />";
                        mailBody += "</div>";
                        mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                        mailBody += "<br />";
                        mailBody += "<img src='cid:dealer_logo' />";
                        mailBody += "<br />";
                        mailBody += "<STRONG>Marcela Clavijo Gutierrez </STRONG>";
                        mailBody += "<br />";
                        mailBody += "<STRONG>Lider PQRS </STRONG>";
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
                        LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                        resource.ContentId = "dealer_logo";
                        htmlView.LinkedResources.Add(resource);

                        mailMessage.AlternateViews.Add(htmlView);

                        mailMessage.From = new MailAddress("admin@asaludltda.com");

                        var otroCorreoSolucionador = Model.otro_solucionador_correo;

                        //mailMessage.To.Add("notificacionespqrs@asaludltda.com");
                        mailMessage.To.Clear();

                        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                        {
                            if (sol.correo_solucionador != null)
                            {
                                if (sol.correo_solucionador != "")
                                {
                                    mailMessage.To.Add(sol.correo_solucionador);
                                }
                            }
                        }
                        else
                        {
                            if (sol.correo_solucionador != null)
                            {
                                if (sol.correo_solucionador != "")
                                {
                                    mailMessage.To.Add(sol.correo_solucionador);
                                    //mailMessage.To.Add("sami.soporte@asaludltda.com");
                                }
                            }
                        }


                        mailMessage.Subject = "[Mensaje Automático]" + "Notificación ingreso Pqr - Número de caso:" + Model.numero_caso;

                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                        mailMessage.IsBodyHtml = true;
                        objMail.Send(mailMessage);

                        //Model.ActualizarEnvioPQRS(Model.id_ecop_PQRS, Convert.ToString(SesionVar.UserName), ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensaje = "NOTIFICACIÓN ENVIADA CORRECTAMENTE.";
                        }
                        else
                        {
                            mensaje = "ERROR AL ENVIAR NOTIFICACIÓN.";
                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                        mensaje = "ERROR AL ENVIAR NOTIFICACIÓN SOLUCIONADOR.";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ENVIAR CORREOS";
            }
        }

        public PartialViewResult ControlCargueMasivo()
        {
            List<management_pqrs_consolidadoMasivoResult> lista = new List<management_pqrs_consolidadoMasivoResult>();
            int idUsuario = 0;
            var conteo = 0;
            try
            {
                idUsuario = SesionVar.IDUser;
                lista = BusClass.PqrsListaMasivos(idUsuario).Where(x => x.existenAbiertos == "SI").ToList(); ;
                conteo = lista.Count();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = lista;
            ViewBag.conteo = conteo;

            return PartialView();
        }

        public ActionResult ControlIngresoArchivoMasivo(int? idMasivo)
        {
            List<management_pqrs_consolidadoMasivo_detalleResult> lista = new List<management_pqrs_consolidadoMasivo_detalleResult>();
            var conteo = 0;
            var idUsuario = SesionVar.IDUser;

            try
            {
                lista = BusClass.PqrsListaMasivosDetalle((int)idMasivo, (int)idUsuario);
                conteo = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.idMasivo = idMasivo;

            return View();
        }

        public PartialViewResult MostrarRepositorioArchivosIngresoMasivo(int? idPqr, string numero_caso, int? idMasivo)
        {
            List<management_pqrs_mirarArchivosResult> lista = new List<management_pqrs_mirarArchivosResult>();
            management_pqrs_mirarArchivosResult dato = new management_pqrs_mirarArchivosResult();
            var conteo = 0;
            var usuarioCreador = "";
            try
            {
                lista = BusClass.ArchivosPqrs((int)idPqr).Where(x => x.id_tipo_documental == 8 || x.id_tipo_documental == 14).ToList();

                if (lista.Count() > 0)
                {
                    dato = lista.OrderByDescending(x => x.id_ecop_pqr).FirstOrDefault();
                    if (dato != null)
                    {
                        if (dato.cargue_usuario != null && dato.cargue_usuario != "")
                        {
                            usuarioCreador = dato.cargue_usuario;
                        }
                    }
                }
                conteo = lista.Count();
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idPqr = idPqr;
            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.creador = usuarioCreador;
            ViewBag.usuarioActual = SesionVar.UserName;
            ViewBag.rol = SesionVar.ROL;
            ViewBag.numCaso = numero_caso;
            ViewBag.idMasivo = idMasivo;


            return PartialView();
        }

        public JsonResult GUardarArchivoDeMasivo(int? idPqr, string numero_caso, string opcion, List<HttpPostedFileBase> files)
        {
            var mensaje = "";
            var rta = 0;

            var ingresanTodos = true;

            try
            {
                if (files != null)
                {
                    if (files.Count() > 0)
                    {
                        try
                        {
                            for (var i = 0; i < files.Count(); i++)
                            {
                                HttpPostedFileBase file = files[i];
                                var respuesta = GuardarArchivoIngresoRadicacionMasivo(idPqr, numero_caso, file, 1, ref MsgRes);
                                if (respuesta == 0)
                                {
                                    ingresanTodos = false;
                                }
                            }

                            if (ingresanTodos)
                            {
                                if (opcion == "SI")
                                {
                                    ecop_PQRS obj = new ecop_PQRS();
                                    obj.id_ecop_PQRS = (int)idPqr;
                                    var actualiza = BusClass.ActualizarPasaArchivoPqrinicial(obj);

                                    mensaje = "PQRS INGRESADO CORRECTAMENTE";
                                }
                                else
                                {
                                    mensaje = "ARCHIVO INGRESADO CORRECTAMENTE";
                                }
                                rta = 1;
                            }
                            else
                            {
                                mensaje = MsgRes.DescriptionResponse;
                                throw new Exception(mensaje);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            throw new Exception(error);
                        }
                    }
                    else
                    {
                        mensaje = "DEBE CARGAR AL MENOS UN ARCHIVO";
                    }
                }
                else
                {
                    //mensaje = "DEBE CARGAR AL MENOS UN ARCHIVO";

                    if (opcion == "SI")
                    {
                        ecop_PQRS obj = new ecop_PQRS();
                        obj.id_ecop_PQRS = (int)idPqr;
                        var actualiza = BusClass.ActualizarPasaArchivoPqrinicial(obj);

                        if (actualiza != 0)
                        {
                            mensaje = "PQRS INGRESADO CORRECTAMENTE";
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ERROR EN LA GESTIÓN";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL CARGUE DE ARCHIVO: " + error;
            }

            return Json(new
            {
                mensaje = mensaje,
                rta = rta,
                idPqr = idPqr
            });
        }

        public JsonResult GuardarArchivoDeInicial(int? idPqr, string numerocaso, string opcion, List<HttpPostedFileBase> files)
        {
            var mensaje = "";
            var rta = 0;
            var secierra = 0;

            var ingresanTodos = true;

            try
            {
                if (files != null)
                {
                    if (files.Count() > 0)
                    {
                        try
                        {
                            for (var i = 0; i < files.Count(); i++)
                            {
                                HttpPostedFileBase file = files[i];
                                var respuesta = GuardarArchivoIngresoRadicacionMasivo(idPqr, numerocaso, file, 2, ref MsgRes);
                                if (respuesta == 0)
                                {
                                    ingresanTodos = false;
                                }
                            }

                            if (ingresanTodos)
                            {
                                if (opcion == "SI")
                                {
                                    ecop_PQRS obj = new ecop_PQRS();
                                    obj.id_ecop_PQRS = (int)idPqr;
                                    var actualiza = BusClass.ActualizarPasaArchivoPqrinicial(obj);
                                    if (actualiza != 0)
                                    {
                                        EnviarNotificacionSolucionador(idPqr);
                                        EnviarNotificacionSolucionadornotificar(idPqr);

                                        var rutaArchivoSolucionador = ConstruirCorreoNotificacionSolucionador(idPqr);

                                        if (rutaArchivoSolucionador != "")
                                        {
                                            var enviaSolucionador = GuardarArchivoNotificacionCorreos(idPqr, numerocaso, rutaArchivoSolucionador, 12);
                                        }
                                        else
                                        {
                                            throw new Exception(MsgRes.DescriptionResponse);
                                        }

                                        var rutaArchivoNotificar = ConstruirCorreoNotificacionNotificarA(idPqr);
                                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                                        {
                                            if (rutaArchivoNotificar != "")
                                            {
                                                var enviaNotificar = GuardarArchivoNotificacionCorreos(idPqr, numerocaso, rutaArchivoNotificar, 13);
                                            }
                                            secierra = 1;
                                        }
                                        else
                                        {
                                            throw new Exception(MsgRes.DescriptionResponse);
                                        }
                                    }
                                }
                                mensaje = "CASO ENVIADO CORRECTAMENTE";
                                rta = 1;
                                EnviarCasoAnalistaAuditor(idPqr, 2);

                            }
                            else
                            {

                                mensaje = MsgRes.DescriptionResponse;
                                throw new Exception(mensaje);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            throw new Exception(error);
                        }
                    }
                    else
                    {
                        mensaje = "DEBE CARGAR AL MENOS UN ARCHIVO";
                    }
                }
                else
                {
                    if (opcion == "SI")
                    {
                        ecop_PQRS obj = new ecop_PQRS();
                        obj.id_ecop_PQRS = (int)idPqr;
                        var actualiza = BusClass.ActualizarPasaArchivoPqrinicial(obj);
                        if (actualiza != 0)
                        {
                            mensaje = "CASO ENVIADO CORRECTAMENTE";
                            rta = 1;

                            EnviarNotificacionSolucionador(idPqr);
                            EnviarNotificacionSolucionadornotificar(idPqr);

                            var rutaArchivoSolucionador = ConstruirCorreoNotificacionSolucionador(idPqr);

                            if (rutaArchivoSolucionador != "")
                            {
                                var enviaSolucionador = GuardarArchivoNotificacionCorreos(idPqr, numerocaso, rutaArchivoSolucionador, 12);
                            }

                            var rutaArchivoNotificar = ConstruirCorreoNotificacionNotificarA(idPqr);
                            if (rutaArchivoNotificar != "")
                            {
                                var enviaNotificar = GuardarArchivoNotificacionCorreos(idPqr, numerocaso, rutaArchivoNotificar, 13);
                            }

                            secierra = 1;

                            EnviarCasoAnalistaAuditor(idPqr, 2);
                        }
                    }
                    else
                    {
                        mensaje = "NO HAY ARCHIVOS PARA INGRESAR, NI SE CIERRA EL CASO.";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL CARGUE DE ARCHIVO: " + error;
            }

            return Json(new
            {
                mensaje = mensaje,
                rta = rta,
                idPqr = idPqr,
                secierra = secierra,
            });
        }

        private int GuardarArchivoNotificacionCorreos(int? idPqr, string numero_caso, string ruta, int? tipo)
        {

            try
            {
                GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                OBJ.id_ecop_pqr = (int)idPqr;
                OBJ.id_tipo_documental = (int)tipo;
                OBJ.numero_caso = numero_caso;
                OBJ.ruta = ruta;
                OBJ.cargue_fecha = DateTime.Now;
                OBJ.cargue_usuario = SesionVar.UserName;
                OBJ.tipo = "application/octet-stream";

                var idCargue = BusClass.InsertarPQRSProyeccion(OBJ, ref MsgRes);
                return idCargue;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public string ConstruirCorreoNotificacionSolucionador(int? idPqr)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            byte[] correoBytes = null; // Variable que contendrá el correo en forma de bytes
            byte[] correoBytes2 = null; // Variable que contendrá el correo en forma de bytes

            string rutaArchivo = "";

            ecop_PQRS obj = new ecop_PQRS();
            management_pqrs_detalleCasoResult detalle = new management_pqrs_detalleCasoResult();
            ref_solucionador sol = new ref_solucionador();
            ref_solucionador auxSol = new ref_solucionador();

            try
            {
                obj = BusClass.LlamarPqrsById((int)idPqr);
                detalle = BusClass.DetallePqrsReporteCorreo(idPqr);

                var solucionador = "";
                var auxsolucionador = "";
                solucionador = obj.solucionador;
                auxsolucionador = obj.auxiliar_solucionador;

                if (!string.IsNullOrEmpty(solucionador))
                {
                    sol = BusClass.getSolucionadorNombre(solucionador, auxsolucionador);
                }

                if (!string.IsNullOrEmpty(solucionador))
                {
                    auxSol = BusClass.TraerAuxSolucionador(auxsolucionador);
                }

                string textBody = "Cordial saludo,";
                textBody += "<br/>";

                //string textBody = "Se ha ingresado como solucionador en Pqr # " + Model.id_ecop_PQRS;
                textBody += "Apreciado Solucionador, informamos que el caso: " + obj.numero_caso + " se ha ingresado a nuestro sistema de quejas y reclamos para realizar la debida gestión, una vez se tenga la respuesta se emitirá una proyección formal por parte de Asalud.";
                textBody += "<br/>";
                textBody += "En el equipo de PQRS nos encontramos a su entera disposición, no solo para la atención de este requerimiento, también para atender los casos que usted considere necesario reportarnos.";
                textBody += "<br/>";

                textBody += "Atentamente,";
                textBody += "<br/>";

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
                mailBody += "</div>";
                mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                mailBody += "<br />";
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    mailBody += "<img src='F:\\GestionDocumentalPQRS\\PQRSCorreosSolucionador\\Asalud.png' width='12%' height='15%'/>";
                }
                else
                {
                    mailBody += "<img src='F:\\GestionDocumentalPQRS\\Asalud.png' width='12%' height='15%'/>";
                }
                mailBody += "<br />";
                mailBody += "<br />";
                mailBody += "<STRONG>ASALUD</STRONG>";
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
                LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                resource.ContentId = "dealer_logo";
                htmlView.LinkedResources.Add(resource);

                mailMessage.AlternateViews.Add(htmlView);

                mailMessage.From = new MailAddress("admin@asaludltda.com");

                var otroCorreoSolucionador = obj.otro_solucionador_correo;

                //mailMessage.To.Add("notificacionespqrs@asaludltda.com");
                mailMessage.To.Clear();

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    if (sol != null)
                    {
                        if (sol.correo_solucionador != null)
                        {
                            if (sol.correo_solucionador != "")
                            {
                                mailMessage.To.Add(sol.correo_solucionador);
                            }
                        }

                        if (auxSol != null)
                        {
                            if (auxSol.correo_auxiliar != "")
                            {
                                mailMessage.To.Add(auxSol.correo_auxiliar);
                            }
                        }

                        mailMessage.To.Add("pqrs.ecopetrol@asalud.co");
                    }
                }

                else
                {
                    mailMessage.To.Add("sami.soporte@asalud.co");
                    mailMessage.To.Add("desarrollo.soporte@asalud.co");
                }

                mailMessage.Subject = "[Mensaje Automático]" + "Notificación ingreso Pqr - Número de caso:" + obj.numero_caso;

                List<GestionDocumentalPQRS2> listaArchivos = new List<GestionDocumentalPQRS2>();

                listaArchivos = BusClass.GetUrlProyeccion(obj.id_ecop_PQRS, ref MsgRes);

                if (listaArchivos != null)
                {
                    if (listaArchivos.Count() > 0)
                    {
                        mailMessage.Attachments.Clear();
                        try
                        {
                            foreach (var item in listaArchivos)
                            {
                                var rutaArc = item.ruta;
                                var nombreCompleto = item.ruta;

                                string dirpathArc = Path.Combine(Request.PhysicalApplicationPath, rutaArc);
                                if (System.IO.File.Exists(dirpathArc))
                                {
                                    string[] nombrePartido = nombreCompleto.Split('\\');
                                    var nombreArchivo = nombrePartido[4];

                                    byte[] bytes = System.IO.File.ReadAllBytes(dirpathArc);

                                    MemoryStream memoryStream = new MemoryStream(bytes);
                                    mailMessage.Attachments.Add(new Attachment(memoryStream, nombreArchivo));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                        }
                    }
                }


                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + "<br>" + mailBody + "</body></HTML>";

                mailMessage.IsBodyHtml = true;

                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;
                string strError = string.Empty;
                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                    carpeta = "PQRSCorreosSolucionador";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                    carpeta = "PQRSCorreosSolucionadorPruebas";
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + "ArchivoCorreo_" + idPqr);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idPqr);
                var nombre = Path.GetFileNameWithoutExtension("ArchivoCorreoSolucionador_" + idPqr + ".EML");
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension("ArchivoCorreoSolucionador_" + idPqr + ".EML"));

                rutaArchivo = Path.Combine(Request.PhysicalApplicationPath, archivo);

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                Models.PQRS.CorreosPqrs correo = new CorreosPqrs();

                objMail.EnableSsl = true;

                var ingresaCorreo = correo.SaveEmailAsEML(mailMessage, rutaArchivo, idPqr, ref MsgRes);

                if (ingresaCorreo == 0)
                {
                    rutaArchivo = "";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

            return rutaArchivo;
        }

        public string ConstruirCorreoNotificacionNotificarA(int? idPqr)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            byte[] correoBytes = null; // Variable que contendrá el correo en forma de bytes
            byte[] correoBytes2 = null; // Variable que contendrá el correo en forma de bytes

            string rutaArchivo = "";
            var mensaje = "";
            ref_solucionador sol = new ref_solucionador();
            var correosAdicion = "";
            var correosCopia = "";
            string[] correosCopias = new string[0];
            string[] correosAdicionales = new string[0];
            var cuerpoCorreo = "";
            var especificacion = "";
            var asuntoCorreo = "";

            ecop_PQRS obj = new ecop_PQRS();
            ecop_pqrs_envioCorreos correo = new ecop_pqrs_envioCorreos();

            management_pqrs_detalleCasoResult detalle = new management_pqrs_detalleCasoResult();

            try
            {
                obj = BusClass.LlamarPqrsById((int)idPqr);
                correo = BusClass.LlamarPqrsCorreoById((int)idPqr);
                detalle = BusClass.DetallePqrsReporteCorreo(idPqr);

                if (correo != null)
                {
                    correosAdicion = correo.correos_notificar;

                    correosAdicionales = correosAdicion.Split(';');
                    cuerpoCorreo = correo.cuerpo;
                    cuerpoCorreo = cuerpoCorreo.Replace("\n", "<br/>");
                    asuntoCorreo = correo.asunto;

                    especificacion = obj.especificacion_solicitud;
                    especificacion = especificacion.Replace("\n", "<br/>");

                    correosCopia = correo.copia_notificar;
                    if (!string.IsNullOrEmpty(correosCopia))
                    {
                        correosCopias = correosCopia.Split(';');
                    }
                }

                var solucionador = "";
                solucionador = obj.solucionador;

                var auxSolucionador = "";
                auxSolucionador = obj.auxiliar_solucionador;

                if (!string.IsNullOrEmpty(solucionador))
                {
                    sol = BusClass.getSolucionadorNombre(solucionador, auxSolucionador);
                }

                if (sol != null)
                {
                    string textBody = "";

                    if (correo != null)
                    {
                        textBody += cuerpoCorreo;
                        textBody += "<br/>";
                        textBody += "<br/>";
                        textBody += " <strong> Especificación del caso: </strong>";
                        textBody += "<br/>";
                        textBody += "<br/>";
                        textBody += "<table border='1'";
                        //textBody += "style='border: solid 1px black; font-family: 'Century Gothic', 'Century Gothic', Sans-Serif; font-size: 10px;'>";
                        textBody += "style='border-collapse: collapse; border: 1px solid black; font-family: \"Century Gothic\", Sans-Serif; font-size: 10px;'>";

                        textBody += "<thead>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>No. de caso sales force:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.numero_caso + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>No. OPC Sales force:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.consecutivo_caso + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Tipo de solicitud:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.descripcionTipoSolicitud + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Ciudad o localidad:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.nombreCiudadCaso + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Regional:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.nombreRegional + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Descripción detallada del caso:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.especificacion_solicitud + "</td>";
                        textBody += "</tr>";
                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Fecha de solicitud:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.fecha_gestion + "</td>";
                        textBody += "</tr>";
                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Solicitante:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.solicitante_nombre + "</td>";
                        textBody += "</tr>";
                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Paciente:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.nombre_paciente + "</td>";
                        textBody += "</tr>";
                        textBody += "</tbody>";
                        textBody += "</table>";

                        textBody += "<br/>";
                        textBody += "<br/>";
                        textBody += "Por favor no responder a este correo ya que fue generado automáticamente. Si es necesario generar una respuesta, por favor dirigirla al correo pqrs.ecopetrol@asalud.co.";
                        textBody += "<br/>";
                        textBody += "<br/>";

                        textBody += "Atentamente,";
                        textBody += "<br/>";
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
                            mailBody += "</div>";
                            mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                            mailBody += "<br />";
                            mailBody += "<img src='F:\\GestionDocumentalPQRS\\Asalud.png' width='12%' height='15%'/>";
                            mailBody += "<br />";
                            mailBody += "<br />";
                            mailBody += "<STRONG>ASALUD SAS</STRONG>";
                            mailBody += "<br />";
                            mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
                            mailBody += "<br />";
                            //mailBody += "Dir. Calle 96 # 13a-03 Piso 4";
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
                            objMail.EnableSsl = true;

                            MailMessage mailMessage = new MailMessage();
                            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody.ToString(), new System.Net.Mime.ContentType("text/html"));
                            LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                            resource.ContentId = "dealer_logo";
                            htmlView.LinkedResources.Add(resource);

                            mailMessage.AlternateViews.Add(htmlView);

                            mailMessage.From = new MailAddress("admin@asaludltda.com");

                            var otroCorreoSolucionador = obj.otro_solucionador_correo;

                            mailMessage.To.Clear();
                            mailMessage.CC.Clear();

                            if (correo != null)
                            {
                                var correosAdicionalesLimpiados = correosAdicionales.Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToList();
                                var correosCopiasLimpiados = correosCopias.Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToList();

                                foreach (var correoAdicional in correosAdicionalesLimpiados)
                                {
                                    mailMessage.To.Add(correoAdicional);
                                }

                                foreach (var correoCopia in correosCopiasLimpiados)
                                {
                                    mailMessage.CC.Add(correoCopia);
                                }

                                //if (correosAdicionales.Count() > 0)
                                //{
                                //    for (var i = 0; i < correosAdicionales.Count(); i++)
                                //    {
                                //        var correoEnviar = correosAdicionales[i];
                                //        correoEnviar = correoEnviar.Replace(" ", "");
                                //        mailMessage.To.Add(correoEnviar);
                                //    }
                                //}

                                //if (correosCopias.Count() > 0)
                                //{
                                //    for (var i = 0; i < correosCopias.Count(); i++)
                                //    {
                                //        var correoCopia = correosCopias[i];
                                //        correoCopia = correoCopia.Replace(" ", "");
                                //        mailMessage.CC.Add(correoCopia);
                                //    }
                                //}
                            }


                            if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                            {
                                mailMessage.To.Add("pqrs.ecopetrol@asalud.co");
                            }
                            else
                            {
                                mailMessage.To.Add("desarrollo.soporte@asaludltda.com");
                                //mailMessage.To.Add("sami.soporte@asalud.co");
                            }

                            mailMessage.Subject = asuntoCorreo;

                            List<GestionDocumentalPQRS2> listaArchivos = new List<GestionDocumentalPQRS2>();

                            listaArchivos = BusClass.GetUrlProyeccion(obj.id_ecop_PQRS, ref MsgRes);
                            if (listaArchivos != null)
                            {
                                if (listaArchivos.Count() > 0)
                                {
                                    mailMessage.Attachments.Clear();
                                    try
                                    {
                                        foreach (var item in listaArchivos)
                                        {
                                            var rutaArc = item.ruta;
                                            var nombreCompleto = item.ruta;

                                            string dirpathArc = Path.Combine(Request.PhysicalApplicationPath, rutaArc);
                                            if (System.IO.File.Exists(dirpathArc))
                                            {
                                                string[] nombrePartido = nombreCompleto.Split('\\');
                                                var nombreArchivo = nombrePartido[4];

                                                byte[] bytes = System.IO.File.ReadAllBytes(dirpathArc);

                                                MemoryStream memoryStream = new MemoryStream(bytes);
                                                mailMessage.Attachments.Add(new Attachment(memoryStream, nombreArchivo));
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        var error = ex.Message;
                                    }
                                }
                            }

                            mailMessage.IsBodyHtml = true;
                            mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + "<br>" + mailBody + "</body></HTML>";

                            mailMessage.IsBodyHtml = true;


                            string strRetorno = string.Empty;
                            StringBuilder sbRutaDefinitiva;
                            string strRutaDefinitiva = string.Empty;
                            string strError = string.Empty;
                            String carpeta = "";

                            if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                            {
                                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                                carpeta = "PQRSCorreosNotificarA";
                            }
                            else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                            {
                                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                                carpeta = "PQRSCorreosNotificarAPruebas";
                            }

                            sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                            string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + "ArchivoCorreo_" + idPqr);
                            string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                            DateTime fecha = DateTime.Now;
                            string archivo = string.Empty;

                            ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idPqr);
                            //ruta = Path.Combine(Request.PhysicalApplicationPath, "C:\\ContratosPrestadores");
                            var nombre = Path.GetFileNameWithoutExtension("ArchivoCorreoNotificarA_" + idPqr + ".EML");
                            archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                            fecha, nombre, Path.GetExtension("ArchivoCorreoNotificarA_" + idPqr + ".EML"));

                            rutaArchivo = Path.Combine(Request.PhysicalApplicationPath, archivo);

                            if (!Directory.Exists(ruta))
                                Directory.CreateDirectory(ruta);

                            Models.PQRS.CorreosPqrs ingresoCorreos = new CorreosPqrs();

                            var ingresaCorreo = ingresoCorreos.SaveEmailAsEML(mailMessage, rutaArchivo, idPqr, ref MsgRes);

                            if (ingresaCorreo == 0)
                            {
                                rutaArchivo = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            throw new Exception(error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return rutaArchivo;
        }

        private int GuardarArchivoIngresoRadicacionMasivo(int? idPqr, string numero_caso, HttpPostedFileBase file, int? tipo, ref MessageResponseOBJ MsgRes)
        {
            if (file != null)
            {
                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;
                string strError = string.Empty;

                try
                {
                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                    {
                        strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                    }
                    else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                    {
                        strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                    }

                    sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                    string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                    DateTime fecha = DateTime.Now;
                    string archivo = string.Empty;

                    String carpeta = "";

                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                    {
                        if (tipo == 1)
                        {
                            carpeta = "PQRSIngresoRadicacionMasivo";
                        }
                        else
                        {
                            carpeta = "PQRSIngresoRadicacion";
                        }
                    }
                    else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                    {
                        if (tipo == 1)
                        {
                            carpeta = "PQRSIngresoRadicacionMasivoPruebas";
                        }
                        else
                        {
                            carpeta = "PQRSIngresoRadicacionPruebas";
                        }


                    }

                    ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idPqr);
                    var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                    archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                    fecha, nombre, Path.GetExtension(file.FileName));


                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    file.SaveAs(archivo);

                    GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                    OBJ.id_ecop_pqr = (int)idPqr;
                    OBJ.id_tipo_documental = 8;
                    OBJ.numero_caso = numero_caso;
                    OBJ.ruta = Convert.ToString(archivo);
                    OBJ.cargue_fecha = DateTime.Now;
                    OBJ.cargue_usuario = SesionVar.UserName;
                    OBJ.tipo = file.ContentType;

                    var idCargue = BusClass.InsertarPQRSProyeccion(OBJ, ref MsgRes);
                    return idCargue;
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public void EnviarNotificacionSolucionador(int? idPqr)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            ref_solucionador sol = new ref_solucionador();
            ref_solucionador auxSol = new ref_solucionador();
            var correosAdicion = "";
            string[] correosAdicionales = new string[0];
            var cuerpoCorreo = "";
            var asuntoCorreo = "";

            ecop_PQRS obj = new ecop_PQRS();
            ecop_pqrs_envioCorreos correo = new ecop_pqrs_envioCorreos();
            management_pqrs_detalleCasoResult detalle = new management_pqrs_detalleCasoResult();
            Ref_PQRS_usuarios userPqr = new Ref_PQRS_usuarios();

            try
            {
                obj = BusClass.LlamarPqrsById((int)idPqr);
                sis_usuario usuarioingresa = BusClass.datosUsuarioId(SesionVar.IDUser);
                var correousuario = usuarioingresa.correo_ins != null ? usuarioingresa.correo_ins : null;
                userPqr = BusClass.GetUsuarioPqrsId(obj.usuario_asignado);

                sis_usuario usuPqr = BusClass.datosUsuarioUser(userPqr.usuario);

                detalle = BusClass.DetallePqrsReporteCorreo(idPqr);

                var solucionador = "";
                var auxsolucionador = "";

                solucionador = obj.solucionador;
                auxsolucionador = obj.auxiliar_solucionador;

                if (!string.IsNullOrEmpty(solucionador))
                {
                    sol = BusClass.getSolucionadorNombre(solucionador, auxsolucionador);
                }

                if (!string.IsNullOrEmpty(solucionador))
                {
                    auxSol = BusClass.TraerAuxSolucionador(auxsolucionador);
                }

                string textBody = "Cordial saludo,";
                textBody += "<br/>";
                textBody += "<br/>";

                //string textBody = "Se ha ingresado como solucionador en Pqr # " + Model.id_ecop_PQRS;
                textBody += "Apreciado Solucionador, informamos que el caso: " + obj.numero_caso + " se ha ingresado a nuestro sistema de quejas y reclamos para realizar la debida gestión, una vez se tenga la respuesta se emitirá una proyección formal por parte de Asalud.";
                textBody += "<br/>";
                textBody += "En el equipo de PQRS nos encontramos a su entera disposición, no solo para la atención de este requerimiento, también para atender los casos que usted considere necesario reportarnos.";
                textBody += "<br/>";

                textBody += "Atentamente,";
                textBody += "<br/>";
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
                    mailBody += "</div>";
                    mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                    mailBody += "<br />";
                    mailBody += "<img src='cid:dealer_logo' />";
                    mailBody += "<br />";

                    if (correo == null)
                    {
                        mailBody += "<STRONG>Marcela Clavijo Gutierrez </STRONG>";
                        mailBody += "<br />";
                        mailBody += "<STRONG>Lider PQRS </STRONG>";
                    }

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
                    LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                    resource.ContentId = "dealer_logo";
                    htmlView.LinkedResources.Add(resource);

                    mailMessage.AlternateViews.Add(htmlView);

                    mailMessage.From = new MailAddress("admin@asaludltda.com");

                    var otroCorreoSolucionador = obj.otro_solucionador_correo;

                    //mailMessage.To.Add("notificacionespqrs@asaludltda.com");
                    mailMessage.To.Clear();


                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                    {
                        if (usuPqr != null)
                        {
                            if (!string.IsNullOrEmpty(usuPqr.correo_ins))
                            {
                                mailMessage.To.Add(sol.correo_solucionador);
                            }
                        }

                        if (sol != null)
                        {
                            if (sol.correo_solucionador != null)
                            {
                                if (sol.correo_solucionador != "")
                                {
                                    mailMessage.To.Add(sol.correo_solucionador);
                                }
                            }

                            if (auxSol != null)
                            {
                                if (auxSol.correo_auxiliar != "")
                                {
                                    mailMessage.To.Add(auxSol.correo_auxiliar);
                                }
                            }

                            if (correousuario != null)
                            {
                                mailMessage.To.Add(correousuario);
                            }

                            mailMessage.To.Add("pqrs.ecopetrol@asalud.co");
                        }
                    }

                    else
                    {
                        if (correousuario != null)
                        {
                            mailMessage.To.Add(correousuario);
                        }

                        mailMessage.To.Add("sami.soporte@asalud.co");
                        mailMessage.To.Add("desarrollo.soporte@asaludltda.com");
                    }

                    mailMessage.Subject = "[Mensaje Automático]" + "Notificación ingreso Pqr - Número de caso:" + obj.numero_caso;

                    List<GestionDocumentalPQRS2> listaArchivos = new List<GestionDocumentalPQRS2>();

                    listaArchivos = BusClass.GetUrlProyeccion(obj.id_ecop_PQRS, ref MsgRes);

                    if (listaArchivos != null)
                    {
                        if (listaArchivos.Count() > 0)
                        {
                            try
                            {
                                foreach (var item in listaArchivos)
                                {
                                    var ruta = item.ruta;
                                    var nombreCompleto = item.ruta;

                                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, ruta);
                                    if (System.IO.File.Exists(dirpath))
                                    {
                                        string[] nombrePartido = nombreCompleto.Split('\\');
                                        var nombreArchivo = nombrePartido[4];

                                        byte[] bytes = System.IO.File.ReadAllBytes(dirpath);

                                        MemoryStream memoryStream = new MemoryStream(bytes);
                                        mailMessage.Attachments.Add(new Attachment(memoryStream, nombreArchivo));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                            }
                        }
                    }

                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                    mailMessage.IsBodyHtml = true;
                    objMail.Send(mailMessage);

                    mensaje = "NOTIFICACIÓN ENVIADA CORRECTAMENTE.";


                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    mensaje = "ERROR AL ENVIAR NOTIFICACIÓN SOLUCIONADOR.";
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ENVIAR CORREOS";
            }
        }

        public void EnviarNotificacionSolucionadornotificar(int? idPqr)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            ref_solucionador sol = new ref_solucionador();
            var correosAdicion = "";
            string[] correosAdicionales = new string[0];
            var cuerpoCorreo = "";
            var especificacion = "";
            var asuntoCorreo = "";
            var correosCopia = "";
            string[] correosCopias = new string[0];

            ecop_PQRS obj = new ecop_PQRS();
            ecop_pqrs_envioCorreos correo = new ecop_pqrs_envioCorreos();

            management_pqrs_detalleCasoResult detalle = new management_pqrs_detalleCasoResult();

            try
            {
                obj = BusClass.LlamarPqrsById((int)idPqr);
                correo = BusClass.LlamarPqrsCorreoById((int)idPqr);
                detalle = BusClass.DetallePqrsReporteCorreo(idPqr);

                if (correo != null)
                {
                    correosAdicion = correo.correos_notificar;
                    correosAdicionales = correosAdicion.Split(';');
                    cuerpoCorreo = correo.cuerpo;
                    cuerpoCorreo = cuerpoCorreo.Replace("\n", "<br/>");
                    asuntoCorreo = correo.asunto;

                    especificacion = obj.especificacion_solicitud;
                    especificacion = especificacion.Replace("\n", "<br/>");

                    correosCopia = correo.copia_notificar;
                    if (!string.IsNullOrEmpty(correosCopia))
                    {
                        correosCopias = correosCopia.Split(';');
                    }
                }

                var solucionador = "";
                solucionador = obj.solucionador;

                var auxSolucionador = "";
                auxSolucionador = obj.auxiliar_solucionador;

                if (!string.IsNullOrEmpty(solucionador))
                {
                    sol = BusClass.getSolucionadorNombre(solucionador, auxSolucionador);
                }

                if (sol != null)
                {
                    string textBody = "";

                    if (correo != null)
                    {
                        textBody += cuerpoCorreo;
                        textBody += "<br/>";
                        textBody += "<br/>";
                        textBody += " <strong> Especificación del caso: </strong>";
                        textBody += "<br/>";
                        textBody += "<br/>";
                        textBody += "<table border='1'";
                        //textBody += "style='border: solid 1px black; font-family: 'Century Gothic', 'Century Gothic', Sans-Serif; font-size: 10px;'>";
                        textBody += "style='border-collapse: collapse; border: 1px solid black; font-family: \"Century Gothic\", Sans-Serif; font-size: 10px;'>";

                        textBody += "<thead>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>No. de caso sales force:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.numero_caso + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>No. OPC Sales force:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.consecutivo_caso + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Tipo de solicitud:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.descripcionTipoSolicitud + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Ciudad o localidad:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.nombreCiudadCaso + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Regional:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.nombreRegional + "</td>";
                        textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Descripción detallada del caso:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.especificacion_solicitud + "</td>";
                        textBody += "</tr>";
                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Fecha de solicitud:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.fecha_gestion + "</td>";
                        textBody += "</tr>";
                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Solicitante:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.solicitante_nombre + "</td>";
                        textBody += "</tr>";
                        textBody += "<tr>";
                        textBody += "<td class='text-center'style='background:#1f3864; color:white;'><strong>Paciente:</strong></td> <td class='text-center' style='background:white; color:#626262;font-family: 'Century Gothic', 'Century Gothic', Sans-Serif;'>" + detalle.nombre_paciente + "</td>";
                        textBody += "</tr>";
                        textBody += "</tbody>";
                        textBody += "</table>";

                        textBody += "<br/>";
                        textBody += "<br/>";
                        textBody += "Por favor no responder a este correo ya que fue generado automáticamente. Si es necesario generar una respuesta, por favor dirigirla al correo pqrs.ecopetrol@asalud.co.";
                        textBody += "<br/>";
                        textBody += "<br/>";

                        textBody += "Atentamente,";
                        textBody += "<br/>";
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
                            mailBody += "</div>";
                            mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                            mailBody += "<br />";
                            mailBody += "<img src='cid:dealer_logo' />";
                            mailBody += "<br />";

                            mailBody += "<br />";
                            mailBody += "<STRONG>ASALUD SAS</STRONG>";
                            mailBody += "<br />";
                            mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
                            mailBody += "<br />";
                            //mailBody += "Dir. Calle 96 # 13a-03 Piso 4";
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

                            mailMessage.From = new MailAddress("admin@asaludltda.com");

                            var otroCorreoSolucionador = obj.otro_solucionador_correo;

                            //mailMessage.To.Add("notificacionespqrs@asaludltda.com");
                            mailMessage.To.Clear();

                            mailMessage.To.Clear();
                            mailMessage.CC.Clear();

                            if (correo != null)
                            {
                                var correosAdicionalesLimpiados = correosAdicionales.Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToList();
                                var correosCopiasLimpiados = correosCopias.Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToList();

                                foreach (var correoAdicional in correosAdicionalesLimpiados)
                                {
                                    mailMessage.To.Add(correoAdicional);
                                }

                                foreach (var correoCopia in correosCopiasLimpiados)
                                {
                                    mailMessage.CC.Add(correoCopia);
                                }
                            }

                            if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                            {
                                mailMessage.To.Add("pqrs.ecopetrol@asalud.co");
                            }
                            else
                            {
                                mailMessage.To.Add("desarrollo.soporte@asaludltda.com");
                                mailMessage.To.Add("sami.soporte@asalud.co");
                            }

                            mailMessage.Subject = asuntoCorreo;

                            List<GestionDocumentalPQRS2> listaArchivos = new List<GestionDocumentalPQRS2>();

                            listaArchivos = BusClass.GetUrlProyeccion(obj.id_ecop_PQRS, ref MsgRes);

                            if (listaArchivos != null)
                            {
                                if (listaArchivos.Count() > 0)
                                {
                                    try
                                    {
                                        foreach (var item in listaArchivos)
                                        {
                                            var ruta = item.ruta;
                                            var nombreCompleto = item.ruta;

                                            string dirpath = Path.Combine(Request.PhysicalApplicationPath, ruta);
                                            if (System.IO.File.Exists(dirpath))
                                            {
                                                string[] nombrePartido = nombreCompleto.Split('\\');
                                                var nombreArchivo = nombrePartido[4];

                                                byte[] bytes = System.IO.File.ReadAllBytes(dirpath);

                                                MemoryStream memoryStream = new MemoryStream(bytes);
                                                mailMessage.Attachments.Add(new Attachment(memoryStream, nombreArchivo));
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        var error = ex.Message;
                                    }
                                }
                            }

                            mailMessage.IsBodyHtml = true;
                            mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                            mailMessage.IsBodyHtml = true;
                            objMail.Send(mailMessage);

                            mensaje = "NOTIFICACIÓN ENVIADA CORRECTAMENTE.";

                            //Model.ActualizarEnvioPQRS(Model.id_ecop_PQRS, Convert.ToString(SesionVar.UserName), ref MsgRes);

                            //if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                            //{
                            //    mensaje = "NOTIFICACIÓN ENVIADA CORRECTAMENTE.";
                            //}
                            //else
                            //{
                            //    mensaje = "ERROR AL ENVIAR NOTIFICACIÓN.";
                            //}

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            mensaje = "ERROR AL ENVIAR NOTIFICACIÓN SOLUCIONADOR.";
                            throw new Exception(error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ENVIAR CORREOS";
            }
        }

        public void descargarDatosPqrsSeguimiento()
        {
            List<management_pqrs_TableroseguimientoResult> lista = (List<management_pqrs_TableroseguimientoResult>)Session["listadoPqrSeguimiento"];

            try
            {
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("SeguimientoPqrs");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:I1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:I1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:I1"].Style.Font.Size = 10;
                    //Sheet.Cells["A1:I1"].Style.WrapText = true;
                    Sheet.Cells["A1:I1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:I1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "ID PQR";
                    Sheet.Cells["B1"].Value = "NÚMERO CASO";
                    Sheet.Cells["C1"].Value = "CIUDAD";
                    Sheet.Cells["D1"].Value = "ASIGNACIÓN";
                    Sheet.Cells["E1"].Value = "FECHA RESPUESTA PROGRAMADA SALESFORCE";
                    Sheet.Cells["F1"].Value = "ESTADO";
                    Sheet.Cells["G1"].Value = "USUARIO REAPERTURA CASO";
                    Sheet.Cells["H1"].Value = "OBSERVACIÓN REAPERTURA CASO";
                    Sheet.Cells["I1"].Value = "FECHA REAPERTURA CASO";

                    int row = 2;

                    foreach (management_pqrs_TableroseguimientoResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":I" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_ecop_PQRS;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.numero_caso;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.ciudad_del_caso_descripcion;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_asignacion;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_egreso_salesforce;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.estadoDescripcion;


                        Sheet.Cells[string.Format("G{0}", row)].Value = item.usuarioReapertura;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.observacionReapertura;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.fechaReapertura;

                        Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;


                        row++;
                    }

                    string namefile = "Tablero seguimiento";
                    Sheet.Cells["A:I"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS PARA VISUALIZAR');" +
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

        /// <summary>
        /// Modificado por: Alexis Quiñones Castillo
        /// Fecha Modificacion: 27 de enero de 2023
        /// </summary>
        /// <param name="idPqrs"></param>
        /// <returns></returns>
        public ActionResult GestorUrlArchivoIngresoPqr(Int32 idPqrs)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            try
            {
                GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
                dato = Model.GetUrlProyeccion(idPqrs).Where(x => x.id_tipo_documental == 8).OrderByDescending(x => x.id_gestion_documental_pqrs).Take(1).FirstOrDefault();

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";
                    string extensionTipo = obj.tipo;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];


                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    if (!string.IsNullOrEmpty(extensionTipo))
                    {
                        return File(dirpath, extensionTipo, nombreFinal);
                    }
                    else
                    {
                        return File(dirpath, extension, nombreFinal);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public int devolverFecha(DateTime fecha)
        {
            var devolucion = 0;
            try
            {
                DateTime fechaDevuelta = new DateTime();
                DateTime fechaActual = DateTime.Now;

                if (fecha < fechaActual)
                {
                    devolucion = 1;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return devolucion;
        }

        public ActionResult TableroControlProyectadasCierre(string numcaso, string numopc, string numdocumento, DateTime? fechainicial, DateTime? fechafinal, Int32? id, int? idPqr)
        {
            Models.General General = new Models.General();
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                if (id != null)
                {
                    ViewData["alerta"] = General.MsgRespuesta("succes", "Se abrió correctamente el caso!", "por favor revisar el tablero PQRS");
                }
                else
                {
                    ViewData["alerta"] = "";
                }

                List<management_pqrs_proyectadasCierreResult> lista = new List<management_pqrs_proyectadasCierreResult>();

                lista = Model.DatosTableroPqrsProyectadasCierre();

                if (!String.IsNullOrEmpty(numcaso))
                {
                    lista = lista.Where(l => l.numero_caso.ToUpper().Contains(numcaso.ToUpper())).ToList();
                }

                if (!String.IsNullOrEmpty(numopc))
                {
                    lista = lista.Where(l => l.consecutivo_caso.ToUpper().Contains(numopc.ToUpper())).ToList();
                }

                if (!String.IsNullOrEmpty(numdocumento))
                {
                    lista = lista.Where(l => l.solicitante_numero_identi.ToUpper().Contains(numdocumento.ToUpper())).ToList();
                }

                if (fechainicial != null)
                {
                    lista = lista.Where(l => l.fecha_gestion != null && l.fecha_gestion.Value.Date >= fechainicial.Value.Date).ToList();
                }

                if (fechafinal != null)
                {
                    lista = lista.Where(l => l.fecha_gestion != null && l.fecha_gestion.Value.Date <= fechafinal.Value.Date).ToList();
                }

                if (idPqr != null && idPqr != 0)
                {
                    lista = lista.Where(l => l.id_ecop_PQRS == idPqr).ToList();
                }

                Session["ListaPQRSCierre"] = lista;

                ViewBag.listaEstado = lista;
                ViewBag.rol = SesionVar.ROL;
                ViewBag.idusuario = SesionVar.IDUser;
                ViewBag.numcaso = numcaso;
                ViewBag.numopc = numopc;
                ViewBag.numdocumento = numdocumento;
                ViewBag.fechainicial = fechainicial;
                ViewBag.fechafinal = fechafinal;

                ViewBag.acto = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public JsonResult ActualizarGestionMasivaCierre(string opcion, string observacionMasivo, String[][] ids)
        {
            var mensaje = "";
            var rta = 0;

            List<ecop_PQRS> objs = new List<ecop_PQRS>();
            var conteoFinal = ids.Length;
            var conteoRecorrido = 0;

            try
            {

                for (var i = 0; i < ids.Count(); i++)
                {
                    var id = Convert.ToString(ids[i][0]);
                    var numCaso = Convert.ToString(ids[i][1]);

                    var idFil = 0;

                    if (!string.IsNullOrEmpty(id))
                    {
                        idFil = Convert.ToInt32(id);
                    }

                    ecop_PQRS obj = new ecop_PQRS();
                    obj.id_ecop_PQRS = idFil;
                    obj.observacion_aprobacionCierre = observacionMasivo;
                    obj.usuario_ingreso = SesionVar.UserName;

                    if (opcion == "si")
                    {
                        obj.estado_gestion = 3;
                        obj.reabierto = 0;
                        obj.fecha_envio_proyectadaCierre = DateTime.Now;
                    }

                    else
                    {
                        obj.estado_gestion = 1;
                        obj.reabierto = 0;
                        obj.fecha_envio_proyectadaCierre = null;
                        obj.devuelto_en_cierre = 1;
                    }

                    var retorno = BusClass.ActualizarCerrarProyectada(obj, ref MsgRes);

                    if (retorno != 0)
                    {
                        conteoRecorrido++;

                        if (obj.estado_gestion == 3)
                        {
                            EnviarNotificacionSolucionadorPqraProyectada(idFil, numCaso);
                        }
                    }
                }

                if (conteoFinal == conteoRecorrido)
                {
                    mensaje = "REGISTROS(S) GESTIONADO(S) CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL GESTIONAR PQR";
                }
            }

            catch (Exception ex)
            {
                var erro = ex.Message;
                mensaje = "ERROR AL GESTIONAR PQRS: " + erro;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public PartialViewResult MostrarRepositorioArchivos(int? idPqrs, int? tipo)
        {
            List<management_pqrs_mirarArchivosResult> lista = new List<management_pqrs_mirarArchivosResult>();
            management_pqrs_mirarArchivosResult dato = new management_pqrs_mirarArchivosResult();
            var conteo = 0;
            var usuarioCreador = "";
            try
            {
                //Archivos ya mandados a proyectadas por analista y proyecciones finales - re abiertos
                if (tipo == 1)
                {
                    //lista = BusClass.ArchivosPqrs((int)idPqrs).Where(x => x.id_tipo_documental == 7 || x.id_tipo_documental == 9 || x.id_tipo_documental == 10 || x.id_tipo_documental == 11).ToList();
                    lista = BusClass.ArchivosPqrs((int)idPqrs).ToList();
                }

                //Archivos de ingreso pqrs
                else if (tipo == 2)
                {
                    lista = BusClass.ArchivosPqrs((int)idPqrs).Where(x => x.id_tipo_documental == 2 || x.id_tipo_documental == 8 || x.id_tipo_documental == 14).ToList();
                }

                //Archivos de analista a revisión
                else if (tipo == 5)
                {
                    lista = BusClass.ArchivosPqrs((int)idPqrs).Where(x => x.id_tipo_documental == 5 || x.id_tipo_documental == 14 || x.id_tipo_documental == 14).ToList();
                }

                //Archivos de respuesta auditor

                else if (tipo == 6)
                {
                    lista = BusClass.ArchivosPqrs((int)idPqrs).Where(x => x.id_tipo_documental == 6 || x.id_tipo_documental == 14).ToList();
                }

                //Archivos de analista a proyectadas
                else if (tipo == 7)
                {
                    lista = BusClass.ArchivosPqrs((int)idPqrs).Where(x => x.id_tipo_documental == 2 || x.id_tipo_documental == 7 || x.id_tipo_documental == 10 || x.id_tipo_documental == 14).ToList();
                }

                //Archivos del cargue masivo sin finalizar
                else if (tipo == 8)
                {
                    lista = BusClass.ArchivosPqrs((int)idPqrs).Where(x => x.id_tipo_documental == 8 || x.id_tipo_documental == 14).ToList();
                }

                //Archivos que se visualizan en tablero dairo
                else if (tipo == 9)
                {
                    lista = BusClass.ArchivosPqrs((int)idPqrs).Where(x => x.id_tipo_documental == 7 || x.id_tipo_documental == 14).ToList();
                    tipo = 7;
                }

                //Archivos ingresados en la gestion analista - proyectada
                else if (tipo == 10)
                {
                    lista = BusClass.ArchivosPqrs((int)idPqrs).Where(x => x.id_tipo_documental == 2 || x.id_tipo_documental == 14).ToList();
                    tipo = 2;
                }

                if (lista.Count() > 0)
                {
                    dato = lista.OrderByDescending(x => x.id_ecop_pqr).FirstOrDefault();
                    if (dato != null)
                    {
                        if (dato.cargue_usuario != null && dato.cargue_usuario != "")
                        {
                            usuarioCreador = dato.cargue_usuario;
                        }
                    }
                }
                conteo = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.idPqrsArchivo = idPqrs;
            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.creador = usuarioCreador;
            ViewBag.usuarioActual = SesionVar.UserName;
            ViewBag.rol = SesionVar.ROL;
            ViewBag.tipoArchivo = tipo;

            return PartialView();
        }

        public PartialViewResult MostrarRepositorioArchivosCerrado(int? idPqrs, int? tipo)
        {
            List<management_pqrs_mirarArchivosResult> lista = new List<management_pqrs_mirarArchivosResult>();
            management_pqrs_mirarArchivosResult dato = new management_pqrs_mirarArchivosResult();
            var conteo = 0;
            var usuarioCreador = "";
            var tipoFinal = 0;

            try
            {
                if (tipo != null)
                {
                    tipoFinal = (int)tipo;
                }

                lista = BusClass.ArchivosPqrs((int)idPqrs);
                if (lista.Count() > 0)
                {
                    dato = lista.OrderByDescending(x => x.id_ecop_pqr).FirstOrDefault();
                    if (dato != null)
                    {
                        if (dato.cargue_usuario != null && dato.cargue_usuario != "")
                        {
                            usuarioCreador = dato.cargue_usuario;
                        }
                    }
                }
                conteo = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.idPqrs = idPqrs;
            ViewBag.lista = lista;
            ViewBag.conteo = conteo;
            ViewBag.creador = usuarioCreador;
            ViewBag.usuarioActual = SesionVar.UserName;
            ViewBag.rol = SesionVar.ROL;

            //Si es tipo uno, puede gestionar archivos, sino, es para tablero seguimiento
            ViewBag.tipo = tipoFinal;

            return PartialView();
        }

        public PartialViewResult MostrarRepositorioArchivosCerradoSeguimiento(int? idPqrs, int? tipo)
        {
            List<management_pqrs_mirarArchivosResult> lista = new List<management_pqrs_mirarArchivosResult>();
            List<management_pqrs_mirarArchivosResult> listaConFiltro = new List<management_pqrs_mirarArchivosResult>();
            List<management_pqrs_mirarArchivosResult> listaSinFiltro = new List<management_pqrs_mirarArchivosResult>();
            management_pqrs_mirarArchivosResult dato = new management_pqrs_mirarArchivosResult();
            var conteo = 0;
            var usuarioCreador = "";
            var tipoFinal = 0;

            try
            {

                if (tipo != null)
                {
                    tipoFinal = (int)tipo;
                }

                lista = BusClass.ArchivosPqrs((int)idPqrs);
                if (lista.Count() > 0)
                {
                    listaConFiltro = lista.Where(x => x.nombreArchivo.ToUpper().Contains("PF")).ToList();
                    listaSinFiltro = lista.Where(x => !x.nombreArchivo.ToUpper().Contains("PF")).ToList();

                    dato = lista.OrderByDescending(x => x.id_ecop_pqr).FirstOrDefault();
                    if (dato != null)
                    {
                        if (dato.cargue_usuario != null && dato.cargue_usuario != "")
                        {
                            usuarioCreador = dato.cargue_usuario;
                        }
                    }
                }
                conteo = lista.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.idPqrs = idPqrs;
            ViewBag.lista = lista;
            ViewBag.listaCon = listaConFiltro;
            ViewBag.listaSin = listaSinFiltro;
            ViewBag.conteo = conteo;
            ViewBag.conteoCon = conteo;
            ViewBag.conteoSin = conteo;
            ViewBag.creador = usuarioCreador;
            ViewBag.usuarioActual = SesionVar.UserName;
            ViewBag.rol = SesionVar.ROL;

            //Si es tipo uno, puede gestionar archivos, sino, es para tablero seguimiento
            ViewBag.tipo = tipoFinal;

            return PartialView();
        }

        public PartialViewResult MostrarDetalleGestionesPqrs(int? idPqrs, int? vieneCierre)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();
            List<vw_PQRS_usuarios> usuarios = new List<vw_PQRS_usuarios>();
            List<Ref_regional> regional = new List<Ref_regional>();
            List<Ref_ciudades> ciudad = new List<Ref_ciudades>();
            try
            {
                Model.id_ecop_PQRS = Convert.ToInt32(idPqrs);

                Model.ConsultaPQRSId(Model.id_ecop_PQRS);
                usuarios = BusClass.GetRefPqrsUsuarios().Where(x => x.id_ref_PQRS_usuarios == Model.usuario_asignado).ToList();
                regional = BusClass.GetRefRegion().Where(x => x.id_ref_regional == Model.regional).ToList();
                ciudad = BusClass.GetCiudades().Where(x => x.id_ref_ciudades == Model.ciudad_del_caso).ToList();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.usuarios = usuarios;
            ViewBag.regional = regional;
            ViewBag.ciudad = ciudad;
            ViewBag.idPqrs = idPqrs;
            ViewBag.vieneCierre = vieneCierre;


            return PartialView(Model);

        }

        public void ExportarLstpqrsProyectadasCierre()
        {
            List<management_pqrs_proyectadasCierreResult> Lista = new List<management_pqrs_proyectadasCierreResult>();
            string proye;

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                Lista = (List<management_pqrs_proyectadasCierreResult>)Session["ListaPQRSCierre"];
                proye = "Cierre_Proyectadas";

                if (Lista == null || Lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(proye);

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:N1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:N1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:N1"].Style.Font.Size = 10;
                    //Sheet.Cells["A1:K1"].Style.WrapText = true;
                    Sheet.Cells["A1:N1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:N1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:N1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "ID PQR";
                    Sheet.Cells["B1"].Value = "NÚMERO CASO";
                    Sheet.Cells["C1"].Value = "NÚMERO OPERACIÓN";
                    Sheet.Cells["D1"].Value = "FECHA GESTIÓN";
                    Sheet.Cells["E1"].Value = "NUM IDENTIFICACIÓN SOLICITANTE";
                    Sheet.Cells["F1"].Value = "ANALISTA ASIGNADO";
                    Sheet.Cells["G1"].Value = "OBSERVACIÓN APROBADA";
                    Sheet.Cells["H1"].Value = "USUARIO PROYECTA";

                    Sheet.Cells["I1"].Value = "USUARIO REAPERTURA CASO SEGUIMIENTO";
                    Sheet.Cells["J1"].Value = "OBSERVACIÓN REAPERTURA CASO SEGUIMIENTO";
                    Sheet.Cells["K1"].Value = "FECHA REAPERTURA CASO SEGUIMIENTO";

                    Sheet.Cells["L1"].Value = "USUARIO REAPERTURA CASO GESTIÓN";
                    Sheet.Cells["M1"].Value = "OBSERVACIÓN REAPERTURA CASO GESTIÓN";
                    Sheet.Cells["N1"].Value = "FECHA REAPERTURA CASO GESTIÓN";

                    int row = 2;

                    foreach (management_pqrs_proyectadasCierreResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":N" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_ecop_PQRS;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.numero_caso;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.consecutivo_caso;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_gestion;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.solicitante_numero_identi;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.nombreAsignado;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.observacion_aprobacion;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nombreProyecta;

                        Sheet.Cells[string.Format("I{0}", row)].Value = item.usuarioReaperturaSeg;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.observacionReaperturaSeg;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.fechaReaperturaSeg;


                        Sheet.Cells[string.Format("L{0}", row)].Value = item.usuarioReaperturaGes;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.observacionReaperturaGes;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.fechaReaperturaGes;


                        Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("K{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile;
                    namefile = "CierreProyectadas";

                    Sheet.Cells["A:N"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
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

        public ActionResult VerArchivoIngresoPqr(Int32 idArchivo)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
                dato = BusClass.traerArchivoPqr(idArchivo);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";
                    string extensionTipo = obj.tipo;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];


                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    if (!string.IsNullOrEmpty(extensionTipo))
                    {
                        return File(dirpath, extensionTipo, nombreFinal);
                    }
                    else
                    {
                        return File(dirpath, extension, nombreFinal);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public ActionResult VerArchivoIngresoPqrId(Int32 idPqrs)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
                dato = BusClass.traerArchivoPqrId(idPqrs);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";
                    string extensionTipo = obj.tipo;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];


                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    if (!string.IsNullOrEmpty(extensionTipo))
                    {
                        return File(dirpath, extensionTipo, nombreFinal);
                    }
                    else
                    {
                        return File(dirpath, extension, nombreFinal);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }


            try
            {
                GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
                dato = BusClass.traerArchivoPqrId(idPqrs);

                if (dato == null)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('ERROR EN LA DESCARGA');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                WebClient User = new WebClient();
                string filename = dato.ruta;
                Byte[] FileBuffer = User.DownloadData(filename);

                string[] tamaño = filename.Split('\\');

                var conteo = tamaño.Count();
                var nombreArchivo = tamaño[conteo - 1];

                var extension = "";
                extension = dato.tipo;

                var extension2 = "";

                if (filename.Contains(".pdf"))
                {
                    extension2 = "application/pdf";
                }
                else
                {
                    extension2 = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                }

                if (FileBuffer != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();
                    if (extension == null)
                    {
                        Response.ContentType = extension2;
                    }
                    else
                    {
                        Response.ContentType = extension;
                    }

                    //Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo);

                    Response.BinaryWrite(FileBuffer);
                    Response.Flush();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                     "window.alert('ERROR EN LA DESCARGA');" +
                     "</script> ";
                Response.Write(rta);
                Response.End();
            }

            return View(Model);
        }

        public JsonResult InsertarArchivosRepositorio(int? idPqrs, List<HttpPostedFileBase> archivos, int? tipoArchivo)
        {
            var mensaje = "";
            var rta = 0;
            var resultado = 0;
            var lineaDañada = 0;
            var dañado = 0;
            var numeroCaso = "";

            ecop_PQRS dato = new ecop_PQRS();

            try
            {
                dato = BusClass.LlamarPqrsById((int)idPqrs);

                if (dato != null)
                {
                    numeroCaso = dato.numero_caso;
                }

                if (archivos != null)
                {
                    if (archivos.Count() > 0)
                    {
                        for (var i = 0; i < archivos.Count(); i++)
                        {
                            resultado = GuardarArchivosRepositorioInicial(archivos[i], (int)idPqrs, numeroCaso, (int)tipoArchivo);
                            lineaDañada = i + 1;
                            if (resultado == 0)
                            {
                                i = archivos.Count();
                                dañado = 1;
                            }
                        }

                        if (dañado == 0)
                        {
                            mensaje = "ARCHIVO(S) INSERTADO(S) CORRECTAMENTE.";
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ERROR EN LA INSERCIÓN DEL ARCHIVOS #" + lineaDañada;
                            rta = 0;
                        }
                    }
                    else
                    {
                        mensaje = "SELECCIONE ARCHIVO A INSERTAR.";
                        rta = 0;
                    }
                }
                else
                {
                    mensaje = "SELECCIONE ARCHIVO A INSERTAR.";
                    rta = 0;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA INSERCIÓN DE ARCHIVOS: " + error;
                rta = 0;
            }

            return Json(new { rta = rta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //private int guardarArchivosRepositorio(HttpPostedFileBase file, int idPQR, string numero_caso, int tipo)
        //{
        //    string strRetorno = string.Empty;
        //    StringBuilder sbRutaDefinitiva;
        //    string strRutaDefinitiva = string.Empty;
        //    var respuesta = 0;

        //    try
        //    {
        //        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
        //        {
        //            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
        //        }
        //        else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
        //        {
        //            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
        //        }

        //        sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
        //        string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
        //        string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

        //        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        //        DateTime fecha = DateTime.Now;
        //        string archivo = string.Empty;

        //        String carpeta = "";

        //        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
        //        {
        //            //Tablero de cierre proyectadas
        //            if (tipo == 1)
        //            {
        //                carpeta = "PQRSCierreProyectada";
        //            }

        //            //Archivos desde gestión analista
        //            else if (tipo == 2)
        //            {
        //                carpeta = "PQRSGestionAnalistas";
        //            }

        //            //Tablero proyectadas ya cerradas
        //            else if (tipo == 3)
        //            {
        //                carpeta = "PQRSProyectadaCerrada";
        //            }

        //            //Archivos desde gestión analista - 
        //            else if (tipo == 5)
        //            {
        //                carpeta = "PQRSGestionAnalistasRevision";
        //            }

        //            //Respuesta auditor
        //            else if (tipo == 6)
        //            {
        //                carpeta = "PQRSRespuestaAuditor";
        //            }

        //            else if (tipo == 8)
        //            {
        //                carpeta = "IngresoPQR";
        //            }
        //            else
        //            {
        //                carpeta = "PQRS";
        //            }
        //        }
        //        else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
        //        {
        //            //Tablero de cierre proyectadas
        //            if (tipo == 1)
        //            {
        //                carpeta = "PQRSCierreProyectadaPruebas";
        //            }

        //            //Archivos desde gestión analista
        //            else if (tipo == 2)
        //            {
        //                carpeta = "PQRSGestionAnalistasPruebas";
        //            }

        //            //Archivos desde gestión analista - revisión


        //            //Tablero proyectadas ya cerradas
        //            else if (tipo == 3)
        //            {
        //                carpeta = "PQRSProyectadaCerradaPruebas";
        //            }

        //            else if (tipo == 5)
        //            {
        //                carpeta = "PQRSGestionAnalistasRevisionPruebas";
        //            }

        //            //Archivos desde respuesta auditor
        //            else if (tipo == 6)
        //            {
        //                carpeta = "PQRSRespuestaAuditorPruebas";
        //            }


        //            else if (tipo == 8)
        //            {
        //                carpeta = "IngresoPQRPruebas";
        //            }

        //            else
        //            {
        //                carpeta = "PQRSPruebas";
        //            }
        //        }

        //        ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idPQR);
        //        var nombre = Path.GetFileNameWithoutExtension(file.FileName);
        //        archivo = String.Format("{0}\\{1:yyyyMMdd_HHmmss}_{2}{3}", ruta,
        //        fecha, nombre, Path.GetExtension(file.FileName));

        //        if (!Directory.Exists(ruta))
        //            Directory.CreateDirectory(ruta);

        //        //file.SaveAs(archivo);

        //        using (var fileStream = new FileStream(archivo, FileMode.Create, FileAccess.Write))
        //        {
        //            file.InputStream.CopyTo(fileStream);
        //        }

        //        GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();
        //        OBJ.id_ecop_pqr = idPQR;

        //        //Tablero de cierre proyectadas
        //        if (tipo == 1)
        //        {
        //            OBJ.id_tipo_documental = 10;
        //        }

        //        //Archivos desde gestión analista - proyectadas
        //        else if (tipo == 2)
        //        {
        //            OBJ.id_tipo_documental = 2;
        //        }

        //        //Tablero proyectadas ya cerradas
        //        else if (tipo == 3)
        //        {
        //            OBJ.id_tipo_documental = 11;
        //        }

        //        else if (tipo == 5)
        //        {
        //            OBJ.id_tipo_documental = 5;
        //        }

        //        else if (tipo == 6)
        //        {
        //            OBJ.id_tipo_documental = 6;
        //        }

        //        //Archivos de ingreso y en gestion analista
        //        else if (tipo == 8)
        //        {
        //            OBJ.id_tipo_documental = 8;
        //        }

        //        else if (tipo == 7)
        //        {
        //            OBJ.id_tipo_documental = 7;
        //        }

        //        OBJ.numero_caso = numero_caso;
        //        OBJ.ruta = Convert.ToString(archivo);
        //        OBJ.cargue_fecha = DateTime.Now;
        //        OBJ.cargue_usuario = SesionVar.UserName;
        //        OBJ.tipo = file.ContentType;

        //        respuesta = BusClass.PqrInsertarArchivoRepositorioCierre(OBJ, ref MsgRes);
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return respuesta;
        //}

        private int GuardarArchivosRepositorioInicial(HttpPostedFileBase file, int idPQR, string numero_caso, int tipo)
        {
            try
            {
                string bdActiva = ConfigurationManager.AppSettings["BDActiva"].ToString();

                var strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                var carpeta = ObtenerCarpetaPorTipoYBD(tipo, bdActiva);
                var rutaBase = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva, carpeta, idPQR.ToString());
                var archivo = GenerarNombreArchivo(rutaBase, file.FileName);

                CrearDirectorioSiNoExiste(rutaBase);
                GuardarArchivoEnRuta(file, archivo);

                return GuardarEnGestionDocumental(idPQR, numero_caso, archivo, file.ContentType, tipo);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: registrar el error o lanzarlo nuevamente según sea necesario
                var error = ex.Message;
                throw new Exception(error);
            }
        }

        private string ObtenerCarpetaPorTipoYBD(int tipo, string bdActiva)
        {
            if (bdActiva == "1")
            {
                switch (tipo)
                {
                    case 1: return "PQRSCierreProyectada";
                    case 2: return "PQRSGestionAnalistas";
                    case 3: return "PQRSProyectadaCerrada";
                    case 5: return "PQRSGestionAnalistasRevision";
                    case 6: return "PQRSRespuestaAuditor";
                    case 8: return "IngresoPQR";
                    default: return "PQRS";
                }
            }
            else if (bdActiva == "2")
            {
                switch (tipo)
                {
                    case 1: return "PQRSCierreProyectadaPruebas";
                    case 2: return "PQRSGestionAnalistasPruebas";
                    case 3: return "PQRSProyectadaCerradaPruebas";
                    case 5: return "PQRSGestionAnalistasRevisionPruebas";
                    case 6: return "PQRSRespuestaAuditorPruebas";
                    case 8: return "IngresoPQRPruebas";
                    default: return "PQRSPruebas";
                }
            }

            return string.Empty;
        }

        private string GenerarNombreArchivo(string rutaBase, string nombreArchivoOriginal)
        {
            var fecha = DateTime.Now;
            var nombre = Path.GetFileNameWithoutExtension(nombreArchivoOriginal);
            return Path.Combine(rutaBase, $"{fecha:yyyyMMdd_HHmmss}_{nombre}{Path.GetExtension(nombreArchivoOriginal)}");
        }

        private void CrearDirectorioSiNoExiste(string ruta)
        {
            try
            {
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception(error);
            }
        }

        private void GuardarArchivoEnRuta(HttpPostedFileBase file, string rutaArchivo)
        {
            try
            {
                using (var fileStream = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write))
                {
                    file.InputStream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception(error);
            }
        }

        private int GuardarEnGestionDocumental(int idPQR, string numero_caso, string archivo, string tipoArchivo, int tipo)
        {
            var idInserta = 0;
            try
            {

                var OBJ = new GestionDocumentalPQRS2
                {
                    id_ecop_pqr = idPQR,
                    id_tipo_documental = ObtenerTipoDocumental(tipo),
                    numero_caso = numero_caso,
                    ruta = archivo,
                    cargue_fecha = DateTime.Now,
                    cargue_usuario = SesionVar.UserName,
                    tipo = tipoArchivo
                };

                var MsgRes = new MessageResponseOBJ();
                idInserta = BusClass.PqrInsertarArchivoRepositorioCierre(OBJ, ref MsgRes);
                return idInserta;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception(error);
            }
        }

        private int ObtenerTipoDocumental(int tipo)
        {
            try
            {

                switch (tipo)
                {
                    case 1: return 10;
                    case 2: return 2;
                    case 3: return 11;
                    case 5: return 5;
                    case 6: return 6;
                    case 8: return 8;
                    case 7: return 7;
                    default: return 0;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception(error);
            }
        }

        public JsonResult EliminarArchivoPqrCierre(int? idArchivo, int? idPqrs)
        {
            String mensaje = "";
            try
            {
                var resultado = BusClass.eliminarArchivoPqrsidArchivo((int)idArchivo);

                if (resultado != 0)
                {
                    log_ecop_pqrs_eliminarArchivos obj = new log_ecop_pqrs_eliminarArchivos();
                    obj.id_archivo = idArchivo;
                    obj.id_ecop_pqrs = idPqrs;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    var eliminacion = BusClass.GuardarLogEliminarArchivoPqr(obj);

                    mensaje = "SE ELIMINÓ CORRECTAMENTE.";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "ERROR AL ELIMINAR :" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EliminarArchivoPqrCierreMasivo(int? idPqr, List<string> listadoId, string[] listadoId2)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                if (listadoId.Count() > 0)
                {
                    for (var i = 0; i < listadoId.Count(); i++)
                    {
                        var idArchivo = listadoId[i];

                        var resultado = BusClass.eliminarArchivoPqrsidArchivo((Convert.ToInt32(idArchivo)));

                        if (resultado != 0)
                        {
                            log_ecop_pqrs_eliminarArchivos obj = new log_ecop_pqrs_eliminarArchivos();
                            obj.id_archivo = (Convert.ToInt32(idArchivo));
                            obj.id_ecop_pqrs = idPqr;
                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            var eliminacion = BusClass.GuardarLogEliminarArchivoPqr(obj);

                            mensaje = "SE ELIMINÓ CORRECTAMENTE.";
                        }
                        else
                        {
                            mensaje = "ERROR AL ELIMINAR ARCHIVO CON ID: " + idArchivo;
                            return Json(new { mensaje = mensaje, rta = 2 });
                        }
                    }
                    return Json(new { mensaje = mensaje, rta = 1 });
                }
                else
                {
                    mensaje = "NO HAY ARCHIVOS PARA ELIMINAR";
                    return Json(new { mensaje = mensaje, rta = 2 });

                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ELIMINACIÓN DE ARCHIVOS";
                return Json(new { mensaje = mensaje, rta = 2 });
            }

        }

        public PartialViewResult _OpcionCerrarProyectada(int ID, string numero_caso)
        {
            ViewBag.id = ID;
            ViewBag.numero_caso = numero_caso;
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            List<vw_ecop_PQRS_DetalleG> listaDetalle = new List<vw_ecop_PQRS_DetalleG>();
            try
            {
                listaDetalle = BusClass.ConsultaPQRSDetalle(ID);
                Model.ConsultaPQRSId(Model.id_ecop_PQRS);

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaDetalle = listaDetalle;
            var conteo = listaDetalle.Count();
            ViewBag.conteoDetalle = conteo;

            return PartialView(Model);
        }

        public JsonResult ActualizarCerrarProyectada(int id, string observacion, string numero_caso, string radio)
        {
            ecop_PQRS obj = new ecop_PQRS();
            var mensaje = "";
            var pasa = true;
            var devolucion = 0;
            try
            {
                obj.id_ecop_PQRS = id;
                obj.observacion_aprobacionCierre = observacion;
                obj.usuario_ingreso = SesionVar.UserName;
                obj.numero_caso = numero_caso;

                if (radio == "si")
                {
                    obj.estado_gestion = 3;
                    obj.reabierto = 0;
                    obj.fecha_envio_proyectadaCierre = DateTime.Now;
                    devolucion = 1; //Avanza
                }

                else
                {
                    obj.estado_gestion = 1;
                    obj.reabierto = 0;
                    obj.fecha_envio_proyectadaCierre = null;
                    obj.devuelto_en_cierre = 1;
                    devolucion = 2; //Se devuelve a tablero anterior
                }

                var retorno = BusClass.ActualizarCerrarProyectada(obj, ref MsgRes);

                if (retorno != 0)
                {
                    if (obj.estado_gestion == 3)
                    {
                        EnviarNotificacionSolucionadorPqraProyectada(id, numero_caso);
                    }
                    mensaje = "REGISTRO GESTIONADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR AL GESTIONAR PQR";
                    pasa = false;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                mensaje = "ERROR AL GESTIONAR PQR: " + error;
                pasa = false;
            }


            return Json(new { message = mensaje, success = pasa, devolucion = devolucion });

        }

        public void EnviarNotificacionSolucionadorPqraProyectada(int? idPqr, string numero_caso)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            ref_solucionador sol = new ref_solucionador();

            ecop_PQRS obj = new ecop_PQRS();
            var solucionador = "";
            var auxSolucionador = "";


            try
            {
                obj = BusClass.LlamarPqrsById((int)idPqr);

                if (obj != null)
                {
                    solucionador = obj.solucionador;
                    auxSolucionador = obj.auxiliar_solucionador;
                }

                if (!string.IsNullOrEmpty(solucionador))
                {
                    sol = BusClass.getSolucionadorNombre(solucionador, auxSolucionador);
                }

                if (sol != null)
                {
                    string textBody = "Cordial saludo,";
                    textBody += "<br/>";
                    textBody += "Se ha generado la proyección final del caso OPC " + numero_caso + " ingresado a nuestro sistema de quejas y reclamos, podrá realizar el descargue a través de la plataforma https://ecopetrol.aplicativoasalud.co/";
                    textBody += "<br/>";
                    textBody += "<br/>";
                    textBody += "Atentamente,";
                    textBody += "<br/>";
                    textBody += "Asalud";
                    textBody += "<br/>";

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
                        mailBody += "</div>";
                        mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                        mailBody += "<br />";
                        mailBody += "<img src='cid:dealer_logo' />";
                        mailBody += "<br />";
                        mailBody += "<STRONG>Marcela Clavijo Gutierrez </STRONG>";
                        mailBody += "<br />";
                        mailBody += "<STRONG>Lider PQRS </STRONG>";
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
                        LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                        resource.ContentId = "dealer_logo";
                        htmlView.LinkedResources.Add(resource);

                        mailMessage.AlternateViews.Add(htmlView);

                        mailMessage.From = new MailAddress("admin@asaludltda.com");

                        //mailMessage.To.Add("notificacionespqrs@asaludltda.com");
                        mailMessage.To.Clear();

                        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                        {
                            if (sol.correo_solucionador != null)
                            {
                                if (sol.correo_solucionador != "")
                                {
                                    mailMessage.To.Add(sol.correo_solucionador);
                                }
                            }
                        }
                        else
                        {
                            if (sol.correo_solucionador != null)
                            {
                                if (sol.correo_solucionador != "")
                                {
                                    mailMessage.To.Add(sol.correo_solucionador);
                                    //mailMessage.To.Add("sami.soporte@asaludltda.com");
                                }
                            }
                        }

                        mailMessage.Subject = "[Mensaje Automático]" + "Notificación ingreso Pqr - Número de caso:" + idPqr;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                        mailMessage.IsBodyHtml = true;
                        objMail.Send(mailMessage);

                        //Model.ActualizarEnvioPQRS(Model.id_ecop_PQRS, Convert.ToString(SesionVar.UserName), ref MsgRes);


                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensaje = "NOTIFICACIÓN ENVIADA CORRECTAMENTE.";
                        }
                        else
                        {
                            mensaje = "ERROR AL ENVIAR NOTIFICACIÓN.";
                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                        mensaje = "ERROR AL ENVIAR NOTIFICACIÓN SOLUCIONADOR.";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ENVIAR CORREOS";
            }
        }

        public JsonResult GuardarObservacionAuditor(string observacion, int? idPqr)
        {
            var mensaje = "";
            var rta = 0;
            ecop_pqrs_observacionesAuditor obj = new ecop_pqrs_observacionesAuditor();
            try
            {
                obj.observacion = observacion;
                obj.id_ecop_pqrs = idPqr;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                var resultado = BusClass.PqrGuardarObservaciopnAuditor(obj);
                if (resultado != 0)
                {
                    mensaje = "OBSERVACIÓN INGRESADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE OBSERVACIÓN";
                    rta = 2;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE OBSERVACIÓN: " + error;
                rta = 2;
            }

            List<Ref_PQRS_estado_Gestion> PqrsEstadoGest = new List<Ref_PQRS_estado_Gestion>();
            PqrsEstadoGest = BusClass.GetRefPqrsGestion();
            ViewBag.listEstados = PqrsEstadoGest;

            return Json(new { msg = mensaje, rta = rta, idPqr = idPqr }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult traeraQuienLlamar()
        {
            var miproyecto = "";
            var listatotal = new object();
            List<Ref_PQRS_llamada> PqrsLLamada = new List<Ref_PQRS_llamada>();
            try
            {
                PqrsLLamada = BusClass.GetRefPqrsLLamada();

                ViewBag.Lista = PqrsLLamada;
                listatotal = (from item in PqrsLLamada
                              select new
                              {
                                  value = item.id_ref_PQRS_llamada,
                                  text = item.descripcion,
                              });
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(listatotal, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult TraerAuditores(Models.PQRS.GestionPqrs Model, int? regional, int? ciudad, string cargo)
        //{
        //    var miproyecto = "";
        //    var listatotal = new object();
        //    List<sis_usuario> ListaAuditores = new List<sis_usuario>();
        //    List<sis_usuario> ListaAuditoresFinal = new List<sis_usuario>();
        //    List<sis_auditor_regional_pqrs> listadoCoordinadores = new List<sis_auditor_regional_pqrs>();

        //    try
        //    {
        //        if (regional != null && ciudad != null && cargo != "")
        //        {
        //            ListaAuditores = Model.GetListAuditorCiudad((int)regional, (int)ciudad).Distinct().ToList();
        //            listadoCoordinadores = BusClass.listadoCoordinadoresPqrs();

        //            List<int> listaCargos = cargo.Split(',').Select(int.Parse).ToList();

        //            if (listaCargos.Count() > 0)
        //            {
        //                foreach (var item in listaCargos)
        //                {
        //                    List<sis_usuario> ListaAudit = new List<sis_usuario>();
        //                    ListaAudit = ListaAuditores.Where(x => x.id_rol_cargo == item || x.id_usuario == listadoCoordinadores.Select(l=>l.id_usuario).FirstOrDefault).Distinct().ToList();



        //                    if (ListaAudit.Count() > 0)
        //                    { 
        //                        ListaAuditoresFinal.AddRange(ListaAudit.Distinct().ToList());
        //                    }
        //                }
        //            }
        //        }

        //        ViewBag.ListaAudi = ListaAuditoresFinal;
        //        listatotal = (from item in ListaAuditoresFinal
        //                      select new
        //                      {
        //                          value = item.id_usuario,
        //                          text = item.nombre,
        //                      });
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return Json(listatotal, JsonRequestBehavior.AllowGet);
        //}


        //public JsonResult TraerAuditores(Models.PQRS.GestionPqrs Model, int? regional, int? ciudad, string cargo)
        //{
        //    List<sis_usuario> listaAuditoresFinal = new List<sis_usuario>();
        //    List<sis_auditor_regional_pqrs> listadoCoordinadores = BusClass.listadoCoordinadoresPqrs();
        //    //List<sis_auditor_regional_pqrs> listadoCoordinadores = BusClass.listadoCoordinadoresPqrs().Where(x=>x.id_usuario == SesionVar.IDUser).ToList();
        //    List<management_pqrs_busquedaAudirotesRegionalResult> listadoTotalAuditores = new List<management_pqrs_busquedaAudirotesRegionalResult>();
        //    try
        //    {
        //        if (regional.HasValue && ciudad.HasValue && !string.IsNullOrEmpty(cargo))
        //        {
        //            var listaAuditores = Model.GetListAuditorCiudad(regional.Value, ciudad.Value).Distinct().ToList();

        //            List<int> listaCargos = cargo.Split(',').Select(int.Parse).ToList();

        //            listaAuditoresFinal = listaCargos
        //                .SelectMany(item => listaAuditores
        //                    .Where(x => x.id_rol_cargo == item || x.id_usuario == listadoCoordinadores.Select(l => l.id_usuario).FirstOrDefault())
        //                    .Distinct())
        //                .Distinct()
        //                .ToList();
        //        }

        //        ViewBag.ListaAudi = listaAuditoresFinal;
        //        var listatotal = listaAuditoresFinal.Select(item => new
        //        {
        //            value = item.id_usuario,
        //            text = item.nombre,
        //        });

        //        return Json(listatotal);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Consider logging the exception here for better debugging
        //        var error = ex.Message;
        //        // Return an appropriate error response if needed
        //        return Json(new { success = false, message = error });
        //    }
        //}


        public JsonResult TraerAuditores(Models.PQRS.GestionPqrs Model, int? regional, int? ciudad, string cargo)
        {
            List<management_pqrs_busquedaAudirotesRegionalResult> listadoTotalAuditores = new List<management_pqrs_busquedaAudirotesRegionalResult>();
            try
            {
                if (regional.HasValue && ciudad.HasValue && !string.IsNullOrEmpty(cargo))
                {
                    List<int> listaCargos = cargo.Split(',').Select(int.Parse).ToList();

                    foreach (var item in listaCargos)
                    {
                        List<management_pqrs_busquedaAudirotesRegionalResult> listadoTotaltemporal = BusClass.BusquedaAuditoresPqrs(regional, item);
                        if (listadoTotaltemporal.Count() > 0)
                        {
                            listadoTotalAuditores.AddRange(listadoTotaltemporal);
                        }
                    }
                }

                ViewBag.ListaAudi = listadoTotalAuditores;
                var listatotal = listadoTotalAuditores.Select(item => new
                {
                    value = item.id_usuario,
                    text = item.nombre,
                });

                return Json(listatotal);
            }
            catch (Exception ex)
            {
                // Consider logging the exception here for better debugging
                var error = ex.Message;
                // Return an appropriate error response if needed
                return Json(new { success = false, message = error });
            }
        }

        public JsonResult TraeraPrestadores()
        {
            var miproyecto = "";
            var listatotal = new object();
            List<Ref_ips_cuentas> listaPrestadores = new List<Ref_ips_cuentas>();
            try
            {
                listaPrestadores = BusClass.GetPrstadorCuentas();

                ViewBag.Lista = listaPrestadores;
                listatotal = (from item in listaPrestadores
                              select new
                              {
                                  value = item.id_ref_ips_cuentas,
                                  text = item.Nombre,
                              });
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(listatotal, JsonRequestBehavior.AllowGet);
        }

        public void EnviarCasoAnalistaAuditor(int? idPqr, int? tipo)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            //1 auditor
            //2 analista

            var mensaje = "";
            ecop_PQRS obj = new ecop_PQRS();
            List<sis_usuario> personal = new List<sis_usuario>();
            var idAnalista = 0;
            var idAuditor = "";
            var correo = "";
            List<string> correos = new List<string>();
            string textBody = "";

            try
            {
                obj = BusClass.LlamarPqrsById((int)idPqr);

                if (obj != null)
                {
                    if (obj.auditor_asignado != null)
                    {
                        idAuditor = obj.auditor_asignado;
                    }
                    if (obj.usuario_asignado != null)
                    {
                        idAnalista = (int)obj.usuario_asignado;
                    }
                }

                if (tipo == 1)
                {

                    string[] listaAuditores = idAuditor.Split(',');
                    if (listaAuditores.Length > 0)
                    {
                        foreach (var item in listaAuditores)
                        {
                            sis_usuario usu = new sis_usuario();
                            var idUsuario = Convert.ToInt32(item);
                            usu = BusClass.datosUsuarioId(idUsuario);
                            if (usu != null)
                            {
                                var cor = usu.correo_ins != null && usu.correo_ins != "" ? usu.correo_ins : "";
                                if (cor == "")
                                {
                                    EnviarCorreoMarelaFaltaCorreoCorp(usu.nombre, "Caso analista auditor");
                                    mensaje = "No existe correo analista auditor";
                                }
                                else
                                {
                                    correos.Add(cor);
                                    personal.Add(usu);
                                }

                            }
                        }
                    }
                    textBody += "Se le ha asignado el caso # " + idPqr + " como auditor(a), por favor gestionarlo en el tablero de auditor https://ecopetrol.aplicativoasalud.co/Pqrs/TableroControlAuditor";
                }

                else
                {
                    Ref_PQRS_usuarios usuario = new Ref_PQRS_usuarios();
                    usuario = BusClass.GetUsuarioPqrsId(idAnalista);

                    if (usuario != null)
                    {
                        sis_usuario per = new sis_usuario();
                        per = BusClass.datosUsuarioUser(usuario.usuario);
                        if (per != null)
                        {
                            correos.Add(per.correo_ins);
                            //personal.Add(per);
                        }
                    }

                    textBody += "Se le ha asignado el caso # " + idPqr + " como analista, por favor gestionarlo en el tablero de control PQRS https://ecopetrol.aplicativoasalud.co/Pqrs/TableroControl";
                }

                textBody += "<br/>";
                textBody += "<br/>";
                textBody += "Por favor no responder a este correo ya que fue generado automáticamente. Si es necesario generar una respuesta, por favor dirigirla al correo pqrs.ecopetrol@asalud.co.";
                textBody += "<br/>";
                textBody += "<br/>";
                textBody += "Atentamente,";
                textBody += "<br/>";

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

                    mailBody += "<br />";
                    mailBody += "</div>";
                    mailBody += "<div id='RightPane' align='center'  style='font-size: 13px;'>";
                    mailBody += "<br />";
                    mailBody += "<img src='cid:dealer_logo' />";
                    mailBody += "<br />";

                    mailBody += "<br />";
                    mailBody += "<STRONG>ASALUD SAS</STRONG>";
                    mailBody += "<br />";
                    mailBody += "<a href='http://www.asalud.co' target='_blank'>Website. www.asalud.co</a>";
                    mailBody += "<br />";
                    //mailBody += "Dir. Calle 96 # 13a-03 Piso 4";
                    //mailBody += "<br />";
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

                    mailMessage.From = new MailAddress("admin@asaludltda.com");
                    mailMessage.To.Clear();
                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                    {
                        foreach (var co in correos)
                        {
                            var datoCorreo = co;
                            if (!string.IsNullOrEmpty(datoCorreo))
                            {
                                mailMessage.To.Add(datoCorreo);
                            }
                        }
                    }
                    else
                    {
                        mailMessage.To.Add("desarrollo.soporte@asalud.co");
                    }

                    if (mailMessage.To.Count() == 0)
                    {
                        mailMessage.To.Add("desarrollo.soporte@asalud.co");
                    }

                    mailMessage.Subject = "[Mensaje Automático]" + "Asignación de caso Pqr - Número de caso:" + obj.numero_caso;

                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                    mailMessage.IsBodyHtml = true;
                    objMail.Send(mailMessage);

                    //Model.ActualizarEnvioPQRS(Model.id_ecop_PQRS, Convert.ToString(SesionVar.UserName), ref MsgRes);

                    //if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    //{
                    //    mensaje = "NOTIFICACIÓN ENVIADA CORRECTAMENTE.";
                    //}
                    //else
                    //{
                    //    mensaje = "ERROR AL ENVIAR NOTIFICACIÓN.";
                    //}
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    mensaje = "ERROR AL ENVIAR NOTIFICACIÓN SOLUCIONADOR.";
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ENVIAR NOTIFICACIÓN SOLUCIONADOR.";
                throw new Exception(error);
            }
        }

        public ActionResult TableroPorcentajeAuditores(string auditor, int? regional)
        {
            List<management_pqrs_PorcentajeAuditoresResult> listado = new List<management_pqrs_PorcentajeAuditoresResult>();
            List<management_pqrs_PorcentajeAuditoresResult> filtrado = new List<management_pqrs_PorcentajeAuditoresResult>();
            vw_auditores_totales_pqrs DatosAuditor = new vw_auditores_totales_pqrs();

            var conteo = 0;
            var porcentajeCor = 0;
            var porcentajeAuditor = 0;
            var conteoAsignado = 0;
            var conteoNoAsignado = 0;
            var conteoCoordinador = 0;
            var usuario = "";
            var rolUsuario = SesionVar.ROL;

            try
            {
                if (!string.IsNullOrEmpty(auditor) || regional != null || rolUsuario == "1" || rolUsuario == "10")
                {
                    if (!string.IsNullOrEmpty(auditor))
                    {
                        DatosAuditor = BusClass.GetAuditorNombrePqrs(auditor);
                    }

                    if (DatosAuditor != null)
                    {
                        if (!string.IsNullOrEmpty(DatosAuditor.nombre))
                        {
                            usuario = DatosAuditor.nombre;
                        }
                    }

                    listado = BusClass.listadoPQRSAuditorPorcentaje(auditor);
                    if (listado.Count() > 0)
                    {
                        listado = listado.Where(x => x.fechaRespuestaAuditor != null).ToList();

                        if (regional != null)
                        {
                            listado = listado.Where(x => x.id_ref_regional == regional).ToList();
                        }

                        filtrado = listado;

                        conteoAsignado = filtrado.Where(x => x.nombreAuditor.ToUpper().Contains(x.nombreAuditorRespuesta)).Count();
                        conteoNoAsignado = filtrado.Where(x => !x.nombreAuditor.ToUpper().Contains(x.nombreAuditorRespuesta)).Count();
                        //conteoNoAsignado = filtrado.Where(x => x.nombreAuditorRespuesta.ToUpper().Contains(usuario)).Count();

                        if (filtrado.Count() > 0)
                        {
                            porcentajeAuditor = ((conteoAsignado * 100) / filtrado.Count());
                            porcentajeCor = 100 - porcentajeAuditor;
                        }
                    }
                }

                conteo = filtrado.Count();
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["listadoPqrsTiemposAuditor"] = listado;

            ViewBag.listadoP = listado;
            ViewBag.conteoP = conteo;
            ViewBag.conteoAsignado = conteoAsignado;
            ViewBag.conteoNoAsignado = conteoNoAsignado;
            ViewBag.porcentajeAu = porcentajeAuditor;
            ViewBag.porcentajeCo = porcentajeCor;
            ViewBag.rol = SesionVar.ROL;
            ViewBag.regionales = BusClass.GetRefRegion();

            return View();
        }

        public JsonResult BuscarAuditorTotal()
        {
            List<vw_auditores_totales_pqrs> auditoresInicial = new List<vw_auditores_totales_pqrs>();
            List<vw_auditores_totales_pqrs> auditoresTotal = new List<vw_auditores_totales_pqrs>();
            var regionalUsuario = 0;
            List<sis_auditor_regional> regi = new List<sis_auditor_regional>();

            var idUsuario = SesionVar.IDUser;

            try
            {
                if (string.IsNullOrEmpty(Request.Params["term"]))
                {
                    return null;
                }

                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    regi = BusClass.listadoRegionalesUsuario(idUsuario);
                    auditoresInicial = BusClass.GetAuditorTotalesPqrs(ref MsgRes);

                    if (regi.Count() > 0)
                    {
                        foreach (var item in regi)
                        {
                            List<vw_auditores_totales_pqrs> auditores = new List<vw_auditores_totales_pqrs>();

                            if (item.id_regional != null)
                            {
                                auditores = auditoresInicial.Where(x => x.id_regional == item.id_regional).ToList();

                                if (auditores.Count() > 0)
                                {
                                    auditores = auditores.Where(x => x.nombre.ToUpper().Contains(term.ToUpper())).ToList();
                                    if (auditores.Count() > 0)
                                    {
                                        auditoresTotal.AddRange(auditores);
                                    }
                                }
                            }
                        }
                    }

                    var lista = (from reg in auditoresTotal
                                 select new
                                 {
                                     nombre = reg.nombre,
                                     label = reg.nombre + "-" + reg.indice,
                                     valor = reg.id_regional,
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
                var error = e.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public void ExportarLstpqrsTiemposAuditor()
        {
            List<management_pqrs_PorcentajeAuditoresResult> Lista = new List<management_pqrs_PorcentajeAuditoresResult>();
            string proye;

            try
            {
                ExcelPackage Ep = new ExcelPackage();
                Lista = (List<management_pqrs_PorcentajeAuditoresResult>)Session["listadoPqrsTiemposAuditor"];
                proye = "ControlTiempos_auditores";

                if (Lista == null || Lista.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('NO HAY DATOS POR MOSTRAR.');" +
                       "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(proye);

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    Sheet.Cells["A1:I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:I1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:I1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:I1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:I1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:I1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "ID PQR";
                    Sheet.Cells["B1"].Value = "NÚMERO CASO";
                    Sheet.Cells["C1"].Value = "REGIONAL";
                    Sheet.Cells["D1"].Value = "CONSECUTIVO CASO";
                    Sheet.Cells["E1"].Value = "FECHA ENVIO A REVISIÓN";
                    Sheet.Cells["F1"].Value = "USUARIO ENVIA A REVISIÓN";
                    Sheet.Cells["G1"].Value = "FECHA RESPUESTA AUDITOR";
                    Sheet.Cells["H1"].Value = "AUDITOR QUE RESPONDE";
                    Sheet.Cells["I1"].Value = "TIEMPO DE DIFERENCIA";

                    int row = 2;

                    foreach (management_pqrs_PorcentajeAuditoresResult item in Lista)
                    {
                        Sheet.Cells["A" + row + ":N" + row].Style.Font.Size = 10;
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_ecop_PQRS;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.numero_caso;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.nombre_regional;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.consecutivo_caso;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_envio_revision;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.nombreAuditor;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.fechaRespuestaAuditor;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nombreAuditorRespuesta;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.diferenciaDiasHoras;

                        Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile;
                    namefile = "ControlTiemposAuditores_" + DateTime.Now;

                    Sheet.Cells["A:I"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
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

        public void EnviarCorreoMarelaFaltaCorreoCorp(string usuarioFaltante, string proceso)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var mensaje = "";
            sis_usuario usuario = new sis_usuario();

            var correoCoorPQRS = "";

            try
            {
                usuario = BusClass.datosUsuarioRol(10);

                if (usuario != null)
                {
                    correoCoorPQRS = usuario.correo_ins;
                }

                string textBody = "Cordial saludo,";
                textBody += "<br/>";
                textBody += "El usuario: " + usuarioFaltante + " No posee correo corporativo, por favor solicitar su actualización con soporte";
                textBody += "<br/>";
                textBody += "<br/>";
                textBody += "Atentamente,";
                textBody += "<br/>";
                textBody += "Asalud";
                textBody += "<br/>";

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
                mailBody += "<STRONG>Marcela Clavijo Gutierrez </STRONG>";
                mailBody += "<br />";
                mailBody += "<STRONG>Lider PQRS </STRONG>";
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
                LinkedResource resource = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/SelloFirma.png");
                resource.ContentId = "dealer_logo";
                htmlView.LinkedResources.Add(resource);

                mailMessage.AlternateViews.Add(htmlView);

                mailMessage.From = new MailAddress("admin@asaludltda.com");

                //mailMessage.To.Add("notificacionespqrs@asaludltda.com");
                mailMessage.To.Clear();


                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    if (!string.IsNullOrEmpty(correoCoorPQRS))
                    {
                        mailMessage.To.Add(correoCoorPQRS);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(correoCoorPQRS))
                    {
                        mailMessage.To.Add("desarrollo.soporte@asalud.co");
                    }
                }

                mailMessage.Subject = "[Mensaje Automático]" + "Notificación Falta Correo Corp";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<HTML><head><META http-equiv=Content-Type content=\"text/html; \"> " + mailCSS + "</head><body> " + textBody + "<br>" + mailBody + "</body></HTML>";

                mailMessage.IsBodyHtml = true;
                objMail.Send(mailMessage);

                //Model.ActualizarEnvioPQRS(Model.id_ecop_PQRS, Convert.ToString(SesionVar.UserName), ref MsgRes);


                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "NOTIFICACIÓN ENVIADA CORRECTAMENTE.";
                }
                else
                {
                    mensaje = "ERROR AL ENVIAR NOTIFICACIÓN.";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ENVIAR NOTIFICACIÓN SOLUCIONADOR.";
            }
        }

        public string pdfConceptoAuditor(ecop_PQRS obj)
        {
            var ruta = "";
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            //RUTA REPORTE

            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptPqrsConceptoAuditor.rdlc");

                //CARGAR LISTA
                List<management_pqrs_datosReporte_conceptoResult> lista = new List<management_pqrs_datosReporte_conceptoResult>();
                lista = BusClass.ReporteConceptoAuditor(obj.id_ecop_PQRS);

                //ASIGNACION  DATASET A REPORT
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPqrsConceptoAuditor", lista);

                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();

                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);

                viewer.LocalReport.EnableHyperlinks = true;

                viewer.LocalReport.Refresh();

                string mimeType;
                string encoding;
                string fileNameExtension;
                string[] streams;
                Microsoft.Reporting.WebForms.Warning[] warnings;
                byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                //RETORNO PDF

                if (pdfContent.Length > 0)
                {
                    GuardarPdf(pdfContent, obj);
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

            return ruta;
        }

        public void GuardarPdf(byte[] bytes, ecop_PQRS obj)
        {
            try
            {
                byte[] array = new byte[0];
                array = bytes;
                array = array.ToArray();

                HttpPostedFileBase sigFile = (HttpPostedFileBase)new HttpPostedFileBaseCustom(array, "application/pdf", "ReporteAuditorConcepto_" + obj.id_ecop_PQRS + ".pdf");

                var resultado = 0;
                if (sigFile != null)
                {
                    resultado = GuardarPDFConcepto(sigFile, obj);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public class HttpPostedFileBaseCustom : HttpPostedFileBase
        {
            private byte[] _Bytes;
            private String _ContentType;
            private String _FileName;
            private MemoryStream _Stream;

            public override Int32 ContentLength { get { return this._Bytes.Length; } }
            public override String ContentType { get { return this._ContentType; } }
            public override String FileName { get { return this._FileName; } }

            public override Stream InputStream
            {
                get
                {
                    if (this._Stream == null)
                    {
                        this._Stream = new MemoryStream(this._Bytes);
                    }
                    return this._Stream;
                }
            }

            public HttpPostedFileBaseCustom(byte[] contentData, String contentType, String fileName)
            {
                this._ContentType = contentType;
                this._FileName = fileName;
                this._Bytes = contentData ?? new byte[0];
            }

            public override void SaveAs(String filename)
            {
                System.IO.File.WriteAllBytes(filename, this._Bytes);
            }
        }

        public int GuardarPDFConcepto(HttpPostedFileBase file, ecop_PQRS pqr)
        {
            string archivo = "";

            ViewBag.af = false;
            try
            {

                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;

                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string nombreSintilde = Regex.Replace(file.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");

                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + nombreSintilde);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                string strError = string.Empty;

                DateTime fecha = DateTime.Now;
                archivo = string.Empty;

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "ReporteConceptoAuditor" + "\\" + "nro_" + pqr.id_ecop_PQRS);

                var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                try
                {
                    file.SaveAs(archivo);

                    string dirpath2 = Path.Combine(Request.PhysicalApplicationPath, archivo);
                    WebClient User = new WebClient();
                    archivo = dirpath2;
                    string filename = archivo;
                    Byte[] FileBuffer = User.DownloadData(filename);
                    var extension = file.ContentType.ToString();

                    GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                    OBJ.id_ecop_pqr = pqr.id_ecop_PQRS;
                    OBJ.id_tipo_documental = 14;
                    OBJ.numero_caso = pqr.numero_caso;
                    OBJ.ruta = Convert.ToString(archivo);
                    OBJ.cargue_fecha = DateTime.Now;
                    OBJ.cargue_usuario = SesionVar.UserName;
                    OBJ.tipo = file.ContentType;

                    var idCargue = BusClass.InsertarPQRSProyeccion(OBJ, ref MsgRes);
                    return idCargue;

                }
                catch (Exception ex)
                {
                    var mensaje = ex.Message;
                    return 0;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        private string GuardarArchivoConceptoAuditoria(ecop_PQRS pqr, HttpPostedFileBase file)
        {

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            var rutaRetorno = "";

            try
            {
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosPQRS2"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "PQRSIngresoRadicacion";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "PQRSIngresoRadicacionPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + pqr.id_ecop_PQRS);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);




                file.SaveAs(archivo);

                GestionDocumentalPQRS2 OBJ = new GestionDocumentalPQRS2();

                OBJ.id_ecop_pqr = pqr.id_ecop_PQRS;
                OBJ.id_tipo_documental = 14;
                OBJ.numero_caso = pqr.numero_caso;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.cargue_fecha = DateTime.Now;
                OBJ.cargue_usuario = SesionVar.UserName;
                OBJ.tipo = file.ContentType;

                var inserta = BusClass.InsertarPQRSProyeccion(OBJ, ref MsgRes);
                if (inserta != 0)
                {
                    rutaRetorno = archivo;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return rutaRetorno;
        }

        public class DeByteAFile : HttpPostedFileBase
        {
            private readonly byte[] fileBytes;
            MemoryStream stream;

            public DeByteAFile(byte[] fileBytes, string filename = null)
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
