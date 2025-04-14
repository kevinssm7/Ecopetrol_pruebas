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
    public class SeguimientoPendientes
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

        private md_seguimiento_pendientes _OBJSeguimientoPendientes;
        public md_seguimiento_pendientes OBJSeguimientoPendientes
        {
            get
            {
                if (_OBJSeguimientoPendientes == null)
                {
                    return _OBJSeguimientoPendientes = new md_seguimiento_pendientes();
                }
                else
                {
                    return _OBJSeguimientoPendientes;
                }

            }

            set
            {
                _OBJSeguimientoPendientes = value;
            }
        }

        private List<Managment_md_Ref_seguimiento_pendientesResult> _ListaDetalleOK;
        public List<Managment_md_Ref_seguimiento_pendientesResult> ListaDetalleOK
        {
            get
            {
                if (_ListaDetalleOK == null)
                {
                    return _ListaDetalleOK = new List<Managment_md_Ref_seguimiento_pendientesResult>();
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

        private md_seguimiento_pendientes_detalle _OBJSeguimientoPendientesDetalle;
        public md_seguimiento_pendientes_detalle OBJSeguimientoPendientesDetalle
        {
            get
            {
                if (_OBJSeguimientoPendientesDetalle == null)
                {
                    return _OBJSeguimientoPendientesDetalle = new md_seguimiento_pendientes_detalle();
                }
                else
                {
                    return _OBJSeguimientoPendientesDetalle;
                }

            }

            set
            {
                _OBJSeguimientoPendientesDetalle = value;
            }
        }

        private List<Managment_md_Ref_seguimiento_pendientesResult> _Lista;
        public List<Managment_md_Ref_seguimiento_pendientesResult> Lista
        {
            get
            {
                if (_Lista == null)
                {
                    return _Lista = new List<Managment_md_Ref_seguimiento_pendientesResult>();
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

        private List<Managment_md_Ref_seguimiento_pendientesResult> _Lista2;
        public List<Managment_md_Ref_seguimiento_pendientesResult> Lista2
        {
            get
            {
                if (_Lista2 == null)
                {
                    return _Lista2 = new List<Managment_md_Ref_seguimiento_pendientesResult>();
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


        private List<vw_md_seguimiento_pendientes> _ListproveedorSeguimiento;
        public List<vw_md_seguimiento_pendientes> ListproveedorSeguimiento
        {
            get
            {
                if (_ListproveedorSeguimiento == null)
                {
                    return _ListproveedorSeguimiento = new List<vw_md_seguimiento_pendientes>();
                }
                else
                {
                    return _ListproveedorSeguimiento;
                }

            }

            set
            {
                _ListproveedorSeguimiento = value;
            }
        }



        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        public int id_md_seguimiento_pendientes{ get; set; }

        public int id_md_seguimiento_pendientes_detalle { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA AUDITORIA")]
        public DateTime? fecha_auditoria { get; set; }
        public DateTime? fecha_auditoriaOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE AUDITOR")]
        public String nombre_auditor { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public String ciudad { get; set; }

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

        public int id_md_ref_seguimiento_pendiente { get; set; }

        public int peso { get; set; }

        public Int32 valor { get; set; }

        public String observaciones { get; set; }

        public String file { get; set; }

        public String TIENE_DOCUMENTO { get; set; }

        #endregion

        #region FUNCIONES

        public List<sis_usuario> Usuarios()
        {
            return BusClass.GetSisUsuarioMd();
        }

        public Int32 InsertarSeguimientoPendientes(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarSeguimientoPendientes(OBJSeguimientoPendientes, ref MsgRes);
        }

        public Int32? InsertarSeguimientoPendientesDetalle(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarSeguimientoPendientesDetalle(OBJSeguimientoPendientesDetalle, ref MsgRes);
        }

        public List<Managment_md_Ref_seguimiento_pendientesResult> ListaSeguimientoPendietnes(Int32 id_md_seguimiento_pendientes)
        {
            ListaDetalleOK = BusClass.DetalleRefSeguimientoPendientes(id_md_seguimiento_pendientes, 2);
            return ListaDetalleOK;
        }

        public vw_total_md_seguimiento_detalle Total_DetalleSeguimientoPendientesMD(Int32 id_md_seguimiento_pendientes)
        {
            return BusClass.Total_DetalleSeguimientoPendientesMD(id_md_seguimiento_pendientes);

        }

        public void ActualizarSeguimientoPendientesMD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarSeguimientoPendientesMD(OBJSeguimientoPendientes, ref MsgRes);
        }

        public void ActualizarSeguimientoPendientesDetalleMD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarSeguimientoPendientesDetalleMD(OBJSeguimientoPendientesDetalle, ref MsgRes);
        }

        public List<Managment_md_Ref_seguimiento_pendientesResult> DetalleRefSeguimientoPendientes(Int32 id_md_seguimiento_pendientes)
        {
            //return BusClass.DetalleRefIndicadores(id_indicadores_medicamentos,1);

            List<Managment_md_Ref_seguimiento_pendientesResult> list1 = new List<Managment_md_Ref_seguimiento_pendientesResult>();
            List<Managment_md_Ref_seguimiento_pendientesResult> list3 = new List<Managment_md_Ref_seguimiento_pendientesResult>();

            Lista = BusClass.DetalleRefSeguimientoPendientes(id_md_seguimiento_pendientes, 1);
            Lista2 = BusClass.DetalleRefSeguimientoPendientes(id_md_seguimiento_pendientes, 2);

            foreach (var item in Lista2)
            {
                Managment_md_Ref_seguimiento_pendientesResult Obj = new Managment_md_Ref_seguimiento_pendientesResult();

                Obj.id_md_ref_seguimiento_pendiente = item.id_md_ref_seguimiento_pendiente;
                Obj.descripcion_seguimiento = item.descripcion_seguimiento;
                Obj.peso = item.peso;

                foreach (var item2 in Lista)
                {
                    if (Obj.id_md_ref_seguimiento_pendiente == item2.id_md_ref_seguimiento_pendiente)
                    {
                        Obj.valor = item2.valor;
                        Obj.resultado = item2.resultado;
                        Obj.observaciones = item2.observaciones;
                        Obj.id_md_seguimiento_pendientes = id_md_seguimiento_pendientes;
                        Obj.Nro = item2.Nro;
                    }



                }


                list1.Add(Obj);
            }



            return list1;
        }

        public List<md_seguimiento_pendientes_detalle> GetSeguimientoPendientesDetalleID(Int32 id_md_ref_seguimiento_pendientes, Int32 id_md_seguimiento_pendientes)
        {
            return BusClass.GetSeguimientoPendientesDetalleID(id_md_ref_seguimiento_pendientes, id_md_seguimiento_pendientes);
        }

        public void ConsultaLista()
        {
            Listproveedor = BusClass.GetMD_Ref_proveedor();
        }

        public List<vw_md_seguimiento_pendientes> SeguimientoPendientesProveedor(String Proveedor)
        {
            ListproveedorSeguimiento = BusClass.SeguimientoPendientesProveedor(Proveedor);

            return ListproveedorSeguimiento;
        }

        public List<md_ref_puntos_dispensacion> ListaPuntosDispersacion(Int32 id)
        {
            List<md_ref_puntos_dispensacion> list = new List<md_ref_puntos_dispensacion>();

            list = BusClass.PuntosDispercion();

            list = list.Where(X => X.id_md_ref_puntos_dispensacion == id).ToList();

            foreach (var item in list)
            {
                direccion_farmacia = item.direccion;
                telefono_farmacia = item.telefono;
                fuerza = item.fuerza;

            }

            return list;



        }

        #endregion

    }
}