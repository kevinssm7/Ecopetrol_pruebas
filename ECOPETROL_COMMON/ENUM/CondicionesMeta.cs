using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOPETROL_COMMON.ENUM
{
    public enum CondicionesMeta
    {
        [Description("= (Igual)")]
        Igual = 1,
        [Description("<= (Menor o Igual)")]
        MenoroIgual = 2,
        [Description("< (Menor)")]
        Menor = 3,
        [Description(">= (Mayor o Igual)")]
        MayoroIgual = 4,
        [Description("> (Mayor)")]
        Mayor = 5,
        [Description("(Entre)")]
        Entre = 6,
    }
}
