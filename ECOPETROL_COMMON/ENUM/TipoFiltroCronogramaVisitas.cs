using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOPETROL_COMMON.ENUM
{
    public enum TipoFiltroCronogramaVisitas
    {

        [Description("Pendientes")]
        Pendientes = 1,
        [Description("Tramitadas")]
        Tramitadas = 2,
        [Description("Novedades")]
        Novedades = 3,
    }
}
