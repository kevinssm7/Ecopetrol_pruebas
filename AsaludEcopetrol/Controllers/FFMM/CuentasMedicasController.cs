using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.FFMM
{
    [SessionExpireFilter]

    public class CuentasMedicasController : Controller
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
        Models.FFMM.CuidadosPaliativos ModelCP = new Models.FFMM.CuidadosPaliativos();


        #endregion

        public ActionResult IngresoCuentasMedicas()
        {

            Models.FFMM.cuentasmedicas Model = new Models.FFMM.cuentasmedicas();

            ViewData["Alerta"] = "";
            ViewData["tipoalerta"] = 0;
            ViewBag.listaRegionales = BusClass.GetRefRegion();

            ViewBag.refcohortes = Model.FFMM_altocosto().ToList();
            return View();
        }

        public ActionResult IngresoCuentasRecepcion(int? alerta, Int32? id_factura)
        {
            Models.General General = new Models.General();

            if (alerta == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transacción exitosa!", "Ingresó el prestador exitosamente!");
            }

            else if (alerta == 2)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Error!", "No se ingresó el prestador exitosamente!");
            }
            else
            {
                ViewData["alerta"] = "";
            }

            ViewBag.meses = BusClass.meses();

            Models.FFMM.CuentasRecepcion Model = new Models.FFMM.CuentasRecepcion();

            if (id_factura != null)
            {
                List<managmentFFMMfacturasRadicadasid_dtllResult> lst = Model.GetRecepcionFacturasDTLLFFMMId(id_factura.Value, ref MsgRes);

                foreach (var item in lst)
                {
                    DateTime? fecha = Convert.ToDateTime(DateTime.Now);
                    ViewBag.añomes = item.fecha_exp_factura.Value.ToString("MM/dd/yyyy");
                    ViewBag.FechaFactura = fecha.Value.ToString("MM/dd/yyyy");
                    Model.optradio = Convert.ToString(item.des_tipo);
                    ViewBag.tipo2 = item.tipo;

                    ViewBag.cod_dane_departamento = Convert.ToInt32(item.id_ref_regional);
                    ViewBag.cod_dane_municipio = item.id_ref_ciudades.Value;
                    ViewBag.proveedor = Convert.ToString(item.prestador);


                    //if (item.tipo == 1)
                    //{
                    //    ViewBag.cod_dane_departamento = Convert.ToInt32(item.id_ref_regional);
                    //    ViewBag.cod_dane_municipio = item.id_ref_ciudades.Value;
                    //    ViewBag.proveedor = Convert.ToString(item.prestador);
                    //}
                    //else
                    //{
                    //    ViewBag.cod_dane_departamento_proveedor = Convert.ToInt32(item.id_ref_regional);
                    //    ViewBag.cod_dane_municipio_proveedor = item.id_ref_ciudades.Value;
                    //}

                    Model.numero_factura = Convert.ToInt32(item.num_factura);
                    Model.vlr_factura = Convert.ToInt32(item.valor_neto);
                    Model.prefijo_factura = Convert.ToString(item.prefijo_factura);
                    ViewBag.factura = Convert.ToInt32(item.valor_neto);
                    ViewBag.id_factura = id_factura;

                }
                ViewBag.id_factura = id_factura;
            }
            else
            {

            }
            ViewBag.tipo = BusClass.tipoIpsPrestador();
            ViewBag.regionales = BusClass.FFMM_departamento();
            ViewBag.unidad = BusClass.FFMM_unudadR();
            return View(Model);
        }

        public string ObtenerSatelite(int? unidad)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_ffmm_unidad_satelite> satelites = new List<Ref_ffmm_unidad_satelite>();
            satelites = BusClass.FFMM_unidad_satelite().Where(x => x.id_ref_ffmm_unudadR == unidad).ToList();

            if (satelites.Count() > 0)
            {
                foreach (var item in satelites)
                {
                    result += "<option value='" + item.id_ref_ffmm_unidad_satelite + "'>" + item.descripciion + "</option>";
                }
            }

            return result;
        }

        public string ObtenerCiudades(int? idDepartamento)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<vw_ffmm_municipio> ciudades = new List<vw_ffmm_municipio>();
            ciudades = BusClass.GetMunicipiosFM((int)idDepartamento, ref MsgRes);

            if (ciudades.Count() > 0)
            {
                foreach (var item in ciudades)
                {
                    result += "<option value='" + item.cod_municipio + "'>" + item.muninombre + "</option>";
                }
            }

            return result;
        }

        public string ObtenerIpsPrestador(int? idCiudad, int? tipo)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<ref_ffmm_ips_prestadores> ipsPrestador = new List<ref_ffmm_ips_prestadores>();

            if (idCiudad != null && tipo != null)
            {
                ipsPrestador = BusClass.ObtenerIpsPrestador((int)idCiudad, (int)tipo);

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


        public ActionResult CuentasAuditoria()
        {
            Models.FFMM.CuentasRecepcion Model = new Models.FFMM.CuentasRecepcion();

            return View(Model);
        }

        public ActionResult IngresoCuentasAuditoria(String id)
        {
            Models.FFMM.cuentasmedicas Model = new Models.FFMM.cuentasmedicas();
            List<ffmm_Cuentas_auditoria> lista = new List<ffmm_Cuentas_auditoria>();
            List<Ref_ffmm_fuerza> fuerza = new List<Ref_ffmm_fuerza>();
            Model.BuscarRadicacionIPsId(Convert.ToInt32(id));
            lista = Model.GetCuentasAuditoria(ref MsgRes);
            int maximo = lista.Max(p => p.numero_lote).Value;
            if (maximo == null)
            {
                Model.numero_lote = 1;
            }
            else
            {
                Model.numero_lote = (maximo + 1);
            }

            fuerza = BusClass.FFMM_fuerza();
            ViewBag.fuerza = fuerza;

            List<ref_auditor> auditor = new List<ref_auditor>();
            auditor = BusClass.listAuditor();

            ViewBag.auditor = auditor;

            Session["OtraGlosa"] = new List<ffmm_cuentas_medicas_glosas>();
            Session["OtraCup"] = new List<ffmm_cuentas_medicas_cups>();
            ViewBag.fecha = String.Format("{0:d}", Model.fecha_presentacion_factura);
            ViewBag.cantidadCups = 0;
            ViewBag.cantidadGlosas = 0;
            return View(Model);
        }

        public ActionResult VerCuentasAuditoria(String id)
        {
            Models.FFMM.cuentasmedicas Model = new Models.FFMM.cuentasmedicas();

            Model.BuscarRadicacionIPsId(Convert.ToInt32(id));
            Model.BuscarRadicacionIPsIdTotal(Convert.ToInt32(id));
            Model.BuscarGlosas(Convert.ToInt32(id));

            return View(Model);
        }




        public JsonResult AgregarOtraCup(int? codigo, String descripcion)
        {
            var result = "";

            List<ffmm_cuentas_medicas_cups> Agregadas = (List<ffmm_cuentas_medicas_cups>)Session["OtraCup"];

            ffmm_cuentas_medicas_cups NewCup = new ffmm_cuentas_medicas_cups
            {
                id_detalles_cup = 0,
                codigo_cups = codigo,
                descripcion_cups = descripcion,
                usuario_digita = SesionVar.UserName,
                fecha_digita = DateTime.Now
            };
            try
            {
                Agregadas.Add(NewCup);
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            Session["OtraCup"] = Agregadas;
            ViewBag.cantidadCups = Agregadas.Count();

            int i = 0;

            foreach (ffmm_cuentas_medicas_cups a in Agregadas)
            {
                i += 1;

                result += "<tr>"
                    + "<td>" + i + "</td>";

                result += "<td>" + a.codigo_cups + "</td>";
                result += "<td>" + a.descripcion_cups + "</td>";
                result += "<td class='text-center'><a title='Remover' href='javascript:removerCups(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                result += "</tr>";
            }

            var data = new
            {
                tabla = result,
                countAsistentes = Agregadas.Count(),

            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RemoverCups(Int32? idOtraCup)
        {
            string result = "";

            List<ffmm_cuentas_medicas_cups> ListaCups = (List<ffmm_cuentas_medicas_cups>)Session["OtraCup"];

            ListaCups.Remove(ListaCups[Convert.ToInt32(idOtraCup) - 1]);

            Session["OtraCup"] = ListaCups;

            int i = 0;

            if (ListaCups.Count != 0)
            {

                foreach (ffmm_cuentas_medicas_cups a in ListaCups)
                {
                    i += 1;

                    result += "<tr>"
                        + "<td>" + i + "</td>";

                    result += "<td>" + a.codigo_cups + "</td>";
                    result += "<td>" + a.descripcion_cups + "</td>"
                        + "<td class='text-center'><a title='Remover' href='javascript:removerCups(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                    result += "</tr>";
                }
            }
            else
            {
                result += "<tr> <td colspan='10' style='text-align:center'> " + "No se han agregado Cups. " + "</td> </tr>";
            }

            var data = new
            {
                tabla = result,
                conteoCups = ListaCups.Count(),
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AgregarOtraGlosa(int? codigoGlosaInicial, String descripcionGlosaInicial)
        {
            var result = "";

            List<ffmm_cuentas_medicas_glosas> Agregadas = (List<ffmm_cuentas_medicas_glosas>)Session["OtraGlosa"];

            ffmm_cuentas_medicas_glosas NewCup = new ffmm_cuentas_medicas_glosas
            {
                id_auditoria_otraGlosa = 0,
                codigo_glosa_inicial = codigoGlosaInicial,
                descripcion_glosa_inicial = descripcionGlosaInicial,
                usuario_digita = SesionVar.UserName,
                fecha_digita = DateTime.Now
            };
            try
            {
                Agregadas.Add(NewCup);
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            Session["OtraGlosa"] = Agregadas;
            ViewBag.cantidadGlosas = Agregadas.Count();

            int i = 0;

            foreach (ffmm_cuentas_medicas_glosas a in Agregadas)
            {
                i += 1;

                result += "<tr>"
                    + "<td>" + i + "</td>";

                result += "<td>" + a.codigo_glosa_inicial + "</td>";
                result += "<td>" + a.descripcion_glosa_inicial + "</td>";
                result += "<td class='text-center'><a title='Remover' href='javascript:removerGlosa(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                result += "</tr>";
            }

            var data = new
            {
                tabla = result,
                conteoGlosas = Agregadas.Count(),

            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RemoverGlosa(Int32? idOtraGlosa)
        {
            string result = "";

            List<ffmm_cuentas_medicas_glosas> ListaGlosas = (List<ffmm_cuentas_medicas_glosas>)Session["OtraGlosa"];

            ListaGlosas.Remove(ListaGlosas[Convert.ToInt32(idOtraGlosa) - 1]);

            Session["OtraGlosa"] = ListaGlosas;

            int i = 0;

            if (ListaGlosas.Count != 0)
            {

                foreach (ffmm_cuentas_medicas_glosas a in ListaGlosas)
                {
                    i += 1;

                    result += "<tr>"
                    + "<td>" + i + "</td>";

                    result += "<td>" + a.codigo_glosa_inicial + "</td>";
                    result += "<td>" + a.descripcion_glosa_inicial + "</td>";
                    result += "<td class='text-center'><a title='Remover' href='javascript:removerGlosa(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                    result += "</tr>";
                }
            }
            else
            {
                result += "<tr> <td colspan='10' style='text-align:center'> " + "No se han agregado Glosas. " + "</td> </tr>";
            }

            var data = new
            {
                tabla = result,
                conteoGlosas = ListaGlosas.Count(),
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult IngresoProveedor()
        {
            Models.FFMM.CuentasRecepcion Model = new Models.FFMM.CuentasRecepcion();

            return View(Model);
        }

        public ActionResult IngresoCuidadosPaliativos()
        {
            Models.FFMM.cuentascuidadosP Model = new Models.FFMM.cuentascuidadosP();
            ViewBag.Unidad = Model.FFMM_unudadR.OrderBy(p => p.descripcion);
            ViewBag.TipoVisita = Model.ListFFMM_TipoV_CP.OrderBy(p => p.descripcion);
            ViewBag.Departamento = Model.GetDepartamentos();
            ViewBag.Municipio = new List<vw_ffmm_municipio>();
            ViewBag.IPS = Model.FFMM_ips.OrderBy(p => p.muninombre);

            List<String> PercepcionLista = new List<String>();
            PercepcionLista.Add("BUENA");
            PercepcionLista.Add("REGULAR");
            PercepcionLista.Add("MALA");

            ViewBag.PercepcionLista = PercepcionLista;


            return View(Model);
        }

        public ActionResult CuidadosPaleativos()
        {

            ViewBag.UnidadSatelite = ModelCP.GetUnidadSatelite(ref MsgRes).Where(p => p.concurrencia.Equals("SI")).OrderBy(p => p.descripciion);
            ViewBag.Unidad = ModelCP.GetUnidad(ref MsgRes).OrderBy(p => p.descripcion);
            ViewBag.TipoVisita = ModelCP.GetTipoVisita(ref MsgRes).OrderBy(p => p.descripcion);
            ViewBag.Departamento = ModelCP.GetDepartamentos(ref MsgRes);
            ViewBag.TipoIdentificacion = ModelCP.GetTipoIdentificacion(ref MsgRes).OrderBy(p => p.descripcion);
            ViewBag.Sexo = ModelCP.GetSexo(ref MsgRes);
            ViewBag.SitioAdscripcion = ModelCP.GetSitioAdscripcion(ref MsgRes);
            ViewBag.TipoAfiliacion = ModelCP.GetTipoAfiliacion(ref MsgRes);
            ViewBag.Estado = ModelCP.GetEstado(ref MsgRes);
            ViewBag.Fuerza = ModelCP.GetFuerza(ref MsgRes);
            ViewBag.SiNo = ModelCP.GetSiNo(ref MsgRes);
            ViewBag.CodigoCIE10 = ModelCP.GetCie10(ref MsgRes);
            List<String> PercepcionLista = new List<String>();
            PercepcionLista.Add("BUENA");
            PercepcionLista.Add("REGULAR");
            PercepcionLista.Add("MALA");

            ViewBag.PercepcionLista = PercepcionLista;

            return View(ModelCP);
        }

        [HttpPost]
        public ActionResult CuentasAuditoria(Models.FFMM.CuentasRecepcion Model)
        {
            Model.BuscarRadicacionIPs();

            return View(Model);
        }

        [HttpPost]
        public ActionResult IngresoProveedor(Models.FFMM.CuentasRecepcion Model)
        {
            Ref_ffmm_prestadores_proveedor obj = new Ref_ffmm_prestadores_proveedor();

            obj.cod_departamento = Model.cod_dane_departamento;
            obj.cod_municipio = Model.cod_dane_municipio;
            obj.codigohabilitacion = Model.codigohabilitacion;
            obj.nombre = Model.proveedor;
            obj.nit = Model.nit;
            obj.direccion = Model.direccion;
            obj.telefono = Convert.ToString(Model.telefono);
            obj.najunombre = Model.najunombre;

            Model.InsertarFFMMref_proveedor(obj, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return RedirectToAction("IngresoCuentasRecepcion", "CuentasMedicas", new { alerta = 1 });
            }
            else
            {
                return RedirectToAction("IngresoCuentasRecepcion", "CuentasMedicas", new { alerta = 2 });
            }
            return View(Model);
        }


        public JsonResult SaveRecepcion(Models.FFMM.CuentasRecepcion Model)
        {
            String mensaje = "";
            try
            {

                ffmm_Cuentas_radicacion obj = new ffmm_Cuentas_radicacion();

                obj.mes = Model.mes;
                obj.año = Model.año;
                obj.fecha_presentacion_factura = Model.fecha_presentacion_factura;
                obj.unidad_regionalizadora = Model.unidad_regionalizadora;
                obj.unidad_satelite = Model.unidad_satelite;
                obj.tipo_proveedor = Model.optradio;

                obj.ips_proveedor = Convert.ToInt32(Model.proveedor);
                obj.cod_dane_departamento = Model.cod_dane_departamento;
                obj.cod_dane_municipio = Model.cod_dane_municipio;

                obj.nit = Model.nit;
                obj.prefijo_factura = Model.prefijo_factura;
                obj.numero_factura = Model.numero_factura;
                obj.vlr_factura = Model.vlr_factura;
                obj.vlr_nota_credito = Model.vlr_nota_credito;
                obj.vlr_atencion = Model.vlr_atencion;
                obj.devolucion = Model.devolucion;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.estado = 1;
                if (Model.id_factura != null)
                {
                    obj.tipo_radicacion = 2;
                }
                else
                {
                    obj.tipo_radicacion = 1;

                }

                obj.id_ref_ffmm_radicacion_Cuentas = Model.InsertarFFMMRadicacion(obj, ref MsgRes);
                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    if (Model.id_factura != null)
                    {
                        Model.ActualizarFFMMFacturas(Model.id_factura, 4, ref MsgRes);
                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensaje = "SE INGRESO CORRECTAMENTE....";
                            return Json(new { success = true, message = mensaje, ID = obj.id_ref_ffmm_radicacion_Cuentas, variable = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            mensaje = "ERROR EL INGRESO...";
                            return Json(new { success = false, message = mensaje, variable = 2 }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        mensaje = "SE INGRESO CORRECTAMENTE....";
                        return Json(new { success = true, message = mensaje, ID = obj.id_ref_ffmm_radicacion_Cuentas, variable = 1 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "ERROR EL INGRESO...";
                    return Json(new { success = false, message = mensaje, variable = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(false);
            }
        }

        public JsonResult SaveAuditoria(Models.FFMM.cuentasmedicas Model)
        {
            String mensaje = "";
            try
            {

                List<ffmm_cuentas_medicas_cups> OtraCup = new List<ffmm_cuentas_medicas_cups>();
                OtraCup = (List<ffmm_cuentas_medicas_cups>)Session["OtraCup"];
                    
                List<ffmm_cuentas_medicas_glosas> ListaGlosas = new List<ffmm_cuentas_medicas_glosas>();
                ListaGlosas = (List<ffmm_cuentas_medicas_glosas>)Session["OtraGlosa"];

                ffmm_Cuentas_auditoria obj = new ffmm_Cuentas_auditoria();

                obj.cobro_pago = Model.cobro_pago;
                obj.cobro_conciliacion = Model.cobro_conciliacion;
                obj.numero_lote = Model.numero_lote;
                obj.nombres_apellidos_usuario = Model.nombres_apellidos_usuario;
                obj.tipo_id = Model.tipo_id;
                obj.numero_id = Model.numero_id;
                obj.fecha_nacimiento = Model.fecha_nacimiento;
                obj.edadN = Model.edadN;
                obj.edadT = Model.edadT;
                obj.grupo_etario = Model.grupo_etario;
                obj.sexo = Model.sexo;
                obj.estado_afilicacion = Model.estado_afilicacion;
                obj.sitio_adscripcion = Model.sitio_adscripcion;
                obj.tipo_afiliacion = Model.tipo_afiliacion;
                obj.grado = Model.grado;

                obj.fuerza = Model.fuerza;
                obj.numero_autorizacion = Model.numero_autorizacion;
                obj.fecha_autorizacion = Model.fecha_autorizacion;
                obj.fecha_ingreso_ips = Model.fecha_ingreso_ips;
                obj.fecha_egreso_ips = Model.fecha_egreso_ips;
                obj.dias_estancia_ips = Model.dias_estancia_ips;
                obj.fecha_emision_factura = Model.fecha_emision_factura;
                obj.nit_contratante = Model.nit_contratante;
                obj.modalidad_pago = Model.modalidad_pago;
                obj.numero_contrato = Model.numero_contrato;
                obj.vlr_contrato = Model.vlr_contrato;
                obj.fecha_inicio_contrato = Model.fecha_inicio_contrato;
                obj.origen_servicio = Model.origen_servicio;
                obj.servicio = Model.servicio;
                obj.tipo_servicio = Model.tipo_servicio;
                obj.especialidad_remitio = "1";

                obj.especialidad_remite = "1";
                obj.grupo_alto_costo = Model.grupo_alto_costo;
                obj.tipo_img_procedimiento_diag = Model.tipo_img_procedimiento_diag;
                obj.nivel_atencion = Model.nivel_atencion;
                obj.nivel_complejidad = Model.nivel_complejidad;
                obj.cie10_principal = Model.cie10_principal;
                obj.descripcion_cie10_principal = Model.descripcion_cie10_principal;
                obj.cie10_secundario = Model.cie10_secundario;
                obj.descripcion_cie10_secundario = Model.descripcion_cie10_secundario;
                obj.codigo_cups = Model.codigo_cups;
                obj.descripcion_cups = Model.descripcion_cups;
                obj.cie10_principal_egreso = Model.cie10_principal_egreso;
                obj.descripcion_cia10_principal_egreso = Model.descripcion_cia10_principal_egreso;
                obj.cie10_secundario_egreso = Model.cie10_secundario_egreso;
                obj.descripcion_cie10_secundario_egrso = Model.descripcion_cie10_secundario_egrso;
                obj.herido_en_combate = Model.herido_en_combate;

                obj.fecha_ingreso_hospitalizacion = Model.fecha_ingreso_hospitalizacion;
                obj.fecha_egreso_hospitalizacion = Model.fecha_egreso_hospitalizacion;
                obj.dias_estancia_hospitalizacion = Model.dias_estancia_hospitalizacion;
                obj.fecha_ingreso_uci = Model.fecha_ingreso_uci;
                obj.fecha_egreso_uci = Model.fecha_egreso_uci;
                obj.dias_estancias_uci = Model.dias_estancias_uci;
                obj.fecha_ingreso_intermedios = Model.fecha_ingreso_intermedios;
                obj.fecha_egreso_intermedios = Model.fecha_egreso_intermedios;
                obj.dias_instancia_intermedios = Model.dias_instancia_intermedios;
                obj.identificacion_glosa = Model.identificacion_glosa;
                obj.fecha_glosa_inicial = Model.fecha_glosa_inicial;
                obj.fecha_notificacion_glosa = Model.fecha_notificacion_glosa;
                obj.vlr_glosa_inicial = Model.vlr_glosa_inicial;
                obj.codigo_glosa_inicial = Model.codigo_glosa_inicial;
                obj.descripcion_glosa_inicial = Model.descripcion_glosa_inicial;
                obj.descrpcion_glosa_auditor = Model.descrpcion_glosa_auditor;
                obj.vlr_pagar_primer_auditoria = Model.vlr_pagar_primer_auditoria;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.id_ref_ffmm_radicacion_Cuentas = Model.id_ref_ffmm_radicacion_Cuentas;
                obj.tipoAuditor = Model.tipoAuditor;

                obj.id_ffmm_Cuentas_auditoria = Model.InsertarFFMMAuditoria(obj, OtraCup, ListaGlosas, ref MsgRes);
                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    ffmm_Cuentas_radicacion obj2 = new ffmm_Cuentas_radicacion();

                    obj2.id_ref_ffmm_radicacion_Cuentas = Model.id_ref_ffmm_radicacion_Cuentas;
                    if (Model.identificacion_glosa == "1")
                    {
                        obj2.estado = 2;
                    }
                    else
                    {
                        obj2.estado = 3;
                    }

                    Model.ActualizarEstadoRadicacion(obj2, ref MsgRes);

                    mensaje = "SE INGRESO CORRECTAMENTE....";

                    return Json(new { success = true, message = mensaje, ID = obj.id_ffmm_Cuentas_auditoria }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    mensaje = "ERROR EL INGRESO...";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(false);
            }

        }

        public JsonResult SaveCuidadosPaleativos(Models.FFMM.CuidadosPaliativos Model)
        {
            String mensaje = "";

            ffmm_cuidados_paliativos objeto = new ffmm_cuidados_paliativos();
            try
            {

                objeto.fecha_auditoria = Model.fecha_auditoria;
                objeto.id_ref_ffmm_unidad_cp = Model.id_ref_ffmm_unidad_cp;
                objeto.id_ref_tipo_visita = Model.id_ref_tipo_visita;
                objeto.visita_numero = Model.visita_numero;
                objeto.contratista_servicios_dom_ips = Model.contratista_servicios_dom_ips;
                objeto.nombre_apellidos = Model.nombre_apellidos;
                objeto.tipoId = Model.tipoId;
                objeto.numero_id = Model.numero_id;
                objeto.fecha_nacimiento = Model.fecha_nacimiento;
                objeto.edadN = Model.edadN;
                objeto.edadT = Model.edadT;
                objeto.grupoEtario = Model.grupoEtario;
                objeto.sexo = Model.sexo;
                objeto.id_estado_afiliacion = Model.id_estado_afiliacion;
                objeto.id_sitio_adscripcion = Model.id_sitio_adcripcion;
                objeto.id_tipo_afiliacion = Model.id_tipo_afiliacion;
                objeto.grado = Model.grado;
                objeto.id_fuerza = Model.id_fuerza;
                objeto.fecha_ingreso_al_programa = Model.fecha_ingreso_al_programa;
                objeto.cumple_criterios = Model.cumple_criterios;
                objeto.criterio_que_no_cumple = Model.criterio_que_no_cumple;
                objeto.cod_cie10Principal = Model.cod_cie10Principal;
                objeto.codigo_cie10_secun = Model.codigo_cie10_secun;
                objeto.fecha_ultima_valoracion = Model.fecha_ultima_valoracion;
                objeto.cantidad_terapia_fisica = Model.cantidad_terapia_fisica;
                objeto.periodidad_terapia_fisica = Model.periodidad_terapia_fisica;
                objeto.pertinencia_medica_terapia_fisica = Model.pertinencia_medica_terapia_fisica;
                objeto.cantidad_terapia_lenguaje = Model.cantidad_terapia_lenguaje;
                objeto.periodidad_terapia_lenguaje = Model.periodidad_terapia_lenguaje;
                objeto.pertinencia_terapia_lenguaje = Model.pertinencia_terapia_lenguaje;
                objeto.cantidad_terapia_ocupacional = Model.cantidad_terapia_ocupacional;
                objeto.periodidad_terapia_ocupacional = Model.periodidad_terapia_ocupacional;
                objeto.pertinencia_terapia_ocupacional = Model.pertinencia_terapia_ocupacional;
                objeto.cantidad_terapia_respiratoria = Model.cantidad_terapia_respiratoria;
                objeto.periodidad_terapia_respiratoria = Model.periodidad_terapia_respiratoria;
                objeto.pertinencia_terapia_respiratoria = Model.pertinencia_terapia_respiratoria;
                objeto.cantidad_psicologia = Model.cantidad_psicologia;
                objeto.periodidad_psicologica = Model.periodidad_psicologica;
                objeto.pertinencia_medica_psicologia = Model.pertinencia_medica_psicologia;
                objeto.cantidad_clinica_heridas = Model.cantidad_clinica_heridas;
                objeto.periodidad_clinica_heridas = Model.periodidad_clinica_heridas;
                objeto.pertinencia_clinica_heridas = Model.pertinencia_clinica_heridas;
                objeto.servicio_enfermeria = Model.servicio_enfermeria;

                if (Model.servicio_enfermeria == "1")
                {
                    objeto.cantidas_horas_periocidad_servicio_enfermeria = (Model.cantidas_horas_periocidad_servicio_enfermeria + " / " + Model.periocidad_servicio_enfermeria);
                }
                else
                {
                    objeto.cantidas_horas_periocidad_servicio_enfermeria = null;
                }

                objeto.pertinencia_servicio_de_enfermeria = Model.pertinencia_servicio_de_enfermeria;
                objeto.servicio_cuidador = Model.servicio_cuidador;
                objeto.cantidad_horas_periodidad_cuidador = Model.cantidad_horas_periodidad_cuidador;
                objeto.pertinencia_cuidador = Model.pertinencia_cuidador;
                objeto.otros_servicios = Model.otros_servicios;
                objeto.escala_discapacidad = Model.escala_discapacidad;
                objeto.medicion_segun_escala_discapacidad = Model.medicion_segun_escala_discapacidad;
                objeto.plan_de_manejo = Model.plan_de_manejo;
                objeto.objetivos_terapeuticos = Model.objetivos_terapeuticos;
                objeto.percepcion_prestacion_servicio = Model.percepcion_prestacion_servicio;
                objeto.observaciones = Model.observaciones;
                objeto.tutela = Model.tutela;
                objeto.concepto_auditoria_med = Model.concepto_auditoria_med;
                objeto.auditor_medico_concurrente = SesionVar.UserName;

                objeto.telefono_usuario = Model.telefono_usuario;
                objeto.celular_usuario = Model.celular_usuario;
                objeto.direccion_usuario = Model.direccion_usuario;
                objeto.barrio = Model.barrio;
                objeto.fecha_tutela = Model.fecha_tutela;
                objeto.cobertura_fallo = Model.cobertura_fallo;
                objeto.nombre_juzgado = Model.nombre_juzgado;
                objeto.cod_municipio_usuario = Model.cod_municipio_usuario;
                objeto.cod_departamento_usuario = Model.cod_departamento_usuario;




                ModelCP.SaveCuidadosPaliativos(objeto, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }



        public JsonResult SaveGlosa(Models.FFMM.cuentasmedicas Model, HttpPostedFileBase file)
        {
            String mensaje = "";
            try
            {
                if (Model.estado_rad == 2)
                {
                    string strRetorno = string.Empty;
                    StringBuilder sbRutaDefinitiva;
                    string strRutaDefinitiva = string.Empty;
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentos"];
                    sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                    string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                    string ruta2 = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                    MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                    string strError = string.Empty;

                    DateTime fecha = DateTime.Now;
                    string archivo = string.Empty;

                    String carpeta = "";
                    String Subcarpeta = "RespuestaGlosa";

                    if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                    {
                        carpeta = "FacturacionFFMM";
                    }
                    else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                    {
                        carpeta = "FacturacionFFMMPruebas";
                    }

                    ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + Subcarpeta + "\\" + Model.id_ffmm_Cuentas_auditoria);

                    var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                    archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                    fecha, nombre, Path.GetExtension(file.FileName));


                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    string fileExt1 = System.IO.Path.GetExtension(file.FileName);
                    string nombreActual1 = file.FileName;
                    string nombreCarpeta = nombreActual1.Remove(nombreActual1.Length - fileExt1.Length);

                    file.SaveAs(archivo);


                    ffmm_cuentas_glosas obj = new ffmm_cuentas_glosas();

                    obj.id_ref_ffmm_radicacion_Cuentas = Model.id_ref_ffmm_radicacion_Cuentas;
                    obj.id_ffmm_Cuentas_auditoria = Model.id_ffmm_Cuentas_auditoria;
                    obj.fecha_respuesta_glosa_inicial = Model.fecha_respuesta_glosa_inicial;
                    obj.codigo_respuesta_glosa = Model.codigo_respuesta_glosa;
                    obj.descripcion_respuesta_glosa = Model.descripcion_respuesta_glosa;
                    obj.vlr_aceptado_respuesta_glosa = Model.vlr_aceptado_respuesta_glosa;
                    obj.vlr_levantado_contratista_res_glosa = Model.vlr_levantado_contratista_res_glosa;
                    obj.vlr_glosa_ratificada_res_glosa_primera_conciliacion = Model.vlr_glosa_ratificada_res_glosa_primera_conciliacion;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_dijita = SesionVar.UserName;
                    obj.ruta_acta_respuesta_glosa = Convert.ToString(archivo);


                    obj.id_ffmm_cuentas_glosas = Model.InsertarFFMMAuditoriaGlosas(obj, ref MsgRes);
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {

                        ffmm_Cuentas_radicacion obj2 = new ffmm_Cuentas_radicacion();

                        obj2.id_ref_ffmm_radicacion_Cuentas = Model.id_ref_ffmm_radicacion_Cuentas;
                        if (Model.vlr_glosa_ratificada_res_glosa_primera_conciliacion == 0)
                        {
                            obj2.estado = 3;
                        }
                        else
                        {
                            obj2.estado = 4;
                        }

                        Model.ActualizarEstadoRadicacion(obj2, ref MsgRes);

                        mensaje = "SE INGRESO CORRECTAMENTE....";

                        return Json(new { success = true, message = mensaje, ID = obj.id_ffmm_Cuentas_auditoria }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO...";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }

                }
                else if (Model.estado_rad == 4)
                {
                    ffmm_cuentas_glosas obj = new ffmm_cuentas_glosas();

                    obj.id_ref_ffmm_radicacion_Cuentas = Model.id_ref_ffmm_radicacion_Cuentas;
                    obj.id_ffmm_Cuentas_auditoria = Model.id_ffmm_Cuentas_auditoria;
                    obj.fecha_primera_conciliacion = Model.fecha_primera_conciliacion;
                    obj.vlr_aceptado_primera_conciliacion = Model.vlr_aceptado_primera_conciliacion;
                    obj.vlr_levantado_primera_conciliacion = Model.vlr_levantado_primera_conciliacion;
                    obj.vlr_glosa_ratificada_contratista_segunda_conciliacion = Model.vlr_glosa_ratificada_contratista_segunda_conciliacion;

                    Model.ActualizarEstadoGlosa(obj, ref MsgRes);
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {

                        ffmm_Cuentas_radicacion obj2 = new ffmm_Cuentas_radicacion();

                        obj2.id_ref_ffmm_radicacion_Cuentas = Model.id_ref_ffmm_radicacion_Cuentas;
                        obj.id_ffmm_Cuentas_auditoria = Model.id_ffmm_Cuentas_auditoria;
                        if (Model.vlr_glosa_ratificada_contratista_segunda_conciliacion == 0)
                        {
                            obj2.estado = 5;
                        }
                        else
                        {
                            obj2.estado = 6;
                        }

                        Model.ActualizarEstadoRadicacion(obj2, ref MsgRes);

                        mensaje = "SE INGRESO CORRECTAMENTE....";

                        return Json(new { success = true, message = mensaje, ID = obj.id_ffmm_Cuentas_auditoria }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO...";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (Model.estado_rad == 6)
                {
                    ffmm_cuentas_glosas obj = new ffmm_cuentas_glosas();

                    obj.id_ref_ffmm_radicacion_Cuentas = Model.id_ref_ffmm_radicacion_Cuentas;
                    obj.id_ffmm_Cuentas_auditoria = Model.id_ffmm_Cuentas_auditoria;
                    obj.fecha_segunda_conciliacion = Model.fecha_segunda_conciliacion;
                    obj.vlr_aceptado_segunda_conciliacion = Model.vlr_aceptado_segunda_conciliacion;
                    obj.vlr_levantado_segunda_conciliacion = Model.vlr_levantado_segunda_conciliacion;
                    obj.vlr_glosa_definitiva_segunda_conciliacion = Model.vlr_glosa_definitiva_segunda_conciliacion;
                    obj.descripcion_glosa_definitiva = Model.descripcion_glosa_definitiva;


                    Model.ActualizarEstadoGlosaSegundaConci(obj, ref MsgRes);
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {

                        ffmm_Cuentas_radicacion obj2 = new ffmm_Cuentas_radicacion();

                        obj2.id_ref_ffmm_radicacion_Cuentas = Model.id_ref_ffmm_radicacion_Cuentas;
                        obj.id_ffmm_Cuentas_auditoria = Model.id_ffmm_Cuentas_auditoria;
                        if (Model.vlr_glosa_ratificada_contratista_segunda_conciliacion == 0)
                        {
                            obj2.estado = 7;
                        }
                        else
                        {
                            obj2.estado = 8;
                        }

                        Model.ActualizarEstadoRadicacion(obj2, ref MsgRes);

                        mensaje = "SE INGRESO CORRECTAMENTE....";

                        return Json(new { success = true, message = mensaje, ID = obj.id_ffmm_Cuentas_auditoria }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO...";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "ERROR EL INGRESO...";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(false);
            }


        }

        public JsonResult SaveCuentas(Models.FFMM.cuentasmedicas Model)
        {
            String mensaje = "";

            mensaje = "ERROR EL INGRESO DEL DETALLE.";
            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCascadeUnidad(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_unudaCP.Select(c => new { id_ref_ffmm_unidad_cp = c.id_ref_ffmm_unidad_cp, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeSatelite(string unidad, Models.FFMM.cuentasmedicas Model)
        {
            Model.ConsultaListaSatelite(unidad, ref MsgRes);

            return Json(Model.FFMM_unudadS.Select(p => new { id_ref_ffmm_unidad_satelite = p.id_ref_ffmm_unidad_satelite, nombre = p.descripciion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadedepartamentoFFMM(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_departamento.Select(c => new { cod_departamento = c.cod_departamento, descripcion = c.departamento }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadedepartamentoProveedorFFMM(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_departamento.Select(c => new { cod_departamento = c.cod_departamento, descripcion = c.departamento }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeMunicipioFFMM(string depart, Models.FFMM.cuentasmedicas Model)
        {
            Model.ConsultaMunicipio(depart, ref MsgRes);

            return Json(Model.FFMM_Municipio.Select(p => new { cod_municipio = p.cod_municipio, muninombre = p.muninombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeMunicipioProveFFMM(string depart, Models.FFMM.cuentasmedicas Model)
        {
            Model.ConsultaMunicipio(depart, ref MsgRes);

            return Json(Model.FFMM_Municipio.Select(p => new { cod_municipio = p.cod_municipio, muninombre = p.muninombre }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCascadeIPSFFMM(string munic, Models.FFMM.cuentasmedicas Model)
        {
            Model.ConsultaIPS(munic, ref MsgRes);

            return Json(Model.FFMM_ips.Select(p => new { id_ref_ffmm_prestadores = p.id_ref_ffmm_prestadores, nombre = p.nombre, nit = p.nit, cod_departamento = p.cod_departamento, cod_municipio = p.cod_municipio, codigohabilitacion = p.codigohabilitacion, najunombre = p.najunombre }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCascadeProveedorFFMM(string munic, Models.FFMM.cuentasmedicas Model)
        {
            Model.ConsultaIPSProeveedor(munic, ref MsgRes);

            return Json(Model.FFMM_prestadorProveedor.Select(p => new { id_ref_ffmm_prestadores = p.id_ref_ffmm_prestadores_proveedor, nombre = p.nombre, nit = p.nit, cod_departamento = p.cod_departamento, cod_municipio = p.cod_municipio, codigohabilitacion = p.codigohabilitacion, najunombre = p.najunombre }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCascadeBuscarIPSfAC(string ips, string fac, string prefijo, Models.FFMM.cuentasmedicas Model)
        {
            if (fac == "")
            {
                fac = "0";
            }

            if (ips == "")
            {
                ips = "0";
            }

            Model.ConsultaIPSFactura(ips, fac, prefijo, ref MsgRes);

            if (Model.LisRadicacionIPSFacturas.Count != 0)
            {
                String mensaje = "";

                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCascadeTipoID(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_TipoID.Select(c => new { id_ref_tipo_documental = c.id_ref_tipo_documental, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeSexo(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_sexo.Select(c => new { id_ref_ffmm_sexo = c.id_ref_ffmm_sexo, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeEstadoAfi(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_estado.Select(c => new { id_ref_ffmm_estado = c.id_ref_ffmm_estado, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCascadeTipoAfi(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_tipoA.Select(c => new { id_ref_ffmm_tipo_afiliacion = c.id_ref_ffmm_tipo_afiliacion, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeFuerza(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_fuerza.Select(c => new { id_ref_ffmm_fuerza = c.id_ref_ffmm_fuerza, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeModalidadP(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_Modalidad_Pagos.Select(c => new { id_ref_modalidad_pago = c.id_ref_modalidad_pago, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCascadeOrigenServicio(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_OrigenServicio.Select(c => new { id_ref_ffmm_origen_servicio = c.id_ref_ffmm_origen_servicio, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeServicio(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_Servicio.Select(c => new { id_ref_ffmm_servicio = c.id_ref_ffmm_servicio, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeTServicio(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_tipoServicio.Select(c => new { id_ref_ffmm_tipo_servicio = c.id_ref_ffmm_tipo_servicio, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeEspremitio(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.LstServicioTratante.Select(c => new { id_servicio_tratante = c.id_servicio_tratante, descripcion = c.des }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeEspremite(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.LstServicioTratante.Select(c => new { id_servicio_tratante = c.id_servicio_tratante, descripcion = c.des }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeAltocosto(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_AltoCosto.Select(c => new { id_ref_ffmm_alto_costo = c.id_ref_ffmm_alto_costo, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeimgdiag(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_imagen.Select(c => new { id_ref_ffmm_imagen_proc = c.id_ref_ffmm_imagen_proc, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCascadeNivAtencion(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_nivelAt.Select(c => new { id_ref_ffmm_nivel_atencion = c.id_ref_ffmm_nivel_atencion, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeNivComplejidad(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_nivelCom.Select(c => new { id_ref_ffmm_nivel_complejidad = c.id_ref_ffmm_nivel_complejidad, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCascadesino(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_sino.Select(c => new { id_ref_ffmm_sino = c.id_ref_ffmm_sino, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeTipoProveedor(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_TipoProveedor.Select(c => new { id_ref_ffmm_tipo_proveedor = c.id_ref_ffmm_tipo_proveedor, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCascadesitio_adscripcion(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_RefGeneral.Select(c => new { sitio_adscripcion = c.sitio_adscripcion }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCascadeGrado(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.FFMM_RefGeneral.Select(c => new { grado = c.grado }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeGlosas(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.ListGlosas.Select(c => new { id_glosa = c.id_glosa, motivo = c.motivo }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeGlosasRespuesta(Models.FFMM.cuentasmedicas Model)
        {
            return Json(Model.ListGlosasR.Select(c => new { id_glosa_respuesta = c.id_glosa_respuesta, motivo = c.motivo }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeUnidadCP(Models.FFMM.cuentascuidadosP Model)
        {
            return Json(Model.List_FFMM_Unidad_CP.Select(c => new { id_ref_ffmm_unidad_cp = c.id_ref_ffmm_unidad_cp, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeTipoV_CP(Models.FFMM.cuentascuidadosP Model)
        {
            return Json(Model.ListFFMM_TipoV_CP.Select(c => new { id_ref_ffmm_tipo_visita = c.id_ref_ffmm_tipo_visita, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        // METODOS DAVID
        public string GetMunicipios(int cod_departamento)
        {
            string result = "<option value=''>- Seleccione una opción -</option>";
            List<vw_ffmm_municipio> lista = ModelCP.GetMunicipios(ref MsgRes);
            lista = lista.Where(p => p.cod_departamento == cod_departamento).ToList();

            foreach (var item in lista)
            {
                result += "<option value='" + item.cod_municipio + "'>" + item.muninombre + "</option>";
            }
            return result;
        }

        public string GetIPS(int cod_municipio)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            List<vw_ffmm_ips> lista = ModelCP.GetIPS(ref MsgRes);
            lista = lista.Where(p => p.cod_municipio == cod_municipio).ToList();

            foreach (var item in lista)
            {
                result += "<option value='" + item.id_ref_ffmm_prestadores + "'>" + item.nombre + "</option>";
            }
            return result;
        }

        public string GetDescripcionCie(string id_cie10)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            List<vw_cie10> lista = ModelCP.GetCie10(ref MsgRes);
            lista = lista.Where(p => p.id_cie10 == id_cie10).ToList();

            foreach (var item in lista)
            {
                result += "<option value='" + item.des + "'>" + item.des + "</option>";
            }
            return result;
        }


        public JsonResult GetCie10Principal()
        {
            ObjectCache cache = MemoryCache.Default;
            List<vw_cie10> fileContents = cache["filecontents"] as List<vw_cie10>;
            List<vw_cie10> LstCie102Principal = new List<vw_cie10>();

            if (fileContents != null)
            {
                LstCie102Principal = fileContents;
            }
            else
            {
                LstCie102Principal = ModelCP.GetCie10(ref MsgRes);
                if (fileContents == null || fileContents.Count == 0)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();
                    CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(300.0);
                    fileContents = LstCie102Principal.ToList();

                    cache.Add("filecontents", fileContents, cacheItemPolicy);

                }
            }

            return Json(LstCie102Principal.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult buscaradscripcion()
        {

            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    List<vw_FMM_RefGeneral> Prestadores = BusClass.FFMM_Reg_General().Where(x => x.sitio_adscripcion.Contains(term)).ToList();
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     sitio = reg.sitio_adscripcion,
                                     grado = reg.grado,
                                     label = reg.sitio_adscripcion,
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


        #region Glosas 



        public ActionResult TableroGlosas()
        {

            ViewBag.Departamento = ModelCP.GetDepartamentos(ref MsgRes);
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<Management_FFMM_Glosas_PrestadoresResult> lista = new List<Management_FFMM_Glosas_PrestadoresResult>();
            lista = Model.GetFFMMGlosasPrestadores(ref MsgRes);
            lista = lista.Where(p => p.estado == null).ToList();
            ViewBag.lista = lista;
            return View();


        }

        public PartialViewResult _IngresarFechaVisita(int ID)
        {
            ViewBag.id_prestador = ID;

            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<vw_ffmm_glosas> lista = new List<vw_ffmm_glosas>();
            lista = Model.GetFFMMGlosas(ref MsgRes).ToList();
            lista = lista.Where(p => p.id_ref_ffmm_prestadores == ID && p.estado is null).ToList();
            ViewBag.lista = lista;
            return PartialView();

        }

        public PartialViewResult _IngresarSegundaFechaVisita(int ID)
        {
            ViewBag.id_glosa = ID;
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<vw_ffmm_glosas> lista = new List<vw_ffmm_glosas>();
            lista = Model.GetFFMMGlosas(ref MsgRes).ToList();
            lista = lista.Where(p => p.id_ref_ffmm_prestadores == ID && p.estado == 4).ToList();
            ViewBag.lista = lista;
            return PartialView();


        }


        public PartialViewResult _IngresarEjecucionVisita(int ID)
        {
            ViewBag.id_glosa = ID;
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<vw_ffmm_glosas> lista = new List<vw_ffmm_glosas>();
            lista = Model.GetFFMMGlosas(ref MsgRes).ToList();
            lista = lista.Where(p => p.id_ref_ffmm_prestadores == ID && p.estado == 1).ToList();
            ViewBag.lista = lista;
            return PartialView();
        }

        public PartialViewResult _IngresarEjecucionSegundaVisita(int ID)
        {
            ViewBag.id_glosa = ID;
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<vw_ffmm_glosas> lista = new List<vw_ffmm_glosas>();
            lista = Model.GetFFMMGlosas(ref MsgRes).ToList();
            lista = lista.Where(p => p.id_ref_ffmm_prestadores == ID && p.estado == 1).ToList();
            ViewBag.lista = lista;
            return PartialView();
        }


        public PartialViewResult _IngresarFechaAprobacion(int ID)
        {
            ViewBag.id_glosa = ID;
            return PartialView();
        }

        public PartialViewResult _IngresarFechaFirma(int ID)
        {
            ViewBag.id_glosa = ID;
            return PartialView();
        }



        public PartialViewResult _IngresarFechaPrimeraActa(int ID)
        {
            ViewBag.id_glosa = ID;
            return PartialView();
        }
        public PartialViewResult _IngresarFechaSegundaActa(int ID)
        {
            ViewBag.id_glosa = ID;
            return PartialView();
        }




        public ActionResult TableroDeVisitasProgramadas()
        {
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<Management_FFMM_Glosas_PrestadoresResult> lista = new List<Management_FFMM_Glosas_PrestadoresResult>();
            lista = Model.GetFFMMGlosasPrestadores(ref MsgRes);
            lista = lista.Where(p => p.estado == 1).ToList();
            ViewBag.lista = lista;
            return View();

        }

        public PartialViewResult _TableroVisitasEjecutadas()
        {
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<Management_FFMM_Glosas_PrestadoresResult> lista = new List<Management_FFMM_Glosas_PrestadoresResult>();
            lista = Model.GetFFMMGlosasPrestadores(ref MsgRes);
            lista = lista.Where(p => p.estado == 2).ToList();
            ViewBag.lista = lista;
            return PartialView();
        }

        public PartialViewResult _TableroSegundaVisita()
        {
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<Management_FFMM_Glosas_PrestadoresResult> lista = new List<Management_FFMM_Glosas_PrestadoresResult>();
            lista = Model.GetFFMMGlosasPrestadores(ref MsgRes);
            lista = lista.Where(p => p.estado == 4).ToList();
            ViewBag.lista = lista;
            return PartialView();
        }

        public PartialViewResult _TableroSegundaEjecucion()
        {
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<Management_FFMM_Glosas_PrestadoresResult> lista = new List<Management_FFMM_Glosas_PrestadoresResult>();
            lista = Model.GetFFMMGlosasPrestadores(ref MsgRes);
            lista = lista.Where(p => p.estado == 5).ToList();
            ViewBag.lista = lista;
            return PartialView();
        }




        public PartialViewResult _TableroVisitasFinalizadas()
        {
            ViewBag.Departamento = ModelCP.GetDepartamentos(ref MsgRes);
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<Management_FFMM_Glosas_PrestadoresResult> lista = new List<Management_FFMM_Glosas_PrestadoresResult>();
            lista = Model.GetFFMMGlosasPrestadores(ref MsgRes);
            lista = lista.Where(p => p.estado == 3).ToList();
            ViewBag.lista = lista;
            return PartialView();
        }

        public PartialViewResult _IngresarGestionGlosa(int ID)
        {
            ViewBag.id_glosa = ID;
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<vw_ffmm_glosas> lista = new List<vw_ffmm_glosas>();
            lista = Model.GetFFMMGlosas(ref MsgRes).ToList();
            lista = lista.Where(p => p.id_ref_ffmm_prestadores == ID && p.estado == 2).ToList();
            ViewBag.lista = lista;
            return PartialView();
        }

        public PartialViewResult _IngresarGestionSegundaGlosa(int ID)
        {
            ViewBag.id_glosa = ID;
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<vw_ffmm_glosas> lista = new List<vw_ffmm_glosas>();
            lista = Model.GetFFMMGlosas(ref MsgRes).ToList();
            lista = lista.Where(p => p.id_ref_ffmm_prestadores == ID && p.estado == 5).ToList();
            ViewBag.lista = lista;
            return PartialView();
        }


        public PartialViewResult _IngresarPrimeraConciliacion(int ID)
        {
            ViewBag.id_glosa = ID;
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            ffmm_cuentas_glosas objeto = new ffmm_cuentas_glosas();
            objeto = Model.GetCuentasGlosas(ID, ref MsgRes);
            @ViewBag.vlr_aceptado_respuesta_glosa = objeto.vlr_aceptado_respuesta_glosa;
            @ViewBag.vlr_levantado_contratista_res_glosa = objeto.vlr_levantado_contratista_res_glosa;
            @ViewBag.vlr_glosa_ratificada_res_glosa_primera_conciliacion = objeto.vlr_glosa_ratificada_res_glosa_primera_conciliacion;
            @ViewBag.codigo_respuesta_glosa = (objeto.codigo_respuesta_glosa + " " + objeto.descripcion_respuesta_glosa);
            @ViewBag.fecha_acta_respuesta_glosa = objeto.fecha_respuesta_glosa_inicial;


            return PartialView();
        }

        public PartialViewResult _IngresarSegundaConciliacion(int ID)
        {
            ViewBag.id_glosa = ID;

            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            ffmm_glosas objeto = new ffmm_glosas();
            objeto = Model.GetGlosas(ID, ref MsgRes);
            @ViewBag.vlr_glosa_ratificada_contratista_segunda_conciliacion = objeto.vlr_glosa_ratificada_contratista_segunda_conciliacion;



            return PartialView();
        }


        public PartialViewResult _IngresarDatosFinalesGlosa(int ID)
        {
            ViewBag.id_glosa = ID;
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            List<vw_ffmm_glosas> lista = new List<vw_ffmm_glosas>();
            lista = Model.GetFFMMGlosas(ref MsgRes).ToList();
            lista = lista.Where(p => p.id_ref_ffmm_prestadores == ID && p.estado == 3).ToList();
            ViewBag.lista = lista;
            return PartialView();
        }

        public PartialViewResult _IngresarDatosFinalesGlosaFormulario(int ID)
        {
            ViewBag.id_glosa = ID;
            return PartialView();

        }


        public JsonResult ProgramarVisita(DateTime fecha, int[] arregloGlosas, TimeSpan horaIni)
        {
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            ffmm_glosas objeto = new ffmm_glosas();
            String mensaje = "";
            try
            {

                for (int i = 0; i < arregloGlosas.Length; i++)
                {
                    objeto.id_ffmm_cuentas_auditoria = arregloGlosas[i];
                    objeto.fecha_programacion_visita = fecha;
                    objeto.hora = horaIni;

                    objeto.estado = 1;
                    Model.SaveProgramarVisitaGlosa(objeto, ref MsgRes);
                }

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ProgramarSegundaVisita(DateTime fecha, int[] arregloGlosas, TimeSpan horaIni)
        {
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();
            ffmm_glosas objeto = new ffmm_glosas();
            String mensaje = "";
            try
            {

                var conteoArray = arregloGlosas.Length;
                var resultado = 0;

                for (int i = 0; i < arregloGlosas.Length; i++)
                {
                    objeto.id_glosa = arregloGlosas[i];
                    objeto.fecha_programacion_visita = fecha;
                    objeto.estado = 5;
                    objeto.hora = horaIni;

                    var result = Model.UpdateProgramarVisitaGlosa(objeto, ref MsgRes);
                    if (result != 0)
                    {
                        resultado++;
                    }
                }

                if (resultado == conteoArray)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult EjecutarVisita(DateTime? fecha, int[] arregloGlosas, string observaciones)
        {
            Models.FFMM.Glosas Model = new Models.FFMM.Glosas();

            ffmm_glosas objeto = new ffmm_glosas();

            String mensaje = "";
            try
            {

                objeto.fecha_ejecucion_visita = fecha;
                objeto.observaciones = observaciones;
                objeto.estado = 2;
                for (int i = 0; i < arregloGlosas.Length; i++)
                {
                    objeto.id_glosa = arregloGlosas[i];
                    Model.UpdateGlosa(objeto, "Ejecutar", ref MsgRes);
                }

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SavePrimeraConciliacion(Models.FFMM.Glosas Model)
        {
            ffmm_glosas objeto = new ffmm_glosas();

            String mensaje = "";
            try
            {
                objeto.id_glosa = Model.id_glosa;
                //objeto.fecha_respuesta_glosa_inicial = Model.fecha_respuesta_glosa_inicial;
                //objeto.codigo_respuesta_glosa = Model.codigo_respuesta_glosa;
                //objeto.descripcion_respuesta_glosa = Model.descripcion_respuesta_glosa;
                objeto.vlr_aceptado_respuesta_glosa = Model.vlr_aceptado_respuesta_glosa;
                objeto.vlr_levantado_contratista_res_glosa = Model.vlr_levantado_contratista_res_glosa;
                objeto.vlr_levantado_contratista_res_glosa = Model.vlr_levantado_contratista_res_glosa;
                objeto.fecha_primera_conciliacion = Model.fecha_primera_conciliacion;
                objeto.vlr_aceptado_primera_conciliacion = Model.vlr_aceptado_primera_conciliacion;
                objeto.vlr_levantado_primera_conciliacion = Model.vlr_levantado_primera_conciliacion;
                objeto.vlr_glosa_ratificada_contratista_segunda_conciliacion = Model.vlr_glosa_ratificada_contratista_segunda_conciliacion;
                objeto.vlr_glosa_ratificada_res_glosa_primera_conciliacion = Model.vlr_glosa_ratificada_res_glosa_primera_conciliacion;




                Model.UpdateGlosa(objeto, "PrimeraConciliacion", ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult SaveSegudnaConciliacion(Models.FFMM.Glosas Model)
        {
            ffmm_glosas objeto = new ffmm_glosas();

            String mensaje = "";
            try
            {
                objeto.id_glosa = Model.id_glosa;

                objeto.fecha_segunda_conciliacion = Model.fecha_segunda_conciliacion;
                objeto.vlr_aceptado_segunda_conciliacion = Model.vlr_aceptado_segunda_conciliacion;
                objeto.vlr_levantado_segunda_conciliacion = Model.vlr_levantado_segunda_conciliacion;
                objeto.vlr_glosa_definitiva_segunda_conciliacion = Model.vlr_glosa_definitiva_segunda_conciliacion;
                objeto.descripcion_glosa_definitiva = Model.descripcion_glosa_definitiva;


                Model.UpdateGlosa(objeto, "SegundaConciliacion", ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveDatosFinales(Models.FFMM.Glosas Model)
        {
            ffmm_glosas objeto = new ffmm_glosas();

            String mensaje = "";
            try
            {
                objeto.id_glosa = Model.id_glosa;

                objeto.fecha_acta_primera_conciliacion = Model.fecha_acta_primera_conciliacion;
                objeto.fecha_acta_segunda_conciliacion = Model.fecha_acta_segunda_conciliacion;


                Model.UpdateGlosa(objeto, "DatosFinales", ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SavePrimeraFechaActa(Models.FFMM.Glosas Model)
        {
            ffmm_glosas objeto = new ffmm_glosas();

            String mensaje = "";
            try
            {
                objeto.id_glosa = Model.id_glosa;
                objeto.fecha_acta_primera_conciliacion = Model.fecha_acta_primera_conciliacion;

                Model.UpdateGlosa(objeto, "FechaPrimeraConciliacion", ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult SaveSegundaActa(Models.FFMM.Glosas Model)
        {
            ffmm_glosas objeto = new ffmm_glosas();

            String mensaje = "";
            try
            {
                objeto.id_glosa = Model.id_glosa;
                objeto.fecha_acta_segunda_conciliacion = Model.fecha_acta_segunda_conciliacion;

                Model.UpdateGlosa(objeto, "FechaSegundaConciliacion", ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESÓ CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "*---OCURRIÓ UN ERROR AL INGRESAR LA INFORMACIÓN-- - *" + ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult traerRespuestaGlosa()
        {
            var miproyecto = "";
            List<Ref_ffmm_glosas_respuesta> getRespuestasGlosas = BusClass.FFMM_respuestas_glosas();
            foreach (var item in getRespuestasGlosas)
            {
                miproyecto = item.motivo;
            }

            ViewBag.Lista = getRespuestasGlosas;

            var listatotal = new object();

            listatotal = (from item in getRespuestasGlosas
                          select new
                          {
                              value = item.id_glosa_respuesta,
                              text = item.motivo

                          }); ;

            return Json(listatotal, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _GestionarPrimeraGlosa()
        {

            return PartialView();
        }

        public PartialViewResult _SegundaPrimeraGlosa()
        {

            return PartialView();
        }

        public string ObtenerGrado(int idFuerza)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<management_unionFuerzasgradosResult> grados = new List<management_unionFuerzasgradosResult>();
            grados = BusClass.getUnionFuerzas(idFuerza);

            if (grados.Count() != 0)
            {
                foreach (var item in grados)
                {
                    result += "<option value='" + item.grado + "'>" + item.grado + "</option>";
                }
            }

            return result;
        }

        #endregion

        //leo 27/04/2022

        //kevin 29-04-22
        //kevin 02/05/22
    }
}