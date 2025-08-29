using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.EventosSalud
{

    public class EventosSalud
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
        #endregion

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #region CAMPOS
        public int? id_evento { get; set; }
        public int? id_cargue { get; set; }
        public int? id_concurrencia { get; set; }
        public int? id_planMejora { get; set; }
        public int? id_evolucion_concurrencia { get; set; }
        public int? Año { get; set; }
        public int? IdMes { get; set; }
        public string Mes { get; set; }
        public DateTime? FechaReporte { get; set; }
        public DateTime? FechaOcurrenciaEvento { get; set; }
        public int? RegionalReporta { get; set; }
        public int? LocalidadServiciosSalud { get; set; }
        public string NombreReportante { get; set; }
        public string IdentificacionReportante { get; set; }
        public string NombrePrestadorEvento { get; set; }
        public string CodigoSAPPrestador { get; set; }
        public string NombreMunicipio { get; set; }
        public string CodigoMunicipal { get; set; }
        public int? RegionalBeneficiario { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string NombreCompleto { get; set; }
        public int? Edad { get; set; }
        public int? FuenteReporte { get; set; }
        public int? AmbitoOcurrenciaEvento { get; set; }
        public string DescripcionEvento { get; set; }
        public int? ClasificacionEvento { get; set; }
        public int? CategoriaEvento { get; set; }
        public int? SubcategoriaEvento { get; set; }
        public int? ResultadoNegativoMedicacion { get; set; }
        public int? ConfirmacionEventoAdverso { get; set; }
        public int? SeveridadDesenlace { get; set; }
        public int? ProbabilidadRepeticion { get; set; }
        public int? ConceptoAuditoria { get; set; }
        public string GestionRegional { get; set; }
        public int? PlanMejoraGenerado { get; set; }
        public string CostoNoCalidad { get; set; }
        public string DescripcionCostoNoCalidad { get; set; }
        #endregion


    }
}