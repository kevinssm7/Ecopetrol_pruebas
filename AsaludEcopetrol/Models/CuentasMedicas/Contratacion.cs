using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class Contratacion
    {
        #region Propiedades

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

        #region Metodos

        public void InsertarCargueContratacion(contratacion_cargue_base obj, List<contratacion_cargue_dtll> ListaContratacion, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarCargueContratacion(obj, ListaContratacion, ref MsgRes);
        }

        public contratacion_cargue_base getcarguecontratacion(int mes, int año)
        {
           return BusClass.getcarguecontratacion(mes, año);
        }
        #endregion
    }
}