using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class GestionObligaciones
    {

        #region Propiedades

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

        public int id_obligaciones_contractuales { get; set; }

        public int id_obligaciones_contractuales_detalle { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA AUDITORIA")]
        public DateTime? fecha_auditoria { get; set; }
        public DateTime? fecha_auditoriaOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE AUDITOR")]
        public String nombre_auditor { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public int ciudad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public int id_regional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE AUDITADO")]
        public String nombre_auditado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PUNTOS DE DISPENSACION")]
        public String nombre_farmacia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "DIRECCION FARMACIA")]
        public String direccion_farmacia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TELEFONO FARMACIA")]
        public String telefono_farmacia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "MATRICULA MERCANTIL")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,50}$", ErrorMessage = "Maximo 50 caracteres")]
        public String matricula_mercantil { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "UNIS")]
        public String unis { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FUERZA")]
        public String fuerza { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "RESULTADO")]
        public Decimal? resultado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "HALLAZGOS")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        public String hallazgos { get; set; }

        public DateTime fecha_ingreso { get; set; }

        public String usuario_ingreso { get; set; }

        public String descripcion { get; set; }

        public int id_md_ref_obligaciones { get; set; }

        public int peso { get; set; }

        public Int32 valor { get; set; }

        public String observaciones { get; set; }

        public String file { get; set; }

        public String TIENE_DOCUMENTO { get; set; }


        private ICollection<GestionIndicadores_detalles> Detalle { get; set; }


        private List<vw_md_Ref_indicador> _ListMDIndicador;
        public List<vw_md_Ref_indicador> ListMDIndicador
        {
            get
            {
                if (_ListMDIndicador == null)
                {
                    return _ListMDIndicador = new List<vw_md_Ref_indicador>();
                }
                else
                {
                    return _ListMDIndicador;
                }

            }

            set
            {
                _ListMDIndicador = value;
            }
        }

        private List<Managment_md_Ref_obligacionesResult> _Lista;
        public List<Managment_md_Ref_obligacionesResult> Lista
        {
            get
            {
                if (_Lista == null)
                {
                    return _Lista = new List<Managment_md_Ref_obligacionesResult>();
                }
                else
                {
                    return _Lista;
                }


            }

            set
            {
                _Lista = value;
            }
        }

        private List<Managment_md_Ref_obligacionesResult> _Lista2;
        public List<Managment_md_Ref_obligacionesResult> Lista2
        {
            get
            {
                if (_Lista2 == null)
                {
                    return _Lista2 = new List<Managment_md_Ref_obligacionesResult>();
                }
                else
                {
                    return _Lista2;
                }


            }

            set
            {
                _Lista2 = value;
            }
        }

        private md_obligaciones_contractuales _OBJObligacionesContractuales;
        public md_obligaciones_contractuales OBJObligacionesContractuales
        {
            get
            {
                if (_OBJObligacionesContractuales == null)
                {
                    return _OBJObligacionesContractuales = new md_obligaciones_contractuales();
                }
                else
                {
                    return _OBJObligacionesContractuales;
                }

            }

            set
            {
                _OBJObligacionesContractuales = value;
            }
        }

        private md_obligaciones_contractuales_detalle _OBJDetallle;
        public md_obligaciones_contractuales_detalle OBJDetallle
        {
            get
            {
                if (_OBJDetallle == null)
                {
                    return _OBJDetallle = new md_obligaciones_contractuales_detalle();
                }
                else
                {
                    return _OBJDetallle;
                }

            }

            set
            {
                _OBJDetallle = value;
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

        private List<Managment_md_Ref_obligacionesResult> _ListaDetalleOK;
        public List<Managment_md_Ref_obligacionesResult> ListaDetalleOK
        {
            get
            {
                if (_ListaDetalleOK == null)
                {
                    return _ListaDetalleOK = new List<Managment_md_Ref_obligacionesResult>();
                }
                else
                {
                    return _ListaDetalleOK;
                }

            }

            set
            {
                _ListaDetalleOK = value;
            }
        }

        public List<md_ref_puntos_dispensacion> _lisPuntosDispersacion;
        public List<md_ref_puntos_dispensacion> lisPuntosDispersacion
        {
            get
            {
                if (_lisPuntosDispersacion == null)
                {
                    _lisPuntosDispersacion = BusClass.PuntosDispercion();

                    _lisPuntosDispersacion = _lisPuntosDispersacion.OrderBy(X => X.nombre_esm).ToList();

                    return _lisPuntosDispersacion;
                }
                else
                {
                    return _lisPuntosDispersacion;
                }

            }

            set
            {
                _lisPuntosDispersacion = value;
            }
        }

        private List<md_Ref_proveedor> _Listproveedor;
        public List<md_Ref_proveedor> Listproveedor
        {
            get
            {
                if (_Listproveedor == null)
                {
                    return _Listproveedor = new List<md_Ref_proveedor>();
                }
                else
                {
                    return _Listproveedor;
                }

            }

            set
            {
                _Listproveedor = value;
            }
        }


        private List<vw_obligaciones_contractuales> _ListproveedorObligaciones;
        public List<vw_obligaciones_contractuales> ListproveedorObligaciones
        {
            get
            {
                if (_ListproveedorObligaciones == null)
                {
                    return _ListproveedorObligaciones = new List<vw_obligaciones_contractuales>();
                }
                else
                {
                    return _ListproveedorObligaciones;
                }

            }

            set
            {
                _ListproveedorObligaciones = value;
            }
        }


        public void ConsultaLista()
        {
            Listproveedor = BusClass.GetMD_Ref_proveedor();
        }

        #endregion



        #region FUNCIONES

        public Int32 InsertarObligaciones(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarObligaciones(OBJObligacionesContractuales, ref MsgRes);
        }

        public Int32 InsertarObligacionesDetalle(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarObligacionesDetalle(OBJDetallle, ref MsgRes);
        }
        public List<Managment_md_Ref_obligacionesResult> ListaObligacionesActivos(Int32 id_obligaciones_contractuales)
        {
            ListaDetalleOK = BusClass.DetalleRefObligaciones(id_obligaciones_contractuales, 2);
            return ListaDetalleOK;
        }

        public List<Managment_md_Ref_obligacionesResult> DetalleRefObligaciones(Int32 id_obligaciones_contractuales)
        {
            //return BusClass.DetalleRefIndicadores(id_indicadores_medicamentos,1);

            List<Managment_md_Ref_obligacionesResult> list1 = new List<Managment_md_Ref_obligacionesResult>();
            List<Managment_md_Ref_obligacionesResult> list3 = new List<Managment_md_Ref_obligacionesResult>();

            Lista = BusClass.DetalleRefObligaciones(id_obligaciones_contractuales, 1);
            Lista2 = BusClass.DetalleRefObligaciones(id_obligaciones_contractuales, 2);

            foreach (var item in Lista2)
            {
                Managment_md_Ref_obligacionesResult Obj = new Managment_md_Ref_obligacionesResult();

                Obj.id_md_ref_obligaciones = item.id_md_ref_obligaciones;
                Obj.descripcion_obligacion = item.descripcion_obligacion;
                Obj.peso = item.peso;

                foreach (var item2 in Lista)
                {
                    if (Obj.id_md_ref_obligaciones == item2.id_md_ref_obligaciones)
                    {
                        Obj.valor = item2.valor;
                        Obj.resultado = item2.resultado;
                        Obj.observaciones = item2.observaciones;
                        Obj.id_obligaciones_contractuales = id_obligaciones_contractuales;
                        Obj.Nro = item2.Nro;
                    }



                }


                list1.Add(Obj);
            }



            return list1;
        }

        public vw_total_md_obligaciones_detalle Total_DetalleObligacionesMD(Int32 id_obligaciones_contractuales)
        {
            return BusClass.Total_DetalleObligacionesMD(id_obligaciones_contractuales);

        }

        public void ActualizarObligacionesMD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarObligacionesMD(OBJObligacionesContractuales, ref MsgRes);
        }

        public List<md_obligaciones_contractuales_detalle> GetObligacionesDetalleID(Int32 id_md_ref_obligaciones, Int32 id_obligaciones_contractuales)
        {
            return BusClass.GetObligacionesDetalleID(id_md_ref_obligaciones, id_obligaciones_contractuales);
        }

        public void ActualizarObligacionesDetalleMD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarObligacionesDetalleMD(OBJDetallle, ref MsgRes);
        }


        public List<vw_obligaciones_contractuales> ObligacionesProveedor(String Proveedor)
        {
            ListproveedorObligaciones = BusClass.ObligacionesProveedor(Proveedor);

            return ListproveedorObligaciones;
        }
        #endregion
    }
}