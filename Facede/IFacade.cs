using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace Facede
{
    public interface IFacade
    {
        #region LOGIN

        void ActualizaContraseña(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes);

        List<sis_usuario> ValidaIngreso(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes);

        List<ManagmentMenuResult> ManagmentMenu(String Strusuario, ref MessageResponseOBJ MsgRes);

        List<sis_usuario> BuscaAuditorUsu(String strUsuario, ref MessageResponseOBJ MsgRes);

        List<sis_usuario> BuscaAuditorNom(String strNombre, ref MessageResponseOBJ MsgRes);

        void GestionUsuarios(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes);

        void CrearUsuairo(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes);

        DateTime ManagmentHora();



        #endregion

        #region CENSO

        List<vw_datos_censo> CensoDocumento(String Documento, ref MessageResponseOBJ MsgRes);

        List<vw_datos_censo> CensoFacturas(ref MessageResponseOBJ MsgRes);

        Int32 InsertarCenso(ecop_censo OBJ, ref MessageResponseOBJ MsgRes);

        List<vw_datos_censo> CensoId(Int32 Id, ref MessageResponseOBJ MsgRes);

        List<ecop_censo> GetCensoId(Int32 Id, ref MessageResponseOBJ MsgRes);

        void ActualizarCenso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes);

        void ActualizarCensoEgreso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes);

        void ActualizarCensoEgresoOK(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes);

        void ActualizaEgresoConcurrenciaOk(ecop_concurrencia ObjConcurrencia, ref MessageResponseOBJ MsgRes);

        List<base_beneficiarios> BeneficiariosDocumento(Decimal Documento, ref MessageResponseOBJ MsgRes);

        List<vw_concurrencia_evolucion_Contrato> ConsultaIdConcurreniaEvolucion(vw_concurrencia_evolucion_Contrato ObjAfiliado, ref MessageResponseOBJ MsgRes);

        List<vw_consulta_censo> ConsultaCenso(ref MessageResponseOBJ MsgRes);

        void CensoEgreso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes);

        #endregion

        #region COMMONCLASS


        List<Ref_tipo_documental> GetTipoDocumnetal();

        List<vw_ips_ciudad> GetIPS();

        List<Ref_ips_cuentas> GetPrstadorCuentas();

        List<Ref_ips_cuentas> GetPrstador();

        List<vw_ciudad_auditor> GetCiudad_auditor();

        List<vw_auditor> Get_auditor();

        List<Ref_origen_evento> GetOrigenEvento();

        List<Ref_regional> GetRefRegion();

        List<Ref_tipo_habitacion> GetTipoHabitacion();

        List<Ref_tipo_ingreso> GetTipoIngreso();

        List<Ref_condicion_alta_censo> GetCondicionAlta();

        List<Ref_cie10> GetCie10();

        List<Ref_cups> GetCups();

        List<Ref_cuentas_glosa> GetCuentaGlosa();

        List<Ref_responsable_glosa> GetResGlosa();

        List<Ref_condicion_del_egreso> GetCondicionEgreso();

        List<Ref_servicio_tratante> GetServiciotratante();

        List<Ref_salud_publica> GetSaludPublica();

        List<Ref_lesiones_severas_y_alto_costo> GetAltoCosto();

        List<vw_tablero_censo> GetTableroCenso();

        List<vw_tablero_concurrencia> GetTableroConcurrencia();

        List<Ref_ciudades> GetCiudades();

        List<vw_cie10> GetCie10Unido();

        List<Ref_causal_egreso> GetCausaEgreso();

        List<vw_consulta_alertas> GetConsultaAlertas();

        List<Total_ciudades> GetTotalCiudades();

        List<Ref_motivo_devolucion_fac> GetMotivoDevolucionFac();

        Int32 InsertarDevolucionFacturas(factura_devolucion OBJ, ref MessageResponseOBJ MsgRes);

        Int32 InsertarDevolucionFacturasDetalle(factura_devolucion_detalle OBJ, ref MessageResponseOBJ MsgRes);
        
        List<vw_sis_auditor_ciudad> GetCiudadesAuditor();

        List<Ref_hallazgos_RIPS> GetRefHallazgos();

        List<Ref_categorias_eventos_adv> GetRefCategoriaEvad();

        List<sis_auditor_regional> GetRegionalAuditor();

        List<Ref_motivo_reingreso> GetRefMotivoRiesgo();

        List<Ref_categorias_situaciones_de_calidad> GetRefCategoriaSituacion();

        List<vw_cie10_alertas> GetRefcie10Alertas();

        List<Ref_enfermedades_Huerfanas> GetRefHuerfanas();

        List<Ref_tipo_ahorro> GetRefTipoAhorro();

        #endregion

        #region CONCURRENCIA

        void ActualizaConcurrencia(ecop_concurrencia ObjConcurrencia, String User, String IPAddress, ref MessageResponseOBJ MsgRes);

        void ActualizaEgresoConcurrencia(ecop_concurrencia ObjConcurrencia, String User, String IPAddress, ref MessageResponseOBJ MsgRes);

        List<ecop_concurrencia> ConsultaCriterioIngresoActualizado(Int32 IdConcu, ref MessageResponseOBJ MsgRes);

        List<ecop_concurrencia_encuesta_satisfacion> ConsultaEncuestaCargada(Int32 IdConcu, ref MessageResponseOBJ MsgRes);

        void InsertaEgreso(egreso_auditoria_Hospitalaria Egreso, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes);

        List<vw_ciudad_ips> ConsultaIdConcurreniaciudad(vw_ciudad_ips ObjAfiliado, ref MessageResponseOBJ MsgRes);

        List<vw_consulta_concurrencia> ConsultaConcurrencia(ref MessageResponseOBJ MsgRes);

        List<vw_consulta_egreso> ConsultaEgreso(ref MessageResponseOBJ MsgRes);

        List<vw_consulta_eventos_adversos> ConsultaEventosAd(ref MessageResponseOBJ MsgRes);

        List<vw_consulta_situacion_calidad> ConsultaSituacionCal(ref MessageResponseOBJ MsgRes);

        List<vw_gestantes> ConsultaGestantes(ref MessageResponseOBJ MsgRes);

        List<vw_gestantes_sin> ConsultaGestantesSin(ref MessageResponseOBJ MsgRes);
        List<vw_Mortalidad> ConsultaMortalidad(ref MessageResponseOBJ MsgRes);

        List<vw_Mortalidad_sin> ConsultaMortalidadSin(ref MessageResponseOBJ MsgRes);

        List<vw_tipo_habitacion_censo> ConsultaTipoHabitacion(vw_tipo_habitacion_censo ObjAfiliado, ref MessageResponseOBJ MsgRes);

        void InsertarEncuestaConcurrencia(ecop_concurrencia_encuesta Encuesta, ref MessageResponseOBJ MsgRes);

        #endregion

        #region EVOLUCION
        void InsertaConcurrenciaEvolucion(ecop_concurrencia_evolucion Evolucion, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes);

        List<ecop_concurrencia_evolucion> ConsultaEvoluciones(ecop_concurrencia_evolucion ObjEvolu, ref MessageResponseOBJ MsgRes);

        void EliminarConcurrenciaEvolucion(ecop_concurrencia_evolucion ObjEvolucion, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes);

        List<ecop_concurrencia_evolucion_diag_def> ConsultaDiagnosticoDefinitivo(ecop_concurrencia_evolucion_diag_def Objdiagdef, ref MessageResponseOBJ MsgRes);
        
        void InsertaDiagnosticoDefinitivo(ecop_concurrencia_evolucion_diag_def Concurrencia_Diagnostico_Definitivo_id, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes);

        void InsertaGlosa(ecop_concurrencia_glosa ObjGlosa, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes);

        List<ecop_concurrencia_glosa> ConsultaGlosa(ecop_concurrencia_glosa ObjGlosa, ref MessageResponseOBJ MsgRes);


        List<Ref_eventos_adversos> GetEventosAdversos();

        List<Ref_grado_lesion> GetGradoLesion();

        List<Ref_factores_contribuyentes> GetFactoresContribuyentes();

        List<Ref_barreras_seguridad> GetBarrerasDeSeguridad();

        List<Ref_acciones_inseguras> GetAccionesInseguras();

        List<Ref_plan_de_manejo> GetPlanDeManejo();

        void InsertaEventoAdverso(ecop_concurrencia_eventos_adversos ObjEventoAdv, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes);
        
        List<ecop_concurrencia_eventos_adversos> ConsultaEventoAdverso(ecop_concurrencia_eventos_adversos ObjEventoAdverso, ref MessageResponseOBJ MsgRes);

        List<Ref_situaciones_de_calidad> GetSituacionesDeCalidad();

        void InsertaSituacionesCalidad(ecop_concurrencia_situaciones_de_calidad ObjSituacionCalid, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes);

        List<ecop_concurrencia_situaciones_de_calidad> ConsultaSituacionesCalidad(ecop_concurrencia_situaciones_de_calidad ObjSituCali, ref MessageResponseOBJ MsgRes);

        List<Ref_motivo_cancelacion_procedimiento> GetMotivoCancelacion();

        void InsertaProcedimientoQuirugicoCancelado(ecop_concurrencia_procedimientos_quirurgicos_cancelados ProcedimientoQuirCance, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes);

        List<ecop_concurrencia_procedimientos_quirurgicos_cancelados> ConsultaProcQuirurgicosCance(ecop_concurrencia_procedimientos_quirurgicos_cancelados ObjProcQuir, ref MessageResponseOBJ MsgRes);

        void InsertarNatalidad(natalidad_sin_concurrencia Natalidad, ref MessageResponseOBJ MsgRes);

        void InsertarMortalidad(mortalidad_sin_concurrencia Mortalidad, ref MessageResponseOBJ MsgRes);

        List<vw_tablero_eventos_adversos> ReportesEventoAdverso();

        void InsertarAlertasConcurrencia(alertas_generadas_concurrencia Alertas, ref MessageResponseOBJ MsgRes);

        void InsertarConcurrenciaAhorro(ecop_concurrencia_ahorro Ahorro, ref MessageResponseOBJ MsgRes);

        List<ecop_concurrencia_ahorro> ConsultaAhorro(ecop_concurrencia_ahorro ObjAhorro, ref MessageResponseOBJ MsgRes);

        List<Ref_causal_glosa> ConsultaCausalGlosa(int id_respnsable_glosa, ref MessageResponseOBJ MsgRes);

        #endregion

        #region CONSULTAS
        List<ManagmentAlertasCalidadResult> CuentaFechaCargue(Int32 Opc, DateTime FechaInicial, DateTime FechaFin, String strProveedor, String strEstado, ref MessageResponseOBJ MsgRes);

        List<ManagmentReportDevolucionFacResult> ConsultaReporteDevolucionFac(Int32 id_devolucion_factura);

        List<vw_Devoluciones_sin_gestionar> DevolucionesSinGestion();

        Int32 InsertarDevolucionGestionadas(factura_devolucion_gestionadas OBJ, ref MessageResponseOBJ MsgRes);

        Int32 InsertarFacturaSinCenso(factura_sin_censo OBJ, ref MessageResponseOBJ MsgRes);

        List<vw_hallazgos_RIPS> HallazgosRipsSinGestion();

        List<vw_facturas_sin_auditar> FacturasporAuditar();

        List<vw_costo_evitado> CostoEvitado(Int32 Id, ref MessageResponseOBJ MsgRes);

        List<vw_facturas_diagnosticos> DiagnosticosCuentas(Int32 Id, ref MessageResponseOBJ MsgRes);

        List<vw_ECOPETROL_DEVOLUCION_FAC> VwDevoluciones();

        List<vw_ECOPETROL_HALLAZGOS_RIPS> VwHallazgosRIPS();

        List<ECOPETROL_RECEPCION_FACTURAS> VwRecepcionFacturas();

        #endregion

        #region FACTURAS


        Int32 InsertarHallazgos(hallazgo_RIPS OBJ, ref MessageResponseOBJ MsgRes);
        
        Int32 InsertarHallazgosDetalle(hallazgo_RIPS_detalle OBJ, ref MessageResponseOBJ MsgRes);

        List<ManagmentReportHallazgosRipResult> ConsultaReporteHallazgosRips(Int32 id_hallazgo_RIPS);

        void ActualizaHallazgosRips(hallazgo_RIPS Objrips, ref MessageResponseOBJ MsgRes);

        List<factura_sin_censo> ConsultaFacturasSinAudi(Int32 id_factura_sin_censo);

        Int32 InsertarCostoEvitado(factura_sin_censo_cos_evitado Obj, ref MessageResponseOBJ MsgRes);

        Int32 InsertarDiagnosticoCuentas(factura_sin_censo_diagnosticos Obj, ref MessageResponseOBJ MsgRes);

        void ActualizaFacturaAuditada(factura_sin_censo ObjAudi, ref MessageResponseOBJ MsgRes);

        List<factura_devolucion> ConsultaDevolucionesFactura(String Numero_factura);

        List<factura_sin_censo> ConsultaFacturaNumero(String Numero_factura);

        List<factura_devolucion> ConsultaDevolucionesFacturaId(Int32 Id_devolucion);

        List<hallazgo_RIPS> ConsultaHallazgosId(Int32 Id_rips);
      

        #endregion

        #region RIPS
        /// <summary>
        /// Inserta los RIPS EN LA TABLA GLOBAL
        /// </summary>
        /// <param name="Objrips"></param>
        /// <returns></returns>
        Int32 InsertarRips(RIPS Objrips,  ref MessageResponseOBJ MsgRes);

        List<RIPS> ConsultaRips(Int32 IdRips, ref MessageResponseOBJ MsgRes);

        bool ActualizaRips(RIPS ObjRips, ref MessageResponseOBJ MsgRes);

        /// <summary>
        /// Inserta los rips en la tabla rips AC
        /// </summary>
        /// <param name="ObjripsAc"></param>
        /// <returns></returns>
        Int32 InsertarRipsAC(RIPS_AC ObjripsAc, ref MessageResponseOBJ MsgRes);
        List<RIPS_AC> ConsultaRipsAC(Int32 IdRips, ref MessageResponseOBJ MsgRes);

        /// <summary>
        /// Inserta los rips en la tabla rips AD
        /// </summary>
        /// <param name="ObjripsAc"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        Int32 InsertarRipsAD(List<RIPS_AD> ObjripsAD, ref MessageResponseOBJ MsgRes);
        
        /// <summary>
        /// insertar en rips af
        /// </summary>
        /// <param name="ObjripsAF"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        Int32 InsertarRipsAF(RIPS_AF ObjripsAF, ref MessageResponseOBJ MsgRes);

        /// <summary>
        /// insertar en rips ah
        /// </summary>
        /// <param name="ObjripsAH"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        Int32 InsertarRipsAH(RIPS_AH ObjripsAH, ref MessageResponseOBJ MsgRes);

        /// <summary>
        /// insertar en rips am
        /// </summary>
        /// <param name="ObjripsAM"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        Int32 InsertarRipsAM(RIPS_AM ObjripsAM, ref MessageResponseOBJ MsgRes);
   
        /// <summary>
        /// insertar en rips an
        /// </summary>
        /// <param name="ObjripsAN"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        Int32 InsertarRipsAN(RIPS_AN ObjripsAN, ref MessageResponseOBJ MsgRes);

        Int32 InsertarRipsAP(RIPS_AP ObjripsAP, ref MessageResponseOBJ MsgRes);

        Int32 InsertarRipsAT(RIPS_AT ObjripsAT, ref MessageResponseOBJ MsgRes);

        Int32 InsertarRipsAU(RIPS_AU ObjripsAU, ref MessageResponseOBJ MsgRes);

        Int32 InsertarRipsCT(RIPS_CT ObjripsCT, ref MessageResponseOBJ MsgRes);

        Int32 InsertarRipsUS(RIPS_US ObjripsUS, ref MessageResponseOBJ MsgRes);

        List<Ref_RIPS_Prestadores> ConsultaPrestadores(string codhabilitacion , ref MessageResponseOBJ MsgRes);

        #endregion

    }
}
