using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.analisis_de_caso
{
    public class AnalisisSaludPublica
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


        public int id_analisis_caso_salud_publica { get; set; }

        public int id_concurrencia { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE ANALISIS")]
        public DateTime? fecha_del_analisis { get; set; }
        public DateTime? fecha_del_analisisok { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EDAD AL MOMENTO DEL ANALISIS")]
        public int edad_momento_analisis { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO DE EVENTO")]
        public int tipo_evento { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA OCURRENCIA DEL EVENTO")]
        public DateTime? fecha_ocurrencia_evento { get; set; }
        public DateTime? fecha_ocurrencia_eventoOK { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,50}$", ErrorMessage = "Maximo 50 caracteres")]
        [Display(Name = "FUENTE DEL REPORTE")]
        public String fuente_del_reporte { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100 caracteres")]
        [Display(Name = "NOMBRE DEL REPORTANTE")]
        public String nombre_reportante { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public int id_regional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public Int32 ciudad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PRESTADOR DONDE OCURRE EL EVENTO")]
        public int id_ips { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,500}$", ErrorMessage = "Maximo 500 caracteres")]
        [Display(Name = "ENTIDADES RESPONSABLES DEL PACIENTE")]
        public String entidades_responsables { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "OBJETIVO DE LA AUDITORIA")]
        public String objetivo_auditoria { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "DESCRIPCION DEL EVENTO")]
        public String descripcion_evento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AMBITO DONDE SUCEDIO EL EVENTO")]
        public int id_ref_ambito_evento { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "RESUMEN CLINICO DEL EVENTO")]
        public String Resumen_clinico_evento { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "RESOLUTIVIDAD DE PRIMER NIVEL DE ATENCION")]
        public String concepto_primer_nivel { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "APLICACION DE GUIAS TERAPEUTICAS SEGUN PATOLOGIAS")]
        public String guias_terapeuticas { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PERIOCIDAD DE LOS CONTROLES MEDICOS")]
        public String periocidad_controles { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PRUEBAS DIAGNOSTICAS DE MONITOREO")]
        public String pruebas_diagnosticas { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PLAN TERAPÉUTICO Y CUMPLIMIENTO DEL TRATAMIENTO")]
        public String plan_terapeutico { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "EVENTUALES CAUSAS DE LA MUERTE RELACIONADAS CON LA ATENCION A NIVEL AMBULATORIO Y HOSPITALARIO")]
        public String eventuales_causas_muerte { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "CONCEPTO AUDITORIA")]
        public String concepto_auditoria { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PROPUESTA DE ACCIONES DE MEJORA ")]
        public String propuesta_acciones { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "RELACION DE ANEXOS")]
        public String relacion_anexos { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,500}$", ErrorMessage = "Maximo 500 caracteres")]
        [Display(Name = "FIRMAS DEL EVALUADOR Y DEL EVALUADO")]
        public String firmas { get; set; }

        public String usuario_digita { get; set; }

        public DateTime fecha_digita { get; set; }



        private List<Ref_analaisis_caso_ambito> _lstAmbito;

        public List<Ref_analaisis_caso_ambito> LstAmbito
        {
            get
            {
                if (_lstAmbito == null)
                {
                    return _lstAmbito = BusClass.Getambito();
                }
                else
                {
                    return _lstAmbito;
                }
            }

            set
            {
                _lstAmbito = value;
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


        #endregion

        #region METODOS

        public int InsertarAnalisisCasoSaludPublica(analisis_caso_salud_publica analisis)
        {
            return BusClass.InsertarAnalisisCasosSaludP(analisis, ref MsgRes);
        }

        public List<analisis_caso_salud_publica> ConsultaEvolucionAnalisisSaludP(Int32 idconcurrencia, Int32? IdAnalisis)
        {
            return BusClass.ConsultaEvolucionAnalisisSaludP(idconcurrencia, IdAnalisis, ref MsgRes);
        }

        public List<ManagmentReporteAnalisisCasoSPResult> ReporteAnalisisCasoSP(Int32 idconcurrencia, Int32 idanalisis)
        {
            return BusClass.ReporteAnalisisCasoSP(idconcurrencia, idanalisis);
        }

        public AnalisisSaludPublica SetearvaloresAnalisis(analisis_caso_salud_publica obj)
        {
            AnalisisSaludPublica Analisis = new AnalisisSaludPublica();
            Analisis.id_concurrencia = obj.id_concurrencia.Value;
            Analisis.id_analisis_caso_salud_publica = obj.id_analisis_caso_salud_publica;
            Analisis.fecha_del_analisisok = obj.fecha_del_analisis;
            Analisis.fecha_del_analisis = obj.fecha_del_analisis;
            Analisis.edad_momento_analisis = obj.edad_momento_analisis.Value;
            Analisis.tipo_evento = obj.tipo_evento.Value;
            Analisis.fecha_ocurrencia_eventoOK = obj.fecha_ocurrencia_evento;
            Analisis.fecha_ocurrencia_evento = obj.fecha_ocurrencia_evento;
            Analisis.fuente_del_reporte = obj.fuente_del_reporte;
            Analisis.nombre_reportante = obj.nombre_reportante;
            Analisis.id_ips = obj.id_ips.Value;
            Analisis.entidades_responsables = obj.entidades_responsables;
            Analisis.objetivo_auditoria = obj.objetivo_auditoria;
            Analisis.descripcion_evento = obj.descripcion_evento;
            Analisis.id_ref_ambito_evento = obj.id_ref_ambito_evento.Value;
            Analisis.Resumen_clinico_evento = obj.Resumen_clinico_evento;
            Analisis.concepto_primer_nivel = obj.concepto_primer_nivel;
            Analisis.guias_terapeuticas = obj.guias_terapeuticas;
            Analisis.periocidad_controles = obj.periocidad_controles;
            Analisis.pruebas_diagnosticas = obj.pruebas_diagnosticas;
            Analisis.plan_terapeutico = obj.plan_terapeutico;
            Analisis.eventuales_causas_muerte = obj.eventuales_causas_muerte;
            Analisis.concepto_auditoria = obj.concepto_auditoria;
            Analisis.propuesta_acciones = obj.propuesta_acciones;
            Analisis.relacion_anexos = obj.relacion_anexos;
            Analisis.firmas = obj.firmas;
            return Analisis;

        }

        public void ActualizarAnalisisCasoSaludPublica(analisis_caso_salud_publica analisis)
        {
             BusClass.ActualizarAnalisisCasoSaludPublica(analisis, ref MsgRes);
        }

        #endregion
    }
}