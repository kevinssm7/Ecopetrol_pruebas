using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.FFMM
{
    public class CuentasRecepcion
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

        public String Items { get; set; }
        public int id_ref_ffmm_radicacion_Cuentas { get; set; }
        public DateTime? fecha_presentacion_factura { get; set; }
        public DateTime? fecha_presentacion_facturaok { get; set; }

        public String optradio { get; set; }
        public String unidad_regionalizadora { get; set; }

        public String unidad_satelite { get; set; }
        [Required(ErrorMessage = "***")]
        public String proveedor { get; set; }
        [Required(ErrorMessage = "***")]
        public String proveedor2 { get; set; }
        [Required(ErrorMessage = "***")]
        public Int32 nit { get; set; }

        public String prefijo_factura { get; set; }

        public Int32 numero_factura { get; set; }

        public Decimal vlr_factura { get; set; }

        public Decimal vlr_nota_credito { get; set; }

        public Decimal vlr_atencion { get; set; }

        public String devolucion { get; set; }
        [Required(ErrorMessage = "***")]
        public Int32 cod_dane_municipio { get; set; }

        [Required(ErrorMessage = "***")]
        public Int32 cod_dane_municipio_proveedor { get; set; }

        [Required(ErrorMessage = "***")]
        public Int32 cod_dane_departamento { get; set; }


        [Required(ErrorMessage = "***")]
        public Int32 cod_dane_departamento_proveedor { get; set; }

        [Required(ErrorMessage = "***")]
        public Int32 codigohabilitacion { get; set; }
        [Required(ErrorMessage = "***")]
        public String direccion { get; set; }
        [Required(ErrorMessage = "***")]
        public int telefono { get; set; }
        [Required(ErrorMessage = "***")]
        public String najunombre { get; set; }

        public DateTime fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        public string año_mes_radicado { get; set; }
        public string año { get; set; }
        public string mes { get; set; }

        private List<vw_ffmm_consulta_radicacion_ips> _ListRecepcionIps;
        public List<vw_ffmm_consulta_radicacion_ips> ListRecepcion
        {
            get
            {
                if (_ListRecepcionIps == null)
                {
                    return _ListRecepcionIps = new List<vw_ffmm_consulta_radicacion_ips>();
                }
                else
                {
                    return _ListRecepcionIps;
                }

            }

            set
            {
                _ListRecepcionIps = value;
            }
        }

        public int id_factura { get; set; }

        #endregion

        #region FUNCIONES

        public Int32 InsertarFFMMRadicacion(ffmm_Cuentas_radicacion OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFFMMRadicacion(OBJ, ref MsgRes);
        }

        public void BuscarRadicacionIPs()
        {
            ListRecepcion = BusClass.GetOdontogRadicacionIPS();
            ListRecepcion = ListRecepcion.Where(x => x.numero_factura == numero_factura).ToList();
        }

        public Int32 InsertarFFMMref_proveedor(Ref_ffmm_prestadores_proveedor OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFFMMref_proveedor(OBJ, ref MsgRes);
        }

        public List<managmentFFMMfacturasRadicadasid_dtllResult> GetRecepcionFacturasDTLLFFMMId(Int32 id_cargue_dtll, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetRecepcionFacturasDTLLFFMMId(id_cargue_dtll, ref MsgRes).ToList();

        }

        public List<Management_actualizar_FacturaDigResult> ActualizarFFMMFacturas(Int32 Id_factura, Int32 estado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ActualizarFFMMFacturas(Id_factura, estado, ref MsgRes);
        }


        #endregion

    }
}