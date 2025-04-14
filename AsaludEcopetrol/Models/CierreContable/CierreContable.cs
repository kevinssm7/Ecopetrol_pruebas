using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.CierreContable
{
    public class CierreContable
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


        #region Metodos


        public Int32 InsertarCierreContable(cierre_contable obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarCierreContable(obj, ref MsgRes);
        }

        public List<cierre_contable> GetListCierreContable(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetListCierreContable( ref MsgRes);
        }

        public bool InsertarFacturasMesInterior(List<cierre_cont_mes_anterior> List, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFacturasMesInterior(List, ref MsgRes);
        }

        public bool InsertarFacturasRechazos(List<cierre_cont_rechazos> List, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFacturasRechazos(List, ref MsgRes);
        }

        public bool InsertarFacturasPendientesProcs(List<cierre_cont_pendiente_procesar> List, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFacturasPendientesProcs(List, ref MsgRes);
        }

        public bool InsertarFacturasdevoluciones(List<cierre_cont_devoluciones> List, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFacturasdevoluciones(List, ref MsgRes);
        }

        public bool InsertarFacturascausadas(List<cierre_cont_causadas> List, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFacturascausadas(List, ref MsgRes);
        }

        public bool InsertarFacturasradicadas(List<cierre_cont_radicadas> List, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFacturasradicadas(List, ref MsgRes);
        }

        public cierre_contable GetCierreContable(int idcierre, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetCierreContable(idcierre, ref MsgRes);
        }


        public List<vw_totales_cierre_contable> GetListTotalesCierreContable(int idcierre, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetListTotalesCierreContable(idcierre, ref MsgRes);
        }

        public List<vw_causas_facturas> GetListCausasCierreContable(int idcierre, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetListCausasCierreContable(idcierre, ref MsgRes);
        }
        #endregion

    }
}