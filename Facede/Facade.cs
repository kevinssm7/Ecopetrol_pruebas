using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA_ACCESS;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using ANALITICA_COMMON.ENTIDADES;
using System.Data.SqlClient;
using System.Data;

namespace Facede
{
    public class Facade
    {
        #region  PROPIEDADES

        private ConsultasDac _DACConsulta;
        public ConsultasDac DACConsulta
        {
            get
            {
                if (_DACConsulta != null)
                {
                    return _DACConsulta;
                }
                else
                {
                    return _DACConsulta = new ConsultasDac();
                }

            }
            set { _DACConsulta = value; }
        }

        private ActualizarDac _DACActualiza;
        public ActualizarDac DACActualiza
        {
            get
            {
                if (_DACActualiza != null)
                {
                    return _DACActualiza;
                }
                else
                {
                    return _DACActualiza = new ActualizarDac();
                }

            }
            set { _DACActualiza = value; }
        }

        private ComonClass _DACComonClass;
        public ComonClass DACComonClass
        {
            get
            {
                if (_DACComonClass != null)
                {
                    return _DACComonClass;
                }
                else
                {
                    return _DACComonClass = new ComonClass();
                }

            }
            set { _DACComonClass = value; }
        }

        private InsertaDac _DACInserta;
        public InsertaDac DACInserta
        {
            get
            {
                if (_DACInserta != null)
                {
                    return _DACInserta;
                }
                else
                {
                    return _DACInserta = new InsertaDac();
                }

            }
            set { _DACInserta = value; }
        }

        private EliminaDac _DACElimina;
        public EliminaDac DACElimina
        {
            get
            {
                if (_DACElimina != null)
                {
                    return _DACElimina;
                }
                else
                {
                    return _DACElimina = new EliminaDac();
                }

            }
            set { _DACElimina = value; }
        }

        #endregion

        #region COMMONCLASS
        public List<sis_usuario> GetSisUsuario()
        {
            return DACComonClass.GetSisUsuario();
        }

        public List<sis_usuario> GetSisUsuarioactivo()
        {
            return DACComonClass.GetSisUsuarioactivo();
        }

        public List<sis_usuario> GetSisUsuarioMd()
        {
            return DACComonClass.GetSisUsuarioMd();
        }

        public List<sis_usuario> GetSisUsuarioOdont()
        {
            return DACComonClass.GetSisUsuarioOdont();
        }

        public void ActualizaCodigoIngreso(string usuario, string codigo, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaCodigoIngreso(usuario, codigo, ref MsgRes);
        }

        public List<Ref_tipo_documental> GetTipoDocumnetal()
        {
            return DACComonClass.GetTipoDocumnetal();

        }

        public List<vw_ips_ciudad> GetIPS()
        {
            return DACComonClass.GetIPS();
        }

        public List<Ref_ips> GetPrstador()
        {
            return DACComonClass.GetPrstador();
        }

        public List<management_censo_tableroDetalladoResult> GetCensoDetallado(DateTime? fechaInicio, DateTime? fechaFin, string documento)
        {
            return DACConsulta.GetCensoDetallado(fechaInicio, fechaFin, documento);
        }

        public List<Ref_ips_cuentas> GetPrstadorCuentas()
        {
            return DACComonClass.GetPrstadorCuentas();
        }
        public List<vw_ciudad_auditor> GetCiudad_auditor()
        {
            return DACComonClass.GetCiudad_auditor();
        }

        public List<vw_auditor> Get_auditor()
        {
            return DACComonClass.Get_auditor();
        }
        public List<Ref_origen_evento> GetOrigenEvento()
        {
            return DACComonClass.GetOrigenEvento();
        }


        public List<Ref_regional> GetRefRegion()
        {
            return DACComonClass.GetRefRegion();
        }

        public Ref_regional GetRefRegionId(int id)
        {
            return DACComonClass.GetRefRegionId(id);
        }

        public List<Ref_regional> listadoRegionalesIndice(string indice)
        {
            return DACConsulta.listadoRegionalesIndice(indice);
        }

        public List<Ref_tipo_habitacion> GetTipoHabitacion()
        {
            return DACComonClass.GetTipoHabitacion();
        }

        public List<Ref_tipo_habitacion> ListadoTipoGabitacion()
        {
            return DACConsulta.ListadoTipoGabitacion();
        }

        public List<Ref_tipo_ingreso> GetTipoIngreso()
        {
            return DACComonClass.GetTipoIngreso();
        }

        public List<Ref_condicion_alta_censo> GetCondicionAlta()
        {
            return DACComonClass.GetCondicionAlta();
        }

        public List<Ref_cie10> GetCie10()
        {
            return DACComonClass.GetCie10();
        }

        public List<vw_ref_cups> GetCups()
        {
            return DACComonClass.GetCups();
        }

        public List<Ref_cuentas_glosa> GetCuentaGlosa()
        {
            return DACComonClass.GetCuentaGlosa();
        }

        public List<Ref_causal_glosa> GetCausalGlosa()
        {
            return DACComonClass.GetCausalGlosa();
        }

        public List<Ref_responsable_glosa> GetResGlosa()
        {
            return DACComonClass.GetResGlosa();
        }

        public List<Ref_condicion_del_egreso> GetCondicionEgreso()
        {
            return DACComonClass.GetCondicionEgreso();
        }

        public List<Ref_servicio_tratante> GetServiciotratante()
        {
            return DACComonClass.GetServiciotratante();
        }

        public List<Ref_salud_publica> GetSaludPublica()
        {
            return DACComonClass.GetSaludPublica();
        }


        public List<Ref_lesiones_severas_y_alto_costo> GetAltoCosto()
        {
            return DACComonClass.GetAltoCosto();
        }

        public List<vw_tablero_censo> GetTableroCenso()
        {
            return DACComonClass.GetTableroCenso();
        }

        public List<management_vw_tablero_censoResult> GetTableroCensoCompleto()
        {
            return DACComonClass.GetTableroCensoCompleto();
        }

        public List<vw_tablero_censo2> GetTableroCenso2()
        {
            return DACComonClass.GetTableroCenso2();
        }

        public List<vw_tablero_concurrencia> GetTableroConcurrencia()
        {
            return DACComonClass.GetTableroConcurrencia();
        }

        public List<management_egresosEvolucionesResult> ConsultaEgresoId(int idEgreso)
        {
            return DACConsulta.ConsultaEgresoId(idEgreso);
        }

        public List<management_concurrencia_alertasResult> ConsultaConcurrenciaAlertasEvolucion()
        {
            return DACConsulta.ConsultaConcurrenciaAlertasEvolucion();
        }

        public List<management_concurrencia_alerta_ReporteResult> ConsultaConcurrenciaAlertasEvolucionReporte()
        {
            return DACConsulta.ConsultaConcurrenciaAlertasEvolucionReporte();
        }
        public int ConsultaConcurrenciaAlertasEvolucionConteo()
        {
            return DACConsulta.ConsultaConcurrenciaAlertasEvolucionConteo();
        }

        public List<Ref_ciudades> GetCiudades()
        {
            return DACComonClass.GetCiudades();
        }

        public List<ref_fis_municipios> TraerMunicipiosFis(int? idDepartamento)
        {
            return DACConsulta.TraerMunicipiosFis(idDepartamento);
        }


        public List<Ref_odont_unis> unisRegional(int? idRegional)
        {
            return DACConsulta.unisRegional(idRegional);
        }

        public List<Ref_ciudades> GetCiudadesXRegional(int? idRegional)
        {
            return DACComonClass.GetCiudadesXRegional(idRegional);
        }

        public List<Ref_ciudades> GetCiudadesXUnis(int? idUnis)
        {
            return DACComonClass.GetCiudadesXUnis(idUnis);
        }

        public List<vw_cie10> GetCie10Unido()
        {
            return DACComonClass.GetCie10Unido();
        }
        public List<vw_cie10> GetCie10UnidoDetalle()
        {
            return DACComonClass.GetCie10UnidoDetalle();

        }

        public List<Ref_causal_egreso> GetCausaEgreso()
        {
            return DACComonClass.GetCausaEgreso();
        }
        public List<vw_consulta_alertas> GetConsultaAlertas()
        {
            return DACComonClass.GetConsultaAlertas();
        }

        public List<Total_ciudades> GetTotalCiudades()
        {
            return DACComonClass.GetTotalCiudades();
        }

        public List<Ref_ciudades> TotalCiudadesXRegional(int? regional)
        {
            return DACConsulta.TotalCiudadesXRegional(regional);
        }
        public List<Ref_motivo_devolucion_fac> GetMotivoDevolucionFac()
        {
            return DACComonClass.GetMotivoDevolucionFac();
        }

        public List<vw_sis_auditor_ciudad> GetCiudadesAuditor()
        {
            return DACComonClass.GetCiudadesAuditor();
        }

        public List<sis_auditor_regional> GetRegionalAuditor()
        {
            return DACComonClass.GetRegionalAuditor();
        }
        public List<sis_auditor_regional> listadoRegionalesUsuario(int idUsuario)
        {
            return DACConsulta.listadoRegionalesUsuario(idUsuario);
        }

        public List<sis_auditor_regional_pqrs> listadoRegionalesUsuarioPqrs(int idUsuario)
        {
            return DACConsulta.listadoRegionalesUsuarioPqrs(idUsuario);
        }

        public List<sis_auditor_regional_pqrs> listadoCoordinadoresPqrs()
        {
            return DACConsulta.listadoCoordinadoresPqrs();
        }

        public sis_auditor_regional GetRegionalAuditor(int? idUsuario)
        {
            return DACConsulta.GetRegionalAuditor(idUsuario);
        }

        public List<vw_sis_auditor_regional> GetVWRegionalAuditor()
        {
            return DACComonClass.GetVWRegionalAuditor();
        }

        public List<Ref_hallazgos_RIPS> GetRefHallazgos()
        {
            return DACComonClass.GetRefHallazgos();
        }

        public List<Ref_categorias_eventos_adv> GetRefCategoriaEvad()
        {
            return DACComonClass.GetRefCategoriaEvad();
        }

        public List<Ref_motivo_reingreso> GetRefMotivoRiesgo()
        {
            return DACComonClass.GetRefMotivoRiesgo();
        }

        public List<Ref_categorias_situaciones_de_calidad> GetRefCategoriaSituacion()
        {
            return DACComonClass.GetRefCategoriaSituacion();
        }

        public List<vw_cie10_alertas> GetRefcie10Alertas()
        {
            return DACComonClass.GetRefcie10Alertas();
        }
        public List<vw_cie10_alertas_detalle> GetRefcie10AlertasNuevo()
        {
            return DACComonClass.GetRefcie10AlertasNuevo();
        }

        public List<Ref_enfermedades_Huerfanas> GetRefHuerfanas()
        {
            return DACComonClass.GetRefHuerfanas();
        }

        public List<Ref_tipo_ahorro> GetRefTipoAhorro()
        {
            return DACComonClass.GetRefTipoAhorro();
        }


        public List<Ref_PQRS_Atributo> GetRefPqrsAtributo()
        {
            return DACComonClass.GetRefPqrsAtributo(); ;
        }


        public List<Ref_PQRS_estado_Gestion> GetRefPqrsGestion()
        {
            return DACComonClass.GetRefPqrsGestion();
        }


        public List<Ref_PQRS_llamada> GetRefPqrsLLamada()
        {
            return DACComonClass.GetRefPqrsLLamada();
        }

        public List<Ref_PQRS_Subtematica> GetRefPqrsSubtematica()
        {
            return DACComonClass.GetRefPqrsSubtematica();
        }

        public List<Ref_PQRS_tipo_solicitud> GetRefPqrsSolicitud()
        {
            return DACComonClass.GetRefPqrsSolicitud();
        }

        public List<vw_PQRS_usuarios> GetRefPqrsUsuarios()
        {
            return DACComonClass.GetRefPqrsUsuarios();
        }

        public void EliminarPQRSEnrevision(int id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarPQRSEnrevision(id_ecop_PQRS, ref MsgRes);
        }
        public List<vw_md_crono_auditores> GetRefCronoAuditor()
        {
            return DACComonClass.GetRefCronoAuditor();
        }


        public List<Ref_analaisis_caso_ambito> Getambito()
        {
            return DACComonClass.Getambito();
        }

        public List<Ref_eventos_decision> GetEventoDecision()
        {
            return DACComonClass.GetEventoDecision();
        }

        public List<Ref_eventos_habilidad> GetEventosHabilidad()
        {
            return DACComonClass.GetEventosHabilidad();
        }

        public List<Ref_eventos_percepcion> GetEventosPercepcion()
        {
            return DACComonClass.GetEventosPercepcion();
        }


        public List<Ref_eventos_rutinaria> GetEventosRutinaria()
        {
            return DACComonClass.GetEventosRutinaria();
        }

        public List<Ref_eventos_excepcionales> GetEventosexcepcionales()
        {
            return DACComonClass.GetEventosexcepcionales();
        }


        public List<Ref_eventos_paciente> GetEventosPaciente()
        {
            return DACComonClass.GetEventosPaciente();
        }

        public List<Ref_eventos_tarea_tec> GetEventostarea()
        {
            return DACComonClass.GetEventostarea();
        }

        public List<Ref_eventos_equipo> GetEventosEquipoT()
        {
            return DACComonClass.GetEventosEquipoT();
        }


        public List<Ref_eventos_individuo> GetEventosIndividuo()
        {
            return DACComonClass.GetEventosIndividuo();
        }

        public List<Ref_eventos_ambiente_tra> GetEventosambienteTrabajo()
        {
            return DACComonClass.GetEventosambienteTrabajo();
        }

        public List<Ref_eventos_organizacion> GetEventosOrganizacion()
        {
            return DACComonClass.GetEventosOrganizacion();
        }

        public List<Ref_eventos_contexto> GetEventosContexto()
        {
            return DACComonClass.GetEventosContexto();
        }

        public List<Ref_eventos_severidad> GetEventosSeveridad()
        {
            return DACComonClass.GetEventosSeveridad();
        }

        public List<Ref_eventos_repeticion> GetEventosProbavilidadR()
        {
            return DACComonClass.GetEventosProbavilidadR();
        }

        public List<Ref_eventos_tipo_evento> GetEventosTipoEvento()
        {
            return DACComonClass.GetEventosTipoEvento();
        }

        public List<vw_md_ref_glosa> GetMedglosa()
        {
            return DACComonClass.GetMedglosa();
        }

        public List<vw_md_Ref_indicador> GetMDIndicadores()
        {
            return DACComonClass.GetMDIndicadores();
        }

        public List<ref_meses_del_año> meses()
        {
            return DACComonClass.meses();
        }

        public List<ref_tipo_cohorte> tipoCohortes()
        {
            return DACComonClass.tipoCohortes();
        }

        public List<Ref_origen_hospitalizacion> GetOrigenHospitalizacion()
        {
            return DACComonClass.GetOrigenHospitalizacion();
        }

        public List<vw_ref_enfermedades_huerfanas> GetEnfermedadesHuerfanas()
        {
            return DACComonClass.GetEnfermedadesHuerfanas();
        }

        public List<vw_evo_ecop_concurrencia> ConsultaIdConcurreniaEvo(vw_evo_ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdConcurreniaEvo(ObjAfiliado, ref MsgRes);
        }

        public List<ecop_concurrencia_evolucion> ConsultaNumeroEvoluciones(ecop_concurrencia_evolucion ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaNumeroEvoluciones(ObjAfiliado, ref MsgRes);
        }

        public List<Ref_rol_cargo> RolCargo()
        {
            return DACComonClass.RolCargo();
        }

        public List<Ref_odont_unis> Odont_unis()
        {
            return DACComonClass.Odont_unis();
        }

        public List<Ref_odont_unis> Odont_unisIdRegional(int? idRegional)
        {
            return DACComonClass.Odont_unisIdRegional(idRegional);
        }
        public Ref_odont_unis traerUnisId(int? idUnis)
        {
            return DACComonClass.traerUnisId(idUnis);
        }

        public List<ref_odontologia_protesisfija_dientes> OdontoProtesisFijaDientes(int? tipo)
        {
            return DACConsulta.OdontoProtesisFijaDientes(tipo);
        }

        public List<ref_odontologia_protesisfija_dientes> OdontoProtesisFijaDientesTotal()
        {
            return DACConsulta.OdontoProtesisFijaDientesTotal();
        }

        public ref_odontologia_protesisfija_dientes TraerDienteId(int? id)
        {
            return DACConsulta.TraerDienteId(id);
        }

        public List<Ref_si_no> Ref_sino()
        {
            return DACComonClass.Ref_sino();

        }

        public List<vw_ref_regiona_ciudad> Ref_regional_ciudad()
        {
            return DACComonClass.Ref_regional_ciudad();
        }
        public List<management_regional_ciudadResult> Ref_regional_ciudad_detallado()
        {
            return DACComonClass.Ref_regional_ciudad_detallado();

        }

        public List<management_ref_regiona_ciudadResult> ListadoPrestadoresRegionalCiudad(int? regional, int? ciudad)
        {
            return DACComonClass.ListadoPrestadoresRegionalCiudad(regional, ciudad);
        }


        public List<Ref_plan_mejora_estado_tarea> estadoTarea()
        {
            return DACComonClass.estadoTarea();
        }

        public List<ManagementPrestadoresAlertasFechaResult> ManagmentAlertas(ref MessageResponseOBJ MsgRes)
        {
            return DACComonClass.ManagmentAlertas(ref MsgRes);
        }

        public List<ref_consulta_ecopetrol> Ref_ConsultasEcopetrol()
        {
            return DACComonClass.Ref_ConsultasEcopetrol();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 24 de enero de 2023
        /// Metodo para insertar una actividad reciente en la base de datos
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void InsertarActividadReciente(sis_actividad_reciente obj, ref MessageResponseOBJ MsgRes)
        {
            DACComonClass.InsertarActividadReciente(obj, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 24 de enero de 2023
        /// Obtener el listado de las actividades recientes agregadas
        /// </summary>
        /// <returns></returns>
        public List<Management_sis_actividad_recienteResult> GetListActividadReciente()
        {
            return DACComonClass.GetListActividadReciente();
        }

        #region FFMM

        public List<Ref_ffmm_glosas> FFMM_glosas()
        {
            return DACComonClass.FFMM_glosas();
        }

        public List<Ref_ffmm_alto_costo> FFMM_altocosto()
        {
            return DACComonClass.FFMM_altocosto();
        }

        public List<Ref_ffmm_especialidad_remite> FFMM_especialidad_remite()
        {
            return DACComonClass.FFMM_especialidad_remite();
        }

        public List<Ref_ffmm_especialidad_remitio> FFMM_especialidad_remitio()
        {
            return DACComonClass.FFMM_especialidad_remitio();
        }

        public List<Ref_ffmm_estado> FFMM_estado()
        {
            return DACComonClass.FFMM_estado();
        }

        public List<Ref_ffmm_fuerza> FFMM_fuerza()
        {
            return DACComonClass.FFMM_fuerza();
        }

        public List<Ref_ffmm_imagen_proc> FFMM_imagen_proc()
        {
            return DACComonClass.FFMM_imagen_proc();
        }

        public List<Ref_ffmm_modalidad_pago> FFMM_modalidad_pago()
        {
            return DACComonClass.FFMM_modalidad_pago();
        }
        public List<Ref_ffmm_nivel_atencion> FFMM_nivel_atencion()
        {
            return DACComonClass.FFMM_nivel_atencion();
        }

        public List<Ref_ffmm_nivel_complejidad> FFMM_nivel_complejidad()
        {
            return DACComonClass.FFMM_nivel_complejidad();
        }

        public List<Ref_ffmm_origen_servicio> FFMM_origen_servicio()
        {
            return DACComonClass.FFMM_origen_servicio();
        }

        public List<Ref_ffmm_prestadores> FFMM_prestadores()
        {
            return DACComonClass.FFMM_prestadores();
        }

        public List<vw_ffmm_ips> FFMM_prestadores_vw()
        {
            return DACComonClass.FFMM_prestadores_vw();
        }
        public List<Ref_ffmm_proceso> FFMM_proceso()
        {
            return DACComonClass.FFMM_proceso();
        }

        public List<Ref_ffmm_servicio> FFMM_servicio()
        {
            return DACComonClass.FFMM_servicio();
        }

        public List<Ref_ffmm_sexo> FFMM_sexo()
        {
            return DACComonClass.FFMM_sexo();
        }

        public List<Ref_ffmm_sino> FFMM_sino()
        {
            return DACComonClass.FFMM_sino();

        }

        public List<Ref_ffmm_tipo_afiliacion> FFMM_tipo_afiliacion()
        {
            return DACComonClass.FFMM_tipo_afiliacion();

        }

        public List<Ref_ffmm_tipo_servicio> FFMM_tipo_servicio()
        {
            return DACComonClass.FFMM_tipo_servicio();

        }

        public List<Ref_ffmm_unidad_satelite> FFMM_unidad_satelite()
        {
            return DACComonClass.FFMM_unidad_satelite();

        }

        public List<Ref_ffmm_unudadR> FFMM_unudadR()
        {
            return DACComonClass.FFMM_unudadR();
        }


        public List<vw_ffmm_departamento> FFMM_departamento()
        {
            return DACComonClass.FFMM_departamento();
        }

        public List<vw_ffmm_municipio> FFMM_municipio()
        {
            return DACComonClass.FFMM_municipio();
        }

        public List<vw_FMM_RefGeneral> FFMM_Reg_General()
        {
            return DACComonClass.FFMM_Reg_General();
        }

        public List<Ref_ffmm_prestadores_proveedor> FFMM_prestadores_Proveedor()
        {
            return DACComonClass.FFMM_prestadores_Proveedor();
        }

        public List<Ref_ffmm_tipo_proveedor> FFMM_tipo_proveedor()
        {
            return DACComonClass.FFMM_tipo_proveedor();
        }

        public List<Ref_ffmm_glosas_respuesta> FFMM_respuestas_glosas()
        {
            return DACComonClass.FFMM_respuestas_glosas();
        }

        public List<Ref_ffmm_unidad_cp> FFMM_Unidad_CP()
        {
            return DACComonClass.FFMM_Unidad_CP();
        }
        public List<Ref_ffmm_tipo_visita_cp> FFMM_TipoV_CP()
        {
            return DACComonClass.FFMM_TipoV_CP();
        }

        public Int32 InsertarFFMMAuditoria(ffmm_auditoria OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFFMMAuditoria(OBJ, ref MsgRes);
        }

        public List<ref_ffmm_ips_prestadores_tipo> tipoIpsPrestador()
        {
            return DACComonClass.tipoIpsPrestador();
        }

        public List<management_traerIpsoPrestadoresResult> traerPrestadoresId(int tipo, int nit)
        {
            return DACConsulta.traerPrestadoresId(tipo, nit);
        }
        public int InsertarIpsPrestador(ref_ffmm_ips_prestadores obj)
        {
            return DACInserta.InsertarIpsPrestador(obj);
        }
        public List<ref_ffmm_ips_prestadores> existenciaIpsPrestadores(int nit)
        {
            return DACConsulta.existenciaIpsPrestadores(nit);
        }
        public List<ref_ffmm_ips_prestadores> existenciaIpsPrestadoresNombre(String nombre)
        {
            return DACConsulta.existenciaIpsPrestadoresNombre(nombre);
        }
        public int ActualizarIpsPrestadores(ref_ffmm_ips_prestadores obj)
        {
            return DACActualiza.ActualizarIpsPrestadores(obj);
        }

        public List<ref_ffmm_ips_prestadores> ObtenerIpsPrestador(int idCiudad, int tipo)
        {
            return DACConsulta.ObtenerIpsPrestador(idCiudad, tipo);
        }

        public List<ref_ffmm_ips_prestadores> ObtenerIpsPrestadorSinTipo(int idCiudad)
        {
            return DACConsulta.ObtenerIpsPrestadorSinTipo(idCiudad);
        }
        public List<ref_ffmm_ips_prestadores> ObtenerIpsPrestadorCompleto()
        {
            return DACConsulta.ObtenerIpsPrestadorCompleto();
        }
        public List<management_contratos_prestadoresResult> ObtenerIpsPrestadorTablero()
        {
            return DACConsulta.ObtenerIpsPrestadorTablero();
        }


        public List<managmentFFMMfacturasRadicadasLoteResult> GetRecepcionFacturasFFMM(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetRecepcionFacturasFFMM(ref MsgRes);
        }

        public List<Management_FFMM_Glosas_PrestadoresResult> GetFFMMGlosasPrestadores(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFFMMGlosasPrestadores(ref MsgRes);
        }
        public List<managmentFFMMfacturasRadicadasdtllResult> GetRecepcionFacturasDTLLFFMM(Int32 id_cargue_base, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetRecepcionFacturasDTLLFFMM(id_cargue_base, ref MsgRes);
        }

        public List<managmentFFMMfacturasRadicadasid_dtllResult> GetRecepcionFacturasDTLLFFMMId(Int32 id_cargue_dtll, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetRecepcionFacturasDTLLFFMMId(id_cargue_dtll, ref MsgRes).ToList();
        }

        public List<Management_actualizar_FacturaDigResult> ActualizarFFMMFacturas(Int32 Id_factura, Int32 estado, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarFFMMFacturas(Id_factura, estado, ref MsgRes);
        }


        public List<Management_FFMM_Consultas_cuentasResult> GetConsultaFFMMCuentas(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMCuentas(ref MsgRes);

        }

        public List<Management_FFMM_Consultas_ConcurreniaResult> GetConsultaFFMMConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMConcurrencia(ref MsgRes);
        }

        public List<Management_FFMM_Consultas_PADResult> GetConsultaFFMMPad(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMPad(ref MsgRes);
        }

        public List<Management_FFMM_consulta_cuentas_primeraResult> GetConsultaFFMMCuentasUno(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMCuentasUno(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_cuentas_segundaResult> GetConsultaFFMMCuentasDos(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMCuentasDos(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_cuentas_terceraResult> GetConsultaFFMMCuentasTres(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMCuentasTres(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_cuentas_cuartaResult> GetConsultaFFMMCuentasCuatro(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMCuentasCuatro(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_cuentas_quintaResult> GetConsultaFFMMCuentasCinco(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMCuentasCinco(fecha_inicial, fecha_final, ref MsgRes);
        }

        public List<ref_auditor> listAuditor()
        {
            return DACConsulta.listAuditor();
        }


        public List<Management_FFMM_consulta_concurrencia_primeraResult> GetConsultaFFMMConcurrenciaUno(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMConcurrenciaUno(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_concurrencia_segundaResult> GetConsultaFFMMConcurrenciaDos(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMConcurrenciaDos(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_concurrencia_terceroResult> GetConsultaFFMMConcurrenciaTercero(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMConcurrenciaTercero(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_concurrencia_cuartoResult> GetConsultaFFMMConcurrenciaCuarto(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsultaFFMMConcurrenciaCuarto(fecha_inicial, fecha_final, ref MsgRes);
        }

        #endregion

        #endregion

        #region LOGIN

        public Int32 CrearUsuairo(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CrearUsuairo(ObjUsuario, ref MsgRes);
        }

        public List<sis_usuario> ValidaIngreso(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ValidaIngreso(ObjUsuario, ref MsgRes);
        }

        public sis_usuario ValidaIngresoClave(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ValidaIngresoClave(ObjUsuario, ref MsgRes);
        }

        public List<ManagmentMenuResult> ManagmentMenu(String Strusuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ManagmentMenu(Strusuario, ref MsgRes);
        }

        public void ActualizaContraseña(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaContraseña(ObjUsuario, ref MsgRes);
        }
        public void ActualizaContraseñaOlvido(sis_usuario Usuario, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaContraseñaOlvido(Usuario, ref MsgRes);
        }
        public void ActualizaEstadoUsuario(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaEstadoUsuario(ObjUsuario, ref MsgRes);
        }

        public List<sis_usuario> BuscaAuditorUsu(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.BuscaAuditorUsu(strUsuario, ref MsgRes);
        }

        public List<sis_usuario> BuscaAuditorNom(String strNombre, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.BuscaAuditorNom(strNombre, ref MsgRes);
        }

        public void GestionUsuarios(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            DACConsulta.GestionUsuarios(ObjUsuario, ref MsgRes);
        }

        public DateTime ManagmentHora()
        {
            return DACConsulta.ManagmentHora();
        }

        public List<vw_rol_usuarios> Ref_rol_Usuario()
        {
            return DACComonClass.Ref_rol_Usuario();
        }
        public List<vw_cargo_usuario> Ref_cargo_Usuario()
        {
            return DACComonClass.Ref_cargo_Usuario();

        }
        public List<sis_usuario> BuscaAuditorDoc(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.BuscaAuditorDoc(strUsuario, ref MsgRes);
        }
        public List<vw_pruebas_img_usuarios> BuscaAuditorImg(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.BuscaAuditorImg(strUsuario, ref MsgRes);
        }

        public List<sis_usuario> BuscaAuditorUsuSami(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.BuscaAuditorUsuSami(strUsuario, ref MsgRes);
        }

        public List<sis_usuario> GetUsuarios()
        {
            return DACConsulta.GetUsuarios();
        }

        public List<management_sis_usuarios_controlActividadesCensoResult> GetUsuariosCenso()
        {
            return DACConsulta.GetUsuariosCenso();
        }

        public Int32 InsertarLogGestionUusario(log_gestion_usuarios log, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarLogGestionUusario(log, ref MsgRes);
        }


        public void ActualizaClaveUsuario(sis_usuario OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaClaveUsuario(OBJ, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 10 de abril de 2023
        /// Metodo para insertar en base de datos los datos de inicio de sesion de un usuario.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void InsertarLogInicioSesion(Log_InicioSession obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarLogInicioSesion(obj, ref MsgRes);
        }

        #endregion

        #region CENSO

        public List<vw_datos_censo> CensoDocumento(String Documento, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CensoDocumento(Documento, ref MsgRes);
        }
        public ecop_censo ConsultaCensoIdentificacionPac(string idPaciente)
        {
            return DACConsulta.ConsultaCensoIdentificacionPac(idPaciente);
        }
        public management_datosPaciente_alertasResult DatosPaciente(int idConcurrencia)
        {
            return DACConsulta.DatosPaciente(idConcurrencia);
        }
        public List<vw_datos_censo> CensoFacturas(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CensoFacturas(ref MsgRes);
        }

        public List<management_datos_censoResult> CensoFacturasDetallado(string documento, string nombre, DateTime? fechaEgresoConcu, DateTime? fechaEgresoCenso, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CensoFacturasDetallado(documento, nombre, fechaEgresoConcu, fechaEgresoCenso, ref MsgRes);
        }

        public Int32 InsertarCenso(ecop_censo OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCenso(OBJ, ref MsgRes);
        }

        public List<vw_datos_censo> CensoId(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CensoId(Id, ref MsgRes);
        }

        public List<ecop_censo> GetCensoId(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetCensoId(Id, ref MsgRes);
        }

        public void ActualizarCenso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarCenso(ActualizaSiniestro, ref MsgRes);
        }

        public ecop_censo ConsultaCensoIdCenso(int? idCenso)
        {
            return DACConsulta.ConsultaCensoIdCenso(idCenso);
        }

        public List<vw_censo_control_concurrencia> CensoConcurrenciasTotales()
        {
            return DACConsulta.CensoConcurrenciasTotales();
        }

        public List<management_censo_control_concurrencia_optimizadaResult> CensoConcurrenciasTotalesOptimizada(int? regional, string documento, string nombre)
        {
            return DACConsulta.CensoConcurrenciasTotalesOptimizada(regional, documento, nombre);
        }

        public List<vw_censo_control_cuentasMedicas> CensoCuentasMedicasTotales()
        {
            return DACConsulta.CensoCuentasMedicasTotales();
        }
        public List<management_censo_control_cuentasMedicas_optimizadaResult> CensoCuentasMedicasTotalesOptimizada(int? regional, string documento, string nombre)
        {
            return DACConsulta.CensoCuentasMedicasTotalesOptimizada(regional, documento, nombre);
        }

        public List<vw_censo_control_visitas> CensoVisitasTotales()
        {
            return DACConsulta.CensoVisitasTotales();
        }

        public List<management_censo_control_visitas_optimizadaResult> CensoVisitasTotalesOptimizada(int? regional, string documento, string nombre)
        {
            return DACConsulta.CensoVisitasTotalesOptimizada(regional, documento, nombre);
        }

        public List<management_sis_usuarios_controlActividadesCenso_optimizadaResult> GetUsuariosCensoOptimizada(int? regional, string documento, string nombre)
        {
            return DACConsulta.GetUsuariosCensoOptimizada(regional, documento, nombre);
        }



        public List<ref_ecop_censo_tiposConsulta> CensoConsultaReportesActividades()
        {
            return DACConsulta.CensoConsultaReportesActividades();
        }

        public Int32 InsertarLogCensoReingreso(log_censo_reingresos OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarLogCensoReingreso(OBJ, ref MsgRes);
        }

        public void ActualizarFechaEgresoCenso(ecop_censo OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarFechaEgresoCenso(OBJ, ref MsgRes);
        }

        public int ActualizarCensoSacarDelTablero(ecop_censo censo)
        {
            return DACActualiza.ActualizarCensoSacarDelTablero(censo);
        }

        public void ActualizarLeEgresoCenso(ecop_censo OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarLeEgresoCenso(OBJ, ref MsgRes);
        }

        public void ActualizarEgresoConcu(ecop_concurrencia OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEgresoConcu(OBJ, ref MsgRes);
        }

        public void ActualizarCensoEgreso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarCensoEgreso(ActualizaSiniestro, ref MsgRes);
        }

        public void ActualizarCensoEgresoOK(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarCensoEgresoOK(ActualizaSiniestro, ref MsgRes);
        }

        public void ActualizaEgresoConcurrenciaOk(ecop_concurrencia ObjConcurrencia, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaEgresoConcurrenciaOk(ObjConcurrencia, ref MsgRes);
        }

        public List<VW_base_beneficiarios> BeneficiariosDocumento(String Documento, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.BeneficiariosDocumento(Documento, ref MsgRes);
        }

        public List<vw_tablero_levante_egreso> GetlevanteEgreso(String Documento, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetlevanteEgreso(Documento, ref MsgRes);
        }


        public List<VW_base_beneficiarios> GetBeneficiarios(string term, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetBeneficiarios(term, ref MsgRes);
        }

        public List<base_beneficiarios> GetUltimoBeneficiarios(string documento, string tipo, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetUltimoBeneficiarios(documento, tipo, ref MsgRes);
        }


        //public List<management_baseBeneficiarios_tableroControlResult> TraerListadoBaseBeneficiarios()
        //{
        //    return DACConsulta.TraerListadoBaseBeneficiarios();
        //}

        //public List<management_baseBeneficiarios_consolidadoDetallePeriodoResult> TraerListadoBaseBeneficiariosConsolidado(string periodo)
        //{
        //    return DACConsulta.TraerListadoBaseBeneficiariosConsolidado(periodo);
        //}

        //public int EliminarBaseBeneficiariosPeriodo(string periodo)
        //{
        //    return DACElimina.EliminarBaseBeneficiariosPeriodo(periodo);
        //}



        public List<vw_consulta_censo> ConsultaCenso(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCenso(ref MsgRes);
        }
        public List<vw_consulta_ecop> ConsultaCensoConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCensoConcurrencia(ref MsgRes);
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de agosto de 2022
        /// Segunda consulta de censo con concurrencia
        /// </summary>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Management_Consulta_EcopResult> ConsultaCensoConcurrenciaII(int tipo, int? regional, string documento, DateTime? fechaInicio, DateTime? fechaFinal, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCensoConcurrenciaII(tipo, regional, documento, fechaInicio, fechaFinal, ref MsgRes);
        }

        public List<Management_Consulta_Ecop2Result> ConsultaCensoConcurrenciaII2(int tipo, int? regional, string documento, DateTime? fechaInicio, DateTime? fechaFinal, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCensoConcurrenciaII2(tipo, regional, documento, fechaInicio, fechaFinal, ref MsgRes);
        }

        public List<vw_consulta_pacientesActivos> ConsultaPacientesActivos()
        {
            return DACConsulta.ConsultaPacientesActivos();
        }

        public void CensoEgreso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.CensoEgreso(ActualizaSiniestro, ref MsgRes);
        }

        public void ActualizarEgresoCenso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEgresoCenso(ActualizaSiniestro, ref MsgRes);
        }

        public void ActualizarFechaEgresoConcucenso(ecop_censo censo, int idconcu, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarFechaEgresoConcucenso(censo, idconcu, ref MsgRes);
        }

        public List<management_egresBuscar_megaResult> BuscarMegaEgreso(string documento)
        {
            return DACConsulta.BuscarMegaEgreso(documento);
        }

        public List<ref_censo_caso_especial> listaCensoCasosEspeciales()
        {
            return DACConsulta.listaCensoCasosEspeciales();
        }


        public List<ref_censo_linea_pago> listaCensoLineasPago()
        {
            return DACConsulta.listaCensoLineasPago();
        }

        public management_censo_ultimaHabitacionResult datosEgreso(int? idCenso)
        {
            return DACConsulta.datosEgreso(idCenso);
        }


        public int InsertarCargueAHCenso(cargue_censo_ah_lote obj, List<cargue_censo_ah_registros> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCargueAHCenso(obj, lista, ref MsgRes);
        }


        public List<management_censo_aseguramientoTableroControlResult> DatosCensoAseguramiento()
        {
            return DACConsulta.DatosCensoAseguramiento();
        }

        public List<management_censo_aseguramientoTableroControl_detallesResult> DatosCensoAseguramiento_detalleId(int? idRegistro)
        {
            return DACConsulta.DatosCensoAseguramiento_detalleId(idRegistro);
        }

        public management_censo_aseguramientoTableroControl_idResult TraerRegistroAH(int? idRegistro)
        {
            return DACConsulta.TraerRegistroAH(idRegistro);
        }


        public int InsertarDetalleRegistroAH(cargue_censo_ah_registros_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDetalleRegistroAH(OBJ, ref MsgRes);
        }

        public int EliminarDetalleAHCenso(int? idDetalle)
        {
            return DACElimina.EliminarDetalleAHCenso(idDetalle);
        }
        #endregion

        #region CONCURRENCIA    

        public List<ecop_concurrencia> ConsultaAfiliadoConcurrenia(ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAfiliadoConcurrenia(ObjAfiliado, ref MsgRes);
        }

        public List<ecop_concurrencia> ConsultaIdConcurrenia(ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdConcurrenia(ObjAfiliado, ref MsgRes);
        }

        public ecop_concurrencia ConsultaConcurrenciaId(int idconcurrencia)
        {
            return DACConsulta.ConsultaConcurrenciaId(idconcurrencia);
        }
        public List<ecop_concurrencia> ConsultaConcurrenciaIdLista(Int32 IdConcu, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaConcurrenciaIdLista(IdConcu, ref MsgRes);
        }

        public List<ecop_censo> ConsultaCensoIdLista(Int32 IdCenso, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCensoIdLista(IdCenso, ref MsgRes);
        }

        public List<vw_ecop_cohortes_evolucion> ConsultaCohortes(vw_ecop_cohortes_evolucion ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCohortes(ObjAfiliado, ref MsgRes);
        }

        public List<vw_tipo_habitacion_censo> ConsultaTipoHabitacion(vw_tipo_habitacion_censo ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaTipoHabitacion(ObjAfiliado, ref MsgRes);
        }
        public List<vw_ciudad_ips> ConsultaIdConcurreniaciudad(vw_ciudad_ips ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdConcurreniaciudad(ObjAfiliado, ref MsgRes);
        }


        public void ActualizaConcurrencia(ecop_concurrencia ObjConcurrencia, String User, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaConcurrencia(ObjConcurrencia, User, IPAddress, ref MsgRes);
        }

        public List<ecop_concurrencia> GetconcurrenciaAfiliados(string numidafiliado)
        {
            return DACConsulta.GetconcurrenciaAfiliados(numidafiliado);
        }

        public List<Ref_ips> GetRefIps()
        {
            return DACConsulta.GetRefIps();
        }

        public List<ref_eps> GetRefEps()
        {
            return DACConsulta.GetRefEps();
        }

        public void ActualizaEgresoConcurrencia(ecop_concurrencia ObjConcurrencia, String User, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaEgresoConcurrencia(ObjConcurrencia, User, IPAddress, ref MsgRes);
        }

        public List<ecop_concurrencia> ConsultaCriterioIngresoActualizado(Int32 IdConcu, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCriterioIngresoActualizado(IdConcu, ref MsgRes);
        }

        public List<ecop_concurrencia_encuesta_satisfacion> ConsultaEncuestaCargada(Int32 IdConcu, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaEncuestaCargada(IdConcu, ref MsgRes);
        }

        public int InsertaEgreso(egreso_auditoria_Hospitalaria Egreso, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertaEgreso(Egreso, UserName, IPAddress, ref MsgRes);
        }

        public List<vw_concurrencia_evolucion_Contrato> ConsultaIdConcurreniaEvolucion(ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdConcurreniaEvolucion(ObjAfiliado, ref MsgRes);
        }


        public List<vw_consulta_concurrencia> ConsultaConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaConcurrencia(ref MsgRes);
        }

        public List<vw_consulta_egreso> ConsultaEgreso(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaEgreso(ref MsgRes);
        }

        public List<managment_vw_consulta_egresoResult> ConsultaEgreso2(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaEgreso2(ref MsgRes);
        }

        public List<vw_consulta_eventos_adversos> ConsultaEventosAd(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaEventosAd(ref MsgRes);
        }

        public List<vw_consulta_situacion_calidad> ConsultaSituacionCal(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaSituacionCal(ref MsgRes);
        }

        public List<vw_gestantes> ConsultaGestantes(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaGestantes(ref MsgRes);
        }
        public List<management_controlNatalidadResult> ConsultaGestantesNuevo(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaGestantesNuevo(ref MsgRes);
        }
        public List<vw_gestantes_sin> ConsultaGestantesSin(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaGestantesSin(ref MsgRes);
        }


        public List<vw_Mortalidad> ConsultaMortalidad(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaMortalidad(ref MsgRes);
        }

        public List<ManagementConsultaConcuMortalidadCon_SinResult> ConsultaMortalidadV2(int tipoconsulta, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaMortalidadV2(tipoconsulta, ref MsgRes);
        }

        public List<vw_Mortalidad_sin> ConsultaMortalidadSin(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaMortalidadSin(ref MsgRes);
        }

        public void InsertarEncuestaConcurrencia(ecop_concurrencia_encuesta Encuesta, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarEncuestaConcurrencia(Encuesta, ref MsgRes);
        }


        public List<egreso_auditoria_Hospitalaria> ConsultaAgresoH(Int32 IdConcu, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAgresoH(IdConcu, ref MsgRes);

        }

        public void Actualizarprevenible(ecop_concurrencia Objconcurrencia, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.Actualizarprevenible(Objconcurrencia, ref MsgRes);
        }

        public List<vw_max_concurrencia_por_documento> ConsultaMaxConcurrenciaDocumento(String Documento, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaMaxConcurrenciaDocumento(Documento, ref MsgRes);
        }

        public void ActualizarEgreso(egreso_auditoria_Hospitalaria Objegreso, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEgreso(Objegreso, ref MsgRes);
        }

        public void InsertarEgresoGestantes(egreso_gestantes Gestantes, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarEgresoGestantes(Gestantes, ref MsgRes);
        }

        public List<egreso_gestantes> ConsultasEgresoGestantes(Int32 id_concurrencia, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultasEgresoGestantes(id_concurrencia, ref MsgRes);
        }

        public List<vw_ecop_egresos_hospitalarios> GetListaEgresos(DateTime? fechainicial, DateTime? fechafinal)
        {
            return DACConsulta.GetListaEgresos(fechainicial, fechafinal);
        }
        public List<management_ecop_egresos_hospitalariosResult> listadoEgresosHospitalarios(DateTime? fechainicial, DateTime? fechafinal)
        {
            return DACConsulta.listadoEgresosHospitalarios(fechainicial, fechafinal);
        }
        public ecop_concurrencia traerDatosBeneficiarioConcurrencia(string documentoBen)
        {
            return DACConsulta.traerDatosBeneficiarioConcurrencia(documentoBen);
        }
        public List<ecop_concurrencia_evolucion_procedimientos> traerDatosEvolucionProcedimientos(int id_evolucion)
        {
            return DACConsulta.traerDatosEvolucionProcedimientos(id_evolucion);
        }
        public List<ref_tipo_hospitalizacion> GetRefTipoHospitalizacion()
        {
            return DACConsulta.GetRefTipoHospitalizacion();
        }
        public List<ref_tipo_patologia_catastrofica> GetRefTipoPatologiaCatastrofica()
        {
            return DACConsulta.GetRefTipoPatologiaCatastrofica();
        }
        public List<ref_pertinencia_estancia_prolongada> GetRefPertinenciaProlongada()
        {
            return DACConsulta.GetRefPertinenciaProlongada();
        }
        public List<ref_condicion_estancia_prolongada> GetRefCondicionProlongada()
        {
            return DACConsulta.GetRefCondicionProlongada();
        }

        public categorizacion_egreso_hospitalario getcatbyegreso(int idegreso)
        {
            return DACConsulta.getcatbyegreso(idegreso);
        }

        public void insertarcategorizacion(categorizacion_egreso_hospitalario obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.insertarcategorizacion(obj, ref MsgRes);
        }
        public List<management_egresos_verCategorizacionResult> traerDatosCategorizacionEgreso(int idEgreso)
        {
            return DACConsulta.traerDatosCategorizacionEgreso(idEgreso);
        }

        public void actualizarcategorizacion(categorizacion_egreso_hospitalario obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.actualizarcategorizacion(obj, ref MsgRes);
        }

        public void ActualizarIps(int idconcurrencia, int idips, ref MessageResponseOBJ Msg)
        {
            DACActualiza.ActualizarIps(idconcurrencia, idips, ref Msg);
        }

        public List<ref_trimeste> GetRefTrimestre()
        {
            return DACConsulta.GetRefTrimestre();
        }
        public List<Ref_plan_mejora_categoria> GetRefplan_mejora_categoria()
        {
            return DACConsulta.GetRefplan_mejora_categoria();
        }
        public List<Ref_plan_mejora_foco> GetRef_plan_mejora_foco()
        {
            return DACConsulta.GetRef_plan_mejora_foco();
        }

        public List<Ref_proceso_auditado> GetRef_proceso_auditado()
        {
            return DACConsulta.GetRef_proceso_auditado();
        }

        public List<management_planmejora_focoResult> Cuentadetallefoco(Int32 id_plan_de_mejora, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.Cuentadetallefoco(id_plan_de_mejora, ref MsgRes);
        }

        public Int32 InsertarPlanMejora(ecop_plan_de_mejora obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejora(obj, ref MsgRes);
        }

        public Int32 InsertarPlanMejorafoco(ecop_plan_mejora_foco_intervencion obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejorafoco(obj, ref MsgRes);
        }

        public int ActualizarFocoPlanMejora(ecop_plan_mejora_foco_intervencion obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarFocoPlanMejora(obj, ref MsgRes);
        }


        public void EliminarDetallePlan(int id_plan_mejora_foco_intervencion, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarDetallePlan(id_plan_mejora_foco_intervencion, ref MsgRes);
        }

        public List<management_planmejora_tareaResult> CuentadetalleTarea(Int32 id_plan_mejora_foco_intervencion, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentadetalleTarea(id_plan_mejora_foco_intervencion, ref MsgRes);
        }

        public void EliminarDetallePlanTarea(int id_plan_mejora_tareas, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarDetallePlanTarea(id_plan_mejora_tareas, ref MsgRes);
        }

        public Int32 InsertarPlanMejoratarea(ecop_plan_mejora_tareas obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejoratarea(obj, ref MsgRes);
        }


        public void ActualizarPlanEgreso(int id_plan_mejora, Int32 estado, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarPlanEgreso(id_plan_mejora, estado, ref MsgRes);
        }

        public List<management_planmejora_tarea_controlResult> CuentadetalleTareacontrol(Int32 id_plan_mejora, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentadetalleTareacontrol(id_plan_mejora, ref MsgRes);
        }

        public List<management_plan_mejora_tableroResult> PlanTableroGeneral()
        {
            return DACConsulta.PlanTableroGeneral();
        }

        public List<management_planMejora_reporteResult> DatosPMReporte(int? idPlan)
        {
            return DACConsulta.DatosPMReporte(idPlan);
        }

        public List<management_planMejora_reporte_detalleCeacionResult> DatosPMReporteDetalleCreacion(int? idPlan)
        {
            return DACConsulta.DatosPMReporteDetalleCreacion(idPlan);
        }

        public List<management_planMejora_reporte_detalleCierreResult> DatosPMReporteDetalleCierre(int? idPlan)
        {
            return DACConsulta.DatosPMReporteDetalleCierre(idPlan);
        }

        public List<management_planMejora_tableroDocumentalResult> DatosPMgestionDocumental(int? regional, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return DACConsulta.DatosPMgestionDocumental(regional, fechaInicio, fechaFin);
        }

        public List<management_planesMejora_reporteSeguimientoResult> DatosPMgestionDocumentalReporte(int? regional, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return DACConsulta.DatosPMgestionDocumentalReporte(regional, fechaInicio, fechaFin);
        }

        public List<ref_planMejora_prioridad> listaPrioridadPM()
        {
            return DACConsulta.listaPrioridadPM();
        }
        public List<management_plan_mejora_tablero_dtllResult> PlanTableroGeneralDtll(Int32 id_plan_de_mejora, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.PlanTableroGeneralDtll(id_plan_de_mejora, ref MsgRes);
        }

        public List<management_planMejora_ampliacionesResult> PlanMejoraAmpliaciones(int? idPlan)
        {
            return DACConsulta.PlanMejoraAmpliaciones(idPlan);
        }

        public List<management_planMejora_DocumentosPlanResult> PlanMejoraArchivoporTipo(int? idPlan, int? tipo)
        {
            return DACConsulta.PlanMejoraArchivoporTipo(idPlan, tipo);
        }

        public int InsertarAmpliacionPlanMejora(log_PM_ampliaciones obj)
        {
            return DACInserta.InsertarAmpliacionPlanMejora(obj);
        }

        public int ActualizarPlanEgresoAmpliacion(DateTime? fechaAmpliacion, string obsAmpliacion, int? idPlan)
        {
            return DACActualiza.ActualizarPlanEgresoAmpliacion(fechaAmpliacion, obsAmpliacion, idPlan);
        }

        public ecop_plan_de_mejora_documental PlanMejoraGestionDocumentalId(int? idPlan, int? tipo)
        {
            return DACConsulta.PlanMejoraGestionDocumentalId(idPlan, tipo);
        }

        public ecop_plan_de_mejora_documental PlanMejoraGestionDocumentalIdArchivo(int? idArchivo)
        {
            return DACConsulta.PlanMejoraGestionDocumentalIdArchivo(idArchivo);
        }

        public void EliminarArchivoPM(int? idArchivo, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarArchivoPM(idArchivo, ref MsgRes);
        }

        public ecop_plan_de_mejora PlanMejoraId(int? idPlan)
        {
            return DACConsulta.PlanMejoraId(idPlan);
        }

        public List<ref_medicion_riesgo> PlanMejoraMedicionRiesgo()
        {
            return DACConsulta.PlanMejoraMedicionRiesgo();
        }

        public List<ref_costos_noCalidad> PlanMejoraCostosNoCalidad()
        {
            return DACConsulta.PlanMejoraCostosNoCalidad();
        }

        public List<ref_cobertura> PlanMejoraCobertura()
        {
            return DACConsulta.PlanMejoraCobertura();
        }

        public List<management_planmejora_tarea_obsResult> GetobsTareas(Int32 id_plan_mejora_tareas, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetobsTareas(id_plan_mejora_tareas, ref MsgRes);
        }

        public Int32 InsertarPlanMejora_obs(ecop_plan_mejora_obs_tareas obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejora_obs(obj, ref MsgRes);
        }

        public int ActualizarDatosPlanMejoraAccion(ecop_plan_mejora_obs_tareas obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarDatosPlanMejoraAccion(obj, ref MsgRes);
        }

        public void Actualizarplan_estado_tarea(int id_plan_mejora_tareas, Int32 estado, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.Actualizarplan_estado_tarea(id_plan_mejora_tareas, estado, ref MsgRes);
        }

        public List<management_planmejora_tarea_control_estadoResult> CuentadetalleTareacontrolEstado(Int32 id_plan_mejora, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentadetalleTareacontrolEstado(id_plan_mejora, ref MsgRes);
        }

        public Int32 InsertarPlanMejora_documentos(ecop_plan_de_mejora_documental obj)
        {
            return DACInserta.InsertarPlanMejora_documentos(obj);
        }

        public int IngresarPlanMejora_notificacionAdmin(ecop_plan_de_mejora_notificarAdmin obj)
        {
            return DACInserta.IngresarPlanMejora_notificacionAdmin(obj);
        }

        public List<management_planMejora_tableroVisitasResult> DatosPMVisitas(string auditor)
        {
            return DACConsulta.DatosPMVisitas(auditor);
        }


        public List<vw_planMejora_tableroVisitas> DatosPMVisitasRoles()
        {
            return DACConsulta.DatosPMVisitasRoles();
        }

        public int ActualizarEstadoPlanMejora(int? idPlan, int? estado)
        {
            return DACActualiza.ActualizarEstadoPlanMejora(idPlan, estado);
        }

        public List<management_planesMejora_alertaVencimientoResult> ListadoAlertasVencimiento()
        {
            return DACConsulta.ListadoAlertasVencimiento();
        }

        public int InsertarLogAlertasPlanes(List<log_planes_mejora_notificaciones> lista)
        {
            return DACInserta.InsertarLogAlertasPlanes(lista);
        }

        public management_planMejora_correosNotificacionIdPlanResult DatosNotificacionCorreos(int? idPlan)
        {
            return DACConsulta.DatosNotificacionCorreos(idPlan);
        }

        public log_planes_mejora_notificaciones TraerUltimoLogNotificacionPM(int? id_plan)
        {
            return DACConsulta.TraerUltimoLogNotificacionPM(id_plan);
        }

        public management_planmejora_datosIntervencionResult datosIntervencionPM(int? idInter)
        {
            return DACConsulta.datosIntervencionPM(idInter);
        }


        public List<management_plan_mejora_tablero_reactivacionResult> TraerListadoPlanesMejora(int? idPlan)
        {
            return DACConsulta.TraerListadoPlanesMejora(idPlan);
        }

        public int ActualizarEstadoPlanesDeMejora(int? idPlan)
        {
            return DACActualiza.ActualizarEstadoPlanesDeMejora(idPlan);
        }

        public int InsertarLogReactivacionPlanMejora(log_plan_mejora_reactivar obj)
        {
            return DACInserta.InsertarLogReactivacionPlanMejora(obj);
        }

        public int EliminarPlanDeMejora_id(int? idPlan)
        {
            return DACElimina.EliminarPlanDeMejora_id(idPlan);
        }

        public int InsertarLogEliminarPlanMejora(log_plan_mejora_eliminar obj)
        {
            return DACInserta.InsertarLogEliminarPlanMejora(obj);
        }


        #endregion

        #region EVOLUCION

        public List<Ref_valor_ahorro> GetRefValorAhorro()
        {
            return DACComonClass.GetRefValorAhorro();
        }

        public void InsertaConcurrenciaEvolucion(ecop_concurrencia_evolucion Evolucion, List<ecop_concurrencia_evolucion_procedimientos> lista, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertaConcurrenciaEvolucion(Evolucion, lista, UserName, IPAddress, ref MsgRes);
        }

        public List<ecop_concurrencia_evolucion> ConsultaEvoluciones(ecop_concurrencia_evolucion ObjEvolu, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaEvoluciones(ObjEvolu, ref MsgRes);
        }

        public List<vw_evo_ecop_concurrencia_evoluciones> ConsultaEvolucionesIps(vw_evo_ecop_concurrencia_evoluciones ObjEvolu, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaEvolucionesIps(ObjEvolu, ref MsgRes);
        }

        public void EliminarConcurrenciaEvolucion(ecop_concurrencia_evolucion ObjEvolucion, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarConcurrenciaEvolucion(ObjEvolucion, UserName, IPAddress, ref MsgRes);
        }

        public void EliminarPlanAccion(ecop_concurrencia_eventos_en_salud_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarPlanAccion(OBJ, ref MsgRes);
        }

        public List<ecop_concurrencia_evolucion_diag_def> ConsultaDiagnosticoDefinitivo(ecop_concurrencia_evolucion_diag_def Objdiagdef, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaDiagnosticoDefinitivo(Objdiagdef, ref MsgRes);
        }

        public List<ecop_concurrencia_cohorte> ConsultaCohorte(ecop_concurrencia_cohorte ObjCohorte, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCohorte(ObjCohorte, ref MsgRes);
        }

        public void InsertaDiagnosticoDefinitivo(ecop_concurrencia_evolucion_diag_def Concurrencia_Diagnostico_Definitivo_id, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertaDiagnosticoDefinitivo(Concurrencia_Diagnostico_Definitivo_id, UserName, IPAddress, ref MsgRes);
        }

        public void InsertaGlosa(ecop_concurrencia_glosa ObjGlosa, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertaGlosa(ObjGlosa, UserName, IPAddress, ref MsgRes);
        }

        public List<ecop_concurrencia_glosa> ConsultaGlosa(ecop_concurrencia_glosa ObjGlosa, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaGlosa(ObjGlosa, ref MsgRes);
        }

        public List<ecop_concurrencia_glosa> ConsultaGlosabyidconcurrencia(int idconcurrencia, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaGlosabyidconcurrencia(idconcurrencia, ref MsgRes);
        }

        public List<ecop_concurrencia_ahorro> ConsultaAhorro(ecop_concurrencia_ahorro ObjAhorro, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAhorro(ObjAhorro, ref MsgRes);
        }

        public List<vw_ecop_concurrencia_ahorro> ConsultaAhorroOtro(vw_ecop_concurrencia_ahorro ObjAhorro, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAhorroOtro(ObjAhorro, ref MsgRes);
        }

        public List<vw_ecop_concurrencia_cohorte> ConsultaCohorte(vw_ecop_concurrencia_cohorte ObjCohorte, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCohorte(ObjCohorte, ref MsgRes);
        }


        public List<Ref_eventos_adversos> GetEventosAdversos()
        {
            return DACComonClass.GetEventosAdversos();
        }
        public List<Ref_grado_lesion> GetGradoLesion()
        {
            return DACComonClass.GetGradoLesion();
        }
        public List<Ref_factores_contribuyentes> GetFactoresContribuyentes()
        {
            return DACComonClass.GetFactoresContribuyentes();
        }
        public List<Ref_barreras_seguridad> GetBarrerasDeSeguridad()
        {
            return DACComonClass.GetBarrerasDeSeguridad();
        }
        public List<Ref_acciones_inseguras> GetAccionesInseguras()
        {
            return DACComonClass.GetAccionesInseguras();
        }
        public List<Ref_plan_de_manejo> GetPlanDeManejo()
        {
            return DACComonClass.GetPlanDeManejo();
        }

        public void InsertaEventoAdverso(ecop_concurrencia_eventos_adversos ObjEventoAdv, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertaEventoAdverso(ObjEventoAdv, UserName, IPAddress, ref MsgRes);
        }

        public List<ecop_concurrencia_eventos_adversos> ConsultaEventoAdverso(ecop_concurrencia_eventos_adversos ObjEventoAdverso, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaEventoAdverso(ObjEventoAdverso, ref MsgRes);
        }

        public List<Ref_situaciones_de_calidad> GetSituacionesDeCalidad()
        {
            return DACComonClass.GetSituacionesDeCalidad();
        }

        public void InsertaSituacionesCalidad(ecop_concurrencia_situaciones_de_calidad ObjSituacionCalid, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertaSituacionesCalidad(ObjSituacionCalid, UserName, IPAddress, ref MsgRes);
        }

        public List<ecop_concurrencia_situaciones_de_calidad> ConsultaSituacionesCalidad(ecop_concurrencia_situaciones_de_calidad ObjSituCali, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaSituacionesCalidad(ObjSituCali, ref MsgRes);
        }
        public List<Ref_motivo_cancelacion_procedimiento> GetMotivoCancelacion()
        {
            return DACComonClass.GetMotivoCancelacion();
        }

        public void InsertaProcedimientoQuirugicoCancelado(ecop_concurrencia_procedimientos_quirurgicos_cancelados ProcedimientoQuirCance, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertaProcedimientoQuirugicoCancelado(ProcedimientoQuirCance, UserName, IPAddress, ref MsgRes);
        }
        public List<ecop_concurrencia_procedimientos_quirurgicos_cancelados> ConsultaProcQuirurgicosCance(ecop_concurrencia_procedimientos_quirurgicos_cancelados ObjProcQuir, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaProcQuirurgicosCance(ObjProcQuir, ref MsgRes);
        }

        public void InsertarNatalidad(natalidad_sin_concurrencia Natalidad, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarNatalidad(Natalidad, ref MsgRes);
        }

        public void InsertarMortalidad(mortalidad_sin_concurrencia Mortalidad, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarMortalidad(Mortalidad, ref MsgRes);
        }
        public List<vw_tablero_eventos_adversos> ReportesEventoAdverso()
        {
            return DACConsulta.ReportesEventoAdverso();
        }

        public void InsertarAlertasConcurrencia(alertas_generadas_concurrencia Alertas, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarAlertasConcurrencia(Alertas, ref MsgRes);
        }

        public void InsertarConcurrenciaAhorro(ecop_concurrencia_ahorro Ahorro, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarConcurrenciaAhorro(Ahorro, ref MsgRes);
        }

        public void InsertarConcurrenciaCohorte(ecop_concurrencia_cohorte Cohorte, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarConcurrenciaCohorte(Cohorte, ref MsgRes);
        }

        public List<Ref_causal_glosa> ConsultaCausalGlosa(int id_respnsable_glosa, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCausalGlosa(id_respnsable_glosa, ref MsgRes);
        }



        public List<alertas_generadas_concurrencia> ConsultaAlertasConcurrencia(Int32 Idalerta, string idcie10, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAlertasConcurrencia(Idalerta, idcie10, ref MsgRes);
        }

        public vw_cie10_alertas ConsultaAlertaCie10(String idcie10, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAlertaCie10(idcie10, ref MsgRes);
        }
        public ref_cie10_detalle ConsultaAlertaCie10Detalle(String idcie10, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAlertaCie10Detalle(idcie10, ref MsgRes);
        }
        public List<analisis_caso_original> ConsultaEvolucionAnalisisCasoOriginal(Int32? idconcurrencia, Int32? idanalisiscaso, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaEvolucionAnalisisCasoOriginal(idconcurrencia, idanalisiscaso, ref MsgRes);
        }

        public List<Ref_valor_ahorro> ValorAhorro()
        {
            return DACConsulta.ValorAhorro();
        }

        public List<vw_evo_ecop_concurrencia_evoluciones> GetConcurrenciaEvolucionById(int id_evolucion)
        {
            return DACConsulta.GetConcurrenciaEvolucionById(id_evolucion);
        }

        public void MandarConcurrenciaContactCenter(List<int> listado, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.MandarConcurrenciaContactCenter(listado, ref MsgRes);
        }

        public void MandarindividualConcurrenciaContactCenter(int? idConcu, string observacion, int? usuario, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.MandarindividualConcurrenciaContactCenter(idConcu, observacion, usuario, ref MsgRes);
        }
        public vw_ecop_evo_evaluacion_pertinencia GetEvaluacionPertinenciaById(int idevolucion)
        {
            return DACConsulta.GetEvaluacionPertinenciaById(idevolucion);
        }

        public List<management_evolucionEgresosListaResult> GetEvolucionesConcurrencia(int idConcu)
        {
            return DACConsulta.GetEvolucionesConcurrencia(idConcu);
        }

        public List<Management_evolucion_procedimientosResult> ConsultaProcedimientosConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaProcedimientosConcurrencia(ref MsgRes);
        }

        #endregion

        #region CONSULTAS
        public List<ManagmentAlertasCalidadResult> CuentaFechaCargue(Int32 Opc, DateTime FechaInicial, DateTime FechaFin, String strProveedor, String strEstado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentaFechaCargue(Opc, FechaInicial, FechaFin, strProveedor, strEstado, ref MsgRes);
        }

        public List<vw_Devoluciones_sin_gestionar> DevolucionesSinGestion()
        {
            return DACConsulta.DevolucionesSinGestion();
        }

        public Int32 InsertarDevolucionGestionadas(factura_devolucion_gestionadas OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDevolucionGestionadas(OBJ, ref MsgRes);
        }

        public List<vw_hallazgos_RIPS> HallazgosRipsSinGestion()
        {
            return DACConsulta.HallazgosRipsSinGestion();
        }

        public List<vw_facturas_sin_auditar> FacturasporAuditar()
        {
            return DACConsulta.FacturasporAuditar();
        }

        public List<vw_costo_evitado> CostoEvitado(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CostoEvitado(Id, ref MsgRes);
        }

        public List<vw_facturas_diagnosticos> DiagnosticosCuentas(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.DiagnosticosCuentas(Id, ref MsgRes);
        }

        public List<vw_ECOPETROL_DEVOLUCION_FAC> VwDevoluciones()
        {
            return DACConsulta.VwDevoluciones();
        }

        public List<vw_ECOPETROL_HALLAZGOS_RIPS> VwHallazgosRIPS()
        {
            return DACConsulta.VwHallazgosRIPS();
        }

        public List<ECOPETROL_RECEPCION_FACTURAS> VwRecepcionFacturas()
        {
            return DACConsulta.VwRecepcionFacturas();
        }

        public void InsertarMega(List<megas_cargue_base> ListMega, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarMega(ListMega, ref MsgRes);
        }


        public List<ManagmentClinicosCensoConConcurrenciaResult> CensoConcurrenciaEcopetrol(DateTime fecha_ini, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CensoConcurrenciaEcopetrol(fecha_ini, fecha_final, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 21 de diciembre de 2022
        /// Metodo que reemplazara la consulta CensoConcurrenciaEcopetrol se añade consulta por SQL COMMAND
        /// </summary>
        /// <param name="fecha_ini"></param>
        /// <param name="fecha_final"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public DataTable CensoConcurrenciaEcopetrolII(DateTime fecha_ini, DateTime fecha_final, String Conexion, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CensoConcurrenciaEcopetrolII(fecha_ini, fecha_final, Conexion, ref MsgRes);
        }

        public List<ManagmentClinicosCensoResult> CensoEcopetrol(DateTime fecha_ini, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CensoEcopetrol(fecha_ini, fecha_final, ref MsgRes);
        }

        public List<ManagmentClinicosConsultasAlertasResult> AlertasEcopetrol(DateTime fecha_ini, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.AlertasEcopetrol(fecha_ini, fecha_final, ref MsgRes);
        }


        #endregion

        #region FACTURAS

        public Int32 InsertarDevolucionFacturas(factura_devolucion OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDevolucionFacturas(OBJ, ref MsgRes);
        }

        public Int32 InsertarDevolucionFacturasDetalle(factura_devolucion_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDevolucionFacturasDetalle(OBJ, ref MsgRes);
        }

        public List<ManagmentReportDevolucionFacResult> ConsultaReporteDevolucionFac(Int32 id_devolucion_factura)
        {
            return DACConsulta.ConsultaReporteDevolucionFac(id_devolucion_factura);
        }

        public List<ManagmentReportDevolucionFac_glosasResult> ConsultaReporteDevolucionFac_glosa(int? id_af)
        {
            return DACConsulta.ConsultaReporteDevolucionFac_glosa(id_af);
        }

        public Int32 InsertarFacturaSinCenso(factura_sin_censo OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturaSinCenso(OBJ, ref MsgRes);
        }

        public Int32 InsertarHallazgos(hallazgo_RIPS OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarHallazgos(OBJ, ref MsgRes);
        }

        public Int32 InsertarHallazgosDetalle(hallazgo_RIPS_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarHallazgosDetalle(OBJ, ref MsgRes);
        }

        public List<ManagmentReportHallazgosRipResult> ConsultaReporteHallazgosRips(Int32 id_hallazgo_RIPS)
        {
            return DACConsulta.ConsultaReporteHallazgosRips(id_hallazgo_RIPS);
        }

        public void ActualizaHallazgosRips(hallazgo_RIPS Objrips, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaHallazgosRips(Objrips, ref MsgRes);
        }

        public List<factura_sin_censo> ConsultaFacturasSinAudi(Int32 id_factura_sin_censo)
        {
            return DACConsulta.ConsultaFacturasSinAudi(id_factura_sin_censo);

        }

        public Int32 InsertarCostoEvitado(factura_sin_censo_cos_evitado Obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCostoEvitado(Obj, ref MsgRes);
        }
        public Int32 InsertarDiagnosticoCuentas(factura_sin_censo_diagnosticos Obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDiagnosticoCuentas(Obj, ref MsgRes);
        }

        public void ActualizaFacturaAuditada(factura_sin_censo ObjAudi, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaFacturaAuditada(ObjAudi, ref MsgRes);
        }

        public List<factura_devolucion> ConsultaDevolucionesFactura(String Numero_factura)
        {
            return DACConsulta.ConsultaDevolucionesFactura(Numero_factura);
        }

        public List<factura_sin_censo> ConsultaFacturaNumero(String Numero_factura)
        {
            return DACConsulta.ConsultaFacturaNumero(Numero_factura);
        }

        public List<factura_devolucion> ConsultaDevolucionesFacturaId(Int32 Id_devolucion)
        {
            return DACConsulta.ConsultaDevolucionesFacturaId(Id_devolucion);
        }

        public List<hallazgo_RIPS> ConsultaHallazgosId(Int32 Id_rips)
        {
            return DACConsulta.ConsultaHallazgosId(Id_rips);
        }


        public List<management_rips_busqueda_acResult> TraerConsultaRIPSAC(DateTime? fechaInicio, DateTime? fechaFin, string codCups, string diagnostico, string cedula)
        {
            return DACConsulta.TraerConsultaRIPSAC(fechaInicio, fechaFin, codCups, diagnostico, cedula);
        }

        public List<management_rips_busqueda_apResult> TraerConsultaRIPSAP(DateTime? fechaInicio, DateTime? fechaFin, string codCups, string diagnostico, string cedula)
        {
            return DACConsulta.TraerConsultaRIPSAP(fechaInicio, fechaFin, codCups, diagnostico, cedula);
        }


        public Int32 InsertarloteContabilizado(ecop_gestion_factura_digital_contabilizados_lote OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarloteContabilizado(OBJ, ref MsgRes);
        }

        public List<management_reportelotecontabilizadoResult> ConsultaReporteLote(Int32 ID)
        {
            return DACConsulta.ConsultaReporteLote(ID);
        }

        public List<facturacion_sap_cargue> validarCargueFacturaSap(int? mes, int? año)
        {
            return DACConsulta.validarCargueFacturaSap(mes, año);
        }
        public int InsertarFacturacionSAP(List<facturacion_sap_dtll> List, facturacion_sap_cargue objbase, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturacionSAP(List, objbase, ref MsgRes);
        }
        public List<management_facturacionSAP_listaResult> ListarFacturacionSAP()
        {
            return DACConsulta.ListarFacturacionSAP();
        }
        public List<management_facturacionSAP_listaDetalleResult> ListarFacturacionSAPDetalle(int año, string mes)
        {
            return DACConsulta.ListarFacturacionSAPDetalle(año, mes);
        }
        public List<management_facturacionSAP_listaREPORTEResult> ListarFacturacionSAPReporte(DateTime fechaIni, DateTime fechaFin)
        {
            return DACConsulta.ListarFacturacionSAPReporte(fechaIni, fechaFin);
        }
        #endregion

        #region CUENTAS MEDICAS

        public Int32 InsertarRips(RIPS Objrips, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRips(Objrips, ref MsgRes);
        }

        public List<RIPS> ConsultaRips(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaRips(IdRips, ref MsgRes);
        }

        public bool ActualizaRips(RIPS ObjRips, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizaRips(ObjRips, ref MsgRes);
        }


        public Int32 InsertarRipsAC(List<RIPS_AC> ObjripsAc, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsAC(ObjripsAc, ref MsgRes);
        }

        public Int32 InsertarRipsAD(List<RIPS_AD> ObjripsAD, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsAD(ObjripsAD, ref MsgRes);
        }

        public Int32 InsertarRipsAF(List<RIPS_AF> ObjripsAF, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsAF(ObjripsAF, ref MsgRes);
        }

        public Int32 InsertarRipsAH(List<RIPS_AH> ObjripsAH, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsAH(ObjripsAH, ref MsgRes);
        }

        public Int32 InsertarRipsAM(List<RIPS_AM> ObjripsAM, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsAM(ObjripsAM, ref MsgRes);
        }

        public Int32 InsertarRipsAN(List<RIPS_AN> ObjripsAN, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsAN(ObjripsAN, ref MsgRes);
        }

        public Int32 InsertarRipsAP(List<RIPS_AP> ObjripsAP, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsAP(ObjripsAP, ref MsgRes);
        }

        public Int32 InsertarRipsAT(List<RIPS_AT> ObjripsAT, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsAT(ObjripsAT, ref MsgRes);
        }

        public Int32 InsertarRipsAU(List<RIPS_AU> ObjripsAU, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsAU(ObjripsAU, ref MsgRes);
        }

        public Int32 InsertarRipsCT(List<RIPS_CT> ObjripsCT, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsCT(ObjripsCT, ref MsgRes);
        }

        public Int32 InsertarRipsUS(List<RIPS_US> ObjripsUS, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRipsUS(ObjripsUS, ref MsgRes);
        }

        public List<ECOPETROL_COMMON.ENUM.reporterips> ConsultaRipsEvaluacion(Int32 IdRips, string conexion, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaRipsEvaluacion(IdRips, conexion, ref MsgRes);
        }

        public List<managmentReportePrestadoresNoExistentesResult> getprestadoresnoexistentes(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.getprestadoresnoexistentes(IdRips, ref MsgRes);
        }

        public List<Logerroresevaluacionrips> ConsultaLogRipsEvaluacion(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaLogRipsEvaluacion(IdRips, ref MsgRes);
        }

        public List<management_rips_tableroControlResult> TraerListadoRips()
        {
            return DACConsulta.TraerListadoRips();
        }

        public List<management_rips_tableoControl_detalladoResult> TraerListadoRipsConsolidadoId(int? idRips)
        {
            return DACConsulta.TraerListadoRipsConsolidadoId(idRips);
        }

        public int EliminarCargueRipsId(int? idRips)
        {
            return DACElimina.EliminarCargueRipsId(idRips);
        }

        public int InsertarLogEliminacionRips(log_rips_eliminarCargue log)
        {
            return DACInserta.InsertarLogEliminacionRips(log);
        }

        public List<RIPS> GetListaRipsPorMesYAño(int mes, int año, int? regional)
        {
            return DACConsulta.GetListaRipsPorMesYAño(mes, año, regional);
        }

        public List<ManagmentErroresRipsEvaluacion_historicoResult> ConsultaLogRipsEvaluacionHistorico(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaLogRipsEvaluacionHistorico(IdRips, ref MsgRes);
        }

        public List<LogerroresevaluacionripsDtll> ConsultaLogRipsEvaluacionDtll(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaLogRipsEvaluacionDtll(IdRips, ref MsgRes);
        }

        public List<ManagmentErroresRipsEvaluacion_Dtll_historicoResult> ConsultaLogRipsEvaluacionDtllHistorico(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaLogRipsEvaluacionDtllHistorico(IdRips, ref MsgRes);
        }

        public vw_totales_rips_evaluacion ConsultaTotalesRipsEvaluacion(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaTotalesRipsEvaluacion(IdRips, ref MsgRes);
        }

        public RIPS_AC GetRipsAcById(int idripsac)
        {
            return DACConsulta.GetRipsAcById(idripsac);
        }

        public RIPS_AP GetRipsApById(int idripsap)
        {
            return DACConsulta.GetRipsApById(idripsap);
        }

        public RIPS_AU GetRipsAuById(int id)
        {
            return DACConsulta.GetRipsAuById(id);
        }

        public RIPS_AH GetRipsAhById(int id)
        {
            return DACConsulta.GetRipsAhById(id);
        }

        public RIPS_AN GetRipsAnById(int id)
        {
            return DACConsulta.GetRipsAnById(id);
        }

        public List<RIPS_AC_HISTORICO> GetRipsAcHistoricoById(int id, int modo)
        {
            return DACConsulta.GetRipsAcHistoricoById(id, modo);
        }

        public List<RIPS_AP_HISTORICO> GetRipsApHistoricoById(int id, int modo)
        {
            return DACConsulta.GetRipsApHistoricoById(id, modo);
        }

        public List<RIPS_AU_HISTORICO> GetRipsAuHistoricoById(int id, int modo)
        {
            return DACConsulta.GetRipsAuHistoricoById(id, modo);
        }

        public List<RIPS_AH_HISTORICO> GetRipsAhHistoricoById(int id, int modo)
        {
            return DACConsulta.GetRipsAhHistoricoById(id, modo);
        }

        public List<RIPS_AN_HISTORICO> GetRipsAnHistoricoById(int id, int modo)
        {
            return DACConsulta.GetRipsAnHistoricoById(id, modo);
        }

        public List<RIPS_AF_HISTORICO> GetRipsAfHistoricoById(int id)
        {
            return DACConsulta.GetRipsAfHistoricoById(id);
        }

        public List<RIPS_US_HISTORICO> GetRipsUsHistoricoById(int id)
        {
            return DACConsulta.GetRipsUsHistoricoById(id);
        }

        //Oportunidad rips
        public List<RIPS_AC_HISTORICO> GetRipsAcOportunidadById(int id, int modo)
        {
            return DACConsulta.GetRipsAcOportunidadById(id, modo);
        }

        public List<RIPS_AP_HISTORICO> GetRipsApOportunidadById(int id, int modo)
        {
            return DACConsulta.GetRipsApOportunidadById(id, modo);
        }
        public List<Managment_ReportePrefacturas_total_abiertasResult> GetPrefacturasTotalAbiertas()
        {
            return DACConsulta.GetPrefacturasTotalAbiertas();
        }
        public List<Managment_ReportePrefacturas_total_cerradasResult> GetPrefacturasTotalCerradas()
        {
            return DACConsulta.GetPrefacturasTotalCerradas();
        }


        public List<RIPS_AU_HISTORICO> GetRipsAuoportunidadById(int id, int modo)
        {
            return DACConsulta.GetRipsAuoportunidadById(id, modo);
        }

        public List<RIPS_AH_HISTORICO> GetRipsAhOportunidadById(int id, int modo)
        {
            return DACConsulta.GetRipsAhOportunidadById(id, modo);
        }

        public List<RIPS_AN_HISTORICO> GetRipsAnOportunidadById(int id, int modo)
        {
            return DACConsulta.GetRipsAnOportunidadById(id, modo);
        }

        //fin rips oportunidad

        public List<managmentRips_AC_FechaconsultaResult> ConsultaRipsFechaConsulta(DateTime FechaInicio, DateTime FechaFinal, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaRipsFechaConsulta(FechaInicio, FechaFinal, ref MsgRes);
        }

        public List<managmentRips_AP_FechaProcedimientoResult> ConsultaRipsFechaProcedimeinto(int? regional, DateTime FechaInicio, DateTime FechaFinal, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaRipsFechaProcedimiento(regional, FechaInicio, FechaFinal, ref MsgRes);
        }

        public List<vw_consulta_rips_an_fechanacimiento> GetListRipsFechaNacimiento(DateTime FechaInicio, DateTime FechaFinal, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaRipsFechaNacimiento(FechaInicio, FechaFinal, ref MsgRes);
        }

        public List<Ref_tipo_rips> ConsultaTipoRips(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaTipoRips(ref MsgRes);
        }

        public List<vw_consulta_rips_ah_mortalidad> GetListRipsMortalidadAH(DateTime? FechaInicial, DateTime? FechaFinal, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetListRipsMortalidadAH(FechaInicial, FechaFinal, ref MsgRes);
        }


        public List<vw_consulta_rips_au_mortalidad> GetListRipsMortalidadAU(DateTime? FechaInicial, DateTime? FechaFinal, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetListRipsMortalidadAU(FechaInicial, FechaFinal, ref MsgRes);
        }

        public RIPS ValidacionCargueRips(int idregional, int mes, int año)
        {
            return DACConsulta.ValidacionCargueRips(idregional, mes, año);
        }
        #endregion

        #region PQRS

        public List<ecop_pqrs_a_quien_llamo> aQuienLlamoId(int? idPqr)
        {
            return DACConsulta.aQuienLlamoId(idPqr);
        }


        public void ActualizarPQRS(ecop_PQRS ObjPqrs, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarPQRS(ObjPqrs, ref MsgRes);
        }

        public void ActualizarPQRSAuditor(ecop_PQRS_Auditor obj)
        {
            DACActualiza.ActualizarPQRSAuditor(obj);
        }

        public int? ActualizarPQRSAuditorId(int? idPqrs)
        {
            return DACActualiza.ActualizarPQRSAuditorId(idPqrs);
        }

        public ecop_PQRS LlamarPqrsById(int id)
        {
            return DACConsulta.LlamarPqrsById(id);
        }

        public int eliminarArchivoPqrsidArchivo(int id)
        {
            return DACElimina.eliminarArchivoPqrsidArchivo(id);
        }

        public int GuardarLogEliminarArchivoPqr(log_ecop_pqrs_eliminarArchivos obj)
        {
            return DACInserta.GuardarLogEliminarArchivoPqr(obj);
        }
        public void ActualizarEstadoEnrevisionpryectada(ecop_PQRS_enrevision OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEstadoEnrevisionpryectada(OBJ, ref MsgRes);
        }

        public List<vw_ecop_PQRS> ConsultaPQRS()
        {
            return DACConsulta.ConsultaPQRS();
        }

        public List<management_pqrs_tableroAuditorResult> ConsultaTableroAuditor(int? filtrado, int? idAuditor)
        {
            return DACConsulta.ConsultaTableroAuditor(filtrado, idAuditor);
        }

        public List<management_pqrsAuditor_reporteResult> ReporteAuditorPqrs(int idPqrs)
        {
            return DACConsulta.ReporteAuditorPqrs(idPqrs);
        }

        public List<management_pqrs_datosReporte_conceptoResult> ReporteConceptoAuditor(int idPqrs)
        {
            return DACConsulta.ReporteConceptoAuditor(idPqrs);
        }

        public GestionDocumentalPQRS2 ExisteArchivoConceptoAuditor(int idPqr)
        {
            return DACConsulta.ExisteArchivoConceptoAuditor(idPqr);
        }


        public List<vw_ecop_PQRS_enrevision> ConsultaPQRSEnRevision()
        {
            return DACConsulta.ConsultaPQRSEnRevision();
        }


        public List<Ref_PQRS_usuarios> GetusuariosPQRS()
        {
            return DACConsulta.GetusuariosPQRS();
        }


        public List<ecop_PQRS> GetPQRSId(int id)
        {
            return DACConsulta.GetPQRSId(id);
        }

        public List<ecop_PQRS_enrevision> GetPQRSIdEnrevision(int id)
        {
            return DACConsulta.GetPQRSIdEnrevision(id);
        }

        public List<ecop_PQRS_enrevision> GetIdPQRSEnrevision(int id)
        {
            return DACConsulta.GetIdPQRSEnrevision(id);
        }


        public List<vw_ecop_PQRS2> ConsultaPQRS2()
        {
            return DACConsulta.ConsultaPQRS2();
        }

        public Int32 InsertarPQRS(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPQRS(OBJ, ref MsgRes);
        }

        public List<vw_ecop_PQRS_DetalleG> ConsultaPQRSDetalle(Int32 Id_pqrs)
        {

            return DACConsulta.ConsultaPQRSDetalle(Id_pqrs);
        }

        public List<management_vw_ecop_PQRS_DetalleGResult> ConsultaPQRSDetalle2(Int32 Id_pqrs)
        {
            return DACConsulta.ConsultaPQRSDetalle2(Id_pqrs);
        }

        public List<ecop_pqrs_prestadores> ListadoPrestadoresIdPqrs(int? idPqrs)
        {
            return DACConsulta.ListadoPrestadoresIdPqrs(idPqrs);
        }


        public log_pqrs_reinicioConteo_asignacionAnalistas BuscarReinicioConteoPqrs(int? mes, int? año)

        {
            return DACConsulta.BuscarReinicioConteoPqrs(mes, año);
        }

        public int InsertarLogReinicioConteoPqrs(log_pqrs_reinicioConteo_asignacionAnalistas obj)
        {
            return DACInserta.InsertarLogReinicioConteoPqrs(obj);
        }

        public int ActualizaConteoPqrsAnalistas()
        {
            return DACActualiza.ActualizaConteoPqrsAnalistas();
        }

        public List<management_buscarAnalistaPqrsResult> AnalistaPqr(int ciudad, int regional)
        {
            return DACConsulta.AnalistaPqr(ciudad, regional);
        }
        public Ref_PQRS_usuarios GetUsuarioPqrs(string usuario)
        {
            return DACConsulta.GetUsuarioPqrs(usuario);
        }

        public Ref_PQRS_usuarios GetUsuarioPqrsId(int? idUsuario)
        {
            return DACConsulta.GetUsuarioPqrsId(idUsuario);
        }

        public List<Ref_PQRS_Atributo> listaAtributosPqrs()
        {
            return DACConsulta.listaAtributosPqrs();
        }

        public List<sis_usuario> GetListAuditorCiudad(Int32? regional, Int32? ciudad, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetListAuditorCiudad(regional, ciudad, ref MsgRes);
        }

        public List<management_pqrs_busquedaAudirotesRegionalResult> BusquedaAuditoresPqrs(int? idRegional, int? cargo)
        {
            return DACConsulta.BusquedaAuditoresPqrs(idRegional, cargo);
        }


        public Int32? Getidauditor(string nombre)
        {
            return DACConsulta.Getidauditor(nombre);
        }

        public List<vw_ecop_PQRS> ConsultaPQRSId(Int32 id_ecop_PQRS)
        {
            return DACConsulta.ConsultaPQRSId(id_ecop_PQRS);
        }

        public List<vw_ecop_PQRS2> ConsultaPQRSId2(Int32 id_ecop_PQRS)
        {
            return DACConsulta.ConsultaPQRSId2(id_ecop_PQRS);
        }

        public List<management_vw_ecop_PQRS2Result> ConsultaPQRSId3(Int32 id_ecop_PQRS)
        {
            return DACConsulta.ConsultaPQRSId3(id_ecop_PQRS);
        }

        public Int32 InsertarPQRSAuditor(ecop_PQRS_Auditor OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPQRSAuditor(OBJ, ref MsgRes);
        }

        public Int32 InsertarPQRSProyeccion(GestionDocumentalPQRS2 OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPQRSProyeccion(OBJ, ref MsgRes);
        }
        public Int32 InsertarArchivoPQRRespuestaProyectada(GestionDocumentalPQRS2 OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarArchivoPQRRespuestaProyectada(OBJ, ref MsgRes);
        }
        public Int32 PqrInsertarArchivoRepositorioCierre(GestionDocumentalPQRS2 OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.PqrInsertarArchivoRepositorioCierre(OBJ, ref MsgRes);
        }
        public int InsertarArchivoReaperturaPQR(GestionDocumentalPQRS2 OBJ)
        {
            return DACInserta.InsertarArchivoReaperturaPQR(OBJ);
        }

        public Int32 InsertarPQRSEnrevision(ecop_PQRS_enrevision OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPQRSEnrevision(OBJ, ref MsgRes);
        }


        public List<ecop_PQRS_Auditor> ConsultaPQRSAuditor(Int32 Id_pqrs)
        {
            return DACConsulta.ConsultaPQRSAuditor(Id_pqrs);
        }
        public List<management_pqrs_auditorListaResult> ListaPqrsAuditor(int idPqrs)
        {
            return DACConsulta.ListaPqrsAuditor(idPqrs);
        }

        public List<GestionDocumentalPQRS2> GetUrlProyeccion(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetUrlProyeccion(Id, ref MsgRes);
        }
        public List<management_pqrs_mirarArchivosResult> ArchivosPqrs(Int32 idPqr)
        {
            return DACConsulta.ArchivosPqrs(idPqr);
        }
        public GestionDocumentalPQRS2 traerArchivoPqr(int idArchivo)
        {
            return DACConsulta.traerArchivoPqr(idArchivo);
        }
        public GestionDocumentalPQRS2 traerArchivoPqrId(int idPqr)
        {
            return DACConsulta.traerArchivoPqrId(idPqr);
        }

        public List<GestionDocumentalPQRS> GetUrlDocumentosPqrs(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetUrlDocumentosPqrs(Id, ref MsgRes);
        }



        public void ActualizarFechaPQRS(Int32 id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarFechaPQRS(id_ecop_PQRS, ref MsgRes);
        }

        public void ActualizaestadoPQRSEnrevision(ecop_PQRS_enrevision obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaestadoPQRSEnrevision(obj, ref MsgRes);
        }

        public void ActualizarGestionPQRSEnrevision(ecop_PQRS_enrevision obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarGestionPQRSEnrevision(obj, ref MsgRes);
        }


        public void ActualizaReabrirPQRS(ecop_PQRS obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaReabrirPQRS(obj, ref MsgRes);
        }

        public void ActualizarFechaPQRSDirec(Int32 id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarFechaPQRSDirec(id_ecop_PQRS, ref MsgRes);
        }

        public int ActualizarPqrsEstado(ecop_PQRS obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarPqrsEstado(obj, ref MsgRes);
        }



        public List<vw_ecop_PQRS_correo_direc> ConsultaPQRSCorreo()
        {
            return DACConsulta.ConsultaPQRSCorreo();
        }


        public void EliminarPQRS(int id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarPQRS(id_ecop_PQRS, ref MsgRes);
        }

        public Int32 InsertarPQRSEliminar(Log_eliminacion_pqrs OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPQRSEliminar(OBJ, ref MsgRes);
        }

        public List<vw_prestadores_lotes> GetRecepcionFacturas(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetRecepcionFacturas(ref MsgRes);
        }

        public List<vw_analistas_lotes> GetRecepcionFacturasAnalistas(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetRecepcionFacturasAnalistas(ref MsgRes);
        }
        public List<vw_prestadores_lotes> GetRecepcionFacturas2(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetRecepcionFacturas2(ref MsgRes);
        }

        public List<managment_prestadores_facturasResult> GetFacturasByIdRecepcion(int idrecepcion, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByIdRecepcion(idrecepcion, ref MsgRes);
        }

        public List<managment_prestadores_facturas2Result> GetFacturasByIdRecepcion2(int idrecepcion, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByIdRecepcion2(idrecepcion, ref MsgRes);
        }

        public List<managment_prestadores_facturasResult> GetFactura(int idrecepcion, int iddetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFactura(idrecepcion, iddetalle, ref MsgRes);
        }
        public managment_prestadores_facturas_GDResult GetFacturaGD(int iddetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturaGD(iddetalle, ref MsgRes);
        }
        public managment_prestadores_facturas_GD_zipResult GetFacturaGD2(int iddetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturaGD2(iddetalle, ref MsgRes);
        }

        public List<managmentprestadoresfacturasestadoResult> GetFacturasByEstado(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByEstado(idestado, ref MsgRes);
        }
        public List<managmentprestadoresfacturasaceptadasResult> GetFacturasAceptadas(int idestado, int id_usuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasAceptadas(idestado, id_usuario, ref MsgRes);
        }
        public List<managmentprestadoresfacturasaceptadasOKResult> GetFacturasAceptadas2(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasAceptadas2(ref MsgRes);
        }
        public List<managmentprestadoresfacturasgestionadas3Result> GetGestionFacturas()
        {
            return DACConsulta.GetGestionFacturas();
        }

        public List<managmentprestadoresfacturasgestionadas3_numFacturaResult> BuscarFactura_numFactura(string numFactura)
        {
            return DACConsulta.BuscarFactura_numFactura(numFactura);
        }

        /*Obtener los datos del procedimiento otra forma*/
        public List<managmentprestadoresfacturasgestionadasFechasResult> GetGestionFacturasFechas(DateTime fechaIni, DateTime fechaFin)
        {
            return DACConsulta.GetGestionFacturasFechas(fechaIni, fechaFin);
        }

        public List<managmentprestadoresfacturasgestionadas3Result> GetGestionFacturasv2(int? idDetalle, DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac)
        {
            return DACConsulta.GetGestionFacturasv2(idDetalle, fechainicial, fechafinal, estado, regional, prestador, nit, numFac);
        }


        public List<managmentprestadoresfacturasgestionadas2Result> GetGestionFacturasV3Filtrada(int? idDetalle, DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac, string rol, int? idUsuario)
        {
            return DACConsulta.GetGestionFacturasV3Filtrada(idDetalle, fechainicial, fechafinal, estado, regional, prestador, nit, numFac, rol, idUsuario);
        }


        public List<managmentprestadoresfacturasgestionadasCompletaResult> GetGestionFacturasv3(String numFac, String nit, String prestador, String sap, int? estado, int? idDetalle)
        {
            return DACConsulta.GetGestionFacturasv3(numFac, nit, prestador, sap, estado, idDetalle);
        }

        public List<managmentprestadoresfacturasgestionadasTrazabilidadResult> GetGestionFacturasTrazabilidad()
        {
            return DACConsulta.GetGestionFacturasTrazabilidad();
        }

        public List<managmentprestadoresfacturasgestionadasTrazabilidadResult> GetGestionFacturasTrazabilidadV2(DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac)
        {
            return DACConsulta.GetGestionFacturasTrazabilidadV2(fechainicial, fechafinal, estado, regional, prestador, nit, numFac);
        }


        public List<managmentprestadores_estados_factura_totalResult> GetTotalFacturas()
        {
            return DACConsulta.GetTotalFacturas();
        }

        public List<vw_ref_estado_factura_total_rango> GetRecepcionFacturasRango(Int32 opc)
        {
            return DACConsulta.GetRecepcionFacturasRango(opc);
        }

        public List<managmentprestadoresFacturasResult> GetFacturasByEstadoAceptadas(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByEstadoAceptadas(idestado, ref MsgRes);
        }

        public List<managmentprestadoresFacturas_devueltasResult> GetFacturasByEstadoDevueltas(int idestado, int? id, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByEstadoDevueltas(idestado, id, ref MsgRes);
        }
        public List<managmentprestadoresFacturas_rangoResult> GetFacturasByEstadoAceptadasRango(int rango, Int32 id_regional)
        {
            return DACConsulta.GetFacturasByEstadoAceptadasRango(rango, id_regional);
        }

        public List<managmentprestadoresFacturasAuditorResult> GetFacturasByAuditor(int idestado, int id_usuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByAuditor(idestado, id_usuario, ref MsgRes);
        }

        public List<managmentprestadoresFacturasAuditorOKResult> GetFacturasByAuditor2(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByAuditor2(ref MsgRes);
        }
        public List<managmentprestadoresFacturasAuditorOKCompletaResult> GetFacturasByAuditor3(int idAuditor)
        {
            return DACConsulta.GetFacturasByAuditor3(idAuditor);
        }

        public List<managmentprestadoresFacturasAuditorEnSubsanacionResult> GetFacturasByAuditorEnSubsanacion(int idAuditor)
        {
            return DACConsulta.GetFacturasByAuditorEnSubsanacion(idAuditor);
        }

        public List<managmentprestadoresFacturasAprobadasResult> GetFacturasAprobadas(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasAprobadas(idestado, ref MsgRes);
        }
        public List<managmentprestadoresFacturasReporteResult> GetFacturasByEstadoReporte(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByEstadoReporte(idestado, ref MsgRes);
        }

        public List<managmentprestadoresFacturasReporte_fisResult> GetFacturasByEstadoReporte_fis(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByEstadoReporte_fis(idestado, ref MsgRes);
        }

        public List<managmentRechazoFacturasReporteResult> GetFacturasByRechazoReporte(int id_dtll, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByRechazoReporte(id_dtll, ref MsgRes);
        }

        public List<managmentRechazoLoteFacturasReporteResult> GetLoteFacturasByRechazoReporte(int id_lote, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetLoteFacturasByRechazoReporte(id_lote, ref MsgRes);
        }

        public List<managmentRechazoLoteDtllFacturasReporteResult> GetLoteFacturasdtllByRechazoReporte(int id_lote, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetLoteFacturasdtllByRechazoReporte(id_lote, ref MsgRes);
        }

        public List<managment_prestadores_soportes_clinicosResult> GetSoportesClinicosList(int idcargue, int detalle)
        {
            return DACConsulta.GetSoportesClinicosList(idcargue, detalle);
        }

        public List<managment_prestadores_documentosResult> GetSoportesList(int detalle)
        {
            return DACConsulta.GetSoportesList(detalle);
        }

        public List<managment_ffmm_documentosResult> GetSoportesListFFMM(int detalle)
        {
            return DACConsulta.GetSoportesListFFMM(detalle);
        }
        public management_prestadores_get_soporteResult Getsoporteclinico(int idsoportee)
        {
            return DACConsulta.Getsoporteclinico(idsoportee);
        }

        public List<ref_rechazos_Fac> Getref_rechazos_Fac(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.Getref_rechazos_Fac(ref MsgRes);
        }
        public List<vw_auditores_totales> GetAuditorTotales(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetAuditorTotales(ref MsgRes);
        }

        public List<vw_auditores_totales_pqrs> GetAuditorTotalesPqrs(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetAuditorTotalesPqrs(ref MsgRes);
        }

        public List<managment_prestadores_list_rechazosResult> GetFacturasByRechazoList(int id_dtll, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasByRechazoList(id_dtll, ref MsgRes);
        }

        public void ActualizarEnvioPQRS(Int32 id_ecop_PQRS, String usuario, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEnvioPQRS(id_ecop_PQRS, usuario, ref MsgRes);
        }

        public ref_solucionador getSolucionadorNombre(string nombre, string auxsolucionador)
        {
            return DACConsulta.getSolucionadorNombre(nombre, auxsolucionador);
        }

        public ref_solucionador TraerAuxSolucionador(string nombreAux)
        {
            return DACConsulta.TraerAuxSolucionador(nombreAux);
        }

        public List<Ref_PQRS_correo_envio> ConsultaPQRSref_correo()
        {
            return DACConsulta.ConsultaPQRSref_correo();
        }

        public List<Ref_PQRS_categorizacion> ConsultaPQRSCategorizacion()
        {
            return DACConsulta.ConsultaPQRSCategorizacion();
        }

        public List<management_pqrs_tablero_controlResult> GestiontableroPQRS()
        {
            return DACConsulta.GestiontableroPQRS();
        }

        public ecop_PQRS_Auditor TraerRespuestaAuditor(int? idPqrs)
        {
            return DACConsulta.TraerRespuestaAuditor(idPqrs);
        }

        public List<management_pqrs_tablero_control_proyectadasFinalesResult> DatosTableroPqrsProyectadasFinales()
        {
            return DACConsulta.DatosTableroPqrsProyectadasFinales();
        }
        public List<management_pqrs_proyectadasCierreResult> DatosTableroPqrsProyectadasCierre()
        {
            return DACConsulta.DatosTableroPqrsProyectadasCierre();
        }
        public List<management_pqrs_tablero_control_proyectadasResult> GestiontableroPQRSProyectadas(string numCaso, string numOpc, string numDocumento, DateTime? fechaInicial, DateTime? fechaFinal, int? idPqr)
        {
            return DACConsulta.GestiontableroPQRSProyectadas(numCaso, numOpc, numDocumento, fechaInicial, fechaFinal, idPqr);
        }
        public List<management_pqrs_TableroseguimientoResult> GestiontableeroSeguimientoPqrs(string usuario, string solucionador, int? tipoBusqueda, int? idUsuario)
        {
            return DACConsulta.GestiontableeroSeguimientoPqrs(usuario, solucionador, tipoBusqueda, idUsuario);
        }

        public List<ref_solucionador> getSolucionador(int idCiudad)
        {
            return DACConsulta.getSolucionador(idCiudad);
        }

        public List<ref_solucionador> getSolucionadorReg(int idRegional)
        {
            return DACConsulta.getSolucionadorReg(idRegional);
        }

        public List<management_ref_solucionadorResult> getSolucionadorRegActivos(int idRegional)
        {
            return DACConsulta.getSolucionadorRegActivos(idRegional);
        }

        public List<ref_solucionador> getSolucionadorRegional(int? idRegional)
        {
            return DACConsulta.getSolucionadorRegional(idRegional);
        }

        public List<Ref_ciudades> TotalCiudades()
        {
            return DACConsulta.TotalCiudades();
        }

        public Ref_ciudades CiudadesId(int? id)
        {
            return DACConsulta.CiudadesId(id);
        }

        public List<ref_solucionador> getSolucionadorTotal()
        {
            return DACConsulta.getSolucionadorTotal();
        }
        public List<Management_PQRS_solucionadoresResult> getSolucionadorAux()
        {
            return DACConsulta.getSolucionadorAux();
        }
        public int ActualizarUsuarioAsignado(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarUsuarioAsignado(OBJ, ref MsgRes);
        }
        public int ActualizarCategorizacionPQR(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarCategorizacionPQR(OBJ, ref MsgRes);
        }
        public List<management_facturas_sinDocumentacionResult> ListaFacturasIncompletas()
        {
            return DACConsulta.ListaFacturasIncompletas();
        }

        public int ActualizarAvanzarProyectada(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarAvanzarProyectada(OBJ, ref MsgRes);
        }
        public int ActualizarCerrarProyectada(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarCerrarProyectada(OBJ, ref MsgRes);
        }
        public int ActualizarDatosReaperturaPQR(ecop_PQRS OBJ)
        {
            return DACActualiza.ActualizarDatosReaperturaPQR(OBJ);
        }
        public int InsertarLogReaperturaPqrs(log_pqrs_reaperturas obj)
        {
            return DACInserta.InsertarLogReaperturaPqrs(obj);
        }

        public int InsertarLogCierrePqrsSolucionador(log_pqrs_cerradasSolucionador obj)
        {
            return DACInserta.InsertarLogCierrePqrsSolucionador(obj);
        }

        public int CargueMedicamentosRegulados(ecop_pqrs_registroMasivo obj, List<ecop_PQRS> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueMedicamentosRegulados(obj, detalle, ref MsgRes);
        }
        public List<ecop_PQRS> ListadoPqrsMasivo(int idMasivo)
        {
            return DACConsulta.ListadoPqrsMasivo(idMasivo);
        }

        public int ActualizarAnalistaAsignadoPqr(ecop_PQRS obj)
        {
            return DACActualiza.ActualizarAnalistaAsignadoPqr(obj);
        }
        public int PqrGuardarObservaciopnAuditor(ecop_pqrs_observacionesAuditor obj)
        {
            return DACInserta.PqrGuardarObservaciopnAuditor(obj);
        }
        public List<management_pqrs_observacionesAuditorResult> PqrsListaObservacionesAuditor(int idPqrs)
        {
            return DACConsulta.PqrsListaObservacionesAuditor(idPqrs);
        }
        public int CargueMasivoQuienLlamoPqrs(List<ecop_pqrs_a_quien_llamo> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueMasivoQuienLlamoPqrs(detalle, ref MsgRes);
        }

        public int EiminarQuienesLlamoGestion(int? idPqr)
        {
            return DACElimina.EiminarQuienesLlamoGestion(idPqr);
        }

        public int CargueMasivoAuditores(List<ecop_pqrs_auditores> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueMasivoAuditores(detalle, ref MsgRes);
        }

        public int CargueMasivoPrestadores(List<ecop_pqrs_prestadores> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueMasivoPrestadores(detalle, ref MsgRes);
        }

        public List<management_pqrs_consolidadoMasivoResult> PqrsListaMasivos(int? idUsuario)
        {
            return DACConsulta.PqrsListaMasivos(idUsuario);
        }

        public List<management_pqrs_consolidadoMasivo_detalleResult> PqrsListaMasivosDetalle(int? idMasivo, int? idUsuario)
        {
            return DACConsulta.PqrsListaMasivosDetalle(idMasivo, idUsuario);
        }

        public List<management_pqrs_sinArchivoInicialResult> listadoPqrsInicialSinArchivo(int? idUsuario)
        {
            return DACConsulta.listadoPqrsInicialSinArchivo(idUsuario);
        }

        public int insertarDatosCorreos(ecop_pqrs_envioCorreos obj)
        {
            return DACInserta.insertarDatosCorreos(obj);
        }
        public ecop_pqrs_envioCorreos LlamarPqrsCorreoById(int id)
        {
            return DACConsulta.LlamarPqrsCorreoById(id);
        }
        public int ActualizarPasaArchivoPqrinicial(ecop_PQRS obj)
        {
            return DACActualiza.ActualizarPasaArchivoPqrinicial(obj);
        }

        public int CerrarCasoPqrSolucionador(ecop_PQRS obj)
        {
            return DACActualiza.CerrarCasoPqrSolucionador(obj);
        }

        public ecop_PQRS buscarNumeroCasoPqrs(string numero_caso)
        {
            return DACConsulta.buscarNumeroCasoPqrs(numero_caso);
        }


        public management_devolverFechaHabil_diasResult DevolverDiasHabiles(DateTime? fecha, int? tipoSolicitud)
        {
            return DACConsulta.DevolverDiasHabiles(fecha, tipoSolicitud);
        }

        public management_pqrs_detalleCasoResult DetallePqrsReporteCorreo(int? idPqr)
        {
            return DACConsulta.DetallePqrsReporteCorreo(idPqr);
        }

        public List<management_pqrs_PorcentajeAuditoresResult> listadoPQRSAuditorPorcentaje(string auditor)
        {
            return DACConsulta.listadoPQRSAuditorPorcentaje(auditor);
        }

        public vw_auditores_totales GetAuditorNombre(string nombre)
        {
            return DACConsulta.GetAuditorNombre(nombre);
        }

        public vw_auditores_totales GetAuditorId(int? id)
        {
            return DACConsulta.GetAuditorId(id);
        }

        public vw_auditores_totales_pqrs GetAuditorNombrePqrs(string nombre)
        {
            return DACConsulta.GetAuditorNombrePqrs(nombre);
        }

        #endregion

        #region Gestion dOCUMENTAL

        public List<Ref_procesos> GetProcesosGD()
        {
            return DACComonClass.GetProcesosGD();
        }

        public List<Ref_gestion_tipo_documental> ConsultaGestionTipoDocumental(Int32 idproceso)
        {
            return DACConsulta.ConsultaGestionTipoDocumental(idproceso);
        }


        public vw_md_consolidado_fac MD_CosolidadofAC(String numero_factura)
        {
            return DACConsulta.MD_CosolidadofAC(numero_factura);
        }

        public List<vw_md_consolidado_fac> MD_CosolidadofACDetalle(String numero_factura)
        {
            return DACConsulta.MD_CosolidadofACDetalle(numero_factura);
        }

        public List<vw_md_consolidado_fac> MD_CosolidadofAC2(String factura)
        {
            return DACConsulta.MD_CosolidadofAC2(factura);
        }

        public List<Ref_gestion_tipo_documental> ConsultaCodigoGD(Ref_gestion_tipo_documental objBusqueda, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCodigoGD(objBusqueda, ref MsgRes);
        }

        public Int32 InsertarGestionDoc(GestionDocumentalMedicamentos ObjobjGD, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGestionDoc(ObjobjGD, ref MsgRes);
        }

        public Int32 InsertarGestionDocMedCalidad(GestionDocumentalMedicamentosCad ObjobjGD, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGestionDocMedCalidad(ObjobjGD, ref MsgRes);
        }

        public void InsertarGestionDocPQRS(GestionDocumentalPQRS Obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarGestionDocPQRS(Obj, ref MsgRes);
        }

        public void InsertarGestionDocVisitasCalidad(GestionDocumentalVisitasCalidad Obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarGestionDocVisitasCalidad(Obj, ref MsgRes);
        }

        public List<vw_g_documental_medicamentos> ConsultaFactura(String FacMedicamentos)
        {
            return DACConsulta.ConsultaFactura(FacMedicamentos);
        }

        public List<vw_g_documental_medicamentos> ConsultaDocumento(Decimal DocMedicamentos)
        {
            return DACConsulta.ConsultaDocumento(DocMedicamentos);
        }

        public List<vw_fac_consolidado> ConsultaFactura2(String FacMedicamentos)
        {
            return DACConsulta.ConsultaFactura2(FacMedicamentos);
        }

        public List<vw_fac_consolidado> ConsultaDocumento2(String DocMedicamentos)
        {
            return DACConsulta.ConsultaDocumento2(DocMedicamentos);
        }

        public vw_g_documental_medicamentos ConsultaIdGestionDocumental(Int32 id_gestion_documental, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdGestionDocumental(id_gestion_documental, ref MsgRes);
        }

        public List<vw_g_documental_medicamentos> ConsultaIdGestionDocumental2(Int32 id_gestion_documental, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdGestionDocumental2(id_gestion_documental, ref MsgRes);
        }

        public List<vw_g_documental_medicamentos> ConsultaIdGestionDocumentalFormula(String formula, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdGestionDocumentalFormula(formula, ref MsgRes);
        }
        public List<vw_gestion_documental_med_cad_dtll> ConsultaIdGestionDocumentalMDCalidad(Int32 id_indicadores_medicamentos, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdGestionDocumentalMDCalidad(id_indicadores_medicamentos, ref MsgRes);
        }

        public ecop_PQRS ConsultaPQRSbyNumCaso(string numcaso)
        {
            return DACConsulta.ConsultaPQRSbyNumCaso(numcaso);
        }

        public GestionDocumentalPQRS ConsultaGestorPQRSbyId(Int32 Id)
        {
            return DACConsulta.ConsultaGestorPQRSbyId(Id);
        }


        public List<GestionDocumentalPQRS> ConsultanumcasoGestionDocumentalPQRS(string numcaso)
        {
            return DACConsulta.ConsultanumcasoGestionDocumentalPQRS(numcaso);
        }

        public void EliminarDocumento_med_cal(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarDocumento_med_cal(id, ref MsgRes);
        }

        public void EliminarDocumento_med(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarDocumento_med(id, ref MsgRes);
        }

        public bool EliminarDocPQRS(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            return DACElimina.EliminarDocPQRS(id, ref MsgRes);
        }

        public void InsertarLogActividad(Log_GestionDocumental Log)
        {
            DACInserta.InsertarLogActividad(Log);
        }

        public List<GestionDocumentalMedicamentos> TraerPdf()
        {
            return DACComonClass.TraerPdf();
        }

        public String ActualizarRutaByteMed(GestionDocumentalMedicamentos obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarRutaByteMed(obj, ref MsgRes);
        }

        public String ActualizarRutasDocsVisitas(cronograma_visita_documento obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarRutasDocsVisitas(obj, ref MsgRes);
        }

        public void ActualizarRutaDocumentoVisitasCronograma(string ruta, int? idVisita)
        {
            DACActualiza.ActualizarRutaDocumentoVisitasCronograma(ruta, idVisita);
        }

        public String ActualizarRutaBytePQRS(GestionDocumentalPQRS2 obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarRutaBytePQRS(obj, ref MsgRes);
        }
        public int insertarConteoAnalistaPQR(int idAnalista, int idUsuario)
        {
            return DACInserta.insertarConteoAnalistaPQR(idAnalista, idUsuario);
        }
        public String ActualizarRutaByteMedCalidad(GestionDocumentalMedicamentosCad obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarRutaByteMedCalidad(obj, ref MsgRes);
        }

        public List<GestionDocumentalMedicamentos> ConsultaGestionMedCargue()
        {
            return DACConsulta.ConsultaGestionMedCargue();
        }
        public List<vw_g_documental_medicamentos_masivo> GestionDocumentalmasivo()
        {
            return DACConsulta.GestionDocumentalmasivo();
        }
        public List<management_masivo_pqrsResult> GestionDocumentalmasivo2()
        {
            return DACConsulta.GestionDocumentalmasivo2();
        }
        public List<md_Ref_com_dirigido> GetDirigido()
        {
            return DACComonClass.GetDirigido();
        }

        public List<md_Ref_com_tipo> GetMdTipo()
        {
            return DACComonClass.GetMdTipo();
        }

        public List<md_ref_tipo_visita> GetMdTipoVisita()
        {
            return DACComonClass.GetMdTipoVisita();
        }

        public void InsertarComunicaciones(md_comunicaciones OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarComunicaciones(OBJ, ref MsgRes);
        }

        public void InsertarCronoVisitas(md_crono_visita OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCronoVisitas(OBJ, ref MsgRes);
        }

        public List<ManagmentRefPuntosDispersacionResult> ConsultaListaPD(int opc, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListaPD(opc, ref MsgRes);
        }

        public vw_gestion_documental_med_cad_dtll ConsultaIdGestionDocumentalMDCalId(Int32 id_gestion_documental__medicamentos_cad, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdGestionDocumentalMDCalId(id_gestion_documental__medicamentos_cad, ref MsgRes);
        }

        #endregion

        #region MEDICAMENTOS

        public List<ManagmentFacMedicamentosResult> CuentaFacMedicamentos(String factura, String formula, Int32 OPC, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentaFacMedicamentos(factura, formula, OPC, ref MsgRes);
        }

        public List<Managment_md_tablerocontrolResult> CuentaFacTableroControl(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentaFacTableroControl(fecha_inicial, fecha_final, ref MsgRes);
        }

        public List<Managment_md_tablero_ConciliacionesResult> CuentaFacTableroControlConciliaciones(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentaFacTableroControlConciliaciones(ref MsgRes);
        }

        public List<Managment_md_tablero_Conciliaciones_detalleResult> CuentaFacTableroControlConciliacionesdtll(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentaFacTableroControlConciliacionesdtll(ref MsgRes);
        }

        public List<ManagmentFacMedicamentosDetalleResult> CuentaFacMedicamentosDetalle(String factura, String formula, Int32 OPC, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentaFacMedicamentosDetalle(factura, formula, OPC, ref MsgRes);
        }

        public List<md_Ref_resultado_auditoria> GetResultadoAUD()
        {
            return DACComonClass.GetResultadoAUD();
        }

        public Int32 InsertarGlosaMD(md_glosa OBJGlosa, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGlosaMD(OBJGlosa, ref MsgRes);
        }

        public Int32 InsertarGlosaDetalleMD(md_glosa_detalle OBJGlosaDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGlosaDetalleMD(OBJGlosaDetalle, ref MsgRes);
        }

        public List<vw_glosa_medicamentos> ConsultaGlosa(String formula)
        {
            return DACConsulta.ConsultaGlosa(formula);
        }

        public void EliminarGlosa(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarGlosa(id, ref MsgRes);
        }

        public Int32 InsertarIndicador(md_indicadores OBJIndicadores, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarIndicador(OBJIndicadores, ref MsgRes);
        }

        public List<md_indicadores_detalle> GetIndicadoresDetalle(Int32 id_indicadores_medicamentos)
        {
            return DACConsulta.GetIndicadoresDetalle(id_indicadores_medicamentos);
        }

        public Int32 InsertarIndicadorDetalle(md_indicadores_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarIndicadorDetalle(OBJDetalle, ref MsgRes);
        }

        public void ActualizarIndicadoresMedicamentos(md_indicadores_detalle OBJIndicadoresMD, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarIndicadoresMedicamentos(OBJIndicadoresMD, ref MsgRes);
        }

        public List<md_indicadores_detalle> GetIndicadoresDetalleID(Int32 id_md_ref_indicador, Int32 id_indicadores_medicamentos)
        {
            return DACConsulta.GetIndicadoresDetalleID(id_md_ref_indicador, id_indicadores_medicamentos);
        }

        public List<vw_indicador_detalle_sin_cumplir> GetIndicadoresSinCumplir(Int32 id_indicadores_medicamentos)
        {
            return DACConsulta.GetIndicadoresSinCumplir(id_indicadores_medicamentos);
        }

        public List<Managment_md_Ref_indicadorResult> DetalleRefIndicadores(Int32 id_indicadores_medicamentos, Int32 opc)
        {
            return DACConsulta.DetalleRefIndicadores(id_indicadores_medicamentos, opc);
        }

        public List<ManagmentReporIndicadorMDResult> ReporteIndicadores(Int32 id_indicadores_medicamentos)
        {
            return DACConsulta.ReporteIndicadores(id_indicadores_medicamentos);
        }

        public void ActualizarIndicadoresMD(md_indicadores OBJIndicadoresMD, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarIndicadoresMD(OBJIndicadoresMD, ref MsgRes);
        }

        public vw_total_md_detalle Total_DetalleIndMD(Int32 id_indicadores_medicamentos)
        {
            return DACConsulta.Total_DetalleIndMD(id_indicadores_medicamentos);

        }

        public List<vw_table_gestion_MD> ConsultaGestionMd()
        {
            return DACConsulta.ConsultaGestionMd();
        }

        public List<md_Ref_tipo_hallazgo> TipoHallazgo()
        {
            return DACConsulta.TipoHallazgo();
        }

        public Int32 InsertarIndicadorGestion(md_indicadores_gestion OBJGestion, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarIndicadorGestion(OBJGestion, ref MsgRes);
        }

        public void ActualizarIndicadoresMDEstado(md_indicadores OBJIndicadoresMD, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarIndicadoresMDEstado(OBJIndicadoresMD, ref MsgRes);
        }

        public List<md_Ref_consultas> GetRefConsulta()
        {
            return DACComonClass.GetRefConsulta();
        }

        public List<Managment_md_ConsultasResult> CuentaConsultasMedicamentos(Int32 opc, DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentaConsultasMedicamentos(opc, fecha_inicial, fecha_final, ref MsgRes);
        }

        public List<md_Ref_proveedor> GetMD_Ref_proveedor()
        {
            return DACComonClass.GetMD_Ref_proveedor();
        }

        public IEnumerable<vw_md_Ref_indicador_datalle> GetVwIndicadoresDetalle(Int32 id_indicadores_medicamentos)
        {
            return DACConsulta.GetVwIndicadoresDetalle(id_indicadores_medicamentos);
        }

        public List<md_ref_puntos_dispensacion> PuntosDispercion()
        {
            return DACConsulta.PuntosDispercion();
        }

        public List<vw_indicadores_medicamentos> IndicadoresProvvedor(String Proveeedor)
        {
            return DACConsulta.IndicadoresProvvedor(Proveeedor);
        }

        public List<vw_obligaciones_contractuales> ObligacionesProveedor(String Proveedor)
        {
            return DACConsulta.ObligacionesProveedor(Proveedor);
        }

        public Int32 InsertarObligaciones(md_obligaciones_contractuales OBJObligacionesContractuales, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarObligaciones(OBJObligacionesContractuales, ref MsgRes);
        }

        public List<Managment_md_Ref_obligacionesResult> DetalleRefObligaciones(Int32 id_obligaciones_contractuales, Int32 opc)
        {
            return DACConsulta.DetalleRefObligaciones(id_obligaciones_contractuales, opc);
        }

        public Int32 InsertarObligacionesDetalle(md_obligaciones_contractuales_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarObligacionesDetalle(OBJDetalle, ref MsgRes);
        }

        public vw_total_md_obligaciones_detalle Total_DetalleObligacionesMD(Int32 id_obligaciones_contractuales)
        {
            return DACConsulta.Total_DetalleObligacionesMD(id_obligaciones_contractuales);

        }

        public void ActualizarObligacionesMD(md_obligaciones_contractuales OBJObligacionesContractuales, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarObligacionesMD(OBJObligacionesContractuales, ref MsgRes);

        }


        public void ActualizarObligacionesDetalleMD(md_obligaciones_contractuales_detalle OBJObligacionesContractualesDetalle, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarObligacionesDetalleMD(OBJObligacionesContractualesDetalle, ref MsgRes);

        }

        public List<md_obligaciones_contractuales_detalle> GetObligacionesDetalleID(Int32 id_md_ref_obligaciones, Int32 id_obligaciones_contractuales)
        {
            return DACConsulta.GetObligacionesDetalleID(id_md_ref_obligaciones, id_obligaciones_contractuales);
        }



        public Int32 InsertarHerramientaTecnologica(md_herramienta_tec OBJHerramienta, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarHerramientaTecnologica(OBJHerramienta, ref MsgRes);
        }

        public Int32 InsertarDetallet1(List<md_herramienta_tec_detalle_t1> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDetallet1(OBJDetalle, ref MsgRes);

        }

        public Int32 InsertarDetallet2(List<md_herramienta_tec_detalle_t2> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDetallet2(OBJDetalle, ref MsgRes);
        }


        public Int32 InsertarDetallet3(List<md_herramienta_tec_detalle_t3> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDetallet3(OBJDetalle, ref MsgRes);

        }


        public Int32 InsertarDetallet4(List<md_herramienta_tec_detalle_t4> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDetallet4(OBJDetalle, ref MsgRes);
        }


        public List<vw_herramientas_tecnologicas> IndicadoresProvvedorHerramientas(Int32 Proveeedor)
        {
            return DACConsulta.IndicadoresProvvedorHerramientas(Proveeedor);
        }

        public List<md_ref_tabla1> ref_tabla1()
        {
            return DACConsulta.ref_tabla1();
        }

        public List<vw_md_crono> ConsultaCronograma()
        {
            return DACConsulta.ConsultaCronograma();
        }

        public List<vw_md_crono_auditores> GetUsuarioCronoId(String usuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetUsuarioCronoId(usuario, ref MsgRes);
        }

        public List<md_ref_puntos_dispensacion> GetPuntosDispensacion()
        {
            return DACComonClass.GetPuntosDispensacion();
        }
        public List<md_ref_puntos_control> GetpuntoControl()
        {
            return DACConsulta.GetpuntoControl();
        }
        public List<Managment_md_Ref_crono_auditoresResult> ConsultaListaCronoAuditores(int opc1, Int32? opc2, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListaCronoAuditores(opc1, opc2, ref MsgRes);
        }

        public Int32 InsertarInterventoriaGeneral(md_interventoria_general OBJInterventoriaGeneral, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarInterventoriaGeneral(OBJInterventoriaGeneral, ref MsgRes);
        }

        public List<Managment_md_Ref_General1Result> DetalleRefInterventoriaGeneral1(Int32 id_md_interventoria_general, Int32 opc)
        {
            return DACConsulta.DetalleRefInterventoriaGeneral1(id_md_interventoria_general, opc);
        }

        public List<Managment_md_Ref_General2Result> DetalleRefInterventoriaGeneral2(Int32 id_md_interventoria_general, Int32 opc)
        {
            return DACConsulta.DetalleRefInterventoriaGeneral2(id_md_interventoria_general, opc);
        }

        public List<Managment_md_Ref_General3Result> DetalleRefInterventoriaGeneral3(Int32 id_md_interventoria_general, Int32 opc)
        {
            return DACConsulta.DetalleRefInterventoriaGeneral3(id_md_interventoria_general, opc);
        }

        public List<Managment_md_Ref_General4Result> DetalleRefInterventoriaGeneral4(Int32 id_md_interventoria_general, Int32 opc)
        {
            return DACConsulta.DetalleRefInterventoriaGeneral4(id_md_interventoria_general, opc);
        }

        public Int32 InsertarGeneral1Detalle(md_interventoria_general_detalle1 OBJDetallleG1, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGeneral1Detalle(OBJDetallleG1, ref MsgRes);
        }

        public Int32 InsertarGeneral2Detalle(md_interventoria_general_detalle2 OBJDetallleG2, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGeneral2Detalle(OBJDetallleG2, ref MsgRes);
        }


        public Int32 InsertarGeneral3Detalle(md_interventoria_general_detalle3 OBJDetallleG3, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGeneral3Detalle(OBJDetallleG3, ref MsgRes);
        }

        public Int32 InsertarGeneral4Detalle(md_interventoria_general_detalle4 OBJDetallleG4, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGeneral4Detalle(OBJDetallleG4, ref MsgRes);
        }

        public List<md_ref_tabla2> ref_tabla2()
        {
            return DACConsulta.ref_tabla2();
        }
        public List<md_ref_tabla3> ref_tabla3()
        {
            return DACConsulta.ref_tabla3();
        }
        public List<md_ref_tabla4> ref_tabla4()
        {
            return DACConsulta.ref_tabla4();
        }
        public List<vw_tabla1_categ> Tabla1Catg()
        {
            return DACConsulta.Tabla1Catg();
        }

        public List<vw_md_detalle_T1> Tabla1Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            return DACConsulta.Tabla1Detalle(id_cat, id_herramienta_tec);
        }
        public List<vw_md_detalle_T2> Tabla2Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            return DACConsulta.Tabla2Detalle(id_cat, id_herramienta_tec);
        }
        public List<vw_md_detalle_T3> Tabla3Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            return DACConsulta.Tabla3Detalle(id_cat, id_herramienta_tec);
        }
        public List<vw_md_detalle_T4> Tabla4Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            return DACConsulta.Tabla4Detalle(id_cat, id_herramienta_tec);
        }

        public vw_md_total_T1 totalesT1(Int32 id)
        {
            return DACConsulta.totalesT1(id);
        }
        public vw_md_total_T2 totalesT2(Int32 id)
        {
            return DACConsulta.totalesT2(id);
        }
        public vw_md_total_T3 totalesT3(Int32 id)
        {
            return DACConsulta.totalesT3(id);
        }
        public vw_md_total_T4 totalesT4(Int32 id)
        {
            return DACConsulta.totalesT4(id);
        }


        public void ActualizarDetallet1(md_herramienta_tec_detalle_t1 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarDetallet1(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarDetallet2(md_herramienta_tec_detalle_t2 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarDetallet2(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarDetallet3(md_herramienta_tec_detalle_t3 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarDetallet3(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarDetallet4(md_herramienta_tec_detalle_t4 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarDetallet4(OBJDetalleT, ref MsgRes);
        }


        public void ActualizarGeneral1(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarGeneral1(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarGeneral2(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarGeneral2(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarGeneral3(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarGeneral3(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarGeneral4(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarGeneral4(OBJDetalleT, ref MsgRes);
        }


        public vw_total_md_interventoria_detalle Total_DetalleInterventoriaGeneralMD(Int32 id_md_interventoria_general)
        {
            return DACConsulta.Total_DetalleInterventoriaGeneralMD(id_md_interventoria_general);

        }

        public void ActualizarInterventoriaGeneralMD(md_interventoria_general OBJInterventoriaGeneral, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarInterventoriaGeneralMD(OBJInterventoriaGeneral, ref MsgRes);

        }

        public List<md_interventoria_general_detalle1> GetInterventoriaDetalle1ID(Int32 id_md_ref_inte_general1, Int32 id_md_interventoria_general)
        {
            return DACConsulta.GetInterventoriaDetalle1ID(id_md_ref_inte_general1, id_md_interventoria_general);
        }

        public List<md_interventoria_general_detalle2> GetInterventoriaDetalle2ID(Int32 id_md_ref_inte_general2, Int32 id_md_interventoria_general)
        {
            return DACConsulta.GetInterventoriaDetalle2ID(id_md_ref_inte_general2, id_md_interventoria_general);
        }

        public List<md_interventoria_general_detalle3> GetInterventoriaDetalle3ID(Int32 id_md_ref_inte_general3, Int32 id_md_interventoria_general)
        {
            return DACConsulta.GetInterventoriaDetalle3ID(id_md_ref_inte_general3, id_md_interventoria_general);
        }

        public List<md_interventoria_general_detalle4> GetInterventoriaDetalle4ID(Int32 id_md_ref_inte_general4, Int32 id_md_interventoria_general)
        {
            return DACConsulta.GetInterventoriaDetalle4ID(id_md_ref_inte_general4, id_md_interventoria_general);
        }


        public void ActualizarInterventoriaGeneralDetalle1MD(md_interventoria_general_detalle1 OBJDetallleG1, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarInterventoriaGeneralDetalle1MD(OBJDetallleG1, ref MsgRes);

        }

        public void ActualizarInterventoriaGeneralDetalle2MD(md_interventoria_general_detalle2 OBJDetallleG2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarInterventoriaGeneralDetalle2MD(OBJDetallleG2, ref MsgRes);

        }
        public void ActualizarInterventoriaGeneralDetalle3MD(md_interventoria_general_detalle3 OBJDetallleG3, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarInterventoriaGeneralDetalle3MD(OBJDetallleG3, ref MsgRes);

        }
        public void ActualizarInterventoriaGeneralDetalle4MD(md_interventoria_general_detalle4 OBJDetallleG4, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarInterventoriaGeneralDetalle4MD(OBJDetallleG4, ref MsgRes);

        }

        public List<vw_md_interventoria_general> InterventoriaGeneralProveedor(String Proveedor)
        {
            return DACConsulta.InterventoriaGeneralProveedor(Proveedor);
        }
        public Int32 InsertarCargueCuentasMd(md_cargue_cuentas OBJCargueCuentas, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCargueCuentasMd(OBJCargueCuentas, ref MsgRes);
        }

        public Int32 InsertarSeguimientoPendientes(md_seguimiento_pendientes OBJSeguimientoPendientes, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarSeguimientoPendientes(OBJSeguimientoPendientes, ref MsgRes);
        }

        public Int32? InsertarSeguimientoPendientesDetalle(md_seguimiento_pendientes_detalle OBJSeguimientoPendientesDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarSeguimientoPendientesDetalle(OBJSeguimientoPendientesDetalle, ref MsgRes);
        }

        public List<Managment_md_Ref_seguimiento_pendientesResult> DetalleRefSeguimientoPendientes(Int32 id_md_seguimiento_pendientes, Int32 opc)
        {
            return DACConsulta.DetalleRefSeguimientoPendientes(id_md_seguimiento_pendientes, opc);
        }


        public vw_total_md_seguimiento_detalle Total_DetalleSeguimientoPendientesMD(Int32 id_md_seguimiento_pendientes)
        {
            return DACConsulta.Total_DetalleSeguimientoPendientesMD(id_md_seguimiento_pendientes);

        }

        public void ActualizarSeguimientoPendientesMD(md_seguimiento_pendientes OBJSeguimientoPendientes, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarSeguimientoPendientesMD(OBJSeguimientoPendientes, ref MsgRes);

        }

        public List<md_seguimiento_pendientes_detalle> GetSeguimientoPendientesDetalleID(Int32 id_md_ref_seguimiento_pendientes, Int32 id_md_seguimiento_pendientes)
        {
            return DACConsulta.GetSeguimientoPendientesDetalleID(id_md_ref_seguimiento_pendientes, id_md_seguimiento_pendientes);
        }

        public void ActualizarSeguimientoPendientesDetalleMD(md_seguimiento_pendientes_detalle OBJSeguimientoPendientesDetalle, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarSeguimientoPendientesDetalleMD(OBJSeguimientoPendientesDetalle, ref MsgRes);

        }


        public List<vw_md_seguimiento_pendientes> SeguimientoPendientesProveedor(String Proveedor)
        {
            return DACConsulta.SeguimientoPendientesProveedor(Proveedor);
        }

        public Int32 InsertarConsolidadoFacturacion(List<md_consolidado_facturacion> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarConsolidadoFacturacion(OBJDetalle, ref MsgRes);
        }

        public List<vw_gestionDocumentalCad> GestionDocumentalMedCalidad(Int32 id, Int32 id2)
        {
            return DACConsulta.GestionDocumentalMedCalidad(id, id2);
        }

        public Int32 InsertarHerramientaGestion(md_herramienta_tec_gestion OBJGestion, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarHerramientaGestion(OBJGestion, ref MsgRes);

        }

        public List<vw__md_herramientaT_sin_cumplir> GetHerramientasSincumplir(Int32 id_herramienta_tec_med, Int32 tabla)
        {
            return DACConsulta.GetHerramientasSincumplir(id_herramienta_tec_med, tabla);
        }


        public List<ManagmentReportNotifiAuditoriaResult> ReportNotificacionAudi(Int32 id)
        {
            return DACConsulta.ReportNotificacionAudi(id);

        }


        public Int32 Insertardispensacion_ambulatoria(md_dispensacion_ambulatoria OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.Insertardispensacion_ambulatoria(OBJDetalle, ref MsgRes);
        }

        public Int32 Insertardispensacion_Hospitalaria(md_dispensacion_hospitalaria OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.Insertardispensacion_Hospitalaria(OBJDetalle, ref MsgRes);
        }

        public Int32 Insertardispensacion_ambulatoriaDetalle(md_dispensacion_ambulatoria_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.Insertardispensacion_ambulatoriaDetalle(OBJDetalle, ref MsgRes);

        }

        public Int32 Insertardispensacion_HospitalariaDetalle(md_dispensacion_hospitalaria_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.Insertardispensacion_HospitalariaDetalle(OBJDetalle, ref MsgRes);
        }

        public List<md_ref_dispens_ambulatoria> RefDispersacionAmbulatoria()
        {
            return DACConsulta.RefDispersacionAmbulatoria();
        }

        public List<md_ref_dispens_hospitalaria> RefDispersacionHospitalaria()
        {
            return DACConsulta.RefDispersacionHospitalaria();
        }
        public List<Managment_md_Ref_ambulatorioResult> DetalleRefAmbulatorio(Int32 id_md_dispensacion_ambulatoria)
        {
            return DACConsulta.DetalleRefAmbulatorio(id_md_dispensacion_ambulatoria);
        }

        public List<Managment_md_Ref_hospitalarioResult> DetalleRefHospitalario(Int32 id_md_dispensacion_Hospitalaria)
        {
            return DACConsulta.DetalleRefHospitalario(id_md_dispensacion_Hospitalaria);
        }

        public List<md_dispensacion_ambulatoria_detalle> GetAmbulatorioDetalleID(Int32 id_md_ref_dispens_ambulatoria, Int32 id_md_dispensacion_ambulatoria)
        {
            return DACConsulta.GetAmbulatorioDetalleID(id_md_ref_dispens_ambulatoria, id_md_dispensacion_ambulatoria);
        }

        public List<md_dispensacion_hospitalaria_detalle> GetHospitalarioDetalleID(Int32 id_md_ref_dispens_hospitalaria, Int32 id_md_dispensacion_hospitalaria)
        {
            return DACConsulta.GetHospitalarioDetalleID(id_md_ref_dispens_hospitalaria, id_md_dispensacion_hospitalaria);
        }


        public void ActualizarDispersacionAmbulatorio(md_dispensacion_ambulatoria_detalle OBJMD, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarDispersacionAmbulatorio(OBJMD, ref MsgRes);
        }

        public void ActualizarDispersacionHospitalizacion(md_dispensacion_hospitalaria_detalle OBJMD, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarDispersacionHospitalizacion(OBJMD, ref MsgRes);
        }

        public List<vw_dispensacion_ambulatoria> AmbulatorioProvvedor(String Proveeedor)
        {
            return DACConsulta.AmbulatorioProvvedor(Proveeedor);
        }

        public List<vw_dispensacion_hospitalaria> hospitalarioProvvedor(String Proveeedor)
        {
            return DACConsulta.hospitalarioProvvedor(Proveeedor);
        }

        public void ActualizarAmbulatoriaMD(md_dispensacion_ambulatoria OBJMD, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarAmbulatoriaMD(OBJMD, ref MsgRes);
        }

        public void ActualizarHospitalariaMD(md_dispensacion_hospitalaria OBJMD, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarHospitalariaMD(OBJMD, ref MsgRes);
        }

        public md_control_gastos control_gastosMes(Int32 mes, String año)
        {
            return DACConsulta.control_gastosMes(mes, año);

        }
        public Int32 Insertarcontrol_gasto(md_control_gastos OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.Insertarcontrol_gasto(OBJDetalle, ref MsgRes);
        }

        public void ActualizarControlGastos(md_control_gastos OBJMD, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarControlGastos(OBJMD, ref MsgRes);
        }

        public vw_md_control_gasto control_gastosTotal(Int32 mes)
        {
            return DACConsulta.control_gastosTotal(mes);
        }

        public List<Managment_md_control_gastos_tableroResult> tableroControlGastos(int opc, int año)
        {
            return DACConsulta.tableroControlGastos(opc, año);
        }

        public List<Managment_md_control_gastos_tablero2Result> tableroControlGastos2(int opc, int año)
        {
            return DACConsulta.tableroControlGastos2(opc, año);
        }



        public vw_md_total_ambulatoria Total_DetalleAmbulatoria(Int32 id_md_dispensacion_ambulatoria)
        {
            return DACConsulta.Total_DetalleAmbulatoria(id_md_dispensacion_ambulatoria);
        }

        public vw_md_total_hospitalaria Total_DetalleHospitalaria(Int32 id_md_dispensacion_hospitalaria)
        {
            return DACConsulta.Total_DetalleHospitalaria(id_md_dispensacion_hospitalaria);
        }

        /*Alexis Quiñones 4-dic-2019*/

        //public void CarguePrefacturas(md_prefacturas_cargue_base carguebase, List<md_prefacturas_detalle> carguedetalle, ref MessageResponseOBJ MsgRes)
        //{
        //    DACInserta.CarguePrefacturas(carguebase, carguedetalle, ref MsgRes);
        //}

        //kevin suarez 16-03-22


        public int CarguePrefacturaBase(md_prefacturas_cargue_base carguebase, List<md_prefacturas_detalle> listaDetalle)
        {
            return DACInserta.CarguePrefacturaBase(carguebase, listaDetalle);
        }
        public int CarguePrefacturaLista(List<md_prefacturas_detalle> listadoCargue)
        {
            return DACInserta.CarguePrefacturaLista(listadoCargue);
        }


        public int CargueLUPEBase(md_Lupe_cargue_base carguebase, List<md_lupe_cargue_base_detalle> listadoCargue)
        {
            return DACInserta.CargueLUPEBase(carguebase, listadoCargue);
        }
        public int ActualizarVigenciaHastaLupe(md_Lupe_cargue_base obj)
        {
            return DACActualiza.ActualizarVigenciaHastaLupe(obj);
        }

        public List<management_listadoPrefacturasCruzanResult> listadoSiCruzanPrefacturasLupe(int idBase)
        {
            return DACConsulta.listadoSiCruzanPrefacturasLupe(idBase);
        }

        public List<management_listadoPrefacturasNoCruzanResult> listadoNoCruzanPrefacturasLupe(int idBase)
        {
            return DACConsulta.listadoNoCruzanPrefacturasLupe(idBase);
        }

        public List<management_lupe_carguesResult> listadoCargueLupe()
        {
            return DACConsulta.listadoCargueLupe();
        }
        public List<management_lupe_cargues_intermediacionesResult> listadoCargueLupeIntermediaciones(int idLupe)
        {
            return DACConsulta.listadoCargueLupeIntermediaciones(idLupe);
        }
        public int EliminarLupe(int idLupe, string usuarioElimina)
        {
            return DACElimina.EliminarLupe(idLupe, usuarioElimina);
        }
        public int EliminarMedicamentosRegulados(int idMed, string usuarioElimina)
        {
            return DACElimina.EliminarMedicamentosRegulados(idMed, usuarioElimina);
        }
        public md_Lupe_cargue_base UltimoCargueLupe(int? idPrestador)
        {
            return DACConsulta.UltimoCargueLupe(idPrestador);
        }
        public int CargueLUPELista(List<md_lupe_cargue_base_detalle> listadoCargue, int id_cargueBase)
        {
            return DACInserta.CargueLUPELista(listadoCargue, id_cargueBase);
        }
        public int CargueLUPEListaReaprobacion(List<md_lupe_cargue_base_detalle_reaprobacion> listadoCargue, int idCargue, int idPrefactura)
        {
            return DACInserta.CargueLUPEListaReaprobacion(listadoCargue, idCargue, idPrefactura);
        }
        public int InsertarReparobacioPrefacturas(List<md_prefacturas_reaprobacionMasiva> listadoCargue, int idPrefacturaBase)
        {
            return DACInserta.InsertarReparobacioPrefacturas(listadoCargue, idPrefacturaBase);
        }
        public int InsertarDesaparobacioPrefacturas(List<md_prefacturas_desaprobacionMasiva> listadoCargue, int idPrefacturaBase)
        {
            return DACInserta.InsertarDesaparobacioPrefacturas(listadoCargue, idPrefacturaBase);
        }

        public int InsertarCierrePrefacturasMasivo(List<md_prefacturas_cierreMasivo> listadoCargue, int idPrefacturaBase)
        {
            return DACInserta.InsertarCierrePrefacturasMasivo(listadoCargue, idPrefacturaBase);
        }

        //public List<management_prefacturas_notificacionOPLResult> ListaDatoaReportePrefacturasaOPL(int? idCargue)
        //{
        //    return DACConsulta.ListaDatoaReportePrefacturasaOPL(idCargue);
        //}

        public List<management_prefacturas_notificacionOPLNoPasanResult> ListaDatoaReportePrefacturasaOPLNoPasan(int? idCargue)
        {
            return DACConsulta.ListaDatoaReportePrefacturasaOPLNoPasan(idCargue);
        }

        public List<management_prefacturas_notificacionOPLPasanResult> ListaDatoaReportePrefacturasaOPLPasan(int? idCargue)
        {
            return DACConsulta.ListaDatoaReportePrefacturasaOPLPasan(idCargue);
        }
        public List<management_prefacturas_listaMedicamentosReguladosResult> ListaMedicamentosRegulados()
        {
            return DACConsulta.ListaMedicamentosRegulados();
        }
        public int CargueLupeIntermediacionBase(md_lupe_intermediacion obj, int idCargueBase)
        {
            return DACInserta.CargueLupeIntermediacionBase(obj, idCargueBase);
        }

        public int CargueLupeIntermediacionLista(List<md_lupe_intermediacion_dtll> listadoCargue)
        {
            return DACInserta.CargueLupeIntermediacionLista(listadoCargue);

        }

        public void CargueLupe(md_Lupe_cargue_base carguebase, List<md_lupe_cargue_base_detalle> carguedetalle, List<md_lupe_intermediacion_dtll> Intermediaciones, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.CargueLupe(carguebase, carguedetalle, Intermediaciones, ref MsgRes);
        }
        public int CargueMedicamentosRegulados(md_medicamentos_regulados obj, List<md_medicamentos_regulados_detalle> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueMedicamentosRegulados(obj, detalle, ref MsgRes);
        }

        public int CargueMedicamentosDatosOPLS(md_medicamentos_OPLS obj, List<md_medicamentos_OPLS_detalle> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueMedicamentosDatosOPLS(obj, detalle, ref MsgRes);
        }

        public List<md_prefacturas_cargue_base> GetPrefacturasList()
        {
            return DACConsulta.GetPrefacturasList();
        }
        public List<management_validadorPrefacturasResult> GetPrefacturasListado(int? rol, string usuario)
        {
            return DACConsulta.GetPrefacturasListado(rol, usuario);
        }

        public List<management_validadorCarguePrefacturasResult> GetPrefacturasListadoCargue(int? rol, string usuario)
        {
            return DACConsulta.GetPrefacturasListadoCargue(rol, usuario);
        }
        public management_prefacturasDatosCargueResult DatosPrefacturaIdCargue(int idCargue)
        {
            return DACConsulta.DatosPrefacturaIdCargue(idCargue);
        }
        public List<management_consolidadoInicialPrefacturasResult> GetPrefacturasListadoConsolidadoInicial(int? idUsuario)
        {
            return DACConsulta.GetPrefacturasListadoConsolidadoInicial(idUsuario);
        }

        public int CargarLoteMedicamentosDispensacion(medicamentos_dispen_lote obj)
        {
            return DACInserta.CargarLoteMedicamentosDispensacion(obj);
        }

        public int eliminarLoteMedicamentosDispensacion(int lote)
        {
            return DACElimina.eliminarLoteMedicamentosDispensacion(lote);
        }


        public md_prefacturas_cargue_base GetPrefacturas(int id)
        {
            return DACConsulta.GetPrefacturas(id);
        }

        public log_prefacturas_estadoValidacion GetLogEstadoValidacionPrefacturas(int? id)
        {
            return DACConsulta.GetLogEstadoValidacionPrefacturas(id);
        }

        public List<log_prefacturas_estadoValidacion> GetLogEstadoValidacionPrefacturasFases(int? id, int? fase)
        {
            return DACConsulta.GetLogEstadoValidacionPrefacturasFases(id, fase);
        }
        public log_control_validaciones_prefacturas GetLogPrefacturas(int? idCargue)
        {
            return DACConsulta.GetLogPrefacturas(idCargue);
        }

        public int ActualizarFasePrefacturas(int? cargueBase, int? fase)
        {
            return DACActualiza.ActualizarFasePrefacturas(cargueBase, fase);
        }

        public int ActualizarEnValidacionPrefacturas(int? cargueBase, int? estado)
        {
            return DACActualiza.ActualizarEnValidacionPrefacturas(cargueBase, estado);
        }

        public int LogErroresFasesPrefacturas(log_prefacturas_errorFases obj)
        {
            return DACInserta.LogErroresFasesPrefacturas(obj);
        }

        public List<md_prefacturas_detalle> GetPrefacturasById(string numeroPrefactura)
        {
            return DACConsulta.GetPrefacturasById(numeroPrefactura);
        }

        public md_prefacturas_detalle GetPrefacturasID(int? id_detprefactura)
        {
            return DACConsulta.GetPrefacturasID(id_detprefactura);
        }

        public List<ManagmentReportePrefacturasResult> GetRptPrefacturas(int idcargue)
        {
            return DACConsulta.GetRptPrefacturas(idcargue);
        }

        public void ActualizarPrefactura(md_prefacturas_detalle obj)
        {
            DACActualiza.ActualizarPrefactura(obj);
        }

        public int ActualizarPrefacturaLista(List<int> ListaIds, string observaciones, double nuevo_valor)
        {
            return DACActualiza.ActualizarPrefacturaLista(ListaIds, observaciones, nuevo_valor);
        }
        public int DesaprobarPrefacturas(List<int> ListaIds, string observacionDesaprobacion)
        {
            return DACActualiza.DesaprobarPrefacturas(ListaIds, observacionDesaprobacion);
        }

        public int guardarLogDesaprobacionPrefacturas(List<log_prefacturas_desaprobacion> lista)
        {
            return DACInserta.guardarLogDesaprobacionPrefacturas(lista);
        }

        public int guardarLogAprobacionPrefacturas(List<log_prefacturas_aprobacion> lista)
        {
            return DACInserta.guardarLogAprobacionPrefacturas(lista);
        }

        public int GuardarLogAprobacionMasiva(log_prefacturas_aprobacionMasiva obj)
        {
            return DACInserta.GuardarLogAprobacionMasiva(obj);
        }

        public int GuardarLogControl_validacionesPrefacturas(log_control_validaciones_prefacturas obj)
        {
            return DACInserta.GuardarLogControl_validacionesPrefacturas(obj);
        }


        public int GuardarLogDesaprobacionMasiva(log_prefacturas_desaprobacionMasiva obj)
        {
            return DACInserta.GuardarLogDesaprobacionMasiva(obj);
        }

        public int TraerUltimoCargueLogAprobacion()
        {
            return DACConsulta.TraerUltimoCargueLogAprobacion();
        }

        public int TraerUltimoCargueLogDesaprobacion()
        {
            return DACConsulta.TraerUltimoCargueLogDesaprobacion();
        }

        public int GuardarLogDatosAprobacionMasiva(int? idCargue, int? idLog, string usuarioDigita)
        {
            return DACInserta.GuardarLogDatosAprobacionMasiva(idCargue, idLog, usuarioDigita);
        }

        public int GuardarLogDatosDesaprobacionMasiva(int? idCargue, int? idLog, string usuarioDigita)
        {
            return DACInserta.GuardarLogDatosDesaprobacionMasiva(idCargue, idLog, usuarioDigita);
        }


        public void ActualizarPrefacturaListaTotal(int idCargue, string observaciones, double nuevo_valor)
        {
            DACActualiza.ActualizarPrefacturaListaTotal(idCargue, observaciones, nuevo_valor);
        }

        public void InsertarCargueMasivoDispensaciones(dispensacion_cargue_base obj, List<dispensacion_cargue_base_dtll> lista, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCargueMasivoDispensaciones(obj, lista, ref MsgRes);
        }


        public List<ManagmentocargueconsolidadoResult> CuentaConsolidadoMD(String numero_factura, String numero_formula, DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.CuentaConsolidadoMD(numero_factura, numero_formula, fecha_inicial, fecha_final, ref MsgRes);
        }

        public Int32 InsertarFFMMGlosaConciliacion(md_glosa_conciliacion OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFFMMGlosaConciliacion(OBJ, ref MsgRes);
        }

        public vw_md_glosa_conciliacion ConsultaGlosaDtllId(Int32 id_md_glosa_detalle)
        {
            return DACConsulta.ConsultaGlosaDtllId(id_md_glosa_detalle);
        }

        public int GuardarMedicamentosFacturacion(medicamentos_facturacion Obj, List<medicamentos_facturacion_dtll> Result, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.GuardarMedicamentosFacturacion(Obj, Result, ref MsgRes);
        }


        public List<ManagementMedicamentosFacturacionResult> GetListMdFacturacion()
        {
            return DACConsulta.GetListMdFacturacion();
        }

        public List<managemente_medicamentos_facturacionResult> Getmedicamentos_facturacion(int mes, int año, int regional)
        {
            return DACConsulta.Getmedicamentos_facturacion(mes, año, regional);
        }

        public List<ManagementMedicamentosFacturacionResult> GetMedicamentosFacturacionList(int? mesinicio, int? añoinicio, int? mesfinal, int? añofin, string Prestador, string regional)
        {
            return DACConsulta.GetMedicamentosFacturacionList(mesinicio, añoinicio, mesfinal, añofin, Prestador, regional);
        }


        public List<Managment_ReportePrefacturas_totalResult> GetPrefacturasTotal()
        {
            return DACConsulta.GetPrefacturasTotal();
        }
        public List<Managment_ReportePrefacturas_cerrar_abiertasResult> GetPrefacturasCerrarAbiertas()
        {
            return DACConsulta.GetPrefacturasCerrarAbiertas();
        }

        public List<management_prefacturas_consolidado_abiertasResult> GetPrefacturasAbiertas(int? cargueBase)
        {
            return DACConsulta.GetPrefacturasAbiertas(cargueBase);
        }
        public List<management_prefacturas_consolidado_cerradasResult> GetPrefacturasCerradas(int? cargueBase)
        {
            return DACConsulta.GetPrefacturasCerradas(cargueBase);
        }
        public List<Managment_ReportePrefacturas_cerrar_cerradasResult> GetPrefacturasCerrarCerradas()
        {
            return DACConsulta.GetPrefacturasCerrarCerradas();
        }

        public int ActualizarPrefacturaCerrar(md_prefacturas_detalle obj)
        {
            return DACActualiza.ActualizarPrefacturaCerrar(obj);
        }

        public int GuardarPrefacturaCerrada(md_prefacturas_cargue_cerradas obj)
        {
            return DACInserta.GuardarPrefacturaCerrada(obj);
        }

        public void EliminarCarguePrefactura(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarCarguePrefactura(idCargue, ref MsgRes);
        }

        public void EliminarCargueLUPE(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarCargueLUPE(idCargue, ref MsgRes);
        }

        public void EliminarMedicamentosRegulados(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarMedicamentosRegulados(idCargue, ref MsgRes);
        }


        public List<md_prefacturas_cargue_base> ConsultaExistenciaPrefactura(int regional, string numPrefactura, int idPrestador)
        {
            return DACConsulta.ConsultaExistenciaPrefactura(regional, numPrefactura, idPrestador);
        }

        public List<ref_referencia_pago> GetReferenciaPagoList()
        {
            return DACConsulta.GetReferenciaPagoList();
        }


        #endregion

        #region PROCESOS INTERNOS

        public List<indicador_regional> ConsultarIndicadorRegionalList(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultarIndicadorRegionalList(ref MsgRes);

        }

        public List<vw_visitas> ConsultaCronogramaVisitas(Int32? idcronograma, ref MessageResponseOBJ MsgRta)
        {
            return DACConsulta.ConsultaCronogramaVisitas(idcronograma, ref MsgRta);
        }

        public List<Management_Consulta_Cronograma_VisitasResult> ConsultaCronogramaVisitasProcedimiento(int tipoFiltro, string Auditor)
        {
            return DACConsulta.ConsultaCronogramaVisitasProcedimiento(tipoFiltro, Auditor);
        }

        public List<cronograma_visita_detalle> ConsultaCronogramaVisitaDetalle(int idcronograma)
        {
            return DACConsulta.ConsultaCronogramaVisitaDetalle(idcronograma);
        }

        public List<cronograma_visita_detalle_indicador> ConsultaCronogramaVisitaDetalleInd(int idcronograma)
        {
            return DACConsulta.ConsultaCronogramaVisitaDetalleInd(idcronograma);
        }

        public cronograma_visitas getvisitabyid(Int32 idvisita, ref MessageResponseOBJ MsgRta)
        {
            return DACConsulta.getvisitabyid(idvisita, ref MsgRta);
        }

        public void InsertarCronogramaVisitas(cronograma_visitas objcronograma, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCronogramaVisitas(objcronograma, ref MsgRes);
        }

        public void ActualizarCronogramaVisitas(cronograma_visitas objcronograma, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarCronogramaVisitas(objcronograma, ref MsgRes);
        }

        public void insertardetallevisita(int id_cronograma, int id_regional, int id_indicador, List<item_capitulo> listadoitems, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.insertardetallevisita(id_cronograma, id_regional, id_indicador, listadoitems, ref MsgRes);
        }

        public void insertarcalificacionesvisita(int idcronograma, string[] calificaciones, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.insertarcalificacionesvisita(idcronograma, calificaciones, ref MsgRes);
        }

        public int insertardetallevisitaindicador(List<capitulo_indicador> capitulos, int idcronograma, int idprestador, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.insertardetallevisitaindicador(capitulos, idcronograma, idprestador, ref MsgRes);
        }

        public List<capitulos> GetCapitulosList()
        {
            return DACConsulta.GetCapitulosList();
        }

        public List<capitulo_indicador> GetCapitulosByIndicador(Int32? idindicador, Int32 idregioanl, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetCapitulosByIndicador(idindicador, idregioanl, ref MsgRes);
        }

        public List<ManagementCalidadDtllIndicadorResult> GetCapitulosEvaluadosByIndicador(Int32? idindicador, Int32 idregioanl, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetCapitulosEvaluadosByIndicador(idindicador, idregioanl, ref MsgRes);
        }

        public capitulo_indicador getcapbyindicadorcap(int idcapitulo, int idindicador, int idregional)
        {
            return DACConsulta.getcapbyindicadorcap(idcapitulo, idindicador, idregional);
        }

        public List<indicadores> ConsultarIndicadores(int? idindicador, ref MessageResponseOBJ MegRes)
        {
            return DACConsulta.ConsultarIndicadores(idindicador, ref MegRes);
        }

        public item_capitulo Getitemcapbyid(Int32 IdItem)
        {
            return DACConsulta.Getitemcapbyid(IdItem);
        }

        public List<item_capitulo> Getitemcapbyindcap(Int32 idregional, Int32 idindicador, Int32? idcap)
        {
            return DACConsulta.Getitemcapbyindcap(idregional, idindicador, idcap);
        }

        public List<cronograma_visita_detalle> Getdetalllescronograma(int idcronograma)
        {
            return DACConsulta.Getdetalllescronograma(idcronograma);
        }

        public bool ActualizarItemCapitulo(item_capitulo objitem)
        {
            return DACActualiza.ActualizarItemCapitulo(objitem);
        }

        public capitulos Getcapitulobyid(Int32 idcapitulo)
        {
            return DACConsulta.Getcapitulobyid(idcapitulo);
        }

        public bool InsertarItemCapitulo(item_capitulo itemcapitulo)
        {
            return DACInserta.InsertarItemCapitulo(itemcapitulo);
        }

        public bool InsertaCapitulo(capitulos capitulo)
        {
            return DACInserta.InsertaCapitulo(capitulo);
        }

        public bool ActualizarCapituloIndicador(Int32 idcapituloIndicador, int pesogen)
        {
            return DACActualiza.ActualizarCapituloIndicador(idcapituloIndicador, pesogen);
        }

        public Ref_RIPS_Prestadores getPrestador(double codprestador, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.getPrestador(codprestador, ref MsgRes);
        }

        public List<ref_usuario_responsable> ConsultResponsablesbyusuario(Int32 idusuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultResponsablesbyusuario(idusuario, ref MsgRes);
        }

        public List<sis_usuario> LstResponsables()
        {
            return DACConsulta.LstResponsables();
        }

        public List<calidad_prestadores> getprestadoresList()
        {
            return DACConsulta.getprestadoresList();
        }

        public calidad_prestadores getPresadorById(int idprestador)
        {
            return DACConsulta.getPresadorById(idprestador);
        }

        public List<calidad_ref_especialidad> GetRefEspecialidadList()
        {
            return DACConsulta.GetRefEspecialidadList();
        }

        public List<calidad_ref_regimen> GetRefRegimentList()
        {
            return DACConsulta.GetRefRegimentList();
        }

        public List<Ref_clase_persona> GetClasePersonaList()
        {
            return DACConsulta.GetClasePersonaList();
        }

        public List<vw_calidad_prestador_indicador> GetListIndicadoresPrestador(int id_prestador)
        {
            return DACConsulta.GetListIndicadoresPrestador(id_prestador);
        }

        public void InsertarPrestador(calidad_prestadores Obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarPrestador(Obj, ref MsgRes);
        }

        public int ActualizarContratosPrestadorVisitas(string sap, DateTime? fechaInicio, DateTime? fechaFin, string numContrato, int? tipo)
        {
            return DACActualiza.ActualizarContratosPrestadorVisitas(sap, fechaInicio, fechaFin, numContrato, tipo);
        }

        public void InsertarVisita(cronograma_visitas ObjCronocrama, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarVisita(ObjCronocrama, ref MsgRes);
        }

        public void insertaRegionalPrestadores(Int32 idregional, List<int> prestadores)
        {
            DACInserta.insertaRegionalPrestadores(idregional, prestadores);
        }

        public void InsertarCapitulosPrestador(Int32 idregional, Int32 idindicador, List<int> capitulos)
        {
            DACInserta.InsertarCapitulosPrestador(idregional, idindicador, capitulos);
        }

        public void EliminarCapitulo(int idcapitulo)
        {
            DACElimina.EliminarCapitulo(idcapitulo);
        }

        public void EliminarVisita(Int32 idvisita, log_eliminacion_visitas obj, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarVisita(idvisita, obj, ref MsgRes);
        }

        public void EliminarEvaluacionVisita(Int32 idvisita, log_eliminacion_visitas obj, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarEvaluacionVisita(idvisita, obj, ref MsgRes);
        }

        public void EliminarEgreso(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarEgreso(id, ref MsgRes);
        }

        public Int32 InsertarCargueRanking(calidad_cargue_ranking ranking)
        {
            return DACInserta.InsertarCargueRanking(ranking);
        }

        public void InsertarDetalleCargueRanking(List<calidad_detalle_cargue_ranking> detalleranking)
        {
            DACInserta.InsertarDetalleCargueRanking(detalleranking);
        }

        public void InsertarNovedadVisita(cronograma_visita_novedades obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarNovedadVisita(obj, ref MsgRes);
        }

        public void InsertarMasivamenteReportesEvaluacionVisitas(List<cronograma_visitas_reportesDoc_evaluacion_calidad> obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarMasivamenteReportesEvaluacionVisitas(obj, ref MsgRes);
        }

        public Management_get_info_visita_por_idResult GetInfoVisitaById(int idCronograma)
        {
            return DACConsulta.GetInfoVisitaById(idCronograma);
        }

        public void actualizarPrestador(calidad_prestadores Obj, int idprestador)
        {
            DACActualiza.actualizarPrestador(Obj, idprestador);
        }

        public List<Ref_calidad_municipios> GetMunicipiosDane()
        {
            return DACConsulta.GetMunicipiosDane();
        }

        public List<vw_visitas> GetListVisitas(Int32? id_visita, Int32? id_prestador, string numcontrato)
        {
            return DACConsulta.GetListVisitas(id_visita, id_prestador, numcontrato);
        }

        public List<ref_cronograma_visitas_novedades> GetListTipoNovedad()
        {
            return DACConsulta.GetListTipoNovedad();
        }

        public void GuardarActaVisitas(cronograma_visita_documento obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.GuardarActaVisitas(obj, ref MsgRes);
        }


        public void GuardarInformeOperativo(cronograma_visita_informeOperativo obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.GuardarInformeOperativo(obj, ref MsgRes);
        }

        public cronograma_visita_informeOperativo TraerArchivoInformeOperativo(int? idArchivo)
        {
            return DACConsulta.TraerArchivoInformeOperativo(idArchivo);
        }

        //public void GuardarActasVisitas(cronograma_visita_documento obj)
        //{
        //    DACInserta.GuardarActasVisita(obj);
        //}


        public cronograma_visita_documento Getactavisita(int idvisita)
        {
            return DACConsulta.Getactavisita(idvisita);
        }

        public management_cronograma_visita_documento_idResult Getactavisita2(int idvisita)
        {
            return DACConsulta.Getactavisita2(idvisita);
        }

        public List<management_cronograma_visita_documento_sinRutaResult> GetactavisitaSinRuta()
        {
            return DACConsulta.GetactavisitaSinRuta();
        }

        public List<vw_visitas_documentos> GetActasVisitas(int regional, int mes, int año)
        {
            return DACConsulta.GetActasVisitas(regional, mes, año);
        }

        public List<ManagementConsultaGnralVisitasResult> GetConsultageneralVisitas(int regional, int prestador, DateTime fecha, string nit, string codsap)
        {
            return DACConsulta.GetConsultageneralVisitas(regional, prestador, fecha, nit, codsap);
        }

        public cronograma_visita_detalle Getresultadovisitaindicador(int idvisita, int idindicador)
        {
            return DACConsulta.Getresultadovisitaindicador(idvisita, idindicador);
        }

        public List<cronograma_visitas_calificaciones> GetCalificacionesVisita(int id_cronograma)
        {
            return DACConsulta.GetCalificacionesVisita(id_cronograma);
        }

        #endregion

        #region ANALISIS CASO

        public List<analisis_caso_salud_publica> ConsultaEvolucionAnalisisSaludP(Int32 idconcurrencia, Int32? IdAnalisis, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaEvolucionAnalisisSaludP(idconcurrencia, IdAnalisis, ref MsgRes);
        }

        public List<analisis_caso_alertas> ConsultaAnalisisCasoAlertas(Int32? idconcurrencia, Int32? IdAnalisis, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAnalisisCasoAlertas(idconcurrencia, IdAnalisis, ref MsgRes);
        }

        public List<analisis_caso_muestreo> ConsultaAnalisisCasoMuestreo(Int32? idconcurrencia, Int32? IdAnalisis, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAnalisisCasoMuestreo(idconcurrencia, IdAnalisis, ref MsgRes);
        }

        public List<ecop_concurrencia_eventos_en_asalud> ConsultaAnalisisEventosenSalud(Int32 idconcurrencia, Int32? IdAnalisis, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAnalisisEventosenSalud(idconcurrencia, IdAnalisis, ref MsgRes);
        }

        public List<ecop_concurrencia_eventos_en_salud_detalle> ConsultaAnalisisEventosenSaludDetalle(ecop_concurrencia_eventos_en_salud_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaAnalisisEventosenSaludDetalle(OBJ, ref MsgRes);
        }

        public List<ecop_concurrencia_eventos_en_salud_detalle> GetAnalisisEventosenSaludDetalle(int idanalisis)
        {
            return DACConsulta.GetAnalisisEventosenSaludDetalle(idanalisis);
        }

        public int InsertarAnalisisCasos(analisis_caso_original Analisis, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarAnalisisCasos(Analisis, ref MsgRes);
        }

        public int InsertarAnalisisMuestreo(analisis_caso_muestreo AnalisisMuestreo, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarAnalisisMuestreo(AnalisisMuestreo, ref MsgRes);
        }

        public int InsertarAnalisisCasosAlerta(analisis_caso_alertas AnalisisAlerta, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarAnalisisCasosAlerta(AnalisisAlerta, ref MsgRes);
        }

        public void ActualizarAnalisisCasos(analisis_caso_original Analisis, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarAnalisisCasos(Analisis, ref MsgRes);
        }

        public void ActualizarAnalisisMuestreo(analisis_caso_muestreo AnalisisM, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarAnalisisMuestreo(AnalisisM, ref MsgRes);
        }

        public void ActualizarAnalisisAlertas(analisis_caso_alertas AnalisisA, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarAnalisisAlertas(AnalisisA, ref MsgRes);
        }

        public int InsertarAnalisisCasosSaludP(analisis_caso_salud_publica Analisis, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarAnalisisCasosSaludP(Analisis, ref MsgRes);
        }

        public void ActualizarAnalisisCasoSaludPublica(analisis_caso_salud_publica analisis, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarAnalisisCasoSaludPublica(analisis, ref MsgRes);
        }

        public void InsertarAnalisisCasosEventos(ecop_concurrencia_eventos_en_asalud Analisis, List<ecop_concurrencia_eventos_en_salud_detalle> otrosiList, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarAnalisisCasosEventos(Analisis, otrosiList, ref MsgRes);
        }

        public void InsertarAnalisisCasosEventosDet(ecop_concurrencia_eventos_en_asalud Analisis, List<ecop_concurrencia_eventos_en_salud_detalle> otrosiList, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarAnalisisCasosEventosDet(Analisis, otrosiList, ref MsgRes);
        }

        public Int32 InsertarAnalisisCasosEventosDetalle(ecop_concurrencia_eventos_en_salud_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarAnalisisCasosEventosDetalle(OBJ, ref MsgRes);
        }

        public void ActualizarAnalisisEventosSalud(ecop_concurrencia_eventos_en_asalud Analisis, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarAnalisisEventosSalud(Analisis, ref MsgRes);
        }

        public List<ManagmentReporteAnalisisCasoSPResult> ReporteAnalisisCasoSP(Int32 idconcurrencia, Int32 idanalisis)
        {
            return DACConsulta.ReporteAnalisisCasoSP(idconcurrencia, idanalisis);
        }

        public List<ManagmentReporteAnalisisCasoOrgResult> ReporteAnalisisCasoOriginal(Int32 idConcurrencia, Int32 idAnalisis, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ReporteAnalisisCasoOriginal(idConcurrencia, idAnalisis, ref MsgRes);
        }

        public List<ManagmentReporteAnalisisESResult> ReporteEventosSalud(Int32 IdConcurrencia, Int32 Idanalisis)
        {
            return DACConsulta.ReporteEventosSalud(IdConcurrencia, Idanalisis);
        }

        public List<vw_tablero_analisis_casos> ConsultaTableroAnalisisCasos(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaTableroAnalisisCasos(ref MsgRes);
        }

        public void Insertargestionanalisisdecaso(GestionAnalisisDeCasos Analisis, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.Insertargestionanalisisdecaso(Analisis, ref MsgRes);
        }

        public void Actualizargestionanalisisdecaso(GestionAnalisisDeCasos Analisis, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.Actualizargestionanalisisdecaso(Analisis, ref MsgRes);
        }

        public GestionAnalisisDeCasos GetAnalisisGestion(Int32? idtipoanalisis, Int32? idanalsis, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetAnalisisGestion(idtipoanalisis, idanalsis, ref MsgRes);
        }

        public List<vw_analisis_caso_alertas> GetIdAnalisisAlertas(Int32 id_concurrencia, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetIdAnalisisAlertas(id_concurrencia, ref MsgRes);
        }

        public List<vw_analisis_caso_muestreo> GetIdAnalisisMuestras(Int32 id_concurrencia, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetIdAnalisisMuestras(id_concurrencia, ref MsgRes);
        }

        #endregion

        #region URGENCIAS
        public void InsertarUrgencias(List<urg_cargue_base_ok> ListUrgencias, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarUrgencias(ListUrgencias, ref MsgRes);
        }

        public void InsertarAuditoriaUrgencias(urg_auditoria_urgencias aud_urgencias, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarAuditoriaUrgencias(aud_urgencias, ref MsgRes);
        }

        public List<urg_cargue_base_ok> ConsultarUrgencias(int? idurgencia, DateTime? fechadesde, DateTime? fechahasta, int? regional, int? idusuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultarUrgencias(idurgencia, fechadesde, fechahasta, regional, idusuario, ref MsgRes);
        }

        public List<Ref_tipo_egreso> ConsultaTipoEgreso(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaTipoEgreso(ref MsgRes);
        }

        public List<ref_urg_destino_paciente> ConsultaDestinoPaciente(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaDestinoPaciente(ref MsgRes);
        }

        public List<vw_tablero_urgencias_ok> ConsultaTablerUrgenciasOk()
        {
            return DACConsulta.ConsultaTablerUrgenciasOk();
        }


        #endregion

        #region CIERRE CONTABLE

        public Int32 InsertarCierreContable(cierre_contable obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCierreContable(obj, ref MsgRes);
        }

        public List<cierre_contable> GetListCierreContable(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetListCierreContable(ref MsgRes);
        }

        public bool InsertarFacturasMesInterior(List<cierre_cont_mes_anterior> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturasMesInterior(List, ref MsgRes);
        }

        public bool InsertarFacturasRechazos(List<cierre_cont_rechazos> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturasRechazos(List, ref MsgRes);
        }

        public bool InsertarFacturasPendientesProcs(List<cierre_cont_pendiente_procesar> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturasPendientesProcs(List, ref MsgRes);
        }

        public bool InsertarFacturasdevoluciones(List<cierre_cont_devoluciones> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturasdevoluciones(List, ref MsgRes);
        }

        public bool InsertarFacturascausadas(List<cierre_cont_causadas> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturascausadas(List, ref MsgRes);
        }

        public bool InsertarFacturasradicadas(List<cierre_cont_radicadas> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturasradicadas(List, ref MsgRes);
        }

        public cierre_contable GetCierreContable(int idcierre, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetCierreContable(idcierre, ref MsgRes);
        }

        public List<vw_totales_cierre_contable> GetListTotalesCierreContable(int idcierre, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetListTotalesCierreContable(idcierre, ref MsgRes);
        }

        public List<vw_causas_facturas> GetListCausasCierreContable(int idcierre, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetListCausasCierreContable(idcierre, ref MsgRes);
        }


        public cierre_contable_cargue_base traerCierreContable(int? mes, int? año, int? regional)
        {
            return DACConsulta.traerCierreContable(mes, año, regional);
        }

        public List<management_cierre_contable_tableroControlResult> TraerDatosCierreContable()
        {
            return DACConsulta.TraerDatosCierreContable();
        }

        public int InsertarCierreContable(cierre_contable_cargue_base obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCierreContable(obj, ref MsgRes);
        }

        public void InsertarCierreContableDetalle(List<cierre_contable_cargue_detalle> dtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCierreContableDetalle(dtll, ref MsgRes);
        }

        public int EliminarCargueCierreContable(int idCargue)
        {
            return DACElimina.EliminarCargueCierreContable(idCargue);
        }

        public int InsertarLogEliminarCierreContable(log_cierreContable_eliminarConsolidado obj)
        {
            return DACInserta.InsertarLogEliminarCierreContable(obj);
        }

        #endregion

        #region COHORTES

        public List<ref_cohortes> Get_refCohortes()
        {
            return DACConsulta.Get_refCohortes();
        }

        public List<ref_cohortes> Get_refCohortesSindh()
        {
            return DACConsulta.Get_refCohortesSindh();
        }
        public List<ref_adh_modalidad_prestacion> Get_adherencia_modalidad_prestacion()
        {
            return DACConsulta.Get_adherencia_modalidad_prestacion();
        }
        public int InsertCohortesDatos(cohortes_cargue_base obj, List<cohortes_detalle_cargue_OK> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertCohortesDatos(obj, lista, ref MsgRes);
        }
        public int InsertCohortesEPOC(cohortes_cargue_base obj, List<cohortes_detalle_cargue_OK> listaepoc, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertCohortesEPOC(obj, listaepoc, ref MsgRes);
        }

        public void InsertCohortesPAD(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaPAD, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertCohortesPAD(cargue, listaPAD, ref MsgRes);
        }

        public void InsertCohortesRCV(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaRCV, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertCohortesRCV(cargue, listaRCV, ref MsgRes);
        }

        public void InsertCohortesGESTANTES(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaGestantes, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertCohortesGESTANTES(cargue, listaGestantes, ref MsgRes);
        }

        public void EliminarCargueCohorte(int? idCargue, int? tipo, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarCargueCohorte(idCargue, tipo, ref MsgRes);
        }
        public List<management_cohortesBeneficiarioResult> GetCohortesBeneficiario(string idDoc)
        {
            return DACConsulta.GetCohortesBeneficiario(idDoc);
        }
        public List<management_HospitalizacionEvitable_cohortesResult> HospitalizacionPrevenible_cohortes(string idDoc)
        {
            return DACConsulta.HospitalizacionPrevenible_cohortes(idDoc);
        }
        public List<management_hospitalizacionPrevenible_TableroResult> GetHospitalizacionPrevenible()
        {
            return DACConsulta.GetHospitalizacionPrevenible();
        }
        public management_hospitalizacionPrevenible_detalleResult GetHospitalizacionPrevenibleDetalle(int idHE)
        {
            return DACConsulta.GetHospitalizacionPrevenibleDetalle(idHE);
        }

        public int InsertarHospitalizacionPrevenible(ecop_hospitalizacion_prevenible obj)
        {
            return DACInserta.InsertarHospitalizacionPrevenible(obj);
        }

        public List<ecop_directorioPPE_correos> GetEcop_DirectorioPPE_Correos(string regional)
        {
            return DACConsulta.GetEcop_DirectorioPPE_Correos(regional);
        }
        public List<ecop_directorioPPE_correos> GetEcop_DirectorioPPE_CorreosDocumento(string documento)
        {
            return DACConsulta.GetEcop_DirectorioPPE_CorreosDocumento(documento);

        }

        public List<ref_cargue_vigilanciaCohortes_tipos> ListaTiposVCohorte()
        {
            return DACConsulta.ListaTiposVCohorte();
        }

        #endregion

        #region ADHERENCIA 

        public List<ref_adh_tipo_criterio> get_ref_TipoCriterio()
        {
            return DACConsulta.get_ref_TipoCriterio();
        }

        public List<ref_adh_grupo_tipocriterio> get_ref_grupoTipoCriterio()
        {
            return DACConsulta.get_ref_grupoTipoCriterio();
        }

        public List<adh_tipocriterio> get_adh_tipocriterio(int idadherencia)
        {
            return DACConsulta.get_adh_tipocriterio(idadherencia);
        }

        public List<ref_adh_grupo_tipocriterio> get_ref_adh_grupotipocriterio()
        {
            return DACConsulta.get_ref_adh_grupotipocriterio();
        }

        public List<ref_adh_tipo_criterio> get_ref_TipoCriterio_cohorte(int idtipocohorte)
        {
            return DACConsulta.get_ref_TipoCriterio_cohorte(idtipocohorte);
        }


        public List<adh_criterio> getcriteriosbytipocohorte(int tipocohorte)
        {
            return DACConsulta.getcriteriosbytipocohorte(tipocohorte);
        }

        public void InsertarTipoCriterio(ref_adh_tipo_criterio obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarTipoCriterio(obj, ref MsgRes);
        }

        public void InsertarCriterio(adh_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCriterio(criterio, ref MsgRes);
        }

        public void ActualizarTipoCriterio(ref_adh_tipo_criterio obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarTipoCriterio(obj, ref MsgRes);
        }

        public void ActualizarCriterio(adh_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarCriterio(criterio, ref MsgRes);
        }

        public void EliminarCriterioCohorte(int idcriterio, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarCriterioCohorte(idcriterio, ref MsgRes);
        }

        public void EliminarTipoCriterio(int idtipocriterio, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarTipoCriterio(idtipocriterio, ref MsgRes);
        }

        public adh_criterio ConsultarCriterioById(int idcriterio)
        {
            return DACConsulta.ConsultarCriterioById(idcriterio);
        }

        public int InsertarResultados(adh_resultados resultados, List<string> resultadoshc1, ref MessageResponseOBJ Msg)
        {
            return DACInserta.InsertarResultados(resultados, resultadoshc1, ref Msg);
        }

        public List<adh_resultados> GetResultadosPrestador(int idprestador, int profesional, int mes, int año)
        {
            return DACConsulta.GetResultadosPrestadorv2(idprestador, profesional, mes, año);
        }

        public List<vw_rptResultadosAdherencia> GetResultadosPrestador(Int32? idresultados)
        {
            return DACConsulta.GetResultadosPrestador(idresultados);
        }

        public List<managmentReporteResultadosAdherenciaResult> GetResultadosAdherencia(Int32 idresultados)
        {
            return DACConsulta.GetResultadosAdherencia(idresultados);
        }

        public List<managmentReporteResultadosAdherencia2Result> GetResultadosAdherencia2(Int32 idresultados)
        {
            return DACConsulta.GetResultadosAdherencia2(idresultados);
        }

        public List<Management_adh_cantidad_resultados_grupoResult> GetResultadosGrupoAdherencia(Int32 idresultados)
        {
            return DACConsulta.GetResultadosGrupoAdherencia(idresultados);
        }

        public List<Ref_ips_cuentas> getprestadores()
        {
            return DACConsulta.getprestadores();

        }
        public List<management_prestadoresHomologadosResult> getprestadoresHomologados()
        {
            return DACConsulta.getprestadoresHomologados();
        }

        public void InsertarTipoCohorte(ref_cohortes obj)
        {
            DACInserta.InsertarTipoCohorte(obj);
        }

        public void ActualizarTipoCohorte(ref_cohortes obj)
        {
            DACActualiza.ActualizarTipoCohorte(obj);
        }

        public ref_cohortes getTipoCohorteById(int idtipocohorte)
        {
            return DACConsulta.getTipoCohorteById(idtipocohorte);
        }

        public void InsertarAdminCriterios(int tipoadherencia, List<int> seleccionados, List<int> seleccionados2, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarAdminCriterios(tipoadherencia, seleccionados, seleccionados2, ref MsgRes);
        }

        public List<ref_adherencia_unis> GetUnisByRegional(int idregional)
        {
            return DACConsulta.GetUnisByRegional(idregional);
        }

        public List<ref_adherencia_ciudad> GetciudadByunis(int idunis)
        {
            return DACConsulta.GetciudadByunis(idunis);
        }

        public List<ref_adherencia_prestador_ciudad> GetPrestadoresByciudad(int idciudad)
        {
            return DACConsulta.GetPrestadoresByciudad(idciudad);
        }

        public List<ref_adherencia_profesional_prestador> GetProfesionalesByprestador(int idprestador)
        {
            return DACConsulta.GetProfesionalesByprestador(idprestador);
        }

        #region ODONTOLOGIA

        public List<Ref_odont_list_check_ortod> getcheckOrtod()
        {
            return DACConsulta.getcheckOrtod();
        }

        public List<Ref_odont_check_porcentajes> getcheckPorcentaje()
        {
            return DACConsulta.getcheckPorcentaje();
        }

        public List<Ref_odont_tipo_endodoncia> getListTipoEndodoncia()
        {
            return DACConsulta.getListTipoEndodoncia();
        }

        public List<Ref_odont_parametros_auditados> getListParametrosAuditados()
        {
            return DACConsulta.getListParametrosAuditados();
        }


        public Int32 InsertarOdontOrtodoncia(odont_tratamiento_ortodoncia OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarOdontOrtodoncia(OBJ, ref MsgRes);
        }

        public Int32 InsertarOdontOrtodonciaDetalle(odont_tratamiento_ortodoncia_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarOdontOrtodonciaDetalle(OBJ, ref MsgRes);
        }

        public Int32 InsertarOdontEndodoncia(odont_tratamiento_endodoncia OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarOdontEndodoncia(OBJ, ref MsgRes);
        }

        public Int32 InsertarOdontFija(odont_rehabilitacion_oral_protesis_fija OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarOdontFija(OBJ, ref MsgRes);
        }

        public void InsertarOdontFijaDtll(List<odont_rehabilitacion_oral_protesis_fija_dtll> OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarOdontFijaDtll(OBJ, ref MsgRes);
        }

        public Int32 InsertarOdontRemovible(odont_rehabilitacion_oral_protesis_removibles OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarOdontRemovible(OBJ, ref MsgRes);
        }

        public List<vw_odont_ortodoncia_report> ConsultaIdReporteOrtodoncia(Int32 id_tratamiento_ortodoncia, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdReporteOrtodoncia(id_tratamiento_ortodoncia, ref MsgRes);
        }
        public Int32 InsertarOdontRemovibledtll(odont_rehabilitacion_oral_protesis_removibles_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarOdontRemovibledtll(OBJ, ref MsgRes);
        }

        public Int32 InsertarOdontEndodonciadtll(odont_tratamiento_endodoncia_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarOdontEndodonciadtll(OBJ, ref MsgRes);
        }

        public List<vw_odont_removible_report> ConsultaIdReporteRemovible(Int32 id_rehabilitacion_oral_protesis_removibles, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdReporteRemovible(id_rehabilitacion_oral_protesis_removibles, ref MsgRes);
        }
        public List<vw_odont_endodoncia_report> ConsultaIdReporteEndodoncia(Int32 id_tratamiento_endodoncia, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdReporteEndodoncia(id_tratamiento_endodoncia, ref MsgRes);
        }

        public List<vw_odont_fija_report> ConsultaIdReporteProtesisFija(Int32 id_tratamiento_Protesis_Fija, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdReporteProtesisFija(id_tratamiento_Protesis_Fija, ref MsgRes);
        }

        public List<odont_rehabilitacion_oral_protesis_fija_dtll> ConsultaIdReporteProtesisFijaDtll(Int32 id_tratamiento_Protesis_Fija, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdReporteProtesisFijaDtll(id_tratamiento_Protesis_Fija, ref MsgRes);
        }

        public List<vw_odont_porcentaje_d1_fija> Getporcentaje_d1_fija(Int32 id_protesis_fija, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.Getporcentaje_d1_fija(id_protesis_fija, ref MsgRes);
        }

        public List<vw_odont_porcentaje_d2_fija> Getporcentaje_d2_fija(Int32 id_protesis_fija, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.Getporcentaje_d2_fija(id_protesis_fija, ref MsgRes);
        }


        public List<vw_odont_reporte_removible_dtll> ConsultaIdReporteProtesisRemovibleDtll(Int32 id_rehabilitacion_oral_protesis_removibles, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdReporteProtesisRemovibleDtll(id_rehabilitacion_oral_protesis_removibles, ref MsgRes);
        }

        public List<vw_odont_tableros_ortodoncia> ConsultaListadoTTOsOrodoncia(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListadoTTOsOrodoncia(ref MsgRes);
        }

        public List<vw_odont_tableros_ortodoncia_prof> ConsultaListadoTTOsOrodonciaProf(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListadoTTOsOrodonciaProf(ref MsgRes);
        }

        public List<vw_odont_tableros_ProtesisFija> ConsultaListadoTTOsPPF(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListadoTTOsPPF(ref MsgRes);
        }

        public List<vw_odont_tableros_ProtesisFija_prof> ConsultaListadoTTOsProf(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListadoTTOsProf(ref MsgRes);
        }


        public List<vw_odont_tableros_ProtesisRemov> ConsultaListadoTTOsRemovible(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListadoTTOsRemovible(ref MsgRes);
        }

        public List<vw_odont_tableros_ProtesisRemov_prof> ConsultaListadoTTOsRemoviblesProf(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListadoTTOsRemoviblesProf(ref MsgRes);
        }

        public List<vw_odont_tableros_endodoncia> ConsultaListadoTTOsEndodoncia(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListadoTTOsEndodoncia(ref MsgRes);
        }

        public List<vw_odont_tableros_endodoncia_prof> ConsultaListadoEndodonciaProf(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaListadoEndodonciaProf(ref MsgRes);
        }

        public List<Ref_odont_acciones_mejora> GetListAccionesMejora()
        {
            return DACConsulta.GetListAccionesMejora();
        }

        public List<Ref_odont_estado_accion> GetListEstadosAccionesMejora()
        {
            return DACConsulta.GetListEstadosAccionesMejora();
        }

        public List<vw_odont_tbl_prestadores> GetprestadoresPlanMejora()
        {
            return DACConsulta.GetprestadoresPlanMejora();
        }

        public List<vw_odont_planes_mejora> GetPlanesMejora()
        {
            return DACConsulta.GetPlanesMejora();
        }

        public Int32 InsertarPlanMejoraTratamiento(odont_plan_mejora_tratamiento OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejoraTratamiento(OBJ, ref MsgRes);
        }

        public Int32 InsertarPlanMejoraTratamientodetalle(odont_plan_mejora_tratamiento_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejoraTratamientodetalle(OBJ, ref MsgRes);
        }

        public Int32 InsertarPlanMejoraBeneficiario(odont_plan_mejora_beneficiario OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejoraBeneficiario(OBJ, ref MsgRes);
        }

        public Int32 InsertarPlanMejoraBeneficiariodetalle(odont_plan_mejora_beneficiario_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejoraBeneficiariodetalle(OBJ, ref MsgRes);
        }

        public void ActualizarOdontPlanMejora(odont_plan_mejora_dtll obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarOdontPlanMejora(obj2, ref MsgRes);
        }

        public void ActualizarOdontPlanMejoraBeneficiario(odont_plan_mejora_beneficiario_dtll obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarOdontPlanMejoraBeneficiario(obj2, ref MsgRes);
        }

        public List<odont_plan_mejora> GetPlanMejoraTra()
        {
            return DACConsulta.GetPlanMejoraTra();
        }

        public List<odont_plan_mejora_beneficiario> GetPlanMejoraBen()
        {
            return DACConsulta.GetPlanMejoraBen();
        }

        public List<vw_odont_planes_mejora> GetPlanMejoraTradtll(Int32 id_odont_plan_mejora, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetPlanMejoraTradtll(id_odont_plan_mejora, ref MsgRes);
        }

        public List<vw_odont_planes_mejora_ben> GetPlanMejoraBendtll(Int32 id_odont_plan_mejora_beneficiario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetPlanMejoraBendtll(id_odont_plan_mejora_beneficiario, ref MsgRes);
        }

        public void ActualizarOdontPlanMejoraObsTratamiento(odont_plan_mejora obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarOdontPlanMejoraObsTratamiento(obj2, ref MsgRes);
        }

        public void ActualizarOdontPlanMejoraObsBeneficiario(odont_plan_mejora_beneficiario obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarOdontPlanMejoraObsBeneficiario(obj2, ref MsgRes);
        }

        public List<vw_tablero_plan_mejora_ben> ConsultaTableroPlanBen()
        {
            return DACConsulta.ConsultaTableroPlanBen();
        }

        public void ActualizarOdontPlanMejoraCierreTrat(odont_plan_mejora obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarOdontPlanMejoraCierreTrat(obj2, ref MsgRes);
        }
        public void ActualizarOdontPlanMejoraCierreBen(odont_plan_mejora_beneficiario obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarOdontPlanMejoraCierreBen(obj2, ref MsgRes);
        }

        public Int32 InsertarHistoriaClinica(odont_historia_clinica OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarHistoriaClinica(OBJ, ref MsgRes);
        }
        public Int32 InsertarHistoriaClinicaPaciente(odont_historia_clinica_paciente OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarHistoriaClinicaPaciente(OBJ, ref MsgRes);
        }
        public Int32 InsertarHistoriaClinicaDetalle(odont_historia_clinica_detalle_calidad OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarHistoriaClinicaDetalle(OBJ, ref MsgRes);
        }
        public Int32 InsertarHistoriaClinicaDetalleConte(odont_historia_clinica_detalle_contenido OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarHistoriaClinicaDetalleConte(OBJ, ref MsgRes);
        }
        public List<odont_historia_clinica> GetHistoriaClinica()
        {
            return DACConsulta.GetHistoriaClinica();
        }

        public List<odont_historia_clinica_paciente> GetHistoriaClinicaPaciente(Int32 id_odont_historia_clinica, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetHistoriaClinicaPaciente(id_odont_historia_clinica, ref MsgRes);
        }
        public List<vw_odont_historia_clinica_detalle> GetVWHistoriaClinicaDetalle(Int32 id_odont_historia_clinica_paciente, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetVWHistoriaClinicaDetalle(id_odont_historia_clinica_paciente, ref MsgRes);
        }
        public List<vw_odont_historia_clinica_detalle_contenido> GetVWHistoriaClinicaDetalleConten(Int32 id_odont_historia_clinica_paciente, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetVWHistoriaClinicaDetalleConten(id_odont_historia_clinica_paciente, ref MsgRes);
        }

        public List<Ref_odont_hc_calidad_formal> GetHistoriaClinicaRefCalidadFormal()
        {
            return DACConsulta.GetHistoriaClinicaRefCalidadFormal();
        }

        public List<Ref_odont_hc_calidad_contenido> GetHistoriaClinicaRefContenido()
        {
            return DACConsulta.GetHistoriaClinicaRefContenido();
        }

        public void ActualizarOdontHCdtll1(odont_historia_clinica_detalle_calidad obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarOdontHCdtll1(obj2, ref MsgRes);

        }

        public void ActualizarOdontHCdtll2(odont_historia_clinica_detalle_contenido obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarOdontHCdtll2(obj2, ref MsgRes);
        }

        public void ActualizarOdontHCdtllFinal(odont_historia_clinica_paciente obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarOdontHCdtllFinal(obj2, ref MsgRes);

        }

        public List<Ref_odont_prestadores> GetPrestadoresOdont()
        {
            return DACConsulta.GetPrestadoresOdont();
        }

        public Int32 InsertarPlanMejora(odont_plan_mejora OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejora(OBJ, ref MsgRes);
        }

        public Int32 InsertarPlanMejoradetalle(odont_plan_mejora_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPlanMejoradetalle(OBJ, ref MsgRes);
        }

        public List<vw_odont_totales_hc> GetOdontogTotales(Int32 id_odont_historia_clinica, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetOdontogTotales(id_odont_historia_clinica, ref MsgRes);
        }

        public List<vw_odont_detalle_plan_mejora> GetOdontogdetallePlanMejora()
        {
            return DACConsulta.GetOdontogdetallePlanMejora();
        }

        public List<vw_odont_tableros_ortodoncia> GetOdontogTablerosOrtodoncia()
        {
            return DACConsulta.GetOdontogTablerosOrtodoncia();
        }

        public List<vw_odont_tableros_ProtesisFija> GetOdontogTablerosPT()
        {
            return DACConsulta.GetOdontogTablerosPT();
        }

        public List<vw_odont_tableros_ProtesisRemov> GetOdontogTablerosPR()
        {
            return DACConsulta.GetOdontogTablerosPR();
        }

        public List<vw_odont_tableros_endodoncia> GetOdontogTablerosEndodoncia()
        {
            return DACConsulta.GetOdontogTablerosEndodoncia();
        }

        public List<Ref_odont_parametros_auditados_rem> GetparametrosRem()
        {
            return DACConsulta.GetparametrosRem();
        }

        public List<Ref_odont_tratamiento_rem> GetTratamientosRem()
        {
            return DACConsulta.GetTratamientosRem();
        }

        public List<vw_odont_tableros_plan_mejora> GetOdontogTablerosPlanMejora()
        {
            return DACConsulta.GetOdontogTablerosPlanMejora();

        }

        public List<vw_odont_consolidado_hc> GetConsolidadoHc(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsolidadoHc(ref MsgRes);
        }

        public List<vw_odont_consolidado_hc_prestador> GetConsolidadoHcporprestador(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetConsolidadoHcporprestador(ref MsgRes);
        }

        public List<vw_odont_count_planes_mejora> GetListCountPlanesMejora(int idregional)
        {
            return DACConsulta.GetListCountPlanesMejora(idregional);
        }

        public List<vw_odont_count_acciones_mejora> GetListCountAccionesMejora(int idregional)
        {
            return DACConsulta.GetListCountAccionesMejora(idregional);
        }

        public void InsertarRemisionesEspecialidades(odont_remisiones_especialidades obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarRemisionesEspecialidades(obj, ref MsgRes);
        }

        public List<vw_odont_remisiones_especialidades> GetRemisiones(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetRemisiones(ref MsgRes);
        }

        public void InsertarRemisionesVerificadas(odont_remisiones_verificadas obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarRemisionesVerificadas(obj, ref MsgRes);
        }

        public List<vw_odont_remisiones_verificadas> GetRemisionesVerificadas(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetRemisionesVerificadas(ref MsgRes);
        }
        public List<vw_totales_odont> GetTotalPaciente(Int32 id_odont_historia_clinica, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetTotalPaciente(id_odont_historia_clinica, ref MsgRes);
        }

        public List<vw_reportesTratamientosUnificados> GetReportTratamientosUnificados(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetReportTratamientosUnificados(ref MsgRes);
        }

        public void InsertarprestadorOdontologia(Ref_odont_prestadores obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarprestadorOdontologia(obj, ref MsgRes);
        }
        public List<vw_prestadores_odontologiaUnificado> GetPrestadoresOdonUnificados(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetPrestadoresOdonUnificados(ref MsgRes);
        }

        #endregion

        #region FFMM

        public Int32 InsertarFFMMRadicacion(ffmm_Cuentas_radicacion OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFFMMRadicacion(OBJ, ref MsgRes);
        }

        public List<vw_ffmm_consulta_radicacion_ips> GetOdontogRadicacionIPS()
        {
            return DACConsulta.GetOdontogRadicacionIPS();
        }

        public List<ffmm_Cuentas_radicacion> GetRadicacionIPSFacturas(Int32 id_proveedor, Int32 id_factura, string prefijo, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetRadicacionIPSFacturas(id_proveedor, id_factura, prefijo, ref MsgRes);
        }

        public Int32 InsertarFFMMAuditoria(ffmm_Cuentas_auditoria OBJ, List<ffmm_cuentas_medicas_cups> obj2, List<ffmm_cuentas_medicas_glosas> obj3, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFFMMAuditoria(OBJ, obj2, obj3, ref MsgRes);
        }

        public List<management_CupsAuditoriaResult> ListaCupsAuditoria()
        {
            return DACConsulta.ListaCupsAuditoria();
        }

        public void ActualizarEstadoRadicacion(ffmm_Cuentas_radicacion obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEstadoRadicacion(obj2, ref MsgRes);
        }
        public List<ffmm_Cuentas_auditoria> GetIPSTotal(Int32 id_ref_ffmm_radicacion_Cuentas, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetIPSTotal(id_ref_ffmm_radicacion_Cuentas, ref MsgRes);
        }

        public Int32 InsertarFFMMAuditoriaGlosas(ffmm_cuentas_glosas OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFFMMAuditoriaGlosas(OBJ, ref MsgRes);
        }
        public List<ffmm_cuentas_glosas> GetIPSTotalGlosas(Int32 id_ref_ffmm_radicacion_Cuentas, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetIPSTotalGlosas(id_ref_ffmm_radicacion_Cuentas, ref MsgRes);
        }

        public void ActualizarEstadoGlosa(ffmm_cuentas_glosas obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEstadoGlosa(obj2, ref MsgRes);
        }

        public void ActualizarEstadoGlosaSegundaConci(ffmm_cuentas_glosas obj2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEstadoGlosaSegundaConci(obj2, ref MsgRes);
        }
        public Int32 InsertarFFMMref_proveedor(Ref_ffmm_prestadores_proveedor OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFFMMref_proveedor(OBJ, ref MsgRes);
        }

        public Int32 InsertarFFMMref_paliativos(ffmm_cuidados_paliativos OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFFMMref_paliativos(OBJ, ref MsgRes);
        }


        public int InsertarContratosFFMM(ffmm_contratos obj)
        {
            return DACInserta.InsertarContratosFFMM(obj);
        }

        public int InsertarCargueMasivoPagoFactura(List<ffmm_cargue_facturas_pago> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCargueMasivoPagoFactura(List, ref MsgRes);
        }
        public List<management_facturas_pagosResult> ListFacturasPago()
        {
            return DACConsulta.ListFacturasPago();
        }


        #endregion

        #endregion

        #region CONFIGURACION
        public sis_configuracion GetParametro(string parametro)
        {
            return DACConsulta.GetParametro(parametro);
        }
        #endregion

        #region COVID19


        public Int32 InsertarSeguimientoDetalleCovid19(List<cargue_seguimiento_covid19_detalle> OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarSeguimientoDetalleCovid19(OBJ, ref MsgRes);
        }

        public Int32 InsertarConsolidadoSeguimientoCovid19(List<cargue_seguimiento_covid19> OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarConsolidadoSeguimientoCovid19(OBJ, ref MsgRes);
        }


        public Int32 InsertarSeguimientoCovid19Detalle(cargue_seguimiento_covid19_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarSeguimientoCovid19Detalle(OBJ, ref MsgRes);
        }


        public List<cargue_seguimiento_covid19> ConsultaIdSeguimientoCovid19(Int32 ID, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdSeguimientoCovid19(ID, ref MsgRes);
        }

        public List<vw_seguimiento_covid19_detalle> ConsultaIdSeguimientoCovid19Detalle(Int32 ID, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdSeguimientoCovid19Detalle(ID, ref MsgRes);
        }

        public List<vw_seguimiento_covid19_ultimo_detalle> ConsultaIdSeguimientoCovid19DetalleUltimo(Int32 ID, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaIdSeguimientoCovid19DetalleUltimo(ID, ref MsgRes);
        }


        public List<cargue_seguimiento_covid19> ConsultaDocumentoPacienteCovid19(String ID)
        {
            return DACConsulta.ConsultaDocumentoPacienteCovid19(ID);
        }
        public List<cargue_seguimiento_covid19> ConsultaCargueCovid19(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCargueCovid19(ref MsgRes);
        }

        public List<cargue_seguimiento_covid19_detalle> ConsultaDetalleSeguimientoCovid19(Int32 id_cargue, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaDetalleSeguimientoCovid19(id_cargue, ref MsgRes);
        }

        public List<vw_seguimiento_covid19_diario> ConsultaListadoseguimientoCovid19()
        {
            return DACConsulta.ConsultaListadoseguimientoCovid19();
        }

        public List<vw_seguimiento_covid19_interdiario> ConsultaListadoseguimientoInterdiarioCovid19()
        {
            return DACConsulta.ConsultaListadoseguimientoInterdiarioCovid19();
        }

        public List<vw_seguimiento_covid19_casos_cerrados> ConsultaListadoseguimientoCerradosCovid19()
        {
            return DACConsulta.ConsultaListadoseguimientoCerradosCovid19();
        }

        public List<ref_covid19_tipificacion> ConsultaListadoTipicacionCovid19()
        {
            return DACConsulta.ConsultaListadoTipicacionCovid19();
        }

        public List<ref_covid19_tipificacion7_detalle> ConsultaListadoTipicacion7Covid19()
        {
            return DACConsulta.ConsultaListadoTipicacion7Covid19();
        }


        public void ActualizarEstadoSeguimientoCovid19(cargue_seguimiento_covid19 OBJ2, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEstadoSeguimientoCovid19(OBJ2, ref MsgRes);
        }


        public void Actualizacasocarguecovid19(cargue_seguimiento_covid19 OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.Actualizacasocarguecovid19(OBJ, ref MsgRes);
        }


        public List<ref_covid19_estado_asalud> Consultaestadoasaludcovid19()
        {
            return DACConsulta.Consultaestadoasaludcovid19();
        }



        public List<vw_seguimiento_covid19_descarga_diaria> ConsultaListadoseguimientodescargaCovid19()
        {
            return DACConsulta.ConsultaListadoseguimientodescargaCovid19();
        }


        public List<vw_seguimiento_covid19_descarga_interdiaria> ConsultaListadoseguimientointerdiariodescargaCovid19()
        {
            return DACConsulta.ConsultaListadoseguimientointerdiariodescargaCovid19();
        }

        public List<vw_seguimiento_covid19_descarga_casos_cerrados> ConsultaListadoseguimientoCasosCerradosdescargaCovid19()
        {
            return DACConsulta.ConsultaListadoseguimientoCasosCerradosdescargaCovid19();
        }


        public List<vw_seguimiento_covid19_general_detalle> Consultageneraldetalleseguimientocovid()
        {
            return DACConsulta.Consultageneraldetalleseguimientocovid();
        }





        #endregion

        #region METODOS SIN AUTOR
        public List<vw_md_tablero_interventoria_general> Getinterventoriagneral()
        {
            return DACConsulta.Getinterventoriagneral();
        }

        public List<vw_md_tablero_intenventoria_general_detalle1> Detalleinterventoria1(Int32 ID)
        {
            return DACConsulta.Detalleinterventoria1(ID);
        }

        public List<vw_md_tablero_interventoria_general_detalle2> Detalleinterventoria2(Int32 ID)
        {
            return DACConsulta.Detalleinterventoria2(ID);
        }
        public List<vw_md_tablero_interventoria_general_detalle3> Detalleinterventoria3(Int32 ID)
        {
            return DACConsulta.Detalleinterventoria3(ID);
        }
        public List<vw_md_tablero_interventoria_general_detalle4> Detalleinterventoria4(Int32 ID)
        {
            return DACConsulta.Detalleinterventoria4(ID);
        }

        public int InsertarHospitalizacionPrevenibleDtll(ecop_hospitalizacion_prevenible_dtll obj)
        {
            return DACInserta.InsertarHospitalizacionPrevenibleDtll(obj);
        }
        public List<management_vigilancia_epidemiologica_tableroResult> GetVigilanciaEpidemiologica()
        {
            return DACConsulta.GetVigilanciaEpidemiologica();
        }
        public int InsertarVigilanciaEpidemiologica_archivos(ecop_VE_gestion_documental obj)
        {
            return DACInserta.InsertarVigilanciaEpidemiologica_archivos(obj);
        }
        public int InsertarGestionAlertaEpidemio(alerta_epidemiologica_gestion obj)
        {
            return DACInserta.InsertarGestionAlertaEpidemio(obj);
        }
        public int InsertarVigilanciaEpidemiologica(ecop_vigilancia_epidemiologica obj)
        {
            return DACInserta.InsertarVigilanciaEpidemiologica(obj);
        }
        public Int32 InsertarArchivoHospitalziacionEvitable(ecop_HE_gestion_documental obj)
        {
            return DACInserta.InsertarArchivoHospitalziacionEvitable(obj);
        }
        public List<ref_he_analisisCaso> ListAnalisisCasoHE()
        {
            return DACConsulta.ListAnalisisCasoHE();
        }
        public List<ref_he_analisisCaso_si> ListAnalisisCasoHESi()
        {
            return DACConsulta.ListAnalisisCasoHESi();
        }
        public List<ref_he_analisisCaso_no> ListAnalisisCasoHENo()
        {
            return DACConsulta.ListAnalisisCasoHENo();
        }
        public List<management_hospitalizacionPrevenible_detalle_gestionResult> GetHospitalizacionPrevenibleDetalle_gestion(int idHE)
        {
            return DACConsulta.GetHospitalizacionPrevenibleDetalle_gestion(idHE);
        }
        public ecop_HE_gestion_documental buscarArchivoHEDtll(int HEDtll)
        {
            return DACConsulta.buscarArchivoHEDtll(HEDtll);
        }
        public ecop_VE_gestion_documental buscarArchivoVE(int idVE, int tipo)
        {
            return DACConsulta.buscarArchivoVE(idVE, tipo);
        }
        public List<management_hospitalizacionPrevenible_reporteResult> GetHospitalizacionPrevenible_Reporte()
        {
            return DACConsulta.GetHospitalizacionPrevenible_Reporte();
        }
        public List<vw_md_datos_comunicado> GetDatosComunicado()
        {
            return DACConsulta.GetDatosComunicado();
        }

        public List<md_comunicaciones> TraerdocumentoComunicados(Int32 ID)
        {
            return DACConsulta.TraerdocumentoComunicados(ID);
        }


        public GestionDocumentalMedicamentosCad Traerimagenindicacioes(Int32 ID)
        {
            return DACConsulta.Traerimagenindicacioes(ID);
        }

        public List<vw_md_consolidado_sin_auditor> Getfactursinauditor()
        {
            return DACConsulta.Getfactursinauditor();
        }


        public ManagmentIngresarIncapacidadResult GetAsignarAuditorConsolidado(String factura, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetAsignarAuditorConsolidado(factura, ref MsgRes);
        }

        //Codigo facturas rechazadas
        public List<managmentprestadoresFacturasRechazadasResult> GetFacturasRechazadas(string factura, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturasRechazadas(factura, ref MsgRes);
        }

        public int CerrarAlertaEpidemiologica(int? idRegistro, string usuario)
        {
            return DACActualiza.CerrarAlertaEpidemiologica(idRegistro, usuario);
        }

        public List<ref_tipo_beneficiario> ListadoTipoBeneficiarios()
        {
            return DACComonClass.ListadoTipoBeneficiarios();
        }

        #endregion

        #region FACTURACION

        public void InsertarCargueContratacion(contratacion_cargue_base obj, List<contratacion_cargue_dtll> ListaContratacion, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCargueContratacion(obj, ListaContratacion, ref MsgRes);
        }

        public contratacion_cargue_base getcarguecontratacion(int mes, int año)
        {
            return DACConsulta.getcarguecontratacion(mes, año);
        }

        public ecop_gestion_facturas_aprobadas GetFacturasAprobadas(int id_cargue_dtll)
        {
            return DACConsulta.GetFacturasAprobadas(id_cargue_dtll);
        }

        public List<managementFacturasanalistas_lotesResult> GetFacturaAnalistas(String usuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturaAnalistas(usuario, ref MsgRes);
        }

        public List<managementFacturasanalistas_lotes_okResult> GetFacturaAnalistasok(int? idRol, string usuario, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFacturaAnalistasok(idRol, usuario, ref MsgRes);
        }
        public List<Management_Lotes_totales_con_analistaResult> GetLotesAnalistaTotal(DateTime fecha_inicio, DateTime fecha_fin, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetLotesAnalistaTotal(fecha_inicio, fecha_fin, ref MsgRes);
        }


        public List<Management_Lotes_totales_con_analistaDtllResult> GetLotesAnalistaTotalDtll(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetLotesAnalistaTotalDtll(Id, ref MsgRes);
        }


        public Int32 InsertarFirmadigital(ecop_firma_digital_cod_barras obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFirmadigital(obj, ref MsgRes);
        }
        public ecop_firma_digital_cod_barras GetDtll_codBarras(Int32? idDetalle)
        {
            return DACConsulta.GetDtll_codBarras(idDetalle);
        }
        public Management_consulta_QRResult GetConsultaQr(Int32? idDetalle)
        {
            return DACConsulta.GetConsultaQr(idDetalle);
        }
        public Int32 InsertarFirmadigitalsami(ecop_firma_digital_sami obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFirmadigitalsami(obj, ref MsgRes);
        }
        public List<vw_odontologia_detallado_historia_clinica> getdetallehistoriaclinica()
        {
            return DACConsulta.getdetallehistoriaclinica();
        }

        public Int32 InsertarGestionFacturadigital(ecop_gestion_factura_digital obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGestionFacturadigital(obj, ref MsgRes);
        }
        public Int32 InsertarGestionFacturadigitalGasto(ecop_gestion_factura_digital_gasto obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGestionFacturadigitalGasto(obj, ref MsgRes);
        }

        public void insertarListadoGestionFacturadigitalGasto(List<ecop_gestion_factura_digital_gasto> obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.insertarListadoGestionFacturadigitalGasto(obj, ref MsgRes);
        }

        public void ActualizarGestionFacturadigitalGasto(vw_factura_digital_gasto_total obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarGestionFacturadigitalGasto(obj, ref MsgRes);
        }

        public List<ref_tipo_gasto_facturas> Getref_tipo_gasto_facturas(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.Getref_tipo_gasto_facturas(ref MsgRes);
        }

        public ecop_firma_digital_sami GetFirmas(Int32? idusuario)
        {
            return DACComonClass.GetFirmas(idusuario);
        }

        public management_ecop_firma_digital_samiResult GetFirmasId(int? idUsuario)
        {
            return DACComonClass.GetFirmasId(idUsuario);
        }

        public ecop_firma_digital_sami traerDatosFirma(int? idUsuario)
        {
            return DACConsulta.traerDatosFirma(idUsuario);
        }

        public List<management_ecop_firma_digital_sami_todoResult> ListadoFirmasSinRuta()
        {
            return DACConsulta.ListadoFirmasSinRuta();
        }
        public List<ecop_firma_digital_sami> listaFirmasUsuarios()
        {
            return DACComonClass.listaFirmasUsuarios();
        }

        public void ActualizarRutaFirmasDigital(string ruta, int? idFirma)
        {
            DACActualiza.ActualizarRutaFirmasDigital(ruta, idFirma);
        }

        public int cantidaddias(int idconcurrencia)
        {
            return DACConsulta.cantidaddias(idconcurrencia);
        }


        public void ActualizarAuditorConcurrencia(ecop_concurrencia OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarAuditorConcurrencia(OBJ, ref MsgRes);
        }

        public void ActualizarAuditorCenso(ecop_censo OBJ, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarAuditorCenso(OBJ, ref MsgRes);
        }

        public List<ManagmentDetalleFacturasDevueltasResult> GetDetalleFacturadevuletas(int id_detalle)
        {
            return DACConsulta.GetDetalleFacturadevuletas(id_detalle);
        }

        public List<ManagmentDetalleFacturasDevueltas_fisResult> GetDetalleFacturadevuletas_FIS(int id_detalle)
        {
            return DACConsulta.GetDetalleFacturadevuletas_FIS(id_detalle);
        }

        public List<view_ref_estado_facturas> GetEstadoFacturas()
        {
            return DACConsulta.GetEstadoFacturas();
        }

        public Int32 InsertarLogCambiosGetionHospitalaria(log_cambios_gestion_hospitalaria obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarLogCambiosGetionHospitalaria(obj, ref MsgRes);
        }


        public void ActualizarCambiosPacienteCenso(ecop_censo OBJ, String tipocambio, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarCambiosPacienteCenso(OBJ, tipocambio, ref MsgRes);
        }

        public void ActualizarCambiosPacienteConcu(ecop_concurrencia OBJ, String tipocambio, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarCambiosPacienteConcu(OBJ, tipocambio, ref MsgRes);
        }

        public int ActualizarEstanciaEvolucion(ecop_concurrencia_evolucion obj)
        {
            return DACActualiza.ActualizarEstanciaEvolucion(obj);
        }

        public int InsertarLogCambioEstanciaEvolucion(log_ecop_concurrencia_evolucion_habitacion obj)
        {
            return DACInserta.InsertarLogCambioEstanciaEvolucion(obj);
        }

        public List<management_egresos_categorizacionResult> listadoEgresosCategorizacion(int idConcurrencia)
        {
            return DACConsulta.listadoEgresosCategorizacion(idConcurrencia);
        }

        public List<management_alertas_epidemiologicas_tableroControlResult> ListadoAlertasEpidemiologicas()
        {
            return DACConsulta.ListadoAlertasEpidemiologicas();
        }

        public List<vw_reporte_alertas_epidemiologia> ListadoAlertasEpidemiologicasReporte()
        {
            return DACConsulta.ListadoAlertasEpidemiologicasReporte();
        }

        public List<vw_reporte_alertas_epidemiologia_gestiones> ListadoAlertasEpidemiologicasReporteGestiones()
        {
            return DACConsulta.ListadoAlertasEpidemiologicasReporteGestiones();
        }

        public management_alertas_epidemiologicas_tableroControl_idGestionResult TraerDatosGestionId(int? idRegistro)
        {
            return DACConsulta.TraerDatosGestionId(idRegistro);
        }

        public List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> ListadoAlertasEpidemiologicasGestionadas()
        {
            return DACConsulta.ListadoAlertasEpidemiologicasGestionadas();
        }

        public List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> ListadoAlertasEpidemiologicasGestionadasTotal()
        {
            return DACConsulta.ListadoAlertasEpidemiologicasGestionadasTotal();
        }

        public management_alertas_epidemiologicas_gestionesResult TraerGestionAlertasEpidemiologicas(int? idRegistro)
        {
            return DACConsulta.TraerGestionAlertasEpidemiologicas(idRegistro);
        }

        public List<alerta_epidemiologica_gestion_archivos> ListadoArchivosEpidemiologicaId(int? idRegistro, int? tipo)
        {
            return DACConsulta.ListadoArchivosEpidemiologicaId(idRegistro, tipo);
        }

        public alerta_epidemiologica_gestion_archivos TraerArchivoEpidemiologicoId(int? id)
        {
            return DACConsulta.TraerArchivoEpidemiologicoId(id);
        }

        public List<ref_AE_tiposDemoras> ListadoAE()
        {
            return DACConsulta.ListadoAE();
        }

        public List<ref_AE_tiposDemoras_detallado> ListadoAEDetallado()
        {
            return DACConsulta.ListadoAEDetallado();
        }

        public List<management_alertaEpidemiologica_reporteIdResult> TraerGestionAnalisis(int? idGestion)
        {
            return DACConsulta.TraerGestionAnalisis(idGestion);
        }

        public List<management_alertaEpidemiologica_reporteId_demorasResult> TraerGestionAnalisis_demoras(int? idGestion)
        {
            return DACConsulta.TraerGestionAnalisis_demoras(idGestion);
        }

        public List<management_alertas_epidemiologicas_descargableArchivos_documentosResult> TraerDocumentosArchivosGestionAE(int? regional, int? localidad, int? unis, DateTime? fechaAlertaIni, DateTime? fechaAlertaFin,
    string documentoPaciente, int? idConcurrencia, DateTime? fechaIngresoIni, DateTime? fechaIngresoFin, int? conEgreso)
        {
            return DACConsulta.TraerDocumentosArchivosGestionAE(regional, localidad, unis, fechaAlertaIni, fechaAlertaFin, documentoPaciente, idConcurrencia, fechaIngresoIni, fechaIngresoFin, conEgreso);

        }
        public List<management_alertas_epidemiologicas_descargableArchivosResult> TraerArchivosGestionAE(string documentoPaciente)
        {
            return DACConsulta.TraerArchivosGestionAE(documentoPaciente);
        }


        public int EliminarArchivoEpidemiologico(int? idArchivo)
        {
            return DACElimina.EliminarArchivoEpidemiologico(idArchivo);
        }
        public int InsertarMasivoArchivosAlertasEpidemiologicas(List<alerta_epidemiologica_gestion_archivos> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarMasivoArchivosAlertasEpidemiologicas(detalle, ref MsgRes);
        }

        public int InsertarGestionRequiereAnlisis(alerta_epidemiologica_gestion_gestionAnalisis obj)
        {
            return DACInserta.InsertarGestionRequiereAnlisis(obj);
        }

        public int InsertrMasivogestionesDemorasAE(List<alerta_epidemiologica_gestion_gestionAnalisis_demoras> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertrMasivogestionesDemorasAE(detalle, ref MsgRes);
        }

        public int ActualizarGestionAnalisisAE(alerta_epidemiologica_gestion_gestionAnalisis obj)
        {
            return DACActualiza.ActualizarGestionAnalisisAE(obj);
        }

        public int EliminarDemorasEpidemiologicas(int? idGestion)
        {
            return DACElimina.EliminarDemorasEpidemiologicas(idGestion);
        }
        public alerta_epidemiologica_gestion_gestionAnalisis TraerGestionAEId(int? idGestion)
        {
            return DACConsulta.TraerGestionAEId(idGestion);
        }

        public List<alerta_epidemiologica_gestion_gestionAnalisis_demoras> TraerGestionAEIdDemoras(int? idGestion)
        {
            return DACConsulta.TraerGestionAEIdDemoras(idGestion);
        }

        public List<management_alertas_epidemiologicas_alerta_epidemiologica_gestion_gestionAnalisisDetalladoResult> TraerGestionAEIdDemorasDetallado(int? idGestion)
        {
            return DACConsulta.TraerGestionAEIdDemorasDetallado(idGestion);
        }

        public List<management_evoluciones_listadoIdConcurrenciaResult> TraerEvolucionesIdConcurrencia(int? idConcurrencia)
        {
            return DACConsulta.TraerEvolucionesIdConcurrencia(idConcurrencia);
        }


        public Int32 InsertarFacturaAprobadas(ecop_gestion_facturas_aprobadas obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturaAprobadas(obj, ref MsgRes);
        }


        public List<vw_analistas_recepcion> GetListAnalistas()
        {
            return DACConsulta.GetListAnalistas();
        }


        public void Insertaranalistalote(ref_analista_lote obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.Insertaranalistalote(obj, ref MsgRes);
        }
        public List<managmentprestadoresFacturasOBSResult> GetConsultaObsFactura(Int32? id_af)
        {
            return DACConsulta.GetConsultaObsFactura(id_af);
        }
        public List<managmentprestadoresfacturasDEV_RECHResult> GetConsultaRechDevFactura()
        {
            return DACConsulta.GetConsultaRechDevFactura();
        }

        public List<managmentprestadoresfacturasDEV_RECHV2Result> GetConsultaRechDevFacturaV2(int? idfactura)
        {
            return DACConsulta.GetConsultaRechDevFacturaV2(idfactura);
        }

        public List<getfacturabynumfactura_idprestadorResult> ValidarEvistenciaFactura(int idfactura, string numnuevofactura, string numidprestador)
        {
            return DACConsulta.ValidarEvistenciaFactura(idfactura, numnuevofactura, numidprestador);
        }

        public List<ecop_gestion_factura_digital> GetConsultaGestionFactura(Int32? idDetalle)
        {
            return DACConsulta.GetConsultaGestionFactura(idDetalle);
        }
        public List<factura_devolucion> GetConsultaGestionDevolucion(Int32? idDetalle)
        {
            return DACConsulta.GetConsultaGestionDevolucion(idDetalle);
        }
        public List<managmentprestadoresfacturasACEP_ASIGResult> GetConsultaAcep_AsigFactura()
        {
            return DACConsulta.GetConsultaAcep_AsigFactura();
        }
        public List<managmentprestadoresNumeroFacturaResult> GetConsultaNumeroFactura(String numeroFac)
        {
            return DACConsulta.GetConsultaNumeroFactura(numeroFac);
        }

        public List<factura_devolucion> GetfacturadevolucionByIdFactura(int idfactura)
        {
            return DACConsulta.GetfacturadevolucionByIdFactura(idfactura);
        }

        public Int32 InsertarFacturacionContabilizado(List<ecop_gestion_factura_digital_contabilizados> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFacturacionContabilizado(OBJDetalle, ref MsgRes);
        }
        public Int32 InsertarControlCambios(ecop_gestion_factura_digital_control_cambios OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarControlCambios(OBJ, ref MsgRes);
        }
        public int ActualizarEstado_Facturas(int idFac, int estadoAntiguo, int estadoNuevo)
        {
            return DACActualiza.ActualizarEstado_Facturas(idFac, estadoAntiguo, estadoNuevo);
        }

        public List<md_prefacturas_detalle> GetPrefacturasByIdLote(int lote)
        {
            return DACConsulta.GetPrefacturasByIdLote(lote);
        }

        public management_prefacturas_existeBeneficiarioResult PrefacturasExisteBeneficiario(string numeroBeneficiario, DateTime fechaDespachoFormula)
        {
            return DACConsulta.PrefacturasExisteBeneficiario(numeroBeneficiario, fechaDespachoFormula);
        }

        public string PrefacturasExisteFactura(string numeroBeneficiario, int numeroUnidades, DateTime fechaDespachoFormula, decimal vlrUnidades
            , string cum, string nombreComercial)
        {
            return DACConsulta.PrefacturasExisteFactura(numeroBeneficiario, numeroUnidades, fechaDespachoFormula, vlrUnidades, cum, nombreComercial);
        }

        public management_prefacturas_regionalBeneficiarioResult PrefacturasRegionalBeneficiario(string numeroBeneficiario, DateTime fechaDespachoFormula, string nombreEspecial)
        {
            return DACConsulta.PrefacturasRegionalBeneficiario(numeroBeneficiario, fechaDespachoFormula, nombreEspecial);
        }

        public void ActualizarPrefacturaCargue(int? cargueBase, int? fase, string usuario, int? terminado)
        {
            DACActualiza.ActualizarPrefacturaCargue(cargueBase, fase, usuario, terminado);
        }
        public void ActualizarPrefacturaCargueFase(int? cargueBase, int? fase, string usuario)
        {
            DACActualiza.ActualizarPrefacturaCargueFase(cargueBase, fase, usuario);
        }

        public int ActualizarPrefacturaCargueFaseDevolver(int? cargueBase)
        {
            return DACActualiza.ActualizarPrefacturaCargueFaseDevolver(cargueBase);
        }


        public int ActualizarConteo_Facturas_fase1(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_Facturas_fase1(idCargue, usuarioDigita, ref MsgRes);
        }

        public int ActualizarConteo_Facturas_fase2(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_Facturas_fase2(idCargue, usuarioDigita, ref MsgRes);
        }

        public int ActualizarConteo_Facturas_fase2_2(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_Facturas_fase2_2(idCargue, usuarioDigita, ref MsgRes);
        }

        //public async Task<int> ActualizarConteo_Facturas_fase2Async(int idCargue, string usuarioDigita)
        //{
        //    return await DACActualiza.ActualizarConteo_Facturas_fase2Async(idCargue, usuarioDigita);
        //}

        public int ActualizarConteo_Facturas_fase3(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_Facturas_fase3(idCargue, usuarioDigita, ref MsgRes);
        }


        public int ActualizarConteo_FacturasInicial(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_FacturasInicial(idCargue, ref MsgRes);
        }

        public int ActualizarConteo_FacturasUno(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_FacturasUno(idCargue, usuarioDigita, ref MsgRes);
        }

        public int ActualizarConteo_Facturas(int idCargue, string usuarioDigita, int? tipo, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_Facturas(idCargue, usuarioDigita, tipo, ref MsgRes);
        }

        public List<management_prefacturas_reporteCierreResult> ReportePrefacturasCerradas(int? idCargue)
        {
            return DACConsulta.ReportePrefacturasCerradas(idCargue);
        }

        public int ActualizarConteo_Facturas2(int idCargue, string usuario, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_Facturas2(idCargue, usuario, ref MsgRes);
        }

        public int ActualizarConteo_Facturas3(int idCargue, string usuarioValida, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_Facturas3(idCargue, usuarioValida, ref MsgRes);
        }

        public int ActualizarConteo_Facturas4(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_Facturas4(idCargue, ref MsgRes);
        }

        public int ActualizarConteo_Facturas5(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarConteo_Facturas5(idCargue, ref MsgRes);
        }

        public management_prefacturas_buscarEnHistoricoPrefacturasResult BuscarHistoricoPrefacturas(string num_documento_beneficiario, string cum,
        string nombre_comercial_medicacmento, string num_unidades_entregadas, DateTime fecha_despacho_formula, string vlr_unitario_und_entregada)
        {
            return DACConsulta.BuscarHistoricoPrefacturas(num_documento_beneficiario, cum, nombre_comercial_medicacmento, num_unidades_entregadas, fecha_despacho_formula, vlr_unitario_und_entregada);
        }

        public md_prefactura_contador TraerDatosContadorPrefacturas(int idDetallePrefactura)
        {
            return DACConsulta.TraerDatosContadorPrefacturas(idDetallePrefactura);
        }
        public List<management_Validador_datosCorreosResult> ListadoCorreosValidadorOPL(int? idRegional)
        {
            return DACConsulta.ListadoCorreosValidadorOPL(idRegional);
        }
        public List<management_prestadores_regionalResult> GetPrestadoresRegional(int idRegional)
        {
            return DACConsulta.GetPrestadoresRegional(idRegional);
        }


        public List<vw_factura_digital_gasto_total> GetGatosFactura(int id)
        {
            return DACConsulta.GetGatosFactura(id);
        }

        public List<managementprestadores_alertas_activasResult> GetConsultaAlertasactivas()
        {
            return DACConsulta.GetConsultaAlertasactivas();
        }

        public List<managmentprestadoresfacturasgestionadasdtllResult> GetListFacturasByNumFactura(string numfactura)
        {
            return DACConsulta.GetListFacturasByNumFactura(numfactura);
        }
        public List<managmentprestadoresfacturasgestionadasdtllCompletaResult> GetListFacturasByNumFacturaCompleta(String numFac, String nit, String prestador, String sap, int? idFactura)
        {
            return DACConsulta.GetListFacturasByNumFacturaCompleta(numFac, nit, prestador, sap, idFactura);

        }

        public ManagementPrestadoresFacturasByIdDtllResult GetInfoFacturaById(int idcarguedtll)
        {
            return DACConsulta.GetInfoFacturaById(idcarguedtll);
        }

        public List<managmentprestadoresFacturas_analistasResult> prestadoresFacturas_analistas()
        {
            return DACConsulta.prestadoresFacturas_analistas();
        }

        public List<managmentprestadoresFacturas_auditoresResult> prestadoresFacturas_auditores()
        {
            return DACConsulta.prestadoresFacturas_auditores();
        }


        public Int32 InsertarGestionAnalista(ref_cuentas_medicas_analista OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGestionAnalista(OBJ, ref MsgRes);
        }

        public List<vw_recep_facturas_cargue_base> GetHistoricoRadicacionById(int idcargue)
        {
            return DACConsulta.GetHistoricoRadicacionById(idcargue);
        }

        public List<ManagmentFacturasV2Result> GetFacturasByRecepcionV2(int idrecepcion)
        {
            return DACConsulta.GetFacturasByRecepcionV2(idrecepcion);
        }

        public Int32 InsertarGestionAuditor(ref_cuentas_medicas_auditores OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGestionAuditor(OBJ, ref MsgRes);

        }

        public void ActualizaAnalistaAsignado(ref_cuentas_medicas_analista ObjAudi, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaAnalistaAsignado(ObjAudi, ref MsgRes);
        }

        public void ActualizaAuditorAsignado(ref_cuentas_medicas_auditores ObjAudi, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizaAuditorAsignado(ObjAudi, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 17-nov-2022
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void InsertarLogBusquedaTableros(log_busquedas_tableros_facturas obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarLogBusquedaTableros(obj, ref MsgRes);
        }


        #endregion

        #region GESTION INTERNA

        public List<ref_gestion_interna> GetGestionInternaList()
        {
            return DACConsulta.GetGestionInternaList();
        }

        public ref_gestion_interna GetGestionInternaById(int idgestion)
        {
            return DACConsulta.GetGestionInternaById(idgestion);
        }

        public List<vw_odont_historia_clinica> ListHistoriaClinica(int id_historia)
        {
            return DACConsulta.ListHistoriaClinica(id_historia);
        }

        public List<vw_odont_historia_clinica> GetListHistoriaClinicaXOdontologo(string nomodontologo)
        {
            return DACConsulta.GetListHistoriaClinicaXOdontologo(nomodontologo);
        }

        public void EliminarHistoriaclinica(int id_hc_paciente, log_eliminacion_historias_clinicas_odontologia obj, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarHistoriaclinica(id_hc_paciente, obj, ref MsgRes);
        }

        public void InsertarLogActualizacionFechaEgreso(log_update_fecha_egreso log, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarLogActualizacionFechaEgreso(log, ref MsgRes);
        }

        #endregion

        #region GASTOS POR SERVICIO

        public int InsertarGastosPorServicio(gasto_por_servicio_cargue_base obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarGastosPorServicio(obj, ref MsgRes);
        }

        public void InsertarGastosPorServicioDtll(List<gasto_por_Servicio_cargue_dtll> dtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarGastosPorServicioDtll(dtll, ref MsgRes);
        }

        public gasto_por_servicio_cargue_base getcarguebase(int mes, int año, string regional)
        {
            return DACConsulta.getcarguebase(mes, año, regional);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 19 de julio de 2022
        /// Metodo que obtiene el listado de cargues de gasto por servicio
        /// </summary>
        /// <returns></returns>
        public List<vw_consulta_gasto_por_servicio> ObtenerListadoCarguesGastoPorServicio()
        {
            return DACConsulta.ObtenerListadoCarguesGastoPorServicio();
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 1 de agosto de 2022
        /// Metodo para obtener el consolidado de gasto por servicio
        /// </summary>
        /// <param name="idCargueBase"></param>
        /// <returns></returns>
        public List<Management_gasto_x_servicio_consolidadoResult> ObtenerConsolidadoGastoPorServicioPorIdCargueBase(int idCargueBase)
        {
            return DACConsulta.ObtenerConsolidadoGastoPorServicioPorIdCargueBase(idCargueBase);
        }
        /// <summary>
        /// Autor: Kevin Suarez
        /// Fecha: 12 de agosto de 2022
        /// </summary>
        /// 
        public List<management_gastoxservicio_consultaResult> ObtenerGastoPorsercicioConsulta(DateTime? fechaInicio, DateTime? fechaFin, string factura, int cedula, string servicio, string tiga, DateTime? fechaInicioPre, DateTime? fechaFinPre)
        {
            return DACConsulta.ObtenerGastoPorsercicioConsulta(fechaInicio, fechaFin, factura, cedula, servicio, tiga, fechaInicioPre, fechaFinPre);
        }

        public List<management_gastoxservicio_consulta2Result> ObtenerGastoPorsercicioConsulta2(DateTime? fechaInicio, DateTime? fechaFin, string factura, string cedula, string servicio, string tiga, DateTime? fechaInicioPre, DateTime? fechaFinPre)
        {
            return DACConsulta.ObtenerGastoPorsercicioConsulta2(fechaInicio, fechaFin, factura, cedula, servicio, tiga, fechaInicioPre, fechaFinPre);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha 04/08/2022
        /// Metodo para elminar el cargue de gastos por servicio
        /// </summary>
        /// <param name="idCargue"></param>
        public int EliminarCargueGastoPorServicio(int idCargue)
        {
            return DACElimina.EliminarCargueGastoPorServicio(idCargue);
        }

        public int InsertarLogEliminarGastoxServicio(log_gastoxServicio_eliminarConsolidado obj)
        {
            return DACInserta.InsertarLogEliminarGastoxServicio(obj);
        }

        #endregion

        #region SEGUIMIENTO ENTREGABLES

        public List<seguimiento_entregables_periodo> GetListEntregablesPeriodo(int id_seg_entregable)
        {
            return DACConsulta.GetListEntregablesPeriodo(id_seg_entregable);
        }

        public seguimiento_entregables_periodo GetEntregablePeriodoById(int id_ent_periodo)
        {
            return DACConsulta.GetEntregablePeriodoById(id_ent_periodo);
        }

        public List<ref_periodicidad_entregables> GetListPeriodicidadEntregables()
        {
            return DACConsulta.GetListPeriodicidadEntregables();
        }

        public void InsertarOActualizarSeguimientoEntregable(seguimiento_entregables obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarOActualizarSeguimientoEntregable(obj, ref MsgRes);
        }

        public void InsertarSeguimientoEntregableDTLL(int id_seg_entregable, seguimiento_dtll_entrega obj, List<seguimiento_entregables_documentos> Objdocumentos, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarSeguimientoEntregableDTLL(id_seg_entregable, obj, Objdocumentos, ref MsgRes);
        }

        public Int32 InsertarSeguimientoEntregableDTLL1(int id_seg_entregable, seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarSeguimientoEntregableDTLL1(id_seg_entregable, obj, ref MsgRes);
        }

        public void InsertarSeguimientoEntregableDTLL2(List<seguimiento_entregables_documentos> lista, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarSeguimientoEntregableDTLL2(lista, ref MsgRes);
        }

        public List<vw_seguimiento_entregables> GetListEntregables(int? periodicidad)
        {
            return DACConsulta.GetListEntregables(periodicidad);
        }

        public seguimiento_dtll_entrega GetseguimientoDtllEntrega(int id_dtll)
        {
            return DACConsulta.GetseguimientoDtllEntrega(id_dtll);
        }

        public seguimiento_dtll_entrega GetseguimientoDtllEntregaPresentado(int? id_dtll)
        {
            return DACConsulta.GetseguimientoDtllEntregaPresentado(id_dtll);
        }

        public List<seguimiento_dtll_entrega> GetListseguimientoDtllEntrega(int id_seg_periodo)
        {
            return DACConsulta.GetListseguimientoDtllEntrega(id_seg_periodo);
        }

        public List<seguimiento_entregables_documentos> GetSeguimientoEntregableDocs(int id)
        {
            return DACConsulta.GetSeguimientoEntregableDocs(id);
        }
        public seguimiento_entregables_documentos traerDocumentoEntregableId(int idDocumento)
        {
            return DACConsulta.traerDocumentoEntregableId(idDocumento);
        }

        public List<managmentSeguimiento_entregables_documentosResult> GetSeguimientoEntregableDocs2(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetSeguimientoEntregableDocs2(ref MsgRes);
        }

        public seguimiento_entregables GetSeguimientoEntregable(int id)
        {
            return DACConsulta.GetSeguimientoEntregable(id);
        }

        public void InsertarSeguimientoEntregablePeriodo(seguimiento_entregables_periodo obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarSeguimientoEntregablePeriodo(obj, ref MsgRes);
        }

        public void InsertarGestionEntregable(int id_seg_entregable_periodo, seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarGestionEntregable(id_seg_entregable_periodo, obj, ref MsgRes);
        }

        public List<ref_cobertura_seguimiento_entregable> GetCoberturaSegEntregable()
        {
            return DACConsulta.GetCoberturaSegEntregable();
        }

        public void ActualizarEntregable(int id_seg_entregable_periodo, seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarEntregable(id_seg_entregable_periodo, obj, ref MsgRes);
        }



        public void GuardarRespuestaObservaciones(seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.GuardarRespuestaObservaciones(obj, ref MsgRes);
        }

        public List<ref_seguimiento_entregable_usuario_gestion> GetUsuariosSegGestion()
        {
            return DACConsulta.GetUsuariosSegGestion();
        }

        public int InsertarPeriodoEntregable(seguimiento_entregables_periodo obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarPeriodoEntregable(obj, ref MsgRes);
        }

        public int ActualizarEntregablePeriodo(seguimiento_entregables_periodo obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarEntregablePeriodo(obj, ref MsgRes);
        }

        public List<vw_seguimiento_entregables_competencias> GetSeguimientoEntregablesCompetencias()
        {
            return DACConsulta.GetSeguimientoEntregablesCompetencias();
        }

        public List<ref_proceso_entregable> Getprocesoentregable()
        {
            return DACConsulta.Getprocesoentregable();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Casstillo
        /// Fecha: 27/diciembre/2022
        /// Metodo para obtener el listado de tipo de seguimiento entregables
        /// </summary>
        /// <returns></returns>
        public List<ref_seguimiento_entregables_tipo_entregable> GetListTipoEntregable()
        {
            return DACConsulta.GetListTipoEntregable();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 28 de diciembre de 2022
        /// Metodo para obtener el listado de estado de entregables
        /// </summary>
        /// <returns></returns>
        public List<ref_estado_entregable> GetListEstadoEntregable()
        {
            return DACConsulta.GetListEstadoEntregable();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Catillo
        /// Fecha: 28 de diciembre de 2022
        /// Metodo utilizado para consultar el listado de alertas enviadas
        /// 
        /// </summary>
        /// <returns></returns>
        public List<seguimiento_entregables_alerta_diaria> GetListNotificacionesEnviadasSeguimientoEntregables(DateTime? fecha)
        {
            return DACConsulta.GetListNotificacionesEnviadasSeguimientoEntregables(fecha);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 4 de enero de 2023
        /// Metodo para consultar el listado de entregables para mostrar en el tablero de gestion 
        /// </summary>
        /// <param name="periodicidad"></param>
        /// <param name="tipoEntregable"></param>
        /// <returns></returns>
        public List<Management_seguimiento_entregables_gestionResult> GetListSeguimientoEntregableGestion(int? periodicidad, int? tipoEntregable)
        {
            return DACConsulta.GetListSeguimientoEntregableGestion(periodicidad, tipoEntregable);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 4 de enero de 2023
        /// Metodo para consultar el listado de periodos entregados y por entregar de un entregable
        /// </summary>
        /// <param name="idSeguimientoEntregable"></param>
        /// <returns></returns>
        public List<vw_seguimiento_entregables> GetListEntregablesPorIdEntregable(int? idSeguimientoEntregable)
        {
            return DACConsulta.GetListEntregablesPorIdEntregable(idSeguimientoEntregable);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 11 de enero de 2022
        /// Metodo para guardar los datos de la evaluacion de calidad realizada a un entregable.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void GuardarDatosEvalCalidadSegEntregable(seguimiento_entregables_periodo_eval_calidad obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.GuardarDatosEvalCalidadSegEntregable(obj, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 12 de enero de 2023
        /// Metodo utilizado para retornar los datos ingresados en la evaluacion de calidad de un entregable filtrado por el id periodo entregable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public seguimiento_entregables_periodo_eval_calidad ConsultarEvaluacionPorIdPeriodoSegEntregable(int id)
        {
            return DACConsulta.ConsultarEvaluacionPorIdPeriodoSegEntregable(id);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 16 de enero de 2022
        /// Metodo para obtener el listado de oportunidad  de seguimiento entregables
        /// </summary>
        /// <param name="personaResponsable"></param>
        /// <param name="tipoEntregable"></param>
        /// <param name="periodicidad"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_seguimiento_entregables_indicadoresResult> GetListadoOportunidadSeguimientoEntregables(string personaResponsable, int? tipoEntregable, int? periodicidad, int? año)
        {
            return DACConsulta.GetListadoOportunidadSeguimientoEntregables(personaResponsable, tipoEntregable, periodicidad, año);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorXPersonaResult> GetListadoIndicadoresXPersonaSeguimientoEntregables(int mesInicial, int mesFinal, int año, string responsable)
        {
            return DACConsulta.GetListadoIndicadoresXPersonaSeguimientoEntregables(mesInicial, mesFinal, año, responsable);
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorXComponenteResult> GetListadoIndicadoresXComponenteSeguimientoEntregables(int mesInicial, int mesFinal, int año, int? idProceso)
        {
            return DACConsulta.GetListadoIndicadoresXComponenteSeguimientoEntregables(mesInicial, mesFinal, año, idProceso);
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorXCompyPeridicidadResult> GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables(int mesInicial, int mesFinal, int año, int? idProceso, int? idPeriodicidad)
        {
            return DACConsulta.GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables(mesInicial, mesFinal, año, idProceso, idPeriodicidad);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 3 de febrero de 2023
        /// </summary>
        /// <param name="responsable"></param>
        /// <param name="idProceso"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorVencimientoResult> GetIndicadorDiasVencimientSegEntregables(string responsable, int? idProceso, int? año)
        {
            return DACConsulta.GetIndicadorDiasVencimientSegEntregables(responsable, idProceso, año);
        }
        public int eliminarEvaluacioEntregablesID(int? idPeriodo)
        {
            return DACElimina.eliminarEvaluacioEntregablesID(idPeriodo);
        }
        public int eliminarFelicitacionesEntregablesID(int? idPeriodo)
        {
            return DACElimina.eliminarFelicitacionesEntregablesID(idPeriodo);
        }

        #endregion

        #region CONTACT CENTER

        public List<ref_contact_clasificacion_contacto> GetListClasificacionContacto()
        {
            return DACConsulta.GetListClasificacionContacto();
        }

        public List<ref_contact_tipificacion> GetListTipificacion()
        {
            return DACConsulta.GetListTipificacion();
        }

        public List<ref_contact_tipo_servicio> GetListTipoServicio()
        {
            return DACConsulta.GetListTipoServicio();
        }

        public List<ref_contact_tipo_solicitud> GetListTipoSolicitud()
        {
            return DACConsulta.GetListTipoSolicitud();
        }

        public List<ref_contact_tipoSolicitudBitacora> GetListTipoSolicitudBitacora()
        {
            return DACConsulta.GetListTipoSolicitudBitacora();
        }
        public List<Ref_cie10> GetCie10Bycodigo(string term)
        {
            return DACConsulta.GetCie10Bycodigo(term);
        }

        public management_buscarBeneficiarios_MorNatResult TraerDatosBeneficiario(int? idBB)
        {
            return DACConsulta.TraerDatosBeneficiario(idBB);

        }
        public List<base_beneficiarios_analitica> TraerBeneficiarioDocumento(string documento)
        {
            return DACConsulta.TraerBeneficiarioDocumento(documento);
        }

        public List<fis_rips_cups> TraerListadoCupsCodigo(string codigo)
        {
            return DACConsulta.TraerListadoCupsCodigo(codigo);
        }

        public List<ref_cie10_mortNat> GetCie10MorNatBycodigo(string term)
        {
            return DACConsulta.GetCie10MorNatBycodigo(term);
        }

        public List<ref_contact_estado_solicitud> GetListEstadoSolicitud()
        {
            return DACConsulta.GetListEstadoSolicitud();
        }

        public List<ref_contact_medio_notificacion> GetListMediosNotificacion()
        {
            return DACConsulta.GetListMediosNotificacion();
        }

        public int InsertarIngresoContactCenter(contact_center obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarIngresoContactCenter(obj, ref MsgRes);
        }

        public void InsertarBitacoraCallCenter(List<contact_center_dtll> List, int id_contact_center, string usuario)
        {
            DACInserta.InsertarBitacoraCallCenter(List, id_contact_center, usuario);
        }
        public int InsertarBitacoraContactCenter(contact_center_dtll obj)
        {
            return DACInserta.InsertarBitacoraContactCenter(obj);
        }

        public contact_center GetContactCenterById(int id)
        {
            return DACConsulta.GetContactCenterById(id);
        }

        public List<contact_center_dtll> GetListBitacoraByIngreso(int id_contact_center, int? censo, int? idConcurrencia)
        {
            return DACConsulta.GetListBitacoraByIngreso(id_contact_center, censo, idConcurrencia);
        }

        public int ActualizarContactCenterPrincipal(int? idContact)
        {
            return DACActualiza.ActualizarContactCenterPrincipal(idContact);
        }
        public List<vw_contact_center> GetListContactCenter(int? estado)
        {
            return DACConsulta.GetListContactCenter(estado);
        }
        public List<management_contact_centerResult> ListaTableroContactCenter(DateTime? fechaIni, DateTime? fechaFin)
        {
            return DACConsulta.ListaTableroContactCenter(fechaIni, fechaFin);
        }
        public management_contact_centerResult GetContactCenterCensoIdContact(int id)
        {
            return DACConsulta.GetContactCenterCensoIdContact(id);
        }

        public management_contact_centerResult GetContactCenterCensoIdCenso(int id)
        {
            return DACConsulta.GetContactCenterCensoIdCenso(id);
        }

        public management_contact_centerResult GetContactCenterCensoIdConcurrencia(int id)
        {
            return DACConsulta.GetContactCenterCensoIdConcurrencia(id);
        }

        public int ActualizarEnContactCenterConcurrencia(int? idConcurrencia, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarEnContactCenterConcurrencia(idConcurrencia, ref MsgRes);
        }

        public int ActualizarEnContactCenterCenso(int? idCenso, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarEnContactCenterCenso(idCenso, ref MsgRes);
        }

        public void InsertarLogConcurrenciaEnviadaCallCenter(List<log_concurrenciaEnviada_contactCenter> log, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarLogConcurrenciaEnviadaCallCenter(log, ref MsgRes);
        }

        public void InsertarLogindividualConcurrenciaEnviadaCallCenter(log_concurrenciaEnviada_contactCenter log, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarLogindividualConcurrenciaEnviadaCallCenter(log, ref MsgRes);
        }
        public void ActualizarImagenCaso(string rutaImagen, string tipo, int contactcenter)
        {
            DACActualiza.ActualizarImagenCaso(rutaImagen, tipo, contactcenter);
        }

        public List<ref_contact_solicitante> GetlistSolicitantesbytipo(string term, int tipo)
        {
            return DACConsulta.GetlistSolicitantesbytipo(term, tipo);
        }

        public List<management_contact_center_camposObligatoriosResult> ListaCamposObligatoriosCC(int? idContact, int? idConcurrencia, int? idCenso)
        {
            return DACConsulta.ListaCamposObligatoriosCC(idContact, idConcurrencia, idCenso);
        }

        public List<management_contact_center_seguimientoResult> ListaTableroContactCenterSeguimiento(DateTime? fechaIni, DateTime? fechaFin)
        {
            return DACConsulta.ListaTableroContactCenterSeguimiento(fechaIni, fechaFin);
        }


        public int ActualizarContactObligatorios(contact_center obj)
        {
            return DACActualiza.ActualizarContactObligatorios(obj);
        }

        #endregion

        #region Insumos

        public bool ValidarExistenciaQuejasValidas(int mes, int año)
        {
            return DACConsulta.ValidarExistenciaQuejasValidas(mes, año);
        }

        public void InsertarQuejasValidasDtll(List<calidad_quejas_validas_dtll> List, calidad_quejas_validas objbase, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarQuejasValidasDtll(List, objbase, ref MsgRes);
        }

        public List<vw_calidad_quejas_validas> GetListCalidadQuejasValidas()
        {
            return DACConsulta.GetListCalidadQuejasValidas();
        }

        public List<calidad_quejas_validas_base_zip> GetListBasesCargadasQuejasValidas()
        {
            return DACConsulta.GetListBasesCargadasQuejasValidas();
        }

        public calidad_quejas_validas_base_zip GetArchivoById(int id)
        {
            return DACConsulta.GetArchivoById(id);
        }

        public void EliminarArchivoZipQuejasValidas(calidad_quejas_validas_base_zip obj)
        {
            DACElimina.EliminarArchivoZipQuejasValidas(obj);
        }

        public void InsertarArchivoQuejasValidas(calidad_quejas_validas_base_zip obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarArchivoQuejasValidas(obj, ref MsgRes);
        }

        public bool ValidarExistenciaOportunidadRIPS(int mes, int año)
        {
            return DACConsulta.ValidarExistenciaOportunidadRIPS(mes, año);
        }

        public void InsertarOportunidadRips(List<calidad_oportunidad_rips_dtll> List, calidad_oportunidad_rips objbase, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarOportunidadRips(List, objbase, ref MsgRes);
        }

        public List<vw_calidad_oportunidad_rips_indicador> GetListCalidadOportunidadRips()
        {
            return DACConsulta.GetListCalidadOportunidadRips();
        }

        public void InsertarCalidadRips(List<calidad_de_rips_dtll> List, calidad_de_rips objbase, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCalidadRips(List, objbase, ref MsgRes);
        }

        public List<vw_calidad_de_rips_indicador> GetListCalidadCalidadRips()
        {
            return DACConsulta.GetListCalidadCalidadRips();
        }

        public void InsertarOportunidadCitasMedicas(List<calidad_oportunidad_citas_medicina_gnral_dtll> List, calidad_oportunidad_citas_medicina_gnral objbase, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarOportunidadCitasMedicas(List, objbase, ref MsgRes);
        }

        public void InsertarCalidadCitasCumplidas(List<calidad_citas_cumplidas_dtll> List, calidad_citas_cumplidas objbase, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCalidadCitasCumplidas(List, objbase, ref MsgRes);
        }

        public List<vw_calidad_oportunidad_citas_medicina_gnral_indicador> GetListCalidadOporCitasMedicas()
        {
            return DACConsulta.GetListCalidadOporCitasMedicas();
        }


        public void InsertarOportunidadOdontologia(List<calidad_oportunidad_odontologia_gnral_dtll> List, calidad_oportunidad_odontologia_gnral objbase, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarOportunidadOdontologia(List, objbase, ref MsgRes);
        }


        public List<vw_calidad_oportunidad_odontologia_gnral_indicador> GetListCalidadOporOdontologia()
        {
            return DACConsulta.GetListCalidadOporOdontologia();
        }

        public List<vw_calidad_citas_cumplidas_indicador> GetListCalidadCitasCumplidas()
        {
            return DACConsulta.GetListCalidadCitasCumplidas();
        }

        public void InsertarEventosAdversos(List<calidad_evento_adverso> List, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarEventosAdversos(List, ref MsgRes);
        }

        public List<calidad_evento_adverso> GetListCalidadEventoAdverso()
        {
            return DACConsulta.GetListCalidadEventoAdverso();
        }

        public void InsertarDocumentoInsumo(calidad_gestor_documental_insumos obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarDocumentoInsumo(obj, ref MsgRes);
        }

        public List<calidad_gestor_documental_insumos> GetListGestorDocumentalInsumos()
        {
            return DACConsulta.GetListGestorDocumentalInsumos();
        }

        public calidad_gestor_documental_insumos GetDocumentoById(int id)
        {
            return DACConsulta.GetDocumentoById(id);
        }
        public vw_calidad_gestor_documental_insumos VwGetDocumentoById(int id)
        {
            return DACConsulta.VwGetDocumentoById(id);
        }
        public vw_calidad_gestor_documental_insumos TarerArchivoInsumosId(int id)
        {
            return DACConsulta.TarerArchivoInsumosId(id);
        }


        public void EliminarDocumento(calidad_gestor_documental_insumos obj)
        {
            DACElimina.EliminarDocumento(obj);
        }

        public List<ref_calidad_insumos_tipo_documental> GetListInsumoTipoDocumental()
        {
            return DACConsulta.GetListInsumoTipoDocumental();
        }

        public List<vw_calidad_quejas_validas_prestadores> GetPrestadoresQuejasValidas(string term, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetPrestadoresQuejasValidas(term, ref MsgRes);
        }

        public List<vw_calidad_oportunidad_calidad_rips_indicador_prestadores> GetPrestadoresOportunidadRips(string term, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetPrestadoresOportunidadRips(term, ref MsgRes);
        }

        public List<vw_calidad_oportunidad_calidad_rips_indicador_prestadores> GetCodPrestadoresOportunidadRips(string term, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetCodPrestadoresOportunidadRips(term, ref MsgRes);
        }

        public List<vw_calidad_opor_citas_y_odont_prestadores> GetPrestadoresCitasmedicasyodontologia(string term, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetPrestadoresCitasmedicasyodontologia(term, ref MsgRes);
        }

        public List<vw_calidad_opor_citas_y_odon_profesionales> GetProfesionalesCitasmedicasyodontologia(string term, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetProfesionalesCitasmedicasyodontologia(term, ref MsgRes);
        }


        public List<vw_calidad_eventos_adversos_prestadores> GetprestadoresEventosAdversos(string term, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetprestadoresEventosAdversos(term, ref MsgRes);
        }

        public List<vw_calidad_citas_cumplidas_profesionales> GetProfesionalesCitasCumplidas(string term, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetProfesionalesCitasCumplidas(term, ref MsgRes);
        }

        public List<management_insumos_capacidad_resolutiva_listaResult> ListaInsumosCapacidadResolutiva()
        {
            return DACConsulta.ListaInsumosCapacidadResolutiva();
        }

        public bool ValidarExistenciaIndicadorCalidad(int mes, int año)
        {
            return DACConsulta.ValidarExistenciaIndicadorCalidad(mes, año);
        }

        public void InsertarIndicadoresCalidadDtll(List<insumos_capacidad_resolutiva_dtll> List, insumos_capacidad_resolutiva objbase, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarIndicadoresCalidadDtll(List, objbase, ref MsgRes);
        }

        public List<calidad_ref_especialidad> GetEspecialidades()
        {
            return DACComonClass.GetEspecialidades();
        }

        public int InsertarBaseBeneficiariosMasivo(List<base_beneficiarios> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarBaseBeneficiariosMasivo(List, ref MsgRes);
        }
        public int InsertarLogBaseBeneficiarios(log_cargue_base_beneficiarios obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarLogBaseBeneficiarios(obj, ref MsgRes);
        }

        public void EliminarBaseBeneficiariosEco(ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarBaseBeneficiariosEco(ref MsgRes);
        }

        public base_beneficiarios getUltimoPeriodoBeneficiarios()
        {
            base_beneficiarios list = DACConsulta.getUltimoPeriodoBeneficiarios();
            return list;
        }

        public List<ref_adherencia_ciudad> GetCiudad()
        {
            return DACConsulta.GetCiudad();
        }


        public int insertarPrestador(ref_adherencia_prestador obj, List<ref_adherencia_profesional> lista, int creado)
        {
            return DACInserta.insertarPrestador(obj, lista, creado);
        }

        public int insertarPrestadorCiudad(ref_adherencia_prestador_ciudad obj)
        {
            return DACInserta.insertarPrestadorCiudad(obj);
        }

        public List<ref_adherencia_prestador> traerPrestadores()
        {
            return DACConsulta.traerPrestadores();
        }

        public List<management_traerPrestadoresResult> traerPrestadoresId(string id)
        {
            return DACConsulta.traerPrestadoresId(id);
        }

        public List<management_baseBeneficiariosPeriodoValidoResult> GetBeneficiariosPerodoValido(int mes, int año)
        {
            return DACConsulta.GetBeneficiariosPerodoValido(mes, año);
        }

        public List<ref_adherencia_ciudad> getCiudadesUnis(int idUnis)
        {
            return DACConsulta.getCiudadesUnis(idUnis);
        }

        public List<base_beneficiarios_vip> listadoBBVIP()
        {
            return DACConsulta.listadoBBVIP();
        }

        #endregion

        #region verificacion
        public List<ref_ver_tipoCriterio> Get_refTipoCriterio()
        {
            return DACConsulta.Get_refTipoCriterio();
        }
        public List<ref_verificacion_farmaceutico> Get_refVerificacionFarmaceutita()
        {
            return DACConsulta.Get_refVerificacionFarmaceutita();
        }

        public List<management_verificacionListaResult> getTipoCriterioId(int idTipo)
        {
            return DACConsulta.getTipoCriterioId(idTipo);
        }
        public List<management_verificacionListaResult> getTotalDatosDispen()
        {
            return DACConsulta.getTotalDatosDispen();
        }

        public ref_verificacion_farmaceutico Get_refVerificacionFarmaceutitaById(int idTipoVer)
        {
            return DACConsulta.Get_refVerificacionFarmaceutitaById(idTipoVer);
        }

        public void InsertarVerificacion(ref_verificacion_farmaceutico obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarVerificacion(obj, ref MsgRes);
        }
        public void ActualizarVerificacion(ref_verificacion_farmaceutico obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarVerificacion(obj, ref MsgRes);
        }

        public int ActualizarDesVerificacionFarmaceutica(int? id, string desc)
        {
            return DACActualiza.ActualizarDesVerificacionFarmaceutica(id, desc);
        }


        public List<ref_tipoCriterio> ListImpactosEvaluacion()
        {
            return DACConsulta.ListImpactosEvaluacion();
        }

        public void InsertarTipoCriteriover(ref_ver_tipoCriterio obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarTipoCriteriover(obj, ref MsgRes);
        }

        public void ActualizarTipoCriteriover(ref_ver_tipoCriterio obj, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarTipoCriteriover(obj, ref MsgRes);
        }


        public List<ver_tipocriterio> get_ref_tipoCriterio(int idVerificacion)
        {
            return DACConsulta.get_ref_tipoCriterio(idVerificacion);
        }

        public List<ver_tipocriterio> get_ref_tipoCriterioGeneral()
        {
            return DACConsulta.get_ref_tipoCriterioGeneral();
        }


        public List<ref_ver_grupo_tpocriterio> get_ver_grupoTipoCritero()
        {
            return DACConsulta.get_ver_grupoTipoCritero();
        }

        public void InsertarAdminCriteriosver(int tipoVerificacion, List<int> seleccionados, List<int> seleccionados2, List<string> seleccionados3, string usuario, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarAdminCriteriosver(tipoVerificacion, seleccionados, seleccionados2, seleccionados3, usuario, ref MsgRes);
        }

        public void EliminarTipoCriteriover(int idtipocriterio, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarTipoCriteriover(idtipocriterio, ref MsgRes);
        }

        public List<ver_criterio> getcriteriosbytipoverificacion(int tipoverificacion)
        {
            return DACConsulta.getcriteriosbytipoverificacion(tipoverificacion);
        }

        public ver_criterio ConsultarCriterioById2(int idcriterio)
        {
            return DACConsulta.ConsultarCriterioById2(idcriterio);
        }

        public void InsertarCriteriover(ver_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCriteriover(criterio, ref MsgRes);
        }

        public void ActualizarCriteriover(ver_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            DACActualiza.ActualizarCriteriover(criterio, ref MsgRes);
        }

        public List<ref_verificacionFarmaceutica_tipos> getTiposVerificacion()
        {
            return DACConsulta.getTiposVerificacion();
        }

        public void EliminarCriterioVerificacion(int idcriterio, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarCriterioVerificacion(idcriterio, ref MsgRes);
        }

        public void InsertarCarguePuntoDispensacion(List<ver_puntoDispensacion> List, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCarguePuntoDispensacion(List, ref MsgRes);
        }

        public List<ver_puntoDispensacion> getPuntoDispensacionList()
        {
            return DACConsulta.getPuntoDispensacionList();
        }
        public List<management_dispensacion_archivosRepositorioResult> MostrarArchivosEvaluacionVisitasMD(int? idEvaluacion)
        {
            return DACConsulta.MostrarArchivosEvaluacionVisitasMD(idEvaluacion);
        }
        public int ActualizarPuntoDispensacion(ver_puntoDispensacion obj)
        {
            return DACActualiza.ActualizarPuntoDispensacion(obj);
        }
        public int ActualizarAuditadoVisitasDispensacion(ver_puntoDispensacion obj)
        {
            return DACActualiza.ActualizarAuditadoVisitasDispensacion(obj);
        }
        public List<ver_evaluacion_archivos> TraerArchivosVisitasDispensacion(int idEvaluacion)
        {
            return DACConsulta.TraerArchivosVisitasDispensacion(idEvaluacion);
        }
        public int InsertarEvaluacionDispensacion(ver_dispen_evaluacion obj)
        {
            return DACInserta.InsertarEvaluacionDispensacion(obj);
        }

        public int InsertarEvaluacionDispensacionTotal(List<ver_dispen_evaluacion_total> List)
        {
            return DACInserta.InsertarEvaluacionDispensacionTotal(List);
        }
        public List<management_dispensacion_evaluacionRelacionResult> getDispensacionEvaluacion()
        {
            return DACConsulta.getDispensacionEvaluacionl();
        }

        public List<management_dispensacion_evaluacionRelacion_totalResult> getDispensacionEvaluacionTotal()
        {
            return DACConsulta.getDispensacionEvaluacionTotal();
        }

        public List<management_dispensacion_evaluacionRelacionResult> getDispensacionEvaluacionId(int Id)
        {
            return DACConsulta.getDispensacionEvaluacionId(Id);
        }

        public List<management_dispensacion_evaluacionRelacion_totalIdResult> getDispensacionEvaluacionTotalId(int id)
        {
            return DACConsulta.getDispensacionEvaluacionTotalId(id);
        }


        public int InsertarArchivosEvaluacion(ver_evaluacion_archivos obj)
        {
            return DACInserta.InsertarArchivosEvaluacion(obj);
        }
        public int InsertarArchivosEvaluacionPDFS(ver_evaluacion_pdfs obj)
        {
            return DACInserta.InsertarArchivosEvaluacionPDFS(obj);
        }
        public ver_evaluacion_pdfs TraerPDFEvaluacionVisitas(int idEvaluacion)
        {
            return DACConsulta.TraerPDFEvaluacionVisitas(idEvaluacion);
        }
        public int EliminarArchivosPDFevaluacionDispensacion(int idEvaluacion)
        {
            return DACElimina.EliminarArchivosPDFevaluacionDispensacion(idEvaluacion);
        }
        public int EliminarArchivosEvaluacionVisitas(int idEvaluacion, int idArchivo)
        {
            return DACElimina.EliminarArchivosEvaluacionVisitas(idEvaluacion, idArchivo);
        }
        public ver_evaluacion_archivos DescargarArchivoEvaluacionVisitas(int idArchivo)
        {
            return DACConsulta.DescargarArchivoEvaluacionVisitas(idArchivo);
        }
        public List<management_dispensacion_evaluacionRelacion_hallazgoResult> getEvolucionHallazgos()
        {
            return DACConsulta.getEvolucionHallazgos();
        }

        public List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> getDispensacionEvaluacionTotalIdHallazgoId(int id)
        {
            return DACConsulta.getDispensacionEvaluacionTotalIdHallazgoId(id);
        }


        public List<ref_evaluacion_estadoTotal> getEstadosEvaluacionHallazgos()
        {
            return DACConsulta.getEstadosEvaluacionHallazgos();
        }

        public List<ref_evaluacion_tipoHallazgo> getTipoHallazgoEvaluacion()
        {
            return DACConsulta.getTipoHallazgoEvaluacion();
        }

        public List<ref_evaluacion_cumplimiento> getCumplimientoEvaluacion()
        {
            return DACConsulta.getCumplimientoEvaluacion();
        }
        public List<ref_evaluacion_tipoSoporte> getTipoSoporteEvaluacion()
        {
            return DACConsulta.getTipoSoporteEvaluacion();
        }

        public int insertarHallazgoItemEvaluacion(ver_evaluacion_hallazgo obj)
        {
            return DACInserta.insertarHallazgoItemEvaluacion(obj);
        }

        public List<ver_evaluacion_hallazgo> ExisteHallazgoSubsanado(int idTotal, int id_tipoCriterio)
        {
            return DACConsulta.ExisteHallazgoSubsanado(idTotal, id_tipoCriterio);
        }

        public int ActualizarHallazgoVisitas(ver_evaluacion_hallazgo obj)
        {
            return DACActualiza.ActualizarHallazgoVisitas(obj);
        }

        public int insertarHallazgoItemEvaluacionArchivos(ver_evaluacion_hallazgo_archivos obj)
        {
            return DACInserta.insertarHallazgoItemEvaluacionArchivos(obj);
        }

        public List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> getDispensacionEvaluacionTotalHallazgo()
        {
            return DACConsulta.getDispensacionEvaluacionTotalHallazgo();
        }

        #endregion

        #region METODOS DAVID 

        public int SaveCuidadosPaliativos(ffmm_cuidados_paliativos objeto, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.SaveCuidadosPaliativos(objeto, ref MsgRes);
        }


        public List<Ref_ffmm_unidad_satelite> GetUnidadSatelite(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetUnidadSatelite(ref MsgRes);
        }
        public List<Ref_ffmm_unidad_cp> GetUnidad(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetUnidad(ref MsgRes);
        }
        public List<vw_ffmm_departamento> GetDepartamentos(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetDepartamentos(ref MsgRes);
        }
        public List<vw_ffmm_municipio> GetMunicipios(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetMunicipios(ref MsgRes);
        }
        public List<vw_ffmm_municipio> GetMunicipiosFM(int idDepartamento, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetMunicipiosFM(idDepartamento, ref MsgRes);
        }
        public List<Ref_ffmm_tipo_visita_cp> GetTipoVisita(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetTipoVisita(ref MsgRes);
        }
        public List<vw_ffmm_ips> GetIPS(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetIPS(ref MsgRes);
        }
        public List<Ref_tipo_documental> GetTipoIdentificacion(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetTipoIdentificacion(ref MsgRes);
        }
        public ref_solucionador UltimaRegionalUsuario(string nombre)
        {
            return DACConsulta.UltimaRegionalUsuario(nombre);
        }
        public List<Ref_ffmm_sexo> GetSexo(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetSexo(ref MsgRes);
        }
        public List<Ref_ffmm_unidad_cp> GetSitioAdscripcion(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetSitioAdscripcion(ref MsgRes);
        }
        public List<Ref_ffmm_tipo_afiliacion> GetTipoAfiliacion(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetTipoAfiliacion(ref MsgRes);
        }

        public List<Ref_ffmm_estado> GetEstado(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetEstado(ref MsgRes);
        }
        public List<Ref_ffmm_fuerza> GetFuerza(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFuerza(ref MsgRes);
        }
        public List<Ref_ffmm_sino> GetSiNo(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetSiNo(ref MsgRes);
        }

        public List<vw_cie10> GetCie10(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetCie10(ref MsgRes);
        }

        public List<vw_ffmm_glosas> GetFFMMGlosas(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetFFMMGlosas(ref MsgRes);
        }

        #endregion

        #region correosPPE

        public int CargueCorreosPPE(List<ecop_directorioPPE_correos> listadoCargue, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueCorreosPPE(listadoCargue, ref MsgRes);
        }

        public int eliminarCorreosPPE(ref MessageResponseOBJ MsgRes)
        {
            return DACElimina.eliminarCorreosPPE(ref MsgRes);
        }

        #endregion

        #region GlosasFFMM
        public Int32 SaveProgramarVisitaGlosa(ffmm_glosas objeto, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.SaveProgramarVisitaGlosa(objeto, ref MsgRes);

        }


        public Int32 UpdateGlosa(ffmm_glosas objeto, string caso, ref MessageResponseOBJ MsgRes)
        {

            return DACActualiza.UpdateGlosa(objeto, caso, ref MsgRes);

        }


        #endregion

        #region AuditoriaFacturas 
        public List<ffmm_Cuentas_auditoria> GetCuentasAuditoria(ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetCuentasAuditoria(ref MsgRes);

        }

        public Int32 UpdateProgramarVisitaGlosa(ffmm_glosas objeto, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.UpdateProgramarVisitaGlosa(objeto, ref MsgRes);

        }

        public ffmm_cuentas_glosas GetCuentasGlosas(int id_glosa, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetCuentasGlosas(id_glosa, ref MsgRes);
        }


        public ffmm_glosas GetGlosas(int id_glosa, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.GetGlosas(id_glosa, ref MsgRes);
        }


        public ffmm_Cuentas_auditoria ultimoPagoyConciliacion()
        {
            return DACConsulta.ultimoPagoyConciliacion();
        }


        #endregion

        #region Metodos medicamentos dispensacion y otros
        public List<management_unionFuerzasgradosResult> getUnionFuerzas(int idFuerza)
        {
            return DACConsulta.getUnionFuerzas(idFuerza);
        }
        //public List<medicamentos_dispen_cargue> ExistenciaMedicamentosDispen(int año, int mes)
        //{
        //    return DACConsulta.ExistenciaMedicamentosDispen(año, mes);
        //}

        public int InsertarDispensacionMedicamentosCargue(medicamentos_dispen_cargue objbase, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDispensacionMedicamentosCargue(objbase, ref MsgRes);
        }
        //Leo
        public void EliminarCargueDispen(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarCargueDispen(idCargue, ref MsgRes);
        }
        //Leo
        public void EliminarCargueDispendtll(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            DACElimina.EliminarCargueDispendtll(idCargue, ref MsgRes);
        }

        //leo
        public int InsertarDispensacionMedicamentosCalidad(List<medicamentos_dispen_calidad> List, Int32 id_cargue, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarDispensacionMedicamentosCalidad(List, id_cargue, ref MsgRes);
        }
        public List<management_medicamentosDispen_listResult> ListaMedicamentosDispensacion(DateTime? fechaInicio, DateTime? fechaFin)
        {
            return DACConsulta.ListaMedicamentosDispensacion(fechaInicio, fechaFin);
        }

        public List<management_medicamentosDispen_consolidadoIdResult> ListaMedicamentosDispensacionConsolidado(int? idCargue)
        {
            return DACConsulta.ListaMedicamentosDispensacionConsolidado(idCargue);
        }

        public List<management_medicamentosDispen_reporteResult> ListaMedicamentosDispensacionReporte(int idCargue)
        {
            return DACConsulta.ListaMedicamentosDispensacionReporte(idCargue);
        }

        public List<management_listaMedicDspensacionResult> ListaMedicamentosDispensacionPrestadores(int mes, int año)
        {
            return DACConsulta.ListaMedicamentosDispensacionPrestadores(mes, año);
        }

        public List<management_listaMedicDspensacion_agrupacionResult> ListaMedicamentosDispensacionPrestadoresAgrupacion(int mes, int año)
        {
            return DACConsulta.ListaMedicamentosDispensacionPrestadoresAgrupacion(mes, año);
        }

        public List<management_medicamentosDispen_consultaResult> ListaMedicamentosDispenConsulta(DateTime fechaIni, DateTime fechaFin)
        {
            return DACConsulta.ListaMedicamentosDispenConsulta(fechaIni, fechaFin);
        }
        public List<management_medicamentosDispen_consulta_armadaResult> ListaMedicamentosDispenConsultaArmada(DateTime fechaIni, DateTime fechaFin, string documento, string familiar, string formula)
        {
            return DACConsulta.ListaMedicamentosDispenConsultaArmada(fechaIni, fechaFin, documento, familiar, formula);
        }
        public List<management_medicamentosDispen_consulta_filtros_docResult> ListaMedicamentosDispenConsultaFiltroDoc(string documento)
        {
            return DACConsulta.ListaMedicamentosDispenConsultaFiltroDoc(documento);
        }

        public List<management_medicamentosDispen_consulta_filtros_familiarResult> ListaMedicamentosDispenConsultaFiltroFamiliar(string familia)
        {
            return DACConsulta.ListaMedicamentosDispenConsultaFiltroFamiliar(familia);
        }

        public List<management_medicamentosDispen_consulta_filtros_formulaResult> ListaMedicamentosDispenConsultaFiltroFormu(string documento)
        {
            return DACConsulta.ListaMedicamentosDispenConsultaFiltroFormu(documento);
        }


        #region ANALISTAS
        public int ValidaExisteAnalista(int regional, int unis, int analista)
        {
            return DACConsulta.ValidaExisteAnalista(regional, unis, analista);
        }

        public ref_cuentas_medicas_analista TraerAnalistasIngresados(ref_cuentas_medicas_analista obj)
        {
            return DACConsulta.TraerAnalistasIngresados(obj);
        }

        public vw_analistas_recepcion TraerAnalista(int idUsuario)
        {
            return DACConsulta.TraerAnalista(idUsuario);
        }
        public int InsertarAnalistas(List<ref_cuentas_medicas_analista> obj)
        {
            return DACInserta.InsertarAnalistas(obj);
        }
        #endregion

        public List<ManagmentRipsHomologacionFacResult> ConsultaHomologacionFac(String num_factura, String tipo_id_prestador, String num_id_prestador)
        {
            return DACConsulta.ConsultaHomologacionFac(num_factura, tipo_id_prestador, num_id_prestador);
        }

        public List<ManagmentRipsHomologacionFacDTLLResult> ConsultaHomologacionFacdtll(String num_factura, String tipo_id_prestador, String num_id_prestador, Int32 id_rips)
        {
            return DACConsulta.ConsultaHomologacionFacdtll(num_factura, tipo_id_prestador, num_id_prestador, id_rips);
        }

        public int Insertar_rips_homologacion(rips_homologacion objbase, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.Insertar_rips_homologacion(objbase, ref MsgRes);
        }
        public int Insertar_rips_homologacion_dtll(List<rips_homologacion_dtll> objbase, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.Insertar_rips_homologacion_dtll(objbase, ref MsgRes);
        }
        public List<rips_homologacion> Traerhomologacion_dtll(rips_homologacion obj)
        {
            return DACConsulta.Traerhomologacion_dtll(obj);
        }
        public List<ManagmentRipsHomologacionFacDTLLFinalResult> ConsultaHomologacionFacdtllFinal(String num_factura, Int32 id_rips)
        {
            return DACConsulta.ConsultaHomologacionFacdtllFinal(num_factura, id_rips);
        }

        public List<vw_rips_homologacion_tarifario> TarifarioRips()
        {
            return DACComonClass.TarifarioRips();
        }

        public int Actualizar_md_Lupe_cargue_base_H(rips_homologacion_dtll obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.Actualizar_md_Lupe_cargue_base_H(obj, ref MsgRes);
        }
        public int Actualizar_md_Lupe_cargue_base_G(rips_homologacion_dtll obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.Actualizar_md_Lupe_cargue_base_G(obj, ref MsgRes);
        }

        public List<ManagmentRipsHomologacionFacFinalResult> ConsultaHomologacionFacFinal(String num_factura, String tipo_id_prestador, String num_id_prestador, Int32 id_rips)
        {
            return DACConsulta.ConsultaHomologacionFacFinal(num_factura, tipo_id_prestador, num_id_prestador, id_rips);
        }
        public int Actualizar_rips_homologacion(rips_homologacion obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.Actualizar_rips_homologacion(obj, ref MsgRes);
        }
        public void ActualizarFacturas_automaticas(int idBaseFactura)
        {
            DACActualiza.ActualizarFacturas_automaticas(idBaseFactura);
        }
        public List<management_gastoServicio_nombreServicioResult> ConsultarNombreServicioGXS(string nombre)
        {
            return DACConsulta.ConsultarNombreServicioGXS(nombre);
        }
        public int EliminarTotalEvaluacion(int idEvaluacion)
        {
            return DACElimina.EliminarTotalEvaluacion(idEvaluacion);
        }

        public int EliminarTotalEvaluacionArchivos(int idEvaluacion, int? tipoCriterio, int? idVerificacion)
        {
            return DACElimina.EliminarTotalEvaluacionArchivos(idEvaluacion, tipoCriterio, idVerificacion);
        }

        public ver_evaluacion_archivos BuscarExistenciaArchivo(int? idEvaluacion, int? idCriterio, int? idVerificacion, string nombre)
        {
            return DACConsulta.BuscarExistenciaArchivo(idEvaluacion, idCriterio, idVerificacion, nombre);
        }

        public int ActualizarVisitaDispensacion(ver_dispen_evaluacion obj)
        {
            return DACActualiza.ActualizarVisitaDispensacion(obj);
        }
        public int EliminarEvaluacionVisitasAutoguardado(int idEvaluacion)
        {
            return DACElimina.EliminarEvaluacionVisitasAutoguardado(idEvaluacion);
        }
        public List<management_informacionUsuarios_prestadoresResult> UsuariosPrestadores(string nit, string nombre, string cedula)
        {
            return DACConsulta.UsuariosPrestadores(nit, nombre, cedula);
        }

        public List<management_informacionUsuarios_prestadoresDetalleResult> UsuariosPrestadoresDetalle(int idUsuario)
        {
            return DACConsulta.UsuariosPrestadoresDetalle(idUsuario);
        }

        public int EliminarLoteFacturas(int id)
        {
            return DACElimina.EliminarLoteFacturas(id);
        }
        public sis_usuario datosUsuarioId(int idUsuario)
        {
            return DACConsulta.datosUsuarioId(idUsuario);
        }

        public sis_usuario datosUsuarioRol(int? idRol)
        {
            return DACConsulta.datosUsuarioRol(idRol);
        }

        public sis_usuario datosUsuarioUser(string usuario)
        {
            return DACConsulta.datosUsuarioUser(usuario);
        }

        public sis_usuario datosUsuarioNombre(string nombre)
        {
            return DACConsulta.datosUsuarioNombre(nombre);
        }

        public List<management_existeLoteAsignado_FacturaResult> ExisteLoteAsignado(int idFac)
        {
            return DACConsulta.ExisteLoteAsignado(idFac);
        }
        public List<Ref_ips_cuentas> getprestadoresEspecial(string nit, string prestador)
        {
            return DACConsulta.getprestadoresEspecial(nit, prestador);
        }
        public management_prestadoresRegionalIdPrResult PrestadorRegional(int idPrestador)
        {
            return DACConsulta.PrestadorRegional(idPrestador);
        }
        public List<vw_sis_auditor_regional> UsuariosxRegional(int idRegional)
        {
            return DACConsulta.UsuariosxRegional(idRegional);
        }
        public List<ref_cuentas_medicas_analista> getAnalistasAsignadosprestador(int idPrestador)
        {
            return DACConsulta.getAnalistasAsignadosprestador(idPrestador);
        }
        public int ActualizarAsignacion_automatica(int idPrestador)
        {
            return DACActualiza.ActualizarAsignacion_automatica(idPrestador);
        }
        public ref_cuentas_medicas_analista ListadoActualizarAnalistas(int idPrestador, int idAnalista)
        {
            return DACConsulta.ListadoActualizarAnalistas(idPrestador, idAnalista);
        }
        public int ActualizarAsignacionAutomatica(ref_cuentas_medicas_analista obj)
        {
            return DACActualiza.ActualizarAsignacionAutomatica(obj);
        }
        public int InsertarNuevosAnalistas_asignacionAutomatica(List<ref_cuentas_medicas_analista> obj)
        {
            return DACInserta.InsertarNuevosAnalistas_asignacionAutomatica(obj);
        }
        public List<management_facturacion_tableroControlResult> ListadoMedicamentosFacturas(DateTime fechaInicio, DateTime fechaFin, string identificacion)
        {
            return DACConsulta.ListadoMedicamentosFacturas(fechaInicio, fechaFin, identificacion);
        }

        public List<management_facturacion_consolidado_listaResult> ListadoMedicamentosFacturasConsolidadoLista(DateTime fechaIni, DateTime fechaFin)
        {
            return DACConsulta.ListadoMedicamentosFacturasConsolidadoLista(fechaIni, fechaFin);
        }
        public managemenet_prestadores_traerDatosFacturasResult ListadoFacturasIdAf(int id_af)
        {
            return DACConsulta.ListadoFacturasIdAf(id_af);
        }

        public managemenet_prestadores_traerDatosFacturas_idDetalleResult ListadoFacturasIdDetalle(int? idDetalle)
        {
            return DACConsulta.ListadoFacturasIdDetalle(idDetalle);
        }

        public management_fisRips_prestadorTieneContratoResult UnPrestadorTeieneNegociacion(string nit)
        {
            return DACConsulta.UnPrestadorTeieneNegociacion(nit);
        }

        public managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult ListadoFacturasIdDetalleAuditor(int? idDetalle)
        {
            return DACConsulta.ListadoFacturasIdDetalleAuditor(idDetalle);
        }
        public List<ref_componente_hospitalario> GetComponentesHospitalarios()
        {
            return DACConsulta.GetComponentesHospitalarios();
        }
        public int EliminarCarguePrefacturasCompleto(int idCargue)
        {
            return DACElimina.EliminarCarguePrefacturasCompleto(idCargue);
        }

        public int GuardarLogEliminacionPrefacturas(log_prefacturas_eliminarCargues obj)
        {
            return DACInserta.GuardarLogEliminacionPrefacturas(obj);
        }

        public List<management_prefacturas_tableroCierreResult> TableroInformacionCierrePrefacturas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            return DACConsulta.TableroInformacionCierrePrefacturas(fechaInicio, fechaFin);
        }

        public List<management_prefacturas_tableroAhorroResult> TableroInformacionAhorroPrefacturas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            return DACConsulta.TableroInformacionAhorroPrefacturas(fechaInicio, fechaFin);
        }

        public List<ref_analista_lote> TraerAnalistaLoteExistente(int idlote)
        {
            return DACConsulta.TraerAnalistaLoteExistente(idlote);
        }
        public int ActualizarLoteAnalista(ref_analista_lote obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarLoteAnalista(obj, ref MsgRes);
        }

        public List<management_fis_facturasCuv_tableroEliminarResult> TraerListadoDetallesId(int? idFactura, string numFactura)
        {
            return DACConsulta.TraerListadoDetallesId(idFactura, numFactura);
        }

        public management_fis_facturasCuv_tableroEliminar_conteoResult DevolverConteoFacturas(int? idFactura, string numFactura)
        {
            return DACConsulta.DevolverConteoFacturas(idFactura, numFactura);
        }

        public int EliminarRegistroFis(int? id)
        {
            return DACElimina.EliminarRegistroFis(id);
        }

        public int InsertarLogEliminaRegistroFis(log_fis_rips_cargue_registrosCompletos_eliminar obj)
        {
            return DACInserta.InsertarLogEliminaRegistroFis(obj);
        }

        public int EliminarTodoCargueFIS(int? idFactura)
        {
            return DACElimina.EliminarTodoCargueFIS(idFactura);
        }

        #endregion

        #region INVENTARIO FACTURAS CONTABILIZADAS

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Metodo que guardar y retorna el id del cargue base de inventario de facturas contabilizadas
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public Int32 InsertarInventarioFacturasContabilizadasCargueBase(inventario_facturas_contabilizadas_carguebase obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarInventarioFacturasContabilizadasCargueBase(obj, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Metodo que guarda el detalle de inventario de facturas contabilizadas
        /// </summary>
        /// <param name="dtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarInventarioFacturasContabilizadasCargueDtll(List<inventario_facturas_contabilizadas_carguedtll> dtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarInventarioFacturasContabilizadasCargueDtll(dtll, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Metodo para consultar el estado de facturas medianto su estado
        /// </summary>
        /// <param name="idEstado"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Management_inventario_facturas_contabilizadasResult> ConsultarInventarioFacturasPorEstado(int idEstado, DateTime? fechainicio, DateTime? fechafinal, int? regional, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultarInventarioFacturasPorEstado(idEstado, fechainicio, fechafinal, regional, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 28 de noviembre de 2022
        /// Metodo utilizado para guardar la gestion hecha a una factura en el inventario
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void GuardarGestionInvetarioFacturaContabilizada(inventario_facturas_contabilizadas_gestion obj, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.GuardarGestionInvetarioFacturaContabilizada(obj, ref MsgRes);
        }


        /// <summary>
        ///Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para consultar el estado de facturas 
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="regional"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Management_inventario_facturas_contabilizadas_cordinacionResult> ConsultarInventarioFacturasCordinacion(int mes, int año, int regional, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultarInventarioFacturasCordinacion(mes, año, regional, ref MsgRes);
        }

        /// <summary>
        ///Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para consultar el inventario de facturas contabilizadas pero consolidado por mes, año y regional
        /// </summary>
        /// <returns></returns>
        public List<Management_inventario_facturas_contabilizadas_consolidadoResult> ConsultarInventarioFacturasConsolidado()
        {
            return DACConsulta.ConsultarInventarioFacturasConsolidado();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para consultar la gestion realizada a una factura contabilizada que fue cargada a traves del id Detalle
        /// </summary>
        /// <param name="idDetalle"></param>
        /// <returns></returns>
        public inventario_facturas_contabilizadas_gestion consultarGestionFacturaContabilizadaporIdDetalle(int idDetalle)
        {
            return DACConsulta.consultarGestionFacturaContabilizadaporIdDetalle(idDetalle);
        }

        public inventario_facturas_contabilizadas_gestion consultarGestionFacturaContabilizadaporIdGestion(int idGestion)
        {
            return DACConsulta.consultarGestionFacturaContabilizadaporIdGestion(idGestion);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de noviembre de 2022
        /// Metodo utilizado para porder insertar los datos del archivo cargado en el tablero de inventario facturas contabilizadas consolidado
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="MsgRes"></param>
        public void insertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado(inventario_facturas_contabilizadas_gestor_documental doc, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.insertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado(doc, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de noviembre de 2022
        /// Metodo utilizado para consultar los archivos cargados por id
        /// </summary>
        /// <param name="idArchivo"></param>
        /// <param name=""></param>
        public inventario_facturas_contabilizadas_gestor_documental ConsultarRegistroArchivoCargadoPorId(int idArchivo, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultarRegistroArchivoCargadoPorId(idArchivo, ref MsgRes);
        }

        public List<inventario_facturas_contabilizadas_gestor_documental> ConsultarRegistroArchivoCargadoPorIdLista(int? mes, int? año, int? regional, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultarRegistroArchivoCargadoPorIdLista(mes, año, regional, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 02 de diciembre de 2022
        /// Metodo utilizado para validar la existencia de un periodo que vayana a cargar
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="regional"></param>
        /// <returns></returns>
        public inventario_facturas_contabilizadas_carguebase ConsultarExistenciaPeriodoCargado(int mes, int año, int regional)
        {
            return DACConsulta.ConsultarExistenciaPeriodoCargado(mes, año, regional);
        }
        public List<management_inventario_facturas_contabilizadas_reporteResult> ReporteInventarioContabilizadas(int estado)
        {
            return DACConsulta.ReporteInventarioContabilizadas(estado);
        }

        #endregion

        #region INVENTARIO ALTO COSTO
        public int insercionMasivaAltoCosto(inventario_altoCosto_carguebase obj, List<inventario_altoCosto_detalle> dtl, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.insercionMasivaAltoCosto(obj, dtl, ref MsgRes);
        }
        public List<management_inventario_altoCosto_tableroResult> ListadoInventarioAltoCosto()
        {
            return DACConsulta.ListadoInventarioAltoCosto();
        }

        public int insercionGestionInventario(inventario_altoCosto_gestiones obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.insercionGestionInventario(obj, ref MsgRes);
        }


        public List<ref_inventario_altoCostoCancer_atc> listadoInventarioATC()
        {
            return DACConsulta.listadoInventarioATC();
        }

        public List<inventario_altoCosto_gestiones> listaInvAltoCostoGestionadas(int? idDetalle)
        {
            return DACConsulta.listaInvAltoCostoGestionadas(idDetalle);
        }

        public inventario_altoCosto_gestiones DatoInvAltoCostoGestionID(int? idGestion)
        {
            return DACConsulta.DatoInvAltoCostoGestionID(idGestion);
        }

        public inventario_altoCosto_gestiones DatoUltimoInvAltoCostoGestionID(int? idDetalle)
        {
            return DACConsulta.DatoUltimoInvAltoCostoGestionID(idDetalle);
        }

        public Int32 InsertarArchivoisAltoCostoCancer(List<inventario_altoCosto_archivos> archivos, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarArchivoisAltoCostoCancer(archivos, ref MsgRes);
        }

        public List<management_inventario_altoCosto_verArchivosResult> ListadoArchivosGestionados(int? idGestion)
        {
            return DACConsulta.ListadoArchivosGestionados(idGestion);
        }

        public inventario_altoCosto_archivos traerArchivoAltoCostoIdArchivo(int? idArchivo)
        {
            return DACConsulta.traerArchivoAltoCostoIdArchivo(idArchivo);
        }
        public int eliminarArchivoAltoCostoCanceridArchivo(int idArchivo)
        {
            return DACElimina.eliminarArchivoAltoCostoCanceridArchivo(idArchivo);
        }
        public int InsertarLogEliminacionArchivoAltoCosto(log_inventario_altoCosto_eliminacionArchivos obj)
        {
            return DACInserta.InsertarLogEliminacionArchivoAltoCosto(obj);
        }

        public List<management_inventario_altoCosto_tableroGestionesResult> ListaAltoCostoGestiones(int? idDetalle)
        {
            return DACConsulta.ListaAltoCostoGestiones(idDetalle);
        }

        public List<ref_cargue_cuentas_altoCosto> listadoCargueGsdRastreo()
        {
            return DACConsulta.listadoCargueGsdRastreo();
        }

        public List<ref_cargue_cuentas_altoCosto_estados> listadoEstadosCuentaAltoCosto()
        {
            return DACConsulta.listadoEstadosCuentaAltoCosto();
        }

        public int eliminarDatosCuentasAltoCosto(int idCargue, int? tipo)
        {
            return DACElimina.eliminarDatosCuentasAltoCosto(idCargue, tipo);
        }

        public int cargue_cuentas_altoCosto(cargue_cuentas_altoCosto obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.cargue_cuentas_altoCosto(obj, ref MsgRes);
        }

        public int InsertarCuentasAltoCostoConfirmnada(List<cargue_cuentas_altoCosto_confirmada> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCuentasAltoCostoConfirmnada(List, ref MsgRes);
        }

        public int InsertarCuentasAltoCostoCancer(List<cargue_cuentas_altoCosto_cancer> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCuentasAltoCostoCancer(List, ref MsgRes);
        }

        public int GuardarGestionCuentasAltoCosto(cargue_cuentas_altoCosto_gestiones obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.GuardarGestionCuentasAltoCosto(obj, ref MsgRes);
        }

        public List<management_cuentasAltoCosto_gestionesResult> listadoGestionesAltoCosto(int? idRegistro, int? tipo)
        {
            return DACConsulta.listadoGestionesAltoCosto(idRegistro, tipo);
        }

        public int InsertarCuentasAltoCostoHemofilia(List<cargue_cuentas_altoCosto_hemofilia> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCuentasAltoCostoHemofilia(List, ref MsgRes);
        }

        public int InsertarCuentasAltoCostoArtritis(List<cargue_cuentas_altoCosto_artritis> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCuentasAltoCostoArtritis(List, ref MsgRes);
        }

        public int InsertarCuentasAltoCostoVIH(List<cargue_cuentas_altoCosto_vih> List, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCuentasAltoCostoVIH(List, ref MsgRes);
        }

        public List<management_cuentasAltoCosto_rastreosResult> ListadoDatosRastreoTotal(int? tipo)
        {
            return DACConsulta.ListadoDatosRastreoTotal(tipo);
        }

        public List<management_cuentasAltoCosto_rastreosConfirmadosResult> ListadoDatosCuentasAltoCostoConfirmados(int? tipo)
        {
            return DACConsulta.ListadoDatosCuentasAltoCostoConfirmados(tipo);
        }



        public List<management_cuentasAltoCosto_repositorioResult> CuentasAltoCostoRepositorio(int? idRegistro, int? tipo)
        {
            return DACConsulta.CuentasAltoCostoRepositorio(idRegistro, tipo);
        }

        public List<ref_cuentas_altocosto_tipoSoporte> tipoSoporteCAC()
        {
            return DACConsulta.tipoSoporteCAC();
        }

        public cargue_cuentas_altoCosto_archivos TraerArchivoRepositorio(int? idArchivo)
        {
            return DACConsulta.TraerArchivoRepositorio(idArchivo);
        }

        public Int32 InsertarArchivoReposAltoCosto(cargue_cuentas_altoCosto_archivos OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarArchivoReposAltoCosto(OBJ, ref MsgRes);
        }

        public int eliminarArchivoRepositorioAltoCosto(int id)
        {
            return DACElimina.eliminarArchivoRepositorioAltoCosto(id);
        }

        public Int32 LogArchivoReposAltoCosto(log_cargue_cuentas_altoCosto_archivos OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.LogArchivoReposAltoCosto(OBJ, ref MsgRes);
        }

        public List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoResult> ListadoDatosCuentasAltoCostoConfirmadosConArchivos()
        {
            return DACConsulta.ListadoDatosCuentasAltoCostoConfirmadosConArchivos();
        }

        public List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoCompletaResult> ListadoDatosCuentasAltoCostoConfirmadosConArchivosDetallada()
        {
            return DACConsulta.ListadoDatosCuentasAltoCostoConfirmadosConArchivosDetallada();
        }

        public List<management_cuentasAltoCosto_rastreosConfirmados_observacionesResult> ListadoObservacionesCuentasAltoCostoGestionadas(int? idRegistro, int? tipo)
        {
            return DACConsulta.ListadoObservacionesCuentasAltoCostoGestionadas(idRegistro, tipo);
        }

        public Int32 GuardarObservacionesCuentasAltoCosto(cargue_cuentas_altoCosto_observaciones OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.GuardarObservacionesCuentasAltoCosto(OBJ, ref MsgRes);
        }

        public int eliminarObservacionAltoCosto(int id)
        {
            return DACElimina.eliminarObservacionAltoCosto(id);
        }

        public List<management_cuentasAltoCosto_consolidadoArchivosResult> ListaArchivosPorDocumentoCAC(string documento, int? tipo)
        {
            return DACConsulta.ListaArchivosPorDocumentoCAC(documento, tipo);
        }

        public List<management_cuentasAltoCosto_documentosArchivosResult> DocumentosConArchivos(int? año, string mes, string regional, int? tipo, string documento)
        {
            return DACConsulta.DocumentosConArchivos(año, mes, regional, tipo, documento);
        }

        #endregion


        #region CARGUE MASIVO CONTRATOS

        public int CargueMasivoContratos(contratos_cargue obj, List<contratos_detalle> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueMasivoContratos(obj, detalle, ref MsgRes);
        }

        public List<management_contratos_listadoResult> listadoContratos()
        {
            return DACConsulta.listadoContratos();
        }
        public contratos_detalle MostrarDatosContratoId(int? idContrato)
        {
            return DACConsulta.MostrarDatosContratoId(idContrato);
        }
        public int ActualizarContrato(contratos_detalle obj, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarContrato(obj, ref MsgRes);
        }

        public contratos_detalle MostrarDetallePContrato(string sap)
        {
            return DACConsulta.MostrarDetallePContrato(sap);
        }

        public int InsertarContratoNuevoPrestador(contratos_detalle obj)
        {
            return DACInserta.InsertarContratoNuevoPrestador(obj);
        }

        public int InsertarLogContratoNuevoPrestador(log_contratos_detalle obj)
        {
            return DACInserta.InsertarLogContratoNuevoPrestador(obj);
        }

        #endregion


        public List<management_usuarios_regionalResult> ListadoRegionalUsuario()
        {
            return DACComonClass.ListadoRegionalUsuario();
        }

        public int ActualizarCargueMasivoContratos(fis_rips_prestadores_contratos_lote obj, List<fis_rips_prestadores_contratos> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACActualiza.ActualizarCargueMasivoContratos(obj, lista, ref MsgRes);
        }


        #region CARGUE MASIVO RIPS DEPURADOS

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo usado para consultar la existencia de un cargue
        /// </summary>
        /// <param name="tipoRips"></param>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public rips_depurados_carguebase ConsultarCargueBaseRipsDepurados(string tipoRips, int mes, int año)
        {
            return DACConsulta.ConsultarCargueBaseRipsDepurados(tipoRips, mes, año);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue base de los rips depurados
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public int GuardarCargueBaseRipsDepurados(rips_depurados_carguebase cb, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.GuardarCargueBaseRipsDepurados(cb, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AC
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAC(List<rips_depurados_ac_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCargueMasivoRipsDepuradosAC(cdtll, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AC
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAP(List<rips_depurados_ap_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCargueMasivoRipsDepuradosAP(cdtll, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AU
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAU(List<rips_depurados_au_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCargueMasivoRipsDepuradosAU(cdtll, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AM
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAM(List<rips_depurados_am_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCargueMasivoRipsDepuradosAM(cdtll, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AM
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAN(List<rips_depurados_an_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCargueMasivoRipsDepuradosAN(cdtll, ref MsgRes);
        }

        public int InsertarPrestadorRIPS(Ref_RIPS_Prestadores obj)
        {
            return DACInserta.InsertarPrestadorRIPS(obj);
        }


        public int InsertarPrestadorRIPS2(Ref_RIPS_Prestadores obj)
        {
            return DACInserta.InsertarPrestadorRIPS2(obj);
        }

        public List<Ref_RIPS_Prestadores> ConsultaPrestadoresRipsNit(double nit, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaPrestadoresRipsNit(nit, ref MsgRes);
        }

        public List<Ref_RIPS_Prestadores> ConsultaPrestadoresRipsIdPrestador(string IDPrestador, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaPrestadoresRipsIdPrestador(IDPrestador, ref MsgRes);
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AH
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAH(List<rips_depurados_ah_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarCargueMasivoRipsDepuradosAH(cdtll, ref MsgRes);
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo Para eliminar el cargue base de un registro de cargue rips depurados mediante su ID
        /// </summary>
        /// <param name="idCargueBase"></param>
        public void EliminarRipsDepuradosCargueBasePorId(int idCargueBase)
        {
            DACElimina.EliminarRipsDepuradosCargueBasePorId(idCargueBase);
        }

        #endregion


        #region REEMBOLSO

        public int InsertarRembolso(cuentas_reembolsos obj)
        {
            return DACInserta.InsertarRembolso(obj);
        }

        public int InsertarRembolsoDetalle(cuentas_reembolso_detalle obj)
        {
            return DACInserta.InsertarRembolsoDetalle(obj);
        }

        public int InsertarRembolsoArchivos(cuentas_reembolsos_archivos obj)
        {
            return DACInserta.InsertarRembolsoArchivos(obj);
        }
        public List<ref_tipo_moneda> TipoMoneda()
        {
            return DACComonClass.TipoMoneda();
        }

        public List<ref_estado_reembolso> EstadoReembolso()
        {
            return DACComonClass.EstadoReembolso();
        }

        public List<ref_tipo_reembolso> TipoReembolso()
        {
            return DACComonClass.TipoReembolso();
        }

        public List<management_reembolsos_tableroResult> listadoReembolsosIngresados(int? idRegional)
        {
            return DACConsulta.listadoReembolsosIngresados(idRegional);
        }

        public List<management_reembolsos_tablero_gestionadosResult> listadoReembolsosGestionados(int? idRegional)
        {
            return DACConsulta.listadoReembolsosGestionados(idRegional);
        }

        public List<ref_unis_reembolsos> UnisReembolsos()
        {
            return DACConsulta.UnisReembolsos();
        }

        public List<management_reembolsos_gestionResult> listadoReembolsosIngresadosGestiones(int? idReembolso)
        {
            return DACConsulta.listadoReembolsosIngresadosGestiones(idReembolso);
        }

        public List<management_cuentas_reembolso_ArchivosResult> listadoReembolsosArchivos(int? idReembolso)
        {
            return DACConsulta.listadoReembolsosArchivos(idReembolso);
        }
        public int ActualizarEstadoReembolso(cuentas_reembolsos reem)
        {
            return DACActualiza.ActualizarEstadoReembolso(reem);
        }

        public int ActualizarDatosReembolso(cuentas_reembolsos reem)
        {
            return DACActualiza.ActualizarDatosReembolso(reem);
        }

        public int EliminarArchivoReembolsos(int? idArchivo)
        {
            return DACElimina.EliminarArchivoReembolsos(idArchivo);
        }

        public cuentas_reembolsos TraerDatosReembolso(int? idReembolso)
        {
            return DACConsulta.TraerDatosReembolso(idReembolso);
        }

        public cuentas_reembolsos_archivos TrarArchivoReembolso(int? idArchivo)
        {
            return DACConsulta.TrarArchivoReembolso(idArchivo);
        }

        #endregion REEMBOLSO

        #region NORIPS

        public int InsertarNoRips(cuentas_medicas_norips obj, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarNoRips(obj, ref MsgRes);
        }

        public int EliminarCasoNoRips(int? idMed)
        {
            return DACElimina.EliminarCasoNoRips(idMed);
        }

        public List<management_usuariosAnalistas_noripsResult> ListadoAnalistas()
        {
            return DACConsulta.ListadoAnalistas();
        }

        public List<Total_Departamento> TraerDepartamentos()
        {
            return DACConsulta.TraerDepartamentos();
        }
        public Total_Departamento TraerDepartamentoId(int? id)
        {
            return DACConsulta.TraerDepartamentoId(id);
        }

        public List<management_total_departamentosResult> TraerDepartamentosRegional(int? regional)
        {
            return DACConsulta.TraerDepartamentosRegional(regional);
        }

        public List<ref_cuentasmedicas_notips_motivos> ListaMotivosNoRips()
        {
            return DACConsulta.ListaMotivosNoRips();
        }
        public Int32 IngresoArchivosRipsNoEsalud(cuentas_medicas_norips_Archivos OBJ, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.IngresoArchivosRipsNoEsalud(OBJ, ref MsgRes);
        }

        public List<management_cuentasMedicas_ripsNoEsaludResult> TableroRipsNoEsalud(DateTime? fechaInicio, DateTime? fechaFin, int? regional)
        {
            return DACConsulta.TableroRipsNoEsalud(fechaInicio, fechaFin, regional);
        }

        public List<management_cuentasMedicas_ripsNoEsalud_archivosResult> TableroRepositorioRipsNoEsalud(int? idMed)
        {
            return DACConsulta.TableroRepositorioRipsNoEsalud(idMed);
        }

        public cuentas_medicas_norips_Archivos traerArchivoNoRips(int idArchivo)
        {
            return DACConsulta.traerArchivoNoRips(idArchivo);
        }

        public List<management_cuentasMedicas_ripsNoEsalud_TodosArchivosResult> TraerTodosLosArchivosRipsNoEsalud(DateTime? fechaInicio, DateTime? fechaFin, int? regional)
        {
            return DACConsulta.TraerTodosLosArchivosRipsNoEsalud(fechaInicio, fechaFin, regional);
        }
        public List<management_baseBeneficiarios_xDocumentoResult> BusquedaBeneficiarioCedula(string documento)
        {
            return DACConsulta.BusquedaBeneficiarioCedula(documento);
        }
        #endregion NORIPS

        #region CAMPAÑAS
        public int InsertarCreacionCampanas(creacion_campana obj)
        {
            return DACInserta.InsertarCreacionCampanas(obj);
        }

        public int InsertarCreacionCampanasDetalle(creacion_campana_detalle obj)
        {
            return DACInserta.InsertarCreacionCampanasDetalle(obj);
        }

        public int InsertarCreacionCampanasDetalleListados(List<creacion_campana_listas> listas, List<creacion_campana_camposSimples> simples)
        {
            return DACInserta.InsertarCreacionCampanasDetalleListados(listas, simples);
        }

        public List<management_campana_tableroControlResult> ListadoCampanas()
        {
            return DACConsulta.ListadoCampanas();
        }

        public ref_campanas_permisosEdicion traerPermisosEdicionCampana(int? idUsuario)
        {
            return DACConsulta.traerPermisosEdicionCampana(idUsuario);
        }

        public creacion_campana TraerCampanaGeneralId(int? id)
        {
            return DACConsulta.TraerCampanaGeneralId(id);
        }


        public creacion_campana TraerCampanaVersionGeneralId(int? id)
        {
            return DACConsulta.TraerCampanaVersionGeneralId(id);
        }

        public List<creacion_campana_detalle> TraerCampanaGeneraDetallelId(int? id)
        {
            return DACConsulta.TraerCampanaGeneraDetallelId(id);
        }

        public List<ref_campana_tipoPregunta> TraerTipoPreguntaCampaña()
        {
            return DACConsulta.TraerTipoPreguntaCampaña();
        }
        public List<creacion_campana_camposSimples> TraerCampanaCamposSimplesIdCampana(int? id)
        {
            return DACConsulta.TraerCampanaCamposSimplesIdCampana(id);
        }

        public List<creacion_campana_listas> TraerCampanaCamposListaIdCampana(int? id)
        {
            return DACConsulta.TraerCampanaCamposListaIdCampana(id);
        }

        public int insertarRespuestasCamapana(List<campana_respuestas> listaPreguntas, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.insertarRespuestasCamapana(listaPreguntas, ref MsgRes);
        }

        public int IngresarRespuestaCampañaVerificacion_Archivos(campana_respuestas obj)
        {
            return DACInserta.IngresarRespuestaCampañaVerificacion_Archivos(obj);
        }

        public int insertarRespuestasCampanaVerificaciones(List<campana_respuestas_tipoVerificaciones> listaVerificaciones, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.insertarRespuestasCampanaVerificaciones(listaVerificaciones, ref MsgRes);
        }

        public int insertarRespuestasCampanaArchivos(List<campana_respuestas_tipoArchivo> listaArchivos, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.insertarRespuestasCampanaArchivos(listaArchivos, ref MsgRes);
        }

        public int DesactivarActivarCampaña(int? idCampana, int? estado)
        {
            return DACActualiza.DesactivarActivarCampaña(idCampana, estado);
        }


        public creacion_campana_camposSimples TraerCampanaCamposSimplesIdDetalle(int? id)
        {
            return DACConsulta.TraerCampanaCamposSimplesIdDetalle(id);
        }

        public List<creacion_campana_listas> TraerCampanaCamposListaIdDetalle(int? id)
        {
            return DACConsulta.TraerCampanaCamposListaIdDetalle(id);
        }

        public creacion_campana_detalle TraerDatosPreguntaCampana(int? id)
        {
            return DACConsulta.TraerDatosPreguntaCampana(id);
        }


        public List<creacion_campana_detalle> ConsultaDtllPreguntasCampana(int? idcampana)
        {
            return DACConsulta.ConsultaDtllPreguntasCampana(idcampana);
        }

        public int ActualizarVersionCampaña(creacion_campana cam)
        {
            return DACActualiza.ActualizarVersionCampaña(cam);
        }

        public int ActualizarDatosCampañaPregunta(creacion_campana_detalle cam)
        {
            return DACActualiza.ActualizarDatosCampañaPregunta(cam);
        }



        public void ActualizarInactivas(List<creacion_campana_detalle> ActualizarInactivas, ref MessageResponseOBJ msg)
        {
            DACActualiza.ActualizarInactivas(ActualizarInactivas, ref msg);
        }


        public int InsertarLoteCampaña(campana_respuestas_lote lote)
        {
            return DACInserta.InsertarLoteCampaña(lote);
        }

        public int ActualizarCamposPreguntas(int? idPregunta)
        {
            return DACActualiza.ActualizarCamposPreguntas(idPregunta);
        }

        public List<ref_campaña_tipoFecha> TraertiposFechaCmpana()
        {
            return DACConsulta.TraertiposFechaCmpana();
        }
        public List<management_campana_reporteIdResult> ListadoCampanasId(int? idCampana)
        {
            return DACConsulta.ListadoCampanasId(idCampana);
        }

        #endregion CAMPAÑAS

        public int InsertarLogCarguesMasivos(log_cargues_masivos obj)
        {
            return DACInserta.InsertarLogCarguesMasivos(obj);
        }



        #region
        public int CargueAlertasDispensacion(alertas_dispensacion obj, List<alertas_dispensacion_registros> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueAlertasDispensacion(obj, detalle, ref MsgRes);
        }

        public List<management_alertasDispensacion_tableroControlResult> ListadoAlertasDispensacion(DateTime? fecha_prescripcion, string numero_formula, string documento_beneficiario, string id_prescriptor, string nombre_comercial)
        {
            return DACConsulta.ListadoAlertasDispensacion(fecha_prescripcion, numero_formula, documento_beneficiario, id_prescriptor, nombre_comercial);
        }

        public alertas_dispensacion_detalle TraerDatosgestionTramite(int? idDetalle)
        {
            return DACConsulta.TraerDatosgestionTramite(idDetalle);
        }

        public List<management_alertasDispensacion_datosTramiteResult> TraerDatosAMTramite(int? idDetalle)
        {
            return DACConsulta.TraerDatosAMTramite(idDetalle);
        }

        public List<management_alertasDispensacion_datosTramite_respuestasResult> TraerDatosAMTramiteRespuestas(int? idDetalle)
        {
            return DACConsulta.TraerDatosAMTramiteRespuestas(idDetalle);
        }

        public List<management_alertasDispensacion_tableroControl_idResult> TraerRegistroAlerta(int? idRegistro)
        {
            return DACConsulta.TraerRegistroAlerta(idRegistro);
        }

        public List<ref_alertas_dispensacion_GestionTramite> ListadoTipotramite()
        {
            return DACConsulta.ListadoTipotramite();
        }

        public List<management_alertasDispensacion_buscarNombreComercialResult> TraerNombreComercial(string nombre_comercial)
        {
            return DACConsulta.TraerNombreComercial(nombre_comercial);
        }


        public List<management_alertasDispensacion_tableroControl_gestionadasResult> ListadoAlertasDispensacionGestionadas()
        {
            return DACConsulta.ListadoAlertasDispensacionGestionadas();
        }


        public List<management_alertasDispensacion_tableroControl_gestionadasDetalladaResult> ListadoAlertasDispensacionGestionadasDetallada()
        {
            return DACConsulta.ListadoAlertasDispensacionGestionadasDetallada();
        }

        public List<management_alertasDispensacion_tableroControl_gestionadasArchivosResult> ListadoAlertasDispensacionGestionadasArchivos(int? idDetalle)
        {
            return DACConsulta.ListadoAlertasDispensacionGestionadasArchivos(idDetalle);
        }

        public int InsertarRespuestaAlertaDiepnsacion(alertas_dispensacion_detalle obj)
        {
            return DACInserta.InsertarRespuestaAlertaDiepnsacion(obj);
        }

        public int InsertarLogRespuestaAlertaDiepnsacion(log_alertas_dispensacion_detalle obj)
        {
            return DACInserta.InsertarLogRespuestaAlertaDiepnsacion(obj);
        }


        public int InsertarRespuestaAlertaDiepnsacionTramite(List<alertas_dispensacion_detalle_entramite> obj)
        {
            return DACInserta.InsertarRespuestaAlertaDiepnsacionTramite(obj);
        }

        public int InsertarRespuestaAlertaDiepnsacionTramiteRespuestas(List<alertas_dispensacion_detalle_entramite_respuestas> obj)
        {
            return DACInserta.InsertarRespuestaAlertaDiepnsacionTramiteRespuestas(obj);
        }

        public int ActualizarAlertaDispensacionDetalle(alertas_dispensacion_detalle obj)
        {
            return DACActualiza.ActualizarAlertaDispensacionDetalle(obj);
        }

        public int InsertarArchivoAlertasDispen(alertas_dispensacion_detalle_archivos obj)
        {
            return DACInserta.InsertarArchivoAlertasDispen(obj);
        }

        public alertas_dispensacion_detalle_archivos TraerArchivoAlertasDispen(int? idArchivo)
        {
            return DACConsulta.TraerArchivoAlertasDispen(idArchivo);
        }

        public List<management_alertasDispensacion_tableroControl_respuestasResult> ListadoAlertasDispensacionGestiones(int? idDetalle)
        {
            return DACConsulta.ListadoAlertasDispensacionGestiones(idDetalle);
        }

        #endregion

        #region MORTALIDAD_NATALIDAD

        public int InsercionMortalidadRegistro(mortalidad_registros obj)
        {
            return DACInserta.InsercionMortalidadRegistro(obj);
        }

        public List<management_tiposBeneficiarioResult> TraerTiposBeneficiario()
        {
            return DACConsulta.TraerTiposBeneficiario();
        }

        public mortalidad_registros TraerDatosMortalidad(int? idMortalidad)
        {
            return DACConsulta.TraerDatosMortalidad(idMortalidad);
        }


        public mortalidad_registros TraerDatosMortalidadIdentificacion(string identificacion)
        {
            return DACConsulta.TraerDatosMortalidadIdentificacion(identificacion);
        }

        public List<management_TableroMortalidadResult> TraerMortalidadesTablero(int? año, int? trimestre, int? mes, int? unis, int? regional, string documento, DateTime? fechaFiltro)
        {
            return DACConsulta.TraerMortalidadesTablero(año, trimestre, mes, unis, regional, documento, fechaFiltro);
        }
        public List<management_TableroNatalidadResult> TraerNatalidadesTablero(int? año, int? trimestre, int? mes, int? unis, int? regional, string documento, DateTime? fechaFiltro)
        {
            return DACConsulta.TraerNatalidadesTablero(año, trimestre, mes, unis, regional, documento, fechaFiltro);
        }
        public int ActualizarRegistroMortalidad(mortalidad_registros reg)
        {
            return DACActualiza.ActualizarRegistroMortalidad(reg);
        }

        public int InsercionNatalidadRegistro(natalidad_registros obj)
        {
            return DACInserta.InsercionNatalidadRegistro(obj);
        }

        public int ActualizarRegistroNatalidad(natalidad_registros nat)
        {
            return DACActualiza.ActualizarRegistroNatalidad(nat);
        }

        public natalidad_registros TraerDatosNatalidad(int? idNatalidad)
        {
            return DACConsulta.TraerDatosNatalidad(idNatalidad);
        }

        public int EliminarMOrtalidadId(int? idMortalidad, string usuario)
        {
            return DACElimina.EliminarMOrtalidadId(idMortalidad, usuario);
        }

        public int EliminarNatalidadId(int? idNatalidad, string usuario)
        {
            return DACElimina.EliminarNatalidadId(idNatalidad, usuario);
        }

        #endregion

        #region eventos_salud
        public int CargueEventosSalud(evento_salud_cargue obj, List<eventos_salud_registros> detalle, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.CargueEventosSalud(obj, detalle, ref MsgRes);
        }

        public int InsertarEventoSalud(eventos_salud_registros evento)
        {
            return DACInserta.InsertarEventoSalud(evento);
        }

        public List<ref_eventosSalud_fuenteReporte> ListaEventosSaludFuenteReporte()
        {
            return DACConsulta.ListaEventosSaludFuenteReporte();
        }

        public List<ref_eventosSalud_ambitoOcurrencia> ListaEventosSaludAmbitoOcurrencia()
        {
            return DACConsulta.ListaEventosSaludAmbitoOcurrencia();
        }

        public List<ref_eventosSalud_severidadDesenlace> ListaEventosSaludSeveridadDesenlace()
        {
            return DACConsulta.ListaEventosSaludSeveridadDesenlace();
        }

        public List<ref_eventosSalud_ProbabilidadRepeticion> ListaEventosSaludProbabilidadRepeticion()
        {
            return DACConsulta.ListaEventosSaludProbabilidadRepeticion();
        }

        public List<ref_eventosSalud_conceptoAuditoria> ListaEventosSaludConceptoAuditoria()
        {
            return DACConsulta.ListaEventosSaludConceptoAuditoria();
        }

        public List<ref_eventosSalud_seguimiento> ListaEventosSaludSeguimiento()
        {
            return DACConsulta.ListaEventosSaludSeguimiento();
        }
        public List<ref_eventosSalud_categoriaEvento> ListaEventosSaludCategoria()
        {
            return DACConsulta.ListaEventosSaludCategoria();
        }
        public List<ref_eventosSalud_subCategoriaEvento> ListaEventosSaludSubCategoria()
        {
            return DACConsulta.ListaEventosSaludSubCategoria();
        }

        public List<ref_eventosSalud_subCategoriaEvento> EventosSaludTraerSubCategoriaId(int? idCategoria)
        {
            return DACConsulta.EventosSaludTraerSubCategoriaId(idCategoria);
        }

        public List<ref_eventosSalud_resultadoNegativo> ListaEventosSaludResNegativoIdCategoriaClasificacion(int? idCategoria, int? idClasificacin)
        {
            return DACConsulta.ListaEventosSaludResNegativoIdCategoriaClasificacion(idCategoria, idClasificacin);
        }

        public List<ref_eventosSalud_resultadoNegativo> ListaEventosSaludResNegativo()
        {
            return DACConsulta.ListaEventosSaludResNegativo();
        }

        public List<ref_eventosSalud_clasificacionEvento> ListaEventosSaludClasificacion()
        {
            return DACConsulta.ListaEventosSaludClasificacion();
        }

        public List<management_eventosSalud_tableroResult> ListadoEventosEnSaludTablero(int? mes, int? año)
        {
            return DACConsulta.ListadoEventosEnSaludTablero(mes, año);
        }


        public eventos_salud_registros TraerDatosEventosSaludId(int? idEvento)
        {
            return DACConsulta.TraerDatosEventosSaludId(idEvento);
        }

        public Ref_ips_cuentas getprestadoresNit(string nit)
        {
            return DACConsulta.getprestadoresNit(nit);
        }

        public int InsertarPrestadorIpsCuentas(Ref_ips_cuentas obj)
        {
            return DACInserta.InsertarPrestadorIpsCuentas(obj);
        }

        public int ActualizarRegistroEventosSalud(eventos_salud_registros even)
        {
            return DACActualiza.ActualizarRegistroEventosSalud(even);
        }

        #endregion

        #region Vistas



        public List<cronograma_visita_documento> ObtenerListadoDocumentosVisitas()
        {
            return DACConsulta.ObtenerListadoDocumentosVisitas();
        }
        public List<management_cronograma_visita_traerByteResult> ObtenerListadoDocumentosVisitasSinRuta()
        {
            return DACConsulta.ObtenerListadoDocumentosVisitasSinRuta();
        }

        public int EliminarActaVisitasCronogramaId(int? idCronograma, ref MessageResponseOBJ MsgRes)
        {
            return DACElimina.EliminarActaVisitasCronogramaId(idCronograma, ref MsgRes);
        }

        public int EliminarInformeOperativo(int? idCronograma, ref MessageResponseOBJ MsgRes)
        {
            return DACElimina.EliminarInformeOperativo(idCronograma, ref MsgRes);
        }



        public int GuardarLogEliminarActaVisitas(int? idVisita, int? idArchivo, string nombreArchivo, string usuario)
        {
            return DACInserta.GuardarLogEliminarActaVisitas(idVisita, idArchivo, nombreArchivo, usuario);
        }


        #endregion

        #region ENCUESTA SAMI

        public List<management_encuesta_tipoPreguntaResult> listaEncuestaSAMI()
        {
            return DACConsulta.listaEncuestaSAMI();
        }

        public int InsertarRespuestaSAMI(encuesta_sami dato, List<encuesta_sami_respuestas> detalles, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarRespuestaSAMI(dato, detalles, ref MsgRes);
        }

        public List<management_encuesta_sami_datosResult> listaRespuestasSAMI()
        {
            return DACConsulta.listaRespuestasSAMI();
        }

        public List<management_encuesta_sami_datos_detalleResult> listaRespuestasSAMIDetalle()
        {
            return DACConsulta.listaRespuestasSAMIDetalle();
        }

        public List<management_encuesta_sami_datos_promediosResult> listaRespuestasSAMIPromedios()
        {
            return DACConsulta.listaRespuestasSAMIPromedios();
        }

        public encuesta_sami TraerEncuestaEsteMes()
        {
            return DACConsulta.TraerEncuestaEsteMes();
        }

        #endregion ENCUESTA SAMI

        #region FIS PRESTADORES

        public management_fisPrestadoresResult TraerPrestadorId(int? idPrestador)
        {
            return DACConsulta.TraerPrestadorId(idPrestador);
        }

        public List<management_fisPrestadores_sedesResult> TraerPrestadorSedesId(int? idPrestador)
        {
            return DACConsulta.TraerPrestadorSedesId(idPrestador);
        }

        public int InsertarPrestadorFis(fis_rips_prestadores prestador)
        {
            return DACInserta.InsertarPrestadorFis(prestador);
        }

        public int InsertarSedePrestadorFis(fis_rips_prestadores_sedes sede)
        {
            return DACInserta.InsertarSedePrestadorFis(sede);
        }

        public int TraerprestadorSedeCodHabilitacion(string codigo_sede, string codigo_habilitacion, int? idPrestador)
        {
            return DACConsulta.TraerprestadorSedeCodHabilitacion(codigo_sede, codigo_habilitacion, idPrestador);
        }

        public int ExisteTigaContrato(int? idTiga, int? idContrato)
        {
            return DACConsulta.ExisteTigaContrato(idTiga, idContrato);
        }

        public int EliminarCupsFis(int? idCups)
        {
            return DACElimina.EliminarCupsFis(idCups);
        }

        public int ActualizarEstadoPrestadoresFIS(int? idPrestador)
        {
            return DACActualiza.ActualizarEstadoPrestadoresFIS(idPrestador);
        }

        public int EliminarSedePrestadores(int? idSede)
        {
            return DACElimina.EliminarSedePrestadores(idSede);
        }

        public int ActualizarDatosPrestador(fis_rips_prestadores pre)
        {
            return DACActualiza.ActualizarDatosPrestador(pre);
        }

        public List<management_regional_usuarioResult> ListadoRegionalesUsuarioId(int? idUsuario)
        {
            return DACConsulta.ListadoRegionalesUsuarioId(idUsuario);
        }

        public List<management_usuarios_regionalIdResult> listadoRegionalesUsuarioReg(int? idRegional)
        {
            return DACConsulta.listadoRegionalesUsuarioReg(idRegional);
        }

        public int GuardarArchivosPrestador(fis_rips_prestadores_archivos archivo)
        {
            return DACInserta.GuardarArchivosPrestador(archivo);
        }

        public List<management_fisPrestadores_tableroControlResult> TableroControlPrestadores(string nit, string sap)
        {
            return DACConsulta.TableroControlPrestadores(nit, sap);
        }

        public List<management_fisPrestadores_tableroControl_detalladoResult> TableroControlPrestadoresDetallado(string nit, string sap)
        {
            return DACConsulta.TableroControlPrestadoresDetallado(nit, sap);
        }

        public List<management_fisPrestadores_tableroControl_archivosResult> TraerArchivosPrestadorId(int? idPrestador)
        {
            return DACConsulta.TraerArchivosPrestadorId(idPrestador);
        }

        public fis_rips_prestadores_archivos ArchivoPrestadorId(int? idArchivo)
        {
            return DACConsulta.ArchivoPrestadorId(idArchivo);
        }

        public int EliminarArchivoPrestador(int? idArchivo)
        {
            return DACElimina.EliminarArchivoPrestador(idArchivo);
        }

        public int EliminarNegociacionContrato(int? idMasivo)
        {
            return DACElimina.EliminarNegociacionContrato(idMasivo);
        }

        public fis_rips_prestadores_contratos TraerDatosContratoPrestadorId(int? idContrato)
        {
            return DACConsulta.TraerDatosContratoPrestadorId(idContrato);
        }

        public fis_rips_prestadores_contratos TraerDatosContratoPrestadorIdPrestador(int? idPrestador)
        {
            return DACConsulta.TraerDatosContratoPrestadorIdPrestador(idPrestador);
        }

        public int InsertarLogEliminarNegociacion(log_fisPrestador_contrato_eliminar obj)
        {
            return DACInserta.InsertarLogEliminarNegociacion(obj);
        }

        public List<fis_rips_prestadores> ConsultaPrestadoresFis(string nit, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaPrestadoresFis(nit, ref MsgRes);
        }

        public List<fis_rips_prestadores> ConsultaPrestadoresFisCodHab(string codHabilitacion, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaPrestadoresFisCodHab(codHabilitacion, ref MsgRes);
        }


        public List<fis_rips_prestadores_contratos> ConsultaContratoPrestadoresFis(string numContrato, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaContratoPrestadoresFis(numContrato, ref MsgRes);
        }

        public List<fis_rips_prestadores> ConsultaPrestadoresFisSAP(decimal sap, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaPrestadoresFisSAP(sap, ref MsgRes);
        }

        public List<fis_rips_tigas> TraerTigas()
        {
            return DACConsulta.TraerTigas();
        }

        public List<ref_tigas_detallados> TraerTigasDetallados()
        {
            return DACConsulta.TraerTigasDetallados();
        }

        public List<fis_rips_tigas> TraerTigasCod(string codigo)
        {
            return DACConsulta.TraerTigasCod(codigo);
        }

        public List<ref_tigas_detallados> TraerTigasDetalladosCod(string detallado)
        {
            return DACConsulta.TraerTigasDetalladosCod(detallado);
        }

        public management_fisPrestadores_contratosResult TraerDatosContrato(int? idCOntrato)
        {
            return DACConsulta.TraerDatosContrato(idCOntrato);
        }

        public List<management_fisPrestadores_contratos_tigasResult> TraerDatosContratoTigas(int? idCOntrato)
        {
            return DACConsulta.TraerDatosContratoTigas(idCOntrato);
        }

        public int GuardarContratoPrestador(fis_rips_prestadores_contratos contrato)
        {
            return DACInserta.GuardarContratoPrestador(contrato);
        }
        public int ActualizarDatosContratoPrestador(fis_rips_prestadores_contratos contra)
        {
            return DACActualiza.ActualizarDatosContratoPrestador(contra);
        }

        public int GuardarTigaContratoPrestador(fis_rips_prestadores_contrato_tigas tiga)
        {
            return DACInserta.GuardarTigaContratoPrestador(tiga);
        }

        public int EliminarTigaContratosPrestadores(int? idTiga)
        {
            return DACElimina.EliminarTigaContratosPrestadores(idTiga);
        }

        public int ActualizarEstadoTigasContrato(int? idContrato)
        {
            return DACActualiza.ActualizarEstadoTigasContrato(idContrato);
        }

        public List<fis_rips_regional> TraerregionalesFis()
        {
            return DACConsulta.TraerregionalesFis();
        }

        public List<management_fis_departamento_regionalResult> TraerDepartamentosFis(int? idRegional)
        {
            return DACConsulta.TraerDepartamentosFis(idRegional);
        }

        public List<ref_fis_departamentos> TraerDepartamentosFisTodos()
        {
            return DACConsulta.TraerDepartamentosFisTodos();
        }


        public List<fis_rips_ciudad> TraerCiudadesFis(int? idDepartamento)
        {
            return DACConsulta.TraerCiudadesFis(idDepartamento);
        }

        public int insertarCargueTarifas(fis_rips_prestadores_contratos_tarifas obj, List<fis_rips_prestadores_contratos_tarifas_registros> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.insertarCargueTarifas(obj, lista, ref MsgRes);
        }

        public int InsertarCargueDetalles(fis_rips_sinJson_lote obj, List<fis_rips_sinJson_detalle> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCargueDetalles(obj, lista, ref MsgRes);
        }

        public int InsertarCargueIVM(fis_rips_cargueMasivo_ivm_lote obj, List<fis_rips_cargueMasivo_ivm_registros> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCargueIVM(obj, lista, ref MsgRes);
        }

        public int InsertarCarguePedidos(fis_rips_cargueMasivo_pedidos_lote obj, List<fis_rips_cargueMasivo_pedidos_registros> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCarguePedidos(obj, lista, ref MsgRes);
        }


        public fis_rips_cups TraerCupsCodigo(string codigo)
        {
            return DACConsulta.TraerCupsCodigo(codigo);
        }

        public List<fis_rips_cups> TraerListadoCups()
        {
            return DACConsulta.TraerListadoCups();
        }


        public int ActualizarEstadoTarifas(int? idContrato)
        {
            return DACActualiza.ActualizarEstadoTarifas(idContrato);
        }

        public int ActualizarEstadoTarifasDiferenteId(int? idContrato, int? idTarifas)
        {
            return DACActualiza.ActualizarEstadoTarifasDiferenteId(idContrato, idTarifas);
        }


        public List<management_fisPrestadores_contratos_tableroControlResult> DatosTableroControlContratos()
        {
            return DACConsulta.DatosTableroControlContratos();
        }

        public List<management_fisPrestadores_contratos_tableroControl_porVencerResult> DatosTableroControlContratosxVencer()
        {
            return DACConsulta.DatosTableroControlContratosxVencer();
        }
        public int CrearCups(fis_rips_cups obj)
        {
            return DACInserta.CrearCups(obj);
        }

        public int ActualizarCupsFis(fis_rips_cups cups)
        {
            return DACActualiza.ActualizarCupsFis(cups);
        }

        public List<management_fis_cupsResult> TraerCUpsTablero()
        {
            return DACConsulta.TraerCUpsTablero();
        }
        public fis_rips_cups TraerCupsId(int? idCups)
        {
            return DACConsulta.TraerCupsId(idCups);
        }

        public List<management_fis_refTipoConsultasResult> ListadoTipoConsultaJson()
        {
            return DACConsulta.ListadoTipoConsultaJson();
        }

        public chatbot_ref_respuestas TraerRespuestaId(int? idRespuesta)
        {
            return DACConsulta.TraerRespuestaId(idRespuesta);
        }
        public chatbot_ref_preguntas TraerPreguntaId(int? idPregunta)
        {
            return DACConsulta.TraerPreguntaId(idPregunta);
        }
        public chatbot_ref_subprocesos TraerSubProcesoId(int? idSub)
        {
            return DACConsulta.TraerSubProcesoId(idSub);
        }
        public chatbot_ref_procesos TraerProcesoId(int? idProceso)
        {
            return DACConsulta.TraerProcesoId(idProceso);
        }
        public chatbot_ref_proyectos TraerProyectoId(int? idProyecto)
        {
            return DACConsulta.TraerProyectoId(idProyecto);
        }
        public List<fis_rips_cargue_LoteConsultas> ConsultaCUVFIS(string codCuv, ref MessageResponseOBJ MsgRes)
        {
            return DACConsulta.ConsultaCUVFIS(codCuv, ref MsgRes);
        }

        public List<management_fis_cargueRips_consultaResult> ListadoRipsConsulta(string codCuv)
        {
            return DACConsulta.ListadoRipsConsulta(codCuv);
        }


        public List<management_fis_cargueRips_hospitalizacionResult> ListadoRipsHospitalizacion(string codCuv)
        {
            return DACConsulta.ListadoRipsHospitalizacion(codCuv);
        }

        public List<management_fis_cargueRips_medicamentosResult> ListadoRipsMedicamentos(string codCuv)
        {
            return DACConsulta.ListadoRipsMedicamentos(codCuv);
        }

        public List<management_fis_cargueRips_otrosServiciosResult> ListadoRipsOtrosServicios(string codCuv)
        {
            return DACConsulta.ListadoRipsOtrosServicios(codCuv);
        }

        public List<management_fis_cargueRips_procedimientosResult> ListadoRipsProcedimientos(string codCuv)
        {
            return DACConsulta.ListadoRipsProcedimientos(codCuv);
        }

        public List<management_fis_cargueRips_recienNacidoResult> ListadoRipsRecienNacido(string codCuv)
        {
            return DACConsulta.ListadoRipsRecienNacido(codCuv);
        }

        public List<management_fis_cargueRips_transaccionResult> ListadoRipsTransaccion(string codCuv)
        {
            return DACConsulta.ListadoRipsTransaccion(codCuv);
        }

        public List<management_fis_cargueRips_urgenciasResult> ListadoRipsUrgencias(string codCuv)
        {
            return DACConsulta.ListadoRipsUrgencias(codCuv);
        }

        public List<management_fis_cargueRips_usuariosResult> ListadoRipsUsuarios(int? idFactura)
        {
            return DACConsulta.ListadoRipsUsuarios(idFactura);
        }

        public management_prestadores_traerCUVResult TraerCuv(int? idDetalle)
        {
            return DACConsulta.TraerCuv(idDetalle);
        }

        public int EliminarDatosRipsFisFacturasIdFactura(int? idFactura)
        {
            return DACElimina.EliminarDatosRipsFisFacturasIdFactura(idFactura);
        }

        public int EliminarGlosaFactura(int? idGlosa)
        {
            return DACElimina.EliminarGlosaFactura(idGlosa);
        }

        public int EliminarRegistroRipsFis(int? idRegistro)
        {
            return DACElimina.EliminarRegistroRipsFis(idRegistro);
        }

        public List<management_baseBeneficiarios_tableroControlResult> TraerListadoBaseBeneficiarios()
        {
            return DACConsulta.TraerListadoBaseBeneficiarios();
        }

        public List<management_baseBeneficiarios_consolidadoDetallePeriodoResult> TraerListadoBaseBeneficiariosConsolidado(string periodo)
        {
            return DACConsulta.TraerListadoBaseBeneficiariosConsolidado(periodo);
        }

        public int EliminarBaseBeneficiariosPeriodo(string periodo)
        {
            return DACElimina.EliminarBaseBeneficiariosPeriodo(periodo);
        }


        public base_beneficiarios TraerBeneficiarioCedula(string cedula)
        {
            return DACConsulta.TraerBeneficiarioCedula(cedula);
        }

        public management_fis_traerDatosContrato_nitResult TraerDatosContratoNit(string filtrado)
        {
            return DACConsulta.TraerDatosContratoNit(filtrado);
        }

        public List<management_fis_traerDatosContrato_nitResult> TraerListaDatosContratoNit(string filtrado)
        {
            return DACConsulta.TraerListaDatosContratoNit(filtrado);
        }


        public management_fis_traerFechas_cuvResult TraerLimiteFechasFactura(int? factura)
        {
            return DACConsulta.TraerLimiteFechasFactura(factura);
        }

        //public List<management_fis_facturasCuvResult> TraerListadoCupsFacturas(string cuv, int? idDetalle, int? idUsuario)
        //{
        //    return DACConsulta.TraerListadoCupsFacturas(cuv, idDetalle, idUsuario);
        //}

        public List<management_fis_facturasCuvResult> TraerListadoCupsFacturas(int? idDetalle)
        {
            return DACConsulta.TraerListadoCupsFacturas(idDetalle);
        }

        public List<management_fis_facturasCuv_conBeneficiariosResult> TraerListadoCupsFacturasConBenficiarios(int? idDetalle)
        {
            return DACConsulta.TraerListadoCupsFacturasConBenficiarios(idDetalle);

        }
        public int InsertarGlosaFacturaFis(fis_rips_facturas_glosa obj)
        {
            return DACInserta.InsertarGlosaFacturaFis(obj);
        }

        public List<management_fisBuscarCupsResult> TraerListaCupsIdFactura(string cups, int? idFactura)
        {
            return DACConsulta.TraerListaCupsIdFactura(cups, idFactura);
        }

        public List<ref_fisFacturas_conceptoGeneral> ListadoConceptosGeneral()
        {
            return DACConsulta.ListadoConceptosGeneral();
        }

        public List<ref_fisFacturas_conceptoEspecifico> ListadoConceptosEspecifico(int? idGeneral)
        {
            return DACConsulta.ListadoConceptosEspecifico(idGeneral);
        }

        public List<ref_fisFacturas_conceptoAplicacion> ListadoConceptosAplicacion(int? especifico)
        {
            return DACConsulta.ListadoConceptosAplicacion(especifico);
        }

        public List<management_fisFacturas_glosaResult> ListaGlosas(int? idDetalle)
        {
            return DACConsulta.ListaGlosas(idDetalle);
        }

        public List<management_fis_facturasCuvGlosas_reporteResult> ListaReporteCupsGlosas(int? idFactura)
        {
            return DACConsulta.ListaReporteCupsGlosas(idFactura);
        }

        public List<ref_cie10_fis> listadoCie10FISCodigo(string cie10)
        {
            return DACConsulta.listadoCie10FISCodigo(cie10);
        }

        public List<ref_cie10_fis> listadoCie10FIS()
        {
            return DACConsulta.listadoCie10FIS();
        }

        public List<ref_cie10_fis> listadoCie10FISDescripcion(string descripcion)
        {
            return DACConsulta.listadoCie10FISDescripcion(descripcion);
        }

        public ref_cie10_fis TraerCie10Codigo(string codigo)
        {
            return DACConsulta.TraerCie10Codigo(codigo);
        }

        public ref_cie10_fis TraerCie10Descripcion(string descripcion)
        {
            return DACConsulta.TraerCie10Descripcion(descripcion);
        }

        public int InsertarFacturaFis(fis_rips_facturas obj)
        {
            return DACInserta.InsertarFacturaFis(obj);
        }

        public int InsertarLogFacturaFis(log_fis_rips_facturas obj)
        {
            return DACInserta.InsertarLogFacturaFis(obj);
        }

        public int InsertarUsuariosFisMasivo(List<fis_rips_facturas_usuarios> list)
        {
            return DACInserta.InsertarUsuariosFisMasivo(list);
        }



        public int ActualizarEstadoFacturaFis(int? idDetalle, int? estado)
        {
            return DACActualiza.ActualizarEstadoFacturaFis(idDetalle, estado);
        }

        public int Levantarglosa(int? idGlosa)
        {
            return DACActualiza.Levantarglosa(idGlosa);
        }
        public int MantenerGlosa(int? idGlosa, string observacionMantenida)
        {
            return DACActualiza.MantenerGlosa(idGlosa, observacionMantenida);
        }

        public List<fis_rips_facturas_glosa> TraerGlosasMantenidas(int? idFactura)
        {
            return DACConsulta.TraerGlosasMantenidas(idFactura);
        }

        public List<management_fis_glosaTieneNotaCreditoResult> TraerGlosasConNoptaCredito(int? idFactura)
        {
            return DACConsulta.TraerGlosasConNoptaCredito(idFactura);
        }

        public int ActualizarTipoIva(int? idRegistro, string tipo_iva, int? iva)
        {
            return DACActualiza.ActualizarTipoIva(idRegistro, tipo_iva, iva);
        }

        public management_fis_devolverIVAResult DevolverIvaTotalIdFactura(int? idFactura)
        {
            return DACConsulta.DevolverIvaTotalIdFactura(idFactura);
        }


        public int FinalizarGlosa(int? idGlosa)
        {
            return DACActualiza.FinalizarGlosa(idGlosa);
        }

        public int ActualizarEstadoGlosa_prestadores(int? idRegistro, int? estado)
        {
            return DACActualiza.ActualizarEstadoGlosa_prestadores(idRegistro, estado);
        }


        public management_fisFactura_ultimoDiagnosticoResult TraerUltimoDiagnosticoCie100FacturaFis(string cuv)
        {
            return DACConsulta.TraerUltimoDiagnosticoCie100FacturaFis(cuv);
        }
        public management_fisFactura_ultimoDiagnostico_relacionadoResult TraerUltimoDiagnosticoCie100FacturaFisRelacionado(string cuv)
        {
            return DACConsulta.TraerUltimoDiagnosticoCie100FacturaFisRelacionado(cuv);
        }


        public int InsertarLogGlosaFacturasFis(log_fisFactura_levantarGlosa obj)
        {
            return DACInserta.InsertarLogGlosaFacturasFis(obj);
        }

        public int InsertarLogGlosaFacturasFisMantener(log_fisFactura_mantenerGlosa obj)
        {
            return DACInserta.InsertarLogGlosaFacturasFisMantener(obj);
        }

        public List<management_fisFacturas_buscarExistenciaCuvResult> ListadoCuvExistentesCodigo(string cuv)
        {
            return DACConsulta.ListadoCuvExistentesCodigo(cuv);
        }

        public int ActualizarCuvFacturaId(int? idFactura, string cuv)
        {
            return DACActualiza.ActualizarCuvFacturaId(idFactura, cuv);
        }

        public fis_rips_facturas_glosa ListadoGlosaSinLevantar(int? idFactura)
        {
            return DACConsulta.ListadoGlosaSinLevantar(idFactura);
        }

        public int ActualizarVersionFactura(int? idDetalle)
        {
            return DACActualiza.ActualizarVersionFactura(idDetalle);
        }

        public List<management_fis_rips_usuariosSinTigaResult> TraerUsuariosSinTiga(int? idAf)
        {
            return DACConsulta.TraerUsuariosSinTiga(idAf);
        }

        public List<management_fis_rips_usuariosSinFechaPrestacionResult> TraerUsuariosSinFechaPrestacion(int? idAf)
        {
            return DACConsulta.TraerUsuariosSinFechaPrestacion(idAf);
        }
        public List<management_fisGlosasSubsanadas_prestadorResult> ListaGlosasSubsanadasPrestador(int? idDetalle)
        {
            return DACConsulta.ListaGlosasSubsanadasPrestador(idDetalle);
        }

        public List<management_fisPrestadores_contratos_negociacionesResult> TraerNegociacionesContrato(int? idContrato)
        {
            return DACConsulta.TraerNegociacionesContrato(idContrato);
        }

        public List<management_fisPrestadores_contratos_negociaciones_idPrestadorResult> TraerNegociacionesPrestador(int? idPrestador)
        {
            return DACConsulta.TraerNegociacionesPrestador(idPrestador);
        }

        public List<management_fisPrestadores_contratos_negociaciones_detalleResult> TraerNegociacionesContratoDetalle(int? idMasivo)
        {
            return DACConsulta.TraerNegociacionesContratoDetalle(idMasivo);
        }

        public fis_rips_facturas_glosa TraerGlosaRips(int? idGlosa)
        {
            return DACConsulta.TraerGlosaRips(idGlosa);
        }

        public int ActualizarGlosaRipsFactura(fis_rips_facturas_glosa obj)
        {
            return DACActualiza.ActualizarGlosaRipsFactura(obj);
        }

        public int GuardarRegistroTarifasRipsFacturas(string cuv, int? idFactura, string usuario)
        {
            return DACInserta.GuardarRegistroTarifasRipsFacturas(cuv, idFactura, usuario);
        }

        public int InsertarFisRipsConsultaMasivo(List<fis_rips_cargue_consulta> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarFisRipsConsultaMasivo(lista, ref MsgRes);
        }

        public fis_rips_cargue_registrosCompletos TraerRegistroRipsId(int? idRegistro)
        {
            return DACConsulta.TraerRegistroRipsId(idRegistro);
        }

        public fis_rips_cargue_registrosCompletos TraerRegistroRipsIdFactura(int? idFactura)
        {
            return DACConsulta.TraerRegistroRipsIdFactura(idFactura);
        }

        public int ActualizarRipsFactura(fis_rips_cargue_registrosCompletos obj)
        {
            return DACActualiza.ActualizarRipsFactura(obj);
        }


        public int ActualizarRipsFacturaTiga(fis_rips_cargue_registrosCompletos obj)
        {
            return DACActualiza.ActualizarRipsFacturaTiga(obj);

        }

        public int ActualizarTigaRipsCompletos(fis_rips_cargue_registrosCompletos obj)
        {
            return DACActualiza.ActualizarTigaRipsCompletos(obj);
        }

        public int ActualizarRipsFacturaFac(fis_rips_facturas obj)
        {
            return DACActualiza.ActualizarRipsFacturaFac(obj);
        }

        public int IngresarNuevoRipsFis(fis_rips_cargue_registrosCompletos obj)
        {
            return DACInserta.IngresarNuevoRipsFis(obj);
        }

        public int IngresarLogRipsFis(log_fis_rips_cargue_registrosCompletos obj)
        {
            return DACInserta.IngresarLogRipsFis(obj);
        }

        public int EliminarTodoElCargueFisIdFactura(int? idFactura)
        {
            return DACInserta.EliminarTodoElCargueFisIdFactura(idFactura);
        }

        public int ActualizarCantidadCupsEnGlosa(int? cantidad, int? idRegistro)
        {
            return DACActualiza.ActualizarCantidadCupsEnGlosa(cantidad, idRegistro);
        }

        public ref_tigas_detallados TraerTigasDetallados(string tiga)
        {
            return DACConsulta.TraerTigasDetallados(tiga);
        }


        public List<management_fis_buscarExietenciaBeneficiarioResult> BuscarBeneficiarioEnFis(DateTime? fecha, string tipoId, string identificacion)
        {
            return DACConsulta.BuscarBeneficiarioEnFis(fecha, tipoId, identificacion);
        }

        public management_fis_buscarFechaAtencion_idUsuarioResult BuscarFechaAtencionUsuario(int? idUsuario, int? idTransaccion)
        {
            return DACConsulta.BuscarFechaAtencionUsuario(idUsuario, idTransaccion);
        }

        public List<management_prestadoresFacturasGestionadas_fisResult> TraerListadoFacturas()
        {
            return DACConsulta.TraerListadoFacturas();
        }

        public fis_rips_facturas TraerFacturaIdAf(int? idAf)
        {
            return DACConsulta.TraerFacturaIdAf(idAf);
        }


        public fis_rips_facturas TraerFacturaIdAfAuditor(int? idAf)
        {
            return DACConsulta.TraerFacturaIdAfAuditor(idAf);
        }

        public fis_rips_prestadores PrestadorxNit(string nit)
        {
            return DACConsulta.PrestadorxNit(nit);
        }

        public List<management_fis_rips_ParaActualizar_tigasResult> TraerListadoCUPSActualziarTigas(int? idAf,
            DateTime? fechaIni, DateTime? fechaFin, string codPrestador, string numFactura, string regional)
        {
            return DACConsulta.TraerListadoCUPSActualziarTigas(idAf, fechaIni, fechaFin, codPrestador, numFactura, regional);
        }

        public List<management_fisRips_TableroDetallesCargueResult> ListadoDetallesMasivo(DateTime? fechaIni, DateTime? fechaFin, int? regional)
        {
            return DACConsulta.ListadoDetallesMasivo(fechaFin, fechaFin, regional);
        }

        public int EliminarCargueMasivoDetalles(int? idCargue)
        {
            return DACElimina.EliminarCargueMasivoDetalles(idCargue);
        }

        public int EliminarCargueMasivoDetallesSinJson(int? idCargue)
        {
            return DACElimina.EliminarCargueMasivoDetallesSinJson(idCargue);
        }

        public int InsertarLogEliminarMasivoDetalles(log_eliminar_cargueMasivo_detalles obj)
        {
            return DACInserta.InsertarLogEliminarMasivoDetalles(obj);
        }

        public List<fis_rips_sinJson_detalle> TraerListadoCargueMasivo(int? idDetalle)
        {
            return DACConsulta.TraerListadoCargueMasivo(idDetalle);
        }

        public management_fisBuscarCuv_idFacturaResult FisBuscarCuv(int? idDetalle)
        {
            return DACConsulta.FisBuscarCuv(idDetalle);
        }

        public List<management_fisMasivo_buscarFacturasResult> FisBuscarFacturas(int? idDetalle)
        {
            return DACConsulta.FisBuscarFacturas(idDetalle);
        }

        public List<management_fisMasivo_buscarUsuariosResult> FisBuscarFacturasUsuarios(int? idDetalle, string numFactura)
        {
            return DACConsulta.FisBuscarFacturasUsuarios(idDetalle, numFactura);
        }

        public List<management_fisMasivo_buscarServiciosResult> FisBuscarFacturasServicios(int? idDetalle, string numDocumento)
        {
            return DACConsulta.FisBuscarFacturasServicios(idDetalle, numDocumento);
        }

        public List<fis_rips_cargueMasivo_ivm_registros> ExisteIVM()
        {
            return DACConsulta.ExisteIVM();
        }

        public List<management_fis_tableroCargueContableSapResult> TraerGlosasTerminadasDocumentoSAP()
        {
            return DACConsulta.TraerGlosasTerminadasDocumentoSAP();
        }

        public int ActulizaGlosaDocContable(fis_rips_facturas_glosa obj)
        {
            return DACActualiza.ActulizaGlosaDocContable(obj);
        }


        public List<management_fis_reporteHesResult> TraerDatosReporteHES(DateTime? fechaIni, DateTime? fechaFin, string regional)
        {
            return DACConsulta.TraerDatosReporteHES(fechaIni, fechaFin, regional);
        }

        public List<fis_rips_cargueMasivo_pedidos_registros> ListadoDatosFacturasContables()
        {
            return DACConsulta.ListadoDatosFacturasContables();
        }

        public int ActualizarFacturasAContabilizadas(int? idLote)
        {
            return DACActualiza.ActualizarFacturasAContabilizadas(idLote);
        }


        public int ActualizarDiagnosticosCie10Fis(int? idUsuario, int? idFactura, string dx, string dxDes, string relDx, string relDxDes, string dxA,
     string dxDesA, string relDxA, string relDxDesA)
        {
            return DACActualiza.ActualizarDiagnosticosCie10Fis(idUsuario, idFactura, dx, dxDes, relDx, relDxDes, dxA, dxDesA, relDxA, relDxDesA);
        }

        public List<management_fisSinJson_DescargarDatosResult> TraerListadoDetallesSinJson(int? idLote)
        {
            return DACConsulta.TraerListadoDetallesSinJson(idLote);
        }

        public management_fisFacturas_sumaGlosasValidacionResult BuscarSumatoriaGlosas(int? idRegistro, int? idFactura, int? idUsuario, string cups, decimal? valorNuevo)
        {
            return DACConsulta.BuscarSumatoriaGlosas(idRegistro, idFactura, idUsuario, cups, valorNuevo);
        }

        public int HayDetallesIdFactura(int? idFactura)
        {
            return DACConsulta.HayDetallesIdFactura(idFactura);
        }

        public int EliminarSinJsonIdFactura(int? idFac)
        {
            return DACElimina.EliminarSinJsonIdFactura(idFac);
        }

        public int InsertarDatosGlosaCompleta(int? idFactura, int? concepto_general, int? concepto_especifico, int? concepto_aplicacion, string observacion, string usuario)
        {
            return DACInserta.InsertarDatosGlosaCompleta(idFactura, concepto_general, concepto_especifico, concepto_aplicacion, observacion, usuario);
        }

        public List<management_fis_cargarMasivoDetalles_idsFacturasResult> listadoFacturasCargueMasivo()
        {
            return DACConsulta.listadoFacturasCargueMasivo();
        }

        public management_fis_facturasCuv_conBeneficiarios_idRegistroResult TraerCupsRegistro(int? idFactura, int? idRegistro)
        {
            return DACConsulta.TraerCupsRegistro(idFactura, idRegistro);
        }

        public management_fisFacturas_glosa_idGlopsaResult traerGlosaId(int? idFactuea, int? idGlosa)
        {
            return DACConsulta.traerGlosaId(idFactuea, idGlosa);
        }

        public management_fis_TraerValoresFacturaResult TraerValoreFactura(int? idFactura)
        {
            return DACConsulta.TraerValoreFactura(idFactura);
        }

        public int ActualizarCIE10Registro(fis_rips_cargue_registrosCompletos dato)
        {
            return DACActualiza.ActualizarCIE10Registro(dato);
        }

        public List<fis_rips_cargue_usuarios> listadoUsuariosFisIdFactura(int? idFactura)
        {
            return DACConsulta.listadoUsuariosFisIdFactura(idFactura);
        }

        public int ExisteBeneficiario(string documento)
        {
            return DACConsulta.ExisteBeneficiario(documento);
        }

        public int InsertarCIE1OFIS(ref_cie10_fis obj)
        {
            return DACInserta.InsertarCIE1OFIS(obj);
        }

        public int InsertarLoteCIE10(ref_cie10_fis_lote obj)
        {
            return DACInserta.InsertarLoteCIE10(obj);
        }


        public int InsertarCargueCIE10Masivo(ref_cie10_fis_lote obj, List<ref_cie10_fis> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCargueCIE10Masivo(obj, lista, ref MsgRes);
        }

        public int ActualizarCie10FIS(ref_cie10_fis obj)
        {
            return DACActualiza.ActualizarCie10FIS(obj);
        }

        public int ElininarCIE10FIS(string codigo)
        {
            return DACElimina.ElininarCIE10FIS(codigo);
        }

        public int ActualizarTigaInteralFisFactura(int? tiga, int? idFactura)
        {
            return DACActualiza.ActualizarTigaInteralFisFactura(tiga, idFactura);
        }


        public int InsertarCargueMasivoContratos(fis_rips_prestadores_contratos_lote obj, List<fis_rips_prestadores_contratos> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarCargueMasivoContratos(obj, lista, ref MsgRes);
        }

        public List<management_fis_listadoPrestadoresExistentesResult> ListadoprestadoresFISeXISTENTES()
        {
            return DACConsulta.ListadoprestadoresFISeXISTENTES();
        }

        public List<management_fis_contratosExistentesResult> ListadoContratosFISExistntes()
        {
            return DACConsulta.ListadoContratosFISExistntes();
        }

        public int InsertarTIGASContratoMasivo(fis_rips_prestadores_contrato_tigas_lote obj, List<fis_rips_prestadores_contrato_tigas> lista, ref MessageResponseOBJ MsgRes)
        {
            return DACInserta.InsertarTIGASContratoMasivo(obj, lista, ref MsgRes);
        }

        public List<fis_rips_prestadores_contrato_tigas> listadoTIGAScontratoFIS()
        {
            return DACConsulta.listadoTIGAScontratoFIS();
        }

        public fis_rips_facturas TraerFacturaIdAfAnalista(int? idAf)
        {
            return DACConsulta.TraerFacturaIdAfAnalista(idAf);
        }

        public List<management_fis_tableroActualizarContratos_gestionAnalistaResult> ListadoGestionContratos()
        {
            return DACConsulta.ListadoGestionContratos();
        }

        public int DevolverNroPedidoFacturaId(int? idAf, string usuario)
        {
            return DACElimina.DevolverNroPedidoFacturaId(idAf, usuario);
        }

        public List<management_fis_facturasActuaRutaAprobadoResult> listadoFacturasActualizarAprobadas()
        {
            return DACConsulta.listadoFacturasActualizarAprobadas();
        }

        public int ActualizarContratoGestionFisFactura(int? idFac, fis_rips_prestadores_contratos obj)
        {
            return DACActualiza.ActualizarContratoGestionFisFactura(idFac, obj);
        }


        public int InsertarLogCambioContratoFactura(log_fis_rips_prestadores_contratos_actualizaciones obj)
        {
            return DACInserta.InsertarLogCambioContratoFactura(obj);
        }


        public List<management_fis_validacionExistentesTigasdetalles_contratoResult> ListaTigasSinContrato(int? idFactura)
        {
            return DACConsulta.ListaTigasSinContrato(idFactura);
        }

        public List<ref_facturas_novedades> TiposNovedadesFacturas()
        {
            return DACConsulta.TiposNovedadesFacturas();
        }

        public int InsertarNovedadFactura(factura_novedades obj)
        {
            return DACInserta.InsertarNovedadFactura(obj);
        }

        public int ActualizarNovedadFactura(factura_novedades obj)
        {
            return DACActualiza.ActualizarNovedadFactura(obj);
        }

        public List<management_facturasNovedades_buscarResult> TraerDatosNovedadesFacturas(int? idFactura, int? novedad)
        {
            return DACConsulta.TraerDatosNovedadesFacturas(idFactura, novedad);
        }

        public factura_novedades BuscarNovedadId(int? idNovedad)
        {
            return DACConsulta.BuscarNovedadId(idNovedad);
        }

        public int EliminarNovedadFactura(int? idNovedad)
        {
            return DACElimina.EliminarNovedadFactura(idNovedad);
        }

        public int ActualizarEstadoContrato(int? idContrato, int? estado)
        {
            return DACActualiza.ActualizarEstadoContrato(idContrato, estado);
        }

        public int ActualizarContabilizadoPedidoFactura(int? idFactura, string documentoConta, DateTime? fechaConta, string numPedido, DateTime? fechaPedido)
        {
            return DACActualiza.ActualizarContabilizadoPedidoFactura(idFactura, documentoConta, fechaConta, numPedido, fechaPedido);
        }

        #endregion FIS PRESTADORES

        #region CHATBOT

        public List<Management_chatbot_ref_proyectosResult> ChatBotProyectos()
        {
            return DACConsulta.ChatBotProyectos();
        }

        public List<Management_chatbot_ref_procesosResult> ChatBotProcesos(int? idProyecto)
        {
            return DACConsulta.ChatBotProcesos(idProyecto);
        }

        public List<Management_chatbot_ref_subprocesosResult> ChatBotSubProcesos(int? idProceso)
        {
            return DACConsulta.ChatBotSubProcesos(idProceso);
        }

        public List<Management_chatbot_ref_preguntasResult> ChatBotPreguntas(int? idSubProceso)
        {
            return DACConsulta.ChatBotPreguntas(idSubProceso);
        }

        public List<Management_chatbot_ref_respuestasResult> ChatBotRespuestas(int? idPregunta)
        {
            return DACConsulta.ChatBotRespuestas(idPregunta);
        }

        public int ChatBotInsertarProceso(chatbot_ref_procesos obj)
        {
            return DACInserta.ChatBotInsertarProceso(obj);
        }

        public int ChatBotInsertarSubProceso(chatbot_ref_subprocesos obj)
        {
            return DACInserta.ChatBotInsertarSubProceso(obj);
        }

        public int ChatBotInsertarPreguntas(chatbot_ref_preguntas obj)
        {
            return DACInserta.ChatBotInsertarPreguntas(obj);
        }

        public int ChatBotInsertarRespuestas(chatbot_ref_respuestas obj)
        {
            return DACInserta.ChatBotInsertarRespuestas(obj);
        }

        public int ChatBotInsertarRespuestasArchivos(chatbot_ref_respuestas_archivos obj)
        {
            return DACInserta.ChatBotInsertarRespuestasArchivos(obj);
        }

        public List<Management_chatbot_ref_respuestas_archivosResult> ChatBotRespuestasArchivos(int? idRespuesta)
        {
            return DACConsulta.ChatBotRespuestasArchivos(idRespuesta);
        }

        public chatbot_ref_respuestas_archivos TraerArchivoChatBot(int? idArchivo)
        {
            return DACConsulta.TraerArchivoChatBot(idArchivo);
        }

        #endregion CHATBOT

        #region INSERCION JSONS

        public int GuardarCargueJsonConsultas(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_consulta> lista)
        {
            return DACInserta.GuardarCargueJsonConsultas(lote, lista);
        }

        public int GuardarCargueJsonHospitalizacion(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_hospitalizacion> lista)
        {
            return DACInserta.GuardarCargueJsonHospitalizacion(lote, lista);
        }

        public int GuardarCargueJsonMedicamentos(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_medicamentos> lista)
        {
            return DACInserta.GuardarCargueJsonMedicamentos(lote, lista);
        }

        public int GuardarCargueJsonotrosServicios(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_otros_servicios> lista)
        {
            return DACInserta.GuardarCargueJsonotrosServicios(lote, lista);
        }

        public int GuardarCargueJsonProcedimientos(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_procedimientos> lista)
        {
            return DACInserta.GuardarCargueJsonProcedimientos(lote, lista);
        }

        public int GuardarCargueJsonRecienNacido(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_reciennacido> lista)
        {
            return DACInserta.GuardarCargueJsonRecienNacido(lote, lista);
        }

        public int GuardarCargueJsonTransaccion(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_transaccion> lista)
        {
            return DACInserta.GuardarCargueJsonTransaccion(lote, lista);
        }

        public int GuardarCargueJsonUrgencias(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_urgencias> lista)
        {
            return DACInserta.GuardarCargueJsonUrgencias(lote, lista);
        }

        public int GuardarCargueJsonUsuarios(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_usuarios> lista)
        {
            return DACInserta.GuardarCargueJsonUsuarios(lote, lista);
        }

        public int FisRipsInsercionLote(fis_rips_cargue_LoteConsultas lote)
        {
            return DACInserta.FisRipsInsercionLote(lote);
        }

        public int FisRipsInsercionTransaccion(fis_rips_cargue_transaccion transa)
        {
            return DACInserta.FisRipsInsercionTransaccion(transa);
        }

        public int FisRipsInsercionUsuario(fis_rips_cargue_usuarios usuario)
        {
            return DACInserta.FisRipsInsercionUsuario(usuario);
        }

        public int FisRipsInsercionConsulta(fis_rips_cargue_consulta consulta)
        {
            return DACInserta.FisRipsInsercionConsulta(consulta);
        }

        public List<base_beneficiarios_analitica> TraerBeneficiarioDocumentoActivo(string documento)
        {
            return DACConsulta.TraerBeneficiarioDocumentoActivo(documento);
        }
        public int FisRipsInsercionHospitalizacion(fis_rips_cargue_hospitalizacion hospi)
        {
            return DACInserta.FisRipsInsercionHospitalizacion(hospi);
        }

        public int FisRipsInsercionProcedimientos(fis_rips_cargue_procedimientos proce)
        {
            return DACInserta.FisRipsInsercionProcedimientos(proce);
        }

        public int FisRipsInsercionUrgencias(fis_rips_cargue_urgencias urge)
        {
            return DACInserta.FisRipsInsercionUrgencias(urge);
        }

        public int FisRipsInsercionRecienNacido(fis_rips_cargue_reciennacido recien)
        {
            return DACInserta.FisRipsInsercionRecienNacido(recien);
        }

        public int FisRipsInsercionMedicamento(fis_rips_cargue_medicamentos medi)
        {
            return DACInserta.FisRipsInsercionMedicamento(medi);
        }

        public int FisRipsInsercionOtroServicio(fis_rips_cargue_otros_servicios otro)
        {
            return DACInserta.FisRipsInsercionOtroServicio(otro);
        }

        public int EliminarDetallesIdFactura(int? idFactura)
        {
            return DACElimina.EliminarDetallesIdFactura(idFactura);
        }

        public management_fis_traerNumfactura_idResult TraerNumFactura_idAf(int? idDetalle)
        {
            return DACConsulta.TraerNumFactura_idAf(idDetalle);
        }

        #endregion INSERCION JSONS

        public management_traerRegionalFacturaResult TraerRegionalFacturas(int? idCargue)
        {
            return DACConsulta.TraerRegionalFacturas(idCargue);
        }

        #region VIGILANCIA COHORTES
        public int InsertarGRCP(cargue_vigilanciaCohortes_lote obj)
        {
            return DACInserta.InsertarGRCP(obj);
        }

        public void InsertarDatosGrpc(List<cargue_vigilanciaCohortesRegistros_normal> dtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarDatosGrpc(dtll, ref MsgRes);
        }

        public void InsertarDatosGrpcEpoc(List<cargue_vigilanciaCohortesRegistros_EPOC> dtll, ref MessageResponseOBJ MsgRes)
        {
            DACInserta.InsertarDatosGrpcEpoc(dtll, ref MsgRes);
        }

        public List<management_VigilanciaCohortes_TableroInicialResult> ListadoVigilanciaCohortesInicial(int? mes, int? año,
            string regional, string localidad, string municipio, int? tipo)
        {
            return DACConsulta.ListadoVigilanciaCohortesInicial(mes, año, regional, localidad, municipio, tipo);
        }

        public List<management_VigilanciaCohortes_TableroGestionadosResult> ListadoVigilanciaCohortesGestionados(int? mes, int? año,
          string regional, string localidad, string municipio, int? tipo)
        {
            return DACConsulta.ListadoVigilanciaCohortesGestionados(mes, año, regional, localidad, municipio, tipo);
        }

        public List<management_BuscarLocalidad_descripcionRegionalResult> LocalidadesXRegional(string regional)
        {
            return DACConsulta.LocalidadesXRegional(regional);
        }

        public List<management_BuscarCiudad_descripcionLocalidadResult> CiudadesXLocalidad(string localidad)
        {
            return DACConsulta.CiudadesXLocalidad(localidad);
        }

        public int InsertarGestionVigiCohorte(cargue_vigilanciaCohortes_registros_gestion obj)
        {
            return DACInserta.InsertarGestionVigiCohorte(obj);
        }

        public management_VigilanciaCohortes_gestionesResult TraerGestionVigilanciaCohortes(int? idLote, int? idRegistro)
        {
            return DACConsulta.TraerGestionVigilanciaCohortes(idLote, idRegistro);
        }

        public management_VigilanciaCohortes_detalleParaGestionarResult DetallesVigilanciaCohortes(int? idLote, int? idRegistro)
        {
            return DACConsulta.DetallesVigilanciaCohortes(idLote, idRegistro);
        }

        public List<management_vigilancoaCohortes_busquedaLocalidadesResult> GRPCBusquedaLocalidades(string regional)
        {
            return DACConsulta.GRPCBusquedaLocalidades(regional);
        }

        public List<management_vigilancoaCohortes_busquedaCiudadesResult> GRPCBusquedaCiudades(string localidad)
        {
            return DACConsulta.GRPCBusquedaCiudades(localidad);
        }


        #endregion VIGILANCIA COHORTES


        #region ADICIONALES
        public List<management_facturas_creacionreporteAprobacionResult> listadoFacturasReporteAprobacion()
        {
            return DACConsulta.listadoFacturasReporteAprobacion();
        }

        #endregion ADICIONALES
    }
}


