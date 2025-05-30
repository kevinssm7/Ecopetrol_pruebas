using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class DiagnosticoDefinitivo
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

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();


        private List<ecop_concurrencia_evolucion_diag_def> _lstDiagDef;
        public List<ecop_concurrencia_evolucion_diag_def> lstDiagDef
        {
            get
            {
                if (_lstDiagDef == null)
                {
                    _lstDiagDef = new List<ecop_concurrencia_evolucion_diag_def>();
                    // _lstDiagDef = BusClass.GetCDiagnosticoDefinitivo();
                }

                return _lstDiagDef;
            }
            set { _lstDiagDef = value; }
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

        [Required(ErrorMessage = "***")]
        public Int32 id_concurrencia
        {
            get; set;
        }
        

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,1000}$", ErrorMessage = "Maximo 1000 caracteres")]
        [Display(Name = "DIAGNOSTICO DEFINITIVO")]
        public string diagnosticoDefinitivo { get; set; }
        #endregion


        public void ConsultaDiagnosticoDefinitivo(Int32 idConcu)
        {
            ecop_concurrencia_evolucion_diag_def Objdiagdef = new ecop_concurrencia_evolucion_diag_def();
            Objdiagdef.id_concurrencia = idConcu;
            lstDiagDef = BusClass.ConsultaDiagnosticoDefinitivo(Objdiagdef, ref MsgRes);
        }


        public void InsertaDiagnosticoDefinitivo(ecop_concurrencia_evolucion_diag_def DiagnosticoDefinitivo, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {

            BusClass.InsertaDiagnosticoDefinitivo(DiagnosticoDefinitivo, UserName, IPAddress, ref MsgRes);
        }



    }
}