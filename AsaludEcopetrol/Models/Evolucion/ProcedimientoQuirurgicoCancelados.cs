using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class ProcedimientoQuirurgicoCancelados
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

        private BussinesManager.SessionState _SesionVar;
        public BussinesManager.SessionState SesionVar
        {
            get
            {
                if (_SesionVar == null)
                {
                    _SesionVar = new BussinesManager.SessionState();
                }
                return _SesionVar;
            }
            set { _SesionVar = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        private List<vw_ref_cups> _LstCups;
        public List<vw_ref_cups> LstCups
        {
            get
            {
                if (_LstCups == null)
                {
                    _LstCups = BusClass.GetCups();
                }

                return _LstCups;
            }
            set { _LstCups = value; }
        }

        private List<Ref_motivo_cancelacion_procedimiento> _LstMotivoCance;
        public List<Ref_motivo_cancelacion_procedimiento> LstMotivoCance
        {
            get
            {
                if (_LstMotivoCance == null)
                {
                    _LstMotivoCance = BusClass.GetMotivoCancelacion();
                }

                return _LstMotivoCance;
            }
            set { _LstMotivoCance = value; }
        }


        private List<ecop_concurrencia_procedimientos_quirurgicos_cancelados> _lstProce;
        public List<ecop_concurrencia_procedimientos_quirurgicos_cancelados> lstProce
        {
            get
            {
                if (_lstProce == null)
                {
                    _lstProce = new List<ecop_concurrencia_procedimientos_quirurgicos_cancelados>();
                }

                return _lstProce;
            }
            set { _lstProce = value; }
        }


        [Required(ErrorMessage = "***")]
        public Int32 id_concurrencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PROCEDIMIENTO")]
        public String id_cups { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "MOTIVO CANCELACION")]
        public String id_motivo_cancelacion { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA CANCELACION")]
        public Nullable<DateTime> fecha_cancelacion { get; set; }

        #endregion

        #region METODOS
        public DateTime ManagmentHora()
        {
            return BusClass.ManagmentHora();
        }

        public void InsertaProcedimientoQuirugicoCancelado(ecop_concurrencia_procedimientos_quirurgicos_cancelados ProcedimientoQuirCance, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertaProcedimientoQuirugicoCancelado(ProcedimientoQuirCance, UserName, IPAddress, ref MsgRes);
        }

        public void ConsultaProcedimientoQuirurgicoCancelado(Int32 idConcu)
        {
            ecop_concurrencia_procedimientos_quirurgicos_cancelados objProcQui = new ecop_concurrencia_procedimientos_quirurgicos_cancelados();
            objProcQui.id_concurrencia = idConcu;
            lstProce = BusClass.ConsultaProcQuirurgicosCance(objProcQui, ref MsgRes);
        }

        #endregion
    }
}