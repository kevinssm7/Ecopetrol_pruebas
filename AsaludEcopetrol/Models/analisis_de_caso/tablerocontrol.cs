using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.analisis_de_caso
{
    public class tablerocontrol
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
        #endregion

        #region Metodos

        public List<vw_tablero_analisis_casos> ConsultaTableroAnalisisCasos()
        {
            return BusClass.ConsultaTableroAnalisisCasos(ref MsgRes);
        }

        public void Insertargestionanalisisdecaso(GestionAnalisisDeCasos Analisis)
        {
             BusClass.Insertargestionanalisisdecaso(Analisis, ref MsgRes);
        }

        public void Actualizargestionanalisisdecaso(GestionAnalisisDeCasos Analisis)
        {
            BusClass.Actualizargestionanalisisdecaso(Analisis, ref MsgRes);
        }

        public GestionAnalisisDeCasos GetAnalisisGestion(Int32? idtipoanalisis, Int32? idanalsis)
        {
           return BusClass.GetAnalisisGestion(idtipoanalisis, idanalsis, ref MsgRes);
        }

        #endregion
    }
}