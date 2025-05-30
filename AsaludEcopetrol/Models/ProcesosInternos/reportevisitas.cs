using ECOPETROL_COMMON.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.ProcesosInternos
{
    public class reportevisitas
    {

        public Int32 idcronograma { get; set; }
        public int tipoindicador { get; set; }
        public int idregional { get; set; }
        public DateTime? proximafecha { get; set; }
        public bool isreport = true;
        public List<ECOPETROL_COMMON.ENTIDADES.indicadores> ltaindicadores { get; set; }
        public List<ECOPETROL_COMMON.ENTIDADES.cronograma_visita_detalle_indicador> listacapitulos { get; set; }
        public string NomRegional { get; set; }
        public string NomPrestador { get; set; }
        public string NomAuditor { get; set; }
        public string NomRepresentante { get; set;} 
        public string NoContrato { get; set; }
        public string Observaciones { get; set; }
        public string Nit { get; set; }
        public DateTime? periodo_desde { get; set; }
        public DateTime? periodo_hasta { get; set; }
        public DateTime? fecha_final_visita { get; set; }
        public DateTime? fecha_visita { get; set; }
        public DateTime? fechamodificacion { get; set; }

    }
}