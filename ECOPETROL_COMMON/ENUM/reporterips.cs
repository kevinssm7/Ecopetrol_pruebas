using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOPETROL_COMMON.ENUM
{
    public class reporterips
    {
        public string codhabilitacion { get; set; }
        public string razon_social { get; set; }
        public string muni_nombre { get; set; }
        public int cantidad { get; set; }
        public double registros_facturados_oportunamente { get; set; }
        public double porcentaje_oportunidad { get; set; }
        public int Errores_dx { get; set; }
        public int Errores_pc { get; set; }
        public int Errores_rc { get; set; }
        public int Total_Errores { get; set; }
        public int Registros_unicos_con_error { get; set; }
        public int Registros_sin_error { get; set; }
        public double porcentaje_calidad_rips { get; set; }
        public string nombreRegional { get; set; }
        public DateTime? fecha_cargue { get; set; }
    }
}
