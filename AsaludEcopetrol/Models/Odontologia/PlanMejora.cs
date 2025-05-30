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
    public class PlanMejora
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

        private List<vw_odont_tbl_prestadores> _LIstPrestadores;
        public List<vw_odont_tbl_prestadores> LIstPrestadores
        {
            get
            {
                if (_LIstPrestadores == null)
                {
                   
                    if (SesionVar.ROL == "1" || SesionVar.ROL == "16")
                    {
                        LIstPrestadores = BusClass.GetprestadoresPlanMejora();

                        return _LIstPrestadores;
                    }
                    else
                    {
                        _LIstPrestadores = BusClass.GetprestadoresPlanMejora();

                        _LIstPrestadores = _LIstPrestadores.Where(x => x.usuario_ingreso == SesionVar.UserName).ToList();

                        return _LIstPrestadores;

                    }

                }
                else
                {
                    return _LIstPrestadores;
                }
            }

            set
            {
                _LIstPrestadores = value;
            }
        }

        private List<vw_odont_detalle_plan_mejora> _LstDetallePlanMejora;
        public List<vw_odont_detalle_plan_mejora> LstDetallePlanMejora
        {
            get
            {
                if (_LstDetallePlanMejora == null)
                {

                    _LstDetallePlanMejora = BusClass.GetOdontogdetallePlanMejora();

                        return _LstDetallePlanMejora;
                   
                }
                else
                {
                    return _LstDetallePlanMejora;
                }
            }

            set
            {
                _LstDetallePlanMejora = value;
            }
        }

        private List<vw_tablero_plan_mejora_ben> _ListPLanBeneficiario;
        public List<vw_tablero_plan_mejora_ben> ListPLanBeneficiario
        {
            get
            {
                if (_ListPLanBeneficiario == null)
                {
                    return _ListPLanBeneficiario = BusClass.ConsultaTableroPlanBen();
                }
                else
                {
                    return _ListPLanBeneficiario;
                }
            }
            set
            {
                _ListPLanBeneficiario = value;
            }
        }

        private List<Ref_odont_acciones_mejora> _LIstAccionesMejora;
        public List<Ref_odont_acciones_mejora> LIstAccionesMejora
        {
            get
            {
                if (_LIstAccionesMejora == null)
                {
                    return _LIstAccionesMejora = new List<Ref_odont_acciones_mejora>(); 
                }
                else
                {
                    return _LIstAccionesMejora;
                }
            }

            set
            {
                _LIstAccionesMejora = value;
            }
        }


        private List<Ref_odont_estado_accion> _LIstEstadosAccionesMejora;
        public List<Ref_odont_estado_accion> LIstEstadosAccionesMejora
        {
            get
            {
                if (_LIstEstadosAccionesMejora == null)
                {
                    return _LIstEstadosAccionesMejora = new List<Ref_odont_estado_accion>();
                }
                else
                {
                    return _LIstEstadosAccionesMejora;
                }
            }

            set
            {
                _LIstEstadosAccionesMejora = value;
            }
        }
           

        private odont_plan_mejora_dtll _OBJDetallle;
        public odont_plan_mejora_dtll OBJDetallle
        {
            get
            {
                if (_OBJDetallle == null)
                {


                    return _OBJDetallle = new odont_plan_mejora_dtll();

                }
                else
                {
                    return _OBJDetallle;
                }
            }

            set
            {
                _OBJDetallle = value;
            }
        }

        private odont_plan_mejora_beneficiario_dtll _OBJDetallleB;
        public odont_plan_mejora_beneficiario_dtll OBJDetallleB
        {
            get
            {
                if (_OBJDetallleB == null)
                {


                    return _OBJDetallleB = new odont_plan_mejora_beneficiario_dtll();

                }
                else
                {
                    return _OBJDetallleB;
                }
            }

            set
            {
                _OBJDetallleB = value;
            }
        }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA SEGUIMIENTO")]
        public DateTime? fecha_seguimiento { get; set; }
        public DateTime? fecha_seguimientoOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ACCION DE MEJORA")]
        public Int32 accion_mejora { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ESTADO")]
        public String estado { get; set; }

        public int id_odont_plan_mejora { get; set; }

        public int id_odont_plan_mejora_dtll { get; set; }
        public int id_odont_plan_mejora_tratamiento { get; set; }

        public int id_odont_plan_mejora_tratamiento_dtll { get; set; }

        public int id_odont_plan_mejora_beneficiario_dtll { get; set; }

        public int id_odont_plan_mejora_beneficiario { get; set; }


        public int id_tratamiento { get; set; }

        public int tipo_tratamiento { get; set; }

        public DateTime fecha_ingreso { get; set; }

        public String usuario_ingreso { get; set; }

        public String habilita { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,1000}$", ErrorMessage = "Maximo 1000")]
        [Display(Name = "OBSERVACIÓN:")]
        public String Observacion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DOCUMENTO DE IDENTIDAD :")]
        public Decimal documento_identidad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE DEL BENEFICIARIO:")]
        public String nombre { get; set; }

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

        public String especialista { get; set; }

        public DateTime fecha_cierre { get; set; }

        public String usuario_cierre { get; set; }




        #endregion

        public void ConsultaAccionesmejoras()
        {
            LIstAccionesMejora = BusClass.GetListAccionesMejora(); 
        }

        public void ConsultaEstadoAccionesmejoras()
        {
            LIstEstadosAccionesMejora = BusClass.GetListEstadosAccionesMejora();
        }

        public Int32 InsertarPlanMejoraTratamiento(odont_plan_mejora_tratamiento OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejoraTratamiento(OBJ, ref MsgRes);
        }

        public Int32 InsertarPlanMejoraTratamientodetalle(odont_plan_mejora_tratamiento_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejoraTratamientodetalle(OBJ, ref MsgRes);
        }

        public Int32 InsertarPlanMejoraBeneficiario(odont_plan_mejora_beneficiario OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejoraBeneficiario(OBJ, ref MsgRes);
        }

        public Int32 InsertarPlanMejoraBeneficiariodetalle(odont_plan_mejora_beneficiario_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejoraBeneficiariodetalle(OBJ, ref MsgRes);
        }

        public List<vw_odont_planes_mejora> GetPlanesMejora()
        {
            return BusClass.GetPlanesMejora();
        }

        public void ActualizarOdontPlanMejora(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarOdontPlanMejora(OBJDetallle, ref MsgRes);
        }

        public void ActualizarOdontPlanMejoraBeneficiario(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarOdontPlanMejoraBeneficiario(OBJDetallleB, ref MsgRes);
        }

        public List<odont_plan_mejora> GetPlanMejoraTra()
        {
            return BusClass.GetPlanMejoraTra();
        }

        public List<odont_plan_mejora_beneficiario> GetPlanMejoraBen()
        {
            return BusClass.GetPlanMejoraBen();
        }

        public List<vw_odont_planes_mejora> GetPlanMejoraTradtll(Int32 id_odont_plan_mejora, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetPlanMejoraTradtll(id_odont_plan_mejora, ref MsgRes);
        }

        public List<vw_odont_planes_mejora_ben> GetPlanMejoraBendtll(Int32 id_odont_plan_mejora_beneficiario, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetPlanMejoraBendtll(id_odont_plan_mejora_beneficiario, ref MsgRes);
        }

        public void ActualizarOdontPlanMejoraObsTratamiento(odont_plan_mejora obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarOdontPlanMejoraObsTratamiento(obj2, ref MsgRes);
        }

        public void ActualizarOdontPlanMejoraObsBeneficiario(odont_plan_mejora_beneficiario obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarOdontPlanMejoraObsBeneficiario(obj2, ref MsgRes);
        }

        public void ActualizarOdontPlanMejoraCierreTrat(odont_plan_mejora obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarOdontPlanMejoraCierreTrat(obj2, ref MsgRes);
        }
        public void ActualizarOdontPlanMejoraCierreBen(odont_plan_mejora_beneficiario obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarOdontPlanMejoraCierreBen(obj2, ref MsgRes);
        }

        public Int32 InsertarPlanMejora(odont_plan_mejora OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejora(OBJ, ref MsgRes);
        }

        public Int32 InsertarPlanMejoradetalle(odont_plan_mejora_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejoradetalle(OBJ, ref MsgRes);
        }


    }
}