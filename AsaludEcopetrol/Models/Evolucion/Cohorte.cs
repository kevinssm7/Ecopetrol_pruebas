using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using System.ComponentModel.DataAnnotations;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class Cohorte
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

        private List<vw_ecop_concurrencia_cohorte> _LstCohorte;
        public List<vw_ecop_concurrencia_cohorte> LstCohorte
        {
            get
            {
                if (_LstCohorte == null)
                {
                    return _LstCohorte = new List<vw_ecop_concurrencia_cohorte>();
                }
                else
                {
                    return _LstCohorte;
                }

            }

            set
            {
                _LstCohorte = value;
            }
        }

        private List<Ref_ingreso_cohorte> _LstRefTipoCohorte;
        public List<Ref_ingreso_cohorte> LstRefTipoCohorte
        {
            get
            {
                if (_LstRefTipoCohorte == null)
                {
                    return _LstRefTipoCohorte = BusClass.GetRefTipoCohorte();
                }
                else
                {
                    return _LstRefTipoCohorte;
                }

            }

            set
            {
                _LstRefTipoCohorte = value;
            }
        }

        private ecop_concurrencia_cohorte _OBJCohorte;
        public ecop_concurrencia_cohorte OBJCohorte
        {
            get
            {
                if (_OBJCohorte == null)
                {
                    return _OBJCohorte = new ecop_concurrencia_cohorte();
                }
                else
                {
                    return _OBJCohorte;
                }

            }

            set
            {
                _OBJCohorte = value;
            }
        }


        MessageResponseOBJ MsgRes = new MessageResponseOBJ();


        #endregion


        public Int32 id_concurrencia { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "INGRESO COHORTE")]
        public int id_ref_ingreso_cohorte { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OBSERVACIONES COHORTE")]
        public String Observacion { get; set; }

        #region METODOS     


        public void ConsultaCohorte(Int32 idConcu)
        {
            vw_ecop_concurrencia_cohorte objCC = new vw_ecop_concurrencia_cohorte();
            objCC.id_concurrencia = idConcu;
            LstCohorte = BusClass.ConsultaCohorte(objCC, ref MsgRes);
        }

        public void InsertarConcurrenciaCohorte(ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarConcurrenciaCohorte(OBJCohorte, ref MsgRes);
        }

        #endregion



    }
}