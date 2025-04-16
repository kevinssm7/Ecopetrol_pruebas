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
    public class Endodoncia
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

        private List<Ref_odont_tipo_endodoncia> _LIstTipoEndodoncia;
        public List<Ref_odont_tipo_endodoncia> LIstTipoEndodoncia
        {
            get
            {
                if (_LIstTipoEndodoncia == null)
                {
                    return _LIstTipoEndodoncia = BusClass.getListTipoEndodoncia();
                }
                else
                {
                    return _LIstTipoEndodoncia;
                }

            }

            set
            {
                _LIstTipoEndodoncia = value;
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


        public int id_tratamiento_endodoncia { get; set; }

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
        public Decimal documento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE DEL BENEFICIARIO:")]
        public String nombre { get; set; }

        [Required(ErrorMessage = "***")]
        [Range(typeof(int), "0", "110", ErrorMessage = "Rango 0-110 años")]
        [Display(Name = "EDAD:")]
        public int edad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIENTE EVALUADO:")]
        public int diente_evaluado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ENDODONCISTA TRATANTE:")]
        public String endodoncista_tratante { get; set; }

        [Required(ErrorMessage = "***")]

        [Display(Name = "TIPO DE ENDODONCIA REALIZADA:")]
        public int tipo_endodoncia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ODONTOLOGO PPE QUE REALIZA REMISION:")]
        public String odontologo_ppe { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "SE VERIFICA CALIDAD EN LA EVOLUCION DEL TTO EN HISTORIA CLINICA:")]
        public String calidad_tto_hc { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EL TRATAMIENTO DE ENDODONCIA ES PARA UN TRATAMIENTO DE REHABILITACION ORAL:")]
        public String rehabilitacion_oral { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ESTE TRATAMIENTO DE ENDODONCIA ES RETRATATAMIENTO ANTES DE LOS 12 MESES DE GARANTIA:")]
        public String retratamiento_antes_12_meses { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OBSERVACIONES:")]
        public String observaciones { get; set; }

        public DateTime fecha_digita { get; set; }
        
        public String usuario_digita { get; set; }


        public String retratamiento { get; set; }
        
        public DateTime fecha_retratamiento { get; set; }
        
        public String urgencia { get; set; }

        public DateTime fecha_urgencia { get; set; }

        public DateTime fecha_atencion { get; set; }

        public Decimal check1 { get; set; }
        public Decimal check2 { get; set; }
        public Decimal check3 { get; set; }
        public Decimal check4 { get; set; }
        public Decimal check5 { get; set; }


        #endregion

        #region FUNCIONES

        public void ConsultaLocalidades(int opc, string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            Ciudades = BusClass.GetCiudades();
            Ciudades = Ciudades.Where(l => l.id_ref_odont_unis == Convert.ToInt32(strFiltro)).ToList();
        }

        public Int32 InsertarOdontEndodoncia(odont_tratamiento_endodoncia OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarOdontEndodoncia(OBJ, ref MsgRes);
        }

        public Int32 InsertarOdontEndodonciadtll(odont_tratamiento_endodoncia_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarOdontEndodonciadtll(OBJ, ref MsgRes);
        }


        #endregion

    }
}