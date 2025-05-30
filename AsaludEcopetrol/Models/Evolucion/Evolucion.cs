using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class Evolucion
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

        private List<Ref_tipo_habitacion> _LstTipoHabitacion;
        public List<Ref_tipo_habitacion> LstTipoHabitacion
        {
            get
            {
                if (_LstTipoHabitacion == null)
                {
                    //List<Ref_tipo_habitacion> LstTipoHabitacion2 = new List<Ref_tipo_habitacion>();

                    //LstTipoHabitacion2 = BusClass.GetTipoHabitacion();
                    //_LstTipoHabitacion = new List<Ref_tipo_habitacion>();
                    //foreach (Ref_tipo_habitacion item in LstTipoHabitacion2)
                    //{
                    //    if (item.id_ref_tipo_habitacion != 1)
                    //    {
                    //        _LstTipoHabitacion.Add(item);
                    //    }
                    //}

                    _LstTipoHabitacion = BusClass.GetTipoHabitacion();
                    _LstTipoHabitacion = _LstTipoHabitacion.Where(x => x.estado == "A").ToList();

                }

                return _LstTipoHabitacion;
            }
            set { _LstTipoHabitacion = value; }
        }

        private List<Ref_tipo_habitacion> _LstTipoHabitacionConsulta;
        public List<Ref_tipo_habitacion> LstTipoHabitacionConsulta
        {
            get
            {
                if (_LstTipoHabitacionConsulta == null)
                {
                    _LstTipoHabitacionConsulta = BusClass.GetTipoHabitacion();
                }

                return _LstTipoHabitacionConsulta;
            }
            set { _LstTipoHabitacionConsulta = value; }
        }

        private List<ecop_concurrencia_evolucion_diag_def> _lstDiagDef;
        public List<ecop_concurrencia_evolucion_diag_def> lstDiagDef
        {
            get
            {
                if (_lstDiagDef == null)
                {
                    _lstDiagDef = new List<ecop_concurrencia_evolucion_diag_def>();

                    // _lstDiagDef = BusClass.GetCDiagnosticoDefinitivo();
                }

                return _lstDiagDef;
            }
            set { _lstDiagDef = value; }
        }

        private List<vw_tipo_habitacion_censo> _ListTipoH;

        public List<vw_tipo_habitacion_censo> ListTipoH
        {
            get
            {
                if (_ListTipoH == null)
                {
                    return _ListTipoH = new List<vw_tipo_habitacion_censo>();
                }
                else
                {
                    return _ListTipoH;
                }

            }

            set
            {
                _ListTipoH = value;
            }
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

        private List<vw_cie10> _LstCie10Principal;
        public List<vw_cie10> LstCie102Principal
        {
            get
            {
                if (_LstCie10Principal == null)
                {
                    _LstCie10Principal = BusClass.GetCie10Unido();
                    //  _LstCie10Principal = _LstCie10Principal.Where(x => x.activo_cie10_principal == "SI").ToList();
                }

                return _LstCie10Principal;
            }
            set { _LstCie10Principal = value; }
        }

        private List<vw_cie10> _LstCie10Secundarios;
        public List<vw_cie10> LstCie10Secundarios
        {
            get
            {
                if (_LstCie10Secundarios == null)
                {
                    return _LstCie10Secundarios = new List<vw_cie10>();
                }
                else
                {

                    return _LstCie10Secundarios;
                }

            }
            set { _LstCie10Secundarios = value; }
        }





        //private List<vw_cie10_alertas> _ListaAlertasCie10;

        //public List<vw_cie10_alertas> ListaAlertasCie10
        //{
        //    get
        //    {
        //        if (_ListaAlertasCie10 == null)
        //        {
        //            return _ListaAlertasCie10 = new List<vw_cie10_alertas>();
        //        }
        //        else
        //        {
        //            return _ListaAlertasCie10;
        //        }

        //    }

        //    set
        //    {
        //        _ListaAlertasCie10 = value;
        //    }
        //}

        private List<vw_cie10_alertas_detalle> _ListaAlertasCie10;

        public List<vw_cie10_alertas_detalle> ListaAlertasCie10
        {
            get
            {
                if (_ListaAlertasCie10 == null)
                {
                    return _ListaAlertasCie10 = new List<vw_cie10_alertas_detalle>();
                }
                else
                {
                    return _ListaAlertasCie10;
                }

            }

            set
            {
                _ListaAlertasCie10 = value;
            }
        }


        private List<ecop_concurrencia_evolucion> _LstNumeroEvoluciones;
        public List<ecop_concurrencia_evolucion> LstNumeroEvoluciones
        {
            get
            {
                if (_LstNumeroEvoluciones == null)
                {
                    _LstNumeroEvoluciones = new List<ecop_concurrencia_evolucion>();
                }

                return _LstNumeroEvoluciones;
            }
            set { _LstNumeroEvoluciones = value; }
        }


        private List<vw_ecop_cohortes_evolucion> _LstCohortesAfil;
        public List<vw_ecop_cohortes_evolucion> LstCohortesAfil
        {
            get
            {
                if (_LstCohortesAfil == null)
                {
                    _LstCohortesAfil = new List<vw_ecop_cohortes_evolucion>();
                }

                return _LstCohortesAfil;
            }
            set { _LstCohortesAfil = value; }
        }

        private List<ecop_concurrencia_evolucion> _LstEvolucionesCargadas;
        public List<ecop_concurrencia_evolucion> LstEvolucionesCargadas
        {
            get
            {
                if (_LstEvolucionesCargadas == null)
                {
                    _LstEvolucionesCargadas = new List<ecop_concurrencia_evolucion>();
                }

                return _LstEvolucionesCargadas;
            }
            set { _LstEvolucionesCargadas = value; }
        }



        private List<egreso_auditoria_Hospitalaria> _LstEgreso;
        public List<egreso_auditoria_Hospitalaria> LstEgreso
        {
            get
            {
                if (_LstEgreso == null)
                {
                    _LstEgreso = BusClass.ConsultaAgresoH(id_concurrencia, ref MsgRes);
                }
                else
                {
                    return _LstEgreso;
                }
                return _LstEgreso;
            }

            set
            {
                _LstEgreso = value;
            }
        }


        private List<ecop_concurrencia> _LstConcu2;
        public List<ecop_concurrencia> LstConcu2
        {
            get
            {
                if (_LstConcu2 == null)
                {
                    _LstConcu2 = new List<ecop_concurrencia>();
                }

                return _LstConcu2;
            }
            set { _LstConcu2 = value; }
        }

        private List<ecop_concurrencia> _LstPaciente;
        public List<ecop_concurrencia> LstPaciente
        {
            get
            {
                if (_LstPaciente == null)
                {
                    _LstPaciente = new List<ecop_concurrencia>();
                }

                return _LstPaciente;
            }
            set { _LstPaciente = value; }
        }

        private alertas_generadas_concurrencia _ObjAlertas;
        public alertas_generadas_concurrencia ObjAlertas
        {
            get
            {
                if (_ObjAlertas == null)
                {
                    return _ObjAlertas = new alertas_generadas_concurrencia();
                }
                else
                {
                    return _ObjAlertas;
                }

            }

            set
            {
                _ObjAlertas = value;
            }
        }

        private egreso_auditoria_Hospitalaria _Objegreso;
        public egreso_auditoria_Hospitalaria Objegreso
        {
            get
            {
                if (_Objegreso == null)
                {
                    return _Objegreso = new egreso_auditoria_Hospitalaria();
                }
                else
                {
                    return _Objegreso;
                }

            }

            set
            {
                _Objegreso = value;
            }
        }

        private List<vw_evo_ecop_concurrencia> _LstPacienteEvo;
        public List<vw_evo_ecop_concurrencia> LstPacienteEvo
        {
            get
            {
                if (_LstPacienteEvo == null)
                {
                    _LstPacienteEvo = new List<vw_evo_ecop_concurrencia>();
                }

                return _LstPacienteEvo;
            }
            set { _LstPacienteEvo = value; }
        }


        private List<vw_evo_ecop_concurrencia_evoluciones> _LstEvolucionesCargadasIps;
        public List<vw_evo_ecop_concurrencia_evoluciones> LstEvolucionesCargadasIps
        {
            get
            {
                if (_LstEvolucionesCargadasIps == null)
                {
                    _LstEvolucionesCargadasIps = new List<vw_evo_ecop_concurrencia_evoluciones>();
                }

                return _LstEvolucionesCargadasIps;
            }
            set { _LstEvolucionesCargadasIps = value; }
        }


        private List<ecop_concurrencia_evolucion> _LstEvo3;
        public List<ecop_concurrencia_evolucion> LstEvo3
        {
            get
            {
                if (_LstEvo3 == null)
                {
                    _LstEvo3 = new List<ecop_concurrencia_evolucion>();
                }

                return _LstEvo3;
            }
            set { _LstEvo3 = value; }
        }



        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
        public Int32 id_concurrencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO DE HABITACION")]
        public Int32 id_tipo_habitacion { get; set; }


        [Display(Name = "EGRESO")]
        public Boolean egreso { get; set; }


        [Display(Name = "INFECCION INTRAHOSPITALARIA")]
        public string infencion_intrahospitalaria { get; set; }


        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100 caracteres")]
        [Display(Name = "CUAL")]
        public string des_infencion_intrahospitalaria { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA EVOLUCIÓN")]
        public Nullable<DateTime> fecha_evolucion { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "DESCRIPCIÓN EVOLUCIÓN")]
        public string descripcion_evolucion { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "GESTIÓN DE AUDITORIA")]
        public string gestion_auditor { get; set; }


        [RegularExpression(@"^[a-zA-Z\W\S]{1,8000}$", ErrorMessage = "Maximo 8000 caracteres")]
        [Display(Name = "NOTA INGRESO")]
        public string notaIngreso { get; set; }



        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "DIAGNOSTICO DEFINITIVO")]
        public string diagnosticoDefinitivo { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "PLAN DE MANEJO Y JUSTIFICACIÓN DE LA ESTANCIA")]
        public string justificacionEstancia { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "PRCEDIMIENTO QUIRURJICO")]
        public string tieneProcedimientosQui { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICOS PRINCIPAL")]
        public string id_cie10_1 { get; set; }

        public string id_cie10_1ok { get; set; }

        [Display(Name = "DIAGNOSTICOS CIE10_2")]
        public string id_cie10_2 { get; set; }
        public string id_cie10_2ok { get; set; }

        [Display(Name = "DIAGNOSTICOS CIE10_3")]
        public string id_cie10_3 { get; set; }
        public string id_cie10_3ok { get; set; }


        [Display(Name = "DIAGNOSTICOS CIE10_4")]
        public string id_cie10_4 { get; set; }
        public string id_cie10_4ok { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "CUPS")]
        public string id_cups_qx { get; set; }

        [Display(Name = "CUPS 2")]
        public string id_cups_qx2 { get; set; }

        [Display(Name = "CUPS 3")]
        public string id_cups_qx3 { get; set; }

        // private string _tieneProcedimientoQ;

        [Display(Name = "PROCEDIMIENTO QUIRURGICO")]
        public string tieneProcedimientoQ { get; set; }

        // private string _tieneGlosa;
        [Required(ErrorMessage = "***")]
        [Display(Name = "GLOSA")]
        public string tieneGlosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AHORRO")]
        public string Ahorro { get; set; }

        // private string _tieneEventoA;
        [Required(ErrorMessage = "***")]
        [Display(Name = "EVENTO ADVERSO")]
        public string tieneEventoA { get; set; }

        // private string _tieneSituacionCA;
        [Required(ErrorMessage = "***")]
        [Display(Name = "SITUACION DE CALIDAD EN LA ATENCION")]
        public string tieneSituacionCA { get; set; }
        public string tieneCohorteBenef { get; set; }


        public string fecha_ingreso { get; set; }

        public string validacion { get; set; }

        public string fecha_por_ingresar { get; set; }

        public string evoluciones_cargadas { get; set; }

        public DateTime? fecha_evolucionok { get; set; }

        public String tipo_h { get; set; }

        public int? afi_edad { get; set; }

        public String id_afi { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ESTANCIA PERTINENTE")]
        public string tiene_estancia_pertinente { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "RESULTADO  DE TSH")]
        public String resultadoTCH { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE TSH")]
        public DateTime? fechaTCH { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? fechaTCHok { get; set; }

        [RegularExpression(@"^[a-zA-Z\W\S]{1,8000}$", ErrorMessage = "Maximo 8000 caracteres")]
        [Display(Name = "NOTA INGRESO")]
        public string notaIngresoDilegenciada { get; set; }

        public string tieneCohorte { get; set; }

        #endregion

        #region METODOS
        public DateTime ManagmentHora()
        {
            return BusClass.ManagmentHora();
        }

        public void InsertaConcurrenciaEvolucion(ecop_concurrencia_evolucion Evolucion, List<ecop_concurrencia_evolucion_procedimientos> lista, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertaConcurrenciaEvolucion(Evolucion, lista, UserName, IPAddress, ref MsgRes);
        }


        public void ConsultaEvolucion(Int32 idconcu)
        {
            try
            {
                LstEvolucionesCargadasIps = new List<vw_evo_ecop_concurrencia_evoluciones>();
                vw_evo_ecop_concurrencia_evoluciones ObjEvolu = new vw_evo_ecop_concurrencia_evoluciones();
                ObjEvolu.id_concurrencia = idconcu;
                LstEvolucionesCargadasIps = BusClass.ConsultaEvolucionesIps(ObjEvolu, ref MsgRes);
                int conta = 1;
                int val = 1;
                DateTime dtFechaIngreso = DateTime.Now;
                ecop_concurrencia ObjConcu = new ecop_concurrencia();
                ecop_concurrencia_evolucion OBJ = new ecop_concurrencia_evolucion();
                List<vw_concurrencia_evolucion_Contrato> LstConcu = new List<vw_concurrencia_evolucion_Contrato>();
                ObjConcu.id_concurrencia = idconcu;
                OBJ.id_concurrencia = idconcu;
                LstConcu = BusClass.ConsultaIdConcurreniaEvolucion(ObjConcu, ref MsgRes);
                LstConcu2 = BusClass.ConsultaIdConcurrenia(ObjConcu, ref MsgRes);
                LstEvo3 = BusClass.ConsultaEvoluciones(OBJ, ref MsgRes);

                foreach (var itemNota in LstEvo3)
                {
                    notaIngresoDilegenciada = itemNota.notaIngreso;
                }
                foreach (var item in LstConcu2)
                {
                    afi_edad = item.afi_edad;
                    id_afi = item.id_afi;

                }

                ConsultaAfiliado(Convert.ToInt32(ObjEvolu.id_concurrencia));
                if (tipo_h == "B")
                {
                    foreach (vw_concurrencia_evolucion_Contrato item in LstConcu)
                    {
                        if (val == 1)
                        {
                            dtFechaIngreso = Convert.ToDateTime(item.fecha_ingreso);
                            fecha_ingreso = Convert.ToString(item.fecha_ingreso);
                            val = val + 1;
                        }
                        if (LstEvolucionesCargadasIps.Count == 0)
                        {
                            if (string.IsNullOrEmpty(item.fecha_ingreso.ToString()))
                            {
                                fecha_por_ingresar = DateTime.Now.ToString();
                            }
                            else
                            {
                                fecha_por_ingresar = item.fecha_ingreso.Value.ToString("dd/MM/yyyy");

                            }
                            evoluciones_cargadas = "NO";
                            if (item.fecha_egreso != null)
                            {
                                egreso = true;

                            }

                        }
                        foreach (vw_evo_ecop_concurrencia_evoluciones item2 in LstEvolucionesCargadasIps)
                        {
                            if (!(String.IsNullOrEmpty(item.fecha_egreso.ToString())))
                            {
                                if (item.fecha_egreso.Value.ToString("MM/dd/yyyy") == item2.fecha.Value.ToString("MM/dd/yyyy"))
                                {
                                    egreso = true;
                                    evoluciones_cargadas = "SI";
                                }
                                else
                                {
                                    evoluciones_cargadas = "NO";
                                }
                            }
                            else
                            {
                                evoluciones_cargadas = "NO";
                            }
                            if (item.fecha_ingreso.Value.ToString("dd/MM/yyyy") != item2.fecha.Value.ToString("dd/MM/yyyy"))
                            {
                                if (evoluciones_cargadas == "SI")
                                {
                                    validacion = "!!...   El paciente ya tiene ingresada todas las evoluciones cargadas...!!!";
                                }
                                else
                                {

                                }


                            }
                            else
                            {

                            }
                            dtFechaIngreso = dtFechaIngreso.AddDays(conta);
                        }

                    }
                }
                else
                {
                    foreach (vw_concurrencia_evolucion_Contrato item in LstConcu)
                    {
                        if (val == 1)
                        {
                            dtFechaIngreso = Convert.ToDateTime(item.fecha_ingreso);

                            fecha_ingreso = Convert.ToString(item.fecha_ingreso);
                            val = val + 1;
                        }
                        if (LstEvolucionesCargadasIps.Count == 0)
                        {
                            if (string.IsNullOrEmpty(item.fecha_ingreso.ToString()))
                            {
                                fecha_por_ingresar = DateTime.Now.ToString();
                            }
                            else
                            {
                                fecha_por_ingresar = item.fecha_ingreso.Value.ToString("dd/MM/yyyy");

                            }
                            evoluciones_cargadas = "NO";
                            if (item.fecha_egreso != null)
                            {
                                egreso = true;

                            }

                        }
                        foreach (vw_evo_ecop_concurrencia_evoluciones item2 in LstEvolucionesCargadasIps)
                        {
                            DateTime dtFechaIngresook = Convert.ToDateTime(item2.fecha);
                            if (!(String.IsNullOrEmpty(item.fecha_egreso.ToString())))
                            {
                                if (item.fecha_egreso.Value.ToString("MM/dd/yyyy") == item2.fecha.Value.ToString("MM/dd/yyyy"))
                                {
                                    egreso = true;
                                    evoluciones_cargadas = "SI";
                                }
                                else
                                {
                                    evoluciones_cargadas = "NO";
                                }
                            }
                            else
                            {
                                evoluciones_cargadas = "NO";
                            }
                            if (item.fecha_ingreso.Value.ToString("dd/MM/yyyy") != item2.fecha.Value.ToString("dd/MM/yyyy"))
                            {
                                if (evoluciones_cargadas == "SI")
                                {
                                    validacion = "!!...   El paciente ya tiene ingresada todas las evoluciones cargadas...!!!";
                                }
                                else
                                {
                                    validacion = "!!...Debe ingresar la Evolucion del día  " + Convert.ToString(dtFechaIngresook.AddDays(1).ToString("dd/MM/yyyy")) + "...!!";
                                }

                                fecha_por_ingresar = Convert.ToString(dtFechaIngresook.AddDays(1).ToString("dd/MM/yyyy"));
                            }
                            else
                            {
                                validacion = "!!...Debe ingresar la Evolucion del día  " + Convert.ToString(dtFechaIngresook.AddDays(1).ToString("dd/MM/yyyy")) + "...!!";
                                fecha_por_ingresar = Convert.ToString(dtFechaIngresook.AddDays(1).ToString("dd/MM/yyyy"));
                            }
                            dtFechaIngreso = dtFechaIngreso.AddDays(conta);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void EliminarEvolucion(Int32 idEvol)
        {
            ecop_concurrencia_evolucion ObjEvolu = new ecop_concurrencia_evolucion();
            ObjEvolu.id_evolucion = idEvol;
            BusClass.EliminarConcurrenciaEvolucion(ObjEvolu, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);
        }

        public List<ecop_concurrencia_evolucion> ConsultaEvoluciones(ecop_concurrencia_evolucion Evolucion, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaEvoluciones(Evolucion, ref MsgRes);
        }

        public List<ecop_concurrencia> ConsultaIdConcurrenia(ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdConcurrenia(ObjAfiliado, ref MsgRes);
        }

        public List<vw_concurrencia_evolucion_Contrato> ConsultaIdConcurreniaEvolucion(ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdConcurreniaEvolucion(ObjAfiliado, ref MsgRes);
        }

        public void ConsultaAfiliado(Int32 id_concu)
        {
            vw_evo_ecop_concurrencia ObjAfiliado = new vw_evo_ecop_concurrencia();
            ObjAfiliado.id_concurrencia = id_concu;
            LstPacienteEvo = BusClass.ConsultaIdConcurreniaEvo(ObjAfiliado, ref MsgRes);

            ecop_concurrencia_evolucion_diag_def Objdiagdef = new ecop_concurrencia_evolucion_diag_def();
            Objdiagdef.id_concurrencia = id_concu;
            lstDiagDef = BusClass.ConsultaDiagnosticoDefinitivo(Objdiagdef, ref MsgRes);

            vw_tipo_habitacion_censo ObjTipoH = new vw_tipo_habitacion_censo();
            ObjTipoH.id_concurrencia = id_concu;
            ListTipoH = BusClass.ConsultaTipoHabitacion(ObjTipoH, ref MsgRes);

            foreach (var item in ListTipoH)
            {
                tipo_h = item.tipo;
            }
        }

        public void InsertaDiagnosticoDefinitivo(ecop_concurrencia_evolucion_diag_def DiagnosticoDefinitivo, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {

            BusClass.InsertaDiagnosticoDefinitivo(DiagnosticoDefinitivo, UserName, IPAddress, ref MsgRes);
        }

        public void ConsultaDiagnosticoDefinitivo(Int32 idConcu)
        {
            ecop_concurrencia_evolucion_diag_def Objdiagdef = new ecop_concurrencia_evolucion_diag_def();
            Objdiagdef.id_concurrencia = idConcu;
            lstDiagDef = BusClass.ConsultaDiagnosticoDefinitivo(Objdiagdef, ref MsgRes);
        }


        //public void ConsultaListaAlertas(string strFiltro, ref MessageResponseOBJ MsgRes)
        //{
        //    ListaAlertasCie10 = BusClass.GetRefcie10Alertas();
        //    ListaAlertasCie10 = ListaAlertasCie10.Where(x => x.id_cie10 == strFiltro).ToList();

        //}
        public void ConsultaListaAlertasNuevo(string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            ListaAlertasCie10 = BusClass.GetRefcie10AlertasNuevo();
            ListaAlertasCie10 = ListaAlertasCie10.Where(x => x.id_cie10 == strFiltro).ToList();
        }

        public void ConsultaCie10Second(string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            LstCie10Secundarios = BusClass.GetCie10Unido();
            LstCie10Secundarios = LstCie10Secundarios.Where(x => x.activo_cie10_principal == "NO").ToList();
            LstCie10Secundarios = LstCie10Secundarios.Where(x => x.id_cie10 == strFiltro).ToList();

        }
        public void InsertarAlertasConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarAlertasConcurrencia(ObjAlertas, ref MsgRes);
        }

        public void ActualizarEgreso(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarEgreso(Objegreso, ref MsgRes);
        }

        public List<alertas_generadas_concurrencia> ConsultaAlertasConcurrencia(Int32 Idalerta, string idcie10)
        {
            return BusClass.ConsultaAlertasConcurrencia(Idalerta, idcie10, ref MsgRes);
        }

        public vw_cie10_alertas ConsultaAlertaCie10(String idcie10)
        {
            return BusClass.ConsultaAlertaCie10(idcie10, ref MsgRes);
        }

        public void ConsultaNumeroEvolucion(Int32 id_concu)
        {
            ecop_concurrencia_evolucion ObjAfiliado = new ecop_concurrencia_evolucion();
            ObjAfiliado.id_concurrencia = id_concu;
            LstNumeroEvoluciones = BusClass.ConsultaNumeroEvoluciones(ObjAfiliado, ref MsgRes);
        }



        public void ConsultaCohorteAfil(String Documento)
        {
            vw_ecop_cohortes_evolucion ObjAfiliado = new vw_ecop_cohortes_evolucion();
            ObjAfiliado.No_documento = Documento;
            LstCohortesAfil = BusClass.ConsultaCohortes(ObjAfiliado, ref MsgRes);

            if (LstCohortesAfil.Count() == 0)
            {
                tieneCohorte = "FALSE";

            }
            else
            {
                tieneCohorte = "TRUE";
            }




        }

        public List<vw_ref_cups> ListaCups()
        {
            return BusClass.GetCups();
        }

        #endregion
    }
}