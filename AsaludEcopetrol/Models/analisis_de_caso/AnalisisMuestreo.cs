using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.analisis_de_caso
{
    public class AnalisisMuestreo
    {

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

        #region PROPIEDADES


        public int opc { get; set; }

        public int id_analisis_caso_muestreo { get; set; }

        public int id_concurrencia { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "NUMERO DE CASOS")]
        public String numero_casos { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE REVISION MM/DD/AAAA")]
        public DateTime? fecha_revision { get; set; }
        public DateTime? fecha_revisionOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL DE ORIGEN")]
        public int id_regional { get; set; }

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
        [Display(Name = "FECHA ULTIMO CONTROL")]
        public DateTime? fecha_ultimo_control { get; set; }
        public DateTime? fecha_ultimo_controlOK { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "COHORTE A LA QUE PERTENECE")]
        public String cohorte_paciente { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "ANTECEDENTES PERSONALES")]
        public String antecedentes_personales { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "DESCRIPCION DE ATENCIONES AMBULATORIAS (RESUMEN CLINICO Y SECUENCIAL DE ATENCIONES DEL ULTIMO AÑO)")]
        public String descripciones_atenciones_ambulatorias { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "NUMERO  DE CONTROLES")]
        public String numero_controles { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PLAN DE MANEJO DEFINIDO EN EL ÚLTIMO CONTROL")]
        public String plan_manejo_ultimo_control { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "CONCEPTO DE AUDITORIA AMBULATORIA")]
        public String concepto_auditoria_ambulatoria { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "CONCLUSION DEL CASO")]
        public String conclusion_caso_ambulatorio { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "ADHERENCIA DEL PACIENTE  AL PROGRAMA (SI/NO)")]
        public String adherencia_paciente { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "PLAN DE MEJORA A PPE ")]
        public String plan_de_mejora_ppe { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "RECOMENDACIONES AMBULATORIA")]
        public String recomendaciones_ambulatoria { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE IPS")]
        public Int32 id_ips { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "AUDITOR ASALUD")]
        public String auditor_asalud { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO DE INGRESO")]
        public String cie10_ingreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO DE EGRESO")]
        public String cie10_egreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA INGRESO")]
        public DateTime? fecha_ingreso { get; set; }
        public DateTime? fecha_ingresoOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA EGRESO")]
        public DateTime? fecha_egreso { get; set; }
        public DateTime? fecha_egresoOK { get; set; }
    
        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "DESCRIPCION DE  HOSPITALIZACION -(RESUMEN CLINICO Y SECUENCIAL DEL EVENTO)")]
        public String descripcion_hospitalizacion { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "CONCEPTO DE AUDITORIA")]
        public String concepto_auditoria { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "CONCLUSION DEL CASO")]
        public String conclusion_caso_hospitalario { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "HOSPITALIZACION EVITABLE (SI/NO)")]
        public String hospitalizacion_evitable { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "RECOMENDACIONES HOSPITALARIO")]
        public String recomendaciones_hospitalaraio { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "DEFINICION INTEGRAL  DEL CASO")]
        public String definicion_integral_caso { get; set; }


        private List<vw_analisis_caso_muestreo> _ListVWAnalisisCasoMuestreo;
        public List<vw_analisis_caso_muestreo> ListVWAnalisisCasoMuestreo
        {
            get
            {
                if (_ListVWAnalisisCasoMuestreo == null)
                {
                    return _ListVWAnalisisCasoMuestreo = new List<vw_analisis_caso_muestreo>();
                }
                else
                {
                    return _ListVWAnalisisCasoMuestreo;
                }

            }

            set
            {
                _ListVWAnalisisCasoMuestreo = value;
            }
        }

        private List<Ref_ips> _ListIPS;
        public List<Ref_ips> ListIPS
        {
            get
            {
                if (_ListIPS == null)
                {
                    _ListIPS = BusClass.GetPrstador();
                    _ListIPS = _ListIPS.OrderBy(X => X.Nombre).ToList();

                    return _ListIPS;
                }
                else
                {
                    return _ListIPS;
                }

            }

            set
            {
                _ListIPS = value;
            }
        }

        private List<vw_cie10> _ListCie10;
        public List<vw_cie10> ListCie10
        {
            get
            {
                if (_ListCie10 == null)
                {
                    _ListCie10 = BusClass.GetCie10Unido();
                    _ListCie10 = _ListCie10.OrderBy(X => X.des).ToList();

                    return _ListCie10;
                }
                else
                {
                    return _ListCie10;
                }

            }

            set
            {
                _ListCie10 = value;
            }
        }

        #endregion

        #region METODOS

        public int InsertarAnalisisMuestreo(analisis_caso_muestreo AnalisisMuestreo)
        {
            return BusClass.InsertarAnalisisMuestreo(AnalisisMuestreo, ref MsgRes);
        }

        public void ActualizarAnalisisMuestreo(analisis_caso_muestreo AnalisisMuestreo)
        {
            BusClass.ActualizarAnalisisMuestreo(AnalisisMuestreo, ref MsgRes);
        }

        public List<analisis_caso_muestreo> ConsultaAnalisisCasoMuestreo(Int32? idconcurrencia, Int32? IdAnalisis)
        {
            return BusClass.ConsultaAnalisisCasoMuestreo(idconcurrencia, IdAnalisis, ref MsgRes);
        }

        public AnalisisMuestreo SetearvaloresAnalisisMuestreo(analisis_caso_muestreo obj)
        {
            AnalisisMuestreo AnalisisMuestreo = new AnalisisMuestreo();
            AnalisisMuestreo.id_concurrencia = obj.id_concurrencia.Value;
            AnalisisMuestreo.id_analisis_caso_muestreo = obj.id_analisis_caso_muestreo;
            AnalisisMuestreo.id_regional = Convert.ToInt32(obj.regional_origen);
            AnalisisMuestreo.numero_casos = numero_casos;
            AnalisisMuestreo.fecha_revision = obj.fecha_revision;
            AnalisisMuestreo.id_ips = Convert.ToInt32(obj.id_ips);
            AnalisisMuestreo.tipo_documento = obj.tipo_documento;
            AnalisisMuestreo.numero_identificacion = obj.numero_documento;
            AnalisisMuestreo.nombres_apellidos_pacientes = obj.nombres_paciente;
            AnalisisMuestreo.edad = obj.edad;
            AnalisisMuestreo.sexo = obj.sexo;
            AnalisisMuestreo.nombre_del_mega = obj.nombre_mega;
            AnalisisMuestreo.fecha_ultimo_control = obj.fecha_ultimo_control;
            AnalisisMuestreo.cohorte_paciente = obj.cohorte_pertenece;
            AnalisisMuestreo.antecedentes_personales = obj.antecedentes_personales;
            AnalisisMuestreo.descripciones_atenciones_ambulatorias = obj.descripcion_atenciones_ambulatorias;
            AnalisisMuestreo.cie10_ingreso = obj.diagnostico_ingreso;
            AnalisisMuestreo.cie10_egreso = obj.diagnostico_egreso;
            AnalisisMuestreo.numero_controles = obj.numero_controles;
            AnalisisMuestreo.plan_manejo_ultimo_control = obj.plan_manejo_definido_ultimo_control;
            AnalisisMuestreo.concepto_auditoria_ambulatoria = obj.concepto_auditoria_ambulatoria;
            AnalisisMuestreo.adherencia_paciente = obj.adherencia_del_paciente;
            AnalisisMuestreo.plan_de_mejora_ppe = obj.plan_mejora_ppe;
            AnalisisMuestreo.recomendaciones_ambulatoria = obj.recomendaciones_ambulatorio;
            AnalisisMuestreo.auditor_asalud = obj.auditor_asalud;
            AnalisisMuestreo.fecha_ingreso = obj.fecha_ingreso;
            AnalisisMuestreo.fecha_egreso = obj.fecha_egreso;
            AnalisisMuestreo.descripcion_hospitalizacion = obj.descripcion_hospitalizacion;
            AnalisisMuestreo.concepto_auditoria = obj.concepto_auditoria;
            AnalisisMuestreo.conclusion_caso_ambulatorio = obj.conclusion_caso_ambulatorio;
            AnalisisMuestreo.hospitalizacion_evitable = obj.hospitalizacion_evitable;
            AnalisisMuestreo.recomendaciones_hospitalaraio = obj.recomendaciones_hospitalario;
            AnalisisMuestreo.definicion_integral_caso = obj.definicion_integral_caso;


            return AnalisisMuestreo;

        }

        public void ConsultaIdConcurrenciaMuestreo(Int32 id_concurrencia)
        {
            vw_analisis_caso_muestreo ObjAnalisisCasoM = new vw_analisis_caso_muestreo();
            ObjAnalisisCasoM.id_concurrencia = id_concurrencia;
            ListVWAnalisisCasoMuestreo = BusClass.GetIdAnalisisMuestras(id_concurrencia, ref MsgRes);

            foreach (var item in ListVWAnalisisCasoMuestreo)
            {
                id_concurrencia = item.id_concurrencia;
                numero_casos = item.numero_casos;
                id_regional = Convert.ToInt32(item.regional_origen);
                id_ips = Convert.ToInt32(item.id_ips);
                tipo_documento = item.afi_tipo_doc;
                numero_identificacion = item.id_afi;
                nombres_apellidos_pacientes = item.afi_nom;
                edad = Convert.ToString(item.afi_edad);
                sexo = item.sexo;
                nombre_del_mega = item.med_ser_trata;
                auditor_asalud = item.auditor_asalud;
                cie10_ingreso = item.Diagnostico_ingreso;
                cie10_egreso = item.Diagnostico_egreso;
                fecha_ingreso = item.fecha_ingreso;
                fecha_egreso = item.fecha_egreso;
                descripcion_hospitalizacion = item.Descripcion_hospitalizacion;
            }
        }



        #endregion

    }
}