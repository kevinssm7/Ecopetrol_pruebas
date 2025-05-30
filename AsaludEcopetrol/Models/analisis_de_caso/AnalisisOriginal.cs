using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;

namespace AsaludEcopetrol.Models.analisis_de_caso
{
    public class AnalisisOriginal
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


        #region PROPIEDADES ALERTAS


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "TIPO DOCUMENTO")]
        public String tipo_documento { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "NO. IDENTIFICACION")]
        public String numero_identificacion { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "NOMBRES Y APELLIDOS DEL PACIENTE")]
        public String nombres_apellidos_pacientes { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "EDAD")]
        public String edad { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "SEXO")]
        public String sexo { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "NOMBRE DEL MEGA")]
        public String nombre_del_mega { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE REVISION")]
        public DateTime? fecha_revision { get; set; }

        public DateTime? fecha_revisionOK { get; set; }


        public int id_analisis_caso_alertas { get; set; }

      


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "NOMBRE DE ALERTA")]
        public String nombre_alerta { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "DESCRIPCION DE LA ALERTA")]
        public String descripcion_alerta { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE REGISTRO")]
        public DateTime? fecha_registro { get; set; }
        public DateTime? fecha_registroOK { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO DE INGRESO")]
        public String cie10_ingreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO DE EGRESO")]
        public String cie10_egreso { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "RESUMEN CASO")]
        public String resumen_caso { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "ANALISIS DE CASO")]
        public String analisis_caso { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "FUENTE DE LA INFORMACION")]
        public String fuente_informacion { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "EVENTUALES CAUSAS DEL EVENTO")]
        public String eventuales_causas_evento { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "FALLOS DE CALIDAD")]
        public String fallos_calidad { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "ALERTA EVITABLE (SI / NO)")]
        public String alerta_evitable { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "CONCLUSIONES")]
        public String conclusiones { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "RECOMENDACIONES")]
        public String recomendaciones { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PLAN MEJORA")]
        public String plan_mejora { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO DE EVENTO")]
        public String tipo_evento_alerta { get; set; }



        #endregion


        public int opc { get; set; }

        public int id_analisis_caso_original { get; set; }

        public int id_concurrencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE ANALISIS")]
        public DateTime? fecha_analisis { get; set; }

        public DateTime? fecha_analisisOK { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "SOLICITUD")]
        public String solicitud { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public int id_regional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public Int32 ciudad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO DE EVENTO")]
        public int tipo_evento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA INICIO EVENTO")]
        public DateTime? fecha_inicio_evento { get; set; }
        public DateTime? fecha_inicio_eventoOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA FIN EVENTO")]
        public DateTime? fecha_fin_evento { get; set; }
        public DateTime? fecha_fin_eventoOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE IPS")]
        public int id_ips { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PRESTADORES INTERVINIENTES EN LA ATENCION")]
        public String prestadores_intervinientes { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "OBJETO Y ALCANCE DE LA ACTIVIDAD")]
        public String objeto_alcance_actividad { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "MARCO LEGAL")]
        public String marco_legal { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO 1")]
        public String cie101 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO 2")]
        public String cie102 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO 3")]
        public String cie103 { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "RESUMEN SECUENCIAL DEL CASO")]
        public String resumen_secuencial_caso { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "ANÁLISIS CLÍNICO DEL CASO")]
        public String analisis_clinico_caso { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "APLICACIÓN DE LA METODOLOGÍA DE LAS UNIDADESDE ANALISIS")]
        public String aplicacion_metodologia { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "EVENTUALES CAUSAS DEL EVENTO POR PRESTADOR INTERVINIENTE")]
        public String eventuales_causas { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "EVENTUALES FALLAS DE CALIDAD")]
        public String eventuales_fallas_calidad { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "EVENTUALES FALLAS DE CONTROL DEL PROCESO")]
        public String eventuales_fallas_control { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "FUENTES DE INFORMACIÓN")]
        public String fuentes_info { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "CONCLUSIONES DEL ANALISIS DEL CASO")]
        public String conclucion_analisis { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "EVENTOS ADVERSOS PREVENIBLE")]
        public String prevenible_atribuible { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,500}$", ErrorMessage = "Maximo 500 caracteres")]
        [Display(Name = "COSTO DE NO CALIDAD DE LA ATENCION")]
        public String costo_no_calidad { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PATOLOGIA TRAZADORAS O CENTINELAS EVITABLES")]
        public String patologias_eventos_centinelas { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "HALLAZGOS EN CUMPLIMIENTO LEGAL")]
        public String hallazgos_legal { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "CUMPLIMIENTOS INDICADORES")]
        public String cumplimientos_indicadores { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "EVALUACIÓN DE INPACTOS")]
        public String evaluacion_impacto { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PERTINENCIAS DE LAS ACCIONES IMPLEMENTADAS")]
        public String pertinencia_acciones { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "EFICACIA DE LAS ACCIONES DE MEJORA")]
        public String eficacia_acciones { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "RECOMENDACIONES DE MEJORA")]
        public String recomendaciones_mejora { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "COMPROMISOS Y ACCIONES DE MEJORA")]
        public String compromiso_mejora { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,500}$", ErrorMessage = "Maximo 500 caracteres")]
        [Display(Name = "FIRMAS")]
        public String firmas { get; set; }


        public String usuario_digita { get; set; }


        public DateTime fecha_digita { get; set; }

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

        private List<vw_analisis_caso_alertas> _ListVWAnalisisCasoAlertas;
        public List<vw_analisis_caso_alertas> ListVWAnalisisCasoAlertas
        {
            get
            {
                if (_ListVWAnalisisCasoAlertas == null)
                {
                    return _ListVWAnalisisCasoAlertas = new List<vw_analisis_caso_alertas>();
                }
                else
                {
                    return _ListVWAnalisisCasoAlertas;
                }

            }

            set
            {
                _ListVWAnalisisCasoAlertas = value;
            }
        }


        #endregion

        #region METODOS




        /// <summary>
        /// Metodo para insertar en base de datos  analisis de caso original
        /// </summary>
        /// <param name="AnalisiscOriginal"></param>
        public int InsertarAnalisisCasoOriginal(analisis_caso_original AnalisiscOriginal)
        {
            return BusClass.InsertarAnalisisCasos(AnalisiscOriginal, ref MsgRes);
        }

        public int InsertarAnalisisCasoAlerta(analisis_caso_alertas AnalisiscAlertas)
        {
            return BusClass.InsertarAnalisisCasosAlerta(AnalisiscAlertas, ref MsgRes);
        }

        public void ActualizarAnalisisCasoOriginal(analisis_caso_original AnalisiscOriginal)
        {
            BusClass.ActualizarAnalisisCasos(AnalisiscOriginal, ref MsgRes);
        }

        public void ActualizarAnalisisAlertas(analisis_caso_alertas AnalisisA)
        {
            BusClass.ActualizarAnalisisAlertas(AnalisisA, ref MsgRes);
        }

        /// <summary>
        /// Metodo para consultar analisis de caso original
        /// </summary>
        /// <param name="idconcurrencia"></param>
        /// <returns></returns>
        public List<analisis_caso_original> ConsultaAnalisisCasoOriginal(Int32? idconcurrencia, Int32? idanalisiscaso)
        {
            return BusClass.ConsultaEvolucionAnalisisCasoOriginal(idconcurrencia, idanalisiscaso, ref MsgRes);
        }

        public List<ManagmentReporteAnalisisCasoOrgResult> ReporteAnalisisCasoOriginal(Int32 idConcurrencia, Int32 idAnalisis)
        {
            return BusClass.ReporteAnalisisCasoOriginal(idConcurrencia, idAnalisis, ref MsgRes);
        }

        public AnalisisOriginal SetearvaloresAnalisis(analisis_caso_original ObjAnalisis)
        {
            AnalisisOriginal Analisis = new AnalisisOriginal();
            Analisis.id_analisis_caso_original = ObjAnalisis.id_analisis_caso_original;
            Analisis.id_concurrencia = ObjAnalisis.id_concurrencia.Value;
            Analisis.fecha_analisis = ObjAnalisis.fecha_analisis;
            Analisis.fecha_analisisOK = ObjAnalisis.fecha_analisis;
            Analisis.solicitud = ObjAnalisis.solicitud;
            Analisis.id_regional = ObjAnalisis.id_regional.Value;
            Analisis.tipo_evento = ObjAnalisis.tipo_evento.Value;
            Analisis.fecha_inicio_evento = ObjAnalisis.fecha_inicio_evento;
            Analisis.fecha_inicio_eventoOK = ObjAnalisis.fecha_inicio_evento;
            Analisis.fecha_fin_evento = ObjAnalisis.fecha_fin_evento;
            Analisis.fecha_fin_eventoOK = ObjAnalisis.fecha_fin_evento;
            Analisis.id_ips = ObjAnalisis.id_ips.Value;
            Analisis.prestadores_intervinientes = ObjAnalisis.prestadores_intervinientes;
            Analisis.objeto_alcance_actividad = ObjAnalisis.objeto_alcance_actividad;
            Analisis.marco_legal = ObjAnalisis.marco_legal;
            Analisis.cie101 = ObjAnalisis.cie101;
            Analisis.cie102 = ObjAnalisis.cie102;
            Analisis.cie103 = ObjAnalisis.cie103;
            Analisis.resumen_secuencial_caso = ObjAnalisis.resumen_secuencial_caso;
            Analisis.analisis_clinico_caso = ObjAnalisis.analisis_clinico_caso;
            Analisis.aplicacion_metodologia = ObjAnalisis.aplicacion_metodologia;
            Analisis.eventuales_causas = ObjAnalisis.eventuales_causas;
            Analisis.eventuales_fallas_control = ObjAnalisis.eventuales_fallas_control;
            Analisis.eventuales_fallas_calidad = ObjAnalisis.eventuales_fallas_calidad;
            Analisis.fuentes_info = ObjAnalisis.fuentes_info;
            Analisis.conclucion_analisis = ObjAnalisis.conclucion_analisis;
            Analisis.prevenible_atribuible = ObjAnalisis.prevenible_atribuible;
            Analisis.costo_no_calidad = ObjAnalisis.costo_no_calidad;
            Analisis.hallazgos_legal = ObjAnalisis.hallazgos_legal;
            Analisis.cumplimientos_indicadores = ObjAnalisis.cumplimientos_indicadores;
            Analisis.patologias_eventos_centinelas = ObjAnalisis.patologias_eventos_centinelas;
            Analisis.pertinencia_acciones = ObjAnalisis.pertinencia_acciones;
            Analisis.eficacia_acciones = ObjAnalisis.eficacia_acciones;
            Analisis.recomendaciones_mejora = ObjAnalisis.recomendaciones_mejora;
            Analisis.compromiso_mejora = ObjAnalisis.compromiso_mejora;
            Analisis.evaluacion_impacto = ObjAnalisis.evaluacion_impacto;
            Analisis.usuario_digita = SesionVar.UserName;
            Analisis.fecha_digita = DateTime.Now;
            return Analisis;
        }

        public AnalisisOriginal SetearvaloresAnalisisAlertas(analisis_caso_alertas obj)
        {
            AnalisisOriginal AnalisisAlerta = new AnalisisOriginal();
            AnalisisAlerta.id_concurrencia = obj.id_concurrencia.Value;
            AnalisisAlerta.id_analisis_caso_alertas = obj.id_analisis_caso_alertas;
            AnalisisAlerta.id_regional = Convert.ToInt32(obj.id_regional);
            AnalisisAlerta.ciudad  = Convert.ToInt32(obj.id_ciudad);
            AnalisisAlerta.id_ips = obj.id_ips.Value;
            AnalisisAlerta.tipo_documento = obj.tipo_documento;
            AnalisisAlerta.numero_identificacion = obj.numero_identificacion;
            AnalisisAlerta.nombres_apellidos_pacientes = obj.nombres_completos;
            AnalisisAlerta.edad = obj.edad;
            AnalisisAlerta.sexo = obj.sexo;
            AnalisisAlerta.nombre_del_mega = obj.nombre_mega;
            AnalisisAlerta.fecha_revision = obj.fecha_revision;
            AnalisisAlerta.tipo_evento_alerta = obj.tipo_evento;
            AnalisisAlerta.nombre_alerta = obj.nombre_alerta;
            AnalisisAlerta.descripcion_alerta = obj.descripcion_alerta;
            AnalisisAlerta.fecha_registro = obj.fecha_registro;
            AnalisisAlerta.cie10_ingreso = obj.diagnostico_ingreso;
            AnalisisAlerta.cie10_egreso = obj.diagnostico_egreso;
            AnalisisAlerta.resumen_caso = obj.resumen_caso;
            AnalisisAlerta.analisis_caso = obj.analisis_caso;
            AnalisisAlerta.fuente_informacion = obj.fuente_informacion;
            AnalisisAlerta.eventuales_causas_evento = obj.eventual_causa_evento;
            AnalisisAlerta.fallos_calidad = obj.fallos_calidad;
            AnalisisAlerta.alerta_evitable = obj.alerta_evitable;
            AnalisisAlerta.conclusiones = obj.conclusiones;
            AnalisisAlerta.recomendaciones = obj.recomendaciones;
            AnalisisAlerta.plan_mejora = obj.plan_mejora;

            return AnalisisAlerta;

        }


        public void ConsultaIdConcurrenciaAlertas(Int32 id_concurrencia)
        {
            vw_analisis_caso_alertas ObjAnalisisCasoA = new vw_analisis_caso_alertas();
            ObjAnalisisCasoA.id_concurrencia = id_concurrencia;
            ListVWAnalisisCasoAlertas = BusClass.GetIdAnalisisAlertas(id_concurrencia, ref MsgRes);

            foreach (var item in ListVWAnalisisCasoAlertas)
            {
                id_concurrencia = item.id_concurrencia;
                id_regional = Convert.ToInt32(item.id_ref_regional);
                ciudad = Convert.ToInt32(item.id_ref_ciudades);
                id_ips = Convert.ToInt32(item.id_ips);
                tipo_documento = item.afi_tipo_doc;
                numero_identificacion = item.id_afi;
                nombres_apellidos_pacientes = item.afi_nom;
                edad = Convert.ToString(item.afi_edad);
                sexo = item.sexo;
                nombre_del_mega = item.med_ser_trata;
                tipo_evento_alerta = item.tipo;
                nombre_alerta = item.descripcion;
                descripcion_alerta = item.descripcion_general;
                fecha_registro = item.Fecha_Ultima_Evolucion;
                cie10_ingreso = item.dx1;
                cie10_egreso = item.DxprincipalEgreso;
                resumen_caso = item.Resumen_caso;

            }
        }


        public List<analisis_caso_alertas> ConsultaAnalisisCasoAlertas(Int32? idconcurrencia, Int32? IdAnalisis)
        {
            return BusClass.ConsultaAnalisisCasoAlertas(idconcurrencia, IdAnalisis, ref MsgRes);
        }


        #endregion

    }
}