using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Concurrencia
{
    public class Concurrencia
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

        private ecop_concurrencia _OBJConcurencia;
        public ecop_concurrencia OBJConcurencia
        {
            get
            {
                if (_OBJConcurencia == null)
                {
                    _OBJConcurencia = new ecop_concurrencia();
                }

                return _OBJConcurencia;
            }
            set { _OBJConcurencia = value; }
        }

        private List<ecop_concurrencia> _LstConcurencia;
        public List<ecop_concurrencia> LstConcurencia
        {
            get
            {
                if (_LstConcurencia == null)
                {
                    _LstConcurencia = new List<ecop_concurrencia>();
                }

                return _LstConcurencia;
            }
            set { _LstConcurencia = value; }
        }

        private List<Ref_tipo_documental> _LstTipoDocumento;
        public List<Ref_tipo_documental> LstTipoDocumento
        {
            get
            {
                if (_LstTipoDocumento == null)
                {
                    _LstTipoDocumento = BusClass.GetTipoDocumnetal();
                }

                return _LstTipoDocumento;
            }
            set { _LstTipoDocumento = value; }
        }

        private List<vw_ips_ciudad> _ListIPS;
        public List<vw_ips_ciudad> ListIPS
        {

            get
            {
                if (_ListIPS == null)
                {
                    _ListIPS = BusClass.GetIPS();
                    //  _ListIPS = _ListIPS.Where(x => x.usuario == Convert.ToString(SesionVar.UserName)).ToList();

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

        private List<vw_ciudad_auditor> _ListCiudadAuditor;
        public List<vw_ciudad_auditor> ListCiudadAuditor
        {
            get
            {
                if (_ListCiudadAuditor == null)
                {
                    _ListCiudadAuditor = BusClass.GetCiudad_auditor();

                    _ListCiudadAuditor = _ListCiudadAuditor.Where(x => x.usuario == Convert.ToString(SesionVar.UserName)).ToList();
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

        private List<Ref_ciudades> _ListCiudades;

        public List<Ref_ciudades> ListCiudades
        {
            get
            {
                if (_ListCiudades == null)
                {
                    return _ListCiudades = BusClass.GetCiudades();
                }
                else
                {
                    return _ListCiudades;
                }

            }

            set
            {
                _ListCiudades = value;
            }
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


                    if(afi_Edad >= 18)
                    {
                        _LstServicioTratante = _LstServicioTratante.Where(x => !x.des.Contains("PEDIATRIA")).ToList();
                    }
                }

                return _LstServicioTratante;
            }
            set { _LstServicioTratante = value; }
        }


        private List<Ref_salud_publica> _LstSaludPublica;
        public List<Ref_salud_publica> LstSaludPublica
        {
            get
            {
                if (_LstSaludPublica == null)
                {
                    _LstSaludPublica = BusClass.GetSaludPublica();
                    _LstSaludPublica = _LstSaludPublica.Where(x => x.orden != null).ToList();
                    _LstSaludPublica = _LstSaludPublica.OrderBy(x => x.orden).ToList();

                    return _LstSaludPublica;
                }
                else
                {
                    return _LstSaludPublica;
                }

            }

            set
            {
                _LstSaludPublica = value;
            }
        }

        private List<Ref_origen_hospitalizacion> _LstOrigenHospitalizacion;
        public List<Ref_origen_hospitalizacion> LstOrigenHospitalizacion
        {
            get
            {
                if (_LstOrigenHospitalizacion == null)
                {
                    _LstOrigenHospitalizacion = BusClass.GetOrigenHospitalizacion();
                    _LstOrigenHospitalizacion = _LstOrigenHospitalizacion.OrderBy(x => x.descripcion).ToList();

                    return _LstOrigenHospitalizacion;
                }
                else
                {
                    return _LstOrigenHospitalizacion;
                }

            }

            set
            {
                _LstOrigenHospitalizacion = value;
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


        private List<Ref_lesiones_severas_y_alto_costo> _LstAltoCosto_Consulta;
        public List<Ref_lesiones_severas_y_alto_costo> LstAltoCosto_consulta
        {
            get
            {
                if (_LstAltoCosto_Consulta == null)
                {
                    return _LstAltoCosto_Consulta = BusClass.GetAltoCosto();

                }
                else
                {
                    return _LstAltoCosto_Consulta;
                }

            }

            set
            {
                _LstAltoCosto_Consulta = value;
            }
        }

        private List<vw_tablero_concurrencia> _GetLista;
        public List<vw_tablero_concurrencia> GetLista
        {
            get
            {
                if (_GetLista == null)
                {
                    if (SesionVar.ROL == "1" || SesionVar.ROL == "14" || SesionVar.ROL == "2" || SesionVar.ROL == "9")
                    {
                        _GetLista = BusClass.GetTableroConcurrencia();
                        _GetLista = _GetLista.OrderBy(X => X.fecha_ingreso).ToList();
                    }
                    else
                    {
                        _GetLista = BusClass.GetTableroConcurrencia();
                        _GetLista = _GetLista.Where(x => x.usuario == SesionVar.UserName).ToList();
                        _GetLista = _GetLista.OrderBy(X => X.fecha_ingreso).ToList();
                    }
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

        private List<vw_tablero_concurrencia> _GetListaTrabajador;
        public List<vw_tablero_concurrencia> GetListaTrabajador
        {
            get
            {
                if (_GetListaTrabajador == null)
                {


                    _GetListaTrabajador = BusClass.GetTableroConcurrencia();

                    _GetListaTrabajador = _GetListaTrabajador.Where(x => x.id_concurrencia == id_concurrencia).ToList();

                    return _GetListaTrabajador;

                }
                else
                {
                    return _GetListaTrabajador;
                }

            }

            set
            {
                _GetLista = value;
            }
        }


        private List<VW_base_beneficiarios> _ListBase;
        public List<VW_base_beneficiarios> ListBase
        {
            get
            {
                if (_ListBase == null || _ListBase.Count == 0)
                {
                    return _ListBase = BusClass.BeneficiariosDocumento(afi_NumDoc, ref MsgRes);
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

        private List<Ref_motivo_reingreso> _ListMotivoReingreso;
        public List<Ref_motivo_reingreso> ListMotivoReingreso
        {
            get
            {
                if (_ListMotivoReingreso == null)
                {
                    return _ListMotivoReingreso = BusClass.GetRefMotivoRiesgo();
                }
                else
                {
                    return _ListMotivoReingreso;
                }

            }

            set
            {
                _ListMotivoReingreso = value;
            }
        }



        public Int32 id_concurrencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AFILIADO TIPO DOCUMENTO")]
        public string afi_tipoDoc { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[0-9]{1,16}$", ErrorMessage = "Solo Numeros Max.16 digitos")]
        [Display(Name = "AFILIADO NO. DOCUMENTO")]
        public string afi_NumDoc { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100 caracteres")]
        [Display(Name = "AFIILIADO NOMBRE")]
        public string afi_Nom { get; set; }

        [Required(ErrorMessage = "***")]
        [Range(typeof(int), "0", "110", ErrorMessage = "Rango 0-110 años")]
        [Display(Name = "AFIILIADO EDAD")]
        public Int32 afi_Edad { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100 caracteres")]
        [Display(Name = "AFILIADO DIRECCIÓN")]
        public string afi_Dir { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,20}$", ErrorMessage = "Maximo 20 caracteres")]
        [Display(Name = "AFIILIADO TELEFONO1")]
        public string afi_tel1 { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,20}$", ErrorMessage = "Maximo 20 caracteres")]
        [Display(Name = "AFIILIADO CELULAR")]
        public string afi_cel { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,100}$", ErrorMessage = "Maximo 100 caracteres")]
        [Display(Name = "NOMBRE DEL CONTACTO (FAMILIAR DEL PACIENTE)")]
        public string contacto_paciente { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "GENERO")]
        public string genero { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,20}$", ErrorMessage = "Maximo 20 caracteres")]
        [Display(Name = "CONTACTO CELULAR")]
        public string contacto_celular { get; set; }



        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public Int32 id_ciudad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "IPS")]
        public Int32 id_ips { get; set; }

        public DateTime fecha_egreso { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA INGRESO")]
        public Nullable<DateTime> fecha_ingreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAS HOSPITALIZACIÓN")]
        public string dias_hospitalizado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "SERVICIO TRATANTE")]
        public string id_servicio_tratante { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE MEDICO TRATANTE")]
        public string nombre_medico_tratante { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ALTO COSTO/CATASTROFICAS")]
        public Boolean lesiones_severas { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "DESCRIPCION ALTO COSTO")]
        public Int32 id_lesiones_severas { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DESCRIPCION ALTO COSTO")]
        public Int32 id_accidente_grave { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REINGRESO")]
        public string reingreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "MOTIVO REINGRESO")]
        public int id_reingreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "MOTIVO REINGRESO")]
        public String otro_reingreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICOS CIE10_1")]
        public string id_cie10_1 { get; set; }

        [Display(Name = "ID CENSO")]
        public int id_censo { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "DESCRIPCION SALUD PUBLICA")]
        public int id_salud_publica { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DESCRIPCION ORIGEN HOSPITALIZACION")]
        public String id_origen_hospitalizacion { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "HOSPITALIZACION PREVENIBLE")]
        public String hospitalizacion_prevenible { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{40,200}$", ErrorMessage = "Minimo 40 caracteres Maximo 200")]
        [Display(Name = "JUSTIFICACIÓN PREVENIBLE ")]
        public String descripcion_prevenible { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "GESTANTES")]
        public String Gestantes { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TRABAJADOR DE ECOPETROL:")]

        public String Trabajador { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD TRABAJADOR")]
        public String ciudad_trabajador { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "TRIAGE")]
        public String triage { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AUDITORIA TELEFONICA")]
        public String auditoria_telefonica { get; set; }


        [Display(Name = "EGRESO")]
        public string egreso { get; set; }

        public string modificado { get; set; }


        public List<vw_tablero_censo> Lista { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "SALUD PUBLICA3")]
        public Boolean salud_publica { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "SALUD PUBLICA:")]
        public string otroSalud { get; set; }

        public String DesAlto { get; set; }

        public String Desips { get; set; }

        public int id_ciudadIPS { get; set; }


        [Display(Name = "FECHA ULTIMA HOSPITALIZACIÓN:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string fecha_ultima_hospitalizacion { get; set; }

        public int? dias { get; set; }

        #endregion

        #region METODOS

        public void ConsultaIdConcurrenia(Int32 IntIdconcu)
        {
            OBJConcurencia.id_concurrencia = IntIdconcu;
            LstConcurencia = BusClass.ConsultaIdConcurrenia(OBJConcurencia, ref MsgRes);
            if (LstConcurencia.Count != 0)
            {
                CargarConcurrencia();
            }
        }

        public void CargarConcurrencia()
        {
            var idCenso = 0;
            foreach (ecop_concurrencia item in LstConcurencia)
            {
                id_concurrencia = Convert.ToInt32(item.id_concurrencia);
                id_censo = Convert.ToInt32(item.id_censo);
                afi_tipoDoc = item.afi_tipo_doc;
                afi_Nom = item.afi_nom;
                afi_NumDoc = item.id_afi;
                idCenso = Convert.ToInt32(item.id_censo);
                //afi_Edad = Convert.ToInt32(item.afi_edad);
                genero = item.Genero;
                afi_Dir = item.afi_dir;
                afi_tel1 = item.afi_tel;
                afi_cel = item.afi_cel;
                contacto_paciente = item.afi_contacto_nom;
                contacto_celular = item.afi_contacto_cel;

                id_ciudad = Convert.ToInt32(item.id_ciudad);
                id_ips = Convert.ToInt32(item.id_ips);

                fecha_ingreso = item.fecha_ingreso;
                fecha_egreso = Convert.ToDateTime(item.fecha_egreso);
                id_servicio_tratante = item.servicio;
                nombre_medico_tratante = item.med_ser_trata;
                if (item.lesion_severa == "SI")
                {
                    lesiones_severas = true;
                    id_lesiones_severas = Convert.ToInt32(item.id_lesion_severa);
                }
                else
                {
                    lesiones_severas = false;
                }

                var salud = item.salud_publica;

                if (salud == "NO")
                {
                    salud_publica = false;
                }
                else
                {
                    salud_publica = true;
                }

                if (item.id_salud_publica != null)
                {
                    id_salud_publica = Convert.ToInt32(item.id_salud_publica);
                }

                reingreso = item.reingreso;
                id_cie10_1 = item.dx1;

                if (item.fecha_egreso != null)
                {
                    egreso = "SI";

                }
                else
                {
                    egreso = "NO";
                }

                if (item.fecha_mod != null)
                {
                    modificado = "SI";
                }
                else
                {
                    modificado = "NO";

                }
                hospitalizacion_prevenible = item.hospitalizacion_prevenible;
                Gestantes = item.Gestantes;
                Trabajador = item.Trabajador;
                ciudad_trabajador = item.ciudad_trabajador;
                triage = item.triage;
                auditoria_telefonica = item.auditoria_telefonica;
                otroSalud = item.otro_salud_publica;

                vw_ciudad_ips obj = new vw_ciudad_ips();
                List<vw_ciudad_ips> list2 = new List<vw_ciudad_ips>();

                obj.id_concurrencia = id_concurrencia;

                list2 = BusClass.ConsultaIdConcurreniaciudad(obj, ref MsgRes);

                foreach (var item3 in list2)
                {
                    id_ciudadIPS = Convert.ToInt32(item3.id_ref_ciudades);
                }

                List<vw_max_concurrencia_por_documento> ListMax = new List<vw_max_concurrencia_por_documento>();

                ListMax = BusClass.ConsultaMaxConcurrenciaDocumento(afi_NumDoc, ref MsgRes);

                foreach (var item2 in ListMax)
                {
                    fecha_ultima_hospitalizacion = item2.fecha_ingreso.Value.ToString("dd/MM/yyyy");
                    dias = item2.dias_medico;
                }
            }

            ecop_censo censo = new ecop_censo();
            censo = BusClass.GetCensoId(idCenso, ref MsgRes).OrderByDescending(x => x.fecha_ingreso).FirstOrDefault();
            var fechaNacimiento = censo.fecha_nacimiento;

            //afi_Edad =

            DateTime thisDay = DateTime.Today;
            TimeSpan age = (TimeSpan)(thisDay - fechaNacimiento);
            var diasPasados = age.Days;
            var añosPasados = (diasPasados / 365);
            var diasReales = (diasPasados - (añosPasados / 4));
            var edadFinal = (diasReales / 365);

            afi_Edad = (int)edadFinal;
        }

        public List<vw_max_concurrencia_por_documento> GetConcurenciasByDocumento(String Documento)
        {
            return BusClass.ConsultaMaxConcurrenciaDocumento(Documento, ref MsgRes);
        }

        public void ActualizaConcurrencia(ecop_concurrencia ObjConcurrencia, String User, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaConcurrencia(ObjConcurrencia, User, IPAddress, ref MsgRes);
        }

        public List<ecop_concurrencia> GetconcurrenciaAfiliados(string numidafiliado)
        {
            return BusClass.GetconcurrenciaAfiliados(numidafiliado);
        }

        public List<Ref_ips> GetRefIps()
        {
            return BusClass.GetRefIps();
        }

        public void ActualizarIps(int idconcurrencia, int idips, ref MessageResponseOBJ Msg)
        {
            BusClass.ActualizarIps(idconcurrencia, idips, ref Msg);
        }

        #endregion
    }
}