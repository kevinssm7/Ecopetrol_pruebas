using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Facturacion
{
    public class FacturaDevolucionDetalle
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

        public Int32 id_devolucion_factura_detalle { get; set; }

        public Int32 id_devolucion_factura { get; set; }

      

        [Required(ErrorMessage = "***")]
        [Range(0, 999999999)]
        [Display(Name = "VALOR GLOSA")]
        public String ValorGlosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "MOTIVO")]
        public int? Id_motivo { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "DESCRIPCION")]
        public String Descripcion_devolucion { get; set; }

     
        public String Observaciones { get; set; }

      


        #endregion

        #region METODOS


      

        #endregion
    }
}