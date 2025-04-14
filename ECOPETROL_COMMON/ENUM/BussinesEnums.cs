using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOPETROL_COMMON.ENUM
{
  public  class BussinesEnums
    {
        public enum EnumTipoEjecucion
        {
            SP,
            Query
        }
        public enum EnumTipoRespuesta
        {
            Error,
            Correcto
        }

        public enum EnumTipoDeLog
        {
            User,
            Trace,
            Error,
            DataBaseError,
            DataBaseTime,
            CriticsChangeClient
        }
    }
}
