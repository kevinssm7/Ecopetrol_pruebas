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
    public class TableroControlOdontologia
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

        private List<vw_odont_tableros_ortodoncia> _ListCheck;
        public List<vw_odont_tableros_ortodoncia> ListCheck
        {
            get
            {
                if (_ListCheck == null)
                {
                    return _ListCheck = new List<vw_odont_tableros_ortodoncia>();
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

        private List<vw_odont_tableros_ProtesisFija> _ListCheck2;
        public List<vw_odont_tableros_ProtesisFija> ListCheck2
        {
            get
            {
                if (_ListCheck2 == null)
                {
                    return _ListCheck2 = new List<vw_odont_tableros_ProtesisFija>();
                }
                else
                {
                    return _ListCheck2;
                }

            }

            set
            {
                _ListCheck2 = value;
            }
        }

        private List<vw_odont_tableros_ProtesisRemov> _ListCheck3;
        public List<vw_odont_tableros_ProtesisRemov> ListCheck3
        {
            get
            {
                if (_ListCheck3 == null)
                {
                    return _ListCheck3 = new List<vw_odont_tableros_ProtesisRemov>();
                }
                else
                {
                    return _ListCheck3;
                }

            }

            set
            {
                _ListCheck3 = value;
            }
        }

        private List<vw_odont_tableros_endodoncia> _ListCheck4;
        public List<vw_odont_tableros_endodoncia> ListCheck4
        {
            get
            {
                if (_ListCheck4 == null)
                {
                    return _ListCheck4 = new List<vw_odont_tableros_endodoncia>();
                }
                else
                {
                    return _ListCheck4;
                }

            }

            set
            {
                _ListCheck4 = value;
            }
        }

        private List<vw_odont_tableros_plan_mejora> _ListCheck5;
        public List<vw_odont_tableros_plan_mejora> ListCheck5
        {
            get
            {
                if (_ListCheck5 == null)
                {
                    return _ListCheck5 = new List<vw_odont_tableros_plan_mejora>();
                }
                else
                {
                    return _ListCheck5;
                }

            }

            set
            {
                _ListCheck5 = value;
            }
        }

        public int id_tratamiento_ortodoncia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL:")]
        public int id_regional { get; set; }

        #endregion

        #region FUNCIONES

        public List<vw_odont_tableros_ortodoncia> LIstTableroOrtodoncia()
        {
            return BusClass.GetOdontogTablerosOrtodoncia();
        }

        public List<vw_odont_tableros_ProtesisFija> LIstTableroPT()
        {
            return BusClass.GetOdontogTablerosPT();
        }

        public List<vw_odont_tableros_ProtesisRemov> LIstTableroPR()
        {
            return BusClass.GetOdontogTablerosPR();
        }

        public List<vw_odont_tableros_endodoncia> LIstTableroEndodoncia()
        {
            return BusClass.GetOdontogTablerosEndodoncia();
        }

        public List<vw_odont_tableros_plan_mejora> GetOdontogTablerosPlanMejora()
        {
            return BusClass.GetOdontogTablerosPlanMejora();

        }

        public List<Ref_odont_unis> LIstUnis()
        {
            return BusClass.Odont_unis();
        }

        public List<Ref_regional> LIstRegional()
        {
            return BusClass.GetRefRegion();
        }


        #endregion
    }
}