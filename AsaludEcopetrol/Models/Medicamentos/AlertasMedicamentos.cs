using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class AlertasMedicamentos
    {
        public int? id_alerta { get; set; }
        public int? id_registro { get; set; }
        public int? id_detalle { get; set; }
        public string tienefactura { get; set; }
        public string numero_factura { get; set; }
        public decimal valor_facturado { get; set; }
        public string cantPertinente { get; set; }
        public string diagnosticoHC { get; set; }
        public string medicamento_pertinente { get; set; }
        public string cantCorrespondePres { get; set; }
        public string desviacion { get; set; }
        public string causa_desviacion { get; set; }
        public string responsable_desviacion { get; set; }
        public string plan_accion { get; set; }
        public string confirmacionAlerta { get; set; }
        public string observaciones { get; set; }
        public string tipoDato { get; set; }
        public string tiene_soporte { get; set; }
        public string observacion_soporte { get; set; }
        public DateTime? fecha_recepcion_soporte { get; set; }
    }
}