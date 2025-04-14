using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Concurrencia
{
    public class ConcurrenciaEncuesta
    {
        #region PROPIEDADES

        private Facede.Facade _BusClass;
        public Facede.Facade BusClass
        {
            get
            {
                if (_BusClass != null)
                {
                    return _BusClass;
                }
                else
                {
                    return _BusClass = new Facede.Facade();
                }

            }
            set { _BusClass = value; }
        }

        private BussinesManager.SessionState _SesionVar;
        public BussinesManager.SessionState SesionVar
        {
            get
            {
                if (_SesionVar == null)
                {
                    _SesionVar = new BussinesManager.SessionState();
                }
                return _SesionVar;
            }
            set { _SesionVar = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();


        private ecop_concurrencia_encuesta _OBJEncuesta;
        public ecop_concurrencia_encuesta OBJEncuesta
        {
            get
            {
                if (_OBJEncuesta == null)
                {
                    return _OBJEncuesta = new ecop_concurrencia_encuesta();
                }
                else
                {
                    return _OBJEncuesta;
                }
            }

            set
            {
                _OBJEncuesta = value;
            }
        }

        public Int32 id_concurrencia { get; set; }

        public int id_encuesta_servicio_salud { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "Nombre quien responde la encuesta")]
        public string nombre_responde_encuesta { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "Canal por donde realiza encuesta")]
        public string canal { get; set; }

        public string numero_encuesta { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "1. La agilidad en el trámite para su ingreso al hospital o clínica la considera:")]
        public string pregunta1 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "2. El trato que recibió (amabilidad y respeto) por parte del personal de admisiones fue:")]
        public string pregunta2 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "3. El estado de la habitación asignada en cuanto a comodidad, orden y aseo fue:")]
        public string pregunta3 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "4. Está satisfecho con la alimentación suministrada por la institución según su condición clínica:")]
        public string pregunta4 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "5. La oportunidad en la respuesta a sus solicitudes por parte del personal de enfermería fue:")]
        public string pregunta5 { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "6. El trato (amabilidad y respeto) que recibió por parte del personal de enfermería fue:")]
        public string pregunta6 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "7. El trato (amabilidad y respeto) que recibió por parte de los médicos tratantes fue:")]
        public string pregunta7 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "8. ¿Recibió información clara y suficiente sobre procedimientos realizados durante su estancia en la institución?")]
        public string pregunta8 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "9. La agilidad en el trámite para su salida del hospital o clínica fue:")]
        public string pregunta9 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "10. Recibió información clara y suficiente sobre los medicamentos administrados durante su estancia en la institución.")]
        public string pregunta11 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "11. Recibió información clara y suficiente sobre los cuidados y controles posteriores a su estancia en el hospital o clínica.")]
        public string pregunta10 { get; set; }

        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }


        //[Display(Name = "JUSTIFICACIÓN DE CASOS CON CALIFICACIÓN REGULAR, MALA, NUNCA O CASI NUNCA")]
        [Display(Name = "Justificación de casos con calificación regular, mala, nunca o casi nunca")]
        public string justificacion { get; set; }

       
        public DateTime fecha_digita { get; set; }

      
        public string usuario_digita { get; set; }

        #endregion

        #region METODOS

        public void InsertarEncuestaConcurrencia( ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarEncuestaConcurrencia(OBJEncuesta, ref MsgRes);
        }


        #endregion
    }
}