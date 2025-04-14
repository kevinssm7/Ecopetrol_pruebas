using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.MorNatSinConcurrencia
{
    public class MorNatSinConcurrencia
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

        private List<Ref_tipo_documental> _ListTipoDocumento;

        public List<Ref_tipo_documental> ListTipoDocumento
        {
            get
            {
                if (_ListTipoDocumento == null)
                {
                    return _ListTipoDocumento = BusClass.GetTipoDocumnetal();
                }
                else
                {
                    return _ListTipoDocumento;
                }
              
            }

            set
            {
                _ListTipoDocumento = value;
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

        private natalidad_sin_concurrencia _ObjNatalidad;
        public natalidad_sin_concurrencia ObjNatalidad
        {
            get
            {
                if (_ObjNatalidad == null)
                {
                    return _ObjNatalidad = new natalidad_sin_concurrencia();
                }
                else
                {
                    return _ObjNatalidad;
                }
               
            }

            set
            {
                _ObjNatalidad = value;
            }
        }

        private mortalidad_sin_concurrencia _ObjMortalidad;
        public mortalidad_sin_concurrencia ObjMortalidad
        {
            get
            {
                if (_ObjMortalidad == null)
                {
                    return _ObjMortalidad = new mortalidad_sin_concurrencia();
                }
                else
                {
                    return _ObjMortalidad;
                }
               
            }

            set
            {
                _ObjMortalidad = value;
            }
        }

        private List<Total_ciudades> _GetCiudades;
        public List<Total_ciudades> GetCiudades
        {
            get
            {
                if (_GetCiudades == null)
                {
                    return _GetCiudades = BusClass.GetTotalCiudades();
                }
                else
                {
                    return _GetCiudades;
                }
               
            }

            set
            {
                _GetCiudades = value;
            }
        }

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
        [Range(typeof(int), "1", "110", ErrorMessage = "Rango 0-110 años")]
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

        [Required(ErrorMessage = "***")]
        [Display(Name = "IPS")]
        public String nom_ips { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA INGRESO")]
        public Nullable<DateTime> fecha_ingreso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "GESTANTES")]
        public String Gestantes { get; set; }

        [Required(ErrorMessage = "***")]
        public string IdSeleccionado { get; set; }

        [Required(ErrorMessage = "***")]
        public String Items { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DE DEFUNCION")]
        public String NumeroDefuncion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "HORA DE DEFUNCION")]
        public String HoraDefuncion { get; set; }
        
        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA FALLECIMIENTO")]
        public DateTime? fecha_de_muerte { get; set; }

        public DateTime? fecha_de_muerteok { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OBSERVACIONES/CAUSA DETALLADA DE LA MUERTE (NOTA AUDITORÍA)")]
        public String Observacion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO CAUSA DIRECTA MUERTE")]
        public String diag_causa_directa_muerte { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO CAUSA BASICA MUERTE")]
        public String diag_causa_basica_muerte { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO CAUSA ANTECEDENTE MUERTE")]
        public String diag_causa_antecedente_muerte { get; set; }

        [Display(Name = "FECHA EXPEDICION CEDULA")]
        public DateTime? fecha_exp_cedula_fallecido { get; set; }

        public DateTime? fecha_exp_cedula_fallecidook { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA NACIMIENTO DEL MENOR")]
        public DateTime? fecha_nacimiento { get; set; }

        public DateTime? fecha_nacimientook { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EDAD GESTACIONAL")]
        public String edad_gestacional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PESO")]
        public String peso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VIA DEL PARTO")]
        public String id_via_parto { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TALLA")]
        public String talla { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "SEXO")]
        public String sexo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "APGAR")]
        public String apgar { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CONTROL PRENATAL")]
        public String control_prenatal { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA BCG")]
        public DateTime? fecha_BCG { get; set; }
        public DateTime? fecha_BCGok { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA VITAMINA K")]
        public DateTime? fecha_vitaminaK { get; set; }

        public DateTime? fecha_vitaminaKok { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA CONSEJERIA  DE LACTANCIA")]
        public DateTime? fecha_consenjeria_lactancia { get; set; }

        public DateTime? fecha_consenjeria_lactanciaok { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "RESULTADO  DE TSH")]
        public String resultadoTCH { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE TSH")]
        public DateTime? fechaTCH { get; set; }

        public DateTime? fechaTCHok { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA CONSULTA PEDIATRIA")]
        public DateTime? fecha_consulta_pediatria { get; set; }
        public DateTime? fecha_consulta_pediatriaok { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE LA VACUNA HEPATITIS B")]
        public DateTime? fecha_hepatitisB { get; set; }

        public DateTime? fecha_hepatitisBok { get; set; }

      




        #endregion

        #region METODOS

        public List<VW_base_beneficiarios> BaseBeneficiarios()
        {
          return  BusClass.BeneficiariosDocumento(afi_NumDoc, ref MsgRes);
        }

        public void InsertarNatalidad( ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarNatalidad(ObjNatalidad, ref MsgRes);
        }

        public void InsertarMortalidad( ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarMortalidad(ObjMortalidad, ref MsgRes);
        }
        
        #endregion
    }
}