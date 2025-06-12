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
    public class ProtesisFija
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

        private List<Ref_odont_parametros_auditados> _LIstParametrosAuditados;
        public List<Ref_odont_parametros_auditados> LIstParametrosAuditados
        {
            get
            {
                if (_LIstParametrosAuditados == null)
                {
                    return _LIstParametrosAuditados = BusClass.getListParametrosAuditados();
                }
                else
                {
                    return _LIstParametrosAuditados;
                }

            }

            set
            {
                _LIstParametrosAuditados = value;
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

      
        public int id_rehabilitacion_oral_protesis_fija { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "MES")]
        public int mesIngresado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AÑO")]
        public int ano { get; set; }

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
        [Display(Name = "DOCUMENTO DE IDENTIDAD:")]
        public decimal documento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE DEL BENEFICIARIO:")]
        public String nombre { get; set; }

        public String Nombre_Municipio { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = " EDAD:")]
        public int edad { get; set; }

        [Required(ErrorMessage = "***")]
      
        [Display(Name = "TIEMPO DE DURACION DE TRATAMIENTO EN MESES:")]
        public int tiempo_tratamiento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = " EL PACIENTE QUEDO SATISFECHO CON EL TRATAMIENTO:")]
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
        [Display(Name = " PACIENTE QUE USA  PROTESIS FIJA POR PRIMERA VEZ Ó REPETIDA:")]
        public String protesis { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "HACE CUANTO SE REALIZÓ LA PROTESIS ANTERIOR?:")]
        public String tiempo_protesis_aterior { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "SE PUEDE VERIFICAR QUE LAS EVOLUCIONES  DE LA HISTORIA CLINICA CONTENGAN EL TRATAMIENTO REALIZADO:")]
        public String hc_tratamiento_realizado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ES TRATAMIENTO TERMINADO DE PROTESIS FIJA SOBRE IMPLANTE:")]
        public String terminado_protesis_fija_implante { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OBSERVACIONES:")]
        public String observaciones { get; set; }

        public DateTime fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        public List<ProtesisFijaDtll> lista_detalle_dientes = new List<ProtesisFijaDtll>();
        public string detalle_dientes_uno { get; set; }
        public string detalle_dientes_dos { get; set; }
        public String alerta { get; set; }

        #endregion

        #region FUNCIONES

        public void ConsultaLocalidades(int opc, string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            Ciudades = BusClass.GetCiudades();
            Ciudades = Ciudades.Where(l => l.id_ref_odont_unis == Convert.ToInt32(strFiltro)).ToList();
        }

        public Int32 InsertarOdontFija(odont_rehabilitacion_oral_protesis_fija OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarOdontFija(OBJ, ref MsgRes);
        }

        public void InsertarOdontFijaDtll(List<odont_rehabilitacion_oral_protesis_fija_dtll> OBJ, ref MessageResponseOBJ MsgRes)
        {
             BusClass.InsertarOdontFijaDtll(OBJ, ref MsgRes);
        }

        public void InsertarprestadorOdontologia(Ref_odont_prestadores obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarprestadorOdontologia(obj, ref MsgRes);
        }
        public List<Ref_odont_prestadores> GetPrestadoresOdont()
        {
            return BusClass.GetPrestadoresOdont();
        }

        #endregion

    }
}