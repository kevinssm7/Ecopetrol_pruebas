using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Concurrencia
{
    public class ConcurrenciaEgreso
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

        private List<Ref_condicion_del_egreso> _LstCondicionEgreso;
        public List<Ref_condicion_del_egreso> LstCondicionEgreso
        {
            get
            {
                if (_LstCondicionEgreso != null)
                {
                    return _LstCondicionEgreso;
                }
                else
                {
                    return _LstCondicionEgreso = BusClass.GetCondicionEgreso();
                }

            }
            set { _LstCondicionEgreso = value; }
        }

        private List<vw_cie10> _LstCie10;
        public List<vw_cie10> LstCie10
        {
            get
            {
                if (_LstCie10 == null)
                {
                    _LstCie10 = BusClass.GetCie10Unido();


                }

                return _LstCie10;
            }
            set { _LstCie10 = value; }
        }

        private List<vw_cie10> _LstCie102;
        public List<vw_cie10> LstCie102
        {
            get
            {
                if (_LstCie102 == null)
                {
                    _LstCie102 = BusClass.GetCie10Unido();

                    _LstCie102 = LstCie102.Where(x => x.estado_egreso == "A").ToList();
                    _LstCie102 = _LstCie102.Where(x => x.activo_cie10_principal == "SI").ToList();
                }

                return _LstCie102;
            }
            set { _LstCie102 = value; }
        }

        private List<vw_cie10> _LstCie10Principal;
        public List<vw_cie10> LstCie102Principal
        {
            get
            {
                if (_LstCie10Principal == null)
                {
                    _LstCie10Principal = BusClass.GetCie10Unido();

                    _LstCie10Principal = _LstCie10Principal.Where(x => x.estado_egreso == "A").ToList();
                    _LstCie10Principal = _LstCie10Principal.Where(x => x.activo_cie10_principal == "SI").ToList();
                    _LstCie10Principal = _LstCie10Principal.Where(x => x.presuntivo == "NO" || x.presuntivo.Contains("NO")).ToList();
                }

                return _LstCie10Principal;
            }
            set { _LstCie10Principal = value; }
        }


        private List<vw_cie10> _LstCie10Principal2;
        public List<vw_cie10> LstCie102Principal2
        {
            get
            {
                if (_LstCie10Principal2 == null)
                {
                    _LstCie10Principal2 = BusClass.GetCie10Unido();

                    _LstCie10Principal2 = _LstCie10Principal.Where(x => x.estado_egreso == "A").ToList();
                    _LstCie10Principal2 = _LstCie10Principal.Where(x => x.activo_cie10_principal == "SI").ToList();
                }

                return _LstCie10Principal2;
            }
            set { _LstCie10Principal2 = value; }
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

        private List<Ref_causal_egreso> _LisCausaEgreso;
        public List<Ref_causal_egreso> LisCausaEgreso
        {
            get
            {
                if (_LisCausaEgreso == null)
                {
                    return _LisCausaEgreso = BusClass.GetCausaEgreso();

                }
                else
                {
                    return _LisCausaEgreso;
                }

            }

            set
            {
                _LisCausaEgreso = value;
            }
        }

        private List<vw_ref_cups> _LstCups;
        public List<vw_ref_cups> LstCups
        {
            get
            {
                if (_LstCups == null)
                {
                    _LstCups = BusClass.GetCups();
                }

                return _LstCups;
            }
            set { _LstCups = value; }
        }

        private List<vw_ref_enfermedades_huerfanas> _LstEnfermedadesHuerfanas;
        public List<vw_ref_enfermedades_huerfanas> LstEnfermedadesHuerfanas
        {
            get
            {
                if (_LstEnfermedadesHuerfanas == null)
                {
                    _LstEnfermedadesHuerfanas = BusClass.GetEnfermedadesHuerfanas();
                }

                return _LstEnfermedadesHuerfanas;
            }
            set { _LstEnfermedadesHuerfanas = value; }
        }

        private egreso_auditoria_Hospitalaria _ObjEgreso;
        public egreso_auditoria_Hospitalaria ObjEgreso
        {
            get
            {
                if (_ObjEgreso == null)
                {
                    return _ObjEgreso = new egreso_auditoria_Hospitalaria();
                }
                else
                {
                    return _ObjEgreso;
                }

            }

            set
            {
                _ObjEgreso = value;
            }
        }

        private List<vw_tablero_concurrencia> _GetLista;
        public List<vw_tablero_concurrencia> GetLista
        {
            get
            {
                if (_GetLista == null)
                {
                    _GetLista = BusClass.GetTableroConcurrencia();
                    _GetLista = _GetLista.Where(X => X.id_concurrencia == id_concurrencia).ToList();

                    return _GetLista;

                }
                else
                {
                    return _GetLista;
                }

            }

            set
            {
                _GetLista = value;
            }
        }

        private List<Ref_lesiones_severas_y_alto_costo> _LstAltoCosto;
        public List<Ref_lesiones_severas_y_alto_costo> LstAltoCosto
        {
            get
            {
                if (_LstAltoCosto == null)
                {
                    _LstAltoCosto = BusClass.GetAltoCosto();
                    _LstAltoCosto = _LstAltoCosto.Where(x => x.estado == "A").ToList();

                    return _LstAltoCosto;

                }
                else
                {
                    return _LstAltoCosto;
                }

            }

            set
            {
                _LstAltoCosto = value;
            }
        }

        private List<Ref_enfermedades_Huerfanas> _GetHuerfanas;
        public List<Ref_enfermedades_Huerfanas> GetHuerfanas
        {
            get
            {
                if (_GetHuerfanas == null)
                {
                    _GetHuerfanas = BusClass.GetRefHuerfanas();
                    _GetHuerfanas = _GetHuerfanas.OrderBy(x => x.descripcion).ToList();

                    return _GetHuerfanas;
                }
                else
                {
                    return _GetHuerfanas;
                }

            }

            set
            {
                _GetHuerfanas = value;
            }
        }

        private ecop_censo _OBJCenso;
        public ecop_censo OBJCenso
        {
            get
            {
                if (_OBJCenso == null)
                {
                    return _OBJCenso = new ecop_censo();
                }
                else
                {
                    return _OBJCenso;
                }

            }

            set
            {
                _OBJCenso = value;
            }
        }

        private egreso_gestantes _OBJEgresoGestantes;
        public egreso_gestantes OBJEgresoGestantes
        {
            get
            {
                if (_OBJEgresoGestantes == null)
                {
                    return _OBJEgresoGestantes = new egreso_gestantes();
                }
                else
                {
                    return _OBJEgresoGestantes;
                }

            }

            set
            {
                _OBJEgresoGestantes = value;
            }
        }

        private List<egreso_gestantes> _ListEgresoGestantes;
        public List<egreso_gestantes> ListEgresoGestantes
        {
            get
            {
                if (_ListEgresoGestantes == null)
                {
                    return _ListEgresoGestantes = BusClass.ConsultasEgresoGestantes(id_concurrencia, ref MsgRes);
                }
                else
                {
                    return _ListEgresoGestantes;
                }

            }

            set
            {
                _ListEgresoGestantes = value;
            }
        }

        private categorizacion_egreso_hospitalario _OBJcategoriazcion;
        public categorizacion_egreso_hospitalario OBJcategoriazcion
        {
            get
            {
                if (_OBJcategoriazcion == null)
                {
                    return _OBJcategoriazcion = new categorizacion_egreso_hospitalario();
                }
                else
                {
                    return _OBJcategoriazcion;
                }

            }

            set
            {
                _OBJcategoriazcion = value;
            }
        }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        public Int32 id_concurrencia { get; set; }

        public Int32 id_censo { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha egreso")]
        public Nullable<DateTime> fecha_egreso { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "DIAGNÓSTICO PRINCIPAL EGRESO")]
        public string diagnostico_calificado { get; set; }


        [Display(Name = "Diagnóstico egreso2")]
        public string diagnostico_calificado2 { get; set; }

        [Display(Name = "Diagnóstico egreso3")]
        public string diagnostico_calificado3 { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Condicion alta")]
        public String CondicionAlta { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Número de defunción")]
        public String NumeroDefuncion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Hora de defunción")]
        public String HoraDefuncion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Tipo de instancia")]
        public int tipo_instancia { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Especialidad")]
        public int especialidad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Eventos en salud")]
        public String eventos_en_salud { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Procedimiento qx")]
        public String procedimientoqx { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "¿Editar concurrencia?")]
        public String mostrarConcu { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Cups")]
        public string id_cups_qx { get; set; }

        [Display(Name = "Cups 2")]
        public string id_cups_qx2 { get; set; }

        [Display(Name = "Cups 3")]
        public string id_cups_qx3 { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Incapacidades")]
        public String incapacidades { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Hospitalización prevenible")]
        public String hospitalizacion_prevenible { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Justificación prevenible")]
        public String descripcion_prevenible { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Alto costo/catastroficas")]
        public Boolean lesiones_severas { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Descripción alto costo")]
        public Int32 id_lesiones_severas { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha inicial incapacidad")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_inicial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha final incapacidad")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_final { get; set; }

        public int cantidad_dias { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Origen incapacidad")]
        public String origen_incap { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha fallecimiento")]
        public DateTime? fecha_de_muerte { get; set; }

        public DateTime? fecha_de_muerteok { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Observaciones/causa detallada de la muerte (nota auditoría)")]
        public String Observacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Diagnostico causa directa muerte")]
        public String diag_causa_directa_muerte { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Diagnostico causa basica muerte")]
        public String diag_causa_basica_muerte { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Diagnostico causa antecedente muerte")]
        public String diag_causa_antecedente_muerte { get; set; }

        [Display(Name = "Fecha expedicion cedula")]
        public DateTime? fecha_exp_cedula_fallecido { get; set; }

        public DateTime? fecha_exp_cedula_fallecidook { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Recien nacido")]
        public String gestante { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Edad gestacional")]
        public String edad_gestacional { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha nacimiento del menor")]
        public DateTime? fecha_nacimiento { get; set; }

        public DateTime? fecha_nacimientook { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Pesp")]
        public Int32 peso { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Via del parto")]
        public String id_via_parto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Talla")]
        public String talla { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Sexo")]
        public String sexo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "APGAR")]
        public String apgar { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Control prenatal")]
        public String control_prenatal { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha bcg")]
        public DateTime? fecha_BCG { get; set; }
        public DateTime? fecha_BCGok { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha vitamina k")]
        public DateTime? fecha_vitaminaK { get; set; }

        public DateTime? fecha_vitaminaKok { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha consejeria  de lactancia")]
        public DateTime? fecha_consenjeria_lactancia { get; set; }

        public DateTime? fecha_consenjeria_lactanciaok { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Resultado  de tsh")]
        public String resultadoTCH { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha de tsh")]
        public DateTime? fechaTCH { get; set; }

        public DateTime? fechaTCHok { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha consulta pediatria")]
        public DateTime? fecha_consulta_pediatria { get; set; }
        public DateTime? fecha_consulta_pediatriaok { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha de la vacuna hepatitis b")]
        public DateTime? fecha_hepatitisB { get; set; }

        public DateTime? fecha_hepatitisBok { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Causa egreso")]
        public String causal_egreso { get; set; }

        public DateTime fecha_digita { get; set; }

        public String usuario_digita { get; set; }


        public String Numero_factura { get; set; }

        public String valor_factura { get; set; }

        public String usuario_factura { get; set; }

        public DateTime fecha_factura { get; set; }

        public Nullable<DateTime> fecha_egresook { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Tiene enfermedades huérfanas")]
        public String tiene_enfermedad_huer { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Enfermedad huerfana")]
        public String id_enfermedades_huerfanas { get; set; }

        public String beneficiario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Tamizaje Auditivo RN (Res. 3280/2018)")]
        public String tamizaje { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Fecha realización tamizaje")]
        public DateTime? fecha_tamizaje { get; set; }
        public DateTime? fecha_tamizajeOk { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Calidad de diagnóstico de egreso")]
        public String calidadDiagnostico_egreso { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Diagnóstico de egreso de la historia clínica")]
        public string diagnosticoEgreso_historiaClnica { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Justificación")]
        public String justificacion { get; set; }

        //diagnostico_calificado
        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Infección intrahospitalaria")]
        public String infeccion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Nombre de microorganismo/germen responsable de la infección")]
        public String microorganismo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio*")]
        [Display(Name = "Acompañamiento durante el parto")]
        public String compañiaduranteparto { get; set; }

        #endregion

        #region METODOS
        public void ActualizaEgresoConcurrencia(ecop_concurrencia ObjConcurrencia, String User, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaEgresoConcurrencia(ObjConcurrencia, User, IPAddress, ref MsgRes);
        }

        public List<ecop_concurrencia_evolucion> ConsultaEvoluciones(ecop_concurrencia_evolucion Evolucion, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaEvoluciones(Evolucion, ref MsgRes);
        }

        public List<ecop_concurrencia> ConsultaCriterioIngresoActualizado(Int32 IdConcu, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaCriterioIngresoActualizado(IdConcu, ref MsgRes);
        }
        public List<ecop_concurrencia_encuesta_satisfacion> ConsultaEncuestaCargada(Int32 IdConcu, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaEncuestaCargada(IdConcu, ref MsgRes);
        }

        public int InsertaEgreso(egreso_auditoria_Hospitalaria ObjEgreso, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertaEgreso(ObjEgreso, UserName, IPAddress, ref MsgRes);
        }

        public void ActualizarCensoEgresoOK(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarCensoEgresoOK(ActualizaSiniestro, ref MsgRes);
        }

        public void ActualizaEgresoConcurrenciaOk(ecop_concurrencia ObjConcurrencia, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaEgresoConcurrenciaOk(ObjConcurrencia, ref MsgRes);
        }


        public void ActualizarEgresoCenso(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarEgresoCenso(OBJCenso, ref MsgRes);
        }

        public void Actualizarprevenible(ecop_concurrencia Objconcurrencia, ref MessageResponseOBJ MsgRes)
        {
            BusClass.Actualizarprevenible(Objconcurrencia, ref MsgRes);
        }

        public void InsertarEgresoGestantes(ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarEgresoGestantes(OBJEgresoGestantes, ref MsgRes);
        }

        //Alexis

        public List<vw_ecop_egresos_hospitalarios> GetListaEgresos(DateTime? fechainicial, DateTime? fechafinal)
        {
            return BusClass.GetListaEgresos(fechainicial, fechafinal);
        }
        public List<management_ecop_egresos_hospitalariosResult> listadoEgresosHospitalarios(DateTime? fechainicial, DateTime? fechafinal)
        {
            return BusClass.listadoEgresosHospitalarios(fechainicial, fechafinal);
        }

        public List<ref_tipo_hospitalizacion> GetRefTipoHospitalizacion()
        {
            return BusClass.GetRefTipoHospitalizacion();
        }
        public List<ref_tipo_patologia_catastrofica> GetRefTipoPatologiaCatastrofica()
        {
            return BusClass.GetRefTipoPatologiaCatastrofica();
        }
        public List<ref_pertinencia_estancia_prolongada> GetRefPertinenciaProlongada()
        {
            return BusClass.GetRefPertinenciaProlongada();
        }
        public List<ref_condicion_estancia_prolongada> GetRefCondicionProlongada()
        {
            return BusClass.GetRefCondicionProlongada();
        }

        public categorizacion_egreso_hospitalario getcatbyegreso(int idegreso)
        {
            return BusClass.getcatbyegreso(idegreso);
        }

        public void insertarcategorizacion(categorizacion_egreso_hospitalario obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.insertarcategorizacion(obj, ref MsgRes);
        }

        public void actualizarcategorizacion(categorizacion_egreso_hospitalario obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.actualizarcategorizacion(obj, ref MsgRes);
        }

        public int retornartipohabitacion(List<vw_evo_ecop_concurrencia_evoluciones> evoluciones)
        {
            int resultado = 1;

            var uci = evoluciones.Where(l => l.id_tipo_habitacion == 4).ToList();
            var uci_neonatal = evoluciones.Where(l => l.id_tipo_habitacion == 5).ToList();
            var uci_pediatrica = evoluciones.Where(l => l.id_tipo_habitacion == 6).ToList();
            var intermedios = evoluciones.Where(l => l.id_tipo_habitacion == 17).ToList();

            if (intermedios.Count > 0)
            {
                resultado = 2;
            }

            if (uci_neonatal.Count > 0)
            {
                resultado = 6;
            }

            if (uci_pediatrica.Count > 0)
            {
                resultado = 7;
            }

            if (uci.Count > 0)
            {
                resultado = 5;
            }


            return resultado;
        }

        public string RetornarPertinenciaEstancia(List<vw_evo_ecop_concurrencia_evoluciones> evoluciones)
        {
            string resultado = "SI";

            var list = evoluciones.Where(l => string.IsNullOrEmpty(l.tiene_estancia_pertinente)).ToList();

            if (list.Count > 0)
            {
                resultado = "NO";
            }

            return resultado;
        }

        public List<vw_evo_ecop_concurrencia_evoluciones> GetConcurrenciaEvolucionById(int id_evolucion)
        {
            return BusClass.GetConcurrenciaEvolucionById(id_evolucion);
        }

        public vw_ecop_evo_evaluacion_pertinencia GetEvaluacionPertinenciaById(int idevolucion)
        {
            return BusClass.GetEvaluacionPertinenciaById(idevolucion);
        }

        public List<Management_evolucion_procedimientosResult> ConsultaProcedimientosConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaProcedimientosConcurrencia(ref MsgRes);
        }

        #endregion
    }
}