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
    public class GestionIndicadores
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

        public int id_md_dispensacion_ambulatoria { get; set; }

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

        public int id_md_indicador_detalle { get; set; }
        public int id_gestion_documental__medicamentos_cad { get; set; }

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

        public int id_md_ref_indicador { get; set; }

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

        private List<Managment_md_Ref_indicadorResult> _Lista;
        public List<Managment_md_Ref_indicadorResult> Lista
        {
            get
            {
                if (_Lista == null)
                {
                    return _Lista = new List<Managment_md_Ref_indicadorResult>();
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

        private List<Managment_md_Ref_indicadorResult> _Lista2;
        public List<Managment_md_Ref_indicadorResult> Lista2
        {
            get
            {
                if (_Lista2 == null)
                {
                    return _Lista2 = new List<Managment_md_Ref_indicadorResult>();
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

        private md_indicadores _OBJIndicadores;
        public md_indicadores OBJIndicadores
        {
            get
            {
                if (_OBJIndicadores == null)
                {
                    return _OBJIndicadores = new md_indicadores();
                }
                else
                {
                    return _OBJIndicadores;
                }

            }

            set
            {
                _OBJIndicadores = value;
            }
        }

        private md_indicadores_detalle _OBJDetallle;
        public md_indicadores_detalle OBJDetallle
        {
            get
            {
                if (_OBJDetallle == null)
                {
                    return _OBJDetallle = new md_indicadores_detalle();
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

        private List<Managment_md_Ref_indicadorResult> _ListaDetalleOK;
        public List<Managment_md_Ref_indicadorResult> ListaDetalleOK
        {
            get
            {
                if (_ListaDetalleOK == null)
                {
                    return _ListaDetalleOK = new List<Managment_md_Ref_indicadorResult>();
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

        

        #endregion

        #region FUNCIONES


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

        public List<md_indicadores_detalle> GetIndicadoresDetalle(Int32 id_indicadores_medicamentos)
        {
            return BusClass.GetIndicadoresDetalle(id_indicadores_medicamentos);
        }

        public List<vw_md_Ref_indicador> GetMDIndicadores()
        {
            return BusClass.GetMDIndicadores();
        }

        public Int32 InsertarIndicador(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarIndicador(OBJIndicadores, ref MsgRes);
        }


        public Int32 InsertarIndicadorDetalle(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarIndicadorDetalle(OBJDetallle, ref MsgRes);
        }

        public void ActualizarIndicadoresMedicamentos(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarIndicadoresMedicamentos(OBJDetallle, ref MsgRes);
        }


        public List<md_indicadores_detalle> GetIndicadoresDetalleID(Int32 id_md_ref_indicador, Int32 id_indicadores_medicamentos)
        {
            return BusClass.GetIndicadoresDetalleID(id_md_ref_indicador, id_indicadores_medicamentos);
        }

        public List<Managment_md_Ref_indicadorResult> DetalleRefIndicadores(Int32 id_indicadores_medicamentos)
        {
            //return BusClass.DetalleRefIndicadores(id_indicadores_medicamentos,1);

            List<Managment_md_Ref_indicadorResult> list1 = new List<Managment_md_Ref_indicadorResult>();
            List<Managment_md_Ref_indicadorResult> list3 = new List<Managment_md_Ref_indicadorResult>();

            Lista = BusClass.DetalleRefIndicadores(id_indicadores_medicamentos, 1);
            Lista2 = BusClass.DetalleRefIndicadores(id_indicadores_medicamentos, 2);

            foreach (var item in Lista2)
            {

                Managment_md_Ref_indicadorResult Obj = new Managment_md_Ref_indicadorResult();
                
                Obj.id_md_ref_indicador = item.id_md_ref_indicador;
                Obj.descripcion = item.descripcion;
                Obj.peso = item.peso;

                foreach (var item2 in Lista)
                {
                    if (Obj.id_md_ref_indicador == item2.id_md_ref_indicador )
                    {
                        Obj.valor = item2.valor;
                        Obj.resultado = item2.resultado;
                        Obj.observaciones = item2.observaciones;
                        Obj.TIENE_DOCUMENTO = item2.TIENE_DOCUMENTO;
                        TIENE_DOCUMENTO = item2.TIENE_DOCUMENTO;
                        Obj.id_indicadores_medicamentos = id_indicadores_medicamentos;
                        Obj.Nro = item2.Nro;
                        Obj.imagenes = item2.imagenes;
                    }
                }
                list1.Add(Obj);
            }
            return list1;
        }
   
        public void ConsultaLista(int opc, string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            ListCiudades = BusClass.GetCiudades();
        }

        public void ActualizarIndicadoresMD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarIndicadoresMD(OBJIndicadores, ref MsgRes);
        }

        public vw_total_md_detalle Total_DetalleIndMD(Int32 id_indicadores_medicamentos)
        {
            return BusClass.Total_DetalleIndMD(id_indicadores_medicamentos);
        }
        
        public List<Ref_gestion_tipo_documental> ConsultaCodigoGD(Ref_gestion_tipo_documental objBusqueda, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaCodigoGD(objBusqueda, ref MsgRes);
        }

        public Int32 InsertarGestionDocMedCalidad(GestionDocumentalMedicamentosCad ObjobjGD, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGestionDocMedCalidad(ObjobjGD, ref MsgRes);
        }
        
        public List<Managment_md_Ref_indicadorResult> ListaIndicadoresActivos(Int32 id_indicadores_medicamentos)
        {
            ListaDetalleOK = BusClass.DetalleRefIndicadores(id_indicadores_medicamentos, 2);
            return ListaDetalleOK;
        }

        public IEnumerable<vw_md_Ref_indicador_datalle> GetVwIndicadoresDetalle(Int32 id_indicadores_medicamentos)
        {
            return BusClass.GetVwIndicadoresDetalle(id_indicadores_medicamentos);
        }

        public void ConsultaLista()
        {
            Listproveedor = BusClass.GetMD_Ref_proveedor();
        }

        public List<vw_indicadores_medicamentos> IndicadoresProvvedor(String Proveeedor)
        {
            ListproveedorIndicador = BusClass.IndicadoresProvvedor(Proveeedor);

            return ListproveedorIndicador;
        }

        public List<vw_gestionDocumentalCad> GestionDocumentalMedCalidad(Int32 id, Int32 id2)
        {
            return BusClass.GestionDocumentalMedCalidad(id, id2);
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

        #endregion

    }
}