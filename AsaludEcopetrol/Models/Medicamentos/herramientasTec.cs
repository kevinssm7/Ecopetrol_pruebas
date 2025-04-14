using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class herramientasTec
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

        public int id_indicadores_medicamentos { get; set; }
        public int id_herramienta_tec_med { get; set; }
        public int id_md_detalle_tabla1 { get; set; }
        public int id_md_detalle_tabla2 { get; set; }
        public int id_md_detalle_tabla3 { get; set; }
        public int id_md_detalle_tabla4 { get; set; }
        public int id_categoria { get; set; }



        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA AUDITORIA")]
        public DateTime? fecha_auditoria { get; set; }
        public DateTime? fecha_auditoriaOK { get; set; }

        public String nombre_auditor { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public int ciudad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public int id_regional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE AUDITADO")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,50}$", ErrorMessage = "Maximo 50 caracteres")]
        public String nombre_auditado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PUNTOS DE DISPENSACION:")]
        public String nombre_farmacia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "RESULTADO")]
        public Decimal resultado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "HALLAZGOS")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        public String hallazgos { get; set; }

        public DateTime fecha_ingreso { get; set; }

        public String usuario_ingreso { get; set; }

        public String descripcion { get; set; }

        public int id_md_ref_indicador { get; set; }

        public int peso { get; set; }

        public Int32 valor { get; set; }

        public String observaciones { get; set; }

        public String file { get; set; }

        public String TIENE_DOCUMENTO { get; set; }

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
                    return _Listproveedor = BusClass.GetMD_Ref_proveedor();
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


        private List<vw_indicadores_medicamentos> _ListproveedorIndicador;
        public List<vw_indicadores_medicamentos> ListproveedorIndicador
        {
            get
            {
                if (_ListproveedorIndicador == null)
                {
                    return _ListproveedorIndicador = new List<vw_indicadores_medicamentos>();
                }
                else
                {
                    return _ListproveedorIndicador;
                }

            }

            set
            {
                _ListproveedorIndicador = value;
            }
        }

        private List<vw_herramientas_tecnologicas> _ListproveedorHerramienta;
        public List<vw_herramientas_tecnologicas> ListproveedorIndicadorHerramienta
        {
            get
            {
                if (_ListproveedorHerramienta == null)
                {
                    return _ListproveedorHerramienta = new List<vw_herramientas_tecnologicas>();
                }
                else
                {
                    return _ListproveedorHerramienta;
                }

            }

            set
            {
                _ListproveedorHerramienta = value;
            }
        }


        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO HALLAZGO")]
        public int tipo_hallazgo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "SOPORTE")]
        public String soporte { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PLAN MEJORA")]
        public String plan_mejora { get; set; }

        [Required(ErrorMessage = "***")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA LIMITE RESPUESTA")]
        public DateTime? fecha_limite_respuesta { get; set; }
        public DateTime? fecha_limite_respuestaok { get; set; }


        #endregion

        #region FUNCIONES

        public void ConsultaLista()
        {
            Listproveedor = BusClass.GetMD_Ref_proveedor();
        }

        public List<vw_herramientas_tecnologicas> IndicadoresProvvedorHerramientas(Int32 Proveeedor)
        {
            ListproveedorIndicadorHerramienta = BusClass.IndicadoresProvvedorHerramientas(Proveeedor);

            return _ListproveedorHerramienta;
        }

    
        public Int32 InsertarHerramientaTecnologica(md_herramienta_tec OBJHerramienta, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarHerramientaTecnologica(OBJHerramienta, ref MsgRes);
        }

        public Int32 InsertarDetallet1(List<md_herramienta_tec_detalle_t1> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarDetallet1(OBJDetalle, ref MsgRes);

        }

        public Int32 InsertarDetallet2(List<md_herramienta_tec_detalle_t2> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarDetallet2(OBJDetalle, ref MsgRes);
        }

        public Int32 InsertarDetallet3(List<md_herramienta_tec_detalle_t3> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarDetallet3(OBJDetalle, ref MsgRes);

        }

        public Int32 InsertarDetallet4(List<md_herramienta_tec_detalle_t4> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarDetallet4(OBJDetalle, ref MsgRes);
        }

        public List<md_ref_tabla1> ref_tabla1()
        {
            return BusClass.ref_tabla1();
        }

        public List<md_ref_tabla2> ref_tabla2()
        {
            return BusClass.ref_tabla2();
        }
        public List<md_ref_tabla3> ref_tabla3()
        {
            return BusClass.ref_tabla3();
        }
        public List<md_ref_tabla4> ref_tabla4()
        {
            return BusClass.ref_tabla4();
        }
        public List<vw_tabla1_categ> Tabla1Catg()
        {
            return BusClass.Tabla1Catg();
        }

        public List<vw_md_detalle_T1> Tabla1Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            return BusClass.Tabla1Detalle(id_cat, id_herramienta_tec);
        }
        public List<vw_md_detalle_T2> Tabla2Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            return BusClass.Tabla2Detalle(id_cat, id_herramienta_tec);
        }
        public List<vw_md_detalle_T3> Tabla3Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            return BusClass.Tabla3Detalle(id_cat, id_herramienta_tec);
        }
        public List<vw_md_detalle_T4> Tabla4Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            return BusClass.Tabla4Detalle(id_cat, id_herramienta_tec);
        }

        public void ActualizarDetallet1(md_herramienta_tec_detalle_t1 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarDetallet1(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarDetallet2(md_herramienta_tec_detalle_t2 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarDetallet2(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarDetallet3(md_herramienta_tec_detalle_t3 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarDetallet3(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarDetallet4(md_herramienta_tec_detalle_t4 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarDetallet4(OBJDetalleT, ref MsgRes);
        }

        public vw_md_total_T1 totalesT1(Int32 id)
        {
            return BusClass.totalesT1(id);
        }
        public vw_md_total_T2 totalesT2(Int32 id)
        {
            return BusClass.totalesT2(id);
        }
        public vw_md_total_T3 totalesT3(Int32 id)
        {
            return BusClass.totalesT3(id);
        }
        public vw_md_total_T4 totalesT4(Int32 id)
        {
            return BusClass.totalesT4(id);
        }

        public void ActualizarGeneral1(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarGeneral1(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarGeneral2(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarGeneral2(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarGeneral3(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarGeneral3(OBJDetalleT, ref MsgRes);
        }
        public void ActualizarGeneral4(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarGeneral4(OBJDetalleT, ref MsgRes);
        }

       


        #endregion

    }
}