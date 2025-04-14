using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace AsaludEcopetrol.Helpers
{
    public class ArchivosHelper
    {
        /// <summary>
        /// Obtiene la ruta donde se va guardar el fichero
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public static string ObtenerRutaDefinitiva(string nombreArchivo)
        {
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;

            // Inicializar objetos
            strRutaDefinitiva = ConfigurationManager.AppSettings[StringHelper.STR_RutaAdjuntos];

            sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
            if (!strRutaDefinitiva.EndsWith(StringHelper.STR_BackSlash))
            {
                sbRutaDefinitiva.Append(StringHelper.STR_BackSlash);
            }
            sbRutaDefinitiva.Append(nombreArchivo);

            return sbRutaDefinitiva.ToString();
        }

        /// <summary>
        /// Obtener la ruta temporal para guardar archivos
        /// </summary>
        /// <returns>string con la ruta temporal, leida desde parametros</returns>
        public static string ObtenerDirectorioTemporal()
        {
            string strRutaTemporal = string.Empty;
            strRutaTemporal = ConfigurationManager.AppSettings[StringHelper.STR_RutaTemporal];

            //if (!strRutaTemporal.EndsWith(StringHelper.STR_BackSlash))
            //{
            //    strRutaTemporal = string.Format("{0}{1}", strRutaTemporal, StringHelper.STR_BackSlash);
            //}
            strRutaTemporal = string.Format("{0}{1}", strRutaTemporal, StringHelper.STR_BackSlash);
            return strRutaTemporal;
        }

        public static string ObtenerDirectorioTemporal2()
        {
            string strRutaTemporal = string.Empty;
            strRutaTemporal = ConfigurationManager.AppSettings[StringHelper.STR_RutaTemporal2];

            //if (!strRutaTemporal.EndsWith(StringHelper.STR_BackSlash))
            //{
            //    strRutaTemporal = string.Format("{0}{1}", strRutaTemporal, StringHelper.STR_BackSlash);
            //}
            strRutaTemporal = string.Format("{0}", strRutaTemporal, StringHelper.STR_BackSlash);
            return strRutaTemporal;
        }

    }
}