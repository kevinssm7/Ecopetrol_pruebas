using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class glosa_detalle
    {
        public int id_md_glosa { get; set; }

        public int Motivo_glosa { get; set; }

        public String Valor_glosa { get; set; }

        public String Observaciones { get; set; }

        public DateTime? fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        public virtual GestionMedicamentos GestionMedicamentos { get; set; }

    }

    #region ViewModels
    public partial class glosaDetalleViewModel
    {
        public int id_md_glosa { get; set; }

        public int motivo_glosa { get; set; }

        public String valor_glosa { get; set; }

        public String observaciones { get; set; }

        public DateTime? fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        public bool Retirar { get; set; }
    }
    #endregion
}