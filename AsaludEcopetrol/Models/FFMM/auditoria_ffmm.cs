using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.FFMM
{
    public class auditoria_ffmm
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


        public int id_ffmm_auditoria { get; set; }
        public DateTime? fecha_auditoria { get; set; }
        public String unidad{ get; set; }
        public int id_ref_forma_auditoria { get; set; }
        public int id_ref_tipo_visita { get; set; }
        public Int32 id_ref_ips { get; set; }
        public String cod_dane_municipio { get; set; }
        public int id_municipio { get; set; }
        public String cod_dane_departamento { get; set; }
        public int id_departamento { get; set; }
        public DateTime? fecha_solicitud_autorizacion { get; set; }
        public DateTime? fecha_generacion_autorizacion { get; set; }
        public DateTime? fecha_ingreso_ips { get; set; }
        public DateTime? fecha_egreso_ips { get; set; }
        public int dias_instancia_ips { get; set; }
        public String nombre_apellidos_usuario{ get; set; }
        public String tipoId { get; set; }
        public int numeroId { get; set; }
        public DateTime? fecha_nacimiento { get; set; }
        public int edadN { get; set; }
        public int id_ref_grupo_etario { get; set; }
        public String sexo { get; set; }
        public String estado_afiliacion { get; set; }
        public String sitio_adscripcion { get; set; }
        public int id_ref_tipo_afiliacion { get; set; }
        public String direccion { get; set; }
        public String telefono { get; set; }
        public String grado { get; set; }
        public String fuerza { get; set; }
        public String nivel_atencion { get; set; }
        public int id_ref_nivel_complejidad { get; set; }
        public int id_ref_via_ingreso { get; set; }
        public String otro_via_ingreso { get; set; }
        public String cie10_1 { get; set; }
        public String descripcion_cie10_1 { get; set; }
        public String cie10_2 { get; set; }
        public string descripcion_cie10_2 { get; set; }
        public int is_cups { get; set; }
        public String descripcion_cups { get; set; }
        public String origen_servicio { get; set; }
        public int id_ref_servicio { get; set; }
        public String otro_servicio { get; set; }
        public DateTime? fecha_ingreso_hospitalizacion { get; set; }
        public DateTime? fecha_egreso_hospitalizacion { get; set; }
        public int dias_estancia_hospitalizacion { get; set; }
        public DateTime? fecha_ingreso_uci { get; set; }
        public DateTime? fecha_egreso_uci { get; set; }
        public int dias_estancia_uci { get; set; }
        public DateTime? fecha_ingreso_intermedios { get; set; }
        public DateTime? fecha_egraso_intermedios { get; set; }
        public Int32 dias_estancia_intermedios { get; set; }
        public String estancia_prolongada { get; set; }
        public int id_ref_estancia_prolongada { get; set; }
        public String otro_estancia_prolongada { get; set; }
        public String evento_materno { get; set; }
        public int id_ref_evento_materno { get; set; }
        public String otro_evnto_materno { get; set; }
        public String edad_gestacional { get; set; }
        public int num_controles_prenatales { get; set; }
        public String complicacion_parto_puerperio { get; set; }
        public String otro_complicaciones_parto { get; set; }
        public int id_ref_complicacion_parto { get; set; }
        public string recien_nacido { get; set; }
        public int id_ref_condiciones_recien_nacido { get; set; }
        public String otro_condicion_recien_nacido { get; set; }
        public String patologia_alto_costo { get; set; }
        public String cie10_3 { get; set; }
        public String descripcion_cie10_patologia_alto_costo { get; set; }
        public string alto_riesgo { get; set; }
        public int id_ref_alto_riesgo { get; set; }
        public String otro_alto_riesgo { get; set; }
        public String condicion_trazadora { get; set; }
        public int id_ref_tipo_condicion_trazadora { get; set; }
        public String otro_tipo_condicion_trazadora { get; set; }
        public String evento_adverso { get; set; }
        public String tipo_eveno_adverso { get; set; }
        public DateTime? fecha_presentacion_evento_adverso { get; set; }
        public int id_ref_causal_evento_adverso_eps { get; set; }
        public String otro_causal_evento_adverso_eps { get; set; }
        public int id_ref_causal_evento_adverso_ips { get; set; }
        public String otro_causal_evento_adverso_ips { get; set; }
        public DateTime? fecha_seguimiento_evento_adverso { get; set; }
        public String evento_centinela { get; set; }
        public int id_ref_causal_evento_centinela { get; set; }
        public String otro_causal_evento_centinela { get; set; }
        public String evento_salud_publica { get; set; }
        public int id_ref_causal_evento_salud_publica { get; set; }
        public String causal_evento_salud_publica { get; set; }
        public String complicacion_patologica { get; set; }
        public int id_ref_causal_complicacion_patologica { get; set; }
        public String otro_causal_complicacion_patologica { get; set; }
        public String uso_antibiotico { get; set; }
        public String cuales_antibioticos { get; set; }
        public int id_ref_requiere_inclusion { get; set; }
        public String otro_requiere_inclusion { get; set; }
        public String efectividad_inclusion { get; set; }
        public String cie10_4 { get; set; }
        public String descripcion_cie10_1_egreso { get; set; }
        public String cie10_5 { get; set; }
        public String descripcion_cie10_2_egreso{ get; set; }
        public int id_ref_destino_egreso { get; set; }
        public String otro_destino_egreso { get; set; }
        public String observaciones { get; set; }
        public String auditor_medico_concurrente { get; set; }
        public DateTime? fecha_ingreso { get; set; }
        public String usuario_ingreso { get; set; }

        #endregion

        #region FUNCIONES

        public List<vw_ffmm_departamento> FFMM_departamento()
        {
            return BusClass.FFMM_departamento();

        }
        public Int32 InsertarFFMMAuditoria(ffmm_auditoria OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFFMMAuditoria(OBJ, ref MsgRes);
        }

        public List<Ref_tipo_documental> GetTipoDocumnetal()
        {
            return BusClass.GetTipoDocumnetal();
        }
        public List<Ref_ffmm_tipo_afiliacion> FFMM_tipo_afiliacion()
        {
            return BusClass.FFMM_tipo_afiliacion();
        }
        public List<Ref_ffmm_origen_servicio> FFMM_origen_servicio()
        {
            return BusClass.FFMM_origen_servicio();
        }
        public List<Ref_ffmm_unidad_satelite> GetUnidadSatelite(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetUnidadSatelite(ref MsgRes);
        }
        public List<Ref_ffmm_unidad_cp> GetSitioAdscripcion(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetSitioAdscripcion(ref MsgRes);
        }

        public List<managmentFFMMfacturasRadicadasLoteResult> GetRecepcionFacturasFFMM(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetRecepcionFacturasFFMM(ref MsgRes);
        }
        public List<managmentFFMMfacturasRadicadasdtllResult> GetRecepcionFacturasDTLLFFMM(Int32 id_cargue_base, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetRecepcionFacturasDTLLFFMM(id_cargue_base, ref MsgRes);
        }
        public List<managment_ffmm_documentosResult> GetSoportesListFFMM(int detalle)
        {
            return BusClass.GetSoportesListFFMM(detalle);
        }

        #endregion
    }
}