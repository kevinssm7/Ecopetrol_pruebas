using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class GestionTablero
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

        public int id_md_gestion { get; set; }

        public int id_herramienta_tec_med { get; set; }

        public int tabla { get; set; }

        public int id_indicadores_medicamentos { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO HALLAZGO")]
        public int tipo_hallazgo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "SOPORTE")]
        public String soporte { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PLAN MEJORA")]
        public String plan_mejora { get; set; }

        [Required(ErrorMessage = "***")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA LIMITE RESPUESTA")]
        public DateTime? fecha_limite_respuesta { get; set; }
        public DateTime? fecha_limite_respuestaok { get; set; }

        public DateTime? fecha_ingreso { get; set; }
        
        public String usuario_ingreso { get; set; }




        private List<md_Ref_tipo_hallazgo> _GETTipoHallazgo;
        public List<md_Ref_tipo_hallazgo> GETTipoHallazgo
        {
            get
            {
                if (_GETTipoHallazgo == null)
                {
                    return _GETTipoHallazgo = BusClass.TipoHallazgo();

                }
                else
                {
                    return _GETTipoHallazgo;
                }
                
            }

            set
            {
                _GETTipoHallazgo = value;
            }
        }

        private md_indicadores_gestion _OBJGestion;
        public md_indicadores_gestion OBJGestion
        {
            get
            {
                if (_OBJGestion == null)
                {
                    return _OBJGestion = new md_indicadores_gestion();
                }
                else
                {
                    return _OBJGestion;
                }
                
            }

            set
            {
                _OBJGestion = value;
            }
        }

        private List<vw_indicador_detalle_sin_cumplir> _ListaIndicadorDetalle;
        public List<vw_indicador_detalle_sin_cumplir> ListaIndicadorDetalle
        {
            get
            {
                if (_ListaIndicadorDetalle == null)
                {
                    _ListaIndicadorDetalle = BusClass.GetIndicadoresSinCumplir(id_indicadores_medicamentos);

                  

                    return _ListaIndicadorDetalle;
                }
                else
                {
                    return _ListaIndicadorDetalle;
                }

            }

            set
            {
                _ListaIndicadorDetalle = value;
            }
        }

        private List<vw__md_herramientaT_sin_cumplir> _ListaHerramientasSinCumplir;
        public List<vw__md_herramientaT_sin_cumplir> ListaHerramientasSinCumplir
        {
            get
            {
                if (_ListaHerramientasSinCumplir == null)
                {
                    _ListaHerramientasSinCumplir = BusClass.GetHerramientasSincumplir(id_herramienta_tec_med,tabla);

                    return _ListaHerramientasSinCumplir;
                }
                else
                {
                    return _ListaHerramientasSinCumplir;
                }

            }

            set
            {
                _ListaHerramientasSinCumplir = value;
            }
        }

        #endregion

        #region FUNCIONES

        public Int32 InsertarIndicadorGestion( ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarIndicadorGestion(OBJGestion, ref MsgRes);
        }

        public void ActualizarIndicadoresMDEstado(md_indicadores OBJIndicadoresMD, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarIndicadoresMDEstado(OBJIndicadoresMD, ref MsgRes);
        }

        public List<vw_indicador_detalle_sin_cumplir> GetIndicadoresSinCumplir(Int32 id_indicadores_medicamentos)
        {
            List<vw_indicador_detalle_sin_cumplir> lista = new List<vw_indicador_detalle_sin_cumplir>();

            lista = BusClass.GetIndicadoresSinCumplir(id_indicadores_medicamentos);

            return lista;
        }

        public Int32 InsertarHerramientaGestion(md_herramienta_tec_gestion OBJGestion, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarHerramientaGestion(OBJGestion, ref MsgRes);
        }
        public List<vw__md_herramientaT_sin_cumplir> GetHerramientasSincumplir(Int32 id_herramienta_tec_med, Int32 tabla)
        {
            return BusClass.GetHerramientasSincumplir(id_herramienta_tec_med, tabla);
        }

        #endregion
    }
}