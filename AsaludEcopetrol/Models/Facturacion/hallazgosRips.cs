using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Facturacion
{
    public class hallazgosRips
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


        private List<Ref_ips> _ListIPS;
        public List<Ref_ips> ListIPS
        {
            get
            {
                if (_ListIPS == null)
                {
                    _ListIPS = BusClass.GetPrstador();
                    _ListIPS = _ListIPS.OrderBy(X => X.Nombre).ToList();

                    return _ListIPS;
                }
                else
                {
                    return _ListIPS;
                }

            }

            set
            {
                _ListIPS = value;
            }
        }

        private hallazgo_RIPS _OBJHallazgo;
        public hallazgo_RIPS OBJHallazgo
        {
            get
            {
                if (_OBJHallazgo == null)
                {
                    return _OBJHallazgo = new hallazgo_RIPS();
                }
                else
                {
                    return _OBJHallazgo;
                }
                
            }

            set
            {
                _OBJHallazgo = value;
            }
        }

        private hallazgo_RIPS_detalle _OBJHallazgoDetalle;
        public hallazgo_RIPS_detalle OBJHallazgoDetalle
        {
            get
            {
                if (_OBJHallazgoDetalle == null)
                {
                    return _OBJHallazgoDetalle = new hallazgo_RIPS_detalle();
                }
                else
                {
                    return _OBJHallazgoDetalle;
                }
               
            }

            set
            {
                _OBJHallazgoDetalle = value;
            }
        }

        private hallazgo_RIPS _OBJRips;
        public hallazgo_RIPS OBJRips
        {
            get
            {
                if (_OBJRips == null)
                {
                    return _OBJRips = new hallazgo_RIPS();
                }
                else
                {
                    return _OBJRips;
                }
               
            }

            set
            {
                _OBJRips = value;
            }
        }


        public Int32 id_hallazgo_RIPS { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DE RADICADO RIPS")]
        public String numero_factura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DE REPORTE HALLAZGO")]
        public DateTime? fecha_factura { get; set; }

        public DateTime? fecha_facturaOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA RECEPCION")]
        public DateTime? fecha_recepcion { get; set; }

        public DateTime? fecha_recepcionOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PRESTADOR")]
        public Int32? proveedor { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public Int32? id_regional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public Int32 ciudad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PRESTADOR")]
        public Int32 Prestador { get; set; }


        public DateTime fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        [Required(ErrorMessage = "***")]
        public ICollection<hallazgosRipsDetalle> Detalle { get; set; }

        private ICollection<Ref_hallazgos_RIPS> _productos;
        public ICollection<Ref_hallazgos_RIPS> Productos
        {
            get
            {
                if (_productos == null)
                {
                    return _productos = BusClass.GetRefHallazgos();
                }
                else
                {
                    return _productos;
                }

            }

            set
            {
                _productos = value;
            }
        }

        public Int32 id_devolucion_factura_detalle { get; set; }

        
        #endregion

        #region METODOS

        public hallazgosRips()
        {
            this.Detalle = new HashSet<hallazgosRipsDetalle>();
        }

        public Int32 InsertarHallazgos(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarHallazgos(OBJHallazgo, ref MsgRes);
        }

        public Int32 InsertarHallazgosDetalle( ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarHallazgosDetalle(OBJHallazgoDetalle, ref MsgRes);
        }
        
        public void ActualizaHallazgosRips(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaHallazgosRips(OBJRips, ref MsgRes);
        }

        #endregion
    }
}