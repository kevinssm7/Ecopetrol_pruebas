using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Facturacion
{
    public class FacturasinCenso
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

       private factura_sin_censo _ObjFac;
       public factura_sin_censo ObjFac
        {
            get
            {
                if (_ObjFac==null)
                {
                    return _ObjFac = new factura_sin_censo();
                }
                else
                {
                    return _ObjFac;
                }
              
            }

            set
            {
                _ObjFac = value;
            }
        }
        
        private List<factura_sin_censo> _ListFacturas;
        public List<factura_sin_censo> ListFacturas
        {
            get
            {
                if (_ListFacturas == null)
                {
                    return _ListFacturas = new List<factura_sin_censo>();
                }
                else
                {
                    return _ListFacturas;
                }
                
            }

            set
            {
                _ListFacturas = value;
            }
        }

        private List<vw_costo_evitado> _ListCostoEvitado;
        public List<vw_costo_evitado> ListCostoEvitado
        {
            get
            {
                if (_ListCostoEvitado == null)
                {
                    return _ListCostoEvitado = BusClass.CostoEvitado(id_factura_sin_censo,ref MsgRes);
                }
                else
                {
                    return _ListCostoEvitado;
                }
                
            }

            set
            {
                _ListCostoEvitado = value;
            }
        }

        private List<vw_facturas_diagnosticos> _ListDiagnostico;
        public List<vw_facturas_diagnosticos> ListDiagnostico
        {
            get
            {
                if (_ListDiagnostico == null)
                {
                    return _ListDiagnostico = BusClass.DiagnosticosCuentas(id_factura_sin_censo, ref MsgRes);
                }
                else
                {
                    return _ListDiagnostico;
                }
                
            }

            set
            {
                _ListDiagnostico = value;
            }
        }

        private factura_sin_censo_cos_evitado _Obj_costoEvitado;
        public factura_sin_censo_cos_evitado Obj_costoEvitado
        {
            get
            {
                if (_Obj_costoEvitado == null)
                {
                    return _Obj_costoEvitado = new factura_sin_censo_cos_evitado();
                }
                else
                {
                    return _Obj_costoEvitado;
                }
               
            }

            set
            {
                _Obj_costoEvitado = value;
            }
        }

        private factura_sin_censo_diagnosticos _Obj_diagnosticos;
        public factura_sin_censo_diagnosticos Obj_diagnosticos
        {
            get
            {
                if (_Obj_diagnosticos == null)
                {
                    return _Obj_diagnosticos = new factura_sin_censo_diagnosticos();
                }
                else
                {
                    return _Obj_diagnosticos;
                }
                
            }

            set
            {
                _Obj_diagnosticos = value;
            }
        }


        private List<vw_facturas_sin_auditar> _ListFacturasSinId;
        public List<vw_facturas_sin_auditar> ListFacturasSinId
        {
            get
            {
                if (_ListFacturasSinId == null)
                {
                    _ListFacturasSinId = BusClass.FacturasporAuditar();
                    _ListFacturasSinId = _ListFacturasSinId.Where(x => x.id_factura_sin_censo == id_factura_sin_censo).ToList();

                    return _ListFacturasSinId;
                }
                else
                {
                    return _ListFacturasSinId;
                }

            }

            set
            {
                _ListFacturasSinId = value;
            }
        }

        private List<factura_sin_censo> _LstFacturasCensoId;
        public List<factura_sin_censo> LstFacturasCensoId
        {
            get
            {
                if (_LstFacturasCensoId == null)
                {
                    return _LstFacturasCensoId = new List<factura_sin_censo>();
                }
                else
                {
                    return _LstFacturasCensoId;
                }

            }

            set
            {
                _LstFacturasCensoId = value;
            }
        }

        public Int32 id_factura_sin_censo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO FACTURA")]
        public String numero_factura { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA FACTURA")]
        public DateTime? fecha_factura { get; set; }
        public DateTime? fecha_facturaOK { get; set; }

        [Display(Name = "FECHA FACTURA")]
        public String fecha_factura_result { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA RECEPCION")]
        public DateTime? fecha_recepcion { get; set; }
        public DateTime? fecha_recepcionOK { get; set; }

        [Display(Name = "FECHA RECEPCION")]
        public String fecha_recepcion_result { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "IPS")]
        public String ips { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD IPS")]
        public int ciudad_ips { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public Int32? id_regional { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "PRESTADOR")]
        public String Prestador { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public Int32 ciudad { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR FACTURA")]
        public String valor_factura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO")]
        public String tipo_factura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR DEFINITIVO FACTURA")]
        public String valor_factura_definitiva { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "INGRESAR COSTO EVITADO")]
        public String tiene_costo_evitado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "INGRESAR DIAGNOSTICOS")]
        public String diagnostico { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "COSTO EVITADO")]
        public String costo_evitado { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "CUPS")]
        public String id_cups { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "CANTIDAD")]
        public int cantidad_glosa { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR UNITARIO")]
        public float valor_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR TOTAL")]
        public float valor_total { get; set; }


        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "OBSERVACION")]
        public String observacion { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "RESPONSABLE")]
        public String responsable_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "MOTIVO")]
        public int motivo_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIAGNOSTICO")]
        public String cie10 { get; set; }

        public DateTime fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        public String auditada { get; set; }

        [Display(Name = "MULTIUSUARIO")]
        public String tipo { get; set; }    

        
        [Display(Name = "NUMERO DE DOCUMENTO:")]
        public String multiusuario { get; set; }
        
        public String numdocumento_multiusuario { get; set; }

        [Required(ErrorMessage = "SELECCIONE UNA OPCION")]
        public String id_tipo_busqueda { get; set; }

        [Required(ErrorMessage = "SELECCIONE UNA OPCION")]
        [Display(Name = "SELECCIONE ALERTA:")]
        public String id_tipo_busqueda2 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ID DEVOLUCION:")]
        public Int32 id_devolucion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ID RIPS:")]
        public Int32 id_rips { get; set; }
        
        #endregion

        #region METODOS


        public Int32 InsertarFacturaSinCenso( ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFacturaSinCenso(ObjFac, ref MsgRes);
        }


        public void ConsultaFactura(Int32 id_factura)
        {
            factura_sin_censo ObjCenso = new factura_sin_censo();
            ObjCenso.id_factura_sin_censo = id_factura;
           // ListFacturas = BusClass.ConsultaFacturasSinAudi(id_factura);
            foreach (var item in ListFacturasSinId)
            {
                id_factura_sin_censo = item.id_factura_sin_censo;
                numero_factura = item.numero_factura;
                fecha_factura_result = item.fecha_factura.Value.ToString("dd/MM/yyyy"); ;
                fecha_recepcion_result = item.fecha_recepcion.Value.ToString("dd/MM/yyyy"); ;
                ips = item.ips;
                valor_factura = Convert.ToString(item.valor_factura);
                tipo_factura = item.tipo_factura;
                tipo = item.tipo;
            }

        }


        public Int32 InsertarCostoEvitado( ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarCostoEvitado(Obj_costoEvitado, ref MsgRes);
        }
        public Int32 InsertarDiagnosticoCuentas( ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarDiagnosticoCuentas(Obj_diagnosticos, ref MsgRes);
        }

        public void ActualizaFacturaAuditada(factura_sin_censo ObjAudi, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaFacturaAuditada(ObjAudi, ref MsgRes);
        }


        public List<factura_sin_censo> ConsultaFacturaNumero(String Numero_factura)
        {
            LstFacturasCensoId = BusClass.ConsultaFacturaNumero(Numero_factura);

            return LstFacturasCensoId;
        }
             

        #endregion
    }
}