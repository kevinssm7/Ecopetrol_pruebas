using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.CuentasMedicas;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Censo
{
    public class censo : BaseModelo
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

        private List<vw_datos_censo> _ListCensoD;
        public List<vw_datos_censo> ListCensoD
        {
            get
            {
                if (_ListCensoD == null)
                {
                    return _ListCensoD = BusClass.CensoDocumento(num_identifi_afil, ref MsgRes);
                }
                else
                {
                    return _ListCensoD;
                }

            }

            set
            {
                _ListCensoD = value;
            }
        }


        private List<vw_datos_censo> _ListCensoFac;
        public List<vw_datos_censo> ListCensoDFac
        {
            get
            {
                if (_ListCensoFac == null)
                {
                    _ListCensoFac = BusClass.CensoFacturas(ref MsgRes);
                    _ListCensoFac = _ListCensoFac.Where(x => x.fecha_egreso != null || x.fecha_egreso_censo != null).ToList();

                    _ListCensoFac = _ListCensoFac.Where(x => x.Numero_factura == null).ToList();

                    return _ListCensoFac;
                }
                else
                {
                    return _ListCensoFac;
                }

            }

            set
            {
                _ListCensoFac = value;
            }
        }

        private List<vw_datos_censo> _ListCensoN;
        public List<vw_datos_censo> ListCensoN
        {
            get
            {
                if (_ListCensoN == null)
                {
                    return _ListCensoN = BusClass.CensoId(id_censo, ref MsgRes);
                }
                else
                {
                    return _ListCensoN;
                }

            }

            set
            {
                _ListCensoN = value;
            }
        }

        private List<Ref_tipo_documental> _ListTipoDoc;
        public List<Ref_tipo_documental> ListTipoDoc
        {
            get
            {
                if (_ListTipoDoc == null)
                {
                    return _ListTipoDoc = BusClass.GetTipoDocumnetal();
                }
                else
                {
                    return _ListTipoDoc;
                }

            }

            set
            {
                _ListTipoDoc = value;
            }
        }

        private List<vw_sis_auditor_ciudad> _ListCiudadAuditor;
        public List<vw_sis_auditor_ciudad> ListCiudadAuditor
        {
            get
            {
                if (_ListCiudadAuditor == null)
                {
                    _ListCiudadAuditor = BusClass.GetCiudadesAuditor();
                    _ListCiudadAuditor = _ListCiudadAuditor.Where(x => x.id_auditor == SesionVar.IDUser).ToList();


                    return _ListCiudadAuditor;
                }
                else
                {
                    return _ListCiudadAuditor;
                }

            }

            set
            {
                _ListCiudadAuditor = value;
            }
        }

        private List<vw_ips_ciudad> _ListIPS;
        public List<vw_ips_ciudad> ListIPS

        {
            get
            {
                if (_ListIPS == null)
                {
                    _ListIPS = BusClass.GetIPS();
                    _ListIPS = _ListIPS.Where(x => x.usuario == Convert.ToString(SesionVar.UserName)).ToList();

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

        private List<Ref_ips> __ListIPSOK;
        public List<Ref_ips> ListIPSOK
        {
            get
            {
                if (__ListIPSOK == null)
                {
                    List<Ref_ips> lista = new List<Ref_ips>();
                    Ref_ips obj = new Ref_ips();

                    foreach (var item in ListCiudadAuditor)
                    {
                        __ListIPSOK = BusClass.GetPrstador();
                        __ListIPSOK = __ListIPSOK.Where(x => x.id_ref_ciudades == item.id_ciudad).ToList();

                        foreach (var item2 in __ListIPSOK)
                        {
                            obj.id_ref_ips = Convert.ToInt32(item2.id_ref_ips);
                            obj.Nombre = Convert.ToString(item2.Nombre);
                            obj.Nit = Convert.ToString(item2.Nit);
                            obj.id_ref_ciudades = Convert.ToInt32(item2.id_ref_ciudades);


                            lista.Add(obj);
                            obj = new Ref_ips();
                        }


                    }
                    __ListIPSOK = lista;

                }

                return __ListIPSOK;
            }

            set
            {
                __ListIPSOK = value;
            }
        }


        private List<ref_censo_caso_especial> _ListCasoEspecial;
        public List<ref_censo_caso_especial> ListCasoEspecial
        {
            get
            {
                if (_ListCasoEspecial == null)
                {

                    List<ref_censo_caso_especial> lista = new List<ref_censo_caso_especial>();
                    lista = BusClass.listaCensoCasosEspeciales();

                    _ListCasoEspecial = lista;
                    return _ListCasoEspecial;
                }
                else
                {
                    return _ListCasoEspecial;
                }
            }
            set
            {
                _ListCasoEspecial = value;
            }
        }


        private List<ref_censo_linea_pago> _ListLineasPago;
        public List<ref_censo_linea_pago> ListLineasPago
        {
            get
            {
                if (_ListCasoEspecial == null)
                {

                    List<ref_censo_linea_pago> lista = new List<ref_censo_linea_pago>();
                    lista = BusClass.listaCensoLineasPago();

                    _ListLineasPago = lista;
                    return _ListLineasPago;
                }
                else
                {
                    return _ListLineasPago;
                }
            }
            set
            {
                _ListLineasPago = value;
            }
        }


        private List<vw_sis_auditor_regional> _ListAuditor;
        public List<vw_sis_auditor_regional> ListAuditor
        {
            get
            {
                if (_ListAuditor == null)
                {
                    List<vw_ciudad_auditor> Lista = new List<vw_ciudad_auditor>();
                    Lista = BusClass.GetCiudad_auditor();
                    //Lista = Lista.Where(x => x.usuario == Convert.ToString(SesionVar.UserName)).ToList();
                    List<vw_ciudad_auditor> List = new List<vw_ciudad_auditor>();


                    List<vw_ciudad_auditor> Lista2 = new List<vw_ciudad_auditor>();
                    List<vw_ciudad_auditor> ListaAuditor = new List<vw_ciudad_auditor>();

                    //

                    foreach (var item in ListaAuditor)
                    {

                        vw_ciudad_auditor obj = new vw_ciudad_auditor();

                        obj.id_ciudad = item.id_ciudad.Value;

                        List.Add(obj);
                    }


                    return _ListAuditor;
                }
                else
                {
                    return _ListAuditor;
                }

            }

            set
            {
                _ListAuditor = value;
            }

        }

        private List<vw_auditores_totales> _ListAuditorOK;
        public List<vw_auditores_totales> ListAuditorOK
        {
            get
            {
                List<vw_auditores_totales> listaAuditores = new List<vw_auditores_totales>();

                List<vw_auditores_totales> Lista3 = new List<vw_auditores_totales>();
                List<sis_auditor_regional> list = new List<sis_auditor_regional>();

                listaAuditores = BusClass.GetAuditorTotales(ref MsgRes);

                regional obj = new regional();

                list = BusClass.GetRegionalAuditor();
                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                foreach (var item in list)
                {
                    List<vw_auditores_totales> listaAuditores2 = new List<vw_auditores_totales>();

                    listaAuditores2 = listaAuditores.Where(l => l.id_regional == item.id_regional).ToList();
                    Lista3 = Lista3.Concat(listaAuditores2).ToList();
                }

                _ListAuditorOK = Lista3;

                return _ListAuditorOK;
            }
            set
            {
                _ListAuditorOK = value;
            }
            //get
            //{
            //    if (_ListAuditorOK == null)
            //    {
            //        List<vw_sis_auditor_regional> lista = new List<vw_sis_auditor_regional>();
            //        vw_sis_auditor_regional obj = new vw_sis_auditor_regional();

            //        foreach (var item in ListCiudadAuditor)
            //        {
            //            _ListAuditorOK = BusClass.GetVWRegionalAuditor();
            //            _ListAuditorOK = _ListAuditorOK.Where(x => x.id_regional == item.id_ref_regional).ToList();

            //            foreach (var item2 in _ListAuditorOK)
            //            {
            //                obj.id_auditor = Convert.ToInt32(item2.id_auditor);
            //                obj.nombre = Convert.ToString(item2.nombre);
            //                obj.id_regional = Convert.ToInt32(item2.id_regional);


            //                lista.Add(obj);
            //                obj = new vw_sis_auditor_regional();
            //            }
            //        }
            //        _ListAuditorOK = lista;
            //    }
            //    return _ListAuditorOK;
            //}
        }

        private List<vw_tablero_censo> _ListTablerosCenso;
        public List<vw_tablero_censo> ListTablerosCenso
        {
            get
            {
                if (_ListTablerosCenso == null)
                {
                    return _ListTablerosCenso = BusClass.GetTableroCenso();
                }
                else
                {
                    return _ListTablerosCenso;
                }
            }

            set
            {
                _ListTablerosCenso = value;
            }
        }

        private List<Ref_tipo_ingreso> _ListTipoIngreso;
        public List<Ref_tipo_ingreso> ListTipoIngreso
        {
            get
            {

                if (_ListTipoIngreso == null)
                {
                    _ListTipoIngreso = BusClass.GetTipoIngreso();
                    _ListTipoIngreso = _ListTipoIngreso.Where(x => x.estado == "A").ToList();

                    return _ListTipoIngreso;
                }
                else
                {
                    return _ListTipoIngreso;
                }

            }

            set
            {
                _ListTipoIngreso = value;
            }
        }

        private List<Ref_origen_evento> _ListOrigenEvento;
        public List<Ref_origen_evento> ListOrigenEvento
        {
            get
            {

                if (_ListOrigenEvento == null)
                {
                    return _ListOrigenEvento = BusClass.GetOrigenEvento();
                }
                else
                {
                    return _ListOrigenEvento;
                }

            }

            set
            {
                _ListOrigenEvento = value;
            }
        }

        private List<Ref_tipo_habitacion> _ListTipoHabitacion;
        public List<Ref_tipo_habitacion> ListTipoHabitacion
        {
            get
            {
                if (_ListTipoHabitacion == null)
                {
                    _ListTipoHabitacion = BusClass.GetTipoHabitacion();
                    _ListTipoHabitacion = _ListTipoHabitacion.Where(x => x.estado == "A").ToList();

                    return _ListTipoHabitacion;
                }
                else
                {
                    return _ListTipoHabitacion;
                }

            }

            set
            {
                _ListTipoHabitacion = value;
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

        private List<Ref_condicion_alta_censo> _ListCondicionAlta;
        public List<Ref_condicion_alta_censo> ListCondicionAlta
        {
            get
            {
                if (_ListCondicionAlta == null)
                {
                    return _ListCondicionAlta = BusClass.GetCondicionAlta();
                }
                else
                {
                    return _ListCondicionAlta;
                }

            }

            set
            {
                _ListCondicionAlta = value;
            }
        }


        private List<vw_cie10> _ListCie10;
        public List<vw_cie10> ListCie10
        {
            get
            {
                if (_ListCie10 == null)
                {
                    return _ListCie10 = BusClass.GetCie10Unido();
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

        private List<VW_base_beneficiarios> _ListBase;
        public List<VW_base_beneficiarios> ListBase
        {
            get
            {
                if (_ListBase == null || _ListBase.Count == 0)
                {
                    _ListBase = BusClass.BeneficiariosDocumento(num_identifi_afil, ref MsgRes).OrderByDescending(x => x.id_base_beneficiarios).Take(1).ToList();
                    return _ListBase;
                }
                else
                {
                    return _ListBase;
                }

            }

            set
            {
                _ListBase = value;
            }
        }


        private List<ecop_censo> _ListCenso;
        public List<ecop_censo> ListCenso
        {
            get
            {
                if (_ListCenso == null)
                {
                    return _ListCenso = new List<ecop_censo>();
                }
                else
                {
                    return _ListCenso;
                }

            }

            set
            {
                _ListCenso = value;
            }
        }

        public Int32 id_censo { get; set; }

        public int nombre_equipodeauditoria { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Fecha recepción:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? fecha_recepcion_censo { get; set; }

        public Int32 nit_ips { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Ips:")]
        public String ips_primaria { get; set; }
        public int id_ref_ciudades { get; set; }

        public String ips_primaria2 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Sucursal:")]
        public int sucursal { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Tipo documento:")]
        public String tipo_identifi_afiliado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Número documento:")]
        public String num_identifi_afil { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "Primer apellido:")]
        public String primer_apellido { get; set; }

        [Display(Name = "Segundo apellido:")]
        public String segundo_apellido { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Primer nombre:")]
        public String primer_nombre { get; set; }

        [Display(Name = "Segundo nombre:")]
        public String segundo_nombre { get; set; }

        [Display(Name = "Edad:")]
        public int edad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Fecha nacimiento:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? fecha_nacimiento { get; set; }

        public DateTime? fecha_nacimientook { get; set; }


        public DateTime fecha_actual = Convert.ToDateTime(DateTime.Now);


        [Required(ErrorMessage = "***")]
        [Display(Name = "Género:")]
        public String genero { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "Fecha ingreso:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_ingreso { get; set; }


        public DateTime? fecha_ingresook { get; set; }

        public Nullable<DateTime> fecha_recepcion_censoOK { get; set; }

        public String edadok { get; set; }



        [Required(ErrorMessage = "***")]
        [Display(Name = "Fecha egreso:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fecha_egreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Fecha egreso:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_egreso_censo { get; set; }

        public DateTime? fecha_egreso_censook { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Condición alta:")]
        public int condicion_alta { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "Tipo de habitación:")]
        public int tipo_habitacion { get; set; }


        [Display(Name = "Dias de estancia:")]
        public int dias_estancia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Cie10 diagnostico:")]
        public String cie10dx_actual { get; set; }



        [Required(ErrorMessage = "***")]
        [Display(Name = "Tipo de ingreso:")]
        public int tipo_ingreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Diagnostico:")]
        public String dx_actual { get; set; }
        public String dx_actualok { get; set; }


        [Display(Name = "Cie10 diagnostico egreso:")]
        public String cie10dx_egreso { get; set; }


        [Display(Name = "Diagnostico egreso:")]
        public String dx_egreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Origen evento:")]
        public int origen_evento { get; set; }

        public String Log_registros { get; set; }

        public String plan_egreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Valor egreso:")]
        public Int64 valor_egreso { get; set; }

        [Display(Name = "Médico auditor")]
        public int id_medico_auditor { get; set; }

        public DateTime digita_fecha { get; set; }

        public String digita_usuario { get; set; }

        [Display(Name = "Incapacidad:")]
        public String incapacidad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Número factura:")]
        public String Numero_factura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Fecha factura:")]
        public DateTime? fecha_factura { get; set; }

        public DateTime? fecha_facturaok { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "Fecha recepción factura:")]
        public DateTime? fecha_recepcion_factura { get; set; }
        public DateTime? fecha_recepcion_facturaok { get; set; }


        public String Items { get; set; }

        public List<vw_datos_censo> std { get; set; }

        //public List<vw_tablero_censo> Lista { get; set; }
        public List<management_vw_tablero_censoResult> Lista { get; set; }
        public List<vw_tablero_censo2> Lista2 { get; set; }


        [Display(Name = "Eegreso")]
        public string egreso { get; set; }

        public string regional_beneficiario { get; set; }


        [DataType(DataType.Date)]
        public DateTime? achPayDate { get; set; }

        public string ciudad { get; set; }

        public int? BB { get; set; }

        public string TI { get; set; }


        public int caso_Especial { get; set; }

        public int caso_Especial_detalle { get; set; }

        public int linea_pago { get; set; }


        public string mensajeIngresoFacturasDetalle { get; set; }

        public int rtaIngresoFacturasDetalle { get; set; }

        public int id_registro { get; set; }



        public int? idLote { get; set; }
        public int? idDetalle { get; set; }
        public int? tipoHabitacion { get; set; }
        public int? lineaPago { get; set; }
        public string cie10 { get; set; }
        public string cie10Des { get; set; }
        public int? caso_inferior_72horas { get; set; }
        public string notas { get; set; }


        #endregion

        #region FUNCIONES


        public Int32 InsertarCenso(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarCenso(OBJCenso, ref MsgRes);
        }

        public void ConsultaCenso(Int32 idCenso)
        {
            ecop_censo ObjCenso = new ecop_censo();
            ObjCenso.id_censo = idCenso;
            ListCenso = BusClass.GetCensoId(idCenso, ref MsgRes);
            var tipoHabitacion = 0;

            foreach (var item in ListCenso)
            {
                idCenso = item.id_censo;
                ips_primaria = item.ips_primaria;
                ips_primaria2 = item.ips_primaria;

                tipo_identifi_afiliado = item.tipo_identifi_afiliado;
                num_identifi_afil = item.num_identifi_afil;
                primer_apellido = item.primer_apellido;
                segundo_apellido = item.segundo_apellido;
                primer_nombre = item.primer_nombre;
                segundo_nombre = item.segundo_nombre;

                fecha_nacimiento = Convert.ToDateTime(item.fecha_nacimiento);

                fecha_ingreso = Convert.ToDateTime(item.fecha_ingreso);
                fecha_egreso = Convert.ToDateTime(item.fecha_egreso);

                genero = item.genero;
                edad = Convert.ToInt32(item.edad);
                tipo_habitacion = Convert.ToInt32(item.tipo_habitacion);

                //tipoHabitacion = (int)item.tipo_habitacion;
                //if (tipoHabitacion == 4 ||
                //    tipoHabitacion == 5 ||
                //    tipoHabitacion == 6 ||
                //    tipoHabitacion == 7 ||
                //    tipoHabitacion == 8 ||
                //    tipoHabitacion == 22 ||
                //    tipoHabitacion == 23 ||
                //    tipoHabitacion == 24)
                //{
                //    fecha_recepcion_censo = Convert.ToDateTime(item.fecha_recepcion_censo).AddDays(3);
                //}
                //else
                //{
                if (Convert.ToDateTime(item.fecha_recepcion_censo).Date >= DateTime.Now.Date)
                {
                    fecha_recepcion_censo = Convert.ToDateTime(item.fecha_recepcion_censo);

                }
                else
                {
                    fecha_recepcion_censo = Convert.ToDateTime(item.fecha_recepcion_censo).AddDays(1);
                }

                //}

                DateTime dia1 = Convert.ToDateTime(item.fecha_ingreso);
                DateTime dia2 = Convert.ToDateTime(DateTime.Now);

                TimeSpan ts = dia2 - dia1;

                int differenceInDays = ts.Days;
                dias_estancia = differenceInDays;


                //dias_estancia = Convert.ToInt32(item.dias_estancia);
                tipo_ingreso = Convert.ToInt32(item.tipo_ingreso);


                dx_actual = item.dx_actual;
                dx_actualok = item.dx_actual;
                origen_evento = Convert.ToInt32(item.origen_evento);
                id_medico_auditor = item.id_medico_auditor;
                condicion_alta = Convert.ToInt32(item.condicion_alta);
                valor_egreso = Convert.ToInt64(item.valor_egreso);

                Numero_factura = item.Numero_factura;
                incapacidad = item.incapacidad;
                dx_egreso = item.dx_egreso;
                cie10dx_actual = item.cie10dx_actual;
                cie10dx_actual = item.cie10dx_actual;

                if (item.linea_pago != null)
                {
                    linea_pago = (int)item.linea_pago;
                }

                if (item.caso_Especial != null)
                {
                    caso_Especial = (int)item.caso_Especial;

                    if (caso_Especial != 0)
                    {
                        caso_Especial_detalle = (int)item.caso_Especial_detalle;
                    }
                }

                if (item.fecha_egreso != null)
                {
                    egreso = "SI";

                }
                else
                {
                    egreso = "NO";
                }

            }

        }

        public void ActualizarCenso(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarCenso(OBJCenso, ref MsgRes);
        }

        public void ActualizarCensoEgreso(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarCensoEgreso(OBJCenso, ref MsgRes);
        }

        public void CensoEgreso(ref MessageResponseOBJ MsgRes)
        {
            BusClass.CensoEgreso(OBJCenso, ref MsgRes);
        }
        //public List<vw_tablero_censo> GetLista()
        //{
        //    return BusClass.GetTableroCenso();
        //}


        public List<management_vw_tablero_censoResult> GetLista()
        {
            return BusClass.GetTableroCensoCompleto();
        }


        public List<vw_tablero_censo2> GetLista2()
        {
            return BusClass.GetTableroCenso2();
        }


        public string ExcelMasivoAHCenso(DataTable dt2, cargue_censo_ah_lote lote, ref MessageResponseOBJ MsgRes)
        {
            List<cargue_censo_ah_registros> Listado = new List<cargue_censo_ah_registros>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        cargue_censo_ah_registros obj = new cargue_censo_ah_registros();

                        fila++;
                        columna = "Tipo de documento";

                        if (!string.IsNullOrEmpty(item["Tipo de documento"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "Tipo de documento";
                            try
                            {
                                texto = Convert.ToString(item["Tipo de documento"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.tipo_documento = Convert.ToString(item["Tipo de documento"]);
                                }
                                else
                                {
                                    obj.tipo_documento = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Numero documento";
                            try
                            {
                                texto = Convert.ToString(item["Numero documento"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.numero_documento = Convert.ToString(item["Numero documento"]);
                                }
                                else
                                {
                                    obj.numero_documento = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Genero";
                            try
                            {
                                texto = Convert.ToString(item["Genero"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.genero = Convert.ToString(item["Genero"]);
                                }
                                else
                                {
                                    obj.genero = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Primer apellido";
                            try
                            {
                                texto = Convert.ToString(item["Primer apellido"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.primer_apellido = Convert.ToString(item["Primer apellido"]);
                                }
                                else
                                {
                                    obj.primer_apellido = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Segundo apellido";
                            try
                            {
                                texto = Convert.ToString(item["Segundo apellido"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.segundo_apellido = Convert.ToString(item["Segundo apellido"]);
                                }
                                else
                                {
                                    obj.segundo_apellido = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Primer nombre";
                            try
                            {
                                texto = Convert.ToString(item["Primer nombre"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.primer_nombre = Convert.ToString(item["Primer nombre"]);
                                }
                                else
                                {
                                    obj.primer_nombre = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }


                            columna = "Segundo nombre";
                            try
                            {
                                texto = Convert.ToString(item["Segundo nombre"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.segundo_nombre = Convert.ToString(item["Segundo nombre"]);
                                }
                                else
                                {
                                    obj.segundo_nombre = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }


                            columna = "Fecha de nacimiento";
                            try
                            {
                                var datoPrueba = item["Fecha de nacimiento"];

                                if (datoPrueba != null)
                                {
                                    string fechaTexto = datoPrueba.ToString().Trim();
                                    fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                    if (!string.IsNullOrEmpty(fechaTexto))
                                    {
                                        // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                        string[] formatos = {
                                        "dd/MM/yyyy",
                                        //"MM/dd/yyyy"
                                        };

                                        DateTime fechaEspecial;

                                        // Probar los diferentes formatos
                                        bool fechaValida = DateTime.TryParseExact(
                                            fechaTexto,
                                            formatos,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                            out fechaEspecial
                                        );

                                        if (fechaValida)
                                        {
                                            obj.fecha_nacimiento = fechaEspecial;
                                        }
                                        else
                                        {
                                            textError = columna + ", tiene formato incorrecto. Debe ser dd/MM/yyyy";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacío";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            columna = "Regional";
                            try
                            {
                                texto = Convert.ToString(item["Regional"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.regional = Convert.ToString(item["Regional"]);
                                }
                                else
                                {
                                    obj.regional = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "UNIS";
                            try
                            {
                                texto = Convert.ToString(item["UNIS"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.unis = Convert.ToString(item["UNIS"]);
                                }
                                else
                                {
                                    obj.unis = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Municipio";
                            try
                            {
                                texto = Convert.ToString(item["Municipio"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.municipio = Convert.ToString(item["Municipio"]);
                                }
                                else
                                {
                                    obj.municipio = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "IPS";
                            try
                            {
                                texto = Convert.ToString(item["IPS"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.ips = Convert.ToString(item["IPS"]);
                                }
                                else
                                {
                                    obj.ips = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Edad";
                            try
                            {
                                texto = Convert.ToString(item["Edad"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.edad = Convert.ToInt32(item["Edad"]);
                                }
                                else
                                {
                                    obj.edad = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Fecha de ingreso";
                            try
                            {
                                var datoPrueba = item["Fecha de ingreso"];

                                if (datoPrueba != null)
                                {
                                    string fechaTexto = datoPrueba.ToString().Trim();
                                    fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                    if (!string.IsNullOrEmpty(fechaTexto))
                                    {
                                        // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                        string[] formatos = {
                                        "dd/MM/yyyy",
                                        };

                                        DateTime fechaEspecial;

                                        // Probar los diferentes formatos
                                        bool fechaValida = DateTime.TryParseExact(
                                            fechaTexto,
                                            formatos,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                            out fechaEspecial
                                        );

                                        if (fechaValida)
                                        {
                                            obj.fecha_ingreso = fechaEspecial;
                                        }
                                        else
                                        {
                                            textError = columna + ", tiene formato incorrecto. Debe ser dd/MM/yyyy";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacío";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }


                            columna = "Fecha de egreso";
                            try
                            {
                                var datoPrueba = item["Fecha de egreso"];

                                if (datoPrueba != null)
                                {
                                    string fechaTexto = datoPrueba.ToString().Trim();
                                    fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                    if (!string.IsNullOrEmpty(fechaTexto))
                                    {
                                        // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                        string[] formatos = {
                                        "dd/MM/yyyy",
                                        };

                                        DateTime fechaEspecial;

                                        // Probar los diferentes formatos
                                        bool fechaValida = DateTime.TryParseExact(
                                            fechaTexto,
                                            formatos,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                            out fechaEspecial
                                        );

                                        if (fechaValida)
                                        {
                                            obj.fecha_egreso = fechaEspecial;
                                        }
                                        else
                                        {
                                            textError = columna + ", tiene formato incorrecto. Debe ser dd/MM/yyyy";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacío";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }


                            columna = "Tipo de ingreso";
                            try
                            {
                                texto = Convert.ToString(item["Tipo de ingreso"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.tipo_ingreso = Convert.ToString(item["Tipo de ingreso"]);
                                }
                                else
                                {
                                    obj.tipo_ingreso = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Origen del evento";
                            try
                            {
                                texto = Convert.ToString(item["Origen del evento"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.origen_evento = Convert.ToString(item["Origen del evento"]);
                                }
                                else
                                {
                                    obj.origen_evento = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Caso especial";
                            try
                            {
                                texto = Convert.ToString(item["Caso especial"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.caso_especial = Convert.ToString(item["Caso especial"]);
                                }
                                else
                                {
                                    obj.caso_especial = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Tipo de caso";
                            try
                            {
                                texto = Convert.ToString(item["Tipo de caso"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.tipo_caso = Convert.ToString(item["Tipo de caso"]);
                                }
                                else
                                {
                                    obj.tipo_caso = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Diagnostico CIE10";
                            try
                            {
                                texto = Convert.ToString(item["Diagnostico CIE10"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.cie10 = Convert.ToString(item["Diagnostico CIE10"]);
                                }
                                else
                                {
                                    obj.cie10 = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }


                            columna = "Diagnostico descripción";
                            try
                            {
                                texto = Convert.ToString(item["Diagnostico descripción"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.decripcion_cie10 = Convert.ToString(item["Diagnostico descripción"]);
                                }
                                else
                                {
                                    obj.decripcion_cie10 = "";
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Listado.Add(obj);
                            obj = new cargue_censo_ah_registros();
                        }
                        else
                        {
                            throw new Exception("ID detalle no puede ir vacio");
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        id_registro = BusClass.InsertarCargueAHCenso(lote, Listado, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensajeIngresoFacturasDetalle = "SE INGRESARON CORRECTAMENTE LOS REGISTROS";
                            rtaIngresoFacturasDetalle = 1;
                        }
                        else
                        {
                            mensajeIngresoFacturasDetalle = "ERROR EN EL INGRESO: " + MsgRes.DescriptionResponse;
                            rtaIngresoFacturasDetalle = 2;
                        }

                        return mensajeIngresoFacturasDetalle;
                    }
                    else
                    {
                        mensajeIngresoFacturasDetalle = "Hoja vacía.";
                        rtaIngresoFacturasDetalle = 2;
                        return mensajeIngresoFacturasDetalle;
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
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna + " - " + ex.Message;
                    }

                    MsgRes.CodeError = ex.Message;

                    mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                    rtaIngresoFacturasDetalle = 2;
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

                mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                rtaIngresoFacturasDetalle = 2;
            }

            return mensajeIngresoFacturasDetalle;
        }

        #endregion
    }


    public class AlertaEpidemiologica : BaseModelo
    {
        #region PROPIEDADES
        public int id_gestionAnalisis { get; set; }
        public int id_gestion { get; set; }
        public DateTime fecha_analisis { get; set; }
        public string nombres_beneficiario { get; set; }
        public string tipo_beneficiario { get; set; }
        public string identificacion_beneficiario { get; set; }
        public DateTime fecha_inicio_servicio { get; set; }
        public int? edad_momento_evento { get; set; }
        public string tipo_evento { get; set; }
        public DateTime fecha_ocurrencia_evento { get; set; }
        public DateTime fecha_fin_evento { get; set; }
        public string fuente_reporte { get; set; }
        public string nombre_reportante { get; set; }
        public string nombre_prestador_ocurre_evento { get; set; }
        public string nit_prestador { get; set; }
        public string profesionales_entidadesresponsables_nivel1 { get; set; }
        public string diagnosticos { get; set; }
        public string objetivo_auditoria { get; set; }
        public string descripcion_evento { get; set; }
        public string ambito_sucedio_evento { get; set; }
        public string analisis_critico_caso { get; set; }
        public string concepto_resolutividad_primer_nivel { get; set; }
        public string aplicacion_guias_terapeuticas { get; set; }
        public string periodicidad_controles_medicos { get; set; }
        public string aplicacion_pruebas_diagnosticos_monitoreo { get; set; }
        public string informacion_paciente_cuidadores_plan_terapeutico { get; set; }
        public string descripcion_eventuales_Causasmuerte_relacionadas { get; set; }
        public string conclusiones_analisis_caso { get; set; }
        public string concepto_auditoria_evento_prevenible { get; set; }
        public string propuesta_acciones_mejora { get; set; }
        public string problemasGestionDemoras { get; set; }

        #endregion PROPIEDADES

        #region FUNCIONES

        public List<alerta_epidemiologica_gestion_gestionAnalisis_demoras> ArreglarListadoDemoras(int? idGestionAnalisis, string gestionDemoras, string usuario, int? tipoIngreso)
        {
            //tipoIngreso 1: guardado total
            //tipoIngreso 2: guardado parcial

            List<alerta_epidemiologica_gestion_gestionAnalisis_demoras> listado = new List<alerta_epidemiologica_gestion_gestionAnalisis_demoras>();
            try
            {
                string[] gestiones = gestionDemoras.Split('|');

                if (gestiones.Count() == 0)
                {
                    throw new Exception("no se pudieron gestionar las demoras");
                }
                else
                {
                    for (var i = 0; i < gestiones.Count(); i++)
                    {
                        string[] resultadoGestiones = gestiones[i].Split('-');
                        if (resultadoGestiones.Count() == 0)
                        {
                            throw new Exception("No se pudieron gestionar los detalles de las demoras");
                        }
                        else
                        {
                            alerta_epidemiologica_gestion_gestionAnalisis_demoras demora = new alerta_epidemiologica_gestion_gestionAnalisis_demoras()
                            {
                                id_gestionAnalisis = idGestionAnalisis,
                                id_ref = Convert.ToInt32(resultadoGestiones[0]),
                                id_refDetallado = Convert.ToInt32(resultadoGestiones[1]),
                                respuesta_demora = resultadoGestiones[2],
                                observaciones = resultadoGestiones[3],
                                fecha_digita = DateTime.Now,
                                usuario_digita = usuario,
                                estado = tipoIngreso == 1 ? 1 : 0
                            };

                            if (demora != null)
                            {
                                listado.Add(demora);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                problemasGestionDemoras = error;
                listado = new List<alerta_epidemiologica_gestion_gestionAnalisis_demoras>();
            }

            return listado;
        }

        #endregion FUNCIONES

    }

}