using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Facturacion
{
    public class FacturaDevolucion
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

        private List<Ref_regional> _refRegional;
        public List<Ref_regional> RefRegional
        {
            get
            {
                if (_refRegional == null)
                {
                    _refRegional = BusClass.GetRefRegion();

                    return _refRegional;
                }
                else
                {
                    return _refRegional;
                }

            }

            set
            {
                _refRegional = value;
            }
        }

        private List<Ref_ciudades> _ListCiudades;
        public List<Ref_ciudades> ListCiudades
        {
            get
            {
                if (_ListCiudades == null)
                {
                    return _ListCiudades = new List<Ref_ciudades>();
                }
                else
                {
                    return _ListCiudades;
                }

            }

            set
            {
                _ListCiudades = value;
            }
        }

        private factura_devolucion _OBJFactura;
        public factura_devolucion OBJFactura
        {
            get
            {
                if (_OBJFactura == null)
                {
                    return _OBJFactura = new factura_devolucion();
                }
                else
                {
                    return _OBJFactura;
                }

            }

            set
            {
                _OBJFactura = value;
            }
        }

        private factura_devolucion_detalle _OBJFacturaDetalle;
        public factura_devolucion_detalle OBJFacturaDetalle
        {
            get
            {
                if (_OBJFacturaDetalle == null)
                {
                    return _OBJFacturaDetalle = new factura_devolucion_detalle();
                }
                else
                {
                    return _OBJFacturaDetalle;
                }

            }

            set
            {
                _OBJFacturaDetalle = value;
            }
        }

        private List<Ref_ips_cuentas> _ListIPS;
        public List<Ref_ips_cuentas> ListIPS
        {
            get
            {
                if (_ListIPS == null)
                {
                    _ListIPS = BusClass.GetPrstadorCuentas();
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

        private List<Ref_ips_cuentas> _ListIPS2;
        public List<Ref_ips_cuentas> ListIPS2
        {
            get
            {
                if (_ListIPS2 == null)
                {
                    return _ListIPS2 = new List<Ref_ips_cuentas>();
                }
                else
                {
                    return _ListIPS2;
                }

            }

            set
            {
                _ListIPS2 = value;
            }
        }

        private List<factura_devolucion> _ListFacturaId;
        public List<factura_devolucion> ListFacturaId
        {
            get
            {
                if (_ListFacturaId == null)
                {
                    return _ListFacturaId = new List<factura_devolucion>();
                }
                else
                {
                    return _ListFacturaId;
                }

            }

            set
            {
                _ListFacturaId = value;
            }
        }

        private List<vw_tablero_urgencias_ok> _LstTableroUrgencias;
        public List<vw_tablero_urgencias_ok> LstTableroUrgencias
        {
            get
            {
                if (_LstTableroUrgencias == null)
                {

                    _LstTableroUrgencias = BusClass.ConsultaTablerUrgenciasOk();



                    return _LstTableroUrgencias;
                }
                else
                {
                    return _LstTableroUrgencias;
                }

            }

            set
            {
                _LstTableroUrgencias = value;
            }
        }

        private List<ref_componente_hospitalario> _ListComponente;
        public List<ref_componente_hospitalario> ListComponente
        {
            get
            {
                if (_ListComponente == null)
                {
                    _ListComponente = BusClass.GetComponentesHospitalarios();
                    _ListComponente = _ListComponente.OrderBy(X => X.id_componente).ToList();

                    return _ListComponente;
                }
                else
                {
                    return _ListComponente;
                }

            }

            set
            {
                _ListComponente = value;
            }
        }



        public Int32 id_devolucion_factura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO FACTURA")]
        public String NumeroFactura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR FACTURA")]
        public String ValorFactura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PRESTADOR")]
        public Int32 Prestador { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA DEVOLUCION")]
        public DateTime? fecha_devolucion { get; set; }

        public DateTime? fecha_devolucionOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public Int32? id_regional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public Int32? ciudad { get; set; }

        public DateTime fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DEVOLUCION")]
        public String tipo { get; set; }

        public String ModuloPrestadores { get; set; }

        public int id_af { get; set; }

        public DateTime? fecha_dev { get; set; }

        [Required(ErrorMessage = "***")]
        public ICollection<FacturaDevolucionDetalle> Detalle { get; set; }

        private ICollection<Ref_motivo_devolucion_fac> _productos;
        public ICollection<Ref_motivo_devolucion_fac> Productos
        {
            get
            {
                if (_productos == null)
                {
                    return _productos = BusClass.GetMotivoDevolucionFac();
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

        private ICollection<Ref_cuentas_glosa> _productos2;
        public ICollection<Ref_cuentas_glosa> Productos2
        {
            get
            {
                if (_productos2 == null)
                {
                    return _productos2 = BusClass.GetCuentaGlosa();
                }
                else
                {
                    return _productos2;
                }

            }

            set
            {
                _productos2 = value;
            }
        }

        public Int32 id_devolucion_factura_detalle { get; set; }

        public int? tipoDevolucion { get; set; }

        #endregion

        #region METODOS

        public FacturaDevolucion()
        {
            this.Detalle = new HashSet<FacturaDevolucionDetalle>();
        }


        public List<Ref_motivo_devolucion_fac> GetMotivoDevolucionFac()
        {
            return BusClass.GetMotivoDevolucionFac();
        }


        public Int32 InsertarDevolucionFacturas(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarDevolucionFacturas(OBJFactura, ref MsgRes);
        }

        public Int32 InsertarDevolucionFacturasDetalle(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarDevolucionFacturasDetalle(OBJFacturaDetalle, ref MsgRes);
        }

        public void ConsultaLista(int opc, string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            ListCiudades = BusClass.GetCiudades();
            ListCiudades = ListCiudades.Where(x => x.id_ref_regional == Convert.ToInt32(strFiltro)).ToList();
        }

        public void ConsultaListaIps(int opc, string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            ListIPS2 = BusClass.GetPrstadorCuentas();
            ListIPS2 = ListIPS2.Where(x => x.id_ref_ciudades == Convert.ToInt32(strFiltro)).ToList();
            ListIPS2 = ListIPS2.OrderBy(X => X.Nombre).ToList();
        }

        public List<factura_devolucion> ConsultaDevolucionesFactura(String Numero_factura)
        {
            ListFacturaId = BusClass.ConsultaDevolucionesFactura(Numero_factura);

            return ListFacturaId;
        }

        public List<managmentprestadoresFacturasResult> GetFacturasByEstadoAceptadas(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByEstadoAceptadas(idestado, ref MsgRes);
        }

        public List<managmentprestadoresFacturas_devueltasResult> GetFacturasByEstadoDevueltas(int idestado, int? id, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByEstadoDevueltas(idestado, id, ref MsgRes);
        }

        public List<managmentprestadoresNumeroFacturaResult> GetConsultaNumeroFactura(String numeroFac)
        {
            return BusClass.GetConsultaNumeroFactura(numeroFac);
        }


        public List<factura_devolucion> GetfacturadevolucionByIdFactura(int idfactura)
        {
            return BusClass.GetfacturadevolucionByIdFactura(idfactura);
        }


        #endregion
    }
}