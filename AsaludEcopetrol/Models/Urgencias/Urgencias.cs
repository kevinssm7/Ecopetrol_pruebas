using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AsaludEcopetrol.Models.Urgencias
{
    public class Urgencias
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

        private List<Ref_regional> _refRegional;
        public List<Ref_regional> RefRegional
        {
            get
            {
                if (_refRegional == null)
                {
                    _refRegional = BusClass.GetRefRegion();

                    return _refRegional;
                }
                else
                {
                    return _refRegional;
                }

            }

            set
            {
                _refRegional = value;
            }
        }

        private List<vw_tablero_urgencias_ok> _LstTableroUrgencias;
        public List<vw_tablero_urgencias_ok> LstTableroUrgencias
        {
            get
            {
                if (_LstTableroUrgencias == null)
                {

                    _LstTableroUrgencias = BusClass.ConsultaTablerUrgenciasOk();
                    _LstTableroUrgencias = _LstTableroUrgencias.Where(p => p.COORDINACION == regional).ToList();
                    _LstTableroUrgencias = _LstTableroUrgencias.Where(p => p.fecha_digita.Value.Date >= fecha_desde.Value.Date && p.fecha_digita.Value.Date <= fecha_hasta.Value.Date).ToList();
                    _LstTableroUrgencias = _LstTableroUrgencias.OrderBy(p => p.fecha_digita).ToList();

                    return _LstTableroUrgencias;
                }
                else
                {
                    return _LstTableroUrgencias;
                }

            }

            set
            {
                _LstTableroUrgencias = value;
            }
        }


        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DESDE")]
        public DateTime? fecha_desde { get; set; }
        public DateTime? fecha_desdeOK { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA HASTA")]
        public DateTime? fecha_hasta { get; set; }
        public DateTime? fecha_hastaOK { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public String regional { get; set; }

        #endregion

        #region METODOS

        public void InsertarUrgencias(List<urg_cargue_base_ok> ListUrgencias)
        {
            BusClass.InsertarUrgencias(ListUrgencias, ref MsgRes);
        }

        public void InsertarAuditoriaUrgencias(urg_auditoria_urgencias aud_urgencias)
        {
            BusClass.InsertarAuditoriaUrgencias(aud_urgencias, ref MsgRes);
        }

        public List<urg_cargue_base_ok> ConsultarUrgencias(int? idurgencia, DateTime? fechadesde, DateTime? fechahasta, int? regional, int? idusuario)
        {
            return BusClass.ConsultarUrgencias(idurgencia, fechadesde, fechahasta, regional, idusuario, ref MsgRes);
        }

        public List<Ref_tipo_egreso> ConsultaTipoEgreso()
        {
            return BusClass.ConsultaTipoEgreso(ref MsgRes);
        }

        public List<ref_urg_destino_paciente> ConsultaDestinoPaciente()
        {
            return BusClass.ConsultaDestinoPaciente(ref MsgRes);
        }



        

        #endregion

    }
}