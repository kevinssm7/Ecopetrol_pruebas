using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class SoportesClinicos
    {
        public int id_cargue_base { get; set; }
        public int id_cargue_dtll { get; set; }
        public string num_factura { get; set; }
        public string nom_documento { get; set; }
        public string ruta { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string usuario { get; set; }
    }
}