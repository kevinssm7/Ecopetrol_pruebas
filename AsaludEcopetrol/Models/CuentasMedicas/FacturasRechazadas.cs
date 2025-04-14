using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class FacturasRechazadas
    {

        public int id_cargue_base { get; set; }

        public int id_cargue_dtll { get; set; }

        public int prestador { get; set; }

        public String nombre_prestador { get; set; }

        public String num_id_prestador { get; set; }

        public String num_factura { get; set; }

        public Decimal? valor_neto { get; set; }

        public int? estado_factura { get; set; }

        public int? id_ref_ciudades { get; set; }

        public String ciudad { get; set; }

        public int? id_ref_regional { get; set; }

        public String nombre_regional { get; set; }

        public int? id_auditor_asignado { get; set; }

        public String nom_auditor { get; set; }

        public int? id_analista_gestiona { get; set; }

        public String nom_analitica { get; set; }

        public String multiusuario { get; set; }

        public String id_diagnostico { get; set; }

        public String diagnostico { get; set; }

        public DateTime? fecha_recepcion_fac { get; set; }

        public DateTime? fecha_aprobacion { get; set; }

        public String estado_des { get; set; }

        public int? count_soportes { get; set; }

        public int? count_soportes_zip { get; set; }

        public DateTime? fecha_exp_factura { get; set; }

        public String tipoGastos { get; set; }

        public String ruta { get; set; }
    }
}