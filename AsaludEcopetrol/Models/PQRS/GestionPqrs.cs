using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.PQRS
{
    public class GestionPqrs
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


        private List<Ref_PQRS_tipo_solicitud> _ListTipoSolicitud;
        public List<Ref_PQRS_tipo_solicitud> ListTipoSolicitud
        {
            get
            {
                if (_ListTipoSolicitud == null)
                {
                    return _ListTipoSolicitud = BusClass.GetRefPqrsSolicitud();
                }
                else
                {
                    return _ListTipoSolicitud;
                }

            }

            set
            {
                _ListTipoSolicitud = value;
            }
        }

        private List<Ref_PQRS_categorizacion> _ListTipocatego;
        public List<Ref_PQRS_categorizacion> ListTipocatego
        {
            get
            {
                if (_ListTipocatego == null)
                {
                    return _ListTipocatego = BusClass.ConsultaPQRSCategorizacion();
                }
                else
                {
                    return _ListTipocatego;
                }

            }

            set
            {
                _ListTipocatego = value;
            }
        }

        private List<Ref_PQRS_Atributo> _PqrsAtributo;
        public List<Ref_PQRS_Atributo> PqrsAtributo
        {
            get
            {
                if (_PqrsAtributo == null)
                {
                    return _PqrsAtributo = BusClass.GetRefPqrsAtributo();
                }
                else
                {
                    return _PqrsAtributo;
                }

            }

            set
            {
                _PqrsAtributo = value;
            }
        }


        private List<Ref_PQRS_estado_Gestion> _PqrsEstadoGest;
        public List<Ref_PQRS_estado_Gestion> PqrsEstadoGest
        {
            get
            {
                if (_PqrsEstadoGest == null)
                {
                    _PqrsEstadoGest = BusClass.GetRefPqrsGestion();
                    _PqrsEstadoGest = _PqrsEstadoGest.Where(x => x.id_ref_PQRS_estado_Gestion >= 2).ToList();

                    return _PqrsEstadoGest;
                }
                else
                {
                    return _PqrsEstadoGest;
                }

            }

            set
            {
                _PqrsEstadoGest = value;
            }
        }

        private List<Ref_PQRS_llamada> _PqrsLLamada;
        public List<Ref_PQRS_llamada> PqrsLLamada
        {
            get
            {
                if (_PqrsLLamada == null)
                {
                    return _PqrsLLamada = BusClass.GetRefPqrsLLamada();
                }
                else
                {
                    return _PqrsLLamada;
                }

            }

            set
            {
                _PqrsLLamada = value;
            }
        }

        private List<Ref_PQRS_Subtematica> _PqrSubtematica;
        public List<Ref_PQRS_Subtematica> PqrSubtematica
        {
            get
            {
                if (_PqrSubtematica == null)
                {
                    return _PqrSubtematica = BusClass.GetRefPqrsSubtematica();
                }
                else
                {
                    return _PqrSubtematica;
                }

            }

            set
            {
                _PqrSubtematica = value;
            }
        }


        private List<vw_PQRS_usuarios> _PqrsUsuarios;
        public List<vw_PQRS_usuarios> PqrsUsuarios
        {
            get
            {
                if (_PqrsUsuarios == null)
                {
                    return _PqrsUsuarios = BusClass.GetRefPqrsUsuarios();
                }
                else
                {
                    return _PqrsUsuarios;
                }

            }

            set
            {
                _PqrsUsuarios = value;
            }
        }

        private ecop_PQRS _ObjPqrs;
        public ecop_PQRS ObjPqrs
        {
            get
            {
                if (_ObjPqrs == null)
                {
                    return _ObjPqrs = new ecop_PQRS();
                }
                else
                {
                    return _ObjPqrs;
                }

            }

            set
            {
                _ObjPqrs = value;
            }
        }

        private List<vw_ecop_PQRS> _Lstvencidas;
        public List<vw_ecop_PQRS> Lstvencidas
        {
            get
            {
                if (_Lstvencidas == null)
                {
                    return _Lstvencidas = new List<vw_ecop_PQRS>();
                }
                else
                {
                    return _Lstvencidas;
                }
            }
            set
            {
                _Lstvencidas = value;
            }
        }

        private List<vw_ecop_PQRS> _Lstvencidas_tiempos;
        public List<vw_ecop_PQRS> Lstvencidas_tiempos
        {
            get
            {
                if (_Lstvencidas_tiempos == null)
                {
                    return _Lstvencidas_tiempos = new List<vw_ecop_PQRS>();
                }
                else
                {
                    return _Lstvencidas_tiempos;
                }
            }
            set
            {
                _Lstvencidas_tiempos = value;
            }
        }

        private List<vw_ecop_PQRS> _LstProntovencer;
        public List<vw_ecop_PQRS> LstProntovencer
        {
            get
            {
                if (_LstProntovencer == null)
                {
                    return _LstProntovencer = new List<vw_ecop_PQRS>();
                }
                else
                {
                    return _LstProntovencer;
                }
            }
            set
            {
                _LstProntovencer = value;
            }
        }

        private List<vw_ecop_PQRS> _Lstok;
        public List<vw_ecop_PQRS> Lstok
        {
            get
            {
                if (_Lstok == null)
                {
                    return _Lstok = new List<vw_ecop_PQRS>();
                }
                else
                {
                    return _Lstok;
                }
            }
            set
            {
                _Lstok = value;
            }
        }

        private List<vw_ecop_PQRS> _LstAmpliacion;
        public List<vw_ecop_PQRS> LstAmpliacion
        {
            get
            {
                if (_LstAmpliacion == null)
                {
                    return _LstAmpliacion = new List<vw_ecop_PQRS>();
                }
                else
                {
                    return _LstAmpliacion;
                }
            }
            set
            {
                _LstAmpliacion = value;
            }
        }

        private List<GestionDocumentalPQRS> _LstGestionDocumentalPqrs;
        public List<GestionDocumentalPQRS> LstGestionDocumentalPqrs
        {
            get
            {
                if (_LstGestionDocumentalPqrs == null)
                {
                    return _LstGestionDocumentalPqrs = new List<GestionDocumentalPQRS>();
                }
                else
                {
                    return _LstGestionDocumentalPqrs;
                }
            }
            set
            {
                _LstGestionDocumentalPqrs = value;
            }
        }

        private List<vw_ecop_PQRS> _LstAmpliacionVencidas;
        public List<vw_ecop_PQRS> LstAmpliacionVencidas
        {
            get
            {
                if (_LstAmpliacionVencidas == null)
                {
                    return _LstAmpliacionVencidas = new List<vw_ecop_PQRS>();
                }
                else
                {
                    return _LstAmpliacionVencidas;
                }
            }
            set
            {
                _LstAmpliacionVencidas = value;
            }
        }


        private List<vw_ecop_PQRS> _LstPQRS;
        public List<vw_ecop_PQRS> LstPQRS
        {
            get
            {
                if (_LstPQRS == null)
                {

                    if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "13" || SesionVar.ROL == "32")
                    {
                        _LstPQRS = BusClass.ConsultaPQRS();
                        _LstPQRS = _LstPQRS.Where(x => x.estado_gestion != 3).ToList();
                        _LstPQRS = _LstPQRS.OrderBy(x => x.dias_restantes10sales).ToList();



                    }
                    else
                    {
                        _LstPQRS = BusClass.ConsultaPQRS();
                        _LstPQRS = _LstPQRS.Where(x => x.estado_gestion != 3).ToList();
                        _LstPQRS = _LstPQRS.Where(x => x.usuario == SesionVar.UserName).ToList();
                        _LstPQRS = _LstPQRS.OrderBy(x => x.dias_restantes10sales).ToList();


                    }
                    return _LstPQRS;
                }
                else
                {
                    return _LstPQRS;
                }

            }

            set
            {
                _LstPQRS = value;
            }
        }

        private List<vw_ecop_PQRS_DetalleG> _ListDetallePqrs;
        public List<vw_ecop_PQRS_DetalleG> ListDetallePqrs
        {
            get
            {
                if (_ListDetallePqrs == null)
                {
                    _ListDetallePqrs = BusClass.ConsultaPQRSDetalle(id_ecop_PQRS);
                    _ListDetallePqrs = _ListDetallePqrs.OrderBy(x => x.fecha_gestion).ToList();

                    return _ListDetallePqrs;
                }
                else
                {
                    return _ListDetallePqrs;
                }

            }

            set
            {
                _ListDetallePqrs = value;
            }
        }

        private List<vw_ecop_PQRS> _LstPQRS2;
        public List<vw_ecop_PQRS> LstPQRS2
        {
            get
            {
                if (_LstPQRS2 == null)
                {

                    if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "32")
                    {
                        _LstPQRS2 = BusClass.ConsultaPQRS();
                        _LstPQRS2 = _LstPQRS2.Where(x => x.estado_gestion != 3).ToList();
                        _LstPQRS2 = _LstPQRS2.OrderBy(x => x.dias_restantes10sales).ToList();

                    }
                    else
                    {
                        _LstPQRS2 = BusClass.ConsultaPQRS();
                        _LstPQRS2 = _LstPQRS2.Where(x => x.estado_gestion != 3).ToList();
                        _LstPQRS2 = _LstPQRS2.Where(x => x.nombre_auditor == SesionVar.IDUser).ToList();
                        _LstPQRS2 = _LstPQRS2.OrderBy(x => x.dias_restantes10sales).ToList();

                    }
                    return _LstPQRS2;
                }
                else
                {
                    return _LstPQRS2;
                }

            }

            set
            {
                _LstPQRS2 = value;
            }
        }

        private List<vw_ecop_PQRS> _LstPQRSTotales;
        public List<vw_ecop_PQRS> LstPQRSTotales
        {
            get
            {
                if (_LstPQRSTotales == null)
                {
                    return _LstPQRSTotales = new List<vw_ecop_PQRS>();
                }
                else
                {
                    return _LstPQRSTotales;
                }

            }

            set
            {
                _LstPQRSTotales = value;
            }
        }
        public List<management_pqrs_tableroAuditorResult> _LstTableroAuditor;
        public List<management_pqrs_tableroAuditorResult> LstTableroAuditor
        {
            get
            {
                if (_LstTableroAuditor == null)
                {
                    return _LstTableroAuditor = new List<management_pqrs_tableroAuditorResult>();
                }
                else
                {
                    return _LstTableroAuditor;
                }

            }

            set
            {
                _LstTableroAuditor = value;
            }
        }

        private List<management_pqrs_tablero_controlResult> _LstPQRSTotalesTablero;
        public List<management_pqrs_tablero_controlResult> LstPQRSTotalesTablero
        {
            get
            {
                if (_LstPQRSTotalesTablero == null)
                {
                    return _LstPQRSTotalesTablero = new List<management_pqrs_tablero_controlResult>();
                }
                else
                {
                    return _LstPQRSTotalesTablero;
                }

            }

            set
            {
                _LstPQRSTotalesTablero = value;
            }
        }

        private List<vw_ecop_PQRS> _LstPQRS3;
        public List<vw_ecop_PQRS> LstPQRS3
        {
            get
            {
                if (_LstPQRS3 == null)
                {
                    if (SesionVar.ROL_CARGO == "9")
                    {
                        List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                        int RegionalAuditor = 0;

                        list = BusClass.GetRegionalAuditor();
                        list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                        foreach (var item in list)
                        {
                            RegionalAuditor = Convert.ToInt32(item.id_regional);
                        }

                        _LstPQRS3 = BusClass.ConsultaPQRS();
                        _LstPQRS3 = _LstPQRS3.Where(x => x.estado_gestion != 3).ToList();
                        _LstPQRS3 = _LstPQRS3.Where(x => x.regional == RegionalAuditor).ToList();
                        _LstPQRS3 = _LstPQRS3.OrderBy(x => x.dias_restantes10sales).ToList();

                        return _LstPQRS3;
                    }
                    else
                    {
                        return new List<vw_ecop_PQRS>();
                    }

                }
                else
                {
                    return _LstPQRS3;
                }

            }

            set
            {
                _LstPQRS3 = value;
            }
        }

        private List<management_pqrs_auditorListaResult> _ListDetallePqrsAud;
        public List<management_pqrs_auditorListaResult> ListDetallePqrsAud
        {
            get
            {
                if (_ListDetallePqrsAud == null)
                {
                    _ListDetallePqrsAud = BusClass.ListaPqrsAuditor(id_ecop_PQRS);
                    _ListDetallePqrsAud = _ListDetallePqrsAud.OrderBy(x => x.fecha_ingreso).ToList();

                    return _ListDetallePqrsAud;
                }
                else
                {
                    return _ListDetallePqrsAud;
                }

            }

            set
            {
                _ListDetallePqrsAud = value;
            }
        }

        private List<sis_usuario> _voboauditores;
        public List<sis_usuario> voboauditores
        {
            get
            {
                if (_voboauditores == null)
                {
                    _voboauditores = BusClass.GetUsuarios();
                    _voboauditores = _voboauditores.Where(x => x.id_rol == 7).ToList();

                    return _voboauditores;
                }
                else
                {
                    return _voboauditores;
                }

            }

            set
            {
                _voboauditores = value;
            }
        }

        public List<ecop_PQRS_Auditor> ConsultaPQRSAuditor(Int32 Id_pqrs)
        {
            return BusClass.ConsultaPQRSAuditor(Id_pqrs);
        }

        private List<ref_solucionador> _ListSolucionador;
        public List<ref_solucionador> ListSolucionador
        {
            get
            {
                if (_ListSolucionador == null)
                {
                    return _ListSolucionador = new List<ref_solucionador>();
                }
                else
                {
                    return _ListSolucionador;
                }

            }

            set
            {
                _ListSolucionador = value;
            }
        }

        public int id_ecop_PQRS { get; set; }

        public int id_ecop_PQRA_enrevision { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Número de caso:")]
        public String numero_caso { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Consecutivo del caso:")]
        public String consecutivo_caso { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha ingreso SalesForce:")]
        public DateTime? fecha_ingreso_salesforce { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha respuesta programada Salesforce:")]
        public DateTime? fecha_egreso_salesforce { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Hora SalesForce:")]
        public String horasalesforce { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Hora buzón:")]
        public String horabuzon { get; set; }

        public DateTime? fecha_ingreso_salesforceOK { get; set; }
        public DateTime? fecha_egreso_salesforceOK { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha ingreso buzón Asalud:")]
        public DateTime? fecha_ingreso_buzon_asalud { get; set; }
        public DateTime? fecha_ingreso_buzon_asaludOK { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha asignación:")]
        public DateTime? fecha_asignacion { get; set; }
        public DateTime? fecha_asignacionOK { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Número identificación solicitante:")]
        public String solicitante_numero_identi { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Nombre solicitante:")]
        public String solicitante_nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Regional:")]
        public int regional { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Ciudad:")]
        public int ciudad_del_caso { get; set; }

        [Display(Name = "Cargo:")]
        public int? cargo { get; set; }

        [Display(Name = "Auditor:")]
        public int? auditor { get; set; }

        //[Required(ErrorMessage = "Este campo es obligatorio *")]

        [Display(Name = "Tipo solicitud:")]
        public int tipo_solicitud { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Tipo categorización:")]
        public int tipo_categorizacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,20000}$", ErrorMessage = "Maximo 20000")]
        [Display(Name = "Especificación de la solicitud:")]
        public String especificacion_solicitud { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Usuario para asignar:")]
        public Int32 usuario_asignado { get; set; }

        public DateTime fecha_ingreso { get; set; }


        public String usuario_ingreso { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Estado gestión:")]
        public int estado_gestion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Subtemática:")]
        public int id_pqrs_subtematica { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Prestador:")]
        public int prestador { get; set; }
        public string prestadorTexto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Otro prestador:")]
        public string otro_prestador { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha de gestión:")]
        public DateTime? fecha_gestion { get; set; }
        public DateTime? fecha_gestionOK { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha de ampliación:")]
        public DateTime? fecha_ampliacion { get; set; }
        public DateTime? fecha_ampliacionok { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Requirió llamada:")]
        public string requirio_llamada { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿A quién llamó?:")]
        public int a_quie_llamo { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Telefono quien llamó?:")]
        public string telefono_llamo { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Nombre quién llamo:")]
        public String nombre_quien_llamo { get; set; }

        //nombre a quienes llamar al gestionar pqr

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Nombre quién llamo prestador:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,50}$", ErrorMessage = "Maximo 50 carácteres")]

        public String nombrellamo_prestador { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Telefono quien llamó prestador?:")]
        public string telefono_llamo_prestador { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Nombre quién llamo auditor:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,50}$", ErrorMessage = "Maximo 50 carácteres")]
        public String nombrellamo_auditor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Telefono quien llamó auditor?:")]
        public string telefono_llamo_auditor { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Nombre quién llamo Ecopetrol:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,50}$", ErrorMessage = "Maximo 50 carácteres")]
        public String nombrellamo_ecopetrol { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Telefono quien llamó Ecopetrol?:")]
        public string telefono_llamo_ecopetrol { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Nombre quién llamo paciente:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,50}$", ErrorMessage = "Maximo 50 carácteres")]
        public String nombrellamo_paciente { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Telefono quien llamó paciente?:")]
        public string telefono_llamo_paciente { get; set; }


        //fin a quienes llamar en gestionar pqr

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Es evento adverso?:")]
        public String evento_adverso { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000")]
        [Display(Name = "Observación gestión:")]
        public String observacion_gestion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000")]
        [Display(Name = "Observacion ampliacion:")]

        public String observacion_ampliacion { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Validez:")]
        public String validez { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Atributo:")]
        public int atributo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Solucionador:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100")]
        public String solucionador { get; set; }
        public String solucionadorLleno { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Otro solucionador:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100")]
        public String otro_solucionador { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Otro solucionador correo:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100")]
        public String otro_solucionador_correo { get; set; }

        public String Habilitar { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Archivo de la proyección:")]
        public String pdf { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Archivo de la revisión:")]
        public String pdf2 { get; set; }

        [Display(Name = "Archivo auditor:")]
        public String pdf3 { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha de envio al prestador:")]
        public DateTime? fechaenvioprestadorselect { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha de envio al prestador:")]
        public DateTime? fechaenvioprestador { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha de recepción respuesta del prestador:")]
        public DateTime? fecharecepcionprestadorselect { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha de recepción respuesta del prestador:")]
        public DateTime? fecharecepcionprestador { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "VoBo auditor:")]
        public string Voboauditor { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Requiere análisis de caso?:")]
        public string requiereAnalisis_caso { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Documento beneficiario:")]
        public string documento_beneficiario { get; set; }

        [Display(Name = "Edad beneficiario:")]
        public int? edad_beneficiario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Atributo:")]
        public int? atributo_auditor { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Validez:")]
        public string validez_Auditor { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "VoBo:")]
        public string vobo_Auditor { get; set; }


        //[Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Respuesta del prestador")]
        public string respuesta_prestador { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Contacto con el beneficiario")]
        public string contacto_con_beneficiario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Concepto técnico del auditor:")]
        public string concepto_tecnico_auditor { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Se solucionó el problema?")]
        public string solucion_problema { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Tipo identificación solicitante:")]
        public string tipo_identificacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Nombre paciente:")]
        public string nombre_paciente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Tipo identificación paciente:")]
        public string tipo_identificacion_paciente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Número identificación paciente:")]
        public string identificacion_paciente { get; set; }

        [Display(Name = "Archivo:")]
        public HttpPostedFileBase file { get; set; }

        public String listaQuien { get; set; }

        public String listaAuditor { get; set; }

        public String listaPrestador { get; set; }

        public string pasa_auditor { get; set; }

        public int mismoBeneficiario { get; set; }

        public int notificacionesExternas { get; set; }

        public string correosNotificar { get; set; }
        public string correosCopiar { get; set; }

        public string cuerpoCorreo { get; set; }

        public string asuntoCorreo { get; set; }

        public int regionalOpcion { get; set; }

        private List<sis_usuario> _ListAudPQRS;
        public List<sis_usuario> ListAudPQRS
        {
            get
            {
                _ListAudPQRS = BusClass.GetListAuditorCiudad(regional, ciudad_del_caso, ref MsgRes);
                _ListAudPQRS = _ListAudPQRS.Where(x => x.id_rol_cargo == cargo).ToList();

                return _ListAudPQRS;
            }

            set
            {
                _ListAudPQRS = value;
            }
        }

        private List<Ref_rol_cargo> _RolCargos;
        public List<Ref_rol_cargo> RolCargos
        {
            get
            {
                _RolCargos = BusClass.RolCargo();

                _RolCargos = _RolCargos.Where(x => x.id_rol_cargo == 20 || x.id_rol_cargo == 10 || x.id_rol_cargo == 13 || x.id_rol_cargo == 14 || x.id_rol_cargo == 15 || x.id_rol_cargo == 17).ToList();

                return _RolCargos;
            }
            set
            {
                _RolCargos = value;
            }
        }

        public int total_casos { get; set; }

        public int vencidas { get; set; }

        public int vencidasTiempos { get; set; }

        public int porvencer { get; set; }

        public int restantes { get; set; }

        public int ampliacion { get; set; }

        public int ampliacionVen { get; set; }
        public int totalVobo { get; set; }

        public int totalAsignacion { get; set; }

        public ICollection<CorreosPqrs> Detalle { get; set; }

        public String correo { get; set; }

        public String asunto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Solucionador:")]
        public int id_solicionador { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Auxiliar solucionador:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100")]
        public String auxiliar_solucionador { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Otro Auxiliar solucionador:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100")]
        public string otro_auxiliar_solucionador { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Otro Auxiliar solucionador correo:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100")]
        public string otro_auxiliar_solucionador_correo { get; set; }

        //nuevo

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Reasignar analista?:")]
        public string reasignar_analista { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Nombre nuevo analista:")]
        public String nuevo_analista { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Fecha prestador:")]
        public String FechaPrestador { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Supersalud:")]
        public int superSalud { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Componente hospitalario:")]
        public int? componente { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "¿Requiere plan de mejora?:")]
        public int? plan_mejora { get; set; }



        [Required(ErrorMessage = "Este campo es obligatorio *")]
        [Display(Name = "Observación plan de mejora:")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,1999}$", ErrorMessage = "Maximo 2000")]
        public string observacion_plan_mejora { get; set; }


        public int seHaceLlamada { get; set; }

        //fin nuevo


        #endregion

        #region FUNCIONES

        public Int32 InsertarPQRS(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPQRS(ObjPqrs, ref MsgRes);
        }
        public void ConsultaPQRS()
        {

            List<vw_ecop_PQRS> LIST = new List<vw_ecop_PQRS>();

            LIST = BusClass.ConsultaPQRS();
            LIST = LIST.Where(X => X.id_ecop_PQRS == id_ecop_PQRS).ToList();

            foreach (var item in LIST)
            {
                numero_caso = item.numero_caso;
                consecutivo_caso = item.consecutivo_caso;
                fecha_ingreso_salesforce = item.fecha_ingreso_salesforce;
                fecha_egreso_salesforce = item.fecha_cierre_salesforce;
                fecha_ingreso_buzon_asalud = item.fecha_ingreso_buzon_asalud;
                fecha_asignacion = item.fecha_asignacion;
                solicitante_numero_identi = item.solicitante_numero_identi;
                solicitante_nombre = item.solicitante_nombre;
                regional = item.regional.Value;
                ciudad_del_caso = item.ciudad_del_caso.Value;
                tipo_solicitud = item.tipo_solicitud.Value;
                especificacion_solicitud = item.especificacion_solicitud;
                usuario_asignado = item.usuario_asignado.Value;
                solucionador = item.solucionador;
                otro_prestador = item.otro_prestador;
                //auditor = BusClass.Getidauditor(item.nombre_auditor).Value;

                if (item.estado_gestion == 2)
                {
                    id_pqrs_subtematica = item.id_pqrs_subtematica.Value;
                    prestador = item.prestador.Value;
                }
            }
        }

        public void ConsultaPQRSId(Int32 id_ecop_PQRS)
        {
            List<management_vw_ecop_PQRS2Result> LIST2 = new List<management_vw_ecop_PQRS2Result>();

            try
            {
                LIST2 = BusClass.ConsultaPQRSId3(id_ecop_PQRS);

                foreach (var item in LIST2)
                {
                    numero_caso = item.numero_caso;
                    consecutivo_caso = item.consecutivo_caso;
                    fecha_ingreso_salesforce = item.fecha_ingreso_salesforce;
                    fecha_egreso_salesforce = item.fecha_cierre_salesforce;
                    fecha_ingreso_buzon_asalud = item.fecha_ingreso_buzon_asalud;
                    fecha_asignacion = item.fecha_asignacion;
                    solicitante_numero_identi = item.solicitante_numero_identi;
                    solicitante_nombre = item.solicitante_nombre;
                    regional = item.regional.Value;
                    ciudad_del_caso = item.ciudad_del_caso.Value;
                    tipo_solicitud = (int)item.tipo_solicitud;
                    especificacion_solicitud = item.especificacion_solicitud;
                    usuario_asignado = (int)item.usuario_asignado;
                    solucionador = item.solucionador;
                    otro_prestador = item.otro_prestador;
                    pasa_auditor = item.pasa_auditor;
                    //componente = item.componente;

                    nombre_paciente = item.nombre_paciente;
                    identificacion_paciente = item.identificacion_paciente;

                    //estado_gestion = item.estado_gestion.Value;
                    if (item.estado_gestion == 2 || item.estado_gestion == 5)
                    {
                        id_pqrs_subtematica = item.id_pqrs_subtematica.Value;
                        //prestador = item.prestador;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void CamposGestionPqr(List<management_vw_ecop_PQRS_DetalleGResult> lista, int idPqr)
        {
            if (lista.Count() > 0)
            {
                management_vw_ecop_PQRS_DetalleGResult obj = new management_vw_ecop_PQRS_DetalleGResult();
                try
                {
                    obj = lista.OrderByDescending(x => x.id_ecop_PQRS_Detalle).FirstOrDefault();
                    if (obj != null)
                    {
                        id_pqrs_subtematica = obj.id_pqrs_subtematica.Value;
                        prestadorTexto = obj.prestador;
                        Voboauditor = obj.vobo_auditor;
                        requiereAnalisis_caso = obj.analisis_caso;
                        estado_gestion = (int)obj.estado_gestion;
                        id_pqrs_subtematica = (int)obj.id_pqrs_subtematica;
                        fecha_gestion = obj.fecha_gestion;
                        fecha_gestionOK = obj.fecha_gestion;
                        evento_adverso = obj.evento_adverso;
                        requirio_llamada = obj.requirio_llamada;
                        documento_beneficiario = obj.documento_beneficiario;
                        edad_beneficiario = obj.edad_beneficiario;
                        observacion_gestion = obj.observacion_gestion;
                        evento_adverso = obj.evento_adverso;
                        auditor = obj.id_voboAuditor;
                        componente = obj.componente;
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }
        }
        public void ActualizarPQRS(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarPQRS(ObjPqrs, ref MsgRes);
        }

        public List<sis_usuario> GetListAuditorCiudad(Int32 regional, Int32 ciudad)
        {
            return BusClass.GetListAuditorCiudad(regional, ciudad, ref MsgRes);
        }

        public List<GestionDocumentalPQRS2> GetUrlProyeccion(Int32 id)
        {

            List<GestionDocumentalPQRS2> ListUrl = BusClass.GetUrlProyeccion(id, ref MsgRes);

            //string dirpath = Path.Combine(Request.PhysicalApplicationPath);
            return ListUrl;
        }

        public List<vw_ecop_PQRS> BuscarTablero()
        {
            List<vw_ecop_PQRS> list1 = new List<vw_ecop_PQRS>();

            if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "13" || SesionVar.ROL == "32")
            {
                list1 = BusClass.ConsultaPQRS();
                list1 = list1.Where(x => x.estado_gestion != 3).ToList();
                list1 = list1.OrderBy(x => x.dias_restantes10sales).ToList();

            }
            else
            {
                list1 = BusClass.ConsultaPQRS();
                list1 = list1.Where(x => x.estado_gestion != 3).ToList();
                list1 = list1.Where(x => x.usuario == SesionVar.UserName).ToList();
                list1 = list1.OrderBy(x => x.dias_restantes10sales).ToList();
            }
            return list1;
        }

        public Int32 InsertarPQRSAuditor(ecop_PQRS_Auditor OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPQRSAuditor(OBJ, ref MsgRes);
        }


        public Int32 InsertarPQRSProyeccion(GestionDocumentalPQRS2 OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPQRSProyeccion(OBJ, ref MsgRes);
        }


        public Int32 InsertarPQRSEnrevision(ecop_PQRS_enrevision OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPQRSEnrevision(OBJ, ref MsgRes);
        }



        public void ActualizarFechaPQRS(Int32 id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarFechaPQRS(id_ecop_PQRS, ref MsgRes);
        }


        public void ActualizaestadoPQRSEnrevision(ecop_PQRS_enrevision obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaestadoPQRSEnrevision(obj, ref MsgRes);
        }

        public void ActualizarGestionPQRSEnrevision(ecop_PQRS_enrevision obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarGestionPQRSEnrevision(obj, ref MsgRes);
        }


        public void ActualizaReabrirPQRS(ecop_PQRS obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaReabrirPQRS(obj, ref MsgRes);
        }

        public void ActualizarFechaPQRSDirec(Int32 id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarFechaPQRSDirec(id_ecop_PQRS, ref MsgRes);
        }

        public int ActualizarPqrsEstado(ecop_PQRS obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ActualizarPqrsEstado(obj, ref MsgRes);
        }

        public List<Ref_PQRS_Atributo> listaAtributosPqrs()
        {
            return BusClass.listaAtributosPqrs();
        }
        public void ConsultaTotales()
        {
            if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "32" || SesionVar.ROL == "15")
            {
                LstPQRSTotalesTablero = BusClass.GestiontableroPQRS();
                LstPQRSTotalesTablero = LstPQRSTotalesTablero.OrderBy(x => x.dias_restantes10sales).ToList();

                Lstvencidas = LstPQRSTotales.Where(x => x.dias_restantes10sales == 0 && x.estado_gestion != 4).ToList();
                Lstvencidas_tiempos = LstPQRSTotales.Where(x => x.dias_restantes10sales > 0 && x.dias_restantes10sales <= 2 && x.estado_gestion != 4).ToList();
                LstProntovencer = LstPQRSTotales.Where(x => x.dias_restantes10sales >= 3 && x.dias_restantes10sales <= 8).ToList();
                Lstok = LstPQRSTotales.Where(x => x.dias_restantes10sales > 8).ToList();
                LstAmpliacion = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli > 2).ToList();
                LstAmpliacionVencidas = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli <= 2).ToList();

                total_casos = LstPQRSTotales.Count();
                vencidas = Lstvencidas.Count();
                vencidasTiempos = Lstvencidas_tiempos.Count();
                porvencer = LstProntovencer.Count();
                restantes = Lstok.Count();
                ampliacion = LstAmpliacion.Count();
                ampliacionVen = LstAmpliacionVencidas.Count();

            }
            else
            {
                LstPQRSTotales = BusClass.ConsultaPQRS();
                LstPQRSTotales = LstPQRSTotales.Where(x => x.estado_gestion != 3).ToList();
                LstPQRSTotales = LstPQRSTotales.Where(x => x.usuario == SesionVar.UserName).ToList();
                LstPQRSTotales = LstPQRSTotales.OrderBy(x => x.dias_restantes10sales).ToList();

                Lstvencidas = LstPQRSTotales.Where(x => x.dias_restantes10sales == 0 && x.estado_gestion != 4).ToList();
                Lstvencidas_tiempos = LstPQRSTotales.Where(x => x.dias_restantes10sales > 0 && x.dias_restantes10sales <= 2 && x.estado_gestion != 4).ToList();
                LstProntovencer = LstPQRSTotales.Where(x => x.dias_restantes10sales >= 3 && x.dias_restantes10sales <= 8).ToList();
                Lstok = LstPQRSTotales.Where(x => x.dias_restantes10sales > 8).ToList();
                LstAmpliacion = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli > 2).ToList();
                LstAmpliacionVencidas = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli <= 2).ToList();

                total_casos = LstPQRSTotales.Count();
                vencidas = Lstvencidas.Count();
                porvencer = LstProntovencer.Count();
                restantes = Lstok.Count();
                ampliacion = LstAmpliacion.Count();
                ampliacionVen = LstAmpliacionVencidas.Count();
            }
        }

        //public List<management_pqrs_tableroAuditorResult> consultaTableroAuditor()
        //{
        //    sis_usuario usuario = new sis_usuario();

        //    var idUsuario = 0;
        //    idUsuario = SesionVar.IDUser;

        //    try
        //    {
        //        usuario = BusClass.datosUsuarioId(idUsuario);
        //        var idRolCargo = 0;

        //        if (usuario != null)
        //        {
        //            idRolCargo = (int)usuario.id_rol_cargo;
        //        }

        //        if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "2" || SesionVar.ROL == "32" || idRolCargo == 9)
        //        {
        //            LstTableroAuditor = BusClass.ConsultaTableroAuditor(0, 0).Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI")).ToList();

        //            if (SesionVar.ROL != "1" || idRolCargo != 1 || SesionVar.ROL != "10" || SesionVar.ROL == "32")
        //            {
        //                List<management_pqrs_tableroAuditorResult> lista2 = new List<management_pqrs_tableroAuditorResult>();

        //                if (SesionVar.ROL == "2")
        //                {
        //                    List<sis_auditor_regional_pqrs> listadoRegionales = new List<sis_auditor_regional_pqrs>();
        //                    listadoRegionales = BusClass.listadoRegionalesUsuarioPqrs(Convert.ToInt32(idUsuario));

        //                    if (listadoRegionales != null)
        //                    {
        //                        foreach (var item in listadoRegionales)
        //                        {
        //                            List<management_pqrs_tableroAuditorResult> lista1 = new List<management_pqrs_tableroAuditorResult>();
        //                            lista1 = LstTableroAuditor.Where(x => x.regional == item.id_regional).ToList();

        //                            lista2.AddRange(lista1);
        //                        }

        //                        LstTableroAuditor = lista2;
        //                    }
        //                }
        //                else
        //                {

        //                    List<sis_auditor_regional> listadoRegionales = new List<sis_auditor_regional>();
        //                    listadoRegionales = BusClass.listadoRegionalesUsuario(Convert.ToInt32(idUsuario));

        //                    if (listadoRegionales != null)
        //                    {
        //                        foreach (var item in listadoRegionales)
        //                        {
        //                            List<management_pqrs_tableroAuditorResult> lista1 = new List<management_pqrs_tableroAuditorResult>();
        //                            lista1 = LstTableroAuditor.Where(x => x.regional == item.id_regional).ToList();

        //                            lista2.AddRange(lista1);
        //                        }
        //                        LstTableroAuditor = lista2;
        //                    }
        //                }
        //            }

        //            LstTableroAuditor = LstTableroAuditor.OrderBy(x => x.dias_restantes10sales).ToList();
        //        }

        //        else
        //        {
        //            LstTableroAuditor = BusClass.ConsultaTableroAuditor(1, SesionVar.IDUser).Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI")).OrderBy(x => x.dias_restantes10sales).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return LstTableroAuditor;
        //}


        //public List<management_pqrs_tableroAuditorResult> consultaTableroAuditor()
        //{
        //    try
        //    {
        //        var idUsuario = SesionVar.IDUser;
        //        var usuario = BusClass.datosUsuarioId(idUsuario);

        //        if (usuario == null)
        //        {
        //            return new List<management_pqrs_tableroAuditorResult>();
        //        }

        //        var idRolCargo = usuario.id_rol_cargo.GetValueOrDefault();

        //        List<management_pqrs_tableroAuditorResult> lstTableroAuditor = new List<management_pqrs_tableroAuditorResult>();

        //        if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "2" || SesionVar.ROL == "32" || idRolCargo == 9)
        //        {
        //            lstTableroAuditor = BusClass.ConsultaTableroAuditor(0, 0)
        //                .Where(x => x.vobo_auditor == "SI" || x.analisis_caso == "SI")
        //                .ToList();

        //            if (!(SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "32" || idRolCargo == 1))
        //            {
        //                var regionales = (SesionVar.ROL == "2")
        //                    ? BusClass.listadoRegionalesUsuarioPqrs(idUsuario)
        //                    : BusClass.listadoRegionalesUsuario(idUsuario);

        //                if (regionales != null)
        //                {
        //                    lstTableroAuditor = lstTableroAuditor
        //                        .Where(x => regionales.Any(r => r.id_regional == x.regional))
        //                        .ToList();
        //                }
        //            }

        //            lstTableroAuditor = lstTableroAuditor
        //                .OrderBy(x => x.dias_restantes10sales)
        //                .ToList();
        //        }
        //        else
        //        {
        //            lstTableroAuditor = BusClass.ConsultaTableroAuditor(1, idUsuario)
        //                .Where(x => x.vobo_auditor == "SI" || x.analisis_caso == "SI")
        //                .OrderBy(x => x.dias_restantes10sales)
        //                .ToList();
        //        }

        //        return lstTableroAuditor;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Se maneja el error, se puede loguear si es necesario
        //        var error = ex.Message;
        //        return new List<management_pqrs_tableroAuditorResult>();
        //    }
        //}


        //public List<management_pqrs_tableroAuditorResult> consultaTableroAuditor()
        //{
        //    try
        //    {
        //        var idUsuario = SesionVar.IDUser;
        //        var usuario = BusClass.datosUsuarioId(idUsuario);

        //        if (usuario == null)
        //        {
        //            return new List<management_pqrs_tableroAuditorResult>();
        //        }

        //        var idRolCargo = usuario.id_rol_cargo.GetValueOrDefault();

        //        List<management_pqrs_tableroAuditorResult> lstTableroAuditor = new List<management_pqrs_tableroAuditorResult>();

        //        // Verificamos si el rol es uno de los roles especiales o el rol cargo es 9
        //        if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "2" || SesionVar.ROL == "32" || idRolCargo == 9)
        //        {
        //            lstTableroAuditor = BusClass.ConsultaTableroAuditor(0, 0)
        //                .Where(x => x.vobo_auditor == "SI" || x.analisis_caso == "SI")
        //                .ToList();

        //            // Si no tiene rol administrativo o el rol cargo no es 1, filtra por regionales
        //            if (!(SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "32" || idRolCargo == 1))
        //            {
        //                List<dynamic> regionales = null;

        //                // Usamos el listado correspondiente dependiendo del rol
        //                if (SesionVar.ROL == "2")
        //                {
        //                    regionales = BusClass.listadoRegionalesUsuarioPqrs(idUsuario)
        //                        .Cast<dynamic>().ToList(); // Cast a dynamic para manejar tipos diferentes
        //                }
        //                else
        //                {
        //                    regionales = BusClass.listadoRegionalesUsuario(idUsuario)
        //                        .Cast<dynamic>().ToList();
        //                }

        //                if (regionales != null)
        //                {
        //                    lstTableroAuditor = lstTableroAuditor
        //                        .Where(x => regionales.Any(r => r.id_regional == x.regional))
        //                        .ToList();
        //                }
        //            }

        //            lstTableroAuditor = lstTableroAuditor
        //                .OrderBy(x => x.dias_restantes10sales)
        //                .ToList();
        //        }
        //        else
        //        {
        //            lstTableroAuditor = BusClass.ConsultaTableroAuditor(1, idUsuario)
        //                .Where(x => x.vobo_auditor == "SI" || x.analisis_caso == "SI")
        //                .OrderBy(x => x.dias_restantes10sales)
        //                .ToList();
        //        }

        //        return lstTableroAuditor;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Se maneja el error, se puede loguear si es necesario
        //        var error = ex.Message;
        //        return new List<management_pqrs_tableroAuditorResult>();
        //    }
        //}


        public List<management_pqrs_tableroAuditorResult> consultaTableroAuditor(int? estado)
        {
            sis_usuario usuario = new sis_usuario();

            var idUsuario = 0;
            idUsuario = SesionVar.IDUser;

            try
            {
                usuario = BusClass.datosUsuarioId(idUsuario);
                var idRolCargo = 0;

                if (usuario != null)
                {
                    idRolCargo = (int)usuario.id_rol_cargo;
                }

                if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "2" || SesionVar.ROL == "32" || idRolCargo == 9)
                {
                    LstTableroAuditor = estado != null ? FiltrarPorEstado(BusClass.ConsultaTableroAuditor(0, 0).Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI")).ToList(), estado) : BusClass.ConsultaTableroAuditor(0, 0).Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI")).ToList();

                    if (SesionVar.ROL != "1" || idRolCargo != 1 || SesionVar.ROL != "10" || SesionVar.ROL == "32")
                    {
                        List<management_pqrs_tableroAuditorResult> lista2 = new List<management_pqrs_tableroAuditorResult>();

                        if (SesionVar.ROL == "2")
                        {
                            List<sis_auditor_regional_pqrs> listadoRegionales =  BusClass.listadoRegionalesUsuarioPqrs(Convert.ToInt32(idUsuario));

                            if (listadoRegionales != null)
                            {
                                foreach (var item in listadoRegionales)
                                {
                                    List<management_pqrs_tableroAuditorResult> lista1 =  LstTableroAuditor.Where(x => x.regional == item.id_regional).ToList();
                                    lista2.AddRange(lista1);
                                }

                                LstTableroAuditor = lista2;
                            }
                        }
                        else
                        {

                            List<sis_auditor_regional> listadoRegionales = BusClass.listadoRegionalesUsuario(Convert.ToInt32(idUsuario));

                            if (listadoRegionales != null)
                            {
                                foreach (var item in listadoRegionales)
                                {
                                    List<management_pqrs_tableroAuditorResult> lista1 = LstTableroAuditor.Where(x => x.regional == item.id_regional).ToList();
                                    lista2.AddRange(lista1);
                                }
                                LstTableroAuditor = lista2;
                            }
                        }
                    }

                    LstTableroAuditor = LstTableroAuditor.OrderBy(x => x.dias_restantes10sales).ToList();
                }

                else
                {
                    LstTableroAuditor = BusClass.ConsultaTableroAuditor(1, SesionVar.IDUser).Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI")).OrderBy(x => x.dias_restantes10sales).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return LstTableroAuditor;
        }

        public List<management_pqrs_tableroAuditorResult> FiltrarPorEstado(List<management_pqrs_tableroAuditorResult> lista, int? estado)
        {
            switch (estado)
            {
                case 1:
                    return lista.Where(x => x.supersalud == 1).ToList();
                case 2:
                    return lista.Where(x => x.reabierto == 1).ToList();
                case 3:
                    return lista.Where(x => x.dias_ok_salfe_egreso > 1 && x.estado_gestion != 4).ToList();
                case 4:
                    return lista.Where(x => x.dias_ok_salfe_egreso <= 1 && x.estado_gestion != 4).ToList();
                case 5:
                    return lista.Where(x => x.semaforo_ok_salfe == "5").ToList();
                case 6:
                    return lista.Where(x => x.semaforo_ok_salfe == "4").ToList();
                case 7:
                    return lista.Where(x => x.vobo_auditor == "SI").ToList();
                case 8:
                    return lista.Where(x => x.analisis_caso == "SI").ToList();
                case 9:
                    return lista.Where(x => x.pasa_auditor == "SI").ToList();
                default:
                    return lista;
            }
        }


        public void ConsultaTotalesAuditor()
        {
            sis_usuario usuario = new sis_usuario();

            var idUsuario = 0;
            idUsuario = SesionVar.IDUser;

            try
            {
                usuario = BusClass.datosUsuarioId(idUsuario);
                var idRolCargo = 0;

                if (usuario != null)
                {
                    idRolCargo = (int)usuario.id_rol_cargo;
                }

                if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "2" || SesionVar.ROL == "32" || idRolCargo == 9)
                {
                    LstPQRSTotales = BusClass.ConsultaPQRS();
                    //LstPQRSTotales = LstPQRSTotales.Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI") && x.estado_gestion == 5 && x.pasa_auditor == "SI").ToList();
                    LstPQRSTotales = LstPQRSTotales.Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI")).ToList();

                    //LstPQRSTotales = LstPQRSTotales.Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI") && x.pasa_auditor == "SI").ToList();

                    if (SesionVar.ROL != "1" || idRolCargo != 1 || SesionVar.ROL != "10" || SesionVar.ROL == "32")
                    {
                        List<vw_ecop_PQRS> lista2 = new List<vw_ecop_PQRS>();

                        List<sis_auditor_regional> listadoRegionales = new List<sis_auditor_regional>();
                        listadoRegionales = BusClass.listadoRegionalesUsuario(Convert.ToInt32(idUsuario));

                        if (listadoRegionales != null)
                        {
                            foreach (var item in listadoRegionales)
                            {
                                List<vw_ecop_PQRS> lista1 = new List<vw_ecop_PQRS>();
                                lista1 = LstPQRSTotales.Where(x => x.regional == item.id_regional).ToList();

                                lista2.AddRange(lista1);
                            }
                            LstPQRSTotales = lista2;
                        }
                    }

                    LstPQRSTotales = LstPQRSTotales.OrderBy(x => x.dias_restantes10sales).ToList();

                    Lstvencidas = LstPQRSTotales.Where(x => x.dias_restantes10sales <= 3 && x.estado_gestion != 4).ToList();
                    LstProntovencer = LstPQRSTotales.Where(x => x.dias_restantes10sales >= 4 && x.dias_restantes10sales <= 8).ToList();
                    Lstok = LstPQRSTotales.Where(x => x.dias_restantes10sales > 8).ToList();
                    LstAmpliacion = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli > 2).ToList();
                    LstAmpliacionVencidas = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli <= 2).ToList();

                    total_casos = LstPQRSTotales.Count();
                    vencidas = Lstvencidas.Count();
                    porvencer = LstProntovencer.Count();
                    restantes = Lstok.Count();
                    ampliacion = LstAmpliacion.Count();
                    ampliacionVen = LstAmpliacionVencidas.Count();

                    totalVobo = LstPQRSTotales.Where(x => x.vobo_auditor == "SI").Count();
                    totalAsignacion = LstPQRSTotales.Where(x => x.analisis_caso == "SI").Count();
                }

                else
                {
                    LstPQRSTotales = BusClass.ConsultaPQRS();
                    //LstPQRSTotales = LstPQRSTotales.Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI") && x.estado_gestion == 5 && x.pasa_auditor == "SI").ToList();
                    LstPQRSTotales = LstPQRSTotales.Where(x => (x.vobo_auditor == "SI" || x.analisis_caso == "SI")).ToList();
                    LstPQRSTotales = LstPQRSTotales.Where(x => x.nombre_auditor == SesionVar.IDUser).ToList();
                    LstPQRSTotales = LstPQRSTotales.OrderBy(x => x.dias_restantes10sales).ToList();

                    Lstvencidas = LstPQRSTotales.Where(x => x.dias_restantes10sales <= 3 && x.estado_gestion != 4).ToList();
                    LstProntovencer = LstPQRSTotales.Where(x => x.dias_restantes10sales >= 4 && x.dias_restantes10sales <= 8).ToList();
                    Lstok = LstPQRSTotales.Where(x => x.dias_restantes10sales > 8).ToList();
                    LstAmpliacion = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli > 2).ToList();
                    LstAmpliacionVencidas = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli <= 2).ToList();

                    total_casos = LstPQRSTotales.Count();
                    vencidas = Lstvencidas.Count();
                    porvencer = LstProntovencer.Count();
                    restantes = Lstok.Count();
                    ampliacion = LstAmpliacion.Count();
                    ampliacionVen = LstAmpliacionVencidas.Count();

                    totalVobo = LstPQRSTotales.Where(x => x.vobo_auditor == "SI").Count();
                    totalAsignacion = LstPQRSTotales.Where(x => x.analisis_caso == "SI").Count();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void ConsultaTotalesCordinador()
        {
            try
            {


                if (SesionVar.ROL_CARGO == "9" || SesionVar.ROL_CARGO == "19" || SesionVar.ROL == "1")
                {
                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                    int RegionalAuditor = 0;

                    list = BusClass.GetRegionalAuditor();
                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                    foreach (var item in list)
                    {
                        RegionalAuditor = Convert.ToInt32(item.id_regional);
                    }

                    LstPQRSTotales = BusClass.ConsultaPQRS();
                    LstPQRSTotales = LstPQRSTotales.Where(x => x.estado_gestion != 3).ToList();
                    LstPQRSTotales = LstPQRSTotales.Where(x => x.regional == RegionalAuditor).ToList();
                    LstPQRSTotales = LstPQRSTotales.OrderBy(x => x.dias_restantes10sales).ToList();

                    Lstvencidas = LstPQRSTotales.Where(x => x.dias_restantes10sales <= 3 && x.estado_gestion != 4).ToList();
                    LstProntovencer = LstPQRSTotales.Where(x => x.dias_restantes10sales >= 4 && x.dias_restantes10sales <= 8).ToList();
                    Lstok = LstPQRSTotales.Where(x => x.dias_restantes10sales > 8).ToList();
                    LstAmpliacion = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli > 2).ToList();
                    LstAmpliacionVencidas = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli <= 2).ToList();

                    total_casos = LstPQRSTotales.Count();
                    vencidas = Lstvencidas.Count();
                    porvencer = LstProntovencer.Count();
                    restantes = Lstok.Count();
                    ampliacion = LstAmpliacion.Count();
                    ampliacionVen = LstAmpliacionVencidas.Count();

                }
                else
                {
                    LstPQRSTotales = new List<vw_ecop_PQRS>();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void ConsultaTotalesProyectadas()
        {
            List<vw_ecop_PQRS> totales = new List<vw_ecop_PQRS>();
            try
            {


                LstPQRSTotales = BusClass.ConsultaPQRS();
                //LstPQRSTotales = LstPQRSTotales.Where(x => x.estado_gestion == 3 || x.estado_gestion == 6).ToList();
                LstPQRSTotales = LstPQRSTotales.Where(x => x.estado_gestion == 3).ToList();
                LstPQRSTotales = LstPQRSTotales.Where(x => x.fecha_envio_proyectada != null).ToList();
                #region
                foreach (vw_ecop_PQRS item in LstPQRSTotales)
                {

                    var caso = totales.Where(l => l.numero_caso == item.numero_caso).FirstOrDefault();
                    if (caso == null)
                    {
                        var val = LstPQRSTotales.Where(l => l.numero_caso == item.numero_caso).OrderByDescending(l => l.fecha_asignacion).ToList();
                        if (val.Count == 1)
                        {
                            totales.AddRange(val);
                        }
                        else
                        {
                            totales.Add(val[0]);
                        }
                    }

                }
                #endregion
                LstPQRSTotales = totales;

                if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "32")
                {
                    LstPQRSTotales = LstPQRSTotales.OrderBy(x => x.dias_restantes10sales).ToList();
                    Lstvencidas = LstPQRSTotales.Where(x => x.dias_restantes10sales == 0 && x.estado_gestion != 4).ToList();
                    Lstvencidas_tiempos = LstPQRSTotales.Where(x => x.dias_restantes10sales > 0 && x.dias_restantes10sales <= 2 && x.estado_gestion != 4).ToList();
                    LstProntovencer = LstPQRSTotales.Where(x => x.dias_restantes10sales >= 3 && x.dias_restantes10sales <= 8).ToList();
                    Lstok = LstPQRSTotales.Where(x => x.dias_restantes10sales > 8).ToList();
                    LstAmpliacion = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli > 2).ToList();
                    LstAmpliacionVencidas = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli <= 2).ToList();


                    total_casos = LstPQRSTotales.Count();
                    vencidas = Lstvencidas.Count();
                    vencidasTiempos = Lstvencidas_tiempos.Count();
                    porvencer = LstProntovencer.Count();
                    restantes = Lstok.Count();
                    ampliacion = LstAmpliacion.Count();
                    ampliacionVen = LstAmpliacionVencidas.Count();
                }

                else
                {
                    LstPQRSTotales = LstPQRSTotales.Where(x => x.usuario == SesionVar.UserName).ToList();
                    LstPQRSTotales = LstPQRSTotales.OrderBy(x => x.dias_restantes10sales).ToList();

                    Lstvencidas = LstPQRSTotales.Where(x => x.dias_restantes10sales <= 3 && x.estado_gestion != 4).ToList();
                    LstProntovencer = LstPQRSTotales.Where(x => x.dias_restantes10sales >= 4 && x.dias_restantes10sales <= 8).ToList();
                    Lstok = LstPQRSTotales.Where(x => x.dias_restantes10sales > 8).ToList();
                    LstAmpliacion = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli > 2).ToList();
                    LstAmpliacionVencidas = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli <= 2).ToList();

                    total_casos = LstPQRSTotales.Count();
                    vencidas = Lstvencidas.Count();
                    porvencer = LstProntovencer.Count();
                    restantes = Lstok.Count();
                    ampliacion = LstAmpliacion.Count();
                    ampliacionVen = LstAmpliacionVencidas.Count();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void ConsultaTotalesProyectadasFinal()
        {
            List<vw_ecop_PQRS> totales = new List<vw_ecop_PQRS>();
            try
            {


                LstPQRSTotales = BusClass.ConsultaPQRS();
                LstPQRSTotales = LstPQRSTotales.Where(x => x.estado_gestion == 7).ToList();
                LstPQRSTotales = LstPQRSTotales.Where(x => x.fecha_envio_proyectada == null).ToList();
                #region
                foreach (vw_ecop_PQRS item in LstPQRSTotales)
                {

                    var caso = totales.Where(l => l.numero_caso == item.numero_caso).FirstOrDefault();
                    if (caso == null)
                    {
                        var val = LstPQRSTotales.Where(l => l.numero_caso == item.numero_caso).OrderByDescending(l => l.fecha_asignacion).ToList();
                        if (val.Count == 1)
                        {
                            totales.AddRange(val);
                        }
                        else
                        {
                            totales.Add(val[0]);
                        }
                    }

                }
                #endregion
                LstPQRSTotales = totales;

                if (SesionVar.ROL == "1" || SesionVar.ROL == "10" || SesionVar.ROL == "32")
                {
                    LstPQRSTotales = LstPQRSTotales.OrderBy(x => x.dias_restantes10sales).ToList();
                    Lstvencidas = LstPQRSTotales.Where(x => x.dias_restantes10sales == 0 && x.estado_gestion != 4).ToList();
                    Lstvencidas_tiempos = LstPQRSTotales.Where(x => x.dias_restantes10sales > 0 && x.dias_restantes10sales <= 2 && x.estado_gestion != 4).ToList();
                    LstProntovencer = LstPQRSTotales.Where(x => x.dias_restantes10sales >= 3 && x.dias_restantes10sales <= 8).ToList();
                    Lstok = LstPQRSTotales.Where(x => x.dias_restantes10sales > 8).ToList();
                    LstAmpliacion = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli > 2).ToList();
                    LstAmpliacionVencidas = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli <= 2).ToList();


                    total_casos = LstPQRSTotales.Count();
                    vencidas = Lstvencidas.Count();
                    vencidasTiempos = Lstvencidas_tiempos.Count();
                    porvencer = LstProntovencer.Count();
                    restantes = Lstok.Count();
                    ampliacion = LstAmpliacion.Count();
                    ampliacionVen = LstAmpliacionVencidas.Count();
                }

                else
                {
                    LstPQRSTotales = LstPQRSTotales.Where(x => x.usuario == SesionVar.UserName).ToList();
                    LstPQRSTotales = LstPQRSTotales.OrderBy(x => x.dias_restantes10sales).ToList();

                    Lstvencidas = LstPQRSTotales.Where(x => x.dias_restantes10sales <= 3 && x.estado_gestion != 4).ToList();
                    LstProntovencer = LstPQRSTotales.Where(x => x.dias_restantes10sales >= 4 && x.dias_restantes10sales <= 8).ToList();
                    Lstok = LstPQRSTotales.Where(x => x.dias_restantes10sales > 8).ToList();
                    LstAmpliacion = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli > 2).ToList();
                    LstAmpliacionVencidas = LstPQRSTotales.Where(x => x.estado_gestion == 4 && x.dias_restantesAmpli <= 2).ToList();

                    total_casos = LstPQRSTotales.Count();
                    vencidas = Lstvencidas.Count();
                    porvencer = LstProntovencer.Count();
                    restantes = Lstok.Count();
                    ampliacion = LstAmpliacion.Count();
                    ampliacionVen = LstAmpliacionVencidas.Count();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }


        public List<vw_ecop_PQRS_correo_direc> ConsultaPQRSCorreo()
        {
            return BusClass.ConsultaPQRSCorreo();
        }

        public void EliminarPQRS(int id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarPQRS(id_ecop_PQRS, ref MsgRes);
        }

        public Int32 InsertarPQRSEliminar(Log_eliminacion_pqrs OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPQRSEliminar(OBJ, ref MsgRes);
        }
        public void ActualizarEnvioPQRS(Int32 id_ecop_PQRS, String usuario, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarEnvioPQRS(id_ecop_PQRS, usuario, ref MsgRes);
        }

        public List<Ref_PQRS_correo_envio> ConsultaPQRSref_correo()
        {
            return BusClass.ConsultaPQRSref_correo();
        }


        public void EliminarPQRSEnrevision(int id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarPQRSEnrevision(id_ecop_PQRS, ref MsgRes);
        }

        public List<Ref_PQRS_categorizacion> ConsultaPQRSCategorizacion()
        {
            return BusClass.ConsultaPQRSCategorizacion();
        }

        public List<management_pqrs_tablero_controlResult> GestiontableroPQRS()
        {
            return BusClass.GestiontableroPQRS();
        }

        public List<management_pqrs_tablero_control_proyectadasFinalesResult> DatosTableroPqrsProyectadasFinales()
        {
            return BusClass.DatosTableroPqrsProyectadasFinales();
        }
        public List<management_pqrs_proyectadasCierreResult> DatosTableroPqrsProyectadasCierre()
        {
            return BusClass.DatosTableroPqrsProyectadasCierre();
        }
        public List<management_pqrs_tablero_control_proyectadasResult> GestiontableroPQRSProyectadas(string numCaso, string numOpc, string numDocumento, DateTime? fechaInicial, DateTime? fechaFinal, int? idPqr)
        {
            return BusClass.GestiontableroPQRSProyectadas(numCaso, numOpc, numDocumento, fechaInicial, fechaFinal, idPqr);
        }

        public List<ref_solucionador> getSolucionador(int idCiudad)
        {
            ListSolucionador = BusClass.getSolucionador(idCiudad);
            return BusClass.getSolucionador(idCiudad);
        }

        public List<Management_PQRS_solucionadoresResult> getSolucionadorAux()
        {
            return BusClass.getSolucionadorAux();
        }
        public int ActualizarUsuarioAsignado(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ActualizarUsuarioAsignado(OBJ, ref MsgRes);
        }
        public int ActualizarCategorizacionPQR(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ActualizarCategorizacionPQR(OBJ, ref MsgRes);
        }

        internal IEnumerable<GestionDocumentalPQRS2> GetUrlProyeccion(string idPqrs)
        {
            throw new NotImplementedException();
        }

        public int CargueMasivoQuienLlamoPqrs(List<ecop_pqrs_a_quien_llamo> detalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CargueMasivoQuienLlamoPqrs(detalle, ref MsgRes);
        }

        public int CargueMasivoAuditores(List<ecop_pqrs_auditores> detalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CargueMasivoAuditores(detalle, ref MsgRes);
        }

        public int CargueMasivoPrestadores(List<ecop_pqrs_prestadores> detalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CargueMasivoPrestadores(detalle, ref MsgRes);
        }

        public int ExcelMasivoPqrs(DataTable dt2, ecop_pqrs_registroMasivo Pqr, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_PQRS> Listado = new List<ecop_PQRS>();
            var RtaInsercion = 0;

            var usuario = SesionVar.NombreUsuario;
            ref_solucionador regPropio = new ref_solucionador();
            var regionalPropia = 0;
            List<ref_solucionador> solucionadorLis = new List<ref_solucionador>();
            ref_solucionador solucionador = new ref_solucionador();
            List<Ref_ciudades> Listaciudades = new List<Ref_ciudades>();
            List<Ref_tipo_documental> tipoDocumento = new List<Ref_tipo_documental>();
            List<Ref_regional> regionales = new List<Ref_regional>();
            List<Ref_PQRS_categorizacion> categorizacion = new List<Ref_PQRS_categorizacion>();
            List<Ref_PQRS_tipo_solicitud> tipoSolicitud = new List<Ref_PQRS_tipo_solicitud>();
            var mensajeLog = "";
            log_cargues_masivos logMas = new log_cargues_masivos();

            logMas.fecha_Cargue = DateTime.Now;
            logMas.periodo_cargue = DateTime.Now;
            logMas.nombre_digita = SesionVar.NombreUsuario;
            logMas.usuario_digita = SesionVar.UserName;
            logMas.tipo_registro = "Cargue PQRS";

            try
            {
                tipoSolicitud = BusClass.GetRefPqrsSolicitud();
                tipoDocumento = BusClass.GetTipoIdentificacion(ref MsgRes);
                regionales = BusClass.GetRefRegion();
                Listaciudades = BusClass.TotalCiudades();
                regPropio = BusClass.UltimaRegionalUsuario(usuario);
                categorizacion = BusClass.ConsultaPQRSCategorizacion();

                if (regPropio != null)
                {
                    regionalPropia = (int)regPropio.id_regional;
                    solucionadorLis = BusClass.getSolucionadorRegional((int)regionalPropia);

                    if (solucionadorLis.Count() > 0)
                    {
                        solucionador = solucionadorLis.Where(x => x.nombre_solucionador == usuario).FirstOrDefault();
                    }

                    Listaciudades = Listaciudades.Where(x => x.id_ref_regional == regPropio.id_regional).ToList();
                }

                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        ecop_PQRS obj = new ecop_PQRS();

                        List<ecop_PQRS> duplicado = new List<ecop_PQRS>();
                        List<Ref_ciudades> ciudades = new List<Ref_ciudades>();
                        ciudades = Listaciudades;

                        fila++;
                        if (!string.IsNullOrEmpty(item["Número caso"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "Número caso";
                            texto = Convert.ToString(item["Número caso"]);
                            if (texto.Length <= 49)
                            {
                                var respuesta = buscarNumeroCaso(texto);
                                if (respuesta != 1)
                                {
                                    obj.numero_caso = Convert.ToString(item["Número caso"]).ToUpper();
                                }
                                else
                                {
                                    textError = columna + ", Número de caso ya existe";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 49 caracteres.";
                                throw new Exception(textError);
                            }

                            duplicado = Listado.Where(x => x.numero_caso == obj.numero_caso).ToList();
                            if (duplicado.Count() > 0)
                            {
                                textError = columna + ", Ya se encuentra dentro del mismo cargue.";
                                throw new Exception(textError);
                            }

                            columna = "Consecutivo caso";
                            texto = Convert.ToString(item["Consecutivo caso"]);
                            if (texto.Length <= 49)
                            {
                                obj.consecutivo_caso = Convert.ToString(item["Consecutivo caso"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 49 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha ingreso buzón Asalud";
                            obj.fecha_ingreso_buzon_asalud = DateTime.Now;

                            columna = "Fecha ingreso SalesForce";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha ingreso SalesForce"]);
                                if (fechas != null)
                                {
                                    if (fechas > DateTime.Now)
                                    {
                                        textError = columna + ", no puede ser mayor a la fecha actual";
                                        throw new Exception(textError);
                                    }
                                    else
                                    {
                                        obj.fecha_ingreso_salesforce = Convert.ToDateTime(item["Fecha ingreso SalesForce"]);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("ser mayor a la fecha actual"))
                                {
                                    textError = ex.Message;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                }

                                throw new Exception(textError);
                            }

                            columna = "Fecha respuesta programada";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha respuesta programada"]);
                                if (fechas != null)
                                {
                                    obj.fecha_egreso_salesforce = Convert.ToDateTime(item["Fecha respuesta programada"]);
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            if (obj.fecha_egreso_salesforce < obj.fecha_ingreso_salesforce)
                            {
                                textError = "Fecha ingreso SalesForce no puede ser mayor a la fecha de respuesta programada SalesForce.";
                                throw new Exception(textError);
                            }

                            columna = "Validación fechas SalesForce-";
                            try
                            {
                                if (Convert.ToDateTime(obj.fecha_ingreso_salesforce) > Convert.ToDateTime(obj.fecha_egreso_salesforce))
                                {
                                    textError = columna + "Fecha ingreso SalesForce no puede ser mayor a Fecha egreso SalesForce";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + "Fecha ingreso SalesForce no puede ser mayor a Fecha egreso SalesForce";
                                throw new Exception(textError);
                            }

                            columna = "Tipo solicitud";
                            try
                            {
                                numero = Convert.ToInt32(item["Tipo solicitud"]);
                                if (numero != null)
                                {
                                    obj.tipo_solicitud = Convert.ToInt32(item["Tipo solicitud"]);

                                    Ref_PQRS_tipo_solicitud solicitud = new Ref_PQRS_tipo_solicitud();
                                    solicitud = tipoSolicitud.Where(x => x.id_ref_pqrs_tipo_solicitud == obj.tipo_solicitud).FirstOrDefault();

                                    if (solicitud == null)
                                    {
                                        textError = columna + ", no existe este tipo solicitud";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("no existe este tipo solicitud"))
                                {
                                    textError = ex.Message;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                }
                                throw new Exception(textError);
                            }

                            columna = "Tipo categorización";
                            try
                            {
                                numero = Convert.ToInt32(item["Tipo categorización"]);
                                if (numero != null)
                                {
                                    obj.id_ref_categorizacon = Convert.ToInt32(item["Tipo categorización"]);
                                    Ref_PQRS_categorizacion categoria = new Ref_PQRS_categorizacion();
                                    categoria = categorizacion.Where(x => x.id_ref_categorizacon == obj.id_ref_categorizacon).FirstOrDefault();
                                    if (categoria == null)
                                    {
                                        textError = columna + ", no existe esta categorización";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("no existe esta categorización"))
                                {
                                    textError = ex.Message;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                }
                                throw new Exception(textError);
                            }

                            columna = "Regional";
                            try
                            {
                                if (regionalPropia != 0)
                                {
                                    obj.regional = regionalPropia;
                                }
                                else
                                {
                                    numero = Convert.ToInt32(item["Regional"]);
                                    if (numero != null)
                                    {

                                        obj.regional = Convert.ToInt32(item["Regional"]);

                                        Ref_regional region = new Ref_regional();
                                        region = regionales.Where(x => x.id_ref_regional == obj.regional).FirstOrDefault();
                                        if (region == null)
                                        {
                                            textError = columna + ", no existe esta regional";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacio";
                                        throw new Exception(textError);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("no existe esta regional"))
                                {
                                    textError = ex.Message;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                }
                                throw new Exception(textError);
                            }

                            columna = "Ciudad";
                            try
                            {
                                numero = Convert.ToInt32(item["Ciudad"]);

                                if (numero != null)
                                {
                                    obj.ciudad_caso_DANE = Convert.ToInt32(item["Ciudad"]);

                                    if (regionalPropia == 0)
                                    {
                                        ciudades = ciudades.Where(x => x.id_ref_regional == obj.regional).ToList();
                                    }

                                    Ref_ciudades ciudad = new Ref_ciudades();
                                    ciudad = ciudades.Where(x => x.Codigo_DANE == obj.ciudad_caso_DANE).FirstOrDefault();

                                    if (ciudad == null)
                                    {
                                        textError = columna + ", ciudad no corresponde a esta regional.";
                                        throw new Exception(textError);
                                    }
                                    else
                                    {
                                        obj.ciudad_del_caso = ciudad.id_ref_ciudades;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("ciudad no corresponde a esta regional"))
                                {
                                    textError = ex.Message;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                }
                                throw new Exception(textError);
                            }

                            columna = "SuperSalud";
                            try
                            {
                                numero = Convert.ToInt32(item["SuperSalud"]);
                                if (numero != null)
                                {
                                    obj.supersalud = Convert.ToInt32(item["SuperSalud"]);
                                    if (obj.supersalud != 1 && obj.supersalud != 0)
                                    {
                                        textError = columna + ", solo puede incluir 1 (SI) y 0 (NO)";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)

                            {
                                if (ex.Message.Contains("solo puede incluir 1 (SI) y 0 (NO)"))
                                {
                                    textError = ex.Message;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";

                                }
                                throw new Exception(textError);
                            }

                            columna = "Tipo identificación solicitante";
                            texto = Convert.ToString(item["Tipo identificación solicitante"]);
                            if (texto.Length <= 20)
                            {
                                obj.tipo_identi_solicitante = Convert.ToString(item["Tipo identificación solicitante"]).ToUpper();

                                Ref_tipo_documental documento = new Ref_tipo_documental();
                                documento = tipoDocumento.Where(x => x.id_ref_tipo_documental == obj.tipo_identi_solicitante).FirstOrDefault();

                                if (documento == null)
                                {
                                    textError = columna + ", no existe este tipo de documento";
                                    throw new Exception(textError);
                                }

                            }
                            else
                            {
                                textError = columna + ", solo puede contener 20 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre solicitante";
                            texto = Convert.ToString(item["Nombre solicitante"]);
                            if (texto.Length <= 50)
                            {
                                obj.solicitante_nombre = Convert.ToString(item["Nombre solicitante"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Número identificación solicitante";
                            texto = Convert.ToString(item["Número identificación solicitante"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50)
                                {
                                    obj.solicitante_numero_identi = Convert.ToString(item["Número identificación solicitante"]).ToUpper();
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Tipo identificación paciente";
                            texto = Convert.ToString(item["Tipo identificación paciente"]);
                            if (texto.Length <= 20)
                            {
                                obj.tipo_identificacion = Convert.ToString(item["Tipo identificación paciente"]).ToUpper();

                                Ref_tipo_documental documento = new Ref_tipo_documental();
                                documento = tipoDocumento.Where(x => x.id_ref_tipo_documental == obj.tipo_identificacion).FirstOrDefault();

                                if (documento == null)
                                {
                                    textError = columna + ", no existe este tipo de documento";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 20 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre paciente";
                            texto = Convert.ToString(item["Nombre paciente"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_paciente = Convert.ToString(item["Nombre paciente"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Número identificación paciente";
                            texto = Convert.ToString(item["Número identificación paciente"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50)
                                {
                                    obj.numero_identificacion = Convert.ToString(item["Número identificación paciente"]).ToUpper();
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Especificación solicitud";
                            texto = Convert.ToString(item["Especificación solicitud"]);
                            if (texto.Length <= 4500)
                            {
                                obj.especificacion_solicitud = Convert.ToString(item["Especificación solicitud"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 4500 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Solucionador";
                            texto = Convert.ToString(item["Solucionador"]);

                            if (solucionador != null)
                            {
                                obj.solucionador = solucionador.nombre_solucionador;
                            }
                            else
                            {
                                if (texto.Length <= 100)
                                {
                                    obj.solucionador = Convert.ToString(item["Solucionador"]).ToUpper();
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }
                            }

                            columna = "Auxiliar solucionador";
                            texto = Convert.ToString(item["Auxiliar solucionador"]);
                            if (texto.Length <= 100)
                            {


                                if (!string.IsNullOrEmpty(obj.auxiliar_solucionador))
                                {
                                    obj.auxiliar_solucionador = Convert.ToString(item["Auxiliar solucionador"]).ToUpper();
                                }
                                else
                                {
                                    obj.auxiliar_solucionador = "";
                                }


                                obj.solucionador = Convert.ToString(item["Solucionador"]).ToUpper();

                                ref_solucionador solu = new ref_solucionador();

                                if (obj.solucionador != "")
                                {
                                    if (obj.auxiliar_solucionador != "")
                                    {
                                        solu = solucionadorLis.Where(x => x.nombre_solucionador == obj.solucionador && x.auxiliar_solucionador == obj.auxiliar_solucionador).FirstOrDefault();

                                        if (solu == null)
                                        {
                                            textError = columna + ", este auxiliar solucionador no corresponde al solucionador colocado";
                                            throw new Exception(textError);
                                        }

                                        if (solu.nombre_solucionador == null)
                                        {
                                            textError = columna + ", este auxiliar solucionador no corresponde al solucionador colocado";
                                            throw new Exception(textError);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Otro solucionador";
                            texto = Convert.ToString(item["Otro solucionador"]);
                            if (texto.Length <= 100)
                            {
                                obj.otro_solucionador = Convert.ToString(item["Otro solucionador"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Otro auxiliar solucionador";
                            texto = Convert.ToString(item["Otro auxiliar solucionador"]);
                            if (texto.Length <= 100)
                            {
                                obj.otro_auxiliar_solucionador = Convert.ToString(item["Otro auxiliar solucionador"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            obj.estado_gestion = 1;
                            obj.fecha_asignacion = Convert.ToDateTime(DateTime.Now);
                            obj.tipo_ingreso = 3;
                            obj.conArchivo = 0;
                            obj.fecha_ingreso = DateTime.Now;
                            obj.usuario_ingreso = SesionVar.UserName;

                            Listado.Add(obj);
                            obj = new ecop_PQRS();
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        RtaInsercion = BusClass.CargueMedicamentosRegulados(Pqr, Listado, ref MsgRes);

                        mensajeLog = "Cargue completado";

                        logMas.id_cargue = RtaInsercion;
                        logMas.estado_cargue = mensajeLog;
                        logMas.registros_Cargados = Listado.Count();

                        var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);


                        return RtaInsercion;
                    }
                    else
                    {
                        var mensaje = "";
                        mensaje = "Hoja vacía.";
                        MsgRes.DescriptionResponse = mensaje;
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                        mensajeLog = "Error en el cargue";

                        logMas.id_cargue = RtaInsercion;
                        logMas.estado_cargue = mensajeLog;
                        logMas.registros_Cargados = Listado.Count();

                        var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                        return RtaInsercion;
                    }
                }

                catch (Exception ex)
                {
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                    if (textError != "" && textError != null)
                    {
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + textError;
                    }
                    else
                    {
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                    }
                    MsgRes.CodeError = ex.Message;

                    mensajeLog = "Error en el cargue";

                    logMas.id_cargue = RtaInsercion;
                    logMas.estado_cargue = mensajeLog;
                    logMas.registros_Cargados = Listado.Count();

                    var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);


                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                var mensaje = "";

                if (error.Contains("Valid worksheet names"))
                {
                    mensaje = "Error de nombre de hoja. El nombre debe ser: Cargue";
                    MsgRes.DescriptionResponse = mensaje;
                }
                else
                {
                    MsgRes.DescriptionResponse = ex.Message;
                }

                mensajeLog = "Error en el cargue";

                logMas.id_cargue = RtaInsercion;
                logMas.estado_cargue = mensajeLog;
                logMas.registros_Cargados = Listado.Count();

                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);
            }

            return RtaInsercion;
        }


        public int buscarNumeroCaso(string numeroCaso)
        {
            ecop_PQRS pqr = new ecop_PQRS();
            var respuesta = 0;
            try
            {
                pqr = BusClass.buscarNumeroCasoPqrs(numeroCaso);

                if (pqr != null)
                {
                    respuesta = 1;
                }

            }
            catch (Exception e)
            {
                var error = e.Message;
            }

            return respuesta;

        }

        #endregion
    }
}