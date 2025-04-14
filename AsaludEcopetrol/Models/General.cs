using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models
{
    public class General
    {
        /// <summary>
        /// Metodo que genera una alerta
        /// </summary>
        /// <param name="tipomensaje"></param>
        /// <param name="msjprincipal"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public string MsgRespuesta(string tipomensaje, string msjprincipal, string mensaje)
        {
            string result = "";
            result += "<div class='alert alert-" + tipomensaje + " row'>" +
                   "<strong>" + msjprincipal + "&nbsp; </strong>" + mensaje + " </div>";
            return result;
        }

    }
}