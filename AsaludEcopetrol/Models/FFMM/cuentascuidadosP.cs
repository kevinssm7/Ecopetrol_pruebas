using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.FFMM
{
    public class cuentascuidadosP
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


        public int id_ffmm_cuidados_paliativos { get; set; }

        public DateTime? fecha_auditoria { get; set; }

        public int id_ref_ffmm_unidad_cp { get; set; }

        public int id_ref_tipo_visita { get; set; }

        public int visita_numero { get; set; }

        public int contratista_servicios_dom_ips { get; set; }

        public String nombre_apellidos { get; set; }

        public int tipoId { get; set; }

        public Decimal numero_id { get; set; }

        public DateTime? fecha_nacimiento { get; set; }

        public int edadN { get; set; }

        public int edadT { get; set; }

        public String grupoEtario { get; set; }

        public String sexo { get; set; }

        public int id_estado_afiliacion { get; set; }

        public String sitio_adcripcion { get; set; }

        public int id_tipo_afiliacion { get; set; }

        public String grado { get; set; }

        public int id_fuerza { get; set; }

        public DateTime fecha_ingreso_al_programa { get; set; }

        public String cumple_criterios { get; set; }

        public String criterio_que_no_cumple { get; set; }

        public String cod_cie10Principal { get; set; }

        public String descripcion_cie10_principal { get; set; }

        public String codigo_cie10_secun { get; set; }

        public String descripcion_cie10_secun { get; set; }

        public DateTime? fecha_ultima_valoracion { get; set; }

        public Int32 cantidad_terapia_fisica { get; set; }

        public String periodidad_terapia_fisica { get; set; }

        public String pertinencia_medica_terapia_fisica { get; set; }

        public int cantidad_terapia_lenguaje { get; set; }

        public String periodidad_terapia_lenguaje { get; set; }

        public String pertinencia_terapia_lenguaje { get; set; }

        public int cantidad_terapia_ocupacional { get; set; }

        public String periodidad_terapia_ocupacional { get; set; }

        public String pertinencia_terapia_ocupacional { get; set; }

        public int cantidad_terapia_respiratoria { get; set; }

        public String periodidad_terapia_respiratoria { get; set; }

        public String pertinencia_terapia_respiratoria { get; set; }

        public int cantidad_psicologia { get; set; }

        public String periodidad_psicologica { get; set; }

        public String pertinencia_medica_psicologia { get; set; }

        public int cantidad_clinica_heridas { get; set; }

        public String periodidad_clinica_heridas { get; set; }

        public String pertinencia_clinica_heridas { get; set; }

        public String servicio_enfermeria { get; set; }

        public String cantidas_horas_periocidad_servicio_enfermeria { get; set; }

        public String pertinencia_servicio_de_enfermeria { get; set; }

        public String servicio_cuidador { get; set; }

        public String cantidad_horas_periodidad_cuidador { get; set; }

        public String pertinencia_cuidador { get; set; }

        public String otros_servicios { get; set; }

        public String escala_discapacidad { get; set; }

        public String medicion_segun_escala_discapacidad { get; set; }

        public String plan_de_manejo { get; set; }

        public String objetivos_terapeuticos { get; set; }

        public int id_percepcion_prestacion_servicio { get; set; }

        public String observaciones { get; set; }

        public String tutela { get; set; }

        public DateTime? detalle_tutela { get; set; }

        public String concepto_auditoria_med { get; set; }

        public String auditor_medico_concurrente { get; set; }

        public Int32 cod_dane_municipio { get; set; }

        public Int32 cod_dane_departamento { get; set; }



        private List<Ref_ffmm_unudadR> _FFMM_unudadR;
        public List<Ref_ffmm_unudadR> FFMM_unudadR
        {
            get
            {
                if (_FFMM_unudadR == null)
                {
                    return _FFMM_unudadR = BusClass.FFMM_unudadR();
                }
                else
                {
                    return _FFMM_unudadR;
                }

            }

            set
            {
                _FFMM_unudadR = value;
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
                    return _FFMM_Municipio = BusClass.FFMM_municipio(); ;
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

        private List<Ref_ffmm_unidad_cp> _List_FFMM_Unidad_CP;
        public List<Ref_ffmm_unidad_cp> List_FFMM_Unidad_CP
        {
            get
            {
                if (_List_FFMM_Unidad_CP == null)
                {
                    return _List_FFMM_Unidad_CP = BusClass.FFMM_Unidad_CP();
                }
                else
                {
                    return _List_FFMM_Unidad_CP;
                }

            }

            set
            {
                _List_FFMM_Unidad_CP = value;
            }
        }

        private List<Ref_ffmm_tipo_visita_cp> _ListFFMM_TipoV_CP;
        public List<Ref_ffmm_tipo_visita_cp> ListFFMM_TipoV_CP
        {
            get
            {
                if (_ListFFMM_TipoV_CP == null)
                {
                    return _ListFFMM_TipoV_CP = BusClass.FFMM_TipoV_CP();
                }
                else
                {
                    return _ListFFMM_TipoV_CP;
                }

            }

            set
            {
                _ListFFMM_TipoV_CP = value;
            }
        }

        #endregion

        #region FUNCIONES

        public Int32 InsertarFFMMref_paliativos(ffmm_cuidados_paliativos OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFFMMref_paliativos(OBJ, ref MsgRes);
        }


        //METODOS DAVID


        public List<vw_ffmm_departamento> GetDepartamentos()
        {
            return BusClass.GetDepartamentos(ref MsgRes);
        }

        public List<vw_ffmm_municipio> GetMunicipios(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetMunicipios(ref MsgRes);
        }


        #endregion
    }
}