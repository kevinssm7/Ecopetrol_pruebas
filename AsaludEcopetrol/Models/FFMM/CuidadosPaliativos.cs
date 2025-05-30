using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.FFMM
{
    public class CuidadosPaliativos
    {

        #region CONEXIONES 
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

        #region ATRIBUTOS 

        public int id_ffmm_cuidados_paliativos { get; set; }
        public DateTime fecha_auditoria { get; set; }
        public int id_ref_ffmm_unidad_cp { get; set; }
        public int id_ref_tipo_visita { get; set; }
        public int visita_numero { get; set; }
        public int contratista_servicios_dom_ips { get; set; }
        public string nombre_apellidos { get; set; }
        public string tipoId { get; set; }
        public decimal numero_id { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public int edadN { get; set; }
        public int edadT { get; set; }
        public string grupoEtario { get; set; }
        public int sexo { get; set; }
        public int id_estado_afiliacion { get; set; }
        public int id_sitio_adcripcion { get; set; }
        public int id_tipo_afiliacion { get; set; }
        public string grado { get; set; }
        public int id_fuerza { get; set; }
        public DateTime fecha_ingreso_al_programa { get; set; }
        public string cumple_criterios { get; set; }
        public string criterio_que_no_cumple { get; set; }
        public string cod_cie10Principal { get; set; }
        public string descripcion_cie10_principal { get; set; }
        public string codigo_cie10_secun { get; set; }
        public string descripcion_cie10_secun { get; set; }
        public DateTime fecha_ultima_valoracion { get; set; }
        public int cantidad_terapia_fisica { get; set; }
        public string periodidad_terapia_fisica { get; set; }
        public string pertinencia_medica_terapia_fisica { get; set; }
        public int cantidad_terapia_lenguaje { get; set; }
        public string periodidad_terapia_lenguaje { get; set; }
        public string pertinencia_terapia_lenguaje { get; set; }
        public int cantidad_terapia_ocupacional { get; set; }
        public string periodidad_terapia_ocupacional { get; set; }
        public string pertinencia_terapia_ocupacional { get; set; }
        public int cantidad_terapia_respiratoria { get; set; }
        public string periodidad_terapia_respiratoria { get; set; }
        public string pertinencia_terapia_respiratoria { get; set; }
        public int cantidad_psicologia { get; set; }
        public string periodidad_psicologica { get; set; }
        public string pertinencia_medica_psicologia { get; set; }
        public int cantidad_clinica_heridas { get; set; }
        public string periodidad_clinica_heridas { get; set; }
        public string pertinencia_clinica_heridas { get; set; }
        public string servicio_enfermeria { get; set; }
        public string cantidas_horas_periocidad_servicio_enfermeria { get; set; }
        public string periocidad_servicio_enfermeria { get; set; }
        public string pertinencia_servicio_de_enfermeria { get; set; }
        public string servicio_cuidador { get; set; }
        public string cantidad_horas_periodidad_cuidador { get; set; }
        public string pertinencia_cuidador { get; set; }
        public string otros_servicios { get; set; }
        public string escala_discapacidad { get; set; }
        public string medicion_segun_escala_discapacidad { get; set; }
        public string plan_de_manejo { get; set; }
        public string objetivos_terapeuticos { get; set; }
        public string percepcion_prestacion_servicio { get; set; }
        public string observaciones { get; set; }
        public string tutela { get; set; }
        public string concepto_auditoria_med { get; set; }
        public string auditor_medico_concurrente { get; set; }
        public string cobertura_fallo { get; set; }
        public string nombre_juzgado { get; set; }
        public DateTime  fecha_tutela { get; set; }


        public string telefono_usuario { get; set; }
        public string celular_usuario  { get; set; }
        public string direccion_usuario { get; set; }
        public string barrio { get; set; }

        public string departamento_usuario { get; set; }
        public string municipio_usuario { get; set; }
        public int cod_municipio_usuario { get; set; }
        public int cod_departamento_usuario { get; set; }

        public string id_cie10_1 { get; set; }




        #endregion

        #region FUNCIONES Y MÉTODOS



        public int SaveCuidadosPaliativos(ffmm_cuidados_paliativos objeto, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.SaveCuidadosPaliativos(objeto, ref MsgRes);
        }



        public List<Ref_ffmm_unidad_cp> GetUnidad(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetUnidad(ref MsgRes);
        }


        public List<Ref_ffmm_unidad_satelite> GetUnidadSatelite(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetUnidadSatelite(ref MsgRes);
        }


        public List<Ref_ffmm_tipo_visita_cp> GetTipoVisita(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetTipoVisita(ref MsgRes);
        }
        public List<vw_ffmm_departamento> GetDepartamentos(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetDepartamentos(ref MsgRes);
        }
        public List<vw_ffmm_municipio> GetMunicipios(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetMunicipios(ref MsgRes);
        }
        public List<vw_ffmm_ips> GetIPS(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetIPS(ref MsgRes);
        }
        public List<Ref_tipo_documental> GetTipoIdentificacion(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetTipoIdentificacion(ref MsgRes);
        }
        public List<Ref_ffmm_sexo> GetSexo(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetSexo(ref MsgRes);
        }

        public List<Ref_ffmm_unidad_cp> GetSitioAdscripcion(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetSitioAdscripcion(ref MsgRes);
        }
        public List<Ref_ffmm_tipo_afiliacion> GetTipoAfiliacion(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetTipoAfiliacion(ref MsgRes);
        }

        public List<Ref_ffmm_estado> GetEstado(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetEstado(ref MsgRes);
        }
        public List<Ref_ffmm_fuerza> GetFuerza(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFuerza(ref MsgRes);
        }
        public List<Ref_ffmm_sino> GetSiNo(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetSiNo(ref MsgRes);
        }
        public List<vw_cie10> GetCie10(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetCie10(ref MsgRes);
        }

    



        #endregion
    }
}