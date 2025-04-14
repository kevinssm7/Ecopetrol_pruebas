using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Campañas
{
    public class Campaña
    {

    }

    public class CreacionCampaña
    {
        public int? idCampana { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string[][] ArrayBidimensionalPreguntas { get; set; }
        public string[][] ArrayRespuestaBreve { get; set; }
        public string[][][] ArrayOpcionMultiple { get; set; }
        public string[][][] ArrayCasillasVerificacion { get; set; }
        public string[][][] ArrayListaDesplegable { get; set; }
        public string[][] ArrayCargaArchivos { get; set; }
        public string[][] arrayCargaFecha { get; set; }







        public string[][] ArrayBidimensionalPreguntasNuevas { get; set; }
        public string[][] ArrayRespuestaBreveNuevas { get; set; }
        public string[][][] ArrayOpcionMultipleNuevas { get; set; }
        public string[][][] ArrayCasillasVerificacionNuevas { get; set; }
        public string[][][] ArrayListaDesplegableNuevas { get; set; }
        public string[][] ArrayCargaArchivosNuevas { get; set; }
        public string[][] arrayCargaFechaNuevas { get; set; }
    }

    public class GuardarCampaña
    {
        public int idCampaña { get; set; }

        public int existeArchivos { get; set; }

        public string[][] arrayListasPreguntas { get; set; }
        public string[][] ArraySimples { get; set; }
        public string[] conteoArchivos { get; set; }
        public string[][] ArraySimplesFechas { get; set; }
        public string[][] ArrayListasMultiple { get; set; }
        public string[][] ArrayListasSelect { get; set; }
        public string[][][] ArrayListasVerificacion { get; set; }
        public List<HttpPostedFileBase> ArraySimplesArchivos { get; set; }

        public HttpPostedFileBase[] ArraySimplesArchivos2 { get; set; }

    }

}