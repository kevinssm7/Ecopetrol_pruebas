using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECOPETROL_COMMON.UTILOBJECTS
{
    public class Logerroresevaluacionrips
    {
        public string codigo_prestador { get; set; }
        public string prestador { get; set; }

        public Int32 AC_Error_en_DX_Genero { get; set; }
        public Int32 AC_Num_factura_no_existe_en_AF { get; set; }
        public Int32 AC_Dx_no_corresponde_con_finalidad { get; set; }
        public Int32 AC_Usuario_debe_estar_en_US { get; set; }
        public Int32 AC_sin_DX { get; set; }

        public Int32 AP_Error_en_DX_Genero { get; set; }
        public Int32 AH_Error_en_DX_Genero { get; set; }
        public Int32 AP_Num_factura_no_existe_en_AF { get; set; }
        public Int32 AU_Num_factura_no_existe_en_AF { get; set; }
        public Int32 AH_Num_factura_no_existe_en_AF { get; set; }
        public Int32 AP_Usuario_debe_estar_en_US { get; set; }
        public Int32 AU_Usuario_debe_estar_en_US { get; set; }
        public Int32 AH_Usuario_debe_estar_en_US { get; set; }
        public Int32 AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado { get; set; }
        public Int32 AP_Sin_ambito_en_el_CUPS { get; set; }
        public Int32 AP_Sin_CUPS { get; set; }
        public Int32 AU_Sin_causa_basica_de_muerte { get; set; }
        public Int32 AU_Error_en_fecha_de_egreso { get; set; }
        public Int32 AU_Error_en_causa_externa { get; set; }
        public Int32 AH_Error_en_causa_externa { get; set; }
        public Int32 AH_Error_de_DX_no_aplica_R_Z { get; set; }
        public Int32 AU_Error_de_DX_no_aplica_R_Z { get; set; }
        public Int32 AN_Sin_fecha_de_muerte { get; set; }
        public Int32 AN_Sin_hora_de_muerte { get; set; }
        public Int32 Total_Errores { get; set; }
        public string Mes { get; set; }
        public string Año { get; set; }
        public string Regional { get; set; }
    }

    public class LogErroresevaluacionRipsAC
    {

        public Int32 numerrores { get; set; }
        public string detallelog { get; set; }
    }

    public class LogErroresevaluacionRipsAP
    {

        public Int32 numerrores { get; set; }
        public string detallelog { get; set; }
    }

    public class LogErroresevaluacionRipsAN
    {

        public Int32 numerrores { get; set; }
        public string detallelog { get; set; }
    }

    public class LogErroresevaluacionRipsAU
    {

        public Int32 numerrores { get; set; }
        public string detallelog { get; set; }
    }

    public class LogErroresevaluacionRipsAH
    {

        public Int32 numerrores { get; set; }
        public string detallelog { get; set; }
    }


}