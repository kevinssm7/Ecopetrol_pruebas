using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENUM;
using System.ComponentModel;
using System.Reflection;

namespace AsaludEcopetrol.Models.Odontologia
{
    public class HistoriaClinica
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

        private List<Ref_regional> _regionales;
        public List<Ref_regional> regionales
        {
            get
            {
                if (_regionales == null)
                {
                    _regionales = BusClass.GetRefRegion();


                    return _regionales;
                }
                else
                {
                    return _regionales;
                }

            }

            set
            {
                _regionales = value;
            }
        }

        public int id_odont_historia_clinica { get; set; }

        public int id_odont_historia_clinica_detalle_contenido { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AÑO:")]
        public int año { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "MES:")]
        public int mes { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL:")]
        public int id_regional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "UNIS:")]
        public int id_unis { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ODONTOLOGO AUDITADO:")]
        public String nom_odontologo_auditado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ODONTOLOGO AUDITOR:")]
        public String nom_odontologo_auditor { get; set; }

        public DateTime fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        public int id_odont_historia_clinica_paciente { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DOCUMENTO PACIENTE:")]
        public int numero_hc { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE DEL PACIENTE:")]
        public String paciente { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA ATENCION:")]
        public DateTime? fecha_atencion { get; set; }
        public DateTime? fecha_atencionok { get; set; }

        [RegularExpression(@"^[a-zA-Z\W\S]{1,500}$", ErrorMessage = "Maximo 500 caracteres")]
        public String observaciones { get; set; }


        public int id_odont_historia_clinica_detalle { get; set; }

        public int id_ref_odont_hc_calidad_formal { get; set; }

        public int calificacionf { get; set; }

        public int ponderacionf { get; set; }

        public Decimal calificacion_ponderadaf { get; set; }

        public int id_ref_odont_hc_calidad_contenido { get; set; }

        public int calificacionc { get; set; }

        public int ponderacionc { get; set; }

        public Decimal calificacion_ponderadac { get; set; }

        
        
        #endregion

        #region FUNCIONES


        public Int32 InsertarHistoriaClinica(odont_historia_clinica OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarHistoriaClinica(OBJ, ref MsgRes);
        }
        public Int32 InsertarHistoriaClinicaPaciente(odont_historia_clinica_paciente OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarHistoriaClinicaPaciente(OBJ, ref MsgRes);
        }
        public Int32 InsertarHistoriaClinicaDetalle(odont_historia_clinica_detalle_calidad OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarHistoriaClinicaDetalle(OBJ, ref MsgRes);
        }
        public Int32 InsertarHistoriaClinicaDetalleConte(odont_historia_clinica_detalle_contenido OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarHistoriaClinicaDetalleConte(OBJ, ref MsgRes);
        }
        public List<odont_historia_clinica> GetHistoriaClinica()
        {
            return BusClass.GetHistoriaClinica();
        }


        public List<odont_historia_clinica_paciente> GetHistoriaClinicaPaciente(Int32 id_odont_historia_clinica, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetHistoriaClinicaPaciente(id_odont_historia_clinica, ref MsgRes);
        }
     

        public List<vw_odont_historia_clinica_detalle> GetVWHistoriaClinicaDetalle(Int32 id_odont_historia_clinica_paciente, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetVWHistoriaClinicaDetalle(id_odont_historia_clinica_paciente, ref MsgRes);
        }

        public List<vw_odont_historia_clinica_detalle_contenido> GetVWHistoriaClinicaDetalleConten(Int32 id_odont_historia_clinica_paciente, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetVWHistoriaClinicaDetalleConten(id_odont_historia_clinica_paciente, ref MsgRes);
        }

        public List<Ref_odont_hc_calidad_formal> GetHistoriaClinicaRefCalidadFormal()
        {
            return BusClass.GetHistoriaClinicaRefCalidadFormal();
        }

        public List<Ref_odont_hc_calidad_contenido> GetHistoriaClinicaRefContenido()
        {
            return BusClass.GetHistoriaClinicaRefContenido();
        }

        public void ActualizarOdontHCdtll1(odont_historia_clinica_detalle_calidad obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarOdontHCdtll1(obj2, ref MsgRes);

        }

        public void ActualizarOdontHCdtll2(odont_historia_clinica_detalle_contenido obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarOdontHCdtll2(obj2, ref MsgRes);
        }

        public void ActualizarOdontHCdtllFinal(odont_historia_clinica_paciente obj2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarOdontHCdtllFinal(obj2, ref MsgRes);

        }

        public List<vw_odont_totales_hc> GetOdontogTotales(Int32 id_odont_historia_clinica, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetOdontogTotales(id_odont_historia_clinica, ref MsgRes);
        }

        public List<vw_totales_odont> GetTotalPaciente(Int32 id_odont_historia_clinica, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetTotalPaciente(id_odont_historia_clinica, ref MsgRes);
        }


        public List<vw_odont_consolidado_hc> GetConsolidadoHc(int? regional, int? mes, int? año, ref MessageResponseOBJ MsgRes)
        {
            List <vw_odont_consolidado_hc> lst = BusClass.GetConsolidadoHc(ref MsgRes);
            if (regional != null)
                lst = lst.Where(l => l.Id_Regional == regional).ToList();

            if (mes != null)
                lst = lst.Where(l => l.Mes == mes).ToList();

            if (año != null)
                lst = lst.Where(l => l.Año == año).ToList();

            return lst;
        }

        public List<vw_odont_consolidado_hc_prestador> GetConsolidadoHcporprestador(int? idauditor, int? regional, int? mes, int? año, ref MessageResponseOBJ MsgRes)
        {

            List<vw_odont_consolidado_hc_prestador> lst = BusClass.GetConsolidadoHcporprestador(ref MsgRes);
            
            if (regional != null)
                lst = lst.Where(l => l.Id_Regional == regional).ToList();

            if (idauditor != null)
                lst = lst.Where(l => l.Id_Medico_Auditor == idauditor).ToList();

            if (mes != null)
                lst = lst.Where(l => l.Mes == mes).ToList();

            if (año != null)
                lst = lst.Where(l => l.Año == año).ToList();

            return lst;
        }
        
        public List<vw_odont_count_planes_mejora> GetListCountPlanesMejora(int idregional)
        {
            return BusClass.GetListCountPlanesMejora(idregional);
        }

        public List<vw_odont_count_acciones_mejora> GetListCountAccionesMejora(int idregional)
        {
            return BusClass.GetListCountAccionesMejora(idregional);
        }


        public void InsertarRemisionesEspecialidades(odont_remisiones_especialidades obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarRemisionesEspecialidades(obj, ref MsgRes);
        }

        public List<vw_odont_remisiones_especialidades>GetRemisiones(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetRemisiones(ref MsgRes);
        }

        public void InsertarRemisionesVerificadas(odont_remisiones_verificadas obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarRemisionesVerificadas(obj, ref MsgRes);
        }

        public List<vw_odont_remisiones_verificadas> GetRemisionesVerificadas(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetRemisionesVerificadas(ref MsgRes);
        }

        public List<vw_odont_historia_clinica> GetListHistoriaClinica(int id_historia)
        {
            return BusClass.ListHistoriaClinica(id_historia);
        }

        public List<vw_odont_historia_clinica> GetListHistoriaClinicaXOdontologo(string nomodontologo)
        {
            return BusClass.GetListHistoriaClinicaXOdontologo(nomodontologo);
        }

        public void EliminarHistoriaclinica(int id_hc_paciente, log_eliminacion_historias_clinicas_odontologia obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarHistoriaclinica(id_hc_paciente, obj, ref MsgRes);
        }

        #endregion

    }
}