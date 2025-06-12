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
    public class ortodoncia
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


        private List<Ref_odont_prestadores> _Odont_Especialistas;
        public List<Ref_odont_prestadores> Odont_Especialistas
        {
            get
            {
                if (_Odont_Especialistas == null)
                {
                    return _Odont_Especialistas = new List<Ref_odont_prestadores>();
                }
                else
                {
                    return _Odont_Especialistas;
                }

            }

            set
            {
                _Odont_Especialistas = value;
            }
        }

        private List<ref_meses_del_año> _meses;
        public List<ref_meses_del_año> meses
        {
            get
            {
                if (_meses == null)
                {
                    _meses = BusClass.meses();


                    return _meses;
                }
                else
                {
                    return _meses;
                }

            }

            set
            {
                _meses = value;
            }
        }

        private List<vw_reportesTratamientosUnificados> _ListReporteTratUnif;
        public List<vw_reportesTratamientosUnificados> ListReporteTratUnif
        {
            get
            {
                if (_ListReporteTratUnif == null)
                {
                    return _ListReporteTratUnif = new List<vw_reportesTratamientosUnificados>();
                }
                else
                {
                    return _ListReporteTratUnif;
                }

            }

            set
            {
                _ListReporteTratUnif = value;
            }
        }


        public int id_tratamiento_ortodoncia { get; set; }

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
        [Display(Name = "DOCUMENTO DE IDENTIDAD :")]
        public Decimal documento_identidad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE DEL BENEFICIARIO:")]
        public String nombre { get; set; }


        [Required(ErrorMessage = "***")]
        [Range(typeof(int), "0", "110", ErrorMessage = "Rango 0-110 años")]
        [Display(Name = "EDAD:")]
        public int edad { get; set; }

        [Required(ErrorMessage = "***")]
 
        [Display(Name = "TIEMPO DE DURACION DE TRATAMIENTO EN MESES :")]
        public int tiempo_tratamiento { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "ORTODONCISTA TRATANTE:")]
        public String ortodoncista_tratante { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "HUBO COLABORACION POR PARTE DEL PACIENTE EN EL TRATAMIENTO:")]
        public String colaboracion_paciente { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "ODONTOLOGO PPE QUE REALIZA REMISION:")]
        public String ppe_quien_realiza { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "EL PACIENTE QUEDO SATISFECHO CON EL TRATAMIENTO:")]
        public String paciente_satisfecho { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "SE COLOCA TRATAMIENTO DE ORTODONCIA POR PRIMERA VEZ:")]
        public String ortodoncia_primera_vez { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "SE VERIFICA CALIDAD EN LA EVOLUCION DEL TTO EN HISTORIA CLINICA:")]
        public String verifica_calidad_tto_hc { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "ES NO ES CASO DE TRANSFERENCIA:")]
        public String caso_trasferencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DE DONDE:")]
        public String donde_trasferencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OBSERVACIONES:")]
        public String observaciones { get; set; }


        public DateTime fecha_auditoria { get; set; }
        public String usuario_digita { get; set; }


        public String numero_caso { get; set; }

        public Decimal check1 { get; set; }
        public Decimal check2 { get; set; }
        public Decimal check3 { get; set; }
        public Decimal check4 { get; set; }
        public Decimal check5 { get; set; }
        public Decimal check6 { get; set; }
        public Decimal check7 { get; set; }
        public Decimal check8 { get; set; }
        public Decimal check9 { get; set; }



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

        public void ConsultaEspecialistas(string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            Odont_Especialistas = BusClass.GetPrestadoresOdont();
            Odont_Especialistas = Odont_Especialistas.Where(x => x.regional == Convert.ToInt32(strFiltro)).ToList();

        }

        public void ConsultaReportes()
        {
            ListReporteTratUnif = BusClass.GetReportTratamientosUnificados(ref  MsgRes);
          
        }
        

        public List<Ref_odont_list_check_ortod> getcheckOrtod()
        {
            ListCheck = BusClass.getcheckOrtod();

            return ListCheck;
        }


        public Int32 InsertarOdontOrtodoncia(odont_tratamiento_ortodoncia OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarOdontOrtodoncia(OBJ, ref MsgRes);
        }

        public Int32 InsertarOdontOrtodonciaDetalle(odont_tratamiento_ortodoncia_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarOdontOrtodonciaDetalle(OBJ, ref MsgRes);
        }
        
        public List<VW_base_beneficiarios> ListaBeneficiarios(String id)
        {
            List<VW_base_beneficiarios> list = new List<VW_base_beneficiarios>();

            list = BusClass.BeneficiariosDocumento(id, ref MsgRes);

            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    nombre = item.Nombre + ' ' + item.Apellidos;
                    edad = Convert.ToInt32(item.edad);
                }
            }
          
            return list;

        }

        public List<Ref_odont_prestadores> GetPrestadoresOdont()
        {
            return BusClass.GetPrestadoresOdont();
        }

        public List<vw_prestadores_odontologiaUnificado> GetPrestadoresOdonUnificados()
        {
            return BusClass.GetPrestadoresOdonUnificados(ref MsgRes);
        }

        #endregion
    }
}