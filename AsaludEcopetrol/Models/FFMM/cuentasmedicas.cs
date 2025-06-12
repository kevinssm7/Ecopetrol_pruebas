using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.FFMM
{
    public class cuentasmedicas
    {
        #region PROPIEDADES

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


        public int id_ffmm_Cuentas_auditoria { get; set; }

        public int id_ffmm_cuentas_glosas { get; set; }

        public int id_factura_ffmm { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = ":")]
        public int cobro_pago { get; set; }

        public int cobro_conciliacion { get; set; }

        public int numero_lote { get; set; }

        public string año_mes_radicado { get; set; }




        public int dv { get; set; }

        public int codigo_habilitacion { get; set; }

        public String naturaleza_juridica { get; set; }



        public String departamento { get; set; }

        public String nombres_apellidos_usuario { get; set; }

        public String tipo_id { get; set; }

        public DateTime? fecha_nacimiento { get; set; }

        public int edadN { get; set; }

        public String edadT { get; set; }

        public String grupo_etario { get; set; }

        public String sexo { get; set; }

        public String estado_afilicacion { get; set; }

        public String sitio_adscripcion { get; set; }

        public String tipo_afiliacion { get; set; }

        public String grado { get; set; }

        public String fuerza { get; set; }

        [Required]
        [MaxLength(50), MinLength(4)]
        [Display(Name = "Numero autorización")]
        public String numero_autorizacion { get; set; }

        public DateTime? fecha_autorizacion { get; set; }

        [Required]
        public DateTime? fecha_ingreso_ips { get; set; }

        public DateTime? fecha_egreso_ips { get; set; }

        public int dias_estancia_ips { get; set; }



        public DateTime? fecha_emision_factura { get; set; }



        public String nit_contratante { get; set; }

        public String modalidad_pago { get; set; }

        public String numero_contrato { get; set; }

        public Decimal vlr_contrato { get; set; }

        public DateTime? fecha_inicio_contrato { get; set; }

        public String origen_servicio { get; set; }

        public String servicio { get; set; }

        public String tipo_servicio { get; set; }

        public String especialidad_remitio { get; set; }

        public String especialidad_remite { get; set; }

        public String grupo_alto_costo { get; set; }

        public String tipo_img_procedimiento_diag { get; set; }

        public String nivel_atencion { get; set; }

        public String nivel_complejidad { get; set; }

        public String cie10_principal { get; set; }

        public String descripcion_cie10_principal { get; set; }

        public String cie10_secundario { get; set; }

        public String descripcion_cie10_secundario { get; set; }

        public String codigo_cups { get; set; }

        public String descripcion_cups { get; set; }

        public String cie10_principal_egreso { get; set; }

        public String descripcion_cia10_principal_egreso { get; set; }

        public String cie10_secundario_egreso { get; set; }

        public String descripcion_cie10_secundario_egrso { get; set; }

        public String herido_en_combate { get; set; }

        public DateTime? fecha_ingreso_hospitalizacion { get; set; }

        public DateTime? fecha_egreso_hospitalizacion { get; set; }

        public int dias_estancia_hospitalizacion { get; set; }

        public DateTime? fecha_ingreso_uci { get; set; }

        public DateTime? fecha_egreso_uci { get; set; }

        public int dias_estancias_uci { get; set; }

        public DateTime? fecha_ingreso_intermedios { get; set; }

        public DateTime? fecha_egreso_intermedios { get; set; }

        public int dias_instancia_intermedios { get; set; }

        public String identificacion_glosa { get; set; }

        public DateTime? fecha_glosa_inicial { get; set; }

        public DateTime? fecha_notificacion_glosa { get; set; }

        public Decimal vlr_glosa_inicial { get; set; }

        public String codigo_glosa_inicial { get; set; }

        public String descripcion_glosa_inicial { get; set; }

        public String descrpcion_glosa_auditor { get; set; }

        public Decimal vlr_pagar_primer_auditoria { get; set; }

        public DateTime? fecha_respuesta_glosa_inicial { get; set; }

        public String codigo_respuesta_glosa { get; set; }

        public String descripcion_respuesta_glosa { get; set; }

        public Decimal vlr_aceptado_respuesta_glosa { get; set; }

        public Decimal vlr_levantado_contratista_res_glosa { get; set; }

        public Decimal vlr_glosa_ratificada_res_glosa_primera_conciliacion { get; set; }

        public DateTime? fecha_primera_conciliacion { get; set; }

        public Decimal vlr_aceptado_primera_conciliacion { get; set; }

        public Decimal vlr_levantado_primera_conciliacion { get; set; }

        public Decimal vlr_glosa_ratificada_contratista_segunda_conciliacion { get; set; }

        public DateTime? fecha_segunda_conciliacion { get; set; }

        public Decimal vlr_aceptado_segunda_conciliacion { get; set; }

        public Decimal vlr_levantado_segunda_conciliacion { get; set; }

        public Decimal vlr_glosa_definitiva_segunda_conciliacion { get; set; }

        public String descripcion_glosa_definitiva { get; set; }

        public DateTime? fecha_acta_respuesta_glosa { get; set; }

        public int numero_acta_respuesta_glosa { get; set; }

        public DateTime? fecha_acta_primera_conciliacion { get; set; }

        public int numero_acta_primera_conciliacion { get; set; }

        public DateTime? fecha_acta_segunda_conciliacion { get; set; }

        public int numero_acta_segunda_conciliacion { get; set; }

        public String analista_cuentas_medicas { get; set; }

        public String auditor_profesional_salud_cuentas { get; set; }

        public String auditor_medico_cuentas { get; set; }

        public DateTime? fecha_entrega_contratistas_factura { get; set; }

        public DateTime? fecha_entrega_contratistas_respuesta_glosa { get; set; }

        public DateTime? fecha_entrega_contratistas_primera_conciliacion_glosa { get; set; }

        public DateTime? fecha_entraga_contratistas_segunda_conciliacion_glosa { get; set; }

        public String procesoMyProperty { get; set; }



        public int id_ref_ffmm_radicacion_Cuentas { get; set; }

        public string tipoAuditor { get; set; }
        public DateTime? fecha_presentacion_factura { get; set; }
        public DateTime? fecha_presentacion_facturaok { get; set; }

        public String optradio { get; set; }
        public String unidad_regionalizadora { get; set; }

        public String unidad_satelite { get; set; }

        public String proveedor { get; set; }

        public int nit { get; set; }

        public String prefijo_factura { get; set; }

        public int numero_factura { get; set; }

        public Decimal vlr_factura { get; set; }

        public Decimal vlr_nota_credito { get; set; }

        public Decimal vlr_atencion { get; set; }

        public String devolucion { get; set; }

        public Int32 cod_dane_municipio { get; set; }

        public Int32 cod_dane_departamento { get; set; }

        public String usuario_digita { get; set; }

        public int numero_id { get; set; }

        public DateTime? fecha_digita { get; set; }

        public String usuario_dijita { get; set; }

        public Int32 estado_rad { get; set; }







        private List<Ref_ffmm_unidad_cp> _FFMM_unudaCP;
        public List<Ref_ffmm_unidad_cp> FFMM_unudaCP
        {
            get
            {
                if (_FFMM_unudaCP == null)
                {
                    return _FFMM_unudaCP = BusClass.FFMM_Unidad_CP();
                }
                else
                {
                    return _FFMM_unudaCP;
                }
            }

            set
            {
                _FFMM_unudaCP = value;
            }
        }

        private List<Ref_ffmm_unidad_satelite> _FFMM_unudadS;
        public List<Ref_ffmm_unidad_satelite> FFMM_unudadS
        {
            get
            {
                if (_FFMM_unudadS == null)
                {
                    return _FFMM_unudadS = new List<Ref_ffmm_unidad_satelite>();
                }
                else
                {
                    return _FFMM_unudadS;

                }
            }
            set
            {
                _FFMM_unudadS = value;
            }
        }

        private List<Ref_ffmm_prestadores> _FFMM_prestador;
        public List<Ref_ffmm_prestadores> FFMM_prestador
        {
            get
            {
                if (_FFMM_prestador == null)
                {
                    return _FFMM_prestador = BusClass.FFMM_prestadores();
                }
                else
                {
                    return _FFMM_prestador;
                }

            }

            set
            {
                _FFMM_prestador = value;
            }
        }

        private List<Ref_ffmm_prestadores_proveedor> _FFMM_prestadorProveedor;
        public List<Ref_ffmm_prestadores_proveedor> FFMM_prestadorProveedor
        {
            get
            {
                if (_FFMM_prestadorProveedor == null)
                {
                    return _FFMM_prestadorProveedor = new List<Ref_ffmm_prestadores_proveedor>();
                }
                else
                {
                    return _FFMM_prestadorProveedor;
                }

            }

            set
            {
                _FFMM_prestadorProveedor = value;
            }
        }

        private List<vw_ffmm_ips> _FFMM_ips;
        public List<vw_ffmm_ips> FFMM_ips
        {
            get
            {
                if (_FFMM_ips == null)
                {
                    return _FFMM_ips = new List<vw_ffmm_ips>();
                }
                else
                {
                    return _FFMM_ips;
                }

            }

            set
            {
                _FFMM_ips = value;
            }
        }

        private List<vw_ffmm_departamento> _FFMM_departamento;
        public List<vw_ffmm_departamento> FFMM_departamento
        {
            get
            {
                if (_FFMM_departamento == null)
                {
                    _FFMM_departamento = BusClass.FFMM_departamento();

                    return _FFMM_departamento;

                }
                else
                {
                    return _FFMM_departamento;
                }

            }

            set
            {
                _FFMM_departamento = value;
            }
        }

        private List<Ref_tipo_documental> _FFMM_TipoID;
        public List<Ref_tipo_documental> FFMM_TipoID
        {
            get
            {
                if (_FFMM_TipoID == null)
                {
                    return _FFMM_TipoID = BusClass.GetTipoDocumnetal();
                }
                else
                {
                    return _FFMM_TipoID;
                }

            }

            set
            {
                _FFMM_TipoID = value;
            }
        }

        private List<Ref_ffmm_sexo> _FFMM_sexo;
        public List<Ref_ffmm_sexo> FFMM_sexo
        {
            get
            {
                if (_FFMM_sexo == null)
                {
                    return _FFMM_sexo = BusClass.FFMM_sexo();
                }
                else
                {
                    return _FFMM_sexo;
                }

            }

            set
            {
                _FFMM_sexo = value;
            }
        }

        private List<Ref_ffmm_estado> _FFMM_estado;
        public List<Ref_ffmm_estado> FFMM_estado
        {
            get
            {
                if (_FFMM_estado == null)
                {
                    return _FFMM_estado = BusClass.FFMM_estado();
                }
                else
                {
                    return _FFMM_estado;
                }

            }

            set
            {
                _FFMM_estado = value;
            }
        }

        private List<Ref_ffmm_tipo_afiliacion> _FFMM_tipoA;
        public List<Ref_ffmm_tipo_afiliacion> FFMM_tipoA
        {
            get
            {
                if (_FFMM_tipoA == null)
                {
                    return _FFMM_tipoA = BusClass.FFMM_tipo_afiliacion();
                }
                else
                {
                    return _FFMM_tipoA;
                }

            }

            set
            {
                _FFMM_tipoA = value;
            }
        }

        private List<Ref_ffmm_fuerza> _FFMM_fuerza;
        public List<Ref_ffmm_fuerza> FFMM_fuerza
        {
            get
            {
                if (_FFMM_fuerza == null)
                {
                    return _FFMM_fuerza = BusClass.FFMM_fuerza();
                }
                else
                {
                    return _FFMM_fuerza;
                }

            }

            set
            {
                _FFMM_fuerza = value;
            }
        }

        private List<Ref_ffmm_modalidad_pago> _FFMM_Modalidad_Pagos;
        public List<Ref_ffmm_modalidad_pago> FFMM_Modalidad_Pagos
        {
            get
            {
                if (_FFMM_Modalidad_Pagos == null)
                {
                    return _FFMM_Modalidad_Pagos = BusClass.FFMM_modalidad_pago();
                }
                else
                {
                    return _FFMM_Modalidad_Pagos;
                }

            }

            set
            {
                _FFMM_Modalidad_Pagos = value;
            }
        }

        private List<Ref_ffmm_origen_servicio> _FFMM_OrigenServicio;
        public List<Ref_ffmm_origen_servicio> FFMM_OrigenServicio
        {
            get
            {
                if (_FFMM_OrigenServicio == null)
                {
                    return _FFMM_OrigenServicio = BusClass.FFMM_origen_servicio();
                }
                else
                {
                    return _FFMM_OrigenServicio;
                }

            }

            set
            {
                _FFMM_OrigenServicio = value;
            }
        }

        private List<Ref_ffmm_servicio> _FFMM_Servicio;
        public List<Ref_ffmm_servicio> FFMM_Servicio
        {
            get
            {
                if (_FFMM_Servicio == null)
                {
                    return _FFMM_Servicio = BusClass.FFMM_servicio();
                }
                else
                {
                    return _FFMM_Servicio;
                }

            }

            set
            {
                _FFMM_Servicio = value;
            }
        }

        private List<Ref_ffmm_tipo_servicio> _FFMM_tipoServicio;
        public List<Ref_ffmm_tipo_servicio> FFMM_tipoServicio
        {
            get
            {
                if (_FFMM_tipoServicio == null)
                {
                    return _FFMM_tipoServicio = BusClass.FFMM_tipo_servicio();
                }
                else
                {
                    return _FFMM_tipoServicio;
                }

            }

            set
            {
                _FFMM_tipoServicio = value;
            }
        }

        private List<Ref_ffmm_especialidad_remitio> _FFMM_especialidadR;
        public List<Ref_ffmm_especialidad_remitio> FFMM_especialidadR
        {
            get
            {
                if (_FFMM_especialidadR == null)
                {
                    return _FFMM_especialidadR = BusClass.FFMM_especialidad_remitio();
                }
                else
                {
                    return _FFMM_especialidadR;
                }

            }

            set
            {
                _FFMM_especialidadR = value;
            }
        }

        private List<Ref_ffmm_especialidad_remite> _FFMM_especialidadRE;
        public List<Ref_ffmm_especialidad_remite> FFMM_especialidadRE
        {
            get
            {
                if (_FFMM_especialidadRE == null)
                {
                    return _FFMM_especialidadRE = BusClass.FFMM_especialidad_remite();
                }
                else
                {
                    return _FFMM_especialidadRE;
                }

            }

            set
            {
                _FFMM_especialidadRE = value;
            }
        }

        private List<Ref_ffmm_alto_costo> _FFMM_AltoCosto;
        public List<Ref_ffmm_alto_costo> FFMM_AltoCosto
        {
            get
            {
                if (_FFMM_AltoCosto == null)
                {
                    return _FFMM_AltoCosto = BusClass.FFMM_altocosto();
                }
                else
                {
                    return _FFMM_AltoCosto;
                }

            }

            set
            {
                _FFMM_AltoCosto = value;
            }
        }

        private List<Ref_ffmm_imagen_proc> _FFMM_imagen;
        public List<Ref_ffmm_imagen_proc> FFMM_imagen
        {
            get
            {
                if (_FFMM_imagen == null)
                {
                    return _FFMM_imagen = BusClass.FFMM_imagen_proc();
                }
                else
                {
                    return _FFMM_imagen;
                }

            }

            set
            {
                _FFMM_imagen = value;
            }
        }

        private List<Ref_ffmm_nivel_atencion> _FFMM_nivelAt;
        public List<Ref_ffmm_nivel_atencion> FFMM_nivelAt
        {
            get
            {
                if (_FFMM_nivelAt == null)
                {
                    return _FFMM_nivelAt = BusClass.FFMM_nivel_atencion();
                }
                else
                {
                    return _FFMM_nivelAt;
                }

            }

            set
            {
                _FFMM_nivelAt = value;
            }
        }

        private List<Ref_ffmm_nivel_complejidad> _FFMM_nivelCom;
        public List<Ref_ffmm_nivel_complejidad> FFMM_nivelCom
        {
            get
            {
                if (_FFMM_nivelCom == null)
                {
                    return _FFMM_nivelCom = BusClass.FFMM_nivel_complejidad();
                }
                else
                {
                    return _FFMM_nivelCom;
                }

            }

            set
            {
                _FFMM_nivelCom = value;
            }
        }


        private List<Ref_ffmm_sino> _FFMM_sino;
        public List<Ref_ffmm_sino> FFMM_sino
        {
            get
            {
                if (_FFMM_sino == null)
                {
                    return _FFMM_sino = BusClass.FFMM_sino();
                }
                else
                {
                    return _FFMM_sino;
                }

            }

            set
            {
                _FFMM_sino = value;
            }
        }


        private List<vw_ffmm_municipio> _FFMM_Municipio;
        public List<vw_ffmm_municipio> FFMM_Municipio
        {
            get
            {
                if (_FFMM_Municipio == null)
                {
                    return _FFMM_Municipio = new List<vw_ffmm_municipio>();
                }
                else
                {
                    return _FFMM_Municipio;
                }

            }

            set
            {
                _FFMM_Municipio = value;
            }
        }

        private List<vw_FMM_RefGeneral> _FFMM_RefGeneral;
        public List<vw_FMM_RefGeneral> FFMM_RefGeneral
        {
            get
            {
                if (_FFMM_RefGeneral == null)
                {
                    return _FFMM_RefGeneral = BusClass.FFMM_Reg_General();
                }
                else
                {
                    return _FFMM_RefGeneral;
                }

            }

            set
            {
                _FFMM_RefGeneral = value;
            }
        }

        private List<Ref_ffmm_tipo_proveedor> _FFMM_TipoProveedor;
        public List<Ref_ffmm_tipo_proveedor> FFMM_TipoProveedor
        {
            get
            {
                if (_FFMM_TipoProveedor == null)
                {
                    return _FFMM_TipoProveedor = BusClass.FFMM_tipo_proveedor();
                }
                else
                {
                    return _FFMM_TipoProveedor;
                }

            }

            set
            {
                _FFMM_TipoProveedor = value;
            }
        }


        private List<vw_ffmm_consulta_radicacion_ips> _ListRecepcionIps;
        public List<vw_ffmm_consulta_radicacion_ips> ListRecepcion
        {
            get
            {
                if (_ListRecepcionIps == null)
                {
                    return _ListRecepcionIps = new List<vw_ffmm_consulta_radicacion_ips>();
                }
                else
                {
                    return _ListRecepcionIps;
                }

            }

            set
            {
                _ListRecepcionIps = value;
            }
        }

        private List<Ref_ffmm_glosas> _ListGlosas;
        public List<Ref_ffmm_glosas> ListGlosas
        {
            get
            {
                if (_ListGlosas == null)
                {
                    return _ListGlosas = BusClass.FFMM_glosas();
                }
                else
                {
                    return _ListGlosas;
                }

            }

            set
            {
                _ListGlosas = value;
            }
        }

        private List<ffmm_Cuentas_auditoria> _ListRecepcionIpsT;
        public List<ffmm_Cuentas_auditoria> ListRecepcionT
        {
            get
            {
                if (_ListRecepcionIpsT == null)
                {
                    return _ListRecepcionIpsT = new List<ffmm_Cuentas_auditoria>();
                }
                else
                {
                    return _ListRecepcionIpsT;
                }

            }

            set
            {
                _ListRecepcionIpsT = value;
            }
        }

        private List<Ref_ffmm_glosas_respuesta> _ListGlosasR;
        public List<Ref_ffmm_glosas_respuesta> ListGlosasR
        {
            get
            {
                if (_ListGlosasR == null)
                {
                    return _ListGlosasR = BusClass.FFMM_respuestas_glosas();
                }
                else
                {
                    return _ListGlosasR;
                }
            }
            set
            {
                _ListGlosasR = value;
            }
        }

        private List<ffmm_cuentas_glosas> _ListTotalGlosas;
        public List<ffmm_cuentas_glosas> ListTotalGlosas
        {
            get
            {
                if (_ListTotalGlosas == null)
                {
                    return _ListTotalGlosas = new List<ffmm_cuentas_glosas>();
                }
                else
                {
                    return _ListTotalGlosas;
                }
            }
            set
            {
                _ListTotalGlosas = value;
            }
        }

        private List<ffmm_Cuentas_radicacion> _LisRadicacionIPSFacturas;
        public List<ffmm_Cuentas_radicacion> LisRadicacionIPSFacturas
        {
            get
            {
                if (_LisRadicacionIPSFacturas == null)
                {
                    return _LisRadicacionIPSFacturas = new List<ffmm_Cuentas_radicacion>();
                }
                else
                {
                    return _LisRadicacionIPSFacturas;
                }
            }
            set
            {
                _LisRadicacionIPSFacturas = value;
            }
        }

        private List<Ref_servicio_tratante> _LstServicioTratante;
        public List<Ref_servicio_tratante> LstServicioTratante
        {
            get
            {
                if (_LstServicioTratante == null)
                {
                    _LstServicioTratante = BusClass.GetServiciotratante();
                    _LstServicioTratante = _LstServicioTratante.Where(x => x.estado == "A").ToList();
                    _LstServicioTratante = _LstServicioTratante.OrderBy(x => x.des).ToList();
                }

                return _LstServicioTratante;
            }
            set { _LstServicioTratante = value; }
        }

        #endregion

        #region FUNCIONES

        public List<Ref_ffmm_alto_costo> FFMM_altocosto()
        {
            return BusClass.FFMM_altocosto();
        }

        public void ConsultaListaSatelite(string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            FFMM_unudadS = BusClass.FFMM_unidad_satelite();
            FFMM_unudadS = FFMM_unudadS.Where(x => x.id_ref_ffmm_unudadR == Convert.ToInt32(strFiltro)).ToList();

        }

        public void ConsultaMunicipio(string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            FFMM_Municipio = BusClass.FFMM_municipio();
            FFMM_Municipio = FFMM_Municipio.Where(x => x.cod_departamento == Convert.ToInt32(strFiltro)).ToList();
        }

        public void ConsultaIPS(string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            FFMM_ips = BusClass.FFMM_prestadores_vw();
            FFMM_ips = FFMM_ips.Where(x => x.cod_municipio == Convert.ToInt32(strFiltro)).ToList();
        }

        public void ConsultaIPSProeveedor(string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            FFMM_prestadorProveedor = BusClass.FFMM_prestadores_Proveedor();
            FFMM_prestadorProveedor = FFMM_prestadorProveedor.Where(x => x.cod_municipio == Convert.ToInt32(strFiltro)).ToList();
        }

        public void ConsultaIPSFactura(string strFiltro, String strFiltro2, string prefijo, ref MessageResponseOBJ MsgRes)
        {
            LisRadicacionIPSFacturas = BusClass.GetRadicacionIPSFacturas(Convert.ToInt32(strFiltro), Convert.ToInt32(strFiltro2), prefijo, ref MsgRes);
        }

        public void BuscarRadicacionIPsId(Int32 id)
        {
            ListRecepcion = BusClass.GetOdontogRadicacionIPS();
            ListRecepcion = ListRecepcion.Where(x => x.id_ref_ffmm_radicacion_Cuentas == id).ToList();

            foreach (var item in ListRecepcion)
            {
                id_ref_ffmm_radicacion_Cuentas = item.id_ref_ffmm_radicacion_Cuentas;
                fecha_presentacion_factura = item.fecha_presentacion_factura;
                proveedor = item.nombre_razon_social_ips_proveedor;
                numero_factura = item.numero_factura.Value;
                vlr_factura = item.vlr_factura.Value;
                vlr_nota_credito = item.vlr_nota_credito.Value;
                vlr_atencion = item.vlr_atencion.Value;
                prefijo_factura = item.prefijo_factura;
                estado_rad = item.estado.Value;

            }
        }

        public void BuscarRadicacionIPsIdTotal(Int32 id)
        {
            ListRecepcionT = BusClass.GetIPSTotal(id, ref MsgRes);


            foreach (var item in ListRecepcionT)
            {
                id_ref_ffmm_radicacion_Cuentas = item.id_ref_ffmm_radicacion_Cuentas.Value;
                id_ffmm_Cuentas_auditoria = item.id_ffmm_Cuentas_auditoria;
                cobro_pago = item.cobro_pago.Value;
                cobro_conciliacion = item.cobro_conciliacion.Value;
                numero_lote = item.numero_lote.Value;
                nombres_apellidos_usuario = item.nombres_apellidos_usuario;
                tipo_id = item.tipo_id;
                numero_id = Convert.ToInt32(item.numero_id.Value);
                fecha_nacimiento = item.fecha_nacimiento;
                edadN = item.edadN.Value;
                edadT = item.edadT;
                grupo_etario = item.grupo_etario;
                sexo = item.sexo;
                estado_afilicacion = item.estado_afilicacion;
                sitio_adscripcion = item.sitio_adscripcion;
                tipo_afiliacion = item.tipo_afiliacion;
                grado = item.grado;
                fuerza = item.fuerza;
                numero_autorizacion = item.numero_autorizacion;
                fecha_autorizacion = item.fecha_autorizacion;
                fecha_ingreso_ips = item.fecha_ingreso_ips;
                fecha_egreso_ips = item.fecha_egreso_ips;
                dias_estancia_ips = item.dias_estancia_ips.Value;
                fecha_emision_factura = item.fecha_emision_factura;
                nit_contratante = item.nit_contratante;
                modalidad_pago = item.modalidad_pago;
                numero_contrato = item.numero_contrato;
                vlr_contrato = item.vlr_contrato.Value;
                fecha_inicio_contrato = item.fecha_inicio_contrato;
                origen_servicio = item.origen_servicio;
                servicio = item.servicio;
                tipo_servicio = item.tipo_servicio;
                especialidad_remitio = item.especialidad_remitio;
                especialidad_remite = item.especialidad_remite;
                grupo_alto_costo = item.grupo_alto_costo;
                tipo_img_procedimiento_diag = item.tipo_img_procedimiento_diag;
                nivel_atencion = item.nivel_atencion;
                nivel_complejidad = item.nivel_complejidad;
                cie10_principal = item.cie10_principal;
                descripcion_cie10_principal = item.descripcion_cie10_principal;
                cie10_secundario = item.cie10_secundario;
                descripcion_cie10_secundario = item.descripcion_cie10_secundario;
                codigo_cups = item.codigo_cups;
                descripcion_cups = item.descripcion_cups;
                cie10_principal_egreso = item.cie10_principal_egreso;
                descripcion_cia10_principal_egreso = item.descripcion_cia10_principal_egreso;
                cie10_secundario_egreso = item.cie10_secundario_egreso;
                descripcion_cie10_secundario_egrso = item.descripcion_cie10_secundario_egrso;
                herido_en_combate = item.herido_en_combate;
                fecha_ingreso_hospitalizacion = item.fecha_ingreso_hospitalizacion;
                fecha_egreso_hospitalizacion = item.fecha_egreso_hospitalizacion;
                dias_estancia_hospitalizacion = item.dias_estancia_hospitalizacion.Value;
                fecha_ingreso_uci = item.fecha_ingreso_uci;
                fecha_egreso_uci = item.fecha_egreso_uci;
                dias_estancias_uci = item.dias_estancias_uci.Value;
                fecha_ingreso_intermedios = item.fecha_ingreso_intermedios;
                fecha_egreso_intermedios = item.fecha_egreso_intermedios;
                dias_instancia_intermedios = item.dias_instancia_intermedios.Value;
                identificacion_glosa = item.identificacion_glosa;
                fecha_glosa_inicial = item.fecha_glosa_inicial;
                fecha_notificacion_glosa = item.fecha_notificacion_glosa;
                vlr_glosa_inicial = item.vlr_glosa_inicial.Value;
                codigo_glosa_inicial = item.codigo_glosa_inicial;
                descripcion_glosa_inicial = item.descripcion_glosa_inicial;
                descrpcion_glosa_auditor = item.descrpcion_glosa_auditor;
                vlr_pagar_primer_auditoria = item.vlr_pagar_primer_auditoria.Value;
            }
        }

        public void BuscarGlosas(Int32 id)
        {
            ListTotalGlosas = BusClass.GetIPSTotalGlosas(id, ref MsgRes);


            foreach (var item in ListTotalGlosas)
            {
                id_ref_ffmm_radicacion_Cuentas = item.id_ref_ffmm_radicacion_Cuentas.Value;
                id_ffmm_Cuentas_auditoria = item.id_ffmm_Cuentas_auditoria;
                id_ffmm_cuentas_glosas = item.id_ffmm_cuentas_glosas;

                fecha_respuesta_glosa_inicial = item.fecha_respuesta_glosa_inicial;
                codigo_respuesta_glosa = item.codigo_respuesta_glosa;
                descripcion_respuesta_glosa = item.descripcion_respuesta_glosa;
                vlr_aceptado_respuesta_glosa = item.vlr_aceptado_respuesta_glosa.Value;
                vlr_levantado_contratista_res_glosa = item.vlr_levantado_contratista_res_glosa.Value;
                vlr_glosa_ratificada_res_glosa_primera_conciliacion = item.vlr_glosa_ratificada_res_glosa_primera_conciliacion.Value;

                fecha_primera_conciliacion = item.fecha_primera_conciliacion;
                vlr_aceptado_primera_conciliacion = Convert.ToDecimal(item.vlr_aceptado_primera_conciliacion);
                vlr_levantado_primera_conciliacion = Convert.ToDecimal(item.vlr_levantado_primera_conciliacion);
                vlr_glosa_ratificada_contratista_segunda_conciliacion = Convert.ToDecimal(item.vlr_glosa_ratificada_contratista_segunda_conciliacion);

                fecha_segunda_conciliacion = item.fecha_segunda_conciliacion;
                vlr_aceptado_segunda_conciliacion = Convert.ToDecimal(item.vlr_aceptado_segunda_conciliacion);
                vlr_levantado_segunda_conciliacion = Convert.ToDecimal(item.vlr_levantado_segunda_conciliacion);
                vlr_glosa_definitiva_segunda_conciliacion = Convert.ToDecimal(item.vlr_glosa_definitiva_segunda_conciliacion);
                descripcion_glosa_definitiva = item.descripcion_glosa_definitiva;

                //fecha_acta_respuesta_glosa = item.fecha_acta_respuesta_glosa;
                //numero_acta_respuesta_glosa = item.numero_acta_respuesta_glosa.Value;
                //fecha_acta_primera_conciliacion = item.fecha_acta_primera_conciliacion;
                //numero_acta_primera_conciliacion = item.numero_acta_primera_conciliacion.Value;
                //fecha_acta_segunda_conciliacion = item.fecha_acta_segunda_conciliacion;
                //numero_acta_segunda_conciliacion = item.numero_acta_segunda_conciliacion.Value;





            }
        }

        public Int32 InsertarFFMMAuditoria(ffmm_Cuentas_auditoria OBJ, List<ffmm_cuentas_medicas_cups> obj2, List<ffmm_cuentas_medicas_glosas> obj3, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFFMMAuditoria(OBJ, obj2, obj3, ref MsgRes);
        }
        public void ActualizarEstadoRadicacion(ffmm_Cuentas_radicacion obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarEstadoRadicacion(obj2, ref MsgRes);
        }
        public Int32 InsertarFFMMAuditoriaGlosas(ffmm_cuentas_glosas OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFFMMAuditoriaGlosas(OBJ, ref MsgRes);
        }

        public void ActualizarEstadoGlosa(ffmm_cuentas_glosas obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarEstadoGlosa(obj2, ref MsgRes);
        }

        public void ActualizarEstadoGlosaSegundaConci(ffmm_cuentas_glosas obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarEstadoGlosaSegundaConci(obj2, ref MsgRes);
        }

        public List<ffmm_Cuentas_auditoria> GetCuentasAuditoria(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetCuentasAuditoria(ref MsgRes);

        }

        public List<management_CupsAuditoriaResult> ListaCupsAuditoria()
        {
            return BusClass.ListaCupsAuditoria();
        }

        public ffmm_Cuentas_auditoria ultimoPagoyConciliacion()
        {
            return BusClass.ultimoPagoyConciliacion();
        }

        #endregion
        /// <summary>
        ///  DAVID FONSECA 
        /// </summary>

        public DateTime? fecha_auditoria { get; set; }
        public int tipo_visita { get; set; }
        public int visita_numero { get; set; }
        public int contratista_servicios_dom_ips { get; set; }






    }
}