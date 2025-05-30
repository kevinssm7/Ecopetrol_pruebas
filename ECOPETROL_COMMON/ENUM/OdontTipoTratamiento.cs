using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOPETROL_COMMON.ENUM
{
    public enum OdontTipoTratamiento
    {
        [Description("Ortodoncia")]
        Ortodoncia = 1,
        [Description("Protesis Fija")]
        Fija = 2,
        [Description("Protesis Removible")]
        Removible = 3,
        [Description("Endodoncia")]
        Endodoncia = 4,
    }
}
