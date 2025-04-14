using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Odontologia
{
    public class ProtesisRemovible
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

        private List<Ref_odont_list_check_ortod> _ListCheck;
        public List<Ref_odont_list_check_ortod> ListCheck
        {
            get
            {
                if (_ListCheck == null)
                {
                    return _ListCheck = new List<Ref_odont_list_check_ortod>();
                }
                else
                {
                    return _ListCheck;
                }

            }

            set
            {
                _ListCheck = value;
            }
        }

        private List<Ref_odont_check_porcentajes> _LIstPorcentajes;
        public List<Ref_odont_check_porcentajes> LIstPorcentajes
        {
            get
            {
                if (_LIstPorcentajes == null)
                {
                    return _LIstPorcentajes = BusClass.getcheckPorcentaje();
                }
                else
                {
                    return _LIstPorcentajes;
                }

            }

            set
            {
                _LIstPorcentajes = value;
            }
        }

        private List<Ref_odont_unis> _Odont_unis;
        public List<Ref_odont_unis> Odont_unis
        {
            get
            {
                if (_Odont_unis == null)
                {
                    return _Odont_unis = new List<Ref_odont_unis>();
                }
                else
                {
                    return _Odont_unis;
                }

            }

            set
            {
                _Odont_unis = value;
            }
        }

        private List<Ref_ciudades> _Ciudades;
        public List<Ref_ciudades> Ciudades
        {
            get
            {
                if (_Ciudades == null)
                {
                    return _Ciudades = new List<Ref_ciudades>();
                }
                else
                {
                    return _Ciudades;
                }

            }

            set
            {
                _Ciudades = value;
            }
        }

        private List<Ref_odont_parametros_auditados_rem> _LIstParametrosRem;
        public List<Ref_odont_parametros_auditados_rem> LIstParametrosRem
        {
            get
            {
                if (_LIstParametrosRem == null)
                {
                    return _LIstParametrosRem = BusClass.GetparametrosRem();
                }
                else
                {
                    return _LIstParametrosRem;
                }

            }

            set
            {
                _LIstParametrosRem = value;
            }
        }

        private List<Ref_odont_tratamiento_rem> _LIsttratamientosRem;
        public List<Ref_odont_tratamiento_rem> LIsttratamientosRem
        {
            get
            {
                if (_LIsttratamientosRem == null)
                {
                    return _LIsttratamientosRem = BusClass.GetTratamientosRem();
                }
                else
                {
                    return _LIsttratamientosRem;
                }

            }

            set
            {
                _LIsttratamientosRem = value;
            }
        }

        public int id_rehabilitacion_oral_protesis_removibles { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL:")]
        public int id_regional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "UNIS:")]
        public int id_unis { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "LOCALIDAD:")]
        public int id_localidad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "MES")]
        public int mesIngresado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AÑO")]
        public int ano { get; set; } 

        [Required(ErrorMessage = "***")]
        [Display(Name = "DOCUMENTO DE IDENTIDAD:")]
        public decimal documento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE DEL BENEFICIARIO:")]
        public String nombre { get; set; }

        [Required(ErrorMessage = "***")]
        [Range(typeof(int), "0", "110", ErrorMessage = "Rango 0-110 Años")]
        [Display(Name = "EDAD:")]
        public int edad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIEMPO DE DURACION DE TRATAMIENTO EN MESES :")]
        public int tiempo_tratamiento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EL PACIENTE QUEDO SATISFECHO CON EL TRATAMIENTO:")]
        public string paciente_satisfecho { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ESPECIALISTA TRATANTE:")]
        public String especialista_tratante { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "HUBO COLABORACION POR PARTE DEL PACIENTE EN EL TRATAMIENTO:")]
        public String colaboracion_paciente { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ODONTOLOGO PPE QUE REALIZA REMISION:")]
        public String ppe_quien_realiza { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ES TRATAMIENTO REALIZADO DENTRO DE LOS TIEMPOS DE GARANTIA:")]
        public String se_realizo_en_los_tiempos { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "No DE PROTESIS  TOTAL Y/O REMOVIBLES   AUDITADAS:")]
        public int numero_protesis { get; set; }

        public int numero_protesis_valor { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PACIENTE QUE USA  PROTESIS REMOVIBLE Y/O TOTAL POR PRIMERA VEZ Ó REPETIDA:")]
        public String protesis { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "HACE CUANTO SE REALIZÓ LA PROTESIS ANTERIOR?:")]
        public int tiempo_protesis_aterior { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "SE VERIFICA EVOLUCION DEL TRATAMIENTO EN HISTORIA CLINICA:")]
        public String hc_tratamiento_realizado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ES PROTESIS  TOTAL- SOBREDENTADURA REALIZADA SOBRE IMPLANTES DE OSEOINTEGRACION:")]
        public String sobredentadura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OBSERVACIONES:")]
        public String observaciones { get; set; }

        public DateTime fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        public string detalle { get; set; }

        public String alerta { get; set; }

        public List<ProtesisRemovibleDtll> lista_detalle = new List<ProtesisRemovibleDtll>();

        #endregion

        #region FUNCIONES

        public void ConsultaLista(int opc, string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            Odont_unis = BusClass.Odont_unis();
            Odont_unis = Odont_unis.Where(x => x.id_regional == Convert.ToInt32(strFiltro)).ToList();
        }

        public void ConsultaLocalidades(int opc, string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            Ciudades = BusClass.GetCiudades();
            Ciudades = Ciudades.Where(l => l.id_ref_odont_unis == Convert.ToInt32(strFiltro)).ToList();
        }

        public Int32 InsertarOdontRemovible(odont_rehabilitacion_oral_protesis_removibles OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarOdontRemovible(OBJ, ref MsgRes);
        }
      
        public Int32 InsertarOdontRemovibledtll(odont_rehabilitacion_oral_protesis_removibles_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarOdontRemovibledtll(OBJ, ref MsgRes);
        }


        #endregion

    }
}