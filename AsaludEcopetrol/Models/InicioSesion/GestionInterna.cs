using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;

namespace AsaludEcopetrol.Models.InicioSesion
{
    public class GestionInterna
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

        #endregion

        #region FUNCIONES 

        public List<ref_gestion_interna> GetGestionInternaList()
        {
            return BusClass.GetGestionInternaList();
        }

        public ref_gestion_interna GetGestionInternaById(int idgestion)
        {
            return BusClass.GetGestionInternaById(idgestion);
        }

        #endregion
    }
}