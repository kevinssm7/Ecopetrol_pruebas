using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using System.ComponentModel.DataAnnotations;

namespace AsaludEcopetrol.Models.GestorDocumental
{
    public class GestorDocumental
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

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO CASO")]
        public decimal numero_caso { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DOCUMENTO")]
        public decimal numero_documento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PROCESO")]
        public int proceso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO DE DOCUMENTO")]
        public int tipo_documento { get; set; }


        [RegularExpression(@"^[a-zA-Z\W\S]{1,1000}$", ErrorMessage = "Maximo 1000 caracteres")]
        [Display(Name = "OBSERVACIONES")]
        public String observacion { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO DE FACTURA")]
        public int tipo_factura { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO DE CONSULTA")]
        public int tipo_consulta { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DE FACTURA")]
        public String numero_factura { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DE FACTURA")]
        public String numero_factura_cuentas { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DE FORMULA")]
        public String numero_formula { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "PROVEEDOR")]
        public String numero_proveedor { get; set; }

        //comunicaciones


        [Required(ErrorMessage = "***")]
        [Display(Name = "DIRIGIDO")]
        public Int32 id_com_digirido { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PUNTOS DISPENSACION")]
        public Int32 id_puntos_dispensacion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO")]
        public Int32 id_com_tipo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OBSERVACIONES")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,1000}$", ErrorMessage = "Maximo 1000 caracteres")]
        public String observaciones_comunicado { get; set; }

        [Display(Name = "SELECCIONE EL PDF")]
        public String cargue_pdf { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA")]
        public DateTime? fecha_co { get; set; }

        public DateTime? fecha_coOK { get; set; }

        public Int32 referencia { get; set; }


        /// 

        public int tipo { get; set; }

        public int marca { get; set; }

        public int id_indicadores_medicamentos { get; set; }


        public String camponuevoFFMM { get; set; }



        private List<Ref_procesos> _lstProcesos;
        public List<Ref_procesos> LstProcesos
        {
            get
            {
                if (_lstProcesos == null)
                {
                    if (SesionVar.ROL == "13")
                    {
                        return _lstProcesos = BusClass.GetProcesosGD().Where(l => l.id_proceso == 1).ToList();
                    }
                    else
                    {
                        return _lstProcesos = BusClass.GetProcesosGD();
                    }

                }
                else
                {
                    return _lstProcesos;
                }

            }

            set
            {
                _lstProcesos = value;
            }
        }


        private vw_md_consolidado_fac _lstConsolidado;
        public vw_md_consolidado_fac LstConsolidado
        {
            get
            {
                if (_lstConsolidado == null)
                {
                    return _lstConsolidado = new vw_md_consolidado_fac();
                }
                else
                {
                    return _lstConsolidado;
                }

            }

            set
            {
                _lstConsolidado = value;
            }
        }


        private List<vw_md_consolidado_fac> _lstConsolidadoDetalle;
        public List<vw_md_consolidado_fac> LstConsolidadoDetalle
        {
            get
            {
                if (_lstConsolidadoDetalle == null)
                {
                    return _lstConsolidadoDetalle = BusClass.MD_CosolidadofACDetalle(Convert.ToString(numero_factura));
                }
                else
                {
                    return _lstConsolidadoDetalle;
                }

            }

            set
            {
                _lstConsolidadoDetalle = value;
            }
        }

        private List<Ref_gestion_tipo_documental> _LstDocumnetal;
        public List<Ref_gestion_tipo_documental> LstDocumnetal
        {
            get
            {
                if (_LstDocumnetal == null)
                {
                    return _LstDocumnetal = new List<Ref_gestion_tipo_documental>();
                }
                else
                {
                    return _LstDocumnetal;
                }

            }

            set
            {
                _LstDocumnetal = value;
            }
        }

        private List<vw_g_documental_medicamentos> _LstFacMed;
        public List<vw_g_documental_medicamentos> LstFacMed
        {
            get
            {
                if (_LstFacMed == null)
                {
                    _LstFacMed = BusClass.ConsultaFactura(numero_factura);

                    _LstFacMed = _LstFacMed.Where(x => x.numero_formula == "0").ToList();

                    return _LstFacMed;
                }
                else
                {
                    return _LstFacMed;
                }

            }

            set
            {
                _LstFacMed = value;
            }
        }

        private List<vw_g_documental_medicamentos> _LstFacMedDetallle;
        public List<vw_g_documental_medicamentos> LstFacMedDetalle
        {
            get
            {
                if (_LstFacMedDetallle == null)
                {

                    _LstFacMed = BusClass.ConsultaFactura(numero_factura);

                    _LstFacMed = _LstFacMed.Where(x => x.numero_formula != "0").ToList();

                    return _LstFacMed;
                }
                else
                {
                    return _LstFacMedDetallle;
                }

            }

            set
            {
                _LstFacMedDetallle = value;
            }
        }

        private List<vw_fac_consolidado> _LstFacMed2;
        public List<vw_fac_consolidado> LstFacMed2
        {
            get
            {
                if (_LstFacMed2 == null)
                {
                    return _LstFacMed2 = BusClass.ConsultaFactura2(numero_factura);
                }
                else
                {
                    return _LstFacMed2;
                }

            }

            set
            {
                _LstFacMed2 = value;
            }
        }

        private List<vw_ffmm_consulta_radicacion_ips> _ListOdontogRadicacionIPS;
        public List<vw_ffmm_consulta_radicacion_ips> LisRadicacionIPSFacturas
        {
            get
            {
                if (_ListOdontogRadicacionIPS == null)
                {
                    return _ListOdontogRadicacionIPS = new List<vw_ffmm_consulta_radicacion_ips>();
                }
                else
                {
                    return _ListOdontogRadicacionIPS;
                }
            }
            set
            {
                _ListOdontogRadicacionIPS = value;
            }
        }

        private List<vw_fac_consolidado> _LstDocMed2;
        public List<vw_fac_consolidado> LstDocMed2
        {
            get
            {
                if (_LstDocMed2 == null)
                {
                    return _LstDocMed2 = BusClass.ConsultaDocumento2(Convert.ToString(numero_documento));
                }
                else
                {
                    return _LstDocMed2;
                }

            }

            set
            {
                _LstDocMed2 = value;
            }
        }


        private vw_g_documental_medicamentos _OBJGestionD;
        public vw_g_documental_medicamentos OBJGestionD
        {
            get
            {
                if (_OBJGestionD == null)
                {
                    _OBJGestionD = new vw_g_documental_medicamentos();
                }

                return _OBJGestionD;
            }
            set { _OBJGestionD = value; }
        }


        private List<vw_g_documental_medicamentos> _ListUrl;
        public List<vw_g_documental_medicamentos> ListUrl
        {
            get
            {
                if (_ListUrl == null)
                {
                    _ListUrl = new List<vw_g_documental_medicamentos>();

                }

                return _ListUrl;
            }
            set { _ListUrl = value; }
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

        private List<vw_table_gestion_MD> _GestionMd;
        public List<vw_table_gestion_MD> GestionMd
        {
            get
            {
                if (_GestionMd == null)
                {
                    _GestionMd = BusClass.ConsultaGestionMd();

                    _GestionMd = _GestionMd.Where(x => x.nombre_auditado == numero_proveedor).ToList();

                    return _GestionMd;
                }
                else
                {
                    return _GestionMd;
                }


            }

            set
            {
                _GestionMd = value;
            }
        }


        private List<vw_gestion_documental_med_cad_dtll> _GetCalidadDetalle;
        public List<vw_gestion_documental_med_cad_dtll> GetCalidadDetalle
        {
            get
            {
                if (_GetCalidadDetalle == null)
                {
                    return _GetCalidadDetalle = BusClass.ConsultaIdGestionDocumentalMDCalidad(id_indicadores_medicamentos, ref MsgRes);
                }
                else
                {
                    return _GetCalidadDetalle;
                }

            }

            set
            {
                _GetCalidadDetalle = value;
            }
        }

        private List<vw_gestion_documental_med_cad_dtll> _LstFacCalida;
        public List<vw_gestion_documental_med_cad_dtll> LstFacCalida
        {
            get
            {
                if (_LstFacCalida == null)
                {
                    _LstFacCalida = BusClass.ConsultaIdGestionDocumentalMDCalidad(id_indicadores_medicamentos, ref MsgRes);


                    return _LstFacCalida;
                }
                else
                {
                    return _LstFacCalida;
                }

            }

            set
            {
                _LstFacCalida = value;
            }
        }

        private List<GestionDocumentalMedicamentos> _LstGestionMed;
        public List<GestionDocumentalMedicamentos> LstGestionMed
        {
            get
            {
                if (_LstGestionMed == null)
                {
                    _LstGestionMed = BusClass.ConsultaGestionMedCargue();

                    return _LstGestionMed;
                }
                else
                {
                    return _LstGestionMed;
                }

            }

            set
            {
                _LstGestionMed = value;
            }
        }

        private List<vw_g_documental_medicamentos_masivo> _LstGestionMedMas;
        public List<vw_g_documental_medicamentos_masivo> LstGestionMedMas
        {
            get
            {
                if (_LstGestionMedMas == null)
                {
                    _LstGestionMedMas = BusClass.GestionDocumentalmasivo();

                    return _LstGestionMedMas;
                }
                else
                {
                    return _LstGestionMedMas;
                }

            }

            set
            {
                _LstGestionMedMas = value;
            }
        }

        private List<md_Ref_com_dirigido> _LstDirigido;
        public List<md_Ref_com_dirigido> LstDirigido
        {
            get
            {
                if (_LstDirigido == null)
                {
                    _LstDirigido = BusClass.GetDirigido();
                }

                return _LstDirigido;
            }
            set { _LstDirigido = value; }
        }

        private List<md_Ref_com_tipo> _LstMdTipo;
        public List<md_Ref_com_tipo> LstMdTipo
        {
            get
            {
                if (_LstMdTipo == null)
                {
                    _LstMdTipo = BusClass.GetMdTipo();
                }

                return _LstMdTipo;
            }
            set { _LstMdTipo = value; }
        }

        private md_comunicaciones _ObjComu;
        public md_comunicaciones ObjComu
        {
            get
            {
                if (_ObjComu == null)
                {
                    return _ObjComu = new md_comunicaciones();
                }
                else
                {
                    return _ObjComu;
                }

            }

            set
            {
                _ObjComu = value;
            }
        }


        private List<ManagmentRefPuntosDispersacionResult> _LstPuntosDispersacion;
        public List<ManagmentRefPuntosDispersacionResult> LstPuntosDispersacion
        {
            get
            {
                if (_LstPuntosDispersacion == null)
                {
                    _LstPuntosDispersacion = new List<ManagmentRefPuntosDispersacionResult>();
                }

                return _LstPuntosDispersacion;
            }
            set

            { _LstPuntosDispersacion = value; }


        }
        #endregion

        #region metodos
        public List<Ref_procesos> ConsultaProcesos()
        {
            return BusClass.GetProcesosGD();
        }

        public List<Ref_gestion_tipo_documental> ConsultaGestionTipoDocumental(int idproceso)
        {
            return LstDocumnetal = BusClass.ConsultaGestionTipoDocumental(idproceso);
        }

        public List<Ref_gestion_tipo_documental> ConsultaCodigoGD(Ref_gestion_tipo_documental objBusqueda, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaCodigoGD(objBusqueda, ref MsgRes);
        }
        public Int32 InsertarGestionDoc(GestionDocumentalMedicamentos ObjobjGD, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGestionDoc(ObjobjGD, ref MsgRes);
        }

        public Int32 InsertarGestionDocMedCalidad(GestionDocumentalMedicamentosCad ObjobjGD, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGestionDocMedCalidad(ObjobjGD, ref MsgRes);
        }

        public void InsertarGestionDocPQRS(GestionDocumentalPQRS ObjGest, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarGestionDocPQRS(ObjGest, ref MsgRes);
        }

        public void InsertarGestionDocVisitasCalidad(GestionDocumentalVisitasCalidad Obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarGestionDocVisitasCalidad(Obj, ref MsgRes);
        }

        public void ConsultaIdGestionDocumental(Int32 id_gestion_documental, ref MessageResponseOBJ MsgRes)
        {
            OBJGestionD = BusClass.ConsultaIdGestionDocumental(id_gestion_documental, ref MsgRes);
        }

        public List<vw_g_documental_medicamentos> ConsultaIdGestionDocumental2(Int32 id_gestion_documental, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdGestionDocumental2(id_gestion_documental, ref MsgRes);
        }

        public List<vw_g_documental_medicamentos> ConsultaIdGestionDocumentalFormula(String formula, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdGestionDocumentalFormula(formula, ref MsgRes);
        }

        public void BuscarRadicacionIPsId(Int32 id)
        {
            LisRadicacionIPSFacturas = BusClass.GetOdontogRadicacionIPS();
            LisRadicacionIPSFacturas = LisRadicacionIPSFacturas.Where(x => x.numero_factura == id).ToList();

        }
        public void ConsultaFactura()
        {
            LstConsolidado = BusClass.MD_CosolidadofAC(numero_factura);
        }

        public void ConsultaLista()
        {
            Listproveedor = BusClass.GetMD_Ref_proveedor();
        }

        public void EliminarDocumento_med_cal(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarDocumento_med_cal(id, ref MsgRes);
        }

        public void EliminarDocumento_med(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarDocumento_med(id, ref MsgRes);
        }

        public ecop_PQRS ConsultaPQRSbyNumCaso(string numcaso)
        {
            return BusClass.ConsultaPQRSbyNumCaso(numcaso);
        }

        public GestionDocumentalPQRS ConsultaGestorPQRSbyId(Int32 Id)
        {
            return BusClass.ConsultaGestorPQRSbyId(Id);
        }

        public List<GestionDocumentalPQRS> LstGestionDocPQRS(string numcaso)
        {
            return BusClass.ConsultanumcasoGestionDocumentalPQRS(numcaso);
        }

        public bool EliminarDocPQRS(Int32 id)
        {
            return BusClass.EliminarDocPQRS(id, ref MsgRes);
        }

        public void InsertarLogActividad(Log_GestionDocumental Log)
        {
            BusClass.InsertarLogActividad(Log);
        }

        public List<GestionDocumentalMedicamentos> TraerPdf()
        {
            return BusClass.TraerPdf();
        }

        public String ActualizarRutaByteMed(GestionDocumentalMedicamentos obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ActualizarRutaByteMed(obj, ref MsgRes);
        }
        public String ActualizarRutaBytePQRS(GestionDocumentalPQRS2 obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ActualizarRutaBytePQRS(obj, ref MsgRes);
        }
        public String ActualizarRutaByteMedCalidad(GestionDocumentalMedicamentosCad obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ActualizarRutaByteMedCalidad(obj, ref MsgRes);
        }


        public void InsertarComunicaciones(ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarComunicaciones(ObjComu, ref MsgRes);
        }

        public void ConsultaListaPD(int opc, ref MessageResponseOBJ MsgRes)
        {
            if (opc == 1)
            {
                LstPuntosDispersacion = BusClass.ConsultaListaPD(opc, ref MsgRes);
            }
            if (opc == 2)
            {

            }
        }

        public vw_gestion_documental_med_cad_dtll ConsultaIdGestionDocumentalMDCalId(Int32 id_gestion_documental__medicamentos_cad, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdGestionDocumentalMDCalId(id_gestion_documental__medicamentos_cad, ref MsgRes);
        }

        public List<GestionDocumentalPQRS2> GetUrlProyeccion(Int32 id)
        {

            List<GestionDocumentalPQRS2> ListUrl = BusClass.GetUrlProyeccion(id, ref MsgRes);

            //string dirpath = Path.Combine(Request.PhysicalApplicationPath);
            return ListUrl;
        }

        public List<GestionDocumentalPQRS> GetUrlDocumentosPqrs(Int32 id)
        {

            List<GestionDocumentalPQRS> ListUrl = BusClass.GetUrlDocumentosPqrs(id, ref MsgRes);

            //string dirpath = Path.Combine(Request.PhysicalApplicationPath);
            return ListUrl;
        }

        public List<management_masivo_pqrsResult> GestionDocumentalmasivo2()
        {
            return BusClass.GestionDocumentalmasivo2();
        }

        #endregion
    }
}