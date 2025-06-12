using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;

namespace AsaludEcopetrol.Models.MorNat
{
    public class Mortalidad
    {
        public int IdMortalidad { get; set; }
        public int Año { get; set; }
        public int Trimestre { get; set; }
        public int Mes { get; set; }
        public int Regional { get; set; }
        public int Unis { get; set; }
        public int CiudadSmed { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public int TipoBeneficiario { get; set; }
        public string NroCertificado { get; set; }
        public DateTime FechaFallecimiento { get; set; }
        public string CausaFallecimiento { get; set; }
        public string ConfirmadoDescartado { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public string Fuente { get; set; }
        public int Version { get; set; }
        public DateTime FechaDigita { get; set; }
        public string UsuarioDigita { get; set; }
        public string observacion { get; set; }

    }

    public class Natalidad
    {
        public int IdNatalidad { get; set; }
        public int IdRegional { get; set; }
        public string IdentificacionMadre { get; set; }
        public string CodPersonal { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int IdMes { get; set; }
        public int IdTrimestre { get; set; }
        public int Anio { get; set; }
        public int IdCiudadSmed { get; set; }
        public int IdLocalidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdUnis { get; set; }
        public string EdadGestacional { get; set; }
        public int? Peso { get; set; }
        public string ViaParto { get; set; }
        public int? Talla { get; set; }
        public string Sexo { get; set; }
        public string Apgar { get; set; }
        public string CausaEgreso { get; set; }
        public string ControlPrenatal { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public int VersionN { get; set; }

    }
}