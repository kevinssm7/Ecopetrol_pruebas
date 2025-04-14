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
    public class dispensaciones
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

        public int id_md_dispensacion_ambulatoria { get; set; }
        public int id_md_dispensacion_hospitalaria { get; set; }
        public int id_md_ref_dispens_ambulatoria { get; set; }
        public int id_md_ref_dispens_hospitalaria { get; set; }

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
        [Display(Name = "PUNTOS DE DISPENSACION:")]
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
        public Decimal resultado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "HALLAZGOS")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        public String hallazgos { get; set; }

        public DateTime fecha_ingreso { get; set; }

        public String usuario_ingreso { get; set; }

        public String descripcion { get; set; }

        public int peso { get; set; }

        public Int32 valor { get; set; }

        public String observaciones { get; set; }

        public String file { get; set; }


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

        private List<vw_dispensacion_ambulatoria> _ListproveedorAmbulatorio;
        public List<vw_dispensacion_ambulatoria> ListproveedorAmbulatorio
        {
            get
            {
                if (_ListproveedorAmbulatorio == null)
                {
                    return _ListproveedorAmbulatorio = new List<vw_dispensacion_ambulatoria>();
                }
                else
                {
                    return _ListproveedorAmbulatorio;
                }

            }

            set
            {
                _ListproveedorAmbulatorio = value;
            }
        }

        private List<vw_dispensacion_hospitalaria> _ListproveedorHospitalario;
        public List<vw_dispensacion_hospitalaria> ListproveedorHospitalario
        {
            get
            {
                if (_ListproveedorHospitalario == null)
                {
                    return _ListproveedorHospitalario = new List<vw_dispensacion_hospitalaria>();
                }
                else
                {
                    return _ListproveedorHospitalario;
                }

            }

            set
            {
                _ListproveedorHospitalario = value;
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


        #endregion

        #region FUNCIONES

        public void ConsultaLista()
        {
            Listproveedor = BusClass.GetMD_Ref_proveedor();
        }

        public List<vw_dispensacion_ambulatoria> AmbulatorioProvvedor(String Proveeedor)
        {
            ListproveedorAmbulatorio = BusClass.AmbulatorioProvvedor(Proveeedor);

            return ListproveedorAmbulatorio ;
        }
        public List<vw_dispensacion_hospitalaria> hospitalarioProvvedor(String Proveeedor)
        {
            ListproveedorHospitalario = BusClass.hospitalarioProvvedor(Proveeedor);

            return ListproveedorHospitalario;
        }

        public Int32 Insertardispensacion_ambulatoria(md_dispensacion_ambulatoria OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Insertardispensacion_ambulatoria(OBJDetalle, ref MsgRes);
        }

        public Int32 Insertardispensacion_Hospitalaria(md_dispensacion_hospitalaria OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Insertardispensacion_Hospitalaria(OBJDetalle, ref MsgRes);
        }

        public Int32 Insertardispensacion_ambulatoriaDetalle(md_dispensacion_ambulatoria_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Insertardispensacion_ambulatoriaDetalle(OBJDetalle, ref MsgRes);

        }

        public Int32 Insertardispensacion_HospitalariaDetalle(md_dispensacion_hospitalaria_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Insertardispensacion_HospitalariaDetalle(OBJDetalle, ref MsgRes);
        }

        public List<md_ref_dispens_ambulatoria> RefDispersacionAmbulatoria()
        {
            return BusClass.RefDispersacionAmbulatoria();
        }

        public List<md_ref_dispens_hospitalaria> RefDispersacionHospitalaria()
        {
            return BusClass.RefDispersacionHospitalaria();
        }

        public List<Managment_md_Ref_ambulatorioResult> DetalleRefAmbulatorio(Int32 id_md_dispensacion_ambulatoria)
        {
            return BusClass.DetalleRefAmbulatorio(id_md_dispensacion_ambulatoria);
        }

        public List<Managment_md_Ref_hospitalarioResult> DetalleRefHospitalario(Int32 id_md_dispensacion_Hospitalaria)
        {
            return BusClass.DetalleRefHospitalario(id_md_dispensacion_Hospitalaria);
        }

        public List<md_dispensacion_ambulatoria_detalle> GetAmbulatorioDetalleID(Int32 id_md_ref_dispens_ambulatoria, Int32 id_md_dispensacion_ambulatoria)
        {
            return BusClass.GetAmbulatorioDetalleID(id_md_ref_dispens_ambulatoria, id_md_dispensacion_ambulatoria);
        }

        public List<md_dispensacion_hospitalaria_detalle> GetHospitalarioDetalleID(Int32 id_md_ref_dispens_hospitalaria, Int32 id_md_dispensacion_hospitalaria)
        {
            return BusClass.GetHospitalarioDetalleID(id_md_ref_dispens_hospitalaria, id_md_dispensacion_hospitalaria);
        }

        public void ActualizarDispersacionAmbulatorio(md_dispensacion_ambulatoria_detalle OBJMD, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarDispersacionAmbulatorio(OBJMD, ref MsgRes);
        }
        public void ActualizarDispersacionHospitalizacion(md_dispensacion_hospitalaria_detalle OBJMD, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarDispersacionHospitalizacion(OBJMD, ref MsgRes);
        }

        public vw_md_total_ambulatoria Total_DetalleAmbulatoria(Int32 id_md_dispensacion_ambulatoria)
        {
            return BusClass.Total_DetalleAmbulatoria(id_md_dispensacion_ambulatoria);
        }

        public vw_md_total_hospitalaria Total_DetalleHospitalaria(Int32 id_md_dispensacion_hospitalaria)
        {
            return BusClass.Total_DetalleHospitalaria(id_md_dispensacion_hospitalaria);
        }

        public void ActualizarAmbulatoriaMD(md_dispensacion_ambulatoria OBJMD, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarAmbulatoriaMD(OBJMD, ref MsgRes);
        }

        public void ActualizarHospitalariaMD(md_dispensacion_hospitalaria OBJMD, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarHospitalariaMD(OBJMD, ref MsgRes);
        }

        /*Alexis quiñones castillo*/
        public void InsertarCargueMasivoDispensaciones(dispensacion_cargue_base obj, List<dispensacion_cargue_base_dtll> lista, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarCargueMasivoDispensaciones(obj, lista, ref MsgRes);
        }

        #endregion

    }
}