using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.Consulta;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Kendo.Mvc.UI;
using LinqToExcel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.FFMM
{
    [SessionExpireFilter]
    public class ffmmAuditoriaController : Controller
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


        #endregion
        public ActionResult Index()
        {
            Models.FFMM.auditoria_ffmm Model = new Models.FFMM.auditoria_ffmm();

            ViewBag.regionales = Model.FFMM_departamento();
            ViewBag.tipoDoc = Model.GetTipoDocumnetal();
            ViewBag.tipoAfi = Model.FFMM_tipo_afiliacion();
            ViewBag.origen_servicio = Model.FFMM_origen_servicio();
            ViewBag.UnidadSatelite = Model.GetUnidadSatelite(ref MsgRes).Where(p => p.concurrencia.Equals("SI")).OrderBy(p => p.descripciion);
            ViewBag.SitioAdscripcion = Model.GetSitioAdscripcion(ref MsgRes);
            return View(Model);
        }

        public string ObtenerIpsPrestadorSinTipo(int? idCiudad)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<ref_ffmm_ips_prestadores> ipsPrestador = new List<ref_ffmm_ips_prestadores>();

            if (idCiudad != null)
            {
                ipsPrestador = BusClass.ObtenerIpsPrestadorSinTipo((int)idCiudad);

                if (ipsPrestador.Count() > 0)
                {
                    foreach (var item in ipsPrestador)
                    {
                        result += "<option value='" + item.ip_ips_proveedor + "'>" + item.nombre + "</option>";
                    }
                }
            }

            return result;
        }

        public JsonResult SavegestionAuditoria(Models.FFMM.auditoria_ffmm Model)
        {
            String mensaje = "";
            try
            {
                ffmm_auditoria obj = new ffmm_auditoria();

                obj.fecha_auditoria = Model.fecha_auditoria;
                obj.unidad = Model.unidad;
                obj.id_ref_forma_auditoria = Model.id_ref_forma_auditoria;
                obj.id_ref_tipo_visita = Model.id_ref_tipo_visita;
                obj.id_departamento = Model.id_departamento;
                obj.id_municipio = Model.id_municipio;
                obj.id_ref_ips = Model.id_ref_ips;
                obj.fecha_solicitud_autorizacion = Model.fecha_solicitud_autorizacion;
                obj.fecha_generacion_autorizacion = Model.fecha_generacion_autorizacion;
                obj.fecha_ingreso_ips = Model.fecha_ingreso_ips;
                obj.fecha_egreso_ips = Model.fecha_egreso_ips;
                obj.dias_instancia_ips = Model.dias_instancia_ips;
                obj.nombre_apellidos_usuario = Model.nombre_apellidos_usuario;
                obj.tipoId = Model.tipoId;
                obj.numeroId = Model.numeroId;
                obj.fecha_nacimiento = Model.fecha_nacimiento;
                obj.edadN = Model.edadN;
                obj.id_ref_grupo_etario = Model.id_ref_grupo_etario;
                obj.sexo = Model.sexo;
                obj.estado_afiliacion = Model.estado_afiliacion;
                obj.sitio_adscripcion = Model.sitio_adscripcion;
                obj.id_ref_tipo_afiliacion = Model.id_ref_tipo_afiliacion;
                obj.direccion = Model.direccion;
                obj.telefono = Model.telefono;
                obj.grado = Model.grado;
                obj.fuerza = Model.fuerza;
                obj.nivel_atencion = Model.nivel_atencion;
                obj.id_ref_nivel_complejidad = Model.id_ref_nivel_complejidad;
                obj.id_ref_via_ingreso = Model.id_ref_via_ingreso;
                obj.otro_via_ingreso = Model.otro_via_ingreso;
                obj.id_cie10_1 = Model.cie10_1;
                obj.id_cie10_2 = Model.cie10_2;
                obj.is_cups = Model.is_cups;
                obj.origen_servicio = Model.origen_servicio;

                obj.id_ref_servicio = Model.id_ref_servicio;
                obj.otro_servicio = Model.otro_servicio;
                obj.fecha_ingreso_hospitalizacion = Model.fecha_ingreso_hospitalizacion;
                obj.fecha_egreso_hospitalizacion = Model.fecha_egreso_hospitalizacion;
                obj.dias_estancia_hospitalizacion = Model.dias_estancia_hospitalizacion;
                obj.fecha_ingreso_uci = Model.fecha_ingreso_uci;
                obj.fecha_egreso_uci = Model.fecha_egreso_uci;
                obj.dias_estancia_uci = Model.dias_estancia_uci;
                obj.fecha_ingreso_intermedios = Model.fecha_ingreso_intermedios;
                obj.fecha_egraso_intermedios = Model.fecha_egraso_intermedios;
                obj.dias_estancia_intermedios = Model.dias_estancia_intermedios;
                obj.estancia_prolongada = Model.estancia_prolongada;
                obj.id_ref_estancia_prolongada = Model.id_ref_estancia_prolongada;
                obj.otro_estancia_prolongada = Model.otro_estancia_prolongada;
                obj.evento_materno = Model.evento_materno;

                obj.id_ref_evento_materno = Model.id_ref_evento_materno;
                obj.otro_evnto_materno = Model.otro_evnto_materno;
                obj.edad_gestacional = Model.edad_gestacional;
                obj.num_controles_prenatales = Model.num_controles_prenatales;
                obj.complicacion_parto_puerperio = Model.complicacion_parto_puerperio;
                obj.id_ref_complicacion_parto = Model.id_ref_complicacion_parto;
                obj.otro_complicaciones_parto = Model.otro_complicaciones_parto;
                obj.recien_nacido = Model.recien_nacido;
                obj.id_ref_condiciones_recien_nacido = Model.id_ref_condiciones_recien_nacido;
                obj.otro_condicion_recien_nacido = Model.otro_condicion_recien_nacido;
                obj.patologia_alto_costo = Model.patologia_alto_costo;
                obj.cie10_patologia_alto_costo = Model.cie10_3;
                obj.alto_riesgo = Model.alto_riesgo;
                obj.id_ref_alto_riesgo = Model.id_ref_alto_riesgo;
                obj.otro_alto_riesgo = Model.otro_alto_riesgo;
                obj.condicion_trazadora = Model.condicion_trazadora;
                obj.id_ref_tipo_condicion_trazadora = Model.id_ref_tipo_condicion_trazadora;
                obj.otro_tipo_condicion_trazadora = Model.otro_tipo_condicion_trazadora;
                obj.evento_adverso = Model.evento_adverso;
                obj.tipo_eveno_adverso = Model.tipo_eveno_adverso;
                obj.id_ref_causal_evento_adverso_ips = Model.id_ref_causal_evento_adverso_ips;
                obj.otro_causal_evento_adverso_ips = Model.otro_causal_evento_adverso_ips;
                obj.id_ref_causal_evento_adverso_eps = Model.id_ref_causal_evento_adverso_eps;
                obj.otro_causal_evento_adverso_eps = Model.otro_causal_evento_adverso_eps;
                obj.fecha_presentacion_evento_adverso = Model.fecha_presentacion_evento_adverso;
                obj.fecha_seguimiento_evento_adverso = Model.fecha_seguimiento_evento_adverso;
                obj.evento_centinela = Model.evento_centinela;
                obj.id_ref_causal_evento_centinela = Model.id_ref_causal_evento_centinela;
                obj.otro_causal_evento_centinela = Model.otro_causal_evento_centinela;
                obj.evento_salud_publica = Model.evento_salud_publica;
                obj.id_ref_causal_evento_salud_publica = Model.id_ref_causal_evento_salud_publica;
                obj.causal_evento_salud_publica = Model.causal_evento_salud_publica;
                obj.complicacion_patologica = Model.complicacion_patologica;
                obj.id_ref_causal_complicacion_patologica = Model.id_ref_causal_complicacion_patologica;
                obj.otro_causal_complicacion_patologica = Model.otro_causal_complicacion_patologica;
                obj.uso_antibiotico = Model.uso_antibiotico;
                obj.cuales_antibioticos = Model.cuales_antibioticos;
                obj.id_ref_requiere_inclusion = Model.id_ref_requiere_inclusion;
                obj.otro_requiere_inclusion = Model.otro_requiere_inclusion;
                obj.efectividad_inclusion = Model.efectividad_inclusion;
                obj.cie10_1_egreso = Model.cie10_4;
                obj.cie10_2_egreso = Model.cie10_5;
                obj.id_ref_destino_egreso = Model.id_ref_destino_egreso;
                obj.otro_destino_egreso = Model.otro_destino_egreso;
                obj.observaciones = Model.observaciones;
                obj.auditor_medico_concurrente = SesionVar.UserName;
                obj.fecha_ingreso = DateTime.Now;
                obj.usuario_ingreso = SesionVar.UserName;

                Model.id_ffmm_auditoria = Model.InsertarFFMMAuditoria(obj, ref MsgRes);

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

        public ActionResult CreacionPrestadores(int? alerta)
        {
            Models.FFMM.ips_prestadores Model = new Models.FFMM.ips_prestadores();

            Models.General General = new Models.General();

            if (alerta == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transacción exitosa!", "Ingresó el prestador exitosamente!");
            }

            else if (alerta == 2)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Error!", "No se ingresó el prestador exitosamente!");
            }
            else if (alerta == 3)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transacción exitosa!", "Se actualizó el prestador exitosamente!");
            }

            else if (alerta == 4)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Error!", "No se actualizó el prestador exitosamente!");
            }

            else
            {
                ViewData["alerta"] = "";
            }

            ViewBag.tipo = Model.tipoIpsPrestador();
            ViewBag.regionales = Model.FFMM_departamento();

            //ViewBag.tipoDoc = Model.GetTipoDocumnetal();
            //ViewBag.tipoAfi = Model.FFMM_tipo_afiliacion();
            //ViewBag.origen_servicio = Model.FFMM_origen_servicio();
            //ViewBag.UnidadSatelite = Model.GetUnidadSatelite(ref MsgRes).Where(p => p.concurrencia.Equals("SI")).OrderBy(p => p.descripciion);
            //ViewBag.SitioAdscripcion = Model.GetSitioAdscripcion(ref MsgRes);

            return View(Model);
        }
        public string ObtenerCiudades(int idDepartamento)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.FFMM.ips_prestadores Model = new Models.FFMM.ips_prestadores();

            List<vw_ffmm_municipio> ciudades = new List<vw_ffmm_municipio>();
            ciudades = Model.GetMunicipios(idDepartamento, ref MsgRes);

            if (ciudades.Count() != 0)
            {
                foreach (var item in ciudades)
                {
                    result += "<option value='" + item.cod_municipio + "'>" + item.muninombre + "</option>";
                }
            }

            return result;
        }


        public JsonResult ObtenerCiudades2(Int32 idDepartamento)
        {
            Models.FFMM.ips_prestadores Model = new Models.FFMM.ips_prestadores();

            List<vw_ffmm_municipio> ciudades = new List<vw_ffmm_municipio>();
            ciudades = Model.GetMunicipios(idDepartamento, ref MsgRes);

            return Json(ciudades.Select(p => new { idMun = p.cod_municipio, nombre = p.muninombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarPrestador(int value, int tipo)
        {
            try
            {
                string term = Convert.ToString(value);

                if (term.Length >= 3)
                {
                    List<management_traerIpsoPrestadoresResult> prestadores = new List<management_traerIpsoPrestadoresResult>();
                    prestadores = BusClass.traerPrestadoresId(tipo, value).ToList();

                    var lista = (from reg in prestadores
                                 select new
                                 {
                                     nit = reg.nit,
                                     nombre = reg.nombre,
                                     departamento = reg.cod_departamento,
                                     ciudad = reg.cod_municipio,
                                     var = 0,
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

        public JsonResult RegistroPrestador(Models.FFMM.ips_prestadores model)
        {
            try
            {
                ref_ffmm_ips_prestadores obj = new ref_ffmm_ips_prestadores();
                obj.tipo = model.tipo;
                obj.nit = model.nit;
                obj.nombre = model.nombre;
                obj.codigohabilitacion = model.codigohabilitacion;
                obj.cod_departamento = model.cod_departamento;
                obj.cod_municipio = model.cod_municipio;
                obj.digito_verificacion = model.digito_verificacion;
                obj.direccion = model.direccion;
                obj.telefono = model.telefono;
                obj.red_interna = model.red_interna;
                obj.medicamentos = "NO";
                obj.regional = "NO";
                obj.masivo = "NO";

                var naju = model.naju;
                if (naju == 1)
                {
                    obj.najunombre = "Privada";
                }
                else
                {
                    obj.najunombre = "Publica";
                }


                int nit = (int)obj.nit;

                List<ref_ffmm_ips_prestadores> existencia = model.existenciaIpsPrestadores(nit);

                if (existencia.Count() != 0)
                {
                    var actualizado = model.ActualizarIpsPrestadores(obj);
                    if (actualizado != 0)
                    {
                        return Json(new { success = true, variable = 3 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, variable = 4 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var resultado = model.InsertarIpsPrestador(obj);
                    if (resultado != 0)
                    {
                        return Json(new { success = true, variable = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, variable = 2 }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { success = false, variable = 2 }, JsonRequestBehavior.AllowGet);
            }
        }

        #region contratos

        public ActionResult TableroContratos(int? nit, String prestador)
        {
            return View();
        }

        public string prestadoresSinContrato(int? nit, String prestador, int tipo)
        {
            string result = "";

            List<management_contratos_prestadoresResult> list = new List<management_contratos_prestadoresResult>();

            if ((nit != 0 && nit != null) || (!string.IsNullOrEmpty(prestador)))
            {
                list = BusClass.ObtenerIpsPrestadorTablero().Where(x => x.ruta_archivo == null || x.ruta_archivo == "").ToList();
            }

            if (nit != 0 && nit != null)
            {
                list = list.Where(x => x.nit == nit).ToList();
            }

            if (!string.IsNullOrEmpty(prestador))
            {
                list = list.Where(x => x.nombre.Contains(prestador)).ToList();
            }


            int i = 0;

            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    i += 1;
                    result += "<tr>";
                    result += "<td>" + i + "</td>";
                    result += "<td>" + item.nit + "</td>";
                    result += "<td>" + item.nombre + "</td>";
                    result += "<td>" + item.departamento + "</td>";
                    result += "<td>" + item.muninombre + "</td>";
                    result += "<td> <a class='btn button_Asalud_Aceptar' href='/ffmmAuditoria/IngresoContratos?nit=" + item.nit + "'})'>Agregar</a></td>";
                    result += "</tr>";
                }
            }
            else
            {
                if (tipo == 1)
                {
                    result += "<tr>";
                    result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                    result += "<label>Use los filtros de prestador para buscar registros.</label>";
                    result += "</td>";
                    result += "</tr>";
                }
                else if (tipo == 2)
                {
                    result += "<tr>";
                    result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                    result += "<label>No hay prestador sin contrato.</label>";
                    result += "</td>";
                    result += "</tr>";
                }
            }
            return result;
        }

        public ActionResult _TableroContratosCompleto()
        {
            return View();
        }

        public string prestadoresconContrato(int? nit, String prestador, int tipo)
        {
            string result = "";

            List<management_contratos_prestadoresResult> list = new List<management_contratos_prestadoresResult>();

            if ((nit != 0 && nit != null) || (!string.IsNullOrEmpty(prestador)))
            {
                list = BusClass.ObtenerIpsPrestadorTablero().Where(x => x.ruta_archivo != null && x.ruta_archivo != "").ToList();
            }

            if (nit != 0 && nit != null)
            {
                list = list.Where(x => x.nit == nit).ToList();
            }

            if (!string.IsNullOrEmpty(prestador))
            {
                list = list.Where(x => x.nombre.Contains(prestador)).ToList();
            }

            int i = 0;

            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    i += 1;
                    result += "<tr>";
                    result += "<td>" + i + "</td>";
                    result += "<td>" + item.nit + "</td>";
                    result += "<td>" + item.nombre + "</td>";
                    result += "<td>" + item.valor_contrato + "</td>";
                    result += "<td>" + item.fecha_inicio.Value.ToString("dd/MM/yyyy") + "</td>";
                    result += "<td>" + item.fecha_fin.Value.ToString("dd/MM/yyyy") + "</td>";
                    result += "<td>" + item.departamento + "</td>";
                    result += "<td>" + item.muninombre + "</td>";
                    result += "<td><a href='javascript:abrirducumento(" + item.id_contrato + ")'>Ver documento</a></td>";
                    result += "</tr>";
                }
            }
            else
            {
                if (tipo == 1)
                {
                    result += "<tr>";
                    result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                    result += "<label>Use los filtros de prestador para buscar registros.</label>";
                    result += "</td>";
                    result += "</tr>";
                }
                else if (tipo == 2)
                {
                    result += "<tr>";
                    result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                    result += "<label>No hay prestador con contrato.</label>";
                    result += "</td>";
                    result += "</tr>";
                }
            }
            return result;
        }

        public ActionResult IngresoContratos(int? alerta, int? nit)
        {
            Models.General General = new Models.General();

            if (alerta == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transacción exitosa!", "Se ingresó el contrato exitosamente!");
            }

            else if (alerta == 2)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Error!", "No se ingresó el contrato exitosamente!");
            }

            else if (alerta == 3)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Error!", "No se ingresó el archivo exitosamente!");
            }


            else
            {
                ViewData["alerta"] = "";
            }

            ViewBag.nit = 0;
            ViewBag.nombre = "";

            var nombre = "";

            if (nit != null && nit != 0)
            {
                List<ref_ffmm_ips_prestadores> prestador = new List<ref_ffmm_ips_prestadores>();
                ref_ffmm_ips_prestadores pre = new ref_ffmm_ips_prestadores();
                prestador = BusClass.existenciaIpsPrestadores((int)nit);

                if (prestador.Count() != 0)
                {
                    pre = prestador.FirstOrDefault();
                    nombre = pre.nombre;
                    ViewBag.nombre = nombre;
                }
                ViewBag.nit = nit;
            }

            ViewBag.ipspre = BusClass.ObtenerIpsPrestadorCompleto();
            return View();
        }
        public JsonResult buscarPrestadorNombre()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >=2)
                {
                    List<ref_ffmm_ips_prestadores> Prestadores = BusClass.ObtenerIpsPrestadorCompleto().Where(x => x.nombre.Contains(term)).ToList();
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.nombre,
                                     Nit = reg.nit,
                                     label = reg.nombre,
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
        public JsonResult buscarPrestadorCod()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];

                int cod = int.Parse(term);

                if (term.Length >= 2)
                {
                    List<ref_ffmm_ips_prestadores> Prestadores = BusClass.ObtenerIpsPrestadorCompleto().Where(x => Convert.ToString(x.nit).Contains(term)).ToList();
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.nombre,
                                     Nit = reg.nit,
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
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GuardarContratos(int? ipsPre, DateTime? fechaIni, DateTime? fechaFin, decimal valorContrato, String documento, List<HttpPostedFileBase> file, String observaciones)
        {
            ffmm_contratos obj = new ffmm_contratos();
            var mensaje = "";
            try
            {
                obj.id_tipoIpsPrestador = ipsPre;
                obj.fecha_inicio = fechaIni;
                obj.fecha_fin = fechaFin;
                obj.valor_contrato = valorContrato;
                obj.valor_contrato = valorContrato;
                obj.observaciones = observaciones;

                if (documento == "1")
                {
                    obj.documento = true;
                }
                else
                {
                    obj.documento = false;
                }

                obj.fecha_ingreso = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                string ruta = "";

                if (file.Count() != 0)
                {
                    HttpPostedFileBase archivo = file[0];
                    ruta = cargarArchivos(archivo);
                    obj.tipo_archivo = archivo.ContentType;
                }

                if (!string.IsNullOrEmpty(ruta) && !ruta.Contains("error"))
                {
                    obj.ruta_archivo = ruta;
                }

                var resultado = BusClass.InsertarContratosFFMM(obj);

                if (resultado != 0)
                {
                    mensaje = "INGRESO DE CONTRATO CORRECTAMENTE.";
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
                var error = ex.Message;
                mensaje = "ERROR EL INGRESO DEL DETALLE.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }


        private string cargarArchivos(HttpPostedFileBase file)
        {
            string strRetorno = "";

            try
            {

                if (file != null)
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = file.FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    string[] formatos = new string[] { "pdf" };
                    if (Array.IndexOf(formatos, ext) < 0)
                    {
                        strRetorno = "Formato de archivo inválido.";
                    }
                    else
                    {
                        strRetorno = GuardarArchivo(file);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                strRetorno = "error";
            }
            return strRetorno;
        }

        private string GuardarArchivo(HttpPostedFileBase file)
        {
            string ruta = "";
            try
            {

                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaContratosPrestadores"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaContratosPrestadoresPruebas"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                string strError = string.Empty;

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "contratos";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "contratosPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "//" + carpeta + "//" + file.FileName);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                return archivo;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return ruta;
            }
        }


        public void VerDocumentoCargado(int idContrato)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();

            List<management_contratos_prestadoresResult> list = new List<management_contratos_prestadoresResult>();
            management_contratos_prestadoresResult dato = new management_contratos_prestadoresResult();
            list = BusClass.ObtenerIpsPrestadorTablero().Where(x => x.id_contrato == idContrato).ToList();
            dato = list.FirstOrDefault();

            try
            {
                if (!string.IsNullOrEmpty(dato.ruta_archivo))
                {
                    byte[] bytes;
                    using (FileStream file = new FileStream(dato.ruta_archivo, FileMode.Open, FileAccess.Read))
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
                        Response.ContentType = dato.tipo_archivo;
                        Response.BinaryWrite(array);
                        Response.AddHeader("content-length", array.Length.ToString());

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
                Response.Write("<script language=javascript>alert('Error');</script>");
            }

        }


        public ActionResult cargueFacturasPago(int? alerta, string msg)
        {
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";

            return View();
        }

        [HttpPost]
        public ActionResult cargueFacturasPago(HttpPostedFileBase file)
        {
            string ruta = "";
            if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
            {
                ruta = ConfigurationManager.AppSettings["rutaContratosPrestadores"];
            }
            else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
            {
                ruta = ConfigurationManager.AppSettings["rutaContratosPrestadoresPruebas"];
            }
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(ruta, fileName);


            try
            {
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(path);

                ffmm_cargue_facturas_pago obj = new ffmm_cargue_facturas_pago();

                List<ffmm_cargue_facturas_pago> list = VerificacionCargue(path, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    var resultado = BusClass.InsertarCargueMasivoPagoFactura(list, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["rta"] = 1;
                        ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                        System.IO.File.Delete(path);
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        ViewData["Msg"] = MsgRes.DescriptionResponse;
                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["exitoso"] = 0;
                    ViewData["Msg"] = MsgRes.DescriptionResponse;
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                ViewData["exitoso"] = 0;
                ViewData["rta"] = 2;
                ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
                System.IO.File.Delete(path);
            }
            return View();

        }
        private List<ffmm_cargue_facturas_pago> VerificacionCargue(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            List<ffmm_cargue_facturas_pago> result = new List<ffmm_cargue_facturas_pago>();
            var book = new ExcelQueryFactory(rutafichero);
            var EData1 = (from c in book.WorksheetRange("A1", "P999999", "Cargue") select c).ToList();
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {
                    ffmm_cargue_facturas_pago obj = new ffmm_cargue_facturas_pago();
                    var item = EData1[i];
                    fila++;
                    if (item[0] != null && item[0] != "")
                    {
                        var texto = "";
                        var entero = 0;
                        DateTime fechaDato = new DateTime();

                        columna = "NUMERO FACTURA";
                        texto = item[0];
                        if (texto.Length <= 50)
                        {
                            obj.numero_factura = Convert.ToString(item[0]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "PREFIJO";
                        texto = item[1];
                        if (texto.Length <= 50)
                        {
                            obj.prefijo = Convert.ToString(item[1]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }



                        columna = "VALOR";
                        try
                        {
                            entero = Convert.ToInt32(item[2]);
                            if (entero != null && entero != 0)
                            {
                                obj.valor = Convert.ToInt32(item[2]);
                            }
                            else
                            {
                                textError = columna + ", no puede quedar vacío";
                                throw new Exception(textError);
                            }
                        }
                        catch
                        {
                            textError = columna + ", formato incorrecto";
                            throw new Exception(textError);
                        }


                        columna = "NIT PRESTADOR";
                        try
                        {
                            entero = Convert.ToInt32(item[3]);
                            if (entero != null && entero != 0)
                            {
                                obj.id_prestador = Convert.ToInt32(item[3]);
                            }
                            else
                            {
                                textError = columna + ", no puede quedar vacío";
                                throw new Exception(textError);
                            }
                        }
                        catch
                        {
                            textError = columna + ", no puede quedar vacío";
                            throw new Exception(textError);
                        }
                        columna = "FECHA FACTURA";
                        try
                        {
                            fechaDato = Convert.ToDateTime(item[4]);
                            if (fechaDato != null)
                            {
                                obj.fecha_factura = Convert.ToDateTime(item[4]);
                            }
                            else
                            {
                                textError = columna + ", no puede quedar vacío";
                                throw new Exception(textError);
                            }
                        }
                        catch
                        {
                            textError = columna + ", formato incorrecto";
                            throw new Exception(textError);
                        }

                        columna = "FECHA PAGO";
                        try
                        {
                            fechaDato = Convert.ToDateTime(item[5]);
                            if (fechaDato != null)
                            {
                                obj.fecha_de_pago = Convert.ToDateTime(item[5]);
                            }
                            else
                            {
                                textError = columna + ", no puede quedar vacío";
                                throw new Exception(textError);
                            }
                        }
                        catch
                        {
                            textError = columna + ", formato incorrecto";
                            throw new Exception(textError);
                        }

                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = SesionVar.UserName;

                        result.Add(obj);
                        obj = new ffmm_cargue_facturas_pago();
                    }
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                if (textError != "" && textError != null)
                {
                    MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + textError;
                }
                else
                {
                    MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                }
                MsgRes.CodeError = ex.Message;
            }

            book.Dispose();
            return result;
        }

        public ActionResult TableroFacturasPago()
        {
            return View();
        }

        public string FacturasSinPago(int? nit, String prestador, int tipo)
        {
            string result = "";

            var prestadorFinal = prestador.ToUpper().Trim();

            List<management_facturas_pagosResult> list = new List<management_facturas_pagosResult>();

            if ((nit != null) || (!string.IsNullOrEmpty(prestador)))
            {
                list = BusClass.ListFacturasPago().Where(x => x.id_tabla_cargue == null || x.id_tabla_cargue == 0).ToList();
            }

            if (nit != 0 && nit != null)
            {
                list = list.Where(x => x.nit == nit).ToList();
            }

            if (!string.IsNullOrEmpty(prestadorFinal))
            {
                list = list.Where(x => x.nombre.ToUpper() == prestadorFinal.ToUpper() || x.nombre == null).ToList();
            }

            int i = 0;

            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    i += 1;
                    result += "<tr>";
                    result += "<td>" + i + "</td>";
                    result += "<td>" + item.nit + "</td>";
                    result += "<td>" + item.nombre + "</td>";
                    result += "<td>" + item.numero_factura + "</td>";
                    result += "<td>" + item.vlr_factura + "</td>";
                    result += "<td>" + item.año_mes_radicado + "</td>";
                    result += "<td>" + item.fecha_presentacion_factura.Value.ToString("dd/MM/yyyy") + "</td>";
                    result += "<td>" + item.tipo_proveedor + "</td>";
                    result += "</tr>";
                }
            }
            else
            {
                if (tipo == 1)
                {
                    result += "<tr>";
                    result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                    result += "<label>Use los filtros de prestador para buscar registros.</label>";
                    result += "</td>";
                    result += "</tr>";
                }
                else if (tipo == 2)
                {
                    result += "<tr>";
                    result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                    result += "<label>No hay facturas sin pagar.</label>";
                    result += "</td>";
                    result += "</tr>";
                }
            }
            return result;
        }

        public ActionResult TableroFacturasConPago()
        {
            return View();
        }

        public string FacturasConPago(int? nit, String prestador, int tipo)
        {
            string result = "";

            List<management_facturas_pagosResult> list = new List<management_facturas_pagosResult>();

            if ((nit != 0 && nit != null) || (!string.IsNullOrEmpty(prestador)))
            {
                list = BusClass.ListFacturasPago().Where(x => x.id_tabla_cargue != null || x.id_tabla_cargue != 0).ToList();
            }

            if (nit != 0 && nit != null)
            {
                list = list.Where(x => x.nit == nit).ToList();
            }

            if (!string.IsNullOrEmpty(prestador))
            {
                list = list.Where(x => x.nombre.Contains(prestador)).ToList();
            }

            int i = 0;

            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    i += 1;
                    result += "<tr>";
                    result += "<td>" + i + "</td>";
                    result += "<td>" + item.id_prestador_cargue + "</td>";
                    result += "<td>" + item.nombre + "</td>";
                    result += "<td>" + item.numero_factura_cargue + "</td>";
                    result += "<td>" + item.prefijo_cargue + "</td>";
                    result += "<td>" + item.valor_cargue + "</td>";
                    result += "<td>" + item.fecha_de_pago_cargue.Value.ToString("dd/MM/yyyy") + "</td>";
                    result += "<td>" + item.tipo_proveedor + "</td>";
                    result += "</tr>";
                }
            }
            else
            {
                if (tipo == 1)
                {
                    result += "<tr>";
                    result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                    result += "<label>Use los filtros de prestador para buscar registros.</label>";
                    result += "</td>";
                    result += "</tr>";
                }
                else if (tipo == 2)
                {
                    result += "<tr>";
                    result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                    result += "<label>No hay facturas pagadas.</label>";
                    result += "</td>";
                    result += "</tr>";
                }
            }
            return result;
        }

        public ActionResult FacturaDigital()
        {
            Models.FFMM.auditoria_ffmm Model = new Models.FFMM.auditoria_ffmm();

            return View(Model);
        }

        public ActionResult Selection_Read([DataSourceRequest] DataSourceRequest request)
        {
            Models.FFMM.auditoria_ffmm Model = new Models.FFMM.auditoria_ffmm();

            List<managmentFFMMfacturasRadicadasLoteResult> Lista = new List<managmentFFMMfacturasRadicadasLoteResult>();

            Lista = Model.GetRecepcionFacturasFFMM(ref MsgRes);
            ViewBag.fecha = Convert.ToDateTime(DateTime.Now);
            var lista = new object();

            lista = (from item in Lista
                     select new
                     {

                         id_cargue_base = item.id_cargue_base,
                         Nombre = item.Nombre,
                         prestador = item.prestador,
                         id_regional = item.id_ref_regional,
                         fecha_digita = item.fecha_digita.ToString("dd/MM/yyyy"),


                     }).Distinct().OrderByDescending(f => f.id_cargue_base);

            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public string tablaFacturas(int idcargue)
        {
            string result = "";

            Models.FFMM.auditoria_ffmm Model = new Models.FFMM.auditoria_ffmm();
            List<managmentFFMMfacturasRadicadasdtllResult> lst = Model.GetRecepcionFacturasDTLLFFMM(idcargue, ref MsgRes);
            lst = lst.OrderBy(x => x.id_cargue_dtll).ToList();

            int i = 0;
            foreach (var item in lst)
            {
                i += 1;
                result += "<tr>";
                result += "<td>" + i + "</td>";
                result += "<td>" + item.id_cargue_dtll + "</td>";
                result += "<td>" + item.nombre_prestador + "</td>";
                result += "<td>" + item.num_factura + "</td>";
                result += "<td>" + item.valor_neto + "</td>";
                result += "<td>" + item.estado_des + "</td>";

                result += "<td><button class='btn button_Asalud_Aceptar btn-sm' type='button' onclick='documentos(" + item.id_cargue_base + "," + item.id_cargue_dtll + ")'>Ver documentos</button></td>";
                if (item.estado_factura == 1)
                {
                    result += "<td><button class='btn button_Asalud_Guardar btn-sm' type='button' onclick='aceptar(" + item.id_cargue_dtll + ")'>Aceptar</button></td>";
                    result += "<td><button class='btn button_Asalud_Rechazar btn-sm' type='button' onclick='Rechazar(" + item.id_cargue_dtll + ")'>Rechazar</button></td>";

                }
                else if (item.estado_factura == 2)
                {
                    result += "<td><button class='btn button_Asalud_Guardar btn-sm' type='button' onclick='gestionar(" + item.id_cargue_dtll + ")'>Gestionar</button></td>";
                }

                result += "</tr>";
            }

            return result;
        }

        //leo
        public ActionResult ConsultasFFMM()
        {
            AsaludEcopetrol.Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            ViewBag.regional = Model.FFMM_unudadR();
            ViewBag.UnidadSatelite = Model.GetUnidadSatelite(ref MsgRes).Where(p => p.concurrencia.Equals("SI")).OrderBy(p => p.descripciion);
            ViewBag.origen_servicio = Model.FFMM_origen_servicio();

            return View(Model);
        }

        public ActionResult DownloadExcelEPPlus(Int32 lista_consultas, DateTime? fechainicial, DateTime? fechafin)
        {
            Consulta Model = new Consulta();
            JsonResult result = new JsonResult();
            String mensaje = "";

            if (lista_consultas == 1)
            {
                List<Management_FFMM_Consultas_cuentasResult> lista1 = new List<Management_FFMM_Consultas_cuentasResult>();

                lista1 = Model.GetConsultaFFMMCuentas(ref MsgRes);

                DateTime fechaIni = Convert.ToDateTime(fechainicial);
                DateTime fechaFin = Convert.ToDateTime(fechafin);

                if (fechainicial != null)
                {
                    lista1 = lista1.Where(x => x.fecha_digita_radicacion >= fechaIni).ToList();
                    lista1 = lista1.Where(x => x.fecha_digita_radicacion <= fechaFin).ToList();
                }

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista1, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Cuentas-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);

            }
            else if (lista_consultas == 2)
            {
                List<Management_FFMM_Consultas_PADResult> lista2 = new List<Management_FFMM_Consultas_PADResult>();

                lista2 = Model.GetConsultaFFMMPad(ref MsgRes);

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista2, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"PAD-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);

            }
            else if (lista_consultas == 3)
            {
                List<Management_FFMM_Consultas_ConcurreniaResult> lista3 = new List<Management_FFMM_Consultas_ConcurreniaResult>();

                lista3 = Model.GetConsultaFFMMConcurrencia(ref MsgRes);


                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista3, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Concurrencia-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);
            }

            return View();
        }

        public ActionResult DownloadExcelEPPlus2(Int32 lista_consultas, DateTime fechainicial, DateTime fechafin, int? id_regional, int? id_unidad, int? id_ips)
        {
            Consulta Model = new Consulta();
            JsonResult result = new JsonResult();
            String mensaje = "";

            if (lista_consultas == 1)
            {
                List<Management_FFMM_consulta_cuentas_primeraResult> lista1 = new List<Management_FFMM_consulta_cuentas_primeraResult>();

                lista1 = Model.GetConsultaFFMMCuentasUno(fechainicial, fechafin, ref MsgRes);

                if (id_regional != null)
                {
                    if (id_regional != 100)
                    {
                        lista1 = lista1.Where(x => x.unidad_regionalizadora == Convert.ToString(id_regional)).ToList();
                    }

                }

                if (id_unidad != null)
                {
                    if (id_unidad != 100)
                    {
                        lista1 = lista1.Where(x => x.unidad_satelite == Convert.ToString(id_unidad)).ToList();
                    }

                }

                var resultado = from a in lista1
                                group a by new { a.AÑO_MES_RADICADO, a.UNIDAD_REGIONALIZADORA_DES, a.UNIDAD_SATELITE_DES } into g
                                select new
                                {
                                    RANGO_INICIAL = fechainicial,
                                    RANFO_FINAL = fechafin,
                                    AÑO_MES_RADICADO = g.Key.AÑO_MES_RADICADO,
                                    UNIDAD_REGIONALIZADORA = g.Key.UNIDAD_REGIONALIZADORA_DES,
                                    UNIDAD_SATELITE = g.Key.UNIDAD_SATELITE_DES,
                                    NUMERO_FACTURAS = g.Count(),
                                    VALOR_FACTURAS = g.Sum(y => y.VL_ATENCION_FACTURAS_RADICADAS)

                                };

                var lista = resultado;

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Cuentas-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);

            }
            else if (lista_consultas == 2)
            {
                List<Management_FFMM_consulta_cuentas_segundaResult> lista2 = new List<Management_FFMM_consulta_cuentas_segundaResult>();

                lista2 = Model.GetConsultaFFMMCuentasDos(fechainicial, fechafin, ref MsgRes);

                if (id_regional != null)
                {
                    if (id_regional != 100)
                    {
                        lista2 = lista2.Where(x => x.unidad_regionalizadora == Convert.ToString(id_regional)).ToList();
                    }

                }

                if (id_unidad != null)
                {
                    if (id_unidad != 100)
                    {
                        lista2 = lista2.Where(x => x.unidad_satelite == Convert.ToString(id_unidad)).ToList();
                    }

                }

                var resultado2 = from a in lista2
                                 group a by new { a.AÑO_MES_RADICADO, a.UNIDAD_REGIONALIZADORA_DES, a.UNIDAD_SATELITE_DES, a.NIT_PRESTADOR, a.NOMBRE_O_RAZON_SOCIAL_DE_LA_IPS_O_PROVEDOR } into g
                                 select new
                                 {
                                     RANGO_INICIAL = fechainicial,
                                     RANFO_FINAL = fechafin,
                                     AÑO_MES_RADICADO = g.Key.AÑO_MES_RADICADO,
                                     NOMBRE_O_RAZON_SOCIAL_DE_LA_IPS_O_PROVEDOR = g.Key.NOMBRE_O_RAZON_SOCIAL_DE_LA_IPS_O_PROVEDOR,
                                     NIT_PRESTADOR = g.Key.NIT_PRESTADOR,
                                     UNIDAD_REGIONALIZADORA = g.Key.UNIDAD_REGIONALIZADORA_DES,
                                     UNIDAD_SATELITE = g.Key.UNIDAD_SATELITE_DES,
                                     NUMERO_FACTURAS = g.Count(),
                                     VALOR_FACTURAS = g.Sum(y => y.VL_ATENCION_FACTURAS_RADICADAS)

                                 };

                var lista = resultado2;

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"PAD-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);

            }
            else if (lista_consultas == 3)
            {
                List<Management_FFMM_consulta_cuentas_terceraResult> lista3 = new List<Management_FFMM_consulta_cuentas_terceraResult>();

                lista3 = Model.GetConsultaFFMMCuentasTres(fechainicial, fechafin, ref MsgRes);

                if (id_regional != null)
                {
                    if (id_regional != 100)
                    {
                        lista3 = lista3.Where(x => x.unidad_regionalizadora == Convert.ToString(id_regional)).ToList();
                    }

                }

                if (id_unidad != null)
                {
                    if (id_unidad != 100)
                    {
                        lista3 = lista3.Where(x => x.unidad_satelite == Convert.ToString(id_unidad)).ToList();
                    }

                }

                var resultado2 = from a in lista3
                                 group a by new { a.AÑO_MES_RADICADO, a.UNIDAD_REGIONALIZADORA_DES, a.UNIDAD_SATELITE_DES, a.NIT_PRESTADOR, a.NOMBRE_O_RAZON_SOCIAL_DE_LA_IPS_O_PROVEDOR } into g
                                 select new
                                 {
                                     RANGO_INICIAL = fechainicial,
                                     RANFO_FINAL = fechafin,
                                     AÑO_MES_RADICADO = g.Key.AÑO_MES_RADICADO,
                                     UNIDAD_REGIONALIZADORA = g.Key.UNIDAD_REGIONALIZADORA_DES,
                                     UNIDAD_SATELITE = g.Key.UNIDAD_SATELITE_DES,
                                     NOMBRE_O_RAZON_SOCIAL_DE_LA_IPS_O_PROVEDOR = g.Key.NOMBRE_O_RAZON_SOCIAL_DE_LA_IPS_O_PROVEDOR,
                                     NIT_PRESTADOR = g.Key.NIT_PRESTADOR,
                                     NUMERO_FACTURAS = g.Count(),
                                     VALOR_FACTURAS = g.Sum(y => y.VL_ATENCION_FACTURAS_RADICADAS)

                                 };

                var lista = resultado2;

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Concurrencia-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);
            }
            else if (lista_consultas == 4)
            {
                List<Management_FFMM_consulta_cuentas_cuartaResult> lista4 = new List<Management_FFMM_consulta_cuentas_cuartaResult>();

                lista4 = Model.GetConsultaFFMMCuentasCuatro(fechainicial, fechafin, ref MsgRes);

                if (id_regional != null)
                {
                    if (id_regional != 100)
                    {
                        lista4 = lista4.Where(x => x.unidad_regionalizadora == Convert.ToString(id_regional)).ToList();
                    }

                }

                if (id_unidad != null)
                {
                    if (id_unidad != 100)
                    {
                        lista4 = lista4.Where(x => x.unidad_satelite == Convert.ToString(id_unidad)).ToList();
                    }

                }

                var resultado2 = from a in lista4
                                 group a by new { a.AÑO_MES_RADICADO, a.UNIDAD_REGIONALIZADORA_DES, a.UNIDAD_SATELITE_DES, a.NOMBRE_Y_APELLIDO_USUARIO, a.No_DOCUMENTO_DEL_USUARIO, a.EDAD_N, a.EDAD_T } into g
                                 select new
                                 {
                                     RANGO_INICIAL = fechainicial,
                                     RANFO_FINAL = fechafin,
                                     AÑO_MES_RADICADO = g.Key.AÑO_MES_RADICADO,
                                     UNIDAD_REGIONALIZADORA = g.Key.UNIDAD_REGIONALIZADORA_DES,
                                     UNIDAD_SATELITE = g.Key.UNIDAD_SATELITE_DES,
                                     NOMBRE_Y_APELLIDO_USUARIO = g.Key.NOMBRE_Y_APELLIDO_USUARIO,
                                     No_DOCUMENTO_DEL_USUARIO = g.Key.No_DOCUMENTO_DEL_USUARIO,
                                     NUMERO_FACTURAS = g.Count(),
                                     VL_ATENCION = g.Sum(y => y.VL_ATENCION)

                                 };

                var lista = resultado2;

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Concurrencia-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);
            }
            else if (lista_consultas == 5)
            {
                List<Management_FFMM_consulta_cuentas_quintaResult> lista5 = new List<Management_FFMM_consulta_cuentas_quintaResult>();

                lista5 = Model.GetConsultaFFMMCuentasCinco(fechainicial, fechafin, ref MsgRes);

                if (id_regional != null)
                {
                    if (id_regional != 100)
                    {
                        lista5 = lista5.Where(x => x.unidad_regionalizadora == Convert.ToString(id_regional)).ToList();
                    }

                }

                if (id_unidad != null)
                {
                    if (id_unidad != 100)
                    {
                        lista5 = lista5.Where(x => x.unidad_satelite == Convert.ToString(id_unidad)).ToList();
                    }

                }

                var resultado2 = from a in lista5
                                 group a by new { a.AÑO_MES_RADICADO, a.UNIDAD_REGIONALIZADORA_DES, a.UNIDAD_SATELITE_DES, a.CIE_10, a.DESCRIPCION__CIE_10 } into g
                                 select new
                                 {
                                     RANGO_INICIAL = fechainicial,
                                     RANFO_FINAL = fechafin,
                                     AÑO_MES_RADICADO = g.Key.AÑO_MES_RADICADO,
                                     UNIDAD_REGIONALIZADORA = g.Key.UNIDAD_REGIONALIZADORA_DES,
                                     UNIDAD_SATELITE = g.Key.UNIDAD_SATELITE_DES,
                                     CIE_10 = g.Key.CIE_10,
                                     DESCRIPCION__CIE_10 = g.Key.DESCRIPCION__CIE_10,
                                     NUMERO_FACTURAS = g.Count(),
                                     VL_ATENCION = g.Sum(y => y.VL_ATENCION)

                                 };

                var lista = resultado2;


                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Concurrencia-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);
            }

            return View();
        }


        public ActionResult DownloadExcelEPPlus3(Int32 lista_consultas, DateTime fechainicial, DateTime fechafin, int? id_auditor, int? id_unidad, Int32? servicio,String genero,Int32? grupoE)
        {
            Consulta Model = new Consulta();
            JsonResult result = new JsonResult();
            String mensaje = "";

            if (lista_consultas == 1)
            {
                List<Management_FFMM_consulta_concurrencia_primeraResult> lista1 = new List<Management_FFMM_consulta_concurrencia_primeraResult>();

                lista1 = Model.GetConsultaFFMMConcurrenciaUno(fechainicial, fechafin, ref MsgRes);

                if (id_auditor != null)
                {
                    if (id_auditor != 100)
                    {
                        //lista1 = lista1.Where(x => x.unidad_regionalizadora == Convert.ToString(id_auditor)).ToList();
                    }

                }

                if (servicio != null)
                {
                    if (servicio != 100)
                    {
                        lista1 = lista1.Where(x => x.SERVICIO == Convert.ToInt32(servicio)).ToList();
                    }

                }

                if (id_unidad != null)
                {
                    if (id_unidad != 100)
                    {
                        lista1 = lista1.Where(x => x.UNIDAD == Convert.ToString(id_unidad)).ToList();
                    }

                }

                var resultado = from a in lista1
                                group a by new { a.AUDITOR_MEDICO_CONCURRENTE, a.DESCRIPCION_UNIDAD, a.DESCRIPCIÓN_SERVICIO } into g
                                select new
                                {
                                    RANGO_INICIAL = fechainicial,
                                    RANFO_FINAL = fechafin,
                                    AUDITOR_MEDICO_CONCURRENTE = g.Key.AUDITOR_MEDICO_CONCURRENTE,
                                    DESCRIPCION_UNIDAD = g.Key.DESCRIPCION_UNIDAD,
                                    DESCRIPCIÓN_SERVICIO = g.Key.DESCRIPCIÓN_SERVICIO,
                                    NUMERO_VISITAS = g.Count(),

                                };


                var lista = resultado;



                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Cuentas-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);



            }
            else if (lista_consultas == 2)
            {
                List<Management_FFMM_consulta_concurrencia_segundaResult> lista2 = new List<Management_FFMM_consulta_concurrencia_segundaResult>();

                lista2 = Model.GetConsultaFFMMConcurrenciaDos(fechainicial, fechafin, ref MsgRes);

                if (id_auditor != null)
                {
                    if (id_auditor != 100)
                    {
                        //lista2 = lista2.Where(x => x.unidad_regionalizadora == Convert.ToString(id_auditor)).ToList();
                    }

                }

                if (servicio != null)
                {
                    if (servicio != 100)
                    {
                        lista2 = lista2.Where(x => x.SERVICIO == Convert.ToInt32(servicio)).ToList();
                    }

                }

                if (id_unidad != null)
                {
                    if (id_unidad != 100)
                    {
                        lista2 = lista2.Where(x => x.UNIDAD == Convert.ToString(id_unidad)).ToList();
                    }

                }

                var resultado2 = from a in lista2
                                 group a by new { a.DESCRIPCION_UNIDAD, a.DESCRIPCIÓN_SERVICIO, a.CODIGO_CIE_10_PRINCIPAL_DE_INGRESO, a.DESCRIPCIÓN_CODIGO_CIE_10_PRINCIPAL_DE_INGRESO } into g
                                 select new
                                 {
                                     RANGO_INICIAL = fechainicial,
                                     RANFO_FINAL = fechafin,
                                     DESCRIPCION_UNIDAD = g.Key.DESCRIPCION_UNIDAD,
                                     DESCRIPCIÓN_SERVICIO = g.Key.DESCRIPCIÓN_SERVICIO,
                                     CODIGO_CIE_10_PRINCIPAL_DE_INGRESO = g.Key.CODIGO_CIE_10_PRINCIPAL_DE_INGRESO,
                                     DESCRIPCIÓN_CODIGO_CIE_10_PRINCIPAL_DE_INGRESO = g.Key.DESCRIPCIÓN_CODIGO_CIE_10_PRINCIPAL_DE_INGRESO,

                                 };

                var lista = resultado2;

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"PAD-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);

            }
            else if (lista_consultas == 3)
            {
                List<Management_FFMM_consulta_concurrencia_terceroResult> lista3 = new List<Management_FFMM_consulta_concurrencia_terceroResult>();

                lista3 = Model.GetConsultaFFMMConcurrenciaTercero(fechainicial, fechafin, ref MsgRes);


                if (genero != null)
                {
                    if (genero != "100")
                    {
                        lista3 = lista3.Where(x => x.GENERO == Convert.ToString(genero)).ToList();
                    }

                }

                if (id_unidad != null)
                {
                    if (id_unidad != 100)
                    {
                        lista3 = lista3.Where(x => x.UNIDAD == Convert.ToString(id_unidad)).ToList();
                    }

                }

                var resultado2 = from a in lista3
                                 group a by new { a.DESCRIPCION_UNIDAD, a.GENERO, a.CODIGO_CIE_10_PRINCIPAL_DE_INGRESO, a.DESCRIPCIÓN_CODIGO_CIE_10_PRINCIPAL_DE_INGRESO } into g
                                 select new
                                 {
                                     RANGO_INICIAL = fechainicial,
                                     RANFO_FINAL = fechafin,
                                     DESCRIPCION_UNIDAD = g.Key.DESCRIPCION_UNIDAD,
                                     GENERO = g.Key.GENERO,
                                     CODIGO_CIE_10_PRINCIPAL_DE_INGRESO = g.Key.CODIGO_CIE_10_PRINCIPAL_DE_INGRESO,
                                     DESCRIPCIÓN_CODIGO_CIE_10_PRINCIPAL_DE_INGRESO = g.Key.DESCRIPCIÓN_CODIGO_CIE_10_PRINCIPAL_DE_INGRESO,
                                 };

                var lista = resultado2;

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Concurrencia-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);
            }
            else if (lista_consultas == 4)
            {
                List<Management_FFMM_consulta_concurrencia_cuartoResult> lista4 = new List<Management_FFMM_consulta_concurrencia_cuartoResult>();

                lista4 = Model.GetConsultaFFMMConcurrenciaCuarto(fechainicial, fechafin, ref MsgRes);

               
                if (grupoE != null)
                {
                    if (grupoE != 100)
                    {
                        lista4 = lista4.Where(x => x.GRUPO_ETARIO == Convert.ToInt32(grupoE)).ToList();
                    }

                }

                if (id_unidad != null)
                {
                    if (id_unidad != 100)
                    {
                        lista4 = lista4.Where(x => x.UNIDAD == Convert.ToString(id_unidad)).ToList();
                    }

                }


                var resultado2 = from a in lista4
                                 group a by new { a.DESCRIPCION_UNIDAD, a.DESCRIPCIÓN_GRUPO_ETARIO, a.CODIGO_CIE_10_PRINCIPAL_DE_INGRESO, a.DESCRIPCIÓN_CODIGO_CIE_10_PRINCIPAL_DE_INGRESO } into g
                                 select new
                                 {
                                     RANGO_INICIAL = fechainicial,
                                     RANFO_FINAL = fechafin,
                                     DESCRIPCION_UNIDAD = g.Key.DESCRIPCION_UNIDAD,
                                     DESCRIPCIÓN_GRUPO_ETARIO = g.Key.DESCRIPCIÓN_GRUPO_ETARIO,
                                     CODIGO_CIE_10_PRINCIPAL_DE_INGRESO = g.Key.CODIGO_CIE_10_PRINCIPAL_DE_INGRESO,
                                     DESCRIPCIÓN_CODIGO_CIE_10_PRINCIPAL_DE_INGRESO = g.Key.DESCRIPCIÓN_CODIGO_CIE_10_PRINCIPAL_DE_INGRESO,

                                 };

                var lista = resultado2;

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Concurrencia-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);
            }

            return View();
        }

        public string tablasoportesTotales(int iddetalle)
        {
            string result = "";

            Models.FFMM.auditoria_ffmm Model = new Models.FFMM.auditoria_ffmm();
            List<managment_ffmm_documentosResult> lst = Model.GetSoportesListFFMM(iddetalle);
            lst = lst.OrderBy(x => x.num_tipo).ToList();

            int i = 0;
            foreach (var item in lst)
            {
                i += 1;
                result += "<tr>";
                result += "<td>" + i + "</td>";
                result += "<td>" + item.tipo + "</td>";
                result += "<td>" + item.nombre + "</td>";
                if (item.tipo != "ZIP")
                {
                    result += "<td><a href='javascript:AbrirSoporteClinico2(" + item.Id_gestion_documental + "," + item.id_cargue_dtll + ")'>Ver documento</a></td>";
                }
                else
                {
                    result += "<td><a href='javascript:AbrirSoporteClinicoZip(" + item.id_cargue_dtll + ")'>Ver documento Zip</a></td>";
                }

                result += "</tr>";
            }

            return result;
        }

        public ActionResult versoporteclinico2(Int32 idsoporteclinico, Int32 idDtll)
        {
            String mensaje = "";
            try
            {
                Models.FFMM.auditoria_ffmm Model = new Models.FFMM.auditoria_ffmm();
                List<managment_ffmm_documentosResult> List = Model.GetSoportesListFFMM(idDtll);
                List = List.Where(x => x.Id_gestion_documental == idsoporteclinico).ToList();
                foreach (var item in List)
                {
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, item.ruta);
                    WebClient User = new WebClient();
                    item.ruta = dirpath;
                    string filename = item.ruta;
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
                return View();
            }
            catch (Exception ex)
            {
                mensaje = "*---ERROR -- - *" + ex.Message;
                return null;
            }

        }

        public JsonResult ActualizarFacturaDig(Int32 id_factura, Int32 estado)
        {
            Models.FFMM.CuentasRecepcion Model = new Models.FFMM.CuentasRecepcion();
            String mensaje = "";
            try
            {

                Model.ActualizarFFMMFacturas(id_factura, estado, ref MsgRes);
                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR AL ACTUALIZAR.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        // Leo

        //leo




        #endregion
    }
}
