using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using System.Data;
using System.Configuration;
using System.IO;

namespace AsaludEcopetrol.Models.Insumos
{
    public class Indicador_calidad
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

        public int id_indicadores_calidad { get; set; }
        public int id_capacidad { get; set; }
        public string regional { get; set; }
        public string unis { get; set; }
        public string ciudad { get; set; }
        public int nit_prestador { get; set; }
        public string prestador { get; set; }
        public string prestador_homologado { get; set; }
        public int documento_profesional { get; set; }
        public string profesional { get; set; }
        public string especialidad { get; set; }
        public string periodo { get; set; }
        public string mes { get; set; }
        public string trimestre { get; set; }
        public string clave { get; set; }
        public int autorizaciones { get; set; }
        public int citas { get; set; }
        public Decimal capacidad_resolutiva { get; set; }


        private List<management_insumos_capacidad_resolutiva_listaResult> _IndicadorLista;
        public List<management_insumos_capacidad_resolutiva_listaResult> IndicadorLista
        {
            get
            {
                if (_IndicadorLista == null)
                {
                    _IndicadorLista = new List<management_insumos_capacidad_resolutiva_listaResult>();
                }
                return _IndicadorLista;
            }
            set
            {
                _IndicadorLista = value;
            }
        }

        public bool ValidarExistenciaIndicadorCalidad(int mes, int año)
        {
            return BusClass.ValidarExistenciaIndicadorCalidad(mes, año);
        }

        public void InsertarIndicadoresCalidadDtll(List<insumos_capacidad_resolutiva_dtll> List, insumos_capacidad_resolutiva objbase, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarIndicadoresCalidadDtll(List, objbase, ref MsgRes);
        }
    }
}