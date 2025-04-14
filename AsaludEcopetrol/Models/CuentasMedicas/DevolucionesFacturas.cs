using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class DevolucionesFacturas
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

        private SessionState _SesionVar;
        public SessionState SesionVar
        {
            get
            {
                if (_SesionVar == null)
                {
                    _SesionVar = new SessionState();
                }
                return _SesionVar;
            }
            set { _SesionVar = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        private factura_devolucion_gestionadas _OBJdEV;
        public factura_devolucion_gestionadas OBJdEV
        {
            get
            {
                if (_OBJdEV == null)
                {
                    return _OBJdEV = new factura_devolucion_gestionadas();
                }
                else
                {
                    return _OBJdEV;
                }
                
            }

            set
            {
                _OBJdEV = value;
            }
        }

        public int? id_factura_devolucion_gestionadas { get; set; }

        public int? id_devolucion_factura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DE FACTURA")]
        public String numero_factura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR FACTURA")]
        public String valor_factura { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "OBSERVACIONES")]
        public String observaciones { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA  FACTURA")]
        public DateTime? fecha_factura { get; set; }
        public DateTime? fecha_facturaok { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA  RECEPCION ASALUD")]
        public DateTime? fecha_recepcion_asalud { get; set; }
        public DateTime? fecha_recepcion_asaludok { get; set; }

        public DateTime fecha_digita { get; set; }
        public String usuario_digita { get; set; }

  


        #endregion

        #region METODOS

        public Int32 InsertarDevolucionGestionadas( ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarDevolucionGestionadas(OBJdEV, ref MsgRes);
        }

        #endregion
    }
}