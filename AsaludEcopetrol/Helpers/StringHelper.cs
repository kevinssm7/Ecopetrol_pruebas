using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Helpers
{
    public class StringHelper
    {
        public const string STR_DefaultContentType = "text/HTML";

        public const string STR_RutaAdjuntos = "rutaAdjuntos";

        public const string STR_BackSlash = "\\";

        public const string STR_Coma = ",";

        public const string STR_Igual = "=";

        public const string STR_RutaTemporal = "RutaTemporalArchivos";

        public const string STR_RutaTemporal2 = "FileLocation";
        
        public const string STR_RutaTemporal3 = "RutaTemporalArchivos2";

        public const string STR_pattern = @"(@)?(%)";

        public const string STR_LETRAÑ = @"&#209;";

        public const string STR_LETRA1 = @"Ñ";

        public const string STR_CEDULA = @"CC";

        public const string STR_CEDULAEXTRANJERIA = @"CE";

        public const string STR_MENORSINIDENTIFICACION = @"MS";

        public const string STR_NUMEROUNICODEIDENTIFICACION = @"NU";

        public const string STR_PASAPORTE = @"PA";

        public const string STR_REGISTROCIVIL = @"RC";

        public const string STR_REGISTRONACIDOVIVO = @"RN";

        public const string STR_TARJETADEIDENTIDAD = "TI";

        public const string STR_VACIO = @"&nbsp;";

        public DateTime STR_FECHAVACIA = Convert.ToDateTime("01/01/0001 12:00:00 a.m.");

        public const string STR_FECHADEFECTO = "1800-01-01 00:00:00.000";

        public const string STR_TILDE = @"&#205;";

        public const string STR_TILDEO = @"&#243;";

        public const string STR_FECHADEFECTO1 = ("01/01/1800 12:00:00 a.m.");
    }
}