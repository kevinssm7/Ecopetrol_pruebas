using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System.ComponentModel.DataAnnotations;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class Eventos_en_salud_detalle
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

        private List<ecop_concurrencia_eventos_en_salud_detalle> _LstEventosSaludDetalle;
        public List<ecop_concurrencia_eventos_en_salud_detalle> LstEventosSaludDetalle
        {
            get
            {
                if (_LstEventosSaludDetalle == null)
                {
                    _LstEventosSaludDetalle = new List<ecop_concurrencia_eventos_en_salud_detalle>();
                }

                return _LstEventosSaludDetalle;
            }
            set { _LstEventosSaludDetalle = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        public Int32 id_ecop_concurrencia_eventos_en_asalud_detalle { get; set; }

        public Int32 id_ecop_concurrencia_eventos_en_asalud { get; set; }



        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "acciones de mejora")]
        public String accion_mejora { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "responsable")]
        public String responsable { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "fecha_cumplimiento")]
        public String fecha_cumplimiento { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "seguimiento")]
        public String seguimiento { get; set; }


        public void SetearvaloresAnalisisDetalle(int idconcu)
        {

            ecop_concurrencia_eventos_en_salud_detalle ObjanalisisDetalle = new ecop_concurrencia_eventos_en_salud_detalle();
            ObjanalisisDetalle.id_ecop_concurrencia_eventos_en_salud = idconcu;
            LstEventosSaludDetalle = BusClass.ConsultaAnalisisEventosenSaludDetalle(ObjanalisisDetalle, ref MsgRes);
 

        }

        #endregion



    }
}