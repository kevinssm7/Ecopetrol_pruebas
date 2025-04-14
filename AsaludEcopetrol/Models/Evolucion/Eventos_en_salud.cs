using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class Eventos_en_salud
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

        private BussinesManager.SessionState _SesionVar;
        public BussinesManager.SessionState SesionVar
        {
            get
            {
                if (_SesionVar == null)
                {
                    _SesionVar = new BussinesManager.SessionState();
                }
                return _SesionVar;
            }
            set { _SesionVar = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();


        public int opc { get; set; }

        public int id_ecop_concurrencia_eventos_en_asalud { get; set; }

        public int id_concurrencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE ANALISIS")]
        public DateTime? fecha_analisis { get; set; }
        public DateTime? fecha_analisisok { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE ANALISIS")]
        public DateTime? fecha_cumplimiento { get; set; }
        public DateTime? fecha_cumplimientoOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EXAMENES DE HALLAZGOS")]
        public String examenes_hallazgos { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO ANTES DEL EVENTO")]
        public String cie10_antes_del_evento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO RESULTADO DEL EVENTO")]
        public String cie10_resultado_evento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CONDUCTA INMEDIATA A SEGUIR")]
        public String conducta_inmediata { get; set; }

        [Display(Name = "CATEGORIA EVENTOS ADVERSOS")]
        public Int32 id_categoria { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EVENTO ADVERSO")]
        public int id_ref_evento_adv { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ACCIONES INSEGURAS")]
        public String acciones_inseguras { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DECISIÓN")]
        public int id_decisiones { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRA DECISIÓN")]
        public String otro_dicision { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "HABILIDAD")]
        public int id_habilidad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRA HABILIDAD")]
        public String otro_habilidad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PERCEPCIÓN")]
        public int id_percepcion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRA PERCEPCIÓN")]
        public String otro_percepcion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "RUTINA")]
        public int id_rutinaria { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRA RUTINA")]
        public String otro_rutinaria { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EXCEPCIONALES")]
        public int id_excepcionales { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTROS GENERAL ACCIONES INSEGURAS")]
        public String otro_general_factores_contributivos { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTROS GENERAL ACCIONES INSEGURAS")]
        public String otro_general_accion_inseguras { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRO EXCEPCIONAL")]
        public String otro_excepcionales { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FACTORES CONTRIBUTIVOS")]
        public String factores_contributivos { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PACIENTE")]
        public int id_paciente { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRO PACIENTE")]
        public String otro_paciente { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TAREA Y TECNOLOGIA")]
        public int id_tarea_tecnologia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRA TAREA Y TECNOLOGIA")]
        public String otro_tarea_tecnologia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EQUIPO DE TRABAJO")]
        public int id_equipo_trabajo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRO EQUIPO DE TRABAJO")]
        public String otro_equipo_trabajo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "INDIVIDUO")]
        public int id_individuo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRO INDIVIDUO")]
        public String otro_individuo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AMBIENTE DE TRABAJO")]
        public int id_ambiente_trabajo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRO AMBIENTE DE TRABAJO")]
        public String otro_ambiente_trabajo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ORGANIZACION Y GERENCIA")]
        public int id_organizacion_gerencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRA ORGANIZACION Y GERENCIA")]
        public String otro_organizacion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CONTEXTO")]
        public int id_contexto { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRO CONTEXTO")]
        public String otro_contexto { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FALLA ACTIVA FINAL")]
        public String falla_activa_final { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "SEVERIDAD DEL DESENLACE")]
        public int id_severidad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PROBABILIDAD DE REPETICIÓN")]
        public int probabilidad_repeticion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO DE EVENTO")]
        public int id_tipo_evento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PREVENIBLE")]
        public String prevenible { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CONCLUSIONES Y RECOMENDACIONES")]
        public String concluciones { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "SEGUIMIENTO AUDITORIA")]
        public String seguimiento_auditoria { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EQUIPO DE ANALISIS")]
        public String equipo_analisis { get; set; }

        
        public String usuario_digita { get; set; }

        public DateTime fecha_digita { get; set; }


        private List<Ref_eventos_decision> _lstDecision;
        public List<Ref_eventos_decision> LstDecision
        {
            get
            {
                if (_lstDecision == null)
                {
                    return _lstDecision = BusClass.GetEventoDecision();
                }
                else
                {
                    return _lstDecision;
                }

            }

            set
            {
                _lstDecision = value;
            }
        }


        private List<Ref_eventos_habilidad> _LstHabilidad;
        public List<Ref_eventos_habilidad> LstHabilidad
        {
            get
            {
                if (_LstHabilidad == null)
                {
                    return _LstHabilidad = BusClass.GetEventosHabilidad();
                }
                else
                {
                    return _LstHabilidad;
                }

            }

            set
            {
                _LstHabilidad = value;
            }
        }

        private List<Ref_eventos_percepcion> _LstPercepcion;
        public List<Ref_eventos_percepcion> LstPercepcion
        {
            get
            {
                if (_LstPercepcion == null)
                {
                    return _LstPercepcion = BusClass.GetEventosPercepcion();
                }
                else
                {
                    return _LstPercepcion;
                }

            }

            set
            {
                _LstPercepcion = value;
            }
        }


        private List<Ref_eventos_rutinaria> _LstRutinaria;
        public List<Ref_eventos_rutinaria> LstRutinaria
        {
            get
            {
                if (_LstRutinaria == null)
                {
                    return _LstRutinaria = BusClass.GetEventosRutinaria();
                }
                else
                {
                    return _LstRutinaria;
                }

            }

            set
            {
                _LstRutinaria = value;
            }
        }

        private List<Ref_eventos_excepcionales> _LstExcep;
        public List<Ref_eventos_excepcionales> LstExcep
        {
            get
            {
                if (_LstExcep == null)
                {
                    return _LstExcep = BusClass.GetEventosexcepcionales();
                }
                else
                {
                    return _LstExcep;
                }

            }

            set
            {
                _LstExcep = value;
            }
        }

        private List<Ref_eventos_paciente> _LstPaciente;
        public List<Ref_eventos_paciente> LstPaciente
        {
            get
            {
                if (_LstPaciente == null)
                {
                    return _LstPaciente = BusClass.GetEventosPaciente();
                }
                else
                {
                    return _LstPaciente;
                }

            }

            set
            {
                _LstPaciente = value;
            }
        }

        private List<Ref_eventos_tarea_tec> _LstTarea;
        public List<Ref_eventos_tarea_tec> LstTarea
        {
            get
            {
                if (_LstTarea == null)
                {
                    return _LstTarea = BusClass.GetEventostarea();
                }
                else
                {
                    return _LstTarea;
                }

            }

            set
            {
                _LstTarea = value;
            }
        }

        private List<Ref_eventos_equipo> _LstEquipo;
        public List<Ref_eventos_equipo> LstEquipo
        {
            get
            {
                if (_LstEquipo == null)
                {
                    return _LstEquipo = BusClass.GetEventosEquipoT();
                }
                else
                {
                    return _LstEquipo;
                }

            }

            set
            {
                _LstEquipo = value;
            }
        }

        private List<Ref_eventos_individuo> _LstIndividuo;
        public List<Ref_eventos_individuo> LstIndividuo
        {
            get
            {
                if (_LstIndividuo == null)
                {
                    return _LstIndividuo = BusClass.GetEventosIndividuo();
                }
                else
                {
                    return _LstIndividuo;
                }

            }

            set
            {
                _LstIndividuo = value;
            }
        }

        private List<Ref_eventos_ambiente_tra> _LstAmbiente;
        public List<Ref_eventos_ambiente_tra> LstAmbiente
        {
            get
            {
                if (_LstAmbiente == null)
                {
                    return _LstAmbiente = BusClass.GetEventosambienteTrabajo();
                }
                else
                {
                    return _LstAmbiente;
                }

            }

            set
            {
                _LstAmbiente = value;
            }
        }

        private List<Ref_eventos_organizacion> _LstOrganizacion;
        public List<Ref_eventos_organizacion> LstOrganizacion
        {
            get
            {
                if (_LstOrganizacion == null)
                {
                    return _LstOrganizacion = BusClass.GetEventosOrganizacion();
                }
                else
                {
                    return _LstOrganizacion;
                }

            }

            set
            {
                _LstOrganizacion = value;
            }
        }

        private List<Ref_eventos_contexto> _LisEventosContexto;
        public List<Ref_eventos_contexto> LisEventosContexto
        {
            get
            {
                if (_LisEventosContexto == null)
                {
                    return _LisEventosContexto = BusClass.GetEventosContexto();
                }
                else
                {
                    return _LisEventosContexto;
                }

            }

            set
            {
                _LisEventosContexto = value;
            }
        }

        private List<Ref_eventos_severidad> _lstSeveridad;
        public List<Ref_eventos_severidad> LstSeveridad
        {
            get
            {
                if (_lstSeveridad == null)
                {
                    return _lstSeveridad = BusClass.GetEventosSeveridad();
                }
                else
                {
                    return _lstSeveridad;
                }

            }

            set
            {
                _lstSeveridad = value;
            }
        }

        private List<Ref_eventos_repeticion> _LstRepeticion;
        public List<Ref_eventos_repeticion> LstRepeticion
        {
            get
            {
                if (_LstRepeticion == null)
                {
                    return _LstRepeticion = BusClass.GetEventosProbavilidadR();
                }
                else
                {
                    return _LstRepeticion;
                }


            }

            set
            {
                _LstRepeticion = value;
            }
        }

        private List<Ref_eventos_tipo_evento> _LstTipoEvento;
        public List<Ref_eventos_tipo_evento> LstTipoEvento
        {
            get
            {
                if (_LstTipoEvento == null)
                {
                    return _LstTipoEvento = BusClass.GetEventosTipoEvento();
                }
                else
                {
                    return _LstTipoEvento;
                }

            }

            set
            {
                _LstTipoEvento = value;
            }
        }

        private List<ecop_concurrencia_eventos_en_salud_detalle> _LstEventosSaludDetalle;
        public List<ecop_concurrencia_eventos_en_salud_detalle> LstEventosSaludDetalle
        {
            get
            {
                if (_LstEventosSaludDetalle == null)
                {
                    _LstEventosSaludDetalle = new List<ecop_concurrencia_eventos_en_salud_detalle>();
                }

                return _LstEventosSaludDetalle;
            }
            set { _LstEventosSaludDetalle = value; }
        }




        #endregion

        #region METODOS

        public void InsertarAnalisisEventosSalud(ecop_concurrencia_eventos_en_asalud Analisis, List<ecop_concurrencia_eventos_en_salud_detalle> otrosiList)
        {
            BusClass.InsertarAnalisisCasosEventos(Analisis, otrosiList,  ref MsgRes);
        }

        public void InsertarAnalisisCasosEventosDet(ecop_concurrencia_eventos_en_asalud Analisis, List<ecop_concurrencia_eventos_en_salud_detalle> otrosiList)
        {
            BusClass.InsertarAnalisisCasosEventosDet(Analisis, otrosiList, ref MsgRes);
        }

        public Int32 InsertarAnalisisCasosEventosDetalle(ecop_concurrencia_eventos_en_salud_detalle OBJ)
        {
            return BusClass.InsertarAnalisisCasosEventosDetalle(OBJ, ref MsgRes);
        }

        public void ActualizarAnalisisEventosSalud(ecop_concurrencia_eventos_en_asalud Analisis)
        {
            BusClass.ActualizarAnalisisEventosSalud(Analisis, ref MsgRes);
        }

        public List<ecop_concurrencia_eventos_en_asalud> ConsultaAnalisisEventosenSalud(Int32 idconcurrencia, Int32? IdAnalisis)
        {
            return BusClass.ConsultaAnalisisEventosenSalud(idconcurrencia, IdAnalisis, ref MsgRes);
        }

        public List<ecop_concurrencia_eventos_en_salud_detalle> ConsultaAnalisisEventosenSaludDetalle(ecop_concurrencia_eventos_en_salud_detalle OBJ)
        {
            return BusClass.ConsultaAnalisisEventosenSaludDetalle(OBJ, ref MsgRes);
        }


        public List<ManagmentReporteAnalisisESResult> ReporteEventosSalud(Int32 IdConcurrencia, Int32 Idanalisis)
        {
            return BusClass.ReporteEventosSalud(IdConcurrencia, Idanalisis);
        }

        public Eventos_en_salud SetearvaloresAnalisis(ecop_concurrencia_eventos_en_asalud Obj)
        {

            Eventos_en_salud analisis = new Eventos_en_salud();
            analisis.id_ecop_concurrencia_eventos_en_asalud = Obj.id_ecop_concurrencia_eventos_en_asalud;
            analisis.id_concurrencia = Obj.id_concurrencia;
            analisis.fecha_analisis = Obj.fecha_analisis;
            analisis.fecha_analisisok = Obj.fecha_analisis;
            analisis.examenes_hallazgos = Obj.examenes_hallazgos;
            analisis.cie10_antes_del_evento = Obj.cie10_antes_del_evento;
            analisis.cie10_resultado_evento = Obj.cie10_resultado_evento;
            analisis.conducta_inmediata = Obj.conducta_inmediata;
            analisis.id_ref_evento_adv = Obj.id_ref_evento_adv.Value;
            analisis.acciones_inseguras = Obj.acciones_inseguras;
            analisis.id_decisiones = Obj.id_decisiones.Value;
            analisis.otro_general_accion_inseguras = Obj.otros_general_acciones_inseguras;
            analisis.otro_general_factores_contributivos = Obj.otros_general_factores_contibutivos;
            analisis.id_habilidad = Obj.id_habilidad.Value;
            analisis.id_percepcion = Obj.id_percepcion.Value;
            analisis.id_rutinaria = Obj.id_rutinaria.Value;
            analisis.id_excepcionales = Obj.id_excepcionales.Value;
            analisis.factores_contributivos = Obj.factores_contributivos;
            analisis.id_paciente = Obj.id_paciente.Value;
            analisis.id_tarea_tecnologia = Obj.id_tarea_tecnologia.Value;
            analisis.id_equipo_trabajo = Obj.id_equipo_trabajo.Value;
            analisis.id_individuo = Obj.id_individuo.Value;
            analisis.id_ambiente_trabajo = Obj.id_ambiente_trabajo.Value;
            analisis.id_organizacion_gerencia = Obj.id_organizacion_gerencia.Value;
            analisis.id_contexto = Obj.id_contexto.Value;
            analisis.falla_activa_final = Obj.falla_activa_final;
            analisis.probabilidad_repeticion = Obj.probabilidad_repeticion.Value;
            analisis.id_tipo_evento = Obj.id_tipo_evento.Value;
            analisis.prevenible = Obj.prevenible;
            analisis.concluciones = Obj.concluciones;
            analisis.seguimiento_auditoria = Obj.seguimiento_auditoria;
            analisis.equipo_analisis = Obj.equipo_analisis;
            analisis.id_severidad = Obj.id_severidad.Value;
            
            return analisis;
        }


        public void SetearvaloresAnalisisDetalle(int idconcu)
        {

            ecop_concurrencia_eventos_en_salud_detalle ObjanalisisDetalle = new ecop_concurrencia_eventos_en_salud_detalle();
            ObjanalisisDetalle.id_ecop_concurrencia_eventos_en_salud = idconcu;
            LstEventosSaludDetalle = BusClass.ConsultaAnalisisEventosenSaludDetalle(ObjanalisisDetalle, ref MsgRes);


        }


        public List<ecop_concurrencia_eventos_en_salud_detalle> GetAnalisisEventosenSaludDetalle(int idanalisis)
        {
            return BusClass.GetAnalisisEventosenSaludDetalle(idanalisis);
        }


        public void EliminarPlanAccion(Int32? id)
        {
            ecop_concurrencia_eventos_en_salud_detalle objplan = new ecop_concurrencia_eventos_en_salud_detalle();
            objplan.id_ecop_concurrencia_eventos_en_salud_detalle = Convert.ToInt32(id);
            BusClass.EliminarPlanAccion(objplan,ref MsgRes);
        }

        #endregion
    }
}