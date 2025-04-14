using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Newtonsoft.Json;
using NHunspell;
using System.Collections.Generic;
using System.Linq;

namespace AsaludEcopetrol.Models.PruebasApi
{
    public class PruebaApi
    {

        public string NombreMetodo { get; set; }
        public double TiempoEjecucion { get; set; }


        private readonly string endpointUrl;
        private readonly string subscriptionKey;

        public string Correccion(string text)
        {
            try
            {
                // Construir la solicitud HTTP
                string url = $"{endpointUrl}?mode=proof&mkt=es-ES&text={Uri.EscapeDataString(text)}";

                // Crear la solicitud web
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;

                // Obtener la respuesta del servidor
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // Leer y deserializar la respuesta JSON
                        string jsonResponse = reader.ReadToEnd();
                        dynamic result = JsonConvert.DeserializeObject(jsonResponse);

                        // Obtener el texto corregido
                        string correctedText = result.flaggedTokens;
                        return correctedText;
                    }
                    else
                    {
                        return $"Error al corregir la ortografía: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error al corregir la ortografía: {ex.Message}";
            }
        }
    }

    public class PerfilRendimiento
    {
        public string NombreMetodo { get; set; }
        public double TiempoEjecucion { get; set; }
    }
}